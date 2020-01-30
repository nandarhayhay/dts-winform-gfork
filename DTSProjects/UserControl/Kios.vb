Public Class Kios
    Private IsLoadingCombo As Boolean, IsloadingForm As Boolean
    Private PageIndex As Int32 = 0, PageSize As Integer = 0, TotalIndex As Integer = 0, RowCount As Integer = 0
    Private ClsKios As New NufarmBussinesRules.Kios.ManageKios()
    Public ReadOnly Property GridEx() As Janus.Windows.GridEX.GridEX
        Get
            Return TManager1.GridEX1
        End Get
    End Property
    Private Sub FormatDataGrid()
        For Each col As Janus.Windows.GridEX.GridEXColumn In TManager1.GridEX1.RootTable.Columns
            If col.Type Is Type.GetType("System.Int32") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            ElseIf col.Type Is Type.GetType("System.Decimal") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FormatString = "#,##0.000"
            End If
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
        Next
        TManager1.GridEX1.AutoSizeColumns()
        TManager1.GridEX1.RootTable.Columns(0).Visible = False
        TManager1.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    End Sub
    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String)
        RowCount = 0
        Dim Dv As DataView = Nothing
        'If FromDate <> "" Or UntilDate <> "" Then
        '    Dv = Me.clsPOKios.PopulateQuery(SearchString, SearchBy, Me.PageIndex, Me.PageSize, Me.StartYear, Me.EndYear, _
        '    FromDate, UntilDate, RowCount)
        'Else
        '    Dv = Me.ReloadData()
        'End If
        If CInt(Me.TManager1.txtMaxRecord.Text) < CInt(Me.TManager1.cbPageSize.Text) Then
            Me.PageSize = CInt(Me.TManager1.txtMaxRecord.Text)
        End If
        Dv = Me.ClsKios.PopulateQuery(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount)
        If Not IsNothing(Dv) Then
            If (Dv.Count > 0) Then
                Me.TManager1.GridEX1.DataSource = Dv : Me.TManager1.GridEX1.RetrieveStructure() : Me.FormatDataGrid()
            Else
                If Not IsNothing(Me.TManager1.GridEX1.RootTable) Then
                    TManager1.GridEX1.RootTable.Columns.Clear()
                End If
            End If
        Else
            If Not IsNothing(Me.TManager1.GridEX1.RootTable) Then
                Me.TManager1.GridEX1.RootTable.Columns.Clear()
            End If
        End If

    End Sub
    Private Sub GetData()

        RowCount = 0
        Dim SearchString As String = Me.TManager1.txtSearch.Text, SearchBy As String = Me.TManager1.cbCategory.Text
        If (Me.TManager1.cbCategory.Text = "") Then
            SearchBy = "Kios_Name"
        End If
       
        Me.RunQuery(Me.TManager1.txtSearch.Text, SearchBy)
        If TManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub
    Private Sub ButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles TManager1.ButonClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If (TypeOf (sender) Is Button) Then
                Me.PageIndex = 1 : Me.Cursor = Cursors.WaitCursor
            ElseIf (TypeOf (sender) Is Janus.Windows.EditControls.UIButton) Then
                Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                    Case "btnGoFirst" : Me.PageIndex = 1 : Case "btnGoPrevios" : Me.PageIndex -= 1
                    Case "btnNext" : Me.PageIndex += 1 : Case "btnGoLast" : Me.PageIndex = Me.TotalIndex
                End Select
            End If
            If CInt(Me.TManager1.txtMaxRecord.Text) < CInt(Me.TManager1.cbPageSize.Text) Then
                Me.PageSize = CInt(Me.TManager1.txtMaxRecord.Text)
            Else
                Me.PageSize = CInt(Me.TManager1.cbPageSize.Text)
            End If
            Me.GetData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub TextBoxKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TManager1.TetxBoxKeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                Me.Cursor = Cursors.WaitCursor
                Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub TextBoxKeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TManager1.TextBoxKeyPress
        If (Not Char.IsDigit(e.KeyChar)) And (Not Char.IsControl(e.KeyChar)) Then
            e.Handled = True : Return
        End If
    End Sub
    Private Sub ComboBoxSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TManager1.CmbSelectedIndexChanged
        Try
            If IsLoadingCombo Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor : If TManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
        Catch

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub LoadData()
        IsLoadingCombo = True : IsloadingForm = True
        Me.PageIndex = 1 : Me.PageSize = 500 : Me.TManager1.cbPageSize.Text = "500"
        Me.TManager1.cbCategory.Items.Clear()
        Me.RunQuery("", "Kios_Name")
        With Me.TManager1.cbCategory.Items
            .Add("IDKios")
            .Add("Kios_Name")
            '.Add("Kios_Owner")
            .Add("NPWP")
            .Add("TerritoryArea")
            .Add("RegionalArea")
        End With
        If TManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub
    Private Sub SetButtonStatus()
        Me.TManager1.btnGoFirst.Enabled = Me.PageIndex <> 1
        Me.TManager1.btnGoPrevios.Enabled = Me.PageIndex <> 1
        Me.TManager1.btnNext.Enabled = Me.PageIndex <> TotalIndex : If TotalIndex <= 0 Then : TManager1.btnNext.Enabled = False : End If
        Me.TManager1.btnGoLast.Enabled = Me.PageIndex <> TotalIndex : If TotalIndex <= 0 Then : TManager1.btnGoLast.Enabled = False : End If
        If (TManager1.GridEX1.DataSource Is Nothing) Then
            TManager1.btnGoFirst.Enabled = False : TManager1.btnGoLast.Enabled = False
            TManager1.btnNext.Enabled = False : TManager1.btnGoLast.Enabled = False
        ElseIf TManager1.GridEX1.RecordCount <= 0 Then
            TManager1.btnGoFirst.Enabled = False : TManager1.btnGoLast.Enabled = False
            TManager1.btnNext.Enabled = False : TManager1.btnGoLast.Enabled = False
        ElseIf TManager1.GridEX1.RecordCount <= 0 Then
        End If
    End Sub
    Private Sub SetStatusRecord()
        Me.TotalIndex = 0
        If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
            RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
        End If
        Me.TManager1.lblResult.Text = String.Format("Found {0} Record(s)", RowCount.ToString())
        If (RowCount <> 0) Then
            Me.TotalIndex = RowCount / Me.PageSize
            If (RowCount - (TotalIndex * PageSize) > 0) Then
                TotalIndex += 1
            End If
        End If
        TManager1.lblPosition.Text = String.Format("Page {0} Of {1} page(s)", Me.PageIndex, Me.TotalIndex)
    End Sub

    Private Sub Kios_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IsLoadingCombo = False : IsloadingForm = False : Cursor = Cursors.Default
        End Try
    End Sub
End Class
