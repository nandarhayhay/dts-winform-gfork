Public Class PriceHistory
    Private IsLoadingCombo As Boolean = True
    Private IsloadedForm As Boolean = True : Private clsPriceHistory As New NufarmBussinesRules.Brandpack.PriceHistory()
    Private PageIndex As Int32 = 0, PageSize As Integer = 0, TotalIndex As Integer = 0, RowCount As Integer = 0
    Private m_DataType As NufarmBussinesRules.common.Helper.DataTypes = NufarmBussinesRules.common.Helper.DataTypes.VarChar
    Private m_Criteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private OriginalCriteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private isUndoingCriteria As Boolean = False : Friend CMain As Main = Nothing
    Private isLoadingRow As Boolean = False
    Private Sub FormatDataGrid()
        For Each col As Janus.Windows.GridEX.GridEXColumn In TManager1.GridEX1.RootTable.Columns
            If col.Type Is Type.GetType("System.Int32") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            ElseIf col.Type Is Type.GetType("System.Decimal") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FormatString = "#,##0.000"
            ElseIf col.Type Is Type.GetType("System.DateTime") Then
                col.FormatString = "dd MMMM yyyy"
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            End If
            With Me.TManager1.GridEX1.RootTable
                .Columns("PRICE_TAG").Visible = False
                .Columns("IDApp").Visible = False

                .Columns("BRANDPACK_ID").Visible = True
                If Me.btnCatPlantation.Checked Then
                    .Columns("DISTRIBUTOR_ID").Visible = False
                    .Columns("PLANTATION_ID").Visible = False
                End If
            End With
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            col.AutoSize()
        Next
        TManager1.GridEX1.RootTable.Columns(0).Visible = False
        TManager1.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        If (Me.FilterEditor1.Visible) And (Not IsNothing(Me.FilterEditor1.SourceControl)) Then
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
        Else
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        End If
    End Sub
    Friend Sub GetData()
        RowCount = 0
        Dim SearchString As String = Me.TManager1.txtSearch.Text, SearchBy As String = Me.TManager1.cbCategory.Text
        If (Me.TManager1.cbCategory.Text = "") Then
            SearchBy = "BRANDPACK_NAME"
        End If
        If Me.TManager1.txtMaxRecord.Text <> "" Then
            If CInt(Me.TManager1.txtMaxRecord.Text) < CInt(Me.TManager1.cbPageSize.Text) Then
                Me.PageSize = CInt(Me.TManager1.txtMaxRecord.Text)
            Else
                Me.PageSize = CInt(Me.TManager1.cbPageSize.Text)
            End If
        Else
            Me.PageSize = CInt(Me.TManager1.cbPageSize.Text)
        End If
        Me.RunQuery(Me.TManager1.txtSearch.Text, SearchBy)
        If TManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub
    Friend Sub LoadData()
        Me.IsloadedForm = True : Me.btnCatFreeOrOther.Checked = True
        Me.PageIndex = 1 : Me.PageSize = 500 : Me.TManager1.cbPageSize.Text = "500"
        Me.TManager1.cbCategory.Items.Clear() : Me.TManager1.btnCriteria.Text = "*.*"
        Me.TManager1.btnCriteria.Text = "*.*" : Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
        Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
        Me.RunQuery("", "BRANDPACK_NAME")
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
        Me.TManager1.lblResult.Text = String.Format("Found {0} Record(s)", RowCount.ToString())
        If (RowCount <> 0) Then
            Me.TotalIndex = RowCount / Me.PageSize
            If (RowCount - (TotalIndex * PageSize) > 0) Then
                TotalIndex += 1
            End If
        End If
        TManager1.lblPosition.Text = String.Format("Page {0} Of {1} page(s)", Me.PageIndex, Me.TotalIndex)
    End Sub
    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String)
        RowCount = 0 : Dim Dv As DataView = Nothing
        If Me.btnCatFreeOrOther.Checked Then
            Dv = Me.clsPriceHistory.PopulateQuery(NufarmBussinesRules.Brandpack.PriceHistory.Category.FreeMarket, _
                     SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)

        ElseIf Me.btnCatPlantation.Checked Then
            Dv = Me.clsPriceHistory.PopulateQuery(NufarmBussinesRules.Brandpack.PriceHistory.Category.SpecialPlantation, _
                             SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
        ElseIf Me.btnGenPrice.Checked Then
            Dv = Me.clsPriceHistory.PopulateQuery(NufarmBussinesRules.Brandpack.PriceHistory.Category.GeneralPricePlantation, _
                             SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
        End If
        If Not IsNothing(Dv) Then
            If (Dv.Count > 0) Then
                Me.TManager1.GridEX1.DataSource = Dv : Me.TManager1.GridEX1.RetrieveStructure()
                Me.FormatDataGrid()
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
    Private Sub ButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles TManager1.ButonClick
        Try
            If Me.IsloadedForm Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            If (TypeOf (sender) Is Button) Then
                Me.PageIndex = 1
            ElseIf (TypeOf (sender) Is Janus.Windows.EditControls.UIButton) Then
                Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                    Case "btnGoFirst" : Me.PageIndex = 1 : Case "btnGoPrevios" : Me.PageIndex -= 1
                    Case "btnNext" : Me.PageIndex += 1 : Case "btnGoLast" : Me.PageIndex = Me.TotalIndex
                End Select
            End If
            Me.GetData() : Me.SetOriginalCriteria()
            Me.IsLoadingCombo = False
            Me.isLoadingRow = False
        Catch ex As Exception
            Me.IsLoadingCombo = False
            Me.isLoadingRow = False
            Me.UndoCriteria() : MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub TManager1_TetxBoxKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TManager1.TetxBoxKeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                Me.Cursor = Cursors.WaitCursor
                Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
            End If
        Catch ex As Exception
            Me.IsLoadingCombo = False
            Me.isLoadingRow = False
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub TManager1_CmbSelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.CmbSelectedIndexChanged
        Try
            If IsLoadingCombo Then : Return : End If
            If IsloadedForm Then : Return : End If
            Me.Cursor = Cursors.WaitCursor : If TManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            Select Case Me.TManager1.cbCategory.Text
                Case "DISTRIBUTOR_NAME", "BRANDPACK_NAME", "PLANTATION"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
                Case "START_DATE"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.DateTime
            End Select
            Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
        Catch ex As Exception
            Me.IsLoadingCombo = False
            Me.isLoadingRow = False
            Me.LogMyEvent(ex.Message, Me.Name + "_TManager1_CmbSelectedIndexChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub UndoCriteria()
        Me.isUndoingCriteria = True
        With Me.TManager1
            Select Case Me.OriginalCriteria
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
                    .btnCriteria.Text = "|*"
                    .btnCriteria.CompareOperator = CompareOperator.BeginWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                    .btnCriteria.Text = "*|"
                    .btnCriteria.CompareOperator = CompareOperator.EndWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                    .btnCriteria.Text = "="
                    .btnCriteria.CompareOperator = CompareOperator.Equal
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                    .btnCriteria.Text = ">"
                    .btnCriteria.CompareOperator = CompareOperator.Greater
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                    .btnCriteria.Text = ">="
                    .btnCriteria.CompareOperator = CompareOperator.GreaterOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.In
                    .btnCriteria.Text = "*|*"
                    .btnCriteria.CompareOperator = CompareOperator.In
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.In
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                    .btnCriteria.Text = "<"
                    .btnCriteria.CompareOperator = CompareOperator.Less
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                    .btnCriteria.Text = "<="
                    .btnCriteria.CompareOperator = CompareOperator.LessOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                    .btnCriteria.Text = "*.*"
                    .btnCriteria.CompareOperator = CompareOperator.Like
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
                    .btnCriteria.Text = "<>"
                    .btnCriteria.CompareOperator = CompareOperator.NotEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
            End Select
        End With
        Me.isUndoingCriteria = False
    End Sub
    Private Sub SetOriginalCriteria()
        Select Case Me.TManager1.btnCriteria.CompareOperator
            Case CompareOperator.BeginWith
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
            Case CompareOperator.EndWith
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
            Case CompareOperator.Equal
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
            Case CompareOperator.Greater
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
            Case CompareOperator.GreaterOrEqual
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
            Case CompareOperator.In
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.In
            Case CompareOperator.Less
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Less
            Case CompareOperator.LessOrEqual
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
            Case CompareOperator.Like
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
            Case CompareOperator.NotEqual
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
        End Select
    End Sub
    Private Sub Criteria_Changed(ByVal sender As Object, ByVal e As EventArgs) Handles TManager1.ChangesCriteria
        Try
            If IsloadedForm Then : Return : End If
            If isUndoingCriteria Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Select Case Me.TManager1.btnCriteria.CompareOperator
                Case CompareOperator.BeginWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
                Case CompareOperator.EndWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                Case CompareOperator.Equal
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                Case CompareOperator.Greater
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                Case CompareOperator.GreaterOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                Case CompareOperator.In
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.In
                Case CompareOperator.Less
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                Case CompareOperator.LessOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                Case CompareOperator.Like
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                Case CompareOperator.NotEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
            End Select
            Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Criteria_Changed")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub TManager1_TextBoxKeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TManager1.TextBoxKeyPress
        If (Not Char.IsDigit(e.KeyChar)) And (Not Char.IsControl(e.KeyChar)) Then
            e.Handled = True : Return
        End If
    End Sub
    Private Sub PriceHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            With Me.TManager1.cbCategory
                .Items.Add("BRANDPACK_NAME")
                .Items.Add("START_DATE")
            End With
        Catch ex As Exception
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.LogMyEvent(ex.Message, Me.Name + "_PriceHistory_Load")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Try
                Me.FormatDataGrid() : Me.IsloadedForm = False : Me.IsLoadingCombo = False
            Catch ex As Exception
            End Try
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnShowFieldChooser"
                    Me.TManager1.GridEX1.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.TManager1.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.TManager1.GridEX1
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
                    Me.FilterEditor1.SourceControl = Me.TManager1.GridEX1
                    Me.TManager1.GridEX1.RemoveFilters()
                    Me.FilterEditor1.Visible = True
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.TManager1.GridEX1.RemoveFilters()
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Case "btnExport"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.TManager1.GridEX1
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnRefresh"
                    Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
            End Select
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            If TypeOf item Is DevComponents.DotNetBar.ButtonItem Then
                CType(item, DevComponents.DotNetBar.ButtonItem).Checked = Not CType(item, DevComponents.DotNetBar.ButtonItem).Checked
            End If

            Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_ItemPanel1_ItemClick")
        Finally
            Me.isLoadingRow = False : Me.IsLoadingCombo = False : Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub TManager1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TManager1.Enter
        Me.AcceptButton = Me.TManager1.btnSearch
    End Sub

    Private Sub ItemPanel1_ButtonCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ButtonCheckedChanged
        Try
            If Me.isLoadingRow Or Me.IsLoadingCombo Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)

            Me.isLoadingRow = True
            For Each b As DevComponents.DotNetBar.BaseItem In Me.ItemPanel1.Items
                If TypeOf b Is DevComponents.DotNetBar.ButtonItem Then
                    If CType(b, DevComponents.DotNetBar.ButtonItem).Name <> item.Name Then
                        CType(b, DevComponents.DotNetBar.ButtonItem).Checked = False
                    End If
                End If
            Next
            Dim c As DevComponents.DotNetBar.ButtonItem = CType(sender, DevComponents.DotNetBar.ButtonItem)
            If c.Checked Then
                Select Case item.Name
                    Case "btnCatPlantation"
                        'If Me.btnCatPlantation.Checked Then 'btnCatPlantation check nya dilepaskan
                        'If Me.btnCatFreeOrOther.Checked Then
                        '    Me.TManager1.Enabled = True
                        '    Me.IsLoadingCombo = True
                        '    With Me.TManager1.cbCategory
                        '        .Text = "" : .Items.Clear()
                        '        .Items.Add("BRANDPACK_NAME")
                        '        .Items.Add("START_DATE")
                        '    End With
                        'Else
                        '    Me.TManager1.GridEX1.SetDataBinding(Nothing, "")
                        '    Me.TManager1.GridEX1.Update() : Me.btnCatPlantation.Checked = False
                        '    With Me.TManager1
                        '        .btnGoFirst.Enabled = False : .btnGoLast.Enabled = False : .btnNext.Enabled = False : .btnGoPrevios.Enabled = False
                        '        .lblResult.Text = ""
                        '        .lblPosition.Text = ""
                        '    End With
                        '    Me.TManager1.Enabled = False
                        '    Return
                        'End If
                        'Me.btnCatPlantation.Checked = False
                        'Else
                        '    Me.TManager1.Enabled = True
                        '    Me.IsLoadingCombo = True
                        '    With Me.TManager1.cbCategory
                        '        .Text = "" : .Items.Clear()
                        '        .Items.Add("BRANDPACK_NAME")
                        '        .Items.Add("DISTRIBUTOR_NAME")
                        '        .Items.Add("PLANTATION_NAME")
                        '        .Items.Add("START_DATE")
                        '    End With
                        '    Me.btnCatPlantation.Checked = True : Me.btnCatFreeOrOther.Checked = False
                        'End If
                        Me.TManager1.Enabled = True
                        Me.IsLoadingCombo = True
                        With Me.TManager1.cbCategory
                            .Text = "" : .Items.Clear()
                            .Items.Add("BRANDPACK_NAME")
                            .Items.Add("DISTRIBUTOR_NAME")
                            .Items.Add("PLANTATION_NAME")
                            .Items.Add("START_DATE")
                        End With
                    Case "btnGenPrice", "btnCatFreeOrOther"
                        Me.TManager1.Enabled = True
                        Me.IsLoadingCombo = True
                        With Me.TManager1.cbCategory
                            .Text = "" : .Items.Clear()
                            .Items.Add("BRANDPACK_NAME")
                            .Items.Add("START_DATE")
                        End With
                        'Case 
                        '    Me.TManager1.Enabled = True
                        '    Me.IsLoadingCombo = True
                        '    With Me.TManager1.cbCategory
                        '        .Text = "" : .Items.Clear()
                        '        .Items.Add("BRANDPACK_NAME")
                        '        .Items.Add("START_DATE")
                        '    End With
                        'If Me.btnCatFreeOrOther.Checked Then
                        '    If Me.btnCatPlantation.Checked Then
                        '        Me.TManager1.Enabled = True
                        '        Me.IsLoadingCombo = True
                        '        With Me.TManager1.cbCategory
                        '            .Text = "" : .Items.Clear()
                        '            .Items.Add("BRANDPACK_NAME")
                        '            .Items.Add("DISTRIBUTOR_NAME")
                        '            .Items.Add("PLANTATION_NAME")
                        '            .Items.Add("START_DATE")
                        '        End With
                        '    Else
                        '        Me.TManager1.GridEX1.SetDataBinding(Nothing, "") : Me.TManager1.GridEX1.Update()
                        '        Me.btnCatFreeOrOther.Checked = False
                        '        With Me.TManager1
                        '            .btnGoFirst.Enabled = False : .btnGoLast.Enabled = False : .btnNext.Enabled = False : .btnGoPrevios.Enabled = False
                        '            .lblResult.Text = ""
                        '            .lblPosition.Text = ""
                        '        End With
                        '        Me.TManager1.Enabled = False
                        '        Return
                        '    End If
                        '    Me.btnCatFreeOrOther.Checked = False
                        'Else
                        '    Me.TManager1.Enabled = True
                        '    Me.IsLoadingCombo = True
                        '    With Me.TManager1.cbCategory
                        '        .Text = "" : .Items.Clear()
                        '        .Items.Add("BRANDPACK_NAME")
                        '        .Items.Add("START_DATE")
                        '    End With
                        '    Me.btnCatFreeOrOther.Checked = True : Me.btnCatPlantation.Checked = False
                        'End If
                End Select
            Else
                With Me.TManager1
                    .GridEX1.SetDataBinding(Nothing, "")
                    .btnGoFirst.Enabled = False
                    .btnGoLast.Enabled = False
                    .btnNext.Enabled = False
                    .btnGoPrevios.Enabled = False
                    .lblResult.Text = ""
                    .lblPosition.Text = ""
                End With
            End If
            Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.isloadingRow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
