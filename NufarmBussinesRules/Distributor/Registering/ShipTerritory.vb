Namespace DistributorRegistering
    Public Class ShipTerritory
        Inherits NufarmBussinesRules.DistributorRegistering.DistributorRegistering
        Private m_ViewTerritoryArea As DataView
        Private m_ViewShipToTerritory As DataView
        Public Shadows TERRITORY_ID As String 'VARCHAR(5),
        Public TERRITORY_AREA As String 'VARCHAR(30),
        Public TERRITORY_DESCRIPTION As String 'VARCHAR(150),
        Private m_ViewUnReservedTerritoryArea As DataView
        'PUBLIC CREATE_BY VARCHAR(30)
        Public Sub New()
            MyBase.New()
            Me.m_ViewShipToTerritory = Nothing
            Me.m_ViewTerritoryArea = Nothing
            Me.TERRITORY_ID = ""
            Me.TERRITORY_DESCRIPTION = ""
            Me.TERRITORY_AREA = ""
        End Sub
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewShipToTerritory) Then
                Me.m_ViewShipToTerritory.Dispose()
                Me.m_ViewShipToTerritory = Nothing
            End If
            If Not IsNothing(Me.m_ViewTerritoryArea) Then
                Me.m_ViewTerritoryArea.Dispose()
                Me.m_ViewTerritoryArea = Nothing
            End If
            If Not IsNothing(Me.m_ViewUnReservedTerritoryArea) Then
                Me.m_ViewUnReservedTerritoryArea.Dispose()
                Me.m_ViewUnReservedTerritoryArea = Nothing
            End If
        End Sub
        Public ReadOnly Property ViewUnReservedTerritory() As DataView
            Get
                Return Me.m_ViewUnReservedTerritoryArea
            End Get
        End Property

        Public ReadOnly Property ViewTerritoryArea() As DataView
            Get
                Return Me.m_ViewTerritoryArea
            End Get
        End Property
        Public ReadOnly Property ViewShipToTerritory() As DataView
            Get
                Return Me.m_ViewShipToTerritory
            End Get
        End Property
        Public Shadows Sub GetDataView()
            Try
                Me.CreateCommandSql("Usp_Select_Territory_Area", "")
                Dim tblTerritory As New DataTable("TERRITORY_SHIP_TO")
                tblTerritory.Clear()
                Me.SqlDat = New SqlClient.SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(tblTerritory)
                Me.m_ViewTerritoryArea = tblTerritory.DefaultView()

                Me.SqlCom.CommandText = "Usp_Create_View_Distributor_Territory"
                Dim tblShipToArea As New DataTable("SHIP_TO_AREA")
                tblShipToArea.Clear()
                Me.SqlDat.Fill(tblShipToArea)
                Me.m_ViewShipToTerritory = tblShipToArea.DefaultView()

                Me.ClearCommandParameters()
                Me.SqlCom.CommandText = "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR"
                Me.SqlCom.CommandType = CommandType.Text
                Dim tblDistributor As New DataTable("DISTRIBUTOR")
                tblDistributor.Clear()
                Me.SqlDat.Fill(tblDistributor)
                Me.ViewDistributor() = tblDistributor.DefaultView()
                Me.CloseConnection()

            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Function IsHasReferencedData(ByVal TERRITORY_ID As String) As Boolean
            Try
                Me.CreateCommandSql("", "SELECT COUNT(TERRITORY_ID) FROM DIST_SHIP_TO" & _
                " WHERE TERRITORY_ID = '" & TERRITORY_ID & "'")
                If CInt(Me.ExecuteScalar()) > 0 Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function CreateViewTerritory() As DataView
            Try
                Me.CreateCommandSql("Usp_Select_Territory_Area", "")
                Dim tblTerritory As New DataTable("TERRITORY_SHIP_TO")
                tblTerritory.Clear()
                Me.SqlDat = New SqlClient.SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(tblTerritory)
                Me.m_ViewTerritoryArea = tblTerritory.DefaultView()
                Return Me.m_ViewTerritoryArea
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        'TERRITORY UNTUK DI BINDING DI CHECKED COMBO
        Public Function CreateViewTerritory(ByVal DistributorID As String)
            Try
                Me.CreateCommandSql("Usp_Select_Territory_UnReserved_by_Distributor", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                Dim tblTerritory As New DataTable("TERRITORY")
                tblTerritory.Clear()
                Me.FillDataTable(tblTerritory)
                Me.m_ViewUnReservedTerritoryArea = tblTerritory.DefaultView()
                Return Me.m_ViewUnReservedTerritoryArea
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Sub DeleteTerritoryArea(ByVal TERRITORY_ID As String)
            Try
                Me.CreateCommandSql("Usp_Delete_Territory_Area", "")
                Me.AddParameter("@TERRITORY_ID", SqlDbType.VarChar, TERRITORY_ID, 10)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Sub DeleteShipToArea(ByVal DIST_SHIP_TO_ID As String)
            Try
                Me.CreateCommandSql("Usp_Delete_Dist_Ship_To", "")
                Me.AddParameter("@DIST_SHIP_TO_ID", SqlDbType.VarChar, DIST_SHIP_TO_ID, 20)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Function CreateViewShipToArea() As DataView
            Try
                Me.CreateCommandSql("Usp_Create_View_Distributor_Territory", "")
                Dim tblShipToArea As New DataTable("SHIP_TO_AREA")
                tblShipToArea.Clear()
                Me.FillDataTable(tblShipToArea)
                Me.m_ViewShipToTerritory = tblShipToArea.DefaultView()
                Return Me.m_ViewShipToTerritory
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Sub SaveTerritoryArea(ByVal SaveType As String)
            Try

                Select Case SaveType
                    Case "Insert"
                        Me.CreateCommandSql("Usp_Insert_Territory_Area", "")
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                    Case "Update"
                        Me.CreateCommandSql("Usp_Update_Territory_Area", "")
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30)
                End Select
                Me.AddParameter("@TERRITORY_ID", SqlDbType.VarChar, Me.TERRITORY_ID, 10) '  VARCHAR(5),
                Me.AddParameter("@TERRITORY_AREA", SqlDbType.VarChar, Me.TERRITORY_AREA, 30) 'VARCHAR(30),
                Me.AddParameter("@TERRITORY_DESCRIPTION", SqlDbType.VarChar, Me.TERRITORY_DESCRIPTION, 150) ' VARCHAR(150),
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Sub SaveShiptoTerritory(ByVal TERRITORY_IDS As Collection)
            Try
                If Not TERRITORY_IDS.Count <= 0 Then
                    Me.CreateCommandSql("Usp_Insert_Dist_Ship_To", "")
                    Me.OpenConnection()
                    Me.BeginTransaction()
                    Me.SqlCom.Transaction = Me.SqlTrans
                    For I As Integer = 1 To TERRITORY_IDS.Count
                        Dim DIST_SHIP_TO_ID As String = Me.DistributorID + TERRITORY_IDS(I).ToString()
                        Me.AddParameter("@DIST_SHIP_TO_ID", SqlDbType.VarChar, DIST_SHIP_TO_ID, 20)
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DistributorID, 10) 'VARCHAR(10),
                        Me.AddParameter("@TERRITORY_ID", SqlDbType.VarChar, TERRITORY_IDS(I).ToString(), 10) 'VARCHAR(5),
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                        'Me.ExecuteNonQuery()
                        Me.SqlCom.ExecuteNonQuery()
                        Me.ClearCommandParameters()
                    Next
                    Me.CommiteTransaction()
                    Me.CloseConnection()
                End If
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
    End Class
End Namespace

