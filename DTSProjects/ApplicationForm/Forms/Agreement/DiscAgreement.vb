Imports System.Threading
Public Class DiscAgreement

#Region " Deklarasi "
    Private clsOA As NufarmBussinesRules.DistributorAgreement.Core
    Private SFG As StateFillingGrid
    Private SFM As StateFillingCombo
    Private Delegate Sub onConnecting(ByVal message As String)
    Private Event Connecting As onConnecting
    Private Delegate Sub onClosingConnection()
    Private Event ClosingConnection As onClosingConnection
    Dim rnd As Random
    Private Shadows tickCount As Integer
    Private SI_MCB As SelectedItemMCB
    Private ValueDistributor As Object
    Private HasComputed As Boolean = False
    Private ThreadProcess As Thread
    Private LD As Loading = Nothing
    Friend CMain As Main = Nothing
#End Region

#Region " Enum "
    Private Ticcount As Integer = 0
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private Enum StatusProgress
        None
        Processing
    End Enum
    Private Enum SelectedItemMCB
        FromMCB
        FromRefresh
    End Enum
    Private Enum StateFillingGrid
        Filling
        HasFilled
    End Enum

    Private Enum StateFillingCombo
        Filling
        HasFilled
    End Enum

    Private Enum SemesterlyDiscount
        Semesterly_1
        Semesterly_2
    End Enum

    Private Enum QuarterlyDiscount
        Quarterly_1
        Quarterly_2
        Quarterly_3
        Quarterly_4
    End Enum
    Private Sub GetStateChecked(ByVal btn As DevComponents.DotNetBar.ButtonItem, ByVal IsChecked As Boolean)
        Me.btnYear.Checked = False
        Me.btnHYear.Checked = False
        Me.btnHQ1.Checked = False
        Me.btnHQ2.Checked = False
        Me.btnHQ3.Checked = False
        Me.btnHQ4.Checked = False
        Me.btnHS1.Checked = False
        Me.btnHS2.Checked = False
        Me.btnQ1.Checked = False
        Me.btnQ2.Checked = False
        Me.btnQ3.Checked = False
        Me.btnQ4.Checked = False
        Me.btnS1.Checked = False
        Me.btnS2.Checked = False
        btn.Checked = IsChecked
    End Sub
    Private Sub GetdataChecked(ByVal btn As DevComponents.DotNetBar.ButtonItem, ByVal IsChecked As Boolean)
        Select Case btn.Name
            Case "btnQ1"
                Me.btnQ1_Click(Me.btnQ1, New System.EventArgs())
                Me.btnQ1.Checked = IsChecked
            Case "btnQ2"
                Me.btnQ2_Click(Me.btnQ2, New System.EventArgs())
                Me.btnQ2.Checked = IsChecked
            Case "btnQ3"
                Me.btnQ3_Click(Me.btnQ3, New System.EventArgs())
                Me.btnQ3.Checked = IsChecked
            Case "btnQ4"
                Me.btnQ4_Click(Me.btnQ4, New System.EventArgs())
                Me.btnQ4.Checked = IsChecked
            Case "btnS1"
                Me.btnS1_Click(Me.btnS1, New System.EventArgs())
                Me.btnS1.Checked = IsChecked
            Case "btnS2"
                Me.btnS2_Click(Me.btnS2, New System.EventArgs())
                Me.btnS2.Checked = IsChecked
            Case "btnYear"
                Me.btnYear_Click(Me.btnYear, New System.EventArgs())
                Me.btnYear.Checked = IsChecked
            Case "btnHQ1"
                Me.btnHQ1_Click(Me.btnHQ1, New System.EventArgs())
                Me.btnHQ1.Checked = IsChecked
            Case "btnHQ2"
                Me.btnHQ2_Click(Me.btnHQ2, New System.EventArgs())
                Me.btnHQ2.Checked = IsChecked
            Case "btnHQ3"
                Me.btnHQ3_Click(Me.btnHQ3, New System.EventArgs())
                Me.btnHQ3.Checked = IsChecked
            Case "btnHQ4"
                Me.btnHQ4_Click(Me.btnHQ4, New System.EventArgs())
                Me.btnHQ4.Checked = IsChecked
            Case "btnHS1"
                Me.btnHS1_Click(Me.btnHS1, New System.EventArgs())
                Me.btnHS1.Checked = IsChecked
            Case "btnHS2"
                Me.btnHS2_Click(Me.btnHS2, New System.EventArgs())
                Me.btnHS2.Checked = IsChecked
            Case "btnHYear"
                Me.btnYear_Click(Me.btnYear, New System.EventArgs())
                Me.btnYear.Checked = IsChecked
        End Select
    End Sub
