Imports Microsoft.VisualBasic
Public Class DistributorAgreement

#Region " Deklarasi "
    Friend Event CloseThis()
    Private clsDiscAgreement As NufarmBussinesRules.DistributorAgreement.DistrAGreement
    Private Mode As SaveMode
    Private HasLoad As Boolean
    Private DateFromGridEx As Date 'original date in database when gridEx Is Selected ann mode = Update
    Private CanUpdateData As Boolean
    Private WhileSaving As Saving
    Private SFG As StateFillingGrid

    Private OStartDate As Object = Nothing
    Private OEndDate As Object = Nothing
    Private OFlag As String = ""
    Private DVSemesterly As DataView = Nothing
    Private DVSemesterlyV As DataView = Nothing
    Private DVQuarterly As DataView = Nothing
    Private DVQuarterlyV As DataView = Nothing
    Private DVYearly As DataView = Nothing
    Private DVYearlyV As DataView = Nothing
    Public CMain As Main = Nothing
#End Region

#Region " Enum "
    Private Enum SaveMode
        Save
        Update
    End Enum
    Private Enum Saving
        Waiting
        Failed
        Succes
    End Enum
    Private Enum StateFillingGrid
        Filling
        HasFilled
    End Enum

#End Region

#Region " Sub "
    Friend Sub InitializeData()
        Try
            Me.LoadData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RefreshData()
        Me.LoadData()
        Me.btnAdd_Click(Me.btnAdd, New System.EventArgs())
    End Sub

    Private Sub ClearData(ByVal dgv As DataGridView)
        Select Case dgv.Name
            Case "dgvPeriodic"
                If Me.rdbQuarterly.Checked = True Then
                    Me.clsDiscAgreement.GetTableQuarterly().Clear()
                    Me.clsDiscAgreement.GetTableQuarterly.AcceptChanges()
                    Me.DVQuarterly = Me.clsDiscAgreement.GetTableQuarterly().DefaultView()
                    Me.dgvPeriodic.DataSource = Me.DVQuarterly

                    Me.clsDiscAgreement.getTableQuarterlyV.Clear()
                    Me.clsDiscAgreement.getTableQuarterlyV.AcceptChanges()
                    Me.DVQuarterlyV = Me.clsDiscAgreement.getTableQuarterlyV().DefaultView()
                    Me.dgvPeriodicVal.DataSource = Me.DVQuarterlyV

                ElseIf Me.rdbSemeterly.Checked = True Then
                    Me.clsDiscAgreement.GetTableSemesterly().Clear()
                    Me.clsDiscAgreement.GetTableSemesterly.AcceptChanges()
                    Me.DVSemesterly = Me.clsDiscAgreement.GetTableSemesterly().DefaultView()
                    Me.dgvPeriodic.DataSource = Me.DVSemesterly

                    Me.clsDiscAgreement.getTableSemesterlyV.Clear()
                    Me.clsDiscAgreement.getTableSemesterlyV.AcceptChanges()
                    Me.DVSemesterlyV = Me.clsDiscAgreement.getTableSemesterlyV().DefaultView()
                    Me.dgvPeriodicVal.DataSource = Me.DVSemesterlyV

                ElseIf (Me.rdbQuarterly.Checked = False) And (Me.rdbQuarterly.Checked = False) Then
                    If Not IsNothing(Me.clsDiscAgreement.GetTableQuarterly()) Then
                        Me.clsDiscAgreement.GetTableQuarterly().Clear()
                        Me.clsDiscAgreement.GetTableQuarterly.AcceptChanges()
                        Me.DVQuarterly = Me.clsDiscAgreement.GetTableQuarterly().DefaultView()
                        Me.dgvPeriodic.DataSource = Me.DVQuarterly
                        Me.tbPeriode.Text = "Quarterly Discount"
                    ElseIf Not IsNothing(Me.clsDiscAgreement.GetTableSemesterly()) Then
                        Me.clsDiscAgreement.GetTableSemesterly().Clear()
                        Me.clsDiscAgreement.GetTableSemesterly.AcceptChanges()
                        Me.DVSemesterly = Me.clsDiscAgreement.GetTableSemesterly().DefaultView()
                        Me.dgvPeriodic.DataSource = Me.DVSemesterly
                        Me.tbPeriode.Text = "Semesterly Discount"
                    End If
                    If Not IsNothing(Me.clsDiscAgreement.getTableQuarterlyV()) Then
                        Me.clsDiscAgreement.getTableQuarterlyV().Clear()
                        Me.clsDiscAgreement.getTableQuarterlyV.AcceptChanges()
                        Me.DVQuarterlyV = Me.clsDiscAgreement.getTableQuarterlyV().DefaultView()
                        Me.dgvPeriodicVal.DataSource = Me.DVQuarterlyV
                        Me.tbPeriodicVal.Text = "Quarterly Discount"
                    ElseIf Not IsNothing(Me.clsDiscAgreement.getTableSemesterlyV()) Then
                        Me.clsDiscAgreement.getTableSemesterlyV().Clear()
                        Me.clsDiscAgreement.getTableSemesterlyV.AcceptChanges()
                        Me.DVSemesterlyV = Me.clsDiscAgreement.getTableSemesterlyV().DefaultView()
                        Me.dgvPeriodicVal.DataSource = Me.DVSemesterlyV
                        Me.tbPeriodicVal.Text = "Semesterly Discount"
                    End If
                End If
            Case "dgvYearly"
                If Not IsNothing(Me.clsDiscAgreement.GetTableYearly()) Then
                    Me.clsDiscAgreement.GetTableYearly().Clear()
                    Me.clsDiscAgreement.GetTableYearly.AcceptChanges()
                    Me.DVYearly = Me.clsDiscAgreement.GetTableYearly().DefaultView()
                    Me.dgvYearly.DataSource = Me.DVYearly
                End If
                If Not IsNothing(Me.clsDiscAgreement.getTableYearlyV) Then
                    Me.clsDiscAgreement.getTableYearlyV.Clear()
                    Me.clsDiscAgreement.getTableYearlyV.AcceptChanges()
                    Me.DVYearlyV = Me.clsDiscAgreement.getTableYearlyV().DefaultView()
                    Me.dgvYearlyVal.DataSource = Me.DVYearlyV
                End If
        End Select
    End Sub

    'PROCEDURE UNTUK MENGISI DATAGRID DENGAN VALUE AGREEMENT_NO DAN QS_FLAG BILA AGREEMENT_NO DI CHANGED
    'DAN BILA DATA DI UPDATE
    Private Sub FillHideColumnWithDefaultValue(ByVal dgv As DataGridView)
        Select Case dgv.Name
            Case "dgvPeriodic"
                If Me.rdbSemeterly.Checked = True Then
                    For i As Integer = 0 To Me.DVSemesterly.Count - 1
                        DVSemesterly(i).BeginEdit()
                        DVSemesterly(i)("AGREEMENT_NO") = Me.txtAgreementNumber.Text
                        DVSemesterly(i)("QSY_DISC_FLAG") = "S"
                        DVSemesterly(i).EndEdit()
                    Next
                    For i As Integer = 0 To Me.DVSemesterlyV.Count - 1
                        Me.DVSemesterlyV(i).BeginEdit()
                        Me.DVSemesterly(i)("AGREEMENT_NO") = Me.txtAgreementNumber.Text
                        Me.DVSemesterly(i)("QSY_DISC_FLAG") = "S"
                        Me.DVSemesterly(i).EndEdit()
                    Next
                    ''NGESUK maning
                ElseIf Me.rdbQuarterly.Checked = True Then
                    For i As Integer = 0 To Me.DVQuarterly.Count - 1
                        Me.DVQuarterly(i).BeginEdit()
                        Me.DVQuarterly(i)("AGREEMENT_NO") = Me.txtAgreementNumber.Text
                        Me.DVQuarterly(i)("QSY_DISC_FLAG") = "Q"
                        Me.DVQuarterly(i).EndEdit()
                    Next
                    For i As Integer = 0 To Me.DVQuarterlyV.Count - 1
                        Me.DVQuarterlyV(i).BeginEdit()
                        Me.DVQuarterlyV(i)("AGREEMENT_NO") = Me.txtAgreementNumber.Text
                        Me.DVQuarterlyV(i)("QSY_DISC_FLAG") = "Q"
                        Me.DVQuarterlyV(i).EndEdit()
                    Next
                Else
                    If Not IsNothing(Me.clsDiscAgreement.GetDataSetPeriod()) Then
                        If Me.clsDiscAgreement.GetDataSetPeriod().HasChanges() Then
                            Me.clsDiscAgreement.GetDataSetPeriod().RejectChanges()
                            'Me.dgvPeriodic.DataSource = Me.clsDiscAgreement.GetTableQuarterly() 'default datasource
                            Me.DVYearly = Me.clsDiscAgreement.GetTableYearly().DefaultView()
                            Me.dgvYearly.DataSource = Me.DVYearly
                            Me.DVYearlyV = Me.clsDiscAgreement.getTableYearlyV().DefaultView()
                            Me.dgvYearlyVal.DataSource = Me.DVYearlyV
                        End If
                    End If
                End If
            Case "dgvYearly"
                For i As Integer = 0 To Me.DVYearly.Count - 1
                    Me.DVYearly(i).BeginEdit()
                    Me.DVYearly(i)("AGREEMENT_NO") = Me.txtAgreementNumber.Text
                    Me.DVYearly(i)("QSY_DISC_FLAG") = "Y"
                    Me.DVYearly(i).EndEdit()
                Next
                For i As Integer = 0 To Me.DVYearlyV.Count - 1
                    Me.DVYearlyV(i).BeginEdit()
                    Me.DVYearlyV(i)("AGREEMENT_NO") = Me.txtAgreementNumber.Text
                    Me.DVYearlyV(i)("QSY_DISC_FLAG") = "Y"
                    Me.DVYearlyV(i).EndEdit()
                Next
        End Select
    End Sub

    Private Sub UnabledControl()
        Me.txtAgreementNumber.Enabled = False
        Me.txtAgrementDescription.Enabled = False
        'Me.txtTargetYear.Enabled = False
        Me.rdbSemeterly.Enabled = False
        Me.rdbQuarterly.Enabled = False
        Me.rdbFMP.Enabled = False
        Me.dgvPeriodic.Enabled = False
        Me.dgvYearly.Enabled = False
        Me.dtPicStart.Enabled = False
        Me.dtPicEnd.Enabled = False
        Me.dgvPeriodicVal.Enabled = False
        Me.dgvYearlyVal.Enabled = False
        Me.CanUpdateData = False
    End Sub

    Private Sub EnabledControl()
        Me.txtAgreementNumber.Enabled = True
        Me.txtAgrementDescription.Enabled = True
        'Me.txtTargetYear.Enabled = True
        Me.rdbSemeterly.Enabled = True
        Me.rdbQuarterly.Enabled = True
        Me.rdbFMP.Enabled = True
        Me.dgvPeriodic.Enabled = True
        Me.dgvYearly.Enabled = True
        Me.dtPicStart.Enabled = True
        Me.dtPicEnd.Enabled = True
        Me.dgvPeriodicVal.Enabled = True
        Me.dgvYearlyVal.Enabled = True

        Me.CanUpdateData = True
    End Sub

    Private Sub LoadData()
        If IsNothing(Me.clsDiscAgreement) Then
            Me.clsDiscAgreement = New NufarmBussinesRules.DistributorAgreement.DistrAGreement
        End If
        Me.clsDiscAgreement.GetData("By Range Date") : Me.BindGrid("")
        Me.BindMulticolumnCombo(Me.clsDiscAgreement.ViewDistributor, "")
        Me.BINDcheckedCombo(Me.clsDiscAgreement.ViewDistributor(), "")
        Dim EndProg = New DateTime(2019, 7, 31)
        If DateTime.Now > EndProg Then
            Me.tbProgressive.Visible = False
        End If
    End Sub

    Private Overloads Sub ClearControl()
        Me.rdbQuarterly.Checked = False
        Me.rdbSemeterly.Checked = False
        Me.rdbFMP.Checked = False
        Me.dtPicStart.Value = CDate(NufarmBussinesRules.SharedClass.ServerDate)
        Me.dtPicEnd.Value = CDate(DateSerial(Me.dtPicStart.Value.Year, Me.dtPicStart.Value.Month + 12, Me.dtPicStart.Value.Day - 1))
        'Me.txtTargetYear.Text = ""
        Me.txtAgreementNumber.Text = ""
        Me.txtAgrementDescription.Text = ""
        Me.ListView1.Items.Clear()
        Me.MultiColumnCombo1.Value = Nothing
        Me.chkAgreementGroup.Checked = False
        Me.chkComboDistributor.UncheckAll() : Me.chkComboDistributor.Text = ""
    End Sub

    Private Sub BindGrid(ByVal rowFilter As String)
        Me.SFG = StateFillingGrid.Filling
        Me.clsDiscAgreement.ViewDistAgreement.RowFilter = rowFilter
        Me.GridEX1.SetDataBinding(Me.clsDiscAgreement.ViewDistAgreement(), "")
        Me.GridEX1.RootTable.Columns("TERRITORY_AREA").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        'Me.GridEX1.RootTable.Columns("MANAGER").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("AGREEMENT_NO").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("AGREEMENT_DESC").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("START_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.GridEX1.RootTable.Columns("END_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        'Me.GridEX1.RootTable.Columns("TARGET_YEAR").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        'Me.GridEX1.RetrieveStructure(True)
        Me.AddConditionalFormating()
        Me.SFG = StateFillingGrid.HasFilled
    End Sub
    Private Sub BINDcheckedCombo(ByVal dtview As DataView, ByVal rowfilter As String)
        dtview.RowFilter = rowfilter
        Me.chkComboDistributor.SetDataBinding(dtview, "")
        'Me.chkComboDistributor.DropDownDataSource = dtview
        'Me.chkComboDistributor.RetrieveStructure()
        'Me.chkComboDistributor.DropDownDisplayMember = "DISTRIBUTOR_NAME"
        'Me.chkComboDistributor.DropDownValueMember = "DISTRIBUTOR_ID"
        'Me.chkComboDistributor.DroppedDown = True
        For Each COL As Janus.Windows.GridEX.GridEXColumn In Me.chkComboDistributor.DropDownList.Columns
            COL.AutoSize()
        Next
        'Me.chkComboDistributor.DroppedDown = False
    End Sub

    Private Sub BindMulticolumnCombo(ByVal dtview As DataView, ByVal rowFilter As String)
        dtview.RowFilter = rowFilter
        'Me.MultiColumnCombo1.SetDataBinding(dtview, "")
        Me.MultiColumnCombo1.DataSource = dtview
        Me.MultiColumnCombo1.DropDownList.RetrieveStructure()
        Me.MultiColumnCombo1.DisplayMember = "DISTRIBUTOR_NAME"
        Me.MultiColumnCombo1.ValueMember = "DISTRIBUTOR_ID"
        Me.MultiColumnCombo1.DroppedDown = True
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.MultiColumnCombo1.DropDownList.Columns
            col.AutoSize()
        Next
        Me.MultiColumnCombo1.DroppedDown = False
    End Sub

    Private Sub AddConditionalFormating()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.GridEX1.RootTable.Columns("END_DATE"), Janus.Windows.GridEX.ConditionOperator.LessThan, CObj(NufarmBussinesRules.SharedClass.ServerDate()))
        fc.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
        fc.FormatStyle.ForeColor = SystemColors.GrayText
        GridEX1.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DistributorAgreement = True Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If (NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DistributorAgreement = True) And (NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DistributorAgreement = True) Then
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DistributorAgreement = True Then
                Me.btnAdd.Enabled = True
                Me.btnSave.Enabled = True
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DistributorAgreement = True Then
                Me.btnAdd.Enabled = False
                Me.btnSave.Enabled = True
            Else
                Me.btnAdd.Enabled = False
                Me.btnSave.Enabled = False
            End If
        End If
    End Sub

    Private Sub BuildPeriode(ByVal Flag As String)
        'get start


        Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                      StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                      StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                      StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing, _
                      StartDateF1 As DateTime = Nothing, EndDateF1 As DateTime = Nothing, StartDateF2 As DateTime = Nothing, EndDateF2 As DateTime = Nothing, _
                      StartDateF3 As DateTime = Nothing, EndDateF3 As DateTime = Nothing

        StartDate = Convert.ToDateTime(Me.dtPicStart.Value.ToShortDateString())
        EndDate = Convert.ToDateTime(Me.dtPicEnd.Value.ToShortDateString())

        If (OStartDate = StartDate) And (OEndDate = EndDate) And (Flag = Me.OFlag) Then 'untuk mengecek agar tidak redrraw listview
        Else
            Me.ListView1.Items.Clear()
            Dim TotalDays As Long = DateDiff(DateInterval.Day, StartDate, EndDate)
            StartDateQ1 = StartDate : StartDateS1 = StartDate
            EndDateQ4 = EndDate : EndDateS2 = EndDate
            If Flag = "Q" Then
                EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
                StartDateQ2 = EndDateQ1.AddDays(1)
                EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
                StartDateQ3 = EndDateQ2.AddDays(1)
                EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
                StartDateQ4 = EndDateQ3.AddDays(1)
                EndDateQ4 = EndDate
            ElseIf Flag = "S" Then
                EndDateS1 = StartDateS1.AddMonths(6).AddDays(-1)
                StartDateS2 = EndDateS1.AddDays(1)
                EndDateS2 = EndDate
            ElseIf Flag = "F" Then
                StartDateF1 = StartDate
                EndDateF1 = StartDateF1.AddMonths(4).AddDays(-1)
                StartDateF2 = EndDateF1.AddDays(1)
                EndDateF2 = StartDateF2.AddMonths(4).AddDays(-1)
                StartDateF3.AddDays(1)
                EndDateF3 = EndDate
            End If
            'simpan di memory agar tidak redraw listview
            OStartDate = StartDate
            OEndDate = EndDate
            OFlag = Flag

            Select Case Flag.ToUpper()
                Case "Q"
                    Dim LVItemQ1 As New ListViewItem("Q1")
                    Dim LVItemQ2 As New ListViewItem("Q2")
                    Dim LVItemQ3 As New ListViewItem("Q3")
                    Dim LVItemQ4 As New ListViewItem("Q4")
                    With Me.ListView1
                        With LVItemQ1
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", StartDateQ1))
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", EndDateQ1))
                        End With
                        .Items.Add(LVItemQ1)
                        With LVItemQ2
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", StartDateQ2))
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", EndDateQ2))
                        End With
                        .Items.Add(LVItemQ2)
                        With LVItemQ3
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", StartDateQ3))
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", EndDateQ3))
                        End With
                        .Items.Add(LVItemQ3)
                        With LVItemQ4
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", StartDateQ4))
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", EndDateQ4))
                        End With
                        .Items.Add(LVItemQ4)
                    End With
                Case "S"
                    Dim LVItemS1 As New ListViewItem("S1")
                    Dim LVItemS2 As New ListViewItem("S2")
                    With Me.ListView1
                        With LVItemS1
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", StartDateS1))
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", EndDateS1))
                        End With
                        .Items.Add(LVItemS1)
                        With LVItemS2
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", StartDateS2))
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", EndDateS2))
                        End With
                        .Items.Add(LVItemS2)
                    End With
                Case "F"
                    Dim LVItemF1 As New ListViewItem("F1"), LVItemF2 As New ListViewItem("F2"), LVItemF3 As New ListViewItem("F3")
                    With Me.ListView1
                        With LVItemF1
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", StartDateF1))
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", EndDateF1))
                        End With
                        .Items.Add(LVItemF1)
                        With LVItemF2
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", StartDateF2))
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", EndDateF2))
                        End With
                        .Items.Add(LVItemF2)
                        With LVItemF3
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", StartDateF3))
                            .SubItems.Add(String.Format("{0:dd MMMM yyyy}", EndDateF3))
                        End With
                        .Items.Add(LVItemF3)
                    End With
            End Select
        End If
    End Sub

