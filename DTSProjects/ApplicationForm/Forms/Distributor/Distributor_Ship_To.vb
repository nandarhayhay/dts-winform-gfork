Public Class Distributor_Ship_To
#Region " Deklarasi "
    Private m_clsShipTo As NufarmBussinesRules.DistributorRegistering.ShipTerritory
    Private SGrid As SelectedGrid
    Private SaveMode As Mode
    Private SFG As StateFillingGrid
    Private SFM As StateFillingMCB
    Private Property clsShipTo() As NufarmBussinesRules.DistributorRegistering.ShipTerritory
        Get
            If Me.m_clsShipTo Is Nothing Then
                Me.m_clsShipTo = New NufarmBussinesRules.DistributorRegistering.ShipTerritory()
            End If
            Return Me.m_clsShipTo
        End Get
        Set(ByVal value As NufarmBussinesRules.DistributorRegistering.ShipTerritory)
            Me.m_clsShipTo = value
        End Set
    End Property
    Private Sub BindCheckedCombo(ByVal dtview As DataView, ByVal rowfilter As String)
        Me.SFM = StateFillingMCB.Filing
        If dtview Is Nothing Then
            Me.chkTerritory.SetDataBinding(Nothing, "")
        End If
        'Me.chkTerritory.SetDataBinding(dtview, "")
        If Not dtview Is Nothing Then
            dtview.RowFilter = rowfilter
            Me.chkTerritory.SetDataBinding(dtview, "")
            'Me.chkTerritory.DropDownList.Columns("DISTRIBUTOR_ID").AutoSize()
            'Me.chkTerritory.DropDownList.Columns("DISTRIBUTOR_ID").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCellsAndHeader
            'Me.chkTerritory.DropDownList.Columns("DISTRIBUTOR_NAME").AutoSize()
            'Me.chkTerritory.DropDownList.Columns("DISTRIBUTOR_NAME").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCellsAndHeader
            Me.chkTerritory.DropDownList.Columns("TERRITORY_ID").AutoSize()
            Me.chkTerritory.DropDownList.Columns("TERRITORY_ID").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCellsAndHeader

            Me.chkTerritory.DropDownList.Columns("TERRITORY_AREA").AutoSize()
            Me.chkTerritory.DropDownList.Columns("TERRITORY_AREA").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCellsAndHeader
        End If
        Me.SFM = StateFillingGrid.HasFilled
    End Sub
    Private Sub BindMultiColumnCombo(ByVal dtview As DataView, ByVal rowfilter As String)
        Me.SFM = StateFillingMCB.Filing
        If dtview Is Nothing Then
            Me.mcbDistributor.SetDataBinding(Nothing, "")
        End If
        If Not dtview Is Nothing Then
            dtview.RowFilter = rowfilter
            Me.mcbDistributor.SetDataBinding(dtview, "")
            Me.mcbDistributor.DropDownList.Columns("DISTRIBUTOR_ID").AutoSize()
            Me.mcbDistributor.DropDownList.Columns("DISTRIBUTOR_ID").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCellsAndHeader
            Me.mcbDistributor.DropDownList.Columns("DISTRIBUTOR_NAME").AutoSize()
            Me.mcbDistributor.DropDownList.Columns("DISTRIBUTOR_NAME").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCellsAndHeader

        End If
        Me.SFM = StateFillingMCB.HasFilled
    End Sub
#End Region

#Region " Enum "

    Private Enum SelectedGrid
        grdTerritory
        grdShipTo
    End Enum
    Private Enum Mode
        Save
        Update
    End Enum
    Private Enum StateFillingGrid
        Filling
        Disposing
        HasFilled
    End Enum
    Private Enum StateFillingMCB
        Filing
        HasFilled
    End Enum
#End Region

