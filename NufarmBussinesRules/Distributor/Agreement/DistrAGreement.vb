Imports System.Data.SqlClient
Namespace DistributorAgreement
    Public Class DistrAGreement
#Region " Deklarasi "
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private m_ViewDistributor As DataView
        Private m_AgDistributorView As DataView
        Private m_dataTableQuarterly As DataTable
        Private m_dataTableYearly As DataTable
        Private m_dataTableSemeterly As DataTable

        Private m_dataTableQuarterlyv As DataTable
        Private m_dataTableYearlyv As DataTable
        Private m_dataTableSemeterlyv As DataTable

        Private m_dsPeriod As DataSet
        Public DISTRIBUTOR_ID As String
        Public AGREEMENT_NO As String
        'Public AGREEMENT_DESC As String
        Public QS_Treatment_Flag As String
        Public StartDate As Object
        Public EndDate As Object
        'Public TargetYear As Int64
        Public AgreementDescription As Object
        Public dsProghasChanged As Boolean 'unutk mengecek dataset period apakah sudah changed / belum bila sudah set property jadi true
        Public O_AGREEMENT_NO As String
        Dim Query As String = ""
#End Region

#Region " Property "
        Public ReadOnly Property GetDataSetPeriod() As DataSet
            Get
                Return Me.m_dsPeriod
            End Get
        End Property
        Public ReadOnly Property ViewDistributor() As DataView
            Get
                Return Me.m_ViewDistributor
            End Get
        End Property
        Public ReadOnly Property ViewDistAgreement() As DataView
            Get
                Return Me.m_AgDistributorView
            End Get
        End Property
        Public ReadOnly Property GetTableSemesterly() As DataTable
            Get
                Return Me.m_dataTableSemeterly
            End Get
        End Property
        Public ReadOnly Property GetTableQuarterly() As DataTable
            Get

                Return Me.m_dataTableQuarterly
            End Get
        End Property
        Public ReadOnly Property GetTableYearly() As DataTable
            Get
                'Me.m_dataTableYearly.Clear()
                Return Me.m_dataTableYearly
            End Get
        End Property
        Public ReadOnly Property getTableYearlyV() As DataTable
            Get
                Return Me.m_dataTableYearlyv
            End Get
        End Property
        Public ReadOnly Property getTableSemesterlyV() As DataTable
            Get
                Return Me.m_dataTableSemeterlyv
            End Get
        End Property
        Public ReadOnly Property getTableQuarterlyV() As DataTable
            Get
                Return Me.m_dataTableQuarterlyv
            End Get
        End Property
#End Region

