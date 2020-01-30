Imports System.Threading
Public Class Distributor_Report
    Private clsDistRep As NufarmBussinesRules.DistributorReport.Dist_Report
    Private SFM As StateFillingMCB
    Private Delegate Sub onShowingProgress(ByVal message As String)
    Private Event ShowProgres As onShowingProgress
    Private Delegate Sub onClossingProgress()
    Private Event CloseProgress As onClossingProgress
    Private tickCount As Integer
    Private WithEvents ST As DTSProjects.Progress
    Dim rnd As Random
    Private ResultRandom As Integer
    Private HasLoadReport As Boolean
    Private IsFailedToLoad As Boolean
    Private IsFirstLoadReport As Boolean
    Private CounterTimer2 As Integer = 0
    Private LD As Loading = Nothing
    Private Sub ChekTimer2(ByVal sender As Object, ByVal e As EventArgs)
        If Me.CounterTimer2 > 1 Then
            Me.Timer2.Stop() : Me.Timer2.Enabled = False
            Me.CounterTimer2 = 0
            Me.getData()
            Me.HasLoadReport = True
        End If

    End Sub
    Private Enum StateFillingMCB
        Filling
        HasFilled
    End Enum
    Private Ticcount As Integer = 0
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
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
        Me.Ticcount = 0
    End Sub
    Private Sub closeProgres() Handles Me.CloseProgress
        Me.Timer1.Enabled = False
        Me.Timer1.Stop()
        'If Not IsNothing(Me.ST) Then
        '    Me.ST.Close()
        '    Me.ST = Nothing
        '    Me.tickCount = 0
        'End If
        Me.Timer2.Enabled = False
        If Not IsNothing(Me.LD) Then : Me.LD.Close() : Me.LD = Nothing : End If
        Me.tickCount = 0
        Me.CounterTimer2 = 0
    End Sub
    Private Overloads Sub ShowProgress(ByVal Message As String) Handles Me.ShowProgres
        Me.ST = New Progress
        Application.DoEvents()
        Me.ST.Show(Message)

        'Me.ST.PictureBox1.Refresh()
        'Me.ST.Refresh()
        '
    End Sub
    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.tickCount >= Me.ResultRandom Then
            If Me.HasLoadReport = True Then
                If Me.IsFirstLoadReport = False Then
                    If Not Me.IsFailedToLoad Then
                        Me.GridEX1.SetDataBinding(Me.clsDistRep.ViewDistReport(), "")
                        Me.GridEX1.RootTable.Columns("OA_REF_NO").AutoSize()
                        Me.GridEX1.RootTable.Columns("PO_REF_NO").AutoSize()
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
    Private Sub getData()
        Try
            Me.Cursor = Cursors.WaitCursor : Me.GridEX1.SetDataBinding(Nothing, "") : Me.GridEX1.Update()
            If (Me.dtPicfrom.Text <> "") And (Me.dtPicUntil.Text <> "") Then
                Me.clsDistRep.CreateViewDistReport(CDate(Me.dtPicfrom.Value.ToShortDateString()), CDate(Me.dtPicUntil.Value.ToShortDateString()))
            ElseIf Me.dtPicfrom.Text <> "" Then
                Me.clsDistRep.CreateViewDistReport(CObj(CDate(Me.dtPicfrom.Value.ToShortDateString())), DBNull.Value)
            ElseIf Me.dtPicUntil.Text <> "" Then
                Me.clsDistRep.CreateViewDistReport(DBNull.Value, CObj(CDate(Me.dtPicUntil.Value.ToShortDateString())))
            Else
                Me.HasLoadReport = True : Me.StatProg = StatusProgress.None
                'RaiseEvent CloseProgress()
                MessageBox.Show("System can not view data" & vbCrLf & "Because no datetime data is selected.")
                Return
            End If
            If Me.IsFirstLoadReport = True Then
                Me.GridEX1.SetDataBinding(Me.clsDistRep.ViewDistReport(), "")
                Me.GridEX1.RootTable.Columns("OA_REF_NO").AutoSize()
                Me.GridEX1.RootTable.Columns("PO_REF_NO").AutoSize()
                Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
            Else
                If Not Me.IsFailedToLoad Then
                    Me.GridEX1.SetDataBinding(Me.clsDistRep.ViewDistReport(), "")
                    Me.GridEX1.RootTable.Columns("OA_REF_NO").AutoSize()
                    Me.GridEX1.RootTable.Columns("PO_REF_NO").AutoSize()
                    Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
                Else
                    Me.GridEX1.SetDataBinding(Nothing, "")
                End If
            End If
            Me.HasLoadReport = True : Me.IsFailedToLoad = False
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.IsFirstLoadReport = False : Me.HasLoadReport = True : Me.IsFailedToLoad = True : Me.StatProg = StatusProgress.None : Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Friend Sub InitializeDate()
        Me.dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate()
        Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate()
        Me.clsDistRep = New NufarmBussinesRules.DistributorReport.Dist_Report()

        'Me.RefreshData()
    End Sub
    Friend Sub RefreshData()
        Me.clsDistRep = New NufarmBussinesRules.DistributorReport.Dist_Report()
        'Me.clsDistRep.CreateViewDistributor()
        'Dim ValueDist As Object = Me.cmbDistributor.Value
        'Me.BindMulticolumnCombo()
        'Me.cmbDistributor.Value = Nothing
        'Me.cmbDistributor.Value = ValueDist
        Me.btnApplyRange_Click(Me.btnApplyRange, New System.EventArgs())
    End Sub
    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.HasLoadReport = False : Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            Me.getData()
        Catch ex As Exception
            Me.HasLoadReport = True
            Me.IsFailedToLoad = True
            Me.StatProg = StatusProgress.None
            MessageBox.Show(ex.Message)
        Finally
            Me.HasLoadReport = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Distributor_Report_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsDistRep) Then
                Me.clsDistRep.Dispose(False)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Distributor_Report_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate()
            Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate()

            If IsNothing(Me.clsDistRep) Then
                Me.clsDistRep = New NufarmBussinesRules.DistributorReport.Dist_Report()
            End If
            Me.btnApplyRange_Click(Me.btnApplyRange, New System.EventArgs())
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub dtPicfrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicfrom.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.dtPicfrom.Text = ""
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtPicUntil_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicUntil.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.dtPicUntil.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.tickCount += 1
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Me.CounterTimer2 += 1
    End Sub
End Class
