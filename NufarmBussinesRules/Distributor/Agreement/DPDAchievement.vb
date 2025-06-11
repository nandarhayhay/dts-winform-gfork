Imports System.Data
Imports System.Data.SqlClient
Imports NufarmBussinesRules
Imports NufarmBussinesRules.SharedClass
Namespace DistributorAgreement
    Public Class DPDAchievementR
        Inherits NufarmBussinesRules.DistributorAgreement.Target_Agreement : Implements IDisposable
   
        Public Sub New()
            MyBase.New()
        End Sub
        Private tblDiscProgressive As DataTable = Nothing
        Private tblPrevAchievement As DataTable = Nothing
        Private tblCurAchiement As DataTable = Nothing
        Public IsTransitionTime As Boolean = False
        Private curAgreeStartDate As Object = Nothing
        Private curAgreeEndDate As Object = Nothing
        Private PrevAgreementNo As String = ""
        Private tblAVGPrice As DataTable = Nothing
        Private mustDeletedBeforeInsert As Boolean = False
        Private MustReinsertedData As Boolean = True
        Dim tblGP As New DataTable("T_GP")
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
                .Add(New DataColumn("AVGPriceID", Type.GetType("System.Int32")))
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


                .Add(New DataColumn("GP_ID", Type.GetType("System.Int64")))
                .Item("GP_ID").DefaultValue = 0
                'ACH_HEADER_ID, DISTRIBUTOR_ID, AGREEMENT_NO, BRAND_ID, TARGET_FM, TARGET_PL, TARGET_VALUE, 

                'TOTAL_ACTUAL
                .Add(New DataColumn("TOTAL_ACTUAL", Type.GetType("System.Decimal")))
                .Item("TOTAL_ACTUAL").DefaultValue = 0
                'ACH_DISPRO
                .Add(New DataColumn("ACH_DISPRO", Type.GetType("System.Decimal")))
                .Item("ACH_DISPRO").DefaultValue = 0
                'DISC_QTY
                .Add(New DataColumn("ACH_BY_CAT", Type.GetType("System.Decimal")))
                .Item("ACH_BY_CAT").DefaultValue = 0
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
                '  TOTAL_CPQ2, TOTAL_PBQ2, 
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
                ' TOTAL_CPF1, TOTAL_CPF2, 
                .Add(New DataColumn("TOTAL_CPF1", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPF1").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPF2", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPF2").DefaultValue = 0

                'TOTAL_PBF2
                .Add(New DataColumn("TOTAL_PBF2", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBF2").DefaultValue = 0

                'TOTAL_PBF3
                .Add(New DataColumn("TOTAL_PBF3", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBF3").DefaultValue = 0


                .Add(New DataColumn("GPPBF2", Type.GetType("System.Decimal"))) ''diambil dari progressive discount sebelumnya
                .Item("GPPBF2").DefaultValue = 0

                .Add(New DataColumn("GPPBF3", Type.GetType("System.Decimal"))) ''diambil dari progressive discount sebelumnya
                .Item("GPPBF3").DefaultValue = 0

                ''untuk perhitungan saja bukan untuk ke database
                .Add(New DataColumn("TOTAL_CPQ1", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ1").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPQ2", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ2").DefaultValue = 0

                ' TOTAL_CPQ3,
                .Add(New DataColumn("TOTAL_CPQ3", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ3").DefaultValue = 0

                'ACTUAL_DIST,PO_DIST,PO_VALUE_DIST,DISC_DIST,CPQ1_DIST,CPQ2_DIST,CPQ3_DIST,CPF1_DIST,CPF2_DIST,PBF3_DIST

                .Add(New DataColumn("ACTUAL_DIST", Type.GetType("System.Decimal")))
                .Item("ACTUAL_DIST").DefaultValue = 0

                .Add(New DataColumn("PO_DIST", Type.GetType("System.Decimal")))
                .Item("PO_DIST").DefaultValue = 0

                .Add(New DataColumn("PO_VALUE_DIST", Type.GetType("System.Decimal")))
                .Item("PO_VALUE_DIST").DefaultValue = 0

                .Add(New DataColumn("DISC_DIST", Type.GetType("System.Decimal")))
                .Item("DISC_DIST").DefaultValue = 0

                .Add(New DataColumn("CPQ1_DIST", Type.GetType("System.Decimal")))
                .Item("CPQ1_DIST").DefaultValue = 0

                .Add(New DataColumn("CPQ2_DIST", Type.GetType("System.Decimal")))
                .Item("CPQ2_DIST").DefaultValue = 0

                .Add(New DataColumn("CPQ3_DIST", Type.GetType("System.Decimal")))
                .Item("CPQ3_DIST").DefaultValue = 0

                .Add(New DataColumn("CPF1_DIST", Type.GetType("System.Decimal")))
                .Item("CPF1_DIST").DefaultValue = 0

                .Add(New DataColumn("CPF2_DIST", Type.GetType("System.Decimal")))
                .Item("CPF2_DIST").DefaultValue = 0

                .Add(New DataColumn("PBF2_DIST", Type.GetType("System.Decimal")))
                .Item("PBF2_DIST").DefaultValue = 0

                .Add(New DataColumn("PBF3_DIST", Type.GetType("System.Decimal")))
                .Item("PBF3_DIST").DefaultValue = 0

                '.Add(New DataColumn("GPPBQ3", Type.GetType("System.Decimal")))
                '.Item("GPPBQ3").DefaultValue = 0

                '.Add(New DataColumn("GPPBQ4", Type.GetType("System.Decimal")))
                '.Item("GPPBQ4").DefaultValue = 0

                '.Add(New DataColumn("GPPBS2", Type.GetType("System.Decimal")))
                '.Item("GPPBS2").DefaultValue = 0

                '.Add(New DataColumn("GPPBYear", Type.GetType("System.Decimal")))
                '.Item("GPPBPYear").DefaultValue = 0

                .Add(New DataColumn("GPCPF1", Type.GetType("System.Decimal")))
                .Item("GPCPF1").DefaultValue = 0

                .Add(New DataColumn("GPCPF2", Type.GetType("System.Decimal")))
                .Item("GPCPF2").DefaultValue = 0

                .Add(New DataColumn("GPCPQ2", Type.GetType("System.Decimal")))
                .Item("GPCPQ2").DefaultValue = 0

                .Add(New DataColumn("GPCPQ3", Type.GetType("System.Decimal")))
                .Item("GPCPQ3").DefaultValue = 0

                '.Add(New DataColumn("GPCPS1", Type.GetType("System.Decimal")))
                '.Item("GPCPS1").DefaultValue = 0

                '.Add(New DataColumn("GPPBY", Type.GetType("System.Decimal")))
                '.Item("GPPBY").DefaultValue = 0
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


                .Add(New DataColumn("TOTAL_PBF2", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBF2").DefaultValue = 0


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
            End With
            Dim Key(1) As DataColumn : Key(0) = tblAchDetail.Columns("ACHIEVEMENT_BRANDPACK_ID")
            tblAchDetail.PrimaryKey = Key
        End Sub
        Public Shadows Function GetAgreementNo(ByVal Flag As String, Optional ByVal DistributorID As String = "", Optional ByVal AGREEMENT_NO As String = "", Optional ByVal DefaultMaxyear As Integer = 2) As DataView
            Try
                Query = "SET NOCOUNT ON;"
                Dim strFlag As String = Flag
                strFlag = strFlag.Remove(1, 1)
                If AGREEMENT_NO <> "" And DistributorID <> "" Then
                    Query &= vbCrLf
                    'If Me.IsTransitionTime Then
                    Query &= "SELECT TOP 100 AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                              " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%'))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                              " AND AA.QS_TREATMENT_FLAG IN('Q','F') ;"
                    'Else
                    '    Query &= "SELECT TOP 100 AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                    '          " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%'))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                    '          " AND AA.QS_TREATMENT_FLAG = @FLAG ;"
                    'End If

                ElseIf DistributorID <> "" Then
                    'If IsTransitionTime Then
                    Query &= "SELECT AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                              " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID ))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                              " AND AA.QS_TREATMENT_FLAG IN('Q','F')  AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - @DefMaxyear ;"
                    'Else
                    '    Query &= "SELECT AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                    '          " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID ))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                    '          " AND AA.QS_TREATMENT_FLAG = @FLAG  AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - @DefMaxyear ;"
                    'End If
                ElseIf AGREEMENT_NO <> "" Then
                    Query &= vbCrLf
                    'If IsTransitionTime Then
                    Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%' " & vbCrLf & _
                            " AND QS_TREATMENT_FLAG IN('Q','F') ;"
                    'Else
                    '    Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%' " & vbCrLf & _
                    '            " AND QS_TREATMENT_FLAG = @FLAG ;"
                    'End If

                Else
                    Query &= vbCrLf
                    'If IsTransitionTime Then
                    Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) - @DefMaxyear " & vbCrLf & _
                             " AND QS_TREATMENT_FLAG IN('Q','F') ;"
                    'Else
                    '    Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) - @DefMaxyear " & vbCrLf & _
                    '             " AND QS_TREATMENT_FLAG = @FLAG ;"
                    'End If

                End If
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Me.AddParameter("@DefMaxyear", SqlDbType.Int, DefaultMaxyear)
                Me.AddParameter("@FLAG", SqlDbType.Char, strFlag, 1)
                Dim dtTable As New DataTable("T_Agreement")
                dtTable.Clear() : Me.FillDataTable(dtTable)
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Shadows Function GetDistributorAgrement(ByVal Flag As String, Optional ByVal Searchstring As String = "") As DataView
            Try
                'Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                '        "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                '        " WHERE AA.END_DATE >=  GETDATE() AND AA.QS_TREATMENT_FLAG = '" & Flag.Remove(1, 1) & "' AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                '        " AND DR.DISTRIBUTOR_NAME LIKE '%" & Searchstring & "%';"
                Flag = Flag.Remove(1, 1)
                If Flag = "Q" Or Flag = "F" Then
                    If Searchstring = "" Then
                        'If IsTransitionTime Then
                        Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                                "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                " WHERE YEAR(AA.END_DATE) >=  YEAR(@GETDATE) - 1 AND AA.QS_TREATMENT_FLAG IN('Q','F') AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) "
                        'Else
                        '    Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                        '            "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                        '            " WHERE YEAR(AA.END_DATE) >=  YEAR(@GETDATE) - 1 AND AA.QS_TREATMENT_FLAG = @FLAG AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) "
                        'End If

                    Else
                        'hilangkan end_date karene custom search
                        'If IsTransitionTime Then
                        Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                                 "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                 " WHERE AA.QS_TREATMENT_FLAG = IN('Q','F') AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                                 " AND DR.DISTRIBUTOR_NAME LIKE '%" & Searchstring & "%';"
                        'Else
                        '    Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                        '             "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                        '             " WHERE AA.QS_TREATMENT_FLAG = @FLAG AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                        '             " AND DR.DISTRIBUTOR_NAME LIKE '%" & Searchstring & "%';"
                        'End If
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
        Public Shadows Function getAchievement(ByVal Flag As String, Optional ByVal DISTRIBUTOR_ID As String = "", Optional ByVal ListAGREEMENT_NO As List(Of String) = Nothing) As DataSet

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
                'ACHIEVEMENT_DISPRO, DISPRO, DISC_QTY, TOTAL_CPQ1,  TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3

                '-----------------------------------Header Query -->achievement header--------------------------
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                        "SELECT ACRH.ACH_HEADER_ID,ACRH.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,ACRH.AGREEMENT_NO,AA.START_DATE,AA.END_DATE,ACRH.FLAG,ACRH.BRAND_ID,BB.BRAND_NAME," & vbCrLf & _
                        "ISNULL(AP.AVGPRICE,0) AS AVG_PRICE_FM,ISNULL(AP.AVGPRICE_PL,0) AS AVG_PRICE_PL,ACRH.TARGET_FM,ACRH.TARGET_PL,ISNULL(AP.AVGPRICE,0) * ACRH.TOTAL_TARGET AS TARGET_VALUE,ACRH.TOTAL_PO_VALUE,ACRH.TOTAL_TARGET," & vbCrLf & _
                        " ACRH.TOTAL_PO,ACRH.TOTAL_ACTUAL,ACRH.BALANCE,ACRH.ACH_BY_CAT/100 AS ACH_BY_CAT,ACRH.ACH_DISPRO/100 AS ACHIEVEMENT_DISPRO,ACRH.DISPRO/100 AS DISPRO,  " & vbCrLf & _
                        " ACRH.DISC_QTY, ACRH.TOTAL_CPQ1,ACRH.TOTAL_CPQ2, " & vbCrLf & _
                        " ACRH.TOTAL_CPQ3, " & vbCrLf & _
                        " ACRH.TOTAL_CPF1,ACRH.TOTAL_CPF2,ACRH.TOTAL_PBF2,ACRH.TOTAL_PBF3,ACRH.[DESCRIPTIONS],ACRH.ACTUAL_DIST,ACRH.PO_DIST,ACRH.PO_VALUE_DIST,ACRH.DISC_DIST,ACRH.CPQ1_DIST,ACRH.CPQ2_DIST,ACRH.CPQ3_DIST,ACRH.CPF1_DIST,ACRH.CPF2_DIST,ACRH.PBF2_DIST,ACRH.PBF3_DIST " & vbCrLf & _
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
                If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                Else : Me.CreateCommandSql("", Query)
                End If
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
                Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; SELECT ACD.ACH_DETAIL_ID,ACD.ACH_HEADER_ID,ACRH.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,ACRH.AGREEMENT_NO,ACD.BRANDPACK_ID,BP.BRANDPACK_NAME," & vbCrLf & _
                          "ACD.TOTAL_PO,ACD.TOTAL_ACTUAL, ACD.DISC_QTY," & vbCrLf & _
                          " ACD.TOTAL_CPQ1,ACD.TOTAL_CPQ2,ACD.TOTAL_CPQ3,ACD.TOTAL_CPF1,ACD.TOTAL_CPF2,ACD.TOTAL_PBF2,ACD.TOTAL_PBF3,ACD.[DESCRIPTIONS]" & vbCrLf & _
                          " FROM ACHIEVEMENT_DETAIL ACD INNER JOIN ACHIEVEMENT_HEADER ACRH " & vbCrLf & _
                          " ON ACD.ACH_HEADER_ID = ACRH.ACH_HEADER_ID INNER JOIN BRND_BRANDPACK BP " & vbCrLf & _
                          "  ON ACD.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                          " INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = ACRH.DISTRIBUTOR_ID " & vbCrLf
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
                Me.DisposeTempDB()
                Me.CloseConnection() : Me.ClearCommandParameters()
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
        Public Function CalculateAchievement(ByVal Flag As String, Optional ByVal tblAgreement As DataTable = Nothing, Optional ByVal DISTRIBUTOR_ID As String = "") As DataSet
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
                    'edit after debugging
                    IsTransitionTime = StartDate >= New DateTime(2019, 8, 1) And EndDate <= New DateTime(2020, 7, 31)
                    PrevAgreementNo = ""
                    curAgreeStartDate = StartDate
                    curAgreeEndDate = EndDate
                    StartDateF1 = StartDate
                    EndDateF1 = StartDateF1.AddMonths(4).AddDays(-1)
                    StartDateF2 = EndDateF1.AddDays(1)
                    EndDateF2 = StartDateF2.AddMonths(4).AddDays(-1)
                    StartDateF3 = EndDateF2.AddDays(1)
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
                    Me.MessageError = MessageHeader & vbCrLf & MessageDetail
                    System.Windows.Forms.MessageBox.Show(Me.MessageError, "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                End If
                ''drop table ''tempd db
                Me.ClearCommandParameters()
                Dim ListAgreement As New List(Of String)
                For i As Integer = 0 To tblDistAgreement.Rows.Count - 1
                    ListAgreement.Add(tblDistAgreement.Rows(i)("AGREEMENT_NO"))
                Next
                Dim Ds As DataSet = Me.getAchievement(Flag, DISTRIBUTOR_ID, ListAgreement)
                'Me.DisposeTempDB()
                Return Ds
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.RollbackTransaction()
                Me.DisposeTempDB() : Me.CloseConnection()
                Throw ex
            End Try

        End Function
        Private Sub getTblCurProgAndPrevAchievement()
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT PRODUCT_CATEGORY,PS_CATEGORY,UP_TO_PCT,DISC_PCT, FLAG FROM AGREE_PROG_DISC_R " & vbCrLf & _
                    " WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO;"
            tblDiscProgressive = New DataTable("tblProgressive")
            Me.ResetCommandText(CommandType.Text, Query)
            setDataAdapter(Me.SqlCom).Fill(tblDiscProgressive)
            'Me.SqlCom.Parameters.RemoveAt("@START_DATE")
            'Me.SqlCom.Parameters.RemoveAt("@END_DATE")

            'GET achievement data from current agreement previous flag 
            Query = "SET NOCOUNT ON;" & vbCrLf
            If Me.IsTransitionTime Then
                Query = "SELECT ACHIEVEMENT_ID,AGREEMENT_NO + BRAND_ID AS AGREE_BRAND_ID,FLAG,DISPRO FROM ACCRUED_HEADER WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO AND FLAG IN('Q1','Q2','Q3') " & vbCrLf & _
                " UNION " & vbCrLf
            End If
            Query &= "SELECT ACH_HEADER_ID AS ACHIEVEMENT_ID,AGREEMENT_NO + BRAND_ID AS AGREE_BRAND_ID,FLAG,DISPRO FROM ACHIEVEMENT_HEADER WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO AND FLAG IN('F1','F2') "
            Me.ResetCommandText(CommandType.Text, Query)
            Me.tblCurAchiement = New DataTable("tblProgressive")
            setDataAdapter(Me.SqlCom).Fill(Me.tblCurAchiement)

            'GET achievement data from previous agreement agreement previous flag only PBF3
            'get previous agreement no 
            Query = "SET NOCOUNT ON;" & vbCrLf & _
            " DECLARE @V_PREV_AG_NO VARCHAR(25); " & vbCrLf & _
            " SET @V_PREV_AG_NO = (SELECT TOP 1 AA.AGREEMENT_NO FROM AGREE_AGREEMENT AA INNER JOIN DISTRIBUTOR_AGREEMENT DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
            "                       WHERE AA.AGREEMENT_NO != @AGREEMENT_NO AND AA.START_DATE < @START_DATE AND DATEDIFF(MONTH,START_DATE,END_DATE) =11 ORDER BY AA.START_DATE DESC);  " & vbCrLf & _
            " SELECT ACH_HEADER_ID AS ACHIEVEMENT_ID,AGREEMENT_NO + BRAND_ID AS AGREE_BRAND_ID,FLAG,DISPRO FROM ACHIEVEMENT_HEADER WHERE AGREEMENT_NO = @V_PREV_AG_NO AND FLAG = 'F3' ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.tblPrevAchievement = New DataTable("tblPreviousAchievement")
            setDataAdapter(Me.SqlCom).Fill(Me.tblPrevAchievement)
        End Sub
        Private Sub GenerateDiscount(ByVal FLAG As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal AgreementNO As String, _
            ByRef tblAchHeader As DataTable, ByRef tblAchDetail As DataTable, ByRef Message As String, ByRef HasTarget As Boolean, ByVal EndDateAgreement As DateTime)

            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT  1 WHERE EXISTS(SELECT APD.AGREEMENT_NO FROM AGREE_AGREEMENT AA INNER JOIN AGREE_PROG_DISC_R APD " & vbCrLf & _
                    "                       ON AA.AGREEMENT_NO = APD.AGREEMENT_NO WHERE RTRIM(LTRIM(AA.AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
                    "                       AND APD.FLAG = @FLAG)"

            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNO, 32)
            Me.AddParameter("@FLAG", SqlDbType.VarChar, FLAG)
            Dim retval As Object = Me.SqlCom.ExecuteScalar()
            If IsNothing(retval) Or IsDBNull(retval) Then : Me.ClearCommandParameters() : Message &= "FLAG for " & FLAG & vbCrLf & "Has not been defined yet" : Return : End If
            If CInt(retval) <= 0 Then : Me.ClearCommandParameters() : HasTarget = False : Message &= "FLAG for " & FLAG & vbCrLf & "Has not been defined yet" : Return : End If

            CreateOrRecreatTblAchHeader(tblAchHeader)
            CreateOrRecreateTblAchDetail(tblAchDetail)

            Me.FillAchHeaderAndDetail(AgreementNO, FLAG, tblAchHeader, tblAchDetail, StartDate, EndDate)

            If tblAchHeader.Rows.Count <= 0 Then : Me.ClearCommandParameters() : Return : Message &= "No data to calculate for " & FLAG : End If

            'get tbl progresive, and curent achievement dan previous achievement
            getTblCurProgAndPrevAchievement()
            UpdateTotalAllActualAndPO(tblAchHeader)
            'HITUNG DISCOUNT tblAchHeader (sudah include hitung previous discount)
            Me.CalculateHeaderRoundup(AgreementNO, FLAG, tblAchHeader)
            Me.ClearCommandParameters()
            'hitung disc detail sudah include penghitungan disc previos
            Me.CalculatePreviousDetail(FLAG, tblAchDetail, tblAchHeader)

            ''save to database
            Me.SaveToDataBase(AgreementNO, FLAG, tblAchHeader, tblAchDetail)
        End Sub
        Private Sub SaveToDataBase(ByVal AgreementNO As String, ByVal FLAG As String, ByRef tblAchHeader As DataTable, ByRef tblAchDetail As DataTable)

            'header dulu
            Dim RowsSelect() As DataRow = tblAchHeader.Select("")
            If RowsSelect.Length > 0 Then
                For i As Integer = 0 To RowsSelect.Length - 1
                    RowsSelect(i).SetAdded()
                Next
            End If
            'INSERT DETAIL
            Dim RowsSelectDetail = tblAchDetail.Select("")
            If RowsSelectDetail.Length > 0 Then
                For i As Integer = 0 To RowsSelectDetail.Length - 1
                    RowsSelectDetail(i).SetAdded()
                Next
            End If

            Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans

            ''delete dulu data by agreement dan flag
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
            "DELETE FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO AND FLAG = @FLAG);"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNO, 25)
            Me.AddParameter("@FLAG", SqlDbType.VarChar, FLAG)
            Me.SqlCom.ExecuteScalar()

            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "DELETE FROM ACHIEVEMENT_HEADER WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO AND FLAG = @FLAG;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.SqlCom.ExecuteScalar()

            'INSERT HEADER
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    " INSERT INTO ACHIEVEMENT_HEADER(ACH_HEADER_ID, AGREEMENT_NO, DISTRIBUTOR_ID, BRAND_ID, AvgPriceID, FLAG, TOTAL_TARGET, TARGET_FM, TARGET_PL, TOTAL_PO, " & vbCrLf & _
                    " TOTAL_PO_VALUE, TOTAL_ACTUAL, BALANCE, ACH_DISPRO, ACH_BY_CAT,DISPRO, DISC_QTY, TOTAL_CPQ1, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_CPF1, TOTAL_CPF2,TOTAL_PBF2, TOTAL_PBF3," & vbCrLf & _
                    " DESCRIPTIONS, ACTUAL_DIST,PO_DIST,PO_VALUE_DIST,DISC_DIST,CPQ1_DIST,CPQ2_DIST,CPQ3_DIST,CPF1_DIST,CPF2_DIST,PBF2_DIST,PBF3_DIST,GP_ID,CreatedDate, CreatedBy) " & vbCrLf & _
                    " VALUES(@ACH_HEADER_ID, @AGREEMENT_NO, @DISTRIBUTOR_ID, @BRAND_ID, @AvgPriceID, @FLAG, @TOTAL_TARGET, @TARGET_FM, @TARGET_PL, @TOTAL_PO, " & vbCrLf & _
                    " @TOTAL_PO_VALUE, @TOTAL_ACTUAL, @BALANCE, @ACH_DISPRO, @ACH_BY_CAT,@DISPRO, @DISC_QTY, @TOTAL_CPQ1, @TOTAL_CPQ2, @TOTAL_CPQ3, @TOTAL_CPF1, " & vbCrLf & _
                    " @TOTAL_CPF2,@TOTAL_PBF2, @TOTAL_PBF3,@DESCRIPTIONS,@ACTUAL_DIST,@PO_DIST,@PO_VALUE_DIST,@DISC_DIST,@CPQ1_DIST,@CPQ2_DIST,@CPQ3_DIST,@CPF1_DIST,@CPF2_DIST,@PBF2_DIST,@PBF3_DIST,@GP_ID,@CreatedDate, @CreatedBy);"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.ResetAdapterCRUD()
            With Me.SqlCom
                .Parameters.Add("@GP_ID", SqlDbType.BigInt, 0, "GP_ID")
                .Parameters.Add("@ACH_HEADER_ID", SqlDbType.VarChar, 55, "ACH_HEADER_ID")
                '.Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 25, "AGREEMENT_NO")
                .Parameters.Add("@DISTRIBUTOR_ID", SqlDbType.VarChar, 10, "DISTRIBUTOR_ID")
                .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 7, "BRAND_ID")
                .Parameters.Add("@AvgPriceID", SqlDbType.Int, 0, "AvgPriceID")
                '.Parameters.Add("@FLAG", SqlDbType.VarChar, 4, "FLAG")
                .Parameters.Add("@TOTAL_TARGET", SqlDbType.Decimal, 0, "TOTAL_TARGET")
                .Parameters.Add("@TARGET_FM", SqlDbType.Decimal, 0, "TARGET_FM")
                .Parameters.Add("@TARGET_PL", SqlDbType.Decimal, 0, "TARGET_PL")
                .Parameters.Add("@TOTAL_PO", SqlDbType.Decimal, 0, "TOTAL_PO")
                .Parameters.Add("@TOTAL_PO_VALUE", SqlDbType.Decimal, 0, "TOTAL_PO_VALUE")
                .Parameters.Add("@TOTAL_ACTUAL", SqlDbType.Decimal, 0, "TOTAL_ACTUAL")
                .Parameters.Add("@BALANCE", SqlDbType.Decimal, 0, "BALANCE")
                .Parameters.Add("@ACH_DISPRO", SqlDbType.Decimal, 0, "ACH_DISPRO")
                .Parameters.Add("@ACH_BY_CAT", SqlDbType.Decimal, 0, "ACH_BY_CAT")
                .Parameters.Add("@DISPRO", SqlDbType.Decimal, 0, "DISPRO")
                .Parameters.Add("@DISC_QTY", SqlDbType.Decimal, 0, "DISC_QTY")
                .Parameters.Add("@TOTAL_CPQ1", SqlDbType.Decimal, 0, "TOTAL_CPQ1")
                .Parameters.Add("@TOTAL_CPQ2", SqlDbType.Decimal, 0, "TOTAL_CPQ2")
                .Parameters.Add("@TOTAL_CPQ3", SqlDbType.Decimal, 0, "TOTAL_CPQ3")
                .Parameters.Add("@TOTAL_CPF1", SqlDbType.Decimal, 0, "TOTAL_CPF1")
                .Parameters.Add("@TOTAL_CPF2", SqlDbType.Decimal, 0, "TOTAL_CPF2")
                .Parameters.Add("@TOTAL_PBF2", SqlDbType.Decimal, 0, "TOTAL_PBF2")
                .Parameters.Add("@TOTAL_PBF3", SqlDbType.Decimal, 0, "TOTAL_PBF3")
                .Parameters.Add("@DESCRIPTIONS", SqlDbType.VarChar, 250, "DESCRIPTIONS")
                .Parameters.Add("@ACTUAL_DIST", SqlDbType.Decimal, 0, "ACTUAL_DIST")
                .Parameters.Add("@PO_DIST", SqlDbType.Decimal, 0, "PO_DIST")
                .Parameters.Add("@PO_VALUE_DIST", SqlDbType.Decimal, 0, "PO_VALUE_DIST")
                .Parameters.Add("@DISC_DIST", SqlDbType.Decimal, 0, "DISC_DIST")
                .Parameters.Add("@CPQ1_DIST", SqlDbType.Decimal, 0, "CPQ1_DIST")
                .Parameters.Add("@CPQ2_DIST", SqlDbType.Decimal, 0, "CPQ2_DIST")
                .Parameters.Add("@CPQ3_DIST", SqlDbType.Decimal, 0, "CPQ3_DIST")
                .Parameters.Add("@CPF1_DIST", SqlDbType.Decimal, 0, "CPF1_DIST")
                .Parameters.Add("@CPF2_DIST", SqlDbType.Decimal, 0, "CPF2_DIST")
                .Parameters.Add("@PBF2_DIST", SqlDbType.Decimal, 0, "PBF2_DIST")
                .Parameters.Add("@PBF3_DIST", SqlDbType.Decimal, 0, "PBF3_DIST")
                .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                Me.SqlDat.InsertCommand = Me.SqlCom
                Me.SqlDat.Update(RowsSelect) : Me.ClearCommandParameters()
            End With

            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    " INSERT INTO ACHIEVEMENT_DETAIL (ACH_DETAIL_ID, ACH_HEADER_ID, BRANDPACK_ID, TOTAL_PO, TOTAL_ACTUAL, DISC_QTY, TOTAL_CPQ1," & vbCrLf & _
                    " TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_CPF1, TOTAL_CPF2,TOTAL_PBF2, TOTAL_PBF3, DESCRIPTIONS, CreatedDate, CreatedBy)" & vbCrLf & _
                    " VALUES(@ACH_DETAIL_ID, @ACH_HEADER_ID, @BRANDPACK_ID, @TOTAL_PO, @TOTAL_ACTUAL, @DISC_QTY, @TOTAL_CPQ1, " & vbCrLf & _
                    " @TOTAL_CPQ2, @TOTAL_CPQ3, @TOTAL_CPF1, @TOTAL_CPF2,@TOTAL_PBF2,@TOTAL_PBF3, @DESCRIPTIONS, @CreatedDate, @CreatedBy) ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.ResetAdapterCRUD()
            With Me.SqlCom
                .Parameters.Add("@ACH_HEADER_ID", SqlDbType.VarChar, 55, "ACH_HEADER_ID")
                .Parameters.Add("@ACH_DETAIL_ID", SqlDbType.VarChar, 70, "ACH_DETAIL_ID")
                .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                .Parameters.Add("@TOTAL_PO", SqlDbType.Decimal, 0, "TOTAL_PO")
                .Parameters.Add("@TOTAL_ACTUAL", SqlDbType.Decimal, 0, "TOTAL_ACTUAL")
                .Parameters.Add("@DISC_QTY", SqlDbType.Decimal, 0, "DISC_QTY")
                .Parameters.Add("@TOTAL_CPQ1", SqlDbType.Decimal, 0, "TOTAL_CPQ1")
                .Parameters.Add("@TOTAL_CPQ2", SqlDbType.Decimal, 0, "TOTAL_CPQ2")
                .Parameters.Add("@TOTAL_CPQ3", SqlDbType.Decimal, 0, "TOTAL_CPQ3")
                .Parameters.Add("@TOTAL_CPF1", SqlDbType.Decimal, 0, "TOTAL_CPF1")
                .Parameters.Add("@TOTAL_CPF2", SqlDbType.Decimal, 0, "TOTAL_CPF2")
                .Parameters.Add("@TOTAL_PBF2", SqlDbType.Decimal, 0, "TOTAL_PBF2")
                .Parameters.Add("@TOTAL_PBF3", SqlDbType.Decimal, 0, "TOTAL_PBF3")
                .Parameters.Add("@DESCRIPTIONS", SqlDbType.VarChar, 250, "DESCRIPTIONS")
                .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                Me.SqlDat.InsertCommand = Me.SqlCom
                Me.SqlDat.Update(RowsSelectDetail)
                Me.ClearCommandParameters()
            End With
            Me.CommiteTransaction()
        End Sub
        Private Sub CalculatePreviousDetail(ByVal Flag As String, ByRef tblAchDetail As DataTable, ByVal tblAchHeader As DataTable)
            Dim DVPrevAch As DataView = Nothing, Descriptions As String = ""
            If Not IsNothing(Me.tblPrevAchievement) Then
                DVPrevAch = tblPrevAchievement.DefaultView()
            End If
            Dim DVCurAch As DataView = tblCurAchiement.DefaultView
            'Dim DVGivenProg As DataView = Me.tblGP.DefaultView
            'DVGivenProg.Sort = "IDApp"

            For i As Integer = 0 To tblAchHeader.Rows.Count - 1

                Dim RowHeader As DataRow = tblAchHeader.Rows(i), RowDetail As DataRow = Nothing
                Dim Dispro As Decimal = Convert.ToDecimal(RowHeader("DISPRO"))
                Dim GPID As Object = RowHeader("GP_ID")
                '===========COMMENT THIS AFTER DEBUGGING=========================
                'If BrandID = "77230" Or BrandID = "77240" Then
                '    Stop
                'End If
                '===============END COMMENT THIS AFTER DEBUGGING ==============================
                Dim AchHeaderID As String = RowHeader("ACH_HEADER_ID").ToString()
                Dim RowsDetail() As DataRow = tblAchDetail.Select("ACH_HEADER_ID = '" & AchHeaderID & "'")
                Dim Index As Integer = -1
                For i1 As Integer = 0 To RowsDetail.Length - 1
                    RowDetail = RowsDetail(i1)
                    Dim PrevDisPro As Decimal = 0, BonusQTy As Decimal = 0
                    Dim totalInvoiceBeforeF3 As Decimal = 0, totalInvoiceCurrentF1 As Decimal = 0, totalInvoiceCurrentF2 As Decimal = 0, _
                    totalInvoiceCurrentQ1 As Decimal = 0, totalInvoiceCurrentQ2 As Decimal = 0, totalInvoiceCurrentQ3 As Decimal = 0
                    Dim AchDetailID As String = RowDetail("ACH_DETAIL_ID")
                    Dim DiscQtyBefore As Decimal = 0

                    Descriptions = ""
                    Select Case Flag
                        Case "F3" 'CPQ1,CPQ2,CPQ3,F1,F2
                            If IsTransitionTime Then
                                If AchHeaderID.Contains("F3") Then
                                    AchHeaderID.Replace("F3", "")
                                End If
                                totalInvoiceCurrentQ1 = RowDetail("TOTAL_CPQ1")
                                'Dim AchHeaderIDQ1 As String,
                                Dim AchHeaderIDQ2 As String, AchHeaderIDQ3 As String
                                'Dim DiscQ1 As Decimal = 0, 
                                Dim DiscQ2 As Decimal = 0, DiscQ3 As Decimal = 0
                                totalInvoiceCurrentQ2 = RowDetail("TOTAL_CPQ2")
                                If CDec(totalInvoiceCurrentQ2) > 0 Then
                                    AchHeaderIDQ2 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                    AchHeaderIDQ2 = AchHeaderIDQ2 + "Q2"

                                    If CDec(RowHeader("GPCPQ2")) > 0 Then
                                        PrevDisPro = Convert.ToDecimal(RowHeader("GPCPQ2"))
                                    ElseIf Not IsNothing(DVCurAch) Then
                                        DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDQ2 & "'"
                                        If DVCurAch.Count > 0 Then
                                            PrevDisPro = DVCurAch(0)("DISPRO")
                                        End If
                                    End If
                                    If PrevDisPro > 0 Then
                                        DiscQ2 = (PrevDisPro / 100) * totalInvoiceCurrentQ2
                                        DiscQtyBefore += DiscQ2
                                        If Descriptions <> "" Then
                                            Descriptions &= ", "
                                        End If
                                        Descriptions &= String.Format("Q2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentQ2, DiscQ2)
                                    End If
                                End If

                                'TOTAL_CPQ3
                                totalInvoiceCurrentQ3 = RowDetail("TOTAL_CPQ3")
                                If CDec(totalInvoiceCurrentQ3) > 0 Then
                                    AchHeaderIDQ3 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                    AchHeaderIDQ3 = AchHeaderIDQ3 + "Q3"

                                    If CDec(RowHeader("GPCPQ3")) > 0 Then
                                        PrevDisPro = Convert.ToDecimal(RowHeader("GPCPQ3"))
                                    ElseIf Not IsNothing(DVCurAch) Then
                                        DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDQ3 & "'"
                                        If DVCurAch.Count > 0 Then
                                            PrevDisPro = DVCurAch(0)("DISPRO")
                                        End If
                                    End If
                                    If PrevDisPro > 0 Then
                                        DiscQ3 = (PrevDisPro / 100) * totalInvoiceCurrentQ3
                                        DiscQtyBefore += DiscQ3
                                        If Descriptions <> "" Then
                                            Descriptions &= ", "
                                        End If
                                        Descriptions &= String.Format("Q3 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentQ3, DiscQ3)
                                    End If
                                End If
                            End If
                            totalInvoiceCurrentF1 = RowDetail("TOTAL_CPF1")
                            Dim DiscF1 As Decimal = 0, DiscF2 As Decimal = 0
                            Dim AchHeaderIDF2 As String = "", AchHeaderIDF1 As String
                            If CDec(totalInvoiceCurrentF1) > 0 Then
                                AchHeaderIDF1 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF1 = AchHeaderIDF1 + "F1"

                                If CDec(RowHeader("GPCPF1")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPCPF1"))
                                ElseIf Not IsNothing(DVCurAch) Then
                                    DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF1 & "'"
                                    If DVCurAch.Count > 0 Then
                                        PrevDisPro = DVCurAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF1 = (PrevDisPro / 100) * totalInvoiceCurrentF1
                                    DiscQtyBefore += DiscF1
                                    If Descriptions <> "" Then
                                        Descriptions &= ", "
                                    End If
                                    Descriptions &= String.Format("F1 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF1, DiscF1)
                                End If
                            End If
                            totalInvoiceCurrentF2 = RowDetail("TOTAL_CPF2")
                            If CDec(totalInvoiceCurrentF2) > 0 Then
                                AchHeaderIDF2 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF2 = AchHeaderIDF2 + "F2"

                                If CDec(RowHeader("GPCPF2")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPCPF2"))
                                ElseIf Not IsNothing(DVCurAch) Then
                                    DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF2 & "'"
                                    If DVCurAch.Count > 0 Then
                                        PrevDisPro = DVCurAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF2 = (PrevDisPro / 100) * totalInvoiceCurrentF2
                                    DiscQtyBefore += DiscF2
                                    If Descriptions <> "" Then
                                        Descriptions &= ", "
                                    End If
                                    Descriptions &= String.Format("F2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF2, DiscF2)
                                End If
                            End If
                        Case "F2"
                            totalInvoiceCurrentF1 = RowDetail("TOTAL_CPF1")
                            Dim AchHeaderIDF1 As String = ""
                            Dim DiscF1 As Decimal = 0
                            If CDec(totalInvoiceCurrentF1) > 0 Then
                                AchHeaderIDF1 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF1 = AchHeaderIDF1 + "F1"
                                If CDec(RowHeader("GPCPF1")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPCPF1"))
                                ElseIf Not IsNothing(DVCurAch) Then
                                    DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF1 & "'"
                                    If DVCurAch.Count > 0 Then
                                        PrevDisPro = DVCurAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF1 = (PrevDisPro / 100) * totalInvoiceCurrentF1
                                    DiscQtyBefore = DiscF1
                                    Descriptions &= String.Format("F1 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF1, DiscF1)
                                End If
                            End If
                            totalInvoiceBeforeF3 = RowDetail("TOTAL_PBF3")
                            Dim AchHeaderIDF3 As String = "", DiscF3 As Decimal = 0
                            If CDec(totalInvoiceBeforeF3) > 0 Then
                                AchHeaderIDF3 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF3 = AchHeaderIDF3 + "F3"

                                If CDec(RowHeader("GPPBF3")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPPBF3"))
                                ElseIf Not IsNothing(DVPrevAch) Then
                                    DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF3 & "'"
                                    If DVPrevAch.Count > 0 Then
                                        PrevDisPro = DVPrevAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF3 = (PrevDisPro / 100) * totalInvoiceBeforeF3
                                    DiscQtyBefore += DiscF3
                                    If Descriptions <> "" Then
                                        Descriptions &= ", "
                                    End If
                                    Descriptions &= String.Format("F3 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF3, DiscF3)
                                End If
                            End If
                        Case "F1"
                            Dim totalInvoiceBeforeF2 = RowDetail("TOTAL_PBF2")
                            Dim AchHeaderIDF2 As String = "", DiscF2 As Decimal = 0
                            If CDec(totalInvoiceBeforeF2) > 0 And Not IsNothing(DVPrevAch) Then
                                AchHeaderIDF2 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF2 = AchHeaderIDF2 + "F2"

                                If CDec(RowHeader("GPPBF2")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPPBF2"))
                                ElseIf Not IsNothing(DVPrevAch) Then
                                    DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF2 & "'"
                                    If DVPrevAch.Count > 0 Then
                                        PrevDisPro = DVPrevAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF2 = (PrevDisPro / 100) * totalInvoiceBeforeF2
                                    DiscQtyBefore = DiscF2
                                    Descriptions &= String.Format("F2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF2, DiscF2)
                                End If
                            End If
                            totalInvoiceBeforeF3 = RowDetail("TOTAL_PBF3")
                            Dim AchHeaderIDF3 As String = "", DiscF3 As Decimal = 0
                            If CDec(totalInvoiceBeforeF3) > 0 And Not IsNothing(DVPrevAch) Then
                                AchHeaderIDF3 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF3 = AchHeaderIDF3 + "F3"

                                If CDec(RowHeader("GPPBF3")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPPBF3"))
                                ElseIf Not IsNothing(DVPrevAch) Then
                                    DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF3 & "'"
                                    If DVPrevAch.Count > 0 Then
                                        PrevDisPro = DVPrevAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF3 = (PrevDisPro / 100) * totalInvoiceBeforeF3
                                    DiscQtyBefore += DiscF3
                                    If Descriptions <> "" Then
                                        Descriptions &= ", "
                                    End If
                                    Descriptions &= String.Format("F3 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF3, DiscF3)
                                End If
                            End If
                    End Select
                    Dim TotalActual As Decimal = Convert.ToDecimal(RowDetail("TOTAL_ACTUAL"))
                    BonusQTy = (Dispro / 100) * TotalActual
                    BonusQTy += DiscQtyBefore

                    RowDetail.BeginEdit()
                    RowDetail("DISC_QTY") = BonusQTy
                    RowDetail("DESCRIPTIONS") = Descriptions
                    RowDetail.EndEdit()
                Next
            Next
            tblAchDetail.AcceptChanges()
        End Sub

        Private Function getDispro(ByVal percentAch As Decimal, ByVal ProdCat As String, ByVal PSCat As String, ByVal flag As String) As Decimal
            Dim Dispro As Decimal = 0
            Dim DVCurProgress As DataView = tblDiscProgressive.DefaultView()

            DVCurProgress.RowFilter = " FLAG = '" & flag & "' AND PRODUCT_CATEGORY = '" & ProdCat & "' AND PS_CATEGORY = '" & PSCat & "'"
            DVCurProgress.Sort = "UP_TO_PCT DESC"
            For i As Integer = 0 To DVCurProgress.Count - 1
                Dim UpToPCT As Decimal = Convert.ToDecimal(DVCurProgress(i)("UP_TO_PCT"))
                If percentAch >= UpToPCT Then
                    Dispro = Convert.ToDecimal(DVCurProgress(i)("DISC_PCT"))
                    Exit For
                End If
            Next
            Return Dispro
        End Function
        Private Sub UpdateTotalAllActualAndPO(ByRef tblAchHeader As DataTable)
            Dim listAgreeBrandIDs As New List(Of String)
            For i As Integer = 0 To tblAchHeader.Rows.Count - 1
                Dim AgreeBrandID As String = tblAchHeader.Rows(i)("AGREE_BRAND_ID").ToString()
                If Not listAgreeBrandIDs.Contains(AgreeBrandID) Then
                    listAgreeBrandIDs.Add(AgreeBrandID)
                End If
            Next
            'update all
            For i As Integer = 0 To listAgreeBrandIDs.Count - 1
                Dim rows() As DataRow = tblAchHeader.Select("AGREE_BRAND_ID = '" & listAgreeBrandIDs(i) & "'")
                Dim TotalPO As Decimal = 0, TotalActual As Decimal = 0, TotalPOValue As Decimal = 0, _
                TotalCPQ1 As Decimal = 0, TotalCPQ2 As Decimal = 0, TotalCPQ3 As Decimal = 0, TotalCPF1 As Decimal = 0, TotalCPF2 As Decimal = 0, _
                TotalPBF3 As Decimal = 0, TotalPBF2 As Decimal = 0
                Dim row As DataRow = Nothing
                For i1 As Integer = 0 To rows.Length - 1
                    row = rows(i1)
                    TotalPO += Convert.ToDecimal(row("PO_DIST"))
                    TotalActual += Convert.ToDecimal(row("ACTUAL_DIST"))
                    TotalPOValue += Convert.ToDecimal(row("PO_VALUE_DIST"))
                    TotalCPQ1 += Convert.ToDecimal(row("CPQ1_DIST"))
                    TotalCPQ2 += Convert.ToDecimal(row("CPQ2_DIST"))
                    TotalCPQ3 += Convert.ToDecimal(row("CPQ3_DIST"))
                    TotalCPF1 += Convert.ToDecimal(row("CPF1_DIST"))
                    TotalCPF2 += Convert.ToDecimal(row("CPF2_DIST"))
                    TotalPBF3 += Convert.ToDecimal(row("PBF3_DIST"))
                    TotalPBF2 += Convert.ToDecimal(row("PBF2_DIST"))
                Next
                For i1 As Integer = 0 To rows.Length - 1
                    row = rows(i1)
                    row.BeginEdit()
                    'TOTAL_PO, TOTAL_PO_VALUE, BALANCE, TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_ACTUAL,
                    row("TOTAL_ACTUAL") = TotalActual
                    row("TOTAL_PO") = TotalPO
                    row("TOTAL_PO_VALUE") = TotalPOValue
                    row("TOTAL_CPQ1") = TotalCPQ1
                    row("TOTAL_CPQ2") = TotalCPQ2
                    row("TOTAL_CPQ3") = TotalCPQ3
                    row("TOTAL_CPF1") = TotalCPF1
                    row("TOTAL_CPF2") = TotalCPF2
                    row("TOTAL_PBF3") = TotalPBF3
                    row("TOTAL_PBF2") = TotalPBF2
                    row.EndEdit()
                Next
            Next
        End Sub
        Private Sub getTotalForCertainBrands(ByVal RowsSelect() As DataRow, ByRef TotalTarget As Decimal, ByRef TotalPO As Decimal)
            Dim listAgreeBrandIDs As New List(Of String)
            For i As Integer = 0 To RowsSelect.Length - 1
                Dim AgreeBrandID As String = RowsSelect(i)("AGREE_BRAND_ID").ToString()
                If Not listAgreeBrandIDs.Contains(AgreeBrandID) Then
                    listAgreeBrandIDs.Add(AgreeBrandID)
                    TotalTarget += Convert.ToDecimal(RowsSelect(i)("TOTAL_TARGET"))
                    TotalPO += Convert.ToDecimal(RowsSelect(i)("TOTAL_PO"))
                End If
            Next
        End Sub
        Private Function hasTotalPrevPeriod(ByVal rowsSelect() As DataRow, ByVal CurFlag As String) As Boolean
            Select Case curFlag
                Case "F1"
                    For i As Integer = 0 To rowsSelect.Length - 1
                        Dim Qty As Object = rowsSelect(i)("PBF3_DIST")
                        If Not IsNothing(Qty) Then
                            If CDec(Qty) > 0 Then
                                Return True
                            End If
                        End If
                        'Qty = rowsSelect(i)("CPQ4_DIST")
                    Next
                Case "F2"
                    For i As Integer = 0 To rowsSelect.Length - 1
                        Dim Qty As Object = rowsSelect(i)("CPF1_DIST")
                        If Not IsNothing(Qty) Then
                            If CDec(Qty) > 0 Then
                                Return True
                            End If
                        End If
                        'Qty = rowsSelect(i)("CPQ4_DIST")
                    Next
                Case "F3"
                    For i As Integer = 0 To rowsSelect.Length - 1
                        Dim Qty As Object = rowsSelect(i)("CPF2_DIST")
                        If Not IsNothing(Qty) Then
                            If CDec(Qty) > 0 Then
                                Return True
                            End If
                        End If
                        'Qty = rowsSelect(i)("CPQ4_DIST")
                    Next
            End Select
            Return False
        End Function
        Private Sub CalculateHeaderRoundup(ByVal AgreementNO As String, ByVal FLAG As String, ByRef tblAchHeader As DataTable)
            Dim RowsSelect() As DataRow = Nothing, _
            TTargetSPSG_RPM As Decimal = 0, TTargetBPSG_RPM As Decimal = 0, Percentage_SPSG_RPM As Decimal = 0, Percentage_BPSG_RPM As Decimal = 0, _
            TTargetSPSG_BIO As Decimal = 0, TTargetBPSG_BIO As Decimal = 0, Percentage_SPSG_BIO As Decimal = 0, Percentage_BPSG_BIO As Decimal = 0, _
            TTargetSPSG_TR As Decimal = 0, TTargetBPSG_TR As Decimal = 0, Percentage_SPSG_TR As Decimal = 0, Percentage_BPSG_TR As Decimal = 0, _
            TPO_SPSG_RPM As Decimal = 0, TPO_BPSG_RPM As Decimal = 0, _
            TPO_SPSG_BIO As Decimal = 0, TPO_BPSG_BIO As Decimal = 0, _
            TPO_SPSG_TR As Decimal = 0, TPO_BPSG_TR As Decimal = 0, _
            DiscDist As Decimal = 0, BonusQty As Decimal = 0, Balance As Decimal = 0, Dispro As Decimal = 0, _
            Row As DataRow = Nothing, Description As String = "", Rows As DataRow() = Nothing, retval As Object = Nothing

            'AMBIL DATA Progressive Discount
            'power max kemasan kecil 00681,00684
            'power max kemasan besar 006820

            'Biosorb kemasan kecil 00601,0060200,00604
            'Biosorb kemasan besar 006020

            'Biosorb kemasan kecil 007801,007804,0078200
            'Biosorb kemasan besar 007820
            'check achievement method dulu
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

            'hitung achievement
            '--------------------------------------------------------------------------------------------

            'totalkan target dan actual berdasarkan psg dan merk

            '-========================ROUNDUP BIOSORB 007801,007804,0078200,006020======================
            RowsSelect = tblAchHeader.Select("BRAND_ID IN('00601','0060200','00604')")
            If Not IsNothing(RowsSelect) And Not IsDBNull(RowsSelect) Then
                'TTargetSPSG_BIO = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('00601','0060200','00604')")
                'TPO_SPSG_BIO = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('00601','0060200','00604')")
                getTotalForCertainBrands(RowsSelect, TTargetSPSG_BIO, TPO_SPSG_BIO)
                Percentage_SPSG_BIO = common.CommonClass.GetPercentage(100, TPO_SPSG_BIO, TTargetSPSG_BIO)
            End If

            RowsSelect = tblAchHeader.Select("BRAND_ID IN('006020')")
            If RowsSelect.Length > 0 Then
                'TTargetBPSG_BIO = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('006020')")
                'TPO_BPSG_BIO = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('006020')")
                getTotalForCertainBrands(RowsSelect, TTargetBPSG_BIO, TPO_BPSG_BIO)
                Percentage_BPSG_BIO = common.CommonClass.GetPercentage(100, TPO_BPSG_BIO, TTargetBPSG_BIO)
            End If
            Description = ""

            'hitung packsize group kecil
            Dispro = getDispro(Percentage_SPSG_BIO, "ROUNDUP BIOSORB", "S", FLAG)
            RowsSelect = tblAchHeader.Select("BRAND_ID IN('00601','0060200','00604')")
            If Percentage_SPSG_BIO > 0 Or TTargetBPSG_BIO > 0 Or hasTotalPrevPeriod(RowsSelect, FLAG) Then
                'hitung Dispro
                ''only update that has changed
                'RowsSelect = tblAchHeader.Select("BRAND_ID IN('00601','0060200','00604')")
                For i As Integer = 0 To RowsSelect.Length - 1
                    Row = RowsSelect(i)
                    Description = ""
                    Dim TotalPO As Decimal = Convert.ToDecimal(Row("TOTAL_PO"))
                    Dim TotalTarget As Decimal = Convert.ToDecimal(Row("TOTAL_TARGET"))
                    Dim PerAch As Decimal = common.CommonClass.GetPercentage(100, TotalPO, TotalTarget)
                    Dim ActualDist As Decimal = Convert.ToDecimal(Row("ACTUAL_DIST"))
                    Balance = TotalPO - TotalTarget
                    If Balance >= 0 Then
                        Balance = 0
                    End If
                    Dim TotalActual = Convert.ToDecimal(Row("TOTAL_ACTUAL"))
                    BonusQty = TotalActual * (Dispro / 100)
                    DiscDist = ActualDist * (Dispro / 100)
                    'hitung bonus previous year(PBFR) dan CPQ1,CPQ2,CPQ3,dan CPF1,CPF2
                    CalculateDiscPrevious(FLAG, Row, Description, BonusQty, DiscDist)
                    Row.BeginEdit()
                    Row("DISC_DIST") = DiscDist
                    Row("DISPRO") = Dispro
                    Row("ACH_BY_CAT") = Percentage_SPSG_BIO
                    Row("ACH_DISPRO") = PerAch
                    Row("DISC_QTY") = BonusQty
                    Row("BALANCE") = Balance
                    Row("DESCRIPTIONS") = Description
                    Row.EndEdit()
                Next
            End If

            RowsSelect = tblAchHeader.Select("BRAND_ID IN('006020')")
            If Percentage_BPSG_BIO > 0 Or TTargetBPSG_BIO > 0 Or hasTotalPrevPeriod(RowsSelect, FLAG) Then
                'hitung Dispro
                Dispro = 0
                DiscDist = 0
                Description = ""
                BonusQty = 0
                Balance = 0
                Dispro = getDispro(Percentage_BPSG_BIO, "ROUNDUP BIOSORB", "B", FLAG)
                For i As Integer = 0 To RowsSelect.Length - 1
                    Row = RowsSelect(i)
                    Description = ""
                    Dim ActualDist As Decimal = Convert.ToDecimal(Row("ACTUAL_DIST"))
                    Dim TotalPO As Decimal = Convert.ToDecimal(Row("TOTAL_PO"))
                    Dim TotalTarget As Decimal = Convert.ToDecimal(Row("TOTAL_TARGET"))
                    Dim PerAch As Decimal = common.CommonClass.GetPercentage(100, TotalPO, TotalTarget)
                    Balance = TotalPO - TotalTarget
                    If Balance >= 0 Then
                        Balance = 0
                    End If
                    Dim TotalActual = Convert.ToDecimal(Row("TOTAL_ACTUAL"))
                    BonusQty = TotalActual * (Dispro / 100)
                    DiscDist = ActualDist * (Dispro / 100)
                    'hitung bonus previous year(PBFR) dan CPQ1,CPQ2,CPQ3,dan CPF1,CPF2
                    CalculateDiscPrevious(FLAG, Row, Description, BonusQty, DiscDist)
                    Row.BeginEdit()
                    Row("DISC_DIST") = DiscDist
                    Row("DISPRO") = Dispro
                    Row("ACH_DISPRO") = PerAch
                    Row("ACH_BY_CAT") = Percentage_SPSG_BIO
                    Row("DISC_QTY") = BonusQty
                    Row("BALANCE") = Balance
                    Row("DESCRIPTIONS") = Description
                    Row.EndEdit()
                Next
            End If
            '-========================END ROUNDUP BIOSORB======================================================

            '-========================ROUNDUP TRANSORB===========================================================
            RowsSelect = tblAchHeader.Select("BRAND_ID IN('007801','007804','0078200')")
            If RowsSelect.Length > 0 Then
                'TTargetSPSG_TR = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('007801','007804','0078200')")
                'TPO_SPSG_TR = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('007801','007804','0078200')")
                getTotalForCertainBrands(RowsSelect, TTargetSPSG_TR, TPO_SPSG_TR)
                Percentage_SPSG_TR = common.CommonClass.GetPercentage(100, TPO_SPSG_TR, TTargetSPSG_TR)
            End If

            RowsSelect = tblAchHeader.Select("BRAND_ID IN('007820')")
            If RowsSelect.Length > 0 Then
                'TTargetBPSG_TR = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('007820')")
                'TPO_BPSG_TR = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('007820')")
                getTotalForCertainBrands(RowsSelect, TTargetBPSG_TR, TPO_BPSG_TR)
                Percentage_BPSG_TR = common.CommonClass.GetPercentage(100, TPO_BPSG_TR, TTargetBPSG_TR)
            End If

            'hitung packsize group kecil
            RowsSelect = tblAchHeader.Select("BRAND_ID IN('007801','007804','0078200')")
            If Percentage_SPSG_TR > 0 Or TTargetSPSG_TR > 0 Or hasTotalPrevPeriod(RowsSelect, FLAG) Then
                Dispro = 0
                DiscDist = 0
                Description = ""
                BonusQty = 0
                Balance = 0
                'hitung Dispro
                Dispro = getDispro(Percentage_SPSG_TR, "ROUNDUP TRANSORB", "S", FLAG)
                ''only update that has changed
                'RowsSelect = tblAchHeader.Select("BRAND_ID IN('007801','007804','0078200')")
                For i As Integer = 0 To RowsSelect.Length - 1
                    Row = RowsSelect(i)
                    Description = ""
                    Dim TotalPO As Decimal = Convert.ToDecimal(Row("TOTAL_PO"))
                    Dim TotalTarget As Decimal = Convert.ToDecimal(Row("TOTAL_TARGET"))
                    Dim ActualDist As Decimal = Convert.ToDecimal(Row("ACTUAL_DIST"))
                    Dim PerAch As Decimal = common.CommonClass.GetPercentage(100, TotalPO, TotalTarget)
                    Balance = TotalPO - TotalTarget
                    If Balance >= 0 Then
                        Balance = 0
                    End If
                    Dim TotalActual = Convert.ToDecimal(Row("TOTAL_ACTUAL"))
                    BonusQty = TotalActual * (Dispro / 100)
                    DiscDist = ActualDist * (Dispro / 100)
                    'hitung bonus previous year(PBFR) dan CPQ1,CPQ2,CPQ3,dan CPF1,CPF2
                    CalculateDiscPrevious(FLAG, Row, Description, BonusQty, DiscDist)
                    Row.BeginEdit()
                    Row("DISC_DIST") = DiscDist
                    Row("DISPRO") = Dispro
                    Row("ACH_DISPRO") = PerAch
                    Row("ACH_BY_CAT") = Percentage_SPSG_TR
                    Row("DISC_QTY") = BonusQty
                    Row("BALANCE") = Balance
                    Row("DESCRIPTIONS") = Description
                    Row.EndEdit()
                Next
            End If

            RowsSelect = tblAchHeader.Select("BRAND_ID IN('007820')")
            If Percentage_BPSG_TR > 0 Or TTargetBPSG_TR > 0 Or hasTotalPrevPeriod(RowsSelect, FLAG) Then
                Dispro = 0
                DiscDist = 0
                Description = ""
                BonusQty = 0
                Balance = 0
                'hitung Dispro
                Dispro = getDispro(Percentage_BPSG_TR, "ROUNDUP TRANSORB", "B", FLAG)
                For i As Integer = 0 To RowsSelect.Length - 1
                    Row = RowsSelect(i)
                    Description = ""
                    Dim TotalPO As Decimal = Convert.ToDecimal(Row("TOTAL_PO"))
                    Dim TotalTarget As Decimal = Convert.ToDecimal(Row("TOTAL_TARGET"))
                    Dim ActualDist As Decimal = Convert.ToDecimal(Row("ACTUAL_DIST"))
                    Dim PerAch As Decimal = common.CommonClass.GetPercentage(100, TotalPO, TotalTarget)
                    Balance = TotalPO - TotalTarget
                    If Balance >= 0 Then
                        Balance = 0
                    End If
                    Dim TotalActual = Convert.ToDecimal(Row("TOTAL_ACTUAL"))
                    BonusQty = TotalActual * (Dispro / 100)
                    DiscDist = ActualDist * (Dispro / 100)
                    'hitung bonus previous year(PBFR) dan CPQ1,CPQ2,CPQ3,dan CPF1,CPF2
                    CalculateDiscPrevious(FLAG, Row, Description, BonusQty, DiscDist)
                    Row.BeginEdit()
                    Row("DISC_DIST") = DiscDist
                    Row("DISPRO") = Dispro
                    Row("ACH_DISPRO") = PerAch
                    Row("ACH_BY_CAT") = Percentage_BPSG_TR
                    Row("DISC_QTY") = BonusQty
                    Row("BALANCE") = Balance
                    Row("DESCRIPTIONS") = Description
                    Row.EndEdit()
                Next
            End If

            '-====================END ROUNDUP TRANSORB===========================================================

            '-=====================ROUNDUP POWER MAX=============================================================
            RowsSelect = tblAchHeader.Select("BRAND_ID IN('00681','00684')")
            If RowsSelect.Length > 0 Then
                'TTargetSPSG_RPM = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('00681','00684')")
                'TPO_SPSG_RPM = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('00681','00684')")
                getTotalForCertainBrands(RowsSelect, TTargetSPSG_RPM, TPO_SPSG_RPM)
                Percentage_SPSG_RPM = common.CommonClass.GetPercentage(100, TPO_SPSG_RPM, TTargetSPSG_RPM)
            End If

            RowsSelect = tblAchHeader.Select("BRAND_ID IN('006820')")
            If RowsSelect.Length > 0 Then
                'TTargetBPSG_RPM = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('006820')")
                'TPO_BPSG_RPM = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('006820')")
                getTotalForCertainBrands(RowsSelect, TTargetBPSG_RPM, TPO_BPSG_RPM)
                Percentage_BPSG_RPM = common.CommonClass.GetPercentage(100, TPO_BPSG_RPM, TTargetBPSG_RPM)
            End If

            'hitung packsize group kecil
            RowsSelect = tblAchHeader.Select("BRAND_ID IN('00681','00684')")
            If Percentage_SPSG_RPM > 0 Or TTargetSPSG_RPM > 0 Or hasTotalPrevPeriod(RowsSelect, FLAG) Then
                Dispro = 0
                DiscDist = 0
                Description = ""
                BonusQty = 0
                Balance = 0
                'hitung Dispro
                Dispro = getDispro(Percentage_SPSG_RPM, "ROUNDUP POWERMAX", "S", FLAG)
                ''only update that has changed

                For i As Integer = 0 To RowsSelect.Length - 1
                    Row = RowsSelect(i)
                    Description = ""
                    Dim TotalPO As Decimal = Convert.ToDecimal(Row("TOTAL_PO"))
                    Dim TotalTarget As Decimal = Convert.ToDecimal(Row("TOTAL_TARGET"))
                    Dim ActualDist As Decimal = Convert.ToDecimal(Row("ACTUAL_DIST"))
                    Dim PerAch As Decimal = common.CommonClass.GetPercentage(100, TotalPO, TotalTarget)
                    Balance = TotalPO - TotalTarget
                    If Balance >= 0 Then
                        Balance = 0
                    End If
                    Dim TotalActual = Convert.ToDecimal(Row("TOTAL_ACTUAL"))
                    BonusQty = TotalActual * (Dispro / 100)
                    DiscDist = ActualDist * (Dispro / 100)
                    'hitung bonus previous year(PBFR) dan CPQ1,CPQ2,CPQ3,dan CPF1,CPF2
                    CalculateDiscPrevious(FLAG, Row, Description, BonusQty, DiscDist)
                    Row.BeginEdit()
                    Row("DISC_DIST") = DiscDist
                    Row("DISPRO") = Dispro
                    Row("ACH_DISPRO") = PerAch
                    Row("ACH_BY_CAT") = Percentage_SPSG_RPM
                    Row("DISC_QTY") = BonusQty
                    Row("BALANCE") = Balance
                    Row("DESCRIPTIONS") = Description
                    Row.EndEdit()
                Next
            End If

            RowsSelect = tblAchHeader.Select("BRAND_ID IN('006820')")
            If Percentage_BPSG_RPM > 0 Or TTargetBPSG_RPM > 0 Or hasTotalPrevPeriod(RowsSelect, FLAG) Then
                Dispro = 0
                DiscDist = 0
                Description = ""
                BonusQty = 0
                Balance = 0
                'hitung Dispro
                Dispro = getDispro(Percentage_BPSG_RPM, "ROUNDUP POWERMAX", "B", FLAG)

                For i As Integer = 0 To RowsSelect.Length - 1
                    Row = RowsSelect(i)
                    Description = ""
                    Dim TotalPO As Decimal = Convert.ToDecimal(Row("TOTAL_PO"))
                    Dim TotalTarget As Decimal = Convert.ToDecimal(Row("TOTAL_TARGET"))
                    Dim ActualDist As Decimal = Convert.ToDecimal(Row("ACTUAL_DIST"))
                    Dim PerAch As Decimal = common.CommonClass.GetPercentage(100, TotalPO, TotalTarget)
                    Balance = TotalPO - TotalTarget
                    If Balance >= 0 Then
                        Balance = 0
                    End If
                    Dim TotalActual = Convert.ToDecimal(Row("TOTAL_ACTUAL"))
                    BonusQty = TotalActual * Dispro / 100
                    DiscDist = ActualDist * (Dispro / 100)
                    'hitung bonus previous year(PBFR) dan CPQ1,CPQ2,CPQ3,dan CPF1,CPF2
                    CalculateDiscPrevious(FLAG, Row, Description, BonusQty, DiscDist)
                    Row.BeginEdit()
                    Row("DISC_DIST") = DiscDist
                    Row("DISPRO") = Dispro
                    Row("ACH_DISPRO") = PerAch
                    Row("ACH_BY_CAT") = Percentage_BPSG_RPM
                    Row("DISC_QTY") = BonusQty
                    Row("BALANCE") = Balance
                    Row("DESCRIPTIONS") = Description
                    Row.EndEdit()
                Next
            End If
            '-=====================END ROUNDUP POWER MAX=============================================================
            tblAchHeader.AcceptChanges()
        End Sub
        ''' <summary>
        ''' </summary>
        ''' <param name="Flag"></param>
        ''' <param name="Row">DataRow tblAchHeader</param>
        ''' <param name="Description">Description of previous discount to get</param>
        ''' <param name="BonusQty">Disc Qty + all previous discount(TOTAL(CPQ2,CPQ3,CPF2,CPF1,PBF3))</param>
        ''' <remarks>Function to get and calculate Previos Actual PO, discount qty and descriptions to get</remarks>
        Private Sub CalculateDiscPrevious(ByVal Flag As String, ByVal Row As DataRow, ByRef Description As String, ByRef BonusQty As Decimal, ByRef DiscDist As Decimal)

            Dim totalInvoiceBeforeF3 As Decimal = 0, totalInvoiceCurrentF1 As Decimal = 0, totalInvoiceCurrentF2 As Decimal = 0, totalInvoiceCurrentQ1 As Decimal = 0, totalInvoiceCurrentQ2 As Decimal = 0, _
            totalInvoiceCurrentQ3 As Decimal = 0, PrevDisPro As Decimal = 0, DVCurAch As DataView = tblCurAchiement.DefaultView, _
            DVPrevAch As DataView = Nothing
            'Dim DVGivenProg As DataView = Me.tblGP.DefaultView
            'DVGivenProg.Sort = "IDApp"
            Dim PBF3Dist As Decimal = 0, CPF1Dist As Decimal = 0, CPF2Dist As Decimal = 0, CPQ1Dist As Decimal = 0, CPQ2Dist As Decimal = 0, _
            CPQ3Dist As Decimal = 0
            Dim GPID As Object = Row("GP_ID")
            If Not IsNothing(Me.tblPrevAchievement) Then
                DVPrevAch = tblPrevAchievement.DefaultView()
            End If
            Dim rowsCheck() As DataRow = Nothing
            Dim Index As Integer = -1
            'AGREE_BRAND_ID,UP_TO_PCT,PRGSV_DISC_PCT
            Dim AchID As String = Row("ACH_HEADER_ID").ToString()
            Select Case Flag
                Case "F3" 'Q1,Q2,Q3,F1,F2
                    If IsTransitionTime Then
                        If AchID.Contains("F3") Then
                            AchID = AchID.Replace("F3", "")
                        End If
                        Dim AchQ1 As String = AchID + "Q1" 'AchID.Remove(AchID.LastIndexOf("|") + 1)
                        'AchQ1 = AchQ1 + "Q1"
                        'Dim DiscQ1 As Decimal = 0, 
                        Dim DiscQ2 As Decimal = 0, DiscQ3 As Decimal = 0
                        totalInvoiceCurrentQ1 = Row("TOTAL_CPQ1")
                        CPQ1Dist = Row("CPQ1_DIST")
                        Dim AchQ2 As String = AchID + "Q2" 'AchID.Remove(AchID.LastIndexOf("|") + 1)
                        'AchQ2 = AchQ2 + "Q2"
                        totalInvoiceCurrentQ2 = Row("TOTAL_CPQ2")
                        CPQ2Dist = Row("CPQ2_DIST")
                        If CDec(totalInvoiceCurrentQ2) > 0 Then
                            'Index = DVGivenProg.Find(GPID)
                            If CDec(Row("GPCPQ2")) > 0 Then
                                PrevDisPro = Convert.ToDecimal(Row("GPCPQ2"))
                            ElseIf Not IsNothing(DVCurAch) Then
                                DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchQ2 & "'"
                                If DVCurAch.Count > 0 Then
                                    PrevDisPro = DVCurAch(0)("DISPRO")
                                End If
                            End If
                            If PrevDisPro > 0 Then
                                DiscQ2 = (PrevDisPro / 100) * totalInvoiceCurrentQ2
                                DiscDist = (PrevDisPro / 100) * CPQ2Dist
                                BonusQty += DiscQ2
                                Description &= String.Format("Q2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentQ2, DiscQ2)
                            End If
                        End If

                        Dim AchQ3 = AchID + "Q3" '.Remove(AchID.LastIndexOf("|") + 1)
                        'AchQ3 = AchQ3 + "Q3"
                        totalInvoiceCurrentQ3 = Row("TOTAL_CPQ3")
                        CPQ3Dist = Row("CPQ3_DIST")
                        If CDec(totalInvoiceCurrentQ3) > 0 Then
                            If CDec(Row("GPCPQ3")) > 0 Then
                                PrevDisPro = Convert.ToDecimal(Row("GPCPQ3"))
                            ElseIf Not IsNothing(DVCurAch) Then
                                DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchQ3 & "'"
                                If DVCurAch.Count > 0 Then
                                    PrevDisPro = DVCurAch(0)("DISPRO")
                                End If
                            End If
                            If PrevDisPro > 0 Then
                                DiscQ3 = (PrevDisPro / 100) * totalInvoiceCurrentQ3
                                DiscDist += (PrevDisPro / 100) * CPQ3Dist
                                BonusQty += DiscQ3
                                If Description <> "" Then
                                    Description &= ", "
                                End If
                                Description &= String.Format("Q3 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentQ3, DiscQ3)
                            End If
                        End If
                    End If
                    Dim DiscF1 As Decimal = 0, DiscF2 As Decimal = 0

                    Dim AchF2 As String = AchID.Remove(AchID.LastIndexOf("|") + 1) : AchF2 += "F2" 'AchID.Remove(AchID.LastIndexOf("|") + 1)

                    'AchF2 = AchF2 + "F2"
                    totalInvoiceCurrentF2 = Row("TOTAL_CPF2")
                    CPF2Dist = Row("CPF2_DIST")
                    If totalInvoiceCurrentF2 > 0 Then
                        If CDec(Row("GPCPF2")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPCPF2"))
                        ElseIf Not IsNothing(DVCurAch) Then
                            DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchF2 & "'"
                            If DVCurAch.Count > 0 Then
                                PrevDisPro = DVCurAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF2 = (PrevDisPro / 100) * totalInvoiceCurrentF2
                            DiscDist += (PrevDisPro / 100) * CPF2Dist
                            BonusQty += DiscF2
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("F2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF2, DiscF2)
                        End If
                    End If
                    Dim AchF1 As String = AchID.Remove(AchID.LastIndexOf("|") + 1) : AchF1 += "F1" ': AchID.Remove(AchID.LastIndexOf("|") + 1)
                    'AchF1 = AchF1 + "F1"
                    totalInvoiceCurrentF1 = Row("TOTAL_CPF1")
                    CPF1Dist = Row("CPF1_DIST")
                    If totalInvoiceCurrentF1 > 0 Then
                        If CDec(Row("GPCPF1")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPCPF1"))
                        ElseIf Not IsNothing(DVCurAch) Then
                            DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchF1 & "'"
                            If DVCurAch.Count > 0 Then
                                PrevDisPro = DVCurAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF1 = (PrevDisPro / 100) * totalInvoiceCurrentF1
                            DiscDist += (PrevDisPro / 100) * CPF1Dist
                            BonusQty += DiscF1
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("F1 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF1, DiscF1)
                        End If
                    End If
                Case "F2" 'F1, PBF3
                    Dim AchF1 As String = AchID.Remove(AchID.LastIndexOf("|") + 1)
                    AchF1 = AchF1 + "F1"
                    totalInvoiceCurrentF1 = Row("TOTAL_CPF1")
                    CPF1Dist = Row("CPF1_DIST")
                    Dim DiscF1 As Decimal = 0
                    If totalInvoiceCurrentF1 > 0 Then
                        If CDec(Row("GPCPF1")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPCPF1"))
                        ElseIf Not IsNothing(DVCurAch) Then
                            DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchF1 & "'"
                            If DVCurAch.Count > 0 Then
                                PrevDisPro = DVCurAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF1 = (PrevDisPro / 100) * totalInvoiceCurrentF1
                            DiscDist += (PrevDisPro / 100) * CPF1Dist
                            BonusQty += DiscF1
                            Description &= String.Format("F1 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF1, DiscF1)
                        End If
                    End If
                    Dim AchPBF3 As String = AchID.Remove(AchID.LastIndexOf("|") + 1)
                    Dim DiscF3 As Decimal = 0
                    totalInvoiceBeforeF3 = Row("TOTAL_PBF3")
                    PBF3Dist = Row("PBF3_DIST")
                    AchPBF3 = AchPBF3 + "F3"
                    If totalInvoiceBeforeF3 > 0 Then
                        If CDec(Row("GPPBF3")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPPBF3"))
                        ElseIf Not IsNothing(DVPrevAch) Then
                            DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchPBF3 & "'"
                            If DVPrevAch.Count > 0 Then
                                PrevDisPro = DVPrevAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF3 = (PrevDisPro / 100) * totalInvoiceBeforeF3
                            DiscDist += (PrevDisPro / 100) * PBF3Dist
                            BonusQty += DiscF3
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("F3 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF3, DiscF3)
                        End If
                    End If
                Case "F1"
                    Dim AchPBF2 As String = AchID.Remove(AchID.LastIndexOf("|") + 1)
                    Dim DiscF2 As Decimal = 0
                    Dim totalInvoiceBeforeF2 As Decimal = Row("TOTAL_PBF2"), PBF2Dist As Decimal = Row("PBF2_DIST")
                    AchPBF2 = AchPBF2 + "F2"
                    If totalInvoiceBeforeF2 > 0 Then
                        If CDec(Row("GPPBF2")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPPBF2"))
                        ElseIf Not IsNothing(DVPrevAch) Then
                            DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchPBF2 & "'"
                            If DVPrevAch.Count > 0 Then
                                PrevDisPro = DVPrevAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF2 = (PrevDisPro / 100) * totalInvoiceBeforeF2
                            DiscDist += (PrevDisPro / 100) * PBF2Dist
                            BonusQty += DiscF2
                            Description &= String.Format("F2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF2, DiscF2)
                        End If
                    End If

                    Dim AchPBF3 As String = AchID.Remove(AchID.LastIndexOf("|") + 1)
                    Dim DiscF3 As Decimal = 0
                    totalInvoiceBeforeF3 = Row("TOTAL_PBF3")
                    PBF3Dist = Row("PBF3_DIST")
                    AchPBF3 = AchPBF3 + "F3"
                    If totalInvoiceBeforeF3 > 0 Then
                        If CDec(Row("GPPBF3")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPPBF3"))
                        ElseIf Not IsNothing(DVPrevAch) Then
                            DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchPBF3 & "'"
                            If DVPrevAch.Count > 0 Then
                                PrevDisPro = DVPrevAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF3 = (PrevDisPro / 100) * totalInvoiceBeforeF3
                            DiscDist += (PrevDisPro / 100) * PBF3Dist
                            BonusQty += DiscF3
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("F3 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF3, DiscF3)
                        End If
                    End If
            End Select
        End Sub
        Private Function FillAchHeaderAndDetail(ByVal AgreementNo As String, ByVal Flag As String, ByRef tblAchHeader As DataTable, _
            ByRef tblAchDetail As DataTable, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As Boolean
            Query = "SET NOCOUNT ON ;" & vbCrLf & _
            " SELECT TOP 1 START_DATE, END_DATE FROM AGREE_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            Dim StartDatePKD As New DateTime(2019, 8, 1), EndDatePKD As New DateTime(2020, 7, 31)
            While Me.SqlRe.Read()
                StartDatePKD = Me.SqlRe.GetDateTime(0) : EndDatePKD = Me.SqlRe.GetDateTime(1)
            End While : Me.SqlRe.Close()
            'nanti harus di reset
            IsTransitionTime = StartDatePKD >= New DateTime(2019, 8, 1) And EndDatePKD <= New DateTime(2020, 7, 31)
            '-----------------------------DECLARASI TABLE------------------------------------------------------------
            Dim Row As DataRow = Nothing, RowsSelect() As DataRow = Nothing, _
           ListAchievementID As New List(Of String), strFlag As String = ""
            Select Case Flag
                Case "F1" : strFlag = "FMP1"
                Case "F2" : strFlag = "FMP2"
                Case "F3" : strFlag = "FMP3"
            End Select


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
                     "  SELECT PO.PO_REF_NO,PO.PO_REF_DATE,PO.DISTRIBUTOR_ID,ABI.BRAND_ID,ABP.BRANDPACK_ID,OPB.PO_ORIGINAL_QTY,OPB.PO_PRICE_PERQTY,OOAB.QTY_EVEN + ISNULL(SB.TOTAL_DISC_QTY,0) AS SPPB_QTY,OOA.RUN_NUMBER ," & vbCrLf & _
                     "  IncludeDPD = CASE WHEN (OPB.ExcludeDPD = 0) THEN 'YESS' " & vbCrLf & _
                     "  WHEN (OPB.ExcludeDPD = 1) THEN 'NO'" & vbCrLf & _
                     "  WHEN ((OPB.PRICE_CATEGORY = 'SP') AND EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PRICE_TAG = OPB.PRICE_TAG AND PRICE = OPB.PO_PRICE_PERQTY AND START_DATE >= DATEADD(MONTH,-6,@START_DATE) AND END_DATE <= @END_DATE AND IncludeDPD = 1)) THEN 'YESS' " & vbCrLf & _
                     "  WHEN ((OPB.PRICE_CATEGORY = 'SP') AND EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PRICE_TAG = OPB.PRICE_TAG AND PRICE = OPB.PO_PRICE_PERQTY AND START_DATE >= DATEADD(MONTH,-6,@START_DATE) AND END_DATE <= @END_DATE AND IncludeDPD = 0)) THEN 'NO' " & vbCrLf & _
                     "  WHEN ((OPB.PRICE_CATEGORY = 'GP') AND EXISTS(SELECT PRICE_TAG FROM GEN_PLANT_PRICE WHERE PRICE_TAG = OPB.PRICE_TAG AND IncludeDPD = 1)) THEN 'YESS' " & vbCrLf & _
                     "  WHEN ((OPB.PRICE_CATEGORY = 'GP') AND EXISTS(SELECT PRICE_TAG FROM GEN_PLANT_PRICE WHERE PRICE_TAG = OPB.PRICE_TAG AND IncludeDPD = 0)) THEN 'NO' " & vbCrLf & _
                     "  WHEN (OPB.PRICE_CATEGORY = 'FM') THEN 'YESS' " & vbCrLf & _
                     "  ELSE 'NO' END " & vbCrLf & _
                     "  FROM Nufarm.dbo.AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                     "  INNER JOIN Nufarm.DBO.AGREE_BRANDPACK_INCLUDE ABP ON ABI.AGREE_BRAND_ID = ABP.AGREE_BRAND_ID" & vbCrLf & _
                     "  INNER JOIN Nufarm.dbo.ORDR_PO_BRANDPACK OPB ON OPB.BRANDPACK_ID = ABP.BRANDPACK_ID " & vbCrLf & _
                     "  INNER JOIN Nufarm.dbo.ORDR_PURCHASE_ORDER PO " & vbCrLf & _
                     "  ON PO.PO_REF_NO = OPB.PO_REF_NO INNER JOIN Nufarm.dbo.ORDR_ORDER_ACCEPTANCE OOA " & vbCrLf & _
                     "  ON OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                     "  INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOA.OA_ID = OOAB.OA_ID AND OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                     "  LEFT OUTER JOIN (SELECT OOBD.OA_BRANDPACK_ID,ISNULL(SUM(OOBD.DISC_QTY),0) AS TOTAL_DISC_QTY FROM ORDR_OA_BRANDPACK_DISC OOBD " & vbCrLf & _
                     "      WHERE OOBD.OA_BRANDPACK_ID = ANY(  " & vbCrLf & _
                     "      SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK OOB INNER JOIN ORDR_PO_BRANDPACK PB ON OOB.PO_BRANDPACK_ID = PB.PO_BRANDPACK_ID " & vbCrLf & _
                     "              INNER JOIN ORDR_PURCHASE_ORDER PO1 ON PO1.PO_REF_NO = PB.PO_REF_NO WHERE PO1.PO_REF_DATE >= @START_DATE AND PO1.PO_REF_DATE <= @END_DATE " & vbCrLf & _
                     "      ) " & vbCrLf & _
                     "      GROUP BY OOBD.OA_BRANDPACK_ID " & vbCrLf & _
                     "  )SB ON SB.OA_BRANDPACK_ID = OOAB.OA_BRANDPACK_ID" & vbCrLf & _
                     "  WHERE RTRIM(LTRIM(ABI.AGREEMENT_NO)) = @AGREEMENT_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND OPB.PO_ORIGINAL_QTY > 0 " & vbCrLf & _
                     "  AND PO.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO) " & vbCrLf & _
                     " )B ;" & vbCrLf & _
                     " --CREATE CLUSTERED INDEX IX_T_MASTER_PO ON ##T_MASTER_PO_" & Me.ComputerName & "(PO_REF_DATE,PO_REF_NO,RUN_NUMBER,DISTRIBUTOR_ID,BRANDPACK_ID) ;"
            '============================= END UNCOMMENT THIS AFTER DEBUGGING =============================================================
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate.AddMonths(-10).AddDays(-1))
            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)

            Me.SqlCom.ExecuteScalar()
            ''sum po inovoice_qty dimana po bukan di antara periode flag(periode sebelumnya)
            Me.SqlCom.Parameters.RemoveAt("@START_DATE")
            Me.SqlCom.Parameters.RemoveAt("@END_DATE")
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)

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
                    "       	 SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
                    "     		 )" & vbCrLf & _
                    "       AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' " & vbCrLf & _
                    "      )INV1   " & vbCrLf & _
                    " GROUP BY DISTRIBUTOR_ID,BRAND_ID ; " & vbCrLf & _
                    " CREATE CLUSTERED INDEX IX_T_Agreement_Brand ON ##T_Agreement_Brand_" & Me.ComputerName & "(TOTAL_INVOICE,BRAND_ID) ;"
            Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()


            ''SEKALIAN AMBIL AVGPRICEID dan PriceFM & PricePL
            'chek data
            'query hanya insert ke database
            'jika total rows  di agreement brand by PSG <> total Rows di ach_header -hapus rows di ach header
            'jika ada perubahan data di ach_header karena ada perubahan actual di invoce --hapus rows di ach header
            Query = " SET NOCOUNT ON;" & vbCrLf & _
            "SELECT COUNT(*) FROM AGREE_BRAND_INCLUDE WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO;"
            Me.ResetCommandText(CommandType.Text, Query)
            Dim totalRowsAgree As Integer = CInt(Me.SqlCom.ExecuteScalar())
            Query = " SET NOCOUNT ON" & vbCrLf & _
            " SELECT COUNT(*) FROM ACHIEVEMENT_HEADER WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO AND FLAG = @FLAG"
            Me.ResetCommandText(CommandType.Text, Query)
            Dim totalRowsAch As Integer = CInt(Me.SqlCom.ExecuteScalar())
            If totalRowsAch <> totalRowsAgree Then
                'hapus achievement
                Me.mustDeletedBeforeInsert = True
                Me.MustReinsertedData = True
            End If
            Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2)
            If Not MustReinsertedData Then 'check apakah ada perubahan actual data antara invoice dan ach header
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT 1 WHERE EXISTS(SELECT * FROM Nufarm.DBO.ACHIEVEMENT_HEADER ACH " & vbCrLf & _
                        " INNER JOIN Tempdb..##T_Agreement_Brand_" & Me.ComputerName & " INV" & vbCrLf & _
                        " ON ACH.BRAND_ID = INV.BRAND_ID AND ACH.DISTRIBUTOR_ID = INV.DISTRIBUTOR_ID " & vbCrLf & _
                        " WHERE RTRIM(LTRIM(ACH.AGREEMENT_NO)) = @AGREEMENT_NO AND ACH.TOTAL_ACTUAL <> CAST((ISNULL(INV.TOTAL_INVOICE,0))AS DECIMAL(18,3)) AND ACH.FLAG = @FLAG) ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                If IsNothing(retval) Or IsDBNull(retval) Then
                    Return False
                End If
                If CInt(retval) > 0 Then
                    MustReinsertedData = True
                Else
                    Return False
                End If
            End If

            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    " SELECT DISTINCT ABI.AGREE_BRAND_ID,(SELECT MAX(IDApp) FROM brnd_AVGPrice WHERE BRAND_ID = ABI.BRAND_ID AND START_DATE >= AA.START_DATE AND END_DATE <= AA.END_DATE) AS AvgPriceID   " & vbCrLf & _
                    " FROM AGREE_AGREEMENT AA INNER JOIN AGREE_BRAND_INCLUDE ABI ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                    " WHERE RTRIM(LTRIM(AA.AGREEMENT_NO)) = @AGREEMENT_NO ;"
            Me.ResetCommandText(CommandType.Text, Query)
            tblAVGPrice = New DataTable("AVGPrice")
            setDataAdapter(Me.SqlCom).Fill(tblAVGPrice)
            Dim DVAPrice As DataView = tblAVGPrice.DefaultView()

            DVAPrice.Sort = "AGREE_BRAND_ID"
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT DA.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID,ABI.BRAND_ID,ABI.TARGET_" & strFlag & ", ABI.TARGET_" & strFlag.Remove(3, 1) & "_FM" & Flag.Remove(0, 1) & ", ABI.TARGET_" & strFlag.Remove(3, 1) & "_PL" & Flag.Remove(0, 1) & vbCrLf & _
                    " FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRAND_INCLUDE ABI ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                    " WHERE RTRIM(LTRIM(DA.AGREEMENT_NO)) = @AGREEMENT_NO ;"
            'ACH_HEADER_ID, DISTRIBUTOR_ID, AGREEMENT_NO, BRAND_ID,AGREE_BRAND_ID,TARGET, TARGET_FM, TARGET_PL, TARGET_VALUE,FLAG,
            'TOTAL_TARGET, TOTAL_PO, TOTAL_PO_VALUE, BALANCE, TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_ACTUAL, ACH_DISPRO, DISPRO,DESCRIPTIONS
            'CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsNew, IsChanged, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3, 
            'ACTUAL_DIST,PO_DIST,PO_VALUE_DIST,DISC_DIST,CPQ1_DIST,CPQ2_DIST,CPQ3_DIST,CPF1_DIST,CPF2_DIST,PBF3_DIST
            Me.ResetCommandText(CommandType.Text, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read()
                Row = tblAchHeader.NewRow()
                Dim BrandID As String = SqlRe.GetString(2)
                Dim Target As Decimal = SqlRe.GetDecimal(3)
                Dim TargetFM As Decimal = SqlRe.GetDecimal(4)
                Dim TargetPL As Decimal = SqlRe.GetDecimal(5)
                Dim AgreeBrandID As String = SqlRe.GetString(1)
                Dim AchievementID As String = SqlRe.GetString(0) + "|" + AgreementNo + BrandID + "|" + Flag
                Row("ACH_HEADER_ID") = AchievementID
                Row("AGREEMENT_NO") = AgreementNo
                Row("DISTRIBUTOR_ID") = SqlRe.GetString(0)
                Row("AGREE_BRAND_ID") = SqlRe.GetString(1)
                Row("BALANCE") = 0 - Target
                Dim index As Integer = DVAPrice.Find(AgreeBrandID)
                If index <> -1 Then
                    Row("AVGPriceID") = DVAPrice(index)("AvgPriceID")
                End If
                Row("BRAND_ID") = BrandID
                Row("FLAG") = Flag
                Row("TOTAL_TARGET") = Target
                Row("TARGET_FM") = TargetFM
                Row("TARGET_PL") = TargetPL
                Row.EndEdit() : tblAchHeader.Rows.Add(Row)
                If Not ListAchievementID.Contains(AchievementID) Then
                    ListAchievementID.Add(AchievementID)
                End If
            End While : Me.SqlRe.Close()
            Me.ClearCommandParameters()

            ''hanya mengambil actual saja, karena actual tidak mengacu pad PKD
            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Total_Qty_Brand_By_Invoice")
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNo, 25)
            Me.AddParameter("@COMPUTERNAME", SqlDbType.VarChar, Me.ComputerName, 100)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            Dim ACH_HEADER_ID As String = ""
            While Me.SqlRe.Read()
                ACH_HEADER_ID = SqlRe.GetString(0) & "|" & AgreementNo & SqlRe.GetString(1) & "|" & Flag
                RowsSelect = tblAchHeader.Select("ACH_HEADER_ID = '" & ACH_HEADER_ID & "'")
                If RowsSelect.Length > 0 Then
                    Row = RowsSelect(0) : Row.BeginEdit()
                    Row("ACTUAL_DIST") = SqlRe.GetDecimal(2)
                    Row.EndEdit()
                End If
            End While : SqlRe.Close()

            Dim tblPOByBrandPack As New DataTable("T_POBrandPack"), TblPOByBrand As New DataTable("T_POBrand")

            ''total PO BY BRAND
            'hanya mengambil total_PO karena TotalPO mengacu ke PKD
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ; SELECT ABI.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID," & vbCrLf & _
                    " ISNULL(SUM(PO.PO_ORIGINAL_QTY),0)AS PO_DIST,ISNULL(SUM(PO.PO_AMOUNT),0) AS PO_VALUE_DIST FROM Nufarm.DBO.VIEW_AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                    " INNER JOIN tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO ON PO.DISTRIBUTOR_ID = ABI.DISTRIBUTOR_ID AND PO.BRANDPACK_ID = ABI.BRANDPACK_ID " & vbCrLf & _
                    " WHERE RTRIM(LTRIM(ABI.AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
                    " AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE " & vbCrLf & _
                    " GROUP BY ABI.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID OPTION(KEEP PLAN);"
            Me.ResetCommandText(CommandType.Text, Query)
            tblPOByBrandPack.Clear()
            setDataAdapter(Me.SqlCom).Fill(TblPOByBrand)
            If TblPOByBrand.Rows.Count > 0 Then
                For i1 As Integer = 0 To TblPOByBrand.Rows.Count - 1
                    ACH_HEADER_ID = TblPOByBrand.Rows(i1)("DISTRIBUTOR_ID").ToString() & "|" & TblPOByBrand.Rows(i1)("AGREE_BRAND_ID").ToString() & "|" & Flag
                    RowsSelect = tblAchHeader.Select("ACH_HEADER_ID = '" & ACH_HEADER_ID & "'")
                    If RowsSelect.Length > 0 Then
                        Row = RowsSelect(0)
                        Row.BeginEdit()
                        Row("PO_DIST") = TblPOByBrand.Rows(i1)("PO_DIST")
                        Row("PO_VALUE_DIST") = TblPOByBrand.Rows(i1)("PO_VALUE_DIST")
                        Row.EndEdit()
                    End If
                Next
            End If
            Me.ClearCommandParameters()

            '--------------------------------------------------------------------------------------------------
            Dim strListAch As String = "IN('"
            For i As Integer = 0 To ListAchievementID.Count - 1
                strListAch &= ListAchievementID(i).ToString() & "'"
                If i < ListAchievementID.Count - 1 Then
                    strListAch &= ",'"
                End If
            Next
            strListAch &= ")"

            ''fill table detail
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    " SELECT ABI.BRANDPACK_ID,ABI.ACH_HEADER_ID,ABI.ACH_DETAIL_ID " & vbCrLf & _
                    " FROM ( " & vbCrLf & _
                    "       SELECT ACH_HEADER_ID = ABI.DISTRIBUTOR_ID + '|' + ABI.AGREE_BRAND_ID + '|" & Flag & "'," & vbCrLf & _
                    "       ABI.BRANDPACK_ID,ABI.DISTRIBUTOR_ID + '|' + ABI.AGREE_BRAND_ID + '|" & Flag & "|' + ABI.BRANDPACK_ID AS ACH_DETAIL_ID " & vbCrLf & _
                    "       FROM VIEW_AGREE_BRANDPACK_INCLUDE ABI WHERE DISTRIBUTOR_ID + '|' + AGREE_BRAND_ID + '|" & Flag & "'" & strListAch & vbCrLf & _
                    "      )ABI "
            Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            'ACH_HEADER_ID,BRANDPACK_ID,ACH_DETAIL_ID,TOTAL_PO,TOTAL_ACTUAL,DISC_QTY,TOTAL_CPQ1,TOTAL_CPQ2,TOTAL_CPQ2,
            'TOTAL_CPQ3,TOTAL_CPF1,TOTAL_CPF2,TOTAL_PBF3,DESCRIPTIONS,CreatedBy,CreatedDate,CreatedDate,ModifiedBy,ModifiedDate,IsNew,IsChanged
            While Me.SqlRe.Read()
                Row = tblAchDetail.NewRow()
                Dim BRANDPACK_ID As String = SqlRe.GetString(0)
                ACH_HEADER_ID = SqlRe.GetString(1)
                Row("ACH_HEADER_ID") = ACH_HEADER_ID
                Row("BRANDPACK_ID") = BRANDPACK_ID
                Row("ACH_DETAIL_ID") = SqlRe.GetString(2)
                Row.EndEdit() : tblAchDetail.Rows.Add(Row)
            End While : SqlRe.Close() : Me.ClearCommandParameters()

            'Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
            '        " SELECT PO.DISTRIBUTOR_ID,tmbp.BRAND_ID_DTS AS BRAND_ID,tmbp.BRANDPACK_ID_DTS AS BRANDPACK_ID,(INV.QTY/ISNULL(PO.SPPB_QTY,0)) * ISNULL(PO.PO_ORIGINAL_QTY,0) AS QTY " & vbCrLf & _
            '        " FROM COMPARE_ITEM tmbp " & vbCrLf & _
            '        " INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON tmbp.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
            '        " INNER JOIN tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO ON PO.BRANDPACK_ID = tmbp.BRANDPACK_ID_DTS " & vbCrLf & _
            '        " AND ((PO.RUN_NUMBER = INV.REFERENCE) OR (PO.PO_REF_NO = INV.PONUMBER) ) " & vbCrLf & _
            '        " WHERE PO.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO ) AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' AND INV.QTY > 0 ; "
            ''Dim DistributorID As String = ListAchievementID(0).Remove(ListAchievementID(0).IndexOf("|"))
            'Me.ResetCommandText(CommandType.Text, Query)
            'Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            'Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
            ''Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
            'Me.SqlCom.ExecuteScalar()

            Dim AchDetailID As String = ""

            'hanya untuk mengambil total actual karena actual tidak mengacu pada PKD
            'Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Total_Qty_BrandPack_By_Invoice")
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                    " SELECT DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID,ISNULL(SUM(QTY),0) AS TOTAL_INVOICE " & vbCrLf & _
                    " FROM(" & vbCrLf & _
                    " SELECT PO.DISTRIBUTOR_ID,PO.BRAND_ID,PO.BRANDPACK_ID,(ISNULL(INVOICE.QTY,0)/ISNULL(PO.SPPB_QTY,0)) * PO.PO_ORIGINAL_QTY AS QTY" & vbCrLf & _
                    " FROM tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO " & vbCrLf & _
                    " INNER JOIN (SELECT INV.PONUMBER,INV.REFERENCE,Tmbp.BRANDPACK_ID_DTS AS BRANDPACK_ID,INV.QTY,INV.INV_AMOUNT " & vbCrLf & _
                    "	          FROM ##T_SELECT_INVOICE_" & Me.ComputerName & " INV INNER JOIN COMPARE_ITEM Tmbp " & vbCrLf & _
                    "	          ON INV.BRANDPACK_ID =  Tmbp.BRANDPACK_ID_ACCPAC AND INV.QTY > 0 " & vbCrLf & _
                    "   	)INVOICE" & vbCrLf & _
                    " ON PO.BRANDPACK_ID = INVOICE.BRANDPACK_ID " & vbCrLf & _
                    " AND ((PO.PO_REF_NO = INVOICE.PONUMBER) Or (PO.RUN_NUMBER = INVOICE.REFERENCE)) " & vbCrLf & _
                    " WHERE PO.DISTRIBUTOR_ID = SOME( SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO )" & vbCrLf & _
                    " AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' " & vbCrLf & _
                    ")INV " & vbCrLf & _
                    " GROUP BY DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
            Me.AddParameter("@COMPUTERNAME", SqlDbType.VarChar, Me.ComputerName, 100)
            Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNo, 25)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read()
                'ACH_HEADER_ID = SqlRe.GetString(0) & "|" & AgreementNo & SqlRe.GetString(1) & "|" & Flag
                ACH_HEADER_ID = SqlRe.GetString(0) & "|" & AgreementNo & SqlRe.GetString(1) & "|" & Flag
                AchDetailID = ACH_HEADER_ID & "|" & SqlRe.GetString(2)
                RowsSelect = tblAchDetail.Select("ACH_DETAIL_ID = '" & AchDetailID & "'")
                If RowsSelect.Length > 0 Then
                    Row = RowsSelect(0)
                    Row.BeginEdit()
                    Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(3)
                    Row.EndEdit()
                End If
            End While : SqlRe.Close()

            ''total PO BY BRANDPACK_ID
            'hanya mengambil total_PO karena TotalPO mengacu ke PKD
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ; SELECT ABI.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID,ABI.BRANDPACK_ID," & vbCrLf & _
                    " ISNULL(SUM(PO.PO_ORIGINAL_QTY),0)AS TOTAL_PO_ORIGINAL FROM Nufarm.DBO.VIEW_AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                    " INNER JOIN tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO ON PO.DISTRIBUTOR_ID = ABI.DISTRIBUTOR_ID AND PO.BRANDPACK_ID = ABI.BRANDPACK_ID " & vbCrLf & _
                    " WHERE RTRIM(LTRIM(ABI.AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
                    " AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS'" & vbCrLf & _
                    " GROUP BY ABI.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID,ABI.BRANDPACK_ID OPTION(KEEP PLAN);"
            Me.ResetCommandText(CommandType.Text, Query)
            tblPOByBrandPack.Clear()
            setDataAdapter(Me.SqlCom).Fill(tblPOByBrandPack)
            If tblPOByBrandPack.Rows.Count > 0 Then
                For i1 As Integer = 0 To tblPOByBrandPack.Rows.Count - 1
                    ACH_HEADER_ID = tblPOByBrandPack.Rows(i1)("DISTRIBUTOR_ID").ToString() & "|" & tblPOByBrandPack.Rows(i1)("AGREE_BRAND_ID").ToString() & "|" & Flag
                    Dim BrandPackID As String = tblPOByBrandPack.Rows(i1)("BRANDPACK_ID").ToString()
                    AchDetailID = ACH_HEADER_ID & "|" & BrandPackID
                    RowsSelect = tblAchDetail.Select("ACH_DETAIL_ID = '" & AchDetailID & "'")
                    If RowsSelect.Length > 0 Then
                        Row = RowsSelect(0)
                        Row.BeginEdit()
                        Row("TOTAL_PO") = tblPOByBrandPack.Rows(i1)("TOTAL_PO_ORIGINAL")
                        ' Row("TOTAL_PO_AMOUNT") = tblPO.Rows(i1)("TOTAL_PO_AMOUNT")
                        Row.EndEdit()
                    End If
                Next
            End If
            Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                       "DECLARE @V_START_DATE SMALLDATETIME ;" & vbCrLf & _
                       "SET @V_START_DATE = (SELECT START_DATE FROM AGREE_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO); " & vbCrLf & _
                       "SELECT TOP 1 AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE,AA.QS_TREATMENT_FLAG FROM AGREE_AGREEMENT AA INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                       " WHERE AA.END_DATE < @V_START_DATE " & vbCrLf & _
                       " AND DA.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO) ORDER BY AA.START_DATE DESC ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Dim tblStartDate As New DataTable("T_StartDate1")
            ''SET privouse Agreement   
            tblStartDate.Clear() : Me.setDataAdapter(Me.SqlCom).Fill(tblStartDate)
            If tblStartDate.Rows.Count > 0 Then
                PrevAgreementNo = tblStartDate.Rows(0)("AGREEMENT_NO")
            End If
            'TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_ACTUAL, ACH_DISPRO, DISPRO,
            'CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsNew, IsChanged, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3, GPPBF3, GPCPQ1

            'TOTAL_CPQ1,TOTAL_CPQ2,TOTAL_CPQ2,
            'TOTAL_CPQ3,TOTAL_CPF1,TOTAL_CPF2,TOTAL_PBF3

            'Dim PBQ1 As Object = Nothing, PBEQ1 As Object = Nothing, PBQ2 As Object = Nothing, PBEQ2 As Object = Nothing, _
            '                   PBQ3 As Object = Nothing, PBEQ3 As Object = Nothing, PBQ4 As Object = Nothing, PBEQ4 As Object = Nothing, _
            '                   PBS1 As Object = Nothing, PBES1 As Object = Nothing, PBS2 As Object = Nothing, PBES2 As Object = Nothing, _
            '                   PBStartYear As Object = Nothing, PBEndyear As Object = Nothing
            'Dim CPQ1 As Object = Nothing, CPEQ1 As Object = Nothing, CPQ2 As Object = Nothing, CPEQ2 As Object = Nothing, _
            'CPQ3 As Object = Nothing, CPEQ3 As Object = Nothing, CPS1 As Object = Nothing, CPES1 As Object = Nothing
            Dim CPF1 As Object = Nothing, CPEF1 As Object = Nothing, CPF2 As Object = Nothing, CPEF2 As Object = Nothing, CPQ2 As Object = Nothing, CPQ3 As Object = Nothing, _
            CPEQ2 As Object = Nothing, CPEQ3 As Object = Nothing, CPQ1 As Object = Nothing, CPEQ1 As Object = Nothing
            Select Case Flag
                Case "F2"
                    CPEF1 = StartDate.AddDays(-1)
                    CPF1 = Convert.ToDateTime(CPEF1).AddMonths(-4).AddDays(1)
                    'strFlag
                Case "F3"
                    If IsTransitionTime Then

                        'EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
                        'StartDateQ2 = EndDateQ1.AddDays(1)
                        'EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
                        'StartDateQ3 = EndDateQ2.AddDays(1)
                        'EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
                        'StartDateQ4 = EndDateQ3.AddDays(1)
                        CPQ1 = StartDatePKD
                        CPEQ1 = StartDatePKD.AddMonths(3).AddDays(-1)
                        CPQ2 = Convert.ToDateTime(CPEQ1).AddDays(1)
                        CPEQ2 = Convert.ToDateTime(CPQ2).AddMonths(3).AddDays(-1)

                        CPQ3 = Convert.ToDateTime(CPEQ2).AddDays(1) 'cuma dua bulan pebruari dan maret
                        CPEQ3 = Convert.ToDateTime(CPQ3).AddMonths(2).AddDays(-1)

                        'CPEQ3 = StartDate.AddDays(-1)
                        'CPQ3 = Convert.ToDateTime(CPEQ3).AddMonths(-3).AddDays(1)
                        'CPEQ2 = Convert.ToDateTime(CPQ3).AddDays(-1)
                        'CPQ2 = Convert.ToDateTime(CPEQ2).AddMonths(-3).AddDays(1)
                        'CPEQ1 = Convert.ToDateTime(CPQ2).AddDays(-1)
                        'CPQ1 = Convert.ToDateTime(CPEQ1).AddMonths(-3).AddDays(1)
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
                                 "       	    SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
                                 "     		)" & vbCrLf & _
                                 "       AND PO.PO_REF_DATE >= @START_DATE AND PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' " & vbCrLf & _
                                 "      )INV1   " & vbCrLf & _
                                 " GROUP BY DISTRIBUTOR_ID,BRAND_ID OPTION(KEEP PLAN); "

            '---------------------QUERY UNTUK MENTOTAL KAN TOTAL_invoice BRANDPACK DIANTARA START_DATE AND END_DATE PO grouped by BRANDPACK---------------------------
            Dim Query2 As String = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                                   " SELECT DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID,ISNULL(SUM(QTY),0) AS TOTAL_INVOICE  " & vbCrLf & _
                                   "  FROM( " & vbCrLf & _
                                   "       SELECT PO.DISTRIBUTOR_ID,PO.BRAND_ID,PO.BRANDPACK_ID,(ISNULL(INV.QTY,0)/PO.SPPB_QTY)* PO.PO_ORIGINAL_QTY AS QTY " & vbCrLf & _
                                   "       FROM tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO " & vbCrLf & _
                                   "       INNER JOIN COMPARE_ITEM Tmbp ON PO.BRANDPACK_ID = Tmbp.BRANDPACK_ID_DTS " & vbCrLf & _
                                   "       INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON Tmbp.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
                                   "       AND ((PO.RUN_NUMBER = INV.REFERENCE) OR (PO.PO_REF_NO = INV.PONUMBER)) " & vbCrLf & _
                                   "            WHERE PO.DISTRIBUTOR_ID = SOME( " & vbCrLf & _
                                   "       	    SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
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
                        Dim PBEF2 = Convert.ToDateTime(PBF2).AddMonths(4).AddDays(-1)
                        Dim PBF3 As Object = Convert.ToDateTime(PBEF2).AddDays(1)
                        Dim PBEF3 As Object = PBEndDate
                        'PBF2
                        If Not IsNothing(PBF2) Then
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBF2)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBEF2))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "PBF2_DIST")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_PBF2")
                                End If
                            End If
                        End If
                        If Not IsNothing(PBF3) Then
                            'PBF3
                            Me.ResetCommandText(CommandType.Text, Query1)
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBF3)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBEF3))
                            tblTemp = New DataTable("T_TEMP")
                            tblTemp.Clear()
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "PBF3_DIST")
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
                    If tblStartDate.Rows.Count > 0 Then
                        Dim PBStartDate As Object = Nothing
                        Dim PBEndDate As Object = Nothing
                        PBStartDate = Convert.ToDateTime(tblStartDate.Rows(0)("START_DATE"))
                        PBEndDate = Convert.ToDateTime(tblStartDate.Rows(0)("END_DATE"))
                        Dim PBFlag As String = tblStartDate.Rows(0)("QS_TREATMENT_FLAG").ToString()
                        If PBFlag = "S" Then
                            Dim PBS2 As Object = Convert.ToDateTime(PBEndDate).AddMonths(-6).AddDays(-1) 'ambil cuma 6 bulan startdate hanya agustus dan enddate po sampai 
                            Dim PBSES2 = PBEndDate
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBS2)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBSES2))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "PBS2_DIST")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_PBS2")
                                End If
                            End If
                        ElseIf PBFlag = "F" Then
                            Dim PBF1 = PBStartDate
                            Dim PBEF1 = Convert.ToDateTime(PBF1).AddMonths(4).AddDays(-1)
                            Dim PBF2 = Convert.ToDateTime(PBEF1).AddDays(1)
                            Dim FBEF2 = Convert.ToDateTime(PBF2).AddMonths(4).AddDays(-1)
                            Dim PBF3 As Object = Convert.ToDateTime(FBEF2).AddDays(1)
                            Dim PBEF3 As Object = PBEndDate
                            'PBF3
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBF3)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBEF3))
                            tblTemp = New DataTable("T_TEMP")
                            tblTemp.Clear()
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "PBF3_DIST")
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
                    If Not IsNothing(CPF1) Then
                        Me.ResetCommandText(CommandType.Text, Query1)
                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPF1)
                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPEF1))
                        tblTemp = New DataTable("T_TEMP")
                        tblTemp.Clear()
                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                        If tblTemp.Rows.Count > 0 Then
                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "CPF1_DIST")
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
                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "CPF2_DIST")
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
                        Me.ResetCommandText(CommandType.Text, Query1)
                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPF1)
                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPEF1))
                        tblTemp = New DataTable("T_TEMP")
                        tblTemp.Clear()
                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                        If tblTemp.Rows.Count > 0 Then
                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "CPF1_DIST")
                            Me.ResetCommandText(CommandType.Text, Query2)
                            tblTemp = New DataTable("T_TEMP")
                            tblTemp.Clear()
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_CPF1")
                            End If
                        End If
                    End If
                    If Not IsNothing(CPQ1) Then
                        Me.ResetCommandText(CommandType.Text, Query1)
                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPQ1)
                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPEQ1))
                        tblTemp = New DataTable("T_TEMP")
                        tblTemp.Clear()
                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                        If tblTemp.Rows.Count > 0 Then
                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "CPQ1_DIST")
                            Me.ResetCommandText(CommandType.Text, Query2)
                            tblTemp = New DataTable("T_TEMP")
                            tblTemp.Clear()
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_CPQ1")
                            End If
                        End If
                    End If
                    If Not IsNothing(CPQ2) Then
                        Me.ResetCommandText(CommandType.Text, Query1)
                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPQ2)
                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPEQ2))
                        tblTemp = New DataTable("T_TEMP")
                        tblTemp.Clear()
                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                        If tblTemp.Rows.Count > 0 Then
                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "CPQ2_DIST")
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
                        Me.ResetCommandText(CommandType.Text, Query1)
                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPQ3)
                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPEQ3))
                        tblTemp = New DataTable("T_TEMP")
                        tblTemp.Clear()
                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                        If tblTemp.Rows.Count > 0 Then
                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "CPQ3_DIST")
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
            'AMBIL DATA POTENSI BERDASARKAN AGREEMENTNO,DENGAN COLUM DISTRIBUTOR_ID,BRAND_ID,
            Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                    "SELECT GP.IDApp,ACHIEVEMENT_ID = DA.DISTRIBUTOR_ID + '|' + ABI.AGREE_BRAND_ID + '|' + @FLAG,GP.CPF1,GP.CPF2,GP.PBF2,GP.PBF3,GP.CPQ2,GP.CPQ3 " & vbCrLf & _
                    " FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRAND_INCLUDE ABI ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                    " INNER JOIN GIVEN_PROGRESSIVE GP ON GP.AGREE_BRAND_ID = ABI.AGREE_BRAND_ID WHERE RTRIM(LTRIM(ABI.AGREEMENT_NO)) = @AGREEMENT_NO ;"
            Me.ResetCommandText(CommandType.Text, Query)
            'Me.SqlCom.Parameters.RemoveAt("@START_DATE")
            'Me.SqlCom.Parameters.RemoveAt("@END_DATE")
            Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag)
            tblGP.Clear()
            setDataAdapter(Me.SqlCom).Fill(tblGP)
            If tblGP.Rows.Count > 0 Then
                Dim rows() As DataRow = Nothing
                For i As Integer = 0 To tblGP.Rows.Count - 1
                    'rows = tblAchHeader.Select("ACHIEVEMENT_ID = '" & tblGP.Rows(i)("DISTRIBUTOR_ID").ToString() & "|" & tblGP.Rows(i)("AGREE_BRAND_ID").ToString() & "|" & Flag & "'")
                    rows = tblAchHeader.Select("ACH_HEADER_ID = '" & tblGP.Rows(i)("ACHIEVEMENT_ID") & "'")
                    If rows.Length > 0 Then
                        rows(0).BeginEdit()
                        rows(0)("GP_ID") = tblGP.Rows(i)("IDApp")
                        If IsDBNull(tblGP.Rows(i)("CPF1")) Or IsNothing(tblGP.Rows(i)("CPF1")) Then
                            rows(0)("GPCPF1") = 0
                        Else
                            rows(0)("GPCPF1") = Convert.ToDecimal(tblGP.Rows(i)("CPF1"))
                        End If

                        If IsDBNull(tblGP.Rows(i)("CPF2")) Or IsNothing(tblGP.Rows(i)("CPF2")) Then
                            rows(0)("GPCPF2") = 0
                        Else
                            rows(0)("GPCPF2") = Convert.ToDecimal(tblGP.Rows(i)("CPF2"))
                        End If

                        If IsDBNull(tblGP.Rows(i)("CPQ2")) Or IsNothing(tblGP.Rows(i)("CPQ2")) Then
                            rows(0)("GPCPQ2") = 0
                        Else
                            rows(0)("GPCPQ2") = Convert.ToDecimal(tblGP.Rows(i)("CPQ2"))
                        End If

                        If IsDBNull(tblGP.Rows(i)("CPQ3")) Or IsNothing(tblGP.Rows(i)("CPQ3")) Then
                            rows(0)("GPCPQ3") = 0
                        Else
                            rows(0)("GPCPQ3") = Convert.ToDecimal(tblGP.Rows(i)("CPQ3"))
                        End If

                        If IsDBNull(tblGP.Rows(i)("PBF3")) Or IsNothing(tblGP.Rows(i)("PBF3")) Then
                            rows(0)("GPPBF3") = 0
                        Else
                            rows(0)("GPPBF3") = Convert.ToDecimal(tblGP.Rows(i)("PBF3"))
                        End If

                        If IsDBNull(tblGP.Rows(i)("PBF2")) Or IsNothing(tblGP.Rows(i)("PBF2")) Then
                            rows(0)("GPPBF2") = 0
                        Else
                            rows(0)("GPPBF2") = Convert.ToDecimal(tblGP.Rows(i)("PBF2"))
                        End If
                        rows(0).EndEdit()
                        rows(0).AcceptChanges()
                    End If
                Next
            End If
            tblAchHeader.AcceptChanges()
            Return True
        End Function
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

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overloads Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
                Me.DropTempTable()
                Dispose(True)
                GC.SuppressFinalize(Me)
            End If
            Me.disposedValue = True
        End Sub
        Private Sub DropTempTable()

            '---------------UNCOMENT THIS AFTER DEBUGGIN -----------------------------
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
            Me.OpenConnection()
            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        'Public Overloads Sub Dispose(ByVal disposing As Boolean)
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.

        '    Dispose(True)
        '    GC.SuppressFinalize(Me)
        'End Sub

    End Class

    Public Class DPDAchievementN
        Inherits NufarmBussinesRules.DistributorAgreement.Target_Agreement : Implements IDisposable

        Public Sub New()
            MyBase.New()
        End Sub
        Private tblDiscProgressive As DataTable = Nothing
        Private tblPrevAchievement As DataTable = Nothing
        Private tblCurAchiement As DataTable = Nothing
        Public IsTransitionTime As Boolean = False
        Private curAgreeStartDate As Object = Nothing
        Private curAgreeEndDate As Object = Nothing
        Private PrevAgreementNo As String = ""
        Private tblAVGPrice As DataTable = Nothing
        Private mustDeletedBeforeInsert As Boolean = False
        Private MustReinsertedData As Boolean = True
        Dim tblGP As New DataTable("T_GP")
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
                .Add(New DataColumn("AVGPriceID", Type.GetType("System.Int32")))
                .Add(New DataColumn("ISTARGET_GROUP", Type.GetType("System.Boolean")))
                .Item("ISTARGET_GROUP").DefaultValue = False

                .Add(New DataColumn("CombinedWith", Type.GetType("System.String")))
                .Item("CombinedWith").DefaultValue = DBNull.Value
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


                .Add(New DataColumn("GP_ID", Type.GetType("System.Int64")))
                .Item("GP_ID").DefaultValue = 0
                'ACH_HEADER_ID, DISTRIBUTOR_ID, AGREEMENT_NO, BRAND_ID, TARGET_FM, TARGET_PL, TARGET_VALUE, 

                'TOTAL_ACTUAL
                .Add(New DataColumn("TOTAL_ACTUAL", Type.GetType("System.Decimal")))
                .Item("TOTAL_ACTUAL").DefaultValue = 0
                'ACH_DISPRO
                .Add(New DataColumn("ACH_DISPRO", Type.GetType("System.Decimal")))
                .Item("ACH_DISPRO").DefaultValue = 0
                'DISC_QTY
                .Add(New DataColumn("ACH_BY_CAT", Type.GetType("System.Decimal")))
                .Item("ACH_BY_CAT").DefaultValue = 0
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
                '  TOTAL_CPQ2, TOTAL_PBQ2, 
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
                ' TOTAL_CPF1, TOTAL_CPF2, 
                .Add(New DataColumn("TOTAL_CPF1", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPF1").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPF2", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPF2").DefaultValue = 0

                'TOTAL_PBF2
                .Add(New DataColumn("TOTAL_PBF2", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBF2").DefaultValue = 0

                'TOTAL_PBF3
                .Add(New DataColumn("TOTAL_PBF3", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBF3").DefaultValue = 0

                'TOTAL_PBS2
                .Add(New DataColumn("TOTAL_PBS2", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBS2").DefaultValue = 0


                .Add(New DataColumn("GPPBF2", Type.GetType("System.Decimal"))) ''diambil dari progressive discount sebelumnya
                .Item("GPPBF2").DefaultValue = 0

                .Add(New DataColumn("GPPBF3", Type.GetType("System.Decimal"))) ''diambil dari progressive discount sebelumnya
                .Item("GPPBF3").DefaultValue = 0

                .Add(New DataColumn("GPPBS2", Type.GetType("System.Decimal")))
                .Item("GPPBS2").DefaultValue = 0

                ''untuk perhitungan saja bukan untuk ke database
                .Add(New DataColumn("TOTAL_CPQ1", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ1").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPQ2", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ2").DefaultValue = 0

                ' TOTAL_CPQ3,
                .Add(New DataColumn("TOTAL_CPQ3", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ3").DefaultValue = 0


                'ACTUAL_DIST,PO_DIST,PO_VALUE_DIST,DISC_DIST,CPQ1_DIST,CPQ2_DIST,CPQ3_DIST,CPF1_DIST,CPF2_DIST,PBF3_DIST

                .Add(New DataColumn("ACTUAL_DIST", Type.GetType("System.Decimal")))
                .Item("ACTUAL_DIST").DefaultValue = 0

                .Add(New DataColumn("PO_DIST", Type.GetType("System.Decimal")))
                .Item("PO_DIST").DefaultValue = 0

                .Add(New DataColumn("PO_VALUE_DIST", Type.GetType("System.Decimal")))
                .Item("PO_VALUE_DIST").DefaultValue = 0

                .Add(New DataColumn("DISC_DIST", Type.GetType("System.Decimal")))
                .Item("DISC_DIST").DefaultValue = 0

                .Add(New DataColumn("CPQ1_DIST", Type.GetType("System.Decimal")))
                .Item("CPQ1_DIST").DefaultValue = 0

                .Add(New DataColumn("CPQ2_DIST", Type.GetType("System.Decimal")))
                .Item("CPQ2_DIST").DefaultValue = 0

                .Add(New DataColumn("CPQ3_DIST", Type.GetType("System.Decimal")))
                .Item("CPQ3_DIST").DefaultValue = 0

                .Add(New DataColumn("CPF1_DIST", Type.GetType("System.Decimal")))
                .Item("CPF1_DIST").DefaultValue = 0

                .Add(New DataColumn("CPF2_DIST", Type.GetType("System.Decimal")))
                .Item("CPF2_DIST").DefaultValue = 0


                .Add(New DataColumn("PBF2_DIST", Type.GetType("System.Decimal")))
                .Item("PBF2_DIST").DefaultValue = 0

                .Add(New DataColumn("PBF3_DIST", Type.GetType("System.Decimal")))
                .Item("PBF3_DIST").DefaultValue = 0

                .Add(New DataColumn("PBS2_DIST", Type.GetType("System.Decimal")))
                .Item("PBS2_DIST").DefaultValue = 0

                '.Add(New DataColumn("GPPBQ3", Type.GetType("System.Decimal")))
                '.Item("GPPBQ3").DefaultValue = 0

                '.Add(New DataColumn("GPPBQ4", Type.GetType("System.Decimal")))
                '.Item("GPPBQ4").DefaultValue = 0

                '.Add(New DataColumn("GPPBS2", Type.GetType("System.Decimal")))
                '.Item("GPPBS2").DefaultValue = 0

                '.Add(New DataColumn("GPPBYear", Type.GetType("System.Decimal")))
                '.Item("GPPBPYear").DefaultValue = 0

                .Add(New DataColumn("GPCPF1", Type.GetType("System.Decimal")))
                .Item("GPCPF1").DefaultValue = 0

                .Add(New DataColumn("GPCPF2", Type.GetType("System.Decimal")))
                .Item("GPCPF2").DefaultValue = 0

                .Add(New DataColumn("GPCPQ2", Type.GetType("System.Decimal")))
                .Item("GPCPQ2").DefaultValue = 0

                .Add(New DataColumn("GPCPQ3", Type.GetType("System.Decimal")))
                .Item("GPCPQ3").DefaultValue = 0


                '.Add(New DataColumn("GPCPS1", Type.GetType("System.Decimal")))
                '.Item("GPCPS1").DefaultValue = 0

                '.Add(New DataColumn("GPPBY", Type.GetType("System.Decimal")))
                '.Item("GPPBY").DefaultValue = 0
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


                .Add(New DataColumn("TOTAL_PBF2", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBF2").DefaultValue = 0

                .Add(New DataColumn("TOTAL_PBF3", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBF3").DefaultValue = 0

                .Add(New DataColumn("TOTAL_PBS2", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBS2").DefaultValue = 0

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
            End With
            Dim Key(1) As DataColumn : Key(0) = tblAchDetail.Columns("ACHIEVEMENT_BRANDPACK_ID")
            tblAchDetail.PrimaryKey = Key
        End Sub
        Public Shadows Function GetAgreementNo(ByVal Flag As String, Optional ByVal DistributorID As String = "", Optional ByVal AGREEMENT_NO As String = "", Optional ByVal DefaultMaxyear As Integer = 2) As DataView
            Try
                Query = "SET NOCOUNT ON;"
                Dim strFlag As String = Flag
                strFlag = strFlag.Remove(1, 1)
                If AGREEMENT_NO <> "" And DistributorID <> "" Then
                    Query &= vbCrLf
                    'If Me.IsTransitionTime Then
                    Query &= "SELECT TOP 100 AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                              " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%'))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                              " AND AA.QS_TREATMENT_FLAG = 'F' ;"
                    'Else
                    '    Query &= "SELECT TOP 100 AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                    '          " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%'))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                    '          " AND AA.QS_TREATMENT_FLAG = @FLAG ;"
                    'End If

                ElseIf DistributorID <> "" Then
                    'If IsTransitionTime Then
                    Query &= "SELECT AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                              " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID ))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                              " AND AA.QS_TREATMENT_FLAG = 'F'  AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - @DefMaxyear ;"
                    'Else
                    '    Query &= "SELECT AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                    '          " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID ))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                    '          " AND AA.QS_TREATMENT_FLAG = @FLAG  AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - @DefMaxyear ;"
                    'End If
                ElseIf AGREEMENT_NO <> "" Then
                    Query &= vbCrLf
                    'If IsTransitionTime Then
                    Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%' " & vbCrLf & _
                            " AND QS_TREATMENT_FLAG = 'F' ;"
                    'Else
                    '    Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%' " & vbCrLf & _
                    '            " AND QS_TREATMENT_FLAG = @FLAG ;"
                    'End If

                Else
                    Query &= vbCrLf
                    'If IsTransitionTime Then
                    Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) - @DefMaxyear " & vbCrLf & _
                             " AND QS_TREATMENT_FLAG ='F' ;"
                    'Else
                    '    Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) - @DefMaxyear " & vbCrLf & _
                    '             " AND QS_TREATMENT_FLAG = @FLAG ;"
                    'End If

                End If
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Me.AddParameter("@DefMaxyear", SqlDbType.Int, DefaultMaxyear)
                Me.AddParameter("@FLAG", SqlDbType.Char, strFlag, 1)
                Dim dtTable As New DataTable("T_Agreement")
                dtTable.Clear() : Me.FillDataTable(dtTable)
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Shadows Function GetDistributorAgrement(ByVal Flag As String, Optional ByVal Searchstring As String = "") As DataView
            Try
                'Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                '        "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                '        " WHERE AA.END_DATE >=  GETDATE() AND AA.QS_TREATMENT_FLAG = '" & Flag.Remove(1, 1) & "' AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                '        " AND DR.DISTRIBUTOR_NAME LIKE '%" & Searchstring & "%';"
                Flag = Flag.Remove(1, 1)
                If Flag = "Q" Or Flag = "F" Then
                    If Searchstring = "" Then
                        'If IsTransitionTime Then
                        Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                                "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                " WHERE YEAR(AA.END_DATE) >=  YEAR(@GETDATE) - 1 AND AA.QS_TREATMENT_FLAG ='F' AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) "
                        'Else
                        '    Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                        '            "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                        '            " WHERE YEAR(AA.END_DATE) >=  YEAR(@GETDATE) - 1 AND AA.QS_TREATMENT_FLAG = @FLAG AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) "
                        'End If

                    Else
                        'hilangkan end_date karene custom search
                        'If IsTransitionTime Then
                        Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                                 "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                 " WHERE AA.QS_TREATMENT_FLAG = ='F' AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                                 " AND DR.DISTRIBUTOR_NAME LIKE '%" & Searchstring & "%';"
                        'Else
                        '    Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                        '             "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                        '             " WHERE AA.QS_TREATMENT_FLAG = @FLAG AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                        '             " AND DR.DISTRIBUTOR_NAME LIKE '%" & Searchstring & "%';"
                        'End If
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
        Public Shadows Function getAchievement(ByVal Flag As String, Optional ByVal DISTRIBUTOR_ID As String = "", Optional ByVal ListAGREEMENT_NO As List(Of String) = Nothing) As DataSet

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
                'ACHIEVEMENT_DISPRO, DISPRO, DISC_QTY, TOTAL_CPQ1,  TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3

                '-----------------------------------Header Query -->achievement header--------------------------
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                        "SELECT ACRH.ACH_HEADER_ID,ACRH.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,ACRH.AGREEMENT_NO,AA.START_DATE,AA.END_DATE,ACRH.FLAG,ACRH.BRAND_ID,BB.BRAND_NAME," & vbCrLf & _
                        "ISNULL(AP.AVGPRICE,0) AS AVG_PRICE_FM,ISNULL(AP.AVGPRICE_PL,0) AS AVG_PRICE_PL,ACRH.TARGET_FM,ACRH.TARGET_PL,ISNULL(AP.AVGPRICE,0) * ACRH.TOTAL_TARGET AS TARGET_VALUE,ACRH.TOTAL_PO_VALUE,ACRH.TOTAL_TARGET," & vbCrLf & _
                        " ACRH.TOTAL_PO,ACRH.TOTAL_ACTUAL,ACRH.BALANCE,ACRH.ACH_BY_CAT/100 AS ACH_BY_CAT,ACRH.ACH_DISPRO/100 AS ACHIEVEMENT_DISPRO,ACRH.DISPRO/100 AS DISPRO,  " & vbCrLf & _
                        " ACRH.DISC_QTY, ACRH.TOTAL_CPQ1,ACRH.TOTAL_PBS2,ISTARGET_GROUP,COMBINED_BRAND = CASE WHEN (ACRH.COMBINED_BRAND_ID IS NOT NULL) THEN 1 ELSE 0 END, " & vbCrLf & _
                        " ACRH.TOTAL_CPF1,ACRH.TOTAL_CPF2,TOTAL_PBF3,ACRH.[DESCRIPTIONS],ACRH.ACTUAL_DIST,ACRH.PO_DIST,ACRH.PO_VALUE_DIST,ACRH.DISC_DIST,ACRH.CPQ1_DIST,ACRH.CPQ2_DIST,ACRH.CPQ3_DIST,ACRH.CPF1_DIST,ACRH.CPF2_DIST,ACRH.PBF3_DIST,ACRH.PBS2_DIST " & vbCrLf & _
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
                If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                Else : Me.CreateCommandSql("", Query)
                End If
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
                Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; SELECT ACD.ACH_DETAIL_ID,ACD.ACH_HEADER_ID,ACRH.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,ACRH.AGREEMENT_NO,ACD.BRANDPACK_ID,BP.BRANDPACK_NAME," & vbCrLf & _
                          "ACD.TOTAL_PO,ACD.TOTAL_ACTUAL, ACD.DISC_QTY," & vbCrLf & _
                          " ACD.TOTAL_PBS2,ACD.TOTAL_CPF1,ACD.TOTAL_CPF2,ACD.TOTAL_PBF3,ACD.[DESCRIPTIONS]" & vbCrLf & _
                          " FROM ACHIEVEMENT_DETAIL ACD INNER JOIN ACHIEVEMENT_HEADER ACRH " & vbCrLf & _
                          " ON ACD.ACH_HEADER_ID = ACRH.ACH_HEADER_ID INNER JOIN BRND_BRANDPACK BP " & vbCrLf & _
                          "  ON ACD.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                          " INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = ACRH.DISTRIBUTOR_ID " & vbCrLf
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
                Me.DisposeTempDB()
                Me.CloseConnection() : Me.ClearCommandParameters()
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
        Public Function CalculateAchievement(ByVal Flag As String, Optional ByVal tblAgreement As DataTable = Nothing, Optional ByVal DISTRIBUTOR_ID As String = "") As DataSet
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
                    'edit after debugging
                    'IsTransitionTime = StartDate >= New DateTime(2019, 8, 1) And EndDate <= New DateTime(2020, 7, 31)
                    PrevAgreementNo = ""
                    curAgreeStartDate = StartDate
                    curAgreeEndDate = EndDate
                    StartDateF1 = StartDate
                    EndDateF1 = StartDateF1.AddMonths(4).AddDays(-1)
                    StartDateF2 = EndDateF1.AddDays(1)
                    EndDateF2 = StartDateF2.AddMonths(4).AddDays(-1)
                    StartDateF3 = EndDateF2.AddDays(1)
                    EndDateF3 = EndDate
                    'start calculating
                    Select Case Flag
                        Case "F1"
                            ''prepare data
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateF1)
                            If NufarmBussinesRules.SharedClass.ServerDate.Year = 2021 Then 'agustus september di hitung jadi F1
                                strDecStartDate = common.CommonClass.getNumericFromDate(StartDateF1.AddMonths(-2))
                            End If
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateF1)
                            Me.CreateTempTable(StartDateF1, EndDateF1, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount(Flag, StartDateF1, EndDateF1, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString().Trim(), tblAchHeader, tblAchDetail, MessageDetail, HasTarget, EndDate)
                        Case "F2"
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateF2)
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateF2)
                            Me.CreateTempTable(StartDateF2, EndDateF2, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount(Flag, StartDateF2, EndDateF2, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString().Trim(), tblAchHeader, tblAchDetail, MessageDetail, HasTarget, EndDate)
                        Case "F3"
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateF3)
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateF3)
                            Me.CreateTempTable(StartDateF3, EndDateF3, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount(Flag, StartDateF3, EndDateF3, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString().Trim(), tblAchHeader, tblAchDetail, MessageDetail, HasTarget, EndDate)
                    End Select
                    If Not HasTarget Then : tblDistAgreement.Rows.RemoveAt(i) : i -= 1 : End If
                Next
                If MessageDetail <> "" Then
                    Me.MessageError = MessageHeader & vbCrLf & MessageDetail
                    'System.Windows.Forms.MessageBox.Show(Me.MessageError, "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    Me.ClearCommandParameters()
                    Me.DisposeTempDB()
                    Throw New System.Exception(Me.MessageError)
                End If
                ''drop table ''tempd db
                Me.ClearCommandParameters()
                Dim ListAgreement As New List(Of String)
                For i As Integer = 0 To tblDistAgreement.Rows.Count - 1
                    ListAgreement.Add(tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString().Trim())
                Next
                Dim Ds As DataSet = Me.getAchievement(Flag, DISTRIBUTOR_ID, ListAgreement)
                Me.DisposeTempDB()
                Return Ds
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.RollbackTransaction()
                Me.DisposeTempDB() : Me.CloseConnection()
                Throw ex
            End Try

        End Function
        Private Sub getTblCurProgAndPrevAchievement()
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT ABI.AGREE_BRAND_ID,APD.UP_TO_PCT,APD.PRGSV_DISC_PCT,APD.QSY_DISC_FLAG FROM AGREE_PROG_DISC APD " & vbCrLf & _
                    " INNER JOIN AGREE_BRAND_INCLUDE ABI ON ABI.AGREE_BRAND_ID = APD.AGREE_BRAND_ID WHERE RTRIM(LTRIM(ABI.AGREEMENT_NO)) = @AGREEMENT_NO;"
            tblDiscProgressive = New DataTable("tblProgressive")
            Me.ResetCommandText(CommandType.Text, Query)
            setDataAdapter(Me.SqlCom).Fill(tblDiscProgressive)
            'Me.SqlCom.Parameters.RemoveAt("@START_DATE")
            'Me.SqlCom.Parameters.RemoveAt("@END_DATE")

            'GET achievement data from current agreement previous flag 
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT ACH_HEADER_ID AS ACHIEVEMENT_ID,AGREEMENT_NO + BRAND_ID AS AGREE_BRAND_ID,FLAG,DISPRO FROM ACHIEVEMENT_HEADER WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO AND FLAG IN('F1','F2') "
            'If Me.IsTransitionTime Then
            '    Query = "SELECT ACHIEVEMENT_ID,AGREEMENT_NO + BRAND_ID AS AGREE_BRAND_ID,FLAG,DISPRO FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND FLAG IN('','Q2','Q3') " & vbCrLf & _
            '    " UNION " & vbCrLf
            'End If

            Me.ResetCommandText(CommandType.Text, Query)
            Me.tblCurAchiement = New DataTable("tblProgressive")
            setDataAdapter(Me.SqlCom).Fill(Me.tblCurAchiement)

            'GET achievement data from previous agreement agreement previous flag only PBF3
            'get previous agreement no 
            Query = "SET NOCOUNT ON;" & vbCrLf & _
            " DECLARE @V_PREV_AG_NO VARCHAR(25);DECLARE @V_START_DATE SMALLDATETIME ; " & vbCrLf & _
            " SET @V_START_DATE  = (SELECT TOP 1 START_DATE FROM AGREE_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO);" & vbCrLf & _
            " SET @V_PREV_AG_NO = (SELECT TOP 1 AA.AGREEMENT_NO FROM AGREE_AGREEMENT AA INNER JOIN DISTRIBUTOR_AGREEMENT DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
            "                       WHERE RTRIM(LTRIM(AA.AGREEMENT_NO)) != @AGREEMENT_NO AND AA.START_DATE < @V_START_DATE AND DATEDIFF(MONTH,START_DATE,END_DATE) =11 " & vbCrLf & _
            "                       AND DISTRIBUTOR_ID = ANY(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO) ORDER BY AA.START_DATE DESC);  " & vbCrLf & _
            " SELECT ACH_HEADER_ID AS ACHIEVEMENT_ID,AGREEMENT_NO + BRAND_ID AS AGREE_BRAND_ID,FLAG,DISPRO FROM ACHIEVEMENT_HEADER WHERE AGREEMENT_NO = @V_PREV_AG_NO AND FLAG IN ('S2','F3') ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.tblPrevAchievement = New DataTable("tblPreviousAchievement")
            setDataAdapter(Me.SqlCom).Fill(Me.tblPrevAchievement)
        End Sub
        Private Sub GenerateDiscount(ByVal FLAG As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal AgreementNO As String, _
            ByRef tblAchHeader As DataTable, ByRef tblAchDetail As DataTable, ByRef Message As String, ByRef HasTarget As Boolean, ByVal EndDateAgreement As DateTime)

            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT  1 WHERE EXISTS(SELECT AA.AGREEMENT_NO FROM AGREE_AGREEMENT AA INNER JOIN AGREE_BRAND_INCLUDE ABI ON ABI.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN AGREE_PROG_DISC APD " & vbCrLf & _
                    "                       ON APD.AGREE_BRAND_ID = ABI.AGREE_BRAND_ID  " & vbCrLf & _
                    "                       WHERE RTRIM(LTRIM(AA.AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
                    "                       AND APD.QSY_DISC_FLAG = 'F1')"

            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNO, 32)
            Me.AddParameter("@FLAG", SqlDbType.VarChar, FLAG)
            Dim retval As Object = Me.SqlCom.ExecuteScalar()
            If IsNothing(retval) Or IsDBNull(retval) Then : Me.ClearCommandParameters() : Message &= "FLAG for " & FLAG & vbCrLf & "Has not been defined yet" : Return : End If
            If CInt(retval) <= 0 Then : Me.ClearCommandParameters() : HasTarget = False : Message &= "FLAG for " & FLAG & vbCrLf & "Has not been defined yet" : Return : End If

            CreateOrRecreatTblAchHeader(tblAchHeader)
            CreateOrRecreateTblAchDetail(tblAchDetail)

            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT COUNT(AGREEMENT_NO) FROM DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO OPTION(KEEP PLAN);"

            Me.ResetCommandText(CommandType.Text, Query)
            Dim IsTargetGroup As Boolean = (CInt(Me.SqlCom.ExecuteScalar()) > 1)

            Me.FillAchHeaderAndDetail(AgreementNO, FLAG, tblAchHeader, tblAchDetail, StartDate, EndDate, IsTargetGroup)
            If tblAchHeader.Rows.Count <= 0 Then : Me.ClearCommandParameters() : Return : Message &= "No data to calculate for " & FLAG : End If

            'get tbl progresive, and curent achievement dan previous achievement
            getTblCurProgAndPrevAchievement()
            UpdateTotalAllActualAndPO(tblAchHeader)
            'HITUNG DISCOUNT tblAchHeader (sudah include hitung previous discount)
            Me.CalculateHeaderNufarm(AgreementNO, FLAG, tblAchHeader)
            Me.ClearCommandParameters()
            'hitung disc detail sudah include penghitungan disc previos
            Me.CalculatePreviousDetail(FLAG, tblAchDetail, tblAchHeader)

            ''save to database
            Me.SaveToDataBase(AgreementNO, FLAG, tblAchHeader, tblAchDetail)
        End Sub
        Private Sub SaveToDataBase(ByVal AgreementNO As String, ByVal FLAG As String, ByRef tblAchHeader As DataTable, ByRef tblAchDetail As DataTable)

            'header dulu
            Dim RowsSelect() As DataRow = tblAchHeader.Select("")
            If RowsSelect.Length > 0 Then
                For i As Integer = 0 To RowsSelect.Length - 1
                    RowsSelect(i).SetAdded()
                Next
            End If
            'INSERT DETAIL
            Dim RowsSelectDetail = tblAchDetail.Select("")
            If RowsSelectDetail.Length > 0 Then
                For i As Integer = 0 To RowsSelectDetail.Length - 1
                    RowsSelectDetail(i).SetAdded()
                Next
            End If

            Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans

            ''delete dulu data by agreement dan flag
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
            "DELETE FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO AND FLAG = @FLAG);"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNO, 25)
            Me.AddParameter("@FLAG", SqlDbType.VarChar, FLAG)
            Me.SqlCom.ExecuteScalar()

            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "DELETE FROM ACHIEVEMENT_HEADER WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO AND FLAG = @FLAG;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.SqlCom.ExecuteScalar()

            'INSERT HEADER
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    " INSERT INTO ACHIEVEMENT_HEADER(ACH_HEADER_ID, AGREEMENT_NO, DISTRIBUTOR_ID, BRAND_ID, AvgPriceID, FLAG, TOTAL_TARGET, TARGET_FM, TARGET_PL, TOTAL_PO, " & vbCrLf & _
                    " TOTAL_PO_VALUE, TOTAL_ACTUAL, BALANCE, ACH_DISPRO, ACH_BY_CAT,DISPRO, DISC_QTY, TOTAL_CPQ1, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_CPF1, TOTAL_CPF2,TOTAL_PBF2, TOTAL_PBF3,TOTAL_PBS2,COMBINED_BRAND_ID,ISTARGET_GROUP," & vbCrLf & _
                    " DESCRIPTIONS, ACTUAL_DIST,PO_DIST,PO_VALUE_DIST,DISC_DIST,CPQ1_DIST,CPQ2_DIST,CPQ3_DIST,CPF1_DIST,CPF2_DIST,PBF2_DIST,PBF3_DIST,PBS2_DIST,GP_ID,CreatedDate, CreatedBy) " & vbCrLf & _
                    " VALUES(@ACH_HEADER_ID, @AGREEMENT_NO, @DISTRIBUTOR_ID, @BRAND_ID, @AvgPriceID, @FLAG, @TOTAL_TARGET, @TARGET_FM, @TARGET_PL, @TOTAL_PO, " & vbCrLf & _
                    " @TOTAL_PO_VALUE, @TOTAL_ACTUAL, @BALANCE, @ACH_DISPRO, @ACH_BY_CAT,@DISPRO, @DISC_QTY, @TOTAL_CPQ1, @TOTAL_CPQ2, @TOTAL_CPQ3, @TOTAL_CPF1, " & vbCrLf & _
                    " @TOTAL_CPF2,@TOTAL_PBF2, @TOTAL_PBF3,@TOTAL_PBS2,@COMBINED_BRAND_ID,@ISTARGET_GROUP,@DESCRIPTIONS,@ACTUAL_DIST,@PO_DIST,@PO_VALUE_DIST,@DISC_DIST,@CPQ1_DIST,@CPQ2_DIST,@CPQ3_DIST,@CPF1_DIST,@CPF2_DIST,@PBF2_DIST,@PBF3_DIST,@PBS2_DIST,@GP_ID,@CreatedDate, @CreatedBy);"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.ResetAdapterCRUD()
            With Me.SqlCom
                .Parameters.Add("@GP_ID", SqlDbType.BigInt, 0, "GP_ID")
                .Parameters.Add("@ACH_HEADER_ID", SqlDbType.VarChar, 55, "ACH_HEADER_ID")
                '.Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 25, "AGREEMENT_NO")
                .Parameters.Add("@DISTRIBUTOR_ID", SqlDbType.VarChar, 10, "DISTRIBUTOR_ID")
                .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 7, "BRAND_ID")
                .Parameters.Add("@AvgPriceID", SqlDbType.Int, 0, "AvgPriceID")
                '.Parameters.Add("@FLAG", SqlDbType.VarChar, 4, "FLAG")
                .Parameters.Add("@TOTAL_TARGET", SqlDbType.Decimal, 0, "TOTAL_TARGET")
                .Parameters.Add("@TARGET_FM", SqlDbType.Decimal, 0, "TARGET_FM")
                .Parameters.Add("@TARGET_PL", SqlDbType.Decimal, 0, "TARGET_PL")
                .Parameters.Add("@TOTAL_PO", SqlDbType.Decimal, 0, "TOTAL_PO")
                .Parameters.Add("@TOTAL_PO_VALUE", SqlDbType.Decimal, 0, "TOTAL_PO_VALUE")
                .Parameters.Add("@TOTAL_ACTUAL", SqlDbType.Decimal, 0, "TOTAL_ACTUAL")
                .Parameters.Add("@BALANCE", SqlDbType.Decimal, 0, "BALANCE")
                .Parameters.Add("@ACH_DISPRO", SqlDbType.Decimal, 0, "ACH_DISPRO")
                .Parameters.Add("@ACH_BY_CAT", SqlDbType.Decimal, 0, "ACH_BY_CAT")
                .Parameters.Add("@DISPRO", SqlDbType.Decimal, 0, "DISPRO")
                .Parameters.Add("@DISC_QTY", SqlDbType.Decimal, 0, "DISC_QTY")
                .Parameters.Add("@TOTAL_CPQ1", SqlDbType.Decimal, 0, "TOTAL_CPQ1")
                .Parameters.Add("@TOTAL_CPQ2", SqlDbType.Decimal, 0, "TOTAL_CPQ2")
                .Parameters.Add("@TOTAL_CPQ3", SqlDbType.Decimal, 0, "TOTAL_CPQ3")
                .Parameters.Add("@TOTAL_CPF1", SqlDbType.Decimal, 0, "TOTAL_CPF1")
                .Parameters.Add("@TOTAL_CPF2", SqlDbType.Decimal, 0, "TOTAL_CPF2")
                .Parameters.Add("@TOTAL_PBF2", SqlDbType.Decimal, 0, "TOTAL_PBF2")
                .Parameters.Add("@TOTAL_PBF3", SqlDbType.Decimal, 0, "TOTAL_PBF3")
                .Parameters.Add("@TOTAL_PBS2", SqlDbType.Decimal, 0, "TOTAL_PBS2")

                .Parameters.Add("@DESCRIPTIONS", SqlDbType.VarChar, 250, "DESCRIPTIONS")
                .Parameters.Add("@ACTUAL_DIST", SqlDbType.Decimal, 0, "ACTUAL_DIST")
                .Parameters.Add("@PO_DIST", SqlDbType.Decimal, 0, "PO_DIST")
                .Parameters.Add("@PO_VALUE_DIST", SqlDbType.Decimal, 0, "PO_VALUE_DIST")
                .Parameters.Add("@DISC_DIST", SqlDbType.Decimal, 0, "DISC_DIST")
                .Parameters.Add("@CPQ1_DIST", SqlDbType.Decimal, 0, "CPQ1_DIST")
                .Parameters.Add("@CPQ2_DIST", SqlDbType.Decimal, 0, "CPQ2_DIST")
                .Parameters.Add("@CPQ3_DIST", SqlDbType.Decimal, 0, "CPQ3_DIST")
                .Parameters.Add("@CPF1_DIST", SqlDbType.Decimal, 0, "CPF1_DIST")
                .Parameters.Add("@CPF2_DIST", SqlDbType.Decimal, 0, "CPF2_DIST")
                .Parameters.Add("@PBF2_DIST", SqlDbType.Decimal, 0, "PBF2_DIST")
                .Parameters.Add("@PBF3_DIST", SqlDbType.Decimal, 0, "PBF3_DIST")
                .Parameters.Add("@PBS2_DIST", SqlDbType.Decimal, 0, "PBS2_DIST")

                .Parameters.Add("@COMBINED_BRAND_ID", SqlDbType.VarChar, 50, "CombinedWith")
                .Parameters.Add("@ISTARGET_GROUP", SqlDbType.Bit, 0, "ISTARGET_GROUP")

                .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                Me.SqlDat.InsertCommand = Me.SqlCom
                Me.SqlDat.Update(RowsSelect) : Me.ClearCommandParameters()
            End With

            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    " INSERT INTO ACHIEVEMENT_DETAIL (ACH_DETAIL_ID, ACH_HEADER_ID, BRANDPACK_ID, TOTAL_PO, TOTAL_ACTUAL, DISC_QTY, TOTAL_CPQ1," & vbCrLf & _
                    " TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_CPF1, TOTAL_CPF2,TOTAL_PBF2, TOTAL_PBF3,TOTAL_PBS2, DESCRIPTIONS, CreatedDate, CreatedBy)" & vbCrLf & _
                    " VALUES(@ACH_DETAIL_ID, @ACH_HEADER_ID, @BRANDPACK_ID, @TOTAL_PO, @TOTAL_ACTUAL, @DISC_QTY, @TOTAL_CPQ1, " & vbCrLf & _
                    " @TOTAL_CPQ2, @TOTAL_CPQ3, @TOTAL_CPF1, @TOTAL_CPF2,@TOTAL_PBF2,@TOTAL_PBF3,@TOTAL_PBS2,@DESCRIPTIONS, @CreatedDate, @CreatedBy) ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.ResetAdapterCRUD()
            With Me.SqlCom
                .Parameters.Add("@ACH_HEADER_ID", SqlDbType.VarChar, 55, "ACH_HEADER_ID")
                .Parameters.Add("@ACH_DETAIL_ID", SqlDbType.VarChar, 70, "ACH_DETAIL_ID")
                .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                .Parameters.Add("@TOTAL_PO", SqlDbType.Decimal, 0, "TOTAL_PO")
                .Parameters.Add("@TOTAL_ACTUAL", SqlDbType.Decimal, 0, "TOTAL_ACTUAL")
                .Parameters.Add("@DISC_QTY", SqlDbType.Decimal, 0, "DISC_QTY")
                .Parameters.Add("@TOTAL_CPQ1", SqlDbType.Decimal, 0, "TOTAL_CPQ1")
                .Parameters.Add("@TOTAL_CPQ2", SqlDbType.Decimal, 0, "TOTAL_CPQ2")
                .Parameters.Add("@TOTAL_CPQ3", SqlDbType.Decimal, 0, "TOTAL_CPQ3")
                .Parameters.Add("@TOTAL_CPF1", SqlDbType.Decimal, 0, "TOTAL_CPF1")
                .Parameters.Add("@TOTAL_CPF2", SqlDbType.Decimal, 0, "TOTAL_CPF2")
                .Parameters.Add("@TOTAL_PBF2", SqlDbType.Decimal, 0, "TOTAL_PBF2")
                .Parameters.Add("@TOTAL_PBF3", SqlDbType.Decimal, 0, "TOTAL_PBF3")
                .Parameters.Add("@TOTAL_PBS2", SqlDbType.Decimal, 0, "TOTAL_PBS2")
                .Parameters.Add("@DESCRIPTIONS", SqlDbType.VarChar, 250, "DESCRIPTIONS")
                .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                Me.SqlDat.InsertCommand = Me.SqlCom
                Me.SqlDat.Update(RowsSelectDetail)
                Me.ClearCommandParameters()
            End With
            Me.CommiteTransaction()
        End Sub
        Private Sub CalculatePreviousDetail(ByVal Flag As String, ByRef tblAchDetail As DataTable, ByVal tblAchHeader As DataTable)
            Dim DVPrevAch As DataView = Nothing, Descriptions As String = ""
            If Not IsNothing(Me.tblPrevAchievement) Then
                DVPrevAch = tblPrevAchievement.DefaultView()
            End If
            Dim DVCurAch As DataView = tblCurAchiement.DefaultView
            'Dim DVGivenProg As DataView = Me.tblGP.DefaultView
            'DVGivenProg.Sort = "IDApp"

            For i As Integer = 0 To tblAchHeader.Rows.Count - 1

                Dim RowHeader As DataRow = tblAchHeader.Rows(i), RowDetail As DataRow = Nothing
                Dim Dispro As Decimal = Convert.ToDecimal(RowHeader("DISPRO"))
                Dim GPID As Object = RowHeader("GP_ID")
                '===========COMMENT THIS AFTER DEBUGGING=========================
                'If BrandID = "77230" Or BrandID = "77240" Then
                '    Stop
                'End If
                '===============END COMMENT THIS AFTER DEBUGGING ==============================
                Dim AchHeaderID As String = RowHeader("ACH_HEADER_ID").ToString()
                Dim RowsDetail() As DataRow = tblAchDetail.Select("ACH_HEADER_ID = '" & AchHeaderID & "'")
                Dim Index As Integer = -1
                For i1 As Integer = 0 To RowsDetail.Length - 1
                    RowDetail = RowsDetail(i1)
                    Dim PrevDisPro As Decimal = 0, BonusQTy As Decimal = 0
                    Dim totalInvoiceBeforeF3 As Decimal = 0, totalInvoiceBeforeS2 As Decimal = 0, totalInvoiceCurrentF1 As Decimal = 0, totalInvoiceCurrentF2 As Decimal = 0, _
                    totalInvoiceCurrentQ1 As Decimal = 0, totalInvoiceCurrentQ2 As Decimal = 0, totalInvoiceCurrentQ3 As Decimal = 0
                    Dim AchDetailID As String = RowDetail("ACH_DETAIL_ID")
                    Dim DiscQtyBefore As Decimal = 0

                    Descriptions = ""
                    Select Case Flag
                        Case "F3" 'CPQ3,F1,F2
                            totalInvoiceCurrentF1 = RowDetail("TOTAL_CPF1")
                            Dim DiscF1 As Decimal = 0, DiscF2 As Decimal = 0
                            Dim AchHeaderIDF2 As String = "", AchHeaderIDF1 As String
                            If CDec(totalInvoiceCurrentF1) > 0 Then
                                AchHeaderIDF1 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF1 = AchHeaderIDF1 + "F1"

                                If CDec(RowHeader("GPCPF1")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPCPF1"))
                                ElseIf Not IsNothing(DVCurAch) Then
                                    DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF1 & "'"
                                    If DVCurAch.Count > 0 Then
                                        PrevDisPro = DVCurAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF1 = (PrevDisPro / 100) * totalInvoiceCurrentF1
                                    DiscQtyBefore += DiscF1
                                    If Descriptions <> "" Then
                                        Descriptions &= ", "
                                    End If
                                    Descriptions &= String.Format("F1 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF1, DiscF1)
                                End If
                            End If
                            totalInvoiceCurrentF2 = RowDetail("TOTAL_CPF2")
                            If CDec(totalInvoiceCurrentF2) > 0 Then
                                AchHeaderIDF2 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF2 = AchHeaderIDF2 + "F2"

                                If CDec(RowHeader("GPCPF2")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPCPF2"))
                                ElseIf Not IsNothing(DVCurAch) Then
                                    DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF2 & "'"
                                    If DVCurAch.Count > 0 Then
                                        PrevDisPro = DVCurAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF2 = (PrevDisPro / 100) * totalInvoiceCurrentF2
                                    DiscQtyBefore += DiscF2
                                    If Descriptions <> "" Then
                                        Descriptions &= ", "
                                    End If
                                    Descriptions &= String.Format("F2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF2, DiscF2)
                                End If
                            End If
                        Case "F2"
                            totalInvoiceCurrentF1 = RowDetail("TOTAL_CPF1")
                            Dim AchHeaderIDF1 As String = ""
                            Dim DiscF1 As Decimal = 0
                            If CDec(totalInvoiceCurrentF1) > 0 Then
                                AchHeaderIDF1 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF1 = AchHeaderIDF1 + "F1"
                                If CDec(RowHeader("GPCPF1")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPCPF1"))
                                ElseIf Not IsNothing(DVCurAch) Then
                                    DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF1 & "'"
                                    If DVCurAch.Count > 0 Then
                                        PrevDisPro = DVCurAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF1 = (PrevDisPro / 100) * totalInvoiceCurrentF1
                                    DiscQtyBefore = DiscF1
                                    Descriptions &= String.Format("F1 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF1, DiscF1)
                                End If
                            End If
                            totalInvoiceBeforeF3 = RowDetail("TOTAL_PBF3")
                            Dim AchHeaderIDF3 As String = "", DiscF3 As Decimal = 0
                            If CDec(totalInvoiceBeforeF3) > 0 Then
                                AchHeaderIDF3 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF3 = AchHeaderIDF3 + "F3"

                                If CDec(RowHeader("GPPBF3")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPPBF3"))
                                ElseIf Not IsNothing(DVPrevAch) Then
                                    DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF3 & "'"
                                    If DVPrevAch.Count > 0 Then
                                        PrevDisPro = DVPrevAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF3 = (PrevDisPro / 100) * totalInvoiceBeforeF3
                                    DiscQtyBefore += DiscF3
                                    If Descriptions <> "" Then
                                        Descriptions &= ", "
                                    End If
                                    Descriptions &= String.Format("F3 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF3, DiscF3)
                                End If
                            End If
                            totalInvoiceBeforeS2 = RowDetail("TOTAL_PBS2")
                            Dim AchHeaderIDPBS2 As String = "", DiscPBS2 As Decimal = 0
                            If CDec(totalInvoiceBeforeS2) > 0 Then
                                AchHeaderIDPBS2 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDPBS2 = AchHeaderIDPBS2 + "S2"

                                If CDec(RowHeader("GPPBS2")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPPBS2"))
                                ElseIf Not IsNothing(DVPrevAch) Then
                                    DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDPBS2 & "'"
                                    If DVPrevAch.Count > 0 Then
                                        PrevDisPro = DVPrevAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscPBS2 = (PrevDisPro / 100) * totalInvoiceBeforeS2
                                    DiscQtyBefore += DiscPBS2
                                    If Descriptions <> "" Then
                                        Descriptions &= ", "
                                    End If
                                    Descriptions &= String.Format("S2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeS2, DiscPBS2)
                                End If
                            End If

                        Case "F1"
                            totalInvoiceBeforeF3 = RowDetail("TOTAL_PBF3")
                            Dim AchHeaderIDF3 As String = "", DiscF3 As Decimal = 0
                            Dim totalInvoiceBeforeF2 = RowDetail("TOTAL_PBF2")

                            If CDec(totalInvoiceBeforeF3) > 0 And Not IsNothing(DVPrevAch) Then
                                AchHeaderIDF3 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF3 = AchHeaderIDF3 + "F3"

                                If CDec(RowHeader("GPPBF3")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPPBF3"))
                                ElseIf Not IsNothing(DVPrevAch) Then
                                    DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF3 & "'"
                                    If DVPrevAch.Count > 0 Then
                                        PrevDisPro = DVPrevAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF3 = (PrevDisPro / 100) * totalInvoiceBeforeF3
                                    DiscQtyBefore = DiscF3
                                    Descriptions &= String.Format("F3 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF3, DiscF3)
                                End If
                            End If
                            Dim AchHeaderIDF2 As String = "", DiscF2 As Decimal = 0

                            If CDec(totalInvoiceBeforeF2) > 0 And Not IsNothing(DVPrevAch) Then
                                AchHeaderIDF2 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDF2 = AchHeaderIDF2 + "F2"

                                If CDec(RowHeader("GPPBF2")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPPBF2"))
                                ElseIf Not IsNothing(DVPrevAch) Then
                                    DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDF2 & "'"
                                    If DVPrevAch.Count > 0 Then
                                        PrevDisPro = DVPrevAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscF2 = (PrevDisPro / 100) * totalInvoiceBeforeF2
                                    DiscQtyBefore += DiscF2
                                    If Descriptions <> "" Then
                                        Descriptions &= ", "
                                    End If
                                    Descriptions &= String.Format("F2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF2, DiscF2)
                                End If
                            End If

                            totalInvoiceBeforeS2 = RowDetail("TOTAL_PBS2")
                            Dim AchHeaderIDPBS2 As String = "", DiscPBS2 As Decimal = 0
                            If CDec(totalInvoiceBeforeS2) > 0 Then
                                AchHeaderIDPBS2 = AchHeaderID.Remove(AchHeaderID.LastIndexOf("|") + 1)
                                AchHeaderIDPBS2 = AchHeaderIDPBS2 + "S2"

                                If CDec(RowHeader("GPPBS2")) > 0 Then
                                    PrevDisPro = Convert.ToDecimal(RowHeader("GPPBS2"))
                                ElseIf Not IsNothing(DVPrevAch) Then
                                    DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchHeaderIDPBS2 & "'"
                                    If DVPrevAch.Count > 0 Then
                                        PrevDisPro = DVPrevAch(0)("DISPRO")
                                    End If
                                End If
                                If PrevDisPro > 0 Then
                                    DiscPBS2 = (PrevDisPro / 100) * totalInvoiceBeforeS2
                                    DiscQtyBefore += DiscPBS2
                                    If Descriptions <> "" Then
                                        Descriptions &= ", "
                                    End If
                                    Descriptions &= String.Format("S2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeS2, DiscPBS2)
                                End If
                            End If
                    End Select
                    Dim TotalActual As Decimal = Convert.ToDecimal(RowDetail("TOTAL_ACTUAL"))
                    BonusQTy = (Dispro / 100) * TotalActual
                    BonusQTy += DiscQtyBefore

                    RowDetail.BeginEdit()
                    RowDetail("DISC_QTY") = BonusQTy
                    RowDetail("DESCRIPTIONS") = Descriptions
                    RowDetail.EndEdit()
                Next
            Next
            tblAchDetail.AcceptChanges()
        End Sub

        Private Sub UpdateTotalAllActualAndPO(ByRef tblAchHeader As DataTable)
            Dim listAgreeBrandIDs As New List(Of String)
            For i As Integer = 0 To tblAchHeader.Rows.Count - 1
                Dim AgreeBrandID As String = tblAchHeader.Rows(i)("AGREE_BRAND_ID").ToString()
                If Not listAgreeBrandIDs.Contains(AgreeBrandID) Then
                    listAgreeBrandIDs.Add(AgreeBrandID)
                End If
            Next
            'update all
            For i As Integer = 0 To listAgreeBrandIDs.Count - 1
                Dim rows() As DataRow = tblAchHeader.Select("AGREE_BRAND_ID = '" & listAgreeBrandIDs(i) & "'")
                Dim TotalPO As Decimal = 0, TotalActual As Decimal = 0, TotalPOValue As Decimal = 0, _
                TotalCPQ1 As Decimal = 0, TotalCPQ2 As Decimal = 0, TotalCPQ3 As Decimal = 0, TotalCPF1 As Decimal = 0, TotalCPF2 As Decimal = 0, _
                TotalPBF2 As Decimal = 0, TotalPBF3 As Decimal = 0, TotalPBS2 As Decimal = 0
                Dim row As DataRow = Nothing
                For i1 As Integer = 0 To rows.Length - 1
                    row = rows(i1)
                    TotalPO += Convert.ToDecimal(row("PO_DIST"))
                    TotalActual += Convert.ToDecimal(row("ACTUAL_DIST"))
                    TotalPOValue += Convert.ToDecimal(row("PO_VALUE_DIST"))
                    TotalCPQ1 += Convert.ToDecimal(row("CPQ1_DIST"))
                    TotalCPQ2 += Convert.ToDecimal(row("CPQ2_DIST"))
                    TotalCPQ3 += Convert.ToDecimal(row("CPQ3_DIST"))
                    TotalCPF1 += Convert.ToDecimal(row("CPF1_DIST"))
                    TotalCPF2 += Convert.ToDecimal(row("CPF2_DIST"))
                    TotalPBF2 += Convert.ToDecimal(row("PBF2_DIST"))
                    TotalPBF3 += Convert.ToDecimal(row("PBF3_DIST"))
                    TotalPBS2 = Convert.ToDecimal(row("PBS2_DIST"))
                Next
                For i1 As Integer = 0 To rows.Length - 1
                    row = rows(i1)
                    row.BeginEdit()
                    'TOTAL_PO, TOTAL_PO_VALUE, BALANCE, TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_ACTUAL,
                    row("TOTAL_ACTUAL") = TotalActual
                    row("TOTAL_PO") = TotalPO
                    row("TOTAL_PO_VALUE") = TotalPOValue
                    row("TOTAL_CPQ1") = TotalCPQ1
                    row("TOTAL_CPQ2") = TotalCPQ2
                    row("TOTAL_CPQ3") = TotalCPQ3
                    row("TOTAL_CPF1") = TotalCPF1
                    row("TOTAL_CPF2") = TotalCPF2
                    row("TOTAL_PBF2") = TotalPBF2
                    row("TOTAL_PBF3") = TotalPBF3
                    row("TOTAL_PBS2") = TotalPBS2
                    row.EndEdit()
                Next
            Next
        End Sub
        Private Sub getTotalForCertainBrands(ByVal RowsSelect() As DataRow, ByRef TotalTarget As Decimal, ByRef TotalPO As Decimal)
            Dim listAgreeBrandIDs As New List(Of String)
            For i As Integer = 0 To RowsSelect.Length - 1
                Dim AgreeBrandID As String = RowsSelect(i)("AGREE_BRAND_ID").ToString()
                If Not listAgreeBrandIDs.Contains(AgreeBrandID) Then
                    listAgreeBrandIDs.Add(AgreeBrandID)
                    TotalTarget += Convert.ToDecimal(RowsSelect(i)("TOTAL_TARGET"))
                    TotalPO += Convert.ToDecimal(RowsSelect(i)("TOTAL_PO"))
                End If
            Next
        End Sub
        Private Sub CalculateHeaderNufarm(ByVal AgreementNO As String, ByVal FLAG As String, ByRef tblAchHeader As DataTable)
            Dim RowsSelect() As DataRow = tblAchHeader.Select(""), RowSSelect1() As DataRow = Nothing
            Dim AchievementDispro As Decimal = 0, BonusQty As Decimal = 0, _
            Balance As Decimal = 0, Dispro As Decimal = 0 : Dim Row As DataRow = Nothing, Description As String = ""
            'RowsSelect = tblAchHeader.Select(CategoryIsNewOrIsChanged & " = " & True)
            Dim ListCombBrand As New List(Of String)
            Dim tblListBonusBefore As New DataTable("T_BonusBefore")
            Dim colPB As New DataColumn("PB", Type.GetType("System.String"))
            Dim colGP As New DataColumn("GP", Type.GetType("System.Decimal"))
            Dim colAgree As New DataColumn("AGREE_BRAND_ID", Type.GetType("System.String"))
            Dim Rows As DataRow() = Nothing
            colPB.DefaultValue = String.Empty : colGP.DefaultValue = 0
            tblListBonusBefore.Columns.Add(colAgree)
            tblListBonusBefore.Columns.Add(colPB)
            tblListBonusBefore.Columns.Add(colGP)
            Dim ListAgreeBrand1 As New List(Of String), ListAgreeBrand As New List(Of String)
            For i As Integer = 0 To RowsSelect.Length - 1
                If Not ListAgreeBrand1.Contains(RowsSelect(i)("AGREE_BRAND_ID").ToString()) Then
                    ListAgreeBrand1.Add(RowsSelect(i)("AGREE_BRAND_ID").ToString())
                End If
            Next
            Dim DVCustom As DataView = Me.tblDiscProgressive.DefaultView
            DVCustom.Sort = ""
            For i As Integer = 0 To ListAgreeBrand1.Count - 1
                Dim Target As Decimal = 0, TotalPOOriginal As Decimal = 0, TotalActual As Decimal = 0, CombAgreeBrandID As String = ""
                Dispro = 0 : AchievementDispro = 0 : BonusQty = 0 : Balance = 0
                tblListBonusBefore.Clear() : Description = ""
                If Not ListAgreeBrand.Contains(ListAgreeBrand1(i)) Then
                    ListAgreeBrand.Add(ListAgreeBrand1(i))
                    RowsSelect = tblAchHeader.Select("AGREE_BRAND_ID = '" & ListAgreeBrand1(i).ToString() & "'")
                    If Not RowsSelect.Length <= 0 Then
                        TotalPOOriginal = Convert.ToDecimal(RowsSelect(0)("TOTAL_PO"))
                        TotalActual = Convert.ToDecimal(RowsSelect(0)("TOTAL_ACTUAL"))
                        Target = Convert.ToDecimal(RowsSelect(0)("TOTAL_TARGET"))
                    End If
                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            CombAgreeBrandID = RowsSelect(0)("CombinedWith").ToString()
                            'If Not ListCombBrand.Contains(CombAgreeBrandID) Then
                            '    ListCombBrand.Add(CombAgreeBrandID)
                            'End If
                            RowSSelect1 = tblAchHeader.Select("AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                            'If (RowSSelect1(0)("BRAND_ID") = "77240" Or RowsSelect(0)("BRAND_ID") = "77230") Then
                            '    Stop
                            'End If
                            If Not ListAgreeBrand.Contains(CombAgreeBrandID) Then
                                ListAgreeBrand.Add(CombAgreeBrandID)
                            End If
                            If Not RowSSelect1.Length <= 0 Then
                                TotalPOOriginal += Convert.ToDecimal(RowSSelect1(0)("TOTAL_PO"))
                                TotalActual = Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL"))
                                Target = Convert.ToDecimal(RowSSelect1(0)("TOTAL_TARGET"))
                            End If
                        End If
                    End If
                    '-------------------------------------------------------------------------------------------
                    ''jika flag bukan 'y' 'hitung TotalActual
                    'If FLAG <> "Y" Then : Me.GetTotalActual(FLAG, StartDate, EndDate, Target, TotalActual) : End If
                    '--------------------------------------------------------------------------------------------
                    Dim Percentage As Decimal = 0
                    If Target <> 0 Then
                        Percentage = common.CommonClass.GetPercentage(100, TotalPOOriginal, Target)
                    End If
                    DVCustom.RowFilter = "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "' AND QSY_DISC_FLAG = '" & FLAG & "'"
                    If DVCustom.Count <= 0 Then
                        If (CombAgreeBrandID <> "") Then
                            DVCustom.RowFilter = "AGREE_BRAND_ID = '" & CombAgreeBrandID & "'" & "' AND QSY_DISC_FLAG = '" & FLAG & "'"
                        End If
                    End If
                    If DVCustom.Count > 0 Then
                        ''looping cari yang masuk dalam category
                        For i1 As Integer = 0 To DVCustom.Count - 1
                            If Not IsDBNull(DVCustom(i1)("UP_TO_PCT")) And Not IsNothing(DVCustom(i1)("UP_TO_PCT")) Then
                                If Percentage > Convert.ToDecimal(DVCustom(i1)("UP_TO_PCT")) Then
                                    Dispro = Convert.ToDecimal(DVCustom(i1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            End If
                        Next
                    End If

                    'check sharing target nya 
                    ''jika sharing target nya tidak tercapai
                    '' perhitungan sperti dibawah ini
                    AchievementDispro = Percentage : Balance = TotalPOOriginal - Target
                    If Balance > 0 Then : Balance = 0 : End If
                    'hitung bonusnya
                    BonusQty = (Dispro / 100) * TotalActual
                    'Dim TEST As String = ""
                    'Me.setTotalInvoiceBefore(FLAG, RowsSelect, RowSSelect1, tblListBonusBefore, Row, DescriptionTotal, Description, BonusQty, Rows)
                    For i1 As Integer = 0 To RowsSelect.Length - 1
                        'If ListAgreeBrand1(i) = "0359/NI/II/2016.20-2100903" Then
                        '    TEST = "FIND"
                        '    Stop
                        'End If
                        Dim DiscDist As Decimal = 0
                        Row = RowsSelect(i1) : Row.BeginEdit()
                        DiscDist = (Dispro / 100) * Convert.ToDecimal(Row("ACTUAL_DIST"))
                        CalculateDiscPrevious(FLAG, Row, Description, BonusQty, DiscDist)
                        Row("DISC_DIST") = DiscDist
                        Row("DISPRO") = Dispro
                        Row("ACH_BY_CAT") = AchievementDispro
                        Row("ACH_DISPRO") = AchievementDispro
                        Row("DISC_QTY") = BonusQty
                        Row("BALANCE") = Balance
                        Row("DESCRIPTIONS") = Description
                        'Row("CombinedWith") = DBNull.Value
                        Row("TOTAL_ACTUAL") = TotalActual
                        If (CombAgreeBrandID <> "") Then
                            Row("CombinedWith") = CombAgreeBrandID
                        End If
                        Row.EndEdit()
                    Next
                    If CombAgreeBrandID <> "" Then
                        For i1 As Integer = 0 To RowSSelect1.Length - 1
                            Row = RowSSelect1(i1) : Row.BeginEdit()
                            Dim DiscDist As Decimal = 0
                            Row = RowSSelect1(i1) : Row.BeginEdit()
                            DiscDist = (Dispro / 100) * Convert.ToDecimal(Row("ACTUAL_DIST"))
                            CalculateDiscPrevious(FLAG, Row, Description, BonusQty, DiscDist)
                            Row("DISC_DIST") = DiscDist
                            Row("DISPRO") = Dispro
                            Row("ACH_BY_CAT") = AchievementDispro
                            Row("ACH_DISPRO") = AchievementDispro
                            Row("DISC_QTY") = BonusQty
                            Row("BALANCE") = Balance
                            Row("DESCRIPTIONS") = Description
                            Row("CombinedWith") = ListAgreeBrand1(i).ToString()
                            Row("TOTAL_ACTUAL") = TotalActual
                            Row.EndEdit()
                        Next
                    End If
                End If
            Next
            tblAchHeader.AcceptChanges()
        End Sub
        ''' <summary>
        ''' </summary>
        ''' <param name="Flag"></param>
        ''' <param name="Row">DataRow tblAchHeader</param>
        ''' <param name="Description">Description of previous discount to get</param>
        ''' <param name="BonusQty">Disc Qty + all previous discount(TOTAL(CPQ2,CPQ3,CPF2,CPF1,PBF3))</param>
        ''' <remarks>Function to get and calculate Previos Actual PO, discount qty and descriptions to get</remarks>
        Private Sub CalculateDiscPrevious(ByVal Flag As String, ByVal Row As DataRow, ByRef Description As String, ByRef BonusQty As Decimal, ByRef DiscDist As Decimal)

            Dim totalInvoiceBeforeF3 As Decimal = 0, totalInvoiceBeforeF2 As Decimal = 0, totalInvoiceBeforeS2 As Decimal = 0, totalInvoiceCurrentF1 As Decimal = 0, totalInvoiceCurrentF2 As Decimal = 0, PrevDisPro As Decimal = 0, DVCurAch As DataView = tblCurAchiement.DefaultView, _
            DVPrevAch As DataView = Nothing
            'Dim DVGivenProg As DataView = Me.tblGP.DefaultView
            'DVGivenProg.Sort = "IDApp"
            Dim PBF3Dist As Decimal = 0, PBF2Dist As Decimal = 0, PBS2Dist As Decimal = 0, CPF1Dist As Decimal = 0, CPF2Dist As Decimal = 0, CPQ1Dist As Decimal = 0, CPQ2Dist As Decimal = 0, _
            CPQ3Dist As Decimal = 0
            Dim GPID As Object = Row("GP_ID")
            If Not IsNothing(Me.tblPrevAchievement) Then
                DVPrevAch = tblPrevAchievement.DefaultView()
            End If

            Dim rowsCheck() As DataRow = Nothing
            Dim Index As Integer = -1
            'AGREE_BRAND_ID,UP_TO_PCT,PRGSV_DISC_PCT
            Dim AchID As String = Row("ACH_HEADER_ID").ToString()
            Select Case Flag
                Case "F3" 'F1,F2
                    Dim DiscF1 As Decimal = 0, DiscF2 As Decimal = 0
                    Dim AchF2 As String = AchID.Remove(AchID.LastIndexOf("|") + 1) : AchF2 += "F2" 'AchID.Remove(AchID.LastIndexOf("|") + 1)
                    'AchF2 = AchF2 + "F2"
                    totalInvoiceCurrentF2 = Row("TOTAL_CPF2")
                    CPF2Dist = Row("CPF2_DIST")
                    If totalInvoiceCurrentF2 > 0 Then
                        If CDec(Row("GPCPF2")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPCPF2"))
                        ElseIf Not IsNothing(DVCurAch) Then
                            DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchF2 & "'"
                            If DVCurAch.Count > 0 Then
                                PrevDisPro = DVCurAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF2 = (PrevDisPro / 100) * totalInvoiceCurrentF2
                            DiscDist += (PrevDisPro / 100) * CPF2Dist
                            BonusQty += DiscF2
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("F2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF2, DiscF2)
                        End If
                    End If
                    Dim AchF1 As String = AchID.Remove(AchID.LastIndexOf("|") + 1) : AchF1 += "F1" ' AchID.Remove(AchID.LastIndexOf("|") + 1)
                    'AchF1 = AchF1 + "F1"
                    totalInvoiceCurrentF1 = Row("TOTAL_CPF1")
                    CPF1Dist = Row("CPF1_DIST")
                    If totalInvoiceCurrentF1 > 0 Then
                        If CDec(Row("GPCPF1")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPCPF1"))
                        ElseIf Not IsNothing(DVCurAch) Then
                            DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchF1 & "'"
                            If DVCurAch.Count > 0 Then
                                PrevDisPro = DVCurAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF1 = (PrevDisPro / 100) * totalInvoiceCurrentF1
                            DiscDist += (PrevDisPro / 100) * CPF1Dist
                            BonusQty += DiscF1
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("F1 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF1, DiscF1)
                        End If
                    End If
                Case "F2" 'F1, PBF3
                    Dim AchF1 As String = AchID.Remove(AchID.LastIndexOf("|") + 1)
                    AchF1 = AchF1 + "F1"
                    totalInvoiceCurrentF1 = Row("TOTAL_CPF1")
                    CPF1Dist = Row("CPF1_DIST")
                    Dim DiscF1 As Decimal = 0
                    If totalInvoiceCurrentF1 > 0 Then
                        If CDec(Row("GPCPF1")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPCPF1"))
                        ElseIf Not IsNothing(DVCurAch) Then
                            DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchF1 & "'"
                            If DVCurAch.Count > 0 Then
                                PrevDisPro = DVCurAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF1 = (PrevDisPro / 100) * totalInvoiceCurrentF1
                            DiscDist += (PrevDisPro / 100) * CPF1Dist
                            BonusQty += DiscF1
                            Description &= String.Format("F1 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceCurrentF1, DiscF1)
                        End If
                    End If
                    Dim AchPBF3 As String = AchID.Remove(AchID.LastIndexOf("|") + 1)
                    Dim DiscF3 As Decimal = 0
                    totalInvoiceBeforeF3 = Row("TOTAL_PBF3")
                    PBF3Dist = Row("PBF3_DIST")
                    AchPBF3 = AchPBF3 + "F3"
                    If totalInvoiceBeforeF3 > 0 Then
                        If CDec(Row("GPPBF3")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPPBF3"))
                        ElseIf Not IsNothing(DVPrevAch) Then
                            DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchPBF3 & "'"
                            If DVPrevAch.Count > 0 Then
                                PrevDisPro = DVPrevAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF3 = (PrevDisPro / 100) * totalInvoiceBeforeF3
                            DiscDist += (PrevDisPro / 100) * PBF3Dist
                            BonusQty += DiscF3
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("F3 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF3, DiscF3)
                        End If
                    End If
                    Dim AchPBS2 As String = AchID.Remove(AchID.LastIndexOf("|") + 1)
                    Dim DiscPBS2 As Decimal = 0
                    totalInvoiceBeforeS2 = Row("TOTAL_PBS2")
                    PBS2Dist = Row("PBS2_DIST")
                    AchPBS2 = AchPBS2 + "S2"
                    If totalInvoiceBeforeS2 > 0 Then
                        If CDec(Row("GPPBS2")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPPBS2"))
                        ElseIf Not IsNothing(DVPrevAch) Then
                            DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchPBS2 & "'"
                            If DVPrevAch.Count > 0 Then
                                PrevDisPro = DVPrevAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscPBS2 = (PrevDisPro / 100) * totalInvoiceBeforeS2
                            DiscDist += (PrevDisPro / 100) * PBS2Dist
                            BonusQty += DiscPBS2
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("S2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeS2, DiscPBS2)
                        End If
                    End If

                Case "F1"
                    Dim AchPBF3 As String = AchID.Remove(AchID.LastIndexOf("|") + 1)
                    Dim DiscF3 As Decimal = 0
                    totalInvoiceBeforeF3 = Row("TOTAL_PBF3")
                    PBF3Dist = Row("PBF3_DIST")
                    AchPBF3 = AchPBF3 + "F3"
                    If totalInvoiceBeforeF3 > 0 Then
                        If CDec(Row("GPPBF3")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPPBF3"))
                        ElseIf Not IsNothing(DVPrevAch) Then
                            DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchPBF3 & "'"
                            If DVPrevAch.Count > 0 Then
                                PrevDisPro = DVPrevAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF3 = (PrevDisPro / 100) * totalInvoiceBeforeF3
                            DiscDist += (PrevDisPro / 100) * PBF3Dist
                            BonusQty += DiscF3
                            Description &= String.Format("F3 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF3, DiscF3)
                        End If
                    End If

                    Dim AchPBF2 As String = AchID.Remove(AchID.LastIndexOf("|") + 1)
                    Dim DiscF2 As Decimal = 0
                    totalInvoiceBeforeF2 = Row("TOTAL_PBF2")
                    PBF2Dist = Row("PBF2_DIST")
                    AchPBF2 = AchPBF2 + "F2"
                    If totalInvoiceBeforeF2 > 0 Then
                        If CDec(Row("GPPBF2")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPPBF2"))
                        ElseIf Not IsNothing(DVPrevAch) Then
                            DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchPBF2 & "'"
                            If DVPrevAch.Count > 0 Then
                                PrevDisPro = DVPrevAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscF2 = (PrevDisPro / 100) * totalInvoiceBeforeF2
                            DiscDist += (PrevDisPro / 100) * PBF2Dist
                            BonusQty += DiscF2
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("F2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeF2, DiscF2)
                        End If
                    End If

                    Dim AchPBS2 As String = AchID.Remove(AchID.LastIndexOf("|") + 1)
                    Dim DiscPBS2 As Decimal = 0
                    totalInvoiceBeforeS2 = Row("TOTAL_PBS2")
                    PBS2Dist = Row("PBS2_DIST")
                    AchPBS2 = AchPBS2 + "S2"
                    If totalInvoiceBeforeS2 > 0 Then
                        If CDec(Row("GPPBS2")) > 0 Then
                            PrevDisPro = Convert.ToDecimal(Row("GPPBS2"))
                        ElseIf Not IsNothing(DVPrevAch) Then
                            DVPrevAch.RowFilter = "ACHIEVEMENT_ID = '" & AchPBS2 & "'"
                            If DVPrevAch.Count > 0 Then
                                PrevDisPro = DVPrevAch(0)("DISPRO")
                            End If
                        End If
                        If PrevDisPro > 0 Then
                            DiscPBS2 = (PrevDisPro / 100) * totalInvoiceBeforeS2
                            DiscDist += (PrevDisPro / 100) * PBS2Dist
                            BonusQty += DiscPBS2
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("S2 = {0:p} of {1:#,##0.000} = {2:#,##0.000}", PrevDisPro / 100, totalInvoiceBeforeS2, DiscPBS2)
                        End If
                    End If
            End Select
        End Sub
        Private Function FillAchHeaderAndDetail(ByVal AgreementNo As String, ByVal Flag As String, ByRef tblAchHeader As DataTable, _
            ByRef tblAchDetail As DataTable, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal IsTargetGroup As Boolean) As Boolean
            Query = "SET NOCOUNT ON ;" & vbCrLf & _
            " SELECT TOP 1 START_DATE, END_DATE FROM AGREE_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            Dim StartDatePKD As New DateTime(2019, 8, 1), EndDatePKD As New DateTime(2020, 7, 31)
            While Me.SqlRe.Read()
                StartDatePKD = Me.SqlRe.GetDateTime(0) : EndDatePKD = Me.SqlRe.GetDateTime(1)
            End While : Me.SqlRe.Close()
            'nanti harus di reset
            'IsTransitionTime = StartDatePKD >= New DateTime(2019, 8, 1) And EndDatePKD <= New DateTime(2020, 7, 31)
            '-----------------------------DECLARASI TABLE------------------------------------------------------------
            Dim Row As DataRow = Nothing, RowsSelect() As DataRow = Nothing, _
           ListAchievementID As New List(Of String), strFlag As String = ""
            Select Case Flag
                Case "F1" : strFlag = "FMP1"
                Case "F2" : strFlag = "FMP2"
                Case "F3" : strFlag = "FMP3"
            End Select


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
                     "  SELECT PO.PO_REF_NO,PO.PO_REF_DATE,PO.DISTRIBUTOR_ID,ABI.BRAND_ID,ABP.BRANDPACK_ID,OPB.PO_ORIGINAL_QTY,OPB.PO_PRICE_PERQTY,OOAB.QTY_EVEN + ISNULL(SB.TOTAL_DISC_QTY,0) AS SPPB_QTY,OOA.RUN_NUMBER ," & vbCrLf & _
                     "  IncludeDPD = CASE WHEN (OPB.ExcludeDPD = 0) THEN 'YESS' " & vbCrLf & _
                     "  WHEN (OPB.ExcludeDPD = 1) THEN 'NO'" & vbCrLf & _
                     "  WHEN ((OPB.PRICE_CATEGORY = 'SP') AND EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PRICE_TAG = OPB.PRICE_TAG AND PRICE = OPB.PO_PRICE_PERQTY AND START_DATE >= DATEADD(MONTH,-6,@START_DATE) AND END_DATE <= @END_DATE AND IncludeDPD = 1)) THEN 'YESS' " & vbCrLf & _
                     "  WHEN ((OPB.PRICE_CATEGORY = 'SP') AND EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PRICE_TAG = OPB.PRICE_TAG AND PRICE = OPB.PO_PRICE_PERQTY AND START_DATE >= DATEADD(MONTH,-6,@START_DATE) AND END_DATE <= @END_DATE AND IncludeDPD = 0)) THEN 'NO' " & vbCrLf & _
                     "  WHEN ((OPB.PRICE_CATEGORY = 'GP') AND EXISTS(SELECT PRICE_TAG FROM GEN_PLANT_PRICE WHERE PRICE_TAG = OPB.PRICE_TAG AND IncludeDPD = 1)) THEN 'YESS' " & vbCrLf & _
                     "  WHEN ((OPB.PRICE_CATEGORY = 'GP') AND EXISTS(SELECT PRICE_TAG FROM GEN_PLANT_PRICE WHERE PRICE_TAG = OPB.PRICE_TAG AND IncludeDPD = 0)) THEN 'NO' " & vbCrLf & _
                     "  WHEN (OPB.PRICE_CATEGORY = 'FM') THEN 'YESS' " & vbCrLf & _
                     "  ELSE 'NO' END " & vbCrLf & _
                     "  FROM Nufarm.dbo.AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                     "  INNER JOIN Nufarm.DBO.AGREE_BRANDPACK_INCLUDE ABP ON ABI.AGREE_BRAND_ID = ABP.AGREE_BRAND_ID" & vbCrLf & _
                     "  INNER JOIN Nufarm.dbo.ORDR_PO_BRANDPACK OPB ON OPB.BRANDPACK_ID = ABP.BRANDPACK_ID " & vbCrLf & _
                     "  INNER JOIN Nufarm.dbo.ORDR_PURCHASE_ORDER PO " & vbCrLf & _
                     "  ON PO.PO_REF_NO = OPB.PO_REF_NO INNER JOIN Nufarm.dbo.ORDR_ORDER_ACCEPTANCE OOA " & vbCrLf & _
                     "  ON OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                     "  INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOA.OA_ID = OOAB.OA_ID AND OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                     "  LEFT OUTER JOIN (SELECT OOBD.OA_BRANDPACK_ID,ISNULL(SUM(OOBD.DISC_QTY),0) AS TOTAL_DISC_QTY FROM ORDR_OA_BRANDPACK_DISC OOBD " & vbCrLf & _
                     "      WHERE OOBD.OA_BRANDPACK_ID = ANY(  " & vbCrLf & _
                     "      SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK OOB INNER JOIN ORDR_PO_BRANDPACK PB ON OOB.PO_BRANDPACK_ID = PB.PO_BRANDPACK_ID " & vbCrLf & _
                     "              INNER JOIN ORDR_PURCHASE_ORDER PO1 ON PO1.PO_REF_NO = PB.PO_REF_NO WHERE PO1.PO_REF_DATE >= @START_DATE AND PO1.PO_REF_DATE <= @END_DATE " & vbCrLf & _
                     "      ) " & vbCrLf & _
                     "      GROUP BY OOBD.OA_BRANDPACK_ID " & vbCrLf & _
                     "  )SB ON SB.OA_BRANDPACK_ID = OOAB.OA_BRANDPACK_ID" & vbCrLf & _
                     "  WHERE RTRIM(LTRIM(ABI.AGREEMENT_NO)) = @AGREEMENT_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND OPB.PO_ORIGINAL_QTY > 0 " & vbCrLf & _
                     "  AND PO.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO) " & vbCrLf & _
                     " )B ;" & vbCrLf & _
                     " --CREATE CLUSTERED INDEX IX_T_MASTER_PO ON ##T_MASTER_PO_" & Me.ComputerName & "(PO_REF_DATE,PO_REF_NO,RUN_NUMBER,DISTRIBUTOR_ID,BRANDPACK_ID) ;"
            '============================= END UNCOMMENT THIS AFTER DEBUGGING =============================================================
            Me.ResetCommandText(CommandType.Text, Query)
            If StartDate.Year = 2020 Then
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate.AddMonths(-12))
            Else
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate.AddMonths(-10))
            End If

            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)

            Me.SqlCom.ExecuteScalar()
            ''sum po inovoice_qty dimana po bukan di antara periode flag(periode sebelumnya)
            Me.SqlCom.Parameters.RemoveAt("@START_DATE")
            Me.SqlCom.Parameters.RemoveAt("@END_DATE")
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)

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
                    "       	 SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
                    "     		 )" & vbCrLf & _
                    "       AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' " & vbCrLf & _
                    "      )INV1   " & vbCrLf & _
                    " GROUP BY DISTRIBUTOR_ID,BRAND_ID ; " & vbCrLf & _
                    " --CREATE CLUSTERED INDEX IX_T_Agreement_Brand ON ##T_Agreement_Brand_" & Me.ComputerName & "(TOTAL_INVOICE,BRAND_ID) ;"
            Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()


            ''SEKALIAN AMBIL AVGPRICEID dan PriceFM & PricePL
            'chek data
            'query hanya insert ke database
            'jika total rows  di agreement brand by PSG <> total Rows di ach_header -hapus rows di ach header
            'jika ada perubahan data di ach_header karena ada perubahan actual di invoce --hapus rows di ach header
            Query = " SET NOCOUNT ON;" & vbCrLf & _
            "SELECT COUNT(*) FROM AGREE_BRAND_INCLUDE WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO;"
            Me.ResetCommandText(CommandType.Text, Query)
            Dim totalRowsAgree As Integer = CInt(Me.SqlCom.ExecuteScalar())
            Query = " SET NOCOUNT ON" & vbCrLf & _
            " SELECT COUNT(*) FROM ACHIEVEMENT_HEADER WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO AND FLAG = @FLAG"
            Me.ResetCommandText(CommandType.Text, Query)
            Dim totalRowsAch As Integer = CInt(Me.SqlCom.ExecuteScalar())
            If totalRowsAch <> totalRowsAgree Then
                'hapus achievement
                Me.mustDeletedBeforeInsert = True
                Me.MustReinsertedData = True
            End If
            Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2)
            If Not MustReinsertedData Then 'check apakah ada perubahan actual data antara invoice dan ach header
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT 1 WHERE EXISTS(SELECT * FROM Nufarm.DBO.ACHIEVEMENT_HEADER ACH " & vbCrLf & _
                        " INNER JOIN Tempdb..##T_Agreement_Brand_" & Me.ComputerName & " INV" & vbCrLf & _
                        " ON ACH.BRAND_ID = INV.BRAND_ID AND ACH.DISTRIBUTOR_ID = INV.DISTRIBUTOR_ID " & vbCrLf & _
                        " WHERE RTRIM(LTRIM(ACH.AGREEMENT_NO)) = @AGREEMENT_NO AND ACH.TOTAL_ACTUAL <> CAST((ISNULL(INV.TOTAL_INVOICE,0))AS DECIMAL(18,3)) AND ACH.FLAG = @FLAG) ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                If IsNothing(retval) Or IsDBNull(retval) Then
                    Return False
                End If
                If CInt(retval) > 0 Then
                    MustReinsertedData = True
                Else
                    Return False
                End If
            End If

            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    " SELECT DISTINCT ABI.AGREE_BRAND_ID,(SELECT MAX(IDApp) FROM brnd_AVGPrice WHERE BRAND_ID = ABI.BRAND_ID AND START_DATE >= AA.START_DATE AND END_DATE <= AA.END_DATE) AS AvgPriceID   " & vbCrLf & _
                    " FROM AGREE_AGREEMENT AA INNER JOIN AGREE_BRAND_INCLUDE ABI ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                    " WHERE RTRIM(LTRIM(AA.AGREEMENT_NO)) = @AGREEMENT_NO ;"
            Me.ResetCommandText(CommandType.Text, Query)
            tblAVGPrice = New DataTable("AVGPrice")
            setDataAdapter(Me.SqlCom).Fill(tblAVGPrice)
            Dim DVAPrice As DataView = tblAVGPrice.DefaultView()

            DVAPrice.Sort = "AGREE_BRAND_ID"
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT DA.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID,ABI.BRAND_ID,ABI.TARGET_" & strFlag & ", ABI.TARGET_" & strFlag.Remove(3, 1) & "_FM" & Flag.Remove(0, 1) & ", ABI.TARGET_" & strFlag.Remove(3, 1) & "_PL" & Flag.Remove(0, 1) & vbCrLf & _
                    " FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRAND_INCLUDE ABI ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                    " WHERE RTRIM(LTRIM(DA.AGREEMENT_NO)) = @AGREEMENT_NO ;"
            'ACH_HEADER_ID, DISTRIBUTOR_ID, AGREEMENT_NO, BRAND_ID,AGREE_BRAND_ID,TARGET, TARGET_FM, TARGET_PL, TARGET_VALUE,FLAG,
            'TOTAL_TARGET, TOTAL_PO, TOTAL_PO_VALUE, BALANCE, TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_ACTUAL, ACH_DISPRO, DISPRO,DESCRIPTIONS
            'CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsNew, IsChanged, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3, 
            'ACTUAL_DIST,PO_DIST,PO_VALUE_DIST,DISC_DIST,CPQ1_DIST,CPQ2_DIST,CPQ3_DIST,CPF1_DIST,CPF2_DIST,PBF3_DIST
            Me.ResetCommandText(CommandType.Text, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read()
                Row = tblAchHeader.NewRow()
                Dim BrandID As String = SqlRe.GetString(2)
                Dim Target As Decimal = SqlRe.GetDecimal(3)
                Dim TargetFM As Decimal = SqlRe.GetDecimal(4)
                Dim TargetPL As Decimal = SqlRe.GetDecimal(5)
                Dim AgreeBrandID As String = SqlRe.GetString(1)
                Dim AchievementID As String = SqlRe.GetString(0) + "|" + AgreementNo + BrandID + "|" + Flag
                Row("ACH_HEADER_ID") = AchievementID
                Row("AGREEMENT_NO") = AgreementNo
                Row("DISTRIBUTOR_ID") = SqlRe.GetString(0)
                Row("AGREE_BRAND_ID") = SqlRe.GetString(1)
                Row("BALANCE") = 0 - Target
                Dim index As Integer = DVAPrice.Find(AgreeBrandID)
                If index <> -1 Then
                    Row("AVGPriceID") = DVAPrice(index)("AvgPriceID")
                End If
                Row("BRAND_ID") = BrandID
                Row("FLAG") = Flag
                Row("TOTAL_TARGET") = Target
                Row("TARGET_FM") = TargetFM
                Row("TARGET_PL") = TargetPL
                Row("ISTARGET_GROUP") = IsTargetGroup
                Row.EndEdit() : tblAchHeader.Rows.Add(Row)
                If Not ListAchievementID.Contains(AchievementID) Then
                    ListAchievementID.Add(AchievementID)
                End If
            End While : Me.SqlRe.Close()
            Me.ClearCommandParameters()

            ''hanya mengambil actual saja, karena actual tidak mengacu pad PKD
            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Total_Qty_Brand_By_Invoice")
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNo, 25)
            Me.AddParameter("@COMPUTERNAME", SqlDbType.VarChar, Me.ComputerName, 100)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            Dim ACH_HEADER_ID As String = ""
            While Me.SqlRe.Read()
                ACH_HEADER_ID = SqlRe.GetString(0) & "|" & AgreementNo & SqlRe.GetString(1) & "|" & Flag
                RowsSelect = tblAchHeader.Select("ACH_HEADER_ID = '" & ACH_HEADER_ID & "'")
                If RowsSelect.Length > 0 Then
                    Row = RowsSelect(0) : Row.BeginEdit()
                    Row("ACTUAL_DIST") = SqlRe.GetDecimal(2)
                    Row.EndEdit()
                End If
            End While : SqlRe.Close()

            Dim tblPOByBrandPack As New DataTable("T_POBrandPack"), TblPOByBrand As New DataTable("T_POBrand")

            ''total PO BY BRAND
            'hanya mengambil total_PO karena TotalPO mengacu ke PKD
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ; SELECT ABI.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID," & vbCrLf & _
                    " ISNULL(SUM(PO.PO_ORIGINAL_QTY),0)AS PO_DIST,ISNULL(SUM(PO.PO_AMOUNT),0) AS PO_VALUE_DIST FROM Nufarm.DBO.VIEW_AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                    " INNER JOIN tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO ON PO.DISTRIBUTOR_ID = ABI.DISTRIBUTOR_ID AND PO.BRANDPACK_ID = ABI.BRANDPACK_ID " & vbCrLf & _
                    " WHERE RTRIM(LTRIM(ABI.AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
                    " AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE " & vbCrLf & _
                    " GROUP BY ABI.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID OPTION(KEEP PLAN);"
            Me.ResetCommandText(CommandType.Text, Query)
            tblPOByBrandPack.Clear()
            setDataAdapter(Me.SqlCom).Fill(TblPOByBrand)
            If TblPOByBrand.Rows.Count > 0 Then
                For i1 As Integer = 0 To TblPOByBrand.Rows.Count - 1
                    ACH_HEADER_ID = TblPOByBrand.Rows(i1)("DISTRIBUTOR_ID").ToString() & "|" & TblPOByBrand.Rows(i1)("AGREE_BRAND_ID").ToString() & "|" & Flag
                    RowsSelect = tblAchHeader.Select("ACH_HEADER_ID = '" & ACH_HEADER_ID & "'")
                    If RowsSelect.Length > 0 Then
                        Row = RowsSelect(0)
                        Row.BeginEdit()
                        Row("PO_DIST") = TblPOByBrand.Rows(i1)("PO_DIST")
                        Row("PO_VALUE_DIST") = TblPOByBrand.Rows(i1)("PO_VALUE_DIST")
                        Row.EndEdit()
                    End If
                Next
            End If
            Me.ClearCommandParameters()

            '--------------------------------------------------------------------------------------------------
            Dim strListAch As String = "IN('"
            For i As Integer = 0 To ListAchievementID.Count - 1
                strListAch &= ListAchievementID(i).ToString() & "'"
                If i < ListAchievementID.Count - 1 Then
                    strListAch &= ",'"
                End If
            Next
            strListAch &= ")"

            ''fill table detail
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    " SELECT ABI.BRANDPACK_ID,ABI.ACH_HEADER_ID,ABI.ACH_DETAIL_ID " & vbCrLf & _
                    " FROM ( " & vbCrLf & _
                    "       SELECT ACH_HEADER_ID = ABI.DISTRIBUTOR_ID + '|' + ABI.AGREE_BRAND_ID + '|" & Flag & "'," & vbCrLf & _
                    "       ABI.BRANDPACK_ID,ABI.DISTRIBUTOR_ID + '|' + ABI.AGREE_BRAND_ID + '|" & Flag & "|' + ABI.BRANDPACK_ID AS ACH_DETAIL_ID " & vbCrLf & _
                    "       FROM VIEW_AGREE_BRANDPACK_INCLUDE ABI WHERE DISTRIBUTOR_ID + '|' + AGREE_BRAND_ID + '|" & Flag & "'" & strListAch & vbCrLf & _
                    "      )ABI "
            Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            'ACH_HEADER_ID,BRANDPACK_ID,ACH_DETAIL_ID,TOTAL_PO,TOTAL_ACTUAL,DISC_QTY,TOTAL_CPQ1,TOTAL_CPQ2,TOTAL_CPQ2,
            'TOTAL_CPQ3,TOTAL_CPF1,TOTAL_CPF2,TOTAL_PBF3,DESCRIPTIONS,CreatedBy,CreatedDate,CreatedDate,ModifiedBy,ModifiedDate,IsNew,IsChanged
            While Me.SqlRe.Read()
                Row = tblAchDetail.NewRow()
                Dim BRANDPACK_ID As String = SqlRe.GetString(0)
                ACH_HEADER_ID = SqlRe.GetString(1)
                Row("ACH_HEADER_ID") = ACH_HEADER_ID
                Row("BRANDPACK_ID") = BRANDPACK_ID
                Row("ACH_DETAIL_ID") = SqlRe.GetString(2)
                Row.EndEdit() : tblAchDetail.Rows.Add(Row)
            End While : SqlRe.Close() : Me.ClearCommandParameters()

            'Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
            '        " SELECT PO.DISTRIBUTOR_ID,tmbp.BRAND_ID_DTS AS BRAND_ID,tmbp.BRANDPACK_ID_DTS AS BRANDPACK_ID,(INV.QTY/ISNULL(PO.SPPB_QTY,0)) * ISNULL(PO.PO_ORIGINAL_QTY,0) AS QTY " & vbCrLf & _
            '        " FROM COMPARE_ITEM tmbp " & vbCrLf & _
            '        " INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON tmbp.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
            '        " INNER JOIN tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO ON PO.BRANDPACK_ID = tmbp.BRANDPACK_ID_DTS " & vbCrLf & _
            '        " AND ((PO.RUN_NUMBER = INV.REFERENCE) OR (PO.PO_REF_NO = INV.PONUMBER) ) " & vbCrLf & _
            '        " WHERE PO.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO ) AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' AND INV.QTY > 0 ; "
            ''Dim DistributorID As String = ListAchievementID(0).Remove(ListAchievementID(0).IndexOf("|"))
            'Me.ResetCommandText(CommandType.Text, Query)
            'Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            'Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
            ''Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
            'Me.SqlCom.ExecuteScalar()

            Dim AchDetailID As String = ""

            'hanya untuk mengambil total actual karena actual tidak mengacu pada PKD
            'Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Total_Qty_BrandPack_By_Invoice")
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                    " SELECT DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID,ISNULL(SUM(QTY),0) AS TOTAL_INVOICE " & vbCrLf & _
                    " FROM(" & vbCrLf & _
                    " SELECT PO.DISTRIBUTOR_ID,PO.BRAND_ID,PO.BRANDPACK_ID,(ISNULL(INVOICE.QTY,0)/ISNULL(PO.SPPB_QTY,0)) * PO.PO_ORIGINAL_QTY AS QTY" & vbCrLf & _
                    " FROM tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO " & vbCrLf & _
                    " INNER JOIN (SELECT INV.PONUMBER,INV.REFERENCE,Tmbp.BRANDPACK_ID_DTS AS BRANDPACK_ID,INV.QTY,INV.INV_AMOUNT " & vbCrLf & _
                    "	          FROM ##T_SELECT_INVOICE_" & Me.ComputerName & " INV INNER JOIN COMPARE_ITEM Tmbp " & vbCrLf & _
                    "	          ON INV.BRANDPACK_ID =  Tmbp.BRANDPACK_ID_ACCPAC AND INV.QTY > 0 " & vbCrLf & _
                    "   	)INVOICE" & vbCrLf & _
                    " ON PO.BRANDPACK_ID = INVOICE.BRANDPACK_ID " & vbCrLf & _
                    " AND ((PO.PO_REF_NO = INVOICE.PONUMBER) Or (PO.RUN_NUMBER = INVOICE.REFERENCE)) " & vbCrLf & _
                    " WHERE PO.DISTRIBUTOR_ID = SOME( SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO )" & vbCrLf & _
                    " AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' " & vbCrLf & _
                    ")INV " & vbCrLf & _
                    " GROUP BY DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
            Me.AddParameter("@COMPUTERNAME", SqlDbType.VarChar, Me.ComputerName, 100)
            Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNo, 25)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read()
                'ACH_HEADER_ID = SqlRe.GetString(0) & "|" & AgreementNo & SqlRe.GetString(1) & "|" & Flag
                ACH_HEADER_ID = SqlRe.GetString(0) & "|" & AgreementNo & SqlRe.GetString(1) & "|" & Flag
                AchDetailID = ACH_HEADER_ID & "|" & SqlRe.GetString(2)
                RowsSelect = tblAchDetail.Select("ACH_DETAIL_ID = '" & AchDetailID & "'")
                If RowsSelect.Length > 0 Then
                    Row = RowsSelect(0)
                    Row.BeginEdit()
                    Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(3)
                    Row.EndEdit()
                End If
            End While : SqlRe.Close()

            ''total PO BY BRANDPACK_ID
            'hanya mengambil total_PO karena TotalPO mengacu ke PKD
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ; SELECT ABI.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID,ABI.BRANDPACK_ID," & vbCrLf & _
                    " ISNULL(SUM(PO.PO_ORIGINAL_QTY),0)AS TOTAL_PO_ORIGINAL FROM Nufarm.DBO.VIEW_AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                    " INNER JOIN tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO ON PO.DISTRIBUTOR_ID = ABI.DISTRIBUTOR_ID AND PO.BRANDPACK_ID = ABI.BRANDPACK_ID " & vbCrLf & _
                    " WHERE RTRIM(LTRIM(ABI.AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
                    " AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS'" & vbCrLf & _
                    " GROUP BY ABI.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID,ABI.BRANDPACK_ID OPTION(KEEP PLAN);"
            Me.ResetCommandText(CommandType.Text, Query)
            tblPOByBrandPack.Clear()
            setDataAdapter(Me.SqlCom).Fill(tblPOByBrandPack)
            If tblPOByBrandPack.Rows.Count > 0 Then
                For i1 As Integer = 0 To tblPOByBrandPack.Rows.Count - 1
                    ACH_HEADER_ID = tblPOByBrandPack.Rows(i1)("DISTRIBUTOR_ID").ToString() & "|" & tblPOByBrandPack.Rows(i1)("AGREE_BRAND_ID").ToString() & "|" & Flag
                    Dim BrandPackID As String = tblPOByBrandPack.Rows(i1)("BRANDPACK_ID").ToString()
                    AchDetailID = ACH_HEADER_ID & "|" & BrandPackID
                    RowsSelect = tblAchDetail.Select("ACH_DETAIL_ID = '" & AchDetailID & "'")
                    If RowsSelect.Length > 0 Then
                        Row = RowsSelect(0)
                        Row.BeginEdit()
                        Row("TOTAL_PO") = tblPOByBrandPack.Rows(i1)("TOTAL_PO_ORIGINAL")
                        ' Row("TOTAL_PO_AMOUNT") = tblPO.Rows(i1)("TOTAL_PO_AMOUNT")
                        Row.EndEdit()
                    End If
                Next
            End If

            Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                       "DECLARE @V_START_DATE SMALLDATETIME ;" & vbCrLf & _
                       "SET @V_START_DATE = (SELECT START_DATE FROM AGREE_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO); " & vbCrLf & _
                       "SELECT TOP 1 AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE,AA.QS_TREATMENT_FLAG FROM AGREE_AGREEMENT AA INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                       " WHERE AA.END_DATE < @V_START_DATE AND DATEDIFF(MONTH,AA.START_DATE,AA.END_DATE) = 11 " & vbCrLf & _
                       " AND DA.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO) ORDER BY AA.START_DATE DESC ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Dim tblStartDate As New DataTable("T_StartDate1")
            ''SET privouse Agreement   
            tblStartDate.Clear() : Me.setDataAdapter(Me.SqlCom).Fill(tblStartDate)
            If tblStartDate.Rows.Count > 0 Then
                PrevAgreementNo = tblStartDate.Rows(0)("AGREEMENT_NO")
            End If
            'TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_ACTUAL, ACH_DISPRO, DISPRO,
            'CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsNew, IsChanged, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3, GPPBF3, GPCPQ1

            'TOTAL_CPQ1,TOTAL_CPQ2,TOTAL_CPQ2,
            'TOTAL_CPQ3,TOTAL_CPF1,TOTAL_CPF2,TOTAL_PBF3

            Dim CPF1 As Object = Nothing, CPEF1 As Object = Nothing, CPF2 As Object = Nothing, CPEF2 As Object = Nothing, CPQ2 As Object = Nothing, CPQ3 As Object = Nothing, _
            CPEQ2 As Object = Nothing, CPEQ3 As Object = Nothing, CPQ1 As Object = Nothing, CPEQ1 As Object = Nothing
            Select Case Flag
                Case "F2"
                    CPEF1 = StartDate.AddDays(-1)
                    CPF1 = Convert.ToDateTime(CPEF1).AddMonths(-4).AddDays(1)
                    'strFlag
                Case "F3"

                    CPEF2 = StartDate.AddDays(-1)
                    CPF2 = Convert.ToDateTime(CPEF2).AddMonths(-4).AddDays(1)
                    CPEF1 = Convert.ToDateTime(CPF2).AddDays(-1)
                    CPF1 = Convert.ToDateTime(CPEF1).AddMonths(-4).AddDays(1)

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
                                 "       	    SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
                                 "     		)" & vbCrLf & _
                                 "       AND PO.PO_REF_DATE >= @START_DATE AND PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' " & vbCrLf & _
                                 "      )INV1   " & vbCrLf & _
                                 " GROUP BY DISTRIBUTOR_ID,BRAND_ID OPTION(KEEP PLAN); "

            '---------------------QUERY UNTUK MENTOTAL KAN TOTAL_invoice BRANDPACK DIANTARA START_DATE AND END_DATE PO grouped by BRANDPACK---------------------------
            Dim Query2 As String = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                                   " SELECT DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID,ISNULL(SUM(QTY),0) AS TOTAL_INVOICE  " & vbCrLf & _
                                   "  FROM( " & vbCrLf & _
                                   "       SELECT PO.DISTRIBUTOR_ID,PO.BRAND_ID,PO.BRANDPACK_ID,(ISNULL(INV.QTY,0)/PO.SPPB_QTY)* PO.PO_ORIGINAL_QTY AS QTY " & vbCrLf & _
                                   "       FROM tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO " & vbCrLf & _
                                   "       INNER JOIN COMPARE_ITEM Tmbp ON PO.BRANDPACK_ID = Tmbp.BRANDPACK_ID_DTS " & vbCrLf & _
                                   "       INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON Tmbp.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
                                   "       AND ((PO.RUN_NUMBER = INV.REFERENCE) OR (PO.PO_REF_NO = INV.PONUMBER)) " & vbCrLf & _
                                   "            WHERE PO.DISTRIBUTOR_ID = SOME( " & vbCrLf & _
                                   "       	    SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE RTRIM(LTRIM(AGREEMENT_NO)) = @AGREEMENT_NO " & vbCrLf & _
                                   "     		)" & vbCrLf & _
                                   "       AND PO.PO_REF_DATE >= @START_DATE AND PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' " & vbCrLf & _
                                   "      )INV1   " & vbCrLf & _
                                   " GROUP BY DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID OPTION(KEEP PLAN); "
            Me.ResetCommandText(CommandType.Text, Query1)
            Select Case Flag
                Case "F1"
                    If tblStartDate.Rows.Count > 0 Then
                        Dim PBStartDate As Object = Nothing
                        Dim PBEndDate As Object = Nothing
                        Dim PBFlag As String = tblStartDate.Rows(0)("QS_TREATMENT_FLAG").ToString()
                        PBStartDate = Convert.ToDateTime(tblStartDate.Rows(0)("START_DATE"))
                        PBEndDate = Convert.ToDateTime(tblStartDate.Rows(0)("END_DATE"))
                        'PBFlag = tblStartDate.Rows(0)("QS_TREATMENT_FLAG").ToString()
                        If PBFlag = "S" Then
                            Dim PBS2 As Object = Convert.ToDateTime(PBStartDate).AddMonths(-6).AddDays(-1) 'tambah 2 bulan lagi karena ada transisi
                            Dim PBSES2 = PBEndDate
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBS2)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBSES2))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "PBS2_DIST")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_PBS2")
                                End If
                            End If
                        ElseIf PBFlag = "F" Then
                            Dim PBF1 = PBStartDate
                            Dim PBEF1 = Convert.ToDateTime(PBF1).AddMonths(4).AddDays(-1)
                            Dim PBF2 = Convert.ToDateTime(PBEF1).AddDays(1)
                            Dim FBEF2 = Convert.ToDateTime(PBF2).AddMonths(4).AddDays(-1)
                            Dim PBF3 As Object = Convert.ToDateTime(FBEF2).AddDays(1)
                            Dim PBEF3 As Object = PBEndDate
                            'PBF2
                            Me.ResetCommandText(CommandType.Text, Query1)
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBF2)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(FBEF2))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "PBF2_DIST")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_PBF2")
                                End If
                            End If
                            'PBF3
                            Me.ResetCommandText(CommandType.Text, Query1)
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBF3)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBEF3))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "PBF3_DIST")
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
                    If tblStartDate.Rows.Count > 0 Then
                        Dim PBStartDate As Object = Nothing
                        Dim PBEndDate As Object = Nothing
                        PBStartDate = Convert.ToDateTime(tblStartDate.Rows(0)("START_DATE"))
                        PBEndDate = Convert.ToDateTime(tblStartDate.Rows(0)("END_DATE"))
                        Dim PBFlag As String = tblStartDate.Rows(0)("QS_TREATMENT_FLAG").ToString()
                        If PBFlag = "S" Then
                            Dim PBS2 As Object = Convert.ToDateTime(PBEndDate).AddMonths(-6).AddDays(-1) 'ambil cuma 6 bulan startdate hanya agustus dan enddate po sampai 

                            Dim PBSES2 = PBEndDate
                            'If NufarmBussinesRules.SharedClass.ServerDate.Year = 2021 Then 'ambil masa transisi 2 bulan dan F1 (4 bulan) total jadi 6 bulan
                            '    PBSES2 = Convert.ToDateTime(PBEndDate).AddMonths(2).AddDays(-1) 'tambah 2 bulan lagi karena ada transisi
                            'End If
                            'PBS2
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBS2)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBSES2))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "PBS2_DIST")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_PBS2")
                                End If
                            End If
                        ElseIf PBFlag = "F" Then
                            Dim PBF1 = PBStartDate
                            Dim PBEF1 = Convert.ToDateTime(PBF1).AddMonths(4).AddDays(-1)
                            Dim PBF2 = Convert.ToDateTime(PBEF1).AddDays(1)
                            Dim FBEF2 = Convert.ToDateTime(PBF2).AddMonths(4).AddDays(-1)
                            Dim PBF3 As Object = Convert.ToDateTime(FBEF2).AddDays(1)
                            Dim PBEF3 As Object = PBEndDate
                            'PBF3
                            Me.ResetCommandText(CommandType.Text, Query1)
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBF3)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBEF3))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "PBF3_DIST")
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
                    If Not IsNothing(CPF1) Then
                        Me.ResetCommandText(CommandType.Text, Query1)
                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPF1)
                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPEF1))
                        tblTemp = New DataTable("T_TEMP")
                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                        If tblTemp.Rows.Count > 0 Then
                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "CPF1_DIST")
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
                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "CPF2_DIST")
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
                        Me.ResetCommandText(CommandType.Text, Query1)
                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPF1)
                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPEF1))
                        tblTemp = New DataTable("T_TEMP")
                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                        If tblTemp.Rows.Count > 0 Then
                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "CPF1_DIST")
                            Me.ResetCommandText(CommandType.Text, Query2)
                            tblTemp = New DataTable("T_TEMP")
                            tblTemp.Clear()
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_CPF1")
                            End If
                        End If
                    End If

            End Select
            'AMBIL DATA POTENSI BERDASARKAN AGREEMENTNO,DENGAN COLUM DISTRIBUTOR_ID,BRAND_ID,
            Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                    "SELECT GP.IDApp,ACHIEVEMENT_ID = DA.DISTRIBUTOR_ID + '|' + ABI.AGREE_BRAND_ID + '|' + @FLAG,GP.CPF1,GP.CPF2,GP.PBF2,GP.PBF3,GP.PBS2 " & vbCrLf & _
                    " FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRAND_INCLUDE ABI ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                    " INNER JOIN GIVEN_PROGRESSIVE GP ON GP.AGREE_BRAND_ID = ABI.AGREE_BRAND_ID WHERE RTRIM(LTRIM(ABI.AGREEMENT_NO)) = @AGREEMENT_NO ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag)
            tblGP.Clear()
            setDataAdapter(Me.SqlCom).Fill(tblGP)
            If tblGP.Rows.Count > 0 Then
                Dim rows() As DataRow = Nothing
                For i As Integer = 0 To tblGP.Rows.Count - 1
                    'rows = tblAchHeader.Select("ACHIEVEMENT_ID = '" & tblGP.Rows(i)("DISTRIBUTOR_ID").ToString() & "|" & tblGP.Rows(i)("AGREE_BRAND_ID").ToString() & "|" & Flag & "'")
                    rows = tblAchHeader.Select("ACH_HEADER_ID = '" & tblGP.Rows(i)("ACHIEVEMENT_ID") & "'")
                    If rows.Length > 0 Then
                        rows(0).BeginEdit()
                        rows(0)("GP_ID") = tblGP.Rows(i)("IDApp")
                        If IsDBNull(tblGP.Rows(i)("PBS2")) Or IsNothing(tblGP.Rows(i)("PBS2")) Then
                            rows(0)("GPPBS2") = 0
                        Else
                            rows(0)("GPPBS2") = Convert.ToDecimal(tblGP.Rows(i)("PBS2"))
                        End If
                        If IsDBNull(tblGP.Rows(i)("CPF1")) Or IsNothing(tblGP.Rows(i)("CPF1")) Then
                            rows(0)("GPCPF1") = 0
                        Else
                            rows(0)("GPCPF1") = Convert.ToDecimal(tblGP.Rows(i)("CPF1"))
                        End If

                        If IsDBNull(tblGP.Rows(i)("CPF2")) Or IsNothing(tblGP.Rows(i)("CPF2")) Then
                            rows(0)("GPCPF2") = 0
                        Else
                            rows(0)("GPCPF2") = Convert.ToDecimal(tblGP.Rows(i)("CPF2"))
                        End If

                        If IsDBNull(tblGP.Rows(i)("PBF3")) Or IsNothing(tblGP.Rows(i)("PBF3")) Then
                            rows(0)("GPPBF3") = 0
                        Else
                            rows(0)("GPPBF3") = Convert.ToDecimal(tblGP.Rows(i)("PBF3"))
                        End If

                        If IsDBNull(tblGP.Rows(i)("PBF2")) Or IsNothing(tblGP.Rows(i)("PBF2")) Then
                            rows(0)("GPPBF2") = 0
                        Else
                            rows(0)("GPPBF2") = Convert.ToDecimal(tblGP.Rows(i)("PBF2"))
                        End If
                        rows(0).EndEdit()
                        rows(0).AcceptChanges()
                    End If
                Next
            End If
            tblAchHeader.AcceptChanges()
            Return True
        End Function
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
        Private Sub DropTempTable()

            '---------------UNCOMENT THIS AFTER DEBUGGIN -----------------------------
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
            Me.OpenConnection()
            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
        End Sub
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overloads Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
                Me.DropTempTable()
                Dispose(True)
                GC.SuppressFinalize(Me)
            End If
            Me.disposedValue = True
        End Sub
    End Class

End Namespace

