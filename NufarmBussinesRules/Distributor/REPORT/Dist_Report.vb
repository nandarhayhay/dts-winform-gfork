Imports System.Data.SqlClient
Namespace DistributorReport
    Public Class Dist_Report
        Inherits NufarmBussinesRules.DistributorRegistering.DistributorRegistering
        Private m_ViewDistReport As DataView
        Private m_View_Target As DataView
        Private m_View_Target_4MPeriode As DataView
        Private m_ViewPODispro As DataView
        Private Query As String = ""
        Public Function GetDPDFMP(ByVal START_DATE As Object, ByVal END_DATE As Object, Optional ByVal _
            DISTRIBUTOR_ID As String = "") As DataView
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql(CommandType.StoredProcedure, "Usp_Get_Total_PO_And_DPD")
                Else
                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Total_PO_And_DPD")
                End If
                If DISTRIBUTOR_ID = "" Then
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DBNull.Value, 10) ' VARCHAR(10),
                Else
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                End If
                    Me.AddParameter("@START_DATE", SqlDbType.DateTime, START_DATE) ' DATETIME,
                    Me.AddParameter("@END_DATE", SqlDbType.DateTime, END_DATE) ' DATETIME
                Dim dtTable As New DataTable("RPODispro") : dtTable.Clear()
                setDataAdapter(Me.SqlCom).Fill(dtTable)
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Function Create_View_ReportPODispro(ByVal START_DATE As Object, ByVal END_DATE As Object, Optional ByVal _
            DISTRIBUTOR_ID As String = "") As DataView
            Try

                'Me.CreateViewDistributor()
                'Dim AllowRulesReportDisprobyBrand As Boolean = False
                'Dim ConfigSetting As common.SettingConfigurations = Nothing
                ' ''cari settingan dispro by brand codeapp = RPT001
                ''Setting untuk menentukan Report Report Summary Dispro by Brand di hubungkan ke invoice = 1,PO = 0
                'For Each ConfigSetting In NufarmBussinesRules.SharedClass.ListSettings
                '    If ConfigSetting.CodeApp = "RPT001" Then
                '        If ConfigSetting.AllowRules = True Then
                '            AllowRulesReportDisprobyBrand = True
                '        End If
                '    End If
                'Next
                'If Not AllowRulesReportDisprobyBrand Then
                '    Me.CreateCommandSql(CommandType.StoredProcedure, "Usp_Get_Detail_Qty_Dispro", ConnectionTo.Nufarm)
                'Else
                Me.CreateCommandSql(CommandType.StoredProcedure, "Usp_Get_Detail_Qty_Dispro_By_Invoice", ConnectionTo.Nufarm)
                'End If

                If DISTRIBUTOR_ID = "" Then
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DBNull.Value, 10) ' VARCHAR(10),
                Else
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                End If
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, START_DATE) ' DATETIME,
                Me.AddParameter("@END_DATE", SqlDbType.DateTime, END_DATE) ' DATETIME
                Dim dtTable As New DataTable("RPODispro") : dtTable.Clear()
                Me.FillDataTable(dtTable) : Me.m_ViewPODispro = dtTable.DefaultView()
                Return Me.m_ViewPODispro
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Private Sub CreateTempTable(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal StrDecStartDate As String, ByVal strDecEndDate As String)
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                   "IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                   " BEGIN " & vbCrLf & _
                   " EXEC Usp_Create_Temp_Date_Invoice @I_START_DATE = @START_DATE,@I_END_DATE = @END_DATE; " & vbCrLf & _
                   " END "
            If Not IsNothing(Me.SqlCom) Then
                Me.ResetCommandText(CommandType.Text, Query)
                Me.SqlCom.Connection = Me.GetConnection() : Else : Me.CreateCommandSql("", Query)
            End If
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
            Me.OpenConnection()
            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            Dim retvalStartDate As DateTime = Nothing, retvalEndDate As DateTime = Nothing
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT START_DATE,END_DATE FROM tempdb..##T_START_DATE_" & Me.ComputerName & " ;"
            Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read() : retvalStartDate = SqlRe.GetDateTime(0) : retvalEndDate = SqlRe.GetDateTime(1) : End While
            Me.SqlRe.Close() : Me.ClearCommandParameters()
            If (Not StartDate.Equals(retvalStartDate)) Or (Not EndDate.Equals(retvalEndDate)) Then
                'bikin baru
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN UPDATE tempdb..##T_START_DATE_" & Me.ComputerName & " SET START_DATE = @START_DATE,END_DATE = @END_DATE;  END " & vbCrLf & _
                        " ELSE " & vbCrLf & _
                        " BEGIN EXEC Usp_Create_Temp_Date_Invoice @I_START_DATE = @START_DATE,@I_END_DATE = @END_DATE; END " & vbCrLf & _
                        " IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_SELECT_INVOICE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN  DROP TABLE tempdb..##T_SELECT_INVOICE_" & Me.ComputerName & " ; END " & vbCrLf & _
                        " EXEC Usp_Create_Temp_Invoice_Table @DEC_START_DATE = @D_START_DATE,@DEC_END_DATE = @D_END_DATE,@COMPUTERNAME = @C_NAME ; "
            Else
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_SELECT_INVOICE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN  EXEC Usp_Create_Temp_Invoice_Table @DEC_START_DATE = @D_START_DATE,@DEC_END_DATE = @D_END_DATE,@COMPUTERNAME = @C_NAME ; END " & vbCrLf & _
                        " IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_BRANDPACK' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN  EXEC Usp_Create_Temp_Table_BrandPack; END "
            End If
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
            Me.AddParameter("@D_START_DATE", SqlDbType.VarChar, StrDecStartDate)
            Me.AddParameter("@D_END_DATE", SqlDbType.VarChar, strDecEndDate)
            Me.AddParameter("@C_NAME", SqlDbType.VarChar, Me.ComputerName, 100)
            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
        End Sub
        Public Function Create_View_PODisproByBrand(ByVal START_DATE As Object, ByVal END_DATE As Object) As DataView
            Try
                If Not IsNothing(Me.SqlCom) Then : Me.GetConnection()
                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Total_PO_Dispro_Brand_By_Invoice")
                Else
                    Me.CreateCommandSql("Usp_Get_Total_PO_Dispro_Brand_By_Invoice", "")
                End If
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, START_DATE)
                Me.AddParameter("@END_DATE", SqlDbType.DateTime, END_DATE)
                Dim dtTable As New DataTable("RPDisproBrand")
                dtTable.Clear()
                Me.FillDataTable(dtTable)
                Me.m_ViewPODispro = dtTable.DefaultView()
                Return Me.m_ViewPODispro
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        'Public Function Create_View_TA_4MPeriode(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal HasChangedPeriode As Boolean) As DataView
        '    Try
        '        If HasChangedPeriode Then
        '            Query = "SET NOCOUNT ON;" & vbCrLf & _
        '            " IF EXISTS(SELECT * FROM tempdb.sys.objects WHERE [NAME] = '##T_TargetAgreementReport_4MPeriode_" & Me.ComputerName & "') " & vbCrLf & _
        '            " BEGIN DROP TABLE TEMPDB..##T_TargetAgreementReport_4MPeriode_" & Me.ComputerName & "; END "

        '            If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
        '            Else
        '                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
        '            End If
        '            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)

        '            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

        '        End If
        '    Catch ex As Exception
        '        Me.CloseConnection() : Me.ClearCommandParameters()
        '        Throw ex
        '    End Try
        'End Function
        Public Function Create_View_TA(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal HasChangedPeriode As Boolean) As DataView
            Try
                Me.OpenConnection()
                If HasChangedPeriode Then

                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " IF EXISTS(SELECT * FROM tempdb.sys.objects WHERE [NAME] = '##T_TargetAgreementReport') " & vbCrLf & _
                    " BEGIN DROP TABLE TEMPDB..##T_TargetAgreementReport; END "

                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                    Else
                        Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    End If
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)

                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Create_View_Target_Reaching_Agreement")
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                    Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " IF NOT EXISTS(SELECT * FROM tempdb.sys.objects WHERE [NAME] = '##T_TargetAgreementReport')" & vbCrLf & _
                            "   BEGIN EXEC Usp_Create_View_Target_Reaching_Agreement @START_DATE = @I_START_DATE,@END_DATE = @I_END_DATE ; END " & vbCrLf & _
                            " ELSE " & vbCrLf & _
                            "   BEGIN SELECT * FROM Tempdb.sys.##T_TargetAgreementReport; END "
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If

                    Me.AddParameter("@I_START_DATE", SqlDbType.SmallDateTime, StartDate)
                    Me.AddParameter("@I_END_DATE", SqlDbType.SmallDateTime, EndDate)
                End If
                Dim dtTable As New DataTable("Target_Agreement")
                dtTable.Clear()

                Me.setDataAdapter(Me.SqlCom).Fill(dtTable)
                'Me.FillDataTable(dtTable)
                Me.m_View_Target = dtTable.DefaultView()
                Me.ClearCommandParameters()
                Return Me.m_View_Target
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Sub GetCurrentAgreement(ByRef StartDate As DateTime, ByRef EndDate As DateTime, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT TOP 1 START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE END_DATE >= CONVERT(VARCHAR(100),GETDATE(),101); "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "stmt")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteReader() : Me.ClearCommandParameters()
                While Me.SqlRe.Read()
                    StartDate = SqlRe.GetDateTime(0)
                    EndDate = SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close()
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Function getNewOrModifiedData(ByVal FromDate As Object, ByVal untilDate As Object)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT * FROM Tempdb.sys.##T_TargetAgreementReport WHERE (MODIFY_DATE >= @FromDate AND MODIFY_DATE <= @UntilDate) AND ((DATEDIFF(DAY,CREATE_DATE,MODIFY_DATE) > 1) OR (DATEDIFF(MONTH,CREATE_DATE,MODIFY_DATE)>1)) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@FromDate", SqlDbType.SmallDateTime, FromDate)
                Me.AddParameter("@UntilDate", SqlDbType.SmallDateTime, untilDate)
                Me.OpenConnection()
                Dim dtTable As New DataTable("Target_Agreement")
                dtTable.Clear()
                Me.setDataAdapter(Me.SqlCom).Fill(dtTable)
                'Me.FillDataTable(dtTable)
                Me.m_View_Target = dtTable.DefaultView()
                Return Me.m_View_Target
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Function CreateViewDistributor_1() As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR;"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_Distributor")
                dtTable.Clear() : Me.FillDataTable(dtTable)
                Me.ViewDistributor = dtTable.DefaultView
                Me.ViewDistributor.Sort = "DISTRIBUTOR_NAME"
                Return Me.ViewDistributor()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try

        End Function
        Public Function CreateViewDistributor() As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR " & vbCrLf & _
                        " WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER); "
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Dim dtTable As New DataTable("T_Distributor") : dtTable.Clear()
                Me.OpenConnection() : Me.SqlDat.Fill(dtTable) : Me.ViewDistributor = dtTable.DefaultView()
                Me.CloseConnection() : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.ViewDistributor()
        End Function
        Public Function CreateViewDistReport(Optional ByVal FROMOADATE As Object = Nothing, Optional ByVal UNTILOADATE As Object = Nothing)
            Try
                'bikin datatable
                Dim tblDistReport As New DataTable("DISTRIBUTOR_REPORT")
            
                Me.CreateCommandSql("Usp_Create_View_Distributor_Report_1", "")
                'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                If (Not IsNothing(FROMOADATE)) And (Not IsDBNull(UNTILOADATE)) Then
                    Me.AddParameter("@StartDate", SqlDbType.DateTime, FROMOADATE) ' DATETIME,
                    Me.AddParameter("@EndDate", SqlDbType.DateTime, UNTILOADATE) ' DATETIME,
                ElseIf Not IsNothing(FROMOADATE) Then
                    Me.AddParameter("@StartDate", SqlDbType.DateTime, FROMOADATE) ' DATETIME,
                    Me.AddParameter("@EndDate", SqlDbType.DateTime, UNTILOADATE) ' DATETIME,
                ElseIf Not IsNothing(UNTILOADATE) Then
                    Me.AddParameter("@EndDate", SqlDbType.DateTime, UNTILOADATE) ' DATETIME,
                    Me.AddParameter("@EndDate", SqlDbType.DateTime, FROMOADATE) ' DATETIME,
                    
                End If
                tblDistReport.Clear() : Me.FillDataTable(tblDistReport)
                Me.m_ViewDistReport = tblDistReport.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistReport
        End Function
        Public ReadOnly Property ViewDistReport() As DataView
            Get
                Return Me.m_ViewDistReport
            End Get
        End Property
        Public ReadOnly Property ViewPODisPro() As DataView
            Get
                Return Me.m_ViewPODispro
            End Get
        End Property
        Public ReadOnly Property ViewTarget() As DataView
            Get
                Return Me.m_View_Target
            End Get
        End Property
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewDistReport) Then
                Me.m_ViewDistReport.Dispose()
                Me.m_ViewDistReport = Nothing
            End If
            If Not IsNothing(Me.m_ViewPODispro) Then
                Me.m_ViewPODispro.Dispose()
                Me.m_ViewPODispro = Nothing
            End If
            If Not IsNothing(Me.m_View_Target) Then
                Me.m_View_Target.Dispose()
                Me.m_View_Target = Nothing
            End If
        End Sub

    End Class
End Namespace

