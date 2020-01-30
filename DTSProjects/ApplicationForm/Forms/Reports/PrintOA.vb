Imports System.Collections
Imports CrystalDecisions.Shared
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Configuration
Public Class PrintOA
    Private connInfo As ConnectionInfo
    Private ReportDoc As ReportDocument
    Private Const PARAMETER_FIELD_NAME As String = "@OA_REF_NO"
    Private Sub SetCurrentValuesForParameterField(ByVal myParameterFields As ParameterFields, ByVal myArrayList As ArrayList)
        Dim currentParameterValues As ParameterValues = New ParameterValues()
        For Each submittedValue As Object In myArrayList
            Dim myParameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
            myParameterDiscreteValue.Value = submittedValue.ToString()
            'myParameterDiscreteValue.Value = submittedValue.ToString()
            currentParameterValues.Add(myParameterDiscreteValue)
        Next
        Dim myParameterField As ParameterField = myParameterFields(PARAMETER_FIELD_NAME)
        myParameterField.CurrentValues = currentParameterValues

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
    'Private Sub SetDateRangeForOrders(ByVal myReportDocument As ReportDocument, ByVal startDate As String, ByVal endDate As String)
    '    Dim myParameterRangeValue As ParameterRangeValue = New ParameterRangeValue()
    '    myParameterRangeValue.StartValue = startDate
    '    myParameterRangeValue.EndValue = endDate
    '    myParameterRangeValue.LowerBoundType = RangeBoundType.BoundInclusive
    '    myParameterRangeValue.UpperBoundType = RangeBoundType.BoundInclusive

    '    Dim myParameterFieldDefinitions As ParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields
    '    Dim myParameterFieldDefinition As ParameterFieldDefinition = myParameterFieldDefinitions(PARAMETER_FIELD_NAME)
    '    myParameterFieldDefinition.CurrentValues.Add(myParameterRangeValue)
    'End Sub
    Friend Sub InitializeData(ByVal OA_ID As String, ByVal PODate As Date)
        'Me.crvOA.SelectionFormula = ""
        'Me.crvOA.ReportSource = Nothing
        'Me.crvOA.RefreshReport()
        'Me.connInfo = New ConnectionInfo()

        Dim OARep As Object = Nothing
        'If PODate >= New Date(2019, 3, 1) Then
        '    OARep = New DOC()
        'Else
        '    OARep = New OA_Per_ID()
        'End If
        Dim frmConfirm As New OCReportType()
        If frmConfirm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            If frmConfirm.rdbNewReport.Checked Then
                OARep = New DOC()
            ElseIf frmConfirm.rdbOldReport.Checked Then
                OARep = New OA_Per_ID()
            End If
        Else
            Me.ShowMessageInfo("Please define report type")
            Return
        End If
        If OARep Is Nothing Then
            Me.ShowMessageInfo("Please define report type")
            Return
        End If
        Me.ConfigureCrystalReports()
        Me.ReportDoc = OARep
        Dim myArrayList As ArrayList = New ArrayList()
        myArrayList.Add(OA_ID)

        Me.SetCurrentValuesForParameterField(Me.ReportDoc, myArrayList)
        Me.crvOA.ReportSource = Me.ReportDoc

        Me.SetDBLogonForReport(Me.connInfo)

        'Me.crvOA.RefreshReport()
        'Dim myParameterFields As ParameterFields = Me.crvOA.ParameterFieldInfo()

        'Me.crvOA.ReportSource = Me.ReportDoc
        'Me.crvOA.RefreshReport()
    End Sub
    Private Sub SetDBLogonForReport(ByVal myConnectionInfo As ConnectionInfo)
        Dim myTableLogOnInfos As TableLogOnInfos = Me.crvOA.LogOnInfo()
        For Each myTableLogOnInfo As TableLogOnInfo In myTableLogOnInfos
            myTableLogOnInfo.ConnectionInfo = myConnectionInfo
        Next
    End Sub
    Private Sub SetCurrentValuesForParameterField(ByVal myReportDocument As ReportDocument, ByVal myArrayList As ArrayList)
        Dim currentParameterValues As ParameterValues = New ParameterValues()
        Dim submittedValue As Object
        For Each submittedValue In myArrayList
            Dim myParameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
            myParameterDiscreteValue.Value = submittedValue.ToString()
            currentParameterValues.Add(myParameterDiscreteValue)
        Next

        Dim myParameterFieldDefinitions As ParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields
        Dim myParameterFieldDefinition As ParameterFieldDefinition = myParameterFieldDefinitions(PARAMETER_FIELD_NAME)
        myParameterFieldDefinition.ApplyCurrentValues(currentParameterValues)
    End Sub
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
        'ConfigurationManager.ConnectionStrings("NufarmConstring").
        'Me.connInfo.IntegratedSecurity = True
        'SetDBLogonForReport(myConnectionInfo, northwindCustomersReport)

        'myCrystalReportViewer.ReportSource = northwindCustomersReport
    End Sub

 
    Private Sub PrintOA_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.ReportDoc) Then
                Me.ReportDoc.Dispose()
                Me.ReportDoc = Nothing
            End If
            If Not IsNothing(Me.connInfo) Then
                Me.connInfo = Nothing
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
