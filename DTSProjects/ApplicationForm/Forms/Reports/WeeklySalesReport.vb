Imports NufarmBussinesRules.OrderAcceptance
Public Class WeeklySalesReport
    Friend ClsSPPBManager As SPPBGONManager = Nothing
    Private Sub GridEX1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        If e.KeyCode = Keys.F7 Then
            Try
                Dim FE As New Janus.Windows.GridEX.Export.GridEXExporter()
                Me.Cursor = Cursors.WaitCursor
                FE.IncludeChildTables = False
                FE.IncludeHeaders = True
                FE.SheetName = "NUFARM_WEEKLY_SALES_REPORT"
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
        End If
    End Sub

    Private Sub btnFilteDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilteDate.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim tblSales As DataTable = Me.ClsSPPBManager.GetSalesReport(Convert.ToDateTime(Me.DateTimePicker1.Value.ToShortDateString()), Convert.ToDateTime(Me.DateTimePicker2.Value.ToShortDateString()))
            Me.GridEX1.SetDataBinding(tblSales.DefaultView, "")
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnFilteDate_Click")
        End Try
    End Sub
End Class
