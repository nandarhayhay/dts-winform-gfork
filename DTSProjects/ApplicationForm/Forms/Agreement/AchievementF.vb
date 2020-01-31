Imports System.Threading
Public Class AchievementF
    Private clsDPD As NufarmBussinesRules.DistributorAgreement.DPDAchievement
    Friend CMain As Main = Nothing

    Private isLoadingCombo As Boolean = True
    Private SelectGrid As SelectedGrid = SelectedGrid.Header
    Private Grid As Janus.Windows.GridEX.GridEX = Nothing
    Private DS As DataSet = Nothing
    Private isLoadingRow As Boolean = True
    Private IsHasLoadedForm As Boolean = False
    Private HasLoadReport As Boolean = True
    'Private LRF As LoadReportFrom = LoadReportFrom.FirsLoadedForm
    Private IsGeneratingOA As Boolean = False : Private Flag As String = ""
    Private CounterTimer2 As Integer = 0
    Private LD As Loading = Nothing
    Private IsProcessingDiscount As Boolean = False
    Private SP As StatusProgress
    Private ThreadProcess As Thread
    Private AgreeAchBy As String = ""
    Dim Rg As New NufarmBussinesRules.SettingDTS.RegUser()
    Dim tblSetting As New DataTable("Settingan")
    Dim ListAgreementNo As New List(Of String)
    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            btnFlag.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Achievement
            'If Not NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Achievement Then
            '    Me.btnFlag.Enabled = False
            'End If
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Achievement Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
        End If
    End Sub
    Private Enum SelectedGrid
        Header
        Detail
    End Enum
    Private Enum StatusProgress
        ProcessingDisc
        LoadingAchiement
        None
    End Enum
    Private Enum LoadReportFrom
        FirsLoadedForm
        AfterLoadedForm
    End Enum
    Private Sub CheckedFilter()
        If Me.chkFilter.Checked Then
            Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
        Else
            If Not IsNothing(Me.GridEX2.DataSource) Then
                Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                DV.RowFilter = ""
                Me.GridEX2.SetDataBinding(DV, "")
            End If
            'If Not IsNothing(Me.GridEX3.DataSource) Then
            '    Dim DV As DataView = CType(Me.GridEX3.DataSource, DataView)
            '    DV.RowFilter = ""
            '    Me.GridEX3.SetDataBinding(DV, "")
            'End If
        End If
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged

    End Sub


    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            'check reference data
            Me.Cursor = Cursors.WaitCursor
            Dim AchHeaderID As String = Me.GridEX1.GetValue("ACH_HEADER_ID").ToString()
            'delete data beserta anaknya
            Me.clsDPD.DeleteAchievementHeader(Me.GridEX1.GetValue("ACH_HEADER_ID").ToString())
            Dim DV As DataView = Me.DS.Tables(1).DefaultView()
            DV.Sort = "ACH_HEADER_ID"
            Dim Dr() As DataRowView = DV.FindRows(AchHeaderID)
            If Dr.Length > 0 Then
                For i As Integer = 0 To DV.Count - 1
                    Dim Index As Integer = DV.Find(AchHeaderID)
                    If Index <> -1 Then
                        DV(i).Delete()
                        i -= 1
                    Else
                        Exit For
                    End If
                Next
            End If
            e.Cancel = False
            CheckedFilter()
            Me.GridEX1.UpdateData()
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles chkFilter.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.isLoadingRow = True
            CheckedFilter()
            Me.isLoadingRow = False
        Catch ex As Exception
            Me.isLoadingRow = False
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub HideColumnsPBOrCP(ByVal Grid As Janus.Windows.GridEX.GridEX, ByVal cols As List(Of String), ByVal isVisible As Boolean)
        Select Case Grid.Name
            Case "GridEX1"
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                    If col.Caption.Contains("LEFT_PERIODE_") Or col.Caption.Contains("TOTAL_LEFT_") Then
                        If cols.Contains(col.Caption) Then
                            col.Visible = isVisible
                        Else
                            col.Visible = Not isVisible
                        End If
                    End If
                Next
            Case "GridEX2"
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX2.RootTable.Columns
                    If col.Caption.Contains("LEFT_PERIODE_") Then
                        If cols.Contains(col.Caption) Then
                            col.Visible = isVisible
                        Else
                            col.Visible = Not isVisible
                        End If
                    End If
                Next
        End Select
    End Sub
    Private Sub getDS(ByVal Sprog As StatusProgress)
        Try
            Select Case Sprog
                Case StatusProgress.LoadingAchiement
                    If Me.Flag = "" Then
                        Me.SP = StatusProgress.None : Me.Cursor = Cursors.Default
                        MessageBox.Show("System can not view data when flag is not defined", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                    ''check if Disc has been Applied by (Volume/Value

                    If (Not IsNothing(Me.mcbDistributor.Value)) And (Not IsNothing(Me.chkDistributors.CheckedValues)) Then
                        If Me.chkDistributors.CheckedValues.Length > 0 Then
                            Me.DS = Me.clsDPD.getAccrued(Me.Flag, Me.mcbDistributor.Value.ToString(), ListAgreementNo)
                        Else
                            Me.DS = Me.clsDPD.getAccrued(Me.Flag, Me.mcbDistributor.Value.ToString())
                        End If
                    ElseIf Not IsNothing(Me.chkDistributors.CheckedValues) Then
                        If Me.chkDistributors.CheckedValues.Length > 0 Then
                            For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
                                If Not ListAgreementNo.Contains(Me.chkDistributors.CheckedValues.GetValue(i).ToString()) Then
                                    ListAgreementNo.Add(Me.chkDistributors.CheckedValues.GetValue(i).ToString())
                                End If
                            Next
                            Me.DS = Me.clsDPD.getAccrued(Me.Flag, , ListAgreementNo)
                        End If
                    ElseIf (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                        Me.DS = Me.clsDPD.getAccrued(Me.Flag, Me.mcbDistributor.Value.ToString())
                    Else
                        Me.DS = Me.clsDPD.getAccrued(Me.Flag, True)
                    End If
                Case StatusProgress.ProcessingDisc
                    Dim DVAgreement As DataView = Nothing 'DirectCast(Me.chkDistributors.DropDownDataSource, DataView).ToTable()
                    Dim DVAgreeOri As DataView = Nothing
                    If (Not IsNothing(Me.mcbDistributor.Value) And Me.mcbDistributor.SelectedIndex <> -1) And _
                        (Not IsNothing(Me.chkDistributors.CheckedValues)) Then
                        If Me.chkDistributors.CheckedValues.Length > 0 Then
                            DVAgreement = DirectCast(Me.chkDistributors.DropDownDataSource, DataView).ToTable.Copy().DefaultView
                            DVAgreeOri = DirectCast(Me.chkDistributors.DropDownDataSource, DataView).ToTable.Copy().DefaultView
                            DVAgreement.Table.Clear() : DVAgreement.Sort = "AGREEMENT_NO"
                            DVAgreeOri.Sort = "AGREEMENT_NO"
                            Dim DrV As DataRowView = Nothing
                            For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
                                Dim AgreementNo = Me.chkDistributors.CheckedValues.GetValue(i)
                                Dim Index As Integer = DVAgreement.Find(AgreementNo)
                                If Index = -1 Then
                                    DrV = DVAgreement.AddNew()
                                    Dim IndexOri As Integer = DVAgreeOri.Find(AgreementNo)
                                    DrV("AGREEMENT_NO") = AgreementNo
                                    DrV("START_DATE") = DVAgreeOri(IndexOri)("START_DATE")
                                    DrV("END_DATE") = DVAgreeOri(IndexOri)("END_DATE")
                                    DrV.EndEdit()
                                End If
                            Next
                            'Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, Me.mcbDistributor.Value.ToString(), DVAgreement.ToTable())
                        Else
                            'Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, Me.mcbDistributor.Value.ToString())
                        End If
                    ElseIf (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                        'Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, Me.mcbDistributor.Value.ToString())
                    ElseIf Not IsNothing(Me.chkDistributors.CheckedValues) Then
                        If Me.chkDistributors.CheckedValues.Length > 0 Then
                            DVAgreement = DirectCast(Me.chkDistributors.DropDownDataSource, DataView).ToTable.Copy().DefaultView
                            DVAgreeOri = DirectCast(Me.chkDistributors.DropDownDataSource, DataView).ToTable.Copy().DefaultView
                            DVAgreement.Table.Clear() : DVAgreement.Sort = "AGREEMENT_NO"
                            DVAgreeOri.Sort = "AGREEMENT_NO"
                            Dim DrV As DataRowView = Nothing
                            For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
                                Dim AgreementNo = Me.chkDistributors.CheckedValues.GetValue(i)
                                Dim Index As Integer = DVAgreement.Find(AgreementNo)
                                If Index = -1 Then
                                    DrV = DVAgreement.AddNew()
                                    Dim IndexOri As Integer = DVAgreeOri.Find(AgreementNo)
                                    DrV("AGREEMENT_NO") = AgreementNo
                                    DrV("START_DATE") = DVAgreeOri(IndexOri)("START_DATE")
                                    DrV("END_DATE") = DVAgreeOri(IndexOri)("END_DATE")
                                    DrV.EndEdit()
                                End If
                            Next
                            'Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, , DVAgreement.ToTable())
                        Else
                            'Me.DS = Me.clsTA.CalculateAccrue(Me.Flag)
                        End If
                    ElseIf Me.Flag <> "" Then
                        'Me.DS = Me.clsTA.CalculateAccrue(Me.Flag)
                    End If
            End Select
        Catch ex As Exception
            'If Me.SP = StatusProgress.ProcessingAcrrue Then : Me.EnabledFlag("") : End If
            Me.HasLoadReport = True : Me.SP = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False : Me.TickCount = 0
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_getDS") : Me.Cursor = Cursors.Default
            'Me.LAF = LoadAchievementFrom.None
            'Finally
            '    Me.SP = StatusProgress.None
        End Try
    End Sub
    Private Sub BindGrid()
        If Not Me.isLoadingRow Then : Me.isLoadingRow = True : End If
        If Not IsNothing(Me.DS) Then
            Me.GridEX1.SetDataBinding(Me.DS.Tables("ACHIEVEMENT_HEADER").DefaultView, "")
            Me.GridEX1.DropDowns("BRAND").SetDataBinding(Me.DS.Tables("T_BRAND").DefaultView(), "")
            Me.GridEX2.SetDataBinding(Me.DS.Tables("ACHIEVEMENT_DETAIL").DefaultView(), "")
            If Me.chkFilter.Checked Then
                Me.isLoadingRow = False
                Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
            End If
        Else
            Me.GridEX1.SetDataBinding(Nothing, "") : Me.GridEX2.SetDataBinding(Nothing, "")
        End If
        'Dim listCols As New List(Of String)
        'Dim ListColsCopy As New List(Of String)
        ''LEFT_PREVIOUS_Q1,LEFT_CURRENT_Q2,LEFT_PREVIOUS_Q3,LEFT_CURRENT_Q3,LEFT_CURRENT_F1,LEFT_CURRENT_F2,LEFT_PREVIOUS_F3
        'Select Case Me.Flag
        '    Case "F1" : listCols.AddRange(New String() {"LEFT_PREVIOUS_Q3", "LEFT_CURRENT_Q2", "LEFT_CURRENT_Q3", "LEFT_PREVIOUS_F3"})
        '        ListColsCopy.AddRange(New String() {"LEFT_PREVIOUS_Q3", "LEFT_CURRENT_Q2", "LEFT_CURRENT_Q3", "LEFT_PREVIOUS_F3"})
        '    Case "F2" : listCols.AddRange(New String() {"LEFT_CURRENT_Q2", "LEFT_CURRENT_Q3", "LEFT_CURRENT_F1"})
        '        ListColsCopy.AddRange(New String() {"LEFT_CURRENT_Q2", "LEFT_CURRENT_Q3", "LEFT_CURRENT_F1"})
        '    Case "F3" : listCols.AddRange(New String() {"LEFT_CURRENT_Q3", "LEFT_CURRENT_F2"})
        '        ListColsCopy.AddRange(New String() {"LEFT_PERIODE_Q1", "TOTAL_LEFT_Q1", "LEFT_PERIODE_Q2", "TOTAL_LEFT_Q2"})
        '    Case "Q4" : listCols.AddRange(New String() {"LEFT_PERIODE_Q2", "TOTAL_LEFT_Q2", "LEFT_PERIODE_CUR_Q3", "TOTAL_LEFT_CUR_Q3"})
        '        ListColsCopy.AddRange(New String() {"LEFT_PERIODE_Q2", "TOTAL_LEFT_Q2", "LEFT_PERIODE_CUR_Q3", "TOTAL_LEFT_CUR_Q3"})
        '    Case "S1" : listCols.AddRange(New String() {"LEFT_PERIODE_S2", "TOTAL_LEFT_S2", "LEFT_PERIODE_Q3_BEFORE", "TOTAL_LEFT_Q3_BEFORE", "LEFT_PERIODE_Q4", "TOTAL_LEFT_Q4"})
        '        ListColsCopy.AddRange(New String() {"LEFT_PERIODE_S2", "TOTAL_LEFT_S2", "LEFT_PERIODE_Q3_BEFORE", "TOTAL_LEFT_Q3_BEFORE", "LEFT_PERIODE_Q4", "TOTAL_LEFT_Q4"})
        '    Case "S2" : listCols.AddRange(New String() {"LEFT_PERIODE_S1", "TOTAL_LEFT_S1"})
        '        ListColsCopy.AddRange(New String() {"LEFT_PERIODE_S1", "TOTAL_LEFT_S1"})
        'End Select
        'TOTAL_PBQ1, TOTAL_PBQ2, TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3
        'Me.HideColumnsPBOrCP(Me.GridEX1, listCols, True)
        'For Each colTotal As String In ListColsCopy
        '    If colTotal.Contains("TOTAL_LEFT") Then
        '        listCols.Remove(colTotal)
        '    End If
        'Next
        'Me.HideColumnsPBOrCP(Me.GridEX2, listCols, True)
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.Timer1.Stop()
            Me.Timer1.Enabled = False

            If Me.SP = StatusProgress.LoadingAchiement Then ''DS di peroleh di procedure ApplyDiscByVolume
            Else
                Me.getDS(Me.SP)
            End If
            Me.HasLoadReport = True : Me.BindGrid()
            Me.SP = StatusProgress.None
            If Me.clsDPD.MessageError <> "" Then
                'Me.ShowMessageInfo(Me.clsTA.MessageError)
            End If
            Me.SP = StatusProgress.None
            If Me.isLoadingRow Then : isLoadingRow = False : End If
        Catch ex As Exception
            Me.SP = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False
            'Me.OtherUserProcessing = ""
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AchievementF_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not IsNothing(Me.DS) Then
            Me.isLoadingRow = True
            Me.DS.Dispose()
            'Me.clsTA.Dispose()
            Me.clsDPD.Dispose()
        End If
    End Sub
    Private Function GetFlag(ByVal Flag As String) As String
        Dim strFlag As String = ""
        Select Case Me.cmbFlag.Text
            Case "4 MONTHS PERIODE 1" : strFlag = "F1"
            Case "4 MONTHS PERIODE 2" : strFlag = "F2"
            Case "4 MONTHS PERIODE 3" : strFlag = "F3"
        End Select
        Return strFlag
    End Function
    Private Sub checkEnabledFlagValue()
        'reset enabled button
        Me.btnRecomputeF1.Enabled = False
        Me.btnRecomputeF2.Enabled = False
        Me.btnRecomputeF3.Enabled = False
        'Me.btnQuarter4V.Enabled = False
        'Me.btnSemester1V.Enabled = False
        'Me.btnSemester2V.Enabled = False
        'Me.btnYearlyV.Enabled = False

        Dim Flag As String = ""
        Dim BFind As Boolean = False
        If Me.cmbFlag.Text <> "<< Choose Flag >>" Then
            'check MCB distributor
            Flag = Me.GetFlag(Me.cmbFlag.Text)
            If Not IsNothing(Me.chkDistributors.CheckedValues()) Then
                If Me.chkDistributors.CheckedValues.Length > 0 Then
                    BFind = True
                End If
            End If
        End If
        If BFind Then
            Select Case Flag
                Case "F1" : Me.btnRecomputeF1.Enabled = True
                Case "F2" : Me.btnRecomputeF2.Enabled = True
                Case "Q3" : Me.btnRecomputeF3.Enabled = True
            End Select
        End If
        If Not Me.btnRecomputeF1.Enabled Then : Me.btnRecomputeF1.Checked = False : End If
        If Not Me.btnRecomputeF2.Enabled Then : Me.btnRecomputeF2.Checked = False : End If
        If Not Me.btnRecomputeF3.Enabled Then : Me.btnRecomputeF3.Checked = False : End If
        'If Not Me.btnQuarter4V.Enabled Then : Me.btnQuarter4V.Checked = False : End If
        'If Not Me.btnSemester1V.Enabled Then : Me.btnSemester1V.Checked = False : End If
        'If Not Me.btnSemester2V.Enabled Then : Me.btnSemester2V.Checked = False : End If
        'If Not Me.btnYearlyV.Enabled Then : Me.btnYearlyV.Checked = False : End If
    End Sub
    Private Sub EnabledFlag(ByVal flag As String)
        Me.btnRecomputeF1.Enabled = (flag = "F1")
        If Not btnRecomputeF1.Enabled Then : Me.btnRecomputeF1.Checked = False : End If
        Me.btnRecomputeF2.Enabled = (flag = "F2")
        If Not btnRecomputeF1.Enabled Then : Me.btnRecomputeF2.Checked = False : End If
        Me.btnRecomputeF2.Enabled = (flag = "F3")
        If Not btnRecomputeF3.Enabled Then : Me.btnRecomputeF3.Checked = False : End If
    End Sub
    Private Sub AchievementF_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AddHandler Timer1.Tick, AddressOf ChekTimer
        Me.GridEX1_Enter(Me.GridEX1, New EventArgs())
        Me.IsHasLoadedForm = True
        Me.isLoadingCombo = False
        Me.EnabledFlag("")
        checkEnabledFlagValue()
        Me.ReadAcces()
        CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
        Me.ExpandableSplitter2.Expanded = False
    End Sub

    Private Sub GridEX2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX2.Enter
        Me.Grid = Me.GridEX2
        Me.Panel1.BorderStyle = BorderStyle.Fixed3D
        Me.Panel2.BorderStyle = BorderStyle.None
        Me.SelectGrid = SelectedGrid.Detail
        Dim B As Boolean = Me.FilterEditor1.Visible
        Me.FilterEditor1.Visible = B
        Me.FilterEditor1.SourceControl = Me.Grid
        Me.FilterEditor1.SortFieldList = False
    End Sub

    Private Sub GridEX1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.Enter
        Me.Grid = Me.GridEX1
        Me.Panel1.BorderStyle = BorderStyle.Fixed3D
        Me.Panel2.BorderStyle = BorderStyle.None
        Me.SelectGrid = SelectedGrid.Header
        Dim B As Boolean = Me.FilterEditor1.Visible
        Me.FilterEditor1.Visible = B
        Me.FilterEditor1.SourceControl = Me.Grid
        Me.FilterEditor1.SortFieldList = False
    End Sub
End Class
