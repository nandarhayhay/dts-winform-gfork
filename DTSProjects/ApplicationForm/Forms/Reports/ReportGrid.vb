Public Class ReportGrid
    Friend m_Grid As Janus.Windows.GridEX.GridEX
    Private ActiveDoc As UserControl
    Private FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Friend CMain As Main = Nothing
    Friend MustReloadData As Boolean = False
    Public Event ShowSPPBData()
    Private Sub GetStateChecked(ByRef item As DevComponents.DotNetBar.ButtonItem)
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
    Private Sub ShowFieldChooser()
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "DPRD" Then
                Me.m_Grid = DirectCast(Me.ActiveDoc, DPRD).Grid
            End If
        End If
        If Not Me.m_Grid Is Nothing Then
            Me.m_Grid.ShowFieldChooser(Me)
        End If
    End Sub
    Private Sub ShowSettingGrid()
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "DPRD" Then
                Me.m_Grid = DirectCast(Me.ActiveDoc, DPRD).Grid
            End If
        End If
        If Not (Me.m_Grid Is Nothing) Then
            Dim SetGrid As New SettingGrid()
            SetGrid.Grid = Me.m_Grid
            SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
            SetGrid.ShowDialog(Me)
        End If
    End Sub
    Private Sub ShowPrinting()
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "DPRD" Then
                Me.m_Grid = DirectCast(Me.ActiveDoc, DPRD).Grid
            End If
        End If
        If Not IsNothing(Me.m_Grid) Then
            Me.GridEXPrintDocument1.GridEX = Me.m_Grid
            Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
            If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
            End If
            'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
            If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Me.PrintPreviewDialog1.Document.Print()
            End If
        End If
    End Sub
    Private Sub ShowCustomFilter()
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "DPRD" Then
                Me.m_Grid = DirectCast(Me.ActiveDoc, DPRD).Grid
            End If
            If Not IsNothing(Me.m_Grid) Then
                Me.FilterEditor1.Visible = True
                Me.FilterEditor1.SortFieldList = False
                Me.FilterEditor1.SourceControl = Me.m_Grid
                Me.m_Grid.FilterMode = Janus.Windows.GridEX.FilterMode.None
                Me.m_Grid.RemoveFilters()
                Select Case Me.ActiveDoc.Name
                    Case "OA_Report"
                        DirectCast(Me.ActiveDoc, OA_Report).dtPicFrom.Text = ""
                        DirectCast(Me.ActiveDoc, OA_Report).dtPicUntil.Text = ""
                        DirectCast(Me.ActiveDoc, OA_Report).grpRangedate.Enabled = False
                    Case "Distributor_Report"
                        DirectCast(Me.ActiveDoc, Distributor_Report).dtPicfrom.Text = ""
                        DirectCast(Me.ActiveDoc, Distributor_Report).dtPicUntil.Text = ""
                        DirectCast(Me.ActiveDoc, Distributor_Report).grpRangedate.Enabled = False
                        'Case "ThirdParty_Report"
                        '    DirectCast(Me.ActiveDoc, ThirdParty_Report).dtPicfrom.Text = ""
                        '    DirectCast(Me.ActiveDoc, ThirdParty_Report).dtPicUntil.Text = ""
                        '    DirectCast(Me.ActiveDoc, ThirdParty_Report).grpRangeDate.Enabled = False
                    Case "SPPB_Report"
                        DirectCast(Me.ActiveDoc, SPPB_Report).dtPicfrom.Text = ""
                        DirectCast(Me.ActiveDoc, SPPB_Report).dtPicUntil.Text = ""
                        DirectCast(Me.ActiveDoc, SPPB_Report).grpRangeDate.Enabled = False
                    Case "Distributor_PO_Dispro"
                        DirectCast(Me.ActiveDoc, Distributor_PO_Dispro).dtPicfrom.Text = ""
                        DirectCast(Me.ActiveDoc, Distributor_PO_Dispro).dtPicUntil.Text = ""
                        DirectCast(Me.ActiveDoc, Distributor_PO_Dispro).grpRangedate.Enabled = False
                    Case "DisproByBrand"
                        DirectCast(Me.ActiveDoc, DisproByBrand).dtPicfrom.Text = ""
                        DirectCast(Me.ActiveDoc, DisproByBrand).dtPicUntil.Text = ""
                        DirectCast(Me.ActiveDoc, DisproByBrand).grpRangedate.Enabled = False
                    Case "Agreement_Target"
                    Case "DPRD"
                    Case "Accrue"
                        DirectCast(Me.ActiveDoc, Accrue).dtPicFrom.Text = ""
                        DirectCast(Me.ActiveDoc, Accrue).dtPicUntil.Text = ""
                    Case "RecapNasionalDPRD"
                        DirectCast(Me.ActiveDoc, RecapNasionalDPRD).dtPicfrom.Text = ""
                        DirectCast(Me.ActiveDoc, RecapNasionalDPRD).dtPicUntil.Text = ""
                    Case "SPPBManager"
                        DirectCast(Me.ActiveDoc, SPPBManager).dtpicFrom.Text = ""
                        DirectCast(Me.ActiveDoc, SPPBManager).dtPicUntil.Text = ""
                    Case "DDDRReport"
                        DirectCast(Me.ActiveDoc, DDDR).grpRangeDate.Enabled = False
                        DirectCast(Me.ActiveDoc, DDDR).dtPicfrom.Text = ""
                        DirectCast(Me.ActiveDoc, DDDR).dtPicUntil.Text = ""
                End Select
                If Me.FilterEditor1.Visible Then
                    Me.FilterEditor1.Dock = DockStyle.Top
                    Me.FilterEditor1.BringToFront()
                    Me.ActiveDoc.BringToFront()
                End If
            End If
        End If
    End Sub
    Private Sub ShowFilterEqual()
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "DPRD" Then
                Me.m_Grid = DirectCast(Me.ActiveDoc, DPRD).Grid
            End If
            If Not IsNothing(Me.m_Grid) Then
                Me.FilterEditor1.Visible = False
                Me.m_Grid.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Me.m_Grid.RemoveFilters()
                Select Case Me.ActiveDoc.Name
                    Case "OA_Report" : DirectCast(Me.ActiveDoc, OA_Report).grpRangedate.Enabled = True
                    Case "Distributor_Report" : DirectCast(Me.ActiveDoc, Distributor_Report).grpRangedate.Enabled = True
                        'Case "ThirdParty_Report" : DirectCast(Me.ActiveDoc, ThirdParty_Report).grpRangeDate.Enabled = True '.Enabled = True
                    Case "SPPB_Report" : DirectCast(Me.ActiveDoc, SPPB_Report).grpRangeDate.Enabled = True '.Enabled = True
                    Case "Distributor_PO_Dispro" : DirectCast(Me.ActiveDoc, Distributor_PO_Dispro).grpRangedate.Enabled = True
                    Case "DDDRReport" : DirectCast(Me.ActiveDoc, DDDR).grpRangeDate.Enabled = True
                    Case "Agreement_Target", "DPRD", "Accrue", "RecapNasionalDPRD", "btnSummaryPlantation"

                End Select
            End If
            'Select Case ActiveDoc.Name

            '    Case "DisproByBrand"
            '        'If Not IsNothing(Me.m_Grid) Then
            '        '    Me.FilterEditor1.Visible = False
            '        '    Me.m_Grid.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            '        '    Me.m_Grid.RemoveFilters()
            '        '    DirectCast(Me.ActiveDoc, DisproByBrand).grpRangedate.Enabled = True
            '        'End If
            '    Case "Agreement_Target", "DPRD", "Accrue", "RecapNasionalDPRD"
            '        If Not IsNothing(Me.m_Grid) Then
            '            Me.FilterEditor1.Visible = False
            '            Me.m_Grid.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            '            Me.m_Grid.RemoveFilters()
            '            'CType(Me.ActiveDoc, Agreement_Target).grpRangedate.Enabled = True
            '        End If
            'End Select
            If Me.FilterEditor1.Visible Then
                Me.FilterEditor1.Visible = False
            End If
            Me.FilterEditor1.SendToBack()
        End If
    End Sub
    Private Sub ShowExportReport()
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "DPRD" Then
                Me.m_Grid = DirectCast(Me.ActiveDoc, DPRD).Grid
            End If
        End If
        If IsNothing(Me.m_Grid) Then
            Return
        End If
        If IsNothing(Me.m_Grid.DataSource) Then
            Return
        End If
        If ActiveDoc.Name = "SalesProgramReport" Then
            If Me.m_Grid.Name = "GridEX1" Then
                If CType(ActiveDoc, SalesProgramReport).btnSalesbyPO.Checked = True Then
                    Me.SaveFileDialog1.Title = "Define the location File"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.m_Grid
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If Me.m_Grid.RecordCount > 0 Then
                        'Dim tempTable As New DataTable("T_temp_po")
                        Dim tblToExport As DataTable = DirectCast(DirectCast(Me.ActiveDoc, SalesProgramReport).GridEX1.DataSource, DataView).ToTable().Copy()
                        Dim colProgBrandPackDistID As New DataColumn("PROG_BRANDPACK_DIST_ID", Type.GetType("System.String"))
                        'Dim colTotalPO As New DataColumn("TOTAL_PO_ORIGINAL", Type.GetType("System.Decimal"))
                        'tempTable.Columns.Add(colProgBrandPackDistID)
                        'tempTable.Columns.Add(colTotalPO)

                        tblToExport.Columns.Add(New DataColumn("TOTAL_PO_ORIGINAL", Type.GetType("System.Decimal")))
                        tblToExport.Columns("TOTAL_PO_ORIGINAL").SetOrdinal(11)
                        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.m_Grid.RootTable.Columns
                            If Not col.Visible Then
                                If tblToExport.Columns.Contains(col.DataMember) Then
                                    If col.DataMember <> "PROG_BRANDPACK_DIST_ID" Then
                                        tblToExport.Columns.Remove(col.DataMember)
                                    End If
                                End If
                            End If
                        Next
                        Dim childTable As DataTable = DirectCast(Me.ActiveDoc, SalesProgramReport).DS.Tables(1).Copy()
                        Dim Rows() As DataRow = Nothing
                        Dim row As DataRow = Nothing
                        For Each Jrow As Janus.Windows.GridEX.GridEXRow In Me.m_Grid.GetRows()
                            Dim ProgBrandPackDistID As String = Jrow.Cells("ID").Value.ToString()
                            Dim TotalPO As Object = childTable.Compute("SUM(PO_ORIGINAL_QTY)", "PROG_BRANDPACK_DIST_ID = '" & ProgBrandPackDistID & "'")
                            If Not IsNothing(TotalPO) And Not IsDBNull(TotalPO) Then
                                Rows = tblToExport.Select("PROG_BRANDPACK_DIST_ID = '" & ProgBrandPackDistID & "'")
                                If Rows.Length > 0 Then
                                    Rows(0).BeginEdit()
                                    Rows(0)("TOTAL_PO_ORIGINAL") = TotalPO
                                    Rows(0).EndEdit()
                                End If
                            End If
                        Next
                        tblToExport.AcceptChanges()
                        Using JGrid As New Janus.Windows.GridEX.GridEX()
                            JGrid.DataSource = tblToExport
                            JGrid.RetrieveStructure()
                            For Each col As Janus.Windows.GridEX.GridEXColumn In JGrid.RootTable.Columns
                                If col.DataMember = "PROG_BRANDPACK_DIST_ID" Then
                                    col.Visible = False
                                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                                    col.ColumnType = Janus.Windows.GridEX.ColumnType.Text
                                    col.Key = "ID"
                                    col.Caption = "ID"
                                End If
                                If col.Type Is Type.GetType("System.Decimal") Or col.Type Is Type.GetType("System.Int32") Then
                                    If col.DataMember.ToString().Contains("GIVEN") Then
                                        col.FormatString = "p"
                                        col.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                                    Else
                                        col.FormatString = "#,##0.000"
                                    End If
                                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                                    col.ColumnType = Janus.Windows.GridEX.ColumnType.Text
                                ElseIf col.Type Is Type.GetType("System.DateTime") Then
                                    col.FormatString = "dd MMMM yyyy"
                                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
                                    col.ColumnType = Janus.Windows.GridEX.ColumnType.Text
                                ElseIf col.Type Is Type.GetType("System.Boolean") Then
                                    col.ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox
                                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
                                Else
                                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                                End If
                            Next
                            JGrid.AutoSizeColumns()
                            Using JExporter As New Janus.Windows.GridEX.Export.GridEXExporter()
                                JExporter.GridEX = JGrid
                                JExporter.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                                Dim SFD As New SaveFileDialog()
                                With SFD
                                    .OverwritePrompt = True
                                    .InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                    .Title = "Export data to Excel"
                                    .Filter = "Excel files|*.xls"
                                    If (.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
                                        Using FS As New System.IO.FileStream(.FileName, IO.FileMode.Create)
                                            JExporter.Export(FS) : FS.Flush()
                                            Me.ShowMessageInfo("Data Exported to " & .FileName)
                                        End Using
                                    End If
                                End With
                            End Using
                        End Using
                    End If
                End If
            Else
                Me.SaveFileDialog1.Title = "Define the location File"
                Me.SaveFileDialog1.OverwritePrompt = True
                Me.SaveFileDialog1.DefaultExt = ".xls"
                Me.SaveFileDialog1.Filter = "All Files|*.*"
                Me.SaveFileDialog1.InitialDirectory = "C:\"
                If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                    Me.GridEXExporter1.GridEX = Me.m_Grid
                    Me.GridEXExporter1.Export(FS)
                    FS.Close()
                    MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
            Return
        Else
            Me.SaveFileDialog1.Title = "Define the location File"
            Me.SaveFileDialog1.OverwritePrompt = True
            Me.SaveFileDialog1.DefaultExt = ".xls"
            Me.SaveFileDialog1.Filter = "All Files|*.*"
            Me.SaveFileDialog1.InitialDirectory = "C:\"
            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                Me.GridEXExporter1.GridEX = Me.m_Grid
                Me.GridEXExporter1.Export(FS)
                FS.Close()
                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub
    Private Sub RefreshData()
        If Not IsNothing(Me.ActiveDoc) Then
            Select Case ActiveDoc.Name
                Case "Distributor_Report"
                    DirectCast(Me.ActiveDoc, Distributor_Report).RefreshData()
                Case "OA_Report"
                    DirectCast(Me.ActiveDoc, OA_Report).RefreshData()
                Case "SPPB_Report"
                    DirectCast(Me.ActiveDoc, SPPB_Report).RefreshData()
                    'Case "ThirdParty_Report"
                    '    DirectCast(Me.ActiveDoc, ThirdParty_Report).RefreshData()
                Case "Distributor_PO_Dispro"
                    DirectCast(Me.ActiveDoc, Distributor_PO_Dispro).RefreshData()
                Case "Agreement_Target"
                    DirectCast(Me.ActiveDoc, Agreement_Target).RefreshData()
                Case "DisproByBrand"
                    DirectCast(Me.ActiveDoc, DisproByBrand).RefreshData()
                Case "DPRD"
                    DirectCast(Me.ActiveDoc, DPRD).RefreshData()
                Case "Accrue"
                    DirectCast(Me.ActiveDoc, Accrue).RefReshData()
                Case "RecapNasionalDPRD"
                    DirectCast(Me.ActiveDoc, RecapNasionalDPRD).RefreshData()
                Case "SummaryPlantationReport"
                    DirectCast(Me.ActiveDoc, SummaryPlantationReport).refreshdata()
                Case "SalesProgramReport"
                    DirectCast(Me.ActiveDoc, SalesProgramReport).RefreshData()
                Case "DDDRReport"
                    DirectCast(Me.ActiveDoc, DDDR).RefreshData()
                Case "SPPBManager"
                    RaiseEvent ShowSPPBData()
            End Select
        End If
    End Sub
    Private Sub ShowDistributorReport(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "Distributor_Report" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim Distreport As New Distributor_Report()
                Distreport.Parent = Me.ExpandablePanel1
                Distreport.Dock = DockStyle.Fill
                Distreport.InitializeDate()
                Distreport.Show()
                Me.m_Grid = Distreport.GridEX1
                Me.ActiveDoc = Distreport
                Distreport.BringToFront()
            End If
        Else
            Dim Distreport As New Distributor_Report()
            Distreport.Parent = Me.ExpandablePanel1
            Distreport.Dock = DockStyle.Fill
            Distreport.InitializeDate()
            Distreport.Show()
            Me.m_Grid = Distreport.GridEX1
            Me.ActiveDoc = Distreport
            Distreport.BringToFront()
        End If
        Me.FilterEditor1.SourceControl = Me.m_Grid
        Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
    End Sub
    Private Sub ShowSPPBManager(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "SPPBManager" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim SPPBRep As New SPPBManager
                'SPPBRep.InitializeDate()
                SPPBRep.Parent = Me.ExpandablePanel1

                SPPBRep.frmParentGrid = Me
                SPPBRep.Show()
                SPPBRep.Dock = DockStyle.Fill
                Me.ActiveDoc = SPPBRep
                Me.m_Grid = DirectCast(Me.ActiveDoc, SPPBManager).grdHeader
                DirectCast(Me.ActiveDoc, SPPBManager).grdHeader.Focus()

                SPPBRep.BringToFront()
            End If
        Else
            Dim SPPBRep As New SPPBManager
            SPPBRep.frmParentGrid = Me
            'SPPBRep.InitializeDate()
            SPPBRep.Parent = Me.ExpandablePanel1
            SPPBRep.Show()
            SPPBRep.Dock = DockStyle.Fill
            Me.ActiveDoc = SPPBRep
            Me.m_Grid = DirectCast(Me.ActiveDoc, SPPBManager).grdHeader
            DirectCast(Me.ActiveDoc, SPPBManager).grdHeader.Focus()

            SPPBRep.BringToFront()
        End If
        Me.FilterEditor1.SourceControl = Me.m_Grid
        Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
    End Sub
    Private Sub ShowSPPBReport(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "SPPB_Report" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim SPPBRep As New SPPB_Report()
                SPPBRep.Parent = Me.ExpandablePanel1
                SPPBRep.Dock = DockStyle.Fill
                SPPBRep.Show()
                Me.m_Grid = SPPBRep.GridEX1
                Me.ActiveDoc = SPPBRep
                SPPBRep.BringToFront()
            End If
        Else
            Dim SPPBRep As New SPPB_Report()
            SPPBRep.Parent = Me.ExpandablePanel1
            SPPBRep.Dock = DockStyle.Fill
            SPPBRep.Show()
            Me.m_Grid = SPPBRep.GridEX1
            Me.ActiveDoc = SPPBRep
            SPPBRep.BringToFront()
        End If
        Me.FilterEditor1.SourceControl = Me.m_Grid
        Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
    End Sub
    'Private Sub showThirdPartyReport(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Not IsNothing(Me.ActiveDoc) Then
    '        If Me.ActiveDoc.Name = "ThirdParty_Report" Then
    '        Else
    '            Me.ActiveDoc.Dispose()
    '            Dim RepThirdPart As New ThirdParty_Report()
    '            RepThirdPart.Parent = Me.ExpandablePanel1
    '            RepThirdPart.Dock = DockStyle.Fill
    '            RepThirdPart.Show()
    '            Me.m_Grid = RepThirdPart.grdMain
    '            Me.ActiveDoc = RepThirdPart
    '            RepThirdPart.BringToFront()
    '        End If
    '    Else
    '        Dim RepThirdPart As New ThirdParty_Report()
    '        RepThirdPart.Parent = Me.ExpandablePanel1
    '        RepThirdPart.Dock = DockStyle.Fill
    '        RepThirdPart.Show()
    '        Me.m_Grid = RepThirdPart.grdMain
    '        Me.ActiveDoc = RepThirdPart
    '        RepThirdPart.BringToFront()
    '    End If
    '    Me.FilterEditor1.SourceControl = Me.m_Grid
    '    Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
    'End Sub
    Private Sub ShowOAReport(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "OA_Report" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim OARep As New OA_Report()
                'OARep.initial
                OARep.Parent = Me.ExpandablePanel1
                OARep.Dock = DockStyle.Fill
                OARep.Show()
                Me.m_Grid = OARep.GridEX1
                Me.ActiveDoc = OARep
                OARep.BringToFront()
            End If
        Else
            Dim OARep As New OA_Report()
            OARep.Parent = Me.ExpandablePanel1
            OARep.Dock = DockStyle.Fill
            OARep.Show()
            Me.m_Grid = OARep.GridEX1
            Me.ActiveDoc = OARep
            OARep.BringToFront()
        End If
        Me.FilterEditor1.SourceControl = Me.m_Grid
        Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
    End Sub
    Private Sub ShowPoDispro(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "Distributor_PO_Dispro" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim DPD As New Distributor_PO_Dispro()
                DPD.Parent = Me.ExpandablePanel1
                DPD.Dock = DockStyle.Fill
                DPD.Show()
                Me.m_Grid = DPD.GridEX1
                Me.ActiveDoc = DPD
                DPD.BringToFront()
            End If
        Else
            'Me.ActiveDoc.Dispose()
            Dim DPD As New Distributor_PO_Dispro()
            DPD.Parent = Me.ExpandablePanel1
            DPD.Dock = DockStyle.Fill
            DPD.Show()
            Me.m_Grid = DPD.GridEX1
            Me.ActiveDoc = DPD
            DPD.BringToFront()
        End If
        Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
    End Sub
    Private Sub ShowAccrue(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "Acrue" Then
            Else
                Me.ActiveDoc.Dispose() : Dim Acr As New Accrue() : Acr.Parent = Me.ExpandablePanel1
                Acr.Dock = DockStyle.Fill : Acr.Show() : Me.m_Grid = Acr.GridEX1 : Me.ActiveDoc = Acr
                Acr.BringToFront()
            End If
        Else
            Dim Acr As New Accrue()
            With Acr
                .Parent = Me.ExpandablePanel1 : .Dock = DockStyle.Fill : .Show()
                Me.m_Grid = .GridEX1 : Me.ActiveDoc = Acr
                .BringToFront()
            End With
        End If
        Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
    End Sub
    Private Sub ShowPODisproByBrand(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "btnPODisproByBrand" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim PDB As New DisproByBrand()
                PDB.Parent = Me.ExpandablePanel1
                PDB.Dock = DockStyle.Fill
                PDB.Show()
                Me.m_Grid = PDB.GridEX1
                Me.ActiveDoc = PDB
                PDB.BringToFront()
            End If
        Else
            Dim PDB As New DisproByBrand()
            PDB.Parent = Me.ExpandablePanel1
            PDB.Dock = DockStyle.Fill
            PDB.Show()
            Me.m_Grid = PDB.GridEX1
            Me.ActiveDoc = PDB
            PDB.BringToFront()
        End If
        Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
    End Sub
    Private Sub ShowTargetAgreement(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "Agreement_Target" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim AT As New Agreement_Target()
                AT.initializeData()
                AT.Parent = Me.ExpandablePanel1
                AT.Dock = DockStyle.Fill
                AT.Show()
                Me.m_Grid = AT.GridEX1
                Me.ActiveDoc = AT
                AT.BringToFront()
            End If
        Else
            'Me.ActiveDoc.Dispose()
            Dim AT As New Agreement_Target()
            AT.initializeData()
            AT.Parent = Me.ExpandablePanel1
            AT.Dock = DockStyle.Fill
            AT.Show()
            Me.m_Grid = AT.GridEX1
            Me.ActiveDoc = AT
            AT.BringToFront()
        End If
        Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
    End Sub
    Private Sub ShowDPRD(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "DPRD" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim ctrlDPRD As New DPRD()
                ctrlDPRD.Parent = Me.ExpandablePanel1
                ctrlDPRD.Dock = DockStyle.Fill
                ctrlDPRD.Show()
                Me.ActiveDoc = ctrlDPRD
                ctrlDPRD.BringToFront()
                Me.m_Grid = ctrlDPRD.Grid()
                ctrlDPRD.FE = Me.FilterEditor1
            End If
        Else
            Dim ctrlDPRD As New DPRD()
            ctrlDPRD.Parent = Me.ExpandablePanel1
            ctrlDPRD.Dock = DockStyle.Fill
            ctrlDPRD.Show()
            Me.ActiveDoc = ctrlDPRD
            ctrlDPRD.BringToFront()
            Me.m_Grid = ctrlDPRD.Grid()
            ctrlDPRD.FE = Me.FilterEditor1
        End If
        Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
    End Sub
    Private Sub ShowRecapNasionalDPRD(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "RecapNasionalDPRD" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim ctrlRec As New RecapNasionalDPRD()
                With ctrlRec
                    Me.m_Grid = .GridEX1 : .Parent = Me.ExpandablePanel1 : .Dock = DockStyle.Fill : .Show() : .BringToFront()
                End With
                Me.ActiveDoc = ctrlRec
            End If
        Else
            Dim ctrlRec As New RecapNasionalDPRD()
            With ctrlRec
                Me.m_Grid = .GridEX1 : .Parent = Me.ExpandablePanel1 : .Dock = DockStyle.Fill : .Show() : .BringToFront()
            End With
            Me.ActiveDoc = ctrlRec
        End If
        Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
    End Sub
    Private Sub ShowReportSummaryPlantation(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "SummaryPlantationReport" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim ctrlSumPlantation As New SummaryPlantationReport()
                With ctrlSumPlantation
                    .frmParent = Me : .Parent = Me.ExpandablePanel1 : .Dock = DockStyle.Fill : .Show() : Me.AcceptButton = .btnFilteDate : .BringToFront()
                End With
                Me.ActiveDoc = ctrlSumPlantation
            End If
        Else
            Dim ctrlSumPlantation As New SummaryPlantationReport()
            With ctrlSumPlantation
                .frmParent = Me : .Parent = Me.ExpandablePanel1 : .Dock = DockStyle.Fill : .Show() : Me.AcceptButton = .btnFilteDate : .BringToFront()
            End With
            Me.ActiveDoc = ctrlSumPlantation
        End If
    End Sub
    Private Sub ShowSalesReport(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "SalesProgramReport" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim ctrlSalesProgramReport As New SalesProgramReport()
                With ctrlSalesProgramReport
                    .frmParent = Me : .Parent = Me.ExpandablePanel1 : .Dock = DockStyle.Fill : .Show() : Me.AcceptButton = .btnApplyRange : .BringToFront()
                End With
                Me.ActiveDoc = ctrlSalesProgramReport
            End If
        Else
            Dim ctrlSalesProgramReport As New SalesProgramReport()
            With ctrlSalesProgramReport
                .frmParent = Me : .Parent = Me.ExpandablePanel1 : .Dock = DockStyle.Fill : .Show() : Me.AcceptButton = .btnApplyRange : .BringToFront()
            End With
            Me.ActiveDoc = ctrlSalesProgramReport
        End If
    End Sub

    Private Sub ShowDDDRReport(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "DDDRReport" Then
            Else
                Me.ActiveDoc.Dispose()
                Dim ctrlDDDR As New DDDR()
                With ctrlDDDR
                    .frmParent = Me : .Parent = Me.ExpandablePanel1 : .Dock = DockStyle.Fill : .Show() : Me.AcceptButton = .btnApplyRange : .BringToFront()
                End With
                Me.ActiveDoc = ctrlDDDR
                Me.m_Grid = DirectCast(Me.ActiveDoc, DDDR).grpCategoryDiscount
            End If
        Else
            Dim ctrlDDDR As New DDDR()
            With ctrlDDDR
                .frmParent = Me : .Parent = Me.ExpandablePanel1 : .Dock = DockStyle.Fill : .Show() : Me.AcceptButton = .btnApplyRange : .BringToFront()
            End With
            Me.ActiveDoc = ctrlDDDR
            Me.m_Grid = DirectCast(Me.ActiveDoc, DDDR).grpCategoryDiscount
        End If
        DirectCast(Me.ActiveDoc, DDDR).dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate.AddMonths(-1)
        DirectCast(Me.ActiveDoc, DDDR).dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate.AddMonths(1)
        DirectCast(Me.ActiveDoc, DDDR).cmbApplyDiscount.SelectedIndex = 0
    End Sub
    Private Sub ShowCardView()
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "DPRD" Then
                Me.m_Grid = DirectCast(Me.ActiveDoc, DPRD).Grid
            End If
        End If
        If Not IsNothing(Me.m_Grid) Then
            Me.m_Grid.View = Janus.Windows.GridEX.View.CardView
            Me.m_Grid.RecordNavigator = False
            Me.m_Grid.CollapseCards()
        End If
    End Sub
    Private Sub ShowSingleCard()
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "DPRD" Then
                Me.m_Grid = DirectCast(Me.ActiveDoc, DPRD).Grid
            End If
        End If
        If Not IsNothing(Me.m_Grid) Then
            Me.m_Grid.View = Janus.Windows.GridEX.View.SingleCard
            Me.m_Grid.RecordNavigator = True
            'Me.m_Grid.RootTable.Columns("Icon").Visible = False
        End If
    End Sub
    Private Sub ShowTableView()
        If Not IsNothing(Me.ActiveDoc) Then
            If Me.ActiveDoc.Name = "DPRD" Then
                Me.m_Grid = DirectCast(Me.ActiveDoc, DPRD).Grid
            End If
        End If
        If Not IsNothing(Me.m_Grid) Then
            Me.m_Grid.View = Janus.Windows.GridEX.View.TableView
            'Me.m_Grid.RootTable.Columns("Icon").Visible = True
        End If
    End Sub


    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ClearCheckedState()
            If CType(sender, DevComponents.DotNetBar.BaseItem).SubItems.Count > 0 Then
                If Me.MdiChildren.Length <= 0 Then : Return : End If
            End If
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnShowFieldChooser" : Me.ShowFieldChooser()
                Case "btnSettingGrid" : Me.ShowSettingGrid()
                Case "btnPrint" : Me.ShowPrinting()
                Case "btnPageSettings" : If Not IsNothing(Me.m_Grid) Then : Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1 : Me.PageSetupDialog1.ShowDialog(Me) : End If
                Case "btnCustomFilter" : Me.ShowCustomFilter()
                Case "btnFilterEqual" : Me.ShowFilterEqual()
                Case "btnExport" : Me.ShowExportReport()
                Case "btnRefresh" : Me.RefreshData()
                Case "btnDistributorReport" : Me.ShowDistributorReport(sender, e)
                Case "btnSPPBReport"
                    Me.ShowSPPBManager(sender, e)
                    'Me.ShowSPPBReport(sender, e)
                    'Case "btnThirdParty" : Me.showThirdPartyReport(sender, e)
                Case "btnOAReport" : Me.ShowOAReport(sender, e)
                Case "btnPODispro" : Me.ShowPoDispro(sender, e)
                Case "btnAccrue" : Me.ShowAccrue(sender, e)
                Case "btnPODisproByBrand" : Me.ShowPODisproByBrand(sender, e)
                Case "btnTargetAgreement" : Me.ShowTargetAgreement(sender, e)
                Case "btnDPRD" : Me.ShowDPRD(sender, e)
                Case "RecapNasionalDPRD" : Me.ShowRecapNasionalDPRD(sender, e)
                Case "btnSummaryPlantation" : Me.ShowReportSummaryPlantation(sender, e)
                Case "btnCardView" : Me.ShowCardView()
                Case "btnSingleCard" : Me.ShowSingleCard()
                Case "btnTableView" : Me.ShowTableView()
                Case "btnReportSales" : Me.ShowSalesReport(sender, e)
                Case "btnReportDDDR" : Me.ShowDDDRReport(sender, e)
            End Select
            If Not IsNothing(Me.ActiveDoc) Then
                If Not IsNothing(Me.m_Grid) Then
                    If Me.m_Grid.RootTable.Columns.Contains("Icon") Then
                        Me.m_Grid.RootTable.Columns("Icon").Visible = False
                        'Me.m_Grid.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                    End If
                End If
            End If
            'Me.GetStateChecked(DirectCast(sender, DevComponents.DotNetBar.ButtonItem))
        Catch ex As Exception
            MyBase.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
            If TypeOf (Me.ActiveDoc) Is SPPBManager Then
                DirectCast(Me.ActiveDoc, SPPBManager).IsLoadding = False
                DirectCast(Me.ActiveDoc, SPPBManager).StatProg = SPPBManager.StatusProgress.None
            End If
            Me.ClearCheckedState() : MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Report_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not IsNothing(Me.ActiveDoc) Then
                Me.ActiveDoc.Dispose()
                Me.ActiveDoc = Nothing
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Report_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.Transparent
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Contains
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 25)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(0, 0)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(1028, 29)
        Me.FilterEditor1.Visible = False
        Me.Controls.Add(Me.FilterEditor1)
        CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
    End Sub

    Private Sub ExpandablePanel1_ControlAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles ExpandablePanel1.ControlAdded
        Select Case e.Control.Name
            Case "DPRD"
                Me.ExpandablePanel1.TitleText = "DISCOUNT PROGRESSIVE RETAILER DEALER REPORT DATA"
            Case "OA_Report"
                Me.ExpandablePanel1.TitleText = "ORDER ACCEPTANCE REPORT(SUMMARY)"
            Case "Distributor_Report"
                Me.ExpandablePanel1.TitleText = "PURCHASE ORDER REPORT WITH ALL DISTRIBUTORS"
            Case "ThirdParty_Report"
                Me.ExpandablePanel1.TitleText = "THIRD PARTY REPORT DATA"
            Case "SPPB_Report"
                Me.ExpandablePanel1.TitleText = "SPPB REPORT DATA"
            Case "Distributor_PO_Dispro"
                Me.ExpandablePanel1.TitleText = "DISTRIBUTOR PO AND DISPRO REPORT DATA"
            Case "DisproByBrand"
                Me.ExpandablePanel1.TitleText = "DISPRO BY BRAND (SUMMARY)"
            Case "Agreement_Target"
                Me.ExpandablePanel1.TitleText = "TARGET AGREEMENT DISTRIBUTOR REPORT DATA"
            Case "Accrue"
                Me.ExpandablePanel1.TitleText = "ACCRUE REPORT DATA"
            Case "RecapNasionalDPRD"
                Me.ExpandablePanel1.TitleText = "RECAP DPRD REPORT DATA"
            Case "SummaryPlantationReport"
                Me.ExpandablePanel1.TitleText = "ACTUAL OF PLANTATION REPORT DATA"
            Case "SalesProgramReport"
                Me.ExpandablePanel1.TitleText = "TRACKING OF SALES PROGRAM DATA"
            Case "SPPBManager"
                Me.ExpandablePanel1.TitleText = "SPPB & GON REPORT"
            Case "DDDR"
                Me.ExpandablePanel1.TitleText = "DETAIL REPORT OF DD DR CBD"
        End Select
        'Me.ExpandablePanel1.TitleText = e.Control.Text
    End Sub
End Class
