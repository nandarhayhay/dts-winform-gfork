Imports System.Data.SqlClient
Namespace ManageSendData
    Public Class Sendata : Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Dim Query As String = ""
        Private m_DataSet As DataSet = Nothing
        Public Property DS() As DataSet
            Get
                If (m_DataSet Is Nothing) Then
                    'table0 pack
                    'table1 brand
                    'table2 brandpack
                    'table3 distributor
                    'table4 distributor agreement
                    'table5 agreement
                    'table6 agrement brand
                    'table7 agreement brandpack
                    'table8 territory
                    'table9 TM
                    'table10 REGIONAL
                    'table11 viewagreement
                    'Table12 PrivieledgeRSM
                    'table13 priviledgeTM
                    Try
                        Dim Query As String = ""

                        Me.CreateCommandSql("sp_executesql", "")
                        Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.OpenConnection()
                        Query = "SET NOCOUNT ON;IF EXISTS(SELECT MANAGER FROM DIST_REGIONAL DR WHERE NOT EXISTS(" & vbCrLf _
                              & "SELECT [UserName] FROM UserPriviledgeDPRD UP WHERE UP.TypeApp = 'RSM'" & vbCrLf _
                              & " AND UP.RegionalCode = DR.REGIONAL_ID AND UP.[Username] = DR.MANAGER ))" & vbCrLf _
                              & " BEGIN " & vbCrLf _
                              & " SELECT REGIONAL_ID AS RegionalCode,MANAGER AS RegionalManager,HP,CREATE_BY AS CreatedBy,CREATE_DATE AS CreatedDate" & vbCrLf _
                              & " FROM DIST_REGIONAL DR WHERE NOT EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE " & vbCrLf _
                              & " RegionalCode = DR.REGIONAL_ID AND TypeApp ='RSM' AND [UserName] = DR.MANAGER)OPTION(KEEP PLAN);" & vbCrLf _
                              & " END"
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Dim DtRegManager As New DataTable("T_RegManager") : Me.SqlDat.Fill(DtRegManager)
                        Me.ClearCommandParameters()
                        If (DtRegManager.Rows.Count > 0) Then
                            Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                            Me.SqlCom.CommandType = CommandType.Text
                            For I As Integer = 0 To DtRegManager.Rows.Count - 1
                                Query = "SET NOCOUNT ON;DECLARE @V_IDApp INT,@V_Password VARCHAR(100);" & vbCrLf _
                                      & "SET @V_IDApp = ((SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('UserPriviledgeDPRD')  AND (index_id=0 or index_id=1) ) + 1 );" & vbCrLf _
                                      & "SET @V_Password = 'NFD01_RSM_' + CAST(@V_IDApp AS VARCHAR(100));" & vbCrLf _
                                      & " IF NOT EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = @UserName AND TypeApp = 'RSM' AND RegionalCode = @RegionalCode)" & vbCrLf _
                                      & " BEGIN " & vbCrLf _
                                      & " INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,RegionalCode,[Password],CreatedBy,CreatedDate)" & vbCrLf _
                                      & " VALUES(@UserName,'RSM',@RegionalCode,@V_Password,@CreatedBy,GETDATE());" & vbCrLf _
                                      & " END "
                                Me.AddParameter("@UserName", SqlDbType.VarChar, DtRegManager.Rows(I)("RegionalManager"))
                                Me.AddParameter("@CreatedBy", SqlDbType.VarChar, DtRegManager.Rows(I)("CreatedBy"))
                                Me.AddParameter("@RegionalCode", SqlDbType.VarChar, DtRegManager.Rows(I)("RegionalCode"))
                                Me.SqlCom.CommandText = Query : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Next
                        End If
                        ''sekarang TerritoryManager
                        Me.SqlCom.CommandType = CommandType.StoredProcedure : Me.SqlCom.CommandText = "sp_executesql"
                        Query = "SET NOCOUNT ON;IF EXISTS(SELECT MANAGER FROM" & vbCrLf _
                              & "(SELECT ST.TERRITORY_ID AS TerritoryCode,ST.INACTIVE,TM.MANAGER FROM SHIP_TO ST INNER JOIN TERRITORY_MANAGER TM" & vbCrLf _
                              & " ON TM.TM_ID = ST.TM_ID)TM WHERE NOT EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD UP WHERE UP.[UserName] = TM.MANAGER" & vbCrLf _
                              & " AND UP.TerritoryCode = TM.TerritoryCode AND UP.TypeApp = 'TM') AND TM.INACTIVE = 0)" & vbCrLf _
                              & " BEGIN" & vbCrLf _
                              & "SELECT * FROM (SELECT TER.REGIONAL_ID AS RegionalCode,ST.SHIP_TO_ID AS TMCode,ST.TERRITORY_ID AS TerritoryCode,ST.INACTIVE,TM.MANAGER AS TerritoryManager,TM.HP," & vbCrLf _
                              & "ST.CREATE_BY AS CreatedBy,ST.CREATE_DATE AS CreatedDate  FROM SHIP_TO ST INNER JOIN TERRITORY_MANAGER TM" & vbCrLf _
                              & " ON TM.TM_ID = ST.TM_ID INNER JOIN TERRITORY TER ON TER.TERRITORY_ID = ST.TERRITORY_ID)TM " & vbCrLf _
                              & " WHERE NOT EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD UP WHERE UP.[UserName] = TM.TerritoryManager" & vbCrLf _
                              & " AND UP.TerritoryCode = TM.TerritoryCode AND UP.TypeApp = 'TM') AND TM.INACTIVE = 0 OPTION(KEEP PLAN);" & vbCrLf _
                              & " END "
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Dim DtTerManager As New DataTable("T_TM") : Me.SqlDat.Fill(DtTerManager)
                        If (DtTerManager.Rows.Count > 0) Then
                            If IsNothing(Me.SqlTrans) Then
                                Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                            End If
                            Me.SqlCom.CommandType = CommandType.Text
                            For i As Integer = 0 To DtTerManager.Rows.Count - 1
                                Query = "SET NOCOUNT ON;DECLARE @V_IDApp INT,@V_Password VARCHAR(100)" & vbCrLf _
                                      & "SET @V_IDApp = ((SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('UserPriviledgeDPRD')  AND (index_id=0 or index_id=1)) + 1 );" & vbCrLf _
                                      & "SET @V_Password = 'NFD01_TM_' + CAST(@V_IDApp AS VARCHAR(100));" & vbCrLf _
                                      & " IF NOT EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = @UserName AND TypeApp = 'TM' AND TerritoryCode = @TerritoryCode)" & vbCrLf _
                                      & " BEGIN " & vbCrLf _
                                      & " INSERT INTO UserPriviledgeDPRD([UserName],TypeApp,TMCode,TerritoryCode,RegionalCode," & vbCrLf _
                                      & "[Password],CreatedBy,CreatedDate)" & vbCrLf _
                                      & " VALUES(@UserName,'TM',@TMCode,@TerritoryCode,@RegionalCode,@V_Password,@CreatedBy,GETDATE());" & vbCrLf _
                                      & " END "
                                Me.AddParameter("@UserName", SqlDbType.VarChar, DtTerManager.Rows(i)("TerritoryManager"))
                                Me.AddParameter("@TerritoryCode", SqlDbType.VarChar, DtTerManager.Rows(i)("TerritoryCode"))
                                Me.AddParameter("@RegionalCode", SqlDbType.VarChar, DtTerManager.Rows(i)("RegionalCode"))
                                Me.AddParameter("@TMCode", SqlDbType.VarChar, DtTerManager.Rows(i)("TMCode"), 14)
                                Me.AddParameter("@CreatedBy", SqlDbType.VarChar, DtTerManager.Rows(i)("CreatedBy"))
                                Me.SqlCom.CommandText = Query : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Next
                        End If
                        ''SET PASSWORD UNTUK MASING MASING USER SATU BIARPUN ROWNYA BANYAK'
                        Query = "SET NOCOUNT ON;IF EXISTS(SELECT UserName,Count(UserName) AS JUMLAH,TypeApp FROM UserPriviledgeDPRD " & vbCrLf & _
                                "GROUP BY UserName,TypeApp HAVING Count(UserName) > 1 AND COUNT(TypeApp) > 1)" & vbCrLf & _
                                " BEGIN " & vbCrLf & _
                                " SELECT IDApp,[UserName],TypeApp FROM UserPriviledgeDPRD WHERE [UserName] IN(" & vbCrLf & _
                                "SELECT [UserName] FROM(SELECT [UserName],Count([UserName]) AS JUMLAH FROM UserPriviledgeDPRD " & vbCrLf & _
                                "GROUP BY [UserName],TypeApp HAVING Count(UserName) > 1 AND Count(TypeApp) > 1)UP) OPTION(KEEP PLAN);" & vbCrLf & _
                                "END "
                        Me.SqlCom.CommandText = "sp_executesql" : Me.SqlCom.CommandType = CommandType.StoredProcedure
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Dim DtUser As New DataTable("T_User") : Me.SqlDat.Fill(DtUser) : Me.ClearCommandParameters()
                        If (DtUser.Rows.Count > 0) Then
                            Me.SqlCom.CommandType = CommandType.Text
                            For i As Integer = 0 To DtUser.Rows.Count - 1
                                Query = "SET NOCOUNT ON; UPDATE UserPriviledgeDPRD SET [Password] = 'NFD01_' + @TypeApp + '_' + CAST(@IDApp AS VARCHAR(100))" & _
                                " WHERE [UserName] = @UserName AND TypeApp = @TypeApp;"
                                Me.AddParameter("@UserName", SqlDbType.VarChar, DtUser.Rows(i)("UserName"))
                                Me.AddParameter("@TypeApp", SqlDbType.VarChar, DtUser.Rows(i)("TypeApp"))
                                Me.AddParameter("@IDApp", SqlDbType.Int, DtUser.Rows(i)("IDApp"))
                                Me.SqlCom.CommandText = Query : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Next
                        End If
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                " DELETE FROM UserPriviledgeDPRD WHERE TMCode = ANY(SELECT TM_ID FROM SHIP_TO WHERE INACTIVE = 1) AND TypeApp IN('FS','TM') ;" & vbCrLf & _
                                " DELETE FROM UserPriviledgeDPRD WHERE RegionalCode = ANY(SELECT REGIONAL_ID FROM DIST_REGIONAL WHERE INACTIVE = 1) AND TypeApp = 'RSM';"
                        Me.SqlCom.CommandText = "sp_executesql" : Me.SqlCom.CommandType = CommandType.StoredProcedure
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                        Me.CommiteTransaction()

                        Query = " SET NOCOUNT ON;" & vbCrLf _
                               & " --(0)============================== PACKAGING =============================================================" & vbCrLf _
                               & "SELECT PACK_ID,PACK_NAME,QUANTITY,DEVIDE_FACTOR,UNIT ,CASE IsActive WHEN 1 THEN 0 ELSE 1 END AS INACTIVE,CREATE_BY,CREATE_DATE FROM BRND_PACK" _
                               & " WHERE EXISTS(SELECT PACK_ID FROM BRND_BRANDPACK WHERE PACK_ID = BRND_PACK.PACK_ID AND IsActive = 1 AND (IsObsolete = 0 OR IsObsolete IS NULL))" _
                               & " AND QUANTITY IS NOT NULL AND DEVIDE_FACTOR IS NOT NULL  AND IsActive = 1 AND (IsObsolete = 0 OR IsObsolete IS NULL) OPTION(KEEP PLAN);" & vbCrLf _
                               & " --(1)============================== BRAND ================================================================= " & vbCrLf _
                               & " SELECT BRAND_ID, BRAND_NAME ,CASE IsActive WHEN 1 THEN 0 ELSE 1 END AS INACTIVE,CREATE_BY,CREATE_DATE FROM BRND_BRAND WHERE EXISTS(SELECT BRAND_ID " _
                               & " FROM BRND_BRANDPACK WHERE BRAND_ID = BRND_BRAND.BRAND_ID AND IsActive = 1 AND (IsObsolete = 0 OR IsObsolete IS NULL)) AND IsActive = 1 AND (IsObsolete = 0 OR IsObsolete IS NULL) OPTION(KEEP PLAN);" & vbCrLf _
                               & " --(2)=============================== BRANDPACK =============================================================" & vbCrLf _
                               & " SELECT BRAND_ID,PACK_ID,BRANDPACK_ID,BRANDPACK_NAME,DEVIDED_QUANTITY,UNIT,CASE IsActive WHEN 1 THEN 0 ELSE 1 END AS INACTIVE,CREATE_BY,CREATE_DATE " _
                               & " FROM BRND_BRANDPACK WHERE DEVIDED_QUANTITY IS NOT NULL  AND IsActive = 1 AND (IsObsolete = 0 OR IsObsolete IS NULL) OPTION(KEEP PLAN);" & vbCrLf _
                               & " --(3)=============================== DISTRIBUTOR ==========================================================" & vbCrLf _
                               & " SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME,PARENT_DISTRIBUTOR_ID,TERRITORY_ID," _
                               & " NPWP,MAX_DISC_PER_PO,ADDRESS,CONTACT,PHONE,FAX,HP,CREATE_BY,CREATE_DATE FROM DIST_DISTRIBUTOR OPTION(KEEP PLAN);" & vbCrLf _
                               & " --(4)=============================== DISTRIBUTOR AGREEMENT ================================================" & vbCrLf _
                               & " SELECT * FROM DISTRIBUTOR_AGREEMENT DA WHERE EXISTS(SELECT AGREEMENT_NO " _
                               & " FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = DA.AGREEMENT_NO AND END_DATE >= @GETDATE) OPTION(KEEP PLAN);" & vbCrLf _
                               & " --(5)================================AGREEMENT_HEADER =====================================================" & vbCrLf _
                               & " SELECT AGREEMENT_NO,AGREEMENT_DESC,START_DATE,END_DATE,QS_TREATMENT_FLAG,CREATE_BY,CREATE_DATE " _
                               & " FROM AGREE_AGREEMENT WHERE END_DATE >= @GETDATE OPTION(KEEP PLAN); " & vbCrLf _
                               & " --(6)=============================== AGREEMENT BRAND =====================================================" & vbCrLf _
                               & " SELECT AGREE_BRAND_ID,AGREEMENT_NO,BRAND_ID,GIVEN_DISC_PCT,TARGET_Q1,TARGET_Q2,TARGET_Q3," _
                               & " TARGET_Q4,TARGET_S1,TARGET_S2,CREATE_BY,CREATE_DATE FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO IN(" _
                               & " SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE END_DATE >= @GETDATE ) OPTION(KEEP PLAN);" _
                               & " --(7)================================= AGREEMENT BRANDPACK ================================================" & vbCrLf _
                               & " SELECT AGREE_BRANDPACK_ID,AGREEMENT_NO,AGREE_BRAND_ID,BRANDPACK_ID,CREATE_BY,CREATE_DATE FROM AGREE_BRANDPACK_INCLUDE " & vbCrLf _
                               & " WHERE AGREEMENT_NO IN(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE END_DATE >= @GETDATE) OPTION(KEEP PLAN);" & vbCrLf _
                               & " --(8)================================= TERITORIAL =========================================================" & vbCrLf _
                               & " SELECT TER.TERRITORY_ID,TER.TERRITORY_AREA,REG.REGIONAL_ID,REG.REGIONAL_AREA,TER.PARENT_TERRITORY AS PARENT_TERRITORY_ID,PARENT_TERRITORY = CASE " & vbCrLf _
                               & " WHEN(TER.PARENT_TERRITORY IS NULL) THEN NULL ELSE (SELECT TERRITORY_AREA FROM TERRITORY WHERE TERRITORY_ID = TER.PARENT_TERRITORY) END, " & vbCrLf _
                               & " TER.CREATE_BY,TER.CREATE_DATE FROM TERRITORY TER INNER JOIN DIST_REGIONAL REG ON REG.REGIONAL_ID = TER.REGIONAL_ID WHERE TER.INACTIVE = 0 OPTION(KEEP PLAN); " & vbCrLf _
                               & " --(9)================================= TERRITORIAL MANAGER ================================================" & vbCrLf _
                               & " SELECT TM_ID,MANAGER,HP,CREATE_BY,CREATE_DATE FROM TERRITORY_MANAGER OPTION(KEEP PLAN);" & vbCrLf _
                               & " --(10)================================= REGIONAL MANAGER ===================================================" & vbCrLf _
                               & " SELECT REG.REGIONAL_ID,REG.REGIONAL_AREA,REG.PARENT_REGIONAL AS PARENT_REGIONAL_ID,PARENT_REGIONAL = CASE " & vbCrLf _
                               & " WHEN(REG.PARENT_REGIONAL IS NULL) THEN NULL ELSE (SELECT REGIONAL_AREA FROM DIST_REGIONAL WHERE REGIONAL_ID = REG.PARENT_REGIONAL) END,REG.MANAGER,REG.HP," & vbCrLf _
                               & " REG.CREATE_BY,REG.CREATE_DATE FROM DIST_REGIONAL REG WHERE REG.INACTIVE = 0 OPTION(KEEP PLAN);" & vbCrLf _
                               & " --(11)================================= AGREEMENT(PKD) TO VIEW ON LISTVIEW =================================" & vbCrLf _
                               & " SELECT DA.DISTRIBUTOR_ID, DA.AGREEMENT_NO, DR.DISTRIBUTOR_NAME, AA.AGREEMENT_DESC, ABI.AGREE_BRAND_ID, " _
                               & " BB.BRANDPACK_ID, BB.BRANDPACK_NAME, AA.START_DATE, AA.END_DATE, AA.QS_TREATMENT_FLAG, TERR.TERRITORY_AREA, REG.REGIONAL_AREA " & vbCrLf _
                               & " FROM AGREE_BRAND_INCLUDE ABI INNER JOIN " & vbCrLf _
                               & " AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO INNER JOIN " & vbCrLf _
                               & " DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO AND DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf _
                               & " INNER JOIN AGREE_BRANDPACK_INCLUDE ABP ON ABP.AGREE_BRAND_ID = ABI.AGREE_BRAND_ID AND ABP.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf _
                               & " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = ABP.BRANDPACK_ID " & vbCrLf _
                               & " INNER JOIN  DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = DA.DISTRIBUTOR_ID " & vbCrLf _
                               & " INNER JOIN TERRITORY TERR ON TERR.TERRITORY_ID = DR.TERRITORY_ID " & vbCrLf _
                               & " INNER JOIN DIST_REGIONAL REG ON REG.REGIONAL_ID = TERR.REGIONAL_ID " & vbCrLf _
                               & " WHERE AA.AGREEMENT_NO = ANY(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE END_DATE >= @GETDATE) OPTION(KEEP PLAN);" & vbCrLf _
                               & " --(12)================================= PRIVILEDGE FOR RSM =================================================" & vbCrLf _
                               & " SELECT UP.IDApp,UP.[UserName],UP.TypeApp AS TypeUser,UP.RegionalCode,DR.REGIONAL_AREA AS RegionalArea," _
                               & " UP.[Password],UP.CreatedBy,UP.CreatedDate FROM " _
                               & " UserPriviledgeDPRD UP INNER JOIN DIST_REGIONAL DR ON DR.REGIONAL_ID = UP.RegionalCode WHERE UP.TypeApp = 'RSM' AND DR.INACTIVE = 0 " & vbCrLf _
                               & " OPTION(KEEP PLAN);" & vbCrLf _
                               & " --(13)================================= PRIVILEDGE FOR TM/JTM ==============================================" & vbCrLf _
                               & " SELECT UP.IDApp,UP.[UserName],UP.TMCode,UP.TypeApp AS TypeUser,UP.TerritoryCode," _
                               & " TER.TERRITORY_AREA AS TerritoryArea,TM.TM_ID,UP.RegionalCode, UP.[Password],UP.CreatedBy,UP.CreatedDate" _
                               & " FROM UserPriviledgeDPRD UP INNER JOIN TERRITORY_MANAGER TM ON TM.MANAGER = UP.[UserName]" _
                               & " INNER JOIN TERRITORY TER ON TER.TERRITORY_ID = UP.TerritoryCode" & vbCrLf _
                               & " WHERE UP.TypeApp = 'TM' AND TM.TM_ID = ANY(SELECT TM_ID FROM SHIP_TO WHERE INACTIVE = 0) OPTION(KEEP PLAN) ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
                        'Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, New DateTime(2019, 7, 31))
                        Me.m_DataSet = New DataSet("DSData")
                        Me.SqlDat.Fill(Me.m_DataSet) : Me.ClearCommandParameters()
                        '============================= DATA column territori dan regional agar bisa memfilter distributor by territory/regional ===============================
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                " SELECT TERR.TERRITORY_ID,TERR.TERRITORY_AREA,REG.REGIONAL_AREA FROM TERRITORY TERR INNER JOIN DIST_REGIONAL REG ON REG.REGIONAL_ID = TERR.REGIONAL_ID  " & vbCrLf & _
                                " WHERE (TERR.PARENT_TERRITORY IS NULL OR TERR.PARENT_TERRITORY = '')AND TERR.INACTIVE = 0 " & vbCrLf & _
                                " UNION ALL " & vbCrLf & _
                                " SELECT TERR.TERRITORY_ID,TERR.TERRITORY_AREA,REG.REGIONAL_AREA FROM TERRITORY TERR INNER JOIN DIST_REGIONAL REG ON REG.REGIONAL_ID = TERR.REGIONAL_ID " & vbCrLf & _
                                " WHERE (TERR.PARENT_TERRITORY IS NOT NULL AND TERR.PARENT_TERRITORY != '') AND TERR.INACTIVE = 0 " & vbCrLf & _
                                " UNION ALL " & vbCrLf & _
                                " SELECT TERR.TERRITORY_ID,TERR.TERRITORY_AREA,REG.REGIONAL_AREA FROM TERRITORY TERR INNER JOIN DIST_REGIONAL REG ON REG.REGIONAL_ID = TERR.REGIONAL_ID  " & vbCrLf & _
                                " WHERE (TERR.PARENT_TERRITORY IS  NULL AND TERR.PARENT_TERRITORY = '') AND TERR.INACTIVE = 0  " & vbCrLf & _
                                " AND (REG.PARENT_REGIONAL IS NULL OR REG.PARENT_REGIONAL = '') AND REG.INACTIVE = 0 " & vbCrLf & _
                                "  UNION ALL " & vbCrLf & _
                                " SELECT TERR.TERRITORY_ID,TERR.TERRITORY_AREA,REG.REGIONAL_AREA FROM TERRITORY TERR INNER JOIN DIST_REGIONAL REG ON REG.REGIONAL_ID = TERR.REGIONAL_ID  " & vbCrLf & _
                                " WHERE (TERR.PARENT_TERRITORY IS NOT NULL AND TERR.PARENT_TERRITORY != '') AND TERR.INACTIVE = 0 " & vbCrLf & _
                                " AND (REG.PARENT_REGIONAL IS NOT NULL OR REG.PARENT_REGIONAL != '') AND REG.INACTIVE = 0 "
                        Dim T_Territory As New DataTable("T_TerritoryRegional") : T_Territory.Clear()
                        Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Me.SqlDat.Fill(T_Territory) : Me.ClearCommandParameters()
                        'join table distributor dengan territory
                        m_DataSet.AcceptChanges()
                        Dim colTerritoryArea As New DataColumn("TERRITORY_AREA", Type.GetType("System.String"))
                        Dim colRegionalArea As New DataColumn("REGIONAL_AREA", Type.GetType("System.String"))

                        ''table distributor
                        m_DataSet.Tables("table3").Columns.Add(colTerritoryArea)
                        m_DataSet.Tables("table3").Columns.Add(colRegionalArea)
                        m_DataSet.AcceptChanges()
                        If T_Territory.Rows.Count > 0 Then
                            Dim SelectedRows() As DataRow = Nothing
                            Dim editRow As DataRow = Nothing
                            For i As Integer = 0 To T_Territory.Rows.Count - 1
                                SelectedRows = m_DataSet.Tables("table3").Select("TERRITORY_ID = '" & T_Territory.Rows(i)("TERRITORY_ID").ToString() & "'")
                                If SelectedRows.Length > 0 Then
                                    For i1 As Integer = 0 To SelectedRows.Length - 1
                                        SelectedRows(i1).BeginEdit()
                                        SelectedRows(i1)("TERRITORY_AREA") = T_Territory.Rows(i)("TERRITORY_AREA")
                                        SelectedRows(i1)("REGIONAL_AREA") = T_Territory.Rows(i)("REGIONAL_AREA")
                                        SelectedRows(i1).EndEdit()
                                    Next
                                    m_DataSet.Tables("table3").AcceptChanges()
                                End If
                            Next
                        End If
                        m_DataSet.AcceptChanges()
                        Me.CloseConnection()
                    Catch ex As Exception
                        Me.RollbackTransaction() : Me.CloseConnection() : Throw ex
                    End Try
                End If
                Return m_DataSet
            End Get
            Set(ByVal value As DataSet)
                m_DataSet = value
            End Set
        End Property
        Public Function GetTerritory(ByVal ListDistributor As List(Of String)) As DataTable
            If (IsNothing(ListDistributor)) Then
                Throw New Exception("Please check distributor")
            ElseIf ListDistributor.Count <= 0 Then
                Throw New Exception("Please check distributor")
            End If
            Dim strListDistributors As String = "IN('"
            For i As Integer = 0 To ListDistributor.Count - 1
                strListDistributors &= ListDistributor(i) & "'"
                If (i < ListDistributor.Count - 1) Then
                    strListDistributors &= ",'"
                End If
            Next
            strListDistributors &= ")"
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT * FROM TERRITORY TER WHERE EXISTS(SELECT TERRITORY_ID FROM DIST_DISTRIBUTOR WHERE TERRITORY_ID = TER.TERRITORY_ID AND DISTRIBUTOR_ID " & strListDistributors & ") ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)

                Dim dtTerritory As New DataTable("Territory") : dtTerritory.Clear()
                Me.setDataAdapter(Me.SqlCom).Fill(dtTerritory)
                Return dtTerritory
                Me.ClearCommandParameters()
            Catch ex As Exception
                If Not IsNothing(Me.SqlCom) Then
                    Me.ClearCommandParameters()
                End If
                Me.CloseConnection()
                Throw ex
            End Try

        End Function
        Public Function getDSFS() As DataTable
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT * FROM FS F WHERE NOT EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = F.FS_NAME " & vbCrLf & _
                        " AND TerritoryCode = F.CUR_TERRITORY_ID AND TypeApp = 'FS') AND F.INACTIVE = 0) " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "SELECT * FROM (SELECT TER.REGIONAL_ID AS RegionalCode,F.CUR_TERRITORY_ID AS TerritoryCode,F.FS_ID,F.FS_NAME ,F.HP," & vbCrLf _
                        & " F.CREATE_BY AS CreatedBy,F.CREATE_DATE AS CreatedDate  FROM FS F" & vbCrLf _
                        & " INNER JOIN TERRITORY TER ON TER.TERRITORY_ID = F.CUR_TERRITORY_ID WHERE NOT EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = F.FS_NAME " & vbCrLf & _
                        " AND TerritoryCode = F.CUR_TERRITORY_ID AND TypeApp = 'FS') AND F.INACTIVE = 0)FS ;" & vbCrLf & _
                        " END "
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("stmt", SqlDbType.NVarChar, Query)
                Dim tblResult As New DataTable("FIELD_SUPERVISOR") : tblResult.Clear()
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(tblResult) : Me.ClearCommandParameters()
                Dim recCount As Integer = tblResult.Rows.Count
                If recCount > 0 Then
                    Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                    Query = "SET NOCOUNT ON;DECLARE @V_IDApp INT,@V_Password VARCHAR(100);" & vbCrLf & _
                             "SET @V_IDApp = (SELECT (MAX(IDApp) + 1) FROM UserPriviledgeDPRD);" & vbCrLf & _
                             " SET @V_Password = 'NFD01_FS_' + CAST(@V_IDApp AS VARCHAR(100));" & vbCrLf & _
                             " INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,Password,CreatedBy,CreatedDate) " & vbCrLf & _
                             " VALUES(@UserName,'FS',@FS_ID,@TerritoryCode,@RegionalCode,@V_Password,@CreatedBy,@CreatedDate) ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    For i As Integer = 0 To recCount - 1
                        Me.AddParameter("@UserName", SqlDbType.VarChar, tblResult.Rows(i)("FS_NAME"), 50)
                        Me.AddParameter("@FS_ID", SqlDbType.VarChar, tblResult.Rows(i)("FS_ID"), 16)
                        Me.AddParameter("@TerritoryCode", SqlDbType.VarChar, tblResult.Rows(i)("TerritoryCode"), 16)
                        Me.AddParameter("@RegionalCode", SqlDbType.VarChar, tblResult.Rows(0)("RegionalCode"), 10)
                        Me.AddParameter("@CreatedBy", SqlDbType.VarChar, tblResult.Rows(i)("CreatedBy"), 100)
                        Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, Convert.ToDateTime(tblResult.Rows(i)("CreatedDate")))
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next
                End If
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                       "SELECT IDApp FROM UserPriviledgeDprd UP WHERE TypeApp = 'FS' AND EXISTS(SELECT FS_ID FROM FS WHERE FS_ID = UP.TMCode AND INACTIVE = 1)  ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblFS As New DataTable("T_FS") : tblFS.Clear()
                Me.SqlDat.Fill(tblFS)
                If tblFS.Rows.Count > 0 Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    "DELETE FROM UserPriviledgeDPRD WHERE IDApp = @IDApp ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    For i As Integer = 0 To tblFS.Rows.Count - 1
                        Me.AddParameter("@IDApp", SqlDbType.Int, Convert.ToInt32(tblFS.Rows(i)("IDApp")))
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next
                End If
                Me.CommiteTransaction()
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT UP.IDApp,UP.[UserName],UP.TMCode AS FS_ID,UP.TypeApp AS TypeUser,UP.TerritoryCode," & vbCrLf & _
                        " TER.TERRITORY_AREA AS TerritoryArea,UP.RegionalCode, UP.[Password],UP.CreatedBy,UP.CreatedDate" & vbCrLf & _
                        " FROM UserPriviledgeDPRD UP " & vbCrLf & _
                        " INNER JOIN TERRITORY TER ON TER.TERRITORY_ID = UP.TerritoryCode WHERE UP.TypeApp = 'FS' ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                tblResult = New DataTable("FIELD_SUPERVISOR") : tblResult.Clear()
                SqlDat.Fill(tblResult) : Me.ClearCommandParameters()
                Me.CloseConnection()
                Return tblResult
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub deleteUnNeccesssaryUser()
            'Try
            '    Query = "SET NOCOUNT ON ;" & vbCrLf & _
            '    "DELETE FROM UserPriviledgeDprd WHERE TypeApp = 'TM' AND TMCode NOT IN(SELECT SHIP_TO_ID FROM SHIP_TO  " & vbCrLf & _
            '    "WHERE INACTIVE = 0 AND INACTIVE IS NOT NULL);" & vbCrLf & _
            '    "DELETE FROM UserPriviledgeDPRD WHERE TypeApp = 'FS' AND TMCode NOT IN(SELECT FS_ID FROM FS WHERE INACTIVE = 0 AND INACTIVE IS NOT NULL);" & vbCrLf & _
            '    "DELETE FROM UserPriviledgeDPRD WHERE TypeApp = 'RSM' AND UserName NOT IN(SELECT MANAGER FROM DIST_REGIONAL WHERE INACTIVE = 0 AND INACTIVE IS NOT NULL);"

            '    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql(CommandType.StoredProcedure, "sp_executesql", ConnectionTo.Nufarm)
            '    Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            '    End If
            '    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            '    Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            'Catch ex As Exception
            '    Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            'End Try
        End Sub
        Public Sub GetPriviledPM(ByVal DVToProceed As DataView)
            Try

                Me.GetConnection() : Me.SqlCom = New SqlCommand() : Me.SqlCom.CommandType = CommandType.Text
                Me.SqlCom.Connection = Me.SqlConn : Me.OpenConnection()
                If (Not IsNothing(DVToProceed)) Then
                    Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                    For i As Integer = 0 To DVToProceed.Count - 1
                        Query = "SET NOCOUNT ON;DECLARE @V_IDApp INT,@V_Password VARCHAR(100)" & vbCrLf & _
                                " SET @V_IDApp = (SELECT MAX(IDApp) FROM UserPriviledgeDPRD);" & vbCrLf & _
                                " SET @V_Password = 'NFD01_PM_' + CAST(@V_IDApp AS VARCHAR(100));" & vbCrLf & _
                                "IF NOT EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = @UserName " & vbCrLf & _
                                " AND TypeApp = 'PM')" & vbCrLf & _
                                " BEGIN " & vbCrLf & _
                                " INSERT INTO UserPriviledgeDPRD([UserName],TypeApp,[Password],CreatedBy,CreatedDate)" & vbCrLf & _
                                "VALUES(@UserName,'PM',@V_Password,@CreatedBy,GETDATE())" & vbCrLf & _
                                "END "
                        Me.AddParameter("@UserName", SqlDbType.VarChar, DVToProceed(i)("UserName"))
                        Me.AddParameter("@CreatedBy", SqlDbType.VarChar, DVToProceed(i)("CreatedBy"))
                        Me.SqlCom.CommandText = Query : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next
                    Me.CommiteTransaction()
                End If
                Me.SqlCom.CommandText = "sp_executesql" : Me.SqlCom.CommandType = CommandType.StoredProcedure
                Query = "SET NOCOUNT ON;SELECT IDApp,[UserName],TypeApp,[Password],CreatedBy,CreatedDate FROM " & _
                          " UserPriviledgeDPRD WHERE TypeApp = 'PM'"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim Dt As New DataTable("PriviledgePM") : Me.FillDataTable(Dt)
                If Not IsNothing(Me.DS) Then
                    If (DS.Tables.Contains("PriviledgePM")) Then
                        DS.Tables.Remove("PriviledgePM") : DS.AcceptChanges()
                    End If
                    DS.Tables.Add(Dt) : DS.AcceptChanges()
                End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Throw ex
            End Try
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            If (Not IsNothing(m_DataSet)) Then
                Me.m_DataSet.Dispose() : Me.m_DataSet = Nothing
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace

