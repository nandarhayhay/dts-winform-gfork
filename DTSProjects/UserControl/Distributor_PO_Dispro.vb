Imports System.Threading
Public Class Distributor_PO_Dispro
    Private m_DistReport As NufarmBussinesRules.DistributorReport.Dist_Report
    Private Delegate Sub onShowingProgress(ByVal message As String)
    Private Event ShowProgres As onShowingProgress
    Private Delegate Sub onClossingProgress()
    Private Event CloseProgress As onClossingProgress
    Private tickCount As Integer = 0
    Private WithEvents ST As DTSProjects.Progress
    Dim rnd As Random
    Private ResultRandom As Integer
    Private HasLoadReport As Boolean
    Private IsFailedToLoad As Boolean
    Private IsFirstLoadReport As Boolean
    Private LD As Loading = Nothing
    Private CounterTimer2 As Integer = 0
    Private StatProg As StatusProgress = StatusProgress.None
    Private ThreadProgress As Thread = Nothing
    Private Enum StatusProgress
        None
        Processing
    End Enum
    'Private Sub closeProgres() Handles Me.CloseProgress
    '    Me.Timer1.Enabled = False
    '    Me.Timer1.Stop()
    '    'If Not IsNothing(Me.ST) Then
    '    '    Me.ST.Close()
    '    '    Me.ST = Nothing
    '    '    Me.tickCount = 0
    '    'End If
    '    Me.tickCount = 0 : Me.Timer2.Enabled = False
    '    Me.CounterTimer2 = 0
    '    If Not IsNothing(Me.LD) Then : Me.LD.Close() : Me.LD = Nothing : End If
    'End Sub
    Private Sub ShowProceed()
        Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Me.LD.Refresh() : Thread.Sleep(100) : Application.DoEvents()
        End While
        Thread.Sleep(100) : Me.LD.Close() : Me.LD = Nothing
    End Sub
    'Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
    '    If Me.tickCount = Me.ResultRandom Then
    '        If Me.HasLoadReport = True Then
    '            If Me.IsFirstLoadReport = False Then
    '                If Not Me.IsFailedToLoad Then
    '                    Me.GridEX1.SetDataBinding(Me.clsDistReport.ViewPODisPro(), "")
    '                    Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").AutoSize()
    '                    Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
    '                Else
    '                    Me.GridEX1.SetDataBinding(Nothing, "")
    '                End If
    '            End If
    '            RaiseEvent CloseProgress()
    '            'Me.Timer1.Enabled = False
    '            'Me.Timer1.Stop()
    '            Me.IsFirstLoadReport = False
    '        Else
    '            Me.ResultRandom += 1
    '        End If
    '        'Me.FormatGrid()
    '    End If
    'End Sub
    'Private Sub ChekTimer2(ByVal sender As Object, ByVal e As EventArgs)
    '    If Me.CounterTimer2 > 1 Then
    '        Me.Timer2.Stop() : Me.Timer2.Enabled = False
    '        Me.CounterTimer2 = 0
    '        Me.getData()
    '        Me.HasLoadReport = True
    '    End If
    'End Sub
    'Private Overloads Sub ShowProgress(ByVal Message As String) Handles Me.ShowProgres
    '    Me.ST = New Progress
    '    Application.DoEvents()
    '    Me.ST.Show(Message)
    'End Sub
    Private Sub getData()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.GridEX1.SetDataBinding(Nothing, "")
            Me.GridEX1.Update()
            If (Me.mcbDistributor.Value Is Nothing) Or (Me.mcbDistributor.SelectedItem Is Nothing) Then
                Me.clsDistReport.Create_View_ReportPODispro(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            Else
                Me.clsDistReport.Create_View_ReportPODispro(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), Me.mcbDistributor.Value.ToString())
            End If
            Me.GridEX1.SetDataBinding(Me.clsDistReport.ViewPODisPro(), "")
            Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").AutoSize()
            Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
            'If Me.IsFirstLoadReport = True Then
            '    Me.GridEX1.SetDataBinding(Me.clsDistReport.ViewPODisPro(), "")
            '    Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").AutoSize()
            '    Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
            'End If
            Me.HasLoadReport = True : Me.IsFailedToLoad = False
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.IsFirstLoadReport = False : Me.HasLoadReport = True : Me.IsFailedToLoad = True : RaiseEvent CloseProgress() : Throw ex
        Finally
            Me.StatProg = StatusProgress.None : Me.Cursor = Cursors.Default
        End Try

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
            Dim DV As DataView = Me.clsDistReport.CreateViewDistributor()
            Me.mcbDistributor.SetDataBinding(DV, "")
            'AddHandler Timer1.Tick, AddressOf ChekTimer
            'AddHandler Timer2.Tick, AddressOf ChekTimer2
            'Addhandler Timer2.Tick,Addressof che
            Me.IsFirstLoadReport = True
            'btnApplyRange_Click(Me.btnApplyRange, New EventArgs())
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.HasLoadReport = False
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            Me.getData()
            'RaiseEvent ShowProgres("Executing Query batched.......")
            'Me.rnd = New Random()
            'Me.ResultRandom = Me.rnd.Next(1, 5)
            'Me.Timer1.Enabled = True : Me.Timer1.Start()
            'Me.Timer2.Enabled = True : Me.Timer2.Start()
            'Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
            'Application.DoEvents()
            'Me.GridEX1.SetDataBinding(Nothing, "")
            'Me.GridEX1.Update()
            'If (Me.mcbDistributor.Value Is Nothing) Or (Me.mcbDistributor.SelectedItem Is Nothing) Then
            '    Me.clsDistReport.Create_View_ReportPODispro(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            'Else
            '    Me.clsDistReport.Create_View_ReportPODispro(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), Me.mcbDistributor.Value.ToString())
            'End If
            'If Me.IsFirstLoadReport = True Then
            '    Me.GridEX1.SetDataBinding(Me.clsDistReport.ViewPODisPro(), "")
            '    Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").AutoSize()
            '    Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
            'End If
            Me.HasLoadReport = True
        Catch ex As Exception
            'RaiseEvent CloseProgress()
            Me.StatProg = StatusProgress.None
            Me.HasLoadReport = True : Me.IsFailedToLoad = True
            MessageBox.Show(ex.Message)
        Finally
            Me.StatProg = StatusProgress.None 'Me.HasLoadReport = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    Me.tickCount += 1
    'End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rowfilter As String = "DISTRIBUTOR_NAME LIKE '%" & Me.mcbDistributor.Text & "%'"
            CType(Me.mcbDistributor.DataSource, DataView).RowFilter = rowfilter
            Me.mcbDistributor.DropDownList().Refetch()
            Dim itemCount As Integer = Me.mcbDistributor.DropDownList().RecordCount()
            MessageBox.Show(itemCount.ToString() & " item(s) found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
    '    Me.CounterTimer2 += 1
    'End Sub
End Class