#End Region

#Region " Function "
    Private Function IsValid() As Boolean
        If Me.txtAgreementNumber.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.txtAgreementNumber, "AGREEMENT NUMBER is NULL." & vbCrLf & "Please defined Agreement number.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtAgreementNumber), Me.txtAgreementNumber, 2000)
            Me.txtAgreementNumber.Focus()
            Return False
        End If
        If Me.chkAgreementGroup.Checked = True Then
            If Me.Mode = SaveMode.Update Then
                If Me.btnAddDistributor.Text = "&Cancel" Then
                    If Me.chkComboDistributor.CheckedValues.Length <= 0 Then
                        Me.baseTooltip.Show("Please define Distributor.", Me.chkComboDistributor, 2500)
                        Me.chkComboDistributor.DroppedDown = True
                        Return False
                    End If
                End If
            Else
                If Me.chkComboDistributor.CheckedValues.Length <= 0 Then
                    Me.baseTooltip.Show("Please define Distributor.", Me.chkComboDistributor, 2500)
                    Me.chkComboDistributor.DroppedDown = True
                    Return False
                End If
            End If

        ElseIf (Me.MultiColumnCombo1.Text = "") Or (Me.MultiColumnCombo1.SelectedIndex = -1) Then
            Me.baseTooltip.SetToolTip(Me.MultiColumnCombo1, "Distributor is NULL!." & vbCrLf & "Distributor must be suplied")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.MultiColumnCombo1), Me.MultiColumnCombo1, 2000)
            Me.MultiColumnCombo1.DroppedDown = True
            Return False
        End If
        If (Me.rdbQuarterly.Checked = False) And (Me.rdbSemeterly.Checked = False) And (Me.rdbFMP.Enabled = False) Then
            Me.baseTooltip.SetToolTip(Me.grpPeriod, "Flag is NULL !." & vbCrLf & "Please Defined Period treatment.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.grpPeriod), Me.grpPeriod, 2000)
            Return False
        End If
        If Me.txtAgrementDescription.Text.Trim = "" Then
            Me.baseTooltip.SetToolTip(Me.grpPeriod, "Agrement description is null !." & vbCrLf & "Please suply Agreement Description.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtAgrementDescription), Me.txtAgrementDescription, 2000)
        End If
        'If Not IsNothing(Me.clsDiscAgreement.GetDataSetPeriod()) Then
        '    If Me.clsDiscAgreement.GetDataSetPeriod.HasChanges() Then
        '        If Me.rdbQuarterly.Checked = True Then
        '            For i As Integer = 0 To Me.DVQuarterly.Count - 1
        '                If (Me.DVQuarterly(i)("UP_TO_PCT") Is DBNull.Value) Or (Me.DVQuarterly(i)("PRGSV_DISC_PCT") Is DBNull.Value) Then
        '                    Me.baseTooltip.SetToolTip(Me.dgvPeriodic, "Invalid / Null value." & vbCrLf & "Some column has an invalid / null value")
        '                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dgvPeriodic), Me.dgvPeriodic, 2000)
        '                    Me.tbProgressive.SelectedIndex = 0
        '                    Me.tbPeriodic.SelectedIndex = 0
        '                    Return False
        '                End If
        '            Next
        '            For i As Integer = 0 To Me.DVQuarterly.Count - 1
        '                If (Me.DVQuarterlyV(i)("UP_TO_PCT") Is DBNull.Value) Or (Me.DVQuarterlyV(i)("PRGSV_DISC_PCT") Is DBNull.Value) Then
        '                    Me.baseTooltip.SetToolTip(Me.dgvPeriodicVal, "Invalid / Null value." & vbCrLf & "Some column has an invalid / null value")
        '                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dgvPeriodicVal), Me.dgvPeriodicVal, 2000)
        '                    Me.tbProgressive.SelectedIndex = 1
        '                    Me.tbPeriodicVal.SelectedIndex = 0
        '                    Return False
        '                End If
        '                If Me.DVQuarterlyV(i)("QSY_DISC_FLAG") Is DBNull.Value Then
        '                    Me.baseTooltip.Show("Please enter flag within(Q1/Q2/Q3/Q4)", Me.dgvPeriodicVal, 2500)
        '                    Me.dgvPeriodicVal.Focus()
        '                    Me.tbProgressive.SelectedIndex = 1
        '                    Me.tbPeriodicVal.SelectedIndex = 0
        '                    Return False
        '                ElseIf Me.DVQuarterlyV(i)("QSY_DISC_FLAG") = "" Then
        '                    Me.baseTooltip.Show("Please enter flag within(Q1/Q2/Q3/Q4)", Me.dgvPeriodicVal, 2500)
        '                    Me.dgvPeriodicVal.Focus()
        '                    Me.tbProgressive.SelectedIndex = 1
        '                    Me.tbPeriodicVal.SelectedIndex = 0
        '                    Return False
        '                ElseIf Me.DVQuarterlyV(i)("QSY_DISC_FLAG") = "S" Or Me.DVQuarterly(i)("QSY_DISC_FLAG") = "Q" Then
        '                    Me.baseTooltip.Show("Please enter flag within(Q1/Q2/Q3/Q4)", Me.dgvPeriodicVal, 2500)
        '                    Me.dgvPeriodicVal.Focus()
        '                    Me.tbProgressive.SelectedIndex = 1
        '                    Me.tbPeriodicVal.SelectedIndex = 0
        '                    Return False
        '                End If
        '            Next
        '        ElseIf Me.rdbSemeterly.Checked = True Then
        '            For i As Integer = 0 To Me.DVSemesterly.Count - 1
        '                If (Me.DVSemesterly(i)("UP_TO_PCT") Is DBNull.Value) Or (Me.DVSemesterly(i)("PRGSV_DISC_PCT") Is DBNull.Value) Then
        '                    Me.baseTooltip.SetToolTip(Me.dgvPeriodic, "Invalid / Null value." & vbCrLf & "Some column has an invalid / null value")
        '                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dgvPeriodic), Me.dgvPeriodic, 2000)
        '                    Me.tbProgressive.SelectedIndex = 0
        '                    Me.tbPeriodic.SelectedIndex = 0
        '                    Return False
        '                End If
        '            Next
        '            For i As Integer = 0 To Me.DVSemesterlyV.Count - 1
        '                If (Me.DVSemesterlyV(i)("UP_TO_PCT") Is DBNull.Value) Or (Me.DVSemesterlyV(i)("PRGSV_DISC_PCT") Is DBNull.Value) Then
        '                    Me.baseTooltip.SetToolTip(Me.dgvPeriodicVal, "Invalid / Null value." & vbCrLf & "Some column has an invalid / null value")
        '                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dgvPeriodicVal), Me.dgvPeriodicVal, 2000)
        '                    Me.tbProgressive.SelectedIndex = 1
        '                    Me.tbPeriodicVal.SelectedIndex = 0
        '                    Return False
        '                End If
        '                If Me.DVSemesterlyV(i)("QSY_DISC_FLAG") Is DBNull.Value Then
        '                    Me.baseTooltip.Show("Please enter flag within(S1/S2)", Me.dgvPeriodicVal, 2500)
        '                    Me.dgvPeriodicVal.Focus()
        '                    Me.tbProgressive.SelectedIndex = 1
        '                    Me.tbPeriodicVal.SelectedIndex = 0
        '                    Return False
        '                ElseIf Me.DVSemesterlyV(i)("QSY_DISC_FLAG") = "" Then
        '                    Me.baseTooltip.Show("Please enter flag within(S1/S2)", Me.dgvPeriodicVal, 2500)
        '                    Me.dgvPeriodicVal.Focus()
        '                    Me.tbProgressive.SelectedIndex = 1
        '                    Me.tbPeriodicVal.SelectedIndex = 0
        '                    Return False
        '                ElseIf Me.DVSemesterlyV(i)("QSY_DISC_FLAG") = "S" Or Me.DVSemesterlyV(i)("QSY_DISC_FLAG") = "Q" Then
        '                    Me.baseTooltip.Show("Please enter flag within(S1/S2)", Me.dgvPeriodicVal, 2500)
        '                    Me.dgvPeriodicVal.Focus()
        '                    Me.tbProgressive.SelectedIndex = 1
        '                    Me.tbPeriodicVal.SelectedIndex = 0
        '                    Return False
        '                End If
        '            Next
        '        End If
        '        If Me.DVYearly.Count > 0 Then
        '            For i As Integer = 0 To Me.DVYearly.Count - 1
        '                If (Me.DVYearly(i)("UP_TO_PCT") Is DBNull.Value) Or (Me.DVYearly(i)("PRGSV_DISC_PCT") Is DBNull.Value) Then
        '                    Me.baseTooltip.SetToolTip(Me.dgvYearly, "Invalid / Null value." & vbCrLf & "Some column has an invalid / null value")
        '                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dgvYearly), Me.dgvYearly, 2000)
        '                    Me.tbProgressive.SelectedIndex = 0
        '                    Me.tbPeriodic.SelectedIndex = 1
        '                    Return False
        '                End If
        '            Next
        '        End If
        '        If Me.DVYearlyV.Count > 0 Then
        '            For i As Integer = 0 To Me.DVYearlyV.Count - 1
        '                If (Me.DVYearlyV(i)("UP_TO_PCT") Is DBNull.Value) Or (Me.DVYearlyV(i)("PRGSV_DISC_PCT") Is DBNull.Value) Then
        '                    Me.baseTooltip.SetToolTip(Me.dgvYearlyVal, "Invalid / Null value." & vbCrLf & "Some column has an invalid / null value")
        '                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dgvYearlyVal), Me.dgvYearlyVal, 2000)
        '                    Me.tbProgressive.SelectedIndex = 1
        '                    Me.tbPeriodicVal.SelectedIndex = 1
        '                    Return False
        '                End If
        '            Next
        '        End If
        '    End If
        'End If
        Return True
    End Function
#End Region

#Region " Event "

#Region " Event Form "

    Private Sub DistributorAgreement_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsDiscAgreement.Dispose()
            Me.Dispose(True)
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
            RaiseEvent CloseThis()
        End Try
    End Sub

    Private Sub DistributorAgreement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.HasLoad = False
            Me.dtPicStart.Value = CDate(NufarmBussinesRules.SharedClass.ServerDate)
            Me.dtPicEnd.Value = CDate(DateSerial(Me.dtPicStart.Value.Year, Me.dtPicStart.Value.Month + 12, Me.dtPicStart.Value.Day - 1))
            Me.rdbQuarterly.Enabled = False
            Me.rdbSemeterly.Enabled = False
            Me.chkComboDistributor.Visible = False
            Me.MultiColumnCombo1.Visible = True
            Me.Mode = SaveMode.Save
            'AddHandler btnSave.Click, AddressOf Me.Timer1_Tick
            Me.AcceptButton = Me.btnSave
            Me.CancelButton = Me.btnCLose
            Me.chkAgreementGroup.Enabled = True
            Me.ExpandablePanel1.Expanded = False
            Me.dtPicFilterStart.Text = ""
            Me.dtPicFilterEnd.Text = ""
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "DistributorAgreement_Load")
        Finally
            Me.ReadAcces()
            Me.Cursor = Cursors.Default
            Me.HasLoad = True
        End Try
    End Sub

#End Region

#Region " Button "

    Private Sub btnSearch_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.btnClick
        Try
            If Not IsNothing(Me.clsDiscAgreement) Then
                If Me.chkAgreementGroup.Checked = True Then
                    Me.BINDcheckedCombo(Me.clsDiscAgreement.ViewDistributor, "DISTRIBUTOR_NAME LIKE '%" + Me.chkComboDistributor.Text + "%'")
                    Dim itemCount As Integer = Me.chkComboDistributor.DropDownList().RecordCount()
                    Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
                Else
                    Me.BindMulticolumnCombo(Me.clsDiscAgreement.ViewDistributor, "DISTRIBUTOR_NAME LIKE '%" + Me.MultiColumnCombo1.Text + "%'")
                    Dim itemCount As Integer = Me.MultiColumnCombo1.DropDownList().RecordCount()
                    Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnSearch_btnClick")
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Me.Cursor = Cursors.WaitCursor : Me.SFG = StateFillingGrid.Filling
            Me.chkAgreementGroup.Checked = False : Me.txtAgreementNumber.Enabled = True
            Me.txtAgrementDescription.Enabled = True
            'Me.txtTargetYear.Enabled = True
            Me.ClearData(Me.dgvPeriodic)
            Me.ClearData(Me.dgvYearly)
            Me.MultiColumnCombo1.Readonly = False
            'Me.txtTargetYear.Enabled = True
            Me.dtPicEnd.Enabled = True
            Me.dtPicStart.Enabled = True
            Me.chkAgreementGroup.Checked = False
            Me.chkAgreementGroup.Enabled = True
            If Me.HasLoad = True Then
                Me.clsDiscAgreement.GetDataViewDistributor()
                Me.BindMulticolumnCombo(Me.clsDiscAgreement.ViewDistributor(), "")
                Me.MultiColumnCombo1.Value = Nothing
            End If
            Me.ClearControl() : Me.chkComboDistributor.ReadOnly = False
            ''get last agreement enddate
            Dim StartDate As DateTime = clsDiscAgreement.getLastStartDate()
            Me.dtPicFilterStart.Value = StartDate
            Me.btnAddDistributor.Enabled = False
            Me.btnAddDistributor.Text = "&Add"
            Me.Mode = SaveMode.Save
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnAdd_Click")
        Finally
            Me.SFG = StateFillingGrid.HasFilled : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'Me.WhileSaving = Saving.Waiting
            If Me.IsValid = False Then
                Me.Cursor = Cursors.Default
                'Me.WhileSaving = Saving.Failed
                Return
            End If
            If Me.chkAgreementGroup.Checked = False Then
                Me.clsDiscAgreement.DISTRIBUTOR_ID = Me.MultiColumnCombo1.Value
            End If
            Me.clsDiscAgreement.AGREEMENT_NO = Me.txtAgreementNumber.Text
            If Me.rdbQuarterly.Checked = True Then
                Me.clsDiscAgreement.QS_Treatment_Flag = "Q"
            ElseIf Me.rdbSemeterly.Checked = True Then
                Me.clsDiscAgreement.QS_Treatment_Flag = "S"
            ElseIf Me.rdbFMP.CheckedValue Then
                Me.clsDiscAgreement.QS_Treatment_Flag = "F"
            End If
            If Me.txtAgrementDescription.Text = "" Then
                Me.clsDiscAgreement.AgreementDescription = DBNull.Value
            Else
                Me.clsDiscAgreement.AgreementDescription = Me.txtAgrementDescription.Text
            End If
            'Me.clsDiscAgreement.TargetYear = Me.txtTargetYear.Value
            Me.clsDiscAgreement.StartDate = Convert.ToDateTime(Me.dtPicStart.Value.ToShortDateString())
            Me.clsDiscAgreement.EndDate = Convert.ToDateTime(Me.dtPicEnd.Value.ToShortDateString())
            If Me.clsDiscAgreement.GetDataSetPeriod().HasChanges() Then
                Me.clsDiscAgreement.dsProghasChanged = True
            Else
                Me.clsDiscAgreement.dsProghasChanged = False
            End If
            'If Me.Mode = SaveMode.Save Then
            '    If Me.ShowConfirmedMessage(Me.MessageInsertData) = Windows.Forms.DialogResult.No Then
            '        Me.WhileSaving = Saving.Failed
            '        Return
            '    End If
            'ElseIf Me.Mode = SaveMode.Update Then
            '    If Me.ShowConfirmedMessage(Me.MessageUpdateData) = Windows.Forms.DialogResult.No Then
            '        Me.WhileSaving = Saving.Failed
            '        Return
            '    End If
            'End If
            'Me.ShowProgress()
            Select Case Me.Mode
                Case SaveMode.Save
                    If Me.clsDiscAgreement.IsExistAgreementNo(Me.txtAgreementNumber.Text.Trim()) = True Then
                        'Me.WhileSaving = Saving.Failed
                        Me.ShowMessageInfo("AGREEMENT_NO Has already Existed in Data Base" & vbCrLf & "If no AGREEMENT_NO " & _
                        Me.txtAgreementNumber.Text & "In DataGrid." & vbCrLf & "Some User perhaps has entered the Same AGREEMENT_NO" _
                        & vbCrLf & "Please refresh the Datagrid or press F5")
                        Me.Cursor = Cursors.Default
                        Return
                    End If
                    If Me.chkAgreementGroup.Checked = True Then
                        Dim DISTRIBUTOR_IDS As New Collection()
                        Dim x As Integer = 1
                        For i As Integer = 0 To Me.chkComboDistributor.CheckedValues.Length - 1
                            DISTRIBUTOR_IDS.Add(Me.chkComboDistributor.CheckedValues.GetValue(i), x)
                            x += 1
                        Next
                        If Me.clsDiscAgreement.GetDataSetPeriod().HasChanges() Then
                            Me.clsDiscAgreement.SaveAgreementGroup("Save", DISTRIBUTOR_IDS, True, Me.clsDiscAgreement.GetDataSetPeriod().GetChanges())
                        Else
                            Me.clsDiscAgreement.SaveAgreementGroup("Save", DISTRIBUTOR_IDS, True)
                        End If
                    Else
                        If Me.clsDiscAgreement.GetDataSetPeriod().HasChanges() Then
                            Me.clsDiscAgreement.SaveAgreement(Me.clsDiscAgreement.GetDataSetPeriod().GetChanges())
                        Else
                            Me.clsDiscAgreement.SaveAgreement(Nothing)
                        End If
                    End If
                Case SaveMode.Update
                    If Me.chkAgreementGroup.Checked = True Then
                        Dim DISTRIBUTOR_IDS As New Collection()
                        Dim x As Integer = 1
                        If Me.btnAddDistributor.Text = "&Cancel" Then
                            If IsNothing(Me.chkComboDistributor.CheckedValues) Then
                                Me.ShowMessageInfo("You Intent to Add Distributor but don't suply") : Return
                            End If
                            For i As Integer = 0 To Me.chkComboDistributor.CheckedValues.Length - 1
                                DISTRIBUTOR_IDS.Add(Me.chkComboDistributor.CheckedValues.GetValue(i), x)
                                x += 1
                            Next
                        ElseIf Not IsNothing(Me.chkComboDistributor.CheckedValues) Then
                            If Me.chkComboDistributor.CheckedValues.Length > 0 Then
                                For i As Integer = 0 To Me.chkComboDistributor.CheckedValues.Length - 1
                                    DISTRIBUTOR_IDS.Add(Me.chkComboDistributor.CheckedValues.GetValue(i), x)
                                    x += 1
                                Next
                            Else
                                Me.ShowMessageInfo("You intend to add Distributor but don't choose any of them") : Return
                            End If
                        Else
                            Dim dtview As DataView = CType(Me.chkComboDistributor.DropDownDataSource, DataView)
                            For i As Integer = 0 To dtview.Count - 1
                                DISTRIBUTOR_IDS.Add(dtview(i)("DISTRIBUTOR_ID"))
                                x += 1
                            Next
                        End If

                        If Me.clsDiscAgreement.GetDataSetPeriod().HasChanges() Then
                            If Me.btnAddDistributor.Text = "&Cancel" Then
                                Me.clsDiscAgreement.SaveAgreementGroup("Update", DISTRIBUTOR_IDS, True, Me.clsDiscAgreement.GetDataSetPeriod().GetChanges())
                            Else
                                Me.clsDiscAgreement.SaveAgreementGroup("Update", DISTRIBUTOR_IDS, False, Me.clsDiscAgreement.GetDataSetPeriod().GetChanges())
                            End If
                        Else
                            If Me.btnAddDistributor.Text = "&Cancel" Then
                                Me.clsDiscAgreement.SaveAgreementGroup("Update", DISTRIBUTOR_IDS, True)
                            Else
                                Me.clsDiscAgreement.SaveAgreementGroup("Update", DISTRIBUTOR_IDS, False)
                            End If
                        End If
                    Else
                        If Me.clsDiscAgreement.GetDataSetPeriod().HasChanges() Then
                            Me.clsDiscAgreement.UpdateAgreement(Me.clsDiscAgreement.GetDataSetPeriod().GetChanges())
                        Else
                            Me.clsDiscAgreement.UpdateAgreement(Nothing)
                        End If
                    End If
            End Select
            'Me.WhileSaving = Saving.Succes
            Me.SFG = StateFillingGrid.Filling

            If Me.ExpandablePanel1.Expanded Then
                If Me.btnbyAgreementNO.Checked Then
                    Me.clsDiscAgreement.GetData("By Agreement NO", Me.txtSearchAgreement.Text.Trim())
                ElseIf Me.btnbyRangedDate.Checked Then
                    If Me.dtPicFilterStart.Text <> "" And Me.dtPicFilterEnd.Text <> "" Then
                        Me.clsDiscAgreement.GetData("By Range Date", "", Convert.ToDateTime(Me.dtPicFilterStart.Value.ToShortDateString()), _
                     Convert.ToDateTime(Me.dtPicFilterEnd.Value.ToShortDateString()))
                    ElseIf Me.dtPicFilterStart.Text <> "" Then
                        Me.clsDiscAgreement.GetData("By Range Date", "", Convert.ToDateTime(Me.dtPicFilterStart.Value.ToShortDateString()))
                    ElseIf Me.dtPicFilterEnd.Text <> "" Then
                        Me.clsDiscAgreement.GetData("By Range Date", "", Convert.ToDateTime(Me.dtPicFilterEnd.Value.ToShortDateString()))
                    Else : Me.clsDiscAgreement.GetData("By Range Date")
                    End If
                End If
            Else
                Me.clsDiscAgreement.GetData("By Range Date")
            End If
            Me.ClearControl()
            Me.ClearData(Me.dgvPeriodic)
            Me.ClearData(dgvYearly)
            Me.GridEX1.SetDataBinding(Me.clsDiscAgreement.ViewDistAgreement(), "")
            Me.AddConditionalFormating()
            Me.SFG = StateFillingGrid.HasFilled

        Catch ex As Exception
            Me.WhileSaving = Saving.Failed
            Me.SFG = StateFillingGrid.HasFilled
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnSave_Click")
        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnCLose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsDiscAgreement.GetDataSetPeriod()) Then
                If Me.clsDiscAgreement.GetDataSetPeriod.HasChanges() Then
                    Me.clsDiscAgreement.GetDataSetPeriod().RejectChanges()
                End If
            End If
            Me.Close()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnCLose_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnChekExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChekExisting.Click
        Try
            If Me.txtAgreementNumber.Text = "" Then
                Me.ShowMessageInfo("Please Define Agreement no")
                Return
            End If
            If Me.clsDiscAgreement.IsExistAgreementNo(Me.txtAgreementNumber.Text) = True Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
            Else
                Me.ShowMessageInfo(Me.MessageDataSaveToAdd)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAddDistributor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDistributor.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case Me.btnAddDistributor.Text
                Case "&Add"
                    If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        Me.clsDiscAgreement.GetDataViewDistributor(Me.GridEX1.GetValue("AGREEMENT_NO").ToString(), False)
                        Me.BINDcheckedCombo(Me.clsDiscAgreement.ViewDistributor(), "")
                        Me.chkComboDistributor.ReadOnly = False
                        Me.chkComboDistributor.UncheckAll() : Me.chkComboDistributor.Text = ""
                        Me.btnAddDistributor.Text = "&Cancel"
                    End If
                Case "&Cancel"
                    If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        Me.clsDiscAgreement.GetDataViewDistributor(Me.GridEX1.GetValue("AGREEMENT_NO").ToString(), True)
                        Me.BINDcheckedCombo(Me.clsDiscAgreement.ViewDistributor(), "")
                        Me.chkComboDistributor.CheckAll()
                        Me.btnAddDistributor.Text = "&Add"
                    End If
            End Select

        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_btnAddDistributor_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnAplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.btnbyAgreementNO.Checked Then
                Me.clsDiscAgreement.GetData("By Agreement NO", Me.txtSearchAgreement.Text.Trim())
            ElseIf Me.btnbyRangedDate.Checked Then
                If Me.dtPicFilterStart.Text <> "" And Me.dtPicFilterEnd.Text <> "" Then
                    Me.clsDiscAgreement.GetData("By Range Date", "", Convert.ToDateTime(Me.dtPicFilterStart.Value.ToShortDateString()), _
                 Convert.ToDateTime(Me.dtPicFilterEnd.Value.ToShortDateString()))
                ElseIf Me.dtPicFilterStart.Text <> "" Then
                    Me.clsDiscAgreement.GetData("By Range Date", "", Convert.ToDateTime(Me.dtPicFilterStart.Value.ToShortDateString()))
                ElseIf Me.dtPicFilterEnd.Text <> "" Then
                    Me.clsDiscAgreement.GetData("By Range Date", "", Convert.ToDateTime(Me.dtPicFilterEnd.Value.ToShortDateString()))
                Else : Me.clsDiscAgreement.GetData("By Range Date")
                End If
                'If Me.GridEX1.DataSource Is Nothing Or Me.GridEX1.SelectedItems.Count <= 0 Then
                '    Me.HasLoad = False : Me.SFG = StateFillingGrid.Filling : Me.btnAdd_Click(Me.btnAdd, New EventArgs()) : Me.HasLoad = True : Me.SFG = StateFillingGrid.HasFilled
                '    Me.Cursor = Cursors.Default : Return
                'ElseIf Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                '    Me.HasLoad = False : Me.SFG = StateFillingGrid.Filling : Me.btnAdd_Click(Me.btnAdd, New EventArgs()) : Me.HasLoad = True : Me.SFG = StateFillingGrid.HasFilled
                '    Me.Cursor = Cursors.Default : Return
                'End If
            End If
            Me.BindGrid("")
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnAplyRange_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " DateTime Picker "

    Private Sub dtPicStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicStart.ValueChanged
        Try
            Me.dtPicEnd.MinDate = Me.dtPicStart.Value
            'Me.dtPicStart.Value = CDate(NufarmBussinesRules.SharedClass.ServerDate)
            Me.dtPicEnd.Value = CDate(DateSerial(Me.dtPicStart.Value.Year, Me.dtPicStart.Value.Month + 12, Me.dtPicStart.Value.Day - 1))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtPicEnd_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicEnd.ValueChanged
        If Me.SFG = StateFillingGrid.Filling Then
            Return
        End If
        If Me.rdbQuarterly.Checked = False And Me.rdbSemeterly.Checked = False Then
            Me.baseTooltip.Show("Please define Flag", Me.grpPeriod, 2500) : Me.grpPeriod.Focus() : Return
        End If
        Try
            Me.Cursor = Cursors.WaitCursor
            If DateDiff(DateInterval.Day, Me.dtPicStart.Value.Date, Me.dtPicEnd.Value.Date) <= 1 Then
                Me.dtPicEnd.Value = DateSerial(Me.dtPicStart.Value.Year, Me.dtPicStart.Value.Month + 12, Me.dtPicStart.Value.Day - 1)
            End If
            Dim Flag As String = ""
            If Me.rdbQuarterly.Checked = True Then
                Flag = "Q"
            ElseIf Me.rdbSemeterly.Checked Then : Flag = "S"
            Else : Flag = "F"
            End If
            Me.BuildPeriode(Flag)
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub dtPicFilterStart_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicFilterStart.KeyDown
        If e.KeyCode = Keys.Delete Then
            Me.dtPicFilterStart.Text = ""
        End If
    End Sub

    Private Sub dtPicFilterEnd_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicFilterEnd.KeyDown
        If e.KeyCode = Keys.Delete Then
            Me.dtPicFilterEnd.Text = ""
        End If
    End Sub


