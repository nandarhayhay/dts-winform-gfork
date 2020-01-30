Imports System.Threading
Imports System.Globalization
Public Class SMS
    'Private clsOA As NufarmBussinesRules.OrderAcceptance.OABrandPack
    Private clsPO As NufarmBussinesRules.PurchaseOrder.PO_BrandPack
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private SRow As isSelectingRow
    Private WithEvents AM As AlternateMessage
    Private DISTRIBUTOR_ID As String
    Private PO_REF_NO As String
    Dim isItemInserted As Boolean
    Dim FailedToInsert As Integer
    Private frmSMSPO As SMSPO = Nothing
    Private frmSMSGON As SMSGON = Nothing
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private toTableName As String = "SMS_TABLE"
    Friend Cmain As Main = Nothing

    Private Enum StatusProgress
        None
        Processing
        hide
    End Enum

    Private Sub ShowProceed()
        Me.ST = New Progress() : Me.ST.Show("Sending SMS to system") : Me.ST.StartPosition = FormStartPosition.CenterScreen : Me.ST.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            If Me.StatProg = StatusProgress.hide Then
                If Me.ST.Visible Then : Me.ST.Hide() : End If
            ElseIf Not Me.ST.Visible Then
                Me.ST.Show() : Me.ST.Refresh() : Thread.Sleep(50) : Application.DoEvents()
            Else
                Me.ST.Refresh() : Thread.Sleep(50) : Application.DoEvents()
            End If
        End While
        Thread.Sleep(100) : Me.ST.Close() : Me.ST = Nothing
    End Sub
    Private Enum isSelectingRow
        selecting
        none
    End Enum
    Private Sub AddControl(ByVal ctrl As Control)
        'Me.pnlData.Closed = False
        If (Not Me.XpGradientPanel1.Controls.Contains(ctrl)) Then
            Me.XpGradientPanel1.Controls.Add(ctrl)

        End If
    End Sub
    Private Sub LoadSMSPO()
        If IsNothing(Me.frmSMSPO) Then
            Me.frmSMSPO = New SMSPO()
        End If
        If IsNothing(Me.clsPO) Then
            Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PO_BrandPack()
        End If
        Dim DV As DataView = Me.clsPO.CreateViewPOSMS()
        If Not IsNothing(Me.GridEX1) Then
            Me.GridEX1 = Nothing
        End If
        frmSMSPO.GridEX1.DataSource = DV : frmSMSPO.GridEX1.RetrieveStructure()
        Me.AddControl(Me.frmSMSPO) : Me.ShowControl(Me.frmSMSPO)
        For Each col As Janus.Windows.GridEX.GridEXColumn In frmSMSPO.GridEX1.RootTable.Columns
            col.AutoSize()
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            If col.DataMember = "TOTAL_PO" Then
                col.FormatString = "#,##0.000"
            ElseIf col.DataMember = "PO_REF_NO" Then
                col.ShowRowSelector = True : col.UseHeaderSelector = True
            End If
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Next
        Me.GridEX1 = Me.frmSMSPO.GridEX1

        'Me.GridEX1.SetDataBinding(Me.clsPO.ViewPOSMS(), "")

        Me.btnSendSMSPO.Checked = True : Me.bntSendSMSGON.Checked = False
    End Sub
    Private Sub LoadSMSGON()
        If IsNothing(Me.frmSMSGON) Then
            Me.frmSMSGON = New SMSGON
        End If
        If Not IsNothing(Me.GridEX1) Then
            Me.GridEX1 = Nothing
        End If
        If IsNothing(Me.clsPO) Then
            Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PO_BrandPack()
        End If
        Dim DV As DataView = Me.clsPO.CreateViewGONSMS()
        Me.frmSMSGON.GridEX1.SetDataBinding(DV, "") : Me.frmSMSGON.GridEX1.RetrieveStructure()
        Me.AddControl(Me.frmSMSGON) : Me.ShowControl(Me.frmSMSGON)
        For Each col As Janus.Windows.GridEX.GridEXColumn In frmSMSGON.GridEX1.RootTable.Columns
            col.AutoSize()
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            If col.DataMember = "TransactionID" Then
                col.Visible = False
            ElseIf col.DataMember = "GON_NO" Then
                col.ShowRowSelector = True : col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo : col.UseHeaderSelector = True
            ElseIf col.DataMember = "GON_DATE" Then
                col.FilterEditType = Janus.Windows.GridEX.EditType.CalendarCombo
                col.FormatString = "dd MMMM yyyy"
                'ElseIf col.Type Is Type.GetType("System.Decimal") Then
                '    col.FilterEditType = Janus.Windows.GridEX.EditType.Combo
                '    col.FormatString = "#,##0.000"
            Else
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            End If
        Next
        Me.GridEX1 = Me.frmSMSGON.GridEX1
        Me.btnSendSMSPO.Checked = False : Me.bntSendSMSGON.Checked = True
    End Sub
    Private Sub ShowControl(ByVal Ctrl As Control)
        For Each ctr As Control In Me.XpGradientPanel1.Controls
            If Not ctr.Equals(Ctrl) Then
                ctr.Dock = DockStyle.None : ctr.SendToBack() : ctr.Hide()
            End If
        Next
        Ctrl.Dock = DockStyle.Fill
        Ctrl.Show()
        Ctrl.BringToFront()
    End Sub
    Private Sub ReadAcces()
        If Not Cmain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SMS = True Then
                Me.btnSystem.Visible = True
            Else
                Me.btnSystem.Visible = False
            End If
        End If
    End Sub
    Private Sub btnSystem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSystem.Click
        Try
            If IsNothing(Me.GridEX1) Then : Return : End If
            If IsNothing(Me.GridEX1.DataSource) Then : Return : End If
            If Me.GridEX1.RecordCount <= 0 Then : Return : End If

            If Me.GridEX1.GetCheckedRows.Length <= 0 Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.isItemInserted = False : Me.FailedToInsert = 0 : Dim CheckedColl As String = ""
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
            Me.ThreadProgress.Start()

            If Me.btnSendSMSPO.Checked Then
                Me.SRow = isSelectingRow.selecting
                For i As Integer = 0 To Me.GridEX1.RecordCount - 1
                    Me.GridEX1.MoveTo(i)
                    If Me.GridEX1.GetRow().CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                        CheckedColl &= Me.GridEX1.GetValue(0).ToString()
                        If Me.clsPO.IsValidDistributorDescription(Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString(), False) = False Then
                            Me.StatProg = StatusProgress.hide
                            Me.ShowMessageInfo("Contact_Mobile / Contact Person for Distributor " & Me.GridEX1.GetValue("DISTRIBUTOR_NAME").ToString() & vbCrLf & "Has not been set yet")
                            Me.StatProg = StatusProgress.Processing
                            FailedToInsert += 1 'String.Format("{0:#,##0.00}", CDec(Me.clsOADiscount.TOTAL_PRICE_DISTRIBUTOR))

                        Else
                            'BPK/Ibu Yth,PO PT. PANCA AGRO NIAGA LESTARI NO:A-3410/PES.PANL/VI/08,30/06/2008,telah diterima.Terima kasih-Nufarm
                            Dim Podesc As String = Me.GridEX1.GetValue("Descriptions").ToString()

                            Dim MESSAGE As String = "Bpk/Ibu Yth,PO " & Me.GridEX1.GetValue("DISTRIBUTOR_NAME").ToString() & " NO:" & Me.GridEX1.GetValue(0).ToString() & ";" & String.Format(New CultureInfo("id-ID"), ",TGL {0:dd-MM-yyyy}", Convert.ToDateTime(Me.GridEX1.GetValue(1))) & _
                            ",JML " & String.Format(New CultureInfo("id-ID"), "Rp {0:N}", Convert.ToDecimal(Me.GridEX1.GetValue("TOTAL_PO"))) & _
                            ",sudah masuk ke sistem kami," & _
                            "terima kasih-Nufarm."

                            If (Podesc + "" + MESSAGE).Length <= 159 Then
                                MESSAGE = "Bpk/Ibu Yth,PO " & Me.GridEX1.GetValue("DISTRIBUTOR_NAME").ToString() & " NO:" & Me.GridEX1.GetValue(0).ToString() & ";" & String.Format(New CultureInfo("id-ID"), ",TGL {0:dd-MM-yyyy}", Convert.ToDateTime(Me.GridEX1.GetValue(1))) & _
                                ",JML " & String.Format(New CultureInfo("id-ID"), " Rp {0:N}", Convert.ToDecimal(Me.GridEX1.GetValue("TOTAL_PO"))) & _
                                Podesc & ",sudah masuk ke sistem kami," & _
                                "terima kasih-Nufarm."
                            End If
                            'Dim lstMessage As New List(Of String)
                            'Dim remaindMessage As String = MESSAGE

                            'If remaindMessage.Length > 120 Then
                            '    While remaindMessage.Length > 0
                            '        Dim entryMessage As String = remaindMessage.Substring(0, 120)
                            '        lstMessage.Add(entryMessage)
                            '        remaindMessage = remaindMessage.Remove(0, 120)
                            '        If remaindMessage.Length <= 120 Then
                            '            lstMessage.Add(remaindMessage)
                            '            remaindMessage = ""
                            '        End If
                            '    End While
                            'Else
                            '    lstMessage.Add(remaindMessage)
                            'End If
                            'Me.clsPO.InsertSMSTable(Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString(), Me.GridEX1.GetValue("PO_REF_NO").ToString(), lstMessage, Me.toTableName, False)
                            'isItemInserted = True

                            'Dim lstMessage As New List(Of String)
                            'lstMessage.Add(MESSAGE)

                            'Me.clsPO.InsertSMSTable(Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString(), Me.GridEX1.GetValue("PO_REF_NO").ToString(), lstMessage, Me.toTableName, False)
                            'isItemInserted = True
                            If MESSAGE.Length > 159 Then
                                Me.StatProg = StatusProgress.hide
                                If Me.ShowConfirmedMessage("Message is more than 159 chars" & vbCrLf & "Message cannot be sent to distributor!." & _
                                vbCrLf & "Original Message = " & MESSAGE & vbCrLf & "Characters length = " & MESSAGE.Length.ToString() & _
                                vbCrLf & "Would you mind trimming the message by yourself ?") = Windows.Forms.DialogResult.Yes Then
                                    If Me.AM Is Nothing OrElse Me.AM.IsDisposed() Then
                                        AM = New AlternateMessage()
                                    End If
                                    Me.DISTRIBUTOR_ID = Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()
                                    Me.PO_REF_NO = Me.GridEX1.GetValue("PO_REF_NO").ToString()
                                    AM.txtMessage.Text = MESSAGE
                                    AM.Text &= " PO : " & Me.GridEX1.GetValue(0).ToString()
                                    AM.txtMessage.SelectAll()
                                    AM.ShowInTaskbar = False
                                    AM.ShowDialog(Me)
                                End If
                                Me.StatProg = StatusProgress.Processing
                            Else
                                Dim lstMessage As New List(Of String)
                                lstMessage.Add(MESSAGE)

                                Me.clsPO.InsertSMSTable(Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString(), Me.GridEX1.GetValue("PO_REF_NO").ToString(), lstMessage, Me.toTableName, False, , True)
                                isItemInserted = True
                            End If
                        End If
                    End If
                Next
            ElseIf Me.bntSendSMSGON.Checked Then
                Me.SRow = isSelectingRow.selecting
                For i As Integer = 0 To Me.GridEX1.RecordCount - 1
                    Me.GridEX1.MoveTo(i)
                    If Me.GridEX1.GetRow().CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                        CheckedColl &= Me.GridEX1.GetValue(1).ToString()
                        If Me.clsPO.IsValidDistributorDescription(Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString(), False) = False Then
                            Me.StatProg = StatusProgress.hide
                            Me.ShowMessageInfo("Contact_Mobile / Contact Person for Distributor " & Me.GridEX1.GetValue("DISTRIBUTOR_NAME").ToString() & vbCrLf & "Has not been set yet")
                            Me.StatProg = StatusProgress.Processing
                            FailedToInsert += 1 'String.Format("{0:#,##0.00}", CDec(Me.clsOADiscount.TOTAL_PRICE_DISTRIBUTOR))
                        Else
                            'BPK/Ibu Yth,PO PT. PANCA AGRO NIAGA LESTARI NO:A-3410/PES.PANL/VI/08,30/06/2008,telah diterima.Terima kasih-Nufarm
                            Dim MESSAGE As String = Me.GridEX1.GetValue("MESSAGE").ToString()
                            Dim lstMessage As New List(Of String)
                            lstMessage.Add(MESSAGE)

                            Me.clsPO.InsertSMSTable(Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString(), Me.GridEX1.GetValue("PO_REF_NO").ToString(), lstMessage, Me.toTableName, False, Me.GridEX1.GetValue("TransactionID").ToString())
                            isItemInserted = True
                            '& " " & Me.GridEX1.GetValue("ITEM_DESCRIPTION").ToString() '& " untuk PO NO:" & Me.GridEX1.GetValue("PO_REF_NO").ToString() & ",telah kami kirim.Terima kasih-Nufarm"
                            '                            MESSAGE = MESSAGE.Replace(",Terima kasih-Nufarm", "")
                            '                            MESSAGE &= ",Terima kasih-Nufarm"
                            ' String.Format("{0:#,##0.00}", Me.GridEX1.GetValue("TOTAL_PO")) & " SUDAH MASUK SYSTEM KAMI.NUFARM"
                            'Dim lstMessage As New List(Of String)
                            'Dim remaindMessage As String = MESSAGE

                            'If remaindMessage.Length > 120 Then
                            '    While remaindMessage.Length > 0
                            '        Dim entryMessage As String = remaindMessage.Substring(0, 120)
                            '        lstMessage.Add(entryMessage)
                            '        remaindMessage = remaindMessage.Remove(0, 120)
                            '        If remaindMessage.Length <= 120 Then
                            '            lstMessage.Add(remaindMessage)
                            '            remaindMessage = ""
                            '        End If
                            '    End While
                            'Else
                            '    lstMessage.Add(remaindMessage)
                            'End If
                            'Me.clsPO.InsertSMSTable(Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString(), Me.GridEX1.GetValue("PO_REF_NO").ToString(), lstMessage, Me.toTableName, False)
                            'isItemInserted = True

                            'If MESSAGE.Length > 120 Then
                            '    Me.StatProg = StatusProgress.hide
                            '    If Me.ShowConfirmedMessage("Message is more than 120 chars" & vbCrLf & "Message cannot be sent to distributor!." & _
                            '    vbCrLf & "Original Message = " & MESSAGE & vbCrLf & ", Characters length = " & MESSAGE.Length.ToString() & _
                            '    vbCrLf & "Would you mind trimming the message by yourself ?") = Windows.Forms.DialogResult.Yes Then
                            '        If Me.AM Is Nothing OrElse Me.AM.IsDisposed() Then
                            '            AM = New AlternateMessage()
                            '        End If
                            '        'Me.DISTRIBUTOR_ID = Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()
                            '        'Me.PO_REF_NO = Me.GridEX1.GetValue("PO_REF_NO").ToString()
                            '        AM.txtMessage.Text = MESSAGE
                            '        AM.Text &= " GON : " & Me.GridEX1.GetValue(0).ToString()
                            '        AM.txtMessage.SelectAll()
                            '        AM.ShowInTaskbar = False
                            '        AM.ShowDialog(Me)
                            '    End If
                            '    Me.StatProg = StatusProgress.Processing
                            'Else
                            '    Dim lstMessage As New List(Of String)
                            '    lstMessage.Add(MESSAGE)

                            '    Me.clsPO.InsertSMSTable(Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString(), Me.GridEX1.GetValue("PO_REF_NO").ToString(), lstMessage, Me.toTableName, False, Me.GridEX1.GetValue("TransactionID").ToString())
                            '    isItemInserted = True
                            'End If

                        End If
                    End If
                Next
            End If
            Me.btnRefresh_Click(Me.btnRefresh, New System.EventArgs())
            Me.StatProg = StatusProgress.None
            If CheckedColl = "" Then
                Me.ShowMessageInfo("No item seleted to send")
                Return
            End If
            If (FailedToInsert > 0) And (isItemInserted = True) Then
                Me.ShowMessageInfo("Data sent to system and will be sent to distributors in seconds" & vbCrLf & "But some data(s) couldn't be sent " & vbCrLf & "Due to incomplete distributor's property")
            ElseIf (FailedToInsert > 0) And (isItemInserted = False) Then
                Me.ShowMessageInfo("data(s) couldn't be sent due to incomplete's property")
            ElseIf (FailedToInsert <= 0) And (isItemInserted = True) Then
                Me.ShowMessageInfo("Data sent to system and will be sent to distributors in seconds")
            ElseIf (FailedToInsert <= 0) And (isItemInserted = False) Then
                Me.ShowMessageInfo("could not proces 0 transaction")
            End If
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.SRow = isSelectingRow.none : Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub SMS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ReadAcces()
            'Me.mcbDistributor.DropDownList().Columns("DISTRIBUTOR_ID").AutoSize()
            'Me.mcbDistributor.DropDownList().Columns("DISTRIBUTOR_NAME").AutoSize()
        Catch ex As Exception

        Finally
            Cmain.FormLoading = Main.StatusForm.HasLoaded : Me.Cmain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SMS_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsPO) Then
                Me.clsPO.Dispose(True)
                Me.clsPO = Nothing
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            If Me.SRow = isSelectingRow.selecting Then : Return : End If
            If Not IsNothing(Me.GridEX1) Then
                If Not IsNothing(Me.GridEX1.DataSource) Then
                    If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        If Me.GridEX1.RootTable.Columns.Contains("PO_DESCRIPTION") Then
                            Me.GridEX1.CellToolTipText = Me.GridEX1.GetValue("PO_DESCRIPTION").ToString()
                        ElseIf Me.GridEX1.RootTable.Columns.Contains("DESCRIPTION") Then
                            Me.GridEX1.CellToolTipText = Me.GridEX1.GetValue("DESCRIPTION").ToString()
                        End If
                    End If
                End If
            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PO_BrandPack()
            'Me.BindMulticolumnCombo(Me.mcbDistributor, Me.clsPO.ViewDistributor())
            'Me.clsPO.CreateViewPOSMS()
            'Me.GridEX1.SetDataBinding(Me.clsPO.ViewPOSMS(), "")
            'Me.ItemPanel1_ItemClick(Me.ItemPanel1, New EventArgs())
            If Me.btnSendSMSPO.Checked Then
                Me.LoadSMSPO()
            ElseIf Me.bntSendSMSGON.Checked Then
                Me.LoadSMSGON()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AM_Acceptmessage(ByVal sender As Object, ByVal e As System.EventArgs) Handles AM.Acceptmessage
        Try
            Me.Cursor = Cursors.WaitCursor
            If IsNothing(Me.clsPO) Then
                Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PO_BrandPack()
            End If
            If Me.AM.txtMessage.Text.Length > 159 Then
                If Me.ShowConfirmedMessage("Message is more than 159 chars" & vbCrLf & "Message cannot be sent to distributor!." & _
                                vbCrLf & "Characters length = " & AM.txtMessage.Text.Length.ToString() & _
                                  vbCrLf & "Would you mind trimming the message by yourself ?") = Windows.Forms.DialogResult.Yes Then
                    AM.txtMessage.SelectAll()
                    Return
                Else
                    Me.FailedToInsert += 1
                End If
            Else
                Dim lstMessage As New List(Of String)
                lstMessage.Add(AM.txtMessage.Text)
                If Me.btnSendSMSPO.Checked Then
                    Me.clsPO.InsertSMSTable(Me.DISTRIBUTOR_ID, Me.PO_REF_NO, lstMessage, "SMS_TABLE", False, , True)
                ElseIf Me.bntSendSMSGON.Checked Then
                    Me.clsPO.InsertSMSTable(Me.DISTRIBUTOR_ID, Me.PO_REF_NO, lstMessage, False, False, Me.GridEX1.GetValue("SPPB_NO").ToString())
                End If
                Me.isItemInserted = True
                AM.Close()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_AM_Acceptmessage")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim btn As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case btn.Name
                Case "btnSendSMSPO" : Me.LoadSMSPO() : Me.toTableName = "SMS_TABLE"
                Case "bntSendSMSGON" : Me.LoadSMSGON() : Me.toTableName = "GON_SMS"
            End Select
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Me.btnSendSMSPO.Checked Then : e.Cancel = True : Return : End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True : Return
            End If
            Me.Cursor = Cursors.WaitCursor
            If Me.btnSendSMSPO.Checked Then

            ElseIf Me.bntSendSMSGON.Checked Then
                Me.clsPO.DeleteSMS("GON_SMS", Me.GridEX1.GetValue("TransactionID").ToString())
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
