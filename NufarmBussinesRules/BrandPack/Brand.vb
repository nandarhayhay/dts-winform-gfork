Imports System.Data
Imports System.Data.SqlClient
Namespace Brandpack
    Public Class Brand
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Public Sub New()
            MyBase.New()
        End Sub
        Private m_dataView As DataView
        Private m_dataSet As DataSet
        Private m_dtViewAllBrand As DataView
        Protected Query As String = ""
        Public ReadOnly Property GetDataSet() As DataSet
            Get
                Return m_dataSet
            End Get
        End Property
        Public Function GetDataViewRowState(ByVal RowState As String) As DataView
            Select Case RowState
                Case "ModifiedAdded"
                    Me.m_dataView.RowStateFilter = DataViewRowState.Added
                Case "ModifiedOriginal"
                    Me.m_dataView.RowStateFilter = DataViewRowState.ModifiedOriginal
                Case "Deleted"
                    Me.m_dataView.RowStateFilter = DataViewRowState.Deleted
                Case "Current"
                    Me.m_dataView.RowStateFilter = DataViewRowState.CurrentRows
                Case "Unchaigned"
                    Me.m_dataView.RowStateFilter = DataViewRowState.Unchanged
                Case "OriginalRows"
                    Me.m_dataView.RowStateFilter = DataViewRowState.OriginalRows
            End Select
            Return Me.m_dataView
        End Function
        Public Sub SaveData(ByVal ds As DataSet)
            Try
                ' ds.AcceptChanges()
                'generate command to save data automatically
                Me.CreateCommandSql("", "SELECT * FROM BRND_BRAND")
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlDat.Update(ds.Tables(0))
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Function CreateAllDataViewBrand(ByVal MustReloadToAccPac As Boolean) As DataView
            Try
                'ambil brand yand ada di accpac ,bila brand belum ada di dts input kan ke dts,
                'bila brand dengan nama yang sama maka update statusnya jadi obsolete
                'untuk brand yang baru status semuanya active
                If MustReloadToAccPac Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                           "SELECT DISTINCT RIGHT(RTRIM(ITEM.SEGMENT1),1) + '' + RTRIM(ITEM.SEGMENT2) AS BRAND_ID" & vbCrLf & _
                           ",RTRIM(SUBSTRING(ITEM.[DESC],1,CHARINDEX('@',ITEM.[DESC],0)- 1))AS BRAND_NAME FROM NI87.dbo.ICITEM ITEM " & vbCrLf & _
                           " WHERE NOT EXISTS(SELECT BRAND_ID FROM Nufarm.dbo.BRND_BRAND WHERE BRAND_ID = " & vbCrLf & _
                           " RIGHT(RTRIM(ITEM.SEGMENT1),1) + '' + RTRIM(ITEM.SEGMENT2)) " & vbCrLf & _
                           " AND ITEM.INACTIVE = 0 AND RTRIM(ITEM.[DESC]) NOT LIKE '%OTHER%' AND ITEM.[DESC] LIKE '%@%'" & vbCrLf & _
                           " AND RTRIM(ITEM.[DESC]) NOT LIKE 'ROUNDUP%' OPTION(KEEP PLAN);"
                    Me.CreateCommandSql("sp_executesql", "") : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Dim dtTable As New DataTable("T_Brand1") : dtTable.Clear() : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                    Me.OpenConnection() : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()

                    If dtTable.Rows.Count > 0 Then
                        'INSERT DATA DENGAN STATUS = ACTIVE
                        Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                " DECLARE @V_CREATE_BY VARCHAR(50); " & vbCrLf & _
                                " SET @V_CREATE_BY = (SELECT TOP 1 AUDTUSER FROM NI87.DBO.ICITEM ITEM WHERE RIGHT(RTRIM(ITEM.SEGMENT1),1) + '' + RTRIM(ITEM.SEGMENT2) = @BRAND_ID); " & vbCrLf & _
                                " IF NOT EXISTS(SELECT BRAND_ID FROM BRND_BRAND WHERE BRAND_ID = @BRAND_ID)" & vbCrLf & _
                                "  BEGIN " & vbCrLf & _
                                "  INSERT INTO BRND_BRAND(BRAND_ID,BRAND_NAME,IsActive,IsObsolete,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                                "  VALUES(@BRAND_ID,@BRAND_NAME,1,0,@V_CREATE_BY,@CREATE_DATE);" & vbCrLf & _
                                " END"
                        Me.ResetCommandText(CommandType.Text, Query)
                        For i As Integer = 0 To dtTable.Rows.Count - 1

                            Me.AddParameter("@BRAND_NAME", SqlDbType.VarChar, dtTable.Rows(i)("BRAND_NAME"), 50)
                            Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, dtTable.Rows(i)("BRAND_ID"), 7)
                            Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Next
                    End If
                    'bikin table temporary supaya gak berat
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "IF OBJECT_ID('tempdb..##T_P_Brand') IS NULL " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " SELECT * INTO ##T_P_Brand " & vbCrLf & _
                            " FROM ( SELECT '02-2003' AS BRAND_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2006' AS BRAND_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2010' AS BRAND_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2011' AS BRAND_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2012' AS BRAND_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2014' AS BRAND_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2034' AS BRAND_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2039' AS BRAND_ID UNION ALL " & vbCrLf & _
                            "        SELECT '00898'   AS BRAND_ID " & vbCrLf & _
                            "        )T; " & vbCrLf & _
                            " CREATE CLUSTERED INDEX IDX_T_P_B_2 ON tempdb..##T_P_Brand(BRAND_ID); " & vbCrLf & _
                           " END "
                    Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()
                    'INSERT BRANDPACK GIBGRO SP
                 
                    Query = "SET NOCOUNT ON;SELECT BRAND_ID FROM BRND_BRAND WHERE BRAND_NAME LIKE 'ROUNDUP%' AND isActive = 1;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql") : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Dim tblbrand As New DataTable("T_P_Brand") : tblbrand.Clear() : Me.SqlDat.Fill(tblbrand)
                    Me.ClearCommandParameters()

                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "IF NOT EXISTS(SELECT BRAND_ID FROM tempdb..##T_P_Brand WHERE BRAND_ID = @BRAND_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "   INSERT INTO tempdb..##T_P_Brand(BRAND_ID) " & vbCrLf & _
                            "   VALUES(@BRAND_ID); " & vbCrLf & _
                            " END "
                    Me.ResetCommandText(CommandType.Text, Query)
                    If IsNothing(Me.SqlTrans) Then
                        Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                    End If

                    For i As Integer = 0 To tblbrand.Rows.Count - 1
                        Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, tblbrand.Rows(i)("BRAND_ID"), 7)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next

                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " UPDATE Nufarm.dbo.BRND_BRAND SET IsActive = 0 WHERE BRAND_ID = " & vbCrLf & _
                            "  ANY(SELECT BRAND_ID FROM Nufarm.dbo.BRND_BRAND BR " & vbCrLf & _
                            "      WHERE NOT EXISTS( " & vbCrLf & _
                            "                       SELECT RIGHT(RTRIM(SEGMENT1),1) + '' + RTRIM(SEGMENT2) " & vbCrLf & _
                            "                       FROM NI87.dbo.ICITEM WHERE INACTIVE = 0 AND " & vbCrLf & _
                            "                       RIGHT(RTRIM(SEGMENT1),1) + '' + RTRIM(SEGMENT2) = BR.BRAND_ID AND (RTRIM(ITEMBRKID) = 'FG' OR RTRIM(ITEMBRKID) = 'FGST') AND [DESC] NOT LIKE '%BULK%' AND UPPER([DESC]) NOT LIKE '%OTHER%' " & vbCrLf & _
                            "                      ) " & vbCrLf & _
                            "      AND NOT EXISTS(SELECT BRAND_ID FROM tempdb..##T_P_Brand WHERE BRAND_ID = BR.BRAND_ID) " & vbCrLf & _
                            "      );" & vbCrLf & _
                            "UPDATE Nufarm.dbo.BRND_BRAND SET IsActive = 1,IsObsolete = 1 WHERE BRAND_ID = " & vbCrLf & _
                            " ANY(SELECT BRAND_ID FROM Nufarm.dbo.BRND_BRAND BR " & vbCrLf & _
                            "     WHERE EXISTS(SELECT BRAND_ID FROM Nufarm.dbo.AGREE_BRAND_INCLUDE WHERE BRAND_ID = BR.BRAND_ID) " & vbCrLf & _
                            "     AND NOT EXISTS( " & vbCrLf & _
                            "                    SELECT RIGHT(RTRIM(SEGMENT1),1) + '' + RTRIM(SEGMENT2) " & vbCrLf & _
                            "                    FROM NI87.dbo.ICITEM WHERE INACTIVE = 0 AND " & vbCrLf & _
                            "                    RIGHT(RTRIM(SEGMENT1),1) + '' + RTRIM(SEGMENT2) = BR.BRAND_ID AND (RTRIM(ITEMBRKID) = 'FG' OR  RTRIM(ITEMBRKID) = 'FGST') AND [DESC] NOT LIKE '%BULK%' AND UPPER([DESC]) NOT LIKE '%OTHER%'" & vbCrLf & _
                            "                   ) " & vbCrLf & _
                            "     AND NOT EXISTS(SELECT BRAND_ID FROM tempdb..##T_P_Brand WHERE BRAND_ID = BR.BRAND_ID) " & vbCrLf & _
                            "     );"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Me.CommiteTransaction()
                End If
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT * FROM BRND_BRAND; "

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If

                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblAlBrand As New DataTable("T_AllBrand")
                tblAlBrand.Clear()
                Me.FillDataTable(tblAlBrand)
                Me.m_dataSet = New DataSet("DSBrand")
                Me.m_dataSet.Tables.Add(tblAlBrand)
                'Me.m_dtViewAllBrand = Me.m_dataSet.Tables(0).DefaultView()

                'Me.m_dataSet = MyBase.baseDataSet
                Me.m_dataView = MyBase.CreateDataView(m_dataSet.Tables(0))
                Me.m_dataView.RowStateFilter = DataViewRowState.CurrentRows
                Me.m_dataView.Sort = "BRAND_ID"
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_dataView
        End Function
        Public ReadOnly Property getAllDataViewBrand() As DataView
            Get
                Return Me.m_dtViewAllBrand
            End Get
        End Property
        Public Property ViewBrand() As DataView
            Get
                Return Me.m_dataView
            End Get
            Set(ByVal value As DataView)
                Me.m_dataView = value
            End Set
        End Property
        Public Function GetDataView() As DataView
            Try
                MyBase.FilDataSet("Sp_Select_Brand", "", "BrandDataSet")
                Me.m_dataSet = MyBase.baseDataSet
                Me.m_dataView = MyBase.CreateDataView(m_dataSet.Tables(0))
                Me.m_dataView.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_dataView.Sort = "BRAND_ID"
            Catch ex As Exception
                Throw ex
            End Try
            Return m_dataView
        End Function
        Public Function HasReferencedData(ByVal BRAND_ID) As Integer
            Dim RetVal As Integer = 0
            Try
                If Not IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Check_Brand_ID")
                Else : CreateCommandSql("Sp_Check_Brand_ID", "")
                End If
                Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, BRAND_ID, 7)
                'Me.AddParameter("@RETVAL", SqlDbType.Int, ParameterDirection.ReturnValue)
                'Me.SqlCom.Parameters.Add("@RETVAL", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                RetVal = CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE"))
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
            End Try
            Return RetVal
        End Function
        Public Overloads Sub dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_dataView) Then
                Me.m_dataView.Dispose()
                Me.m_dataView = Nothing
            End If
            If Not IsNothing(Me.m_dataSet) Then
                Me.m_dataSet.Dispose()
                Me.m_dataSet = Nothing
            End If
            MyBase.Dispose(True)
        End Sub
        Public Function hasReferencedClassID(ByVal ClassID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT TOP 1 CLASS_ID FROM PRODUCT_CLASS WHERE CLASS_ID = @CLASS_ID ;"
                CreateCommandSql("", Query)
                AddParameter("@CLASS_ID", SqlDbType.VarChar, ClassID, 10)
                OpenConnection()
                Dim Retval As Object = Me.SqlCom.ExecuteScalar() : ClearCommandParameters()
                If mustCloseConnection Then
                    CloseConnection()
                End If
                If ((Not IsNothing(Retval)) And (Not IsDBNull(Retval))) Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function hasExistsClassID(ByVal ClassID As String, ByVal MustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT TOP 1 CLASS_ID FROM T_CLASS WHERE CLASS_ID = @CLASS_ID; "
                CreateCommandSql("", Query)
                AddParameter("@CLASS_ID", SqlDbType.VarChar, ClassID, 10)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If MustCloseConnection Then
                    CloseConnection()
                End If
                If ((Not IsNothing(retval)) And (Not IsDBNull(retval))) Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub SaveChangesClass(ByVal tclass As DataTable, ByVal mustCloseConnection As Boolean)
            Try
                Dim InsertedRows() As DataRow = tclass.Select("", "", DataViewRowState.Added)
                Dim UpdatedRows() As DataRow = tclass.Select("", "", DataViewRowState.ModifiedOriginal)
                Dim DeletedRows() As DataRow = tclass.Select("", "", DataViewRowState.Deleted)
                Dim CommandInsert As SqlCommand = Nothing, CommandUpdate As SqlCommand = Nothing, CommandDelete As SqlCommand = Nothing

                If ((InsertedRows.Length > 0) Or (UpdatedRows.Length > 0) Or (DeletedRows.Length > 0)) Then
                    Me.OpenConnection() : Me.SqlDat = New SqlDataAdapter()
                    Me.BeginTransaction()
                Else
                    Throw New Exception("NO Changes have occured" & vbCrLf & "No Need Saving")
                End If

                If InsertedRows.Length > 0 Then
                    CommandInsert = Me.SqlConn.CreateCommand()
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "INSERT INTO T_CLASS (CLASS_ID, CLASS_NAME, INACTIVE, CREATE_BY, CREATE_DATE)" & vbCrLf & _
                            " VALUES(@CLASS_ID, @CLASS_NAME, @INACTIVE, @CREATE_BY, @CREATE_DATE) ;"
                    CommandInsert.Transaction = Me.SqlTrans : CommandInsert.CommandType = CommandType.Text : CommandInsert.CommandText = Query
                    With CommandInsert
                        .Parameters.Add("@CLASS_ID", SqlDbType.VarChar, 10, "CLASS_ID")
                        .Parameters.Add("@CLASS_NAME", SqlDbType.VarChar, 100, "CLASS_NAME")
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50, "CREATE_BY")
                        .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_DATE")
                        Me.SqlDat.InsertCommand = CommandInsert
                        Me.SqlDat.Update(InsertedRows) : CommandInsert.Parameters.Clear()
                    End With
                End If
                If UpdatedRows.Length > 0 Then
                    CommandUpdate = Me.SqlConn.CreateCommand()
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                             "UPDATE T_CLASS SET CLASS_NAME = @CLASS_NAME, INACTIVE = @INACTIVE, MODIFY_BY = @MODIFY_BY, MODIFY_DATE = @MODIFY_DATE WHERE CLASS_ID = @CLASS_ID ;"
                    With CommandUpdate
                        .CommandText = Query
                        .CommandType = CommandType.Text
                        .Transaction = Me.SqlTrans
                        .Parameters.Add("@CLASS_NAME", SqlDbType.VarChar, 100, "CLASS_NAME")
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100, "MODIFY_BY")
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime, 0, "MODIFY_DATE")
                        .Parameters.Add("@CLASS_ID", SqlDbType.VarChar, 10, "CLASS_ID")
                        .Parameters()("@CLASS_ID").SourceVersion = DataRowVersion.Original
                        Me.SqlDat.UpdateCommand = CommandUpdate
                        Me.SqlDat.Update(UpdatedRows)
                        CommandUpdate.Parameters.Clear()
                    End With
                End If
                If DeletedRows.Length > 0 Then
                    CommandDelete = Me.SqlConn.CreateCommand()
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "IF EXISTS(SELECT CLASS_ID FROM T_CLASS WHERE CLASS_ID = @CLASS_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " DELETE FROM T_CLASS WHERE CLASS_ID = @CLASS_ID;  " & vbCrLf & _
                            " END "
                    With CommandDelete
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Transaction = SqlTrans
                        .Parameters.Add("@CLASS_ID", SqlDbType.VarChar, 10, "CLASS_ID")
                        .Parameters()("@CLASS_ID").SourceVersion = DataRowVersion.Original
                        SqlDat.DeleteCommand = CommandDelete
                        SqlDat.Update(DeletedRows)
                        CommandDelete.Parameters.Clear()
                    End With
                End If
                Me.CommiteTransaction()
                If mustCloseConnection Then
                    CloseConnection()
                End If
            Catch ex As Exception
                Me.RollbackTransaction() : CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function getClass(ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT * FROM T_CLASS "
                If Not IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else : CreateCommandSql("sp_executesql", "")
                End If
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblClass As New DataTable("T_CLASS")
                setDataAdapter(Me.SqlCom).Fill(tblClass) : ClearCommandParameters()
                Return tblClass
            Catch ex As Exception
                CloseConnection()
                ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace

