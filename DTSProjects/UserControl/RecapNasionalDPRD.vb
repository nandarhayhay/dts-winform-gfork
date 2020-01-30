Imports System.Threading
Public Class RecapNasionalDPRD
    Private LD As Loading = Nothing
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private _clsAchDPRD As NufarmBussinesRules.Kios.ReachingTarget
    Private Enum StatusProgress
        None
        Processing
    End Enum

    Private Sub ShowProceed()
        Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Me.LD.Refresh() : Thread.Sleep(100) : Application.DoEvents()
        End While
        Thread.Sleep(100) : Me.LD.Close() : Me.LD = Nothing
    End Sub
    Private Sub setGroupReport()
        If Not IsNothing(Me.GridEX1.DataSource) Then
            Dim grGroupTerritorial As New Janus.Windows.GridEX.GridEXGroup(Me.GridEX1.RootTable.Columns("TERRITORY_AREA"))
            Dim grGroupregional As New Janus.Windows.GridEX.GridEXGroup(Me.GridEX1.RootTable.Columns("REGIONAL_AREA"))
            Dim grGroupByBrand As New Janus.Windows.GridEX.GridEXGroup(Me.GridEX1.RootTable.Columns("BRAND_NAME"))
            If Me.btnByRegional.Checked Then
                Me.GridEX1.RootTable.Groups.Add(grGroupByBrand)
                Me.GridEX1.RootTable.Columns("BRAND_NAME").Visible = False
                Me.GridEX1.RootTable.Columns("BRAND_NAME").Group.GroupPrefix = ""
                Me.GridEX1.RootTable.Groups.Add(grGroupregional)
                Me.GridEX1.RootTable.Columns("REGIONAL_AREA").Visible = False
                Me.GridEX1.RootTable.Columns("REGIONAL_AREA").Group.GroupPrefix = ""
                Me.GridEX1.RootTable.Columns("TERRITORY_AREA").Visible = True
            ElseIf Me.btnByTerritorial.Checked Then
                'Me.GridEX1.RootTable.Groups.Add(grGroupTerritorial)
                Me.GridEX1.RootTable.Columns("BRAND_NAME").Visible = True
                Me.GridEX1.RootTable.Columns("BRAND_NAME").DefaultGroupPrefix = ""
                Me.GridEX1.RootTable.Groups.Add(grGroupTerritorial)
                Me.GridEX1.RootTable.Columns("TERRITORY_AREA").Visible = False
                Me.GridEX1.RootTable.Columns("TERRITORY_AREA").Group.GroupPrefix = ""
                Me.GridEX1.RootTable.Columns("REGIONAL_AREA").Visible = False

            ElseIf Me.btnSItemByNational.Checked Then
                Me.GridEX1.RootTable.Groups.Add(grGroupByBrand)
                Me.GridEX1.RootTable.Columns("BRAND_NAME").Visible = False
                Me.GridEX1.RootTable.Columns("BRAND_NAME").Group.GroupPrefix = ""
                Me.GridEX1.RootTable.Columns("REGIONAL_AREA").Visible = True
                Me.GridEX1.RootTable.Columns("TERRITORY_AREA").Visible = True

            End If
        End If
        Me.GridEX1.AutoSizeColumns()
    End Sub
    Private Sub FormatDataGrid()
        If Not IsNothing(Me.GridEX1.DataSource) Then
            If Me.btnSDetailReportView.Checked Then
                Me.GridEX1.GroupByBoxVisible = False
                Me.GridEX1.RootTable.Columns("PROGRAM_ID").Visible = False
                Me.GridEX1.RootTable.Columns("BRAND_ID").Visible = False
                Me.GridEX1.RootTable.Columns("TERRITORY_ID").Visible = False
                'ElseIf btnSD.Checked Then
                'Me.GridEX1.RootTable.Columns("BUDGET_TERRITORY").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GridEX1.RootTable.Columns("BUDGET_TERRITORY").FormatString = "#,##0.000"
                Me.GridEX1.RootTable.Columns("BUDGET_TERRITORY").TotalFormatString = "#,##0.000"
                Me.GridEX1.RootTable.Columns("BUDGET_TERRITORY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                If Me.btnSitemDPRDM.Checked Then
                    Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").FormatString = "#,##0.00"
                    Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").TotalFormatString = "#,##0.00"

                    Me.GridEX1.RootTable.Columns("TOTAL_BONUS").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.GridEX1.RootTable.Columns("TOTAL_BONUS").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.GridEX1.RootTable.Columns("TOTAL_BONUS").FormatString = "#,##0.00"
                    Me.GridEX1.RootTable.Columns("TOTAL_BONUS").TotalFormatString = "#,##0.00"
                Else
                    Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").FormatString = "p"
                    Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").Visible = False
                    Me.GridEX1.RootTable.Columns("TOTAL_DISC").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.GridEX1.RootTable.Columns("TOTAL_DISC").FormatString = "#,##0.000"
                    Me.GridEX1.RootTable.Columns("TOTAL_DISC").TotalFormatString = "#,##0.000"
                End If
                Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").Visible = False
            Else
                Me.GridEX1.RootTable.Columns("TOTAL_BUDGET").FormatString = "#,##0.000"
                Me.GridEX1.RootTable.Columns("TOTAL_BUDGET").TotalFormatString = "#,##0.000"
                Me.GridEX1.RootTable.Columns("TOTAL_BUDGET").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns("TOTAl_BUDGET").Caption = "TOTAL_TARGET"
                'Me.GridEX1.RootTable.Columns("% ACHIEVEMENT").FormatString = "p"
                'Me.GridEX1.RootTable.Columns("% ACHIEVEMENT").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                If Me.btnSitemDPRDM.Checked Then
                    Me.GridEX1.RootTable.Columns("TOTAL_BONUS").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.GridEX1.RootTable.Columns("TOTAL_BONUS").FormatString = "#,##0.00"
                    Me.GridEX1.RootTable.Columns("TOTAL_BONUS").TotalFormatString = "#,##0.00"
                    Me.GridEX1.RootTable.Columns("TOTAL_BONUS").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Else
                    'Me.GridEX1.RootTable.Columns("TOTAL_DISC").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").FormatString = "p"
                    Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").Visible = False
                    Me.GridEX1.RootTable.Columns("TOTAL_DISC").FormatString = "#,##0.000"
                    Me.GridEX1.RootTable.Columns("TOTAL_DISC").TotalFormatString = "#,##0.000"
                    Me.GridEX1.RootTable.Columns("TOTAL_DISC").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                End If
                'Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").FormatString = "#,##0.00"
                'Me.GridEX1.RootTable.Columns("BUDGET_DISPRO").TotalFormatString = "#,##0.00"
            End If
            Me.GridEX1.RootTable.Columns("% ACHIEVEMENT").FormatString = "p"
            Me.GridEX1.RootTable.Columns("% ACHIEVEMENT").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("% COVERAGE").FormatString = "p"
            Me.GridEX1.RootTable.Columns("% COVERAGE").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            If btnSitemDPRDM.Checked Then
                Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            End If
            Me.GridEX1.RootTable.Columns("TOTAL_SPKIOS").FormatString = "#,##0.000"
            Me.GridEX1.RootTable.Columns("TOTAL_SPKIOS").TotalFormatString = "#,##0.000"
            Me.GridEX1.RootTable.Columns("TOTAL_SPKIOS").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.GridEX1.RootTable.Columns("TOTAL_ACTUAL").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("TOTAL_ACTUAL").FormatString = "#,##0.000"
            Me.GridEX1.RootTable.Columns("TOTAL_ACTUAL").TotalFormatString = "#,##0.000"
            Me.GridEX1.RootTable.Columns("TOTAL_ACTUAL").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            For Each COL As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If COL.Type Is Type.GetType("System.String") Or COL.Type Is Type.GetType("System.Decimal") Then
                    COL.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                ElseIf COL.Type Is Type.GetType("System.DateTime") Then
                    COL.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
                End If
            Next
        End If

    End Sub
    Friend Sub RefreshData()
        Me.btnApplyRange_Click(Me.btnApplyRange, New EventArgs())
    End Sub

    Private ReadOnly Property clsAchDPRD() As NufarmBussinesRules.Kios.ReachingTarget
        Get
            If IsNothing(Me._clsAchDPRD) Then
                Me._clsAchDPRD = New NufarmBussinesRules.Kios.ReachingTarget()
            End If
            Return Me._clsAchDPRD
        End Get
    End Property

    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If (Me.btnSitemDPRDM.Checked = False And Me.btnSItemDPRDS.Checked = False) Then : Return : End If
            Dim BFind As Boolean = False
            Dim ButItem As DevComponents.DotNetBar.ButtonItem = Nothing
            If Me.btnSDetailReportView.Checked = False And Me.btnSSummaryReportView.Checked = False Then : Return : End If
            If Me.btnSDetailReportView.Checked Then
                If Me.btnByRegional.Checked = False And Me.btnByTerritorial.Checked = False And Me.btnSItemByNational.Checked = False Then : Return : End If
            End If
            Dim reportType As String = ""
            If Me.btnSitemDPRDM.Checked Then : reportType = "DPRDM"
            ElseIf Me.btnSItemDPRDS.Checked Then : reportType = "DPRDS"
            End If
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            Dim DV As DataView = Nothing
            If Me.btnSDetailReportView.Checked Then
                'get report by detail
                DV = Me.clsAchDPRD.getRecapDPRD(reportType, Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            ElseIf Me.btnSSummaryReportView.Checked Then
                DV = Me.clsAchDPRD.getRecapSummaryDPRD(reportType, Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            End If
            Me.GridEX1.DataSource = DV : Me.GridEX1.RetrieveStructure()
            Me.FormatDataGrid() : Me.setGroupReport()
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub RecapNasionalDPRD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate
        Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
    End Sub

    Private Sub ExplorerBar1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExplorerBar1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            If item.Name = "grReportType" Or item.Name = "grdDetailReportBy" Or item.Name = "grDPRDType" Then : Return : End If
            Dim btnItem As DevComponents.DotNetBar.ButtonItem = CType(item, DevComponents.DotNetBar.ButtonItem)
            Select Case item.Name
                Case "btnSitemDPRDM"
                    btnSItemDPRDS.Checked = False
                    btnItem.Checked = Not btnItem.Checked
                Case "btnSItemDPRDS"
                    btnSitemDPRDM.Checked = False
                    btnItem.Checked = Not btnItem.Checked
                Case "btnSItemByNational"
                    If btnSSummaryReportView.Checked Then
                        btnSItemByNational.Checked = False : Return
                    End If
                    btnByRegional.Checked = False
                    btnByTerritorial.Checked = False
                    btnItem.Checked = Not btnItem.Checked
                Case "btnByRegional"
                    If btnSSummaryReportView.Checked Then
                        btnByRegional.Checked = False : Return
                    End If
                    btnSItemByNational.Checked = False
                    btnByTerritorial.Checked = False
                    btnItem.Checked = Not btnItem.Checked
                Case "btnByTerritorial"
                    If btnSSummaryReportView.Checked Then
                        btnByTerritorial.Checked = False : Return
                    End If
                    btnSItemByNational.Checked = False
                    btnByRegional.Checked = False
                    btnItem.Checked = Not btnItem.Checked
                Case "btnSSummaryReportView"
                    btnSDetailReportView.Checked = False
                    btnItem.Checked = Not btnItem.Checked
                    Me.grdDetailReportBy.Enabled = False
                    Me.btnSItemByNational.Checked = False
                    Me.btnByRegional.Checked = False
                    Me.btnByTerritorial.Checked = False
                Case "btnSDetailReportView"
                    Me.GridEX1.DataSource = Nothing : Me.GridEX1.RetrieveStructure()
                    btnSSummaryReportView.Checked = False
                    btnItem.Checked = Not btnItem.Checked
                    Me.grdDetailReportBy.Enabled = True
                    Me.grdDetailReportBy.Expanded = True
                    Me.grdDetailReportBy.Refresh()
            End Select
            Me.btnApplyRange_Click(Me.btnApplyRange, New EventArgs())

            'If (item.Name = "btnByRegional" Or item.Name = "btnByTerritorial" Or item.Name = "btnSItemByNational") Then
            '    If btnItem.Checked Then
            '        If btnSDetailReportView.Checked Then
            '            If Not IsNothing(Me.GridEX1.DataSource) Then
            '                Me.setGroupReport() : Return
            '            End If
            '        End If
            '    End If
            'Else

            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