#Region " Sub "
        'FUNTION UNTUK MENGECEK ADA / TIDAK ADANYA DATA AGREEMENT_NO DI DATABASE
        Public Function IsExistAgreementNo(ByVal AGREEMENT_NO As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_AGREEMENT_NO", "")
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                If CInt(Me.ExecuteScalar()) > 0 Then
                    Return True
                End If
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function
        Public Sub FetchDsSetPeriod(ByVal Agreement_no As String, ByVal QS_Flag As String, ByVal mustCloseConnection As Boolean)
            Try
                Me.m_dsPeriod = New DataSet("DsPeriod")
                Me.m_dsPeriod.Clear()
                Dim Query1 As String = "SET NOCOUNT ON;" & vbCrLf & _
                                      "SELECT UNIQUE_ID,AGREEMENT_NO,UP_TO_PCT, PRGSV_DISC_PCT , QSY_DISC_FLAG " & vbCrLf & _
                                      "FROM AGREE_PROGRESSIVE_DISCOUNT WHERE (QSY_DISC_FLAG = @QS_FLAG) AND (AGREEMENT_NO = @AGREEMENT_NO) ORDER BY UP_TO_PCT ASC;"
                'Me.m_dataTableQuarterly = MyBase.baseDataTable

                Dim Query2 As String = "SET NOCOUNT ON;" & vbCrLf & _
                                       " SELECT IDApp,AGREEMENT_NO,UP_TO_PCT, PRGSV_DISC_PCT, QSY_DISC_FLAG " & vbCrLf & _
                                       " FROM AGREE_PROGRESSIVE_DISC_VAL WHERE (QSY_DISC_FLAG LIKE @QS_FLAG+'%') AND (AGREEMENT_NO = @AGREEMENT_NO) ORDER BY UP_TO_PCT ASC;"

                Me.AddParameter("@QS_FLAG", SqlDbType.Char, QS_Flag, 1)
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Agreement_no, 32)
                Me.OpenConnection()
                Select Case QS_Flag
                    Case "Q"
                        'Me.CreateCommandSql("Sp_Select_QS_Flag", "")
                        If Me.SqlCom Is Nothing Then : Me.CreateCommandSql("", Query1)
                        Else : Me.ResetCommandText(CommandType.Text, Query1)
                        End If
                        Me.m_dataTableQuarterly = New DataTable("T_Q_Flag")
                        Me.m_dataTableQuarterly.Clear()
                        Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                        Me.SqlDat.Fill(Me.m_dataTableQuarterly)

                        Me.ResetCommandText(CommandType.Text, Query2)
                        Me.m_dataTableQuarterlyv = New DataTable("T_Q_Val_Flag")
                        Me.m_dataTableQuarterlyv.Clear()
                        Me.SqlDat.Fill(Me.m_dataTableQuarterlyv)

                        Me.m_dsPeriod.Tables.Add(Me.m_dataTableQuarterly)
                        Me.m_dsPeriod.Tables.Add(Me.m_dataTableQuarterlyv)
                    Case "S"
                        'Me.SqlCom.CommandText = 
                        If Me.SqlCom Is Nothing Then : Me.CreateCommandSql("", Query1)
                        Else : Me.ResetCommandText(CommandType.Text, Query1)
                        End If
                        Me.m_dataTableSemeterly = New DataTable("T_S_Flag")
                        Me.m_dataTableSemeterly.Clear()
                        Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                        Me.SqlDat.Fill(Me.m_dataTableSemeterly)

                        Me.ResetCommandText(CommandType.Text, Query2)
                        Me.m_dataTableSemeterlyv = New DataTable("T_S_Val_Flag")
                        Me.m_dataTableSemeterlyv.Clear()
                        Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                        Me.SqlDat.Fill(Me.m_dataTableSemeterlyv)

                        Me.m_dsPeriod.Tables.Add(Me.m_dataTableSemeterly)
                        Me.m_dsPeriod.Tables.Add(Me.m_dataTableSemeterlyv)
                End Select
                'Me.SqlCom.CommandText = "Sp_Select_Y_Flag"
                'Me.SqlCom.CommandType = CommandType.StoredProcedure
                'Me.CreateCommandSql("Sp_Select_Y_Flag", "")
                Me.ResetCommandText(CommandType.Text, Query1)
                Me.SqlCom.Parameters()("@QS_FLAG").Value = "Y"
                Me.m_dataTableYearly = New DataTable("T_Y_Flag")
                Me.m_dataTableYearly.Clear()
                Me.SqlDat.Fill(Me.m_dataTableYearly)

                Me.ResetCommandText(CommandType.Text, Query2)
                Me.m_dataTableYearlyv = New DataTable("T_Y_Val_Flag")
                Me.m_dataTableYearlyv.Clear()
                Me.SqlDat.Fill(Me.m_dataTableYearlyv)

                Me.m_dsPeriod.Tables.Add(Me.m_dataTableYearly)
                Me.m_dsPeriod.Tables.Add(Me.m_dataTableYearlyv)
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            'Return Me.m_dsPeriod
        End Sub
        Private Sub SavaDataSet(ByVal Ds As DataSet)
            Dim CommandInsert As SqlCommand = Me.SqlConn.CreateCommand()
            Dim CommandInsertV As SqlCommand = Me.SqlConn.CreateCommand()

            Dim CommandUpdate As SqlCommand = Me.SqlConn.CreateCommand()
            Dim CommandUpdateV As SqlCommand = Me.SqlConn.CreateCommand()

            Dim CommandDelete As SqlCommand = Me.SqlConn.CreateCommand()
            Dim CommandDeleteV As SqlCommand = Me.SqlConn.CreateCommand()

            If Not IsNothing(Ds) Then
                With CommandInsert
                    .CommandType = CommandType.Text
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " IF EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                            "  BEGIN " & vbCrLf & _
                            "       DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER " & vbCrLf & _
                            "       WHERE AGREEMENT_NO = @AGREEMENT_NO AND FLAG LIKE @QSY_DISC_FLAG  + '%') AND LEFT_QTY = DISC_QTY  ; " & vbCrLf & _
                            "       DELETE FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER  ACH " & vbCrLf & _
                            "       WHERE NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ACH.ACHIEVEMENT_ID)) AND FLAG LIKE @QSY_DISC_FLAG  + '%' ; " & vbCrLf & _
                            "  END  " & vbCrLf & _
                            " INSERT INTO AGREE_PROGRESSIVE_DISCOUNT(AGREEMENT_NO,PRGSV_DISC_PCT,QSY_DISC_FLAG,UP_TO_PCT,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                            " VALUES(@AGREEMENT_NO,@PRGSV_DISC_PCT,@QSY_DISC_FLAG,@UP_TO_PCT,@CREATE_BY,@CREATE_DATE); "
                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                End With
                With CommandInsertV
                    .CommandType = CommandType.Text
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " INSERT INTO AGREE_PROGRESSIVE_DISC_VAL (AGREEMENT_NO, UP_TO_PCT, PRGSV_DISC_PCT, QSY_DISC_FLAG, CREATE_BY, CREATE_DATE)" & vbCrLf & _
                            " VALUES(@AGREEMENT_NO, @UP_TO_PCT, @PRGSV_DISC_PCT, @QSY_DISC_FLAG, @CREATE_BY, @CREATE_DATE); " & vbCrLf & _
                            " IF EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND AGREE_ACH_BY = 'VAL' AND FLAG LIKE @QSY_DISC_FLAG  + '%') " & vbCrLf & _
                            " BEGIN UPDATE ACCRUED_HEADER SET TARGET_BY_VALUE = 0,ACTUAL_BY_VALUE = 0,DISPRO_BY_VALUE = 0,ACH_DISPRO_BY_VALUE = 0,DISC_BY_VALUE = 0,AGREE_ACH_BY = 'VOL',MODIFY_BY = 'SYSTEM',MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                            " WHERE AGREEMENT_NO = @AGREEMENT_NO AND AGREE_ACH_BY = 'VAL' AND FLAG LIKE @QSY_DISC_FLAG  + '%' ; END "
                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                End With

                With CommandUpdate
                    .CommandType = CommandType.Text
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ACCRUED_HEADER' AND TABLE_TYPE = 'base table') " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "  IF EXISTS(SELECT AC.ACHIEVEMENT_ID FROM ACCRUED_HEADER AC WHERE AC.AGREEMENT_NO  = @AGREEMENT_NO AND " & vbCrLf & _
                            "             EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = AC.ACHIEVEMENT_ID AND CAN_RELEASE = 1 AND LEFT_QTY < DISC_QTY)) " & vbCrLf & _
                            "   BEGIN " & vbCrLf & _
                            "        RAISERROR('BrandPack already used in OA',16,1);" & vbCrLf & _
                            "        RETURN;" & vbCrLf & _
                            "   END " & vbCrLf & _
                            "  ELSE IF EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                            "  BEGIN " & vbCrLf & _
                            "       DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER " & vbCrLf & _
                            "       WHERE AGREEMENT_NO = @AGREEMENT_NO AND FLAG LIKE @QSY_DISC_FLAG  + '%') AND LEFT_QTY = DISC_QTY; " & vbCrLf & _
                            "       DELETE FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER  ACH " & vbCrLf & _
                            "       WHERE NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ACH.ACHIEVEMENT_ID)) AND FLAG LIKE @QSY_DISC_FLAG  + '%' ; " & vbCrLf & _
                            "  END  " & vbCrLf & _
                            " END " & vbCrLf & _
                           " IF EXISTS(SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING BBS INNER JOIN AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                            "    ON ABI.AGREE_BRANDPACK_ID = BBS.AGREE_BRANDPACK_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND BBS.LEFT_QTY < BBS.DISC_QTY) " & vbCrLf & _
                            "  BEGIN " & vbCrLf & _
                            "        RAISERROR('BrandPack already used in OA',16,1);" & vbCrLf & _
                            "        RETURN;" & vbCrLf & _
                            "  END " & vbCrLf & _
                            " UPDATE AGREE_PROGRESSIVE_DISCOUNT SET AGREEMENT_NO = @AGREEMENT_NO,QSY_DISC_FLAG = @QSY_DISC_FLAG," & _
                            "PRGSV_DISC_PCT = @PRGSV_DISC_PCT,UP_TO_PCT = @UP_TO_PCT,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " & vbCrLf & _
                            " WHERE UNIQUE_ID = CAST(@UNIQUE_ID AS VARCHAR(100));"
                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                End With

                With CommandUpdateV
                    .CommandType = CommandType.Text
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " UPDATE AGREE_PROGRESSIVE_DISC_VAL SET UP_TO_PCT = @UP_TO_PCT, PRGSV_DISC_PCT = @PRGSV_DISC_PCT, QSY_DISC_FLAG = @QSY_DISC_FLAG, " & vbCrLf & _
                            " MODIFY_BY = @MODIFY_BY, MODIFY_DATE = CONVERT(VARCHAR(100),GETDATE(),101) WHERE IDApp = @IDApp ;"
                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                End With
                With CommandDelete
                    .CommandType = CommandType.Text
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ACCRUED_HEADER' AND TABLE_TYPE = 'base table') " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "  IF EXISTS(SELECT AC.ACHIEVEMENT_ID FROM ACCRUED_HEADER AC WHERE AC.AGREEMENT_NO = @AGREEMENT_NO AND " & vbCrLf & _
                            "             EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = AC.ACHIEVEMENT_ID AND CAN_RELEASE = 1 AND LEFT_QTY < DISC_QTY)) " & vbCrLf & _
                            "   BEGIN " & vbCrLf & _
                            "        RAISERROR('BrandPack already used in OA',16,1); " & vbCrLf & _
                            "        RETURN;" & vbCrLf & _
                            "   END " & vbCrLf & _
                            " DELETE FROM AGREE_PROGRESSIVE_DISCOUNT WHERE UNIQUE_ID = CAST(@UNIQUE_ID AS VARCHAR(100)); " & vbCrLf & _
                            " IF NOT EXISTS(SELECT AGREEMENT_NO FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                            "        AND NOT EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_PROG_DISC APD " & vbCrLf & _
                            "                        WHERE EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = APD.AGREE_BRAND_ID " & vbCrLf & _
                            "                        AND AGREEMENT_NO = @AGREEMENT_NO)) " & vbCrLf & _
                            "   BEGIN " & vbCrLf & _
                            "    DELETE FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER " & vbCrLf & _
                            "    WHERE AGREEMENT_NO = @AGREEMENT_NO AND FLAG LIKE @QSY_DISC_FLAG  + '%') AND LEFT_QTY = DISC_QTY; " & vbCrLf & _
                            "    DELETE FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND ACHIEVEMENT_ID = ANY(SELECT ACHIEVEMENT_ID FROM ACCRUED_HEADER  ACH " & vbCrLf & _
                            "    WHERE NOT EXISTS(SELECT ACHIEVEMENT_ID FROM ACCRUED_DETAIL WHERE ACHIEVEMENT_ID = ACH.ACHIEVEMENT_ID)) AND FLAG LIKE @QSY_DISC_FLAG  + '%'; " & vbCrLf & _
                            "   END " & vbCrLf & _
                            " END " & vbCrLf & _
                            " IF EXISTS(SELECT BRND_B_S_ID FROM BRND_BRANDPACK_SAVING BBS INNER JOIN AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                            "    ON ABI.AGREE_BRANDPACK_ID = BBS.AGREE_BRANDPACK_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND BBS.LEFT_QTY < BBS.DISC_QTY) " & vbCrLf & _
                            "  BEGIN " & vbCrLf & _
                            "        RAISERROR('BrandPack already used in OA',16,1); " & vbCrLf & _
                            "        RETURN; " & vbCrLf & _
                            "  END " & vbCrLf & _
                            " DELETE FROM AGREE_PROGRESSIVE_DISCOUNT WHERE UNIQUE_ID = CAST(@UNIQUE_ID AS VARCHAR(100)); " & vbCrLf & _
                            " IF NOT EXISTS(SELECT AGREEMENT_NO FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                            "        AND NOT EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_PROG_DISC APD " & vbCrLf & _
                            "                        WHERE EXISTS(SELECT AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = APD.AGREE_BRAND_ID " & vbCrLf & _
                            "                        AND AGREEMENT_NO = @AGREEMENT_NO)) " & vbCrLf & _
                            "   BEGIN " & vbCrLf & _
                            "     DELETE FROM BRND_BRANDPACK_SAVING WHERE AGREE_BRANDPACK_ID = ANY( " & vbCrLf & _
                            "     SELECT AGREE_BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE AGREEMENT_NO = @AGREEMENT_NO); " & vbCrLf & _
                            "   END "
                    .CommandType = CommandType.Text
                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                End With

                With CommandDeleteV
                    .CommandType = CommandType.Text
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " DELETE FROM AGREE_PROGRESSIVE_DISC_VAL WHERE IDApp = @IDApp ;"
                    .CommandText = Query
                    .Transaction = Me.SqlTrans
                End With

                ''set data adafter to sqlcommand
                Me.SqlDat.InsertCommand = CommandInsert : Me.SqlDat.UpdateCommand = CommandUpdate
                Me.SqlDat.DeleteCommand = CommandDelete

                With CommandInsert
                    .Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 32).SourceColumn = "AGREEMENT_NO"
                    .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal).SourceColumn = "PRGSV_DISC_PCT"
                    '.Parameters("@PRGSV_DISC_PCT").Scale = 3
                    .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.Char, 1).SourceColumn = "QSY_DISC_FLAG"
                    .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal).SourceColumn = "UP_TO_PCT"
                    '.Parameters("@UP_TO_PCT").Scale = 3
                    .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                    .Parameters.Add("@CREATE_DATE", SqlDbType.DateTime).Value = NufarmBussinesRules.SharedClass.ServerDate()
                End With
                Dim NewRows() As DataRow = Ds.Tables(0).Select("", "", DataViewRowState.Added) 'TABEL T_Q_FLAG/T_S_FLAG
                If (NewRows.Length > 0) Then
                    Me.SqlDat.Update(NewRows)
                End If
                NewRows = Ds.Tables(2).Select("", "", DataViewRowState.Added) ''T_Y_FLAG
                If NewRows.Length > 0 Then
                    Me.SqlDat.Update(NewRows)
                End If

                With CommandUpdate
                    .Parameters.Add("@UNIQUE_ID", SqlDbType.UniqueIdentifier).SourceColumn = "UNIQUE_ID"
                    .Parameters("@UNIQUE_ID").SourceVersion = DataRowVersion.Original
                    .Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 32).SourceColumn = "AGREEMENT_NO"
                    .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal).SourceColumn = "PRGSV_DISC_PCT"
                    '.Parameters("@PRGSV_DISC_PCT").Scale = 3
                    .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.Char, 1).SourceColumn = "QSY_DISC_FLAG"
                    .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal).SourceColumn = "UP_TO_PCT"
                    '.Parameters("@UP_TO_PCT").Scale = 3
                    .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                    .Parameters.Add("@MODIFY_DATE", SqlDbType.DateTime).Value = NufarmBussinesRules.SharedClass.ServerDate()
                End With
                Dim UpdatedRows() As DataRow = Ds.Tables(0).Select("", "", DataViewRowState.ModifiedOriginal) 'TABEL T_Q_FLAG/T_S_FLAG
                If (UpdatedRows.Length > 0) Then
                    SqlDat.Update(UpdatedRows)
                End If
                UpdatedRows = Ds.Tables(2).Select("", "", DataViewRowState.ModifiedOriginal) ''T_Y_FLAG
                If UpdatedRows.Length > 0 Then
                    SqlDat.Update(UpdatedRows)
                End If

                With CommandDelete
                    .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.Char, 1).SourceColumn = "QSY_DISC_FLAG"
                    .Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 32).SourceColumn = "AGREEMENT_NO"
                    .Parameters("@AGREEMENT_NO").SourceVersion = DataRowVersion.Original
                    .Parameters.Add("@UNIQUE_ID", SqlDbType.UniqueIdentifier).SourceColumn = "UNIQUE_ID"
                    .Parameters("@UNIQUE_ID").SourceVersion = DataRowVersion.Original
                End With
                Dim DeletedRows() As DataRow = Ds.Tables(0).Select("", "", DataViewRowState.Deleted) 'TABEL T_Q_FLAG/T_S_FLAG
                If (DeletedRows.Length > 0) Then
                    SqlDat.Update(DeletedRows)
                End If
                DeletedRows = Ds.Tables(2).Select("", "", DataViewRowState.Deleted) ''T_Y_FLAG
                If DeletedRows.Length > 0 Then
                    SqlDat.Update(DeletedRows)
                End If

                ''reset sqldat
                Me.SqlDat.InsertCommand = CommandInsertV : Me.SqlDat.UpdateCommand = CommandUpdateV
                Me.SqlDat.DeleteCommand = CommandDeleteV

                With CommandInsertV
                    .Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 32, "AGREEMENT_NO")
                    .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal, 0, "UP_TO_PCT")
                    .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal, 0, "PRGSV_DISC_PCT")
                    .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                    .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                    .Parameters.Add("CREATE_DATE", SqlDbType.SmallDateTime).Value = NufarmBussinesRules.SharedClass.ServerDate
                End With
                NewRows = Ds.Tables(1).Select("", "", DataViewRowState.Added)
                If NewRows.Length > 0 Then
                    SqlDat.Update(NewRows)
                End If
                NewRows = Ds.Tables(3).Select("", "", DataViewRowState.Added)
                If NewRows.Length > 0 Then
                    SqlDat.Update(NewRows)
                End If
                With CommandUpdateV
                    .Parameters.Add("@IDApp", SqlDbType.Int).SourceColumn = "IDApp"
                    .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                    .Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 32).SourceColumn = "AGREEMENT_NO"
                    .Parameters.Add("@PRGSV_DISC_PCT", SqlDbType.Decimal).SourceColumn = "PRGSV_DISC_PCT"
                    '.Parameters("@PRGSV_DISC_PCT").Scale = 3
                    .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                    .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal).SourceColumn = "UP_TO_PCT"
                    .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                End With
                UpdatedRows = Ds.Tables(1).Select("", "", DataViewRowState.ModifiedOriginal)
                If UpdatedRows.Length > 0 Then
                    SqlDat.Update(UpdatedRows)
                End If
                UpdatedRows = Ds.Tables(3).Select("", "", DataViewRowState.ModifiedOriginal)
                If UpdatedRows.Length > 0 Then
                    SqlDat.Update(UpdatedRows)
                End If

                With CommandDeleteV
                    .Parameters.Add("@AGREEMENT_NO", SqlDbType.VarChar, 32).SourceColumn = "AGREEMENT_NO"
                    .Parameters.Add("@QSY_DISC_FLAG", SqlDbType.VarChar, 2).SourceColumn = "QSY_DISC_FLAG"
                    .Parameters.Add("@IDApp", SqlDbType.Int).SourceColumn = "IDApp"
                    .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                End With
                DeletedRows = Ds.Tables(1).Select("", "", DataViewRowState.Deleted)
                If DeletedRows.Length > 0 Then
                    SqlDat.Update(DeletedRows)
                End If
                DeletedRows = Ds.Tables(3).Select("", "", DataViewRowState.Deleted)
                If DeletedRows.Length > 0 Then
                    SqlDat.Update(DeletedRows)
                End If
            End If
        End Sub
        Public Sub SaveAgreementGroup(ByVal SaveType As String, ByVal DISTRIBUTOR_IDS As Collection, ByVal isAddedGroup As Boolean, Optional ByVal dsProgressive As DataSet = Nothing)
            Try
                If SaveType = "Save" Then
                    Me.CreateCommandSql("Sp_Insert_Agree_Agreement", "")
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) '  VARCHAR(30)
                Else
                    Me.CreateCommandSql("Sp_Update_Agreement", "")
                    Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                End If
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Me.AGREEMENT_NO, 25) ' VARCHAR(25),
                Me.AddParameter("@AGREEMENT_DESC", SqlDbType.VarChar, Me.AgreementDescription, 50) ' VARCHAR(50),
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, Me.StartDate) 'DATETIME,
                Me.AddParameter("@END_DATE", SqlDbType.DateTime, Me.EndDate) ' DATETIME,
                Me.AddParameter("@QS_TREATMENT_FLAG", SqlDbType.Char, Me.QS_Treatment_Flag, 1) ' CHAR(1),
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If SaveType = "Update" Then
                    If isAddedGroup = False Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "DELETE FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "';"
                        Me.SqlCom.CommandText = "sp_executesql"
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    End If
                End If
                Me.SqlCom.CommandText = "Usp_Insert_Distributor_Agreement"
                Me.SqlCom.CommandType = CommandType.StoredProcedure
                For i As Integer = 1 To DISTRIBUTOR_IDS.Count
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_IDS(i), 10)
                    Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Me.AGREEMENT_NO, 25)
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                    Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Next
                If (Me.dsProghasChanged = True) And (Not IsNothing(dsProgressive)) Then
                    Me.SavaDataSet(dsProgressive)
                End If
                Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Sub SaveAgreement(Optional ByVal dsProgressive As DataSet = Nothing)
            'must make a singleleton object ado.net to automaticaly saving to database
            Try
                Me.GetConnection() 'set up connection
                'insert Agreement first
                Me.InsertData("Sp_Insert_Agree_Agreement", "")
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Me.AGREEMENT_NO, 25) ' VARCHAR(25),
                Me.AddParameter("@AGREEMENT_DESC", SqlDbType.VarChar, Me.AgreementDescription, 50) ' VARCHAR(50),
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, Me.StartDate) 'DATETIME,
                Me.AddParameter("@END_DATE", SqlDbType.DateTime, Me.EndDate) ' DATETIME,
                Me.AddParameter("@QS_TREATMENT_FLAG", SqlDbType.Char, Me.QS_Treatment_Flag, 1) ' CHAR(1),
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) '  VARCHAR(30)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Me.SqlCom.CommandText = "Usp_Insert_Distributor_Agreement"
                Me.SqlCom.CommandType = CommandType.StoredProcedure
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Me.AGREEMENT_NO, 25) ' VARCHAR(25)
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If (Me.dsProghasChanged = True) And (Not IsNothing(dsProgressive)) Then 'detect if dsprogressive has changed 
                    Me.SavaDataSet(dsProgressive)
                End If
                Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                Me.dsProghasChanged = False
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Sub UpdateAgreement(Optional ByVal dsProgressive As DataSet = Nothing)
            Try
                Me.GetConnection()
                'UPDATE AGREEMENT FIRST
                Me.UpdateData("Sp_Update_Agreement", "")
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Me.AGREEMENT_NO, 25) ' VARCHAR(25),
                Me.AddParameter("@AGREEMENT_DESC", SqlDbType.VarChar, Me.AgreementDescription, 50) ' (50),
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, Me.StartDate) ' DATETIME,
                Me.AddParameter("@END_DATE", SqlDbType.DateTime, Me.EndDate) ' DATETIME,
                Me.AddParameter("@QS_TREATMENT_FLAG", SqlDbType.Char, Me.QS_Treatment_Flag, 1) ' CHAR(1),
                Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) '  VARCHAR(30)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Me.SqlCom.CommandText = "sp_executesql" : Me.SqlCom.CommandType = CommandType.StoredProcedure
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "DELETE FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Me.SqlCom.CommandText = "Usp_Insert_Distributor_Agreement"
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, Me.AGREEMENT_NO, 25) ' VARCHAR(25)
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If (Me.dsProghasChanged = True) And (Not IsNothing(dsProgressive)) Then 'detect if dsprogressive has changed 
                    Me.SavaDataSet(dsProgressive)
                End If
                Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                Me.dsProghasChanged = False
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Sub DeleteAgreement(ByVal AGREEMENT_NO As String, Optional ByVal isDeletedAll As Boolean = False, Optional ByVal DISTRIBUTOR_ID As String = "")
            Try
                If isDeletedAll = True Then
                    Me.CreateCommandSql("Sp_Delete_AGREE_PROGRESSIVE_DISCOUNT", "")
                    Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                    Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                    Me.SqlCom.ExecuteScalar() ': Me.ClearCommandParameters()

                    Me.SqlCom.CommandText = "SET NOCOUNT ON;DELETE FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO ;"
                    Me.SqlCom.CommandType = CommandType.Text
                    'Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                    Me.SqlCom.ExecuteScalar()

                    Me.SqlCom.CommandText = "Sp_Delete_Agreement"
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                    'Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                    Me.SqlCom.ExecuteScalar() : Me.CommiteTransaction()
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " DELETE FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "' AND DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'"
                    Me.CreateCommandSql(CommandType.StoredProcedure, "sp_executesql", ConnectionTo.Nufarm)
                    Me.OpenConnection() : Me.SqlCom.ExecuteScalar()
                    'Me.BeginTransaction()
                    'Me.SqlCom.Transaction = Me.SqlTrans
                    'Me.SqlCom.ExecuteNonQuery()
                    'Me.ClearCommandParameters()
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
#End Region

