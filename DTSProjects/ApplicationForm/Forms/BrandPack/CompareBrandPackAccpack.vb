Imports System.Threading
Public Class CompareBrandPackAccpack
    Dim dtTable As DataTable = Nothing
    Private LD As Loading
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private Ticcount As Integer = 0
    Private clsCI As NufarmBussinesRules.Brandpack.CompareItem
    Friend CMain As Main = Nothing
    Private Enum StatusProgress
        None
        Processing
    End Enum

    Private Sub ShowProceed()
        Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Me.LD.Refresh() : Thread.Sleep(100) : Application.DoEvents()
        End While
        Thread.Sleep(100) : Me.LD.Close() : Me.LD = Nothing
        Me.Ticcount = 0
    End Sub
    Private Sub LoadData(ByVal MustRebuildNew As Boolean)
        If Not IsNothing(dtTable) Then : dtTable.Clear() : End If
        If Me.clsCI Is Nothing Then
            Me.clsCI = New NufarmBussinesRules.Brandpack.CompareItem()
        End If
        dtTable = clsCI.GetCompareItems(MustRebuildNew)
        'Using cls As New NufarmBussinesRules.Brandpack.CompareItem
        '    dtTable = cls.GetCompareItems(MustRebuildNew)
        'End Using
        Me.GridEX1.SetDataBinding(dtTable, "")
    End Sub
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case DirectCast(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnRenameColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar2)
                Case "btnFieldChooser"
                    Me.GridEX1.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog()
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                        Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    End If
                    'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                Case "btnPageSetting"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
                Case "btnCustomFilter"
                    Me.FilterEditor1.Visible = True
                    Me.FilterEditor1.SortFieldList = False
                    Me.FilterEditor1.SourceControl = Me.GridEX1
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.GridEX1.RemoveFilters()
                Case "btnFilterEqual"
                    Me.GridEX1.RemoveFilters()
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.FilterEditor1.Visible = False
                Case "btnExport"
                    Me.SaveFileDialog1.Title = "Define the location File"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.GridEX1
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnRefresh"
                    Me.StatProg = StatusProgress.Processing
                    Me.ThreadProgress = New Thread(AddressOf ShowProceed)
                    Me.ThreadProgress.Start() : Me.LoadData(True)
                    Me.StatProg = StatusProgress.None
            End Select
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CompareBrandPackAccpack_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.LoadData(False)
        Catch ex As Exception
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CompareBrandPackAccpack_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsCI) Then
                Me.clsCI.Dispose(True)
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class