#End Region

#Region " SUB "

    Private Overloads Sub ShowProgress(ByVal Message As String) Handles Me.Connecting
        Me.ST = New Progress
        Me.ST.Show(Message)
        'Me.ST.PictureBox1.Refresh()
        'Me.ST.Refresh()
        'Application.DoEvents()
    End Sub

    Private Sub closeConnection() Handles Me.ClosingConnection
        'If Not IsNothing(Me.ST) Then
        '    Me.ST.Close()
        '    Me.ST = Nothing
        '    Me.tickCount = 0
        'End If
        Me.tickCount = 0
        Me.Timer1.Enabled = False
        Me.Timer1.Stop()
    End Sub
    Private Sub ShowProcessing()
        LD = New Loading : LD.TopMost = True : LD.Show()
        While Not Me.HasComputed
            LD.Refresh() : Application.DoEvents()
            Thread.Sleep(50)
        End While
        Thread.Sleep(50) : LD.Close() : LD = Nothing
        Me.Ticcount = 0
    End Sub
    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscAgreement = True Then
                Me.btnCompute.Visible = True
            Else
                Me.btnCompute.Visible = False
            End If
        End If
    End Sub

    Friend Sub InitializeData()
        Me.RefreshData()
    End Sub

    Private Sub RefreshData()
        Me.SFM = StateFillingCombo.Filling
        Me.SFG = StateFillingGrid.Filling
        Me.clsOA = New NufarmBussinesRules.DistributorAgreement.Core()
        'If Me.btngeneratedAgreement.Checked = True Then
        '    Me.btngeneratedAgreement_Click(Me.btngeneratedAgreement, New System.EventArgs())
        'ElseIf Me.btnMustgenerate.Checked = True Then
        '    Me.btnMustgenerate_Click(Me.btnMustgenerate, New System.EventArgs())
        'Else
        '    Me.btnMustgenerate_Click(Me.btnMustgenerate, New System.EventArgs())
        'End If
        Me.clsOA.CreateViewDistributorAgreement()
        'Me.cmbDistributor.set = Me.clsOA.ViewDistributorAgreement()
        Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")

    End Sub

    Private Sub BindComboBox(ByVal dtView As DataView, ByVal rowFilter As String)
        Me.SFM = StateFillingCombo.Filling
        dtView.RowFilter = rowFilter
        Me.cmbDistributor.SetDataBinding(dtView, "")
        Me.cmbDistributor.Value = Nothing
        'Me.cmbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        'Me.cmbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.SFM = StateFillingCombo.HasFilled
    End Sub

    'Private Sub BindGrid(ByVal dtview As DataView)
    '    Me.GridEX1.SetDataBinding(dtview, "")
    'End Sub

    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.tickCount >= Me.ResultRandom Then
            If Me.HasComputed = True Then
                RaiseEvent ClosingConnection()
                Me.Timer1.Enabled = False
                Me.Timer1.Stop()
            Else
                Me.ResultRandom += 1
            End If

        End If
    End Sub

#End Region

