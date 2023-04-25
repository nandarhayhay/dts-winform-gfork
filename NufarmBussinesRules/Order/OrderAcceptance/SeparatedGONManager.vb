Imports System.Data.SqlClient
Namespace OrderAcceptance
    Public Class SeparatedGONManager
        Inherits SeparatedGON
        Public Function PopulateQueryPendingGon(ByVal SearchBy As String, ByVal value As Object, _
               ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer, _
               ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes) As DataView
            Try
                Dim dtTable As New DataTable("GON_OTHERS") : dtTable.Clear()
                Me.OpenConnection()
                common.CommonClass.columnKey = SearchBy
                Query = "SET NOCOUNT ON; SELECT TOP " & PageSize & " * " & vbCrLf & _
                   " FROM uv_gon_separated_PO_Pending_Gon " & vbCrLf & _
                   " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & _
                   " FROM uv_gon_separated_PO_Pending_Gon WHERE (" & SearchBy
                Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query &= ") ORDER BY IDApp DESC)"
                Query &= " AND " & SearchBy
                Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query &= " ORDER BY IDApp DESC OPTION(KEEP PLAN);"

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.SqlCom.ExecuteScalar()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                If value = "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('GON_SEPARATED_DETAIL') AND (index_id=0 or index_id=1) ;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Else
                    Query = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM uv_gon_separated_PO_Pending_Gon WHERE " & SearchBy
                    Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                End If
                Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters() : Me.CloseConnection()
                If (dtTable.Rows.Count > 0) Then : Else : Return Nothing : End If
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function PopulateQueryStatusCompleted(ByVal SearchBy As String, ByVal value As Object, _
       ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer, _
       ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes) As DataView
            Try
                Dim dtTable As New DataTable("GON_OTHERS") : dtTable.Clear()
                Me.OpenConnection()
                common.CommonClass.columnKey = SearchBy
                Query = "SET NOCOUNT ON; SELECT TOP " & PageSize & " * " & vbCrLf & _
                   " FROM uv_gon_separated_PO_Status_Completed " & vbCrLf & _
                   " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & _
                   " FROM uv_gon_separated_PO_Status_Completed WHERE (" & SearchBy
                Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query &= ") ORDER BY IDApp DESC)"
                Query &= " AND " & SearchBy
                Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query &= " ORDER BY IDApp DESC OPTION(KEEP PLAN);"

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.SqlCom.ExecuteScalar()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                If value = "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('GON_SEPARATED_DETAIL') AND (index_id=0 or index_id=1) ;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Else
                    Query = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM uv_gon_separated_PO_Status_Completed WHERE " & SearchBy
                    Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                End If
                Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters() : Me.CloseConnection()
                If (dtTable.Rows.Count > 0) Then : Else : Return Nothing : End If
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function PopulateQuery(ByVal SearchBy As String, ByVal value As Object, _
               ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer, _
               ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes) As DataView
            Try
                Dim dtTable As New DataTable("GON_OTHERS") : dtTable.Clear()
                Me.OpenConnection()
                common.CommonClass.columnKey = SearchBy
                Query = "SET NOCOUNT ON; SELECT TOP " & PageSize & " * " & vbCrLf & _
                   " FROM uv_gon_separated_PO " & vbCrLf & _
                   " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & _
                   " FROM uv_gon_separated_PO WHERE (" & SearchBy
                Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query &= ") ORDER BY IDApp DESC)"
                Query &= " AND " & SearchBy
                Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query &= " ORDER BY IDApp DESC OPTION(KEEP PLAN);"

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.SqlCom.ExecuteScalar()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                If value = "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('GON_SEPARATED_DETAIL') AND (index_id=0 or index_id=1) ;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Else
                    Query = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM uv_gon_separated_PO WHERE " & SearchBy
                    Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                End If
                Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters() : Me.CloseConnection()
                If (dtTable.Rows.Count > 0) Then : Else : Return Nothing : End If
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function PopulateQueryAllData(ByVal SearchBy As String, ByVal value As Object, _
                       ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer, _
                       ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes) As DataView
            Try
                Dim dtTable As New DataTable("GON_OTHERS") : dtTable.Clear()
                Me.OpenConnection()
                common.CommonClass.columnKey = SearchBy
                Query = "SET NOCOUNT ON; SELECT TOP " & PageSize & " * " & vbCrLf & _
                   " FROM uv_gon_separated_PO_All " & vbCrLf & _
                   " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & _
                   " FROM uv_gon_separated_PO_All WHERE (" & SearchBy
                Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query &= ") ORDER BY IDApp DESC)"
                Query &= " AND " & SearchBy
                Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query &= " ORDER BY IDApp DESC OPTION(KEEP PLAN);"

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.SqlCom.ExecuteScalar()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                If value = "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('GON_SEPARATED_DETAIL') AND (index_id=0 or index_id=1) ;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Else
                    Query = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM uv_gon_separated_PO_All WHERE " & SearchBy
                    Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                End If
                Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters() : Me.CloseConnection()
                If (dtTable.Rows.Count > 0) Then : Else : Return Nothing : End If
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        ''' <summary>
        ''' bila IDApp ada maka Gon_Number harus di sertakan
        ''' </summary>
        ''' <param name="IDApp"></param>
        ''' <param name="IDAppPODetail">Jika di isi User harus User HO</param>
        ''' <param name="PO_NUMBER"></param>
        ''' <param name="GON_NUMBER">jika di isi User Harus HO User</param>
        ''' <param name="isHoUser"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function delete(ByVal IDApp As Integer, ByRef mustReload As Boolean, Optional ByVal IDAppPODetail As Integer = 0, Optional ByVal PO_NUMBER As String = "", Optional ByVal GON_NUMBER As Object = Nothing, Optional ByVal isHoUser As Boolean = False, Optional ByVal isSystemAdmin As Boolean = False) As Boolean
            Try
                Me.OpenConnection() : BeginTransaction()
                Me.SqlCom.Transaction = Me.SqlTrans
                If IDApp > 0 Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    "DECLARE @FKApp INT ;" & vbCrLf & _
                    "SET @FKApp = (SELECT TOP 1 FKAppPoDetail FROM GON_SEPARATED_DETAIL WHERE IDApp = @IDApp);" & vbCrLf & _
                    "DELETE FROM GON_SEPARATED_DETAIL WHERE IDApp = @IDApp;" & vbCrLf & _
                    " SELECT FKApp = @FKApp;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                    Dim FKAppPODetail As Object = Me.SqlCom.ExecuteScalar()
                    'Me.ClearCommandParameters()
                    ''UPDATE STATUS
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " DECLARE @V_TOTAL_GON DECIMAL(18,4), @V_STATUS VARCHAR(30),@V_PO_ORIGINAL DECIMAL(18,4); " & vbCrLf & _
                    " SET @V_PO_ORIGINAL = (SELECT TOP 1 QUANTITY FROM GON_SEPARATED_PO_DETAIL WHERE IDApp =@FKApp); " & vbCrLf & _
                    " SET @V_TOTAL_GON = (SELECT ISNULL(SUM(QUANTITY),0)FROM GON_SEPARATED_PO_DETAIL WHERE FKApp = @FKApp AND IDApp != @IDApp);" & vbCrLf & _
                    " IF (@V_TOTAL_GON > 0) AND (@V_TOTAL_GON < @V_PO_ORIGINAL) SET @V_STATUS ='Partial'; " & vbCrLf & _
                    " ELSE IF @V_TOTAL_GON <= 0 SET @V_STATUS = 'Pending' ;" & vbCrLf & _
                    " ELSE IF @V_TOTAL_GON >= @V_PO_ORIGINAL SET @V_STATUS = 'Completed' ;" & vbCrLf & _
                    " UPDATE GON_SEPARATED_PO_DETAIL SET STATUS = @V_STATUS,ModifiedBy =@ModifiedBy,ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),101) WHERE IDApp = @FKApp;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@FKApp", SqlDbType.Int, FKAppPODetail)
                    Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)

                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    If Not IsNothing(GON_NUMBER) And Not IsDBNull(GON_NUMBER) Then
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " DECLARE @IDAppGonHeader INT;" & vbCrLf & _
                        " SET @IDAppGonHeader = (SELECT TOP 1 IDApp FROM GON_SEPARATED_HEADER WHERE GON_NUMBER = @GON_NUMBER);" & vbCrLf & _
                        " IF NOT EXISTS(SELECT FKAppGonHeader FROM GON_SEPARATED_DETAIL WHERE FKAppGonHeader =@IDAppGonHeader)" & vbCrLf & _
                        " BEGIN DELETE FROM GON_SEPARATED_HEADER WHERE IDApp = @IDAppGonHeader; END "
                        'If Not IsNothing(FKAppPODetail) And Not IsDBNull(FKAppPODetail) Then
                        '    Me.ResetCommandText(CommandType.Text, Query)
                        '    Me.AddParameter("@GON_NUMBER", SqlDbType.Int, GON_NUMBER)
                        '    Me.SqlCom.ExecuteScalar()
                        'End If
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@GON_NUMBER", SqlDbType.VarChar, GON_NUMBER)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    End If
                    If Not Me.hasItemInSeparatedDetail(GON_NUMBER, False) Then
                        mustReload = True
                    End If
                ElseIf IDAppPODetail > 0 Then
                    If isHoUser Or isSystemAdmin Then
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " DECLARE @V_FKAppPOHeader INT ;" & vbCrLf & _
                        " SET @V_FKAppPOHeader = (SELECT TOP 1 FKApp FROM GON_SEPARATED_PO_DETAIL WHERE IDApp = @IDAppPODetail);" & vbCrLf & _
                        " DELETE FROM GON_SEPARATED_PO_DETAIL WHERE IDApp = @IDAppPODetail ;" & vbCrLf & _
                        " SELECT IDAppPOHeader = @V_FKAppPOHeader ;"
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                        Else : Me.ResetCommandText(CommandType.Text, Query)
                        End If
                        Me.AddParameter("@IDAppPODetail", SqlDbType.Int, IDAppPODetail)
                        Dim FKAppPOHeader As Object = Me.SqlCom.ExecuteScalar()
                        Me.ClearCommandParameters()
                        If Not IsNothing(FKAppPOHeader) And Not IsDBNull(FKAppPOHeader) Then
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "DECLARE @V_MUST_RELOAD INT;" & vbCrLf & _
                            "IF NOT EXISTS(SELECT FKApp FROM GON_SEPARATED_PO_DETAIL WHERE FKApp = @FKApp)" & vbCrLf & _
                            " BEGIN SET @V_MUST_RELOAD = 1; DELETE FROM GON_SEPARATED_PO_HEADER WHERE IDApp = @FKApp; END" & vbCrLf & _
                            "SELECT MUST_RELOAD = @V_MUST_RELOAD;"
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@FKApp", SqlDbType.Int, FKAppPOHeader)
                            Dim retval As Object = Me.SqlCom.ExecuteScalar()
                            If Not IsNothing(retval) And Not IsDBNull(retval) Then
                                mustReload = CInt(retval) > 0
                            End If
                            Me.ClearCommandParameters()
                        End If
                    Else
                        System.Windows.Forms.MessageBox.Show("Sorry you haven't had a privilege to do such operation", "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                        Me.RollbackTransaction()
                        Return False
                    End If
                ElseIf PO_NUMBER <> "" Then
                    If isHoUser Or isSystemAdmin Then
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " DECLARE @V_FKApp INT ;" & vbCrLf & _
                        "DECLARE @V_MUST_RELOAD INT;" & vbCrLf & _
                        " SELECT @V_FKApp = (SELECT IDApp FROM GON_SEPARATED_HEADER WHERE PO_NUMBER = @PO_NUMBER);" & vbCrLf & _
                        "IF NOT EXISTS(SELECT FKApp FROM GON_SEPARATED_PO_DETAIL WHERE FKApp = @V_FKApp)" & vbCrLf & _
                        " BEGIN SET @V_MUST_RELOAD = 1; DELETE FROM GON_SEPARATED_PO_HEADER WHERE PO_NUMBER = @PO_NUMBER; END" & vbCrLf & _
                        " SELECT MUST_RELOAD = @V_MUST_RELOAD;"
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                        Else : Me.ResetCommandText(CommandType.Text, Query)
                        End If
                        Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PO_NUMBER)
                        Dim retval As Object = Me.SqlCom.ExecuteScalar()
                        If Not IsNothing(retval) And Not IsDBNull(retval) Then
                            mustReload = CInt(retval) > 0
                        End If
                        Me.ClearCommandParameters()
                    Else
                        System.Windows.Forms.MessageBox.Show("Sorry you haven't had a privilege to do such operation", "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                        Me.RollbackTransaction()
                        Return False
                    End If
                End If
                Me.CommiteTransaction()

                Return True
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function hasItemInSeparatedDetail(ByVal GON_NUMBER As String, ByVal CloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT GSD.IDApp FROM GON_SEPARATED_DETAIL GSD INNER JOIN GON_SEPARATED_HEADER GSH ON GSH.IDApp = GSD.FKAppGonHeader WHERE GSH.GON_NUMBER = @GON_NUMBER);"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@GON_NUMBER", SqlDbType.VarChar, GON_NUMBER)
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return True
                End If
                Me.ClearCommandParameters()
                If CloseConnection Then
                    Me.CloseConnection()
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace

