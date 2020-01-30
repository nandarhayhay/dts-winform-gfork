Public Class OA_Report
    Private clsReportOA As NufarmBussinesRules.OrderAcceptance.Core
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
    Private Sub closeProgres() Handles Me.CloseProgress
        Me.Timer1.Enabled = False
        Me.Timer1.Stop()
        If Not IsNothing(Me.ST) Then
            Me.ST.Close()
            Me.ST = Nothing
            Me.tickCount = 0
        End If
    End Sub
    Private Overloads Sub ShowProgress(ByVal Message As String) Handles Me.ShowProgres
        Me.ST = New Progress
        Application.DoEvents()
        Me.ST.Show(Message)
    End Sub
    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.tickCount >= Me.ResultRandom Then
            If Me.HasLoadReport = True Then
                If Me.IsFirstLoadReport = False Then
                    If Not Me.IsFailedToLoad Then
                        Me.GridEX1.SetDataBinding(Me.clsReportOA.ViewOAReport(), "")
                        Me.GridEX1.RootTable.Columns("PO_REF_NO").AutoSize()
                        Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").AutoSize()
                        Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
                    Else
                        Me.GridEX1.SetDataBinding(Nothing, "")
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
    Public Sub RefreshData()
        Me.btnApplyRange_Click(Me.btnApplyRange, New System.EventArgs())
    End Sub
    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.HasLoadReport = False
            RaiseEvent ShowProgres("Executing Query batched.......")
            Me.rnd = New Random()
            Me.ResultRandom = Me.rnd.Next(1, 5)
            Me.Timer1.Enabled = True
            Me.Timer1.Start()
            Me.GridEX1.SetDataBinding(Nothing, "")
            Me.GridEX1.Update()
            If Me.rdbOA.Checked = True Then
                If (Me.dtPicFrom.Text <> "") And (Me.dtPicUntil.Text <> "") Then
                    Me.clsReportOA.generateReport(CObj(CDate(Me.dtPicFrom.Value.ToShortDateString())), CObj(CDate(Me.dtPicUntil.Value.ToShortDateString())))
                ElseIf Me.dtPicFrom.Text <> "" Then
                    Me.clsReportOA.generateReport(CObj(CDate(Me.dtPicFrom.Value.ToShortDateString())))
                ElseIf Me.dtPicUntil.Text <> "" Then
                    Me.clsReportOA.generateReport(, CObj(CDate(Me.dtPicUntil.Value.ToShortDateString())))
                Else
                    Me.HasLoadReport = True
                    RaiseEvent CloseProgress()
                    MessageBox.Show("System can not view data" & vbCrLf & "Because no datetime data is selected.")
                    Return
                End If
            ElseIf Me.rdbPO.Checked = True Then
                If (Me.dtPicFrom.Text <> "") And (Me.dtPicUntil.Text <> "") Then
                    Me.clsReportOA.generateReport(, , CObj(CDate(Me.dtPicFrom.Value.ToShortDateString())), CObj(CDate(Me.dtPicUntil.Value.ToShortDateString())))
                ElseIf Me.dtPicFrom.Text <> "" Then
                    Me.clsReportOA.generateReport(, , CObj(CDate(Me.dtPicFrom.Value.ToShortDateString())))
                ElseIf Me.dtPicUntil.Text <> "" Then
                    Me.clsReportOA.generateReport(, , , CObj(CDate(Me.dtPicUntil.Value.ToShortDateString())))
                Else
                    Me.HasLoadReport = True
                    RaiseEvent CloseProgress()
                    MessageBox.Show("System can not view data" & vbCrLf & "Because no datetime data is selected.")
                    Return
                    'MessageBox.Show("System can not view data" & vbCrLf & "Because no datetime data is selected.")
                    'Return
                End If
            Else
                Me.GridEX1.SetDataBinding(Nothing, "")
                Return
            End If
            If Me.IsFirstLoadReport = True Then
                Me.GridEX1.SetDataBinding(Me.clsReportOA.ViewOAReport(), "")
                Me.GridEX1.RootTable.Columns("PO_REF_NO").AutoSize()
                Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").AutoSize()
                Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
            End If

        Catch ex As Exception
            Me.HasLoadReport = True
            Me.IsFailedToLoad = True
            RaiseEvent CloseProgress()
            MessageBox.Show(ex.Message)
        Finally
            Me.HasLoadReport = True
            'Me.IsFirstLoadReport = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OA_Report_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            If Not IsNothing(Me.clsReportOA) Then
                Me.clsReportOA.Discpose(False)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub OA_Report_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsReportOA = New NufarmBussinesRules.OrderAcceptance.Core()
            Me.dtPicFrom.Value = NufarmBussinesRules.SharedClass.ServerDate()
            Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate()
            If IsNothing(Me.clsReportOA) Then
                Me.clsReportOA = New NufarmBussinesRules.OrderAcceptance.Core()
            End If
            AddHandler Timer1.Tick, AddressOf ChekTimer
            Me.IsFirstLoadReport = True
            btnApplyRange_Click(Me.btnApplyRange, New EventArgs())

            'Me.clsReportOA.generateReport(CObj(CDate(Me.dtPicFrom.Value.ToShortDateString())), CObj(CDate(Me.dtPicUntil.Value.ToShortDateString())))
            'Me.GridEX1.SetDataBinding(Me.clsReportOA.ViewOAReport(), "")
            'Me.GridEX1.RootTable.Columns("PO_REF_NO").AutoSize()
            'Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").AutoSize()
            'Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
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

    Private Sub dtPicUntil_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicUntil.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.dtPicUntil.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.tickCount += 1
    End Sub
End Class