#Region " Sub procedure "

    Friend Sub InitalizeData()
        Me.LoadData()
    End Sub

    Private Sub InflateDatea(ByVal Sgrid As SelectedGrid)
        Select Case Sgrid
            Case SelectedGrid.grdShipTo
                'data shipto tidak boleh di edit
            Case SelectedGrid.grdTerritory
                Me.ClearControl(Me.grpterritpry)
                Me.grpShipTo.Visible = False
                Me.grpTerritory.Visible = True
                Me.txtTerritoryID.Text = Me.grdTerritory.GetValue("TERRITORY_ID").ToString()
                Me.txtTerritoryArea.Text = Me.grdTerritory.GetValue("TERRITORY_AREA").ToString()
                txtTerritoryDescription.Text = Me.grdTerritory.GetValue("TERRITORY_DESCRIPTION").ToString()

        End Select
    End Sub

    Private Sub Bindgrid(ByVal grid As Janus.Windows.GridEX.GridEX, ByVal Dtview As DataView)
        Me.SFG = StateFillingGrid.Filling
        grid.SetDataBinding(Dtview, "")
        Me.SFG = StateFillingGrid.HasFilled

    End Sub

    Private Sub LoadData()
        Me.clsShipTo.GetDataView()
        Me.Bindgrid(Me.grdShipTo, Me.clsShipTo.ViewShipToTerritory())
        Me.Bindgrid(Me.grdTerritory, Me.clsShipTo.ViewTerritoryArea())
        Me.BindMultiColumnCombo(Me.clsShipTo.ViewDistributor(), "")
        Me.BindCheckedCombo(Nothing, "")
    End Sub

    Private Function IsValid() As Boolean
        Try
            If Me.grpShipTo.Visible = True Then
                'If Me.chkTerritory.Values Is Nothing Then
                '    Me.baseTooltip.Show("Please define Territory", Me.chkTerritory, 3000)
                '    Me.chkTerritory.Focus()
                '    Return False
                'Else
                If Me.chkTerritory.CheckedValues.Length <= 0 Then
                    Me.baseTooltip.Show("Please define Territory", Me.chkTerritory, 3000)
                    Me.chkTerritory.Focus()
                    Return False
                ElseIf Me.mcbDistributor.SelectedItem Is Nothing Then
                    Me.baseTooltip.Show("Please define Distributor", Me.mcbDistributor, 3000)
                    Me.mcbDistributor.Focus()
                    Return False
                End If
            ElseIf Me.grdTerritory.Visible = True Then
                If Me.txtTerritoryID.Text = "" Then
                    Me.baseTooltip.Show("Please define Terrirory_ID", Me.txtTerritoryID, 3000)
                    Me.txtTerritoryID.Focus()
                    Return False
                ElseIf Me.txtTerritoryArea.Text = "" Then
                    Me.baseTooltip.Show("Please define Territory_Area", Me.txtTerritoryArea, 3000)
                    Me.txtTerritoryArea.Focus()
                    Return False
                ElseIf Me.txtTerritoryDescription.Text = "" Then
                    Me.baseTooltip.Show("Please define Territory Description", Me.txtTerritoryDescription, 3000)
                    Me.txtTerritoryDescription.Focus()
                    Return False
                End If
            Else
                Me.ShowMessageInfo("nothing to select")
                Return False
            End If


            'Select Case Me.SGrid
            '    Case SelectedGrid.grdShipTo
            '        'grid shipto hanya mode save saja

            '    Case SelectedGrid.grdTerritory

            'End Select
            Return True
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region " Event Procedure "

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Select Case Me.SGrid
                Case SelectedGrid.grdShipTo
                    Me.SFM = StateFillingMCB.Filing
                    Me.mcbDistributor.Value = Nothing
                    Me.chkTerritory.Values = Nothing
                    Me.chkTerritory.UncheckAll()
                    Me.mcbDistributor.Focus()
                    Me.ClearControl(Me.grpterritpry)
                    Me.txtTerritoryID.Enabled = True
                Case SelectedGrid.grdTerritory
                    Me.ClearControl(Me.grpterritpry)
                    Me.txtTerritoryID.Enabled = True
                    Me.txtTerritoryID.Focus()
            End Select
            Me.btnInsert.Enabled = True
            Me.SaveMode = Mode.Save
            Me.btnInsert.Text = "&Insert"

        Catch ex As Exception

            Me.LogMyEvent(ex.Message, Me.Name + "_btnAdd_Click")
        Finally
            Me.SFM = StateFillingMCB.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mcbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mcbDistributor.Value Is Nothing Then
                Return
            End If
            If Me.mcbDistributor.SelectedItem Is Nothing Then
                Return
            End If
            If Me.SFM = StateFillingMCB.Filing Then
                Return
            End If
            Me.clsShipTo.CreateViewTerritory(Me.mcbDistributor.Value.ToString())
            Me.BindCheckedCombo(Me.clsShipTo.ViewUnReservedTerritory(), "")

        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbDistributor_ValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Dim dtview As DataView = CType(Me.mcbDistributor.DataSource, DataView)
            Dim rowfilter As String = "DISTRIBUTOR_NAME LIKE '%" & Me.mcbDistributor.Text & "%'"
            Me.BindMultiColumnCombo(dtview, rowfilter)
            Dim itemCount As Integer = Me.mcbDistributor.DropDownList.RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " Item(s) found")
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterShipTo_btnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterShipTo.btnClick
        Try
            Dim rowfilter As String = "TERRITORY_AREA LIKE '%" & Me.chkTerritory.Text & "%'"
            Dim dtview As DataView = CType(Me.chkTerritory.DropDownDataSource, DataView)
            Me.BindCheckedCombo(dtview, rowfilter)
            Dim itemCount As String = Me.chkTerritory.DropDownList.RecordCount().ToString()
            Me.ShowMessageInfo(itemCount & " Item(s) found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mnuBar_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBar.ItemClick
        Try
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnRenameColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    Select Case Me.SGrid
                        Case SelectedGrid.grdShipTo
                            MC.grid = Me.grdShipTo
                        Case SelectedGrid.grdTerritory
                            MC.grid = Me.grdTerritory
                    End Select
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar1)
                Case "btnShowFieldChooser"
                    Select Case Me.SGrid
                        Case SelectedGrid.grdShipTo
                            Me.grdShipTo.ShowFieldChooser(Me)
                        Case SelectedGrid.grdTerritory
                            Me.grdTerritory.ShowFieldChooser(Me)
                    End Select
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    Select Case Me.SGrid
                        Case SelectedGrid.grdShipTo
                            SetGrid.Grid = Me.grdShipTo
                            SetGrid.GridExPrintDock = Me.GridEXPrintDocument2
                        Case SelectedGrid.grdTerritory
                            SetGrid.Grid = Me.grdTerritory
                            SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    End Select
                    SetGrid.ShowDialog()
                Case "btnPrint"
                    Select Case Me.SGrid
                        Case SelectedGrid.grdShipTo
                            Me.GridEXPrintDocument2.GridEX = Me.grdShipTo
                            Me.PrintPreviewDialog2.Document = Me.GridEXPrintDocument2
                            If Not IsNothing(Me.PageSetupDialog2.PageSettings) Then
                                Me.PrintPreviewDialog2.Document.DefaultPageSettings = Me.PageSetupDialog2.PageSettings
                            End If
                            'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                            If Me.PrintPreviewDialog2.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Me.PrintPreviewDialog2.Document.Print()
                            End If
                        Case SelectedGrid.grdTerritory
                            Me.GridEXPrintDocument1.GridEX = Me.grdTerritory
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
                    Select Case SGrid
                        Case SelectedGrid.grdShipTo
                            Me.PageSetupDialog2.Document = Me.GridEXPrintDocument2
                            Me.PageSetupDialog2.ShowDialog(Me)
                        Case SelectedGrid.grdTerritory
                            Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                            Me.PageSetupDialog1.ShowDialog(Me)
                    End Select
                Case "btnCustomFilter"
                    Me.FilterEditor1.Visible = True
                    Me.FilterEditor1.SortFieldList = False
                    Select Case Me.SGrid
                        Case SelectedGrid.grdShipTo
                            Me.FilterEditor1.SourceControl = Me.grdShipTo
                            Me.grdShipTo.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            Me.grdShipTo.RemoveFilters()
                            Me.clsShipTo.ViewShipToTerritory().RowFilter = ""
                            Me.grdShipTo.Refetch()
                            'Me.dtPicFrom.Text = ""
                            'Me.dtpicUntil.Text = ""
                            'Me.dtPicFrom.Enabled = False
                            'Me.dtpicUntil.Enabled = False
                            'Me.btnAplyRange.Enabled = False
                        Case SelectedGrid.grdTerritory
                            Me.FilterEditor1.SourceControl = Me.grdTerritory
                            Me.grdTerritory.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            Me.grdTerritory.RemoveFilters()
                            Me.clsShipTo.ViewTerritoryArea().RowFilter = ""
                            Me.grdTerritory.Refetch()
                    End Select
                    'Me.GetStateChecked(Me.btnCustomFilter)
                Case "btnFilterDefault"
                    Me.FilterEditor1.Visible = False
                    Select Case Me.SGrid
                        Case SelectedGrid.grdShipTo
                            Me.grdShipTo.RemoveFilters()
                            'Me.GetStateChecked(Me.btnFilterEqual)
                            Me.grdShipTo.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            'Me.btnAplyRange.Enabled = True
                            'Me.dtPicFrom.Enabled = True
                            'Me.dtpicUntil.Enabled = True
                        Case SelectedGrid.grdTerritory
                            Me.grdTerritory.RemoveFilters()
                            'Me.GetStateChecked(Me.btnFilterEqual)
                            'Me.btnAplyRange.Enabled = True
                            'Me.dtPicFrom.Enabled = True
                            'Me.dtpicUntil.Enabled = True
                            Me.grdTerritory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    End Select
                Case "btnAddNew"
                    'Dim frmPurchaseOrder As New Purchase_Order()
                    'frmPurchaseOrder.ShowInTaskbar = False
                    'frmPurchaseOrder.Mode = Purchase_Order.ModeSave.Save
                    'frmPurchaseOrder.InitializeData()
                    'Me.GetStateChecked(Me.btnAddNew)
                    'frmPurchaseOrder.ShowDialog(Me)
                    'Me.ClearCheckedState()
                    'Me.RefreshData()
                    Me.btnAddNew.Checked = Not Me.btnAddNew.Checked
                    If Me.btnAddNew.Checked = True Then
                        Me.ExpandableSplitter1.Expanded = True
                        Select Case Me.SGrid
                            Case SelectedGrid.grdShipTo
                                Me.grpShipTo.Visible = True
                                Me.grpterritpry.Visible = False
                                Me.SFM = StateFillingMCB.Filing
                                Me.mcbDistributor.Value = Nothing
                                Me.chkTerritory.Values = Nothing
                                Me.mcbDistributor.Focus()
                            Case SelectedGrid.grdTerritory
                                Me.grpterritpry.Visible = True
                                Me.grpShipTo.Visible = False
                                Me.ClearControl(Me.grpterritpry)
                                Me.txtTerritoryID.Enabled = True
                                Me.txtTerritoryID.Focus()
                        End Select

                        Me.btnInsert.Text = "&Insert"
                        Me.btnInsert.Enabled = True
                        Me.SaveMode = Mode.Save
                    Else
                        Me.ExpandableSplitter1.Expanded = False
                    End If

                Case "btnExport"
                    Select Case Me.SGrid
                        Case SelectedGrid.grdShipTo
                            Me.SaveFileDialog1.Title = "Define the location File"
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.grdShipTo
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Case SelectedGrid.grdTerritory
                            Me.SaveFileDialog1.Title = "Define the location File"
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.grdTerritory
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                    End Select

                Case "btnRefresh"
                    'Me.LoadData()
                    Me.LoadData()

            End Select
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_mnuBar_ItemClick")
        Finally
            Me.SFM = StateFillingMCB.HasFilled
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdTerritory_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTerritory.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If (Me.SFG = StateFillingGrid.Filling) Or (Me.SFG = StateFillingGrid.Disposing) Then
                Return
            End If
            If Not Me.grdTerritory.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Me.SaveMode = Mode.Update
            'check refereced data
            Me.InflateDatea(SelectedGrid.grdTerritory)
            If Me.clsShipTo.IsHasReferencedData(Me.grdTerritory.GetValue("TERRITORY_ID").ToString()) = True Then
                'Me.txtTerritoryArea.Enabled = False
                'Me.txtTerritoryDescription.Enabled = False
                Me.txtTerritoryID.Enabled = False
                'Me.btnInsert.Enabled = False
            Else
                'Me.txtTerritoryArea.Enabled = True
                'Me.txtTerritoryDescription.Enabled = True
                Me.txtTerritoryID.Enabled = True
                'Me.btnInsert.Enabled = True
            End If
            Me.btnInsert.Text = "&Update"
            Me.ExpandableSplitter1.Expanded = True
            Me.grpterritpry.Visible = True
            Me.grpShipTo.Visible = False
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled
            Me.LogMyEvent(ex.Message, Me.Name + "_grdTerritory_CurrentCellChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdShipTo_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdShipTo.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If (Me.SFG = StateFillingGrid.Filling) Or (Me.SFG = StateFillingGrid.Disposing) Then
                Return
            End If
            If Not Me.grdShipTo.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Me.ExpandableSplitter1.Expanded = False
            Me.grpShipTo.Visible = False

        Catch ex As Exception
        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Distributor_Ship_To_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsShipTo) Then
                Me.SFG = StateFillingGrid.Disposing
                Me.clsShipTo.Dispose(True)
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_Distributor_Ship_To_FormClosing")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Distributor_Ship_To_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.AcceptButton = Me.btnInsert
            Me.ExpandableSplitter1.Expanded = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdShipTo_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdShipTo.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Return
            End If
            Me.clsShipTo.DeleteShipToArea(Me.grdShipTo.GetValue("DIST_SHIP_TO_ID").ToString())
            e.Cancel = False
            Me.LoadData()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdShipTo_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdShipTo_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdShipTo.MouseDoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.grdShipTo.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                'e.Cancel = True
                Return
            End If
            If Not Me.grdShipTo.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Me.clsShipTo.DeleteShipToArea(Me.grdShipTo.GetValue("DIST_SHIP_TO_ID").ToString())
            'e.Cancel = False
            Me.LoadData()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdShipTo_MouseDoubleClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdTerritory_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdTerritory.MouseDoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.grdTerritory.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                'e.Cancel = True
                Return
            End If
            If Not Me.grdTerritory.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Me.clsShipTo.DeleteTerritoryArea(Me.grdTerritory.GetValue("TERRITORY_ID").ToString())
            'e.Cancel = False
            Me.LoadData()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name & "_grdTerritory_MouseDoubleClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdTerritory_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdTerritory.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Return
            End If
            Me.clsShipTo.DeleteTerritoryArea(Me.grdTerritory.GetValue("TERRITORY_ID").ToString())
            e.Cancel = False
            Me.LoadData()
            Me.ClearControl(Me.grpterritpry)
            Me.grdTerritory_CurrentCellChanged(Me.grdTerritory, New EventArgs())
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdTerritory_DeletingRecord")
            e.Cancel = True
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.IsValid() = False Then
                Return
            End If
            Dim x As Integer = 1

            Select Case Me.SGrid
                Case SelectedGrid.grdShipTo
                    Me.clsShipTo.DistributorID = Me.mcbDistributor.Value.ToString()
                    Dim TERRITORY_IDS As New Collection()
                    TERRITORY_IDS.Clear()
                    For I As Integer = 0 To Me.chkTerritory.CheckedValues.Length - 1
                        TERRITORY_IDS.Add(Me.chkTerritory.CheckedValues().GetValue(I).ToString(), x)
                        x += 1
                    Next
                    Me.clsShipTo.SaveShiptoTerritory(TERRITORY_IDS)
                    Me.mcbDistributor.Value = Nothing
                    Me.chkTerritory.Values = Nothing
                Case SelectedGrid.grdTerritory
                    Me.clsShipTo.TERRITORY_ID = Me.txtTerritoryID.Text.Trim()
                    Me.clsShipTo.TERRITORY_AREA = Me.txtTerritoryArea.Text
                    Me.clsShipTo.TERRITORY_DESCRIPTION = Me.txtTerritoryDescription.Text
                    Select Case Me.SaveMode
                        Case Mode.Save
                            Me.clsShipTo.SaveTerritoryArea("Insert")
                        Case Mode.Update
                            Me.clsShipTo.SaveTerritoryArea("Update")
                            Me.ClearControl(Me.grpterritpry)
                    End Select

            End Select
            Me.ShowMessageInfo(Me.MessageSavingSucces)
            Me.LoadData()
            Me.btnAdd_Click(Me.btnAdd, New EventArgs())
            'Me.btnInsert.Text = "&Insert"
            'Me.btnInsert.Focus()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnInsert_Click")
        Finally
            Me.SaveMode = Mode.Save
            Me.SFM = StateFillingMCB.HasFilled
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdShipTo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdShipTo.Enter
        Me.SGrid = SelectedGrid.grdShipTo
        Dim S As Boolean = Me.FilterEditor1.Visible
        Me.FilterEditor1.SourceControl = Me.grdShipTo
        Me.FilterEditor1.Visible = S


        Me.grpShipTo.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdShipTo.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdShipTo.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.grdShipTo.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.grdShipTo.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdShipTo.BackColor = Color.FromArgb(158, 190, 245)

        Me.grdTerritory.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdTerritory.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdTerritory.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdTerritory.BackColor = Color.FromArgb(194, 217, 247)
    End Sub

    Private Sub grdTerritory_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTerritory.Enter
        Me.SGrid = SelectedGrid.grdTerritory
        Dim S As Boolean = Me.FilterEditor1.Visible
        Me.FilterEditor1.SourceControl = Me.grdShipTo
        Me.FilterEditor1.Visible = S
        Me.grpTerritory.BackColor = Color.FromArgb(158, 190, 245)
        grdTerritory.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        grdTerritory.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        grdTerritory.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        grdTerritory.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdTerritory.BackColor = Color.FromArgb(158, 190, 245)

        Me.grdShipTo.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdShipTo.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdShipTo.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
        Me.grdShipTo.BackColor = Color.FromArgb(194, 217, 247)
    End Sub

#End Region

End Class