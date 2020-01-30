Imports System.Diagnostics
Public Class SettingGrid

#Region "Deklarasi"
    Friend Grid As Janus.Windows.GridEX.GridEX
    Friend GridExPrintDock As Janus.Windows.GridEX.GridEXPrintDocument
#End Region

#Region "Sub"
    '  private void FillComboWithEnumValues(ComboBox combo,Type enumType)
    '{
    '	System.Array values = Enum.GetValues(enumType);
    '	object[] styles = new object[values.Length];
    '	values.CopyTo(styles,0);
    '	combo.Items.AddRange(styles);
    '}
    Private Sub FillComboColumn()
        Me.cmbColumn.Items.Clear()
        For i As Integer = 0 To Me.Grid.RootTable.Columns.Count - 1
            If Me.Grid.RootTable.Columns(i).Caption = "Icon" Then
            Else
                If Me.Grid.RootTable.Columns(i).Visible = True Then
                    Me.cmbColumn.Items.Add(Me.Grid.RootTable.Columns(i).Key)
                End If
            End If
        Next
        'If Me.Grid.RootTable.ChildTables().Count > 0 Then
        '    For i As Integer = 0 To Me.Grid.RootTable.ChildTables(0).Columns.Count - 1
        '        Me.cmbColumn.Items.Add(Me.Grid.RootTable.ChildTables(0).Columns(i).DataMember)
        '    Next
        'End If
    End Sub
    Private Sub FillComboWithEnumValues(ByVal Combo As ComboBox, ByVal enumType As Type)
        Dim values As System.Array = System.Enum.GetValues(enumType)
        Dim styles(values.Length - 1) As Object
        values.CopyTo(styles, 0)
        Combo.Items.AddRange(styles)
    End Sub
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
#End Region
    Private Sub chkkRecordNavigator_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkkRecordNavigator.CheckedChanged
        If Me.chkkRecordNavigator.Checked = True Then
            Me.Grid.RecordNavigator = True
        Else
            Me.Grid.RecordNavigator = False
        End If
    End Sub
    Private Sub chkTotalRow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTotalRow.Click
        If Me.chkTotalRow.Checked = True Then
            Me.Grid.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
        Else
            Me.Grid.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
        End If
    End Sub

    Private Sub chkRowHeaders_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRowHeaders.CheckedChanged
        If Me.chkRowHeaders.Checked = True Then
            Me.Grid.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True
        Else
            Me.Grid.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.False
        End If
    End Sub

    Private Sub chkAlternatingColumn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAlternatingColumn.CheckedChanged
        If Me.chkAlternatingColumn.Checked = True Then
            Me.Grid.AlternatingColors = True
        Else
            Me.Grid.AlternatingColors = False
        End If
    End Sub

    Private Sub cmbGridLine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGridLine.SelectedIndexChanged
        If Me.cmbGridLine.SelectedIndex <> -1 Then
            Me.Grid.GridLines = CType(Me.cmbGridLine.SelectedItem, Janus.Windows.GridEX.GridLines)
        End If
    End Sub

    Private Sub cmbGridLineStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGridLineStyle.SelectedIndexChanged
        If Me.cmbGridLineStyle.SelectedIndex <> -1 Then
            Me.Grid.GridLineStyle = CType(Me.cmbGridLineStyle.SelectedItem, Janus.Windows.GridEX.GridLineStyle)
        End If
    End Sub

    'Private Sub txtHeaderDistance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
    '        e.Handled = True
    '    End If

    'End Sub

    'Private Sub txtFooterDistance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    Try
    '        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
    '        Else
    '            e.Handled = True
    '            Return
    '        End If
    '        Me.GridExPrintDock.FooterDistance = CInt(Me.txtFooterDistance.Text.Trim())
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub chkTranslate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTranslate.CheckedChanged
        If Not IsNothing(Me.GridExPrintDock) Then
            If Me.chkTranslate.Checked = True Then
                Me.GridExPrintDock.TranslateSystemColors = True
            Else
                Me.GridExPrintDock.TranslateSystemColors = False
            End If
        End If

    End Sub

    Private Sub chkRepeatHeader_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRepeatHeader.CheckedChanged
        If Not IsNothing(Me.GridExPrintDock) Then
            If Me.chkRepeatHeader.Checked = True Then
                Me.GridExPrintDock.RepeatHeaders = True
            Else
                Me.GridExPrintDock.RepeatHeaders = False
            End If
        End If
    End Sub

    Private Sub chkExpand_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExpand.CheckedChanged
        If Not IsNothing(Me.GridExPrintDock) Then
            If Me.chkExpand.Checked = True Then
                Me.GridExPrintDock.ExpandFarColumn = True
            Else
                Me.GridExPrintDock.ExpandFarColumn = False
            End If
        End If
    End Sub

    Private Sub chkOriginMargin_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOriginMargin.CheckedChanged
        If Not IsNothing(Me.GridExPrintDock) Then
            If Me.chkOriginMargin.Checked = True Then
                Me.GridExPrintDock.OriginAtMargins = True
            Else
                Me.GridExPrintDock.OriginAtMargins = False
            End If
        End If
    End Sub

    Private Sub cmbFitColumns_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFitColumns.SelectedIndexChanged
        If Not IsNothing(Me.GridExPrintDock) Then
            If Me.cmbFitColumns.SelectedIndex <> -1 Then
                Me.GridExPrintDock.FitColumns = CType(Me.cmbFitColumns.SelectedIndex, Janus.Windows.GridEX.FitColumnsMode)
            End If
        End If
    End Sub

    Private Sub SettingGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.FillComboWithEnumValues(Me.cmbAgregate, GetType(Janus.Windows.GridEX.AggregateFunction))
            Me.FillComboWithEnumValues(Me.cmbGridLine, GetType(Janus.Windows.GridEX.GridLines))
            Me.FillComboWithEnumValues(Me.cmbGridLineStyle, GetType(Janus.Windows.GridEX.GridLineStyle))
            Me.FillComboWithEnumValues(Me.cmbGroupInterval, GetType(Janus.Windows.GridEX.GroupInterval))
            Me.FillComboWithEnumValues(Me.cmbTotalRowPosition, GetType(Janus.Windows.GridEX.TotalRowPosition))
            Me.FillComboWithEnumValues(Me.cmbVisualStyle, GetType(Janus.Windows.GridEX.VisualStyle))
            Me.FillComboWithEnumValues(Me.cmbColorScheme, GetType(Janus.Windows.GridEX.Office2007ColorScheme))
            Me.FillComboWithEnumValues(Me.cmbGroupInterval, GetType(Janus.Windows.GridEX.GroupInterval))
            Me.FillComboColumn()
            Me.chkAlternatingColumn.Checked = Me.Grid.AlternatingColors
            Me.chkkRecordNavigator.Checked = Me.Grid.RecordNavigator
            If Me.Grid.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True Then
                Me.chkRowHeaders.Checked = True
            Else
                Me.chkRowHeaders.Checked = False
            End If
            If Me.Grid.GroupByBoxVisible = True Then
                Me.chkGroupbyBox.Checked = True
            Else
                Me.chkGroupbyBox.Checked = False
            End If
            If Me.Grid.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True Then
                Me.chkTotalRow.Checked = True
            Else
                Me.chkTotalRow.Checked = False
            End If

            If Not IsNothing(Me.GridExPrintDock) Then
                Me.chkExpand.Checked = Me.GridExPrintDock.ExpandFarColumn
                Me.chkOriginMargin.Checked = Me.GridExPrintDock.OriginAtMargins
                Me.chkRepeatHeader.Checked = Me.GridExPrintDock.RepeatHeaders
                Me.chkTranslate.Checked = Me.GridExPrintDock.TranslateSystemColors
                Me.txtFooterDistance.Text = Me.GridExPrintDock.FooterDistance
                Me.txtHeaderDistance.Text = Me.GridExPrintDock.HeaderDistance
                Me.txtPageFooterCenter.Text = Me.GridExPrintDock.PageFooterCenter
                Me.txtPageFooterLeft.Text = Me.GridExPrintDock.PageFooterLeft
                Me.txtPageFooterRight.Text = Me.GridExPrintDock.PageFooterRight
                Me.txtPageHeaderCenter.Text = Me.GridExPrintDock.PageHeaderCenter
                Me.txtPageHeaderLeft.Text = Me.GridExPrintDock.PageHeaderLeft
                Me.txtPageHeaderRight.Text = Me.GridExPrintDock.PageHeaderRight
                Me.cmbFitColumns.SelectedIndex = CType(Me.GridExPrintDock.FitColumns, Object)
            Else
                Me.chkExpand.Checked = False
                Me.chkOriginMargin.Checked = False
                Me.chkRepeatHeader.Checked = False
                Me.chkTranslate.Checked = False : Me.chkTranslate.Enabled = False
                Me.txtFooterDistance.Text = Me.GridExPrintDock.FooterDistance
                Me.txtHeaderDistance.Text = Me.GridExPrintDock.HeaderDistance
                Me.txtPageFooterCenter.Text = Me.GridExPrintDock.PageFooterCenter
                Me.txtPageFooterLeft.Text = Me.GridExPrintDock.PageFooterLeft
                Me.txtPageFooterRight.Text = Me.GridExPrintDock.PageFooterRight
                Me.txtPageHeaderCenter.Text = Me.GridExPrintDock.PageHeaderCenter
                Me.txtPageHeaderLeft.Text = Me.GridExPrintDock.PageHeaderLeft
                Me.txtPageHeaderRight.Text = Me.GridExPrintDock.PageHeaderRight
                Me.cmbFitColumns.SelectedIndex = CType(Me.GridExPrintDock.FitColumns, Object)
            End If
            Me.cmbGridLine.SelectedItem = CType(Me.Grid.GridLines, Object)
            Me.cmbGridLineStyle.SelectedItem = CType(Me.Grid.GridLineStyle, Object)
            Me.cmbVisualStyle.SelectedItem = CType(Me.Grid.VisualStyle, Object)
            Me.cmbColorScheme.SelectedItem = CType(Me.Grid.Office2007ColorScheme, Object)
            Me.cmbTotalRowPosition.SelectedItem = CType(Me.Grid.TotalRowPosition, Object)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Me.LogMyEvent(ex.Message, "SettingGrid_Load")
        End Try
    End Sub

    'Private Sub txtHeaderDistance_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHeaderDistance.TextChanged
    '    If Not IsNothing(Me.GridExPrintDock) Then
    '        If IsNumeric(Me.txtHeaderDistance.Text) Then
    '            Me.GridExPrintDock.HeaderDistance = CInt(Me.txtHeaderDistance.Text)
    '        End If
    '    End If

    'End Sub

    Private Sub txtFooterDistance_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFooterDistance.TextChanged
        Try
            Me.GridExPrintDock.FooterDistance = CInt(Me.txtFooterDistance.Text)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmbAgregate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAgregate.SelectedIndexChanged
        If (Me.cmbColumn.Text = "") Or (Me.cmbColumn.SelectedIndex = -1) Then
            Return
        End If
        If Me.cmbAgregate.SelectedIndex = -1 Then
            Return
        End If
        Me.Grid.RootTable.Columns(Me.cmbColumn.Text).AggregateFunction = CType(Me.cmbAgregate.SelectedItem, Janus.Windows.GridEX.AggregateFunction)
    End Sub

    Private Sub cmbGroupInterval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGroupInterval.SelectedIndexChanged
        Try
            If Me.cmbGroupInterval.SelectedIndex = -1 Then
                Return
            End If
            If (Me.cmbGroupInterval.Text = "") Or (Me.cmbGroupInterval.SelectedIndex = -1) Then
                Return
            End If
            If (CType(Me.cmbGroupInterval.SelectedItem, Janus.Windows.GridEX.GroupInterval) = Janus.Windows.GridEX.GroupInterval.Date) _
            Or (CType(Me.cmbGroupInterval.SelectedItem, Janus.Windows.GridEX.GroupInterval) = Janus.Windows.GridEX.GroupInterval.Hour) _
            Or (CType(Me.cmbGroupInterval.SelectedItem, Janus.Windows.GridEX.GroupInterval) = Janus.Windows.GridEX.GroupInterval.Minute) _
            Or (CType(Me.cmbGroupInterval.SelectedItem, Janus.Windows.GridEX.GroupInterval) = Janus.Windows.GridEX.GroupInterval.Month) _
            Or (CType(Me.cmbGroupInterval.SelectedItem, Janus.Windows.GridEX.GroupInterval) = Janus.Windows.GridEX.GroupInterval.OutlookDates) _
            Or (CType(Me.cmbGroupInterval.SelectedItem, Janus.Windows.GridEX.GroupInterval) = Janus.Windows.GridEX.GroupInterval.Quarter) _
            Or (CType(Me.cmbGroupInterval.SelectedItem, Janus.Windows.GridEX.GroupInterval) = Janus.Windows.GridEX.GroupInterval.Second) _
            Or (CType(Me.cmbGroupInterval.SelectedItem, Janus.Windows.GridEX.GroupInterval) = Janus.Windows.GridEX.GroupInterval.Year) Then
                If Not (Me.Grid.RootTable.Columns(Me.cmbColumn.Text).DataTypeCode = TypeCode.DateTime) Then
                    MessageBox.Show("Invalid Conversion Type !" + vbCrLf & " Column Type Must be DateTime Type", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.cmbGroupInterval.Text = ""
                    Me.cmbGroupInterval.SelectedIndex = -1
                    Return
                Else
                    Me.Grid.RootTable.Columns(Me.cmbColumn.Text).DefaultGroupPrefix = "GroupBy " + Me.cmbGroupInterval.SelectedItem().ToString + " "
                    Me.Grid.RootTable.Columns(Me.cmbColumn.Text).DefaultGroupInterval = CType(Me.cmbGroupInterval.SelectedItem, Janus.Windows.GridEX.GroupInterval)
                End If
            Else
                Me.Grid.RootTable.Columns(Me.cmbColumn.Text).DefaultGroupPrefix = "GroupBy :"
                Me.Grid.RootTable.Columns(Me.cmbColumn.Text).DefaultGroupInterval = CType(Me.cmbGroupInterval.SelectedItem, Janus.Windows.GridEX.GroupInterval)
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub txtPageHeaderRight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPageHeaderRight.TextChanged
        Try
            Me.GridExPrintDock.PageHeaderRight = Me.txtPageHeaderRight.Text
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtHeaderDistance_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHeaderDistance.TextChanged
        Try
            'If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
            'Else
            '    e.Handled = True
            '    Return
            'End If
            Me.GridExPrintDock.HeaderDistance = CInt(Me.txtHeaderDistance.Text.Trim())
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPageHeaderCenter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPageHeaderCenter.TextChanged
        Try
            Me.GridExPrintDock.PageHeaderCenter = Me.txtPageHeaderCenter.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPageFooterCenter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPageFooterCenter.TextChanged
        Try

            Me.GridExPrintDock.PageFooterCenter = Me.txtPageFooterCenter.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPageHeaderLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPageHeaderLeft.TextChanged
        Try
            Me.GridExPrintDock.PageHeaderLeft = Me.txtPageHeaderLeft.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPageFooterRight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPageFooterRight.TextChanged
        Try
            Me.GridExPrintDock.PageFooterRight = Me.txtPageFooterRight.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPageFooterLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPageFooterLeft.TextChanged
        Try
            Me.GridExPrintDock.PageFooterLeft = Me.txtPageFooterLeft.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtHeaderDistance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeaderDistance.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
            Else
                e.Handled = True
                Return
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtFooterDistance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFooterDistance.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
            Else
                e.Handled = True
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkGroupbyBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGroupbyBox.CheckedChanged
        Try
            If Me.chkGroupbyBox.Checked = True Then
                Me.Grid.GroupByBoxVisible = True
            Else
                Me.Grid.GroupByBoxVisible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbTotalRowPosition_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTotalRowPosition.SelectedIndexChanged
        Try
            If Me.cmbTotalRowPosition.SelectedIndex <> -1 Then
                Me.Grid.TotalRowPosition = CType(Me.cmbTotalRowPosition.SelectedItem, Janus.Windows.GridEX.TotalRowPosition)
            End If
        Catch ex As Exception

        End Try
    End Sub

   
    Private Sub cmbColorScheme_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbColorScheme.SelectedIndexChanged
        Try
            If Me.cmbColorScheme.SelectedIndex <> -1 Then
                Me.Grid.Office2007ColorScheme = CType(Me.cmbColorScheme.SelectedItem, Janus.Windows.GridEX.Office2007ColorScheme)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbVisualStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVisualStyle.SelectedIndexChanged
        Try
            If Me.cmbVisualStyle.SelectedIndex <> -1 Then
                Me.Grid.VisualStyle = CType(Me.cmbVisualStyle.SelectedItem, Janus.Windows.GridEX.VisualStyle)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbColumn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbColumn.SelectedIndexChanged
        Try
            If Me.cmbColumn.SelectedIndex = -1 Then
                Me.cmbAgregate.Text = ""
                Me.cmbGroupInterval.Text = ""
            Else
                Me.cmbAgregate.SelectedItem = CType(Me.Grid.RootTable.Columns(Me.cmbColumn.Text).AggregateFunction, Object)
                Me.cmbGroupInterval.SelectedItem = CType(Me.Grid.RootTable.Columns(Me.cmbColumn.Text).DefaultGroupInterval, Object)
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub cmbColumn_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbColumn.TextChanged
    '    If Me.cmbColumn.FindStringExact(Me.cmbColumn.Text) = -1 Then
    '        Me.cmbAgregate.Text = ""
    '        Me.cmbGroupInterval.Text = ""
    '    End If
    'End Sub

   
End Class