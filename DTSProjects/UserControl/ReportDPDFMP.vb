Imports System.Threading
Public Class ReportDPDFMP
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
    Private Sub ShowProceed()
        Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Me.LD.Refresh() : Thread.Sleep(100) : Application.DoEvents()
        End While
        Thread.Sleep(100) : Me.LD.Close() : Me.LD = Nothing
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
    Private Sub getData()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim DV As DataView = Nothing
            If (Me.mcbDistributor.Value Is Nothing) Or (Me.mcbDistributor.SelectedItem Is Nothing) Then
                DV = Me.clsDistReport.GetDPDFMP(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            Else
                DV = Me.clsDistReport.GetDPDFMP(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), Me.mcbDistributor.Value.ToString())
            End If
            Me.GridEX1.SetDataBinding(DV, "")
            Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").AutoSize()
            Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
            Me.HasLoadReport = True : Me.IsFailedToLoad = False
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.IsFirstLoadReport = False : Me.HasLoadReport = True : Me.IsFailedToLoad = True : RaiseEvent CloseProgress() : Throw ex
        Finally
            Me.StatProg = StatusProgress.None : Me.Cursor = Cursors.Default
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

    Private Sub ReportDPDFMP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate()
        Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate()
        Dim DV As DataView = Me.clsDistReport.CreateViewDistributor()
        Me.mcbDistributor.SetDataBinding(DV, "")
        Me.IsFirstLoadReport = True
    End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rowfilter As String = "DISTRIBUTOR_NAME LIKE '%" & Me.mcbDistributor.Text & "%'"
            CType(Me.mcbDistributor.DataSource, DataView).RowFilter = rowfilter
            Me.mcbDistributor.DropDownList().Refetch()
            Dim itemCount As Integer = Me.mcbDistributor.DropDownList().RecordCount()
            MessageBox.Show(itemCount.ToString() & " item(s) found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
