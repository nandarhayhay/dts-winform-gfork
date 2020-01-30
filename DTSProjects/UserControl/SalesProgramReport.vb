Imports System.Threading

Public Class SalesProgramReport
    Private clsProgram As NufarmBussinesRules.Program.BrandPackInclude
    Private isLoadingRow As Boolean = False
    Public frmParent As ReportGrid = Nothing
    Private OtherUserProcessing As String = ""
    Private LD As Loading
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Friend CMain As Main = Nothing
    Friend DS As DataSet
    Private HasLoadedForm As Boolean = False
    Private hasLoadgrid As Boolean = False
    Private reportToView As ViewReport
    Private Enum ViewReport
        CPD
        PKPP
        DK
        CPRD
        SalesByTM
        CPMRT
        None
    End Enum
    Private Enum StatusProgress
        None
        Processing
        WaitingForAnotherProcess
    End Enum
    Private Function mReportView() As ViewReport
        If Me.btnCPD.Checked Then
            Return ViewReport.CPD
        ElseIf Me.btnCPRD.Checked Then
            Return ViewReport.CPRD
        ElseIf Me.btnDK.Checked Then
            Return ViewReport.DK
        ElseIf Me.btnPKPP.Checked Then
            Return ViewReport.PKPP
        ElseIf Me.btnSalesbyPO.Checked Then
            Return ViewReport.SalesByTM
        ElseIf Me.btnCPMRT.Checked Then
            Return ViewReport.CPMRT
        End If
    End Function
    Private Sub Bidgrid()
        Me.hasLoadgrid = False
        If IsNothing(Me.DS) Then
            Me.GridEX1.SetDataBinding(Nothing, "")
            Me.GridEX2.SetDataBinding(Nothing, "")
        End If

        If Not IsNothing(Me.DS.Tables(0)) Then
            If Not IsNothing(Me.GridEX1.DataSource) Then
                Me.GridEX1.RootTable.Columns.Clear()
            End If
            Me.GridEX1.SetDataBinding(Me.DS.Tables(0).DefaultView, "")
            If Not Me.hasLoadgrid Then
                Me.GridEX1.RetrieveStructure()
            ElseIf Me.reportToView <> Me.mReportView Then
                Me.GridEX1.RetrieveStructure()
            End If

        End If
        If Not IsNothing(Me.DS.Tables(1)) Then
            If Not IsNothing(Me.GridEX2.DataSource) Then
                Me.GridEX2.RootTable.Columns.Clear()
            End If
            Me.GridEX2.SetDataBinding(Me.DS.Tables(1).DefaultView, "")
            If Not Me.hasLoadgrid Then
                Me.GridEX2.RetrieveStructure()
            End If
        End If
        If Not Me.hasLoadgrid Then
            Me.FormatGrid(Me.GridEX1)
            Me.FormatGrid(Me.GridEX2)
        ElseIf Me.reportToView <> Me.mReportView Then
            Me.FormatGrid(Me.GridEX1)
        End If
        Me.hasLoadgrid = True
    End Sub

    Private Sub FormatGrid(ByVal grid As Janus.Windows.GridEX.GridEX)
        If grid.Name = "GridEX1" Then
            If Not IsNothing(Me.GridEX1.DataSource) Then
                If Me.GridEX1.RecordCount > 0 Then
                    For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
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
                                col.TotalFormatString = "#,##0.000"
                                If col.DataMember = "TOTAL_SALES_VALUE" Or col.DataMember = "TOTAL" Or col.DataMember = "PRICE" Then
                                    col.FormatString = "#,##0.00"
                                    col.TotalFormatString = "#,##0.00"
                                End If
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
                    Me.GridEX1.AutoSizeColumns()
                End If
            End If
        Else
            If Not IsNothing(Me.GridEX2.DataSource) Then
                If Me.GridEX2.RecordCount > 0 Then
                    For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX2.RootTable.Columns
                        If col.DataMember = "PROG_BRANDPACK_DIST_ID" Then
                            col.Visible = False
                            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                            col.ColumnType = Janus.Windows.GridEX.ColumnType.Text
                            col.Key = "ID"
                            col.Caption = "ID"
                        End If
                        If col.Type Is Type.GetType("System.Decimal") Or col.Type Is Type.GetType("System.Int32") Then
                            col.FormatString = "#,##0.000"
                            col.TotalFormatString = "#,##0.000"
                            col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                            col.ColumnType = Janus.Windows.GridEX.ColumnType.Text
                            If col.DataMember = "TOTAL_SALES_VALUE" Or col.DataMember = "TOTAL" Or col.DataMember = "PRICE" Then
                                col.FormatString = "#,##0.00"
                                col.TotalFormatString = "#,##0.00"
                            End If
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
                    Me.GridEX2.AutoSizeColumns()
                    Dim ProgType As String = ""
                    If Me.btnCPD.Checked Then : ProgType = "CPD"
                    ElseIf Me.btnCPRD.Checked Then : ProgType = "CPRD"
                    ElseIf Me.btnDK.Checked Then : ProgType = "DK"
                    ElseIf Me.btnPKPP.Checked Then : ProgType = "PKPP"
                    ElseIf Me.btnSalesbyPO.Checked Then : ProgType = "SALES BY TERRITORY"
                    ElseIf Me.btnCPMRT.Checked Then : ProgType = "CP(RMT)"
                    End If
                    Me.GridEX1.RootTable.Caption = "SUMMARY OF " & ProgType & ", Periode : " & String.Format("{0:dd MMMM yyyy}", Me.dtPicfrom.Value) & " - " & String.Format("{0:dd MMMM yyyy}", Me.dtPicUntil.Value)
                    Me.GridEX1.RootTable.HeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
                End If
            End If
        End If
        Me.GridEX2.RootTable.HeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
    End Sub
    Private Sub BindChildGrid(ByVal DV As DataView)
        Me.hasLoadgrid = False
        Me.GridEX2.SetDataBinding(DV, "")
        If Not Me.hasLoadgrid Then
            Me.GridEX2.RetrieveStructure()
        End If
        Me.FormatGrid(Me.GridEX2)
        Me.hasLoadgrid = True
    End Sub
    Private Sub ShowProceed()
        LD = New Loading
        LD.Show() : LD.TopMost = True
        Application.DoEvents()
        Dim BFind As Boolean = False
        While Not Me.StatProg = StatusProgress.None
            Me.LD.Label1.Text = "Processing procedure...."
            Thread.Sleep(50)
            LD.Refresh()
        End While
        Thread.Sleep(50)
        LD.Close() : LD = Nothing
    End Sub
    Private Sub getData()
        Me.StatProg = StatusProgress.Processing
        Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
        Me.ThreadProgress.Start()
        Dim Flag As String = ""
        If Me.btnCPD.Checked Then
            Flag = "CPD"
        ElseIf Me.btnPKPP.Checked Then
            Flag = "PKPP"
        ElseIf Me.btnDK.Checked Then
            Flag = "DK"
        ElseIf btnCPRD.Checked Then
            Flag = "CPRD"
        ElseIf btnSalesbyPO.Checked Then
            Flag = "SLSTM"
        ElseIf Me.btnCPMRT.Checked Then
            Flag = "CPRMT"
            'ElseIf Me.btnDKN.Checked Then
            '    Flag = "DN"
        End If
        Dim EndDate As Object = Nothing
        If Me.dtPicUntil.Text <> "" Then
            EndDate = Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())
        End If
        If Flag <> "" Then
            Me.DS = Me.clsProgram.getSalesHistory(Flag, Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), EndDate)
        End If
        Me.isLoadingRow = True : Me.Bidgrid() : Me.GridEX1_Enter(Me.GridEX1, New EventArgs())
        Me.isLoadingRow = False
        'check apakah chek filter di check/gak
        If Me.chkFilter.Checked Then
            Me.GridEX1.MoveFirst()
        End If
        Me.StatProg = StatusProgress.None : Me.Cursor = Cursors.Default
    End Sub
    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If (TypeOf (sender) Is DevComponents.DotNetBar.LabelItem) Then
                Return
            End If
            Dim item As DevComponents.DotNetBar.ButtonItem = CType(sender, DevComponents.DotNetBar.ButtonItem)

            For Each ctl As DevComponents.DotNetBar.BaseItem In Me.ItemPanel1.Items
                If Not (ctl Is item) Then
                    If (TypeOf (ctl) Is DevComponents.DotNetBar.ButtonItem) Then
                        CType(ctl, DevComponents.DotNetBar.ButtonItem).Checked = False
                    End If
                End If
            Next
            item.Checked = Not item.Checked
            If Me.btnCPD.Checked = False And Me.btnDK.Checked = False And Me.btnPKPP.Checked = False And Me.btnCPRD.Checked = False And btnSalesbyPO.Checked = False And Me.btnCPMRT.Checked = False Then
                MessageBox.Show("Please chek one of the menu(s) to show data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.GridEX1.SetDataBinding(Nothing, "")
                Me.GridEX2.SetDataBinding(Nothing, "")
                Me.Cursor = Cursors.Default : Return
            End If
            Me.getData()
            'clear data
            If item.Checked Then
                Select Case item.Name
                    Case "btnCPD"
                        Me.reportToView = ViewReport.CPD
                    Case "btnCPRD"
                        Me.reportToView = ViewReport.CPRD
                    Case "btnDK"
                        Me.reportToView = ViewReport.DK
                    Case "btnPKPP"
                        Me.reportToView = ViewReport.PKPP
                    Case "btnSalesbyPO"
                        Me.reportToView = ViewReport.SalesByTM
                    Case "btnCPMRT"
                        Me.reportToView = ViewReport.CPMRT
                End Select
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default : Me.StatProg = StatusProgress.None : Me.isLoadingRow = False : Me.hasLoadgrid = False : MessageBox.Show(ex.Message)
        End Try

    End Sub
    Friend Sub RefreshData()
        Me.btnApplyRange_Click(Me.btnApplyRange, New EventArgs())
    End Sub
    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.btnCPD.Checked = False And Me.btnDK.Checked = False And Me.btnPKPP.Checked = False And Me.btnCPRD.Checked = False And Me.btnSalesbyPO.Checked = False And Me.btnCPMRT.Checked = False Then
                MessageBox.Show("Please chek one of the menu(s) to show data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Cursor = Cursors.Default : Return
            End If
            Me.getData()
        Catch ex As Exception
            Me.Cursor = Cursors.Default : Me.StatProg = StatusProgress.None : Me.isLoadingRow = False : Me.hasLoadgrid = False : MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dtPicUntil_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicUntil.KeyDown
        Me.dtPicUntil.Text = ""
    End Sub

    Private Sub SalesProgramReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.clsProgram = New NufarmBussinesRules.Program.BrandPackInclude()
            Me.dtPicfrom.Value = DateTime.Now.AddMonths(-3)
            Me.dtPicUntil.Value = DateTime.Now
            frmParent.m_Grid = Me.GridEX1
            Me.GridEX1_Enter(Me.GridEX1, New EventArgs())
            Me.reportToView = ViewReport.None
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.HasLoadedForm = True
        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        If Me.isLoadingRow Then : Return : End If
        If Not Me.HasLoadedForm Then
            Return
        End If

        Me.Cursor = Cursors.WaitCursor

        If Not IsNothing(Me.GridEX2.DataSource) Then
            Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
            Dim RowFilter As String = ""

            Try
                If Me.chkFilter.Checked Then
                    If Me.GridEX1.RecordCount > 0 Then
                        If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                            If Me.btnSalesbyPO.Checked Then
                                RowFilter = "TERRITORY_MANAGER = '" & Me.GridEX1.GetValue("TERRITORY_MANAGER").ToString() & "' AND BRANDPACK_NAME = '" & Me.GridEX1.GetValue("BRANDPACK_NAME").ToString() & "'"
                            Else
                                RowFilter = "PROG_BRANDPACK_DIST_ID = '" & Me.GridEX1.GetValue("ID").ToString() & "'"
                            End If
                            DV.RowFilter = RowFilter
                        Else
                            If DV.RowFilter <> "" Then
                                DV.RowFilter = ""
                            End If
                        End If
                    Else
                        If DV.RowFilter <> "" Then
                            DV.RowFilter = ""
                        End If
                    End If

                Else
                    If DV.RowFilter <> "" Then
                        DV.RowFilter = ""
                    End If
                End If
                'Me.BindChildGrid(DV)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End If

    End Sub

    Private Sub GridEX1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.Enter
        If Not IsNothing(Me.frmParent) Then
            frmParent.m_Grid = Me.GridEX1
        End If
        Me.GridEX1.BorderStyle = Janus.Windows.GridEX.BorderStyle.SunkenLight3D
        Me.GridEX2.BorderStyle = Janus.Windows.GridEX.BorderStyle.None

        Me.GridEX1.BackColor = Color.FromArgb(158, 190, 245)
        Me.GridEX1.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.GridEX1.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.GridEX1.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.GridEX1.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        'ME.GridEX1.SelectedFormatStyle.ForeColor = SYSTEM.Drawing.Color.
        Me.GridEX2.BackColor = Color.FromArgb(194, 217, 247)
        Me.GridEX2.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
        Me.GridEX2.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
    End Sub


    Private Sub GridEX2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX2.Enter
        If Not IsNothing(Me.frmParent) Then
            frmParent.m_Grid = Me.GridEX2
        End If
        Me.GridEX2.BorderStyle = Janus.Windows.GridEX.BorderStyle.SunkenLight3D
        Me.GridEX1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None

        Me.GridEX2.BackColor = Color.FromArgb(158, 190, 245)
        Me.GridEX2.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.GridEX2.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        Me.GridEX2.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.GridEX2.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        'ME.GridEX1.SelectedFormatStyle.ForeColor = SYSTEM.Drawing.Color.
        Me.GridEX1.BackColor = Color.FromArgb(194, 217, 247)
        Me.GridEX1.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
        Me.GridEX1.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
    End Sub

    Private Sub chkFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilter.CheckedChanged
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.chkFilter.Checked Then
                If Not IsNothing(Me.GridEX1.DataSource) Then
                    If Me.GridEX1.RecordCount > 0 Then
                        Try
                            Me.Cursor = Cursors.WaitCursor
                            Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                                DV.RowFilter = "PROG_BRANDPACK_DIST_ID = '" & Me.GridEX1.GetValue("ID").ToString() & "'"
                            Else
                                If DV.RowFilter <> "" Then
                                    DV.RowFilter = ""
                                End If
                            End If
                            Me.BindChildGrid(DV)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        Finally
                            Me.Cursor = Cursors.Default
                        End Try
                    End If
                End If
            Else
                If Not IsNothing(Me.GridEX2.DataSource) Then
                    Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                    If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                        DV.RowFilter = "PROG_BRANDPACK_DIST_ID = '" & Me.GridEX1.GetValue("ID").ToString() & "'"
                    Else
                        If DV.RowFilter <> "" Then
                            DV.RowFilter = ""
                        End If
                    End If
                    Me.BindChildGrid(DV)
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default : MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
