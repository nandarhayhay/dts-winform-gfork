Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Xml
Public Class frmReport
    'private OARep as DTSProjects
    Private SFM As StateFillingMCB
    Private clsPO As NufarmBussinesRules.PurchaseOrder.PORegistering
    Private connInfo As ConnectionInfo
    Private ReportDoc As ReportDocument
    Friend Sub InitializeData()
        Me.connInfo = New ConnectionInfo()
        Me.ConfigureCrystalReports()
        Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PORegistering()
        Me.clsPO.CreateViewDistributorPO()
        Me.BindMulticolumnCombo(Me.mcbDistributor, Me.clsPO.ViewDistributor(), "")

    End Sub
    Private Enum StateFillingMCB
        Filling
        HasFilled
    End Enum
    Private Sub BindMulticolumnCombo(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal dtview As DataView, ByVal rowFilter As String)
        Me.SFM = StateFillingMCB.Filling
        dtview.RowFilter = rowFilter
        mcb.SetDataBinding(dtview, "")
        Me.SFM = StateFillingMCB.HasFilled
    End Sub
    'Private connInfo As ConnectionInfo
    'Private ReportDoc As ReportDocument
    'Private ParFromOADate As String = "@FROM_OADATE"
    'Private ParUntilOADate As String = "@UNTIL_OADATE"
    'Private ParFromPODate As String = "@FROM_PODATE"
    'Private ParUntilPODate As String = "@UNTIL_PODATE"
    Private Sub LoadReportOA()
        'me.CrystalReportViewer1.
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.ReportSource = Nothing
        Me.CrystalReportViewer1.Refresh()
        Me.ReportDoc = New OAReport()
        Me.SetDBLogonForReport(Me.connInfo, Me.ReportDoc)
        Me.CrystalReportViewer1.ReportSource = Me.ReportDoc
        If Me.rdbOA.Checked = True Then
            Me.CrystalReportViewer1.SelectionFormula = "{Usp_Create_View_OA_Report;1.OA_DATE}  >= DateTime (" & Me.dtPicFrom.Value.Year.ToString() & "," & _
            Me.dtPicFrom.Value.Month.ToString() & "," & Me.dtPicFrom.Value.Day.ToString() & ",00,00,00) and {Usp_Create_View_OA_Report;1.OA_DATE} <= DateTime (" & Me.dtPicUntil.Value.Year.ToString() & "," & _
            Me.dtPicUntil.Value.Month.ToString() & "," & Me.dtPicUntil.Value.Day.ToString() & ",00,00,00)"
            'Me.SetCurrentValuesForParameterField(Me.ReportDoc, DBNull.Value, DBNull.Value, CObj(Me.dtPicFrom.Value.ToShortDateString()), CObj(Me.dtPicUntil.Value.ToShortDateString()))
            'Me.CrystalReportViewer1.RefreshReport()
        ElseIf Me.rdbPO.Checked = True Then
            Me.CrystalReportViewer1.SelectionFormula = "{Usp_Create_View_OA_Report;1.PO_REF_DATE} >= DateTime (" & Me.dtPicFrom.Value.Year.ToString() & "," & _
            Me.dtPicFrom.Value.Month.ToString() & "," & Me.dtPicFrom.Value.Day.ToString() & ",00,00,00) and {Usp_Create_View_OA_Report;1.PO_REF_DATE} <= DateTime (" & Me.dtPicUntil.Value.Year.ToString() & "," & _
            Me.dtPicUntil.Value.Month.ToString() & "," & Me.dtPicUntil.Value.Day.ToString() & ",00,00,00)"
            'Me.SetCurrentValuesForParameterField(Me.ReportDoc, CObj(Me.dtPicFrom.Value.ToShortDateString()), CObj(Me.dtPicUntil.Value.ToShortDateString()), DBNull.Value, DBNull.Value)
            'Me.CrystalReportViewer1.RefreshReport()
        End If
        If (Not IsNothing(Me.mcbDistributor.SelectedItem)) And (Not IsNothing(Me.mcbBrandPack.SelectedItem)) Then
            Me.CrystalReportViewer1.SelectionFormula &= " and {Usp_Create_View_OA_Report;1.DISTRIBUTOR_ID} = '" & Me.mcbDistributor.Value.ToString() & _
            "' and {Usp_Create_View_OA_Report;1.BRANDPACK_NAME} = '" & Me.mcbBrandPack.Text.ToString() & "'" 'ABAMECTIN 19.5 EC @ 100 ML - D""
        ElseIf Not IsNothing(Me.mcbDistributor.SelectedItem) Then
            Me.CrystalReportViewer1.SelectionFormula &= " and  {Usp_Create_View_OA_Report;1.DISTRIBUTOR_ID} = '" & Me.mcbDistributor.Value.ToString() & "'"
        ElseIf Not IsNothing(Me.mcbBrandPack.SelectedItem) Then
            Me.CrystalReportViewer1.SelectionFormula &= " and {Usp_Create_View_OA_Report;1.BRANDPACK_NAME} = '" & Me.mcbBrandPack.Text.ToString() & "'" 'ABAMECTIN 19.5 EC @ 100 ML - D""
        End If
        'Me.CrystalReportViewer1.ReportSource = Me.ReportDoc
        Me.CrystalReportViewer1.RefreshReport()
    End Sub
    Private Sub LoadReportDistributor()
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.ReportSource = Nothing
        Me.CrystalReportViewer1.Refresh()
        Me.ReportDoc = New Dist_Report()
        Me.SetDBLogonForReport(Me.connInfo, Me.ReportDoc)
        Me.CrystalReportViewer1.ReportSource = Me.ReportDoc
        If Me.rdbOA.Checked = True Then
            Me.CrystalReportViewer1.SelectionFormula = "{Usp_Create_View_Distributor_Report;1.OA_DATE} >= DateTime (" & Me.dtPicFrom.Value.Year.ToString() & "," & _
            Me.dtPicFrom.Value.Month.ToString() & "," & Me.dtPicFrom.Value.Day.ToString() & ",00,00,00) and {Usp_Create_View_Distributor_Report;1.OA_DATE} <= DateTime (" & Me.dtPicUntil.Value.Year.ToString() & "," & _
            Me.dtPicUntil.Value.Month.ToString() & "," & Me.dtPicUntil.Value.Day.ToString() & ",00,00,00)"
            'Me.SetCurrentValuesForParameterField(Me.ReportDoc, CObj(Me.dtPicFrom.Value.ToShortDateString()), CObj(Me.dtPicUntil.Value.ToShortDateString()), DBNull.Value, DBNull.Value)
            'Me.CrystalReportViewer1.RefreshReport()
        ElseIf Me.rdbPO.Checked = True Then
            Me.CrystalReportViewer1.SelectionFormula = "{Usp_Create_View_Distributor_Report;1.PO_REF_DATE} >= DateTime (" & Me.dtPicFrom.Value.Year.ToString() & "," & _
            Me.dtPicFrom.Value.Month.ToString() & "," & Me.dtPicFrom.Value.Day.ToString() & ",00,00,00) and {Usp_Create_View_Distributor_Report;1.PO_REF_DATE} <= DateTime (" & Me.dtPicUntil.Value.Year.ToString() & "," & _
            Me.dtPicUntil.Value.Month.ToString() & "," & Me.dtPicUntil.Value.Day.ToString() & ",00,00,00)"
            'Me.SetCurrentValuesForParameterField(Me.ReportDoc, DBNull.Value, DBNull.Value, CObj(Me.dtPicFrom.Value.ToShortDateString()), CObj(Me.dtPicUntil.Value.ToShortDateString()))
            'Me.CrystalReportViewer1.RefreshReport()
        End If

        If (Not IsNothing(Me.mcbDistributor.SelectedItem)) And (Not IsNothing(Me.mcbBrandPack.SelectedItem)) Then
            Me.CrystalReportViewer1.SelectionFormula &= " and {Usp_Create_View_Distributor_Report;1.DISTRIBUTOR_ID} = '" & Me.mcbDistributor.Value.ToString() _
            & "' and {Usp_Create_View_Distributor_Report;1.BRANDPACK_NAME} = '" & Me.mcbBrandPack.Text & "'" 'ABAMECTIN 19.5 EC @ 100 ML - D"
        ElseIf Not IsNothing(Me.mcbDistributor.SelectedItem) Then
            Me.CrystalReportViewer1.SelectionFormula &= " and {Usp_Create_View_Distributor_Report;1.DISTRIBUTOR_ID} = '" & Me.mcbDistributor.Value.ToString() & "'"
        ElseIf Not IsNothing(Me.mcbBrandPack.SelectedItem) Then
            Me.CrystalReportViewer1.SelectionFormula &= " and {Usp_Create_View_Distributor_Report;1.BRANDPACK_NAME} = '" & Me.mcbBrandPack.Text & "'" 'ABAMECTIN 19.5 EC @ 100 ML - D""
        End If

        Me.CrystalReportViewer1.RefreshReport()
    End Sub
    'Private Sub SetCurrentValuesForParameterField(ByVal myReportDocument As ReportDocument, ByVal ParFromOADate As Object _
    ', ByVal ParUntilOADate As Object, ByVal ParFromPODate As Object, ByVal ParUntilPODate As Object)
    '    Dim myParameterFieldDefinitions As ParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields

    '    Dim pvCollection As ParameterValues = New ParameterValues()
    '    pvCollection.Clear()
    '    Dim myParameterDiscreteValue1 As ParameterDiscreteValue = New ParameterDiscreteValue()
    '    myParameterDiscreteValue1.Value = ParFromOADate
    '    pvCollection.Add(myParameterDiscreteValue1)
    '    'myParameterFieldDefinitions(me.ParFromPODate).va

    '    'Dim myParameterFieldDefinition1 As ParameterFieldDefinition = myParameterFieldDefinitions(Me.ParFromOADate)
    '    myParameterFieldDefinitions(Me.ParFromOADate).EnableNullValue = True
    '    myParameterFieldDefinitions(Me.ParFromOADate).ApplyCurrentValues(pvCollection)
    '    'myParameterFieldDefinition1.ParameterValueKind = ParameterValueKind.DateTimeParameter
    '    'myParameterFieldDefinition1.PromptText = ParFromOADate.ToString()
    '    'myParameterFieldDefinition1.ApplyCurrentValues(ParFromOADateValues)
    '    pvCollection.Clear()

    '    'Dim ParUntilOADateValues As New ParameterValues()
    '    Dim myParameterDiscreteValue2 As ParameterDiscreteValue = New ParameterDiscreteValue()
    '    myParameterDiscreteValue2.Value = ParUntilOADate.ToString()
    '    pvCollection.Add(myParameterDiscreteValue2)
    '    myParameterFieldDefinitions(Me.ParUntilOADate).EnableNullValue = True
    '    myParameterFieldDefinitions(Me.ParUntilOADate).ApplyCurrentValues(pvCollection)
    '    'myParameterFieldDefinition2.PromptText = ParUntilOADate.ToString()
    '    'myParameterFieldDefinition2.ApplyCurrentValues(ParUntilOADateValues)

    '    pvCollection.Clear()

    '    'Dim ParFromPODateValues As New ParameterValues()
    '    Dim myParameterDiscreteValue3 As ParameterDiscreteValue = New ParameterDiscreteValue()
    '    myParameterDiscreteValue3.Value = ParFromPODate
    '    pvCollection.Add(myParameterDiscreteValue3)
    '    myParameterFieldDefinitions(Me.ParFromPODate).EnableNullValue = True
    '    myParameterFieldDefinitions(Me.ParFromPODate).ApplyCurrentValues(pvCollection)
    '    'myParameterFieldDefinition3.PromptText = ""
    '    'myParameterFieldDefinition3.ApplyCurrentValues(ParFromPODateValues)

    '    pvCollection.Clear()
    '    'Dim ParUntilPODateValues As New ParameterValues()
    '    Dim myParameterDiscreteValue4 As ParameterDiscreteValue = New ParameterDiscreteValue()
    '    myParameterDiscreteValue4.Value = ParUntilPODate
    '    pvCollection.Add(myParameterDiscreteValue4)
    '    myParameterFieldDefinitions(Me.ParUntilPODate).EnableNullValue = True
    '    myParameterFieldDefinitions(Me.ParUntilPODate).ApplyCurrentValues(pvCollection)
    '    'myParameterFieldDefinition4.PromptText = ""
    '    'myParameterFieldDefinition4.ApplyCurrentValues(ParUntilPODateValues)
    '    ''dim Par
    'End Sub
    Private Sub ConfigureCrystalReports()
        Dim xdoc As Xml.XmlDocument = New System.Xml.XmlDocument()
        Dim path As String = Application.StartupPath() & "\" & "str.xml"
        xdoc.Load(path)
        Dim xNodeList As XmlNodeList = xdoc.SelectNodes("descendant::Con/@Server")
        Dim strServer As String = xNodeList(0).Value.ToString()
        xNodeList = xdoc.SelectNodes("descendant::Con/@Database")
        Dim strDB As String = xNodeList(0).Value.ToString()
        xNodeList = xdoc.SelectNodes("descendant::Usr/@UserID")
        Dim strUID As String = xNodeList.Item(0).Value.ToString()
        xNodeList = xdoc.SelectNodes("descendant::Usr/@Password")
        Dim strPass As String = xNodeList.Item(0).Value.ToString()
        Me.connInfo = New ConnectionInfo()
        Me.connInfo.ServerName = strServer
        Me.connInfo.DatabaseName = strDB
        Me.connInfo.UserID = strUID
        Me.connInfo.Password = strPass
        'Me.connInfo.IntegratedSecurity = True
        'SetDBLogonForReport(myConnectionInfo, northwindCustomersReport)

        'myCrystalReportViewer.ReportSource = northwindCustomersReport
    End Sub

    Private Sub SetDBLogonForReport(ByVal myConnectionInfo As ConnectionInfo, ByVal myReportDocument As ReportDocument)
        Dim myTables As Tables = myReportDocument.Database.Tables
        Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
        For Each myTable In myTables
            Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
            myTableLogonInfo.ConnectionInfo = myConnectionInfo
            myTable.ApplyLogOnInfo(myTableLogonInfo)
        Next
    End Sub
 

    Private Sub rdbOAReport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbOAReport.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbOAReport.Checked = True Then
                Me.LoadReportOA()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbReportOA_CheckedChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbDistributor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDistributor.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbDistributor.Checked = True Then
                Me.LoadReportDistributor()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbDistributor_CheckedChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mcbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mcbDistributor.Value Is Nothing Then
                Return
            End If
            If (Me.rdbDistributor.Checked = False) And (Me.rdbOAReport.Checked = False) Then
                Me.ShowMessageInfo("Please define report type")
                Me.mcbDistributor.Value = Nothing
                Return
            End If
            If Me.mcbDistributor.SelectedItem Is Nothing Then
                Return
            End If
            Me.clsPO.CreateViewBrandPackByDistributor(Me.mcbDistributor.Value.ToString())
            Me.BindMulticolumnCombo(Me.mcbBrandPack, Me.clsPO.ViewPOBrandPack(), "")
            Me.mcbBrandPack.DropDownList().Columns("BRANDPACK_ID").AutoSize()
            Me.mcbBrandPack.DropDownList().Columns("BRANDPACK_NAME").AutoSize()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbDistributor_ValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub mcbBrandPack_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbBrandPack.DropDown
    '    Try
    '        If Me.mcbDistributor.SelectedItem Is Nothing Then
    '            Me.ShowMessageInfo("Please define distributor")
    '            Me.mcbBrandPack.Value = Nothing
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub btnApllyFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApllyFilter.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbOAReport.Checked = True Then
                Me.LoadReportOA()
            ElseIf Me.rdbDistributor.Checked = True Then
                Me.LoadReportDistributor()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnApplyRange_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.CrystalReportViewer1.SelectionFormula = ""
            Me.CrystalReportViewer1.ReportSource = Nothing
            Me.CrystalReportViewer1.Refresh()
            Me.dtPicFrom.Value = NufarmBussinesRules.SharedClass.ServerDate()
            Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate()
            If Me.rdbDistributor.Checked = True Then
                Me.ReportDoc = New Dist_Report()
                Me.SetDBLogonForReport(Me.connInfo, Me.ReportDoc)
                Me.CrystalReportViewer1.ReportSource = Me.ReportDoc
                If Me.rdbOA.Checked = True Then
                    Me.CrystalReportViewer1.SelectionFormula = "{Usp_Create_View_Distributor_Report;1.OA_DATE} >= DateTime (" & Me.dtPicFrom.Value.Year.ToString() & "," & _
                    Me.dtPicFrom.Value.Month.ToString() & "," & Me.dtPicFrom.Value.Day.ToString() & ",00,00,00) and {Usp_Create_View_Distributor_Report;1.OA_DATE} <= DateTime (" & Me.dtPicUntil.Value.Year.ToString() & "," & _
                    Me.dtPicUntil.Value.Month.ToString() & "," & Me.dtPicUntil.Value.Day.ToString() & ",00,00,00)"
                    'Me.SetCurrentValuesForParameterField(Me.ReportDoc, CObj(Me.dtPicFrom.Value.ToShortDateString()), CObj(Me.dtPicUntil.Value.ToShortDateString()), DBNull.Value, DBNull.Value)
                    'Me.CrystalReportViewer1.RefreshReport()
                ElseIf Me.rdbPO.Checked = True Then
                    Me.CrystalReportViewer1.SelectionFormula = "{Usp_Create_View_Distributor_Report;1.PO_REF_DATE} >= DateTime (" & Me.dtPicFrom.Value.Year.ToString() & "," & _
                    Me.dtPicFrom.Value.Month.ToString() & "," & Me.dtPicFrom.Value.Day.ToString() & ",00,00,00) and {Usp_Create_View_Distributor_Report;1.PO_REF_DATE} <= DateTime (" & Me.dtPicUntil.Value.Year.ToString() & "," & _
                    Me.dtPicUntil.Value.Month.ToString() & "," & Me.dtPicUntil.Value.Day.ToString() & ",00,00,00)"
                    'Me.SetCurrentValuesForParameterField(Me.ReportDoc, DBNull.Value, DBNull.Value, CObj(Me.dtPicFrom.Value.ToShortDateString()), CObj(Me.dtPicUntil.Value.ToShortDateString()))
                    'Me.CrystalReportViewer1.RefreshReport()

                End If
                Me.CrystalReportViewer1.RefreshReport()
            ElseIf Me.rdbOAReport.Checked = True Then
                Me.ReportDoc = New OAReport()
                Me.SetDBLogonForReport(Me.connInfo, Me.ReportDoc)
                Me.CrystalReportViewer1.ReportSource = Me.ReportDoc
                If Me.rdbOA.Checked = True Then
                    Me.CrystalReportViewer1.SelectionFormula = "{Usp_Create_View_OA_Report;1.OA_DATE}  >= DateTime (" & Me.dtPicFrom.Value.Year.ToString() & "," & _
                    Me.dtPicFrom.Value.Month.ToString() & "," & Me.dtPicFrom.Value.Day.ToString() & ",00,00,00) and {Usp_Create_View_OA_Report;1.OA_DATE} <= DateTime (" & Me.dtPicUntil.Value.Year.ToString() & "," & _
                    Me.dtPicUntil.Value.Month.ToString() & "," & Me.dtPicUntil.Value.Day.ToString() & ",00,00,00)"
                    'Me.SetCurrentValuesForParameterField(Me.ReportDoc, DBNull.Value, DBNull.Value, CObj(Me.dtPicFrom.Value.ToShortDateString()), CObj(Me.dtPicUntil.Value.ToShortDateString()))
                    'Me.CrystalReportViewer1.RefreshReport()
                ElseIf Me.rdbPO.Checked = True Then
                    Me.CrystalReportViewer1.SelectionFormula = "{Usp_Create_View_OA_Report;1.PO_REF_DATE} >= DateTime (" & Me.dtPicFrom.Value.Year.ToString() & "," & _
                    Me.dtPicFrom.Value.Month.ToString() & "," & Me.dtPicFrom.Value.Day.ToString() & ",00,00,00) and {Usp_Create_View_OA_Report;1.PO_REF_DATE} <= DateTime (" & Me.dtPicUntil.Value.Year.ToString() & "," & _
                    Me.dtPicUntil.Value.Month.ToString() & "," & Me.dtPicUntil.Value.Day.ToString() & ",00,00,00)"
                    'Me.SetCurrentValuesForParameterField(Me.ReportDoc, CObj(Me.dtPicFrom.Value.ToShortDateString()), CObj(Me.dtPicUntil.Value.ToShortDateString()), DBNull.Value, DBNull.Value)
                    'Me.CrystalReportViewer1.RefreshReport()
                End If
                Me.CrystalReportViewer1.RefreshReport()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnRemove_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub frmReport_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsPO) Then
                Me.clsPO.Dispose(True)
                Me.clsPO = Nothing
            End If
            Me.Dispose(True)
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub frmReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
            Me.dtPicFrom.Value = NufarmBussinesRules.SharedClass.ServerDate()
            Me.mcbDistributor.DropDownList.Columns("DISTRIBUTOR_ID").AutoSize()
            Me.mcbDistributor.DropDownList().Columns("DISTRIBUTOR_NAME").AutoSize()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_frmReport_Load")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnFilterBrandPack_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterBrandPack.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            'If Me.mcbDistributor.SelectedItem Is Nothing Then
            '    Me.ShowMessageInfo("Please define report type")
            '    Me.mcbDistributor.Value = Nothing
            '    Return
            'End If
            Dim dtview As DataView = CType(Me.mcbBrandPack.DataSource, DataView)
            'dtview.RowFilter =
            Me.BindMulticolumnCombo(Me.mcbBrandPack, dtview, "BRANDPACK_NAME LIKE '%" & Me.mcbBrandPack.Text & "%'")
            Dim itemCount As Integer = Me.mcbBrandPack.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString & " item(s) found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dtview As DataView = CType(Me.mcbDistributor.DataSource, DataView)
            Me.BindMulticolumnCombo(Me.mcbDistributor, dtview, "DISTRIBUTOR_NAME LIKE '%" & Me.mcbDistributor.Text & "%'")
            Dim itemCount As Integer = Me.mcbDistributor.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub
End Class