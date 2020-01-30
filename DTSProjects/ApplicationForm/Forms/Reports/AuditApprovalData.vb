Imports System.Threading

Public Class AuditApprovalData
    Private DS As DataSet = Nothing
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private LD As Loading
    Private isChecking As Boolean = False
    Private IsLoadingRow As Boolean = False

    Private Enum StatusProgress
        None
        Processing
    End Enum
    Private Sub ShowProceed()
        Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Thread.Sleep(50) : Me.LD.Refresh() : Application.DoEvents()
        End While
        Thread.Sleep(100)
        Me.LD.Close() : Me.LD = Nothing
    End Sub
    Dim m_clsAudit As NufarmBussinesRules.Audit.MasterData = Nothing
    Friend CMain As Main
    Private WithEvents DP As DefinePeriode
    Private ReadOnly Property clsAudit() As NufarmBussinesRules.Audit.AuditApprovalData
        Get
            If IsNothing(m_clsAudit) Then
                m_clsAudit = New NufarmBussinesRules.Audit.AuditApprovalData()
            End If
            Return m_clsAudit
        End Get
    End Property
    Private BoundariStartPKD As Object = Nothing, BoundaryEndPKD As Object = Nothing
    Private Function GetFlag() As String
        Return Me.cmbFlag.SelectedItem.ToString()
    End Function
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Me.Cursor = Cursors.WaitCursor
        Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
        Try
            Select Case item.Name
                Case "btnShowFieldChooser"
                    Me.GridEX1.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog()
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
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
                    Me.FilterEditor1.Visible = True
                    Me.FilterEditor1.SortFieldList = False
                    Me.FilterEditor1.SourceControl = Me.GridEX1
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.GridEX1.RemoveFilters()
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.GridEX1.RemoveFilters()
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Case "btnExport"
                    Dim SFD As New SaveFileDialog()
                    With SFD
                        .Title = "Define the location file"
                        .OverwritePrompt = True
                        .DefaultExt = ".xls"
                        .Filter = "excel file|*.xls"
                        .InitialDirectory = "C:\"
                        If SFD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                            Using FS As New System.IO.FileStream(SFD.FileName, IO.FileMode.Create)
                                Using Exporter As New Janus.Windows.GridEX.Export.GridEXExporter()
                                    Exporter.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                                    Exporter.IncludeHeaders = True
                                    Exporter.IncludeExcelProcessingInstruction = True
                                    Exporter.IncludeChildTables = True
                                    Exporter.IncludeFormatStyle = True
                                    Exporter.GridEX = Me.GridEX1
                                    Exporter.Export(FS) : FS.Flush()
                                    Me.ShowMessageInfo("Data exported to " & SFD.FileName)
                                End Using
                            End Using
                        End If
                    End With
                Case "btnSetPeriodePKD"
                    Me.btnSetPeriodePKD.Checked = (Not Me.btnSetPeriodePKD.Checked)
                    If btnSetPeriodePKD.Checked Then
                        If IsNothing(Me.DP) OrElse Me.DP.IsDisposed Then
                            Me.DP = New DefinePeriode()
                        End If
                        Dim StartDate As DateTime = New DateTime(NufarmBussinesRules.SharedClass.ServerDate.Year - 1, 8, 1)
                        Dim EndDate As DateTime = New DateTime(NufarmBussinesRules.SharedClass.ServerDate.Year, 7, 31)
                        If Not IsNothing(Me.BoundariStartPKD) Then
                            StartDate = Convert.ToDateTime(Me.BoundariStartPKD)
                            EndDate = Convert.ToDateTime(Me.BoundaryEndPKD)
                        Else
                            Me.clsAudit.getCurrentPKD(StartDate, EndDate, False)
                        End If
                        With DP
                            .ShowInTaskbar = False
                            .dtPicFilterStart.Value = StartDate
                            .dtPicFilterEnd.Value = EndDate
                            .StartPosition = FormStartPosition.CenterParent
                            .WindowState = FormWindowState.Normal
                            .ShowDialog()
                        End With
                    End If
                    'If btnSetPeriodePKD.Checked Then
                    '    Me.StatProg = StatusProgress.Processing
                    '    Me.ThreadProgress = New Thread(AddressOf ShowProceed)
                    '    Me.ThreadProgress.Start()
                    '    If Me.btnAdjusmentPKD.Checked Then
                    '        Me.LoadData(btnAdjusmentPKD, False)
                    '    ElseIf Me.btnAveragePrice.Checked Then
                    '        Me.LoadData(Me.btnAveragePrice, False)
                    '    ElseIf Me.btnDPD.Checked Then
                    '        Me.LoadData(btnDPD, False)
                    '    ElseIf Me.btnPriceFM.Checked Then
                    '        Me.btnPriceFM.Checked = False
                    '        If Not IsNothing(DS) Then
                    '            DS.RejectChanges()
                    '        End If
                    '        Me.GridEX1.SetDataBinding(Nothing, "")
                    '    ElseIf Me.btnPricePL.Checked Then
                    '        btnPricePL.Checked = False
                    '        If Not IsNothing(DS) Then
                    '            DS.RejectChanges()
                    '        End If
                    '        Me.GridEX1.SetDataBinding(Nothing, "")
                    '    ElseIf Me.btnSalesProgram.Checked Then
                    '        Me.LoadData(btnSalesProgram, False)
                    '    ElseIf Me.btnTargetPKD.Checked Then
                    '        Me.LoadData(btnTargetPKD, False)
                    '    ElseIf Me.btnCPDAuto.Checked Then
                    '        Me.LoadData(Me.btnCPDAuto, False)
                    '    End If
                    'End If
                    Me.cmbFlag.Enabled = Me.btnSetPeriodePKD.Checked
            End Select
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.IsLoadingRow = False
            Me.StatProg = StatusProgress.None
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
            Me.ShowMessageInfo(ex.Message)
            If item.Name = "btnSetPeriodePKD" Then
                btnSetPeriodePKD.Checked = (Not Me.btnSetPeriodePKD.Checked)
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Try
            If IsNothing(Me.DS) Then
                Return
            ElseIf Not Me.DS.HasChanges() Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            If DS.HasChanges() Then
                Me.StatProg = StatusProgress.Processing
                Me.ThreadProgress = New Thread(AddressOf ShowProceed)
                Me.ThreadProgress.Start()
                Dim tblCopy As DataTable = DS.Tables(0).GetChanges(DataRowState.Modified)
                Dim rows() As DataRow = tblCopy.Select("IsApproved = " & True)
                If rows.Length > 0 Then
                    For i As Integer = 0 To rows.Length - 1
                        rows(i).BeginEdit()
                        rows(i)("ApprovedBy") = NufarmBussinesRules.User.UserLogin.UserName
                        rows(i)("ApprovedDate") = NufarmBussinesRules.SharedClass.ServerDate
                        rows(i).EndEdit()
                    Next
                End If
                Dim rows1() As DataRow = tblCopy.Select("IsApproved = " & False)
                If rows1.Length > 0 Then
                    For i As Integer = 0 To rows1.Length - 1
                        rows1(i).BeginEdit()
                        rows1(i)("ApprovedBy") = DBNull.Value
                        rows1(i)("ApprovedDate") = DBNull.Value
                        rows1(i).EndEdit()
                    Next
                End If
                Me.clsAudit.SaveData(tblCopy, False)
                Dim btnItem As DevComponents.DotNetBar.ButtonItem = Nothing
                If Me.btnAdjusmentPKD.Checked Then
                    btnItem = btnAdjusmentPKD
                ElseIf Me.btnAveragePrice.Checked Then
                    btnItem = btnAveragePrice
                ElseIf Me.btnDPD.Checked Then
                    btnItem = btnDPD
                ElseIf Me.btnPriceFM.Checked Then
                    btnItem = btnPriceFM
                ElseIf Me.btnPricePL.Checked Then
                    btnItem = btnPricePL
                ElseIf Me.btnSalesProgram.Checked Then
                    btnItem = btnSalesProgram
                ElseIf Me.btnTargetPKD.Checked Then
                    btnItem = btnTargetPKD
                ElseIf Me.btnCPDAuto.Checked Then
                    btnItem = Me.btnCPDAuto
                End If
                If Not IsNothing(btnItem) Then
                    If Me.OnlyApprovedToolStripMenuItem.Checked Then
                        Me.LoadData(btnItem, True)
                    ElseIf Me.OnlyNotApprovedToolStripMenuItem.Checked Then
                        Me.LoadData(btnItem, False)
                    End If
                End If
            End If
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.IsLoadingRow = False
            Me.StatProg = StatusProgress.None
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
            Me.ShowMessageInfo(ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub BindGrid()
        Me.IsLoadingRow = True
        If IsNothing(Me.DS) Then
            Me.GridEX1.SetDataBinding(Nothing, "")
            Me.IsLoadingRow = False
            Return
        End If

        Me.GridEX1.SetDataBinding(DS.Tables(0).DefaultView, "")
        Me.GridEX1.RetrieveStructure()
        ''set priviledge apakah allow edit/gak
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True

        For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If Item.Type Is Type.GetType("System.DateTime") Then
                Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                Item.FormatString = "dd MMMM yyyy"
            ElseIf Item.Type Is Type.GetType("System.Boolean") Then
                Item.ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox
                Item.FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
            ElseIf (Item.Type Is Type.GetType("System.Decimal")) Or (Item.Type Is Type.GetType("System.Double")) _
                Or Item.Type Is Type.GetType("System.Single") Then
                If (Item.DataMember.Contains("VALUE")) Then
                    Item.FormatString = "#,##0.00"
                    Item.TotalFormatString = "#,##0.00"
                    Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    Item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Item.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Else
                    Item.FormatString = "#,##0.000"
                    Item.TotalFormatString = "#,##0.000"
                    Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    Item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Item.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                End If
            ElseIf (Item.DataMember.Contains("VALUE")) Then
                Item.FormatString = "#,##0.00"
                Item.TotalFormatString = "#,##0.00"
                Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                Item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Item.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ElseIf Item.DataMember.Contains("_ID") Then
                Item.Visible = False
            End If
            Item.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            Item.EditType = Janus.Windows.GridEX.EditType.NoEdit
        Next
        If Me.GridEX1.RootTable.Columns.Contains("IDApp") Then
            Me.GridEX1.RootTable.Columns("IDApp").Visible = False
        End If
        If Me.GridEX1.RootTable.Columns.Contains("CodeApp") Then
            Me.GridEX1.RootTable.Columns("CodeApp").Visible = False
        End If
        Dim AllowUpdateApproval As Boolean = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AuditApprovalData Or Main.IsSystemAdministrator
        If Me.GridEX1.RootTable.Columns.Contains("IsApproved") Then
            If AllowUpdateApproval Then
                Me.GridEX1.RootTable.Columns("IsApproved").EditType = Janus.Windows.GridEX.EditType.CheckBox
            Else
                Me.GridEX1.RootTable.Columns("IsApproved").EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
            Me.GridEX1.RootTable.Columns("IsApproved").Caption = ""
            Me.GridEX1.RootTable.Columns("IsApproved").Width = 21
        End If
        If Me.GridEX1.RootTable.Columns.Contains("IsApproved") Then
            If AllowUpdateApproval Then
                Me.GridEX1.RootTable.Columns("ApprovedDesc").EditType = Janus.Windows.GridEX.EditType.TextBox
            Else
                Me.GridEX1.RootTable.Columns("ApprovedDesc").EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
            Me.GridEX1.RootTable.Columns("ApprovedDesc").Caption = "APPROVED_DESCRIPTION"
            Me.GridEX1.RootTable.Columns("ApprovedDesc").Position = Me.GridEX1.RootTable.Columns("IsApproved").Position + 1
        End If
        If Me.GridEX1.RootTable.Columns.Contains("PROG_BRANDPACK_DIST_ID") Then
            Me.GridEX1.RootTable.Columns("PROG_BRANDPACK_DIST_ID").Visible = False
        End If
        Me.GridEX1.AutoSizeColumns()

        GridEX1.RepeatHeaders = Janus.Windows.GridEX.InheritableBoolean.False
        Me.GridEX1.RootTable.TableHeader = Janus.Windows.GridEX.InheritableBoolean.True
        Me.GridEX1.RootTable.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
        Me.GridEX1.RootTable.TableHeaderFormatStyle.ForeColor = Color.Maroon
        Me.GridEX1.RootTable.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        If Me.OnlyApprovedToolStripMenuItem.Checked Then
            Me.GridEX1.RootTable.Columns("IsApproved").Visible = False
        ElseIf Me.OnlyNotApprovedToolStripMenuItem.Checked Then
            Me.GridEX1.RootTable.Columns("IsApproved").Visible = True
        End If
        Me.btnApprove.Enabled = (AllowUpdateApproval And (Me.GridEX1.RootTable.Columns("IsApproved").Visible = True))
        Me.chkCheckAll.Enabled = Me.btnApprove.Enabled
        IsLoadingRow = False
    End Sub
    Private Sub LoadData(ByVal btnItem As DevComponents.DotNetBar.ButtonItem, ByVal OnlyApproved As Boolean)
        If IsNothing(btnItem) Then : Return : End If
        Dim dt As DataTable = Nothing
        If IsNothing(BoundariStartPKD) Then
            Dim DefStartPKD As Object = Nothing, DefEndPKD As Object = Nothing
            Dim curMonth As Integer = NufarmBussinesRules.SharedClass.ServerDate.Month
            If curMonth >= 8 And curMonth <= 12 Then
                DefStartPKD = New DateTime(NufarmBussinesRules.SharedClass.ServerDate.Year, 8, 1)
                DefEndPKD = New DateTime((NufarmBussinesRules.SharedClass.ServerDate.Year + 1), 7, 31)
            Else
                DefStartPKD = New DateTime(NufarmBussinesRules.SharedClass.ServerDate.Year - 1, 8, 1)
                DefEndPKD = New DateTime((NufarmBussinesRules.SharedClass.ServerDate.Year), 7, 31)
            End If
            BoundariStartPKD = DefStartPKD
            BoundaryEndPKD = DefEndPKD
        End If
        'Me.cmbFlag.Enabled = False

        Select Case btnItem.Name
            Case "btnAdjusmentPKD"
                ''get DataApproval AdjusmentPKD
                dt = Me.clsAudit.getApprovalAdjustment(Convert.ToDateTime(Me.BoundariStartPKD), Convert.ToDateTime(Me.BoundaryEndPKD), OnlyApproved, False)
                Me.btnSetPeriodePKD.Enabled = True
                Me.cmbFlag.SelectedItem = ""
            Case "btnAveragePrice"
                dt = Me.clsAudit.getApprovalAVGPrice(Convert.ToDateTime(Me.BoundariStartPKD), Convert.ToDateTime(Me.BoundaryEndPKD), OnlyApproved, False)
                Me.btnSetPeriodePKD.Enabled = True
                Me.cmbFlag.Text = ""
            Case "btnDPD"
                Me.StatProg = StatusProgress.None
                Me.Cursor = Cursors.Default
                If IsNothing(Me.cmbFlag.SelectedItem) Then
                    Me.ShowMessageInfo("Please choose flag")
                    If Not IsNothing(btnItem) Then
                        btnItem.Checked = (Not btnItem.Checked)
                    End If
                    Cursor = Cursors.Default : Me.cmbFlag.Focus() : Return
                End If
                If String.IsNullOrEmpty(Me.cmbFlag.SelectedItem.ToString()) Then
                    Me.ShowMessageInfo("Please choose flag")
                    If Not IsNothing(btnItem) Then
                        btnItem.Checked = (Not btnItem.Checked)
                    End If
                    Cursor = Cursors.Default : Me.cmbFlag.Focus() : Return
                End If
                dt = Me.clsAudit.getApprovalDPD(Me.GetFlag(), Convert.ToDateTime(Me.BoundariStartPKD), Convert.ToDateTime(Me.BoundaryEndPKD), OnlyApproved, False)
                Me.btnSetPeriodePKD.Enabled = True
                Me.cmbFlag.Enabled = True
            Case "btnPriceFM"
                dt = Me.clsAudit.getApprovalPriceFM(OnlyApproved, False)
                Me.btnSetPeriodePKD.Checked = False
                Me.btnSetPeriodePKD.Enabled = False
                Me.BoundariStartPKD = Nothing
                Me.BoundaryEndPKD = Nothing
                Me.cmbFlag.SelectedItem = ""
            Case "btnSalesProgram"
                dt = Me.clsAudit.getApprovalSalesProgram(Convert.ToDateTime(Me.BoundariStartPKD), Convert.ToDateTime(Me.BoundaryEndPKD), OnlyApproved, False)
                Me.btnSetPeriodePKD.Enabled = True
                Me.cmbFlag.SelectedItem = ""
            Case "btnTargetPKD"
                dt = Me.clsAudit.getApprovalTargetPKD(Convert.ToDateTime(Me.BoundariStartPKD), Convert.ToDateTime(Me.BoundaryEndPKD), OnlyApproved, False)
                Me.btnSetPeriodePKD.Enabled = True
                Me.cmbFlag.SelectedItem = ""
            Case "btnPricePL"
                dt = Me.clsAudit.getApprovalPricePL(Convert.ToDateTime(Me.BoundariStartPKD), Convert.ToDateTime(Me.BoundaryEndPKD), OnlyApproved, False)
                'Me.btnSetPeriodePKD.Checked = False
                'Me.btnSetPeriodePKD.Enabled = False
                'Me.BoundariStartPKD = Nothing
                'Me.BoundaryEndPKD = Nothing
                Me.btnSetPeriodePKD.Enabled = True
                Me.cmbFlag.SelectedItem = ""
            Case "btnCPDAuto"
                dt = Me.clsAudit.getApprovalCPDAuto(Convert.ToDateTime(Me.BoundariStartPKD), Convert.ToDateTime(Me.BoundaryEndPKD), OnlyApproved, False)
                Me.btnSetPeriodePKD.Enabled = True
                Me.cmbFlag.SelectedItem = ""
        End Select
        If Not IsNothing(dt) Then
            DS = New DataSet("DS_APPROVAL")
            DS.Tables.Add(dt)
            DS.AcceptChanges()
            Me.BindGrid()
        Else
            Me.GridEX1.SetDataBinding(Nothing, "")
        End If
        Me.isChecking = True
        Me.chkCheckAll.Checked = False
        Me.isChecking = False
    End Sub
    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Me.Cursor = Cursors.WaitCursor
        Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.ButtonItem)
        Dim btnItem As DevComponents.DotNetBar.ButtonItem = Nothing
        If TypeOf (item) Is DevComponents.DotNetBar.ButtonItem Then
            btnItem = CType(item, DevComponents.DotNetBar.ButtonItem)
            For Each ctrl As DevComponents.DotNetBar.ButtonItem In Me.ItemPanel1.Items
                If Not ctrl.Equals(btnItem) Then
                    ctrl.Checked = False
                End If
            Next
            btnItem.Checked = (Not btnItem.Checked)
        End If
        Try
            If btnItem.Checked Then
                Me.StatProg = StatusProgress.Processing
                Me.ThreadProgress = New Thread(AddressOf ShowProceed)
                Me.ThreadProgress.Start()
            End If
            If Not IsNothing(btnItem) Then
                If btnItem.Checked Then
                    Me.OnlyNotApprovedToolStripMenuItem.Checked = True
                    Me.OnlyApprovedToolStripMenuItem.Checked = False
                    Me.LoadData(btnItem, False)
                Else
                    Me.GridEX1.DataSource = Nothing
                    Me.FilterEditor1.Visible = False
                    DS.Clear() : DS.Dispose() : DS = Nothing
                End If
                If btnDPD.Checked = False Then
                    Me.cmbFlag.SelectedItem = Nothing
                End If
            Else
                Me.GridEX1.DataSource = Nothing
                Me.FilterEditor1.Visible = False
                DS.Clear() : DS.Dispose()
                Me.cmbFlag.SelectedItem = Nothing
            End If
            Me.isChecking = True : Me.chkCheckAll.Checked = False : Me.isChecking = False
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsLoadingRow = False
            Me.StatProg = StatusProgress.None
            Me.LogMyEvent(ex.Message, Me.Name + "_ItemPanel1_ItemClick")
            Me.ShowMessageInfo(ex.Message)
            If Not IsNothing(btnItem) Then
                btnItem.Checked = (Not btnItem.Checked)
            End If
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub HandleClickBoundDaryPeriode(ByVal sender As Object, ByVal e As EventArgs) Handles DP.ApplyButtonClick
        Try
            Me.BoundariStartPKD = Convert.ToDateTime(Me.DP.dtPicFilterStart.Value.ToShortDateString())
            Me.BoundaryEndPKD = Convert.ToDateTime(Me.DP.dtPicFilterEnd.Value.ToShortDateString())
            Me.DP.Close()
            Me.DP = Nothing
            If btnSetPeriodePKD.Checked Then
                Me.StatProg = StatusProgress.Processing
                Me.ThreadProgress = New Thread(AddressOf ShowProceed)
                Me.ThreadProgress.Start()
                If Me.btnAdjusmentPKD.Checked Then
                    Me.LoadData(btnAdjusmentPKD, False)
                ElseIf Me.btnAveragePrice.Checked Then
                    Me.LoadData(Me.btnAveragePrice, False)
                ElseIf Me.btnDPD.Checked Then
                    Me.LoadData(btnDPD, False)
                ElseIf Me.btnPriceFM.Checked Then
                    Me.btnPriceFM.Checked = False
                    If Not IsNothing(DS) Then
                        DS.RejectChanges()
                    End If
                    Me.GridEX1.SetDataBinding(Nothing, "")
                ElseIf Me.btnPricePL.Checked Then
                    btnPricePL.Checked = False
                    If Not IsNothing(DS) Then
                        DS.RejectChanges()
                    End If
                    Me.GridEX1.SetDataBinding(Nothing, "")
                ElseIf Me.btnSalesProgram.Checked Then
                    Me.LoadData(btnSalesProgram, False)
                ElseIf Me.btnTargetPKD.Checked Then
                    Me.LoadData(btnTargetPKD, False)
                ElseIf Me.btnCPDAuto.Checked Then
                    Me.LoadData(Me.btnCPDAuto, False)
                End If
            End If
        Catch ex As Exception
            Me.IsLoadingRow = False
            Me.StatProg = StatusProgress.None
            Me.LogMyEvent(ex.Message, Me.Name + "_HandleClickBoundDaryPeriode")
            Me.ShowMessageInfo(ex.Message)
        End Try
        Me.Cursor = Cursors.Default
      
    End Sub

    Private Sub AuditApprovalData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Dim DefStartPKD As Object = Nothing, DefEndPKD As Object = Nothing
        Dim StartDate As New DateTime(DateTime.Now.Year - 1, 8, 1)
        Dim EndDate As New DateTime(DateTime.Now.Year, 7, 31)
        Dim curMonth As Integer = NufarmBussinesRules.SharedClass.ServerDate.Month
        If curMonth >= 8 And curMonth <= 12 Then
            DefStartPKD = New DateTime(NufarmBussinesRules.SharedClass.ServerDate.Year, 8, 1)
            DefEndPKD = New DateTime((NufarmBussinesRules.SharedClass.ServerDate.Year + 1), 7, 31)
        Else
            DefStartPKD = New DateTime(NufarmBussinesRules.SharedClass.ServerDate.Year - 1, 8, 1)
            DefEndPKD = New DateTime((NufarmBussinesRules.SharedClass.ServerDate.Year), 7, 31)
        End If
        'Me.clsAudit.getCurrentPKD(StartDate, EndDate, True)
        Me.BoundariStartPKD = DefStartPKD
        Me.BoundaryEndPKD = DefEndPKD
        Dim AllowUpdateApproval As Boolean = (NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AuditApprovalData Or Main.IsSystemAdministrator)
        Me.btnApprove.Enabled = AllowUpdateApproval
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub chkCheckAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckAll.CheckedChanged
        If isChecking Then : Return : End If
        If Not IsNothing(Me.DS) Then
            If Me.DS.Tables.Count <= 0 Then
                isChecking = True : Me.chkCheckAll.Checked = Not Me.chkCheckAll.Checked : isChecking = False : Return
            ElseIf DS.Tables(0).Rows.Count <= 0 Then
                isChecking = True : Me.chkCheckAll.Checked = Not Me.chkCheckAll.Checked : isChecking = False : Return
            End If
        Else
            isChecking = True : Me.chkCheckAll.Checked = Not Me.chkCheckAll.Checked : isChecking = False : Return
        End If
        Try

            If GridEX1.RecordCount <= 0 Then : Return : End If
            isChecking = True
            Dim tbl As DataTable = Me.DS.Tables(0)

            Dim Recount As Integer = Me.GridEX1.RecordCount
            If Recount >= 100 Then
                Me.StatProg = StatusProgress.Processing
                Me.ThreadProgress = New Thread(AddressOf ShowProceed)
                Me.ThreadProgress.Start()
            End If

            Dim PKColumn As Janus.Windows.GridEX.GridEXColumn = Me.GridEX1.RootTable.Columns(0)
            Dim Row As DataRow = Nothing
            Dim rows() As DataRow = Nothing
            For i As Integer = 0 To Recount - 1
                GridEX1.MoveTo(i)
                'gridEx1.SetValue("IsApproved") = 
                Dim valPKCol As Object = GridEX1.GetValue(PKColumn)
                Dim ApprovedDesc As Object = Me.GridEX1.GetValue("ApprovedDesc")
                Select Case PKColumn.DataMember
                    Case "IDApp"
                        rows = tbl.Select("IDApp = " + valPKCol.ToString())
                    Case "ACHIEVEMENT_BRANDPACK_ID"
                        rows = tbl.Select("ACHIEVEMENT_BRANDPACK_ID = '" + valPKCol.ToString() + "'")
                    Case "PROG_BRANDPACK_DIST_ID"
                        rows = tbl.Select("PROG_BRANDPACK_DIST_ID = '" + valPKCol.ToString() + "'")
                    Case "AGREE_BRAND_ID"
                        rows = tbl.Select("AGREE_BRAND_ID = '" + valPKCol.ToString() + "'")
                End Select
                If rows.Length > 0 Then
                    Row = rows(0)
                    Row.BeginEdit()
                    Row("IsApproved") = Me.chkCheckAll.Checked
                    If Me.chkCheckAll.Checked Then
                        Row("ApprovedBy") = NufarmBussinesRules.User.UserLogin.UserName & "(" & Environment.MachineName & ")"
                        Row("ApprovedDate") = NufarmBussinesRules.SharedClass.ServerDate
                        Row("ApprovedDesc") = ApprovedDesc
                    Else
                        Row("ApprovedBy") = DBNull.Value
                        Row("ApprovedDate") = DBNull.Value
                        Row("ApprovedDesc") = DBNull.Value
                    End If
                    Row.EndEdit()
                End If
            Next
            'For i As Integer = 0 To tbl.Rows.Count - 1
            '    tbl.Rows(i).BeginEdit()
            '    tbl.Rows(i)("IsApproved") = Me.chkCheckAll.Checked
            '    If Me.chkCheckAll.Checked Then
            '        tbl.Rows(i)("ApprovedBy") = NufarmBussinesRules.User.UserLogin.UserName & "(" & Environment.MachineName & ")"
            '        tbl.Rows(i)("ApprovedDate") = NufarmBussinesRules.SharedClass.ServerDate
            '    Else
            '        tbl.Rows(i)("ApprovedBy") = DBNull.Value
            '        tbl.Rows(i)("ApprovedDate") = DBNull.Value
            '    End If
            '    tbl.Rows(i).EndEdit()
            'Next
            isChecking = False
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            isChecking = False
            Me.StatProg = StatusProgress.None
            Me.LogMyEvent(ex.Message, Me.Name + "_chkCheckAll_CheckedChanged")
            Me.ShowMessageInfo(ex.Message)
        End Try
    End Sub

    Private Sub ReloadF5ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReloadF5ToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim btnItem As DevComponents.DotNetBar.ButtonItem = Nothing
            If Me.btnAdjusmentPKD.Checked Then
                btnItem = Me.btnAdjusmentPKD
            ElseIf Me.btnAveragePrice.Checked Then
                btnItem = Me.btnAveragePrice
            ElseIf Me.btnDPD.Checked Then
                btnItem = Me.btnDPD
            ElseIf Me.btnPriceFM.Checked Then
                btnItem = Me.btnPriceFM
            ElseIf Me.btnPricePL.Checked Then
                btnItem = Me.btnPricePL
            ElseIf Me.btnSalesProgram.Checked Then
                btnItem = Me.btnSalesProgram
            ElseIf Me.btnTargetPKD.Checked Then
                btnItem = Me.btnTargetPKD
            ElseIf Me.btnCPDAuto.Checked Then
                btnItem = Me.btnCPDAuto
            End If
            If IsNothing(btnItem) Then : Me.Cursor = Cursors.Default : Return : End If
            If Me.OnlyApprovedToolStripMenuItem.Checked Then
                Me.LoadData(btnItem, True)
            ElseIf Me.OnlyNotApprovedToolStripMenuItem.Checked Then
                Me.LoadData(btnItem, False)
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsLoadingRow = False
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
            Me.LogMyEvent(ex.Message, Me.Name + "_ReloadF5ToolStripMenuItem_Click")
            Me.ShowMessageInfo(ex.Message)
        End Try
    End Sub

    Private Sub ExportF7ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportF7ToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim SFD As New SaveFileDialog()
            With SFD
                .Title = "Define the location file"
                .OverwritePrompt = True
                .DefaultExt = ".xls"
                .Filter = "excel file|*.xls"
                .InitialDirectory = "C:\"
                If SFD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Using FS As New System.IO.FileStream(SFD.FileName, IO.FileMode.Create)
                        Using Exporter As New Janus.Windows.GridEX.Export.GridEXExporter()
                            Exporter.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                            Exporter.IncludeHeaders = True
                            Exporter.IncludeExcelProcessingInstruction = True
                            Exporter.IncludeChildTables = True
                            Exporter.IncludeFormatStyle = True
                            Exporter.GridEX = Me.GridEX1
                            Exporter.Export(FS) : FS.Flush()
                            Me.ShowMessageInfo("Data exported to " & SFD.FileName)
                        End Using
                    End Using
                End If
            End With
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
            Me.LogMyEvent(ex.Message, Me.Name + "_ExportF7ToolStripMenuItem_Click")
            Me.ShowMessageInfo(ex.Message)
        End Try
    End Sub

    Private Sub OnlyApprovedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlyApprovedToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim btnItem As DevComponents.DotNetBar.ButtonItem = Nothing

            If Me.btnAdjusmentPKD.Checked Then
                btnItem = Me.btnAdjusmentPKD
            ElseIf Me.btnAveragePrice.Checked Then
                btnItem = Me.btnAveragePrice
            ElseIf Me.btnDPD.Checked Then
                btnItem = Me.btnDPD
            ElseIf Me.btnPriceFM.Checked Then
                btnItem = Me.btnPriceFM
            ElseIf Me.btnPricePL.Checked Then
                btnItem = Me.btnPricePL
            ElseIf Me.btnSalesProgram.Checked Then
                btnItem = Me.btnSalesProgram
            ElseIf Me.btnTargetPKD.Checked Then
                btnItem = Me.btnTargetPKD
            ElseIf Me.btnCPDAuto.Checked Then
                btnItem = Me.btnCPDAuto
            End If
            Me.OnlyApprovedToolStripMenuItem.Checked = True
            Me.OnlyNotApprovedToolStripMenuItem.Checked = False
            If Not IsNothing(btnItem) Then
                Me.LoadData(btnItem, True)
            End If

            Me.chkCheckAll.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            Me.LogMyEvent(ex.Message, Me.Name + "_OnlyApprovedToolStripMenuItem_Click")
            Me.ShowMessageInfo(ex.Message)
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OnlyNotApprovedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlyNotApprovedToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim btnItem As DevComponents.DotNetBar.ButtonItem = Nothing

            If Me.btnAdjusmentPKD.Checked Then
                btnItem = Me.btnAdjusmentPKD
            ElseIf Me.btnAveragePrice.Checked Then
                btnItem = Me.btnAveragePrice
            ElseIf Me.btnDPD.Checked Then
                btnItem = Me.btnDPD
            ElseIf Me.btnPriceFM.Checked Then
                btnItem = Me.btnPriceFM
            ElseIf Me.btnPricePL.Checked Then
                btnItem = Me.btnPricePL
            ElseIf Me.btnSalesProgram.Checked Then
                btnItem = Me.btnSalesProgram
            ElseIf Me.btnTargetPKD.Checked Then
                btnItem = Me.btnTargetPKD
            ElseIf Me.btnCPDAuto.Checked Then
                btnItem = Me.btnCPDAuto
            End If
            Me.OnlyNotApprovedToolStripMenuItem.Checked = True
            Me.OnlyApprovedToolStripMenuItem.Checked = False
            If Not IsNothing(btnItem) Then
                Me.LoadData(btnItem, False)
            End If
            Me.chkCheckAll.Enabled = True
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            Me.LogMyEvent(ex.Message, Me.Name + "_OnlyNotApprovedToolStripMenuItem_Click")
            Me.ShowMessageInfo(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        If Me.isChecking Then : Return : End If
        If Me.IsLoadingRow Then : Return : End If
        If Me.GridEX1.DataSource Is Nothing Then : Return : End If
        If Me.GridEX1.RecordCount <= 0 Then : Return : End If
        If e.Column.Key = "ApprovedDesc" Then
            If Me.btnApprove.Enabled = True Then : Me.GridEX1.UpdateData() : Return : End If
            Try
                Cursor = Cursors.WaitCursor
                Dim tblToUpdate As DataTable = CType(Me.GridEX1.DataSource, DataView).Table
                Me.GridEX1.UpdateData()
                tblToUpdate.AcceptChanges()
                Dim PrimaryKey As Object = Nothing
                If IsNumeric(Me.GridEX1.GetValue(0)) Then
                    PrimaryKey = CInt(Me.GridEX1.GetValue(0))
                Else
                    PrimaryKey = CStr(Me.GridEX1.GetValue(0))
                End If
                Me.clsAudit.UpdateApproval(tblToUpdate, PrimaryKey, False) ''set false karena munkin akan mengupdate yang lainnya
                Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_CellUpdated")
                Me.ShowMessageInfo(ex.Message)
            End Try
        Else
            Me.GridEX1.UpdateData()
        End If
    End Sub

    Private Sub GridEX1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        Me.Cursor = Cursors.WaitCursor
        Try
            If e.KeyCode = Keys.F7 Then
                Dim SFD As New SaveFileDialog()
                With SFD
                    .Title = "Define the location file"
                    .OverwritePrompt = True
                    .DefaultExt = ".xls"
                    .Filter = "excel file|*.xls"
                    .InitialDirectory = "C:\"
                    If SFD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Using FS As New System.IO.FileStream(SFD.FileName, IO.FileMode.Create)
                            Using Exporter As New Janus.Windows.GridEX.Export.GridEXExporter()
                                Exporter.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                                Exporter.IncludeHeaders = True
                                Exporter.IncludeExcelProcessingInstruction = True
                                Exporter.IncludeChildTables = True
                                Exporter.IncludeFormatStyle = True
                                Exporter.GridEX = Me.GridEX1
                                Exporter.Export(FS) : FS.Flush()
                                Me.ShowMessageInfo("Data exported to " & SFD.FileName)
                            End Using
                        End Using
                    End If
                End With
            ElseIf e.KeyCode = Keys.F5 Then
                Dim btnItem As DevComponents.DotNetBar.ButtonItem = Nothing
                If Me.btnAdjusmentPKD.Checked Then
                    btnItem = Me.btnAdjusmentPKD
                ElseIf Me.btnAveragePrice.Checked Then
                    btnItem = Me.btnAveragePrice
                ElseIf Me.btnDPD.Checked Then
                    btnItem = Me.btnDPD
                ElseIf Me.btnPriceFM.Checked Then
                    btnItem = Me.btnPriceFM
                ElseIf Me.btnPricePL.Checked Then
                    btnItem = Me.btnPricePL
                ElseIf Me.btnSalesProgram.Checked Then
                    btnItem = Me.btnSalesProgram
                ElseIf Me.btnTargetPKD.Checked Then
                    btnItem = Me.btnTargetPKD
                ElseIf Me.btnCPDAuto.Checked Then
                    btnItem = Me.btnCPDAuto
                End If
                If Not IsNothing(btnItem) Then
                    If Me.OnlyApprovedToolStripMenuItem.Checked Then
                        Me.LoadData(btnItem, True)
                    ElseIf Me.OnlyNotApprovedToolStripMenuItem.Checked Then
                        Me.LoadData(btnItem, False)
                    End If
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_KeyDown")
            Me.ShowMessageInfo(ex.Message)
        End Try
    End Sub

    Private Sub cmbFlag_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFlag.TextChanged
        If IsLoadingRow Then : Return : End If

        Try
            Dim btnItem As DevComponents.DotNetBar.ButtonItem = Nothing
            If Me.btnAdjusmentPKD.Checked Then
                btnItem = Me.btnAdjusmentPKD
            ElseIf Me.btnAveragePrice.Checked Then
                btnItem = Me.btnAveragePrice
            ElseIf Me.btnDPD.Checked Then
                btnItem = Me.btnDPD
            ElseIf Me.btnPriceFM.Checked Then
                btnItem = Me.btnPriceFM
            ElseIf Me.btnPricePL.Checked Then
                btnItem = Me.btnPricePL
            ElseIf Me.btnSalesProgram.Checked Then
                btnItem = Me.btnSalesProgram
            ElseIf Me.btnTargetPKD.Checked Then
                btnItem = Me.btnTargetPKD
            ElseIf Me.btnCPDAuto.Checked Then
                btnItem = Me.btnCPDAuto
            End If
            If Not IsNothing(btnItem) Then
                If Me.OnlyApprovedToolStripMenuItem.Checked Then
                    Me.LoadData(btnItem, True)
                ElseIf Me.OnlyNotApprovedToolStripMenuItem.Checked Then
                    Me.LoadData(btnItem, False)
                End If
            End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
            Me.LogMyEvent(ex.Message, Me.Name + "_cmbFlag_TextChanged")
            Me.ShowMessageInfo(ex.Message)
        End Try
    End Sub
End Class
