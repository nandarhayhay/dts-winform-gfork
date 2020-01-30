Imports System.Threading
Public Class Attachment

    Public Ds As DataSet
    Private MessageSavingSucces As String = ""
    Private Delegate Sub onShowingProgress(ByVal message As String)
    Private Event ShowProgres As onShowingProgress
    Private Delegate Sub onClossingProgress()
    Private Event CloseProgress As onClossingProgress
    Private tickCount As Integer = 0
    Private WithEvents ST As DTSProjects.Progress
    Dim rnd As Random
    Private ResultRandom As Integer
    Private IsSuccesSaving As Boolean
    'PO,agreement,pencapaian
    'metode delete data berdasarkan id dulu trus insert ke database
    Public Event btnSaveClick(ByVal sender As Object, ByVal e As EventArgs)
    Private OriginalWaterMarkText As String = ""
    Private ProcessData As ExecutingData
    Public Event ShowThis()
    Private Ticcount As Integer = 0
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private Enum StatusProgress
        None
        Processing
        Saving
    End Enum

    Private Sub ShowProceed()
        Me.ST = New Progress
        If Me.ProcessData = ExecutingData.Saving Then
            Me.ST.Show("Saving.... please wait")
        Else
            Me.ST.Show("Ordering data.... please wait")
        End If
        Me.ST.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Me.ST.Refresh() : Thread.Sleep(50) : Application.DoEvents()
        End While
        Thread.Sleep(50) : Me.ST.Close() : Me.ST = Nothing
        Me.Ticcount = 0
    End Sub

    Private Enum ExecutingData
        Reordering
        Saving
    End Enum
    Private Sub closeProgres() Handles Me.CloseProgress
        Me.Timer1.Enabled = False
        Me.Timer1.Stop()
        If Not IsNothing(Me.ST) Then

            Me.ST.Close()
            Me.ST = Nothing
            Me.tickCount = 0
        End If
        If Me.ProcessData = ExecutingData.Reordering Then
            Me.Show()
            RaiseEvent ShowThis()
        End If
    End Sub

    Private Overloads Sub ShowProgress(ByVal Message As String) Handles Me.ShowProgres
        Me.ST = New Progress
        Application.DoEvents()
        Me.ST.Show(Message)
    End Sub

    Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.tickCount >= Me.ResultRandom Then
            If Me.IsSuccesSaving = True Then
                RaiseEvent CloseProgress()
                If Me.ProcessData = ExecutingData.Saving Then
                    If Ds.DataSetName = "DSSalesProgramDTS" Then
                        If Me.IsSuccesSaving Then
                            RaiseEvent btnSaveClick(Me.btnSave, New EventArgs())
                            MessageBox.Show(Me.MessageSavingSucces & "Some Saved succesfully" & vbCrLf & "You can check them in form Distributor history / sales program manager", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.MessageSavingSucces = ""
                        End If
                        'If Me.MessageSavingSucces <> "" Then

                        'End If
                    Else
                        MessageBox.Show("Data Saved succesfully" & vbCrLf & "You can check by click View data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        RaiseEvent btnSaveClick(Me.btnSave, New EventArgs())
                    End If
                    Me.IsSuccesSaving = False
                End If
            Else
                Me.ResultRandom += 1
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            If MessageBox.Show("Save data to database ?." & vbCrLf & "Process can not be undone", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.StatProg = StatusProgress.Saving
            Me.ProcessData = ExecutingData.Saving
            IsSuccesSaving = False
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            'RaiseEvent ShowProgres("Executing Query batched.....")
            'Application.DoEvents()
            'Me.rnd = New Random()
            'Me.ResultRandom = Me.rnd.Next(1, 5)
            'Me.Timer1.Enabled = True
            'Me.Timer1.Start()
            'Me.StatProg = StatusProgress.Processing
            Select Case Ds.DataSetName
                Case "DSPO"
                    Using _POKios As New NufarmBussinesRules.Kios.POKios()
                        _POKios.InsertPO(Ds)
                    End Using
                Case "DSSalesProgramDTS"
                    Using BPI As New NufarmBussinesRules.Program.BrandPackInclude()
                        BPI.InsertAgreementDPRD(Ds)
                    End Using
                Case "TargetReaching"
                    Using TGR As New NufarmBussinesRules.Kios.ReachingTarget()
                        TGR.InsertReaching(Ds)
                    End Using
                Case "DsKios"
                    Using _Kios As New NufarmBussinesRules.Kios.ManageKios()
                        _Kios.InsertKios(Ds)
                    End Using
                Case "DSAchievementDPRD"
                    Using TGR As New NufarmBussinesRules.Kios.ReachingTarget()
                        TGR.InsertAchievementDPRDM(Ds)
                    End Using
                Case ""
            End Select
            Me.IsSuccesSaving = True
            Me.StatProg = StatusProgress.None
            If Ds.DataSetName = "DSSalesProgramDTS" Then
                If Me.IsSuccesSaving Then
                    RaiseEvent btnSaveClick(Me.btnSave, New EventArgs())
                    MessageBox.Show(Me.MessageSavingSucces & "Some Saved succesfully" & vbCrLf & "You can check them in form Distributor history / sales program manager", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.MessageSavingSucces = ""
                End If
            Else
                MessageBox.Show("Data Saved succesfully" & vbCrLf & "You can check by click View data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                RaiseEvent btnSaveClick(Me.btnSave, New EventArgs())
            End If

            'RaiseEvent BntClick(Me.Ds)
            'RaiseEvent ShowThis()
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            Me.IsSuccesSaving = False
            If ex.Source = "my Error" Then
                Me.MessageSavingSucces = ex.Message
            End If

            '': RaiseEvent CloseProgress()
            'If ex.Source = "my Error" Then
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DestroyObject(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        Try
            Cursor = Cursors.WaitCursor
            If (Not IsNothing(Ds)) Then
                Ds.Dispose() : Ds = Nothing
            End If
        Catch

        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Function ValueRow(ByVal DV As DataView, ByVal row As DataRowView, ByVal ColName As String) As String
        Dim Val As String = ""
        If row(ColName) Is DBNull.Value Then
            Val = String.Empty
        ElseIf (DV.Table.Columns(ColName).DataType Is Type.GetType("System.Int32")) Then

        ElseIf (DV.Table.Columns(ColName).DataType Is Type.GetType("System.Decimal")) Then
            If DV.Table.Columns(ColName).ColumnName = "Target" Or DV.Table.Columns(ColName).ColumnName = "Quantity" _
            Or DV.Table.Columns(ColName).ColumnName = "TotalActual" Or DV.Table.Columns(ColName).ColumnName = "BonusQty" Then
                Val = String.Format("{0:#,##0.000}", Convert.ToDecimal(row(ColName)))
            ElseIf DV.Table.Columns(ColName).ColumnName = "GivenDisc" _
            Or DV.Table.Columns(ColName).ColumnName = "MaximumDisc" _
          Or DV.Table.Columns(ColName).ColumnName = "VariablePercent" _
          Or DV.Table.Columns(ColName).ColumnName = "Dispro" _
          Or DV.Table.Columns(ColName).ColumnName = "BonusDispro" Then
                Val = String.Format("{0:p}", Convert.ToDecimal(row(ColName)))
            Else
                Val = String.Format("{0:#,##0.00}", Convert.ToDecimal(row(ColName)))
            End If
        ElseIf (DV.Table.Columns(ColName).DataType Is Type.GetType("System.DateTime")) Then
            Val = String.Format("{0:D}", Convert.ToDateTime(row(ColName)))
        Else : Val = row(ColName).ToString()
        End If
        Return Val
    End Function

    Private Sub CreateListViewColumnsPO()
        With Me.ListView1 : .Columns.Clear()
            .Columns.Add("IDKios", 200, HorizontalAlignment.Left)
            .Columns.Add("Kios_Name", 200, HorizontalAlignment.Left)
            .Columns.Add("DistributorID", 120, HorizontalAlignment.Left)
            .Columns.Add("DistributorName", 180, HorizontalAlignment.Left)
            .Columns.Add("PO_Ref_No", 130, HorizontalAlignment.Left)
            .Columns.Add("PO_Date", 130, HorizontalAlignment.Left)
            .Columns.Add("BrandPackName", 210, HorizontalAlignment.Left)
            .Columns.Add("Quantity", 120, HorizontalAlignment.Right)

        End With
    End Sub

    Private Sub CreateColumnsReaching()
        With Me.ListView1 : .Columns.Clear()
            .Columns.Add("ProgramID", 120, HorizontalAlignment.Left)
            .Columns.Add("IDKios", 200, HorizontalAlignment.Left)
            .Columns.Add("BrandID", 120, HorizontalAlignment.Left)
            .Columns.Add("BrandName", 200, HorizontalAlignment.Left)
            .Columns.Add("Skema", 120, HorizontalAlignment.Right) 'Target
            .Columns.Add("Dispro", 120, HorizontalAlignment.Right)
            .Columns.Add("Actual", 120, HorizontalAlignment.Right) 'TotalActual
            .Columns.Add("BonusDispro", 130, HorizontalAlignment.Right)
            .Columns.Add("BonusQty", 120, HorizontalAlignment.Right)
        End With
    End Sub
    Private Sub CreateColumnsAchDPRDM()
        With Me.ListView1 : .Columns.Clear()
            .Columns.Add("ProgramID", 110, HorizontalAlignment.Left)
            .Columns.Add("IDKios", 200, HorizontalAlignment.Left)
            .Columns.Add("Kios_Name", 200, HorizontalAlignment.Left)
            .Columns.Add("BrandID", 100, HorizontalAlignment.Left)
            .Columns.Add("BrandName", 200, HorizontalAlignment.Left)
            .Columns.Add("Target", 120, HorizontalAlignment.Right) 'Target
            .Columns.Add("Actual", 120, HorizontalAlignment.Right) 'TotalActual
            .Columns.Add("BudgetDispro", 120, HorizontalAlignment.Right)
            .Columns.Add("BonusValue", 120, HorizontalAlignment.Right)
        End With
    End Sub
    Private Sub CreateListViewColumnsKios()
        ListView1.Columns.Clear()
        ListView1.Columns.Add("IDKios", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kios_Name", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kios_Owner", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("NPWP", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Address", 170, HorizontalAlignment.Left)
        ListView1.Columns.Add("Province", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("City", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Distict1", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("District2", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("PostalCode", 110, HorizontalAlignment.Left)
        ListView1.Columns.Add("PhoneNumber", 110, HorizontalAlignment.Left)
        ListView1.Columns.Add("ContactMobile", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Email", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Fax", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("CustomerType", 150, HorizontalAlignment.Left)
    End Sub

    Private Sub CreatListViewColumnDSSalesProgramDTS()
        Me.ListView1.Columns.Clear()
        ListView1.Columns.Add("ProgramID", 80, HorizontalAlignment.Left) : ListView1.Columns.Add("StartDate", 90, HorizontalAlignment.Left)
        ListView1.Columns.Add("EndDate", 90, HorizontalAlignment.Left) : ListView1.Columns.Add("Brand ID", 80, HorizontalAlignment.Left)
        ListView1.Columns.Add("Brand Name", 180, HorizontalAlignment.Left) : ListView1.Columns.Add("Distributor ID", 90, HorizontalAlignment.Left)
        ListView1.Columns.Add("Distributor Name", 180, HorizontalAlignment.Left) : ListView1.Columns.Add("Disc Max(%)", 80, HorizontalAlignment.Right)
        ListView1.Columns.Add("Variable %", 100, HorizontalAlignment.Right) : ListView1.Columns.Add("Skema", 80, HorizontalAlignment.Right)
        ListView1.Columns.Add("Given Disc(%)", 100, HorizontalAlignment.Right)
    End Sub

    Private Sub FillListView(ByVal Dv As DataView)
        Me.ListView1.Items.Clear()
        Select Case Ds.DataSetName
            Case "DsKios"
                For i As Integer = 0 To Dv.Count - 1
                    Dim LVItem As New ListViewItem(ValueRow(Dv, Dv(i), Dv.Table.Columns("IDKios").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Kios_Name").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Kios_Owner").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("NPWP").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Address").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("State").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("City").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("District1").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("District2").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("PostalCode").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("PhoneNumber").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("ContactMobile").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Email").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Fax").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("CustomerType").ToString()))
                    ListView1.Items.Add(LVItem)
                Next
            Case "TargetReaching" 'ds.Tables(2)
                For i As Integer = 0 To Dv.Count - 1
                    Dim LVItem As New ListViewItem(ValueRow(Dv, Dv(i), Dv.Table.Columns("ProgramID").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("IDKios").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("BrandID").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("BrandName").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Target").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Dispro").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("TotalActual").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("BonusDispro").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("BonusQty").ToString()))
                    ListView1.Items.Add(LVItem)
                Next
            Case "DSPO" 'ds.Tables(2)
                For i As Integer = 0 To Dv.Count - 1
                    Dim LVItem As New ListViewItem(ValueRow(Dv, Dv(i), Dv.Table.Columns("IDKIos").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Kios_Name").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("DistributorID").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("DistributorName").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("PO_Ref_No").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("PO_Date").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("BrandPackName").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Quantity").ToString()))
                    ListView1.Items.Add(LVItem)
                Next
            Case "DSSalesProgramDTS" 'ds.tables(3)
                For i As Integer = 0 To Dv.Count - 1
                    Dim LVItem As New ListViewItem(ValueRow(Dv, Dv(i), Dv.Table.Columns("SpCode").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("StartDate").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("EndDate").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("BrandCode").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("BrandName").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("DistributorCode").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("DistributorName").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("MaximumDisc").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("VariablePercent").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Target").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("GivenDisc").ToString()))
                    ListView1.Items.Add(LVItem)
                Next
            Case "DSAchievementDPRD"
                For i As Integer = 0 To Dv.Count - 1
                    Dim LVItem As New ListViewItem(ValueRow(Dv, Dv(i), Dv.Table.Columns("SpCode").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("KiosCode").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Kios_Name").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("BrandCode").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("BrandName").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("Target").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("TotalActual").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("DisproValue").ToString()))
                    LVItem.SubItems.Add(ValueRow(Dv, Dv(i), Dv.Table.Columns("BonusValue").ToString()))
                    ListView1.Items.Add(LVItem)
                Next

        End Select
    End Sub

    Private Sub AttachmentDPRD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Not IsNothing(Ds) Then
                Me.Cursor = Cursors.WaitCursor
                Me.Hide() : Application.DoEvents()
                Me.ProcessData = ExecutingData.Reordering
                IsSuccesSaving = False : Me.StatProg = StatusProgress.Processing
                Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
                Me.ThreadProgress.Start()
                Select Case Ds.DataSetName
                    Case "DSPO"
                        Me.txtSearch.WaterMarkText = "Enter PO Number/Kios_Name to search"
                        Me.CreateListViewColumnsPO()
                        Me.FillListView(Ds.Tables(2).DefaultView)
                    Case "DSSalesProgramDTS"
                        Me.txtSearch.WaterMarkText = "Enterd DistributorName to search"
                        Me.CreatListViewColumnDSSalesProgramDTS()
                        Me.FillListView(Ds.Tables(3).DefaultView)
                    Case "TargetReaching"
                        Me.txtSearch.Text = "Enter Kios_Name to search"
                        Me.CreateColumnsReaching()
                        Me.FillListView(Ds.Tables(2).DefaultView)
                    Case "DsKios"
                        Me.txtSearch.WaterMarkText = "Enter Kios_Name to search"
                        Me.CreateListViewColumnsKios()
                        Me.FillListView(Ds.Tables(4).DefaultView)
                    Case "DSAchievementDPRD"
                        Me.txtSearch.WaterMarkText = "Enter Kios_Name to search"
                        Me.CreateColumnsAchDPRDM() : Me.FillListView(Ds.Tables(0).DefaultView)
                End Select
                Me.OriginalWaterMarkText = Me.txtSearch.WaterMarkText
                Me.StatProg = StatusProgress.None
                AddHandler ListView1.ColumnClick, AddressOf ColumnClick
            End If
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.IsSuccesSaving = True : Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ColumnClick(ByVal o As Object, ByVal e As ColumnClickEventArgs)
        ' Set the ListViewItemSorter property to a new ListViewItemComparer 
        ' object. Setting this property immediately sorts the 
        ' ListView using the ListViewItemComparer object.
        ListView1.Sort()
        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub

    Private Sub txtSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Enter

        If Me.txtSearch.WaterMarkText.Equals(Me.OriginalWaterMarkText) Then
            Me.txtSearch.Text = ""
        End If
    End Sub

    Private Sub txtSearch_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.MouseLeave
        Try
            Me.Cursor = Cursors.WaitCursor
            If (Not Me.txtSearch.WaterMarkText.Equals(Me.OriginalWaterMarkText)) Then
                Me.txtSearch_KeyDown(Me.txtSearch, New KeyEventArgs(Keys.Enter))
            Else
                Me.txtSearch.WaterMarkText = Me.OriginalWaterMarkText
            End If
        Catch
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Try
            Me.Cursor = Cursors.WaitCursor
            If (e.KeyCode = Keys.Enter) Then
                If (Not IsNothing(Ds)) Then
                    Dim DV As DataView = Nothing
                    Select Case Ds.DataSetName
                        Case "DSPO"
                            DV = Ds.Tables(2).DefaultView
                            DV.RowFilter = "PO_Ref_NO LIKE '%" + Me.txtSearch.Text + "%' OR Kios_Name LIKE '%" & Me.txtSearch.Text + "%'"
                        Case "DSSalesProgramDTS"
                            DV = Ds.Tables(3).DefaultView
                            DV.RowFilter = "DistributorName LIKE '%" + Me.txtSearch.Text + "%'"
                        Case "TargetReaching"
                            DV = Ds.Tables(2).DefaultView
                            DV.RowFilter = "Kios_Name LIKE '%" + Me.txtSearch.Text + "%'"
                        Case "DsKios"
                            DV = Ds.Tables(4).DefaultView
                            DV.RowFilter = "Kios_Name LIKE '%" + Me.txtSearch.Text + "%'"
                        Case "DSAchievementDPRD"
                            DV = Ds.Tables(0).DefaultView()
                            DV.RowFilter = "Kios_Name LIKE '%" + Me.txtSearch.Text + "%'"
                    End Select
                    If (Not IsNothing(DV)) Then
                        Me.FillListView(DV)
                    End If
                End If
            End If
        Catch
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.tickCount += 1
    End Sub
End Class
'Class ListViewItemComparer
'    Implements IComparer

'    Private col As Integer

'    Public Sub New()
'        col = 0
'    End Sub

'    Public Sub New(ByVal column As Integer)
'        col = column
'    End Sub

'    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
'       Implements IComparer.Compare
'        Return [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
'    End Function
'End Class

