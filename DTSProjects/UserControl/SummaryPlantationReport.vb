Imports System.Threading
Public Class SummaryPlantationReport
    Private LD As Loading
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Friend CMain As Main = Nothing
    Private originalStartDate As String = ""
    Private originalEndDate As String = ""
    Private HasLoadForm As Boolean = False
    Private clsPlantation As New NufarmBussinesRules.Plantation.Plantation()
    Private isLoadingRow As Boolean = False
    Public frmParent As ReportGrid = Nothing
    Private OtherUserProcessing As String = ""
    Private isAwaiting As Boolean = False
    Private MustReload As Boolean = False
    Private Enum StatusProgress
        None
        Processing
        WaitingForAnotherProcess
    End Enum
    Private Sub ShowProceed()
        LD = New Loading
        LD.Show() : LD.TopMost = True
        Application.DoEvents()
        Dim BFind As Boolean = False
        BFind = Me.clsPlantation.hasReservedInvoice(Me.OtherUserProcessing)
        While Not Me.StatProg = StatusProgress.None
            If BFind Then
                If Me.OtherUserProcessing <> NufarmBussinesRules.User.UserLogin.UserName Then
                    While BFind
                        Thread.Sleep(1000)
                        Me.LD.Label1.Text = "Waiting for " + Me.OtherUserProcessing + " to finish processing procedure"
                        Application.DoEvents()
                        Me.LD.Refresh()
                        BFind = Me.clsPlantation.hasReservedInvoice(Me.OtherUserProcessing)
                        If BFind Then
                            If Me.OtherUserProcessing <> NufarmBussinesRules.User.UserLogin.UserName Then
                                isAwaiting = True
                            Else
                                isAwaiting = False
                                BFind = False
                            End If
                        End If
                    End While
                Else
                    isAwaiting = False
                End If
            Else
                isAwaiting = False
            End If

            'If Not BFind Then
            'Me.SP = StatusProgress.ProcessingAcrrue
            Me.LD.Label1.Text = "Processing procedure...."
            Thread.Sleep(50)
            LD.Refresh()
        End While
        Thread.Sleep(50)
        LD.Close() : LD = Nothing
    End Sub
    Friend Sub refreshdata()
        Me.MustReload = True
        Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
    End Sub

    Private Sub btnFilteDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilteDate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            
            Me.isAwaiting = True
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed) : ThreadProgress.Start()
            'hidupkan timer
            Me.Timer1.Enabled = True : Me.Timer1.Start()
        Catch ex As Exception
            Me.Cursor = Cursors.Default : Me.OtherUserProcessing = "" : Me.StatProg = StatusProgress.None : Me.Timer1.Interval = 1000 : MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.isLoadingRow = False
        End Try
    End Sub

    Private Sub SummaryPlantationReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
            Me.dtpicFrom.Value = NufarmBussinesRules.SharedClass.ServerDate.AddMonths(-2)
            Me.ExpandableSplitter1.Expandable = False
            If Not IsNothing(Me.frmParent) Then
                frmParent.m_Grid = Me.GridEX1
            End If
            Me.GridEX1_Enter(Me.GridEX1, New EventArgs())
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default : Me.HasLoadForm = True
        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            If Me.isLoadingRow Then : Return : End If
            If Not Me.HasLoadForm Then : Return : End If
            If Me.GridEX1.DataSource Is Nothing Then : Return : End If
            If Me.GridEX1.RecordCount <= 0 Then : Return : End If
            If Me.GridEX1.SelectedItems.Count <= 0 Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If Me.chkFilter.Checked Then
                    If Not IsNothing(Me.GridEX2.DataSource) Then
                        Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                        Dim PlantationID As String = Me.GridEX1.GetValue("PLANTATION_ID").ToString()
                        Dim BrandPackID As String = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
                        DV.RowFilter = "PLANTATION_ID = '" & PlantationID & "' AND BRANDPACK_ID = '" & BrandPackID & "'"
                        Me.GridEX2.SetDataBinding(DV, "")
                    End If
                Else
                    If Not IsNothing(Me.GridEX2.DataSource) Then
                        Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                        If DV.RowFilter <> "" Then
                            DV.RowFilter = ""
                            Me.GridEX2.SetDataBinding(DV, "")
                        End If
                    End If
                End If
            Else
                If Not IsNothing(Me.GridEX2.DataSource) Then
                    Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                    DV.RowFilter = ""
                    Me.GridEX2.SetDataBinding(DV, "")
                End If
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilter.CheckedChanged
        Try
            If Me.chkFilter.Checked Then
                If Not IsNothing(Me.GridEX1.DataSource) Then
                    If Me.GridEX1.RecordCount > 0 Then
                        If Not IsNothing(Me.GridEX2.DataSource) Then
                            Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
                        End If
                    End If
                End If
            Else
                If Not IsNothing(Me.GridEX2.DataSource) Then
                    Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView) : DV.RowFilter = ""
                    Me.GridEX2.SetDataBinding(DV, "")
                End If
            End If
        Catch ex As Exception

        End Try
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

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Cursor = Cursors.WaitCursor
        If Me.isAwaiting Then
            Return
        End If
        If Me.StatProg = StatusProgress.Processing Then
            Try
                Me.Timer1.Stop() : Me.Timer1.Enabled = False
                Me.Timer1.Interval = 1000
                'Me.StatProg = StatusProgress.Processing
                Me.OtherUserProcessing = ""
                Me.isLoadingRow = True
                Dim IsChangedOriginalDate = False
                If Me.originalStartDate <> NufarmBussinesRules.common.CommonClass.getNumericFromDate(Convert.ToDateTime(Me.dtpicFrom.Value.ToShortDateString())) Then
                    IsChangedOriginalDate = True
                ElseIf Me.originalEndDate <> NufarmBussinesRules.common.CommonClass.getNumericFromDate(Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())) Then
                    IsChangedOriginalDate = True
                ElseIf MustReload Then
                    IsChangedOriginalDate = True
                End If
                Dim DS As DataSet = Me.clsPlantation.getReportPlantation(Convert.ToDateTime(Me.dtpicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), IsChangedOriginalDate)
                Me.GridEX1.SetDataBinding(DS.Tables(0).DefaultView(), "")
                Me.GridEX2.SetDataBinding(DS.Tables(1).DefaultView(), "")
                Me.originalEndDate = NufarmBussinesRules.common.CommonClass.getNumericFromDate(Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
                Me.originalStartDate = NufarmBussinesRules.common.CommonClass.getNumericFromDate(Convert.ToDateTime(Me.dtpicFrom.Value.ToShortDateString()))
                Me.MustReload = False
                Me.StatProg = StatusProgress.None : Me.isLoadingRow = False
            Catch ex As Exception
                Me.MustReload = False : Me.isLoadingRow = False : Me.StatProg = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False
            End Try
            Me.Cursor = Cursors.Default
        End If
    End Sub
End Class
