Imports System.Threading
Public Class Accrue
    Private DV As DataView = Nothing
    Private clsAccrue As New NufarmBussinesRules.DistributorAgreement.Target_Agreement

    Private isHasLoad As Boolean = False
    Private IsFailLoadReport As Boolean = False

    Private Ticcount As Integer = 0
    Private LD As Loading
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
    Private Sub HideColumns()
        With Me.GridEX1.RootTable
            .Columns("TARGET_Q1").Visible = Me.rdbQuarter.Checked
            .Columns("ACTUAL_Q1").Visible = Me.rdbQuarter.Checked
            .Columns("ACHIEVEMENT_DISPROQ1").Visible = Me.rdbQuarter.Checked
            .Columns("DISPROQ1").Visible = Me.rdbQuarter.Checked
            .Columns("BONUS_QTYQ1").Visible = Me.rdbQuarter.Checked
            .Columns("TARGET_Q2").Visible = Me.rdbQuarter.Checked
            .Columns("ACTUAL_Q2").Visible = Me.rdbQuarter.Checked
            .Columns("ACHIEVEMENT_DISPROQ2").Visible = Me.rdbQuarter.Checked
            .Columns("DISPROQ2").Visible = Me.rdbQuarter.Checked
            .Columns("BONUS_QTYQ2").Visible = Me.rdbQuarter.Checked
            .Columns("TARGET_Q3").Visible = Me.rdbQuarter.Checked
            .Columns("ACTUAL_Q3").Visible = Me.rdbQuarter.Checked
            .Columns("ACHIEVEMENT_DISPROQ3").Visible = Me.rdbQuarter.Checked
            .Columns("DISPROQ3").Visible = Me.rdbQuarter.Checked
            .Columns("BONUS_QTYQ3").Visible = Me.rdbQuarter.Checked
            .Columns("TARGET_Q4").Visible = Me.rdbQuarter.Checked
            .Columns("ACTUAL_Q4").Visible = Me.rdbQuarter.Checked
            .Columns("ACHIEVEMENT_DISPROQ4").Visible = Me.rdbQuarter.Checked
            .Columns("DISPROQ4").Visible = Me.rdbQuarter.Checked
            .Columns("BONUS_QTYQ4").Visible = Me.rdbQuarter.Checked
            .Columns("TARGET_S1").Visible = Me.rdbSemester.Checked
            .Columns("ACTUAL_S1").Visible = Me.rdbSemester.Checked
            .Columns("ACHIEVEMENT_DISPROS1").Visible = Me.rdbSemester.Checked
            .Columns("DISPROS1").Visible = Me.rdbSemester.Checked
            .Columns("BONUS_QTYS1").Visible = Me.rdbSemester.Checked
            .Columns("TARGET_S2").Visible = Me.rdbSemester.Checked
            .Columns("ACTUAL_S2").Visible = Me.rdbSemester.Checked
            .Columns("ACHIEVEMENT_DISPROS2").Visible = Me.rdbSemester.Checked
            .Columns("DISPRO_S2").Visible = Me.rdbSemester.Checked
            .Columns("BONUS_QTYS2").Visible = Me.rdbSemester.Checked

        End With
    End Sub
    Private Sub bindCheckedCombo(ByVal DV As DataView)
        With Me.chkDistributor
            Me.chkDistributor.SetDataBinding(DV, "")
            If DV Is Nothing Then : Return : End If

            .DropDownDisplayMember = "DISTRIBUTOR_NAME"
            .DropDownValueMember = "DISTRIBUTOR_ID"
            '.DropDownList.Columns("DISTRIBUTOR_ID").ShowRowSelector = True

            .DropDownList.AutoSizeColumns()
            '.DropDownList.Columns("DISTRIBUTOR_ID").AutoSize()
            '.DropDownList.Columns("DISTRIBUTOR_NAME").AutoSize()
            '.DroppedDown = True

            '.DroppedDown = False : .Refresh()
            '.Update()
        End With
    End Sub
    Friend Sub RefReshData()
        Try
            Me.btnApplyFilter_Click(Me.btnApplyFilter, New EventArgs())
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rdbSemester_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSemester.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.isHasLoad Then
                If Me.rdbSemester.Checked Then
                     Me.btnApplyFilter_Click(Me.btnApplyFilter, New EventArgs())
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbQuarter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbQuarter.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.isHasLoad Then
                If Me.rdbQuarter.Checked Then
                    Me.btnApplyFilter_Click(Me.btnApplyFilter, New EventArgs())
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSearchDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim DV As DataView = Me.clsAccrue.GetDistributorAgreement(RTrim(Me.chkDistributor.Text))
            Me.bindCheckedCombo(DV)
            Dim ItemCount As Integer = Me.chkDistributor.DropDownList.RecordCount
            MessageBox.Show(ItemCount.ToString & " item(s) found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception : MessageBox.Show(ex.Message)
        Finally : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnApplyFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyFilter.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            Me.Timer1.Enabled = True : Me.Timer1.Start()
            Dim Flag As String = ""
            If Me.rdbQuarter.Checked Then : Flag = "Q" : ElseIf Me.rdbSemester.Checked Then : Flag = "S" : End If
            If Not IsNothing(Me.chkDistributor.CheckedValues) Then
                If Me.chkDistributor.CheckedValues.Length > 0 Then
                    Dim ListDistributor As New List(Of String)
                    For i As Integer = 0 To Me.chkDistributor.CheckedValues.Length - 1
                        ListDistributor.Add(Me.chkDistributor.CheckedValues.GetValue(i).ToString())
                    Next
                    DV = Me.clsAccrue.GetAccrued(Flag, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), ListDistributor)
                Else : DV = Me.clsAccrue.GetAccrued(Flag, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
                End If
            Else : DV = Me.clsAccrue.GetAccrued(Flag, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            End If
            'Me.GridEX1.SetDataBinding(DV, "")
            'If Me.rdbQuarter.Checked Then : Me.rdbQuarter_CheckedChanged(Me.rdbQuarter, New EventArgs())
            'ElseIf Me.rdbSemester.Checked Then : Me.rdbSemester_CheckedChanged(Me.rdbSemester, New EventArgs())
            'End If
            Me.IsFailLoadReport = False

        Catch ex As Exception
            Me.IsFailLoadReport = True : Me.StatProg = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False : MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Accrue_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.dtPicFrom.Value = NufarmBussinesRules.SharedClass.ServerDate
            Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
            Dim DV As DataView = Me.clsAccrue.GetDistributorAgreement("")
            Me.bindCheckedCombo(DV)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.isHasLoad = True : Me.chkDistributor.DroppedDown = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.IsFailLoadReport Then
            Me.StatProg = StatusProgress.None : Me.IsFailLoadReport = False
            Me.GridEX1.SetDataBinding(Nothing, "")
        End If
        Me.Ticcount += 1
        If Me.Ticcount >= 2 Then
            If Not Me.IsFailLoadReport Then
                Me.StatProg = StatusProgress.None
            End If : Me.Timer1.Stop() : Me.Timer1.Enabled = False
            Me.GridEX1.SetDataBinding(DV, "")
            Me.HideColumns()
        End If
    End Sub
End Class
