Public Class PlantationGroup
    Private isLoadingRow As Boolean = False
    Private clsPlantation As New NufarmBussinesRules.Plantation.Plantation
    Public Mode As NufarmBussinesRules.common.Helper.SaveMode
    Private originalWaterMaxTextBox As String = ""
    Private Sub FillListView(ByVal Dv As DataView)
        isLoadingRow = True
        Me.ListView1.Items.Clear()
        If Not IsNothing(Dv) Then
            For i As Integer = 0 To Dv.Count - 1
                Dim LVItem As New ListViewItem(Dv(i)("GROUP_ID").ToString())
                With LVItem
                    .SubItems.Add(Dv(i)("GROUP_NAME")) : .SubItems.Add(Dv(i)("DESCRIPTIONS"))
                End With
                Me.ListView1.Items.Add(LVItem)
            Next
        End If
    End Sub
   
    Public Overloads Function ShowDialog(ByVal dtTable As DataTable) As DialogResult
        Dim DgResult As DialogResult = Windows.Forms.DialogResult.Cancel
        Try
            Me.IsLoadingRow = True
            If Me.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If Me.txtGroupName.Text = "" Then
                    Me.baseTooltip.Show("Please Enter Name", Me.txtGroupName, 2500)
                    Return Windows.Forms.DialogResult.None
                End If
                Me.Cursor = Cursors.WaitCursor
                Me.clsPlantation.SaveGroupData(Me.txtGroupName.Text, Me.txtDescription.Text, Me.Mode, Me.lblGroupID.Text)
                dtTable.Columns.Add(New DataColumn("GROUP_ID", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("GROUP_NAME", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("DESCRIPTIONS", Type.GetType("System.String")))
                Dim dtRow As DataRow = dtTable.NewRow()
                dtRow("GROUP_ID") = Me.lblGroupID.Text
                dtRow("GROUP_NAME") = Me.txtGroupName.Text
                dtRow("DESCRIPTIONS") = RTrim(Me.txtDescription.Text)
                dtRow.EndEdit() : dtTable.Rows.Add(dtRow)
                DgResult = Windows.Forms.DialogResult.OK
            End If
            Return DgResult
        Catch ex As Exception
            Return Windows.Forms.DialogResult.None
            Me.LogMyEvent(ex.Message, Me.Name + "_ShowDialog")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.isLoadingRow = False : Me.Cursor = Cursors.Default
        End Try

    End Function
    Private Sub ColumnClick(ByVal o As Object, ByVal e As ColumnClickEventArgs)
        Me.ListView1.Sort()
        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub
    Private Sub PlantationGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.ListView1.Visible = False
            Application.DoEvents()
            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
            Dim DV As DataView = Me.clsPlantation.GetGroupPlantation(String.Empty)
            Me.FillListView(DV) : Application.DoEvents() : System.Threading.Thread.Sleep(200)
            Me.ListView1.Visible = True
            Me.originalWaterMaxTextBox = txtSearchGroupName.WaterMarkText
            Me.txtGroupName.Focus()
            Me.Location = New Point(18, 253)
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_PlantationGroup_Load")
        Finally
            : Me.isLoadingRow = False : Me.Cursor = Cursors.Default
        End Try
        AddHandler ListView1.ColumnClick, AddressOf ColumnClick

        ''hide listview1 dulu sebelum semua data di load
        'Me.Location = New System.Drawing.Point(18, 254)
    End Sub
    Private Sub ListView1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.ListView1.Items) Then
                If Me.ListView1.SelectedItems.Count > 0 Then
                    If Not IsNothing(Me.ListView1.FocusedItem) Then
                        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
                        Me.lblGroupID.Text = Me.ListView1.FocusedItem.Text
                        Me.IsLoadingRow = True
                        Me.txtGroupName.Text = Me.ListView1.FocusedItem.SubItems(1).Text
                        Me.txtDescription.Text = Me.ListView1.FocusedItem.SubItems(2).Text
                    End If
                End If
            End If
            Me.AcceptButton = Me.btnOK
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_ListView1_MouseClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.IsLoadingRow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtSearchGroupName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchGroupName.Enter
        If Me.txtSearchGroupName.WaterMarkText = Me.originalWaterMaxTextBox Then
            Me.txtSearchGroupName.WaterMarkText = ""
        End If
        Me.AcceptButton = Nothing
    End Sub

    Private Sub txtSearchGroupName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchGroupName.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.Cursor = Cursors.WaitCursor
                ''search ke database
                Dim DV As DataView = Me.clsPlantation.GetGroupPlantation(RTrim(Me.txtSearchGroupName.Text))
                Me.ListView1.Visible = True : Application.DoEvents()
                Me.FillListView(DV)
                Application.DoEvents() : System.Threading.Thread.Sleep(200) : Application.DoEvents()
                Me.ListView1.Visible = True
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_txtSearchGroupName_KeyDown")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtSearchGroupName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchGroupName.Leave
        If Me.txtSearchGroupName.Text = "" Then
            Me.txtSearchGroupName.WaterMarkText = Me.originalWaterMaxTextBox
        End If
    End Sub

    Private Sub txtGroupName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGroupName.Enter, txtDescription.Enter
        Me.AcceptButton = Me.btnOK
    End Sub

    Private Sub ListView1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If Not IsNothing(Me.ListView1.Items) Then
                    If Me.ListView1.SelectedItems.Count > 0 Then
                        If Not IsNothing(Me.ListView1.FocusedItem) Then
                            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                                Return
                            End If
                            Me.ListView1.Visible = False : Application.DoEvents()
                            Dim PlantGroupID As String = Me.ListView1.FocusedItem.Text
                            Me.clsPlantation.deleteGroup(PlantGroupID, Me.txtSearchGroupName.Text)
                            'Me.ListView1.Items(PlantGroupID)
                            Dim LVItem As ListViewItem = Me.ListView1.FindItemWithText(PlantGroupID)
                            If Not IsNothing(LVItem) Then
                                Me.ListView1.Items.Remove(LVItem)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_ListView1_KeyDown")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
End Class