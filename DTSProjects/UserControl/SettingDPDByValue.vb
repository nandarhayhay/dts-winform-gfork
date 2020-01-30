Public Class SettingDPDByValue
    Private IsReadingSetting As Boolean = True
    Public MainParent As Settings
    Private clsSetting As New NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting()
    Dim isLoadingRow As Boolean = False

    Private Sub SettingDPDByValue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'get data bizRules
        Try
            Cursor = Cursors.WaitCursor
            Dim frmSet As Settings = CType(MainParent, Settings)
            frmSet.dtSetDPD = Me.clsSetting.getDPDByVal(True)
            Me.GridEX1.SetDataBinding(frmSet.dtSetDPD.DefaultView(), "")
            Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        Try
            'INSERT INTO RefBussinesRules(CodeApp,NameApp,TypeApp,DescriptionApp,ParamValue,ParamValueType,AllowRules,CreatedDate,CreatedBy,START_DATE,END_DATE,ValueNumeric) " & vbCrLf & _
            '                            " VALUES(@CodeApp,'SETTING ACH DPD TO VALUE','DPDToValue','SETTING PENCAPAIAN DPD BY VALUE, TOTAL YANG BISA MENDAPATKAN DISCOUNT',@FLAG,'VARCHAR',1,CONVERT(VARCHAR(100),GETDATE(),101),@CreatedBy,@START_DATE,@END_DATE,@ValueNumeric);
            'check startDate
            Me.Cursor = Cursors.WaitCursor
            Dim startDate As Object = Me.GridEX1.GetValue("START_DATE")
            If IsNothing(startDate) Or IsDBNull(startDate) Then
                MessageBox.Show("Please enter StartDate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                e.Cancel = True : Me.Cursor = Cursors.Default : Return
            End If
            Dim endDate As Object = Me.GridEX1.GetValue("END_DATE")
            If IsNothing(endDate) Or IsDBNull(endDate) Then
                MessageBox.Show("Please enter EndDate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                e.Cancel = True : Me.Cursor = Cursors.Default : Return
            End If
            Dim flag As Object = Me.GridEX1.GetValue("FLAG")
            If IsNothing(flag) Or IsDBNull(flag) Then
                MessageBox.Show("Please enter Flag", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                e.Cancel = True : Me.Cursor = Cursors.Default : Return
            End If
            If String.IsNullOrEmpty(flag) Then
                MessageBox.Show("Please enter Flag", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                e.Cancel = True : Me.Cursor = Cursors.Default : Return
            End If
            Dim valnum As Object = Me.GridEX1.GetValue("ACHIEVEMENT")
            If IsNothing(valnum) Or IsDBNull(valnum) Then
                MessageBox.Show("Please enter achievement", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                e.Cancel = True : Me.Cursor = Cursors.Default : Return
            End If
            If Convert.ToDecimal(Me.GridEX1.GetValue("ACHIEVEMENT")) <= 0 Then
                MessageBox.Show("Please enter achievement", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                e.Cancel = True : Me.Cursor = Cursors.Default : Return
            End If
            'created CodeApp StartYer + EndYear + Flag
            Dim CodeApp As String = CStr(Convert.ToDateTime(startDate).Year) + "-" + CStr(Convert.ToDateTime(endDate).Year) + flag.ToString().ToUpper()
            Me.GridEX1.SetValue("CodeApp", CodeApp)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            e.Cancel = True
            MessageBox.Show(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_CellValueChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellValueChanged
        If isLoadingRow Then : Return : End If
        Try
            If Me.GridEX1.GetRow.RowType() = Janus.Windows.GridEX.RowType.Record Then
                Select Case e.Column.Key
                    Case "START_DATE"
                        Dim startDate As Object = Me.GridEX1.GetValue(e.Column)
                        If IsNothing(startDate) Or IsDBNull(startDate) Then
                            MessageBox.Show("Please enter StartDate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.GridEX1.CancelCurrentEdit() : Me.Cursor = Cursors.Default : Return
                        End If
                    Case "END_DATE"
                        Dim endDate As Object = Me.GridEX1.GetValue(e.Column)
                        If IsNothing(endDate) Or IsDBNull(endDate) Then
                            MessageBox.Show("Please enter EndDate", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.GridEX1.CancelCurrentEdit() : Me.Cursor = Cursors.Default : Return
                        End If
                    Case "FLAG"
                        Dim flag As Object = Me.GridEX1.GetValue(e.Column)
                        If IsNothing(flag) Or IsDBNull(flag) Then
                            MessageBox.Show("Please enter Flag", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.GridEX1.CancelCurrentEdit() : Me.Cursor = Cursors.Default : Return
                        End If
                        If String.IsNullOrEmpty(flag) Then
                            MessageBox.Show("Please enter Flag", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.GridEX1.CancelCurrentEdit() : Me.Cursor = Cursors.Default : Return
                        End If
                    Case "ACHIEVEMENT"
                        Dim valnum As Object = Me.GridEX1.GetValue(e.Column)
                        If IsNothing(valnum) Or IsDBNull(valnum) Then
                            MessageBox.Show("Please enter achievement", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.GridEX1.CancelCurrentEdit() : Me.Cursor = Cursors.Default : Return
                        End If
                        If Convert.ToDecimal(Me.GridEX1.GetValue(e.Column)) <= 0 Then
                            MessageBox.Show("Please enter achievement", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.GridEX1.CancelCurrentEdit() : Me.Cursor = Cursors.Default : Return
                        End If
                End Select
                Me.GridEX1.UpdateData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Me.GridEX1.CancelCurrentEdit()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.clsSetting.hasUsed(Convert.ToDateTime(Me.GridEX1.GetValue("START_DATE")), Convert.ToDateTime(Me.GridEX1.GetValue("END_DATE")), Me.GridEX1.GetValue("FLAG").ToString()) Then
                MessageBox.Show("can not delete data" & vbCrLf & "Data has been used in DPD", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
                e.Cancel = True : Me.Cursor = Cursors.Default : Return
            End If
            If MessageBox.Show("Are you sure you want to delete data ?", "Action can not be undone", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                e.Cancel = True : Me.Cursor = Cursors.Default : Return
            End If
            e.Cancel = False
            Me.GridEX1.UpdateData()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            e.Cancel = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RecordAdded
        Me.GridEX1.UpdateData()
    End Sub
End Class
