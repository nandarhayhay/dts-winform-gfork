Imports System.Threading
Public Class ThirdParty_Report
    Private clsThirdParty As New NufarmBussinesRules.OrderAcceptance.ThirdParty
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
    Private HasLoadForm As Boolean = False
    Private LD As Loading
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private Enum StatusProgress
        None
        Processing
    End Enum
    Private Enum StateFillingMCB
        Filling
        HasFilled
    End Enum

    Private Sub ShowProceed()
        Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Me.LD.Refresh() : Thread.Sleep(100) : Application.DoEvents()
        End While
        Thread.Sleep(100) : Me.LD.Close() : Me.LD = Nothing
    End Sub
    Private Sub closeProgres() Handles Me.CloseProgress
        Me.Timer1.Enabled = False
        Me.Timer1.Stop()
        'If Not IsNothing(Me.ST) Then
        '    Me.ST.Close()
        '    Me.ST = Nothing
        'End If
        Me.Timer2.Enabled = False
        Me.tickCount = 0
        Me.CounterTimer2 = 0
        If Not IsNothing(Me.LD) Then : Me.LD.Close() : Me.LD = Nothing : End If
    End Sub
    Private Sub ChekTimer2(ByVal sender As Object, ByVal e As EventArgs)
        If Me.CounterTimer2 > 1 Then
            Me.Timer2.Stop() : Me.Timer2.Enabled = False
            Me.CounterTimer2 = 0
            Me.getData()
            Me.HasLoadReport = True
        End If

    End Sub
    Private Overloads Sub ShowProgress(ByVal Message As String) Handles Me.ShowProgres
        Me.ST = New Progress
        Application.DoEvents()
        Me.ST.Show(Message)
        'Me.ST.PictureBox1.Refresh()
        'Me.ST.Refresh()
        '
    End Sub

    Friend Sub RefreshData()
        'Me.clsThirdParty = New NufarmBussinesRules.OrderAcceptance.ThirdParty()
        'Me.clsDistRep.CreateViewDistributor()
        'Dim ValueDist As Object = Me.cmbDistributor.Value
        'Me.BindMulticolumnCombo()
        'Me.cmbDistributor.Value = Nothing
        'Me.cmbDistributor.Value = ValueDist
        Me.btnApplyRange_Click(Me.btnApplyRange, New System.EventArgs())
    End Sub

    Private Sub getData()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.grdMain.SetDataBinding(Nothing, "")
            Me.grdMain.Update()
            If (Me.dtPicfrom.Text <> "") And (Me.dtPicUntil.Text <> "") Then
                Me.clsThirdParty.CreateViewThirdParty(DBNull.Value, CDate(Me.dtPicfrom.Value.ToShortDateString()), CDate(Me.dtPicUntil.Value.ToShortDateString()))
            ElseIf (Me.dtPicfrom.Text <> "") And (Me.dtPicUntil.Text = "") Then
                Me.clsThirdParty.CreateViewThirdParty(DBNull.Value, CDate(Me.dtPicfrom.Value.ToShortDateString()), DBNull.Value)
            ElseIf (Me.dtPicfrom.Text = "") And (Me.dtPicUntil.Text <> "") Then
                Me.clsThirdParty.CreateViewThirdParty(DBNull.Value, DBNull.Value, CDate(Me.dtPicUntil.Value.ToShortDateString()))
            Else
                Me.HasLoadReport = True : Me.IsFailedToLoad = True
                Me.StatProg = StatusProgress.None
                MessageBox.Show("System can not view data" & vbCrLf & "Because no datetime data is selected.")
                Return
            End If
            If Me.IsFirstLoadReport = True Then
                Me.grdMain.SetDataBinding(Me.clsThirdParty.ViewThirdParty(), "")
            End If
            Me.HasLoadReport = True
            Me.IsFailedToLoad = False
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.IsFirstLoadReport = False : Me.HasLoadReport = True : Me.IsFailedToLoad = True : RaiseEvent CloseProgress() : Throw ex
        Finally
            Me.StatProg = StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.tickCount >= Me.ResultRandom Then
            If Me.HasLoadReport = True Then
                If Me.IsFirstLoadReport = False Then
                    If Not Me.IsFailedToLoad Then
                        Me.grdMain.SetDataBinding(Me.clsThirdParty.ViewThirdParty(), "")
                    Else
                        Me.grdMain.SetDataBinding(Nothing, "")
                    End If
                End If
                'Me.clsDiscMarketing.CreateViewDiscount(Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString())
                'Me.GridEX1.SetDataBinding(Me.clsDiscMarketing.ViewDiscount(), "")
                'Me.FormatGrid()
                RaiseEvent CloseProgress()
                'Me.Timer1.Enabled = False
                'Me.Timer1.Stop()
                Me.IsFirstLoadReport = False
            Else
                Me.ResultRandom += 1
            End If

            'Me.FormatGrid()
        End If
    End Sub

    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.HasLoadReport = False
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            Me.getData()
            'RaiseEvent ShowProgres("Executing Query batched.....")
            'LD = New Loading
            'LD.Show() : LD.TopMost = True
            'Application.DoEvents()

            'Me.rnd = New Random() : Me.ResultRandom = Me.rnd.Next(1, 6)
            'Me.Timer1.Enabled = True : Me.Timer1.Start()
            'Application.DoEvents() : Me.HasLoadReport = False
            'Me.Timer2.Enabled = True : Me.Timer2.Start()

            'RaiseEvent ShowProgres("Processing,Please wait...")
            'RaiseEvent ShowProgres()

            'Me.grdMain.SetDataBinding(Nothing, "")
            'Me.grdMain.Update()
            'If (Me.dtPicfrom.Text <> "") And (Me.dtPicUntil.Text <> "") Then
            '    Me.clsThirdParty.CreateViewThirdParty(DBNull.Value, CDate(Me.dtPicfrom.Value.ToShortDateString()), CDate(Me.dtPicUntil.Value.ToShortDateString()))
            'ElseIf (Me.dtPicfrom.Text <> "") And (Me.dtPicUntil.Text = "") Then
            '    Me.clsThirdParty.CreateViewThirdParty(DBNull.Value, CDate(Me.dtPicfrom.Value.ToShortDateString()), DBNull.Value)
            'ElseIf (Me.dtPicfrom.Text = "") And (Me.dtPicUntil.Text <> "") Then
            '    Me.clsThirdParty.CreateViewThirdParty(DBNull.Value, DBNull.Value, CDate(Me.dtPicUntil.Value.ToShortDateString()))
            'Else
            '    Me.HasLoadReport = True
            '    RaiseEvent CloseProgress()
            '    MessageBox.Show("System can not view data" & vbCrLf & "Because no datetime data is selected.")
            '    Return
            'End If
            'If Me.IsFirstLoadReport = True Then
            '    Me.grdMain.SetDataBinding(Me.clsThirdParty.ViewThirdParty(), "")
            'End If
            'Me.IsFailedToLoad = False
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            Me.HasLoadReport = True
            Me.IsFailedToLoad = True
            'RaiseEvent CloseProgress()
            MessageBox.Show(ex.Message)
            'Finally
            '    'Me.HasLoadReport = True
            '    Me.Cursor = Cursors.Default
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

    Private Sub dtPicUntil_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicUntil.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.dtPicUntil.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ThirdParty_Report_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsThirdParty) Then
                Me.clsThirdParty.Dispose(True)
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ThirdParty_Report_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate()
            Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate()
            If IsNothing(Me.clsThirdParty) Then
                Me.clsThirdParty = New NufarmBussinesRules.OrderAcceptance.ThirdParty()
            End If
            AddHandler Timer1.Tick, AddressOf ChekTimer
            AddHandler Timer2.Tick, AddressOf ChekTimer2
            Me.IsFirstLoadReport = True
            Me.btnApplyRange_Click(Me.btnApplyRange, New System.EventArgs())
        Catch ex As Exception
            Me.HasLoadReport = True : Me.IsFailedToLoad = True : RaiseEvent CloseProgress() : MessageBox.Show(ex.Message)
        Finally
            Me.HasLoadForm = True : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.tickCount += 1
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If Me.HasLoadForm Then
            Me.CounterTimer2 += 1
        End If
    End Sub
End Class
