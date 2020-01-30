Public Class Agreement
#Region " Deklarasi "
    Private WithEvents frmAgBrand As AgreementRelation
    Private clsAgreement As NufarmBussinesRules.DistributorAgreement.Agreement
    Private WithEvents frmAgDistributor As DistributorAgreement
    Private SelectedGrid As GridSelect
    Friend CMain As Main = Nothing
#End Region

#Region " Enum "
    Private Enum GridSelect
        grdAgreement
        grdBrand
    End Enum
#End Region

#Region " Sub "
    Private Sub FrmAgreementBradnClose() Handles frmAgBrand.CloseThis
        Me.frmAgBrand = Nothing
        'next result
    End Sub
    Private Sub frmAgrDistributorClose() Handles frmAgDistributor.CloseThis
        Me.frmAgDistributor = Nothing
    End Sub
    Private Sub RefreshData()
        Me.LoadData()
    End Sub
    Private Sub LoadData()
        Me.clsAgreement = New NufarmBussinesRules.DistributorAgreement.Agreement()
        'Me.clsAgreement.CreateViewAgreement()
        'If Me.chkShowAll.Checked = False Then
        '    Me.clsAgreement.ViewAgreement().RowFilter = "END_DATE >= " + NufarmBussinesRules.SharedClass.ServerDateString()
        'Else
        '    Me.clsAgreement.ViewAgreement().RowFilter = ""
        'End If
        'Me.BindGrid(Me.grdAgreement, Me.clsAgreement.ViewAgreement())
    End Sub
    Public Sub InitializeData()
        Me.LoadData()
    End Sub
    Private Sub AddConditionalFormating()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdAgreement.RootTable.Columns("END_DATE"), Janus.Windows.GridEX.ConditionOperator.LessThan, NufarmBussinesRules.SharedClass.ServerDate())
        fc.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
        fc.FormatStyle.ForeColor = SystemColors.GrayText
        Me.grdAgreement.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub BindGrid(ByVal grd As Janus.Windows.GridEX.GridEX, ByVal dtView As DataView)
        grd.SetDataBinding(dtView, "")
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
    Private Sub grdAgreement_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAgreement.Enter
        Try
            Me.SelectedGrid = GridSelect.grdAgreement
            Dim S As Boolean = Me.FilterEditor1.Visible
            Me.FilterEditor1.SourceControl = grdAgreement
            Me.FilterEditor1.Visible = S
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnRename"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.grdAgreement
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar3, True)
                Case "btnShowFieldChooser"
                    Me.grdAgreement.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.grdAgreement
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.grdAgreement
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
                    Me.FilterEditor1.SourceControl = Me.grdAgreement
                    Me.grdAgreement.RemoveFilters()
                    Me.FilterEditor1.Visible = True
                    Me.grdAgreement.FilterMode = Janus.Windows.GridEX.FilterMode.None
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.grdAgreement.RemoveFilters()
                    Me.grdAgreement.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Case "btnDistAgreement"
                    Me.frmAgDistributor = New DistributorAgreement()
                    Me.frmAgDistributor.CMain = Me.CMain
                    Me.frmAgDistributor.InitializeData()
                    Me.frmAgDistributor.ShowInTaskbar = False
                    Me.frmAgDistributor.ShowDialog(Me)
                    Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs())
                Case "btnAgreeDetail"
                    Me.frmAgBrand = New AgreementRelation()
                    Me.frmAgBrand.CMain = Me.CMain
                    frmAgBrand.InitializeData()
                    Me.frmAgBrand.ShowInTaskbar = False
                    Me.frmAgBrand.ShowDialog(Me)
                    Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs())
                Case "btnExport"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.grdAgreement
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnRefresh"
                    Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs())
            End Select

        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Agreement_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not IsNothing(Me.clsAgreement) Then
                Me.clsAgreement.Dispose(True)
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub chkShowAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowAll.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.chkShowAll.Checked = False Then
                Me.clsAgreement.ViewAgreement().RowFilter = "END_DATE >= " + NufarmBussinesRules.SharedClass.ServerDateString()
            Else
                Me.clsAgreement.ViewAgreement().RowFilter = ""
            End If
            Me.BindGrid(Me.grdAgreement, Me.clsAgreement.ViewAgreement())
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Agreement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.FilterEditor1.Visible = False
            ''get agreement Periode
            Dim startDate As DateTime = DateTime.Now
            Dim endDate As DateTime = DateTime.Now
            Me.clsAgreement.GetCurrentAgreement(startDate, endDate, False)
            Me.dtPicFrom.Value = startDate
            Me.dtPicUntil.Value = endDate
            If Not CMain.IsSystemAdministrator Then
                If NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DistributorAgreement = True Then
                    Me.btnDistAgreement.Visible = True
                Else
                    Me.btnDistAgreement.Visible = False
                End If
                If NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AgreementRelation = True Then
                    Me.btnAgreeDetail.Visible = True
                Else
                    Me.btnAgreeDetail.Visible = False
                End If
                If Me.btnManage.VisibleSubItems <= 0 Then
                    Me.btnManage.Visible = False
                Else
                    Me.btnManage.Visible = True
                End If
            End If
            Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs())
        Catch ex As Exception
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Agreement_Load")
        Finally
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnAplyrange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyrange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim START_DATE As Date
            Dim END_DATE As Date
            Me.Cursor = Cursors.WaitCursor
            If (Me.dtPicFrom.Text <> "") And (Me.dtPicUntil.Text <> "") Then
                START_DATE = Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString())
                END_DATE = Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())
                Me.clsAgreement.CreateViewAgreement(START_DATE, END_DATE)
            ElseIf (Me.dtPicFrom.Text <> "") And (Me.dtPicUntil.Text = "") Then
                START_DATE = Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString())
                Me.clsAgreement.CreateViewAgreement(START_DATE, Nothing)
            ElseIf (Me.dtPicFrom.Text = "") And (Me.dtPicUntil.Text <> "") Then
                END_DATE = Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())
                Me.clsAgreement.CreateViewAgreement(Nothing, END_DATE)
            Else
                Me.clsAgreement.CreateViewAgreement()
            End If
            Me.chkShowAll_CheckedChanged(Me.chkShowAll, New EventArgs())
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_btnAplyrange_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub dtPicFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicFrom.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.dtPicFrom.Text = ""
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

#End Region

End Class