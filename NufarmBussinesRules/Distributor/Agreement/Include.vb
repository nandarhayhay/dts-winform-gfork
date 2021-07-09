Imports System.Data
Imports System.Data.SqlClient
Namespace DistributorAgreement
    Public Class Include
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet

#Region " Deklarasi "
        Private m_ViewBrand As DataView
        Private m_ViewAgreement As DataView
        Private m_DiscGenerate As DataView
        Private m_OAHistory As DataView
        Public Agreement_no As String
        Private m_ViewCombined As DataView
        Private m_DSPeriodic As DataSet
        Private m_ViewFilterBrand As DataView
        Private m_ViewBrandpack1 As DataView
        Private m_ViewAgFirstSecond As DataView
        Private m_ViewGivenStory As DataView
        Public BrandID As String
        Public Q1 As Decimal = 0
        Public Q2 As Decimal = 0
        Public Q3 As Decimal = 0
        Public Q4 As Decimal = 0
        Public S1 As Decimal = 0
        Public S2 As Decimal = 0
        Public PBQ3 As Decimal = 0
        Public PBQ4 As Decimal = 0
        Public PBS2 As Decimal = 0
        Public CPQ1 As Decimal = 0
        Public CPQ2 As Decimal = 0
        Public CPQ3 As Decimal = 0
        Public CPS1 As Decimal = 0
        Public PBF2 As Decimal = 0
        Public PBF3 As Decimal = 0
        Public CPF1 As Decimal = 0
        Public CPF2 As Decimal = 0
        Public PBY As Decimal = 0
        Public Flag As String
        Public Q1_FM As Decimal = 0
        Public Q2_FM As Decimal = 0
        Public Q3_FM As Decimal = 0
        Public Q4_FM As Decimal = 0
        Public S1_FM As Decimal = 0
        Public S2_FM As Decimal = 0

        Public Q1_PL As Decimal = 0
        Public Q2_PL As Decimal = 0
        Public Q3_PL As Decimal = 0
        Public Q4_PL As Decimal = 0
        Public S1_PL As Decimal = 0
        Public S2_PL As Decimal = 0

        Public FMP1 As Decimal = 0, FMP2 As Decimal = 0, FMP3 As Decimal = 0
        Public FMP_FM1 As Decimal
        Public FMP_FM2 As Decimal
        Public FMP_FM3 As Decimal
        Public FMP_PL1 As Decimal
        Public FMP_PL2 As Decimal
        Public FMP_PL3 As Decimal
        Public GIvenDiscount As Decimal = 0
        Public Agree_Brand_ID As String
        Public Comb_Agree_Brand_ID As Object
        'Private m_ViewBrandPack_FilterBrand As DataView
        Dim HastCollCommBined As Hashtable ' hashtable untuk dari datatable sementara
        Dim NVCombUnik As System.Collections.Specialized.NameValueCollection  'Namevalue Collection untuk comb_brand_id setelah hastCollCombined di remove keynya
        Dim ColNode As Collection
        Private tbl_Quantity_Combined As DataTable
        Private Tbl_BrandComboFisrtSecond As DataTable
        Private tblIBIncluded As DataTable
        Private TblItemBrandPack As DataTable
        Dim ViewBrandpackIncluded As DataView
        Private m_ViewOAHistory As DataView
        Private m_dataTableQuarterly As DataTable
        Private m_dataTableYearly As DataTable
        Private m_dataTableSemeterly As DataTable
        Private m_dataTableSemeterlyV As DataTable
        Private m_dataTableYearlyV As DataTable
        Private m_dataTableQuarterlyV As DataTable
        Private Query As String = ""
#End Region

