Imports System.Threading
Public Class DPRD
    Private clsReaching As New NufarmBussinesRules.Kios.ReachingTarget
    Private IsLoadingCombo As Boolean = False
    Private Delegate Sub onShowingProgress(ByVal message As String)
    Private Event ShowProgres As onShowingProgress
    Private Delegate Sub onClossingProgress()
    Private Event CloseProgress As onClossingProgress
    Private tickCount As Integer
    Private WithEvents ST As DTSProjects.Progress
    Dim rnd As Random
    Private ResultRandom As Integer
    Private HasLoadReport As Boolean
    Private IsFailedToLoad As Boolean
    Private m_grid As Janus.Windows.GridEX.GridEX
    Public FE As Janus.Windows.FilterEditor.FilterEditor
    Private IsloadingRow As Boolean = False
    Private DVHeader As DataView, DVDetail As DataView, DVKios As New DataView()
    Private ReportType As CategoryReport
    Private LD As Loading
    Private CounterTimer2 As Integer = 0
    Private Ticcount As Integer = 0
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
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
    Public Property Grid() As Janus.Windows.GridEX.GridEX
        Get
            If Me.AG = ActiveGrid.GrdDetail Then
                m_grid = Me.grdDetail
            ElseIf Me.AG = ActiveGrid.GrdHeader Then
                m_grid = Me.grdHeader
            End If
            Return m_grid
        End Get
        Set(ByVal value As Janus.Windows.GridEX.GridEX)
            m_grid = value
        End Set
    End Property

    Private AG As ActiveGrid

    Public Enum ActiveGrid
        GrdHeader
        GrdDetail
    End Enum

    Private Enum CategoryReport
        None
        TargetReaching
        POAndTargetReaching
    End Enum

    Private Sub closeProgres() Handles Me.CloseProgress
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

    Private Overloads Sub ShowProgress(ByVal Message As String) Handles Me.ShowProgres
        Me.ST = New Progress
        Application.DoEvents()
        Me.ST.Show(Message)
        'Me.ST.PictureBox1.Refresh()
        'Me.ST.Refresh()
    End Sub

    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.tickCount >= Me.ResultRandom Then
            If Me.HasLoadReport = True Then
                Me.IsloadingRow = True
                If Not Me.IsFailedToLoad Then
                    Me.grdHeader.DataSource = Me.DVHeader : Me.grdHeader.RetrieveStructure()
                    Me.grdDetail.DataSource = Me.DVDetail : Me.grdDetail.RetrieveStructure()
                    Me.grdHeader.Update() : Me.grdDetail.Update()
                    If Not IsNothing(Me.grdHeader.DataSource) And Not IsNothing(Me.grdDetail.DataSource) Then
                        Me.FormatGrid()
                    End If
                Else
                    Me.grdDetail.DataSource = Nothing : Me.grdHeader.DataSource = Nothing
                End If
                RaiseEvent CloseProgress()
                Me.IsloadingRow = False
            Else
                Me.ResultRandom += 1
            End If
        End If
    End Sub
    Private Sub ChekTimer2(ByVal sender As Object, ByVal e As EventArgs)
        If Me.CounterTimer2 > 1 Then
            Me.Timer2.Stop() : Me.Timer2.Enabled = False
            Me.CounterTimer2 = 0
            Me.getData(Me.ReportType)
            Me.HasLoadReport = True
        End If

    End Sub
    Private Sub FillComboProgram(ByVal SpCode As String)
        Me.IsLoadingCombo = True : Me.mcbProgram.SetDataBinding(Me.clsReaching.GetDPRD(String.Empty), "") : Me.IsLoadingCombo = False
    End Sub

    Friend Sub RefreshData()
        If Me.rdbReachingTarget.Checked Then
            Me.rdbReachingTarget_CheckedChanged(Me.rdbReachingTarget, New EventArgs())
        ElseIf Me.rdbSumaryDPRD.Checked Then
            Me.rdbSumaryDPRD_CheckedChanged(Me.rdbSumaryDPRD, New EventArgs())
        End If
    End Sub

    Private Sub FormatGrid()
        Select Case Me.ReportType
            Case CategoryReport.POAndTargetReaching
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdHeader.RootTable.Columns
                    If col.Type Is Type.GetType("System.Decimal") Then
                        col.FormatString = "#,##0.000"
                        col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        col.TotalFormatString = "#,##0.000"
                    End If
                    If col.Key = "DisproSP" Or col.Key = "ActualDispro" Then
                        col.FormatString = "p"
                        col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    End If
                    col.AutoSize()
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit

                Next
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdDetail.RootTable.Columns
                    If col.Type Is Type.GetType("System.Decimal") Then
                        col.FormatString = "#,##0.000"
                        col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        col.TotalFormatString = "#,##0.000"
                    End If
                    col.AutoSize()
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                Next

                If Me.grdHeader.View = Janus.Windows.GridEX.View.TableView Then
                    Me.grdHeader.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.grdHeader.RootTable.Columns("DISTRIBUTOR_NAME")))
                    Me.grdHeader.RootTable.Groups(Me.grdHeader.RootTable.Columns("DISTRIBUTOR_NAME")) _
                    .GroupPrefix = "DPRDS " & Me.mcbProgram.Text & " " & "Periode : " & Me.lblStartDate.Text & " - " _
                     & Me.lblEndDate.Text & "." & " Distributor Name : "
                    Me.grdHeader.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.grdHeader.RootTable.Columns("BRAND_NAME")))
                    Me.grdHeader.RootTable.Columns("BRAND_NAME").DefaultGroupPrefix = ""
                End If
                Me.grdHeader.RootTable.Columns("DISTRIBUTOR_NAME").Visible = False
                Me.grdHeader.RootTable.Columns("BRAND_ID").Visible = False
                Me.grdHeader.RootTable.Columns("BRANDPACK_ID").Visible = False
                Me.grdHeader.RootTable.Caption = "Header Data for DPRDS " & Me.mcbProgram.Text
                Me.grdDetail.RootTable.Columns("BRAND_ID").Visible = False
                Me.grdDetail.RootTable.Columns("BRAND_NAME").Visible = False
                Me.grdDetail.RootTable.Columns("BRANDPACK_ID").Visible = False
                Me.grdHeader.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
                Me.grdHeader.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup

            Case CategoryReport.TargetReaching
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdHeader.RootTable.Columns
                    If (col.Key = "TotalActual") Or (col.Key = "Target") Or (col.Key = "TotalBonus") Then
                        col.FormatString = "#,##0.000"
                        col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        col.TotalFormatString = "#,##0.000"
                    ElseIf (col.Key = "DisproSP") Or (col.Key = "ActualDispro") Then
                        col.FormatString = "p"
                        col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        col.TotalFormatString = "p"
                    End If
                    col.AutoSize()
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                Next
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdDetail.RootTable.Columns
                    If (col.Key = "TotalPOFromDistributor") Or (col.Key = "DiscQty") Then
                        col.FormatString = "#,##0.000"
                        col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        col.TotalFormatString = "#,##0.000"
                    ElseIf col.Key = "KiosCode" Then
                        col.Caption = "IDKios"
                        col.Visible = False
                        col.ShowInFieldChooser = False
                    End If
                    col.AutoSize()
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                Next
                Me.grdHeader.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.grdHeader.RootTable.Columns("IDKios")))
                Me.grdHeader.RootTable.Columns("IDKios").Visible = False
                Me.grdHeader.RootTable.Columns("BRAND_ID").Visible = False
                Me.grdHeader.RootTable.Groups(Me.grdHeader.RootTable.Columns("IDKios")).GroupPrefix = ""
                Me.grdDetail.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.grdDetail.RootTable.Columns("IDKios")))
                Me.grdDetail.RootTable.Groups(Me.grdDetail.RootTable.Columns("IDKios")).GroupPrefix = ""
                Me.grdDetail.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.grdDetail.RootTable.Columns("Description")))
                Me.grdDetail.RootTable.Groups(Me.grdDetail.RootTable.Columns("Description")).GroupPrefix = ""

                Me.grdDetail.RootTable.Columns("IDKios").Visible = False
                Me.grdDetail.RootTable.Columns("Description").Visible = False
                Me.grdDetail.RootTable.Columns("BRAND_ID").Visible = False
                Me.grdDetail.RootTable.Columns("BRANDPACK_ID").Visible = True
                Me.grdDetail.RootTable.Columns("BRAND_NAME").Visible = False
        End Select
        Me.grdHeader.RootTable.Caption = "Header Data"
        Me.grdDetail.RootTable.Caption = "Detail Data"
    End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.IsLoadingCombo = True
            Dim DV As DataView = DirectCast(Me.chkDistributor.DropDownDataSource, DataView)
            DV.RowFilter = "DISTRIBUTOR_NAME LIKE '%" & Me.chkDistributor.Text & "%'"
            Dim ItemCount As Integer = DV.Count : MessageBox.Show(ItemCount.ToString() & " item(s) found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.chkDistributor.SetDataBinding(DV, "")
        Catch

        Finally
            Me.IsLoadingCombo = False : Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnFilterProgram_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterProgram.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.IsLoadingCombo = True
            Dim Dv As DataView = Me.clsReaching.GetDPRD(Me.mcbProgram.Text)
            Me.mcbProgram.SetDataBinding(Dv, "")
            MessageBox.Show(Dv.Count.ToString() & " item(s) found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch
        Finally
            Me.IsLoadingCombo = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbAllDistributor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAllDistributor.CheckedChanged
        If Me.IsLoadingCombo Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mcbProgram.SelectedIndex = -1 Then : Return : End If
            Me.IsLoadingCombo = True : Me.chkDistributor.Text = ""
            Me.chkDistributor.ReadOnly = True
            If Me.rdbAllDistributor.Checked Then
                If Me.rdbSumaryDPRD.Checked Then
                    Me.rdbSumaryDPRD_CheckedChanged(Me.rdbSumaryDPRD, New EventArgs())
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.IsLoadingCombo = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbPerDistributor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPerDistributor.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mcbProgram.SelectedIndex = -1 Then : Return : End If
            'clearkan datagrid
            If Me.rdbPerDistributor.Checked Then
                Me.grdDetail.DataSource = Nothing
                Me.chkDistributor.ReadOnly = False
                Me.rdbSumaryDPRD_CheckedChanged(Me.rdbSumaryDPRD, New EventArgs())
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkDistributor_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDistributor.CheckedValuesChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If (Me.rdbSumaryDPRD.Checked) Then
                Me.rdbSumaryDPRD_CheckedChanged(Me.rdbSumaryDPRD, New EventArgs())
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DPRD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            'isi chkCombo
            'isi mcbProgram
            Me.pnlViewbyDistributor.Visible = False : Me.pnlViewByKios.Visible = False
            Me.chkDistributor.ReadOnly = True : Me.FillComboProgram(String.Empty)
            AddHandler Timer1.Tick, AddressOf ChekTimer
            AddHandler Timer2.Tick, AddressOf ChekTimer2
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DPRD_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsReaching.Dispose(True)
        Catch
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbReachingTarget_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbReachingTarget.CheckedChanged
        Try
            If Me.rdbReachingTarget.Checked Then
                DVHeader = New DataView() : DVDetail = New DataView()
                Me.pnlViewbyDistributor.Hide()
                Me.ReportType = CategoryReport.TargetReaching
                If Me.mcbProgram.SelectedIndex = -1 Then
                    Me.grdDetail.DataSource = Nothing
                    Me.grdHeader.DataSource = Nothing
                    Return
                Else
                    Me.HasLoadReport = False : Me.StatProg = StatusProgress.Processing
                    Me.Cursor = Cursors.WaitCursor
                    Me.ThreadProgress = New Thread(AddressOf ShowProceed)
                    Me.ThreadProgress.Start()
                    Me.getData(Me.ReportType)
                    If Not Me.IsFailedToLoad Then
                        Me.IsloadingRow = True
                        Me.grdHeader.DataSource = Me.DVHeader : Me.grdHeader.RetrieveStructure()
                        Me.grdDetail.DataSource = Me.DVDetail : Me.grdDetail.RetrieveStructure()
                        Me.grdHeader.Update() : Me.grdDetail.Update()
                        If Not IsNothing(Me.grdHeader.DataSource) And Not IsNothing(Me.grdDetail.DataSource) Then
                            Me.FormatGrid()
                        End If
                    Else
                        Me.grdDetail.DataSource = Nothing : Me.grdHeader.DataSource = Nothing
                    End If
                End If
            End If
            Me.IsloadingRow = False
        Catch ex As Exception
            Me.HasLoadReport = True : Me.IsFailedToLoad = True : Me.IsloadingRow = False
            Me.StatProg = StatusProgress.None
            'RaiseEvent CloseProgress()
            MessageBox.Show(ex.Message) : Me.Cursor = Cursors.Default
            'Finally
            '    Me.IsLoadingCombo = False : Me.Cursor = Cursors.Default : Me.HasLoadReport = True
        End Try
    End Sub
    Private Sub getData(ByVal cr As CategoryReport)
        Try
            Select Case cr
                Case CategoryReport.POAndTargetReaching
                    If Me.rdbPerDistributor.Checked = True Then
                        Me.Cursor = Cursors.WaitCursor
                        'Me.ReportType = CategoryReport.POAndTargetReaching
                        If Not IsNothing(Me.chkDistributor.CheckedValues) Then
                            If Me.chkDistributor.CheckedValues.Length > 0 Then

                                Dim ListDistributor As New List(Of String)
                                For i As Integer = 0 To Me.chkDistributor.CheckedValues.Length - 1
                                    If (Not ListDistributor.Contains(Me.chkDistributor.CheckedValues.GetValue(i).ToString())) Then
                                        ListDistributor.Add(Me.chkDistributor.CheckedValues().GetValue(i).ToString())
                                    End If
                                Next
                                'Me.Cursor = Cursors.WaitCursor
                                'Me.HasLoadReport = False
                                'LD = New Loading
                                'LD.Show() : LD.TopMost = True
                                'Application.DoEvents()

                                'Me.rnd = New Random() : Me.ResultRandom = Me.rnd.Next(1, 6)
                                'Me.Timer1.Enabled = True : Me.Timer1.Start()
                                'Application.DoEvents() : Me.HasLoadReport = False
                                'Me.Timer2.Enabled = True : Me.Timer2.Start()
                                Me.clsReaching.GetPOTargetReaching(Me.mcbProgram.Value.ToString(), Convert.ToDateTime(Me.lblStartDate.Text), _
                                Convert.ToDateTime(Me.lblEndDate.Text), False, ListDistributor, DVHeader, DVDetail)
                            Else
                                Me.StatProg = StatusProgress.None : Me.Cursor = Cursors.Default : Me.IsFailedToLoad = True : Me.HasLoadReport = True : Return
                            End If
                        Else
                            Me.StatProg = StatusProgress.None : Me.Cursor = Cursors.Default : Me.IsFailedToLoad = True : Me.HasLoadReport = True : Return
                        End If
                    ElseIf Me.rdbAllDistributor.Checked Then
                        'get summary report
                        Me.clsReaching.GetPOTargetReaching(Me.mcbProgram.Value.ToString(), Convert.ToDateTime(Me.lblStartDate.Text), _
                        Convert.ToDateTime(Me.lblEndDate.Text), True, Nothing, DVHeader, DVDetail)
                    End If
                    Me.pnlViewByKios.Visible = False
                    Me.pnlViewbyDistributor.Visible = True : Me.IsLoadingCombo = True : Me.chkKios.SetDataBinding(Nothing, "")
                    Me.rdbPerKios.Checked = False : Me.rdbAllKios.Checked = False
                    If Me.rdbAllDistributor.Checked Then
                        Me.chkDistributor.ReadOnly = True
                    Else
                        Me.chkDistributor.ReadOnly = False
                    End If
                Case CategoryReport.TargetReaching
                    Me.clsReaching.GetTargetReaching(Me.mcbProgram.Value.ToString(), DVHeader, DVDetail, DVKios)
                    Me.IsLoadingCombo = True
                    Me.chkKios.SetDataBinding(DVKios, "")
                    Me.pnlViewbyDistributor.Visible = False
                    Me.pnlViewByKios.Visible = True : Me.rdbAllDistributor.Checked = False
                    Me.rdbPerDistributor.Checked = False : Me.chkDistributor.UncheckAll()
                    Me.btnFilterKios.Enabled = False : Me.chkKios.ReadOnly = True
                    Me.rdbAllKios.Checked = True
            End Select
            Me.IsFailedToLoad = False : Me.HasLoadReport = True
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            Me.HasLoadReport = True : Me.IsFailedToLoad = True : Throw ex
        Finally
            Me.IsLoadingCombo = False : Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub rdbSumaryDPRD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSumaryDPRD.CheckedChanged
        Try
            If Me.mcbProgram.SelectedIndex = -1 Then : Return : End If
            Me.ReportType = CategoryReport.POAndTargetReaching
            DVHeader = New DataView() : DVDetail = New DataView()
            If Me.rdbSumaryDPRD.Checked Then
                'If IsNothing(Me.chkDistributor.CheckedValues) Then : Return
                'ElseIf Me.chkDistributor.CheckedValues.Length <= 0 Then : Return
                'End If
                Me.Cursor = Cursors.WaitCursor
                If Me.rdbAllDistributor.Checked = False And Me.rdbPerDistributor.Checked = False Then
                    Me.IsLoadingCombo = True
                    Me.rdbAllDistributor.Checked = True
                    Me.IsLoadingCombo = False
                End If
                Me.StatProg = StatusProgress.Processing : Me.HasLoadReport = False
                Me.ThreadProgress = New Thread(AddressOf ShowProceed) : Me.ThreadProgress.Start()
                Me.getData(Me.ReportType)
                If Not Me.IsFailedToLoad Then
                    Me.IsloadingRow = True
                    Me.grdHeader.DataSource = Nothing : Me.grdHeader.Update()
                    Me.grdHeader.DataSource = Me.DVHeader : Me.grdHeader.RetrieveStructure()
                    Me.grdDetail.DataSource = Nothing : Me.grdDetail.Update()
                    Me.grdDetail.DataSource = Me.DVDetail : Me.grdDetail.RetrieveStructure()
                    Me.grdHeader.Update() : Me.grdDetail.Update()
                    If Not IsNothing(Me.grdHeader.DataSource) And Not IsNothing(Me.grdDetail.DataSource) Then
                        Me.FormatGrid()
                    End If
                Else
                    Me.grdDetail.DataSource = Nothing : Me.grdHeader.DataSource = Nothing
                End If
                Me.IsloadingRow = False
            End If
        Catch ex As Exception
            Me.HasLoadReport = True : Me.IsFailedToLoad = True
            Me.StatProg = StatusProgress.None
            Me.Cursor = Cursors.Default : Me.IsloadingRow = False
            'RaiseEvent CloseProgress()
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Finally
            '    Me.Cursor = Cursors.Default : Me.HasLoadReport = True
        End Try
    End Sub

    Private Sub mcbProgram_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbProgram.ValueChanged
        Try
            If Me.IsLoadingCombo Then : Return : End If
            If Me.mcbProgram.SelectedIndex = -1 Then : Me.lblEndDate.Text = "" : Me.lblStartDate.Text = "" : Return : End If
            Me.Cursor = Cursors.WaitCursor : Me.rdbReachingTarget.Checked = False
            Me.rdbSumaryDPRD.Checked = False
            Me.grdDetail.DataSource = Nothing : Me.grdHeader.DataSource = Nothing
            Me.grdHeader.RetrieveStructure() : Me.grdDetail.RetrieveStructure()
            Me.grdHeader.Update() : Me.grdDetail.Update()
            'isi combobox chk distributor
            Dim StartDate As DateTime = Now, EndDate As DateTime = Now
            Me.chkDistributor.SetDataBinding(Me.clsReaching.GetDistributorIncluded(Me.mcbProgram.Value.ToString(), StartDate, EndDate), "")
            Me.lblStartDate.Text = StartDate.ToLongDateString() : Me.lblEndDate.Text = EndDate.ToLongDateString()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdDetail_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDetail.Enter
        Me.pnlDetail.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        Me.pnlHeader.BorderStyle = Windows.Forms.BorderStyle.None
        Me.AG = ActiveGrid.GrdDetail
        If Not IsNothing(Me.FE) Then
            Dim S As Boolean = Me.FE.Visible
            Me.FE.SourceControl = Me.grdDetail
            Me.FE.Visible = S
        End If
    End Sub

    Private Sub grdHeader_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdHeader.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.IsloadingRow Then : Return : End If
            If Me.grdHeader.DataSource Is Nothing Then : Return : End If
            If Me.grdHeader.RecordCount <= 0 Then : Return : End If
            Dim RowFilter As String = ""
            Select Case Me.ReportType
                Case CategoryReport.POAndTargetReaching
                    If Me.grdHeader.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                        If Not IsNothing(Me.grdDetail.DataSource) Then
                            RowFilter = "DISTRIBUTOR_ID = '" & Me.grdHeader.GetValue("DISTRIBUTOR_ID").ToString() _
                            & "' AND BRANDPACK_NAME = '" & Me.grdHeader.GetValue("BRANDPACK_NAME").ToString() & "'"
                            Me.grdDetail.RootTable.Groups.Clear() : Me.grdDetail.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.grdDetail.RootTable.Columns("BRANDPACK_NAME")))
                            Me.grdDetail.RootTable.Groups(Me.grdDetail.RootTable.Columns("BRANDPACK_NAME")).GroupPrefix = ""

                        End If
                    ElseIf Me.grdHeader.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Then
                        If (Me.grdHeader.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader) Then
                            'RowFilter = "DISTRIBUTOR_NAME = '" & Me.grdHeader.GetValue(Me.grdHeader.GetRow().Group.Column).ToString()
                            Return
                        End If
                    ElseIf Not IsNothing(Me.chkDistributor.CheckedValues) Then
                        Me.grdDetail.RootTable.Groups.Clear()
                        RowFilter = "DISTRIBUTOR_ID IN('"
                        For i As Integer = 0 To Me.chkDistributor.CheckedValues.Length - 1
                            RowFilter &= Me.chkDistributor.CheckedValues.GetValue(i).ToString()
                            If i < Me.chkDistributor.CheckedValues.Length - 1 Then
                                RowFilter &= ","
                            End If
                        Next
                        RowFilter &= "')"
                    End If
                    DirectCast(Me.grdDetail.DataSource, DataView).RowFilter = RowFilter
                    Me.grdDetail.Refetch() : Me.grdDetail.ExpandGroups()
                Case CategoryReport.TargetReaching
                    If Me.grdHeader.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                        If Not IsNothing(Me.grdDetail.DataSource) Then
                            RowFilter = " IDKios = '" & Me.grdHeader.GetValue("IDKios").ToString() _
                            & "'" & " AND BRAND_ID = '" & Me.grdHeader.GetValue("BRAND_ID").ToString() & "'"
                        End If
                    ElseIf Me.grdHeader.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Then
                        Return
                    ElseIf Not IsNothing(Me.chkKios.CheckedValues) Then
                        If Me.chkKios.CheckedValues.Length > 0 Then
                            RowFilter = "IDKios IN('"
                            For i As Integer = 0 To chkKios.CheckedValues.Length - 1
                                RowFilter &= chkKios.CheckedValues.GetValue(i).ToString()
                                If i < chkKios.CheckedValues.Length - 1 Then
                                    RowFilter &= ","
                                End If
                            Next
                            RowFilter &= "')"
                        End If
                    End If
                    DirectCast(Me.grdDetail.DataSource, DataView).RowFilter = RowFilter
                    Me.grdDetail.Refetch() : Me.grdDetail.ExpandGroups()
                Case CategoryReport.None
                    Return
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdHeader_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdHeader.Enter
        Me.pnlHeader.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDetail.BorderStyle = Windows.Forms.BorderStyle.None
        Me.AG = ActiveGrid.GrdHeader
        If Not IsNothing(Me.FE) Then
            Dim S As Boolean = Me.FE.Visible
            Me.FE.SourceControl = Me.grdHeader
            Me.FE.Visible = S
        End If
    End Sub

    Private Sub rdbAllKios_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAllKios.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.IsLoadingCombo Then : Return : End If
            'If Me.IsloadingRow Then : Return : End If
            Me.chkKios.UncheckAll()
            Me.chkKios.ReadOnly = True : Me.btnFilterKios.Enabled = False
            Me.IsloadingRow = True
            If Me.rdbReachingTarget.Checked Then
                If Not IsNothing(Me.grdHeader.DataSource) Then
                    DirectCast(Me.grdHeader.DataSource, DataView).RowFilter = ""
                    Me.grdHeader.Refetch()
                End If
                If Not IsNothing(Me.grdDetail.DataSource) Then
                    DirectCast(Me.grdDetail.DataSource, DataView).RowFilter = ""
                    Me.grdDetail.Refetch()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.IsloadingRow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbPerKios_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPerKios.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbPerKios.Checked Then
                Me.chkKios.ReadOnly = False
                Me.btnFilterKios.Enabled = True
                Me.chkKios_CheckedValuesChanged(Me.chkKios, New EventArgs())
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkKios_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkKios.CheckedValuesChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If (IsLoadingCombo) Then : Return : End If
            'If chkKios.Text = "" Then : Return : End If
            'If Me.chkKios.CheckedItems Is Nothing Then : Return : End If
            'If Me.chkKios.CheckedValues.Length <= 0 Then : Return : End If
            Dim RowFilter As String = ""
            If Not IsNothing(Me.chkKios.CheckedValues) Then
                If Me.chkKios.CheckedValues.Length > 0 Then
                    RowFilter = "IDKios IN('"
                    For i As Integer = 0 To chkKios.CheckedValues.Length - 1
                        RowFilter &= chkKios.CheckedValues.GetValue(i).ToString()
                        If i < chkKios.CheckedValues.Length - 1 Then
                            RowFilter &= "','"
                        End If

                    Next
                    RowFilter &= "')"
                End If
            End If
            If Me.rdbReachingTarget.Checked Then
                Me.IsloadingRow = True
                If Not IsNothing(Me.grdHeader.DataSource) Then
                    DirectCast(Me.grdHeader.DataSource, DataView).RowFilter = RowFilter
                    Me.grdHeader.Refetch()
                End If
                If Not IsNothing(Me.grdDetail.DataSource) Then
                    DirectCast(Me.grdDetail.DataSource, DataView).RowFilter = RowFilter
                    Me.grdDetail.Refetch()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.IsloadingRow = False : Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnFilterKios_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterKios.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rowfilter As String = ""
            If Not IsNothing(Me.chkKios.DropDownDataSource) Then
                Me.IsLoadingCombo = True
                rowfilter = "Kios_Name LIKE '%" & Me.chkKios.Text & "%'"
                DirectCast(Me.chkKios.DropDownDataSource, DataView).RowFilter = rowfilter
                Me.chkKios.DropDownList.Refetch()
                Dim ItemCount As Integer = Me.chkKios.DropDownList.RecordCount
                MessageBox.Show(ItemCount.ToString() & " item(s) found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.IsLoadingCombo = False : Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.tickCount += 1
    End Sub
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Me.CounterTimer2 += 1
    End Sub
End Class
