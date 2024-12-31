Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports NufarmBussinesRules
Imports NufarmBussinesRules.common
Imports NufarmBussinesRules.User
Imports NufarmDataAccesLayer.DataAccesLayer
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices
Namespace SettingDTS

    Public Class RefBussinesRulesSetting
        Inherits ADODotNet
        Private Query As String

        Public Sub New()
            MyBase.New()
            Me.Query = ""
        End Sub

        Public Sub DeleteLogErr(ByVal UserName As String)
            Try
                Me.Query = "SET NOCOUNT ON; " & VbCrLf & " DELETE FROM LOG_ERR WHERE CreatedBy = @CreatedBy;"
                If (Not Information.IsNothing(Me.SqlCom)) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If
                Me.AddParameter("@CreatedBy", SqlDbType.VarChar, UserName)
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
        End Sub

        Public Sub filDataExpeditions(ByVal mustCloseconnection As Boolean)
            Try
                Me.Query = "SET NOCOUNT ON;" & VbCrLf & "SELECT POLICE_NO_TRANS FROM GON_HEADER WHERE POLICE_NO_TRANS IS NOT NULL AND POLICE_NO_TRANS != '' " & VbCrLf & "UNION" & VbCrLf & "SELECT POLICE_NO_TRANS FROM GON_SEPARATED_HEADER WHERE POLICE_NO_TRANS IS NOT NULL AND POLICE_NO_TRANS != '';"
                If (Not Information.IsNothing(Me.SqlCom)) Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else
                    Me.CreateCommandSql("sp_executesql", "")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                SharedClass.tblPoliceNumber = New DataTable("Police_no_Trans")
                Me.setDataAdapter(Me.SqlCom).Fill(SharedClass.tblPoliceNumber)
                Me.ClearCommandParameters()
                Me.Query = "SET NOCOUNT ON;" & VbCrLf & " SELECT DRIVER_TRANS FROM GON_HEADER WHERE DRIVER_TRANS IS NOT NULL AND DRIVER_TRANS != ''" & VbCrLf & " UNION" & VbCrLf & " SELECT DRIVER_TRANS FROM GON_SEPARATED_HEADER WHERE DRIVER_TRANS IS NOT NULL AND DRIVER_TRANS != '' ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                SharedClass.tblDriverTrans = New DataTable("Driver_Trans")
                Me.setDataAdapter(Me.SqlCom).Fill(SharedClass.tblDriverTrans)
                Me.ClearCommandParameters()
                If (mustCloseconnection) Then
                    Me.CloseConnection()
                End If
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
        End Sub

        Public Sub FillDataUnit(ByVal mustCloseConnection As Boolean)
            Try
                Me.Query = "SET NOCOUNT ON;" & VbCrLf & " SELECT UNIT1 FROM BRND_PROD_CONV " & VbCrLf & " GROUP BY UNIT1"
                If (Not Information.IsNothing(Me.SqlCom)) Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else
                    Me.CreateCommandSql("sp_executesql", "")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                SharedClass.unit1 = New DataTable()
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(SharedClass.unit1)
                Me.ClearCommandParameters()
                Me.Query = "SET NOCOUNT ON;" & VbCrLf & " SELECT UNIT2 FROM BRND_PROD_CONV " & VbCrLf & " GROUP BY UNIT2"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                SharedClass.unit2 = New DataTable()
                Me.setDataAdapter(Me.SqlCom).Fill(SharedClass.unit2)
                Me.ClearCommandParameters()
                If (mustCloseConnection) Then
                    Me.CloseConnection()
                End If
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
        End Sub

        Public Sub FillDataUOM(ByVal mustCloseConnection As Boolean)
            Try
                Me.Query = "SET NOCOUNT ON;" & VbCrLf & " SELECT UnitOfMeasure FROM BRND_PROD_CONV " & VbCrLf & " GROUP BY UnitOfMeasure"
                If (Not Information.IsNothing(Me.SqlCom)) Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else
                    Me.CreateCommandSql("sp_executesql", "")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                SharedClass.tblUOM = New DataTable()
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(SharedClass.tblUOM)
                Me.ClearCommandParameters()
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
        End Sub

        Public Sub FillDataVolume(ByVal mustCloseConnection As Boolean)
            Try
                Me.Query = "SET NOCOUNT ON;" & VbCrLf & " SELECT VOL1 FROM BRND_PROD_CONV " & VbCrLf & " GROUP BY VOL1"
                If (Not Information.IsNothing(Me.SqlCom)) Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else
                    Me.CreateCommandSql("sp_executesql", "")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                SharedClass.tblVo11 = New DataTable()
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(SharedClass.tblVo11)
                Me.ClearCommandParameters()
                Me.Query = "SET NOCOUNT ON;" & VbCrLf & " SELECT VOL2 FROM BRND_PROD_CONV " & VbCrLf & " GROUP BY VOL2"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                SharedClass.tblVol2 = New DataTable()
                Me.setDataAdapter(Me.SqlCom).Fill(SharedClass.tblVol2)
                Me.ClearCommandParameters()
                If (mustCloseConnection) Then
                    Me.CloseConnection()
                End If
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
        End Sub

        Public Function getDPDByVal(ByVal mustCloseconnection As Boolean) As System.Data.DataTable
            Dim dataTable As System.Data.DataTable
            Try
                Me.Query = "SET NOCOUNT ON; " & VbCrLf & " SELECT IDApp,CodeApp,START_DATE,END_DATE,ParamValue AS FLAG,ValueNumeric AS ACHIEVEMENT FROM RefBussinesRules WHERE TypeApp = 'DPDToValue'  ORDER BY END_DATE DESC ;"
                If (Not Information.IsNothing(Me.SqlCom)) Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else
                    Me.CreateCommandSql("sp_executesql", "")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                Me.OpenConnection()
                Dim dataTable1 As System.Data.DataTable = New System.Data.DataTable("SETTING_DPD_BY_VALUE")
                Me.setDataAdapter(Me.SqlCom).Fill(dataTable1)
                If (mustCloseconnection) Then
                    Me.CloseConnection()
                End If
                dataTable = dataTable1
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.RollbackTransaction()
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw exception
            End Try
            Return dataTable
        End Function

        Public Function hasUsed(ByVal StartDate As Object, ByVal endDate As Object, ByVal Flag As String) As Boolean
            Dim flag1 As Boolean
            Try
                Me.Query = "SET NOCOUNT ON; " & VbCrLf & " SELECT 1 WHERE EXISTS(SELECT IDApp FROM ACCRUED_HEADER WHERE AGREEMENT_NO = ANY(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE START_DATE >= @StartDate AND END_DATE <= @EndDate) AND FLAG = @FLAG AND DISC_OBTAINED_FROM = 'T2')"
                If (Not Information.IsNothing(Me.SqlCom)) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, Convert.ToDateTime(RuntimeHelpers.GetObjectValue(StartDate)))
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, Convert.ToDateTime(RuntimeHelpers.GetObjectValue(endDate)))
                Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2)
                Me.OpenConnection()
                Dim objectValue As Object = RuntimeHelpers.GetObjectValue(Me.SqlCom.ExecuteScalar())
                Me.ClearCommandParameters()
                Me.CloseConnection()
                flag1 = IIf(Information.IsNothing(RuntimeHelpers.GetObjectValue(objectValue)), False, Conversions.ToInteger(objectValue) > 0)
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
            Return flag1
        End Function

        Public Sub ReadSettings()
            Me.Query = "SET NOCOUNT ON ; " & VbCrLf & "SELECT CodeApp,NameApp,TypeApp,DescriptionApp,ParamValue,ParamValueType,AllowRules,CreatedBy,CreatedDate FROM RefBussinesRules WHERE TypeApp NOT LIKE '%DPDDToVal%' ;"
            If (Information.IsNothing(Me.SqlCom)) Then
                Me.CreateCommandSql("sp_executesql", "")
            Else
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            End If
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
            Dim dataTable As System.Data.DataTable = New System.Data.DataTable("T_Setting")
            dataTable.Clear()
            Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
            Me.OpenConnection()
            Me.SqlDat.Fill(dataTable)
            Me.ClearCommandParameters()
            Dim settingConfigurations As List(Of NufarmBussinesRules.common.SettingConfigurations) = New List(Of NufarmBussinesRules.common.SettingConfigurations)()
            Dim count As Integer = dataTable.Rows.Count - 1
            Dim num As Integer = 0
            Do
                Dim settingConfiguration As NufarmBussinesRules.common.SettingConfigurations = New NufarmBussinesRules.common.SettingConfigurations()
                Dim flag As NufarmBussinesRules.common.SettingConfigurations = settingConfiguration
                flag.AllowRules = Conversions.ToBoolean(dataTable.Rows(num)("AllowRules"))
                flag.CodeApp = Conversions.ToString(dataTable.Rows(num)("CodeApp"))
                flag.CreatedBy = Conversions.ToString(dataTable.Rows(num)("CreatedBy"))
                flag.CreatedDate = Convert.ToDateTime(RuntimeHelpers.GetObjectValue(dataTable.Rows(num)("CreatedDate")))
                flag.DescriptionApp = Conversions.ToString(dataTable.Rows(num)("DescriptionApp"))
                flag.NameApp = Conversions.ToString(dataTable.Rows(num)("NameApp"))
                flag.TypeApp = Conversions.ToString(dataTable.Rows(num)("TypeApp"))
                flag.ParamValue = RuntimeHelpers.GetObjectValue(dataTable.Rows(num)("ParamValue"))
                flag.ParamValueType = RuntimeHelpers.GetObjectValue(dataTable.Rows(num)("ParamValueType"))
                If (Operators.CompareString(flag.CodeApp, "MSC0006", False) = 0) Then
                    If (Not flag.AllowRules) Then
                        SharedClass.DISC_AGREE_FROM = "PO"
                    Else
                        SharedClass.DISC_AGREE_FROM = "INVOICE"
                    End If
                End If
                flag = Nothing
                settingConfigurations.Add(settingConfiguration)
                num = num + 1
            Loop While num <= count
            SharedClass.ListSettings = settingConfigurations
            Me.ClearCommandParameters()
            Me.Query = "SET NOCOUNT ON;" & VbCrLf & " SELECT ParamValue FROM RefBussinesRules WHERE START_DATE <= CONVERT(VARCHAR(100),GETDATE(),101) AND END_DATE >= CONVERT(VARCHAR(100),GETDATE(),101) AND CreatedBy = @UserName " & VbCrLf & " AND CodeApp = 'MSC0011';"
            Me.ResetCommandText(CommandType.Text, Me.Query)
            Me.AddParameter("UserName", SqlDbType.VarChar, RuntimeHelpers.GetObjectValue(Interaction.IIf(Operators.CompareString(UserLogin.UserName, "", False) <> 0, UserLogin.UserName, "System Administrator")))
            Dim objectValue As Object = RuntimeHelpers.GetObjectValue(Me.SqlCom.ExecuteScalar())
            Me.ClearCommandParameters()
            Dim dateTime As System.DateTime = New System.DateTime(2021, 9, 30)
            If (Information.IsNothing(RuntimeHelpers.GetObjectValue(objectValue))) Then
                Me.Query = "SET NOCOUNT ON;" & VbCrLf & " SELECT TOP 1 END_DATE FROM RefBussinesRules WHERE CodeApp = 'MSC0011' ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                objectValue = RuntimeHelpers.GetObjectValue(Me.SqlCom.ExecuteScalar())
                dateTime = Convert.ToDateTime(RuntimeHelpers.GetObjectValue(objectValue))
                If (System.DateTime.Compare(SharedClass.ServerDate, dateTime) > 0) Then
                    SharedClass.DBInvoiceTo = SharedClass.CurrentInvToUse.NI109
                End If
            ElseIf (Not (Operators.CompareString(objectValue.ToString(), "NI87", False) <> 0 And Operators.CompareString(objectValue.ToString(), "", False) <> 0)) Then
                Me.Query = "SET NOCOUNT ON;" & VbCrLf & " SELECT TOP 1 END_DATE FROM RefBussinesRules WHERE CodeApp = 'MSC0011' ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                objectValue = RuntimeHelpers.GetObjectValue(Me.SqlCom.ExecuteScalar())
                dateTime = Convert.ToDateTime(RuntimeHelpers.GetObjectValue(objectValue))
                If (System.DateTime.Compare(SharedClass.ServerDate, dateTime) > 0) Then
                    SharedClass.DBInvoiceTo = SharedClass.CurrentInvToUse.NI109
                End If
            Else
                SharedClass.DBInvoiceTo = SharedClass.CurrentInvToUse.NI109
            End If
            Me.ClearCommandParameters()
        End Sub

        Public Sub SaveLog(ByVal ErrMessage As String, ByVal EventName As String)
            Dim shortDateString() As String = {EventName, "|", UserLogin.UserName, "|", Nothing}
            shortDateString(4) = SharedClass.ServerDate.ToShortDateString()
            Dim str As String = String.Concat(shortDateString)
            Try
                Me.Query = "SET NOCOUNT ON; " & VbCrLf & " IF NOT EXISTS(SELECT LOG_ID FROM LOG_ERR WHERE LOG_ID = @LOG_ID) " & VbCrLf & " BEGIN " & VbCrLf & " INSERT INTO LOG_ERR(LOG_ID,[ERROR_DESC],EVENT_NAME,CreatedBy,CreatedDate)" & VbCrLf & " VALUES(@LOG_ID,@ERROR_DESC,@EVENT_NAME,@CreatedBy,@CreatedDate)" & VbCrLf & " END " & VbCrLf & " ELSE " & VbCrLf & " BEGIN " & VbCrLf & " UPDATE LOG_ERR SET [ERROR_DESC] = @ERROR_DESC, ModifiedBy = @ModifiedBy, ModifiedDate = GETDATE() WHERE LOG_ID = @LOG_ID ;" & VbCrLf & " END "
                If (Not Information.IsNothing(Me.SqlCom)) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If
                Me.AddParameter("@LOG_ID", SqlDbType.VarChar, str, 200)
                Me.AddParameter("@ERROR_DESC", SqlDbType.VarChar, ErrMessage, 200)
                Me.AddParameter("@EVENT_NAME", SqlDbType.VarChar, EventName, 100)
                Me.AddParameter("@CreatedBy", SqlDbType.VarChar, UserLogin.UserName, 100)
                Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, SharedClass.ServerDate)
                Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, UserLogin.UserName)
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                Me.ClearCommandParameters()
                ProjectData.ClearProjectError()
            End Try
        End Sub

        Public Sub SaveSettingDPDVal(ByVal dtSetDPD As DataTable, ByVal mustCloseConnection As Boolean)
            Try
                Dim sqlCommand As System.Data.SqlClient.SqlCommand = Nothing
                Dim sqlCommand1 As System.Data.SqlClient.SqlCommand = Nothing
                Dim sqlCommand2 As System.Data.SqlClient.SqlCommand = Nothing
                Me.SqlDat = New SqlDataAdapter()
                Me.OpenConnection()
                Me.BeginTransaction()
                Dim dataRowArray As DataRow() = dtSetDPD.[Select]("", "", DataViewRowState.Added)
                If (CInt(dataRowArray.Length) > 0) Then
                    Me.Query = "SET NOCOUNT ON; " & VbCrLf & " IF NOT EXISTS(SELECT CodeApp FROM RefBussinesRules WHERE CodeApp = @CodeApp AND TypeApp = 'DPDToValue') " & VbCrLf & " BEGIN " & VbCrLf & " INSERT INTO RefBussinesRules(CodeApp,NameApp,TypeApp,DescriptionApp,ParamValue,ParamValueType,AllowRules,CreatedDate,CreatedBy,START_DATE,END_DATE,ValueNumeric) " & VbCrLf & " VALUES(@CodeApp,'SETTING ACH DPD TO VALUE','DPDToValue','SETTING PENCAPAIAN DPD BY VALUE, TOTAL YANG BISA MENDAPATKAN DISCOUNT',@FLAG,'VARCHAR',1,CONVERT(VARCHAR(100),GETDATE(),101),@CreatedBy,@START_DATE,@END_DATE,@ValueNumeric); " & VbCrLf & " END "
                    sqlCommand = Me.SqlConn.CreateCommand()
                    Dim query As System.Data.SqlClient.SqlCommand = sqlCommand
                    query.CommandType = CommandType.Text
                    query.CommandText = Me.Query
                    query.Transaction = Me.SqlTrans
                    query.Parameters.Add("@CodeApp", SqlDbType.VarChar, 50, "CodeApp")
                    query.Parameters.Add("@FLAG", SqlDbType.VarChar, 2, "FLAG")
                    query.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = UserLogin.UserName
                    query.Parameters.Add("@START_DATE", SqlDbType.SmallDateTime, 0, "START_DATE")
                    query.Parameters.Add("@END_DATE", SqlDbType.SmallDateTime, 0, "END_DATE")
                    query.Parameters.Add("@ValueNumeric", SqlDbType.[Decimal], 0, "Achievement")
                    query = Nothing
                    Me.SqlDat.InsertCommand = sqlCommand
                    Me.SqlDat.Update(dataRowArray)
                    sqlCommand.Parameters.Clear()
                End If
                Dim dataRowArray1 As DataRow() = dtSetDPD.[Select]("", "", DataViewRowState.ModifiedOriginal)
                If (CInt(dataRowArray1.Length) > 0) Then
                    Me.Query = "SET NOCOUNT ON; " & VbCrLf & " UPDATE RefBussinesRules SET ParamValue = @FLAG,START_DATE = @StartDate,END_DATE = @EndDate,ModifiedBy = @ModifiedBy,ValueNumeric = @ValueNumeric " & VbCrLf & " WHERE CodeApp = @CodeApp ;"
                    sqlCommand1 = Me.SqlConn.CreateCommand()
                    Dim sqlTrans As System.Data.SqlClient.SqlCommand = sqlCommand1
                    sqlTrans.CommandType = CommandType.Text
                    sqlTrans.CommandText = Me.Query
                    sqlTrans.Transaction = Me.SqlTrans
                    sqlTrans.Parameters.Add("@FLAG", SqlDbType.VarChar, 2, "FLAG")
                    sqlTrans.Parameters.Add("@StartDate", SqlDbType.SmallDateTime, 0, "START_DATE")
                    sqlTrans.Parameters.Add("@EndDate", SqlDbType.SmallDateTime, 0, "END_DATE")
                    sqlTrans.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100).Value = UserLogin.UserName
                    sqlTrans.Parameters.Add("@ValueNumeric", SqlDbType.[Decimal], 0, "Achievement")
                    sqlTrans.Parameters.Add("@CodeApp", SqlDbType.VarChar, 50, "CodeApp")
                    sqlTrans.Parameters("@CodeApp").SourceVersion = DataRowVersion.Original
                    sqlTrans = Nothing
                    Me.SqlDat.UpdateCommand = sqlCommand1
                    Me.SqlDat.Update(dataRowArray1)
                    sqlCommand1.Parameters.Clear()
                End If
                Dim dataRowArray2 As DataRow() = dtSetDPD.[Select]("", "", DataViewRowState.Deleted)
                If (CInt(dataRowArray2.Length) > 0) Then
                    Me.Query = "SET NOCOUNT ON; " & VbCrLf & " DELETE FROM RefBussinesRules WHERE CodeApp = @CodeApp;"
                    sqlCommand2 = Me.SqlConn.CreateCommand()
                    Dim query1 As System.Data.SqlClient.SqlCommand = sqlCommand2
                    query1.CommandType = CommandType.Text
                    query1.CommandText = Me.Query
                    query1.Transaction = Me.SqlTrans
                    query1.Parameters.Add("@CodeApp", SqlDbType.VarChar, 50, "CodeApp")
                    query1.Parameters("@CodeApp").SourceVersion = DataRowVersion.Original
                    query1 = Nothing
                    Me.SqlDat.DeleteCommand = sqlCommand2
                    Me.SqlDat.Update(dataRowArray2)
                    sqlCommand2.Parameters.Clear()
                End If
                Me.CommiteTransaction()
                If (mustCloseConnection) Then
                    Me.CloseConnection()
                End If
                Me.ClearCommandParameters()
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.RollbackTransaction()
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw exception
            End Try
        End Sub

        Public Sub SaveSettings(ByVal ListSettings As List(Of SettingConfigurations))
            Try
                If (ListSettings.Count > 0) Then
                    Me.Query = "SET NOCOUNT ON; " & VbCrLf & "UPDATE RefBussinesRules SET AllowRules = @AllowRules,ParamValue = @ParamValue,ParamValueType = @ParamValueType WHERE CodeApp = @CodeApp; "
                    If (Not Information.IsNothing(Me.SqlCom)) Then
                        Me.ResetCommandText(CommandType.Text, Me.Query)
                    Else
                        Me.CreateCommandSql("", Me.Query)
                    End If
                    Me.OpenConnection()
                    Me.BeginTransaction()
                    Me.SqlCom.Transaction = Me.SqlTrans
                    Dim count As Integer = ListSettings.Count - 1
                    Dim num As Integer = 0
                    Do
                        Me.AddParameter("@AllowRules", SqlDbType.Bit, Convert.ToBoolean(ListSettings(num).AllowRules))
                        Me.AddParameter("@CodeApp", SqlDbType.VarChar, ListSettings(num).CodeApp, 50)
                        Me.AddParameter("@ParamValue", SqlDbType.NVarChar, RuntimeHelpers.GetObjectValue(ListSettings(num).ParamValue), 50)
                        Me.AddParameter("@ParamValueType", SqlDbType.NVarChar, RuntimeHelpers.GetObjectValue(ListSettings(num).ParamValueType), 20)
                        Me.SqlCom.ExecuteScalar()
                        Me.ClearCommandParameters()
                        num = num + 1
                    Loop While num <= count
                    Me.CommiteTransaction()
                    Me.ReadSettings()
                    Me.CloseConnection()
                End If
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
        End Sub
    End Class
End Namespace

