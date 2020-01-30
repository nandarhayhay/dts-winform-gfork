Imports System.Threading
Public Class SPPB_Report
    Private clsSPPBDetail As New NufarmBussinesRules.OrderAcceptance.SPPB_Detail
    Private SFM As StateFillingMCB
    Private Delegate Sub onShowingProgress(ByVal message As String)
    Private Event ShowProgres As onShowingProgress
    Private Delegate Sub onClossingProgress()
    Private Event CloseProgress As onClossingProgress
    Private tickCount As Integer
    Private WithEvents ST As DTSProjects.Progress
    Dim rnd As Random : Private ResultRandom As Integer
    Private HasLoadReport As Boolean : Private IsFailedToLoad As Boolean
    Private IsFirstLoadReport As Boolean : Private LD As Loading
    Private CounterTimer2 As Integer = 0
    Private HasLoad As Boolean = False
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
            Me.LD.Refresh() : Thread.Sleep(50) : Application.DoEvents()
        End While
        Thread.Sleep(50) : Me.LD.Close() : Me.LD = Nothing
        Me.Ticcount = 0
    End Sub
    Private Sub ChekTimer2(ByVal sender As Object, ByVal e As EventArgs)
        If Me.CounterTimer2 > 1 Then
            Me.Timer2.Stop() : Me.Timer2.Enabled = False
            Me.CounterTimer2 = 0
            Me.getData()
            Me.HasLoadReport = True
        End If
    End Sub
    Private Sub getData()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.GridEX1.SetDataBinding(Nothing, "")
            Me.GridEX1.Update()
            'If (Me.dtPicfrom.Text <> "") And (Me.dtPicUntil.Text <> "") Then
            '    Me.clsSPPBDetail.CreateViewSPPBDetail("BySPPB", DBNull.Value, CDate(Me.dtPicfrom.Value.ToShortDateString()), CDate(Me.dtPicUntil.Value.ToShortDateString()))
            'ElseIf Me.dtPicfrom.Text <> "" Then
            '    Me.clsSPPBDetail.CreateViewSPPBDetail("BySPPB", DBNull.Value, CObj(CDate(Me.dtPicfrom.Value.ToShortDateString())), DBNull.Value)
            'ElseIf Me.dtPicUntil.Text <> "" Then
            '    Me.clsSPPBDetail.CreateViewSPPBDetail("BySPPB", DBNull.Value, DBNull.Value, CObj(CDate(Me.dtPicUntil.Value.ToShortDateString())))
            'Else
            '    Me.StatProg = StatusProgress.None : Me.HasLoadReport = True : Me.IsFailedToLoad = True
            '    'RaiseEvent CloseProgress()
            '    MessageBox.Show("System can not view data" & vbCrLf & "Because no datetime data is selected.")
            '    Return
            'End If
            Me.clsSPPBDetail.CreateViewSPPBReport(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            'If Not Me.IsFailedToLoad Then
            '    Me.BindGrid(Me.clsSPPBDetail.ViewSPPBDetail())
            'Else
            '    Me.GridEX1.SetDataBinding(Nothing, "")
            'End If
            'If Me.IsFirstLoadReport = True Then
            '    Me.BindGrid(Me.clsSPPBDetail.ViewSPPBDetail())
            'End If
            Me.IsFailedToLoad = False : Me.HasLoadReport = True
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.IsFirstLoadReport = False : Me.HasLoadReport = True : Me.IsFailedToLoad = True : Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub BindGrid(ByVal dtview As DataView)
        If (dtview.Count <= 0) Or (IsNothing(dtview)) Then
            Me.GridEX1.SetDataBinding(dtview, "")
            Return
        End If
        For I As Int64 = 0 To dtview.Count - 1
            Dim sppbQty As Object = dtview(I)("SPPB_QTY")
            If IsNothing(sppbQty) Or IsDBNull(sppbQty) Then
                dtview(I)("STATUS") = "PENDING"
                dtview(I)("BALANCE") = DBNull.Value
                dtview(I).EndEdit()
            ElseIf CDec(dtview(I)("SPPB_QTY")) <= 0 Then
                dtview(I)("STATUS") = "PENDING"
                dtview(I)("BALANCE") = DBNull.Value
                dtview(I).EndEdit()
            End If
        Next
        Me.GridEX1.SetDataBinding(dtview, "")
        If Not IsNothing(dtview) Then
            If Me.GridEX1.GetTotal(Me.GridEX1.RootTable.Columns("GON_6_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
                Me.GridEX1.RootTable.Columns("GON_6_NO").Visible = False
                Me.GridEX1.RootTable.Columns("GON_6_DATE").Visible = False
                Me.GridEX1.RootTable.Columns("GON_6_QTY").Visible = False
            Else
                With Me.GridEX1.RootTable
                    .Columns("GON_6_NO").Visible = True
                    .Columns("GON_6_DATE").Visible = True
                    .Columns("GON_6_QTY").Visible = True
                End With
            End If
        End If
        If Me.GridEX1.GetTotal(Me.GridEX1.RootTable.Columns("GON_5_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
            With Me.GridEX1.RootTable
                .Columns("GON_5_NO").Visible = False
                .Columns("GON_5_DATE").Visible = False
                .Columns("GON_5_QTY").Visible = False
            End With
        Else
            With Me.GridEX1.RootTable
                .Columns("GON_5_NO").Visible = True
                .Columns("GON_5_DATE").Visible = True
                .Columns("GON_5_QTY").Visible = True
            End With
        End If
        If Me.GridEX1.GetTotal(Me.GridEX1.RootTable.Columns("GON_4_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
            With Me.GridEX1.RootTable
                .Columns("GON_4_NO").Visible = False
                .Columns("GON_4_DATE").Visible = False
                .Columns("GON_4_QTY").Visible = False
            End With
        Else
            With Me.GridEX1.RootTable
                .Columns("GON_4_NO").Visible = True
                .Columns("GON_4_DATE").Visible = True
                .Columns("GON_4_QTY").Visible = True
            End With
        End If
        If Me.GridEX1.GetTotal(Me.GridEX1.RootTable.Columns("GON_3_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
            With Me.GridEX1.RootTable
                .Columns("GON_3_NO").Visible = False
                .Columns("GON_3_DATE").Visible = False
                .Columns("GON_3_QTY").Visible = False
            End With
        Else
            With Me.GridEX1.RootTable
                .Columns("GON_3_NO").Visible = True
                .Columns("GON_3_DATE").Visible = True
                .Columns("GON_3_QTY").Visible = True
            End With
        End If
        If Me.GridEX1.GetTotal(Me.GridEX1.RootTable.Columns("GON_2_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
            With Me.GridEX1.RootTable
                .Columns("GON_2_NO").Visible = False
                .Columns("GON_2_DATE").Visible = False
                .Columns("GON_2_QTY").Visible = False
            End With
        Else
            With Me.GridEX1.RootTable
                .Columns("GON_2_NO").Visible = True
                .Columns("GON_2_DATE").Visible = True
                .Columns("GON_2_QTY").Visible = True
            End With
        End If
        If Me.GridEX1.GetTotal(Me.GridEX1.RootTable.Columns("GON_1_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum) <= 0 Then
            With Me.GridEX1.RootTable
                .Columns("GON_1_NO").Visible = False
                .Columns("GON_1_DATE").Visible = False
                .Columns("GON_1_QTY").Visible = False
            End With
        Else
            With Me.GridEX1.RootTable
                .Columns("GON_1_NO").Visible = True
                .Columns("GON_1_DATE").Visible = True
                .Columns("GON_1_QTY").Visible = True
            End With
        End If
        Me.GridEX1.RootTable.Columns("STATUS").Visible = True
        Me.GridEX1.RootTable.Columns("BALANCE").Visible = True
        Me.GridEX1.RootTable.Columns("REMARK").Visible = True
        With Me.GridEX1.RootTable
            .Columns("OTP_NO").Visible = False
            .Columns("OTP_DATE").Visible = False
            .Columns("OTP_QTY").Visible = False
            '.Columns("DO_TP_NO").Visible = False
            '.Columns("DO_3RD_P_DATE").Visible = False
            '.Columns("DO_3RD_P_QTY").Visible = False
        End With
    End Sub
    Private Enum StateFillingMCB
        Filling
        HasFilled
    End Enum

    'Private Sub closeProgres() Handles Me.CloseProgress
    '    Me.Timer1.Enabled = False
    '    Me.Timer1.Stop()
    '    If Not IsNothing(Me.ST) Then
    '        Me.ST.Close()
    '        Me.ST = Nothing
    '        'Me.Timer1.Enabled = False : Me.Timer1.Stop()
    '        Me.tickCount = 0
    '    End If
    'End Sub
    Private Sub closeProgressbar() Handles Me.CloseProgress
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

    Private Overloads Sub ShowProgress(ByVal Message As String) Handles Me.ShowProgres
        Me.ST = New Progress
        Application.DoEvents()
        Me.ST.Show(Message)
    End Sub

    Friend Sub RefreshData()
        'Me.clsSPPBDetail = New NufarmBussinesRules.OrderAcceptance.SPPB_Detail()
        Me.btnApplyRange_Click(Me.btnApplyRange, New System.EventArgs())
    End Sub

    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.tickCount >= Me.ResultRandom Then
            If Me.HasLoadReport = True Then
                If Me.IsFirstLoadReport = False Then
                    If Not Me.IsFailedToLoad Then
                        Me.BindGrid(Me.clsSPPBDetail.ViewSPPBDetail())
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

    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.HasLoadReport = False : Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            Me.getData()
            If Not Me.IsFailedToLoad Then
                Me.BindGrid(Me.clsSPPBDetail.ViewSPPBDetail())
            Else
                Me.GridEX1.SetDataBinding(Nothing, "")
            End If
            Me.StatProg = StatusProgress.None
            'RaiseEvent ShowProgres("Executing Query batched.....")
            'Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
            'Application.DoEvents()
            'Me.rnd = New Random()
            'Me.ResultRandom = Me.rnd.Next(1, 5)
            'Me.Timer1.Enabled = True : Me.Timer1.Start()
            'Me.Timer2.Enabled = True : Me.Timer2.Start()
            'Me.GridEX1.SetDataBinding(Nothing, "")
            'Me.GridEX1.Update()
            'If (Me.dtPicfrom.Text <> "") And (Me.dtPicUntil.Text <> "") Then
            '    Me.clsSPPBDetail.CreateViewSPPBDetail("BySPPB", DBNull.Value, CDate(Me.dtPicfrom.Value.ToShortDateString()), CDate(Me.dtPicUntil.Value.ToShortDateString()))
            'ElseIf Me.dtPicfrom.Text <> "" Then
            '    Me.clsSPPBDetail.CreateViewSPPBDetail("BySPPB", DBNull.Value, CObj(CDate(Me.dtPicfrom.Value.ToShortDateString())), DBNull.Value)
            'ElseIf Me.dtPicUntil.Text <> "" Then
            '    Me.clsSPPBDetail.CreateViewSPPBDetail("BySPPB", DBNull.Value, DBNull.Value, CObj(CDate(Me.dtPicUntil.Value.ToShortDateString())))
            'Else
            '    Me.HasLoadReport = True
            '    RaiseEvent CloseProgress()
            '    MessageBox.Show("System can not view data" & vbCrLf & "Because no datetime data is selected.")
            '    Return
            'End If

            'If Me.IsFirstLoadReport = True Then
            '    Me.BindGrid(Me.clsSPPBDetail.ViewSPPBDetail())
            'End If
            'Me.IsFailedToLoad = False
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.HasLoadReport = True : Me.IsFailedToLoad = True
            'RaiseEvent CloseProgress()
            MessageBox.Show(ex.Message)
        Finally
            'Me.HasLoadReport = True
            'Me.IsFirstLoadReport = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SPPB_Report_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsSPPBDetail) Then
                Me.clsSPPBDetail.Dispose(True)
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SPPB_Report_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate().AddMonths(-3)
            Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate()

            If IsNothing(Me.clsSPPBDetail) Then
                Me.clsSPPBDetail = New NufarmBussinesRules.OrderAcceptance.SPPB_Detail()
            End If
            AddHandler Timer1.Tick, AddressOf ChekTimer
            AddHandler Timer2.Tick, AddressOf ChekTimer2
            Me.IsFirstLoadReport = True
            Me.btnApplyRange_Click(Me.btnApplyRange, New System.EventArgs())
        Catch ex As Exception

        Finally
            Me.HasLoad = True : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.tickCount += 1
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If Me.HasLoad Then
            Me.CounterTimer2 += 1
        End If

    End Sub
End Class
