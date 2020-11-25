Imports System.Threading
Public Class TargetAgreement4FM
    Private m_cls_agree As NufarmBussinesRules.DistributorAgreement.Agreement
    'Private m_cObject As New cObject
    Private DV As DataView = Nothing
    Private HasLoadForm As Boolean = False
    Private Ticcount As Integer = 0
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private LD As Loading
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
    End Sub
    Private ReadOnly Property cls_agee() As NufarmBussinesRules.DistributorAgreement.Agreement
        Get
            If m_cls_agree Is Nothing Then
                m_cls_agree = New NufarmBussinesRules.DistributorAgreement.Agreement()
            End If
            Return m_cls_agree
        End Get
    End Property
    'Private Function hasChangedData() As Boolean

    '    If New Date(Me.dtPicModFromDate.Value.Year, Me.dtPicModFromDate.Value.Month, Me.dtPicModFromDate.Value.Day) <> Me.m_cObject.StartDate Then
    '        Return True
    '    ElseIf New Date(Me.dtPicModUntilDate.Value.Year, Me.dtPicModUntilDate.Value.Month, Me.dtPicModUntilDate.Value.Day) <> Me.m_cObject.StartDate Then
    '        Return True
    '    ElseIf Me.m_cObject.DPDType <> Me.cmbDPDType.Text Then
    '        Return True
    '    End If
    '    Return False
    'End Function
    Friend Sub ReloadData()
        Me.StatProg = StatusProgress.Processing
        Me.ThreadProgress = New Thread(AddressOf ShowProceed)
        Me.ThreadProgress.Start()
        Dim DV As DataView = Me.cls_agee.getTargetDPD(Me.dtPicModFromDate.Value, Me.dtPicModUntilDate.Value, Me.cmbDPDType.Text)
        Me.GridEX1.DataSource = DV
        If Not IsNothing(DV) Then
            With Me.GridEX1
                .RetrieveStructure()
                For Each col As Janus.Windows.GridEX.GridEXColumn In .RootTable.Columns
                    If col.DataMember.Contains("ID") Then
                        col.Visible = False
                        col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    ElseIf col.DataMember = "PROD_CATEGORY" Then
                    ElseIf col.DataMember = "PS_GROUP" Then

                    ElseIf col.Type Is Type.GetType("System.Decimal") Or col.Type Is Type.GetType("System.Int32") Then
                        If col.DataMember.ToString().Contains("VALUE") Then
                            col.FormatString = "#,##0.00"
                            col.TotalFormatString = "#,##0.00"
                            col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        ElseIf col.DataMember.Contains("TARGET") Then
                            col.FormatString = "#,##0.000"
                            col.TotalFormatString = "#,##0.000"
                        End If
                        col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        col.ColumnType = Janus.Windows.GridEX.ColumnType.Text
                        col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    ElseIf col.Type Is Type.GetType("System.DateTime") Then
                        col.FormatString = "dd MMMM yyyy"
                        col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
                        col.ColumnType = Janus.Windows.GridEX.ColumnType.Text
                    ElseIf col.Type Is Type.GetType("System.Boolean") Then
                        col.ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox
                        col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
                    Else
                        col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    End If
                Next
                ''bin group
                .RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(.RootTable.Columns("TERRITORY_AREA")))
                .RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(.RootTable.Columns("DISTRIBUTOR_NAME")))
                If .RootTable.Columns.Contains("PROD_CATEGORY") Then
                    .RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(.RootTable.Columns("PROD_CATEGORY")))
                    .RootTable.Columns("PROD_CATEGORY").Visible = False
                    .RootTable.Groups(2).GroupPrefix = ""
                End If
                If .RootTable.Columns.Contains("PS_GROUP") Then
                    .RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(.RootTable.Columns("PS_GROUP")))
                    .RootTable.Columns("PS_GROUP").Visible = False
                    .RootTable.Groups(2).GroupPrefix = ""
                End If
                .RootTable.Groups(0).GroupPrefix = ""
                .RootTable.Groups(1).GroupPrefix = ""

                .GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
                .GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
                .GroupByBoxVisible = False
                .TableHeaders = Janus.Windows.GridEX.InheritableBoolean.True
                .TableHeaderFormatStyle.ForeColor = Color.Maroon
                .TableHeaderFormatStyle.FontSize = 16
                .TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
                .TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                .RowHeaders = Janus.Windows.GridEX.InheritableBoolean.False
                .AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.True
                .AllowColumnDrag = True
                .FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                .FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
                .AutoSizeColumns()
            End With
        End If
        Me.StatProg = StatusProgress.None
        Cursor = Cursors.Default
    End Sub
    Private Sub btnAplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyRange.Click
        Try
            Cursor = Cursors.WaitCursor
            ReloadData()
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            MessageBox.Show(ex.Message)
            Cursor = Cursors.Default
        End Try
    End Sub
    'Private Class cObject

    '    Public StartDate As Date = NufarmBussinesRules.SharedClass.ServerDate
    '    Public EndDate As Date
    '    Public DPDType As String = "Semester"
    'End Class

    Private Sub TargetAgreement4FM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim curStartDate As Date = NufarmBussinesRules.SharedClass.ServerDate
            Dim curEndDate As Date = NufarmBussinesRules.SharedClass.ServerDate
            Me.cls_agee.GetCurrentAgreement(curStartDate, curEndDate, True)
            'Me.m_cObject = New cObject()
            'Me.m_cObject.StartDate = curStartDate
            'Me.m_cObject.EndDate = curEndDate
            Me.dtPicModFromDate.Value = curStartDate
            Me.dtPicModUntilDate.Value = curEndDate
            Me.cmbDPDType.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
End Class
