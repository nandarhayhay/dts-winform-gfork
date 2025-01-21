Imports System.Data.SqlClient
Namespace DistributorRegistering
    Public Class Ship_To
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private m_ViewDistributor As DataView
        Private m_ViewTerritory As DataView
        Private m_ViewManager As DataView
        Private m_ViewShip_To As DataView
        Private m_DataSet As DataSet
        Private Query As String = ""
        Public Sub New()
            MyBase.New()
            Me.m_ViewDistributor = Nothing
            Me.m_ViewManager = Nothing
            Me.m_ViewShip_To = Nothing
            Me.m_ViewTerritory = Nothing
        End Sub
        Public Overloads Sub dispose(ByVal disposing As Boolean)
            MyBase.Dispose(True)
            If Not IsNothing(Me.m_ViewDistributor) Then
                Me.m_ViewDistributor.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_ViewDistributor.Dispose()
                Me.m_ViewDistributor = Nothing
            End If
            If Not IsNothing(Me.m_ViewManager) Then
                Me.m_ViewManager.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_ViewManager.Dispose()
                Me.m_ViewManager = Nothing
            End If
            If Not IsNothing(Me.m_ViewShip_To) Then
                Me.m_ViewShip_To.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_ViewShip_To.Dispose()
                Me.m_ViewShip_To = Nothing
            End If
            If Not IsNothing(Me.m_ViewTerritory) Then
                Me.m_ViewTerritory.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_ViewTerritory.Dispose()
                Me.m_ViewTerritory = Nothing
            End If
        End Sub
        Public ReadOnly Property ViewDistributor() As DataView
            Get
                Return Me.m_ViewDistributor
            End Get
        End Property
        Public ReadOnly Property ViewTerritory() As DataView
            Get
                Return Me.m_ViewTerritory
            End Get
        End Property
        Public ReadOnly Property ViewManager() As DataView
            Get
                Return Me.m_ViewManager
            End Get
        End Property
        Public ReadOnly Property ViewShip_To() As DataView
            Get
                Return Me.m_ViewShip_To
            End Get
        End Property
        Public ReadOnly Property GetDateSet() As DataSet
            Get
                Return Me.m_DataSet
            End Get
        End Property
        Public Sub GetDataview()
            Try
                Dim tblDistributor As New DataTable("T_Distributor")
                Dim tblTerritory As New DataTable("T_Territory")
                Dim tblManager As New DataTable("T_Manager")
                Dim tblShip_To As New DataTable("T_Ship_To")
                tblDistributor.Clear()
                tblTerritory.Clear()
                tblManager.Clear()
                tblShip_To.Clear()
                'bikin view ship_to
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT * FROM SHIP_TO ;"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.CreateCommandSql("Usp_Create_View_Ship_To", "")
                Me.OpenConnection()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(tblShip_To) : Me.ClearCommandParameters()
                'bikin territory_area
                Query = "SET NOCOUNT ON ;SELECT TERRITORY_ID,TERRITORY_AREA FROM TERRITORY WHERE (INACTIVE = 0 OR INACTIVE IS NULL);"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlDat.Fill(tblTerritory) : Me.ClearCommandParameters()
                'bikin view Manager
                Query = "SET NOCOUNT ON ;SELECT TM_ID,MANAGER FROM TERRITORY_MANAGER WHERE (INACTIVE = 0 OR INACTIVE IS NULL) ;"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlDat.Fill(tblManager) : Me.ClearCommandParameters()
                'bikin distributor
                'Me.SqlCom.CommandTex = "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR"
                'Me.SqlDat.Fill(tblDistributor)
                Me.CloseConnection()
                Me.m_DataSet = New DataSet()
                Me.m_DataSet.Clear()
                With Me.m_DataSet
                    .Tables.Add(tblShip_To)
                End With
                'Me.m_ViewDistributor = tblDistributor.DefaultView()
                Me.m_ViewManager = tblManager.DefaultView()
                Me.m_ViewShip_To = Me.m_DataSet.Tables("T_Ship_To").DefaultView()
                Me.m_ViewShip_To.Sort = "SHIP_TO_ID"
                Me.m_ViewShip_To.RowStateFilter = DataViewRowState.CurrentRows
                Me.m_ViewTerritory = tblTerritory.DefaultView()

            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Function IsHasReferenced(ByVal SHIP_TO_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Usp_Check_Referenced_Data_SHIP_TO", "")
                Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, SHIP_TO_ID, 25)
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
        Public Function getTerritoryID(ByVal DISTRIBUTOR_ID As String) As String
            Try
                Me.CreateCommandSql("", "SELECT TERRITORY_ID FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = '" & _
                DISTRIBUTOR_ID & "'")
                Dim territoryID As Object = Me.ExecuteScalar()
                If (territoryID Is Nothing) Or (IsDBNull(territoryID)) Then
                    Throw New System.Exception("TerritoryID for distributor is null")
                End If
                Return territoryID.Trim()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Sub SaveChanges(ByVal ds As DataSet)
            Try
                Me.OpenConnection() : Me.BeginTransaction()
                Dim drows() As DataRow
                drows = ds.Tables(0).Select("", "", DataViewRowState.Added)
                If drows.Length > 0 Then
                    Dim cmdInsert As SqlCommand = Me.SqlConn.CreateCommand() : Me.SqlDat = New SqlDataAdapter()
                    Query = "SET NOCOUNT ON " & vbCrLf & _
                            " IF NOT EXISTS(SELECT SHIP_TO_ID FROM SHIP_TO WHERE SHIP_TO_ID = @SHIP_TO_ID) " & vbCrLf & _
                            " BEGIN INSERT INTO SHIP_TO(SHIP_TO_ID,TERRITORY_ID,TM_ID,INACTIVE,SHIP_TO_DESCRIPTIONS,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                            " VALUES(@SHIP_TO_ID,@TERRITORY_ID,@TM_ID,0,@SHIP_TO_DESCRIPTIONS,@CREATE_BY,@CREATE_DATE) ; END "
                    With cmdInsert
                        .CommandType = CommandType.Text : .CommandText = Query
                        .Parameters.Add("@SHIP_TO_ID", SqlDbType.VarChar, 25).SourceColumn = "SHIP_TO_ID"
                        .Parameters.Add("@TERRITORY_ID", SqlDbType.VarChar, 10).SourceColumn = "TERRITORY_ID"
                        .Parameters.Add("@TM_ID", SqlDbType.VarChar, 10).SourceColumn = "TM_ID"
                        .Parameters.Add("CREATE_BY", SqlDbType.VarChar, 50).SourceColumn = "CREATE_BY"
                        .Parameters.Add("CREATE_DATE", SqlDbType.SmallDateTime).SourceColumn = "CREATE_DATE"
                        .Parameters.Add("@SHIP_TO_DESCRIPTIONS", SqlDbType.VarChar, 200).SourceColumn = "SHIP_TO_DESCRIPTIONS"
                        .Transaction = Me.SqlTrans
                    End With
                    Me.SqlDat.InsertCommand = cmdInsert
                    SqlDat.Update(drows)
                End If
                drows = ds.Tables(0).Select("", "", DataViewRowState.ModifiedOriginal)
                If drows.Length > 0 Then
                    Query = "SET NOCOUNT ON " & vbCrLf & _
                               " IF EXISTS(SELECT SHIP_TO_ID FROM SHIP_TO WHERE SHIP_TO_ID = @SHIP_TO_ID) " & vbCrLf & _
                               " BEGIN " & vbCrLf & _
                               " -- CHECK KE DATA BASE APAKAH SHIP_TO SUDAH ADA DI OA_SHIP_TO " & vbCrLf & _
                               "    IF EXISTS(SELECT SHIP_TO_ID FROM OA_SHIP_TO WHERE SHIP_TO_ID = @SHIP_TO_ID) " & vbCrLf & _
                               "     BEGIN UPDATE SHIP_TO SET INACTIVE = @INACTIVE,SHIP_TO_DESCRIPTIONS = @SHIP_TO_DESCRIPTIONS WHERE SHIP_TO_ID = @SHIP_TO_ID ; END " & vbCrLf & _
                               "    ELSE " & vbCrLf & _
                               "     BEGIN UPDATE SHIP_TO SET TERRITORY_ID = @TERRITORY_ID,TM_ID = @TM_ID,INACTIVE = @INACTIVE,SHIP_TO_DESCRIPTIONS = @SHIP_TO_DESCRIPTIONS WHERE SHIP_TO_ID = @SHIP_TO_ID ; END " & vbCrLf & _
                               " END "
                    Dim cmdUpdate As SqlCommand = SqlConn.CreateCommand()
                    With cmdUpdate
                        .CommandText = Query : .CommandType = CommandType.Text
                        .Parameters.Add("@SHIP_TO_ID", SqlDbType.VarChar, 25).SourceColumn = "SHIP_TO_ID"
                        .Parameters.Add("@TERRITORY_ID", SqlDbType.VarChar, 10).SourceColumn = "TERRITORY_ID"
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit).SourceColumn = "INACTIVE"
                        .Parameters.Add("@TM_ID", SqlDbType.VarChar, 10).SourceColumn = "TM_ID"
                        .Parameters("@SHIP_TO_ID").SourceVersion = DataRowVersion.Original
                        .Parameters.Add("@SHIP_TO_DESCRIPTIONS", SqlDbType.VarChar, 200).SourceColumn = "SHIP_TO_DESCRIPTIONS"
                        .Transaction = Me.SqlTrans
                    End With
                    Me.SqlDat.UpdateCommand = cmdUpdate : Me.SqlDat.Update(drows)
                End If
                drows = ds.Tables(0).Select("", "", DataViewRowState.Deleted)
                If drows.Length > 0 Then
                    Query = "SET NOCOUNT ON " & vbCrLf & _
                            "IF EXISTS(SELECT SHIP_TO_ID FROM OA_SHIP_TO WHERE SHIP_TO_ID = @SHIP_TO_ID) " & vbCrLf & _
                            " BEGIN RAISERROR('Can not delete data,Data Already used in OA',16,1);RETURN ; END " & vbCrLf & _
                            " DELETE FROM SHIP_TO WHERE SHIP_TO_ID = @SHIP_TO_ID ;"
                    Dim cmdDelete As SqlCommand = SqlConn.CreateCommand()
                    With cmdDelete
                        .CommandType = CommandType.Text : .CommandText = Query
                        .Parameters.Add("@SHIP_TO_ID", SqlDbType.VarChar, 25, "SHIP_TO_ID").SourceVersion = DataRowVersion.Original
                        .Transaction = Me.SqlTrans
                    End With
                    Me.SqlDat.DeleteCommand = cmdDelete : Me.SqlDat.Update(drows)
                End If
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Sub Delete(ByVal SHIP_TO_ID As String)
            Try
                Me.CreateCommandSql("Usp_Check_Referenced_Data_SHIP_TO", "")
                Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, SHIP_TO_ID, 25)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar()
                If CInt(Me.SqlCom.Parameters("@RETURN_VALUE").Value) > 0 Then
                    Me.CloseConnection()
                    Throw New Exception("Can not delete data " & vbCrLf & "Because has child referenced data")
                End If
                Me.SqlCom.CommandText = "Usp_Delete_SHIP_TO"
                Me.SqlCom.CommandType = CommandType.StoredProcedure
                Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, SHIP_TO_ID, 25)
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
    End Class

End Namespace

