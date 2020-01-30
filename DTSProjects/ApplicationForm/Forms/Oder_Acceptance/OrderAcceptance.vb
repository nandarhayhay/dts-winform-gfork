Public Class OrderAcceptance

#Region " Deklarasi "
    Private clsOA As NufarmBussinesRules.OrderAcceptance.OARegistering
    Friend Mode As ModeSave
    Private SFG As StateFillingGrid
    Private SFM As StateFillingMCB
    Friend UM As UpdateMode
    Friend OA_ID As String
    Private HasLoad As Boolean
    Friend DISTRIBUTOR_ID As String 'DISTRIBUTOR_ID FROM PO
    Friend DISTRIBUTOR_NAME As String 'DISTRIBUTOR_NAME PO
    Friend PO_REF_DATE As String
    Private RunNumber As String = ""
#End Region

#Region " Enum "
    Friend Enum UpdateMode
        FromOriginal
        FromGrid
    End Enum
    Private Enum StateFillingGrid
        fiiling
        HasFilled
    End Enum
    Friend Enum ModeSave
        Save
        Update
    End Enum
    Private Enum StateFillingMCB
        Filling
        HasFilled
    End Enum
#End Region

#Region " Void "
    Private Function generateOANum(ByVal PO_NUM As String) As String
        Dim num As String = "00000000"
        Dim RetVal As String = ""
        Try
            Dim OACount As Integer = Me.clsOA.GetOACount()
            'If OACount = 999999 Then
            '    OACount = 0
            'End If
            OACount += 1
            If OACount > 99999999 Then
                OACount = OACount Mod 99999999
            End If
            Dim X As Integer = num.Length - CStr(OACount).Length
            If X = 0 Then
                num = ""
            Else
                num = num.Remove(X - 1, CStr(OACount).Length)
            End If
            num &= OACount.ToString() : Me.RunNumber = num
            RetVal = PO_NUM & "-" & num
        Catch ex As Exception

        End Try
        Return RetVal
    End Function
    Private Sub EnabledControl()
        Me.mcbPOREFNO.ReadOnly = False
        Me.mcbDistributor.ReadOnly = False
        Me.txtOARef.ReadOnly = False
        Me.dtPicOADate.ReadOnly = False
        Me.btnOK.Enabled = True
    End Sub
    Private Sub UnabledControl()
        Me.mcbPOREFNO.ReadOnly = True
        Me.mcbDistributor.ReadOnly = False
        If Me.Mode = ModeSave.Update Then
            Me.dtPicOADate.ReadOnly = False
        Else
            Me.dtPicOADate.ReadOnly = True
        End If
        Me.txtOARef.ReadOnly = True

    End Sub
    Friend Sub InitializeData()
        Me.refreshData()
    End Sub
    Private Sub Bindgrid()
        Me.GridEX1.SetDataBinding(Me.clsOA.ViewOARegistering_1(), "")
    End Sub
    Private Sub InflateData()
        Me.mcbPOREFNO.Value = Me.GridEX1.GetValue("PO_REF_NO")
        Me.txtOARef.Text = Me.GridEX1.GetValue("OA_ID").ToString()
        Me.dtPicOADate.Value = Convert.ToDateTime(Me.GridEX1.GetValue("OA_DATE"))
        Me.dtPicOADate.IsNullDate = False
        Me.mcbDistributor.Value = Me.GridEX1.GetValue("SHIP_TO_DIST_ID")
    End Sub
    Private Overloads Sub ClearControl()
        Me.txtOARef.Text = ""
        Me.dtPicOADate.Text = ""
        Me.mcbDistributor.Text = ""
        Me.mcbDistributor.Value = Nothing
    End Sub

    Private Sub refreshData()
        Me.SFG = StateFillingGrid.fiiling
        Me.SFM = StateFillingMCB.Filling
        If Me.Mode = ModeSave.Update Then
            If Me.UM = UpdateMode.FromOriginal Then
                Me.clsOA = New NufarmBussinesRules.OrderAcceptance.OARegistering()
                Me.clsOA.CreateViewPORefNO()
                Me.BindMulticolumnCombo(Me.mcbPOREFNO, Me.clsOA.ViewPO(), "PO_REF_NO", "PO_REF_NO")
                If Me.clsOA.OAHasReferencedData(Me.OA_ID) = True Then
                    Me.UnabledControl()
                    Me.ButtonEntry1.btnInsert.Enabled = False
                    Me.ButtonEntry1.btnAddNew.Enabled = False
                    Me.btnDelete.Visible = False
                Else
                    Me.EnabledControl()
                    Me.txtOARef.ReadOnly = True
                    Me.ButtonEntry1.Enabled = True
                    Me.ButtonEntry1.btnAddNew.Enabled = False
                    Me.ButtonEntry1.btnInsert.Text = "&Update"
                    Me.btnDelete.Visible = True
                End If
            Else
                Me.UnabledControl() : Me.ClearControl()
                Me.ButtonEntry1.btnInsert.Text = "&Insert"
                Me.ButtonEntry1.btnInsert.Enabled = False
                Me.ButtonEntry1.btnAddNew.Enabled = True
                Me.ButtonEntry1.btnAddNew.Focus()
            End If
        Else
            If Me.HasLoad = False Then
                Me.clsOA = New NufarmBussinesRules.OrderAcceptance.OARegistering()
                Me.clsOA.CreateViewPORefNO()
                Me.BindMulticolumnCombo(Me.mcbPOREFNO, Me.clsOA.ViewPO(), "PO_REF_NO", "PO_REF_NO")
                Me.clsOA.CreateViewterritory() : Me.UnabledControl() : Me.mcbPOREFNO.ReadOnly = False
            Else
                Me.UnabledControl() : Me.mcbPOREFNO.ReadOnly = True
            End If
            Me.ButtonEntry1.btnAddNew.Enabled = True
            Me.ButtonEntry1.btnInsert.Enabled = False
            Me.ButtonEntry1.btnInsert.Text = "&Insert"
            Me.btnOK.Enabled = True
        End If
    End Sub
    Private Sub BindMulticolumnCombo(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal dtv As DataView, ByVal DisplayMember As String, ByVal ValueMember As String)
        Me.SFM = StateFillingMCB.Filling
        mcb.DataSource = dtv
        If dtv Is Nothing Then
            Me.SFM = StateFillingMCB.HasFilled
            Return
        End If
        mcb.DisplayMember = DisplayMember
        mcb.ValueMember = ValueMember
        mcb.DropDownList.RetrieveStructure()
        mcb.DroppedDown = True
        For Each col As Janus.Windows.GridEX.GridEXColumn In mcb.DropDownList.Columns
            If col.Type Is Type.GetType("System.DateTime") Then
                'col.FormatString = "D" : col.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                col.Visible = False
            End If
            If col.Visible Then
                col.AutoSize()
            End If
        Next
        'mcb.SetDataBinding(dtv, "")
        Me.SFM = StateFillingMCB.HasFilled
        mcb.DroppedDown = False
    End Sub
