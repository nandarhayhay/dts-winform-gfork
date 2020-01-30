Public Class DiscMarketing

#Region " Deklarasi "
    Private clsDiscMarketing As NufarmBussinesRules.Program.MarketingDiscount
    Private SFM As StateFillingCombo
    Private Delegate Sub onConnecting(ByVal message As String)
    Private Event Connecting As onConnecting
    Private Delegate Sub onClosingConnection()
    Private Event ClosingConnection As onClosingConnection
    Dim rnd As Random
    Private Shadows tickCount As Integer
    Private HasgeneratedDiscount As Boolean

#End Region

#Region " Enum "
    Private Enum StateFillingCombo
        Filing
        HasFilled
    End Enum
#End Region

#Region " Void "
    Private Sub FormatGrid()
        Me.GridEX1.RootTable.Columns("PROGRAM_NAME").AutoSize()
        Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").AutoSize()
        Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").AutoSize()
    End Sub
    Private Sub BindMulticolumnCombo(ByVal dtView As DataView)
        Me.SFM = StateFillingCombo.Filing
        Me.cmbDistributor.SetDataBinding(dtView, "")
        Me.SFM = StateFillingCombo.HasFilled
    End Sub
    Friend Sub InitializeData()
        Me.RefreshData()
    End Sub
    Private Sub RefreshData()
        Me.clsDiscMarketing = New NufarmBussinesRules.Program.MarketingDiscount()
        Me.SFM = StateFillingCombo.Filing
        Me.clsDiscMarketing.CreateViewDistributorMarketing()
        Me.BindMulticolumnCombo(Me.clsDiscMarketing.ViewDistibutorMarketing())
    End Sub
    Private Overloads Sub ShowProgress(ByVal Message As String) Handles Me.Connecting
        Me.ST = New Progress
        Me.ST.Show(Message)
        'Me.ST.PictureBox1.Refresh()
        'Me.ST.Refresh()
        'Application.DoEvents()
    End Sub
    Private Sub closeConnection() Handles Me.ClosingConnection
        If Not IsNothing(Me.ST) Then
            Me.ST.Close()
            Me.ST = Nothing
            Me.tickCount = 0
        End If
    End Sub
    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.tickCount = Me.ResultRandom Then
            If Me.HasgeneratedDiscount = True Then
                RaiseEvent ClosingConnection()
                Me.Timer1.Enabled = False
                Me.Timer1.Stop()
                Me.clsDiscMarketing.CreateViewDiscount(Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString())
                Me.GridEX1.SetDataBinding(Me.clsDiscMarketing.ViewDiscount(), "")
                Me.FormatGrid()
            Else
                Me.ResultRandom += 1
            End If
            'Me.FormatGrid()
        End If
    End Sub
    Private Sub ReadAcces()
        If Not NufarmBussinesRules.User.UserLogin.IsAdmin = True Then
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscMarketing = True Then
                Me.btnComputeDiscount.Visible = True
            Else
                Me.btnComputeDiscount.Visible = False
            End If
        End If
    End Sub
    Private Sub GenerateDiscount(ByVal DISTRIBUTOR_ID As String, ByVal PROGRAM_ID As String)
        Try
            Me.clsDiscMarketing.GenerateDiscount_1(DISTRIBUTOR_ID, PROGRAM_ID)
            'Me.clsDiscMarketing.CreateViewDiscount(DISTRIBUTOR_ID, PROGRAM_ID)
            'Me.GridEX1.SetDataBinding(Me.clsDiscMarketing.ViewDiscount(), "")
            'Me.FormatGrid()
            'Me.FormatGrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetStateChecked(ByVal item As DevComponents.DotNetBar.ButtonItem)
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar1.Items
            If Not item1.Name = item.Name Then
                item1.Checked = False
            Else
                item = item1
                item.Checked = True
            End If
        Next
    End Sub
#End Region

