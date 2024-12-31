Imports System.Data
Imports System.Data.SqlClient
Imports Nufarm.Domain
Namespace DistributorRegistering
    Public Class DistributorRegistering
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private m_DataViewDistributor As DataView
        Private m_DataViewRegional As DataView
        Private m_DataViewTerritory As DataView
        Private m_DataViewHolding As DataView
        Public DistributorID As String
        Public DistributorName As String
        Public Parent_Distributor_ID As Object
        Public TERRITORY_ID As String
        Public NPWP As Object
        Public Max_Disc_Per_PO As Decimal
        Public Address As Object
        Public ContactPerson As Object
        Public Phone As Object
        Public Fax As Object
        Public HP As Object
        Public Create_By As Object
        Public Create_date As Object
        Public Modify_By As Object
        Public Modify_Date As Object
        Public BIRTHDATE As Object
        Private Query As String = ""
        Public RESPONSIBLE_PERSON As String
        Public JOIN_DATE As Object = DBNull.Value
        Public HP1 As String = ""
        Public email As Object = DBNull.Value
        'Public Function getShipToDistributor(ByVal DistributorID As String)

        'End Function
        Public Function GetDistributorName(ByVal DistributorID As String) As String
            Dim DistName As String = ""
            Try
                Me.CreateCommandSql("", "SELECT DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = '" & DistributorID & "'")
                DistName = Me.ExecuteScalar().ToString()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return DistName
        End Function
        Public Function CreateView_DistributorSMS() As DataView
            Try
                'Me.SearcData("", "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM VIEW_DISTRIBUTOR")
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME,HP AS CONTACT1,HP1 AS CONTACT2,CONTACT AS CONTACT_PERSON FROM DIST_DISTRIBUTOR ; "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.baseChekTable = New DataTable("T_DISTRIBUTOR")
                OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(Me.baseChekTable)
                Me.ClearCommandParameters()
                Me.CloseConnection()

                Me.m_DataViewDistributor = Me.baseChekTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_DataViewDistributor
        End Function
        Public Function CreateView_RSMTM(ByVal SentTO As String, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = ""
                Select Case SentTO
                    Case "RSM"
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                              " SELECT [MANAGER] AS RSM_NAME,HP AS ContactMobile FROM DIST_REGIONAL WHERE INACTIVE = 0 ;"
                    Case "TMOrJTM"
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT FS_NAME AS TM_NAME,HP AS ContactMobile FROM FS WHERE INActive = 0 AND HP IS NOT NULL " & vbCrLf & _
                                " UNION " & vbCrLf & _
                                " SELECT [MANAGER] AS TM_NAME,HP AS ContactMobile FROM TERRITORY_MANAGER WHERE INACTIVE = 0 AND HP IS NOT NULL ;"
                End Select
                If Query = "" Then
                    Throw New Exception("Please define where SMS to send ")
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim dt As New DataTable("T_Manager")
                dt.Clear()
                Me.setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Function CreateView_DistributorSMS(ByVal MustCloseConnection As Boolean) As DataView
            Try
                'Me.SearcData("", "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM VIEW_DISTRIBUTOR")
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME,HP AS CONTACT1,HP1 AS CONTACT2,CONTACT AS CONTACT_PERSON FROM DIST_DISTRIBUTOR WHERE (HP IS NOT NULL AND HP != ''); "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.baseChekTable = New DataTable("T_DISTRIBUTOR")
                OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(Me.baseChekTable)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.m_DataViewDistributor = Me.baseChekTable.DefaultView()
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_DataViewDistributor
        End Function
        Public Function getRetailerDataContact(ByVal CustomerType As String) As DataTable
            Try
                ''get dataContact to
                Dim tbl1 As New DataTable("T_RetailerContact")
                Dim tbl2 As New DataTable("T_RetailerContact")
                'Dim column1 As DataColumn = tbl1.Columns("IDApp")
                'tbl1.PrimaryKey = New DataColumn() {column1}

                'ping dulu computer RUSTRI [IDD651G625_SQLEXPRESS].[Dprd].dbo.
                Dim IPAddress As String = ""
                Dim IsRunning As Boolean = False

                Dim IsAlive As Boolean = common.DefineConnection.pingAddress("IDD651G625", IPAddress)
                If IsAlive Then
                    IsRunning = common.DefineConnection.TestSqlServer(IPAddress, 1433)
                End If
                If IsRunning Then
                    'get Data from that computer
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT Ter.NameApp AS Territory,Kios.IDKios,Kios.Kios_Owner,ContactMobile,Kios.Address FROM [IDD651G625_SQLEXPRESS].[Dprd].dbo.Uv_Kios Kios " & vbCrLf & _
                            " INNER JOIN [IDD651G625_SQLEXPRESS].[Dprd].dbo.UserPriviledge UP ON UP.UserID = Kios.TMCode " & vbCrLf & _
                            " INNER JOIN [IDD651G625_SQLEXPRESS].[Dprd].dbo.ImpTerritory Ter ON Ter.CodeApp = UP.TerritoryCode  WHERE Kios.INActive = 0 AND Kios.CustomerType = @CustomerType ;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@CustomerType", SqlDbType.VarChar, CustomerType, 30)
                    Me.OpenConnection()
                    Me.setDataAdapter(Me.SqlCom).Fill(tbl1)
                    Me.ClearCommandParameters()
                End If
                'test computer Esti
                IPAddress = ""
                IsRunning = False
                IsAlive = common.DefineConnection.pingAddress("IDD5Z43825", IPAddress)
                If IsAlive Then
                    IsRunning = common.DefineConnection.TestSqlServer(IPAddress, 1433)
                End If
                If IsRunning Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT Ter.NameApp AS Territory,Kios.IDKios,Kios.Kios_Owner,ContactMobile,Kios.Address FROM [IDD5Z43825_SQLEXPRESS].[Dprd].dbo.Uv_Kios Kios " & vbCrLf & _
                            " INNER JOIN [IDD5Z43825_SQLEXPRESS].[Dprd].dbo.UserPriviledge UP ON UP.UserID = Kios.TMCode " & vbCrLf & _
                            " INNER JOIN [IDD5Z43825_SQLEXPRESS].[Dprd].dbo.ImpTerritory Ter ON Ter.CodeApp = UP.TerritoryCode  WHERE Kios.INActive = 0 AND Kios.CustomerType = @CustomerType ;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@CustomerType", SqlDbType.VarChar, CustomerType, 30)
                    Me.OpenConnection()
                    Me.setDataAdapter(Me.SqlCom).Fill(tbl1)
                    Me.ClearCommandParameters()
                End If
                If tbl1.Rows.Count > 0 And tbl2.Rows.Count > 0 Then
                    Dim column1 As DataColumn = tbl1.Columns("IDKios")
                    tbl1.PrimaryKey = New DataColumn() {column1}
                    tbl1.Merge(tbl2)
                End If
                tbl1.AcceptChanges()
                Me.CloseConnection()
                Return tbl1
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
            Return Nothing
        End Function
        Public Sub InsertSMSTable(ByVal tblMessage As DataTable)
            Try
                'find item territory
                Dim rows() As DataRow = tblMessage.Select("", "", DataViewRowState.Added)
                If rows.Length > 0 Then
                    Me.OpenConnection() : Me.BeginTransaction()

                    '==================== UNCOMMENT THIS AFTER DEBUGGING=================================================
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "  INSERT INTO SMS_TABLE(PO_REF_NO,CONTACT_PERSON,CONTACT_MOBILE,ORIGIN_COMPANY,MESSAGE,STATUS_SENT,SENT_DATE,SENT_BY)" & vbCrLf & _
                            " VALUES(@PO_REF_NO,@CONTACT_PERSON,@CONTACT_MOBILE,@ORIGIN_COMPANY,@MESSAGE,0, CURRENT_TIMESTAMP,@SENT_BY)"
                    '==========================================================================================================

                    '==========================COMMENT THIS AFTER DEBUGGING=================================================
                    'Query = "SET NOCOUNT ON;" & vbCrLf & _
                    '        "  INSERT INTO SMS_TABLE(PO_REF_NO,CONTACT_PERSON,CONTACT_MOBILE,ORIGIN_COMPANY,MESSAGE,STATUS_SENT,SENT_DATE,SENT_BY)" & vbCrLf & _
                    '        " VALUES(@PO_REF_NO,@CONTACT_PERSON,@CONTACT_MOBILE,@ORIGIN_COMPANY,@MESSAGE,0, '01/2015/01',@SENT_BY)"
                    '================================================================================================================

                    If IsNothing(Me.SqlCom) Then
                        Me.CreateCommandSql("", Query)
                    Else
                        Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.SqlCom.Transaction = Me.SqlTrans
                    Me.SqlCom.Parameters.Add("@PO_REF_NO", SqlDbType.VarChar, 30).Value = DBNull.Value ' VARCHAR(25),
                    Me.SqlCom.Parameters.Add("@CONTACT_PERSON", SqlDbType.VarChar, 100, "CONTACT_PERSON") ' VARCHAR(30),
                    Me.SqlCom.Parameters.Add("@ORIGIN_COMPANY", SqlDbType.VarChar, 100, "ORIGIN_COMPANY") ' VARCHAR(50),
                    Me.SqlCom.Parameters.Add("@CONTACT_MOBILE", SqlDbType.VarChar, 20, "CONTACT_MOBILE") ' VARCHAR(20),
                    Me.SqlCom.Parameters.Add("@MESSAGE", SqlDbType.VarChar, 160, "MESSAGE") ' VARCHAR(1000),
                    Me.SqlCom.Parameters.Add("@SENT_BY", SqlDbType.VarChar, 100, "SENT_BY") ' VARCHAR(30)
                    Me.SqlDat.InsertCommand = Me.SqlCom
                    Me.SqlDat.Update(rows)
                    Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            Finally
                Me.SqlDat.InsertCommand = Nothing
            End Try
        End Sub
        Public Function HasReferencedChildData(ByVal DISTRIBUTOR_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Select_ReferencedChild_DIST_DISTRIBUTOR", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
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
        Public Function IsDistributorExist(ByVal DistributorID As String) As Boolean
            MyBase.SearcData("", "SELECT DISTRIBUTOR_ID FROM Nufarm.dbo.DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = '" & DistributorID & "'")
            If MyBase.baseChekTable.Rows.Count > 0 Then
                Return True
            End If
            Return False
        End Function
        Public Function SetInactive(ByVal distributorID As String, ByVal ValB As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                "UPDATE DIST_DISTRIBUTOR SET INACTIVE = @INActive,MODIFY_BY = @ModifiedBy,MODIFY_DATE=GETDATE() WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else
                    Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID)
                Me.AddParameter("@INActive", SqlDbType.Bit, ValB)
                Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try

        End Function
        Public Sub GetDataView()
            'DATAVIEW TERRITORY 
            Query = "SET NOCOUNT ON ; SELECT TERRITORY_ID,TERRITORY_AREA FROM TERRITORY WHERE INActive = 0;"
            If IsNothing(Me.SqlCom) Then
                Me.CreateCommandSql("sp_executesql", "")
            Else
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            End If
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
            Dim dtTable As New DataTable("T_Teritory") : dtTable.Clear()
            Me.OpenConnection() : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
            'Me.FillDataTable("", "SELECT TERRITORY_ID,TERRITORY_AREA FROM TERRITORY", "T_Teritory")
            Me.Viewterritory = dtTable.DefaultView()
            Me.Viewterritory.Sort = "TERRITORY_ID"
            '   DATAVIEW REGIONAL
            Query = "SET NOCOUNT ON ; SELECT REGIONAL_ID,REGIONAL_AREA FROM DIST_REGIONAL WHERE INActive = 0;"
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            dtTable = New DataTable("T_Regional") : dtTable.Clear() : Me.SqlDat.Fill(dtTable)
            Me.ClearCommandParameters()

            'Me.FillDataTable("Sp_Select_Regional", "", "T_Regional")
            Me.ViewRegional = dtTable.DefaultView()
            Me.ViewRegional.Sort = "REGIONAL_ID"
            'DATAVIEW DISTRIBUTOR
            Query = "SET NOCOUNT ON ; SELECT * FROM DIST_DISTRIBUTOR ;"
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            dtTable = New DataTable("T_Distributor") : dtTable.Clear() : Me.SqlDat.Fill(dtTable)
            Me.ClearCommandParameters()

            'Me.FillDataTable("Sp_Select_Distributor", "", "T_Distributor")
            Me.ViewDistributor = dtTable.DefaultView()
            Me.ViewDistributor.Sort = "DISTRIBUTOR_ID"
            'DATAVIEW HOLDING
            'Me.FillDataTable("Sp_Select_Distributor", "", "T_Holding")
            Me.ViewHolding = dtTable.DefaultView()
            Me.ViewHolding.Sort = "DISTRIBUTOR_ID"
            Me.CloseConnection() : Me.ClearCommandParameters()
        End Sub
        Public Function FindDistributorHolding(ByVal Distid As Object) As Integer
            Me.ViewHolding.RowStateFilter = DataViewRowState.OriginalRows
            'Me.ViewHolding.Sort = "DISTRIBUTOR_NAME"
            Return Me.ViewHolding.Find(Distid)
        End Function
        Public Function FindTerritory(ByVal TM_ID As Object) As Integer
            Me.Viewterritory.RowStateFilter = DataViewRowState.OriginalRows
            Return Me.Viewterritory.Find(TM_ID)
        End Function
        Public Function IsValidDistributorDescription(ByVal distributorID As String) As Boolean
            Try
                Me.CreateCommandSql("Usp_Chek_Validity_Distributor", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return False
                Else
                    Me.ClearCommandParameters() : Return True
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Shadows Sub InsertData()
            Try
                MyBase.InsertData("Sp_Insert_Distributor", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DistributorID, 10) ' VARCHAR(10),
                Me.AddParameter("@DISTRIBUTOR_NAME", SqlDbType.VarChar, Me.DistributorName, 50) ' VARCHAR(50),
                Me.AddParameter("@RESPONSIBLE_PERSON", SqlDbType.VarChar, Me.RESPONSIBLE_PERSON, 50)
                Me.AddParameter("@TERRITORY_ID", SqlDbType.VarChar, Me.TERRITORY_ID, 10) ' VARCHAR(10),
                Me.AddParameter("@PARENT_DISTRIBUTOR_ID", SqlDbType.VarChar, Me.Parent_Distributor_ID, 10) ' VARCHAR(10),
                Me.AddParameter("@NPWP", SqlDbType.VarChar, Me.NPWP, 30) ' VARCHAR(30),
                Me.AddParameter("@MAX_DISC_PER_PO", SqlDbType.Decimal, Me.Max_Disc_Per_PO)
                Me.AddParameter("@ADDRESS", SqlDbType.VarChar, Me.Address, 150)
                Me.AddParameter("@CONTACT", SqlDbType.VarChar, Me.ContactPerson, 30) ' VARCHAR(30),
                Me.AddParameter("@PHONE", SqlDbType.VarChar, Me.Phone, 30) ' VARCHAR(30),
                Me.AddParameter("@FAX", SqlDbType.VarChar, Me.Fax, 20) ' VARCHAR(20),
                Me.AddParameter("@HP", SqlDbType.VarChar, Me.HP, 20) ' VARCHAR(20),
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                Me.AddParameter("@BIRTH_DATE", SqlDbType.DateTime, Me.BIRTHDATE)
                Me.AddParameter("@JOIN_DATE", SqlDbType.SmallDateTime, Me.JOIN_DATE)
                Me.AddParameter("@HP1", SqlDbType.VarChar, Me.HP1, 20)
                Me.AddParameter("@Email", SqlDbType.NVarChar, Me.email, 150)
                Me.OpenConnection()
                Me.ExecuteNonQuery()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try

        End Sub
        Public Sub DeleteDistributor(ByVal DISTRIBUTOR_ID As String)
            Try
                Me.CreateCommandSql("Sp_Delete_Distributor", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Me.OpenConnection()
                Me.ExecuteNonQuery()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Shadows Sub UpdateData()
            Try
                MyBase.UpdateData("Sp_Update_Distributor", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DistributorID, 10) 'VARCHAR(10),
                Me.AddParameter("@DISTRIBUTOR_NAME", SqlDbType.VarChar, Me.DistributorName, 50) ' VARCHAR(50),
                Me.AddParameter("@RESPONSIBLE_PERSON", SqlDbType.VarChar, Me.RESPONSIBLE_PERSON, 50)
                Me.AddParameter("@TERRITORY_ID", SqlDbType.VarChar, Me.TERRITORY_ID, 10) ' VARCHAR(5),
                Me.AddParameter("@PARENT_DISTRIBUTOR_ID", SqlDbType.VarChar, Me.Parent_Distributor_ID, 10) ' VARCHAR(10),
                Me.AddParameter("@NPWP", SqlDbType.VarChar, Me.NPWP, 30) ' VARCHAR(30),
                Me.AddParameter("@MAX_DISC_PER_PO", SqlDbType.Decimal, Me.Max_Disc_Per_PO) ' INT,
                Me.AddParameter("@ADDRESS", SqlDbType.VarChar, Me.Address, 150) ' VARCHAR(150),
                Me.AddParameter("@CONTACT", SqlDbType.VarChar, Me.ContactPerson, 30) ' VARCHAR(30),
                Me.AddParameter("@PHONE", SqlDbType.VarChar, Me.Phone, 30) ' VARCHAR(30),
                Me.AddParameter("@FAX", SqlDbType.VarChar, Me.Fax, 20) 'VARCHAR(20),
                Me.AddParameter("@HP", SqlDbType.VarChar, Me.HP, 20) ' VARCHAR(20),
                Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) '  VARCHAR(30),
                Me.AddParameter("@BIRTH_DATE", SqlDbType.DateTime, Me.BIRTHDATE)
                Me.AddParameter("@JOIN_DATE", SqlDbType.SmallDateTime, Me.JOIN_DATE)
                Me.AddParameter("@HP1", SqlDbType.VarChar, Me.HP1, 20)
                Me.AddParameter("@Email", SqlDbType.NVarChar, Me.email, 150)
                Me.OpenConnection()
                Me.ExecuteNonQuery()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            
        End Sub
        Public Property ViewHolding() As DataView
            Get
                Return Me.m_DataViewHolding
            End Get
            Set(ByVal value As DataView)
                Me.m_DataViewHolding = value
            End Set
        End Property
        Public Property ViewDistributor() As DataView
            Get
                Return Me.m_DataViewDistributor
            End Get
            Set(ByVal value As DataView)
                Me.m_DataViewDistributor = value
            End Set
        End Property
        Public Property ViewRegional() As DataView
            Get
                Return Me.m_DataViewRegional
            End Get
            Set(ByVal value As DataView)
                Me.m_DataViewRegional = value
            End Set
        End Property

        Public Property Viewterritory() As DataView
            Get
                Return Me.m_DataViewTerritory
            End Get
            Set(ByVal value As DataView)
                Me.m_DataViewTerritory = value
            End Set
        End Property
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_DataViewDistributor) Then
                Me.m_DataViewDistributor.Dispose()
                Me.m_DataViewDistributor = Nothing
            End If
            If Not IsNothing(Me.m_DataViewRegional) Then
                Me.m_DataViewRegional.Dispose()
                Me.m_DataViewRegional = Nothing
            End If
            If Not IsNothing(Me.m_DataViewTerritory) Then
                Me.m_DataViewTerritory.Dispose()
                Me.m_DataViewTerritory = Nothing
            End If
            If Not IsNothing(Me.ViewHolding) Then
                Me.ViewHolding.Dispose()
                Me.ViewHolding = Nothing
            End If
            If Not IsNothing(Me.Modify_Date) Then
                Me.Modify_Date = Nothing
            End If
            If Not IsNothing(Me.Create_date) Then
                Me.Create_date = Nothing
            End If
            Me.Modify_By = Nothing
            Me.Modify_Date = Nothing
            Me.Address = Nothing
            Me.ContactPerson = Nothing
            Me.Create_By = Nothing
            Me.Create_date = Nothing
            Me.DistributorID = Nothing
            Me.DistributorName = Nothing
            Me.Fax = Nothing
            Me.HP = Nothing
        End Sub

        Public Sub New()
            Me.m_DataViewDistributor = Nothing
            Me.m_DataViewHolding = Nothing
            Me.m_DataViewRegional = Nothing
            Me.m_DataViewTerritory = Nothing
            Me.Max_Disc_Per_PO = 0
            Me.Modify_By = DBNull.Value
            Me.Modify_Date = DBNull.Value
            Me.Address = DBNull.Value
            Me.ContactPerson = DBNull.Value
            Me.Create_By = DBNull.Value
            Me.Create_date = DBNull.Value
            Me.DistributorID = ""
            Me.DistributorName = ""
            Me.Fax = DBNull.Value
            Me.HP = DBNull.Value
            Me.Parent_Distributor_ID = DBNull.Value
            Me.NPWP = DBNull.Value
        End Sub
    End Class


End Namespace

