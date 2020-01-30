Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Xml
Public Class frmCrystalReport
    'Private SFM As StateFillingMCB
    Private clsPO As NufarmBussinesRules.PurchaseOrder.PORegistering
    Private connInfo As ConnectionInfo
    Private ReportDoc As ReportDocument
    Private Report As GroupBy
    Private varDistributorID As String = "@DISTRIBUTOR_ID"
    Private varStartDate As String = "@START_DATE"
    Private varEndDate As String = "@END_DATE"
    Private Enum GroupBy
        Distributor
        Brand
    End Enum
    Friend Sub InitializeData()
        Me.connInfo = New ConnectionInfo()
        Me.ConfigureCrystalReports()
        Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PORegistering()
        Me.clsPO.CreateViewDistributorPO()
        Me.BindMulticolumnCombo(Me.mcbDistributor, Me.clsPO.ViewDistributor(), "")

    End Sub
    Private Sub SetDBLogonForReport(ByVal myConnectionInfo As ConnectionInfo)
        Dim myTableLogOnInfos As TableLogOnInfos = Me.CrystalReportViewer1.LogOnInfo()
        For Each myTableLogOnInfo As TableLogOnInfo In myTableLogOnInfos
            myTableLogOnInfo.ConnectionInfo = myConnectionInfo

        Next
    End Sub
    
    Private Sub SetCurrentValuesForParameterField(ByVal myReportDocument As ReportDocument, ByVal myArrayList As ArrayList, ByVal varName As String)
        Dim currentParameterValues As ParameterValues = New ParameterValues()
        Dim submittedValue As Object
        For Each submittedValue In myArrayList
            Dim myParameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
            myParameterDiscreteValue.Value = submittedValue.ToString()
            currentParameterValues.Add(myParameterDiscreteValue)
        Next

        Dim myParameterFieldDefinitions As ParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields
        Dim myParameterFieldDefinition As ParameterFieldDefinition = myParameterFieldDefinitions(varName)
        myParameterFieldDefinition.ApplyCurrentValues(currentParameterValues)
    End Sub
    Private Sub set_parameterDiscretValues(ByVal rpt As GroupBy)
        Dim myArrayList As ArrayList = Nothing
        If rpt = GroupBy.Distributor Then
            myArrayList = New ArrayList()
            If Me.mcbDistributor.SelectedItem Is Nothing Then
                'myArrayList.Add(DBNull.Value)
            Else
                myArrayList.Add(Me.mcbDistributor.Value)
            End If

            Me.SetCurrentValuesForParameterField(Me.ReportDoc, myArrayList, Me.varDistributorID)
        End If
        If Not IsNothing(myArrayList) Then
            myArrayList.Clear()
        Else
            myArrayList = New ArrayList()
        End If
        myArrayList.Add(Me.dtPicFrom.Value.ToShortDateString())
        Me.SetCurrentValuesForParameterField(Me.ReportDoc, myArrayList, Me.varStartDate)
        myArrayList.Clear()
        myArrayList.Add(Me.dtPicUntil.Value.ToShortDateString())
        Me.SetCurrentValuesForParameterField(Me.ReportDoc, myArrayList, Me.varEndDate)
            ''CR Variables
            'Dim crReportDocumentPath As String
            'Dim crParameterFields As ParameterFields = Nothing
            'Dim crParameterField As ParameterField
            'Dim crParameterDiscreteValue As ParameterDiscreteValue
            'Dim crParameterValue As ParameterValue

            ''Report Variables
            'Dim DistributorID As Object = DBNull.Value
            'Dim START_DATE As Object = CDate(Me.dtPicFrom.Value.ToShortDateString())
            'Dim END_DATE As Object = CDate(Me.dtPicUntil.Value.ToShortDateString())
            'Dim iA As Int16

            ''Get full path of the report to open
            'crReportDocumentPath = "\\SERVER\Letters\Letter Template.rpt"


            ''Create a new instance of a discrete parameter object to set the value for the parameter.
            'If rpt = GroupBy.Distributor Then
            '    crParameterDiscreteValue = New ParameterDiscreteValue()
            '    If Me.mcbDistributor.SelectedItem Is Nothing Then
            '        crParameterDiscreteValue.Value = DBNull.Value
            '    Else
            '        crParameterDiscreteValue.Value = Me.mcbDistributor.Value
            '    End If

            '    ''Define the parameter field to pass the parameter values to.
            '    crParameterField = New ParameterField()
            '    crParameterField.ParameterFieldName = "@DISTRIBUTOR_ID"
            '    ''Pass the first value to the discrete parameter
            '    crParameterField.CurrentValues.Add(crParameterDiscreteValue)

            '    ''Destroy the current instance of the discrete value
            '    crParameterDiscreteValue = Nothing

            '    ''Create an instance of the parameter fields collection, and
            '    ''pass the discrete parameter with the two discrete values to the
            '    ''collection of parameter fields.
            '    crParameterFields = New ParameterFields()
            '    crParameterFields.Add(crParameterField)

            '    ''Destroy the current instance of the parameter field
            '    crParameterField = Nothing
            'End If


            ' ''Create a new instance of a discrete parameter object to set the
            ' '' value for the parameter.
            'crParameterDiscreteValue = New ParameterDiscreteValue()
            'crParameterDiscreteValue.Value = CDate(Me.dtPicFrom.Value.ToShortDateString())

            ' ''Define the parameter field to pass the parameter values to.
            'crParameterField = New ParameterField()
            'crParameterField.ParameterFieldName = "@START_DATE"
            ' ''Pass the first value to the discrete parameter
            'crParameterField.CurrentValues.Add(crParameterDiscreteValue)

            ' ''Destroy the current instance of the discrete value
            'crParameterDiscreteValue = Nothing

            ''Add to collection of parameter fields

            'crParameterFields.Add(crParameterField)

            ' ''Destroy the current instance of the parameter field
            'crParameterField = Nothing
            'If IsNothing(crParameterFields) Then
            '    crParameterFields = New ParameterFields()
            'End If
            ''Add to collection of parameter fields
            'crParameterFields.Add(crParameterField)

            ' ''Destroy the current instance of the parameter field
            'crParameterField = Nothing

            ' ''Create a new instance of a discrete parameter object to set the
            ' '' value for the parameter.
            'crParameterDiscreteValue = New ParameterDiscreteValue()
            'crParameterDiscreteValue.Value = CDate(Me.dtPicUntil.Value.ToShortDateString())

            ' ''Define the parameter field to pass the parameter values to.
            'crParameterField = New ParameterField()
            'crParameterField.ParameterFieldName = "@END_DATE"
            ' ''Pass the first value to the discrete parameter
            'crParameterField.CurrentValues.Add(crParameterDiscreteValue)

            ' ''Destroy the current instance of the discrete value
            'crParameterDiscreteValue = Nothing

            ''Add to collection of parameter fields
            'crParameterFields.Add(crParameterField)

            ' ''Destroy the current instance of the parameter field
            'crParameterField = Nothing

            ' ''The collection of parameter fields must be set to the viewer
            'CrystalReportViewer1.ParameterFieldInfo = crParameterFields

    End Sub

    Private Sub BindMulticolumnCombo(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal dtView As DataView, ByVal rowFilter As String)
        If IsNothing(dtView) Then
            Me.mcbDistributor.SetDataBinding(dtView, "")
            Return
        End If
        dtView.RowFilter = rowfilter
        Me.mcbDistributor.SetDataBinding(dtView, "")
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
    Private Sub GetStateChecked(ByVal item As DevComponents.DotNetBar.ButtonItem)
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar1.Items
            If Not item1.Name = item.Name Then
                item1.Checked = False
            Else
                item = item1
                item.Checked = True
            End If
        Next
    End Sub
    Private Sub Bar1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor


            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnDistPODispro"
                    Me.GetStateChecked(Me.btnDistPODispro)

                    'Me.CrystalReportViewer1.ReportSource = Me.ReportDoc
                    'Me.SetDBLogonForReport(Me.connInfo)
                Case "btnDistPODisproRL"

                    Me.GetStateChecked(Me.btnDistPODisproRL)
                Case "btnPOBrandDispro"
                    Me.GetStateChecked(Me.btnPOBrandDispro)
                Case "btnPOBrandDisproRL"
                    Me.GetStateChecked(Me.btnPOBrandDisproRL)
            End Select
            Me.btnAplyFilter_Click(Me.btnAplyFilter, New EventArgs())
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar1_ItemClick")
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnDistPODispro"
                    Me.btnDistPODispro.Checked = False
                Case "btnDistPODisproRL"
                    Me.btnDistPODisproRL.Checked = False
                Case "btnPOBrandDispro"
                    Me.btnPOBrandDispro.Checked = False
                Case "btnPOBrandDisproRL"
                    Me.btnPOBrandDisproRL.Checked = False
            End Select
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Dim rowFilter As String = "DISTRIBUTOR_NAME LIKE '%" & Me.mcbDistributor.Text & "%'"
            Dim dtView As DataView = CType(Me.mcbDistributor.DataSource, DataView)
            Me.BindMulticolumnCombo(Me.mcbDistributor, dtView, rowFilter)
            Dim itemCount As Integer = Me.mcbDistributor.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub frmCrystalReport_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.connInfo) Then
                Me.connInfo = Nothing
            End If
            If Not IsNothing(Me.ReportDoc) Then
                Me.ReportDoc.Dispose()
                Me.ReportDoc = Nothing
            End If
            If Not IsNothing(Me.clsPO) Then
                Me.clsPO.Dispose(True)
                Me.clsPO = Nothing
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    
    Private Sub frmCrystalReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.dtPicFrom.Text = ""
            Me.dtPicUntil.Text = ""

        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnAplyFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyFilter.Click
        Dim btn As DevComponents.DotNetBar.ButtonItem = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor
            'Dim btn As DevComponents.DotNetBar.ButtonItem

            For Each btn In Me.Bar1.Items

                'btn = CType(item, DevComponents.DotNetBar.ButtonItem)
                If btn.Checked Then
                    'Dim myArrayList As New ArrayList()
                    Select Case btn.Name
                        Case "btnDistPODispro"
                            If (Not Me.dtPicFrom.Text = "") And (Not Me.dtPicUntil.Text = "") Then
                                If Not IsNothing(Me.ReportDoc) Then
                                    Me.ReportDoc.Dispose()
                                    Me.ReportDoc = Nothing
                                End If
                                Dim DistPODispro As New PODispro()
                                Me.ReportDoc = DistPODispro

                                Me.set_parameterDiscretValues(GroupBy.Distributor)
                                Me.CrystalReportViewer1.ReportSource = Me.ReportDoc
                                Me.SetDBLogonForReport(Me.connInfo)
                                Exit For
                            Else
                                Me.ShowMessageInfo("Date time period is needed")
                                If Me.btnDistPODispro.Checked = True Then
                                    Me.btnDistPODispro.Checked = False
                                End If
                            End If

                        Case "btnDistPODisproRL"
                            If (Not Me.dtPicFrom.Text = "") And (Not Me.dtPicUntil.Text = "") Then
                                If Not IsNothing(Me.ReportDoc) Then
                                    Me.ReportDoc.Dispose()
                                    Me.ReportDoc = Nothing
                                End If
                                Dim DistPODisproRL As New PODispro_Release_Left()
                                Me.ReportDoc = DistPODisproRL
                                Me.set_parameterDiscretValues(GroupBy.Distributor)
                                'Me.SetCurrentValuesForParameterField(Me.ReportDoc, myArrayList)
                                Me.CrystalReportViewer1.ReportSource = Me.ReportDoc
                                Me.SetDBLogonForReport(Me.connInfo)
                                Exit For
                            Else
                                Me.ShowMessageInfo("Date time period is needed")
                                If Me.btnDistPODisproRL.Checked = True Then
                                    Me.btnDistPODisproRL.Checked = False
                                End If
                            End If

                        Case "btnPOBrandDispro"
                            If (Not Me.dtPicFrom.Text = "") And (Not Me.dtPicUntil.Text = "") Then
                                If Not IsNothing(Me.ReportDoc) Then
                                    Me.ReportDoc.Dispose()
                                    Me.ReportDoc = Nothing
                                End If
                                Dim DistPODisproRL As New Po_Dispro_by_Brand()
                                Me.ReportDoc = DistPODisproRL
                                Me.set_parameterDiscretValues(GroupBy.Brand)
                                'Me.SetCurrentValuesForParameterField(Me.ReportDoc, myArrayList)
                                Me.CrystalReportViewer1.ReportSource = Me.ReportDoc
                                Me.SetDBLogonForReport(Me.connInfo)
                                Exit For
                            Else
                                Me.ShowMessageInfo("Date time period is needed")
                                If Me.btnPOBrandDispro.Checked = True Then
                                    Me.btnPOBrandDispro.Checked = False
                                End If
                            End If

                        Case "btnPOBrandDisproRL"
                            If (Not Me.dtPicFrom.Text = "") And (Not Me.dtPicUntil.Text = "") Then
                                If Not IsNothing(Me.ReportDoc) Then
                                    Me.ReportDoc.Dispose()
                                    Me.ReportDoc = Nothing
                                End If
                                Dim DistPODisproRL As New Po_Dispro_Release_Left_Brand()
                                Me.ReportDoc = DistPODisproRL
                                Me.set_parameterDiscretValues(GroupBy.Brand)
                                'Me.SetCurrentValuesForParameterField(Me.ReportDoc, myArrayList)
                                Me.CrystalReportViewer1.ReportSource = Me.ReportDoc
                                Me.SetDBLogonForReport(Me.connInfo)
                                Exit For
                            Else
                                Me.ShowMessageInfo("Date time period is needed")
                                If Me.btnPOBrandDisproRL.Checked = True Then
                                    Me.btnPOBrandDisproRL.Checked = False
                                End If
                            End If

                    End Select
                End If
            Next
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnAplyFilter_Click")
            Select Case btn.Name
                Case "btnDistPODispro"
                    If Me.btnDistPODispro.Checked = True Then
                        Me.btnDistPODispro.Checked = False
                    End If
                Case "btnDistPODisproRL"
                    If Me.btnDistPODisproRL.Checked = True Then
                        Me.btnDistPODisproRL.Checked = False
                    End If
                Case "btnPOBrandDispro"
                    If Me.btnPOBrandDispro.Checked = True Then
                        Me.btnPOBrandDispro.Checked = False
                    End If
                Case "btnPOBrandDisproRL"
                    If Me.btnPOBrandDisproRL.Checked = True Then
                        Me.btnPOBrandDisproRL.Checked = False
                    End If
            End Select
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
