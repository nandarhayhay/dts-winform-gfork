Public Class ThirdParty
#Region " Deklarasi "
    Private m_clsThirdParty As NufarmBussinesRules.OrderAcceptance.ThirdParty
    Private ModeSave As Mode
    Private SFM As StateFillingMCB
    Private SFG As StateFillingGrid
    Private DeleteRowGrid As DeleteFrom
    Private PO_REF_NO As String = ""
    Private OA_ID As String = ""
    Friend CMain As Main = Nothing
    'Private SVM As SetValueMode
#End Region

#Region " Enum "
    Private Enum StateFillingMCB
        HasFilled
        Filling
    End Enum
    Private Enum StateFillingGrid
        HasFilled
        Filling
        Disposing
    End Enum
    Private Enum Mode
        Save
        Update
    End Enum
    'Private Enum SetValueMode
    '    Changing
    '    HasChanged
    'End Enum
    Private Enum DeleteFrom
        Keyboard
        ButtonClick
    End Enum
#End Region

#Region " Property "
    Private ReadOnly Property clsThirdParty() As NufarmBussinesRules.OrderAcceptance.ThirdParty
        Get
            If Me.m_clsThirdParty Is Nothing Then
                Me.m_clsThirdParty = New NufarmBussinesRules.OrderAcceptance.ThirdParty()
            End If
            Return Me.m_clsThirdParty
        End Get
    End Property
#End Region

#Region " SUB "

    'Private Sub FillCategoriesValueList()
    '    Dim ColumnBrandPackID As Janus.Windows.GridEX.GridEXColumn = Me.grdGoneSequence.RootTable.Columns("REGIONAL_ID")
    '    ColumnBrandPackID.EditType = Janus.Windows.GridEX.EditType.DropDownList
    '    'Set HasValueList property equal to true in order to be able to use the ValueList property
    '    ColumnBrandPackID.HasValueList = True
    '    'Get the ValueList collection associated to this column
    '    Dim ValueList As Janus.Windows.GridEX.GridEXValueListItemCollection = ColumnBrandPackID.ValueList

    '    ValueList.PopulateValueList(Me.clsSPPBDetail.ViewBrandPack(), "BRANDPACK_ID", "BRANDPACK_NAME")
    '    ColumnBrandPackID.EditTarget = Janus.Windows.GridEX.EditTarget.Text
    '    ColumnBrandPackID.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    '    'Fill the ValueList
    '    'ValueList.PopulateValueList(Ds.Tables("Categories").DefaultView, "CategoryID", "CategoryName")
    'End Sub

    Private Sub ReadAcces()
        If Not NufarmBussinesRules.User.UserLogin.IsAdmin Then

            If Not NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PODOThirdParty Then
                Me.btnAddNew.Visible = False
            Else
                Me.btnAddNew.Visible = True
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PODOThirdParty Then
                Me.btnEditRow.Visible = False
            Else
                Me.btnEditRow.Visible = True
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PODOThirdParty Then
                Me.grdMain.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            Else
                Me.grdMain.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            End If
        End If

    End Sub

    Private Sub FillCategoriesValueList(ByVal SPPB_NO As String)
        Me.clsThirdParty.CreateViewBrandPackSPPB(SPPB_NO)
        Dim ColumnBrandPackID As Janus.Windows.GridEX.GridEXColumn = Me.grdOTP.RootTable.Columns("BRANDPACK_ID")
        ColumnBrandPackID.EditType = Janus.Windows.GridEX.EditType.DropDownList
        'Set HasValueList property equal to true in order to be able to use the ValueList property
        ColumnBrandPackID.HasValueList = True
        'Get the ValueList collection associated to this column
        Dim ValueList As Janus.Windows.GridEX.GridEXValueListItemCollection = ColumnBrandPackID.ValueList

        ValueList.PopulateValueList(Me.clsThirdParty.ViewBrandPackSPPB(), "BRANDPACK_ID", "BRANDPACK_NAME")
        ColumnBrandPackID.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        ColumnBrandPackID.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text

        Dim ColumnBrandPackID_1 As Janus.Windows.GridEX.GridEXColumn = Me.grdDO.RootTable.Columns("BRANDPACK_ID")
        ColumnBrandPackID_1.EditType = Janus.Windows.GridEX.EditType.DropDownList
        'Set HasValueList property equal to true in order to be able to use the ValueList property
        ColumnBrandPackID_1.HasValueList = True
        'Get the ValueList collection associated to this column
        Dim ValueList_1 As Janus.Windows.GridEX.GridEXValueListItemCollection = ColumnBrandPackID_1.ValueList

        ValueList_1.PopulateValueList(Me.clsThirdParty.ViewBrandPackSPPB(), "BRANDPACK_ID", "BRANDPACK_NAME")
        ColumnBrandPackID_1.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        ColumnBrandPackID_1.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text

    End Sub

    Private Sub GetDataEntry(ByVal OTP_NO As String)
        Me.clsThirdParty.GetData(OTP_NO)
        Me.Bindgrid(Me.grdOTP, Me.clsThirdParty.ViewOTP())
        Me.Bindgrid(Me.grdDO, Me.clsThirdParty.ViewDOTP())
    End Sub

    Friend Sub InitializeData()
        Me.clsThirdParty.CreateViewDistributorDOTP()
        Me.BindMultiColumnCombo(Me.cmbDistributor, Me.clsThirdParty.ViewDistributor())

    End Sub

    Private Sub Bindgrid(ByVal grd As Janus.Windows.GridEX.GridEX, ByVal dtview As DataView)
        Me.SFG = StateFillingGrid.Filling
        If grd Is Me.grdDO Then
            Me.grdDO.SetDataBinding(dtview, "")
        ElseIf grd Is Me.grdMain Then
            Me.grdMain.SetDataBinding(dtview, "")
        ElseIf grd Is Me.grdOTP Then
            Me.grdOTP.SetDataBinding(dtview, "")
        End If
        Me.SFG = StateFillingGrid.HasFilled
    End Sub

    Private Sub BindMultiColumnCombo(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal dtview As DataView)
        Me.SFM = StateFillingMCB.Filling
        mcb.SetDataBinding(dtview, "")
        'Me.mcbSPPB.Value = Nothing
        Me.SFM = StateFillingMCB.HasFilled
    End Sub

    Private Overloads Sub ClearControl()
        Me.txtDO_TP_NO.Text = ""
        Me.txtOTP_NO.Text = ""
        Me.mcbSPPB.Value = Nothing
        Me.dtpOTPDate.Text = ""
        Me.dtPicDOThirdParty.Text = ""
        Me.Bindgrid(Me.grdDO, Nothing)
        Me.Bindgrid(Me.grdOTP, Nothing)
    End Sub

#End Region

#Region " Function "

    Private Function IsvalidOTP() As Boolean
        If Me.txtOTP_NO.Text = "" Then
            Me.baseTooltip.Show("Please suply this Value No.", Me.txtOTP_NO, 3000)
            Me.txtDO_TP_NO.Focus()
            Return False
        ElseIf Me.mcbSPPB.SelectedItem Is Nothing Then
            Me.baseTooltip.Show("Please define SPPB NO", Me.mcbSPPB, 3000)
            Me.dtpOTPDate.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function IsValidDO() As Boolean
        If Me.txtDO_TP_NO.Text = "" Then
            Me.baseTooltip.Show("Please suply this Value No.", Me.txtDO_TP_NO, 3000)
            Me.txtDO_TP_NO.Focus()
            Return False
        ElseIf Me.mcbSPPB.SelectedItem Is Nothing Then
            Me.baseTooltip.Show("Please define SPPB NO", Me.mcbSPPB, 3000)
            Me.dtPicDOThirdParty.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub CheckData()
        'Dim isReadOnly As Boolean = Me.mcbSPPB.ReadOnly
        If Not IsNothing(Me.clsThirdParty.GetDataSet()) Then
            If Me.clsThirdParty.GetDataSet().HasChanges() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    If Me.IsvalidOTP() = False Then
                        Me.ShowMessageInfo("Couldn't save data due to incomplete reference")
                        Me.clsThirdParty.GetDataSet.RejectChanges()
                        Me.clsThirdParty.Dispose(True)
                        Return
                    ElseIf Me.IsValidDO = False Then
                        'If isReadOnly Then
                        '    Me.mcbSPPB.ReadOnly = False
                        '    Me.mcbSPPB.Select()
                        'End If
                        Me.clsThirdParty.SPPB_NO = Me.mcbSPPB.Value.ToString() ' Me.mcbSPPB.DropDownList.GetValue("PO_REF_NO").ToString()
                        Me.clsThirdParty.OTP_NO = Me.txtOTP_NO.Text.Trim()
                        Me.clsThirdParty.OTP_DATE = CDate(Me.dtpOTPDate.Value.ToShortDateString())
                        Me.clsThirdParty.DO_TP_NO = DBNull.Value
                        Me.clsThirdParty.DO_TP_DATE = DBNull.Value
                        Select Case Me.ModeSave
                            Case Mode.Save
                                Me.clsThirdParty.SaveData("Insert", Me.clsThirdParty.GetDataSet().GetChanges(), True)
                            Case Mode.Update
                                Me.clsThirdParty.SaveData("Update", Me.clsThirdParty.GetDataSet().GetChanges(), True)
                        End Select
                    ElseIf Me.IsValidDO = True Then
                        'If isReadOnly Then
                        '    Me.mcbSPPB.ReadOnly = False
                        '    Me.mcbSPPB.Select()
                        'End If
                        Me.clsThirdParty.SPPB_NO = Me.mcbSPPB.Value.ToString()  'PO_REF_NO = Me.mcbSPPB.DropDownList.GetValue("PO_REF_NO").ToString()
                        Me.clsThirdParty.OTP_NO = Me.txtOTP_NO.Text.Trim()
                        Me.clsThirdParty.OTP_DATE = CDate(Me.dtpOTPDate.Value.ToShortDateString())
                        Me.clsThirdParty.DO_TP_NO = Me.txtDO_TP_NO.Text.Trim()
                        Me.clsThirdParty.DO_TP_DATE = CDate(Me.dtPicDOThirdParty.Value.ToShortDateString())
                        Select Case Me.ModeSave
                            Case Mode.Save
                                Me.clsThirdParty.SaveData("Insert", Me.clsThirdParty.GetDataSet().GetChanges(), True)
                            Case Mode.Update
                                Me.clsThirdParty.SaveData("Update", Me.clsThirdParty.GetDataSet().GetChanges(), True)
                        End Select
                    End If
                    Me.clsThirdParty.GetDataSet().AcceptChanges()
                Else
                    Me.clsThirdParty.GetDataSet().RejectChanges()
                End If
            End If
        End If
        'Me.mcbSPPB.ReadOnly = isReadOnly
    End Sub

