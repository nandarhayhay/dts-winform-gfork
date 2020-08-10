Imports System.Threading
Public Class DDDR
    Private IsFirstLoadReport As Boolean : Private LD As Loading
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private m_clsDDDR
    Public frmParent As ReportGrid = Nothing
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
    End Sub

    Private ReadOnly Property clsDDDR() As NufarmBussinesRules.Brandpack.DiscountPrice
        Get
            If IsNothing(Me.m_clsDDDR) Then
                Me.m_clsDDDR = New NufarmBussinesRules.Brandpack.DiscountPrice()
            End If
            Return Me.m_clsDDDR
        End Get
    End Property
    Friend Sub RefreshData()
        Me.btnApplyRange_Click(Me.btnApplyRange, New EventArgs())
    End Sub

    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            Dim DV As DataView = Me.clsDDDR.getReport(Me.cmbApplyDiscount.Text, Me.cmbParams.Text, Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            Me.grdDDDR.SetDataBinding(DV, "")
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            'RaiseEvent CloseProgress()
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message)

        End Try
    End Sub
End Class
