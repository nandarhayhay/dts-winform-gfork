Imports System.Threading
Public Class AuditMasterData
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private LD As Loading

    Private Enum StatusProgress
        None
        Processing
    End Enum
    Friend CMain As Main
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
    Private WithEvents DP As DefinePeriode
    Private ReadOnly Property clsAudit() As NufarmBussinesRules.Audit.MasterData
        Get
            If IsNothing(m_clsAudit) Then
                m_clsAudit = New NufarmBussinesRules.Audit.MasterData()
            End If
            Return m_clsAudit
        End Get
    End Property
    Private BoundariStartPKD As Object = Nothing, BoundaryEndPKD As Object = Nothing

    Private Sub BindGrid(ByVal ds As Object, ByVal dataMember As String)
        Me.GridEX1.DataSource = ds
        Me.GridEX1.DataMember = dataMember
        Me.GridEX1.RetrieveStructure(True)

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
            ElseIf Item.DataMember.Contains("IDApp") Then
                Item.Visible = False
                'Me.GridEX1.RootTable.ChildTables(0).Columns("IDApp").Visible = False
            ElseIf Item.DataMember.Contains("IDApp1") Then
                Item.Visible = False
            End If
        Next
        For Each item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.ChildTables(0).Columns
            If item.Type Is Type.GetType("System.DateTime") Then
                item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                item.FormatString = "dd MMMM yyyy"
            ElseIf item.Type Is Type.GetType("System.Boolean") Then
                item.ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox
                item.FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
            ElseIf (item.Type Is Type.GetType("System.Decimal")) Or (item.Type Is Type.GetType("System.Double")) _
                Or item.Type Is Type.GetType("System.Single") Then
                If (item.DataMember.Contains("VALUE")) Then
                    item.FormatString = "#,##0.00"
                    item.TotalFormatString = "#,##0.00"
                    item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    item.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Else
                    item.FormatString = "#,##0.000"
                    item.TotalFormatString = "#,##0.000"
                    item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    item.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                End If
            ElseIf (item.DataMember.Contains("VALUE")) Then
                item.FormatString = "#,##0.00"
                item.TotalFormatString = "#,##0.00"
                item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                item.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ElseIf item.DataMember.Contains("_ID") Then
                item.Visible = False
            ElseIf item.DataMember.Contains("IDApp") Then
                item.Visible = False
                'Me.GridEX1.RootTable.ChildTables(0).Columns("IDApp").Visible = False
            ElseIf item.DataMember.Contains("IDApp1") Then
                item.Visible = False
            End If
        Next
        GridEX1.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(GridEX1.RootTable.Columns("ACTION_NAME")))
        'If Me.GridEX1.RootTable.Columns.Contains("IDApp") Then
        '    Me.GridEX1.RootTable.Columns("IDApp").Visible = False
        '    Me.GridEX1.RootTable.ChildTables(0).Columns("IDApp").Visible = False
        'End If
        GridEX1.RepeatHeaders = Janus.Windows.GridEX.InheritableBoolean.False
        Me.GridEX1.RootTable.TableHeader = Janus.Windows.GridEX.InheritableBoolean.True
        Me.GridEX1.RootTable.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
        Me.GridEX1.RootTable.TableHeaderFormatStyle.ForeColor = Color.Maroon
        Me.GridEX1.RootTable.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX1.AutoSizeColumns()
        me.GridEX1.AutoSizeColumns(me.GridEX1.RootTable.ChildTables(0))
        If Me.GridEX1.RootTable.ChildTables.Count > 0 Then
            Me.GridEX1.RootTable.ChildTables(0).TableHeader = Janus.Windows.GridEX.InheritableBoolean.False
            Me.GridEX1.ExpandGroups()
            Me.GridEX1.ExpandRecords()
            Me.GridEX1.AutoSizeColumns(Me.GridEX1.RootTable.ChildTables(0))
            Me.Timer1.Enabled = True : Me.Timer1.Start()
        End If
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
            Dim DS As DataSet = Nothing
            If btnItem.Checked Then

                Me.StatProg = StatusProgress.Processing
                Me.ThreadProgress = New Thread(AddressOf ShowProceed)
                Me.ThreadProgress.Start()
            End If

            If Not IsNothing(btnItem) Then
                If btnItem.Checked Then
                    Select Case btnItem.Name
                        Case "btnAdjusmentPKD"
                            DS = Me.clsAudit.getAdjumentPKD(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), Me.BoundariStartPKD, Me.BoundaryEndPKD, False)
                            Me.BindGrid(DS, "HEADER_DATA")
                            Me.GridEX1.RootTable.Caption = "ADJUSTMENT DPD LOG"
                            If Me.btnSetPeriodePKD.Enabled = False Then
                                Me.btnSetPeriodePKD.Enabled = True
                            End If
                        Case "btnAveragePrice"
                            DS = Me.clsAudit.getDataAvPrice(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), False)
                            Me.BindGrid(DS, "HEADER_DATA")
                            Me.GridEX1.RootTable.Caption = "AVG PRICE LOG"
                            Me.btnSetPeriodePKD.Checked = False
                            Me.btnSetPeriodePKD.Enabled = False
                            Me.BoundariStartPKD = Nothing
                            Me.BoundaryEndPKD = Nothing
                        Case "btnPriceFM"
                            DS = Me.clsAudit.getDataPriceFM(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), False)
                            Me.BindGrid(DS, "HEADER_DATA")
                            Me.GridEX1.RootTable.Caption = "PRICE FM LOG"
                            Me.btnSetPeriodePKD.Checked = False
                            Me.btnSetPeriodePKD.Enabled = False
                            Me.BoundariStartPKD = Nothing
                            Me.BoundaryEndPKD = Nothing
                        Case "btnSalesProgram"
                            DS = clsAudit.getDataSalesProgram(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), False)
                            Me.BindGrid(DS, "HEADER_DATA")
                            Me.GridEX1.RootTable.Caption = "SALES PROGRAM LOG"
                            Me.btnSetPeriodePKD.Checked = False
                            Me.btnSetPeriodePKD.Enabled = False
                            Me.BoundariStartPKD = Nothing
                            Me.BoundaryEndPKD = Nothing
                        Case "btnTargetPKD"
                            DS = Me.clsAudit.getDataTargetPKD(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), Me.BoundariStartPKD, Me.BoundaryEndPKD, False)
                            Me.BindGrid(DS, "HEADER_DATA")
                            Me.GridEX1.RootTable.Caption = "TARGET PKD LOG"
                            If Me.btnSetPeriodePKD.Enabled = False Then
                                Me.btnSetPeriodePKD.Enabled = True
                            End If
                    End Select
                Else
                    Me.GridEX1.DataSource = Nothing
                    Me.FilterEditor1.Visible = False
                End If
            Else
                Me.GridEX1.DataSource = Nothing
                Me.FilterEditor1.Visible = False
            End If
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            Me.LogMyEvent(ex.Message, Me.Name + "_ItemPanel1_ItemClick")
            Me.ShowMessageInfo(ex.Message)
            If Not IsNothing(btnItem) Then
                btnItem.Checked = (Not btnItem.Checked)
            End If
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            Dim DS As DataSet = Nothing
            Dim TrueToLoad As Boolean
            If Me.btnSalesProgram.Checked = True Or Me.btnPriceFM.Checked = True Or Me.btnAveragePrice.Checked = True _
            Or Me.btnAdjusmentPKD.Checked = True Or Me.btnTargetPKD.Checked = True Then
                TrueToLoad = True
            End If
            If TrueToLoad Then
                Me.Cursor = Cursors.WaitCursor
                Me.StatProg = StatusProgress.Processing
                Me.ThreadProgress = New Thread(AddressOf ShowProceed)
                Me.ThreadProgress.Start()
            End If
            If Me.btnAdjusmentPKD.Checked Then
                DS = Me.clsAudit.getAdjumentPKD(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), Me.BoundariStartPKD, Me.BoundaryEndPKD, False)
                If DS.Tables(0).Columns.Contains("IDApp 1") Then
                    DS.Tables(0).Columns.Remove("IDApp 1")
                End If
                DS.AcceptChanges()
                Me.BindGrid(DS, "HEADER_DATA")
                Me.GridEX1.RootTable.Caption = "ADJUSTMENT DPD LOG"
                If Me.btnSetPeriodePKD.Enabled = False Then
                    Me.btnSetPeriodePKD.Enabled = True
                End If
            ElseIf Me.btnAveragePrice.Checked Then
                DS = Me.clsAudit.getDataAvPrice(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), False)
                If DS.Tables(0).Columns.Contains("IDApp 1") Then
                    DS.Tables(0).Columns.Remove("IDApp 1")
                End If
                DS.AcceptChanges()
                Me.BindGrid(DS, "HEADER_DATA")
                Me.GridEX1.RootTable.Caption = "AVG PRICE LOG"
                Me.btnSetPeriodePKD.Checked = False
                Me.btnSetPeriodePKD.Enabled = False
                Me.BoundariStartPKD = Nothing
                Me.BoundaryEndPKD = Nothing
            ElseIf Me.btnPriceFM.Checked Then
                DS = Me.clsAudit.getDataPriceFM(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), False)
                If DS.Tables(0).Columns.Contains("IDApp 1") Then
                    DS.Tables(0).Columns.Remove("IDApp 1")
                End If
                DS.AcceptChanges()
                Me.BindGrid(DS, "HEADER_DATA")
                Me.GridEX1.RootTable.Caption = "PRICE FM LOG"
                Me.btnSetPeriodePKD.Checked = False
                Me.btnSetPeriodePKD.Enabled = False
                Me.BoundariStartPKD = Nothing
                Me.BoundaryEndPKD = Nothing
            ElseIf Me.btnSalesProgram.Checked Then
                DS = clsAudit.getDataSalesProgram(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), False)
                If DS.Tables(0).Columns.Contains("IDApp 1") Then
                    DS.Tables(0).Columns.Remove("IDApp 1")
                End If
                DS.AcceptChanges()
                Me.BindGrid(DS, "HEADER_DATA")
                Me.GridEX1.RootTable.Caption = "SALES PROGRAM LOG"
                Me.btnSetPeriodePKD.Checked = False
                Me.btnSetPeriodePKD.Enabled = False
                Me.BoundariStartPKD = Nothing
                Me.BoundaryEndPKD = Nothing
            ElseIf Me.btnTargetPKD.Checked Then
                DS = Me.clsAudit.getDataTargetPKD(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), Me.BoundariStartPKD, Me.BoundaryEndPKD, False)
                If DS.Tables(0).Columns.Contains("IDApp 1") Then
                    DS.Tables(0).Columns.Remove("IDApp 1")
                End If
                DS.AcceptChanges()
                Me.BindGrid(DS, "HEADER_DATA")
                Me.GridEX1.RootTable.Caption = "TARGET PKD LOG"
                If Me.btnSetPeriodePKD.Enabled = False Then
                    Me.btnSetPeriodePKD.Enabled = True
                End If
            End If
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            Me.LogMyEvent(ex.Message, Me.Name + "_btnApplyRange_Click")
            Me.ShowMessageInfo(ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub AuditMasterData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FilterEditor1.Visible = False
        Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
        Me.dtPicfrom.Value = Me.dtPicUntil.Value.AddMonths(-6)

    End Sub

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
                        .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                        If SFD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                            Using FS As New System.IO.FileStream(SFD.FileName, IO.FileMode.Create)
                                Using Exporter As New Janus.Windows.GridEX.Export.GridEXExporter()
                                    Exporter.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                                    Exporter.IncludeHeaders = True
                                    Exporter.IncludeExcelProcessingInstruction = True
                                    Exporter.IncludeFormatStyle = True
                                    Exporter.IncludeChildTables = True
                                    Exporter.GridEX = Me.GridEX1
                                    Exporter.Export(FS) : FS.Flush()
                                    Me.ShowMessageInfo("Data exported to " & SFD.FileName)
                                End Using
                            End Using
                        End If
                    End With
                Case "btnSetPeriodePKD"
                    If btnSetPeriodePKD.Checked Then
                        Me.BoundaryEndPKD = Nothing
                        Me.BoundariStartPKD = Nothing
                    Else
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
                    Me.btnSetPeriodePKD.Checked = (Not Me.btnSetPeriodePKD.Checked)
            End Select

        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
            Me.ShowMessageInfo(ex.Message)
            If item.Name = "btnSetPeriodePKD" Then
                btnSetPeriodePKD.Checked = (Not Me.btnSetPeriodePKD.Checked)
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub HandleClickBoundDaryPeriode(ByVal sender As Object, ByVal e As EventArgs) Handles DP.ApplyButtonClick
        Me.BoundariStartPKD = Convert.ToDateTime(Me.DP.dtPicFilterStart.Value.ToShortDateString())
        Me.BoundaryEndPKD = Convert.ToDateTime(Me.DP.dtPicFilterEnd.Value.ToShortDateString())
        If NufarmBussinesRules.SharedClass.ServerDate >= Me.BoundariStartPKD And NufarmBussinesRules.SharedClass.ServerDate <= Me.BoundaryEndPKD Then
            If DateDiff(DateInterval.Month, NufarmBussinesRules.SharedClass.ServerDate, Me.BoundaryEndPKD, FirstDayOfWeek.System, FirstWeekOfYear.System) >= 6 Then
                Me.dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate
                Me.dtPicUntil.Value = Me.dtPicfrom.Value.AddMonths(6)
            Else
                Me.dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate
                Me.dtPicUntil.Value = Me.BoundaryEndPKD
            End If
        Else
            Me.dtPicfrom.Value = Me.BoundariStartPKD
            Me.dtPicUntil.Value = Me.BoundaryEndPKD
        End If
     
        Me.DP.Close()
        Me.DP = Nothing
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.TickCount += 1
        If Me.TickCount >= 2 Then
            Me.GridEX1.CollapseGroups()
            Me.Timer1.Stop() : Me.Timer1.Enabled = False
        End If
    End Sub
End Class
