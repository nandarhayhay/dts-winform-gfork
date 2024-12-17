Public Class OtherDDDR
    Private m_clsDisc As NufarmBussinesRules.Brandpack.DiscountPrice
    Friend BrandPackID As String = ""
    Friend DistributorID As String = ""
    Friend OAQty As Decimal = 0
    Friend PODate As DateTime = DateTime.Now
    Friend TypeApp As String = ""
    Friend BrandPackName As String = ""
    Friend Devided_Qty As Decimal = 0
    Private FullSize As New Size(628, 360)
    Private medSize As New Size(389, 233)
    Private Flag As String = ""
    Friend OABrandPackID As String = ""
    Friend PricePrQty As Decimal = 0
    Dim tblResult As New DataTable("T_Result")

    Private ReadOnly Property ClsDisc() As NufarmBussinesRules.Brandpack.DiscountPrice
        Get
            If IsNothing(Me.m_clsDisc) Then
                Me.m_clsDisc = New NufarmBussinesRules.Brandpack.DiscountPrice()
            End If
            Return Me.m_clsDisc
        End Get
    End Property
    Friend Event txtPercentage_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Private Sub OtherDDDR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim Flag As String = "O"
            If Me.btnDiscCBD.Checked = True Or Me.btnDiscDD.Checked = True Or Me.btnDiscDr.Checked = True Or Me.btnDiscDK.Checked = True Or Me.btnDiscDP.Checked = True Then
                If btnDiscCBD.Checked Then
                    Me.pnlUncategorizedDisc.Enabled = False
                    Me.TypeApp = "CBD"
                    Flag = "OCBD"
                ElseIf btnDiscDD.Checked Then
                    Me.pnlUncategorizedDisc.Enabled = False
                    Me.TypeApp = "DD"
                    Flag = "ODD"
                ElseIf btnDiscDr.Checked Then
                    Me.pnlUncategorizedDisc.Enabled = False
                    Me.TypeApp = "DR"
                    Flag = "ODR"
                ElseIf btnDiscDK.Checked Then
                    Me.pnlUncategorizedDisc.Enabled = False
                    Me.TypeApp = "DK"
                    Flag = "ODK"
                ElseIf Me.btnDiscDP.Checked Then
                    Me.pnlUncategorizedDisc.Enabled = False
                    Me.TypeApp = "DP"
                    Flag = "ODP"
                    'Me.GridEX1.SetDataBinding(Nothing, "")
                    'Me.AcceptButton = Me.btnOK
                    'Me.CancelButton = Me.btnCancel
                End If
                'get discount
                Dim dt As DataTable = Me.ClsDisc.getDiscount(Me.TypeApp, Me.BrandPackID, Me.BrandPackName, Me.DistributorID, Me.PODate, Me.OAQty, Me.OABrandPackID, True)
                Me.GridEX1.SetDataBinding(dt.DefaultView(), "")
                Me.ClearControl(pnlUncategorizedDisc)
                Me.pnlUncategorizedDisc.Enabled = False
                Me.AcceptButton = Me.btnOK
                Me.CancelButton = Me.btnCancel
            ElseIf Me.btnUncategorized.Checked Then
                Me.GridEX1.SetDataBinding(Nothing, "")
                Me.pnlUncategorizedDisc.Enabled = True
                Me.TypeApp = "O"
                Flag = "O"
                Me.AcceptButton = Me.btnOK
                Me.CancelButton = Me.btnCancel
            Else
                Me.GridEX1.SetDataBinding(Nothing, "")
                Me.ClearControl(pnlUncategorizedDisc)
                Me.pnlUncategorizedDisc.Enabled = False
                'Me.pnlUncategorizedDisc.Enabled = False
                Me.AcceptButton = Nothing
                Me.CancelButton = Me.btnCancel
                Me.btnOK.Visible = False
                Me.btnCancel.Visible = True
            End If
        Catch ex As Exception
            Me.pnlUncategorizedDisc.Enabled = False
            Me.AcceptButton = Nothing
            Me.btnOK.Enabled = False
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_OtherDDDR_Load")
        End Try
    End Sub

    Private Sub rdbbyUserFree_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbbyUserFree.Click
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
    End Sub

    Private Sub rdbbyPercentage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbbyPercentage.Click
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
    End Sub

    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        If Me.GridEX1.DataSource Is Nothing Then
            Return
        End If
        If Me.GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
            Return
        End If
        Me.GridEX1.GetRow().CheckState = Janus.Windows.GridEX.RowCheckState.Checked
        Me.GridEX1.SetValue(0, True)
        Me.GridEX1.UpdateData()
        Me.btnOK_Click(Me.btnOK, New EventArgs())

    End Sub

    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim btn As DevComponents.DotNetBar.ButtonItem = CType(sender, DevComponents.DotNetBar.ButtonItem)
            Dim IsChecked As Boolean = btn.Checked
            For Each item As DevComponents.DotNetBar.ButtonItem In Me.ItemPanel1.Items
                item.Checked = False
            Next
            If Not IsChecked Then
                btn.Checked = True
                Select Case btn.Name
                    Case "btnDiscCBD"
                        Me.pnlUncategorizedDisc.Enabled = False
                        TypeApp = "CBD"
                        Flag = "OCBD"
                    Case "btnDiscDD"
                        Me.pnlUncategorizedDisc.Enabled = False
                        TypeApp = "DD"
                        Flag = "ODD"
                    Case "btnDiscDr"
                        Me.pnlUncategorizedDisc.Enabled = False
                        TypeApp = "DR"
                        Flag = "ODR"
                    Case "btnDiscDK"
                        Me.pnlUncategorizedDisc.Enabled = False
                        TypeApp = "DK"
                        Flag = "ODK"
                    Case "btnDiscDP"
                        Me.pnlUncategorizedDisc.Enabled = False
                        TypeApp = "DP"
                        Flag = "ODP"
                    Case "btnUncategorized"
                        Me.pnlUncategorizedDisc.Enabled = True
                        TypeApp = "O"
                        Flag = "O"
                End Select
                If Me.TypeApp = "O" And Flag = "O" Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.ClearControl(pnlUncategorizedDisc)
                    Me.pnlUncategorizedDisc.Enabled = True
                ElseIf TypeApp <> "" Then
                    'get discount
                    Dim dt As DataTable = Me.ClsDisc.getDiscount(Me.TypeApp, Me.BrandPackID, Me.BrandPackName, Me.DistributorID, Me.PODate, Me.OAQty, Me.OABrandPackID, True)
                    Me.GridEX1.SetDataBinding(dt.DefaultView(), "")
                    Me.pnlUncategorizedDisc.Enabled = False
                    Me.GridEX1.Visible = True
                End If
                Me.btnOK.Visible = True
                Me.btnCancel.Visible = True
                Me.btnOK.Enabled = True
                Me.AcceptButton = Me.btnOK
                Me.CancelButton = Me.btnCancel
            Else
                Me.GridEX1.SetDataBinding(Nothing, "")
                Me.txtPercentage.Value = 0
                Me.txtResult.Value = 0
                Me.rdbbyPercentage.Checked = False
                Me.rdbbyUserFree.Checked = False
                Me.AcceptButton = Nothing
                Me.btnOK.Enabled = False
                Me.btnOK.Visible = False
                Me.btnCancel.Visible = True
            End If
        Catch ex As Exception
            Me.txtPercentage.Value = 0
            Me.txtResult.Value = 0
            Me.rdbbyPercentage.Checked = False
            Me.rdbbyUserFree.Checked = False
            Me.AcceptButton = Nothing
            Me.btnOK.Enabled = False
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ItemPanel1_ItemClick")
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub txtPercentage_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPercentage.ValueChanged
        RaiseEvent txtPercentage_Changed(sender, e)
    End Sub
    Public Overloads Function ShowDialog(ByRef Result As DataTable, ByRef refFlag As String) As DialogResult
        If Me.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim dgResult As DialogResult = Windows.Forms.DialogResult.No
            'Dim resultQty As Decimal = 0, Flag As String = "O"
            If Me.Flag = "O" Then
                With Me.tblResult
                    .Clear()
                    .Columns.Add("OA_BRANDPACK_ID", Type.GetType("System.String"))
                    .Columns("OA_BRANDPACK_ID").DefaultValue = Me.OABrandPackID
                    .Columns.Add("PRICE_PRQTY", Type.GetType("System.String"))
                    .Columns("PRICE_PRQTY").DefaultValue = Me.PricePrQty
                    .Columns.Add("FK_BRND_DISC_PROG", Type.GetType("System.Int32"))
                    .Columns("FK_BRND_DISC_PROG").DefaultValue = 0
                    .Columns.Add("INC_DISC", Type.GetType("System.Decimal"))
                    .Columns("INC_DISC").DefaultValue = 0
                    .Columns.Add("Flag", Type.GetType("System.String"))
                    Dim row As DataRow = .NewRow()
                    row.BeginEdit()
                    row("PRICE_PRQTY") = Me.PricePrQty
                    row("OA_BRANDPACK_ID") = Me.OABrandPackID
                    row("INC_DISC") = Me.txtResult.Value
                    row("FLAG") = "O"
                    row.EndEdit()
                    .Rows.Add(row)
                    .AcceptChanges()
                End With
            End If
            Result = Me.tblResult
            refFlag = Me.Flag
            Return Me.DialogResult
        End If
    End Function
    Private Function ValidateData() As Boolean
        If Me.pnlUncategorizedDisc.Enabled Then 'typeApp= O
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
        Else
            If Me.GridEX1.SelectedItems Is Nothing Then
                Me.ShowMessageInfo("Please select which data to fecth")
                Return False
            ElseIf Me.GridEX1.SelectedItems.Count <= 0 Then
                Me.ShowMessageInfo("Please select which data to fecth")
                Return False
            ElseIf Me.GridEX1.Row <= -1 Then
                Me.ShowMessageInfo("Please select which data to fecth")
                Return False
            ElseIf Me.GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
                Me.ShowMessageInfo("Please select which data to fecth")
                Return False
            End If
        End If
        Return True
    End Function
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim dgResult As DialogResult = Windows.Forms.DialogResult.No
        If Not Me.ValidateData() Then
            dgResult = Windows.Forms.DialogResult.No
        Else
            If Me.pnlUncategorizedDisc.Enabled = False Then
                If Me.btnDiscCBD.Checked Then
                    Me.Flag = "OCBD"
                    'Me.resultQty = Me.txtDiscType.Value
                ElseIf Me.btnDiscDD.Checked Then
                    Me.Flag = "ODD"
                    'resultQty = Me.txtDiscType.Value
                ElseIf Me.btnDiscDr.Checked Then
                    Me.Flag = "ODR"
                    'Me.resultQty = Me.txtDiscType.Value
                ElseIf Me.btnDiscDK.Checked Then
                    Me.Flag = "ODK"
                ElseIf Me.btnDiscDP.Checked Then
                    Me.Flag = "ODP"
                End If
                With Me.tblResult
                    With Me.tblResult
                        .Clear()
                        .Columns.Add("OA_BRANDPACK_ID", Type.GetType("System.String"))
                        .Columns("OA_BRANDPACK_ID").DefaultValue = Me.OABrandPackID
                        .Columns.Add("PRICE_PRQTY", Type.GetType("System.String"))
                        .Columns("PRICE_PRQTY").DefaultValue = Me.PricePrQty
                        .Columns.Add("FK_BRND_DISC_PROG", Type.GetType("System.Int32"))
                        .Columns("FK_BRND_DISC_PROG").DefaultValue = 0
                        .Columns.Add("INC_DISC", Type.GetType("System.Decimal"))
                        .Columns("INC_DISC").DefaultValue = 0
                        .Columns.Add("FLAG", Type.GetType("System.String"))
                        Dim Rowschecked() As Janus.Windows.GridEX.GridEXRow = Me.GridEX1.GetCheckedRows()
                        For index As Integer = 0 To Rowschecked.Length - 1
                            Dim FK_BRND_DISC_PROG = CInt(Rowschecked(index).Cells("FK_BRND_DISC_PROG").Value)
                            Dim resultQty = Rowschecked(index).Cells("INC_DISC").Value
                            Dim row As DataRow = .NewRow()
                            row.BeginEdit()
                            row("OA_BRANDPACK_ID") = Me.OABrandPackID
                            row("PRICE_PRQTY") = Me.PricePrQty
                            row("FK_BRND_DISC_PROG") = FK_BRND_DISC_PROG
                            row("INC_DISC") = resultQty
                            row("FLAG") = Me.Flag
                            row.EndEdit()
                            .Rows.Add(row)
                        Next
                        .AcceptChanges()
                    End With
                End With
            ElseIf Me.pnlUncategorizedDisc.Visible Then
                Me.Flag = "O"
            End If
            dgResult = Windows.Forms.DialogResult.OK
        End If
        Me.DialogResult = dgResult
    End Sub
End Class
