Public Class GONReceivedBack
    Friend objStartDate As Object = Nothing
    Friend objEndDate As Object = Nothing
    Friend HasStamped As Boolean = False, HasSigned As Boolean = False
    Private isLoadedForm As Boolean = False
    Friend frmParent As SPPB = Nothing

    Private ReadOnly Property clsGON() As NufarmBussinesRules.OrderAcceptance.SPPB_Detail
        Get
            If Me.m_clsGON Is Nothing Then
                Me.m_clsGON = New NufarmBussinesRules.OrderAcceptance.SPPB_Detail()
            End If
            Return Me.m_clsGON
        End Get
    End Property
    Friend SPPB_NO As String = "", GON_NO As String = "", GOnReceiver As String = "", GT_ID As String = ""
    Private m_clsGON As NufarmBussinesRules.OrderAcceptance.SPPB_Detail
    Friend DistributorID As String = ""
    Private IsFillingCombo As Boolean = False

    Friend mode As NufarmBussinesRules.common.Helper.SaveMode = NufarmBussinesRules.common.Helper.SaveMode.Insert
    Private Sub ClearGonInformation()
        Me.lblFromDistributor.Text = ""
        Me.lblGON_DATE.Text = ""
        Me.lblSPPBNumber.Text = ""
        Me.lblSPPDate.Text = ""
        Me.lblTransporter.Text = ""
        Me.mcbGonReceiver.Text = ""
        Me.chkGonSigned.Checked = False
        Me.chkGonStamped.Checked = False
        Me.txtDescriptions.Text = ""
        Me.dtReceivedBackHere.Value = NufarmBussinesRules.SharedClass.ServerDate
    End Sub
    Private Sub FillCombo(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal DV As DataView)
        Me.IsFillingCombo = True
        mcb.SetDataBinding(DV, "")
        If DV Is Nothing Then : Me.IsFillingCombo = False : Return : End If
        mcb.DropDownList.RetrieveStructure()
        Select Case mcb.Name
            Case "mcbGONNO"
                mcb.DisplayMember = "GON_NO" : mcb.ValueMember = "GON_NO"
            Case "mcbGonReceiver"
                mcb.DisplayMember = "RECEIVED_BY" : mcb.ValueMember = "ID_RECEIVER"
        End Select
        mcb.DroppedDown = True
        For Each col As Janus.Windows.GridEX.GridEXColumn In mcb.DropDownList.Columns
            col.AutoSize()
        Next
        mcb.DroppedDown = False
        mcb.Value = Nothing
        Me.IsFillingCombo = False
    End Sub

    Private Sub GONReceivedBack_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsGON.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GONReceivedBack_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mode = NufarmBussinesRules.common.Helper.SaveMode.Insert Then
                Me.dtPicSPPBBUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
                Me.dtPicSPPBFrom.Value = Me.dtPicSPPBFrom.Value.AddDays(-92)
                Dim StartDate As DateTime = Me.dtPicSPPBFrom.Value
                Dim EndDate As DateTime = Me.dtPicSPPBBUntil.Value
                Me.objStartDate = StartDate.ToShortDateString()
                Me.objEndDate = EndDate.ToShortDateString()
            End If
            Dim DV As DataView = Nothing
            With Me.clsGON
                Select Case Me.mode
                    Case NufarmBussinesRules.common.Helper.SaveMode.Insert
                        .CreateGONData(Convert.ToDateTime(Me.dtPicSPPBFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicSPPBBUntil.Value.ToShortDateString()))
                        DV = .getGOnData(String.Empty)
                        Me.FillCombo(Me.mcbGONNO, DV)
                    Case NufarmBussinesRules.common.Helper.SaveMode.Update
                        DV = .getGonData_1(Me.SPPB_NO)
                        Me.FillCombo(Me.mcbGONNO, DV)
                        Me.mcbGONNO.Value = Me.GON_NO
                End Select
                Me.mcbGonReceiver.Value = GOnReceiver
            End With
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GONReceivedBack_Load")
        Finally
            Me.isLoadedForm = True : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.dtPicSPPBFrom.Text <> "" And Me.dtPicSPPBBUntil.Text <> "" Then
                If Not Me.objStartDate.Equals(Me.dtPicSPPBFrom.Value.ToShortDateString()) Or Not Me.objEndDate.Equals(Me.dtPicSPPBBUntil.Value.ToShortDateString()) Then
                    Me.clsGON.CreateGONData(Convert.ToDateTime(Me.dtPicSPPBFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicSPPBBUntil.Value.ToShortDateString()))
                ElseIf Me.mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
                    Me.clsGON.CreateGONData(Convert.ToDateTime(Me.dtPicSPPBFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicSPPBBUntil.Value.ToShortDateString()))
                End If
            Else
                Me.dtPicSPPBFrom.ReadOnly = False : Me.dtPicSPPBBUntil.ReadOnly = False
                Me.IsFillingCombo = True
                Me.dtPicSPPBBUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
                Me.dtPicSPPBFrom.Value = Me.dtPicSPPBFrom.Value.AddDays(-92)
                Me.dtPicSPPBFrom.Text = Me.dtPicSPPBFrom.Value
                Me.dtPicSPPBBUntil.Text = Me.dtPicSPPBBUntil.Value
                Me.objStartDate = Me.dtPicSPPBFrom.Value.ToShortDateString()
                Me.objEndDate = Me.dtPicSPPBBUntil.Value.ToShortDateString()
                Me.clsGON.CreateGONData(Convert.ToDateTime(Me.dtPicSPPBFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicSPPBBUntil.Value.ToShortDateString()))
            End If
            Dim DV As DataView = Me.clsGON.getGOnData(String.Empty)
            Me.FillCombo(Me.mcbGONNO, DV) : Me.chkGonSigned.Checked = False : Me.chkGonStamped.Checked = False
            Me.mcbGonReceiver.Text = "" : Me.mcbGonReceiver.Value = Nothing
            Me.dtPicSPPBFrom.ReadOnly = False : Me.dtPicSPPBBUntil.ReadOnly = False
            Me.ClearGonInformation()
            Me.mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnAddNew_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'If Me.mcbGonReceiver.Text = "" Then
            '    Me.baseTooltip.Show("Please enter gon receiver", Me.mcbGonReceiver, 2500) : Return
            'End If
            Me.Cursor = Cursors.WaitCursor
            With Me.clsGON
                .DistributorID = Me.DistributorID
                .GON_NO = Me.mcbGONNO.Value.ToString()
                .SPPB_NO = Me.lblSPPBNumber.Text
                .ReceivedGonDate = Convert.ToDateTime(Me.dtReceivedBackHere.Value.ToShortDateString())
                .HasStamped = Me.chkGonStamped.Checked
                .HasSigned = Me.chkGonSigned.Checked
                .Remark = RTrim(Me.txtDescriptions.Text)
                If Me.mcbGonReceiver.SelectedIndex <= -1 Then
                    .ReceiverID = ""
                Else
                    .ReceiverID = Me.mcbGonReceiver.Value.ToString()
                End If
                .GOnReceiverName = Me.mcbGonReceiver.Text
                .GT_ID = Me.GT_ID
                .SaveReceivedGON(Me.mode, False)
                If Not IsNothing(Me.frmParent) Then
                    Me.frmParent.MustReloadData = True
                End If
                If Me.mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
                    Me.Close()
                End If
            End With
            Me.objStartDate = Me.dtPicSPPBFrom.Value.ToShortDateString()
            Me.objEndDate = Me.dtPicSPPBBUntil.Value.ToShortDateString()
            Me.btnAddNew_Click(Me.btnAddNew, New EventArgs())
            Me.frmParent.MustReloadData = True
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnSave_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Try
            Me.clsGON.Dispose(True)
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mcbGONNO_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbGONNO.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.IsFillingCombo Then : Return : End If

            If Me.mcbGONNO.DataSource Is Nothing Then : Return : End If
            If Me.isLoadedForm Then
                Me.ClearGonInformation()
                If Me.mcbGONNO.SelectedIndex <= -1 Then : Return : End If
                If Me.mcbGONNO.SelectedItem Is Nothing Then : Return : End If
            End If
            Dim TGonReceiver As New DataTable("T_ListReceiver")
            Dim dtTable As DataTable = Nothing
            Dim transporter As Nufarm.Domain.Transporter = Nothing
            If Not Me.isLoadedForm Then
                dtTable = Me.clsGON.getGonDescription(Me.SPPB_NO, Me.GON_NO, TGonReceiver, transporter, False)
            Else
                dtTable = Me.clsGON.getGonDescription(Me.mcbGONNO.DropDownList.GetValue("SPPB_NO").ToString(), Me.mcbGONNO.Value.ToString(), TGonReceiver, transporter, False)
            End If
            'get gon detail by gon_no
            If dtTable.Rows.Count > 0 Then
                Me.lblFromDistributor.Text = dtTable.Rows(0)("DISTRIBUTOR_NAME").ToString()
                Me.DistributorID = dtTable.Rows(0)("DISTRIBUTOR_ID").ToString()
                Me.lblGON_DATE.Text = Convert.ToDateTime(dtTable.Rows(0)("GON_DATE")).ToLongDateString()
                Me.dtReceivedBackHere.MinDate = Convert.ToDateTime(dtTable.Rows(0)("GON_DATE")).ToLongDateString()
                If Not Me.isLoadedForm Then
                    Me.lblSPPBNumber.Text = Me.SPPB_NO
                Else
                    Me.lblSPPBNumber.Text = Me.mcbGONNO.DropDownList.GetValue("SPPB_NO").ToString()
                End If
                Me.lblSPPDate.Text = Convert.ToDateTime(dtTable.Rows(0)("SPPB_DATE")).ToLongDateString()
                Me.FillCombo(Me.mcbGonReceiver, TGonReceiver.DefaultView())
                If Not IsNothing(transporter) Then
                    Me.GT_ID = transporter.GT_ID
                    Me.lblTransporter.Text = transporter.NameApp
                Else
                    Me.GT_ID = ""
                    Me.lblTransporter.Text = ""
                End If
            Else
                Me.ClearGonInformation()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_mcbGONNO_ValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSearchGON_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchGON.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            SearchData()
            Dim itemCount As Integer = Me.mcbGONNO.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnSearchGON_btnClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SearchData()
        If Not Me.objStartDate.Equals(Me.dtPicSPPBFrom.Value.ToShortDateString()) Or Not Me.objEndDate.Equals(Me.dtPicSPPBBUntil.Value.ToShortDateString()) Then
            Me.clsGON.CreateGONData(Convert.ToDateTime(Me.dtPicSPPBFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicSPPBBUntil.Value.ToShortDateString()))
            Me.objStartDate = Me.dtPicSPPBFrom.Value.ToShortDateString()
            Me.objEndDate = Me.dtPicSPPBBUntil.Value.ToShortDateString()
        End If
        Dim DV As DataView = Me.clsGON.getGOnData(Me.mcbGONNO.Text)
        Me.FillCombo(Me.mcbGONNO, DV)
    End Sub
    Private Sub dtPicSPPBFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicSPPBFrom.ValueChanged
        Try
            If Not Me.isLoadedForm Then : Return : End If
            If Me.IsFillingCombo Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            SearchData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub dtPicSPPBBUntil_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicSPPBBUntil.ValueChanged
        Try
            If Not Me.isLoadedForm Then : Return : End If
            If Me.IsFillingCombo Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            SearchData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

End Class
