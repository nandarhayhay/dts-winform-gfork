Imports System.Data
Imports System.Data.SqlClient
Namespace Brandpack
    Public Class BrandPack
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private m_DataSet As DataSet
        Private m_DataViewPrice As DataView
        Private m_DataViewBrandPack As DataView
        Private m_DataViewPack As DataView
        Private m_DataViewBrand As DataView
        Private clsPack As NufarmBussinesRules.Brandpack.Pack
        Private clsBrand As NufarmBussinesRules.Brandpack.Brand
        Private m_dtViewForDropDownPrice As DataView
        Private m_dtViewAllBrandPack As DataView
        'supaya bisa saving langsung function getdataviewbrand dan getdataviewpack mesti di panggil dulu sebelum 
        'function fetcdataset
        Private Query As String = ""
        Public Enum CategorySearch
            Brand
            Pack
        End Enum
        Public ReadOnly Property GetDataSet() As DataSet
            Get
                Return m_DataSet
            End Get
        End Property
        Public Function GetDataViewRowState(ByVal dtView As DataView, ByVal RowState As String) As DataView
            Select Case RowState
                Case "ModifiedAdded"
                    dtView.RowStateFilter = DataViewRowState.Added
                Case "ModifiedOriginal"
                    dtView.RowStateFilter = DataViewRowState.ModifiedOriginal
                Case "Deleted"
                    dtView.RowStateFilter = DataViewRowState.Deleted
                Case "Current"
                    dtView.RowStateFilter = DataViewRowState.CurrentRows
                Case "Unchaigned"
                    dtView.RowStateFilter = DataViewRowState.Unchanged
                Case "OriginalRows"
                    dtView.RowStateFilter = DataViewRowState.OriginalRows
            End Select
            Return dtView
        End Function
        Public Function CreateAllDataViewBrandPack() As DataView
            Try
                Me.CreateCommandSql("Sp_Select_AllBrandPack", "")
                Dim tbAlBrandPack As New DataTable("T_AllBrandPack")
                tbAlBrandPack.Clear()
                Me.FillDataTable(tbAlBrandPack)
                Me.m_dtViewAllBrandPack = tbAlBrandPack.DefaultView()
                Me.m_dtViewAllBrandPack.Sort = "BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_dtViewAllBrandPack
        End Function
        Public Function GetDataPack(ByVal SearchString As String) As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT PACK_ID,PACK_NAME FROM BRND_PACK WHERE PACK_NAME LIKE '%" & SearchString & "%';"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As DataTable
                If Not IsNothing(Me.m_DataViewPack) Then
                    dtTable = Me.m_DataViewPack.Table
                    dtTable.Clear()
                Else
                    dtTable = New DataTable("T_Pack") : dtTable.Clear()
                End If
                Me.FillDataTable(dtTable) : Me.m_DataViewPack = dtTable.DefaultView()
                Return Me.m_DataViewPack
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetDataBrand(ByVal SearchString As String) As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT BRAND_ID,BRAND_NAME FROM BRND_BRAND WHERE BRAND_NAME LIKE '%" & SearchString & "%';"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As DataTable
                If Not IsNothing(Me.m_DataViewBrand) Then
                    dtTable = Me.m_DataViewBrand.Table : dtTable.Clear()
                Else
                    dtTable = New DataTable("T_Brand")
                End If
                Me.FillDataTable(dtTable) : Me.ClearCommandParameters()
                Me.m_DataViewBrand = dtTable.DefaultView()
                Me.m_DataViewBrand.Sort = "BRAND_ID" : Return Me.m_DataViewBrand
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetDataBrandPackDropDown(ByVal SearchString As String, ByVal cat As CategorySearch) As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT BRANDPACK_ID,BRANDPACK_NAME FROM BRND_BRANDPACK WHERE "
                If cat = CategorySearch.Brand Then
                    Query &= vbCrLf & _
                    " BRAND_ID = ANY(SELECT BRAND_ID FROM BRND_BRAND WHERE BRAND_NAME LIKE '%" & SearchString & "%') "
                Else
                    Query &= " PACK_ID = ANY(SELECT PACK_ID FROM BRND_PACK WHERE PACK_NAME LIKE '%" & SearchString & "%') "
                End If
                Query &= " AND IsActive = 1;"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As DataTable
                If Not IsNothing(Me.m_dtViewForDropDownPrice) Then
                    dtTable = Me.m_dtViewForDropDownPrice.Table : dtTable.Clear()
                Else
                    dtTable = New DataTable("T_DropDownPrice") : dtTable.Clear()
                End If
                Me.FillDataTable(dtTable)
                Me.m_dtViewForDropDownPrice = dtTable.DefaultView()
                Return Me.m_dtViewForDropDownPrice
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub SaveData(ByVal ds As DataSet)
            Try
                Dim CommandInsert As New SqlCommand() : Dim CommandUpdate As New SqlCommand()
                Dim CommandDelete As New SqlCommand()
                CommandInsert = Me.SqlConn.CreateCommand()
                With CommandInsert
                    .CommandText = "SET NOCOUNT ON;" & vbCrLf & _
                                   "INSERT INTO BRND_BRANDPACK(BRANDPACK_ID,BRANDPACK_NAME,BRAND_ID,PACK_ID,UNIT,DEVIDED_QUANTITY,CREATE_DATE,CREATE_BY) " & vbCrLf & _
                                   "VALUES(@BRANDPACK_ID,@BRANDPACK_NAME,@BRAND_ID,@PACK_ID,@UNIT,@DEVIDED_QUANTITY,@CREATE_DATE,@CREATE_BY);"
                    .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                    .Parameters.Add("@BRANDPACK_NAME", SqlDbType.VarChar, 100, "BRANDPACK_NAME")
                    .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 10, "BRAND_ID")
                    .Parameters.Add("@PACK_ID", SqlDbType.VarChar, 10, "PACK_ID")
                    .Parameters.Add("@UNIT", SqlDbType.VarChar, 20, "UNIT")
                    .Parameters.Add("@DEVIDED_QUANTITY", SqlDbType.Decimal, 0, "DEVIDED_QUANTITY")
                    .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50, "CREATE_BY")
                    .Parameters.Add("@CREATE_DATE", SqlDbType.DateTime).SourceColumn = "CREATE_DATE"
                End With
                CommandUpdate = Me.SqlConn.CreateCommand()
                With CommandUpdate
                    .CommandText = "SET NOCOUNT ON;" & vbCrLf & _
                                   " UPDATE BRND_BRANDPACK SET BRANDPACK_ID = @BRANDPACK_ID,BRANDPACK_NAME = @BRANDPACK_NAME," & vbCrLf & _
                                   "BRAND_ID = @BRAND_ID,PACK_ID = @PACK_ID,IsActive = @IsActive,IsObsolete = @IsObsolete, UNIT = @UNIT,DEVIDED_QUANTITY = @DEVIDED_QUANTITY," & vbCrLf & _
                                   "MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE WHERE BRANDPACK_ID = @V_BRANDPACK_ID;"
                    .Parameters.Add("@V_BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                    .Parameters("@V_BRANDPACK_ID").SourceVersion = DataRowVersion.Original
                    .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                    .Parameters("@BRANDPACK_ID").SourceVersion = DataRowVersion.Current
                    .Parameters.Add("@BRANDPACK_NAME", SqlDbType.VarChar, 100, "BRANDPACK_NAME")
                    .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 10, "BRAND_ID")
                    .Parameters.Add("@PACK_ID", SqlDbType.VarChar, 10, "PACK_ID")
                    .Parameters.Add("@UNIT", SqlDbType.VarChar, 20, "UNIT")
                    .Parameters.Add("@DEVIDED_QUANTITY", SqlDbType.Decimal, 0, "DEVIDED_QUANTITY")
                    .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50, "MODIFY_BY")
                    .Parameters.Add("@MODIFY_DATE", SqlDbType.DateTime)
                    .Parameters("@MODIFY_DATE").SourceColumn = "MODIFY_DATE"
                    .Parameters.Add("@IsActive", SqlDbType.Bit).SourceColumn = "IsActive"
                    .Parameters.Add("@IsObsolete", SqlDbType.Bit).SourceColumn = "IsObsolete"
                End With
                CommandDelete = Me.SqlConn.CreateCommand()
                With CommandDelete
                    .CommandText = "SET NOCOUNT ON;" & vbCrLf & _
                                   "DECLARE @V_Retval INT;" & vbCrLf & _
                                   "EXEC @V_Retval = Usp_Check_Referenced_BRANDPACK_ID @BRANDPACK_ID = @V_BRANDPACK_ID " & vbCrLf & _
                                   "IF (@V_Retval > 0) " & vbCrLf & _
                                   " BEGIN " & vbCrLf & _
                                   " RAISERROR('Can not delete BrandPack because has child referenced-data',16,1);RETURN;" & vbCrLf & _
                                   " END " & vbCrLf & _
                                   " DELETE FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @V_BRANDPACK_ID"
                    .Parameters.Add("@V_BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                    .Parameters("@V_BRANDPACK_ID").SourceVersion = DataRowVersion.Original
                End With
                Me.SqlDat = New SqlDataAdapter()
                Me.SqlDat.InsertCommand = CommandInsert
                Me.SqlDat.UpdateCommand = CommandUpdate
                Me.SqlDat.DeleteCommand = CommandDelete

                Me.OpenConnection() : Me.BeginTransaction()
                CommandInsert.Transaction = Me.SqlTrans : CommandUpdate.Transaction = Me.SqlTrans
                CommandDelete.Transaction = Me.SqlTrans
                Dim InsertedRows() As DataRow = ds.Tables(0).Select("", "", DataViewRowState.Added)
                If InsertedRows.Length > 0 Then
                    SqlDat.Update(InsertedRows)
                End If
                CommandInsert.Parameters.Clear()
                Dim UpdatedRows() As DataRow = ds.Tables(0).Select("", "", DataViewRowState.ModifiedOriginal)
                If UpdatedRows.Length > 0 Then
                    SqlDat.Update(UpdatedRows)
                End If
                UpdatedRows = ds.Tables(0).Select("", "", DataViewRowState.ModifiedCurrent)
                If UpdatedRows.Length > 0 Then
                    SqlDat.Update(UpdatedRows)
                End If
                Dim DeletedRows() As DataRow = ds.Tables(0).Select("", "", DataViewRowState.Deleted)
                If DeletedRows.Length > 0 Then
                    SqlDat.Update(DeletedRows)
                End If
                CommandDelete.Parameters.Clear() : CommandUpdate.Parameters.Clear() : CommandInsert.Parameters.Clear()
                ''reset command text
                With CommandInsert
                    .CommandText = "SET NOCOUNT ON;INSERT INTO BRND_PRICE_HISTORY(PRICE_TAG,BRANDPACK_ID,PRICE,START_DATE,CREATE_DATE,CREATE_BY) " & vbCrLf & _
                                   "VALUES(@PRICE_TAG,@BRANDPACK_ID,@PRICE,@START_DATE,@CREATE_DATE,@CREATE_BY);"
                    .Parameters.Add("@PRICE_TAG", SqlDbType.VarChar, 100, "PRICE_TAG")
                    .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                    .Parameters.Add("@PRICE", SqlDbType.Decimal, 0, "PRICE")
                    .Parameters.Add("@START_DATE", SqlDbType.DateTime)
                    .Parameters("@START_DATE").SourceColumn = "START_DATE"
                    .Parameters.Add("@CREATE_DATE", SqlDbType.DateTime)
                    .Parameters("@CREATE_DATE").SourceColumn = "CREATE_DATE"
                    .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50, "CREATE_BY")
                End With
                InsertedRows = ds.Tables(1).Select("", "", DataViewRowState.Added)
                If (InsertedRows.Length > 0) Then
                    SqlDat.Update(InsertedRows)
                End If
                CommandInsert.Parameters.Clear()
                With CommandUpdate
                    .CommandText = "SET NOCOUNT ON;" & vbCrLf & _
                                   "DECLARE @V_Retval INT;" & vbCrLf & _
                                   "EXEC @V_Retval = Sp_Check_Referenced_BRANDPACK_ID_PRICE " & vbCrLf & _
                                   "@BRANDPACK_ID = @V_BRANDPACK_ID,@START_DATE = @V_START_DATE; " & vbCrLf & _
                                   "IF (@V_Retval > 0)" & vbCrLf & _
                                   " BEGIN " & vbCrLf & _
                                   " RAISERROR('Can not update Price because it may have been used',16,1);RETURN;" & vbCrLf & _
                                   " END " & vbCrLf & _
                                   "UPDATE BRND_PRICE_HISTORY SET PRICE_TAG = @PRICE_TAG,BRANDPACK_ID = @V_BRANDPACK_ID,PRICE = @PRICE,START_DATE = @V_START_DATE," & vbCrLf & _
                                   "MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE WHERE PRICE_TAG = @V_PRICE_TAG;"
                    .Parameters.Add("@PRICE_TAG", SqlDbType.VarChar, 100, "PRICE_TAG")
                    .Parameters("@PRICE_TAG").SourceVersion = DataRowVersion.Current
                    '.Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                    .Parameters.Add("@V_BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                    .Parameters.Add("@PRICE", SqlDbType.Decimal, 0, "PRICE")
                    '.Parameters.Add("@START_DATE", SqlDbType.DateTime)
                    '.Parameters("@START_DATE").SourceVersion = DataRowVersion.Current
                    '.Parameters("@START_DATE").SourceColumn = "START_DATE"

                    .Parameters.Add("@V_START_DATE", SqlDbType.DateTime)
                    .Parameters("@V_START_DATE").SourceColumn = "START_DATE"
                    '.Parameters("@V_START_DATE").SourceVersion = DataRowVersion.Original


                    .Parameters.Add("@MODIFY_DATE", SqlDbType.DateTime)
                    .Parameters("@MODIFY_DATE").SourceColumn = "MODIFY_DATE"
                    .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50, "MODIFY_BY")
                    .Parameters.Add("@V_PRICE_TAG", SqlDbType.VarChar, 100, "PRICE_TAG")
                    .Parameters("@V_PRICE_TAG").SourceVersion = DataRowVersion.Original
                End With
                UpdatedRows = ds.Tables(1).Select("", "", DataViewRowState.ModifiedCurrent)
                If UpdatedRows.Length > 0 Then
                    SqlDat.Update(UpdatedRows)
                End If
                UpdatedRows = ds.Tables(1).Select("", "", DataViewRowState.ModifiedOriginal)
                If UpdatedRows.Length > 0 Then
                    SqlDat.Update(UpdatedRows)
                End If
                CommandUpdate.Parameters.Clear()
                With CommandDelete
                    .CommandText = "SET NOCOUNT ON;" & vbCrLf & _
                                    "DECLARE @V_Retval INT;" & vbCrLf & _
                                   "EXEC @V_Retval = Sp_Check_Referenced_BRANDPACK_ID_PRICE " & vbCrLf & _
                                   "@BRANDPACK_ID = @V_BRANDPACK_ID,@START_DATE = @V_START_DATE " & vbCrLf & _
                                   "IF (@V_Retval > 0)" & vbCrLf & _
                                   " BEGIN " & vbCrLf & _
                                   " RAISERROR('Can not update Price because it may have been used',16,1);RETURN;" & vbCrLf & _
                                   " END " & vbCrLf & _
                                   "DELETE FROM BRND_PRICE_HISTORY WHERE PRICE_TAG = @PRICE_TAG;"
                    .Parameters.Add("@PRICE_TAG", SqlDbType.VarChar, 100, "PRICE_TAG")
                    .Parameters("@PRICE_TAG").SourceVersion = DataRowVersion.Original
                    .Parameters.Add("@V_BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                    .Parameters("@V_BRANDPACK_ID").SourceVersion = DataRowVersion.Original

                    .Parameters.Add("@V_START_DATE", SqlDbType.DateTime)
                    .Parameters("@V_START_DATE").SourceVersion = DataRowVersion.Original
                    .Parameters("@V_START_DATE").SourceColumn = "START_DATE"
                End With
                DeletedRows = ds.Tables(1).Select("", "", DataViewRowState.Deleted)
                If DeletedRows.Length > 0 Then
                    SqlDat.Update(DeletedRows)
                End If
                CommandDelete.Parameters.Clear()
                Me.CommiteTransaction() : Me.CloseConnection()
                CommandInsert.Dispose() : CommandUpdate.Dispose() : CommandDelete.Dispose()
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function FetchDataSet(ByVal MustReloadToAccPac As Boolean) As DataSet
            Try
                'get brandpack yang ada di accpack
                If MustReloadToAccPac Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                           " SELECT BR.BRAND_ID,P.PACK_ID,BR.BRAND_ID  + '' + P.PACK_ID AS BRANDPACK_ID," & vbCrLf & _
                           " BR.BRAND_NAME + ' @ ' + P.PACK_NAME AS BRANDPACK_NAME," & vbCrLf & _
                           " P.QUANTITY,P.DEVIDE_FACTOR,P.UNIT FROM Nufarm.dbo.BRND_BRAND BR,Nufarm.dbo.BRND_PACK P " & vbCrLf & _
                           " WHERE EXISTS(SELECT RIGHT(RTRIM(SEGMENT1),1) + '' + RTRIM(SEGMENT2) FROM NI87.dbo.ICITEM " & vbCrLf & _
                           "               WHERE RIGHT(RTRIM(SEGMENT1),1) + '' + RTRIM(SEGMENT2) = BR.BRAND_ID AND CATEGORY <> ANY(SELECT CATEGORY FROM NI87.dbo.ICCATG WHERE [DESC] NOT LIKE '%OTHERS%') " & vbCrLf & _
                           "               AND [DESC] NOT LIKE '%OTHER%' AND [DESC] NOT LIKE 'ROUNDUP%' AND [DESC] NOT LIKE '%BULK%' AND (RTRIM(ITEMBRKID) = 'FG' OR RTRIM(ITEMBRKID) = 'FGST') AND INACTIVE = 0 " & vbCrLf & _
                           "              ) " & vbCrLf & _
                           "   AND EXISTS(SELECT RTRIM(SEGMENT4) + '' + RTRIM(SEGMENT3) FROM NI87.dbo.ICITEM" & vbCrLf & _
                           "               WHERE RTRIM(SEGMENT4) + '' + RTRIM(SEGMENT3)  = P.PACK_ID " & vbCrLf & _
                           "               AND [DESC] NOT LIKE 'OTHER%' " & vbCrLf & _
                           "               AND [DESC] NOT LIKE 'ROUNDUP%' AND [DESC] NOT LIKE '%BULK%' AND (RTRIM(ITEMBRKID) = 'FG' OR RTRIM(ITEMBRKID) = 'FGST') AND INACTIVE = 0 " & vbCrLf & _
                           "              ) " & vbCrLf & _
                           "   AND EXISTS(SELECT ITEMNO FROM NI87.dbo.ICITEM WHERE SUBSTRING(ITEMNO,2,10) = BR.BRAND_ID  + '' + P.PACK_ID " & vbCrLf & _
                           "               AND [DESC] NOT LIKE 'OTHER%' " & vbCrLf & _
                           "               AND [DESC] NOT LIKE 'ROUNDUP%' AND [DESC] NOT LIKE '%BULK%' AND (RTRIM(ITEMBRKID) = 'FG' OR RTRIM(ITEMBRKID) = 'FGST') AND INACTIVE = 0 " & vbCrLf & _
                           "              ) " & vbCrLf & _
                           " AND NOT EXISTS(SELECT BRANDPACK_ID FROM Nufarm.dbo.BRND_BRANDPACK WHERE BRANDPACK_ID =  BR.BRAND_ID  + '' + P.PACK_ID); "
                    Me.CreateCommandSql("sp_executesql", "")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Dim dtTable As New DataTable("T_BrandPack") : dtTable.Clear()
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                    Me.OpenConnection() : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                    If dtTable.Rows.Count > 0 Then

                        '"DECLARE @V_BRAND_NAME VARCHAR(50),@V_BRANDPACK_NAME VARCHAR(100),@V_PACK_NAME VARCHAR(50); " & vbCrLf & _
                        '" IF EXISTS(SELECT SUBSTRING(ITEMNO,2,10) FROM NI87.dbo.ICITEM WHERE SUBSTRING(ITEMNO,2,10) = @BRANDPACK_ID) " & vbCrLf & _
                        '" BEGIN " & vbCrLf & _
                        '"  SET @V_BRAND_NAME = (SELECT TOP 1 RTRIM(SUBSTRING([DESC],1,CHARINDEX('@',[DESC],0)- 1)) " & vbCrLf & _
                        '"  FROM NI87.dbo.ICITEM WHERE SUBSTRING(ITEMNO,2,10) = @BRANDPACK_ID); " & vbCrLf & _
                        '"  SET @V_PACK_NAME = (SELECT TOP 1 PACK_NAME FROM Nufarm.dbo.BRND_PACK WHERE PACK_ID = @PACK_ID); " & vbCrLf & _
                        '"  SET @V_BRANDPACK_NAME = @V_BRAND_NAME + ' @ ' + @V_PACK_NAME; " & vbCrLf & _
                        '"  UPDATE Nufarm.dbo.BRND_BRAND SET BRAND_NAME = @V_BRAND_NAME WHERE BRAND_ID = @BRAND_ID; " & vbCrLf & _
                        ' " INSERT INTO Nufarm.dbo.BRND_BRANDPACK(BRANDPACK_ID,BRANDPACK_NAME,BRAND_ID,PACK_ID,DEVIDED_QUANTITY,UNIT,IsActive,IsObsolete,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                        '"  VALUES(@BRANDPACK_ID,@V_BRANDPACK_NAME,@BRAND_ID,@PACK_ID,@DEVIDED_QUANTITY,@UNIT,1,0,@CREATE_BY,@CREATE_DATE); " & vbCrLf & _
                        '" END " & vbCrLf & _
                        ' "ELSE " & vbCrLf & _
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "IF NOT EXISTS(SELECT BRANDPACK_ID FROM Nufarm.dbo.BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID) " & vbCrLf & _
                               " BEGIN " & vbCrLf & _
                               "  INSERT INTO Nufarm.dbo.BRND_BRANDPACK(BRANDPACK_ID,BRANDPACK_NAME,BRAND_ID,PACK_ID,DEVIDED_QUANTITY,UNIT,IsActive,IsObsolete,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                               "  VALUES(@BRANDPACK_ID,@BRANDPACK_NAME,@BRAND_ID,@PACK_ID,@DEVIDED_QUANTITY,@UNIT,1,0,@CREATE_BY,@CREATE_DATE); " & vbCrLf & _
                               " END "

                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                        For i As Integer = 0 To dtTable.Rows.Count - 1
                            If dtTable.Rows(i)("BRANDPACK_NAME").ToString().Contains("BULK") Then
                            Else
                                Dim DevidedQty As Object = DBNull.Value, DevideFactor As Object = DBNull.Value, Quantity As Object = DBNull.Value
                                Dim CreateBy As String = ""
                                If Not dtTable.Rows(i)("BRANDPACK_NAME").ToString().Contains("OTHER") Then
                                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, dtTable.Rows(i)("BRANDPACK_ID"), 14)
                                    Me.AddParameter("@BRANDPACK_NAME", SqlDbType.VarChar, dtTable.Rows(i)("BRANDPACK_NAME"), 100)
                                    Me.AddParameter("@BRAND_ID", SqlDbType.VarChar, dtTable.Rows(i)("BRAND_ID"), 7)
                                    Me.AddParameter("@PACK_ID", SqlDbType.VarChar, dtTable.Rows(i)("PACK_ID"), 7)
                                    If Not IsDBNull(dtTable.Rows(i)("QUANTITY")) And Not IsNothing(dtTable.Rows(i)("QUANTITY")) Then
                                        Quantity = Convert.ToInt32(dtTable.Rows(i)("QUANTITY"))
                                    End If
                                    If Not IsDBNull(dtTable.Rows(i)("DEVIDE_FACTOR")) And Not IsNothing(dtTable.Rows(i)("DEVIDE_FACTOR")) Then
                                        DevideFactor = Convert.ToInt32(dtTable.Rows(i)("DEVIDE_FACTOR"))
                                    End If
                                    If Quantity > 0 And DevideFactor > 0 Then
                                        DevidedQty = Convert.ToDecimal(Quantity / DevideFactor)
                                    End If
                                    'CreateBy = dtTable.Rows(i)("CREATE_BY").ToString()
                                    Me.AddParameter("@DEVIDED_QUANTITY", SqlDbType.Decimal, DevidedQty)
                                    Me.AddParameter("@UNIT", SqlDbType.VarChar, dtTable.Rows(i)("UNIT"), 20)
                                    'If CreateBy.ToUpper() = "ADMIN" Or CreateBy = "" Then
                                    '    CreateBy = "SYSTEM"
                                    'End If
                                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, "SYSTEM")
                                    Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
                                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                                End If
                            End If
                        Next
                    End If
                    ''ambil brand nya dulu
                    'UPDATE BRANDPACK YANG DI ACCPACK NGGAK ADA
                    'bikin table temporary supaya gak berat
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "IF OBJECT_ID('tempdb..##T_P_BrandPack') IS NULL " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " SELECT * INTO ##T_P_BrandPack " & vbCrLf & _
                            " FROM ( SELECT '02-2003022K-D' AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2006022K-D' AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2010022K-D' AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2011022K-D' AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2012022K-D' AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2014022K-D' AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2034022K-D' AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '02-2039022K-D' AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '00601001LD'    AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '006020020LD'   AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '00604040LD'    AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '0060200200MD'  AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '00055005L-D'   AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '00681001LD'    AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '006820020LD'   AS BRANDPACK_ID UNION ALL " & vbCrLf & _
                            "        SELECT '00684004LD'    AS BRANDPACK_ID " & vbCrLf & _
                            "        )T; " & vbCrLf & _
                            " CREATE CLUSTERED INDEX IDX_T_P_B_1 ON tempdb..##T_P_BRANDPACK(BRANDPACK_ID); " & vbCrLf & _
                           " END "
                    Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()
                    'INSERT BRANDPACK GIBGRO SP
               
                    Query = "SET NOCOUNT ON;SELECT BRANDPACK_ID FROM BRND_BRANDPACK WHERE BRANDPACK_NAME LIKE 'GIBGRO 10 SP%' AND isActive = 1;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql") : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Dim tblbrandPack As New DataTable("T_P_BrandPack") : tblbrandPack.Clear() : Me.SqlDat.Fill(tblbrandPack)
                    Me.ClearCommandParameters()

                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "IF NOT EXISTS(SELECT BRANDPACK_ID FROM tempdb..##T_P_BrandPack WHERE BRANDPACK_ID = @BRANDPACK_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "   INSERT INTO tempdb..##T_P_BrandPack(BRANDPACK_ID) " & vbCrLf & _
                            "   VALUES(@BRANDPACK_ID); " & vbCrLf & _
                            " END "
                    Me.ResetCommandText(CommandType.Text, Query)
                    If IsNothing(Me.SqlTrans) Then
                        Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                    End If

                    For i As Integer = 0 To tblbrandPack.Rows.Count - 1
                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, tblbrandPack.Rows(i)("BRANDPACK_ID"), 14)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next

                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " UPDATE Nufarm.dbo.BRND_BRANDPACK SET IsActive = 0 WHERE BRANDPACK_ID = " & vbCrLf & _
                            " ANY(SELECT BRANDPACK_ID FROM Nufarm.dbo.BRND_BRANDPACK BB " & vbCrLf & _
                            "      WHERE EXISTS(SELECT SUBSTRING(ITEMNO,2,10) FROM NI87.dbo.ICITEM WHERE SUBSTRING(ITEMNO,2,10) = BB.BRANDPACK_ID " & vbCrLf & _
                            "                   AND (RTRIM(ITEMBRKID) = 'FG' OR RTRIM(ITEMBRKID) = 'FGST') AND INACTIVE = 1) " & vbCrLf & _
                            "      AND NOT EXISTS(SELECT BRANDPACK_ID FROM tempdb..##T_P_BrandPack WHERE BRANDPACK_ID = BB.BRANDPACK_ID) " & vbCrLf & _
                            "     );" ''& vbCrLf & _
                    '"UPDATE Nufarm.dbo.BRND_BRANDPACK SET IsActive = 1,IsObsolete = 1 WHERE BRANDPACK_ID = " & vbCrLf & _
                    '" ANY(SELECT BRANDPACK_ID FROM Nufarm.dbo.BRND_BRANDPACK BB " & vbCrLf & _
                    '"     WHERE EXISTS(SELECT BRANDPACK_ID FROM " & vbCrLf & _
                    '"                  Nufarm.dbo.AGREE_BRANDPACK_INCLUDE WHERE BRANDPACK_ID = BB.BRANDPACK_ID) " & vbCrLf & _
                    '"     AND NOT EXISTS(SELECT SUBSTRING(ITEMNO,2,10) FROM NI87.dbo.ICITEM WHERE INACTIVE = 0 " & vbCrLf & _
                    '"                AND RTRIM(ITEMBRKID) = 'FG' AND SUBSTRING(ITEMNO,2,10) = BB.BRANDPACK_ID) " & vbCrLf & _
                    '"     AND NOT EXISTS(SELECT BRANDPACK_ID FROM tempdb..##T_P_BrandPack WHERE BRANDPACK_ID = BB.BRANDPACK_ID) " & vbCrLf & _
                    '"     );"

                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Me.CommiteTransaction()
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql") : End If
                Me.OpenConnection()
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT BRAND_ID,BRAND_NAME FROM BRND_BRAND WHERE BRAND_NAME NOT LIKE 'OTHER%';"
                'Me.CreateCommandSql("sp_executesql", "")

                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtBrand As New DataTable("T_Brand") : dtBrand.Clear()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(dtBrand) : Me.ClearCommandParameters()
                ''reset command text
                'AMBIL DATA PACK
                Query = "SET NOCOUNT ON;SELECT PACK_ID,PACK_NAME,QUANTITY,UNIT,DEVIDE_FACTOR FROM BRND_PACK;"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtPack As New DataTable("T_Pack") : dtPack.Clear()
                Me.SqlDat.Fill(dtPack) : Me.ClearCommandParameters()
                'reset command text 
                'ambil data price for drop down
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                         "SELECT BRANDPACK_ID,BRANDPACK_NAME FROM BRND_BRANDPACK WHERE IsActive = 1;"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtDropDownPrice As New DataTable("T_DropDownPrice")
                dtDropDownPrice.Clear() : Me.SqlDat.Fill(dtDropDownPrice)
                Me.ClearCommandParameters()
                'ambil data price sekarang
                'Dim dateNow As DateTime = Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString())
                'Dim strDate As String = dateNow.Month.ToString() & "/" & dateNow.AddDays(-3).Day.ToString() & "/" & dateNow.Year.ToString()
                'Me.SqlCom.CommandText = "sp_executesql"
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                         "SELECT * FROM BRND_BRANDPACK WHERE IsActive = 1 OR IsObsolete = 0; " & vbCrLf & _
                        "SELECT * FROM BRND_PRICE_HISTORY WHERE CREATE_DATE >= @CreatedDate;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate.AddDays(-3))
                Me.m_DataSet = New DataSet() : Me.m_DataSet.Clear()
                Me.SqlDat.Fill(Me.m_DataSet) : Me.CloseConnection()
                Me.ClearCommandParameters() : Me.m_DataViewBrand = dtBrand.DefaultView()
                Me.m_DataViewBrand.Sort = "BRAND_ID"
                Me.m_DataViewPack = dtPack.DefaultView() : Me.m_DataViewPack.Sort = "PACK_ID"
                Me.m_DataViewBrandPack = Me.m_DataSet.Tables(0).DefaultView()
                Me.m_DataViewBrandPack.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_DataViewBrandPack.Sort = "BRANDPACK_ID"

                Me.m_DataViewPrice = Me.m_DataSet.Tables(1).DefaultView()
                Me.m_DataViewPrice.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_DataViewPrice.Sort = "PRICE_TAG"
                Me.m_dtViewForDropDownPrice = dtDropDownPrice.DefaultView()
                'Me.CreateCommandSql("Sp_Select_PriceHistory", "")
                'Dim tblPriceHistory As New DataTable("T_Price") : tblPriceHistory.Clear()
                'Me.FillDataTable(tblPriceHistory)
                'Me.CreateCommandSql("Sp_Select_BrandPack", "")
                'Dim tblBrandPack As New DataTable("T_Brandpack")
                'tblBrandPack.Clear()
                'Me.FillDataTable(tblBrandPack)
                'Me.m_DataSet = New DataSet()
                'Me.m_DataSet.Clear()
                'Me.m_DataSet.Tables.Add(tblPriceHistory)
                'Me.m_DataSet.Tables.Add(tblBrandPack)
                'MyBase.GetdatasetRelation("StoredProcedure", "Sp_Select_BrandPack", "Sp_Select_PriceHistory", "T_BrandPack", "T_PriceHistory", "BRANDPACK_ID", "BRANDPACK_ID", "BRANDPACK_HISTORY", "Update")
                'Me.m_dtViewAllBrandPack = New DataView(MyBase.baseDataSetRelation.Tables("T_BrandPack"), "", "BRANDPACK_ID", DataViewRowState.OriginalRows)
                'Me.m_dtViewAllBrandPack.Sort = "BRANDPACK_ID"
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
            Return m_DataSet
        End Function
        Public ReadOnly Property getAllDataViewBrandPack() As DataView
            Get
                Return Me.m_dtViewAllBrandPack
            End Get
        End Property
        Public Property GetDataViewBrandPack() As DataView
            Get
                Return Me.m_DataViewBrandPack
            End Get
            Set(ByVal value As DataView)
                Me.m_DataViewBrandPack = value
            End Set
        End Property
        Public Property GetDataViewPrice() As DataView
            Get
                Return Me.m_DataViewPrice
            End Get
            Set(ByVal value As DataView)
                Me.m_DataViewPrice = value
            End Set
        End Property
        Public ReadOnly Property GetDataViewBrand() As DataView
            Get
                Return Me.m_DataViewBrand
            End Get
        End Property
        Public ReadOnly Property GetDataViewPack() As DataView
            Get
                Return Me.m_DataViewPack
            End Get
        End Property
        Public ReadOnly Property ViewDropDownPrice() As DataView
            Get
                Return Me.m_dtViewForDropDownPrice
            End Get
        End Property
        'Public Function CreateDataViewBrand() As DataView
        '    Try
        '        clsBrand = New NufarmBussinesRules.Brandpack.Brand
        '        Me.m_DataViewBrand = clsBrand.CreateAllDataViewBrand()
        '        Me.m_DataViewBrand.Sort = "BRAND_ID"
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        '    Return Me.m_DataViewBrand
        'End Function
        Public Function CreateDataViewPack() As DataView
            Try
                clsPack = New NufarmBussinesRules.Brandpack.Pack
                Me.m_DataViewPack = clsPack.CreateDataViewAllPack()
                Me.m_DataViewPack.Sort = "PACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_DataViewPack
        End Function
        Public Function FilterRowPriceHistrory(ByVal filterRow As String) As DataView
            Me.m_DataViewPrice.RowFilter = filterRow
            Return Me.m_DataViewPrice
        End Function
        'Public Function CreateDataViewDropDownPrice() As DataView
        '    Try
        '        Me.CreateCommandSql("Sp_Select_BRAND_BRANDPACK_FOR_DROPDOWN", "")
        '        Dim T_DR As New DataTable("T_DR")
        '        T_DR.Clear()
        '        Me.FillDataTable(T_DR)
        '        Me.m_dtViewForDropDownPrice = T_DR.DefaultView()
        '        Me.m_dtViewForDropDownPrice.Sort = "BRANDPACK_ID"
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        '    Return Me.m_dtViewForDropDownPrice
        'End Function
        Public Function HasReferenceData(ByVal BRANDPACK_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Usp_Check_Referenced_BRANDPACK_ID", "")
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If Not IsNothing(Me.ExecuteScalar()) Then
                    Return True
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function
        Public Function HasReferenceDataPriceHistory(ByVal BRANDPACK_ID As String, ByVal START_DATE As DateTime) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_Referenced_BRANDPACK_ID_PRICE", "")
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, START_DATE)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_DataSet) Then
                Me.m_DataSet.Dispose()
                Me.m_DataSet = Nothing
            End If
            If Not IsNothing(Me.m_DataViewBrand) Then
                Me.m_DataViewBrand.Dispose()
                Me.m_DataViewBrand = Nothing
            End If
            If Not IsNothing(Me.m_DataViewBrandPack) Then
                Me.m_DataViewBrandPack.Dispose()
                Me.m_DataViewBrandPack = Nothing
            End If
            If Not IsNothing(Me.m_DataViewPack) Then
                Me.m_DataViewPack.Dispose()
                Me.m_DataViewPack = Nothing
            End If
            If Not IsNothing(Me.m_DataViewPrice) Then
                Me.m_DataViewPrice.Dispose()
                Me.m_DataViewPrice = Nothing
            End If
            If Not IsNothing(Me.m_dtViewForDropDownPrice) Then
                Me.m_dtViewForDropDownPrice.Dispose()
                Me.m_dtViewForDropDownPrice = Nothing
            End If
            If Not IsNothing(Me.m_dtViewAllBrandPack) Then
                Me.m_dtViewAllBrandPack.Dispose()
                Me.m_dtViewAllBrandPack = Nothing
            End If
            If Not IsNothing(Me.clsPack) Then
                clsPack.Dispose(False)
            End If
            If Not IsNothing(Me.clsBrand) Then
                Me.clsBrand.dispose(False)
            End If

        End Sub
        Public Overloads Sub SaveChanges(ByVal ds As DataSet)
            Try
                MyBase.ParentCommandtext = "SELECT * FROM BRND_BRANDPACK"
                MyBase.SqlCom.CommandType = CommandType.Text
                MyBase.ChildCommandText = "SELECT * FROM BRND_PRICE_HISTORY"
                MyBase.SaveChangesDSRelation(ds)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
    End Class

End Namespace
