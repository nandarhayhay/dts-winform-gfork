Imports System
Imports System.Configuration
Imports System.Threading
Public Class GonDetailData
    Private ld2 As Loading2
    Private IsLoadding As Boolean = False
    Private MasterCategory As String = "ByGON"
    Private CustomCategory As String = "GON"
    Private MObjChanges As New ObjMustReloadDatda()
    Private m_Grid As Janus.Windows.GridEX.GridEX = Me.grdHeader
    Private IsFirsLoad As Boolean = True
    Private CategoryType As String = "CustomCategory"
    Private isHOUser As Boolean = CBool(ConfigurationManager.AppSettings("IsHO"))
    Private ObjChanges As New ObjMustReloadDatda()
    Private clsGONData As New NufarmBussinesRules.OrderAcceptance.GonDetailData()
    Private dtGon As DataTable = Nothing
    Private tblConv As New DataTable("ConvertionTable")
    Private ThreadProgress As Thread = Nothing
    Friend StatProg As StatusProgress = StatusProgress.None
    Private Sub GetStateChecked(ByVal item As DevComponents.DotNetBar.ButtonItem)
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar2.Items
            If Not item1.Name = item.Name Then
                item1.Checked = False
            Else
                item = item1
                item.Checked = True
            End If
        Next
    End Sub
    Private Sub GonDetailData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.ShowCustomCategory()
            Me.FilterEditor1.Visible = False
            Me.cmbFilterBy.SelectedValue = "GON_NUMBER"
            Me.txtFind.Visible = True
            If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.GonDetailData Or NufarmBussinesRules.User.Privilege.ALLOW_INSERT.GonDetailData Then
                Me.grdHeader.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            End If
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' to detect category that data must be reloaded from server
    ''' </summary>
    ''' <remarks> to detect category that data must be reloaded from server</remarks>
    Private Class ObjMustReloadDatda
        Friend CategoryType As Object = Nothing
        Friend MasterCategory As Object = Nothing
        Friend CustomCategory As Object = Nothing
        'Friend DistributorID As Object = Nothing
        Friend PONumber As Object = Nothing
        Friend SPPBNumber As Object = Nothing
        Friend FromDate As Object = Nothing
        Friend UntilDate As Object = Nothing
        Friend GONNumber As Object = Nothing
    End Class
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Me.Cursor = Cursors.WaitCursor
        Dim item As DevComponents.DotNetBar.BaseItem = DirectCast(sender, DevComponents.DotNetBar.BaseItem)
        Select Case item.Name
            Case "btnRenameColumn"
                Dim MC As New ManipulateColumn()
                MC.ShowInTaskbar = False
                MC.grid = grdHeader
                MC.FillcomboColumn()
                MC.ManipulateColumnName = "Rename"
                MC.TopMost = True
                MC.Show(Me.Bar2)
            Case "btnShowFieldChooser"
                Me.grdHeader.ShowFieldChooser(Me)
            Case "btnSettingGrid"
                Dim SetGrid As New SettingGrid()
                SetGrid.Grid = grdHeader
                SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                SetGrid.ShowDialog()
            Case "btnPrint"
                Me.GridEXPrintDocument1.GridEX = Me.grdHeader
                Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                    Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                End If
                'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Me.PrintPreviewDialog1.Document.Print()
                End If
            Case "btnPageSettings"
                Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                Me.PageSetupDialog2.ShowDialog(Me)
            Case "btnCustomFilter"
                Me.FilterEditor1.Visible = True
                Me.FilterEditor1.SortFieldList = False
                Me.FilterEditor1.SourceControl = Me.grdHeader
                Me.grdHeader.FilterMode = Janus.Windows.GridEX.FilterMode.None
                Me.grdHeader.RemoveFilters()
                Me.GetStateChecked(btnCustomFilter)
            Case "btnFilterEqual"
                Me.FilterEditor1.Visible = False
                Me.grdHeader.RemoveFilters()
                Me.grdHeader.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Me.GetStateChecked(Me.btnFilterEqual)
            Case "btnExport"
                Me.SaveFileDialog1.Title = "Define the location File"
                Me.SaveFileDialog1.OverwritePrompt = True
                Me.SaveFileDialog1.DefaultExt = ".xls"
                Me.SaveFileDialog1.Filter = "All Files|*.*"
                Me.SaveFileDialog1.RestoreDirectory = True
                If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                    Me.GridEXExporter1.GridEX = Me.grdHeader
                    Me.GridEXExporter1.IncludeExcelProcessingInstruction = True
                    Me.GridEXExporter1.IncludeFormatStyle = True
                    Me.GridEXExporter1.Export(FS)
                    FS.Close()
                    MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case "btnRefresh"
                Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
        End Select
        'Me.StatProg = StatusProgress.None
        'Me.SFm = StateFillingMCB.HasFilled : Me.SFG = StateFillingGrid.HasFilled
        'Me.MustReload = False
        Me.Cursor = Cursors.Default
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
            If ((Me.btnCatGON.Checked = False) And (Me.btnCatSPPB.Checked = False) And (Me.btnCustom.Checked = False)) Then
                MessageBox.Show("Please define category to show{PODate,SPPB,or Custom}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Me.MObjChanges = New ObjMustReloadDatda()
            With Me.MObjChanges
                If Me.btnCatGON.Checked Then
                    If Me.dtpicFrom.Text = "" Or Me.dtPicUntil.Text = "" Then
                        MessageBox.Show("Please enter valid date time{From & Until}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                    '.MasterCriteria = ObjMustReloadDatda.ByCategory.ByPODate
                    .FromDate = Convert.ToDateTime(Me.dtpicFrom.Value.ToShortDateString())
                    .UntilDate = Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())
                    '.CustomCriteria = ObjMustReloadDatda.ByCustomCategory.None
                    .CustomCategory = ""
                    .MasterCategory = "ByGON"
                    .GONNumber = Nothing
                    .SPPBNumber = Nothing
                    .GONNumber = Nothing
                    .CategoryType = "MasterCategory"
                    Me.MasterCategory = "ByGON"
                    Me.CustomCategory = ""
                    Me.CategoryType = "MasterCategory"
                ElseIf Me.btnCatSPPB.Checked Then
                    If Me.dtpicFrom.Text = "" Or Me.dtPicUntil.Text = "" Then
                        MessageBox.Show("Please enter valid date time{From & Until}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                    .FromDate = Convert.ToDateTime(Me.dtpicFrom.Value.ToShortDateString())
                    .UntilDate = Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())
                    .CustomCategory = ""
                    .MasterCategory = "BySPPB"
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
                        Me.baseTooltip.Show("Please define custom category to show{GONNumber,SPPBNumber,or PONUmber}", Me.cmbFilterBy, 2500)
                        Return
                    ElseIf Me.cmbFilterBy.Text = "" Then
                        'MessageBox.Show("Please define custom category to show{Distributor,SPPBNumber,or PONUmber}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.baseTooltip.Show("Please define custom category to show{GONNumber,SPPBNumber,or PONUmber}", Me.cmbFilterBy, 2500)
                        Return
                    End If
                    Select Case Me.cmbFilterBy.Text
                        Case "GON_NUMBER"
                            .MasterCategory = "ByCustom"
                            .CustomCategory = "GON"
                            .GONNumber = Me.txtFind.Text.Trim()
                            .SPPBNumber = Nothing
                            .FromDate = Nothing
                            .UntilDate = Nothing
                            Me.CustomCategory = "GON_NUMBER"
                        Case "PO_NUMBER"
                            If Me.txtFind.Text = "" Then
                                Me.baseTooltip.Show("Please enter PONumber to search for", Me.txtFind, 2500)
                                Return
                            End If
                            .MasterCategory = "ByCustom"
                            .CustomCategory = "PO_NUMBER"
                            .PONumber = Me.txtFind.Text.TrimStart().TrimEnd()
                            .SPPBNumber = Nothing
                            .GONNumber = Nothing
                            .FromDate = Nothing
                            .UntilDate = Nothing
                            Me.CustomCategory = "PO_NUMBER"
                        Case "SPPB_NUMBER"
                            If Me.txtFind.Text = "" Then
                                Me.baseTooltip.Show("Please enter SPPB no to search for", Me.txtFind, 2500)
                                Return
                            End If
                            .MasterCategory = "ByCustom"
                            .CustomCategory = "SPPB_NUMBER"
                            .SPPBNumber = Me.txtFind.Text.TrimStart().TrimEnd()
                            .PONumber = Nothing
                            .GONNumber = Nothing
                            .FromDate = Nothing
                            .UntilDate = Nothing
                            ''hilangkan distributor
                            Me.CustomCategory = "SPPB_NUMBER"
                        Case "GON_NUMBER"
                            If Me.txtFind.Text = "" Then
                                Me.baseTooltip.Show("Please enter SPPB no to search for", Me.txtFind, 2500)
                                Return
                            End If
                            .MasterCategory = "ByCustom"
                            .CustomCategory = "GON_NUMBER"
                            .GONNumber = Me.txtFind.Text.TrimStart().TrimEnd()
                            .PONumber = Nothing
                            .SPPBNumber = Nothing
                            .FromDate = Nothing
                            .UntilDate = Nothing
                            Me.CustomCategory = "GON_NUMBER"
                    End Select
                    .CategoryType = "CustomCategory"
                    Me.MasterCategory = "ByCustom"
                    Me.CategoryType = "CustomCategory"
                End If
            End With
            Me.IsLoadding = True
            Me.ShowData()
            ''place new criteria becomes original criteria to detect changes
            Me.ObjChanges = Me.MObjChanges
            Me.IsLoadding = False
        Catch ex As Exception
            Me.IsLoadding = False
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
            If Me.btnCustom.Checked Then : Me.MasterCategory = "ByCustom"
            ElseIf Me.btnCatGON.Checked Then : Me.MasterCategory = "ByGON"
            ElseIf Me.btnCatSPPB.Checked Then : Me.MasterCategory = "BySPPB"
            Else : Me.MasterCategory = ""
            End If
            Me.ShowMessageError(ex.Message)
            Me.IsLoadding = False
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function HasChangedCriteria() As Boolean
        ''check date
        If Me.IsFirsLoad Then : Return True : End If
        If Me.MObjChanges.CategoryType <> Me.ObjChanges.CategoryType Then
            Return True
        End If
        If Me.MasterCategory = "ByGON" Or Me.MasterCategory = "BySPPB" Then
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
    End Function
    Friend Sub ShowProceed()
        ld2 = New Loading2()
        ld2.Show()
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Thread.Sleep(50) : ld2.Refresh() : Application.DoEvents()
        End While
        Thread.Sleep(100)
        ld2.Close() : ld2 = Nothing
    End Sub
    Friend Enum StatusProgress
        None
        Processing
    End Enum
    Private Sub ShowData()

        '=================UNCOMMENT THIS AFTER DEBUGGING========================
        Me.StatProg = StatusProgress.Processing
        Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
        Me.ThreadProgress.Start()
        '====================================================================

        'Dim DistributorID As Object = Nothing
        'If Not IsNothing(Me.frmParentSPPB) Then
        '    frmParentSPPB.cmbDistributor.Enabled = False
        '    If frmParentSPPB.cmbDistributor.Text <> "" And frmParentSPPB.cmbDistributor.SelectedIndex <> -1 Then
        '        DistributorID = frmParentSPPB.cmbDistributor.Value.ToString()
        '    End If
        'End If

        Dim FromDate As Object = IIf((Me.MasterCategory = "ByGON" Or Me.MasterCategory = "BySPPB" And Me.dtpicFrom.Text <> ""), Convert.ToDateTime(Me.dtpicFrom.Value.ToShortDateString()), Nothing)
        Dim UntilDate As Object = IIf((Me.MasterCategory = "ByGON" Or Me.MasterCategory = "BySPPB" And Me.dtPicUntil.Text <> ""), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), Nothing)
        Dim SPPBNumber As Object = IIf((Me.CustomCategory = "SPPB_NUMBER" And Me.cmbFilterBy.Text = "SPPB_NUMBER"), Me.txtFind.Text.TrimStart().TrimEnd(), Nothing)
        Dim PONUmber As Object = IIf((Me.CustomCategory = "PO_NUMBER" And Me.cmbFilterBy.Text = "PO_NUMBER"), Me.txtFind.Text.TrimStart().TrimEnd(), Nothing)
        Dim GONNUmber As Object = IIf((Me.CustomCategory = "GON_NUMBER" And Me.cmbFilterBy.Text = "GON_NUMBER"), Me.txtFind.Text.TrimStart().TrimEnd(), Nothing)
        Me.dtGon = Me.clsGONData.getDTSPPBGON(Me.MasterCategory, Me.CustomCategory, PONUmber, SPPBNumber, GONNUmber, FromDate, UntilDate, Me.HasChangedCriteria(), Me.tblConv)
        '=================================================================================
        ''process data
        Dim row As DataRow = Nothing, rows() As DataRow = Nothing
        Dim listID As New List(Of String)
        For i As Integer = 0 To dtGon.Rows.Count - 1
            'dtGon.Columns.Add("ID", Type.GetType("System.String"))
            Dim ID As String = dtGon.Rows(i)("ID").ToString() ' + dtGon.Rows(i)("BRANDPACK_ID")
            'row.BeginEdit()
            ''row("ID") = ID
            'row.EndEdit()
            If Not listID.Contains(ID) Then
                listID.Add(ID)
            End If
        Next
        dtGon.AcceptChanges()
        For i As Integer = 0 To listID.Count - 1
            Dim DevQty As Object = Nothing, GonQty As Decimal = Nothing, SPPBqty As Decimal = Nothing, TotalDisc As Decimal = Nothing, _
            POOriginal = Nothing, totalGon As Decimal = Nothing, PODIsc As Decimal = 0, GONDisc As Decimal = 0, XSize As Decimal = 0, _
            TotalGONPO As Decimal = 0, TotalGONDisc As Decimal = 0
            rows = dtGon.Select("ID = '" & listID(i) & "'")
            If CInt(rows(0)("TOTAL_GON")) > 1 Then
                TotalGONPO = 0
                For i1 As Integer = 0 To rows.Length - 1
                    row = rows(i1)
                    DevQty = row("DEVIDED_QUANTITY") : GonQty = row("GON_QTY") : SPPBqty = row("SPPB_QTY") : TotalDisc = row("TOTAL_DISC")
                    POOriginal = row("PO_ORIGINAL_QTY") : totalGon = row("TOTAL_GON")
                    row.BeginEdit()

                    If i1 = rows.Length - 1 Then ''yang terakhir
                        PODIsc = POOriginal - TotalGONPO
                        GONDisc = TotalDisc - TotalGONDisc
                    Else
                        XSize = GonQty / DevQty
                        PODIsc = Decimal.Round((POOriginal * XSize) / SPPBqty, MidpointRounding.AwayFromZero)
                        PODIsc = PODIsc * DevQty
                        TotalGONPO += PODIsc

                        GONDisc = Decimal.Round((TotalDisc * XSize) / SPPBqty, MidpointRounding.AwayFromZero)
                        GONDisc = GONDisc * DevQty
                        TotalGONDisc += GONDisc
                    End If
                    If TotalDisc <= 0 Then
                        row("GON_PO") = GonQty
                        row("GON_DISC_INC") = 0
                    Else
                        row("GON_PO") = PODIsc
                        row("GON_DISC_INC") = GONDisc
                    End If

                    row.EndEdit()
                Next
            Else
                row = rows(0)
                DevQty = row("DEVIDED_QUANTITY") : GonQty = row("GON_QTY") : SPPBqty = row("SPPB_QTY") : TotalDisc = row("TOTAL_DISC")
                POOriginal = row("PO_ORIGINAL_QTY") : totalGon = row("TOTAL_GON")
                row.BeginEdit()
                If TotalDisc > 0 Then
                    If GonQty = SPPBqty Then
                        row("GON_PO") = POOriginal
                        row("GON_DISC_INC") = TotalDisc
                    ElseIf GonQty < SPPBqty Then
                        XSize = GonQty / DevQty
                        PODIsc = Decimal.Round((POOriginal * XSize) / SPPBqty, MidpointRounding.AwayFromZero)
                        PODIsc = PODIsc * DevQty
                        GONDisc = Decimal.Round((TotalDisc * XSize) / SPPBqty, MidpointRounding.AwayFromZero)
                        GONDisc = GONDisc * DevQty
                        row("GON_PO") = PODIsc
                        row("GON_DISC_INC") = GONDisc
                    End If
                Else
                    row("GON_PO") = GonQty
                    row("GON_DISC_INC") = 0
                End If
                row.EndEdit()
            End If
        Next

        'For i As Integer = 0 To dtGon.Rows.Count - 1
        '    row = dtGon.Rows(i)
        '    Dim DevQty As Object = row("DEVIDED_QUANTITY"), GonQty As Decimal = row("GON_QTY"), SPPBqty As Decimal = row("SPPB_QTY"), _
        '    TotalDisc As Decimal = row("TOTAL_DISC"), POOriginal = row("PO_ORIGINAL_QTY"), totalGon As Decimal = row("TOTAL_GON"), PODIsc As Decimal = 0, GONDisc As Decimal = 0
        '    If Not IsNothing(DevQty) And Not IsDBNull(DevQty) Then
        '        row.BeginEdit()
        '        'perhitungan untuk contoh terkecil kemasan 100ml (0,1)
        '        'bagi gonqty dengan devqty = X
        '        'ORG = (PO ORIGINAL * X)/sppbqty = x1 
        '        'karena sudah masuk kemasannya, hasil diatas dibulatkan saja 
        '        'lalu di kali dengan kemasan(devqty)

        '        'DISC (totaldisc x X)/gonqty = x2
        '        'karena sudah masuk kemasannya, hasil diatas dibulatkan saja 
        '        'lalu di kali dengan kemasan(devqty) lalu di format jadi 3 digit belakang koma
        '        Dim XSize As Decimal = 0
        '        If totalGon > 1 Then
        '            XSize = GonQty / DevQty
        '            PODIsc = Decimal.Round((POOriginal * XSize) / SPPBqty, MidpointRounding.AwayFromZero)
        '            PODIsc = PODIsc * DevQty

        '            GONDisc = Decimal.Round((TotalDisc * XSize) / SPPBqty, MidpointRounding.AwayFromZero)
        '            GONDisc = GONDisc * DevQty

        '            'XSize = GonQty / DevQty
        '            'PODIsc = (POOriginal * XSize) / SPPBqty
        '            'PODIsc = PODIsc * DevQty

        '            'GONDisc = (TotalDisc * XSize) / SPPBqty
        '            'GONDisc = GONDisc * DevQty

        '            If TotalDisc <= 0 Then
        '                row("GON_PO") = GonQty
        '                row("GON_DISC_INC") = 0
        '            Else
        '                row("GON_PO") = PODIsc
        '                row("GON_DISC_INC") = GONDisc
        '            End If
        '        ElseIf TotalDisc <= 0 Then
        '            row("GON_PO") = GonQty
        '            row("GON_DISC_INC") = 0
        '        End If
        '        row.EndEdit()
        '    End If
        'Next
        '003211-02
        Me.BindGrid(Me.dtGon.DefaultView, Me.grdHeader, False)
        'Me.BindGrid(Me.DS.Tables("GON_DETAIL_INFO").DefaultView(), Me.grdDetail, False)
        Me.IsFirsLoad = False
        Me.StatProg = StatusProgress.None
        '=========================NEW UPDATED PROCESS===========================================
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
        GridN.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        For Each col As Janus.Windows.GridEX.GridEXColumn In GridN.RootTable.Columns
            If col.DataMember.Contains("PRICE") Then
                If Not Me.isHOUser Then
                    col.Visible = False
                    col.ShowInFieldChooser = False
                End If
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
                If Not Me.isHOUser Then
                    col.Visible = False
                    col.ShowInFieldChooser = False
                End If
                col.FormatString = "#,##0.00"
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                col.TotalFormatString = "#,##0.00"
            ElseIf col.DataMember = "TOTAL_DISC" Or col.DataMember = "GON_PO" Or col.DataMember = "GON_DISC_INC" Then
                col.FormatString = "#,##0.000"
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                col.TotalFormatString = "#,##0.000"
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

            If col.Key = "BatchNo" Then
                col.Caption = "BATCH_NO"
            End If
            'GD.BatchNo,GD.UNIT1,GD.VOL1,GD.UNIT2,GD.VOL2
            If col.Key = "UNIT1" Or col.Key = "VOL1" Or col.Key = "UNIT2" Or col.Key = "VOL2" Or col.Key = "IsOpen" Or col.Key = "IsCompleted" Or col.Key = "GT_ID" Or col.Key = "GON_ID_AREA" _
             Or col.Key = "DISTRIBUTOR_NAME" Or col.Key = "PO_CATEGORY" Or col.Key = "PO_REF_NO" Or col.Key = "PO_REF_DATE" _
             Or col.Key = "SHIP_TO_REGIONAL" Or col.Key = "SHIP_TO_TERRITORY" Or col.Key = "PRICE" Or col.Key = "CSE_REMARK" Or col.Key = "SALES_QTY" Or col.Key = "DEVIDED_QUANTITY" _
             Or col.Key = "BALANCE" Or col.Key = "TOTAL_GON" Or col.Key = "ID" Then
                col.Visible = False
            End If
            'col.AutoSize()
            'col.EditType = Janus.Windows.GridEX.EditType.NoEdit
        Next
        grdHeader.AutoSizeColumns()
    End Sub
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
        Me.lblFrom.Text = "Filter By" : Me.lblUntil.Visible = False : Me.txtFind.Visible = True
        Me.cmbFilterBy.Visible = True : Me.cmbFilterBy.Text = ""
        'Me.btnFilteDate.Location = New System.Drawing.Point(431, 4)
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
                    Me.btnCatGON.Checked = False
                    Me.btnCustom.Checked = False
                    If item.Checked Then
                        Me.ShowMasterCategory()
                        Me.MasterCategory = "BySPPB"
                        Me.CustomCategory = ""
                    Else
                        Me.grdHeader.SetDataBinding(Nothing, "")
                        Me.IsLoadding = False
                        Me.Cursor = Cursors.Default
                        Return
                    End If
                    Me.CategoryType = "MasterCategory"
                Case "btnCatGON"
                    Me.btnCatSPPB.Checked = False : Me.btnCustom.Checked = False
                    If item.Checked Then
                        Me.ShowMasterCategory()
                        Me.MasterCategory = "ByGON"
                        Me.CustomCategory = ""
                    Else
                        Me.grdHeader.SetDataBinding(Nothing, "")
                        Me.IsLoadding = False
                        Me.Cursor = Cursors.Default
                        Return
                    End If
                    Me.CategoryType = "MasterCategory"
                Case "btnCustom"
                    Me.btnCatSPPB.Checked = False : Me.btnCatGON.Checked = False
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
                        Me.grdHeader.SetDataBinding(Nothing, "")
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
            ElseIf Me.btnCatGON.Checked Then
                Me.MasterCategory = "ByGON"
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
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtFind_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFind.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
        End If
    End Sub
End Class
