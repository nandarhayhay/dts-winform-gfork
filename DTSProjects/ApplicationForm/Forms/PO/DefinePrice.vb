Imports NufarmBussinesRules.Brandpack.PriceHistory.Category
Public Class DefinePrice
    Private isloadingRow As Boolean = True : Private clsPriceHistory As New NufarmBussinesRules.Brandpack.PriceHistory()
    Friend Enum Category
        FM
        SPlantation
        GPlantation
    End Enum
    Private IsloadedForm As Boolean = False
    Public DistributorID As String = ""
    Public BrandPackID As String = ""
    Public StartDate As String = ""
    Private m_price As Object = Nothing
    Private m_Descriptions As String = ""
    Private m_PlantationID As String = ""
    Private m_territoryID As String = ""
    Private PriceTag As String = ""
    Private originalWaterMaxTextBox As String = ""

    Private Sub FillListView(ByVal cat As Category, ByVal dtTable As DataTable)
        With Me.ListView1
            .Items.Clear() : .Columns.Clear()
            If dtTable.Rows.Count > 0 Then
                .Columns.Add("BRANDPACK_ID", 100, HorizontalAlignment.Left)
                .Columns.Add("BRANDPACK_NAME", 170, HorizontalAlignment.Left)
                If cat = Category.SPlantation Then
                    .Columns.Add("PLANTATION_NAME", 170, HorizontalAlignment.Left)
                End If
                .Columns.Add("PRICE", 100, HorizontalAlignment.Right)
                .Columns.Add("START_DATE", 100, HorizontalAlignment.Left)
                If cat = Category.SPlantation Then
                    .Columns.Add("END_DATE", 100, HorizontalAlignment.Left)
                    .Columns.Add("PLANTATION_ID", 1, HorizontalAlignment.Left)
                End If

                For I As Integer = 0 To dtTable.Rows.Count - 1
                    Dim LVItem As New ListViewItem(dtTable.Rows(I)("BRANDPACK_ID").ToString())
                    With LVItem
                        .SubItems.Add(dtTable.Rows(I)("BRANDPACK_NAME").ToString())
                        If cat = Category.SPlantation Then
                            .SubItems.Add(dtTable.Rows(I)("PLANTATION_NAME"))
                        End If
                        .SubItems.Add(String.Format("{0:#,##0.00}", Convert.ToDecimal(dtTable.Rows(I)("PRICE"))))
                        .SubItems.Add(String.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(dtTable.Rows(I)("START_DATE"))))
                        If cat = Category.SPlantation Then
                            If Not IsNothing(dtTable.Rows(I)("END_DATE")) And Not IsDBNull(dtTable.Rows(I)("END_DATE")) Then
                                .SubItems.Add(String.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(dtTable.Rows(I)("END_DATE"))))
                            Else
                                .SubItems.Add("unspecified date")
                            End If
                            .SubItems.Add(dtTable.Rows(I)("PLANTATION_ID").ToString())
                        End If
                        .SubItems.Add(dtTable.Rows(I)("PRICE_TAG").ToString())
                    End With
                    .Items.Add(LVItem)
                Next
            End If
        End With
    End Sub
    'Private Sub rdbPlantation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Not IsloadedForm Then : Return : End If
    '    If rdbPlantation.Checked Then
    '        Try
    '            'getdata price by plantation_id
    '            isloadingRow = True
    '            Me.Cursor = Cursors.WaitCursor
    '            If Me.rdbPlantation.Checked Then
    '                Dim dtTable As DataTable = Me.clsPriceHistory.getPlantation(NufarmBussinesRules.Brandpack.PriceHistory.Category.Plantation, _
    '                 Me.BrandPackID, Me.DistributorID, Me.StartDate)
    '                Me.FillListView(Category.Plantation, dtTable)
    '            End If
    '        Catch ex As Exception
    '            Me.LogMyEvent(ex.Message, Me.Name + "_rdbPlantation_CheckedChanged")
    '            Me.ShowMessageInfo(ex.Message)
    '        Finally
    '            isloadingRow = False : Me.Cursor = Cursors.Default
    '        End Try
    '    End If
    'End Sub

    'Private Sub rdbFreeMarket_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Not IsloadedForm Then : Return : End If
    '    Try
    '        isloadingRow = True
    '        Me.Cursor = Cursors.WaitCursor
    '        If rdbFreeMarket.Checked Then
    '            Dim dtTable As DataTable = Me.clsPriceHistory.getPlantation(NufarmBussinesRules.Brandpack.PriceHistory.Category.FreeMarket, _
    '             Me.BrandPackID, Me.DistributorID, Me.StartDate)
    '            Me.FillListView(Category.FM, dtTable)
    '        End If
    '    Catch ex As Exception
    '        Me.LogMyEvent(ex.Message, Me.Name + "_rdbFreeMarket_CheckedChanged")
    '        Me.ShowMessageInfo(ex.Message)
    '    Finally
    '        isloadingRow = False : Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Public Overloads Function ShowDialog(ByRef Price As Object, ByRef Description As String, ByRef PlantationID As String, ByRef TerritoryID As String, ByRef Pricetag As String) As DialogResult
        Dim DgResult As DialogResult = Windows.Forms.DialogResult.Cancel
        If Me.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'If Me.ListView1.Items Is Nothing Then
            '    Return Windows.Forms.DialogResult.None
            'ElseIf Me.ListView1.FocusedItem Is Nothing Then
            '    Return Windows.Forms.DialogResult.None
            'End If
            'If Me.rdbFreeMarket.Checked Then
            '    Price = Me.ListView1.FocusedItem.SubItems(2).Text
            'Else
            '    Price = Me.ListView1.FocusedItem.SubItems(3).Text
            'End If
            If Not IsNothing(Me.m_price) Then
                Price = Me.m_price : Description = Me.m_Descriptions : PlantationID = Me.m_PlantationID
                TerritoryID = Me.m_territoryID
                Pricetag = Me.PriceTag
                DgResult = Windows.Forms.DialogResult.OK
            Else
                DgResult = Windows.Forms.DialogResult.None
            End If
        End If
        Return DgResult
    End Function

    Private Sub ListView1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseClick
        'If Not IsNothing(Me.ListView1.FocusedItem()) Then
        '    SendKeys.Send("ENTER")
        'End If
        If Me.ListView1.Items Is Nothing Then
            Return
        ElseIf Me.ListView1.Items.Count <= 0 Then
            Return
        ElseIf Me.ListView1.FocusedItem Is Nothing Then
            Return
        End If
        If Me.btnCatFreeMarket.Checked Then
            m_price = Me.ListView1.FocusedItem.SubItems(2).Text
            m_Descriptions = "PRICE FROM FREE MARKET"
            Me.PriceTag = Me.ListView1.FocusedItem.SubItems(4).Text
        ElseIf Me.btnCatPlantation.Checked Then
            m_price = Me.ListView1.FocusedItem.SubItems(3).Text
            m_Descriptions = "PRICE FROM SPECIAL PLANTATION " & Me.ListView1.FocusedItem.SubItems(2).Text
            Me.m_PlantationID = Me.ListView1.FocusedItem.SubItems(6).Text
            Me.PriceTag = Me.ListView1.FocusedItem.SubItems(7).Text
        ElseIf Me.btnGeneralPlantation.Checked Then
            m_price = Me.ListView1.FocusedItem.SubItems(2).Text
            m_Descriptions = "PRICE FROM GENERAL PLANTATION"
            Me.PriceTag = Me.ListView1.FocusedItem.SubItems(4).Text
        End If
        'check apakah plantationid ada territorynya atau tidak 
        'bila tidak ambil territory distributor
        Me.m_territoryID = Me.clsPriceHistory.getTerritory(Me.DistributorID, Me.m_PlantationID)
    End Sub

    Private Sub DefinePrice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor : Me.IsloadedForm = True
            Me.originalWaterMaxTextBox = Me.txtSearchPlantationName.WaterMarkText
            'If Me.rdbFreeMarket.Checked Then
            '    Me.rdbFreeMarket_CheckedChanged(Me.rdbFreeMarket, New EventArgs())
            'Else
            '    Me.rdbPlantation_CheckedChanged(Me.rdbPlantation, New EventArgs())
            'End If
            If Me.btnCatPlantation.Checked Then
                Me.ShowSpecialPlantation()
            ElseIf Me.btnGeneralPlantation.Checked Then
                Me.ShowGeneralPlantationPrice()
            ElseIf Me.btnCatFreeMarket.Checked Then
                Me.ShowFreemarketPrice()
            End If
            Me.Location = New Point(257, 432)
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Anchor + "_DefinePrice_Load")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.isloadingRow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ShowFreemarketPrice()
        isloadingRow = True
        Dim dtTable As DataTable = Me.clsPriceHistory.getPlantation(FreeMarket, BrandPackID, Me.DistributorID, Me.StartDate)
        Me.FillListView(Category.FM, dtTable)
        isloadingRow = False
    End Sub
    Private Sub ShowSpecialPlantation()
        isloadingRow = True
        Dim SearchString As String = ""
        If Me.txtSearchPlantationName.Text <> "" Then
            If Me.txtSearchPlantationName.WaterMarkText <> Me.originalWaterMaxTextBox Then
                SearchString = RTrim(Me.txtSearchPlantationName.Text)
            End If
        End If
        Dim dtTable As DataTable = Me.clsPriceHistory.getPlantation(SpecialPlantation, _
                           Me.BrandPackID, Me.DistributorID, Me.StartDate, SearchString)
        Me.FillListView(Category.SPlantation, dtTable)
        isloadingRow = False
    End Sub
    Private Sub ShowGeneralPlantationPrice()
        Me.isloadingRow = True
        'Dim SearchString As String = ""
        'If Me.txtSearchPlantationName.Text <> "" Then
        '    If Me.txtSearchPlantationName.WaterMarkText <> Me.originalWaterMaxTextBox Then
        '        SearchString = RTrim(Me.txtSearchPlantationName.Text)
        '    End If
        'End If
        Dim dtTable As DataTable = Me.clsPriceHistory.getPlantation(NufarmBussinesRules.Brandpack.PriceHistory.Category.GeneralPricePlantation, _
                           Me.BrandPackID, "", Me.StartDate, "")
        Me.FillListView(Category.GPlantation, dtTable)
        isloadingRow = False
    End Sub
    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            If TypeOf item Is DevComponents.DotNetBar.ButtonItem Then
                CType(item, DevComponents.DotNetBar.ButtonItem).Checked = Not CType(item, DevComponents.DotNetBar.ButtonItem).Checked
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.isloadingRow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtSearchPlantationName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchPlantationName.Enter
        'Me.txtSearchPlantationName.Text = "" : Me.txtSearchPlantationName.WaterMarkText = ""
        If Me.txtSearchPlantationName.WaterMarkText = Me.originalWaterMaxTextBox Then
            Me.txtSearchPlantationName.Text = "" : Me.txtSearchPlantationName.WaterMarkText = ""
        End If
        Me.AcceptButton = Nothing
    End Sub

    Private Sub txtSearchPlantationName_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchPlantationName.Leave
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.txtSearchPlantationName.Text = "" Then
                Me.txtSearchPlantationName.WaterMarkText = Me.originalWaterMaxTextBox
            Else
                Me.ShowSpecialPlantation()
            End If
            Me.AcceptButton = Me.btnOK
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            isloadingRow = False : Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub txtSearchPlantationName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchPlantationName.KeyDown
        Try
            Me.Cursor = Cursors.WaitCursor
            If e.KeyCode = Keys.Enter Then
                Me.ShowSpecialPlantation()
            End If
            Me.AcceptButton = Nothing
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            isloadingRow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub DefinePrice_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
    '    Dim a As String = ""
    'End Sub

    Private Sub ListView1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub ItemPanel1_ButtonCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ButtonCheckedChanged
        If Me.isloadingRow Then : Return : End If
        Dim c As DevComponents.DotNetBar.ButtonItem = CType(sender, DevComponents.DotNetBar.ButtonItem)
        Me.Cursor = Cursors.WaitCursor

        'clear semua item yg di checked
        Try
            Me.isloadingRow = True
            For Each b As DevComponents.DotNetBar.BaseItem In Me.ItemPanel1.Items
                If TypeOf b Is DevComponents.DotNetBar.ButtonItem Then
                    If CType(b, DevComponents.DotNetBar.ButtonItem).Name <> c.Name Then
                        CType(b, DevComponents.DotNetBar.ButtonItem).Checked = False
                    End If
                End If
            Next
            'c.Checked = Not c.Checked
            Me.ListView1.Items.Clear() : Me.m_Descriptions = "" : Me.m_PlantationID = "" : Me.m_price = ""

            If c.Checked Then
                Select Case c.Name
                    Case "btnCatFreeMarket"
                        Me.txtSearchPlantationName.Enabled = False
                        Me.txtSearchPlantationName.WaterMarkText = ""
                        Me.ShowFreemarketPrice()
                    Case "btnCatPlantation"
                        Me.txtSearchPlantationName.Text = "" : Me.txtSearchPlantationName.WaterMarkText = Me.originalWaterMaxTextBox
                        Me.txtSearchPlantationName.Enabled = True
                        Me.ShowSpecialPlantation()
                    Case "btnGeneralPlantation"
                        'Me.txtSearchPlantationName.Text = "" : Me.txtSearchPlantationName.WaterMarkText = Me.originalWaterMaxTextBox
                        'Me.txtSearchPlantationName.Enabled = True
                        Me.ShowGeneralPlantationPrice()
                End Select
            End If
            Me.isloadingRow = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.ListView1.Items.Clear() : Me.m_Descriptions = "" : Me.m_PlantationID = "" : Me.m_price = ""
            c.Checked = Not c.Checked
            Me.isloadingRow = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
