Imports System.Threading
Public Class AchievementF

    Friend CMain As Main = Nothing

    Private isLoadingCombo As Boolean = True
    Private SelectGrid As SelectedGrid = SelectedGrid.Header
    Private Grid As Janus.Windows.GridEX.GridEX = Nothing
    Private DS As DataSet = Nothing
    Private isLoadingRow As Boolean = True
    Private IsHasLoadedForm As Boolean = False
    Private IsGeneratingOA As Boolean = False : Private Flag As String = ""
    Private LD As Loading = Nothing
    Private SP As StatusProgress
    Private ThreadProcess As Thread
    Dim ListAgreementNo As New List(Of String)
    Private m_clsDPD As NufarmBussinesRules.DistributorAgreement.DPDAchievementR
    Private m_clsDPDN As NufarmBussinesRules.DistributorAgreement.DPDAchievementN
    Private isNufarmDPD As Boolean = True

    Private ReadOnly Property clsDPD() As NufarmBussinesRules.DistributorAgreement.DPDAchievementR
        Get
            If IsNothing(Me.m_clsDPD) Then
                Me.m_clsDPD = New NufarmBussinesRules.DistributorAgreement.DPDAchievementR()
            End If
            Return Me.m_clsDPD
        End Get
    End Property
    Private ReadOnly Property clsDPDN() As NufarmBussinesRules.DistributorAgreement.DPDAchievementN
        Get
            If IsNothing(Me.m_clsDPDN) Then
                Me.m_clsDPDN = New NufarmBussinesRules.DistributorAgreement.DPDAchievementN()
            End If
            Return Me.m_clsDPDN
        End Get
    End Property

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
    Private Sub CheckedFilter()
        If Me.ChkFilterDetail.Checked Then
            Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
        Else
            If Not IsNothing(Me.GridEX2.DataSource) Then
                Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                DV.RowFilter = ""
                Me.GridEX2.SetDataBinding(DV, "")
                For Each item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX2.RootTable.Columns
                    If item.Type Is Type.GetType("System.Decimal") Then
                        If item.Key.Contains("QTY") Or item.Key.Contains("TOTAL") Then
                            item.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                            item.TotalFormatString = "#,##.000"
                        End If
                    End If
                Next
            End If
            Me.GridEX2.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
        End If

    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            If Me.isLoadingRow Then : Return : End If
            If IsNothing(Me.GridEX1.DataSource) Then : Return : End If
            If Me.GridEX1.RecordCount <= 0 Then : Return : End If
            If Me.ChkFilterDetail.Checked Then
                If Me.GridEX1.SelectedItems.Count > 0 Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.isLoadingRow = True
                    If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                        If Not IsNothing(Me.GridEX2.DataSource) Then
                            Dim AchHeaderID As String = Me.GridEX1.GetValue("ACH_HEADER_ID").ToString()
                            Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                            DV.RowFilter = "ACH_HEADER_ID = '" & AchHeaderID & "'"
                            Me.GridEX2.SetDataBinding(DV, "")
                        End If
                    ElseIf Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.FilterRow _
                    Or GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter _
                    Or GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader _
                    Or GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord _
                    Or GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.TotalRow Then
                        If Not IsNothing(Me.GridEX2.DataSource) Then
                            Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                            DV.RowFilter = ""
                            Me.GridEX2.SetDataBinding(DV, "")
                        End If
                    End If
                    For Each item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX2.RootTable.Columns
                        If item.Type Is Type.GetType("System.Decimal") Then
                            If item.Key.Contains("QTY") Or item.Key.Contains("TOTAL") Or item.Key.Contains("QTY") Then
                                item.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                                item.TotalFormatString = "#,##.000"
                            End If
                        End If
                    Next
                    Me.GridEX2.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Else
                    If Not IsNothing(Me.GridEX2.DataSource) Then
                        Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                        DV.RowFilter = ""
                        Me.GridEX2.SetDataBinding(DV, "")
                    End If
                End If
            Else
                If Not IsNothing(Me.GridEX2.DataSource) Then
                    Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                    DV.RowFilter = ""
                    Me.GridEX2.SetDataBinding(DV, "")
                    For Each item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX2.RootTable.Columns
                        If item.Type Is Type.GetType("System.Decimal") Then
                            If item.Key.Contains("QTY") Or item.Key.Contains("TOTAL") Or item.Key.Contains("QTY") Then
                                item.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                                item.TotalFormatString = "#,##.000"
                            End If
                        End If
                    Next
                End If
                Me.GridEX2.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            Me.isLoadingRow = False
        Catch ex As Exception
            Me.isLoadingRow = False
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            'check reference data
            Me.Cursor = Cursors.WaitCursor
            Dim AchHeaderID As String = Me.GridEX1.GetValue("ACH_HEADER_ID").ToString()
            'delete data beserta anaknya
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes Then
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
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs)
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
    Private Sub ProceedRoundupDPD(ByVal Sprog As StatusProgress)
        Select Case Sprog
            Case StatusProgress.LoadingAchiement
                If (Not IsNothing(Me.mcbDistributor.Value)) And (Not IsNothing(Me.chkDistributors.CheckedValues)) Then
                    If Me.chkDistributors.CheckedValues.Length > 0 Then
                        Me.DS = Me.clsDPD.getAchievement(Me.Flag, Me.mcbDistributor.Value.ToString(), ListAgreementNo)
                    Else
                        Me.DS = Me.clsDPD.getAchievement(Me.Flag, Me.mcbDistributor.Value.ToString())
                    End If
                ElseIf Not IsNothing(Me.chkDistributors.CheckedValues) Then
                    If Me.chkDistributors.CheckedValues.Length > 0 Then
                        For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
                            If Not ListAgreementNo.Contains(Me.chkDistributors.CheckedValues.GetValue(i).ToString()) Then
                                ListAgreementNo.Add(Me.chkDistributors.CheckedValues.GetValue(i).ToString())
                            End If
                        Next
                        Me.DS = Me.clsDPD.getAchievement(Me.Flag, , ListAgreementNo)
                    End If
                ElseIf (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                    Me.DS = Me.clsDPD.getAchievement(Me.Flag, Me.mcbDistributor.Value.ToString())
                Else
                    Me.DS = Me.clsDPD.getAchievement(Me.Flag, True)
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
                        Me.DS = Me.clsDPD.CalculateAchievement(Me.Flag, DVAgreement.ToTable(), Me.mcbDistributor.Value.ToString())
                    Else
                        Me.DS = Me.clsDPD.CalculateAchievement(Me.Flag, , Me.mcbDistributor.Value.ToString())
                    End If
                ElseIf (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                    Me.DS = Me.clsDPD.CalculateAchievement(Me.Flag, , Me.mcbDistributor.Value.ToString())
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
                        Me.DS = Me.clsDPD.CalculateAchievement(Me.Flag, DVAgreement.ToTable())
                    Else
                        Me.DS = Me.clsDPD.CalculateAchievement(Me.Flag)
                    End If
                ElseIf Me.Flag <> "" Then
                    Me.DS = Me.clsDPD.CalculateAchievement(Me.Flag)
                End If
        End Select
    End Sub
    Private Sub ProceedNufarmDPD(ByVal Sprog As StatusProgress)
        Select Case Sprog
            Case StatusProgress.LoadingAchiement
                If (Not IsNothing(Me.mcbDistributor.Value)) And (Not IsNothing(Me.chkDistributors.CheckedValues)) Then
                    If Me.chkDistributors.CheckedValues.Length > 0 Then
                        Me.DS = Me.clsDPDN.getAchievement(Me.Flag, Me.mcbDistributor.Value.ToString(), ListAgreementNo)
                    Else
                        Me.DS = Me.clsDPDN.getAchievement(Me.Flag, Me.mcbDistributor.Value.ToString())
                    End If
                ElseIf Not IsNothing(Me.chkDistributors.CheckedValues) Then
                    If Me.chkDistributors.CheckedValues.Length > 0 Then
                        For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
                            If Not ListAgreementNo.Contains(Me.chkDistributors.CheckedValues.GetValue(i).ToString()) Then
                                ListAgreementNo.Add(Me.chkDistributors.CheckedValues.GetValue(i).ToString())
                            End If
                        Next
                        Me.DS = Me.clsDPDN.getAchievement(Me.Flag, , ListAgreementNo)
                    End If
                ElseIf (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                    Me.DS = Me.clsDPDN.getAchievement(Me.Flag, Me.mcbDistributor.Value.ToString())
                Else
                    Me.DS = Me.clsDPDN.getAchievement(Me.Flag, True)
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
                        Me.DS = Me.clsDPDN.CalculateAchievement(Me.Flag, DVAgreement.ToTable(), Me.mcbDistributor.Value.ToString())
                    Else
                        Me.DS = Me.clsDPDN.CalculateAchievement(Me.Flag, , Me.mcbDistributor.Value.ToString())
                    End If
                ElseIf (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                    Me.DS = Me.clsDPDN.CalculateAchievement(Me.Flag, , Me.mcbDistributor.Value.ToString())
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
                        Me.DS = Me.clsDPDN.CalculateAchievement(Me.Flag, DVAgreement.ToTable())
                    Else
                        Me.DS = Me.clsDPDN.CalculateAchievement(Me.Flag)
                    End If
                ElseIf Me.Flag <> "" Then
                    Me.DS = Me.clsDPDN.CalculateAchievement(Me.Flag)
                End If
        End Select
    End Sub
    Private Sub getDS(ByVal Sprog As StatusProgress)
        Try
            'check apakah pkd roundup atau nufarm
            If Me.cmbDPDType.SelectedIndex = 2 Then : Me.isNufarmDPD = False : End If

            If isNufarmDPD Then
                Me.ProceedNufarmDPD(Sprog)
            Else
                Me.ProceedRoundupDPD(Sprog)
            End If

            If Not IsNothing(Me.DS) Then
                Me.BindGrid()
            End If
        Catch ex As Exception
            Me.SP = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False : Me.TickCount = 0
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_getDS") : Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub BindGrid()
        If Not Me.isLoadingRow Then : Me.isLoadingRow = True : End If
        If Not IsNothing(Me.DS) Then
            Me.GridEX1.SetDataBinding(Me.DS.Tables("ACHIEVEMENT_HEADER").DefaultView, "")
            Me.GridEX1.DropDowns("BRAND").SetDataBinding(Me.DS.Tables("T_BRAND").DefaultView(), "")
            Me.GridEX2.SetDataBinding(Me.DS.Tables("ACHIEVEMENT_DETAIL").DefaultView(), "")
            If Me.ChkFilterDetail.Checked Then
                Me.isLoadingRow = False
                Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
            End If
            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If col.DataTypeCode = TypeCode.Boolean Then
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
                ElseIf col.DataTypeCode = TypeCode.DateTime Then
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarDropDown
                Else
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                End If
            Next
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX2.RootTable.Columns
                If col.DataTypeCode = TypeCode.Boolean Then
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
                ElseIf col.DataTypeCode = TypeCode.DateTime Then
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarDropDown
                Else
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                End If
            Next

        Else
            Me.GridEX1.SetDataBinding(Nothing, "") : Me.GridEX2.SetDataBinding(Nothing, "")
        End If
        ''hide column yang tidak perlu sesuai type achievement

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
                Case "F3" : Me.btnRecomputeF3.Enabled = True
            End Select
        End If
        If Not Me.btnRecomputeF1.Enabled Then : Me.btnRecomputeF1.Checked = False : End If
        If Not Me.btnRecomputeF2.Enabled Then : Me.btnRecomputeF2.Checked = False : End If
        If Not Me.btnRecomputeF3.Enabled Then : Me.btnRecomputeF3.Checked = False : End If
    End Sub
    Private Sub EnabledFlag(ByVal flag As String)
        Me.btnRecomputeF1.Enabled = (flag = "F1")
        If Not btnRecomputeF1.Enabled Then : Me.btnRecomputeF1.Checked = False : End If
        Me.btnRecomputeF2.Enabled = (flag = "F2")
        If Not btnRecomputeF1.Enabled Then : Me.btnRecomputeF2.Checked = False : End If
        Me.btnRecomputeF2.Enabled = (flag = "F3")
        If Not btnRecomputeF3.Enabled Then : Me.btnRecomputeF3.Checked = False : End If
    End Sub
    Private Sub ShowLoading()
        LD = New Loading
        LD.Show() : LD.TopMost = True
        Application.DoEvents()
        While Not Me.SP = StatusProgress.None
            Me.LD.Label1.Text = "Processing procedure...."
            Thread.Sleep(50)
            LD.Refresh()
            Application.DoEvents()
        End While
        Thread.Sleep(50)
        LD.Close() : LD = Nothing
    End Sub
    Private Sub AchievementF_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.GridEX1_Enter(Me.GridEX1, New EventArgs())
        Me.IsHasLoadedForm = True
        Me.isLoadingCombo = False
        Me.EnabledFlag("")
        checkEnabledFlagValue()
        Me.ReadAcces()
        CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
        Me.ExpandableSplitter2.Expanded = False
        Me.cmbDPDType.SelectedIndex = 0
    End Sub

    Private Sub GridEX2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX2.Enter
        Me.Grid = Me.GridEX2
        Me.Panel1.BorderStyle = BorderStyle.Fixed3D
        Me.Panel2.BorderStyle = BorderStyle.None
        Me.SelectGrid = SelectedGrid.Detail
        'Dim B As Boolean = Me.FilterEditor1.Visible
        'Me.FilterEditor1.Visible = B
        Me.FilterEditor1.SourceControl = Me.Grid
        Me.FilterEditor1.SortFieldList = False
    End Sub

    Private Sub GridEX1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.Enter
        Me.Grid = Me.GridEX1
        Me.Panel1.BorderStyle = BorderStyle.Fixed3D
        Me.Panel2.BorderStyle = BorderStyle.None
        Me.SelectGrid = SelectedGrid.Header
        'Dim B As Boolean = Me.FilterEditor1.Visible
        'Me.FilterEditor1.Visible = B
        Me.FilterEditor1.SourceControl = Me.Grid
        Me.FilterEditor1.SortFieldList = False
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Dim Btn As Object = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnShowFieldChooser"
                    Me.Grid.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.Grid
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.Grid
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
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
                    Me.FilterEditor1.SourceControl = Me.Grid
                    Me.Grid.RemoveFilters()
                    Me.FilterEditor1.Visible = True
                    GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Grid.RemoveFilters()
                    Grid.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Case "btnExport"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Grid
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnRecomputeF1"
                    If Me.cmbDPDType.SelectedIndex <= 0 Then
                        Me.ShowMessageInfo("Please enter DPD type")
                        Me.cmbDPDType.Focus()
                        Return
                    End If
                    Me.Flag = "F1"
                    Btn = btnRecomputeF1
                    Me.btnRecomputeF1.Checked = True
                    '================UNCOMMENT THIS AFTER DEBUGGING===================

                    Me.SP = StatusProgress.ProcessingDisc
                    'ThreadProcess = New Thread(AddressOf ShowLoading)
                    'ThreadProcess.Start()
                    '=================================================================
                    getDS(Me.SP)
                Case "btnRecomputeF2"
                    If Me.cmbDPDType.SelectedIndex <= 0 Then
                        Me.ShowMessageInfo("Please enter DPD type")
                        Me.cmbDPDType.Focus()
                        Return
                    End If
                    Me.Flag = "F2"
                    Btn = btnRecomputeF2 : Me.btnRecomputeF2.Checked = True
                    '================UNCOMMENT THIS AFTER DEBUGGING===================
                    Me.SP = StatusProgress.ProcessingDisc
                    ThreadProcess = New Thread(AddressOf ShowLoading)
                    ThreadProcess.Start()
                    '=================================================================
                    getDS(Me.SP)
                Case "btnRecomputeF3"
                    If Me.cmbDPDType.SelectedIndex <= 0 Then
                        Me.ShowMessageInfo("Please enter DPD type")
                        Me.cmbDPDType.Focus()
                        Return
                    End If
                    Me.Flag = "F3" : Me.btnRecomputeF3.Checked = True
                    '================UNCOMMENT THIS AFTER DEBUGGING===================
                    Me.SP = StatusProgress.ProcessingDisc
                    ThreadProcess = New Thread(AddressOf ShowLoading)
                    ThreadProcess.Start()
                    '================================================================
                    getDS(Me.SP)
                Case "btnRefresh"
                    Me.btnAplyRange_Click(Me.btnAplyRange, New EventArgs())
            End Select
            Me.SP = StatusProgress.None
        Catch ex As Exception
            Me.SP = StatusProgress.None : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
            If Not IsNothing(Btn) Then
                CType(sender, DevComponents.DotNetBar.ButtonItem).Checked = False
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub BindMultiColumnCombo(ByVal DV As DataView, ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, _
    ByVal ClearCombo As Boolean, ByVal DMember As String, ByVal VMember As String)
        isLoadingCombo = True
        If ClearCombo Then
            mcb.Value = Nothing : mcb.Text = ""
        End If
        With mcb
            .DataSource = DV
            .DropDownList.RetrieveStructure()
            .DroppedDown = True
            .DropDownList.VisibleRows = 20
            .DropDownList.AutoSizeColumns()
            .DisplayMember = DMember
            .ValueMember = VMember
            .DroppedDown = False
        End With
        isLoadingCombo = False
    End Sub
    Private Sub BindCheckedCombo(ByVal DV As DataView, ByVal DMember As String, ByVal VMember As String, ByVal IsCheckedAll As Boolean)
        Me.isLoadingCombo = True
        Me.chkDistributors.Text = ""
        If DV Is Nothing Then
            Me.chkDistributors.SetDataBinding(Nothing, "") : Me.isLoadingCombo = False : Return
        End If
        With Me.chkDistributors
            .SetDataBinding(DV, "")
            .DropDownList.AutoSizeColumns()
        End With
        If IsCheckedAll Then : Me.chkDistributors.CheckAll() : Else : Me.chkDistributors.UncheckAll() : End If
        Me.isLoadingCombo = False
    End Sub
    Private Sub cmbFlag_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFlag.SelectedIndexChanged
        Try
            'bind multicolumn dimana agreement nya masih valid
            Me.Cursor = Cursors.WaitCursor
            If Me.isLoadingCombo Then : Return : End If
            Dim strFlag As String = ""
            If Me.cmbFlag.SelectedIndex <> -1 And Me.cmbFlag.Text <> "<< Choose flag >>" Then
                strFlag = Me.GetFlag(Me.cmbFlag.Text)
                Dim DV As DataView = Me.clsDPD.GetDistributorAgrement(strFlag)
                Me.BindMultiColumnCombo(DV, Me.mcbDistributor, True, "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
                Me.BindCheckedCombo(Nothing, "", "", True)
                Me.Flag = Me.GetFlag(Me.cmbFlag.Text) : Me.EnabledFlag(Me.Flag)
            Else
                Me.EnabledFlag("")
            End If
            Me.checkEnabledFlagValue()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, "_cmbFlag_SelectedIndexChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSearchDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.cmbFlag.Text = "" Then
                Me.baseTooltip.Show("Please define Flag before", Me.cmbFlag, 2500) : Me.cmbFlag.Focus() : Return
            End If
            Dim RefFlag As String = Me.Flag
            If Me.Flag = "" Then : Me.ShowMessageInfo("Please define flag !!") : Return : End If
            Dim DV As DataView = Me.clsDPD.GetDistributorAgrement(RefFlag, Me.mcbDistributor.Text)
            Me.BindMultiColumnCombo(DV, Me.mcbDistributor, True, "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
            Me.BindCheckedCombo(Nothing, "", "", True)
            Dim itemCount As Integer = Me.mcbDistributor.DropDownList.RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnSearchDistributor_btnClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSearchAgreement_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchAgreement.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.cmbFlag.Text = "" Then
                Me.baseTooltip.Show("Please define Flag", Me.cmbFlag, 2500) : Me.cmbFlag.Focus() : Return
            End If
            Dim Dv As DataView = Nothing
            If (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                Dv = Me.clsDPD.GetAgreementNo(Me.Flag, Me.mcbDistributor.Value.ToString(), Me.chkDistributors.Text, 5)
            Else
                Dv = Me.clsDPD.GetAgreementNo(Me.Flag, , Me.chkDistributors.Text, 5)
            End If
            Me.BindCheckedCombo(Dv, "AGREEMENT_NO", "AGREEMENT_NO", False)
            Dim ItemCount As Integer = Me.chkDistributors.DropDownList.RecordCount
            Me.ShowMessageInfo(ItemCount.ToString() & " item(s) found")
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnSearchAgreement_btnClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnAplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyRange.Click
        Try
            If Me.Flag = "" Then
                Me.ShowMessageInfo("Can not view Data " & vbCrLf & "Because flag is not defined.") : Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.SP = StatusProgress.LoadingAchiement
            ThreadProcess = New Thread(AddressOf ShowLoading)
            ThreadProcess.Start()
            Me.getDS(Me.SP)
            Me.BindGrid()
            Me.SP = StatusProgress.None
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.SP = StatusProgress.None : Me.ShowMessageInfo(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mcbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.isLoadingCombo Then : Return : End If
            If Me.cmbFlag.Text = "" Then
                Me.baseTooltip.Show("Please define Flag", Me.cmbFlag, 2500) : Me.cmbFlag.Focus() : Return
            End If
            If Me.mcbDistributor.Value Is Nothing Or Me.mcbDistributor.SelectedIndex <= -1 Then
                Return
            End If
            Me.isLoadingCombo = True
            Dim DV As DataView = Me.clsDPD.GetAgreementNo(Me.Flag, Me.mcbDistributor.Value.ToString())
            Me.BindCheckedCombo(DV, "AGREEMENT_NO", "AGREEMENT_NO", False)
            Me.baseTooltip.Show("Please checklist Agreement no", Me.chkDistributors, 2500)
            Me.baseTooltip.UseAnimation = True
            Me.baseTooltip.ToolTipTitle = "Attention"
            Me.baseTooltip.ToolTipIcon = ToolTipIcon.Info
            Me.checkEnabledFlagValue()
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbDistributor_ValueChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.isLoadingCombo = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkDistributors_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDistributors.CheckedValuesChanged
        If Me.isLoadingCombo Then : Return : End If
        If IsNothing(Me.chkDistributors.CheckedValues) Then : Return : End If
        If Me.chkDistributors.CheckedValues.Length <= 0 Then : Return : End If
        Me.checkEnabledFlagValue()

    End Sub

    Private Sub ChkFilterDetail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkFilterDetail.CheckedChanged
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
End Class
