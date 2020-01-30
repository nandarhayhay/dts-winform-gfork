Public Class POManager
    Private clsPOKios As New NufarmBussinesRules.Kios.POKios
    Private ListInteger As New List(Of Integer)
    Private IsLoadingCombo As Boolean, IsloadingForm As Boolean
    Private PageIndex As Int32 = 0, PageSize As Integer = 0, TotalIndex As Integer = 0
    Private StartYear As Int32 = 0, EndYear As Integer = 0, RowCount As Integer = 0

    Public ReadOnly Property grid() As Janus.Windows.GridEX.GridEX
        Get
            Return Manager1.GridEX1
        End Get

    End Property

    Private Sub FormatDataGrid()
        For Each col As Janus.Windows.GridEX.GridEXColumn In Manager1.GridEX1.RootTable.Columns
            If col.Type Is Type.GetType("System.Int32") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                If col.Key = "IDApp" Then
                    col.Visible = False : col.FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    col.ShowInFieldChooser = False
                End If
            ElseIf col.Type Is Type.GetType("System.Decimal") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FormatString = "#,##0.000"
            ElseIf col.Key = "IDApp" Or col.Key = "FkCode" Then
                col.Visible = False : col.FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                col.ShowInFieldChooser = False
            End If
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            'col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            If col.Type Is Type.GetType("System.String") Or col.Type Is Type.GetType("System.Decimal") Then
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf col.Type Is Type.GetType("System.DateTime") Then
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
                col.EditType = Janus.Windows.GridEX.EditType.TextBox
            End If

        Next
        Manager1.GridEX1.AutoSizeColumns()

    End Sub

    Private Sub FillcomboDay(ByVal cmb As ComboBox)
        ''tentukan apakah leafyear / bukan
        'januari sampai 31,februari lepyear,maret 31,april 30,mei 31,juni 30,juli 31,agustus 31,
        'september 30,oktober 31,november 30,desember 31
        If (cmb.Name = "cmbFromMonth") Then
            Manager1.cmbFromDate.Items.Clear()
            Select Case cmb.Text
                Case "01"
                    For i As Integer = 1 To 31
                        Manager1.cmbFromDate.Items.Add(i)
                    Next
                Case "02"
                    If (DateTime.IsLeapYear(NufarmBussinesRules.SharedClass.ServerDate.Year)) Then
                        ''jika tahun kabisat
                        For i As Integer = 1 To 28
                            Manager1.cmbFromDate.Items.Add(i)
                        Next
                    Else
                        For i As Integer = 1 To 29
                            Manager1.cmbFromDate.Items.Add(i)
                        Next
                    End If
                Case "03"
                    For i As Integer = 1 To 31
                        Manager1.cmbFromDate.Items.Add(i)
                    Next
                Case "04"
                    For i As Integer = 1 To 30
                        Manager1.cmbFromDate.Items.Add(i)
                    Next
                Case "05"
                    For i As Integer = 1 To 31
                        Manager1.cmbFromDate.Items.Add(i)
                    Next
                Case "06"
                    For i As Integer = 1 To 30
                        Manager1.cmbFromDate.Items.Add(i)
                    Next
                Case "07"
                    For i As Integer = 1 To 31
                        Manager1.cmbFromDate.Items.Add(i)
                    Next
                Case "08"
                    For i As Integer = 1 To 31
                        Manager1.cmbFromDate.Items.Add(i)
                    Next
                Case "09"
                    For i As Integer = 1 To 30
                        Manager1.cmbFromDate.Items.Add(i)
                    Next
                Case "10"
                    For i As Integer = 1 To 31
                        Manager1.cmbFromDate.Items.Add(i)
                    Next
                Case "11"
                    For i As Integer = 1 To 30
                        Manager1.cmbFromDate.Items.Add(i)
                    Next
                Case "12"
                    For i As Integer = 1 To 31
                        Manager1.cmbFromDate.Items.Add(i)
                    Next
            End Select

        ElseIf (cmb.Name = "cmbUntilMonth") Then
            Manager1.cmbUntilDate.Items.Clear()
            Select Case cmb.Text
                Case "01"
                    For i As Integer = 1 To 31
                        Manager1.cmbUntilDate.Items.Add(i)
                    Next
                Case "02"
                    If (DateTime.IsLeapYear(NufarmBussinesRules.SharedClass.ServerDate.Year)) Then
                        ''jika tahun kabisat
                        For i As Integer = 1 To 28
                            Manager1.cmbUntilDate.Items.Add(i)
                        Next
                    Else
                        For i As Integer = 1 To 29
                            Manager1.cmbUntilDate.Items.Add(i)
                        Next
                    End If
                Case "03"
                    For i As Integer = 1 To 31
                        Manager1.cmbUntilDate.Items.Add(i)
                    Next
                Case "04"
                    For i As Integer = 1 To 30
                        Manager1.cmbUntilDate.Items.Add(i)
                    Next
                Case "05"
                    For i As Integer = 1 To 31
                        Manager1.cmbUntilDate.Items.Add(i)
                    Next
                Case "06"
                    For i As Integer = 1 To 30
                        Manager1.cmbUntilDate.Items.Add(i)
                    Next
                Case "07"
                    For i As Integer = 1 To 31
                        Manager1.cmbUntilDate.Items.Add(i)
                    Next
                Case "08"
                    For i As Integer = 1 To 31
                        Manager1.cmbUntilDate.Items.Add(i)
                    Next
                Case "09"
                    For i As Integer = 1 To 30
                        Manager1.cmbUntilDate.Items.Add(i)
                    Next
                Case "10"
                    For i As Integer = 1 To 31
                        Manager1.cmbUntilDate.Items.Add(i)
                    Next
                Case "11"
                    For i As Integer = 1 To 30
                        Manager1.cmbUntilDate.Items.Add(i)
                    Next
                Case "12"
                    For i As Integer = 1 To 31
                        Manager1.cmbUntilDate.Items.Add(i)
                    Next
            End Select

        End If
    End Sub

    'Private sub DeleteRow(byval sender as Object,ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) handles Manager1.G
    Private Sub GetData()

        'RowCount = 0
        Dim SearchString As String = Me.Manager1.txtSearch.Text, SearchBy As String = Me.Manager1.cbCategory.Text
        If (Me.Manager1.cbCategory.Text = "") Then
            SearchBy = "PO_REF_NO"
        End If
        Me.RunQuery(Me.Manager1.txtSearch.Text, SearchBy)
        If Manager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.Manager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.Manager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub

    Private Function ReloadData() As DataView
        If Me.Manager1.cmbUntilYear.Text = "" Or Me.Manager1.cmbFromYear.Text = "" Then
            Throw New Exception("Until year & From year must be specified")
        End If
        StartYear = CInt(Me.Manager1.cmbFromYear.Text) : EndYear = CInt(Me.Manager1.cmbUntilYear.Text)
        If (Me.Manager1.cbPageSize.Text = "") Then : Throw New Exception("PageSize must be specified") : End If
        Dim SearchBy As String = Manager1.cbCategory.Text, SearchString As String = Manager1.txtSearch.Text
        If (SearchBy = "") Then : SearchBy = "PO_REF_NO" : End If
        Dim DV As DataView = Me.clsPOKios.PopulateQuery(SearchString, SearchBy, Me.PageIndex, Me.PageSize, StartYear, _
         EndYear, RowCount)
        Return DV
    End Function

    Private Sub LoadData()
        'ambil data tahun sekarang kalau mungkin data sudah ada
        'page index di set jadi satu
        IsloadingForm = True : IsLoadingCombo = False
        'set maxrecord
        Me.Manager1.cmbFromMonth.SelectedIndex = -1 : Me.Manager1.cmbUntilMonth.SelectedIndex = -1
        Me.PageIndex = 1 : Me.PageSize = 50 : Manager1.cbCategory.Items.Clear() : Manager1.cbPageSize.Text = "50"
        Me.clsPOKios = New NufarmBussinesRules.Kios.POKios()
        Dim TblPO As DataTable = Me.clsPOKios.GetData(1, 50, Me.ListInteger, StartYear, EndYear, RowCount)
        Manager1.cmbUntilYear.Items.Clear() : Manager1.cmbFromYear.Items.Clear()
        If Not IsNothing(TblPO) Then
            Me.Manager1.GridEX1.DataSource = TblPO.DefaultView : Me.Manager1.GridEX1.RetrieveStructure() : Me.FormatDataGrid()
            Manager1.cbCategory.Items.AddRange(New Object() {"PO_REF_NO", "DISTRIBUTOR_NAME", "KIOS_NAME", "BRANDPACK_NAME"})
            'Me.SetStatusRecord()
            'Me.SetButtonStatus()
        End If
        If ListInteger.Count > 0 Then
            For i As Integer = 0 To ListInteger.Count - 1
                Manager1.cmbFromYear.Items.Add(ListInteger(i))
                Manager1.cmbUntilYear.Items.Add(ListInteger(i))
            Next
            If StartYear <> 0 Then
                Me.Manager1.cmbFromYear.SelectedIndex = Me.Manager1.cmbFromYear.Items.IndexOf(StartYear)
            End If
            If EndYear <> 0 Then
                Me.Manager1.cmbUntilYear.SelectedIndex = Me.Manager1.cmbUntilYear.Items.IndexOf(EndYear)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub

    Private Sub SetButtonStatus()
        Me.Manager1.btnGoFirst.Enabled = Me.PageIndex <> 1
        Me.Manager1.btnGoPrevios.Enabled = Me.PageIndex <> 1
        Me.Manager1.btnNext.Enabled = Me.PageIndex <> TotalIndex : If TotalIndex <= 0 Then : Manager1.btnNext.Enabled = False : End If
        Me.Manager1.btnGoLast.Enabled = Me.PageIndex <> TotalIndex : If TotalIndex <= 0 Then : Manager1.btnGoLast.Enabled = False : End If
        If (Manager1.GridEX1.DataSource Is Nothing) Then
            Manager1.btnGoFirst.Enabled = False : Manager1.btnGoLast.Enabled = False
            Manager1.btnNext.Enabled = False : Manager1.btnGoLast.Enabled = False
        ElseIf Manager1.GridEX1.RecordCount <= 0 Then
            Manager1.btnGoFirst.Enabled = False : Manager1.btnGoLast.Enabled = False
            Manager1.btnNext.Enabled = False : Manager1.btnGoLast.Enabled = False
        ElseIf Manager1.GridEX1.RecordCount <= 0 Then
        End If
    End Sub

    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String)
        Dim FromDate As String = Me.Manager1.generateFromDate() : Dim UntilDate As String = Manager1.generateUntilDate()
        'RowCount = 0
        Dim Dv As DataView = Nothing
        If (FromDate <> "" And UntilDate <> "") Then
            StartYear = CInt(Me.Manager1.cmbFromYear.Text) : EndYear = CInt(Me.Manager1.cmbUntilYear.Text)
            Dv = Me.clsPOKios.PopulateQuery(SearchString, SearchBy, Me.PageIndex, Me.PageSize, Me.StartYear, Me.EndYear, _
            FromDate, UntilDate, RowCount)
        Else
            Dv = Me.ReloadData()
        End If
        If Not IsNothing(Dv) Then
            If (Dv.Count > 0) Then
                Me.Manager1.GridEX1.DataSource = Dv : Me.Manager1.GridEX1.RetrieveStructure() : Me.FormatDataGrid()
            Else
                If Not IsNothing(Me.Manager1.GridEX1.RootTable) Then
                    Manager1.GridEX1.RootTable.Columns.Clear()
                End If
            End If
        Else
            If Not IsNothing(Me.Manager1.GridEX1.RootTable) Then
                Me.Manager1.GridEX1.RootTable.Columns.Clear()
            End If
        End If
    End Sub

    Private Sub SetStatusRecord()
        Me.TotalIndex = 0
        If RowCount > CInt(Me.Manager1.txtMaxRecord.Text) Then
            RowCount = CInt(Me.Manager1.txtMaxRecord.Text)
        End If
        Me.Manager1.lblResult.Text = String.Format("Found {0} Record(s)", RowCount.ToString())
        If (RowCount <> 0) Then
            Me.TotalIndex = RowCount / Me.PageSize
            If (RowCount - (TotalIndex * PageSize) > 0) Then
                TotalIndex += 1
            End If

        End If
        Manager1.lblPosition.Text = String.Format("Page {0} Of {1} page(s)", Me.PageIndex, Me.TotalIndex)
    End Sub

    Private Sub PopulateCombo()
        Me.ListInteger = Me.clsPOKios.GetListYear()
        Me.Manager1.cmbFromYear.Items.Clear()
        Me.IsLoadingCombo = True
        For i As Integer = 0 To ListInteger.Count
            Me.Manager1.cmbFromYear.Items.Add(ListInteger(i).ToString())
        Next i
        Me.Manager1.cmbUntilYear.Items.Clear()
        For i As Integer = 0 To ListInteger.Count
            Me.Manager1.cmbUntilYear.Items.Add(ListInteger(i).ToString())
        Next i

    End Sub

    Private Sub dtPic_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Manager1.DtPicFromValueChanged
        Try
            If Me.IsloadingForm Then : Return : End If
            If Me.IsLoadingCombo Then : Return : End If
            If (CType(sender, ComboBox).SelectedIndex = -1) Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, ComboBox).Name
                Case "cmbFromMonth"
                    Me.FillcomboDay(CType(sender, ComboBox))
                Case "cmbUntilMonth"
                    Me.FillcomboDay(CType(sender, ComboBox))
            End Select
            Me.ButtonClick(Me.Manager1.btnSearch, New EventArgs())
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtMaxRecord_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Manager1.TetxBoxKeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                Me.Cursor = Cursors.WaitCursor
                Me.ButtonClick(Me.Manager1.btnSearch, New EventArgs())
            End If
        Catch

        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub TextBoxKeyDown(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles Manager1.TextBoxKeyPress
        If (Not Char.IsDigit(e.KeyChar)) And (Not Char.IsControl(e.KeyChar)) Then
            e.Handled = True : Return
        End If
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Manager1.CmbSelectedIndexChanged
        Try
            If IsLoadingCombo Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor : If Manager1.GridEX1.DataSource Is Nothing Then : Return : End If
            'If (CType(sender, ComboBox).Name = "cmbFromMonth") Or (CType(sender, ComboBox).Name = "cmbUntlMonth") Then
            '    Me.FillcomboDay(CType(sender, ComboBox))
            'End If
            Me.ButtonClick(Me.Manager1.btnSearch, New EventArgs())
        Catch

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DeleteRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles Manager1.DeleteRow
        Try
            Me.Cursor = Cursors.WaitCursor
            e.Cancel = True
            Dim PageIndex As Integer = Me.PageIndex
            Me.clsPOKios.DeletePOKios(Me.Manager1.GridEX1.GetValue("FkCode").ToString(), _
            Me.Manager1.GridEX1.GetValue("PO_REF_NO").ToString(), CInt(Me.Manager1.cmbFromYear.Text), CInt(Me.Manager1.cmbUntilYear.Text))
            If PageIndex > 1 Then
                If Me.Manager1.GridEX1.RecordCount = 1 Then
                    PageIndex -= 1
                    If CInt(Me.Manager1.txtMaxRecord.Text) < CInt(Me.Manager1.cbPageSize.Text) Then
                        Me.PageSize = CInt(Me.Manager1.txtMaxRecord.Text)
                    Else
                        Me.PageSize = CInt(Me.Manager1.cbPageSize.Text)
                    End If
                    Me.GetData()
                End If
            End If
            e.Cancel = False
            Me.Manager1.GridEX1.UpdateData()
            MessageBox.Show("For some reasons after you delete any of data(s)" & vbCrLf & "You and all associte user(s) who access the same data(s)" & vbCrLf & "Must  relogin to take effect", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'int PageIndex = this.tManager1.QueryComposite.PageIndex;
            '        this.tManager1.QueryComposite.PageSize = Convert.ToInt32(tManager1.cbPageSize.Text);
            'If (PageIndex > 1) Then
            '        {  if (tManager1.dataGridView1.RowCount == 1) { PageIndex -= 1; }}

            '        this.tManager1.QueryComposite.PageIndex = PageIndex;
            '        _AgreementDataAcces.Delete(Convert.ToInt32(tManager1.dataGridView1[0, tManager1.dataGridView1.CurrentRow.Index].Value.ToString()));
            '        isLoadingRow = true;
            '        this.getData();
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            e.Cancel = True
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Friend Sub ButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles Manager1.ButonClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If (TypeOf (sender) Is Button) Then
                Me.PageIndex = 1
            ElseIf (TypeOf (sender) Is Janus.Windows.EditControls.UIButton) Then
                Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                    Case "btnGoFirst" : Me.PageIndex = 1 : Case "btnGoPrevios" : Me.PageIndex -= 1
                    Case "btnNext" : Me.PageIndex += 1 : Case "btnGoLast" : Me.PageIndex = Me.TotalIndex
                End Select
            End If
            If CInt(Me.Manager1.txtMaxRecord.Text) < CInt(Me.Manager1.cbPageSize.Text) Then
                Me.PageSize = CInt(Me.Manager1.txtMaxRecord.Text)
            Else
                Me.PageSize = CInt(Me.Manager1.cbPageSize.Text)
            End If
            Me.GetData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally : Me.Cursor = Cursors.Default : End Try

    End Sub

    Private Sub POManager_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            Me.Cursor = Cursors.WaitCursor
            ''drop table di tempdb
            Me.clsPOKios.DisposeTempDB()
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub POManager_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            'Me.PopulateCombo()
            Me.LoadData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.IsLoadingCombo = False : Me.IsloadingForm = False : Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
