Public Class DefinePKPP
    Public DistributorID As String = ""
    Public BrandPackID As String = ""
    Private clsMRKT_BRANDPACK As New NufarmBussinesRules.Program.BrandPackInclude()
    Private IDSystem As String = ""
    Private m_BonusValue As Decimal = 0
    Private isHasloaded As Boolean = False
    Public EndDate As Object = Nothing
    Private m_targetPKPP As Decimal = 0
    Public minStartDate As DateTime = Nothing

    Private Sub LoadData(ByVal StartDate As DateTime, ByVal EndDate As Object)
        Dim DV As DataView = Me.clsMRKT_BRANDPACK.getPKPPValue(Me.DistributorID, Me.BrandPackID, StartDate, EndDate)
        Me.FillListView(DV)
    End Sub
    Private Sub FillListView(ByVal dv As DataView)
        'With Me.ListView1
        '    .Items.Clear() : .Columns.Clear()
        '    If dtTable.Rows.Count > 0 Then
        '        .Columns.Add("BRANDPACK_ID", 100, HorizontalAlignment.Left)
        '        .Columns.Add("BRANDPACK_NAME", 200, HorizontalAlignment.Left)
        '        If cat = Category.Plantation Then
        '            .Columns.Add("PLANTATION_NAME", 120, HorizontalAlignment.Left)
        '        End If
        '        .Columns.Add("PRICE", 100, HorizontalAlignment.Right)
        '        .Columns.Add("START_DATE", 110, HorizontalAlignment.Left)
        '        For I As Integer = 0 To dtTable.Rows.Count - 1
        '            Dim LVItem As New ListViewItem(dtTable.Rows(I)("BRANDPACK_ID").ToString())
        '            With LVItem
        '                .SubItems.Add(dtTable.Rows(I)("BRANDPACK_NAME").ToString())
        '                If cat = Category.Plantation Then
        '                    .SubItems.Add(dtTable.Rows(I)("PLANTATION_NAME"))
        '                End If
        '                .SubItems.Add(String.Format("{0:#,##0.00}", Convert.ToDecimal(dtTable.Rows(I)("PRICE"))))
        '                .SubItems.Add(String.Format("{0:D}", Convert.ToDateTime(dtTable.Rows(I)("START_DATE"))))
        '            End With
        '            .Items.Add(LVItem)
        '        Next

        '    End If
        'End With
        With Me.ListView1
            .Items.Clear() : .Columns.Clear()
            If Not IsNothing(dv) Then
                If dv.Count > 0 Then
                    .Columns.Add("PROGRAM_ID", 120, HorizontalAlignment.Left)
                    .Columns.Add("TARGET_PKPP", 120, HorizontalAlignment.Right)
                    .Columns.Add("GIVEN_VALUE", 120, HorizontalAlignment.Right)
                    .Columns.Add("IDSYTEM", 130, HorizontalAlignment.Left)
                    For i As Integer = 0 To dv.Count - 1
                        Dim LVItem As New ListViewItem(dv(i)("PROGRAM_ID").ToString())
                        With LVItem
                            .SubItems.Add(Format(dv(i)("TARGET_PKPP"), "#,##0.000"))
                            .SubItems.Add(Format(dv(i)("GIVEN_VALUE"), "#,##0.00"))
                            .SubItems.Add(dv(i)("IDSYSTEM").ToString())
                        End With
                        .Items.Add(LVItem)
                    Next
                End If
            End If
        End With
    End Sub
    Public Overloads Function ShowDialog(ByRef progBrandPackDistID As String, ByRef BonusValue As Decimal, ByRef TargetPKPP As Decimal) As DialogResult
        Dim dgResult As DialogResult = Windows.Forms.DialogResult.Cancel
        If Me.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                If Me.IDSystem <> "" And Me.m_BonusValue > 0 Then
                    progBrandPackDistID = Me.IDSystem : BonusValue = Me.m_BonusValue : TargetPKPP = Me.m_targetPKPP
                    dgResult = Windows.Forms.DialogResult.OK
                End If
            Catch ex As Exception
                dgResult = Windows.Forms.DialogResult.None
            End Try
        End If
        Return dgResult
    End Function
    Private Sub dtPicFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicFrom.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.isHasloaded Then : Return : End If
            If Me.dtPicFrom.Value > Me.dtPicUntil.Value Then
                MessageBox.Show("Min StartDate > EndDate", "Please fill correct value", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Me.LoadData(Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub dtPicUntil_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicUntil.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.isHasloaded Then : Return : End If
            If Me.dtPicUntil.Value < Me.dtPicFrom.Value Then
                MessageBox.Show("Max EndDate < StartDate", "Please fill correct value", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Me.LoadData(Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DefinePKPP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Me.dtPicFrom.Value = NufarmBussinesRules.SharedClass.ServerDate().AddDays(7)
            'Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
            Me.dtPicUntil.Text = ""
            Me.dtPicFrom.Value = Me.minStartDate
            Me.LoadData(Me.minStartDate, Nothing)
            'Me.dtPicFrom.MinDate = Me.minStartDate
            Me.dtPicUntil.MinDate = Me.minStartDate
            Dim sEndDateAgrement As DateTime = Me.clsMRKT_BRANDPACK.getEndDateAgreement(Me.DistributorID, Me.BrandPackID, Me.minStartDate)
            Me.dtPicUntil.MaxDate = sEndDateAgrement
            Me.dtPicFrom.MaxDate = sEndDateAgrement
            Me.Location = New Point(450, 398)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.isHasloaded = True : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ListView1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseClick
        If Me.ListView1.Items Is Nothing Then
            Return
        ElseIf Me.ListView1.FocusedItem Is Nothing Then
            Return
        End If
        Me.m_BonusValue = Convert.ToDecimal(Me.ListView1.FocusedItem.SubItems(2).Text)
        Me.IDSystem = Me.ListView1.FocusedItem.SubItems(3).Text
        Me.m_targetPKPP = Convert.ToDecimal(Me.ListView1.FocusedItem.SubItems(1).Text)
    End Sub
End Class