#End Region

#Region " Handling Event  "

#Region " Event Form "

    Private Sub ThirdParty_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.grdDO.DataSource) Then
                Me.SFG = StateFillingGrid.Disposing
                Me.CheckData()
                Me.clsThirdParty.Dispose(True)
            End If
        Catch EX As DBConcurrencyException
            Me.ShowMessageInfo(Me.MessageSavingFailed & "Due to uncommited changes")
            Me.LogMyEvent(EX.Message, Me.Name + "_ThirdParty_FormClosing")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub ThirdParty_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            'Me.ExpandableSplitter1.Expanded = False
            Me.grpEntry.Enabled = False
            Me.baseTooltip.SetToolTip(Me.txtDO_TP_NO, "To commit Editing" & vbCrLf & "Press enter when finish editing")
            Me.baseTooltip.SetToolTip(Me.txtOTP_NO, "To Commit editing" & vbCrLf & "Press enter when finish editing")

        Catch ex As Exception

        Finally
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mcbSPPB_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbSPPB.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            'get information from that sppb
            If Me.SFM = StateFillingMCB.Filling Then
                Return
            End If
            If Me.mcbSPPB.Value Is Nothing Then
                Return
            End If
            If Me.mcbSPPB.SelectedItem Is Nothing Then
                Return
            End If
            If Not IsNothing(Me.clsThirdParty.GetDataSet()) Then
                If Me.clsThirdParty.GetDataSet.HasChanges() Then
                    If Me.ShowConfirmedMessage("Data(s) you've just edited has changed" & vbCrLf & "Discard all changes ?") = Windows.Forms.DialogResult.Yes Then
                        Me.clsThirdParty.GetDataSet().RejectChanges()
                    Else
                        Me.baseTooltip.Show("Please Press this button/press enter", Me.btnSave, 3000)
                        Me.btnSave.Focus()
                        Return
                    End If
                End If
            End If
            Me.clsThirdParty.GetRowDataBySPPB_NO(Me.mcbSPPB.Value.ToString())
            Me.txtOTP_NO.Text = Me.clsThirdParty.OTP_NO.ToString()
            Dim dtview As DataView = CType(Me.mcbSPPB.DataSource, DataView)
            Dim index As Integer = dtview.Find(Me.mcbSPPB.Value.ToString())
            Me.dtpOTPDate.MinDate = dtview(index)("SPPB_DATE")
            If Me.clsThirdParty.OTP_DATE Is DBNull.Value Then
                Me.dtpOTPDate.Text = ""
            Else
                Me.dtpOTPDate.Text = CDate(Me.clsThirdParty.OTP_DATE).ToLongDateString()
            End If
            Me.txtDO_TP_NO.Text = Me.clsThirdParty.DO_TP_NO.ToString()
            If Me.dtpOTPDate.Text <> "" Then
                Me.dtPicDOThirdParty.MinDate = CDate(Me.dtpOTPDate.Value.ToLongDateString())
            Else
                Me.dtPicDOThirdParty.MinDate = Me.dtpOTPDate.MinDate()
            End If
            If Me.clsThirdParty.DO_TP_DATE Is DBNull.Value Then
                Me.dtPicDOThirdParty.Text = ""
            Else
                Me.dtPicDOThirdParty.Text = CDate(Me.clsThirdParty.DO_TP_DATE).ToShortDateString()
            End If
            If Me.ModeSave = Mode.Save Then
                Me.grdDO.Enabled = False
                Me.grdOTP.Enabled = False
                Me.txtOTP_NO.ReadOnly = False
                Me.txtDO_TP_NO.ReadOnly = False
            Else
                Me.txtOTP_NO.ReadOnly = True
                Me.txtOTP_NO_KeyPress(Me.txtOTP_NO, New KeyPressEventArgs(Chr(13)))
                If Me.txtDO_TP_NO.Text <> "" Then
                    Me.txtDO_TP_NO.ReadOnly = True
                    Me.grdDO.Enabled = True
                Else
                    Me.txtDO_TP_NO.ReadOnly = False
                    Me.grdDO.Enabled = False
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbSPPB_ValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnRenameColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.grdMain
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar1, True)
                Case "btnShowFieldChooser"
                    Me.grdMain.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.grdMain
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.grdMain
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    'Me.PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                        Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    End If
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                Case "btnPageSettings"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
                Case "btnCustomFilter"
                    Me.FilterEditor1.SourceControl = Me.grdMain
                    Me.grdMain.RemoveFilters()
                    Me.FilterEditor1.Visible = True
                    Me.grdMain.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.XpCaptionPane1.Enabled = False
                    Me.dtpicFrom.Text = ""
                    Me.dtPicUntil.Text = ""
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.grdMain.RemoveFilters()
                    Me.grdMain.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.XpCaptionPane1.Enabled = True
                Case "btnAddNew"
                    If Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please difine distributor")
                        Me.grpEntry.Enabled = False
                        Return
                    End If
                    Me.btnAddNew.Checked = Not Me.btnAddNew.Checked
                    If Me.btnAddNew.Checked Then
                        Me.CheckData()
                        Me.clsThirdParty.CreateViewSPPB_NO(Me.cmbDistributor.Value.ToString(), True)
                        Me.BindMultiColumnCombo(Me.mcbSPPB, Me.clsThirdParty.ViewSPPB_NO)
                        Me.ClearControl()
                        Me.ModeSave = Mode.Save
                        Me.ExpandableSplitter1.Expanded = True
                        Me.grpEntry.Enabled = True
                        'Me.mcbSPPB.ReadOnly = False
                    Else
                        Me.CheckData()
                        Me.ExpandableSplitter1.Expanded = False
                        Me.ClearControl()
                        Me.grpEntry.Enabled = False
                    End If
                    'bind mcbsppb dengan sppb_no yang belum ada di register
                    'check apakah ada perubahan data
                Case "btnExport"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.grdMain
                        Me.GridEXExporter1.SheetName = "PO_DO_ThirdParty"
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnRefresh"
                    'Me.CheckData()
                    Me.btnAddNew.Checked = False
                    Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
                Case "btnEditRow"
                    If Not Me.grdMain.GetRow.RowType() = Janus.Windows.GridEX.RowType.Record Then
                        Me.grpEntry.Enabled = False
                        Return
                    End If
                    'bind mcbsppb
                    If Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please define distributor")
                        Me.grpEntry.Enabled = False
                        Return
                    End If
                    Me.ModeSave = Mode.Update
                    'Me.CheckData()
                    Me.clsThirdParty.CreateViewSPPB_NO(Me.cmbDistributor.Value.ToString(), True)
                    Me.BindMultiColumnCombo(Me.mcbSPPB, Me.clsThirdParty.ViewSPPB_NO)
                    Me.mcbSPPB.Value = Me.grdMain.GetValue("SPPB_NO")
                    Me.mcbSPPB.DropDownList().Select()
                    Me.mcbSPPB.DropDownList().SelectOnExpand() = True
                    Me.mcbSPPB.DroppedDown = True
                    Me.ExpandableSplitter1.Expanded = True
                    Me.grpEntry.Enabled = True
                    Me.ModeSave = Mode.Update
                    Me.btnAddNew.Checked = False
                    Me.mcbSPPB.DroppedDown = False
            End Select
        Catch EX As DBConcurrencyException
            Me.clsThirdParty.GetDataSet().AcceptChanges()
            Me.LogMyEvent(EX.Message, Me.Name + "_Bar2_ItemClick")
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFM = StateFillingMCB.Filling Then
                Return
            End If
            If Me.cmbDistributor.SelectedItem Is Nothing Then
                Return
            End If
            If Me.cmbDistributor.Value Is Nothing Then
                Return
            End If
            'Me.CheckData()
            Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
            Me.Bindgrid(Me.grdMain, Me.clsThirdParty.ViewThirdParty())
            Me.ExpandableSplitter1.Expanded = False
            Me.ClearControl()
        Catch EX As DBConcurrencyException
            Me.clsThirdParty.GetDataSet().AcceptChanges()
            Me.LogMyEvent(EX.Message, Me.Name + "_mcbSPPB_ValueChanged")
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbSPPB_ValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            CType(Me.cmbDistributor.DataSource, DataView).RowFilter = "DISTRIBUTOR_NAME LIKE '%" & Me.cmbDistributor.Text & "%'"
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) Found")
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)

            Me.LogMyEvent(ex.Message, Me.Name + "_btnApply_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.IsvalidOTP() = False Then
                Return
            End If
            If Not IsNothing(Me.clsThirdParty.GetDataSet()) Then
                Dim IsChangedData As Boolean = False
                If Me.clsThirdParty.GetDataSet.HasChanges() Then
                    IsChangedData = True
                End If
                With Me.clsThirdParty
                    .SPPB_NO = Me.mcbSPPB.Value.ToString()
                    .OTP_NO = Me.txtOTP_NO.Text.Trim()
                    .OTP_DATE = CDate(Me.dtpOTPDate.Value.ToShortDateString())
                    .DO_TP_NO = Me.txtDO_TP_NO.Text.Trim()
                    If Me.dtPicDOThirdParty.Text <> "" Then
                        .DO_TP_DATE = CDate(Me.dtPicDOThirdParty.Value.ToShortDateString())
                    Else
                        .DO_TP_DATE = DBNull.Value
                    End If
                    Select Case Me.ModeSave
                        Case Mode.Save
                            If IsChangedData Then
                                Me.clsThirdParty.SaveData("Insert", Me.clsThirdParty.GetDataSet().GetChanges(), True)
                            Else
                                Me.ShowMessageInfo("No Changes should be saved")
                                Return
                            End If

                        Case Mode.Update
                            If IsChangedData Then
                                Me.clsThirdParty.SaveData("Update", Me.clsThirdParty.GetDataSet().GetChanges(), True)
                            Else
                                Me.clsThirdParty.SaveData("Update", Me.clsThirdParty.GetDataSet(), False)
                            End If
                    End Select
                End With
                'Me.ShowMessageInfo(Me.MessageSavingSucces)
                Me.clsThirdParty.GetDataSet.AcceptChanges()
                Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
                Me.ExpandableSplitter1.Expanded = False
                Me.ClearControl()
            End If
        Catch EX As DBConcurrencyException
            Me.clsThirdParty.GetDataSet.AcceptChanges()
            Me.LogMyEvent(EX.Message, Me.Name + "_btnSave_Click")
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
            Me.ExpandableSplitter1.Expanded = False
            If Not IsNothing(Me.clsThirdParty.GetDataSet()) Then
                If Me.clsThirdParty.GetDataSet().HasChanges() Then
                    Me.clsThirdParty.GetDataSet.RejectChanges()
                End If
            End If
            Me.ClearControl()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnCLose_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdMain_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdMain.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes Then
                Me.clsThirdParty.Delete(Me.grdMain.GetValue("OA_BRANDPACK_ID").ToString())
                Me.ShowMessageInfo(Me.MessageSuccesDelete)
                If (Me.dtpicFrom.Text <> "") And (Me.dtPicUntil.Text <> "") Then
                    Me.clsThirdParty.CreateViewThirdParty(Me.cmbDistributor.Value.ToString(), CDate(Me.dtpicFrom.Value.ToShortDateString()), CDate(Me.dtPicUntil.Value.ToShortDateString()))
                ElseIf (Me.dtpicFrom.Text <> "") And (Me.dtPicUntil.Text = "") Then
                    Me.clsThirdParty.CreateViewThirdParty(Me.cmbDistributor.Value.ToString(), CDate(Me.dtpicFrom.Value.ToShortDateString()), DBNull.Value)
                ElseIf (Me.dtpicFrom.Text = "") And (Me.dtPicUntil.Text <> "") Then
                    Me.clsThirdParty.CreateViewThirdParty(Me.cmbDistributor.Value.ToString(), DBNull.Value, CDate(Me.dtPicUntil.Value.ToShortDateString()))
                Else
                    Me.clsThirdParty.CreateViewThirdParty(Me.cmbDistributor.Value.ToString(), DBNull.Value, DBNull.Value)
                End If
                Me.Bindgrid(Me.grdMain, Me.clsThirdParty.ViewThirdParty())
                Me.ExpandableSplitter1.Expanded = False
                Me.ClearControl()
                Me.SFG = StateFillingGrid.Filling
                Me.ModeSave = Mode.Save
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdMain_DeletingRecord")
            e.Cancel = True
        Finally
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdMain_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMain.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If (Me.SFG = StateFillingGrid.Filling) Or (Me.SFG = StateFillingGrid.Disposing) Then
                Return
            End If
            If Not Me.grdMain.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.btnEditRow.Enabled = False
                Me.grpEntry.Enabled = False
                Me.ExpandableSplitter1.Expanded = False
                Me.btnAddNew.Checked = False '.Enabled = False
                Return
            Else
                Me.btnEditRow.Enabled = True
                'Me.ExpandableSplitter1.Expanded = True
            End If
            If (IsNothing(Me.grdMain.GetValue("OTP_NO"))) Or (IsDBNull(Me.grdMain.GetValue("OTP_NO"))) Then
                Me.btnEditRow.Enabled = False
            End If
        Catch ex As Exception
            'Me.ShowMessageInfo(ex.Message)
            'Me.LogMyEvent(ex.Message, Me.Name + "_grdMain_CurrentCellChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdOTP_AddingRecord(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdOTP.AddingRecord
        Try
            'check isvalidPO
            Me.Cursor = Cursors.WaitCursor
            If Me.IsvalidOTP() = False Then
                Me.grdOTP.CancelCurrentEdit()
                Return
            End If
            'CHECK BRANDPACK_ID 
            If (Me.grdOTP.GetValue("BRANDPACK_ID") Is Nothing) Or (Me.grdOTP.GetValue("OA_BRANDPACK_ID") Is DBNull.Value) Then
                Me.ShowMessageInfo("BRANDPACK IS NULL")
                Me.grdOTP.CancelCurrentEdit()
                Me.grdOTP.MoveToNewRecord()
                Return
            ElseIf (IsNothing(Me.grdOTP.GetValue("OTP_QTY"))) Or (IsDBNull(Me.grdOTP.GetValue("OTP_QTY"))) Or (Me.grdOTP.GetValue("OTP_QTY") <= 0) Then
                Me.ShowMessageInfo("Quantity is invalid" & vbCrLf & "Quantity can not be null/ zero while adding")
                Me.grdOTP.CancelCurrentEdit()
                Me.grdOTP.MoveToNewRecord()
                Return
            ElseIf Me.grdOTP.GetValue("OTP_BRANDPACK") Is DBNull.Value Then
                Me.ShowMessageInfo("BRANDPACK IS NULL")
                Me.grdOTP.CancelCurrentEdit()
                Me.grdOTP.MoveToNewRecord()
                Return
            End If
            'check id in dataview
            'Dim OA_ID As String = Me.mcbSPPB.DropDownList.GetValue("OA_REF_NO").ToString()
            'Dim PO_BRANDPACK_ID As String = Me.mcbSPPB.DropDownList.GetValue("PO_REF_NO").ToString()
            Dim OTP_BRANDPACK As Object = Me.grdOTP.GetValue("OTP_BRANDPACK") 'CStr(Me.grdOTP.GetValue("OA_BRANDPACK_ID")) + "" + Me.txtOTP_NO.Text.Trim()
            If Me.clsThirdParty.ViewOTP().Find(OTP_BRANDPACK) <> -1 Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
                Me.grdOTP.CancelCurrentEdit()
                Me.grdOTP.MoveToNewRecord()
                Return
            End If
            If Me.clsThirdParty.EXISTS_OTP_BRANDPACK(OTP_BRANDPACK) Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
                Me.grdOTP.CancelCurrentEdit()
                Me.grdOTP.MoveToNewRecord()
                Return
            End If

            Dim OA_BRANDPACK_ID As String = Me.grdOTP.GetValue("OA_BRANDPACK_ID").ToString()
            Dim SPPB_BRANDPACK_ID As String = Me.mcbSPPB.Value.ToString() + "" + OA_BRANDPACK_ID
            Dim SPPB_QTY As Decimal = Me.clsThirdParty.getSPPB_QTY(SPPB_BRANDPACK_ID)
            If SPPB_QTY < 0 Then
                Me.ShowMessageInfo("OA_BRANDPACK IS 0 ")
                Me.grdOTP.CancelCurrentEdit()
                Return
            End If
            If CDec(Me.grdOTP.GetValue("OTP_QTY")) > SPPB_QTY Then
                Me.ShowMessageInfo("value is more than it should be")
                Me.grdOTP.CancelCurrentEdit()
                Return
            End If
            'INSERT CREATE_DATE
            'Me.SVM = SetValueMode.Changing
            Dim CREATE_BY As String = NufarmBussinesRules.User.UserLogin.UserName
            Dim CREATE_DATE As Date = CDate(NufarmBussinesRules.SharedClass.ServerDate().ToShortDateString())
            Me.grdOTP.SetValue("CREATE_BY", CREATE_BY)
            Me.grdOTP.SetValue("CREATE_DATE", CREATE_DATE)
            'Me.SVM = SetValueMode.HasChanged
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdOTP_AddingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdOTP_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdOTP.CellUpdated
        Try
            'CHECK VALIDITY
            Me.Cursor = Cursors.WaitCursor
            If Me.IsvalidOTP() = False Then
                Return
            End If
            'If Me.SVM = SetValueMode.Changing Then
            '    Return
            'End If
            If (Me.SFG = StateFillingGrid.Filling) Or (Me.SFG = StateFillingGrid.Disposing) Then
                Return
            End If
            'Dim IsEnabled As Boolean = Me.mcbSPPB.ReadOnly
            'If IsEnabled Then
            '    Me.mcbSPPB.ReadOnly = False
            'End If
            PO_REF_NO = "" : OA_ID = ""
            Me.clsThirdParty.getPODescription(Me.mcbSPPB.Text, OA_ID, PO_REF_NO)
            If OA_ID = "" Or PO_REF_NO = "" Then
                Me.ShowMessageInfo("PO_REF_NO refered to SPPB is null " & vbCrLf & "Perhaps has been deleted by user") : Return
            End If
            If Me.grdOTP.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                'set valuenya
                If e.Column.Key = "BRANDPACK_ID" Then
                    If (Not IsNothing(Me.grdOTP.GetValue("BRANDPACK_ID"))) And (Not IsDBNull(Me.grdOTP.GetValue("BRANDPACK_ID"))) Then
                        Dim PO_BRANDPACK_ID As String = PO_REF_NO & "" & Me.grdOTP.GetValue("BRANDPACK_ID").ToString()
                        Dim OA_BRANDPACK_ID As String = OA_ID & "" & PO_BRANDPACK_ID
                        Dim OTP_BRANDPACK As String = Me.txtOTP_NO.Text.Trim() & OA_BRANDPACK_ID
                        If Me.clsThirdParty.ViewOTP().Find(OTP_BRANDPACK) <> -1 Then
                            Me.ShowMessageInfo(Me.MessageDataHasExisted)
                            Me.grdOTP.CancelCurrentEdit()
                            Me.grdOTP.MoveToNewRecord()
                            Return
                        End If
                        If Me.clsThirdParty.EXISTS_OTP_BRANDPACK(OTP_BRANDPACK) Then
                            Me.ShowMessageInfo(Me.MessageDataHasExisted)
                            Me.grdOTP.CancelCurrentEdit()
                            Me.grdOTP.MoveToNewRecord()
                            Return
                        End If

                        'Me.SVM = SetValueMode.Changing
                        Me.grdOTP.SetValue("OTP_BRANDPACK", OTP_BRANDPACK)
                        Me.grdOTP.SetValue("OTP_NO", Me.txtOTP_NO.Text.Trim())
                        Me.grdOTP.SetValue("OA_BRANDPACK_ID", OA_BRANDPACK_ID)
                        Dim SPPB_BRANDPACK_ID As String = Me.mcbSPPB.Text & "" & OA_BRANDPACK_ID
                        'ambil sppb_qty
                        Dim SPPB_QTY As Decimal = Me.clsThirdParty.getSPPB_QTY(SPPB_BRANDPACK_ID)
                        Me.grdOTP.SetValue("OTP_QTY", SPPB_QTY)
                        'Me.SVM = SetValueMode.HasChanged
                    End If
                ElseIf e.Column.Key = "OTP_QTY" Then
                    If (IsNothing(Me.grdOTP.GetValue("BRANDPACK_ID"))) Or (IsDBNull(Me.grdOTP.GetValue("BRANDPACK_ID"))) Then
                        Me.ShowMessageInfo("Please define brandpack first")
                        Me.grdOTP.CancelCurrentEdit()
                        Return
                    End If
                    Dim PO_BRANDPACK_ID As String = PO_REF_NO & "" & Me.grdOTP.GetValue("BRANDPACK_ID").ToString()
                    Dim OA_BRANDPACK_ID As String = OA_ID & "" & PO_BRANDPACK_ID
                    Dim SPPB_BRANDPACK_ID As String = Me.mcbSPPB.Text & "" & OA_BRANDPACK_ID


                    'check qty brandpack
                    Dim SPPB_QTY As Decimal = Me.clsThirdParty.getSPPB_QTY(SPPB_BRANDPACK_ID)
                    If SPPB_QTY < 0 Then
                        Me.ShowMessageInfo("OA_BRANDPACK IS 0 ")
                        Me.grdOTP.CancelCurrentEdit()
                        Me.grdOTP.MoveToNewRecord()
                        Return
                    End If
                    If CDec(Me.grdOTP.GetValue("OTP_QTY")) > SPPB_QTY Then
                        Me.ShowMessageInfo("value is more than it should be")
                        Me.grdOTP.CancelCurrentEdit()
                        Me.grdOTP.MoveToNewRecord()
                        Return
                    End If
                End If
            ElseIf Me.grdOTP.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                'CHECK OTP_BRANDPACK
                If e.Column.Key = "BRANDPACK_ID" Then
                    'Dim PO_BRANDPACK_ID As String = Me.mcbSPPB.DropDownList.GetValue("PO_REF_NO").ToString()
                    'Dim OA_BRANDPACK_ID As String = OA_ID & PO_BRANDPACK_ID
                    'Dim OTP_BRANDPACK As String = Me.txtOTP_NO.Text.Trim() & OA_BRANDPACK_ID
                    If (Not IsNothing(Me.grdOTP.GetValue("BRANDPACK_ID"))) And (Not IsDBNull(Me.grdOTP.GetValue("BRANDPACK_ID"))) Then

                        Dim PO_BRANDPACK_ID As String = PO_REF_NO & "" & Me.grdOTP.GetValue("BRANDPACK_ID").ToString()
                        Dim OA_BRANDPACK_ID As String = OA_ID & "" & PO_BRANDPACK_ID
                        Dim OTP_BRANDPACK As String = Me.txtOTP_NO.Text.Trim() & OA_BRANDPACK_ID
                        If Me.clsThirdParty.ViewOTP().Find(OTP_BRANDPACK) <> -1 Then
                            Me.ShowMessageInfo(Me.MessageDataHasExisted)
                            Me.grdOTP.CancelCurrentEdit()
                            'Me.grdOTP.MoveToNewRecord()
                            Return
                        End If
                        Select Case Me.ModeSave
                            Case Mode.Save
                                If Me.clsThirdParty.EXISTS_OTP_BRANDPACK(OTP_BRANDPACK) Then
                                    Me.ShowMessageInfo(Me.MessageDataHasExisted)
                                    Me.grdOTP.CancelCurrentEdit()
                                    'Me.grdOTP.MoveToNewRecord()
                                    Return
                                End If
                            Case Mode.Update
                                Me.ShowMessageInfo("Can not set BrandPack while Update mode")
                                Me.grdOTP.CancelCurrentEdit()
                                Return
                        End Select
                        'Me.SVM = SetValueMode.Changing
                        Me.grdOTP.SetValue("OTP_BRANDPACK", OTP_BRANDPACK)
                        Me.grdOTP.SetValue("OTP_NO", Me.txtOTP_NO.Text.Trim())
                        Me.grdOTP.SetValue("OA_BRANDPACK_ID", OA_BRANDPACK_ID)
                        Dim SPPB_BRANDPACK_ID As String = Me.mcbSPPB.Text & "" & OA_BRANDPACK_ID
                        'ambil sppb_qty
                        Dim SPPB_QTY As Decimal = Me.clsThirdParty.getSPPB_QTY(SPPB_BRANDPACK_ID)
                        Me.grdOTP.SetValue("OTP_QTY", SPPB_QTY)
                        'Me.SVM = SetValueMode.HasChanged
                    Else
                        Me.ShowMessageInfo("can not set null value for brandpack")
                        Me.grdOTP.CancelCurrentEdit()
                        Return
                    End If
                ElseIf e.Column.Key = "OTP_QTY" Then
                    If (IsNothing(Me.grdOTP.GetValue("BRANDPACK_ID"))) Or (IsDBNull(Me.grdOTP.GetValue("BRANDPACK_ID"))) Then
                        Me.ShowMessageInfo("Please define brandpack first")
                        Me.grdOTP.CancelCurrentEdit()
                        Return
                    ElseIf IsDBNull(Me.grdOTP.GetValue("OTP_QTY")) Then
                        Me.ShowMessageInfo("can not set null value for Quantity")
                        Me.grdOTP.CancelCurrentEdit()
                        Return
                    End If
                    Dim PO_BRANDPACK_ID As String = PO_REF_NO & "" & Me.grdOTP.GetValue("BRANDPACK_ID").ToString()
                    Dim OA_BRANDPACK_ID As String = OA_ID & "" & PO_BRANDPACK_ID

                    If Not IsNothing(Me.grdDO.DataSource) Then
                        If CType(Me.grdDO.DataSource, DataView).Count > 0 Then
                            CType(Me.grdDO.DataSource, DataView).Sort = "OA_BRANDPACK_ID"
                            Dim index As Integer = CType(Me.grdDO.DataSource, DataView).Find(OA_BRANDPACK_ID)
                            If index <> -1 Then
                                Dim DO_TP_QTY As Object = CType(Me.grdDO.DataSource, DataView)(index)("DO_TP_QTY")
                                If (Not IsNothing(DO_TP_QTY)) And (Not IsDBNull(DO_TP_QTY)) Then
                                    If CDec(DO_TP_QTY) > CDec(Me.grdOTP.GetValue(e.Column)) Then
                                        Me.ShowMessageInfo("can not edit data" & vbCrLf & "parent is smaller than child data")
                                        Me.grdOTP.CancelCurrentEdit()
                                        CType(Me.grdDO.DataSource, DataView).Sort = "DO_TP_BRANDPACK"
                                        Return
                                    End If
                                End If
                            End If
                        End If
                    End If
                    Dim SPPB_BRANDPACK_ID As String = Me.mcbSPPB.Text & "" & OA_BRANDPACK_ID
                    'check qty brandpack
                    Dim SPPB_QTY As Decimal = Me.clsThirdParty.getSPPB_QTY(SPPB_BRANDPACK_ID)
                    If SPPB_QTY < 0 Then
                        Me.ShowMessageInfo("SPPB_QTY < 0 ")
                        Me.grdOTP.CancelCurrentEdit()
                        Return
                    End If
                    If CDec(Me.grdOTP.GetValue("OTP_QTY")) > SPPB_QTY Then
                        Me.ShowMessageInfo("value is more than it should be")
                        Me.grdOTP.CancelCurrentEdit()
                        Return
                    End If
                End If
                'SET VALUENYA
                Me.grdOTP.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                Me.grdOTP.SetValue("MODIFY_DATE", CDate(NufarmBussinesRules.SharedClass.ServerDate().ToShortDateString()))
            End If
            'Me.mcbSPPB.ReadOnly = IsEnabled
            CType(Me.grdDO.DataSource, DataView).Sort = "DO_TP_BRANDPACK"
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdOTP_CellValueChanged")
            'Me.SVM = SetValueMode.HasChanged
        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdOTP_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdOTP.ColumnButtonClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.grdOTP.GetRow().RowType() = Janus.Windows.GridEX.RowType.Record Then

                If e.Column.Key = "NO" Then

                    'check keberadaan oa_brandpack_id di grid do
                    If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes Then
                        If Not IsNothing(Me.grdDO.DataSource) Then
                            CType(Me.grdDO.DataSource, DataView).Sort = "OA_BRANDPACK_ID"
                            Dim index As Integer = CType(Me.grdDO.DataSource, DataView).Find(Me.grdOTP.GetValue("OA_BRANDPACK_ID").ToString())
                            If index <> -1 Then
                                CType(Me.grdDO.DataSource, DataView).Delete(index)
                                Me.grdDO.UpdateData()
                                Me.grdDO.Refetch()
                                'Me.grdDO.Refetch()
                            End If
                        End If
                        Me.clsThirdParty.Delete(Me.grdOTP.GetValue("OA_BRANDPACK_ID").ToString())
                        Me.DeleteRowGrid = DeleteFrom.ButtonClick

                        Me.grdOTP.Delete()
                        Me.grdOTP.UpdateData()
                        Me.DeleteRowGrid = DeleteFrom.Keyboard
                        CType(Me.grdDO.DataSource, DataView).Sort = "DO_TP_BRANDPACK"
                    Else
                        'e.Cancel = True
                        Return
                    End If
                    'Dim isEnabled As Boolean = Me.mcbSPPB.ReadOnly
                    'If isEnabled Then
                    '    Me.mcbSPPB.ReadOnly = False
                    'End If
                    If Me.ModeSave = Mode.Update Then
                        If CType(Me.grdOTP.DataSource, DataView).Count <= 0 Then
                            'Me.clsThirdParty.GetDataSet.AcceptChanges()
                            Me.txtOTP_NO.Text = ""
                            Me.txtOTP_NO.ReadOnly = False
                            Me.dtpOTPDate.MinDate = CDate(Me.mcbSPPB.DropDownList().GetValue("SPPB_DATE"))
                            Me.clsThirdParty.GetDataSet.AcceptChanges()
                        Else
                            Me.txtOTP_NO.ReadOnly = True
                        End If
                        If CType(Me.grdDO.DataSource, DataView).Count <= 0 Then
                            Me.txtDO_TP_NO.Text = ""
                            Me.txtDO_TP_NO.ReadOnly = False
                            Me.dtPicDOThirdParty.MinDate = Me.dtpOTPDate.MinDate
                        Else
                            Me.txtDO_TP_NO.ReadOnly = True
                        End If
                        'Me.mcbSPPB.ReadOnly = isEnabled
                    End If

                End If
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdOTP_ColumnButtonClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdOTP_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdOTP.DeletingRecord

        Try
            If Me.DeleteRowGrid = DeleteFrom.ButtonClick Then
                Return
            End If
            'check keberadaan oa_brandpack_id di grid do
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes Then
                If Not IsNothing(Me.grdDO.DataSource) Then
                    CType(Me.grdDO.DataSource, DataView).Sort = "OA_BRANDPACK_ID"
                    Dim index As Integer = CType(Me.grdDO.DataSource, DataView).Find(Me.grdOTP.GetValue("OA_BRANDPACK_ID").ToString())
                    If index <> -1 Then
                        CType(Me.grdDO.DataSource, DataView).Delete(index)
                        Me.grdDO.UpdateData()
                        Me.grdDO.Refetch()
                        'Me.grdDO.Refetch()
                    End If
                End If
                Me.clsThirdParty.Delete(Me.grdOTP.GetValue("OA_BRANDPACK_ID").ToString())
                e.Cancel = False
                Me.grdOTP.UpdateData()

                CType(Me.grdDO.DataSource, DataView).Sort = "DO_TP_BRANDPACK"
            Else
                e.Cancel = True
            End If
            'Dim isEnabled As Boolean = Me.mcbSPPB.ReadOnly
            'If isEnabled Then
            '    Me.mcbSPPB.ReadOnly = False
            'End If
            If Me.ModeSave = Mode.Update Then
                If CType(Me.grdOTP.DataSource, DataView).Count <= 0 Then
                    'Me.clsThirdParty.GetDataSet.AcceptChanges()
                    Me.txtOTP_NO.Text = ""
                    Me.txtOTP_NO.ReadOnly = False
                    Me.dtpOTPDate.MinDate = CDate(Me.mcbSPPB.DropDownList().GetValue("SPPB_DATE"))
                    Me.clsThirdParty.GetDataSet.AcceptChanges()
                Else
                    Me.txtOTP_NO.ReadOnly = True
                End If
                If CType(Me.grdDO.DataSource, DataView).Count <= 0 Then
                    Me.txtDO_TP_NO.Text = ""
                    Me.txtDO_TP_NO.ReadOnly = False
                    Me.dtPicDOThirdParty.MinDate = Me.dtpOTPDate.MinDate
                Else
                    Me.txtDO_TP_NO.ReadOnly = True
                End If
                'Me.mcbSPPB.ReadOnly = isEnabled
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdOTP_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdOTP_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOTP.Enter
        Try
            If Me.IsvalidOTP() = False Then
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdDO_AddingRecord(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdDO.AddingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            'CHECK VALIDITY
            If Me.IsValidDO() = False Then
                Me.grdDO.CancelCurrentEdit()
                Return
            End If
            'check BRANDPACK_ID 
            If (IsNothing(Me.grdDO.GetValue("BRANDPACK_ID"))) Or (Me.grdDO.GetValue("BRANDPACK_ID") Is DBNull.Value) Then
                Me.ShowMessageInfo("Please Suply BrandPack")
                Me.grdDO.CancelCurrentEdit()
                Me.grdDO.MoveToNewRecord()
                Return
            ElseIf (IsNothing(Me.grdDO.GetValue("DO_TP_QTY"))) Or (Me.grdDO.GetValue("DO_TP_QTY") <= 0) Or (IsDBNull(Me.grdDO.GetValue("DO_TP_QTY"))) Then
                Me.ShowMessageInfo("Quantity can not be null / zero")
                Me.grdDO.CancelCurrentEdit()
                Me.grdDO.MoveToNewRecord()
                Return
            End If
            'check ind dataview
            Dim DO_TP_BRANDPACK As Object = Me.grdDO.GetValue("DO_TP_BRANDPACK")
            If IsDBNull(Me.grdDO.GetValue("DO_TP_BRANDPACK")) Then
                Me.ShowMessageInfo("Please Suply BrandPack")
                Me.grdDO.CancelCurrentEdit()
                Me.grdDO.MoveToNewRecord()
                Return
            End If
            If CType(Me.grdDO.DataSource, DataView).Find(DO_TP_BRANDPACK) <> -1 Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
                Me.grdDO.CancelCurrentEdit()
                Me.grdDO.MoveToNewRecord()
                Return
                'check in database
            ElseIf Me.clsThirdParty.EXISTS_DO_TP_BRANDPACK(DO_TP_BRANDPACK) = True Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
                Me.grdDO.CancelCurrentEdit()
                Me.grdDO.MoveToNewRecord()
                Return
            End If
            Dim OTP_QTY As Object = Nothing
            Dim OA_BRANDPACK_ID = Me.grdDO.GetValue("OA_BRANDPACK_ID")
            If Not IsNothing(Me.grdOTP.DataSource) Then
                CType(Me.grdOTP.DataSource, DataView).Sort = "OA_BRANDPACK_ID"
                Dim index As Integer = CType(Me.grdOTP.DataSource, DataView).Find(OA_BRANDPACK_ID)
                If index <> -1 Then
                    OTP_QTY = CType(Me.grdOTP.DataSource, DataView)(index)("OTP_QTY")
                    If OTP_QTY <= 0 Then
                        Me.ShowMessageInfo("invalid value qty" & vbCrLf & "Quantity BrandPack for PO_3RD_PARTY is null/zero")
                        Me.grdDO.CancelCurrentEdit()
                        Return
                    End If
                    If CDec(Me.grdDO.GetValue("DO_TP_QTY")) > OTP_QTY Then
                        Me.ShowMessageInfo("value is more than it should be")
                        Me.grdDO.CancelCurrentEdit()
                        Return
                    End If
                End If
            Else
                Me.ShowMessageInfo("Please fill PO 3rd Party qty first")
                Me.grdDO.CancelCurrentEdit()
                Return
            End If
            'if ctype(me.grdDO.DataSource,DataView).fi'
            'set value
            'Me.SVM = SetValueMode.Changing
            Me.grdDO.SetValue("CREATE_BY", NufarmBussinesRules.User.UserLogin.UserName)
            Me.grdDO.SetValue("CREATE_DATE", CDate(NufarmBussinesRules.SharedClass.ServerDate().ToShortDateString()))
            'Me.SVM = SetValueMode.HasChanged
            CType(Me.grdOTP.DataSource, DataView).Sort = "OTP_BRANDPACK"
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdDO_AddingRecord")
            'Me.SVM = SetValueMode.HasChanged
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdDO_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDO.CellUpdated
        Try
            Me.Cursor = Cursors.WaitCursor
            'check validity
            If Me.IsvalidOTP() = False Then
                Return
            End If
            If Me.IsValidDO() = False Then
                Return
            End If
            'Dim IsEnabled As Boolean = Me.mcbSPPB.ReadOnly
            'If IsEnabled Then
            '    Me.mcbSPPB.ReadOnly = False
            'End If
            'Dim OA_ID As String = Me.mcbSPPB.DropDownList.GetValue("OA_REF_NO").ToString()
            If OA_ID = "" Or PO_REF_NO = "" Then
                Me.ShowMessageInfo("PO_REF_NO refered to SPPB is null ") : Return
            End If
            If Me.grdDO.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                'set valuenya
                If e.Column.Key = "BRANDPACK_ID" Then
                    If (Not IsNothing(Me.grdDO.GetValue("BRANDPACK_ID"))) And (Not IsDBNull(Me.grdDO.GetValue("BRANDPACK_ID"))) Then
                        Dim PO_BRANDPACK_ID As String = PO_REF_NO & "" & Me.grdDO.GetValue("BRANDPACK_ID").ToString()
                        Dim OA_BRANDPACK_ID As String = OA_ID & PO_BRANDPACK_ID
                        Dim DO_TP_BRANDPACK As String = Me.txtDO_TP_NO.Text.Trim() & OA_BRANDPACK_ID
                        'CType(Me.grdDO.DataSource, DataView).Sort = "DO_TP_BRANDPACK"
                        If CType(Me.grdDO.DataSource, DataView).Find(DO_TP_BRANDPACK) <> -1 Then
                            Me.ShowMessageInfo(Me.MessageDataHasExisted)
                            Me.grdDO.CancelCurrentEdit()
                            Return
                        End If
                        CType(Me.grdOTP.DataSource, DataView).Sort = "OA_BRANDPACK_ID"
                        'chekc master otpl_brandpack
                        If CType(Me.grdOTP.DataSource, DataView).Find(OA_BRANDPACK_ID) = -1 Then
                            Me.ShowMessageInfo("Please insert PO 3RD PARTY first")
                            Me.grdDO.CancelCurrentEdit()
                            CType(Me.grdOTP.DataSource, DataView).Sort = "OTP_BRANDPACK"
                            Return
                        ElseIf Me.clsThirdParty.EXISTS_DO_TP_BRANDPACK(DO_TP_BRANDPACK) = True Then
                            Me.ShowMessageInfo(Me.MessageDataHasExisted)
                            Me.grdDO.CancelCurrentEdit()
                            Return
                        End If
                        'Dim PO_BRANDPACK_ID As String = Me.mcbSPPB.DropDownList.GetValue("PO_REF_NO").ToString() & "" & Me.grdOTP.GetValue("BRANDPACK_ID").ToString()
                        'Dim OA_BRANDPACK_ID As String = OA_ID & PO_BRANDPACK_ID
                        'Dim OTP_BRANDPACK As String = Me.txtOTP_NO.Text.Trim() & OA_BRANDPACK_ID
                        'If Me.clsThirdParty.ViewOTP().Find(OTP_BRANDPACK) <> -1 Then
                        '    Me.ShowMessageInfo(Me.MessageDataHasExisted)
                        '    Me.grdOTP.CancelCurrentEdit()
                        '    Me.grdOTP.MoveToNewRecord()
                        '    Return
                        'End If
                        'If Me.clsThirdParty.EXISTS_OTP_BRANDPACK(OTP_BRANDPACK) Then
                        '    Me.ShowMessageInfo(Me.MessageDataHasExisted)
                        '    Me.grdOTP.CancelCurrentEdit()
                        '    Me.grdOTP.MoveToNewRecord()
                        '    Return
                        'End If

                        'Me.SVM = SetValueMode.Changing
                        Me.grdDO.SetValue("DO_TP_BRANDPACK", DO_TP_BRANDPACK)
                        Me.grdDO.SetValue("DO_TP_NO", Me.txtDO_TP_NO.Text.Trim())
                        Me.grdDO.SetValue("OA_BRANDPACK_ID", OA_BRANDPACK_ID)
                        Dim OTP_BRANDPACK As String = Me.txtOTP_NO.Text.Trim() & "" & OA_BRANDPACK_ID
                        'ambil sppb_qty
                        If (IsDBNull(Me.grdDO.GetValue("DO_TP_QTY"))) Or (IsNothing(Me.grdDO.GetValue("DO_TP_QTY"))) Then
                            'Dim OTP_QTY As Decimal = Me.clsThirdParty.getOTPQTY(OTP_BRANDPACK)
                            Dim index As Integer = CType(Me.grdOTP.DataSource, DataView).Find(OA_BRANDPACK_ID)
                            If index <> -1 Then
                                Dim OTP_QTY = CType(Me.grdOTP.DataSource, DataView)(index)("OTP_QTY")
                                If (Not IsNothing(OTP_QTY)) And (Not IsDBNull(OTP_QTY)) Then 'OTP_QTY <= 0 Then
                                    'CType(Me.grdOTP.DataSource, DataView).Sort = "OA_BRANDPACK_ID"
                                    Me.grdDO.SetValue("DO_TP_QTY", OTP_QTY)
                                End If
                            End If
                            Me.grdDO.SelectCurrentCellText()
                        End If
                        'Me.SVM = SetValueMode.HasChanged
                    End If
                ElseIf e.Column.Key = "OTP_QTY" Then
                    If (IsNothing(Me.grdDO.GetValue("BRANDPACK_ID"))) Or (IsDBNull(Me.grdDO.GetValue("BRANDPACK_ID"))) Then
                        Me.ShowMessageInfo("Please define brandpack first")
                        Me.grdDO.CancelCurrentEdit()
                        Return
                    End If
                    Dim PO_BRANDPACK_ID As String = PO_REF_NO & "" & Me.grdDO.GetValue("BRANDPACK_ID").ToString()
                    Dim OA_BRANDPACK_ID As String = OA_ID & PO_BRANDPACK_ID
                    Dim DO_TP_BRANDPACK As String = Me.txtDO_TP_NO.Text.Trim() & OA_BRANDPACK_ID
                    Dim OTP_BRANDPACK As String = Me.txtOTP_NO.Text.Trim() & "" & OA_BRANDPACK_ID
                    Dim OTP_QTY As Object = Nothing
                    If Not IsNothing(Me.grdOTP.DataSource) Then
                        CType(Me.grdOTP.DataSource, DataView).Sort = "OA_BRANDPACK_ID"
                        Dim index As Integer = CType(Me.grdOTP.DataSource, DataView).Find(OA_BRANDPACK_ID)
                        If index <> -1 Then
                            OTP_QTY = CType(Me.grdOTP.DataSource, DataView)(index)("OTP_QTY")
                            If OTP_QTY <= 0 Then
                                Me.ShowMessageInfo("invalid value qty" & vbCrLf & "Quantity BrandPack for PO_3RD_PARTY is null/zero")
                                Me.grdDO.CancelCurrentEdit()
                                Return
                            End If
                            If CDec(Me.grdDO.GetValue("DO_TP_QTY")) > OTP_QTY Then
                                Me.ShowMessageInfo("value is more than it should be")
                                Me.grdDO.CancelCurrentEdit()
                                Return
                            End If
                        End If
                    Else
                        Me.ShowMessageInfo("Please fill PO 3rd Party qty first")
                        Me.grdDO.CancelCurrentEdit()
                        Return
                    End If
                    'Dim OTP_QTY As Decimal = Me.clsThirdParty.getOTPQTY(OTP_BRANDPACK)
                    'If CDec(Me.grdDO.GetValue("DO_TP_QTY")) > OTP_QTY Then
                    '    Me.ShowMessageInfo("value is more than it should be")
                    '    Me.grdDO.CancelCurrentEdit()
                    '    Return
                    'End If
                    Me.grdDO.SetValue("DO_TP_BRANDPACK", DO_TP_BRANDPACK)
                    Me.grdDO.SetValue("DO_TP_NO", Me.txtDO_TP_NO.Text.Trim())
                    Me.grdDO.SetValue("OA_BRANDPACK_ID", OA_BRANDPACK_ID)
                    Me.grdDO.SetValue("DO_TP_QTY", OTP_QTY)
                    Me.grdDO.SelectCurrentCellText()
                    'check qty brandpack

                    'Dim OTP_QTY As Decimal = Me.clsThirdParty.getOTPQTY(OTP_BRANDPACK)
                    'If OTP_QTY < 0 Then
                    '    Me.ShowMessageInfo("SPPB_BRANDPACK IS 0 ")
                    '    Me.grdOTP.CancelCurrentEdit()
                    '    Return
                    'End If

                End If

            ElseIf Me.grdDO.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                'CHECK OTP_BRANDPACK
                If e.Column.Key = "BRANDPACK_ID" Then
                    'Dim PO_BRANDPACK_ID As String = Me.mcbSPPB.DropDownList.GetValue("PO_REF_NO").ToString()
                    'Dim OA_BRANDPACK_ID As String = OA_ID & PO_BRANDPACK_ID
                    'Dim OTP_BRANDPACK As String = Me.txtOTP_NO.Text.Trim() & OA_BRANDPACK_ID
                    If Me.ModeSave = Mode.Update Then
                        Me.ShowMessageInfo("can not set Brandpack while update")
                        Me.grdDO.CancelCurrentEdit()
                        Return
                    End If
                    'CType(Me.grdDO.DataSource, DataView).Sort = "DO_TP_BRANDPACK"
                    'If CType(Me.grdDO.DataSource, DataView).Find(DO_TP_BRANDPACK) <> -1 Then
                    '    Me.ShowMessageInfo(Me.MessageDataHasExisted)
                    '    Me.grdDO.CancelCurrentEdit()
                    '    Return
                    'End If
                    If (Not IsNothing(Me.grdDO.GetValue("BRANDPACK_ID"))) And (Not IsDBNull(Me.grdDO.GetValue("BRANDPACK_ID"))) Then
                        Dim PO_BRANDPACK_ID As String = PO_REF_NO & "" & Me.grdDO.GetValue("BRANDPACK_ID").ToString()
                        Dim OA_BRANDPACK_ID As String = OA_ID & PO_BRANDPACK_ID
                        Dim DO_TP_BRANDPACK As String = Me.txtDO_TP_NO.Text.Trim() & OA_BRANDPACK_ID
                        CType(Me.grdDO.DataSource, DataView).Sort = "DO_TP_BRANDPACK"
                        If CType(Me.grdDO.DataSource, DataView).Find(DO_TP_BRANDPACK) <> -1 Then
                            Me.ShowMessageInfo(Me.MessageDataHasExisted)
                            Me.grdDO.CancelCurrentEdit()
                            Return
                        End If
                        'Me.SVM = SetValueMode.Changing
                        Me.grdDO.SetValue("OA_BRANDPACK_ID", OA_BRANDPACK_ID)
                        Me.grdDO.SetValue("DO_TP_NO", Me.txtDO_TP_NO.Text.Trim())
                        Me.grdDO.SetValue("DO_TP_BRANDPACK", DO_TP_BRANDPACK)
                        'Dim SPPB_BRANDPACK_ID As String = Me.mcbSPPB.Value.ToString() & "" & OA_BRANDPACK_ID
                        'ambil sppb_qty
                        'Dim SPPB_QTY As Decimal = Me.clsThirdParty.getSPPB_QTY(SPPB_BRANDPACK_ID)
                        'Me.grdOTP.SetValue("OTP_QTY", SPPB_QTY)
                        'Me.SVM = SetValueMode.HasChanged
                    Else
                        Me.ShowMessageInfo("can not set null value for brandpack")
                        Me.grdDO.CancelCurrentEdit()
                        Return
                    End If
                ElseIf e.Column.Key = "DO_TP_QTY" Then
                    If (IsNothing(Me.grdDO.GetValue("BRANDPACK_ID"))) Or (IsDBNull(Me.grdDO.GetValue("BRANDPACK_ID"))) Then
                        Me.ShowMessageInfo("Please define brandpack first")
                        Me.grdDO.CancelCurrentEdit()
                        Return
                    ElseIf (IsNothing(Me.grdDO.GetValue("DO_TP_QTY"))) Or (IsDBNull(Me.grdDO.GetValue("DO_TP_QTY"))) Then
                        Me.ShowMessageInfo("can not set null value")
                        Me.grdDO.CancelCurrentEdit()
                        Return
                    End If

                    Dim PO_BRANDPACK_ID As String = PO_REF_NO & "" & Me.grdDO.GetValue("BRANDPACK_ID").ToString()
                    Dim OA_BRANDPACK_ID As String = OA_ID & PO_BRANDPACK_ID
                    Dim OTP_BRANDPACK As String = Me.txtOTP_NO.Text.ToString() & "" & OA_BRANDPACK_ID
                    'check qty brandpaCK
                    'Dim OTP_QTY As Decimal = Me.clsThirdParty.getOTPQTY(OTP_BRANDPACK)
                    If Not IsNothing(Me.grdOTP.DataSource) Then
                        CType(Me.grdOTP.DataSource, DataView).Sort = "OA_BRANDPACK_ID"
                        Dim index As Integer = CType(Me.grdOTP.DataSource, DataView).Find(OA_BRANDPACK_ID)
                        If index <> -1 Then
                            Dim OTP_QTY As Object = CType(Me.grdOTP.DataSource, DataView)(index)("OTP_QTY")
                            If OTP_QTY <= 0 Then
                                Me.ShowMessageInfo("invalid value qty" & vbCrLf & "Quantity BrandPack for PO_3RD_PARTY is null/zero")
                                Me.grdDO.CancelCurrentEdit()
                                Return
                            End If
                            If CDec(Me.grdDO.GetValue("DO_TP_QTY")) > OTP_QTY Then
                                Me.ShowMessageInfo("value is more than it should be")
                                Me.grdDO.CancelCurrentEdit()
                                Return
                            End If
                        End If
                    End If
                End If
                'SET VALUENYA
                Me.grdOTP.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                Me.grdOTP.SetValue("MODIFY_DATE", CDate(NufarmBussinesRules.SharedClass.ServerDate().ToShortDateString()))
            End If


            'Dim OA_ID As String = Me.mcbSPPB.DropDownList.GetValue("OA_REF_NO").ToString()
            'Dim PO_BRANDPACK_ID As String = Me.mcbSPPB.DropDownList.GetValue("PO_REF_NO").ToString()
            'Dim OA_BRANDPACK_ID As String = OA_ID & PO_BRANDPACK_ID
            'Dim DO_TP_BRANDPACK As String = Me.txtDO_TP_NO.Text.Trim() & OA_BRANDPACK_ID
            'If Me.grdDO.GetRow().RowType() = Janus.Windows.GridEX.RowType.NewRecord Then
            '    'setvalue
            '    If CType(Me.grdOTP.DataSource, DataView).Find(OA_BRANDPACK_ID) <= -1 Then
            '        Me.ShowMessageInfo("Please insert PO 3RD PARTY first")
            '        Me.grdDO.CancelCurrentEdit()
            '        Return
            '    End If
            '    If e.Column.Key = "BRANDPACK_ID" Then
            '        Me.SVM = SetValueMode.Changing
            '        Me.grdDO.SetValue("OA_BRANDPACK_ID", OA_BRANDPACK_ID)
            '        Me.grdDO.SetValue("DO_TP_NO", Me.txtDO_TP_NO.Text.Trim())
            '        Me.grdDO.SetValue("DO_TP_BRANDPACK", DO_TP_BRANDPACK)
            '        Me.SVM = SetValueMode.HasChanged
            '    End If
            'ElseIf Me.grdDO.GetRow().RowType() = Janus.Windows.GridEX.RowType.Record Then
            '    If CType(Me.grdDO.DataSource, DataView).Sort = "OA_BRANDPACK_ID" Then
            '        CType(Me.grdDO.DataSource, DataView).Sort = "DO_TP_BRANDPACK"
            '    End If
            '    If e.Column.Key = "BRANDPACK_ID" Then
            '        'check existing data
            '        If CType(Me.grdDO.DataSource, DataView).Find(DO_TP_BRANDPACK) <> -1 Then
            '            Me.ShowMessageInfo(Me.MessageDataHasExisted)
            '            Me.grdDO.CancelCurrentEdit()
            '            Return
            '            'chekc master otpl_brandpack
            '        ElseIf CType(Me.grdOTP.DataSource, DataView).Find(OA_BRANDPACK_ID) <= -1 Then
            '            Me.ShowMessageInfo("Please insert PO 3RD PARTY first")
            '            Me.grdDO.CancelCurrentEdit()
            '            Return
            '        ElseIf Me.clsThirdParty.EXISTS_DO_TP_BRANDPACK(DO_TP_BRANDPACK) = True Then
            '            Me.ShowMessageInfo(Me.MessageDataHasExisted)
            '            Me.grdDO.CancelCurrentEdit()
            '            Return
            '        End If
            '        Me.SVM = SetValueMode.Changing
            '        Me.grdDO.SetValue("OA_BRANDPACK_ID", OA_BRANDPACK_ID)
            '        Me.grdDO.SetValue("DO_TP_NO", Me.txtDO_TP_NO.Text.Trim())
            '        Me.grdDO.SetValue("DO_TP_BRANDPACK", DO_TP_BRANDPACK)
            '        Me.SVM = SetValueMode.HasChanged
            '    ElseIf e.Column.Key = "DO_TP_QTY" Then
            '        If (Me.grdDO.GetValue("DO_TP_QTY") Is DBNull.Value) Or (IsNothing(Me.grdDO.GetValue("DO_TP_QTY"))) Then
            '            Me.ShowMessageInfo("Quantity can not be null")
            '            Me.grdDO.CancelCurrentEdit()
            '            Me.grdDO.MoveToNewRecord()
            '            Return
            '        End If
            '    End If
            'End If
            CType(Me.grdOTP.DataSource, DataView).Sort = "OTP_BRANDPACK"
            'Me.mcbSPPB.ReadOnly = IsEnabled
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdDO_CellValueChanged")
            'Me.SVM = SetValueMode.HasChanged
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdDO_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDO.ColumnButtonClick
        Try
            If Me.grdDO.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "NO" Then

                    Me.DeleteRowGrid = DeleteFrom.Keyboard
                    If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes Then
                        Me.clsThirdParty.DeleteDOThirdParty(Me.grdDO.GetValue("OA_BRANDPACK_ID"))
                        'e.Cancel = False
                        Me.DeleteRowGrid = DeleteFrom.ButtonClick
                        Me.grdDO.Delete()

                        Me.grdDO.UpdateData()

                    Else
                        'e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdDO_ColumnButtonClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdDO_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdDO.DeletingRecord
        Try
            If Me.DeleteRowGrid = DeleteFrom.ButtonClick Then
                Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes Then
                Me.clsThirdParty.DeleteDOThirdParty(Me.grdDO.GetValue("OA_BRANDPACK_ID"))
                e.Cancel = False
                Me.grdDO.UpdateData()
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdDO_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdDO_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDO.Enter
        Try
            If Me.IsValidDO() = False Then
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdDO_Error(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ErrorEventArgs) Handles grdDO.Error
        Try
            If (Me.SFG = StateFillingGrid.Filling) Or (Me.SFG = StateFillingGrid.Disposing) Then
                Return
            End If
            Me.ShowMessageError("Please recheck validity data(s)")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdOTP_Error(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ErrorEventArgs) Handles grdOTP.Error
        Try
            If (Me.SFG = StateFillingGrid.Filling) Or (Me.SFG = StateFillingGrid.Disposing) Then
                Return
            End If
            Me.ShowMessageError("Please recheck validity data(s)")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFilteDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilteDate.Click
        Try
            If Me.cmbDistributor.SelectedItem Is Nothing Then
                Me.ShowMessageInfo("Please define Distributor")
                Return
            End If
            Me.CheckData()
            If (Me.dtpicFrom.Text <> "") And (Me.dtPicUntil.Text <> "") Then
                Me.clsThirdParty.CreateViewThirdParty(Me.cmbDistributor.Value.ToString(), CDate(Me.dtpicFrom.Value.ToShortDateString()), CDate(Me.dtPicUntil.Value.ToShortDateString()))
            ElseIf (Me.dtpicFrom.Text <> "") And (Me.dtPicUntil.Text = "") Then
                Me.clsThirdParty.CreateViewThirdParty(Me.cmbDistributor.Value.ToString(), CDate(Me.dtpicFrom.Value.ToShortDateString()), DBNull.Value)
            ElseIf (Me.dtpicFrom.Text = "") And (Me.dtPicUntil.Text <> "") Then
                Me.clsThirdParty.CreateViewThirdParty(Me.cmbDistributor.Value.ToString(), DBNull.Value, CDate(Me.dtPicUntil.Value.ToShortDateString()))
            Else
                Me.clsThirdParty.CreateViewThirdParty(Me.cmbDistributor.Value.ToString(), DBNull.Value, DBNull.Value)
            End If
            Me.Bindgrid(Me.grdMain, Me.clsThirdParty.ViewThirdParty())
            Me.ExpandableSplitter1.Expanded = False
            Me.ClearControl()
            Me.ModeSave = Mode.Save
        Catch EX As DBConcurrencyException
            Me.clsThirdParty.GetDataSet().AcceptChanges()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
        Finally
            Me.Cursor = Cursors.Default
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

    Private Sub dtPicUntil_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicUntil.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.dtPicUntil.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtOTP_NO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOTP_NO.KeyPress
        Try
            Me.Cursor = Cursors.WaitCursor

            If e.KeyChar = Chr(13) Then
                If Me.txtOTP_NO.Text = """" Then
                    Me.baseTooltip.Show("Please define PO 3rd ", Me.txtOTP_NO, 3000)
                    Me.txtOTP_NO.Focus()
                    Return
                End If
                If Me.ModeSave = Mode.Save Then
                    'check existing data
                    If Me.clsThirdParty.EXISTSOTP_NO(Me.txtOTP_NO.Text.Trim()) = True Then
                        Me.ShowMessageInfo(Me.MessageDataHasExisted)
                        Me.txtOTP_NO.Focus()
                        Me.txtOTP_NO.SelectAll()

                        Return
                    End If
                End If
                If Not IsNothing(Me.clsThirdParty.GetDataSet()) Then
                    If Me.clsThirdParty.GetDataSet.HasChanges() Then
                        If Me.ShowConfirmedMessage("Data(s) you've just edited has changed" & vbCrLf & "Discard all changes ?") = Windows.Forms.DialogResult.Yes Then
                            Me.clsThirdParty.GetDataSet().RejectChanges()
                        Else
                            Me.baseTooltip.Show("Please Press this button/press enter", Me.btnSave, 3000)
                            Me.btnSave.Focus()
                            Return
                        End If
                    End If

                End If

                Me.grdOTP.Enabled = True
                Me.grdDO.Enabled = False
                Me.clsThirdParty.GetData(Me.txtOTP_NO.Text.Trim())
                Me.Bindgrid(Me.grdOTP, Me.clsThirdParty.ViewOTP())
                Me.Bindgrid(Me.grdDO, Me.clsThirdParty.ViewDOTP())
                Me.FillCategoriesValueList(Me.mcbSPPB.Value.ToString())
            Else
                Me.grdOTP.Enabled = False
                Me.grdDO.Enabled = False
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtDO_TP_NO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDO_TP_NO.KeyPress
        Try
            If e.KeyChar = Chr(13) Then
                If Me.txtOTP_NO.Text = "" Then
                    Me.baseTooltip.Show("Please define this value first", Me.txtOTP_NO, 3000)
                    Me.txtOTP_NO.Focus()
                    Return
                End If
                If Me.txtDO_TP_NO.Text = "" Then
                    Me.baseTooltip.Show("Please define value", Me.txtDO_TP_NO, 3000)
                    Me.txtDO_TP_NO.Focus()
                    Return
                End If
                If Me.ModeSave = Mode.Save Then
                    If Me.clsThirdParty.EXIXTS_DO_TP_NO(Me.txtDO_TP_NO.Text.Trim()) Then
                        Me.ShowMessageInfo(Me.MessageDataHasExisted)
                        Me.txtDO_TP_NO.Focus()
                        Me.txtDO_TP_NO.SelectAll()
                        Return
                    End If
                End If

                'Me.clsThirdParty.GetData(Me.txtDO_TP_NO.Text.Trim())
                Me.grdOTP.Enabled = True
                Me.grdDO.Enabled = True
            Else
                Me.grdOTP.Enabled = False
                Me.grdDO.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdOTP_RecordAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOTP.RecordAdded
        Try
            Me.grdOTP.UpdateData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdDO_RecordAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDO.RecordAdded
        Try
            Me.grdDO.UpdateData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFilterOA_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterOA.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsThirdParty.GetDataSet()) Then
                If Me.clsThirdParty.GetDataSet.HasChanges() Then
                    If Me.ShowConfirmedMessage("Data(s) you've just edited has changed" & vbCrLf & "Discard all changes ?") = Windows.Forms.DialogResult.Yes Then
                        Me.clsThirdParty.GetDataSet().RejectChanges()
                    Else
                        Me.baseTooltip.Show("Please Press this button/press enter", Me.btnSave, 3000)
                        Me.btnSave.Focus()
                        Return
                    End If
                End If
            End If
            Me.clsThirdParty.SearchSPPB_NO(Me.cmbDistributor.Value.ToString(), Me.mcbSPPB.Text)
            Me.BindMultiColumnCombo(Me.mcbSPPB, Me.clsThirdParty.ViewSPPB_NO())
            Dim itemCount As Integer = Me.mcbSPPB.DropDownList.RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub dtPicDOThirdParty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicDOThirdParty.KeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                Me.Cursor = Cursors.WaitCursor
                Me.txtDO_TP_NO_KeyPress(Me.txtDO_TP_NO, New KeyPressEventArgs(Chr(13)))
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub dtpOTPDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpOTPDate.KeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                Me.Cursor = Cursors.WaitCursor
                Me.txtOTP_NO_KeyPress(Me.txtOTP_NO, New KeyPressEventArgs(Chr(13)))
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#End Region

End Class
