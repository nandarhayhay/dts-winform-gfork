Imports System.Data.SqlClient
Imports System.Configuration
Namespace SettingDTS

    Public Class RefBussinesRulesSetting : Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet

        Public Function hasUsed(ByVal StartDate As Object, ByVal endDate As Object, ByVal Flag As String) As Boolean
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT 1 WHERE EXISTS(SELECT IDApp FROM ACCRUED_HEADER WHERE AGREEMENT_NO = ANY(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE START_DATE >= @StartDate AND END_DATE <= @EndDate) AND FLAG = @FLAG AND DISC_OBTAINED_FROM = 'T2')"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, Convert.ToDateTime(StartDate))
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, Convert.ToDateTime(endDate))
                Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CloseConnection()
                If Not IsNothing(retval) Then
                    Return (CInt(retval) > 0)
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Private Query As String = ""
        Public Sub ReadSettings()
            Query = "SET NOCOUNT ON ; " & vbCrLf & _
                    "SELECT CodeApp,NameApp,TypeApp,DescriptionApp,ParamValue,ParamValueType,AllowRules,CreatedBy,CreatedDate FROM RefBussinesRules WHERE TypeApp NOT LIKE '%DPDDToVal%' ;"
            If Not IsNothing(Me.SqlCom) Then
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            Else
                Me.CreateCommandSql("sp_executesql", "")
            End If
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            Dim tblSettings As New DataTable("T_Setting") : tblSettings.Clear()
            Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
            Me.OpenConnection()
            Me.SqlDat.Fill(tblSettings) : Me.ClearCommandParameters()
            Dim ListSettings As New List(Of common.SettingConfigurations)
            For i As Integer = 0 To tblSettings.Rows.Count - 1
                Dim SettingConfig As New common.SettingConfigurations()
                With SettingConfig
                    .AllowRules = CBool(tblSettings.Rows(i)("AllowRules"))
                    .CodeApp = CStr(tblSettings.Rows(i)("CodeApp"))
                    .CreatedBy = CStr(tblSettings.Rows(i)("CreatedBy"))
                    .CreatedDate = Convert.ToDateTime(tblSettings.Rows(i)("CreatedDate"))
                    .DescriptionApp = CStr(tblSettings.Rows(i)("DescriptionApp"))
                    .NameApp = CStr(tblSettings.Rows(i)("NameApp"))
                    .TypeApp = CStr(tblSettings.Rows(i)("TypeApp"))
                    .ParamValue = tblSettings.Rows(i)("ParamValue")
                    .ParamValueType = tblSettings.Rows(i)("ParamValueType")
                    If .CodeApp = "MSC0006" Then
                        If .AllowRules Then
                            NufarmBussinesRules.SharedClass.DISC_AGREE_FROM = "INVOICE"
                        Else
                            NufarmBussinesRules.SharedClass.DISC_AGREE_FROM = "PO"
                        End If
                    End If
                End With
                ListSettings.Add(SettingConfig)
            Next
            NufarmBussinesRules.SharedClass.ListSettings = ListSettings

            Me.ClearCommandParameters()
            ''setting Access Accpac Database apakah ke NI87 atau ke NI109 berdasarkan current user
            Query = "SET NOCOUNT ON;" & vbCrLf & _
            " SELECT ParamValue FROM RefBussinesRules WHERE START_DATE <= CONVERT(VARCHAR(100),GETDATE(),101) AND END_DATE >= CONVERT(VARCHAR(100),GETDATE(),101) AND CreatedBy = @UserName " & vbCrLf & _
            " AND CodeApp = 'MSC0011';"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("UserName", SqlDbType.VarChar, IIf(NufarmBussinesRules.User.UserLogin.UserName <> "", NufarmBussinesRules.User.UserLogin.UserName, "System Administrator"))
            Dim retval As Object = Me.SqlCom.ExecuteScalar()
            Me.ClearCommandParameters()
            Dim endDate As Date = New Date(2021, 9, 30)
            If Not IsNothing(retval) Then
                If retval.ToString() <> "NI87" And retval.ToString() <> "" Then
                    NufarmBussinesRules.SharedClass.DBInvoiceTo = SharedClass.CurrentInvToUse.NI109
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT TOP 1 END_DATE FROM RefBussinesRules WHERE CodeApp = 'MSC0011' ;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    retval = Me.SqlCom.ExecuteScalar()
                    endDate = Convert.ToDateTime(retval)
                    If NufarmBussinesRules.SharedClass.ServerDate > endDate Then
                        NufarmBussinesRules.SharedClass.DBInvoiceTo = SharedClass.CurrentInvToUse.NI109
                    End If
                End If
            Else 'cek startdate dan enddate
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT TOP 1 END_DATE FROM RefBussinesRules WHERE CodeApp = 'MSC0011' ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                retval = Me.SqlCom.ExecuteScalar()
                endDate = Convert.ToDateTime(retval)
                If NufarmBussinesRules.SharedClass.ServerDate > endDate Then
                    NufarmBussinesRules.SharedClass.DBInvoiceTo = SharedClass.CurrentInvToUse.NI109
                End If
            End If

            Me.ClearCommandParameters()
        End Sub
        Public Sub filDataExpeditions(ByVal mustCloseconnection As Boolean)
            Try
                ''isi NO polisi
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                "SELECT POLICE_NO_TRANS FROM GON_HEADER WHERE POLICE_NO_TRANS IS NOT NULL AND POLICE_NO_TRANS != '' " & vbCrLf & _
                "UNION" & vbCrLf & _
                "SELECT POLICE_NO_TRANS FROM GON_SEPARATED_HEADER WHERE POLICE_NO_TRANS IS NOT NULL AND POLICE_NO_TRANS != '';"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                NufarmBussinesRules.SharedClass.tblPoliceNumber = New DataTable("Police_no_Trans")
                setDataAdapter(Me.SqlCom).Fill(NufarmBussinesRules.SharedClass.tblPoliceNumber)
                Me.ClearCommandParameters()
                ''isi table supir
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT DRIVER_TRANS FROM GON_HEADER WHERE DRIVER_TRANS IS NOT NULL AND DRIVER_TRANS != ''" & vbCrLf & _
                " UNION" & vbCrLf & _
                " SELECT DRIVER_TRANS FROM GON_SEPARATED_HEADER WHERE DRIVER_TRANS IS NOT NULL AND DRIVER_TRANS != '' ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                NufarmBussinesRules.SharedClass.tblDriverTrans = New DataTable("Driver_Trans")
                setDataAdapter(Me.SqlCom).Fill(NufarmBussinesRules.SharedClass.tblDriverTrans)
                Me.ClearCommandParameters()
                If mustCloseconnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            'Dim colPolTrans As New AutoCompleteStringCollection()
            'For Each row As DataRow In NufarmBussinesRules.SharedClass.tblDriverTrans.Rows
            '    colPolTrans.Add(row("POLICE_NO_TRANS"))
            'Next
            'Me.txtPolice_no_Trans.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'Me.txtPolice_no_Trans.AutoCompleteSource = AutoCompleteSource.CustomSource
            'Me.txtPolice_no_Trans.AutoCompleteCustomSource = colPolTrans

            'Dim colDriverTrans As New AutoCompleteStringCollection()
            'For Each row As DataRow In NufarmBussinesRules.SharedClass.tblDriverTrans.Rows
            '    colDriverTrans.Add(row("DRIVER_TRANS"))
            'Next
            'Me.txtDriverTrans.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'Me.txtPolice_no_Trans.AutoCompleteSource = AutoCompleteSource.CustomSource
            'Me.txtPolice_no_Trans.AutoCompleteCustomSource = colDriverTrans
        End Sub
        ''' <summary>
        ''' Nanti lagi di kerjakan sesuda selesai inti program
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub DeleteLogErr(ByVal UserName As String)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " DELETE FROM LOG_ERR WHERE CreatedBy = @CreatedBy;"

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@CreatedBy", SqlDbType.VarChar, UserName)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        ''' <summary>
        ''' Nanti lagi di kerjakan sesudah selesai inti program
        ''' </summary>
        ''' <param name="ErrMessage"></param>
        ''' <param name="EventName"></param>
        ''' <remarks></remarks>
        Public Sub SaveLog(ByVal ErrMessage As String, ByVal EventName As String)
            'string LogID
            Dim LogID As String = EventName & "|" & NufarmBussinesRules.User.UserLogin.UserName & "|" & NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " IF NOT EXISTS(SELECT LOG_ID FROM LOG_ERR WHERE LOG_ID = @LOG_ID) " & vbCrLf & _
                " BEGIN " & vbCrLf & _
                " INSERT INTO LOG_ERR(LOG_ID,[ERROR_DESC],EVENT_NAME,CreatedBy,CreatedDate)" & vbCrLf & _
                " VALUES(@LOG_ID,@ERROR_DESC,@EVENT_NAME,@CreatedBy,@CreatedDate)" & vbCrLf & _
                " END " & vbCrLf & _
                " ELSE " & vbCrLf & _
                " BEGIN " & vbCrLf & _
                " UPDATE LOG_ERR SET [ERROR_DESC] = @ERROR_DESC, ModifiedBy = @ModifiedBy, ModifiedDate = GETDATE() WHERE LOG_ID = @LOG_ID ;" & vbCrLf & _
                " END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@LOG_ID", SqlDbType.VarChar, LogID, 200) ' [varchar](100) NOT NULL,
                Me.AddParameter("@ERROR_DESC", SqlDbType.VarChar, ErrMessage, 200) ' [varchar](100) NULL,
                Me.AddParameter("@EVENT_NAME", SqlDbType.VarChar, EventName, 100) ' [varchar](100) NULL,
                Me.AddParameter("@CreatedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100) ' [varchar](100) NULL,
                Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate) ' [smalldatetime] NULL,
                Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName) ' [varchar](100) NULL,
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

            Catch ex As Exception
                Me.ClearCommandParameters()
            End Try

        End Sub
        Public Sub SaveSettingDPDVal(ByVal dtSetDPD As DataTable, ByVal mustCloseConnection As Boolean)
            Try
                Dim commandInsert As SqlCommand = Nothing
                Dim commandUpdate As SqlCommand = Nothing
                Dim commandDelete As SqlCommand = Nothing
                Me.SqlDat = New SqlDataAdapter()
                Me.OpenConnection() : Me.BeginTransaction()

                Dim insertedRows() As DataRow = dtSetDPD.Select("", "", DataViewRowState.Added)
                If insertedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " IF NOT EXISTS(SELECT CodeApp FROM RefBussinesRules WHERE CodeApp = @CodeApp AND TypeApp = 'DPDToValue') " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " INSERT INTO RefBussinesRules(CodeApp,NameApp,TypeApp,DescriptionApp,ParamValue,ParamValueType,AllowRules,CreatedDate,CreatedBy,START_DATE,END_DATE,ValueNumeric) " & vbCrLf & _
                            " VALUES(@CodeApp,'SETTING ACH DPD TO VALUE','DPDToValue','SETTING PENCAPAIAN DPD BY VALUE, TOTAL YANG BISA MENDAPATKAN DISCOUNT',@FLAG,'VARCHAR',1,CONVERT(VARCHAR(100),GETDATE(),101),@CreatedBy,@START_DATE,@END_DATE,@ValueNumeric); " & vbCrLf & _
                            " END "
                    commandInsert = Me.SqlConn.CreateCommand()
                    With commandInsert
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Transaction = Me.SqlTrans
                        .Parameters.Add("@CodeApp", SqlDbType.VarChar, 50, "CodeApp")
                        .Parameters.Add("@FLAG", SqlDbType.VarChar, 2, "FLAG")
                        .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                        .Parameters.Add("@START_DATE", SqlDbType.SmallDateTime, 0, "START_DATE")
                        .Parameters.Add("@END_DATE", SqlDbType.SmallDateTime, 0, "END_DATE")
                        .Parameters.Add("@ValueNumeric", SqlDbType.Decimal, 0, "Achievement")
                    End With
                    SqlDat.InsertCommand = commandInsert
                    SqlDat.Update(insertedRows) : commandInsert.Parameters.Clear()
                End If
                Dim updatedRows() As DataRow = dtSetDPD.Select("", "", DataViewRowState.ModifiedOriginal)
                If updatedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " UPDATE RefBussinesRules SET ParamValue = @FLAG,START_DATE = @StartDate,END_DATE = @EndDate,ModifiedBy = @ModifiedBy,ValueNumeric = @ValueNumeric " & vbCrLf & _
                    " WHERE CodeApp = @CodeApp ;"
                    commandUpdate = Me.SqlConn.CreateCommand()
                    With commandUpdate
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Transaction = Me.SqlTrans
                        .Parameters.Add("@FLAG", SqlDbType.VarChar, 2, "FLAG")
                        .Parameters.Add("@StartDate", SqlDbType.SmallDateTime, 0, "START_DATE")
                        .Parameters.Add("@EndDate", SqlDbType.SmallDateTime, 0, "END_DATE")
                        .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100).Value = NufarmBussinesRules.User.UserLogin.UserName
                        .Parameters.Add("@ValueNumeric", SqlDbType.Decimal, 0, "Achievement")
                        .Parameters.Add("@CodeApp", SqlDbType.VarChar, 50, "CodeApp")
                        .Parameters()("@CodeApp").SourceVersion = DataRowVersion.Original
                    End With
                    SqlDat.UpdateCommand = commandUpdate
                    SqlDat.Update(updatedRows) : commandUpdate.Parameters.Clear()
                End If
                Dim deletedRows() As DataRow = dtSetDPD.Select("", "", DataViewRowState.Deleted)
                If deletedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " DELETE FROM RefBussinesRules WHERE CodeApp = @CodeApp;"
                    commandDelete = Me.SqlConn.CreateCommand()
                    With commandDelete
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Transaction = Me.SqlTrans
                        .Parameters.Add("@CodeApp", SqlDbType.VarChar, 50, "CodeApp")
                        .Parameters()("@CodeApp").SourceVersion = DataRowVersion.Original
                    End With
                    SqlDat.DeleteCommand = commandDelete
                    SqlDat.Update(deletedRows) : commandDelete.Parameters.Clear()
                End If
                Me.CommiteTransaction()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.RollbackTransaction() : Me.ClearCommandParameters() : Me.CloseConnection()
                Throw ex
            End Try

        End Sub
        Public Function getDPDByVal(ByVal mustCloseconnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT IDApp,CodeApp,START_DATE,END_DATE,ParamValue AS FLAG,ValueNumeric AS ACHIEVEMENT FROM RefBussinesRules WHERE TypeApp = 'DPDToValue'  ORDER BY END_DATE DESC ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                'START_DATE
                'END_DATE
                'FLAG
                'ACHIEVEMENT
                'IDApp
                'CodeApp
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim tblDPDByValue As New DataTable("SETTING_DPD_BY_VALUE")
                Me.setDataAdapter(Me.SqlCom).Fill(tblDPDByValue)
                If mustCloseconnection Then : Me.CloseConnection() : End If
                Return tblDPDByValue
            Catch ex As Exception
                Me.RollbackTransaction() : Me.ClearCommandParameters() : Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Sub SaveSettings(ByVal ListSettings As List(Of common.SettingConfigurations))
            Try
                If Not ListSettings.Count <= 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                           "UPDATE RefBussinesRules SET AllowRules = @AllowRules,ParamValue = @ParamValue,ParamValueType = @ParamValueType WHERE CodeApp = @CodeApp; "
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.OpenConnection()
                    Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                    For i As Integer = 0 To ListSettings.Count - 1
                        Me.AddParameter("@AllowRules", SqlDbType.Bit, Convert.ToBoolean(ListSettings(i).AllowRules))
                        Me.AddParameter("@CodeApp", SqlDbType.VarChar, ListSettings(i).CodeApp, 50)
                        Me.AddParameter("@ParamValue", SqlDbType.NVarChar, ListSettings(i).ParamValue, 50)
                        Me.AddParameter("@ParamValueType", SqlDbType.NVarChar, ListSettings(i).ParamValueType, 20)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next 'readsetting
                    Me.CommiteTransaction() : Me.ReadSettings() : Me.CloseConnection()
                End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
    End Class
End Namespace

