Imports Nufarm.Domain

Public Class Other_SMS
    Private clsDistributor As NufarmBussinesRules.DistributorRegistering.DistributorRegistering
    Private hasDistributor As Hashtable
    Friend CMain As Main
    Private DVRetailer As DataView = Nothing
    Private DVDistributor As DataView = Nothing
    Private listSendDistributors As New List(Of DistributorContact)
    Private listSendRetailers As New List(Of RetailerContact)
    Private listSendRSMTM As New List(Of RSMTM)

    Friend Sub InitializeData()
        Me.clsDistributor = New NufarmBussinesRules.DistributorRegistering.DistributorRegistering()
        Me.clsDistributor.CreateView_DistributorSMS()
        'Me.mcbDistributor.SetDataBinding(Me.clsDistributor.ViewDistributor(), "")
    End Sub
    Private Sub BindGrid(ByVal grd As Janus.Windows.GridEX.GridEX, ByVal DV As DataView)
        grd.SetDataBinding(DV, "")
        If IsNothing(DV) Then : Return : End If
        grd.RetrieveStructure()
        For Each col As Janus.Windows.GridEX.GridEXColumn In grd.RootTable.Columns
            col.AutoSize()
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            col.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        Next
    End Sub
    'Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        Me.clsDistributor.ViewDistributor().RowFilter = "DISTRIBUTOR_NAME LIKE '%" & Me.mcbDistributor.Text + "%'"
    '        Me.mcbDistributor.DropDownList().Refetch()
    '        Dim itemCount As Integer = Me.mcbDistributor.DropDownList().RecordCount()
    '        Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
    '    Catch ex As Exception

    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub
    Private Sub SendCustomer(ByVal Index As Integer, ByVal Contact As String, ByRef SMSRow As DataRow, ByRef tblSMS As DataTable)
        SMSRow = tblSMS.NewRow()
        Dim ContactMobile As String = listSendRetailers(Index).ContactMobile
        ContactMobile = ContactMobile.Replace(" ", "")
        ContactMobile = ContactMobile.Replace("-", "")
        ContactMobile = ContactMobile.Replace(".", "")
        If ContactMobile.Length > 10 Then
            If ContactMobile.StartsWith("08") Or ContactMobile.StartsWith("+628") Then
                SMSRow = tblSMS.NewRow()
                SMSRow.BeginEdit()
                SMSRow("CONTACT_PERSON") = listSendRetailers(Index).NameApp
                SMSRow("CONTACT_MOBILE") = ContactMobile
                SMSRow("ORIGIN_COMPANY") = listSendRetailers(Index).IDKios
                SMSRow("MESSAGE") = Me.txtMessageRetailer.Text
                SMSRow("SENT_BY") = NufarmBussinesRules.User.UserLogin.UserName
                SMSRow.EndEdit()
                tblSMS.Rows.Add(SMSRow)
            End If
        End If

    End Sub
    Private Sub SendDistributor(ByVal Index As Integer, ByVal Contact As String, ByRef SMSRow As DataRow, ByRef tblSMS As DataTable)
        SMSRow = tblSMS.NewRow()
        SMSRow.BeginEdit()
        SMSRow("CONTACT_PERSON") = listSendDistributors(Index).Contact_Mobile
        SMSRow("CONTACT_MOBILE") = Contact
        SMSRow("ORIGIN_COMPANY") = listSendDistributors(Index).NameApp
        SMSRow("MESSAGE") = Me.txtMessageDistributor.Text
        SMSRow("SENT_BY") = NufarmBussinesRules.User.UserLogin.UserName
        SMSRow.EndEdit()
        tblSMS.Rows.Add(SMSRow)
    End Sub
    Private Sub SendRSMTM(ByVal Index As Integer, ByVal Contact As String, ByRef SMSRow As DataRow, ByRef tblSMS As DataTable)
        SMSRow = tblSMS.NewRow()
        SMSRow.BeginEdit()
        SMSRow("CONTACT_PERSON") = listSendRSMTM(Index).NameApp
        SMSRow("CONTACT_MOBILE") = Contact
        SMSRow("ORIGIN_COMPANY") = listSendRSMTM(Index).NameApp & " NUFARM RSM/TM"
        SMSRow("MESSAGE") = Me.txtMessageRSMTM.Text
        SMSRow("SENT_BY") = NufarmBussinesRules.User.UserLogin.UserName
        SMSRow.EndEdit()
        tblSMS.Rows.Add(SMSRow)
    End Sub

    Private Sub btnDistributor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDistributor.Click
        Try
            'validate input for Retailer SMS
            'check gridRetailer
            Me.Cursor = Cursors.Default
            Me.listSendDistributors.Clear()
            Me.listSendRetailers.Clear()
            Me.listSendRSMTM.Clear()
            Dim CheckedDistRows() As Janus.Windows.GridEX.GridEXRow = Nothing
            Dim CheckedCustomerRows() As Janus.Windows.GridEX.GridEXRow = Nothing
            Dim CheckedRSMTMRows() As Janus.Windows.GridEX.GridEXRow = Nothing
            Dim msgCount As Integer = 0
            If Not IsNothing(Me.grdRetailer.DataSource) Then
                If Me.grdRetailer.RecordCount > 0 Then
                    CheckedCustomerRows = Me.grdRetailer.GetCheckedRows()
                    If CheckedCustomerRows.Length > 0 Then
                        For Each row As Janus.Windows.GridEX.GridEXRow In CheckedCustomerRows
                            Dim retContact As New RetailerContact()
                            retContact.IDKios = row.Cells("IDKios").Value.ToString()
                            retContact.NameApp = row.Cells("Kios_Owner").Value.ToString()
                            retContact.ContactMobile = row.Cells("ContactMobile").Value.ToString()
                            Me.listSendRetailers.Add(retContact)
                        Next
                        msgCount = listSendRetailers.Count
                    End If
                End If
            End If
            If Not IsNothing(Me.grdDistributor.DataSource) Then
                If Me.grdDistributor.RecordCount > 0 Then
                    CheckedDistRows = Me.grdDistributor.GetCheckedRows()
                    If CheckedDistRows.Length > 0 Then
                        For Each row As Janus.Windows.GridEX.GridEXRow In CheckedDistRows
                            Dim DistContact As New DistributorContact()
                            DistContact.CodeApp = row.Cells("DISTRIBUTOR_ID").Value.ToString()
                            DistContact.NameApp = row.Cells("DISTRIBUTOR_NAME").Value.ToString()
                            DistContact.Contact_Mobile = row.Cells("CONTACT_PERSON").Value.ToString()
                            Select Case Me.cmbMobileToSend.Text
                                '1 Contact_Mobile1
                                '2 Contact_Mobile2
                                '3 All(1 and 2)
                                Case "1 Contact_Mobile1"
                                Case "2 Contact_Mobile2"
                                    DistContact.SelectAllContact = False
                                Case "3 All(1 and 2)"
                                    DistContact.SelectAllContact = True
                            End Select
                            DistContact.Contact1 = IIf((IsNothing(row.Cells("CONTACT1").Value) Or IsDBNull(row.Cells("CONTACT1").Value)), "", row.Cells("CONTACT1").Value.ToString())
                            DistContact.Contact2 = IIf((IsNothing(row.Cells("CONTACT2").Value) Or IsDBNull(row.Cells("CONTACT2").Value)), "", row.Cells("CONTACT2").Value.ToString())
                            Me.listSendDistributors.Add(DistContact)
                        Next
                        msgCount += listSendDistributors.Count
                    End If
                End If
            End If
            If Not IsNothing(Me.grdRSMTM.DataSource) Then
                If Me.grdRSMTM.RecordCount > 0 Then
                    CheckedRSMTMRows = Me.grdRSMTM.GetCheckedRows()
                    If CheckedRSMTMRows.Length > 0 Then
                        For Each row As Janus.Windows.GridEX.GridEXRow In CheckedRSMTMRows
                            'Dim DistContact As New DistributorContact()
                            'DistContact.CodeApp = row.Cells("DISTRIBUTOR_ID").Value.ToString()
                            'DistContact.NameApp = row.Cells("DISTRIBUTOR_NAME").Value.ToString()
                            'Select Case Me.cmbMobileToSend.Text
                            '    '1 Contact_Mobile1
                            '    '2 Contact_Mobile2
                            '    '3 All(1 and 2)
                            '    Case "1 Contact_Mobile1"
                            '    Case "2 Contact_Mobile2"
                            '        DistContact.SelectAllContact = False
                            '    Case "3 All(1 and 2)"
                            '        DistContact.SelectAllContact = True
                            'End Select
                            'DistContact.Contact1 = IIf((IsNothing(row.Cells("CONTACT1").Value) Or IsDBNull(row.Cells("CONTACT1").Value)), "", row.Cells("CONTACT1").Value.ToString())
                            'DistContact.Contact2 = IIf((IsNothing(row.Cells("CONTACT2").Value) Or IsDBNull(row.Cells("CONTACT2").Value)), "", row.Cells("CONTACT2").Value.ToString())
                            'Me.listSendDistributors.Add(DistContact)
                            Dim RSMTMContact As New RSMTM()
                            With RSMTMContact
                                .NameApp = row.Cells(0).Value.ToString()
                                .HP = row.Cells(1).Value.ToString()
                            End With
                            Me.listSendRSMTM.Add(RSMTMContact)
                        Next
                        msgCount += listSendRSMTM.Count
                    End If
                End If
            End If
            Dim ConfirmedMessage As String = ""
            If Not IsNothing(CheckedCustomerRows) Then
                If CheckedCustomerRows.Length > 100 Then
                    ConfirmedMessage = "This will send a bunch of sms to retailer " & Me.cmbCustomerType.Text & " ?" & vbCrLf
                End If
            End If
            If Not IsNothing(CheckedDistRows) Then
                If CheckedDistRows.Length > 100 Then
                     ConfirmedMessage &= IIf(ConfirmedMessage <> "", "And Distributors", "This will send a bunch of sms to distributors ? ")
                End If
            End If
            'Dim MessageCount As Integer = IIf((Not IsNothing(CheckedCustomerRows)), CheckedCustomerRows.Length, 0) + IIf((Not IsNothing(CheckedDistRows)), CheckedDistRows.Length, 0)
            ''agar kirim SMS bisa bulk sms ke server
            'maka pakai datatable
            Dim tblSMS As New DataTable("T_SMS")
            tblSMS.Columns.Add(New DataColumn("CONTACT_PERSON", Type.GetType("System.String")))
            tblSMS.Columns.Add(New DataColumn("CONTACT_MOBILE", Type.GetType("System.String")))
            tblSMS.Columns.Add(New DataColumn("ORIGIN_COMPANY", Type.GetType("System.String")))
            tblSMS.Columns.Add(New DataColumn("MESSAGE", Type.GetType("System.String")))
            tblSMS.Columns.Add(New DataColumn("SENT_BY", Type.GetType("System.String")))
            tblSMS.AcceptChanges()
            If msgCount >= 0 Then
                Dim SMSRow As DataRow = Nothing
                'create table Message

                If Not IsNothing(CheckedDistRows) Then
                    '============= SEND DISTRIBUTOR ================================
                    For i As Integer = 0 To Me.listSendDistributors.Count - 1
                        Select Case Me.cmbMobileToSend.Text
                            '1 Contact_Mobile1
                            '2 Contact_Mobile2
                            '3 All(1 and 2)
                            Case "1 Contact_Mobile1"
                                Me.SendDistributor(i, listSendDistributors(i).Contact1, SMSRow, tblSMS)

                            Case "2 Contact_Mobile2"
                                Me.SendDistributor(i, listSendDistributors(i).Contact2, SMSRow, tblSMS)
                            Case "3 All(1 and 2)"
                                Me.SendDistributor(i, listSendDistributors(i).Contact1, SMSRow, tblSMS)
                                Me.SendDistributor(i, listSendDistributors(i).Contact2, SMSRow, tblSMS)
                        End Select
                    Next

                    If CheckedDistRows.Length > 0 And Me.txtDistCCSMS.Text.Length > 10 Then
                        Dim ContactMobile As String = Me.txtDistCCSMS.Text.TrimEnd().TrimStart()
                        ContactMobile = ContactMobile.Replace(" ", "")
                        ContactMobile = ContactMobile.Replace("-", "")
                        ContactMobile = ContactMobile.Replace(".", "")
                        If ContactMobile.Length > 10 Then
                            If ContactMobile.StartsWith("08") Or ContactMobile.StartsWith("+628") Then
                                SMSRow = tblSMS.NewRow()
                                SMSRow.BeginEdit()
                                SMSRow("CONTACT_PERSON") = "SYSTEM"
                                SMSRow("CONTACT_MOBILE") = ContactMobile
                                SMSRow("ORIGIN_COMPANY") = "NUFARM STAFF"
                                SMSRow("MESSAGE") = Me.txtMessageRetailer.Text
                                SMSRow("SENT_BY") = NufarmBussinesRules.User.UserLogin.UserName
                                SMSRow.EndEdit()
                                tblSMS.Rows.Add(SMSRow)
                            End If
                        End If
                    End If
                    '============= END SEND DISTRIBUTOR ==================================
                End If

                If Not IsNothing(CheckedCustomerRows) Then
                    '============= SEND KIOS =============================================
                    For i As Integer = 0 To Me.listSendRetailers.Count - 1
                        Me.SendCustomer(i, "", SMSRow, tblSMS)
                    Next
                    If CheckedCustomerRows.Length > 0 And Me.txtCustCCSMS.Text.TrimEnd().TrimStart().Length > 0 Then
                        Dim ContactMobile As String = Me.txtCustCCSMS.Text.TrimEnd().TrimStart()
                        ContactMobile = ContactMobile.Replace(" ", "")
                        ContactMobile = ContactMobile.Replace("-", "")
                        ContactMobile = ContactMobile.Replace(".", "")
                        If ContactMobile.Length > 10 Then
                            If ContactMobile.StartsWith("08") Or ContactMobile.StartsWith("+628") Then
                                SMSRow = tblSMS.NewRow()
                                SMSRow.BeginEdit()
                                SMSRow("CONTACT_PERSON") = "SYSTEM"
                                SMSRow("CONTACT_MOBILE") = ContactMobile
                                SMSRow("ORIGIN_COMPANY") = "NUFARM STAFF"
                                SMSRow("MESSAGE") = Me.txtMessageRetailer.Text
                                SMSRow("SENT_BY") = NufarmBussinesRules.User.UserLogin.UserName
                                SMSRow.EndEdit()
                                tblSMS.Rows.Add(SMSRow)
                            End If
                        End If
                    End If
                    '===============END SEND KIOS==========================================
                End If

                If Not IsNothing(CheckedRSMTMRows) Then
                    For i As Integer = 0 To Me.listSendRSMTM.Count - 1
                        Me.SendRSMTM(i, listSendRSMTM(i).HP, SMSRow, tblSMS)
                    Next
                    If CheckedRSMTMRows.Length > 0 And Me.txtCCSMSRSMTM.Text.TrimEnd().TrimStart().Length > 0 Then
                        Dim ContactMobile As String = Me.txtCCSMSRSMTM.Text.TrimEnd().TrimStart()
                        ContactMobile = ContactMobile.Replace(" ", "")
                        ContactMobile = ContactMobile.Replace("-", "")
                        ContactMobile = ContactMobile.Replace(".", "")
                        If ContactMobile.Length > 10 Then
                            If ContactMobile.StartsWith("08") Or ContactMobile.StartsWith("+628") Then
                                SMSRow = tblSMS.NewRow()
                                SMSRow.BeginEdit()
                                SMSRow("CONTACT_PERSON") = "SYSTEM"
                                SMSRow("CONTACT_MOBILE") = ContactMobile
                                SMSRow("ORIGIN_COMPANY") = "NUFARM STAFF"
                                SMSRow("MESSAGE") = Me.txtMessageRSMTM.Text
                                SMSRow("SENT_BY") = NufarmBussinesRules.User.UserLogin.UserName
                                SMSRow.EndEdit()
                                tblSMS.Rows.Add(SMSRow)
                            End If
                        End If
                    End If
                End If
            End If

            If tblSMS.Rows.Count > 0 Then
                Me.clsDistributor.InsertSMSTable(tblSMS)
                Me.ShowMessageInfo("Message(s) Sent To System " & vbCrLf & "And will soon be sent to Customer(s)")
            End If

            If Not IsNothing(CheckedCustomerRows) Then
                If CheckedCustomerRows.Length > 0 Then
                    Me.grdRetailer.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                End If
            End If
            If Not IsNothing(CheckedDistRows) Then
                If CheckedDistRows.Length > 0 Then
                    Me.grdDistributor.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                End If
            End If
            If Not IsNothing(CheckedRSMTMRows) Then
                If CheckedRSMTMRows.Length > 0 Then
                    Me.grdRSMTM.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnDistributor_Click")
        End Try
    End Sub

    Private Sub Other_SMS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        '    Me.Cursor = Cursors.WaitCursor

        'Catch ex As Exception

        'End Try
        'Load DataDistributor
        CMain.StatProg = Main.StatusProgress.None

    End Sub

    Private Sub cmbCustomerType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCustomerType.SelectedIndexChanged
        Try
            If cmbCustomerType.Text = "" Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.DVRetailer = Me.clsDistributor.getRetailerDataContact(Me.cmbCustomerType.Text).DefaultView()
            ''bindgrid
            Me.BindGrid(Me.grdRetailer, DVRetailer)
            Me.grdRetailer.RootTable.Columns("IDKios").ShowRowSelector = True
            Me.grdRetailer.RootTable.Columns("IDKios").UseHeaderSelector = True
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_cmbCustomerType_SelectedIndexChanged")
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cmbMobileToSend_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMobileToSend.SelectedIndexChanged
        Try

            If Me.cmbMobileToSend.Text = "" Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.DVDistributor = Me.clsDistributor.CreateView_DistributorSMS(True)
            Me.BindGrid(Me.grdDistributor, DVDistributor)
            Me.grdDistributor.RootTable.Columns("DISTRIBUTOR_ID").ShowRowSelector = True
            Me.grdDistributor.RootTable.Columns("DISTRIBUTOR_ID").UseHeaderSelector = True
            Select Case Me.cmbMobileToSend.Text
                '1 Contact_Mobile1
                '2 Contact_Mobile2
                '3 All(1 and 2)
                Case "1 Contact_Mobile1"
                    Me.grdDistributor.RootTable.Columns("CONTACT1").Visible = True
                    Me.grdDistributor.RootTable.Columns("CONTACT2").Visible = False
                Case "2 Contact_Mobile2"
                    Me.grdDistributor.RootTable.Columns("CONTACT2").Visible = True
                    Me.grdDistributor.RootTable.Columns("CONTACT1").Visible = False
                Case "3 All(1 and 2)"
                    Me.grdDistributor.RootTable.Columns("CONTACT1").Visible = True
                    Me.grdDistributor.RootTable.Columns("CONTACT2").Visible = True
            End Select
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_cmbMobileToSend_SelectedIndexChanged")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbCustomerType_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCustomerType.TextChanged
        If Me.cmbCustomerType.Text = "" Then
            If Not IsNothing(Me.grdRetailer.DataSource) Then
                Me.grdRetailer.UnCheckAllRecords()
                Me.txtCustCCSMS.Text = ""
                Me.txtMessageRetailer.Text = ""
            End If
            Return
        End If
    End Sub

    Private Sub cmbMobileToSend_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMobileToSend.TextChanged
        If Me.cmbMobileToSend.Text = "" Then
            If Not IsNothing(Me.grdDistributor.DataSource) Then
                Me.grdDistributor.UnCheckAllRecords()
                Me.txtDistCCSMS.Text = ""
                Me.txtMessageDistributor.Text = ""
            End If
        End If
    End Sub

    Private Sub cmbRSMTM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRSMTM.SelectedIndexChanged
        If Me.cmbRSMTM.Text = "" Then : Return : End If

        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.grdRSMTM.DataSource) Then
                Me.grdRSMTM.UnCheckAllRecords()
            End If
            Dim DV As DataView = Nothing
            'get data RSM/TM
            DV = Me.clsDistributor.CreateView_RSMTM(Me.cmbRSMTM.Text, True).DefaultView()
            Me.BindGrid(Me.grdRSMTM, DV)
            Me.grdRSMTM.RootTable.Columns(0).ShowRowSelector = True
            Me.grdRSMTM.RootTable.Columns(0).UseHeaderSelector = True
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_cmbRSMTM_SelectedIndexChanged")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbRSMTM_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRSMTM.TextChanged
        If cmbRSMTM.Text = "" Then
            If Not IsNothing(Me.grdRSMTM.DataSource) Then
                Me.grdRSMTM.UnCheckAllRecords()
                Me.txtMessageRSMTM.Text = ""
                Me.txtCCSMSRSMTM.Text = ""
            End If
        End If
    End Sub
End Class
