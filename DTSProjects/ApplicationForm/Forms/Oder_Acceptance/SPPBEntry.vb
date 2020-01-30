Public Class SPPBEntry

#Region " Declarasi "
    Private m_clsSPPBDetail As NufarmBussinesRules.OrderAcceptance.SPPB_Detail
    Private SFG As StateFillingGrid
    Private SFM As StateFillingMCB
    Friend SaveMode As ModeSave
    Private SVM As SetValueMode
    Private Counter As Integer
    Private ChangedGonNumber As String = ""
    Private ChangedGSNo As String = ""
    Private ChangedGonDate As DateTime
    Private isClosingForm As Boolean = False
    Friend frmParent As SPPB = Nothing
    Private IsFormLoaded As Boolean = False
    Public ReleaseDate As Object = DBNull.Value
    Private isReleasedDate As Boolean = False
    Private DVTransporter As DataView = Nothing
#End Region

#Region " ENUM "

    Private Enum StateFillingGrid
        HasFilled
        Filling
        Disposing
    End Enum

    Private Enum StateFillingMCB
        HasFilled
        Filling
    End Enum

    Friend Enum ModeSave
        Save
        Update
    End Enum
    Private Enum SetValueMode
        HasChanged
        Changing
    End Enum

#End Region

#Region " SUB & Property "

    Private ReadOnly Property clsSPPBDetail() As NufarmBussinesRules.OrderAcceptance.SPPB_Detail
        Get
            If Me.m_clsSPPBDetail Is Nothing Then
                Me.m_clsSPPBDetail = New NufarmBussinesRules.OrderAcceptance.SPPB_Detail()
            End If
            Return Me.m_clsSPPBDetail
        End Get
    End Property

    Private Sub FillCategoriesValueList()
        Dim ColumnBrandPackID As Janus.Windows.GridEX.GridEXColumn = Me.grdGoneSequence.RootTable.Columns("BRANDPACK_ID")
        ColumnBrandPackID.EditType = Janus.Windows.GridEX.EditType.DropDownList
        'Set HasValueList property equal to true in order to be able to use the ValueList property
        ColumnBrandPackID.HasValueList = True
        'Get the ValueList collection associated to this column
        Dim ValueList As Janus.Windows.GridEX.GridEXValueListItemCollection = ColumnBrandPackID.ValueList

        ValueList.PopulateValueList(Me.clsSPPBDetail.ViewBrandPack(), "BRANDPACK_ID", "BRANDPACK_NAME")
        ColumnBrandPackID.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        ColumnBrandPackID.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
        'Fill the ValueList
        'ValueList.PopulateValueList(Ds.Tables("Categories").DefaultView, "CategoryID", "CategoryName")
    End Sub

    Friend Sub InitializeData()
        'bindcombobox
        If Me.SaveMode = ModeSave.Save Then
            Me.clsSPPBDetail.CreateViewDistributorOA(False, False)
        Else
            Me.clsSPPBDetail.CreateViewDistributorOA(True, False)
        End If
        Me.BindMulticolumnCombo(Me.mcbDistributor, Me.clsSPPBDetail.ViewDistributorSPPB())

        ''fill GonTransPorter
        Dim dtTable As DataTable = Me.clsSPPBDetail.getTransporter(String.Empty, Me.SaveMode = ModeSave.Save)
        Me.BindMulticolumnCombo(Me.mcbTransporter, dtTable.DefaultView)
    End Sub

    Private Sub InflateDataFromMCB(ByVal OA_REF_NO As String)
        '    Me.txtPoREfNo.Text = Me.mcbOA_REF_NO.DropDownList.GetValue("PO_REF_NO")
        'Else
        Dim dtview As DataView = CType(Me.mcbOA_REF_NO.DataSource, DataView)
        Dim index As Integer = dtview.Find(OA_REF_NO)
        If index <> -1 Then
            Me.txtPODate.Text = String.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(dtview(index)("PO_REF_DATE")))
            Me.txtPoREfNo.Text = dtview(index)("PO_REF_NO").ToString()
        End If
        If Me.SaveMode = ModeSave.Save Then
            If Not Me.mcbOA_REF_NO.SelectedItem Is Nothing Then
                Me.clsSPPBDetail.GetRowOAREf(Me.mcbOA_REF_NO.Value.ToString(), False)
            Else
                Return
            End If
        Else
            If Not Me.mcbOA_REF_NO.Value Is Nothing Then
                Me.clsSPPBDetail.GetRowOAREf(Me.mcbOA_REF_NO.Value.ToString(), False)
            Else
                Return
            End If
        End If
        Me.txtSPPBNo.Text = Me.clsSPPBDetail.SPPB_NO.ToString()
        Me.dtPicGonDate.Text = ""
        If Me.txtSPPBNo.Text = "Undefined" Then
            Me.dtPicSPPBDate.Text = ""
            Me.txtSPPBNo.SelectAll()
            Me.txtSPPBNo.Focus()
            Return
        End If
        If Me.txtSPPBNo.Text <> "" And Me.txtSPPBNo.Text <> "Undefined" Then
            'Me.dtPicSPPBDate.MinDate = DateTime.Parse(Me.txtOADate.Text.Trim())
            Me.dtPicSPPBDate.Text = String.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(Me.clsSPPBDetail.SPPBDate))
            Me.SaveMode = ModeSave.Update
            If Not IsDBNull(Me.ReleaseDate) Then
                Me.dtPicRelease.Checked = True
                Me.dtPicRelease.Value = Convert.ToDateTime(Me.ReleaseDate)
                Me.dtPicRelease.Text = Me.dtPicRelease.Value
            Else
                Me.dtPicRelease.Checked = False : Me.dtPicRelease.Text = ""
            End If
            Me.txtSPPBNo_KeyPress(Me.txtSPPBNo, New KeyPressEventArgs(Chr(13)))
        End If

        'me.txtOADate.Text = Convert.ToDatetime(me.clsSPPBDetail.OA_DATE)
    End Sub

    Private Sub BindMulticolumnCombo(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal dtView As DataView)

        Me.SFM = StateFillingMCB.Filling
        mcb.Value = Nothing
        mcb.SetDataBinding(dtView, "")
        If mcb.Name = "mcbTransporter" Then
            mcb.DisplayMember = "TRANSPORTER_NAME" : mcb.ValueMember = "GT_ID" : mcb.DroppedDown = True : Application.DoEvents()
            mcb.DropDownList.RetrieveStructure()
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.mcbTransporter.DropDownList.Columns
                col.AutoSize()
            Next
            mcb.DroppedDown = False : Application.DoEvents()

        End If
        Me.SFM = StateFillingMCB.HasFilled
    End Sub

    Private Shadows Sub ClearControl()
        Me.SFM = StateFillingMCB.Filling
        Me.mcbDistributor.Value = Nothing
        Me.mcbOA_REF_NO.Value = Nothing
        Me.txtSPPBNo.Text = ""
        Me.dtPicSPPBDate.Text = ""
        Me.txtGon_No.Text = ""
        'Me.txtOADate.Text = ""
        Me.txtPODate.Text = ""
        Me.txtPoREfNo.Text = ""
        Me.txtSPPBNo.Text = ""
        'Me.txtShipTo.Text = ""
        Me.cmbGonSequence.Text = ""
        Me.dtPicGonDate.Text = ""
        Me.cmbGonSequence.Items.Clear()
        Me.BindMulticolumnCombo(Me.mcbOA_REF_NO, Nothing)
    End Sub

    Private Sub SetSelectableGonSequence(ByVal GS_NO As Integer)
        If Me.grdGoneSequence.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdGoneSequence.RootTable.Columns
                If (col Is Me.grdGoneSequence.RootTable.Columns("BRANDPACK_ID")) Or (col Is _
                Me.grdGoneSequence.RootTable.Columns("GON_1_QTY")) Or (col Is Me.grdGoneSequence.RootTable.Columns("GON_2_QTY")) _
               Or (col Is Me.grdGoneSequence.RootTable.Columns("GON_3_QTY")) Or (col Is Me.grdGoneSequence.RootTable.Columns("GON_4_QTY")) _
               Or (col Is Me.grdGoneSequence.RootTable.Columns("GON_5_QTY")) Or (col Is Me.grdGoneSequence.RootTable.Columns("GON_6_QTY")) _
                   Or (col Is Me.grdGoneSequence.RootTable.Columns("REMARK")) Then
                    col.Selectable = True
                Else
                    col.Selectable = False
                End If
            Next
            Select Case CInt(GS_NO)
                Case 1
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = True
                Case 2
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_2_QTY").Selectable = True
                Case 3
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_2_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_3_QTY").Selectable = True
                Case 4
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_2_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_3_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_4_QTY").Selectable = True
                Case 5
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_2_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_3_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_4_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_5_QTY").Selectable = True
                Case 6
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_2_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_3_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_4_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_5_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_6_QTY").Selectable = False
            End Select
        ElseIf Me.grdGoneSequence.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
            If Me.SaveMode = ModeSave.Save Then
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdGoneSequence.RootTable.Columns
                    If (col Is Me.grdGoneSequence.RootTable.Columns("SPPB_QTY")) _
                  Or (col Is Me.grdGoneSequence.RootTable.Columns("STATUS")) Or (col Is Me.grdGoneSequence.RootTable.Columns("BALANCE")) Then
                        col.Selectable = False
                    Else
                        col.Selectable = True
                    End If
                Next
            Else
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdGoneSequence.RootTable.Columns
                    If (col Is Me.grdGoneSequence.RootTable.Columns("SPPB_QTY")) Or (col Is Me.grdGoneSequence.RootTable.Columns("SPPB_NO")) Or _
                    (col Is Me.grdGoneSequence.RootTable.Columns("BRANDPACK_ID")) _
                  Or (col Is Me.grdGoneSequence.RootTable.Columns("STATUS")) Or (col Is Me.grdGoneSequence.RootTable.Columns("BALANCE")) Then
                        col.Selectable = False
                    Else
                        col.Selectable = True
                    End If
                Next
            End If
            Select Case CInt(GS_NO)
                Case 1
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = True
                Case 2
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_2_QTY").Selectable = True
                Case 3
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_2_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_3_QTY").Selectable = True
                Case 4
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_2_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_3_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_4_QTY").Selectable = True
                Case 5
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_2_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_3_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_4_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_5_QTY").Selectable = True
                Case 6
                    Me.grdGoneSequence.RootTable.Columns("GON_1_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_2_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_3_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_4_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_5_QTY").Selectable = False
                    Me.grdGoneSequence.RootTable.Columns("GON_6_QTY").Selectable = True
            End Select
        End If

    End Sub


    Private Sub fillItem()
        'Dim BALANCE As Decimal = 0
        If Not Me.clsSPPBDetail.ViewBrandPack() Is Nothing Then
            If Me.clsSPPBDetail.ViewBrandPack().Count <= 0 Then
                Throw New Exception("No item(s) included in OA")
            End If
        Else
            Throw New Exception("Please define OA_REF_NO")
        End If
        Dim dtview As DataView = Me.clsSPPBDetail.ViewBrandPack()
        Dim dtViewSPPB As DataView = Me.clsSPPBDetail.ViewSPPBDetail()
        dtViewSPPB.Sort = "SPPB_BRANDPACK_ID ASC"
        Dim drv As DataRowView = Nothing
        For i As Integer = 0 To dtview.Count - 1
            Dim PO_BRANDPACK_ID As String = Me.txtPoREfNo.Text & "" & dtview(i)("BRANDPACK_ID").ToString()
            Dim OA_BRANDPACK_ID As String = Me.mcbOA_REF_NO.Value.ToString() & "" & PO_BRANDPACK_ID
            Dim SPPB_BRANDPACK_ID As String = Me.txtSPPBNo.Text.Trim() & "" & OA_BRANDPACK_ID
            Dim Index As Integer = dtViewSPPB.Find(SPPB_BRANDPACK_ID)
            Dim TotalOAQty As Decimal = 0, Remark As String = ""
            TotalOAQty = Me.clsSPPBDetail.GetTotalOAQty(OA_BRANDPACK_ID, False, False, Remark)
            If (Index = -1) Then
                drv = dtViewSPPB.AddNew()
                drv("SPPB_BRANDPACK_ID") = SPPB_BRANDPACK_ID
                drv("SPPB_NO") = Me.txtSPPBNo.Text.Trim()
                drv("OA_BRANDPACK_ID") = OA_BRANDPACK_ID
                drv("SPPB_QTY") = TotalOAQty
                drv("BALANCE") = TotalOAQty
                drv("STATUS") = "PENDING"
                drv("CREATE_BY") = NufarmBussinesRules.User.UserLogin.UserName
                drv("CREATE_DATE") = Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate().ToShortDateString())
                drv("BRANDPACK_ID") = dtview(i)("BRANDPACK_ID")
                drv("GON_1_QTY") = 0
                drv("GON_2_QTY") = 0
                drv("GON_3_QTY") = 0
                drv("GON_4_QTY") = 0
                drv("GON_5_QTY") = 0
                drv("GON_6_QTY") = 0
                drv("REMARK") = Remark
                drv.EndEdit()
            Else
                'TotalOAQty = Me.clsSPPBDetail.GetTotalOAQty(OA_BRANDPACK_ID, False, Remark)
                If Convert.ToDecimal(dtViewSPPB(Index)("SPPB_QTY")) <> TotalOAQty Then
                    dtViewSPPB(Index).BeginEdit()
                    dtViewSPPB(Index)("SPPB_QTY") = TotalOAQty
                    dtViewSPPB(Index)("MODIFY_DATE") = NufarmBussinesRules.SharedClass.ServerDate()
                    dtViewSPPB(Index).EndEdit()
                End If
                If (IsDBNull(dtViewSPPB(Index)("Remark"))) Then
                    dtViewSPPB(Index).BeginEdit()
                    dtViewSPPB(Index)("Remark") = Remark
                    dtViewSPPB(Index)("MODIFY_DATE") = NufarmBussinesRules.SharedClass.ServerDate()
                    dtViewSPPB(Index).EndEdit()
                ElseIf String.IsNullOrEmpty(dtViewSPPB(Index)("Remark")) Then
                    dtViewSPPB(Index).BeginEdit()
                    dtViewSPPB(Index)("Remark") = Remark
                    dtViewSPPB(Index)("MODIFY_DATE") = NufarmBussinesRules.SharedClass.ServerDate()
                    dtViewSPPB(Index).EndEdit()
                End If
                
            End If
            dtViewSPPB.Sort = "SPPB_BRANDPACK_ID ASC"
        Next
        Me.BindGrid(dtViewSPPB)
    End Sub
    Private Sub BindGrid(ByVal dtView As DataView)

        Me.SFG = StateFillingGrid.Filling
        If IsNothing(dtView) Then
            Me.grdGoneSequence.SetDataBinding(Nothing, "")
            Me.SFG = StateFillingGrid.HasFilled
            Return
        End If
        Me.grdGoneSequence.SetDataBinding(dtView, "")
        If Me.grdGoneSequence.RecordCount > 0 Then
            Dim z As Integer = 6
            While z > 0
                Dim SumGonQty As Decimal = Me.grdGoneSequence.GetTotal(Me.grdGoneSequence.RootTable.Columns("GON_" & z.ToString() & "_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum, Nothing)
                If (SumGonQty <= 0) Or (SumGonQty = Nothing) Then
                    Me.grdGoneSequence.RootTable.ColumnSets("GON_" & z.ToString()).Visible = False
                Else
                    Me.grdGoneSequence.RootTable.ColumnSets("GON_" & z.ToString()).Visible = True
                End If
                Me.grdGoneSequence.RootTable.Columns("GON_" & z.ToString() & "_TRANSPORTER_NAME").Selectable = False
                Me.grdGoneSequence.RootTable.Columns("GON_" & z.ToString() & "_TRANSPORTER_NAME").EditType = Janus.Windows.GridEX.EditType.NoEdit
                z -= 1
            End While
        Else
            For i As Int16 = 1 To 6
                Me.grdGoneSequence.RootTable.ColumnSets(i + 1).Visible = False
            Next
        End If
        Me.FillCategoriesValueList()
        Me.SFG = StateFillingGrid.HasFilled
    End Sub

    Private Sub FillCMBGonSequence(ByVal SM As ModeSave)
        Me.cmbGonSequence.Items.Clear()
        Select Case SM

            Case ModeSave.Save
                For i As Integer = 1 To 6
                    Me.cmbGonSequence.Items.Add(i)
                Next
            Case ModeSave.Update
                Dim GS_NO As Integer = Me.clsSPPBDetail.GetGonSequenceNo(Me.txtSPPBNo.Text.TrimEnd().TrimStart(), False)

                Select Case GS_NO
                    Case 0
                        Me.cmbGonSequence.Items.Add(1)
                    Case 1
                        Me.cmbGonSequence.Items.Add(1)
                        Me.cmbGonSequence.Items.Add(2)
                    Case 2
                        Me.cmbGonSequence.Items.Add(1)
                        Me.cmbGonSequence.Items.Add(2)
                        Me.cmbGonSequence.Items.Add(3)
                    Case 3
                        Me.cmbGonSequence.Items.Add(1)
                        Me.cmbGonSequence.Items.Add(2)
                        Me.cmbGonSequence.Items.Add(3)
                        Me.cmbGonSequence.Items.Add(4)
                    Case 4
                        Me.cmbGonSequence.Items.Add(1)
                        Me.cmbGonSequence.Items.Add(2)
                        Me.cmbGonSequence.Items.Add(3)
                        Me.cmbGonSequence.Items.Add(4)
                        Me.cmbGonSequence.Items.Add(5)
                    Case 5
                        Me.cmbGonSequence.Items.Add(1)
                        Me.cmbGonSequence.Items.Add(2)
                        Me.cmbGonSequence.Items.Add(3)
                        Me.cmbGonSequence.Items.Add(4)
                        Me.cmbGonSequence.Items.Add(5)
                        Me.cmbGonSequence.Items.Add(6)
                    Case 6
                        Me.cmbGonSequence.Items.Add(1)
                        Me.cmbGonSequence.Items.Add(2)
                        Me.cmbGonSequence.Items.Add(3)
                        Me.cmbGonSequence.Items.Add(4)
                        Me.cmbGonSequence.Items.Add(5)
                        Me.cmbGonSequence.Items.Add(6)
                End Select
        End Select
    End Sub

    Private Function IsValidDate() As Boolean
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_6_DATE")) Then
            'check gon_5_date
            If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_5_DATE")) Then
                If Convert.ToDatetime(Me.grdGoneSequence.GetValue("GON_6_DATE")) < Convert.ToDatetime(Me.grdGoneSequence.GetValue("GON_5_DATE")) Then
                    Me.ShowMessageInfo("GON_6_DATE < GON_5_DATE")
                    Me.grdGoneSequence.CancelCurrentEdit()
                    Return False
                End If
                'Else
                '    Me.ShowMessageInfo("GON_5_DATE is null")
                '    Me.grdGoneSequence.CancelCurrentEdit()
                '    Return False
            End If
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_5_DATE")) Then
            If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_4_DATE")) Then
                If Convert.ToDatetime(Me.grdGoneSequence.GetValue("GON_5_DATE")) < Convert.ToDatetime(Me.grdGoneSequence.GetValue("GON_4_DATE")) Then
                    Me.ShowMessageInfo("GON_5_DATE < GON_4_DATE")
                    Me.grdGoneSequence.CancelCurrentEdit()
                    Return False
                End If
                'Else
                '    Me.ShowMessageInfo("GON_4_DATE is null")
                '    Me.grdGoneSequence.CancelCurrentEdit()
                '    Return False
            End If
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_4_DATE")) Then
            If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_3_DATE")) Then
                If Convert.ToDatetime(Me.grdGoneSequence.GetValue("GON_4_DATE")) < Convert.ToDatetime(Me.grdGoneSequence.GetValue("GON_3_DATE")) Then
                    Me.ShowMessageInfo("GON_4_DATE < GON_3_DATE")
                    Me.grdGoneSequence.CancelCurrentEdit()
                    Return False
                End If
                'Else
                '    Me.ShowMessageInfo("GON_3_DATE is null")
                '    Me.grdGoneSequence.CancelCurrentEdit()
                '    Return False
            End If
        End If

        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_3_DATE")) Then
            If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_2_DATE")) Then
                If Convert.ToDatetime(Me.grdGoneSequence.GetValue("GON_3_DATE")) < Convert.ToDatetime(Me.grdGoneSequence.GetValue("GON_2_DATE")) Then
                    Me.ShowMessageInfo("GON_3_DATE < GON_3_DATE")
                    Me.grdGoneSequence.CancelCurrentEdit()
                    Return False
                End If
                'Else
                '    Me.ShowMessageInfo("GON_2_DATE is null")
                '    Me.grdGoneSequence.CancelCurrentEdit()
                '    Return False
            End If
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_2_DATE")) Then
            If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_1_DATE")) Then
                If Convert.ToDatetime(Me.grdGoneSequence.GetValue("GON_2_DATE")) < Convert.ToDatetime(Me.grdGoneSequence.GetValue("GON_1_DATE")) Then
                    Me.ShowMessageInfo("GON_2_DATE < GON_1_DATE")
                    Me.grdGoneSequence.CancelCurrentEdit()
                    Return False
                End If
                'Else
                '    Me.ShowMessageInfo("GON_1_DATE is null")
                '    Me.grdGoneSequence.CancelCurrentEdit()
                '    Return False
            End If
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_1_DATE")) Then
            If Me.dtPicSPPBDate.Text = "" Then
                Me.ShowMessageInfo("Please define SPPB_Date")
                Me.grdGoneSequence.CancelCurrentEdit()
                Return False
            Else
                If Convert.ToDatetime(Me.grdGoneSequence.GetValue("GON_1_DATE")) < Convert.ToDatetime(Me.dtPicSPPBDate.Value.ToShortDateString()) Then
                    Me.ShowMessageInfo("GON_1_DATE < SPPB_Date")
                    Me.grdGoneSequence.CancelCurrentEdit()
                    Return False
                End If
            End If
        End If
        Return True
    End Function

#End Region

#Region " Function "

    Private Function getBalcance(ByVal SPPB_QTYToCompare As Decimal) As Decimal
        Dim SUMGON_QTY As Decimal = 0
        'If (Not IsDBNull(Me.grdGoneSequence.GetValue("GON_1_QTY"))) Or (Not IsDBNull(Me.grdGoneSequence.GetValue("GON_2_QTY"))) _
        'Or (Not IsDBNull(Me.grdGoneSequence.GetValue("GON_3_QTY"))) Or (Not IsDBNull(Me.grdGoneSequence.GetValue("GON_4_QTY"))) _
        'Or (Not IsDBNull(Me.grdGoneSequence.GetValue("GON_5_QTY"))) Or (Not IsDBNull(Me.grdGoneSequence.GetValue("GON_6_QTY"))) Then
        '    SUMGON_QTY = Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_1_QTY")) + Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_2_QTY")) _
        '          + Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_3_QTY")) + Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_4_QTY")) + Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_5_QTY")) _
        '          + Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_6_QTY"))
        'End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_1_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_1_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_1_QTY"))
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_2_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_2_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_2_QTY"))
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_3_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_3_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_3_QTY"))
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_4_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_4_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_4_QTY"))
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_5_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_5_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_5_QTY"))
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_6_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_6_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_6_QTY"))
        End If
        Dim BALANCE As Decimal = (SPPB_QTYToCompare - SUMGON_QTY)
        Return BALANCE
    End Function

    Private Function GetSumGonQTY() As Decimal
        Dim SUMGON_QTY As Decimal = 0
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_1_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_1_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_1_QTY"))
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_2_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_2_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_2_QTY"))
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_3_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_3_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_3_QTY"))
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_4_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_4_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_4_QTY"))
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_5_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_5_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_5_QTY"))
        End If
        If Not IsDBNull(Me.grdGoneSequence.GetValue("GON_6_QTY")) And Not IsNothing(Me.grdGoneSequence.GetValue("GON_6_QTY")) Then
            SUMGON_QTY += Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_6_QTY"))
        End If
        Return SUMGON_QTY
    End Function

    Private Function IsExists(ByVal SPPB_BRANDPACK_ID As String) As Boolean
        Dim dtview As DataView = CType(Me.grdGoneSequence.DataSource, DataView)
        If dtview.Find(SPPB_BRANDPACK_ID) <> -1 Then
            Me.ShowMessageInfo("Data has existed.")
            'e.Cancel = True
            Me.grdGoneSequence.MoveToNewRecord()
            Return True
        End If
        'check in database 
        If Me.clsSPPBDetail.IsExistedOnSPPBBrandPack(SPPB_BRANDPACK_ID) = True Then
            Me.ShowMessageInfo(Me.MessageDataHasExisted)
            'e.Cancel = True
            Me.grdGoneSequence.MoveToNewRecord()
            Return True
        End If
        Return False
    End Function

    Private Function IsValid() As Boolean
        If Me.mcbDistributor.SelectedItem Is Nothing Then
            Me.baseTooltip.Show("Please define Distributor", Me.mcbDistributor, 3000)
            Me.mcbOA_REF_NO.Focus()
            Return False
        ElseIf Me.mcbOA_REF_NO.SelectedItem Is Nothing Then
            Me.baseTooltip.Show("Please define OA Ref No", Me.mcbOA_REF_NO, 3000)
            Me.mcbOA_REF_NO.Focus()
            Return False
        ElseIf (Me.txtSPPBNo.Text = "") Or (Me.txtSPPBNo.Text = "Undefined") Then
            Me.baseTooltip.Show("Please define SPPB NO", Me.txtSPPBNo, 3000)
            Me.txtSPPBNo.Focus()
            Return False
        ElseIf Me.dtPicSPPBDate.Text = "" Then
            Me.baseTooltip.Show("Please define SPPB Date", Me.dtPicSPPBDate, 3000)
            Me.dtPicSPPBDate.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function IsValidEntry() As Boolean
        If Me.cmbGonSequence.Text = "" Then
            Me.baseTooltip.Show("Please define Gon sequence", Me.cmbGonSequence, 2500)
            Me.cmbGonSequence.Focus()
            Return False
        ElseIf Me.txtGon_No.Text = "" Then
            Me.baseTooltip.Show("Please define GON_NO", Me.txtGon_No, 2500)
            Me.txtGon_No.Focus()
            Return False
        ElseIf Me.dtPicGonDate.Text = "" Then
            Me.baseTooltip.Show("Please define GON_Date", Me.dtPicSPPBDate, 2500)
            Me.dtPicSPPBDate.Focus()
            Return False
        ElseIf IsNothing(Me.mcbTransporter.Value) Then
            Me.baseTooltip.Show("Please define Transporter", Me.mcbTransporter, 2500)
            Me.mcbTransporter.Focus()
            Return False
            'ElseIf Me.mcbTransporter.SelectedIndex <= -1 Then
            '    Me.baseTooltip.Show("Please define Transporter", Me.mcbTransporter, 2500)
            '    Me.mcbTransporter.Focus()
            '    Return False
        End If
        Return True
    End Function

    Private Function IsvalidGonQTY(ByVal GS_NO As Integer) As Boolean
        Select Case GS_NO
            Case 1
                If Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_1_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                End If
            Case 2
                If Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_1_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_2_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                End If
            Case 3
                If Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_1_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_2_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_3_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                End If
            Case 4
                If Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_1_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_2_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_3_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_4_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                End If
            Case 5
                If Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_1_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_2_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_3_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_4_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_5_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                End If
            Case 6
                If Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_1_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_2_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_3_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_4_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_5_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_6_QTY")) <= 0 Then
                    Me.ShowMessageInfo("Invalid Gon_Qty")
                    'Me.grdGoneSequence.MoveToNewRecord()
                    Return False
                End If

        End Select
        Return True
    End Function

#End Region

#Region " Event Procedure "

#Region " Form Event "

    Private Sub SPPBEntry_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.isClosingForm = True
            If Not IsNothing(Me.clsSPPBDetail) Then
                Me.clsSPPBDetail.Dispose(True)
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SPPBEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            'Me.AcceptButton = Me.btnSave
            Me.CancelButton = Me.btnCLose
            Me.baseTooltip.SetToolTip(Me.txtSPPBNo, "To commit editing SPPB_NO." & vbCrLf & "Press enter when finish editing")
            If Me.SaveMode = ModeSave.Save Then
                Me.dtPicSPPBDate.Text = ""
            End If
            'For Each c As Janus.Windows.GridEX.GridEXColumnSet In Me.grdGoneSequence.RootTable.ColumnSets
            '    c.Visible = True
            'Next
            If Me.SaveMode = ModeSave.Save Then
                Me.dtPicRelease.Text = ""
            End If
            Me.dtPicRelease.CustomFormat = "dd MMMM yyyy"
        Catch ex As Exception

        Finally
            'Me.Counter += 1
            Me.IsFormLoaded = True : Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " Multi column Combo "

    Private Sub mcbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mcbDistributor.Value Is Nothing Then
                Return
            End If
            If Me.SFM = StateFillingMCB.Filling Then
                Return
            End If
            If Me.SaveMode = ModeSave.Save Then
                If Me.mcbDistributor.SelectedItem Is Nothing Then
                    Return
                End If
            Else
                If Me.mcbDistributor.Value Is Nothing Then
                    Return
                End If
            End If
            'create OAref_
            If Me.SaveMode = ModeSave.Save Then
                Me.clsSPPBDetail.CreateViewOARefNo(Me.mcbDistributor.Value.ToString(), False, True)
            Else
                Me.clsSPPBDetail.CreateViewOARefNo(Me.mcbDistributor.Value.ToString(), True, Me.SaveMode = ModeSave.Save)
            End If
            Me.BindMulticolumnCombo(Me.mcbOA_REF_NO, Me.clsSPPBDetail.ViewOARefNo())
            MyBase.ClearControl(Me.grpSPPB)
            Me.dtPicSPPBDate.Text = ""
            Me.grdGoneSequence.SetDataBinding(Nothing, "")
            MyBase.ClearControl(Me.grpDetail)
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbDistributor_ValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ClearInfo()
        Me.SFM = StateFillingMCB.Filling
        Me.grdGoneSequence.SetDataBinding(Nothing, "")
        Me.txtPODate.Text = "" : Me.txtPoREfNo.Text = ""
        Me.cmbGonSequence.SelectedIndex = -1
        Me.txtGon_No.Text = ""
        Me.dtPicGonDate.Text = ""
        Me.dtPicSPPBDate.Text = ""
        Me.txtSPPBNo.Text = ""
        Me.SFM = StateFillingMCB.HasFilled
    End Sub
    Private Sub mcbOA_REF_NO_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mcbOA_REF_NO.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFM = StateFillingMCB.Filling Then
                Return
            End If
            If Me.mcbDistributor.Value Is Nothing Then
                Me.ShowMessageInfo("Please define distributor")
                Me.mcbDistributor.Focus()
                Return
            End If
            If Me.SaveMode = ModeSave.Save Then
                If IsNothing(Me.mcbOA_REF_NO.SelectedItem) Then
                    ClearInfo() : Return
                End If
            Else
                If IsNothing(Me.mcbOA_REF_NO.Value) Then
                    ClearInfo() : Return
                End If
            End If
            Me.SFG = StateFillingGrid.Filling
            Me.clsSPPBDetail.CreateViewBrandPack(Me.mcbOA_REF_NO.Value.ToString(), False)
            Me.grdGoneSequence.Enabled = False

            Me.InflateDataFromMCB(Me.mcbOA_REF_NO.Value.ToString())
            Me.dtPicSPPBDate.MinDate = DateTime.Parse(Me.txtPODate.Text)
            Me.SFG = StateFillingGrid.HasFilled
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbOA_REF_NO_ValueChanged")
            Me.SFG = StateFillingGrid.HasFilled
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    ''' <summary>
    ''' use only for updated status 
    ''' </summary>
    ''' <param name="GSNo"></param>
    ''' <remarks></remarks>
    Private Function getTransPorterByGONSec(ByVal GSNo As Integer) As Object
        Dim TransID As Object = DBNull.Value
        Dim dummy As DataTable = CType(Me.grdGoneSequence.DataSource, DataView).ToTable().Copy()
        If (dummy.Rows.Count > 0) Then


            For i As Integer = 0 To dummy.Rows.Count - 1
                If Not IsNothing(dummy.Rows(i)("GON_" & GSNo.ToString() & "_GT_ID")) And Not IsDBNull(dummy.Rows(i)("GON_" & GSNo.ToString() & "_GT_ID")) Then
                    If Not String.IsNullOrEmpty(dummy.Rows(i)("GON_" & GSNo.ToString() & "_GT_ID")) Then
                        TransID = dummy.Rows(i)("GON_" & GSNo.ToString() & "_GT_ID").ToString()
                        Exit For
                    End If
                End If
            Next
        End If
        Return TransID
    End Function

    Private Sub cmbGonSequence_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGonSequence.TextChanged
        Try
            If Me.SaveMode = ModeSave.Save Then
                If Not Me.IsFormLoaded Then : Return : End If
                If Me.SFG = StateFillingGrid.Filling Or Me.SFG = StateFillingGrid.Disposing Then : Return : End If
            End If
            If (Me.txtSPPBNo.Text = "") Or (Me.txtSPPBNo.Text = "Undefined") Then
                Me.baseTooltip.Show("Please define SPPB_NO.", Me.txtSPPBNo, 3000)
                Me.txtSPPBNo.Focus()
                Return
            End If
            If Not Me.cmbGonSequence.Text = "" Then
                If Not IsNumeric(Me.cmbGonSequence.Text) Then
                    Me.baseTooltip.Show("Please define GON_NO.", Me.cmbGonSequence, 2500)
                    Me.cmbGonSequence.Focus()
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                If Me.SaveMode = ModeSave.Update Then
                    If Not Me.IsFormLoaded Then : Return : End If
                    Dim TransID As Object = Me.getTransPorterByGONSec(CInt(Me.cmbGonSequence.Text))
                    Me.SFM = StateFillingMCB.Filling
                    If Not IsNothing(TransID) And Not IsDBNull(TransID) Then
                        Me.mcbTransporter.Value = TransID
                    Else
                        Me.mcbTransporter.Value = Nothing
                    End If
                    Me.SFM = StateFillingMCB.HasFilled
                End If

                For i As Integer = 1 To 6
                    Me.grdGoneSequence.RootTable.ColumnSets(i + 1).Visible = False
                Next
                For i As Integer = 1 To CInt(Me.cmbGonSequence.Text)
                    Me.grdGoneSequence.RootTable.ColumnSets(i + 1).Visible = True
                Next
                Me.txtGon_No.Focus()
                'Me.grdGoneSequence.Enabled = True
                Me.SetSelectableGonSequence(CInt(Me.cmbGonSequence.Text))
                'get_gon_date & gon_no
                Me.clsSPPBDetail.GetGONNoAndDate(Me.txtSPPBNo.Text.Trim(), CInt(Me.cmbGonSequence.Text), False)
                If (Not IsNothing(Me.clsSPPBDetail.GON_NO)) And (Not IsDBNull(Me.clsSPPBDetail.GON_NO)) Then
                    Me.txtGon_No.Text = CStr(Me.clsSPPBDetail.GON_NO)
                Else
                    Me.txtGon_No.Text = ""
                End If
                If (Not IsNothing(Me.clsSPPBDetail.GON_DATE)) And (Not IsDBNull(Me.clsSPPBDetail.GON_DATE)) Then
                    Me.dtPicGonDate.Text = Convert.ToDateTime(Me.clsSPPBDetail.GON_DATE).ToLongDateString()
                Else
                    Me.dtPicGonDate.Text = ""
                End If
                'check gon_date 

                Dim lastGonDate As Object = Me.clsSPPBDetail.getGonDate(Me.txtSPPBNo.Text.Trim(), Me.cmbGonSequence.Text)
                If (IsNothing(lastGonDate)) Or (IsDBNull(lastGonDate)) Then
                    If CInt(Me.cmbGonSequence.Text) > 1 Then
                        Me.ShowMessageInfo("GON_" & CInt(Me.cmbGonSequence.Text).ToString() & "_DATE has not been defined yet" & vbCrLf & "Please choose another one")
                        Me.cmbGonSequence.Text = ""
                        Return
                    Else
                        Me.dtPicGonDate.MinDate = Me.dtPicSPPBDate.MinDate()
                    End If
                Else
                    Me.dtPicGonDate.MinDate = Convert.ToDateTime(Convert.ToDateTime(lastGonDate).ToShortDateString())
                End If
                Me.dtPicGonDate.ReadOnly = False
            Else
                'Me.grdGoneSequence.Enabled = False
                Me.dtPicGonDate.ReadOnly = True
                Me.dtPicGonDate.Text = ""
            End If
            Me.ChangedGSNo = Me.cmbGonSequence.Text
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.SFM = StateFillingMCB.HasFilled
        End Try
    End Sub

#End Region

#Region " Button "

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            CType(Me.grdGoneSequence.DataSource, DataView).Table.DataSet.AcceptChanges()
            Me.ClearControl() : Me.BindGrid(Nothing) : Me.SaveMode = ModeSave.Save
            Me.XpGradientPanel1.Enabled = True : Me.mcbOA_REF_NO.ReadOnly = False
            Me.mcbDistributor.ReadOnly = False : Me.txtSPPBNo.Enabled = True
            Me.dtPicGonDate.ReadOnly = True : Me.dtPicSPPBDate.ReadOnly = True
            Me.InitializeData()
        Catch ex As Exception

        Finally
            Me.SFM = StateFillingMCB.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'check validity
            Me.Cursor = Cursors.WaitCursor
            If Me.IsValid() = False Then
                Return
            End If
            Me.clsSPPBDetail.SPPB_NO = Me.txtSPPBNo.Text.Trim()
            Me.clsSPPBDetail.SPPBDate = Convert.ToDatetime(Me.dtPicSPPBDate.Value.ToShortDateString())
            Me.clsSPPBDetail.PO_REF_NO = Me.txtPoREfNo.Text.Trim()
            Dim GonSect As Integer = 0
            If Not String.IsNullOrEmpty(Me.cmbGonSequence.Text) Then
                If IsNumeric(Me.cmbGonSequence.Text) Then
                    GonSect = CInt(Me.cmbGonSequence.Text.TrimEnd().TrimStart())
                End If
            End If
            Select Case Me.SaveMode
                Case ModeSave.Save
                    If Me.clsSPPBDetail.IsExistsSPPBNO(Me.txtSPPBNo.Text.Trim(), False) = True Then
                        Me.baseTooltip.Show(Me.MessageDataHasExisted, Me.txtSPPBNo, 3000)
                        Return
                    End If
                    If Me.clsSPPBDetail.GetDataSet.HasChanges() Then
                        If Me.dtPicGonDate.Text <> "" Then
                            Me.clsSPPBDetail.ChangedGonDate = Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString())
                        End If
                        If Me.txtGon_No.Text <> "" Then
                            Me.clsSPPBDetail.ChangedGonNumber = RTrim(txtGon_No.Text)
                        Else
                            Me.clsSPPBDetail.ChangedGonNumber = ""
                        End If
                        If Me.cmbGonSequence.Text <> "" Then
                            Me.clsSPPBDetail.ChangedGSNumber = Me.cmbGonSequence.Text
                        Else
                            Me.clsSPPBDetail.ChangedGSNumber = ""
                        End If
                        Me.clsSPPBDetail.Save("Insert", Me.clsSPPBDetail.GetDataSet.GetChanges(), GonSect)
                        Me.ShowMessageInfo(Me.MessageSavingSucces)
                        Me.clsSPPBDetail.GetDataSet().AcceptChanges()
                        If Not IsNothing(Me.frmParent) Then
                            Me.frmParent.MustReloadData = True
                        End If
                    End If
                Case ModeSave.Update
                    Me.clsSPPBDetail.ChangedGonNumber = Me.ChangedGonNumber
                    Me.clsSPPBDetail.ChangedGonDate = Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString())
                    Me.clsSPPBDetail.ChangedGSNumber = Me.ChangedGSNo
                    If Me.clsSPPBDetail.GetDataSet.HasChanges() Then
                       
                        Me.clsSPPBDetail.Save("Update", Me.clsSPPBDetail.GetDataSet.GetChanges(), GonSect)
                        Me.ShowMessageInfo(Me.MessageSavingSucces)
                        Me.clsSPPBDetail.GetDataSet().AcceptChanges()
                        If Not IsNothing(Me.frmParent) Then
                            Me.frmParent.MustReloadData = True
                        End If
                    End If
            End Select
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnSave_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.isClosingForm = True
            If Not IsNothing(Me.clsSPPBDetail.GetDataSet()) Then
                If Me.clsSPPBDetail.GetDataSet().HasChanges() Then
                    If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                        If Me.IsValid() = False Then
                            Return
                        End If
                        Me.clsSPPBDetail.SPPB_NO = Me.txtSPPBNo.Text.Trim()
                        Me.clsSPPBDetail.SPPBDate = Convert.ToDatetime(Me.dtPicSPPBDate.Value.ToShortDateString())
                        Me.clsSPPBDetail.PO_REF_NO = Me.txtPoREfNo.Text.Trim()
                        Select Case Me.SaveMode
                            Case ModeSave.Save
                                If Me.clsSPPBDetail.IsExistsSPPBNO(Me.txtSPPBNo.Text.Trim(), False) = True Then
                                    Me.baseTooltip.Show(Me.MessageDataHasExisted, Me.txtSPPBNo, 3000)
                                    Return
                                End If
                                If Me.clsSPPBDetail.GetDataSet.HasChanges() Then
                                    'update seluruh gon_no_dan gon_date di samakan
                                    Me.clsSPPBDetail.Save("Insert", Me.clsSPPBDetail.GetDataSet.GetChanges())
                                    Me.ShowMessageInfo(Me.MessageSavingSucces)
                                    Me.clsSPPBDetail.GetDataSet().AcceptChanges()
                                Else
                                    Me.ShowMessageInfo("Data can not be saved due to no data's changed")
                                End If
                            Case ModeSave.Update
                                If Me.clsSPPBDetail.GetDataSet.HasChanges() Then
                                    'Dim dtview As DataView = Me.UpdateGonValue()
                                    Me.clsSPPBDetail.Save("Update", Me.clsSPPBDetail.GetDataSet.GetChanges())
                                    Me.ShowMessageInfo(Me.MessageSavingSucces)
                                    Me.clsSPPBDetail.GetDataSet().AcceptChanges()
                                Else
                                    Me.ShowMessageInfo("Data can not be saved due to no data's changed")
                                End If
                        End Select
                    End If
                End If
                Me.SFG = StateFillingGrid.Disposing

                Me.clsSPPBDetail.Dispose(True)
            End If
            Me.Close()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnCLose_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnChekExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChekExisting.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.txtSPPBNo.Text = "" Or Me.txtSPPBNo.Text = "Undefined" Then
                Me.baseTooltip.Show("Please type SPPB_NO", Me.txtSPPBNo, 3000)
                Me.txtSPPBNo.Focus()
                Return
            End If
            If Me.clsSPPBDetail.IsExistsSPPBNO(Me.txtSPPBNo.Text.Trim(), False) = True Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
                Me.txtSPPBNo_KeyPress(Me.txtSPPBNo, New KeyPressEventArgs(Chr(13)))
                Me.grdGoneSequence.Enabled = False
            Else
                Me.ShowMessageInfo(Me.MessageDataSaveToAdd)
                Me.grdGoneSequence.Enabled = True
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnChekExisting_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.mcbDistributor.DataSource) Then
                Dim dtview As DataView = CType(Me.mcbDistributor.DataSource, DataView)
                dtview.RowFilter = "DISTRIBUTOR_NAME LIKE '%" & Me.mcbDistributor.Text & "%'"
                Me.BindMulticolumnCombo(Me.mcbDistributor, dtview)
                Dim itemCount As Integer = Me.mcbDistributor.DropDownList().RecordCount()
                Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterOAREfNo_btnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterOAREfNo.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsSPPBDetail.SearchOA_REF_NO(Me.mcbOA_REF_NO.Text, Me.mcbDistributor.Value.ToString(), True)
            Me.BindMulticolumnCombo(Me.mcbOA_REF_NO, Me.clsSPPBDetail.ViewOARefNo())
            'Dim dtview As DataView = CType(Me.mcbOA_REF_NO.DataSource, DataView)
            Dim itemCount As Integer = Me.mcbOA_REF_NO.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub
#End Region

#Region " Textbox "

    Private Sub txtSPPBNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSPPBNo.KeyPress
        If e.KeyChar = Chr(13) Then
            Try
                Me.Cursor = Cursors.WaitCursor
                If Me.mcbOA_REF_NO.Value Is Nothing Then
                    Me.ShowMessageInfo("Please define OA_REF_NO")
                    Me.mcbOA_REF_NO.Focus()
                    Return
                End If
                'Me.btnChekExisting_Click(Me.btnChekExisting, New EventArgs())
                Me.clsSPPBDetail.CreateViewSPPBDetail(Me.txtSPPBNo.Text.Trim(), False)
                Me.BindGrid(Me.clsSPPBDetail.ViewSPPBDetail())
                If Me.clsSPPBDetail.ViewSPPBDetail.Count > 0 Then
                    Me.SaveMode = ModeSave.Update
                Else
                    Me.SaveMode = ModeSave.Save
                End If
                If (Me.txtSPPBNo.Text = "") Or (Me.txtSPPBNo.Text = "Undefined") Then
                    Me.grdGoneSequence.Enabled = False
                    Me.dtPicGonDate.ReadOnly = True
                    Me.dtPicSPPBDate.ReadOnly = True
                Else
                    Me.grdGoneSequence.Enabled = True
                    Me.dtPicGonDate.ReadOnly = False
                    Me.dtPicSPPBDate.ReadOnly = False
                End If
                If Me.SaveMode = ModeSave.Save Then
                    Me.FillCMBGonSequence(ModeSave.Save)
                ElseIf Me.SaveMode = ModeSave.Update Then
                    Me.FillCMBGonSequence(ModeSave.Update)
                    'Me.clsSPPBDetail.GetGonSequenceNo(Me.txtSPPBNo.Text.Trim())
                End If
                Me.fillItem()
                Me.cmbGonSequence_TextChanged(Me.cmbGonSequence, New EventArgs())
                'Me.grdGoneSequence.Enabled = True
            Catch ex As Exception
                Me.SFM = StateFillingMCB.Filling : Me.mcbOA_REF_NO.Value = Nothing : Me.ClearInfo() : Me.ShowMessageInfo(ex.Message)
                Me.LogMyEvent(ex.Message, Me.Name + "_txtSPPBNo_TextChanged")
                Me.SFM = StateFillingMCB.HasFilled
            Finally
                Me.SVM = SetValueMode.HasChanged
                Me.Cursor = Cursors.Default
            End Try
        End If
    End Sub

#End Region

#Region " DataGrid "

    Private Sub grdGoneSequence_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdGoneSequence.AddingRecord
        Try
            'check validity
            If (Me.SFG = StateFillingGrid.Filling) Or (Me.SFG = StateFillingGrid.Disposing) Then
                Return
            End If
            If Me.SVM = SetValueMode.Changing Then
                Return
            End If
            If Me.IsValid() = False Then
                Me.grdGoneSequence.CancelCurrentEdit()
                Return
                'ElseIf Me.IsValidEntry() = False Then
                '    Me.grdGoneSequence.CancelCurrentEdit()
                '    Return
            End If
            'check BRANDPACK_NAME
            If IsDBNull(Me.grdGoneSequence.GetValue("SPPB_BRANDPACK_ID")) Then
                Me.ShowMessageInfo("Please Define Brandpack")
                e.Cancel = True
                Me.grdGoneSequence.MoveToNewRecord()
                Return
            ElseIf IsDBNull(Me.grdGoneSequence.GetValue("SPPB_NO")) Then
                Me.baseTooltip.Show("Please type SPPB NO", Me.txtSPPBNo, 3000)
                e.Cancel = True
                Me.grdGoneSequence.MoveToNewRecord()
                Return
            ElseIf IsDBNull(Me.grdGoneSequence.GetValue("OA_BRANDPACK_ID")) Then
                Me.ShowMessageInfo("OA_REF_NO or PO_REF_NO or BRANDPACK_NAME is null")
                e.Cancel = True
                Me.grdGoneSequence.MoveToNewRecord()
                Return
            End If
            If IsNothing(Me.grdGoneSequence.GetValue("BRANDPACK_ID")) Or (Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value) Then
                Me.ShowMessageInfo("Please Define Brandpack")
                e.Cancel = True
                Me.grdGoneSequence.MoveToNewRecord()
                Return
            ElseIf (IsNothing(Me.grdGoneSequence.GetValue("SPPB_QTY"))) Or (Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0) Then
                Me.ShowMessageInfo("Please fill correct value")
                e.Cancel = True
                Me.grdGoneSequence.MoveToNewRecord()
                Return
                'ElseIf Me.IsvalidGonQTY(CInt(Me.cmbGonSequence.Text)) = False Then
                '    e.Cancel = True
                '    Me.grdGoneSequence.MoveToNewRecord()
                '    Return
            End If
            'set value
            'CHECK SPPB_BRANDPACK
            If Me.IsExists(Me.grdGoneSequence.GetValue("SPPB_BRANDPACK_ID").ToString()) = True Then
                e.Cancel = True
                Me.grdGoneSequence.MoveToNewRecord()
                Return
            End If
            Dim CREATE_BY As String = NufarmBussinesRules.User.UserLogin.UserName
            Dim CREATE_DATE As Date = Convert.ToDatetime(NufarmBussinesRules.SharedClass.ServerDate().ToShortDateString())
            Me.SVM = SetValueMode.Changing
            Me.grdGoneSequence.SetValue("CREATE_DATE", CREATE_DATE)
            Me.grdGoneSequence.SetValue("CREATE_BY", CREATE_BY)
            Me.SVM = SetValueMode.HasChanged
            'If CInt(Me.cmbGonSequence.Text) = 1 Then

            'End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdGoneSequence_AddingRecord")
            Me.SVM = SetValueMode.HasChanged
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdGoneSequence_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdGoneSequence.DeletingRecord
        Try
            'check reference data
            'If Me.clsSPPBDetail.IsHasChildReference(Me.grdGoneSequence.GetValue("OA_BRANDPACK_ID").ToString()) Then
            '    Me.ShowMessageInfo(Me.MessageCantDeleteData)
            '    e.Cancel = True
            '    Return
            'End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Return
            End If
            'try delete data
            'Me.clsSPPBDetail.DeleteSPPPB_BrandPack(Me.grdGoneSequence.GetValue("OA_BRANDPACK_ID").ToString())
            e.Cancel = False
            Me.grdGoneSequence.UpdateData()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdGoneSequence_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdGoneSequence_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdGoneSequence.RecordAdded
        Me.grdGoneSequence.UpdateData()
    End Sub

    Private Sub grdGoneSequence_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdGoneSequence.RecordsDeleted
        Me.grdGoneSequence.UpdateData()
    End Sub

    Private Sub grdGoneSequence_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdGoneSequence.Enter
        Try
            Me.Counter += 1
            If Me.Counter > 1 Then
                If Me.IsValid() = False Then
                    Return
                End If
                'If Me.IsValidEntry() = False Then
                '    Return
                'End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtPicSPPBDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtPicSPPBDate.ValueChanged
        If Not Me.IsFormLoaded Then : Return : End If
        If Me.SFG = StateFillingGrid.Filling Or Me.SFG = StateFillingGrid.Disposing Then : Return : End If
        Try

            If Not Me.dtPicGonDate.Text = "" Then
                If (Me.txtSPPBNo.Text = "") Or (Me.txtSPPBNo.Text = "Undefined") Then
                    Me.ShowMessageInfo("Please define SPPB_NO")
                    Me.dtPicSPPBDate.Text = ""
                    Return
                End If

            End If
            Me.dtPicGonDate.MinDate = Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString())
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdGoneSequence_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdGoneSequence.SelectionChanged
        Try
            If Me.SVM = SetValueMode.Changing Then
                Return
            End If
            If (Me.SFG = StateFillingGrid.Filling) Or (Me.SFG = StateFillingGrid.Disposing) Then
                Return
            End If
            'If Me.Counter <= 1 Then
            '    Return
            'End If
            If Me.grdGoneSequence.GetRow().RowType() = Janus.Windows.GridEX.RowType.Record Then
                If Me.cmbGonSequence.Text <> "" Then
                    Me.SetSelectableGonSequence(CInt(Me.cmbGonSequence.Text))
                Else
                    Me.SetSelectableGonSequence(0)
                    'Return
                End If
            Else
                If Me.cmbGonSequence.Text <> "" Then
                    Me.SetSelectableGonSequence(CInt(Me.cmbGonSequence.Text))
                Else
                    Me.SetSelectableGonSequence(0)
                    'Return
                End If

            End If
            If Me.grdGoneSequence.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                Me.grdGoneSequence.RootTable.Columns("ISREVISION").Selectable = False
            ElseIf Me.grdGoneSequence.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                If CBool(Me.grdGoneSequence.GetValue("ISREVISION")) = True Then
                    Me.grdGoneSequence.RootTable.Columns("SPPB_QTY").Selectable = True
                Else
                    Me.grdGoneSequence.RootTable.Columns("SPPB_QTY").Selectable = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function CreateOrGetTransporterID(ByVal TransporterName As String) As String
        Dim transID As String = ""
        If Me.mcbTransporter.SelectedIndex <> -1 Then
            transID = Me.mcbTransporter.Value.ToString()
            'getdata
        Else
            If Me.mcbTransporter.Text = "" Then
                Throw New Exception("Please define Transporter")
            End If
            transID = Me.clsSPPBDetail.CreatetransporterID(TransporterName)
        End If
        Return transID
    End Function

    Private Sub grdGoneSequence_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdGoneSequence.CellUpdated
        Try
            Me.Cursor = Cursors.WaitCursor
            If (Me.SFG = StateFillingGrid.Filling) Or (Me.SFG = StateFillingGrid.Disposing) Then
                Return
            End If
            'If Me.SVM = SetValueMode.Changing Then
            '    Return
            'End If
            If Me.IsValid() = False Then
                Me.grdGoneSequence.CancelCurrentEdit()
                Return
            End If
            If Me.IsValidDate() = False Then : Return : End If
            'If Me.IsValidEntry() = False Then
            '    Me.grdGoneSequence.CancelCurrentEdit()
            '    Return
            'End If
            Dim BALANCE As Decimal = 0
            If Me.grdGoneSequence.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                Select Case e.Column.Key
                    Case "BRANDPACK_ID"
                        'ISI COLUMN SPPB_BRANDPACK_ID,SPPB_NO,OA_BRANDPACK_ID
                        If Me.clsSPPBDetail.ViewBrandPack().Find(Me.grdGoneSequence.GetValue("BRANDPACK_ID")) <> -1 Then

                            Dim PO_BRANDPACK_ID As String = Me.txtPoREfNo.Text.Trim() & Me.grdGoneSequence.GetValue(e.Column)
                            Dim OA_BRANDPACK_ID As String = Me.mcbOA_REF_NO.Value.ToString() & PO_BRANDPACK_ID
                            Dim SPPB_BRANDPACK_ID As String = Me.txtSPPBNo.Text.Trim() & OA_BRANDPACK_ID
                            'check kedatabase and dataview apakah udah ada sppb_brandpack_idnya
                            If Me.IsExists(SPPB_BRANDPACK_ID) = True Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                'Me.grdGoneSequence.MoveToNewRecord()
                                Return
                            End If
                            'Me.SVM = SetValueMode.Changing
                            Me.grdGoneSequence.SetValue("SPPB_BRANDPACK_ID", SPPB_BRANDPACK_ID)
                            Me.grdGoneSequence.SetValue("OA_BRANDPACK_ID", OA_BRANDPACK_ID)
                            Me.grdGoneSequence.SetValue("SPPB_NO", Me.txtSPPBNo.Text.Trim())
                            'AMBIL TOTAL_OA_QTY 
                            Dim Remark As String = ""
                            Dim TotalOAQty As Decimal = Me.clsSPPBDetail.GetTotalOAQty(OA_BRANDPACK_ID, False, False, Remark)
                            If TotalOAQty <= 0 Then
                                Me.ShowMessageInfo("Total_OA_QTY <= 0")
                                Me.grdGoneSequence.CancelCurrentEdit()
                                'Me.grdGoneSequence.MoveToNewRecord()
                                Return
                            End If
                            Me.grdGoneSequence.SetValue("SPPB_QTY", TotalOAQty)
                            'Me.SVM = SetValueMode.HasChanged
                        End If

                    Case "GON_1_QTY"
                        If Me.IsValidEntry() = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        End If
                        If Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_1_QTY")) <= 0 Then
                            Me.ShowMessageInfo("Please suply GON_QTY")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        If Me.IsvalidGonQTY(1) = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        'Me.SVM = SetValueMode.Changing
                        Me.grdGoneSequence.SetValue("GON_1_NO", Me.txtGon_No.Text)
                        Me.grdGoneSequence.SetValue("GON_1_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        Me.grdGoneSequence.SetValue("GON_1_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        Me.grdGoneSequence.SetValue("GON_1_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())

                        'Me.SVM = SetValueMode.HasChanged

                    Case "GON_2_QTY"
                        If Me.IsValidEntry() = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        End If
                        If Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_2_QTY")) <= 0 Then
                            Me.ShowMessageInfo("Please suply GON_QTY")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        If Me.IsvalidGonQTY(2) = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        'Me.SVM = SetValueMode.Changing
                        Me.grdGoneSequence.SetValue("GON_2_NO", Me.txtGon_No.Text.Trim())
                        Me.grdGoneSequence.SetValue("GON_2_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        Me.grdGoneSequence.SetValue("GON_2_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        Me.grdGoneSequence.SetValue("GON_2_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())

                        'Me.SVM = SetValueMode.HasChanged

                    Case "GON_3_QTY"
                        If Me.IsValidEntry() = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        End If
                        If Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_3_QTY")) <= 0 Then
                            Me.ShowMessageInfo("Please suply GON_QTY")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        If Me.IsvalidGonQTY(3) = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If

                        'Me.SVM = SetValueMode.Changing
                        Me.grdGoneSequence.SetValue("GON_3_NO", Me.txtGon_No.Text.Trim())
                        Me.grdGoneSequence.SetValue("GON_3_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        Me.grdGoneSequence.SetValue("GON_3_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        Me.grdGoneSequence.SetValue("GON_3_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())

                        'Me.SVM = SetValueMode.HasChanged

                    Case "GON_4_QTY"
                        If Me.IsValidEntry() = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        End If
                        If Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_4_QTY")) <= 0 Then
                            Me.ShowMessageInfo("Please suply GON_QTY")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        If Me.IsvalidGonQTY(4) = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        If Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_4_QTY")) <= 0 Then
                            Me.ShowMessageInfo("Please suply GON_QTY")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        If Me.IsvalidGonQTY(4) = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        'Me.SVM = SetValueMode.Changing
                        Me.grdGoneSequence.SetValue("GON_4_NO", Me.txtGon_No.Text.Trim())
                        Me.grdGoneSequence.SetValue("GON_4_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        Me.grdGoneSequence.SetValue("GON_4_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        Me.grdGoneSequence.SetValue("GON_4_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())

                        'Me.SVM = SetValueMode.HasChanged

                    Case "GON_5_QTY"
                        If Me.IsValidEntry() = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        End If
                        If Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_5_QTY")) <= 0 Then
                            Me.ShowMessageInfo("Please suply GON_QTY")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        If Me.IsvalidGonQTY(5) = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        'Me.SVM = SetValueMode.Changing
                        Me.grdGoneSequence.SetValue("GON_5_NO", Me.txtGon_No.Text.Trim())
                        Me.grdGoneSequence.SetValue("GON_5_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        Me.grdGoneSequence.SetValue("GON_5_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        Me.grdGoneSequence.SetValue("GON_5_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())

                        'Me.SVM = SetValueMode.HasChanged

                    Case "GON_6_QTY"
                        If Me.IsValidEntry() = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        End If
                        If (Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value) Or (IsNothing(Me.grdGoneSequence.GetValue("BRANDPACK_ID"))) Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_6_QTY")) <= 0 Then
                            Me.ShowMessageInfo("Please suply GON_QTY")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        If Me.IsvalidGonQTY(6) = False Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            'Me.grdGoneSequence.MoveToNewRecord()
                            Return
                        End If
                        'Me.SVM = SetValueMode.Changing
                        Me.grdGoneSequence.SetValue("GON_6_NO", Me.txtGon_No.Text.Trim())
                        Me.grdGoneSequence.SetValue("GON_6_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        Me.grdGoneSequence.SetValue("GON_6_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        Me.grdGoneSequence.SetValue("GON_6_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())

                End Select

                BALANCE = Me.getBalcance(Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")))
                If BALANCE < 0 Then
                    Me.ShowMessageInfo("SUM GON_QTY > SPPB_QTY")
                    Me.grdGoneSequence.CancelCurrentEdit()
                    If Me.grdGoneSequence.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                        'Me.grdGoneSequence.MoveToNewRecord()
                    End If
                    Return
                ElseIf BALANCE = 0 Then
                    Me.grdGoneSequence.SetValue("STATUS", "COMPLETED")
                ElseIf (BALANCE > 0) And (Me.GetSumGonQTY > 0) Then
                    Me.grdGoneSequence.SetValue("STATUS", "PARTIAL")
                ElseIf (BALANCE > 0) And (Me.GetSumGonQTY <= 0) Then
                    Me.grdGoneSequence.SetValue("STATUS", "PENDING")
                End If
                Me.grdGoneSequence.SetValue("BALANCE", BALANCE)
                'Me.SVM = SetValueMode.HasChanged
            ElseIf Me.grdGoneSequence.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                'check apakah sudah ada reference data
                Select Case e.Column.Key
                    Case "BRANDPACK_ID"
                        'ISI COLUMN SPPB_BRANDPACK_ID,SPPB_NO,OA_BRANDPACK_ID
                        If Me.clsSPPBDetail.ViewBrandPack().Find(Me.grdGoneSequence.GetValue("BRANDPACK_ID")) <> -1 Then
                            'Me.SVM = SetValueMode.Changing
                            Dim PO_BRANDPACK_ID As String = Me.txtPoREfNo.Text.Trim() & Me.grdGoneSequence.GetValue(e.Column)
                            Dim OA_BRANDPACK_ID As String = Me.mcbOA_REF_NO.Value.ToString() & PO_BRANDPACK_ID
                            If Me.clsSPPBDetail.IsHasChildReference(OA_BRANDPACK_ID) = True Then
                                Me.ShowMessageInfo(Me.MessageDataCantChanged)
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                            Dim SPPB_BRANDPACK_ID As String = Me.txtSPPBNo.Text.Trim() & OA_BRANDPACK_ID
                            'check kedatabase and dataview apakah udah ada sppb_brandpack_idnya
                            If Me.IsExists(Me.grdGoneSequence.GetValue("SPPB_BRANDPACK_ID").ToString()) = True Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                'Me.grdGoneSequence.MoveToNewRecord()
                                Return
                            End If
                            Me.grdGoneSequence.SetValue("SPPB_BRANDPACK_ID", SPPB_BRANDPACK_ID)
                            Me.grdGoneSequence.SetValue("OA_BRANDPACK_ID", OA_BRANDPACK_ID)
                            Me.grdGoneSequence.SetValue("SPPB_NO", Me.txtSPPBNo.Text.Trim())
                            'AMBIL TOTAL_OA_QTY 
                            Dim Remark As String = ""
                            Dim TotalOAQty As Decimal = Me.clsSPPBDetail.GetTotalOAQty(OA_BRANDPACK_ID, False, False, Remark)
                            If TotalOAQty <= 0 Then
                                Me.ShowMessageInfo("Total_OA_QTY <= 0")
                                Me.grdGoneSequence.CancelCurrentEdit()
                                'Me.grdGoneSequence.MoveToNewRecord()
                                Return
                            End If
                            Me.grdGoneSequence.SetValue("SPPB_QTY", TotalOAQty)
                            'Me.SVM = SetValueMode.HasChanged
                        End If
                    Case "GON_1_DATE" ', "GON_2_DATE", "GON_3_DATE", "GON_4_DATE", "GON_5_DATE", "GON_6_DATE"
                        'If Me.IsValidEntry() = False Then
                        '    Me.grdGoneSequence.CancelCurrentEdit()
                        '    Return
                        'End If
                        If Convert.ToDateTime(Me.grdGoneSequence.GetValue(e.Column)) < Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString()) Then
                            Me.ShowMessageInfo("Date is smaler than SPPB_DATE")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        End If
                    Case "GON_2_DATE"
                        'If IsDBNull(Me.grdGoneSequence.GetValue("GON_1_DATE")) Then
                        '    Me.ShowMessageInfo("GON_1_DATE IS NULL")
                        '    Me.grdGoneSequence.CancelCurrentEdit()
                        '    Return
                        'End If
                        If (Not IsDBNull(Me.grdGoneSequence.GetValue("GON_1_DATE"))) Then
                            If Convert.ToDateTime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_1_DATE")) Then
                                Me.ShowMessageInfo("Date is smaler than GON_1_DATE")
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        End If

                    Case "GON_3_DATE"
                        'If IsDBNull(Me.grdGoneSequence.GetValue("GON_2_DATE")) Then
                        '    Me.ShowMessageInfo("GON_2_DATE IS NULL")
                        '    Me.grdGoneSequence.CancelCurrentEdit()
                        '    Return
                        'End If
                        If (Not IsDBNull(Me.grdGoneSequence.GetValue("GON_2_DATE"))) Then
                            If Convert.ToDateTime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_2_DATE")) Then
                                Me.ShowMessageInfo("Date is smaler than GON_2_DATE")
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        End If

                        'If Convert.ToDatetime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_2_DATE")) Then
                        '    Me.ShowMessageInfo("Date is smaler than GON_2_DATE")
                        '    Me.grdGoneSequence.CancelCurrentEdit()
                        '    Return
                        'End If
                    Case "GON_4_DATE"
                        'If IsDBNull(Me.grdGoneSequence.GetValue("GON_3_DATE")) Then
                        '    Me.ShowMessageInfo("GON_3_DATE IS NULL")
                        '    Me.grdGoneSequence.CancelCurrentEdit()
                        '    Return
                        'End If
                        'If Convert.ToDatetime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_3_DATE")) Then
                        '    Me.ShowMessageInfo("Date is smaler than GON_3_DATE")
                        '    Me.grdGoneSequence.CancelCurrentEdit()
                        '    Return
                        'End If
                        If (Not IsDBNull(Me.grdGoneSequence.GetValue("GON_3_DATE"))) Then
                            If Convert.ToDateTime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_3_DATE")) Then
                                Me.ShowMessageInfo("Date is smaler than GON_3_DATE")
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        End If

                    Case "GON_5_DATE"
                        'If IsDBNull(Me.grdGoneSequence.GetValue("GON_4_DATE")) Then
                        '    Me.ShowMessageInfo("GON_4_DATE IS NULL")
                        '    Me.grdGoneSequence.CancelCurrentEdit()
                        '    Return
                        'End If
                        'If Convert.ToDatetime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_4_DATE")) Then
                        '    Me.ShowMessageInfo("Date is smaler than GON_4_DATE")
                        '    Me.grdGoneSequence.CancelCurrentEdit()
                        '    Return
                        'End If
                        If (Not IsDBNull(Me.grdGoneSequence.GetValue("GON_4_DATE"))) Then
                            If Convert.ToDateTime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_4_DATE")) Then
                                Me.ShowMessageInfo("Date is smaler than GON_4_DATE")
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        End If

                    Case "GON_6_DATE"
                        'If IsDBNull(Me.grdGoneSequence.GetValue("GON_5_DATE")) Then
                        '    Me.ShowMessageInfo("GON_5_DATE IS NULL")
                        '    Me.grdGoneSequence.CancelCurrentEdit()
                        '    Return
                        'End If
                        'If Convert.ToDatetime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_5_DATE")) Then
                        '    Me.ShowMessageInfo("Date is smaler than GON_5_DATE")
                        '    Me.grdGoneSequence.CancelCurrentEdit()
                        '    Return
                        'End If
                        If (Not IsDBNull(Me.grdGoneSequence.GetValue("GON_5_DATE"))) Then
                            If Convert.ToDateTime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_5_DATE")) Then
                                Me.ShowMessageInfo("Date is smaler than GON_5_DATE")
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        End If

                    Case "GON_1_QTY"
                        If (Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value) Or (IsNothing(Me.grdGoneSequence.GetValue("BRANDPACK_ID"))) Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Me.grdGoneSequence.GetValue(e.Column) < 0 Then
                            Me.ShowMessageInfo("GON_QTY < 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Me.grdGoneSequence.GetValue("GON_1_NO") Is DBNull.Value Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        ElseIf (Me.grdGoneSequence.GetValue("GON_1_DATE") Is DBNull.Value) Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        End If

                        If Convert.ToDecimal(Me.grdGoneSequence.GetValue(e.Column)) <= 0 Then
                            'UPDATE COLUM GON_QTY YANG LAIN JADI NOL DAN ""
                            If Me.ShowConfirmedMessage("Are you sure you want to fil <= 0 value ?") = Windows.Forms.DialogResult.Yes Then
                                With Me.grdGoneSequence
                                    .SetValue("GON_1_NO", DBNull.Value)
                                    .SetValue("GON_1_DATE", DBNull.Value)
                                    .SetValue("GON_1_GT_ID", DBNull.Value)
                                    .SetValue("GON_1_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_2_QTY", 0)
                                    .SetValue("GON_2_NO", DBNull.Value)
                                    .SetValue("GON_2_DATE", DBNull.Value)
                                    .SetValue("GON_2_GT_ID", DBNull.Value)
                                    .SetValue("GON_2_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_3_NO", DBNull.Value)
                                    .SetValue("GON_3_QTY", 0)
                                    .SetValue("GON_3_DATE", DBNull.Value)
                                    .SetValue("GON_3_GT_ID", DBNull.Value)
                                    .SetValue("GON_3_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_4_NO", DBNull.Value)
                                    .SetValue("GON_4_QTY", 0)
                                    .SetValue("GON_4_DATE", DBNull.Value)
                                    .SetValue("GON_4_GT_ID", DBNull.Value)
                                    .SetValue("GON_4_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_5_NO", DBNull.Value)
                                    .SetValue("GON_5_QTY", 0)
                                    .SetValue("GON_5_DATE", DBNull.Value)
                                    .SetValue("GON_5_GT_ID", DBNull.Value)
                                    .SetValue("GON_5_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_6_NO", DBNull.Value)
                                    .SetValue("GON_6_QTY", 0)
                                    .SetValue("GON_6_DATE", DBNull.Value)
                                    .SetValue("GON_6_GT_ID", DBNull.Value)
                                    .SetValue("GON_6_TRANSPORTER_NAME", DBNull.Value)
                                End With

                                'Else
                                '    Me.grdGoneSequence.CancelCurrentEdit()
                                '    Return
                            End If

                        End If
                        If IsDBNull(Me.grdGoneSequence.GetValue("GON_1_DATE")) Or IsNothing((Me.grdGoneSequence.GetValue("GON_1_DATE"))) Then
                            Me.grdGoneSequence.SetValue("GON_1_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        End If
                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_1_NO"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_1_NO"))) Then
                            Me.grdGoneSequence.SetValue("GON_1_NO", Me.txtGon_No.Text.Trim())
                        End If
             
                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_1_GT_ID"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_1_GT_ID"))) Then
                            Me.grdGoneSequence.SetValue("GON_1_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        End If

                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_1_TRANSPORTER_NAME"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_1_TRANSPORTER_NAME"))) Then
                            Me.grdGoneSequence.SetValue("GON_1_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())
                        End If



                    Case "GON_2_QTY"
                        If (Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value) Or (IsNothing(Me.grdGoneSequence.GetValue("BRANDPACK_ID"))) Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Me.grdGoneSequence.GetValue(e.Column) < 0 Then
                            Me.ShowMessageInfo("GON_QTY < 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Me.grdGoneSequence.GetValue("GON_2_NO") Is DBNull.Value Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        ElseIf (Me.grdGoneSequence.GetValue("GON_2_DATE") Is DBNull.Value) Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        End If
                        If Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_2_QTY")) <= 0 Then
                            If Me.ShowConfirmedMessage("Are you sure you wan to fill <= 0 value ?") = Windows.Forms.DialogResult.Yes Then
                                With Me.grdGoneSequence
                                    .SetValue("GON_2_NO", DBNull.Value)
                                    .SetValue("GON_2_DATE", DBNull.Value)
                                    .SetValue("GON_2_GT_ID", DBNull.Value)
                                    .SetValue("GON_2_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_3_NO", DBNull.Value)
                                    .SetValue("GON_3_QTY", 0)
                                    .SetValue("GON_3_DATE", DBNull.Value)
                                    .SetValue("GON_3_GT_ID", DBNull.Value)
                                    .SetValue("GON_3_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_4_NO", DBNull.Value)
                                    .SetValue("GON_4_QTY", 0)
                                    .SetValue("GON_4_DATE", DBNull.Value)
                                    .SetValue("GON_4_GT_ID", DBNull.Value)
                                    .SetValue("GON_4_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_5_NO", DBNull.Value)
                                    .SetValue("GON_5_QTY", 0)
                                    .SetValue("GON_5_DATE", DBNull.Value)
                                    .SetValue("GON_5_GT_ID", DBNull.Value)
                                    .SetValue("GON_5_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_6_NO", DBNull.Value)
                                    .SetValue("GON_6_QTY", 0)
                                    .SetValue("GON_6_DATE", DBNull.Value)
                                    .SetValue("GON_6_GT_ID", DBNull.Value)
                                    .SetValue("GON_6_TRANSPORTER_NAME", DBNull.Value)
                                End With

                                'Else
                                '    Me.grdGoneSequence.CancelCurrentEdit()
                                '    Return
                            End If
                        End If
                        If IsDBNull(Me.grdGoneSequence.GetValue("GON_2_DATE")) Or IsNothing((Me.grdGoneSequence.GetValue("GON_2_DATE"))) Then
                            Me.grdGoneSequence.SetValue("GON_2_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        End If
                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_2_NO"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_2_NO"))) Then
                            Me.grdGoneSequence.SetValue("GON_2_NO", Me.txtGon_No.Text.Trim())
                        End If

                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_2_GT_ID"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_2_GT_ID"))) Then
                            Me.grdGoneSequence.SetValue("GON_2_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        End If

                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_2_TRANSPORTER_NAME"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_2_TRANSPORTER_NAME"))) Then
                            Me.grdGoneSequence.SetValue("GON_2_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())
                        End If

                    Case "GON_3_QTY"
                        If (Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value) Or (IsNothing(Me.grdGoneSequence.GetValue("BRANDPACK_ID"))) Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Me.grdGoneSequence.GetValue(e.Column) < 0 Then
                            Me.ShowMessageInfo("GON_QTY < 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf IsDBNull(Me.grdGoneSequence.GetValue("GON_3_NO")) Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        ElseIf (Me.grdGoneSequence.GetValue("GON_3_DATE") Is DBNull.Value) Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        End If
                        If Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_3_QTY")) <= 0 Then
                            If Me.ShowConfirmedMessage("Are you sure you want to fill <= 0 value ?") = Windows.Forms.DialogResult.Yes Then
                                With Me.grdGoneSequence
                                    .SetValue("GON_3_NO", DBNull.Value)
                                    .SetValue("GON_3_QTY", 0)
                                    .SetValue("GON_3_DATE", DBNull.Value)
                                    .SetValue("GON_3_GT_ID", DBNull.Value)
                                    .SetValue("GON_3_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_4_NO", DBNull.Value)
                                    .SetValue("GON_4_QTY", 0)
                                    .SetValue("GON_4_DATE", DBNull.Value)
                                    .SetValue("GON_4_GT_ID", DBNull.Value)
                                    .SetValue("GON_4_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_5_NO", DBNull.Value)
                                    .SetValue("GON_5_QTY", 0)
                                    .SetValue("GON_5_DATE", DBNull.Value)
                                    .SetValue("GON_5_GT_ID", DBNull.Value)
                                    .SetValue("GON_5_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_6_NO", DBNull.Value)
                                    .SetValue("GON_6_QTY", 0)
                                    .SetValue("GON_6_DATE", DBNull.Value)
                                    .SetValue("GON_6_GT_ID", DBNull.Value)
                                    .SetValue("GON_6_TRANSPORTER_NAME", DBNull.Value)
                                End With
                            End If

                            'Else
                            '    Me.grdGoneSequence.CancelCurrentEdit()
                            '    Return
                        End If
                        If IsDBNull(Me.grdGoneSequence.GetValue("GON_3_DATE")) Or IsNothing((Me.grdGoneSequence.GetValue("GON_3_DATE"))) Then
                            Me.grdGoneSequence.SetValue("GON_3_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        End If
                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_3_NO"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_3_NO"))) Then
                            Me.grdGoneSequence.SetValue("GON_3_NO", Me.txtGon_No.Text.Trim())
                        End If

                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_3_GT_ID"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_3_GT_ID"))) Then
                            Me.grdGoneSequence.SetValue("GON_3_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        End If

                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_3_TRANSPORTER_NAME"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_3_TRANSPORTER_NAME"))) Then
                            Me.grdGoneSequence.SetValue("GON_3_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())
                        End If

                    Case "GON_4_QTY"
                        If (Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value) Or (IsNothing(Me.grdGoneSequence.GetValue("BRANDPACK_ID"))) Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Me.grdGoneSequence.GetValue(e.Column) < 0 Then
                            Me.ShowMessageInfo("GON_QTY < 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Me.grdGoneSequence.GetValue("GON_4_NO") Is DBNull.Value Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        ElseIf (Me.grdGoneSequence.GetValue("GON_4_DATE") Is DBNull.Value) Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        End If
                        If Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_4_QTY")) <= 0 Then
                            If Me.ShowConfirmedMessage("Are you sure you want to fill <= 0 value ?") = Windows.Forms.DialogResult.Yes Then
                                With Me.grdGoneSequence
                                    .SetValue("GON_4_NO", DBNull.Value)
                                    .SetValue("GON_4_QTY", 0)
                                    .SetValue("GON_4_DATE", DBNull.Value)
                                    .SetValue("GON_4_GT_ID", DBNull.Value)
                                    .SetValue("GON_4_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_5_NO", DBNull.Value)
                                    .SetValue("GON_5_QTY", 0)
                                    .SetValue("GON_5_DATE", DBNull.Value)
                                    .SetValue("GON_5_GT_ID", DBNull.Value)
                                    .SetValue("GON_5_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_6_NO", DBNull.Value)
                                    .SetValue("GON_6_QTY", 0)
                                    .SetValue("GON_6_DATE", DBNull.Value)
                                    .SetValue("GON_6_GT_ID", DBNull.Value)
                                    .SetValue("GON_6_TRANSPORTER_NAME", DBNull.Value)
                                End With
                            End If
                            'Else
                            '    Me.grdGoneSequence.CancelCurrentEdit()
                            '    Return
                        End If
                        If IsDBNull(Me.grdGoneSequence.GetValue("GON_4_DATE")) Or IsNothing((Me.grdGoneSequence.GetValue("GON_4_DATE"))) Then
                            Me.grdGoneSequence.SetValue("GON_4_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        End If
                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_4_NO"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_4_NO"))) Then
                            Me.grdGoneSequence.SetValue("GON_4_NO", Me.txtGon_No.Text.Trim())
                        End If

                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_4_GT_ID"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_4_GT_ID"))) Then
                            Me.grdGoneSequence.SetValue("GON_4_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        End If

                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_4_TRANSPORTER_NAME"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_4_TRANSPORTER_NAME"))) Then
                            Me.grdGoneSequence.SetValue("GON_4_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())
                        End If

                    Case "GON_5_QTY"
                        If (Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value) Or (IsNothing(Me.grdGoneSequence.GetValue("BRANDPACK_ID"))) Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Me.grdGoneSequence.GetValue(e.Column) < 0 Then
                            Me.ShowMessageInfo("GON_QTY < 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Me.grdGoneSequence.GetValue("GON_5_NO") Is DBNull.Value Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        ElseIf (Me.grdGoneSequence.GetValue("GON_5_DATE") Is DBNull.Value) Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        End If
                        If Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_5_QTY")) <= 0 Then
                            If Me.ShowConfirmedMessage("Are you sure you want to fill <= 0 value ?") = Windows.Forms.DialogResult.Yes Then
                                With Me.grdGoneSequence
                                    .SetValue("GON_5_NO", DBNull.Value)
                                    .SetValue("GON_5_QTY", 0)
                                    .SetValue("GON_5_DATE", DBNull.Value)
                                    .SetValue("GON_5_GT_ID", DBNull.Value)
                                    .SetValue("GON_5_TRANSPORTER_NAME", DBNull.Value)

                                    .SetValue("GON_6_NO", DBNull.Value)
                                    .SetValue("GON_6_QTY", 0)
                                    .SetValue("GON_6_DATE", DBNull.Value)
                                    .SetValue("GON_6_GT_ID", DBNull.Value)
                                    .SetValue("GON_6_TRANSPORTER_NAME", DBNull.Value)
                                End With
                            End If

                            'Else
                            '    Me.grdGoneSequence.CancelCurrentEdit()
                            '    Return
                        End If
                        If IsDBNull(Me.grdGoneSequence.GetValue("GON_5_DATE")) Or IsNothing((Me.grdGoneSequence.GetValue("GON_5_DATE"))) Then
                            Me.grdGoneSequence.SetValue("GON_5_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        End If
                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_5_NO"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_5_NO"))) Then
                            Me.grdGoneSequence.SetValue("GON_5_NO", Me.txtGon_No.Text.Trim())
                        End If

                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_5_GT_ID"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_5_GT_ID"))) Then
                            Me.grdGoneSequence.SetValue("GON_5_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        End If

                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_5_TRANSPORTER_NAME"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_5_TRANSPORTER_NAME"))) Then
                            Me.grdGoneSequence.SetValue("GON_5_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())
                        End If

                    Case "GON_6_QTY"
                        If (Me.grdGoneSequence.GetValue("BRANDPACK_ID") Is DBNull.Value) Or (IsNothing(Me.grdGoneSequence.GetValue("BRANDPACK_ID"))) Then
                            Me.ShowMessageInfo("Please define BrandPack first")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <= 0 Then
                            Me.ShowMessageInfo("SPPB_QTY <= 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Me.grdGoneSequence.GetValue(e.Column) < 0 Then
                            Me.ShowMessageInfo("GON_QTY < 0" & vbCrLf & "System can not procced request")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        ElseIf Me.grdGoneSequence.GetValue("GON_6_NO") Is DBNull.Value Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        ElseIf (Me.grdGoneSequence.GetValue("GON_6_DATE") Is DBNull.Value) Then
                            If Me.IsValidEntry() = False Then
                                Me.grdGoneSequence.CancelCurrentEdit()
                                Return
                            End If
                        End If
                        If Convert.ToDecimal(Me.grdGoneSequence.GetValue("GON_6_QTY")) <= 0 Then
                            If Me.ShowConfirmedMessage("Are you sure you want to fill <= 0 Value ?") Then
                                With Me.grdGoneSequence
                                    .SetValue("GON_6_NO", DBNull.Value)
                                    .SetValue("GON_6_QTY", 0)
                                    .SetValue("GON_6_DATE", DBNull.Value)
                                    .SetValue("GON_6_GT_ID", DBNull.Value)
                                    .SetValue("GON_6_TRANSPORTER_NAME", DBNull.Value)
                                End With
                            End If
                            'Else
                            '    Me.grdGoneSequence.CancelCurrentEdit()
                            '    Return
                        End If
                        If IsDBNull(Me.grdGoneSequence.GetValue("GON_6_DATE")) Or IsNothing((Me.grdGoneSequence.GetValue("GON_6_DATE"))) Then
                            Me.grdGoneSequence.SetValue("GON_6_DATE", Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString()))
                        End If
                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_6_NO"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_6_NO"))) Then
                            Me.grdGoneSequence.SetValue("GON_6_NO", Me.txtGon_No.Text.Trim())
                        End If

                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_6_GT_ID"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_6_GT_ID"))) Then
                            Me.grdGoneSequence.SetValue("GON_6_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                        End If

                        If (IsDBNull(Me.grdGoneSequence.GetValue("GON_6_TRANSPORTER_NAME"))) Or IsNothing((Me.grdGoneSequence.GetValue("GON_6_TRANSPORTER_NAME"))) Then
                            Me.grdGoneSequence.SetValue("GON_6_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())
                        End If

                End Select
                
                Me.grdGoneSequence.UpdateData()

                'Me.SVM = SetValueMode.Changing
                BALANCE = Me.getBalcance(Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")))
                If BALANCE < 0 Then
                    Me.ShowMessageInfo("SUM GON_QTY > SPPB_QTY")
                    Me.grdGoneSequence.CancelCurrentEdit()
                    'If Me.grdGoneSequence.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                    '    Me.grdGoneSequence.MoveToNewRecord()
                    'End If
                    Return
                ElseIf BALANCE = 0 Then
                    Me.grdGoneSequence.SetValue("STATUS", "COMPLETED")
                ElseIf (BALANCE > 0) And (Me.GetSumGonQTY > 0) Then
                    Me.grdGoneSequence.SetValue("STATUS", "PARTIAL")
                ElseIf (BALANCE > 0) And (Me.GetSumGonQTY <= 0) Then
                    Me.grdGoneSequence.SetValue("STATUS", "PENDING")
                End If
                Me.grdGoneSequence.SetValue("BALANCE", BALANCE)

                Me.grdGoneSequence.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                Me.grdGoneSequence.SetValue("MODIFY_DATE", Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()))
                'Me.SVM = SetValueMode.HasChanged
                If Me.SaveMode = ModeSave.Update Then
                    If (e.Column.Key = "GON_1_NO") Or (e.Column.Key = "GON_2_NO") Or (e.Column.Key = "GON_3_NO") Or (e.Column.Key = "GON_4_NO") _
                                        Or (e.Column.Key = "GON_5_NO") Or (e.Column.Key = "GON_6_NO") Then
                        If IsDBNull(Me.grdGoneSequence.GetValue(e.Column)) Or IsNothing(Me.grdGoneSequence.GetValue(e.Column)) Then
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Me.ShowMessageInfo("Please define GON_NO")
                            Return
                        End If
                    End If
                    If Not IsDBNull(Me.grdGoneSequence.GetValue("ISREVISION")) Then
                        If CBool(Me.grdGoneSequence.GetValue("ISREVISION")) = True Then
                            Me.grdGoneSequence.RootTable.Columns("SPPB_QTY").Selectable = True
                        Else
                            Me.grdGoneSequence.RootTable.Columns("SPPB_QTY").Selectable = False
                        End If
                    Else
                        Me.grdGoneSequence.RootTable.Columns("SPPB_QTY").Selectable = False
                    End If
                End If
            End If
            Me.grdGoneSequence.UpdateData()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.grdGoneSequence.CancelCurrentEdit()
            Me.LogMyEvent(ex.Message, Me.Name + "_grdGoneSequence_CellValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " Calendar combo "
    Private Sub dtPicGonDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtPicGonDate.ValueChanged
        Try

            If Me.SaveMode = ModeSave.Save Then
                If Not Me.IsFormLoaded Then : Return : End If
                If Me.SFG = StateFillingGrid.Filling Or Me.SFG = StateFillingGrid.Disposing Then : Return : End If
            End If
            Me.Cursor = Cursors.WaitCursor
            If Me.dtPicGonDate.Text <> "" Then
                If Me.cmbGonSequence.Text <> "" Then
                    If IsNumeric(Me.cmbGonSequence.Text) Then
                        Return
                    Else
                        Me.ShowMessageInfo("Please define GS_NO first.")
                        Me.cmbGonSequence.Focus()
                        Me.dtPicGonDate.Text = ""
                        Return
                    End If
                Else
                    Me.ShowMessageInfo("Please define GS_NO first.")
                    Me.cmbGonSequence.Focus()
                    Me.dtPicGonDate.Text = ""
                    Return
                End If
            End If
            Me.ChangedGonDate = Convert.ToDateTime(Me.dtPicGonDate.Value.ToShortDateString())

        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#End Region

    'Private Sub grdGoneSequence_CellValueChanged(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdGoneSequence.CellValueChanged
    '    Try
    '        If (Me.SFG = StateFillingGrid.Filling) Or (Me.SFG = StateFillingGrid.Disposing) Then
    '            Return
    '        End If
    '        If e.Column.Key = "GON_1_DATE" Then
    '            If Convert.ToDatetime(Me.grdGoneSequence.GetValue(e.Column)) < Convert.ToDatetime(Me.dtPicSPPBDate.Value.ToShortDateString()) Then
    '                Me.ShowMessageInfo("Date is smaler than SPPB_DATE")
    '                Me.grdGoneSequence.CancelCurrentEdit()
    '                Return
    '            End If
    '        End If
    '        If e.Column.Key = "GON_2_DATE" Then
    '            If IsDBNull(Me.grdGoneSequence.GetValue("GON_1_DATE")) Then
    '                Me.ShowMessageInfo("GON_1_DATE IS NULL")
    '                Me.grdGoneSequence.CancelCurrentEdit()
    '                Return
    '            End If
    '            If Convert.ToDatetime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_1_DATE")) Then
    '                Me.ShowMessageInfo("Date is smaler than GON_1_DATE")
    '                Me.grdGoneSequence.CancelCurrentEdit()
    '                Return
    '            End If
    '        ElseIf e.Column.Key = "GON_3_DATE" Then
    '            If IsDBNull(Me.grdGoneSequence.GetValue("GON_2_DATE")) Then
    '                Me.ShowMessageInfo("GON_2_DATE IS NULL")
    '                Me.grdGoneSequence.CancelCurrentEdit()
    '                Return
    '            End If

    '            If Convert.ToDatetime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_2_DATE")) Then
    '                Me.ShowMessageInfo("Date is smaler than GON_2_DATE")
    '                Me.grdGoneSequence.CancelCurrentEdit()
    '                Return
    '            End If
    '        ElseIf e.Column.Key = "GON_4_DATE" Then
    '            If IsDBNull(Me.grdGoneSequence.GetValue("GON_3_DATE")) Then
    '                Me.ShowMessageInfo("GON_3_DATE IS NULL")
    '                Me.grdGoneSequence.CancelCurrentEdit()
    '                Return
    '            End If
    '            If Convert.ToDatetime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_3_DATE")) Then
    '                Me.ShowMessageInfo("Date is smaler than GON_3_DATE")
    '                Me.grdGoneSequence.CancelCurrentEdit()
    '                Return
    '            End If
    '        ElseIf e.Column.Key = "GON_5_DATE" Then
    '            If IsDBNull(Me.grdGoneSequence.GetValue("GON_4_DATE")) Then
    '                Me.ShowMessageInfo("GON_4_DATE IS NULL")
    '                Me.grdGoneSequence.CancelCurrentEdit()
    '                Return
    '            End If
    '            If Convert.ToDatetime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_4_DATE")) Then
    '                Me.ShowMessageInfo("Date is smaler than GON_4_DATE")
    '                Me.grdGoneSequence.CancelCurrentEdit()
    '                Return
    '            End If
    '        ElseIf e.Column.Key = "GON_6_DATE" Then
    '            If IsDBNull(Me.grdGoneSequence.GetValue("GON_5_DATE")) Then
    '                Me.ShowMessageInfo("GON_5_DATE IS NULL")
    '                Me.grdGoneSequence.CancelCurrentEdit()
    '                Return
    '            End If
    '            If Convert.ToDatetime(Me.grdGoneSequence.GetValue(e.Column)) < DateTime.Parse(Me.grdGoneSequence.GetValue("GON_5_DATE")) Then
    '                Me.ShowMessageInfo("Date is smaler than GON_5_DATE")
    '                Me.grdGoneSequence.CancelCurrentEdit()
    '                Return
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub grdGoneSequence_CellValueChanged(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdGoneSequence.CellValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim Remark As String = ""
            If Not IsDBNull(Me.grdGoneSequence.GetValue("ISREVISION")) Then
                Me.grdGoneSequence.RootTable.Columns("SPPB_QTY").Selectable = False
                If CBool(Me.grdGoneSequence.GetValue("ISREVISION")) = True Then
                    Me.grdGoneSequence.RootTable.Columns("SPPB_QTY").Selectable = True
                Else
                    Me.grdGoneSequence.RootTable.Columns("SPPB_QTY").Selectable = False
                    'CHECK APAKAH OA_QTY = SPPB_QTY
                    If e.Column.Key = "ISREVISION" Then
                        Dim OA_QTY As Decimal = Me.clsSPPBDetail.GetTotalOAQty(Me.grdGoneSequence.GetValue("OA_BRANDPACK_ID").ToString(), True, False, Remark)
                        If Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <> OA_QTY Then
                            Me.ShowMessageInfo("Can not change revision " & vbCrLf & "Because OA_QTY <> SPPB_QTY" & vbCrLf & "Please correct OA_QTY")
                            Me.grdGoneSequence.CancelCurrentEdit()
                            Return
                        End If
                    End If
                End If
            Else
                'CHECK APAKAH OA_QTY = SPPB_QTY
                If e.Column.Key = "ISREVISION" Then
                    Dim OA_QTY As Decimal = Me.clsSPPBDetail.GetTotalOAQty(Me.grdGoneSequence.GetValue("OA_BRANDPACK_ID").ToString(), True, False, Remark)
                    If Convert.ToDecimal(Me.grdGoneSequence.GetValue("SPPB_QTY")) <> OA_QTY Then
                        Me.ShowMessageInfo("Can not change revision " & vbCrLf & "Because OA_QTY <> SPPB_QTY" & vbCrLf & "Please correct OA_QTY")
                        Me.grdGoneSequence.CancelCurrentEdit()
                        Return
                    End If
                End If
            End If

            'Me.grdGoneSequence.UpdateData()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdGoneSequence_CellValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtGon_No_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGon_No.KeyDown
        If e.KeyCode = Keys.Enter Then

            Try
                Me.Cursor = Cursors.WaitCursor
                If Me.txtGon_No.Text <> "" Then
                    Select Case Me.SaveMode
                        Case ModeSave.Save
                            If Me.clsSPPBDetail.IsGonHasExists(RTrim(Me.txtGon_No.Text), RTrim(Me.txtSPPBNo.Text), "GON_" & Me.cmbGonSequence.Text, NufarmBussinesRules.common.Helper.SaveMode.Insert) = True Then
                                Me.txtGon_No.Text = Me.txtGon_No.Text.Remove(Me.txtGon_No.Text.Length, 1) : Me.ShowMessageInfo("GON_NO " & RTrim(Me.txtGon_No.Text) & " with SPPB_NO " & RTrim(Me.txtSPPBNo.Text) & " has existed !.") : Return
                            End If

                            'check apakah no gon yang di maksud dengan sppb_yang tercetak do SPPB_NO sudah ada / belum
                        Case ModeSave.Update
                            'chek apakah no gon lebih dari satu / gak untuk sppb_ini
                            If Me.clsSPPBDetail.IsGonHasExists(RTrim(Me.txtGon_No.Text), RTrim(Me.txtSPPBNo.Text), "GON_" & Me.cmbGonSequence.Text, NufarmBussinesRules.common.Helper.SaveMode.Update) = True Then
                                Me.txtGon_No.Text = Me.txtGon_No.Text.Remove(Me.txtGon_No.Text.Length, 1) : Me.ShowMessageInfo("GON_NO " & RTrim(Me.txtGon_No.Text) & " with SPPB_NO " & RTrim(Me.txtSPPBNo.Text) & " has existed !.") : Return
                            End If
                    End Select
                End If
            Catch ex As Exception
                Me.txtGon_No.SelectAll() : Me.txtGon_No.Focus() : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_txtGon_No_KeyDown")
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End If
    End Sub

    Private Sub txtGon_No_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGon_No.Leave
        Try
            Try
                Me.Cursor = Cursors.WaitCursor
                If Me.isClosingForm Then : Return : End If
                If Me.txtGon_No.Text <> "" Then
                    Select Case Me.SaveMode
                        Case ModeSave.Save
                            If Me.clsSPPBDetail.IsGonHasExists(RTrim(Me.txtGon_No.Text), RTrim(Me.txtSPPBNo.Text), "GON_" & Me.cmbGonSequence.Text, NufarmBussinesRules.common.Helper.SaveMode.Insert) = True Then
                                Me.txtGon_No.Text = Me.txtGon_No.Text.Remove(Me.txtGon_No.Text.Length, 1) : Me.ShowMessageInfo("GON_NO " & RTrim(Me.txtGon_No.Text) & " with SPPB_NO " & RTrim(Me.txtSPPBNo.Text) & " has existed !.") : Return
                            End If

                            'check apakah no gon yang di maksud dengan sppb_yang tercetak do SPPB_NO sudah ada / belum
                        Case ModeSave.Update
                            'chek apakah no gon lebih dari satu / gak untuk sppb_ini
                            If Me.clsSPPBDetail.IsGonHasExists(RTrim(Me.txtGon_No.Text), RTrim(Me.txtSPPBNo.Text), "GON_" & Me.cmbGonSequence.Text, NufarmBussinesRules.common.Helper.SaveMode.Update) = True Then
                                Me.txtGon_No.Text = Me.txtGon_No.Text.Remove(Me.txtGon_No.Text.Length - 1, 1) : Me.ShowMessageInfo("GON_NO " & RTrim(Me.txtGon_No.Text) & " with SPPB_NO " & RTrim(Me.txtSPPBNo.Text) & " has existed !.") : Return
                            End If
                    End Select
                End If

            Catch ex As Exception
                Me.txtGon_No.SelectAll() : Me.txtGon_No.Focus() : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_txtGon_No_KeyDown")
            Finally
                Me.Cursor = Cursors.Default
            End Try
        Catch ex As Exception

        End Try

    End Sub


    Private Sub txtGon_No_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGon_No.TextChanged
        If Me.txtGon_No.Text <> "" Then
            Me.ChangedGonNumber = RTrim(Me.txtGon_No.Text)
        End If
    End Sub

    Private Sub dtPicRelease_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicRelease.CheckedChanged
        Try
            If Not Me.IsFormLoaded Then : Return : End If
            If Not Me.SFG = StateFillingGrid.HasFilled Then : Return : End If
            If Not IsNothing(Me.grdGoneSequence.DataSource) Then
                If Me.grdGoneSequence.RecordCount > 0 Then
                    If dtPicRelease.Checked Then
                        For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdGoneSequence.GetDataRows()
                            row.BeginEdit()
                            row.Cells("RELEASE_DATE").Value = Convert.ToDateTime(Me.dtPicRelease.Value.ToShortDateString())
                            row.EndEdit()
                        Next
                        'Me.grdGoneSequence.SetValue("RELEASE_DATE", )
                    Else
                        For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdGoneSequence.GetDataRows()
                            row.BeginEdit()
                            row.Cells("RELEASE_DATE").Value = DBNull.Value
                            row.EndEdit()
                        Next
                        If Me.SFG = StateFillingGrid.Filling Then
                            Me.dtPicRelease.Text = ""
                        Else
                            Me.SFG = StateFillingGrid.Filling : Me.dtPicRelease.Text = "" : Me.SFG = StateFillingGrid.HasFilled
                        End If
                    End If
                    Me.grdGoneSequence.UpdateData()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub dtPicRelease_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicRelease.TextChanged
        Try
            If Not Me.IsFormLoaded Then : Return : End If
            If Not Me.SFG = StateFillingGrid.HasFilled Then : Return : End If
            If Not Me.dtPicRelease.Checked Then
                If Me.SFG = StateFillingGrid.Filling Then
                    Me.dtPicRelease.Text = ""
                Else
                    Me.SFG = StateFillingGrid.Filling : Me.dtPicRelease.Text = "" : Me.SFG = StateFillingGrid.HasFilled : Return
                End If

            End If
            If Not IsNothing(Me.grdGoneSequence.DataSource) Then
                If Me.grdGoneSequence.RecordCount > 0 Then
                    If (Me.dtPicRelease.Text <> "") Then
                        For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdGoneSequence.GetDataRows()
                            row.BeginEdit()
                            row.Cells("RELEASE_DATE").Value = DateTime.Parse(dtPicRelease.Text)
                            row.EndEdit()
                        Next
                    Else
                        For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdGoneSequence.GetDataRows()
                            row.BeginEdit()
                            row.Cells("RELEASE_DATE").Value = DBNull.Value
                            row.EndEdit()
                        Next
                    End If
                    Me.grdGoneSequence.UpdateData()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFilterTransporter_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterTransporter.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dtTable As DataTable = Me.clsSPPBDetail.getTransporter(Me.mcbTransporter.Text.TrimEnd().TrimStart(), True)
            Me.BindMulticolumnCombo(Me.mcbTransporter, dtTable.DefaultView) : Me.mcbTransporter.Focus()
            Me.Cursor = Cursors.Default
            MessageBox.Show(dtTable.Rows.Count.ToString() & " item(s) found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub mcbTransporter_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Me.SFM = StateFillingMCB.Filling Then : Return : End If
    '    If Not Me.IsFormLoaded Then : Return : End If
    '    If Me.SFG = StateFillingGrid.Filling Or Me.SFG = StateFillingGrid.Disposing Then : Return : End If
    '    If IsNothing(Me.mcbTransporter.Value) Then : Return : End If
    '    If Me.mcbTransporter.SelectedIndex <= -1 Then : Return : End If

    'End Sub

End Class
