Imports System.Diagnostics
Public Class Filter

#Region "Deklarasi"
    Private myTable As Janus.Windows.GridEX.GridEXTable
#End Region

#Region "Event"
    Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        Try
            Dim myColumn As Janus.Windows.GridEX.GridEXColumn = CType(Me.cmbColumn.SelectedItem, Janus.Windows.GridEX.GridEXColumn)
            Dim myConditionOperator As Janus.Windows.GridEX.ConditionOperator = CType(Me.cmbFilter.SelectedItem, Janus.Windows.GridEX.ConditionOperator)
            Dim myFilter As Janus.Windows.GridEX.GridEXFilterCondition
            Dim Value1 = Nothing
            Dim value2 As Object = Nothing
            If Me.txtValue1.Text = "" Then
                Return
            End If
            If Me.grpTime.Enabled = True Then
                Value1 = Me.ParseText(Me.dtPicFrom.Value.ToShortDateString(), myColumn) 'CType(Me.dtPicFrom.Value.ToShortDateString(), Object)
                If Me.grpDateUntil.Enabled = True Then
                    value2 = Me.ParseText(Me.dtPicUntil.Value.ToShortDateString(), myColumn) 'CType(Me.dtPicUntil.Value.ToShortDateString(), Object)
                End If
            ElseIf Me.grpDataNumeric.Enabled = True Then
                If myColumn.Type.Equals(Type.GetType("System.Boolean")) Then
                    If Me.txtValue1.Text = "True" Or Me.txtValue1.Text = "true" Then
                        Value1 = CType(True, Object)
                    ElseIf Me.txtValue1.Text = "False" Or Me.txtValue1.Text = "false" Then
                        Value1 = CType(True, Object)
                    Else
                        MessageBox.Show("Type of Filtering for Boolean only 'True' or 'false'", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                Else
                    Value1 = Me.ParseText(Me.txtValue1.Text, myColumn)
                    If Me.txtValue2.Enabled = True Then
                        value2 = Me.ParseText(Me.txtValue2.Text, myColumn)
                    End If
                End If
            End If
            myFilter = New Janus.Windows.GridEX.GridEXFilterCondition(myColumn, myConditionOperator, Value1, value2)
            myTable.FilterCondition = myFilter
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            'Me.LogMyEvent(ex.Message, "btnFilter_Click")
        End Try
    End Sub

    Private Sub cmbFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFilter.SelectedIndexChanged
        If Me.cmbFilter.SelectedIndex = -1 Then
            Me.grpDataNumeric.Enabled = False
            Me.txtValue1.Text = ""
            Me.txtValue2.Text = ""
            Me.grpTime.Enabled = False
            Return
        End If
        Dim myConditionOperator As Janus.Windows.GridEX.ConditionOperator = CType(Me.cmbFilter.SelectedItem, Janus.Windows.GridEX.ConditionOperator)
        If (myConditionOperator = Janus.Windows.GridEX.ConditionOperator.Between) Or (myConditionOperator = Janus.Windows.GridEX.ConditionOperator.NotBetween) Then
            If Me.grpDataNumeric.Enabled = True Then
                Me.GroupBox1.Enabled = True
                Me.txtValue2.Enabled = True
            ElseIf Me.grpTime.Enabled = True Then
                Me.grpDateUntil.Enabled = True
                Me.dtPicUntil.Enabled = True
            End If
        Else
            Me.txtValue2.Enabled = False
            Me.dtPicUntil.Enabled = False
        End If
    End Sub

    Private Sub cmbColumn_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbColumn.TextChanged
        Try
            'if(this.myTable.Columns[this.cmbColumn.SelectedIndex].Type.Equals(Type.GetType("System.DateTime")))
            '			{
            '				this.grpDataTextNumeric.Enabled = false;
            '				this.grpTime.Enabled = true;
            '				this.grpUntilDateTime.Enabled = true;
            '				this.dtPicUntil.Enabled = true;
            '			}
            '            Else
            '			{
            '				this.grpTime.Enabled = false;
            '				this.grpDataTextNumeric.Enabled = true;
            '                this.grpUntilD()
            If Me.cmbColumn.Text = "" Then
                Me.cmbFilter.Text = ""
                Me.cmbFilter.SelectedIndex = -1
                Me.cmbFilter.Enabled = False
                Return
            End If
            Me.cmbFilter.Enabled = True
            If Me.myTable.Columns(Me.cmbColumn.SelectedIndex).Type.Equals(Type.GetType("System.DateTime")) Then
                Me.grpTime.Enabled = True
                Me.grpDataNumeric.Enabled = False
                Me.grpDateFrom.Enabled = True
                Me.grpDateUntil.Enabled = False
            Else
                Me.grpTime.Enabled = False
                Me.grpDataNumeric.Enabled = True
                Me.grpDateUntil.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Me.LogMyEvent(ex.Message, "cmbColumn_TextChanged")
        End Try
    End Sub
    Private Sub txtValue1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValue1.KeyPress
        Try
            'Dim myColumn As Boolean = Type.Equals("System.Boolean")
            'If myColumn.Type.Equals(Type.GetType("System.Boolean")) Then
            '    If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
            '        e.Handled = True
            '    End If
            'End If
            If Me.cmbColumn.SelectedIndex = -1 Or Me.cmbColumn.Text = "Icon" Then
                MessageBox.Show("Please Define the column", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim myColumn As Janus.Windows.GridEX.GridEXColumn = CType(Me.cmbColumn.SelectedItem, Janus.Windows.GridEX.GridEXColumn)
            If myColumn.Type.Equals(Type.GetType("System.Int16")) _
            Or myColumn.Type.Equals(Type.GetType("System.Int32")) _
            Or myColumn.Type.Equals(Type.GetType("System.Int64")) _
            Or myColumn.Type.Equals(Type.GetType("System.Double")) _
            Or myColumn.Type.Equals(Type.GetType("System.Decimal")) Then
                If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
                ElseIf e.KeyChar = "." And txtValue1.Text.IndexOf(".") = -1 And txtValue1.Text.IndexOf(",") = -1 Then
                ElseIf e.KeyChar = "," And txtValue1.Text.IndexOf(",") = -1 And txtValue1.Text.IndexOf(".") = -1 Then
                Else
                    e.Handled = True
                End If
            ElseIf myColumn.Type.Equals(Type.GetType("System.Boolean")) Then
                If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Me.LogMyEvent(ex.Message, "txtValue1_KeyPress")
        End Try
    End Sub

    '		private void grpTime_EnabledChanged(object sender, System.EventArgs e)
    '		{
    '			if(this.grpTime.Enabled == true)
    '			{
    '				this.grpFromDateTime.Enabled = true;
    '				this.grpUntilDateTime.Enabled = false;
    '			}
    '		}
    Private Sub grpDataNumeric_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpDataNumeric.EnabledChanged
        If Me.grpDataNumeric.Enabled = True Then
            Me.grpValue1.Enabled = True
            Me.GroupBox1.Enabled = True
        Else
            Me.grpValue1.Enabled = False
            Me.GroupBox1.Enabled = False
        End If
    End Sub

    Private Sub grpTime_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpTime.EnabledChanged
        If Me.grpTime.Enabled = True Then
            Me.grpDateFrom.Enabled = True
            Me.grpDateUntil.Enabled = True
        Else
            Me.grpDateFrom.Enabled = False
            Me.grpDateUntil.Enabled = False
        End If
    End Sub
    Private Sub txtValue2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValue2.KeyPress
        Try
            'Dim myColumn As Boolean = Type.Equals("System.Boolean")
            'If myColumn.Type.Equals(Type.GetType("System.Boolean")) Then
            '    If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
            '        e.Handled = True
            '    End If
            'End If
            If Me.cmbColumn.SelectedIndex = -1 Or Me.cmbColumn.Text = "Icon" Then
                MessageBox.Show("Please Define the column", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim myColumn As Janus.Windows.GridEX.GridEXColumn = CType(Me.cmbColumn.SelectedItem, Janus.Windows.GridEX.GridEXColumn)
            If myColumn.Type.Equals(Type.GetType("System.Int16")) _
            Or myColumn.Type.Equals(Type.GetType("System.Int32")) _
            Or myColumn.Type.Equals(Type.GetType("System.Int64")) _
            Or myColumn.Type.Equals(Type.GetType("System.Double")) _
            Or myColumn.Type.Equals(Type.GetType("System.Decimal")) Then
                If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
                ElseIf e.KeyChar = "." And txtValue2.Text.IndexOf(".") = -1 And txtValue2.Text.IndexOf(",") = -1 Then
                ElseIf e.KeyChar = "," And txtValue2.Text.IndexOf(",") = -1 And txtValue2.Text.IndexOf(".") = -1 Then
                Else
                    e.Handled = True
                End If
            ElseIf myColumn.Type.Equals(Type.GetType("System.Boolean")) Then
                If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Me.LogMyEvent(ex.Message, "txtValue1_KeyPress")
        End Try
    End Sub
#End Region

#Region "Sub"
    Protected Sub LogMyEvent(ByVal Message As String, ByVal NamaEvent As String)
        Try
            If Not EventLog.SourceExists("AppException") Then
                EventLog.CreateEventSource("AppException", "Nufarm")
            End If
            EventLog.WriteEntry("AppException", "Date  " + System.DateTime.Now.ToShortDateString() + ": On Hour " + DateTime.Now.Hour.ToString() & _
            ":" & DateTime.Now.Minute.ToString() + " Error = " + Message + " Event = " + NamaEvent, EventLogEntryType.Error)
        Catch ex As Exception

        End Try
    End Sub
    Private Function ParseText(ByVal text As String, ByVal column As Janus.Windows.GridEX.GridEXColumn) As Object
        If column.Type.Equals(Type.GetType("System.Int32")) Then
            Return Int32.Parse(text)
        ElseIf column.Type.Equals(Type.GetType("System.DateTime")) Then
            Return DateTime.Parse(text)
        ElseIf column.Type.Equals(Type.GetType("System.Int16")) Then
            Return Int16.Parse(text)
        ElseIf column.Type.Equals(Type.GetType("System.Double")) Then
            Return Double.Parse(text)
        Else
            Return text
        End If
    End Function
    Private Sub FillOperatorCombo()
        Dim myArray As System.Array = System.Enum.GetValues(Janus.Windows.GridEX.ConditionOperator.Equal.GetType())
        Me.cmbFilter.DataSource = myArray
    End Sub
    Friend Sub ReadData(ByVal Table As Janus.Windows.GridEX.GridEXTable)
        Try
            Me.myTable = Table
            Me.cmbColumn.DataSource = myTable.Columns
            Me.cmbColumn.DisplayMember = "Caption"
            Me.FillOperatorCombo()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Me.LogMyEvent(ex.Message, "Sub Show")
        End Try
    End Sub

#End Region

End Class