#End Region

#Region " Function "
    Private Function IsValid() As Boolean
        If Me.mcbPOREFNO.Value Is Nothing Or Me.mcbPOREFNO.SelectedIndex <= -1 Then
            MessageBox.Show(Me.mcbPOREFNO, "PO_REF_NO is Null !." & vbCrLf & "Please Defined PO_REF_NO.")
            'Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbPOREFNO), Me.mcbPOREFNO, 2000)
            ''Me.baseBallonSmall.SetBalloonCaption(Me.mcbPOREFNO, "PO_REF_NO is NULL.")
            ''Me.baseBallonSmall.SetBalloonText(Me.mcbPOREFNO, "Please Defined PO_REF_NO.")
            ''Me.baseBallonSmall.ShowBalloon(Me.mcbPOREFNO)
            'Me.mcbPOREFNO.Focus()
            Return False
        ElseIf Me.mcbDistributor.Value Is Nothing Then
            MessageBox.Show(Me.mcbDistributor, "Distributor to be shipped is Null !." & vbCrLf & "Please Defined distributor name.")
            'Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbDistributor), Me.mcbDistributor, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.mcbDistributor, "DISTRIBUTOR TO SHIP is NULL.")
            'Me.baseBallonSmall.SetBalloonText(Me.mcbDistributor, "Please Defined distributor to ship.")
            'Me.baseBallonSmall.ShowBalloon(Me.mcbDistributor)
            Me.mcbDistributor.Focus()
            Return False
        ElseIf Me.txtOARef.Text = "" Then
            MessageBox.Show(Me.txtOARef, "OA_REF_NO is Null !." & vbCrLf & "Please Defined OA_REF_NO.")
            'Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtOARef), Me.txtOARef, 2000)
            ''Me.baseBallonSmall.SetBalloonCaption(Me.txtOARef, "OA_REF is NULL.")
            ''Me.baseBallonSmall.SetBalloonCaption(Me.txtOARef, "Please defined OA_REF.")
            ''Me.baseBallonSmall.ShowBalloon(Me.txtOARef)
            'Me.txtOARef.Focus()
            '    Return False
            'ElseIf (Me.txtShare1.Value Is Nothing) Or (Me.txtShare1.Value = 0) Then
            '    MessageBox.Show(Me.txtShare1, "Share % is Null !." & vbCrLf & "Share % must not be Null / Zero.")
            'Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtShare), Me.txtShare, 2000)
            ''Me.baseBallonSmall.SetBalloonCaption(Me.txtShare, "Share % is NULL.")
            ''Me.baseBallonSmall.SetBalloonText(Me.txtShare, "Share % must not be NULL/Zero.")
            ''Me.baseBallonSmall.ShowBalloon(Me.txtShare)
            'Me.txtShare.Focus()
            Return False
        ElseIf Me.dtPicOADate.Text = "" Then
            MessageBox.Show(Me.dtPicOADate, "Date is Null!." & vbCrLf & "Date must not be Null.")
            Return False
        ElseIf Me.mcbTM.SelectedIndex <= -1 Then
            Me.ShowMessageInfo("Please define TM") : Return False
            'ElseIf Me.chkTerritory.DropDownDataSource Is Nothing Then
            '    Me.baseTooltip.Show("Please define Territory", Me.chkTerritory, 3000)
            '    Return False
            'ElseIf IsNothing(Me.chkTerritory.CheckedValues()) Then
            '    Me.baseTooltip.Show("Please define Territory", Me.chkTerritory, 3000)
            '    Return False
        End If
        Return True
    End Function
    'Private Function generateOANum(ByVal PO_NUM As String) As String
    '    Dim num As String = "00000"
    '    Dim RetVal As String = ""
    '    Try
    '        Dim OACount As String = Me.clsOA.GetOACount().ToString()
    '        If OACount = "0" Then
    '            OACount = "1"
    '        ElseIf OACount = "99999" Then
    '            OACount = "1"
    '        End If
    '        Dim X As Integer = num.Length - OACount.Length
    '        num = num.Remove(X - 1, OACount.Length)
    '        num &= OACount
    '        'dim PO_REF_NO AS String = ME.clsOA.GE
    '        RetVal = PO_NUM & "-" & num
    '    Catch ex As Exception

    '    End Try
    '    Return RetVal
    'End Function
