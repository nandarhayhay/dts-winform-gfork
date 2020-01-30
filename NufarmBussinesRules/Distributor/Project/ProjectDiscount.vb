Namespace DistributorProject
    Public Class ProjectDiscount
        Inherits NufarmBussinesRules.DistributorProject.ProjectBrandPack
        Private m_ViewDiscount As DataView
        Private m_ViewDistProject As DataView
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

        Public Overloads Sub Discpose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewDiscount) Then
                Me.m_ViewDiscount.Dispose()
                Me.m_ViewDiscount = Nothing
            End If
            If Not IsNothing(Me.m_ViewDistProject) Then
                Me.m_ViewDistProject.Dispose()
                Me.m_ViewDistProject = Nothing
            End If
        End Sub
        Public Function CreateViewDiscount(ByVal DistributorID As String, ByVal PROJ_REF_NO As String) As DataView
            Try
                Me.CreateCommandSql("Usp_GetView_Project_Discount", "")
                Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, PROJ_REF_NO, 15) ' VARCHAR(15),
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10) ' VARCHAR(10)
                Dim tblDiscount As New DataTable("DISCOUNT_PROJECT")
                tblDiscount.Clear()
                Me.FillDataTable(tblDiscount)
                Me.m_ViewDiscount = tblDiscount.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
            End Try
            Return Me.m_ViewDiscount
        End Function
        Public Function CreateViewDistProject()
            Try
                Me.CreateCommandSql("", "SELECT PROJ_PROJECT.DISTRIBUTOR_ID,DIST_DISTRIBUTOR.DISTRIBUTOR_NAME,PROJ_PROJECT.PROJ_REF_NO" & _
                                       " FROM PROJ_PROJECT,DIST_DISTRIBUTOR WHERE PROJ_PROJECT.DISTRIBUTOR_ID = DIST_DISTRIBUTOR.DISTRIBUTOR_ID")
                Dim tblProject As New DataTable("PROJECT")
                tblProject.Clear()
                Me.FillDataTable(tblProject)
                Me.m_ViewDistProject = tblProject.DefaultView()
                Me.m_ViewDistProject.Sort = "PROJ_REF_NO"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistProject
        End Function
        Public Sub GenerateDiscount(ByVal DistributorID As String, ByVal PROJ_REF_NO As String)
            Try
                Dim END_D As Date = NufarmBussinesRules.SharedClass.ServerDate()
                Dim END_DATE As String = CStr("'" & Month(END_D).ToString() & "/" & Day(END_D).ToString() & "/" & Year(END_D).ToString() & "'")
                Me.SearcData("", "SELECT PROJ_BRANDPACK_ID,BRANDPACK_ID,APPROVED_DISC_PCT FROM PROJ_BRANDPACK WHERE PROJ_REF_NO = '" & PROJ_REF_NO & _
                "' AND END_DATE < CAST((" & END_DATE & ") AS DATETIME)")
                Dim tblProjBrandpack As New DataTable("Proj_Brandpack")
                tblProjBrandpack.Clear()
                Me.FillDataTable(tblProjBrandpack)
                If tblProjBrandpack.Rows.Count <= 0 Then
                Else
                    For i As Integer = 0 To tblProjBrandpack.Rows.Count - 1
                        Dim Total_PO_Qty As Decimal = 0
                        Dim Disc_Qty As Decimal = 0
                        Dim Disc_pct As Decimal = CDec(Me.RepLaceDotWithComa(tblProjBrandpack.Rows(i)("APPROVED_DISC_PCT")))
                        Dim PROJ_BRANDPACK_ID As String = tblProjBrandpack.Rows(i)("PROJ_BRANDPACK_ID").ToString()
                        Dim BRANDPACK_ID As String = tblProjBrandpack.Rows(i)("BRANDPACK_ID").ToString()
                        Dim IDENTITY As Integer = Me.ExecuteScalar("", "SELECT COUNT(PROJ_DISC_HIST_ID) FROM PROJ_DISC_HISTORY WHERE PROJ_BRANDPACK_ID = '" & PROJ_BRANDPACK_ID & "'")
                        Dim MARK As String = "@"
                        Dim PROJ_DISC_HIST_ID As String = PROJ_BRANDPACK_ID & "" & MARK & "" & IDENTITY.ToString()
                        'SUM BRANDPACK IN PO
                        Me.CreateCommandSql("", "SELECT SUM(ORDR_PO_BRANDPACK.PO_ORIGINAL_QTY) FROM  ORDR_PO_BRANDPACK,ORDR_PURCHASE_ORDER" & _
                        " WHERE ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & BRANDPACK_ID & "' AND ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO" & _
                        " AND ORDR_PURCHASE_ORDER.PROJ_REF_NO = '" & PROJ_REF_NO & "' AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DistributorID & _
                        "' AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= (SELECT START_DATE FROM PROJ_BRANDPACK WHERE PROJ_BRANDPACK_ID = '" & PROJ_BRANDPACK_ID & "') AND ORDR_PURCHASE_ORDER.PO_REF_DATE <= " & _
                        " (SELECT END_DATE FROM PROJ_BRANDPACK WHERE PROJ_BRANDPACK_ID = '" & PROJ_BRANDPACK_ID & "')")
                        Dim SUMPO As Object = Me.ExecuteScalar()
                        If (IsDBNull(SUMPO)) Or (IsNothing(SUMPO)) Then
                            'INSERT DATA WITH 0 VALUE

                            If IsNothing(Me.ExecuteScalar("", "SELECT PROJ_DISC_HIST_ID FROM PROJ_DISC_HISTORY WHERE PROJ_DISC_HIST_ID LIKE '" & PROJ_BRANDPACK_ID & "%'")) Then
                                Me.CreateCommandSql("Sp_Insert_PROJ_DISC_HISTORY", "")
                                Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, PROJ_DISC_HIST_ID, 66) ' VARCHAR(66),
                                Me.AddParameter("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, PROJ_BRANDPACK_ID, 30) ' VARCHAR(25),
                                'ME.AddParameter("@OA_BRANDPACK_ID",SqlDbType.VarChar, VARCHAR(44),
                                Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.VarChar, CObj("0"), 14) ' INT,
                                Me.AddParameter("@PROJ_DISC_PCT", SqlDbType.VarChar, Me.RepLaceComaWithDot(Disc_pct), 6) ' VARCHAR(6),
                                Me.AddParameter("@PROJ_DISC_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                Me.AddParameter("@PROJ_RELEASE_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                Me.AddParameter("@PROJ_LEFT_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                Me.OpenConnection()
                                Me.BeginTransaction()
                                Me.ExecuteNonQuery()
                                Me.CommiteTransaction()
                                Me.CloseConnection()
                            End If
                        Else
                            If IsNothing(Me.ExecuteScalar("", "SELECT PROJ_DISC_HIST_ID FROM PROJ_DISC_HISTORY WHERE PROJ_DISC_HIST_ID LIKE '" & PROJ_BRANDPACK_ID & "%'")) Then
                                Total_PO_Qty = CDec(SUMPO)
                                Disc_Qty = CDec(CDec(Disc_pct / 100) * Total_PO_Qty)
                                Me.CreateCommandSql("Sp_Insert_PROJ_DISC_HISTORY", "")
                                Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, PROJ_DISC_HIST_ID, 66) ' VARCHAR(66),
                                Me.AddParameter("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, PROJ_BRANDPACK_ID, 30) ' VARCHAR(25),
                                'ME.AddParameter("@OA_BRANDPACK_ID",SqlDbType.VarChar, VARCHAR(44),
                                Me.AddParameter("@TOTAL_PO_QTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(Total_PO_Qty.ToString()), 14) ' INT,
                                Me.AddParameter("@PROJ_DISC_PCT", SqlDbType.VarChar, Me.RepLaceComaWithDot(Disc_pct.ToString()), 6) ' VARCHAR(6),
                                Me.AddParameter("@PROJ_DISC_QTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(Disc_Qty.ToString()), 10) ' INT,
                                Me.AddParameter("@PROJ_RELEASE_QTY", SqlDbType.VarChar, CObj("0"), 10) ' INT,
                                Me.AddParameter("@PROJ_LEFT_QTY", SqlDbType.VarChar, Me.RepLaceComaWithDot(Disc_Qty.ToString()), 10) ' INT,
                                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                Me.OpenConnection()
                                Me.BeginTransaction()
                                Me.ExecuteNonQuery()
                                Me.CommiteTransaction()
                                Me.CloseConnection()
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public ReadOnly Property ViewDiscount() As DataView
            Get
                Return Me.m_ViewDiscount
            End Get
        End Property

        Public ReadOnly Property ViewDistProject() As DataView
            Get
                Return Me.m_ViewDistProject
            End Get
        End Property
    End Class
End Namespace

