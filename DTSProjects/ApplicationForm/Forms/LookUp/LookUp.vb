Public Class LookUp
    Friend Event OkClick(ByVal sender As Object, ByVal e As EventArgs)
    Friend Event SearchKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Friend Event GridRowCheckStateChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs)
    Friend Event GridDoubleClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'Friend Event ColHeaderClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
    Friend IsloadingRow As Boolean = False
    'Friend Event gridRowChecked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs)
    Friend Property watermarkText() As String
        Get
            Return Me.txtSearch.WaterMarkText
        End Get
        Set(ByVal value As String)
            Me.txtSearch.WaterMarkText = value
        End Set
    End Property

    Friend ReadOnly Property Grid() As Janus.Windows.GridEX.GridEX
        Get
            Return Me.GridEX1
        End Get

    End Property
    Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        RaiseEvent SearchKeyDown(sender, e)
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        RaiseEvent OkClick(sender, e)
    End Sub

    Private Sub LookUp_ClosingForm() Handles Me.ClosingForm
        Me.Grid.Dispose()
    End Sub

    Private Sub LookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsNothing(Me.Grid.DataSource) Then
            Me.Grid.AutoSizeColumns()
        End If
        Me.Cursor = Cursors.Default

    End Sub

    'Private Sub GridEX1_RowCheckStateChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles GridEX1.RowCheckStateChanged
    '    RaiseEvent gridRowChecked(sender, e)
    'End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub GridEX1_RowCheckStateChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles GridEX1.RowCheckStateChanged
        If IsloadingRow Then
            Return
        End If
        RaiseEvent GridRowCheckStateChanged(sender, e)
    End Sub

    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        RaiseEvent GridDoubleClicked(sender, e)
    End Sub

    'Private Sub GridEX1_ColumnHeaderClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnHeaderClick
    '    If e.Column.Key = "BRANDPACK_ID" Then
    '        Select Case CType(sender, Janus.Windows.EditControls.UICheckBox).Checked
    '            Case False
    '                Dim c As String = "pass"
    '                MessageBox.Show(c)
    '            Case True
    '                Dim c As String = "pass"
    '                MessageBox.Show(c)
    '        End Select

    '    End If
    'End Sub
End Class