#End Region

#Region " Event Procedure "

    Private Sub btnFilter_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.btnClick
        Try
            Me.clsOA.SearchPoRefNo(Me.mcbPOREFNO.Text)
            Me.BindMulticolumnCombo(Me.mcbPOREFNO, Me.clsOA.ViewPO(), "PO_REF_NO", "PO_REF_NO")
            Dim itemCount As Integer = Me.mcbPOREFNO.DropDownList.RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mcbPOREFNO_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbPOREFNO.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFM = StateFillingMCB.Filling Then
                Return
            End If
            If Me.Mode = ModeSave.Save Then
                If (Me.mcbPOREFNO.Value Is Nothing) Or (Me.mcbPOREFNO.SelectedIndex = -1) Then
                    Me.BindMulticolumnCombo(Me.mcbDistributor, Nothing, Nothing, Nothing)
                    Me.txtPORefDate.Text = "" : Me.txtProject.Text = ""
                    Me.txtDistributor.Text = "" : Me.UnabledControl()
                    Me.mcbPOREFNO.ReadOnly = False : Me.txtOARef.Text = ""
                    Me.txtOARef.ReadOnly = True:
                    Me.ButtonEntry1.btnInsert.Text = "&Insert"
                    Me.ButtonEntry1.btnInsert.Enabled = False
                    Return
                End If
            ElseIf Me.UM = UpdateMode.FromGrid Then
                If Me.mcbPOREFNO.SelectedItem Is Nothing Then
                    Me.BindMulticolumnCombo(Me.mcbDistributor, Nothing, Nothing, Nothing)
                    Me.txtPORefDate.Text = "" : Me.txtProject.Text = ""
                    Me.txtDistributor.Text = "" : Me.UnabledControl()
                    Me.mcbPOREFNO.ReadOnly = False : Me.txtOARef.Text = ""
                    Me.ButtonEntry1.btnInsert.Text = "&Insert"
                    Me.ButtonEntry1.btnInsert.Enabled = False
                    Return
                End If
            End If
            Me.txtOARef.ReadOnly = False
            Me.txtDistributor.Text = Me.mcbPOREFNO.DropDownList.GetValue("DISTRIBUTOR_NAME")
            Me.txtPORefDate.Text = String.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(Me.mcbPOREFNO.DropDownList.GetValue("PO_REF_DATE")))
            Me.clsOA.CreateViewShipTo(Me.mcbPOREFNO.DropDownList.GetValue("DISTRIBUTOR_ID"), Me.txtDistributor.Text)
            Me.mcbDistributor.Value = Nothing
            Me.BindMulticolumnCombo(Me.mcbDistributor, Me.clsOA.ViewShipTo(), "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
            'Me.mcbterritory1.Value = Me.clsOA.GetDistributorTerritory(Me.clsOA.ViewPO(index)("DISTRIBUTOR_ID"))
            If Me.Mode = ModeSave.Save Then
                Me.txtOARef.Text = Me.generateOANum(Me.mcbPOREFNO.Value.ToString())
            End If
            Me.dtPicOADate.MinDate = Convert.ToDateTime(Me.mcbPOREFNO.DropDownList.GetValue("PO_REF_DATE"))
            'Dim index As Integer = Me.clsOA.ViewPO().Find(Me.mcbPOREFNO.Value)
            'If index <> -1 Then
            '    Me.txtDistributor.Text = Me.clsOA.ViewPO(index)("DISTRIBUTOR_NAME")
            '    Me.txtPORefDate.Text = Convert.ToDateTime(Me.clsOA.ViewPO(index)("PO_REF_DATE")).ToLongDateString()
            '    'If Not IsNothing(Me.clsOA.ViewPO(index)("PROJ_REF_NO")) Then
            '    '    If Me.clsOA.ViewPO(index)("PROJ_REF_NO") Is DBNull.Value Then
            '    '        Me.txtProject.Text = ""
            '    '    Else
            '    '        Me.txtProject.Text = Me.clsOA.ViewPO(index)("PROJ_REF_NO").ToString()
            '    '    End If
            '    'Else
            '    '    Me.txtProject.Text = ""
            '    'End If
            '    Me.clsOA.CreateViewShipTo(Me.clsOA.ViewPO(index)("DISTRIBUTOR_ID").ToString(), Me.txtDistributor.Text)
            '    Me.mcbDistributor.Value = Nothing
            '    Me.BindMulticolumnCombo(Me.mcbDistributor, Me.clsOA.ViewShipTo(), "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
            '    'Me.mcbterritory1.Value = Me.clsOA.GetDistributorTerritory(Me.clsOA.ViewPO(index)("DISTRIBUTOR_ID"))
            '    If Me.Mode = ModeSave.Save Then
            '        Me.txtOARef.Text = Me.generateOANum(Me.mcbPOREFNO.Value.ToString())
            '    End If
            'Else
            '    Me.ClearControl(Me.grpOA)
            '    Me.ClearControl(Me.grpPO)
            'End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            If Not Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            If Me.SFG = StateFillingGrid.fiiling Then
                Return
            End If
            Me.Mode = ModeSave.Update
            Me.UM = UpdateMode.FromGrid
            Me.InflateData()
            If Me.clsOA.OAHasReferencedData(Me.GridEX1.GetValue("OA_ID").ToString()) = True Then
                Me.UnabledControl()
            Else
                Me.EnabledControl()
                Me.txtOARef.ReadOnly = True
                Me.ButtonEntry1.btnInsert.Text = "&Update"
                Me.ButtonEntry1.btnInsert.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Not e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            If Me.clsOA.OAHasReferencedData(Me.GridEX1.GetValue("OA_ID").ToString()) = True Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                e.Cancel = True
                Me.GridEX1.Refetch()
                Return
            ElseIf Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Me.GridEX1.Refetch()
                Return
            Else
                Me.clsOA.DeleteOA(Me.GridEX1.GetValue("OA_ID"))
                e.Cancel = False
                Me.GridEX1.UpdateData()
                Me.ButtonEntry1.btnInsert.Text = "&Insert"
                Me.ButtonEntry1.btnInsert.Enabled = False
                Me.ClearControl(Me.grpPO)
                Me.ClearControl()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnChekExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChekExisting.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.txtOARef.Text = "" Then
                Me.baseTooltip.SetToolTip(Me.txtOARef, "OA_REF is Null !." & vbCrLf & "Please Suply OA_REF.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtOARef), Me.txtOARef, 2000)
                Me.txtOARef.Focus()
                Return
            ElseIf Me.mcbTM.SelectedIndex <= -1 Then
                Me.baseTooltip.Show("Please define TM_ID", Me.mcbTM, 300)
                Me.mcbTM.Focus() : Return
            End If
            If Me.clsOA.IsExistedOA(Me.txtOARef.Text.Trim(), Me.mcbTM.Value.ToString()) = True Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
            Else
                Me.ShowMessageInfo(Me.MessageDataSaveToAdd)
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ButtonEntry1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEntry1.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                Case "btnAddNew"
                    Me.ClearControl() : Me.UnabledControl()
                    Me.mcbPOREFNO.ReadOnly = False : Me.ButtonEntry1.btnInsert.Text = "&Insert"
                    If Not IsNothing(Me.mcbPOREFNO.Value) Then
                        Me.txtOARef.ReadOnly = False : Me.txtOARef.Focus()
                    Else
                        Me.mcbPOREFNO.Focus()
                    End If
                    Me.Mode = ModeSave.Save
                Case "btnInsert"
                    If Me.IsValid() = False Then : Return : End If
                    If Me.Mode = ModeSave.Save Then
                        If Me.clsOA.IsExistedOA(Me.txtOARef.Text) = True Then
                            Me.baseTooltip.SetToolTip(Me.txtOARef, Me.txtOARef.Text & " Has existed in database .!")
                            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtOARef), Me.txtOARef, 2000)
                            Return
                        End If
                    End If
                    Me.clsOA.OA_ID = Me.txtOARef.Text.Trim()
                    Me.clsOA.DISTRIBUTOR_NAME = Me.mcbDistributor.Text
                    Me.clsOA.DSTRIBUTOR_ID = Me.mcbDistributor.Value.ToString()
                    Me.clsOA.OA_DATE = CObj(Me.dtPicOADate.Value.ToShortDateString())
                    Me.clsOA.PO_REF_NO = Me.mcbPOREFNO.Value.ToString()
                    Me.SFG = StateFillingGrid.fiiling
                    Me.ShowMessageInfo(Me.MessageSavingSucces) : Me.refreshData()
                Case "btnClose"
                    Me.Close()
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ButtonEntry1_btnClick")
        Finally
            Me.SFG = StateFillingGrid.HasFilled
            Me.SFM = StateFillingMCB.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OrderAcceptance_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not IsNothing(Me.clsOA) Then
                Me.clsOA.Dispose(True)
                Me.clsOA = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub OrderAcceptance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.AcceptButton = Me.btnOK
            Me.CancelButton = Me.btnCancel
            If Me.Mode = ModeSave.Save Then
                Me.dtPicOADate.MaxDate = NufarmBussinesRules.SharedClass.ServerDate()
            End If
            Me.Location = New Point(150, 109)
        Catch ex As Exception
        Finally
            Me.HasLoad = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mcbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbDistributor.ValueChanged
        Try
            If Me.SFM = StateFillingMCB.Filling Then
                Return
            End If
            If Me.mcbDistributor.SelectedItem Is Nothing Then
                Return
            End If
            Me.clsOA.GetTM(Me.mcbDistributor.Value.ToString())
            Me.BindMulticolumnCombo(Me.mcbTM, Me.clsOA.ViewTM(), "SHIP_TO_ID", "SHIP_TO_ID")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtOARef_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOARef.TextChanged
        Try
            If Me.Mode = ModeSave.Save Then
                If Me.txtOARef.Text <> "" Then
                    Me.EnabledControl()
                    Me.ButtonEntry1.btnInsert.Enabled = True
                    Me.dtPicOADate.ReadOnly = False
                    Me.txtOARef.ReadOnly = True
                Else
                    Me.UnabledControl()
                    Me.mcbPOREFNO.ReadOnly = False
                    Me.txtOARef.ReadOnly = True
                    Me.txtOARef.Focus()
                    Me.ButtonEntry1.btnInsert.Enabled = False
                    Me.dtPicOADate.Value = NufarmBussinesRules.SharedClass.ServerDate()
                End If
            ElseIf Me.UM = UpdateMode.FromOriginal Then
                Me.clsOA = New NufarmBussinesRules.OrderAcceptance.OARegistering
                Me.clsOA.GetRowByOARefNO(Me.txtOARef.Text)
                Me.SFM = StateFillingMCB.Filling
                Me.mcbPOREFNO.Value = Me.clsOA.PO_REF_NO
                'Me.txtDistributor.Text = Me.mcbPOREFNO.DropDownList.GetValue("DISTRIBUTOR_NAME").ToString()
                Me.txtPORefDate.Text = Me.clsOA.PO_REF_DATE.ToLongDateString()
                'Me.mcbDistributor.Value = Me.DISTRIBUTOR_ID
                Me.dtPicOADate.Value = Convert.ToDateTime(Me.clsOA.OA_DATE)
                Me.txtDistributor.Text = Me.DISTRIBUTOR_NAME
                Me.clsOA.CreateViewShipTo(Me.DISTRIBUTOR_ID, Me.DISTRIBUTOR_NAME) 'SHIP TO DISTRIBUTOR
                Me.BindMulticolumnCombo(Me.mcbDistributor, Me.clsOA.ViewShipTo(), "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
                'AMBIL DISTRIBUTOR_ID BY OA_ID
                Dim DistributorShipTo As String = Me.clsOA.GetShipToDistributorByOA_ID(Me.OA_ID)
                Me.mcbDistributor.Value = DistributorShipTo
                Me.clsOA.GetTM(Me.mcbDistributor.Value.ToString())
                Me.BindMulticolumnCombo(Me.mcbTM, Me.clsOA.ViewTM(), "SHIP_TO_ID", "SHIP_TO_ID")
                Me.mcbTM.Value = Me.clsOA.getTMByOAID(Me.OA_ID)
                'Me.dtPicOADate.Value = Convert.ToDateTime(Me.clsOA.OA_DATE)
                Dim DateOriginal As Date = Convert.ToDateTime(Me.clsOA.PO_REF_DATE)
                Me.dtPicOADate.IsNullDate = False
                Me.dtPicOADate.MinDate = DateOriginal
            End If
        Catch ex As Exception

        Finally
            Me.SFM = StateFillingMCB.HasFilled
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                Return
            End If
            If Me.clsOA.OAHasReferencedData(Me.txtOARef.Text) = True Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                Return
            End If
            Me.clsOA.DeleteOA(Me.txtOARef.Text)
            Me.ShowMessageInfo(Me.MessageSuccesDelete)
            Me.ClearControl()
            Me.mcbPOREFNO.Value = Nothing
            Me.txtPORefDate.Text = ""
            Me.txtProject.Text = ""
            Me.txtDistributor.Text = ""
            Me.Mode = ModeSave.Save
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.Mode = ModeSave.Save Then
                If Me.IsValid() = False Then : Return : End If
            End If
            If Me.Mode = ModeSave.Save Then
                If Me.clsOA.IsExistedOA(Me.mcbPOREFNO.Text, Me.mcbTM.Value.ToString()) = True Then
                    Me.baseTooltip.Show(Me.MessageDataHasExisted, Me.mcbTM, 3000)
                    Return
                End If
            End If
            If Me.Mode = ModeSave.Save Then
                Me.clsOA.OA_ID = Me.generateOANum(Me.mcbPOREFNO.Value.ToString())
            Else
                Me.clsOA.OA_ID = Me.OA_ID
            End If
            Me.clsOA.DISTRIBUTOR_NAME = Me.mcbDistributor.Text
            Me.clsOA.DSTRIBUTOR_ID = Me.mcbDistributor.Value.ToString()
            Me.clsOA.OA_DATE = Convert.ToDateTime(Me.dtPicOADate.Value.ToShortDateString())
            Me.clsOA.PO_REF_NO = Me.mcbPOREFNO.Text.TrimEnd()
            Me.clsOA.RunNumber = Me.RunNumber : Me.SFG = StateFillingGrid.fiiling
            Dim SHIP_TO_IDS As New Collection() ''RUBAH MANING RUBAH MANING SON,TAPI GUWA ENJOY
            'GW PINGIN NYA SEH TRUS AJA RUBAH SAMPAI GW MATI..BILA PERLU MAH.
            SHIP_TO_IDS.Clear() : SHIP_TO_IDS.Add(Me.mcbTM.Value.ToString())
            If Me.Mode = ModeSave.Save Then
                Me.clsOA.SaveOA("Save", SHIP_TO_IDS)
            Else
                Me.clsOA.SaveOA("Update", SHIP_TO_IDS)
            End If
            NufarmBussinesRules.SharedClass.OA_REF_NO = Me.txtOARef.Text : Me.Close()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnOK_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFilterTM_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterTM.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rowfilter As String = "MANAGER LIKE '%" & Me.mcbTM.Text & "%'"
            Me.clsOA.ViewTM().RowFilter = rowfilter
            Me.BindMulticolumnCombo(Me.mcbTM, Me.clsOA.ViewTM(), "SHIP_TO_ID", "SHIP_TO_ID")
            Dim itemCount As Integer = Me.mcbTM.DropDownList.RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region
   
End Class
