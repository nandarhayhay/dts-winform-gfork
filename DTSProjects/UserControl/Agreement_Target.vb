Imports System.Threading
Public Class Agreement_Target
    Private m_DistReport As NufarmBussinesRules.DistributorReport.Dist_Report
    Private HasLoadReport As Boolean = True
    Private LD As Loading
    Private CounterTimer2 As Integer = 0
    Private Delegate Sub onShowingProgress()
    Private Event ShowProgres As onShowingProgress
    Private Delegate Sub onClossingProgress()
    Private Event CloseProgress As onClossingProgress
    Dim rnd As Random : Private ResultRandom As Integer
    Private tickCount As Integer = 0
    Private DV As DataView = Nothing
    Private HasLoadForm As Boolean = False
    Private Ticcount As Integer = 0
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Dim isChecking As Boolean = False
    Dim OriginStartDate As DateTime, OriginEndDate As DateTime

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
        Me.Ticcount = 0
    End Sub
    Private Property clsDistReport() As NufarmBussinesRules.DistributorReport.Dist_Report
        Get
            If IsNothing(Me.m_DistReport) Then
                Me.m_DistReport = New NufarmBussinesRules.DistributorReport.Dist_Report()
            End If
            Return Me.m_DistReport
        End Get
        Set(ByVal value As NufarmBussinesRules.DistributorReport.Dist_Report)
            Me.m_DistReport = value
        End Set
    End Property
    Private Sub Bindgrid(ByVal DV As DataView)
        Dim rowfilter As String = "END_DATE >= " & NufarmBussinesRules.SharedClass.ServerDateString
        Me.DV.RowFilter = rowfilter
        If IsNothing(DV) Then
            Me.GridEX1.SetDataBinding(Nothing, "") : Return
        End If
        If Me.btnShowAll.Checked = False Then
            Me.DV.RowFilter = rowfilter
        Else
            'Me.DV.RowFilter = rowfilter
            Me.DV.RowFilter = ""
        End If
        Me.GridEX1.SetDataBinding(DV, "")
        Me.GridEX1.RetrieveStructure()
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If col.DataMember.Contains("ID") Then
                col.Visible = False
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf col.Type Is Type.GetType("System.Decimal") Or col.Type Is Type.GetType("System.Int32") Then
                If col.DataMember.ToString().Contains("VALUE") Then
                    col.FormatString = "#,##0.00"
                    col.TotalFormatString = "#,##0.00"
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                ElseIf col.DataMember.Contains("TARGET") Then
                    col.FormatString = "#,##0.000"
                    col.TotalFormatString = "#,##0.000"
                End If
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                col.ColumnType = Janus.Windows.GridEX.ColumnType.Text
            ElseIf col.Type Is Type.GetType("System.DateTime") Then
                col.FormatString = "dd MMMM yyyy"
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
                col.ColumnType = Janus.Windows.GridEX.ColumnType.Text
            ElseIf col.Type Is Type.GetType("System.Boolean") Then
                col.ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
            Else
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            End If
        Next
        Me.AddConditionalFormating()
        Me.GridEX1.AutoSizeColumns()
    End Sub

    'Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
    '    If Me.tickCount >= Me.ResultRandom Then
    '        If Me.HasLoadReport Then
    '            Me.Bindgrid(Me.DV)
    '            RaiseEvent CloseProgress()
    '        Else
    '            Me.ResultRandom += 1
    '        End If
    '    End If
    'End Sub
    'Private Sub ChekTimer2(ByVal sender As Object, ByVal e As EventArgs)
    '    If Me.CounterTimer2 > 1 Then
    '        Me.Timer2.Stop() : Me.Timer2.Enabled = False
    '        Me.CounterTimer2 = 0
    '        Me.getdata()
    '        Me.HasLoadReport = True
    '    End If

    'End Sub
    Private Sub closeProgressbar() Handles Me.CloseProgress
        Me.Timer1.Enabled = False
        Me.Timer1.Stop()
        'If Not IsNothing(Me.ST) Then
        '    Me.ST.Close()
        '    Me.ST = Nothing
        'End If
        Me.Timer2.Enabled = False
        Me.tickCount = 0
        Me.CounterTimer2 = 0
        If Not IsNothing(Me.LD) Then : Me.LD.Close() : Me.LD = Nothing : End If

    End Sub
    Friend Sub RefreshData()
        Me.btnAplyRange_Click(Me.btnAplyRange, New EventArgs())
    End Sub
    'Private Sub getdata()
    '    Try
    '        Me.DV = Me.clsDistReport.Create_View_TA()
    '    Catch ex As Exception
    '        Me.HasLoadReport = True
    '    End Try
    'End Sub
    Private Sub AddConditionalFormating()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.GridEX1.RootTable.Columns("END_DATE"), Janus.Windows.GridEX.ConditionOperator.LessThan, CObj(NufarmBussinesRules.SharedClass.ServerDate()))
        fc.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
        fc.FormatStyle.ForeColor = SystemColors.GrayText
        GridEX1.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub Agreement_Target_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsDistReport) Then
                Me.clsDistReport.Dispose(False)
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub LoadData()
        'Me.chkShowAll.Checked = False
        'LD = New Loading
        'LD.Show() : LD.TopMost = True
        'Application.DoEvents()
        'Me.rnd = New Random() : Me.ResultRandom = Me.rnd.Next(1, 6)
        'Me.Timer1.Enabled = True : Me.Timer1.Start()
        'Application.DoEvents() : Me.HasLoadReport = False
        'Me.Timer2.Enabled = True : Me.Timer2.Start()
    End Sub
    'Private Sub chkShowAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowAll.CheckedChanged
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        If Not IsNothing(Me.GridEX1.DataSource) Then
    '            Dim rowfilter As String = "END_DATE >= " & NufarmBussinesRules.SharedClass.ServerDateString
    '            If Me.chkShowAll.Checked = False Then
    '                Me.DV.RowFilter = rowfilter
    '            Else
    '                Me.DV.RowFilter = ""
    '            End If
    '            Me.GridEX1.SetDataBinding(Me.DV, "")
    '            Me.AddConditionalFormating()
    '        End If
    '    Catch ex As Exception

    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If Me.HasLoadForm Then
            Me.CounterTimer2 += 1
        End If

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.tickCount += 1
    End Sub

    Private Sub Agreement_Target_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Cursor = Cursors.WaitCursor
            Me.ExpandablePanel1.Expanded = False
            'AddHandler Timer1.Tick, AddressOf ChekTimer
            'AddHandler Timer2.Tick, AddressOf ChekTimer2
            'Me.LoadData()
            'get current Agreement
            Dim StartPeriode As DateTime = DateTime.Now
            Dim EndPeriode As DateTime = StartPeriode.AddMonths(12)
            Me.clsDistReport.GetCurrentAgreement(StartPeriode, EndPeriode, False)
            Me.dtPicFilterStart.Value = StartPeriode
            Me.dtPicFilterEnd.Value = EndPeriode
            Me.dtPicModFromDate.Value = StartPeriode
            Me.dtPicModUntilDate.Value = StartPeriode.AddMonths(6)
            Me.ExpandablePanel2.Expanded = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.HasLoadForm = True : Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function hasChangeData() As Boolean
        If IsNothing(Me.OriginStartDate) Or IsNothing(Me.OriginEndDate) Then
            Return True
        ElseIf Me.OriginStartDate <> Convert.ToDateTime(Me.dtPicFilterStart.Value.ToShortDateString()) Then
            Return True
        ElseIf Me.OriginEndDate <> Convert.ToDateTime(Me.dtPicFilterEnd.Value.ToShortDateString()) Then
            Return True
        Else
            Return False
        End If
        Return True
    End Function
    Private Sub btnAplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            Me.DV = Me.clsDistReport.Create_View_TA(Convert.ToDateTime(Me.dtPicFilterStart.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicFilterEnd.Value.ToShortDateString()), hasChangeData)
            Me.Bindgrid(Me.DV)
            Me.OriginStartDate = Convert.ToDateTime(Me.dtPicFilterStart.Value.ToShortDateString())
            Me.OriginEndDate = Convert.ToDateTime(Me.dtPicFilterEnd.Value.ToShortDateString())
            'set langsung dateTime Picker modify
            If Me.DV.Count > 0 Then
                Me.dtPicModFromDate.Value = Convert.ToDateTime(Me.dtPicFilterStart.Value.ToShortDateString())
                Me.dtPicModUntilDate.Value = Convert.ToDateTime(Me.dtPicModFromDate.Value.ToShortDateString()).AddMonths(6)
            End If
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : MessageBox.Show(ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub getModifiedData()
        Me.StatProg = StatusProgress.Processing
        Me.ThreadProgress = New Thread(AddressOf ShowProceed)
        Me.ThreadProgress.Start()
        Me.DV = Me.clsDistReport.getNewOrModifiedData(Convert.ToDateTime(Me.dtPicModFromDate.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicModUntilDate.Value.ToShortDateString()))
        Me.Bindgrid(Me.DV)
        Me.OriginStartDate = Convert.ToDateTime(Me.dtPicFilterStart.Value.ToShortDateString())
        Me.OriginEndDate = Convert.ToDateTime(Me.dtPicFilterEnd.Value.ToShortDateString())
        Me.StatProg = StatusProgress.None
    End Sub
    Private Sub dtPicModUntilDate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicModUntilDate.KeyDown, dtPicModFromDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                Me.Cursor = Cursors.Default
                Me.getModifiedData()
            Catch ex As Exception
                Me.StatProg = StatusProgress.None : MessageBox.Show(ex.Message)
            End Try
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            If isChecking Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            If TypeOf (sender) Is DevComponents.DotNetBar.ButtonItem Then
                Try
                    Dim item As DevComponents.DotNetBar.ButtonItem = CType(sender, DevComponents.DotNetBar.ButtonItem)
                    item.Checked = Not item.Checked
                    Dim itemChecked As DevComponents.DotNetBar.ButtonItem = Nothing
                    If item.Checked Then
                        isChecking = True
                        For Each itemTest As Object In Me.ItemPanel1.Controls
                            If TypeOf (itemTest) Is DevComponents.DotNetBar.ButtonItem Then
                                If CType(itemTest, DevComponents.DotNetBar.ButtonItem).Name <> item.Name Then
                                    CType(itemTest, DevComponents.DotNetBar.ButtonItem).Checked = False
                                End If
                            End If
                        Next
                        isChecking = False
                        Select Case item.Name
                            Case "btnShowAll"
                                btnAplyRange_Click(Me.btnAplyRange, New EventArgs())
                            Case "btnRangeDate"
                                Me.getModifiedData()
                        End Select
                    Else
                        For Each itemTest As Object In Me.ItemPanel1.Controls
                            If TypeOf (itemTest) Is DevComponents.DotNetBar.ButtonItem Then
                                If CType(itemTest, DevComponents.DotNetBar.ButtonItem).Checked Then
                                    itemChecked = CType(itemTest, DevComponents.DotNetBar.ButtonItem)
                                    Exit For
                                End If
                            End If
                        Next
                        If Not IsNothing(itemChecked) Then
                            isChecking = True
                            For Each itemTest As Object In Me.ItemPanel1.Controls
                                If TypeOf (itemTest) Is DevComponents.DotNetBar.ButtonItem Then
                                    If CType(itemTest, DevComponents.DotNetBar.ButtonItem).Name <> itemChecked.Name Then
                                        CType(itemTest, DevComponents.DotNetBar.ButtonItem).Checked = False
                                    End If
                                End If
                            Next
                            isChecking = False
                        End If
                        If Not IsNothing(itemChecked) Then
                            Select Case itemChecked.Name
                                Case "btnShowAll"
                                    btnAplyRange_Click(Me.btnAplyRange, New EventArgs())
                                Case "btnRangeDate"
                                    Me.getModifiedData()
                            End Select
                        End If
                    End If
                Catch ex As Exception
                    Me.StatProg = StatusProgress.None : MessageBox.Show(ex.Message)
                End Try
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : MessageBox.Show(ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
End Class
