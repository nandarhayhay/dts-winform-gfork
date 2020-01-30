Public Class DiscProject

#Region " Deklarasi "
    Private clsProjDiscount As NufarmBussinesRules.DistributorProject.ProjectDiscount
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
    Private Sub BindMulticolumnCombo(ByVal dtView As DataView)
        Me.SFM = StateFillingCombo.Filing
        Me.cmbDistributor.SetDataBinding(dtView, "")
        Me.SFM = StateFillingCombo.HasFilled
    End Sub
    Friend Sub InitializeData()
        Me.RefreshData()
    End Sub
    Private Sub RefreshData()
        Me.clsProjDiscount = New NufarmBussinesRules.DistributorProject.ProjectDiscount()
        'Me.clsDiscMarketing = New NufarmBussinesRules.Program.MarketingDiscount()
        Me.SFM = StateFillingCombo.Filing
        Me.clsProjDiscount.CreateViewDistProject() '.CreateViewDistributorMarketing()
        Me.BindMulticolumnCombo(Me.clsProjDiscount.ViewDistProject()) '.ViewDistibutorMarketing())
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
    Private Sub ReadAcces()
        If Not NufarmBussinesRules.User.UserLogin.IsAdmin = True Then
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscProject = True Then
                Me.btnComputeDiscount.Visible = True
            Else
                Me.btnComputeDiscount.Visible = False
            End If
        End If
    End Sub
    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Me.tickCount = Me.ResultRandom Then
                If Me.HasgeneratedDiscount = True Then
                    RaiseEvent ClosingConnection()
                    Me.Timer1.Enabled = False
                    Me.Timer1.Stop()
                    Me.clsProjDiscount.CreateViewDiscount(Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString())
                    Me.GridEX1.SetDataBinding(Me.clsProjDiscount.ViewDiscount(), "")
                Else
                    Me.ResultRandom += 1
                End If
            End If
        Catch ex As Exception
            If Not IsNothing(Me.ST) Then
                Me.ST.Close()
                Me.ST = Nothing
                Me.tickCount = 0
            End If
        End Try

    End Sub
    Private Sub GenerateDiscount(ByVal DISTRIBUTOR_ID As String, ByVal PROJ_REF_NO As String)
        Try
            Me.clsProjDiscount.GenerateDiscount(DISTRIBUTOR_ID, PROJ_REF_NO)
            'Me.clsProjDiscount.CreateViewDiscount(DISTRIBUTOR_ID, PROJ_REF_NO)
            'Me.GridEX1.SetDataBinding(Me.clsProjDiscount.ViewDiscount(), "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region " Event "
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
                        Me.GridEXExporter1.SheetName = "Disc_Project_" & Me.cmbDistributor.Text
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnViewComputeDiscount"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define Project held by distributor")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define project held by distributor")
                        Return
                    Else
                        Me.clsProjDiscount.CreateViewDiscount(Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString())
                        Me.GridEX1.SetDataBinding(Me.clsProjDiscount.ViewDiscount(), "")
                    End If

                Case "btnComputeDiscount"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define project held by distributor")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define project held by distributor")
                        Return
                    ElseIf Me.ShowConfirmedMessage("Compute Discount ?" & vbCrLf & "If discount has been computed" & vbCrLf & "System will recompute discount.") = Windows.Forms.DialogResult.No Then
                        Return
                    Else
                        RaiseEvent Connecting("GENERATING DISCOUNT ....")
                        Me.rnd = New Random()
                        Me.ResultRandom = Me.rnd.Next(1, 4)
                        Me.Timer1.Enabled = False
                        Me.Timer1.Start()
                        Me.GenerateDiscount(Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString())
                        Me.HasgeneratedDiscount = True
                    End If
                Case "btnRefresh "
                    Dim PROJ_REF_NO As Object = Nothing
                    If Not IsNothing(Me.cmbDistributor.SelectedItem()) Then
                        PROJ_REF_NO = Me.cmbDistributor.Value().ToString()
                    End If
                    'Me.cmbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
                    'Me.cmbDistributor.ValueMember = "DISTRIBUTOR_ID"
                    'Me.cmbDistributor.Value = DISTRIBUTOR_ID
                    Me.RefreshData()
                    Me.SFM = StateFillingCombo.HasFilled
                    Me.cmbDistributor.Value = Nothing
                    Me.cmbDistributor.Value = PROJ_REF_NO
            End Select
        Catch ex As Exception
            If Me.Timer1.Enabled = True Then
                Me.Timer1.Enabled = False
                Me.Timer1.Stop()
            End If
            Me.tickCount = 0
            If Not IsNothing(Me.ST) Then
                RaiseEvent ClosingConnection()
            End If

            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.cmbDistributor.SelectedItem) Then
                Me.GridEX1.RootTable.Caption = "DISTRIBUTOR NAME : " & Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_NAME").ToString() & _
                ", PROJECT NAME : " & Me.cmbDistributor.Value.ToString()
                Me.clsProjDiscount.CreateViewDiscount(Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString())
                Me.GridEX1.SetDataBinding(Me.clsProjDiscount.ViewDiscount(), "")
            Else
                Me.GridEX1.RootTable.Caption = ""
            End If

        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_cmbDistributor_ValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            Me.clsProjDiscount.ViewDistProject().RowFilter = " DISTRIBUTOR_NAME LIKE '%" & Me.cmbDistributor.Text & "%'"
            Me.cmbDistributor.DropDownList().Refetch()
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DiscProject_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not IsNothing(Me.clsProjDiscount) Then
                Me.clsProjDiscount.Discpose(True)
                Me.clsProjDiscount = Nothing
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DiscProject_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.cmbDistributor.DropDownList().Columns("DISTRIBUTOR_NAME").AutoSize()
            Me.cmbDistributor.DropDownList().Columns("PROJ_REF_NO").AutoSize()
            Me.FilterEditor1.Visible = False
            AddHandler Timer1.Tick, AddressOf ChekTimer
        Catch ex As Exception

        Finally
            Me.ReadAcces()
            If Me.SFM = StateFillingCombo.Filing Then
                Me.SFM = StateFillingCombo.HasFilled
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.tickCount += 1
    End Sub
#End Region

End Class