#Region " Sub "

        Public Sub New()
            Me.Q1 = 0
            Me.Q2 = 0
            Me.Q3 = 0
            Me.Q4 = 0
            Me.S1 = 0
            Me.S2 = 0
            Me.Comb_Agree_Brand_ID = DBNull.Value
            Me.Flag = ""
        End Sub

        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            'dont dispose mybase because will be used in agreenment form
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_DiscGenerate) Then
                Me.m_DiscGenerate.Dispose()
                Me.m_DiscGenerate = Nothing
            End If
            If Not IsNothing(Me.m_DSPeriodic) Then
                Me.m_DSPeriodic.Dispose()
                Me.m_DSPeriodic = Nothing
            End If

            If Not IsNothing(Me.m_OAHistory) Then
                Me.m_OAHistory.Dispose()
                Me.m_OAHistory = Nothing
            End If
            If Not IsNothing(Me.m_ViewAgreement) Then
                Me.m_ViewAgreement.Dispose()
                Me.m_ViewAgreement = Nothing
            End If
            If Not IsNothing(Me.m_ViewBrand) Then
                Me.m_ViewBrand.Dispose()
                Me.m_ViewBrand = Nothing
            End If
            If Not IsNothing(Me.m_ViewCombined) Then
                Me.m_ViewCombined.Dispose()
                Me.m_ViewCombined = Nothing
            End If
            If Not IsNothing(Me.m_ViewFilterBrand) Then
                Me.m_ViewFilterBrand.Dispose()
                Me.m_ViewFilterBrand = Nothing
            End If
            If Not IsNothing(Me.tblIBIncluded) Then
                Me.tblIBIncluded.Dispose()
                Me.tblIBIncluded = Nothing
            End If
            If Not IsNothing(Me.m_ViewBrandpack1) Then
                Me.m_ViewBrandpack1.Dispose()
                Me.m_ViewBrandpack1 = Nothing
            End If
            If Not IsNothing(m_ViewAgFirstSecond) Then
                Me.m_ViewAgFirstSecond.Dispose()
                Me.m_ViewAgFirstSecond = Nothing
            End If
            If Not IsNothing(Me.Tbl_BrandComboFisrtSecond) Then
                Me.Tbl_BrandComboFisrtSecond.Dispose()
                Me.Tbl_BrandComboFisrtSecond = Nothing
            End If
            If Not IsNothing(Me.tbl_Quantity_Combined) Then
                Me.tbl_Quantity_Combined.Dispose()
                Me.tbl_Quantity_Combined = Nothing
            End If
            If Not IsNothing(Me.tblIBIncluded) Then
                Me.tblIBIncluded.Dispose()
                Me.tblIBIncluded = Nothing
            End If
            If Not IsNothing(Me.TblItemBrandPack) Then
                Me.TblItemBrandPack.Dispose()
                Me.tblIBIncluded = Nothing
            End If
            If Not IsNothing(Me.ViewBrandpackIncluded) Then
                Me.ViewBrandpackIncluded.Dispose()
                Me.ViewBrandpackIncluded = Nothing
            End If

            If Not IsNothing(Me.ColNode) Then
                Me.ColNode = Nothing
            End If
            If Not IsNothing(Me.NVCombUnik) Then
                Me.NVCombUnik = Nothing
            End If
            If Not IsNothing(Me.HastCollCommBined) Then
                Me.HastCollCommBined = Nothing
            End If
        End Sub

        Public Sub GetData()
            Try
                'YANG ASLI SELECT DISTRIBUTOR_NAME,DISTRIBUTOR_ID,AGREEMENT_NO,AGREEMENT_DESC,QS_TREATMENT_FLAG FROM VIEW_AGREEMENT WHERE END_DATE > getdate()
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME,AGREEMENT_NO,AGREEMENT_DESC,QS_TREATMENT_FLAG,START_DATE,END_DATE FROM VIEW_AGREEMENT " & vbCrLf & _
                        " WHERE END_DATE >= " & NufarmBussinesRules.SharedClass.ShortGetDate()
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblAgreement As New DataTable("T_Agreement")
                tblAgreement.Clear() : Me.FillDataTable(tblAgreement)
                Me.m_ViewAgreement = tblAgreement.DefaultView()
                Me.m_ViewAgreement.Sort = "AGREEMENT_NO"
                Me.m_ViewAgreement.AllowDelete = False
                Me.m_ViewAgreement.AllowEdit = False
                Me.m_ViewAgreement.AllowNew = False
                'Me.CreateCommandSql("", "SELECT * FROM VIEW_AGREE_BRANDPACK_INCLUDE")
                'Dim TblBrandPack As New DataTable("T_BrandPackInclude")
                'TblBrandPack.Clear()
                'Me.FillDataTable(TblBrandPack)
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub UpdateCombinedBrand(ByVal AGREEEMENT_NO As String, ByVal AGREE_BRAND_ID As Object, ByVal COMB_AGREE_BRAND_ID As Object, _
        ByVal ClearCombined As Boolean, ByVal IsRoundUp As Boolean)
            Dim BIsRoundup As Integer = 0
            If IsRoundUp Then : BIsRoundup = 1 : End If
            Try
                Me.GetConnection() : Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                If ClearCombined = True Then
                    If IsNothing(Me.SqlCom) Then
                        Me.UpdateData("Sp_Update_ClearCombined_brand", "")
                    Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Update_ClearCombined_brand")
                    End If
                    Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEEMENT_NO, 25) ' varchar(25),
                    Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32) ' VARCHAR(32),
                    Me.AddParameter("@IsRoundUp", SqlDbType.SmallInt, BIsRoundup)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Me.SqlCom.CommandText = "Sp_Update_ClearCombined_brand"
                    Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEEMENT_NO, 25) ' varchar(25),
                    Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, COMB_AGREE_BRAND_ID, 32) 'ARCHAR(32
                    Me.AddParameter("@IsRoundUp", SqlDbType.SmallInt, BIsRoundup)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Else
                    If IsNothing(Me.SqlCom) Then
                        Me.UpdateData("Sp_Update_Combined_Brand", "")
                    Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Update_Combined_Brand")
                    End If
                    Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEEMENT_NO, 25) ' varchar(25),
                    Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32) ' VARCHAR(32),
                    Me.AddParameter("@COMB_AGREE_BRAND_ID", SqlDbType.VarChar, COMB_AGREE_BRAND_ID, 32) 'ARCHAR(32
                    'Me.AddParameter("@IsRoundUp", SqlDbType.SmallInt, BIsRoundup)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub UpdateBrandInclude(ByVal QS_FLAG As String, Optional ByRef ds As DataSet = Nothing, Optional ByVal ds4months As DataSet = Nothing, Optional ByVal BRANDPACK_IDS As Collection = Nothing, Optional ByVal IsRoundUP As Boolean = False, Optional ByVal HasChangedPrev As Boolean = False)
            Try
                Me.GetConnection()
                'If IsNothing(Me.SqlCom) Then
                '    Me.UpdateData("Sp_Update_AGREE_BRAND_ICNLUDE", "")
                'Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Update_AGREE_BRAND_ICNLUDE")
                'End If
                Query = " SET NOCOUNT ON; " & vbCrLf & _
                        "UPDATE AGREE_BRAND_INCLUDE SET AGREEMENT_NO = @AGREEMENT_NO,BRAND_ID = @BRAND_ID " & vbCrLf & _
                        ",GIVEN_DISC_PCT = @GIVEN_DISC_PCT,TARGET_Q1 = @TARGET_Q1,TARGET_Q2 = @TARGET_Q2," & vbCrLf & _
                        "TARGET_Q3 = @TARGET_Q3,TARGET_Q4 = @TARGET_Q4,TARGET_S1 = @TARGET_S1,TARGET_S2 = @TARGET_S2," & vbCrLf & _
                        " TARGET_Q1_FM = @TARGET_Q1_FM, TARGET_Q2_FM = @TARGET_Q2_FM, TARGET_Q3_FM = @TARGET_Q3_FM, TARGET_Q4_FM = @TARGET_Q4_FM, " & vbCrLf & _
                        " TARGET_Q1_PL = @TARGET_Q1_PL, TARGET_Q2_PL = @TARGET_Q2_PL, TARGET_Q3_PL = @TARGET_Q3_PL, " & vbCrLf & _
                        " TARGET_Q4_PL = @TARGET_Q4_PL, TARGET_S1_FM = @TARGET_S1_FM, TARGET_S2_FM = @TARGET_S2_FM, TARGET_S1_PL = @TARGET_S1_PL, TARGET_S2_PL = @TARGET_S2_PL, " & vbCrLf & _
                        " TARGET_FMP1=@TARGET_FMP1,TARGET_FMP2=@TARGET_FMP2,TARGET_FMP3=@TARGET_FMP3,TARGET_FMP_FM1=@TARGET_FMP_FM1,TARGET_FMP_FM2=@TARGET_FMP_FM2," & vbCrLf & _
                        " TARGET_FMP_FM3=@TARGET_FMP_FM3,TARGET_FMP_PL1=@TARGET_FMP_PL1,TARGET_FMP_PL2=@TARGET_FMP_PL2,TARGET_FMP_PL3=@TARGET_FMP_PL3, COMB_AGREE_BRAND_ID = @COMB_AGREE_BRAND_ID,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = GETDATE() WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID ; "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, Me.Agree_Brand_ID, 32) ' VARCHAR(32),
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Me.Agreement_no, 25) ' VARCHAR(25),
                Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, Me.BrandID, 7) ' VARCHAR(7),
                Me.AddParameter("@GIVEN_DISC_PCT", SqlDbType.Decimal, Me.GIvenDiscount) ' DECIMAL,
                Me.AddParameter("@TARGET_Q1", SqlDbType.Decimal, Me.Q1) ' BIGINT,
                Me.AddParameter("@TARGET_Q2", SqlDbType.Decimal, Me.Q2) ' BIGINT,
                Me.AddParameter("@TARGET_Q3", SqlDbType.Decimal, Me.Q3) ' BIGINT,
                Me.AddParameter("@TARGET_Q4", SqlDbType.Decimal, Me.Q4) ' BIGINT,
                Me.AddParameter("@TARGET_S1", SqlDbType.Decimal, Me.S1) ' BIGINT,
                Me.AddParameter("@TARGET_S2", SqlDbType.Decimal, Me.S2) ' BIGINT,
                Me.AddParameter("@TARGET_Q1_FM", SqlDbType.Decimal, Me.Q1_FM)
                Me.AddParameter("@TARGET_Q2_FM", SqlDbType.Decimal, Me.Q2_FM)
                Me.AddParameter("@TARGET_Q3_FM", SqlDbType.Decimal, Me.Q3_FM)
                Me.AddParameter("@TARGET_Q4_FM", SqlDbType.Decimal, Me.Q4_FM)
                Me.AddParameter("@TARGET_Q1_PL", SqlDbType.Decimal, Me.Q1_PL)
                Me.AddParameter("@TARGET_Q2_PL", SqlDbType.Decimal, Me.Q2_PL)
                Me.AddParameter("@TARGET_Q3_PL", SqlDbType.Decimal, Me.Q3_PL)
                Me.AddParameter("@TARGET_Q4_PL", SqlDbType.Decimal, Me.Q4_PL)
                Me.AddParameter("@TARGET_S1_FM", SqlDbType.Decimal, Me.S1_FM)
                Me.AddParameter("@TARGET_S2_FM", SqlDbType.Decimal, Me.S2_FM)
                Me.AddParameter("@TARGET_S1_PL", SqlDbType.Decimal, Me.S1_PL)
                Me.AddParameter("@TARGET_S2_PL", SqlDbType.Decimal, Me.S2_PL)

                Me.AddParameter("@TARGET_FMP1", SqlDbType.Decimal, Me.FMP1)
                Me.AddParameter("@TARGET_FMP2", SqlDbType.Decimal, Me.FMP2)
                Me.AddParameter("@TARGET_FMP3", SqlDbType.Decimal, Me.FMP3)

                Me.AddParameter("@TARGET_FMP_FM1", SqlDbType.Decimal, Me.FMP_FM1)
                Me.AddParameter("@TARGET_FMP_FM2", SqlDbType.Decimal, Me.FMP_FM2)
                Me.AddParameter("@TARGET_FMP_FM3", SqlDbType.Decimal, Me.FMP_FM3)

                Me.AddParameter("@TARGET_FMP_PL1", SqlDbType.Decimal, Me.FMP_PL1)
                Me.AddParameter("@TARGET_FMP_PL2", SqlDbType.Decimal, Me.FMP_PL2)
                Me.AddParameter("@TARGET_FMP_PL3", SqlDbType.Decimal, Me.FMP_PL3)

                Me.AddParameter("@COMB_AGREE_BRAND_ID", SqlDbType.VarChar, Me.Comb_Agree_Brand_ID, 32) ' VARCHAR(32),
                Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) '  VARCHAR(30)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If (Not IsNothing(ds)) Then
                    Me.SaveAgreeProgDiscount(QS_FLAG, True, ds, IsRoundUP, Me.Comb_Agree_Brand_ID)
                End If
                If (Not IsNothing(BRANDPACK_IDS)) Then
                    If (BRANDPACK_IDS.Count > 0) Then
                        For i As Integer = 1 To BRANDPACK_IDS.Count
                            Dim AGREE_BRANDPACK_ID As String = Agreement_no & "" & BRANDPACK_IDS(i).ToString()
                            Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_AGREE_BRANDPACK_INCLUDE")
                            Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39)
                            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Agreement_no, 25) 'AR(25),
                            Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, Agree_Brand_ID, 32) ' VARCHAR(32),
                            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_IDS(i), 14) ' VARCHAR(14),
                            Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) '
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Next
                    End If
                End If
                If Not IsNothing(ds4months) Then
                    If ds4months.HasChanges() Then
                        SaveDS4Month(ds4months.Tables(0), True)
                    End If
                End If

                'If Me.PBQ3 <= 0 And Me.PBQ4 <= 0 And Me.PBS2 <= 0 And Me.PBY <= 0 And Me.CPQ1 <= 0 And Me.CPQ2 <= 0 And Me.CPQ3 <= 0 And Me.CPS1 <= 0 Then
                'Else
                '    Me.SaveGivenProgressive()
                'End If
                If HasChangedPrev Then
                    Me.SaveGivenProgressive()
                End If
                If Not IsNothing(ds) Then
                    ds.AcceptChanges()
                End If
                If Not IsNothing(ds4months) Then
                    ds4months.AcceptChanges()
                End If
                Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub SaveBrandPack(ByVal AGREE_BRANDPACK_ID As String, ByVal AGREEMENT_NO As String, ByVal BRANDPACK_ID As String, ByVal AGREE_BRAND_ID As String)
            Try
                Me.GetConnection()
                If IsNothing(Me.SqlCom) Then
                    Me.InsertData("Sp_Insert_AGREE_BRANDPACK_INCLUDE", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_AGREE_BRANDPACK_INCLUDE")
                End If
                Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39)
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25) 'AR(25),
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32) ' VARCHAR(32),
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.CloseConnection() : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub SaveBrandPack(ByVal AGREEMENT_NO As String, ByVal AGREE_BRAND_ID As String, ByVal BRANDPACK_IDS As Collection)
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Sp_Insert_AGREE_BRANDPACK_INCLUDE", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_AGREE_BRANDPACK_INCLUDE")
                End If
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                For i As Integer = 1 To BRANDPACK_IDS.Count
                    Dim AGREE_BRANDPACK_ID As String = AGREEMENT_NO & "" & BRANDPACK_IDS(i).ToString()
                    Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39)
                    Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25) 'AR(25),
                    Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32) ' VARCHAR(32),
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_IDS(i), 14) ' VARCHAR(14),
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) '
                    Me.SqlCom.ExecuteNonQuery() : Me.ClearCommandParameters()
                Next
                Me.CommiteTransaction()
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        Public Sub DeleteBrandPack(ByVal AGREE_BRANDPACK_INCLUDE As String)
            Try
                Me.GetConnection()
                If IsNothing(Me.SqlCom) Then
                    Me.DeleteData("Sp_Delete_AGREE_BRANDPACK_INCLUDE", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Delete_AGREE_BRANDPACK_INCLUDE")
                End If
                Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_INCLUDE, 39)
                Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        Public Sub SaveBrandInclude(ByVal QS_FLAG As String, Optional ByVal ds As DataSet = Nothing, Optional ByVal DSR As DataSet = Nothing, Optional ByVal IsRoundUp As Boolean = False, Optional ByVal HasChangedPrev As Boolean = False)
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " IF NOT EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID) " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " INSERT INTO AGREE_BRAND_INCLUDE(AGREE_BRAND_ID,AGREEMENT_NO,BRAND_ID,GIVEN_DISC_PCT,TARGET_Q1,TARGET_Q2,TARGET_Q3," & vbCrLf & _
                        " TARGET_Q4,TARGET_S1,TARGET_S2,COMB_AGREE_BRAND_ID,CREATE_BY,CREATE_DATE,TARGET_Q1_FM, TARGET_Q2_FM, TARGET_Q3_FM, TARGET_Q4_FM, TARGET_Q1_PL, TARGET_Q2_PL, TARGET_Q3_PL, " & vbCrLf & _
                        " TARGET_Q4_PL, TARGET_S1_FM, TARGET_S2_FM, TARGET_S1_PL, TARGET_S2_PL,TARGET_FMP1,TARGET_FMP2,TARGET_FMP3,TARGET_FMP_FM1,TARGET_FMP_FM2,TARGET_FMP_FM3," & vbCrLf & _
                        " TARGET_FMP_PL1,TARGET_FMP_PL2,TARGET_FMP_PL3) " & vbCrLf & _
                        " VALUES(@AGREE_BRAND_ID,@AGREEMENT_NO,@BRAND_ID,@GIVEN_DISC_PCT,@TARGET_Q1,@TARGET_Q2,@TARGET_Q3,@TARGET_Q4, " & vbCrLf & _
                        " @TARGET_S1,@TARGET_S2,@COMB_AGREE_BRAND_ID,@CREATE_BY,CONVERT(VARCHAR(100),GETDATE(),101), @TARGET_Q1_FM, @TARGET_Q2_FM, @TARGET_Q3_FM, @TARGET_Q4_FM, @TARGET_Q1_PL, " & vbCrLf & _
                        " @TARGET_Q2_PL, @TARGET_Q3_PL, @TARGET_Q4_PL, @TARGET_S1_FM, @TARGET_S2_FM, @TARGET_S1_PL, @TARGET_S2_PL, " & vbCrLf & _
                        " @TARGET_FMP1,@TARGET_FMP2,@TARGET_FMP3,@TARGET_FMP_FM1,@TARGET_FMP_FM2,@TARGET_FMP_FM3," & vbCrLf & _
                        " @TARGET_FMP_PL1,@TARGET_FMP_PL2,@TARGET_FMP_PL3 ) ;" & vbCrLf & _
                        " END "
                'If IsNothing(Me.SqlCom) Then
                '    Me.InsertData("Sp_Insert_AGREE_BRAND_ICNLUDE", "")
                'Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_AGREE_BRAND_ICNLUDE")
                'End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, Me.Agree_Brand_ID, 32) 'agr VARCHAR(32),
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Me.Agreement_no, 25) ' VARCHAR(25),
                Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, Me.BrandID, 7) ' VARCHAR(7),
                Me.AddParameter("@GIVEN_DISC_PCT", SqlDbType.Decimal, Me.GIvenDiscount) ',ECIMAL,
                Me.AddParameter("@TARGET_Q1", SqlDbType.Decimal, Me.Q1)
                Me.AddParameter("@TARGET_Q2", SqlDbType.Decimal, Me.Q2) ' BIGINT,
                Me.AddParameter("@TARGET_Q3", SqlDbType.Decimal, Me.Q3) ' BIGINT,
                Me.AddParameter("@TARGET_Q4", SqlDbType.Decimal, Me.Q4) ' BIGINT,
                Me.AddParameter("@TARGET_S1", SqlDbType.Decimal, Me.S1) ' BIGINT,
                Me.AddParameter("@TARGET_S2", SqlDbType.Decimal, Me.S2) ' BIGINT,
                Me.AddParameter("@TARGET_Q1_FM", SqlDbType.Decimal, Me.Q1_FM)
                Me.AddParameter("@TARGET_Q2_FM", SqlDbType.Decimal, Me.Q2_FM)
                Me.AddParameter("@TARGET_Q3_FM", SqlDbType.Decimal, Me.Q3_FM)
                Me.AddParameter("@TARGET_Q4_FM", SqlDbType.Decimal, Me.Q4_FM)
                Me.AddParameter("@TARGET_Q1_PL", SqlDbType.Decimal, Me.Q1_PL)
                Me.AddParameter("@TARGET_Q2_PL", SqlDbType.Decimal, Me.Q2_PL)
                Me.AddParameter("@TARGET_Q3_PL", SqlDbType.Decimal, Me.Q3_PL)
                Me.AddParameter("@TARGET_Q4_PL", SqlDbType.Decimal, Me.Q4_PL)
                Me.AddParameter("@TARGET_S1_FM", SqlDbType.Decimal, Me.S1_FM)
                Me.AddParameter("@TARGET_S2_FM", SqlDbType.Decimal, Me.S2_FM)
                Me.AddParameter("@TARGET_S1_PL", SqlDbType.Decimal, Me.S1_PL)
                Me.AddParameter("@TARGET_S2_PL", SqlDbType.Decimal, Me.S2_PL)
                Me.AddParameter("@TARGET_FMP1", SqlDbType.Decimal, Me.FMP1)
                Me.AddParameter("@TARGET_FMP2", SqlDbType.Decimal, Me.FMP2)
                Me.AddParameter("@TARGET_FMP3", SqlDbType.Decimal, Me.FMP3)

                Me.AddParameter("@TARGET_FMP_FM1", SqlDbType.Decimal, Me.FMP_FM1)
                Me.AddParameter("@TARGET_FMP_FM2", SqlDbType.Decimal, Me.FMP_FM2)
                Me.AddParameter("@TARGET_FMP_FM3", SqlDbType.Decimal, Me.FMP_FM3)

                Me.AddParameter("@TARGET_FMP_PL1", SqlDbType.Decimal, Me.FMP_PL1)
                Me.AddParameter("@TARGET_FMP_PL2", SqlDbType.Decimal, Me.FMP_PL2)
                Me.AddParameter("@TARGET_FMP_PL3", SqlDbType.Decimal, Me.FMP_PL3)

                Me.AddParameter("@COMB_AGREE_BRAND_ID", SqlDbType.VarChar, Me.Comb_Agree_Brand_ID, 32) ' VARCHAR(32),
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) 'SETELAH DEBUGING MESTI DIGANTI DENGAN NAMA USER NufarmBussinesRules.User.UserLogin.UserName) ' VARCHAR(30)

                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If (Not IsNothing(ds)) Then
                    Me.SaveAgreeProgDiscount(QS_FLAG, True, ds, Me.Comb_Agree_Brand_ID)
                End If
                'save given progressive
                If Not IsNothing(DSR) Then
                    If DSR.HasChanges() Then
                        SaveDS4Month(DSR.Tables(0), True)
                    End If
                End If
                If HasChangedPrev Then
                    Me.SaveGivenProgressive()
                End If
                'If Me.PBQ3 <= 0 And Me.PBQ4 <= 0 And Me.PBS2 <= 0 And Me.PBY <= 0 And Me.CPQ1 <= 0 And Me.CPQ2 <= 0 And Me.CPQ3 <= 0 And Me.CPS1 <= 0 Then
                'Else
                '    Me.SaveGivenProgressive()
                'End If
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Sub SaveDS4Month(ByRef dt As DataTable, ByVal IsPendingTransc As Boolean)
            If Not IsPendingTransc Then
                Me.OpenConnection() : Me.BeginTransaction()
            End If
            Dim commandInsert As SqlCommand = Me.SqlConn.CreateCommand()
            Dim commandUpdate As SqlCommand = Me.SqlConn.CreateCommand()
            Dim commandDelete As SqlCommand = Me.SqlConn.CreateCommand()
            Dim insertedRows() As DataRow = dt.Select("", "", DataViewRowState.Added)
            Dim updatedRows() As DataRow = dt.Select("", "", DataViewRowState.ModifiedOriginal)
            Dim deletedRows() As DataRow = dt.Select("", "", DataViewRowState.Deleted)
            SqlDat = New SqlDataAdapter()
            If insertedRows.Length > 0 Then
                Query = " SET NOCOUNT ON; " & vbCrLf & _
                        " INSERT INTO AGREE_PROG_DISC_R(AGREEMENT_NO,PRODUCT_CATEGORY,PS_CATEGORY, ACH_METHODE, UP_TO_PCT, DISC_PCT, FLAG, Createdby, CreatedDate)" & vbCrLf & _
                        " VALUES (@AGREEMENT_NO,@PRODUCT_CATEGORY, @PS_CATEGORY, @ACH_METHODE, @UP_TO_PCT, @DISC_PCT, @FLAG, @Createdby, @CreatedDate);"
                With commandInsert
                    .CommandType = CommandType.Text
                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                    .Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 25, "AGREEMENT_NO")
                    .Parameters.Add("@PRODUCT_CATEGORY", SqlDbType.VarChar, 20, "PRODUCT_CATEGORY")
                    .Parameters.Add("@PS_CATEGORY", SqlDbType.Char, 1, "PS_CATEGORY")
                    .Parameters.Add("@ACH_METHODE", SqlDbType.VarChar, 5, "ACH_METHODE")
                    .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal, 0, "UP_TO_PCT")
                    .Parameters.Add("@DISC_PCT", SqlDbType.Decimal, 0, "DISC_PCT")
                    .Parameters.Add("@FLAG", SqlDbType.VarChar, 3, "FLAG")
                    .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                    .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                    SqlDat.InsertCommand = commandInsert
                    SqlDat.Update(insertedRows)
                    commandInsert.Parameters.Clear()
                End With
            End If
            If updatedRows.Length > 0 Then
                Query = " SET NOCOUNT ON; " & vbCrLf & _
                        " UPDATE AGREE_PROG_DISC_R SET PRODUCT_CATEGORY = @PRODUCT_CATEGORY,PS_CATEGORY =@PS_CATEGORY, " & vbCrLf & _
                        " ACH_METHODE=@ACH_METHODE, AGREEMENT_NO=@AGREEMENT_NO, UP_TO_PCT =@UP_TO_PCT, DISC_PCT=@DISC_PCT, FLAG=@FLAG," & vbCrLf & _
                        " ModifiedBy =@ModifiedBy, ModifiedDate = @ModifiedDate " & vbCrLf & _
                        " WHERE IDApp = @IDApp ;"
                With commandUpdate
                    .CommandType = CommandType.Text
                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                    .Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 25, "AGREEMENT_NO")
                    .Parameters.Add("@PS_CATEGORY", SqlDbType.Char, 1, "PS_CATEGORY")
                    .Parameters.Add("@PRODUCT_CATEGORY", SqlDbType.VarChar, 20, "PRODUCT_CATEGORY")
                    .Parameters.Add("@ACH_METHODE", SqlDbType.VarChar, 5, "ACH_METHODE")
                    .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal, 0, "UP_TO_PCT")
                    .Parameters.Add("@DISC_PCT", SqlDbType.Decimal, 0, "DISC_PCT")
                    .Parameters.Add("@FLAG", SqlDbType.VarChar, 3, "FLAG")
                    .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100, "ModifiedBy")
                    .Parameters.Add("@ModifiedDate", SqlDbType.SmallDateTime, 0, "ModifiedDate")
                    .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp").SourceVersion = DataRowVersion.Original
                    SqlDat.UpdateCommand = commandUpdate
                    SqlDat.Update(updatedRows)
                    commandUpdate.Parameters.Clear()
                End With
            End If
            If deletedRows.Length > 0 Then
                With commandDelete
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " DELETE FROM AGREE_PROG_DISC_R WHERE IDApp = @IDApp ;"
                    .CommandType = CommandType.Text
                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                    .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp").SourceVersion = DataRowVersion.Original
                    SqlDat.DeleteCommand = commandDelete
                    SqlDat.Update(deletedRows)
                    commandDelete.Parameters.Clear()
                End With
            End If
            If Not IsPendingTransc Then
                Me.CommiteTransaction() : Me.CloseConnection()
            End If
            dt.AcceptChanges()
        End Sub

        Private Sub SaveGivenProgressive()
            Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    "IF EXISTS(SELECT AGREE_BRAND_ID FROM GIVEN_PROGRESSIVE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID) " & vbCrLf & _
                    "BEGIN " & vbCrLf & _
                    "UPDATE GIVEN_PROGRESSIVE SET PBQ3 = @PBQ3,PBQ4 = @PBQ4,PBS2 = @PBS2,CPQ1 = @CPQ1,CPQ2 = @CPQ2,CPQ3 = @CPQ3,CPS1 = @CPS1," & vbCrLf & _
                    " PBF2 = @PBF2,PBF3 = @PBF3,CPF1 = @CPF1,CPF2 = @CPF2, PBY = @PBY," & vbCrLf & _
                    "MODIFY_BY = @MODIFY_BY WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID ;" & vbCrLf & _
                    "END " & vbCrLf & _
                    "ELSE " & vbCrLf & _
                    "BEGIN " & vbCrLf & _
                    "INSERT INTO GIVEN_PROGRESSIVE(AGREE_BRAND_ID,PBQ3,PBQ4,PBS2,CPQ1,CPQ2,CPQ3,CPS1,PBF2,PBF3,CPF1,CPF2,PBY,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                    "VALUES(@AGREE_BRAND_ID,@PBQ3,@PBQ4,@PBS2,@CPQ1,@CPQ2,@CPQ3,@CPS1,@PBF2,@PBF3,@CPF1,@CPF2,@PBY,@CREATE_BY,@CREATE_DATE) ;" & vbCrLf & _
                    "END "
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, Agree_Brand_ID, 32)
            Me.AddParameter("@PBQ3", SqlDbType.Decimal, Me.PBQ3)
            Me.AddParameter("@PBQ4", SqlDbType.Decimal, Me.PBQ4)
            Me.AddParameter("@PBS2", SqlDbType.Decimal, Me.PBS2)
            Me.AddParameter("@CPQ1", SqlDbType.Decimal, Me.CPQ1)
            Me.AddParameter("@CPQ2", SqlDbType.Decimal, Me.CPQ2)
            Me.AddParameter("@CPQ3", SqlDbType.Decimal, Me.CPQ3)
            Me.AddParameter("@CPS1", SqlDbType.Decimal, Me.CPS1)
            Me.AddParameter("@PBF2", SqlDbType.Decimal, Me.PBF2)
            Me.AddParameter("@PBF3", SqlDbType.Decimal, Me.PBF3)
            Me.AddParameter("@CPF1", SqlDbType.Decimal, Me.CPF1)
            Me.AddParameter("@CPF2", SqlDbType.Decimal, Me.CPF2)
            Me.AddParameter("@PBY", SqlDbType.Decimal, Me.PBY)
            Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
            Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
            Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
            Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
        End Sub
        Public Sub DeleteBrandInclude(ByVal AGREE_BRAND_ID As String, Optional ByVal CombinedBrandID As Object = Nothing)
            Try
                Me.GetConnection()
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "DELETE FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AGREE_BRAND_ID + "' ;"
                If IsNothing(Me.SqlCom) Then
                    Me.DeleteData("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(CombinedBrandID) Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                             "UPDATE AGREE_BRAND_INCLUDE SET COMB_AGREE_BRAND_ID = NULL WHERE AGREE_BRAND_ID = '" & CombinedBrandID.ToString() & "'; "
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.SqlCom.ExecuteScalar()
                End If
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                          "IF EXISTS(SELECT AGREE_BRAND_ID FROM GIVEN_PROGRESSIVE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID) " & vbCrLf & _
                          " BEGIN " & vbCrLf & _
                          " DELETE FROM GIVEN_PROGRESSIVE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID ;" & vbCrLf & _
                          " END "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Delete_AGREE_BRAND_INCLUDE")
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub DeleteAgreeProgDiscount(ByRef ds As DataSet, ByVal AgreeBrandID As String, ByVal Flag As String, ByVal mustCloseConnection As Boolean)
            Try
                If Not Flag.ToString().Contains("F") Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " DECLARE @CombAgreeBrandID VARCHAR(35) " & vbCrLf & _
                            " SET @CombAgreeBrandID = (SELECT TOP 1 COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID) ; " & vbCrLf & _
                            "  IF EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER WHERE((AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID)) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "    DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER " & vbCrLf & _
                            "    WHERE ((AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID) AND FLAG LIKE @QSY_DISC_FLAG + '%') AND LEFT_QTY = DISC_QTY;" & vbCrLf & _
                            "    DELETE FROM ACCRUED_HEADER WHERE (AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID " & vbCrLf & _
                            "    AND ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER ACRH " & vbCrLf & _
                            "    WHERE NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID)) AND FLAG LIKE @QSY_DISC_FLAG + '%';" & vbCrLf & _
                            "   IF (@CombAgreeBrandID IS NOT NULL)" & vbCrLf & _
                            "      BEGIN " & vbCrLf & _
                            "       DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER " & vbCrLf & _
                            "       WHERE ((AGREEMENT_NO + '' + BRAND_ID) = @CombAgreeBrandID) AND FLAG LIKE @QSY_DISC_FLAG + '%') AND LEFT_QTY = DISC_QTY;" & vbCrLf & _
                            "       DELETE FROM ACCRUED_HEADER WHERE (AGREEMENT_NO + '' + BRAND_ID) = @CombAgreeBrandID " & vbCrLf & _
                            "       AND ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER ACRH " & vbCrLf & _
                            "       WHERE NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID)) AND FLAG LIKE @QSY_DISC_FLAG + '%';" & vbCrLf & _
                            "      END " & vbCrLf & _
                            " END " & vbCrLf & _
                            " DELETE FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID; " & vbCrLf & _
                            " DELETE FROM AGREE_PROG_DISC_VAL WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID; " & vbCrLf & _
                            " DELETE FROM GIVEN_PROGRESSIVE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID ;" & vbCrLf & _
                            " IF (@CombAgreeBrandID IS NOT NULL) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " DELETE FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = @CombAgreeBrandID ; " & vbCrLf & _
                            " DELETE FROM GIVEN_PROGRESSIVE WHERE AGREE_BRAND_ID = @CombAgreeBrandID ;" & vbCrLf & _
                            " DELETE FROM AGREE_PROG_DISC_VAL WHERE AGREE_BRAND_ID = @CombAgreeBrandID ;" & vbCrLf & _
                            " END "
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " DECLARE @CombAgreeBrandID VARCHAR(35) " & vbCrLf & _
                            " SET @CombAgreeBrandID = (SELECT TOP 1 COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID) ; " & vbCrLf & _
                            "  IF EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER WHERE((AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID)) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "    DELETE FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER " & vbCrLf & _
                            "    WHERE ((AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID) AND FLAG LIKE @QSY_DISC_FLAG + '%') AND LEFT_QTY = DISC_QTY;" & vbCrLf & _
                            "    DELETE FROM ACHIEVEMENT_HEADER WHERE (AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID " & vbCrLf & _
                            "    AND ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER ACRH " & vbCrLf & _
                            "    WHERE NOT EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ACRH.ACH_HEADER_ID)) AND FLAG LIKE @QSY_DISC_FLAG + '%';" & vbCrLf & _
                            "   IF (@CombAgreeBrandID IS NOT NULL)" & vbCrLf & _
                            "     BEGIN " & vbCrLf & _
                            "       DELETE FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER " & vbCrLf & _
                            "       WHERE ((AGREEMENT_NO + '' + BRAND_ID) = @CombAgreeBrandID) AND FLAG LIKE @QSY_DISC_FLAG + '%') AND LEFT_QTY = DISC_QTY;" & vbCrLf & _
                            "       DELETE FROM ACHIEVEMENT_HEADER WHERE (AGREEMENT_NO + '' + BRAND_ID) = @CombAgreeBrandID " & vbCrLf & _
                            "       AND ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER ACRH " & vbCrLf & _
                            "       WHERE NOT EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ACRH.ACH_HEADER_ID)) AND FLAG LIKE @QSY_DISC_FLAG + '%';" & vbCrLf & _
                            "     END " & vbCrLf & _
                            " END "
                End If

                '" IF EXISTS(SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING BBS INNER JOIN AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                '"    ON ABI.AGREE_BRANDPACK_ID = BBS.AGREE_BRANDPACK_ID WHERE ABI.AGREE_BRAND_ID = @AGREE_BRAND_ID AND BBS.QSY_FLAG LIKE @QSY_DISC_FLAG + '%') " & vbCrLf & _
                '"  BEGIN " & vbCrLf & _
                '"    IF(@IsRoundUp = 0) " & vbCrLf & _
                '"       BEGIN " & vbCrLf & _
                '"        RAISERROR('Agreement already generated / computed',16,1);" & vbCrLf & _
                '"        RETURN;" & vbCrLf & _
                '"       END " & vbCrLf & _
                '"  END " & vbCrLf & _
                '" DELETE FROM BRND_BRANDPACK_SAVING " & vbCrLf & _
                '" WHERE BRND_B_S_ID = ANY(SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING BBS INNER JOIN AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                '" ON BBS.AGREE_BRANDPACK_ID = ABI.AGREE_BRANDPACK_ID WHERE ABI.AGREE_BRAND_ID = @AGREE_BRAND_ID AND BBS.QSY_FLAG LIKE @QSY_DISC_FLAG + '%' AND BBS.LEFT_QTY = BBS.DISC_QTY); " & vbCrLf & _
                '" DELETE FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql(CommandType.Text, Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                'Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AgreeBrandID, 32)
                Me.AddParameter("@QSY_DISC_FLAG", SqlDbType.VarChar, Flag)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Me.CommiteTransaction() : If mustCloseConnection Then : Me.CloseConnection() : End If

            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Function getGivenProgressive(ByVal AgreeBrandID As String, ByVal muscloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT * FROM GIVEN_PROGRESSIVE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AgreeBrandID, 32)
                Me.OpenConnection()
                Dim tblGivenProgressive As New DataTable("T_Progress") : tblGivenProgressive.Clear()
                Me.setDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(tblGivenProgressive)
                Me.ClearCommandParameters()
                If muscloseConnection Then
                    Me.CloseConnection()
                End If
                Return tblGivenProgressive
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function HasUsedGivenProgressive(ByVal AGREE_BRAND_ID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT 1 WHERE EXISTS(SELECT GP_ID FROM ACCRUED_HEADER WHERE AGREEMENT_NO + '' + BRAND_ID = @AGREE_BRAND_ID) " & vbCrLf & _
                        " OR EXISTS(SELECT GP_ID FROM ACHIEVEMENT_HEADER WHERE AGREEMENT_NO + '' + BRAND_ID = @AGREE_BRAND_ID) "
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32)
                Me.OpenConnection()
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
        'memilih brandpackname yang udah di branpack include
        Public Function GetItemBrandPackIncludedByBrandID(ByVal brandID As String, ByVal AGREEMENT_NO As String, ByVal mustCloseConnection As Boolean) As DataView
            Try
                Me.CreateCommandSql("Usp_Select_BrandpakNameInclude_BrandIDDefined", "")
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25) ' VARCHAR(25),
                Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, brandID, 7)
                'lst.DataSource = Nothing
                tblIBIncluded = New DataTable("T_IBIncluded")
                tblIBIncluded.Clear() : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(tblIBIncluded) : Me.ClearCommandParameters()
                'Me.FillDataTable(tblIBIncluded)
                Me.ViewBrandpackIncluded = tblIBIncluded.DefaultView()
                Me.ViewBrandpackIncluded.Sort = "BRANDPACK_ID"
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
                'lst.DataSource = Me.ViewBrandpackIncluded
                'lst.DisplayMember = "BRANDPACK_NAME"
                'lst.ValueMember = "BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return ViewBrandpackIncluded
        End Function
        'memilih item brandpack yang belum ada di brandpack include
        Public Sub GetItemBrandPackByBrandName(ByVal BrandId As String, ByVal AgreementNo As String, ByVal lst As System.Windows.Forms.ListBox)

            Try
                Me.CreateCommandSql("Usp_Select_BPNAMENOTIN_AGREE_BRANDPACK_INCLUDE", "")
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNo, 25) ' VARCHAR(25),
                Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, BrandId, 7)
                lst.DataSource = Nothing
                lst.Items.Clear()
                Dim TblItemBrandPack As New DataTable("T_IBrandPack")
                TblItemBrandPack.Clear()
                Me.FillDataTable(TblItemBrandPack)
                Me.m_ViewBrandpack1 = TblItemBrandPack.DefaultView()
                Me.m_ViewBrandpack1.Sort = "BRANDPACK_NAME"
                lst.DataSource = Me.m_ViewBrandpack1
                lst.DisplayMember = "BRANDPACK_NAME"
                lst.ValueMember = "BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub SaveAgreeProgDiscount(ByVal QS_FLAG As String, ByVal IsPending_Transaction As Boolean, ByRef ds As DataSet, Optional ByVal IsRoundUp As Boolean = False, Optional ByRef CombAgreeBrandID As Object = Nothing)
            Try
                If CombAgreeBrandID Is Nothing Then : CombAgreeBrandID = DBNull.Value : End If
                If Not IsPending_Transaction Then
                    Me.GetConnection() : Me.SqlDat = New SqlDataAdapter()
                    Me.OpenConnection() : Me.BeginTransaction()
                End If
                ''===================== PROG DISC VOLUME===========================================

                Dim CommandInsert As SqlCommand = Me.SqlConn.CreateCommand()
                With CommandInsert
                    .CommandType = CommandType.Text
                    If QS_FLAG.ToUpper().Contains("F") Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "IF (LEN(@QSY_DISC_FLAG) > 1) " & vbCrLf & _
                                " BEGIN " & vbCrLf & _
                                "   IF EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER AC WHERE ((AC.AGREEMENT_NO + AC.BRAND_ID) = @AGREE_BRAND_ID) AND " & vbCrLf & _
                                "             EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = AC.ACH_HEADER_ID) " & vbCrLf & _
                                "             AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                "      BEGIN " & vbCrLf & _
                               "        RAISERROR('Discount already used in OA',16,1);" & vbCrLf & _
                                "       RETURN;" & vbCrLf & _
                                "      END " & vbCrLf & _
                                "   ELSE IF EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER AC WHERE ((AC.AGREEMENT_NO + AC.BRAND_ID) = @AGREE_BRAND_ID) " & vbCrLf & _
                                "                   AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                "       BEGIN " & vbCrLf & _
                                "         DELETE FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER " & vbCrLf & _
                                "         WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG));" & vbCrLf & _
                                "         DELETE FROM ACHIEVEMENT_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                                "         AND ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER ACRH " & vbCrLf & _
                                "         WHERE NOT EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ACRH.ACH_HEADER_ID)) ;" & vbCrLf & _
                                "       END " & vbCrLf & _
                                " END " & vbCrLf & _
                                "INSERT INTO AGREE_PROG_DISC(AGREE_BRAND_ID,PRGSV_DISC_PCT,QSY_DISC_FLAG,UP_TO_PCT,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                                "VALUES(@AGREE_BRAND_ID,@PRGSV_DISC_PCT,UPPER(@QSY_DISC_FLAG),@UP_TO_PCT,@CREATE_BY,@CREATE_DATE);"
                    Else
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "IF (LEN(@QSY_DISC_FLAG) > 1) " & vbCrLf & _
                                " BEGIN " & vbCrLf & _
                                "   IF EXISTS(SELECT ACH_HEADER_ID FROM ACCRUED_HEADER AC WHERE ((AC.AGREEMENT_NO + AC.BRAND_ID) = @AGREE_BRAND_ID) AND " & vbCrLf & _
                                "             EXISTS(SELECT ACH_HEADER_ID FROM ACCRUED_DETAIL WHERE ACH_HEADER_ID = AC.ACH_HEADER_ID AND CAN_RELEASE = 1 " & vbCrLf & _
                                "             AND LEFT_QTY < DISC_QTY)) AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                "      BEGIN " & vbCrLf & _
                               "        RAISERROR('Discount already used in OA',16,1);" & vbCrLf & _
                                "       RETURN;" & vbCrLf & _
                                "      END " & vbCrLf & _
                                "   ELSE IF EXISTS(SELECT ACH_HEADER_ID FROM ACCRUED_HEADER AC WHERE ((AC.AGREEMENT_NO + AC.BRAND_ID) = @AGREE_BRAND_ID) " & vbCrLf & _
                                "                   AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                "       BEGIN " & vbCrLf & _
                                "         DELETE FROM ACCRUED_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACCRUED_HEADER " & vbCrLf & _
                                "         WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG)) AND LEFT_QTY = DISC_QTY ;" & vbCrLf & _
                                "         DELETE FROM ACCRUED_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                                "         AND ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACCRUED_HEADER ACRH " & vbCrLf & _
                                "         WHERE NOT EXISTS(SELECT ACH_HEADER_ID FROM ACCRUED_DETAIL WHERE ACH_HEADER_ID = ACRH.ACH_HEADER_ID)) ;" & vbCrLf & _
                                "       END " & vbCrLf & _
                                " END " & vbCrLf & _
                                "INSERT INTO AGREE_PROG_DISC(AGREE_BRAND_ID,PRGSV_DISC_PCT,QSY_DISC_FLAG,UP_TO_PCT,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                                "VALUES(@AGREE_BRAND_ID,@PRGSV_DISC_PCT,UPPER(@QSY_DISC_FLAG),@UP_TO_PCT,@CREATE_BY,@CREATE_DATE);"
                    End If

                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                End With
                Dim CommandUpdate As SqlCommand = Me.SqlConn.CreateCommand()
                With CommandUpdate
                    .CommandType = CommandType.Text
                    If QS_FLAG.ToUpper().Contains("F") Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                 "IF (LEN(@QSY_DISC_FLAG) > 1) " & vbCrLf & _
                                 " BEGIN " & vbCrLf & _
                                 "  IF EXISTS(SELECT AC.ACH_HEADER_ID FROM ACHIEVEMENT_HEADER AC WHERE((AC.AGREEMENT_NO + '' + AC.BRAND_ID) = @AGREE_BRAND_ID) AND " & vbCrLf & _
                                 "             EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = AC.ACH_HEADER_ID) AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                 "   BEGIN " & vbCrLf & _
                                 "    IF(@IsRoundUp = 0) " & vbCrLf & _
                                 "       BEGIN " & vbCrLf & _
                                 "        RAISERROR('Discount already used in OA',16,1);" & vbCrLf & _
                                 "        RETURN;" & vbCrLf & _
                                 "       END " & vbCrLf & _
                                 "   END " & vbCrLf & _
                                 " END " & vbCrLf & _
                                 " IF EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER AC WHERE ((AC.AGREEMENT_NO + AC.BRAND_ID) = @AGREE_BRAND_ID) " & vbCrLf & _
                                 "            AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                 "   BEGIN " & vbCrLf & _
                                 "      DELETE FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER " & vbCrLf & _
                                 "      WHERE (AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG));" & vbCrLf & _
                                 "      DELETE FROM ACHIEVEMENT_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                                 "      AND ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER ACRH " & vbCrLf & _
                                 "      WHERE NOT EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ACRH.ACH_HEADER_ID)) ;" & vbCrLf & _
                                 "   END " & vbCrLf & _
                                 "UPDATE AGREE_PROG_DISC SET AGREE_BRAND_ID = @AGREE_BRAND_ID,QSY_DISC_FLAG = UPPER(@QSY_DISC_FLAG)," & _
                                 "PRGSV_DISC_PCT = @PRGSV_DISC_PCT,UP_TO_PCT = @UP_TO_PCT,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " & vbCrLf & _
                                 " WHERE UNIQUE_ID = CAST(@UNIQUE_ID AS VARCHAR(100));"
                    Else
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "IF (LEN(@QSY_DISC_FLAG) > 1) " & vbCrLf & _
                                " BEGIN " & vbCrLf & _
                                "  IF EXISTS(SELECT AC.ACH_HEADER_ID FROM ACCRUED_HEADER AC WHERE((AC.AGREEMENT_NO + '' + AC.BRAND_ID) = @AGREE_BRAND_ID) AND " & vbCrLf & _
                                "             EXISTS(SELECT ACH_HEADER_ID FROM ACCRUED_DETAIL WHERE ACH_HEADER_ID = AC.ACH_HEADER_ID AND CAN_RELEASE = 1 AND LEFT_QTY < DISC_QTY) AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                "   BEGIN " & vbCrLf & _
                                "    IF(@IsRoundUp = 0) " & vbCrLf & _
                                "       BEGIN " & vbCrLf & _
                                "        RAISERROR('Discount already used in OA',16,1);" & vbCrLf & _
                                "        RETURN;" & vbCrLf & _
                                "       END " & vbCrLf & _
                                "   END " & vbCrLf & _
                                " END " & vbCrLf & _
                                " IF EXISTS(SELECT ACH_HEADER_ID FROM ACCRUED_HEADER AC WHERE ((AC.AGREEMENT_NO + AC.BRAND_ID) = @AGREE_BRAND_ID) " & vbCrLf & _
                                "            AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                "   BEGIN " & vbCrLf & _
                                "      DELETE FROM ACCRUED_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACCRUED_HEADER " & vbCrLf & _
                                "      WHERE (AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG)) AND LEFT_QTY = DISC_QTY ;" & vbCrLf & _
                                "      DELETE FROM ACCRUED_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                                "      AND ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACCRUED_HEADER ACRH " & vbCrLf & _
                                "      WHERE NOT EXISTS(SELECT ACH_HEADER_ID FROM ACCRUED_DETAIL WHERE ACH_HEADER_ID = ACRH.ACH_HEADER_ID)) ;" & vbCrLf & _
                                "   END " & vbCrLf & _
                                "UPDATE AGREE_PROG_DISC SET AGREE_BRAND_ID = @AGREE_BRAND_ID,QSY_DISC_FLAG = UPPER(@QSY_DISC_FLAG)," & _
                                "PRGSV_DISC_PCT = @PRGSV_DISC_PCT,UP_TO_PCT = @UP_TO_PCT,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " & vbCrLf & _
                                " WHERE UNIQUE_ID = CAST(@UNIQUE_ID AS VARCHAR(100));"
                    End If

                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                End With
                Dim CommandDelete As SqlCommand = Me.SqlConn.CreateCommand()
                With CommandDelete
                    .CommandType = CommandType.Text
                    If QS_FLAG.ToUpper().Contains("F") Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                  "  IF EXISTS(SELECT AC.ACH_HEADER_ID FROM ACHIEVEMENT_HEADER AC WHERE (AC.AGREEMENT_NO + '' + AC.BRAND_ID) = @AGREE_BRAND_ID AND " & vbCrLf & _
                                  "             EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = AC.ACH_HEADER_ID) AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                  "   BEGIN " & vbCrLf & _
                                  "    IF(@IsRoundUp = 0) " & vbCrLf & _
                                  "       BEGIN " & vbCrLf & _
                                  "        RAISERROR('BrandPack already used in OA',16,1);" & vbCrLf & _
                                  "        RETURN;" & vbCrLf & _
                                  "       END " & vbCrLf & _
                                  "   END " & vbCrLf & _
                                  "    DELETE FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER " & vbCrLf & _
                                  "    WHERE (AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID AND FLAG =  UPPER(@QSY_DISC_FLAG));" & vbCrLf & _
                                  "    DELETE FROM ACHIEVEMENT_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                                  "    AND ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER ACRH " & vbCrLf & _
                                  "    WHERE NOT EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ACRH.ACH_HEADER_ID)) ;" & vbCrLf & _
                                  " IF (@CombAgreeBrandID IS NOT NULL)" & vbCrLf & _
                                  "      BEGIN " & vbCrLf & _
                                  "        DELETE FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER " & vbCrLf & _
                                  "        WHERE ((AGREEMENT_NO + '' + BRAND_ID) = @CombAgreeBrandID) AND FLAG = UPPER(@QSY_DISC_FLAG)) ;" & vbCrLf & _
                                   "       DELETE FROM ACHIEVEMENT_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                                  "        AND ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER ACRH " & vbCrLf & _
                                  "        WHERE NOT EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = ACRH.ACH_HEADER_ID)) ;" & vbCrLf & _
                                  "      END " & vbCrLf & _
                                  " DELETE FROM AGREE_PROG_DISC WHERE UNIQUE_ID = CAST(@UNIQUE_ID AS  VARCHAR(100));"
                    Else
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "  IF EXISTS(SELECT AC.ACH_HEADER_ID FROM ACCRUED_HEADER AC WHERE (AC.AGREEMENT_NO + '' + AC.BRAND_ID) = @AGREE_BRAND_ID AND " & vbCrLf & _
                                "             EXISTS(SELECT ACH_HEADER_ID FROM ACCRUED_DETAIL WHERE ACH_HEADER_ID = AC.ACH_HEADER_ID AND CAN_RELEASE = 1 AND LEFT_QTY < DISC_QTY) AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                "   BEGIN " & vbCrLf & _
                                "   IF(@IsRoundUp = 0) " & vbCrLf & _
                                "       BEGIN " & vbCrLf & _
                                "        RAISERROR('BrandPack already used in OA',16,1);" & vbCrLf & _
                                "        RETURN;" & vbCrLf & _
                                "       END " & vbCrLf & _
                                "   END " & vbCrLf & _
                                "    DELETE FROM ACCRUED_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACCRUED_HEADER " & vbCrLf & _
                                "    WHERE (AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID AND FLAG =  UPPER(@QSY_DISC_FLAG)) AND LEFT_QTY = DISC_QTY ;" & vbCrLf & _
                                "         DELETE FROM ACCRUED_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                                "         AND ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACCRUED_HEADER ACRH " & vbCrLf & _
                                "         WHERE NOT EXISTS(SELECT ACH_HEADER_ID FROM ACCRUED_DETAIL WHERE ACH_HEADER_ID = ACRH.ACH_HEADER_ID)) ;" & vbCrLf & _
                                " IF (@CombAgreeBrandID IS NOT NULL)" & vbCrLf & _
                                "      BEGIN " & vbCrLf & _
                                "        DELETE FROM ACCRUED_DETAIL WHERE ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACCRUED_HEADER " & vbCrLf & _
                                "        WHERE ((AGREEMENT_NO + '' + BRAND_ID) = @CombAgreeBrandID) AND FLAG = UPPER(@QSY_DISC_FLAG)) AND LEFT_QTY = DISC_QTY ;" & vbCrLf & _
                                 "       DELETE FROM ACCRUED_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                                "        AND ACH_HEADER_ID = ANY(SELECT ACH_HEADER_ID FROM ACCRUED_HEADER ACRH " & vbCrLf & _
                                "        WHERE NOT EXISTS(SELECT ACH_HEADER_ID FROM ACCRUED_DETAIL WHERE ACH_HEADER_ID = ACRH.ACH_HEADER_ID)) ;" & vbCrLf & _
                                "      END " & vbCrLf & _
                                " DELETE FROM AGREE_PROG_DISC WHERE UNIQUE_ID = CAST(@UNIQUE_ID AS  VARCHAR(100));"
                    End If
                    .CommandType = CommandType.Text
                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                End With
                Me.SqlDat.InsertCommand = CommandInsert : Me.SqlDat.UpdateCommand = CommandUpdate
                Me.SqlDat.DeleteCommand = CommandDelete
                Dim tblName As String = "T_" & QS_FLAG & "_Flag"
                Dim NewRows() As DataRow = Nothing

                ''============TABLE T_Q/S_FLAG====================
                NewRows = ds.Tables(tblName).Select("", "", DataViewRowState.Added)
                If (NewRows.Length > 0) Then
                    With CommandInsert
                        .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                        .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal).SourceColumn = "PRGSV_DISC_PCT"
                        .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                        .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal).SourceColumn = "UP_TO_PCT"
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                        .Parameters.Add("@CREATE_DATE", SqlDbType.DateTime).Value = NufarmBussinesRules.SharedClass.ServerDate()
                    End With
                    Me.SqlDat.Update(NewRows)
                    CommandInsert.Parameters.Clear() 'Clearkan parameter
                End If

                ''=========== TABLE T_Y_Flag =====================================
                NewRows = ds.Tables("T_Y_Flag").Select("", "", DataViewRowState.Added)
                If (NewRows.Length > 0) Then
                    With CommandInsert
                        .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                        .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal).SourceColumn = "PRGSV_DISC_PCT"
                        .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                        .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal).SourceColumn = "UP_TO_PCT"
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                        .Parameters.Add("@CREATE_DATE", SqlDbType.DateTime).Value = NufarmBussinesRules.SharedClass.ServerDate()
                    End With
                    Me.SqlDat.Update(NewRows)
                    CommandInsert.Parameters.Clear() 'Clearkan parameter
                End If

                ''============TABLE T_Q/S_FLAG====================
                Dim UpdatedRows() As DataRow = ds.Tables(tblName).Select("", "", DataViewRowState.ModifiedCurrent Or DataViewRowState.ModifiedOriginal)
                If (UpdatedRows.Length > 0) Then
                    With CommandUpdate
                        .Parameters.Add("@UNIQUE_ID", SqlDbType.UniqueIdentifier).SourceColumn = "UNIQUE_ID"
                        .Parameters("@UNIQUE_ID").SourceVersion = DataRowVersion.Original
                        .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                        .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal).SourceColumn = "PRGSV_DISC_PCT"
                        '.Parameters("@PRGSV_DISC_PCT").Scale = 3
                        .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                        .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal).SourceColumn = "UP_TO_PCT"
                        '.Parameters("@UP_TO_PCT").Scale = 3
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.DateTime).Value = NufarmBussinesRules.SharedClass.ServerDate()
                        If (IsRoundUp) Then
                            .Parameters.Add("@IsRoundUp", SqlDbType.SmallInt).Value = 1
                        Else
                            .Parameters.Add("@IsRoundUp", SqlDbType.SmallInt).Value = 0
                        End If
                    End With
                    SqlDat.Update(UpdatedRows)
                    CommandUpdate.Parameters.Clear()
                End If

                ''=========== TABLE T_Y_Flag =====================================
                UpdatedRows = ds.Tables("T_Y_Flag").Select("", "", DataViewRowState.ModifiedCurrent Or DataViewRowState.ModifiedOriginal)
                If (UpdatedRows.Length > 0) Then
                    CommandUpdate.Parameters.Clear()
                    With CommandUpdate
                        .Parameters.Add("@UNIQUE_ID", SqlDbType.UniqueIdentifier).SourceColumn = "UNIQUE_ID"
                        .Parameters("@UNIQUE_ID").SourceVersion = DataRowVersion.Original
                        .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                        .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal).SourceColumn = "PRGSV_DISC_PCT"
                        '.Parameters("@PRGSV_DISC_PCT").Scale = 3
                        .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                        .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal).SourceColumn = "UP_TO_PCT"
                        '.Parameters("@UP_TO_PCT").Scale = 3
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.DateTime).Value = NufarmBussinesRules.SharedClass.ServerDate()
                        If (IsRoundUp) Then
                            .Parameters.Add("@IsRoundUp", SqlDbType.SmallInt).Value = 1
                        Else
                            .Parameters.Add("@IsRoundUp", SqlDbType.SmallInt).Value = 0
                        End If
                    End With
                    SqlDat.Update(UpdatedRows)
                    CommandUpdate.Parameters.Clear()
                End If

                ''============TABLE T_Q/S_FLAG====================
                Dim DeletedRows() As DataRow = ds.Tables(tblName).Select("", "", DataViewRowState.Deleted)
                If (DeletedRows.Length > 0) Then
                    With CommandDelete
                        .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                        .Parameters("@AGREE_BRAND_ID").SourceVersion = DataRowVersion.Original
                        .Parameters.Add("@UNIQUE_ID", SqlDbType.UniqueIdentifier).SourceColumn = "UNIQUE_ID"
                        .Parameters("@UNIQUE_ID").SourceVersion = DataRowVersion.Original
                        .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                        If (IsRoundUp) Then
                            .Parameters.Add("@IsRoundUp", SqlDbType.SmallInt).Value = 1
                        Else
                            .Parameters.Add("@IsRoundUp", SqlDbType.SmallInt).Value = 0
                        End If
                        .Parameters.Add("@CombAgreeBrandID", SqlDbType.VarChar, 32).Value = CombAgreeBrandID
                    End With
                    SqlDat.Update(DeletedRows)
                    CommandDelete.Parameters.Clear()
                End If

                ''=========== TABLE T_Y_Flag =====================================
                DeletedRows = ds.Tables("T_Y_Flag").Select("", "", DataViewRowState.Deleted)
                If DeletedRows.Length > 0 Then
                    CommandDelete.Parameters.Clear()
                    With CommandDelete
                        .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                        .Parameters("@AGREE_BRAND_ID").SourceVersion = DataRowVersion.Original
                        .Parameters.Add("@UNIQUE_ID", SqlDbType.UniqueIdentifier).SourceColumn = "UNIQUE_ID"
                        .Parameters("@UNIQUE_ID").SourceVersion = DataRowVersion.Original
                        .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                        If (IsRoundUp) Then
                            .Parameters.Add("@IsRoundUp", SqlDbType.SmallInt).Value = 1
                        Else
                            .Parameters.Add("@IsRoundUp", SqlDbType.SmallInt).Value = 0
                        End If
                        .Parameters.Add("@CombAgreeBrandID", SqlDbType.VarChar, 32).Value = CombAgreeBrandID
                    End With
                    SqlDat.Update(DeletedRows)
                    CommandDelete.Parameters.Clear()
                End If

                ''============END PROG DISC VOLUME===============================================================
                ''===============================================================================================


                ''===================== PROG DISC VAL===========================================
                tblName = "T_" & QS_FLAG & "_Flag_Val"
                If ds.Tables.Contains(tblName) Then
                    If ds.Tables(tblName).GetChanges().Rows.Count() > 0 Then
                        ''============TABLE T_Q/S_FLAG====================
                        Query = " SET NOCOUNT ON;" & vbCrLf & _
                               " IF EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER AC WHERE ((AC.AGREEMENT_NO + AC.BRAND_ID) = @AGREE_BRAND_ID) AND " & vbCrLf & _
                               "             EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = AC.ACHIEVEMENT_ID AND CAN_RELEASE = 1 " & vbCrLf & _
                               "             AND LEFT_QTY < DISC_QTY) AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                               "      BEGIN " & vbCrLf & _
                               "        RAISERROR('Discount already used in OA',16,1);" & vbCrLf & _
                               "       RETURN;" & vbCrLf & _
                               "      END " & vbCrLf & _
                               " ELSE IF EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER AC WHERE ((AC.AGREEMENT_NO + AC.BRAND_ID) = @AGREE_BRAND_ID) " & vbCrLf & _
                               "                   AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                               "       BEGIN " & vbCrLf & _
                               "         DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER " & vbCrLf & _
                               "         WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG)) AND LEFT_QTY = DISC_QTY ;" & vbCrLf & _
                               "         DELETE FROM ACCRUED_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                               "         AND ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER ACRH " & vbCrLf & _
                               "         WHERE NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID)) ;" & vbCrLf & _
                               "       END " & vbCrLf & _
                               " INSERT INTO AGREE_PROG_DISC_VAL(AGREE_BRAND_ID,PRGSV_DISC_PCT,QSY_DISC_FLAG,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                               " VALUES(@AGREE_BRAND_ID,@PRGSV_DISC_PCT,UPPER(@QSY_DISC_FLAG),@CREATE_BY,@CREATE_DATE); "
                        NewRows = ds.Tables(tblName).Select("", "", DataViewRowState.Added)
                        If NewRows.Length > 0 Then
                            With CommandInsert
                                .CommandText = Query
                                If IsNothing(.Transaction) Then
                                    .Transaction = Me.SqlTrans
                                End If
                                .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                                .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal).SourceColumn = "PRGSV_DISC_PCT"
                                .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                                .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                                .Parameters.Add("@CREATE_DATE", SqlDbType.DateTime).Value = NufarmBussinesRules.SharedClass.ServerDate()
                            End With
                            Me.SqlDat.Update(NewRows)
                            CommandInsert.Parameters.Clear()
                        End If

                        ''============TABLE T_Y_Flag_Val====================
                        NewRows = ds.Tables("T_Y_Flag_Val").Select("", "", DataViewRowState.Added)
                        If NewRows.Length > 0 Then
                            With CommandInsert
                                .CommandText = Query
                                If IsNothing(.Transaction) Then
                                    .Transaction = Me.SqlTrans
                                End If
                                .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                                .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal).SourceColumn = "PRGSV_DISC_PCT"
                                .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                                .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                                .Parameters.Add("@CREATE_DATE", SqlDbType.DateTime).Value = NufarmBussinesRules.SharedClass.ServerDate()
                            End With
                            Me.SqlDat.Update(NewRows)
                            CommandInsert.Parameters.Clear()
                        End If

                        ''============TABLE T_Q/S_FLAG====================
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                  " IF EXISTS(SELECT AC.ACHIEVEMENT_ID FROM ACCRUED_HEADER AC WHERE((AC.AGREEMENT_NO + '' + AC.BRAND_ID) = @AGREE_BRAND_ID) AND " & vbCrLf & _
                                  "             EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = AC.ACHIEVEMENT_ID AND CAN_RELEASE = 1 AND LEFT_QTY < DISC_QTY) AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                  "   BEGIN " & vbCrLf & _
                                  "        RAISERROR('Discount already used in OA',16,1);" & vbCrLf & _
                                  "        RETURN;" & vbCrLf & _
                                  "   END " & vbCrLf & _
                                  " IF EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER AC WHERE ((AC.AGREEMENT_NO + AC.BRAND_ID) = @AGREE_BRAND_ID) " & vbCrLf & _
                                  "            AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                                  "   BEGIN " & vbCrLf & _
                                  "      DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER " & vbCrLf & _
                                  "      WHERE (AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG)) AND LEFT_QTY = DISC_QTY ;" & vbCrLf & _
                                  "      DELETE FROM ACCRUED_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                                  "      AND ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER ACRH " & vbCrLf & _
                                  "      WHERE NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID)) ;" & vbCrLf & _
                                  "   END " & vbCrLf & _
                                  " UPDATE AGREE_PROG_DISC SET QSY_DISC_FLAG = UPPER(@QSY_DISC_FLAG)," & _
                                  " PRGSV_DISC_PCT = @PRGSV_DISC_PCT,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " & vbCrLf & _
                                  " WHERE IDApp = @IDApp; "
                        UpdatedRows = ds.Tables(tblName).Select("", "", DataViewRowState.ModifiedOriginal)
                        If UpdatedRows.Length > 0 Then
                            With CommandUpdate
                                .CommandText = Query
                                If IsNothing(.Transaction) Then
                                    .Transaction = Me.SqlTrans
                                End If
                                .Parameters.Add("@IDApp", SqlDbType.Int).SourceColumn = "IDApp"
                                .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                                .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal).SourceColumn = "PRGSV_DISC_PCT"
                                .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                                .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                                .Parameters.Add("@MODIFY_DATE", SqlDbType.DateTime).Value = NufarmBussinesRules.SharedClass.ServerDate()
                            End With
                            SqlDat.Update(UpdatedRows)
                            CommandUpdate.Parameters.Clear()
                        End If

                        ''============TABLE T_Y_Flag_Val====================
                        UpdatedRows = ds.Tables("T_Y_Flag_Val").Select("", "", DataViewRowState.ModifiedOriginal)
                        If UpdatedRows.Length > 0 Then
                            With CommandUpdate
                                .CommandText = Query
                                If IsNothing(.Transaction) Then
                                    .Transaction = Me.SqlTrans
                                End If
                                .Parameters.Add("@IDApp", SqlDbType.Int).SourceColumn = "IDApp"
                                .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                                .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal).SourceColumn = "PRGSV_DISC_PCT"
                                .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                                .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                                .Parameters.Add("@MODIFY_DATE", SqlDbType.DateTime).Value = NufarmBussinesRules.SharedClass.ServerDate()
                            End With
                            SqlDat.Update(UpdatedRows)
                            CommandUpdate.Parameters.Clear()
                        End If

                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                               "  IF EXISTS(SELECT AC.ACHIEVEMENT_ID FROM ACCRUED_HEADER AC WHERE (AC.AGREEMENT_NO + '' + AC.BRAND_ID) = @AGREE_BRAND_ID AND " & vbCrLf & _
                               "             EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = AC.ACHIEVEMENT_ID AND CAN_RELEASE = 1 AND LEFT_QTY < DISC_QTY) AND FLAG = UPPER(@QSY_DISC_FLAG)) " & vbCrLf & _
                               "   BEGIN " & vbCrLf & _
                               "    DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER " & vbCrLf & _
                               "        WHERE (AGREEMENT_NO + '' + BRAND_ID) = @AGREE_BRAND_ID AND FLAG =  UPPER(@QSY_DISC_FLAG)) AND LEFT_QTY = DISC_QTY ;" & vbCrLf & _
                               "    DELETE FROM ACCRUED_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                               "         AND ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER ACRH " & vbCrLf & _
                               "         WHERE NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID)) ;" & vbCrLf & _
                               "   END " & vbCrLf & _
                               " IF (@CombAgreeBrandID IS NOT NULL)" & vbCrLf & _
                               "      BEGIN " & vbCrLf & _
                               "        DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER " & vbCrLf & _
                               "        WHERE ((AGREEMENT_NO + '' + BRAND_ID) = @CombAgreeBrandID) AND FLAG = UPPER(@QSY_DISC_FLAG)) AND LEFT_QTY = DISC_QTY ;" & vbCrLf & _
                               "       DELETE FROM ACCRUED_HEADER WHERE (AGREEMENT_NO + BRAND_ID) = @AGREE_BRAND_ID AND FLAG = UPPER(@QSY_DISC_FLAG) " & vbCrLf & _
                               "        AND ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER ACRH " & vbCrLf & _
                               "        WHERE NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID)) ;" & vbCrLf & _
                               "      END " & vbCrLf & _
                               " DELETE FROM AGREE_PROG_DISC_VAL WHERE IDApp = @IDApp ;"

                        ''============TABLE T_Q/S_FLAG====================
                        DeletedRows = ds.Tables(tblName).Select("", "", DataViewRowState.Deleted)
                        If DeletedRows.Length > 0 Then
                            With CommandDelete
                                .CommandText = Query
                                If IsNothing(.Transaction) Then
                                    .Transaction = Me.SqlTrans
                                End If
                                .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                                .Parameters("@AGREE_BRAND_ID").SourceVersion = DataRowVersion.Original
                                .Parameters.Add("@IDApp", SqlDbType.UniqueIdentifier).SourceColumn = "IDApp"
                                .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                                .Parameters("@QSY_DISC_FLAG").SourceVersion = DataRowVersion.Original
                                .Parameters.Add("@CombAgreeBrandID", SqlDbType.VarChar, 32).Value = CombAgreeBrandID
                            End With
                            SqlDat.Update(DeletedRows)
                            CommandDelete.Parameters.Clear()
                        End If

                        ''============TABLE T_Y_Flag_Val====================
                        DeletedRows = ds.Tables("T_Y_Flag_Val").Select("", "", DataViewRowState.Deleted)
                        If DeletedRows.Length > 0 Then
                            With CommandDelete
                                .CommandText = Query
                                If IsNothing(.Transaction) Then
                                    .Transaction = Me.SqlTrans
                                End If
                                .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32).SourceColumn = "AGREE_BRAND_ID"
                                .Parameters("@AGREE_BRAND_ID").SourceVersion = DataRowVersion.Original
                                .Parameters.Add("@IDApp", SqlDbType.UniqueIdentifier).SourceColumn = "IDApp"
                                .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                                .Parameters("@QSY_DISC_FLAG").SourceVersion = DataRowVersion.Original
                                .Parameters.Add("@CombAgreeBrandID", SqlDbType.VarChar, 32).Value = CombAgreeBrandID
                            End With
                            SqlDat.Update(DeletedRows)
                            CommandDelete.Parameters.Clear()
                        End If
                    End If
                End If
                ''======================== END PROG DISC VALUE ====================================================
                ''===============================================================================================
                If Not IsPending_Transaction Then
                    Me.CommiteTransaction() : Me.CloseConnection()
                End If
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Function getSchemaR(ByVal AgreementNo As String, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                "SELECT A.*,IDRow = A.AGREEMENT_NO+A.PRODUCT_CATEGORY + A.PS_CATEGORY+A.FLAG, " & vbCrLf & _
                " HasRef = CASE " & vbCrLf & _
                " WHEN EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND FLAG = A.FLAG) THEN 1 ELSE 0 END " & vbCrLf & _
                " FROM AGREE_PROG_DISC_R A WHERE A.AGREEMENT_NO = @AGREEMENT_NO;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNo, 25)
                Dim tbl As New DataTable("TblProgressive")
                Me.OpenConnection()
                setDataAdapter(Me.SqlCom).Fill(tbl)
                ''cek reference
                'Query = "SET NOCOUNT ON; " & vbCrLf & _
                '"SELECT 1 WHERE EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO);"
                'Me.ResetCommandText(CommandType.Text, Query)
                'Dim retval As Object = Me.SqlCom.ExecuteScalar()
                'If Not IsNothing(retval) Then
                '    If CInt(retval) > 0 Then
                '        hasRef = True
                '    End If
                'End If
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return tbl
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Function HasGeneratedDiscountFMP(ByVal AgreeBrandID As String, ByVal Flag As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT IDApp FROM ACHIEVEMENT_HEADER WHERE AGREEMENT_NO + BRAND_ID = @AGREE_BRAND_ID AND FLAG = @FLAG);"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql(CommandType.Text, Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AgreeBrandID, 32)
                Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then : Return True : End If
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        'Public Function HasgeneratedDiscount(ByVal AGREE_BRAND_ID As String, ByVal FLAG As String) As Boolean
        '    Try
        '        Query = "SET NOCOUNT ON;" & vbCrLf & _
        '                 "DECLARE @V_RETVAL INT;" & vbCrLf & _
        '                 "SET @V_RETVAL = 0; " & vbCrLf & _
        '                 "IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ACCRUED_HEADER' AND TABLE_TYPE = 'base table') " & vbCrLf & _
        '                 " BEGIN " & vbCrLf & _
        '                 "  IF EXISTS(SELECT AC.ACHIEVEMENT_ID FROM ACCRUED_HEADER AC WHERE (AC.AGREEMENT_NO + '' + AC.BRAND_ID) = @AGREE_BRAND_ID AND " & vbCrLf & _
        '                 "             EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = AC.ACHIEVEMENT_ID AND CAN_RELEASE = 1 AND LEFT_QTY < DISC_QTY) " & vbCrLf & _
        '                 "             AND AC.FLAG LIKE @FLAG + '%' ) " & vbCrLf & _
        '                 "   BEGIN " & vbCrLf & _
        '                 "     SET @V_RETVAL = 1 ; " & vbCrLf & _
        '                 "   END " & vbCrLf & _
        '                 " END " & vbCrLf & _
        '                 " SELECT RETVAL = @V_RETVAL;"
        '        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql(CommandType.Text, Query)
        '        Else : Me.ResetCommandText(CommandType.Text, Query)
        '        End If
        '        Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32)
        '        Me.AddParameter("@FLAG", SqlDbType.VarChar, FLAG, 2)
        '        OpenConnection()
        '        Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
        '        If CInt(retval) > 0 Then : Return True : End If
        '    Catch ex As Exception
        '        Me.CloseConnection() : Me.ClearCommandParameters()
        '        Throw ex
        '    End Try
        'End Function

        Public Function HasgeneratedDiscount(ByVal AGREE_BRAND_ID As String, ByVal FLAG As String) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                         "DECLARE @V_RETVAL INT;" & vbCrLf & _
                         "SET @V_RETVAL = 0; " & vbCrLf & _
                         "  IF EXISTS(SELECT AC.ACHIEVEMENT_ID FROM ACCRUED_HEADER AC WHERE (AC.AGREEMENT_NO + '' + AC.BRAND_ID) = @AGREE_BRAND_ID " & vbCrLf & _
                         "             AND AC.FLAG LIKE @FLAG + '%' ) " & vbCrLf & _
                         " OR EXISTS(SELECT AC1.ACH_HEADER_ID FROM ACCRUED_HEADER AC1 WHERE (AC1.AGREEMENT_NO + '' + AC1.BRAND_ID) = @AGREE_BRAND_ID " & vbCrLf & _
                         "             AND AC1.FLAG LIKE @FLAG + '%' ) " & vbCrLf & _
                         "   BEGIN " & vbCrLf & _
                         "     SET @V_RETVAL = 1 ; " & vbCrLf & _
                         "   END " & vbCrLf & _
                         " SELECT RETVAL = @V_RETVAL;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql(CommandType.Text, Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32)
                Me.AddParameter("@FLAG", SqlDbType.VarChar, FLAG, 2)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If CInt(retval) > 0 Then : Return True : End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Function GetItemBrandPackByBrandID(ByVal BRAND_ID As String) As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT BRANDPACK_ID,BRANDPACK_NAME FROM BRND_BRANDPACK WHERE BRAND_ID = '" & BRAND_ID & "' AND IsActive = 1 AND IsObsolete = 0; "
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.m_ViewBrandpack1 = Me.FillDataTable(New DataTable("T_BrandPack")).DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewBrandpack1
        End Function

        Public Function GetItemBrandPackByBrandName(ByVal BrandId As String, ByVal AgreementNo As String, ByVal mustCloseConnection As Boolean) As DataView

            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_Select_BPNAMENOTIN_AGREE_BRANDPACK_INCLUDE", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Select_BPNAMENOTIN_AGREE_BRANDPACK_INCLUDE")
                End If
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNo, 25) ' VARCHAR(25),
                Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, BrandId, 7)
                'chkl.Items.Clear()
                Dim TblItemBrandPack As New DataTable("T_IBrandPack")
                TblItemBrandPack.Clear()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(TblItemBrandPack) : Me.ClearCommandParameters()
                'Me.FillDataTable(TblItemBrandPack)
                Me.m_ViewBrandpack1 = TblItemBrandPack.DefaultView()
                Me.m_ViewBrandpack1.Sort = "BRANDPACK_NAME"
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return Me.m_ViewBrandpack1
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function

        Public Sub FilComboBrand(ByVal AGREEMENT_NO As String, ByVal cmb As System.Windows.Forms.ComboBox)
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Sp_SelectBrand_NOTINCOMBAGREE_BRAND_INCLUDE", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_SelectBrand_NOTINCOMBAGREE_BRAND_INCLUDE")
                End If
                Dim tbl_AgFirstSecond As New DataTable
                tbl_AgFirstSecond.Clear()
                Me.FillDataTable(tbl_AgFirstSecond)
                m_ViewAgFirstSecond = tbl_AgFirstSecond.DefaultView()
                m_ViewAgFirstSecond.Sort = "BRAND_NAME"
                cmb.DataSource = m_ViewAgFirstSecond
                cmb.DisplayMember = "BRAND_NAME"
                cmb.ValueMember = "BRAND_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub CreatViewCombinedBrand(ByVal AGREEMENT_NO As String, ByVal TRV As System.Windows.Forms.TreeView, ByVal mustCloseConnection As Boolean)
            Try
                TRV.Nodes.Clear()
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Sp_Select_CABID_ABID", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Select_CABID_ABID")
                End If
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Dim tblSementara As New DataTable("T_Sementara")
                tblSementara.Clear() : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection() : Me.SqlDat.Fill(tblSementara) : Me.ClearCommandParameters()
                'Me.FillDataTable(tblSementara)
                If tblSementara.Rows.Count > 0 Then
                    ColNode = New Collection()
                    Me.ColNode.Clear()
                    'buat treeview 
                    'bikin coleksi hashtable baru
                    Me.HastCollCommBined = New Hashtable()
                    Me.HastCollCommBined.Clear()
                    For i As Integer = 0 To tblSementara.Rows.Count - 1
                        'masukan key dari agree_brand_id,valuenya denga comb_agree_brand_id
                        Me.HastCollCommBined.Add(tblSementara.Rows(i)("COMB_AGREE_BRAND_ID"), tblSementara.Rows(i)("AGREE_BRAND_ID"))
                    Next

                    'Dim CollValue As New System.Collections.Specialized.NameValueCollection
                    'item data table sudah masuk semua sekarang buat NameValueCollection unik
                    Me.NVCombUnik = New System.Collections.Specialized.NameValueCollection
                    Me.NVCombUnik.Clear()
                    For i As Integer = 0 To tblSementara.Rows.Count - 1 ' looping sampai habis
                        'IF ME.HastCollCommBined.ContainsValue(
                        If Me.HastCollCommBined.ContainsValue(tblSementara.Rows(i)("COMB_AGREE_BRAND_ID")) Then ' jika ditemukan 
                            Me.NVCombUnik.Add(tblSementara.Rows(i)("COMB_AGREE_BRAND_ID").ToString(), tblSementara.Rows(i)("AGREE_BRAND_ID").ToString()) ' isi NameValueCollection unik
                            Me.HastCollCommBined.Remove(tblSementara.Rows(i)("COMB_AGREE_BRAND_ID")) 'remove item yang memilki key = comb_agree_brand_id
                        End If
                    Next
                    ' sekarang NameValueCollection UNIK HANYA DI ISI DENGAN KEY AGREE_BRAND_ID DAN VALUE COMB_BRAND_ID
                    'BUAT PARENT NODE
                    'LOOPING HASHTABELUNIK SAMPAI HABIS
                    'SATU HASHTABEL UNIK MEMILIKI DUA BRAND_ID(AMBIL BRAND_NAMENYA)
                    '' sekarang tinggal buat parent node dengan node parent pertama dari brandname dari tblbrand
                    Dim TreeNodeParent As System.Windows.Forms.TreeNode
                    'Dim TreeNodeBrand As System.Windows.Forms.TreeNode
                    'Dim TreeNodeBrandPack As System.Windows.Forms.TreeNode
                    Dim TreeNodeFirstSecond As System.Windows.Forms.TreeNode
                    Dim TreeNodeAfterTNBrand As System.Windows.Forms.TreeNode
                    Dim TreeNodeChild As System.Windows.Forms.TreeNode
                    TreeNodeParent = TRV.Nodes.Add("TreeView Combined-Brand")
                    TreeNodeParent.Tag = "TreeView Combined-Brand"
                    Me.ColNode.Add(TreeNodeParent, "TreeView Combined-Brand")
                    TreeNodeFirstSecond = CType(ColNode.Item("TreeView Combined-Brand"), System.Windows.Forms.TreeNode)
                    For I As Integer = 0 To Me.NVCombUnik.Count - 1
                        'CARI BRAND_ID DI TABEL AGREE_BRAND_INCLUDE DENGAN AGREE_BRAND_ID DI KEY HASHTABEL UNIK
                        'DAN AGREEMENT_NO DI PARAMETER SUB DENGAN MENGEXECUTE STORED PROCEDURE >> Sp_Select_BrandID_WITHKEYFROMHASHTABLE <<
                        ColNode.Clear()
                        Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Select_BrandID_WITHKEYFROMHASHTABLE")
                        'Me.CreateCommandSql("Sp_Select_BrandID_WITHKEYFROMHASHTABLE", "")
                        Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                        Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, Me.NVCombUnik.Get(I), 32)
                        Dim tblBrand As New DataTable("T_Brand")
                        tblBrand.Clear() : Me.SqlDat.SelectCommand = Me.SqlCom : Me.SqlDat.Fill(tblBrand) : Me.ClearCommandParameters()
                        'Me.FillDataTable(tblBrand)
                        If tblBrand.Rows.Count > 0 Then
                            ''brand_name dan brand_id sudah dapat
                            TreeNodeParent = TreeNodeFirstSecond.Nodes.Add(tblBrand.Rows(0)("BRAND_NAME").ToString() + " + ")
                            TreeNodeParent.Tag = tblBrand.Rows(0)("BRAND_ID")
                            Me.ColNode.Add(TreeNodeParent, tblBrand.Rows(0)("BRAND_ID"))
                            'SELECSI BRANPACK DARI BRAND_ID DI TBLBRAND YANG ADA DI AGREE_BRANDPACK_INCLUDE DENGAN MENGEXECURE PROCEDURE >>> Sp_Select_BPNAME_INCLUDE_INABINCLUDE <<
                            Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Select_BPNAME_INCLUDE_INABINCLUDE")
                            'Me.CreateCommandSql("Sp_Select_BPNAME_INCLUDE_INABINCLUDE", "")
                            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25) ' VARCHAR(25),
                            Me.AddParameter("AGREE_BRAND_ID", SqlDbType.VarChar, Me.NVCombUnik.Get(I), 32)
                            Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, tblBrand.Rows(0)("BRAND_ID"), 7) ' VARCHAR(7)
                            Dim TblBrandpack As New DataTable("T_BrandPack")
                            TblBrandpack.Clear() : Me.SqlDat.Fill(TblBrandpack) : Me.ClearCommandParameters()
                            'Me.FillDataTable(TblBrandpack)
                            If TblBrandpack.Rows.Count > 0 Then
                                'treenode yang di hasilkan adalah value brand_packname yang di include di AGREE_BRANDPACK_INCLUDE   
                                For i0 As Integer = 0 To TblBrandpack.Rows.Count - 1
                                    TreeNodeAfterTNBrand = CType(ColNode.Item(tblBrand.Rows(0)("BRAND_ID")), System.Windows.Forms.TreeNode)
                                    TreeNodeChild = TreeNodeAfterTNBrand.Nodes.Add(TblBrandpack.Rows(i0)("BRANDPACK_NAME"))
                                    TreeNodeChild.Tag = TblBrandpack.Rows(i0)("BRANDPACK_ID")
                                    'ColNode.Add(TreeNodeChild, TblBrandpack.Rows(i)("BRANDPACK_ID"))
                                    TreeNodeChild.ForeColor = System.Drawing.Color.DarkRed
                                    'me.ColNode.Add(tblBrandPack.Rows(i)("BRAND_ID")
                                Next
                            End If
                            'CHILD NODE DARI BRAND NAME + SUDAH DIISI DENGAN BRANDPACK_NAMENYA
                            'SEKARANG TINGGAL MENGISI DENGAN BRAND_NAME KEDUA DAN BRANDPACK_NAMENYA
                            Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Select_BrandID_WITHKEYFROMHASHTABLE")
                            'Me.CreateCommandSql("Sp_Select_BrandID_WITHKEYFROMHASHTABLE", "")
                            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                            Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, Me.NVCombUnik.GetKey(I), 32)
                            'CLEARKAN ITEM DARI TBLBRAND UNTUK DIISI DENGAN ITEM BRAN_NAME BARU
                            tblBrand.Clear() : Me.SqlDat.Fill(tblBrand) : Me.ClearCommandParameters()
                            'Me.FillDataTable(tblBrand)
                            If tblBrand.Rows.Count > 0 Then
                                'Dim TreeNodeBrandPack1 As System.Windows.Forms.TreeNode
                                'Dim TreeNodeAfterTNBrand1 As System.Windows.Forms.TreeNode
                                Dim TreeNodeChild1 As System.Windows.Forms.TreeNode
                                TreeNodeParent.Text = TreeNodeParent.Text + "" + tblBrand.Rows(0)("BRAND_NAME")
                                TreeNodeParent.Tag = tblBrand.Rows(0)("BRAND_ID")
                                'CLEARKAN COLLNODE
                                Me.ColNode.Clear()
                                Me.ColNode.Add(TreeNodeParent, tblBrand.Rows(0)("BRAND_ID"))
                                'SELECSI BRANPACK DARI BRAND_ID DI TBLBRAND YANG ADA DI AGREE_BRANDPACK_INCLUDE DENGAN MENGEXECURE PROCEDURE >>> Sp_Select_BPNAME_INCLUDE_INABINCLUDE <<
                                Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Select_BPNAME_INCLUDE_INABINCLUDE")
                                'Me.CreateCommandSql("Sp_Select_BPNAME_INCLUDE_INABINCLUDE", "")
                                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25) ' VARCHAR(25),
                                Me.AddParameter("AGREE_BRAND_ID", SqlDbType.VarChar, Me.NVCombUnik.GetKey(I), 32)
                                Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, tblBrand.Rows(0)("BRAND_ID"), 7) ' VARCHAR(7)
                                'CLEARKAN TBLBRANDPACK
                                TblBrandpack.Clear() : Me.SqlDat.Fill(TblBrandpack) : Me.ClearCommandParameters()
                                'Me.FillDataTable(TblBrandpack)
                                If TblBrandpack.Rows.Count > 0 Then
                                    'treenode yang di hasilkan adalah value brand_packname yang di include di AGREE_BRANDPACK_INCLUDE   
                                    For i1 As Integer = 0 To TblBrandpack.Rows.Count - 1
                                        TreeNodeAfterTNBrand = CType(ColNode.Item(tblBrand.Rows(0)("BRAND_ID")), System.Windows.Forms.TreeNode)
                                        TreeNodeChild1 = TreeNodeAfterTNBrand.Nodes.Add(TblBrandpack.Rows(i1)("BRANDPACK_NAME"))
                                        TreeNodeChild1.Tag = TblBrandpack.Rows(i1)("BRANDPACK_ID")
                                        'ColNode.Add(TreeNodeChild, TblBrandpack.Rows(i)("BRANDPACK_ID"))
                                        TreeNodeChild1.ForeColor = System.Drawing.Color.DarkRed
                                        'me.ColNode.Add(tblBrandPack.Rows(i)("BRAND_ID")
                                    Next
                                End If
                            End If
                        End If
                    Next
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If : TRV.ExpandAll() : TRV.Update()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Function SearchAgreement(ByVal SearchAgree As String) As DataView
            Try
                Dim Query As String = "SET NOCOUNT ON;SELECT TOP 100 DISTRIBUTOR_ID,DISTRIBUTOR_NAME,AGREEMENT_NO,AGREEMENT_DESC,QS_TREATMENT_FLAG,START_DATE,END_DATE," & _
                "QS_TREATMENT_FLAG FROM VIEW_AGREEMENT WHERE AGREEMENT_NO LIKE '%" & SearchAgree & "%'"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_Agreement") : dtTable.Clear()
                Me.FillDataTable(dtTable)
                Me.m_ViewAgreement = dtTable.DefaultView() : Me.m_ViewAgreement.Sort = "AGREEMENT_NO"
                Return Me.m_ViewAgreement
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub SaveGivenHistory(ByVal SaveType As String, ByVal Given_ID As String, ByVal AGREE_BRAND_ID As String, _
        ByVal START_DATE As DateTime, ByVal DISC_PCT As Decimal, _
        ByVal GIVEN_DESCRIPTION As String)
            Try
                If SaveType = "Save" Then
                    'type save = insert
                    Me.CreateCommandSql("Usp_INSERT_GIVEN_STORY", "")
                    Me.SqlCom.Parameters.Add("@O_MESSAGE", SqlDbType.VarChar, 150).Direction = ParameterDirection.Output
                    'Me.AddParameter("@O_MESSAGE", SqlDbType.VarChar, ParameterDirection.Output)
                    'Me.SqlCom.Parameters()("@O_MESSAGE").Size = 150
                Else
                    Me.CreateCommandSql("Usp_UPDATE_GIVEN_STORY", "")
                End If
                Me.AddParameter("@GIVEN_ID", SqlDbType.VarChar, Given_ID, 44) ' VARCHAR(44),
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, START_DATE) ' SMALLDATETIME,
                'Me.AddParameter("@END_DATE", SqlDbType.DateTime, END_DATE) ' SMALLDATETIME,
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32) ' VARCHAR(32),
                Me.AddParameter("@DISC_PCT", SqlDbType.Decimal, DISC_PCT) ' VARCHAR(6),
                Me.AddParameter("@GIVEN_DESCRIPTION", SqlDbType.VarChar, GIVEN_DESCRIPTION, 150) ' VARCHAR(150)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.SqlCom.Transaction = Me.SqlTrans
                'Me.SqlCom.ExecuteNonQuery()
                'Me.CommiteTransaction()
                If Me.SqlCom.ExecuteNonQuery() > 0 Then
                    If SaveType = "Save" Then
                        Dim Message As String = Me.SqlCom.Parameters()("@O_MESSAGE").Value.ToString()
                        If Message <> "" Then
                            Me.RollbackTransaction()
                            Me.CloseConnection()
                            Throw New System.Exception(Message)
                        End If
                    End If
                    Me.CommiteTransaction()
                Else
                    Me.RollbackTransaction()
                    Me.CloseConnection()
                    If SaveType = "Save" Then
                        If Me.SqlCom.Parameters()("@O_MESSAGE").Value.ToString() <> "" Then
                            Throw New System.Exception(Me.SqlCom.Parameters()("@O_MESSAGE").Value.ToString())
                        Else
                            Throw New System.Exception(Me.MessageDBConcurency)
                        End If
                    Else
                        Throw New System.Exception(Me.MessageDBConcurency)
                    End If
                End If
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Public Sub DeleteGivenStory(ByVal Given_ID As String)
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_DELETE_GIVEN_STORY", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_DELETE_GIVEN_STORY")
                End If
                Me.AddParameter("@GIVEN_ID", SqlDbType.VarChar, Given_ID, 44) ' VARCHAR(44),
                'Me.AddParameter("@O_MESSAGE", SqlDbType.VarChar, ParameterDirection.Output) ' VARCHAR(100
                Me.SqlCom.Parameters.Add("@O_MESSAGE", SqlDbType.VarChar, 150).Direction = ParameterDirection.Output
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteNonQuery()
                Dim Message As String = Me.SqlCom.Parameters()("@O_MESSAGE").Value.ToString()
                If Message <> "" Then
                    Throw New System.Exception(Message)
                End If
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

#End Region

#Region " Property "


        Public ReadOnly Property ViewFilterBrand() As DataView
            Get
                Return Me.m_ViewFilterBrand
            End Get
        End Property

        Public ReadOnly Property ViewBrand() As DataView
            Get
                Return Me.m_ViewBrand
            End Get
        End Property

        Public ReadOnly Property ViewAgreement() As DataView
            Get
                Return Me.m_ViewAgreement
            End Get
        End Property

        Public ReadOnly Property GetTblQuantityCombined() As DataTable
            Get
                Return Me.tbl_Quantity_Combined
            End Get
        End Property

        Public ReadOnly Property Gettbl_ComboFirstSecond() As DataTable
            Get
                Return Me.Tbl_BrandComboFisrtSecond
            End Get
        End Property

        Public ReadOnly Property ViewOAHistory() As DataView
            Get
                Return Me.m_OAHistory
            End Get
        End Property

        Public ReadOnly Property ViewBrandPack() As DataView
            Get
                Return Me.m_ViewBrandpack1
            End Get
        End Property
        Public ReadOnly Property ViewBrandGivenHistory()
            Get
                Return Me.m_ViewGivenStory
            End Get
        End Property

        Public ReadOnly Property GetTableSemesterly() As DataTable
            Get
                Return Me.m_dataTableSemeterly
            End Get
        End Property
        Public ReadOnly Property GetTableSemesterlyV() As DataTable
            Get
                Return Me.m_dataTableSemeterlyV
            End Get
        End Property

        Public ReadOnly Property GetTableQuarterly() As DataTable
            Get

                Return Me.m_dataTableQuarterly
            End Get
        End Property
        Public ReadOnly Property GetTableQuarterlyV() As DataTable
            Get

                Return Me.m_dataTableQuarterlyV
            End Get
        End Property

        Public ReadOnly Property GetTableYearly() As DataTable
            Get
                'Me.m_dataTableYearly.Clear()
                Return Me.m_dataTableYearly
            End Get
        End Property
        Public ReadOnly Property GetTableYearlyV() As DataTable
            Get
                'Me.m_dataTableYearly.Clear()
                Return Me.m_dataTableYearlyV
            End Get
        End Property

        Public ReadOnly Property getDsPeriod() As DataSet
            Get
                Return Me.m_DSPeriodic
            End Get
        End Property
        Private m_dataTableFMP As DataTable
        Public ReadOnly Property GetTableFMP() As DataTable
            Get
                Return Me.m_dataTableFMP
            End Get
        End Property
#End Region

#Region " Function "

        Public Function GetDsSetPeriod(ByVal AGREE_BRAND_ID As String, ByVal QS_Flag As String) As DataSet
            Try

                Me.m_DSPeriodic = New DataSet("DsPeriod") : Me.m_DSPeriodic.Clear()
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_Select_QS_Flag", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Select_QS_Flag")
                End If
                Me.AddParameter("@QS_FLAG", SqlDbType.VarChar, QS_Flag, 2)
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32)
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection()
                Select Case QS_Flag
                    Case "Q"
                        ''============FILL DISCOUNT VOLUME==================
                        Me.m_dataTableQuarterly = New DataTable("T_Q_Flag")
                        Me.m_dataTableQuarterly.Clear()
                        Me.SqlDat.Fill(Me.m_dataTableQuarterly)

                        ''transisi
                        'Me.m_dataTableFMP = New DataTable("T_F_Flag")
                        'Me.m_dataTableFMP.Clear()
                        'Me.SqlDat.Fill(Me.m_dataTableFMP)

                        ''============FILL DISCOUNT VALUE==================
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                " SELECT IDApp,AGREE_BRAND_ID,QSY_DISC_FLAG, PRGSV_DISC_PCT  FROM AGREE_PROG_DISC_VAL " & vbCrLf & _
                                " WHERE (QSY_DISC_FLAG LIKE @QS_FLAG + '%') AND (AGREE_BRAND_ID = @AGREE_BRAND_ID) "
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.m_dataTableQuarterlyV = New DataTable("T_Q_Flag_Val")
                        Me.m_dataTableQuarterlyV.Clear()
                        Me.SqlDat.Fill(Me.m_dataTableQuarterlyV)

                        '==========ADD BOTH Tables to dataset=========================
                        Me.m_DSPeriodic.Tables.Add(Me.m_dataTableQuarterly)
                        Me.m_DSPeriodic.Tables.Add(Me.m_dataTableQuarterlyV)

                        'transisi
                        'Me.m_dataTableFMPV = New DataTable("T_F_Flag_Val")
                        'Me.m_dataTableFMPV.Clear()
                        'Me.SqlDat.Fill(Me.m_dataTableFMPV)

                        ''==========ADD BOTH Tables to dataset=========================
                        'Me.m_DSPeriodic.Tables.Add(Me.m_dataTableFMP)
                        'Me.m_DSPeriodic.Tables.Add(Me.m_dataTableFMPV)

                    Case "S"
                        ''============FILL DISCOUNT VOLUME==================
                        Me.m_dataTableSemeterly = New DataTable("T_S_Flag")
                        Me.m_dataTableSemeterly.Clear()
                        Me.SqlDat.Fill(Me.m_dataTableSemeterly)

                        ''============FILL DISCOUNT VALUE==================
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                              " SELECT IDApp,AGREE_BRAND_ID,QSY_DISC_FLAG, PRGSV_DISC_PCT  FROM AGREE_PROG_DISC_VAL " & vbCrLf & _
                              " WHERE (QSY_DISC_FLAG LIKE @QS_FLAG + '%') AND (AGREE_BRAND_ID = @AGREE_BRAND_ID) "
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.m_dataTableSemeterlyV = New DataTable("T_S_Flag_Val")
                        Me.m_dataTableSemeterlyV.Clear()
                        Me.SqlDat.Fill(Me.m_dataTableSemeterlyV)

                        '==========ADD BOTH Tables to dataset=========================
                        Me.m_DSPeriodic.Tables.Add(Me.m_dataTableSemeterly)
                        Me.m_DSPeriodic.Tables.Add(Me.m_dataTableSemeterlyV)
                    Case "F"
                        ''============FILL DISCOUNT VOLUME==================
                        'Query = "SELECT * FROM 
                        Me.m_dataTableFMP = New DataTable("T_F_Flag")
                        Me.m_dataTableFMP.Clear()
                        Me.SqlDat.Fill(Me.m_dataTableFMP)
                        Me.m_DSPeriodic.Tables.Add(Me.m_dataTableFMP)
                        ' ''============FILL DISCOUNT VALUE==================
                        'Query = "SET NOCOUNT ON; " & vbCrLf & _
                        '      " SELECT IDApp,AGREE_BRAND_ID,QSY_DISC_FLAG, PRGSV_DISC_PCT  FROM AGREE_PROG_DISC_VAL " & vbCrLf & _
                        '      " WHERE (QSY_DISC_FLAG LIKE @QS_FLAG + '%') AND (AGREE_BRAND_ID = @AGREE_BRAND_ID) "
                        'Me.ResetCommandText(CommandType.Text, Query)
                        'Me.m_dataTableFMPV = New DataTable("T_F_Flag_Val")
                        'Me.m_dataTableFMPV.Clear()
                        'Me.SqlDat.Fill(Me.m_dataTableFMPV)

                        ''==========ADD BOTH Tables to dataset=========================
                        'Me.m_DSPeriodic.Tables.Add(Me.m_dataTableFMP)
                        'Me.m_DSPeriodic.Tables.Add(Me.m_dataTableFMPV)
                End Select
                Me.ClearCommandParameters()
                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Select_Y_Flag")
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32)
                Me.m_dataTableYearly = New DataTable("T_Y_Flag")
                Me.m_dataTableYearly.Clear()
                setDataAdapter(Me.SqlCom).Fill(Me.m_dataTableYearly)

                '==========ADD BOTH Tables yearly to dataset=========================
                Me.m_DSPeriodic.Tables.Add(Me.m_dataTableYearly)
                If Not Flag.Contains("F") Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT IDApp,AGREE_BRAND_ID,QSY_DISC_FLAG, PRGSV_DISC_PCT " & vbCrLf & _
                            " FROM AGREE_PROG_DISC_VAL " & vbCrLf & _
                            " WHERE (QSY_DISC_FLAG = 'Y') AND (AGREE_BRAND_ID = @AGREE_BRAND_ID) "
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.m_dataTableYearlyV = New DataTable("T_Y_Flag_Val")
                    Me.m_dataTableYearlyV.Clear()
                    setDataAdapter(Me.SqlCom).Fill(Me.m_dataTableYearlyV)
                    Me.m_DSPeriodic.Tables.Add(Me.m_dataTableYearlyV)
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_DSPeriodic
        End Function

        Public Function CreateViewGivenStory(ByVal AGREE_BRAND_ID As String) As DataView
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_Create_View_Agree_Brand_Given_Story", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Create_View_Agree_Brand_Given_Story")
                End If
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32)
                Dim tblGiven As New DataTable("GIVEN_HISTORY")
                tblGiven.Clear()
                Me.FillDataTable(tblGiven)
                Me.m_ViewGivenStory = tblGiven.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewGivenStory
        End Function

        Public Function CreateViewOAHistory(ByVal AGREEMENT_NO As String) As DataView
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Sp_GetView_OA_BY_AGREEMENT_NO", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_GetView_OA_BY_AGREEMENT_NO")
                End If
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Dim tblOAHistory As New DataTable("OA_HISTORY")
                tblOAHistory.Clear()
                Me.FillDataTable(tblOAHistory)
                Me.m_OAHistory = tblOAHistory.DefaultView()
                Me.m_OAHistory.Sort = "OA_REF_NO"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_OAHistory
        End Function

        Public Function GetTabelQuantityCombined(ByVal Comb_Agree_Brand_id As String, ByVal AGREEMENT_NO As String, ByVal QS_FLAG As String) As DataTable
            Try
                Select Case QS_FLAG
                    Case "Q"
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT BRAND_NAME,TARGET_Q1,TARGET_Q2,TARGET_Q3,TARGET_Q4 FROM VIEW_AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO =@AGREEMENT_NO" & vbCrLf & _
                                " AND [ID] = @COMB_AGREE_BRAND_ID"
                        'If IsNothing(Me.SqlCom) Then
                        '    Me.CreateCommandSql("Sp_SelectQQTY_FROM_ABI", "")
                        'Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_SelectQQTY_FROM_ABI")
                        'End If
                        'Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25) ' VARCHAR(25),
                        'Me.AddParameter("@COMB_AGREE_BRAND_ID", SqlDbType.VarChar, Comb_Agree_Brand_id, 32) ' VARCHAR(32)
                    Case "S"
                        'If IsNothing(Me.SqlCom) Then
                        '    Me.CreateCommandSql("Sp_SelectSQTY_FROM_ABI", "")
                        'Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_SelectSQTY_FROM_ABI")
                        'End If
                        Query = "SET  NOCOUNT ON;" & vbCrLf & _
                                " SELECT BRAND_NAME,TARGET_S1,TARGET_S2 FROM VIEW_AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = @AGREEMENT_NO AND " & vbCrLf & _
                                " [ID] = @COMB_AGREE_BRAND_ID"
                    Case "F"
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT BRAND_NAME,TARGET_FMP1,TARGET_FMP2,TARGET_FMP3 FROM VIEW_AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO =@AGREEMENT_NO" & vbCrLf & _
                                " AND [ID]= @COMB_AGREE_BRAND_ID"
                End Select
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25) ' VARCHAR(25),
                Me.AddParameter("@COMB_AGREE_BRAND_ID", SqlDbType.VarChar, Comb_Agree_Brand_id, 32) ' VARCHAR(32)
                Me.tbl_Quantity_Combined = New DataTable()
                Me.tbl_Quantity_Combined.Clear()
                Me.FillDataTable(tbl_Quantity_Combined)
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.tbl_Quantity_Combined
        End Function
        Public Function CountProgDisc(ByVal AGREE_BRAND_ID As String) As Integer
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " DECLARE @V_COMB_BRAND_ID VARCHAR(100),@V_RETVAL INT ; " & vbCrLf & _
                        " SET @V_COMB_BRAND_ID = (SELECT TOP 1 COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID) ; " & vbCrLf & _
                        " SET @V_RETVAL = (SELECT ISNULL((SELECT 1 WHERE EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID) " & vbCrLf & _
                        "                                               OR EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_PROG_DISC_VAL WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID)),0)); " & vbCrLf & _
                        " IF @V_RETVAL > 0 BEGIN SELECT RETVAL = @V_RETVAL ; RETURN ; END " & vbCrLf & _
                        " IF (@V_COMB_BRAND_ID IS NOT NULL) " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "  SET @V_RETVAL = (SELECT ISNULL((SELECT 1 WHERE EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = @V_COMB_BRAND_ID) " & vbCrLf & _
                        "                                               OR EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_PROG_DISC_VAL WHERE AGREE_BRAND_ID = @V_COMB_BRAND_ID)),0)); " & vbCrLf & _
                        " END " & vbCrLf & _
                        " IF @V_RETVAL > 0 BEGIN SELECT RETVAL = @V_RETVAL ; RETURN ; END "
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Return (IIf((Not IsNothing(retval) And Not IsDBNull(retval)), 1, 0))
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function IsExistItemBrandInOtherAgreement(ByVal BRAND_ID As String, ByVal DISTRIBUTOR_IDS As List(Of String), ByRef Message As String) As Boolean
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_Check_Existing_BI_By_Distributor", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Existing_BI_By_Distributor")
                End If
                Me.OpenConnection()
                For i As Integer = 0 To DISTRIBUTOR_IDS.Count - 1
                    Dim Retval As Object = Nothing
                    Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, BRAND_ID, 7) ' VARCHAR(7),
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_IDS(i), 10) ' VARCHAR(10)
                    Retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If CInt(Retval) > 0 Then
                        Dim DistributorName As String = "", AgreementNo As String = ""
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "SELECT TOP 1 DR.DISTRIBUTOR_NAME,DA.AGREEMENT_NO FROM DIST_DISTRIBUTOR DR " & vbCrLf & _
                                " INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DR.DISTRIBUTOR_ID = DA.DISTRIBUTOR_ID " & vbCrLf & _
                                " WHERE EXISTS(SELECT BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = DA.AGREEMENT_NO AND BRAND_ID = @BRAND_ID) " & vbCrLf & _
                                " AND DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                                " AND EXISTS(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = DA.AGREEMENT_NO AND END_DATE >= @GETDATE);"
                        Me.SqlCom.CommandText = Query : Me.SqlCom.CommandType = CommandType.Text
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_IDS(i), 10)
                        Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, BRAND_ID, 7)
                        Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
                        Me.SqlRe = Me.SqlCom.ExecuteReader()
                        While Me.SqlRe.Read()
                            DistributorName = SqlRe.GetString(0)
                            AgreementNo = SqlRe.GetString(1)
                        End While : SqlRe.Close() : Me.ClearCommandParameters()
                        Message = "Distributor " & DistributorName & " Has held agreement with the same brand" & vbCrLf & _
                                 "The agreementNo distributor's held is " & AgreementNo
                        Me.CloseConnection() : Return True
                    End If
                Next
                Me.CloseConnection() : Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function hasRefData(ByVal AgreementNO As String, ByVal BRAND_ID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Dim AgreeBrandID As String = AgreementNO + BrandID
                'Check Given Progressive
                'check Agree_Prog_disc
                'check Agree_brandPack_include
                'check Given_history
                'Check AccruedHeader
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT 1 WHERE EXISTS(SELECT AGREE_BRAND_ID FROM GIVEN_PROGRESSIVE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID) " & vbCrLf & _
                        " OR EXISTS(SELECT COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID AND COMB_AGREE_BRAND_ID IS NOT NULL) " & vbCrLf & _
                        " OR EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID )" & vbCrLf & _
                        " OR EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID) " & vbCrLf & _
                        " OR EXISTS(SELECT AGREE_BRAND_INCLUDE FROM GIVEN_STORY WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID) " & vbCrLf & _
                        " OR EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER WHERE (AGREEMENT_NO + BRAND_ID = @AGREE_BRAND_ID)) ;"

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AgreeBrandID, 100)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return CInt(retval) > 0
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetViewBrandInclude(ByVal AGREEMENT_NO As String) As DataView
            Try
                'BIKIN PARENT TABLE DULU
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT ABI.*,ProgressiveDiscountType = CASE " & vbCrLf & _
                        " WHEN EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = ABI.[ID])  THEN 'Custom' " & vbCrLf & _
                        " WHEN EXISTS(SELECT IDApp FROM AGREE_PROG_DISC_R WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "') THEN 'Custom' " & vbCrLf & _
                        " WHEN EXISTS(SELECT AGREEMENT_NO FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "') THEN 'General' " & vbCrLf & _
                        " ELSE 'Unspecified' END " & vbCrLf & _
                        " FROM VIEW_AGREE_BRAND_INCLUDE ABI WHERE AGREEMENT_NO = '" + AGREEMENT_NO + "';"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_BrandInclude") : dtTable.Clear()
                Me.FillDataTable(dtTable) : Me.m_ViewBrand = dtTable.DefaultView()
                Me.m_ViewBrand.Sort = "BRAND_NAME ASC"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewBrand
        End Function
        Public Function CreateView_Brand_Include(ByVal AGREEMENT_NO As String) As DataView
            Try

                Me.CreateCommandSql("", "SET NOCOUNT ON;SELECT DISTINCT BRAND_ID,BRAND_NAME FROM VIEW_AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'")
                Dim tblBrandInclude As New DataTable("BrandInclude")
                tblBrandInclude.Clear()
                Me.FillDataTable(tblBrandInclude)
                Me.m_ViewBrand = tblBrandInclude.DefaultView()
                Me.m_ViewBrand.Sort = "BRAND_ID"
                Return Me.m_ViewBrand
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        'procedure untuk mengambil agreement no dengan parameter agree_brand_id
        Public Function getAgreeMentNO(ByVal AGREE_BRAND_ID As String) As Object
            Dim retval As Object = Nothing
            Try
                retval = Me.ExecuteScalar("", "SET NOCOUNT ON;SELECT AGREEMENT_NO FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & _
                AGREE_BRAND_ID & "'")
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return retval
        End Function
        Public Function GetAgreement(ByVal DISTRIBUTOR_ID As String) As DataView
            Try
                Query = "SET NOCOUNT ON;SELECT DIST_DISTRIBUTOR.DISTRIBUTOR_NAME,AGREE_AGREEMENT.AGREEMENT_NO" & _
                " FROM AGREE_AGREEMENT,DIST_DISTRIBUTOR,DISTRIBUTOR_AGREEMENT WHERE DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = DIST_DISTRIBUTOR.DISTRIBUTOR_ID " & _
                " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "' AND DISTRIBUTOR_AGREEMENT.AGREEMENT_NO = AGREE_AGREEMENT.AGREEMENT_NO AND AGREE_AGREEMENT.END_DATE >= " & NufarmBussinesRules.SharedClass.ShortGetDate()
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("stmt", SqlDbType.NVarChar, Query)
                Me.m_ViewAgreement = Me.FillDataTable(New DataTable("T_Agreement")).DefaultView()
                Return Me.m_ViewAgreement
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function GetBrandID(ByVal BRANDPACK_ID As String) As Object
            Try
                Return Me.ExecuteScalar("", "SET NOCOUNT ON;SELECT BRAND_ID FROM BRND_BRANDPACK WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "'")
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function GetAgree_Brand_ID(ByVal AGREEMENT_NO As String, ByVal BRANDPACK_ID As String) As String
            Try
                Dim Retval As Object = Nothing
                Query = "SET NOCOUNT ON;SELECT AGREE_BRAND_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREEMENT_NO = '" & _
                       AGREEMENT_NO & "' AND BRANDPACK_ID = '" & BRANDPACK_ID & "'"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Retval = Me.ExecuteScalar()
                If (Not IsNothing(Retval)) And (Not IsDBNull(Retval)) Then
                    Me.Agree_Brand_ID = Retval.ToString()
                    Return Me.Agree_Brand_ID
                End If
                Return ""
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function IsExistedGIVEN_IDIN_ADH(ByVal GIVEN_ID As String) As Boolean
            Try
                Query = "SET NOCOUNT ON ; SELECT COUNT(GIVEN_ID) FROM AGREE_DISC_HISTORY WHERE GIVEN_ID = '" & GIVEN_ID & "' ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                If CInt(Me.ExecuteScalar()) > 0 Then
                    Return True
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return False
        End Function
        'fucntion untuk menampilkan data di datagrid
        Public Function GetItemBrandByAgreementNo(ByVal AGREEMENT_NO As String, ByRef AgreementEndDate As DateTime) As DataView
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_Select_FilterBrandName", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Select_FilterBrandName")
                End If
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Dim TblFilterBrand As New DataTable("T_FilterBrand")
                TblFilterBrand.Clear() : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection() : Me.SqlDat.Fill(TblFilterBrand)  'Me.FillDataTable(TblFilterBrand)
                Me.ClearCommandParameters()
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO; "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                AgreementEndDate = Convert.ToDateTime(Me.SqlCom.ExecuteScalar())
                Me.ClearCommandParameters() : Me.CloseConnection()
                Me.m_ViewFilterBrand = TblFilterBrand.DefaultView()
                With Me.m_ViewFilterBrand
                    .Sort = "BRAND_ID"
                    .AllowNew = False
                    .AllowDelete = False
                    .AllowEdit = False
                End With

            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewFilterBrand
        End Function
        'fucntion untuk mengecek referensi table anak di brandpak_include
        Public Function CheckChildReference_BrandPack_Include(ByVal BRANDPACK_ID As String, ByVal Agreement_no As String, ByRef RefInformation As String) As Integer
            Dim RetVal As Integer = 0
            Try
                'Me.CreateCommandSql("Sp_Select_AGREE_BRANDPACKID_MRKT_BRANDPACKID_DISTRIBUTOR", "")
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Sp_Select_AGREE_BRANDPACKID_AGREE_DISC_HISTORY", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Select_AGREE_BRANDPACKID_AGREE_DISC_HISTORY")
                End If
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Agreement_no, 25) ' VARCHAR(25),
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14)
                Me.OpenConnection() : Dim RetvalObject As Object = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If CInt(RetvalObject) > 0 Then
                    RefInformation = "Data already generated in discount agreement"
                    Me.CloseConnection() : Return CInt(RetvalObject)
                End If

                Me.SqlCom.CommandText = "sp_executesql"
                Dim Query As String = "SET NOCOUNT ON; " & vbCrLf & _
                " DECLARE @V_START_DATE DATETIME; " & vbCrLf & _
                " SET @V_START_DATE = (SELECT START_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & Agreement_no + "'); " & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT BRANDPACK_ID FROM ORDR_PO_BRANDPACK OPB " & vbCrLf & _
                " INNER JOIN ORDR_PURCHASE_ORDER PO ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                " WHERE PO.DISTRIBUTOR_ID = SOME(" & vbCrLf & _
                "                                 SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & Agreement_no & "'" & vbCrLf & _
                "                                )" & vbCrLf & _
                " AND OPB.BRANDPACK_ID = '" & BRANDPACK_ID & "'  AND PO.PO_REF_DATE >= @V_START_DATE );"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                RetvalObject = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(RetvalObject) Then
                    If CInt(RetvalObject) > 0 Then
                        RefInformation = "Data already used in Purchase Order"
                        Me.CloseConnection() : Return CInt(RetvalObject)
                    End If
                End If
                Me.SqlCom.CommandText = "Sp_Select_AGREE_BRANDPACKID_MRKT_BRANDPACKID_DISTRIBUTOR"
                'Me.CreateCommandSql("Sp_Select_AGREE_BRANDPACKID_AGREE_DISC_HISTORY", "")
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Agreement_no, 25) ' VARCHAR(25),
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14)
                RetvalObject = Me.SqlCom.ExecuteScalar()
                If CInt(RetvalObject) > 0 Then
                    RefInformation = "Data already used in registered sales program"
                    Me.CloseConnection() : Return CInt(RetvalObject)
                End If
                Me.CloseConnection() : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return RetVal
        End Function
        Public Function IsExisted(ByVal AGREE_BRAND_ID As String) As Boolean
            Try
                Dim retVal As Object = Nothing
                Query = "SET NOCOUNT ON;SELECT AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & AGREE_BRAND_ID & "' ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                retVal = Me.ExecuteScalar()
                If retVal IsNot Nothing Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function IsProgressiveDisc_R(ByVal AgreementNo As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT AGREEMENT_NO FROM AGREE_PROG_DISC_R WHERE AGREEMENT_NO = @AGREEMENT_NO);"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNo, 25)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then : Return True : End If
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function
        Public Function IsCustomProgressiveDiscount(ByVal AGREE_BRAND_ID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON ; SELECT ISNULL((SELECT 1 WHERE EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = '" & AGREE_BRAND_ID & "')),0) ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If CInt(retval) > 0 Then : Return True : End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Function FillCommBoFirstsSecond(ByVal AGREEMENT_NO As String, ByVal IsCustomProgressive As Boolean, ByVal cmb As System.Windows.Forms.ComboBox) As DataTable
            Try
                If (IsCustomProgressive) Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT ABI.BRAND_ID,BR.BRAND_NAME FROM BRND_BRAND BR INNER JOIN AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                            " ON BR.BRAND_ID = ABI.BRAND_ID WHERE ABI.COMB_AGREE_BRAND_ID IS NULL " & vbCrLf & _
                            " AND NOT EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_PROG_DISC WHERE AGREE_BRAND_ID = ABI.AGREE_BRAND_ID) " & vbCrLf & _
                            " AND ABI.AGREEMENT_NO = '" & AGREEMENT_NO & "';"
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT ABI.BRAND_ID,BR.BRAND_NAME FROM BRND_BRAND BR INNER JOIN AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                            " ON BR.BRAND_ID = ABI.BRAND_ID WHERE ABI.COMB_AGREE_BRAND_ID IS NULL AND ABI.AGREEMENT_NO = '" & AGREEMENT_NO & "';"
                End If
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.Tbl_BrandComboFisrtSecond = New DataTable()
                Me.Tbl_BrandComboFisrtSecond.Clear()
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(Tbl_BrandComboFisrtSecond)
                Me.ClearCommandParameters()
                cmb.DataSource = Nothing
                cmb.DataSource = Me.Tbl_BrandComboFisrtSecond
                cmb.ValueMember = "BRAND_ID"
                cmb.DisplayMember = "BRAND_NAME"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.Tbl_BrandComboFisrtSecond
        End Function

        'Public Function HasReferencedGivenHistory(ByVal AGREE_BRAND_ID As String, ByVal mustCloseConnection As Boolean) As Boolean
        '    Try
        '        ''chek di Agree_disc_history dan given_story
        '        Query = "SET NOCOUNT ON; " & vbCrLf & _
        '        " SELECT 1 WHERE EXISTS(SELECT GS.GIVEN_ID FROM GIVEN_STORY GS INNER JOIN AGREE_BRAND_INCLUDE ABR ON ABR.AGREE_BRAND_ID = GS.AGREE_BRAND_ID " & vbCrLf & _
        '        "                 WHERE ABR.AGREE_BRAND_ID = @AGREE_BRAND_ID) " & vbCrLf & _
        '        "               OR EXISTS(SELECT ADH.AGREE_DISC_HIST_ID FROM AGREE_DISC_HISTORY ADH INNER JOIN AGREE_BRANDPACK_INCLUDE ABI ON ABI.AGREE_BRANDPACK_ID = ADH.AGREE_BRANDPACK_ID " & vbCrLf & _
        '        "               WHERE ABI.AGREE_BRAND_ID = @AGREE_BRAND_ID);"
        '        If Not IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
        '        Else : Me.ResetCommandText(CommandType.Text, Query)
        '        End If
        '        Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32)
        '        Me.OpenConnection()
        '        Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
        '        If Not IsNothing(retval) And Not IsDBNull(retval) Then
        '            If mustCloseConnection Then : Me.CloseConnection() : End If
        '            Return (CInt(retval) > 0)
        '        End If
        '        If mustCloseConnection Then : Me.CloseConnection() : End If
        '        Return False
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        'End Function

        Public Function HasReferencedGivenHistory(ByVal AGREE_BRAND_ID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                ''chek di Agree_disc_history dan given_story
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT ADH.AGREE_DISC_HIST_ID FROM AGREE_DISC_HISTORY ADH INNER JOIN AGREE_BRANDPACK_INCLUDE ABI ON ABI.AGREE_BRANDPACK_ID = ADH.AGREE_BRANDPACK_ID " & vbCrLf & _
                "               WHERE ABI.AGREE_BRAND_ID = @AGREE_BRAND_ID);"
                If Not IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREE_BRAND_ID", SqlDbType.VarChar, AGREE_BRAND_ID, 32)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    If mustCloseConnection Then : Me.CloseConnection() : End If
                    Return (CInt(retval) > 0)
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function GetLatestInsertDate(ByVal AGREE_BRAND_ID As String) As Object
            Try
                Query = "SET NOCOUNT ON;SELECT TOP 1 CREATE_DATE FROM AGREE_DISC_HISTORY WHERE AGREE_BRANDPACK_ID LIKE '" & AGREE_BRAND_ID & "%' ORDER BY CREATE_DATE DESC ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Return Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function getAgreementStartDate(ByVal AGREE_BRAND_ID As String) As Object
            Try
                Dim START_DATE As Object = Nothing
                Dim AGREEMENT_NO As Object = Me.getAgreeMentNO(AGREE_BRAND_ID)
                If Not IsNothing(AGREEMENT_NO) Then
                    If IsNothing(Me.SqlCom) Then
                        Me.CreateCommandSql("sp_executesql", "")
                    Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    End If
                    Query = "SET NOCOUNT ON; SELECT START_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "' ;"
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    START_DATE = Me.ExecuteScalar()
                End If
                Return START_DATE
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function GetAgreementEndDate(ByVal AGREEMENT_NO As String) As Object
            Try
                Dim END_DATE As Object = Nothing
                Query = "SET NOCOUNT ON; SELECT END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "' ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                END_DATE = Me.ExecuteScalar()
                Return END_DATE
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function getEndDateGivenAgreeBrandID(ByVal AGREE_BRAND_ID As String) As Object
            Try
                Query = "SET NOCOUNT ON; SELECT TOP 1 START_DATE FROM GIVEN_STORY WHERE AGREE_BRAND_ID = '" & AGREE_BRAND_ID & "' ORDER BY START_DATE DESC ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Return Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
#End Region

    End Class

End Namespace