#End Region

#Region " DataGrid "

#Region " DataGridView "

    Private Sub dgvPeriodic_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If (Me.rdbSemeterly.Checked = False And Me.rdbQuarterly.Checked = False) Then
                Me.dgvPeriodic.Enabled = False
                Me.dgvYearly.Enabled = False
                Return
            End If
            Select Case Me.chkAgreementGroup.Checked
                Case True
                    If Me.chkComboDistributor.CheckedValues.Length <= 0 Then
                        Me.dgvPeriodic.Enabled = False
                        Me.dgvYearly.Enabled = False
                        Return
                    End If
                Case False
                    If Me.MultiColumnCombo1.SelectedItem Is Nothing Then
                        Me.dgvPeriodic.Enabled = False
                        Me.dgvYearly.Enabled = False
                        Return
                    End If
            End Select

            If Me.txtAgreementNumber.Text = "" Then
                Me.dgvPeriodic.Enabled = False
                Me.dgvYearly.Enabled = False
                Return
            End If
            'If Me.dgvPeriodic.Rows.Count > 0 Then
            '    Me.dgvPeriodic.Enabled = False
            'Else
            '    Me.dgvPeriodic.Enabled = True
            'End If
            'Me.dgvPeriodic.Enabled = True
            'Me.dgvYearly.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvYearly_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvYearly.DataError
        Me.baseTooltip.Show("Please suply with valid value" & vbCrLf & "If you want to delete row " & vbCrLf & "Please press delete key on the keyboard !.", Me.dgvYearly, 2000)
        Me.dgvYearly.CancelEdit()
    End Sub

    Private Sub dgvYearly_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If (Me.rdbSemeterly.Checked = False And rdbQuarterly.Checked = False) Then
                Me.dgvPeriodic.Enabled = False
                Me.dgvYearly.Enabled = False
                Return
            End If
            Select Case Me.chkAgreementGroup.Checked
                Case True
                    If Me.chkComboDistributor.CheckedValues.Length <= 0 Then
                        Me.dgvPeriodic.Enabled = False
                        Me.dgvYearly.Enabled = False
                        Return
                    End If
                Case False
                    If Me.MultiColumnCombo1.SelectedItem Is Nothing Then
                        Me.dgvPeriodic.Enabled = False
                        Me.dgvYearly.Enabled = False
                        Return
                    End If
            End Select
            If Me.txtAgreementNumber.Text = "" Then
                Me.dgvPeriodic.Enabled = False
                Me.dgvYearly.Enabled = False
                Return
            End If
            'If Me.dgvYearly.Rows.Count > 0 Then
            '    Me.dgvYearly.Enabled = False
            'Else
            '    Me.dgvYearly.Enabled = True
            'End If
            'Me.dgvPeriodic.Enabled = True
            'Me.dgvYearly.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    'bila menambah data / mengedit
    Private Sub dgvPeriodic_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPeriodic.CellEndEdit
        Try
            If Not (Me.dgvPeriodic.Item(2, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvPeriodic.Item(2, e.RowIndex).Value) > 200 Then
                    Me.ShowMessageInfo("The value exceeds from 200")
                    Me.dgvPeriodic.CancelEdit()
                    Return
                End If
            End If
            If Not (Me.dgvPeriodic.Item(3, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvPeriodic.Item(3, e.RowIndex).Value) > 100 Then
                    Me.ShowMessageInfo("The value exceeds from 100")
                    Me.dgvPeriodic.CancelEdit()
                    Return
                End If
            End If
            If (Not IsNothing(Me.dgvPeriodic.Item(2, e.RowIndex).Value)) Or (Not IsNothing(Me.dgvPeriodic.Item(3, e.RowIndex).Value)) Then
                Me.dgvPeriodic.Item(1, e.RowIndex).Value = CObj(Me.txtAgreementNumber.Text)
                If Me.rdbQuarterly.Checked = True Then
                    Me.dgvPeriodic.Item(4, e.RowIndex).Value = CObj("Q")
                ElseIf Me.rdbSemeterly.Checked = True Then
                    Me.dgvPeriodic.Item(4, e.RowIndex).Value = CObj("S")
                End If
            Else
                Me.dgvPeriodic.Item(4, e.RowIndex).Value = DBNull.Value
                Me.dgvPeriodic.Item(1, e.RowIndex).Value = DBNull.Value
            End If
            If (Me.dgvPeriodic.Item(1, e.RowIndex).Value Is DBNull.Value) Or (Me.dgvPeriodic.Item(4, e.RowIndex).Value Is DBNull.Value) Then
                Me.ShowMessageInfo("No row's Been Edited" + vbCrLf & "Any Changed Rows will be discarded")
                Me.dgvPeriodic.CancelEdit()
            Else
                Me.dgvPeriodic.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
            'set column 0(agreement no) dengan agrement no dan qsy_flag dengan melihat rdbqs checkchanged nya
        Catch ex As InvalidCastException
            If Not IsNothing(ex) Then
                Me.dgvPeriodic.CancelEdit()
            End If
        Catch ex As Exception

        End Try
    End Sub

    'bila menambah data / mengedit
    Private Sub dgvYearly_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvYearly.CellEndEdit
        Try
            If Not (Me.dgvYearly.Item(2, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvYearly.Item(2, e.RowIndex).Value) > 200 Then
                    Me.ShowMessageInfo("The value exceeds from 200")
                    Me.dgvYearly.CancelEdit()
                    Return
                End If
            End If
            If Not (Me.dgvYearly.Item(3, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvYearly.Item(3, e.RowIndex).Value) > 100 Then
                    Me.ShowMessageInfo("The value exceeds from 100")
                    Me.dgvYearly.CancelEdit()
                    Return
                End If
            End If
            If (Not IsNothing(Me.dgvYearly.Item(2, e.RowIndex).Value)) Or (Not IsNothing(Me.dgvYearly.Item(3, e.RowIndex).Value)) Then
                Me.dgvYearly.Item(1, e.RowIndex).Value = CObj(Me.txtAgreementNumber.Text)
                If Me.rdbQuarterly.Checked = True Then
                    Me.dgvYearly.Item(4, e.RowIndex).Value = CObj("Y")
                ElseIf Me.rdbSemeterly.Checked = True Then
                    Me.dgvYearly.Item(4, e.RowIndex).Value = CObj("Y")
                End If
            Else
                Me.dgvYearly.Item(4, e.RowIndex).Value = DBNull.Value
                Me.dgvYearly.Item(1, e.RowIndex).Value = DBNull.Value
            End If
            If (Me.dgvYearly.Item(1, e.RowIndex).Value Is DBNull.Value) Or (Me.dgvYearly.Item(4, e.RowIndex).Value Is DBNull.Value) Then
                Me.ShowMessageInfo("No row's Been Edited" + vbCrLf & "Any Changed Rows will be discarded")
                Me.dgvYearly.CancelEdit()
            Else
                Me.dgvYearly.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
        Catch ex As Exception
            If Not IsNothing(ex) Then
                Me.dgvYearly.CancelEdit()
            End If
        End Try
    End Sub

    Private Sub dgvPeriodic_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvPeriodic.DataError
        Me.baseTooltip.Show("Please suply with valid value" & vbCrLf & "If you want to delete row " & vbCrLf & "Please press delete key on the keyboard !.", Me.dgvPeriodic, 2500)
        Me.dgvPeriodic.CancelEdit()
    End Sub
    Private Sub dgvYearly_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvYearly.UserDeletedRow
        Me.dgvYearly.CommitEdit(DataGridViewDataErrorContexts.RowDeletion)
    End Sub

#End Region

#Region " GridEx "

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            Dim HasReferencedData As Boolean = False, HasGeneratedDiscountProgressive As Boolean = False, _
            HasOutOfDate As Boolean = False

            Me.Cursor = Cursors.WaitCursor
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If

            If Me.GridEX1.DataSource Is Nothing Then
                'Me.HasLoad = False : Me.btnAdd_Click(Me.btnAdd, New EventArgs()) : Me.HasLoad = True : Me.SFG = StateFillingGrid.HasFilled
                Return
            End If
            If Me.GridEX1.SelectedItems.Count <= 0 Then
                'Me.HasLoad = False : Me.btnAdd_Click(Me.btnAdd, New EventArgs()) : Me.HasLoad = True : Me.SFG = StateFillingGrid.HasFilled
                Return
            End If
            If Not Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                'Me.HasLoad = False : Me.btnAdd_Click(Me.btnAdd, New EventArgs()) : Me.HasLoad = True : Me.SFG = StateFillingGrid.HasFilled
                Return
            End If
            If Me.HasLoad = True Then
                Me.SFG = StateFillingGrid.Filling
                Me.Mode = SaveMode.Update
                If Me.clsDiscAgreement.CheckAgreement_Group(Me.GridEX1.GetValue("AGREEMENT_NO").ToString()) = True Then
                    Me.BINDcheckedCombo(Me.clsDiscAgreement.ViewDistributor(), "")
                    Me.MultiColumnCombo1.Visible = False : Me.chkAgreementGroup.Checked = True : Me.btnAddDistributor.Enabled = True
                    Me.chkComboDistributor.Visible = True : Me.chkComboDistributor.CheckAll()
                Else
                    Me.clsDiscAgreement.GetDataViewDistributor()
                    'Me.BindMulticolumnCombo(Me.clsDiscAgreement.ViewDistributor(), "")
                    Me.MultiColumnCombo1.Visible = True : Me.chkComboDistributor.Visible = False
                    Me.chkAgreementGroup.Checked = False : Me.MultiColumnCombo1.Value = Me.GridEX1.GetValue("DISTRIBUTOR_ID")
                    Me.btnAddDistributor.Enabled = False
                End If
                Me.txtAgreementNumber.Text = Me.GridEX1.GetValue("AGREEMENT_NO").ToString()
                'Me.txtTargetYear.Text = Format(Me.txtTargetYear.Text, "###0.")
                If (CStr(Me.GridEX1.GetValue("QS_TREATMENT_FLAG")) = "Q") Then
                    Me.rdbQuarterly.Checked = True : Me.rdbQuarterly_Click(Me.rdbQuarterly, New System.EventArgs())
                ElseIf (CStr(Me.GridEX1.GetValue("QS_TREATMENT_FLAG")) = "S") Then
                    'Me.rdbSemeterly.Checked = False
                    Me.rdbSemeterly.Checked = True : Me.rdbSemeterly_Click(Me.rdbSemeterly, New System.EventArgs())
                End If
                Me.dtPicStart.Value = Convert.ToDateTime(Me.GridEX1.GetValue("START_DATE"))
                Me.DateFromGridEx = Convert.ToDateTime(Me.GridEX1.GetValue("START_DATE"))
                Me.dtPicEnd.Value = Convert.ToDateTime(Me.GridEX1.GetValue("END_DATE"))
                Me.txtAgrementDescription.Text = Me.GridEX1.GetValue("AGREEMENT_DESC").ToString()
                If Convert.ToDateTime(Me.GridEX1.GetValue("END_DATE")) < NufarmBussinesRules.SharedClass.ServerDate() Then
                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.UnabledControl() : Me.chkComboDistributor.ReadOnly = True
                    Me.MultiColumnCombo1.ReadOnly = True : HasOutOfDate = True
                    If Me.chkAgreementGroup.Checked = True Then
                        Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = True
                    Else
                        Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = False
                    End If
                ElseIf Me.clsDiscAgreement.HasReferecedData(Me.GridEX1.GetValue("AGREEMENT_NO").ToString(), False) = True Then
                    HasReferencedData = True
                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.UnabledControl() : Me.dgvYearly.Enabled = True : Me.dgvPeriodic.Enabled = True
                    Me.dgvYearlyVal.Enabled = True : Me.dgvPeriodicVal.Enabled = True
                    Me.MultiColumnCombo1.ReadOnly = True : Me.chkComboDistributor.ReadOnly = True
                    If Me.chkAgreementGroup.Checked = True Then
                        Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = True
                    Else
                        Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = False
                    End If
                Else
                    Me.EnabledControl() : Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.chkComboDistributor.ReadOnly = False
                    If Me.chkAgreementGroup.Checked = True Then
                        Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = True
                        Me.MultiColumnCombo1.Visible = False
                    Else
                        Me.MultiColumnCombo1.Visible = True : Me.chkComboDistributor.Visible = False
                        Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = False
                    End If
                    'CHECK_GROUP
                End If
                If Me.rdbQuarterly.Checked = True Then
                    If Me.clsDiscAgreement.HasGeneratedProgressiveDiscount(Me.GridEX1.GetValue("AGREEMENT_NO").ToString(), "Q", False) = True Then
                        HasGeneratedDiscountProgressive = True
                        Me.dgvPeriodic.Enabled = False : Me.MultiColumnCombo1.ReadOnly = True
                        Me.dgvPeriodicVal.Enabled = True
                        If Me.chkAgreementGroup.Checked = True Then
                            Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = True
                        Else
                            Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = False
                        End If
                    Else
                        Me.MultiColumnCombo1.ReadOnly = False
                    End If
                ElseIf Me.rdbSemeterly.Checked = True Then
                    If Me.clsDiscAgreement.HasGeneratedProgressiveDiscount(Me.GridEX1.GetValue("AGREEMENT_NO").ToString(), "S", False) = True Then
                        HasGeneratedDiscountProgressive = True
                        Me.dgvPeriodic.Enabled = False : Me.MultiColumnCombo1.ReadOnly = True
                        Me.dgvPeriodicVal.Enabled = True
                        If Me.chkAgreementGroup.Checked = True Then
                            Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = True
                        Else
                            Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = False
                        End If
                    Else
                        Me.MultiColumnCombo1.ReadOnly = False
                    End If
                End If
                If Me.clsDiscAgreement.HasGeneratedProgressiveDiscount(Me.GridEX1.GetValue("AGREEMENT_NO").ToString(), "Y", True) = True Then
                    Me.dgvYearly.Enabled = False : HasGeneratedDiscountProgressive = True
                    Me.MultiColumnCombo1.ReadOnly = True
                    Me.dgvYearlyVal.Enabled = True
                    If Me.chkAgreementGroup.Checked = True Then
                        Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = True
                    Else
                        Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = False
                    End If
                Else
                    Me.dgvYearly.Enabled = True : Me.dgvYearlyVal.Enabled = True
                    Me.MultiColumnCombo1.ReadOnly = False
                End If
                Me.txtAgreementNumber.Enabled = False : Me.rdbQuarterly.Enabled = False
                Me.rdbSemeterly.Enabled = False 'Me.txtTargetYear.Enabled = False
                Dim Flag As String = ""
                If Me.rdbSemeterly.Checked Then : Flag = "S"
                ElseIf Me.rdbQuarterly.Checked Then : Flag = "Q"
                Else : Flag = "F"
                End If
                Me.BuildPeriode(Flag)
                If HasGeneratedDiscountProgressive Or HasReferencedData Or HasOutOfDate Then
                    Me.MultiColumnCombo1.ReadOnly = True : Me.chkComboDistributor.ReadOnly = True
                End If
            End If
            Me.chkAgreementGroup.Enabled = False : Me.btnAddDistributor.Text = "&Add"
            Me.SFG = StateFillingGrid.HasFilled
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled : Me.HasLoad = True
        Finally
            Me.ReadAcces()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Not (e.Row.RowType = Janus.Windows.GridEX.RowType.Record) Then
                Return
            End If
            'check referensi table anak 
            Dim AGREEMENT_NO As String = Me.GridEX1.GetValue("AGREEMENT_NO").ToString()
            If Me.clsDiscAgreement.HasReferecedData(AGREEMENT_NO, False) = True Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                e.Cancel = True
                Return
            Else
                If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True
                    Return
                End If
                If Me.chkAgreementGroup.Checked = True Then
                    If Me.ShowConfirmedMessage("Delete all data based on agreement ?" & vbCrLf & "If yes all rows with the Agreemen_no '" & Me.GridEX1.GetValue("AGREEMENT_NO").ToString() & _
                                   vbCrLf & "Will be deleted too.") = Windows.Forms.DialogResult.No Then
                        Me.clsDiscAgreement.DeleteAgreement(AGREEMENT_NO, False, Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString())
                        Me.GridEX1.MoveTo(Me.GridEX1.FilterRow)
                    Else
                        Me.clsDiscAgreement.DeleteAgreement(AGREEMENT_NO, True)
                        Me.LoadData()
                        Me.GridEX1.MoveTo(Me.GridEX1.FilterRow)
                    End If
                Else
                    Me.clsDiscAgreement.DeleteAgreement(AGREEMENT_NO, True)
                    Me.LoadData()
                    Me.GridEX1.MoveTo(Me.GridEX1.FilterRow)
                End If
                'Me.RefreshData()
                Me.ShowMessageInfo(Me.MessageSuccesDelete)
                'refresh data
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        End Try
    End Sub

    Private Sub GridEX1_Error(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ErrorEventArgs) Handles GridEX1.Error
        Try
            Me.ShowMessageInfo("Data Can not be Updated due to " & e.Exception.Message)
            Me.GridEX1.CancelCurrentEdit()
        Catch ex As Exception

        End Try
    End Sub

#End Region

#End Region

#Region " TextBox "

    Private Sub txtAgreementNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAgreementNumber.TextChanged
        'If Me.clsDiscAgreement.GetDataSetPeriod().HasChanges() Then
        'End If
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        If Me.txtAgreementNumber.Text <> "" Then
            If Not IsNothing(Me.clsDiscAgreement.GetDataSetPeriod()) Then
                If Me.clsDiscAgreement.GetDataSetPeriod().HasChanges() Then
                    If Me.Mode = SaveMode.Save Then
                        Me.FillHideColumnWithDefaultValue(Me.dgvPeriodic)
                        Me.FillHideColumnWithDefaultValue(Me.dgvYearly)
                    End If
                End If
            End If
            Me.rdbSemeterly.Enabled = True
            Me.rdbQuarterly.Enabled = True
            Me.dgvPeriodic.Enabled = True
            Me.dgvPeriodicVal.Enabled = True
            Me.dgvYearly.Enabled = True
            Me.dgvYearlyVal.Enabled = True
            'Me.dgvPeriodic.Enabled = True
            'Me.dgvYearly.Enabled = True
        Else
            If Not IsNothing(Me.clsDiscAgreement.GetDataSetPeriod()) Then
                If Me.clsDiscAgreement.GetDataSetPeriod().HasChanges() Then
                    Me.clsDiscAgreement.GetDataSetPeriod().RejectChanges()
                    If Me.rdbQuarterly.Checked = True Then
                        If IsNothing(Me.DVQuarterly) Then : Me.DVQuarterly = Me.clsDiscAgreement.GetTableQuarterly().DefaultView() : End If
                        Me.dgvPeriodic.DataSource = Me.DVQuarterly
                        If IsNothing(Me.DVQuarterlyV) Then : Me.DVQuarterlyV = Me.clsDiscAgreement.getTableQuarterlyV().DefaultView() : End If
                        Me.dgvPeriodicVal.DataSource = Me.DVQuarterlyV
                    ElseIf Me.rdbSemeterly.Checked = True Then
                        If IsNothing(Me.DVSemesterly) Then : Me.DVSemesterly = Me.clsDiscAgreement.GetTableSemesterly().DefaultView() : End If
                        Me.dgvPeriodic.DataSource = Me.DVSemesterly
                        If IsNothing(Me.DVSemesterlyV) Then : Me.DVSemesterlyV = Me.clsDiscAgreement.getTableSemesterlyV().DefaultView() : End If
                        Me.dgvPeriodicVal.DataSource = Me.DVSemesterlyV
                    Else
                        If IsNothing(Me.DVQuarterly) Then : Me.DVQuarterly = Me.clsDiscAgreement.GetTableQuarterly().DefaultView() : End If
                        Me.dgvPeriodic.DataSource = Me.DVQuarterly
                        If IsNothing(Me.DVQuarterlyV) Then : Me.DVQuarterlyV = Me.clsDiscAgreement.getTableQuarterlyV().DefaultView() : End If
                        Me.dgvPeriodicVal.DataSource = Me.DVQuarterlyV
                    End If
                    If IsNothing(Me.DVYearly) Then : Me.DVYearly = Me.clsDiscAgreement.GetTableYearly().DefaultView() : End If
                    Me.dgvYearly.DataSource = Me.DVYearly
                    If IsNothing(Me.DVYearlyV) Then : Me.DVYearlyV = Me.clsDiscAgreement.getTableYearlyV().DefaultView() : End If
                    Me.dgvYearlyVal.DataSource = Me.DVYearlyV
                End If
            End If
            Me.rdbQuarterly.Checked = False
            Me.rdbSemeterly.Checked = False
            Me.rdbSemeterly.Enabled = False
            Me.rdbQuarterly.Enabled = False
            Me.dgvPeriodic.Enabled = False
            Me.dgvPeriodicVal.Enabled = False
            Me.dgvYearly.Enabled = False
            Me.dgvYearlyVal.Enabled = False
            'Me.dgvPeriodic.Enabled = False
            'Me.dgvYearly.Enabled = False
        End If

    End Sub

#End Region

#Region " Bar "
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnRenameColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.grpGrid)
                Case "btnShowFieldChooser"
                    Me.GridEX1.ShowFieldChooser(Me, "Drag column here to hide from grid")
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnPrint"
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    'Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                        Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    End If
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                Case "btnPageSettings"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
                Case "btnCustFilter"
                    Me.FilterEditor1.Visible = True
                    Me.FilterEditor1.SourceControl = Me.GridEX1
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.GridEX1.RemoveFilters()
                Case "btnDefaultFilter"
                    Me.FilterEditor1.Visible = False
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.GridEX1.RemoveFilters()
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
                Case "btnCardView"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.CardView
                    Me.GridEX1.RootTable.Columns("Icon").Visible = False
                Case "btnSingleCard"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.SingleCard
                    Me.GridEX1.RootTable.Columns("Icon").Visible = False
                Case "btnTableView"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.TableView
                    Me.GridEX1.RootTable.Columns("Icon").Visible = True
                Case "btnRefresh"
                    Me.btnAplyRange_Click(Me.btnAplyRange, New EventArgs())
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        End Try
    End Sub
#End Region

#Region " Radio Button "

    Private Sub rdbQuarterly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbQuarterly.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbQuarterly.Checked = True Then
                Select Case Me.Mode
                    Case SaveMode.Save
                        Me.clsDiscAgreement.FetchDsSetPeriod(Me.txtAgreementNumber.Text.Trim(), "Q", False)
                        Me.ClearData(Me.dgvPeriodic)
                        Me.ClearData(Me.dgvYearly)
                    Case SaveMode.Update
                        Me.clsDiscAgreement.FetchDsSetPeriod(Me.txtAgreementNumber.Text.Trim(), "Q", False)

                        Me.DVQuarterly = Me.clsDiscAgreement.GetTableQuarterly().DefaultView()
                        Me.dgvPeriodic.DataSource = Me.DVQuarterly
                        Me.DVYearly = Me.clsDiscAgreement.GetTableYearly().DefaultView()
                        Me.dgvYearly.DataSource = Me.DVYearly

                        Me.DVQuarterlyV = Me.clsDiscAgreement.getTableQuarterlyV().DefaultView()
                        Me.dgvPeriodicVal.DataSource = Me.DVQuarterlyV
                        Me.DVYearlyV = Me.clsDiscAgreement.getTableYearlyV().DefaultView()
                        Me.dgvYearlyVal.DataSource = Me.DVYearlyV
                End Select
                Me.tbPeriode.Text = "Quarterly Discount"
                Me.dgvPeriodic.Enabled = True
                Me.dgvYearly.Enabled = True

                Me.TbPeriodeVal.Text = "Quarterly Discount"
                Me.dgvPeriodicVal.Enabled = True
                Me.dgvYearlyVal.Enabled = True

            End If
            'next result
        
            Me.dtPicEnd_ValueChanged(Me.dtPicFilterEnd, New EventArgs())
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name & "_rdbQuarterly_CheckedChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbSemeterly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSemeterly.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbSemeterly.Checked = True Then
                Select Case Me.Mode
                    Case SaveMode.Save
                        Me.clsDiscAgreement.FetchDsSetPeriod(Me.txtAgreementNumber.Text.Trim(), "S", False)
                        Me.ClearData(Me.dgvPeriodic)
                        Me.ClearData(Me.dgvYearly)
                    Case SaveMode.Update
                        Me.clsDiscAgreement.FetchDsSetPeriod(Me.txtAgreementNumber.Text.Trim(), "S", False)
                        'bind grid
                        Me.DVSemesterly = Me.clsDiscAgreement.GetTableSemesterly().DefaultView()
                        Me.dgvPeriodic.DataSource = Me.DVSemesterly

                        Me.DVYearly = Me.clsDiscAgreement.GetTableYearly().DefaultView()
                        Me.dgvYearly.DataSource = Me.DVYearly

                        Me.DVSemesterlyV = Me.clsDiscAgreement.getTableSemesterlyV().DefaultView()
                        Me.dgvPeriodicVal.DataSource = Me.DVSemesterlyV
                        Me.DVYearlyV = Me.clsDiscAgreement.getTableYearlyV().DefaultView()
                        Me.dgvYearlyVal.DataSource = Me.clsDiscAgreement.getTableYearlyV()
                End Select
                Me.tbPeriode.Text = "Semesterly Discount"
                Me.dgvPeriodic.Enabled = True
                Me.dgvYearly.Enabled = True

                Me.TbPeriodeVal.Text = "Semesterly Discount"
                Me.dgvPeriodicVal.Enabled = True
                Me.dgvYearlyVal.Enabled = True

                Me.dtPicEnd_ValueChanged(Me.dtPicFilterEnd, New EventArgs())
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name & "_rdbSemeterly_CheckedChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " Timer "

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Application.DoEvents()
        If Me.WhileSaving = Saving.Succes Then
            For i As Integer = 0 To Me.ResultRandom
                For index As Integer = 1 To 30
                    MyBase.ST.Refresh()
                    MyBase.ST.Label1.Refresh()
                    MyBase.ST.PictureBox1.Refresh()
                Next
            Next
            Me.btnAplyRange_Click(Me.btnAplyRange, New EventArgs()) : Me.GridEX1.MoveTo(Me.GridEX1.FilterRow)
            Me.CloseProgres(True)
        ElseIf Me.WhileSaving = Saving.Failed Then
            Me.CloseProgres(False)
        End If
    End Sub

#End Region

#Region " Check Box "

    'Private Sub chkShowAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        If Me.chkShowAll.Checked = False Then
    '            Me.clsDiscAgreement.GetData(False, Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()))
    '            Me.BindGrid("")
    '        Else
    '            Me.GridEX1.Hide()
    '            Me.clsDiscAgreement.GetData(True, Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()))
    '            Me.BindGrid("") : Me.GridEX1.Show() : Me.GridEX1.Update()
    '        End If
    '    Catch ex As Exception

    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub chkAgreementGroup_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAgreementGroup.CheckedChanged
        Try
            If Me.SFG = StateFillingGrid.Filling Then
                Return
            End If
            If Me.chkAgreementGroup.Checked = True Then
                Me.chkComboDistributor.Visible = True
                Me.MultiColumnCombo1.Visible = False
                Me.BINDcheckedCombo(Me.clsDiscAgreement.ViewDistributor(), "")
                Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = True
                Me.chkComboDistributor.UncheckAll()
                Me.chkComboDistributor.Values.Clear()
            Else
                Me.chkComboDistributor.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = False
                Me.MultiColumnCombo1.Visible = True
                Me.chkComboDistributor.Visible = False
                Me.BindMulticolumnCombo(Me.clsDiscAgreement.ViewDistributor(), "")
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#End Region

    Private Sub dgvPeriodic_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvPeriodic.Enter
        If (Me.rdbSemeterly.Checked = False And Me.rdbQuarterly.Checked = False) Then
            Me.dgvPeriodic.Enabled = False
            Me.dgvYearly.Enabled = False
            Return
        End If
        Select Case Me.chkAgreementGroup.Checked
            Case True
                If Me.chkComboDistributor.CheckedValues.Length <= 0 Then
                    Me.dgvPeriodic.Enabled = False
                    Me.dgvYearly.Enabled = False
                    Return
                End If
            Case False
                If Me.MultiColumnCombo1.SelectedItem Is Nothing Then
                    Me.dgvPeriodic.Enabled = False
                    Me.dgvYearly.Enabled = False
                    Return
                End If
        End Select

        If Me.txtAgreementNumber.Text = "" Then
            Me.dgvPeriodic.Enabled = False
            Me.dgvYearly.Enabled = False
            Return
        End If
    End Sub

    Private Sub dgvPeriodicVal_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvPeriodicVal.Enter
        If (Me.rdbSemeterly.Checked = False And Me.rdbQuarterly.Checked = False) Then
            Me.dgvPeriodicVal.Enabled = False
            Me.dgvYearlyVal.Enabled = False
            Return
        End If
        Select Case Me.chkAgreementGroup.Checked
            Case True
                If Me.chkComboDistributor.CheckedValues.Length <= 0 Then
                    Me.dgvPeriodicVal.Enabled = False
                    Me.dgvYearlyVal.Enabled = False
                    Return
                End If
            Case False
                If Me.MultiColumnCombo1.SelectedItem Is Nothing Then
                    Me.dgvPeriodicVal.Enabled = False
                    Me.dgvYearlyVal.Enabled = False
                    Return
                End If
        End Select

        If Me.txtAgreementNumber.Text = "" Then
            Me.dgvPeriodicVal.Enabled = False
            Me.dgvYearlyVal.Enabled = False
            Return
        End If
    End Sub

    Private Sub dgvPeriodicVal_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPeriodicVal.CellEndEdit
        Try
            If Not (Me.dgvPeriodicVal.Item(2, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvPeriodicVal.Item(2, e.RowIndex).Value) > 200 Then
                    Me.ShowMessageInfo("The value exceeds from 200")
                    Me.dgvPeriodicVal.CancelEdit()
                    Return
                End If
            End If
            If Not (Me.dgvPeriodicVal.Item(3, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvPeriodicVal.Item(3, e.RowIndex).Value) > 100 Then
                    Me.ShowMessageInfo("The value exceeds from 100")
                    Me.dgvPeriodicVal.CancelEdit()
                    Return
                End If
            End If
            If (Not IsNothing(Me.dgvPeriodicVal.Item(2, e.RowIndex).Value)) Or (Not IsNothing(Me.dgvPeriodicVal.Item(3, e.RowIndex).Value)) Then
                Me.dgvPeriodicVal.Item(1, e.RowIndex).Value = CObj(Me.txtAgreementNumber.Text)
                If Me.rdbQuarterly.Checked = True Then
                    If Me.dgvPeriodicVal.Item(4, e.RowIndex).Value Is DBNull.Value Then
                        Me.dgvPeriodicVal.Item(4, e.RowIndex).Value = CObj("Q")
                    End If
                ElseIf Me.rdbSemeterly.Checked = True Then
                    If Me.dgvPeriodicVal.Item(4, e.RowIndex).Value Is DBNull.Value Then
                        Me.dgvPeriodicVal.Item(4, e.RowIndex).Value = CObj("S")
                    End If
                End If
            Else
                Me.dgvPeriodicVal.Item(4, e.RowIndex).Value = DBNull.Value
                Me.dgvPeriodicVal.Item(1, e.RowIndex).Value = DBNull.Value
            End If
            If (Me.dgvPeriodicVal.Item(1, e.RowIndex).Value Is DBNull.Value) Or (Me.dgvPeriodicVal.Item(4, e.RowIndex).Value Is DBNull.Value) Then
                Me.ShowMessageInfo("No row's Been Edited" + vbCrLf & "Any Changed Rows will be discarded")
                Me.dgvPeriodicVal.CancelEdit()
            Else
                Me.dgvPeriodicVal.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
            'set column 0(agreement no) dengan agrement no dan qsy_flag dengan melihat rdbqs checkchanged nya
        Catch ex As InvalidCastException
            If Not IsNothing(ex) Then
                Me.dgvPeriodicVal.CancelEdit()
            End If
        Catch ex As Exception
            If Not IsNothing(ex) Then
                Me.dgvPeriodicVal.CancelEdit()
            End If
        End Try
    End Sub

    Private Sub dgvYearlyVal_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvYearlyVal.CellEndEdit
        Try
            If Not (Me.dgvYearlyVal.Item(2, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvYearlyVal.Item(2, e.RowIndex).Value) > 200 Then
                    Me.ShowMessageInfo("The value exceeds from 200")
                    Me.dgvYearlyVal.CancelEdit()
                    Return
                End If
            End If
            If Not (Me.dgvYearlyVal.Item(3, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvYearlyVal.Item(3, e.RowIndex).Value) > 100 Then
                    Me.ShowMessageInfo("The value exceeds from 100")
                    Me.dgvYearlyVal.CancelEdit()
                    Return
                End If
            End If
            If (Not IsNothing(Me.dgvYearlyVal.Item(2, e.RowIndex).Value)) Or (Not IsNothing(Me.dgvYearlyVal.Item(3, e.RowIndex).Value)) Then
                Me.dgvYearlyVal.Item(1, e.RowIndex).Value = CObj(Me.txtAgreementNumber.Text)
                If Me.rdbQuarterly.Checked = True Then
                    Me.dgvYearlyVal.Item(4, e.RowIndex).Value = CObj("Y")
                ElseIf Me.rdbSemeterly.Checked = True Then
                    Me.dgvYearlyVal.Item(4, e.RowIndex).Value = CObj("Y")
                End If
            Else
                Me.dgvYearlyVal.Item(4, e.RowIndex).Value = DBNull.Value
                Me.dgvYearlyVal.Item(1, e.RowIndex).Value = DBNull.Value
            End If
            If (Me.dgvYearlyVal.Item(1, e.RowIndex).Value Is DBNull.Value) Or (Me.dgvYearlyVal.Item(4, e.RowIndex).Value Is DBNull.Value) Then
                Me.ShowMessageInfo("No row's Been Edited" + vbCrLf & "Any Changed Rows will be discarded")
                Me.dgvYearlyVal.CancelEdit()
            Else
                Me.dgvYearlyVal.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
        Catch ex As Exception
            If Not IsNothing(ex) Then
                Me.dgvPeriodicVal.CancelEdit()
            End If
        End Try
    End Sub

    Private Sub dgvYearly_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvYearly.Enter
        If (Me.rdbSemeterly.Checked = False And rdbQuarterly.Checked = False) Then
            Me.dgvPeriodic.Enabled = False
            Me.dgvYearly.Enabled = False
            Return
        End If
        Select Case Me.chkAgreementGroup.Checked
            Case True
                If Me.chkComboDistributor.CheckedValues.Length <= 0 Then
                    Me.dgvPeriodic.Enabled = False
                    Me.dgvYearly.Enabled = False
                    Return
                End If
            Case False
                If Me.MultiColumnCombo1.SelectedItem Is Nothing Then
                    Me.dgvPeriodic.Enabled = False
                    Me.dgvYearly.Enabled = False
                    Return
                End If
        End Select
        If Me.txtAgreementNumber.Text = "" Then
            Me.dgvPeriodic.Enabled = False
            Me.dgvYearly.Enabled = False
            Return
        End If
        'If Me.dgvYearly.Rows.Count > 0 Then
        '    Me.dgvYearly.Enabled = False
        'Else
        '    Me.dgvYearly.Enabled = True
        'End If
        'Me.dgvPeriodic.Enabled = True
        'Me.dgvYearly.Enabled = True
    End Sub

    Private Sub dgvYearlyVal_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvYearlyVal.Enter
        If (Me.rdbSemeterly.Checked = False And rdbQuarterly.Checked = False) Then
            Me.dgvPeriodicVal.Enabled = False
            Me.dgvYearlyVal.Enabled = False
            Return
        End If
        Select Case Me.chkAgreementGroup.Checked
            Case True
                If Me.chkComboDistributor.CheckedValues.Length <= 0 Then
                    Me.dgvPeriodicVal.Enabled = False
                    Me.dgvYearlyVal.Enabled = False
                    Return
                End If
            Case False
                If Me.MultiColumnCombo1.SelectedItem Is Nothing Then
                    Me.dgvPeriodicVal.Enabled = False
                    Me.dgvYearlyVal.Enabled = False
                    Return
                End If
        End Select
        If Me.txtAgreementNumber.Text = "" Then
            Me.dgvPeriodicVal.Enabled = False
            Me.dgvYearlyVal.Enabled = False
            Return
        End If
        'If Me.dgvYearly.Rows.Count > 0 Then
        '    Me.dgvYearly.Enabled = False
        'Else
        '    Me.dgvYearly.Enabled = True
        'End If
        'Me.dgvPeriodic.Enabled = True
        'Me.dgvYearly.Enabled = True
    End Sub

    Private Sub dgvPeriodicVal_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvPeriodicVal.DataError
        Me.baseTooltip.Show("Please suply with valid value" & vbCrLf & "If you want to delete row " & vbCrLf & "Please press delete key on the keyboard !.", Me.dgvPeriodicVal, 2500)
        Me.dgvPeriodicVal.CancelEdit()
    End Sub

    Private Sub dgvPeriodicVal_UserDeletedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvPeriodicVal.UserDeletedRow
        Me.dgvPeriodicVal.CommitEdit(DataGridViewDataErrorContexts.RowDeletion)
    End Sub

    Private Sub dgvYearlyVal_UserDeletedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvYearlyVal.UserDeletedRow
        Me.dgvYearlyVal.CommitEdit(DataGridViewDataErrorContexts.RowDeletion)
    End Sub

    Private Sub dgvYearlyVal_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvYearlyVal.DataError
        Me.baseTooltip.Show("Please suply with valid value" & vbCrLf & "If you want to delete row " & vbCrLf & "Please press delete key on the keyboard !.", Me.dgvYearlyVal, 2000)
        Me.dgvYearlyVal.CancelEdit()
    End Sub

    Private Sub dgvPeriodic_UserDeletedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvPeriodic.UserDeletedRow
        Me.dgvPeriodic.CommitEdit(DataGridViewDataErrorContexts.RowDeletion)
    End Sub

    Private Sub btnbyAgreementNO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbyAgreementNO.Click
        Me.SetRangeDateFilter(btnbyAgreementNO, True)
    End Sub

    Private Sub btnbyRangedDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbyRangedDate.Click
        Me.SetRangeDateFilter(btnbyRangedDate, True)
    End Sub
    Private Sub SetRangeDateFilter(ByVal btn As DevComponents.DotNetBar.ButtonItem, ByVal IsChecked As Boolean)
        'lepasin semua chek
        Me.btnbyAgreementNO.Checked = False
        Me.btnbyRangedDate.Checked = False
        If IsChecked Then 'menu di chedked
            Select Case btn.Name
                Case "btnbyAgreementNO"
                    Me.btnbyRangedDate.Checked = False
                    Me.txtSearchAgreement.Visible = True : Me.txtSearchAgreement.Text = ""
                    Me.txtSearchAgreement.Focus()
                    Me.dtPicFilterStart.Visible = False
                    Me.dtPicFilterStart.Text = ""

                    Me.dtPicFilterEnd.Visible = False
                    Me.dtPicFilterEnd.Text = ""

                    Me.btnAplyRange.Text = "Find agreement"
                    'Me.btnbyAgreementNO.Checked = True

                    Me.btnAplyRange.Enabled = True
                Case "btnbyRangedDate"
                    Me.btnbyAgreementNO.Checked = False
                    Me.dtPicFilterStart.Visible = True
                    Me.dtPicFilterEnd.Visible = True

                    Me.txtSearchAgreement.Visible = False
                    Me.btnAplyRange.Text = "Apply filter"
                    'Me.btnbyRangedDate.Checked = True
                    Me.btnAplyRange.Enabled = True
            End Select
            btn.Checked = True
        Else
            'menu dilepasin semua'
            Me.SFG = StateFillingGrid.Filling
            Me.btnAplyRange.Text = "Apply filter"
            Me.txtSearchAgreement.Enabled = False
            Me.dtPicFilterStart.Enabled = False
            Me.dtPicFilterEnd.Enabled = False
            Me.txtSearchAgreement.Text = ""
            Me.btnAplyRange.Enabled = False
            Me.GridEX1.DataSource = Nothing
            Me.SFG = StateFillingGrid.HasFilled
            btn.Checked = False
            Return
        End If
        'Me.grdPurchaseOrder.SetDataBinding(Nothing, "")
        Me.SFG = StateFillingGrid.Filling
        Me.GridEX1.DataSource = Nothing
        Me.SFG = StateFillingGrid.HasFilled
    End Sub
    Private Function IsValidRow(ByVal DGV As DataGridView, ByVal RowIndex As Integer) As Boolean
        Dim ValcolPercent As Object = DGV.Item(2, RowIndex).Value
        Dim ValColDisc As Object = DGV.Item(3, RowIndex).Value
        If IsDBNull(ValcolPercent) Or IsNothing(ValcolPercent) Then
            Return False
        End If
        If IsDBNull(ValColDisc) Or IsNothing(ValColDisc) Then
            Return False
        End If
        Return True
    End Function

    Private Sub dgvPeriodic_RowLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPeriodic.RowLeave
        Try
            If e.RowIndex >= 0 Then
                Dim AgreeNO As String = ""
                If IsNothing(Me.dgvPeriodic.Item(1, e.RowIndex).Value) Then
                    AgreeNO = Me.txtAgreementNumber.Text.TrimEnd().TrimStart()
                ElseIf IsDBNull(Me.dgvPeriodic.Item(1, e.RowIndex).Value) Then
                    AgreeNO = Me.txtAgreementNumber.Text.TrimEnd().TrimStart()
                End If
                Me.dgvPeriodic.Item(1, e.RowIndex).Value.ToString()
                Dim ValidRow As Boolean = IsValidRow(Me.dgvPeriodic, e.RowIndex)
                If ValidRow Then
                    Me.dgvPeriodic.Item(1, e.RowIndex).Value = Me.txtAgreementNumber.Text
                    Me.dgvPeriodic.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    'Else
                    '    Me.ShowMessageInfo("[More than %] and dispro")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvYearly_RowLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvYearly.RowLeave
        Try
            If e.RowIndex >= 0 Then
                Dim AgreeNO As String = ""
                If IsNothing(dgvYearly.Item(1, e.RowIndex).Value) Then
                    AgreeNO = Me.txtAgreementNumber.Text.TrimEnd().TrimStart()
                ElseIf IsDBNull(dgvYearly.Item(1, e.RowIndex).Value) Then
                    AgreeNO = Me.txtAgreementNumber.Text.TrimEnd().TrimStart()
                End If
                Dim ValidRow As Boolean = IsValidRow(Me.dgvYearly, e.RowIndex)
                If ValidRow Then
                    Me.dgvYearly.Item(1, e.RowIndex).Value = Me.txtAgreementNumber.Text
                    Me.dgvYearly.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If
                If String.IsNullOrEmpty(AgreeNO) Then

                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub dgvPeriodicVal_RowLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPeriodicVal.RowLeave
        Try
            If e.RowIndex >= 0 Then
                Dim AgreeNO = ""
                If IsNothing(Me.dgvPeriodicVal.Item(1, e.RowIndex).Value) Then
                    AgreeNO = Me.txtAgreementNumber.Text.TrimEnd().TrimStart()
                ElseIf IsDBNull(Me.dgvPeriodicVal.Item(1, e.RowIndex).Value) Then
                    AgreeNO = Me.txtAgreementNumber.Text.TrimEnd().TrimStart()
                End If
                Dim ValidRow As Boolean = IsValidRow(dgvPeriodicVal, e.RowIndex)
                If ValidRow Then
                    Me.dgvPeriodicVal.Item(1, e.RowIndex).Value = Me.txtAgreementNumber.Text
                    Me.dgvPeriodicVal.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub dgvYearlyVal_RowLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvYearlyVal.RowLeave
        Try
            If e.RowIndex >= 0 Then
                Dim AgreeNO As String = ""
                If IsNothing(Me.dgvYearlyVal.Item(1, e.RowIndex).Value) Then
                    AgreeNO = Me.txtAgreementNumber.Text.TrimEnd().TrimStart()
                ElseIf IsDBNull(Me.dgvYearlyVal.Item(1, e.RowIndex).Value) Then
                    AgreeNO = Me.txtAgreementNumber.Text.TrimEnd().TrimStart()
                End If

                Dim ValidRow As Boolean = IsValidRow(dgvYearlyVal, e.RowIndex)
                If ValidRow Then
                    Me.dgvYearlyVal.Item(1, e.RowIndex).Value = Me.txtAgreementNumber.Text
                    Me.dgvYearlyVal.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub
End Class
