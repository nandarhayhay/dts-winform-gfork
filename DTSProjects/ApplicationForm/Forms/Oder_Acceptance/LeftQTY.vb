Public Class LeftQTY
    Public Event LeftQtyOK(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    Public Event LeftQtyCancel(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    Public Event QtyChanged(ByVal sender As Object, ByVal e As EventArgs)
    Private m_RemaindingQty As Decimal = 0
    Private m_BrandPack_ID As String
    'Dim clsOADiscount As NufarmBussinesRules.OrderAcceptance.OADiscount
    Private m_Unit As String
    Friend Property Unit() As String
        Get
            Return Me.m_Unit
        End Get
        Set(ByVal value As String)
            Me.m_Unit = value
        End Set
    End Property

    Friend Property RemaindingQty() As Decimal
        Get
            Return Me.m_RemaindingQty
        End Get
        Set(ByVal value As Decimal)
            Me.m_RemaindingQty = value
        End Set
    End Property
    Friend Property BRANDPACK_ID() As String
        Get
            Return Me.m_BrandPack_ID
        End Get
        Set(ByVal value As String)
            Me.m_BrandPack_ID = value
        End Set
    End Property
    Private Sub LeftQTY_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.AcceptButton = Me.lnkOK
        Me.CancelButton = Me.lnkCancel
    End Sub

    Private Sub lnkOK_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkOK.LinkClicked
        Try
            RaiseEvent LeftQtyOK(sender, e)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lnkCancel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCancel.LinkClicked
        Try
            RaiseEvent LeftQtyCancel(sender, e)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtQty_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.ValueChanged
        Try
            If Not Me.RemaindingQty = 0 Then
                If CDec(Me.txtQty.Value) > Me.RemaindingQty Then
                    Me.txtQty.Text = Me.txtQty.Text.Remove(Me.txtQty.Text.Length - 1, 1)
                    Return
                End If
                RaiseEvent QtyChanged(sender, e)

                'if Cdec(me.txtQty.Value) mod  >0

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class