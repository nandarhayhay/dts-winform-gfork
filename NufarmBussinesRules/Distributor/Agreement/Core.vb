Namespace DistributorAgreement
    Public Class Core
        Inherits NufarmBussinesRules.DistributorAgreement.Include
        Private m_ViewDistributorAgreement As DataView
        Private m_ViewDiscount As DataView
        Private Query
#Region " OTHER PROPERTY AND FUNTION "

        Public Function GetPercentage(ByVal persen As Decimal, ByVal SUM_PO_ORIGINALQTY As Decimal, ByVal TARGET_QTY As Decimal) As Decimal
            Return (SUM_PO_ORIGINALQTY * persen * 1) / TARGET_QTY
        End Function

        Public Function CreateViewDiscount(ByVal AGREEMENT_NO As String, ByVal QSY_Flag As Object, ByVal DISTRIBUTOR_ID As String) As DataView
            Try
                Me.CreateCommandSql("Usp_GetView_BrandPack_Saving", "")
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25) ' VARCHAR(25),
                Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, QSY_Flag, 2) ' CHAR(1)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Dim tblDiscount As New DataTable("DISCOUNT_AGREEMENT")
                tblDiscount.Clear()
                Me.FillDataTable(tblDiscount)
                Me.m_ViewDiscount = tblDiscount.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewDiscount
        End Function

        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewDiscount) Then
                Me.m_ViewDiscount.Dispose()
                Me.m_ViewDiscount = Nothing
            End If
            If Not IsNothing(Me.m_ViewDistributorAgreement) Then
                Me.m_ViewDistributorAgreement.Dispose()
                Me.m_ViewDistributorAgreement = Nothing
            End If
        End Sub

        Public ReadOnly Property ViewDistributorAgreement() As DataView
            Get
                Return Me.m_ViewDistributorAgreement
            End Get
        End Property

        Public ReadOnly Property ViewDiscount() As DataView
            Get
                Return Me.m_ViewDiscount
            End Get
        End Property
        Public Function IsDistributorHoldAgreement(ByVal DISTRIBUTOR_ID As String, ByVal AGREEMENT_NO As String) As Boolean
            Try
                Query = "SET NOCOUNT ON;SELECT 1 WHERE EXISTS(SELECT TOP 1 DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE DISTRIBUTOR_ID = '" & _
                               DISTRIBUTOR_ID & "' AND AGREEMENT_NO = '" & AGREEMENT_NO & "');"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim retval As Object = Me.ExecuteScalar()
                If (IsNothing(retval)) Then
                    Throw New Exception("Such AGREEMENT_NO is not held by distributorID " & DISTRIBUTOR_ID)
                End If
                Return True
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Sub Delete(ByVal ID As String)
            Try
                Query = "SET NOCOUNT ON;IF EXISTS(SELECT BRND_B_S_ID FROM ORDR_OA_BRANDPACK_DISC WHERE BRND_B_S_ID = @BRND_B_S_ID)" & _
                               vbCrLf & " BEGIN " & _
                               vbCrLf & " RAISERROR('Data has been used in OA discount,can not delete data',16,1);RETURN" & _
                               vbCrLf & " END " & _
                               vbCrLf & "ELSE " & _
                               vbCrLf & " BEGIN " & _
                               vbCrLf & " DELETE FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = @BRND_B_S_ID" & _
                               vbCrLf & " END"
                Me.CreateCommandSql("", QUERY)
                Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, ID)
                Me.OpenConnection() : Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection() : Throw ex
            End Try
        End Sub
#End Region

