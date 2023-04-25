Imports System.Collections
Imports CrystalDecisions.Shared
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Configuration
Public Class FrmGonReport
    Private connInfo As ConnectionInfo
    Friend ReportDoc As ReportDocument
    Friend IsImmediatePrint As Boolean = False
    'Const ParDefName_GON_TYPE As String = "@GON_TYPE"
    'Const ParDefName_GON_NUMBER As String = "@GON_NUMBER"
    'Friend Sub InitializeData(ByVal GON_TYPE As String, ByVal GON_NUMBER As String)
    '    'Me.crvOA.SelectionFormula = ""
    '    'Me.crvOA.ReportSource = Nothing
    '    'Me.crvOA.RefreshReport()
    '    'Me.connInfo = New ConnectionInfo()

    '    Dim GONDoc As Object = Nothing
    '    Me.ConfigureCrystalReports()

    '    Me.SetCurrentValuesForParameterField(Me.ReportDoc, ParDefName_GON_TYPE, GON_TYPE)
    '    Me.SetCurrentValuesForParameterField(Me.ReportDoc, ParDefName_GON_NUMBER, GON_NUMBER)
    '    Me.crvGON.ReportSource = Me.ReportDoc

    '    Me.SetDBLogonForReport(Me.connInfo, Me.ReportDoc)

    '    'Me.crvOA.RefreshReport()
    '    'Dim myParameterFields As ParameterFields = Me.crvOA.ParameterFieldInfo()

    '    'Me.crvOA.ReportSource = Me.ReportDoc
    '    'Me.crvOA.RefreshReport()
    'End Sub
    'Private Sub SetDBLogonForReport(ByVal myConnectionInfo As ConnectionInfo, ByVal myReportDocument As ReportDocument)
    '    Dim myTables As Tables = myReportDocument.Database.Tables
    '    Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
    '    For Each myTable In myTables
    '        Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
    '        myTableLogonInfo.ConnectionInfo = myConnectionInfo
    '        myTable.ApplyLogOnInfo(myTableLogonInfo)
    '    Next
    'End Sub
    'Private Sub SetCurrentValuesForParameterField(ByVal myReportDocument As ReportDocument, ByVal ParName As String, ByVal ValParameter As Object)
    '    Dim currentParameterValues As ParameterValues = New ParameterValues()
    '    'Dim submittedValue As Object
    '    'For Each submittedValue In myArrayList
    '    '    Dim myParameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
    '    '    myParameterDiscreteValue.Value = submittedValue.ToString()
    '    '    currentParameterValues.Add(myParameterDiscreteValue)
    '    'Next
    '    Dim myParameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
    '    myParameterDiscreteValue.Value = ValParameter

    '    currentParameterValues.Add(myParameterDiscreteValue)

    '    Dim myParameterFieldDefinitions As ParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields
    '    Dim myParamDefinitionGon As ParameterFieldDefinition = myParameterFieldDefinitions(ParName)
    '    myParamDefinitionGon.ApplyCurrentValues(currentParameterValues)
    'End Sub
    'Private Sub ConfigureCrystalReports()
    '    Dim xdoc As Xml.XmlDocument = New System.Xml.XmlDocument()
    '    Dim path As String = Application.StartupPath() & "\" & "str.xml"
    '    xdoc.Load(path)
    '    Dim xNodeList As XmlNodeList = xdoc.SelectNodes("descendant::Con/@Server")
    '    Dim strServer As String = xNodeList(0).Value.ToString()
    '    xNodeList = xdoc.SelectNodes("descendant::Con/@Database")
    '    Dim strDB As String = xNodeList(0).Value.ToString()
    '    xNodeList = xdoc.SelectNodes("descendant::Usr/@UserID")
    '    Dim strUID As String = xNodeList.Item(0).Value.ToString()
    '    xNodeList = xdoc.SelectNodes("descendant::Usr/@Password")
    '    Dim strPass As String = xNodeList.Item(0).Value.ToString()
    '    Me.connInfo = New ConnectionInfo()
    '    Me.connInfo.ServerName = strServer
    '    Me.connInfo.DatabaseName = strDB
    '    Me.connInfo.UserID = strUID
    '    Me.connInfo.Password = strPass
    '    'ConfigurationManager.ConnectionStrings("NufarmConstring").
    '    'Me.connInfo.IntegratedSecurity = True
    '    'SetDBLogonForReport(myConnectionInfo, northwindCustomersReport)

    '    'myCrystalReportViewer.ReportSource = northwindCustomersReport
    'End Sub

    Private Sub FrmGonReport_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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

    Private Sub FrmGonReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If IsImmediatePrint Then
        '    Me.crvGON.PrintReport()
        '    System.Threading.Thread.Sleep(200)
        '    Me.Close()
        'End If
    End Sub
End Class
