Imports System.Data.SqlClient
Imports Nufarm.Domain
Namespace Program
    Public Class Addjustment : Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Query As String = ""
        ''' <summary>
        ''' function untuk membinding kan ke dropdown datagrid dropdown distributor
        ''' </summary>
        ''' <param name="mustCloseConnection">Close / not the connection after data table filled</param>
        ''' <returns>data table</returns>
        ''' <remarks></remarks>
        Public Function getBrandPack(ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT BR.BRANDPACK_ID,BR.BRANDPACK_NAME FROM BRND_BRANDPACK BR WHERE EXISTS(SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE BRANDPACK_ID = BR.BRANDPACK_ID) " & vbCrLf & _
                        " AND IsActive = 1 AND (IsObsolete = 0 OR IsObsolete IS NULL);"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim dt As New DataTable("T_BrandPack")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return Nothing
        End Function
        Public Function GetHistoryAdjustment(ByVal StartDate As DateTime, ByVal EndDate As DateTime) As DataTable
            Try
                'START_PERIODE
                'END_PERIODE
                'ADJUSTMENT_FOR
                'ADJ_DESCRIPTION
                'DISTRIBUTOR_NAME
                'BRANDPACK_NAME
                'MAX_QTY
                'RELEASE_QTY
                'RELEASE_PO_REF_NO
                'CREATE_PO_BY
                'CREATE_PO_DATE
                'ISGROUP
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT AH.START_DATE,AH.END_DATE,CASE AH.TypeApp WHEN 'RP' THEN 'RETAILER PROGRAM' WHEN 'DPD' THEN 'DPD' ELSE '' END AS ADJUSTMENT_FOR,AH.NameApp AS ADJ_DESCRIPTION,DR.DISTRIBUTOR_NAME, " & vbCrLf & _
                        " BP.BRANDPACK_NAME,AH.QTY AS MAX_QTY,AT.IDApp,AT.ADJ_DISC_QTY AS RELEASE_QTY,PO.PO_REF_NO AS RELEASE_PO_REF_NO,PO.CREATE_BY AS CREATE_PO_BY,  " & vbCrLf & _
                        " PO.CREATE_DATE AS CREATE_PO_DATE,AH.IsGroup " & vbCrLf & _
                        " FROM ADJUSTMENT_DPD AH INNER JOIN ADJUSTMENT_TRANS AT ON AH.IDApp = AT.FKApp INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = AT.PO_REF_NO AND AH.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID " & vbCrLf & _
                        " INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID " & vbCrLf & _
                        " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = AH.BRANDPACK_ID " & vbCrLf & _
                        " WHERE AH.DISTRIBUTOR_ID IS NOT NULL AND AH.IsGroup = 0 AND PO.PO_REF_DATE >= @StartDate AND PO.PO_REF_DATE <= @EndDate AND AT.ADJ_DISC_QTY > 0 "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Dim tbl1 As New DataTable("HISTORY_OF_ADJUSTMENT")
                Me.setDataAdapter(Me.SqlCom).Fill(tbl1)
                Dim column1 As DataColumn = tbl1.Columns("IDApp")

                tbl1.PrimaryKey = New DataColumn() {column1}
                tbl1.AcceptChanges()

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT AH.START_DATE,AH.END_DATE,CASE AH.TypeApp WHEN 'RP' THEN 'RETAILER PROGRAM' WHEN 'DPD' THEN 'DPD' ELSE '' END AS ADJUSTMENT_FOR,AH.NameApp AS ADJ_DESCRIPTION,DR.DISTRIBUTOR_NAME, " & vbCrLf & _
                        " BP.BRANDPACK_NAME,AH.QTY AS MAX_QTY,AT.IDApp,AT.ADJ_DISC_QTY AS RELEASE_QTY,PO.PO_REF_NO AS RELEASE_PO_REF_NO,PO.CREATE_BY AS CREATE_PO_BY,  " & vbCrLf & _
                        " PO.CREATE_DATE AS CREATE_PO_DATE,AH.IsGroup " & vbCrLf & _
                        " FROM ADJUSTMENT_DPD AH INNER JOIN ADJUSTMENT_DPD_DIST AD ON AH.IDApp = AD.FKApp " & vbCrLf & _
                        " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.DISTRIBUTOR_ID = AD.DISTRIBUTOR_ID INNER JOIN ADJUSTMENT_TRANS AT ON AT.PO_REF_NO = PO.PO_REF_NO AND AT.FKApp = AH.IDApp AND AT.FKApp = AD.FKApp " & vbCrLf & _
                        " INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID " & vbCrLf & _
                        " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = AH.BRANDPACK_ID " & vbCrLf & _
                        " WHERE AH.DISTRIBUTOR_ID IS NULL AND AH.IsGroup = 1 AND PO.PO_REF_DATE >= @StartDate AND PO.PO_REF_DATE <= @EndDate AND AT.ADJ_DISC_QTY > 0 ; "
                Me.ResetCommandText(CommandType.Text, Query)
                Dim tbl2 As DataTable = tbl1.Clone()
                tbl2.Clear()
                tbl2.AcceptChanges()
                setDataAdapter(Me.SqlCom).Fill(tbl2)
                tbl2.AcceptChanges()

                Me.ClearCommandParameters() : Me.CloseConnection()
                tbl1.Merge(tbl2)
                tbl1.AcceptChanges()
                Return tbl1
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getBrandPack(ByVal ListDistributors As List(Of String), Optional ByVal SearchBrandPackName As String = "") As DataTable
            Try
                'Dim rowFilter As String = " = SOME("
                'For i As Integer = 0 To ListDistributors.Count - 1
                '    rowFilter &= "SELECT DISTRIBUTOR_ID = '" & ListDistributors(i) & "'"
                '    If i < ListDistributors.Count - 1 Then
                '        rowFilter &= " UNION " & vbCrLf
                '    End If
                'Next
                'rowFilter &= ")"
                Dim tbl1 As New DataTable("T_BrandPack")

                If SearchBrandPackName <> "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                             " SELECT BR.BRANDPACK_ID,BR.BRANDPACK_NAME FROM BRND_BRANDPACK BR WHERE EXISTS(SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE BRANDPACK_ID = BR.BRANDPACK_ID) " & vbCrLf & _
                             " AND IsActive = 1 AND (IsObsolete = 0 OR IsObsolete IS NULL) AND EXISTS( " & vbCrLf & _
                             " SELECT ABI.BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE ABI INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                             " WHERE DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ABI.BRANDPACK_ID = BR.BRANDPACK_ID) " & vbCrLf & _
                             " AND BR.BRANDPACK_NAME LIKE '%'+@BrandPackName+'%' ;"
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                           " SELECT BR.BRANDPACK_ID,BR.BRANDPACK_NAME FROM BRND_BRANDPACK BR WHERE EXISTS(SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE BRANDPACK_ID = BR.BRANDPACK_ID) " & vbCrLf & _
                           " AND IsActive = 1 AND (IsObsolete = 0 OR IsObsolete IS NULL) AND EXISTS( " & vbCrLf & _
                           " SELECT ABI.BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE ABI INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                           " WHERE DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ABI.BRANDPACK_ID = BR.BRANDPACK_ID) ;"
                End If
                Me.OpenConnection()
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                'Me.AddParameter("@DistributorID", SqlDbType.VarChar, DistributorID)
                If SearchBrandPackName <> "" Then
                    Me.AddParameter("@BrandPackName", SqlDbType.VarChar, SearchBrandPackName)
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, ListDistributors(0), 10)
                setDataAdapter(Me.SqlCom).Fill(tbl1)
                Dim column1 As DataColumn = tbl1.Columns("BRANDPACK_ID")

                tbl1.PrimaryKey = New DataColumn() {column1}
                tbl1.AcceptChanges()
                Dim tbl2 As DataTable = tbl1.Clone()
                ListDistributors.RemoveAt(0)
                For i As Integer = 0 To ListDistributors.Count - 1
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, ListDistributors(i), 10)
                    tbl2.Clear()
                    setDataAdapter(Me.SqlCom).Fill(tbl2)
                    tbl2.AcceptChanges()
                    tbl1.Merge(tbl2)
                    tbl1.AcceptChanges()
                Next
                Me.ClearCommandParameters() : Me.CloseConnection()
                Return tbl1
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return Nothing
        End Function

        Public Function getBrandPack(ByVal DistributorID As String, ByVal mustCloseConnection As Boolean, Optional ByVal SearchBrandPackName As String = "") As DataTable
            Try
                If SearchBrandPackName <> "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                             " SELECT BR.BRANDPACK_ID,BR.BRANDPACK_NAME FROM BRND_BRANDPACK BR WHERE EXISTS(SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE BRANDPACK_ID = BR.BRANDPACK_ID) " & vbCrLf & _
                             " AND IsActive = 1 AND (IsObsolete = 0 OR IsObsolete IS NULL) AND EXISTS( " & vbCrLf & _
                             " SELECT ABI.BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE ABI INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO WHERE DA.DISTRIBUTOR_ID = @DistributorID AND ABI.BRANDPACK_ID = BR.BRANDPACK_ID) " & vbCrLf & _
                             " AND BR.BRANDPACK_NAME LIKE '%'+@BrandPackName+'%' ;"
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                           " SELECT BR.BRANDPACK_ID,BR.BRANDPACK_NAME FROM BRND_BRANDPACK BR WHERE EXISTS(SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE BRANDPACK_ID = BR.BRANDPACK_ID) " & vbCrLf & _
                           " AND IsActive = 1 AND (IsObsolete = 0 OR IsObsolete IS NULL) AND EXISTS( " & vbCrLf & _
                           " SELECT ABI.BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE ABI INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO WHERE DA.DISTRIBUTOR_ID = @DistributorID AND ABI.BRANDPACK_ID = BR.BRANDPACK_ID) ;"
                End If

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@DistributorID", SqlDbType.VarChar, DistributorID)
                If SearchBrandPackName <> "" Then
                    Me.AddParameter("@BrandPackName", SqlDbType.VarChar, SearchBrandPackName)
                End If
                Me.OpenConnection()
                Dim dt As New DataTable("T_BrandPack")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return Nothing
        End Function
        Public Sub GetCurrentPeriode(ByRef StartDate As DateTime, ByRef EndDate As DateTime)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT TOP 1 START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE START_DATE <= CONVERT(VARCHAR(100),GETDATE(),101) AND END_DATE >= CONVERT(VARCHAR(100),GETDATE(),101) ORDER BY END_DATE DESC ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteReader()
                Dim OStartDate As Object = Nothing
                Dim OEndDate As Object = Nothing
                While Me.SqlRe.Read()
                    OStartDate = SqlRe("START_DATE")
                    OEndDate = SqlRe("END_DATE")
                End While : Me.SqlRe.Close()
                Me.ClearCommandParameters() : Me.CloseConnection()
                If Not IsNothing(OStartDate) And Not IsDBNull(OStartDate) Then
                    StartDate = Convert.ToDateTime(OStartDate)
                End If
                If Not IsNothing(OEndDate) And Not IsDBNull(OEndDate) Then
                    EndDate = Convert.ToDateTime(OEndDate)
                End If
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        ''' <summary>
        ''' Function untuk mengambil data di table ADJUSTMENT_DPD_DIST
        ''' </summary>
        ''' <param name="IDApp"></param>
        ''' <returns></returns>
        ''' <remarks>Function ini hanya di panggil di mode open/edit saja</remarks>
        Public Function getDistributorGroup(ByVal IDApp As Integer) As DataTable
            Query = " SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT AD.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM ADJUSTMENT_DPD_DIST AD INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = AD.DISTRIBUTOR_ID WHERE AD.FKApp = @IDApp"
            If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
            Else : Me.ResetCommandText(CommandType.Text, Query)
            End If
            Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
            Try

                Me.OpenConnection()
                Dim tblDistInclude As New DataTable("DISTRIBUTOR_INCLUDED")
                setDataAdapter(Me.SqlCom).Fill(tblDistInclude)
                'Dim distributors() As Object
                'distributors = New Object() {}
                'For i As Integer = 0 To tblDistInclude.Rows.Count - 1
                '    distributors(i) = tblDistInclude.Rows(i)("DISTRIBUTOR_ID")
                'Next
                'Return distributors
                Me.ClearCommandParameters() : Me.CloseConnection()
                Return tblDistInclude
            Catch ex As Exception
                Me.ClearCommandParameters() : Me.CloseConnection() : Throw ex
            End Try
            Return Nothing
        End Function
        Public Function getDistributorGroup(ByVal SearchDistributor As String, ByVal MustCloseConnection As Boolean) As DataTable
            Try
                If SearchDistributor <> "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS( " & vbCrLf & _
                        " SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                        " AND DISTRIBUTOR_NAME LIKE '%SANTANI%' OR DISTRIBUTOR_NAME LIKE '%PANCA AGRO NIAGA%' OR DISTRIBUTOR_NAME LIKE '%BUMI INTAN JAYA%';"
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                           " SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS( " & vbCrLf & _
                           " SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) AND DR.DISTRIBUTOR_NAME LIKE '%" & SearchDistributor & "%' ;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim dt As New DataTable("T_Distributors")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If MustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function getDistributor(ByVal MustCloseConnection As Boolean, ByVal SearchDistributor As String) As DataTable
            Try
                If (SearchDistributor = "") Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                          " SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS( " & vbCrLf & _
                          " SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID);"
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                           " SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS( " & vbCrLf & _
                           " SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) AND DR.DISTRIBUTOR_NAME LIKE '%" & SearchDistributor & "%' ;"
                End If

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim dt As New DataTable("T_Distributor")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If MustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getAddjustmentData(ByVal mustCloseConnection As Boolean, ByVal StartDate As DateTime, ByVal EndDate As DateTime, Optional ByVal DistributorID As String = "")
            Try
                If DistributorID = "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                              " SELECT IDApp,CodeApp,DISTRIBUTOR_ID,BRANDPACK_ID,QTY AS QUANTITY,ADJUSTMENT_FOR = CASE WHEN (TypeApp = 'DPD') THEN 'DPD' ELSE 'RETAILER PROGRAM' END,START_DATE AS START_PERIODE," & vbCrLf & _
                              " END_DATE AS END_PERIODE,NameApp AS ADJ_DESCRIPTION,LEFT_QTY,RELEASE_QTY,IsGroup, CASE GroupCode WHEN 'PAN' THEN 'PANCA AGRO NIAGA' WHEN 'SAN' THEN 'SANTANI' WHEN 'BIJ' THEN 'BUMI INTAN JAYA' ELSE '' END AS GroupName, CreatedBy AS CREATE_BY,CreatedDate AS CREATE_DATE " & vbCrLf & _
                              " FROM ADJUSTMENT_DPD WHERE START_DATE >= @StartDate AND END_DATE <= @EndDate ORDER BY CreatedDate DESC  ;"
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                              " SELECT IDApp,CodeApp,DISTRIBUTOR_ID,BRANDPACK_ID,QTY AS QUANTITY,ADJUSTMENT_FOR = CASE WHEN (TypeApp = 'DPD') THEN 'DPD' ELSE 'RETAILER PROGRAM' END,START_DATE AS START_PERIODE," & vbCrLf & _
                              " END_DATE AS END_PERIODE,NameApp AS ADJ_DESCRIPTION,LEFT_QTY,RELEASE_QTY,IsGroup,CreatedBy AS CREATE_BY,CreatedDate AS CREATE_DATE " & vbCrLf & _
                              " FROM ADJUSTMENT_DPD WHERE START_DATE >= @StartDate AND END_DATE <= @EndDate AND DISTRIBUTOR_ID = @DistributorID ORDER BY CreatedDate DESC  ;"
                End If

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                If Not String.IsNullOrEmpty(DistributorID) Then
                    Me.AddParameter("@DistributorID", SqlDbType.VarChar, DistributorID)
                End If
                Me.OpenConnection()
                Dim dt As New DataTable("T_BrandPack")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        ''' <summary>
        ''' Query untuk mengecek apakah data sudah di pakai dalam sebuah transaction
        ''' </summary>
        ''' <param name="IDApp">Primary key</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function hasUsedInTransaction(ByVal IDApp As Integer, ByVal IsGroup As Boolean) As Boolean
            Try
                If Not IsGroup Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                                  " SELECT 1 WHERE EXISTS(SELECT IDApp FROM ADJUSTMENT_TRANS WHERE FKApp = @IDApp AND ADJ_DISC_QTY > 0) " & vbCrLf & _
                                  "           OR EXISTS(SELECT FKCodeAdjust FROM ORDR_OA_REMAINDING WHERE FKCodeAdjust = @IDApp) ;"
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT 1 WHERE EXISTS(SELECT ADJ.PO_REF_NO FROM ADJUSTMENT_TRANS ADJ INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = ADJ.PO_REF_NO " & vbCrLf & _
                            " WHERE PO.DISTRIBUTOR_ID = ANY(SELECT DISTRIBUTOR_ID FROM ADJUSTMENT_DPD_DIST WHERE FKApp = @IDApp) AND ADJ.FKApp = @IDApp)  " & vbCrLf & _
                            " OR EXISTS(SELECT FKCodeAdjust FROM ORDR_OA_REMAINDING WHERE FKCodeAdjust = @IDApp) ;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return (CInt(retval) > 0)
                End If
                Me.CloseConnection()
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub Delete(ByVal IDApp As Integer, ByVal IsGroup As Boolean, ByVal DISTRIBUTOR_ID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByRef mustReload As Boolean, ByVal mustCloseConnection As Boolean)
            Try

                If IsGroup Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " DECLARE = @Result INT; " & vbCrLf & _
                            " DELETE FROM ORDR_OA_REMAINDING WHERE FKCodeAdjust = @IDApp AND OA_BRANDPACK_ID = ANY(SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID " & vbCrLf & _
                            " = ANY(SELECT PO_BRANDPACK_ID FROM ORDR_PO_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID AND PO_REF_NO = ANY(SELECT " & vbCrLf & _
                            "	PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID  AND PO_REF_DATE >= @StartDate AND PO_REF_DATE <= @EndDate))); " & vbCrLf & _
                            " DELETE FROM ADJUSTMENT_TRANS WHERE FKApp = @IDApp AND PO_REF_NO = " & vbCrLf & _
                            " ANY(PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND PO_REF_DATE >= @StartDate AND PO_REF_DATE <= @EndDate); " & vbCrLf & _
                            " IF NOT EXISTS(SELECT FKApp FROM ADJUSTMENT_DPD_DIST WHERE FKApp = @IDApp) " & vbCrLf & _
                            " BEGIN  " * vbCrLf & _
                            " DELETE FROM ADJUSTMENT_DPD WHERE IDApp = @IDApp ;" & vbCrLf & _
                            " SET @Result = 1; " & vbCrLf & _
                            " END " & vbCrLf & _
                            " SELECT Result = @Result ;"
                Else
                    Query = "SET  NOCOUNT ON; " & vbCrLf & _
                            " DELETE FROM ADJUSTMENT_TRANS WHERE FKApp = @IDApp ;" & vbCrLf & _
                            " DELETE FROM ORDR_OA_REMAINDING WHERE FKCodeAdjust = @IDApp ;" & vbCrLf & _
                            " DELETE FROM ADJUSTMENT_DPD WHERE IDApp = @IDApp ;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                If IsGroup Then
                    Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                    Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                End If
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    If CInt(retval) > 0 Then
                        mustReload = True
                    End If
                End If
                If Not mustReload Then
                    If mustCloseConnection Then : Me.CloseConnection() : End If
                End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Sub SaveData(ByVal Adj As Nufarm.Domain.Adjustment, ByVal Mode As NufarmBussinesRules.common.Helper.SaveMode, ByRef AutoIdentity As Integer, ByVal MustCloseConnection As Boolean)
            Try
                If Mode = common.Helper.SaveMode.Insert Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " IF EXISTS(SELECT CodeApp FROM ADJUSTMENT_DPD WHERE CodeApp = @CodeApp) " & vbCrLf & _
                            " BEGIN RAISERROR('Data has existed !',16,1) ; RETURN ; END " & vbCrLf & _
                            " INSERT INTO ADJUSTMENT_DPD(CodeApp,TypeApp, NameApp, DISTRIBUTOR_ID, BRANDPACK_ID, QTY, RELEASE_QTY, LEFT_QTY, START_DATE, END_DATE, CreatedDate, CreatedBy,IsGroup,GroupCode)" & vbCrLf & _
                            " VALUES(@CodeApp,@TypeApp, @NameApp, @DISTRIBUTOR_ID, @BRANDPACK_ID, @QTY, 0, @LEFT_QTY, @START_DATE, @END_DATE, @CreatedDate, @CreatedBy,@IsGroup,@GroupCode) ; " & vbCrLf & _
                            " SELECT [Identity] = @@IDENTITY ; "
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " UPDATE ADJUSTMENT_DPD SET QTY = @QTY,RELEASE_QTY = 0,LEFT_QTY = @QTY, NameApp = @NameApp,TypeApp = @TypeApp WHERE IDApp = @IDApp ;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@CodeApp", SqlDbType.VarChar, Adj.CodeApp, 110)
                Me.AddParameter("@TypeApp", SqlDbType.VarChar, Adj.TypeApp)
                Me.AddParameter("@NameApp", SqlDbType.VarChar, Adj.NameApp, 100)
                If String.IsNullOrEmpty(Adj.DistributorID) Then
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DBNull.Value, 16)
                Else
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Adj.DistributorID, 16)
                End If
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, Adj.BrandPackID, 16)
                Me.AddParameter("@QTY", SqlDbType.Decimal, Adj.Quantity)
                Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, Adj.Quantity)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Adj.StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Adj.EndDate)
                Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, Adj.CreatedDate)
                Me.AddParameter("@CreatedBy", SqlDbType.VarChar, Adj.CreatedBy)
                Me.AddParameter("@IsGroup", SqlDbType.Bit, Adj.IsGroup)
                Me.AddParameter("@GroupCode", SqlDbType.VarChar, IIf(Adj.IsGroup = True, Adj.GroupCode, DBNull.Value))
                If Mode = common.Helper.SaveMode.Update Then
                    Me.AddParameter("@IDApp", SqlDbType.Int, Adj.IDApp)
                End If
                OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Mode = common.Helper.SaveMode.Insert Then
                    AutoIdentity = IIf((IsNothing(retval) Or IsDBNull(retval)), 0, Convert.ToInt32(retval))
                End If
                If Mode = common.Helper.SaveMode.Insert Then
                    If Adj.ListDistributors.Count > 0 Then ''berarti group distributor
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "INSERT INTO ADJUSTMENT_DPD_DIST(FKApp,DISTRIBUTOR_ID,CreatedBy,CreatedDate) " & vbCrLf & _
                                " VALUES(@FKApp,@DISTRIBUTOR_ID,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101));"
                        ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@FKApp", SqlDbType.Int, AutoIdentity)
                        Me.AddParameter("@CreatedBy", SqlDbType.VarChar, Adj.CreatedBy)
                        For i As Integer = 0 To Adj.ListDistributors.Count - 1
                            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Adj.ListDistributors(i), 10)
                            Me.SqlCom.ExecuteScalar()
                        Next
                    End If
                End If
                Me.CommiteTransaction() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
    End Class

End Namespace

