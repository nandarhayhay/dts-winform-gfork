Imports System.Threading
Imports NufarmBussinesRules.SettingDTS.RegUser
Public Class AchievementDPD
    Private clsTA As New NufarmBussinesRules.DistributorAgreement.Target_Agreement()
    Private isLoadingCombo As Boolean = True
    Private SelectGrid As SelectedGrid = SelectedGrid.Header
    Private Grid As Janus.Windows.GridEX.GridEX = Nothing
    Private DS As DataSet = Nothing
    Private isLoadingRow As Boolean = True
    Private IsHasLoadedForm As Boolean = False
    Dim rnd As Random : Private HasLoadReport As Boolean = True
    Private LRF As LoadReportFrom = LoadReportFrom.FirsLoadedForm
    Private IsGeneratingOA As Boolean = False : Private Flag As String = ""
    Private CounterTimer2 As Integer = 0
    Private LD As Loading = Nothing
    Private IsProcessingDiscount As Boolean = False
    Private SP As StatusProgress
    Private ThreadProcess As Thread
    Friend CMain As Main = Nothing
    Private OtherUserProcessing As String = ""
    Private isAwaiting As Boolean = False
    Private LAF As LoadAchievementFrom = LoadAchievementFrom.None
    Private AgreeAchBy As String = ""
    Dim Rg As New NufarmBussinesRules.SettingDTS.RegUser()
    Dim tblSetting As New DataTable("Settingan")
    Dim ListAgreementNo As New List(Of String)
    Dim isTransitionTime = NufarmBussinesRules.SharedClass.ServerDate <= New DateTime(2020, 7, 31) And NufarmBussinesRules.SharedClass.ServerDate >= New DateTime(2019, 8, 1)
    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            Me.btnGenerateOA.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Agreement
        End If
    End Sub
    Public ListDefaultVisibleColumns As New List(Of String)
    Private Sub checkEnabledFlagValue(ByVal mustCloseConnection As Boolean)
        'reset enabled button
        Me.btnQuarter1V.Enabled = False
        Me.btnQuarter2V.Enabled = False
        Me.btnQuarter3V.Enabled = False
        Me.btnQuarter4V.Enabled = False
        Me.btnSemester1V.Enabled = False
        Me.btnSemester2V.Enabled = False
        Me.btnYearlyV.Enabled = False

        Dim Flag As String = ""
        Dim BFind As Boolean = False
        If Me.cmbFlag.Text <> "<< Choose Flag>> " Then
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
                Case "Q1" : Me.btnQuarter1V.Enabled = True
                Case "Q2" : Me.btnQuarter2V.Enabled = True
                Case "Q3" : Me.btnQuarter3V.Enabled = True
                Case "Q4" : Me.btnQuarter4V.Enabled = True
                Case "S1" : Me.btnSemester1V.Enabled = True
                Case "S2" : Me.btnSemester2V.Enabled = True
                Case "Y" : Me.btnYearlyV.Enabled = True
            End Select
        End If
        If Not Me.btnQuarter1V.Enabled Then : Me.btnQuarter1V.Checked = False : End If
        If Not Me.btnQuarter2V.Enabled Then : Me.btnQuarter2V.Checked = False : End If
        If Not Me.btnQuarter3V.Enabled Then : Me.btnQuarter3V.Checked = False : End If
        If Not Me.btnQuarter4V.Enabled Then : Me.btnQuarter4V.Checked = False : End If
        If Not Me.btnSemester1V.Enabled Then : Me.btnSemester1V.Checked = False : End If
        If Not Me.btnSemester2V.Enabled Then : Me.btnSemester2V.Checked = False : End If
        If Not Me.btnYearlyV.Enabled Then : Me.btnYearlyV.Checked = False : End If
    End Sub
    Private Enum StatusProgress
        ProcessingAcrrue
        LoadingAccrue
        GeneratingDiscount
        WaitingForAnotherProcess
        CalculatingDPDByValue
        ApplyingDiscount
        None
    End Enum
    Private Enum LoadReportFrom
        FirsLoadedForm
        AfterLoadedForm
    End Enum

    Private Enum LoadAchievementFrom
        None
        Value
        Volume
        ValueWithApplyDisc
        VolumeWithApplyDisc
        LastView 'dari settingan
    End Enum
    Friend Sub LoadData()
        'Dim DV As DataView = Me.clsTA.GetDistributorAgrement()
        'Me.BindMultiColumnCombo(DV, Me.mcbDistributor, True, "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
    End Sub
    Private Function GetFlag(ByVal Flag As String) As String
        Dim strFlag As String = ""
        Select Case Me.cmbFlag.Text
            Case "QUARTER 1" : strFlag = "Q1"
            Case "QUARTER 2" : strFlag = "Q2"
            Case "QUARTER 3" : strFlag = "Q3"
            Case "QUARTER 4" : strFlag = "Q4"
            Case "SEMESTER 1" : strFlag = "S1"
            Case "SEMESTER 2" : strFlag = "S2"
            Case "YEARLY" : strFlag = "Y"
        End Select
        Return strFlag
    End Function
    Private Enum SelectedGrid
        Header
        Detail
        DetailDPD
    End Enum
    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.TickCount >= Me.ResultRandom Then
            If Me.HasLoadReport = True Then
                Me.BindGrid()
                '    Me.closeProgressbar()
                Me.SP = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False
                Me.TickCount = 0 : Me.HasLoadReport = True
                Me.Cursor = Cursors.Default
            Else
                Me.ResultRandom += 1
            End If
        End If
    End Sub

    Private Sub EnabledFlag(ByVal flag As String)
        Me.btnQuarter1.Enabled = (flag = "Q1")
        If Not btnQuarter1.Enabled Then : Me.btnQuarter1.Checked = False : End If
        Me.btnQuarter2.Enabled = (flag = "Q2")
        If Not Me.btnQuarter2.Enabled Then : Me.btnQuarter2.Checked = False : End If
        Me.btnQuarter3.Enabled = (flag = "Q3")
        If Not Me.btnQuarter3.Enabled Then : Me.btnQuarter3.Checked = False : End If
        Me.btnQuarter4.Enabled = (flag = "Q4")
        If Not Me.btnQuarter4.Enabled Then : Me.btnQuarter4.Checked = False : End If
        Me.btnSemester1.Enabled = (flag = "S1")
        If Not Me.btnSemester1.Enabled Then : Me.btnSemester1.Checked = False : End If
        Me.btnSemester2.Enabled = (flag = "S2")
        If Not Me.btnSemester2.Enabled Then : Me.btnSemester2.Checked = False : End If
        Me.btnYearly.Enabled = (flag = "Y")
        If Not Me.btnYearly.Enabled Then : Me.btnYearly.Checked = False : End If
    End Sub

    Private Sub ShowLoading()
        LD = New Loading
        LD.Show() : LD.TopMost = True
        Application.DoEvents()
        'Dim BFind As Boolean = False
        'BFind = Me.clsTA.hasReservedInvoice(Me.OtherUserProcessing)
        While Not Me.SP = StatusProgress.None
            'If BFind Then
            '    If Me.OtherUserProcessing <> NufarmBussinesRules.User.UserLogin.UserName Then
            '        While BFind
            '            Thread.Sleep(1000)
            '            Me.LD.Label1.Text = "Waiting for " + Me.OtherUserProcessing + " to finish processing procedure"
            '            Application.DoEvents()
            '            Me.LD.Refresh()
            '            BFind = Me.clsTA.hasReservedInvoice(Me.OtherUserProcessing)
            '            If BFind Then
            '                If Me.OtherUserProcessing <> NufarmBussinesRules.User.UserLogin.UserName Then
            '                    isAwaiting = True
            '                Else
            isAwaiting = False
            '                    BFind = False
            '                End If
            '            End If
            '        End While
            '    End If
            'Else
            '    isAwaiting = False
            'End If
            'Thread.Sleep(1000)
            'Me.LD.Refresh()
            'Me.LD.Label1.Text = "Waiting for " + Me.OtherUserProcessing + " to finish processing procedure"


            'BFind = Me.clsTA.hasReservedInvoice(Me.OtherUserProcessing)
            'If BFind Then
            '    If Me.OtherUserProcessing <> NufarmBussinesRules.User.UserLogin.UserName Then
            '        isAwaiting = True
            '    Else
            '        isAwaiting = False
            '        BFind = False
            '    End If
            'End If
            'If Not BFind Then
            'Me.SP = StatusProgress.ProcessingAcrrue
            Me.LD.Label1.Text = "Processing procedure...."
            Thread.Sleep(50)
            LD.Refresh()
            Application.DoEvents()
        End While
        Thread.Sleep(50)
        LD.Close() : LD = Nothing
    End Sub
    Private Sub getDS(ByVal Sprog As StatusProgress)
        Try
            Select Case Sprog
                Case StatusProgress.GeneratingDiscount
                    Me.IsGeneratingOA = True
                    Dim CanUpdate As Boolean = False
                    Dim tblListAchievementBrandpack As New DataTable("T_AchievementBrandPack")
                    With tblListAchievementBrandpack
                        .Columns.Add(New DataColumn("ACHIEVEMENT_BRANDPACK_ID", Type.GetType("System.String")))
                        .Columns.Add(New DataColumn("CAN_UPDATE", Type.GetType("System.Boolean")))
                        .Columns("CAN_UPDATE").DefaultValue = False
                    End With
                    For i As Integer = 0 To Me.GridEX2.RecordCount - 1
                        Me.GridEX2.MoveTo(i)
                        If Me.GridEX2.GetRow.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                            If Convert.ToDecimal(Me.GridEX2.GetValue("LEFT_QTY")) > 0 Then
                                Dim newRow As DataRow = tblListAchievementBrandpack.NewRow()
                                newRow("ACHIEVEMENT_BRANDPACK_ID") = Me.GridEX2.GetValue("ACHIEVEMENT_BRANDPACK_ID").ToString()
                                newRow("ACHIEVEMENT_BRANDPACK_ID") = Me.GridEX2.GetValue("ACHIEVEMENT_BRANDPACK_ID").ToString()
                                If Convert.ToBoolean(Me.GridEX2.GetValue("CAN_UPDATE")) = True Then
                                    newRow("CAN_UPDATE") = True : CanUpdate = True
                                End If
                                newRow.EndEdit()
                                tblListAchievementBrandpack.Rows.Add(newRow)
                            End If
                            'If Not ListAgree.Contains(Me.GridEX2.GetValue("AGREEMENT_NO").ToString()) Then
                            '    ListAgree.Add(Me.GridEX2.GetValue("AGREEMENT_NO").ToString())
                            'End If
                        End If
                    Next
                    If (tblListAchievementBrandpack.Rows.Count <= 0) Then
                        Me.SP = StatusProgress.None : Me.SP = StatusProgress.None
                        Me.ShowMessageInfo("Please chek list data and make sure Left_Qty > 0")
                        Me.Cursor = Cursors.Default : Return
                    End If
                    'For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX2.GetCheckedRows
                    '    If Convert.ToDecimal(Me.GridEX2.GetValue("LEFT_QTY")) > 0 Then
                    '        Dim newRow As DataRow = tblListAchievementBrandpack.NewRow()
                    '        newRow("ACHIEVEMENT_BRANDPACK_ID") = row.Cells("ACHIEVEMENT_BRANDPACK_ID").Value
                    '        If Convert.ToBoolean(row.Cells("CAN_UPDATE").Value) = True Then
                    '            newRow("CAN_UPDATE") = True : CanUpdate = True
                    '        End If
                    '        newRow.EndEdit()
                    '        tblListAchievementBrandpack.Rows.Add(newRow)
                    '    End If
                    'Next

                    If Not IsNothing(Me.DS) Then
                        Me.IsGeneratingOA = True
                        Dim dtTable As DataTable = Nothing
                        If (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) _
                        And (Not IsNothing(Me.chkDistributors.CheckedValues)) Then
                            If Me.chkDistributors.CheckedValues.Length > 0 Then
                                dtTable = Me.clsTA.GenerateDiscountForOA(tblListAchievementBrandpack, Me.Flag, Me.mcbDistributor.Value.ToString(), _
                                                            ListAgreementNo, CanUpdate)
                            Else
                                dtTable = Me.clsTA.GenerateDiscountForOA(tblListAchievementBrandpack, Me.Flag, Me.mcbDistributor.Value.ToString(), , CanUpdate)
                            End If
                        ElseIf (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                            dtTable = Me.clsTA.GenerateDiscountForOA(tblListAchievementBrandpack, Me.Flag, Me.mcbDistributor.Value.ToString(), , CanUpdate)
                        ElseIf Not IsNothing(Me.chkDistributors.CheckedValues) Then
                            If (Me.chkDistributors.CheckedValues.Length > 0) Then
                                dtTable = Me.clsTA.GenerateDiscountForOA(tblListAchievementBrandpack, Me.Flag, , _
                                                                ListAgreementNo, CanUpdate)
                            Else
                                dtTable = Me.clsTA.GenerateDiscountForOA(tblListAchievementBrandpack, Me.Flag, , , CanUpdate)
                            End If

                        Else
                            dtTable = Me.clsTA.GenerateDiscountForOA(tblListAchievementBrandpack, Me.Flag, , , CanUpdate)
                        End If
                        If Not IsNothing(dtTable) Then
                            Me.DS.Tables.Remove(Me.DS.Tables("ACHIEVEMENT_DETAIL")) : Me.DS.Tables.Add(dtTable)
                        End If
                    End If
                Case StatusProgress.LoadingAccrue
                    If Me.Flag = "" Then
                        Me.SP = StatusProgress.None : Me.Cursor = Cursors.Default
                        MessageBox.Show("System can not view accrue when flag is not defined", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                    ''check if Disc has been Applied by (Volume/Value

                    If (Not IsNothing(Me.mcbDistributor.Value)) And (Not IsNothing(Me.chkDistributors.CheckedValues)) Then
                        If Me.chkDistributors.CheckedValues.Length > 0 Then
                            Me.DS = Me.clsTA.GetAccrued(Me.Flag, Me.mcbDistributor.Value.ToString(), ListAgreementNo)
                        Else
                            Me.DS = Me.clsTA.GetAccrued(Me.Flag, Me.mcbDistributor.Value.ToString())
                        End If
                    ElseIf Not IsNothing(Me.chkDistributors.CheckedValues) Then
                        If Me.chkDistributors.CheckedValues.Length > 0 Then
                            For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
                                If Not ListAgreementNo.Contains(Me.chkDistributors.CheckedValues.GetValue(i).ToString()) Then
                                    ListAgreementNo.Add(Me.chkDistributors.CheckedValues.GetValue(i).ToString())
                                End If
                            Next
                            Me.DS = Me.clsTA.GetAccrued(Me.Flag, , ListAgreementNo)
                        End If
                    ElseIf (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                        Me.DS = Me.clsTA.GetAccrued(Me.Flag, Me.mcbDistributor.Value.ToString())
                    Else
                        Me.DS = Me.clsTA.GetAccrued(Me.Flag)
                    End If

                Case StatusProgress.ProcessingAcrrue

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
                            Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, Me.mcbDistributor.Value.ToString(), DVAgreement.ToTable())
                        Else
                            Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, Me.mcbDistributor.Value.ToString())
                        End If
                    ElseIf (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                        Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, Me.mcbDistributor.Value.ToString())
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
                            Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, , DVAgreement.ToTable())
                        Else
                            Me.DS = Me.clsTA.CalculateAccrue(Me.Flag)
                        End If
                    ElseIf Me.Flag <> "" Then
                        Me.DS = Me.clsTA.CalculateAccrue(Me.Flag)
                    End If
                Case StatusProgress.CalculatingDPDByValue
                    Dim DVAgreement As DataView = Nothing 'DirectCast(Me.chkDistributors.DropDownDataSource, DataView).ToTable()
                    Dim DVAgreeOri As DataView = Nothing
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
                    If ListAgreementNo.Count <= 0 Then
                        Me.SP = StatusProgress.None
                        Me.Timer1.Stop() : Me.Timer1.Enabled = False : Me.TickCount = 0
                        Me.baseTooltip.Show("Please enter agreement no", Me.chkDistributors, 2500)
                        Me.chkDistributors.Focus() : Me.Cursor = Cursors.Default
                        Me.HasLoadReport = True : Me.IsGeneratingOA = False : Me.LAF = LoadAchievementFrom.None
                        Return
                    End If
                    Me.DS = Me.clsTA.CalculateDPDToValue(Me.Flag, ListAgreementNo, DVAgreement.ToTable())
            End Select
            Me.HasLoadReport = True
            Me.IsGeneratingOA = False

        Catch ex As Exception
            'If Me.SP = StatusProgress.ProcessingAcrrue Then : Me.EnabledFlag("") : End If
            Me.HasLoadReport = True : Me.SP = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False : Me.TickCount = 0
            Me.IsGeneratingOA = False
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_getDS") : Me.Cursor = Cursors.Default
            Me.LAF = LoadAchievementFrom.None
            'Finally
            '    Me.SP = StatusProgress.None
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            Me.isLoadingRow = True
            If Not IsNothing(Me.DS) Then
                If Me.IsGeneratingOA Then
                    If Me.chkFilter.Checked Then
                        If Not IsNothing(Me.GridEX1.DataSource) Then
                            If Me.GridEX1.SelectedItems.Count > 0 Then
                                Dim IndexRow As Integer = Me.GridEX1.Row
                                Me.GridEX1.Row = IndexRow
                                Me.GridEX2.SetDataBinding(Me.DS.Tables("ACHIEVEMENT_DETAIL").DefaultView(), "")
                                Me.isLoadingRow = False
                                Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
                                Me.GridEX2_CurrentCellChanged(Me.GridEX2, New EventArgs())
                            End If
                        End If
                    Else
                        Me.GridEX2.SetDataBinding(Me.DS.Tables("ACHIEVEMENT_DETAIL").DefaultView(), "")
                        Me.GridEX2.RemoveFilters()
                        Me.GridEX3.SetDataBinding(Me.DS.Tables("DETAIL_RELEASED_DPD").DefaultView(), "")
                        Me.GridEX3.RemoveFilters()
                    End If
                Else
                    Me.GridEX1.SetDataBinding(Me.DS.Tables("ACHIEVEMENT_HEADER").DefaultView, "")
                    Me.GridEX1.DropDowns("BRAND").SetDataBinding(Me.DS.Tables("T_BRAND").DefaultView(), "")
                    Me.GridEX2.SetDataBinding(Me.DS.Tables("ACHIEVEMENT_DETAIL").DefaultView(), "")
                    Me.GridEX3.SetDataBinding(Me.DS.Tables("DETAIL_RELEASED_DPD").DefaultView(), "")
                    If Me.chkFilter.Checked Then
                        Me.isLoadingRow = False
                        Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
                        Me.GridEX2_CurrentCellChanged(Me.GridEX2, New EventArgs())
                    End If
                End If
            Else
                Me.GridEX1.SetDataBinding(Nothing, "") : Me.GridEX2.SetDataBinding(Nothing, "") : Me.GridEX3.SetDataBinding(Nothing, "")
            End If
            Dim HasCombinedTarget As Boolean = Me.DS.Tables("ACHIEVEMENT_HEADER").Select("ISCOMBINED_TARGET = " & True).Length > 0
            Dim HasSharingTarget As Boolean = Me.DS.Tables("ACHIEVEMENT_HEADER").Select("T_GROUP = 'YESS'").Length > 0
            Dim HasComputedAgree As Boolean = Me.DS.Tables("ACHIEVEMENT_HEADER").Select("AGREE_ACH_BY <> ''").Length > 0
            Dim listCols As New List(Of String)
            Dim ListColsCopy As New List(Of String)
            Select Case Me.Flag
                Case "Q1" : listCols.AddRange(New String() {"LEFT_PERIODE_Q3_BEFORE", "TOTAL_LEFT_Q3_BEFORE", "LEFT_PERIODE_Q4", "TOTAL_LEFT_Q4", "LEFT_PERIODE_S2", "TOTAL_LEFT_S2"})
                    ListColsCopy.AddRange(New String() {"LEFT_PERIODE_Q3_BEFORE", "TOTAL_LEFT_Q3_BEFORE", "LEFT_PERIODE_Q4", "TOTAL_LEFT_Q4", "LEFT_PERIODE_S2", "TOTAL_LEFT_S2"})
                Case "Q2" : listCols.AddRange(New String() {"LEFT_PERIODE_Q4", "TOTAL_LEFT_Q4", "LEFT_PERIODE_Q1", "TOTAL_LEFT_Q1"})
                    ListColsCopy.AddRange(New String() {"LEFT_PERIODE_Q4", "TOTAL_LEFT_Q4", "LEFT_PERIODE_Q1", "TOTAL_LEFT_Q1"})
                Case "Q3" : listCols.AddRange(New String() {"LEFT_PERIODE_Q1", "TOTAL_LEFT_Q1", "LEFT_PERIODE_Q2", "TOTAL_LEFT_Q2"})
                    ListColsCopy.AddRange(New String() {"LEFT_PERIODE_Q1", "TOTAL_LEFT_Q1", "LEFT_PERIODE_Q2", "TOTAL_LEFT_Q2"})
                Case "Q4" : listCols.AddRange(New String() {"LEFT_PERIODE_Q2", "TOTAL_LEFT_Q2", "LEFT_PERIODE_CUR_Q3", "TOTAL_LEFT_CUR_Q3"})
                    ListColsCopy.AddRange(New String() {"LEFT_PERIODE_Q2", "TOTAL_LEFT_Q2", "LEFT_PERIODE_CUR_Q3", "TOTAL_LEFT_CUR_Q3"})
                Case "S1" : listCols.AddRange(New String() {"LEFT_PERIODE_S2", "TOTAL_LEFT_S2", "LEFT_PERIODE_Q3_BEFORE", "TOTAL_LEFT_Q3_BEFORE", "LEFT_PERIODE_Q4", "TOTAL_LEFT_Q4"})
                    ListColsCopy.AddRange(New String() {"LEFT_PERIODE_S2", "TOTAL_LEFT_S2", "LEFT_PERIODE_Q3_BEFORE", "TOTAL_LEFT_Q3_BEFORE", "LEFT_PERIODE_Q4", "TOTAL_LEFT_Q4"})
                Case "S2" : listCols.AddRange(New String() {"LEFT_PERIODE_S1", "TOTAL_LEFT_S1"})
                    ListColsCopy.AddRange(New String() {"LEFT_PERIODE_S1", "TOTAL_LEFT_S1"})
            End Select
            Me.HideColumnsPBOrCP(Me.GridEX1, listCols, True)
            For Each colTotal As String In ListColsCopy
                If colTotal.Contains("TOTAL_LEFT") Then
                    listCols.Remove(colTotal)
                End If
            Next
            Me.HideColumnsPBOrCP(Me.GridEX2, listCols, True)
            Select Case Me.LAF
                Case LoadAchievementFrom.Value
                    Me.ShowVolumeInfo(False)
                    Me.ShowValueInfo(True)
                    Me.GridEX1.RootTable.Columns("DISC_QTY").Visible = False
                    Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = False
                    Me.GridEX1.RootTable.Columns("TOTAL_PO_VALUE").Visible = True
                    Me.GridEX2.RootTable.Columns("DISC_QTY").Visible = False
                    Me.GridEX2.RootTable.Columns("DISC_BY_VOLUME").Visible = False
                    Me.GridEX2.RootTable.Columns("DISC_BY_VALUE").Visible = True
                    Me.GridEX2.RootTable.Columns("RELEASE_QTY").Visible = False
                    Me.GridEX2.RootTable.Columns("LEFT_QTY").Visible = False
                    Me.GridEX2.RootTable.Columns("REMAIND_QTY").Visible = False
                    If HasCombinedTarget Then
                        Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").Visible = False
                        Me.GridEX1.RootTable.Columns("ACTUAL").Visible = False
                        Me.GridEX1.RootTable.Columns("ACTUAL_DISTRIBUTOR").Visible = True
                        Me.GridEX1.RootTable.Columns("ACTUAL_DIST_VALUE").Visible = True
                    Else
                        Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").Visible = True
                        Me.GridEX1.RootTable.Columns("ACTUAL").Visible = True
                        Me.GridEX1.RootTable.Columns("ACTUAL_DISTRIBUTOR").Visible = False
                        Me.GridEX1.RootTable.Columns("ACTUAL_DIST_VALUE").Visible = False
                    End If
                    AddConditionalFormating(HasSharingTarget, HasCombinedTarget)
                Case LoadAchievementFrom.ValueWithApplyDisc
                    Me.ShowVolumeInfo(False)
                    Me.ShowValueInfo(True)
                    Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = True
                    Me.GridEX1.RootTable.Columns("DISC_QTY").Visible = True
                    Me.GridEX1.RootTable.Columns("DISC_BY_VALUE").Visible = False
                    Me.GridEX1.RootTable.Columns("TOTAL_PO_VALUE").Visible = True
                    Me.GridEX2.RootTable.Columns("DISC_BY_VALUE").Visible = False
                    Me.GridEX2.RootTable.Columns("DISC_BY_VOLUME").Visible = False
                    Me.GridEX2.RootTable.Columns("DISC_QTY").Visible = True
                    Me.GridEX2.RootTable.Columns("RELEASE_QTY").Visible = True
                    Me.GridEX2.RootTable.Columns("LEFT_QTY").Visible = True
                    Me.GridEX2.RootTable.Columns("REMAIND_QTY").Visible = True
                    If HasCombinedTarget Then
                        Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").Visible = False
                        Me.GridEX1.RootTable.Columns("ACTUAL").Visible = False
                        Me.GridEX1.RootTable.Columns("ACTUAL_DISTRIBUTOR").Visible = True
                        Me.GridEX1.RootTable.Columns("ACTUAL_DIST_VALUE").Visible = True
                    Else
                        Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").Visible = True
                        Me.GridEX1.RootTable.Columns("ACTUAL").Visible = True
                        Me.GridEX1.RootTable.Columns("ACTUAL_DISTRIBUTOR").Visible = False
                        Me.GridEX1.RootTable.Columns("ACTUAL_DIST_VALUE").Visible = False
                    End If
                    AddConditionalFormating(HasSharingTarget, HasCombinedTarget)
                Case LoadAchievementFrom.Volume
                    Me.ShowVolumeInfo(True)
                    Me.ShowValueInfo(False)
                    Me.GridEX1.RootTable.Columns("DISC_QTY").Visible = False
                    Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = False
                    Me.GridEX1.RootTable.Columns("ACTUAL").Visible = True

                    Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").Visible = False
                    Me.GridEX1.RootTable.Columns("ACTUAL_DISTRIBUTOR").Visible = False
                    Me.GridEX1.RootTable.Columns("ACTUAL_DIST_VALUE").Visible = False

                    Me.GridEX2.RootTable.Columns("RELEASE_QTY").Visible = False
                    Me.GridEX2.RootTable.Columns("LEFT_QTY").Visible = False
                    Me.GridEX2.RootTable.Columns("REMAIND_QTY").Visible = False
                    Me.GridEX2.RootTable.Columns("DISC_QTY").Visible = False
                    Me.GridEX2.RootTable.Columns("DISC_BY_VOLUME").Visible = True
                    Me.GridEX2.RootTable.Columns("DISC_BY_VALUE").Visible = False
                    'AddConditionalFormating(HasSharingTarget, HasCombinedTarget)
                Case LoadAchievementFrom.VolumeWithApplyDisc
                    Me.ShowVolumeInfo(True)
                    Me.ShowValueInfo(False)
                    Me.GridEX1.RootTable.Columns("DISC_BY_VOLUME").Visible = False
                    Me.GridEX1.RootTable.Columns("DISC_QTY").Visible = True
                    Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = True
                    Me.GridEX1.RootTable.Columns("ACTUAL").Visible = True

                    Me.GridEX2.RootTable.Columns("RELEASE_QTY").Visible = True
                    Me.GridEX2.RootTable.Columns("LEFT_QTY").Visible = True
                    Me.GridEX2.RootTable.Columns("REMAIND_QTY").Visible = True
                    Me.GridEX2.RootTable.Columns("DISC_QTY").Visible = True
                    Me.GridEX2.RootTable.Columns("DISC_BY_VOLUME").Visible = False
                    Me.GridEX2.RootTable.Columns("DISC_BY_VALUE").Visible = False

                    Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").Visible = False
                    Me.GridEX1.RootTable.Columns("ACTUAL_DISTRIBUTOR").Visible = False
                    Me.GridEX1.RootTable.Columns("ACTUAL_DIST_VALUE").Visible = False
                    'AddConditionalFormating(HasSharingTarget, HasCombinedTarget)
            End Select
            ''================CHECKING IF DISCOUNT BY VOLUME HAS BEEN COMPUTED====================

            Me.checkEnabledFlagValue(False)
            Dim ListFlag As New List(Of String)
            Dim Flag As String = Me.GetFlag(Me.cmbFlag.Text)
            Select Case Flag
                'Case "Q1"
                'Case "S1"
                Case "Q2" : ListFlag.Add("Q1")
                Case "Q3" : ListFlag.AddRange(New String() {"Q1", "Q2"})
                Case "Q4" : ListFlag.AddRange(New String() {"Q1", "Q2", "Q3"})
                Case "S2" : ListFlag.Add("S1")
                Case "Y" : ListFlag.AddRange(New String() {"S1", "S2", "Q1", "Q2", "Q3", "Q4"})
            End Select
            Dim lastAgreeAch As String = "", IsHasComputedDPD As Boolean = False
            If ListFlag.Count > 0 Then
                If Me.clsTA.HasAppliedDisc(ListAgreementNo, ListFlag, lastAgreeAch, IsHasComputedDPD, False) Then
                    Me.btnApplyDiscByVolume.Enabled = True
                    Me.btnApplyDiscByValue.Enabled = True
                    If lastAgreeAch = "VOL" Then
                        Me.btnApplyDiscByValue.Enabled = False
                        Me.btnRecomputeByValue.Enabled = False
                    ElseIf lastAgreeAch = "VAL" Then
                        Me.btnApplyDiscByVolume.Enabled = False
                        Me.btnRecomputeByVolume.Enabled = False
                    End If
                End If
            End If
            ''==========================================================================
        Catch ex As Exception
            ''================CHECKING IF DISCOUNT BY VOLUME HAS BEEN COMPUTED====================
            Me.checkEnabledFlagValue(True)

            ''==========================================================================
        Finally

            Me.isLoadingRow = False : Me.IsGeneratingOA = False
            Me.ctmnComputeByValue.Enabled = ((Me.GridEX1.RecordCount > 0 And Me.chkDistributors.CheckedValues.Length > 0) _
                                            And (Me.LAF = LoadAchievementFrom.Volume Or Me.LAF = LoadAchievementFrom.VolumeWithApplyDisc Or Me.LAF = LoadAchievementFrom.None) _
                                            And (Me.btnRecomputeByValue.Enabled = True And Me.btnApplyDiscByValue.Enabled = True))
            Me.cxmnComputeByVolume.Enabled = ((Me.GridEX1.RecordCount > 0 And Me.chkDistributors.CheckedValues.Length > 0) _
                                            And (Me.LAF = LoadAchievementFrom.Value Or Me.LAF = LoadAchievementFrom.ValueWithApplyDisc Or Me.LAF = LoadAchievementFrom.None) _
                                            And (Me.btnRecomputeByVolume.Enabled = True And Me.btnApplyDiscByVolume.Enabled = True))
        End Try
        'Me.isLoadingRow = True
    End Sub

    Private Sub AddConditionalFormating(ByVal HasSharingTarget As Boolean, ByVal HasCombinedtarget As Boolean)
        Me.GridEX1.RootTable.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
        Me.GridEX1.RootTable.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Dim colTargetValue As Janus.Windows.GridEX.GridEXColumn = Me.GridEX1.RootTable.Columns("TARGET_BY_VALUE")
        Dim colPOValue As Janus.Windows.GridEX.GridEXColumn = Me.GridEX1.RootTable.Columns("TOTAL_PO_VALUE")
        Me.GridEX1.RootTable.Groups.Clear()
        If Not HasSharingTarget Then
            'jika agreementNya banyak, bikin group
            If ListAgreementNo.Count > 1 Then
                Dim groupAgr As New Janus.Windows.GridEX.GridEXGroup(Me.GridEX1.RootTable.Columns("AGREEMENT_NO"), Janus.Windows.GridEX.SortOrder.Ascending, Janus.Windows.GridEX.GroupInterval.Value)
                groupAgr.GroupPrefix = ""
                Me.GridEX1.RootTable.Groups.Add(groupAgr)
                Me.GridEX1.RootTable.Columns("AGREEMENT_NO").Visible = False
                Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Expanded
                Me.GridEX1.RootTable.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
                Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
                Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
                Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").Width = 130
                'Dim FAgreementNo As New Janus.Windows.GridEX.GridEXFilterCondition()
                'FAgreementNo.Column = Me.GridEX1.RootTable.Columns("AGREEMENT_NO")
                'FAgreementNo.ConditionOperator = Janus.Windows.GridEX.ConditionOperator.Equal
                'For i As Integer = 0 To Me.ListAgreementNo.Count - 1
                '    FAgreementNo.Value1 = ListAgreementNo(i)
                'Next
                Dim TotalPO As Decimal = Me.GridEX1.GetTotalRow().GetSubTotal(colPOValue, Janus.Windows.GridEX.AggregateFunction.Sum)
                Dim totalTarget As Decimal = Me.GridEX1.GetTotalRow().GetSubTotal(colTargetValue, Janus.Windows.GridEX.AggregateFunction.Sum)
                Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").TotalFormatString = String.Format("TOTAL_ACH={0:P}", TotalPO / totalTarget)
            Else
                Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
                Me.GridEX1.RootTable.Columns("AGREEMENT_NO").Visible = True
                colTargetValue.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                colPOValue.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GridEX1.RootTable.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                If Me.chkDistributors.CheckedValues.Length <= 1 Then
                    Dim FAgreementNo As New Janus.Windows.GridEX.GridEXFilterCondition()
                    FAgreementNo.Column = Me.GridEX1.RootTable.Columns("AGREEMENT_NO")
                    FAgreementNo.ConditionOperator = Janus.Windows.GridEX.ConditionOperator.Equal
                    'FAgreementNo.AddCondition(
                    FAgreementNo.Value1 = Me.chkDistributors.CheckedValues.GetValue(0).ToString()
                    Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
                    Dim TotalAchievement As Decimal = Convert.ToDecimal(Me.GridEX1.GetTotal(colPOValue, Janus.Windows.GridEX.AggregateFunction.Sum, FAgreementNo)) / (Convert.ToDecimal(Me.GridEX1.GetTotal(colTargetValue, Janus.Windows.GridEX.AggregateFunction.Sum, FAgreementNo)))
                    Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                    Dim strTotalAchievement = String.Format("TOTAL_ACH = {0:p} ", TotalAchievement)
                    Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").TotalFormatString = strTotalAchievement.ToString()
                    Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").Width = 130
                End If
            End If
        Else
            colPOValue.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.None
            colTargetValue.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.None
            Me.GridEX1.RootTable.Columns("BALANCE_BY_VALUE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.None
            Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.None
            Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.None
            Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").TotalFormatString = ""
        End If
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
    Private Sub ShowValueInfo(ByVal isVisible As Boolean)
        Me.GridEX1.RootTable.Columns("TARGET_BY_VALUE").Visible = isVisible
        Me.GridEX1.RootTable.Columns("DISPRO_BY_VALUE").Visible = isVisible
        Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").Visible = isVisible
        Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").Visible = isVisible
        Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = isVisible
        Me.GridEX1.RootTable.Columns("DISC_BY_VALUE").Visible = isVisible
        Me.GridEX1.RootTable.Columns("AVG_PRICE_FM").Visible = isVisible
        Me.GridEX1.RootTable.Columns("AVG_PRICE_PL").Visible = isVisible
        Me.GridEX1.RootTable.Columns("BALANCE_BY_VALUE").Visible = isVisible
        Me.GridEX1.RootTable.Columns("TOTAL_PO_VALUE").Visible = isVisible
        'Dim rowCombined() As DataRow = Me.DS.Tables("ACHIEVEMENT_HEADER").Select("COMBINED_BRAND_ID <> ''", "", "")
        'If Not IsNothing(rowCombined) Then
        '    If rowCombined.Length > 0 Then
        '        Return
        '    End If
        'End If
        Me.GridEX1.RootTable.Columns("TARGET_BY_VALUE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        Me.GridEX1.RootTable.Columns("BALANCE_BY_VALUE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        Me.GridEX1.RootTable.Columns("ACTUAL_DIST_VALUE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    End Sub

    Private Sub ShowVolumeInfo(ByVal isVisible As Boolean)
        Me.GridEX1.RootTable.Columns("DISPRO").Visible = isVisible
        Me.GridEX1.RootTable.Columns("ACHIEVEMENT_DISPRO").Visible = isVisible
        Me.GridEX1.RootTable.Columns("DISC_BY_VOLUME").Visible = isVisible
        'Me.GridEX1.RootTable.Columns("DISC_DIST_BY_VOLUME").Visible = isVisible
        'BONUS_QTY(final bila di apply)
        'AGREE_ACH_BY(Final bila di Apply)
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
    Private Sub CalculateAccrue()
        Me.Cursor = Cursors.WaitCursor : Me.HasLoadReport = False
        'check ke database apakah sudah ada user yang reserved data
        Me.getDS(SP)
        Me.HasLoadReport = True : Me.BindGrid()
        Me.SP = StatusProgress.None
        If Me.clsTA.MessageError <> "" Then
            Me.ShowMessageInfo(Me.clsTA.MessageError)
        End If
        Me.OtherUserProcessing = ""
        'Try

        'Catch ex As Exception
        '    Me.SP = StatusProgress.None
        '    Me.HasLoadReport = True : Me.Timer1.Enabled = False
        '    Me.ShowMessageInfo(ex.Message)
        'Finally
        '    Me.Cursor = Cursors.Default
        'End Try
    End Sub
    Private Sub RemoveCheckedBar()
        For Each bar As DevComponents.DotNetBar.ButtonItem In Me.btnFlag.SubItems
            bar.Checked = False
        Next
    End Sub
    Private Function CheckValidProcDiscount()
        Dim ValProc As Boolean = True
        If IsNothing(Me.chkDistributors.CheckedValues()) Then
            Me.baseTooltip.Show("Please define agreementno", Me.chkDistributors, 2500)
            Me.chkDistributors.Focus()
            Return False
        ElseIf Me.chkDistributors.CheckedValues.Length <= 0 Then
            Me.baseTooltip.Show("Please define agreementno", Me.chkDistributors, 2500)
            Me.chkDistributors.Focus()
            Return False
        End If
        Return ValProc
    End Function
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
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
                Case "btnQuarter1"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.Flag = "Q1" : Me.btnQuarter1.Checked = True : Me.LAF = LoadAchievementFrom.Volume
                    'Me.SP = StatusProgress.WaitingForAnotherProcess

                    '===================COMMENT THIS AFTER DEBUGGING=================
                    'Me.isAwaiting = False
                    'Me.SP = StatusProgress.ProcessingAcrrue
                    '================================================================

                    '================UNCOMMENT THIS AFTER DEBUGGING===================
                    isAwaiting = True
                    Me.SP = StatusProgress.ProcessingAcrrue
                    ThreadProcess = New Thread(AddressOf ShowLoading)
                    ThreadProcess.Start()
                    '=================================================================
                    Me.Timer1.Enabled = True : Me.Timer1.Start()
                    'hidupkan timer
                Case "btnQuarter2" : Me.Flag = "Q2"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.btnQuarter2.Checked = True : Me.LAF = LoadAchievementFrom.Volume

                    '===================COMMENT THIS AFTER DEBUGGING=================
                    'Me.isAwaiting = False
                    'Me.SP = StatusProgress.ProcessingAcrrue
                    '================================================================

                    '================UNCOMMENT THIS AFTER DEBUGGING===================
                    Me.isAwaiting = True
                    Me.SP = StatusProgress.ProcessingAcrrue
                    ThreadProcess = New Thread(AddressOf ShowLoading)
                    ThreadProcess.Start()
                    '=================================================================

                    'hidupkan timer
                    Me.Timer1.Enabled = True : Me.Timer1.Start()
                Case "btnQuarter3" : Me.Flag = "Q3"

                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.btnQuarter3.Checked = True : Me.LAF = LoadAchievementFrom.Volume

                    ''===================COMMENT THIS AFTER DEBUGGING=================
                    'Me.isAwaiting = False
                    'Me.SP = StatusProgress.ProcessingAcrrue
                    '================================================================

                    '================UNCOMMENT THIS AFTER DEBUGGING===================
                    Me.isAwaiting = True
                    Me.SP = StatusProgress.ProcessingAcrrue
                    ThreadProcess = New Thread(AddressOf ShowLoading) : ThreadProcess.Start()

                    '=================================================================
                    'hidupkan timer
                    Me.Timer1.Enabled = True : Me.Timer1.Start()
                Case "btnQuarter4" : Me.Flag = "Q4"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.btnQuarter4.Checked = True : Me.LAF = LoadAchievementFrom.Volume
                    '===================COMMENT THIS AFTER DEBUGGING=================
                    'Me.isAwaiting = False
                    'Me.SP = StatusProgress.ProcessingAcrrue
                    '================================================================

                    '================UNCOMMENT THIS AFTER DEBUGGING===================
                    Me.isAwaiting = True
                    Me.SP = StatusProgress.ProcessingAcrrue
                    ThreadProcess = New Thread(AddressOf ShowLoading) : ThreadProcess.Start()
                    '=================================================================

                    'hidupkan timer
                    Me.Timer1.Enabled = True : Me.Timer1.Start()
                Case "btnSemester1" : Me.Flag = "S1"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.btnSemester1.Checked = True : Me.LAF = LoadAchievementFrom.Volume
                    '===================COMMENT THIS AFTER DEBUGGING=================
                    'Me.isAwaiting = False
                    'Me.SP = StatusProgress.ProcessingAcrrue
                    '================================================================

                    '================UNCOMMENT THIS AFTER DEBUGGING===================
                    Me.isAwaiting = False
                    Me.SP = StatusProgress.ProcessingAcrrue

                    ThreadProcess = New Thread(AddressOf ShowLoading)
                    ThreadProcess.Start()
                    '=================================================================

                    'hidupkan timer
                    Me.Timer1.Enabled = True : Me.Timer1.Start()
                Case "btnSemester2" : Me.Flag = "S2"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.btnSemester2.Checked = True : Me.LAF = LoadAchievementFrom.Volume
                    '===================COMMENT THIS AFTER DEBUGGING=================
                    'Me.isAwaiting = False
                    'Me.SP = StatusProgress.ProcessingAcrrue
                    '================================================================

                    '================UNCOMMENT THIS AFTER DEBUGGING===================
                    Me.isAwaiting = True
                    Me.SP = StatusProgress.ProcessingAcrrue
                    ThreadProcess = New Thread(AddressOf ShowLoading) : ThreadProcess.Start()
                    '=================================================================
                    'hidupkan timer
                    Me.Timer1.Enabled = True : Me.Timer1.Start()
                Case "btnYearly" : Me.Flag = "Y"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.btnYearly.Checked = True : Me.LAF = LoadAchievementFrom.Volume
                    Me.clsTA.mustRecomputeYear = True

                    '===================COMMENT THIS AFTER DEBUGGING=================
                    'Me.isAwaiting = False
                    'Me.SP = StatusProgress.ProcessingAcrrue
                    '================================================================

                    '================UNCOMMENT THIS AFTER DEBUGGING===================
                    Me.isAwaiting = True
                    Me.SP = StatusProgress.ProcessingAcrrue
                    ThreadProcess = New Thread(AddressOf ShowLoading) : ThreadProcess.Start()
                    '=================================================================
                    'hidupkan timer
                    Me.Timer1.Enabled = True : Me.Timer1.Start()
                Case "btnQuarter1V"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.CalculateDPDtoValue("Q1")
                Case "btnQuarter2V"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    CalculateDPDtoValue("Q2")
                Case "btnQuarter3V"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.CalculateDPDtoValue("Q3")
                Case "btnQuarter4V"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.CalculateDPDtoValue("Q4")
                Case "btnSemester1V"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.CalculateDPDtoValue("S1")
                Case "btnSemester2V"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.CalculateDPDtoValue("S2")
                Case "btnYearlyV"
                    If Not Me.CheckValidProcDiscount() Then : Return : End If
                    Me.CalculateDPDtoValue("Y")
                Case "btnApplyDiscByVolume"
                    If Not Me.ApplyDiscByVolume() Then
                        Me.SP = StatusProgress.None : Cursor = Cursors.Default : Return : End If
                Case "btnApplyDiscByValue"
                    If Not Me.ApplyDiscByValue() Then : Me.SP = StatusProgress.None : Cursor = Cursors.Default : Return : End If
                Case "btnRefresh"
                    Me.btnAplyFilterPencapaian_Click(Me.btnAplyRange, New EventArgs())
            End Select

        Catch ex As Exception
            Me.LAF = LoadAchievementFrom.None
            Me.clsTA.mustRecomputeYear = False : Me.isAwaiting = False : Me.Timer1.Stop() : Me.Timer1.Enabled = False : Me.SP = StatusProgress.None : Me.HasLoadReport = False : Me.RemoveCheckedBar() : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function ApplyDiscByVolume() As Boolean
        ''check apakah data sudah di pakai oleh CSE
        If IsNothing(Me.chkDistributors.CheckedValues()) Then
            Me.baseTooltip.Show("Please define agreementNo", Me.chkDistributors, 2500) : Me.chkDistributors.Focus() : Return False
        ElseIf Me.chkDistributors.CheckedValues.Length <= 0 Then
            Me.baseTooltip.Show("Please define agreementNo", Me.chkDistributors, 2500) : Me.chkDistributors.Focus() : Return False
        End If
        Me.LAF = LoadAchievementFrom.VolumeWithApplyDisc

        '==========Uncomment this after debugging=============================================
        Me.isAwaiting = True
        Me.SP = StatusProgress.ApplyingDiscount
        ThreadProcess = New Thread(AddressOf ShowLoading) : ThreadProcess.Start()
        Me.Timer1.Enabled = True : Me.Timer1.Start()

        '=============================================================================

        ''==================COMMENT THIS AFTER DEBUGGING==============================
        'Me.isAwaiting = False : Me.SP = StatusProgress.ApplyingDiscount
        'Me.Timer1.Enabled = True : Me.Timer1.Start()
        ''==============================================================================

        Dim Flag As String = Me.GetFlag(Me.cmbFlag.Text)
        Dim ListAgreementNo As New List(Of String)
        For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
            Dim AgreementNO As String = Me.chkDistributors.CheckedValues().GetValue(i).ToString()
            'If Me.clsTA.hasRefdataINOA(AgreementNO, Flag, False) Then : Me.ShowMessageInfo("Can not compute data while discount has been proceed in OC") : Return False : End If
            If Not ListAgreementNo.Contains(AgreementNO) Then
                ListAgreementNo.Add(AgreementNO)
            End If
        Next
        ''pindahkan data dari DISC_BY_VOLUME ,AGREE_ACH_BY, DLL
        Me.DS = Me.clsTA.ApplyDiscByVolume(ListAgreementNo, Flag)
        ''show or hide column
        Return True
    End Function
    Private Function ApplyDiscByValue() As Boolean
        ''check apakah data sudah di pakai oleh CSE
        If IsNothing(Me.chkDistributors.CheckedValues()) Then
            Me.baseTooltip.Show("Please define agreementNo", Me.chkDistributors, 2500) : Me.chkDistributors.Focus() : Return False
        ElseIf Me.chkDistributors.CheckedValues.Length <= 0 Then
            Me.baseTooltip.Show("Please define agreementNo", Me.chkDistributors, 2500) : Me.chkDistributors.Focus() : Return False
        End If
        Me.LAF = LoadAchievementFrom.ValueWithApplyDisc

        '==========Uncomment this after debugging=============================================
        Me.isAwaiting = True
        Me.SP = StatusProgress.ApplyingDiscount
        ThreadProcess = New Thread(AddressOf ShowLoading) : ThreadProcess.Start()
        Me.Timer1.Enabled = True : Me.Timer1.Start()

        '=============================================================================


        ''==================COMMENT THIS AFTER DEBUGGING==============================
        'Me.isAwaiting = False : Me.SP = StatusProgress.ApplyingDiscount
        'Me.Timer1.Enabled = True : Me.Timer1.Start()
        ''==============================================================================

        Dim Flag As String = Me.GetFlag(Me.cmbFlag.Text)

        For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
            Dim AgreementNO As String = Me.chkDistributors.CheckedValues().GetValue(i).ToString()

            '==========Uncomment this after debugging=============================================
            If Me.clsTA.hasRefdataINOA(AgreementNO, Flag, False) Then : Me.ShowMessageInfo("Can not compute data while discount has been processed in OC") : Return False : End If
            '=========================================================================

            If Not ListAgreementNo.Contains(AgreementNO) Then
                ListAgreementNo.Add(AgreementNO)
            End If
        Next
        ''pindahkan data dari DISC_BY_VOLUME ,AGREE_ACH_BY, DLL
        Me.DS = Me.clsTA.ApplyDiscByValue(ListAgreementNo, Flag)
        ''show or hide column
        Return True
    End Function
    Private Sub CalculateDPDtoValue(ByVal Flag As String)
        Me.Flag = Flag : Me.LAF = LoadAchievementFrom.Value
        If Flag = "Q1" Then : Me.btnQuarter1V.Checked = True
        ElseIf Flag = "Q2" Then : Me.btnQuarter2V.Checked = True
        ElseIf Flag = "Q3" Then : Me.btnQuarter3V.Checked = True
        ElseIf Flag = "Q4" Then : Me.btnQuarter4V.Checked = True
        ElseIf Flag = "S1" Then : Me.btnSemester1V.Checked = True
        ElseIf Flag = "S2" Then : Me.btnSemester2V.Checked = True
        ElseIf Flag = "Y" Then : Me.btnYearlyV.Checked = True
        End If
        '==========Uncomment this after debugging=============================================
        Me.isAwaiting = True
        Me.SP = StatusProgress.CalculatingDPDByValue
        ThreadProcess = New Thread(AddressOf ShowLoading) : ThreadProcess.Start()
        Me.Timer1.Enabled = True : Me.Timer1.Start()

        '=============================================================================

        ''==================COMMENT THIS AFTER DEBUGGING==============================
        'Me.isAwaiting = False : Me.SP = StatusProgress.CalculatingDPDByValue
        'Me.Timer1.Enabled = True : Me.Timer1.Start()
        ''==============================================================================

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
            Dim DV As DataView = Me.clsTA.GetAgreementNo(Me.Flag, Me.mcbDistributor.Value.ToString())
            Me.BindCheckedCombo(DV, "AGREEMENT_NO", "AGREEMENT_NO", False)
            Me.baseTooltip.Show("Please checklist Agreement no", Me.chkDistributors, 2500)
            Me.baseTooltip.UseAnimation = True
            Me.baseTooltip.ToolTipTitle = "Attention"
            Me.baseTooltip.ToolTipIcon = ToolTipIcon.Info
            Dim listAgreements As New List(Of String)
            Me.checkEnabledFlagValue(True)
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbDistributor_ValueChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.isLoadingCombo = False : Me.Cursor = Cursors.Default
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
            Dim DV As DataView = Me.clsTA.GetDistributorAgrement(RefFlag, Me.mcbDistributor.Text)
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
                Dv = Me.clsTA.GetAgreementNo(Me.Flag, Me.mcbDistributor.Value.ToString(), Me.chkDistributors.Text, 5)
            Else
                Dv = Me.clsTA.GetAgreementNo(Me.Flag, , Me.chkDistributors.Text, 5)
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

    Private Sub btnAplyFilterPencapaian_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor : Me.isLoadingRow = True
            Me.HasLoadReport = False
            If Me.Flag = "" Then
                Me.ShowMessageInfo("Can not view Data " & vbCrLf & "Because flag is not defined.") : Return
            End If
            Me.OtherUserProcessing = ""

            '==============UNCOMMENT THIS AFTER DEBUGGING =======================================

            Me.isAwaiting = True
            Me.SP = StatusProgress.LoadingAccrue
            ThreadProcess = New Thread(AddressOf ShowLoading)
            ThreadProcess.Start()
            '=================================================================================

            If Me.LAF = LoadAchievementFrom.None Then
                Me.btnApplyDiscByVolume.Enabled = True
                Me.btnApplyDiscByValue.Enabled = True
                Me.btnRecomputeByVolume.Enabled = True
                Me.btnRecomputeByValue.Enabled = True
                Dim ListFlag As New List(Of String)
                Dim Flag As String = Me.GetFlag(Me.cmbFlag.Text)
                Select Case Flag
                    'Case "Q1"
                    'Case "S1"
                    Case "Q2" : ListFlag.Add("Q1")
                    Case "Q3" : ListFlag.AddRange(New String() {"Q1", "Q2"})
                    Case "Q4" : ListFlag.AddRange(New String() {"Q1", "Q2", "Q3"})
                    Case "S2" : ListFlag.Add("S1")
                    Case "Y" : ListFlag.AddRange(New String() {"S1", "S2", "Q1", "Q2", "Q3", "Q4"})
                End Select
                Dim LastAgreeAch As String = "", IsHasComputedDPD As Boolean = False
                If ListFlag.Count > 0 Then
                    If Me.clsTA.HasAppliedDisc(ListAgreementNo, ListFlag, LastAgreeAch, IsHasComputedDPD, False) Then
                        Me.btnApplyDiscByVolume.Enabled = True
                        Me.btnApplyDiscByValue.Enabled = True
                        If LastAgreeAch = "VOL" Then
                            Me.btnApplyDiscByValue.Enabled = False
                        ElseIf LastAgreeAch = "VAL" Then
                            Me.btnApplyDiscByVolume.Enabled = False
                        End If
                    End If
                End If
                ListFlag.Clear()
                Me.AgreeAchBy = ""
                ListFlag.Add(Me.GetFlag(Me.cmbFlag.Text))
                If Me.clsTA.HasAppliedDisc(ListAgreementNo, ListFlag, Me.AgreeAchBy, IsHasComputedDPD, False) Then
                    Select Case Me.AgreeAchBy
                        Case "VOL"
                            Me.LAF = LoadAchievementFrom.VolumeWithApplyDisc
                        Case "VAL"
                            Me.LAF = LoadAchievementFrom.ValueWithApplyDisc
                        Case Else : Me.LAF = LoadAchievementFrom.Volume
                    End Select
                    ''tetap harus merefere ke lastAgreeAch by
                    If LastAgreeAch = "VOL" Then
                        Me.LAF = LoadAchievementFrom.VolumeWithApplyDisc
                    ElseIf LastAgreeAch = "VAL" Then
                        Me.LAF = LoadAchievementFrom.ValueWithApplyDisc
                    End If
                Else
                    If LastAgreeAch = "VOL" Then
                        Me.LAF = LoadAchievementFrom.VolumeWithApplyDisc
                    ElseIf LastAgreeAch = "VAL" Then
                        Me.LAF = LoadAchievementFrom.ValueWithApplyDisc
                    Else
                        Me.LAF = LoadAchievementFrom.Volume
                    End If
                End If
            End If
            ''==================COMMENT THIS AFTER DEBUGGING==============================
            'Me.isAwaiting = False
            ''==============================================================================   
            'hidupkan(Timer)
            Me.Timer1.Enabled = True : Me.Timer1.Start()

        Catch ex As Exception
            Me.SP = StatusProgress.None : Me.HasLoadReport = True : Me.Timer1.Stop() : Me.Timer1.Enabled = False : Me.TickCount = 0
            Me.OtherUserProcessing = "" : Me.LogMyEvent(ex.Message, Me.Name + "_btnAplyFilterPencapaian_Click")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GridEX1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.Enter
        Me.Grid = Me.GridEX1
        Me.Panel2.BorderStyle = BorderStyle.Fixed3D
        Me.Panel1.BorderStyle = BorderStyle.None
        Me.pnlGridDPD.BorderStyle = BorderStyle.None
        Me.SelectGrid = SelectedGrid.Header
        Dim B As Boolean = Me.FilterEditor1.Visible
        Me.FilterEditor1.Visible = B
        Me.FilterEditor1.SourceControl = Me.Grid
        Me.FilterEditor1.SortFieldList = False
    End Sub

    Private Sub GridEX2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX2.Enter
        Me.Grid = Me.GridEX2
        Me.Panel1.BorderStyle = BorderStyle.Fixed3D
        Me.Panel2.BorderStyle = BorderStyle.None
        Me.pnlGridDPD.BorderStyle = BorderStyle.None
        Me.SelectGrid = SelectedGrid.Detail
        Dim B As Boolean = Me.FilterEditor1.Visible
        Me.FilterEditor1.Visible = B
        Me.FilterEditor1.SourceControl = Me.Grid
        Me.FilterEditor1.SortFieldList = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.isAwaiting Then
            Return
        End If
        If (Me.SP <> StatusProgress.None) And (Me.SP <> StatusProgress.WaitingForAnotherProcess) Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Me.Timer1.Stop()
                Me.Timer1.Enabled = False

                If Me.SP = StatusProgress.ApplyingDiscount Then ''DS di peroleh di procedure ApplyDiscByVolume
                Else
                    Me.getDS(Me.SP)
                End If
                Me.HasLoadReport = True : Me.BindGrid()
                Me.SP = StatusProgress.None
                If Me.clsTA.MessageError <> "" Then
                    Me.ShowMessageInfo(Me.clsTA.MessageError)
                End If
                Me.OtherUserProcessing = ""
                Me.SP = StatusProgress.None
                'Me.LAF = LoadAchievementFrom.None
                Me.IsGeneratingOA = False
            Catch ex As Exception
                Me.SP = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False
                Me.OtherUserProcessing = ""
                'Me.LAF = LoadAchievementFrom.None
                Me.IsGeneratingOA = False
                Me.Cursor = Cursors.Default
            End Try
            Me.isAwaiting = False : Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Achievement_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles Me.Disposed
        'Try
        '    Dim strLAF As String = ""
        '    If Me.LAF <> LoadAchievementFrom.None Then
        '        Select Case LAF
        '            Case LoadAchievementFrom.Value : strLAF = "Value"
        '            Case LoadAchievementFrom.ValueWithApplyDisc : strLAF = "ValueWithApplyDisc"
        '            Case LoadAchievementFrom.Volume : strLAF = "Volume"
        '            Case LoadAchievementFrom.VolumeWithApplyDisc : strLAF = "VolumeWithApplyDisc"
        '        End Select
        '    End If
        '    If Not String.IsNullOrEmpty(strLAF) Then
        '        Me.Rg.Write("LVAch", strLAF)
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub Achievement_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not IsNothing(Me.DS) Then
            Me.isLoadingRow = True
            Me.DS.Dispose()
            Me.clsTA.Dispose()
        End If
    End Sub

    Private Sub Accrued_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AddHandler Timer1.Tick, AddressOf ChekTimer
        Me.GridEX2_Enter(Me.GridEX1, New EventArgs())
        Me.IsHasLoadedForm = True
        Me.isLoadingCombo = False
        Me.EnabledFlag("")
        Dim listAgreements As New List(Of String)
        checkEnabledFlagValue(True)
        Me.ReadAcces()
        CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
        Me.ExpandableSplitter2.Expanded = False
        Me.ExpandableSplitter1.Expanded = False
        For i As Integer = 0 To Me.GridEX1.RootTable.Columns.Count - 1
            Me.ListDefaultVisibleColumns.Add(Me.GridEX1.RootTable.Columns(i).Key)
        Next
        Me.ctmnComputeByValue.Enabled = False
        Me.cxmnComputeByVolume.Enabled = False
        If Me.isTransitionTime Then
            Me.btnQuarter4.Visible = False
            Me.btnQuarter4V.Visible = False
        End If
    End Sub

    Private Sub btnGenerateOA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateOA.Click
        Try

            If Me.GridEX2.GetCheckedRows.Length <= 0 Then
                Me.ShowMessageInfo("Please check list the items " & vbCrLf & "those will be generated for OA") : Return
            End If
            If Me.Flag = "" Then
                Me.ShowMessageInfo("Please define Flag") : Return
            End If
            Me.Cursor = Cursors.WaitCursor
            'CHECK AGREEMENT_NO APAKAH SUDAH BOLEH DI RELEASE / BELUM
            'NTAR DI COMMENT
            Dim ListAgree As New List(Of String)
            For i As Integer = 0 To Me.GridEX2.RecordCount - 1
                Me.GridEX2.MoveTo(i)
                If Me.GridEX2.GetRow.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                    If Not ListAgree.Contains(Me.GridEX2.GetValue("AGREEMENT_NO").ToString()) Then
                        ListAgree.Add(Me.GridEX2.GetValue("AGREEMENT_NO").ToString())
                    End If
                End If
            Next
            If ListAgree.Count <= 0 Then : Return : End If
            Dim Message As String = ""
            If Not Me.clsTA.IsCanGenerate(Me.Flag, ListAgree, Message) Then
                Me.ShowMessageInfo("Agreement number for : " & vbCrLf & Message & vbCrLf & "Has not been closed yet.")
                Me.Cursor = Cursors.Default : Return
            End If
            If Me.ShowConfirmedMessage("Generate for OA ?" & vbCrLf & "After you generate them " & vbCrLf & "Discount can be released for OA" _
            & vbCrLf & "After Discount is released" & vbCrLf & "You can not make changes for achievement") = Windows.Forms.DialogResult.Yes Then
                'Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True : Application.DoEvents()
                'Me.SP = StatusProgress.GeneratingDiscount
                'Me.rnd = New Random()
                Me.SP = StatusProgress.GeneratingDiscount
                Me.isAwaiting = True
                Me.ThreadProcess = New Thread(AddressOf ShowLoading)
                Me.ThreadProcess.Start()
                'Me.getDS(Me.SP)
                'Me.rnd = New Random()
                'Me.ResultRandom = Me.rnd.Next(1, 4)
                Me.Timer1.Enabled = True : Me.Timer1.Start()
                'Me.Timer2.Enabled = True
            End If
            'Me.SP = StatusProgress.None
            'Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.SP = StatusProgress.None
            Me.Cursor = Cursors.Default : Me.HasLoadReport = True : Me.ShowMessageInfo(ex.Message)
            'Finally
            '    Me.Cursor = Cursors.Default
        Finally
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            'check reference data
            Me.Cursor = Cursors.WaitCursor
            '============UNCOMMENT THIS AFTER MAINTENANCE ====================
            If Me.clsTA.IsHasReferencedDataAcruedHeader(Me.GridEX1.GetValue("ACHIEVEMENT_ID").ToString(), False) Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData) : e.Cancel = True : Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True : Return
            End If
            '========================================================
            Dim AchievementID As String = Me.GridEX1.GetValue("ACHIEVEMENT_ID").ToString()
            'delete data beserta anaknya
            Me.clsTA.DeleteAccruedHeader(Me.GridEX1.GetValue("ACHIEVEMENT_ID").ToString())
            Dim DV As DataView = Me.DS.Tables(1).DefaultView()
            DV.Sort = "ACHIEVEMENT_ID"
            Dim Dr() As DataRowView = DV.FindRows(AchievementID)
            If Dr.Length > 0 Then
                For i As Integer = 0 To DV.Count - 1
                    Dim Index As Integer = DV.Find(AchievementID)
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

    Private Sub GridEX2_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs)
        Try
            If Me.clsTA.IshasReferencedDataAcrueDetail(Me.GridEX2.GetValue("ACHIEVEMENT_BRANDPACK_ID").ToString()) Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData) : e.Cancel = True : Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True : Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.clsTA.DeleteAccrueDetail(Me.GridEX2.GetValue("ACHIEVEMENT_BRANDPACK_ID"))
            e.Cancel = False
        Catch ex As Exception
            e.Cancel = True : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbFlag_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFlag.SelectedIndexChanged
        Try
            'bind multicolumn dimana agreement nya masih valid
            Me.Cursor = Cursors.WaitCursor
            If Me.isLoadingCombo Then : Return : End If
            Dim strFlag As String = ""
            If Me.cmbFlag.SelectedIndex <> -1 And Me.cmbFlag.Text <> "<< Choose flag >>" Then
                strFlag = Me.GetFlag(Me.cmbFlag.Text)
                Dim DV As DataView = Me.clsTA.GetDistributorAgrement(strFlag)
                Me.BindMultiColumnCombo(DV, Me.mcbDistributor, True, "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
                Me.BindCheckedCombo(Nothing, "", "", True)
                Me.Flag = Me.GetFlag(Me.cmbFlag.Text) : Me.EnabledFlag(Me.Flag)
            Else
                Me.EnabledFlag("")
            End If
            Dim listAgreements As New List(Of String)
            Me.checkEnabledFlagValue(True)
            'reset agreeAchievedBy
            Me.AgreeAchBy = ""
            'reset LAF
            Me.LAF = LoadAchievementFrom.None
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, "_cmbFlag_SelectedIndexChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX2_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX2.CellUpdated
        Me.GridEX2.UpdateData()
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            If Me.isLoadingRow Then : Return : End If
            If IsNothing(Me.GridEX1.DataSource) Then : Return : End If
            If Me.GridEX1.RecordCount <= 0 Then : Return : End If
            If Me.chkFilter.Checked Then
                If Me.GridEX1.SelectedItems.Count > 0 Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.isLoadingRow = True
                    If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                        If Not IsNothing(Me.GridEX2.DataSource) Then
                            Dim AchievementID As String = Me.GridEX1.GetValue("ACHIEVEMENT_ID").ToString()
                            Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                            DV.RowFilter = "ACHIEVEMENT_ID = '" & AchievementID & "'"

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
                            If item.Key.Contains("QTY") Or item.Key.Contains("VOLUME") Or item.Key.Contains("ACTUAL") Then
                                item.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                                item.TotalFormatString = "#,##.000"
                            ElseIf item.Key.Contains("VALUE") Then
                                item.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                                item.TotalFormatString = "#,##.00"
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
                            If item.Key.Contains("QTY") Then
                                item.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.None
                                item.TotalFormatString = "#,##.000"
                            End If
                        End If
                    Next
                End If
                Me.GridEX2.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            'sekarang filter datagrid 3

            If Not IsNothing(Me.GridEX2.DataSource) Then
                If Me.GridEX2.RecordCount > 0 Then
                    Dim AchiementBrandPackIDS As String = "IN('"
                    Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                    For i As Integer = 0 To DV.Count - 1
                        Dim AchBrandPackID As String = DV(i)("ACHIEVEMENT_ID").ToString() & "|" & DV(i)("BRANDPACK_ID").ToString()
                        AchiementBrandPackIDS &= "" & AchBrandPackID & "'"
                        If i < DV.Count - 1 Then
                            AchiementBrandPackIDS &= ",'"
                        End If
                    Next
                    AchiementBrandPackIDS &= ")"
                    If Not IsNothing(Me.GridEX3.DataSource) Then
                        DV = CType(Me.GridEX3.DataSource, DataView)
                        DV.RowFilter = "ACHIEVEMENT_BRANDPACK_ID " & AchiementBrandPackIDS
                        Me.GridEX3.SetDataBinding(DV, "")
                    End If
                End If
            End If
            Me.isLoadingRow = False
        Catch ex As Exception
            Me.isLoadingRow = False
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GridEX2_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX2.CurrentCellChanged
        If Me.isLoadingRow Then : Return : End If
        If IsNothing(Me.GridEX2.DataSource) Then : Return : End If
        If Me.GridEX2.RecordCount <= 0 Then : Return : End If
        If Me.GridEX2.SelectedItems.Count <= 0 Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.chkFilter.Checked Then
                If Not IsNothing(Me.GridEX3.DataSource) Then
                    Dim DV As DataView = CType(Me.GridEX3.DataSource, DataView)
                    DV.RowFilter = "" : Me.GridEX3.SetDataBinding(DV, "")
                Else
                    Me.GridEX3.SetDataBinding(Nothing, "")
                End If
                Return
            End If

            If Me.GridEX2.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If Not IsNothing(Me.GridEX3.DataSource) Then
                    Dim DV As DataView = CType(Me.GridEX3.DataSource, DataView)
                    Dim AchievementBrandPackID As String = Me.GridEX2.GetValue("ACHIEVEMENT_ID").ToString() & "|" & Me.GridEX2.GetValue("BRANDPACK_ID").ToString()
                    DV.RowFilter = "ACHIEVEMENT_BRANDPACK_ID = '" & AchievementBrandPackID & "'"
                    Me.GridEX3.SetDataBinding(DV, "")
                Else
                    Me.GridEX3.SetDataBinding(Nothing, "")
                End If
            Else
                If Not IsNothing(Me.GridEX3.DataSource) Then
                    Dim DV As DataView = CType(Me.GridEX3.DataSource, DataView)
                    DV.RowFilter = "" : Me.GridEX3.SetDataBinding(DV, "")
                Else
                    Me.GridEX3.SetDataBinding(Nothing, "")
                End If
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX3_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX3.Enter
        Me.Grid = Me.GridEX3
        Me.pnlGridDPD.BorderStyle = BorderStyle.Fixed3D
        Me.Panel2.BorderStyle = BorderStyle.None
        Me.Panel1.BorderStyle = BorderStyle.None
        Me.SelectGrid = SelectedGrid.DetailDPD
        Dim B As Boolean = Me.FilterEditor1.Visible
        Me.FilterEditor1.Visible = B
        Me.FilterEditor1.SourceControl = Me.Grid
        Me.FilterEditor1.SortFieldList = False
    End Sub
    Private Sub CheckedFilter()
        If Me.chkFilter.Checked Then
            Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
            Me.GridEX2_CurrentCellChanged(Me.GridEX2, New EventArgs())
        Else
            If Not IsNothing(Me.GridEX2.DataSource) Then
                Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                DV.RowFilter = ""
                Me.GridEX2.SetDataBinding(DV, "")
            End If
            If Not IsNothing(Me.GridEX3.DataSource) Then
                Dim DV As DataView = CType(Me.GridEX3.DataSource, DataView)
                DV.RowFilter = ""
                Me.GridEX3.SetDataBinding(DV, "")
            End If
        End If
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

    Private Sub chkDistributors_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDistributors.CheckedValuesChanged
        Try
            If Me.isLoadingCombo Then : Return : End If
            If IsNothing(Me.chkDistributors.CheckedValues) Then : Return : End If
            If Me.chkDistributors.CheckedValues.Length <= 0 Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.ListAgreementNo.Clear()
            If Not IsNothing(Me.chkDistributors.CheckedValues()) Then
                If Me.chkDistributors.CheckedValues.Length > 0 Then
                    For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
                        If Not ListAgreementNo.Contains(Me.chkDistributors.CheckedValues.GetValue(i).ToString()) Then
                            ListAgreementNo.Add(Me.chkDistributors.CheckedValues.GetValue(i).ToString())
                        End If
                    Next
                End If
            End If
            'check apakah discount sebelumnya sudah di generate / belum
            'bila belum , protect untuk di generate
            ' bila sudah tetapi belum di apply, harus di apply dulu
            Dim FlagBefore As String = ""
            Dim Flag As String = Me.GetFlag(Me.cmbFlag.Text)
            Select Case Flag
                'Case "Q1"
                'Case "S1"
                Case "Q2" : FlagBefore = "Q1"
                Case "Q3" : FlagBefore = "Q2"
                Case "Q4" : FlagBefore = "Q3"
                Case "S2" : FlagBefore = "S1"
                Case "Y" : FlagBefore = "S2/Q4"
            End Select
            Me.btnApplyDiscByVolume.Enabled = True
            Me.btnApplyDiscByValue.Enabled = True
            Me.btnRecomputeByVolume.Enabled = True
            Me.btnRecomputeByValue.Enabled = True

            If FlagBefore <> "" Then
                If Not Me.clsTA.hasComputedDPD(ListAgreementNo, IIf(Flag <> "Y", FlagBefore, ""), True) Then
                    Me.ShowMessageInfo("Please compute Flag " & FlagBefore & vbCrLf & "Before computing " & Flag)
                    Me.isLoadingCombo = True
                    Me.ListAgreementNo.Clear() : Me.chkDistributors.UncheckAll() : Me.chkDistributors.Text = ""
                    Me.LAF = LoadAchievementFrom.None
                    Me.isLoadingCombo = False : Cursor = Cursors.Default : Return
                End If
                Dim ListFlag As New List(Of String)
                Select Case Flag
                    'Case "Q1"
                    'Case "S1"
                    Case "Q2" : ListFlag.Add("Q1")
                    Case "Q3" : ListFlag.AddRange(New String() {"Q1", "Q2"})
                    Case "Q4" : ListFlag.AddRange(New String() {"Q1", "Q2", "Q3"})
                    Case "S2" : ListFlag.Add("S1")
                    Case "Y" : ListFlag.AddRange(New String() {"S1", "S2", "Q1", "Q2", "Q3", "Q4"})
                End Select
                Dim lastAgreeAch As String = "", IsHasComputedDPD As Boolean = False
                If ListFlag.Count > 0 Then
                    Me.btnApplyDiscByVolume.Enabled = True
                    Me.btnApplyDiscByValue.Enabled = True
                    Me.btnRecomputeByVolume.Enabled = True
                    Me.btnRecomputeByValue.Enabled = True

                    If Me.clsTA.HasAppliedDisc(ListAgreementNo, ListFlag, lastAgreeAch, IsHasComputedDPD, False) Then
                        If lastAgreeAch = "VOL" Then
                            Me.btnApplyDiscByValue.Enabled = False
                            Me.btnRecomputeByValue.Enabled = False
                        ElseIf lastAgreeAch = "VAL" Then
                            Me.btnApplyDiscByVolume.Enabled = False
                            Me.btnRecomputeByVolume.Enabled = False
                        Else
                            Me.ShowMessageError("Unknown error" & vbCrLf & "Can not find type of Achieved DPD")
                            Me.isLoadingCombo = True
                            Me.ListAgreementNo.Clear() : Me.chkDistributors.UncheckAll() : Me.chkDistributors.Text = ""
                            Me.btnApplyDiscByVolume.Enabled = False
                            Me.btnApplyDiscByValue.Enabled = False
                            Me.btnRecomputeByVolume.Enabled = False
                            Me.btnRecomputeByValue.Enabled = False
                            Me.LAF = LoadAchievementFrom.None
                            Me.isLoadingCombo = False : Cursor = Cursors.Default
                            Return
                        End If
                    Else
                        If IsHasComputedDPD Then
                            MessageBox.Show("Previous Semester/Quarter hasn't been applied yet ", "Please apply previous discount type before computing " & Flag, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Me.isLoadingCombo = True
                            Me.ListAgreementNo.Clear() : Me.chkDistributors.UncheckAll() : Me.chkDistributors.Text = ""
                            Me.btnApplyDiscByVolume.Enabled = False
                            Me.btnApplyDiscByValue.Enabled = False
                            Me.btnRecomputeByVolume.Enabled = False
                            Me.btnRecomputeByValue.Enabled = False
                            Me.LAF = LoadAchievementFrom.None
                            Me.isLoadingCombo = False : Cursor = Cursors.Default
                            Return
                        End If
                    End If
                End If
            End If
            Me.checkEnabledFlagValue(True)
            Dim StartDate As Date = chkDistributors.DropDownList.GetValue("START_DATE")
            Dim EndDate As Date = chkDistributors.DropDownList.GetValue("END_DATE")
            If StartDate >= New Date(2019, 8, 1) And EndDate <= New Date(2020, 7, 31) Then
                Me.btnQuarter4.Visible = False
                Me.btnQuarter4V.Visible = False
            End If
            'reset AgreeAchivedBy
            Me.AgreeAchBy = ""
            Me.LAF = LoadAchievementFrom.None
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub cxmnComputeByVolume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cxmnComputeByVolume.Click

        If Me.GridEX1.DataSource Is Nothing Then : Return : End If
        If Me.GridEX1.RecordCount <= 0 Then : Return : End If
        Me.Cursor = Cursors.WaitCursor
        Me.LAF = LoadAchievementFrom.Volume
        Me.ShowValueInfo(False)
        Me.GridEX1.MoveFirst()
        If Not String.IsNullOrEmpty(Me.GridEX1.GetValue("AGREE_ACH_BY").ToString()) Then
            If Me.GridEX1.GetValue("AGREE_ACH_BY").ToString() = "VOLUME" Then
                Me.ShowVolumeInfo(True)
                Me.GridEX1.RootTable.Columns("DISC_QTY").Visible = False
                Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = True
                Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = True
                Me.GridEX1.RootTable.Columns("DISC_DIST_BY_VOLUME").Visible = False
                Me.GridEX2.RootTable.Columns("DISC_BY_VOLUME").Visible = True
                Me.GridEX2.RootTable.Columns("RELEASE_QTY").Visible = True
                Me.GridEX2.RootTable.Columns("LEFT_QTY").Visible = True
                Me.GridEX2.RootTable.Columns("REMAIND_QTY").Visible = True
            Else
                Me.ShowVolumeInfo(True)
                Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = False
                Me.GridEX1.RootTable.Columns("DISC_QTY").Visible = False
                Me.GridEX1.RootTable.Columns("DISC_BY_VOLUME").Visible = True
                Me.GridEX2.RootTable.Columns("DISC_QTY").Visible = False
                Me.GridEX2.RootTable.Columns("RELEASE_QTY").Visible = False
                Me.GridEX2.RootTable.Columns("LEFT_QTY").Visible = False
                Me.GridEX2.RootTable.Columns("REMAIND_QTY").Visible = False
            End If
        Else
            Me.ShowVolumeInfo(True)
            Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = False
            Me.GridEX1.RootTable.Columns("DISC_QTY").Visible = False
            Me.GridEX1.RootTable.Columns("DISC_BY_VOLUME").Visible = True
            Me.GridEX2.RootTable.Columns("DISC_QTY").Visible = False
            Me.GridEX2.RootTable.Columns("RELEASE_QTY").Visible = False
            Me.GridEX2.RootTable.Columns("LEFT_QTY").Visible = False
            Me.GridEX2.RootTable.Columns("REMAIND_QTY").Visible = False
        End If
        Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").Visible = False
        Me.GridEX1.RootTable.Columns("ACTUAL").Visible = False
        Me.GridEX1.RootTable.Columns("ACTUAL_DISTRIBUTOR").Visible = False
        Me.GridEX1.RootTable.Columns("ACTUAL_DIST_VALUE").Visible = False
        Dim colPOValue As Janus.Windows.GridEX.GridEXColumn = Me.GridEX1.RootTable.Columns("TOTAL_PO_VALUE")
        colPOValue.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.None
        Dim colTargetValue As Janus.Windows.GridEX.GridEXColumn = Me.GridEX1.RootTable.Columns("TARGET_BY_VALUE")
        colTargetValue.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.None
        Dim colActualValue As Janus.Windows.GridEX.GridEXColumn = Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE")
        colActualValue.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.None
        Me.GridEX1.RootTable.Columns("ACH_DISPRO_BY_VALUE").TotalFormatString = ""

        Me.ctmnComputeByValue.Enabled = ((Me.GridEX1.RecordCount > 0 And Me.chkDistributors.CheckedValues.Length > 0) _
                                          And (Me.LAF = LoadAchievementFrom.Volume Or Me.LAF = LoadAchievementFrom.VolumeWithApplyDisc Or Me.LAF = LoadAchievementFrom.None))

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ctmnComputeByValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ctmnComputeByValue.Click

        Try

            If IsNothing(Me.GridEX1.DataSource) Then : Return : End If
            If Me.GridEX1.RecordCount <= 0 Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Dim ListAgreementNo As New List(Of String)
            For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
                If Not ListAgreementNo.Contains(Me.chkDistributors.CheckedValues.GetValue(i).ToString()) Then
                    ListAgreementNo.Add(Me.chkDistributors.CheckedValues.GetValue(i).ToString())
                End If
            Next

            If Not Me.clsTA.hasComputedValue(ListAgreementNo, Me.GetFlag(Me.cmbFlag.Text), True) Then
                Me.ShowMessageInfo("can not view Achievement, please compute achievement by value")
                Me.Cursor = Cursors.Default : Return
            End If
            Me.LAF = LoadAchievementFrom.Value
            Me.ShowVolumeInfo(False)
            Dim HasCombinedTarget As Boolean = False
            Dim HasSharingTarget As Boolean = False
            If Not IsNothing(Me.DS) Then
                HasCombinedTarget = Me.DS.Tables("ACHIEVEMENT_HEADER").Select("ISCOMBINED_TARGET = " & True).Length > 0
                HasSharingTarget = Me.DS.Tables("ACHIEVEMENT_HEADER").Select("T_GROUP = 'YESS'").Length > 0
            End If
            Me.GridEX1.MoveFirst()
            If Not String.IsNullOrEmpty(Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").ToString()) Then
                If Me.GridEX1.GetValue("AGREE_ACH_BY").ToString() = "VALUE" Then
                    Me.ShowValueInfo(True)

                    Me.GridEX1.RootTable.Columns("DISC_QTY").Visible = True
                    Me.GridEX1.RootTable.Columns("DISC_DIST_BY_VALUE").Visible = False
                    Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = True
                    Me.GridEX1.RootTable.Columns("DISC_BY_VALUE").Visible = False

                    Me.GridEX2.RootTable.Columns("DISC_BY_VALUE").Visible = True
                    Me.GridEX2.RootTable.Columns("RELEASE_QTY").Visible = True
                    Me.GridEX2.RootTable.Columns("LEFT_QTY").Visible = True
                    Me.GridEX2.RootTable.Columns("REMAIND_QTY").Visible = True

                Else
                    Me.ShowValueInfo(True)
                    Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = False
                    Me.GridEX1.RootTable.Columns("DISC_BY_VALUE").Visible = True
                    Me.GridEX1.RootTable.Columns("DISC_QTY").Visible = False

                    Me.GridEX2.RootTable.Columns("DISC_QTY").Visible = False
                    Me.GridEX2.RootTable.Columns("DISC_BY_VALUE").Visible = False
                    Me.GridEX2.RootTable.Columns("RELEASE_QTY").Visible = True
                    Me.GridEX2.RootTable.Columns("LEFT_QTY").Visible = True
                    Me.GridEX2.RootTable.Columns("REMAIND_QTY").Visible = True
                End If
            Else
                Me.ShowValueInfo(True)
                Me.GridEX1.RootTable.Columns("AGREE_ACH_BY").Visible = False

                Me.GridEX1.RootTable.Columns("DISC_BY_VALUE").Visible = True

                Me.GridEX2.RootTable.Columns("DISC_BY_VALUE").Visible = True
                Me.GridEX2.RootTable.Columns("DISC_QTY").Visible = False

                Me.GridEX2.RootTable.Columns("RELEASE_QTY").Visible = True
                Me.GridEX2.RootTable.Columns("LEFT_QTY").Visible = True
                Me.GridEX2.RootTable.Columns("REMAIND_QTY").Visible = True
            End If
            If HasCombinedTarget Then
                Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").Visible = False
                Me.GridEX1.RootTable.Columns("ACTUAL").Visible = False
                Me.GridEX1.RootTable.Columns("ACTUAL_DISTRIBUTOR").Visible = True
                Me.GridEX1.RootTable.Columns("ACTUAL_DIST_VALUE").Visible = True
            Else
                Me.GridEX1.RootTable.Columns("ACTUAL_BY_VALUE").Visible = True
                Me.GridEX1.RootTable.Columns("ACTUAL").Visible = True
                Me.GridEX1.RootTable.Columns("ACTUAL_DISTRIBUTOR").Visible = False
                Me.GridEX1.RootTable.Columns("ACTUAL_DIST_VALUE").Visible = False
            End If
            AddConditionalFormating(HasSharingTarget, HasCombinedTarget)
            'Me.ctmnComputeByValue.Enabled = ((Me.GridEX1.RecordCount > 0 And Me.chkDistributors.CheckedValues.Length > 0) _
            '                                         And (Me.LAF = LoadAchievementFrom.Volume Or Me.LAF = LoadAchievementFrom.VolumeWithApplyDisc Or Me.LAF = LoadAchievementFrom.None))
            Me.cxmnComputeByVolume.Enabled = ((Me.GridEX1.RecordCount > 0 And Me.chkDistributors.CheckedValues.Length > 0) _
                                            And (Me.LAF = LoadAchievementFrom.Value Or Me.LAF = LoadAchievementFrom.ValueWithApplyDisc))
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try

    End Sub
End Class