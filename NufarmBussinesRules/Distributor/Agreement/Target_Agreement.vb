Imports System.Data
Imports System.Data.SqlClient
Imports NufarmBussinesRules
Imports NufarmBussinesRules.SharedClass
Namespace DistributorAgreement
    Public Class Target_Agreement
        Inherits NufarmBussinesRules.DistributorAgreement.Agreement
        Protected Query As String = ""
        'Dim isTransitionTime = NufarmBussinesRules.SharedClass.ServerDate <= New DateTime(2020, 7, 31) And NufarmBussinesRules.SharedClass.ServerDate >= New DateTime(2019, 8, 1)
        Public Sub New()
            MyBase.New()
        End Sub
        Public Enum ShowAchievementFrom
            LoadData
            AfterGenerated
            Processing
        End Enum
        Public mustRecomputeYear As Boolean = False
        Public MessageError As String = ""
        Private StarDateDiscOA As DateTime
        Private strStartDateFlag As String = "", strEndDateFlag As String = ""
        ''' <summary>
        ''' prepare startDate and end_date processing
        ''' </summary>
        ''' <param name="StartDate">datetime data</param>
        ''' <param name="EndDate">datetime data</param>
        ''' <param name="StrDecStartDate">varchar in server = numeric</param>
        ''' <param name="strDecEndDate">varchar in server = numeric</param>
        ''' <remarks></remarks>
        Protected Sub CreateTempTable(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal StrDecStartDate As String, ByVal strDecEndDate As String)
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON;" & vbCrLf & _
                   "IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                   " BEGIN " & vbCrLf & _
                   "SELECT START_DATE = @START_DATE,END_DATE = @END_DATE,UserName = @UserName INTO  ##T_START_DATE_" & Me.ComputerName & " ;" & vbCrLf & _
                   " END "
            If Not IsNothing(Me.SqlCom) Then
                Me.ResetCommandText(CommandType.Text, Query)
                Me.SqlCom.Connection = Me.GetConnection() : Else : Me.CreateCommandSql("", Query)
            End If
            Dim StrStartDate As String = "", strEndDate As String = ""
            StrStartDate = Month(StartDate).ToString() + "/" + Day(StartDate).ToString() + "/" + Year(StartDate).ToString()
            strEndDate = Month(EndDate).ToString() + "/" + Day(EndDate).ToString() + "/" + Year(EndDate).ToString()

            Me.AddParameter("@START_DATE", SqlDbType.VarChar, StrStartDate, 20)
            Me.AddParameter("@END_DATE", SqlDbType.VarChar, strEndDate, 20)
            Me.AddParameter("@UserName", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)
            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            Dim retvalStartDate As String = "", retvalEndDate As String = "", retvalUserName As String = ""
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ;" & vbCrLf & _
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
                        " BEGIN  EXEC " & StoredProcToUse & " @DEC_START_DATE = @D_START_DATE,@DEC_END_DATE = @D_END_DATE,@COMPUTERNAME = @C_NAME; END " '& vbCrLf & _

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
        Public Function IsCanGenerate(ByVal Flag As String, ByVal ListAgreement As List(Of String), ByRef Message As String)
            Dim B As Boolean = True
            Try
                Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                                            StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                                           StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                                           StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing
                Me.OpenConnection() : If IsNothing(Me.SqlCom) Then : Me.SqlCom = New SqlCommand() : Me.SqlCom.Connection = Me.SqlConn : End If

                For i As Integer = 0 To ListAgreement.Count - 1
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT START_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO AND QS_TREATMENT_FLAG = @FLAG OPTION(KEEP PLAN);"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, ListAgreement(i), 32)
                    Select Case Flag
                        Case "Q1"
                            Me.AddParameter("@FLAG", SqlDbType.Char, "Q", 1)
                            StartDate = Convert.ToDateTime(Me.SqlCom.ExecuteScalar())
                            'While Me.SqlRe.Read() : StartDate = SqlRe.GetDateTime(0) : End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                            EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
                            If NufarmBussinesRules.SharedClass.ServerDate <= EndDateQ1 Then : B = False : Message &= ListAgreement(i) & vbCrLf : End If
                        Case "Q2"
                            Me.AddParameter("@FLAG", SqlDbType.Char, "Q", 1)
                            StartDate = Convert.ToDateTime(Me.SqlCom.ExecuteScalar())
                            'While Me.SqlRe.Read() : StartDate = SqlRe.GetDateTime(0) : End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                            EndDateQ2 = StartDate.AddMonths(6).AddDays(-1)
                            If NufarmBussinesRules.SharedClass.ServerDate <= EndDateQ2 Then : B = False : Message &= ListAgreement(i) & vbCrLf : End If
                        Case "Q3"
                            Me.AddParameter("@FLAG", SqlDbType.Char, "Q", 1)
                            StartDate = Convert.ToDateTime(Me.SqlCom.ExecuteScalar())
                            'While Me.SqlRe.Read() : StartDate = SqlRe.GetDateTime(0) : End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                            EndDateQ3 = StartDate.AddMonths(9).AddDays(-1)
                            If NufarmBussinesRules.SharedClass.ServerDate <= EndDateQ3 Then : B = False : Message &= ListAgreement(i) & vbCrLf : End If
                        Case "Q4"
                            Me.AddParameter("@FLAG", SqlDbType.Char, "Q", 1)
                            StartDate = Convert.ToDateTime(Me.SqlCom.ExecuteScalar())
                            'While Me.SqlRe.Read() : StartDate = SqlRe.GetDateTime(0) : End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                            EndDateQ4 = StartDate.AddMonths(12).AddDays(-1)
                            If NufarmBussinesRules.SharedClass.ServerDate <= EndDateQ4 Then : B = False : Message &= ListAgreement(i) & vbCrLf : End If
                        Case "S1"
                            Me.AddParameter("@FLAG", SqlDbType.Char, "S", 1)
                            StartDate = Convert.ToDateTime(Me.SqlCom.ExecuteScalar())
                            'While Me.SqlRe.Read() : StartDate = SqlRe.GetDateTime(0) : End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                            EndDateS1 = StartDate.AddMonths(6).AddDays(-1)
                            If NufarmBussinesRules.SharedClass.ServerDate <= EndDateS1 Then : B = False : Message &= ListAgreement(i) & vbCrLf : End If
                        Case "S2"
                            Me.AddParameter("@FLAG", SqlDbType.Char, "S", 1)
                            StartDate = Convert.ToDateTime(Me.SqlCom.ExecuteScalar())
                            'While Me.SqlRe.Read() : StartDate = SqlRe.GetDateTime(0) : End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                            EndDateS2 = StartDate.AddMonths(12).AddDays(-1)
                            If NufarmBussinesRules.SharedClass.ServerDate <= EndDateS2 Then : B = False : Message &= ListAgreement(i) & vbCrLf : End If
                        Case "Y"
                            Me.ClearCommandParameters()
                            Query = "SET NOCOUNT ON;" & vbCrLf & _
                                     "SELECT END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO OPTION(KEEP PLAN);"
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, ListAgreement(i), 32)
                            EndDate = Convert.ToDateTime(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                            If NufarmBussinesRules.SharedClass.ServerDate <= EndDate Then : B = False : Message &= ListAgreement(i) & vbCrLf : End If
                    End Select
                Next
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return B
        End Function
        Private tblAcrHeaderYear As DataTable = Nothing
        Private tblAcrDetailYear As DataTable = Nothing
        Private Sub CreateOrReCreateTblPeriodeBeforeYearHeader(ByVal AGREEMENT_NO As String, ByVal MustClearParameters As Boolean, ByVal mustCloseConnection As Boolean)
            Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT ACHIEVEMENT_ID,DISTRIBUTOR_ID,AGREEMENT_NO,BRAND_ID,AGREEMENT_NO + '' + BRAND_ID AS AGREE_BRAND_ID,DISTRIBUTOR_ID + AGREEMENT_NO + BRAND_ID AS [ROW_ID],FLAG,TOTAL_PBQ3,TOTAL_PBQ4,TOTAL_PBS2,TOTAL_PBQ3 + TOTAL_PBQ4 + TOTAL_PBS2 AS TOTAL_PB," & vbCrLf & _
                    "TOTAL_JOIN_PBQ3,TOTAL_JOIN_PBQ4,TOTAL_JOIN_PBS2," & vbCrLf & _
                    "TOTAL_PBY,TOTAL_JOIN_PBY FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 50)
            Me.tblAcrHeaderYear = New DataTable("T_AcrHeaderYear")
            Me.tblAcrHeaderYear.Clear()
            setDataAdapter(Me.SqlCom).Fill(tblAcrHeaderYear)
            If (MustClearParameters) Then : Me.ClearCommandParameters() : End If
            If (mustCloseConnection) Then : Me.CloseConnection() : End If
        End Sub
        Private Sub CreateOrReCreateTblPeriodeBeforeYearDetail(ByVal AGREEMENT_NO As String, ByVal MustClearParameters As Boolean, ByVal mustCloseConnection As Boolean)
            Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT ACH.ACHIEVEMENT_ID,ACH.DISTRIBUTOR_ID,ACH.AGREEMENT_NO,ACH.BRAND_ID,ACH.DISTRIBUTOR_ID + '' + ACH.AGREEMENT_NO + '' + ACH.BRAND_ID  + '' + ACD.BRANDPACK_ID AS [ROW_ID],ACH.FLAG,ACD.BRANDPACK_ID,ACD.ACHIEVEMENT_BRANDPACK_ID,ACD.TOTAL_PBQ3,ACD.TOTAL_PBQ4,ACD.TOTAL_PBS2,ACD.TOTAL_PBY " & vbCrLf & _
                    "FROM ACCRUED_DETAIL ACD INNER JOIN ACCRUED_HEADER ACH ON ACH.ACHIEVEMENT_ID = ACD.ACHIEVEMENT_ID WHERE ACH.AGREEMENT_NO = @AGREEMENT_NO ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 50)
            tblAcrDetailYear = New DataTable("T_AcrDetailYear")
            tblAcrDetailYear.Clear()
            setDataAdapter(Me.SqlCom).Fill(tblAcrDetailYear)
            If (MustClearParameters) Then : Me.ClearCommandParameters() : End If
            If (mustCloseConnection) Then : Me.CloseConnection() : End If
        End Sub
        Private Sub CreateOrRecreatTblAcrHeader(ByRef tblAcrHeader As DataTable)
            'Dim tblAcrHeader As New DataTable("T_AccrHeader") : tblAcrHeader.Clear()
            If (tblAcrHeader.Columns.Count > 0) Then
                tblAcrHeader.Rows.Clear() : tblAcrHeader.AcceptChanges() : Return
            End If
            If IsNothing(tblAcrHeader) Then
                tblAcrHeader = New DataTable("T_AcrHeader")
            End If : tblAcrHeader.Clear()
            With tblAcrHeader.Columns
                .Add(New DataColumn("ACHIEVEMENT_ID", Type.GetType("System.String")))
                .Add(New DataColumn("AGREEMENT_NO", Type.GetType("System.String")))
                .Add(New DataColumn("DISTRIBUTOR_ID", Type.GetType("System.String")))
                .Add(New DataColumn("AGREE_BRAND_ID", Type.GetType("System.String")))
                .Add(New DataColumn("BRAND_ID", Type.GetType("System.String")))
                .Add(New DataColumn("FLAG", Type.GetType("System.String")))
                .Add(New DataColumn("ISTARGET_GROUP", Type.GetType("System.Boolean")))
                .Item("ISTARGET_GROUP").DefaultValue = False
                .Add(New DataColumn("TARGET", Type.GetType("System.Decimal")))
                .Item("TARGET").DefaultValue = 0
                .Add(New DataColumn("DISPRO", Type.GetType("System.Decimal")))
                .Item("DISPRO").DefaultValue = 0

                .Add(New DataColumn("GP_ID", Type.GetType("System.Int64")))
                .Item("GP_ID").DefaultValue = 0

                .Add(New DataColumn("TOTAL_ACTUAL", Type.GetType("System.Decimal")))
                .Item("TOTAL_ACTUAL").DefaultValue = 0

                .Add(New DataColumn("ACHIEVEMENT_DISPRO", Type.GetType("System.Decimal")))
                .Item("ACHIEVEMENT_DISPRO").DefaultValue = 0

                .Add(New DataColumn("BONUS_QTY", Type.GetType("System.Decimal")))
                .Item("BONUS_QTY").DefaultValue = 0

                .Add(New DataColumn("ACTUAL_DISTRIBUTOR", Type.GetType("System.Decimal")))
                .Item("ACTUAL_DISTRIBUTOR").DefaultValue = 0

                .Add(New DataColumn("BONUS_DISTRIBUTOR", Type.GetType("System.String")))
                .Item("BONUS_DISTRIBUTOR").DefaultValue = 0

                .Add(New DataColumn("DISC_OBTAINED_FROM", Type.GetType("System.String")))
                .Item("DISC_OBTAINED_FROM").DefaultValue = "T1"

                .Add(New DataColumn("TOTAL_PO_DISTRIBUTOR", Type.GetType("System.Decimal")))
                .Item("TOTAL_PO_DISTRIBUTOR").DefaultValue = 0

                .Add(New DataColumn("CombinedWith", Type.GetType("System.String")))

                .Add(New DataColumn("TOTAL_PO_ORIGINAL", Type.GetType("System.Decimal")))
                .Item("TOTAL_PO_ORIGINAL").DefaultValue = 0

                .Add(New DataColumn("TOTAL_PBQ3", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBQ3").DefaultValue = 0
                .Add(New DataColumn("TOTAL_JOIN_PBQ3", Type.GetType("System.Decimal")))
                .Item("TOTAL_JOIN_PBQ3").DefaultValue = 0

                .Add(New DataColumn("TOTAL_PBQ4", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBQ4").DefaultValue = 0
                .Add(New DataColumn("TOTAL_JOIN_PBQ4", Type.GetType("System.Decimal")))
                .Item("TOTAL_JOIN_PBQ4").DefaultValue = 0

                .Add(New DataColumn("TOTAL_PBS2", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBS2").DefaultValue = 0
                .Add(New DataColumn("TOTAL_JOIN_PBS2", Type.GetType("System.Decimal")))
                .Item("TOTAL_JOIN_PBS2").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPQ1", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ1").DefaultValue = 0
                .Add(New DataColumn("TOTAL_JOIN_CPQ1", Type.GetType("System.Decimal")))
                .Item("TOTAL_JOIN_CPQ1").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPQ2", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ2").DefaultValue = 0
                .Add(New DataColumn("TOTAL_JOIN_CPQ2", Type.GetType("System.Decimal")))
                .Item("TOTAL_JOIN_CPQ2").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPQ3", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ3").DefaultValue = 0
                .Add(New DataColumn("TOTAL_JOIN_CPQ3", Type.GetType("System.Decimal")))
                .Item("TOTAL_JOIN_CPQ3").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPS1", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPS1").DefaultValue = 0
                .Add(New DataColumn("TOTAL_JOIN_CPS1", Type.GetType("System.Decimal")))
                .Item("TOTAL_JOIN_CPS1").DefaultValue = 0


                .Add(New DataColumn("TOTAL_PBY", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBY").DefaultValue = 0
                .Add(New DataColumn("TOTAL_JOIN_PBY", Type.GetType("System.Decimal")))
                .Item("TOTAL_JOIN_PBY").DefaultValue = 0

                .Add(New DataColumn("BALANCE", Type.GetType("System.Decimal")))
                .Item("BALANCE").DefaultValue = 0

                .Add(New DataColumn("DESCRIPTION", Type.GetType("System.String")))
                .Item("DESCRIPTION").DefaultValue = String.Empty

                .Add(New DataColumn("ISCOMBINED_TARGET", Type.GetType("System.Boolean")))
                .Item("ISCOMBINED_TARGET").DefaultValue = False


                .Add(New DataColumn("LAST_UPDATED", Type.GetType("System.DateTime")))
                .Item("LAST_UPDATED").DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()
                .Add(New DataColumn("CREATE_BY", Type.GetType("System.String")))
                .Item("CREATE_BY").DefaultValue = NufarmBussinesRules.User.UserLogin.UserName
                .Add(New DataColumn("CREATE_DATE", Type.GetType("System.DateTime")))
                .Item("CREATE_DATE").DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()
                .Add(New DataColumn("MODIFY_BY", Type.GetType("System.String")))
                .Item("MODIFY_BY").DefaultValue = NufarmBussinesRules.User.UserLogin.UserName
                .Add(New DataColumn("MODIFY_DATE", Type.GetType("System.DateTime")))
                .Item("MODIFY_DATE").DefaultValue = NufarmBussinesRules.SharedClass.ServerDate
                .Add(New DataColumn("IsNew", Type.GetType("System.Boolean")))
                .Item("IsNew").DefaultValue = DBNull.Value
                .Add(New DataColumn("IsChanged", Type.GetType("System.Boolean")))

                .Add(New DataColumn("GPPBQ3", Type.GetType("System.Decimal")))
                .Item("GPPBQ3").DefaultValue = 0

                .Add(New DataColumn("GPPBQ4", Type.GetType("System.Decimal")))
                .Item("GPPBQ4").DefaultValue = 0

                .Add(New DataColumn("GPPBS2", Type.GetType("System.Decimal")))
                .Item("GPPBS2").DefaultValue = 0

                '.Add(New DataColumn("GPPBYear", Type.GetType("System.Decimal")))
                '.Item("GPPBPYear").DefaultValue = 0

                .Add(New DataColumn("GPCPQ1", Type.GetType("System.Decimal")))
                .Item("GPCPQ1").DefaultValue = 0

                .Add(New DataColumn("GPCPQ2", Type.GetType("System.Decimal")))
                .Item("GPCPQ2").DefaultValue = 0

                .Add(New DataColumn("GPCPQ3", Type.GetType("System.Decimal")))
                .Item("GPCPQ3").DefaultValue = 0

                .Add(New DataColumn("GPCPS1", Type.GetType("System.Decimal")))
                .Item("GPCPS1").DefaultValue = 0

                .Add(New DataColumn("GPPBY", Type.GetType("System.Decimal")))
                .Item("GPPBY").DefaultValue = 0

                .Item("IsChanged").DefaultValue = DBNull.Value
                .Add(New DataColumn("IsFromAccHeader", Type.GetType("System.Boolean")))
                .Item("IsFromAccHeader").DefaultValue = DBNull.Value
              
                .Add("DISC_BY_VOLUME", Type.GetType("System.Decimal"))
                .Item("DISC_BY_VOLUME").DefaultValue = 0
                .Add("DISC_DIST_BY_VOLUME", Type.GetType("System.Decimal"))
                .Item("DISC_DIST_BY_VOLUME").DefaultValue = 0
                .Add("AGREE_ACH_BY", Type.GetType("System.String"))
                .Item("AGREE_ACH_BY").DefaultValue = ""

                .Add(New DataColumn("TOTAL_PO_AMOUNT", Type.GetType("System.Decimal")))
                .Item("TOTAL_PO_AMOUNT").DefaultValue = 0

                .Add(New DataColumn("PO_AMOUNT_DISTRIBUTOR", Type.GetType("System.Decimal")))
                .Item("PO_AMOUNT_DISTRIBUTOR").DefaultValue = 0

                .Add(New DataColumn("TOTAL_ACTUAL_AMOUNT", Type.GetType("System.Decimal")))
                .Item("TOTAL_ACTUAL_AMOUNT").DefaultValue = 0

                .Add(New DataColumn("ACTUAL_AMOUNT_DISTRIBUTOR", Type.GetType("System.Decimal")))
                .Item("ACTUAL_AMOUNT_DISTRIBUTOR").DefaultValue = 0

                .Add(New DataColumn("CPQ1_AMOUNT", Type.GetType("System.Decimal")))
                .Item("CPQ1_AMOUNT").DefaultValue = 0
                .Add(New DataColumn("CPQ2_AMOUNT", Type.GetType("System.Decimal")))
                .Item("CPQ2_AMOUNT").DefaultValue = 0
                .Add(New DataColumn("CPQ3_AMOUNT", Type.GetType("System.Decimal")))
                .Item("CPQ3_AMOUNT").DefaultValue = 0
                .Add(New DataColumn("CPS1_AMOUNT", Type.GetType("System.Decimal")))
                .Item("CPS1_AMOUNT").DefaultValue = 0

                .Add(New DataColumn("TARGET_FM", Type.GetType("System.Decimal")))
                .Item("TARGET_FM").DefaultValue = 0
                .Add(New DataColumn("TARGET_PL", Type.GetType("System.Decimal")))
                .Item("TARGET_PL").DefaultValue = 0

            End With
            ''create primary key
            Dim Key(1) As DataColumn : Key(0) = tblAcrHeader.Columns("ACHIEVEMENT_ID")
            tblAcrHeader.PrimaryKey = Key
        End Sub
        Private Sub CreateOrRecreateTblAcrDetail(ByRef tblAcrDetail As DataTable)
            If (tblAcrDetail.Columns.Count > 0) Then
                tblAcrDetail.Rows.Clear() : tblAcrDetail.AcceptChanges() : Return
            End If
            If IsNothing(tblAcrDetail) Then
                tblAcrDetail = New DataTable("T_AcrDetail")
            End If : tblAcrDetail.Clear()
            With tblAcrDetail.Columns
                .Add(New DataColumn("ACHIEVEMENT_ID", Type.GetType("System.String")))
                .Add(New DataColumn("BRANDPACK_ID", Type.GetType("System.String")))
                .Add(New DataColumn("ACHIEVEMENT_BRANDPACK_ID", Type.GetType("System.String")))
                .Add(New DataColumn("TOTAL_ACTUAL", Type.GetType("System.Decimal")))
                .Item("TOTAL_ACTUAL").DefaultValue = 0

                .Add(New DataColumn("TOTAL_ACTUAL_AMOUNT", Type.GetType("System.Decimal")))
                .Item("TOTAL_ACTUAL_AMOUNT").DefaultValue = 0

                .Add(New DataColumn("DISC_QTY", Type.GetType("System.Decimal")))
                .Item("DISC_QTY").DefaultValue = 0
                .Add(New DataColumn("RELEASE_QTY", Type.GetType("System.Decimal")))
                .Item("RELEASE_QTY").DefaultValue = 0
                .Add(New DataColumn("LEFT_QTY", Type.GetType("System.Decimal")))
                .Item("LEFT_QTY").DefaultValue = 0
                .Add(New DataColumn("CAN_RELEASE", Type.GetType("System.Boolean")))
                .Item("CAN_RELEASE").DefaultValue = False
                .Add(New DataColumn("TOTAL_PO_ORIGINAL", Type.GetType("System.Decimal")))
                .Item("TOTAL_PO_ORIGINAL").DefaultValue = 0

                .Add(New DataColumn("TOTAL_PO_AMOUNT", Type.GetType("System.Decimal")))
                .Item("TOTAL_PO_AMOUNT").DefaultValue = 0

                .Add(New DataColumn("TOTAL_PBQ3", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBQ3").DefaultValue = 0
                .Add(New DataColumn("TOTAL_PBQ4", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBQ4").DefaultValue = 0

                .Add(New DataColumn("TOTAL_PBS2", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBS2").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPQ1", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ1").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPQ2", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ2").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPQ3", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ3").DefaultValue = 0
                .Add(New DataColumn("TOTAL_CPS1", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPS1").DefaultValue = 0
                .Add(New DataColumn("TOTAL_PBY", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBY").DefaultValue = 0

                .Add(New DataColumn("CAN_UPDATE", Type.GetType("System.Boolean")))
                .Item("CAN_UPDATE").DefaultValue = False
                .Add(New DataColumn("LAST_UPDATE", Type.GetType("System.DateTime")))
                .Item("LAST_UPDATE").DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()
                .Add(New DataColumn("CREATE_BY", Type.GetType("System.String")))
                .Add(New DataColumn("DESCRIPTIONS", Type.GetType("System.String")))
                .Item("DESCRIPTIONS").DefaultValue = String.Empty
                .Item("CREATE_BY").DefaultValue = NufarmBussinesRules.User.UserLogin.UserName
                .Add(New DataColumn("CREATE_DATE", Type.GetType("System.DateTime")))
                .Item("CREATE_DATE").DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()
                .Add(New DataColumn("MODIFY_BY", Type.GetType("System.String")))
                .Item("MODIFY_BY").DefaultValue = NufarmBussinesRules.User.UserLogin.UserName
                .Add(New DataColumn("MODIFY_DATE", Type.GetType("System.DateTime")))
                .Item("MODIFY_DATE").DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()
                .Add(New DataColumn("IsNew", Type.GetType("System.Boolean")))
                .Item("IsNew").DefaultValue = DBNull.Value
                .Add(New DataColumn("IsChanged", Type.GetType("System.Boolean")))
                .Item("IsChanged").DefaultValue = DBNull.Value
                .Add("DISC_BY_VOLUME", Type.GetType("System.Decimal"))
                .Item("DISC_BY_VOLUME").DefaultValue = 0
                .Add("DISC_BY_VALUE", Type.GetType("System.Decimal"))
                .Item("DISC_BY_VALUE").DefaultValue = 0

                .Add(New DataColumn("DISC_OBTAINED_FROM", Type.GetType("System.String")))
                .Item("DISC_OBTAINED_FROM").DefaultValue = "T1"

                .Add(New DataColumn("CPQ1_AMOUNT", Type.GetType("System.Decimal")))
                .Item("CPQ1_AMOUNT").DefaultValue = 0
                .Add(New DataColumn("CPQ2_AMOUNT", Type.GetType("System.Decimal")))
                .Item("CPQ2_AMOUNT").DefaultValue = 0
                .Add(New DataColumn("CPQ3_AMOUNT", Type.GetType("System.Decimal")))
                .Item("CPQ3_AMOUNT").DefaultValue = 0
                .Add(New DataColumn("CPS1_AMOUNT", Type.GetType("System.Decimal")))
                .Item("CPS1_AMOUNT").DefaultValue = 0
            End With
            Dim Key(1) As DataColumn : Key(0) = tblAcrDetail.Columns("ACHIEVEMENT_BRANDPACK_ID")
            tblAcrDetail.PrimaryKey = Key
        End Sub
        Protected Sub CreateTempInvoiceTable(ByVal Flag As String, ByVal DISTRIBUTOR_ID As String, ByVal ListAGREEMENT_NO As List(Of String), ByVal strAgreementNos As String)
            Dim strDecStartDate As String = "", strDecEndDate As String = ""
            Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                      StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                      StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                      StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing
            'check table temporary
            'jika listAgreement <> nothing
            'ambil max start dan enddate dari agreement yang ada di list agreement
            Dim QSY_FLAG As String = ""
            If Not Flag = "Y" Then
                If Left(Flag, 1) = "Q" Then
                    QSY_FLAG = "Q"
                Else : QSY_FLAG = "S"
                End If
            Else
                QSY_FLAG = "Y"
            End If
            If ((DISTRIBUTOR_ID <> "") And (Not IsNothing(ListAGREEMENT_NO))) Then
                If ListAGREEMENT_NO.Count > 0 Then
                    Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                             "SELECT MIN(AA.START_DATE),MAX(AA.END_DATE) FROM AGREE_AGREEMENT AA INNER JOIN ACCRUED_HEADER ACH ON ACH.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                             " WHERE AA.AGREEMENT_NO  " & strAgreementNos & " AND ACH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ACH.FLAG = @FLAG ;"
                ElseIf DISTRIBUTOR_ID <> "" Then
                    Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT MIN(AA.START_DATE),MAX(AA.END_DATE) FROM AGREE_AGREEMENT AA INNER JOIN ACCRUED_HEADER ACH ON ACH.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                            " WHERE ACH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 1  AND ACH.FLAG = @FLAG ;"
                End If
            ElseIf Not IsNothing(ListAGREEMENT_NO) Then
                If ListAGREEMENT_NO.Count > 0 Then
                    Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT MIN(AA.START_DATE),MAX(AA.END_DATE) FROM AGREE_AGREEMENT AA WHERE AA.AGREEMENT_NO  " & strAgreementNos
                Else
                    Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT MIN(AA.START_DATE),MAX(AA.END_DATE) FROM AGREE_AGREEMENT AA INNER JOIN ACCRUED_HEADER ACH ON ACH.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                            " AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 1 AND ACH.FLAG = @FLAG ;"
                End If

            ElseIf (DISTRIBUTOR_ID <> "") Then
                Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT MIN(AA.START_DATE),MAX(AA.END_DATE) FROM AGREE_AGREEMENT AA INNER JOIN ACCRUED_HEADER ACH ON AA.AGREEMENT_NO = ACH.AGREEMENT_NO " & vbCrLf & _
                        " WHERE ACH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ACH.FLAG = @FLAG AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 1 ; "
            Else 'cuma flag sajayang di ambil....
                'ambil max startdate dan max endate dari agreement yang ada di accrued header dengan year agreement
                Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                " SELECT MIN(AA.START_DATE),MAX(AA.END_DATE) FROM AGREE_AGREEMENT AA INNER JOIN ACCRUED_HEADER ACH ON ACH.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                " AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 1 AND ACH.FLAG = @FLAG ;"
            End If
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
            Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2)
            If DISTRIBUTOR_ID <> "" Then
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
            End If
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            Dim BStartDate As Boolean = False
            While Me.SqlRe.Read()
                If Not IsNothing(Me.SqlRe(0)) And Not IsDBNull(Me.SqlRe(0)) Then
                    StartDate = SqlRe.GetDateTime(0)
                    EndDate = SqlRe.GetDateTime(1)
                    BStartDate = True
                End If
            End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
            If Not BStartDate Then
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw New Exception("Can not found startDate/EndDate agreement" & vbCrLf & "Achievement has not probably been computed yet.")
            End If
            StartDateQ1 = StartDate : StartDateS1 = StartDate : EndDateS2 = EndDate : EndDateQ4 = EndDate
            If Flag <> "Y" Then
                If Left(Flag, 1) = "Q" Then
                    EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
                    StartDateQ2 = EndDateQ1.AddDays(1)
                    EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
                    StartDateQ3 = EndDateQ2.AddDays(1)
                    EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
                    StartDateQ4 = EndDateQ3.AddDays(1)
                    If (EndDateQ4 > StartDateQ4.AddMonths(3).AddDays(1)) Then
                        EndDateQ4 = StartDateQ4.AddMonths(3).AddDays(1)
                    ElseIf EndDateQ4 < StartDateQ4.AddMonths(3).AddDays(-2) Then
                        EndDateQ4 = StartDateQ4.AddMonths(3).AddDays(1)
                    ElseIf IsNothing(EndDateQ4) Then
                        EndDateQ4 = StartDateQ4.AddMonths(3).AddDays(1)
                    End If
                    'EndDateQ4 = EndDate
                    'LongDays = TotalDays / 4 : EndDateQ1 = StartDate.AddDays(LongDays - 1) : StartDateQ2 = StartDate.AddDays(LongDays)
                    'EndDateQ2 = StartDateQ2.AddDays(LongDays - 1) : StartDateQ3 = StartDateQ2.AddDays(LongDays)
                    'EndDateQ3 = StartDateQ3.AddDays(LongDays - 1) : StartDateQ4 = StartDateQ3.AddDays(LongDays)
                ElseIf Left(Flag, 1) = "S" Then
                    EndDateS1 = StartDateS1.AddMonths(6).AddDays(-1)
                    StartDateS2 = EndDateS1.AddDays(1)
                    If (EndDateS2 > StartDateS2.AddMonths(6).AddDays(1)) Then
                        EndDateS2 = StartDateS2.AddMonths(6).AddDays(-1)
                    ElseIf EndDateS2 < StartDateS2.AddMonths(6).AddDays(-2) Then
                        EndDateS2 = StartDateS2.AddMonths(6).AddDays(-1)
                    ElseIf IsNothing(EndDateS2) Then
                        EndDateS2 = StartDateS2.AddMonths(6).AddDays(-1)
                    End If
                    'LongDays = TotalDays / 2 : EndDateS1 = StartDate.AddDays(LongDays - 1) : StartDateS2 = StartDate.AddDays(LongDays)
                End If
                'Else : LongDays = TotalDays
            End If

            Select Case Flag
                Case "Q1"
                    strDecStartDate = common.CommonClass.getNumericFromDate(StartDateQ1)
                    strDecEndDate = common.CommonClass.getNumericFromDate(NufarmBussinesRules.SharedClass.ServerDate)
                    'If Me.strStartDateFlag <> "" And Me.strEndDateFlag <> "" Then
                    '    If ((strStartDateFlag = strDecStartDate) And (strEndDateFlag = strDecEndDate)) Then
                    '        ''check lagi datanya apakaha di server masih ada / gak
                    '        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    '                "IF EXISTS(SELECT NAME FROM tempdb..Sysobjects WHERE NAME = '##T_START_DATE' AND TYPE = 'U') " & vbCrLf & _
                    '                " BEGIN " & vbCrLf & _
                    '                " SELECT 1 WHERE EXISTS(SELECT START_DATE,END_DATE FROM tempdb..##T_START_DATE WHERE START_DATE = @START_DATE AND END_DATE = @END_DATE) ;" & vbCrLf & _
                    '                "END "
                    '        Me.ResetCommandText(CommandType.StoredProcedure, Query)
                    '        Me.AddParameter("@START_DATE", SqlDbType.NVarChar, strDecStartDate, 100)
                    '        Me.AddParameter("@END_DATE", SqlDbType.VarChar, strDecEndDate, 100)
                    '        Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    '        If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    '            Return
                    '        End If
                    '    End If
                    'End If
                    Me.CreateTempTable(StartDateQ1, NufarmBussinesRules.SharedClass.ServerDate, strDecStartDate, strDecEndDate)
                Case "Q2"
                    strDecStartDate = common.CommonClass.getNumericFromDate(StartDateQ2)
                    strDecEndDate = common.CommonClass.getNumericFromDate(NufarmBussinesRules.SharedClass.ServerDate)
                    Me.CreateTempTable(StartDateQ2, NufarmBussinesRules.SharedClass.ServerDate, strDecStartDate, strDecEndDate)
                Case "Q3"
                    strDecStartDate = common.CommonClass.getNumericFromDate(StartDateQ3)
                    strDecEndDate = common.CommonClass.getNumericFromDate(NufarmBussinesRules.SharedClass.ServerDate)
                    Me.CreateTempTable(StartDateQ3, NufarmBussinesRules.SharedClass.ServerDate, strDecStartDate, strDecEndDate)
                Case "Q4"
                    strDecStartDate = common.CommonClass.getNumericFromDate(StartDateQ4)
                    strDecEndDate = common.CommonClass.getNumericFromDate(NufarmBussinesRules.SharedClass.ServerDate)
                    Me.CreateTempTable(StartDateQ4, NufarmBussinesRules.SharedClass.ServerDate, strDecStartDate, strDecEndDate)
                Case "S1"
                    strDecStartDate = common.CommonClass.getNumericFromDate(StartDateS1)
                    strDecEndDate = common.CommonClass.getNumericFromDate(NufarmBussinesRules.SharedClass.ServerDate)
                    Me.CreateTempTable(StartDateS1, NufarmBussinesRules.SharedClass.ServerDate, strDecStartDate, strDecEndDate)
                Case "S2"
                    strDecStartDate = common.CommonClass.getNumericFromDate(StartDateS2)
                    strDecEndDate = common.CommonClass.getNumericFromDate(NufarmBussinesRules.SharedClass.ServerDate)
                    Me.CreateTempTable(StartDateS2, NufarmBussinesRules.SharedClass.ServerDate, strDecStartDate, strDecEndDate)
                Case "Y"
                    strDecStartDate = common.CommonClass.getNumericFromDate(StartDate)
                    strDecEndDate = common.CommonClass.getNumericFromDate(NufarmBussinesRules.SharedClass.ServerDate)
                    Me.CreateTempTable(StartDate, NufarmBussinesRules.SharedClass.ServerDate, strDecStartDate, strDecEndDate)
            End Select
        End Sub
        Public Function hasReservedInvoice(ByRef userName As String) As Boolean
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "  IF EXISTS(SELECT [NAME] FROM [tempdb].[Sys].[columns] WHERE [NAME] = 'UserName' AND Object_ID = OBJECT_ID('tempdb..##T_START_DATE_" & Me.ComputerName & "')) " & vbCrLf & _
                        "   BEGIN " & vbCrLf & _
                        "       SELECT [UserName] FROM tempdb..##T_START_DATE_" & Me.ComputerName & " WHERE [UserName] != @UserName  ;" & vbCrLf & _
                        "   END " & vbCrLf & _
                        "END "
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
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetAccrued(ByVal Flag As String, Optional ByVal DISTRIBUTOR_ID As String = "", Optional ByVal ListAGREEMENT_NO As List(Of String) = Nothing) As DataSet
            Try
                'hitung ulang PO trus update data
                'haduh gantung euy
                'chek temporari tables ##t_select_invoice 

                'melihat casus total actual yang agak aneh dan sulit di pecahkan
                'ini harus di selesaikan dengan procedure Usp_Check_Sum_Disc_qty dimana Total header di dapat dari total detail
                'ini di dapat dengan membuat table temporary berdasarkan flag
                ''------------bikin temporary table dengan group AgreeBrand_ID----
                ''
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


                '-----------------------------------Header Query -->achievement header--------------------------
                ''TAMBAHKAN TARGET_BY_VALUE
                'DISPRO_BY_VALUE()
                'ACTUAL_BY_VALUE()
                'ACH_DISPRO_BY_VALUE()
                'AGREE_ACH_BY()
                'DISC_BY_VOLUME()
                'DISC_BY_VALUE()
                'DISC_DIST_BY_VOLUME()
                'DISC_DIST_BY_VALUE()
                'DISC_OBTAINED_FROM
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                        "SELECT ACRH.ACHIEVEMENT_ID,ACRH.AGREEMENT_NO,AA.START_DATE,AA.END_DATE,ACRH.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,ACRH.BRAND_ID,BB.BRAND_NAME,ACRH.FLAG," & vbCrLf & _
                        "ACRH.TARGET AS TOTAL_TARGET,ACRH.TARGET_FM,ACRH.TARGET_PL,ACRH.TARGET_BY_VALUE,T_GROUP = CASE WHEN ISTARGET_GROUP = 1 THEN 'YESS' ELSE 'NO' END," & vbCrLf & _
                        "ACRH.DISPRO / 100 AS DISPRO,ACRH.TOTAL_ACTUAL AS ACTUAL,ACTUAL_BY_VALUE = ACRH.TOTAL_ACTUAL_AMOUNT,BALANCE_BY_VALUE = CASE WHEN (ACRH.TOTAL_PO_AMOUNT - ACRH.TARGET_BY_VALUE > 0) THEN 0 " & vbCrLf & _
                        " ELSE (ACRH.TOTAL_PO_AMOUNT - ACRH.TARGET_BY_VALUE) END, ACRH.ACHIEVEMENT_DISPRO / 100 AS ACHIEVEMENT_DISPRO, ACRH.ACH_DISPRO_BY_VALUE/100 AS TOTAL_ACHIEVEMENT, " & vbCrLf & _
                        " ACRH.TOTAL_PO_AMOUNT AS TOTAL_PO_VALUE,ISNULL((ACRH.TOTAL_PO_AMOUNT/ACRH.TARGET_BY_VALUE),0) AS ACH_DISPRO_BY_VALUE, DISPRO_BY_VALUE = ACRH.DISPRO_BY_VALUE/100 " & vbCrLf & _
                        ",ISNULL(AP.AVGPRICE,0) AS AVG_PRICE_FM,ISNULL(AP.AVGPRICE_PL,0) AS AVG_PRICE_PL, CASE ACRH.AGREE_ACH_BY WHEN 'VOL' THEN 'VOLUME' WHEN 'VAL' THEN 'VALUE' ELSE '' END AS AGREE_ACH_BY, ACRH.BONUS_QTY,ACRH.DISC_BY_VOLUME,ACRH.DISC_BY_VALUE " & vbCrLf & _
                        ",ACRH.ACTUAL_DISTRIBUTOR,ACTUAL_DIST_VALUE = ACRH.ACTUAL_AMOUNT_DISTRIBUTOR, ACRH.DISC_DIST_BY_VOLUME,ACRH.DISC_DIST_BY_VALUE, CASE ACRH.DISC_OBTAINED_FROM WHEN 'T1' THEN 'TABLE DISC_BY_VOL' WHEN 'T2' THEN 'TABLE DISC_BY_VAL' ELSE 'TABLE DISC_BY_VOL' END AS DISC_OBTAINED_FROM, " & vbCrLf & _
                        "ACRH.BONUS_DISTRIBUTOR,ACRH.BALANCE, ACRH.ISCOMBINED_TARGET,ACRH.TOTAL_PO_DISTRIBUTOR, ACRH.PO_AMOUNT_DISTRIBUTOR AS PO_VALUE_DISTRIBUTOR, ACRH.TOTAL_PO_ORIGINAL,ACRH.TOTAL_PBQ3,ACRH.TOTAL_JOIN_PBQ3,ACRH.TOTAL_PBQ4,ACRH.TOTAL_JOIN_PBQ4, " & vbCrLf & _
                        "ACRH.TOTAL_PBS2,ACRH.TOTAL_JOIN_PBS2,ACRH.TOTAL_CPQ1,ACRH.TOTAL_JOIN_CPQ1,ACRH.TOTAL_CPQ2,ACRH.TOTAL_JOIN_CPQ2,ACRH.TOTAL_CPQ3,ACRH.TOTAL_JOIN_CPQ3, " & vbCrLf & _
                        "ACRH.TOTAL_CPS1,ACRH.TOTAL_JOIN_CPS1,ACRH.TOTAL_JOIN_PBY,ACRH.TOTAL_PBY,ACRH.[DESCRIPTION],ISNULL(ACRH.COMBINED_BRAND_ID,'') AS COMBINED_BRAND_ID " & vbCrLf & _
                        " FROM ACCRUED_HEADER ACRH INNER JOIN AGREE_AGREEMENT AA ON ACRH.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN DIST_DISTRIBUTOR DR ON ACRH.DISTRIBUTOR_ID " & vbCrLf & _
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

                '--------------------------Detail Query -->Achievement detail---------------------------------------
                Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; SELECT ACD.ACHIEVEMENT_ID,ACD.ACHIEVEMENT_BRANDPACK_ID,ACRH.AGREEMENT_NO,ACD.BRANDPACK_ID,BP.BRANDPACK_NAME," & vbCrLf & _
                          "ACD.TOTAL_ACTUAL, ACD.DISC_QTY,ACD.DISC_BY_VOLUME,ACD.DISC_BY_VALUE,CASE ACD.DISC_OBTAINED_FROM WHEN 'T1' THEN 'TABLE DISC_BY_VOL' WHEN 'T2' THEN 'TABLE DISC_BY_VAL' ELSE 'TABLE DISC_BY_VOL' END AS DISC_OBTAINED_FROM," & vbCrLf & _
                          " ACD.CAN_RELEASE, ACD.RELEASE_QTY , ACD.LEFT_QTY,REM.LEFT_QTY AS REMAIND_QTY,ACD.TOTAL_PO_ORIGINAL," & vbCrLf & _
                          " ACD.TOTAL_PBQ3,ACD.TOTAL_PBQ4,ACD.TOTAL_PBS2,ACD.TOTAL_CPQ1,ACD.TOTAL_CPQ2,ACD.TOTAL_CPQ3,ACD.TOTAL_CPS1,ACD.TOTAL_PBY,ACD.CREATE_BY,ACD.[DESCRIPTIONS],ACD.CAN_UPDATE " & vbCrLf & _
                          " FROM ACCRUED_DETAIL ACD INNER JOIN ACCRUED_HEADER ACRH " & vbCrLf & _
                          " ON ACD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID INNER JOIN BRND_BRANDPACK BP " & vbCrLf & _
                          " ON ACD.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                          " LEFT OUTER JOIN(" & vbCrLf & _
                          "                 SELECT ACHIEVEMENT_BRANDPACK_ID,ISNULL(SUM(LEFT_QTY),0)AS LEFT_QTY FROM ORDR_OA_REMAINDING " & vbCrLf & _
                          "                 WHERE ACHIEVEMENT_BRANDPACK_ID IS NOT NULL AND FLAG = '" & Flag & "' GROUP BY ACHIEVEMENT_BRANDPACK_ID" & vbCrLf & _
                          "                 )REM " & vbCrLf & _
                          " ON ACD.ACHIEVEMENT_BRANDPACK_ID = REM.ACHIEVEMENT_BRANDPACK_ID "
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
                '-----------Detail DPD release/ left Qty---------------------------------
                Me.CreateTempInvoiceTable(Flag, DISTRIBUTOR_ID, ListAGREEMENT_NO, strAgreementNos)
                Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT ACD.ACHIEVEMENT_BRANDPACK_ID,ACD.BRANDPACK_ID,BB.BRANDPACK_NAME,ISNULL(OOBD.DISC_QTY,0) + ISNULL(OOR.DISC_QTY,0)AS DISC_QTY, OPB.PO_REF_NO,OOA.OA_ID AS OA_REF_NO,INV.INVNUMBER," & vbCrLf & _
                        " CAST( '' + SUBSTRING(CAST(INV.INVDATE AS VARCHAR(20)),5,2) +  '/' + RIGHT(CAST(INV.INVDATE AS VARCHAR(20)),2) +  '/' + LEFT(CAST(INV.INVDATE AS VARCHAR(20)),4) AS SMALLDATETIME)AS INVDATE " & vbCrLf & _
                        " FROM ACCRUED_DETAIL ACD INNER JOIN BRND_BRANDPACK BB ON ACD.BRANDPACK_ID = BB.BRANDPACK_ID " & vbCrLf & _
                        " INNER JOIN (SELECT OA_BRANDPACK_ID,ACHIEVEMENT_BRANDPACK_ID,ISNULL(SUM(DISC_QTY),0) AS DISC_QTY FROM ORDR_OA_BRANDPACK_DISC " & vbCrLf & _
                        "               WHERE(ACHIEVEMENT_BRANDPACK_ID Is Not NULL And OA_RM_ID Is NULL AND GQSY_SGT_P_FLAG = '" & Flag & "' ) " & vbCrLf & _
                        "               GROUP BY ACHIEVEMENT_BRANDPACK_ID,OA_BRANDPACK_ID)OOBD ON OOBD.ACHIEVEMENT_BRANDPACK_ID = ACD.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                        " INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOAB.OA_BRANDPACK_ID = OOBD.OA_BRANDPACK_ID " & vbCrLf & _
                        " INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_BRANDPACK_ID = OOAB.PO_BRANDPACK_ID " & vbCrLf & _
                        " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OOA.OA_ID = OOAB.OA_ID INNER JOIN COMPARE_ITEM CI ON CI.BRANDPACK_ID_DTS = OPB.BRANDPACK_ID " & vbCrLf & _
                        " LEFT OUTER JOIN( " & vbCrLf & _
                        " SELECT OOR.ACHIEVEMENT_BRANDPACK_ID,OOBD1.OA_BRANDPACK_ID,ISNULL(SUM(OOBD1.DISC_QTY),0) AS DISC_QTY FROM ORDR_OA_REMAINDING OOR INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD1 " & vbCrLf & _
                        " ON OOBD1.OA_RM_ID = OOR.OA_RM_ID WHERE (OOBD1.OA_RM_ID IS NOT NULL AND OOR.ACHIEVEMENT_BRANDPACK_ID IS NOT NULL) AND OOBD1.GQSY_SGT_P_FLAG = '" & Flag & "' " & vbCrLf & _
                        " GROUP BY  OOR.ACHIEVEMENT_BRANDPACK_ID,OOBD1.OA_BRANDPACK_ID " & vbCrLf & _
                        " ) OOR ON OOR.ACHIEVEMENT_BRANDPACK_ID = ACD.ACHIEVEMENT_BRANDPACK_ID AND OOR.OA_BRANDPACK_ID = OOBD.OA_BRANDPACK_ID " & vbCrLf & _
                        " LEFT OUTER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON ((OPB.PO_REF_NO = INV.PONUMBER) OR (INV.REFERENCE = OOA.RUN_NUMBER)) AND INV.BRANDPACK_ID = CI.BRANDPACK_ID_ACCPAC " & vbCrLf & _
                        " INNER JOIN ACCRUED_HEADER ACRH ON ACRH.ACHIEVEMENT_ID = ACD.ACHIEVEMENT_ID " & vbCrLf
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
                    Query &= " WHERE ACRH.AGREEMENT_NO  " & strAgreementNos & " AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                ElseIf (DISTRIBUTOR_ID <> "") Then
                    Query &= " WHERE ACRH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ACRH.AGREEMENT_NO " & vbCrLf & _
                             " = ANY(SELECT DA.AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA " & vbCrLf & _
                             "       ON DA.AGREEMENT_NO = AA.AGREEMENT_NO WHERE DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                             "       AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 2 ) AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                Else
                    Query &= " WHERE ACRH.AGREEMENT_NO = ANY(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) - 2 " & vbCrLf & _
                             "                               )  AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                End If
                Me.ResetCommandText(CommandType.Text, Query)
                If Not String.IsNullOrEmpty(DISTRIBUTOR_ID) Then
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID)
                End If
                Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
                Dim dtTableDPDDetail As New DataTable("DETAIL_RELEASED_DPD") : dtTableDPDDetail.Clear()
                Me.SqlDat.Fill(dtTableDPDDetail) : Me.ClearCommandParameters()
                Me.baseDataSet.Tables.Add(dtTableHeader) : Me.baseDataSet.Tables.Add(dtTableDetail) : Me.baseDataSet.Tables.Add(dtTableDPDDetail)
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
                Me.strStartDateFlag = "" : Me.strEndDateFlag = ""
                Me.ClearCommandParameters() : Me.DropTempTable()
                Return Me.baseDataSet
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Try
                    Me.ClearCommandParameters()
                    Me.DropTempTable()
                Catch ex1 As Exception
                    Me.strStartDateFlag = "" : Me.strEndDateFlag = "" : Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                    Throw ex1
                End Try
                Me.strStartDateFlag = "" : Me.strEndDateFlag = "" : Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Private Sub deleteTheSameAchievementBrandPackID(ByRef tblDPDDetail As DataTable)
            Try
                Dim listAchBrandPackID As New List(Of String)
                For i As Integer = 0 To tblDPDDetail.Rows.Count - 1
                    If (listAchBrandPackID.Contains(tblDPDDetail.Rows(i)("ACHIEVEMENT_BRANDPACK_ID").ToString())) Then
                        tblDPDDetail.Rows.RemoveAt(i)
                        tblDPDDetail.AcceptChanges()
                        If i < tblDPDDetail.Rows.Count - 1 Then
                            i -= 1
                        Else
                            Exit For
                        End If
                    Else
                        listAchBrandPackID.Add(tblDPDDetail.Rows(i)("ACHIEVEMENT_BRANDPACK_ID").ToString())
                    End If
                Next
            Catch ex As Exception

            End Try
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
        Public Function GetAccrued(ByVal Flag As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, Optional ByVal ListDistributor As List(Of String) = Nothing)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_DistAccrue' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE tempdb..##T_DistAccrue ; END "
                If Not IsNothing(Me.SqlCom) Then
                    Me.GetConnection() : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else : Me.CreateCommandSql("sp_executesql", "")
                End If
                Me.AddParameter("stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(ListDistributor) Then
                    If ListDistributor.Count > 0 Then
                        Query = "CREATE TABLE ##T_DistAccrue( " & vbCrLf & _
                                " [DISTRIBUTOR_ID] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL) "
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                        For i As Integer = 0 To ListDistributor.Count - 1
                            Query = "SET NOCOUNT ON;" & vbCrLf & _
                                    "INSERT INTO tempdb..##T_DistAccrue(DISTRIBUTOR_ID) " & vbCrLf & _
                                    " VALUES('" & ListDistributor(i) & "'); "
                            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Next
                        Me.CommiteTransaction()
                    End If
                End If
                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Accrued")
                Me.AddParameter("@QS_TREATMENT_FLAG", SqlDbType.Char, Flag, 1)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Return Me.FillDataTable(New DataTable("T_Accrued")).DefaultView
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.ClearCommandParameters()
                Me.DropTempTable()
                Me.RollbackTransaction() : Me.CloseConnection() : Throw ex
            End Try
        End Function
        Public Function GetDistributorAgreement(ByVal SearchString As String) As DataView
            Try
                If String.IsNullOrEmpty(SearchString) Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR " & vbCrLf & _
                            " WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID); "

                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR " & vbCrLf & _
                            " WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                            " AND DR.DISTRIBUTOR_NAME LIKE '%" & SearchString & "%';"
                End If
                If Me.SqlCom Is Nothing Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.GetConnection() : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Return Me.FillDataTable(New DataTable("T_Distributor")).DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetDistributorAgrement(ByRef Flag As String, Optional ByVal Searchstring As String = "") As DataView
            Try
                'Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                '        "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                '        " WHERE AA.END_DATE >=  GETDATE() AND AA.QS_TREATMENT_FLAG = '" & Flag.Remove(1, 1) & "' AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                '        " AND DR.DISTRIBUTOR_NAME LIKE '%" & Searchstring & "%';"
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
        Public Function GetAgreementNo(ByVal Flag As String, Optional ByVal DistributorID As String = "", Optional ByVal AGREEMENT_NO As String = "", Optional ByVal DefaultMaxyear As Integer = 2) As DataView
            Try
                Query = "SET NOCOUNT ON;"
                Dim strFlag As String = Flag
                If Not strFlag = "Y" Then
                    strFlag = strFlag.Remove(1, 1)
                    If AGREEMENT_NO <> "" And DistributorID <> "" Then
                        Query &= vbCrLf
                        Query &= "SELECT TOP 100 AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                        " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%'))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                        " AND AA.QS_TREATMENT_FLAG = @FLAG ;"
                    ElseIf DistributorID <> "" Then
                        Query &= "SELECT AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                                  " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID ))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                                  " AND AA.QS_TREATMENT_FLAG = @FLAG  AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - @DefMaxyear ;"
                    ElseIf AGREEMENT_NO <> "" Then
                        Query &= vbCrLf
                        Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%' " & vbCrLf & _
                        " AND QS_TREATMENT_FLAG = @FLAG ;"
                    Else
                        Query &= vbCrLf
                        Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) - @DefMaxyear " & vbCrLf & _
                        " AND QS_TREATMENT_FLAG = @FLAG ;"
                    End If
                Else
                    If AGREEMENT_NO <> "" And DistributorID <> "" Then
                        Query &= vbCrLf
                        Query &= "SELECT AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                        " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%'))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                        " AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - @DefMaxyear ;"
                    ElseIf DistributorID <> "" Then
                        Query &= "SELECT AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN(SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                                  " WHERE (DISTRIBUTOR_ID = @DISTRIBUTOR_ID ))DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                                  " AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - @DefMaxyear ;"
                    ElseIf AGREEMENT_NO <> "" Then
                        Query &= vbCrLf
                        Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO LIKE '%" & AGREEMENT_NO & "%' " & vbCrLf & _
                        " AND YEAR(END_DATE) >= YEAR(@GETDATE) - @DefMaxyear;"
                    Else
                        Query &= vbCrLf
                        Query &= "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) - @DefMaxyear ; "
                    End If
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
        Private Function mustUpdateTotalPBY(ByVal tblToCheck As DataTable, ByVal Row_ID As String, ByVal Flag As String) As Boolean

            If Not IsNothing(tblToCheck) Then
                If (tblToCheck.Rows.Count > 0) Then
                    Dim RowsCheck() As DataRow = tblToCheck.Select("ROW_ID = '" & Row_ID & "'")
                    If RowsCheck.Length > 0 Then
                        For i As Integer = 0 To RowsCheck.Length - 1
                            If CDec(RowsCheck(i)("TOTAL_PBY")) > 0 Then
                                ''check apakah flagnya sama
                                If RowsCheck(i)("FLAG").ToString() <> Flag Then
                                    Return False
                                End If
                            End If
                        Next
                        Return True
                    Else
                        Return True
                    End If
                    ''check agree_brand_id
                    ''check row di acrHeaderyear apakah jumlahnya sama/gak dengan rows yang di filter berdasarkan agree_brand_id
                    'Dim RowSSelectWithSameFlag() As DataRow = tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "' AND FLAG = '" & Flag & "'")
                    'For i As Integer = 0 To RowSSelectWithSameFlag.Length - 1
                    '    For i1 As Integer = 0 To RowsSelect.Length - 1
                    '        If RowsSelect(i1)("ACHIEVEMENT_ID").Equals(RowSSelectWithSameFlag(i)("ACHIEVEMENT_ID")) Then
                    '            RowsSelect(i1).BeginEdit()
                    '            RowsSelect(i1)("TOTAL_JOIN_PBY") = TotalJoinPBy
                    '            RowsSelect(i1).EndEdit()
                    '        End If
                    '    Next
                    'Next
                    'For i As Integer = 0 To RowsSelect.Length - 1
                    '    Dim BFind As Boolean = False
                    '    For i1 As Integer = 0 To RowSSelectWithSameFlag.Length - 1
                    '        If RowSSelectWithSameFlag(i1)(" ACHIEVEMENT_ID").ToString() = RowsSelect(i)("ACHIEVEMENT_ID").ToString() Then
                    '            BFind = True
                    '        End If
                    '    Next
                    '    If Not BFind Then
                    '        Dim Row_ID As String = RowsSelect(0)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect(0)("AGREEMENT_NO").ToString() & "" & RowsSelect(0)("BRAND_ID").ToString()
                    '        'check keberadaan nya apakah ada di row yang lain dengan agree_brand_id yang sama
                    '        Dim BFind1 As Boolean = False
                    '        rowsCheck = tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "' AND FLAG = '" & Flag & "'")
                    '        If rowsCheck.Length > 0 Then
                    '            rowsCheck = tblAcrHeaderYear.Select("ROW_ID = '" & Row_ID & "'")
                    '            For i1 As Integer = 0 To rowsCheck.Length - 1
                    '                If Convert.ToDecimal(rowsCheck(i1)("TOTAL_JOIN_PBY")) > 0 Then
                    '                    BFind1 = True
                    '                End If
                    '            Next
                    '            If Not BFind1 Then
                    '                RowsSelect(i).BeginEdit()
                    '                RowsSelect(i)("TOTAL_JOIN_PBY") = TotalJoinPBy
                    '                RowsSelect(i).EndEdit()

                    '            End If
                    '        Else

                    '        End If

                    '    End If
                    'Next

                Else
                    Return True
                End If
            Else
                Return True
            End If
        End Function
        Private Sub fillPerideBeforeYear(ByVal Flag As String, ByRef RowsSelect() As DataRow, ByRef RowsCheck() As DataRow, ByRef RowsSelect1() As DataRow, ByRef TotalJoinPBy As Decimal, ByRef TotalJoinPBY1 As Decimal)
            Select Case Flag
                Case "Q1"        'isi totalPBY untuk seluruh row
                    TotalJoinPBy = Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ3")) + Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4")) + Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBS2"))
                    For i As Integer = 0 To RowsSelect.Length - 1
                        Dim Row_ID As String = RowsSelect(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect(i)("AGREEMENT_NO").ToString() & "" & RowsSelect(i)("BRAND_ID").ToString()
                        If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                            RowsSelect(i).BeginEdit()
                            RowsSelect(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ3")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                            RowsSelect(i).EndEdit()
                        End If
                        'RowsSelect(i).BeginEdit()
                        'RowsSelect(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ3")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                        'RowsSelect(i).EndEdit()
                    Next

                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            TotalJoinPBY1 = Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ3")) + Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ4")) + Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBS2"))
                            'isi totalPBY untuk seluruh row
                            For i As Integer = 0 To RowsSelect1.Length - 1
                                Dim Row_ID As String = RowsSelect1(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect1(i)("AGREEMENT_NO").ToString() & "" & RowsSelect1(i)("BRAND_ID").ToString()
                                If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                                    RowsSelect1(i).BeginEdit()
                                    RowsSelect1(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBQ3")) + Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBS2"))
                                    RowsSelect1(i).EndEdit()
                                End If
                                'RowsSelect1(i).BeginEdit()
                                'RowsSelect1(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBQ3")) + Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBS2"))
                                'RowsSelect1(i).EndEdit()
                            Next
                        End If
                    End If

                Case "Q2"
                    Dim TTotalPBQ3 As Decimal = 0, TTotalPBS2 As Decimal = 0, TPBQ3 As Decimal = 0, TPBS2 As Decimal = 0
                    RowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                    If RowsCheck.Length > 0 Then
                        TTotalPBQ3 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ3)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        TotalJoinPBy += TTotalPBQ3
                        TTotalPBS2 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBS2)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        TotalJoinPBy += TTotalPBS2

                        'TTotalPBQ4 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ4)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        'TotalJoinPBy += TTotalPBQ4
                        For i As Integer = 0 To RowsSelect.Length - 1
                            'RowsSelect(i).BeginEdit()
                            Dim _Row() As DataRow = tblAcrHeaderYear.Select("ACHIEVEMENT_ID = '" & RowsSelect(i)("ACHIEVEMENT_ID").ToString() & "'")
                            If (_Row.Length > 0) Then
                                TPBQ3 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ3"))
                                TPBS2 = Convert.ToDecimal(_Row(0)("TOTAL_PBS2"))
                            End If
                            'RowsSelect(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + TPBQ3 + TPBS2
                            Dim Row_ID As String = RowsSelect(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect(i)("AGREEMENT_NO").ToString() & "" & RowsSelect(i)("BRAND_ID").ToString()
                            If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                                RowsSelect(i).BeginEdit()
                                RowsSelect(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + TPBQ3 + TPBS2
                                RowsSelect(i).EndEdit()
                            End If
                            'Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                            'RowsSelect(i).EndEdit()
                        Next
                        TotalJoinPBy += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4"))

                        If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                            If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                                RowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                If RowsCheck.Length > 0 Then
                                    TTotalPBQ3 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ3)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                    TotalJoinPBY1 += TTotalPBQ3
                                    TTotalPBS2 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBS2)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                    TotalJoinPBY1 += TTotalPBS2
                                    For i As Integer = 0 To RowsSelect1.Length - 1
                                        'RowsSelect1(i).BeginEdit()
                                        Dim _Row() As DataRow = tblAcrHeaderYear.Select("ACHIEVEMENT_ID = '" & RowsSelect1(i)("ACHIEVEMENT_ID").ToString() & "'")
                                        If (_Row.Length > 0) Then
                                            TPBQ3 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ3"))
                                            TPBS2 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ4"))
                                        End If
                                        'RowsSelect1(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBQ4")) + TPBQ3 + TPBS2
                                        Dim Row_ID As String = RowsSelect1(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect1(i)("AGREEMENT_NO").ToString() & "" & RowsSelect1(i)("BRAND_ID").ToString()
                                        If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                                            RowsSelect1(i).BeginEdit()
                                            RowsSelect1(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBQ4")) + TPBQ3 + TPBS2
                                            RowsSelect1(i).EndEdit()
                                        End If
                                        'RowsSelect1(i).EndEdit()
                                    Next
                                End If
                                TotalJoinPBY1 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ4"))
                            End If
                        End If
                    End If
                Case "Q3"
                    Dim TTotalPBQ4 As Decimal = 0, TTotalPBS2 As Decimal = 0, TPBQ4 As Decimal = 0, TPBS2 As Decimal = 0
                    RowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                    If RowsCheck.Length > 0 Then
                        TTotalPBQ4 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ4)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        TotalJoinPBy += TTotalPBQ4
                        TTotalPBS2 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBS2)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        TotalJoinPBy += TTotalPBS2
                        For i As Integer = 0 To RowsSelect.Length - 1
                            'RowsSelect(i).BeginEdit()
                            Dim _Row() As DataRow = tblAcrHeaderYear.Select("ACHIEVEMENT_ID = '" & RowsSelect(i)("ACHIEVEMENT_ID").ToString() & "'")
                            If (_Row.Length > 0) Then
                                TPBQ4 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ4"))
                                TPBS2 = Convert.ToDecimal(_Row(0)("TOTAL_PBS2"))
                            End If
                            'RowsSelect(i)("TOTAL_PBY") = TPBQ4 + TPBS2
                            Dim Row_ID As String = RowsSelect(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect(i)("AGREEMENT_NO").ToString() & "" & RowsSelect(i)("BRAND_ID").ToString()
                            If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                                RowsSelect(i).BeginEdit()
                                RowsSelect(i)("TOTAL_PBY") = TPBQ4 + TPBS2
                                RowsSelect(i).EndEdit()
                            End If

                            'RowsSelect(i).EndEdit()
                        Next
                        If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                            If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                                RowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                If RowsCheck.Length > 0 Then
                                    TTotalPBQ4 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ4)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                    TotalJoinPBY1 += TTotalPBQ4
                                    TTotalPBS2 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBS2)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                    TotalJoinPBY1 += TTotalPBS2
                                    For i As Integer = 0 To RowsSelect1.Length - 1
                                        'RowsSelect1(i).BeginEdit()
                                        Dim _Row() As DataRow = tblAcrHeaderYear.Select("ACHIEVEMENT_ID = '" & RowsSelect1(i)("ACHIEVEMENT_ID").ToString() & "'")
                                        If (_Row.Length > 0) Then
                                            TPBQ4 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ4"))
                                            TPBS2 = Convert.ToDecimal(_Row(0)("TOTAL_PBS2"))
                                        End If
                                        Dim Row_ID As String = RowsSelect1(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect1(i)("AGREEMENT_NO").ToString() & "" & RowsSelect1(i)("BRAND_ID").ToString()
                                        If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                                            RowsSelect1(i).BeginEdit()
                                            RowsSelect1(i)("TOTAL_PBY") = TPBQ4 + TPBS2
                                            RowsSelect1(i).EndEdit()
                                        End If
                                        'Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                                        'RowsSelect1(i).EndEdit()
                                    Next
                                End If
                            End If
                        End If
                    End If
                Case "Q4"
                    Dim TTotalPBQ4 As Decimal = 0, TTotalPBS2 As Decimal = 0, TTotalPBQ3 As Decimal = 0, TPBQ3 As Decimal = 0, TPBQ4 As Decimal = 0, TPBS2 As Decimal = 0
                    RowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                    If RowsCheck.Length > 0 Then
                        TTotalPBQ3 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ3)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        TotalJoinPBy += TTotalPBQ3
                        TTotalPBQ4 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ4)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        TotalJoinPBy += TTotalPBQ4
                        TTotalPBS2 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBS2)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        TotalJoinPBy += TTotalPBS2
                        For i As Integer = 0 To RowsSelect.Length - 1
                            'RowsSelect(i).BeginEdit()
                            Dim _Row() As DataRow = tblAcrHeaderYear.Select("ACHIEVEMENT_ID = '" & RowsSelect(i)("ACHIEVEMENT_ID").ToString() & "'")
                            If (_Row.Length > 0) Then
                                TPBQ3 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ3"))
                                TPBQ4 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ4"))
                                TPBS2 = Convert.ToDecimal(_Row(0)("TOTAL_PBS2"))
                            End If
                            'check di database /tblAcrheadeyEar apakah totalPby sudah ada
                            'dengan filter row_id(distributor_id,agreement_no,brand_id
                            'bila sudah ada check apakah flag nya sama
                            'bila sama flag nya updatedatanya
                            'bila tidak sama abaikan
                            'bila belum ada datanya update rowsSelectnya
                            Dim Row_ID As String = RowsSelect(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect(i)("AGREEMENT_NO").ToString() & "" & RowsSelect(i)("BRAND_ID").ToString()
                            If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                                RowsSelect(i).BeginEdit()
                                RowsSelect(i)("TOTAL_PBY") = TPBQ3 + TPBQ4 + TPBS2
                                RowsSelect(i).EndEdit()
                            End If
                            'Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                            'RowsSelect(i).EndEdit()
                        Next
                    End If
                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            RowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                            If RowsCheck.Length > 0 Then
                                TTotalPBQ3 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ3)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                TotalJoinPBY1 += TTotalPBQ3
                                TTotalPBQ4 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ4)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                TotalJoinPBY1 += TTotalPBQ4
                                TTotalPBS2 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBS2)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                TotalJoinPBY1 += TTotalPBS2
                                For i As Integer = 0 To RowsSelect1.Length - 1
                                    Dim _Row() As DataRow = tblAcrHeaderYear.Select("ACHIEVEMENT_ID = '" & RowsSelect1(i)("ACHIEVEMENT_ID").ToString() & "'")
                                    If (_Row.Length > 0) Then
                                        TPBQ3 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ3"))
                                        TPBQ4 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ4"))
                                        TPBS2 = Convert.ToDecimal(_Row(0)("TOTAL_PBS2"))
                                    End If
                                    Dim Row_ID As String = RowsSelect1(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect1(i)("AGREEMENT_NO").ToString() & "" & RowsSelect1(i)("BRAND_ID").ToString()
                                    If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                                        RowsSelect1(i).BeginEdit()
                                        RowsSelect1(i)("TOTAL_PBY") = TPBQ3 + TPBQ4 + TPBS2
                                        RowsSelect1(i).EndEdit()
                                    End If
                                    'Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                                Next
                            End If
                        End If
                    End If
                Case "S1"

                    RowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                    If RowsCheck.Length > 0 Then
                        For i As Integer = 0 To RowsSelect.Length - 1
                            Dim Row_ID As String = RowsSelect(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect(i)("AGREEMENT_NO").ToString() & "" & RowsSelect(i)("BRAND_ID").ToString()
                            If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                                RowsSelect(i).BeginEdit()
                                RowsSelect(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ3")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                                RowsSelect(i).EndEdit()
                            End If
                        Next
                    Else
                        For i As Integer = 0 To RowsSelect.Length - 1
                            RowsSelect(i).BeginEdit()
                            RowsSelect(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ3")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                            RowsSelect(i).EndEdit()
                        Next
                        'Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ3")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                    End If

                    TotalJoinPBy = Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ3")) + Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4")) + Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBS2"))

                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            RowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                            If RowsCheck.Length > 0 Then
                                For i As Integer = 0 To RowsSelect1.Length - 1
                                    Dim Row_ID As String = RowsSelect1(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect1(i)("AGREEMENT_NO").ToString() & "" & RowsSelect1(i)("BRAND_ID").ToString()
                                    If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                                        RowsSelect1(i).BeginEdit()
                                        RowsSelect1(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBQ3")) + Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBS2"))
                                        RowsSelect1(i).EndEdit()
                                    End If
                                Next
                            Else
                                For i As Integer = 0 To RowsSelect1.Length - 1
                                    RowsSelect1(i).BeginEdit()
                                    RowsSelect1(i)("TOTAL_PBY") = Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBQ3")) + Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect1(i)("TOTAL_PBS2"))
                                    RowsSelect1(i).EndEdit()
                                Next
                                'Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ3")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                            End If
                            TotalJoinPBY1 = Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ3")) + Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ4")) + Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBS2"))
                        End If
                    End If
                Case "S2"
                    Dim TTotalPBQ4 As Decimal = 0, TTotalPBS2 As Decimal = 0, TTotalPBQ3 As Decimal = 0, TPBQ3 As Decimal = 0, TPBQ4 As Decimal = 0, TPBS2 As Decimal = 0
                    RowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                    If RowsCheck.Length > 0 Then
                        TTotalPBQ3 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ3)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        TotalJoinPBy += TTotalPBQ3
                        TTotalPBQ4 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ4)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        TotalJoinPBy += TTotalPBQ4
                        TTotalPBS2 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBS2)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        TotalJoinPBy += TTotalPBS2
                        For i As Integer = 0 To RowsSelect.Length - 1
                            'RowsSelect(i).BeginEdit()
                            Dim _Row() As DataRow = tblAcrHeaderYear.Select("ACHIEVEMENT_ID = '" & RowsSelect(i)("ACHIEVEMENT_ID").ToString() & "'")
                            If (_Row.Length > 0) Then
                                TPBQ3 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ3"))
                                TPBQ4 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ4"))
                                TPBS2 = Convert.ToDecimal(_Row(0)("TOTAL_PBS2"))
                            End If
                            'check di database /tblAcrheadeyEar apakah totalPby sudah ada
                            'dengan filter row_id(distributor_id,agreement_no,brand_id
                            'bila sudah ada check apakah flag nya sama
                            'bila sama flag nya updatedatanya
                            'bila tidak sama abaikan
                            'bila belum ada datanya update rowsSelectnya
                            Dim Row_ID As String = RowsSelect(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect(i)("AGREEMENT_NO").ToString() & "" & RowsSelect(i)("BRAND_ID").ToString()
                            If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                                RowsSelect(i).BeginEdit()
                                RowsSelect(i)("TOTAL_PBY") = TPBQ3 + TPBQ4 + TPBS2
                                RowsSelect(i).EndEdit()
                            End If
                            'Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                            'RowsSelect(i).EndEdit()
                        Next
                    End If
                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            RowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                            If RowsCheck.Length > 0 Then
                                TTotalPBQ3 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ3)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                TotalJoinPBY1 += TTotalPBQ3
                                TTotalPBQ4 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ4)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                TotalJoinPBY1 += TTotalPBQ4
                                TTotalPBS2 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBS2)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                TotalJoinPBY1 += TTotalPBS2
                                For i As Integer = 0 To RowsSelect1.Length - 1
                                    Dim _Row() As DataRow = tblAcrHeaderYear.Select("ACHIEVEMENT_ID = '" & RowsSelect1(i)("ACHIEVEMENT_ID").ToString() & "'")
                                    If (_Row.Length > 0) Then
                                        TPBQ3 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ3"))
                                        TPBQ4 = Convert.ToDecimal(_Row(0)("TOTAL_PBQ4"))
                                        TPBS2 = Convert.ToDecimal(_Row(0)("TOTAL_PBS2"))
                                    End If
                                    Dim Row_ID As String = RowsSelect1(i)("DISTRIBUTOR_ID").ToString() & "" & RowsSelect1(i)("AGREEMENT_NO").ToString() & "" & RowsSelect1(i)("BRAND_ID").ToString()
                                    If mustUpdateTotalPBY(Me.tblAcrHeaderYear, Row_ID, Flag) Then
                                        RowsSelect1(i).BeginEdit()
                                        RowsSelect1(i)("TOTAL_PBY") = TPBQ3 + TPBQ4 + TPBS2
                                        RowsSelect1(i).EndEdit()
                                    End If
                                    'Convert.ToDecimal(RowsSelect(i)("TOTAL_PBQ4")) + Convert.ToDecimal(RowsSelect(i)("TOTAL_PBS2"))
                                Next
                            End If
                        End If
                    End If
            End Select
            Dim _rowS As DataRow = Nothing
            For i As Int32 = 0 To RowsSelect.Length - 1
                _rowS = RowsSelect(i)
                _rowS.BeginEdit()
                _rowS("TOTAL_JOIN_PBY") = TotalJoinPBy + TotalJoinPBY1
                _rowS.EndEdit()
            Next
            If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                    For i As Integer = 0 To RowsSelect1.Length - 1
                        _rowS = RowsSelect1(i)
                        _rowS.BeginEdit()
                        _rowS("TOTAL_JOIN_PBY") = TotalJoinPBy + TotalJoinPBY1
                        _rowS.EndEdit()
                    Next
                End If
            End If
        End Sub

        Private Sub FillPeriodeBeforeYearDetail(ByVal Flag As String, ByVal RowsSelectDetails() As DataRow, ByRef TotalPBy As Decimal, ByVal DistributorID As String, ByVal AgreementNO As String, ByVal BrandID As String)
            Dim RowsCheck() As DataRow = Nothing
            Dim AchievementBrandPackIDs As String = ""
            Dim TotalPBQ3 As Decimal = 0, TotalPBQ4 As Decimal = 0, TotalPBS2 As Decimal = 0

            For i As Integer = 0 To RowsSelectDetails.Length - 1
                Dim BrandPackID As String = RowsSelectDetails(i)("BRANDPACK_ID").ToString()
                Dim Filter As String = "ACHIEVEMENT_BRANDPACK_ID IN('" & DistributorID & "|" & AgreementNO & BrandID & "|Q1|" & BrandPackID & _
                                                                 "','" & DistributorID & "|" & AgreementNO & BrandID & "|Q2|" & BrandPackID & _
                                                                 "','" & DistributorID & "|" & AgreementNO & BrandID & "|Q3|" & BrandPackID & _
                                                                 "','" & DistributorID & "|" & AgreementNO & BrandID & "|Q4|" & BrandPackID & _
                                                                 "','" & DistributorID & "|" & AgreementNO & BrandID & "|S1|" & BrandPackID & _
                                                                 "','" & DistributorID & "|" & AgreementNO & BrandID & "|S2|" & BrandPackID & "')"

                RowsCheck = Me.tblAcrDetailYear.Select(Filter)
                If RowsCheck.Length > 0 Then
                    Select Case Flag
                        Case "Q2"
                            TotalPBQ3 = tblAcrDetailYear.Compute("SUM(TOTAL_PBQ3)", Filter)
                            TotalPBS2 = tblAcrDetailYear.Compute("SUM(TOTAL_PBS2)", Filter)
                            TotalPBQ4 = tblAcrDetailYear.Compute("SUM(TOTAL_PBQ4)", Filter)
                            TotalPBy += (TotalPBQ3 + TotalPBQ4 + TotalPBS2)
                        Case "Q3"
                            TotalPBQ3 = tblAcrDetailYear.Compute("SUM(TOTAL_PBQ3)", Filter)
                            TotalPBS2 = tblAcrDetailYear.Compute("SUM(TOTAL_PBS2)", Filter)
                            TotalPBy += (TotalPBQ3 + TotalPBS2)
                        Case "Q4"
                        Case "S2"
                            TotalPBQ3 = tblAcrDetailYear.Compute("SUM(TOTAL_PBQ3)", Filter)
                            TotalPBS2 = tblAcrDetailYear.Compute("SUM(TOTAL_PBS2)", Filter)
                            TotalPBQ4 = tblAcrDetailYear.Compute("SUM(TOTAL_PBQ4)", Filter)
                            TotalPBy += (TotalPBQ3 + TotalPBQ4 + TotalPBS2)
                    End Select
                End If
            Next

        End Sub
        Private Sub CalculateDetail(ByVal Flag As String, ByVal IsNewOrIsChanged As String, ByRef tblAcrDetail As DataTable, ByVal tblAcrHeader As DataTable, ByRef AchievementBrandPacks As List(Of String))
            Dim RowsSelectHeader() As DataRow = tblAcrHeader.Select(IsNewOrIsChanged & " = " & True)
            For i As Integer = 0 To RowsSelectHeader.Length - 1
                Dim Dispro As Decimal = Convert.ToDecimal(RowsSelectHeader(i)("DISPRO"))
                'Dim BrandID As String = RowsSelectHeader(i)("BRAND_ID")

                Dim DistributorID As String = RowsSelectHeader(i)("DISTRIBUTOR_ID") _
                    , AgreementNo As String = RowsSelectHeader(i)("AGREEMENT_NO").ToString(), BrandID As String = RowsSelectHeader(i)("BRAND_ID").ToString()
                '===========COMMENT THIS AFTER DEBUGGING=========================
                'If BrandID = "77230" Or BrandID = "77240" Then
                '    Stop
                'End If
                '===============END COMMENT THIS AFTER DEBUGGING ==============================
                Dim RowsSelectDetail() As DataRow = tblAcrDetail.Select(IsNewOrIsChanged & " = " & True & " AND ACHIEVEMENT_ID = '" & RowsSelectHeader(i)("ACHIEVEMENT_ID").ToString() & "'")
                For i1 As Integer = 0 To RowsSelectDetail.Length - 1
                    Dim TotalPBy As Decimal = 0
                    Dim totalInvoiceBeforeQ3 As Decimal = 0, totalInvoiceBeforeQ4 As Decimal = 0, totalInvoiceBeforeS2 As Decimal = 0, _
                    totalInvoiceCurrentQ1 As Decimal = 0, totalInvoiceCurrentQ2 As Decimal = 0, totalInvoiceCurrentQ3 As Decimal = 0, totalInvoiceCurrentS1 As Decimal = 0, TotalInvoiceBeforeY As Decimal = 0
                    Dim AchievementBrandPack As String = RowsSelectDetail(i1)("ACHIEVEMENT_BRANDPACK_ID")
                    If Not String.IsNullOrEmpty(AchievementBrandPack) Then
                        AchievementBrandPacks.Add(AchievementBrandPack)
                    End If
                    Dim rowID As String = DistributorID & "" & AgreementNo & "" & BrandID & "" & RowsSelectDetail(i1)("BRANDPACK_ID").ToString()
                    'If i1 < RowsSelectDetail.Length - 1 Then
                    '    AchievementBrandPacks &= ",'"
                    'End If
                    Dim BonusQtyBefore As Decimal = 0
                    Dim givenProgressive As Decimal = 0, totalActualBefore As Decimal = 0, Descriptions As String = "", FlagPeriodBefore As String = ""
                    Select Case Flag
                        Case "Q1"
                            If CDec(RowsSelectDetail(i1)("TOTAL_PBQ3")) > 0 Then
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ3")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPPBQ3")) / 100))
                                totalInvoiceBeforeQ3 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ3"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPPBQ3")) / 100
                                FlagPeriodBefore = "Q3"
                                Descriptions = String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceBeforeQ3)
                            End If
                            If CDec(RowsSelectDetail(i1)("TOTAL_PBQ4")) > 0 Then
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ4")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPPBQ4")) / 100))
                                totalInvoiceBeforeQ4 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ4"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPPBQ4")) / 100
                                FlagPeriodBefore = "Q4"
                                If Descriptions.Length > 0 Then
                                    Descriptions &= ", "
                                End If
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceBeforeQ4)
                            End If
                            If CDec(RowsSelectDetail(i1)("TOTAL_PBS2")) > 0 Then
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBS2")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPPBS2")) / 100))
                                totalInvoiceBeforeS2 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBS2"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPPBS2")) / 100
                                FlagPeriodBefore = "S2"
                                If Descriptions.Length > 0 Then
                                    Descriptions &= ", "
                                End If
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceBeforeS2)
                            End If
                            TotalPBy = totalInvoiceBeforeQ3 + totalInvoiceBeforeQ4 + totalInvoiceBeforeS2
                            'If TotalPBy > 0 Then
                            '    givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPPBY")) / 100
                            '    BonusQtyBefore += (TotalPBy * (givenProgressive / 100))
                            '    If Descriptions.Length > 0 Then
                            '        Descriptions &= String.Format("{0:p} of Y, Y = {1:#,##0.000} Qty", givenProgressive, TotalPBy)
                            '    End If
                            'End If
                        Case "Q2"
                            If CDec(RowsSelectDetail(i1)("TOTAL_PBQ4")) > 0 Then
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ4")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPPBQ4")) / 100))
                                totalInvoiceBeforeQ4 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ4"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPPBQ4")) / 100
                                FlagPeriodBefore = "Q4"
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceBeforeQ4)
                            End If
                            If CDec(RowsSelectDetail(i1)("TOTAL_CPQ1")) > 0 Then
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPQ1")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPCPQ1")) / 100))
                                totalInvoiceCurrentQ1 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPQ1"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPCPQ1")) / 100
                                FlagPeriodBefore &= "Q1"
                                If Descriptions.Length > 0 Then
                                    Descriptions &= ", "
                                End If
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceCurrentQ1)
                            End If
                            Me.FillPeriodeBeforeYearDetail(Flag, RowsSelectDetail, TotalPBy, DistributorID, AgreementNo, BrandID)
                        Case "Q3"
                            If CDec(RowsSelectDetail(i1)("TOTAL_CPQ1")) > 0 Then
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPQ1")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPCPQ1")) / 100))
                                totalInvoiceCurrentQ1 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPQ1"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPCPQ1")) / 100
                                FlagPeriodBefore = "Q1"
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceCurrentQ1)
                            End If
                            If CDec(RowsSelectDetail(i1)("TOTAL_CPQ2")) > 0 Then
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPQ2")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPCPQ2")) / 100))
                                totalInvoiceCurrentQ2 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPQ2"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPCPQ2")) / 100
                                FlagPeriodBefore &= "Q2"
                                If Descriptions.Length > 0 Then
                                    Descriptions &= ", "
                                End If
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceCurrentQ2)
                            End If
                            Me.FillPeriodeBeforeYearDetail(Flag, RowsSelectDetail, TotalPBy, DistributorID, AgreementNo, BrandID)
                        Case "Q4"
                            If CDec(RowsSelectDetail(i1)("TOTAL_CPQ2")) > 0 Then
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPQ2")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPCPQ2")) / 100))
                                totalInvoiceCurrentQ2 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPQ2"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPCPQ2")) / 100
                                FlagPeriodBefore = "Q2"
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceCurrentQ2)
                            End If
                            If CDec(RowsSelectDetail(i1)("TOTAL_CPQ3")) > 0 Then
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPQ3")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPCPQ3")) / 100))
                                totalInvoiceCurrentQ3 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPQ3"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPCPQ3")) / 100
                                FlagPeriodBefore &= "Q3"
                                If Descriptions.Length > 0 Then
                                    Descriptions &= ", "
                                End If
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceCurrentQ3)
                            End If
                            Me.FillPeriodeBeforeYearDetail(Flag, RowsSelectDetail, TotalPBy, DistributorID, AgreementNo, BrandID)
                        Case "S1"
                            If CDec(RowsSelectDetail(i1)("TOTAL_PBS2")) > 0 Then
                                'totalActualBefore += Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBS2"))
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBS2")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPPBS2")) / 100))
                                totalInvoiceBeforeS2 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBS2"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPPBS2")) / 100
                                FlagPeriodBefore = "S2"
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceBeforeS2)
                            End If
                            If CDec(RowsSelectDetail(i1)("TOTAL_PBQ3")) > 0 Then
                                'totalActualBefore += Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ4"))
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ3")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPPBQ3")) / 100))
                                totalInvoiceBeforeQ3 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ3"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPPBQ3")) / 100
                                FlagPeriodBefore &= "Q3"
                                If Descriptions.Length > 0 Then
                                    Descriptions &= ", "
                                End If
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceBeforeQ3)
                            End If
                            If CDec(RowsSelectDetail(i1)("TOTAL_PBQ4")) > 0 Then
                                'totalActualBefore += Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ4"))
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ4")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPPBQ4")) / 100))
                                totalInvoiceBeforeQ4 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBQ4"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPPBQ4")) / 100
                                FlagPeriodBefore &= "Q4"
                                If Descriptions.Length > 0 Then
                                    Descriptions &= ", "
                                End If
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceBeforeQ4)
                            End If
                            TotalPBy = totalInvoiceBeforeS2 + totalInvoiceBeforeQ3 + totalInvoiceBeforeQ4
                        Case "S2"
                            If CDec(RowsSelectDetail(i1)("TOTAL_CPS1")) > 0 Then
                                'totalActualBefore += Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPS1"))
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPS1")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPCPS1")) / 100))
                                totalInvoiceCurrentS1 = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_CPS1"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPCPS1")) / 100
                                FlagPeriodBefore = "S1"
                                Descriptions &= String.Format("{0:p} of " & FlagPeriodBefore & ", " & FlagPeriodBefore & " = {1:#,##0.000} Qty", givenProgressive, totalInvoiceCurrentS1)
                            End If
                            Me.FillPeriodeBeforeYearDetail(Flag, RowsSelectDetail, TotalPBy, DistributorID, AgreementNo, BrandID)
                        Case "Y"
                            If CDec(RowsSelectDetail(i1)("TOTAL_PBY")) > 0 Then
                                BonusQtyBefore += (Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBY")) * (Convert.ToDecimal(RowsSelectHeader(i)("GPPBY")) / 100))
                                totalActualBefore = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_PBY"))
                                givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPPBY")) / 100
                                FlagPeriodBefore = "Y"
                            End If
                    End Select
                    If Flag <> "Y" Then
                        If TotalPBy > 0 Then
                            givenProgressive = Convert.ToDecimal(RowsSelectHeader(i)("GPPBY")) / 100
                            BonusQtyBefore += (TotalPBy * givenProgressive)
                            If Descriptions.Length > 0 Then
                                Descriptions &= " ,"
                            End If
                            Descriptions &= String.Format("{0:p} of Y, Y = {1:#,##0.000} Qty", givenProgressive, TotalPBy)
                        End If
                    End If
                    Dim TotalActual As Decimal = Convert.ToDecimal(RowsSelectDetail(i1)("TOTAL_ACTUAL"))
                    Dim BonusQTy As Decimal = 0, LeftQty As Decimal = 0
                    BonusQTy = (Dispro / 100) * TotalActual
                    BonusQTy += BonusQtyBefore

                    Dim MustUpdateTPBY = Me.mustUpdateTotalPBY(Me.tblAcrDetailYear, rowID, Flag)
                    Dim Row As DataRow = RowsSelectDetail(i1) : Row.BeginEdit()
                    Row("DISC_QTY") = BonusQTy
                    Row("DISC_BY_VOLUME") = BonusQTy
                    'Row("RELEASE_QTY") = 0
                    'Row("LEFT_QTY") = BonusQTy

                    Row("DESCRIPTIONS") = Descriptions
                    If Flag <> "Y" Then
                        If MustUpdateTPBY Then
                            Row("TOTAL_PBY") = TotalPBy
                        End If
                    End If
                    Row.EndEdit()
                Next
            Next
            tblAcrDetail.AcceptChanges()
        End Sub
        Private Sub CreateOrRecreatetblCombined(ByRef tblComb As DataTable)
            If tblComb Is Nothing Then
                tblComb = New DataTable("T_Comb")
            End If
            With tblComb
                If Not IsNothing(tblComb) Then
                    .Clear()
                End If
                .Columns.Add(New DataColumn("ACHIEVEMENT_ID", Type.GetType("System.String")))
                .Columns.Add(New DataColumn("AGREE_BRAND_ID", Type.GetType("System.String")))
                .Columns.Add(New DataColumn("TOTAL_AGREE_BRAND", Type.GetType("System.String")))
                .Columns.Add(New DataColumn("COMB_AGREE_BRAND_ID", Type.GetType("System.String")))
                .Columns.Add(New DataColumn("TOTAL_COMB_AGREE_BRAND_ID", Type.GetType("System.Decimal")))
            End With
        End Sub
        Private Sub GetTotalActual(ByVal Flag As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal Target As Decimal, ByRef OutPutTotalActual As Decimal)
            Dim TargetQ1B1 As Decimal = 0, TargetQ1B2 As Decimal = 0, TargetQ1B3 As Decimal = 0, _
                TargetQ2B1 As Decimal = 0, TargetQ2B2 As Decimal = 0, TargetQ2B3 As Decimal = 0, TargetQ3B1 As Decimal = 0, _
                TargetQ3B2 As Decimal = 0, TargetQ3B3 As Decimal = 0, TargetQ4B1 As Decimal = 0, TargetQ4B2 As Decimal = 0, _
                TargetQ4B3 As Decimal = 0, TargetS1B1 As Decimal = 0, TargetS1B2 As Decimal = 0, _
                TargetS1B3 As Decimal = 0, TargetS1B4 As Decimal = 0, TargetS1B5 As Decimal = 0, TargetS1B6 As Decimal = 0, _
                TargetS2B1 As Decimal = 0, TargetS2B2 As Decimal = 0, TargetS2B3 As Decimal = 0, TargetS2B4 As Decimal = 0, _
                TargetS2B5 As Decimal = 0, TargetS2B6 As Decimal = 0, _
                StartDateQ1B1 As DateTime = Nothing, EndDateQ1B1 As DateTime = Nothing, _
                StartDateQ1B2 As DateTime = Nothing, EndDateQ1B2 As DateTime = Nothing, _
                StartDateQ1B3 As DateTime = Nothing, EndDateQ1B3 As DateTime = Nothing, _
                StartDateQ2B1 As DateTime = Nothing, EndDateQ2B1 As DateTime = Nothing, _
                StartDateQ2B2 As DateTime = Nothing, EndDateQ2B2 As DateTime = Nothing, _
                StartDateQ2B3 As DateTime = Nothing, EndDateQ2B3 As DateTime = Nothing, _
                StartDateQ3B1 As DateTime = Nothing, EndDateQ3B1 As DateTime = Nothing, _
                StartDateQ3B2 As DateTime = Nothing, EndDateQ3B2 As DateTime = Nothing, _
                StartDateQ3B3 As DateTime = Nothing, EndDateQ3B3 As DateTime = Nothing, _
                StartDateQ4B1 As DateTime = Nothing, EndDateQ4B1 As DateTime = Nothing, _
                StartDateQ4B2 As DateTime = Nothing, EndDateQ4B2 As DateTime = Nothing, _
                StartDateQ4B3 As DateTime = Nothing, EndDateQ4B3 As DateTime = Nothing, _
                StartDateS1B1 As DateTime = Nothing, EndDateS1B1 As DateTime = Nothing, _
                StartDateS1B2 As DateTime = Nothing, EndDateS1B2 As DateTime = Nothing, _
                StartDateS1B3 As DateTime = Nothing, EndDateS1B3 As DateTime = Nothing, _
                StartDateS1B4 As DateTime = Nothing, EndDateS1B4 As DateTime = Nothing, _
                StartDateS1B5 As DateTime = Nothing, EndDateS1B5 As DateTime = Nothing, _
                StartDateS1B6 As DateTime = Nothing, EndDateS1B6 As DateTime = Nothing, _
                StartDateS2B1 As DateTime = Nothing, EndDateS2B1 As DateTime = Nothing, _
                StartDateS2B2 As DateTime = Nothing, EndDateS2B2 As DateTime = Nothing, _
                StartDateS2B3 As DateTime = Nothing, EndDateS2B3 As DateTime = Nothing, _
                StartDateS2B4 As DateTime = Nothing, EndDateS2B4 As DateTime = Nothing, _
                StartDateS2B5 As DateTime = Nothing, EndDateS2B5 As DateTime = Nothing, _
                StartDateS2B6 As DateTime = Nothing, EndDateS2B6 As DateTime = Nothing, _
                CurrentDate As DateTime = NufarmBussinesRules.SharedClass.ServerDate, _
                TargetPerMonth As Decimal = 0
            If Flag.Remove(1, 1) = "Q" Then
                TargetPerMonth = Target / 3
                TargetQ1B1 = TargetPerMonth : TargetQ1B2 = TargetPerMonth : TargetQ1B3 = TargetPerMonth
                TargetQ2B1 = TargetPerMonth : TargetQ2B2 = TargetPerMonth : TargetQ2B3 = TargetPerMonth
                TargetQ3B1 = TargetPerMonth : TargetQ3B2 = TargetPerMonth : TargetQ3B3 = TargetPerMonth
                TargetQ4B1 = TargetPerMonth : TargetQ4B2 = TargetPerMonth : TargetQ4B3 = TargetPerMonth
            ElseIf Flag.Remove(1, 1) = "S" Then
                TargetPerMonth = Target / 6
                TargetS1B1 = TargetPerMonth : TargetS1B2 = TargetPerMonth : TargetS1B3 = TargetPerMonth
                TargetS1B4 = TargetPerMonth : TargetS1B5 = TargetPerMonth : TargetS1B6 = TargetPerMonth
                TargetS2B1 = TargetPerMonth : TargetS2B2 = TargetPerMonth : TargetS2B3 = TargetPerMonth
                TargetS2B4 = TargetPerMonth : TargetS2B5 = TargetPerMonth : TargetS2B6 = TargetPerMonth
            End If
            Select Case Flag
                Case "Q1"
                    StartDateQ1B1 = StartDate : EndDateQ1B1 = StartDate.AddMonths(1).AddDays(-1)
                    StartDateQ1B2 = EndDateQ1B1.AddDays(1) : EndDateQ1B2 = StartDateQ1B2.AddMonths(1).AddDays(-1)
                    StartDateQ1B3 = EndDateQ1B2.AddDays(1) : EndDateQ1B3 = EndDate
                    'tentukan sekarang masuk di periode mana dan bulan apa
                    If CurrentDate >= StartDateQ1B1 And CurrentDate <= EndDateQ1B1 Then
                        OutPutTotalActual += (TargetQ1B2 + TargetQ1B3)
                    ElseIf CurrentDate >= StartDateQ1B2 And CurrentDate <= EndDateQ1B2 Then
                        OutPutTotalActual += TargetQ1B3
                    ElseIf CurrentDate < StartDateQ1B1 Then
                        OutPutTotalActual += (TargetQ1B1 + TargetQ1B2 + TargetQ1B3)
                    End If
                Case "Q2"
                    StartDateQ2B1 = StartDate : EndDateQ2B1 = StartDate.AddMonths(1).AddDays(-1)
                    StartDateQ2B2 = EndDateQ2B1.AddDays(1) : EndDateQ2B2 = StartDateQ2B2.AddMonths(1).AddDays(-1)
                    StartDateQ2B3 = EndDateQ2B2.AddDays(1) : EndDateQ2B3 = EndDate
                    'tentukan sekarang masuk di periode mana dan bulan apa
                    If CurrentDate >= StartDateQ2B1 And CurrentDate <= EndDateQ2B1 Then
                        OutPutTotalActual += (TargetQ2B2 + TargetQ2B3)
                    ElseIf CurrentDate >= StartDateQ2B2 And CurrentDate <= EndDateQ2B2 Then
                        OutPutTotalActual += TargetQ2B3
                    ElseIf CurrentDate < StartDateQ2B1 Then
                        OutPutTotalActual += (TargetQ2B1 + TargetQ2B2 + TargetQ2B3)
                    End If
                Case "Q3"
                    StartDateQ3B1 = StartDate : EndDateQ3B1 = StartDate.AddMonths(1).AddDays(-1)
                    StartDateQ3B2 = EndDateQ3B1.AddDays(1) : EndDateQ3B2 = StartDateQ3B2.AddMonths(1).AddDays(-1)
                    StartDateQ3B3 = EndDateQ3B2.AddDays(1) : EndDateQ3B3 = EndDate
                    'tentukan sekarang masuk di periode mana dan bulan apa
                    If CurrentDate >= StartDateQ3B1 And CurrentDate <= EndDateQ3B1 Then
                        OutPutTotalActual += (TargetQ3B2 + TargetQ3B3)
                    ElseIf CurrentDate >= StartDateQ3B2 And CurrentDate <= EndDateQ3B2 Then
                        OutPutTotalActual += TargetQ3B3
                    ElseIf CurrentDate < StartDateQ3B1 Then
                        OutPutTotalActual += (TargetQ3B1 + TargetQ3B2 + TargetQ3B3)
                    End If
                Case "Q4"
                    StartDateQ4B1 = StartDate : EndDateQ4B1 = StartDate.AddMonths(1).AddDays(-1)
                    StartDateQ4B2 = EndDateQ4B1.AddDays(1) : EndDateQ4B2 = StartDateQ4B2.AddMonths(1).AddDays(-1)
                    StartDateQ4B3 = EndDateQ4B2.AddDays(1) : EndDateQ4B3 = EndDate
                    'tentukan sekarang masuk di periode mana dan bulan apa
                    If CurrentDate >= StartDateQ4B1 And CurrentDate <= EndDateQ4B1 Then
                        OutPutTotalActual += (TargetQ4B2 + TargetQ4B3)
                    ElseIf CurrentDate >= StartDateQ4B2 And CurrentDate <= EndDateQ4B2 Then
                        OutPutTotalActual += TargetQ4B3
                    ElseIf CurrentDate < StartDateQ4B1 Then
                        OutPutTotalActual += (TargetQ4B1 + TargetQ4B2 + TargetQ4B3)
                    End If
                Case "S1"
                    StartDateS1B1 = StartDate : EndDateS1B1 = StartDate.AddMonths(1).AddDays(-1)
                    StartDateS1B2 = EndDateS1B1.AddDays(1) : EndDateS1B2 = StartDateS1B2.AddMonths(1).AddDays(-1)
                    StartDateS1B3 = EndDateS1B2.AddDays(1) : EndDateS1B3 = StartDateS1B3.AddMonths(1).AddDays(-1)
                    StartDateS1B4 = EndDateS1B3.AddDays(1) : EndDateS1B4 = StartDateS1B4.AddMonths(1).AddDays(-1)
                    StartDateS1B5 = EndDateS1B4.AddDays(1) : EndDateS1B5 = StartDateS1B5.AddMonths(1).AddDays(-1)
                    StartDateS1B6 = EndDateS1B5.AddDays(1) : EndDateS1B6 = StartDateS1B6.AddMonths(1).AddDays(-1)
                    If CurrentDate >= StartDateS1B1 And CurrentDate <= EndDateS1B1 Then
                        OutPutTotalActual += (TargetS1B2 + TargetS1B3 + TargetS1B4 + TargetS1B5 + TargetS1B6)
                    ElseIf CurrentDate >= StartDateS1B2 And CurrentDate <= EndDateS1B2 Then
                        OutPutTotalActual += (TargetS1B3 + TargetS1B4 + TargetS1B5 + TargetS1B6)
                    ElseIf CurrentDate >= StartDateS1B3 And CurrentDate <= EndDateS1B3 Then
                        OutPutTotalActual += (TargetS1B4 + TargetS1B5 + TargetS1B6)
                    ElseIf CurrentDate >= StartDateS1B4 And CurrentDate <= EndDateS1B4 Then
                        OutPutTotalActual += (TargetS1B5 + TargetS1B6)
                    ElseIf CurrentDate >= StartDateS1B5 And CurrentDate <= EndDateS1B5 Then
                        OutPutTotalActual += (TargetS1B6)
                    ElseIf CurrentDate < StartDateS1B1 Then
                        OutPutTotalActual += (TargetS1B1 + TargetS1B2 + TargetS1B3 + TargetS1B4 + TargetS1B5 + TargetS1B6)
                    End If
                Case "S2"
                    StartDateS2B1 = StartDate : EndDateS2B1 = StartDate.AddMonths(1).AddDays(-1)
                    StartDateS2B2 = EndDateS2B1.AddDays(1) : EndDateS2B2 = StartDateS2B2.AddMonths(1).AddDays(-1)
                    StartDateS2B3 = EndDateS2B2.AddDays(1) : EndDateS2B3 = StartDateS2B3.AddMonths(1).AddDays(-1)
                    StartDateS2B4 = EndDateS2B3.AddDays(1) : EndDateS2B4 = StartDateS2B4.AddMonths(1).AddDays(-1)
                    StartDateS2B5 = EndDateS2B4.AddDays(1) : EndDateS2B5 = StartDateS2B5.AddMonths(1).AddDays(-1)
                    StartDateS2B6 = EndDateS2B5.AddDays(1) : EndDateS2B6 = StartDateS2B6.AddMonths(1).AddDays(-1)

                    If CurrentDate >= StartDateS2B1 And CurrentDate <= EndDateS2B1 Then
                        OutPutTotalActual += (TargetS2B2 + TargetS2B3 + TargetS2B4 + TargetS2B5 + TargetS2B6)
                    ElseIf CurrentDate >= StartDateS2B2 And CurrentDate <= EndDateS2B2 Then
                        OutPutTotalActual += (TargetS2B3 + TargetS2B4 + TargetS2B5 + TargetS2B6)
                    ElseIf CurrentDate >= StartDateS2B3 And CurrentDate <= EndDateS2B3 Then
                        OutPutTotalActual += (TargetS2B4 + TargetS2B5 + TargetS2B6)
                    ElseIf CurrentDate >= StartDateS2B4 And CurrentDate <= EndDateS2B4 Then
                        OutPutTotalActual += (TargetS2B5 + TargetS2B6)
                    ElseIf CurrentDate >= StartDateS2B5 And CurrentDate <= EndDateS2B5 Then
                        OutPutTotalActual += (TargetS1B6)
                    ElseIf CurrentDate < StartDateS2B1 Then
                        OutPutTotalActual += (TargetS2B1 + TargetS2B2 + TargetS2B3 + TargetS2B4 + TargetS2B5 + TargetS2B6)
                    End If
                Case "Y"
                    'hitung total actual dari q1 + q2 + q3 + q4 bila 'q'
                    'hitung total actual dari s1 + s2 bila 's'

            End Select
        End Sub
        Private Sub setTotalInvoiceBefore(ByVal Flag As String, ByVal RowsSelect() As DataRow, ByVal RowsSelect1() As DataRow, ByRef tblListBonusBefore As DataTable, ByRef Row As DataRow, _
        ByRef DescriptionTotal As String, ByRef Description As String, ByRef BonusQty As Decimal, ByRef Rows() As DataRow)
            Dim totalInvoiceBeforeQ3 As Decimal = 0, totalInvoiceBeforeQ4 As Decimal = 0, totalInvoiceBeforeS2 As Decimal = 0, _
          totalInvoiceCurrentQ1 As Decimal = 0, totalInvoiceCurrentQ2 As Decimal = 0, totalInvoiceCurrentQ3 As Decimal = 0, totalInvoiceCurrentS1 As Decimal = 0, TotalInvoiceBeforeY As Decimal = 0, TotalInvoiceBeforeY1 As Decimal = 0
            Dim TotalJoinPBy As Decimal = 0, TotalJoinPBy1 As Decimal = 0
            Dim rowsCheck() As DataRow = Nothing
            Select Case Flag
                Case "Q1"
                    If CDec(RowsSelect(0)("TOTAL_JOIN_PBQ3")) > 0 Then
                        totalInvoiceBeforeQ3 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ3"))
                        DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q3", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ3")))
                        Row = tblListBonusBefore.NewRow()
                        Row("PB") = "TOTAL_PBQ3"
                        Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                        Row.EndEdit()
                        tblListBonusBefore.Rows.Add(Row)
                        'TotalJoinPBy += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ3"))
                    End If
                    If CDec(RowsSelect(0)("TOTAL_JOIN_PBQ4")) > 0 Then
                        TotalInvoiceBeforeQ4 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4"))
                        If DescriptionTotal <> "" Then
                            DescriptionTotal &= ", "
                        End If
                        DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q4", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4")))
                        Row = tblListBonusBefore.NewRow()
                        Row("PB") = "TOTAL_PBQ4"
                        Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                        Row.EndEdit()
                        tblListBonusBefore.Rows.Add(Row)
                        'TotalJoinPBy += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4"))
                    Else
                        If CDec(RowsSelect(0)("TOTAL_JOIN_PBS2")) > 0 Then
                            TotalInvoiceBeforeS2 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBS2"))
                            If DescriptionTotal <> "" Then
                                DescriptionTotal &= ", "
                            End If
                            DescriptionTotal &= String.Format("{0:#,##0.000} Qty S2", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBS2")))
                            Row = tblListBonusBefore.NewRow()
                            Row("PB") = "TOTAL_PBS2"
                            Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                            Row.EndEdit()
                            tblListBonusBefore.Rows.Add(Row)
                            'TotalJoinPBy += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBS2"))
                        End If
                    End If
                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            If CDec(RowsSelect1(0)("TOTAL_JOIN_PBQ3")) > 0 Then
                                totalInvoiceBeforeQ3 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ3"))
                                DescriptionTotal &= ", "
                                DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q3,", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ3")))
                                Row = tblListBonusBefore.NewRow()
                                Row("PB") = "TOTAL_PBQ3"
                                Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                Row.EndEdit()
                                tblListBonusBefore.Rows.Add(Row)
                                'TotalJoinPBy += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ3"))
                            End If
                            If CDec(RowsSelect1(0)("TOTAL_JOIN_PBQ4")) > 0 Then
                                totalInvoiceBeforeQ4 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ4"))
                                DescriptionTotal &= ", "
                                DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q4,", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ4")))
                                Row = tblListBonusBefore.NewRow()
                                Row("PB") = "TOTAL_PBQ4"
                                Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                Row.EndEdit()
                                tblListBonusBefore.Rows.Add(Row)
                                'TotalJoinPBy += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ3"))
                            Else
                                If CDec(RowsSelect1(0)("TOTAL_JOIN_PBS2")) > 0 Then
                                    totalInvoiceBeforeS2 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBS2"))
                                    DescriptionTotal &= ", "
                                    DescriptionTotal &= String.Format("{0:#,##0.000} Qty S2", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBS2")))
                                    Row = tblListBonusBefore.NewRow()
                                    Row("PB") = "TOTAL_PBS2"
                                    Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                    Row.EndEdit()
                                    tblListBonusBefore.Rows.Add(Row)
                                    'TotalJoinPBy += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBS2"))
                                End If
                            End If
                        End If
                    End If
                    If totalInvoiceBeforeQ3 > 0 Then
                        If (CDec(RowsSelect(0)("GPPBQ3")) > 0) Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBQ3")) / 100) * totalInvoiceBeforeQ3
                            Description &= String.Format("{0:p} GP of Q3", Convert.ToDecimal(RowsSelect(0)("GPPBQ3")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBQ3")
                        End If
                        'Else
                    End If
                    If totalInvoiceBeforeQ4 > 0 Then
                        If (CDec(RowsSelect(0)("GPPBQ4")) > 0) Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBQ4")) / 100) * totalInvoiceBeforeQ4
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("{0:p} GP of Q4", Convert.ToDecimal(RowsSelect(0)("GPPBQ4")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBQ4")
                        End If
                    End If
                    If totalInvoiceBeforeS2 > 0 Then
                        If CDec(RowsSelect(0)("GPPBS2")) > 0 Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBS2")) / 100) * totalInvoiceBeforeS2
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("{0:p} GP of S2", Convert.ToDecimal(RowsSelect(0)("GPPBS2")) / 100)
                            Description &= ","

                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBS2")
                        End If
                    End If
                    Description &= " " & DescriptionTotal
                Case "Q2"
                    If CDec(RowsSelect(0)("TOTAL_JOIN_PBQ4")) > 0 Then
                        totalInvoiceBeforeQ4 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4"))
                        DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q4", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4")))
                        Row = tblListBonusBefore.NewRow()
                        Row("PB") = "TOTAL_PBQ4"
                        Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                        Row.EndEdit()
                        tblListBonusBefore.Rows.Add(Row)
                        'chek ke tabelyearheader
                        'TotalJoinPBy += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4"))
                    End If
                    If CDec(RowsSelect(0)("TOTAL_JOIN_CPQ1")) > 0 Then
                        totalInvoiceCurrentQ1 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPQ1"))
                        If DescriptionTotal <> "" Then
                            DescriptionTotal &= ", "
                        End If
                        DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q1", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPQ1")))
                        Row = tblListBonusBefore.NewRow()
                        Row("PB") = "TOTAL_CPQ1"
                        Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                        Row.EndEdit()
                        tblListBonusBefore.Rows.Add(Row)

                    End If
                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            If CDec(RowsSelect1(0)("TOTAL_JOIN_PBQ4")) > 0 Then
                                totalInvoiceBeforeQ4 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ4"))
                                DescriptionTotal &= ", "
                                DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q4,", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ4")))
                                Row = tblListBonusBefore.NewRow()
                                Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                Row("PB") = "TOTAL_PBQ4"
                                Row.EndEdit()
                                tblListBonusBefore.Rows.Add(Row)
                                'TotalJoinPBy += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ4"))
                            End If
                            If CDec(RowsSelect1(0)("TOTAL_JOIN_CPQ1")) > 0 Then
                                totalInvoiceCurrentQ1 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPQ1"))
                                DescriptionTotal &= ", "
                                DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q1,", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPQ1")))
                                Row = tblListBonusBefore.NewRow()
                                Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                Row("PB") = "TOTAL_CPQ1"
                                Row.EndEdit()
                                tblListBonusBefore.Rows.Add(Row)
                            End If
                        End If
                    End If
                    If totalInvoiceBeforeQ4 > 0 Then
                        If (CDec(RowsSelect(0)("GPPBQ4")) > 0) Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBQ4")) / 100) * totalInvoiceBeforeQ4
                            Description &= String.Format("{0:p} GP of Q4", Convert.ToDecimal(RowsSelect(0)("GPPBQ4")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBQ4")
                        End If
                    End If
                    If totalInvoiceCurrentQ1 > 0 Then
                        If (CDec(RowsSelect(0)("GPCPQ1")) > 0) Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPCPQ1")) / 100) * totalInvoiceCurrentQ1
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("{0:p} GP of Q1", Convert.ToDecimal(RowsSelect(0)("GPCPQ1")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPCPQ1")
                        End If
                    End If
                    Description &= " " & DescriptionTotal
                Case "Q3"
                    If CDec(RowsSelect(0)("TOTAL_JOIN_CPQ1")) > 0 Then
                        totalInvoiceCurrentQ1 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPQ1"))
                        DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q1", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPQ1")))
                        Row = tblListBonusBefore.NewRow()
                        Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                        Row("PB") = "TOTAL_CPQ1"
                        Row.EndEdit()
                        tblListBonusBefore.Rows.Add(Row)
                    End If
                    If CDec(RowsSelect(0)("TOTAL_JOIN_CPQ2")) > 0 Then
                        totalInvoiceCurrentQ2 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPQ2"))
                        If DescriptionTotal <> "" Then
                            DescriptionTotal &= ", "
                        End If
                        DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q2", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPQ2")))
                        Row = tblListBonusBefore.NewRow()
                        Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                        Row("PB") = "TOTAL_CPQ2"
                        Row.EndEdit()
                        tblListBonusBefore.Rows.Add(Row)
                    End If

                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            If CDec(RowsSelect1(0)("TOTAL_JOIN_CPQ1")) > 0 Then
                                totalInvoiceCurrentQ1 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPQ1"))
                                DescriptionTotal &= ", "
                                DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q1,", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPQ1")))
                                Row = tblListBonusBefore.NewRow()
                                Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                Row("PB") = "TOTAL_CPQ1"
                                Row.EndEdit()
                                tblListBonusBefore.Rows.Add(Row)
                            End If
                            If CDec(RowsSelect1(0)("TOTAL_JOIN_CPQ2")) > 0 Then
                                totalInvoiceCurrentQ2 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPQ2"))
                                DescriptionTotal &= ", "
                                DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q2,", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPQ2")))
                                Row = tblListBonusBefore.NewRow()
                                Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                Row("PB") = "TOTAL_CPQ2"
                                Row.EndEdit()
                                tblListBonusBefore.Rows.Add(Row)
                            End If
                        End If
                    End If
                    If totalInvoiceCurrentQ1 > 0 Then
                        If (CDec(RowsSelect(0)("GPCPQ1")) > 0) Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPCPQ1")) / 100) * totalInvoiceCurrentQ1
                            Description &= String.Format("{0:p} GP of Q1", Convert.ToDecimal(RowsSelect(0)("GPCPQ1")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPCPQ1")
                        End If
                    End If
                    If totalInvoiceCurrentQ2 > 0 Then
                        If (CDec(RowsSelect(0)("GPCPQ2")) > 0) Then
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPCPQ2")) / 100) * totalInvoiceCurrentQ2
                            Description &= String.Format("{0:p} GP of Q2", Convert.ToDecimal(RowsSelect(0)("GPCPQ2")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPCPQ2")
                        End If
                    End If
                    Description &= " " & DescriptionTotal
                Case "Q4"

                    If CDec(RowsSelect(0)("TOTAL_JOIN_CPQ2")) > 0 Then
                        totalInvoiceCurrentQ2 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPQ2"))
                        DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q2", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPQ2")))
                        Row = tblListBonusBefore.NewRow()
                        Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                        Row("PB") = "TOTAL_CPQ2"
                        Row.EndEdit()
                        tblListBonusBefore.Rows.Add(Row)
                    End If
                    If CDec(RowsSelect(0)("TOTAL_JOIN_CPQ3")) > 0 Then
                        totalInvoiceCurrentQ3 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPQ3"))
                        If DescriptionTotal <> "" Then
                            DescriptionTotal &= ", "
                        End If
                        DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q3", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPQ3")))
                        Row = tblListBonusBefore.NewRow()
                        Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                        Row("PB") = "TOTAL_CPQ3"
                        Row.EndEdit()
                        tblListBonusBefore.Rows.Add(Row)
                    End If
                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            If CDec(RowsSelect1(0)("TOTAL_JOIN_CPQ2")) > 0 Then
                                totalInvoiceCurrentQ2 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPQ2"))
                                DescriptionTotal &= ", "
                                DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q2,", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPQ2")))
                                Row = tblListBonusBefore.NewRow()
                                Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                Row("PB") = "TOTAL_CPQ2"
                                Row.EndEdit()
                                tblListBonusBefore.Rows.Add(Row)
                            End If
                            If CDec(RowsSelect1(0)("TOTAL_JOIN_CPQ3")) > 0 Then
                                totalInvoiceCurrentQ3 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPQ3"))
                                DescriptionTotal &= ", "
                                DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q3,", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPQ3")))
                                Row = tblListBonusBefore.NewRow()
                                Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                Row("PB") = "TOTAL_CPQ3"
                                Row.EndEdit()
                                tblListBonusBefore.Rows.Add(Row)
                            End If
                        End If
                        'rowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                        'If rowsCheck.Length > 0 Then
                        '    TTotalPBQ4 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ4)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                        '    TotalJoinPBy += TTotalPBQ4
                        '    TTotalPBS2 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBS2)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                        '    TotalJoinPBy += TTotalPBS2
                        'End If
                    End If
                    If totalInvoiceCurrentQ2 > 0 Then
                        If (CDec(RowsSelect(0)("GPCPQ2")) > 0) Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPCPQ2")) / 100) * totalInvoiceCurrentQ2
                            Description &= String.Format("{0:p} GP of Q2", Convert.ToDecimal(RowsSelect(0)("GPCPQ2")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPCPQ2")
                        End If
                    End If
                    If totalInvoiceCurrentQ3 > 0 Then
                        If (CDec(RowsSelect(0)("GPCPQ3")) > 0) Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPCPQ3")) / 100) * totalInvoiceCurrentQ3
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("{0:p} GP of Q3", Convert.ToDecimal(RowsSelect(0)("GPCPQ3")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPCPQ3")
                        End If
                    End If
                    Description &= " " & DescriptionTotal
                Case "S1"
                    'Dim TTotalPBQ3 As Decimal = 0, TTotalPBQ4 As Decimal = 0, TTotalPBS2 As Decimal = 0
                    If CDec(RowsSelect(0)("TOTAL_JOIN_PBS2")) > 0 Then
                        totalInvoiceBeforeS2 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBS2"))
                        DescriptionTotal &= String.Format("{0:#,##0.000} Qty S2", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBS2")))
                        Row = tblListBonusBefore.NewRow()
                        Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                        Row("PB") = "TOTAL_PBS2"
                        Row.EndEdit()
                        tblListBonusBefore.Rows.Add(Row)
                        'TTotalPBS2 = Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBS2"))
                        'TotalJoinPBy += TTotalPBS2
                    Else
                        If CDec(RowsSelect(0)("TOTAL_JOIN_PBQ4")) > 0 Then
                            totalInvoiceBeforeQ4 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4"))
                            DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q4", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4")))
                            Row = tblListBonusBefore.NewRow()
                            Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                            Row("PB") = "TOTAL_PBQ4"
                            Row.EndEdit()
                            tblListBonusBefore.Rows.Add(Row)
                            'TTotalPBQ4 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ4"))
                            'TotalJoinPBy += TTotalPBQ4
                        End If
                        If CDec(RowsSelect(0)("TOTAL_JOIN_PBQ3")) > 0 Then
                            totalInvoiceBeforeQ3 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ3"))
                            If DescriptionTotal <> "" Then
                                DescriptionTotal &= ", "
                            End If
                            DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q3", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ3")))
                            Row = tblListBonusBefore.NewRow()
                            Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                            Row("PB") = "TOTAL_PBQ3"
                            Row.EndEdit()
                            tblListBonusBefore.Rows.Add(Row)
                            'TTotalPBQ3 = Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBQ3")))
                            'TotalJoinPBy += TTotalPBQ3
                        End If
                    End If
                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            If CDec(RowsSelect1(0)("TOTAL_JOIN_PBS2")) > 0 Then
                                totalInvoiceBeforeS2 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBS2"))
                                DescriptionTotal &= ", "
                                DescriptionTotal &= String.Format("{0:#,##0.000} Qty S2,", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBS2")))
                                Row = tblListBonusBefore.NewRow()
                                Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                Row("PB") = "TOTAL_PBS2"
                                Row.EndEdit()
                                tblListBonusBefore.Rows.Add(Row)
                                'TTotalPBS2 = Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBS2"))
                                'TotalJoinPBy += TTotalPBS2
                            Else
                                If CDec(RowsSelect1(0)("TOTAL_JOIN_PBQ3")) > 0 Then
                                    totalInvoiceBeforeQ3 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ3"))
                                    DescriptionTotal &= ", "
                                    DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q3", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ3")))
                                    Row = tblListBonusBefore.NewRow()
                                    Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                    Row("PB") = "TOTAL_PBQ3"
                                    Row.EndEdit()
                                    tblListBonusBefore.Rows.Add(Row)
                                    'TTotalPBQ3 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ3"))
                                    'TotalJoinPBy += TTotalPBQ3
                                End If
                                If CDec(RowsSelect1(0)("TOTAL_JOIN_PBQ4")) > 0 Then
                                    totalInvoiceBeforeQ4 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ4"))
                                    DescriptionTotal &= ", "
                                    DescriptionTotal &= String.Format("{0:#,##0.000} Qty Q4", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ4")))
                                    Row = tblListBonusBefore.NewRow()
                                    Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                    Row("PB") = "TOTAL_PBQ4"
                                    Row.EndEdit()
                                    tblListBonusBefore.Rows.Add(Row)
                                    'TTotalPBQ4 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBQ4"))
                                    'TotalJoinPBy += TTotalPBQ4
                                End If
                            End If
                        End If
                    End If
                    If totalInvoiceBeforeS2 > 0 Then
                        If (CDec(RowsSelect(0)("GPPBS2")) > 0) Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBS2")) / 100) * totalInvoiceBeforeS2
                            Description &= String.Format("{0:p} GP of S2", Convert.ToDecimal(RowsSelect(0)("GPPBS2")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBS2")
                        End If
                    End If
                    If totalInvoiceBeforeQ3 > 0 Then
                        If CDec(RowsSelect(0)("GPPBQ3")) > 0 Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBQ3")) / 100) * totalInvoiceBeforeQ3
                            Description &= String.Format("{0:p} GP of Q3", Convert.ToDecimal(RowsSelect(0)("GPPBQ3")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBQ3")
                        End If
                    End If
                    If totalInvoiceBeforeQ4 > 0 Then
                        If CDec(RowsSelect(0)("GPPBQ4")) > 0 Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBQ4")) / 100) * totalInvoiceBeforeQ4
                            Description &= String.Format("{0:p} GP of Q4", Convert.ToDecimal(RowsSelect(0)("GPPBQ4")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBQ4")
                        End If

                    End If
                    Description &= " " & DescriptionTotal
                Case "S2"                    'Dim TTotalPBQ4 As Decimal = 0, TTotalPBS2 As Decimal = 0, TTotalPBQ3 As Decimal = 0
                    If CDec(RowsSelect(0)("TOTAL_JOIN_CPS1")) > 0 Then
                        totalInvoiceCurrentS1 += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPS1"))
                        DescriptionTotal &= String.Format("{0:#,##0.000} Qty S1", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_CPS1")))
                        Row = tblListBonusBefore.NewRow()
                        Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                        Row("PB") = "TOTAL_CPS1"
                        Row.EndEdit()
                        tblListBonusBefore.Rows.Add(Row)
                        'rowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        'If rowsCheck.Length > 0 Then
                        '    TTotalPBQ3 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ3)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        '    TotalJoinPBy += TTotalPBQ3
                        '    TTotalPBQ4 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ4)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        '    TotalJoinPBy += TTotalPBQ4
                        '    TTotalPBS2 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBS2)", "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
                        '    TotalJoinPBy += TTotalPBS2
                        'End If
                    End If
                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            If CDec(RowsSelect1(0)("TOTAL_JOIN_CPS1")) > 0 Then
                                totalInvoiceCurrentS1 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPS1"))
                                DescriptionTotal &= ", "
                                DescriptionTotal &= String.Format("{0:#,##0.000} Qty S1,", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_CPS1")))
                                Row = tblListBonusBefore.NewRow()
                                Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                Row("PB") = "TOTAL_CPS1"
                                Row.EndEdit()
                                tblListBonusBefore.Rows.Add(Row)
                                'rowsCheck = Me.tblAcrHeaderYear.Select("AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                'If rowsCheck.Length > 0 Then
                                '    TTotalPBQ3 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ3)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                '    TotalJoinPBy += TTotalPBQ3
                                '    TTotalPBQ4 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBQ4)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                '    TotalJoinPBy += TTotalPBQ4
                                '    TTotalPBS2 = tblAcrHeaderYear.Compute("SUM(TOTAL_PBS2)", "AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                                '    TotalJoinPBy += TTotalPBS2
                                'End If
                            End If
                        End If
                    End If
                    If totalInvoiceCurrentS1 > 0 Then
                        If (CDec(RowsSelect(0)("GPCPS1")) > 0) Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPCPS1")) / 100) * totalInvoiceCurrentS1
                            Description &= String.Format("{0:p} GP of S1", Convert.ToDecimal(RowsSelect(0)("GPCPS1")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPCPS1")
                        End If
                        Description &= " " & DescriptionTotal
                    End If
                Case "Y"
                    If Not IsDBNull(RowsSelect(0)("TOTAL_JOIN_PBY")) And Not IsNothing(RowsSelect(0)("TOTAL_JOIN_PBY")) Then
                        If CDec(RowsSelect(0)("TOTAL_JOIN_PBY")) > 0 Then
                            TotalInvoiceBeforeY += Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBY"))
                            DescriptionTotal &= String.Format("{0:#,##0.000} Qty Y", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBY")))
                            Row = tblListBonusBefore.NewRow()
                            Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                            Row("PB") = "TOTAL_PBY"
                            Row.EndEdit()
                            tblListBonusBefore.Rows.Add(Row)
                        End If
                    End If
                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                        If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                            If Not IsDBNull(RowsSelect1(0)("TOTAL_JOIN_PBY")) And Not IsNothing(RowsSelect1(0)("TOTAL_JOIN_PBY")) Then
                                If CDec(RowsSelect1(0)("TOTAL_JOIN_PBY")) > 0 Then
                                    TotalInvoiceBeforeY1 += Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBY"))
                                    DescriptionTotal &= ", "
                                    DescriptionTotal &= String.Format("{0:#,##0.000} Qty Y", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBY")))
                                    Row = tblListBonusBefore.NewRow()
                                    Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                                    Row("PB") = "TOTAL_PBY"
                                    Row.EndEdit()
                                    tblListBonusBefore.Rows.Add(Row)
                                End If
                            End If
                        End If
                    End If
                    If TotalInvoiceBeforeY > 0 Then
                        If (CDec(RowsSelect(0)("GPPBY")) > 0) Then
                            BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBY")) / 100) * TotalInvoiceBeforeY
                            Description &= String.Format("{0:p} GP of Y", Convert.ToDecimal(RowsSelect(0)("GPPBY")) / 100)
                            Description &= ", "
                            Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBY")
                        End If
                    End If
                    If TotalInvoiceBeforeY1 > 0 Then
                        If (CDec(RowsSelect1(0)("GPPBY")) > 0) Then
                            Description &= ", "
                            BonusQty += (Convert.ToDecimal(RowsSelect1(0)("GPPBY")) / 100) * TotalInvoiceBeforeY1
                            Description &= String.Format("{0:p} GP of Y", Convert.ToDecimal(RowsSelect1(0)("GPPBY")) / 100)
                            'Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBY")
                        End If
                    End If

                    Description &= " " & DescriptionTotal
            End Select
            If Flag <> "Y" Then
                fillPerideBeforeYear(Flag, RowsSelect, rowsCheck, RowsSelect1, TotalJoinPBy, TotalJoinPBy1)
                If (TotalJoinPBy > 0) Then
                    BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBY")) / 100) * TotalJoinPBy
                    If Description.Length > 0 Then
                        Description &= ", "
                    End If
                    Description &= String.Format("{0:p} GP of Y", Convert.ToDecimal(RowsSelect(0)("GPPBY")) / 100)
                    Description &= ", "
                    Description &= String.Format("{0:#,##0.000} Qty Y", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBY")))
                    Row = tblListBonusBefore.NewRow()
                    Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
                    Row("PB") = "TOTAL_PBY"
                    Row.EndEdit()
                    tblListBonusBefore.Rows.Add(Row)
                    If Not IsDBNull(RowsSelect(0)("CombinedWith")) Then
                        BonusQty += (Convert.ToDecimal(RowsSelect1(0)("GPPBY")) / 100) * TotalJoinPBy1
                        If Description.Length > 0 Then
                            Description &= ", "
                        End If
                        Description &= String.Format("{0:p} GP of Y", Convert.ToDecimal(RowsSelect1(0)("GPPBY")) / 100)
                        Description &= ", "
                        Description &= String.Format("{0:#,##0.000} Qty Y", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBY")))

                        Row = tblListBonusBefore.NewRow()
                        Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
                        Row("PB") = "TOTAL_PBY"
                        Row.EndEdit()
                        tblListBonusBefore.Rows.Add(Row)
                    End If
                    Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBY")
                End If
            End If
            'TotalJoinPBy = Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBY"))
            'TotalJoinPBy1 = Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBY"))
            'If (TotalJoinPBy > 0) Then
            '    BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBY")) / 100) * TotalJoinPBy
            '    If Description.Length > 0 Then
            '        Description &= ", "
            '    End If
            '    Description &= String.Format("{0:p} GP of Y", Convert.ToDecimal(RowsSelect(0)("GPPBY")) / 100)
            '    Description &= ", "
            '    Description &= String.Format("{0:#,##0,000} Qty Y", Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBY")))
            '    Row = tblListBonusBefore.NewRow()
            '    Row("AGREE_BRAND_ID") = RowsSelect(0)("AGREE_BRAND_ID")
            '    Row("PB") = "TOTAL_PBY"
            '    Row.EndEdit()
            '    tblListBonusBefore.Rows.Add(Row)

            '    If Not IsDBNull(RowsSelect(0)("CombinedWith")) Then
            '        BonusQty += (Convert.ToDecimal(RowsSelect1(0)("GPPBY")) / 100) * TotalJoinPBy1
            '        If Description.Length > 0 Then
            '            Description &= ", "
            '        End If
            '        Description &= String.Format("{0:p} GP of Y", Convert.ToDecimal(RowsSelect1(0)("GPPBY")) / 100)
            '        Description &= ", "
            '        Description &= String.Format("{0:#,##0,000} Qty Y", Convert.ToDecimal(RowsSelect1(0)("TOTAL_JOIN_PBY")))

            '        Row = tblListBonusBefore.NewRow()
            '        Row("AGREE_BRAND_ID") = RowsSelect1(0)("AGREE_BRAND_ID")
            '        Row("PB") = "TOTAL_PBY"
            '        Row.EndEdit()
            '        tblListBonusBefore.Rows.Add(Row)
            '    End If
            '    Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBY")
            'End If
            'If (Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBY")) <= 0) Or (CBool(RowsSelect(0)("IsChanged")) = True) Then
            '    BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBY")) / 100) * TotalJoinPBy
            '    Description &= ", "
            '    Description &= String.Format("{0:p} GP of Y", Convert.ToDecimal(RowsSelect(0)("GPPBY")) / 100)
            '    Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBY")
            '    Description &= ", "
            '    DescriptionTotal &= ", "
            '    DescriptionTotal &= String.Format("{0:#,##0,000} Qty Y", TotalJoinPBy)
            'End If
            'If tblListBonusBefore.Rows.Count > 0 Then
            '    tblListBonusBefore.AcceptChanges()
            'End If
        End Sub

        Private Sub CalculateHeader(ByVal FLAG As String, ByRef ListAgreeBrand As List(Of String), ByVal CategoryIsNewOrIsChanged As String, ByRef tblAcrHeader As DataTable, _
        ByRef DVCustom As DataView, ByRef DVTypical As DataView, ByVal AgreementNO As String, ByRef Message As String, _
        ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal IsTargetGroup As Boolean)
            Dim RowsSelect() As DataRow = Nothing, RowSSelect1() As DataRow = Nothing
            Dim AchievementDispro As Decimal = 0, BonusQty As Decimal = 0, _
            Balance As Decimal = 0, Dispro As Decimal = 0 : Dim Row As DataRow = Nothing, Description As String = "", DescriptionTotal As String = ""

            RowsSelect = tblAcrHeader.Select(CategoryIsNewOrIsChanged & " = " & True)
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
            If DVCustom.Count > 0 Then ''yang masuk hanya flag q dan y untuk looping berikut karena custom
                Dim ListAgreeBrand1 As New List(Of String)
                For i As Integer = 0 To RowsSelect.Length - 1
                    If Not ListAgreeBrand1.Contains(RowsSelect(i)("AGREE_BRAND_ID").ToString()) Then
                        ListAgreeBrand1.Add(RowsSelect(i)("AGREE_BRAND_ID").ToString())
                    End If
                Next

                For i As Integer = 0 To ListAgreeBrand1.Count - 1
                    Dim Target As Decimal = 0, TotalPOOriginal As Decimal = 0, TotalActual As Decimal = 0, CombAgreeBrandID As String = "", IsFromAccHeader As Boolean = False
                    Dim TJoinPBYear As Decimal = 0, TPBYear As Decimal = 0, GPPBY As Decimal = 0, GPPBY1 As Decimal = 0, TPBYear1 As Decimal = 0, _
                    TJoinPBYear1 As Decimal = 0
                    Dispro = 0 : AchievementDispro = 0 : BonusQty = 0 : Balance = 0

                    tblListBonusBefore.Clear() : Description = "" : DescriptionTotal = ""
                    If Not ListAgreeBrand.Contains(ListAgreeBrand1(i)) Then
                        ListAgreeBrand.Add(ListAgreeBrand1(i))
                        RowsSelect = tblAcrHeader.Select(CategoryIsNewOrIsChanged & " = " & True & " AND AGREE_BRAND_ID = '" & ListAgreeBrand1(i).ToString() & "'")
                        ''ambil totalactualnya
                        If Not IsDBNull(RowsSelect(0)("IsFromAccHeader")) And Not IsNothing(RowsSelect(0)("IsFromAccHeader")) Then
                            IsFromAccHeader = CBool(RowsSelect(0)("IsFromAccHeader"))
                        End If
                        If Not RowsSelect.Length <= 0 Then

                            'If (RowsSelect(0)("BRAND_ID") = "77240" Or RowsSelect(0)("BRAND_ID") = "77230") Then
                            '    Stop
                            'End If

                            If Not mustRecomputeYear And Not IsFromAccHeader And Not (FLAG = "Y") Then
                                TotalActual = Convert.ToDecimal(RowsSelect(0)("TOTAL_ACTUAL"))
                                Target = Convert.ToDecimal(RowsSelect(0)("TARGET"))
                            End If
                            TotalPOOriginal = Convert.ToDecimal(RowsSelect(0)("TOTAL_PO_ORIGINAL"))
                        End If
                        If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                            If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                                CombAgreeBrandID = RowsSelect(0)("CombinedWith").ToString()
                                If Not ListCombBrand.Contains(CombAgreeBrandID) Then
                                    ListCombBrand.Add(CombAgreeBrandID)
                                End If
                                RowSSelect1 = tblAcrHeader.Select(CategoryIsNewOrIsChanged & " = " & True & " AND AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")

                                'If (RowSSelect1(0)("BRAND_ID") = "77240" Or RowsSelect(0)("BRAND_ID") = "77230") Then
                                '    Stop
                                'End If

                                If Not ListAgreeBrand.Contains(CombAgreeBrandID) Then
                                    ListAgreeBrand.Add(CombAgreeBrandID)
                                End If
                                If Not IsDBNull(RowSSelect1(0)("IsFromAccHeader")) And Not IsNothing(RowSSelect1(0)("IsFromAccHeader")) Then
                                    IsFromAccHeader = CBool(RowSSelect1(0)("IsFromAccHeader"))
                                End If
                                If Not RowSSelect1.Length <= 0 Then
                                    If Not mustRecomputeYear And Not IsFromAccHeader And Not (FLAG = "Y") Then
                                        Target += Convert.ToDecimal(RowSSelect1(0)("TARGET"))
                                        TotalActual += Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL"))
                                    End If
                                    TotalPOOriginal += Convert.ToDecimal(RowSSelect1(0)("TOTAL_PO_ORIGINAL"))
                                End If
                            End If
                        End If
                        If Me.mustRecomputeYear Then
                            'If (RowsSelect(0)("BRAND_ID") = "77240" Or RowsSelect(0)("BRAND_ID") = "77230") Then
                            '    Stop
                            'End If
                            Target = Convert.ToDecimal(RowsSelect(0)("TARGET"))
                            TotalActual = Convert.ToDecimal(RowsSelect(0)("TOTAL_ACTUAL"))

                            If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                                If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                                    If Not IsFromAccHeader Then
                                        Target += Convert.ToDecimal(RowSSelect1(0)("TARGET"))
                                        TotalActual += Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL"))
                                    ElseIf (Convert.ToDecimal(RowSSelect1(0)("TARGET")) <> Target) Then ''check bila total actual dan target gak sama mesti di samain
                                        Target += Convert.ToDecimal(RowSSelect1(0)("TARGET"))
                                        If ((Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL")) <> TotalActual)) Then
                                            TotalActual += Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL"))
                                        End If
                                    End If

                                End If
                            End If
                        End If
                        '-------------------------------------------------------------------------------------------
                        'jika flag bukan 'y' 'hitung TotalActual
                        If FLAG <> "Y" Then : Me.GetTotalActual(FLAG, StartDate, EndDate, Target, TotalActual) : End If
                        '--------------------------------------------------------------------------------------------
                        DVCustom.RowFilter = "AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'"
                        If DVCustom.Count <= 0 Then
                            If (CombAgreeBrandID <> "") Then
                                DVCustom.RowFilter = "AGREE_BRAND_ID = '" & CombAgreeBrandID & "'"
                            End If
                        End If
                        Dim Percentage As Decimal = 0
                        If Target <> 0 Then
                            'Percentage = common.CommonClass.GetPercentage(100, TotalActual, Target)
                            Percentage = common.CommonClass.GetPercentage(100, TotalPOOriginal, Target)
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
                        Me.setTotalInvoiceBefore(FLAG, RowsSelect, RowSSelect1, tblListBonusBefore, Row, DescriptionTotal, Description, BonusQty, Rows)

                        For i1 As Integer = 0 To RowsSelect.Length - 1
                            Dim BonusDistributor As Decimal = 0
                            Row = RowsSelect(i1) : Row.BeginEdit()
                            BonusDistributor += (Dispro / 100) * Convert.ToDecimal(Row("ACTUAL_DISTRIBUTOR"))
                            If tblListBonusBefore.Rows.Count > 0 Then
                                Rows = tblListBonusBefore.Select("AGREE_BRAND_ID = '" & Row("AGREE_BRAND_ID").ToString() & "'")
                                For i2 As Integer = 0 To Rows.Length - 1
                                    BonusDistributor += (Convert.ToDecimal(Row(Rows(i2)("PB"))) * (Convert.ToDecimal(Rows(i2)("GP")) / 100))
                                Next
                            End If

                            '============COMMENT THIS AFTER COUNTING YEAR=================
                            'If FLAG <> "Y" Then

                            'Else
                            '    If (mustRecomputeYear) Then
                            '        TPBYear = 0
                            '        If Not IsNothing(Row("TOTAL_PBY")) And Not IsDBNull(Row("TOTAL_PBY")) Then
                            '            If CDec(Row("TOTAL_PBY")) > 0 Then
                            '                TPBYear = Convert.ToDecimal(Row("TOTAL_PBY"))
                            '            End If
                            '        End If
                            '        BonusDistributor += (GPPBY / 100) * TPBYear
                            '    End If
                            'End If
                            '==================================================================================

                            Row("DISPRO") = Dispro
                            Row("ACHIEVEMENT_DISPRO") = AchievementDispro
                            Row("BONUS_QTY") = BonusQty
                            Row("DISC_BY_VOLUME") = BonusQty
                            Row("BONUS_DISTRIBUTOR") = BonusDistributor
                            Row("DISC_DIST_BY_VOLUME") = BonusDistributor
                            'Row("AGREE_ACH_BY") = "VOL"
                            Row("BALANCE") = Balance
                            Row("CombinedWith") = DBNull.Value
                            Row("DESCRIPTION") = Description
                            If (CombAgreeBrandID <> "") Then
                                Row("ISCOMBINED_TARGET") = True
                                Row("CombinedWith") = CombAgreeBrandID
                                If FLAG = "Y" Then
                                    If Not IsFromAccHeader Then
                                        Row("TOTAL_ACTUAL") = TotalActual
                                    ElseIf (Convert.ToDecimal(RowSSelect1(0)("TARGET")) <> Target) Then ''check bila total actual dan target gak sama mesti di samain
                                        If ((Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL")) <> TotalActual)) Then
                                            Row("TOTAL_ACTUAL") = TotalActual
                                        End If
                                    End If
                                End If
                            End If
                            If FLAG <> "Y" Then : Row("TOTAL_ACTUAL") = TotalActual
                            End If
                            Row.EndEdit()
                        Next
                        If CombAgreeBrandID <> "" Then
                            For i1 As Integer = 0 To RowSSelect1.Length - 1
                                Dim BonusDistributor As Decimal = 0
                                Row = RowSSelect1(i1) : Row.BeginEdit()
                                BonusDistributor += (Dispro / 100) * Convert.ToDecimal(Row("ACTUAL_DISTRIBUTOR"))
                                If FLAG <> "Y" Then
                                    If tblListBonusBefore.Rows.Count > 0 Then
                                        Rows = tblListBonusBefore.Select("AGREE_BRAND_ID = '" & Row("AGREE_BRAND_ID").ToString() & "'")
                                        For i2 As Integer = 0 To Rows.Length - 1
                                            BonusDistributor += (Convert.ToDecimal(Row(Rows(i2)("PB"))) * (Convert.ToDecimal(Rows(i2)("GP")) / 100))
                                        Next
                                    End If
                                End If
                                ''============COMMENT THIS AFTER COUNTING YEAR=================
                                'If FLAG <> "Y" Then

                                'Else
                                '    If (mustRecomputeYear) Then
                                '        TPBYear1 = 0
                                '        If Not IsNothing(Row("TOTAL_PBY")) And Not IsDBNull(Row("TOTAL_PBY")) Then
                                '            If CDec(Row("TOTAL_PBY")) > 0 Then
                                '                TPBYear1 = Convert.ToDecimal(Row("TOTAL_PBY"))
                                '            End If
                                '        End If
                                '        BonusDistributor += (GPPBY / 100) * TPBYear1
                                '    End If
                                'End If
                                '====================================================
                                Row("DISPRO") = Dispro
                                Row("ACHIEVEMENT_DISPRO") = AchievementDispro
                                Row("BONUS_QTY") = BonusQty
                                Row("DISC_BY_VOLUME") = BonusQty
                                Row("BONUS_DISTRIBUTOR") = BonusDistributor
                                Row("DISC_DIST_BY_VOLUME") = BonusDistributor
                                'Row("AGREE_ACH_BY") = "VOL"
                                Row("BALANCE") = Balance
                                Row("ISCOMBINED_TARGET") = True
                                Row("DESCRIPTION") = Description
                                Row("CombinedWith") = ListAgreeBrand1(i).ToString()
                                If FLAG <> "Y" Then : Row("TOTAL_ACTUAL") = TotalActual
                                Else
                                    If Not IsFromAccHeader Then
                                        Row("TOTAL_ACTUAL") = TotalActual
                                    ElseIf (Convert.ToDecimal(RowsSelect(0)("TARGET")) <> Target) Then ''check bila total actual dan target gak sama mesti di samain
                                        If ((Convert.ToDecimal(RowsSelect(0)("TOTAL_ACTUAL")) <> TotalActual)) Then
                                            Row("TOTAL_ACTUAL") = TotalActual
                                        End If
                                    End If
                                End If
                                Row.EndEdit()
                            Next
                        End If
                    End If
                Next
            End If
            RowsSelect = tblAcrHeader.Select(CategoryIsNewOrIsChanged & " = " & True)
            If FLAG <> "Y" Then
                If DVTypical.Count > 0 Then
                    'reset semua decimal data
                    'check apakah agree_brand_id ada di table0
                    'check apakah sudah di masukan di custom
                    Dim ListAgreeBrand1 As New List(Of String) : ListAgreeBrand.Clear()
                    For i As Integer = 0 To RowsSelect.Length - 1
                        DVCustom.RowFilter = "AGREE_BRAND_ID = '" & RowsSelect(i)("AGREE_BRAND_ID").ToString() & "'"
                        If DVCustom.Count <= 0 Then
                            If Not ListAgreeBrand1.Contains(RowsSelect(i)("AGREE_BRAND_ID").ToString()) Then
                                If Not ListCombBrand.Contains(RowsSelect(i)("AGREE_BRAND_ID").ToString()) Then
                                    Dim BFInd As Boolean = False
                                    For i2 As Integer = 0 To ListCombBrand.Count - 1
                                        If RowsSelect(i)("CombinedWith").ToString() = ListCombBrand(i2).ToString() Then
                                            BFInd = True : Exit For
                                        End If
                                    Next
                                    If Not BFInd Then
                                        ListAgreeBrand1.Add(RowsSelect(i)("AGREE_BRAND_ID").ToString())
                                    End If
                                End If
                            End If
                        End If
                    Next
                    For i As Integer = 0 To ListAgreeBrand1.Count - 1
                        Dim Target As Decimal = 0, TotalActual As Decimal = 0, TotalPOOriginal As Decimal = 0, CombAgreeBrandID As String = "", IsFromAccHeader As Boolean = False
                        Dispro = 0 : AchievementDispro = 0 : BonusQty = 0 : Balance = 0
                        tblListBonusBefore.Clear() : Description = "" : DescriptionTotal = ""
                        If Not ListAgreeBrand.Contains(ListAgreeBrand1(i)) Then
                            ListAgreeBrand.Add(ListAgreeBrand1(i))
                            RowsSelect = tblAcrHeader.Select(CategoryIsNewOrIsChanged & " = " & True & " AND AGREE_BRAND_ID = '" & ListAgreeBrand1(i).ToString() & "'")
                            ''ambil totalactualnya
                            If Not RowsSelect.Length <= 0 Then

                                If Not mustRecomputeYear And Not IsFromAccHeader And Not (FLAG = "Y") Then
                                    TotalActual = Convert.ToDecimal(RowsSelect(0)("TOTAL_ACTUAL"))
                                    Target = Convert.ToDecimal(RowsSelect(0)("TARGET"))
                                End If
                                TotalPOOriginal = Convert.ToDecimal(RowsSelect(0)("TOTAL_PO_ORIGINAL"))
                            End If

                            If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                                If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                                    CombAgreeBrandID = RowsSelect(0)("CombinedWith").ToString()
                                    If Not ListAgreeBrand.Contains(CombAgreeBrandID) Then
                                        ListAgreeBrand.Add(CombAgreeBrandID)
                                    End If
                                    RowSSelect1 = tblAcrHeader.Select(CategoryIsNewOrIsChanged & " = " & True & " AND AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                                    If Not RowSSelect1.Length <= 0 Then
                                        Target += Convert.ToDecimal(RowSSelect1(0)("TARGET"))
                                        TotalActual += Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL"))

                                    End If
                                    TotalPOOriginal += Convert.ToDecimal(RowSSelect1(0)("TOTAL_PO_ORIGINAL"))
                                End If
                                'RowsSelect = tblAcrHeader.Select("AGREE_BRAND_ID = '" & )
                            End If
                            If Me.mustRecomputeYear Then

                                'If (RowsSelect(i)("BRAND_ID") = "77240" Or RowsSelect(i)("BRAND_ID") = "77230") Then
                                '    Stop
                                'End If

                                Target = Convert.ToDecimal(RowsSelect(0)("TARGET"))
                                TotalActual = Convert.ToDecimal(RowsSelect(0)("TOTAL_ACTUAL"))
                                If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                                    If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                                        If Not IsFromAccHeader Then
                                            Target += Convert.ToDecimal(RowSSelect1(0)("TARGET"))
                                            TotalActual += Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL"))
                                        ElseIf (Convert.ToDecimal(RowSSelect1(0)("TARGET")) <> Target) Then ''check bila total actual dan target gak sama mesti di samain
                                            Target += Convert.ToDecimal(RowSSelect1(0)("TARGET"))
                                            If ((Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL")) <> TotalActual)) Then
                                                TotalActual += Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL"))
                                            End If
                                        End If
                                    End If
                                End If

                                'TJoinPBYear = Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBY"))
                            End If
                            '-------------------------------------------------------------------------------------------
                            'jika flag bukan 'y' 'hitung TotalActual
                            If FLAG <> "Y" Then : Me.GetTotalActual(FLAG, StartDate, EndDate, Target, TotalActual) : End If
                            '--------------------------------------------------------------------------------------------
                            Dim Percentage As Decimal = 0
                            If Target <> 0 Then
                                Percentage = common.CommonClass.GetPercentage(100, TotalPOOriginal, Target)
                            End If
                            'Dim Percentage As Decimal = common.CommonClass.GetPercentage(100, TotalActual, Target)
                            ''looping cari yang masuk dalam category
                            For i1 As Integer = 0 To DVTypical.Count - 1
                                If Percentage > Convert.ToDecimal(DVTypical(i1)("UP_TO_PCT")) Then
                                    Dispro = Convert.ToDecimal(DVTypical(i1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'check apakah comb_agree bukan null
                            'dapatkan percentagenya
                            'mulai edit row
                            AchievementDispro = Percentage
                            Balance = TotalPOOriginal - Target
                            If Balance > 0 Then : Balance = 0 : End If
                            'hitung bonusnya
                            BonusQty = (Dispro / 100) * TotalActual
                            Me.setTotalInvoiceBefore(FLAG, RowsSelect, RowSSelect1, tblListBonusBefore, Row, DescriptionTotal, Description, BonusQty, Rows)
                            For i1 As Integer = 0 To RowsSelect.Length - 1
                                Dim BonusDistributor As Decimal = 0
                                Row = RowsSelect(i1) : Row.BeginEdit()
                                BonusDistributor += (Dispro / 100) * Convert.ToDecimal(Row("ACTUAL_DISTRIBUTOR"))
                                If tblListBonusBefore.Rows.Count > 0 Then
                                    Rows = tblListBonusBefore.Select("AGREE_BRAND_ID = '" & Row("AGREE_BRAND_ID").ToString() & "'")
                                    For i2 As Integer = 0 To Rows.Length - 1
                                        BonusDistributor += (Convert.ToDecimal(Row(Rows(i2)("PB"))) * (Convert.ToDecimal(Rows(i2)("GP")) / 100))
                                    Next
                                End If
                                Row("DISPRO") = Dispro
                                Row("ACHIEVEMENT_DISPRO") = AchievementDispro
                                Row("BONUS_QTY") = BonusQty
                                Row("DISC_BY_VOLUME") = BonusQty
                                Row("BONUS_DISTRIBUTOR") = BonusDistributor
                                Row("DISC_DIST_BY_VOLUME") = BonusDistributor
                                'Row("AGREE_ACH_BY") = "VOL"
                                Row("BALANCE") = Balance
                                Row("DESCRIPTION") = Description
                                Row("CombinedWith") = DBNull.Value
                                If (CombAgreeBrandID <> "") Then
                                    Row("ISCOMBINED_TARGET") = True
                                    Row("CombinedWith") = CombAgreeBrandID
                                    If FLAG = "Y" Then
                                        If Not IsFromAccHeader Then
                                            Row("TOTAL_ACTUAL") = TotalActual
                                        ElseIf (Convert.ToDecimal(RowSSelect1(0)("TARGET")) <> Target) Then ''check bila total actual dan target gak sama mesti di samain
                                            If ((Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL")) <> TotalActual)) Then
                                                Row("TOTAL_ACTUAL") = TotalActual
                                            End If
                                        End If
                                    End If
                                End If
                                If FLAG <> "Y" Then : Row("TOTAL_ACTUAL") = TotalActual
                                End If

                                Row.EndEdit()
                            Next
                            If CombAgreeBrandID <> "" Then
                                For i1 As Integer = 0 To RowSSelect1.Length - 1
                                    Dim BonusDistributor As Decimal = 0
                                    Row = RowSSelect1(i1) : Row.BeginEdit()
                                    BonusDistributor += (Dispro / 100) * Convert.ToDecimal(Row("ACTUAL_DISTRIBUTOR"))
                                    If tblListBonusBefore.Rows.Count > 0 Then
                                        Rows = tblListBonusBefore.Select("AGREE_BRAND_ID = '" & Row("AGREE_BRAND_ID").ToString() & "'")
                                        For i2 As Integer = 0 To Rows.Length - 1
                                            BonusDistributor += (Convert.ToDecimal(Row(Rows(i2)("PB"))) * (Convert.ToDecimal(Rows(i2)("GP")) / 100))
                                        Next
                                    End If
                                    Row("DISPRO") = Dispro
                                    Row("ACHIEVEMENT_DISPRO") = AchievementDispro
                                    Row("BONUS_QTY") = BonusQty
                                    Row("DISC_BY_VOLUME") = BonusQty
                                    Row("BONUS_DISTRIBUTOR") = BonusDistributor
                                    Row("DISC_DIST_BY_VOLUME") = BonusDistributor
                                    'Row("AGREE_ACH_BY") = "VOL"
                                    Row("BALANCE") = Balance
                                    Row("ISCOMBINED_TARGET") = True
                                    Row("CombinedWith") = ListAgreeBrand1(i).ToString()
                                    Row("DESCRIPTION") = Description

                                    If FLAG <> "Y" Then : Row("TOTAL_ACTUAL") = TotalActual
                                    Else
                                        If Not IsFromAccHeader Then
                                            Row("TOTAL_ACTUAL") = TotalActual
                                        ElseIf (Convert.ToDecimal(RowsSelect(0)("TARGET")) <> Target) Then ''check bila total actual dan target gak sama mesti di samain
                                            If ((Convert.ToDecimal(RowsSelect(0)("TOTAL_ACTUAL")) <> TotalActual)) Then
                                                Row("TOTAL_ACTUAL") = TotalActual
                                            End If
                                        End If
                                    End If
                                    Row.EndEdit()
                                Next
                            End If
                        End If
                    Next
                ElseIf DVCustom.Count <= 0 And DVTypical.Count <= 0 Then
                    'KARENA SUDAH DI PAKE UNTUK Y GAK USAH DEH DI TAMBAHKAN MESSAGE AGREEMENT
                    If FLAG <> "Y" Then : Message &= AgreementNO & vbCrLf : End If
                End If
            Else
                Dim ListAgreeBrand1 As New List(Of String) : ListAgreeBrand.Clear()
                For i As Integer = 0 To RowsSelect.Length - 1
                    DVCustom.RowFilter = "AGREE_BRAND_ID = '" & RowsSelect(i)("AGREE_BRAND_ID").ToString() & "'"
                    If DVCustom.Count <= 0 Then
                        If Not ListAgreeBrand1.Contains(RowsSelect(i)("AGREE_BRAND_ID").ToString()) Then
                            If Not ListCombBrand.Contains(RowsSelect(i)("AGREE_BRAND_ID").ToString()) Then
                                Dim BFInd As Boolean = False
                                For i2 As Integer = 0 To ListCombBrand.Count - 1
                                    If RowsSelect(i)("CombinedWith").ToString() = ListCombBrand(i2).ToString() Then
                                        BFInd = True : Exit For
                                    End If
                                Next
                                If Not BFInd Then
                                    ListAgreeBrand1.Add(RowsSelect(i)("AGREE_BRAND_ID").ToString())
                                End If
                            End If
                        End If
                    End If
                Next
                For i As Integer = 0 To ListAgreeBrand1.Count - 1
                    Dim Target As Decimal = 0, TotalActual As Decimal = 0, TotalPOOriginal As Decimal = 0, CombAgreeBrandID As String = "", IsFromAccHeader As Boolean = False
                    Dim TJoinPBYear As Decimal = 0, TJoinPBYear1 As Decimal = 0, TPBYear As Decimal = 0, GPPBY As Decimal = 0, GPPBY1 As Decimal = 0, BonusDistributor As Decimal = 0
                    Dispro = 0 : AchievementDispro = 0 : BonusQty = 0 : Balance = 0
                    If Not ListAgreeBrand.Contains(ListAgreeBrand1(i)) Then
                        ListAgreeBrand.Add(ListAgreeBrand1(i))
                        RowsSelect = tblAcrHeader.Select(CategoryIsNewOrIsChanged & " = " & True & " AND AGREE_BRAND_ID = '" & ListAgreeBrand1(i).ToString() & "'")
                        If Not IsDBNull(RowsSelect(0)("IsFromAccHeader")) And Not IsNothing(RowsSelect(0)("IsFromAccHeader")) Then
                            IsFromAccHeader = CBool(RowsSelect(0)("IsFromAccHeader"))
                        End If
                        ''ambil totalactualnya
                        If Not RowsSelect.Length <= 0 Then
                            If Not mustRecomputeYear And Not IsFromAccHeader Then
                                TotalActual = Convert.ToDecimal(RowsSelect(0)("TOTAL_ACTUAL"))
                                Target = Convert.ToDecimal(RowsSelect(0)("TARGET"))
                            End If
                            TotalPOOriginal += Convert.ToDecimal(RowsSelect(0)("TOTAL_PO_ORIGINAL"))
                        End If
                        If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                            If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                                CombAgreeBrandID = RowsSelect(0)("CombinedWith").ToString()
                                If Not ListAgreeBrand.Contains(CombAgreeBrandID) Then
                                    ListAgreeBrand.Add(CombAgreeBrandID)
                                End If
                                RowSSelect1 = tblAcrHeader.Select(CategoryIsNewOrIsChanged & " = " & True & " AND AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                                If Not IsDBNull(RowSSelect1(0)("IsFromAccHeader")) And Not IsNothing(RowSSelect1(0)("IsFromAccHeader")) Then
                                    IsFromAccHeader = CBool(RowSSelect1(0)("IsFromAccHeader"))
                                End If
                                If Not RowSSelect1.Length <= 0 Then
                                    If Not mustRecomputeYear And Not IsFromAccHeader Then
                                        Target += Convert.ToDecimal(RowSSelect1(0)("TARGET"))
                                        TotalActual += Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL"))
                                    End If
                                    TotalPOOriginal += Convert.ToDecimal(RowSSelect1(0)("TOTAL_PO_ORIGINAL"))
                                End If
                            End If
                            'RowsSelect = tblAcrHeader.Select("AGREE_BRAND_ID = '" & )
                        End If
                        If Me.mustRecomputeYear Then
                            Target = Convert.ToDecimal(RowsSelect(0)("TARGET"))
                            TotalActual = Convert.ToDecimal(RowsSelect(0)("TOTAL_ACTUAL"))
                            If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                                If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                                    If Not IsFromAccHeader Then
                                        Target += Convert.ToDecimal(RowSSelect1(0)("TARGET"))
                                        TotalActual += Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL"))
                                    ElseIf (Convert.ToDecimal(RowSSelect1(0)("TARGET")) <> Target) Then ''check bila total actual dan target gak sama mesti di samain
                                        Target += Convert.ToDecimal(RowSSelect1(0)("TARGET"))
                                        If ((Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL")) <> TotalActual)) Then
                                            TotalActual += Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL"))
                                        End If
                                    End If
                                End If
                            End If
                            If Not IsNothing(RowsSelect(0)("TOTAL_JOIN_PBY")) And Not IsDBNull(RowsSelect(0)("TOTAL_JOIN_PBY")) Then
                                If CDec(RowsSelect(0)("TOTAL_JOIN_PBY")) > 0 Then
                                    TJoinPBYear = Convert.ToDecimal(RowsSelect(0)("TOTAL_JOIN_PBY"))
                                End If
                            End If
                            If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                                If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                                    If Not IsNothing(RowSSelect1(0)("TOTAL_JOIN_PBY")) And Not IsDBNull(RowSSelect1(0)("TOTAL_JOIN_PBY")) Then
                                        If CDec(RowSSelect1(0)("TOTAL_JOIN_PBY")) > 0 Then
                                            'Target = Convert.ToDecimal(RowSSelect1(0)("TARGET"))
                                            'TotalActual = Convert.ToDecimal(RowSSelect1(0)("TOTAL_ACTUAL"))
                                            TJoinPBYear1 = Convert.ToDecimal(RowSSelect1(0)("TOTAL_JOIN_PBY"))
                                        End If
                                    End If
                                End If

                            End If
                        End If
                        Dim Percentage As Decimal = 0
                        If Target <> 0 Then
                            Percentage = common.CommonClass.GetPercentage(100, TotalPOOriginal, Target)
                        End If
                        'Dim Percentage As Decimal = common.CommonClass.GetPercentage(100, TotalActual, Target)
                        ''looping cari yang masuk dalam category
                        If DVTypical.Count > 0 Then
                            For i1 As Integer = 0 To DVTypical.Count - 1
                                If Percentage > Convert.ToDecimal(DVTypical(i1)("UP_TO_PCT")) Then
                                    Dispro = Convert.ToDecimal(DVTypical(i1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                        Else
                            Dispro = 0
                        End If
                        ''check apakah comb_agree bukan null
                        'dapatkan percentagenya
                        'mulai edit row
                        AchievementDispro = Percentage
                        Balance = TotalPOOriginal - Target
                        If Balance > 0 Then : Balance = 0 : End If
                        'hitung bonusnya
                        BonusQty = (Dispro / 100) * TotalActual

                        If Not IsNothing(RowsSelect(0)("GPPBY")) And Not IsDBNull(RowsSelect(0)("GPPBY")) Then
                            If CDec(RowsSelect(0)("GPPBY")) > 0 Then
                                GPPBY = Convert.ToDecimal(RowsSelect(0)("GPPBY"))
                            End If
                            If Not IsDBNull(RowsSelect(0)("CombinedWith")) And Not IsNothing(RowsSelect(0)("CombinedWith")) Then
                                If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith")) Then
                                    If CDec(RowSSelect1(0)("GPPBY")) > 0 Then
                                        GPPBY1 = Convert.ToDecimal(RowSSelect1(0)("GPPBY"))
                                    End If
                                End If
                            End If
                        End If
                        BonusQty += (GPPBY / 100) * TJoinPBYear
                        BonusQty += (GPPBY1 / 100) * TJoinPBYear1

                        For i1 As Integer = 0 To RowsSelect.Length - 1
                            Row = RowsSelect(i1)
                            TPBYear = 0
                            If Not IsNothing(Row("TOTAL_PBY")) And Not IsDBNull(Row("TOTAL_PBY")) Then
                                If CDec(Row("TOTAL_PBY")) > 0 Then
                                    TPBYear = Convert.ToDecimal(Row("TOTAL_PBY"))
                                End If
                            End If
                            BonusDistributor += (Dispro / 100) * Convert.ToDecimal(Row("ACTUAL_DISTRIBUTOR"))
                            BonusDistributor += (GPPBY / 100) * TPBYear

                            Row.BeginEdit()
                            Row("DISPRO") = Dispro
                            Row("ACHIEVEMENT_DISPRO") = AchievementDispro
                            Row("BONUS_QTY") = BonusQty
                            Row("DISC_BY_VOLUME") = BonusQty
                            Row("BONUS_DISTRIBUTOR") = BonusDistributor
                            Row("DISC_DIST_BY_VOLUME") = BonusDistributor
                            'Row("AGREE_ACH_BY") = "VOL"
                            Row("BALANCE") = Balance
                            Row("CombinedWith") = DBNull.Value
                            If (CombAgreeBrandID <> "") Then
                                Row("ISCOMBINED_TARGET") = True
                                Row("CombinedWith") = CombAgreeBrandID
                            End If
                            Row.EndEdit()
                        Next
                        If CombAgreeBrandID <> "" Then
                            For i1 As Integer = 0 To RowSSelect1.Length - 1
                                Row = RowSSelect1(i1)
                                TPBYear = 0
                                If Not IsNothing(Row("TOTAL_PBY")) And Not IsDBNull(Row("TOTAL_PBY")) Then
                                    If CDec(Row("TOTAL_PBY")) > 0 Then
                                        TPBYear = Convert.ToDecimal(Row("TOTAL_PBY"))
                                    End If
                                End If
                                BonusDistributor += (Dispro / 100) * Convert.ToDecimal(Row("ACTUAL_DISTRIBUTOR"))
                                BonusDistributor += (GPPBY / 100) * TPBYear

                                Row.BeginEdit()
                                Row("DISPRO") = Dispro
                                Row("ACHIEVEMENT_DISPRO") = AchievementDispro
                                Row("BONUS_QTY") = BonusQty
                                Row("DISC_BY_VOLUME") = BonusQty
                                Row("BONUS_DISTRIBUTOR") = BonusDistributor
                                Row("DISC_DIST_BY_VOLUME") = BonusDistributor
                                'Row("AGREE_ACH_BY") = "VOL"
                                Row("BALANCE") = Balance
                                Row("ISCOMBINED_TARGET") = True
                                Row("CombinedWith") = ListAgreeBrand1(i).ToString()
                                Row.EndEdit()
                            Next
                        End If
                    End If
                Next
            End If
            ''sekarang edit yang changed
            tblAcrHeader.AcceptChanges()

        End Sub
        Private Sub setGivenProgressive(ByRef Rows As DataRow(), ByVal RowsSelect As DataRow(), ByVal RowsSelect1 As DataRow(), ByVal tblListBonusBefore As DataTable, ByVal colGiven As String)
            Rows = tblListBonusBefore.Select("AGREE_BRAND_ID = '" & RowsSelect(0)("AGREE_BRAND_ID").ToString() & "'")
            For i As Integer = 0 To Rows.Length - 1
                If (Rows(i)("PB").ToString().Remove(0, 6) = colGiven.Remove(0, 2)) Then
                    Rows(i).BeginEdit()
                    Rows(i)("GP") = Convert.ToDecimal(RowsSelect(0)(colGiven))
                    Rows(i).EndEdit()
                    Rows(i).AcceptChanges()
                End If
            Next
            If Not IsDBNull(RowsSelect(0)("CombinedWith")) Then
                If Not String.IsNullOrEmpty(RowsSelect(0)("CombinedWith").ToString()) Then
                    Rows = tblListBonusBefore.Select("AGREE_BRAND_ID = '" & RowsSelect1(0)("AGREE_BRAND_ID").ToString() & "'")
                    For i As Integer = 0 To Rows.Length - 1
                        If (Rows(i)("PB").ToString().Remove(0, 6) = colGiven.Remove(0, 2)) Then
                            Rows(i).BeginEdit()
                            Rows(i)("GP") = Convert.ToDecimal(RowsSelect1(0)(colGiven))
                            Rows(i).EndEdit()
                            Rows(i).AcceptChanges()
                        End If
                    Next
                End If
            End If
        End Sub
        Private Sub UpdateActualDistributor(ByRef tblAcrHeader As DataTable, ByRef tblAcrDetail As DataTable)
            If tblAcrHeader.Rows.Count > 0 Then
                For i As Integer = 0 To tblAcrHeader.Rows.Count - 1
                    Dim RowsSelect() As DataRow = tblAcrDetail.Select("ACHIEVEMENT_ID = '" & tblAcrHeader.Rows(i)("ACHIEVEMENT_ID").ToString() & "'")
                    Dim TActual As Decimal = 0, BonusDistributor As Decimal = 0
                    For i1 As Integer = 0 To RowsSelect.Length - 1
                        TActual += Convert.ToDecimal(RowsSelect(i1)("TOTAL_ACTUAL"))
                        BonusDistributor += Convert.ToDecimal(RowsSelect(i1)("DISC_QTY"))
                    Next
                    Dim row As DataRow = tblAcrHeader.Rows(i)
                    row.BeginEdit() : row("ACTUAL_DISTRIBUTOR") = TActual
                    row("BONUS_DISTRIBUTOR") = BonusDistributor
                    row.EndEdit()
                Next
            End If
        End Sub

        Private Sub UpdateActual(ByRef tblAcrHeader As DataTable, ByVal ListAgreeBrand As List(Of String))
            Dim RowsSelect() As DataRow = Nothing
            For i As Integer = 0 To ListAgreeBrand.Count - 1
                Dim TotalActual As Decimal = 0, TOTAL_PBQ4 As Decimal = 0, Row As DataRow = Nothing
                RowsSelect = tblAcrHeader.Select("AGREE_BRAND_ID = '" & ListAgreeBrand(i) & "'")
                Dim T_Join_PBQ4 As Decimal = 0, T_Join_PBS2 As Decimal = 0, T_Join_PBQ3 As Decimal = 0, _
                    T_Join_CPQ1 As Decimal = 0, T_Join_CPQ2 As Decimal = 0, T_Join_CPQ3 As Decimal = 0, T_Join_CPS1 As Decimal = 0, _
                    TotalPOOriginal As Decimal = 0, TotalActualAmount = 0, TotalPOAmount = 0
                If RowsSelect.Length > 0 Then
                    For i1 As Integer = 0 To RowsSelect.Length - 1
                        TotalActual += Convert.ToDecimal(RowsSelect(i1)("ACTUAL_DISTRIBUTOR"))
                        TotalActualAmount += Convert.ToDecimal(RowsSelect(i1)("ACTUAL_AMOUNT_DISTRIBUTOR"))
                        T_Join_PBQ4 += Convert.ToDecimal(RowsSelect(i1)("TOTAL_PBQ4"))
                        T_Join_PBS2 += Convert.ToDecimal(RowsSelect(i1)("TOTAL_PBS2"))
                        T_Join_PBQ3 += Convert.ToDecimal(RowsSelect(i1)("TOTAL_PBQ3"))

                        T_Join_CPQ1 += Convert.ToDecimal(RowsSelect(i1)("TOTAL_CPQ1"))
                        T_Join_CPQ2 += Convert.ToDecimal(RowsSelect(i1)("TOTAL_CPQ2"))
                        T_Join_CPQ3 += Convert.ToDecimal(RowsSelect(i1)("TOTAL_CPQ3"))
                        T_Join_CPS1 += Convert.ToDecimal(RowsSelect(i1)("TOTAL_CPS1"))
                        TotalPOOriginal += Convert.ToDecimal(RowsSelect(i1)("TOTAL_PO_DISTRIBUTOR"))
                        TotalPOAmount += Convert.ToDecimal(RowsSelect(i1)("PO_AMOUNT_DISTRIBUTOR"))
                    Next
                    'update totalActual
                    For i1 As Integer = 0 To RowsSelect.Length - 1
                        Row = RowsSelect(i1)
                        Row.BeginEdit()
                        Row("TOTAL_ACTUAL") = TotalActual
                        Row("TOTAL_ACTUAL_AMOUNT") = TotalActualAmount

                        Row("TOTAL_JOIN_PBQ3") = T_Join_PBQ3
                        Row("TOTAL_JOIN_PBS2") = T_Join_PBS2
                        Row("TOTAL_JOIN_PBQ4") = T_Join_PBQ4

                        Row("TOTAL_JOIN_CPQ1") = T_Join_CPQ1
                        Row("TOTAL_JOIN_CPQ2") = T_Join_CPQ2
                        Row("TOTAL_JOIN_CPQ3") = T_Join_CPQ3
                        Row("TOTAL_JOIN_CPS1") = T_Join_CPS1
                        Row("TOTAL_PO_ORIGINAL") = TotalPOOriginal
                        Row("TOTAL_PO_AMOUNT") = TotalPOAmount
                        Row.EndEdit()
                    Next
                End If
            Next
        End Sub
        Private Sub getTotalInvoiceByQS(ByVal AgreementNo As String, ByVal Flag As String, ByVal IsTargetGroup As Boolean, ByRef tblAcrHeader As DataTable, _
        ByRef tblAcrDetail As DataTable, ByRef ListAgreeBrand As List(Of String), ByVal StartDate As DateTime, ByVal EndDate As DateTime)
            '-----------------------------DECLARASI TABLE------------------------------------------------------------
            Dim Row As DataRow = Nothing, RowsSelect() As DataRow = Nothing, _
           IsChangedData As Boolean = False, HasNewData As Boolean = False, _
           ListAchievementChangedData As New List(Of String), ListAchievementNewData As New List(Of String)
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT DA.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID,ABI.BRAND_ID,ABI.COMB_AGREE_BRAND_ID,ABI.TARGET_" & Flag & ", ABI.TARGET_" & Flag & "_FM, ABI.TARGET_" & Flag & "_PL" & vbCrLf & _
                    " FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRAND_INCLUDE ABI ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                    " WHERE DA.AGREEMENT_NO = @AGREEMENT_NO AND NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER  " & vbCrLf & _
                    " WHERE ACHIEVEMENT_ID = DA.DISTRIBUTOR_ID + '|' + @AGREEMENT_NO  + '' + ABI.BRAND_ID + '|" & Flag & "'" & vbCrLf & _
                    " ) OPTION(KEEP PLAN);"

            Dim hasCreatedTempTablePO As Boolean = False
            Me.ResetCommandText(CommandType.Text, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read()
                HasNewData = True
                Row = tblAcrHeader.NewRow()
                Dim BrandID As String = SqlRe.GetString(2)
                Dim Target As Decimal = SqlRe.GetDecimal(4)
                Dim TargetFM As Decimal = SqlRe.GetDecimal(5)
                Dim TargetPL As Decimal = SqlRe.GetDecimal(6)
                Dim AchievementID As String = SqlRe.GetString(0) + "|" + AgreementNo + BrandID + "|" + Flag
                Row("ACHIEVEMENT_ID") = AchievementID
                Row("AGREEMENT_NO") = AgreementNo
                Row("DISTRIBUTOR_ID") = SqlRe.GetString(0)
                Row("AGREE_BRAND_ID") = SqlRe.GetString(1)
                Row("BRAND_ID") = BrandID
                Row("FLAG") = Flag
                Row("TARGET") = Target
                Row("TARGET_FM") = TargetFM
                Row("TARGET_PL") = TargetPL
                Row("ISTARGET_GROUP") = IsTargetGroup
                Row("IsNew") = True
                Row("IsChanged") = False
                If ((Not IsDBNull(SqlRe("COMB_AGREE_BRAND_ID"))) And (Not IsNothing(SqlRe("COMB_AGREE_BRAND_ID")))) Then
                    Row("ISCOMBINED_TARGET") = True
                    Row("CombinedWith") = SqlRe.GetString(3)
                End If
                Row.EndEdit() : tblAcrHeader.Rows.Add(Row)
                If Not ListAgreeBrand.Contains(SqlRe.GetString(1)) Then
                    ListAgreeBrand.Add(SqlRe.GetString(1))
                End If
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
                     "  SELECT PO.PO_REF_NO,PO.PO_REF_DATE,PO.DISTRIBUTOR_ID,ABI.BRAND_ID,ABP.BRANDPACK_ID,OPB.PO_ORIGINAL_QTY,OPB.PO_PRICE_PERQTY,SB.SPPB_QTY,OOA.RUN_NUMBER ," & vbCrLf & _
                     "  IncludeDPD = CASE WHEN (OPB.ExcludeDPD = 0) THEN 'YESS' " & vbCrLf & _
                     "  WHEN (EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PLANTATION_ID = OPB.PLANTATION_ID AND BRANDPACK_ID = OPB.BRANDPACK_ID AND DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID AND PRICE = OPB.PO_PRICE_PERQTY AND START_DATE >= DATEADD(MONTH,-6,@START_DATE) AND END_DATE <= @END_DATE AND IncludeDPD = 1)) THEN 'YESS' " & vbCrLf & _
                     "  WHEN (EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PLANTATION_ID = OPB.PLANTATION_ID AND BRANDPACK_ID = OPB.BRANDPACK_ID AND DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID AND PRICE = OPB.PO_PRICE_PERQTY AND START_DATE >= DATEADD(MONTH,-6,@START_DATE) AND END_DATE <= @END_DATE AND IncludeDPD = 0)) THEN 'NO' " & vbCrLf & _
                     "  WHEN (EXISTS(SELECT PROJ.PROJ_REF_NO, PB.BRANDPACK_ID FROM PROJ_PROJECT PROJ INNER JOIN PROJ_BRANDPACK PB ON PROJ.PROJ_REF_NO = PB.PROJ_REF_NO WHERE PROJ.PROJ_REF_NO = PO.PROJ_REF_NO AND PB.BRANDPACK_ID = OPB.BRANDPACK_ID AND PROJ.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID)) THEN 'NO' " & vbCrLf & _
                     "  WHEN (OPB.PLANTATION_ID IS NULL) THEN 'YESS' ELSE 'NO' END " & vbCrLf & _
                     "  FROM Nufarm.dbo.AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                     "  INNER JOIN Nufarm.DBO.AGREE_BRANDPACK_INCLUDE ABP ON ABI.AGREE_BRAND_ID = ABP.AGREE_BRAND_ID" & vbCrLf & _
                     "  INNER JOIN Nufarm.dbo.ORDR_PO_BRANDPACK OPB ON OPB.BRANDPACK_ID = ABP.BRANDPACK_ID " & vbCrLf & _
                     "  INNER JOIN Nufarm.dbo.ORDR_PURCHASE_ORDER PO " & vbCrLf & _
                     "  ON PO.PO_REF_NO = OPB.PO_REF_NO INNER JOIN Nufarm.dbo.ORDR_ORDER_ACCEPTANCE OOA " & vbCrLf & _
                     "  ON OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                     "  INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOA.OA_ID = OOAB.OA_ID AND OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                     "  LEFT OUTER JOIN SPPB_BRANDPACK SB ON SB.OA_BRANDPACK_ID = OOAB.OA_BRANDPACK_ID  " & vbCrLf & _
                     "  WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND OPB.PO_ORIGINAL_QTY > 0 " & vbCrLf & _
                     "  AND PO.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                     " )B ;" & vbCrLf & _
                     " CREATE CLUSTERED INDEX IX_T_MASTER_PO ON ##T_MASTER_PO_" & Me.ComputerName & "(PO_REF_DATE,PO_REF_NO,RUN_NUMBER,DISTRIBUTOR_ID,BRANDPACK_ID) ;"
            '============================= END UNCOMMENT THIS AFTER DEBUGGING =============================================================
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate.AddMonths(-6).AddDays(-1))
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
                    Dim ACHIEVEMENT_ID As String = SqlRe.GetString(0) & "|" & AgreementNo & SqlRe.GetString(1) & "|" & Flag
                    'If ACHIEVEMENT_ID = "SIS001IDR|0223/NI/III/2010.1100096|S1" Then
                    '    Stop
                    'End If
                    RowsSelect = tblAcrHeader.Select("ACHIEVEMENT_ID = '" & ACHIEVEMENT_ID & "'")
                    If RowsSelect.Length > 0 Then
                        Row = RowsSelect(0) : Row.BeginEdit()
                        Row("ACTUAL_DISTRIBUTOR") = SqlRe.GetDecimal(2)
                        'Row("PO_AMOUNT_DISTRIBUTOR") = SqlRe.GetDecimal(3)
                        Row("ACTUAL_AMOUNT_DISTRIBUTOR") = SqlRe.GetDecimal(4)
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
                    " SELECT DISTRIBUTOR_ID,BRAND_ID,SUM(QTY)AS TOTAL_INVOICE,SUM(PO_AMOUNT)AS PO_AMOUNT_DISTRIBUTOR,SUM(INV_AMOUNT)AS ACTUAL_AMOUNT_DISTRIBUTOR INTO ##T_Agreement_Brand_" & Me.ComputerName & " " & vbCrLf & _
                    "  FROM( " & vbCrLf & _
                    "       SELECT PO.DISTRIBUTOR_ID,PO.BRAND_ID,PO.BRANDPACK_ID,(ISNULL(INV.QTY,0)/PO.SPPB_QTY)* PO.PO_ORIGINAL_QTY AS QTY,PO.PO_AMOUNT,ISNULL(INV.INV_AMOUNT,0) AS INV_AMOUNT " & vbCrLf & _
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

            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Changed_Invoice_By_Brand_ID")
            'Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate) ' SMALLDATETIME,
            Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2)
            Me.AddParameter("@COMPUTERNAME", SqlDbType.VarChar, Me.ComputerName, 100)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read()
                ''tblAcrhHeader diatas hanya di isi dengan data yang new
                'RowsSelect = tblAcrHeader.Select("ACHIEVEMENT_ID = '" & SqlRe.GetString(0) & "|" & AgreementNo & SqlRe.GetString(1) & "|" & Flag & "'")
                'If (RowsSelect.Length > 0) Then
                '    ''update row isi column total_actual nya saja
                '    'Row = RowsSelect(0) : Row.BeginEdit()
                '    'Row("ACTUAL_DISTRIBUTOR") = SqlRe.GetDecimal(3) ''3 = total_invoice
                '    'Row("IsChanged") = False
                '    'Row.EndEdit()
                'Else
                '    'mulai insert baru
                '    IsChangedData = True : Row = tblAcrHeader.NewRow()
                '    Dim BrandID As String = SqlRe.GetString(1)
                '    Dim Target As Decimal = SqlRe.GetDecimal(2)
                '    Dim AchievementID As String = SqlRe.GetString(0) & "|" + AgreementNo & SqlRe.GetString(1) & "|" & Flag
                '    'Row = RowsSelect(0)
                '    Row("ACHIEVEMENT_ID") = AchievementID
                '    Row("AGREEMENT_NO") = AgreementNo
                '    Row("DISTRIBUTOR_ID") = SqlRe.GetString(0)
                '    Row("BRAND_ID") = SqlRe.GetString(1)
                '    Row("AGREE_BRAND_ID") = AgreementNo + SqlRe.GetString(1)
                '    Row("FLAG") = Flag
                '    Row("TARGET") = Target
                '    Row("IsNew") = False
                '    Row("ACTUAL_DISTRIBUTOR") = SqlRe.GetDecimal(3)
                '    Row("IsChanged") = True
                '    Row.EndEdit() : tblAcrHeader.Rows.Add(Row)
                '    If Not ListAgreeBrand.Contains(AgreementNo & "" & SqlRe.GetString(1)) Then
                '        ListAgreeBrand.Add(AgreementNo & "" & SqlRe.GetString(1))
                '    End If
                '    If Not ListAchievementChangedData.Contains(AchievementID) Then
                '        ListAchievementChangedData.Add(AchievementID)
                '    End If
                'End If
                IsChangedData = True : Row = tblAcrHeader.NewRow()
                Dim BrandID As String = SqlRe.GetString(1)
                Dim Target As Decimal = SqlRe.GetDecimal(2)
                Dim AchievementID As String = SqlRe.GetString(0) & "|" + AgreementNo & SqlRe.GetString(1) & "|" & Flag
                'Row = RowsSelect(0)
                If tblAcrHeader.Select("ACHIEVEMENT_ID = '" & AchievementID & "'").Length <= 0 Then
                    Row("ACHIEVEMENT_ID") = AchievementID
                    Row("AGREEMENT_NO") = AgreementNo
                    Row("DISTRIBUTOR_ID") = SqlRe.GetString(0)
                    Row("BRAND_ID") = SqlRe.GetString(1)
                    Row("AGREE_BRAND_ID") = AgreementNo + SqlRe.GetString(1)
                    Row("FLAG") = Flag
                    Row("TARGET") = Target
                    Row("IsNew") = False
                    Row("ACTUAL_DISTRIBUTOR") = SqlRe.GetDecimal(3)
                    Row("PO_AMOUNT_DISTRIBUTOR") = SqlRe.GetDecimal(4)
                    Row("ACTUAL_AMOUNT_DISTRIBUTOR") = SqlRe.GetDecimal(5)
                    Row("TARGET_FM") = SqlRe.GetDecimal(6)
                    Row("TARGET_PL") = SqlRe.GetDecimal(7)
                    Row("IsChanged") = True
                    Row.EndEdit() : tblAcrHeader.Rows.Add(Row)
                    If Not ListAgreeBrand.Contains(AgreementNo & "" & SqlRe.GetString(1)) Then
                        ListAgreeBrand.Add(AgreementNo & "" & SqlRe.GetString(1))
                    End If
                    If Not ListAchievementChangedData.Contains(AchievementID) Then
                        ListAchievementChangedData.Add(AchievementID)
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
                    RowsSelect = tblAcrHeader.Select("IsNew = " & True & " AND IsChanged = " & False & " AND ACHIEVEMENT_ID = '" & ListAchievementNewData(i) & "'")
                    If RowsSelect.Length > 0 Then
                        Dim ACHIEVEMENT_ID As String = ListAchievementNewData(i)
                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                                " SELECT ABI.BRANDPACK_ID,ABI.ACHIEVEMENT_ID,ABI.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                                " FROM ( " & vbCrLf & _
                                "       SELECT ACHIEVEMENT_ID = ABI.DISTRIBUTOR_ID + '|' + ABI.AGREE_BRAND_ID + '|" & Flag & "'," & vbCrLf & _
                                "       ABI.BRANDPACK_ID,ABI.DISTRIBUTOR_ID + '|' + ABI.AGREE_BRAND_ID + '|" & Flag & "|' + ABI.BRANDPACK_ID AS ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                                "       FROM VIEW_AGREE_BRANDPACK_INCLUDE ABI WHERE DISTRIBUTOR_ID + '|' + AGREE_BRAND_ID + '|" & Flag & "' = '" & ListAchievementNewData(i) & "' " & vbCrLf & _
                                "      )ABI " & vbCrLf & _
                                " WHERE NOT EXISTS(SELECT ACD.ACHIEVEMENT_BRANDPACK_ID FROM ACCRUED_DETAIL ACD INNER JOIN ACCRUED_HEADER ACRH " & vbCrLf & _
                                "                   ON ACD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID " & vbCrLf & _
                                "                   WHERE ACRH.ACHIEVEMENT_ID = '" & ListAchievementNewData(i) & "' AND ACD.ACHIEVEMENT_BRANDPACK_ID = ABI.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                                "                   AND ACRH.ACHIEVEMENT_ID = ABI.ACHIEVEMENT_ID " & vbCrLf & _
                                "                  ) OPTION(KEEP PLAN);"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.SqlRe = Me.SqlCom.ExecuteReader()
                        While Me.SqlRe.Read()
                            Row = tblAcrDetail.NewRow()
                            Dim BRANDPACK_ID As String = SqlRe.GetString(0)
                            Row("ACHIEVEMENT_ID") = ACHIEVEMENT_ID
                            Row("BRANDPACK_ID") = BRANDPACK_ID
                            Row("ACHIEVEMENT_BRANDPACK_ID") = SqlRe.GetString(2)
                            Row("IsNew") = True
                            Row("IsChanged") = False
                            Row.EndEdit() : tblAcrDetail.Rows.Add(Row)
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
                                " CREATE CLUSTERED INDEX IX_T_PO_Original_By_Distributor ON ##T_PO_Original_By_Distributor_" & Me.ComputerName & "(QTY,DISTRIBUTOR_ID) ;"
                        Dim DistributorID As String = ListAchievementNewData(i).Remove(ListAchievementNewData(i).IndexOf("|"))
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                        Me.SqlCom.ExecuteScalar()

                        Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Total_Qty_BrandPack_By_Invoice")
                        Me.AddParameter("@COMPUTERNAME", SqlDbType.VarChar, Me.ComputerName, 100)
                        Me.SqlRe = Me.SqlCom.ExecuteReader()
                        While Me.SqlRe.Read()
                            RowsSelect = tblAcrDetail.Select("IsNew = " & True & " AND ACHIEVEMENT_BRANDPACK_ID = '" & ACHIEVEMENT_ID & "|" & SqlRe.GetString(1) & "'")
                            If RowsSelect.Length > 0 Then
                                Row = RowsSelect(0)
                                Row.BeginEdit()
                                Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(2)
                                Row("TOTAL_ACTUAL_AMOUNT") = SqlRe.GetDecimal(3)
                                'Row("TOTAL_PO_ORIGINAL") = SqlRe.GetDecimal(3)
                                Row.EndEdit()
                            End If
                        End While : SqlRe.Close()

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
                                RowsSelect = tblAcrDetail.Select("IsNew = " & True & " AND ACHIEVEMENT_BRANDPACK_ID = '" & ACHIEVEMENT_ID & "|" & BrandPackID & "'")
                                If RowsSelect.Length > 0 Then
                                    Row = RowsSelect(0)
                                    Row.BeginEdit()
                                    'Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(2)
                                    Row("TOTAL_PO_ORIGINAL") = tblPO.Rows(i1)("TOTAL_PO_ORIGINAL")
                                    Row("TOTAL_PO_AMOUNT") = tblPO.Rows(i1)("TOTAL_PO_AMOUNT")
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
                    RowsSelect = tblAcrHeader.Select("IsChanged = " & True & " AND ACHIEVEMENT_ID = '" & ListAchievementChangedData(i) & "'")
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

                        Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Changed_Invoice_By_BrandPack_ID")
                        Me.AddParameter("@COMPUTERNAME", SqlDbType.VarChar, Me.ComputerName, 100)
                        Me.SqlRe = Me.SqlCom.ExecuteReader()
                        Dim ACHIEVEMENT_ID As String = ""
                        While Me.SqlRe.Read()
                            Dim CanRelease As Boolean = SqlRe.GetBoolean(6)
                            ACHIEVEMENT_ID = ListAchievementChangedData(i)
                            Dim BRANDPACK_ID As String = SqlRe.GetString(1)
                            Dim AchievementBrandPackID As String = ACHIEVEMENT_ID + "|" + BRANDPACK_ID
                            If tblAcrDetail.Select("ACHIEVEMENT_BRANDPACK_ID = '" & AchievementBrandPackID & "'").Length <= 0 Then
                                Row = tblAcrDetail.NewRow()
                                Row("ACHIEVEMENT_ID") = ACHIEVEMENT_ID
                                Row("BRANDPACK_ID") = BRANDPACK_ID
                                Row("ACHIEVEMENT_BRANDPACK_ID") = AchievementBrandPackID
                                Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(7) 'TOTAL INVOICE
                                'Row("TOTAL_PO_ORIGINAL") = SqlRe.GetDecimal(8)
                                Row("TOTAL_ACTUAL_AMOUNT") = SqlRe.GetDecimal(9)
                                Row("IsChanged") = True
                                Row("IsNew") = False
                                If CanRelease Then
                                    Row("CAN_RELEASE") = True
                                    Row("RELEASE_QTY") = SqlRe.GetDecimal(4)
                                    Row("LEFT_QTY") = SqlRe.GetDecimal(5)
                                    Row.EndEdit()
                                End If
                                Row.EndEdit() : tblAcrDetail.Rows.Add(Row)
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
                                RowsSelect = tblAcrDetail.Select("IsChanged = " & True & " AND ACHIEVEMENT_BRANDPACK_ID = '" & ACHIEVEMENT_ID & "|" & BrandPackID & "'")
                                If RowsSelect.Length > 0 Then
                                    Row = RowsSelect(0)
                                    Row.BeginEdit()
                                    'Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(2)
                                    Row("TOTAL_PO_ORIGINAL") = tblPO.Rows(i1)("TOTAL_PO_ORIGINAL")
                                    Row("TOTAL_PO_AMOUNT") = tblPO.Rows(i1)("TOTAL_PO_AMOUNT")
                                    Row.EndEdit()
                                End If
                            Next
                        End If
                    End If
                Next
            End If
            If IsChangedData Or HasNewData Then
                ''sum po original diantara periode Flag
                Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                        "SELECT AGREE_BRAND_ID = @AGREEMENT_NO + '' + BRAND_ID,DISTRIBUTOR_ID,ISNULL(SUM(PO_ORIGINAL_QTY),0)AS TOTAL_PO,ISNULL(SUM(PO_AMOUNT),0)AS TOTAL_PO_AMOUNT FROM ##T_MASTER_PO_" & Me.ComputerName & " WHERE PO_REF_DATE >= @START_DATE " & vbCrLf & _
                        "AND PO_REF_DATE <= @END_DATE GROUP BY DISTRIBUTOR_ID,@AGREEMENT_NO + '' +  BRAND_ID ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Dim tblPOOriginal As New DataTable("T_PO_original")
                tblPOOriginal.Clear() : Me.setDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(tblPOOriginal)
                ''ISI TOTAL_ORIGINAL_PO
                If tblPOOriginal.Rows.Count > 0 Then
                    ''loopping terus
                    For i As Integer = 0 To tblPOOriginal.Rows.Count - 1
                        Dim rows As DataRow() = tblAcrHeader.Select("ACHIEVEMENT_ID = '" & tblPOOriginal.Rows(i)("DISTRIBUTOR_ID").ToString() + "|" + tblPOOriginal.Rows(i)("AGREE_BRAND_ID").ToString() + "|" + Flag + "'")
                        If rows.Length > 0 Then
                            rows(0).BeginEdit()
                            rows(0)("TOTAL_PO_DISTRIBUTOR") = Convert.ToDecimal(tblPOOriginal.Rows(i)("TOTAL_PO"))
                            rows(0)("PO_AMOUNT_DISTRIBUTOR") = Convert.ToDecimal(tblPOOriginal.Rows(i)("TOTAL_PO_AMOUNT"))
                            rows(0).EndEdit()
                        End If
                    Next
                End If
                tblAcrHeader.AcceptChanges()
                Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                "DECLARE @V_START_DATE SMALLDATETIME ;" & vbCrLf & _
                "SET @V_START_DATE = (SELECT START_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO); " & vbCrLf & _
                "SELECT TOP 1 AA.START_DATE,AA.END_DATE,AA.QS_TREATMENT_FLAG FROM AGREE_AGREEMENT AA INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                " WHERE AA.END_DATE < @V_START_DATE AND EXISTS(SELECT AGREEMENT_NO FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = AA.AGREEMENT_NO AND TARGET_" & Flag & " > 0 ) " & vbCrLf & _
                " AND DA.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO) ORDER BY AA.START_DATE DESC ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Dim tblStartDate As New DataTable("T_StartDate1")
                tblStartDate.Clear() : Me.setDataAdapter(Me.SqlCom).Fill(tblStartDate)
                Dim PBQ1 As Object = Nothing, PBEQ1 As Object = Nothing, PBQ2 As Object = Nothing, PBEQ2 As Object = Nothing, _
                                   PBQ3 As Object = Nothing, PBEQ3 As Object = Nothing, PBQ4 As Object = Nothing, PBEQ4 As Object = Nothing, _
                                   PBS1 As Object = Nothing, PBES1 As Object = Nothing, PBS2 As Object = Nothing, PBES2 As Object = Nothing, _
                                   PBStartYear As Object = Nothing, PBEndyear As Object = Nothing
                Dim CPQ1 As Object = Nothing, CPEQ1 As Object = Nothing, CPQ2 As Object = Nothing, CPEQ2 As Object = Nothing, _
                CPQ3 As Object = Nothing, CPEQ3 As Object = Nothing, CPS1 As Object = Nothing, CPES1 As Object = Nothing
                Select Case Flag
                    Case "Q2"
                        CPEQ1 = StartDate.AddDays(-1)
                        CPQ1 = Convert.ToDateTime(CPEQ1).AddMonths(-3).AddDays(1)
                    Case "Q3"
                        CPEQ2 = StartDate.AddDays(-1)
                        CPQ2 = Convert.ToDateTime(CPEQ2).AddMonths(-3).AddDays(1)
                        CPEQ1 = Convert.ToDateTime(CPQ2).AddDays(-1)
                        CPQ1 = Convert.ToDateTime(CPEQ1).AddMonths(-3).AddDays(1)
                    Case "Q4"
                        CPEQ3 = StartDate.AddDays(-1)
                        CPQ3 = Convert.ToDateTime(CPEQ3).AddMonths(-3).AddDays(1)
                        CPEQ2 = Convert.ToDateTime(CPQ3).AddDays(-1)
                        CPQ2 = Convert.ToDateTime(CPEQ2).AddMonths(-3).AddDays(1)
                    Case "S2"
                        CPES1 = Convert.ToDateTime(StartDate.AddDays(-1))
                        CPS1 = Convert.ToDateTime(CPES1).AddMonths(-6).AddDays(1)
                End Select
                Dim tblTemp As New DataTable("T_TEMP") : tblTemp.Clear()
                '---------------------QUERY UNTUK MENTOTAL KAN TOTAL_ORIGINAL_QTY BRAND DIANTARA START_DATE AND END_DATE BERDASARKAN BRAND ---------------------------
                Dim Query1 As String = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                                     " SELECT DISTRIBUTOR_ID,BRAND_ID,ISNULL(SUM(QTY),0) AS TOTAL_INVOICE,ISNULL(SUM(INV_AMOUNT),0)AS CP_AMOUNT " & vbCrLf & _
                                     "  FROM( " & vbCrLf & _
                                     "       SELECT PO.DISTRIBUTOR_ID,PO.BRAND_ID,PO.BRANDPACK_ID,(ISNULL(INV.QTY,0)/PO.SPPB_QTY)* PO.PO_ORIGINAL_QTY AS QTY,INV.INV_AMOUNT " & vbCrLf & _
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

                '---------------------QUERY UNTUK MENTOTAL KAN TOTAL_ORIGINAL_QTY BRANDPACK DIANTARA START_DATE AND END_DATE BERDASARKAN BRANDPACK---------------------------
                Dim Query2 As String = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                                       " SELECT DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID,ISNULL(SUM(QTY),0) AS TOTAL_INVOICE,ISNULL(SUM(INV_AMOUNT),0)AS CP_AMOUNT  " & vbCrLf & _
                                       "  FROM( " & vbCrLf & _
                                       "       SELECT PO.DISTRIBUTOR_ID,PO.BRAND_ID,PO.BRANDPACK_ID,(ISNULL(INV.QTY,0)/PO.SPPB_QTY)* PO.PO_ORIGINAL_QTY AS QTY,INV.INV_AMOUNT " & vbCrLf & _
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

                '----------------QUERY UNTUK MENGECEK APAKAH ADA INVOICE DARI PO SEBELUM PERIODE FLAG
                Dim Query3 As String = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                                       " SELECT 1 WHERE EXISTS(SELECT PO.BRANDPACK_ID " & vbCrLf & _
                                       " FROM tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO " & vbCrLf & _
                                       " INNER JOIN COMPARE_ITEM Tmbp ON PO.BRANDPACK_ID = Tmbp.BRANDPACK_ID_DTS " & vbCrLf & _
                                       " INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON Tmbp.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
                                       " AND ((PO.RUN_NUMBER = INV.REFERENCE) OR (PO.PO_REF_NO = INV.PONUMBER)) " & vbCrLf & _
                                       " WHERE PO.DISTRIBUTOR_ID = SOME( " & vbCrLf & _
                                       " SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                                       " AND ((ISNULL(INV.QTY,0)/ISNULL(PO.SPPB_QTY,0)) * ISNULL(PO.PO_ORIGINAL_QTY,0) > 0 )" & vbCrLf & _
                                       " AND PO.PO_REF_DATE < @START_DATE AND PO.IncludeDPD = 'YESS') "

                '-----------------QUERY UNTUK MENGECEK GIVEN PROGRESSIVE
                'Dim Query4 As String = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                '        "SELECT 1 WHERE EXISTS(SELECT GP.IDApp FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRAND_INCLUDE ABI ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                '        " INNER JOIN GIVEN_PROGRESSIVE GP ON GP.AGREE_BRAND_ID = ABI.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO) ;"
                Me.ResetCommandText(CommandType.Text, Query1)
                If tblStartDate.Rows.Count > 0 Then
                    'Me.ResetCommandText(CommandType.Text, Query3)
                    'Dim retval As Object = Me.SqlCom.ExecuteScalar()
                    'If Not IsDBNull(retval) And Not IsNothing(retval) Then
                    '    Me.ResetCommandText(CommandType.Text, Query4)
                    '    retval = Me.SqlCom.ExecuteScalar()
                    '    If IsNothing(retval) Or IsDBNull(retval) Then
                    '        Me.ClearCommandParameters()
                    '        Throw New Exception("GIVEN_PROGRESSIVE FOR Periode " & Flag & ",Before " & vbCrLf & "Has not  been set.")
                    '    End If
                    'End If

                    Dim PBStartDate As Object = Nothing
                    Dim PBEndDate As Object = Nothing, PBFlag As String = ""
                    PBStartDate = Convert.ToDateTime(tblStartDate.Rows(0)("START_DATE"))
                    PBEndDate = Convert.ToDateTime(tblStartDate.Rows(0)("END_DATE"))
                    PBFlag = tblStartDate.Rows(0)("QS_TREATMENT_FLAG").ToString()
                    Select Case PBFlag
                        Case "S"
                            PBS1 = PBStartDate
                            PBES1 = Convert.ToDateTime(PBS1).AddMonths(6).AddDays(-1)
                            PBS2 = Convert.ToDateTime(PBES1).AddDays(1)
                            PBES2 = PBEndDate
                        Case "Q"
                            PBQ1 = PBStartDate
                            PBEQ1 = Convert.ToDateTime(PBQ1).AddMonths(3).AddDays(-1)
                            PBQ2 = Convert.ToDateTime(PBEQ1).AddDays(1)
                            PBEQ2 = Convert.ToDateTime(PBQ2).AddMonths(3).AddDays(-1)
                            PBQ3 = Convert.ToDateTime(PBEQ2).AddDays(1)
                            ''transisi
                            If PBStartDate >= New Date(2019, 8, 1) And PBEndDate <= New Date(2020, 7, 31) Then
                                PBEQ3 = Convert.ToDateTime(PBQ3).AddMonths(2).AddDays(-1)
                            Else
                                PBEQ3 = Convert.ToDateTime(PBQ3).AddMonths(3).AddDays(-1)
                            End If

                            PBQ4 = Convert.ToDateTime(PBEQ3).AddDays(1)
                            PBEQ4 = PBEndDate
                    End Select
                    'bikin table untuk mesplit data invoice yang bukan berada diantara periode Flag(periode sebelumnya)
                    Select Case Flag
                        Case "Q1"
                            Select Case PBFlag
                                Case "S"
                                    If Not IsNothing(PBS2) Then
                                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBS2)
                                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBES2))
                                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                        If tblTemp.Rows.Count > 0 Then
                                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_PBS2")
                                            Me.ResetCommandText(CommandType.Text, Query2)
                                            tblTemp = New DataTable("T_TEMP")
                                            tblTemp.Clear()
                                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                            If tblTemp.Rows.Count > 0 Then
                                                SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_PBS2")
                                            End If
                                        End If
                                    End If
                                Case "Q" 'berarti Q4
                                    If Not IsNothing(PBQ4) Then
                                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBQ4))
                                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, PBEQ4)
                                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                        If tblTemp.Rows.Count > 0 Then
                                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_PBQ4")
                                            Me.ResetCommandText(CommandType.Text, Query2)
                                            tblTemp = New DataTable("T_TEMP")
                                            tblTemp.Clear()
                                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                            If tblTemp.Rows.Count > 0 Then
                                                SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_PBQ4")
                                            End If
                                        End If
                                    End If
                                    If Not IsNothing(PBQ3) Then
                                        Me.ResetCommandText(CommandType.Text, Query1)
                                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBQ3))
                                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, PBEQ3)
                                        tblTemp = New DataTable("T_TEMP")
                                        tblTemp.Clear()
                                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                        If tblTemp.Rows.Count > 0 Then
                                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_PBQ3")
                                            Me.ResetCommandText(CommandType.Text, Query2)
                                            tblTemp = New DataTable("T_TEMP")
                                            tblTemp.Clear()
                                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                            If tblTemp.Rows.Count > 0 Then
                                                SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_PBQ3")
                                            End If
                                        End If
                                    End If
                            End Select
                        Case "S1"
                            Select Case PBFlag
                                Case "S"
                                    If Not IsNothing(PBS2) Then
                                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBS2)
                                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBES2))
                                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                        If tblTemp.Rows.Count > 0 Then
                                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_PBS2")
                                            Me.ResetCommandText(CommandType.Text, Query2)
                                            tblTemp = New DataTable("T_TEMP")
                                            tblTemp.Clear()
                                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                            If tblTemp.Rows.Count > 0 Then
                                                SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_PBS2")
                                            End If
                                        End If
                                    End If
                                Case "Q" 'berarti Q4
                                    If Not IsNothing(PBQ4) Then
                                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBQ4))
                                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, PBEQ4)
                                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                        If tblTemp.Rows.Count > 0 Then
                                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_PBQ4")
                                            Me.ResetCommandText(CommandType.Text, Query2)
                                            tblTemp = New DataTable("T_TEMP")
                                            tblTemp.Clear()
                                            If tblTemp.Rows.Count > 0 Then
                                                SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_PBQ4")
                                            End If
                                        End If
                                    End If
                                    If Not IsNothing(PBQ3) Then
                                        Me.ResetCommandText(CommandType.Text, Query1)
                                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBQ3))
                                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, PBEQ3)
                                        tblTemp = New DataTable("T_TEMP")
                                        tblTemp.Clear()
                                        setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                        If tblTemp.Rows.Count > 0 Then
                                            SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_PBQ3")
                                            Me.ResetCommandText(CommandType.Text, Query2)
                                            tblTemp = New DataTable("T_TEMP")
                                            tblTemp.Clear()
                                            If tblTemp.Rows.Count > 0 Then
                                                SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_PBQ3")
                                            End If
                                        End If
                                    End If
                            End Select
                    End Select
                End If
                Select Case Flag
                    Case "Q2"
                        If Not IsNothing(CPQ1) Then
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPQ1)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, CPEQ1)
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_CPQ1", "CPQ1_AMOUNT")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_CPQ1", "CPQ1_AMOUNT")
                                End If
                            End If
                        End If
                        If Not IsNothing(PBQ4) Then
                            Me.ResetCommandText(CommandType.Text, Query1)
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBQ4)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, PBEQ4)
                            tblTemp = New DataTable("T_TEMP")
                            tblTemp.Clear()
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_PBQ4")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_PBQ4")
                                End If
                            End If
                        End If
                    Case "Q3"
                        If Not IsNothing(CPQ2) Then
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPQ2)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, CPEQ2)
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_CPQ2", "CPQ2_AMOUNT")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_CPQ2", "CPQ2_AMOUNT")
                                End If
                            End If
                        End If
                        If Not IsNothing(CPQ1) Then
                            Me.ResetCommandText(CommandType.Text, Query1)
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPQ1)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, CPEQ1)
                            tblTemp = New DataTable("T_TEMP")
                            tblTemp.Clear()
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_CPQ1", "CPQ1_AMOUNT")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_CPQ1", "CPQ1_AMOUNT")
                                End If
                            End If
                        End If
                    Case "Q4"
                        If Not IsNothing(CPQ3) Then
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPQ3)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, CPEQ3)
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_CPQ3", "CPQ3_AMOUNT")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_CPQ3", "CPQ3_AMOUNT")
                                End If
                            End If
                        End If
                        If Not IsNothing(CPQ2) Then
                            Me.ResetCommandText(CommandType.Text, Query1)
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPQ2)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, CPEQ2)
                            tblTemp = New DataTable("T_TEMP")
                            tblTemp.Clear()
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_CPQ2", "CPQ2_AMOUNT")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_CPQ2", "CPQ2_AMOUNT")
                                End If
                            End If
                        End If
                    Case "S2"
                        If Not IsNothing(CPS1) Then
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPS1)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, CPES1)
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAcrHeader, Flag, "TOTAL_CPS1", "CPS1_AMOUNT")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAcrDetail, AgreementNo, Flag, "TOTAL_CPS1", "CPS1_AMOUNT")
                                End If
                            End If
                        End If
                End Select
                'AMBIL DATA POTENSI BERDASARKAN AGREEMENTNO,DENGAN COLUM DISTRIBUTOR_ID,BRAND_ID,
                Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                        "SELECT DA.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID,GP.IDApp,GP.PBQ3,GP.PBQ4,GP.PBS2,GP.CPQ1,GP.CPQ2,GP.CPQ3,GP.CPS1,GP.PBY " & vbCrLf & _
                        " FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRAND_INCLUDE ABI ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                        " INNER JOIN GIVEN_PROGRESSIVE GP ON GP.AGREE_BRAND_ID = ABI.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.SqlCom.Parameters.RemoveAt("@START_DATE")
                Me.SqlCom.Parameters.RemoveAt("@END_DATE")

                Dim tblGP As New DataTable("T_GP") : tblGP.Clear()
                setDataAdapter(Me.SqlCom).Fill(tblGP)
                If tblGP.Rows.Count > 0 Then
                    Dim rows() As DataRow = Nothing
                    For i As Integer = 0 To tblGP.Rows.Count - 1
                        rows = tblAcrHeader.Select("ACHIEVEMENT_ID = '" & tblGP.Rows(i)("DISTRIBUTOR_ID").ToString() & "|" & tblGP.Rows(i)("AGREE_BRAND_ID").ToString() & "|" & Flag & "'")
                        If rows.Length > 0 Then
                            rows(0).BeginEdit()
                            rows(0)("GP_ID") = tblGP.Rows(i)("IDApp")
                            rows(0)("GPPBQ4") = Convert.ToDecimal(tblGP.Rows(i)("PBQ4"))
                            rows(0)("GPPBS2") = Convert.ToDecimal(tblGP.Rows(i)("PBS2"))
                            rows(0)("GPPBQ3") = Convert.ToDecimal(tblGP.Rows(i)("PBQ3"))
                            rows(0)("GPCPQ1") = Convert.ToDecimal(tblGP.Rows(i)("CPQ1"))
                            rows(0)("GPCPQ2") = Convert.ToDecimal(tblGP.Rows(i)("CPQ2"))
                            rows(0)("GPCPQ3") = Convert.ToDecimal(tblGP.Rows(i)("CPQ3"))
                            rows(0)("GPCPS1") = Convert.ToDecimal(tblGP.Rows(i)("CPS1"))
                            rows(0)("GPPBY") = Convert.ToDecimal(tblGP.Rows(i)("PBY"))
                            rows(0).EndEdit()
                            rows(0).AcceptChanges()
                        End If
                    Next
                End If
                tblAcrHeader.AcceptChanges()
            End If
        End Sub
        Private Sub SetTotalPeriodeBeforeDetail(ByVal tbltemp As DataTable, ByRef tblAcrDetail As DataTable, ByVal AgreementNo As String, ByVal Flag As String, ByVal ColTotalPBFlag As String, Optional ByVal colTotalAmountFlag As String = "")
            Dim rows() As DataRow = Nothing
            For i As Integer = 0 To tbltemp.Rows.Count - 1
                rows = tblAcrDetail.Select("ACHIEVEMENT_BRANDPACK_ID = '" & tbltemp.Rows(i)("DISTRIBUTOR_ID").ToString() & "|" & AgreementNo & "" & tbltemp.Rows(i)("BRAND_ID") & "|" & Flag & "|" & tbltemp.Rows(i)("BRANDPACK_ID") & "'")
                If rows.Length > 0 Then
                    rows(0).BeginEdit()
                    rows(0)(ColTotalPBFlag) = Convert.ToDecimal(tbltemp.Rows(i)("TOTAL_INVOICE"))
                    If Not String.IsNullOrEmpty(colTotalAmountFlag) Then
                        rows(0)(colTotalAmountFlag) = Convert.ToDecimal(tbltemp.Rows(i)("CP_AMOUNT"))
                    End If
                    rows(0).EndEdit()
                    rows(0).AcceptChanges()
                End If
            Next
            tblAcrDetail.AcceptChanges()
        End Sub
        Private Sub SetTotalPeriodBefore(ByVal tblTemp As DataTable, ByVal AgreementNo As String, ByRef tblAcrHeader As DataTable, ByVal Flag As String, ByVal ColTotalPBFlag As String, Optional ByVal colTotalAmountFlag As String = "")
            For i As Integer = 0 To tblTemp.Rows.Count - 1
                Dim rows As DataRow()
                rows = tblAcrHeader.Select("ACHIEVEMENT_ID = '" & tblTemp.Rows(i)("DISTRIBUTOR_ID").ToString() & "|" & AgreementNo & tblTemp.Rows(i)("BRAND_ID").ToString() & "|" & Flag & "'")
                If rows.Length > 0 Then
                    rows(0).BeginEdit()
                    rows(0)(ColTotalPBFlag) = Convert.ToDecimal(tblTemp.Rows(i)("TOTAL_INVOICE"))
                    If Not String.IsNullOrEmpty(colTotalAmountFlag) Then
                        rows(0)(colTotalAmountFlag) = Convert.ToDecimal(tblTemp.Rows(i)("CP_AMOUNT"))
                    End If
                    rows(0).EndEdit()
                    rows(0).AcceptChanges()
                End If
            Next
            tblAcrHeader.AcceptChanges()
        End Sub
        Private Sub GenerateDiscount(ByVal FLAG As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal AgreementNO As String, _
        ByRef tblAcrHeader As DataTable, ByRef tblAcrDetail As DataTable, ByRef Message As String, ByRef HasTarget As Boolean, ByVal EndDateAgreement As DateTime)
            Dim ListAgreeBrand As New List(Of String), IsTargetGroup As Boolean = False, strFlag As String = ""
            '------------------------------------------------------------------------------------------
            'bikin table accrued_header dan detail buat nginsert data
            'bikin satu table brand_id dimana brand_id belum yang belum di bikin di acrrued_header dan brand_id yang sudah di bikin
            'di acrued header yang mungkin sudah ada perubahan data invoice yang berhubungan dengan brand_id tsb
            'CHEK APAKAH SUDAH DI BIKIN DISCOUNTNYA
            'chek apakah data dispro qsy flag dan target strFlag dari salah satu ada yang dirubah sama user / gaknya

            If FLAG <> "Y" Then
                If EndDateAgreement < New DateTime(2010, 9, 1) Then
                    strFlag = FLAG.Remove(1, 1)
                    Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                              "SELECT  1 WHERE EXISTS(SELECT ABI.AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE ABI INNER JOIN AGREE_PROG_DISC APD " & vbCrLf & _
                              "                       ON ABI.AGREE_BRAND_ID = APD.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO " & vbCrLf & _
                              "                       AND APD.QSY_DISC_FLAG = '" & strFlag & "') " & vbCrLf & _
                              "             OR EXISTS(SELECT AA.AGREEMENT_NO FROM AGREE_AGREEMENT AA INNER JOIN AGREE_PROGRESSIVE_DISCOUNT APD " & vbCrLf & _
                              "                       ON AA.AGREEMENT_NO = APD.AGREEMENT_NO INNER JOIN AGREE_BRAND_INCLUDE ABI ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                              "                       WHERE AA.AGREEMENT_NO = @AGREEMENT_NO AND APD.QSY_DISC_FLAG = '" & strFlag & "')" & vbCrLf & _
                              "                       OPTION(KEEP PLAN); "
                Else
                    strFlag = FLAG
                    Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                             "SELECT  1 WHERE EXISTS(SELECT ABI.AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE ABI INNER JOIN AGREE_PROG_DISC APD " & vbCrLf & _
                             "                       ON ABI.AGREE_BRAND_ID = APD.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO " & vbCrLf & _
                             "                       AND APD.QSY_DISC_FLAG = '" & strFlag & "') " & vbCrLf & _
                             "             OR EXISTS(SELECT ABI.AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE ABI INNER JOIN AGREE_PROG_DISC APD " & vbCrLf & _
                             "                       ON ABI.AGREE_BRAND_ID = APD.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO " & vbCrLf & _
                             "                       AND APD.QSY_DISC_FLAG = '" & strFlag.Remove(1, 1) & "') " & vbCrLf & _
                             "             OR EXISTS(SELECT AA.AGREEMENT_NO FROM AGREE_AGREEMENT AA INNER JOIN AGREE_PROGRESSIVE_DISCOUNT APD " & vbCrLf & _
                             "                       ON AA.AGREEMENT_NO = APD.AGREEMENT_NO INNER JOIN AGREE_BRAND_INCLUDE ABI ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                             "                       WHERE AA.AGREEMENT_NO = @AGREEMENT_NO AND APD.QSY_DISC_FLAG = '" & strFlag.Remove(1, 1) & "') " & vbCrLf & _
                             "                       OPTION(KEEP PLAN); "
                End If

            Else
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT  1 WHERE EXISTS(SELECT ABI.[ID] FROM VIEW_AGREE_BRAND_INCLUDE ABI INNER JOIN AGREE_PROG_DISC APD " & vbCrLf & _
                        "                       ON ABI.[ID] = APD.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO " & vbCrLf & _
                        "                       AND APD.QSY_DISC_FLAG = '" & FLAG & "') " & vbCrLf & _
                        "             OR EXISTS(SELECT AA.AGREEMENT_NO FROM AGREE_AGREEMENT AA INNER JOIN AGREE_PROGRESSIVE_DISCOUNT APD " & vbCrLf & _
                        "                       ON AA.AGREEMENT_NO = APD.AGREEMENT_NO INNER JOIN VIEW_AGREE_BRAND_INCLUDE ABI ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                        "                       WHERE AA.AGREEMENT_NO = @AGREEMENT_NO " & vbCrLf & _
                        "                       AND APD.QSY_DISC_FLAG = '" & FLAG & "') " & vbCrLf & _
                        "             OR EXISTS(SELECT [ID] FROM VIEW_AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = @AGREEMENT_NO ) OPTION(KEEP PLAN);"
            End If

            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNO, 32)
            Dim retval As Object = Me.SqlCom.ExecuteScalar()
            If IsNothing(retval) Or IsDBNull(retval) Then : Me.ClearCommandParameters() : Return : End If
            If CInt(retval) <= 0 Then : Me.ClearCommandParameters() : HasTarget = False : Return : End If
            If mustRecomputeYear Then
                'check apakah sudah ada discount yang di generate yang bukan Flag 'Y'
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON;" & vbCrLf & _
                "SELECT 1 WHERE EXISTS(SELECT AGREEMENT_NO FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND FLAG = 'Y') ;"
                Me.ResetCommandText(CommandType.Text, Query)
                retval = Me.SqlCom.ExecuteScalar()
                If IsNothing(retval) Or IsDBNull(retval) Then
                    Me.CloseConnection() : Me.ClearCommandParameters() : Throw New Exception("Please compute any other Flag before year")
                End If
            End If
            '--------------------------CHECK AGREEMENT_NO DI MANA TARGET DAN PROGRESSIVE DISCOUNT ADA-----------------------------------------------------------------
            '---------------------------------------------------------------------------------
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT COUNT(AGREEMENT_NO) FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO OPTION(KEEP PLAN);"

            Me.ResetCommandText(CommandType.Text, Query)
            IsTargetGroup = (CInt(Me.SqlCom.ExecuteScalar()) > 1)
            '-------------------------CHECK APAKAH TARGET_GROUP / BUKAN------------------------------------------------
            '----------------------------------------------------------------------------------
            Me.CreateOrRecreatTblAcrHeader(tblAcrHeader) : Me.CreateOrRecreateTblAcrDetail(tblAcrDetail)
            '-------------------------BIKIN TABLE BUAT NGINSERT DAN UPDATE DATA-------------------

            If FLAG <> "Y" Then
                Me.getTotalInvoiceByQS(AgreementNO, FLAG, IsTargetGroup, tblAcrHeader, tblAcrDetail, ListAgreeBrand, StartDate, EndDate)
            Else
                Me.getTotalInvoiceByYear(AgreementNO, IsTargetGroup, StartDate, EndDate, tblAcrHeader, tblAcrDetail, ListAgreeBrand)
            End If
            '-----------------------------------------------------------------------------------------
            'chek perubahan
            If tblAcrHeader.Rows.Count <= 0 Then : Me.ClearCommandParameters() : Return : End If
            '--------------------------------------------------------------------------------
            If FLAG <> "Y" Then : Me.UpdateActual(tblAcrHeader, ListAgreeBrand) : End If
            Me.SaveToDataBase(AgreementNO, FLAG, ListAgreeBrand, StartDate, EndDate, tblAcrHeader, tblAcrDetail, Message, EndDateAgreement, IsTargetGroup)

        End Sub

        Private Sub getTotalInvoiceByYear(ByVal AgreementNO As String, ByVal IsTargetGroup As Boolean, ByVal StartDate As DateTime, _
        ByVal EndDate As DateTime, ByRef tblAcrHeader As DataTable, ByRef tblAcrDetail As DataTable, ByRef ListAgreeBrand As List(Of String))
            Dim strDecStartDate As String = "" '= common.CommonClass.getNumericFromDate(StartDateQ1)
            Dim strDecEndDate As String = "" 'common.CommonClass.getNumericFromDate(EndDateQ1)
            Dim Row As DataRow = Nothing, RowsSelect() As DataRow = Nothing, HasNewData As Boolean = False, IsChangedData As Boolean = False
            Dim StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, _
                StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, _
                StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing, _
                CurrentFlag As String = "", CurrentAgreementFlag As String = "", _
                CurrentDate As DateTime = NufarmBussinesRules.SharedClass.ServerDate, Retval As Object = Nothing
            StartDateQ1 = StartDate : StartDateS1 = StartDate : EndDateQ4 = EndDate : EndDateS2 = EndDate
            EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
            StartDateQ2 = EndDateQ1.AddDays(1) : EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
            If StartDate >= New Date(2019, 8, 1) And EndDate <= New Date(2020, 7, 31) Then
                ''transition time
                StartDateQ3 = EndDateQ2.AddDays(1) : EndDateQ3 = StartDateQ3.AddMonths(2).AddDays(-1)
            Else
                StartDateQ3 = EndDateQ2.AddDays(1) : EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
            End If
            StartDateQ4 = EndDateQ3.AddDays(1)
            EndDateS1 = StartDateS1.AddMonths(6).AddDays(-1)
            StartDateS2 = EndDateS1.AddDays(1)
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT TOP 1 QS_TREATMENT_FLAG FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO;"
            Me.ResetCommandText(CommandType.Text, Query) : CurrentAgreementFlag = Me.SqlCom.ExecuteScalar().ToString()
            If CurrentAgreementFlag = "S" Then
                If CurrentDate >= StartDateS1 And CurrentDate <= EndDateS1 Then : CurrentFlag = "S1"
                ElseIf CurrentDate >= StartDateS2 And CurrentDate <= EndDateS2 Then : CurrentFlag = "S2"
                End If
            ElseIf CurrentAgreementFlag = "Q" Then
                If CurrentDate >= StartDateQ1 And CurrentDate <= EndDateQ1 Then : CurrentFlag = "Q1"
                ElseIf CurrentDate >= StartDateQ2 And CurrentDate <= EndDateQ2 Then : CurrentFlag = "Q2"
                ElseIf CurrentDate >= StartDateQ3 And CurrentDate <= EndDateQ3 Then : CurrentFlag = "Q3"
                ElseIf CurrentDate >= StartDateQ4 And CurrentDate <= EndDateQ4 Then : CurrentFlag = "Q4"
                End If
            Else : Throw New Exception("Flag For Agreement " & AgreementNO & " is null ")
            End If
            'daripada pusing-pusinglah
            If Me.mustRecomputeYear Then
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " DELETE FROM ACCRUED_DETAIL WHERE ((RELEASE_QTY <= 0) OR (DISC_QTY <= 0)) AND ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND FLAG = 'Y')" & vbCrLf & _
                " DELETE FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND FLAG = 'Y' AND ACHIEVEMENT_ID NOT IN(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL);"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.SqlCom.ExecuteScalar()
            End If
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                   "SELECT ABI.DISTRIBUTOR_ID,ABI.AGREEMENT_NO + '' + ABI.BRAND_ID AS AGREE_BRAND_ID,ABI.BRAND_ID,ABI.COMBINED_BRAND,ABI.TARGET_YEAR,ABI.TARGET_YEAR_FM,ABI.TARGET_YEAR_PL " & vbCrLf & _
                   " FROM VIEW_AGREE_BRAND_INCLUDE ABI WHERE AGREEMENT_NO = @AGREEMENT_NO AND NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER  " & vbCrLf & _
                   " WHERE ACHIEVEMENT_ID = ABI.DISTRIBUTOR_ID + '|' + @AGREEMENT_NO  + '' + ABI.BRAND_ID + '|Y'" & vbCrLf & _
                   " ) OPTION(KEEP PLAN);"
            'Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Changed_Actual_Accrue_By_Year")
            Me.ResetCommandText(CommandType.Text, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read()
                HasNewData = True
                Row = tblAcrHeader.NewRow()
                Dim BrandID As String = SqlRe.GetString(2)
                Dim Target As Decimal = SqlRe.GetDecimal(4)
                Dim AchievementID As String = SqlRe.GetString(0) + "|" + AgreementNO + BrandID + "|Y"
                Row("ACHIEVEMENT_ID") = AchievementID
                Row("AGREEMENT_NO") = AgreementNO
                Row("DISTRIBUTOR_ID") = SqlRe.GetString(0)
                Row("AGREE_BRAND_ID") = SqlRe.GetString(1)
                Row("BRAND_ID") = BrandID
                Row("FLAG") = "Y"
                Row("TARGET") = Target
                Row("TARGET_FM") = SqlRe.GetDecimal(5)
                Row("TARGET_PL") = SqlRe.GetDecimal(6)
                Row("ISTARGET_GROUP") = IsTargetGroup
                'Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(5)
                'Row("ACTUAL_DISTRIBUTOR") = SqlRe.GetDecimal(6)
                Row("IsNew") = True
                Row("IsChanged") = False
                If ((Not IsDBNull(SqlRe("COMBINED_BRAND"))) And (Not IsNothing(SqlRe("COMBINED_BRAND")))) Then
                    Row("ISCOMBINED_TARGET") = True
                    Row("CombinedWith") = SqlRe.GetString(3)
                End If
                Row.EndEdit() : tblAcrHeader.Rows.Add(Row)
                If Not ListAgreeBrand.Contains(SqlRe.GetString(2)) Then
                    ListAgreeBrand.Add(SqlRe.GetString(2))
                End If
            End While : Me.SqlRe.Close() : tblAcrHeader.AcceptChanges()

            If HasNewData Then
                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Total_Acrue_By_Year")
                Me.AddParameter("@CURRENT_FLAG", SqlDbType.VarChar, CurrentFlag, 2) : Me.SqlRe = Me.SqlCom.ExecuteReader()
                'End If
                While Me.SqlRe.Read()
                    Dim ACHIEVEMENT_ID As String = SqlRe.GetString(0) & "|" & SqlRe.GetString(2) & "|Y"
                    RowsSelect = tblAcrHeader.Select("ACHIEVEMENT_ID = '" & ACHIEVEMENT_ID & "'")
                    If RowsSelect.Length > 0 Then
                        Row = RowsSelect(0) : Row.BeginEdit()
                        Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(5)
                        Row("TOTAL_ACTUAL_AMOUNT") = SqlRe.GetDecimal(10)
                        Row("TOTAL_PO_ORIGINAL") = SqlRe.GetDecimal(6)
                        Row("TOTAL_PO_AMOUNT") = SqlRe.GetDecimal(11)
                        Row("TOTAL_PO_DISTRIBUTOR") = SqlRe.GetDecimal(7)
                        Row("PO_AMOUNT_DISTRIBUTOR") = SqlRe.GetDecimal(12)
                        Row("ACTUAL_DISTRIBUTOR") = SqlRe.GetDecimal(8)
                        Row("ACTUAL_AMOUNT_DISTRIBUTOR") = SqlRe.GetDecimal(13)
                        'If (Row("BRAND_ID") = "77240" Or Row("BRAND_ID") = "77230") Then
                        '    Stop
                        'End If
                        Row.EndEdit()
                    End If
                End While : SqlRe.Close()
                tblAcrHeader.AcceptChanges()
            End If
            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Changed_Actual_Accrue_By_Year")
            Me.AddParameter("@CURRENT_FLAG", SqlDbType.VarChar, CurrentFlag, 2) : Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read()
                Dim AchievementID As String = SqlRe.GetString(0) & "|" & SqlRe.GetString(2) & "|Y"
                RowsSelect = tblAcrHeader.Select("IsNew = " & False & " AND ACHIEVEMENT_ID = '" & AchievementID & "'")
                'If (Row("BRAND_ID") = "77240" Or Row("BRAND_ID") = "77230") Then
                '    Stop
                'End If
                If (RowsSelect.Length > 0) Then
                Else
                    'mulai insert baru
                    'If AgreementNO = "1103/NI/X/2008-20L.09" Then
                    '    Stop
                    'End If
                    If tblAcrHeader.Select("ACHIEVEMENT_ID = '" & AchievementID & "'").Length <= 0 Then
                        IsChangedData = True : Row = tblAcrHeader.NewRow()
                        Dim BrandID As String = SqlRe.GetString(3)
                        Dim Target As Decimal = SqlRe.GetDecimal(4)
                        'Dim AchievementID As String = SqlRe.GetString(0) & "|" + AgreementNO & SqlRe.GetString(1) & "|" & Flag
                        Row("ACHIEVEMENT_ID") = AchievementID
                        Row("AGREEMENT_NO") = AgreementNO
                        Row("DISTRIBUTOR_ID") = SqlRe.GetString(0)
                        Row("BRAND_ID") = BrandID
                        Row("AGREE_BRAND_ID") = SqlRe.GetString(2)
                        Row("FLAG") = "Y"
                        Row("TARGET") = Target
                        Row("IsNew") = False
                        Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(5)
                        Row("TOTAL_ACTUAL_AMOUNT") = SqlRe.GetDecimal(10)
                        Row("TOTAL_PO_ORIGINAL") = SqlRe.GetDecimal(6)
                        Row("TOTAL_PO_AMOUNT") = SqlRe.GetDecimal(11)
                        Row("TOTAL_PO_DISTRIBUTOR") = SqlRe.GetDecimal(7)
                        Row("PO_AMOUNT_DISTRIBUTOR") = SqlRe.GetDecimal(12)
                        Row("ACTUAL_DISTRIBUTOR") = SqlRe.GetDecimal(8) ''3 = total_invoice
                        Row("ACTUAL_AMOUNT_DISTRIBUTOR") = SqlRe.GetDecimal(13)
                        Row("IsChanged") = True
                        If ((Not IsDBNull(SqlRe("COMBINED_BRAND"))) And (Not IsNothing(SqlRe("COMBINED_BRAND")))) Then
                            Row("ISCOMBINED_TARGET") = True
                            Row("CombinedWith") = SqlRe.GetString(9)
                        End If
                        Row.EndEdit() : tblAcrHeader.Rows.Add(Row)
                        If Not ListAgreeBrand.Contains(AgreementNO & "" & SqlRe.GetString(1)) Then
                            ListAgreeBrand.Add(AgreementNO & "" & SqlRe.GetString(1))
                        End If
                    End If
                End If
            End While : SqlRe.Close() : If Me.SqlCom.Parameters().IndexOf("@CURRENT_FLAG") <> -1 Then
                Me.SqlCom.Parameters().RemoveAt("@CURRENT_FLAG") : tblAcrHeader.AcceptChanges()
            End If
            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Total_Accrue_By_Year_Detail")
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read()
                Dim ACHIEVEMENT_ID As String = SqlRe.GetString(1) + "|" + SqlRe.GetString(0) + "|Y"
                Row = tblAcrDetail.NewRow()
                Dim BRANDPACK_ID As String = SqlRe.GetString(2)
                Row("ACHIEVEMENT_ID") = ACHIEVEMENT_ID
                Row("BRANDPACK_ID") = BRANDPACK_ID
                Row("ACHIEVEMENT_BRANDPACK_ID") = ACHIEVEMENT_ID + "|" + BRANDPACK_ID
                Row("IsChanged") = False
                Row("IsNew") = True
                'If BRANDPACK_ID.IndexOf("77230") >= 0 Or BRANDPACK_ID.IndexOf("77240") >= 0 Then
                '    Stop
                'End If
                Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(3)
                Row("TOTAL_ACTUAL_AMOUNT") = SqlRe.GetDecimal(4)
                Row("TOTAL_PO_ORIGINAL") = SqlRe.GetDecimal(5)
                Row("TOTAL_PO_AMOUNT") = SqlRe.GetDecimal(6)
                Row.EndEdit() : tblAcrDetail.Rows.Add(Row)
            End While : SqlRe.Close()
            'If IsChangedData Then
            '    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Changed_Acrue_BY_Year_Detail")
            '    Me.SqlRe = Me.SqlCom.ExecuteReader()
            '    While Me.SqlRe.Read()
            '        Dim ACHIEVEMENT_ID As String = SqlRe.GetString(1) + "|" + SqlRe.GetString(0) + "|Y"
            '        Dim BRANDPACK_ID As String = SqlRe.GetString(2)
            '        Dim AchievementBrandPackID As String = ACHIEVEMENT_ID + "|" + BRANDPACK_ID

            '        RowsSelect = tblAcrDetail.Select("ACHIEVEMENT_BRANDPACK_ID = '" & AchievementBrandPackID & "'")
            '        If RowsSelect.Length <= 0 Then
            '            Row = tblAcrDetail.NewRow()
            '            Row("ACHIEVEMENT_ID") = ACHIEVEMENT_ID
            '            Row("BRANDPACK_ID") = BRANDPACK_ID
            '            Row("ACHIEVEMENT_BRANDPACK_ID") = AchievementBrandPackID
            '            Row("IsNew") = False
            '            Row("IsChanged") = True
            '            Row("IsChanged") = False
            '            Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(3)
            '            Row("TOTAL_ACTUAL_AMOUNT") = SqlRe.GetDecimal(4)
            '            Row("TOTAL_PO_ORIGINAL") = SqlRe.GetDecimal(5)
            '            Row("TOTAL_PO_AMOUNT") = SqlRe.GetDecimal(6)
            '            Row.EndEdit() : tblAcrDetail.Rows.Add(Row)
            '        End If
            '    End While : SqlRe.Close()
            'End If
            Dim tblTempAccruedHeader As New DataTable("T_Temp_AccruedHeader")
            tblTempAccruedHeader.Clear()
            Dim tblTempAccruedDetail As New DataTable("T_Temp_AccruedDetail")
            tblTempAccruedDetail.Clear()
            Dim tblHeaderAchievement As New DataTable("T_Temp_HeaderAchievement")
            tblHeaderAchievement.Clear()
            Dim tblDetailAchievement As New DataTable("T_Temp_DetailAchievement")
            tblDetailAchievement.Clear()
            Dim tblYearHeaderJoin As New DataTable("T_PB_Year_Header_Join")
            tblYearHeaderJoin.Clear()

            Dim tblPBYearHeader As New DataTable("T_PB_Year_Header")
            tblPBYearHeader.Clear()

            Dim tblPBYearDetail As New DataTable("T_PB_Year_Detail")
            tblPBYearDetail.Clear()

            Dim tblGPyear As New DataTable("T_P_PB_Year")
            tblGPyear.Clear()
            If mustRecomputeYear Then
                'check ke database apakah ada total PO di semester
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ;" & vbCrLf & _
                        " SELECT 1 WHERE EXISTS(SELECT IDApp FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND FLAG = 'Y') ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Retval = Me.SqlCom.ExecuteScalar()
                If Not IsNothing(Retval) And Not IsDBNull(Retval) Then
                    If CInt(Retval) > 0 Then
                        'mulai hitung
                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT ACRH.*,CombinedWith = CASE ACRH.ISCOMBINED_TARGET WHEN 1 THEN (SELECT TOP 1 COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = ACRH.AGREEMENT_NO + '' + ACRH.BRAND_ID) ELSE '' END," & vbCrLf & _
                                " ACRH.AGREEMENT_NO + '' + ACRH.BRAND_ID AS AGREE_BRAND_ID,IsChanged = CONVERT(BIT,'1'),IsNew = CONVERT(BIT,'0'),IsFromAccHeader = CONVERT(BIT,'1') FROM ACCRUED_HEADER ACRH WHERE ACRH.AGREEMENT_NO = @AGREEMENT_NO AND ACRH.FLAG = 'Y' ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        setDataAdapter(Me.SqlCom).Fill(tblTempAccruedHeader)

                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT ACD.*,IsChanged = CONVERT(BIT,'1'),IsNew = CONVERT(BIT,'0') FROM ACCRUED_DETAIL ACD INNER JOIN ACCRUED_HEADER ACH ON ACH.ACHIEVEMENT_ID = ACD.ACHIEVEMENT_ID " & vbCrLf & _
                                " WHERE ACH.AGREEMENT_NO = @AGREEMENT_NO AND ACH.FLAG = 'Y' ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        setDataAdapter(Me.SqlCom).Fill(tblTempAccruedDetail)

                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; " & vbCrLf & _
                                " SELECT AGREEMENT_NO + BRAND_ID AS AGREE_BRAND_ID,SUM(TOTAL_PBQ3) + SUM(TOTAL_PBQ4) + SUM(TOTAL_PBS2) AS TOTAL_JOIN_PBY " & vbCrLf & _
                                " FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO " & vbCrLf & _
                                " AND FLAG != 'Y' GROUP BY AGREEMENT_NO + BRAND_ID ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        setDataAdapter(Me.SqlCom).Fill(tblYearHeaderJoin)

                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                                " SELECT DISTRIBUTOR_ID,AGREEMENT_NO + BRAND_ID AS AGREE_BRAND_ID,SUM(TOTAL_PBQ3) + SUM(TOTAL_PBQ4) + SUM(TOTAL_PBS2) AS TOTAL_PBY " & vbCrLf & _
                                " FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO " & vbCrLf & _
                                " AND FLAG != 'Y' GROUP BY DISTRIBUTOR_ID,AGREEMENT_NO + BRAND_ID ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        setDataAdapter(Me.SqlCom).Fill(tblPBYearHeader)

                        Query = "SELECT ACRH.DISTRIBUTOR_ID + '|' + ACRH.AGREEMENT_NO AS DISTRIBUTOR_AGREEMENT,ACRH.BRAND_ID,ACD.BRANDPACK_ID," & vbCrLf & _
                                " SUM(ACD.TOTAL_PBQ3) + SUM(ACD.TOTAL_PBQ4) + SUM(ACD.TOTAL_PBS2) AS TOTAL_PBY FROM ACCRUED_DETAIL ACD " & vbCrLf & _
                                " INNER JOIN ACCRUED_HEADER ACRH ON ACD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID " & vbCrLf & _
                                " WHERE ACRH.AGREEMENT_NO = @AGREEMENT_NO AND ACRH.FLAG != 'Y'" & vbCrLf & _
                                " GROUP BY ACRH.DISTRIBUTOR_ID + '|' + ACRH.AGREEMENT_NO,ACRH.BRAND_ID,ACD.BRANDPACK_ID "

                        Me.ResetCommandText(CommandType.Text, Query)
                        setDataAdapter(Me.SqlCom).Fill(tblPBYearDetail)

                        Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                                 "SELECT ACHIEVEMENT_ID = DA.DISTRIBUTOR_ID + '|' + ABI.AGREE_BRAND_ID + '|Y',ABI.AGREE_BRAND_ID,GP.IDApp,GP.PBY " & vbCrLf & _
                                 " FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRAND_INCLUDE ABI ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                                 " INNER JOIN GIVEN_PROGRESSIVE GP ON GP.AGREE_BRAND_ID = ABI.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        setDataAdapter(Me.SqlCom).Fill(tblGPyear)

                        'set tblAcrHeader dengan data yang ini 
                        Dim QueryMaster As String = "", QueryHeader As String = "", QueryDetail As String = ""
                        QueryMaster = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                                     "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_MASTER_PO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                                     " BEGIN DROP TABLE tempdb..##T_MASTER_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
                                     " SELECT PO_REF_NO,PO_REF_DATE,DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID,SPPB_QTY,PO_ORIGINAL_QTY,PO_AMOUNT = PO_ORIGINAL_QTY * PO_PRICE_PERQTY,RUN_NUMBER,IncludeDPD INTO tempdb..##T_MASTER_PO_" & Me.ComputerName & " FROM ( " & vbCrLf & _
                                     "  SELECT PO.PO_REF_NO,PO.PO_REF_DATE,PO.DISTRIBUTOR_ID,ABI.BRAND_ID,ABP.BRANDPACK_ID,OPB.PO_ORIGINAL_QTY,OPB.PO_PRICE_PERQTY,SB.SPPB_QTY,OOA.RUN_NUMBER ," & vbCrLf & _
                                     "  IncludeDPD = CASE  WHEN (OPB.ExcludeDPD = 1) THEN 'NO' " & vbCrLf & _
                                     "  WHEN EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PLANTATION_ID = OPB.PLANTATION_ID AND BRANDPACK_ID = OPB.BRANDPACK_ID AND DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID AND PRICE = OPB.PO_PRICE_PERQTY AND IncludeDPD = 1) THEN 'YESS' " & vbCrLf & _
                                     "  WHEN EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PLANTATION_ID = OPB.PLANTATION_ID AND BRANDPACK_ID = OPB.BRANDPACK_ID AND DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID AND PRICE = OPB.PO_PRICE_PERQTY AND IncludeDPD = 0) THEN 'NO' " & vbCrLf & _
                                     "  WHEN EXISTS(SELECT PB.BRANDPACK_ID FROM PROJ_PROJECT PROJ INNER JOIN PROJ_BRANDPACK PB ON PROJ.PROJ_REF_NO = PB.PROJ_REF_NO WHERE PROJ.PROJ_REF_NO = PO.PROJ_REF_NO AND PB.BRANDPACK_ID = OPB.BRANDPACK_ID AND PROJ.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID) THEN 'NO' " & vbCrLf & _
                                     "  WHEN  OPB.PLANTATION_ID IS NULL THEN 'YESS' ELSE 'NO' END " & vbCrLf & _
                                     "  FROM Nufarm.dbo.AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                                     "  INNER JOIN Nufarm.DBO.AGREE_BRANDPACK_INCLUDE ABP ON ABI.AGREE_BRAND_ID = ABP.AGREE_BRAND_ID" & vbCrLf & _
                                     "  INNER JOIN Nufarm.dbo.ORDR_PO_BRANDPACK OPB ON OPB.BRANDPACK_ID = ABP.BRANDPACK_ID " & vbCrLf & _
                                     "  INNER JOIN Nufarm.dbo.ORDR_PURCHASE_ORDER PO " & vbCrLf & _
                                     "  ON PO.PO_REF_NO = OPB.PO_REF_NO INNER JOIN Nufarm.dbo.ORDR_ORDER_ACCEPTANCE OOA " & vbCrLf & _
                                     "  ON OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                                     "  INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOA.OA_ID = OOAB.OA_ID AND OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                                     "  LEFT OUTER JOIN SPPB_BRANDPACK SB ON SB.OA_BRANDPACK_ID = OOAB.OA_BRANDPACK_ID  " & vbCrLf & _
                                     "  WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND OPB.PO_ORIGINAL_QTY > 0  " & vbCrLf & _
                                     "  AND PO.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                                     " )B ;" & vbCrLf & _
                                    " CREATE CLUSTERED INDEX IX_T_MASTER_PO ON ##T_MASTER_PO_" & Me.ComputerName & "(PO_REF_DATE,PO_REF_NO,RUN_NUMBER,DISTRIBUTOR_ID,BRANDPACK_ID) ;"
                        QueryHeader = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                                      " SELECT DISTRIBUTOR_ID,BRAND_ID,SUM(PO_ORIGINAL_QTY) AS TOTAL_PO_DISTRIBUTOR,SUM(PO_AMOUNT) AS PO_AMOUNT_DISTRIBUTOR FROM temp..##T_MASTER_PO_" & Me.ComputerName & " " & vbCrLf & _
                                      " GROUP BY DISTRIBUTOR_ID,BRAND_ID ;"
                        QueryDetail = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                                      "SELECT DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID,SUM(PO_ORIGINAL_QTY) AS TOTAL_PO_ORIGINAL,SUM(PO_AMOUNT) AS TOTAL_PO_AMOUNT FROM temp..##T_MASTER_PO_" & Me.ComputerName & " " & vbCrLf & _
                                      "GROUP BY DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID ;"
                        Me.ResetCommandText(CommandType.Text, QueryMaster)
                        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                        Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, QueryHeader)
                        setDataAdapter(Me.SqlCom).Fill(tblHeaderAchievement)
                        Me.ClearCommandParameters()

                        Me.AddParameter("@stmt", SqlDbType.NVarChar, QueryDetail)
                        Me.setDataAdapter(Me.SqlCom).Fill(tblDetailAchievement)
                        'buat column achievement id di tblheaderAchievement
                        Dim colAchievementID As New DataColumn("ACHIEVEMENT_ID", Type.GetType("System.String"))
                        tblHeaderAchievement.Columns.Add(colAchievementID)
                        tblHeaderAchievement.Columns("ACHIEVEMENT_ID").SetOrdinal(0)
                        'isi achievement dengan data distributor + | + AGREEMENT_NO + BRAND_ID + | + flag
                        Dim AchievementID As String = "", AchievementBrandPackID As String = ""
                        Dim TempRow As DataRow = Nothing, TotalPoDistributor As Decimal = 0, _
                        POAmountDistributor As Decimal = 0, TotalPOAmount As Decimal = 0, TotalPoOriginal As Decimal = 0, TotalPBY As Decimal, TotalJoinPBY As Decimal = 0
                        For i As Integer = 0 To tblHeaderAchievement.Rows.Count - 1
                            TempRow = tblHeaderAchievement.Rows(i)
                            AchievementID = TempRow("DISTRIBUTOR_ID").ToString() & "|" & AgreementNO & TempRow("BRAND_ID").ToString() & "|Y"
                            TempRow.BeginEdit()
                            TempRow("ACHIEVEMENT_ID") = AchievementID
                            TempRow.EndEdit()
                        Next
                        tblHeaderAchievement.AcceptChanges()
                        Dim colAchievementBrandPackID As New DataColumn("ACHIEVEMENT_BRANDPACK_ID", Type.GetType("System.String"))
                        tblDetailAchievement.Columns.Add(colAchievementBrandPackID)
                        tblDetailAchievement.Columns("ACHIEVEMENT_BRANDPACK_ID").SetOrdinal(0)
                        For i As Integer = 0 To tblDetailAchievement.Rows.Count - 1
                            TempRow = tblDetailAchievement.Rows(i)
                            AchievementBrandPackID = TempRow("DISTRIBUTOR_ID").ToString() & "|" & AgreementNO & TempRow("BRAND_ID").ToString() & "|Y|" & TempRow("BRANDPACK_ID").ToString()
                            TempRow.BeginEdit()
                            TempRow("ACHIEVEMENT_BRANDPACK_ID") = AchievementBrandPackID
                            TempRow.EndEdit()
                        Next
                        tblDetailAchievement.AcceptChanges()
                        Dim rows() As DataRow = Nothing
                        'jika row di tblAccruedHeader belum ada di table tblAcrHeader
                        'import row dari tblAccruedHeader ke tblAcrHeader
                        'isi total PO_DISTRIBUTOR dengan data yang ada di tblHeaderAchievement
                        'dan jika sudah ada maka edit data TOTAL_PO_DISTRIBUTOR di tblAcrHeader
                        ' dengan total_po di tblHeaderAchievement
                        For i As Integer = 0 To tblTempAccruedHeader.Rows.Count - 1
                            rows = tblAcrHeader.Select("ACHIEVEMENT_ID = '" & tblTempAccruedHeader.Rows(i)("ACHIEVEMENT_ID").ToString() & "'")
                            If rows.Length <= 0 Then
                                'import row dari tblAccruedHeader ke tblAcrHeader
                                TempRow = tblTempAccruedHeader.Rows(i)
                                tblAcrHeader.ImportRow(TempRow)
                                If Not ListAgreeBrand.Contains(TempRow("AGREE_BRAND_ID").ToString()) Then
                                    ListAgreeBrand.Add(TempRow("AGREE_BRAND_ID").ToString())
                                End If
                            End If
                        Next : tblAcrHeader.AcceptChanges()

                        For i As Integer = 0 To tblAcrHeader.Rows.Count - 1
                            TempRow = tblAcrHeader.Rows(i)
                            Dim BPPBY As Decimal = 0, GPID As Int64 = 0
                            'isi total podistributor dari table header Achievement
                            rows = tblHeaderAchievement.Select("ACHIEVEMENT_ID = '" & TempRow("ACHIEVEMENT_ID").ToString() & "'")
                            If rows.Length > 0 Then
                                If Not IsDBNull(rows(0)("TOTAL_PO_DISTRIBUTOR")) Then
                                    TotalPoDistributor = Convert.ToDecimal(rows(0)("TOTAL_PO_DISTRIBUTOR"))
                                    POAmountDistributor = Convert.ToDecimal(rows(0)("PO_AMOUNT_DISTRIBUTOR"))
                                Else : TotalPoDistributor = 0 : POAmountDistributor = 0
                                End If
                            Else : TotalPoDistributor = 0 : POAmountDistributor = 0
                            End If

                            'isi total PBY dan total join PBY dengan table pbYearheader
                            rows = tblYearHeaderJoin.Select("AGREE_BRAND_ID = '" & TempRow("AGREE_BRAND_ID").ToString() & "'")
                            If rows.Length > 0 Then
                                If Not IsDBNull(rows(0)("TOTAL_JOIN_PBY")) And Not IsNothing(rows(0)("TOTAL_JOIN_PBY")) Then
                                    TotalJoinPBY = Convert.ToDecimal(rows(0)("TOTAL_JOIN_PBY"))
                                Else : TotalJoinPBY = 0
                                End If
                            End If

                            rows = tblPBYearHeader.Select("DISTRIBUTOR_ID = '" & TempRow("DISTRIBUTOR_ID").ToString() & "' AND AGREE_BRAND_ID = '" & TempRow("AGREE_BRAND_ID").ToString() & "'")
                            If rows.Length > 0 Then
                                If Not IsDBNull(rows(0)("TOTAL_PBY")) And Not IsNothing(rows(0)("TOTAL_PBY")) Then
                                    TotalPBY = Convert.ToDecimal(rows(0)("TOTAL_PBY"))
                                Else : TotalPBY = 0
                                End If
                            Else : TotalPBY = 0
                            End If

                            rows = tblGPyear.Select("ACHIEVEMENT_ID = '" & TempRow("ACHIEVEMENT_ID").ToString() & "'")
                            If rows.Length > 0 Then
                                BPPBY = Convert.ToDecimal(rows(0)("PBY"))
                                GPID = Convert.ToInt64(rows(0)("IDApp"))
                            End If
                            TempRow.BeginEdit()
                            TempRow("TOTAL_PO_DISTRIBUTOR") = TotalPoDistributor
                            TempRow("PO_AMOUNT_DISTRIBUTOR") = POAmountDistributor
                            TempRow("TOTAL_PBY") = TotalPBY
                            TempRow("TOTAL_JOIN_PBY") = TotalJoinPBY
                            TempRow("GPPBY") = BPPBY
                            If GPID > 0 Then
                                TempRow("GP_ID") = GPID
                            End If
                            TempRow.EndEdit()
                        Next : tblAcrHeader.AcceptChanges()
                        For i As Integer = 0 To ListAgreeBrand.Count - 1
                            TotalPOAmount = 0
                            Dim OTotalPOOri As Object = tblAcrHeader.Compute("SUM(TOTAL_PO_DISTRIBUTOR)", "AGREE_BRAND_ID = '" & ListAgreeBrand(i) & "'")
                            If Not IsDBNull(OTotalPOOri) And Not IsNothing(OTotalPOOri) Then
                                TotalPoOriginal = Convert.ToDecimal(OTotalPOOri)
                            End If
                            Dim OTotalPOAMount As Object = tblAcrHeader.Compute("SUM(PO_AMOUNT_DISTRIBUTOR)", "AGREE_BRAND_ID = '" & ListAgreeBrand(i) & "'")
                            If Not IsDBNull(OTotalPOAMount) And Not IsNothing(OTotalPOAMount) Then
                                TotalPOAmount = Convert.ToDecimal(OTotalPOAMount)
                            End If

                            rows = tblAcrHeader.Select("AGREE_BRAND_ID = '" & ListAgreeBrand(i) & "'")
                            If rows.Length > 0 Then
                                For i1 As Integer = 0 To rows.Length - 1
                                    rows(i1).BeginEdit()
                                    rows(i1)("TOTAL_PO_ORIGINAL") = TotalPoOriginal
                                    rows(i1)("TOTAL_PO_AMOUNT") = TotalPOAmount
                                    rows(i1).EndEdit()
                                Next
                            End If
                        Next : tblAcrHeader.AcceptChanges()
                        'hitung PB Year

                        'sekarang ngitung recehan nya
                        tblPBYearDetail.Columns.Add(New DataColumn("ACHIEVEMENT_BRANDPACK_ID", Type.GetType("System.String")))
                        For i As Integer = 0 To tblPBYearDetail.Rows.Count - 1
                            TempRow = tblPBYearDetail.Rows(i)
                            AchievementBrandPackID = TempRow("DISTRIBUTOR_AGREEMENT").ToString() & "" & TempRow("BRAND_ID").ToString() & "|Y|" & TempRow("BRANDPACK_ID").ToString()
                            TempRow.BeginEdit()
                            TempRow("ACHIEVEMENT_BRANDPACK_ID") = AchievementBrandPackID
                            TempRow.EndEdit()
                        Next : tblPBYearDetail.AcceptChanges()

                        tblAcrDetail.AcceptChanges()
                        For i As Integer = 0 To tblTempAccruedDetail.Rows.Count - 1
                            rows = tblAcrDetail.Select("ACHIEVEMENT_BRANDPACK_ID = '" & tblTempAccruedDetail.Rows(i)("ACHIEVEMENT_BRANDPACK_ID").ToString() & "'")
                            If rows.Length <= 0 Then
                                TempRow = tblTempAccruedDetail.Rows(i)
                                tblAcrDetail.ImportRow(TempRow)
                            End If
                        Next : tblAcrDetail.AcceptChanges()
                        For i As Integer = 0 To tblAcrDetail.Rows.Count - 1
                            Dim TPOOriginal As Decimal = 0, TPBYearDetail As Decimal = 0
                            TempRow.BeginEdit()
                            TempRow = tblAcrDetail.Rows(i)
                            rows = tblDetailAchievement.Select("ACHIEVEMENT_BRANDPACK_ID = '" & tblAcrDetail.Rows(i)("ACHIEVEMENT_BRANDPACK_ID").ToString() & "'")
                            If rows.Length > 0 Then
                                TempRow("TOTAL_PO_ORIGINAL") = rows(0)("TOTAL_PO_ORIGINAL")
                                TempRow("TOTAL_PO_AMOUNT") = rows(0)("TOTAL_PO_AMOUNT")
                            End If
                            rows = tblPBYearDetail.Select("ACHIEVEMENT_BRANDPACK_ID = '" & TempRow("ACHIEVEMENT_BRANDPACK_ID").ToString() & "'")
                            If rows.Length > 0 Then
                                TempRow("TOTAL_PBY") = rows(0)("TOTAL_PBY")
                            End If
                            TempRow.EndEdit()
                        Next : tblAcrDetail.AcceptChanges()
                    End If
                End If
            End If

        End Sub

        Protected Sub ResetAdapterCRUD()
            If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter()
            End If
            Me.SqlDat.InsertCommand = Nothing
            Me.SqlDat.UpdateCommand = Nothing
            Me.SqlDat.DeleteCommand = Nothing
        End Sub
        Private Sub SaveToDataBase(ByVal AgreementNO As String, ByVal FLAG As String, ByVal ListAgreeBrand As List(Of String), _
        ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByRef tblAcrHeader As DataTable, ByRef tblAcrDetail As DataTable, _
        ByRef Message As String, ByVal EndDateAgreement As DateTime, ByVal IsTargetGroup As Boolean)
            Dim RowsSelect() As DataRow = Nothing, strFlag As String = ""
            If FLAG <> "Y" Then
                If EndDateAgreement < New DateTime(2010, 9, 1) Then
                    strFlag = FLAG.Remove(1, 1)
                Else
                    strFlag = FLAG
                End If
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                        " IF EXISTS(SELECT ABD.AGREE_BRAND_ID FROM AGREE_PROG_DISC ABD INNER JOIN AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                        "           ON ABD.AGREE_BRAND_ID = ABI.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND ABD.QSY_DISC_FLAG = '" & strFlag & "') " & vbCrLf & _
                        "    BEGIN " & vbCrLf & _
                        "       SELECT ABD.AGREE_BRAND_ID,ABD.UP_TO_PCT,ABD.PRGSV_DISC_PCT FROM AGREE_PROG_DISC ABD INNER JOIN AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                        "       ON ABI.AGREE_BRAND_ID = ABD.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND ABD.QSY_DISC_FLAG = '" & strFlag & "' ORDER BY UP_TO_PCT DESC ;" & vbCrLf & _
                        "       SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = @AGREEMENT_NO AND QSY_DISC_FLAG = '" & strFlag.Substring(0, 1) & "'  ORDER BY UP_TO_PCT DESC;" & vbCrLf & _
                        "    END " & vbCrLf & _
                        " ELSE " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "    SELECT ABD.AGREE_BRAND_ID,ABD.UP_TO_PCT,ABD.PRGSV_DISC_PCT FROM AGREE_PROG_DISC ABD INNER JOIN AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                        "    ON ABI.AGREE_BRAND_ID = ABD.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND ABD.QSY_DISC_FLAG = '" & strFlag.Substring(0, 1) & "' ORDER BY UP_TO_PCT DESC ;" & vbCrLf & _
                        "    SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = @AGREEMENT_NO AND QSY_DISC_FLAG = '" & strFlag.Substring(0, 1) & "' ORDER BY UP_TO_PCT DESC ;" & vbCrLf & _
                        " END "
            Else
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT ABD.AGREE_BRAND_ID,ABD.UP_TO_PCT,ABD.PRGSV_DISC_PCT FROM AGREE_PROG_DISC ABD INNER JOIN AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                        " ON ABI.AGREE_BRAND_ID = ABD.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND ABD.QSY_DISC_FLAG = 'Y' ORDER BY UP_TO_PCT DESC OPTION(KEEP PLAN);" & vbCrLf & _
                        "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = @AGREEMENT_NO AND QSY_DISC_FLAG = 'Y' ORDER BY UP_TO_PCT DESC OPTION(KEEP PLAN);"
            End If

            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNO, 32)
            Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.baseDataSet = New DataSet
            Me.baseDataSet.Clear() : Me.SqlDat.Fill(Me.baseDataSet)
            'Sekarang baru clearkan parameter @AGREEMENT_NO
            Me.ClearCommandParameters()
            ''pisahkan yang baru dengan yang haschanged
            Dim DVCustom As DataView = Me.baseDataSet.Tables(0).DefaultView, _
            DVTypical As DataView = Me.baseDataSet.Tables(1).DefaultView
            DVCustom.RowFilter = "" : DVTypical.RowFilter = ""

            If Not IsNothing(ListAgreeBrand) Then : If ListAgreeBrand.Count > 0 Then : ListAgreeBrand.Clear() : End If : End If
            ''ambil data ke database data periode before year dan masukan ke datatable header begitu juga dengan detail tablenya
            ''agar bisa di proses di prosedure calculateDetail dan setInvoicebefore
            ''datatable ini harus selalu di clearkan setiap beda agreement
            Me.CreateOrReCreateTblPeriodeBeforeYearHeader(AgreementNO, True, False)
            '----------------------PROSES DISCOUNT-----------------------
            '-------------------------------------------------------------------------------
            ''hitung bonusnya
            Me.CalculateHeader(FLAG, ListAgreeBrand, "IsNew", tblAcrHeader, DVCustom, DVTypical, AgreementNO, Message, StartDate, EndDate, IsTargetGroup)
            'reset dataview
            DVCustom.RowFilter = "" : DVTypical.RowFilter = ""
            Me.CalculateHeader(FLAG, ListAgreeBrand, "IsChanged", tblAcrHeader, DVCustom, DVTypical, AgreementNO, Message, StartDate, EndDate, IsTargetGroup)
            Dim AchievementBrandPacks As New List(Of String), ListCanUpdateAB As New List(Of String), _
             strAchievementBrandPacks As String = "IN('"
            ''ambil data ke database data periode before year dan masukan ke datatable detail begitu 
            ''agar bisa di proses di prosedure calculate datail dan setInvoicebefore
            ''datatable ini harus selalu di clearkan setiap beda agreement
            Me.CreateOrReCreateTblPeriodeBeforeYearDetail(AgreementNO, True, False)
            Me.CalculateDetail(FLAG, "IsNew", tblAcrDetail, tblAcrHeader, AchievementBrandPacks)
            Me.CalculateDetail(FLAG, "IsChanged", tblAcrDetail, tblAcrHeader, AchievementBrandPacks)
            For i As Integer = 0 To AchievementBrandPacks.Count - 1
                strAchievementBrandPacks &= "" & AchievementBrandPacks(i) & "'"
                If i < AchievementBrandPacks.Count - 1 Then
                    strAchievementBrandPacks &= ",'"
                End If
            Next
            strAchievementBrandPacks &= ")"
            If AchievementBrandPacks.Count > 0 Then
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                       "SELECT ACHIEVEMENT_BRANDPACK_ID FROM ACCRUED_DETAIL AD WHERE RELEASE_QTY > 0 AND CAN_RELEASE = 1 " & vbCrLf & _
                       "AND (EXISTS(SELECT ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_REMAINDING WHERE ACHIEVEMENT_BRANDPACK_ID IS NOT NULL " & vbCrLf & _
                       " AND ACHIEVEMENT_BRANDPACK_ID = AD.ACHIEVEMENT_BRANDPACK_ID) " & vbCrLf & _
                       " OR EXISTS(SELECT ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_BRANDPACK_DISC WHERE ACHIEVEMENT_BRANDPACK_ID IS NOT NULL " & vbCrLf & _
                       " AND ACHIEVEMENT_BRANDPACK_ID = AD.ACHIEVEMENT_BRANDPACK_ID)) AND CAN_UPDATE = 1 " & vbCrLf & _
                       " AND ACHIEVEMENT_BRANDPACK_ID " & strAchievementBrandPacks & " OPTION(KEEP PLAN);"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    ListCanUpdateAB.Add(SqlRe.GetString(0))
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
            End If
            '-------------------CHEK APAKAAH DISCOUNT SUDAH DI PAKE DI OA---------------------------
            '---------------------------------------------------------------------------------
            Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
            'header dulu

            'RowsSelect = tblAcrHeader.Select("IsNew = " & True, "", DataViewRowState.Added)
            RowsSelect = tblAcrHeader.Select("IsNew = " & True)
            If RowsSelect.Length > 0 Then
                For i As Integer = 0 To RowsSelect.Length - 1
                    RowsSelect(i).SetAdded()
                Next
            End If
            ''reset CRUD (insert,Update,Delete) command to Adapter
            If RowsSelect.Length > 0 Then
                Me.ResetAdapterCRUD()
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                        "IF NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER WHERE ACHIEVEMENT_ID = @ACHIEVEMENT_ID) " & vbCrLf & _
                        "BEGIN " & vbCrLf & _
                        "INSERT INTO ACCRUED_HEADER(GP_ID,TOTAL_PO_ORIGINAL,TOTAL_PO_AMOUNT,TOTAL_PO_DISTRIBUTOR,PO_AMOUNT_DISTRIBUTOR,TOTAL_PBQ3,TOTAL_JOIN_PBQ3,TOTAL_JOIN_PBQ4,TOTAL_PBQ4,TOTAL_JOIN_PBS2,TOTAL_PBS2,TOTAL_JOIN_CPQ1," & vbCrLf & _
                        "TOTAL_CPQ1,CPQ1_AMOUNT,TOTAL_JOIN_CPQ2,TOTAL_CPQ2,CPQ2_AMOUNT,TOTAL_JOIN_CPQ3,TOTAL_CPQ3,CPQ3_AMOUNT,TOTAL_JOIN_CPS1,TOTAL_CPS1,CPS1_AMOUNT,TOTAL_JOIN_PBY,TOTAL_PBY,ACHIEVEMENT_ID,AGREEMENT_NO,DISTRIBUTOR_ID,BRAND_ID,FLAG," & vbCrLf & _
                        "ISTARGET_GROUP,TARGET,TARGET_FM,TARGET_PL,DISPRO,TOTAL_ACTUAL,TOTAL_ACTUAL_AMOUNT,ACTUAL_DISTRIBUTOR,ACTUAL_AMOUNT_DISTRIBUTOR,ACHIEVEMENT_DISPRO,BONUS_QTY,BONUS_DISTRIBUTOR,BALANCE,ISCOMBINED_TARGET,DESCRIPTION,DISC_BY_VOLUME,DISC_DIST_BY_VOLUME,DISC_OBTAINED_FROM,AGREE_ACH_BY,COMBINED_BRAND_ID,CREATE_BY,CREATE_DATE)" & vbCrLf & _
                        "VALUES(@GP_ID,@TOTAL_PO_ORIGINAL,@TOTAL_PO_AMOUNT,@TOTAL_PO_DISTRIBUTOR,@PO_AMOUNT_DISTRIBUTOR,@TOTAL_PBQ3,@TOTAL_JOIN_PBQ3,@TOTAL_JOIN_PBQ4,@TOTAL_PBQ4,@TOTAL_JOIN_PBS2,@TOTAL_PBS2,@TOTAL_JOIN_CPQ1," & vbCrLf & _
                        "@TOTAL_CPQ1,@CPQ1_AMOUNT,@TOTAL_JOIN_CPQ2,@TOTAL_CPQ2,@CPQ2_AMOUNT,@TOTAL_JOIN_CPQ3,@TOTAL_CPQ3,@CPQ3_AMOUNT,@TOTAL_JOIN_CPS1,@TOTAL_CPS1,@CPS1_AMOUNT,@TOTAL_JOIN_PBY,@TOTAL_PBY,@ACHIEVEMENT_ID,@AGREEMENT_NO,@DISTRIBUTOR_ID,@BRAND_ID,@FLAG," & vbCrLf & _
                        "@ISTARGET_GROUP,@TARGET,@TARGET_FM,@TARGET_PL,@DISPRO,@TOTAL_ACTUAL,@TOTAL_ACTUAL_AMOUNT,@ACTUAL_DISTRIBUTOR,@ACTUAL_AMOUNT_DISTRIBUTOR," & vbCrLf & _
                        "@ACHIEVEMENT_DISPRO,@BONUS_QTY,@BONUS_DISTRIBUTOR,@BALANCE,@ISCOMBINED_TARGET,@DESCRIPTION,@DISC_BY_VOLUME,@DISC_DIST_BY_VOLUME,'T1',@AGREE_ACH_BY,REPLACE(@COMBINED_BRAND_ID,@AGREEMENT_NO,''),@CREATE_BY,@CREATE_DATE); " & vbCrLf & _
                        " END"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.SqlCom.Parameters.Add("@GP_ID", SqlDbType.BigInt, 0, "GP_ID")
                Me.SqlCom.Parameters.Add("@TOTAL_PO_ORIGINAL", SqlDbType.Decimal, 0, "TOTAL_PO_ORIGINAL")
                Me.SqlCom.Parameters.Add("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0, "TOTAL_PO_AMOUNT")
                Me.SqlCom.Parameters.Add("@TOTAL_PO_DISTRIBUTOR", SqlDbType.Decimal, 0, "TOTAL_PO_DISTRIBUTOR")
                Me.SqlCom.Parameters.Add("@PO_AMOUNT_DISTRIBUTOR", SqlDbType.Decimal, 0, "PO_AMOUNT_DISTRIBUTOR")

                Me.SqlCom.Parameters.Add("@TOTAL_PBQ3", SqlDbType.Decimal, 0, "TOTAL_PBQ3")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_PBQ3", SqlDbType.Decimal, 0, "TOTAL_JOIN_PBQ3")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_PBQ4", SqlDbType.Decimal, 0, "TOTAL_JOIN_PBQ4")
                Me.SqlCom.Parameters.Add("@TOTAL_PBQ4", SqlDbType.Decimal, 0, "TOTAL_PBQ4")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_PBS2", SqlDbType.Decimal, 0, "TOTAL_JOIN_PBS2")
                Me.SqlCom.Parameters.Add("@TOTAL_PBS2", SqlDbType.Decimal, 0, "TOTAL_PBS2")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_CPQ1", SqlDbType.Decimal, 0, "TOTAL_JOIN_CPQ1")
                Me.SqlCom.Parameters.Add("@TOTAL_CPQ1", SqlDbType.Decimal, 0, "TOTAL_CPQ1")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_CPQ2", SqlDbType.Decimal, 0, "TOTAL_JOIN_CPQ2")
                Me.SqlCom.Parameters.Add("@TOTAL_CPQ2", SqlDbType.Decimal, 0, "TOTAL_CPQ2")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_CPQ3", SqlDbType.Decimal, 0, "TOTAL_JOIN_CPQ3")
                Me.SqlCom.Parameters.Add("@TOTAL_CPQ3", SqlDbType.Decimal, 0, "TOTAL_CPQ3")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_CPS1", SqlDbType.Decimal, 0, "TOTAL_JOIN_CPS1")
                Me.SqlCom.Parameters.Add("@TOTAL_CPS1", SqlDbType.Decimal, 0, "TOTAL_CPS1")

                Me.SqlCom.Parameters.Add("@CPQ1_AMOUNT", SqlDbType.Decimal, 0, "CPQ1_AMOUNT")
                Me.SqlCom.Parameters.Add("@CPQ2_AMOUNT", SqlDbType.Decimal, 0, "CPQ2_AMOUNT")
                Me.SqlCom.Parameters.Add("@CPQ3_AMOUNT", SqlDbType.Decimal, 0, "CPQ3_AMOUNT")
                Me.SqlCom.Parameters.Add("@CPS1_AMOUNT", SqlDbType.Decimal, 0, "CPS1_AMOUNT")

                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_PBY", SqlDbType.Decimal, 0, "TOTAL_JOIN_PBY")
                Me.SqlCom.Parameters.Add("@TOTAL_PBY", SqlDbType.Decimal, 0, "TOTAL_PBY")
                Me.SqlCom.Parameters.Add("@DISC_BY_VOLUME", SqlDbType.Decimal, 0, "DISC_BY_VOLUME")
                Me.SqlCom.Parameters.Add("@DISC_DIST_BY_VOLUME", SqlDbType.Decimal, 0, "DISC_DIST_BY_VOLUME")
                Me.SqlCom.Parameters.Add("@AGREE_ACH_BY", SqlDbType.Char, 3, "AGREE_ACH_BY")
                Me.SqlCom.Parameters.Add("@COMBINED_BRAND_ID", SqlDbType.VarChar, 50, "CombinedWith")
                'Me.AddParameter("@GPCPQ3", SqlDbType.Decimal, Convert.ToDecimal(RowsSelect(i)("GPCPQ3")))
                'Me.AddParameter("@GPCPS1", SqlDbType.Decimal, Convert.ToDecimal(RowsSelect(i)("GPCPS1")))
                Me.SqlCom.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar, 200, "DESCRIPTION")
                Me.SqlCom.Parameters.Add("@ACHIEVEMENT_ID", SqlDbType.VarChar, 55, "ACHIEVEMENT_ID")
                Me.SqlCom.Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 32, "AGREEMENT_NO")
                Me.SqlCom.Parameters.Add("@DISTRIBUTOR_ID", SqlDbType.VarChar, 16, "DISTRIBUTOR_ID")
                Me.SqlCom.Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 7, "BRAND_ID")
                Me.SqlCom.Parameters.Add("@FLAG", SqlDbType.VarChar, 2, "FLAG")
                Me.SqlCom.Parameters.Add("@ISTARGET_GROUP", SqlDbType.Bit, 0, "ISTARGET_GROUP")
                Me.SqlCom.Parameters.Add("@TARGET", SqlDbType.Decimal, 0, "TARGET")
                Me.SqlCom.Parameters.Add("@TARGET_FM", SqlDbType.Decimal, 0, "TARGET_FM")
                Me.SqlCom.Parameters.Add("@TARGET_PL", SqlDbType.Decimal, 0, "TARGET_PL")
                Me.SqlCom.Parameters.Add("@DISPRO", SqlDbType.Decimal, 0, "DISPRO")
                Me.SqlCom.Parameters.Add("@TOTAL_ACTUAL", SqlDbType.Decimal, 0, "TOTAL_ACTUAL")
                Me.SqlCom.Parameters.Add("@TOTAL_ACTUAL_AMOUNT", SqlDbType.Decimal, 0, "TOTAL_ACTUAL_AMOUNT")

                Me.SqlCom.Parameters.Add("@ACTUAL_DISTRIBUTOR", SqlDbType.Decimal, 0, "ACTUAL_DISTRIBUTOR")
                Me.SqlCom.Parameters.Add("@ACTUAL_AMOUNT_DISTRIBUTOR", SqlDbType.Decimal, 0, "ACTUAL_AMOUNT_DISTRIBUTOR")

                Me.SqlCom.Parameters.Add("@ACHIEVEMENT_DISPRO", SqlDbType.Decimal, 0, "ACHIEVEMENT_DISPRO")
                Me.SqlCom.Parameters.Add("@BONUS_QTY", SqlDbType.Decimal, 0, "BONUS_QTY")
                Me.SqlCom.Parameters.Add("@BONUS_DISTRIBUTOR", SqlDbType.Decimal, 0, "BONUS_DISTRIBUTOR")
                Me.SqlCom.Parameters.Add("@BALANCE", SqlDbType.Decimal, 0, "BALANCE")
                Me.SqlCom.Parameters.Add("@ISCOMBINED_TARGET", SqlDbType.Bit, 0, "ISCOMBINED_TARGET")
                Me.SqlCom.Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 100, "CREATE_BY")
                Me.SqlCom.Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_DATE")
                Me.SqlDat.InsertCommand = Me.SqlCom
                Me.SqlDat.Update(RowsSelect) : Me.ClearCommandParameters()
            End If

            '----------------------------------------------------------------------------------
            RowsSelect = tblAcrHeader.Select("IsChanged = " & True)
            If RowsSelect.Length > 0 Then
                For i As Integer = 0 To RowsSelect.Length - 1
                    RowsSelect(i).SetModified()
                Next
                Me.ResetAdapterCRUD()

                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                        "IF EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER WHERE ACHIEVEMENT_ID = @ACHIEVEMENT_ID) " & vbCrLf & _
                        "BEGIN " & vbCrLf & _
                        "UPDATE ACCRUED_HEADER SET GP_ID = @GP_ID,TOTAL_PO_ORIGINAL = @TOTAL_PO_ORIGINAL,TOTAL_PO_AMOUNT = @TOTAL_PO_AMOUNT,TOTAL_PO_DISTRIBUTOR = @TOTAL_PO_DISTRIBUTOR,PO_AMOUNT_DISTRIBUTOR = @PO_AMOUNT_DISTRIBUTOR, " & vbCrLf & _
                        "TOTAL_PBQ3 = @TOTAL_PBQ3,TOTAL_JOIN_PBQ3 = @TOTAL_JOIN_PBQ3,TOTAL_JOIN_PBQ4 = @TOTAL_JOIN_PBQ4,TOTAL_PBQ4 = @TOTAL_PBQ4,TOTAL_JOIN_PBS2 =  @TOTAL_JOIN_PBS2," & vbCrLf & _
                        "TOTAL_PBS2 = @TOTAL_PBS2,TOTAL_JOIN_CPQ1 = @TOTAL_JOIN_CPQ1,TOTAL_CPQ1 = @TOTAL_CPQ1,CPQ1_AMOUNT = @CPQ1_AMOUNT,TOTAL_JOIN_CPQ2 = @TOTAL_JOIN_CPQ2," & vbCrLf & _
                        "TOTAL_CPQ2 = @TOTAL_CPQ2,CPQ2_AMOUNT = @CPQ2_AMOUNT,TOTAL_JOIN_CPQ3 = @TOTAL_JOIN_CPQ3,TOTAL_CPQ3 = @TOTAL_CPQ3,CPQ3_AMOUNT = @CPQ3_AMOUNT,TOTAL_JOIN_CPS1 = @TOTAL_JOIN_CPS1," & vbCrLf & _
                        "TOTAL_CPS1 = @TOTAL_CPS1,CPS1_AMOUNT = @CPS1_AMOUNT,TOTAL_PBY = @TOTAL_PBY,TOTAL_JOIN_PBY = @TOTAL_JOIN_PBY,TOTAL_ACTUAL = @TOTAL_ACTUAL,TOTAL_ACTUAL_AMOUNT = @TOTAL_ACTUAL_AMOUNT, " & vbCrLf & _
                        " ACTUAL_DISTRIBUTOR = @ACTUAL_DISTRIBUTOR, ACTUAL_AMOUNT_DISTRIBUTOR = @ACTUAL_AMOUNT_DISTRIBUTOR, " & vbCrLf & _
                        " ACHIEVEMENT_DISPRO = @ACHIEVEMENT_DISPRO,DISPRO = @DISPRO,BONUS_QTY = @BONUS_QTY,DISC_BY_VOLUME = @DISC_BY_VOLUME,BONUS_DISTRIBUTOR = @BONUS_DISTRIBUTOR,DISC_DIST_BY_VOLUME = @DISC_DIST_BY_VOLUME, " & vbCrLf & _
                        "BALANCE = @BALANCE,ISCOMBINED_TARGET = @ISCOMBINED_TARGET,LAST_UPDATE = GETDATE()," & vbCrLf & _
                        "MODIFY_BY = @MODIFY_BY,MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101),DESCRIPTION = @DESCRIPTION " & vbCrLf & _
                        " WHERE ACHIEVEMENT_ID = @ACHIEVEMENT_ID; " & vbCrLf & _
                        " IF @COMBINED_BRAND_ID IS NOT NULL " & vbCrLf & _
                        " BEGIN UPDATE ACCRUED_HEADER SET COMBINED_BRAND_ID = REPLACE(@COMBINED_BRAND_ID,@AGREEMENT_NO,'') " & vbCrLf & _
                        " WHERE ACHIEVEMENT_ID = @ACHIEVEMENT_ID;  END " & vbCrLf & _
                        "END "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.SqlCom.Parameters.Add("@GP_ID", SqlDbType.BigInt, 0, "GP_ID")
                Me.SqlCom.Parameters.Add("@TOTAL_PO_ORIGINAL", SqlDbType.Decimal, 0, "TOTAL_PO_ORIGINAL")
                Me.SqlCom.Parameters.Add("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0, "TOTAL_PO_AMOUNT")
                Me.SqlCom.Parameters.Add("@TOTAL_PO_DISTRIBUTOR", SqlDbType.Decimal, 0, "TOTAL_PO_DISTRIBUTOR")
                Me.SqlCom.Parameters.Add("@PO_AMOUNT_DISTRIBUTOR", SqlDbType.Decimal, 0, "PO_AMOUNT_DISTRIBUTOR")

                Me.SqlCom.Parameters.Add("@TOTAL_PBQ3", SqlDbType.Decimal, 0, "TOTAL_PBQ3")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_PBQ3", SqlDbType.Decimal, 0, "TOTAL_JOIN_PBQ3")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_PBQ4", SqlDbType.Decimal, 0, "TOTAL_JOIN_PBQ4")
                Me.SqlCom.Parameters.Add("@TOTAL_PBQ4", SqlDbType.Decimal, 0, "TOTAL_PBQ4")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_PBS2", SqlDbType.Decimal, 0, "TOTAL_JOIN_PBS2")
                Me.SqlCom.Parameters.Add("@TOTAL_PBS2", SqlDbType.Decimal, 0, "TOTAL_PBS2")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_CPQ1", SqlDbType.Decimal, 0, "TOTAL_JOIN_CPQ1")
                Me.SqlCom.Parameters.Add("@TOTAL_CPQ1", SqlDbType.Decimal, 0, "TOTAL_CPQ1")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_CPQ2", SqlDbType.Decimal, 0, "TOTAL_JOIN_CPQ2")
                Me.SqlCom.Parameters.Add("@TOTAL_CPQ2", SqlDbType.Decimal, 0, "TOTAL_CPQ2")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_CPQ3", SqlDbType.Decimal, 0, "TOTAL_JOIN_CPQ3")
                Me.SqlCom.Parameters.Add("@TOTAL_CPQ3", SqlDbType.Decimal, 0, "TOTAL_CPQ3")
                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_CPS1", SqlDbType.Decimal, 0, "TOTAL_JOIN_CPS1")
                Me.SqlCom.Parameters.Add("@TOTAL_CPS1", SqlDbType.Decimal, 0, "TOTAL_CPS1")

                Me.SqlCom.Parameters.Add("@CPQ1_AMOUNT", SqlDbType.Decimal, 0, "CPQ1_AMOUNT")
                Me.SqlCom.Parameters.Add("@CPQ2_AMOUNT", SqlDbType.Decimal, 0, "CPQ2_AMOUNT")
                Me.SqlCom.Parameters.Add("@CPQ3_AMOUNT", SqlDbType.Decimal, 0, "CPQ3_AMOUNT")
                Me.SqlCom.Parameters.Add("@CPS1_AMOUNT", SqlDbType.Decimal, 0, "CPS1_AMOUNT")

                Me.SqlCom.Parameters.Add("@TOTAL_JOIN_PBY", SqlDbType.Decimal, 0, "TOTAL_JOIN_PBY")
                Me.SqlCom.Parameters.Add("@TOTAL_PBY", SqlDbType.Decimal, 0, "TOTAL_PBY")
                Me.SqlCom.Parameters.Add("@DISC_BY_VOLUME", SqlDbType.Decimal, 0, "DISC_BY_VOLUME")
                Me.SqlCom.Parameters.Add("@DISC_DIST_BY_VOLUME", SqlDbType.Decimal, 0, "DISC_DIST_BY_VOLUME")
                Me.SqlCom.Parameters.Add("@AGREE_ACH_BY", SqlDbType.Char, 3, "AGREE_ACH_BY")
                Me.SqlCom.Parameters.Add("@COMBINED_BRAND_ID", SqlDbType.VarChar, 50, "CombinedWith")
                'Me.AddParameter("@GPCPQ3", SqlDbType.Decimal, Convert.ToDecimal(RowsSelect(i)("GPCPQ3")))
                'Me.AddParameter("@GPCPS1", SqlDbType.Decimal, Convert.ToDecimal(RowsSelect(i)("GPCPS1")))
                Me.SqlCom.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar, 150, "DESCRIPTION")
                Me.SqlCom.Parameters.Add("@ACHIEVEMENT_ID", SqlDbType.VarChar, 55, "ACHIEVEMENT_ID")
                Me.SqlCom.Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 30, "AGREEMENT_NO")
                Me.SqlCom.Parameters.Add("@DISTRIBUTOR_ID", SqlDbType.VarChar, 10, "DISTRIBUTOR_ID")
                Me.SqlCom.Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 10, "BRAND_ID")
                Me.SqlCom.Parameters.Add("@FLAG", SqlDbType.VarChar, 2, "FLAG")
                Me.SqlCom.Parameters.Add("@ISTARGET_GROUP", SqlDbType.Bit, 0, "ISTARGET_GROUP")
                'Me.SqlCom.Parameters.Add("@TARGET", SqlDbType.Decimal, 0, "TARGET")
                Me.SqlCom.Parameters.Add("@DISPRO", SqlDbType.Decimal, 0, "DISPRO")
                Me.SqlCom.Parameters.Add("@TOTAL_ACTUAL", SqlDbType.Decimal, 0, "TOTAL_ACTUAL")
                Me.SqlCom.Parameters.Add("@TOTAL_ACTUAL_AMOUNT", SqlDbType.Decimal, 0, "TOTAL_ACTUAL_AMOUNT")

                Me.SqlCom.Parameters.Add("@ACTUAL_DISTRIBUTOR", SqlDbType.Decimal, 0, "ACTUAL_DISTRIBUTOR")
                Me.SqlCom.Parameters.Add("@ACTUAL_AMOUNT_DISTRIBUTOR", SqlDbType.Decimal, 0, "ACTUAL_AMOUNT_DISTRIBUTOR")

                Me.SqlCom.Parameters.Add("@ACHIEVEMENT_DISPRO", SqlDbType.Decimal, 0, "ACHIEVEMENT_DISPRO")
                Me.SqlCom.Parameters.Add("@BONUS_QTY", SqlDbType.Decimal, 0, "BONUS_QTY")
                Me.SqlCom.Parameters.Add("@BONUS_DISTRIBUTOR", SqlDbType.Decimal, 0, "BONUS_DISTRIBUTOR")
                Me.SqlCom.Parameters.Add("@BALANCE", SqlDbType.Decimal, 0, "BALANCE")
                Me.SqlCom.Parameters.Add("@ISCOMBINED_TARGET", SqlDbType.Bit, 0, "ISCOMBINED_TARGET")
                Me.SqlCom.Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100).Value = NufarmBussinesRules.User.UserLogin.UserName
                Me.SqlDat.UpdateCommand = Me.SqlCom
                Me.SqlDat.Update(RowsSelect) : Me.ClearCommandParameters()
            End If
            '---------------------------------------------------------------------------
            ''insert yang new dulu

            'RowsSelect = tblAcrDetail.Select("IsNew = " & True, "", DataViewRowState.Added)
            RowsSelect = tblAcrDetail.Select("IsNew = " & True)

            If RowsSelect.Length > 0 Then
                For i As Integer = 0 To RowsSelect.Length - 1
                    ''make anyrows which change becomes modified
                    RowsSelect(i).SetAdded()
                Next

                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                         "IF NOT EXISTS(SELECT ACHIEVEMENT_BRANDPACK_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_BRANDPACK_ID = @ACHIEVEMENT_BRANDPACK_ID) " & vbCrLf & _
                         " BEGIN " & vbCrLf & _
                         "  IF EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER WHERE ACHIEVEMENT_ID = @ACHIEVEMENT_ID) " & vbCrLf & _
                         "   BEGIN " & vbCrLf & _
                         "     INSERT INTO ACCRUED_DETAIL(ACHIEVEMENT_ID,BRANDPACK_ID,ACHIEVEMENT_BRANDPACK_ID,TOTAL_ACTUAL,TOTAL_ACTUAL_AMOUNT,DISC_QTY,DISC_BY_VOLUME " & vbCrLf & _
                         "     ,TOTAL_PO_ORIGINAL,TOTAL_PO_AMOUNT,TOTAL_PBQ3,TOTAL_PBQ4,TOTAL_PBS2,TOTAL_CPQ1,CPQ1_AMOUNT,TOTAL_CPQ2,CPQ2_AMOUNT,TOTAL_CPQ3,CPQ3_AMOUNT,TOTAL_CPS1,CPS1_AMOUNT,TOTAL_PBY,CAN_RELEASE,CAN_UPDATE,DESCRIPTIONS,CREATE_BY,CREATE_DATE)" & vbCrLf & _
                         "     VALUES(@ACHIEVEMENT_ID,@BRANDPACK_ID,@ACHIEVEMENT_BRANDPACK_ID,@TOTAl_ACTUAL,@TOTAL_ACTUAL_AMOUNT,@DISC_QTY,@DISC_BY_VOLUME, " & vbCrLf & _
                         "     @TOTAL_PO_ORIGINAL,@TOTAL_PO_AMOUNT,@TOTAL_PBQ3,@TOTAL_PBQ4,@TOTAL_PBS2,@TOTAL_CPQ1,@CPQ1_AMOUNT,@TOTAL_CPQ2,@CPQ2_AMOUNT,@TOTAL_CPQ3,@CPQ3_AMOUNT,@TOTAL_CPS1,@CPS1_AMOUNT,@TOTAL_PBY,0,0,@DESCRIPTIONS,@CREATE_BY,@CREATE_DATE); " & vbCrLf & _
                         "   END " & vbCrLf & _
                         " END "
                ResetAdapterCRUD()
                Me.ResetCommandText(CommandType.Text, Query)
                Me.SqlCom.Parameters.Add("@TOTAL_PO_ORIGINAL", SqlDbType.Decimal, 0, "TOTAL_PO_ORIGINAL")
                Me.SqlCom.Parameters.Add("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0, "TOTAL_PO_AMOUNT")
                Me.SqlCom.Parameters.Add("@TOTAL_PBQ3", SqlDbType.Decimal, 0, "TOTAL_PBQ3")
                Me.SqlCom.Parameters.Add("@TOTAL_PBQ4", SqlDbType.Decimal, 0, "TOTAL_PBQ4")
                Me.SqlCom.Parameters.Add("@TOTAL_PBS2", SqlDbType.Decimal, 0, "TOTAL_PBS2")
                Me.SqlCom.Parameters.Add("@TOTAL_CPQ1", SqlDbType.Decimal, 0, "TOTAL_CPQ1")
                Me.SqlCom.Parameters.Add("@TOTAL_CPQ2", SqlDbType.Decimal, 0, "TOTAL_CPQ2")
                Me.SqlCom.Parameters.Add("@TOTAL_CPQ3", SqlDbType.Decimal, 0, "TOTAL_CPQ3")
                Me.SqlCom.Parameters.Add("@TOTAL_CPS1", SqlDbType.Decimal, 0, "TOTAL_CPS1")
                Me.SqlCom.Parameters.Add("@TOTAL_PBY", SqlDbType.Decimal, 0, "TOTAL_PBY")
                Me.SqlCom.Parameters.Add("@ACHIEVEMENT_ID", SqlDbType.VarChar, 55, "ACHIEVEMENT_ID")
                Me.SqlCom.Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                Me.SqlCom.Parameters.Add("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, 70, "ACHIEVEMENT_BRANDPACK_ID")
                Me.SqlCom.Parameters.Add("@TOTAL_ACTUAL", SqlDbType.Decimal, 0, "TOTAL_ACTUAL")
                Me.SqlCom.Parameters.Add("@TOTAL_ACTUAL_AMOUNT", SqlDbType.Decimal, 0, "TOTAL_ACTUAL_AMOUNT")


                Me.SqlCom.Parameters.Add("@CPQ1_AMOUNT", SqlDbType.Decimal, 0, "CPQ1_AMOUNT")
                Me.SqlCom.Parameters.Add("@CPQ2_AMOUNT", SqlDbType.Decimal, 0, "CPQ2_AMOUNT")
                Me.SqlCom.Parameters.Add("@CPQ3_AMOUNT", SqlDbType.Decimal, 0, "CPQ3_AMOUNT")
                Me.SqlCom.Parameters.Add("@CPS1_AMOUNT", SqlDbType.Decimal, 0, "CPS1_AMOUNT")


                Me.SqlCom.Parameters.Add("@DISC_QTY", SqlDbType.Decimal, 0, "DISC_QTY")
                Me.SqlCom.Parameters.Add("@DISC_BY_VOLUME", SqlDbType.Decimal, 0, "DISC_BY_VOLUME")
                Me.SqlCom.Parameters.Add("@DESCRIPTIONS", SqlDbType.VarChar, 150, "DESCRIPTIONS")
                Me.SqlCom.Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50, "CREATE_BY")
                Me.SqlCom.Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_DATE")
                SqlDat.InsertCommand = SqlCom : SqlDat.Update(RowsSelect) : Me.ClearCommandParameters()
            End If
            '---------------------------------------------------------------------------

            RowsSelect = tblAcrDetail.Select("IsChanged = " & True)
            If RowsSelect.Length > 0 Then
                For i As Integer = 0 To RowsSelect.Length - 1
                    ''make anyrows which change becomes modified
                    RowsSelect(i).SetModified()
                Next
                Me.ResetAdapterCRUD()
                For i As Integer = 0 To RowsSelect.Length - 1
                    Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                            "IF EXISTS(SELECT ACHIEVEMENT_BRANDPACK_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_BRANDPACK_ID = @ACHIEVEMENT_BRANDPACK_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "   IF NOT EXISTS(SELECT ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_BRANDPACK_DISC WHERE ACHIEVEMENT_BRANDPACK_ID = @ACHIEVEMENT_BRANDPACK_ID) " & vbCrLf & _
                            "   BEGIN " & vbCrLf & _
                            "     IF NOT EXISTS(SELECT ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_REMAINDING WHERE ACHIEVEMENT_BRANDPACK_ID = @ACHIEVEMENT_BRANDPACK_ID ) " & vbCrLf & _
                            "      BEGIN " & vbCrLf & _
                            "       UPDATE ACCRUED_DETAIL SET TOTAL_ACTUAL = @TOTAL_ACTUAL,TOTAL_ACTUAL_AMOUNT = @TOTAL_ACTUAL_AMOUNT,TOTAL_PO_ORIGINAL = @TOTAL_PO_ORIGINAL, " & vbCrLf & _
                            "       TOTAL_PO_AMOUNT = @TOTAL_PO_AMOUNT, TOTAL_PBQ4 = @TOTAL_PBQ4,TOTAL_PBS2 = @TOTAL_PBS2," & vbCrLf & _
                            "       TOTAL_CPQ1 = @TOTAL_CPQ1,CPQ1_AMOUNT = @CPQ1_AMOUNT, TOTAL_CPQ2 = @TOTAL_CPQ2,CPQ2_AMOUNT = @CPQ2_AMOUNT, TOTAL_CPQ3 = @TOTAL_CPQ3,CPQ3_AMOUNT = @CPQ3_AMOUNT, " & vbCrLf & _
                            "       TOTAL_CPS1 = @TOTAL_CPS1,CPS1_AMOUNT = @CPS1_AMOUNT,TOTAL_PBY = @TOTAL_PBY,DESCRIPTIONS = @DESCRIPTIONS,DISC_QTY = @DISC_QTY,DISC_BY_VOLUME = @DISC_BY_VOLUME,LEFT_QTY = @DISC_QTY,  " & vbCrLf & _
                            "       LAST_UPDATE = GETDATE(),MODIFY_BY = @MODIFY_BY,MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                            "       WHERE ACHIEVEMENT_BRANDPACK_ID = @ACHIEVEMENT_BRANDPACK_ID  ; " & vbCrLf & _
                            "      END " & vbCrLf & _
                            "   END " & vbCrLf & _
                            " END "

                    '" IF NOT EXISTS(SELECT ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_REMAINDING WHERE ACHIEVEMENT_BRANDPACK_ID = @ACHIEVEMENT_BRANDPACK_ID ) " & vbCrLf & _
                    '" BEGIN UPDATE ACCRUED_DETAIL SET DISC_QTY = @DISC_QTY,DISC_BY_VOLUME = @DISC_BY_VOLUME WHERE ACHIEVEMENT_BRANDPACK_ID = @ACHIEVEMENT_BRANDPACK_ID ; END " & vbCrLf & _
                    '"END "
                    Me.ResetCommandText(CommandType.Text, Query)

                    Me.SqlCom.Parameters.Add("@TOTAL_PO_ORIGINAL", SqlDbType.Decimal, 0, "TOTAL_PO_ORIGINAL")
                    Me.SqlCom.Parameters.Add("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0, "TOTAL_PO_AMOUNT")
                    Me.SqlCom.Parameters.Add("@TOTAL_PBQ4", SqlDbType.Decimal, 0, "TOTAL_PBQ4")
                    Me.SqlCom.Parameters.Add("@TOTAL_PBS2", SqlDbType.Decimal, 0, "TOTAL_PBS2")
                    Me.SqlCom.Parameters.Add("@TOTAL_CPQ1", SqlDbType.Decimal, 0, "TOTAL_CPQ1")
                    Me.SqlCom.Parameters.Add("@TOTAL_CPQ2", SqlDbType.Decimal, 0, "TOTAL_CPQ2")
                    Me.SqlCom.Parameters.Add("@TOTAL_CPQ3", SqlDbType.Decimal, 0, "TOTAL_CPQ3")
                    Me.SqlCom.Parameters.Add("@TOTAL_CPS1", SqlDbType.Decimal, 0, "TOTAL_CPS1")
                    Me.SqlCom.Parameters.Add("@TOTAL_ACTUAL", SqlDbType.Decimal, 0, "TOTAL_ACTUAL")
                    Me.SqlCom.Parameters.Add("@TOTAL_ACTUAL_AMOUNT", SqlDbType.Decimal, 0, "TOTAL_ACTUAL_AMOUNT")
                    Me.SqlCom.Parameters.Add("@DISC_QTY", SqlDbType.Decimal, 0, "DISC_QTY")
                    Me.SqlCom.Parameters.Add("@DISC_BY_VOLUME", SqlDbType.Decimal, 0, "DISC_BY_VOLUME")
                    Me.SqlCom.Parameters.Add("@TOTAL_PBY", SqlDbType.Decimal, 0, "TOTAL_PBY")

                    Me.SqlCom.Parameters.Add("@CPQ1_AMOUNT", SqlDbType.Decimal, 0, "CPQ1_AMOUNT")
                    Me.SqlCom.Parameters.Add("@CPQ2_AMOUNT", SqlDbType.Decimal, 0, "CPQ2_AMOUNT")
                    Me.SqlCom.Parameters.Add("@CPQ3_AMOUNT", SqlDbType.Decimal, 0, "CPQ3_AMOUNT")
                    Me.SqlCom.Parameters.Add("@CPS1_AMOUNT", SqlDbType.Decimal, 0, "CPS1_AMOUNT")

                    Me.SqlCom.Parameters.Add("@DESCRIPTIONS", SqlDbType.VarChar, 150, "DESCRIPTIONS")
                    Me.SqlCom.Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50, "MODIFY_BY")
                    Me.SqlCom.Parameters.Add("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, 70, "ACHIEVEMENT_BRANDPACK_ID")
                    Me.SqlDat.UpdateCommand = Me.SqlCom : Me.SqlDat.Update(RowsSelect) : Me.ClearCommandParameters()
                Next
            End If
            Me.CommiteTransaction()
        End Sub
        Private Sub CreateOrReCreateAchForDB(ByRef tblAchValue As DataTable)
            If Not tblAchValue.Columns.Contains("AvgPriceID") Then
                tblAchValue.Columns.Add("AvgPriceID", Type.GetType("System.Int32"))
            End If
            '[TARGET_BY_VALUE] [decimal](18, 4) NULL,
            If Not tblAchValue.Columns.Contains("TARGET_BY_VALUE") Then
                tblAchValue.Columns.Add("TARGET_BY_VALUE", Type.GetType("System.Decimal"))
                tblAchValue.Columns("TARGET_BY_VALUE").DefaultValue = 0D
            End If
            '[ACTUAL_BY_VALUE] [decimal](18, 4) NULL,
            If Not tblAchValue.Columns.Contains("ACTUAL_BY_VALUE") Then
                tblAchValue.Columns.Add("ACTUAL_BY_VALUE", Type.GetType("System.Decimal"))
                tblAchValue.Columns("ACTUAL_BY_VALUE").DefaultValue = 0D
            End If
            '[DISPRO_BY_VALUE] [decimal](18, 4) NULL,
            If Not tblAchValue.Columns.Contains("DISPRO_BY_VALUE") Then
                tblAchValue.Columns.Add("DISPRO_BY_VALUE", Type.GetType("System.Decimal"))
                tblAchValue.Columns("DISPRO_BY_VALUE").DefaultValue = 0D
            End If
            '[ACH_DISPRO_BY_VALUE] [decimal](18, 4) NULL,
            If Not tblAchValue.Columns.Contains("ACH_DISPRO_BY_VALUE") Then
                tblAchValue.Columns.Add("ACH_DISPRO_BY_VALUE", Type.GetType("System.Decimal"))
                tblAchValue.Columns("ACH_DISPRO_BY_VALUE").DefaultValue = 0D
            End If
            '[DISC_BY_VALUE] [decimal](18, 4) NULL,
            If Not tblAchValue.Columns.Contains("DISC_BY_VALUE") Then
                tblAchValue.Columns.Add("DISC_BY_VALUE", Type.GetType("System.Decimal"))
                tblAchValue.Columns("DISC_BY_VALUE").DefaultValue = 0D
            End If
            '[DISC_BY_VOLUME] [decimal](18, 3) NULL,
            If Not tblAchValue.Columns.Contains("DISC_BY_VOLUME") Then
                tblAchValue.Columns.Add("DISC_BY_VOLUME", Type.GetType("System.Decimal"))
                tblAchValue.Columns("DISC_BY_VOLUME").DefaultValue = 0D
            End If
            If Not tblAchValue.Columns.Contains("COMBINED_BRAND_ID") Then
                tblAchValue.Columns.Add("COMBINED_BRAND_ID", Type.GetType("System.String"))
            End If
            If Not tblAchValue.Columns.Contains("DISC_DIST_BY_VALUE") Then
                tblAchValue.Columns.Add("DISC_DIST_BY_VALUE", Type.GetType("System.Decimal"))
            End If

            ''create primary key
            Dim Key(1) As DataColumn : Key(0) = tblAchValue.Columns("ACHIEVEMENT_ID")
            tblAchValue.PrimaryKey = Key
        End Sub
        Private Sub ProceedPeriodBeforeForDiscByValue(ByVal Flag As String, ByVal AgreeBrandID As String, ByRef tblAchValue As DataTable, ByRef ListCombined As List(Of String))
            Dim Rows() As DataRow = tblAchValue.Select("AGREE_BRAND_ID = '" & AgreeBrandID & "'") ''datarow untuk yang bukan combined
            Dim rowsC() As DataRow = Nothing '' datarow untuk variable combined brand
            Dim TJoinBefore As Decimal = 0, TInvBefore As Decimal = 0, DiscDistributor As Decimal = 0, DiscByValue As Decimal = 0
            Select Case Flag
                Case "Q1" '-->PBQ3,PBQ4,PBS2,PBY
                    'untuk yang di combined dulu -dibedakan berdasarkan agreement brand
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_PBQ3")) * Convert.ToDecimal(Rows(0)("PERCENT_PBQ3")))
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_PBQ4")) * Convert.ToDecimal(Rows(0)("PERCENT_PBQ4")))
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(Rows(0)("PERCENT_PBY")))
                    If Not Rows(0).IsNull("COMBINED_BRAND_ID") Then
                        If Not ListCombined.Contains(Rows(0)("COMBINED_BRAND_ID").ToString()) Then
                            ListCombined.Add(Rows(0)("COMBINED_BRAND_ID").ToString())
                            rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                            If rowsC.Length > 0 Then
                                TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBQ3")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBQ3"))) '(Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBQ3")) + Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBQ4")) + Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")))
                                TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBQ4")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBQ4")))
                                If Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) > 0 Then
                                    TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBY")))
                                End If
                            End If
                        End If
                    End If
                    'update data

                    For i2 As Integer = 0 To Rows.Length - 1
                        Rows(i2).BeginEdit()
                        DiscByValue = Convert.ToDecimal(Rows(i2)("DISC_BY_VALUE"))
                        Rows(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                        DiscDistributor = Convert.ToDecimal(Rows(i2)("DISC_DIST_BY_VALUE"))
                        TInvBefore = (Convert.ToDecimal(Rows(i2)("PERCENT_PBQ3")) * Convert.ToDecimal(Rows(i2)("TOTAL_PBQ3")))
                        TInvBefore += (Convert.ToDecimal(Rows(i2)("PERCENT_PBQ4")) * Convert.ToDecimal(Rows(i2)("TOTAL_PBQ4")))
                        If Convert.ToDecimal(Rows(i2)("TOTAL_PBY")) > 0 Then
                            TInvBefore += (Convert.ToDecimal(Rows(i2)("PERCENT_PBY")) * Convert.ToDecimal(Rows(i2)("TOTAL_PBY")))
                        End If
                        'If Rows(i2)("ACHIEVEMENT_ID") = "SAN008GRP|013/NI/I/2000.1400780|Q1" Then
                        '    Stop
                        'End If
                        Rows(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                        Rows(i2).EndEdit()
                    Next
                    If Not IsNothing(rowsC) Then ''ini artinya ada combined brand
                        'untuk mengantisipasi restructure table AchValue, karena edit data di Rows, padahal antara rows dan rowsc sama-sama table nya
                        ' maka Reset rowscnya
                        rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                        If rowsC.Length > 0 Then
                            For i2 As Integer = 0 To rowsC.Length - 1
                                rowsC(i2).BeginEdit()
                                DiscByValue = Convert.ToDecimal(rowsC(i2)("DISC_BY_VALUE"))
                                rowsC(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                                DiscDistributor = Convert.ToDecimal(rowsC(i2)("DISC_DIST_BY_VALUE"))
                                TInvBefore = (Convert.ToDecimal(rowsC(i2)("PERCENT_PBQ3")) * Convert.ToDecimal(rowsC(i2)("TOTAL_PBQ3")))
                                TInvBefore += (Convert.ToDecimal(rowsC(i2)("PERCENT_PBQ4")) * Convert.ToDecimal(rowsC(i2)("TOTAL_PBQ4")))
                                If Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")) > 0 Then
                                    TInvBefore += (Convert.ToDecimal(rowsC(i2)("PERCENT_PBY")) * Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")))
                                End If
                                rowsC(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                                rowsC(i2).EndEdit()
                            Next
                        End If
                    End If
                Case "Q2"
                    'TOTAL_JOIN_PBQ4()
                    'TOTAL_JOIN_CPQ1()

                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_PBQ4")) * Convert.ToDecimal(Rows(0)("PERCENT_PBQ4")))
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_CPQ1")) * Convert.ToDecimal(Rows(0)("PERCENT_CPQ1")))
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(Rows(0)("PERCENT_PBY")))
                    If Not Rows(0).IsNull("COMBINED_BRAND_ID") Then
                        If Not ListCombined.Contains(Rows(0)("COMBINED_BRAND_ID").ToString()) Then
                            ListCombined.Add(Rows(0)("COMBINED_BRAND_ID").ToString())
                            rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                            If rowsC.Length > 0 Then
                                TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBQ4")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBQ4")))
                                TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_CPQ1")) * Convert.ToDecimal(rowsC(0)("PERCENT_CPQ1")))
                                If Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) > 0 Then
                                    TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBY")))
                                End If
                            End If
                        End If
                    End If
                    'update data
                    For i2 As Integer = 0 To Rows.Length - 1
                        Rows(i2).BeginEdit()
                        DiscByValue = Convert.ToDecimal(Rows(i2)("DISC_BY_VALUE"))
                        Rows(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                        DiscDistributor = Convert.ToDecimal(Rows(i2)("DISC_DIST_BY_VALUE"))
                        TInvBefore = (Convert.ToDecimal(Rows(i2)("PERCENT_CPQ1")) * Convert.ToDecimal(Rows(i2)("TOTAL_CPQ1")))
                        TInvBefore += (Convert.ToDecimal(Rows(i2)("PERCENT_PBQ4")) * Convert.ToDecimal(Rows(i2)("TOTAL_PBQ4")))
                        If Convert.ToDecimal(Rows(i2)("TOTAL_PBY")) > 0 Then
                            TInvBefore += (Convert.ToDecimal(Rows(i2)("PERCENT_PBY")) * Convert.ToDecimal(Rows(i2)("TOTAL_PBY")))
                        End If
                        Rows(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                        Rows(i2).EndEdit()
                    Next
                    If Not IsNothing(rowsC) Then
                        'untuk mengantisipasi restructure table AchValue, karena edit data di Rows, padahal antara rows dan rowsc sama-sama table nya
                        ' maka Reset rowscnya
                        rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                        If rowsC.Length > 0 Then
                            For i2 As Integer = 0 To rowsC.Length - 1
                                rowsC(i2).BeginEdit()
                                DiscByValue = Convert.ToDecimal(rowsC(i2)("DISC_BY_VALUE"))
                                rowsC(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                                DiscDistributor = Convert.ToDecimal(rowsC(i2)("DISC_DIST_BY_VALUE"))
                                TInvBefore = (Convert.ToDecimal(rowsC(i2)("PERCENT_PBQ4")) * Convert.ToDecimal(rowsC(i2)("TOTAL_PBQ4")))
                                TInvBefore += (Convert.ToDecimal(rowsC(i2)("PERCENT_CPQ1")) * Convert.ToDecimal(rowsC(i2)("TOTAL_CPQ1")))
                                If Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")) > 0 Then
                                    TInvBefore += (Convert.ToDecimal(rowsC(i2)("PERCENT_PBY")) * Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")))
                                End If
                                rowsC(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                                rowsC(i2).EndEdit()
                            Next
                        End If
                    End If
                Case "Q3"
                    'TOTAL_JOIN_CPQ1()
                    'TOTAL_JOIN_CPQ2()
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_CPQ2")) * Convert.ToDecimal(Rows(0)("PERCENT_CPQ2")))
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_CPQ1")) * Convert.ToDecimal(Rows(0)("PERCENT_CPQ1")))
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(Rows(0)("PERCENT_PBY")))
                    If Not Rows(0).IsNull("COMBINED_BRAND_ID") Then
                        If Not ListCombined.Contains(Rows(0)("COMBINED_BRAND_ID").ToString()) Then
                            ListCombined.Add(Rows(0)("COMBINED_BRAND_ID").ToString())
                            rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                            If rowsC.Length > 0 Then
                                TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBQ4")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBQ4")))
                                TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_CPQ1")) * Convert.ToDecimal(rowsC(0)("PERCENT_CPQ1")))
                                If Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) > 0 Then
                                    TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBY")))
                                End If
                            End If
                        End If
                    End If
                    'update data
                    For i2 As Integer = 0 To Rows.Length - 1
                        Rows(i2).BeginEdit()
                        DiscByValue = Convert.ToDecimal(Rows(i2)("DISC_BY_VALUE"))
                        Rows(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                        DiscDistributor = Convert.ToDecimal(Rows(i2)("DISC_DIST_BY_VALUE"))
                        TInvBefore = (Convert.ToDecimal(Rows(i2)("PERCENT_CPQ1")) * Convert.ToDecimal(Rows(i2)("TOTAL_CPQ1")))
                        TInvBefore += (Convert.ToDecimal(Rows(i2)("PERCENT_CPQ2")) * Convert.ToDecimal(Rows(i2)("TOTAL_CPQ2")))
                        If Convert.ToDecimal(Rows(i2)("TOTAL_PBY")) > 0 Then
                            TInvBefore += (Convert.ToDecimal(Rows(i2)("PERCENT_PBY")) * Convert.ToDecimal(Rows(i2)("TOTAL_PBY")))
                        End If
                        Rows(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                        Rows(i2).EndEdit()
                    Next
                    If Not IsNothing(rowsC) Then
                        'untuk mengantisipasi restructure table AchValue, karena edit data di Rows, padahal antara rows dan rowsc sama-sama table nya
                        ' maka Reset rowscnya
                        rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                        If rowsC.Length > 0 Then
                            For i2 As Integer = 0 To rowsC.Length - 1
                                rowsC(i2).BeginEdit()
                                DiscByValue = Convert.ToDecimal(rowsC(i2)("DISC_BY_VALUE"))
                                rowsC(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                                DiscDistributor = Convert.ToDecimal(rowsC(i2)("DISC_DIST_BY_VALUE"))
                                TInvBefore = (Convert.ToDecimal(rowsC(i2)("PERCENT_CPQ1")) * Convert.ToDecimal(rowsC(i2)("TOTAL_CPQ1")))
                                TInvBefore += (Convert.ToDecimal(rowsC(i2)("PERCENT_CPQ2")) * Convert.ToDecimal(rowsC(i2)("TOTAL_CPQ2")))
                                If Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")) > 0 Then
                                    TInvBefore += (Convert.ToDecimal(rowsC(i2)("PERCENT_PBY")) * Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")))
                                End If
                                rowsC(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                                rowsC(i2).EndEdit()
                            Next
                        End If
                    End If
                Case "Q4"
                    'TOTAL_JOIN_CPQ2
                    'TOTAL_JOIN_CPQ3
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_CPQ2")) * Convert.ToDecimal(Rows(0)("PERCENT_CPQ2")))
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_CPQ3")) * Convert.ToDecimal(Rows(0)("PERCENT_CPQ3")))
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(Rows(0)("PERCENT_PBY")))
                    If Not Rows(0).IsNull("COMBINED_BRAND_ID") Then
                        If Not ListCombined.Contains(Rows(0)("COMBINED_BRAND_ID").ToString()) Then
                            ListCombined.Add(Rows(0)("COMBINED_BRAND_ID").ToString())
                            rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                            If rowsC.Length > 0 Then
                                TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_CPQ2")) * Convert.ToDecimal(rowsC(0)("PERCENT_CPQ2")))
                                TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_CPQ3")) * Convert.ToDecimal(rowsC(0)("PERCENT_CPQ3")))
                                If Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) > 0 Then
                                    TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBY")))
                                End If
                            End If
                        End If
                    End If
                    'update data
                    For i2 As Integer = 0 To Rows.Length - 1
                        Rows(i2).BeginEdit()
                        DiscByValue = Convert.ToDecimal(Rows(i2)("DISC_BY_VALUE"))
                        Rows(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                        DiscDistributor = Convert.ToDecimal(Rows(i2)("DISC_DIST_BY_VALUE"))
                        TInvBefore = (Convert.ToDecimal(Rows(i2)("PERCENT_CPQ2")) * Convert.ToDecimal(Rows(i2)("TOTAL_CPQ2")))
                        TInvBefore += (Convert.ToDecimal(Rows(i2)("PERCENT_CPQ3")) * Convert.ToDecimal(Rows(i2)("TOTAL_CPQ3")))
                        If Convert.ToDecimal(Rows(i2)("TOTAL_PBY")) > 0 Then
                            TInvBefore += (Convert.ToDecimal(Rows(i2)("PERCENT_PBY")) * Convert.ToDecimal(Rows(i2)("TOTAL_PBY")))
                        End If
                        Rows(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                        Rows(i2).EndEdit()
                    Next
                    If Not IsNothing(rowsC) Then
                        'untuk mengantisipasi restructure table AchValue, karena edit data di Rows, padahal antara rows dan rowsc sama-sama table nya
                        ' maka Reset rowscnya
                        rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                        If rowsC.Length > 0 Then
                            For i2 As Integer = 0 To rowsC.Length - 1
                                rowsC(i2).BeginEdit()
                                DiscByValue = Convert.ToDecimal(rowsC(i2)("DISC_BY_VALUE"))
                                rowsC(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                                DiscDistributor = Convert.ToDecimal(rowsC(i2)("DISC_DIST_BY_VALUE"))
                                TInvBefore = (Convert.ToDecimal(rowsC(i2)("PERCENT_CPQ2")) * Convert.ToDecimal(rowsC(i2)("TOTAL_CPQ2")))
                                TInvBefore += (Convert.ToDecimal(rowsC(i2)("PERCENT_CPQ3")) * Convert.ToDecimal(rowsC(i2)("TOTAL_CPQ3")))
                                If Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")) > 0 Then
                                    TInvBefore += (Convert.ToDecimal(rowsC(i2)("PERCENT_PBY")) * Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")))
                                End If
                                rowsC(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                                rowsC(i2).EndEdit()
                            Next
                        End If
                    End If
                Case "S1"
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_PBS2")) * Convert.ToDecimal(Rows(0)("PERCENT_PBS2")))
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(Rows(0)("PERCENT_PBY")))
                    If Not Rows(0).IsNull("COMBINED_BRAND_ID") Then
                        If Not ListCombined.Contains(Rows(0)("COMBINED_BRAND_ID").ToString()) Then
                            ListCombined.Add(Rows(0)("COMBINED_BRAND_ID").ToString())
                            rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                            If rowsC.Length > 0 Then
                                TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBS2")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBS2")))
                                If Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) > 0 Then
                                    TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBY")))
                                End If
                            End If
                        End If
                    End If
                    'update data
                    For i2 As Integer = 0 To Rows.Length - 1
                        Rows(i2).BeginEdit()
                        DiscByValue = Convert.ToDecimal(Rows(i2)("DISC_BY_VALUE"))
                        Rows(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                        DiscDistributor = Convert.ToDecimal(Rows(i2)("DISC_DIST_BY_VALUE"))
                        TInvBefore = (Convert.ToDecimal(Rows(i2)("PERCENT_PBS2")) * Convert.ToDecimal(Rows(i2)("TOTAL_PBS2")))
                        If Convert.ToDecimal(Rows(i2)("TOTAL_PBY")) > 0 Then
                            TInvBefore += (Convert.ToDecimal(Rows(i2)("PERCENT_PBY")) * Convert.ToDecimal(Rows(i2)("TOTAL_PBY")))
                        End If
                        Rows(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                        Rows(i2).EndEdit()
                    Next
                    If Not IsNothing(rowsC) Then
                        'untuk mengantisipasi restructure table AchValue, karena edit data di Rows, padahal antara rows dan rowsc sama-sama table nya
                        ' maka Reset rowscnya
                        rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                        If rowsC.Length > 0 Then
                            For i2 As Integer = 0 To rowsC.Length - 1
                                rowsC(i2).BeginEdit()
                                DiscByValue = Convert.ToDecimal(rowsC(i2)("DISC_BY_VALUE"))
                                rowsC(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                                DiscDistributor = Convert.ToDecimal(rowsC(i2)("DISC_DIST_BY_VALUE"))
                                TInvBefore = (Convert.ToDecimal(rowsC(i2)("PERCENT_PBS2")) * Convert.ToDecimal(rowsC(i2)("TOTAL_PBS2")))
                                If Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")) > 0 Then
                                    TInvBefore += (Convert.ToDecimal(rowsC(i2)("PERCENT_PBY")) * Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")))
                                End If
                                rowsC(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                                rowsC(i2).EndEdit()
                            Next
                        End If
                    End If
                Case "S2"
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_CPS1")) * Convert.ToDecimal(Rows(0)("PERCENT_CPS1")))
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(Rows(0)("PERCENT_PBY")))
                    If Not Rows(0).IsNull("COMBINED_BRAND_ID") Then
                        If Not ListCombined.Contains(Rows(0)("COMBINED_BRAND_ID").ToString()) Then
                            ListCombined.Add(Rows(0)("COMBINED_BRAND_ID").ToString())
                            rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                            If rowsC.Length > 0 Then
                                TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_CPS1")) * Convert.ToDecimal(rowsC(0)("PERCENT_CPS1")))
                                If Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) > 0 Then
                                    TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBY")))
                                End If
                            End If
                        End If
                    End If
                    'update data
                    For i2 As Integer = 0 To Rows.Length - 1
                        Rows(i2).BeginEdit()
                        DiscByValue = Convert.ToDecimal(Rows(i2)("DISC_BY_VALUE"))
                        Rows(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                        DiscDistributor = Convert.ToDecimal(Rows(i2)("DISC_DIST_BY_VALUE"))
                        TInvBefore = (Convert.ToDecimal(Rows(i2)("PERCENT_CPS1")) * Convert.ToDecimal(Rows(i2)("TOTAL_CPS1")))
                        If Convert.ToDecimal(Rows(i2)("TOTAL_PBY")) > 0 Then
                            TInvBefore += (Convert.ToDecimal(Rows(i2)("PERCENT_PBY")) * Convert.ToDecimal(Rows(i2)("TOTAL_PBY")))
                        End If
                        Rows(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                        Rows(i2).EndEdit()
                    Next
                    If Not IsNothing(rowsC) Then
                        'untuk mengantisipasi restructure table AchValue, karena edit data di Rows, padahal antara rows dan rowsc sama-sama table nya
                        ' maka Reset rowscnya
                        rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                        If rowsC.Length > 0 Then
                            For i2 As Integer = 0 To rowsC.Length - 1
                                rowsC(i2).BeginEdit()
                                DiscByValue = Convert.ToDecimal(rowsC(i2)("DISC_BY_VALUE"))
                                rowsC(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                                DiscDistributor = Convert.ToDecimal(rowsC(i2)("DISC_DIST_BY_VALUE"))
                                TInvBefore = (Convert.ToDecimal(rowsC(i2)("PERCENT_CPS1")) * Convert.ToDecimal(rowsC(i2)("TOTAL_CPS1")))
                                If Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")) > 0 Then
                                    TInvBefore += (Convert.ToDecimal(rowsC(i2)("PERCENT_PBY")) * Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")))
                                End If
                                rowsC(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                                rowsC(i2).EndEdit()
                            Next
                        End If
                    End If
                Case "Y"
                    TJoinBefore += (Convert.ToDecimal(Rows(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(Rows(0)("PERCENT_PBY")))
                    If Not Rows(0).IsNull("COMBINED_BRAND_ID") Then
                        If Not ListCombined.Contains(Rows(0)("COMBINED_BRAND_ID").ToString()) Then
                            ListCombined.Add(Rows(0)("COMBINED_BRAND_ID").ToString())
                            rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                            If rowsC.Length > 0 Then
                                If Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) > 0 Then
                                    TJoinBefore += (Convert.ToDecimal(rowsC(0)("TOTAL_JOIN_PBY")) * Convert.ToDecimal(rowsC(0)("PERCENT_PBY")))
                                End If
                            End If
                        End If
                    End If
                    'update data
                    For i2 As Integer = 0 To Rows.Length - 1
                        Rows(i2).BeginEdit()
                        DiscByValue = Convert.ToDecimal(Rows(i2)("DISC_BY_VALUE"))
                        Rows(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                        DiscDistributor = Convert.ToDecimal(Rows(i2)("DISC_DIST_BY_VALUE"))
                        If Convert.ToDecimal(Rows(i2)("TOTAL_PBY")) > 0 Then
                            TInvBefore = (Convert.ToDecimal(Rows(i2)("PERCENT_PBY")) * Convert.ToDecimal(Rows(i2)("TOTAL_PBY")))
                        End If
                        Rows(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                        Rows(i2).EndEdit()
                    Next
                    If Not IsNothing(rowsC) Then
                        'untuk mengantisipasi restructure table AchValue, karena edit data di Rows, padahal antara rows dan rowsc sama-sama table nya
                        ' maka Reset rowscnya
                        rowsC = tblAchValue.Select("AGREE_BRAND_ID = '" & Rows(0)("COMBINED_BRAND_ID").ToString() & "'")
                        If rowsC.Length > 0 Then
                            For i2 As Integer = 0 To rowsC.Length - 1
                                rowsC(i2).BeginEdit()
                                DiscByValue = Convert.ToDecimal(rowsC(i2)("DISC_BY_VALUE"))
                                rowsC(i2)("DISC_BY_VALUE") = DiscByValue + TJoinBefore

                                DiscDistributor = Convert.ToDecimal(rowsC(i2)("DISC_DIST_BY_VALUE"))
                                TInvBefore = (Convert.ToDecimal(rowsC(i2)("PERCENT_PBY")) * Convert.ToDecimal(rowsC(i2)("TOTAL_PBY")))
                                rowsC(i2)("DISC_DIST_BY_VALUE") = DiscDistributor + TInvBefore
                                rowsC(i2).EndEdit()
                            Next
                        End If
                    End If
            End Select
        End Sub
        Public Function CalculateDPDToValue(ByVal Flag As String, ByVal ListAgreementNo As List(Of String), ByRef tblAgreement As DataTable) As DataSet
            Try
                Dim tblAchValueForDb As DataTable = Nothing
                Dim tblAchValue As New DataTable("T_AchValue"), tblAchValueDetail As New DataTable("T_AchValueDetail")
                Dim Retval As Object = Nothing
                'If (Flag = "Y") Then
                '    Me.mustRecomputeYear = True
                '    Me.CalculateAccrue(Flag, , tblAgreement, False)
                'End If
                'GET TABEL FROM ACCRUED_HEADER BASED ON COLUMN ACHIEVEMENT_VALUE
                Dim strAgreementNo As String = " IN('"
                For i As Integer = 0 To ListAgreementNo.Count - 1
                    strAgreementNo = strAgreementNo & ListAgreementNo(i) & "'"
                    If i < ListAgreementNo.Count - 1 Then
                        strAgreementNo &= ",'"
                    End If
                Next
                strAgreementNo &= ")"
                ''========= HEADER PROGRESSIVE DISCOUNT BY VALUE ===========================================
                Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                        " SELECT AGREEMENT_NO,UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISC_VAL WHERE AGREEMENT_NO " & strAgreementNo & " AND QSY_DISC_FLAG = @QSY_DISC_FLAG ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                AddParameter("@QSY_DISC_FLAG", SqlDbType.VarChar, Flag, 2)
                Dim TProgDiscHeaderByVal As New DataTable("T_AgrProgDisc")
                OpenConnection()
                setDataAdapter(Me.SqlCom).Fill(TProgDiscHeaderByVal)

                ''========= QUERY DETAIL PRORESSIVE DISCOUNT BY VALUE
                Dim TAgProgDiscBVal As New DataTable("T_AProgDiscBVal")
                Query = " SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                       " SELECT ABI.AGREE_BRAND_ID,ABI.COMB_AGREE_BRAND_ID,APD.PRGSV_DISC_PCT FROM AGREE_PROG_DISC_VAL APD INNER JOIN AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                       " ON ABI.AGREE_BRAND_ID = APD.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO " & strAgreementNo & " AND APD.QSY_DISC_FLAG = @QSY_DISC_FLAG ;"
                Me.ResetCommandText(CommandType.Text, Query)
                setDataAdapter(Me.SqlCom).Fill(TAgProgDiscBVal) : Me.ClearCommandParameters()
                If (TAgProgDiscBVal.Rows.Count <= 0) And (TProgDiscHeaderByVal.Rows.Count <= 0) Then
                    Throw New Exception("Discount for DPD by value hasn't been set yet")
                End If

                ''========= QUERY DETAIL  PROGRESSIVE DISCOUNT BY VOLUME ===========================================
                Dim TAgProgDiscBVol As New DataTable("T_AProgDiscBVol")
                Query = " SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                        " SELECT ABI.AGREE_BRAND_ID,ABI.COMB_AGREE_BRAND_ID,APD.UP_TO_PCT,APD.PRGSV_DISC_PCT FROM AGREE_PROG_DISC APD INNER JOIN AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                        " ON ABI.AGREE_BRAND_ID = APD.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO " & strAgreementNo & " AND APD.QSY_DISC_FLAG = @QSY_DISC_FLAG ;"
                Me.ResetCommandText(CommandType.Text, Query)
                AddParameter("@QSY_DISC_FLAG", SqlDbType.VarChar, Flag, 2)
                setDataAdapter(Me.SqlCom).Fill(TAgProgDiscBVol)

                ''========= HEADER PROGRESSIVE DISCOUNT BY VALUME ===========================================
                Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                        " SELECT AGREEMENT_NO,UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO " & strAgreementNo & " AND QSY_DISC_FLAG = @QSY_DISC_FLAG ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Dim TProgDiscHeaderByVol As New DataTable("T_AgrProgDisc")
                setDataAdapter(Me.SqlCom).Fill(TProgDiscHeaderByVol)

                ''fill Detail achievement 
                Query = "SET DEADLOCK_PRIORITY NORMAL ; SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                        "SELECT AD.ACHIEVEMENT_BRANDPACK_ID,AD.ACHIEVEMENT_ID,AD.TOTAL_ACTUAL,AD.DISC_QTY,AD.TOTAL_PBQ3,AD.TOTAL_PBQ4,AD.TOTAL_PBS2,AD.TOTAL_CPQ1,AD.TOTAL_CPQ2,AD.TOTAL_CPQ3,AD.TOTAL_CPS1,AD.TOTAL_PBY,AD.DISC_BY_VALUE,AD.DISC_OBTAINED_FROM, AD.LAST_UPDATE " & vbCrLf & _
                        " FROM ACCRUED_DETAIL AD INNER JOIN ACCRUED_HEADER AH ON AD.ACHIEVEMENT_ID = AH.ACHIEVEMENT_ID WHERE AH.AGREEMENT_NO " & strAgreementNo & " AND AH.FLAG = @FLAG "
                Me.ResetCommandText(CommandType.Text, Query)
                AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                setDataAdapter(Me.SqlCom).Fill(tblAchValueDetail) : Me.ClearCommandParameters()

                Dim minAchieved As Decimal = 0
                ''get minAchievement from Setting BussinesRules
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "DECLARE @V_ValNum DECIMAL(18,3), @V_START_DATE SMALLDATETIME,@V_END_DATE SMALLDATETIME ; " & vbCrLf & _
                        " SELECT TOP 1 @V_START_DATE = START_DATE,@V_END_DATE = END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO " & strAgreementNo & " ; " & vbCrLf & _
                        " SET @V_ValNum = (SELECT TOP 1 ValueNumeric FROM RefBussinesRules WHERE START_DATE >= @V_START_DATE AND END_DATE <= @V_END_DATE AND ParamValue = @FLAG ORDER BY END_DATE DESC); " & vbCrLf & _
                        " IF (@V_Valnum IS NULL) " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " SET @V_ValNum = (SELECT TOP 1 ValueNumeric FROM RefBussinesRules WHERE ParamValue = @FLAG ORDER BY END_DATE DESC) ; " & vbCrLf & _
                        " END " & vbCrLf & _
                        " SELECT ValNum = @V_ValNum ;"
                Me.ResetCommandText(CommandType.Text, Query)
                AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                Retval = Me.SqlCom.ExecuteScalar()
                If Not IsNothing(Retval) And Not IsDBNull(Retval) Then
                    minAchieved = Convert.ToDecimal(Retval)
                End If
                Dim Query1 As String = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                  " SELECT AGREEMENT_NO = @AGREEMENT_NO, AH.ACHIEVEMENT_ID,AGREE_BRAND_ID = @AGREEMENT_NO + '' + AH.BRAND_ID, AH.BRAND_ID,AH.ISTARGET_GROUP,AH.ISCOMBINED_TARGET,AH.AGREEMENT_NO + '' + AH.COMBINED_BRAND_ID AS COMBINED_BRAND_ID, PRICE_FM = ISNULL((SELECT TOP 1 AVGPRICE FROM BRND_AVGPRICE WHERE BRAND_ID = AH.BRAND_ID ORDER BY START_PERIODE DESC),0)," & vbCrLf & _
                  " PRICE_PL = ISNULL((SELECT TOP 1 AVGPRICE_PL FROM BRND_AVGPRICE WHERE BRAND_ID = AH.BRAND_ID ORDER BY START_PERIODE DESC),0), AH.TOTAL_PO_ORIGINAL,AH.TOTAL_PO_AMOUNT,AH.TOTAL_PBQ3,AH.TOTAL_JOIN_PBQ3,AH.TOTAL_PBQ4,AH.TOTAL_JOIN_PBQ4,AH.TOTAL_PBS2,AH.TOTAL_JOIN_PBS2,AH.TOTAL_CPQ1,AH.TOTAL_JOIN_CPQ1, " & vbCrLf & _
                  " AH.TOTAL_CPQ2,AH.TOTAL_JOIN_CPQ2,AH.TOTAL_CPQ3,AH.TOTAL_JOIN_CPQ3,AH.TOTAL_CPS1,AH.TOTAL_JOIN_CPS1,AH.TOTAL_PBY,AH.TOTAL_JOIN_PBY,PERCENT_PBQ3 = ISNULL(GP.PBQ3,0),PERCENT_PBQ4 = ISNULL(GP.PBQ4,0),PERCENT_PBS2 = ISNULL(GP.PBS2,0), " & vbCrLf & _
                  " PERCENT_CPQ1 = ISNULL(GP.CPQ1,0),PERCENT_CPQ2 = ISNULL(GP.CPQ2,0),PERCENT_CPQ3 = ISNULL(GP.CPQ3,0),PERCENT_CPS1 = ISNULL(GP.CPS1,0), " & vbCrLf & _
                  " PERCENT_PBY = ISNULL(GP.PBY,0),AH.TARGET,AH.TARGET_FM,AH.TARGET_PL,TARGET_BY_VALUE = CONVERT(DECIMAL(18,4),0),AH.TOTAL_ACTUAL,AH.ACTUAL_DISTRIBUTOR, ACTUAL_BY_VALUE = AH.TOTAL_ACTUAL_AMOUNT, DISPRO_BY_VALUE = CONVERT(DECIMAL(18,3),0),ACH_DISPRO_BY_VALUE = CONVERT(DECIMAL(18,4),0)," & vbCrLf & _
                  " DISC_BY_VALUE = CONVERT(DECIMAL(18,3),0),DISC_DIST_BY_VALUE = CONVERT(DECIMAL(18,3),0), AH.DISC_OBTAINED_FROM " & vbCrLf & _
                  " FROM ACCRUED_HEADER AH LEFT OUTER JOIN GIVEN_PROGRESSIVE GP ON AH.GP_ID = GP.IDApp " & vbCrLf & _
                  " WHERE AH.AGREEMENT_NO = @AGREEMENT_NO AND AH.FLAG = @FLAG OPTION(KEEP PLAN); "

                Dim Rows() As DataRow = Nothing
                Dim rowsC() As DataRow = Nothing
                Dim ErrMessage As String = ""
                For i As Integer = 0 To ListAgreementNo.Count - 1
                    Dim TTargetValue As Decimal = 0
                    Me.ResetCommandText(CommandType.Text, Query1)
                    AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, ListAgreementNo(i), 30)
                    AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                    tblAchValue.Clear()
                    setDataAdapter(Me.SqlCom).Fill(tblAchValue) : Me.ClearCommandParameters()
                    'Dim ErrAchValue As String = ""
                    If tblAchValue.Rows.Count <= 0 Then
                        'ErrAchValue = ListAgreementNo(i)
                        'ErrMessage = IIf(ErrMessage.Length > 0, (ErrMessage & vbCrLf & ListAgreementNo(i)), ListAgreementNo(i))
                        'Throw New Exception("You haven't computed achievement by volume " & vbCrLf & "For agreement " & ListAgreementNo(i) & " yet.")
                        If Flag = "Y" Then
                            Me.mustRecomputeYear = True
                        End If
                        Me.CalculateAccrue(Flag, , tblAgreement, False)
                        Me.mustRecomputeYear = False

                        ''ulangi refetch data
                        Me.ResetCommandText(CommandType.Text, Query1)
                        AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, ListAgreementNo(i), 30)
                        AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                        tblAchValue.Clear()
                        setDataAdapter(Me.SqlCom).Fill(tblAchValue) : Me.ClearCommandParameters()
                    End If

                    If Not tblAchValue.Columns.Contains("TARGET_VALUE") Then ''total target by value
                        tblAchValue.Columns.Add("TARGET_VALUE", Type.GetType("System.Decimal"))
                    End If
                    For i1 As Integer = 0 To tblAchValue.Rows.Count - 1
                        If Convert.ToDecimal(tblAchValue.Rows(i1)("PRICE_FM")) <= 0 And Convert.ToDecimal(tblAchValue.Rows(i1)("PRICE_FM")) <= 0 Then
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                 " SELECT TOP 1 BRAND_NAME FROM BRND_BRAND WHERE BRAND_ID = @BRAND_ID ;"
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, tblAchValue.Rows(i1)("BRAND_ID"), 15)
                            Retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Throw New Exception("Brand " & Retval.ToString() & vbCrLf & "Price(FM/PL) hasn't been set yet")
                        End If
                        TTargetValue = 0
                        TTargetValue += (Convert.ToDecimal(tblAchValue.Rows(i1)("TARGET_PL")) * Convert.ToDecimal(tblAchValue.Rows(i1)("PRICE_PL")))
                        TTargetValue += (IIf(CDec(tblAchValue.Rows(i1)("TARGET_FM")) <= 0, Convert.ToDecimal(tblAchValue.Rows(i1)("TARGET")), Convert.ToDecimal(tblAchValue.Rows(i1)("TARGET_FM"))) * Convert.ToDecimal(tblAchValue.Rows(i1)("PRICE_FM")))
                        If TTargetValue > 0 Then
                            tblAchValue.Rows(i1).BeginEdit()
                            tblAchValue.Rows(i1)("TARGET_VALUE") = TTargetValue
                            tblAchValue.Rows(i1).EndEdit()
                        End If
                    Next
                    tblAchValue.AcceptChanges()
                    Dim tblAchValueCopy As DataTable = tblAchValue.Copy()
                    'If IsNothing(tblAchValueForDb) Then : tblAchValueForDb = tblAchValue.Copy() : tblAchValueForDb.Clear() : Me.CreateOrReCreateAchForDB(tblAchValueForDb) : End If

                    Rows = Nothing
                    Dim listAgreeBrands As New List(Of String) ''untuk calculasi yang join
                    Dim Dispro As Decimal = 0
                    If tblAchValue.Rows.Count > 0 Then
                        ''jumlahkan yang bukan sharing target
                        Dim SumTarget As Object = tblAchValue.Compute("SUM(TARGET_VALUE)", "ISTARGET_GROUP = 0 AND ISCOMBINED_TARGET = 0")
                        Dim SumPO As Object = tblAchValue.Compute("SUM(TOTAL_PO_AMOUNT)", "ISTARGET_GROUP = 0 AND ISCOMBINED_TARGET = 0")
                        Dim DecSumTarget As Decimal = 0, DecSumActual As Decimal = 0, DecSumPO As Decimal = 0
                        If Not IsNothing(SumTarget) And Not IsDBNull(SumTarget) Then
                            DecSumTarget = Convert.ToDecimal(SumTarget)
                        End If
                        If Not IsNothing(SumPO) And Not IsDBNull(SumPO) Then
                            DecSumPO = Convert.ToDecimal(SumPO)
                        End If

                        Rows = tblAchValue.Select("ISTARGET_GROUP = 1 AND ISCOMBINED_TARGET = 0")
                        Dim ListTargetBrand As New List(Of String)
                        If Rows.Length > 0 Then
                            For i1 As Integer = 0 To Rows.Length - 1
                                If Not listAgreeBrands.Contains(Rows(i1)("AGREE_BRAND_ID").ToString()) Then
                                    listAgreeBrands.Add(Rows(i1)("AGREE_BRAND_ID").ToString())
                                End If
                                Dim BrandID As String = Rows(i1)("BRAND_ID").ToString()
                                If Not ListTargetBrand.Contains(BrandID) Then
                                    ListTargetBrand.Add(BrandID)
                                    DecSumTarget = DecSumTarget + (IIf(CDec(tblAchValue.Rows(i1)("TARGET_FM")) <= 0, Convert.ToDecimal(tblAchValue.Rows(i1)("TARGET")), Convert.ToDecimal(tblAchValue.Rows(i1)("TARGET_FM"))) * Convert.ToDecimal(tblAchValue.Rows(i1)("PRICE_FM")))
                                    DecSumTarget = DecSumTarget + (Convert.ToDecimal(Rows(i1)("TARGET_PL")) * Convert.ToDecimal(Rows(i1)("PRICE_PL")))
                                    DecSumPO = DecSumPO + (Convert.ToDecimal(Rows(i1)("TOTAL_PO_AMOUNT")))
                                End If
                            Next
                        End If

                        Rows = tblAchValue.Select("ISCOMBINED_TARGET = 1")
                        If Rows.Length > 0 Then
                            For i1 As Integer = 0 To Rows.Length - 1
                                Dim BrandID As String = Rows(i1)("BRAND_ID").ToString()
                                Dim CombBrandID As String = Rows(i1)("COMBINED_BRAND_ID").ToString()
                                'define Price FM & Price_PL
                                Dim RowCombined() As DataRow = tblAchValueCopy.Select("AGREE_BRAND_ID = '" & CombBrandID & "'")
                                Dim AvgPriceFM As Decimal = (Convert.ToDecimal(Rows(i1)("PRICE_FM")) + Convert.ToDecimal(RowCombined(0)("PRICE_FM"))) / 2
                                Dim AvgPricePL As Decimal = (Convert.ToDecimal(Rows(i1)("PRICE_PL")) + Convert.ToDecimal(RowCombined(0)("PRICE_PL"))) / 2
                                Dim AvgPriceFMC As Decimal = 0
                                Dim AVgPricePLC As Decimal = 0
                                If Not listAgreeBrands.Contains(CombBrandID) Then
                                    listAgreeBrands.Add(CombBrandID)
                                End If
                                If Not ListTargetBrand.Contains(BrandID) Then
                                    ListTargetBrand.Add(BrandID)
                                    ListTargetBrand.Add(CombBrandID.Replace(ListAgreementNo(i), ""))
                                    DecSumPO = DecSumPO + (Convert.ToDecimal(Rows(i1)("TOTAL_PO_AMOUNT")))
                                    'hitung AveragePrice
                                    AvgPriceFMC = Convert.ToDecimal(RowCombined(0)("PRICE_FM"))
                                    AVgPricePLC = Convert.ToDecimal(RowCombined(0)("PRICE_PL"))

                                    ''Avg Price = AvgPrice FM 2 merek /2 jika dua-duanya ada avgprice FM
                                    If (AvgPriceFM > 0) And (AvgPriceFMC) > 0 Then
                                        AvgPriceFM = (AvgPriceFM + AvgPriceFMC) / 2
                                    ElseIf AvgPriceFM > 0 Then
                                    ElseIf AvgPriceFMC > 0 Then
                                        AvgPriceFM = AvgPriceFMC
                                    End If
                                    If (AvgPricePL > 0) And (AVgPricePLC > 0) Then
                                        AvgPricePL = (AvgPricePL + AVgPricePLC) / 2
                                    ElseIf AvgPricePL > 0 Then
                                    ElseIf AVgPricePLC > 0 Then
                                        AvgPricePL = AVgPricePLC
                                    End If

                                    Dim targetBrand As Decimal = (IIf(CDec(Rows(i1)("TARGET_FM")) <= 0, Convert.ToDecimal(Rows(i1)("TARGET")), Convert.ToDecimal(Rows(i1)("TARGET_FM"))) * AvgPriceFM)
                                    targetBrand = (targetBrand + (Convert.ToDecimal(Rows(i1)("TARGET_PL")) * AvgPricePL))

                                    Dim targetCombBrand As Decimal = 0
                                    If targetBrand <= 0 Then
                                        targetCombBrand = (IIf(CDec(RowCombined(0)("TARGET_FM")) <= 0, Convert.ToDecimal(RowCombined(0)("TARGET")), Convert.ToDecimal(RowCombined(0)("TARGET_FM"))) * AvgPriceFM)
                                        targetCombBrand += (Convert.ToDecimal(RowCombined(0)("TARGET_PL")) * AvgPricePL)
                                    End If
                                    If targetBrand > 0 Then
                                        DecSumTarget = (DecSumTarget + targetBrand)
                                    ElseIf targetCombBrand > 0 Then
                                        DecSumTarget = (DecSumTarget + targetCombBrand)
                                    End If
                                    'DecSumTarget = DecSumTarget + (IIf(CDec(RowCombined(0)("TARGET_FM")) <= 0, Convert.ToDecimal(RowCombined(0)("TARGET")), Convert.ToDecimal(RowCombined(0)("TARGET_FM"))) * AvgPriceFM)
                                    'DecSumTarget = DecSumTarget + (Convert.ToDecimal(RowCombined(0)("TARGET_PL")) * AvgPricePL)
                                    DecSumPO = DecSumPO + (Convert.ToDecimal(RowCombined(0)("TOTAL_PO_AMOUNT")))
                                End If
                            Next
                        End If
                        Dim isAchieved As Boolean = False
                       
                        Dim PercentAch As Decimal = 0
                        If DecSumTarget > 0 Then
                            PercentAch = common.CommonClass.GetPercentage(100, DecSumPO, DecSumTarget)

                            '=============== COMMENT THIS AFTER DEBUGGING ==============================
                            'isAchieved = (PercentAch >= 32)
                            '============== END COMMENT THIS AFTER DEBUGGING


                            '============= UNCOMMENT THIS AFTER DEBUGGING ==============================
                            If minAchieved > 0 Then
                                isAchieved = (PercentAch >= minAchieved)
                            Else
                                isAchieved = (PercentAch >= 100)
                            End If

                            '============ END UNCOMMENT THIS AFTER DEBUGGING
                        End If
                        For i1 As Integer = 0 To tblAchValue.Rows.Count - 1
                            Rows = Nothing
                            Dispro = 0
                            Dim AvgPriceFM As Decimal = Convert.ToDecimal(tblAchValue.Rows(i1)("PRICE_FM")) ' (Convert.ToDecimal(tblAchValue.Rows(i1)("PRICE_FM")) + Convert.ToDecimal(RowCombined(0)("PRICE_FM"))) / 2
                            Dim AvgPricePL As Decimal = Convert.ToDecimal(tblAchValue.Rows(i1)("PRICE_PL")) '(Convert.ToDecimal(tblAchValue.Rows(i1)("PRICE_PL")) + Convert.ToDecimal(RowCombined(0)("PRICE_PL"))) / 2
                            Dim AvgPriceFMC As Decimal = 0
                            Dim AVgPricePLC As Decimal = 0
                            Dim TargetByValue As Decimal = 0
                            Dim TotalPOAmount As Decimal = Convert.ToDecimal(tblAchValue.Rows(i1)("TOTAL_PO_AMOUNT"))

                            TargetByValue = (IIf(CDec(tblAchValue.Rows(i1)("TARGET_FM")) <= 0, Convert.ToDecimal(tblAchValue.Rows(i1)("TARGET")), Convert.ToDecimal(tblAchValue.Rows(i1)("TARGET_FM"))) * AvgPriceFM)
                            TargetByValue += (Convert.ToDecimal(tblAchValue.Rows(i1)("TARGET_PL")) * AvgPricePL)
                            'check TargetCombine bila targetValue bisa saja kosong
                            Dim CombBrandID As String = ""
                            Dim AgreeBrandID As String = tblAchValue.Rows(i1)("AGREE_BRAND_ID").ToString()
                            Dim DISC_OBTAINED_FROM As String = "T2"
                            'If AgreeBrandID = "118/NI/I/2004.1411028" Then
                            '    Stop
                            'End If

                            If Not tblAchValue.Rows(i1).IsNull("COMBINED_BRAND_ID") Then
                                CombBrandID = tblAchValue.Rows(i1)("COMBINED_BRAND_ID").ToString()
                                rowsC = tblAchValueCopy.Select("AGREE_BRAND_ID = '" & CombBrandID & "'")
                                'hitung average price
                                AvgPriceFMC = Convert.ToDecimal(rowsC(0)("PRICE_FM"))
                                AVgPricePLC = Convert.ToDecimal(rowsC(0)("PRICE_PL"))
                                If (AvgPriceFM > 0) And (AvgPriceFMC) > 0 Then
                                    AvgPriceFM = (AvgPriceFM + AvgPriceFMC) / 2
                                ElseIf AvgPriceFM > 0 Then
                                ElseIf AvgPriceFMC > 0 Then
                                    AvgPriceFM = AvgPriceFMC
                                End If
                                If (AvgPricePL > 0) And (AVgPricePLC > 0) Then
                                    AvgPricePL = (AvgPricePL + AVgPricePLC) / 2
                                ElseIf AvgPricePL > 0 Then
                                ElseIf AVgPricePLC > 0 Then
                                    AvgPricePL = AVgPricePLC
                                End If

                                TargetByValue = (IIf(CDec(tblAchValue.Rows(i1)("TARGET_FM")) <= 0, Convert.ToDecimal(tblAchValue.Rows(i1)("TARGET")), Convert.ToDecimal(tblAchValue.Rows(i1)("TARGET_FM"))) * AvgPriceFM)
                                TargetByValue += (Convert.ToDecimal(tblAchValue.Rows(i1)("TARGET_PL")) * AvgPricePL)

                                If rowsC.Length > 0 Then
                                    If TargetByValue <= 0 Then
                                        TargetByValue = (IIf(CDec(rowsC(0)("TARGET_FM")) <= 0, Convert.ToDecimal(rowsC(0)("TARGET")), Convert.ToDecimal(rowsC(0)("TARGET_FM"))) * AvgPriceFM)
                                        TargetByValue += (Convert.ToDecimal(rowsC(0)("TARGET_PL")) * AvgPricePL)
                                    End If
                                    TotalPOAmount += (Convert.ToDecimal(rowsC(0)("TOTAL_PO_AMOUNT")))
                                End If
                            End If
                            tblAchValue.Rows(i1).BeginEdit()
                            tblAchValue.Rows(i1)("TARGET_BY_VALUE") = TargetByValue
                            'tblAchValue.Rows(i1)("ACTUAL_BY_VALUE") = Convert.ToDecimal(tblAchValue.Rows(i1)("TOTAL_ACTUAL")) * Convert.ToDecimal(tblAchValue.Rows(i1)("PRICE"))
                            Dim PercAchByBrand As Decimal = common.CommonClass.GetPercentage(100, TotalPOAmount, TargetByValue)
                            tblAchValue.Rows(i1)("ACH_DISPRO_BY_VALUE") = PercAchByBrand
                            Dim RowsProgDisc() As DataRow = Nothing
                            If isAchieved Then
                                ''procced discount
                                ''UPDATE TABLE Achievement
                                ''looping Cari Dispro
                                'check apakah custom dispro by brand sudah di insert

                                ''============ GET THIS SON OF A BITCH OF DISPRO -->VERY HARD BOILED ================================================
                                RowsProgDisc = TAgProgDiscBVal.Select("AGREE_BRAND_ID = '" + AgreeBrandID + "'")
                                If RowsProgDisc.Length > 0 Then
                                    If Not RowsProgDisc(0).IsNull("PRGSV_DISC_PCT") Then
                                        Dispro = Convert.ToDecimal(RowsProgDisc(0)("PRGSV_DISC_PCT"))
                                    End If
                                ElseIf Not IsNothing(rowsC) Then
                                    If rowsC.Length > 0 Then
                                        RowsProgDisc = TAgProgDiscBVal.Select("AGREE_BRAND_ID = '" + CombBrandID + "'")
                                        If Not RowsProgDisc(0).IsNull("PRGSV_DISC_PCT") Then
                                            Dispro = Convert.ToDecimal(RowsProgDisc(0)("PRGSV_DISC_PCT"))
                                        End If
                                    Else
                                        RowsProgDisc = TProgDiscHeaderByVal.Select("AGREEMENT_NO = '" + ListAgreementNo(i) + "'", "UP_TO_PCT DESC")
                                        If RowsProgDisc.Length > 0 Then
                                            For i2 As Integer = 0 To RowsProgDisc.Length - 1
                                                If Not IsDBNull(RowsProgDisc(i2)("UP_TO_PCT")) And Not IsNothing(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                                    If PercAchByBrand > Convert.ToDecimal(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                                        Dispro = Convert.ToDecimal(RowsProgDisc(i2)("PRGSV_DISC_PCT"))
                                                        Exit For
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                Else 'check data di Header Progressive
                                    RowsProgDisc = TProgDiscHeaderByVal.Select("AGREEMENT_NO = '" + ListAgreementNo(i) + "'", "UP_TO_PCT DESC")
                                    If RowsProgDisc.Length > 0 Then
                                        For i2 As Integer = 0 To RowsProgDisc.Length - 1
                                            If Not IsDBNull(RowsProgDisc(i2)("UP_TO_PCT")) And Not IsNothing(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                                If PercAchByBrand > Convert.ToDecimal(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                                    Dispro = Convert.ToDecimal(RowsProgDisc(i2)("PRGSV_DISC_PCT"))
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Else
                                Dim AchievementID As String = tblAchValue.Rows(i1)("ACHIEVEMENT_ID").ToString()
                                'If AchievementID = "END001IDR|118/NI/I/2004.1477230|S1" Or AchievementID = "END001IDR|118/NI/I/2004.1477240|S1" Then
                                '    Stop
                                'End If
                                ''hitung masing-masing
                                ''Check apakah progressive discount sudah di insert oleh user
                                If (TAgProgDiscBVol.Rows.Count <= 0) And (TProgDiscHeaderByVol.Rows.Count <= 0) Then
                                    Throw New Exception("Progressive discount by volume hasn't been set yet")
                                End If

                                RowsProgDisc = TAgProgDiscBVol.Select("AGREE_BRAND_ID = '" + AgreeBrandID + "'")
                                If RowsProgDisc.Length > 0 Then
                                    For i2 As Integer = 0 To RowsProgDisc.Length - 1
                                        If Not IsDBNull(RowsProgDisc(i2)("UP_TO_PCT")) And Not IsNothing(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                            If PercAchByBrand > Convert.ToDecimal(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                                Dispro = Convert.ToDecimal(RowsProgDisc(i2)("PRGSV_DISC_PCT"))
                                                Exit For
                                            End If
                                        End If
                                    Next
                                    'If Not RowsProgDisc(0).IsNull("PRGSV_DISC_PCT") Then
                                    '    Dispro = Convert.ToDecimal(RowsProgDisc(0)("PRGSV_DISC_PCT"))
                                    'End If
                                ElseIf Not IsNothing(rowsC) Then
                                    If rowsC.Length > 0 Then
                                        RowsProgDisc = TAgProgDiscBVol.Select("AGREE_BRAND_ID = '" + CombBrandID + "'")
                                        For i2 As Integer = 0 To RowsProgDisc.Length - 1
                                            If Not IsDBNull(RowsProgDisc(i2)("UP_TO_PCT")) And Not IsNothing(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                                If PercAchByBrand > Convert.ToDecimal(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                                    Dispro = Convert.ToDecimal(RowsProgDisc(i2)("PRGSV_DISC_PCT"))
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                        'If Not RowsProgDisc(0).IsNull("PRGSV_DISC_PCT") Then
                                        '    Dispro = Convert.ToDecimal(RowsProgDisc(0)("PRGSV_DISC_PCT"))
                                        'End If
                                    Else
                                        RowsProgDisc = TProgDiscHeaderByVol.Select("AGREEMENT_NO = '" + ListAgreementNo(i) + "'", "UP_TO_PCT DESC")
                                        If RowsProgDisc.Length > 0 Then
                                            For i2 As Integer = 0 To RowsProgDisc.Length - 1
                                                If Not IsDBNull(RowsProgDisc(i2)("UP_TO_PCT")) And Not IsNothing(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                                    If PercAchByBrand > Convert.ToDecimal(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                                        Dispro = Convert.ToDecimal(RowsProgDisc(i2)("PRGSV_DISC_PCT"))
                                                        Exit For
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                Else 'check data di Header Progressive

                                    RowsProgDisc = TProgDiscHeaderByVol.Select("AGREEMENT_NO = '" + ListAgreementNo(i) + "'", "UP_TO_PCT DESC")
                                    If RowsProgDisc.Length > 0 Then
                                        For i2 As Integer = 0 To RowsProgDisc.Length - 1
                                            If Not IsDBNull(RowsProgDisc(i2)("UP_TO_PCT")) And Not IsNothing(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                                If PercAchByBrand > Convert.ToDecimal(RowsProgDisc(i2)("UP_TO_PCT")) Then
                                                    Dispro = Convert.ToDecimal(RowsProgDisc(i2)("PRGSV_DISC_PCT"))
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                                DISC_OBTAINED_FROM = IIf(((Dispro > 0) And (Convert.ToDecimal(tblAchValue.Rows(i1)("TOTAL_ACTUAL")) > 0)), "T1", "T2")
                            End If
                            tblAchValue.Rows(i1)("DISPRO_BY_VALUE") = IIf((Convert.ToDecimal(tblAchValue.Rows(i1)("TOTAL_ACTUAL")) > 0), Dispro, 0)
                            tblAchValue.Rows(i1)("DISC_BY_VALUE") = Convert.ToDecimal(tblAchValue.Rows(i1)("TOTAL_ACTUAL")) * (Dispro / 100)
                            tblAchValue.Rows(i1)("DISC_DIST_BY_VALUE") = Convert.ToDecimal(tblAchValue.Rows(i1)("ACTUAL_DISTRIBUTOR")) * (Dispro / 100)
                            tblAchValue.Rows(i1)("DISC_OBTAINED_FROM") = DISC_OBTAINED_FROM
                            tblAchValue.Rows(i1).EndEdit()
                        Next
                    End If
                    ''tambahkan sisa listAgreeBrandID dari target yang tidak di combine dan bukan group (agreement umum)
                    Rows = tblAchValue.Select("ISTARGET_GROUP = 0 AND ISCOMBINED_TARGET = 0")
                    If Rows.Length > 0 Then
                        For i1 As Integer = 0 To Rows.Length - 1
                            Dim AgreeBrandID As String = Rows(i1)("AGREE_BRAND_ID")
                            If Not listAgreeBrands.Contains(AgreeBrandID) Then
                                listAgreeBrands.Add(AgreeBrandID)
                            End If
                        Next
                    End If
                    ''get total invoice Before
                    Dim ListCombined As New List(Of String)
                    For i1 As Integer = 0 To listAgreeBrands.Count - 1
                        If Not ListCombined.Contains(listAgreeBrands(i1)) Then
                            Me.ProceedPeriodBeforeForDiscByValue(Flag, listAgreeBrands(i1), tblAchValue, ListCombined)
                        End If
                    Next
                Next
                ''UPDATE AchHeader
                ''GET Detail and Update Detilnya
                ''looping lagi
                For i As Integer = 0 To tblAchValue.Rows.Count - 1
                    Rows = tblAchValueDetail.Select("ACHIEVEMENT_ID = '" & tblAchValue.Rows(i)("ACHIEVEMENT_ID").ToString() & "'")
                    Dim DisproByValue As Decimal = Convert.ToDecimal(tblAchValue.Rows(i)("DISPRO_BY_VALUE"))
                    If Rows.Length > 0 Then
                        For i1 As Integer = 0 To Rows.Length - 1
                            Dim DiscQtyByValue As Decimal = Convert.ToDecimal(Rows(i1)("TOTAL_ACTUAL")) * (DisproByValue / 100)
                            Rows(i1).BeginEdit()
                            Select Case Flag
                                Case "Q1"
                                    'PBQ3,PBQ4' and Y if posible
                                    DiscQtyByValue += (Convert.ToDecimal(Rows(i1)("TOTAL_PBQ3")) * Convert.ToDecimal(tblAchValue.Rows(i)("PERCENT_PBQ3")))
                                    DiscQtyByValue += (Convert.ToDecimal(Rows(i1)("TOTAL_PBQ4")) * Convert.ToDecimal(tblAchValue.Rows(i)("PERCENT_PBQ4")))
                                Case "Q2" 'PBQ4,CPQ1 AND Y If Possible
                                    DiscQtyByValue += (Convert.ToDecimal(Rows(i1)("TOTAL_PBQ4")) * Convert.ToDecimal(tblAchValue.Rows(i)("PERCENT_PBQ4")))
                                    DiscQtyByValue += (Convert.ToDecimal(Rows(i1)("TOTAL_CPQ1")) * Convert.ToDecimal(tblAchValue.Rows(i)("PERCENT_CPQ1")))
                                Case "Q3" 'CPQ1,CPQ2 AND Y If Possible
                                    DiscQtyByValue += (Convert.ToDecimal(Rows(i1)("TOTAL_CPQ1")) * Convert.ToDecimal(tblAchValue.Rows(i)("PERCENT_CPQ1")))
                                    DiscQtyByValue += (Convert.ToDecimal(Rows(i1)("TOTAL_CPQ2")) * Convert.ToDecimal(tblAchValue.Rows(i)("PERCENT_CPQ2")))
                                Case "Q4"
                                    DiscQtyByValue += (Convert.ToDecimal(Rows(i1)("TOTAL_CPQ2")) * Convert.ToDecimal(tblAchValue.Rows(i)("PERCENT_CPQ2")))
                                    DiscQtyByValue += (Convert.ToDecimal(Rows(i1)("TOTAL_CPQ3")) * Convert.ToDecimal(tblAchValue.Rows(i)("PERCENT_CPQ3")))
                                Case "S1"
                                    DiscQtyByValue += (Convert.ToDecimal(Rows(i1)("TOTAL_PBS2")) * Convert.ToDecimal(tblAchValue.Rows(i)("PERCENT_PBS2")))
                            End Select
                            If Convert.ToDecimal(Rows(i1)("TOTAL_PBY")) > 0 Then
                                DiscQtyByValue += (Convert.ToDecimal(Rows(i1)("TOTAL_PBY")) * Convert.ToDecimal(tblAchValue.Rows(i)("PERCENT_PBY")))
                            End If
                            Rows(i1)("DISC_BY_VALUE") = DiscQtyByValue
                            Rows(i1)("DISC_OBTAINED_FROM") = tblAchValue.Rows(i)("DISC_OBTAINED_FROM")
                            Rows(i1).EndEdit()
                        Next
                    End If
                Next
                tblAchValueDetail.AcceptChanges()
                tblAchValue.AcceptChanges()
                For i As Integer = 0 To tblAchValue.Rows.Count - 1
                    tblAchValue.Rows(i).SetModified()
                Next
                Rows = tblAchValue.Select("", "", DataViewRowState.ModifiedOriginal)
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                        " DECLARE @AVGPriceID INT, @V_TARGET_BY_VALUE DECIMAL(18,3);" & vbCrLf & _
                        " SET @V_TARGET_BY_VALUE = 0;" & vbCrLf & _
                        " IF (@TARGET > 0) " & vbCrLf & _
                        " BEGIN SET @V_TARGET_BY_VALUE = @TARGET_BY_VALUE ; END " & vbCrLf & _
                        " SET @AVGPriceID = (SELECT TOP 1 IDApp FROM BRND_AVGPRICE WHERE BRAND_ID = @BRAND_ID AND AVGPRICE = @PRICE_FM AND AVGPRICE_PL = @PRICE_PL ORDER BY START_PERIODE DESC) ;" & vbCrLf & _
                        " UPDATE ACCRUED_HEADER SET TARGET_BY_VALUE = @V_TARGET_BY_VALUE,ACTUAL_BY_VALUE = @ACTUAL_BY_VALUE,DISPRO_BY_VALUE = @DISPRO_BY_VALUE,ACH_DISPRO_BY_VALUE = @ACH_DISPRO_BY_VALUE " & vbCrLf & _
                        ",DISC_BY_VALUE = @DISC_BY_VALUE,COMBINED_BRAND_ID = REPLACE(@COMBINED_BRAND_ID,@AGREEMENT_NO,''),DISC_DIST_BY_VALUE = @DISC_DIST_BY_VALUE,DISC_OBTAINED_FROM = @DISC_OBTAINED_FROM, " & vbCrLf & _
                        " AvgPriceID = @AVGPriceID , LAST_UPDATE = GETDATE(), MODIFY_BY = @MODIFY_BY, MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                        " WHERE ACHIEVEMENT_ID = @ACHIEVEMENT_ID "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.SqlCom.Parameters.Add("@TARGET", SqlDbType.Decimal, 0, "TARGET")
                SqlCom.Parameters.Add("@TARGET_BY_VALUE", SqlDbType.Decimal, 0, "TARGET_BY_VALUE")
                SqlCom.Parameters.Add("@PRICE_FM", SqlDbType.Decimal, 0, "PRICE_FM")
                SqlCom.Parameters.Add("@PRICE_PL", SqlDbType.Decimal, 0, "PRICE_PL")
                SqlCom.Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 32, "AGREEMENT_NO")
                SqlCom.Parameters.Add("@ACTUAL_BY_VALUE", SqlDbType.Decimal, 0, "ACTUAL_BY_VALUE")
                SqlCom.Parameters.Add("@DISPRO_BY_VALUE", SqlDbType.Decimal, 0, "DISPRO_BY_VALUE")
                SqlCom.Parameters.Add("@ACH_DISPRO_BY_VALUE", SqlDbType.Decimal, 0, "ACH_DISPRO_BY_VALUE")
                SqlCom.Parameters.Add("@DISC_BY_VALUE", SqlDbType.Decimal, 0, "DISC_BY_VALUE")
                SqlCom.Parameters.Add("@COMBINED_BRAND_ID", SqlDbType.VarChar, 100, "COMBINED_BRAND_ID")
                SqlCom.Parameters.Add("@DISC_DIST_BY_VALUE", SqlDbType.Decimal, 0, "DISC_DIST_BY_VALUE")
                SqlCom.Parameters.Add("@DISC_OBTAINED_FROM", SqlDbType.VarChar, 3, "DISC_OBTAINED_FROM")
                SqlCom.Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 10, "BRAND_ID")
                SqlCom.Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                SqlCom.Parameters.Add("@ACHIEVEMENT_ID", SqlDbType.VarChar, 110, "ACHIEVEMENT_ID")
                SqlCom.Parameters()("@ACHIEVEMENT_ID").SourceVersion = DataRowVersion.Original
                SqlDat = New SqlDataAdapter() : SqlDat.UpdateCommand = Me.SqlCom
                BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                SqlDat.Update(Rows) : Me.ClearCommandParameters()

                ''update detailnya
                For i As Integer = 0 To tblAchValueDetail.Rows.Count - 1
                    tblAchValueDetail.Rows(i).SetModified()
                Next
                Rows = tblAchValueDetail.Select("", "", DataViewRowState.ModifiedOriginal)
                If Rows.Length > 0 Then
                    Query = "SET DEADLOCK_PRIORITY NORMAL ; SET NOCOUNT ON; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                            "UPDATE ACCRUED_DETAIL SET DISC_BY_VALUE = @DISC_BY_VALUE,DISC_OBTAINED_FROM = @DISC_OBTAINED_FROM,LAST_UPDATE = GETDATE(),MODIFY_BY = @MODIFY_BY,MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                            " WHERE ACHIEVEMENT_BRANDPACK_ID = @ACHIEVEMENT_BRANDPACK_ID "
                    Me.ResetAdapterCRUD()
                    Me.ResetCommandText(CommandType.Text, Query)
                    SqlCom.Parameters.Add("@DISC_BY_VALUE", SqlDbType.Decimal, 0, "DISC_BY_VALUE")
                    SqlCom.Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                    SqlCom.Parameters.Add("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, 70, "ACHIEVEMENT_BRANDPACK_ID")
                    SqlCom.Parameters.Add("@DISC_OBTAINED_FROM", SqlDbType.VarChar, 3, "DISC_OBTAINED_FROM")
                    SqlCom.Parameters()("@ACHIEVEMENT_BRANDPACK_ID").SourceVersion = DataRowVersion.Original
                    SqlDat.UpdateCommand = SqlCom
                    SqlDat.Update(Rows) : SqlCom.Parameters.Clear()
                End If
                Me.CommiteTransaction()
                Return Me.GetAccrued(Flag, , ListAgreementNo)
                Me.mustRecomputeYear = False
            Catch ex As Exception
                Me.mustRecomputeYear = False
                Me.RollbackTransaction() : Me.CloseConnection()
                Me.ClearCommandParameters() : Throw ex
            End Try
            mustRecomputeYear = False
            Return Nothing
        End Function


        Public Function CalculateAccrue(ByVal Flag As String, Optional ByVal DISTRIBUTOR_ID As String = "", Optional ByVal tblAgreement As DataTable = Nothing, Optional ByVal ReturnDS As Boolean = True) As DataSet
            Try
                Me.MessageError = ""
                Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing
                Dim tblDistAgreement As New DataTable("T_Agreement") : tblDistAgreement.Clear()
                'Dim row As DataRow = Nothing
                Dim tblDistAgreementCopy As New DataTable("T_AgrCopy") : tblDistAgreementCopy.Clear()
                Me.OpenConnection()
                If Not IsNothing(tblAgreement) Then
                    tblDistAgreement = tblAgreement : tblDistAgreementCopy = tblAgreement
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
                    tblDistAgreementCopy = tblDistAgreement
                ElseIf Flag <> "" Then
                    If Flag = "Y" Then
                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) > YEAR(@GETDATE) - 1 AND YEAR(END_DATE) <> YEAR(GETDATE()) ; "
                    Else
                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE END_DATE >= @GETDATE  AND START_DATE < @GETDATE AND QS_TREATMENT_FLAG = '" & Flag.Remove(1, 1) & "' ;"
                    End If
                    Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                    Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                    Me.SqlDat.Fill(tblDistAgreement) : Me.ClearCommandParameters()
                    tblDistAgreementCopy = tblDistAgreement
                End If
                Dim MessageHeader As String = "Progressif Discount for Agreement"
                Dim MessageDetail As String = ""
                Dim tblAcrHeader As New DataTable("T_AcrHeader") : tblAcrHeader.Clear()
                Dim tblAcrDetail As New DataTable("T_AcrDetail") : tblAcrDetail.Clear()
                Dim strDecStartDate As String = "", strDecEndDate As String = ""
                Dim HasTarget As Boolean = True
                For i As Integer = 0 To tblDistAgreement.Rows.Count - 1
                    Me.ClearCommandParameters() : HasTarget = True
                    StartDate = Convert.ToDateTime(tblDistAgreement.Rows(i)("START_DATE"))
                    StartDateQ1 = StartDate
                    StartDateS1 = StartDate
                    EndDate = Convert.ToDateTime(tblDistAgreement.Rows(i)("END_DATE")) 'EndDate : EndDateS2 = EndDate
                    EndDateQ4 = EndDate
                    EndDateS2 = EndDate
                    'Dim TotalDays As Long = DateDiff(DateInterval.Day, StartDate, EndDate) : Dim LongDays As Integer = 0
                    If Flag <> "Y" Then
                        If Left(Flag, 1) = "Q" Then
                            EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
                            StartDateQ2 = EndDateQ1.AddDays(1)
                            EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
                            StartDateQ3 = EndDateQ2.AddDays(1)
                            If StartDate >= New Date(2019, 8, 1) And EndDate <= New Date(2020, 7, 31) Then
                                EndDateQ3 = StartDateQ3.AddMonths(2).AddDays(-1)
                            Else
                                EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
                            End If
                            StartDateQ4 = EndDateQ3.AddDays(1)
                        ElseIf Left(Flag, 1) = "S" Then
                            EndDateS1 = StartDateS1.AddMonths(6).AddDays(-1)
                            StartDateS2 = EndDateS1.AddDays(1)
                            'LongDays = TotalDays / 2 : EndDateS1 = StartDate.AddDays(LongDays - 1) : StartDateS2 = StartDate.AddDays(LongDays)
                        End If
                        'Else : LongDays = TotalDays
                    End If
                    'start calculating
                    Select Case Flag
                        Case "Q1"
                            ''prepare data
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateQ1)
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateQ1)
                            Me.CreateTempTable(StartDateQ1, EndDateQ1, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount("Q1", StartDateQ1, EndDateQ1, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString(), tblAcrHeader, tblAcrDetail, MessageDetail, HasTarget, EndDate)
                        Case "Q2"
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateQ2)
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateQ2)
                            Me.CreateTempTable(StartDateQ2, EndDateQ2, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount("Q2", StartDateQ2, EndDateQ2, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString(), tblAcrHeader, tblAcrDetail, MessageDetail, HasTarget, EndDate)
                        Case "Q3"
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateQ3)
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateQ3)
                            Me.CreateTempTable(StartDateQ3, EndDateQ3, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount("Q3", StartDateQ3, EndDateQ3, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString(), tblAcrHeader, tblAcrDetail, MessageDetail, HasTarget, EndDate)
                        Case "Q4"
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateQ4)
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateQ4)
                            Me.CreateTempTable(StartDateQ4, EndDateQ4, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount("Q4", StartDateQ4, EndDateQ4, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString(), tblAcrHeader, tblAcrDetail, MessageDetail, HasTarget, EndDate)
                        Case "S1"
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateS1)
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateS1)
                            Me.CreateTempTable(StartDateS1, EndDateS1, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount("S1", StartDateS1, EndDateS1, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString(), tblAcrHeader, tblAcrDetail, MessageDetail, HasTarget, EndDate)
                        Case "S2"
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateS2)
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateS2)
                            Me.CreateTempTable(StartDateS2, EndDateS2, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount("S2", StartDateS2, EndDateS2, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString(), tblAcrHeader, tblAcrDetail, MessageDetail, HasTarget, EndDate)
                        Case "Y"
                            Me.GenerateDiscount("Y", StartDate, EndDate, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString(), tblAcrHeader, tblAcrDetail, MessageDetail, HasTarget, EndDate)
                    End Select
                    If Not HasTarget Then : tblDistAgreement.Rows.RemoveAt(i) : i -= 1 : End If
                Next
                'ulang untuk mengenerate yearly discount
                If Flag <> "Y" Then
                    For i As Integer = 0 To tblDistAgreementCopy.Rows.Count - 1
                        HasTarget = True
                        StartDate = Convert.ToDateTime(tblDistAgreementCopy.Rows(i)("START_DATE"))
                        EndDate = Convert.ToDateTime(tblDistAgreementCopy.Rows(i)("END_DATE"))
                        Me.GenerateDiscount("Y", StartDate, EndDate, tblDistAgreementCopy.Rows(i)("AGREEMENT_NO").ToString(), tblAcrHeader, tblAcrDetail, MessageDetail, HasTarget, EndDate)
                        If Not HasTarget Then : tblDistAgreementCopy.Rows.RemoveAt(i) : i -= 1 : End If
                    Next
                End If
                If MessageDetail <> "" Then
                    Me.MessageError = MessageHeader & vbCrLf & MessageDetail & vbCrLf & "Has not been defined yet"
                End If
                ''drop table ''tempd db
                Me.ClearCommandParameters()
                Dim ListAgreement As New List(Of String)
                For i As Integer = 0 To tblDistAgreement.Rows.Count - 1
                    ListAgreement.Add(tblDistAgreement.Rows(i)("AGREEMENT_NO"))
                Next
                Me.strStartDateFlag = strDecStartDate : Me.strEndDateFlag = strDecEndDate
                If Not ReturnDS Then : Return Nothing : End If
                Dim Ds As DataSet = Me.GetAccrued(Flag, DISTRIBUTOR_ID, ListAgreement)
                Return Ds
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Try
                    Me.ClearCommandParameters()
                    Me.DropTempTable()
                Catch ex1 As Exception
                    Me.strStartDateFlag = "" : Me.strEndDateFlag = "" : Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                    Throw ex1
                End Try
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Me.strStartDateFlag = "" : Me.strEndDateFlag = ""
                mustRecomputeYear = False
                Throw ex
            Finally
                mustRecomputeYear = False
            End Try
        End Function
        Protected Sub DisposeTempDB()
            'Query = "SET NOCOUNT ON ;" & vbCrLf & _
            '          "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
            '          " BEGIN  DROP TABLE  tempdb..##T_START_DATE_" & Me.ComputerName & " ; END " & vbCrLf & _
            '          " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_MASTER_PO_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
            '          " BEGIN DROP TABLE tempdb..##T_MASTER_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
            '          " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Agreement_Brand_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
            '          " BEGIN DROP TABLE tempdb..##T_Agreement_Brand_" & Me.ComputerName & " ; END " & vbCrLf & _
            '          " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PO_Original_By_Distributor_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
            '          " BEGIN DROP TABLE tempdb..##T_PO_Original_By_Distributor_" & Me.ComputerName & " ; END " & vbCrLf & _
            '          " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Agreement_BrandPack_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
            '          " BEGIN DROP TABLE tempdb..##T_Agreement_BrandPacK_" & Me.ComputerName & " ; END " & vbCrLf & _
            '          " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SELECT_INVOICE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
            '          " BEGIN DROP TABLE Tempdb..##T_SELECT_INVOICE_" & Me.ComputerName & " ; END " & vbCrLf & _
            '          " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_AGREEMENT_NO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
            '          " BEGIN DROP TABLE Tempdb..##T_AGREEMENT_NO_" & Me.ComputerName & " ; END "
            'Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            'Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            'Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
        End Sub
        Public Sub DeleteAccruedHeader(ByVal AchievementID As String)
            Try

                Query = "SET NOCOUNT ON;" & vbCrLf & _
                "DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = '" & AchievementID & "' ;" & vbCrLf & _
                "DELETE FROM ACCRUED_HEADER WHERE ACHIEVEMENT_ID = '" & AchievementID & "' ; " & vbCrLf
                'delete year nya juga
                If AchievementID.Substring(AchievementID.LastIndexOf("|") + 1, 1) <> "Y" Then
                    Dim AchievementYear As String = AchievementID.Remove(AchievementID.Length - 2, 2)
                    AchievementYear = AchievementYear.Insert(AchievementID.LastIndexOf("|") + 1, "Y")
                    Query &= "DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = '" & AchievementYear & "' ;" & vbCrLf & _
                     "DELETE FROM ACCRUED_HEADER WHERE ACHIEVEMENT_ID = '" & AchievementYear & "' ;"
                End If

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

        Public Sub DeleteAccrueDetail(ByVal AchievementBrandPackID As String)
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_BRANDPACK_ID = '" & AchievementBrandPackID & "';"
                Me.CreateCommandSql(CommandType.StoredProcedure, "sp_executesql", ConnectionTo.Nufarm)
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        Public Function IsHasReferencedDataAcruedHeader(ByVal AchievementID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Dim B As Boolean = False
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT 1 WHERE EXISTS(SELECT OOBD.ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_BRANDPACK_DISC OOBD " & vbCrLf & _
                        " INNER JOIN ACCRUED_DETAIL AD ON AD.ACHIEVEMENT_BRANDPACK_ID = OOBD.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                        " WHERE AD.ACHIEVEMENT_ID = '" & AchievementID & "')" & vbCrLf & _
                        "OR EXISTS(SELECT OOR.ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_REMAINDING OOR INNER JOIN ACCRUED_DETAIL AD " & vbCrLf & _
                        " ON AD.ACHIEVEMENT_BRANDPACK_ID = OOR.ACHIEVEMENT_BRANDPACK_ID WHERE AD.ACHIEVEMENT_ID = '" & AchievementID & "')"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then
                        B = True
                    End If
                End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return B
        End Function
        Public Function hasComputedValue(ByVal ListAgreementNo As List(Of String), ByVal Flag As String, ByVal mustcloseConnection As Boolean) As Boolean
            Try
                Dim strQueryListAgrNo As String = ""
                For i As Integer = 0 To ListAgreementNo.Count - 1
                    strQueryListAgrNo &= " SELECT '" & ListAgreementNo(i).ToString() & "' AS AGREEMENT_NO "
                    If i < ListAgreementNo.Count - 1 Then
                        strQueryListAgrNo &= " UNION ALL " & vbCrLf
                    End If
                Next

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT TOP 1 ACH.TARGET_BY_VALUE FROM ACCRUED_HEADER ACH WHERE EXISTS(SELECT AGREEMENT_NO FROM (" & vbCrLf & _
                " " & strQueryListAgrNo & vbCrLf & _
                " )T WHERE AGREEMENT_NO = ACH.AGREEMENT_NO) AND ACH.FLAG = @FLAG AND ACH.TARGET_BY_VALUE > 0 ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    If mustcloseConnection Then : Me.CloseConnection() : End If
                    Return (CDec(retval) > 0)
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function HasAppliedDisc(ByVal ListAgreementNo As List(Of String), ByVal ListFlag As List(Of String), ByRef AgreeAchBy As String, ByRef IsHasComputedDPD As Boolean, ByVal mustcloseConnection As Boolean) As Boolean
            Try

                Dim strAgreementNo As String = " IN('"
                For i As Integer = 0 To ListAgreementNo.Count - 1
                    strAgreementNo = strAgreementNo & ListAgreementNo(i) & "'"
                    If i < ListAgreementNo.Count - 1 Then
                        strAgreementNo &= ",'"
                    End If
                Next
                strAgreementNo &= ")"
                If strAgreementNo.Trim() = "IN(')" Then
                    strAgreementNo = "IN('')"
                End If
                Dim strListFlag As String = " IN('"
                For i As Integer = 0 To ListFlag.Count - 1
                    strListFlag = strListFlag & ListFlag(i) & "'"
                    If i < ListFlag.Count - 1 Then
                        strListFlag &= ",'"
                    End If
                Next
                strListFlag &= ")"
                If strListFlag.Trim() = "IN(')" Then : strListFlag = "IN('')" : End If

                Dim retval As Object = Nothing
                'CHECK APakah Sudah di hitung DPD semester sebelumnya
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT TOP 1 ACHIEVEMENT_ID FROM ACCRUED_HEADER  WHERE AGREEMENT_NO " & strAgreementNo & " AND FLAG " & strListFlag & " ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                OpenConnection() : retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    IsHasComputedDPD = True
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT TOP 1 AGREE_ACH_BY FROM ACCRUED_HEADER WHERE LEN(AGREE_ACH_BY) > 2  AND AGREEMENT_NO " & strAgreementNo & " AND FLAG " & strListFlag & " ;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If Not IsNothing(retval) And Not IsDBNull(retval) Then
                        If mustcloseConnection Then : Me.CloseConnection() : End If
                        AgreeAchBy = retval.ToString()
                        Return (Not String.IsNullOrEmpty(retval.ToString()))
                    End If
                End If
                If mustcloseConnection Then : Me.CloseConnection() : End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function ApplyDiscByVolume(ByVal ListAgreementNo As List(Of String), ByVal Flag As String) As DataSet
            Try
                Dim strAgreementNo As String = " IN('"
                For i As Integer = 0 To ListAgreementNo.Count - 1
                    strAgreementNo = strAgreementNo & ListAgreementNo(i) & "'"
                    If i < ListAgreementNo.Count - 1 Then
                        strAgreementNo &= ",'"
                    End If
                Next
                strAgreementNo &= ")"

                '============UNCOMMENT AFTER DEBUGGING ==============================================================

                'CREATE DATA Temporary T_ACH_HEADER + UserName
                Query = "SET DEADLOCK_PRIORITY NORMAL ; SET NOCOUNT ON ; " & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_LIST_ACH_HEADER_" & Me.ComputerName & "'  AND TYPE = 'U')" & vbCrLf & _
                        " BEGIN DROP TABLE ##T_LIST_ACH_HEADER_" & Me.ComputerName & " ; END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT ACHIEVEMENT_ID INTO [tempdb].[sys].##T_LIST_ACH_HEADER_" & Me.ComputerName & " " & vbCrLf & _
                        " FROM ACCRUED_HEADER WHERE AGREEMENT_NO " & strAgreementNo & " AND FLAG = @FLAG ;"
                Me.ResetCommandText(CommandType.Text, Query)
                AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()


                ''CREATE DATA Temporary T_ACH_DETAIL + UserName dimana discount OC sudah di ambil
                Query = "SET DEADLOCK_PRIORITY NORMAL ; SET NOCOUNT ON ; " & vbCrLf & _
                           "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_LIST_ACH_DETAIL_" & Me.ComputerName & "'  AND TYPE = 'U')" & vbCrLf & _
                           " BEGIN DROP TABLE ##T_LIST_ACH_DETAIL_" & Me.ComputerName & " ; END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT ACD.ACHIEVEMENT_ID, OOBD.ACHIEVEMENT_BRANDPACK_ID INTO [tempdb].[sys].##T_LIST_ACH_DETAIL_" & Me.ComputerName & vbCrLf & _
                        " FROM ORDR_OA_BRANDPACK_DISC OOBD INNER JOIN ACCRUED_DETAIL ACD ON OOBD.ACHIEVEMENT_BRANDPACK_ID = ACD.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                        " WHERE ACD.ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM [tempdb].[sys].##T_LIST_ACH_HEADER_" & Me.ComputerName & ") "
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()


                'UPDATE ACCRUED_HEADER
                Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                       " UPDATE ACCRUED_HEADER SET BONUS_QTY = DISC_BY_VOLUME,BONUS_DISTRIBUTOR = DISC_DIST_BY_VOLUME,AGREE_ACH_BY = 'VOL',LAST_UPDATE = GETDATE(), " & vbCrLf & _
                       " MODIFY_BY = @MODIFY_BY,MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                       " WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM [tempdb].[sys].##T_LIST_ACH_HEADER_" & Me.ComputerName & ") " & vbCrLf & _
                       " AND ACHIEVEMENT_ID NOT IN(SELECT ACHIEVEMENT_ID FROM [tempdb].[sys].##T_LIST_ACH_DETAIL_" & Me.ComputerName & ") ;"
                Me.ResetCommandText(CommandType.Text, Query)
                AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar()

                Query = "SET DEADLOCK_PRIORITY NORMAL ; SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                        " UPDATE ACCRUED_DETAIL SET DISC_QTY = DISC_BY_VOLUME,RELEASE_QTY = 0,LEFT_QTY = DISC_BY_VOLUME, LAST_UPDATE = GETDATE(),MODIFY_BY = @MODIFY_BY ,MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101)" & vbCrLf & _
                        " WHERE ACHIEVEMENT_BRANDPACK_ID NOT IN(SELECT ACHIEVEMENT_BRANDPACK_ID FROM [tempdb].[sys].##T_LIST_ACH_DETAIL_" & Me.ComputerName & ") AND  ACHIEVEMENT_ID IN(SELECT ACHIEVEMENT_ID FROM [tempdb].[sys].##T_LIST_ACH_HEADER_" & Me.ComputerName & ") ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.SqlCom.ExecuteScalar()
                '============ END UNCOMMENT AFTER DEBUGGING ==============================================================


                '============COMMENT THIS AFTER DEBUGGING=============================================================
                'Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                '     " UPDATE ACCRUED_HEADER SET BONUS_QTY = DISC_BY_VOLUME,BONUS_DISTRIBUTOR = DISC_DIST_BY_VOLUME,AGREE_ACH_BY = 'VOL',LAST_UPDATE = GETDATE(), " & vbCrLf & _
                '     " MODIFY_BY = @MODIFY_BY,MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                '      " WHERE AGREEMENT_NO " & strAgreementNo & " AND FLAG = @FLAG ; "
                'Me.ResetCommandText(CommandType.Text, Query)
                'AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                'AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                'OpenConnection() : BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                'Me.SqlCom.ExecuteScalar()

                'Query = "SET DEADLOCK_PRIORITY NORMAL ; SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                '        " UPDATE ACCRUED_DETAIL SET DISC_QTY = DISC_BY_VOLUME,RELEASE_QTY = 0,LEFT_QTY = DISC_BY_VOLUME, LAST_UPDATE = GETDATE(),MODIFY_BY = @MODIFY_BY ,MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101)" & vbCrLf & _
                '        " WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER WHERE AGREEMENT_NO " & strAgreementNo & " AND FLAG = @FLAG)  ;"
                'Me.ResetCommandText(CommandType.Text, Query)
                'Me.SqlCom.ExecuteScalar()
                '==============END COMMENT THIS AFTER DEBUGGING=============================================================

                Me.CommiteTransaction() : Me.ClearCommandParameters()
                Return Me.GetAccrued(Flag, , ListAgreementNo)
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function ApplyDiscByValue(ByVal ListAgreementNo As List(Of String), ByVal Flag As String) As DataSet
            Try
                Dim strAgreementNo As String = " IN('"
                For i As Integer = 0 To ListAgreementNo.Count - 1
                    strAgreementNo = strAgreementNo & ListAgreementNo(i) & "'"
                    If i < ListAgreementNo.Count - 1 Then
                        strAgreementNo &= ",'"
                    End If
                Next
                strAgreementNo &= ")"

                'check apakah sudah di compute by value
                Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                         " SELECT 1 WHERE EXISTS(SELECT TARGET_BY_VALUE FROM ACCRUED_HEADER WHERE AGREEMENT_NO " & strAgreementNo & " AND FLAG = @FLAG AND TARGET_BY_VALUE > 0) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If IsNothing(retval) Then
                    Me.CloseConnection() : Me.ClearCommandParameters()
                    Throw New Exception("No Discount by value obtained, which should be displayed" & vbCrLf & "you may (re)compute discount by volume to convince yourself")
                ElseIf IsDBNull(retval) Then
                    Me.CloseConnection() : Me.ClearCommandParameters()
                    Throw New Exception("No Discount by value obtained, which should be displayed" & vbCrLf & "you may (re)compute discount by volume to convince yourself")
                End If
                '============UNCOMMENT AFTER DEBUGGING ==============================================================
                'CREATE DATA Temporary T_ACH_HEADER + UserName
                Query = "SET DEADLOCK_PRIORITY NORMAL ; SET NOCOUNT ON ; " & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_LIST_ACH_HEADER_" & Me.ComputerName & "'  AND TYPE = 'U')" & vbCrLf & _
                        " BEGIN DROP TABLE ##T_LIST_ACH_HEADER_" & Me.ComputerName & " ; END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT ACHIEVEMENT_ID INTO [tempdb].[sys].##T_LIST_ACH_HEADER_" & Me.ComputerName & " " & vbCrLf & _
                        " FROM ACCRUED_HEADER WHERE AGREEMENT_NO " & strAgreementNo & " AND FLAG = @FLAG ;"
                Me.ResetCommandText(CommandType.Text, Query)
                AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                'CREATE DATA Temporary T_ACH_DETAIL + UserName dimana discount OC sudah di ambil
                Query = "SET DEADLOCK_PRIORITY NORMAL ; SET NOCOUNT ON ; " & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_LIST_ACH_DETAIL_" & Me.ComputerName & "'  AND TYPE = 'U')" & vbCrLf & _
                        " BEGIN DROP TABLE ##T_LIST_ACH_DETAIL_" & Me.ComputerName & " ; END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT ACD.ACHIEVEMENT_ID, OOBD.ACHIEVEMENT_BRANDPACK_ID INTO [tempdb].[sys].##T_LIST_ACH_DETAIL_" & Me.ComputerName & vbCrLf & _
                        " FROM ORDR_OA_BRANDPACK_DISC OOBD INNER JOIN ACCRUED_DETAIL ACD ON OOBD.ACHIEVEMENT_BRANDPACK_ID = ACD.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                        " WHERE ACD.ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM [tempdb].[sys].##T_LIST_ACH_HEADER_" & Me.ComputerName & ") "
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                'UPDATE ACCRUED_HEADER
                Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                       " UPDATE ACCRUED_HEADER SET BONUS_QTY = DISC_BY_VALUE,BONUS_DISTRIBUTOR = DISC_DIST_BY_VALUE,AGREE_ACH_BY = 'VAL', LAST_UPDATE = GETDATE(), " & vbCrLf & _
                       " MODIFY_BY = @MODIFY_BY,MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                       " WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM [tempdb].[sys].##T_LIST_ACH_HEADER_" & Me.ComputerName & ") " & vbCrLf & _
                       " AND ACHIEVEMENT_ID NOT IN(SELECT ACHIEVEMENT_ID FROM [tempdb].[sys].##T_LIST_ACH_DETAIL_" & Me.ComputerName & ") ;"
                Me.ResetCommandText(CommandType.Text, Query)
                AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar()

                Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                        " UPDATE ACCRUED_DETAIL SET DISC_QTY = DISC_BY_VALUE, RELEASE_QTY = 0,LEFT_QTY = DISC_BY_VALUE, LAST_UPDATE = GETDATE(),MODIFY_BY = @MODIFY_BY, MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101)" & vbCrLf & _
                        " WHERE ACHIEVEMENT_BRANDPACK_ID NOT IN(SELECT ACHIEVEMENT_BRANDPACK_ID FROM [tempdb].[sys].##T_LIST_ACH_DETAIL_" & Me.ComputerName & ") AND  ACHIEVEMENT_ID IN(SELECT ACHIEVEMENT_ID FROM [tempdb].[sys].##T_LIST_ACH_HEADER_" & Me.ComputerName & ") ;"
                Me.ResetCommandText(CommandType.Text, Query)
                '============ END UNCOMMENT AFTER DEBUGGING ==============================================================

                '=================COMMENT THIS AFTER DEBUGGING===================================================
                'Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                '                 " UPDATE ACCRUED_HEADER SET BONUS_QTY = DISC_BY_VALUE,BONUS_DISTRIBUTOR = DISC_DIST_BY_VALUE,AGREE_ACH_BY = 'VAL', LAST_UPDATE = GETDATE(), " & vbCrLf & _
                '                 " MODIFY_BY = @MODIFY_BY,MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                '                 " WHERE AGREEMENT_NO " & strAgreementNo & " AND FLAG = @FLAG ; "
                'Me.ResetCommandText(CommandType.Text, Query)
                'AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                'AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                'BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                'Me.SqlCom.ExecuteScalar()

                'Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                '        " UPDATE ACCRUED_DETAIL SET DISC_QTY = DISC_BY_VALUE, RELEASE_QTY = 0,LEFT_QTY = DISC_BY_VALUE, LAST_UPDATE = GETDATE(),MODIFY_BY = @MODIFY_BY, MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101)" & vbCrLf & _
                '        " WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER WHERE AGREEMENT_NO " & strAgreementNo & " AND FLAG = @FLAG )  ;"
                'Me.ResetCommandText(CommandType.Text, Query)
                '================= END COMMENT THIS AFTER DEBUGGING=============================================

                Me.SqlCom.ExecuteScalar() : Me.CommiteTransaction() : Me.ClearCommandParameters()

                Return Me.GetAccrued(Flag, , ListAgreementNo)
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function hasRefdataINOA(ByVal AgreementNO As String, ByVal Flag As String, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT 1 WHERE EXISTS(SELECT OOBD.ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_BRANDPACK_DISC OOBD INNER JOIN ACCRUED_DETAIL AD " & vbCrLf & _
                        "                       ON AD.ACHIEVEMENT_BRANDPACK_ID = OOBD.ACHIEVEMENT_BRANDPACK_ID INNER JOIN ACCRUED_HEADER AH ON AH.ACHIEVEMENT_ID = AD.ACHIEVEMENT_ID " & vbCrLf & _
                        "                       WHERE AH.AGREEMENT_NO = @AGREEMENT_NO AND AH.FLAG = @FLAG " & vbCrLf & _
                        "                       )" & vbCrLf & _
                        "             OR EXISTS(SELECT OOR.ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_REMAINDING OOR INNER JOIN ACCRUED_DETAIL AD " & vbCrLf & _
                        "                       ON AD.ACHIEVEMENT_BRANDPACK_ID = OOR.ACHIEVEMENT_BRANDPACK_ID INNER JOIN ACCRUED_HEADER AH ON AH.ACHIEVEMENT_ID = AD.ACHIEVEMENT_ID " & vbCrLf & _
                        "                       WHERE AH.AGREEMENT_NO = @AGREEMENT_NO AND AH.FLAG = @FLAG " & vbCrLf & _
                        "                       ) "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNO, 32)
                Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return (CInt(retval) > 0)
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function IshasReferencedDataAcrueDetail(ByVal AchievementBrandPackID) As Boolean
            Dim B As Boolean = False
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT 1 WHERE EXISTS(SELECT ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_BRANDPACK_DISC " & vbCrLf & _
                        " WHERE ACHIEVEMENT_BRANDPACK_ID = '" & AchievementBrandPackID & "')" & vbCrLf & _
                        "OR EXISTS(SELECT ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_REMAINDING " & vbCrLf & _
                        " WHERE ACHIEVEMENT_BRANDPACK_ID = '" & AchievementBrandPackID & "');"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim retval As Object = Me.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then
                        B = True
                    End If
                End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return B
        End Function

        Private Sub DisposeTBrandPack()
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "IF OBJECT_ID('tempdb..##T_BRANDPACK') IS NOT NULL " & vbCrLf & _
                        "BEGIN  DROP TABLE tempdb..##T_BRANDPACK ; END"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try

        End Sub

        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            'Me.DisposeTBrandPack()
            Me.Dispose(disposing)
        End Sub

        'Public Function getQsyDiscFlag(ByVal ListAgreementNO As List(Of String), ByVal mustCloseConnection As Boolean) As String
        '    Try
        '        Dim strQueryListAgrNo As String = ""
        '        For i As Integer = 0 To ListAgreementNO.Count - 1
        '            strQueryListAgrNo &= " SELECT '" & ListAgreementNO(i).ToString() & "' AS AGREEMENT_NO "
        '            If i < ListAgreementNO.Count - 1 Then
        '                strQueryListAgrNo &= " UNION ALL " & vbCrLf
        '            End If
        '        Next
        '        Query = "SET NOCOUNT ON;" & vbCrLf & _
        '        " IF OBJECT_ID('TEMPDB..##T_AGREEMENT_NO_" & Me.ComputerName & "') IS NOT NULL " & vbCrLf & _
        '        " BEGIN DROP TABLE TEMPDB..##T_AGREEMENT_NO_" & Me.ComputerName & " ; END " & vbCrLf & _
        '        " SELECT AGREEMENT_NO INTO TEMPDB..##T_AGREEMENT_NO_" & Me.ComputerName & " FROM (" & vbCrLf & _
        '        " " & strQueryListAgrNo & vbCrLf & _
        '        " )T "
        '        If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
        '        Else : Me.CreateCommandSql("sp_executesql", "")
        '        End If
        '        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
        '        OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
        '        Query = "SET NOCOUNT ON; " & vbCrLf & _
        '                " SELECT DISTINCT QS_TREATMENT_FLAG FROM "
        '    Catch ex As Exception

        '    End Try
        'End Function
        Public Function hasComputedDPD(ByVal ListAgreementNO As List(Of String), ByVal Flag As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Dim strQueryListAgrNo As String = ""
                For i As Integer = 0 To ListAgreementNO.Count - 1
                    strQueryListAgrNo &= " SELECT '" & ListAgreementNO(i).ToString() & "' AS AGREEMENT_NO "
                    If i < ListAgreementNO.Count - 1 Then
                        strQueryListAgrNo &= " UNION ALL " & vbCrLf
                    End If
                Next
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " IF OBJECT_ID('TEMPDB..##T_AGREEMENT_NO_" & Me.ComputerName & "') IS NOT NULL " & vbCrLf & _
                " BEGIN DROP TABLE TEMPDB..##T_AGREEMENT_NO_" & Me.ComputerName & " ; END " & vbCrLf & _
                " SELECT AGREEMENT_NO INTO TEMPDB..##T_AGREEMENT_NO_" & Me.ComputerName & " FROM (" & vbCrLf & _
                " " & strQueryListAgrNo & vbCrLf & _
                " )T "
                If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else : Me.CreateCommandSql("sp_executesql", "")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Dim retval As Object = Nothing
                If Not String.IsNullOrEmpty(Flag) Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT 1 WHERE EXISTS(SELECT ACH.ACHIEVEMENT_ID FROM ACCRUED_HEADER ACH WHERE ACH.FLAG = @FLAG AND EXISTS(SELECT AGREEMENT_NO FROM TEMPDB..##T_AGREEMENT_NO_" & Me.ComputerName & " WHERE AGREEMENT_NO = ACH.AGREEMENT_NO))"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Else

                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT DISTINCT(QS_TREATMENT_FLAG) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO IN(SELECT AGREEMENT_NO FROM TEMPDB..##T_AGREEMENT_NO_" & Me.ComputerName & ")"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("stmt", SqlDbType.NVarChar, Query)
                    Dim QSFlags As New List(Of String)
                    Me.SqlRe = Me.SqlCom.ExecuteReader()
                    While Me.SqlRe.Read()
                        QSFlags.Add(SqlRe.GetString(0))
                    End While : Me.SqlRe.Close() : Me.ClearCommandParameters()

                    'Dim strQSFlags As String = "IN("
                    'For i As Integer = 0 To QSFlags.Count - 1
                    '    strQSFlags &= "'" & QSFlags(i) & "'"
                    '    If i < QSFlags.Count - 1 Then
                    '        strQSFlags &= ","
                    '    End If
                    'Next
                    'strQSFlags &= ")"
                    Dim strQSFlags As String = "'S1','S2'"
                    If QSFlags.Count > 1 Then
                        If QSFlags(0) = "Q" Then
                            strQSFlags &= ",'Q1','Q2','Q3','Q4'"
                        ElseIf QSFlags(1) = "Q" Then
                            strQSFlags &= ",'Q1','Q2','Q3','Q4'"
                        End If
                    ElseIf QSFlags(0) = "Q" Then
                        strQSFlags &= "'Q1','Q2','Q3','Q4'"
                    End If
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT 1 WHERE EXISTS(SELECT ACH.ACHIEVEMENT_ID FROM ACCRUED_HEADER ACH WHERE ACH.FLAG IN(" & strQSFlags & ")" & vbCrLf & _
                            " AND EXISTS(SELECT AGREEMENT_NO FROM TEMPDB..##T_AGREEMENT_NO_" & Me.ComputerName & " WHERE AGREEMENT_NO = ACH.AGREEMENT_NO))"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                'Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then
                        ''dispose tempdb..
                        If mustCloseConnection Then : Me.CloseConnection() : End If
                        Return True
                    End If
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
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
        ''dipakai bila user menggenerate discount for oa
        Public Function GenerateDiscountForOA(ByVal tblListAChievement As DataTable, _
        ByRef flag As String, Optional ByVal DISTRIBUTOR_ID As String = "", _
        Optional ByVal ListAGREEMENT_NO As List(Of String) = Nothing, Optional ByVal CanUpdate As Boolean = False) As DataTable
            Try
                Dim AchievementID As String = "", strAgreementNos As String = "IN(" : Me.OpenConnection()
                'Dim DateString As String = NufarmBussinesRules.SharedClass.ServerDate.Month.ToString() & "/" _
                '              & NufarmBussinesRules.SharedClass.ServerDate.Day.ToString() & "/" & _
                '              NufarmBussinesRules.SharedClass.ServerDate.Year.ToString()
                Me.CreateCommandSql("sp_executesql", "")
                Me.OpenConnection()
                Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                For i As Integer = 0 To tblListAChievement.Rows.Count - 1
                    If CBool(tblListAChievement.Rows(i)("CAN_UPDATE")) = True Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "UPDATE ACCRUED_DETAIL SET CAN_RELEASE = 1,CAN_UPDATE = 1 " & vbCrLf & _
                                " WHERE ACHIEVEMENT_BRANDPACK_ID = '" & tblListAChievement.Rows(i)("ACHIEVEMENT_BRANDPACK_ID").ToString() & "';"
                    Else
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "UPDATE ACCRUED_DETAIL SET CAN_RELEASE = 1,CAN_UPDATE = 0 " & vbCrLf & _
                                " WHERE ACHIEVEMENT_BRANDPACK_ID = '" & tblListAChievement.Rows(i)("ACHIEVEMENT_BRANDPACK_ID").ToString() & "';"
                    End If
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Next
                Me.CommiteTransaction()
                'If Not CanUpdate Then
                '    Dim rows() As DataRow = tblListAChievement.Select("CAN UPDATE = " & False)
                '    If rows.Length > 0 Then
                '        For i As Integer = 0 To rows.Length - 1
                '            AchievementID &= "'" & rows(i)("ACHIEVEMENT_BRANDPACK_ID").ToString() & "'"
                '            If (i < rows.Length - 1) Then
                '                AchievementID &= ","
                '            End If
                '        Next
                '        Query = "SET NOCOUNT ON;" & vbCrLf & _
                '                "UPDATE ACCRUED_DETAIL SET CAN_RELEASE = 1,CAN_UPDATE = 0 WHERE ACHIEVEMENT_BRANDPACK_ID IN(" & AchievementID & ");"
                '        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                '        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                '    End If
                'End If

                If ((DISTRIBUTOR_ID <> "") And (Not IsNothing(ListAGREEMENT_NO))) Then
                    If ListAGREEMENT_NO.Count > 0 Then
                        For i As Integer = 0 To ListAGREEMENT_NO.Count - 1
                            strAgreementNos &= "'" & ListAGREEMENT_NO(i) & "'"
                            If i < ListAGREEMENT_NO.Count - 1 Then
                                strAgreementNos &= ","
                            End If
                        Next
                        strAgreementNos &= ")"
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                               "SELECT ACD.ACHIEVEMENT_ID,ACRH.AGREEMENT_NO,ACD.BRANDPACK_ID,BP.BRANDPACK_NAME," & vbCrLf & _
                               "ACD.TOTAL_ACTUAL, ACD.DISC_QTY, ACD.CAN_RELEASE, ACD.RELEASE_QTY - ISNULL(REM.LEFT_QTY,0) AS RELEASE_QTY, ACD.LEFT_QTY,ACD.CREATE_BY,ACD.CAN_UPDATE " & vbCrLf & _
                               " FROM ACCRUED_DETAIL ACD INNER JOIN ACCRUED_HEADER ACRH " & vbCrLf & _
                               " ON ACD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID INNER JOIN BRND_BRANDPACK BP " & vbCrLf & _
                               " ON ACD.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                               " LEFT OUTER JOIN(" & vbCrLf & _
                               "                 SELECT ACHIEVEMENT_BRANDPACK_ID,ISNULL(SUM(LEFT_QTY),0)AS LEFT_QTY FROM ORDR_OA_REMAINDING " & vbCrLf & _
                               "                 GROUP BY ACHIEVEMENT_BRANDPACK_ID" & vbCrLf & _
                               "                 )REM " & vbCrLf & _
                               " ON ACD.ACHIEVEMENT_BRANDPACK_ID = REM.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                               " WHERE ACRH.AGREEMENT_NO " & strAgreementNos & " AND ACRH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ACRH.FLAG = '" & flag & "' OPTION(KEEP PLAN);"
                    Else
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                 "SELECT ACD.ACHIEVEMENT_ID,ACD.ACHIEVEMENT_BRANDPACK_ID,ACRH.AGREEMENT_NO,ACD.BRANDPACK_ID,BP.BRANDPACK_NAME," & vbCrLf & _
                                "ACD.TOTAL_ACTUAL, ACD.DISC_QTY, ACD.CAN_RELEASE, ACD.RELEASE_QTY - REM.LEFT_QTY AS RELEASE_QTY, ACD.LEFT_QTY,ACD.CREATE_BY,ACD.CAN_UPDATE " & vbCrLf & _
                                " FROM ACCRUED_DETAIL ACD INNER JOIN ACCRUED_HEADER ACRH " & vbCrLf & _
                                " ON ACD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID INNER JOIN BRND_BRANDPACK BP " & vbCrLf & _
                                " ON ACD.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                                " LEFT OUTER JOIN(" & vbCrLf & _
                                "                 SELECT ACHIEVEMENT_BRANDPACK_ID,ISNULL(SUM(LEFT_QTY),0)AS LEFT_QTY FROM ORDR_OA_REMAINDING " & vbCrLf & _
                                "                 GROUP BY ACHIEVEMENT_BRANDPACK_ID" & vbCrLf & _
                                "                 )REM " & vbCrLf & _
                                " ON ACD.ACHIEVEMENT_BRANDPACK_ID = REM.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                                " WHERE ACRH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID  AND ACRH.AGREEMENT_NO " & vbCrLf & _
                                " = ANY(SELECT DA.AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA " & vbCrLf & _
                                "       ON DA.AGREEMENT_NO = AA.AGREEMENT_NO WHERE DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                                "       AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 1) AND ACRH.FLAG = '" & flag & "' OPTION(KEEP PLAN);"

                    End If

                ElseIf DISTRIBUTOR_ID <> "" Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT ACD.ACHIEVEMENT_ID,ACD.ACHIEVEMENT_BRANDPACK_ID,ACRH.AGREEMENT_NO,ACD.BRANDPACK_ID,BP.BRANDPACK_NAME," & vbCrLf & _
                            "ACD.TOTAL_ACTUAL, ACD.DISC_QTY, ACD.CAN_RELEASE, ACD.RELEASE_QTY - REM.LEFT_QTY AS RELEASE_QTY, ACD.LEFT_QTY,ACD.CREATE_BY,ACD.CAN_UPDATE " & vbCrLf & _
                            " FROM ACCRUED_DETAIL ACD INNER JOIN ACCRUED_HEADER ACRH " & vbCrLf & _
                            " ON ACD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID INNER JOIN BRND_BRANDPACK BP " & vbCrLf & _
                            " ON ACD.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                            " LEFT OUTER JOIN(" & vbCrLf & _
                            "                 SELECT ACHIEVEMENT_BRANDPACK_ID,ISNULL(SUM(LEFT_QTY),0)AS LEFT_QTY FROM ORDR_OA_REMAINDING " & vbCrLf & _
                            "                 GROUP BY ACHIEVEMENT_BRANDPACK_ID" & vbCrLf & _
                            "                 )REM " & vbCrLf & _
                            " ON ACD.ACHIEVEMENT_BRANDPACK_ID = REM.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                            " WHERE ACRH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID  AND ACRH.AGREEMENT_NO " & vbCrLf & _
                            " = ANY(SELECT DA.AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA " & vbCrLf & _
                            "       ON DA.AGREEMENT_NO = AA.AGREEMENT_NO WHERE DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                            "       AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 1 ) AND ACRH.FLAG = '" & flag & "' OPTION(KEEP PLAN);"
                ElseIf Not IsNothing(tblListAChievement) Then
                    For i As Integer = 0 To ListAGREEMENT_NO.Count - 1
                        strAgreementNos &= "'" & ListAGREEMENT_NO(i) & "'"
                        If i < ListAGREEMENT_NO.Count - 1 Then
                            strAgreementNos &= ","
                        End If
                        strAgreementNos &= ")"
                    Next
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                               "SELECT ACD.ACHIEVEMENT_ID,ACRH.AGREEMENT_NO,ACD.BRANDPACK_ID,BP.BRANDPACK_NAME," & vbCrLf & _
                               "ACD.TOTAL_ACTUAL, ACD.DISC_QTY, ACD.CAN_RELEASE, ACD.RELEASE_QTY - ISNULL(REM.LEFT_QTY,0) AS RELEASE_QTY, ACD.LEFT_QTY,ACD.CREATE_BY,ACD.CAN_UPDATE " & vbCrLf & _
                               " FROM ACCRUED_DETAIL ACD INNER JOIN ACCRUED_HEADER ACRH " & vbCrLf & _
                               " ON ACD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID INNER JOIN BRND_BRANDPACK BP " & vbCrLf & _
                               " ON ACD.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                               " LEFT OUTER JOIN(" & vbCrLf & _
                               "                 SELECT ACHIEVEMENT_BRANDPACK_ID,ISNULL(SUM(LEFT_QTY),0)AS LEFT_QTY FROM ORDR_OA_REMAINDING " & vbCrLf & _
                               "                 GROUP BY ACHIEVEMENT_BRANDPACK_ID" & vbCrLf & _
                               "                 )REM " & vbCrLf & _
                               " ON ACD.ACHIEVEMENT_BRANDPACK_ID = REM.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                               " WHERE ACRH.AGREEMENT_NO " & strAgreementNos & " AND ACRH.FLAG = '" & flag & "' OPTION(KEEP PLAN);"
                ElseIf flag = "Y" Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT ACD.ACHIEVEMENT_ID,ACRH.AGREEMENT_NO,ACD.BRANDPACK_ID,BP.BRANDPACK_NAME," & vbCrLf & _
                            "ACD.TOTAL_ACTUAL, ACD.DISC_QTY, ACD.CAN_RELEASE, ACD.RELEASE_QTY, ACD.LEFT_QTY,ACD.CREATE_BY,ACD.CAN_UPDATE " & vbCrLf & _
                            " FROM ACCRUED_DETAIL ACD INNER JOIN ACCRUED_HEADER ACRH " & vbCrLf & _
                            " ON ACD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID INNER JOIN BRND_BRANDPACK BP " & vbCrLf & _
                            " ON ACD.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                            " LEFT OUTER JOIN(" & vbCrLf & _
                            "                 SELECT ACHIEVEMENT_BRANDPACK_ID,ISNULL(SUM(LEFT_QTY),0)AS LEFT_QTY FROM ORDR_OA_REMAINDING " & vbCrLf & _
                            "                 GROUP BY ACHIEVEMENT_BRANDPACK_ID" & vbCrLf & _
                            "                 )REM " & vbCrLf & _
                            " ON ACD.ACHIEVEMENT_BRANDPACK_ID = REM.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                            " WHERE ACRH.AGREEMENT_NO = ANY(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) -1 " & vbCrLf & _
                            " AND QS_TREATMENT_FLAG = 'Y') AND ACRH.FLAG = 'Y' OPTION(KEEP PLAN);"
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT ACD.ACHIEVEMENT_ID,ACD.ACHIEVEMENT_BRANDPACK_ID,ACRH.AGREEMENT_NO,ACD.BRANDPACK_ID,BP.BRANDPACK_NAME," & vbCrLf & _
                            "ACD.TOTAL_ACTUAL, ACD.DISC_QTY, ACD.CAN_RELEASE, ACD.RELEASE_QTY - REM.LEFT_QTY AS RELEASE_QTY, ACD.LEFT_QTY,ACD.CREATE_BY,ACD.CAN_UPDATE " & vbCrLf & _
                            " FROM ACCRUED_DETAIL ACD INNER JOIN ACCRUED_HEADER ACRH " & vbCrLf & _
                            " ON ACD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID INNER JOIN BRND_BRANDPACK BP " & vbCrLf & _
                            " ON ACD.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                            " LEFT OUTER JOIN(" & vbCrLf & _
                            "                 SELECT ACHIEVEMENT_BRANDPACK_ID,ISNULL(SUM(LEFT_QTY),0)AS LEFT_QTY FROM ORDR_OA_REMAINDING " & vbCrLf & _
                            "                 GROUP BY ACHIEVEMENT_BRANDPACK_ID" & vbCrLf & _
                            "                 )REM " & vbCrLf & _
                            " ON ACD.ACHIEVEMENT_BRANDPACK_ID = REM.ACHIEVEMENT_BRANDPACK_ID " & vbCrLf & _
                            " WHERE ACRH.AGREEMENT_NO = ANY(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) - 1 " & vbCrLf & _
                            "                               )  AND ACRH.FLAG = '" & flag & "' OPTION(KEEP PLAN);"
                End If
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                'me.AddParameter("@
                Dim dtTable As New DataTable("ACHIEVEMENT_DETAIL") : dtTable.Clear()
                Return Me.FillDataTable(dtTable)
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        'Public Function getListLeftAndReleasedDPDInVoice(ByVal Flag As String, ByVal mustCloseConnection As Boolean, Optional ByVal DISTRIBUTOR_ID As String = "", Optional ByVal ListAGREEMENT_NO As List(Of String) = Nothing) As DataView
        '    'ACHIEVEMENT_ID()
        '    'BRANDPACK_ID()
        '    'BRANDPACK_NAME()
        '    'DISC_QTY()
        '    'PO_REF_NO()
        '    'PO_REF_NO()
        '    'INVNUMBER()
        '    'INVDATE()
        '    Try
        '        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; " & vbCrLf & _
        '                "SELECT "
        '    Catch ex As Exception

        '    End Try
        'End Function
    End Class
End Namespace

