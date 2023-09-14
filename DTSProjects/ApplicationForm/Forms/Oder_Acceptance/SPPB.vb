Imports NufarmBussinesRules.common.Helper
Imports System.Configuration
Public Class SPPB
    Private NumberClick As Integer = 0
#Region " Deklarasi "
    'Private m_clsSPPBDetail As NufarmBussinesRules.OrderAcceptance.SPPB_Detail
    Private SFM As StateFillingMCB
    Private WithEvents frmManager As UserControl = Nothing

    Private IsLoadding As Boolean
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX = Nothing
    Public Event ComboboxValue_Changed(ByVal sender As Object, ByVal e As EventArgs)
    Public Event ShowSPPBData(ByVal ShowProgress As Boolean)
    Friend CMain As Main = Nothing
    Friend MustReloadData As Boolean = False
    Private m_clsSPPBGONEntry As NufarmBussinesRules.OrderAcceptance.SPPBEntryGON = Nothing
    Private IsHOUser As Boolean = NufarmBussinesRules.SharedClass.IsUserHO
    Private IsSystemAdmin = CBool(ConfigurationManager.AppSettings("SysA"))
    'Private WithEvents flUp As New LookUp()
    Private listCheckSPPB As New List(Of String)

    Private ReadOnly Property clsSPPBGONEntry() As NufarmBussinesRules.OrderAcceptance.SPPBEntryGON
        Get
            If m_clsSPPBGONEntry Is Nothing Then
                m_clsSPPBGONEntry = New NufarmBussinesRules.OrderAcceptance.SPPBEntryGON()
            End If
            Return m_clsSPPBGONEntry
        End Get
    End Property
    Private ReadOnly Property clsSPPBDetail() As NufarmBussinesRules.OrderAcceptance.SPPB_Detail
        Get
            If Me.m_clsSPPBDetail Is Nothing Then
                Me.m_clsSPPBDetail = New NufarmBussinesRules.OrderAcceptance.SPPB_Detail()
            End If
            Return Me.m_clsSPPBDetail
        End Get
    End Property
    Private m_clsSPPBDetail As NufarmBussinesRules.OrderAcceptance.SPPB_Detail
#End Region
    Private Enum StateFillingMCB
        HasFilled
        Filling
    End Enum

#Region " SUB "

    Private Sub BindMulticolumnCombo(ByVal dtview As DataView)
        Me.SFM = StateFillingMCB.Filling
        Me.cmbDistributor.SetDataBinding(dtview, "")
        Me.SFM = StateFillingMCB.HasFilled
    End Sub
    Friend Sub InitializeData()
        'Me.clsSPPBDetail.CreateViewDistributorSPPB()
        'Me.BindMulticolumnCombo(Me.clsSPPBDetail.ViewDistributorSPPB())
    End Sub
    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            'Me.btnGonAfterSignedByDistributor.Visible = NufarmBussinesRules.User.Privilege.ALLOW_VIEW.GONReceivedBack
            Me.btnNewSPPB.Visible = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SPPB
            Me.btnShowSPPB.Visible = NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SPPB
            Me.btnShowReturningGON.Visible = NufarmBussinesRules.User.Privilege.ALLOW_VIEW.GONReceivedBack
            If Not Me.btnNewSPPB.Visible And Not Me.btnGonAfterSignedByDistributor.Visible Then
                Me.btnEditSPPB.Visible = False : Me.btnAddNew.Visible = False
            End If

        End If

    End Sub
    Private Sub AddControl(ByVal UControl As UserControl)
        For Each ctrl As Control In Me.xpgData.Controls
            ctrl.Dispose()
        Next
        Me.xpgData.Controls.Add(UControl) : Me.xpgData.BringToFront()
        'UControl.Show()
        UControl.Dock = DockStyle.Fill
    End Sub