#Region " AGREEMENT NO GROUP "

        Public Function CreateViewDistributorAgreement() As DataView
            Try
                Me.CreateCommandSql("", "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME,AGREEMENT_NO,AGREEMENT_DESC FROM VIEW_AGREEMENT " & _
                " WHERE YEAR(END_DATE) >= (YEAR(GETDATE())-1); ")
                Dim tblDistAgreement As New DataTable("DISTRIBUTOR_AGREEMENT")
                tblDistAgreement.Clear()
                Me.FillDataTable(tblDistAgreement)
                Me.m_ViewDistributorAgreement = tblDistAgreement.DefaultView()
                'Me.m_ViewDistributorAgreement.Sort = "DISTRIBUTOR_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistributorAgreement
        End Function

        Public Function CreateViewDistributorAgreement(ByVal HasGenerated As Boolean, ByVal FLAG As Object) As DataView
            Try
                Me.CreateCommandSql("Usp_Create_View_Generated_Agreement", "")
                If HasGenerated Then
                    Me.AddParameter("@_HAS_GENERATED", SqlDbType.Bit, CObj(1)) ' BIT,
                Else
                    Me.AddParameter("@_HAS_GENERATED", SqlDbType.Bit, CObj(0))

                End If
                Me.AddParameter("@_FLAG", SqlDbType.VarChar, FLAG, 2) ' VARCHAR(2)
                Dim tblDistAgreement As New DataTable("DISTRIBUTOR_AGREEMENT")
                tblDistAgreement.Clear()
                Me.FillDataTable(tblDistAgreement)
                Me.m_ViewDistributorAgreement = tblDistAgreement.DefaultView()
                Return Me.m_ViewDistributorAgreement
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function GetPeriodTreatment(ByVal AGREEMENT_NO As String) As String
            Dim retval As Object = Nothing
            Try
                retval = Me.ExecuteScalar("", "SET NOCOUNT ON;SELECT QS_TREATMENT_FLAG FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                If IsDBNull(retval) Then
                    Throw New System.Exception("AGREEMENT has no Flag")
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return retval.ToString()
        End Function

#End Region

#Region " AGREEMENT GROUP "

        Public Sub GenerateQuarterly2Discount_1(ByVal AGREEMENT_NO As String, ByVal DISTRIBUTOR_ID As String)
            Try
                Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                            StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                            StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                            StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing

                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO OPTION(KEEP PLAN);"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Me.OpenConnection() : Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    StartDate = Me.SqlRe.GetDateTime(0) : EndDate = Me.SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                StartDateQ1 = StartDate : StartDateS1 = StartDate : EndDateQ4 = EndDate : EndDateS2 = EndDate
                EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
                StartDateQ2 = EndDateQ1.AddDays(1)
                EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
                StartDateQ3 = EndDateQ2.AddDays(1)
                EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
                StartDateQ4 = EndDateQ3.AddDays(1)
                EndDateS1 = StartDateS1.AddMonths(6).AddDays(-1)
                StartDateS2 = EndDateS1.AddDays(1)
                'Dim SUMDAYS As Object = Nothing
                'Me.CreateCommandSql("", "SELECT DATEDIFF(day, START_DATE, END_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'SUMDAYS = Me.ExecuteScalar()
                'If IsDBNull(SUMDAYS) Then
                '    Throw New System.Exception("Interval time for Agreement " & AGREEMENT_NO & " Hasn't been set yet")
                'End If
                'SUMDAYS = CInt(SUMDAYS)
                'Dim END_DATE_Q2 As Integer = (SUMDAYS / 4) * 2

                'If Convert.ToDateTime(END_DATE) > PO_REF_DATE Then
                '     ', "information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                'End If

                Dim START_DATE_Q2 As String = "" 'Object = Me.ExecuteScalar("", "SELECT DATEADD(DAY," & CInt(SUMDAYS / 4) & ",START_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'If IsDBNull(START_DATE_Q2) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If

                'START_DATE_Q2 = CStr("'" & Month(Convert.ToDateTime(START_DATE_Q2)).ToString() & "/" & Day(Convert.ToDateTime(START_DATE_Q2)).ToString() & "/" & Year(Convert.ToDateTime(START_DATE_Q2)).ToString() & "'")

                START_DATE_Q2 = "'" & StartDateQ2.Month.ToString() & "/" & StartDateQ2.Day.ToString() & "/" & StartDateQ2.Year.ToString() & "'"

                'Me.CreateCommandSql("", "SELECT DATEADD(DAY,90,CAST((" & START_DATE_Q2 & ")AS DATETIME)) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                Dim END_DATE As String = "" 'Object = Me.ExecuteScalar()
                'If (IsDBNull(END_DATE)) Or (IsNothing(END_DATE)) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                If EndDateQ2 >= NufarmBussinesRules.SharedClass.ServerDate() Then
                    Throw New System.Exception("Quarterly 2 hasn't closed yet" & vbCrLf & "System can not process the request")
                End If
                'END_DATE = CStr("'" & Month(Convert.ToDateTime(END_DATE)).ToString() & "/" & Day(Convert.ToDateTime(END_DATE)).ToString() & "/" & Year(Convert.ToDateTime(END_DATE)).ToString() & "'")

                END_DATE = "'" & EndDateQ2.Month.ToString() & "/" & EndDateQ2.Day.ToString() & "/" & EndDateQ2.Year.ToString() & "'"

                Dim BrandID_1 As Object = Nothing
                Dim AgreeBrandID As Object = Nothing
                Dim Target_Q2 As Decimal = 0
                Dim TotalPO As Decimal = 0
                Dim BrandID_2 As Object = Nothing
                Dim CombAgreeBrandID As Object = Nothing
                'Dim Target_Q2_2 As Object = Nothing
                Dim DISC_QTY As Decimal = 0 'AGREE_DISC_PCT * TotalPO
                Dim TotalPOBrandPack_QTY As Decimal = 0
                Dim TotalAmount As Decimal = 0
                Dim Price As Object = Nothing
                Dim DISC_AMOUNT As Decimal = 0
                Dim BRANDPACK_ID As String = ""

                'UNTUK KASUS YANG TIDAK DI COMBINED-------------------------------------------------
                'SELECSI BRAND_ID DARI AGREEMENT YANG TIDAK DI COMBINED
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,BRAND_ID,TARGET_Q2 FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = '" & _
                AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NULL")
                Dim tblBrand As New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                If tblBrand.Rows.Count = 0 Then
                    'Throw New System.Exception("Brands for Agreement " & AGREEMENT_NO & " Hasn't been defined yet")
                Else
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        BrandID_1 = tblBrand.Rows(i)("BRAND_ID")
                        AgreeBrandID = tblBrand.Rows(i)("AGREE_BRAND_ID")
                        If tblBrand.Rows(i).IsNull("TARGET_Q2") Then
                            Throw New System.Exception("Some Brand for agreement " & AGREEMENT_NO & " has a null value for target Q1" & vbCrLf & _
                                                        "System can not process request due to its Condition")
                        End If
                        Target_Q2 = Convert.ToDecimal(tblBrand.Rows(i)("TARGET_Q2"))
                        'FOUND AGREEMENT_CHECK
                        Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                              & "( " & vbCrLf _
                                              & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                              & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                              & " (" & vbCrLf _
                                              & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                              & " WHERE OPB.PO_REF_NO IN(" _
                                              & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_Q2 _
                                              & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                              & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                              & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                              & ")OPB " & vbCrLf _
                                              & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                              & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                        Dim SUMPO As Object = Me.ExecuteScalar()
                        If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                            TotalPO = Convert.ToDecimal(SUMPO)
                        ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                        Else : TotalPO = 0
                        End If
                        If Not TotalPO <= 0 Then
                            'GET PERCENTAGE
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_Q2)
                            'Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                            '"' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            End If
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Quarterly 2 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                              " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                              " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                             START_DATE_Q2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                            " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q2", 2)
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q2", 2)
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If

                '--------UNTUK KASUS YANG DI COMBINED ------------------------------------------------------
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE" & _
                " WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NOT NULL")
                '------------------------------------------------------------------------------------
                tblBrand = New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                Dim HastCollCommBined As Hashtable = Nothing
                Dim NVCombUnik As System.Collections.Specialized.NameValueCollection = Nothing
                If Not tblBrand.Rows.Count = 0 Then
                    HastCollCommBined = New Hashtable()
                    HastCollCommBined.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        'masukan key dari agree_brand_id,valuenya denga comb_agree_brand_id
                        HastCollCommBined.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID"), tblBrand.Rows(i)("AGREE_BRAND_ID"))
                    Next

                    'Dim CollValue As New System.Collections.Specialized.NameValueCollection
                    'item data table sudah masuk semua sekarang buat NameValueCollection unik
                    NVCombUnik = New System.Collections.Specialized.NameValueCollection
                    NVCombUnik.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1 ' looping sampai habis
                        'IF ME.HastCollCommBined.ContainsValue(
                        If HastCollCommBined.ContainsValue(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) Then ' jika ditemukan 
                            NVCombUnik.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID").ToString(), tblBrand.Rows(i)("AGREE_BRAND_ID").ToString()) ' isi NameValueCollection unik
                            HastCollCommBined.Remove(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) 'remove item yang memilki key = comb_agree_brand_id
                        End If
                    Next
                    ' sekarang NameValueCollection UNIK HANYA DI ISI DENGAN KEY AGREE_BRAND_ID DAN VALUE COMB_BRAND_ID
                    For I As Integer = 0 To NVCombUnik.Count - 1
                        'AMBIL DULU valuenya
                        AgreeBrandID = NVCombUnik.Get(I).ToString()
                        CombAgreeBrandID = NVCombUnik.GetKey(I)
                        Dim T_Q1 As Object = Nothing
                        Dim T_Q2 As Object = Nothing
                        'AGREE_BRAND_ID DULU
                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_Q2 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_1 = Me.SqlRe("BRAND_ID")
                            T_Q1 = Me.SqlRe("TARGET_Q2")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_1)) And (Not IsDBNull(T_Q1)) Then
                            BrandID_1 = CStr(BrandID_1)
                            Target_Q2 = Convert.ToDecimal(T_Q1)
                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                                   & "( " & vbCrLf _
                                                   & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                                   & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                                   & " (" & vbCrLf _
                                                   & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                                   & " WHERE OPB.PO_REF_NO IN(" _
                                                   & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_Q2 _
                                                   & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                                   & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                                   & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                                   & ")OPB " & vbCrLf _
                                                   & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                                   & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO = Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If

                        End If
                        'COMB_AGREE_BRAND_ID

                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_Q2 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_2 = Me.SqlRe("BRAND_ID")
                            T_Q2 = Me.SqlRe("TARGET_Q2")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_2)) And (Not IsDBNull(T_Q2)) Then
                            BrandID_2 = CStr(BrandID_2)
                            Target_Q2 = Target_Q2 + Convert.ToDecimal(T_Q2)

                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                                   & "( " & vbCrLf _
                                                   & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                                   & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                                   & " (" & vbCrLf _
                                                   & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                                   & " WHERE OPB.PO_REF_NO IN(" _
                                                   & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_Q2 _
                                                   & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                                   & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                                   & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                                   & ")OPB " & vbCrLf _
                                                   & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                                   & ") OPO WHERE BRAND_ID = '" & BrandID_2.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO += Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If
                        End If

                        If Not TotalPO = 0 Then
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_Q2)
                            'Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                            '"' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            ElseIf CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + CombAgreeBrandID.ToString() + "'")) Then
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & _
                                                "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                                "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            End If
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Quarterly 2 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                             " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                             " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                            START_DATE_Q2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                         " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q2", 2)
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If


                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                             " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                             " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                            START_DATE_Q2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                         " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q2", 2)
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q2", 2)
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL COMB_AGREE_BRAND_ID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q2", 2)
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                        End If
                    Next
                End If

                '---------------------------------------------------------------------------------------------------

            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub GenerateQuarterly1Discount_1(ByVal AGREEMENT_NO As String, ByVal DISTRIBUTOR_ID As String)
            Try
                Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                             StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                             StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                             StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing

                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO OPTION(KEEP PLAN);"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Me.OpenConnection() : Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    StartDate = Me.SqlRe.GetDateTime(0) : EndDate = Me.SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                StartDateQ1 = StartDate : StartDateS1 = StartDate : EndDateQ4 = EndDate : EndDateS2 = EndDate
                EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
                StartDateQ2 = EndDateQ1.AddDays(1)
                EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
                StartDateQ3 = EndDateQ2.AddDays(1)
                EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
                StartDateQ4 = EndDateQ3.AddDays(1)
                EndDateS1 = StartDateS1.AddMonths(6).AddDays(-1)
                StartDateS2 = EndDateS1.AddDays(1)

                'Dim SUMDAYS As Object = Nothing
                'Me.CreateCommandSql("", "SELECT DATEDIFF(day, START_DATE, END_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'SUMDAYS = Me.ExecuteScalar()
                'If IsDBNull(SUMDAYS) Then
                '    Throw New System.Exception("Interval time for Agreement " & AGREEMENT_NO & " Hasn't been set yet")
                'End If
                'SUMDAYS = CInt(SUMDAYS)
                ''Dim END_DATE_Q1 As Integer = SUMDAYS / 4
                'Me.CreateCommandSql("", "SELECT DATEADD(DAY,90,START_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                Dim END_DATE As String = "" 'Me.ExecuteScalar()
                'If (IsDBNull(END_DATE)) Or (IsNothing(END_DATE)) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                'If Convert.ToDateTime(END_DATE) > PO_REF_DATE Then
                '     ', "information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                'End If
                'If Convert.ToDateTime(END_DATE) >= NufarmBussinesRules.SharedClass.ServerDate() Then
                '    Throw New System.Exception("Quarterly 1 hasn't closed yet" & vbCrLf & "System can not process the request")
                'End If
                If EndDateQ1 >= NufarmBussinesRules.SharedClass.ServerDate() Then
                    Throw New System.Exception("Quarterly 1 hasn't closed yet" & vbCrLf & "System can not process the request")
                End If

                'END_DATE = CStr("'" & Month(Convert.ToDateTime(END_DATE)).ToString() & "/" & Day(Convert.ToDateTime(END_DATE)).ToString() & "/" & Year(Convert.ToDateTime(END_DATE)).ToString() & "'")
                Dim AGREEMENT_START_DATE As String = "" 'Object = Me.ExecuteScalar("", "SELECT START_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'If IsDBNull(AGREEMENT_START_DATE) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                'AGREEMENT_START_DATE = CStr("'" & Month(Convert.ToDateTime(AGREEMENT_START_DATE)).ToString() & "/" & Day(Convert.ToDateTime(AGREEMENT_START_DATE)).ToString() & "/" & Year(Convert.ToDateTime(AGREEMENT_START_DATE)).ToString() & "'")

                AGREEMENT_START_DATE = "'" & StartDate.Month.ToString() & "/" & StartDate.Day.ToString() & "/" & StartDate.Year.ToString() & "'"
                END_DATE = "'" & EndDateQ1.Month.ToString() & "/" & EndDateQ1.Day.ToString() & "/" & EndDateQ1.Year.ToString() & "'"

                Dim BrandID_1 As Object = Nothing
                Dim AgreeBrandID As Object = Nothing
                Dim Target_Q1 As Decimal = 0
                Dim TotalPO As Decimal = 0
                Dim BrandID_2 As Object = Nothing
                Dim CombAgreeBrandID As Object = Nothing
                'Dim Target_Q1_2 As Object = Nothing
                Dim DISC_QTY As Decimal = 0 'AGREE_DISC_PCT * TotalPO
                Dim TotalPOBrandPack_QTY As Decimal = 0
                Dim TotalAmount As Decimal = 0
                Dim Price As Object = DBNull.Value
                Dim DISC_AMOUNT As Decimal = 0
                Dim BRANDPACK_ID As String = ""

                'UNTUK KASUS YANG TIDAK DI COMBINED-------------------------------------------------
                'SELECSI BRAND_ID DARI AGREEMENT YANG TIDAK DI COMBINED
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,BRAND_ID,TARGET_Q1 FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = '" & _
                AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NULL")
                Dim tblBrand As New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                If tblBrand.Rows.Count = 0 Then
                    'Throw New System.Exception("Brands for Agreement " & AGREEMENT_NO & " Hasn't been defined yet")
                Else
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        BrandID_1 = tblBrand.Rows(i)("BRAND_ID")
                        AgreeBrandID = tblBrand.Rows(i)("AGREE_BRAND_ID")
                        If tblBrand.Rows(i).IsNull("TARGET_Q1") Then
                            Throw New System.Exception("Some Brand for agreement " & AGREEMENT_NO & " has a null value for target Q1" & vbCrLf & _
                                                        "System can not process request due to its Condition")
                        End If
                        Target_Q1 = Convert.ToDecimal(tblBrand.Rows(i)("TARGET_Q1"))

                        Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                         & "( " & vbCrLf _
                         & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                         & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                         & " (" & vbCrLf _
                         & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                         & " WHERE OPB.PO_REF_NO IN(" _
                         & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & AGREEMENT_START_DATE _
                         & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                         & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                         & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                         & ")OPB " & vbCrLf _
                         & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                         & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                        Dim SUMPO As Object = Me.ExecuteScalar()
                        If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                            TotalPO = Convert.ToDecimal(SUMPO)
                        ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                        Else : TotalPO = 0
                        End If

                        If Not TotalPO = 0 Then
                            'GET PERCENTAGE
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_Q1)

                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            End If
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Quarterly 1 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()

                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                              " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                              " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                              AGREEMENT_START_DATE & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                            " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),\
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If

                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING

                                    End If
                                Next
                            End If
                        End If
                    Next
                End If

                '--------UNTUK KASUS YANG DI COMBINED ------------------------------------------------------
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE" & _
                " WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NOT NULL")
                '------------------------------------------------------------------------------------
                tblBrand = New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                Dim HastCollCommBined As Hashtable = Nothing
                Dim NVCombUnik As System.Collections.Specialized.NameValueCollection = Nothing
                If Not tblBrand.Rows.Count = 0 Then
                    HastCollCommBined = New Hashtable()
                    HastCollCommBined.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        'masukan key dari agree_brand_id,valuenya denga comb_agree_brand_id
                        HastCollCommBined.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID"), tblBrand.Rows(i)("AGREE_BRAND_ID"))
                    Next

                    'Dim CollValue As New System.Collections.Specialized.NameValueCollection
                    'item data table sudah masuk semua sekarang buat NameValueCollection unik
                    NVCombUnik = New System.Collections.Specialized.NameValueCollection
                    NVCombUnik.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1 ' looping sampai habis
                        'IF ME.HastCollCommBined.ContainsValue(
                        If HastCollCommBined.ContainsValue(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) Then ' jika ditemukan 
                            NVCombUnik.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID").ToString(), tblBrand.Rows(i)("AGREE_BRAND_ID").ToString()) ' isi NameValueCollection unik
                            HastCollCommBined.Remove(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) 'remove item yang memilki key = comb_agree_brand_id
                        End If
                    Next
                    ' sekarang NameValueCollection UNIK HANYA DI ISI DENGAN KEY AGREE_BRAND_ID DAN VALUE COMB_BRAND_ID
                    For I As Integer = 0 To NVCombUnik.Count - 1
                        'AMBIL DULU valuenya
                        AgreeBrandID = NVCombUnik.Get(I).ToString()
                        CombAgreeBrandID = NVCombUnik.GetKey(I)
                        Dim T_Q1 As Object = Nothing
                        Dim T_Q2 As Object = Nothing
                        'AGREE_BRAND_ID DULU
                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_Q1 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_1 = Me.SqlRe("BRAND_ID")
                            T_Q1 = Me.SqlRe("TARGET_Q1")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_1)) And (Not IsDBNull(T_Q1)) Then
                            BrandID_1 = CStr(BrandID_1)
                            Target_Q1 = Convert.ToDecimal(T_Q1)

                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                                    & "( " & vbCrLf _
                                                    & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                                    & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                                    & " (" & vbCrLf _
                                                    & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                                    & " WHERE OPB.PO_REF_NO IN(" _
                                                    & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & AGREEMENT_START_DATE _
                                                    & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                                    & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                                    & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                                    & ")OPB " & vbCrLf _
                                                    & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                                    & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO = Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If
                        End If
                        'COMB_AGREE_BRAND_ID

                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_Q1 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_2 = Me.SqlRe("BRAND_ID")
                            T_Q2 = Me.SqlRe("TARGET_Q1")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_2)) And (Not IsDBNull(T_Q2)) Then
                            BrandID_2 = CStr(BrandID_2)
                            Target_Q1 = Target_Q1 + Convert.ToDecimal(T_Q2)

                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                                     & "( " & vbCrLf _
                                                     & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                                     & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                                     & " (" & vbCrLf _
                                                     & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                                     & " WHERE OPB.PO_REF_NO IN(" _
                                                     & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & AGREEMENT_START_DATE _
                                                     & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                                     & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                                     & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                                     & ")OPB " & vbCrLf _
                                                     & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                                     & ") OPO WHERE BRAND_ID = '" & BrandID_2.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO += Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If
                        End If

                        If Not TotalPO = 0 Then
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_Q1)
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            ElseIf CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + CombAgreeBrandID.ToString() + "'")) Then
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & _
                                                "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            End If
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Quarterly 1 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                            " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                            " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                            AGREEMENT_START_DATE & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                       " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING

                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                            " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                            " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                            AGREEMENT_START_DATE & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                        " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' IN
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL COMB_AGREE_BRAND_ID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                        End If
                    Next
                End If

                '---------------------------------------------------------------------------------------------------

            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub GenerateQuarterly3Discount_1(ByVal AGREEMENT_NO As String, ByVal DISTRIBUTOR_ID As String)
            Try
                Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                             StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                             StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                             StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing

                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO OPTION(KEEP PLAN);"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Me.OpenConnection() : Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    StartDate = Me.SqlRe.GetDateTime(0) : EndDate = Me.SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                StartDateQ1 = StartDate : StartDateS1 = StartDate : EndDateQ4 = EndDate : EndDateS2 = EndDate
                EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
                StartDateQ2 = EndDateQ1.AddDays(1)
                EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
                StartDateQ3 = EndDateQ2.AddDays(1)
                EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
                StartDateQ4 = EndDateQ3.AddDays(1)
                EndDateS1 = StartDateS1.AddMonths(6).AddDays(-1)
                StartDateS2 = EndDateS1.AddDays(1)
                'Dim SUMDAYS As Object = Nothing
                'Me.CreateCommandSql("", "SELECT DATEDIFF(day, START_DATE, END_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'SUMDAYS = Me.ExecuteScalar()
                'If IsDBNull(SUMDAYS) Then
                '    Throw New System.Exception("Interval time for Agreement " & AGREEMENT_NO & " Hasn't been set yet")
                'End If
                'SUMDAYS = CInt(SUMDAYS)
                'Dim START_DATE_Q2 As String = "" 'Object = Me.ExecuteScalar("", "SELECT DATEADD(DAY," & CInt(SUMDAYS / 4) & ",START_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'If IsDBNull(START_DATE_Q2) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If

                'START_DATE_Q2 = CStr("'" & Month(Convert.ToDateTime(START_DATE_Q2)).ToString() & "/" & Day(Convert.ToDateTime(START_DATE_Q2)).ToString() & "/" & Year(Convert.ToDateTime(START_DATE_Q2)).ToString() & "'")

                Dim START_DATE_Q3 As String = "" 'Object = Me.ExecuteScalar("", "SELECT DATEADD(DAY,91 ,CAST((" & START_DATE_Q2 & ")AS DATETIME)) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")

                'If IsDBNull(START_DATE_Q3) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                'START_DATE_Q3 = CStr("'" & Month(Convert.ToDateTime(START_DATE_Q3)).ToString() & "/" & (Day(Convert.ToDateTime(START_DATE_Q3)) + 1).ToString() & "/" & Year(Convert.ToDateTime(START_DATE_Q3)).ToString() & "'")

                START_DATE_Q3 = "'" & StartDateQ3.Month.ToString() & "/" & StartDateQ3.Day.ToString() & "/" & StartDateQ3.Year.ToString() & "'"
                'Me.CreateCommandSql("", "SELECT DATEADD(DAY,90, CAST((" & START_DATE_Q3 & ")AS DATETIME)) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                Dim END_DATE As String = "" 'Object = Me.ExecuteScalar()
                'If (IsDBNull(END_DATE)) Or (IsNothing(END_DATE)) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                If EndDateQ3 >= NufarmBussinesRules.SharedClass.ServerDate() Then
                    Throw New System.Exception("Quarterly 3 hasn't closed yet" & vbCrLf & "System can not process the request")
                End If
                'END_DATE = CStr("'" & Month(Convert.ToDateTime(END_DATE)).ToString() & "/" & Day(Convert.ToDateTime(END_DATE)).ToString() & "/" & Year(Convert.ToDateTime(END_DATE)).ToString() & "'")
                END_DATE = "'" & EndDateQ3.Month.ToString() & "/" & EndDateQ3.Day.ToString() & "/" & EndDateQ3.Year.ToString() & "'"


                Dim BrandID_1 As Object = Nothing
                Dim AgreeBrandID As Object = Nothing
                Dim Target_Q3 As Decimal = 0
                Dim TotalPO As Decimal = 0
                Dim BrandID_2 As Object = Nothing
                Dim CombAgreeBrandID As Object = Nothing
                Dim Target_Q3_2 As Object = Nothing
                Dim DISC_QTY As Decimal = 0 'AGREE_DISC_PCT * TotalPO
                Dim TotalPOBrandPack_QTY As Decimal = 0
                Dim TotalAmount As Decimal = 0
                Dim Price As Object = Nothing
                Dim DISC_AMOUNT As Decimal = 0
                Dim BRANDPACK_ID As String = ""

                'UNTUK KASUS YANG TIDAK DI COMBINED-------------------------------------------------
                'SELECSI BRAND_ID DARI AGREEMENT YANG TIDAK DI COMBINED
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,BRAND_ID,TARGET_Q3 FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = '" & _
                AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NULL")
                Dim tblBrand As New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                If tblBrand.Rows.Count = 0 Then
                    'Throw New System.Exception("Brands for Agreement " & AGREEMENT_NO & " Hasn't been defined yet")
                Else
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        BrandID_1 = tblBrand.Rows(i)("BRAND_ID")
                        AgreeBrandID = tblBrand.Rows(i)("AGREE_BRAND_ID")
                        If tblBrand.Rows(i).IsNull("TARGET_Q3") Then
                            Throw New System.Exception("Some Brand for agreement " & AGREEMENT_NO & " has a null value for target Q1" & vbCrLf & _
                                                        "System can not process request due to its Condition")
                        End If
                        Target_Q3 = Convert.ToDecimal(tblBrand.Rows(i)("TARGET_Q3"))

                        Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                                & "( " & vbCrLf _
                                                & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                                & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                                & " (" & vbCrLf _
                                                & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                                & " WHERE OPB.PO_REF_NO IN(" _
                                                & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_Q3 _
                                                & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                                & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                                & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                                & ")OPB " & vbCrLf _
                                                & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                                & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                        Dim SUMPO As Object = Me.ExecuteScalar()
                        If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                            TotalPO = Convert.ToDecimal(SUMPO)
                        ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                        Else : TotalPO = 0
                        End If
                        If Not TotalPO = 0 Then
                            'GET PERCENTAGE
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_Q3)
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            End If
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Quarterly 3 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                            " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                            " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                            START_DATE_Q3 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                             " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q3"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q3", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q3"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q3", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If

                '--------UNTUK KASUS YANG DI COMBINED ------------------------------------------------------
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE" & _
                " WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NOT NULL")
                '------------------------------------------------------------------------------------
                tblBrand = New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                Dim HastCollCommBined As Hashtable = Nothing
                Dim NVCombUnik As System.Collections.Specialized.NameValueCollection = Nothing
                If Not tblBrand.Rows.Count = 0 Then
                    HastCollCommBined = New Hashtable()
                    HastCollCommBined.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        'masukan key dari agree_brand_id,valuenya denga comb_agree_brand_id
                        HastCollCommBined.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID"), tblBrand.Rows(i)("AGREE_BRAND_ID"))
                    Next

                    'Dim CollValue As New System.Collections.Specialized.NameValueCollection
                    'item data table sudah masuk semua sekarang buat NameValueCollection unik
                    NVCombUnik = New System.Collections.Specialized.NameValueCollection
                    NVCombUnik.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1 ' looping sampai habis
                        'IF ME.HastCollCommBined.ContainsValue(
                        If HastCollCommBined.ContainsValue(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) Then ' jika ditemukan 
                            NVCombUnik.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID").ToString(), tblBrand.Rows(i)("AGREE_BRAND_ID").ToString()) ' isi NameValueCollection unik
                            HastCollCommBined.Remove(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) 'remove item yang memilki key = comb_agree_brand_id
                        End If
                    Next
                    ' sekarang NameValueCollection UNIK HANYA DI ISI DENGAN KEY AGREE_BRAND_ID DAN VALUE COMB_BRAND_ID
                    For I As Integer = 0 To NVCombUnik.Count - 1
                        'AMBIL DULU valuenya
                        AgreeBrandID = NVCombUnik.Get(I).ToString()
                        CombAgreeBrandID = NVCombUnik.GetKey(I)
                        Dim T_Q1 As Object = Nothing
                        Dim T_Q2 As Object = Nothing
                        'AGREE_BRAND_ID DULU
                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_Q3 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_1 = Me.SqlRe("BRAND_ID")
                            T_Q1 = Me.SqlRe("TARGET_Q3")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_1)) And (Not IsDBNull(T_Q1)) Then
                            BrandID_1 = CStr(BrandID_1)
                            Target_Q3 = Convert.ToDecimal(T_Q1)

                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                                   & "( " & vbCrLf _
                                                   & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                                   & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                                   & " (" & vbCrLf _
                                                   & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                                   & " WHERE OPB.PO_REF_NO IN(" _
                                                   & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_Q3 _
                                                   & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                                   & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                                   & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                                   & ")OPB " & vbCrLf _
                                                   & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                                   & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO = Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If
                        End If
                        'COMB_AGREE_BRAND_ID

                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_Q3 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_2 = Me.SqlRe("BRAND_ID")
                            T_Q2 = Me.SqlRe("TARGET_Q3")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_2)) And (Not IsDBNull(T_Q2)) Then
                            BrandID_2 = CStr(BrandID_2)
                            Target_Q3 = Target_Q3 + Convert.ToDecimal(T_Q2)

                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                               & "( " & vbCrLf _
                                               & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                               & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                               & " (" & vbCrLf _
                                               & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                               & " WHERE OPB.PO_REF_NO IN(" _
                                               & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_Q3 _
                                               & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                               & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                               & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                               & ")OPB " & vbCrLf _
                                               & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                               & ") OPO WHERE BRAND_ID = '" & BrandID_2.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO += Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If
                        End If

                        If Not TotalPO = 0 Then
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_Q3)
                            'Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                            '"' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            ElseIf CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + CombAgreeBrandID.ToString() + "'")) Then
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & _
                                                "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            End If
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Quarterly 3 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                           " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                           " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                           START_DATE_Q3 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                         " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q3"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q3", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'comb_agree_brand_id
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                           " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                           " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                           START_DATE_Q3 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                        " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q3"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q3", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q3"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q3", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL COMB_AGREE_BRAND_ID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q3"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q3", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                        End If
                    Next
                End If

                '---------------------------------------------------------------------------------------------------

            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub GenerateQuarterly4Discount_1(ByVal AGREEMENT_NO As String, ByVal DISTRIBUTOR_ID As String)
            Try
                Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                             StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                             StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                             StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing

                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO OPTION(KEEP PLAN);"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Me.OpenConnection() : Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    StartDate = Me.SqlRe.GetDateTime(0) : EndDate = Me.SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                StartDateQ1 = StartDate : StartDateS1 = StartDate : EndDateQ4 = EndDate : EndDateS2 = EndDate
                EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
                StartDateQ2 = EndDateQ1.AddDays(1)
                EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
                StartDateQ3 = EndDateQ2.AddDays(1)
                EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
                StartDateQ4 = EndDateQ3.AddDays(1)
                EndDateS1 = StartDateS1.AddMonths(6).AddDays(-1)
                StartDateS2 = EndDateS1.AddDays(1)


                'Dim SUMDAYS As Object = Nothing
                'Me.CreateCommandSql("", "SELECT DATEDIFF(day, START_DATE, END_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'SUMDAYS = Me.ExecuteScalar()
                'If IsDBNull(SUMDAYS) Then
                '    Throw New System.Exception("Interval time for Agreement " & AGREEMENT_NO & " Hasn't been set yet")
                'End If
                'SUMDAYS = CInt(SUMDAYS)
                'Dim START_DATE_Q2 As Object = Me.ExecuteScalar("", "SELECT DATEADD(DAY," & CInt(SUMDAYS / 4) & ",START_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'If IsDBNull(START_DATE_Q2) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If

                'START_DATE_Q2 = CStr("'" & Month(Convert.ToDateTime(START_DATE_Q2)).ToString() & "/" & Day(Convert.ToDateTime(START_DATE_Q2)).ToString() & "/" & Year(Convert.ToDateTime(START_DATE_Q2)).ToString() & "'")

                'Dim START_DATE_Q3 As Object = Me.ExecuteScalar("", "SELECT DATEADD(DAY,91 ,CAST((" & START_DATE_Q2 & ")AS DATETIME)) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'START_DATE_Q3 = CStr("'" & Month(Convert.ToDateTime(START_DATE_Q3)).ToString() & "/" & (Day(Convert.ToDateTime(START_DATE_Q3)) + 1).ToString() & "/" & Year(Convert.ToDateTime(START_DATE_Q3)).ToString() & "'")
                'If IsDBNull(START_DATE_Q3) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If

                'Me.CreateCommandSql("", "SELECT DATEADD(DAY,91,CAST((" & START_DATE_Q3 & ")AS DATETIME)) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                Dim START_DATE_Q4 As String = "" 'Object = Me.ExecuteScalar()
                'If Convert.ToDateTime(END_DATE) > PO_REF_DATE Then
                '     ', "information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                'End If
                Dim END_DATE_Q4 As String = "" 'Object = Me.ExecuteScalar("", "SELECT END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'If (IsDBNull(END_DATE_Q4)) Or (IsNothing(END_DATE_Q4)) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                If EndDateQ4 >= NufarmBussinesRules.SharedClass.ServerDate() Then
                    Throw New System.Exception("Quarterly 4 hasn't closed yet" & vbCrLf & "System can not process the request")
                End If
                'END_DATE_Q4 = CStr("'" & Month(Convert.ToDateTime(END_DATE_Q4)).ToString() & "/" & Day(Convert.ToDateTime(END_DATE_Q4)).ToString() & "/" & Year(Convert.ToDateTime(END_DATE_Q4)).ToString() & "'")
                'START_DATE_Q4 = CStr("'" & Month(Convert.ToDateTime(START_DATE_Q4)).ToString() & "/" & (Day(Convert.ToDateTime(START_DATE_Q4)) + 1).ToString() & "/" & Year(Convert.ToDateTime(START_DATE_Q4)).ToString() & "'")

                START_DATE_Q4 = "'" & StartDateQ4.Month.ToString() & "/" & StartDateQ4.Day.ToString() & "/" & StartDateQ4.Year.ToString() & "'"
                END_DATE_Q4 = "'" & EndDateQ4.Month.ToString() & "/" & EndDateQ4.Day.ToString() & "/" & EndDateQ4.Year.ToString() & "'"

                Dim BrandID_1 As Object = Nothing
                Dim AgreeBrandID As Object = Nothing
                Dim Target_Q4 As Integer = 0
                Dim TotalPO As Decimal = 0
                Dim BrandID_2 As Object = Nothing
                Dim CombAgreeBrandID As Object = Nothing
                'Dim Target_Q4_2 As Object = Nothing
                Dim DISC_QTY As Decimal = 0 'AGREE_DISC_PCT * TotalPO
                Dim TotalPOBrandPack_QTY As Decimal = 0
                Dim TotalAmount As Decimal = 0
                Dim Price As Object = Nothing
                Dim DISC_AMOUNT As Decimal = 0
                Dim BRANDPACK_ID As String = ""

                'UNTUK KASUS YANG TIDAK DI COMBINED-------------------------------------------------
                'SELECSI BRAND_ID DARI AGREEMENT YANG TIDAK DI COMBINED
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,BRAND_ID,TARGET_Q4 FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = '" & _
                AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NULL")
                Dim tblBrand As New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                If tblBrand.Rows.Count = 0 Then
                    'Throw New System.Exception("Brands for Agreement " & AGREEMENT_NO & " Hasn't been defined yet")
                Else
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        BrandID_1 = tblBrand.Rows(i)("BRAND_ID")
                        AgreeBrandID = tblBrand.Rows(i)("AGREE_BRAND_ID")
                        If tblBrand.Rows(i).IsNull("TARGET_Q4") Then
                            Throw New System.Exception("Some Brand for agreement " & AGREEMENT_NO & " has a null value for target Q1" & vbCrLf & _
                                                        "System can not process request due to its Condition")
                        End If
                        Target_Q4 = Convert.ToDecimal(tblBrand.Rows(i)("TARGET_Q4"))

                        Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                               & "( " & vbCrLf _
                                               & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                               & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                               & " (" & vbCrLf _
                                               & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                               & " WHERE OPB.PO_REF_NO IN(" _
                                               & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_Q4 _
                                               & " AND PO_REF_DATE <= " & END_DATE_Q4 & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                               & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                               & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                               & ")OPB " & vbCrLf _
                                               & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                               & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                        Dim SUMPO As Object = Me.ExecuteScalar()
                        If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                            TotalPO = Convert.ToDecimal(SUMPO)
                        ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                        Else : TotalPO = 0
                        End If

                        If Not TotalPO = 0 Then
                            'GET PERCENTAGE
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_Q4)
                            'Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                            '"' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                             "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            End If
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Quarterly 4 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                           " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                           " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_Q4 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                           START_DATE_Q4 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                            " AND START_DATE <= " & END_DATE_Q4 & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q4"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q4", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q4"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE_Q4 & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q4", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If


                '--------UNTUK KASUS YANG DI COMBINED ------------------------------------------------------
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE" & _
                " WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NOT NULL")
                '------------------------------------------------------------------------------------
                tblBrand = New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                Dim HastCollCommBined As Hashtable = Nothing
                Dim NVCombUnik As System.Collections.Specialized.NameValueCollection = Nothing
                If Not tblBrand.Rows.Count = 0 Then
                    HastCollCommBined = New Hashtable()
                    HastCollCommBined.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        'masukan key dari agree_brand_id,valuenya denga comb_agree_brand_id
                        HastCollCommBined.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID"), tblBrand.Rows(i)("AGREE_BRAND_ID"))
                    Next
                    NVCombUnik = New System.Collections.Specialized.NameValueCollection
                    NVCombUnik.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1 ' looping sampai habis
                        'IF ME.HastCollCommBined.ContainsValue(
                        If HastCollCommBined.ContainsValue(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) Then ' jika ditemukan 
                            NVCombUnik.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID").ToString(), tblBrand.Rows(i)("AGREE_BRAND_ID").ToString()) ' isi NameValueCollection unik
                            HastCollCommBined.Remove(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) 'remove item yang memilki key = comb_agree_brand_id
                        End If
                    Next
                    ' sekarang NameValueCollection UNIK HANYA DI ISI DENGAN KEY AGREE_BRAND_ID DAN VALUE COMB_BRAND_ID
                    For I As Integer = 0 To NVCombUnik.Count - 1
                        'AMBIL DULU valuenya
                        AgreeBrandID = NVCombUnik.Get(I).ToString()
                        CombAgreeBrandID = NVCombUnik.GetKey(I)
                        Dim T_Q1 As Object = Nothing
                        Dim T_Q2 As Object = Nothing
                        'AGREE_BRAND_ID DULU
                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_Q4 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_1 = Me.SqlRe("BRAND_ID")
                            T_Q1 = Me.SqlRe("TARGET_Q4")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_1)) And (Not IsDBNull(T_Q1)) Then
                            BrandID_1 = CStr(BrandID_1)
                            Target_Q4 = Convert.ToDecimal(T_Q1)

                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                                  & "( " & vbCrLf _
                                                  & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                                  & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                                  & " (" & vbCrLf _
                                                  & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                                  & " WHERE OPB.PO_REF_NO IN(" _
                                                  & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_Q4 _
                                                  & " AND PO_REF_DATE <= " & END_DATE_Q4 & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                                  & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                                  & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                                  & ")OPB " & vbCrLf _
                                                  & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                                  & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO = Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If
                        End If
                        'COMB_AGREE_BRAND_ID

                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_Q4 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_2 = Me.SqlRe("BRAND_ID")
                            T_Q2 = Me.SqlRe("TARGET_Q4")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_2)) And (Not IsDBNull(T_Q2)) Then
                            BrandID_2 = CStr(BrandID_2)
                            Target_Q4 = Target_Q4 + Convert.ToDecimal(T_Q2)

                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                              & "( " & vbCrLf _
                                              & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                              & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                              & " (" & vbCrLf _
                                              & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                              & " WHERE OPB.PO_REF_NO IN(" _
                                              & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_Q4 _
                                              & " AND PO_REF_DATE <= " & END_DATE_Q4 & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                              & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                              & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                              & ")OPB " & vbCrLf _
                                              & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                              & ") OPO WHERE BRAND_ID = '" & BrandID_2.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO += Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If
                        End If

                        If Not TotalPO = 0 Then
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_Q4)
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            ElseIf CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + CombAgreeBrandID.ToString() + "'")) Then
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & _
                                                "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                  "' AND QSY_DISC_FLAG = 'Q' ORDER BY UP_TO_PCT DESC")
                            End If
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Quarterly 4 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                       " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                       " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_Q4 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                      START_DATE_Q4 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                        " AND START_DATE <= " & END_DATE_Q4 & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q4"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q4", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Int, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'comb_agree_brand_id
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                     " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                     " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_Q4 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                    START_DATE_Q4 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                         " AND START_DATE <= " & END_DATE_Q4 & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q4"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q4", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Int, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q4"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE_Q4 & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q4", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL COMB_AGREE_BRAND_ID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Q4"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE_Q4 & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Q4", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                        End If
                    Next
                End If

                '---------------------------------------------------------------------------------------------------

            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub GenerateSemesterly1Discount_1(ByVal AGREEMENT_NO As String, ByVal DISTRIBUTOR_ID As String)
            Try
                Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                            StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                            StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                            StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing

                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO OPTION(KEEP PLAN);"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Me.OpenConnection() : Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    StartDate = Me.SqlRe.GetDateTime(0) : EndDate = Me.SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                StartDateQ1 = StartDate : StartDateS1 = StartDate : EndDateQ4 = EndDate : EndDateS2 = EndDate
                EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
                StartDateQ2 = EndDateQ1.AddDays(1)
                EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
                StartDateQ3 = EndDateQ2.AddDays(1)
                EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
                StartDateQ4 = EndDateQ3.AddDays(1)
                EndDateS1 = StartDateS1.AddMonths(6).AddDays(-1)
                StartDateS2 = EndDateS1.AddDays(1)

                'Dim SUMDAYS As Object = Nothing
                'Me.CreateCommandSql("", "SELECT DATEDIFF(day, START_DATE, END_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'SUMDAYS = Me.ExecuteScalar()
                'If IsDBNull(SUMDAYS) Then
                '    Throw New System.Exception("Interval time for Agreement " & AGREEMENT_NO & " Hasn't been set yet")
                'End If
                'SUMDAYS = CInt(SUMDAYS)
                'Dim END_DATE As Integer = (SUMDAYS / 2) - 1
                'Me.CreateCommandSql("", "SELECT DATEADD(DAY," & END_DATE & ",START_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                Dim END_DATE_S1 As String = "" 'As Object = Me.ExecuteScalar()

                'If IsDBNull(END_DATE_S1) Or IsNothing(END_DATE_S1) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                Dim START_DATE_S1 As String = "" 'Object = Me.ExecuteScalar("", "SELECT START_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'If (IsDBNull(START_DATE_S1)) Or (IsNothing(START_DATE_S1)) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                'If Convert.ToDateTime(END_DATE) > PO_REF_DATE Then
                '     ', "information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                'End If
                If Convert.ToDateTime(EndDateS1) >= NufarmBussinesRules.SharedClass.ServerDate() Then
                    Throw New System.Exception("Semesterly 1 hasn't closed yet" & vbCrLf & "System can not process the request")
                End If
                'END_DATE_S1 = CStr("'" & Month(Convert.ToDateTime(END_DATE_S1)).ToString() & "/" & Day(Convert.ToDateTime(END_DATE_S1)).ToString() & "/" & Year(Convert.ToDateTime(END_DATE_S1)).ToString() & "'")
                'START_DATE_S1 = CStr("'" & Month(Convert.ToDateTime(START_DATE_S1)).ToString() & "/" & Day(Convert.ToDateTime(START_DATE_S1)).ToString() & "/" & Year(Convert.ToDateTime(START_DATE_S1)).ToString() & "'")

                END_DATE_S1 = "'" & EndDateS1.Month.ToString() & "/" & EndDateS1.Day.ToString() & "/" & EndDateS1.Year.ToString() & "'"
                START_DATE_S1 = "'" & StartDateS1.Month.ToString() & "/" & StartDateS1.Day.ToString() & "/" & StartDateS1.Year.ToString() & "'"

                Dim BrandID_1 As Object = Nothing
                Dim AgreeBrandID As Object = Nothing
                Dim Target_S1 As Integer = 0
                Dim TotalPO As Decimal = 0
                Dim BrandID_2 As Object = Nothing
                Dim CombAgreeBrandID As Object = Nothing
                'Dim Target_Q4_2 As Object = Nothing
                Dim DISC_QTY As Decimal = 0 'AGREE_DISC_PCT * TotalPO
                Dim TotalPOBrandPack_QTY As Decimal = 0
                Dim TotalAmount As Decimal = 0
                Dim Price As Object = Nothing
                Dim DISC_AMOUNT As Decimal = 0
                Dim BRANDPACK_ID As String = ""
                'Dim PRICE_PERQTY As Object = Nothing
                'UNTUK KASUS YANG TIDAK DI COMBINED-------------------------------------------------
                'SELECSI BRAND_ID DARI AGREEMENT YANG TIDAK DI COMBINED
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,BRAND_ID,TARGET_S1 FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = '" & _
                AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NULL")
                Dim tblBrand As New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                If tblBrand.Rows.Count = 0 Then
                    'Throw New System.Exception("Brands for Agreement " & AGREEMENT_NO & " Hasn't been defined yet")
                Else
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        BrandID_1 = tblBrand.Rows(i)("BRAND_ID")
                        AgreeBrandID = tblBrand.Rows(i)("AGREE_BRAND_ID")
                        If tblBrand.Rows(i).IsNull("TARGET_S1") Then
                            Throw New System.Exception("Some Brand for agreement " & AGREEMENT_NO & " has a null value for target Q1" & vbCrLf & _
                                                        "System can not process request due to its Condition")
                        End If
                        Target_S1 = Convert.ToDecimal(tblBrand.Rows(i)("TARGET_S1"))

                        Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                              & "( " & vbCrLf _
                                              & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                              & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                              & " (" & vbCrLf _
                                              & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                              & " WHERE OPB.PO_REF_NO IN(" _
                                              & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_S1 _
                                              & " AND PO_REF_DATE <= " & END_DATE_S1 & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                              & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                              & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                              & ")OPB " & vbCrLf _
                                              & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                              & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                        Dim SUMPO As Object = Me.ExecuteScalar()
                        If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                            TotalPO = Convert.ToDecimal(SUMPO)
                        ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                        Else : TotalPO = 0
                        End If
                        If Not TotalPO = 0 Then
                            'GET PERCENTAGE
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_S1)
                            ''check apakah AgreeBrandID ada di table agree_prog_disc
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                  "' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            End If
                            'Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                            '"' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Semesterly 1 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                     " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                     " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S1 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                   START_DATE_S1 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                            " AND START_DATE <= " & END_DATE_S1 & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If

                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE_S1 & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If

                '--------UNTUK KASUS YANG DI COMBINED ------------------------------------------------------
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE" & _
                " WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NOT NULL")
                '------------------------------------------------------------------------------------
                tblBrand = New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                Dim HastCollCommBined As Hashtable = Nothing
                Dim NVCombUnik As System.Collections.Specialized.NameValueCollection = Nothing
                If Not tblBrand.Rows.Count = 0 Then
                    HastCollCommBined = New Hashtable()
                    HastCollCommBined.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        'masukan key dari agree_brand_id,valuenya denga comb_agree_brand_id
                        HastCollCommBined.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID"), tblBrand.Rows(i)("AGREE_BRAND_ID"))
                    Next

                    'Dim CollValue As New System.Collections.Specialized.NameValueCollection
                    'item data table sudah masuk semua sekarang buat NameValueCollection unik
                    NVCombUnik = New System.Collections.Specialized.NameValueCollection
                    NVCombUnik.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1 ' looping sampai habis
                        'IF ME.HastCollCommBined.ContainsValue(
                        If HastCollCommBined.ContainsValue(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) Then ' jika ditemukan 
                            NVCombUnik.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID").ToString(), tblBrand.Rows(i)("AGREE_BRAND_ID").ToString()) ' isi NameValueCollection unik
                            HastCollCommBined.Remove(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) 'remove item yang memilki key = comb_agree_brand_id
                        End If
                    Next
                    ' sekarang NameValueCollection UNIK HANYA DI ISI DENGAN KEY AGREE_BRAND_ID DAN VALUE COMB_BRAND_ID
                    For I As Integer = 0 To NVCombUnik.Count - 1
                        'AMBIL DULU valuenya
                        AgreeBrandID = NVCombUnik.Get(I).ToString()
                        CombAgreeBrandID = NVCombUnik.GetKey(I)
                        Dim T_S1 As Object = Nothing
                        Dim T_S2 As Object = Nothing
                        'AGREE_BRAND_ID DULU
                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_S1 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_1 = Me.SqlRe("BRAND_ID")
                            T_S1 = Me.SqlRe("TARGET_S1")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_1)) And (Not IsDBNull(T_S1)) Then
                            BrandID_1 = CStr(BrandID_1)
                            Target_S1 = Convert.ToDecimal(T_S1)
                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                              & "( " & vbCrLf _
                                              & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                              & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                              & " (" & vbCrLf _
                                              & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                              & " WHERE OPB.PO_REF_NO IN(" _
                                              & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_S1 _
                                              & " AND PO_REF_DATE <= " & END_DATE_S1 & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                              & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                              & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                              & ")OPB " & vbCrLf _
                                              & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                              & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO = Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If

                        End If
                        'COMB_AGREE_BRAND_ID

                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_S1 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_2 = Me.SqlRe("BRAND_ID")
                            T_S2 = Me.SqlRe("TARGET_S1")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_2)) And (Not IsDBNull(T_S2)) Then
                            BrandID_2 = CStr(BrandID_2)
                            Target_S1 = Target_S1 + Convert.ToDecimal(T_S2)
                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                              & "( " & vbCrLf _
                                              & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                              & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                              & " (" & vbCrLf _
                                              & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                              & " WHERE OPB.PO_REF_NO IN(" _
                                              & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_S1 _
                                              & " AND PO_REF_DATE <= " & END_DATE_S1 & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                              & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                              & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                              & ")OPB " & vbCrLf _
                                              & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                              & ") OPO WHERE BRAND_ID = '" & BrandID_2.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO += Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If
                        End If

                        If Not TotalPO = 0 Then
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_S1)
                            If Convert.ToInt32(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & _
                                AgreeBrandID.ToString() + "'")) > 0 Then
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                 "' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            ElseIf CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + CombAgreeBrandID.ToString() + "'")) Then
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & _
                                                "' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            Else
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                                   "' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            End If

                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Semesterly 1 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()

                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                 " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                 " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S1 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                START_DATE_S1 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    'Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER,AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT" & _
                                    '          " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO " & _
                                    '          " AND AGREE_AGREEMENT.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO" & _
                                    '                " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID" & _
                                    '          " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S1 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                    '          START_DATE_S1 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                         " AND START_DATE <= " & END_DATE_S1 & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If
                                    'Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                    '" AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    'Price = Me.ExecuteScalar()
                                    'If Not IsDBNull(Price) Then
                                    '    DISC_AMOUNT = Convert.ToDecimal(Convert.ToDecimal(Price) * DISC_QTY)
                                    'Else : DISC_AMOUNT = 0
                                    'End If
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'comb_agree_brand_id
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                 " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                 " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S1 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                START_DATE_S1 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    'Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER,AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT" & _
                                    '          " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO " & _
                                    '          " AND AGREE_AGREEMENT.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO" & _
                                    '                " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID" & _
                                    '          " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S1 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                    '          START_DATE_S1 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                        " AND START_DATE <= " & END_DATE_S1 & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If
                                    'Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                    '" AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    'Price = Me.ExecuteScalar()
                                    'If Not IsDBNull(Price) Then
                                    '    DISC_AMOUNT = Convert.ToDecimal(Convert.ToDecimal(Price) * DISC_QTY)
                                    'Else : DISC_AMOUNT = 0
                                    'End If
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE_S1 & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL COMB_AGREE_BRAND_ID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S1"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                 " AND START_DATE <= " & END_DATE_S1 & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S1", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                        End If
                    Next
                End If

                '---------------------------------------------------------------------------------------------------

            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub GenerateSemesterly2Discount_1(ByVal AGREEMENT_NO As String, ByVal DISTRIBUTOR_ID As String)
            Try
                Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                            StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                            StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                            StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing

                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO OPTION(KEEP PLAN);"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Me.OpenConnection() : Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    StartDate = Me.SqlRe.GetDateTime(0) : EndDate = Me.SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                StartDateQ1 = StartDate : StartDateS1 = StartDate : EndDateQ4 = EndDate : EndDateS2 = EndDate
                EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
                StartDateQ2 = EndDateQ1.AddDays(1)
                EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
                StartDateQ3 = EndDateQ2.AddDays(1)
                EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
                StartDateQ4 = EndDateQ3.AddDays(1)
                EndDateS1 = StartDateS1.AddMonths(6).AddDays(-1)
                StartDateS2 = EndDateS1.AddDays(1)

                'Dim SUMDAYS As Object = Nothing
                'Me.CreateCommandSql("", "SELECT DATEDIFF(day, START_DATE, END_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'SUMDAYS = Me.ExecuteScalar()
                'If IsDBNull(SUMDAYS) Then
                '    Throw New System.Exception("Interval time for Agreement " & AGREEMENT_NO & " Hasn't been set yet")
                'End If
                'SUMDAYS = CInt(SUMDAYS)
                'Dim END_DATE As Integer = SUMDAYS / 2
                'Me.CreateCommandSql("", "SELECT DATEADD(DAY," & END_DATE & ",START_DATE) FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                Dim START_DATE_S2 As String = "" 'Object = Me.ExecuteScalar()
                'If IsDBNull(START_DATE_S2) Or IsNothing(START_DATE_S2) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                Dim END_DATE_S2 As String = "" 'Object = Me.ExecuteScalar("", "SELECT END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")

                'If (IsDBNull(END_DATE_S2)) Or (IsNothing(END_DATE_S2)) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                'If Convert.ToDateTime(END_DATE) > PO_REF_DATE Then
                '     ', "information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                'End If
                If EndDateS2 >= NufarmBussinesRules.SharedClass.ServerDate() Then
                    Throw New System.Exception("Semesterly 2 hasn't closed yet" & vbCrLf & "System can not process the request")
                End If
                'END_DATE_S2 = CStr("'" & Month(Convert.ToDateTime(END_DATE_S2)).ToString() & "/" & Day(Convert.ToDateTime(END_DATE_S2)).ToString() & "/" & Year(Convert.ToDateTime(END_DATE_S2)).ToString() & "'")
                'START_DATE_S2 = CStr("'" & Month(Convert.ToDateTime(START_DATE_S2)).ToString() & "/" & (Day(Convert.ToDateTime(START_DATE_S2)) + 1).ToString() & "/" & Year(Convert.ToDateTime(START_DATE_S2)).ToString() & "'")

                END_DATE_S2 = "'" & EndDateS2.Month.ToString() & "/" & EndDateS2.Day.ToString() & "/" & EndDateS2.Year.ToString() & "'"
                START_DATE_S2 = "'" & StartDateS2.Month.ToString() & "/" & StartDateS2.Day.ToString() & "/" & StartDateS2.Year.ToString() & "'"

                Dim BrandID_1 As Object = Nothing
                Dim AgreeBrandID As Object = Nothing
                Dim Target_S2 As Integer = 0
                Dim TotalPO As Decimal = 0
                Dim BrandID_2 As Object = Nothing
                Dim CombAgreeBrandID As Object = Nothing
                'Dim Target_Q4_2 As Object = Nothing
                Dim DISC_QTY As Decimal = 0 'AGREE_DISC_PCT * TotalPO
                Dim TotalPOBrandPack_QTY As Decimal = 0
                Dim TotalAmount As Decimal = 0
                Dim Price As Object = Nothing
                Dim DISC_AMOUNT As Decimal = 0
                Dim BRANDPACK_ID As String = ""

                'UNTUK KASUS YANG TIDAK DI COMBINED-------------------------------------------------
                'SELECSI BRAND_ID DARI AGREEMENT YANG TIDAK DI COMBINED
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,BRAND_ID,TARGET_S2 FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = '" & _
                AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NULL")
                Dim tblBrand As New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                If tblBrand.Rows.Count = 0 Then
                    'Throw New System.Exception("Brands for Agreement " & AGREEMENT_NO & " Hasn't been defined yet")
                Else
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        BrandID_1 = tblBrand.Rows(i)("BRAND_ID")
                        AgreeBrandID = tblBrand.Rows(i)("AGREE_BRAND_ID")
                        If tblBrand.Rows(i).IsNull("TARGET_S2") Then
                            Throw New System.Exception("Some Brand for agreement " & AGREEMENT_NO & " has a null value for target Q1" & vbCrLf & _
                                                        "System can not process request due to its Condition")
                        End If
                        Target_S2 = CInt(tblBrand.Rows(i)("TARGET_S2"))

                        Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                                 & "( " & vbCrLf _
                                                 & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                                 & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                                 & " (" & vbCrLf _
                                                 & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                                 & " WHERE OPB.PO_REF_NO IN(" _
                                                 & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_S2 _
                                                 & " AND PO_REF_DATE <= " & END_DATE_S2 & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                                 & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                                 & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                                 & ")OPB " & vbCrLf _
                                                 & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                                 & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                        Dim SUMPO As Object = Me.ExecuteScalar()
                        If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                            TotalPO = Convert.ToDecimal(SUMPO)
                        ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                        Else : TotalPO = 0
                        End If
                        If Not TotalPO = 0 Then
                            'GET PERCENTAGE
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_S2)
                            'Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                            '"' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                  "' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            End If
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Semesterly 2 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                             " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                             " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S2 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                            START_DATE_S2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    'Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER,AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT" & _
                                    '          " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO " & _
                                    '          " AND AGREE_AGREEMENT.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO" & _
                                    '                " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID" & _
                                    '          " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S2 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                    '         START_DATE_S2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                             " AND START_DATE <= " & END_DATE_S2 & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If
                                    'Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                    '" AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    'Price = Me.ExecuteScalar()
                                    'If Not IsDBNull(Price) Then
                                    '    DISC_AMOUNT = Convert.ToDecimal(Convert.ToDecimal(Price) * DISC_QTY)
                                    'Else : DISC_AMOUNT = 0
                                    'End If
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S2", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE_S2 & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S2", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If

                '--------UNTUK KASUS YANG DI COMBINED ------------------------------------------------------
                Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID,COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE" & _
                " WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "' AND COMB_AGREE_BRAND_ID IS NOT NULL")
                '------------------------------------------------------------------------------------
                tblBrand = New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                Dim HastCollCommBined As Hashtable = Nothing
                Dim NVCombUnik As System.Collections.Specialized.NameValueCollection = Nothing
                If Not tblBrand.Rows.Count = 0 Then
                    HastCollCommBined = New Hashtable()
                    HastCollCommBined.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        'masukan key dari agree_brand_id,valuenya denga comb_agree_brand_id
                        HastCollCommBined.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID"), tblBrand.Rows(i)("AGREE_BRAND_ID"))
                    Next

                    'Dim CollValue As New System.Collections.Specialized.NameValueCollection
                    'item data table sudah masuk semua sekarang buat NameValueCollection unik
                    NVCombUnik = New System.Collections.Specialized.NameValueCollection
                    NVCombUnik.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1 ' looping sampai habis
                        'IF ME.HastCollCommBined.ContainsValue(
                        If HastCollCommBined.ContainsValue(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) Then ' jika ditemukan 
                            NVCombUnik.Add(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID").ToString(), tblBrand.Rows(i)("AGREE_BRAND_ID").ToString()) ' isi NameValueCollection unik
                            HastCollCommBined.Remove(tblBrand.Rows(i)("COMB_AGREE_BRAND_ID")) 'remove item yang memilki key = comb_agree_brand_id
                        End If
                    Next
                    ' sekarang NameValueCollection UNIK HANYA DI ISI DENGAN KEY AGREE_BRAND_ID DAN VALUE COMB_BRAND_ID
                    For I As Integer = 0 To NVCombUnik.Count - 1
                        'AMBIL DULU valuenya
                        AgreeBrandID = NVCombUnik.Get(I).ToString()
                        CombAgreeBrandID = NVCombUnik.GetKey(I)
                        Dim T_S1 As Object = Nothing
                        Dim T_S2 As Object = Nothing
                        'AGREE_BRAND_ID DULU
                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_S2 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_1 = Me.SqlRe("BRAND_ID")
                            T_S1 = Me.SqlRe("TARGET_S2")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_1)) And (Not IsDBNull(T_S1)) Then
                            BrandID_1 = CStr(BrandID_1)
                            Target_S2 = CInt(T_S1)
                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                                 & "( " & vbCrLf _
                                                 & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                                 & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                                 & " (" & vbCrLf _
                                                 & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                                 & " WHERE OPB.PO_REF_NO IN(" _
                                                 & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_S2 _
                                                 & " AND PO_REF_DATE <= " & END_DATE_S2 & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                                 & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                                 & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                                 & ")OPB " & vbCrLf _
                                                 & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                                 & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO = Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If

                        End If
                        'COMB_AGREE_BRAND_ID

                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_S2 FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_2 = Me.SqlRe("BRAND_ID")
                            T_S2 = Me.SqlRe("TARGET_S2")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_2)) And (Not IsDBNull(T_S2)) Then
                            BrandID_2 = CStr(BrandID_2)
                            Target_S2 = Target_S2 + CInt(T_S2)

                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                                & "( " & vbCrLf _
                                                & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                                & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                                & " (" & vbCrLf _
                                                & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                                & " WHERE OPB.PO_REF_NO IN(" _
                                                & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE_S2 _
                                                & " AND PO_REF_DATE <= " & END_DATE_S2 & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                                & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                                & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                                & ")OPB " & vbCrLf _
                                                & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                                & ") OPO WHERE BRAND_ID = '" & BrandID_2.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO += Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If
                        End If

                        If Not TotalPO = 0 Then
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_S2)
                            'Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                            '"' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            ElseIf CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + CombAgreeBrandID.ToString() + "'")) Then
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & _
                                                "' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                  "' AND QSY_DISC_FLAG = 'S' ORDER BY UP_TO_PCT DESC")
                            End If
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Semesterly 2 " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                           " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                           " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S2 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                           START_DATE_S2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    'Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER,AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT" & _
                                    '          " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO " & _
                                    '          " AND AGREE_AGREEMENT.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO" & _
                                    '                " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID" & _
                                    '          " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S2 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                    '          START_DATE_S2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                        " AND START_DATE <= " & END_DATE_S2 & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If
                                    'Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                    '" AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    'Price = Me.ExecuteScalar()
                                    'If Not IsDBNull(Price) Then
                                    '    DISC_AMOUNT = Convert.ToDecimal(Convert.ToDecimal(Price) * DISC_QTY)
                                    'Else : DISC_AMOUNT = 0
                                    'End If
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S2", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'comb_agree_brand_id
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                           " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                           " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S2 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                          START_DATE_S2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    'Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER,AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT" & _
                                    '          " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO " & _
                                    '          " AND AGREE_AGREEMENT.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO" & _
                                    '                " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID" & _
                                    '          " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S2 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                    '          START_DATE_S2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                        " AND START_DATE <= " & END_DATE_S2 & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If
                                    'Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                    '" AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    'Price = Me.ExecuteScalar()
                                    'If Not IsDBNull(Price) Then
                                    '    DISC_AMOUNT = Convert.ToDecimal(Convert.ToDecimal(Price) * DISC_QTY)
                                    'Else : DISC_AMOUNT = 0
                                    'End If
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S2", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE_S2 & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S2", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL COMB_AGREE_BRAND_ID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "S2"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE_S2 & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "S2", 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                        End If
                    Next
                End If

                '---------------------------------------------------------------------------------------------------

            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub GenerateYearlyDiscount_1(ByVal AGREEMENT_NO As String, ByVal DISTRIBUTOR_ID As String)
            Try
                Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing

                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO OPTION(KEEP PLAN);"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Me.OpenConnection() : Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    StartDate = Me.SqlRe.GetDateTime(0) : EndDate = Me.SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()

                Dim END_DATE As String = "" 'Object = Nothing
                Dim START_DATE As String = "" 'Object = Nothing
                'Me.CreateCommandSql("", "SELECT START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                'Me.ExecuteReader()
                'While Me.SqlRe.Read()
                '    END_DATE = Me.SqlRe("END_DATE")
                '    START_DATE = Me.SqlRe("START_DATE")
                'End While
                'Me.SqlRe.Close()
                'Me.CloseConnection()
                'If IsDBNull(START_DATE) Or IsNothing(START_DATE) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                'If (IsDBNull(END_DATE)) Or (IsNothing(END_DATE)) Then
                '    Throw New System.Exception("START_DATE FROM AGREEMENT_NO " & AGREEMENT_NO & " IS NULL")
                'End If
                'If Convert.ToDateTime(END_DATE) > PO_REF_DATE Then
                '     ', "information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                'End If
                If EndDate >= NufarmBussinesRules.SharedClass.ServerDate() Then
                    Throw New System.Exception("Yearly discount hasn't closed yet" & vbCrLf & "System can not process the request")
                End If
                END_DATE = "'" & EndDate.Month.ToString() & "/" & EndDate.Day.ToString() & "/" & EndDate.Year.ToString() & "'"
                START_DATE = "'" & StartDate.Month.ToString() & "/" & StartDate.Day.ToString() & "/" & StartDate.Year.ToString() & "'"

                Dim BrandID_1 As Object = Nothing
                Dim AgreeBrandID As Object = Nothing
                Dim Target_Year As Int64 = 0
                Dim TotalPO As Decimal = 0
                Dim BrandID_2 As Object = Nothing
                Dim CombAgreeBrandID As Object = Nothing
                'Dim Target_Q4_2 As Object = Nothing
                Dim DISC_QTY As Decimal = 0 'AGREE_DISC_PCT * TotalPO
                Dim TotalPOBrandPack_QTY As Decimal = 0
                Dim TotalAmount As Decimal = 0
                Dim Price As Object = Nothing
                Dim DISC_AMOUNT As Decimal = 0
                Dim BRANDPACK_ID As String = ""

                'UNTUK KASUS YANG TIDAK DI COMBINED-------------------------------------------------
                'SELECSI BRAND_ID DARI AGREEMENT YANG TIDAK DI COMBINED
                Me.CreateCommandSql("", "SELECT [ID],BRAND_ID,TARGET_YEAR FROM VIEW_AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = '" & _
                AGREEMENT_NO & "' AND COMBINED_BRAND IS NULL")
                Dim tblBrand As New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                If tblBrand.Rows.Count = 0 Then
                    'Throw New System.Exception("Brands for Agreement " & AGREEMENT_NO & " Hasn't been defined yet")
                Else
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        BrandID_1 = tblBrand.Rows(i)("BRAND_ID")
                        AgreeBrandID = tblBrand.Rows(i)("ID")
                        If tblBrand.Rows(i).IsNull("TARGET_YEAR") Then
                            Throw New System.Exception("Some Brand for agreement " & AGREEMENT_NO & " has a null value for target Q/S Flag" & vbCrLf & _
                                                        "System can not process request due to its Condition")
                        End If
                        Target_Year = CInt(tblBrand.Rows(i)("TARGET_YEAR"))
                        'Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                        '                         & "( " & vbCrLf _
                        '                         & "SELECT BB.BRAND_ID,SUM(OPB.TOTAL_PO) AS TOTAL_PO " & vbCrLf _
                        '                         & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                        '                         & " (" & vbCrLf _
                        '                         & "SELECT OPB.BRANDPACK_ID,SUM(OPB.PO_ORIGINAL_QTY) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                        '                         & " WHERE OPB.PO_REF_NO IN(" _
                        '                         & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE _
                        '                         & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                        '                         & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                        '                         & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                        '                         & ")OPB " & vbCrLf _
                        '                         & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                        '                         & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")

                        'Dim SUMPO As Object = Me.ExecuteScalar()
                        'If Not IsDBNull(SUMPO) Then
                        '    TotalPO = Convert.ToDecimal(SUMPO)
                        'Else : TotalPO = 0
                        'End If
                        Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                                & "( " & vbCrLf _
                                                & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                                & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                                & " (" & vbCrLf _
                                                & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                                & " WHERE OPB.PO_REF_NO IN(" _
                                                & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE _
                                                & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                                & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                                & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                                & ")OPB " & vbCrLf _
                                                & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                                & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                        Dim SUMPO As Object = Me.ExecuteScalar()
                        If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                            TotalPO = Convert.ToDecimal(SUMPO)
                        ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                        Else : TotalPO = 0
                        End If
                        If Not TotalPO = 0 Then
                            'GET PERCENTAGE
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_Year)
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'Y' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                  "' AND QSY_DISC_FLAG = 'Y' ORDER BY UP_TO_PCT DESC")
                            End If
                            'Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                            '"' AND QSY_DISC_FLAG = 'Y' ORDER BY UP_TO_PCT DESC")
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Progressive Discount Agreement for type Yearly " & vbCrLf & "Has not been set yet")
                            End If
                            'If AgreeBrandID.ToString() = "0011/NI/I/2006.0901006" Then
                            '    Stop
                            'End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                             " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                             " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                            START_DATE & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    'Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER,AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT" & _
                                    '          " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO " & _
                                    '          " AND AGREE_AGREEMENT.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO" & _
                                    '                " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID" & _
                                    '          " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S2 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                    '         START_DATE_S2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                             " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If


                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Y"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Y".Trim(), 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Y"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Y".Trim(), 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If

                '--------UNTUK KASUS YANG DI COMBINED ------------------------------------------------------
                Me.CreateCommandSql("", "SELECT [ID],COMBINED_BRAND FROM VIEW_AGREE_BRAND_INCLUDE" & _
                " WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "' AND COMBINED_BRAND IS NOT NULL")
                '------------------------------------------------------------------------------------
                tblBrand = New DataTable("BRAND_INCLUDE")
                tblBrand.Clear()
                Me.FillDataTable(tblBrand)
                Dim HastCollCommBined As Hashtable = Nothing
                Dim NVCombUnik As System.Collections.Specialized.NameValueCollection = Nothing
                If Not tblBrand.Rows.Count = 0 Then
                    HastCollCommBined = New Hashtable()
                    HastCollCommBined.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1
                        'masukan key dari agree_brand_id,valuenya denga comb_agree_brand_id
                        HastCollCommBined.Add(tblBrand.Rows(i)("COMBINED_BRAND"), tblBrand.Rows(i)("ID"))
                    Next

                    'Dim CollValue As New System.Collections.Specialized.NameValueCollection
                    'item data table sudah masuk semua sekarang buat NameValueCollection unik
                    NVCombUnik = New System.Collections.Specialized.NameValueCollection
                    NVCombUnik.Clear()
                    For i As Integer = 0 To tblBrand.Rows.Count - 1 ' looping sampai habis
                        'IF ME.HastCollCommBined.ContainsValue(
                        If HastCollCommBined.ContainsValue(tblBrand.Rows(i)("COMBINED_BRAND")) Then ' jika ditemukan 
                            NVCombUnik.Add(tblBrand.Rows(i)("COMBINED_BRAND").ToString(), tblBrand.Rows(i)("ID").ToString()) ' isi NameValueCollection unik
                            HastCollCommBined.Remove(tblBrand.Rows(i)("COMBINED_BRAND")) 'remove item yang memilki key = comb_agree_brand_id
                        End If
                    Next
                    ' sekarang NameValueCollection UNIK HANYA DI ISI DENGAN KEY AGREE_BRAND_ID DAN VALUE COMB_BRAND_ID
                    For I As Integer = 0 To NVCombUnik.Count - 1
                        'AMBIL DULU valuenya
                        AgreeBrandID = NVCombUnik.Get(I).ToString()
                        CombAgreeBrandID = NVCombUnik.GetKey(I)
                        Dim T_Y1 As Object = Nothing
                        Dim T_Y2 As Object = Nothing
                        'AGREE_BRAND_ID DULU
                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_YEAR FROM VIEW_AGREE_BRAND_INCLUDE WHERE [ID] = '" & AgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_1 = Me.SqlRe("BRAND_ID")
                            T_Y1 = Me.SqlRe("TARGET_YEAR")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_1)) And (Not IsDBNull(T_Y1)) Then
                            BrandID_1 = CStr(BrandID_1)
                            Target_Year = CInt(T_Y1)
                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                               & "( " & vbCrLf _
                                               & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                               & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                               & " (" & vbCrLf _
                                               & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                               & " WHERE OPB.PO_REF_NO IN(" _
                                               & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE _
                                               & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                               & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                               & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                               & ")OPB " & vbCrLf _
                                               & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                               & ") OPO WHERE BRAND_ID = '" & BrandID_1.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO = Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If

                        End If
                        'COMB_AGREE_BRAND_ID

                        Me.CreateCommandSql("", "SELECT BRAND_ID,TARGET_YEAR FROM VIEW_AGREE_BRAND_INCLUDE WHERE [ID] = '" & CombAgreeBrandID & "'")
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            BrandID_2 = Me.SqlRe("BRAND_ID")
                            T_Y2 = Me.SqlRe("TARGET_YEAR")
                        End While
                        Me.SqlRe.Close()
                        Me.CloseConnection()
                        If (Not IsDBNull(BrandID_2)) And (Not IsDBNull(T_Y2)) Then
                            BrandID_2 = CStr(BrandID_2)
                            Target_Year = Convert.ToDecimal(T_Y2) + Convert.ToDecimal(T_Y1)

                            Me.CreateCommandSql("", "SELECT TOTAL_PO FROM" & vbCrLf _
                                               & "( " & vbCrLf _
                                               & "SELECT BB.BRAND_ID,ISNULL(SUM(OPB.TOTAL_PO),0) AS TOTAL_PO " & vbCrLf _
                                               & " FROM BRND_BRANDPACK BB INNER JOIN" & vbCrLf _
                                               & " (" & vbCrLf _
                                               & "SELECT OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO FROM ORDR_PO_BRANDPACK OPB" _
                                               & " WHERE OPB.PO_REF_NO IN(" _
                                               & "SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= " & START_DATE _
                                               & " AND PO_REF_DATE <= " & END_DATE & " AND DISTRIBUTOR_ID IN(SELECT DISTINCT DISTRIBUTOR_ID FROM " _
                                               & " DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'))" _
                                               & " GROUP BY OPB.BRANDPACK_ID" & vbCrLf _
                                               & ")OPB " & vbCrLf _
                                               & "ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID GROUP BY BB.BRAND_ID" _
                                               & ") OPO WHERE BRAND_ID = '" & BrandID_2.ToString() & "'")
                            Dim SUMPO As Object = Me.ExecuteScalar()
                            If (Not IsDBNull(SUMPO)) And (Not IsNothing(SUMPO)) Then
                                TotalPO += Convert.ToDecimal(SUMPO)
                            ElseIf IsNothing(SUMPO) Then : TotalPO = 0
                            Else : TotalPO = 0
                            End If
                        End If

                        If Not TotalPO = 0 Then
                            Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_Year)
                            If CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + AgreeBrandID.ToString() + "'")) Then
                                'custom discount
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & _
                                                  "' AND QSY_DISC_FLAG = 'Y' ORDER BY UP_TO_PCT DESC")
                            ElseIf CInt(Me.ExecuteScalar("", "SELECT COUNT(AGREE_BRAND_ID) FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" + CombAgreeBrandID.ToString() + "'")) Then
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & _
                                                "' AND QSY_DISC_FLAG = 'Y' ORDER BY UP_TO_PCT DESC")
                            Else
                                'discount umum
                                Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                                  "' AND QSY_DISC_FLAG = 'Y' ORDER BY UP_TO_PCT DESC")
                            End If
                            'Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                            '"' AND QSY_DISC_FLAG = 'Y' ORDER BY UP_TO_PCT DESC")
                            If Me.baseChekTable.Rows.Count = 0 Then
                                Throw New System.Exception("Discount Agreement for type Yearly " & vbCrLf & "Has not been set yet")
                            End If
                            Dim AGREE_DISC_PCT As Decimal = 0
                            For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                                If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                                    AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                                    Exit For
                                End If
                            Next
                            'SUM BRANDPACK WITH LOOPING
                            'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                           " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                           " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                           START_DATE & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    'Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER,AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT" & _
                                    '          " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO " & _
                                    '          " AND AGREE_AGREEMENT.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO" & _
                                    '                " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID" & _
                                    '          " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S2 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                    '          START_DATE_S2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                        " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If
                                    'Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                    '" AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    'Price = Me.ExecuteScalar()
                                    'If Not IsDBNull(Price) Then
                                    '    DISC_AMOUNT = Convert.ToDecimal(Convert.ToDecimal(Price) * DISC_QTY)
                                    'Else : DISC_AMOUNT = 0
                                    'End If
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Y"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Y".Trim(), 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'comb_agree_brand_id
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID.ToString() & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                                           " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                                           " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                                          START_DATE & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                                    'Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER,AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT" & _
                                    '          " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO " & _
                                    '          " AND AGREE_AGREEMENT.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO" & _
                                    '                " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID" & _
                                    '          " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE_S2 & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                                    '          START_DATE_S2 & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                    Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                                    tblPOBrandPackPrice.Clear()
                                    Me.FillDataTable(tblPOBrandPackPrice)
                                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                        " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    Price = Me.ExecuteScalar()
                                    If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                                    Else
                                        Price = 0
                                    End If
                                    TotalPOBrandPack_QTY = 0
                                    TotalAmount = 0
                                    DISC_QTY = 0
                                    DISC_AMOUNT = 0
                                    If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                                        For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                                                TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                                            End If
                                            If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                                                TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                                            End If
                                        Next
                                        DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                                        DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                                    End If
                                    'Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                    '" AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                    'Price = Me.ExecuteScalar()
                                    'If Not IsDBNull(Price) Then
                                    '    DISC_AMOUNT = Convert.ToDecimal(Convert.ToDecimal(Price) * DISC_QTY)
                                    'Else : DISC_AMOUNT = 0
                                    'End If
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Y"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Y".Trim(), 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If
                        Else
                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL AGREEBRANDID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Y"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Y".Trim(), 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                            'INSERT ROW BRANDPACK SAVING WITH 0 FOR ALL COMB_AGREE_BRAND_ID
                            Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & CombAgreeBrandID & "'")
                            If Not Me.baseChekTable.Rows.Count = 0 Then
                                For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                                    'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                                    Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Y"
                                    If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                                        Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                                        Price = Me.ExecuteScalar()
                                        If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                                            Price = 0
                                        End If
                                        'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                                        Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                                        Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                                        Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                                        Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Y".Trim(), 2) ' CHAR(1),
                                        Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@TOTAL_PO_AMOUNT", SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                                        Me.AddParameter("@AGREE_DISC_PCT", SqlDbType.Decimal, 0) ' VARCHAR(6),
                                        Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@PRICE_PERQTY", SqlDbType.Decimal, Convert.ToDecimal(Price))
                                        Me.AddParameter("@DISC_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@RELEASE_AMOUNT", SqlDbType.Decimal, 0)
                                        Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, 0) ' INT,
                                        Me.AddParameter("@LEFT_AMOUNT", SqlDbType.Decimal, 0) ' VARCHAR(10),
                                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                        Me.OpenConnection()
                                        Me.BeginTransaction()
                                        Me.ExecuteNonQuery()
                                        Me.CommiteTransaction()
                                        Me.CloseConnection()
                                        Me.ClearCommandParameters()
                                    End If
                                Next
                            End If

                        End If
                    Next
                End If


                'UNTUK KASUS YANG TIDAK DI COMBINED-------------------------------------------------
                ''SELECSI BRAND_ID DARI AGREEMENT YANG TIDAK DI COMBINED
                'Me.CreateCommandSql("", "SELECT BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = '" & _
                'AGREEMENT_NO & "'")
                'Dim tblBrand As New DataTable("BRAND_INCLUDE")
                'tblBrand.Clear()
                'Me.FillDataTable(tblBrand)
                'If tblBrand.Rows.Count = 0 Then
                '    Throw New System.Exception("Brands for Agreement " & AGREEMENT_NO & " Hasn't been defined yet")
                'End If
                ''Target_Year = CInt(Me.ExecuteScalar("", "SELECT TARGET_YEAR FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'"))
                'For i As Integer = 0 To tblBrand.Rows.Count - 1
                '    BrandID_1 = tblBrand.Rows(i)("BRAND_ID")
                '    AgreeBrandID = AGREEMENT_NO + "" + BrandID_1
                '    Target_Year = CInt(Me.ExecuteScalar("", "SELECT TARGET_YEAR FROM VIEW_AGREE_BRAND_INCLUDE WHERE [ID] = '" & AgreeBrandID & "'"))
                '    'AgreeBrandID = tblBrand.Rows(i)("AGREE_BRAND_ID")
                '    'If tblBrand.Rows(i).IsNull("TARGET_YEAR") Then
                '    '    Throw New System.Exception("Some Brand for agreement " & AGREEMENT_NO & " has a null value for target Q1" & vbCrLf & _
                '    '                                "System can not process request due to its Condition")
                '    'End If
                '    'Target_Year = CInt(tblBrand.Rows(i)("TARGET_YEAR"))

                '    Me.CreateCommandSql("", "SELECT SUM(ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY) FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER,AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT" & _
                '                             " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO " & _
                '                             " AND AGREE_AGREEMENT.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO" & _
                '                                    " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID" & _
                '                             " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                '                             START_DATE & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID LIKE '" & BrandID_1.ToString() & "%'")
                '    Dim SUMPO As Object = Me.ExecuteScalar()
                '    If Not IsDBNull(SUMPO) Then
                '        TotalPO = TotalPO + Convert.ToDecimal(Me.RepLaceDotWithComa(SUMPO))
                '        'Else : TotalPO = 0
                '    End If
                'Next
                'If Not TotalPO = 0 Then
                '    'GET PERCENTAGE
                '    Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TotalPO, Target_Year)
                '    Me.SearcData("", "SELECT UP_TO_PCT,PRGSV_DISC_PCT FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & _
                '    "' AND QSY_DISC_FLAG = 'Y' ORDER BY UP_TO_PCT DESC")
                '    If Me.baseChekTable.Rows.Count = 0 Then
                '        Throw New System.Exception("Discount Agreement for type Yearly 2 " & vbCrLf & "Has not been set yet")
                '    End If
                '    Dim AGREE_DISC_PCT As Decimal = 0
                '    For i_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                '        'CHECK WHICH PERCENTAGE THAT MATCH IN UP_TO_PCT IN TABLE AGREE_PROGRESSIJVE DISCOUNT
                '        If PERCENTAGE_SUMQTY > Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("UP_TO_PCT")) Then
                '            AGREE_DISC_PCT = Convert.ToDecimal(Me.baseChekTable.Rows(i_1)("PRGSV_DISC_PCT"))
                '            Exit For
                '        End If
                '    Next
                '    'SUM BRANDPACK WITH LOOPING
                '    'NOW SELECT ALL BRANDPACK BY AGREEBRANDID
                '    Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                '    tblBrand = New DataTable("AGREE_BRAND_INCLUDE")
                '    tblBrand.Clear()
                '    Me.FillDataTable(tblBrand)
                '    If tblBrand.Rows.Count = 0 Then
                '        Throw New System.Exception("Couldn't find Brand for Agreement " & AGREEMENT_NO)
                '    End If
                '    For i_2 As Integer = 0 To tblBrand.Rows.Count - 1
                '        AgreeBrandID = tblBrand.Rows(i_2)("AGREE_BRAND_ID").ToString()
                '        Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                '        If Me.baseChekTable.Rows.Count = 0 Then
                '            'Throw New System.Exception("Couldn't find BrandPack for Agreement " & AGREEMENT_NO)
                '        Else
                '            For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                '                BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                '                Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                '                                           " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                '                                           " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                '                                          START_DATE & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")

                '                'Me.CreateCommandSql("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER,AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT" & _
                '                '          " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO " & _
                '                '          " AND AGREE_AGREEMENT.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO" & _
                '                '         " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID" & _
                '                '          " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= CAST((" & END_DATE & ")AS DATETIME) AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= CAST((" & _
                '                '         START_DATE & ")AS DATETIME) AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                '                Dim tblPOBrandPackPrice As New DataTable("BRANDPACK_PRICE")
                '                tblPOBrandPackPrice.Clear()
                '                Me.FillDataTable(tblPOBrandPackPrice)
                '                TotalPOBrandPack_QTY = 0
                '                TotalAmount = 0
                '                Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                '                                        " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                '                Price = Me.ExecuteScalar()
                '                If (Not IsDBNull(Price)) And (Not IsNothing(Price)) Then
                '                Else
                '                    Price = 0
                '                End If
                '                TotalPOBrandPack_QTY = 0
                '                TotalAmount = 0
                '                DISC_QTY = 0
                '                DISC_AMOUNT = 0
                '                If Not tblPOBrandPackPrice.Rows.Count = 0 Then
                '                    For i_4 As Integer = 0 To tblPOBrandPackPrice.Rows.Count - 1
                '                        If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_ORIGINAL_QTY") Then
                '                            TotalPOBrandPack_QTY = TotalPOBrandPack_QTY + Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY"))
                '                        End If
                '                        If Not tblPOBrandPackPrice.Rows(i_4).IsNull("PO_PRICE_PERQTY") Then
                '                            TotalAmount = TotalAmount + (Convert.ToDecimal(Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_PRICE_PERQTY"))) * Convert.ToDecimal(tblPOBrandPackPrice.Rows(i_4)("PO_ORIGINAL_QTY")))
                '                        End If
                '                    Next
                '                    DISC_QTY = Convert.ToDecimal(Convert.ToDecimal(AGREE_DISC_PCT / 100) * TotalPOBrandPack_QTY)
                '                    DISC_AMOUNT = Convert.ToDecimal(DISC_QTY * Convert.ToDecimal(Price))
                '                End If

                '                'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                '                Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                '                Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Y"
                '                If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                '                    'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                '                    Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                '                    Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                '                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                '                    Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                '                    Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Y", 2) ' CHAR(1),
                '                    Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, TotalPOBrandPack_QTY)
                '                    Me.AddParameter("@TOTAL_PO_AMOUNT",SqlDbType.Decimal, TotalAmount) ') ' VARCHAR(20),
                '                    Me.AddParameter("@AGREE_DISC_PCT",SqlDbType.Decimal, AGREE_DISC_PCT) ' VARCHAR(6),
                '                    Me.AddParameter("@DISC_QTY",SqlDbType.Decimal, DISC_QTY) ' INT,
                '                    Me.AddParameter("@PRICE_PERQTY",SqlDbType.Decimal, Convert.ToDecimal(Price))
                '                    Me.AddParameter("@DISC_AMOUNT",SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                '                    Me.AddParameter("@RELEASE_QTY",SqlDbType.Decimal, 0) ' INT,
                '                    Me.AddParameter("@RELEASE_AMOUNT",SqlDbType.Decimal, 0)
                '                    Me.AddParameter("@LEFT_QTY",SqlDbType.Decimal, DISC_QTY) ' INT,
                '                    Me.AddParameter("@LEFT_AMOUNT",SqlDbType.Decimal, DISC_AMOUNT) ' VARCHAR(10),
                '                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                '                    Me.OpenConnection()
                '                    Me.BeginTransaction()
                '                    Me.ExecuteNonQuery()
                '                    Me.CommiteTransaction()
                '                    Me.CloseConnection()
                '                    Me.ClearCommandParameters()
                '                End If
                '            Next
                '        End If
                '        'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                '    Next
                'Else
                '    Me.CreateCommandSql("", "SELECT AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                '    tblBrand = New DataTable("AGREE_BRAND_INCLUDE")
                '    tblBrand.Clear()
                '    Me.FillDataTable(tblBrand)
                '    If tblBrand.Rows.Count = 0 Then
                '        Throw New System.Exception("Couldn't find Brand for Agreement " & AGREEMENT_NO)
                '    End If
                '    For i_2 As Integer = 0 To tblBrand.Rows.Count - 1
                '        AgreeBrandID = tblBrand.Rows(i_2)("AGREE_BRAND_ID").ToString()
                '        Me.SearcData("", "SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = '" & AgreeBrandID.ToString() & "'")
                '        If Me.baseChekTable.Rows.Count = 0 Then
                '            'Throw New System.Exception("Couldn't find BrandPack for Agreement " & AGREEMENT_NO)
                '        Else
                '            For i_3 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                '                'INSERT ROW BRANDPACK SAVING WITH 0

                '                BRANDPACK_ID = Me.baseChekTable.Rows(i_3)("BRANDPACK_ID").ToString()
                '                'NOW IS THE TIME TO INSERT BUT BEFORE THAT CHECK WHETER BRND_B_S_ID HAS EXISTS
                '                Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_ID
                '                Dim BRND_BS_ID As String = DISTRIBUTOR_ID & AGREE_BRANDPACK_ID & "Y"
                '                If IsNothing(Me.ExecuteScalar("", "SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING WHERE BRND_B_S_ID = '" & BRND_BS_ID & "'")) Then
                '                    Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                '                                            " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                '                    Price = Me.ExecuteScalar()
                '                    If (IsDBNull(Price)) Or (IsNothing(Price)) Then
                '                        Price = 0
                '                    End If
                '                    'NOW IS THE TIME TO INSERT INTO TABLE BRANDPACK_SAVING
                '                    Me.CreateCommandSql("Usp_INSERT_BRND_BRANDPACK_SAVING", "")
                '                    Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_BS_ID, 60) ' VARCHAR(50),
                '                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                '                    Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(14),
                '                    Me.AddParameter("@QSY_FLAG", SqlDbType.VarChar, "Y", 2) ' CHAR(1),
                '                    Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.Decimal, 0)
                '                    Me.AddParameter("@TOTAL_PO_AMOUNT",SqlDbType.Decimal, 0) ') ' VARCHAR(20),
                '                    Me.AddParameter("@AGREE_DISC_PCT",SqlDbType.Decimal, 0) ' VARCHAR(6),
                '                    Me.AddParameter("@DISC_QTY",SqlDbType.Decimal, 0) ' INT,
                '                    Me.AddParameter("@PRICE_PERQTY",SqlDbType.Decimal, Convert.ToDecimal(Price))
                '                    Me.AddParameter("@DISC_AMOUNT",SqlDbType.Decimal, 0) ' VARCHAR(10),
                '                    Me.AddParameter("@RELEASE_QTY",SqlDbType.Decimal, 0) ' INT,
                '                    Me.AddParameter("@RELEASE_AMOUNT",SqlDbType.Decimal, 0)
                '                    Me.AddParameter("@LEFT_QTY",SqlDbType.Decimal, 0) ' INT,
                '                    Me.AddParameter("@LEFT_AMOUNT",SqlDbType.Decimal, 0) ' VARCHAR(10),
                '                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                '                    Me.OpenConnection()
                '                    Me.BeginTransaction()
                '                    Me.ExecuteNonQuery()
                '                    Me.CommiteTransaction()
                '                    Me.CloseConnection()
                '                    Me.ClearCommandParameters()
                '                End If
                '            Next
                '        End If
                '        'throw new System.Exception("BrandPack for Agreement " & AGREEMENT_NO & " has not been defined yet
                '    Next
                'End If

                '---------------------------------------------------------------------------------------------------

            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

#End Region

    End Class
    
End Namespace

