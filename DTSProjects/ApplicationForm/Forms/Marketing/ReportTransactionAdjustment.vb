Public Class ReportTransactionAdjustment
    Public clsAdj As NufarmBussinesRules.Program.Addjustment = Nothing
    Friend CMain As Main = Nothing
    Private Sub ReportTransactionAdjustment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Try
            'get Default data PKD
            If IsNothing(Me.clsAdj) Then
                Me.clsAdj = New NufarmBussinesRules.Program.Addjustment()
            End If
            Dim StartDate As DateTime = DateTime.Now
            Dim EndDate As DateTime = DateTime.Now.AddYears(10)
            Me.clsAdj.GetCurrentPeriode(StartDate, EndDate)
            ''get data by Current Periode
            Me.DateTimePicker1.Value = StartDate
            Me.DateTimePicker2.Value = EndDate
            Me.GetData()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub GetData()
        Dim tbl As DataTable = Me.clsAdj.GetHistoryAdjustment(Convert.ToDateTime(Me.DateTimePicker1.Value.ToShortDateString()), Convert.ToDateTime(Me.DateTimePicker2.Value.ToShortDateString()))
        Me.GridEX1.SetDataBinding(tbl.DefaultView(), "")
    End Sub
    Private Sub btnFilteDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilteDate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.GetData()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ExportData(ByVal grid As Janus.Windows.GridEX.GridEX)
        Dim SV As New SaveFileDialog()
        With SV
            .Title = "Define the location File"
            .InitialDirectory = System.Environment.SpecialFolder.MyDocuments
            .RestoreDirectory = True
            .OverwritePrompt = True
            .DefaultExt = ".xls"
            .FileName = "All Files|*.*"
        End With
        If SV.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim FS As New System.IO.FileStream(SV.FileName, IO.FileMode.Create)
            Dim Exporter As New Janus.Windows.GridEX.Export.GridEXExporter()
            Exporter.GridEX = grid
            Exporter.Export(FS)
            FS.Close()
            MessageBox.Show("Data Exported to " & SV.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = DirectCast(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnShowFieldChooser"
                    If Not IsNothing(GridEX1.DataSource) Then
                        If GridEX1.RootTable.Columns.Count > 0 Then
                            GridEX1.ShowFieldChooser(Me)
                        End If
                    End If
                Case "btnSettingGrid"
                    If Not IsNothing(GridEX1.DataSource) Then
                        If GridEX1.RootTable.Columns.Count > 0 Then
                            Dim SetGrid As New SettingGrid()
                            SetGrid.Grid = GridEX1
                            SetGrid.ShowDialog()
                        End If
                    End If
                Case "btnCustomFilter"
                    If Not IsNothing(GridEX1.DataSource) Then
                        If GridEX1.RootTable.Columns.Count > 0 Then
                            GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            GridEX1.RemoveFilters()
                            Me.FilterEditor1.Visible = True
                            Me.FilterEditor1.SortFieldList = False
                            Me.FilterEditor1.SourceControl = Me.GridEX1
                        End If
                    End If
                Case "btnFilterEqual"
                    If Not IsNothing(GridEX1.DataSource) Then
                        Me.FilterEditor1.SourceControl = Nothing
                        GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        GridEX1.RemoveFilters()
                    End If
                Case "btnExport"
                    If Not IsNothing(GridEX1.DataSource) Then
                        If GridEX1.RootTable.Columns.Count > 0 Then
                            ExportData(GridEX1)
                        End If
                    End If
                Case "btnRefresh"
                    'reload Data
                    Me.GetData()
            End Select
            Me.Cursor = Cursors.Default
        Catch ex As Exception
          
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default

        End Try
    End Sub
End Class