#Region " Event Procedure "
    Private Sub DiscMarketing_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not IsNothing(Me.clsDiscMarketing) Then
                Me.clsDiscMarketing.Dispose(True)
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DiscMarketing_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.cmbDistributor.DropDownList.Columns("PROGRAM_NAME").AutoSize()
            Me.cmbDistributor.DropDownList.Columns("DISTRIBUTOR_NAME").AutoSize()
            Me.cmbDistributor.DropDownList.Columns("PROGRAM_ID").AutoSize()
            Me.FilterEditor1.Visible = False
            AddHandler Timer1.Tick, AddressOf ChekTimer
        Catch ex As Exception

        Finally
            Me.ReadAcces()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbDistributor_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDistributor.MouseHover
        Try
            Me.baseTooltip.UseAnimation = True
            Me.baseTooltip.ToolTipTitle = "Information"
            Me.baseTooltip.BackColor = Me.BackColor
            Me.baseTooltip.SetToolTip(Me.cmbDistributor, "Please clear this text if you want change view" & vbCrLf & "Clear it, and select another value you want to view")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDistributor.ValueChanged
        Try
            If Me.SFM = StateFillingCombo.Filing Then
                Return
            End If
            If IsNothing(Me.cmbDistributor.SelectedItem) Then
                Me.GridEX1.SetDataBinding(Nothing, "")
                Me.GridEX1.RootTable.Caption = ""
            Else
                Me.GridEX1.RootTable.Caption = "DISTRIBUTOR_NAME : " & Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_NAME").ToString() & _
                " PROGRAM_ID : " & Me.cmbDistributor.Value.ToString()
                'Me.clsDiscMarketing.CreateViewDistributorMarketing()
                Me.clsDiscMarketing.CreateViewDiscount(Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString())
                Me.GridEX1.SetDataBinding(Me.clsDiscMarketing.ViewDiscount(), "")
                Me.FormatGrid()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnRenameColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar1, True)
                Case "btnShowFieldChooser"
                    Me.GridEX1.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    'Me.PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                        Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    End If
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                Case "btnPageSettings"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
                Case "btnCustomFilter"
                    Me.FilterEditor1.SourceControl = Me.GridEX1
                    Me.GridEX1.RemoveFilters()
                    Me.FilterEditor1.Visible = True
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.GridEX1.RemoveFilters()
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Case "btnExport"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.GridEX1
                        Me.GridEXExporter1.SheetName = "Disc_Marketing_" & Me.cmbDistributor.Text
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnViewComputeDiscount"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define program held by distributor")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define program held by distributor")
                        Return
                    Else

                        Me.clsDiscMarketing.CreateViewDiscount(Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString())
                        Me.GridEX1.SetDataBinding(Me.clsDiscMarketing.ViewDiscount(), "")
                        Me.FormatGrid()
                    End If

                Case "btnComputeDiscount"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define program held by distributor")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define program held by distributor")
                        Return
                    ElseIf Me.ShowConfirmedMessage("Compute Discount ?" & vbCrLf & "If discount has been computed" & vbCrLf & "System will recompute discount.") = Windows.Forms.DialogResult.No Then
                        Return
                    Else
                        Me.HasgeneratedDiscount = False
                        RaiseEvent Connecting("GENERATING DISCOUNT ....")
                        Me.rnd = New Random()
                        Me.ResultRandom = Me.rnd.Next(1, 4)
                        Me.Timer1.Enabled = False
                        Me.Timer1.Start()
                        Me.GenerateDiscount(Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString())
                        Me.HasgeneratedDiscount = True
                    End If
                Case "btnRefresh"
                    Dim PROGRAM_ID As Object = Nothing
                    If Not IsNothing(Me.cmbDistributor.SelectedItem()) Then
                        PROGRAM_ID = Me.cmbDistributor.Value().ToString()
                    End If
                    'Me.cmbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
                    'Me.cmbDistributor.ValueMember = "DISTRIBUTOR_ID"
                    'Me.cmbDistributor.Value = DISTRIBUTOR_ID
                    Me.RefreshData()
                    Me.SFM = StateFillingCombo.HasFilled
                    Me.cmbDistributor.Value = Nothing
                    Me.cmbDistributor.Value = PROGRAM_ID
            End Select
            'Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
        Catch ex As Exception
            If Me.Timer1.Enabled = True Then
                Me.Timer1.Enabled = False
                Me.Timer1.Stop()
                Me.tickCount = 0
            End If
            If Not IsNothing(Me.ST) Then
                RaiseEvent ClosingConnection()
            End If
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        Finally
            'If Not IsNothing(Me.ST) Then
            '    RaiseEvent ClosingConnection()
            'End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            Me.clsDiscMarketing.ViewDistibutorMarketing().RowFilter = "DISTRIBUTOR_NAME LIKE '%" & Me.cmbDistributor.Text & "%'"
            Me.BindMulticolumnCombo(Me.clsDiscMarketing.ViewDistibutorMarketing())
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() + " item(s) found")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.tickCount += 1
    End Sub

#End Region

End Class