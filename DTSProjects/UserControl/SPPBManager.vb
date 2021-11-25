Imports System.Threading
Public Class SPPBManager
    Friend WithEvents frmParentSPPB As SPPB = Nothing
    Friend WithEvents frmParentGrid As ReportGrid = Nothing
    'Public Event gridCurrentCell_Changed(ByVal sender As Object, ByVal e As EventArgs)
    Friend IsLoadding As Boolean
    Private LD As Loading
    Private ThreadProgress As Thread = Nothing
    Friend StatProg As StatusProgress = StatusProgress.None
    Private ObjChanges As New ObjMustReloadDatda()
    Private MObjChanges As New ObjMustReloadDatda()
    Private clsSPPBGON As New NufarmBussinesRules.OrderAcceptance.SPPBGONManager()
    Private DS As DataSet = Nothing
    Private MasterCategory As String = "ByPO"
    Private CustomCategory As String = "DISTRIBUTOR"
    Private m_Grid As Janus.Windows.GridEX.GridEX = Me.grdHeader
    Private IsFirsLoad As Boolean = True
    Private CategoryType As String = "MasterCategory"
    Public Property Grid() As Janus.Windows.GridEX.GridEX
        Get
            Return Me.m_Grid
        End Get
        Set(ByVal value As Janus.Windows.GridEX.GridEX)
            Me.m_Grid = value
        End Set
    End Property
    Friend Enum StatusProgress
        None
        Processing
    End Enum
    Friend Sub ShowProceed()
        Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Thread.Sleep(50) : Me.LD.Refresh() : Application.DoEvents()
        End While
        Thread.Sleep(100)
        Me.LD.Close() : Me.LD = Nothing
    End Sub
    Private Sub ReadAcces()
        Dim IsSySA As Boolean = False
        If Not IsNothing(Me.frmParentGrid) Then
            IsSySA = frmParentGrid.CMain.IsSystemAdministrator
        ElseIf Not IsNothing(Me.frmParentSPPB) Then
            IsSySA = Me.frmParentSPPB.CMain.IsSystemAdministrator
        End If
        If Not IsSySA Then
            If Not IsNothing(Me.frmParentSPPB) Then
                frmParentSPPB.btnEditSPPB.Visible = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.SPPB
                frmParentSPPB.btnNewSPPB.Visible = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SPPB
                If Not frmParentSPPB.btnNewSPPB.Visible And Not frmParentSPPB.btnGonAfterSignedByDistributor.Visible Then
                    frmParentSPPB.btnAddNew.Visible = False
                End If
                If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.SPPB Then
                    Me.grdHeader.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                Else
                    Me.grdHeader.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                End If
            End If
        End If
    End Sub
    '====================================OLD PROCESS===============================================
    'Private ReadOnly Property clsSPPBDetail() As NufarmBussinesRules.OrderAcceptance.SPPB_Detail
    '    Get
    '        If Me.m_clsSPPBDetail Is Nothing Then
    '            Me.m_clsSPPBDetail = New NufarmBussinesRules.OrderAcceptance.SPPB_Detail()
    '        End If
    '        Return Me.m_clsSPPBDetail
    '    End Get
    'End Property
    'Private m_clsSPPBDetail As NufarmBussinesRules.OrderAcceptance.SPPB_Detail
    '====================================================================================================
    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdHeader.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.IsLoadding Then : Return : End If

            If IsNothing(Me.grdHeader.DataSource) Then
                If Not IsNothing(Me.frmParentSPPB) Then
                    frmParentSPPB.btnEditSPPB.Enabled = False
                End If
                Return
            End If
            If Me.grdHeader.RecordCount <= 0 Then
                If Not IsNothing(Me.frmParentSPPB) Then
                    frmParentSPPB.btnEditSPPB.Enabled = False
                End If
                Return
            End If
            If Me.grdHeader.SelectedItems.Count <= 0 Then
                If Not IsNothing(Me.frmParentSPPB) Then
                    frmParentSPPB.btnEditSPPB.Enabled = False
                End If
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.IsLoadding = True
            Dim SPPBBrandPackID As Object = DBNull.Value
            If Me.grdHeader.GetRow.RowType() = Janus.Windows.GridEX.RowType.Record Then
                Dim IsSySA As Boolean = False
                If Not IsNothing(Me.frmParentGrid) Then
                    IsSySA = frmParentGrid.CMain.IsSystemAdministrator
                ElseIf Not IsNothing(Me.frmParentSPPB) Then
                    IsSySA = Me.frmParentSPPB.CMain.IsSystemAdministrator
                End If
                SPPBBrandPackID = Me.grdHeader.GetValue("SPPB_BRANDPACK_ID")
                If Not IsNothing(Me.frmParentSPPB) Then
                    If Not IsDBNull(SPPBBrandPackID) And Not IsNothing(SPPBBrandPackID) Then
                        If Not String.IsNullOrEmpty(SPPBBrandPackID) Then
                            If Not IsSySA Then
                                frmParentSPPB.btnEditSPPB.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.SPPB
                            Else : frmParentSPPB.btnEditSPPB.Enabled = True
                            End If
                        Else
                            If Not NufarmBussinesRules.User.UserLogin.IsAdmin Then
                                frmParentSPPB.btnEditSPPB.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.SPPB
                            Else : frmParentSPPB.btnEditSPPB.Enabled = True
                            End If
                        End If
                    Else
                        If Not IsSySA Then
                            frmParentSPPB.btnEditSPPB.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.SPPB
                        Else : frmParentSPPB.btnEditSPPB.Enabled = True
                        End If
                    End If
                Else
                    If Not IsNothing(Me.frmParentSPPB) Then
                        frmParentSPPB.btnEditSPPB.Enabled = False
                    End If
                    If Not IsNothing(Me.grdDetail.DataSource) Then
                        Me.grdDetail.RemoveFilters()
                        CType(Me.grdDetail.DataSource, DataView).RowFilter = ""
                    End If
                End If
            Else
                If Not IsNothing(Me.frmParentSPPB) Then
                    frmParentSPPB.btnEditSPPB.Enabled = False
                End If
                If Not IsNothing(Me.grdDetail.DataSource) Then
                    Me.grdDetail.RemoveFilters()
                    CType(Me.grdDetail.DataSource, DataView).RowFilter = ""
                End If
            End If
            If Me.chkFilter.Checked Then
                ''filter grid Detail dengan SPPB_BrandPackID
                Me.IsLoadding = True
                If IsNothing(SPPBBrandPackID) Then
                ElseIf IsDBNull(SPPBBrandPackID) Then
                Else
                    If Not String.IsNullOrEmpty(SPPBBrandPackID) Then
                        Dim RowFilter As String = "SPPB_BRANDPACK_ID  = '" & SPPBBrandPackID & "'"
                        If Me.DS.Tables.Contains("GON_DETAIL_INFO") Then
                            Dim DV As DataView = Me.DS.Tables("GON_DETAIL_INFO").DefaultView()
                            DV.RowFilter = RowFilter
                            'bind grid
                            Me.BindGrid(DV, Me.grdDetail, False)
                        End If
                    End If
                End If
            End If
            Me.IsLoadding = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.IsLoadding = False
            If Not IsNothing(Me.frmParentSPPB) Then
                Me.frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_GridEX1_CurrentCellChanged")
            ElseIf Not IsNothing(Me.frmParentGrid) Then
                Me.frmParentGrid.LogMyEvent(ex.Message, Me.Name + "_GridEX1_CurrentCellChanged")
            End If

        End Try

    End Sub
    Private Sub BindGrid(ByVal dtView As DataView, ByVal GridN As Janus.Windows.GridEX.GridEX, ByVal mustReload As Boolean)
        Me.IsLoadding = True
        Dim HasLoadDataBefore As Boolean = False
        If Not IsNothing(GridN.DataSource) Then
            If GridN.RootTable.Columns.Count > 0 Then
                HasLoadDataBefore = True
            End If
        End If
        GridN.SetDataBinding(dtView, "")
        If (IsNothing(dtView)) Then : Return : End If
        If dtView.Count <= 0 Then : Return : End If

        If Not HasLoadDataBefore Then
            GridN.RetrieveStructure()
        ElseIf mustReload Then
            GridN.RetrieveStructure()
        End If
        '=========================================OLD PROCESS==========================================================
        'If Me.grdHeader.GetTotal(Me.grdHeader.RootTable.Columns("GON_6_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
        '    Me.grdHeader.RootTable.Columns("GON_6_NO").Visible = False
        '    Me.grdHeader.RootTable.Columns("GON_6_DATE").Visible = False
        '    Me.grdHeader.RootTable.Columns("GON_6_QTY").Visible = False

        '    Me.grdHeader.RootTable.Columns("GON_6_TRANSPORTER_NAME").Visible = False
        'Else
        '    With Me.grdHeader.RootTable
        '        .Columns("GON_6_NO").Visible = True
        '        .Columns("GON_6_DATE").Visible = True
        '        .Columns("GON_6_QTY").Visible = True

        '        .Columns("GON_6_TRANSPORTER_NAME").Visible = True
        '    End With
        'End If

        'If Me.grdHeader.GetTotal(Me.grdHeader.RootTable.Columns("GON_5_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
        '    With Me.grdHeader.RootTable
        '        .Columns("GON_5_NO").Visible = False
        '        .Columns("GON_5_DATE").Visible = False
        '        .Columns("GON_5_QTY").Visible = False

        '        .Columns("GON_5_TRANSPORTER_NAME").Visible = False
        '    End With
        'Else
        '    With Me.grdHeader.RootTable
        '        .Columns("GON_5_NO").Visible = True
        '        .Columns("GON_5_DATE").Visible = True
        '        .Columns("GON_5_QTY").Visible = True

        '        .Columns("GON_5_TRANSPORTER_NAME").Visible = True
        '    End With
        'End If
        'If Me.grdHeader.GetTotal(Me.grdHeader.RootTable.Columns("GON_4_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
        '    With Me.grdHeader.RootTable
        '        .Columns("GON_4_NO").Visible = False
        '        .Columns("GON_4_DATE").Visible = False
        '        .Columns("GON_4_QTY").Visible = False

        '        .Columns("GON_4_TRANSPORTER_NAME").Visible = False
        '    End With
        'Else
        '    With Me.grdHeader.RootTable
        '        .Columns("GON_4_NO").Visible = True
        '        .Columns("GON_4_DATE").Visible = True
        '        .Columns("GON_4_QTY").Visible = True

        '        .Columns("GON_4_TRANSPORTER_NAME").Visible = True
        '    End With
        'End If
        'If Me.grdHeader.GetTotal(Me.grdHeader.RootTable.Columns("GON_3_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
        '    With Me.grdHeader.RootTable
        '        .Columns("GON_3_NO").Visible = False
        '        .Columns("GON_3_DATE").Visible = False
        '        .Columns("GON_3_QTY").Visible = False

        '        .Columns("GON_3_TRANSPORTER_NAME").Visible = False
        '    End With
        'Else
        '    With Me.grdHeader.RootTable
        '        .Columns("GON_3_NO").Visible = True
        '        .Columns("GON_3_DATE").Visible = True
        '        .Columns("GON_3_QTY").Visible = True

        '        .Columns("GON_3_TRANSPORTER_NAME").Visible = True
        '    End With
        'End If
        'If Me.grdHeader.GetTotal(Me.grdHeader.RootTable.Columns("GON_2_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
        '    With Me.grdHeader.RootTable
        '        .Columns("GON_2_NO").Visible = False
        '        .Columns("GON_2_DATE").Visible = False
        '        .Columns("GON_2_QTY").Visible = False

        '        .Columns("GON_2_TRANSPORTER_NAME").Visible = False

        '    End With
        'Else
        '    With Me.grdHeader.RootTable
        '        .Columns("GON_2_NO").Visible = True
        '        .Columns("GON_2_DATE").Visible = True
        '        .Columns("GON_2_QTY").Visible = True

        '        .Columns("GON_2_TRANSPORTER_NAME").Visible = True
        '    End With
        'End If
        'If Me.grdHeader.GetTotal(Me.grdHeader.RootTable.Columns("GON_1_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
        '    With Me.grdHeader.RootTable
        '        .Columns("GON_1_NO").Visible = False
        '        .Columns("GON_1_DATE").Visible = False
        '        .Columns("GON_1_QTY").Visible = False

        '        .Columns("GON_1_TRANSPORTER_NAME").Visible = False
        '    End With
        'Else
        '    With Me.grdHeader.RootTable
        '        .Columns("GON_1_NO").Visible = True
        '        .Columns("GON_1_DATE").Visible = True
        '        .Columns("GON_1_QTY").Visible = True

        '        .Columns("GON_1_TRANSPORTER_NAME").Visible = True
        '    End With
        'End If
        'Me.grdHeader.RootTable.Columns("GON_1_GT_ID").Visible = False
        'Me.grdHeader.RootTable.Columns("GON_2_GT_ID").Visible = False
        'Me.grdHeader.RootTable.Columns("GON_3_GT_ID").Visible = False
        'Me.grdHeader.RootTable.Columns("GON_4_GT_ID").Visible = False
        'Me.grdHeader.RootTable.Columns("GON_5_GT_ID").Visible = False
        'Me.grdHeader.RootTable.Columns("GON_6_GT_ID").Visible = False

        'Me.grdHeader.RootTable.Columns("GON_1_GT_ID").ShowInFieldChooser = False
        'Me.grdHeader.RootTable.Columns("GON_2_GT_ID").ShowInFieldChooser = False
        'Me.grdHeader.RootTable.Columns("GON_3_GT_ID").ShowInFieldChooser = False
        'Me.grdHeader.RootTable.Columns("GON_4_GT_ID").ShowInFieldChooser = False
        'Me.grdHeader.RootTable.Columns("GON_5_GT_ID").ShowInFieldChooser = False
        'Me.grdHeader.RootTable.Columns("GON_6_GT_ID").ShowInFieldChooser = False

        'Me.grdHeader.RootTable.Columns("GON_1_TRANSPORTER_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        'Me.grdHeader.RootTable.Columns("GON_2_TRANSPORTER_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        'Me.grdHeader.RootTable.Columns("GON_3_TRANSPORTER_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        'Me.grdHeader.RootTable.Columns("GON_4_TRANSPORTER_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        'Me.grdHeader.RootTable.Columns("GON_5_TRANSPORTER_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        'Me.grdHeader.RootTable.Columns("GON_6_TRANSPORTER_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        '==================================================================================================

        'Me.grdHeader.RootTable.Columns("STATUS").Visible = True
        'Me.grdHeader.RootTable.Columns("BALANCE").Visible = True
        'Me.grdHeader.RootTable.Columns("REMARK").Visible = True

        'DISTRIBUTOR_ID()
        'BRANDPACK_ID()
        'OA_BRANDPACK_ID()
        'SPPB_BRANDPACK_ID()
        For Each col As Janus.Windows.GridEX.GridEXColumn In GridN.RootTable.Columns
            If col.DataMember = "PRICE" Then
                col.FormatString = "#,##0.00"
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.None
            ElseIf col.DataMember.Contains("QTY") Or col.DataMember.Contains("BALANCE") Then
                col.FormatString = "#,##0.000"
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                col.TotalFormatString = "#,##0.000"
            ElseIf col.DataMember = "TOTAL_SALES_VALUE" Then
                col.FormatString = "#,##0.00"
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                col.TotalFormatString = "#,##0.00"
            ElseIf col.Type Is Type.GetType("System.Boolean") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
                col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ElseIf col.Type Is Type.GetType("System.DateTime") Then
                col.FormatString = "dd MMMM yyyy"
                col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            ElseIf col.DataMember.Contains("_ID") Then
                col.Visible = False
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            Else
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            End If
            If col.DataMember = "CREATE_DATE" Or col.DataMember = "CREATE_BY" Or col.DataMember = "CreatedBy" Or col.DataMember = "CreatedDate" Then
                col.Visible = False
            End If
            If col.DataMember = "TOTAL_GON" Then
                col.FormatString = ""
                col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.None
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End If
            If col.DataMember = "IsUpdatedBySystem" Then : col.Visible = False : col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox : col.EditType = Janus.Windows.GridEX.EditType.CheckBox : End If
            col.AutoSize()
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
        Next
        'Me.IsLoadding = False
        If GridN.Name = "grdHeader" Then
            Me.AddConditionalFormatingGridSPPB() : Me.AddConditionalFormatingGridSPPB1() : Me.AddConditionalFormatingGridSPPB2()
        ElseIf GridN.Name = "grdDetail" Then
            AddConditionalFormatingGridGON() : AddConditionalFormatingGridGON1()
        End If
    End Sub
    Private Sub AddConditionalFormatingGridSPPB()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdHeader.RootTable.Columns("IsUpdatedBySystem"), Janus.Windows.GridEX.ConditionOperator.Equal, True)
        fc.FormatStyle.ForeColor = Color.Red
        fc.AllowMerge = True
        grdHeader.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub AddConditionalFormatingGridSPPB1()
        'UNAVAILABLE_STOCK()
        'AWAITING_TRANSPORTER()
        'QUOTA_SHIPMENT()
        'OTHER()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdHeader.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "UNKNOWN")
        fc.FormatStyle.ForeColor = Color.Red
        fc.AllowMerge = True

        Dim fc1 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdHeader.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "STOCK_UNAVAILABLE")
        fc1.FormatStyle.ForeColor = Color.Red
        fc1.AllowMerge = True

        Dim fc2 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdHeader.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "AWAITING_TRANSPORTER")
        fc2.FormatStyle.ForeColor = Color.Red
        fc2.AllowMerge = True

        Dim fc3 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdHeader.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "QUOTA_SHIPMENT")
        fc3.FormatStyle.ForeColor = Color.Red
        fc3.AllowMerge = True

        Dim fc4 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdHeader.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "OTHER")
        fc4.FormatStyle.ForeColor = Color.Red
        fc4.AllowMerge = True


        Dim fc5 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdHeader.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "")
        fc5.FormatStyle.ForeColor = Color.Red
        fc5.AllowMerge = True

        grdHeader.RootTable.FormatConditions.AddRange(New Janus.Windows.GridEX.GridEXFormatCondition() {fc, fc1, fc2, fc3, fc4, fc5})
    End Sub
    Private Sub AddConditionalFormatingGridGON()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdDetail.RootTable.Columns("IsUpdatedBySystem"), Janus.Windows.GridEX.ConditionOperator.Equal, True)
        fc.FormatStyle.ForeColor = Color.Red
        fc.AllowMerge = True
        Me.grdDetail.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub AddConditionalFormatingGridGON1()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdDetail.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "CANCELED")
        fc.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
        fc.AllowMerge = True
        Me.grdDetail.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub AddConditionalFormatingGridSPPB2()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdHeader.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "CANCELED")
        fc.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
        fc.AllowMerge = True
        grdHeader.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub MustReloadSPPB()

        If Me.IsLoadding Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.frmParentSPPB.MustReloadData = True
            Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
            Me.frmParentSPPB.MustReloadData = False
        Catch ex As Exception
            Me.IsLoadding = False
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
            Me.frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_MustReloadSPPB")
            Me.frmParentSPPB.ShowMessageInfo(ex.Message)
        End Try
    End Sub
    Private Sub MustReloadGrid()

        If Me.IsLoadding Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.frmParentGrid.MustReloadData = True
            Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
            Me.frmParentGrid.MustReloadData = False
        Catch ex As Exception
            Me.IsLoadding = False
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
            Me.frmParentGrid.LogMyEvent(ex.Message, Me.Name + "_MustReloadSPPB")
            Me.frmParentGrid.ShowMessageInfo(ex.Message)
        End Try
    End Sub
    Private Sub cmbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmParentSPPB.ComboboxValue_Changed
        Try
            If Me.IsLoadding Then : Return : End If
            If Me.frmParentSPPB.cmbDistributor.Text = "" Then : Return : End If
            If Me.frmParentSPPB.cmbDistributor.Value Is Nothing Then : Return : End If
            If Me.cmbFilterBy.Text <> "DISTRIBUTOR" Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.ShowData()
            Me.IsLoadding = False
        Catch ex As Exception
            Me.IsLoadding = False
            Me.frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_cmbDistributor_ValueChanged")
            Me.StatProg = StatusProgress.None
        Finally
            Me.frmParentSPPB.cmbDistributor.Enabled = True : Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdHeader.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor

            If CDec(Me.grdHeader.GetValue("SPPB_QTY")) > 0 Then
                If MessageBox.Show("Are you sure you want to delete data ?" & vbCrLf & "Opertion can not be undone.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                    e.Cancel = True
                    Return
                    ' = Windows.Forms.DialogResult.No Then

                Else
                    '==========================OLD PROCESSS==============================================
                    'Me.clsSPPBDetail.DeleteSPPPB_BrandPack(Me.grdHeader.GetValue("OA_BRANDPACK_ID").ToString())
                    '=====================================================================================
                    Me.clsSPPBGON.DeleteSPPBBrandPack(Me.grdHeader.GetValue("OA_BRANDPACK_ID").ToString())
                    e.Cancel = False
                    'Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
                End If
            Else
                'Me.ShowMessageInfo("Can not delete data" & vbCrLf & "SPPB has not been defined")
                e.Cancel = True
            End If
            Me.grdHeader.UpdateData() : Me.grdHeader.MoveToNewRecord()
        Catch ex As Exception
            e.Cancel = True : Me.frmParentSPPB.ShowMessageInfo(ex.Message)
            Me.frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ShowData()


        '=================UNCOMMENT THIS AFTER DEBUGGING========================
        Me.StatProg = StatusProgress.Processing
        Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
        Me.ThreadProgress.Start()
        '====================================================================

        Dim DistributorID As Object = Nothing
        If Not IsNothing(Me.frmParentSPPB) Then
            frmParentSPPB.cmbDistributor.Enabled = False
            If frmParentSPPB.cmbDistributor.Text <> "" And frmParentSPPB.cmbDistributor.SelectedIndex <> -1 Then
                DistributorID = frmParentSPPB.cmbDistributor.Value.ToString()
            End If
        End If

        Dim FromDate As Object = IIf((Me.MasterCategory = "ByPO" Or Me.MasterCategory = "BySPPB" And Me.dtpicFrom.Text <> ""), Convert.ToDateTime(Me.dtpicFrom.Value.ToShortDateString()), Nothing)
        Dim UntilDate As Object = IIf((Me.MasterCategory = "ByPO" Or Me.MasterCategory = "BySPPB" And Me.dtPicUntil.Text <> ""), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), Nothing)
        Dim SPPBNumber As Object = IIf((Me.CustomCategory = "SPPB_NUMBER" And Me.cmbFilterBy.Text = "SPPB_NUMBER"), Me.txtFind.Text.TrimStart().TrimEnd(), Nothing)
        Dim PONUmber As Object = IIf((Me.CustomCategory = "PO_NUMBER" And Me.cmbFilterBy.Text = "PO_NUMBER"), Me.txtFind.Text.TrimStart().TrimEnd(), Nothing)
        Dim GONNUmber As Object = IIf((Me.CustomCategory = "GON_NUMBER" And Me.cmbFilterBy.Text = "GON_NUMBER"), Me.txtFind.Text.TrimStart().TrimEnd(), Nothing)
        Me.DS = Me.clsSPPBGON.getDSSPPBGON(Me.MasterCategory, Me.CustomCategory, DistributorID, SPPBNumber, PONUmber, GONNUmber, FromDate, UntilDate, Me.HasChangedCriteria())

        '=================================================================================
        Me.BindGrid(Me.DS.Tables("SPPB_BRANDPACK_INFO").DefaultView(), Me.grdHeader, False)
        Me.BindGrid(Me.DS.Tables("GON_DETAIL_INFO").DefaultView(), Me.grdDetail, False)
        Me.IsFirsLoad = False
        Me.StatProg = StatusProgress.None
        '=========================NEW UPDATED PROCESS===========================================
    End Sub
    Private Sub InitializeBaseToolTip(ByVal toolTiipText As String)
        With Me.frmParentSPPB
            .baseTooltip.BackColor = frmParentSPPB.BackColor
            .baseTooltip.IsBalloon = True
            .baseTooltip.ShowAlways = False
            .baseTooltip.ToolTipTitle = toolTiipText
        End With
    End Sub

    Private Sub btnFilteDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilteDate.Click
        Try
            'filter by po_date
            Me.Cursor = Cursors.WaitCursor
            'If frmParent.cmbDistributor.SelectedItem Is Nothing Then
            '    MessageBox.Show("Please define distributor", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return
            'End If
            'fill object changed
            If ((Me.btnCatPO.Checked = False) And (Me.btnCatSPPB.Checked = False) And (Me.btnCustom.Checked = False)) Then
                MessageBox.Show("Please define category to show{PODate,SPPB,or Custom}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Me.MObjChanges = New ObjMustReloadDatda()
            With Me.MObjChanges
                If Me.btnCatPO.Checked Then
                    If Me.dtpicFrom.Text = "" Or Me.dtPicUntil.Text = "" Then
                        MessageBox.Show("Please enter valid date time{From & Until}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                    '.MasterCriteria = ObjMustReloadDatda.ByCategory.ByPODate
                    .FromDate = Convert.ToDateTime(Me.dtpicFrom.Value.ToShortDateString())
                    .UntilDate = Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())
                    '.CustomCriteria = ObjMustReloadDatda.ByCustomCategory.None
                    .CustomCategory = ""
                    .MasterCategory = "ByPO"
                    If Not IsNothing(Me.frmParentSPPB) Then
                        If IsNothing(Me.frmParentSPPB.cmbDistributor.Value) Then
                            .DistributorID = ""
                        ElseIf Me.frmParentSPPB.cmbDistributor.Value.ToString() = "" Then
                            .DistributorID = ""
                        Else
                            .DistributorID = Me.frmParentSPPB.cmbDistributor.Value.ToString()
                        End If
                    End If
                    .PONumber = Nothing
                    .SPPBNumber = Nothing
                    .GONNumber = Nothing
                    .CategoryType = "MasterCategory"
                    Me.MasterCategory = "ByPO"
                    Me.CustomCategory = ""
                    Me.CategoryType = "MasterCategory"
                ElseIf Me.btnCatSPPB.Checked Then
                    If Me.dtpicFrom.Text = "" Or Me.dtPicUntil.Text = "" Then
                        MessageBox.Show("Please enter valid date time{From & Until}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                    '.MasterCriteria = ObjMustReloadDatda.ByCategory.BySPPBDate
                    .FromDate = Convert.ToDateTime(Me.dtpicFrom.Value.ToShortDateString())
                    .UntilDate = Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())
                    '.CustomCriteria = ObjMustReloadDatda.ByCustomCategory.None
                    If Not IsNothing(Me.frmParentSPPB) Then
                        If IsNothing(Me.frmParentSPPB.cmbDistributor.Value) Then
                            .DistributorID = ""
                        ElseIf Me.frmParentSPPB.cmbDistributor.Value.ToString() = "" Then
                            .DistributorID = ""
                        Else
                            .DistributorID = Me.frmParentSPPB.cmbDistributor.Value.ToString()
                        End If
                    End If

                    .CustomCategory = ""
                    .MasterCategory = "BySPPB"
                    .PONumber = Nothing
                    .SPPBNumber = Nothing
                    .GONNumber = Nothing
                    .CategoryType = "MasterCategory"
                    Me.MasterCategory = "BySPPB"
                    Me.CustomCategory = ""
                    Me.CategoryType = "MasterCategory"
                ElseIf Me.btnCustom.Checked Then
                    'check custom
                    If Me.cmbFilterBy.SelectedValue Is Nothing Then
                        'MessageBox.Show("Please define custom category to show{Distributor,SPPBNumber,or PONUmber}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.ToolTip1.Show("Please define custom category to show{Distributor,SPPBNumber,or PONUmber}", Me.cmbFilterBy, 2500)
                        Return
                    ElseIf Me.cmbFilterBy.Text = "" Then
                        'MessageBox.Show("Please define custom category to show{Distributor,SPPBNumber,or PONUmber}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.ToolTip1.Show("Please define custom category to show{Distributor,SPPBNumber,or PONUmber}", Me.cmbFilterBy, 2500)
                        Return
                    End If
                    Select Case Me.cmbFilterBy.Text
                        Case "DISTRIBUTOR"
                            If Not IsNothing(Me.frmParentSPPB) Then
                                If Me.frmParentSPPB.cmbDistributor.Value Is Nothing Then
                                    'me.frmParent.baseTooltip.Show(
                                    'MessageBox.Show("Please define custom category to show{Distributor,SPPBNumber,or PONUmber}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Me.frmParentSPPB.baseTooltip.Show("Please define distributor", Me.frmParentSPPB.cmbDistributor, 2500)
                                    Return
                                ElseIf Me.frmParentSPPB.cmbDistributor.Text = "" Then
                                    Me.frmParentSPPB.baseTooltip.Show("Please define distributor", Me.frmParentSPPB.cmbDistributor, 2500)
                                    Return
                                End If
                                '.MasterCriteria = ObjMustReloadDatda.ByCategory.ByCustom
                                .MasterCategory = "ByCustom"
                                .CustomCategory = "DISTRIBUTOR"
                                .DistributorID = Me.frmParentSPPB.cmbDistributor.Value.ToString()
                                .PONumber = Nothing
                                .GONNumber = Nothing
                                .SPPBNumber = Nothing
                                '.CustomCriteria = ObjMustReloadDatda.ByCustomCategory.ByDistributor
                                .FromDate = Nothing
                                .UntilDate = Nothing
                                Me.CustomCategory = "DISTRIBUTOR"
                            Else
                                Return
                            End If

                        Case "PO_NUMBER"
                            If Me.txtFind.Text = "" Then
                                Me.ToolTip1.Show("Please enter PONumber to search for", Me.txtFind, 2500)
                                Return
                            End If
                            .MasterCategory = "ByCustom"
                            .CustomCategory = "PO_NUMBER"
                            .PONumber = Me.txtFind.Text.TrimStart().TrimEnd()
                            .SPPBNumber = Nothing
                            .GONNumber = Nothing
                            .DistributorID = Nothing
                            .FromDate = Nothing
                            .UntilDate = Nothing
                            ''hilangkan distributor
                            If Not IsNothing(Me.frmParentSPPB) Then
                                Me.frmParentSPPB.cmbDistributor.Text = ""
                            End If

                            Me.CustomCategory = "PO_NUMBER"
                        Case "SPPB_NUMBER"
                            If Me.txtFind.Text = "" Then
                                Me.ToolTip1.Show("Please enter SPPB no to search for", Me.txtFind, 2500)
                                Return
                            End If
                            .MasterCategory = "ByCustom"
                            .CustomCategory = "SPPB_NUMBER"
                            .SPPBNumber = Me.txtFind.Text.TrimStart().TrimEnd()
                            .PONumber = Nothing
                            .GONNumber = Nothing
                            .DistributorID = Nothing
                            .FromDate = Nothing
                            .UntilDate = Nothing
                            ''hilangkan distributor
                            If Not IsNothing(Me.frmParentSPPB) Then
                                Me.frmParentSPPB.cmbDistributor.Text = ""
                            End If

                            Me.CustomCategory = "SPPB_NUMBER"
                        Case "GON_NUMBER"
                            If Me.txtFind.Text = "" Then
                                Me.ToolTip1.Show("Please enter SPPB no to search for", Me.txtFind, 2500)
                                Return
                            End If
                            .MasterCategory = "ByCustom"
                            .CustomCategory = "GON_NUMBER"
                            .GONNumber = Me.txtFind.Text.TrimStart().TrimEnd()
                            .PONumber = Nothing
                            .SPPBNumber = Nothing
                            .DistributorID = Nothing
                            .FromDate = Nothing
                            .UntilDate = Nothing
                            ''hilangkan distributor
                            If Not IsNothing(Me.frmParentSPPB) Then
                                Me.frmParentSPPB.cmbDistributor.Text = ""
                            End If
                            Me.CustomCategory = "GON_NUMBER"
                    End Select
                    .CategoryType = "CustomCategory"
                    Me.MasterCategory = "ByCustom"
                    Me.CategoryType = "CustomCategory"
                End If
            End With

            Me.IsLoadding = True
            Me.ShowData()
            If Not IsNothing(Me.frmParentSPPB) Then
                Me.frmParentSPPB.MustReloadData = False
                Me.frmParentSPPB.cmbDistributor.Enabled = (Me.cmbFilterBy.Visible = True And Me.cmbFilterBy.Text = "DISTRIBUTOR")
            ElseIf Not IsNothing(Me.frmParentGrid) Then
                Me.frmParentGrid.MustReloadData = False
            End If
            ''place new criteria becomes original criteria to detect changes
            Me.ObjChanges = Me.MObjChanges
            Me.IsLoadding = False
        Catch ex As Exception
            Me.IsLoadding = False
            Me.StatProg = StatusProgress.None
            If Me.btnCustom.Checked Then : Me.MasterCategory = "ByCustom"
            ElseIf Me.btnCatPO.Checked Then : Me.MasterCategory = "ByPO"
            ElseIf Me.btnCatSPPB.Checked Then : Me.MasterCategory = "BySPPB"
            Else : Me.MasterCategory = ""
            End If
            Me.IsLoadding = False
            If Not IsNothing(Me.frmParentSPPB) Then
                Me.frmParentSPPB.ShowMessageError(ex.Message)
                Me.frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
            ElseIf Not IsNothing(Me.frmParentGrid) Then
                Me.frmParentGrid.ShowMessageError(ex.Message)
                Me.frmParentGrid.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function HasChangedCriteria() As Boolean
        ''check date
        If Me.IsFirsLoad Then : Return True : End If
        If Not IsNothing(Me.frmParentSPPB) Then
            If Me.frmParentSPPB.MustReloadData Then : Return True : End If
        ElseIf Not IsNothing(Me.frmParentGrid) Then
            If Me.frmParentGrid.MustReloadData Then : Return True : End If
        End If
        If Me.MObjChanges.CategoryType <> Me.ObjChanges.CategoryType Then
            Return True
        End If
        If Me.MasterCategory = "ByPO" Or Me.MasterCategory = "BySPPB" Then
            ''check class
            If Me.ObjChanges.MasterCategory <> Me.MasterCategory Then
                Return True
            ElseIf (IsNothing(Me.ObjChanges.FromDate) Or IsNothing(Me.ObjChanges.UntilDate)) Then
                Return True
            ElseIf (Not IsNothing(Me.ObjChanges.FromDate) And Not IsNothing(Me.ObjChanges.UntilDate)) Then
                If Convert.ToDateTime(Me.dtpicFrom.Value.ToShortDateString()) <> Convert.ToDateTime(Me.ObjChanges.FromDate) Then
                    Return True
                ElseIf Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()) <> Convert.ToDateTime(Me.ObjChanges.UntilDate) Then
                    Return True
                End If
            End If
        ElseIf Me.MasterCategory = "ByCustom" Then
            'check field class
            Select Case Me.cmbFilterBy.Text
                Case "DISTRIBUTOR"
                    If Not IsNothing(Me.frmParentSPPB) Then
                        If Me.ObjChanges.DistributorID Is Nothing Then
                            Return True
                        ElseIf Me.ObjChanges.DistributorID.ToString() <> Me.frmParentSPPB.cmbDistributor.Value Then
                            Return True
                        End If
                    Else
                        Return False
                    End If

                Case "PO_NUMBER"
                    If Me.ObjChanges.PONumber Is Nothing Then
                        Return True
                    ElseIf Me.ObjChanges.PONumber.ToString() <> Me.txtFind.Text.TrimStart().TrimEnd() Then
                        Return True
                    End If
                Case "SPPB_NUMBER"
                    If Me.ObjChanges.SPPBNumber Is Nothing Then
                        Return True
                    ElseIf Me.ObjChanges.SPPBNumber.ToString() <> Me.txtFind.Text.TrimStart().TrimEnd() Then
                        Return True
                    End If
                Case "GON_NUMBER"
                    If Me.ObjChanges.GONNumber Is Nothing Then
                        Return True
                    ElseIf Me.ObjChanges.GONNumber.ToString() <> Me.txtFind.Text.TrimStart().TrimEnd() Then
                        Return True
                    End If
            End Select

        End If
        Return False
        'If Me.ObjChanges.MasterCriteria <> Me.MObjChanges.MasterCriteria Then

        'Else

        'End If
        'If Me.ObjChanges.CustomCriteria <> Me.MObjChanges.CustomCriteria Then
        '    Return True
        'ElseIf Me.ObjChanges.DistributorID <> Me.MObjChanges.DistributorID Then
        '    'chek jika Master Category <> ByPO atay BySPPB
        '    If Me.MasterCategory = "ByCustom" Then
        '        Return True
        '    Else

        '    End If
        'ElseIf Me.ObjChanges.FromDate <> Me.MObjChanges.FromDate Then
        '    Return True
        'ElseIf Me.ObjChanges.MasterCriteria <> Me.MObjChanges.MasterCriteria Then
        '    Return True
        'ElseIf Me.ObjChanges.PONumber <> Me.MObjChanges.PONumber Then
        '    Return True
        'ElseIf Me.ObjChanges.SPPBNumber <> Me.MObjChanges.SPPBNumber Then
        '    Return True
        'ElseIf Me.ObjChanges.UntilDate <> Me.MObjChanges.UntilDate Then
        '    Return True
        'End If
        Return False
    End Function
    Private Sub dtPicUntil_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicUntil.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.dtPicUntil.Text = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub dtpicFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpicFrom.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.dtpicFrom.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SPPBManager_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            If TypeOf (Me.frmParentSPPB) Is SPPB Then
                RemoveHandler frmParentSPPB.ComboboxValue_Changed, AddressOf cmbDistributor_ValueChanged
            End If

            Me.clsSPPBGON.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SPPBManager_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
            'initialize data
            With Me.ToolTip1
                .UseFading = True
                .UseAnimation = True
                .ToolTipIcon = ToolTipIcon.Info
                .ShowAlways = False
                .IsBalloon = True
                .BackColor = Me.BackColor
                .ToolTipTitle = "Please fill category/data needed"
            End With
            ''initialize ObjChanges
            With Me.ObjChanges
                .CategoryType = "MasterCategory"
                .MasterCategory = "BySPPB"
                .CustomCategory = ""
                .DistributorID = Nothing
                .FromDate = NufarmBussinesRules.SharedClass.ServerDate
                .UntilDate = NufarmBussinesRules.SharedClass.ServerDate.AddMonths(1)
                Me.dtpicFrom.Value = .FromDate
                Me.dtPicUntil.Value = .UntilDate
                .PONumber = Nothing
                .SPPBNumber = Nothing
                .GONNumber = Nothing
            End With
            Dim IsSySA As Boolean = False
            If Not IsNothing(Me.frmParentGrid) Then
                IsSySA = frmParentGrid.CMain.IsSystemAdministrator
            ElseIf Not IsNothing(Me.frmParentSPPB) Then
                IsSySA = Me.frmParentSPPB.CMain.IsSystemAdministrator
            End If
            If TypeOf (Me.frmParentSPPB) Is SPPB Then
                frmParentSPPB.btnEditSPPB.Enabled = False
                Me.InitializeBaseToolTip("Please define distributor")
                AddHandler frmParentSPPB.ShowSPPBData, AddressOf MustReloadSPPB
                If Not IsSySA Then
                    If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SPPB Then
                        Me.EditGONToolStripMenuItem.Visible = True
                    Else
                        Me.EditGONToolStripMenuItem.Visible = False
                    End If
                End If
            ElseIf Not IsNothing(Me.frmParentGrid) Then
                Me.grdHeader.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                Me.grdDetail.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False

                Me.grdHeader.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                Me.grdHeader.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                AddHandler frmParentGrid.ShowSPPBData, AddressOf MustReloadGrid
                Me.EditGONToolStripMenuItem.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If TypeOf (Me.frmParentSPPB) Is SPPB Then
                Me.ReadAcces() : Me.frmParentSPPB.cmbDistributor.ReadOnly = False
            End If
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Dim itemI As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
        If (Not TypeOf (itemI) Is DevComponents.DotNetBar.ButtonItem) Then
            Return
        End If
        Dim item As DevComponents.DotNetBar.ButtonItem = CType(sender, DevComponents.DotNetBar.ButtonItem)
        Try
            Me.Cursor = Cursors.WaitCursor
            'Dim IsChecked As Boolean = item.Checked
            item.Checked = Not item.Checked
            'clearkan semua item
            Me.IsLoadding = True
            Select Case item.Name
                Case "btnCatSPPB"
                    Me.btnCatPO.Checked = False
                    Me.btnCustom.Checked = False
                    If item.Checked Then
                        Me.ShowMasterCategory()
                        Me.MasterCategory = "BySPPB"
                        Me.CustomCategory = ""
                    Else
                        Me.grdHeader.SetDataBinding(Nothing, "") : Me.grdDetail.SetDataBinding(Nothing, "")
                        Me.IsLoadding = False
                        Me.Cursor = Cursors.Default
                        Return
                    End If
                    Me.CategoryType = "MasterCategory"
                Case "btnCatPO"
                    Me.btnCatSPPB.Checked = False : Me.btnCustom.Checked = False
                    If item.Checked Then
                        Me.ShowMasterCategory()
                        Me.MasterCategory = "ByPO"
                        Me.CustomCategory = ""
                    Else
                        Me.grdHeader.SetDataBinding(Nothing, "") : Me.grdDetail.SetDataBinding(Nothing, "")
                        Me.IsLoadding = False
                        Me.Cursor = Cursors.Default
                        Return
                    End If
                    Me.CategoryType = "MasterCategory"
                Case "btnCustom"
                    Me.btnCatSPPB.Checked = False : Me.btnCatPO.Checked = False
                    If item.Checked Then
                        'check 
                        Me.ShowCustomCategory()
                        Me.MasterCategory = "ByCustom"
                        If Me.cmbFilterBy.Text <> "" Then
                            Me.CustomCategory = Me.cmbFilterBy.Text
                        Else
                            Me.CustomCategory = ""
                        End If
                    Else
                        Me.grdHeader.SetDataBinding(Nothing, "") : Me.grdDetail.SetDataBinding(Nothing, "")
                        Me.IsLoadding = False
                        Me.Cursor = Cursors.Default
                        Return
                    End If
                    Me.CategoryType = "CustomCategory"
            End Select
            Me.IsLoadding = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            item.Checked = Not item.Checked
            If Me.btnCustom.Checked Then
                Me.MasterCategory = "ByCustom"
                If Me.cmbFilterBy.Text <> "" Then
                    Me.CustomCategory = Me.cmbFilterBy.Text
                Else
                    Me.CustomCategory = ""
                End If
                Me.CategoryType = "CustomCategory"
            ElseIf Me.btnCatPO.Checked Then
                Me.MasterCategory = "ByPO"
                Me.CustomCategory = ""
                Me.CategoryType = "MasterCategory"

            ElseIf Me.btnCatSPPB.Checked Then
                Me.MasterCategory = "BySPPB"
                Me.CustomCategory = ""
                Me.CategoryType = "MasterCategory"
            Else
                Me.MasterCategory = ""
                Me.CustomCategory = ""
                Me.CategoryType = ""
            End If
            Me.IsLoadding = False
            If Not IsNothing(Me.frmParentSPPB) Then
                frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_ItemPanel1_ItemClick")
            Else : IsNothing(Me.frmParentGrid)
                frmParentGrid.LogMyEvent(ex.Message, Me.Name + "_ItemPanel1_ItemClick")
            End If
            'Me.frmParent.ShowMessageError(ex.Message)
            'Me.StatProg = StatusProgress.None
        Finally
            If Not IsNothing(Me.frmParentSPPB) Then
                frmParentSPPB.cmbDistributor.Enabled = True : Me.Cursor = Cursors.Default
            End If

        End Try
    End Sub

    Private Sub chkFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilter.CheckedChanged
        If IsNothing(Me.grdHeader.DataSource) Then : Return : End If
        If Me.grdHeader.GetRow().RowType <> Janus.Windows.GridEX.RowType.Record Then : Return : End If
        If Me.grdHeader.RecordCount <= 0 Then : Return : End If
        Me.grdHeader.RemoveFilters()
        Me.IsLoadding = True
        Try
            If Me.chkFilter.Checked Then
                Me.Cursor = Cursors.WaitCursor
                Dim SPPBBrandPackID As Object = Me.grdHeader.GetValue("SPPB_BRANDPACK_ID")
                If IsNothing(SPPBBrandPackID) Then
                ElseIf IsDBNull(SPPBBrandPackID) Then
                Else
                    If Not String.IsNullOrEmpty(SPPBBrandPackID) Then
                        Dim RowFilter As String = "SPPB_BRANDPACK_ID  = '" & SPPBBrandPackID & "'"
                        If Me.DS.Tables.Contains("GON_DETAIL_INFO") Then
                            Dim DV As DataView = Me.DS.Tables("GON_DETAIL_INFO").DefaultView()
                            DV.RowFilter = RowFilter
                            'bind grid
                            Me.BindGrid(DV, Me.grdDetail, False)
                        End If
                    End If
                End If
            Else
                If IsNothing(Me.grdDetail.DataSource) Then : Return : End If
                Me.grdDetail.RemoveFilters()
                CType(Me.grdDetail.DataSource, DataView).RowFilter = ""
            End If
            Me.IsLoadding = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.IsLoadding = False
            If Not IsNothing(Me.frmParentSPPB) Then
                Me.frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_chkFilter_CheckedChanged")
            Else
                Me.frmParentGrid.LogMyEvent(ex.Message, Me.Name + "_chkFilter_CheckedChanged")
            End If
        End Try
    End Sub
    ''' <summary>
    ''' to detect category that data must be reloaded from server
    ''' </summary>
    ''' <remarks> to detect category that data must be reloaded from server</remarks>
    Private Class ObjMustReloadDatda
        Friend CategoryType As Object = Nothing
        Friend MasterCategory As Object = Nothing
        'Friend CustomCriteria As ByCustomCategory = ByCustomCategory.None
        'Friend Enum ByCategory
        '    ByPODate
        '    BySPPBDate
        '    ByCustom
        '    None
        'End Enum
        'Friend Enum ByCustomCategory
        '    ByDistributor
        '    ByPONumber
        '    BySPPBNO
        '    None
        'End Enum
        Friend CustomCategory As Object = Nothing
        Friend DistributorID As Object = Nothing
        Friend PONumber As Object = Nothing
        Friend SPPBNumber As Object = Nothing
        Friend FromDate As Object = Nothing
        Friend UntilDate As Object = Nothing
        Friend GONNumber As Object = Nothing
    End Class
    Private Sub ShowMasterCategory()
        Me.lblFrom.Text = "FROM" : Me.lblUntil.Text = "UNTIL" : Me.lblUntil.Visible = True
        Me.dtpicFrom.Visible = True : Me.dtPicUntil.Visible = True
        'show date from and until'
        Me.dtpicFrom.Visible = True : Me.dtPicUntil.Visible = True
        Me.cmbFilterBy.Visible = False : Me.txtFind.Visible = False
        Me.cmbFilterBy.Text = "" : Me.txtFind.Text = ""
        Me.btnFilteDate.Location = New System.Drawing.Point(698, 4)
    End Sub
    Private Sub ShowCustomCategory()
        Me.dtpicFrom.Visible = False : Me.dtPicUntil.Visible = False
        Me.dtpicFrom.Text = "" : Me.dtPicUntil.Text = ""
        Me.lblFrom.Text = "Filter By" : Me.lblUntil.Visible = False : Me.txtFind.Visible = False
        Me.cmbFilterBy.Visible = True : Me.cmbFilterBy.Text = ""
        'Me.btnFilteDate.Location = New System.Drawing.Point(431, 4)
    End Sub
    Private Sub cmbFilterBy_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFilterBy.SelectedValueChanged
        If Me.IsLoadding Then : Return : End If
        'If Me.cmbFilterBy.Visible Then : Return : End If
        If Me.cmbFilterBy.Text = "" Then : Return : End If
        If Me.cmbFilterBy.SelectedIndex <= -1 Then : Return : End If
        If Not IsNothing(Me.frmParentSPPB) Then
            Select Case Me.cmbFilterBy.Text
                Case "DISTRIBUTOR"
                    Me.lblUntil.Visible = False : Me.txtFind.Visible = False
                    Me.frmParentSPPB.cmbDistributor.Enabled = True
                    Me.txtFind.Text = ""
                    Me.frmParentSPPB.cmbDistributor.Focus()
                    'Me.btnFilteDate.Location = New System.Drawing.Point(698, 4)
                    If Me.frmParentSPPB.cmbDistributor.Value Is Nothing Then
                        Me.frmParentSPPB.baseTooltip.Show("Please define distributo", Me.frmParentSPPB.cmbDistributor, 2500)
                    ElseIf Me.frmParentSPPB.cmbDistributor.Text = "" Then
                        Me.frmParentSPPB.baseTooltip.Show("Please define distributo", Me.frmParentSPPB.cmbDistributor, 2500)
                    End If
                Case "PO_NUMBER"
                    Me.lblUntil.Visible = True : Me.lblUntil.Text = "Search PO"
                    Me.txtFind.Text = "" : Me.txtFind.Visible = True : Me.txtFind.Focus()
                    Me.frmParentSPPB.cmbDistributor.Value = Nothing
                    Me.frmParentSPPB.cmbDistributor.Text = ""
                    Me.frmParentSPPB.cmbDistributor.Enabled = False
                    'Me.btnFilteDate.Location = New System.Drawing.Point(698, 4)
                Case "SPPB_NUMBER"
                    Me.lblUntil.Visible = True
                    Me.lblUntil.Text = "Search SPPB_NO"
                    Me.txtFind.Visible = True : Me.txtFind.Text = "" : Me.txtFind.Focus()
                    Me.frmParentSPPB.cmbDistributor.Value = Nothing
                    Me.frmParentSPPB.cmbDistributor.Text = ""
                    Me.frmParentSPPB.cmbDistributor.Enabled = False
                    'Me.btnFilteDate.Location = New System.Drawing.Point(698, 4)
                Case "GON_NUMBER"
                    Me.lblUntil.Visible = True
                    Me.lblUntil.Text = "Search GON_NO"
                    Me.txtFind.Visible = True : Me.txtFind.Text = "" : Me.txtFind.Focus()
                    Me.frmParentSPPB.cmbDistributor.Value = Nothing
                    Me.frmParentSPPB.cmbDistributor.Text = ""
                    Me.frmParentSPPB.cmbDistributor.Enabled = False
                    'Me.btnFilteDate.Location = New System.Drawing.Point(698, 4)
            End Select
        ElseIf Not IsNothing(Me.frmParentGrid) Then
            Select Case Me.cmbFilterBy.Text
                Case "DISTRIBUTOR"
                    Me.lblUntil.Visible = False : Me.txtFind.Visible = False
                    'Me.frmParentSPPB.cmbDistributor.Enabled = True
                    Me.txtFind.Text = ""
                    'Me.frmParentSPPB.cmbDistributor.Focus()
                    'Me.btnFilteDate.Location = New System.Drawing.Point(698, 4)
                    'If Me.frmParentSPPB.cmbDistributor.Value Is Nothing Then
                    '    Me.frmParentSPPB.baseTooltip.Show("Please define distributo", Me.frmParentSPPB.cmbDistributor, 2500)
                    'ElseIf Me.frmParentSPPB.cmbDistributor.Text = "" Then
                    '    Me.frmParentSPPB.baseTooltip.Show("Please define distributo", Me.frmParentSPPB.cmbDistributor, 2500)
                    'End If
                Case "PO_NUMBER"
                    Me.lblUntil.Visible = True : Me.lblUntil.Text = "Search PO"
                    Me.txtFind.Text = "" : Me.txtFind.Visible = True : Me.txtFind.Focus()
                    'Me.frmParentSPPB.cmbDistributor.Value = Nothing
                    'Me.frmParentSPPB.cmbDistributor.Text = ""
                    'Me.frmParentSPPB.cmbDistributor.Enabled = False
                    'Me.btnFilteDate.Location = New System.Drawing.Point(698, 4)
                Case "SPPB_NUMBER"
                    Me.lblUntil.Visible = True
                    Me.lblUntil.Text = "Search SPPB_NO"
                    Me.txtFind.Visible = True : Me.txtFind.Text = "" : Me.txtFind.Focus()
                    'Me.frmParentGrid.cmbDistributor.Value = Nothing
                    'Me.frmParentGrid.cmbDistributor.Text = ""
                    'Me.frmParentSPPB.cmbDistributor.Enabled = False
                    'Me.btnFilteDate.Location = New System.Drawing.Point(698, 4)
                Case "GON_NUMBER"
                    Me.lblUntil.Visible = True
                    Me.lblUntil.Text = "Search GON_NO"
                    Me.txtFind.Visible = True : Me.txtFind.Text = "" : Me.txtFind.Focus()
            End Select
        End If

        Me.MasterCategory = "ByCustom"
        Me.CustomCategory = Me.cmbFilterBy.Text.TrimStart().TrimEnd()
        'Hide DtPic from and DtPicUntil

    End Sub

    Private Sub grdHeader_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdHeader.Enter
        Me.m_Grid = Me.grdHeader
        If Not IsNothing(Me.frmParentSPPB) Then
            Me.frmParentSPPB.GridEX1 = Me.m_Grid
        ElseIf Not IsNothing(Me.frmParentGrid) Then
            Me.frmParentGrid.m_Grid = Me.m_Grid
        End If
        Me.grdHeader.BorderStyle = Janus.Windows.GridEX.BorderStyle.SunkenLight3D
        Me.grdHeader.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdHeader.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdHeader.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdHeader.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.grdHeader.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText


        Me.grdDetail.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdDetail.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdDetail.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdDetail.BorderStyle = Janus.Windows.GridEX.BorderStyle.None
    End Sub

    Private Sub grdDetail_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDetail.Enter
        Me.m_Grid = Me.grdDetail
        If Not IsNothing(Me.frmParentSPPB) Then
            Me.frmParentSPPB.GridEX1 = Me.m_Grid
            Me.frmParentSPPB.btnEditSPPB.Enabled = False
        ElseIf Not IsNothing(Me.frmParentGrid) Then
            Me.frmParentGrid.m_Grid = Me.m_Grid
        End If
        Me.grdDetail.BorderStyle = Janus.Windows.GridEX.BorderStyle.SunkenLight3D

        Me.grdDetail.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdDetail.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdDetail.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdDetail.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.grdDetail.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        'ME.GridEX1.SelectedFormatStyle.ForeColor = SYSTEM.Drawing.Color.

        Me.grdHeader.BorderStyle = Janus.Windows.GridEX.BorderStyle.None
        Me.grdHeader.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdHeader.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdHeader.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
    End Sub

    Private Sub grdDetail_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdDetail.DeletingRecord
        Try
            If Not IsNothing(Me.frmParentSPPB) Then
                If Me.frmParentSPPB.ShowConfirmedMessage("Are you sure you want to delete data ?" & vbCrLf & "Operation can not be undone!") = DialogResult.No Then
                    e.Cancel = True
                End If
                'delete message
                Me.Cursor = Cursors.WaitCursor
                Dim GHonDetailID As String = Me.grdDetail.GetValue("GON_DETAIL_ID")
                Me.clsSPPBGON.DeleteGON(GHonDetailID)
                Me.frmParentSPPB.MustReloadData = True
                Me.Cursor = Cursors.Default
                Me.frmParentSPPB.ShowMessageInfo("Data deleted succesfully" & vbCrLf & "To reflect any changes, please refresh data")
                e.Cancel = False
            Else
                e.Cancel = False
            End If

        Catch ex As Exception
            e.Cancel = False
            Me.frmParentSPPB.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
            Me.frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_grdDetail_DeletingRecor")
        End Try
    End Sub

    Private Sub SalesReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesReportToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
            Me.ThreadProgress.Start()
            Me.IsLoadding = True
            Me.chkFilter.Checked = False

            Dim tblSales As DataTable = Me.clsSPPBGON.getReportSales(False)
            Me.BindGrid(tblSales.DefaultView(), Me.grdDetail, True)
            Me.IsLoadding = False
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsLoadding = False
            Me.StatProg = StatusProgress.None
            If Me.btnCustom.Checked Then : Me.MasterCategory = "ByCustom"
            ElseIf Me.btnCatPO.Checked Then : Me.MasterCategory = "ByPO"
            ElseIf Me.btnCatSPPB.Checked Then : Me.MasterCategory = "BySPPB"
            Else : Me.MasterCategory = ""
            End If
            Me.IsLoadding = False
            If Not IsNothing(Me.frmParentSPPB) Then
                Me.frmParentSPPB.ShowMessageError(ex.Message)
                Me.frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
            ElseIf Not IsNothing(Me.frmParentGrid) Then
                Me.frmParentGrid.ShowMessageError(ex.Message)
                Me.frmParentGrid.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
            End If
        End Try
    End Sub

    Private Sub SPPBEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPPBEntryToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
            Me.ThreadProgress.Start()

            Me.IsLoadding = True
            Dim tblSales As DataTable = Me.clsSPPBGON.getReportSales(True)
            Me.BindGrid(tblSales.DefaultView(), Me.grdDetail, True)
            Me.IsLoadding = False
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsLoadding = False
            Me.StatProg = StatusProgress.None
            If Me.btnCustom.Checked Then : Me.MasterCategory = "ByCustom"
            ElseIf Me.btnCatPO.Checked Then : Me.MasterCategory = "ByPO"
            ElseIf Me.btnCatSPPB.Checked Then : Me.MasterCategory = "BySPPB"
            Else : Me.MasterCategory = ""
            End If
            Me.IsLoadding = False
            If Not IsNothing(Me.frmParentSPPB) Then
                Me.frmParentSPPB.ShowMessageError(ex.Message)
                Me.frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
            ElseIf Not IsNothing(Me.frmParentGrid) Then
                Me.frmParentGrid.ShowMessageError(ex.Message)
                Me.frmParentGrid.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
            End If
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub EditGONToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditGONToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            frmParentSPPB.EditGON()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsLoadding = False
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
            If Not IsNothing(Me.frmParentSPPB) Then
                Me.frmParentSPPB.ShowMessageError(ex.Message)
                Me.frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
            ElseIf Not IsNothing(Me.frmParentGrid) Then
                Me.frmParentGrid.ShowMessageError(ex.Message)
                Me.frmParentGrid.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
            End If
        End Try
    End Sub

    Private Sub SalesReportSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesReportSummaryToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Me.Cursor = Cursors.WaitCursor
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
            Me.ThreadProgress.Start()
            Me.IsLoadding = True
            Me.chkFilter.Checked = False

            Dim tblSales As DataTable = Me.clsSPPBGON.getWeelySalesReportByClass(False)

            Me.IsLoadding = False

            Dim frmWeelySales As New WeeklySalesReport()
            frmWeelySales.ClsSPPBManager = Me.clsSPPBGON
            frmWeelySales.DateTimePicker1.Value = Me.dtpicFrom.Value
            frmWeelySales.DateTimePicker2.Value = Me.dtPicUntil.Value
            frmWeelySales.GridEX1.SetDataBinding(tblSales.DefaultView, "")
            Me.StatProg = StatusProgress.None
            frmWeelySales.ShowDialog(Me)

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsLoadding = False
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
            If Not IsNothing(Me.frmParentSPPB) Then
                Me.frmParentSPPB.ShowMessageError(ex.Message)
                Me.frmParentSPPB.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
            ElseIf Not IsNothing(Me.frmParentGrid) Then
                Me.frmParentGrid.ShowMessageError(ex.Message)
                Me.frmParentGrid.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
            End If
        End Try
    End Sub
End Class