#Region " Function "
        Public Function HasGeneratedDisCount(ByVal AGREEMENT_NO As String) As Boolean
            Dim retVal As Boolean = False
            Try
                Me.CreateCommandSql("Usp_Select_Reference_AGREE_BRANDPACK_ID_IN_AGREE_DISC_HISTORY", "")
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                If CInt(Me.ExecuteScalar()) > 0 Then
                    Return True
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return retVal
        End Function
        Public Function CheckAgreement_Group(ByVal AGREEMENT_NO As String) As Boolean
            Try
                Me.CreateCommandSql("sp_executesql", "")
                Dim Query As String = "SET NOCOUNT ON;SELECT COUNT(DISTRIBUTOR_ID) FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = '" & AGREEMENT_NO & "'"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Dim DistAgrCount As Integer = CInt(Me.SqlCom.ExecuteScalar())
                Me.ClearCommandParameters()
                If DistAgrCount > 1 Then
                    Me.SqlCom.CommandText = "Usp_Select_Agreement_Group"
                    'Me.SqlCom.CommandType = CommandType.StoredProcedure
                    'Me.CreateCommandSql("Usp_Select_Agreement_Group", "")
                    Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                    Dim tblAgrGroup As New DataTable("AGREEMENT_GROUP")
                    tblAgrGroup.Clear()
                    setDataAdapter(Me.SqlCom).Fill(tblAgrGroup)
                    Me.ClearCommandParameters()
                    Me.m_ViewDistributor = tblAgrGroup.DefaultView()
                    'Me.m_ViewDistributor = Me.baseChekTable.DefaultView()
                    Return True
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function HasGeneratedProgressiveDiscount(ByVal AGREEMENT_NO As String, ByVal FLAG As String, ByVal mustCloseConnection As Boolean) As Boolean
            Dim B As Boolean = False
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf
                If NufarmBussinesRules.SharedClass.DISC_AGREE_FROM = "PO" Then
                    Query &= "SELECT ISNULL((SELECT 1 WHERE EXISTS(SELECT BBS.BRND_B_S_ID FROM BRND_BRANDPACK_SAVING BBS " & vbCrLf & _
                    " INNER JOIN AGREE_BRANDPACK_INCLUDE ABI ON ABI.AGREE_BRANDPACK_ID = BBS.AGREE_BRANDPACK_ID WHERE ABI.AGREEMENT_NO = '" & _
                    AGREEMENT_NO & "' AND SUBSTRING(BBS.QSY_FLAG,1,1) = '" & FLAG & "')),0)"
                Else
                    If FLAG = "F" Then

                    End If
                    Query &= "SELECT 1 WHERE EXISTS(SELECT OOBD.ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_BRANDPACK_DISC OOBD INNER JOIN ACCRUED_DETAIL AD " & vbCrLf & _
                             " ON OOBD.ACHIEVEMENT_BRANDPACK_ID = AD.ACHIEVEMENT_BRANDPACK_ID INNER JOIN ACCRUED_HEADER ACRH " & vbCrLf & _
                             " ON AD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID WHERE ACRH.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND SUBSTRING(ACRH.FLAG,1,1) = '" & FLAG & "') " & vbCrLf & _
                             " OR EXISTS(SELECT AD.ACHIEVEMENT_BRANDPACK_ID FROM ACCRUED_DETAIL AD INNER JOIN ACCRUED_HEADER ACRH " & vbCrLf & _
                             " ON AD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID WHERE ACRH.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND SUBSTRING(ACRH.FLAG,1,1) = '" & FLAG & "')"

                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then
                        Me.ClearCommandParameters() : If mustCloseConnection Then : Me.CloseConnection() : End If : Return True
                    End If
                End If
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        " SELECT 1 WHERE EXISTS(SELECT OOR.ACHIEVEMENT_BRANDPACK_ID FROM ORDR_OA_REMAINDING OOR INNER JOIN ACCRUED_DETAIL AD " & vbCrLf & _
                        " ON OOR.ACHIEVEMENT_BRANDPACK_ID = AD.ACHIEVEMENT_BRANDPACK_ID INNER JOIN ACCRUED_HEADER ACRH " & vbCrLf & _
                        " ON AD.ACHIEVEMENT_ID = ACRH.ACHIEVEMENT_ID WHERE ACRH.AGREEMENT_NO = '" & AGREEMENT_NO & "' AND SUBSTRING(ACRH.FLAG,1,1) = '" & FLAG & "') ; "
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then
                        If mustCloseConnection Then : Me.CloseConnection() : End If : Return True
                    End If
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return B
        End Function
        Public Function GetDataViewDistributor(ByVal AGREEMENT_NO As String, ByVal isOriginalGroup As Boolean) As DataView
            Try
                If isOriginalGroup = False Then
                    Me.SearcData("", "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME" & _
                    " FROM DIST_DISTRIBUTOR DR WHERE NOT EXISTS(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT " & vbCrLf & _
                    " WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID AND AGREEMENT_NO = '" & AGREEMENT_NO & "');")
                    Me.m_ViewDistributor = Me.baseChekTable.DefaultView
                Else
                    Me.CreateCommandSql("Usp_Select_Agreement_Group", "")
                    Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                    Dim tblAgrGroup As New DataTable("AGREEMENT_GROUP")
                    tblAgrGroup.Clear()
                    Me.FillDataTable(tblAgrGroup)
                    Me.m_ViewDistributor = tblAgrGroup.DefaultView()
                End If
                Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
                Return Me.m_ViewDistributor
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function getLastStartDate() As DateTime
            Query = "SET NOCOUNT ON;" & vbCrLf & _
            " SELECT TOP 1 START_DATE FROM AGREE_AGREEMENT ORDER BY START_DATE DESC;"
            If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
            Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            End If
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.OpenConnection()
            Dim retval As Object = Me.SqlCom.ExecuteScalar()
            Me.ClearCommandParameters()
            If Not IsNothing(retval) Then
                Return Convert.ToDateTime(retval)
            End If
            Me.CloseConnection()
            Return Now
        End Function
        Public Function GetDataViewDistributor() As DataView
            Dim query As String = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR WHERE (INACTIVE = 0 OR INACTIVE IS NULL); "
            Me.CreateCommandSql("sp_executesql", "")
            Me.AddParameter("@stmt", SqlDbType.NVarChar, query)
            Dim T_Distributor As New DataTable("T_Distributor") : T_Distributor.Clear()
            Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.OpenConnection() : Me.SqlDat.Fill(T_Distributor)
            Me.ClearCommandParameters()
            Me.m_ViewDistributor = T_Distributor.DefaultView()
            'Me.FillDataTable("", "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR ", "T_Distributor")
            Me.m_ViewDistributor = T_Distributor.DefaultView()
            Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
            Return Me.m_ViewDistributor
        End Function
        Public Function ViewAgDistributor(ByVal rowfilter As String) As DataView
            Me.m_AgDistributorView.RowFilter = rowfilter
            Return Me.m_AgDistributorView
        End Function
        Public Sub GetData(ByVal SearchBy As String, Optional ByVal AgreementNo As String = "", Optional ByVal StartDate As Object = Nothing, Optional ByVal EndDate As Object = Nothing)
            Try
                'Me.GetDataViewDistributor()
                If SearchBy = "By Agreement NO" Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT * FROM VIEW_AGREEMENT WHERE AGREEMENT_NO LIKE '%'+@AgreementNO+'%' ;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@AgreementNo", SqlDbType.VarChar, AgreementNo, 30)
                Else
                    If Not IsNothing(StartDate) And Not IsNothing(EndDate) Then
                        Query = "SET NOCOUNT ON;SELECT * FROM VIEW_AGREEMENT WHERE START_DATE >= @StartDate AND END_DATE <= @EndDate; "
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                        Else : Me.ResetCommandText(CommandType.Text, Query)
                        End If
                        Me.AddParameter("@StartDate", SqlDbType.DateTime, StartDate)
                        Me.AddParameter("@EndDate", SqlDbType.DateTime, EndDate)
                    ElseIf Not IsNothing(StartDate) Then
                        Query = "SET NOCOUNT ON;SELECT * FROM VIEW_AGREEMENT WHERE START_DATE >= @StartDate; "
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                        Else : Me.ResetCommandText(CommandType.Text, Query)
                        End If
                        Me.AddParameter("@StartDate", SqlDbType.DateTime, StartDate)
                    ElseIf Not IsNothing(EndDate) Then
                        Query = "SET NOCOUNT ON;SELECT * FROM VIEW_AGREEMENT WHERE END_DATE >= @EndDate; "
                        Me.CreateCommandSql("", Query)
                        Me.AddParameter("@EndDate", SqlDbType.DateTime, EndDate)
                    Else
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "SELECT * FROM VIEW_AGREEMENT WHERE END_DATE >= @GETDATE;"
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                        Else : Me.ResetCommandText(CommandType.Text, Query)
                        End If
                        Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    End If
                End If

                Dim dtAgreement As New DataTable("T_Agreement") : dtAgreement.Clear()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection() : Me.SqlDat.Fill(dtAgreement)
                Me.ClearCommandParameters() : Me.m_AgDistributorView = dtAgreement.DefaultView()
                Me.m_AgDistributorView.Sort = "AGREEMENT_NO"
                If (IsNothing(Me.m_ViewDistributor)) Then
                    Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR"
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                    Me.SqlCom.CommandText = "sp_executesql"
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Dim T_Distributor As New DataTable("T_Distributor") : T_Distributor.Clear()
                    Me.SqlDat.Fill(T_Distributor) : Me.m_ViewDistributor = T_Distributor.DefaultView
                    Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
                End If
                Me.CloseConnection() : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
            'Return Me.m_DsBrandInclude
        End Sub
        'procedure untuk mengecek reference child data pada agreement pada saat mau di hapus
        Public Function HasReferecedData(ByVal AGREEMENT_NO As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT AGREEMENT_NO FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                " OR EXISTS(SELECT AGREEMENT_NO FROM AGREE_PROGRESSIVE_DISCOUNT WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                " OR EXISTS(SELECT AGREEMENT_NO FROM AGREE_PROGRESSIVE_DISC_VAL WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                " OR EXISTS(SELECT AGREEMENT_NO FROM AGREE_PROG_DISC_R WHERE AGREEMENT_NO = @AGREEMENT_NO) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 30)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    If mustCloseConnection Then : Me.CloseConnection() : End If
                    Return (CInt(retval) > 0)
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
#End Region

#Region " Constructor + Destructor "
        Public Sub New()
            'Me.m_DsBran = Nothing
            'Me.m_ViewDistributor = Nothing
            Me.AgreementDescription = DBNull.Value
        End Sub
        'dont dispose mybase because will be used in the form agreement
        Public Overloads Sub dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_AgDistributorView) Then
                Me.m_AgDistributorView.Dispose()
                Me.m_AgDistributorView = Nothing
            End If
            If Not IsNothing(Me.m_ViewDistributor) Then
                Me.m_ViewDistributor.Dispose()
                Me.m_ViewDistributor = Nothing
            End If
            If Not IsNothing(Me.m_dataTableQuarterly) Then
                Me.m_dataTableQuarterly.Dispose()
                Me.m_dataTableQuarterly = Nothing
            End If
            If Not IsNothing(Me.m_dataTableSemeterly) Then
                Me.m_dataTableSemeterly.Dispose()
                Me.m_dataTableSemeterly = Nothing
            End If
            If Not IsNothing(Me.m_dataTableYearly) Then
                Me.m_dataTableYearly.Dispose()
                Me.m_dataTableYearly = Nothing
            End If
            If Not IsNothing(Me.m_dsPeriod) Then
                Me.m_dsPeriod.Dispose()
                Me.m_dsPeriod = Nothing
            End If
        End Sub
#End Region

    End Class

End Namespace

