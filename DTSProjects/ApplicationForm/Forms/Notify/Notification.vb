
Imports NufarmBussinesRules.Program
Imports System.Globalization
Public Class Notification
    Public CMain As Main = Nothing
    Private clsProgram As BrandPackInclude = Nothing
    Private DV As DataView = Nothing
    Private isSnoozing As Boolean = False
    Private isLoadingRow As Boolean = False
    Private isLoadedForm As Boolean = False
    Friend Snooze As String = "1 Minute"
    Private Sub btnDismisAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDismisAll.Click
        If Me.GridEX1.DataSource Is Nothing Then
            Return
        End If
        If Me.GridEX1.RecordCount <= 0 Then
            Return
        End If
        Cursor = Cursors.Default
        Dim ListProgram As New List(Of String)
        Try
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetDataRows()
                If Not ListProgram.Contains(row.Cells("PROG_BRANDPACK_DIST_ID").Value.ToString()) Then
                    ListProgram.Add(row.Cells("PROG_BRANDPACK_DIST_ID").Value.ToString())
                End If
            Next
            If ListProgram.Count > 0 Then
                Me.clsProgram.DismisReminder(ListProgram, False)
            End If
            ''hapus data di registry
            Dim rg As New NufarmBussinesRules.SettingDTS.RegUser()
            rg.DeleteKey("StartReminderTime")
            rg.DeleteKey("StartReminderName")
            rg.DeleteKey("EndReminder")
            rg.DeleteKey("Reminder")
            Me.Close()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
        End Try

        Cursor = Cursors.Default
    End Sub

    Private Sub btnDismiss_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDismiss.Click
        Try
            If Me.GridEX1.DataSource Is Nothing Then
                Return
            End If
            If Me.GridEX1.RecordCount <= 0 Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim ListProgram As New List(Of String)
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Then
                Dim ProgramDesc As String = ""
                While Me.GridEX1.GetRow().RowType <> Janus.Windows.GridEX.RowType.GroupHeader
                    If ProgramDesc = "" Then
                        ProgramDesc = Me.GridEX1.GetValue("PROGRAM_DESCRIPTIONS").ToString()
                    End If
                    If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                        If ProgramDesc = Me.GridEX1.GetValue("PROGRAM_DESCRIPTIONS").ToString() Then
                            If Not ListProgram.Contains(Me.GridEX1.GetValue("PROG_BRANDPACK_DIST_ID").ToString()) Then
                                ListProgram.Add(Me.GridEX1.GetValue("PROG_BRANDPACK_DIST_ID").ToString())
                            End If
                        End If
                    End If
                End While
            Else
                If Me.GridEX1.GetCheckedRows().Length <= 0 Then
                    Me.ShowMessageInfo("Please mark row to be dismissed") : Cursor = Cursors.Default : Return
                End If
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetCheckedRows()
                    If Not ListProgram.Contains(row.Cells("PROG_BRANDPACK_DIST_ID").Value.ToString()) Then
                        ListProgram.Add(row.Cells("PROG_BRANDPACK_DIST_ID").Value.ToString())
                    End If
                Next
            End If
            If ListProgram.Count > 0 Then
                Me.clsProgram.DismisReminder(ListProgram, False)
            End If
            isLoadingRow = True
            Me.LoadData()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
        End Try
        isLoadingRow = False
        Cursor = Cursors.Default
    End Sub
    Private Sub SnoozeReminder()
        If Me.cmbSnooze.Text <> "" Then
            CMain.tmrReminder.Stop()
            CMain.tmrReminder.Enabled = False
            Dim rg As New NufarmBussinesRules.SettingDTS.RegUser()

            Select Case Me.cmbSnooze.Text
                Case "1 Minute"
                    Me.CMain.tmrReminder.Interval = 60000
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Minute")
                    rg.Write("EndReminder", DateTime.Now.AddMinutes(1).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                    CMain.tmrReminder.Enabled = True
                    CMain.tmrReminder.Start()
                Case "5 Minutes"
                    CMain.tmrReminder.Interval = 5 * 60 * 1000
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Minute")
                    rg.Write("EndReminder", DateTime.Now.AddMinutes(5).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                    CMain.tmrReminder.Enabled = True
                    CMain.tmrReminder.Start()
                Case "10 Minutes"
                    CMain.tmrReminder.Interval = 10 * 60 * 1000
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Minute")
                    rg.Write("EndReminder", DateTime.Now.AddMinutes(10).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                    CMain.tmrReminder.Enabled = True
                    CMain.tmrReminder.Start()
                Case "15 Minutes"
                    CMain.tmrReminder.Interval = 15 * 60 * 1000
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Minute")
                    rg.Write("EndReminder", DateTime.Now.AddMinutes(15).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                    CMain.tmrReminder.Enabled = True
                    CMain.tmrReminder.Start()
                Case "30 Minutes"
                    CMain.tmrReminder.Interval = 30 * 60 * 1000
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Minute")
                    rg.Write("EndReminder", DateTime.Now.AddMinutes(30).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                    CMain.tmrReminder.Enabled = True
                    CMain.tmrReminder.Start()
                Case "1 Hour"
                    CMain.tmrReminder.Interval = 60 * 60 * 1000
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Hour")
                    rg.Write("EndReminder", DateTime.Now.AddHours(1).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                    CMain.tmrReminder.Enabled = True
                    CMain.tmrReminder.Start()
                Case "2 Hours"
                    CMain.tmrReminder.Interval = 2 * 60 * 60 * 1000
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Hour")
                    rg.Write("EndReminder", DateTime.Now.AddHours(2).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                    CMain.tmrReminder.Enabled = True
                    CMain.tmrReminder.Start()
                Case "8 Hours"
                    CMain.tmrReminder.Interval = 8 * 60 * 60 * 1000
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Hour")
                    rg.Write("EndReminder", DateTime.Now.AddHours(8).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                    CMain.tmrReminder.Enabled = True
                    CMain.tmrReminder.Start()
                Case "12 Hours"
                    CMain.tmrReminder.Interval = 8 * 60 * 60 * 1000
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Hour")
                    rg.Write("EndReminder", DateTime.Now.AddHours(8).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                Case "1 Day"
                    CMain.tmrReminder.Stop()
                    CMain.tmrReminder.Enabled = False
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Day")
                    rg.Write("EndReminder", DateTime.Now.AddDays(1).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                Case "3 Days"
                    CMain.tmrReminder.Stop()
                    CMain.tmrReminder.Enabled = False
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Day")
                    rg.Write("EndReminder", DateTime.Now.AddDays(3).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                Case "1 Week"
                    CMain.tmrReminder.Stop()
                    CMain.tmrReminder.Enabled = False
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Day")
                    rg.Write("EndReminder", DateTime.Now.AddDays(7).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                Case "2 Weeks"
                    CMain.tmrReminder.Stop()
                    CMain.tmrReminder.Enabled = False
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Day")
                    rg.Write("EndReminder", DateTime.Now.AddDays(14).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                Case "3 Weeks"
                    CMain.tmrReminder.Stop()
                    CMain.tmrReminder.Enabled = False
                    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    rg.Write("StartReminderName", "Day")
                    rg.Write("EndReminder", DateTime.Now.AddDays(21).ToString())
                    rg.Write("Reminder", Me.cmbSnooze.Text)
                    'Case "1 Month"
                    '    CMain.tmrReminder.Stop()
                    '    CMain.tmrReminder.Enabled = False
                    '    rg.Write("StartReminderTime", DateTime.Now.ToString())
                    '    rg.Write("StartReminderName", "Month")
                    '    rg.Write("EndReminder", DateTime.Now.AddMonths(1).ToString())
            End Select
        End If
    End Sub
    Private Sub btnSnooze_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSnooze.Click
        If Me.cmbSnooze.Text = "" Then
            Me.baseTooltip.Show("Please define elapsed time", Me.cmbSnooze, 2500) : Return
        ElseIf Me.cmbSnooze.SelectedIndex <= -1 Then
            Me.baseTooltip.Show("Please define elapsed time", Me.cmbSnooze, 2500) : Return
        End If
        '' bila lebih dari 8 jam simpan saja di registry
        Me.isSnoozing = True
        Me.SnoozeReminder()
        Me.Close()
    End Sub
    Private Sub LoadData()
        If IsNothing(Me.clsProgram) Then
            Me.clsProgram = New BrandPackInclude()
        End If
        Dim dt As DataTable = Me.clsProgram.getSalesHistoryReminder()
        If Not IsNothing(dt) Then
            Me.DV = dt.DefaultView
        End If
        Me.GridEX1.SetDataBinding(Me.DV, "")
        Me.GridEX1.RetrieveStructure()
        Me.GridEX1.RootTable.Columns("PROG_BRANDPACK_DIST_ID").Caption = ""
        Me.GridEX1.RootTable.Columns("PROG_BRANDPACK_DIST_ID").ShowRowSelector = True
        Me.GridEX1.RootTable.Columns("PROG_BRANDPACK_DIST_ID").UseHeaderSelector = True
        Me.GridEX1.RootTable.Columns("PROG_BRANDPACK_DIST_ID").Position = 0
        Me.GridEX1.RootTable.Columns("DISTRIBUTOR_ID").Visible = False
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If col.Type Is Type.GetType("System.String") Then
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf col.Type Is Type.GetType("System.Decimal") Then
                col.FormatString = "#,##0.000"
                col.TotalFormatString = "#,##0.000"
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf col.Type Is Type.GetType("System.DateTime") Then
                col.FormatString = "dd MMMM yyyy"
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            End If
            col.AutoSize()
        Next
        'Me.GridEX1.RootTable.Columns("PROG_BRANDPACK_DIST_ID").Visible = False

        Me.GridEX1.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.GridEX1.RootTable.Columns("PROGRAM_DESCRIPTIONS")))
        Me.GridEX1.RootTable.Columns("PROGRAM_DESCRIPTIONS").Visible = False
        Me.GridEX1.RootTable.Columns("PROGRAM_DESCRIPTIONS").DefaultGroupPrefix = ""
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.RootTable.Columns("PROG_BRANDPACK_DIST_ID").Width = 20
    End Sub
    Private Sub Notification_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            ''load semua data dimana end_date <= 2 (14 hari)
            Me.CMain.tmrReminder.Enabled = False
            Me.CMain.tmrReminder.Stop()
            Me.LoadData()
            Me.cmbSnooze.Text = Me.Snooze
            Me.cmbSnooze.SelectAll()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
        End Try
        isLoadedForm = True
        isLoadingRow = False
        Cursor = Cursors.Default
    End Sub

    Private Sub GridEX1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles GridEX1.KeyDown
        If e.KeyCode = Keys.F7 Then
            Try
                Me.GridEX1.RootTable.Columns("PROG_BRANDPACK_DIST_ID").Visible = False
                Dim FE As New Janus.Windows.GridEX.Export.GridEXExporter()
                Me.Cursor = Cursors.WaitCursor
                FE.IncludeChildTables = False
                FE.IncludeHeaders = True
                FE.SheetName = "SALES_PROGRAM_REMINDER"
                FE.IncludeFormatStyle = False
                FE.IncludeExcelProcessingInstruction = True
                FE.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                Dim SD As New SaveFileDialog()
                SD.OverwritePrompt = True
                SD.DefaultExt = ".xls"
                SD.Filter = "All Files|*.*"
                SD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                If SD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Using FS As New System.IO.FileStream(SD.FileName, IO.FileMode.Create)

                        FE.GridEX = Me.GridEX1

                        FE.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & SD.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                End If
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.ShowMessageError(ex.Message)
                Cursor = Cursors.Default
            End Try
            Me.GridEX1.RootTable.Columns("PROG_BRANDPACK_DIST_ID").Visible = False
            Me.GridEX1.RootTable.Columns("PROG_BRANDPACK_DIST_ID").Width = 20
        End If
    End Sub

    Private Sub Notification_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If isSnoozing Then
            Return
        End If
        If IsNothing(Me.GridEX1.DataSource) Then
            Return
        ElseIf Me.GridEX1.RecordCount <= 0 Then
            Return
        End If
        'Me.SnoozeReminder()
    End Sub
    Private Sub ClearInfo()
        Me.lblHeaderReminder.Text = ""
        Me.lblTimeReminder.Text = ""
    End Sub
    Private Sub GridEX1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        If isLoadingRow Then : Return : End If
        If Not Me.isLoadedForm Then : Return : End If
        If Me.GridEX1.DataSource Is Nothing Then : ClearInfo() : Return : End If
        If Me.GridEX1.RecordCount <= 0 Then : ClearInfo() : Return : End If
        If Me.GridEX1.SelectedItems Is Nothing Then : ClearInfo() : Return : End If
        If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Then
            Me.lblHeaderReminder.Text = Me.GridEX1.GetRow().GroupCaption()
            Me.lblTimeReminder.Text = "" : Return
        End If
        If Me.GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
            ClearInfo() : Return
        End If

        Me.lblHeaderReminder.Text = Me.GridEX1.GetValue("PROGRAM_DESCRIPTIONS").ToString()
        Me.lblTimeReminder.Text = String.Format(New CultureInfo("id-ID"), "{0:dd-MMMM-yyyy} - {1:dd-MMMM-yyyy}", Convert.ToDateTime(Me.GridEX1.GetValue("START_DATE")), Convert.ToDateTime(Me.GridEX1.GetValue("END_DATE")))
    End Sub
End Class