#End Region

    Private Sub cmbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFM = StateFillingMCB.Filling Then : Return : End If
            If Me.cmbDistributor.Value Is Nothing Then : Return : End If
            If Me.cmbDistributor.Text = "" Then : Return : End If
            If Me.cmbDistributor.SelectedItem Is Nothing Then : Return : End If
            RaiseEvent ComboboxValue_Changed(sender, e)
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_cmbDistributor_ValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
    '    Try
    '        Dim dtView As DataView = CType(Me.cmbDistributor.DataSource, DataView)
    '        dtView.RowFilter = "DISTRIBUTOR_NAME LIKE '%" & Me.cmbDistributor.Text & "%'"
    '        Me.BindMulticolumnCombo(dtView)
    '        Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount()
    '        Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")

    '    Catch ex As Exception
    '        Me.ShowMessageInfo(ex.Message)
    '        Me.LogMyEvent(ex.Message, Me.Name + "_btnApply_Click")
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnRenameColumn"
                    If Not IsNothing(Me.GridEX1) Then
                        Dim MC As New ManipulateColumn()
                        MC.ShowInTaskbar = False
                        MC.grid = Me.GridEX1
                        MC.FillcomboColumn()
                        MC.ManipulateColumnName = "Rename"
                        MC.TopMost = True
                        MC.Show(Me.Bar1, True)
                    End If
                Case "btnShowFieldChooser"
                    If Not IsNothing(Me.GridEX1) Then
                        Me.GridEX1.ShowFieldChooser(Me)
                    End If
                Case "btnSettingGrid"
                    If Not IsNothing(Me.GridEX1) Then
                        Dim SetGrid As New SettingGrid()
                        SetGrid.Grid = Me.GridEX1
                        SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                        SetGrid.ShowDialog(Me)
                    End If
                Case "btnPrint"
                    If Not IsNothing(Me.GridEX1) Then
                        Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                        Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                        'Me.PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                        If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                            Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                        End If
                        If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                            Me.PrintPreviewDialog1.Document.Print()
                        End If
                    End If
                Case "btnPageSettings"
                    If Not IsNothing(Me.GridEX1) Then
                        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                        Me.PageSetupDialog1.ShowDialog(Me)
                    End If
                Case "btnCustomFilter"
                    If Not IsNothing(Me.GridEX1) Then
                        Me.FilterEditor1.SourceControl = Me.GridEX1
                        Me.GridEX1.RemoveFilters()
                        Me.FilterEditor1.Visible = True
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                        If TypeOf (Me.frmManager) Is SPPBManager Then
                            CType(Me.frmManager, SPPBManager).xpgFilterDate.Enabled = False
                            CType(Me.frmManager, SPPBManager).dtpicFrom.Text = ""
                            CType(Me.frmManager, SPPBManager).dtPicUntil.Text = ""
                        ElseIf TypeOf (Me.frmManager) Is GonReturnedBackManager Then
                            CType(Me.frmManager, GonReturnedBackManager).XpCaptionPane1.Enabled = False
                            CType(Me.frmManager, GonReturnedBackManager).dtpicFrom.Text = ""
                            CType(Me.frmManager, GonReturnedBackManager).dtPicUntil.Text = ""
                        End If

                    End If
                Case "btnFilterEqual"
                    If Not IsNothing(Me.GridEX1) Then
                        Me.FilterEditor1.Visible = False
                        Me.GridEX1.RemoveFilters()
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        CType(Me.frmManager, SPPBManager).xpgFilterDate.Enabled = True
                    End If
                    '===============================OLD DATA=====================================================
                    'Case "btnAddNew"
                    'If Me.btnNewSPPB.Visible Or Me.btnGonAfterSignedByDistributor.Visible Then
                    '    Return
                    'End If
                    'If Not IsNothing(Me.frmManager) Then
                    '    If TypeOf (Me.frmManager) Is SPPBManager Then
                    '        Dim frmSPPBentry As New SPPBEntry()
                    '        frmSPPBentry.SaveMode = SPPBEntry.ModeSave.Save
                    '        frmSPPBentry.InitializeData() : frmSPPBentry.StartPosition = FormStartPosition.CenterParent
                    '        frmSPPBentry.ShowDialog(Me)
                    '        If Not IsNothing(Me.cmbDistributor.SelectedItem) Then
                    '            RaiseEvent ComboboxValue_Changed(Me.cmbDistributor, New EventArgs())
                    '        End If
                    '    ElseIf TypeOf (Me.frmManager) Is GonReturnedBackManager Then
                    '        Dim frmGONReceiver As New GONReceivedBack()
                    '        With frmGONReceiver
                    '            .ShowDialog(Me)
                    '        End With
                    '        If Not IsNothing(Me.cmbDistributor.SelectedItem) Then
                    '            RaiseEvent ComboboxValue_Changed(Me.cmbDistributor, New EventArgs())
                    '        End If
                    '    End If
                    'End If
                    '=============================================================================================
                Case "btnNewSPPB"
                    '=============================================================================================
                    'Dim frmSPPBentry As New SPPBEntry()
                    'frmSPPBentry.SaveMode = SPPBEntry.ModeSave.Save
                    'frmSPPBentry.InitializeData() : frmSPPBentry.StartPosition = FormStartPosition.CenterParent
                    'frmSPPBentry.frmParent = Me : Me.MustReloadData = False
                    'frmSPPBentry.ShowDialog(Me)
                    'If Not IsNothing(Me.frmManager) Then
                    '    If TypeOf (Me.frmManager) Is SPPBManager Then
                    '        If MustReloadData Then
                    '            RaiseEvent ComboboxValue_Changed(Me.cmbDistributor, New EventArgs())
                    '        End If
                    '        'If Not IsNothing(Me.cmbDistributor.SelectedItem) Then

                    '        'End If
                    '    End If
                    'End If
                    '============================================================================================
                    Me.showSPPBEntry()
                Case "btnGonAfterSignedByDistributor"
                    Dim frmGONReceiver As New GONReceivedBack()
                    With frmGONReceiver
                        Me.MustReloadData = False
                        .frmParent = Me
                        .ShowDialog(Me)
                    End With
                    If Not IsNothing(Me.frmManager) Then
                        If TypeOf (Me.frmManager) Is GonReturnedBackManager Then
                            'If Not IsNothing(Me.cmbDistributor.SelectedItem) Then
                            If MustReloadData Then
                                RaiseEvent ComboboxValue_Changed(Me.cmbDistributor, New EventArgs())
                            End If
                            'End If
                        End If
                    End If
                Case "btnShowSPPB"
                    Dim USPPBManager As New SPPBManager()
                    USPPBManager.frmParentSPPB = Me
                    Me.SFM = StateFillingMCB.Filling
                    Me.cmbDistributor.Value = Nothing : Me.cmbDistributor.Text = ""
                    Me.SFM = StateFillingMCB.HasFilled
                    Me.AddControl(CType(USPPBManager, UserControl))
                    Me.frmManager = CType(USPPBManager, UserControl)
                    Me.GridEX1 = USPPBManager.grdHeader
                Case "btnShowReturningGON"
                    Dim UGonRBack As New GonReturnedBackManager()
                    UGonRBack.frmParent = Me
                    UGonRBack.CMain = Me.CMain
                    Me.SFM = StateFillingMCB.Filling
                    Me.cmbDistributor.Value = Nothing : Me.cmbDistributor.Text = ""
                    Me.SFM = StateFillingMCB.HasFilled
                    Me.AddControl(UGonRBack)
                    Me.frmManager = CType(UGonRBack, UserControl)
                    Me.GridEX1 = UGonRBack.GridEX1
                Case "btnExport"
                    If Not IsNothing(Me.GridEX1) Then
                        Me.SaveFileDialog1.OverwritePrompt = True
                        Me.SaveFileDialog1.DefaultExt = ".xls"
                        Me.SaveFileDialog1.Filter = "All Files|*.*"
                        Me.SaveFileDialog1.InitialDirectory = "C:\"
                        If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                            Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                            Me.GridEXExporter1.GridEX = Me.GridEX1
                            Me.GridEXExporter1.SheetName = "OA_BRANDPACK"
                            Me.GridEXExporter1.Export(FS)
                            FS.Close()
                            MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Case "btnRefresh"
                    Me.MustReloadData = True
                    If Not IsNothing(Me.frmManager) Then
                        If TypeOf (Me.frmManager) Is SPPBManager Or TypeOf (Me.frmManager) Is GonReturnedBackManager Then
                            If TypeOf (Me.frmManager) Is SPPBManager Then
                                'RaiseEvent ComboboxValue_Changed(Me.cmbDistributor, New EventArgs())
                                'If Me.MustReloadData Then
                                'End If
                                'If Not IsNothing(Me.cmbDistributor.Value) Then
                                'Else
                                '    RaiseEvent ComboboxValue_Changed(Me.cmbDistributor, New EventArgs())
                                'End If
                                RaiseEvent ShowSPPBData(True)
                            Else
                                'If Me.MustReloadData Then
                                RaiseEvent ComboboxValue_Changed(Me.cmbDistributor, New EventArgs())
                            End If
                        End If
                    End If
                Case "btnEditSPPB"
                    Select Case Me.frmManager.Name
                        Case "SPPBManager"
                            Me.EditSPPB()
                        Case "GonReturnedBackManager"
                            Me.EditGONReturn()
                    End Select
                Case "btnCurrentSelection"
                    If Me.GridEX1.Name = "grdHeader" Then
                        If Me.GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
                            Me.ShowMessageInfo("Please select SPPB Data")
                        Else
                            'show crystal report print SPPB
                        End If
                    End If
                Case "btnPrintcustoms"

            End Select
            Me.MustReloadData = False
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
            'Me.MustReloadData = False
        Finally
            Me.SFM = StateFillingMCB.HasFilled : Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function CreateOrReCreateTblGON() As DataTable
        Dim tblGON As New DataTable("GON_DETAIL_INFO")
        Dim colGDID As New DataColumn("GON_DETAIL_ID", Type.GetType("System.String"))
        colGDID.AllowDBNull = False
        colGDID.Unique = True

        Dim colGONNO As New DataColumn("GON_HEADER_ID", Type.GetType("System.String"))
        colGONNO.AllowDBNull = False
        Dim colSPPBBrandpackID As New DataColumn("SPPB_BRANDPACK_ID", Type.GetType("System.String"))
        colSPPBBrandpackID.AllowDBNull = False
        Dim colBRANDPACKID As New DataColumn("BRANDPACK_ID", Type.GetType("System.String"))
        colBRANDPACKID.AllowDBNull = False
        Dim colBrandPackName As New DataColumn("BRANDPACK_NAME", Type.GetType("System.String"))
        colBrandPackName.AllowDBNull = False
        Dim colGOnQty As New DataColumn("GON_QTY", Type.GetType("System.Decimal"))
        colGOnQty.AllowDBNull = False
        colGOnQty.DefaultValue = 0
        Dim colIsOpen As New DataColumn("IsOPen", Type.GetType("System.Boolean"))
        colIsOpen.AllowDBNull = False
        colIsOpen.DefaultValue = False

        Dim colBatchNo As New DataColumn("BatchNo", Type.GetType("System.String"))
        colBatchNo.AllowDBNull = True
        colBatchNo.DefaultValue = DBNull.Value

        Dim colUOfMeasure As New DataColumn("UnitOfMeasure", Type.GetType("System.String"))
        colUOfMeasure.DefaultValue = ""

        Dim colUnit1 As New DataColumn("UNIT1", Type.GetType("System.String"))
        'colUnit1.AllowDBNull = False

        Dim colVO11 As New DataColumn("VOL1", Type.GetType("System.String"))
        'colVO11.AllowDBNull = False

        Dim colUnit2 As New DataColumn("UNIT2", Type.GetType("System.String"))
        'colUnit2.AllowDBNull = False
        Dim colVO12 As New DataColumn("VOL2", Type.GetType("System.String"))
        'colVO12.AllowDBNull = False

        Dim colIsCompleted As New DataColumn("IsCompleted", Type.GetType("System.Boolean"))
        colIsCompleted.AllowDBNull = False
        colIsCompleted.DefaultValue = False

        Dim colIsUpdatedBySystem As New DataColumn("IsUpdatedBySystem", Type.GetType("System.Boolean"))
        colIsUpdatedBySystem.AllowDBNull = False
        colIsUpdatedBySystem.DefaultValue = False

        Dim colCreatedBy As New DataColumn("CreatedBy", Type.GetType("System.String"))
        colCreatedBy.AllowDBNull = False
        colCreatedBy.DefaultValue = NufarmBussinesRules.User.UserLogin.UserName
        Dim colCreatedDate As New DataColumn("CreatedDate", Type.GetType("System.DateTime"))
        colCreatedDate.AllowDBNull = False
        colCreatedDate.DefaultValue = NufarmBussinesRules.SharedClass.ServerDate
        Dim ColModifiedBy As New DataColumn("ModifiedBy", Type.GetType("System.String"))
        Dim colModifiedDate As New DataColumn("ModifiedDate", Type.GetType("System.DateTime"))
        tblGON.Columns.AddRange(New DataColumn() {colGDID, colGONNO, colSPPBBrandpackID, colBRANDPACKID, colBrandPackName, colGOnQty, _
        colIsOpen, colBatchNo, colUOfMeasure, colUnit1, colVO11, colUnit2, colVO12, colIsCompleted, colIsUpdatedBySystem, colCreatedBy, colCreatedDate, ColModifiedBy, colModifiedDate})

        Dim Key(1) As DataColumn
        Key(0) = colGDID
        tblGON.PrimaryKey = Key
        'DataColumn[] Key = new DataColumn[1]; DataColumn colCodeApp = new DataColumn("CodeApp", typeof(string));
        '        tblAchHeader.Columns.Add(colCodeApp); Key[0] = colCodeApp;
        '        tblAchHeader.PrimaryKey = Key;
        tblGON.AcceptChanges()
        Return tblGON
    End Function
    Private Sub EditGONReturn()
        Dim UGOnBack As New GONReceivedBack()
        With UGOnBack
            .frmParent = Me : Me.MustReloadData = False
            .mode = NufarmBussinesRules.common.Helper.SaveMode.Update
            .dtPicSPPBBUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
            .dtPicSPPBFrom.Value = .dtPicSPPBFrom.Value.AddDays(-92)
            Dim StartDate As DateTime = .dtPicSPPBFrom.Value
            Dim EndDate As DateTime = .dtPicSPPBBUntil.Value
            .objStartDate = StartDate.ToShortDateString()
            .objEndDate = EndDate.ToShortDateString()
            .dtPicSPPBFrom.Text = "" : .dtPicSPPBBUntil.Text = ""
            .DistributorID = Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()
            .GON_NO = Me.GridEX1.GetValue("GON_NO").ToString()
            .SPPB_NO = Me.GridEX1.GetValue("SPPB_NO").ToString()
            '.mcbGONNO.Value = Me.GridEX1.GetValue("GON_NO").ToString()
            .dtReceivedBackHere.Value = Convert.ToDateTime(Me.GridEX1.GetValue("RETURNED_DATE"))
            .GOnReceiver = Me.GridEX1.GetValue("ID_RECEIVER").ToString()
            .dtPicSPPBFrom.ReadOnly = True : .dtPicSPPBBUntil.ReadOnly = True
            .chkGonSigned.Checked = CBool(Me.GridEX1.GetValue("IS_SIGNED"))
            .chkGonStamped.Checked = CBool(Me.GridEX1.GetValue("IS_STAMPED"))
            If IsDBNull(Me.GridEX1.GetValue("REMARK")) Or IsNothing(Me.GridEX1.GetValue("REMARK")) Then
                .txtDescriptions.Text = ""
            Else
                .txtDescriptions.Text = Me.GridEX1.GetValue("REMARK").ToString()
            End If
            .ShowInTaskbar = False : .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog(Me)
            If Not IsNothing(Me.frmManager) Then
                If Me.MustReloadData = True Then
                    RaiseEvent ComboboxValue_Changed(Me.cmbDistributor, New EventArgs())
                End If
            End If
            'If Not IsNothing(Me.cmbDistributor.Value) Then
            'End If
        End With
    End Sub
  
    Private Sub EditSPPB()
        If IsNothing(Me.frmManager) Then : Return : End If
        If IsNothing(Me.GridEX1) Then : Return : End If
        If IsNothing(Me.GridEX1.DataSource) Then : Return : End If
        If Me.GridEX1.RecordCount <= 0 Then : Return : End If
        If IsNothing(Me.GridEX1.SelectedItems) Then : Return : End If
        If Me.GridEX1.GetRow().RowType <> Janus.Windows.GridEX.RowType.Record Then : Return : End If
        If Not TypeOf (Me.frmManager) Is SPPBManager Then : Return : End If
        Dim UCSPPBManager As SPPBManager = CType(Me.frmManager, SPPBManager)
        Dim PONumber As String = Me.GridEX1.GetValue("PO_REF_NO")
        Dim SGE As New SPPBEntryGON()
        Me.MustReloadData = False
        With SGE
            .frmParent = Me
            .OpenerManager = Me.frmManager
            .Mode = SaveMode.Update
            .DataToEdit = SPPBEntryGON.EditData.SPPB
            .PO_REF_NO = PONumber
            .mcbOA_REF_NO.Value = PONumber
            .dtPicSPPBDate.Value = Convert.ToDateTime(Me.GridEX1.GetValue("SPPB_DATE"))
            .SPPB_NO = Me.GridEX1.GetValue("SPPB_NO")
            .txtSPPBNO.Text = .SPPB_NO
            Dim DS As New DataSet("DSSPPB_GON") : DS.Clear()
            'fiil tblPO
            Dim tblPO As DataTable = Me.clsSPPBGONEntry.getPO(.PO_REF_NO, SaveMode.Update, False)
            .dvPO = tblPO.DefaultView()
            .dtPicSPPBDate.MinDate = tblPO.Rows(0)("PO_DATE")
            Dim tblSPPBrandPack As DataTable = Me.clsSPPBGONEntry.getSPPBBrandPack(.SPPB_NO, .PO_REF_NO, False, False)
            Dim tblGOn As DataTable = Me.clsSPPBGONEntry.getGOnData(.SPPB_NO, False)
            Dim ObjSPPBHeader As New Nufarm.Domain.SPPBHeader()
            With ObjSPPBHeader
                .SPPBNO = .SPPBNO
                .SPPBDate = Convert.ToDateTime(Me.GridEX1.GetValue("SPPB_DATE"))
                .PONumber = PONumber
            End With
            .OSPPBHeader = ObjSPPBHeader
            If tblGOn.Rows.Count > 0 Then
                Dim ListGON As New List(Of String)
                For Each row As DataRow In tblGOn.Rows
                    Dim GONHeaderID As String = row.Item("GON_HEADER_ID").ToString()
                    Dim GONNUmber As String = GONHeaderID.Remove(0, GONHeaderID.IndexOf("|") + 1)
                    'Dim GONNUmber As String = GonHeaderID.Substring(GonHeaderID.IndexOf("|"),
                    If Not ListGON.Contains(GONNUmber) Then
                        ListGON.Add(GONNUmber)
                    End If
                Next
                If ListGON.Count > 0 And ListGON.Count <= 1 Then
                    .GON_NO = ListGON(0)

                    'chek GON_NO IF There is only one row
                    'if only one row set the value of txtgon no,area,,gon_date,transporter,product,
                    ''status if all status in sppbBrandPack is equal
                    'get gon Description
                    'get gon_NO,gon_date,Remark,status
                    Dim status As String = ""
                    .OGONHeader = Me.clsSPPBGONEntry.getGONDescriptionBySPPB(.SPPB_NO, status, False)
                    .txtGONNO.Text = .OGONHeader.GON_NO
                    .Text = "SPPB " & .SPPB_NO & ", GON " & .OGONHeader.GON_NO
                    .dtPicGONDate.Value = .OGONHeader.GON_DATE
                    .cmbStatusSPPB.Text = status
                    .txtDriverTrans.Text = .OGONHeader.DriverTrans
                    .txtPolice_no_Trans.Text = .OGONHeader.PoliceNoTrans
                    '.DVMConversiProduct = Me.clsSPPBGONEntry.getProdConvertion(SaveMode.Update, True)
                    .txtShipTo.Text = .OGONHeader.ShipTo
                    Dim TransporterName As Object = Nothing, GONArea As Object = Nothing, ListSPPBBrandPack As New List(Of String)
                    Dim DVGONManager As DataView = Nothing

                    If Not IsNothing(UCSPPBManager.grdDetail.DataSource) Then
                        DVGONManager = CType(UCSPPBManager.grdDetail.DataSource, DataView)
                        Dim OrowFilter As String = DVGONManager.RowFilter
                        DVGONManager.RowFilter = ""
                        Dim DVDummy As DataView = DVGONManager.ToTable().Copy().DefaultView()
                        DVDummy.Sort = "GON_NO DESC"
                        Dim Index As String = DVDummy.Find(.GON_NO)
                        If Index <> -1 Then
                            TransporterName = IIf((Not IsNothing(DVDummy(Index)("TRANSPORTER_NAME")) And Not IsDBNull(DVDummy(Index)("TRANSPORTER_NAME"))), DVDummy(Index)("TRANSPORTER_NAME").ToString(), "")
                            GONArea = IIf((Not IsNothing(DVDummy(Index)("AREA")) And Not IsDBNull(DVDummy(Index)("AREA"))), DVDummy(Index)("AREA").ToString(), "")
                        End If
                        DVDummy.RowFilter = "SPPB_NO = '" & .SPPB_NO & "'"
                        'tblGOn.Rows.Clear() : tblGOn.AcceptChanges()
                        tblGOn = Me.CreateOrReCreateTblGON()
                        For i As Integer = 0 To DVDummy.Count - 1
                            ''isi table gon DENGAN DVDummy
                            Dim drv As DataRow = tblGOn.NewRow()
                            drv.BeginEdit()
                            drv("GON_DETAIL_ID") = DVDummy(i)("GON_DETAIL_ID")
                            drv("GON_HEADER_ID") = DVDummy(i)("GON_HEADER_ID")
                            drv("SPPB_BRANDPACK_ID") = DVDummy(i)("SPPB_BRANDPACK_ID")
                            drv("BRANDPACK_ID") = DVDummy(i)("BRANDPACK_ID")
                            drv("BRANDPACK_NAME") = DVDummy(i)("BRANDPACK_NAME")
                            drv("GON_QTY") = DVDummy(i)("GON_QTY")
                            drv("UnitOfMeasure") = DVDummy(i)("UnitOfMeasure")
                            drv("UNIT1") = DVDummy(i)("UNIT1")
                            drv("VOL1") = DVDummy(i)("VOL1")
                            drv("UNIT2") = DVDummy(i)("UNIT2")
                            drv("VOL2") = DVDummy(i)("VOL2")
                            drv("BatchNo") = DVDummy(i)("BatchNo")
                            drv("IsOPen") = True
                            drv("IsCompleted") = DVDummy(i)("IsCompleted")
                            drv("IsUpdatedBySystem") = False
                            drv("CreatedBy") = DVDummy(i)("CreatedBy")
                            drv("CreatedDate") = DVDummy(i)("CreatedDate")
                            drv("ModifiedBy") = String.Empty
                            drv("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
                            drv.EndEdit()
                            tblGOn.Rows.Add(drv)
                        Next
                        tblGOn.AcceptChanges()
                        If DVDummy.Count = 1 Then
                            ListSPPBBrandPack.Add(DVDummy(0)("SPPB_BRANDPACK_ID").ToString())
                        ElseIf DVDummy.Count > 1 Then
                            For i As Integer = 0 To DVDummy.Count - 1
                                If Not ListSPPBBrandPack.Contains(DVDummy(i)("SPPB_BRANDPACK_ID").ToString()) Then
                                    ListSPPBBrandPack.Add(DVDummy(i)("SPPB_BRANDPACK_ID").ToString())
                                End If
                            Next
                        End If
                        Dim SPPBrandPackValues(ListSPPBBrandPack.Count - 1) As Object
                        For i As Integer = 0 To ListSPPBBrandPack.Count - 1
                            SPPBrandPackValues(i) = ListSPPBBrandPack(i)
                        Next
                        .SppBrandPackValues = SPPBrandPackValues
                        If OrowFilter <> "" Then
                            DVGONManager.RowFilter = OrowFilter
                        End If
                    End If
                    '= Me.clsSPPBGONEntry.getTransporterByGONNO(.GON_NO, False)
                    Dim tblTrans As DataTable = Nothing, tblArea As DataTable = Nothing
                    tblTrans = Me.clsSPPBGONEntry.getTransporter(String.Empty, SaveMode.Insert, False)

                    .DVTransporter = tblTrans.DefaultView()
                    tblArea = Me.clsSPPBGONEntry.getAreaGon(String.Empty, SaveMode.Insert, False)
                    .cmdWarhouse.SelectedValue = .OGONHeader.WarhouseCode
                    .DVArea = tblArea.DefaultView()
                    ''get checkedProduct
                    .DVProduct = Me.clsSPPBGONEntry.GetProduct(.SPPB_NO, Nothing, tblSPPBrandPack, False)
                    .txtSPPBNO.ReadOnly = True
                    .txtGONNO.ReadOnly = True
                    .chkProduct.ReadOnly = True
                    .cmbStatusSPPB.ReadOnly = True
                    'get min and max GON_Date
                    '.dtPicGONDate.MaxDate = .OGONHeader.GON_DATE
                    .dtPicGONDate.MinDate = .OSPPBHeader.SPPBDate
                    .dtPicSPPBDate.MaxDate = .OGONHeader.GON_DATE
                    .TransporterName = IIf((Not IsNothing(TransporterName)), TransporterName.ToString(), "")
                    .AreaName = IIf((Not IsNothing(GONArea)), GONArea.ToString(), "")
                    '.mcbTransporter.Value = .OGONHeader.GT_ID
                    '.mcbGonArea.Value = .OGONHeader.GON_ID_AREA
                    .grdSPPB.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    .grdSPPB.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    .grdGon.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    .grdGon.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                    '.cmdWarhouse.SelectedValue = 
                ElseIf ListGON.Count > 1 Then
                    .txtGONNO.Text = ""
                    .dtPicGONDate.Text = ""
                    .mcbTransporter.Text = ""
                    .mcbGonArea.Text = ""
                    .cmbStatusSPPB.Text = ""
                    '.txtRemark.Text = ""
                    .txtPolice_no_Trans.Text = ""
                    .txtDriverTrans.Text = ""
                    .TransporterName = ""
                    .AreaName = ""
                    .dtPicGONDate.ReadOnly = True
                    .txtGONNO.ReadOnly = True
                    .mcbTransporter.ReadOnly = True
                    .mcbGonArea.ReadOnly = True
                    .cmbStatusSPPB.ReadOnly = True
                    .chkProduct.ReadOnly = True
                    .grdGon.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    .grdGon.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True

                    .txtSPPBNO.ReadOnly = True
                    .dtPicSPPBDate.ReadOnly = True
                    .grdSPPB.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    .grdSPPB.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                    'bindmulticolumnCOmbo di form
                End If
            Else
                .txtGONNO.Text = ""
                .dtPicGONDate.Text = ""
                .mcbTransporter.Text = ""
                .mcbGonArea.Text = ""
                .cmbStatusSPPB.Text = ""
                '.txtRemark.Text = ""
                .txtPolice_no_Trans.Text = ""
                .txtDriverTrans.Text = ""
                .TransporterName = ""
                .AreaName = ""
                .dtPicGONDate.ReadOnly = True
                .txtGONNO.ReadOnly = True
                .mcbTransporter.ReadOnly = True
                .mcbGonArea.ReadOnly = True
                .cmbStatusSPPB.ReadOnly = True
                .chkProduct.ReadOnly = True

                .txtSPPBNO.ReadOnly = True
                'get maxDate 
                Dim PODate As Object = Me.GridEX1.GetValue("PO_REF_DATE")
                .dtPicSPPBDate.MinDate = Convert.ToDateTime(PODate)
                .grdSPPB.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                .grdSPPB.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                .grdGon.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                .grdGon.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            End If

            '.lblDistributor.Text = .OSPPBHeader.d
            .lblPODate.Text = String.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(.OSPPBHeader.SPPBDate))
            .lblSalesPerson.Text = Me.clsSPPBGONEntry.getSalesPerson(.PO_REF_NO, False)
            .lblDistributor.Text = Me.GridEX1.GetValue("DISTRIBUTOR_NAME")
            'set txtGON if exists
            .btnAddGon.Enabled = True

            'get DS
            DS.Tables.Add(tblSPPBrandPack)
            DS.Tables.Add(tblGOn)
            DS.AcceptChanges()
            .DS = DS
            .OpenerManager = UCSPPBManager
            .Show()
            '.ShowDialog()
            'If Me.MustReloadData Then
            '    If Not IsNothing(Me.frmManager) Then
            '        If TypeOf (frmManager) Is SPPBManager Then
            '            RaiseEvent ShowSPPBData(True)
            '        End If
            '    End If
            'End If
        End With
        '============================OLD PROCESS==============================================================
        'If Not IsNothing(Me.frmManager) Then
        '    If Not IsNothing(Me.GridEX1) Then
        '        If Not IsNothing(Me.GridEX1.DataSource) Then
        '            If Me.GridEX1.RecordCount > 0 Then
        '                If Me.GridEX1.SelectedItems.Count > 0 Then
        '                    If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
        '                        If TypeOf (Me.frmManager) Is GonReturnedBackManager Then
        '                            'munculkan inputan SOPHandlingPO
        '                            Dim UGOnBack As New GONReceivedBack()
        '                            With UGOnBack
        '                                .frmParent = Me : Me.MustReloadData = False
        '                                .mode = NufarmBussinesRules.common.Helper.SaveMode.Update
        '                                .dtPicSPPBBUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
        '                                .dtPicSPPBFrom.Value = .dtPicSPPBFrom.Value.AddDays(-92)
        '                                Dim StartDate As DateTime = .dtPicSPPBFrom.Value
        '                                Dim EndDate As DateTime = .dtPicSPPBBUntil.Value
        '                                .objStartDate = StartDate.ToShortDateString()
        '                                .objEndDate = EndDate.ToShortDateString()
        '                                .dtPicSPPBFrom.Text = "" : .dtPicSPPBBUntil.Text = ""
        '                                .DistributorID = Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()
        '                                .GON_NO = Me.GridEX1.GetValue("GON_NO").ToString()
        '                                .SPPB_NO = Me.GridEX1.GetValue("SPPB_NO").ToString()
        '                                '.mcbGONNO.Value = Me.GridEX1.GetValue("GON_NO").ToString()
        '                                .dtReceivedBackHere.Value = Convert.ToDateTime(Me.GridEX1.GetValue("RETURNED_DATE"))
        '                                .GOnReceiver = Me.GridEX1.GetValue("ID_RECEIVER").ToString()
        '                                .dtPicSPPBFrom.ReadOnly = True : .dtPicSPPBBUntil.ReadOnly = True
        '                                .chkGonSigned.Checked = CBool(Me.GridEX1.GetValue("IS_SIGNED"))
        '                                .chkGonStamped.Checked = CBool(Me.GridEX1.GetValue("IS_STAMPED"))
        '                                If IsDBNull(Me.GridEX1.GetValue("REMARK")) Or IsNothing(Me.GridEX1.GetValue("REMARK")) Then
        '                                    .txtDescriptions.Text = ""
        '                                Else
        '                                    .txtDescriptions.Text = Me.GridEX1.GetValue("REMARK").ToString()
        '                                End If
        '                                .ShowInTaskbar = False : .StartPosition = FormStartPosition.CenterScreen
        '                                .ShowDialog(Me)
        '                                If Not IsNothing(Me.frmManager) Then
        '                                    If Me.MustReloadData = True Then
        '                                        RaiseEvent ComboboxValue_Changed(Me.cmbDistributor, New EventArgs())
        '                                    End If
        '                                End If
        '                                'If Not IsNothing(Me.cmbDistributor.Value) Then
        '                                'End If
        '                            End With
        '                        ElseIf TypeOf (Me.frmManager) Is SPPBManager Then
        '                            'Dim frmSPPBManager As SPPBManager = CType(Me.frmManager, SPPBManager)
        '                            Dim frmSPPBEntry As New SPPBEntry()
        '                            With frmSPPBEntry
        '                                .frmParent = Me : Me.MustReloadData = False
        '                                .SaveMode = SPPBEntry.ModeSave.Update
        '                                .InitializeData()
        '                                Dim RELEASE_DATE As Object = DBNull.Value
        '                                If Me.GridEX1.GetValue("RELEASE_DATE") Is DBNull.Value Then
        '                                ElseIf IsNothing(Me.GridEX1.GetValue("RELEASE_DATE")) Then
        '                                Else
        '                                    RELEASE_DATE = Convert.ToDateTime(Me.GridEX1.GetValue("RELEASE_DATE"))
        '                                End If
        '                                .ReleaseDate = RELEASE_DATE
        '                                .mcbDistributor.Value = Me.GridEX1.GetValue("DISTRIBUTOR_ID")
        '                                .mcbOA_REF_NO.Value = Me.GridEX1.GetValue("OA_REF_NO")
        '                                '.txtSPPBNo.Text = Me.GridEX1.GetValue("SPPB_NO")
        '                                .txtSPPBNo.Enabled = False
        '                                .mcbOA_REF_NO.ReadOnly = True
        '                                .mcbDistributor.ReadOnly = True
        '                                '.dtPicSPPBDate.Value = CDate(Me.GridEX1.GetValue("SPPB_DATE")).ToShortDateString()
        '                                .StartPosition = FormStartPosition.CenterParent
        '                                .ShowDialog(Me)
        '                                If Not IsNothing(Me.frmManager) Then
        '                                    If Me.MustReloadData = True Then
        '                                        RaiseEvent ComboboxValue_Changed(Me.cmbDistributor, New EventArgs())
        '                                    End If
        '                                End If
        '                            End With
        '                        End If
        '                        'isRowSelected = True
        '                    End If
        '                End If
        '            End If
        '        End If
        '    End If
        'End If
        '===========================================================================

    End Sub
    Friend Sub EditGON()
        'biar gak pusing coding
        'semua properti di set di parent saja 
        'edit gon hanya mengedit tanggal gon lihat max gon sebelum dan sesudahnya,transporter,area,gon_ty di grid,product,go_no,sppb_no,sppb_date,sppbBrandpack(grid) matikan
        'btnAddnew hidupkan bila mungkin akan menambah gon
        '

        'save mode
        'frmParent
        'DataToEdit As EditData = EditData.None ''to define what data to edit
        'GON_NO
        'SPPB_NO
        'OSPPBHeader As New Nufarm.Domain.SPPBHeader()
        'OGONHeader As New Nufarm.Domain.GONHeader()
        '    Friend Property tblMasterGON() As DataTable
        Dim UCSPPBManager As SPPBManager = CType(Me.frmManager, SPPBManager)
        If IsNothing(UCSPPBManager.grdDetail.DataSource) Then
            Return
        End If
        If (UCSPPBManager.grdHeader.RecordCount <= 0) Then
            Return
        End If
        Dim GONNO As Object = UCSPPBManager.grdDetail.GetValue("GON_NO")
        If IsNothing(GONNO) Or IsDBNull(GONNO) Then
            Return
        End If
        If UCSPPBManager.grdDetail.GetRow().RowType <> Janus.Windows.GridEX.RowType.Record Then
            Return
        End If

        Dim PONumber As String = UCSPPBManager.grdDetail.GetValue("PO_REF_NO").ToString()
        Dim SGE As New SPPBEntryGON()
        Me.MustReloadData = False
        Dim TransID As Object = Nothing, GONIDArea As Object = Nothing, ListSPPBBrandPack As New List(Of String)
        Dim DVGONManager As DataView = Nothing
        Dim SPPBBrandPackID As Object = UCSPPBManager.grdDetail.GetValue("SPPB_BRANDPACK_ID")
        With SGE
            .frmParent = Me
            .OpenerManager = UCSPPBManager
            .Mode = SaveMode.Update
            .DataToEdit = SPPBEntryGON.EditData.GON
            .PO_REF_NO = PONumber
            .mcbOA_REF_NO.Value = PONumber
            Dim SPPBDate As Object = Nothing
            If UCSPPBManager.grdDetail.RootTable.Columns.Contains("SPPB_DATE") Then
                SPPBDate = Convert.ToDateTime(UCSPPBManager.grdDetail.GetValue("SPPB_DATE"))
            Else
                'get sppb_date
                Dim DVDummySBManager As DataView = CType(UCSPPBManager.grdHeader.DataSource, DataView).ToTable().Copy().DefaultView()
                DVDummySBManager.RowFilter = "SPPB_BRANDPACK_ID ='" & SPPBBrandPackID & "'"
                SPPBDate = Convert.ToDateTime(DVDummySBManager(0)("SPPB_DATE"))
            End If
            .dtPicSPPBDate.Value = SPPBDate

            .SPPB_NO = UCSPPBManager.grdDetail.GetValue("SPPB_NO").ToString()
            .txtSPPBNO.Text = .SPPB_NO
            Dim DS As New DataSet("DSSPPB_GON") : DS.Clear()
            'fiil tblPO
            Dim tblPO As DataTable = Me.clsSPPBGONEntry.getPO(.PO_REF_NO, SaveMode.Update, False)
            .dvPO = tblPO.DefaultView()
            .dtPicSPPBDate.MinDate = tblPO.Rows(0)("PO_DATE")
            Dim tblSPPBrandPack As DataTable = Me.clsSPPBGONEntry.getSPPBBrandPack(.SPPB_NO, .PO_REF_NO, False, False)
            Dim tblGOn As DataTable = Me.clsSPPBGONEntry.getGOnData(.SPPB_NO, False)
            Dim ObjSPPBHeader As New Nufarm.Domain.SPPBHeader()
            With ObjSPPBHeader
                .SPPBNO = UCSPPBManager.grdDetail.GetValue("SPPB_NO").ToString()
                .SPPBDate = SPPBDate
                .PONumber = PONumber
            End With
            .OSPPBHeader = ObjSPPBHeader
            .GON_NO = GONNO
            .Text = "SPPB " & .SPPB_NO & ", GON " & .GON_NO
            'chek GON_NO IF There is only one row
            'if only one row set the value of txtgon no,area,,gon_date,transporter,product,
            ''status if all status in sppbBrandPack is equal
            'get gon Description
            'get gon_NO,gon_date,Remark,status
            Dim status As String = Me.GridEX1.GetValue("STATUS").ToString()
            .OGONHeader = Me.clsSPPBGONEntry.getGONDescriptionBySPPB(.SPPB_NO, status, False)
            .txtGONNO.Text = .OGONHeader.GON_NO
            .dtPicGONDate.Value = .OGONHeader.GON_DATE
            .cmbStatusSPPB.Text = status
            .txtPolice_no_Trans.Text = .OGONHeader.PoliceNoTrans
            .txtDriverTrans.Text = .OGONHeader.DriverTrans
            .cmdWarhouse.SelectedValue = .OGONHeader.WarhouseCode
            '.txtRemark.Text = .OGONHeader.DescriptionApp
            .txtShipTo.Text = .OGONHeader.ShipTo
            DVGONManager = CType(UCSPPBManager.grdDetail.DataSource, DataView)
            Dim OrowFilter As String = DVGONManager.RowFilter
            DVGONManager.RowFilter = ""
            Dim DVDummy As DataView = DVGONManager.ToTable().Copy().DefaultView()

            DVDummy.Sort = "GON_NO DESC"
            Dim Index As String = DVDummy.Find(.GON_NO)
            If Index <> -1 Then
                TransID = IIf((Not IsNothing(DVDummy(Index)("GT_ID")) And Not IsDBNull(DVDummy(Index)("GT_ID"))), DVDummy(Index)("GT_ID").ToString(), "")
                GONIDArea = IIf((Not IsNothing(DVDummy(Index)("GON_ID_AREA")) And Not IsDBNull(DVDummy(Index)("GON_ID_AREA"))), DVDummy(Index)("GON_ID_AREA").ToString(), "")
            End If
            DVDummy.RowFilter = "GON_NO = '" & .GON_NO & "'"
            'tblGOn.Rows.Clear() : tblGOn.AcceptChanges()
            tblGOn = Me.CreateOrReCreateTblGON()
            For i As Integer = 0 To DVDummy.Count - 1
                ''isi table gon DENGAN DVDummy
                Dim drv As DataRow = tblGOn.NewRow()
                drv.BeginEdit()
                drv("GON_DETAIL_ID") = DVDummy(i)("GON_DETAIL_ID")
                drv("GON_HEADER_ID") = DVDummy(i)("GON_HEADER_ID")
                drv("SPPB_BRANDPACK_ID") = DVDummy(i)("SPPB_BRANDPACK_ID")
                drv("BRANDPACK_ID") = DVDummy(i)("BRANDPACK_ID")
                drv("BRANDPACK_NAME") = DVDummy(i)("BRANDPACK_NAME")
                drv("GON_QTY") = DVDummy(i)("GON_QTY")
                drv("IsOPen") = True
                drv("BatchNo") = IIf(DVDummy.Table.Columns.Contains("BatchNo"), DVDummy(i)("BatchNo"), "")
                drv("UnitOfMeasure") = IIf(DVDummy.Table.Columns.Contains("UnitOfMeasure"), DVDummy(i)("UnitOfMeasure"), "") ' DVDummy(i)("UnitOfMeasure")
                drv("UNIT1") = IIf(DVDummy.Table.Columns.Contains("UNIT1"), DVDummy(i)("UNIT1"), "")
                drv("VOL1") = IIf(DVDummy.Table.Columns.Contains("VOL1"), DVDummy(i)("VOL1"), "")
                drv("UNIT2") = IIf(DVDummy.Table.Columns.Contains("UNIT2"), DVDummy(i)("UNIT2"), "")
                drv("VOL2") = IIf(DVDummy.Table.Columns.Contains("VOL2"), DVDummy(i)("VOL2"), "")
                drv("IsCompleted") = DVDummy(i)("IsCompleted")
                drv("IsUpdatedBySystem") = False
                drv("CreatedBy") = DVDummy(i)("CreatedBy")
                drv("CreatedDate") = DVDummy(i)("CreatedDate")
                drv("ModifiedBy") = String.Empty
                drv("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
                drv.EndEdit()
                tblGOn.Rows.Add(drv)
            Next
            tblGOn.AcceptChanges()
            If DVDummy.Count = 1 Then
                ListSPPBBrandPack.Add(DVDummy(0)("SPPB_BRANDPACK_ID").ToString())
            ElseIf DVDummy.Count > 1 Then
                For i As Integer = 0 To DVDummy.Count - 1
                    If Not ListSPPBBrandPack.Contains(DVDummy(i)("SPPB_BRANDPACK_ID").ToString()) Then
                        ListSPPBBrandPack.Add(DVDummy(i)("SPPB_BRANDPACK_ID").ToString())
                    End If
                Next
            End If
            Dim SPPBrandPackValues(ListSPPBBrandPack.Count - 1) As Object
            For i As Integer = 0 To ListSPPBBrandPack.Count - 1
                SPPBrandPackValues(i) = ListSPPBBrandPack(i)
            Next
            .SppBrandPackValues = SPPBrandPackValues
            If OrowFilter <> "" Then
                DVGONManager.RowFilter = OrowFilter
            End If

            Dim tblTrans As DataTable = Nothing, tblArea As DataTable = Nothing
            tblTrans = Me.clsSPPBGONEntry.getTransporter(String.Empty, SaveMode.Update, False)

            .DVTransporter = tblTrans.DefaultView()
            tblArea = Me.clsSPPBGONEntry.getAreaGon(String.Empty, SaveMode.Update, False)

            .DVArea = tblArea.DefaultView()
            ''get checkedProduct
            .DVProduct = Me.clsSPPBGONEntry.GetProduct(.SPPB_NO, Nothing, tblSPPBrandPack, False)
            .txtSPPBNO.ReadOnly = True
            .txtGONNO.ReadOnly = True
            .chkProduct.ReadOnly = True
            .cmbStatusSPPB.ReadOnly = False
            'get min and max GON_Date
            '.dtPicGONDate.MaxDate = .OGONHeader.GON_DATE
            .dtPicGONDate.MinDate = .OSPPBHeader.SPPBDate
            .dtPicSPPBDate.MaxDate = .OGONHeader.GON_DATE
            .TransporterName = IIf((Not IsNothing(TransID)), TransID.ToString(), "")
            .AreaName = IIf((Not IsNothing(GONIDArea)), GONIDArea.ToString(), "")
            .grdSPPB.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            .grdSPPB.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False

            .grdGon.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            .grdGon.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            .lblPODate.Text = String.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(.OSPPBHeader.SPPBDate))
            .lblSalesPerson.Text = Me.clsSPPBGONEntry.getSalesPerson(.PO_REF_NO, False)
            .lblDistributor.Text = UCSPPBManager.grdDetail.GetValue("DISTRIBUTOR_NAME").ToString()
            .btnAddGon.Enabled = False
            'get DS
            DS.Tables.Add(tblSPPBrandPack)
            DS.Tables.Add(tblGOn)
            DS.AcceptChanges()
            .DS = DS
            .OpenerManager = UCSPPBManager
            .Show()
            '.ShowDialog()
            'If Me.MustReloadData Then
            '    If Not IsNothing(Me.frmManager) Then
            '        If TypeOf (frmManager) Is SPPBManager Then
            '            RaiseEvent ShowSPPBData(True)
            '        End If
            '    End If
            'End If

        End With
    End Sub
    Private Sub showSPPBEntry()
        Dim frmSPPBEntryGON As New SPPBEntryGON()
        Dim UCSPPBManager As SPPBManager = CType(Me.frmManager, SPPBManager)
        With frmSPPBEntryGON
            .frmParent = Me
            .Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
            .OpenerManager = UCSPPBManager
            '.ShowInTaskbar = False
            .Owner = Me
            Me.NumberClick = Me.NumberClick + 1
            .Text = "SPPB 0" & Me.NumberClick.ToString()
            .Show()
            'If Me.MustReloadData Then
            '    If Not IsNothing(Me.frmManager) Then
            '        If TypeOf (frmManager) Is SPPBManager Then
            '            RaiseEvent ShowSPPBData()
            '        End If
            '    End If
            'End If
        End With
    End Sub

    Private Sub SPPB_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.OwnedForms.Length > 0 Then
            Me.ShowMessageError("Can not close parent form while SPPB Form is open")
            e.Cancel = True
        End If
    End Sub
    'Private Sub GridEX1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        If Not NufarmBussinesRules.User.UserLogin.IsAdmin Then
    '            If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.SPPB = True Then
    '                Return
    '            End If
    '        End If
    '        If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
    '            If CDec(Me.GridEX1.GetValue("SPPB_QTY")) > 0 Then
    '                If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
    '                    Return
    '                Else
    '                    Me.clsSPPBDetail.DeleteSPPPB_BrandPack(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString())
    '                    Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
    '                End If
    '            Else
    '                Return
    '            End If
    '            'Me.GridEX1.UpdateData()
    '            'Me.clsSPPBDetail.DeleteSPPPB_BrandPack(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString())
    '            'Me.GridEX1.Delete()
    '            'Me.GridEX1.UpdateData()

    '        End If

    '    Catch ex As Exception
    '        Me.ShowMessageInfo(ex.Message)
    '        Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub SPPB_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsSPPBDetail.CreateViewDistributorSPPB()
            Me.BindMulticolumnCombo(Me.clsSPPBDetail.ViewDistributorSPPB())
            Me.cmbDistributor.ReadOnly = True
        Catch ex As Exception
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_SPPB_Load")
        Finally
            Me.ReadAcces() : CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub btnSearchDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDistributor.btnClick
        Try
            If Me.cmbDistributor.ReadOnly Then : Return : End If
            Dim dtView As DataView = CType(Me.cmbDistributor.DataSource, DataView)
            dtView.RowFilter = "DISTRIBUTOR_NAME LIKE '%" & Me.cmbDistributor.Text & "%'"
            Me.BindMulticolumnCombo(dtView)
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnApply_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    'Private Sub ShowCustomLookUpSPPB()
    '    flUp = New LookUp()
    '    Dim tbl As DataTable = Nothing
    '    'ambil data untuk permulaan
    '    'check SPPB date di grid
    '    'jika kurang dari 3 bulan maka ambil data 3 bulan sampai sekarang
    '    'else startSPPB to current
    '    Dim SPPBDate As Object = Nothing
    '    Dim UCSPPBManager As SPPBManager = CType(Me.frmManager, SPPBManager)
    '    Dim DV As DataView = CType(UCSPPBManager.grdHeader.DataSource, DataView).ToTable().Copy().DefaultView()
    '    DV.Sort = "SPPB_DATE ASC"
    '    Dim oStartDate As Object = DV(0)("SPPB_DATE"), StartDate As Date = DateTime.Now()
    '    If Not IsNothing(oStartDate) And Not IsDBNull(oStartDate) Then
    '        StartDate = CDate(oStartDate)
    '    End If
    '    DV.Sort = "SPPB_DATE DESC"
    '    Dim oEndDate As Object = DV(0)("SPPB_DATE"), EndDate As Date = DateTime.Now()
    '    If Not IsNothing(oEndDate) And Not IsDBNull(oEndDate) Then
    '        EndDate = CDate(oEndDate)
    '    End If
    '    If StartDate.Equals(EndDate) Then
    '        StartDate = StartDate.AddMonths(-3)
    '    ElseIf DateDiff(DateInterval.Month, StartDate, EndDate) < 3 Then
    '        StartDate = EndDate.AddMonths(-3)
    '    End If
    '    tbl = Me.clsSPPBGONEntry.getSPPBBrandPack(String.Empty, StartDate, EndDate)
    '    If Not IsNothing(tbl) Then
    '        DV = tbl.DefaultView()
    '    End If
    '    With flUp
    '        .Grid.SetDataBinding(DV, "")

    '        .Grid.RetrieveStructure()
    '        .Grid.AutoSizeColumns()
    '    End With
    'End Sub

End Class
