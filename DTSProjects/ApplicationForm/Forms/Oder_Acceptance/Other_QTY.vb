

Public Class Other_QTY
    'Friend Event lnkOKClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    'Friend Event lnkCancelClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    Friend Event txtPercentage_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'Private Sub lnkCancel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    '    Try
    '        RaiseEvent lnkCancelClick(sender, e)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub lnkOK_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    '    Try
    '        RaiseEvent lnkOKClick(sender, e)
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Friend BrandPackID As String = ""
    Friend OABrandPackID As String = ""
    Friend DistributorID As String = ""
    Friend OAQty As Decimal = 0
    Friend PODate As DateTime = DateTime.Now
    Private m_clsDisc As NufarmBussinesRules.Brandpack.DiscountPrice
    Friend Devided_Qty As Decimal = 0
    Private RefOther As Integer = 0
    Private resultQty As Decimal = 0
    Private Flag As String = 0
    Private FullSize As New Size(333, 356)
    Private medSize As New Size(335, 248)

    Private ReadOnly Property ClsDisc() As NufarmBussinesRules.Brandpack.DiscountPrice
        Get
            If IsNothing(Me.m_clsDisc) Then
                Me.m_clsDisc = New NufarmBussinesRules.Brandpack.DiscountPrice()
            End If
            Return Me.m_clsDisc
        End Get
    End Property
    Private Function ValidateData() As Boolean

        If Me.pnlUncategorizedDisc.Visible Then
            If txtPercentage.Value Is Nothing Then
                Me.ShowMessageInfo("Invalid Data !" & vbCrLf & "System can not process invalid data." & vbCrLf & "Data can not be null / zero.")
                'Me.Other_QTY.Show(Me.pnlOADiscount, True)
                Me.baseTooltip.Show("Invalid Data !" & vbCrLf & "System can not process invalid data." & vbCrLf & "Data can not be null / zero.", Me.txtPercentage, 2500)
                Me.txtPercentage.Focus() : Me.txtPercentage.SelectAll()
                Return False
            ElseIf txtResult.Value = 0 Then
                Me.baseTooltip.Show("Invalid Data !" & vbCrLf & "System can not process invalid data." & vbCrLf & "Data can not be null / zero.", Me.txtResult, 2500)
                Me.txtResult.SelectAll() : Me.txtResult.SelectAll()
                Return False
            End If
            Dim LeftQTY As Decimal = Me.txtResult.Value Mod Me.Devided_Qty
            Dim ResultQTY As Decimal = (Decimal.Truncate(Me.txtResult.Value / Me.Devided_Qty)) * Me.Devided_Qty
            If (ResultQTY = 0) And (LeftQTY = 0) Then
                Me.baseTooltip.Show("Can not proceed data with 0 value", Me.txtResult, 2500)
                Return False
            End If
        ElseIf Me.pnlTypeOfDisc.Visible Then
            If Me.btnDiscCBD.Checked Or Me.btnDiscCBD.Checked Or Me.btnDiscDr.Checked Then
                If Me.txtDiscType.Value <= 0 Then
                    Me.baseTooltip.Show("Can not proceed data with 0 value", Me.txtDiscType, 2500)
                    Me.txtDiscType.SelectAll() : Me.txtDiscType.Focus() : Return False
                End If
            End If
        End If
        'BAGILANGSUNG DENGAN QUANTITY(LITRE)/KG
        Return True
    End Function
    Private Sub txtPercentage_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPercentage.ValueChanged
        Try
            'If txtPercentage.Value <= CDec(0) Then
            '    Me.txtResult.Value = 0
            'End If
            RaiseEvent txtPercentage_Changed(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Other_QTY_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Size = Me.medSize
            Me.StartPosition = FormStartPosition.CenterScreen
            If Me.btnDiscCBD.Checked = True Or Me.btnDiscDD.Checked = True Or Me.btnDiscDr.Checked = True Or Me.btnUncategorized.Checked = True Then
                Dim TypeApp As String = "DD", Flag As String = "O"
                If btnDiscCBD.Checked Then
                    Me.pnlTypeOfDisc.Visible = True
                    Me.pnlTypeOfDisc.Enabled = True
                    Me.pnlUncategorizedDisc.Visible = False
                    Me.txtDiscType.SelectAll()
                    Me.txtDiscType.Focus()
                    TypeApp = "CBD"
                    Flag = "OCBD"
                ElseIf btnDiscDD.Checked Then
                    Me.pnlTypeOfDisc.Visible = True
                    Me.pnlTypeOfDisc.Enabled = True
                    Me.pnlUncategorizedDisc.Visible = False
                    Me.txtDiscType.SelectAll()
                    Me.txtDiscType.Focus()
                    TypeApp = "DD"
                    Flag = "ODD"
                ElseIf btnDiscDr.Checked Then
                    Me.pnlTypeOfDisc.Visible = True
                    Me.pnlTypeOfDisc.Enabled = True
                    Me.pnlUncategorizedDisc.Visible = False
                    Me.txtDiscType.SelectAll()
                    Me.txtDiscType.Focus()
                    TypeApp = "DR"
                    Flag = "ODR"
                ElseIf Me.btnUncategorized.Checked Then
                    Me.pnlTypeOfDisc.Visible = False
                    Me.pnlUncategorizedDisc.Visible = True
                    Me.pnlUncategorizedDisc.Enabled = True
                    TypeApp = "O"
                    Flag = "O"
                End If
                Dim info() As String = New String() {}
                If TypeApp <> "O" Then
                    Me.txtDiscType.Value = Me.ClsDisc.getDiscount(Me.OABrandPackID, TypeApp, Me.BrandPackID, Me.DistributorID, Me.PODate, Me.OAQty, Me.RefOther, info, True)
                    If info.Length > 0 Then
                        Me.lblProgramID.Text = info(0)
                        Me.lblProgramInfo.Text = info(1)
                        Me.lblDiscInfo.Text = info(2)
                    End If
                Else
                    Me.txtResult.Value = 0

                End If
                Me.btnOK.Enabled = True
                Me.AcceptButton = Me.btnOK
            ElseIf Me.btnUncategorized.Checked Then
                Me.pnlTypeOfDisc.Visible = False
                Me.pnlUncategorizedDisc.Visible = True
                Me.pnlUncategorizedDisc.Enabled = True
                Me.AcceptButton = Me.btnOK
                Me.CancelButton = Me.btnCancel
            Else
                Me.pnlTypeOfDisc.Visible = False
                Me.pnlUncategorizedDisc.Visible = False
                'Me.pnlUncategorizedDisc.Enabled = False
                Me.AcceptButton = Nothing
                Me.CancelButton = Me.btnCancel
            End If
        Catch ex As Exception
            Me.pnlTypeOfDisc.Enabled = False
            Me.pnlUncategorizedDisc.Enabled = False
            Me.AcceptButton = Nothing
            Me.btnOK.Enabled = False
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Other_QTY_Load")
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub rdbbyUserFree_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbbyUserFree.Click
        Try
            If (Me.rdbbyUserFree.Checked = True) Then
                Me.txtResult.ReadOnly = False
                Me.txtPercentage.ReadOnly = True
                Me.txtResult.Value = 0
                Me.txtResult.Focus()
                Me.txtResult.SelectAll()
                'Me.txtPercentage.ReadOnly = True
                Me.txtPercentage.Value = 0
                Me.txtPercentage.BackColor = Color.SandyBrown
                Me.txtResult.BackColor = Color.FromKnownColor(KnownColor.Window)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbbyPercentage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbbyPercentage.Click
        Try
            If (Me.rdbbyPercentage.Checked = True) Then
                Me.txtResult.ReadOnly = True
                Me.txtPercentage.ReadOnly = False
                Me.txtResult.Value = 0
                Me.txtResult.BackColor = Color.SandyBrown
                Me.txtPercentage.ReadOnly = False
                Me.txtPercentage.Value = 0
                Me.txtPercentage.SelectAll()
                Me.txtPercentage.Focus()
                Me.txtPercentage.BackColor = Color.FromKnownColor(KnownColor.Window)
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbbyPercentage_Click")
        End Try
    End Sub


    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim btn As DevComponents.DotNetBar.ButtonItem = CType(sender, DevComponents.DotNetBar.ButtonItem)
            Dim IsChecked As Boolean = btn.Checked
            For Each item As DevComponents.DotNetBar.ButtonItem In Me.ItemPanel1.Items
                item.Checked = False
            Next

            Dim TypeApp As String = "DD", Flag As String = "O"
            If Not IsChecked Then
                btn.Checked = True
                Select Case btn.Name
                    Case "btnDiscCBD"
                        Me.pnlTypeOfDisc.Visible = True
                        Me.pnlTypeOfDisc.Enabled = True
                        Me.pnlUncategorizedDisc.Visible = False
                        Me.txtDiscType.SelectAll()
                        Me.txtDiscType.Focus()
                        TypeApp = "CBD"
                        Flag = "OCBD"
                    Case "btnDiscDD"
                        Me.pnlTypeOfDisc.Visible = True
                        Me.pnlTypeOfDisc.Enabled = True
                        Me.pnlUncategorizedDisc.Visible = False
                        Me.txtDiscType.SelectAll()
                        Me.txtDiscType.Focus()
                        TypeApp = "DD"
                        Flag = "ODD"
                    Case "btnDiscDr"
                        Me.pnlTypeOfDisc.Visible = True
                        Me.pnlTypeOfDisc.Enabled = True
                        Me.pnlUncategorizedDisc.Visible = False
                        Me.txtDiscType.SelectAll()
                        Me.txtDiscType.Focus()
                        TypeApp = "DR"
                        Flag = "ODR"
                    Case "btnUncategorized"
                        Me.pnlTypeOfDisc.Visible = False
                        Me.pnlUncategorizedDisc.Visible = True
                        Me.pnlUncategorizedDisc.Enabled = True
                        TypeApp = "O"
                        Flag = "O"
                End Select
                Me.lblProgramInfo.Text = ""
                Me.lblProgramID.Text = ""
                Me.lblDiscInfo.Text = ""
                Dim info() As String = New String() {}
                If TypeApp <> "O" Then
                    Me.txtDiscType.Value = Me.ClsDisc.getDiscount(Me.OABrandPackID, TypeApp, Me.BrandPackID, Me.DistributorID, Me.PODate, Me.OAQty, Me.RefOther, info, True)
                    If info.Length > 0 Then
                        Me.lblProgramID.Text = info(0)
                        Me.lblProgramInfo.Text = info(1)
                        Me.lblDiscInfo.Text = info(2)
                    End If
                Else
                    Me.txtResult.Value = 0
                End If
                Me.btnOK.Enabled = True
                Me.AcceptButton = Me.btnOK
            Else
                If Me.pnlTypeOfDisc.Visible Then
                    Me.pnlTypeOfDisc.Enabled = False
                ElseIf Me.pnlUncategorizedDisc.Visible Then
                    Me.pnlUncategorizedDisc.Visible = False
                End If
                Me.AcceptButton = Nothing
                Me.btnOK.Enabled = False
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ItemPanel1_ItemClick")
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Public Overloads Function ShowDialog(ByRef resultQTy As Decimal, ByRef Flag As String, ByRef RefOther As Integer) As DialogResult
        If Me.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim dgResult As DialogResult = Windows.Forms.DialogResult.No
            'Dim resultQty As Decimal = 0, Flag As String = "O"
            resultQTy = Me.resultQty
            Flag = Me.Flag
            RefOther = Me.RefOther
            Return Me.DialogResult
        End If
    End Function
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim dgResult As DialogResult = Windows.Forms.DialogResult.No
        If Not Me.ValidateData() Then
            dgResult = Windows.Forms.DialogResult.No
        Else
            If Me.pnlTypeOfDisc.Visible Then
                If Me.btnDiscCBD.Checked Then
                    Me.Flag = "OCBD"
                    Me.resultQty = Me.txtDiscType.Value
                ElseIf Me.btnDiscDD.Checked Then
                    Me.Flag = "ODD"
                    resultQty = Me.txtDiscType.Value
                ElseIf Me.btnDiscDr.Checked Then
                    Me.Flag = "ODR"
                    Me.resultQty = Me.txtDiscType.Value
                End If
            ElseIf Me.pnlUncategorizedDisc.Visible Then
                Me.resultQty = Me.txtResult.Value
                Me.Flag = "O"
            End If
            dgResult = Windows.Forms.DialogResult.OK
        End If
        Me.DialogResult = dgResult
    End Sub
End Class