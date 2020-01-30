Namespace Program
    Public Class MarketingDiscount
        Inherits NufarmBussinesRules.Program.Core
        Private m_ViewDiscount As DataView
        Private m_ViewDistMarketing As DataView

        Private Function GetPercentage(ByVal persen As Integer, ByVal SUM_PO_ORIGINALQTY As Decimal, ByVal TARGET_QTY As Int64) As Decimal
            Return CDec((SUM_PO_ORIGINALQTY * persen * 1) / TARGET_QTY)
        End Function

        Protected Function RepLaceComaWithDot(ByVal text As String) As String
            Dim l As Integer = Len(Trim(text))
            Dim w As Integer = 1
            Dim s As String = ""
            Dim a As String = ""
            Do Until w = l + 1
                s = Mid(Trim(text), w, 1)
                If s = "," Then
                    s = "."
                End If
                a = a & s
                w += 1
            Loop
            Return a
        End Function

        Protected Function RepLaceDotWithComa(ByVal text As String) As String
            Dim l As Integer = Len(Trim(text))
            Dim w As Integer = 1
            Dim s As String = ""
            Dim a As String = ""
            Do Until w = l + 1
                s = Mid(Trim(text), w, 1)
                If s = "." Then
                    s = ","
                End If
                a = a & s
                w += 1
            Loop
            Return a
        End Function

        Public Shadows Function HasGeneratedDiscount(ByVal DISTRIBUTOR_ID As String, ByVal PROGRAM_ID As String) As Boolean
            Try
                Me.CreateCommandSql("", "SELECT COUNT(MRKT_MARKETING_SAVING.PROG_BRANDPACK_DIST_ID) FROM MRKT_BRANDPACK_DISTRIBUTOR,MRKT_MARKETING_SAVING" & _
                " WHERE MRKT_MARKETING_SAVING.PROG_BRANDPACK_DIST_ID = MRKT_BRANDPACK_DISTRIBUTOR.PROG_BRANDPACK_DIST_ID" & _
                " AND MRKT_BRANDPACK_DISTRIBUTOR.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "' AND MRKT_BRANDPACK_DISTRIBUTOR.PROG_BRANDPACK_DIST_ID" & _
                " LIKE '" & PROGRAM_ID & "%'")
                If CInt(Me.ExecuteScalar()) > 0 Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function CreateViewDiscount(ByVal DISTRIBUTOR_ID As String, ByVal PROGRAM_ID As String) As DataView
            Try
                Me.CreateCommandSql("Usp_GetView_Discount_Marketing", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, PROGRAM_ID, 15) ' VARCHAR(15)
                Dim tblDiscount As New DataTable("DISCOUNT_MARKETING")
                tblDiscount.Clear()
                Me.FillDataTable(tblDiscount)
                Me.m_ViewDiscount = tblDiscount.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDiscount
        End Function

        Public Function CreateViewDistributorMarketing() As DataView
            Try
                Me.SearcData("Usp_GetView_Distributor_Marketing", "")
                Me.m_ViewDistMarketing = Me.baseChekTable.DefaultView()
                Me.m_ViewDistMarketing.ApplyDefaultSort() = True
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistMarketing
        End Function

        Public ReadOnly Property ViewDistibutorMarketing() As DataView
            Get
                Return Me.m_ViewDistMarketing
            End Get
        End Property

        Public ReadOnly Property ViewDiscount() As DataView
            Get
                Return Me.m_ViewDiscount
            End Get
        End Property

        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewDiscount) Then
                Me.m_ViewDiscount.Dispose()
                Me.m_ViewDiscount = Nothing
            End If
            If Not IsNothing(Me.m_ViewDistMarketing) Then
                Me.m_ViewDistMarketing.Dispose()
                Me.m_ViewDistMarketing = Nothing
            End If
        End Sub

