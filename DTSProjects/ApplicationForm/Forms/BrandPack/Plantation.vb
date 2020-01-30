Public Class Plantation
    Public Mode As NufarmBussinesRules.common.Helper.SaveMode
    Private clsPlantation As New NufarmBussinesRules.Plantation.Plantation
    Public PlantationID As String = "" : Private IsLoadingRow As Boolean = True
    Private m_DVDistributors As DataView = Nothing
    Private Sub FillListView(ByVal Dv As DataView)
        IsLoadingRow = True
        If Not IsNothing(Dv) Then
            For i As Integer = 0 To Dv.Count - 1
                Dim LVItem As New ListViewItem(Dv(i)("PLANTATION_ID").ToString())
                With LVItem
                    .SubItems.Add(Dv(i)("PLANTATION_NAME")) : .SubItems.Add(Dv(i)("DESCRIPTIONS"))
                End With
                Me.ListView1.Items.Add(LVItem)
            Next
        End If
    End Sub
    Friend Sub LoadData(ByVal DVDistributors As DataView)
        Dim ListDistributors As New List(Of String)
        If Not IsNothing(DVDistributors) Then
            Me.m_DVDistributors = DVDistributors
            DVDistributors.Sort = "DISTRIBUTOR_ID ASC"
            If Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert Then
                'getnewID
                Dim ID As Integer = Me.clsPlantation.getNewID()
                If DVDistributors.Count > 1 Then
                    For i As Integer = 0 To DVDistributors.Count - 1
                        PlantationID &= DVDistributors(i)("DISTRIBUTOR_ID").ToString().Substring(0, 2)
                        If i < DVDistributors.Count - 1 Then
                            PlantationID &= "/"
                        End If
                    Next
                ElseIf DVDistributors.Count = 1 Then
                    PlantationID &= DVDistributors(0)("DISTRIBUTOR_ID").ToString().Substring(0, 2)
                End If
                PlantationID &= "|" & ID.ToString()
            End If
        End If
        For i As Integer = 0 To DVDistributors.Count - 1
            Me.lblDistributors.Text &= DVDistributors(i)("DISTRIBUTOR_NAME").ToString()
            If i < DVDistributors.Count - 1 Then
                Me.lblDistributors.Text &= ","
            End If
            ListDistributors.Add(DVDistributors(i)("DISTRIBUTOR_ID").ToString())
        Next
        If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
            'inflate data
            Dim DtTable As DataTable = Me.clsPlantation.GetRowByID(Me.PlantationID)
            Me.txtPlantationName.Text = DtTable.Rows(0)("PLANTATION_NAME").ToString()
            Me.txtDescription.Text = DtTable.Rows(0)("DESCRIPTIONS").ToString()
        End If
        'getdata

        If ListDistributors.Count > 0 Then
            Dim DV As DataView = Me.clsPlantation.GetPlantation(ListDistributors)
            Me.FillListView(DV)
        End If

    End Sub
    Private Function IsValid() As Boolean
        Dim B As Boolean = True
        If (Me.txtPlantationName.Text = "") Then
            Me.baseTooltip.Show("Please Type PlantationName", Me.txtPlantationName, 2500)
            B = False
        End If
        Return B
    End Function
    Private Sub txtPlantationName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlantationName.TextChanged
        If IsLoadingRow Then
            Return
        End If
        If Me.txtPlantationName.Text <> "" Then
            Me.lblPlantationID.Text = Me.PlantationID
        Else
            Me.lblPlantationID.Text = ""
        End If

    End Sub

    Public Overloads Function ShowDialog(ByVal dtTable As DataTable) As DialogResult
        Dim DgResult As DialogResult = Windows.Forms.DialogResult.Cancel
        Try
            Me.IsLoadingRow = True
            If Me.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If Not Me.IsValid() Then
                    Return Windows.Forms.DialogResult.None
                End If
                Me.Cursor = Cursors.WaitCursor
                Me.clsPlantation.SaveData(Me.txtPlantationName.Text, Me.lblPlantationID.Text, Me.txtDescription.Text, Me.Mode)
                dtTable.Columns.Add(New DataColumn("PLANTATION_ID", Type.GetType("System.String")))
                dtTable.Columns.Add(New DataColumn("PLANTATION_NAME", Type.GetType("System.String")))
                Dim dtRow As DataRow = dtTable.NewRow()
                dtRow("PLANTATION_ID") = Me.lblPlantationID.Text
                dtRow("PLANTATION_NAME") = Me.txtPlantationName.Text
                dtRow.EndEdit() : dtTable.Rows.Add(dtRow)
                DgResult = Windows.Forms.DialogResult.OK
            End If
        Catch ex As Exception
            DgResult = Windows.Forms.DialogResult.None
            Me.LogMyEvent(ex.Message, Me.Name + "_ShowDialog")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.IsLoadingRow = False : Me.Cursor = Cursors.Default
        End Try
        Return DgResult
    End Function
    Private Sub ColumnClick(ByVal o As Object, ByVal e As ColumnClickEventArgs)
        Me.ListView1.Sort()
        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column)

    End Sub
    Private Sub Plantation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ListView1.ColumnClick, AddressOf ColumnClick
        Me.IsLoadingRow = False
        Me.Location = New System.Drawing.Point(18, 254)
    End Sub

    Private Sub ListView1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.ListView1.Items) Then
                If Me.ListView1.SelectedItems.Count > 0 Then
                    If Not IsNothing(Me.ListView1.FocusedItem) Then
                        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
                        Me.lblPlantationID.Text = Me.ListView1.FocusedItem.Text
                        Me.IsLoadingRow = True
                        Me.txtPlantationName.Text = Me.ListView1.FocusedItem.SubItems(1).Text
                        Me.txtDescription.Text = Me.ListView1.FocusedItem.SubItems(2).Text
                        If Not IsNothing(Me.m_DVDistributors) Then
                            Me.lblDistributors.Text = ""
                            For i As Integer = 0 To m_DVDistributors.Count - 1
                                Me.lblDistributors.Text &= m_DVDistributors(i).ToString()
                                If i < m_DVDistributors.Count Then
                                    Me.lblDistributors.Text &= ","
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_ListView1_MouseClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.IsLoadingRow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
