Imports System.Threading
Imports System.Configuration
Public Class PO
#Region " Deklarasi "
    Private clsPO As NufarmBussinesRules.PurchaseOrder.PORegistering
    Private SFm As StateFillingMCB
    Private SFG As StateFillingGrid
    Private m_ViewFlag As DataView
    Private SelecttedGrid As GridSelect
    Friend CMain As Main = Nothing
    Private LD As Loading
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Friend MustReload As Boolean = False
    Private hasLoadGrid As Boolean = False
    Private isHOUser As Boolean = CBool(ConfigurationManager.AppSettings("IsHO"))
    Private ShowPrice As Boolean = CBool(ConfigurationManager.AppSettings("ShowPrice"))
#End Region

#Region " Enum "
    Private Enum StateFillingMCB
        Filling
        HasFilled
    End Enum
    Private Enum StateFillingGrid
        HasFilled
        Filling
    End Enum
    Private Enum GridSelect
        GridPO
        GridPencapaian
    End Enum
    Private Enum StatusProgress
        None
        Processing
    End Enum
#End Region

#Region " SUB "
    Private Sub ShowProceed()
        LD = New Loading() : LD.Show() : LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            LD.Refresh() : Thread.Sleep(100) : Application.DoEvents()
        End While
        Thread.Sleep(100) : LD.Close() : LD = Nothing
    End Sub
    Private Sub ShowThread()
        Me.cmbDistributor.Enabled = False
        Me.StatProg = StatusProgress.Processing
        Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
        Me.ThreadProgress.Start()
    End Sub
    Private Sub BindMulticolumnCombo()
        Me.SFm = StateFillingMCB.Filling
        Me.cmbDistributor.SetDataBinding(Me.clsPO.ViewDistributor(), "")
        If IsNothing(Me.clsPO.ViewDistributor()) Then
            Me.SFm = StateFillingMCB.HasFilled : Return
        End If
        Me.cmbDistributor.DropDownList.RetrieveStructure()
        Me.cmbDistributor.DisplayMember = "DISTRIBUTOR_NAME" : Me.cmbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.cmbDistributor.DroppedDown = True
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.cmbDistributor.DropDownList.Columns
            col.AutoSize()
        Next
        Me.cmbDistributor.DroppedDown = False
        Me.SFm = StateFillingMCB.HasFilled
    End Sub

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

    Private Sub ClearCheckedState()
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar2.Items
            item1.Checked = False
        Next
    End Sub

    Friend Sub InitializeData()
        Me.LoadData()
        'Me.LoadData()
    End Sub

    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator = True And Not NufarmBussinesRules.User.UserLogin.IsAdmin Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PO = True Then
                Me.grdPurchaseOrder.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.grdPurchaseOrder.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.EntryPOHeader = True Then
                Me.btnManagePO.Visible = True
                Me.btnPOHeader.Visible = True
                Me.btnPOHeaderAndDetail.Visible = False
                Me.btnEdit.Visible = False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Achievement Then
                Me.grdPurchaseOrder.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.grdPurchaseOrder.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PO Then
                Me.btnManagePO.Visible = True
                Me.btnPOHeaderAndDetail.Visible = True
                Me.btnManagePO.Visible = True
                Me.btnEdit.Visible = True
            Else
                Me.btnPOHeader.Visible = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.EntryPOHeader
                Me.btnPOHeaderAndDetail.Visible = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PO
                Me.btnManagePO.Visible = False
                Me.btnEdit.Visible = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PO
            End If

        End If
    End Sub

    Private Sub LoadData()
        Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PORegistering
        Me.clsPO.CreateViewDistributorPO()
        Me.BindMulticolumnCombo()
    End Sub

    Private Sub BinfGrid(ByVal grid As Janus.Windows.GridEX.GridEX, ByVal dSource As Object)

        If Not dSource Is Nothing Then
            Select Case grid.Name
                Case "grdPencapaian"
                    Dim dtview As DataView = CType(dSource, DataView)
                    For i As Integer = 0 To dtview.Count - 1
                        If CDec(dtview(i)("LEFT_QTY")) > 0 Then
                            dtview(i)("LEFT_QTY") = CObj(0)
                            dtview(i).EndEdit()
                        End If
                    Next
                    grid.DataSource = dtview
                    grid.DataMember = "REACHING_QTY"
                    grid.RetrieveStructure()
                    Me.Format_GridPencapaian()
                Case "grdPurchaseOrder"
                    grid.DataSource = dSource
                    grid.DataMember = "Purchase_Order"
                    If Not Me.hasLoadGrid Then
                        grid.RetrieveStructure()
                        Me.FormatGridPO()
                    End If
                    Me.hasLoadGrid = True
                    'Me.FillCategoriesValueList()
            End Select
        Else
            grid.SetDataBinding(Nothing, "")
        End If
        'Select Case grid.Name
        '    Case "grdOAHistory" 'terakhir

        '    Case "grdPurchaseOrder"
        '        grid.DataSource = dSource

        'End Select
    End Sub

    Private Sub FillCategoriesValueList()
        Dim colDistributorID As Janus.Windows.GridEX.GridEXColumn = Me.grdPurchaseOrder.RootTable.Columns("DISTRIBUTOR_ID")
        colDistributorID.FilterEditType = Janus.Windows.GridEX.EditType.Combo
        ''Set HasValueList property equal to true in order to be able to use the ValueList property
        colDistributorID.HasValueList = True
        ''Get the ValueList collection associated to this column
        Dim ValueListDistributorID As Janus.Windows.GridEX.GridEXValueListItemCollection = colDistributorID.ValueList
        ValueListDistributorID.PopulateValueList(Me.clsPO.ViewDistributor(), "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME")
        colDistributorID.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        colDistributorID.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    End Sub

    Private Sub RefreshData()
        Dim ValueDistributor As Object = Nothing
        If Not IsNothing(Me.cmbDistributor.SelectedItem) Then
            ValueDistributor = Me.cmbDistributor.Value
        End If
        Me.SFm = StateFillingMCB.Filling
        Me.clsPO.CreateViewDistributorPO() : Me.BindMulticolumnCombo()
        Me.cmbDistributor.Value = Nothing : Me.cmbDistributor.Value = ValueDistributor

        Me.btnAplyRange_Click(Me.btnAplyRange, New System.EventArgs())
        'If Not (Me.cmbDistributor.Value Is Nothing) Then
        'End If
        Me.SFm = StateFillingMCB.HasFilled
        Me.MustReload = False
    End Sub

    Private Sub FormatGridPO()
        'PARENT TABLE
        Me.grdPurchaseOrder.RootTable.Columns("PO_BRANDPACK_ID").Visible = False
        Me.grdPurchaseOrder.RootTable.Columns("PO_BRANDPACK_ID").ShowInFieldChooser = False
        Me.grdPurchaseOrder.RootTable.Columns("DISTRIBUTOR_ID").Visible = True

        Me.grdPurchaseOrder.RootTable.Columns("DISTRIBUTOR_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPurchaseOrder.RootTable.Columns("DISTRIBUTOR_NAME").EditType = Janus.Windows.GridEX.EditType.NoEdit
        'Me.grdPurchaseOrder.RootTable.Columns("DISTRIBUTOR_ID").Caption = "DISTRIBUTOR_NAME"
        Me.grdPurchaseOrder.RootTable.Columns("DISTRIBUTOR_ID").ShowInFieldChooser = True
        Me.grdPurchaseOrder.RootTable.Columns("DISTRIBUTOR_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPurchaseOrder.RootTable.Columns("DISTRIBUTOR_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit

        Me.grdPurchaseOrder.RootTable.Columns("PO_REF_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.grdPurchaseOrder.RootTable.Columns("PO_REF_DATE").LimitToList = True
        Me.grdPurchaseOrder.RootTable.Columns("PO_REF_DATE").AllowSort = True
        Me.grdPurchaseOrder.RootTable.Columns("PO_REF_DATE").FormatString = "dd MMMM yyyy"
        Me.grdPurchaseOrder.RootTable.Columns("PO_REF_DATE").EditType = Janus.Windows.GridEX.EditType.NoEdit

        Me.grdPurchaseOrder.RootTable.Columns("PO_REF_NO").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPurchaseOrder.RootTable.Columns("PO_REF_NO").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.Columns("BRANDPACK_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPurchaseOrder.RootTable.Columns("BRANDPACK_NAME").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True

        Me.grdPurchaseOrder.RootTable.Columns("QUANTITY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPurchaseOrder.RootTable.Columns("QUANTITY").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdPurchaseOrder.RootTable.Columns("QUANTITY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grdPurchaseOrder.RootTable.Columns("QUANTITY").FormatString = "#,##0.000"
        Me.grdPurchaseOrder.RootTable.Columns("QUANTITY").TotalFormatString = "#,##0.000"
        Me.grdPurchaseOrder.RootTable.Columns("QUANTITY").EditType = Janus.Windows.GridEX.EditType.NoEdit

        Me.grdPurchaseOrder.RootTable.Columns("PRICE/QTY").FormatString = "#,##0.00"
        Me.grdPurchaseOrder.RootTable.Columns("PRICE/QTY").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdPurchaseOrder.RootTable.Columns("PRICE/QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grdPurchaseOrder.RootTable.Columns("PRICE/QTY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPurchaseOrder.RootTable.Columns("PRICE/QTY").EditType = Janus.Windows.GridEX.EditType.NoEdit

        Me.grdPurchaseOrder.RootTable.Columns("TOTAL").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        Me.grdPurchaseOrder.RootTable.Columns("TOTAL").TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
        Me.grdPurchaseOrder.RootTable.Columns("TOTAL").TotalFormatString = "Total Amount = {0:#,##0.00}"
        Me.grdPurchaseOrder.RootTable.Columns("TOTAL").FormatString = "#,##0.00"
        Me.grdPurchaseOrder.RootTable.Columns("TOTAL").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo

        Me.grdPurchaseOrder.RootTable.Columns("TOTAL").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdPurchaseOrder.RootTable.Columns("TOTAL").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.grdPurchaseOrder.RootTable.Columns("TOTAL").EditType = Janus.Windows.GridEX.EditType.NoEdit
        If Not Me.isHOUser Then
            Me.grdPurchaseOrder.RootTable.Columns("PRICE/QTY").Visible = False
            Me.grdPurchaseOrder.RootTable.Columns("PRICE/QTY").ShowInFieldChooser = False
            Me.grdPurchaseOrder.RootTable.Columns("TOTAL").Visible = False
            Me.grdPurchaseOrder.RootTable.Columns("TOTAL").ShowInFieldChooser = False
        End If
        Me.grdPurchaseOrder.RootTable.Columns("BRANDPACK_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo

        Me.grdPurchaseOrder.RootTable.Columns("BRANDPACK_ID").Visible = False
        Me.grdPurchaseOrder.RootTable.Columns("BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.GroupByBoxVisible = False
        Me.grdPurchaseOrder.RootTable.Columns("DESCRIPTIONS").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPurchaseOrder.RootTable.Columns("DESCRIPTIONS").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.Columns("DESCRIPTIONS").EditType = Janus.Windows.GridEX.EditType.NoEdit

        Me.grdPurchaseOrder.RootTable.Columns("DESCRIPTIONS2").Caption = "CSE REMARK"
        Me.grdPurchaseOrder.RootTable.Columns("DESCRIPTIONS2").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPurchaseOrder.RootTable.Columns("DESCRIPTIONS2").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.Columns("DESCRIPTIONS2").Width = 210
        Me.grdPurchaseOrder.RootTable.Columns("DESCRIPTIONS2").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.Columns("PLANTATION_NAME").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.Columns("PLANTATION_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPurchaseOrder.RootTable.Columns("PLANTATION_NAME").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.Columns("PROJECT_NAME").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.Columns("PROJECT_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPurchaseOrder.RootTable.Columns("PROJECT_NAME").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.Columns("PROJ_REF_NO").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.Columns("PROJ_REF_NO").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPurchaseOrder.RootTable.Columns("PROJ_REF_NO").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.Columns("CREATE_DATE").Visible = False
        Me.grdPurchaseOrder.RootTable.Columns("CREATE_DATE").FormatString = "dd MMMM yyyy"
        Me.grdPurchaseOrder.RootTable.Columns("CREATE_DATE").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPurchaseOrder.RootTable.Columns("MODIFY_DATE").Visible = False
        Me.grdPurchaseOrder.RootTable.Columns("MODIFY_DATE").FormatString = "dd MMMM yyyy"
        Me.grdPurchaseOrder.RootTable.Columns("MODIFY_DATE").EditType = Janus.Windows.GridEX.EditType.NoEdit

        'For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdPurchaseOrder.RootTable.Columns
        '    col.AutoSize()
        'Next
        Me.grdPurchaseOrder.RootTable.Columns("ExcludeDPD").Visible = NufarmBussinesRules.User.UserLogin.IsAdmin
        Me.grdPurchaseOrder.RootTable.Columns("ExcludeDPD").EditType = Janus.Windows.GridEX.EditType.CheckBox

        Me.AddConditionalFormatingGrid()
    End Sub
    Private Sub AddConditionalFormatingGrid()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdPurchaseOrder.RootTable.Columns("QUANTITY"), Janus.Windows.GridEX.ConditionOperator.Equal, 0)
        fc.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
        fc.AllowMerge = True
        Me.grdPurchaseOrder.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub Format_GridPencapaian()
        For Each item As Janus.Windows.GridEX.GridEXColumn In Me.grdPencapaian.RootTable.Columns
            item.AutoSize()
            item.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            If (item.Type Is Type.GetType("System.Decimal")) Or (item.Type Is Type.GetType("System.Single")) _
            Or (item.Type Is Type.GetType("System.Double")) Then
                item.FormatString = "#,##0.000"
                item.TotalFormatString = "#,##0.000"
                item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                item.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ElseIf (item.Type Is Type.GetType("System.Int32")) Or (item.Type Is Type.GetType("System.Int64")) Then
                item.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End If

        Next

    End Sub

#End Region

#Region " Function "
    Private Function CreateViewFlag(ByVal flag As String) As DataView
        Dim tblFlag As New DataTable("FLAG")
        tblFlag.Clear()
        Dim row As DataRow
        Select Case flag
            Case "Q"
                Dim colQ As New DataColumn("QUARTER", Type.GetType("System.String"))
                tblFlag.Columns.Add(colQ)
                row = tblFlag.NewRow()
                row("QUARTER") = "QUARTER1"
                tblFlag.Rows.Add(row)
                row = tblFlag.NewRow()
                row("QUARTER") = "QUARTER2"
                tblFlag.Rows.Add(row)
                row = tblFlag.NewRow()
                row("QUARTER") = "QUARTER3"
                tblFlag.Rows.Add(row)
                row = tblFlag.NewRow()
                row("QUARTER") = "QUARTER4"
                tblFlag.Rows.Add(row)
                row = tblFlag.NewRow()
                row("QUARTER") = "YEAR"
                tblFlag.Rows.Add(row)

            Case "S"
                Dim colS As New DataColumn("SEMESTER", Type.GetType("System.String"))
                tblFlag.Columns.Add(colS)
                row = tblFlag.NewRow()
                row("SEMESTER") = "SEMESTER1"
                tblFlag.Rows.Add(row)
                row = tblFlag.NewRow()
                row("SEMESTER") = "SEMESTER2"
                tblFlag.Rows.Add(row)
                row = tblFlag.NewRow()
                row("SEMESTER") = "YEAR"
                tblFlag.Rows.Add(row)
        End Select
        Me.m_ViewFlag = tblFlag.DefaultView()
        Return Me.m_ViewFlag
    End Function

    Private Function GetFilterDate() As String
        Dim filterdate As String = ""
        'DIM DateFrom as String = 
        If (Me.dtPicFrom.Text <> "") And (Me.dtpicUntil.Text <> "") Then
            filterdate = "PO_REF_DATE >= #" & Me.dtPicFrom.Value.Month.ToString() & "/" & Me.dtPicFrom.Value.Day.ToString() & "/" & _
            Me.dtPicFrom.Value.Year.ToString & "# AND PO_REF_DATE <= #" & Me.dtpicUntil.Value.Month.ToString() & "/" & Me.dtpicUntil.Value.Day.ToString() & "/" & _
            Me.dtpicUntil.Value.Year.ToString & "#"
            'Me.clsPO.ViewPORegistering.RowFilter = filterdate
        ElseIf Me.dtPicFrom.Text <> "" Then
            filterdate = "PO_REF_DATE >= #" & Me.dtPicFrom.Value.Month.ToString() & "/" & Me.dtPicFrom.Value.Day.ToString() & "/" & _
            Me.dtPicFrom.Value.Year.ToString & "#"
            'Me.clsPO.ViewPORegistering.RowFilter = filterdate
        ElseIf Me.dtpicUntil.Text <> "" Then
            filterdate = "PO_REF_DATE <= #" & Me.dtpicUntil.Value.Month.ToString() & "/" & Me.dtpicUntil.Value.Day.ToString() & "/" & _
            Me.dtpicUntil.Value.Year.ToString() & "#"
        End If
        Return filterdate
    End Function
    Private Function IsValidDate() As Boolean
        Dim ValidDate As Boolean = False
        If Me.dtPicFrom.Text <> "" And Me.dtpicUntil.Text <> "" Then
            ValidDate = True
        Else : ValidDate = False
        End If
        If Not ValidDate Then
            Me.ShowMessageInfo("Please define Start and Date PO") : Return False
        End If
        Return True
    End Function
#End Region

#Region " Event Procedure "

    Private Sub PO_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not IsNothing(Me.clsPO) Then
                Me.clsPO.Dispose(True)
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = DirectCast(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnRenameColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    Select Case Me.SelecttedGrid
                        Case GridSelect.GridPencapaian
                            MC.grid = Me.grdPencapaian
                        Case GridSelect.GridPO
                            MC.grid = Me.grdPurchaseOrder
                    End Select
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar1)
                Case "btnShowFieldChooser"
                    Select Case Me.SelecttedGrid
                        Case GridSelect.GridPencapaian
                            Me.grdPencapaian.ShowFieldChooser(Me)
                        Case GridSelect.GridPO
                            Me.grdPurchaseOrder.ShowFieldChooser(Me)
                    End Select
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    Select Case Me.SelecttedGrid
                        Case GridSelect.GridPencapaian
                            SetGrid.Grid = Me.grdPencapaian
                            SetGrid.GridExPrintDock = Me.GridEXPrintDocument2
                        Case GridSelect.GridPO
                            SetGrid.Grid = Me.grdPurchaseOrder
                            SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    End Select
                    SetGrid.ShowDialog()
                Case "btnPrint"
                    Select Case Me.SelecttedGrid
                        Case GridSelect.GridPencapaian
                            Me.GridEXPrintDocument2.GridEX = Me.grdPencapaian
                            Me.PrintPreviewDialog2.Document = Me.GridEXPrintDocument2
                            If Not IsNothing(Me.PageSetupDialog2.PageSettings) Then
                                Me.PrintPreviewDialog2.Document.DefaultPageSettings = Me.PageSetupDialog2.PageSettings
                            End If
                            'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                            If Me.PrintPreviewDialog2.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Me.PrintPreviewDialog2.Document.Print()
                            End If
                        Case GridSelect.GridPO
                            Me.GridEXPrintDocument1.GridEX = Me.grdPurchaseOrder
                            Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                            If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                                Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                            End If
                            'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                            If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Me.PrintPreviewDialog1.Document.Print()
                            End If
                    End Select

                Case "btnPageSettings"
                    Select Case Me.SelecttedGrid
                        Case GridSelect.GridPencapaian
                            Me.PageSetupDialog2.Document = Me.GridEXPrintDocument2
                            Me.PageSetupDialog2.ShowDialog(Me)
                        Case GridSelect.GridPO
                            Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                            Me.PageSetupDialog1.ShowDialog(Me)
                    End Select
                Case "btnCustomFilter"
                    Me.FilterEditor1.Visible = True
                    Me.FilterEditor1.SortFieldList = False
                    Select Case Me.SelecttedGrid
                        Case GridSelect.GridPencapaian
                            Me.FilterEditor1.SourceControl = Me.grdPencapaian
                            Me.grdPencapaian.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            Me.grdPencapaian.RemoveFilters()
                            Me.clsPO.ViewTargetReaching().RowFilter = ""
                            Me.BinfGrid(Me.grdPencapaian, Me.clsPO.ViewTargetReaching())
                            'Me.dtPicFrom.Text = ""
                            'Me.dtpicUntil.Text = ""
                            'Me.dtPicFrom.Enabled = False
                            'Me.dtpicUntil.Enabled = False
                            'Me.btnAplyRange.Enabled = False
                        Case GridSelect.GridPO
                            Me.FilterEditor1.SourceControl = Me.grdPurchaseOrder
                            Me.grdPurchaseOrder.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            Me.grdPurchaseOrder.RemoveFilters()
                            Me.clsPO.ViewPORegistering().RowFilter = ""
                            Me.BinfGrid(Me.grdPurchaseOrder, Me.clsPO.ViewPORegistering())
                            Me.dtPicFrom.Text = ""
                            Me.dtpicUntil.Text = ""
                            Me.dtPicFrom.Enabled = False
                            Me.dtpicUntil.Enabled = False
                            Me.btnAplyRange.Enabled = False
                    End Select
                    Me.GetStateChecked(Me.btnCustomFilter)
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Select Case Me.SelecttedGrid
                        Case GridSelect.GridPencapaian
                            Me.grdPencapaian.RemoveFilters()
                            Me.GetStateChecked(Me.btnFilterEqual)
                            Me.grdPencapaian.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            'Me.btnAplyRange.Enabled = True
                            'Me.dtPicFrom.Enabled = True
                            'Me.dtpicUntil.Enabled = True
                        Case GridSelect.GridPO
                            Me.grdPurchaseOrder.RemoveFilters()
                            Me.GetStateChecked(Me.btnFilterEqual)
                            Me.btnAplyRange.Enabled = True
                            Me.dtPicFrom.Enabled = True
                            Me.dtpicUntil.Enabled = True
                            Me.grdPurchaseOrder.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    End Select
                Case "btnAddNew"
                Case "btnPOHeader"
                    Dim POHeader As New EntryPOHeader() : POHeader.frmParent = Me
                    With POHeader
                        .ShowInTaskbar = False : .InitializeData() : .StartPosition = FormStartPosition.CenterScreen
                        .ShowDialog(Me) ': Me.btnAplyRange_Click(Me.btnAplyRange, New EventArgs())
                    End With
                    If MustReload Then
                        ReloadData()
                    End If
                Case "btnPOHeaderAndDetail"
                    Dim frmPurchaseOrder As New Purchase_Order() : frmPurchaseOrder.frmParent = Me
                    frmPurchaseOrder.ShowInTaskbar = False
                    frmPurchaseOrder.Mode = Purchase_Order.ModeSave.Save
                    frmPurchaseOrder.InitializeData()
                    Me.GetStateChecked(Me.btnManagePO)
                    frmPurchaseOrder.ShowDialog(Me)
                    Me.ClearCheckedState()
                    If MustReload Then
                        ReloadData()
                    End If
                Case "btnExport"
                    Select Case Me.SelecttedGrid
                        Case GridSelect.GridPencapaian
                            Me.SaveFileDialog1.Title = "Define the location File"
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            Me.SaveFileDialog1.RestoreDirectory = True
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.grdPencapaian
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Case GridSelect.GridPO
                            Me.SaveFileDialog1.Title = "Define the location File"
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.grdPurchaseOrder
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                    End Select

                Case "btnEdit"
                    If Me.grdPurchaseOrder.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Then
                        If Me.grdPurchaseOrder.GetRow().Group.Column Is Me.grdPurchaseOrder.RootTable.Columns("PO_REF_NO") Then
                            While Not Me.grdPurchaseOrder.GetRow().RowType = Janus.Windows.GridEX.RowType.Record
                                Me.grdPurchaseOrder.MoveNext()
                            End While
                        End If
                    ElseIf Me.grdPurchaseOrder.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                    Else
                        Me.ShowMessageInfo("Please Define the row" & vbCrLf & "The row must be record type")
                        Return
                    End If
                    Dim frmPurchaseOrder As New Purchase_Order()
                    frmPurchaseOrder.frmParent = Me
                    frmPurchaseOrder.ShowInTaskbar = False
                    frmPurchaseOrder.Mode = Purchase_Order.ModeSave.Update
                    frmPurchaseOrder.PORef = Me.grdPurchaseOrder.GetValue("PO_REF_NO")
                    frmPurchaseOrder.PO_REF_DATE = Convert.ToDateTime(Me.grdPurchaseOrder.GetValue("PO_REF_DATE"))
                    frmPurchaseOrder.DistributorID = Me.grdPurchaseOrder.GetValue("DISTRIBUTOR_ID")
                    Dim ProjRefNo As Object = Me.grdPurchaseOrder.GetValue("PROJ_REF_NO")
                    If Not IsDBNull(ProjRefNo) And Not IsNothing(ProjRefNo) Then
                        frmPurchaseOrder.PROJ_REF_NO = ProjRefNo.ToString()
                    End If
                    frmPurchaseOrder.InitializeData()
                    frmPurchaseOrder.btnAddNew.Enabled = False
                    Me.GetStateChecked(Me.btnEdit)
                    frmPurchaseOrder.ShowDialog(Me)
                    Me.ClearCheckedState()
                    If MustReload Then : ReloadData() : End If
                Case "btnRefresh"
                    'Me.LoadData()
                    Me.ReloadData()

            End Select
            Me.StatProg = StatusProgress.None
            Me.SFm = StateFillingMCB.HasFilled : Me.SFG = StateFillingGrid.HasFilled
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.SFm = StateFillingMCB.HasFilled : Me.SFG = StateFillingGrid.HasFilled : Me.MustReload = False : Me.ClearCheckedState()
        Finally
            Me.MustReload = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            'Me.myExpand.Expanded = False
            Me.FilterEditor1.Visible = False
            'Me.HASLOAD = True
            Me.dtPicFrom.Value = NufarmBussinesRules.SharedClass.ServerDate().AddDays(-1)
            Me.dtpicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate()
            Me.pnlPencapaian.Expanded = False
            Me.btnAplyRange_Click(Me.btnAplyRange, New EventArgs())
            Me.AcceptButton = Me.btnAplyRange
        Catch ex As Exception
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_PO_Load")
        Finally
            Me.ReadAcces() : CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdPurchaseOrder_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdPurchaseOrder.DeletingRecord
        Try
            If Me.grdPurchaseOrder.Row <= -1 Then
                Return
            End If
            Dim createdDate As Object = Nothing

            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                If Me.clsPO.PO_BRANDPACK_HasReferencedData(Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID").ToString(), False) = True Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    e.Cancel = True
                    Me.grdPurchaseOrder.Refetch()
                    Me.grdPurchaseOrder.SelectCurrentCellText()
                    Return
                End If
                If Not IsNothing(Me.grdPurchaseOrder.GetValue("CREATE_DATE")) And Not IsDBNull(Me.grdPurchaseOrder.GetValue("CREATE_DATE")) Then
                    If CMain.IsSystemAdministrator Or NufarmBussinesRules.User.UserLogin.IsAdmin Then
                    Else
                        If Not IsNothing(Me.grdPurchaseOrder.GetValue("CREATE_DATE")) And Not IsDBNull(Me.grdPurchaseOrder.GetValue("CREATE_DATE")) Then
                            'chek apakah data sudah ada OA_ORIGINAL nya
                            createdDate = Convert.ToDateTime(Me.grdPurchaseOrder.GetValue("CREATE_DATE"))
                            Dim nDay As Integer = DateDiff(DateInterval.Day, createdDate, NufarmBussinesRules.SharedClass.ServerDate)
                            If nDay > 3 Then
                                MessageBox.Show("Can not delete data" & vbCrLf & "data can be deleted before 3 days from which it is made" & vbCrLf & "You can contact Admin or SAE to delete PO", "Data has Already been inserted " & nDay.ToString() & " days before", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                e.Cancel = True
                                Return
                            End If
                        End If
                    End If
                ElseIf Not IsDBNull(Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID")) And Not IsNothing(Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID")) Then
                    Me.ShowMessageInfo("Can not delete data" & vbCrLf & "Unknown CreatedDate data") : e.Cancel = True : Return
                End If
                If Not CMain.IsSystemAdministrator Then
                    If IsNothing(Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID")) Or IsDBNull(Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID")) Then
                        If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.EntryPOHeader Then
                            'CHECK SIAPA YANG NGINPUT PO INI
                            Dim userPO As String = NufarmBussinesRules.User.UserLogin.UserName
                            Dim userCreatedBy As String = Me.clsPO.getCreateBy(Me.grdPurchaseOrder.GetValue("PO_REF_NO").ToString())
                            If Not userCreatedBy = userPO Then
                                Me.ShowMessageInfo("Sory,you have no priviledge to delete PO Header") : e.Cancel = True : Return
                            End If
                        End If
                    ElseIf Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PO Then
                        Me.ShowMessageInfo("Sory ,you have no priviledge to delete item PO") : e.Cancel = True : Return
                    End If
                End If
                If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                'If (Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID") Is DBNull.Value) Or (Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID") Is Nothing) Then
                '    Me.clsPO.Delete(Me.grdPurchaseOrder.GetValue("PO_REF_NO").ToString())
                'ElseIf Me.clsPO.DeletePO_BRANDPACK(Me.grdPurchaseOrder.GetValue("PO_REF_NO").ToString(), Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID")) > 0 Then
                '    Me.ShowMessageInfo(Me.MessageSuccesDelete)
                '    'Else
                '    '    Me.ShowMessageInfo("No row's been deleted !")
                'End If
                If Not IsDBNull(Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID")) And Not IsNothing(Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID")) Then
                    Me.clsPO.DeletePOBrandPack(Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID").ToString(), False)
                    e.Cancel = False : Me.btnAplyRange_Click(Me.btnAplyRange, New EventArgs())
                Else
                    Me.clsPO.DeletePO(Me.grdPurchaseOrder.GetValue("PO_REF_NO").ToString(), False)
                End If
                e.Cancel = False ': Me.btnAplyRange_Click(Me.btnAplyRange, New EventArgs())
                'Me.RefreshData()
            End If
            ''Me.grdPurchaseOrder.ExpandRecords()
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdPurchaseOrder_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdPurchaseOrder_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPurchaseOrder.Enter
        Try

            Me.SelecttedGrid = GridSelect.GridPO
            Dim S As Boolean = Me.FilterEditor1.Visible
            Me.FilterEditor1.SourceControl = Me.grdPurchaseOrder
            Me.FilterEditor1.Visible = S
            Me.grdPurchaseOrder.BackColor = Color.FromArgb(158, 190, 245)
            Me.grdPurchaseOrder.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.grdPurchaseOrder.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.grdPurchaseOrder.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
            Me.grdPurchaseOrder.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
            Me.grdPencapaian.BackColor = Color.FromArgb(194, 217, 247)
            Me.grdPencapaian.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
            Me.grdPencapaian.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdPurchaseOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdPurchaseOrder.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If Me.grdPurchaseOrder.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Then
                    If Me.grdPurchaseOrder.GetRow().Group.Column Is Me.grdPurchaseOrder.RootTable.Columns("PO_REF_NO") Then
                        If Me.clsPO.HasReferencedData(Me.grdPurchaseOrder.GetRow().GroupCaption) = True Then
                            Me.ShowMessageInfo(Me.MessageCantDeleteData)
                            Me.grdPurchaseOrder.Refetch()
                        ElseIf Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                            Me.grdPurchaseOrder.Refetch()
                        Else
                            Me.clsPO.Delete(Me.grdPurchaseOrder.GetRow().GroupCaption)
                            Me.ShowMessageInfo(Me.MessageSuccesDelete)
                            Me.RefreshData()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdPurchaseOrder_KeyDown")
        End Try
    End Sub

    Private Sub chkShowAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.BinfGrid(Me.grdPurchaseOrder, Me.clsPO.GetDataViewForGridEx1())
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_chkShowAll_CheckedChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cmbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFm = StateFillingMCB.Filling Then : Return : End If
            If IsNothing(Me.cmbDistributor.Value) Then : Me.BinfGrid(Me.grdPurchaseOrder, Nothing) : Return
            ElseIf IsNothing(Me.cmbDistributor.SelectedItem) Then : Return
            ElseIf Me.cmbDistributor.SelectedIndex <= -1 Then : Return
            ElseIf Me.cmbDistributor.Text = "" Then : Return
            End If
            'Me.clsPO.ViewPORegistering().RowFilter = Me.GetFilterDate()
            Me.ReloadData()
            Me.clsPO.CreateViewAgreement(Me.cmbDistributor.Value.ToString())
            Me.SFm = StateFillingMCB.Filling : Me.cmbAgreement.DataSource = Me.clsPO.ViewAgreement()
            Me.cmbAgreement.DisplayMember = "AGREEMENT_NO" : Me.cmbAgreement.ValueMember = "AGREEMENT_NO"
            Me.cmbFlag.DataSource = Nothing : Me.cmbFlag.Text = ""
            'Me.pnlPencapaian.Expanded = True
            'If filterdate <> "" Then

            'End If
            Me.StatProg = StatusProgress.None
            Me.cmbDistributor.Enabled = True
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled : Me.StatProg = StatusProgress.None
            Me.cmbDistributor.Enabled = True
        Finally
            Me.SFm = StateFillingMCB.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        Try
            Dim dtView As DataView = CType(Me.cmbDistributor.DataSource, DataView)
            dtView.RowFilter = "DISTRIBUTOR_NAME LIKE '%" & Me.cmbDistributor.Text & "%'"
            Me.cmbDistributor.DropDownList().Refetch()
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ReloadData()
        If (Me.clsPO Is Nothing) Then : Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PORegistering() : End If
        If Me.btnbyRangedDate.Checked Then
            'checked valid date po,jika di from atau until ada yang tidak di pilih
            'tampilkan message
            If Not Me.IsValidDate() Then : Return : End If
            Me.ShowThread()
            Me.clsPO.CreateViewBrandPackByDistributorID(NufarmBussinesRules.PurchaseOrder.PORegistering.FilterDatePOWith.DatePO, Me.cmbDistributor.Value, _
                              Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
            'If Me.dtPicFrom.Text = "" And Me.dtpicUntil.Text = "" Then
            '    Me.clsPO.CreateViewBrandPackByDistributorID(NufarmBussinesRules.PurchaseOrder.PORegistering.FilterDatePOWith.DatePO, Me.cmbDistributor.Value, _
            '    Nothing, Nothing)
            'Else

            'End If
        ElseIf Me.btnByRangeDateAndCancelPO.Checked Then
            If Not Me.IsValidDate() Then : Return : End If
            Me.ShowThread()
            Me.clsPO.CreateViewBrandPackByDistributorID(NufarmBussinesRules.PurchaseOrder.PORegistering.FilterDatePOWith.CancelPO, Me.cmbDistributor.Value, _
                              Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
        ElseIf Me.btnByRangeDateAndPlantation.Checked Then
            If Not Me.IsValidDate() Then : Return : End If
            Me.ShowThread()
            Me.clsPO.CreateViewBrandPackByDistributorID(NufarmBussinesRules.PurchaseOrder.PORegistering.FilterDatePOWith.Plantation, Me.cmbDistributor.Value, _
                              Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
        ElseIf Me.btnbyPO.Checked Then
            If Me.txtSearchPO.Text = "" Then
                Me.ShowMessageInfo("Please enter PO NO to search") : Return
            End If
            Me.ShowThread()
            Me.clsPO.CreateViewBrandPackByDistributorID(RTrim(Me.txtSearchPO.Text), Me.cmbDistributor.Value)
        Else : Return
        End If

        'If Not IsNothing(Me.cmbDistributor.SelectedItem) Then
        '    'If Not IsNothing(Me.clsPO.ViewPORegistering()) Then
        '    '    Me.clsPO.ViewPORegistering.RowFilter = Me.GetFilterDate()
        '    'End If

        'Else
        '    If (Me.clsPO Is Nothing) Then : Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PORegistering() : End If
        '    If Me.dtPicFrom.Text <> "" And Me.dtpicUntil.Text <> "" Then
        '        Me.clsPO.CreateViewBrandPackByDistributorID(Me.cmbDistributor.Value.ToString(), _
        '        Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
        '    ElseIf Me.dtPicFrom.Text <> "" Then
        '        Me.clsPO.CreateViewBrandPackByDistributorID(Me.cmbDistributor.Value.ToString(), _
        '         Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Nothing)
        '    ElseIf Me.dtpicUntil.Text <> "" Then
        '        Me.clsPO.CreateViewBrandPackByDistributorID(Me.cmbDistributor.Value.ToString(), _
        '        Nothing, Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
        '    ElseIf Me.dtPicFrom.Text = "" And Me.dtpicUntil.Text = "" Then
        '        Me.clsPO.CreateViewBrandPackByDistributorID(Me.cmbDistributor.Value.ToString(), _
        '        Nothing, Nothing)
        '    End If
        'End If
        Me.BinfGrid(Me.grdPurchaseOrder, Me.clsPO.ViewPORegistering())
        'AUTOSIZE COLUMNS
        Me.grdPurchaseOrder.AutoSizeColumns(Me.grdPurchaseOrder.RootTable)
    End Sub
    Private Sub btnAplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ReloadData()
            'If NufarmBussinesRules.User.UserLogin.IsAdmin Then
            '    Me.grdPurchaseOrder.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            'Else
            '    Me.grdPurchaseOrder.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            'End If
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnAplyRange_Click")
        Finally
            Me.MustReload = False : Me.cmbDistributor.Enabled = True : Me.SFm = StateFillingMCB.HasFilled : Me.SFG = StateFillingGrid.HasFilled : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbAgreement_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAgreement.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFm = StateFillingMCB.Filling Then
                Return
            End If
            If Me.cmbAgreement.SelectedItem Is Nothing Then
                Return
            End If
            'Me.clsPO.CreateViewTargetReaching(Me.cmbAgreement.SelectedValue.ToString(), _
            'Me.cmbDistributor.Value.ToString(), Me.cmbFlag.SelectedValue.ToString())
            'Me.BinfGrid(Me.grdPencapaian, Me.clsPO.ViewTargetReaching())
            Me.cmbFlag.DataSource = Nothing
            Dim Flag As Object = Me.clsPO.getFlag(Me.cmbAgreement.SelectedValue.ToString())
            If Not IsNothing(Flag) Then
                Select Case Flag.ToString()
                    Case "Q"
                        Me.CreateViewFlag("Q")
                        Me.cmbFlag.DataSource = Me.m_ViewFlag
                        Me.cmbFlag.DisplayMember = "QUARTER"
                        Me.cmbFlag.ValueMember = "QUARTER"
                    Case "S"
                        Me.CreateViewFlag("S")
                        Me.cmbFlag.DataSource = Me.m_ViewFlag
                        Me.cmbFlag.DisplayMember = "SEMESTER"
                        Me.cmbFlag.ValueMember = "SEMESTER"
                End Select
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ButtonSearch1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearch1.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsPO.ViewAgreement.RowFilter = "AGREEMENT_NO LIKE '%" & Me.cmbAgreement.Text & "%'"
            Me.cmbAgreement.DataSource = Me.clsPO.ViewAgreement()
            Dim itemCount As Int32 = Me.cmbAgreement.Items.Count
            MessageBox.Show(itemCount.ToString() & "item(s) found.")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnAplyFilterPencapaian_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyFilterPencapaian.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'CHEK validity
            If Me.cmbDistributor.SelectedItem Is Nothing Then
                Me.baseTooltip.Show("Please define distributor.", Me.cmbDistributor, 2500)
                Return
            ElseIf (Me.cmbAgreement.SelectedValue Is Nothing) Then
                Me.baseTooltip.Show("Please define Agreement.", Me.cmbAgreement, 2500)
                Return
            ElseIf Me.cmbFlag.SelectedValue Is Nothing Then
                Me.baseTooltip.Show("Please define Flag.", Me.cmbFlag, 2500)
                Return
            End If
            Dim Flag As String = ""
            Select Case Me.cmbFlag.SelectedValue.ToString()
                Case "SEMESTER1"
                    Flag = "S1"
                Case "SEMESTER2"
                    Flag = "S2"
                Case "QUARTER1"
                    Flag = "Q1"
                Case "QUARTER2"
                    Flag = "Q2"
                Case "QUARTER3"
                    Flag = "Q3"
                Case "QUARTER4"
                    Flag = "Q4"
                Case "YEAR"
                    Flag = "Y"
                Case Else
                    Me.baseTooltip.Show("Unknown flag !.", Me.cmbFlag, 2500)
                    Return
            End Select
            Me.clsPO.CreateViewTargetReaching(Me.cmbAgreement.SelectedValue().ToString(), Me.cmbDistributor.Value.ToString(), Flag)
            Me.BinfGrid(Me.grdPencapaian, Me.clsPO.ViewTargetReaching())
       
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnAplyFilterPencapaian_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdPencapaian_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPencapaian.Enter
        Me.SelecttedGrid = GridSelect.GridPencapaian ' SelectedGrid.GridEx1
        Dim S As Boolean = Me.FilterEditor1.Visible
        Me.FilterEditor1.SourceControl = Me.grdPencapaian
        Me.FilterEditor1.Visible = S
        Me.grdPencapaian.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdPencapaian.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdPencapaian.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdPencapaian.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.grdPencapaian.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        'ME.GridEX1.SelectedFormatStyle.ForeColor = SYSTEM.Drawing.Color.
        Me.grdPurchaseOrder.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdPurchaseOrder.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdPurchaseOrder.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)

    End Sub

    Private Sub pnlPencapaian_ExpandedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles pnlPencapaian.ExpandedChanged
        If Me.cmbDistributor.SelectedIndex <= -1 Then
            Me.pnlPencapaian.Expanded = False
        End If
    End Sub

    Private Sub btnbyPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbyPO.Click
        Me.SetRangeDateFilter(Me.btnbyPO, Not Me.btnbyPO.Checked)
        'If Me.btnbyPO.Checked Then 'menu di check,
        '    'menu checklistnya di lepaskan
        '    'check apakah btnbyrangedDate  di checklist/ tidak
        '    If Me.btnbyRangedDate.Checked = False Then
        '        'rangedate dan by po dua-duanya di gak di checklist
        '        Me.dtPicFrom.Enabled = False : Me.dtPicFrom.Text = "" : Me.lblPOOrEndDate.Text = "UNTIL"
        '        Me.dtpicUntil.Visible = True : Me.dtpicUntil.Enabled = False : Me.dtpicUntil.Text = ""
        '        Me.txtSearchPO.Text = ""
        '        Me.txtSearchPO.Visible = False : Me.btnAplyRange.Text = "Apply filter"
        '        Me.btnAplyRange.Enabled = False : Me.BinfGrid(Me.grdPurchaseOrder, Nothing)
        '        Me.SFG = StateFillingGrid.Filling : Me.cmbDistributor.Value = Nothing : Me.cmbDistributor.Text = ""
        '        Me.SFG = StateFillingGrid.HasFilled
        '    Else
        '        'range data PO di checklist
        '        Me.dtPicFrom.Enabled = True : Me.lblPOOrEndDate.Text = "UNTIL"
        '        Me.dtpicUntil.Visible = True : Me.txtSearchPO.Text = ""
        '        Me.dtpicUntil.Enabled = True
        '        Me.txtSearchPO.Visible = False : Me.btnAplyRange.Text = "Apply filter"
        '        Me.btnAplyRange.Enabled = True
        '    End If
        '    Me.btnbyPO.Checked = False
        'Else
        '    'check apakah btnby range data juga di checklist / tidak
        '    Me.btnbyRangedDate.Checked = False

        '    Me.dtPicFrom.Enabled = False : Me.dtPicFrom.Text = "" : Me.lblPOOrEndDate.Text = "Search PO NO"
        '    Me.txtSearchPO.Visible = True : Me.txtSearchPO.Text = ""
        '    Me.dtpicUntil.Visible = False : Me.btnAplyRange.Text = "Find PO"
        '    Me.btnbyPO.Checked = True
        '    Me.btnAplyRange.Enabled = True
        'End If
    End Sub

    Private Sub SetRangeDateFilter(ByVal btn As DevComponents.DotNetBar.ButtonItem, ByVal IsChecked As Boolean)
        'lepasin semua chek
        Me.btnbyPO.Checked = False
        Me.btnByRangeDateAndCancelPO.Checked = False
        Me.btnByRangeDateAndPlantation.Checked = False
        Me.btnbyRangedDate.Checked = False
        If IsChecked Then 'menu di chedked
            Select Case btn.Name
                Case "btnbyPO"
                    Me.btnbyRangedDate.Checked = False
                    Me.dtPicFrom.Enabled = False : Me.dtPicFrom.Text = "" : Me.lblPOOrEndDate.Text = "Search PO NO"
                    Me.txtSearchPO.Visible = True : Me.txtSearchPO.Text = ""
                    Me.dtpicUntil.Visible = False : Me.btnAplyRange.Text = "Find PO"
                    'Me.btnbyPO.Checked = True
                    Me.btnAplyRange.Enabled = True
                Case "btnByRangeDateAndCancelPO", "btnByRangeDateAndPlantation", "btnbyRangedDate"
                    Me.btnbyPO.Checked = False
                    Me.dtPicFrom.Enabled = True : Me.lblPOOrEndDate.Text = "UNTIL"
                    If Not Me.dtpicUntil.Visible Then
                        Me.dtpicUntil.Text = "" : Me.dtPicFrom.Text = ""
                    End If
                    Me.txtSearchPO.Visible = False : Me.txtSearchPO.Text = ""
                    Me.dtpicUntil.Visible = True : Me.dtpicUntil.Enabled = True
                    Me.btnAplyRange.Text = "Apply filter"
                    'Me.btnbyRangedDate.Checked = True
                    Me.btnAplyRange.Enabled = True
            End Select
            btn.Checked = True
        Else
            'menu dilepasin semua'
            Me.dtPicFrom.Enabled = False : Me.dtPicFrom.Text = "" : Me.lblPOOrEndDate.Text = "UNTIL"
            Me.dtpicUntil.Visible = True : Me.dtpicUntil.Enabled = False : Me.dtpicUntil.Text = ""
            Me.txtSearchPO.Text = ""
            Me.txtSearchPO.Visible = False : Me.btnAplyRange.Text = "Apply filter"
            Me.btnAplyRange.Enabled = False : Me.BinfGrid(Me.grdPurchaseOrder, Nothing)
            Me.SFG = StateFillingGrid.Filling : Me.cmbDistributor.Value = Nothing : Me.cmbDistributor.Text = ""
            Me.SFG = StateFillingGrid.HasFilled
            btn.Checked = False
        End If
        Me.grdPurchaseOrder.SetDataBinding(Nothing, "")
    End Sub
    Private Sub btnbyRangedDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbyRangedDate.Click
        Me.SetRangeDateFilter(Me.btnbyRangedDate, Not Me.btnbyRangedDate.Checked)

        'If Me.btnbyRangedDate.Checked Then 'menu di check
        '    'menu checklist nya di lepaskan
        '    'check apakah by po di chekclist juga
        '    'If Me.btnbyPO.Checked = False Then
        '    '    'dua duanya menu tidak di cheklist
        '    '    Me.dtPicFrom.Enabled = False : Me.dtPicFrom.Text = "" : Me.lblPOOrEndDate.Text = "UNTIL"
        '    '    Me.dtpicUntil.Visible = True : Me.dtpicUntil.Enabled = False : Me.dtpicUntil.Text = ""
        '    '    Me.txtSearchPO.Text = ""
        '    '    Me.txtSearchPO.Visible = False : Me.btnAplyRange.Text = "Apply filter"
        '    '    Me.btnAplyRange.Enabled = False : Me.BinfGrid(Me.grdPurchaseOrder, Nothing)
        '    '    Me.SFG = StateFillingGrid.Filling : Me.cmbDistributor.Value = Nothing : Me.cmbDistributor.Text = ""
        '    '    Me.SFG = StateFillingGrid.HasFilled
        '    'Else
        '    '    Me.dtPicFrom.Enabled = False : Me.dtpicUntil.Visible = False
        '    '    Me.dtPicFrom.Text = "" : Me.dtpicUntil.Text = ""
        '    '    Me.txtSearchPO.Text = "" : Me.txtSearchPO.Visible = True
        '    '    Me.lblPOOrEndDate.Text = "Search PO NO" : Me.btnAplyRange.Text = "Find PO"
        '    '    Me.btnAplyRange.Enabled = True

        '    'End If
        '    'Me.btnbyRangedDate.Checked = False
        '    Me.SetRangeDateFilter(Me.btnbyRangedDate, False)
        'Else 'check apakah btn by po juga di check list
        '    'Me.btnbyPO.Checked = False
        '    'Me.dtPicFrom.Enabled = True : Me.lblPOOrEndDate.Text = "UNTIL"
        '    'Me.dtpicUntil.Text = "" : Me.dtPicFrom.Text = ""
        '    'Me.txtSearchPO.Visible = False : Me.txtSearchPO.Text = ""
        '    'Me.dtpicUntil.Visible = True : Me.dtpicUntil.Enabled = True
        '    'Me.btnAplyRange.Text = "Apply filter"

        '    'Me.btnbyRangedDate.Checked = True
        '    'Me.btnAplyRange.Enabled = True
        '    Me.SetRangeDateFilter(Me.btnbyRangedDate, True)
        'End If
    End Sub

    'Private Sub dtpicUntil_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpicUntil.KeyDown
    '    If e.KeyCode = Keys.Delete Then
    '        Me.dtpicUntil.Text = ""
    '    End If
    'End Sub
    Private Sub btnByRangeDateAndCancelPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnByRangeDateAndCancelPO.Click
        Me.SetRangeDateFilter(Me.btnByRangeDateAndCancelPO, Not Me.btnByRangeDateAndCancelPO.Checked)
    End Sub

    Private Sub btnByRangeDateAndPlantation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnByRangeDateAndPlantation.Click
        Me.SetRangeDateFilter(Me.btnByRangeDateAndPlantation, Not Me.btnByRangeDateAndPlantation.Checked)
    End Sub
    Private Sub grdPurchaseOrder_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdPurchaseOrder.CellUpdated
        If Not CMain.FormLoading = Main.StatusForm.HasLoaded Then
            Return
        End If
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor
            If (e.Column.Key = "ExcludeDPD") Then
                ''UPDATE data
                Dim C As Boolean = IIf(IsDBNull(Me.grdPurchaseOrder.GetValue(e.Column)), False, CBool(Me.grdPurchaseOrder.GetValue(e.Column)))

                Me.clsPO.SetExecludeDPD(C, Me.grdPurchaseOrder.GetValue("PO_BRANDPACK_ID").ToString())
                Me.SFG = StateFillingGrid.Filling : Me.grdPurchaseOrder.UpdateData() : Me.SFG = StateFillingGrid.HasFilled : Me.Cursor = Cursors.Default
            Else
                Me.grdPurchaseOrder.CancelCurrentEdit()
            End If

        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.Cursor = Cursors.Default
        End Try

    End Sub
#End Region

End Class