#Region " new data generating discount "
        Public Sub GenerateDiscount_1(ByVal DISTRIBUTOR_ID As String, ByVal PROGRAM_ID As String)
            Try
                'ALGORITMA
                'SELECKSI BRANDPACK YANG DIIKUTI DISTRIBUTOR BERDASARKAN PROGRAM_ID
                Me.CreateCommandSql("Usp_Select_BrandPack_Marketing_Distributor", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, PROGRAM_ID, 15) ' VARCHAR(15)
                Dim tblBMDistributor As New DataTable()
                tblBMDistributor.Clear()
                Me.FillDataTable(tblBMDistributor)
                If tblBMDistributor.Rows.Count > 0 Then
                Else
                    Throw New System.Exception("there's no brandpack held by distributor for such marketing program")
                End If
                For i As Integer = 0 To tblBMDistributor.Rows.Count - 1
                    Dim END_DATE As Object = tblBMDistributor.Rows(i)("END_DATE")
                    Dim START_DATE As Object = tblBMDistributor.Rows(i)("START_DATE")
                    If CDate(START_DATE) >= NufarmBussinesRules.SharedClass.ServerDate() Then
                    Else
                        If NufarmBussinesRules.SharedClass.ServerDate > CDate(END_DATE) Then
                            END_DATE = CStr("'" & Month(CDate(END_DATE)).ToString() & "/" & Day(CDate(END_DATE)).ToString() & "/" & Year(CDate(END_DATE)).ToString() & "'")
                            Dim BRANDPACK_ID As String = tblBMDistributor.Rows(i)("BRANDPACK_ID").ToString()
                            Dim PRICE_NOW As Object = Me.ExecuteScalar("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                                                    " AND START_DATE <= " & END_DATE & " ORDER BY START_DATE DESC")
                            If (Not IsDBNull(PRICE_NOW)) And (Not IsNothing(PRICE_NOW)) Then
                                PRICE_NOW = CDec(Me.RepLaceDotWithComa(PRICE_NOW.ToString()))
                            Else
                                PRICE_NOW = 0
                            End If
                            Dim PROG_BRANDPACK_DIST_ID As String = tblBMDistributor.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString()
                            'Dim START_DATE As Object = tblBMDistributor.Rows(i)("START_DATE")
                            Dim TOTAL_REACHED_QTY As Decimal = 0
                            Dim TOTAL_REACHED_AMOUNT As Decimal = 0
                            Dim TARGET_DISC_PCT As Decimal = CDec(Me.RepLaceDotWithComa(tblBMDistributor.Rows(i)("TARGET_DISC_PCT")).ToString())
                            Dim TARGET_QTY As Integer = CInt(tblBMDistributor.Rows(i)("TARGET_QTY"))
                            Dim ST_FLAG As String = tblBMDistributor.Rows(i)("STEPPING_FLAG").ToString()
                            'CDec(Me.RepLaceDotWithComa(tblBMDistributor.Rows(i)("PRICE_PERQTY").ToString()))
                            Dim DISC_QTY As Decimal = 0
                            Dim DISC_AMOUNT As Decimal = 0
                            Dim MRKT_LEFT_QTY As Decimal = 0
                            Dim MRKT_LEFT_AMOUNT As Decimal = 0

                            'SUM PO_QTY BY DISTRIBUTOR BY BRANDPACK_ID
                            'START_DATE = 

                            START_DATE = CStr("'" & Month(CDate(START_DATE)).ToString() & "/" & Day(CDate(START_DATE)).ToString() & "/" & Year(CDate(START_DATE)).ToString() & "'")
                            Me.SearcData("", "SELECT ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY,ORDR_PO_BRANDPACK.PO_PRICE_PERQTY FROM ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                                              " WHERE ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "' AND ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'" & _
                                              " AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= " & START_DATE & " AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= " & END_DATE)
                            If Me.baseChekTable.Rows.Count > 0 Then
                                Dim PRICE As Decimal = 0
                                'HITUNG TOTAL_REACHED_QTY,TOTAL_REACHED_AMOUNT,DISC_QTY,DISC_AMOUNT
                                For I_1 As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                                    TOTAL_REACHED_QTY = TOTAL_REACHED_QTY + CDec(Me.RepLaceDotWithComa(Me.baseChekTable.Rows(I_1)("PO_ORIGINAL_QTY").ToString()))
                                    PRICE = CDec(Me.RepLaceDotWithComa(Me.baseChekTable.Rows(I_1)("PO_PRICE_PERQTY").ToString()))
                                    TOTAL_REACHED_AMOUNT = TOTAL_REACHED_AMOUNT + CDec(PRICE * TOTAL_REACHED_QTY)
                                Next
                                Select Case CBool(ST_FLAG)
                                    Case False
                                        'HITUNG DISCOUNT
                                        If TOTAL_REACHED_QTY >= TARGET_QTY Then
                                            DISC_QTY = CDec(CDec(TARGET_DISC_PCT / 100) * TOTAL_REACHED_QTY)
                                            DISC_AMOUNT = CDec(PRICE_NOW * DISC_QTY)
                                        Else
                                            DISC_QTY = 0
                                            DISC_AMOUNT = 0
                                        End If
                                        Dim MRKT_M_S_ID As String = PROG_BRANDPACK_DIST_ID + "MT"
                                        If IsNothing(Me.ExecuteScalar("", "SELECT MRKT_M_S_ID FROM MRKT_MARKETING_SAVING WHERE MRKT_M_S_ID = '" & MRKT_M_S_ID & "'")) Then
                                            Me.CreateCommandSql("Usp_Insert_MRKT_MARKETING_SAVING", "")
                                            Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, MRKT_M_S_ID, 60) ' VARCHAR(60),
                                            Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID, 40) ' VARCHAR(35),
                                            Me.AddParameter("@ST_FLAG", SqlDbType.Char, "MT", 2) ' CHAR(1),
                                            Me.AddParameter("@TOTAL_REACHED_QTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(TOTAL_REACHED_QTY.ToString()), 14) ' INT,
                                            Me.AddParameter("@TOTAL_REACHED_AMOUNT", SqlDbType.VarChar, Me.RepLaceComaWithDot(TOTAL_REACHED_AMOUNT.ToString()), 20) ' VARCHAR(13),
                                            Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.VarChar, Me.RepLaceComaWithDot(TARGET_DISC_PCT.ToString()), 6) ' VARCHAR(6),
                                            Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(DISC_QTY.ToString()), 10) ' INT,
                                            Me.AddParameter("@PRICE_PERQTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(PRICE_NOW.ToString()), 10) ' VARCHAR(10),
                                            Me.AddParameter("@DISC_AMOUNT", SqlDbType.VarChar, Me.RepLaceComaWithDot(DISC_AMOUNT.ToString()), 10) ' VARCHAR(10),
                                            Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                            Me.AddParameter("@MRKT_RELEASE_AMOUNT", SqlDbType.VarChar, CObj("0"), 10) ' VARCHAR(10),
                                            Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(DISC_QTY.ToString()), 10) ' INT,
                                            Me.AddParameter("@MRKT_LEFT_AMOUNT", SqlDbType.VarChar, Me.RepLaceComaWithDot(DISC_AMOUNT.ToString()), 10) ' VARCHAR(10),
                                            Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                            Me.OpenConnection()
                                            Me.BeginTransaction()
                                            Me.ExecuteNonQuery()
                                            Me.CommiteTransaction()
                                            Me.CloseConnection()
                                        End If
                                    Case True
                                        Dim PERCENTAGE_SUMQTY As Decimal = Me.GetPercentage(100, TOTAL_REACHED_QTY, TARGET_QTY)
                                        Dim MRKT_DISC_PCT As Decimal = 0
                                        Me.CreateCommandSql("", "SELECT MORE_THAN_QTY,STEPPING_DISC_PCT FROM MRKT_STEPPING_DISCOUNT WHERE PROG_BRANDPACK_DIST_ID = '" & _
                                        PROG_BRANDPACK_DIST_ID & "' ORDER BY MORE_THAN_QTY DESC")
                                        Dim tblStepping As New DataTable("STEPPING_DISCOUNT")
                                        tblStepping.Clear()
                                        Me.FillDataTable(tblStepping)
                                        If tblStepping.Rows.Count > 0 Then
                                            For i1 As Integer = 0 To tblStepping.Rows.Count - 1
                                                If PERCENTAGE_SUMQTY > CDec(Me.RepLaceDotWithComa(tblStepping.Rows(i1)("MORE_THAN_QTY").ToString())) Then
                                                    MRKT_DISC_PCT = CDec(Me.RepLaceDotWithComa(tblStepping.Rows(i1)("STEPPING_DISC_PCT").ToString()))
                                                    Exit For
                                                End If
                                            Next
                                            DISC_QTY = CDec(CDec(MRKT_DISC_PCT / 100) * TOTAL_REACHED_QTY)
                                            DISC_AMOUNT = CDec(PRICE_NOW * DISC_QTY)
                                            'NOW IS THE TIME TO INSERT INTO MRKT_MARKETING_SAVING
                                            Dim MRKT_M_S_ID As String = PROG_BRANDPACK_DIST_ID + "MS"
                                            'CHEK MRKT_M_S_ID
                                            If IsNothing(Me.ExecuteScalar("", "SELECT MRKT_M_S_ID FROM MRKT_MARKETING_SAVING WHERE MRKT_M_S_ID = '" & MRKT_M_S_ID & "'")) Then
                                                Me.CreateCommandSql("Usp_Insert_MRKT_MARKETING_SAVING", "")
                                                Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, MRKT_M_S_ID, 60) ' VARCHAR(60),
                                                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID, 40) ' VARCHAR(35),
                                                Me.AddParameter("@ST_FLAG", SqlDbType.VarChar, "MS", 2) ' CHAR(1),
                                                Me.AddParameter("@TOTAL_REACHED_QTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(TOTAL_REACHED_QTY.ToString()), 14) ' INT,
                                                Me.AddParameter("@TOTAL_REACHED_AMOUNT", SqlDbType.VarChar, Me.RepLaceComaWithDot(TOTAL_REACHED_AMOUNT.ToString()), 20) ' VARCHAR(13),
                                                Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.VarChar, Me.RepLaceComaWithDot(MRKT_DISC_PCT.ToString()), 6) ' VARCHAR(6),
                                                Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(DISC_QTY.ToString()), 10) ' INT,
                                                Me.AddParameter("@PRICE_PERQTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(PRICE_NOW.ToString()), 10) ' VARCHAR(10),
                                                Me.AddParameter("@DISC_AMOUNT", SqlDbType.VarChar, Me.RepLaceComaWithDot(DISC_AMOUNT.ToString()), 10) ' VARCHAR(10),
                                                Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                                Me.AddParameter("@MRKT_RELEASE_AMOUNT", SqlDbType.VarChar, CObj("0"), 10) ' VARCHAR(10),
                                                Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(DISC_QTY.ToString()), 10) ' INT,
                                                Me.AddParameter("@MRKT_LEFT_AMOUNT", SqlDbType.VarChar, Me.RepLaceComaWithDot(DISC_AMOUNT.ToString()), 10) ' VARCHAR(10),
                                                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                                Me.OpenConnection()
                                                Me.BeginTransaction()
                                                Me.ExecuteNonQuery()
                                                Me.CommiteTransaction()
                                                Me.CloseConnection()
                                            End If
                                        Else
                                            Me.CreateCommandSql("", "SELECT BRANDPACK_NAME FROM BRND_BRANDPACK WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "'")
                                            Dim BRANDPACK_NAME As String = Me.ExecuteScalar().ToString()
                                            System.Windows.Forms.MessageBox.Show("Discount % : " & BRANDPACK_NAME & " For Distributor_ID : " & DISTRIBUTOR_ID & vbCrLf & _
                                            "Has not been set yet", "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                        End If

                                End Select

                            Else
                                'INSERT DATA WITH ALL 0 VALUE
                                Select Case CBool(ST_FLAG)
                                    Case True
                                        Dim MRKT_M_S_ID As String = PROG_BRANDPACK_DIST_ID + "MS"
                                        If IsNothing(Me.ExecuteScalar("", "SELECT MRKT_M_S_ID FROM MRKT_MARKETING_SAVING WHERE MRKT_M_S_ID = '" & MRKT_M_S_ID & "'")) Then
                                            Me.CreateCommandSql("Usp_Insert_MRKT_MARKETING_SAVING", "")
                                            Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, MRKT_M_S_ID, 60) ' VARCHAR(60),
                                            Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID, 40) ' VARCHAR(35),
                                            Me.AddParameter("@ST_FLAG", SqlDbType.VarChar, "MS", 2) ' CHAR(1),
                                            Me.AddParameter("@TOTAL_REACHED_QTY", SqlDbType.VarChar, CObj("0"), 14) ' INT,
                                            Me.AddParameter("@TOTAL_REACHED_AMOUNT", SqlDbType.VarChar, CObj("0"), 20) ' VARCHAR(13),
                                            Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.VarChar, CObj("0"), 6) ' VARCHAR(6),
                                            Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                            Me.AddParameter("@PRICE_PERQTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(PRICE_NOW.ToString()), 10) ' VARCHAR(10),
                                            Me.AddParameter("@DISC_AMOUNT", SqlDbType.VarChar, CObj("0"), 10) ' VARCHAR(10),
                                            Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                            Me.AddParameter("@MRKT_RELEASE_AMOUNT", SqlDbType.VarChar, CObj("0"), 10) ' VARCHAR(10),
                                            Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                            Me.AddParameter("@MRKT_LEFT_AMOUNT", SqlDbType.VarChar, CObj("0"), 10) ' VARCHAR(10),
                                            Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                            Me.OpenConnection()
                                            Me.BeginTransaction()
                                            Me.ExecuteNonQuery()
                                            Me.CommiteTransaction()
                                            Me.CloseConnection()
                                        End If
                                    Case False
                                        Dim MRKT_M_S_ID As String = PROG_BRANDPACK_DIST_ID + "T"
                                        If IsNothing(Me.ExecuteScalar("", "SELECT MRKT_M_S_ID FROM MRKT_MARKETING_SAVING WHERE MRKT_M_S_ID = '" & MRKT_M_S_ID & "'")) Then
                                            Me.CreateCommandSql("Usp_Insert_MRKT_MARKETING_SAVING", "")
                                            Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, MRKT_M_S_ID, 60) ' VARCHAR(60),
                                            Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID, 40) ' VARCHAR(35),
                                            Me.AddParameter("@ST_FLAG", SqlDbType.VarChar, "T", 2) ' CHAR(1),
                                            Me.AddParameter("@TOTAL_REACHED_QTY", SqlDbType.VarChar, CObj("0"), 14) ' INT,
                                            Me.AddParameter("@TOTAL_REACHED_AMOUNT", SqlDbType.VarChar, CObj("0"), 20) ' VARCHAR(13),
                                            Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.VarChar, Me.RepLaceComaWithDot(TARGET_DISC_PCT.ToString()), 6) ' VARCHAR(6),
                                            Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                            Me.AddParameter("@PRICE_PERQTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(PRICE_NOW.ToString()), 10) ' VARCHAR(10),
                                            Me.AddParameter("@DISC_AMOUNT", SqlDbType.VarChar, CObj("0"), 10) ' VARCHAR(10),
                                            Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                            Me.AddParameter("@MRKT_RELEASE_AMOUNT", SqlDbType.VarChar, CObj("0"), 10) ' VARCHAR(10),
                                            Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                            Me.AddParameter("@MRKT_LEFT_AMOUNT", SqlDbType.VarChar, CObj("0"), 10) ' VARCHAR(10),
                                            Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                            Me.OpenConnection()
                                            Me.BeginTransaction()
                                            Me.ExecuteNonQuery()
                                            Me.CommiteTransaction()
                                            Me.CloseConnection()
                                        End If
                                End Select
                            End If
                        End If
                    End If
                Next
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
#End Region

    End Class
End Namespace