#Region " Generating Discount "

    Private Sub GenerateQuarterlyDiscount(ByVal AGREEMENT_NO As String, ByVal Q_Flag As QuarterlyDiscount, ByVal DISTRIBUTOR_ID As String)
        Try
            Select Case Q_Flag
                Case QuarterlyDiscount.Quarterly_1
                    Me.clsOA.GenerateQuarterly1Discount_1(AGREEMENT_NO, DISTRIBUTOR_ID)
                    Me.CreateViewQuarterlyDiscont(AGREEMENT_NO, QuarterlyDiscount.Quarterly_1, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                Case QuarterlyDiscount.Quarterly_2
                    Me.clsOA.GenerateQuarterly2Discount_1(AGREEMENT_NO, DISTRIBUTOR_ID)
                    Me.CreateViewQuarterlyDiscont(AGREEMENT_NO, QuarterlyDiscount.Quarterly_2, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                Case QuarterlyDiscount.Quarterly_3
                    Me.clsOA.GenerateQuarterly3Discount_1(AGREEMENT_NO, DISTRIBUTOR_ID)
                    Me.CreateViewQuarterlyDiscont(AGREEMENT_NO, QuarterlyDiscount.Quarterly_3, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                Case QuarterlyDiscount.Quarterly_4
                    Me.clsOA.GenerateQuarterly4Discount_1(AGREEMENT_NO, DISTRIBUTOR_ID)
                    Me.CreateViewQuarterlyDiscont(AGREEMENT_NO, QuarterlyDiscount.Quarterly_4, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
            End Select
        Catch ex As Exception
            Throw ex
        End Try

        'me.clsOA.GenerateQuarterlyDiscount(AGREEMENT_NO,
    End Sub

    Private Sub GenerateSemesterlyDiscount(ByVal AGREEMENT_NO As String, ByVal S_Flag As SemesterlyDiscount, ByVal DISTRIBUTOR_ID As String)
        Try
            Select Case S_Flag
                Case SemesterlyDiscount.Semesterly_1
                    Me.clsOA.GenerateSemesterly1Discount_1(AGREEMENT_NO, DISTRIBUTOR_ID)
                    Me.CreateViewSemesterlyDiscount(AGREEMENT_NO, SemesterlyDiscount.Semesterly_1, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                Case SemesterlyDiscount.Semesterly_2
                    Me.clsOA.GenerateSemesterly2Discount_1(AGREEMENT_NO, DISTRIBUTOR_ID)
                    Me.CreateViewSemesterlyDiscount(AGREEMENT_NO, SemesterlyDiscount.Semesterly_2, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GenerateYearlyDiscount(ByVal AGREEMENT_NO As String, ByVal DISTRIBUTOR_ID As String)
        Try
            Me.clsOA.GenerateYearlyDiscount_1(AGREEMENT_NO, DISTRIBUTOR_ID)
            Me.CreateViewYearlyDiscount(AGREEMENT_NO, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub CreateViewSemesterlyDiscount(ByVal AGREEMENT_NO As String, ByVal flag As SemesterlyDiscount, ByVal DISTRIBUTOR_ID As String)
        Try
            Select Case flag
                Case SemesterlyDiscount.Semesterly_1
                    Me.clsOA.CreateViewDiscount(AGREEMENT_NO, "S1", DISTRIBUTOR_ID)
                Case SemesterlyDiscount.Semesterly_2
                    Me.clsOA.CreateViewDiscount(AGREEMENT_NO, "S2", DISTRIBUTOR_ID)
            End Select
            Me.GridEX1.SetDataBinding(Me.clsOA.ViewDiscount(), "")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreateViewQuarterlyDiscont(ByVal AGREEMENT_NO As String, ByVal Q_Flag As QuarterlyDiscount, ByVal DISTRIBUTOR_ID As String)
        Try
            Select Case Q_Flag
                Case QuarterlyDiscount.Quarterly_1
                    Me.clsOA.CreateViewDiscount(AGREEMENT_NO, "Q1", DISTRIBUTOR_ID)
                Case QuarterlyDiscount.Quarterly_2
                    Me.clsOA.CreateViewDiscount(AGREEMENT_NO, "Q2", DISTRIBUTOR_ID)
                Case QuarterlyDiscount.Quarterly_3
                    Me.clsOA.CreateViewDiscount(AGREEMENT_NO, "Q3", DISTRIBUTOR_ID)
                Case QuarterlyDiscount.Quarterly_4
                    Me.clsOA.CreateViewDiscount(AGREEMENT_NO, "Q4", DISTRIBUTOR_ID)
            End Select
            Me.GridEX1.SetDataBinding(Me.clsOA.ViewDiscount(), "")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreateViewYearlyDiscount(ByVal AGREEMENT_NO As String, ByVal DISTRIBUTOR_ID As String)
        Try
            Me.clsOA.CreateViewDiscount(AGREEMENT_NO, "Y", DISTRIBUTOR_ID)
            Me.GridEX1.SetDataBinding(Me.clsOA.ViewDiscount(), "")
        Catch ex As Exception
            'Me.ShowMessageInfo(ex.Message)
        End Try
    End Sub

#End Region

#Region " Event Procedure "

    Private Sub cmbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.cmbDistributor.Value Is Nothing Then
                Me.btnViewCompute.ShowSubItems = False
                Me.btnCompute.ShowSubItems = False
                Me.GridEX1.RootTable.Caption = ""
            ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                Me.btnViewCompute.ShowSubItems = False
                Me.btnCompute.ShowSubItems = False
                Me.GridEX1.RootTable.Caption = ""
            Else
                Me.btnViewCompute.ShowSubItems = True
                Me.btnCompute.ShowSubItems = True
                Me.ValueDistributor = Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID")
                Select Case Me.clsOA.GetPeriodTreatment(Me.cmbDistributor.Value.ToString()).ToString()
                    'CHEK APAKAH DATA ADA / BELUM 
                    Case "S"
                        Me.rdbCDPeriods1.Visible = True
                        Me.rdbCDPeriods2.Visible = True
                        Me.rdbCDPeriods3.Visible = False
                        Me.rdbCDPeriods4.Visible = False
                        Me.rdbCDPeriods1.Text = "Semesterly 1"
                        Me.rdbCDPeriods2.Text = "Semesterly 2"

                        Me.btnPeriods1.Visible = True
                        Me.btnPeriods2.Visible = True
                        Me.btnPeriods3.Visible = False
                        Me.btnPeriods4.Visible = False

                        Me.btnPeriods1.Text = "Semesterly 1"
                        Me.btnPeriods2.Text = "Semesterly 2"
                        'Me.GridEX1.RootTable.Caption = "DISTRIBUTOR : " & Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_NAME").ToString() _
                        '& " | TYPE : SEMESTERLY | AGREEMENT_NO : " & Me.cmbDistributor.Value.ToString()
                    Case "Q"
                        Me.rdbCDPeriods1.Visible = True
                        Me.rdbCDPeriods2.Visible = True
                        Me.rdbCDPeriods3.Visible = True
                        Me.rdbCDPeriods4.Visible = True

                        Me.btnPeriods1.Visible = True
                        Me.btnPeriods2.Visible = True
                        Me.btnPeriods3.Visible = True
                        Me.btnPeriods4.Visible = True

                        Me.rdbCDPeriods1.Text = "Quarterly 1"
                        Me.rdbCDPeriods2.Text = "Quarterly 2"
                        Me.rdbCDPeriods3.Text = "Quarterly 3"
                        Me.rdbCDPeriods4.Text = "Quarterly 4"

                        Me.btnPeriods1.Text = "Quarterly 1"
                        Me.btnPeriods2.Text = "Quarterly 2"
                        Me.btnPeriods3.Text = "Quarterly 3"
                        Me.btnPeriods4.Text = "Quarterly 4"
                        'Me.GridEX1.RootTable.Caption = "DISTRIBUTOR : " & Me.cmbDistributor.Text & " | TYPE : QUARTERLY | AGREEMENT_NO : " & Me.cmbDistributor.Value.ToString()
                End Select
                Me.GridEX1.RootTable.Caption = "DISTRIBUTOR : " & Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_NAME").ToString() _
                      & " | TYPE : SEMESTERLY | AGREEMENT_NO : " & Me.cmbDistributor.Value.ToString()
                If Me.SI_MCB = SelectedItemMCB.FromRefresh Then
                    Me.clsOA.CreateViewDiscount(Me.cmbDistributor.Value.ToString(), DBNull.Value, Me.ValueDistributor.ToString())
                Else
                    Me.clsOA.CreateViewDiscount(Me.cmbDistributor.Value.ToString(), DBNull.Value, Me.ValueDistributor)
                End If
                Me.GridEX1.SetDataBinding(Me.clsOA.ViewDiscount(), "")
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnRenameColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False : MC.grid = Me.GridEX1
                    MC.FillcomboColumn() : MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True : MC.Show(Me.Bar1, True)
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
                        Me.GridEXExporter1.SheetName = "Disc_Agreement_" & Me.cmbDistributor.Text
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "rdbCDPeriods1"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    ElseIf Me.ShowConfirmedMessage("Compute Discount ?" & vbCrLf & "If discount has been computed" & vbCrLf & "system will recompute discount") = Windows.Forms.DialogResult.No Then
                        Return
                    Else
                        If Me.clsOA.IsDistributorHoldAgreement(Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString()) = True Then
                            'RaiseEvent Connecting("GENERATING DISCOUNT ....")
                            HasComputed = False
                            'Me.rnd = New Random()
                            'Me.ResultRandom = Me.rnd.Next(1, 5)
                            'Me.Timer1.Enabled = False
                            'Me.Timer1.Start()
                            ThreadProcess = New Thread(AddressOf ShowProcessing)
                            ThreadProcess.Start()
                            Select Case Me.rdbCDPeriods1.Text
                                Case "Semesterly 1"
                                    'CHECK DISTRIBUTOR AGREEMENT
                                    Me.GenerateSemesterlyDiscount(Me.cmbDistributor.Value.ToString(), SemesterlyDiscount.Semesterly_1, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                                    Me.GetStateChecked(Me.btnS1, True)
                                Case "Quarterly 1"
                                    Me.GenerateQuarterlyDiscount(Me.cmbDistributor.Value.ToString(), QuarterlyDiscount.Quarterly_1, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                                    Me.GetStateChecked(Me.btnQ1, True)
                            End Select
                        End If
                        'ME.GenerateQuarterlyDiscount(
                    End If

                Case "rdbCDPeriods2"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    Else
                        If Me.clsOA.IsDistributorHoldAgreement(Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString()) = True Then
                            'RaiseEvent Connecting("GENERATING DISCOUNT ....")
                            HasComputed = False
                            'Me.rnd = New Random()
                            'Me.ResultRandom = Me.rnd.Next(1, 5)
                            'Me.Timer1.Enabled = False
                            'Me.Timer1.Start()
                            ThreadProcess = New Thread(AddressOf ShowProcessing)
                            ThreadProcess.Start()
                            Select Case Me.rdbCDPeriods2.Text
                                Case "Quarterly 2"
                                    Me.GenerateQuarterlyDiscount(Me.cmbDistributor.Value.ToString(), QuarterlyDiscount.Quarterly_2, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                                    Me.GetStateChecked(Me.btnQ2, True)
                                Case "Semesterly 2"
                                    Me.GenerateSemesterlyDiscount(Me.cmbDistributor.Value.ToString(), SemesterlyDiscount.Semesterly_2, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                                    Me.GetStateChecked(Me.btnS2, True)
                            End Select
                        End If
                    End If
                Case "rdbCDPeriods3"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    Else
                        If Me.clsOA.IsDistributorHoldAgreement(Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString()) = True Then
                            'RaiseEvent Connecting("GENERATING DISCOUNT ....")
                            HasComputed = False
                            'Me.rnd = New Random()
                            'Me.ResultRandom = Me.rnd.Next(1, 5)
                            'Me.Timer1.Enabled = False
                            'Me.Timer1.Start()
                            ThreadProcess = New Thread(AddressOf ShowProcessing)
                            ThreadProcess.Start()
                            Me.GenerateQuarterlyDiscount(Me.cmbDistributor.Value.ToString(), QuarterlyDiscount.Quarterly_3, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                            Me.GetStateChecked(Me.btnQ3, True)
                        End If
                    End If
                Case "rdbCDPeriods4"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    Else
                        If Me.clsOA.IsDistributorHoldAgreement(Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString()) = True Then
                            'RaiseEvent Connecting("GENERATING DISCOUNT ....")
                            HasComputed = False
                            'Me.rnd = New Random()
                            'Me.ResultRandom = Me.rnd.Next(1, 5)
                            'Me.Timer1.Enabled = False
                            'Me.Timer1.Start()
                            ThreadProcess = New Thread(AddressOf ShowProcessing)
                            ThreadProcess.Start()
                            Me.GenerateQuarterlyDiscount(Me.cmbDistributor.Value.ToString(), QuarterlyDiscount.Quarterly_4, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                            Me.GetStateChecked(Me.btnQ4, True)
                        End If

                    End If
                Case "rdbCDYearly"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    Else
                        If Me.clsOA.IsDistributorHoldAgreement(Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString(), Me.cmbDistributor.Value.ToString()) = True Then
                            'RaiseEvent Connecting("GENERATING DISCOUNT ....")

                            HasComputed = False
                            'Me.rnd = New Random()
                            'Me.ResultRandom = Me.rnd.Next(1, 5)
                            'Me.Timer1.Enabled = False
                            'Me.Timer1.Start()

                            ThreadProcess = New Thread(AddressOf ShowProcessing)
                            ThreadProcess.Start()

                            Me.GenerateYearlyDiscount(Me.cmbDistributor.Value.ToString(), Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                            Me.GetStateChecked(Me.btnYear, True)
                        End If

                    End If
                Case "btnPeriods1"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    Else
                        Select Case Me.btnPeriods1.Text
                            Case "Quarterly 1"
                                Me.CreateViewQuarterlyDiscont(Me.cmbDistributor.Value.ToString(), QuarterlyDiscount.Quarterly_1, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                            Case "Semesterly 1"
                                Me.CreateViewSemesterlyDiscount(Me.cmbDistributor.Value.ToString(), SemesterlyDiscount.Semesterly_1, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                        End Select
                    End If
                Case "btnPeriods2"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    Else
                        Select Case Me.btnPeriods2.Text
                            Case "Quarterly 2"
                                Me.CreateViewQuarterlyDiscont(Me.cmbDistributor.Value.ToString(), QuarterlyDiscount.Quarterly_2, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                            Case "Semesterly 2"
                                Me.CreateViewSemesterlyDiscount(Me.cmbDistributor.Value.ToString(), SemesterlyDiscount.Semesterly_2, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                        End Select
                    End If
                Case "btnPeriods3"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    Else
                        Me.CreateViewQuarterlyDiscont(Me.cmbDistributor.Value.ToString(), QuarterlyDiscount.Quarterly_3, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                    End If
                Case "btnPeriods4"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    Else
                        Me.CreateViewQuarterlyDiscont(Me.cmbDistributor.Value.ToString(), QuarterlyDiscount.Quarterly_4, Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                    End If
                Case "btnYearly"
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    ElseIf Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor_Agreement")
                        Return
                    Else
                        Me.CreateViewYearlyDiscount(Me.cmbDistributor.Value.ToString(), Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString())
                    End If
                Case "btnRefresh"
                    Dim AGREEMENT_NO As Object = Nothing
                    If Not IsNothing(Me.cmbDistributor.SelectedItem()) Then
                        AGREEMENT_NO = Me.cmbDistributor.Value()
                        Me.ValueDistributor = Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID")
                    End If
                    Me.SFM = StateFillingCombo.Filling
                    'Me.cmbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
                    'Me.cmbDistributor.ValueMember = "DISTRIBUTOR_ID"
                    Me.RefreshData()
                    'Me.cmbDistributor.Value = AGREEMENT_NO
                    Me.SFM = StateFillingCombo.HasFilled
                    Me.cmbDistributor.Value = Nothing
                    Me.SI_MCB = SelectedItemMCB.FromRefresh

                    Me.cmbDistributor.Value = AGREEMENT_NO
                    Me.SI_MCB = SelectedItemMCB.FromMCB
                Case "btnCompute"
                    If (Me.cmbDistributor.Value Is Nothing) Or (Me.cmbDistributor.SelectedItem Is Nothing) Then
                        Me.btnCompute.ShowSubItems = False
                    Else
                        Me.btnCompute.ShowSubItems = True
                    End If
                Case "btnViewCompute"
                    If (Me.cmbDistributor.Value Is Nothing) Or (Me.cmbDistributor.SelectedItem Is Nothing) Then
                        Me.btnViewCompute.ShowSubItems = False
                    Else
                        Me.btnViewCompute.ShowSubItems = True
                    End If
            End Select

            HasComputed = True
        Catch ex As Exception
            HasComputed = True
            If Me.Timer1.Enabled = True Then
                Me.Timer1.Enabled = False
                Me.Timer1.Stop()
            End If
            'RaiseEvent ClosingConnection()
            'If Not IsNothing(Me.ST) Then
            '    RaiseEvent ClosingConnection()
            'End If
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        Finally
            'If Not IsNothing(Me.ST) Then
            '    RaiseEvent ClosingConnection()
            'End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DiscAgreement_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.Dispose(True)
            Me.Dispose(True)
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsOA.ViewDistributorAgreement()) Then
                Dim rowFilter As String = "DISTRIBUTOR_NAME LIKE '%" & Me.cmbDistributor.Text & "%'"
                'Me.clsOA.ViewDistributorAgreement().RowFilter = rowFilter
                Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), rowFilter)
                Dim iTemCount As Integer = Me.cmbDistributor.DropDownList().RecordCount()
                Me.ShowMessageInfo(iTemCount.ToString() + " Item(s) found")
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DiscAgreement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.btnPeriods1.Visible = False
            Me.btnPeriods2.Visible = False
            Me.btnPeriods3.Visible = False
            Me.btnPeriods4.Visible = False
            Me.FilterEditor1.Visible = False

            Me.btnViewCompute.ShowSubItems = False
            Me.btnCompute.ShowSubItems = False
            AddHandler Timer1.Tick, AddressOf ChekTimer
        Catch ex As Exception
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_DiscAgreement_Load")
        Finally
            Me.ReadAcces() : CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.tickCount += 1
    End Sub

    Private Sub btnHQ1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHQ1.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(True, "Q1")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnHQ1, True)
        Catch ex As Exception
            Me.GetStateChecked(btnHQ1, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnHQ2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHQ2.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(True, "Q2")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnHQ2, True)
        Catch ex As Exception
            Me.GetStateChecked(btnHQ2, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnHQ3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHQ3.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(True, "Q3")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnHQ3, True)
        Catch ex As Exception
            Me.GetStateChecked(btnHQ3, False)

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnHQ4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHQ4.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(True, "Q4")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnHQ4, True)
        Catch ex As Exception
            Me.GetStateChecked(btnHQ4, False)

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnHS1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHS1.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(True, "S1")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnHS1, True)
        Catch ex As Exception
            Me.GetStateChecked(btnHS1, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnHS2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHS2.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(True, "S2")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnHS2, True)
        Catch ex As Exception
            Me.GetStateChecked(btnHS2, False)

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnHYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHYear.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(True, "Y")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnHYear, True)
        Catch ex As Exception
            Me.GetStateChecked(btnHYear, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYear.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(False, "Y")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnYear, True)
        Catch ex As Exception
            Me.GetStateChecked(btnYear, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnQ1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQ1.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(False, "Q1")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnQ1, True)
        Catch ex As Exception
            Me.GetStateChecked(btnQ1, False)

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnQ2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQ2.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(False, "Q2")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnQ2, True)
        Catch ex As Exception
            Me.GetStateChecked(btnQ2, False)

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnQ3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQ3.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(False, "Q3")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnQ3, True)
        Catch ex As Exception
            Me.GetStateChecked(btnQ3, False)

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnQ4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQ4.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(False, "Q4")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnQ4, True)
        Catch ex As Exception
            Me.GetStateChecked(btnQ4, False)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnS1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnS1.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(False, "S1")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnS1, True)
        Catch ex As Exception
            Me.GetStateChecked(btnS1, False)

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnS2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnS2.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.CreateViewDistributorAgreement(False, "S2")
            Me.BindComboBox(Me.clsOA.ViewDistributorAgreement(), "")
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
            Me.GetStateChecked(Me.btnS2, True)
        Catch ex As Exception
            Me.GetStateChecked(btnS2, False)

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterAgreement_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterAgreement.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dtview As DataView = CType(Me.cmbDistributor.DataSource, DataView)
            dtview.RowFilter = "DISTRIBUTOR_NAME LIKE '%" & Me.cmbDistributor.Text & "%'"
            Me.cmbDistributor.Value = Nothing
            Me.cmbDistributor.DropDownList.Refetch()
            Dim itemCount As Integer = Me.cmbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.clsOA.Delete(Me.GridEX1.GetValue("BRND_B_S_ID").ToString())
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            e.Cancel = True
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

End Class