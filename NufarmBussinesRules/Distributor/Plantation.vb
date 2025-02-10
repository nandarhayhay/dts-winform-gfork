Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows.Forms
Imports NufarmBussinesRules.SharedClass
Namespace Plantation
    Public Class Plantation
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private Query As String = ""
        Public Sub New()
            MyBase.New()
        End Sub
        Public ListTerritory As List(Of String)
        Public PlantationName As String = "", TerritoryID As String = "", PlantGroupID As String, DescriptionsPlantation As String = ""
        Public Function GetTerritory(ByVal searchString As String) As DataView
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT TERRITORY_ID,TERRITORY_AREA FROM TERRITORY ;"
                If Not String.IsNullOrEmpty(searchString) Then
                    Query &= vbCrLf
                    Query &= " WHERE TERRITORY_AREA LIKE '%" & searchString & "%' ;"
                End If
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Dim dt As New DataTable("t_Territory") : dt.Clear()
                Me.SqlDat.Fill(dt) : Me.ClearCommandParameters()
                Return dt.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub SaveGroupData(ByVal GroupName As String, ByVal Description As String, ByVal mode As common.Helper.SaveMode, ByRef PlantGroupID As String)
            Try
                Dim RecCountGroupID As Integer = 0
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT SUM (row_count) + 1 FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('PLANTATION_GROUP')  AND (index_id=0 or index_id=1)  ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.OpenConnection()
                RecCountGroupID = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                'test apakah ada nama group yang sama
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT 1 WHERE EXISTS(SELECT PLANT_GROUP_NAME FROM PLANTATION_GROUP WHERE PLANT_GROUP_NAME = '" & GroupName & "') ;"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim result As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(result) Then
                    If CInt(result) > 0 And mode = common.Helper.SaveMode.Insert Then
                        If MessageBox.Show("Group Name has existed" & vbCrLf & "If you continue you will insert double groupname" & vbCrLf & "Continue anyway ?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                            Me.CloseConnection() : Return
                        End If
                    End If
                End If
                If mode = common.Helper.SaveMode.Insert Then
                    Dim num As String = "00000", BFind As Boolean = True
                    While BFind = True
                        Dim X As Integer = num.Length - CStr(RecCountGroupID).Length
                        If X <= 0 Then
                            num = ""
                        Else
                            num = num.Remove(X - 1, CStr(RecCountGroupID).Length)
                        End If
                        num &= RecCountGroupID.ToString()
                        PlantGroupID = num
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                  "SELECT 1 WHERE EXISTS(SELECT PLANT_GROUP_ID FROM PLANTATION_GROUP WHERE PLANT_GROUP_ID = @PLANT_GROUP_ID);"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@PLANT_GROUP_ID", SqlDbType.VarChar, PlantGroupID, 10)
                        result = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        If Not IsNothing(result) Then
                            If (Convert.ToInt32(result) > 0) Then
                                BFind = True : RecCountGroupID += 1
                            Else : BFind = False
                            End If
                        Else : BFind = False
                        End If
                    End While
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                           " INSERT INTO PLANTATION_GROUP(PLANT_GROUP_ID,PLANT_GROUP_NAME,DESCRIPTIONS,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                           " VALUES(@PLANT_GROUP_ID,@PLANT_GROUP_NAME,@DESCRIPTIONS,@CREATE_BY,@CREATE_DATE); "
                    'Me.ResetCommandText(CommandType.Text, Query)
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " IF NOT EXISTS(SELECT PLANT_GROUP_ID FROM PLANTATION_GROUP WHERE PLANT_GROUP_ID = @PLANT_GROUP_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " RAISERROR('PlantationID has been deleted',16,1); RETURN;" & vbCrLf & _
                            " END " & vbCrLf & _
                            " UPDATE PLANTATION_GROUP SET PLANT_GROUP_NAME = @PLANT_GROUP_NAME,DESCRIPTIONS = @DESCRIPTIONS,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " & vbCrLf & _
                            " WHERE PLANT_GROUP_ID = @PLANT_GROUP_ID ;"
                End If
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PLANT_GROUP_ID", SqlDbType.VarChar, PlantGroupID, 5)
                Me.AddParameter("@PLANT_GROUP_NAME", SqlDbType.VarChar, GroupName, 100)
                If Not String.IsNullOrEmpty(Description) Then
                    Me.AddParameter("@DESCRIPTIONS", SqlDbType.VarChar, Description, 150)
                Else
                    Me.AddParameter("@DESCRIPTIONS", SqlDbType.VarChar, DBNull.Value, 150)
                End If
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Sub SaveData(ByVal PlantationName As String, _
        ByVal PlantationID As String, ByVal Description As String, ByVal Mode As common.Helper.SaveMode)
            Try
                Dim IDDistributors As String = PlantationID.Substring(0, PlantationID.IndexOf("|"))
                If (Mode = common.Helper.SaveMode.Insert) Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                              "IF EXISTS(SELECT PLANTATION_ID FROM PLANTATION WHERE PLANTATION_ID = @PLANTATION_ID)" & vbCrLf & _
                              "BEGIN " & vbCrLf & _
                              " RAISERROR('Data has existed',16,1);RETURN;" & vbCrLf & _
                              "END " & vbCrLf & _
                              "INSERT INTO PLANTATION(PLANTATION_ID,PLANTATION_NAME,DESCRIPTIONS,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                              "VALUES(@PLANTATION_ID,@PLANTATION_NAME,@DESCRIPTION,@CREATE_BY,@CREATE_DATE);"
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "IF NOT EXISTS(SELECT PLANTATION_ID FROM PLANTATION WHERE PLANTATION_ID = @PLANTATION_ID)" & vbCrLf & _
                            "BEGIN " & vbCrLf & _
                            " RAISERROR('PlantationID has been deleted',16,1);RETURN;" & vbCrLf & _
                            "END " & vbCrLf & _
                            "UPDATE PLANTATION SET PLANTATION_NAME = @PLANTATION_NAME,DESCRIPTIONS = @DESCRIPTION," & _
                            "MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE WHERE PLANTATION_ID = @PLANTATION_ID;"
                End If
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@PLANTATION_ID", SqlDbType.VarChar, PlantationID, 50)
                Me.AddParameter("@PLANTATION_NAME", SqlDbType.VarChar, PlantationName, 150)
                Me.AddParameter("@DESCRIPTION", SqlDbType.VarChar, Description, 200)
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()))
                Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()))
                Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Private Sub insertPlantation(ByVal Mode As common.Helper.SaveMode, ByVal PlantID As String)
            For I As Integer = 0 To Me.ListTerritory.Count - 1
                Me.TerritoryID = ListTerritory(I).ToString()
                Dim RecCountGroupID As Integer = 0, PlantationID As String = ""
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT SUM (row_count) + 1 FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID ('PLANTATION')  AND (index_id=0 or index_id=1) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim PlantRecCount As Integer = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                If (Mode = common.Helper.SaveMode.Insert) Then

                    Dim num As String = "00000", BFind As Boolean = True
                    While BFind = True
                        Dim X As Integer = num.Length - CStr(PlantRecCount).Length
                        If X <= 0 Then
                            num = ""
                        Else
                            num = num.Remove(X - 1, CStr(PlantRecCount).Length)
                        End If
                        num &= PlantRecCount.ToString()
                        PlantationID = TerritoryID & "-" & num
                        If Me.PlantGroupID <> "" Then
                            PlantationID &= "-" & PlantGroupID
                        End If
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT 1 WHERE EXISTS(SELECT PLANTATION_ID FROM PLANTATION WHERE PLANTATION_ID = @PLANTATION_ID) ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@PLANTATION_ID", SqlDbType.NVarChar, PlantationID)
                        Dim Result As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        If Not IsNothing(Result) Then
                            If CInt(Result) > 0 Then
                                BFind = True
                            Else : BFind = False : PlantRecCount += 1
                            End If
                        Else : BFind = False : PlantRecCount += 1
                        End If
                    End While
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                              "IF EXISTS(SELECT PLANTATION_ID FROM PLANTATION WHERE PLANTATION_ID = @PLANTATION_ID)" & vbCrLf & _
                              "BEGIN " & vbCrLf & _
                              " RAISERROR('Data has existed',16,1);RETURN;" & vbCrLf & _
                              "END " & vbCrLf & _
                              "INSERT INTO PLANTATION(PLANTATION_ID,PLANTATION_NAME,TERRITORY_ID,PLANT_GROUP_ID,DESCRIPTIONS,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                              "VALUES(@PLANTATION_ID,@PLANTATION_NAME,@TERRITORY_ID,@PLANT_GROUP_ID,@DESCRIPTION,@CREATE_BY,@CREATE_DATE);"
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "IF NOT EXISTS(SELECT PLANTATION_ID FROM PLANTATION WHERE PLANTATION_ID = @PLANTATION_ID)" & vbCrLf & _
                            "BEGIN " & vbCrLf & _
                            " RAISERROR('PlantationID has been deleted',16,1);RETURN;" & vbCrLf & _
                            "END " & vbCrLf & _
                            "UPDATE PLANTATION SET PLANTATION_NAME = @PLANTATION_NAME,DESCRIPTIONS = @DESCRIPTION,TERRITORY_ID = @TERRITORY_ID,PLANT_GROUP_ID = @PLANT_GROUP_ID," & _
                            "MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE WHERE PLANTATION_ID = @PLANTATION_ID;"
                End If
                Me.ResetCommandText(CommandType.Text, Query)
                If Mode = common.Helper.SaveMode.Insert Then
                    Me.AddParameter("@PLANTATION_ID", SqlDbType.VarChar, PlantationID, 50)
                Else : Me.AddParameter("@PLANTATION_ID", SqlDbType.VarChar, PlantID, 50)
                End If
                Me.AddParameter("@PLANTATION_NAME", SqlDbType.VarChar, PlantationName, 100)
                If Me.TerritoryID = "" Then
                    Me.AddParameter("@TERRITORY_ID", SqlDbType.VarChar, DBNull.Value, 10)
                Else
                    Me.AddParameter("@TERRITORY_ID", SqlDbType.VarChar, Me.TerritoryID, 10)
                End If
                If Me.PlantGroupID = "" Then
                    Me.AddParameter("@PLANT_GROUP_ID", SqlDbType.VarChar, DBNull.Value, 10)
                Else : Me.AddParameter("@PLANT_GROUP_ID", SqlDbType.VarChar, Me.PlantGroupID, 10)
                End If
                'ME.AddParameter("@PLANT_GROUP_ID",SqlDbType.VarChar,
                Me.AddParameter("@DESCRIPTION", SqlDbType.VarChar, DescriptionsPlantation, 150)
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()))
                Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()))
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            Next
        End Sub
        Private Function CheckExisting(ByVal PlantationName As String, Optional ByVal PlantGroupID As String = "", Optional ByVal TerritoryID As String = "") As Boolean
            Try
                'CHECK existing
                'jika plant_group_id & TerritoryID ada
                'check existing plantation_name + plant_group_id + TerritoryID

                If PlantGroupID <> "" And TerritoryID <> "" Then
                    Query = "SET NOCOUNT ON;" & _
                    "SELECT PLANTATION_NAME FROM PLANTATION WHERE PLANT_GROUP_ID = @PLANT_GROUP_ID AND TERRITORY_ID = @TERRITORY_ID AND PLANTATION_NAME = @PLANTATION_NAME ;"
                ElseIf TerritoryID <> "" Then
                    Query = "SET NOCOUNT ON;" & _
                    "SELECT PLANTATION_NAME FROM PLANTATION WHERE TERRITORY_ID = @TERRITORY_ID AND PLANTATION_NAME = @PLANTATION_NAME ;"
                Else
                    Query = "SET NOCOUNT ON;" & _
                             "SELECT PLANTATION_NAME FROM PLANTATION WHERE PLANTATION_NAME = @PLANTATION_NAME ;"
                End If

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@PLANTATION_NAME", SqlDbType.VarChar, PlantationName)
                If PlantGroupID <> "" And TerritoryID <> "" Then
                    Me.AddParameter("@PLANT_GROUP_ID", SqlDbType.VarChar, PlantGroupID)
                    Me.AddParameter("@TERRITORY_ID", SqlDbType.VarChar, TerritoryID)
                ElseIf TerritoryID <> "" Then
                    Me.AddParameter("@TERRITORY_ID", SqlDbType.VarChar, TerritoryID)
                End If
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return (retval.ToString() <> "")
                End If
                Return False
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Sub SaveData(ByVal Mode As common.Helper.SaveMode, ByVal MustCloseConnection As Boolean, Optional ByVal PlantID As String = "")
            Try
                'RUMUS UNTUK BIKIN PLANTATION_ID
                'GROUP_ID DI BIKIN BERDASARKAN RUMUS GP0001'
                'GP SINGKATAN DARI GROUP_PLANTATION 0001 ADALAH RUNNING NUMBER
                
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                If Me.SqlCom Is Nothing Then : Me.SqlCom = New SqlCommand() : Me.SqlCom.Connection = Me.GetConnection() : End If
                If Me.ListTerritory.Count > 0 Then
                    Me.insertPlantation(Mode, PlantID)
                Else
                    Dim RecCountGroupID As Integer = 0, PlantationID As String = ""
                    'CHECK existing
                    'jika plant_group_id & TerritoryID ada
                    'check existing plantation_name + plant_group_id + TerritoryID
                    If Me.CheckExisting(Me.PlantationName, Me.PlantGroupID, Me.TerritoryID) Then
                        Throw New Exception("Data has existed")
                    End If
                    If (Mode = common.Helper.SaveMode.Insert) Then
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT SUM (row_count) + 1 FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('PLANTATION')  AND (index_id=0 or index_id=1) ;"
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                        Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                        End If
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Dim PlantRecCount As Integer = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                        Dim num As String = "00000"
                        Dim X As Integer = num.Length - CStr(PlantRecCount).Length
                        If X <= 0 Then
                            num = ""
                        Else
                            num = num.Remove(X - 1, CStr(PlantRecCount).Length)
                        End If
                        num &= PlantRecCount.ToString()
                        PlantationID = num
                        If Me.PlantGroupID <> "" Then
                            PlantationID &= "-" & PlantGroupID
                        End If
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                  "IF EXISTS(SELECT PLANTATION_ID FROM PLANTATION WHERE PLANTATION_ID = @PLANTATION_ID)" & vbCrLf & _
                                  "BEGIN " & vbCrLf & _
                                  " RAISERROR('Data has existed',16,1);RETURN;" & vbCrLf & _
                                  "END " & vbCrLf & _
                                  "INSERT INTO PLANTATION(PLANTATION_ID,PLANTATION_NAME,TERRITORY_ID,PLANT_GROUP_ID,DESCRIPTIONS,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                                  "VALUES(@PLANTATION_ID,@PLANTATION_NAME,@TERRITORY_ID,@PLANT_GROUP_ID,@DESCRIPTION,@CREATE_BY,@CREATE_DATE);"
                    Else
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "IF NOT EXISTS(SELECT PLANTATION_ID FROM PLANTATION WHERE PLANTATION_ID = @PLANTATION_ID)" & vbCrLf & _
                                "BEGIN " & vbCrLf & _
                                " RAISERROR('PlantationID has been deleted',16,1);RETURN;" & vbCrLf & _
                                "END " & vbCrLf & _
                                "UPDATE PLANTATION SET PLANTATION_NAME = @PLANTATION_NAME,DESCRIPTIONS = @DESCRIPTION,TERRITORY_ID = @TERRITORY_ID,PLANT_GROUP_ID = @PLANT_GROUP_ID," & _
                                "MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE WHERE PLANTATION_ID = @PLANTATION_ID;"
                    End If
                    Me.ResetCommandText(CommandType.Text, Query)
                    If Mode = common.Helper.SaveMode.Insert Then
                        Me.AddParameter("@PLANTATION_ID", SqlDbType.VarChar, PlantationID, 50)
                    Else : Me.AddParameter("@PLANTATION_ID", SqlDbType.VarChar, PlantID, 50)
                    End If
                    Me.AddParameter("@PLANTATION_NAME", SqlDbType.VarChar, PlantationName, 100)
                    Me.AddParameter("@TERRITORY_ID", SqlDbType.VarChar, DBNull.Value, 10)
                    If Me.PlantGroupID = "" Then
                        Me.AddParameter("@PLANT_GROUP_ID", SqlDbType.VarChar, DBNull.Value, 10)
                    Else : Me.AddParameter("@PLANT_GROUP_ID", SqlDbType.VarChar, Me.PlantGroupID, 10)
                    End If
                    'ME.AddParameter("@PLANT_GROUP_ID",SqlDbType.VarChar,
                    Me.AddParameter("@DESCRIPTION", SqlDbType.VarChar, DescriptionsPlantation, 150)
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()))
                    Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()))
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If

                Me.CommiteTransaction()
                If MustCloseConnection Then
                    Me.CloseConnection()
                End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            Finally
                'clearkan property
                Me.PlantGroupID = "" : Me.TerritoryID = ""
            End Try
        End Sub


        Public Function PopulateQuery(ByVal SearchBy As String, ByVal value As Object, _
               ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer, _
               ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes, ByVal IsChangedCriteriaSearch As Boolean) As DataView
            Try
                Me.OpenConnection()
                Dim ResolvedCriteria As String = common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                If IsChangedCriteriaSearch Then
                    'bikin table Temporary dengan max data berdesarkan max record
                    'Query = "SET NOCOUNT ON; " & vbCrLf & _
                    '        "DECLARE @V_MAXRECORD INT; " & vbCrLf & _
                    '        "SELECT @V_MAXRECORD = CASE @SearchBy WHEN 'PLANTATION_NAME' THEN (SELECT COUNT(IDApp)  FROM PLANTATION WHERE PLANTATION_NAME " & ResolvedCriteria & ") " & vbCrLf & _
                    '        "                   WHEN 'PLANT_GROUP_NAME' THEN (SELECT COUNT(PL.IDApp) FROM PLANTATION PL INNER JOIN PLANTATION_GROUP PG ON  PL.PLANT_GROUP_ID = PG.PLANT_GROUP_ID " & vbCrLf & _
                    '        "                        WHERE PG.PLANT_GROUP_NAME " & ResolvedCriteria & ") " & vbCrLf & _
                    '        " WHEN 'TERRITORY_AREA' THEN (SELECT COUNT(PL.IDApp) FROM PLANTATION PL INNER JOIN TERRITORY TER ON PL.TERRITORY_ID = TER.TERRITORY_ID  " & vbCrLf & _
                    '        "   WHERE TER.TERRITORY_AREA " & ResolvedCriteria & ") ;"
                    'If IsNothing(Me.SqlCom) Then
                    '    Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                    'Else : Me.ResetCommandText(CommandType.Text, Query)
                    'End If
                    'Me.AddParameter("@SearchBy", SqlDbType.VarChar, SearchBy, 50)
                    'Dim retval As Integer = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                    Query = "SET  NOCOUNT ON; " & vbCrLf & _
                            " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PLANTATION_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                            " BEGIN DROP TABLE ##T_PLANTATION_" & Me.ComputerName & " ; END " & vbCrLf & _
                            " SELECT  PL.IDApp,PL.PLANTATION_ID,PL.PLANTATION_NAME,PG.PLANT_GROUP_ID,'PLANT_GROUP_NAME' = CASE WHEN PL.PLANT_GROUP_ID IS NULL THEN PL.PLANTATION_NAME ELSE PG.PLANT_GROUP_NAME END,PG.DESCRIPTIONS,PL.CREATE_DATE,TER.TERRITORY_ID,TER.TERRITORY_AREA INTO ##T_PLANTATION_" & Me.ComputerName & " " & vbCrLf & _
                            " FROM PLANTATION PL LEFT OUTER JOIN PLANTATION_GROUP PG ON PL.PLANT_GROUP_ID = PG.PLANT_GROUP_ID " & vbCrLf & _
                            " LEFT OUTER JOIN TERRITORY TER ON TER.TERRITORY_ID = PL.TERRITORY_ID " & vbCrLf
                    Select Case SearchBy
                        Case "PLANTATION_NAME" : Query &= " WHERE PL.PLANTATION_NAME " & ResolvedCriteria & vbCrLf
                        Case "PLANT_GROUP_NAME" : Query &= " WHERE PG.PLANT_GROUP_NAME " & ResolvedCriteria & vbCrLf
                        Case "TERRITORY_AREA" : Query &= " WHERE TER.TERRITORY_AREA " & ResolvedCriteria & vbCrLf
                    End Select
                    Query &= " CREATE CLUSTERED INDEX IX_T_Plantation ON ##T_PLANTATION_" & Me.ComputerName & "(IDApp,PLANTATION_NAME,TERRITORY_AREA) ;"
                    If IsNothing(Me.SqlCom) Then
                        Me.CreateCommandSql("sp_executesql", "")
                    Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    End If
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PLANTATION_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " SELECT TOP " & PageSize & " PLANTATION_ID,PLANTATION_NAME,PLANT_GROUP_ID AS GROUP_ID,PLANT_GROUP_NAME AS GROUP_NAME,DESCRIPTIONS,CREATE_DATE,TERRITORY_ID,TERRITORY_AREA FROM ##T_PLANTATION_" & Me.ComputerName & " " & vbCrLf & _
                        " WHERE IDApp < ALL(SELECT TOP " & (PageSize * (PageIndex - 1)).ToString() & " IDApp FROM ##T_PLANTATION_" & Me.ComputerName & " " & vbCrLf & _
                        " WHERE (" & SearchBy & " " & ResolvedCriteria & " ) ORDER BY IDApp DESC) " & vbCrLf & _
                        " AND " & SearchBy & " " & ResolvedCriteria & " ORDER BY IDApp DESC OPTION(KEEP PLAN); " & vbCrLf & _
                        " END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim Dt As New DataTable("PLANTATION_DATA") : Dt.Clear()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(Dt) : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT SUM (row_count) FROM Tempdb.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('tempdb..##T_PLANTATION_" & Me.ComputerName & "')  AND (index_id=0 or index_id=1) ;"
                'Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                'SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & _
                '        " FROM Uv_Price_Distributor WHERE (" & SearchBy
                'Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                'Query &= ") ORDER BY IDApp ASC)"
                'Query &= " AND " & SearchBy
                'Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                'Query &= " ORDER BY IDApp ASC OPTION(KEEP PLAN);"
                Dim dv As DataView = Dt.DefaultView()
                'dv.Sort = "CREATE_DATE DESC"
                Return dv
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function Delete(ByVal PlantationID As String) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "IF EXISTS(SELECT PLANTATION_ID FROM DIST_PLANT_PRICE WHERE PLANTATION_ID = '" _
                        & PlantationID & "')" & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " RAISERROR('Plantation has been held by Distributor',16,1);RETURN;" & vbCrLf & _
                        " END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If MessageBox.Show("Are you sure you want to delete data" & vbCrLf & "Operation can not be undone", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                    Me.CloseConnection() : Return False
                End If
                Query = "SET NOCOUNT ON; DELETE FROM PLANTATION WHERE PLANTATION_ID = '" & PlantationID & "'; "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteScalar() : Return True
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub deleteGroup(ByVal PlantgroupID As String, ByVal SearchGroup As String)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "SELECT PLANT_GROUP_ID FROM PLANTATION WHERE PLANT_GROUP_ID = @PLANT_GROUP_ID ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PLANT_GROUP_ID", SqlDbType.VarChar, PlantgroupID, 4)
                Dim Result As Object = Me.SqlCom.ExecuteScalar()
                If Not IsNothing(Result) And Not IsDBNull(Result) Then
                    Me.CloseConnection() : Me.ClearCommandParameters() : Throw New Exception("Can not delete data" & vbCrLf & "Data has had Plantation")
                End If
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                         " DELETE FROM PLANTATION_GROUP WHERE PLANT_GROUP_ID = @PLANT_GROUP_ID ;"
                Me.ResetCommandText(CommandType.Text, Query)
                'If IsNothing(Me.SqlCom) Then
                '    Me.CreateCommandSql("", Query)
                'Else
                'End If
                Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function GetRowByID(ByVal PlantationID As String) As DataTable
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT PLANTATION_NAME,DESCRIPTIONS FROM PLANTATION WHERE PLANTATION_ID = '" & PlantationID & "';"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_Plantation") : dtTable.Clear()
                Me.FillDataTable(dtTable) : Return dtTable
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetPlantation(ByVal ListDistributors As List(Of String)) As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT DPP.PLANTATION_ID,PL.PLANTATION_NAME,DESCRIPTIONS FROM " & vbCrLf & _
                        " ( " & vbCrLf & _
                        "SELECT DISTINCT PLANTATION_ID FROM DIST_PLANT_PRICE WHERE EXISTS(" & vbCrLf & _
                        "SELECT TOP 1 DISTRIBUTOR_ID FROM DIST_PLANT_PRICE WHERE DISTRIBUTOR_ID = '"
                For i As Integer = 0 To ListDistributors.Count - 1
                    Query &= ListDistributors(i).ToString()
                    Query &= "')"
                    If i < ListDistributors.Count - 1 Then
                        Query &= " AND EXISTS(SELECT TOP 1 DISTRIBUTOR_ID FROM DIST_PLANT_PRICE WHERE DISTRIBUTOR_ID = '"
                    End If
                Next
                Query &= ")DPP INNER JOIN PLANTATION PL ON PL.PLANTATION_ID = DPP.PLANTATION_ID OPTION(KEEP PLAN);"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim DtTable As New DataTable("T_Plantation") : DtTable.Clear()
                Me.FillDataTable(DtTable) : Return DtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub GetPlantation(ByVal SearchString As String, ByRef DV As DataView)
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                "SELECT TER.TERRITORY_AREA ,PL.PLANTATION_ID,PL.PLANTATION_NAME, 'GROUP_NAME' = CASE WHEN PL.PLANT_GROUP_ID IS NULL THEN PL.PLANTATION_NAME ELSE PG.PLANT_GROUP_NAME END FROM PLANTATION PL " & vbCrLf & _
                " LEFT OUTER JOIN PLANTATION_GROUP PG ON PL.PLANT_GROUP_ID = PG.PLANT_GROUP_ID LEFT OUTER JOIN TERRITORY TER ON TER.TERRITORY_ID = PL.TERRITORY_ID WHERE PL.PLANTATION_NAME LIKE '%" & SearchString & "%';"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_Plantation")
                dtTable.Clear() : Me.FillDataTable(dtTable)
                DV = dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function GetGroupPlantation(ByVal SearchGroup As String, ByVal TerritoryIDs As List(Of String)) As DataView
            Try
                Dim StrTerritories As String = "IN('"
                For i As Integer = 0 To TerritoryIDs.Count - 1
                    StrTerritories &= TerritoryIDs(i).ToString() & "'"
                    If i < TerritoryIDs.Count - 1 Then
                        StrTerritories &= ",'"
                    End If
                Next
                StrTerritories &= ")"
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT PG.PLANT_GROUP_ID AS GROUP_ID,PG.PLANT_GROUP_NAME AS GROUP_NAME,DESCRIPTIONS FROM PLANTATION_GROUP PG " & vbCrLf & _
                        " WHERE EXISTS(SELECT PLANT_GROUP_ID FROM PLANTATION WHERE PLANT_GROUP_ID = PG.PLANT_GROUP_ID AND TERRITORY_ID " & StrTerritories & " ) "

                If Not String.IsNullOrEmpty(SearchGroup) Then
                    Query &= vbCrLf & _
                            " AND PG.PLANT_GROUP_NAME LIKE '%" & SearchGroup & "%' ;"
                End If
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql(CommandType.StoredProcedure, "sp_executesql")
                Else : Me.ResetCommandText(CommandType.Text, "sp_executesql")
                End If

                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_Group")
                dtTable.Clear() : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection() : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                If dtTable.Rows.Count > 0 Then
                    Return dtTable.DefaultView()
                End If
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT TOP 100 PLANT_GROUP_ID AS GROUP_ID,PLANT_GROUP_NAME AS GROUP_NAME,DESCRIPTIONS FROM PLANTATION_GROUP ORDER BY CREATE_DATE DESC;" & vbCrLf  'WHERE PLANT_GROUP_NAME LIKE '%" & SearchGroup & "%'; "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Return Me.FillDataTable(New DataTable("t_Group_Plantation")).DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetGroupPlantation(ByVal SearchGroup As String) As DataView
            Try
             
                If Not String.IsNullOrEmpty(SearchGroup) Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT PLANT_GROUP_ID AS GROUP_ID,PLANT_GROUP_NAME AS GROUP_NAME,ISNULL(DESCRIPTIONS,'')AS DESCRIPTIONS FROM PLANTATION_GROUP " & vbCrLf & _
                            "  WHERE PLANT_GROUP_NAME LIKE '%" & SearchGroup & "%'; "
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                     " SELECT TOP 100 PLANT_GROUP_ID AS GROUP_ID,PLANT_GROUP_NAME AS GROUP_NAME,ISNULL(DESCRIPTIONS,'') AS DESCRIPTIONS FROM PLANTATION_GROUP ORDER BY CREATE_DATE DESC;" & vbCrLf  'WHERE PLANT_GROUP_NAME LIKE '%" & SearchGroup & "%'; "
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Return Me.FillDataTable(New DataTable("t_Group_Plantation")).DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getNewID() As Int32
            Try
                Query = "SET NOCOUNT ON;SELECT ISNULL(MAX(IDApp),0) + 1 FROM PLANTATION;"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Return CInt(Me.ExecuteScalar())
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function hasReservedInvoice(ByRef userName As String) As Boolean
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "  IF EXISTS(SELECT [NAME] FROM tempdb.sys.columns WHERE [NAME] = 'UserName' AND object_id=OBJECT_ID('tempdb..##T_START_DATE_" & Me.ComputerName & "')) " & vbCrLf & _
                        "   BEGIN " & vbCrLf & _
                        "       SELECT [UserName] FROM tempdb..##T_START_DATE_" & Me.ComputerName & " WHERE [UserName] != @UserName  ;" & vbCrLf & _
                        "   END " & vbCrLf & _
                        "END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@UserName", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then

                    If Not retval.ToString() Is String.Empty Then
                        userName = retval.ToString() : Return True
                    End If
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getReportPlantation(ByVal StartDate As DateTime, ByVal endDate As DateTime, ByVal IsDateChanged As Boolean) As DataSet
            Try
                Dim strDecStartDate As String = common.CommonClass.getNumericFromDate(StartDate)
                Dim strDecEndDate As String = common.CommonClass.getNumericFromDate(endDate)
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT 1 WHERE EXISTS(SELECT NAME FROM [tempdb].[sys].[objects]  WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U');"
                If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else : Me.CreateCommandSql("sp_executesql", "") : End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters() : Dim BTempStartDate As Boolean = False
                If Not IsNothing(retval) And Not IsDBNull(retval) Then : BTempStartDate = CInt(retval) > 0 : End If
                Dim StrStartDate As String = "", strEndDate As String = ""
                If Not BTempStartDate Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                             "SELECT START_DATE = @START_DATE,END_DATE = @END_DATE,UserName = @UserName INTO  ##T_START_DATE_" & Me.ComputerName & " ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    StrStartDate = Month(StartDate).ToString() + "/" + StartDate.Day.ToString() + "/" + Year(StartDate).ToString()
                    strEndDate = Month(endDate).ToString() + "/" + endDate.Day.ToString() + "/" + Year(endDate).ToString()

                    Me.AddParameter("@START_DATE", SqlDbType.VarChar, StrStartDate, 20)
                    Me.AddParameter("@END_DATE", SqlDbType.VarChar, strEndDate, 20)
                    Me.AddParameter("@UserName", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If

                'Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Dim retvalStartDate As String = "", retvalEndDate As String = ""
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT START_DATE,END_DATE FROM tempdb..##T_START_DATE_" & Me.ComputerName & " ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read() : retvalStartDate = SqlRe.GetString(0) : retvalEndDate = SqlRe.GetString(1) : End While
                Me.SqlRe.Close() : Me.ClearCommandParameters()
                Dim StoredProcNI87 As String = "Usp_Create_Temp_Invoice_Table", StoredProcNI109 = "Usp_Create_Temp_Invoice_Table_NI109"
                Dim StoredProcToUse As String = StoredProcNI87
                If DBInvoiceTo = CurrentInvToUse.NI109 Then
                    StoredProcToUse = StoredProcNI109
                End If
                If Not ((StrStartDate.Equals(retvalStartDate)) Or (strEndDate.Equals(retvalEndDate))) Then
                    'bikin baru
                    Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON;" & vbCrLf & _
                            "IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                            " BEGIN UPDATE tempdb..##T_START_DATE_" & Me.ComputerName & " SET START_DATE = @D_START_DATE,END_DATE = @D_END_DATE;  END " & vbCrLf & _
                            " ELSE " & vbCrLf & _
                            " BEGIN SELECT START_DATE = @D_START_DATE,END_DATE = @D_END_DATE,UserName = @UserName INTO  ##T_START_DATE_" & Me.ComputerName & " ; END " & vbCrLf & _
                            " IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_SELECT_INVOICE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                            " BEGIN  DROP TABLE tempdb..##T_SELECT_INVOICE_" & Me.ComputerName & " ; END " & vbCrLf & _
                            " EXEC " & StoredProcToUse & " @DEC_START_DATE = @D_START_DATE,@DEC_END_DATE = @D_END_DATE,@COMPUTERNAME = @C_NAME ; "
                Else
                    Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON;" & vbCrLf & _
                            "IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_SELECT_INVOICE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                            " BEGIN  EXEC " & StoredProcToUse & " @DEC_START_DATE = @D_START_DATE,@DEC_END_DATE = @D_END_DATE,@COMPUTERNAME = @C_NAME ; END " '& vbCrLf & _
                    '" IF NOT EXISTS(SELECT NAME FROM tempdb..SYSOBJECTS WHERE NAME = '##T_BRANDPACK' AND TYPE = 'U') " & vbCrLf & _
                    '" BEGIN  EXEC Usp_Create_Temp_Table_BrandPack; END "
                End If
                Me.ResetCommandText(CommandType.Text, Query)
                'Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                'Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Me.AddParameter("@UserName", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)
                Me.AddParameter("@D_START_DATE", SqlDbType.VarChar, StrDecStartDate)
                Me.AddParameter("@D_END_DATE", SqlDbType.VarChar, strDecEndDate)
                Me.AddParameter("@C_NAME", SqlDbType.VarChar, Me.ComputerName, 100)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If IsDateChanged Then
                    Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ;SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF; " & vbCrLf & _
                            "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Total_Qty_Plantation_" & Me.ComputerName & "' AND Type = 'U') " & vbCrLf & _
                            " BEGIN DROP TABLE ##T_Total_Qty_Plantation_" & Me.ComputerName & " ; END " & vbCrLf & _
                            " SELECT OPO.PO_REF_NO,OPO.PO_REF_DATE AS PO_DATE,OPO.DISTRIBUTOR_ID,tmpb.BRANDPACK_ID_DTS AS BRANDPACK_ID,OPB.TERRITORY_ID,OPB.PLANTATION_ID,OPB.PO_ORIGINAL_QTY,OPB.PO_PRICE_PERQTY AS [PRICE]," & vbCrLf & _
                            " INV.INVNUMBER,CAST( '' + SUBSTRING(CAST(INV.INVDATE AS VARCHAR(20)),5,2) +  '/' + RIGHT(CAST(INV.INVDATE AS VARCHAR(20)),2) +  '/' + LEFT(CAST(INV.INVDATE AS VARCHAR(20)),4) AS SMALLDATETIME)AS INVDATE ,(INV.QTY/ISNULL(SB.SPPB_QTY,0)) * OPB.PO_ORIGINAL_QTY AS INVOICE_QTY " & vbCrLf & _
                            " INTO ##T_Total_Qty_Plantation_" & Me.ComputerName & " FROM COMPARE_ITEM tmpb " & vbCrLf & _
                            " INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON tmpb.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
                            " INNER JOIN Nufarm.dbo.ORDR_PO_BRANDPACK OPB ON OPB.BRANDPACK_ID = tmpb.BRANDPACK_ID_DTS " & vbCrLf & _
                            " INNER JOIN Nufarm.dbo.ORDR_PURCHASE_ORDER OPO ON OPO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                            " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OOA.PO_REF_NO = OPO.PO_REF_NO " & vbCrLf & _
                            " AND ((INV.REFERENCE = OOA.RUN_NUMBER) OR (INV.PONUMBER = OPO.PO_REF_NO)) " & vbCrLf & _
                            " INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOAB.OA_ID = OOA.OA_ID AND OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                            " INNER JOIN SPPB_BRANDPACK SB ON SB.OA_BRANDPACK_ID = OOAB.OA_BRANDPACK_ID " & vbCrLf & _
                            " WHERE OPB.PO_ORIGINAL_QTY > 0 AND OPB.PLANTATION_ID IS NOT NULL ; " & vbCrLf & _
                            " CREATE CLUSTERED INDEX IX_T_T_Total_Qty_Plantation ON ##T_Total_Qty_Plantation_" & Me.ComputerName & "(BRANDPACK_ID,PLANTATION_ID,DISTRIBUTOR_ID) ;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    'bikin lagi temporarary table untuk sum nya

                    Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; " & vbCrLf & _
                            "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Sumary_Plantation_" & Me.ComputerName & "')" & vbCrLf & _
                            " BEGIN DROP TABLE ##T_Sumary_Plantation_" & Me.ComputerName & " ; END " & vbCrLf & _
                            " SELECT TERRITORY_ID,PLANTATION_ID,BRANDPACK_ID,ISNULL(SUM(INVOICE_QTY),0) AS TOTAL_INVOICE INTO ##T_Sumary_Plantation_" & Me.ComputerName & " FROM ##T_Total_Qty_Plantation_" & Me.ComputerName & " GROUP BY TERRITORY_ID,PLANTATION_ID,BRANDPACK_ID ;" & vbCrLf & _
                            " CREATE CLUSTERED INDEX IX_T_T_Sumary_Plantation ON ##T_Sumary_Plantation_" & Me.ComputerName & "(PLANTATION_ID,BRANDPACK_ID) ;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                ''sekarang tinggal generate report 
                'ambil report berdasarkan brand dulu
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; " & vbCrLf & _
                        "SELECT TS.PLANTATION_ID,PL.PLANTATION_NAME,TER.TERRITORY_AREA,'GROUP_NAME' = CASE WHEN PL.PLANT_GROUP_ID IS NULL THEN PL.PLANTATION_NAME ELSE PG.PLANT_GROUP_NAME END,BR.BRAND_ID,BR.BRAND_NAME,TS.BRANDPACK_ID,BB.BRANDPACK_NAME,TS.TOTAL_INVOICE AS TOTAL_ACTUAL " & vbCrLf & _
                        "FROM ##T_Sumary_Plantation_" & Me.ComputerName & " TS INNER JOIN PLANTATION PL ON TS.PLANTATION_ID = PL.PLANTATION_ID " & vbCrLf & _
                        " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = TS.BRANDPACK_ID " & vbCrLf & _
                        " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BB.BRAND_ID " & vbCrLf & _
                        " LEFT OUTER JOIN PLANTATION_GROUP PG ON PG.PLANT_GROUP_ID = PL.PLANT_GROUP_ID " & vbCrLf & _
                        " LEFT OUTER JOIN TERRITORY TER ON TER.TERRITORY_ID = TS.TERRITORY_ID ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtHeader As New DataTable("SUMMARY_OF_PLANTATION_REPORT") : dtHeader.Clear()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtHeader)
                Me.ClearCommandParameters()
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT TS.PLANTATION_ID,PL.PLANTATION_NAME,TS.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,TS.PO_REF_NO,TS.PO_DATE,TS.BRANDPACK_ID,BB.BRANDPACK_NAME,TS.PO_ORIGINAL_QTY,TS.PRICE,TS.INVNUMBER,TS.INVDATE,TS.INVOICE_QTY " & vbCrLf & _
                        " FROM ##T_Total_Qty_Plantation_" & Me.ComputerName & " TS INNER JOIN DIST_DISTRIBUTOR DR ON TS.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = TS.BRANDPACK_ID INNER JOIN PLANTATION PL ON PL.PLANTATION_ID = TS.PLANTATION_ID; "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtDetail As New DataTable("DETAIL_OF_PLANTATION_REPORT") : dtDetail.Clear()
                Me.SqlDat.Fill(dtDetail) : Me.ClearCommandParameters()

                ''update username nya 
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                      "IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                      " BEGIN " & vbCrLf & _
                      "  IF EXISTS(SELECT [NAME] FROM tempdb.sys.columns WHERE [NAME] = 'UserName' AND object_id=OBJECT_ID('tempdb..##T_START_DATE_" & Me.ComputerName & "'))  " & vbCrLf & _
                      "   BEGIN " & vbCrLf & _
                      "       UPDATE  tempdb..##T_START_DATE_" & Me.ComputerName & " SET [UserName] = '' ;" & vbCrLf & _
                      "   END " & vbCrLf & _
                      "END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                MyBase.baseDataSet = New DataSet("PLANTATION_REPORT") : MyBase.baseDataSet.Clear()
                MyBase.baseDataSet.Tables.Add(dtHeader) : MyBase.baseDataSet.Tables.Add(dtDetail)
                Me.ReleaseHoldUser()
                Return MyBase.baseDataSet

            Catch ex As Exception
                Try
                    Me.ReleaseHoldUser()
                Catch ex1 As Exception
                    Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex1
                End Try
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Private Sub ReleaseHoldUser()
            If Not Me.hasReservedInvoice(NufarmBussinesRules.User.UserLogin.UserName) Then
                If Not Me.hasReservedInvoice(NufarmBussinesRules.User.UserLogin.UserName) Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                              "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                              " BEGIN  DROP TABLE  tempdb..##T_START_DATE_" & Me.ComputerName & " ; END " & vbCrLf & _
                              " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Total_Qty_Plantation_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                              " BEGIN DROP TABLE tempdb..##T_Total_Qty_Plantation_" & Me.ComputerName & " ; END " & vbCrLf & _
                              " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Sumary_Plantation_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                              " BEGIN DROP TABLE tempdb..##T_Sumary_Plantation_" & Me.ComputerName & " ; END " & vbCrLf & _
                              " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SELECT_INVOICE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                              " BEGIN DROP TABLE Tempdb..##T_SELECT_INVOICE_" & Me.ComputerName & " ; END "
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
            End If
        End Sub
        Public Overloads Sub Dispose(ByVal disposing As Boolean)

            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace

