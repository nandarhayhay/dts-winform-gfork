Imports System.Data.SqlClient
Imports System.Data
Namespace DistributorRegistering
    Public Class RegionalTerritory
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private m_ViewRegional As DataView
        Private m_ViewTerritory As DataView
        Private m_ViewTerritoryManager As DataView
        Private m_viewFS As DataView
        Private m_dataSet As DataSet
        Private Query As String = ""
        'Public Function ViewTerritory() As DataView
        '    Return Me.m_ViewTerritory
        'End Function
        'Public Function ViewRegional() As DataView

        '    Return Me.m_ViewRegional
        'End Function
        'Public ReadOnly Property GetDataViewRegional() As DataView
        '    Get
        '        Return Me.m_ViewReg
        '    End Get
        'End Property
        Public ReadOnly Property GetDataSet() As DataSet
            Get
                Return Me.m_dataSet
            End Get
        End Property
        Public Function GetDataViewRowState(ByVal DV As DataView, ByVal RowState As String) As DataView
            Select Case RowState
                Case "ModifiedAdded"
                    DV.RowStateFilter = DataViewRowState.Added Or DataViewRowState.ModifiedCurrent
                Case "ModifiedOriginal"
                    DV.RowStateFilter = DataViewRowState.ModifiedOriginal
                Case "Deleted"
                    DV.RowStateFilter = DataViewRowState.Deleted
                Case "Current"
                    DV.RowStateFilter = DataViewRowState.CurrentRows
                Case "Unchaigned"
                    DV.RowStateFilter = DataViewRowState.Unchanged
                Case "OriginalRows"
                    DV.RowStateFilter = DataViewRowState.OriginalRows
            End Select
            Return DV
        End Function
        Public ReadOnly Property ViewRegional() As DataView
            Get
                Return Me.m_ViewRegional
            End Get
        End Property
        Public ReadOnly Property ViewTerritory() As DataView
            Get
                Return Me.m_ViewTerritory
            End Get
        End Property
        Public ReadOnly Property ViewTerritoryManager() As DataView
            Get
                Return Me.m_ViewTerritoryManager
            End Get
        End Property
        Public ReadOnly Property ViewFS() As DataView
            Get
                Return Me.m_viewFS
            End Get
        End Property
        Public Sub GetDataView()
            Try
                'MyBase.GetdatasetRelation("StoredProcedure", "Sp_Select_Regional", "Sp_Select_Territory", "T_Regional", "T_Territory", "REGIONAL_ID", "REGIONAL_ID", "Regional_Territory", "Update")
                Me.m_dataSet = New DataSet()
                Me.m_dataSet.Clear()
                Dim tblRegional As New DataTable("Regional")
                Dim tblTerritoryManager As New DataTable("Territory_Manager")
                tblTerritoryManager.Clear()
                Dim tblTerritory As New DataTable("Territory")
                tblTerritory.Clear()

                'select regional
                Query = "SET  NOCOUNT ON ;" & vbCrLf & _
                        "SELECT * FROM DIST_REGIONAL ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(tblRegional) : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT * FROM TERRITORY"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(tblTerritory)

                'Me.SqlCom.CommandText = "Sp_Select_Territory"
                'Me.SqlCom.CommandType = CommandType.StoredProcedure
                'Me.SqlCom.CommandText = "Usp_Select_Territory_Manager"

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT * FROM TERRITORY_MANAGER ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlDat.Fill(tblTerritoryManager)

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT * FROM FS "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblFs As New DataTable("FIELD_SUPERVISOR")
                tblFs.Clear() : Me.SqlDat.Fill(tblFs) : Me.ClearCommandParameters()
                Me.CloseConnection()

                tblFs.Columns("CUR_TERRITORY_ID").SetOrdinal(0)
                Me.m_dataSet.Tables.Add(tblTerritoryManager)
                Me.m_dataSet.Tables.Add(tblRegional)
                Me.m_dataSet.Tables.Add(tblTerritory)
                Me.m_dataSet.Tables.Add(tblFs)

                Me.m_ViewRegional = Me.m_dataSet.Tables("Regional").DefaultView()
                Me.m_ViewRegional.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_ViewRegional.Sort = "REGIONAL_ID"
                Me.m_ViewTerritory = Me.m_dataSet.Tables("Territory").DefaultView()
                Me.m_ViewTerritory.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_ViewTerritory.Sort = "TERRITORY_ID"
                Me.m_ViewTerritoryManager = Me.m_dataSet.Tables("Territory_Manager").DefaultView()
                Me.m_ViewTerritoryManager.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_ViewTerritoryManager.Sort = "TM_ID"

                Me.m_viewFS = Me.m_dataSet.Tables("FIELD_SUPERVISOR").DefaultView()
                Me.m_viewFS.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_viewFS.Sort = "FS_ID"
                Me.m_dataSet.AcceptChanges()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Sub New()

        End Sub
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewTerritory) Then
                Me.m_ViewTerritory.Dispose()
                Me.m_ViewTerritory = Nothing
            End If
            If Not IsNothing(Me.m_ViewRegional) Then
                Me.m_ViewRegional.Dispose()
                Me.m_ViewRegional = Nothing
            End If
            If Not IsNothing(Me.m_ViewTerritoryManager) Then
                Me.m_ViewTerritoryManager.Dispose()
                Me.m_ViewTerritoryManager = Nothing
            End If
            If Not IsNothing(Me.m_viewFS) Then
                Me.m_viewFS.Dispose() : Me.m_viewFS = Nothing
            End If
            If Not IsNothing(Me.m_dataSet) Then
                Me.m_dataSet.Dispose()
                Me.m_dataSet = Nothing
            End If
        End Sub
        Public Function HasReferencedTerritory(ByVal TERRITORY_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Select_REFERENCED_TERRITORY", "")
                Me.AddParameter("@TERRITORY_ID", SqlDbType.VarChar, TERRITORY_ID, 10)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.VarChar, ParameterDirection.ReturnValue)
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
        Public Function HasReferencedTerritoryManager(ByVal TM_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Usp_Select_Referenced_Territory_Manager", "")
                Me.AddParameter("@TM_ID", SqlDbType.VarChar, TM_ID, 10)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function HasRererencedRegional(ByVal REGIONAL_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_REFERENCED_REGIONAL", "")
                Me.AddParameter("@REGIONAL_ID", SqlDbType.VarChar, REGIONAL_ID, 7)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.VarChar, ParameterDirection.ReturnValue)
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
        Public Overloads Sub SaveChanges(ByVal ds As DataSet)
            Try
                'MyBase.ParentCommandtext = "SELECT * FROM DIST_REGIONAL"
                'MyBase.ChildCommandText = "SELECT * FROM DIST_TERRITORY"
                'MyBase.SqlCom.CommandType = CommandType.Text
                'MyBase.SaveChangesDSRelation(ds)
                Me.OpenConnection()
                'Me.BeginTransaction()
                'Dim commandInsert As SqlCommand = Me.SqlConn.CreateCommand()
                'Dim commandUpdate As SqlCommand = Me.SqlConn.CreateCommand()
                'Dim commandDelete As SqlCommand = Me.SqlConn.CreateCommand()
                'If IsNothing(Me.SqlDat) Then Me.SqlDat = New SqlDataAdapter()
                'Dim insertedRows() As DataRow = ds.Tables("Territory").Select("", "", DataViewRowState.Added)
                'If insertedRows.Length > 0 Then
                '    With commandInsert
                '        .Transaction = Me.SqlTrans
                '        .CommandText = "SET NOCOUNT ON ;" & vbCrLf & _
                '                        "INSERT INTO TERRITORY(TERRITORY_ID,TERRITORY_AREA,TERRITORY_DESCRIPTION,REGIONAL_ID,CREATE_BY,CREATE_DATE,INACTIVE) " & vbCrLf & _
                '                        "VALUES(@TERRITORY_ID,@TERRITORY_AREA,@TERRITORY_DESCRIPTION,@REGIONAL_ID,@CREATE_BY,@CREATE_DATE,0) ;"
                '        .Parameters.Add("@TERRITORY_ID", SqlDbType.VarChar, 10, "TERRITORY_ID")
                '        .Parameters.Add("@TERRITORY_AREA", SqlDbType.VarChar, 30, "TERRITORY_AREA")
                '        .Parameters.Add("@TERRITORY_DESCRIPTION", SqlDbType.VarChar, 150, "TERRITORY_DESCRIPTION")
                '        .Parameters.Add("@REGIONAL_ID", SqlDbType.VarChar, 5, "REGIONAL_ID")
                '        .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_DATE")
                '        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 100, "CREATE_BY")
                '    End With
                '    SqlDat.InsertCommand = commandInsert
                '    SqlDat.Update(insertedRows)
                'End If
                'Dim UpdatedRows() As DataRow = ds.Tables("Territory").Select("", "", DataViewRowState.ModifiedOriginal)
                'If UpdatedRows.Length > 0 Then
                '    With commandUpdate
                '        .Transaction = Me.SqlTrans
                '        .CommandText = "IF EXISTS(SELECT TERRITORY_ID FROM TERRITORY WHERE TERRITORY_ID = @TERRITORY_ID ) " & vbCrLf & _
                '        " BEGIN " & vbCrLf & _
                '        " UPDATE TERRITORY SET TERRITORY_AREA = @TERRITORY_AREA,TERRITORY_DESCRIPTION = @TERRITORY_DESCRIPTION,INACTIVE = @INACTIVE,REGIONAL_ID = @REGIONAL_ID, " & vbCrLf & _
                '        " MODIFY_BY = @MODIFY_BY ,MODIFY_DATE = @MODIFY_DATE WHERE TERRITORY_ID = @TERRITORY_ID ;" & vbCrLf & _
                '        " END "
                '        .Parameters.Add("@TERRITORY_ID", SqlDbType.VarChar, 10, "TERRITORY_ID")
                '        .Parameters("@TERRITORY_ID").SourceVersion = DataRowVersion.Original
                '        .Parameters.Add("@TERRITORY_AREA", SqlDbType.VarChar, 30, "TERRITORY_AREA")
                '        .Parameters.Add("@TERRITORY_DESCRIPTION", SqlDbType.VarChar, 150, "TERRITORY_DESCRIPTION")
                '        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                '        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime, 0, "MODIFY_DATE")
                '        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100, "MODIFY_BY")
                '    End With
                '    SqlDat.UpdateCommand = commandUpdate
                '    SqlDat.Update(UpdatedRows)
                'End If
                'Dim deletedRows() As DataRow = ds.Tables("Territory").Select("", "", DataViewRowState.Deleted)
                'If deletedRows.Length > 0 Then

                'End If
                Me.BeginTransaction()
                Me.SqlCom.Transaction = Me.SqlTrans
                Dim commandInsert As SqlCommand = Me.SqlConn.CreateCommand()
                Dim commandUpdate As SqlCommand = Me.SqlConn.CreateCommand()
                Dim commandDelete As SqlCommand = Me.SqlConn.CreateCommand()
                If IsNothing(Me.SqlDat) Then
                    Me.SqlDat = New SqlDataAdapter()
                End If
                Me.SqlDat.UpdateCommand = commandUpdate
                Me.SqlDat.InsertCommand = commandInsert
                Me.SqlDat.DeleteCommand = commandDelete

                'Me.CreateCommandSql("", "SELECT * FROM TERRITORY")
                'Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                'Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                ''territory dulu

                Dim InsertedRows() As DataRow = ds.Tables("Territory").Select("", "", DataViewRowState.Added)
                Dim UpdatedRows() As DataRow = ds.Tables("Territory").Select("", "", DataViewRowState.ModifiedCurrent Or DataViewRowState.ModifiedOriginal)
                Dim DeletedRows() As DataRow = ds.Tables("Territory").Select("", "", DataViewRowState.Deleted)
                If InsertedRows.Length > 0 Then
                    Query = " SET NOCOUNT ON ;" & vbCrLf & _
                            " IF NOT EXISTS(SELECT TERRITORY_ID FROM TERRITORY WHERE TERRITORY_ID = @TERRITORY_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " INSERT INTO TERRITORY(TERRITORY_ID,TERRITORY_AREA,PARENT_TERRITORY,REGIONAL_ID,TERRITORY_DESCRIPTION,TERRITORY_FOR,CREATE_BY,CREATE_DATE,INACTIVE) " & vbCrLf & _
                            " VALUES(@TERRITORY_ID,@TERRITORY_AREA,@PARENT_TERRITORY,@REGIONAL_ID,@TERRITORY_DESCRIPTION,@TERRITORY_FOR,@CREATE_BY,@CREATE_DATE,0) " & vbCrLf & _
                            " END " & vbCrLf & _
                            " ELSE " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " UPDATE TERRITORY SET TERRITORY_AREA = @TERRITORY_AREA,REGIONAL_ID = @REGIONAL_ID,TERRITORY_DESCRIPTION = @TERRITORY_DESCRIPTION,PARENT_TERRITORY = @PARENT_TERRITORY,TERRITORY_FOR = @TERRITORY_FOR,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE,INACTIVE = @INACTIVE " & vbCrLf & _
                            " WHERE TERRITORY_ID = @TERRITORY_ID " & vbCrLf & _
                            " END "
                    With commandInsert
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@TERRITORY_ID", SqlDbType.VarChar, 14, "TERRITORY_ID")
                        .Parameters.Add("@PARENT_TERRITORY", SqlDbType.VarChar, 16, "PARENT_TERRITORY")
                        .Parameters.Add("@REGIONAL_ID", SqlDbType.VarChar, 16, "REGIONAL_ID")
                        .Parameters.Add("@TERRITORY_AREA", SqlDbType.VarChar, 100, "TERRITORY_AREA")
                        .Parameters.Add("@TERRITORY_DESCRIPTION", SqlDbType.VarChar, 150, "TERRITORY_DESCRIPTION")
                        .Parameters.Add("@TERRITORY_FOR", SqlDbType.VarChar, 5, "TERRITORY_FOR")
                        .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_DATE")
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 100, "CREATE_BY")
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100, "MODIFY_BY")
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime, 0, "MODIFY_DATE")
                    End With
                    commandInsert.Transaction = Me.SqlTrans
                    Me.SqlDat.Update(InsertedRows) : commandInsert.Parameters.Clear()
                End If
                If UpdatedRows.Length > 0 Then
                    Query = " SET NOCOUNT ON ;" & vbCrLf & _
                            " IF EXISTS(SELECT TERRITORY_ID FROM TERRITORY WHERE TERRITORY_ID = @O_TERRITORY_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " UPDATE TERRITORY SET TERRITORY_AREA = @TERRITORY_AREA,REGIONAL_ID = @REGIONAL_ID,TERRITORY_DESCRIPTION = @TERRITORY_DESCRIPTION,PARENT_TERRITORY = @PARENT_TERRITORY, TERRITORY_FOR = @TERRITORY_FOR, MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE,INACTIVE = @INACTIVE " & vbCrLf & _
                            " WHERE TERRITORY_ID = @O_TERRITORY_ID ;" & vbCrLf & _
                            " END "
                    With commandUpdate
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@O_TERRITORY_ID", SqlDbType.VarChar, 14, "TERRITORY_ID")
                        .Parameters("@O_TERRITORY_ID").SourceVersion = DataRowVersion.Original
                        .Parameters.Add("@TERRITORY_AREA", SqlDbType.VarChar, 100, "TERRITORY_AREA")
                        .Parameters.Add("@REGIONAL_ID", SqlDbType.VarChar, 16, "REGIONAL_ID")
                        .Parameters.Add("@TERRITORY_DESCRIPTION", SqlDbType.VarChar, 150, "TERRITORY_DESCRIPTION")
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .Parameters.Add("@TERRITORY_FOR", SqlDbType.VarChar, 5, "TERRITORY_FOR")
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100, "MODIFY_BY")
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime, 0, "MODIFY_DATE")
                        .Parameters.Add("@PARENT_TERRITORY", SqlDbType.VarChar, 16, "PARENT_TERRITORY")
                    End With
                    commandUpdate.Transaction = Me.SqlTrans
                    Me.SqlDat.Update(UpdatedRows) : commandUpdate.Parameters.Clear()
                End If
                If DeletedRows.Length > 0 Then
                    Query = " SET NOCOUNT ON ;" & vbCrLf & _
                            " IF EXISTS(SELECT TERRITORY_ID FROM TERRITORY WHERE TERRITORY_ID = @O_TERRITORY_ID) " & vbCrLf & _
                            " BEGIN  DELETE FROM TERRITORY WHERE TERRITORY_ID = @O_TERRITORY_ID " & vbCrLf & _
                            " END "
                    With commandDelete
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@O_TERRITORY_ID", SqlDbType.VarChar, 14, "TERRITORY_ID")
                        .Parameters("@O_TERRITORY_ID").SourceVersion = DataRowVersion.Original
                    End With
                    commandDelete.Transaction = Me.SqlTrans
                    Me.SqlDat.Update(DeletedRows) : commandDelete.Parameters.Clear()
                End If


                'Me.SqlDat.Update(ds.Tables("Territory"))

                'Me.SqlCom.CommandText = "SELECT * FROM TERRITORY_MANAGER"
                'Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                'Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                'Me.SqlDat.Update(ds.Tables("Territory_Manager"))

                InsertedRows = ds.Tables("Territory_Manager").Select("", "", DataViewRowState.Added)
                UpdatedRows = ds.Tables("Territory_Manager").Select("", "", DataViewRowState.ModifiedCurrent Or DataViewRowState.ModifiedOriginal)
                DeletedRows = ds.Tables("Territory_Manager").Select("", "", DataViewRowState.Deleted)

                If InsertedRows.Length > 0 Then
                    If IsNothing(commandInsert) Then
                        commandInsert = Me.SqlConn.CreateCommand()
                    End If
                    If IsNothing(commandInsert.Transaction) Then
                        commandInsert.Transaction = Me.SqlTrans
                    End If
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "IF NOT EXISTS(SELECT TM_ID FROM TERRITORY_MANAGER WHERE TM_ID = @TM_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " INSERT INTO TERRITORY_MANAGER(TM_ID,MANAGER,HP,DESCRIPTIONS,CREATE_BY,CREATE_DATE,INACTIVE) " & vbCrLf & _
                            " VALUES(@TM_ID,@MANAGER,@HP,@DESCRIPTIONS,@CREATE_BY,@CREATE_DATE,0) " & vbCrLf & _
                            " END " & vbCrLf & _
                            " ELSE " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " UPDATE TERRITORY_MANAGER SET MANAGER = @MANAGER,HP = @HP,DESCRIPTIONS = @DESCRIPTIONS,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE,INACTIVE = @INACTIVE " & vbCrLf & _
                            " WHERE TM_ID = @TM_ID ;" & vbCrLf & _
                            " END "
                    With commandInsert
                        .Parameters.Add("@TM_ID", SqlDbType.VarChar, 10, "TM_ID")
                        .Parameters.Add("@MANAGER", SqlDbType.VarChar, 100, "MANAGER")
                        .Parameters.Add("@HP", SqlDbType.VarChar, 20, "HP")
                        .Parameters.Add("@DESCRIPTIONS", SqlDbType.VarChar, 150, "DESCRIPTIONS")
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 100, "CREATE_BY")
                        .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_DATE")
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100, "MODIFY_BY")
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime, 0, "MODIFY_DATE")
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .CommandType = CommandType.Text
                        .CommandText = Query
                    End With

                    SqlDat.InsertCommand = commandInsert
                    SqlDat.Update(InsertedRows) : commandInsert.Parameters.Clear()
                End If
                If UpdatedRows.Length > 0 Then
                    If IsNothing(commandUpdate) Then : commandUpdate = Me.SqlConn.CreateCommand() : End If
                    If IsNothing(commandUpdate.Transaction) Then
                        commandUpdate.Transaction = Me.SqlTrans
                    End If

                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "IF EXISTS(SELECT TM_ID FROM TERRITORY_MANAGER WHERE TM_ID = @O_TM_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " UPDATE TERRITORY_MANAGER SET MANAGER = @MANAGER,HP = @HP,DESCRIPTIONS = @DESCRIPTIONS,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE,INACTIVE = @INACTIVE " & vbCrLf & _
                            " WHERE TM_ID = @O_TM_ID ;" & vbCrLf & _
                            " END "
                    With commandUpdate
                        .Parameters.Add("@MANAGER", SqlDbType.VarChar, 100, "MANAGER")
                        .Parameters.Add("@HP", SqlDbType.VarChar, 20, "HP")
                        .Parameters.Add("@DESCRIPTIONS", SqlDbType.VarChar, 150, "DESCRIPTIONS")
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 0, "MODIFY_BY")
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime, 0, "MODIFY_DATE")
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .Parameters.Add("@O_TM_ID", SqlDbType.VarChar, 10, "TM_ID")
                        .Parameters("@O_TM_ID").SourceVersion = DataRowVersion.Original
                        .CommandType = CommandType.Text
                        .CommandText = Query
                    End With
                    SqlDat.UpdateCommand = commandUpdate
                    SqlDat.Update(UpdatedRows) : commandUpdate.Parameters.Clear()
                End If
                If DeletedRows.Length > 0 Then

                    If IsNothing(commandDelete) Then : commandDelete = Me.SqlConn.CreateCommand() : End If
                    If IsNothing(commandDelete.Transaction) Then
                        commandDelete.Transaction = Me.SqlTrans
                    End If
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                     "IF EXISTS(SELECT TM_ID FROM TERRITORY_MANAGER WHERE TM_ID = @O_TM_ID) " & vbCrLf & _
                     " BEGIN " & vbCrLf & _
                     " DELETE FROM TERRITORY_MANAGER WHERE TM_ID = @O_TM_ID ;" & vbCrLf & _
                     " END "
                    With commandDelete
                        .Parameters.Add("@O_TM_ID", SqlDbType.VarChar, 10, "TM_ID")
                        .Parameters("@O_TM_ID").SourceVersion = DataRowVersion.Original
                        .CommandType = CommandType.Text
                        .CommandText = Query
                    End With
                    SqlDat.DeleteCommand = commandDelete
                    SqlDat.Update(DeletedRows) : commandDelete.Parameters.Clear()
                End If

                'Me.SqlCom.CommandText = "SELECT * FROM DIST_REGIONAL"
                'Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                'Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                'Me.SqlDat.Update(ds.Tables("Regional"))

                InsertedRows = ds.Tables("Regional").Select("", "", DataViewRowState.Added)
                UpdatedRows = ds.Tables("Regional").Select("", "", DataViewRowState.ModifiedCurrent Or DataViewRowState.ModifiedOriginal)
                DeletedRows = ds.Tables("Regional").Select("", "", DataViewRowState.Deleted)

                If InsertedRows.Length > 0 Then
                    If IsNothing(commandInsert) Then
                        commandInsert = Me.SqlConn.CreateCommand()
                    End If
                    If IsNothing(commandInsert.Transaction) Then
                        commandInsert.Transaction = Me.SqlTrans
                    End If
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    "IF NOT EXISTS(SELECT REGIONAL_ID FROM DIST_REGIONAL WHERE REGIONAL_ID = @REGIONAL_ID ) " & vbCrLf & _
                    " BEGIN " & vbCrLf & _
                    " INSERT INTO DIST_REGIONAL(REGIONAL_ID,REGIONAL_AREA,MANAGER,HP,PARENT_REGIONAL,CREATE_BY,CREATE_DATE,INACTIVE) " & vbCrLf & _
                    " VALUES(@REGIONAL_ID,@REGIONAL_AREA,@MANAGER,@HP,@PARENT_REGIONAL,@CREATE_BY,@CREATE_DATE,0) " & vbCrLf & _
                    " END " & vbCrLf & _
                    " ELSE " & vbCrLf & _
                    " BEGIN " & vbCrLf & _
                    " UPDATE DIST_REGIONAL SET REGIONAL_AREA = @REGIONAL_AREA,MANAGER = @MANAGER,HP = @HP,PARENT_REGIONAL = @PARENT_REGIONAL,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE,INACTIVE = @INACTIVE " & vbCrLf & _
                    " WHERE REGIONAL_ID = @REGIONAL_ID ;" & vbCrLf & _
                    " END "
                    With commandInsert
                        .Parameters.Add("@REGIONAL_ID", SqlDbType.VarChar, 10, "REGIONAL_ID")
                        .Parameters.Add("@REGIONAL_AREA", SqlDbType.VarChar, 100, "REGIONAL_AREA")
                        .Parameters.Add("@MANAGER", SqlDbType.VarChar, 100, "MANAGER")
                        .Parameters.Add("@HP", SqlDbType.VarChar, 20, "HP")
                        .Parameters.Add("@PARENT_REGIONAL", SqlDbType.VarChar, 10, "PARENT_REGIONAL")
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 100, "CREATE_BY")
                        .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_DATE")
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100, "MODIFY_BY")
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime, 0, "MODIFY_DATE")
                        .CommandText = Query
                        .CommandType = CommandType.Text
                    End With
                    SqlDat.InsertCommand = commandInsert : SqlDat.Update(InsertedRows) : commandInsert.Parameters.Clear()
                End If
                If UpdatedRows.Length > 0 Then
                    If IsNothing(commandUpdate) Then
                        commandUpdate = Me.SqlConn.CreateCommand()
                    End If
                    If IsNothing(commandUpdate.Transaction) Then
                        commandUpdate.Transaction = Me.SqlTrans
                    End If
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "IF EXISTS(SELECT REGIONAL_ID FROM DIST_REGIONAL WHERE REGIONAL_ID = @O_REGIONAL_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " UPDATE DIST_REGIONAL SET REGIONAL_AREA = @REGIONAL_AREA,MANAGER = @MANAGER,HP = @HP,PARENT_REGIONAL = @PARENT_REGIONAL,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE,INACTIVE = @INACTIVE " & vbCrLf & _
                            " WHERE REGIONAL_ID = @O_REGIONAL_ID ;" & vbCrLf & _
                            " END "
                    With commandUpdate
                        .Parameters.Add("@O_REGIONAL_ID", SqlDbType.VarChar, 10, "REGIONAL_ID")
                        .Parameters("@O_REGIONAL_ID").SourceVersion = DataRowVersion.Original
                        .Parameters.Add("@REGIONAL_AREA", SqlDbType.VarChar, 150, "REGIONAL_AREA")
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .Parameters.Add("@MANAGER", SqlDbType.VarChar, 100, "MANAGER")
                        .Parameters.Add("@HP", SqlDbType.VarChar, 20, "HP")
                        .Parameters.Add("@PARENT_REGIONAL", SqlDbType.VarChar, 10, "PARENT_REGIONAL")
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100, "MODIFY_BY")
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime, 0, "MODIFY_DATE")
                        .CommandType = CommandType.Text
                        .CommandText = Query
                    End With
                    SqlDat.UpdateCommand = commandUpdate : SqlDat.Update(UpdatedRows) : commandUpdate.Parameters.Clear()
                End If
                If DeletedRows.Length > 0 Then
                    If IsNothing(commandDelete) Then
                        commandDelete = Me.SqlConn.CreateCommand()
                    End If
                    If IsNothing(commandDelete.Transaction) Then
                        commandDelete.Transaction = Me.SqlTrans
                    End If
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "IF EXISTS(SELECT REGIONAL_ID FROM DIST_REGIONAL WHERE REGIONAL_ID = @O_REGIONAL_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " DELETE FROM DIST_REGIONAL WHERE REGIONAL_ID = @O_REGIONAL_ID ; " & vbCrLf & _
                            " END "
                    With commandDelete
                        .Parameters.Add("@O_REGIONAL_ID", SqlDbType.VarChar, 10, "REGIONAL_ID")
                        .Parameters("@O_REGIONAL_ID").SourceVersion = DataRowVersion.Original
                        .CommandType = CommandType.Text
                        .CommandText = Query
                    End With
                    SqlDat.DeleteCommand = commandDelete : SqlDat.Update(DeletedRows) : commandDelete.Parameters.Clear()
                End If

                'Me.SqlCom.CommandText = "SELECT * FROM FS "
                'Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                'Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                'Me.SqlDat.Update(ds.Tables("FIELD_SUPERVISOR"))
                InsertedRows = ds.Tables("FIELD_SUPERVISOR").Select("", "", DataViewRowState.Added)
                UpdatedRows = ds.Tables("FIELD_SUPERVISOR").Select("", "", DataViewRowState.ModifiedCurrent Or DataViewRowState.ModifiedOriginal)
                DeletedRows = ds.Tables("FIELD_SUPERVISOR").Select("", "", DataViewRowState.Deleted)
                If InsertedRows.Length > 0 Then
                    If IsNothing(commandInsert) Then
                        commandInsert = Me.SqlConn.CreateCommand()
                    End If
                    If IsNothing(commandInsert.Transaction) Then
                        commandInsert.Transaction = Me.SqlTrans
                    End If
                    Query = "SET NOCOUNT  ON ;" & vbCrLf & _
                            " IF NOT EXISTS(SELECT FS_ID FROM FS WHERE FS_ID = @FS_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " INSERT INTO FS(FS_ID,FS_NAME,CUR_TERRITORY_ID,HP,INACTIVE,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                            "  VALUES(@FS_ID,@FS_NAME,@CUR_TERRITORY_ID,@HP,0,@CREATE_BY,@CREATE_DATE) ;" & vbCrLf & _
                            " END " & vbCrLf & _
                            " ELSE " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " UPDATE FS SET FS_NAME = @FS_NAME,CUR_TERRITORY_ID = @CUR_TERRITORY_ID,HP = @HP,INACTIVE = @INACTIVE,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " & vbCrLf & _
                            " WHERE FS_ID = @FS_ID " & vbCrLf & _
                            " END "

                    With commandInsert
                        .Parameters.Add("@FS_ID", SqlDbType.VarChar, 16, "FS_ID")
                        .Parameters.Add("@FS_NAME", SqlDbType.VarChar, 100, "FS_NAME")
                        .Parameters.Add("@CUR_TERRITORY_ID", SqlDbType.VarChar, 10, "CUR_TERRITORY_ID")
                        .Parameters.Add("@HP", SqlDbType.VarChar, 20, "HP")
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 0, "CREATE_BY")
                        .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_DATE")
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100, "MODIFY_BY")
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime, 0, "MODIFY_DATE")
                        .CommandType = CommandType.Text : .CommandText = Query
                    End With
                    SqlDat.InsertCommand = commandInsert : SqlDat.Update(InsertedRows) : commandInsert.Parameters.Clear()
                End If
                If (UpdatedRows.Length > 0) Then
                    If IsNothing(commandUpdate) Then
                        commandUpdate = Me.SqlConn.CreateCommand()
                    End If
                    If IsNothing(commandUpdate.Transaction) Then
                        commandUpdate.Transaction = Me.SqlTrans
                    End If
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            " IF EXISTS(SELECT FS_ID FROM FS WHERE FS_ID = @O_FS_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " UPDATE FS SET FS_NAME = @FS_NAME,CUR_TERRITORY_ID = @CUR_TERRITORY_ID,HP = @HP,INACTIVE = @INACTIVE,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " & vbCrLf & _
                            " WHERE FS_ID = @O_FS_ID " & vbCrLf & _
                            " END "
                    With commandUpdate
                        .Parameters.Add("@O_FS_ID", SqlDbType.VarChar, 16, "FS_ID")
                        .Parameters.Add("@FS_NAME", SqlDbType.VarChar, 100, "FS_NAME")
                        .Parameters.Add("@CUR_TERRITORY_ID", SqlDbType.VarChar, 10, "CUR_TERRITORY_ID")
                        .Parameters.Add("@HP", SqlDbType.VarChar, 20, "HP")
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100, "MODIFY_BY")
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime, 0, "MODIFY_DATE")
                        .CommandType = CommandType.Text : .CommandText = Query
                    End With
                    SqlDat.UpdateCommand = commandUpdate : SqlDat.Update(UpdatedRows) : commandUpdate.Parameters.Clear()
                End If
                If DeletedRows.Length > 0 Then
                    If IsNothing(commandDelete) Then
                        commandDelete = Me.SqlConn.CreateCommand()
                    End If
                    If IsNothing(commandDelete.Transaction) Then
                        commandDelete.Transaction = Me.SqlTrans
                    End If
                    Query = " SET NOCOUNT ON ;" & vbCrLf & _
                            " IF EXISTS(SELECT FS_ID FROM FS WHERE FS_ID = @O_FS_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " DELETE FROM FS WHERE FS_ID = @O_FS_ID ;" & vbCrLf & _
                            " END "
                    With commandDelete
                        .Parameters.Add("@O_FS_ID", SqlDbType.VarChar, 16, "FS_ID")
                        .Parameters("@O_FS_ID").SourceVersion = DataRowVersion.Original
                        .CommandType = CommandType.Text : .CommandText = Query
                    End With
                    SqlDat.DeleteCommand = commandDelete : SqlDat.Update(DeletedRows) : commandDelete.Parameters.Clear()
                End If
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
    End Class
End Namespace

