Public Class ReachingTargetKios
    Private IsLoadingCombo As Boolean = False
    Private clsTargetKios As New NufarmBussinesRules.Kios.ReachingTarget
    Private ListProgram As New List(Of String)
    Private DescriptionSP As New DataTable()
    Public ReadOnly Property GridEx() As Janus.Windows.GridEX.GridEX
        Get
            Return Me.GridEX1
        End Get
    End Property
    Private Sub GetData(ByVal SPCode As String, ByVal TypeApp As String)
        DescriptionSP = New DataTable() : DescriptionSP.Clear()
        Dim DV As DataView = Me.clsTargetKios.GetTargetReaching(SPCode, TypeApp, DescriptionSP)
        If (Not IsNothing(DV)) Then
            Me.ClearText()
            If DV.Count > 0 Then
                If DescriptionSP.Rows.Count > 0 Then
                    Me.lblStartDate.Text = Convert.ToDateTime(DescriptionSP.Rows(0)("START_DATE")).ToLongDateString()
                    Me.lblEndDate.Text = Convert.ToDateTime(DescriptionSP.Rows(0)("END_DATE")).ToLongDateString()
                    Me.lblProgramName.Text = DescriptionSP.Rows(0)("PROGRAM_NAME").ToString()
                    Me.lblCreatedBy.Text = DescriptionSP.Rows(0)("CreatedBy").ToString()
                End If
            End If
        Else
            Me.lblStartDate.Text = "" : Me.lblEndDate.Text = "" : Me.lblProgramName.Text = ""
            Me.lblCreatedBy.Text = ""
        End If
        Me.GridEX1.SetDataBinding(DV, "") : Me.GridEX1.RetrieveStructure()
        If Me.rdbDPRDM.Checked Then
            Me.GridEX1.RootTable.Caption = " ACHIEVEMENT DPRDM  " & SPCode & ", PERIODE  " & lblStartDate.Text & " - " & Me.lblEndDate.Text _
            & " PROPOSED " & lblCreatedBy.Text
        Else
            Me.GridEX1.RootTable.Caption = " ACHIEVEMENT DPRDS  " & SPCode & ", PERIODE  " & lblStartDate.Text & " - " & Me.lblEndDate.Text _
            & " FROM " & lblCreatedBy.Text
        End If : Me.CreateGroup()
        Me.FormatDataGrid()
        'Me.clsTargetKios.GetPOTargetReaching(Convert.ToDateTime(Me.lblStartDate.Text), Convert.ToDateTime(Me.lblEndDate.Text), True, New List(Of String), New DataView(), New DataView())
    End Sub
    Private Sub CreateGroup()
        If Not IsNothing(Me.GridEX1.DataSource) Then
            Me.GridEX1.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.GridEX1.RootTable.Columns("TERRITORY_AREA")))
            Me.GridEX1.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.GridEX1.RootTable.Columns("IDKios")))
            Me.GridEX1.RootTable.SortKeys.Add(New Janus.Windows.GridEX.GridEXSortKey(Me.GridEX1.RootTable.Columns("BRAND_NAME"), Janus.Windows.GridEX.SortOrder.Descending))
            Me.GridEX1.RootTable.Groups(Me.GridEX1.RootTable.Columns("TERRITORY_AREA")).GroupPrefix = ""
            Me.GridEX1.RootTable.Groups(Me.GridEX1.RootTable.Columns("IDKios")).GroupPrefix = ""
            Me.GridEX1.RootTable.Columns("TERRITORY_AREA").Visible = False
            Me.GridEX1.RootTable.Columns("IDKios").Visible = False
        End If
    End Sub
    Private Sub FormatDataGrid()
        If Not IsNothing(Me.GridEX1.DataSource) Then
            Me.GridEX1.RootTable.Columns("TARGET").FormatString = "#,##0.000"
            Me.GridEX1.RootTable.Columns("TOTAL_ACTUAL").FormatString = "#,##0.000"
            Me.GridEX1.RootTable.Columns("%ACHIEVEMENT").FormatString = "p"
            If Me.rdbDPRDS.Checked Then
                Me.GridEX1.RootTable.Columns("SCHEMA_DISPRO").FormatString = "p"
                Me.GridEX1.RootTable.Columns("% BONUS").FormatString = "P"
                Me.GridEX1.RootTable.Columns("BONUS_QTY").FormatString = "#,##0.000"
            Else
                Me.GridEX1.RootTable.Columns("SCHEMA_DISPRO").FormatString = "#,##0.00"
                Me.GridEX1.RootTable.Columns("BONUS_VALUE").FormatString = "#,##0.00"
            End If
        End If
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If col.Type Is Type.GetType("System.Decimal") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf col.Type Is Type.GetType("System.String") Then
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf col.Type Is Type.GetType("System.DateTime") Then
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            End If
        Next
        Me.GridEX1.AutoSizeColumns()
        Me.GridEX1.GridLines = Janus.Windows.GridEX.GridLines.Horizontal
    End Sub
    Private Sub FillCombo(ByVal SearchString As String, ByVal typeApp As String)
        Me.IsLoadingCombo = True
        Me.mcbProgramID.Text = String.Empty
        Me.mcbProgramID.SetDataBinding(Me.clsTargetKios.getSPAndCreaterSP(typeApp, SearchString), "")
        Me.mcbProgramID.DisplayMember = "PROGRAM_ID" : Me.mcbProgramID.ValueMember = "PROGRAM_ID"
        Me.mcbProgramID.DropDownList.RetrieveStructure()
        Me.mcbProgramID.DroppedDown = True : Me.mcbProgramID.DropDownList.AutoSizeColumns() : Me.mcbProgramID.DroppedDown = False
        Me.IsLoadingCombo = False
    End Sub
    Private Sub ClearText()
        Me.lblCreatedBy.Text = ""
        Me.lblEndDate.Text = ""
        Me.lblProgramName.Text = ""
        Me.lblStartDate.Text = ""
    End Sub
    'Private Sub mcbProgramID_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbProgramID.ValueChanged
    '    Try
    '        If (IsLoadingCombo) Then : Return : End If
    '        If (Me.mcbProgramID.SelectedIndex = -1) Then : Return : End If
    '        Me.Cursor = Cursors.WaitCursor
    '        Dim TypeApp As String = ""
    '        If Me.rdbDPRDS.Checked Then : TypeApp = "DPRDS"
    '        ElseIf Me.rdbDPRDM.Checked Then : TypeApp = "DPRDM"
    '        End If
    '        Me.GetData(Me.mcbProgramID.Value.ToString(), TypeApp)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Finally
    '        Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub btnSearchProgramID_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchProgramID.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim typeApp As String = ""
            If Me.rdbDPRDM.Checked Then : typeApp = "DPRDM"
            Else : typeApp = "DPRDS"
            End If
            Me.FillCombo(RTrim(Me.mcbProgramID.Text), typeApp)
            Dim itemCount As Integer = Me.mcbProgramID.DropDownList.RecordCount
            MessageBox.Show(itemCount.ToString() & " item(s) found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally : Me.Cursor = Cursors.Default : End Try
    End Sub

    Private Sub ReachingTargetKios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            'Me.FillCombo(String.Empty, "DPRDS")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally : Me.Cursor = Cursors.Default : End Try
    End Sub

    
    Private Sub rdbDPRDS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDPRDS.CheckedChanged
        Try
            If (rdbDPRDS.Checked) Then
                Cursor = Cursors.WaitCursor
                Me.GridEX1.DataSource = Nothing : Me.GridEX1.RetrieveStructure()
                Me.FillCombo(String.Empty, "DPRDS") : Me.ClearText()
            End If
        Catch ex As Exception
            Me.IsLoadingCombo = False
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.ClearText()
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbDPRDM_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDPRDM.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbDPRDM.Checked Then
                Me.GridEX1.DataSource = Nothing : Me.GridEX1.RetrieveStructure()
                Me.FillCombo(String.Empty, "DPRDM")
                Me.ClearText()
            End If
        Catch ex As Exception
            Me.IsLoadingCombo = False
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.ClearText()
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mcbProgramID_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbProgramID.ValueChanged
        Try
            If (IsLoadingCombo) Then : Return : End If
            If (Me.mcbProgramID.SelectedIndex = -1) Then : ClearText() : Return : End If
            If Me.rdbDPRDM.Checked = False And Me.rdbDPRDS.Checked = False Then : ClearText() : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Dim typeApp As String = ""
            If Me.rdbDPRDM.Checked Then : typeApp = "DPRDM"
            Else : typeApp = "DPRDS"
            End If
            Me.GetData(Me.mcbProgramID.Value.ToString(), typeApp)
            'getdata
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.ClearText()
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
End Class
