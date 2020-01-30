Imports Nufarm.Domain
Public Class AjdustmentPKD
    Private m_clsAdj As NufarmBussinesRules.Program.Addjustment
    Private IsloadingRow As Boolean = False
    Private dtDistributorGroup As DataTable = Nothing
    Private dtDistributor As DataTable = Nothing
    Private dtBrandPack As DataTable = Nothing
    Private WithEvents frmEntry As frmEntryAdjPKD
    Friend CMain As Main = Nothing
    Private frmRepDetail As ReportTransactionAdjustment = Nothing
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AdjustmentPKD Then
                GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            Else
                GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AdjustmentPKD Then
                Me.btnAddNew.Visible = False
            Else
                Me.btnAddNew.Visible = True
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AdjustmentPKD Then
                Me.btnEditRow.Visible = False
            Else
                Me.btnEditRow.Visible = True
            End If
        End If
    End Sub
    Private ReadOnly Property ClsAdj() As NufarmBussinesRules.Program.Addjustment
        Get
            If m_clsAdj Is Nothing Then
                Me.m_clsAdj = New NufarmBussinesRules.Program.Addjustment()
            End If
            Return m_clsAdj
        End Get
    End Property
    Private Sub ExportData(ByVal grid As Janus.Windows.GridEX.GridEX)
        Dim SV As New SaveFileDialog()
        With SV
            .Title = "Define the location File"
            .InitialDirectory = System.Environment.SpecialFolder.MyDocuments
            .RestoreDirectory = True
            .OverwritePrompt = True
            .DefaultExt = ".xls"
            .FileName = "All Files|*.*"
        End With
        If SV.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim FS As New System.IO.FileStream(SV.FileName, IO.FileMode.Create)
            Dim Exporter As New Janus.Windows.GridEX.Export.GridEXExporter()
            Exporter.GridEX = grid
            Exporter.Export(FS)
            FS.Close()
            MessageBox.Show("Data Exported to " & SV.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub GetData()
        If Not IsloadingRow Then : IsloadingRow = True : End If
        Dim DistributorID As String = ""
        'IIf((Me.mcbDistributor.Text = ""), "", Me.mcbDistributor.Value.ToString())
        If Me.mcbDistributor.Text <> "" Then : DistributorID = Me.mcbDistributor.Value.ToString() : End If
        If IsNothing(Me.dtDistributor) Then
            Me.dtDistributor = Me.ClsAdj.getDistributor(False, DistributorID)
        End If
        If IsNothing(Me.dtBrandPack) Then
            Me.dtBrandPack = Me.ClsAdj.getBrandPack(False)
        End If
        If IsNothing(Me.dtDistributorGroup) Then
            Me.dtDistributorGroup = Me.ClsAdj.getDistributorGroup("", False)
        End If
        Dim dt As DataTable = Me.ClsAdj.getAddjustmentData(True, Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), DistributorID)
        Me.GridEX1.SetDataBinding(dt.DefaultView, "")
        Me.GridEX1.DropDowns("DISTRIBUTOR").SetDataBinding(Me.dtDistributor.DefaultView, "")
        Me.GridEX1.DropDowns("BRANDPACK").SetDataBinding(Me.dtBrandPack.DefaultView, "")
    End Sub
    Private Function SaveData(ByVal IAdj As Adjustment) As Boolean

        'Air Filter (Filter Udara), pastikan selalu diganti / dibersihkan secara teratur.

        'Menjaga kebersihan Air Flow Sensor Unit dari debu atau kotoran lainnya.

        'Memastikan sensor-sensor dalam kondisi baik dan terpasang dengan benar seperti:

        'Coolant Temperature Sensor, Throttle Position Sensor, Idle Position Switch, Power Steering Oil Pressure Switch, Amplifier AC, Inhibitor Switch (automatic transmission only).

        'Menjaga kebersihan bagian dalam Throttle Body

        'Menjaga kebersihan Fuel Injector

        'Menjaga kebersihan EGR Valve.

        'Memastikan selang-selang vacuum dalam kondisi baik dan terpasang dengan baik.

        'Menjaga kebersihan Servo.

        'Periksa kondisi & kebersihan PCV (Positive Crankcase Valve)


    End Function

    Private Sub frmEntry_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frmEntry.FormClosed
        Me.frmEntry.Dispose()
        Me.frmEntry = Nothing
    End Sub
    Private Sub Submitted_SaveEntry(ByRef IAdj As Adjustment, ByRef OMode As NufarmBussinesRules.common.Helper.SaveMode, ByRef Err As Object) Handles frmEntry.SaveData
        Me.Cursor = Cursors.Default
        Try
            Dim IDApp As Integer = 0
            Me.ClsAdj.SaveData(IAdj, OMode, IDApp, False)
            If OMode = NufarmBussinesRules.common.Helper.SaveMode.Insert Then
                IAdj.IDApp = IDApp
            End If
            Me.GetData()
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.frmEntry.Show()
            Me.Cursor = Cursors.Default
            Err = ex
            Me.IsloadingRow = False
        End Try
    End Sub
    Private Sub AdjustmentPKD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'get Current PKD
            Me.Cursor = Cursors.WaitCursor
            Dim StartDate As DateTime = DateTime.Now
            Dim EndDate As DateTime = StartDate.AddMonths(3)
            Me.ClsAdj.GetCurrentPeriode(StartDate, EndDate)
            Me.dtPicfrom.Value = StartDate : Me.dtPicUntil.Value = EndDate
            Me.GetData()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
            Me.IsloadingRow = False
        End Try
        'Me.dtPicfrom.Text = ""
        'Me.dtPicUntil.Text = ""
        Me.ReadAccess()
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = DirectCast(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnShowFieldChooser"
                    If Not IsNothing(GridEX1.DataSource) Then
                        If GridEX1.RootTable.Columns.Count > 0 Then
                            GridEX1.ShowFieldChooser(Me)
                        End If
                    End If
                Case "btnSettingGrid"
                    If Not IsNothing(GridEX1.DataSource) Then
                        If GridEX1.RootTable.Columns.Count > 0 Then
                            Dim SetGrid As New SettingGrid()
                            SetGrid.Grid = GridEX1
                            SetGrid.ShowDialog()
                        End If
                    End If
                Case "btnCustomFilter"
                    If Not IsNothing(GridEX1.DataSource) Then
                        If GridEX1.RootTable.Columns.Count > 0 Then
                            GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            GridEX1.RemoveFilters()
                            Me.FilterEditor1.Visible = True
                            Me.FilterEditor1.SortFieldList = False
                            Me.FilterEditor1.SourceControl = Me.GridEX1
                        End If
                    End If
                Case "btnFilterEqual"
                    If Not IsNothing(GridEX1.DataSource) Then
                        Me.FilterEditor1.SourceControl = Nothing
                        GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        GridEX1.RemoveFilters()
                    End If
                Case "btnExport"
                    If Not IsNothing(GridEX1.DataSource) Then
                        If GridEX1.RootTable.Columns.Count > 0 Then
                            ExportData(GridEX1)
                        End If
                    End If
                Case "btnRefresh"
                    If Me.btnAddNew.Checked = True Or Me.btnEditRow.Checked = True Then
                        Me.ShowMessageInfo("Please close entry area to refresh/reload data")
                        Me.Cursor = Cursors.Default : Return
                    End If
                    'reload Data
                    Me.GetData()
                Case "btnAddNew"
                    If IsNothing(Me.frmEntry) OrElse Me.frmEntry.IsDisposed Then
                        Me.frmEntry = New frmEntryAdjPKD()
                        If IsNothing(Me.dtDistributor) Then
                            Me.dtDistributor = Me.ClsAdj.getDistributor(False, "")
                        End If
                        If IsNothing(Me.dtBrandPack) Then
                            Me.dtBrandPack = Me.ClsAdj.getBrandPack(False)
                        End If
                        If IsNothing(Me.dtDistributorGroup) Then
                            Me.dtDistributorGroup = Me.ClsAdj.getDistributorGroup("", False)
                        End If
                    End If
                    With Me.frmEntry
                        .clsAdj = Me.ClsAdj
                        .Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                        .dtPicStartPeriode.Value = Me.dtPicfrom.Value
                        .dtPicEndPeriode.Value = Me.dtPicUntil.Value
                        .mcbBrandPack.SetDataBinding(Me.dtBrandPack.DefaultView(), "")
                        '.mcbBrandPack.DropDownList.RetrieveStructure()
                        '.mcbBrandPack.DropDownList().AutoSizeColumns()
                        .mcbDistributor.SetDataBinding(Me.dtDistributor.DefaultView(), "")
                        '.mcbDistributor.DropDownList().RetrieveStructure()
                        '.mcbDistributor.DropDownList.AutoSizeColumns()
                        .ChkDistributor.SetDataBinding(Me.dtDistributorGroup.DefaultView(), "")
                        Me.btnAddNew.Checked = True
                        .ShowDialog(Me)
                    End With
                Case "btnTransactionDetail"
                    If IsNothing(Me.frmRepDetail) OrElse Me.frmRepDetail.IsDisposed Then
                        frmRepDetail = New ReportTransactionAdjustment()
                        frmRepDetail.CMain = Me.CMain
                        frmRepDetail.ShowInTaskbar = False
                        frmRepDetail.MdiParent = Me.CMain
                        frmRepDetail.Show()
                    Else
                        frmRepDetail = New ReportTransactionAdjustment()
                        frmRepDetail.ShowInTaskbar = False
                        frmRepDetail.MdiParent = CMain
                        frmRepDetail.Show()
                    End If
                Case "btnEditRow"
                    Me.ShowEditOpenData()
            End Select
            Me.btnEditRow.Checked = False
            Me.btnAddNew.Checked = False
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.btnAddNew.Checked = False
            Me.btnEditRow.Checked = False
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
            Me.IsloadingRow = False
        End Try
    End Sub
    Private Sub ShowEditOpenData()
        ''check apakah data sudah pernah di pakai dalam transaksi
        If Me.GridEX1.SelectedItems Is Nothing Then
            Me.Cursor = Cursors.Default : Return
        End If
        If Me.GridEX1.SelectedItems.Count <= 0 Then : Me.Cursor = Cursors.Default : Return : End If
        If Me.GridEX1.DataSource Is Nothing Then : Me.Cursor = Cursors.Default : Return : End If
        If Me.GridEX1.RecordCount <= 0 Then : Me.Cursor = Cursors.Default : Return : End If
        If Me.GridEX1.GetRow().RowType <> Janus.Windows.GridEX.RowType.Record Then
            Me.Cursor = Cursors.Default : Return
        End If
        Dim IDApp As Integer = Convert.ToInt32(Me.GridEX1.GetValue("IDApp"))
        Dim IsGroup As Boolean = IIf((IsNothing(Me.GridEX1.GetValue("IsGroup")) Or IsDBNull(Me.GridEX1.GetValue("IsGroup"))), False, Convert.ToBoolean(Me.GridEX1.GetValue("IsGroup")))
        Dim HasTrans As Boolean = Me.ClsAdj.hasUsedInTransaction(IDApp, IsGroup)
        Dim tblDistGroup As DataTable = Nothing
        If IsNothing(Me.frmEntry) OrElse Me.frmEntry.IsDisposed Then
            Me.frmEntry = New frmEntryAdjPKD()
        End If
        With Me.frmEntry
            .clsAdj = Me.ClsAdj
            .Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
            .dtPicStartPeriode.Value = Convert.ToDateTime(Me.GridEX1.GetValue("START_PERIODE"))
            .dtPicEndPeriode.Value = Convert.ToDateTime(Me.GridEX1.GetValue("END_PERIODE"))
            .txtAdjusmentName.Text = IIf((IsNothing(Me.GridEX1.GetValue("ADJ_DESCRIPTION")) Or IsDBNull(Me.GridEX1.GetValue("ADJ_DESCRIPTION"))), "", Me.GridEX1.GetValue("ADJ_DESCRIPTION").ToString())
            .txtMaxQty.Value = Convert.ToDecimal(Me.GridEX1.GetValue("QUANTITY"))
            .mcbBrandPack.SetDataBinding(Me.dtBrandPack.DefaultView(), "")
            If IsGroup Then
                .rdbGroupDistributor.Checked = True
                .rdbPerDistributor.Checked = False
                .ChkDistributor.Visible = True
                .mcbDistributor.Visible = False
                ''get data Distributors included Adjustment group
                tblDistGroup = Me.ClsAdj.getDistributorGroup(IDApp)
                .ChkDistributor.SetDataBinding(tblDistGroup.DefaultView(), "")
            Else
                .rdbGroupDistributor.Checked = False
                .rdbPerDistributor.Checked = True
                .ChkDistributor.Visible = False
                .mcbDistributor.Visible = True
                .mcbDistributor.SetDataBinding(Me.dtDistributor.DefaultView(), "")
            End If
            '.mcbBrandPack.DropDownList.RetrieveStructure()
            '.mcbBrandPack.DropDownList().AutoSizeColumns()
            '.mcbDistributor.SetDataBinding(Me.dtDistributor.DefaultView(), "")
            '.mcbDistributor.DropDownList().RetrieveStructure()
            '.mcbDistributor.DropDownList.AutoSizeColumns()
            .cmbAdjustmentFor.Text = Me.GridEX1.GetValue("ADJUSTMENT_FOR").ToString()
            Dim OAdj As New NuFarm.Domain.Adjustment()
            With OAdj
                .BrandPackID = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
                .CodeApp = Me.GridEX1.GetValue("CodeApp").ToString()
                .CreatedBy = Me.GridEX1.GetValue("CREATE_BY").ToString()
                .CreatedDate = Convert.ToDateTime(Me.GridEX1.GetValue("CREATE_DATE"))
                .NameApp = Me.GridEX1.GetValue("ADJ_DESCRIPTION")
                .Quantity = Convert.ToDecimal(Me.GridEX1.GetValue("QUANTITY"))
                .StartDate = Convert.ToDateTime(Me.GridEX1.GetValue("START_PERIODE"))
                .EndDate = Convert.ToDateTime(Me.GridEX1.GetValue("END_PERIODE"))
                .IDApp = CInt(Me.GridEX1.GetValue("IDApp"))
                .IsGroup = IsGroup
                If Not IsGroup Then
                    .DistributorID = Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()
                Else
                    .DistributorID = ""
                End If
                Dim TypeApp As String = ""
                Select Case Me.GridEX1.GetValue("ADJUSTMENT_FOR").ToString()
                    Case "RETAILER PROGRAM"
                        TypeApp = "RP"
                    Case "DPD"
                        TypeApp = "DPD"
                End Select
                .TypeApp = TypeApp
            End With
            .OAdj = OAdj
            If HasTrans Then
                .txtMaxQty.Enabled = False
            Else
                .txtMaxQty.Enabled = True
            End If
            Me.btnEditRow.Checked = True
            .ShowDialog(Me)
        End With
    End Sub
    Private Sub GridEX1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridEX1.MouseDoubleClick
        Try
            If GridEX1.DataSource Is Nothing Then
                Return
            End If
            If GridEX1.RecordCount <= 0 Then : Return : End If
            If GridEX1.SelectedItems Is Nothing Then : Return : End If
            If GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            'inflate data 
            Me.ShowEditOpenData()
            Me.btnEditRow.Checked = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsloadingRow = False
            Me.ShowMessageError(ex.Message)
            Me.btnEditRow.Checked = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim IDApp As Integer = CInt(Me.GridEX1.GetValue("IDApp"))
            Dim isGroup As Boolean = IIf(IsNothing(Me.GridEX1.GetValue("IsGroup")) Or IsDBNull(Me.GridEX1.GetValue("IsGroup")), False, Convert.ToBoolean(Me.GridEX1.GetValue("IsGroup")))
            Dim StartDate As DateTime = Convert.ToDateTime(Me.GridEX1.GetValue("START_PERIODE"))
            Dim EndDate As DateTime = Convert.ToDateTime(Me.GridEX1.GetValue("END_PERIODE"))
            Dim DistributorID As String = Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()
            If Me.ClsAdj.hasUsedInTransaction(IDApp, isGroup) Then
                Me.ShowMessageInfo(Me.MessageDataCantChanged) : Me.Cursor = Cursors.Default : e.Cancel = True : Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes Then
                e.Cancel = False
            End If
            Dim MustReload As Boolean = False
            Me.ClsAdj.Delete(IDApp, isGroup, DistributorID, StartDate, EndDate, MustReload, True)
            If MustReload Then
                Me.GetData()
            Else
                e.Cancel = False
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
            e.Cancel = True
            Me.IsloadingRow = False
        End Try
    End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.IsloadingRow = True
            Dim dt As DataTable = Me.ClsAdj.getDistributor(True, Me.mcbDistributor.Text)

            Me.mcbDistributor.SetDataBinding(dt.DefaultView, "")
            Dim itemCount As Integer = Me.mcbDistributor.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
            Me.IsloadingRow = False
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
            Me.IsloadingRow = False
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            If Me.dtPicfrom.Text = "" Or Me.dtPicUntil.Text = "" Then
                Me.baseTooltip.Show("Please enter valid periode", Me.grpRangedate, 2500)
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.GetData()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
            Me.IsloadingRow = False
        End Try
    End Sub
End Class