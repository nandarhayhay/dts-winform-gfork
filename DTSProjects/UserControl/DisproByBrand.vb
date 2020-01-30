Imports System.Threading
Public Class DisproByBrand
    Private m_DistReport As NufarmBussinesRules.DistributorReport.Dist_Report
    Private Delegate Sub onShowingProgress(ByVal message As String)
    Private Event ShowProgres As onShowingProgress
    Private Delegate Sub onClossingProgress()
    Private Event CloseProgress As onClossingProgress
    Private tickCount As Integer = 0 : Private WithEvents ST As DTSProjects.Progress
    Dim rnd As Random : Private ResultRandom As Integer
    Private HasLoadReport As Boolean = True : Private IsFailedToLoad As Boolean = False
    Private IsFirstLoadReport As Boolean
    Private clsAccrue As New NufarmBussinesRules.DistributorAgreement.Target_Agreement
    Private ThreadProgress As Thread = Nothing : Private StatProg As StatusProgress = StatusProgress.None
    Private LD As Loading : Private DV As DataView = Nothing
    Private Enum StatusProgress
        None
        Processing
    End Enum
    Private IsFailLoadReport As Boolean = False
    Private Sub ShowProceed()
        Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Me.LD.Refresh() : Thread.Sleep(100) : Application.DoEvents()
        End While
        Thread.Sleep(100) : Me.LD.Close() : Me.LD = Nothing
        Me.tickCount = 0
    End Sub
    Private Sub closeProgres() Handles Me.CloseProgress
        Me.Timer1.Enabled = False
        Me.Timer1.Stop()
        If Not IsNothing(Me.ST) Then
            Me.ST.Close()
            Me.ST = Nothing
            Me.tickCount = 0
        End If
    End Sub

    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.tickCount >= Me.ResultRandom Then
            If Me.HasLoadReport = True Then
                If Me.IsFirstLoadReport = False Then
                    If Not Me.IsFailedToLoad Then
                        Me.GridEX1.SetDataBinding(Me.clsDistReport.ViewPODisPro(), "")
                        Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
                    Else
                        Me.GridEX1.SetDataBinding(Nothing, "")
                    End If
                End If
                RaiseEvent CloseProgress()
                Me.IsFirstLoadReport = False
            Else
                Me.ResultRandom += 1
            End If
        End If
    End Sub

    Private Overloads Sub ShowProgress(ByVal Message As String) Handles Me.ShowProgres
        Me.ST = New Progress
        Application.DoEvents()
        Me.ST.Show(Message)
    End Sub
    Private Property clsDistReport() As NufarmBussinesRules.DistributorReport.Dist_Report
        Get
            If IsNothing(Me.m_DistReport) Then
                Me.m_DistReport = New NufarmBussinesRules.DistributorReport.Dist_Report()
            End If
            Return Me.m_DistReport
        End Get
        Set(ByVal value As NufarmBussinesRules.DistributorReport.Dist_Report)
            Me.m_DistReport = value
        End Set
    End Property

    Friend Sub RefreshData()
        Me.btnApplyRange_Click(Me.btnApplyRange, New EventArgs())
    End Sub

    Private Sub Distributor_PO_Dispro_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsDistReport) Then
                Me.clsDistReport.Dispose(False)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Distributor_PO_Dispro_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate()
            Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate()
            'Me.clsDistReport.CreateViewDistributor_1()
            'Me.mcbDistributor.SetDataBinding(Me.clsDistReport.ViewDistributor(), "")
            'AddHandler Timer1.Tick, AddressOf ChekTimer
            'Me.IsFirstLoadReport = True
            'btnApplyRange_Click(Me.btnApplyRange, New EventArgs())
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.HasLoadReport = False : Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start() : Me.Timer1.Enabled = True : Me.Timer1.Start()
            'RaiseEvent ShowProgres("Executing Query batched.......")
            'Me.rnd = New Random()
            'Me.ResultRandom = Me.rnd.Next(1, 5)
            'Me.Timer1.Enabled = True
            'Me.Timer1.Start()
            'Me.GridEX1.SetDataBinding(Nothing, "")
            'Me.GridEX1.Update()
            'If (Me.mcbDistributor.Value Is Nothing) Or (Me.mcbDistributor.SelectedItem Is Nothing) Then
            '    Me.clsDistReport.Create_View_ReportPODispro(CDate(Me.dtPicfrom.Value.ToShortDateString()), CDate(Me.dtPicUntil.Value.ToShortDateString()))
            'Else
            '    Me.clsDistReport.Create_View_ReportPODispro(CDate(Me.dtPicfrom.Value.ToShortDateString()), CDate(Me.dtPicUntil.Value.ToShortDateString()), Me.mcbDistributor.Value.ToString())
            'End If
            Me.DV = Me.clsDistReport.Create_View_PODisproByBrand(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            'If Me.IsFirstLoadReport = True Then
            '    Me.GridEX1.SetDataBinding(Me.clsDistReport.ViewPODisPro(), "")
            '    'Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").AutoSize()
            '    Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
            'End If
            Me.HasLoadReport = True
        Catch ex As Exception
            Me.IsFailedToLoad = True : Me.StatProg = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False
            'RaiseEvent CloseProgress()
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.IsFailedToLoad Then
            Me.StatProg = StatusProgress.None : Me.GridEX1.SetDataBinding(Nothing, "")
            Me.Timer1.Stop() : Me.Timer1.Enabled = False : Me.IsFailedToLoad = False : Return
        End If
        Me.tickCount += 1
        If Me.tickCount >= 2 Then
            If Me.HasLoadReport Then
                Me.GridEX1.SetDataBinding(Me.DV, "")
                Me.StatProg = StatusProgress.None
                Me.Timer1.Stop() : Me.Timer1.Enabled = False
            End If
        End If
    End Sub
End Class
