Imports System.Threading
Public Class Cancel_PO
#Region " Deklarasi "
    Private clsPO As NufarmBussinesRules.PurchaseOrder.PO_BrandPack
    Private Delegate Sub onConnecting(ByVal message As String)
    Private Event Connecting As onConnecting
    Private Delegate Sub onClosingConnection()
    Private Event ClosingConnection As onClosingConnection
    Dim rnd As Random
    Private Shadows tickCount As Integer
    Private HasgeneratedDiscount As Boolean
    Private colPoRefNo As Hashtable
    Private WithEvents AM As AlternateMessage = Nothing
    Private DISTRIBUTOR_ID As String
    Private PO_REF_NO As String
    Private _DvPOBrandPack As DataView = Nothing
    Dim DVDistributor As DataView = Nothing
    Private ListPOREFNO As New List(Of String)
    Private Ticcount As Integer = 0
    Friend CMain As Main = Nothing
    Private LD As Loading
#End Region

#Region " Procedure "

    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private Enum StatusProgress
        None
        Processing
    End Enum

    Private Sub ShowProceed()
        'Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
        Me.ST = New Progress : Me.ST.Show("Updating purchase order.....")
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Me.ST.Refresh() : Thread.Sleep(50) : Application.DoEvents()
        End While
        Thread.Sleep(50) : Me.ST.Close() : Me.ST = Nothing : Me.Ticcount = 0

    End Sub
    Private ReadOnly Property DvPOBrandPack() As DataView
        Get
            If (_DvPOBrandPack Is Nothing) Then
                Dim DtPOBrandPack As New DataTable("T_POBrandPack")
                DtPOBrandPack.Columns.Add(New DataColumn("PO_BRANDPACK_ID", Type.GetType("System.String")))
                DtPOBrandPack.Columns.Add(New DataColumn("PO_ORIGINAL_QTY", Type.GetType("System.Decimal")))
                DtPOBrandPack.Columns.Add(New DataColumn("PO_REF_NO", Type.GetType("System.String")))
                DtPOBrandPack.Columns.Add(New DataColumn("PO_REF_DATE", Type.GetType("System.DateTime")))
                DtPOBrandPack.Columns.Add(New DataColumn("DISTRIBUTOR_ID", Type.GetType("System.String")))
                DtPOBrandPack.Columns.Add(New DataColumn("DISTRIBUTOR_NAME", Type.GetType("System.String")))
                DtPOBrandPack.Columns.Add(New DataColumn("REASON", Type.GetType("System.String")))
                Me._DvPOBrandPack = DtPOBrandPack.DefaultView : Me._DvPOBrandPack.Sort = "PO_BRANDPACK_ID"
            End If
            Return _DvPOBrandPack
        End Get

    End Property
    Private Sub FillValueList()
        'AMBIL DATA DI ACCPACK 
        Dim VList() As String = {"CREDIT LIMIT", "OVERDUE", "REVISE", "UNPRODUCED_PRODUCT"}
        Dim ColReason As Janus.Windows.GridEX.GridEXColumn = Me.GridEX1.RootTable.Columns("REASON")
        ColReason.EditType = Janus.Windows.GridEX.EditType.Combo
        ColReason.AutoComplete = True : ColReason.HasValueList = True
        Dim ValueListUnit As Janus.Windows.GridEX.GridEXValueListItemCollection = ColReason.ValueList
        ValueListUnit.PopulateValueList(VList, "REASON")
        ColReason.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        ColReason.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    End Sub
    Private Overloads Sub ShowProgress(ByVal Message As String) Handles Me.Connecting
        'Application.DoEvents()
        Me.ST = New Progress
        Me.ST.Show(Message)
    End Sub

    Private Sub closeConnection() Handles Me.ClosingConnection
        If Not IsNothing(Me.ST) Then
            Me.ST.Close()
            Me.ST = Nothing
            Me.tickCount = 0
        End If
    End Sub

    Friend Sub InitializeData()
        Me.LoadData()
    End Sub

    Private Sub LoadData()
        Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PO_BrandPack()
        Me.CreateViewActivePO()
    End Sub

    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Cancel_PO = True Then
                If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PO = True Then
                    Me.btnRelease.Visible = True
                Else
                    Me.btnRelease.Visible = False
                End If
            Else
                Me.btnRelease.Visible = False
            End If
        End If
    End Sub

    Private Sub CreateViewActivePO()
        If Me.CmbDistributor.Value Is Nothing Then
            Me.clsPO.CreateViewActivePO(Me.DVDistributor, String.Empty)
        Else
            Me.clsPO.CreateViewActivePO(Me.DVDistributor, Me.CmbDistributor.Value.ToString())
        End If
        Me.GridEX1.SetDataBinding(Me.clsPO.ViewActivePO(), "")
        Me.CmbDistributor.SetDataBinding(Me.DVDistributor, "")
    End Sub

    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.tickCount >= Me.ResultRandom Then
            If Me.HasgeneratedDiscount = True Then
                RaiseEvent ClosingConnection()
                Me.Timer1.Enabled = False
                Me.Timer1.Stop()
                Me.LoadData()
            Else
                Me.ResultRandom += 1
            End If
        End If
    End Sub

    Private Sub Cancel_PO_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsPO) Then
                Me.clsPO.Dispose(True)
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Cancel_PO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            'AddHandler Timer1.Tick, AddressOf ChekTimer
            Me.ReadAcces()
        Catch ex As Exception
        Finally
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.GridEX1.RecordCount <= 0 Then
                Return
            End If
            If Me.DvPOBrandPack.Count <= 0 Then : Return : End If
            'check reason untuk setiap item yang di cheklist
            For i As Integer = 0 To Me.DvPOBrandPack.Count - 1
                If IsDBNull(Me.DvPOBrandPack(i)("REASON")) Or IsNothing(Me.DvPOBrandPack(i)("REASON")) Then
                    Me.ShowMessageInfo("You must enter the REASON for every item") : Return
                ElseIf Me.DvPOBrandPack(i)("REASON").ToString() = "" Then
                    Me.ShowMessageInfo("You must enter the REASON for every item") : Return
                End If
            Next

            Me.HasgeneratedDiscount = False : Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            'RaiseEvent Connecting("Updating Purchase Order..............") : Application.DoEvents()
            'Me.rnd = New Random()
            'Me.ResultRandom = Me.rnd.Next(1, 4)
            'Me.Timer1.Enabled = False
            'Me.Timer1.Start()
            'Dim DVOriginal As DataView = DirectCast(Me.GridEX1.DataSource, DataView).ToTable.Copy().DefaultView()
            Me.clsPO.CancelPO(Me.DvPOBrandPack, False)
            'batal kan sms po
            For i As Integer = 0 To Me.ListPOREFNO.Count - 1
                DvPOBrandPack.RowFilter = "PO_REF_NO = '" + ListPOREFNO(i).ToString() + "'"
                Dim CountPOBrandPack As Integer = Me.clsPO.Getcount(ListPOREFNO(i).ToString(), False)
                If DvPOBrandPack.Count > 0 Then
                    Dim PO_REF_DATE As String = Convert.ToDateTime(Me.DvPOBrandPack(0)("PO_REF_DATE")).ToShortDateString()
                    Dim DISTRIBUTOR_ID As String = Me.DvPOBrandPack(0)("DISTRIBUTOR_ID").ToString()
                    Dim DISTRIBUTOR_NAME As String = Me.DvPOBrandPack(0)("DISTRIBUTOR_NAME").ToString()
                    Dim PO_REF_NO As String = Me.DvPOBrandPack(0)("PO_REF_NO").ToString()
                    Dim MESSAGE As String = ""
                    If DvPOBrandPack.Count < CountPOBrandPack Then
                        MESSAGE = "Bpk/Ibu Yth,PO " & DISTRIBUTOR_NAME & _
                                 ",NO:" & PO_REF_NO & ";" & PO_REF_DATE & _
                                 ";telah direfisi.Terimakasih-Nufarm"
                    ElseIf DvPOBrandPack.Count = CountPOBrandPack Then
                        MESSAGE = "Bpk/Ibu Yth,PO " & DISTRIBUTOR_NAME & _
                                   ",NO:" & PO_REF_NO & ";" & PO_REF_DATE & _
                                   ";telah dibatalkan.Terimakasih-Nufarm"
                    End If
                    Dim lstMessage As New List(Of String)
                    Dim remaindMessage As String = MESSAGE

                    If remaindMessage.Length > 120 Then
                        While remaindMessage.Length > 0
                            Dim entryMessage As String = remaindMessage.Substring(0, 120)
                            lstMessage.Add(entryMessage)
                            remaindMessage = remaindMessage.Remove(0, 120)
                            If remaindMessage.Length <= 120 Then
                                lstMessage.Add(remaindMessage)
                                remaindMessage = ""
                            End If
                        End While
                    Else
                        lstMessage.Add(remaindMessage)
                    End If
                    Me.clsPO.InsertSMSTable(DISTRIBUTOR_ID, PO_REF_NO, lstMessage, "SMS_TABLE", False)
                    'If MESSAGE.Length > 120 Then
                    '    Me.StatProg = StatusProgress.None
                    '    If Me.ShowConfirmedMessage("Message is more than 120 chars" & vbCrLf & "Message cannot be sent to distributor!." & _
                    '    vbCrLf & "Original Message = " & vbCrLf & " >> " & MESSAGE & " << " & vbCrLf & "Characters length = " & MESSAGE.Length.ToString() & _
                    '    vbCrLf & "Would you mind trimming the message by yourself ?") = Windows.Forms.DialogResult.Yes Then
                    '        If Me.AM Is Nothing OrElse Me.AM.IsDisposed() Then
                    '            AM = New AlternateMessage()
                    '        End If
                    '        If Not IsNothing(Me.ST) Then
                    '            Me.ST.Hide()
                    '        End If
                    '        Me.DISTRIBUTOR_ID = DISTRIBUTOR_ID
                    '        Me.PO_REF_NO = PO_REF_NO
                    '        AM.txtMessage.Text = MESSAGE
                    '        AM.txtMessage.SelectAll()
                    '        AM.Text &= " PO : " & PO_REF_NO
                    '        AM.ShowInTaskbar = False
                    '        AM.ShowDialog(Me)
                    '        If Not IsNothing(Me.ST) Then
                    '            Me.ST.Show()
                    '        End If
                    '    End If
                    'Else
                    '    Me.clsPO.InsertSMSTable(DISTRIBUTOR_ID, PO_REF_NO, MESSAGE, "SMS_TABLE", False)
                    'End If
                End If
            Next
            Me.HasgeneratedDiscount = True : Me.ListPOREFNO.Clear()
            Me.DvPOBrandPack.Table.Clear() : Me.GridEX1.UnCheckAllRecords()
            Me.CreateViewActivePO() : Me.StatProg = StatusProgress.None
        Catch ex As Exception
            'If Me.Timer1.Enabled = True Then
            '    Me.Timer1.Enabled = False
            '    Me.Timer1.Stop()
            'End If
            Me.tickCount = 0
            'If Not IsNothing(Me.ST) Then
            '    RaiseEvent ClosingConnection()
            'End If
            Me.StatProg = StatusProgress.None
            Me.ShowMessageInfo(ex.Message)
            Me.ShowMessageInfo("For some reasons due to data integrity" & vbCrLf & "Please contact Administrator due to this error ")
            Me.LogMyEvent(ex.Message, Me.Name + "_btnRelease_Click")
        Finally
            Me.DvPOBrandPack.RowFilter = ""
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub RefreshData1_BtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshData1.BtnClick
        Try
            If (Me.DvPOBrandPack.Count > 0) Then
                Me.ShowMessageInfo("You should Relese PO for item you've just checked.") : Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.LoadData()
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.tickCount += 1
    End Sub

    'Private Sub AM_Acceptmessage(ByVal sender As Object, ByVal e As System.EventArgs) Handles AM.Acceptmessage
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        If IsNothing(Me.clsPO) Then
    '            Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PO_BrandPack()
    '        End If
    '        If Me.AM.txtMessage.Text.Length > 120 Then
    '            If Me.ShowConfirmedMessage("Message is more than 120 chars" & vbCrLf & "Message cannot be sent to distributor!." & _
    '                            vbCrLf & "Characters length = " & AM.txtMessage.Text.Length.ToString() & _
    '                              vbCrLf & "Would you mind trimming the message by yourself ?") = Windows.Forms.DialogResult.Yes Then
    '                AM.txtMessage.SelectAll()
    '                Return
    '            End If
    '        Else
    '            Me.clsPO.InsertSMSTable(Me.DISTRIBUTOR_ID, Me.PO_REF_NO, AM.txtMessage.Text, "SMS_TABLE", False)
    '            AM.Close()
    '        End If
    '    Catch ex As Exception
    '        Me.ShowMessageInfo(ex.Message)
    '        Me.LogMyEvent(ex.Message, Me.Name + "_AM_Acceptmessage")
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        Try
            Me.Cursor = Cursors.WaitCursor
            If e.Column.DataMember = "REASON" Then
                Dim index As Integer = Me.DvPOBrandPack.Find(Me.GridEX1.GetValue("PO_BRANDPACK_ID"))
                If index <> -1 Then
                    Me.DvPOBrandPack(index).BeginEdit() : Me.DvPOBrandPack(index)("REASON") = Me.GridEX1.GetValue("REASON").ToString() : Me.DvPOBrandPack(index).EndEdit()
                End If
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_RowCheckStateChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles GridEX1.RowCheckStateChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            ''check data discount

            Dim DrView As DataRowView = Nothing
            If e.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                If IsNothing(Me.clsPO) Then : Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PO_BrandPack : End If
                If (Not Me.clsPO.IsHasAgreementDiscount(Me.GridEX1.GetValue("PO_BRANDPACK_ID").ToString())) Then
                    If (Not Me.clsPO.isHasRemainFromOtherOA(Me.GridEX1.GetValue("PO_BRANDPACK_ID").ToString())) Then
                        If (Me.DvPOBrandPack.Find(Me.GridEX1.GetValue("PO_BRANDPACK_ID")) <= -1) Then
                            With Me.DvPOBrandPack
                                DrView = .AddNew()
                                DrView("PO_BRANDPACK_ID") = Me.GridEX1.GetValue("PO_BRANDPACK_ID")
                                DrView("PO_ORIGINAL_QTY") = Me.GridEX1.GetValue("PO_ORIGINAL_QTY")
                                DrView("PO_REF_NO") = Me.GridEX1.GetValue("PO_REF_NO")
                                DrView("PO_REF_DATE") = Convert.ToDateTime(Me.GridEX1.GetValue("PO_REF_DATE"))
                                DrView("DISTRIBUTOR_ID") = Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()
                                DrView("DISTRIBUTOR_NAME") = Me.GridEX1.GetValue("DISTRIBUTOR_NAME").ToString()
                                DrView("REASON") = Me.GridEX1.GetValue("REASON").ToString()
                                DrView.EndEdit()
                            End With
                        End If
                        If Not Me.ListPOREFNO.Contains(Me.GridEX1.GetValue("PO_REF_NO").ToString()) Then
                            Me.ListPOREFNO.Add(Me.GridEX1.GetValue("PO_REF_NO").ToString())
                        End If
                    End If
                End If
            Else
                Dim Index As Integer = Me.DvPOBrandPack.Find(Me.GridEX1.GetValue("PO_BRANDPACK_ID"))
                If (Index >= 0) Then
                    Me.DvPOBrandPack(Index).Delete()
                    Me.DvPOBrandPack.RowFilter = "PO_REF_NO = '" + Me.GridEX1.GetValue("PO_REF_NO").ToString() + "'"
                    If Me.DvPOBrandPack.Count <= 0 Then
                        If (Me.ListPOREFNO.Contains(Me.GridEX1.GetValue("PO_REF_NO").ToString())) Then
                            Me.ListPOREFNO.Remove(Me.GridEX1.GetValue("PO_REF_NO").ToString())
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Me.GridEX1.GetRow.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_RowCheckStateChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim DV As DataView = DirectCast(Me.CmbDistributor.DataSource, DataView)
            DV.RowFilter = "DISTRIBUTOR_NAME LIKE '%" & Me.CmbDistributor.Text & "%'"
            Me.CmbDistributor.DropDownList.Refetch() : Me.ShowMessageInfo(DV.Count.ToString() & " item(s) found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CmbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbDistributor.ValueChanged
        Try
            If Me.CmbDistributor.SelectedIndex = -1 Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.clsPO.CreateViewActivePO(Me.DVDistributor, Me.CmbDistributor.Value.ToString())
            Me.GridEX1.SetDataBinding(Me.clsPO.ViewActivePO(), "") : Me.FillValueList()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

End Class


