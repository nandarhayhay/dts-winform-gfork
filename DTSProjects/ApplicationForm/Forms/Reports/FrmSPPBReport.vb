Imports CrystalDecisions.Shared
Imports System.Xml
Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmSPPBReport

    Friend isSingleReport As Boolean
    Friend ReportDoc As ReportDocument
    Friend hasLoadForm As Boolean = False
    Friend Event Grid_RowCheckStateChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs)
    Friend Event Grid_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Friend Event Search_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Friend Event Search_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Friend Event Search_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Private Sub GridEX1_RowCheckStateChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles GridEX1.RowCheckStateChanged
        RaiseEvent Grid_RowCheckStateChanged(sender, e)
    End Sub

    Private Sub GridEX1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        RaiseEvent Grid_KeyDown(sender, e)
    End Sub

    Private Sub txtSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Enter
        RaiseEvent Search_Enter(sender, e)
    End Sub

    Private Sub txtSearch_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Leave
        RaiseEvent Search_Leave(sender, e)
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        RaiseEvent Search_KeyDown(sender, e)
    End Sub

    Private Sub FrmSPPBReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If isSingleReport Then
            Me.pnlGrid.Size = New Size(0, Me.pnlGrid.Height)
            Me.ExpandableSplitter1.Expandable = False
        End If
        Me.hasLoadForm = True
    End Sub
End Class
