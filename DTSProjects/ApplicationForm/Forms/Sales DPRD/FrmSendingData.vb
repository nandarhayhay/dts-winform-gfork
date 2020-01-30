Imports System.Runtime.Serialization.Formatters.Soap
Public Class FrmSendingData
    Private clsSendData As New NufarmBussinesRules.ManageSendData.Sendata()
    Private OriginalWaterMarkText As String = ""
    Private ListBrandPack As New List(Of String)
    Private ListAgreement As New List(Of String)
    Private ListBrand As New List(Of String)
    Private ListPack As New List(Of String)
    Private ListDistributor As New List(Of String)
    Private ListRSM As New List(Of Integer)
    Private ListPM As New List(Of Integer)
    Private ListTM As New List(Of Integer)
    Private listFS As New List(Of Integer)
    Private ListTerritory As New List(Of String)
    Private ListRegional As New List(Of String)
    Private ballon As System.Windows.Forms.ToolTip
    Private IsLoadingRow As Boolean = False
    Friend CMain As Main = Nothing
    Private tblFS As DataTable = Nothing
    Private hasCheckedData As Boolean = False
    Private Sub ShowBallonTip(ByVal Message As String, ByVal Title As String)
        If IsNothing(Me.ballon) Then
            ballon = New ToolTip()
        End If
        ballon.ToolTipIcon = ToolTipIcon.Info
        ballon.UseAnimation = True
        ballon.ToolTipTitle = Title
        ballon.IsBalloon = True
        ballon.Show(Message, Me.txtSearch, 3000)
    End Sub

    Private Sub AddList(ByVal item As String)
        Select Case Me.ViewingData
            Case ViewData.Agreement
                If Me.ListAgreement.Contains(item) Then
                Else
                    Me.ListAgreement.Add(item)
                End If
            Case ViewData.Brand
                If Me.ListBrand.Contains(item) Then
                Else
                    Me.ListBrand.Add(item)
                End If
            Case ViewData.BrandPack
                If Me.ListBrandPack.Contains(item) Then
                Else
                    Me.ListBrandPack.Add(item)
                End If
            Case ViewData.Distributor
                If Me.ListDistributor.Contains(item) Then
                Else
                    Me.ListDistributor.Add(item)
                End If
            Case ViewData.Pack
                If Not Me.ListPack.Contains(item) Then
                    Me.ListPack.Add(item)
                End If
            Case ViewData.PriviledgePM
                If Me.ListPM.Contains(item) Then
                Else
                    Me.ListPM.Add(item)
                End If
            Case ViewData.PriviledgeRSM
                If Me.ListRSM.Contains(item) Then
                Else
                    Me.ListRSM.Add(item)
                End If
            Case ViewData.priviledgeTM
                If Me.ListTM.Contains(item) Then
                Else
                    Me.ListTM.Add(item)
                End If
            Case ViewData.PriviledgeFS
                If Me.listFS.Contains(item) Then
                Else
                    Me.listFS.Add(item)
                End If
            Case ViewData.Regional
                If Me.ListRegional.Contains(item) Then
                Else
                    Me.ListRegional.Add(item)
                End If
            Case ViewData.Territorial
                If Me.ListTerritory.Contains(item) Then
                Else
                    Me.ListTerritory.Add(item)
                End If
        End Select
    End Sub
    Private Sub RemoveList(ByVal item As String)
        Select Case Me.ViewingData
            Case ViewData.Agreement
                If Me.ListAgreement.Contains(item) Then
                    Me.ListAgreement.Remove(item)
                End If
            Case ViewData.Brand
                If Me.ListBrand.Contains(item) Then
                    Me.ListBrand.Remove(item)
                End If
            Case ViewData.BrandPack
                If Me.ListBrandPack.Contains(item) Then
                    Me.ListBrandPack.Remove(item)
                End If
            Case ViewData.Distributor
                If Me.ListDistributor.Contains(item) Then
                    Me.ListDistributor.Remove(item)
                End If
            Case ViewData.Pack
                If Me.ListPack.Contains(item) Then
                    Me.ListPack.Remove(item)
                End If
            Case ViewData.PriviledgePM
                If Me.ListPM.Contains(item) Then
                    Me.ListPM.Remove(item)
                End If
            Case ViewData.PriviledgeRSM
                If Me.ListRSM.Contains(item) Then
                    Me.ListRSM.Remove(item)
                End If
            Case ViewData.priviledgeTM
                If Me.ListTM.Contains(item) Then
                    Me.ListTM.Remove(item)
                End If
            Case ViewData.PriviledgeFS
                If Me.listFS.Contains(item) Then
                    Me.listFS.Remove(item)
                End If
            Case ViewData.Regional
                If Me.ListRegional.Contains(item) Then
                    Me.ListRegional.Remove(item)
                End If
            Case ViewData.Territorial
                If Me.ListTerritory.Contains(item) Then
                    Me.ListTerritory.Remove(item)
                End If
        End Select
    End Sub
    Private Function ValueRow(ByVal DV As DataView, ByVal row As DataRowView, ByVal ColName As String) As String
        Dim Val As String = ""
        If row(ColName) Is DBNull.Value Then
            Val = String.Empty
        ElseIf (DV.Table.Columns(ColName).DataType Is Type.GetType("System.Int32")) Then
            Val = String.Format("{0:G}", Convert.ToInt32(row(ColName)))
        ElseIf (DV.Table.Columns(ColName).DataType Is Type.GetType("System.Decimal")) Then
            If DV.Table.Columns(ColName).ColumnName = "DEVIDED_QUANTITY" Then
                Val = String.Format("{0:#,##0.000}", Convert.ToDecimal(row(ColName)))
            Else
                Val = String.Format("{0:#,##0.00}", Convert.ToDecimal(row(ColName)))
            End If
        ElseIf (DV.Table.Columns(ColName).DataType Is Type.GetType("System.DateTime")) Then
            Val = String.Format("{0:D}", Convert.ToDateTime(row(ColName)))
        Else : Val = row(ColName).ToString()
        End If
        Return Val
    End Function
    Private ViewingData As ViewData
    Private Enum ViewData
        None
        Brand
        Pack
        BrandPack
        Agreement
        Distributor
        PriviledgeRSM
        PriviledgePM
        PriviledgeTM
        PriviledgeFS
        Territorial
        Regional
    End Enum
    Private Sub CreateColumnsBrandPack()
        With Me.ListView1
            .Items.Clear()
            .Groups.Clear()
            .Columns.Clear()
            .Columns.Add("BRANDPACK_ID", 120, HorizontalAlignment.Left)
            .Columns.Add("BRAND_ID", 100, HorizontalAlignment.Left)
            .Columns.Add("PACK_ID", 100, HorizontalAlignment.Left)
            .Columns.Add("BRANDPACK_NAME", 230, HorizontalAlignment.Left)
            .Columns.Add("DIVIDED_QUANTITY", 130, HorizontalAlignment.Right) 'DEVIDED_QUANTITY
            .Columns.Add("UNIT", 130, HorizontalAlignment.Center)
            '.Columns.Add("INACTIVE", 0, HorizontalAlignment.Center)
        End With
    End Sub
    Private Sub ChekAvailableItem()
        Select Case Me.ViewingData
            Case ViewData.Agreement
                If Me.ListAgreement.Count > 0 Then
                    For i As Integer = 0 To Me.ListView1.Items.Count - 1
                        Dim AgreeBrandPack_ID As String = Me.ListView1.Items(i).Text + Me.ListView1.Items(i).SubItems(4).Text
                        If Me.ListAgreement.Contains(AgreeBrandPack_ID) Then
                            ListView1.Items(i).Checked = True
                        End If
                    Next
                    'For i As Integer = 0 To Me.ListAgreement.Count - 1
                    '    Dim ItemFound As ListViewItem = Me.ListView1.FindItemWithText(ListAgreement(i).ToString())

                    '    If Not IsNothing(ItemFound) Then
                    '        Me.ListView1.Items(ListAgreement(i)).Checked = True

                    '    End If
                    'Next
                End If
            Case ViewData.Brand
                If Me.ListBrand.Count > 0 Then
                    For i As Integer = 0 To Me.ListBrand.Count - 1
                        Dim ItemFound As ListViewItem = Me.ListView1.FindItemWithText(ListBrand(i).ToString())
                        If Not IsNothing(ItemFound) Then
                            Me.ListView1.Items(ItemFound.Index).Checked = True
                        End If
                    Next
                End If
            Case ViewData.BrandPack
                If Me.ListBrandPack.Count > 0 Then
                    For i As Integer = 0 To Me.ListBrandPack.Count - 1
                        Dim ItemFound As ListViewItem = Me.ListView1.FindItemWithText(ListBrandPack(i).ToString())
                        If Not IsNothing(ItemFound) Then
                            Me.ListView1.Items(ItemFound.Index).Checked = True
                        End If
                    Next
                End If
            Case ViewData.Distributor
                If Me.ListDistributor.Count > 0 Then
                    For i As Integer = 0 To Me.ListDistributor.Count - 1
                        Dim ItemFound As ListViewItem = Me.ListView1.FindItemWithText(ListDistributor(i).ToString())
                        If Not IsNothing(ItemFound) Then
                            Me.ListView1.Items(ItemFound.Index).Checked = True
                        End If
                    Next
                End If
            Case ViewData.Pack
                If Me.ListPack.Count > 0 Then
                    For i As Integer = 0 To Me.ListPack.Count - 1
                        Dim ItemFound As ListViewItem = Me.ListView1.FindItemWithText(ListPack(i).ToString())
                        If Not IsNothing(ItemFound) Then
                            Me.ListView1.Items(ItemFound.Index).Checked = True
                        End If
                    Next
                End If
            Case ViewData.PriviledgePM
                If Me.ListPM.Count > 0 Then
                    For i As Integer = 0 To Me.ListPM.Count - 1
                        Dim ItemFound As ListViewItem = Me.ListView1.FindItemWithText(ListPM(i).ToString())
                        If Not IsNothing(ItemFound) Then
                            Me.ListView1.Items(ItemFound.Index).Checked = True
                        End If
                    Next
                End If
            Case ViewData.PriviledgeRSM
                If Me.ListRSM.Count > 0 Then
                    For i As Integer = 0 To Me.ListRSM.Count - 1
                        Dim ItemFound As ListViewItem = Me.ListView1.FindItemWithText(ListRSM(i).ToString())
                        If Not IsNothing(ItemFound) Then
                            Me.ListView1.Items(ItemFound.Index).Checked = True
                        End If
                    Next
                End If
            Case ViewData.priviledgeTM
                If Me.ListTM.Count > 0 Then
                    For i As Integer = 0 To Me.ListTM.Count - 1
                        Dim ItemFound As ListViewItem = Me.ListView1.FindItemWithText(ListTM(i).ToString())
                        If Not IsNothing(ItemFound) Then
                            Me.ListView1.Items(ItemFound.Index).Checked = True
                        End If
                    Next
                End If
            Case ViewData.PriviledgeFS
                If Me.listFS.Count > 0 Then
                    For i As Integer = 0 To Me.listFS.Count - 1
                        Dim ItemFound As ListViewItem = Me.ListView1.FindItemWithText(listFS(i).ToString())
                        If Not IsNothing(ItemFound) Then
                            Me.ListView1.Items(ItemFound.Index).Checked = True
                        End If
                    Next
                End If
            Case ViewData.Regional
                If Me.ListRegional.Count > 0 Then
                    For i As Integer = 0 To Me.ListRegional.Count - 1
                        Dim ItemFound As ListViewItem = Me.ListView1.FindItemWithText(ListRegional(i))
                        If Not IsNothing(ItemFound) Then
                            Me.ListView1.Items(ItemFound.Index).Checked = True
                        End If
                    Next
                End If
            Case ViewData.Territorial
                If Me.ListTerritory.Count > 0 Then
                    For i As Integer = 0 To Me.ListTerritory.Count - 1
                        Dim ItemFound As ListViewItem = Me.ListView1.FindItemWithText(ListTerritory(i))
                        If Not IsNothing(ItemFound) Then
                            Me.ListView1.Items(ItemFound.Index).Checked = True
                        End If
                    Next
                End If
        End Select
    End Sub
    Private Sub FillListViewBrandPack(ByVal Dv As DataView, ByVal IsViewResult As Boolean)
        With Me.ListView1
            .Groups.Clear()
            .Items.Clear()
            Dim DataToview As Integer = Dv.Count
            Me.ProgresBar1.Value = 1
            Me.ProgresBar1.Minimum = 1
            Dim StepStatis As Integer = Dv.Count / 100
            If Dv.Count < 100 Then
                StepStatis = Convert.ToDecimal(Dv.Count)
            End If
            Me.ProgresBar1.Maximum = 100
            If IsViewResult Then
                If Me.ListBrandPack.Count > 0 Then
                    For i As Integer = 0 To DataToview - 1
                        Dim BrandPackID As String = Dv(i)("BRANDPACK_ID").ToString()
                        If Me.ListBrandPack.Contains(BrandPackID) Then
                            Dim LVItem As New ListViewItem(ValueRow(Dv, Dv(i), "BRANDPACK_ID").ToString())
                            With LVItem
                                .SubItems.Add(ValueRow(Dv, Dv(i), "BRAND_ID").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "PACK_ID").ToString())
                                '.SubItems.Add(new ListViewItem.ListViewSubItem(
                                .SubItems.Add(ValueRow(Dv, Dv(i), "BRANDPACK_NAME").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "DEVIDED_QUANTITY").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "UNIT").ToString())
                            End With
                            .Items.Add(LVItem)
                            .Items(LVItem.Index).Checked = True
                        End If
                        If i Mod StepStatis = 0 Then
                            If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                                Me.ProgresBar1.Value += 1
                            End If
                        End If
                    Next
                End If
                Me.lblResult.Text = String.Format("{0} item(s) found", Me.ListBrandPack.Count)
            Else
                For I As Integer = 0 To DataToview - 1
                    Dim LVItem As New ListViewItem(ValueRow(Dv, Dv(I), "BRANDPACK_ID").ToString())
                    With LVItem
                        .SubItems.Add(ValueRow(Dv, Dv(I), "BRAND_ID").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(I), "PACK_ID").ToString())
                        '.SubItems.Add(new ListViewItem.ListViewSubItem(
                        .SubItems.Add(ValueRow(Dv, Dv(I), "BRANDPACK_NAME").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(I), "DEVIDED_QUANTITY").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(I), "UNIT").ToString())
                    End With
                    .Items.Add(LVItem)
                    If I Mod StepStatis = 0 Then
                        If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                            Me.ProgresBar1.Value += 1
                        End If
                    End If
                Next
                Me.lblResult.Text = String.Format("{0} item(s) found", DataToview)
            End If
        End With
    End Sub
    Private Sub CreateColumnsBrand()
        With Me.ListView1
            .Items.Clear()
            .Groups.Clear()
            .Columns.Clear()
            .Columns.Add("BRAND_ID", 120, HorizontalAlignment.Left)
            .Columns.Add("BRAND_NAME", 170, HorizontalAlignment.Left)
            '.Columns.Add("INACTIVE", 0, HorizontalAlignment.Center)
        End With
    End Sub
    Private Sub FillListViewBrand(ByVal DV As DataView, ByVal IsViewResult As Boolean)
        With Me.ListView1
            Dim DataToview As Integer = DV.Count
            .Groups.Clear()
            .Items.Clear()
            'If DV.Count > 100 Then
            '    If Not IsViewResult Then
            '        Me.ShowBallonTip("Data over than 100 rows" & vbCrLf & "For some reasons , system limits viewing data to 100 rows", "you should filter data to view")
            '        Dim LeftData As Integer = DV.Count - 100
            '        DataToview = DV.Count - LeftData
            '    End If
            'End If
            Me.ProgresBar1.Value = 1
            Me.ProgresBar1.Minimum = 1
            Dim StepStatis As Integer = DV.Count / 100
            If DV.Count < 100 Then
                StepStatis = Convert.ToDecimal(DV.Count)
            End If

            Me.ProgresBar1.Maximum = 100
            If IsViewResult Then
                If Me.ListBrand.Count > 0 Then
                    For i As Integer = 0 To DataToview - 1
                        Dim BrandID As String = DV(i)("BRAND_ID").ToString()
                        If Me.ListBrand.Contains(BrandID) Then
                            Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "BRAND_ID").ToString())
                            With LVItem
                                .SubItems.Add(ValueRow(DV, DV(i), "BRAND_NAME"))

                            End With
                            .Items.Add(LVItem)
                            .Items(LVItem.Index).Checked = True
                        End If
                        If i Mod StepStatis = 0 Then
                            If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                                Me.ProgresBar1.Value += 1
                            End If
                        End If
                    Next
                End If
                Me.lblResult.Text = String.Format("{0} item(s) found", Me.ListBrand.Count)
            Else
                For i As Integer = 0 To DataToview - 1
                    Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "BRAND_ID").ToString())
                    With LVItem
                        .SubItems.Add(ValueRow(DV, DV(i), "BRAND_NAME"))
                    End With
                    .Items.Add(LVItem)
                    If i Mod StepStatis = 0 Then
                        If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                            Me.ProgresBar1.Value += 1
                        End If
                    End If
                Next
                Me.lblResult.Text = String.Format("{0} item(s) found", DataToview)
            End If
        End With
    End Sub
    Private Sub CreateColumnsPack()
        With Me.ListView1
            .Items.Clear()
            .Groups.Clear()
            .Columns.Clear()
            .Columns.Add("PACK_ID", 110, HorizontalAlignment.Left)
            .Columns.Add("PACK_NAME", 120, HorizontalAlignment.Left)
            .Columns.Add("QUANTITY", 120, HorizontalAlignment.Right)
            .Columns.Add("DIVIDE_FACTOR", 120, HorizontalAlignment.Right)
            '.Columns.Add("INACTIVE", 0, HorizontalAlignment.Center)
        End With
    End Sub
    Private Sub FillListViewPack(ByVal DV As DataView, ByVal IsViewResult As Boolean)
        With Me.ListView1
            Dim DataToview As Integer = DV.Count
            'If DV.Count > 100 Then
            '    If Not IsViewResult Then
            '        Me.ShowBallonTip("Data over than 100 rows" & vbCrLf & "For some reasons , system limits viewing data to 100 rows", "you should filter data to view")
            '        Dim LeftData As Integer = DV.Count - 100
            '        DataToview = DV.Count - LeftData
            '    End If
            'End If
            .Groups.Clear()
            .Items.Clear()
            Me.ProgresBar1.Value = 1
            Me.ProgresBar1.Minimum = 1
            Dim StepStatis As Integer = DV.Count / 100
            If DV.Count < 100 Then
                StepStatis = Convert.ToDecimal(DV.Count)
            End If

            Me.ProgresBar1.Maximum = 100
            If IsViewResult Then
                If Me.ListPack.Count > 0 Then
                    For i As Integer = 0 To DataToview - 1
                        Dim Pack_ID As String = DV(i)("PACK_ID").ToString()
                        If Me.ListPack.Contains(Pack_ID) Then
                            Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "PACK_ID").ToString())
                            With LVItem
                                .SubItems.Add(ValueRow(DV, DV(i), "PACK_NAME").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "QUANTITY").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "DEVIDE_FACTOR").ToString())
                            End With
                            .Items.Add(LVItem)
                            .Items(LVItem.Index).Checked = True
                        End If
                        If i Mod StepStatis = 0 Then
                            If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                                Me.ProgresBar1.Value += 1
                            End If
                        End If
                    Next
                End If
                Me.lblResult.Text = String.Format("{0} item(s) found", Me.ListPack.Count)
            Else
                For i As Integer = 0 To DataToview - 1
                    Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "PACK_ID").ToString())
                    With LVItem
                        .SubItems.Add(ValueRow(DV, DV(i), "PACK_NAME").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "QUANTITY").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "DEVIDE_FACTOR").ToString())
                    End With
                    .Items.Add(LVItem)
                    If i Mod StepStatis = 0 Then
                        If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                            Me.ProgresBar1.Value += 1
                        End If
                    End If
                Next
                Me.lblResult.Text = String.Format("{0} item(s) found", DataToview)
            End If
        End With
    End Sub
    Private Sub CreateColumnsDistributor()
        With Me.ListView1
            .Items.Clear()
            .Groups.Clear()
            .Columns.Clear()
            .Columns.Add("DISTRIBUTOR_ID", 110, HorizontalAlignment.Left)
            .Columns.Add("DISTRIBUTOR_NAME", 240, HorizontalAlignment.Left)
            .Columns.Add("PARENT_DISTRIBUTOR_ID", 110, HorizontalAlignment.Left)
            .Columns.Add("TERRITORY_ID", 110, HorizontalAlignment.Left)
            .Columns.Add("NPWP", 110, HorizontalAlignment.Left)
            .Columns.Add("MAX_DISC_PER_OA", 120, HorizontalAlignment.Right) 'MAX_DISC_PER_PO
            .Columns.Add("ADDRESS", 200, HorizontalAlignment.Left)
            .Columns.Add("CONTACT", 140, HorizontalAlignment.Left)
            .Columns.Add("PHONE", 120, HorizontalAlignment.Left)
            .Columns.Add("FAX", 110, HorizontalAlignment.Left)
            .Columns.Add("HP", 110, HorizontalAlignment.Left)
            .Columns.Add("TERRITORY_AREA", 150, HorizontalAlignment.Left)
            .Columns.Add("REGIONAL_AREA", 170, HorizontalAlignment.Left)
        End With
    End Sub
    Private Sub FilListViewDistributor(ByVal DV As DataView, ByVal IsViewResult As Boolean)
        With Me.ListView1
            Dim DataToview As Integer = DV.Count
            .Groups.Clear()
            .Items.Clear()
            Me.ProgresBar1.Value = 1
            Me.ProgresBar1.Minimum = 1
            Dim StepStatis As Integer = DV.Count / 100
            If DV.Count < 100 Then
                StepStatis = Convert.ToDecimal(DV.Count)
            End If
            Me.ProgresBar1.Show()
            Me.ProgresBar1.Maximum = 100
            If IsViewResult Then
                If Me.ListDistributor.Count > 0 Then
                    For i As Integer = 0 To DataToview - 1
                        Dim DistributorID As String = DV(i)("DISTRIBUTOR_ID").ToString()
                        If ListDistributor.Contains(DistributorID) Then
                            Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "DISTRIBUTOR_ID").ToString())
                            With LVItem
                                .SubItems.Add(ValueRow(DV, DV(i), "DISTRIBUTOR_NAME").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "PARENT_DISTRIBUTOR_ID").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "TERRITORY_ID").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "NPWP").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "MAX_DISC_PER_PO").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "ADDRESS").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "CONTACT").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "PHONE").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "FAX").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "HP").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "TERRITORY_AREA").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "REGIONAL_AREA").ToString())
                            End With
                            .Items.Add(LVItem)
                            .Items(LVItem.Index).Checked = True
                        End If
                        If i Mod StepStatis = 0 Then
                            If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                                Me.ProgresBar1.Value += 1
                            End If
                        End If
                    Next
                End If
                Me.lblResult.Text = String.Format("{0} item(s) found", Me.ListDistributor.Count)
            Else
                For i As Integer = 0 To DataToview - 1
                    Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "DISTRIBUTOR_ID").ToString())
                    With LVItem
                        .SubItems.Add(ValueRow(DV, DV(i), "DISTRIBUTOR_NAME").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "PARENT_DISTRIBUTOR_ID").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "TERRITORY_ID").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "NPWP").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "MAX_DISC_PER_PO").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "ADDRESS").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "CONTACT").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "PHONE").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "FAX").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "HP").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "TERRITORY_AREA").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "REGIONAL_AREA").ToString())
                    End With
                    .Items.Add(LVItem)
                    If i Mod StepStatis = 0 Then
                        If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                            Me.ProgresBar1.Value += 1
                        End If
                    End If
                Next
                Me.lblResult.Text = String.Format("{0} item(s) found", DataToview)
            End If
        End With
    End Sub
    Private Sub CreateColumnsAgreement()
        With Me.ListView1
            .Items.Clear()
            .Groups.Clear()
            .Columns.Clear()
            '.Groups.Add(New ListViewGroup("AGREEMENT_NO", "AGREEMENT_NO"))
            .Columns.Add("AGREEMENT_NO", 120, HorizontalAlignment.Left)
            .Columns.Add("DISTRIBUTOR_ID", 110, HorizontalAlignment.Left)
            .Columns.Add("DISTRIBUTOR_NAME", 160, HorizontalAlignment.Left)
            .Columns.Add("AGREEMENT_NAME", 140, HorizontalAlignment.Left) 'AGREEMENT_DESC
            .Columns.Add("BRANDPACK_ID", 120, HorizontalAlignment.Left)
            .Columns.Add("BRANDPACK_NAME", 240, HorizontalAlignment.Left)
            .Columns.Add("START_DATE", 130, HorizontalAlignment.Left)
            .Columns.Add("END_DATE", 140, HorizontalAlignment.Left)
            .Columns.Add("FLAG", 80, HorizontalAlignment.Left) 'QS_TREATMENT_FLAG
            .Columns.Add("TERRITORY_AREA", 130, HorizontalAlignment.Left)
            .Columns.Add("REGIONAL_AREA", 110, HorizontalAlignment.Left)

        End With
    End Sub
    Private Sub FillListViewAgreement(ByVal Dv As DataView, ByVal IsViewResult As Boolean)
        With Me.ListView1
            Dim DataToview As Integer = Dv.Count
            'If Dv.Count > 100 Then
            '    If Not IsViewResult Then
            '        Me.ShowBallonTip("Data over than 100 rows" & vbCrLf & "For some reasons , system limits viewing data to 100 rows", "you should filter data to view")
            '        Dim LeftData As Integer = Dv.Count - 100
            '        DataToview = Dv.Count - LeftData
            '    End If
            'End If
            .Groups.Clear()
            .Items.Clear()
            Me.ProgresBar1.Value = 1
            Me.ProgresBar1.Minimum = 1
            Dim StepStatis As Integer = Dv.Count / 100
            If Dv.Count < 100 Then
                StepStatis = Convert.ToDecimal(Dv.Count)
            End If
            Me.ProgresBar1.Maximum = 100
            If IsViewResult Then
                If Me.ListAgreement.Count > 0 Then
                    For i As Integer = 0 To DataToview - 1
                        Dim AgreeBrandPack_ID As String = Dv(i)("AGREEMENT_NO").ToString() + Dv(i)("BRANDPACK_ID").ToString()
                        If ListAgreement.Contains(AgreeBrandPack_ID) Then
                            Dim LVItem As New ListViewItem(ValueRow(Dv, Dv(i), "AGREEMENT_NO").ToString())
                            With LVItem
                                .SubItems.Add(ValueRow(Dv, Dv(i), "DISTRIBUTOR_ID").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "DISTRIBUTOR_NAME").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "AGREEMENT_DESC").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "BRANDPACK_ID").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "BRANDPACK_NAME").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "START_DATE").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "END_DATE").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "QS_TREATMENT_FLAG").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "TERRITORY_AREA").ToString())
                                .SubItems.Add(ValueRow(Dv, Dv(i), "REGIONAL_AREA").ToString())
                            End With
                            .Items.Add(LVItem)
                            .Items(LVItem.Index).Checked = True
                        End If
                        If i Mod StepStatis = 0 Then
                            If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                                Me.ProgresBar1.Value += 1
                            End If
                        End If
                    Next
                End If
                Me.lblResult.Text = String.Format("{0} item(s) found", Me.ListAgreement.Count)
            Else
                For i As Integer = 0 To DataToview - 1
                    Dim LVItem As New ListViewItem(ValueRow(Dv, Dv(i), "AGREEMENT_NO").ToString())
                    With LVItem
                        .SubItems.Add(ValueRow(Dv, Dv(i), "DISTRIBUTOR_ID").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(i), "DISTRIBUTOR_NAME").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(i), "AGREEMENT_DESC").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(i), "BRANDPACK_ID").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(i), "BRANDPACK_NAME").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(i), "START_DATE").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(i), "END_DATE").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(i), "QS_TREATMENT_FLAG").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(i), "TERRITORY_AREA").ToString())
                        .SubItems.Add(ValueRow(Dv, Dv(i), "REGIONAL_AREA").ToString())
                    End With
                    .Items.Add(LVItem)
                    If i Mod StepStatis = 0 Then
                        If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                            Me.ProgresBar1.Value += 1
                        End If
                    End If
                Next
                Me.lblResult.Text = String.Format("{0} item(s) found", DataToview)
            End If
        End With
    End Sub
    Private Sub CreateColumnsPriviledge(ByVal IsTM As Boolean)
        With Me.ListView1
            .Items.Clear() : .Groups.Clear() : .Columns.Clear()
            .Columns.Add("IDSystem", 100, HorizontalAlignment.Right)
            If (IsTM) Then
                .Columns.Add("TMCode", 90, HorizontalAlignment.Left)
            End If
            .Columns.Add("UserName", 140, HorizontalAlignment.Left)
            .Columns.Add("Password", 140, HorizontalAlignment.Left)
        End With
    End Sub

    Private Sub FillListViewRSM(ByVal DV As DataView, ByVal IsViewResult As Boolean)
        With Me.ListView1
            Dim DataToview As Integer = DV.Count
            .Groups.Clear()
            .Items.Clear()
            Me.ProgresBar1.Value = 1
            Me.ProgresBar1.Minimum = 1
            Dim StepStatis As Integer = DV.Count / 100
            If DV.Count < 100 Then
                StepStatis = Convert.ToDecimal(DV.Count)
            End If
            Me.ProgresBar1.Show()
            Me.ProgresBar1.Maximum = 100
            If IsViewResult Then
                If Me.ListRSM.Count > 0 Then
                    For i As Integer = 0 To DataToview - 1
                        Dim IDRSM As String = DV(i)("IDApp")
                        If (ListRSM.Contains(IDRSM)) Then
                            Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "IDApp"))
                            With LVItem
                                .SubItems.Add(ValueRow(DV, DV(i), "UserName").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "Password").ToString())
                            End With
                            .Items.Add(LVItem)
                            .Items(LVItem.Index).Checked = True
                        End If
                        If i Mod StepStatis = 0 Then
                            If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                                Me.ProgresBar1.Value += 1
                            End If
                        End If
                    Next
                End If
                Me.lblResult.Text = String.Format("{0} item(s) found", Me.ListDistributor.Count)
            Else
                For i As Integer = 0 To DataToview - 1
                    Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "IDApp"))
                    With LVItem
                        .SubItems.Add(ValueRow(DV, DV(i), "UserName").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "Password").ToString())
                    End With
                    .Items.Add(LVItem)
                    If i Mod StepStatis = 0 Then
                        If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                            Me.ProgresBar1.Value += 1
                        End If
                    End If
                Next
                Me.lblResult.Text = String.Format("{0} item(s) found", DataToview)
            End If

        End With

    End Sub

    Private Sub FillListViewPM(ByVal DV As DataView, ByVal IsViewResult As Boolean)
        With Me.ListView1
            Dim DataToview As Integer = DV.Count
            .Groups.Clear()
            .Items.Clear()
            Me.ProgresBar1.Value = 1
            Me.ProgresBar1.Minimum = 1
            Dim StepStatis As Integer = DV.Count / 100
            If DV.Count < 100 Then
                StepStatis = Convert.ToDecimal(DV.Count)
            End If
            Me.ProgresBar1.Show()
            Me.ProgresBar1.Maximum = 100
            If IsViewResult Then
                If Me.ListPM.Count > 0 Then
                    For i As Integer = 0 To DataToview - 1
                        Dim IDPM As String = DV(i)("IDApp")
                        If (ListPM.Contains(IDPM)) Then
                            Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "IDApp"))
                            With LVItem
                                .SubItems.Add(ValueRow(DV, DV(i), "UserName").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "Password").ToString())
                            End With
                            .Items.Add(LVItem)
                            .Items(LVItem.Index).Checked = True
                        End If
                        If i Mod StepStatis = 0 Then
                            If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                                Me.ProgresBar1.Value += 1
                            End If
                        End If
                    Next
                End If
                Me.lblResult.Text = String.Format("{0} item(s) found", Me.ListPM.Count)
            Else
                For i As Integer = 0 To DataToview - 1
                    Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "IDApp"))
                    With LVItem
                        .SubItems.Add(ValueRow(DV, DV(i), "UserName").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "Password").ToString())
                    End With
                    .Items.Add(LVItem)
                    If i Mod StepStatis = 0 Then
                        If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                            Me.ProgresBar1.Value += 1
                        End If
                    End If
                Next
                Me.lblResult.Text = String.Format("{0} item(s) found", DataToview)
            End If

        End With

    End Sub

    Private Sub FillListViewTM(ByVal DV As DataView, ByVal IsViewResult As Boolean)
        With Me.ListView1
            Dim DataToview As Integer = DV.Count
            .Groups.Clear()
            .Items.Clear()
            Me.ProgresBar1.Value = 1
            Me.ProgresBar1.Minimum = 1
            Dim StepStatis As Integer = DV.Count / 100
            If DV.Count < 100 Then
                StepStatis = Convert.ToDecimal(DV.Count)
            End If
            Me.ProgresBar1.Show()
            Me.ProgresBar1.Maximum = 100
            If IsViewResult Then
                If Me.ListTM.Count > 0 Then
                    For i As Integer = 0 To DataToview - 1
                        Dim IDTM As String = DV(i)("IDApp")
                        If (ListTM.Contains(IDTM)) Then
                            Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "IDApp"))
                            With LVItem
                                .SubItems.Add(ValueRow(DV, DV(i), "TMCode").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "UserName").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "Password").ToString())
                            End With
                            .Items.Add(LVItem)
                            .Items(LVItem.Index).Checked = True
                        End If
                        If i Mod StepStatis = 0 Then
                            If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                                Me.ProgresBar1.Value += 1
                            End If
                        End If
                    Next
                End If
                Me.lblResult.Text = String.Format("{0} item(s) found", Me.ListTM.Count)
            Else
                For i As Integer = 0 To DataToview - 1
                    Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "IDApp"))
                    With LVItem
                        .SubItems.Add(ValueRow(DV, DV(i), "TMCode").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "UserName").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "Password").ToString())
                    End With
                    .Items.Add(LVItem)
                    If i Mod StepStatis = 0 Then
                        If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                            Me.ProgresBar1.Value += 1
                        End If
                    End If
                Next
                Me.lblResult.Text = String.Format("{0} item(s) found", DataToview)
            End If

        End With

    End Sub

    Private Sub CreateColumnsFS()
        With Me.ListView1
            .Items.Clear()
            .Groups.Clear()
            .Columns.Clear()
            .Columns.Add("IDSystem", 100, HorizontalAlignment.Right)
            .Columns.Add("FS_ID", 90, HorizontalAlignment.Left)
            .Columns.Add("UserName", 140, HorizontalAlignment.Left)
            .Columns.Add("Password", 140, HorizontalAlignment.Left)
        End With
    End Sub
    Private Sub fillListViewFS(ByVal DV As DataView, ByVal IsViewResult As Boolean)
        With Me.ListView1
            Dim DataToview As Integer = DV.Count
            .Groups.Clear() : .Items.Clear()
            Me.ProgresBar1.Value = 1
            Me.ProgresBar1.Minimum = 1
            Dim StepStatis As Integer = DV.Count / 100
            If DV.Count < 100 Then
                StepStatis = Convert.ToDecimal(DV.Count)
            End If
            Me.ProgresBar1.Show()
            Me.ProgresBar1.Maximum = 100
            If IsViewResult Then
                If Me.listFS.Count > 0 Then
                    For i As Integer = 0 To DataToview - 1
                        Dim IDSystem As String = DV(i)("IDApp")
                        If (listFS.Contains(IDSystem)) Then
                            Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "IDApp"))
                            With LVItem
                                .SubItems.Add(ValueRow(DV, DV(i), "FS_ID").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "UserName").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "Password").ToString())
                            End With
                            .Items.Add(LVItem)
                            .Items(LVItem.Index).Checked = True
                        End If
                        If i Mod StepStatis = 0 Then
                            If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                                Me.ProgresBar1.Value += 1
                            End If
                        End If
                    Next
                End If
                Me.lblResult.Text = String.Format("{0} item(s) found", Me.listFS.Count)
            Else
                For i As Integer = 0 To DataToview - 1
                    Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "IDApp"))
                    With LVItem
                        .SubItems.Add(ValueRow(DV, DV(i), "FS_ID").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "UserName").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "Password").ToString())
                    End With
                    .Items.Add(LVItem)
                    If i Mod StepStatis = 0 Then
                        If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                            Me.ProgresBar1.Value += 1
                        End If
                    End If
                Next
                Me.lblResult.Text = String.Format("{0} item(s) found", DataToview)
            End If

        End With
    End Sub
    Private Sub CreateColumnTerritory()
        With Me.ListView1
            .Items.Clear()
            .Groups.Clear()
            .Columns.Clear()
            'TERRITORY_ID,TERRITORY_AREA,TERRITORY_DESCRIPTION,REGIONAL_ID,PARENT_TERRITORY
            .Columns.Add("TERRITORY_ID", 110, HorizontalAlignment.Left)
            .Columns.Add("TERRITORY_AREA", 150, HorizontalAlignment.Left)
            .Columns.Add("REGIONAL_ID", 100, HorizontalAlignment.Left)
            .Columns.Add("REGIONAL_AREA", 150, HorizontalAlignment.Left)
            .Columns.Add("PARENT_TERRITORY_ID", 150, HorizontalAlignment.Left)
            .Columns.Add("PARENT_TERRITORY", 150, HorizontalAlignment.Left)
        End With
    End Sub
    Private Sub fillListViewTerritory(ByVal DV As DataView, ByVal IsViewResult As Boolean)
        With Me.ListView1
            Dim DataToview As Integer = DV.Count
            .Groups.Clear() : .Items.Clear()
            Me.ProgresBar1.Value = 1
            Me.ProgresBar1.Minimum = 1
            Dim StepStatis As Integer = DV.Count / 100
            If DV.Count < 100 Then
                StepStatis = Convert.ToDecimal(DV.Count)
            End If
            Me.ProgresBar1.Show()
            Me.ProgresBar1.Maximum = 100
            If IsViewResult Then
                If Me.ListTerritory.Count > 0 Then
                    For i As Integer = 0 To DataToview - 1
                        Dim TerritoryCode As String = DV(i)("TERRITORY_ID")
                        If (ListTerritory.Contains(TerritoryCode)) Then
                            Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "TERRITORY_ID"))
                            With LVItem
                                .SubItems.Add(ValueRow(DV, DV(i), "TERRITORY_AREA").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "REGIONAL_ID").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "REGIONAL_AREA").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "PARENT_TERRITORY_ID").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "PARENT_TERRITORY").ToString())
                            End With
                            .Items.Add(LVItem)
                            .Items(LVItem.Index).Checked = True
                        End If
                        If i Mod StepStatis = 0 Then
                            If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                                Me.ProgresBar1.Value += 1
                            End If
                        End If
                    Next
                End If
                Me.lblResult.Text = String.Format("{0} item(s) found", Me.ListTerritory.Count)
            Else
                For i As Integer = 0 To DataToview - 1
                    Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "TERRITORY_ID"))
                    With LVItem
                        .SubItems.Add(ValueRow(DV, DV(i), "TERRITORY_AREA").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "REGIONAL_ID").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "REGIONAL_AREA").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "PARENT_TERRITORY_ID").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "PARENT_TERRITORY").ToString())
                    End With
                    .Items.Add(LVItem)
                    If i Mod StepStatis = 0 Then
                        If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                            Me.ProgresBar1.Value += 1
                        End If
                    End If
                Next
                Me.lblResult.Text = String.Format("{0} item(s) found", DataToview)
            End If
        End With
    End Sub
    Private Sub CreateColumnsRegional()
        With Me.ListView1
            .Items.Clear()
            .Groups.Clear()
            .Columns.Clear()
            'TERRITORY_ID,TERRITORY_AREA,TERRITORY_DESCRIPTION,REGIONAL_ID,PARENT_TERRITORY
            .Columns.Add("REGIONAL_ID", 100, HorizontalAlignment.Left)
            .Columns.Add("REGIONAL_AREA", 170, HorizontalAlignment.Left)
            .Columns.Add("PARENT_REGIONAL_ID", 150, HorizontalAlignment.Left)
            .Columns.Add("PARENT_REGIONAL", 150, HorizontalAlignment.Left)
            .Columns.Add("MANAGER", 150, HorizontalAlignment.Left)
        End With
    End Sub
    Private Sub fillListRegional(ByVal DV As DataView, ByVal IsViewResult As Boolean)
        With Me.ListView1
            Dim DataToview As Integer = DV.Count
            .Groups.Clear() : .Items.Clear()
            Me.ProgresBar1.Value = 1
            Me.ProgresBar1.Minimum = 1
            Dim StepStatis As Integer = DV.Count / 100
            If DV.Count < 100 Then
                StepStatis = Convert.ToDecimal(DV.Count)
            End If
            Me.ProgresBar1.Show()
            Me.ProgresBar1.Maximum = 100
            If IsViewResult Then
                If Me.ListRegional.Count > 0 Then
                    For i As Integer = 0 To DataToview - 1
                        Dim RegionalCode As String = DV(i)("REGIONAL_ID")
                        If (ListRegional.Contains(RegionalCode)) Then
                            Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "REGIONAL_ID"))
                            With LVItem
                                .SubItems.Add(ValueRow(DV, DV(i), "REGIONAL_AREA").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "PARENT_REGIONAL_ID").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "PARENT_REGIONAL").ToString())
                                .SubItems.Add(ValueRow(DV, DV(i), "MANAGER").ToString())
                            End With
                            .Items.Add(LVItem)
                            .Items(LVItem.Index).Checked = True
                        End If
                        If i Mod StepStatis = 0 Then
                            If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                                Me.ProgresBar1.Value += 1
                            End If
                        End If
                    Next
                End If
                Me.lblResult.Text = String.Format("{0} item(s) found", Me.ListRegional.Count)
            Else
                For i As Integer = 0 To DataToview - 1
                    Dim LVItem As New ListViewItem(ValueRow(DV, DV(i), "REGIONAL_ID"))
                    With LVItem
                        .SubItems.Add(ValueRow(DV, DV(i), "REGIONAL_AREA").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "PARENT_REGIONAL_ID").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "PARENT_REGIONAL").ToString())
                        .SubItems.Add(ValueRow(DV, DV(i), "MANAGER").ToString())
                    End With
                    .Items.Add(LVItem)
                    If i Mod StepStatis = 0 Then
                        If Me.ProgresBar1.Value < Me.ProgresBar1.Maximum Then
                            Me.ProgresBar1.Value += 1
                        End If
                    End If
                Next
                Me.lblResult.Text = String.Format("{0} item(s) found", DataToview)
            End If
        End With
    End Sub
    Private Sub BtnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click, btnCancel.Click
        Try
            Select Case DirectCast(sender, Janus.Windows.EditControls.UIButton).Name
                Case "btnOK"
                    If (Me.rdbAdmin.Checked = False And Me.rdbPM.Checked = False And Me.rdbRSM.Checked = False And Me.rdbTM.Checked = False And Me.rdbFS.Checked = False) Then
                        Me.ShowMessageInfo("type of file is recomended to choose " & vbCrLf & "To avoid mistaking sending file")
                        Return
                    End If
                    Me.Cursor = Cursors.WaitCursor
                    Dim SendTo As String = ""
                    If Me.rdbAdmin.Checked = True Then
                        SendTo = "Admin"
                    ElseIf Me.rdbPM.Checked = True Then
                        SendTo = "PM"
                    ElseIf Me.rdbRSM.Checked = True Then
                        SendTo = "RSM"
                    ElseIf Me.rdbTM.Checked = True Then
                        SendTo = "TM"
                    ElseIf Me.rdbFS.Checked Then
                        SendTo = "FS"
                    End If
                    Select Case Me.ViewingData
                        Case ViewData.Agreement
                            ''LOOPING BRANDPACK APA SAJA YANG DI CHECKLIST
                            Dim DV As DataView = Me.clsSendData.DS.Tables(7).DefaultView
                            DV.RowFilter = ""
                            If Me.ListAgreement.Count > 0 Then
                                If Me.ShowConfirmedMessage("Confirmed to make file?") = Windows.Forms.DialogResult.No Then
                                    Return
                                End If
                                DV.Sort = "AGREE_BRANDPACK_ID"
                                For i As Integer = 0 To DV.Count - 1
                                    If Not ListAgreement.Contains(DV(i)("AGREE_BRANDPACK_ID").ToString()) Then
                                        DV(i).Delete()
                                        i -= 1
                                    End If
                                    If i = DV.Count - 1 Then
                                        Exit For
                                    End If
                                Next
                                Dim DVAgreeBrand As DataView = Me.clsSendData.DS.Tables(6).DefaultView() ''LOOPING BRANDNYA
                                DV.Sort = "AGREE_BRAND_ID"
                                For i As Integer = 0 To DVAgreeBrand.Count - 1
                                    If (DV.Find(DVAgreeBrand(i)("AGREE_BRAND_ID")) = -1) Then
                                        DVAgreeBrand(i).Delete() : i -= 1
                                    End If
                                    If i = DVAgreeBrand.Count - 1 Then
                                        Exit For
                                    End If
                                Next
                                Dim DVAgreement As DataView = Me.clsSendData.DS.Tables(5).DefaultView()
                                DVAgreeBrand.Sort = "AGREEMENT_NO"
                                For i As Integer = 0 To DVAgreement.Count - 1
                                    If (DVAgreeBrand.Find(DVAgreement(i)("AGREEMENT_NO")) = -1) Then
                                        DVAgreement(i).Delete() : i -= 1
                                    End If
                                    If i = DVAgreement.Count - 1 Then
                                        Exit For
                                    End If
                                Next
                                Dim DVDistrAgreement As DataView = Me.clsSendData.DS.Tables(4).DefaultView()
                                DVAgreement.Sort = "AGREEMENT_NO"
                                For i As Integer = 0 To DVDistrAgreement.Count - 1
                                    If (DVAgreement.Find(DVDistrAgreement(i)("AGREEMENT_NO")) = -1) Then
                                        DVDistrAgreement(i).Delete() : i -= 1
                                    End If
                                    If i = DVDistrAgreement.Count - 1 Then
                                        Exit For
                                    End If
                                Next

                                If (DV.Count > 0) Then
                                    'DV.ToTable().AcceptChanges()
                                    Dim SPD As New SaveFileDialog()
                                    With SPD
                                        .Title = "Save file to send to " & SendTo
                                        .OverwritePrompt = True
                                        .DefaultExt = ".PKD"
                                        .Filter = "PKD files|*.PKD"
                                        .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                    End With
                                    If SPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                        Dim FileName As String = SPD.FileName.Replace(".PKD", "")
                                        FileName &= "_" & NufarmBussinesRules.SharedClass.ServerDate.ToLongDateString()
                                        FileName &= "_ For_" & SendTo & ".PKD"
                                        Dim Ds As New DataSet("DsAgreement")
                                        Ds.Tables.Add(DV.ToTable().Copy()) : Ds.Tables.Add(DVAgreeBrand.ToTable().Copy()) : Ds.Tables.Add(DVAgreement.ToTable().Copy())
                                        Ds.Tables.Add(DVDistrAgreement.ToTable().Copy())
                                        Ds.WriteXml(FileName, XmlWriteMode.WriteSchema)
                                        MessageBox.Show("Data Created to " + FileName)
                                    End If
                                End If
                            End If
                        Case ViewData.Brand
                            'table0 pack
                            'table1 brand
                            Dim DV As DataView = Me.clsSendData.DS.Tables(1).DefaultView
                            DV.RowFilter = ""
                            If Me.ListBrand.Count > 0 Then
                                DV.Sort = "BRAND_ID"
                                For i As Integer = 0 To DV.Count - 1
                                    If Not ListBrand.Contains(DV(i)("BRAND_ID").ToString()) Then
                                        DV(i).Delete()
                                        i -= 1
                                    End If
                                    If i = DV.Count - 1 Then
                                        Exit For
                                    End If
                                Next
                                If (DV.Count > 0) Then
                                    Dim SPD As New SaveFileDialog()
                                    With SPD
                                        .Title = "Save file to send to " & SendTo
                                        .OverwritePrompt = True
                                        .DefaultExt = ".Brand"
                                        .Filter = "Brand files|*.Brand"
                                        .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                    End With
                                    If SPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                        Dim FileName As String = SPD.FileName.Replace(".Brand", "")
                                        FileName &= "_" & NufarmBussinesRules.SharedClass.ServerDate.ToLongDateString()
                                        FileName &= "_ For_" & SendTo & ".Brand"
                                        Dim Ds As New DataSet("DsBrand") : Ds.Tables.Add(DV.ToTable().Copy())
                                        Ds.WriteXml(FileName, XmlWriteMode.WriteSchema)
                                        MessageBox.Show("Data Created to " + FileName)
                                    End If
                                End If
                            End If
                        Case ViewData.BrandPack
                            'table2 brandpack
                            Dim DV As DataView = Me.clsSendData.DS.Tables(2).DefaultView
                            DV.RowFilter = ""
                            If Me.ListBrandPack.Count > 0 Then
                                DV.Sort = "BRANDPACK_ID"
                                For i As Integer = 0 To DV.Count - 1
                                    If Not ListBrandPack.Contains(DV(i)("BRANDPACK_ID").ToString()) Then
                                        DV(i).Delete()
                                        i -= 1
                                    End If
                                    If i = DV.Count - 1 Then
                                        Exit For
                                    End If
                                Next
                                If (DV.Count > 0) Then
                                    'DV.ToTable().AcceptChanges()
                                    Dim SPD As New SaveFileDialog()
                                    With SPD
                                        .Title = "Save file to send to " & SendTo
                                        .OverwritePrompt = True
                                        .DefaultExt = ".BrandPack"
                                        .Filter = "BrandPack files|*.BrandPack"
                                        .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                    End With
                                    If SPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                        Dim FileName As String = SPD.FileName.Replace(".BrandPack", "")
                                        FileName &= "_" & NufarmBussinesRules.SharedClass.ServerDate.ToLongDateString()
                                        FileName &= "_ For_" & SendTo & ".BrandPack"
                                        Dim ds As New DataSet("DSBrandPack")
                                        ds.Tables.Add(DV.ToTable().Copy())
                                        ds.WriteXml(FileName, XmlWriteMode.WriteSchema)
                                        MessageBox.Show("Data Created to " + FileName)
                                    End If
                                End If
                            End If
                        Case ViewData.Distributor
                            'table3 distributor
                            Dim DV As DataView = Me.clsSendData.DS.Tables(3).DefaultView
                            DV.RowFilter = ""
                            If Me.ListDistributor.Count > 0 Then
                                DV.Sort = "DISTRIBUTOR_ID"
                                For i As Integer = 0 To DV.Count - 1
                                    If Not ListDistributor.Contains(DV(i)("DISTRIBUTOR_ID").ToString()) Then
                                        DV(i).Delete()
                                        i -= 1
                                    End If
                                    If i = DV.Count - 1 Then
                                        Exit For
                                    End If
                                Next
                                If (DV.Count > 0) Then
                                    'DV.ToTable().AcceptChanges()
                                    Dim SPD As New SaveFileDialog()
                                    With SPD
                                        .Title = "Save file to send to " & SendTo
                                        .OverwritePrompt = True
                                        .DefaultExt = ".Distributor"
                                        .Filter = "Distributor files|*.Distributor"
                                        .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                    End With
                                    If SPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

                                        Dim FileName As String = SPD.FileName.Replace(".Distributor", "")
                                        FileName &= "_" & NufarmBussinesRules.SharedClass.ServerDate.ToLongDateString()
                                        FileName &= "_ For_" & SendTo & ".Distributor"
                                        Dim ds As New DataSet("DSDistributor")
                                        ds.Tables.Add(DV.ToTable().Copy())
                                        Dim dtTable As DataTable = Me.clsSendData.GetTerritory(Me.ListDistributor)
                                        If (dtTable.Rows.Count > 0) Then
                                            ds.Tables.Add(dtTable)
                                        End If
                                        ds.AcceptChanges()
                                        ds.WriteXml(FileName, XmlWriteMode.WriteSchema)
                                        MessageBox.Show("Data Created to " + FileName)
                                    End If
                                End If
                            End If
                        Case ViewData.Pack
                            Dim DV As DataView = Me.clsSendData.DS.Tables(0).DefaultView
                            DV.RowFilter = ""
                            If Me.ListPack.Count > 0 Then
                                DV.Sort = "PACK_ID"
                                For i As Integer = 0 To DV.Count - 1
                                    If Not ListPack.Contains(DV(i)("PACK_ID").ToString()) Then
                                        DV(i).Delete()
                                        i -= 1
                                    End If
                                    If i = DV.Count - 1 Then
                                        Exit For
                                    End If
                                Next
                                If (DV.Count > 0) Then
                                    'DV.ToTable().AcceptChanges()
                                    Dim SPD As New SaveFileDialog()
                                    With SPD
                                        .Title = "Save file to send to " & SendTo
                                        .OverwritePrompt = True
                                        .DefaultExt = ".Pack"
                                        .Filter = "Packaging files|*.Pack"
                                        .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                    End With
                                    If SPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                        Dim FileName As String = SPD.FileName.Replace(".Pack", "")
                                        FileName &= "_" & NufarmBussinesRules.SharedClass.ServerDate.ToLongDateString()
                                        FileName &= "_ For_" & SendTo & ".Pack"
                                        Dim ds As New DataSet("DSPack")
                                        ds.Tables.Add(DV.ToTable().Copy())
                                        ds.WriteXml(FileName, XmlWriteMode.WriteSchema)
                                        MessageBox.Show("Data Created to " + FileName)
                                    End If

                                End If
                            End If
                        Case ViewData.PriviledgePM
                            If Me.clsSendData.DS.Tables.Contains("PriviledgePM") Then
                                If Me.ListPM.Count <= 0 Then
                                    Me.ShowMessageInfo("Can not create priviledge" & vbCrLf & "Please check UserName")
                                    Return
                                ElseIf Me.ListPM.Count > 1 Then
                                    MessageBox.Show("Priviledge only applies for one for each user", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Return
                                End If
                                Dim DV As DataView = Me.clsSendData.DS.Tables(14).DefaultView
                                DV.RowFilter = "IDApp = " & ListPM(0)
                                Dim Hs As New Hashtable() : Hs.Add("TypeUser", "PM") : Hs.Add("PM", DV(0)("UserName"))
                                Hs.Add("Password", DV(0)("Password")) : Hs.Add("CreatedBy", DV(0)("CreatedBy"))
                                Hs.Add("CreatedDate", Convert.ToDateTime(DV(0)("CreatedDate")))
                                Hs.Add("ModifiedBy", NufarmBussinesRules.User.UserLogin.UserName)
                                Hs.Add("ModifiedDate", Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()))
                                Dim SFD As New SaveFileDialog()
                                With SFD
                                    .Title = "Save file to send to " & SendTo
                                    .OverwritePrompt = True
                                    .DefaultExt = ".Priviledge"
                                    .Filter = "Priviledges file|*.Priviledge"
                                    .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                End With
                                If SFD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                    Dim Formatter As New SoapFormatter() : Dim FileName As String = SFD.FileName.Replace(".Priviledge", "")
                                    FileName &= "_" & SendTo & "_" & DV(0)("UserName").ToString() & ".Priviledge"
                                    Using FS As New IO.FileStream(FileName, IO.FileMode.Create, IO.FileAccess.Write)
                                        Formatter.Serialize(FS, Hs)
                                        IO.File.SetAttributes(FileName, IO.FileAttributes.ReadOnly) : FS.Flush()
                                    End Using
                                    Me.ShowMessageInfo("Priviledge for " & SendTo & "  " & DV(0)("UserName").ToString() & " Created to " & FileName)
                                End If
                            End If
                        Case ViewData.PriviledgeRSM
                            If Me.ListRSM.Count <= 0 Then
                                Me.ShowMessageInfo("Can not create priviledge" & vbCrLf & "Please check UserName")
                                Return
                            ElseIf Me.ListRSM.Count > 1 Then
                                MessageBox.Show("Priviledge only applies for one for each user", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return
                            End If
                            Dim DV As DataView = Me.clsSendData.DS.Tables(12).DefaultView
                            DV.RowFilter = "IDApp = " & ListRSM(0)
                            Dim RegionalCode As String = DV(0)("RegionalCode").ToString(), ParentRegional As String = ""
                            Dim rows() As DataRow = Me.clsSendData.DS.Tables("table10").Select("REGIONAL_ID = '" + RegionalCode + "'")
                            If rows.Length > 0 Then
                                ParentRegional = IIf(IsDBNull(rows(0)("PARENT_REGIONAL")), "", rows(0)("PARENT_REGIONAL"))
                            End If
                            Dim Hs As New Hashtable() : Hs.Add("TypeUser", "RSM") : Hs.Add("RegionalCode", RegionalCode)
                            Hs.Add("RegionalManager", DV(0)("UserName")) : Hs.Add("RegionalArea", DV(0)("RegionalArea"))
                            Hs.Add("ParentRegional", ParentRegional)
                            Hs.Add("Password", DV(0)("Password")) : Hs.Add("CreatedBy", DV(0)("CreatedBy"))
                            Hs.Add("CreatedDate", Convert.ToDateTime(DV(0)("CreatedDate")))
                            Hs.Add("ModifiedBy", NufarmBussinesRules.User.UserLogin.UserName)
                            Hs.Add("ModifiedDate", Convert.ToDateTime(NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString()))
                            Dim SFD As New SaveFileDialog()
                            With SFD
                                .Title = "Save file to send to " & SendTo
                                .OverwritePrompt = True
                                .DefaultExt = ".Priviledge"
                                .Filter = "Priviledge Files|*.Priviledge"
                                .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                            End With
                            If SFD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim Formatter As New SoapFormatter() : Dim FileName As String = SFD.FileName.Replace(".Priviledge", "")
                                FileName &= "_" & SendTo & "_" & DV(0)("UserName").ToString() & ".Priviledge"
                                Using FS As New IO.FileStream(FileName, IO.FileMode.Create, IO.FileAccess.Write)
                                    Formatter.Serialize(FS, Hs)
                                    IO.File.SetAttributes(FileName, IO.FileAttributes.ReadOnly) : FS.Flush()
                                End Using
                                Me.ShowMessageInfo("Priviledge for " & SendTo & "  " & DV(0)("UserName").ToString() & " Created to " & FileName)
                            End If

                        Case ViewData.priviledgeTM
                            If Me.ListTM.Count <= 0 Then
                                Me.ShowMessageInfo("Can not create priviledge" & vbCrLf & "Please check UserName")
                                Return
                            ElseIf Me.ListTM.Count > 1 Then
                                MessageBox.Show("Priviledge only applies for one for each user", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return
                            End If
                            Dim DV As DataView = Me.clsSendData.DS.Tables(13).DefaultView
                            DV.RowFilter = "IDApp = " & ListTM(0)
                            Dim Hs As New Hashtable(), TerritoryCode As String = "", ParentTerritory As String = ""
                            Hs.Add("TypeUser", "TM") : Hs.Add("TerritoryManager", DV(0)("UserName"))
                            Hs.Add("Password", DV(0)("Password"))
                            TerritoryCode = DV(0)("TerritoryCode")
                            Hs.Add("TerritoryCode", TerritoryCode)
                            Dim rows() As DataRow = Me.clsSendData.DS.Tables("table8").Select("TERRITORY_ID = '" + TerritoryCode + "'")
                            If rows.Length > 0 Then
                                ParentTerritory = IIf(IsNothing(rows(0)("PARENT_TERRITORY_ID")) Or IsDBNull(rows(0)("PARENT_TERRITORY_ID")), "", rows(0)("PARENT_TERRITORY_ID").ToString())
                            End If
                            Hs.Add("TerritoryArea", DV(0)("TerritoryArea")) : Hs.Add("CreatedBy", DV(0)("CreatedBy"))
                            Hs.Add("RegionalCode", DV(0)("RegionalCode"))
                            Hs.Add("ParentTerritory", ParentTerritory)
                            Hs.Add("TMCode", DV(0)("TMCode"))
                            Hs.Add("TM_ID", DV(0)("TM_ID")) : Hs.Add("CreatedDate", DV(0)("CreatedDate"))
                            Hs.Add("ModifiedBy", NufarmBussinesRules.User.UserLogin.UserName)
                            Hs.Add("ModifiedDate", NufarmBussinesRules.SharedClass.ServerDate)
                            Dim SFD As New SaveFileDialog()
                            With SFD
                                .Title = "Save file to send to " & SendTo
                                .OverwritePrompt = True
                                .DefaultExt = ".Priviledge"
                                .Filter = "Privilege files|*.Priviledge"
                                .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                            End With
                            If SFD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim Formatter As New SoapFormatter() : Dim FileName As String = SFD.FileName.Replace(".Priviledge", "")
                                'FileName &= FileName.Replace(".soap", "")
                                FileName &= "_" & SendTo & "_" & DV(0)("UserName").ToString() & ".Priviledge"
                                Using FS As New IO.FileStream(FileName, IO.FileMode.Create, IO.FileAccess.Write)
                                    Formatter.Serialize(FS, Hs)
                                    IO.File.SetAttributes(FileName, IO.FileAttributes.ReadOnly) : FS.Flush()
                                End Using
                                Me.ShowMessageInfo("Priviledge for " & SendTo & "  " & DV(0)("UserName").ToString() & " Created to " & FileName)
                            End If
                        Case ViewData.PriviledgeFS
                            If Me.listFS.Count <= 0 Then
                                Me.ShowMessageInfo("Can not create priviledge" & vbCrLf & "Please check UserName")
                                Return
                            ElseIf Me.listFS.Count > 1 Then
                                MessageBox.Show("Priviledge only applies for one for each user", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return
                            End If
                            Dim DV As DataView = Me.tblFS.DefaultView
                            DV.RowFilter = "IDApp = " & listFS(0)
                            Dim Hs As New Hashtable(), TerritoryCode As String = "", ParentTerritory As String = ""
                            Hs.Add("TypeUser", "FS") : Hs.Add("FS_Name", DV(0)("UserName"))
                            Hs.Add("Password", DV(0)("Password"))
                            TerritoryCode = DV(0)("TerritoryCode")
                            Hs.Add("TerritoryCode", TerritoryCode)
                            Dim rows() As DataRow = Me.clsSendData.DS.Tables("table8").Select("TERRITORY_ID = '" + TerritoryCode + "'")
                            If rows.Length > 0 Then
                                ParentTerritory = IIf(IsNothing(rows(0)("PARENT_TERRITORY_ID")) Or IsDBNull(rows(0)("PARENT_TERRITORY_ID")), "", rows(0)("PARENT_TERRITORY_ID").ToString())
                            End If
                            Hs.Add("TerritoryArea", DV(0)("TerritoryArea")) : Hs.Add("CreatedBy", DV(0)("CreatedBy"))
                            Hs.Add("RegionalCode", DV(0)("RegionalCode")) : Hs.Add("FS_ID", DV(0)("FS_ID"))
                            Hs.Add("ParentTerritory", ParentTerritory)
                            Hs.Add("CreatedDate", DV(0)("CreatedDate"))
                            Hs.Add("ModifiedBy", NufarmBussinesRules.User.UserLogin.UserName)
                            Hs.Add("ModifiedDate", NufarmBussinesRules.SharedClass.ServerDate)
                            Dim SFD As New SaveFileDialog()
                            With SFD
                                .Title = "Save file to send to " & SendTo
                                .OverwritePrompt = True
                                .DefaultExt = ".Priviledge"
                                .Filter = "Privilege files|*.Priviledge"
                                .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                            End With
                            If SFD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim Formatter As New SoapFormatter() : Dim FileName As String = SFD.FileName.Replace(".Priviledge", "")
                                'FileName &= FileName.Replace(".soap", "")
                                FileName &= "_" & SendTo & "_" & DV(0)("UserName").ToString() & ".Priviledge"
                                Using FS As New IO.FileStream(FileName, IO.FileMode.Create, IO.FileAccess.Write)
                                    Formatter.Serialize(FS, Hs)
                                    IO.File.SetAttributes(FileName, IO.FileAttributes.ReadOnly) : FS.Flush()
                                End Using
                                Me.ShowMessageInfo("Priviledge for " & SendTo & "  " & DV(0)("UserName").ToString() & " Created to " & FileName)
                            End If
                        Case ViewData.Regional
                            'table10 Regional
                            Dim DV As DataView = Me.clsSendData.DS.Tables(10).DefaultView
                            DV.RowFilter = ""
                            If Me.ListRegional.Count > 0 Then
                                DV.Sort = "REGIONAL_ID"
                                For i As Integer = 0 To DV.Count - 1
                                    If Not ListRegional.Contains(DV(i)("REGIONAL_ID").ToString()) Then
                                        DV(i).Delete()
                                        i -= 1
                                    End If
                                    If i = DV.Count - 1 Then
                                        Exit For
                                    End If
                                Next
                                If (DV.Count > 0) Then
                                    'DV.ToTable().AcceptChanges()
                                    Dim SPD As New SaveFileDialog()
                                    With SPD
                                        .Title = "Save file to send to " & SendTo
                                        .OverwritePrompt = True
                                        .DefaultExt = ".rgnl"
                                        .Filter = "Regional data|*.rgnl"
                                        .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                    End With
                                    If SPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                        Dim FileName As String = SPD.FileName.Replace(".rgnl", "")
                                        FileName &= "_" & NufarmBussinesRules.SharedClass.ServerDate.ToLongDateString()
                                        FileName &= "_ For_" & SendTo & ".rgnl"
                                        Dim ds As New DataSet("DSRegional")
                                        ds.Tables.Add(DV.ToTable().Copy())
                                        ds.AcceptChanges()
                                        ds.WriteXml(FileName, XmlWriteMode.WriteSchema)
                                        MessageBox.Show("Data Created to " + FileName)
                                    End If
                                End If
                            End If
                        Case ViewData.Territorial
                            'table8 Territorial
                            Dim DV As DataView = Me.clsSendData.DS.Tables(8).DefaultView
                            DV.RowFilter = ""
                            If Me.ListTerritory.Count > 0 Then
                                DV.Sort = "TERRITORY_ID"
                                For i As Integer = 0 To DV.Count - 1
                                    If Not ListTerritory.Contains(DV(i)("TERRITORY_ID").ToString()) Then
                                        DV(i).Delete()
                                        i -= 1
                                    End If
                                    If i = DV.Count - 1 Then
                                        Exit For
                                    End If
                                Next
                                If (DV.Count > 0) Then
                                    Dim SPD As New SaveFileDialog()
                                    With SPD
                                        .Title = "Save file to send to " & SendTo
                                        .OverwritePrompt = True
                                        .DefaultExt = ".tri"
                                        .Filter = "Territorial data|*.tri"
                                        .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                    End With
                                    If SPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                        Dim FileName As String = SPD.FileName.Replace(".tri", "")
                                        FileName &= "_" & NufarmBussinesRules.SharedClass.ServerDate.ToLongDateString()
                                        FileName &= "_ For_" & SendTo & ".tri"
                                        Dim ds As New DataSet("DSTerritorial")
                                        ds.Tables.Add(DV.ToTable().Copy())
                                        ds.AcceptChanges()
                                        ds.WriteXml(FileName, XmlWriteMode.WriteSchema)
                                        MessageBox.Show("Data Created to " + FileName)
                                    End If
                                End If
                            End If
                    End Select
                    Me.clsSendData.DS.RejectChanges()
                Case "btnCancel"
                    Me.Close()
                    If Not IsNothing(Me.clsSendData) Then
                        Me.clsSendData.Dispose(True)
                    End If
            End Select
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_BtnClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub RadioButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles rdbAdmin.Click, _
     rdbBrand.Click, rdbBrandPack.Click, rdbDistributor.Click, rdbPack.Click, rdbPM.Click, rdbRSM.Click, rdbTM.Click, rdbAgreement.Click, rdbPriviledgeTM.Click, rdbPriviledgeRSM.Click, rdbPriviledgePM.Click, rdbPriviledgeFS.Click, rdbTerritory.Click, rdbRegional.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.IsLoadingRow = True : Me.txtSearch.Text = "" : Me.ListView1.Hide()
            Me.ProgresBar1.Visible = True : Me.lblResult.Visible = False : Me.lblResult.Refresh()
            Me.chkCheckListAll.Enabled = False : Me.chkCheckListAll.Checked = False
            Select Case CType(sender, RadioButton).Name
                Case "rdbBrand"
                    If Not IsNothing(Me.clsSendData.DS) Then
                        Me.CreateColumnsBrand()
                        Me.FillListViewBrand(Me.clsSendData.DS.Tables(1).DefaultView, False)
                        Me.txtSearch.WaterMarkText = "Enter Brand Name to search"
                        'Me.ListBrand.Clear()
                        Me.ViewingData = ViewData.Brand
                        Me.chkCheckListAll.Enabled = True
                    End If
                Case "rdbBrandPack"
                    If Not IsNothing(Me.clsSendData.DS) Then
                        Me.CreateColumnsBrandPack()
                        Me.FillListViewBrandPack(Me.clsSendData.DS.Tables(2).DefaultView, False)
                        Me.txtSearch.WaterMarkText = "Enter BrandPack Name to search"
                        'Me.ListBrandPack.Clear()
                        Me.ViewingData = ViewData.BrandPack
                        Me.chkCheckListAll.Enabled = True
                    End If
                Case "rdbDistributor"
                    If Not IsNothing(Me.clsSendData.DS) Then
                        Me.CreateColumnsDistributor()
                        Me.FilListViewDistributor(Me.clsSendData.DS.Tables(3).DefaultView, False)
                        Me.txtSearch.WaterMarkText = "Enter Distributor Name or Territory or Regional"
                        'Me.ListDistributor.Clear()
                        Me.ViewingData = ViewData.Distributor
                        Me.chkCheckListAll.Enabled = True
                    End If
                    'table0 pack
                    'table1 brand
                    'table2 brandpack
                    'table3 distributor
                    'table4 distributor agreement
                    'table5 agreement
                    'table6 agrement brand
                    'table7 agreement brandpack
                    'table8 territory
                    'table9 TM
                    'table10 Regional
                    'table 11 viewagreement
                    'Table12 PrivieledRSM
                    'table13 priviledTM
                Case "rdbPack"
                    If Not IsNothing(Me.clsSendData.DS) Then
                        Me.CreateColumnsPack()
                        Me.FillListViewPack(Me.clsSendData.DS.Tables(0).DefaultView, False)
                        Me.txtSearch.WaterMarkText = "Enter Pack Name to search" 'Me.ListPack.Clear()
                        Me.ViewingData = ViewData.Pack
                        Me.chkCheckListAll.Enabled = True
                    End If
                Case "rdbAgreement"
                    If Not IsNothing(Me.clsSendData.DS) Then
                        Me.CreateColumnsAgreement()
                        Me.FillListViewAgreement(Me.clsSendData.DS.Tables(11).DefaultView, False)
                        Me.txtSearch.WaterMarkText = "Enter Agreement_NO or Distributor_Name to search"
                        Me.ViewingData = ViewData.Agreement
                        Me.chkCheckListAll.Enabled = True
                    End If
                Case "rdbAdmin" 'Me.ListAgreement.Clear()
                Case "rdbPM"
                Case "rdbRSM"
                Case "rdbTM"
                Case "rdbPriviledgeRSM"
                    If Not IsNothing(Me.clsSendData.DS) Then
                        Me.CreateColumnsPriviledge(False)
                        Me.FillListViewRSM(Me.clsSendData.DS.Tables("Table12").DefaultView(), False)
                    End If
                    If Not Me.hasCheckedData Then
                        Me.clsSendData.deleteUnNeccesssaryUser()
                        Me.hasCheckedData = True
                    End If
                    Me.rdbRSM.Checked = True
                    Me.txtSearch.WaterMarkText = "Enter UserName to search"
                    Me.ViewingData = ViewData.PriviledgeRSM
                    Me.chkCheckListAll.Enabled = False
                Case "rdbPriviledgePM" 'bikin showDialog
                    Dim DV As DataView = Nothing : Me.ProgresBar1.Visible = False : Me.lblResult.Visible = False
                    If MessageBox.Show("Input new data PM ?.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Dim EditorPM As New frmManagePM()
                        EditorPM.ShowInTaskbar = False : DV = New DataView()
                        If Not EditorPM.ShowDialog(DV) = Windows.Forms.DialogResult.OK Then
                            DV = Nothing
                        End If
                    End If
                    Application.DoEvents()
                    Me.clsSendData.GetPriviledPM(DV)
                    Me.ProgresBar1.Visible = True
                    If Not IsNothing(Me.clsSendData.DS) Then
                        Me.CreateColumnsPriviledge(False)
                        Me.FillListViewPM(Me.clsSendData.DS.Tables("PriviledgePM").DefaultView(), False)
                    End If
                    Me.ViewingData = ViewData.PriviledgePM
                    Me.rdbPM.Checked = True
                    Me.txtSearch.WaterMarkText = "Enter UserName to search"
                    Me.chkCheckListAll.Enabled = False
                Case "rdbPriviledgeTM"
                    If Not IsNothing(Me.clsSendData.DS) Then
                        Me.CreateColumnsPriviledge(True)
                        Me.FillListViewTM(Me.clsSendData.DS.Tables("table13").DefaultView(), False)
                    End If
                    If Not Me.hasCheckedData Then
                        Me.clsSendData.deleteUnNeccesssaryUser()
                        Me.hasCheckedData = True
                    End If
                    Me.lblResult.Visible = True
                    Me.rdbTM.Checked = True
                    Me.ViewingData = ViewData.PriviledgeTM
                    Me.txtSearch.WaterMarkText = "Enter UserName to search"
                    Me.chkCheckListAll.Enabled = False
                Case "rdbPriviledgeFS"
                    If IsNothing(Me.tblFS) Then
                        'get data FS
                        Me.tblFS = Me.clsSendData.getDSFS()
                    End If
                    Me.ViewingData = ViewData.PriviledgeFS
                    Me.lblResult.Visible = True
                    Me.CreateColumnsFS()
                    Me.fillListViewFS(Me.tblFS.DefaultView(), False)
                    Me.rdbFS.Checked = True
                    Me.txtSearch.WaterMarkText = "Enter UserName to search"
                    Me.chkCheckListAll.Enabled = False
                Case "rdbRegional"
                    If Not IsNothing(Me.clsSendData.DS) Then
                        Me.CreateColumnsRegional()
                        Me.fillListRegional(Me.clsSendData.DS.Tables(10).DefaultView(), False)
                    End If
                    Me.lblResult.Visible = True
                    Me.rdbTM.Checked = True
                    Me.ViewingData = ViewData.Regional
                    Me.txtSearch.WaterMarkText = "Enter regional area/parent regional/manager"
                    Me.chkCheckListAll.Enabled = True
                Case "rdbTerritory"
                    If Not IsNothing(Me.clsSendData.DS) Then
                        Me.CreateColumnTerritory()
                        Me.fillListViewTerritory(Me.clsSendData.DS.Tables(8).DefaultView(), False)
                    End If
                    Me.lblResult.Visible = True
                    Me.rdbTM.Checked = True
                    Me.ViewingData = ViewData.Territorial
                    Me.txtSearch.WaterMarkText = "Enter regional area/territori area/parent territory"
                    Me.chkCheckListAll.Enabled = True
            End Select
            Me.ChekAvailableItem()
            Me.ListView1.Show()
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_RadioButtonClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.lblResult.Visible = True
            Me.ProgresBar1.Visible = False : Me.IsLoadingRow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkCheckListAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckListAll.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.IsLoadingRow = True
            If Me.ListView1.Items.Count <= 0 Then : Return : End If
            If (Me.chkCheckListAll.Checked) Then
                For i As Integer = 0 To ListView1.Items.Count - 1
                    With ListView1
                        .Items(i).Checked = True
                        If Me.ViewingData = ViewData.Agreement Then
                            Me.AddList(.Items(i).Text & .Items(i).SubItems(4).Text)
                        Else
                            Me.AddList(.Items(i).Text)
                        End If
                    End With
                Next
            Else
                For i As Integer = 0 To ListView1.Items.Count - 1
                    With ListView1
                        .Items(i).Checked = False
                        If Me.ViewingData = ViewData.Agreement Then
                            Me.RemoveList(.Items(i).Text & .Items(i).SubItems(4).Text)
                        Else
                            Me.RemoveList(.Items(i).Text)
                        End If
                    End With
                Next
            End If
        Catch
        Finally
            Me.IsLoadingRow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub txtSearch_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Leave
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        If (Not Me.txtSearch.WaterMarkText.Equals(Me.OriginalWaterMarkText)) Then
    '            Me.txtSearch_KeyDown(Me.txtSearch, New KeyEventArgs(Keys.Enter))
    '        Else
    '            Me.txtSearch.WaterMarkText = Me.OriginalWaterMarkText
    '        End If
    '    Catch
    '    Finally
    '        Me.Cursor = Cursors.WaitCursor
    '    End Try
    'End Sub

    Private Sub txtSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Enter
        If Me.txtSearch.WaterMarkText.Equals(Me.OriginalWaterMarkText) Then
            Me.txtSearch.Text = ""
        End If
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Try
            Me.Cursor = Cursors.WaitCursor
            'table0 pack
            'table1 brand
            'table2 brandpack
            'table3 distributor
            'table4 distributor agreement
            'table5 agreement
            'table6 agrement brand
            'table7 agreement brandpack
            'table8 territory
            'table9 TM
            'table10 RSM
            'table 11 viewagreement
            'Table12 PrivieledRSM
            'table13 priviledTM
            Me.lblResult.Visible = False
            Me.ProgresBar1.Visible = True
            If (e.KeyCode = Keys.Enter) Then
                Me.IsLoadingRow = True
                Me.chkCheckListAll.Checked = False
                Dim DV As DataView = Nothing
                Select Case Me.ViewingData
                    Case ViewData.Agreement
                        DV = Me.clsSendData.DS.Tables(11).DefaultView
                        DV.RowFilter = "AGREEMENT_NO LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + _
                        "%' OR DISTRIBUTOR_NAME LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%' OR TERRITORY_AREA LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%' OR REGIONAL_AREA LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%'"
                        Me.FillListViewAgreement(DV, False)
                    Case ViewData.Brand
                        DV = Me.clsSendData.DS.Tables(1).DefaultView
                        DV.RowFilter = "BRAND_NAME LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() & "%'"
                        Me.FillListViewBrand(DV, False)
                    Case ViewData.BrandPack
                        DV = Me.clsSendData.DS.Tables(2).DefaultView
                        DV.RowFilter = "BRANDPACK_NAME LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%'"
                        Me.FillListViewBrandPack(DV, False)
                    Case ViewData.None
                        Return
                    Case ViewData.Pack
                        DV = Me.clsSendData.DS.Tables(0).DefaultView
                        DV.RowFilter = "PACK_NAME LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%'"
                        Me.FillListViewPack(DV, False)
                    Case ViewData.Distributor
                        DV = Me.clsSendData.DS.Tables(3).DefaultView
                        DV.RowFilter = "DISTRIBUTOR_NAME LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%' OR TERRITORY_AREA LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%' OR REGIONAL_AREA LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%'"
                        Me.FilListViewDistributor(DV, False)
                    Case ViewData.PriviledgePM
                        If (Me.clsSendData.DS.Tables.Contains("PriviledgePM")) Then
                            DV = Me.clsSendData.DS.Tables("PriviledgePM").DefaultView
                            DV.RowFilter = "UserName LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%'"
                            Me.FillListViewPM(DV, False)
                        End If
                    Case ViewData.PriviledgeRSM
                        DV = Me.clsSendData.DS.Tables(12).DefaultView
                        DV.RowFilter = "UserName LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%'"
                        Me.FillListViewRSM(DV, False)
                    Case ViewData.priviledgeTM
                        DV = Me.clsSendData.DS.Tables(13).DefaultView
                        DV.RowFilter = "UserName LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%'"
                        Me.FillListViewTM(DV, False)
                    Case ViewData.PriviledgeFS
                        DV = Me.tblFS.DefaultView()
                        DV.RowFilter = "UserName LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%'"
                        Me.fillListViewFS(DV, False)
                    Case ViewData.Regional
                        DV = Me.clsSendData.DS.Tables(10).DefaultView()
                        DV.RowFilter = "REGIONAL_AREA LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%' OR PARENT_REGIONAL LIKE '%" + Me.txtSearch.Text.TrimStart().TrimEnd() + "%' OR MANAGER LIKE '%" + Me.txtSearch.Text.TrimStart().TrimEnd() + "%'"
                        Me.fillListRegional(DV, False)
                    Case ViewData.Territorial
                        DV = Me.clsSendData.DS.Tables(8).DefaultView()
                        DV.RowFilter = "TERRITORY_AREA LIKE '%" + Me.txtSearch.Text.TrimStart().TrimEnd() + "%' OR REGIONAL_AREA LIKE '%" + Me.txtSearch.Text.TrimEnd().TrimStart() + "%' OR PARENT_TERRITORY LIKE '%" + Me.txtSearch.Text.TrimStart().TrimEnd() + "%'"
                        Me.fillListViewTerritory(DV, False)
                End Select
                Me.ChekAvailableItem()
            End If
        Catch
        Finally
            Me.IsLoadingRow = False : Me.ProgresBar1.Visible = False : Me.lblResult.Visible = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub FrmSendingData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.OriginalWaterMarkText = Me.txtSearch.WaterMarkText 'Me.AcceptButton = Me.btnOK
            Me.CancelButton = Me.btnCancel
            Me.ProgresBar1.Visible = False
            Me.lblResult.Visible = False
            Me.AcceptButton = Nothing
            AddHandler ListView1.ColumnClick, AddressOf ColumnClick

        Catch ex As Exception

        Finally
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default : Me.CMain.StatProg = Main.StatusProgress.None
        End Try
    End Sub

    Private Sub ListView1_ItemChecked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles ListView1.ItemChecked
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.IsLoadingRow Then : Return : End If

            If e.Item.Checked Then
                If Me.ViewingData = ViewData.PriviledgePM Then
                    If Me.ListPM.Count > 0 Then
                        IsLoadingRow = True
                        e.Item.Checked = False
                        MessageBox.Show("Priviledge only applies for one for each user", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        IsLoadingRow = False
                        Return
                    End If
                ElseIf Me.ViewingData = ViewData.PriviledgeRSM Then
                    If Me.ListRSM.Count > 0 Then
                        IsLoadingRow = True
                        e.Item.Checked = False
                        MessageBox.Show("Priviledge only applies for one for each user", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        IsLoadingRow = False
                        Return
                    End If
                ElseIf Me.ViewingData = ViewData.priviledgeTM Then
                    If Me.ListTM.Count > 0 Then
                        IsLoadingRow = True
                        e.Item.Checked = False
                        MessageBox.Show("Priviledge only applies for one for each user", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        IsLoadingRow = False
                        Return
                    End If
                ElseIf Me.ViewingData = ViewData.PriviledgeFS Then
                    If Me.listFS.Count > 0 Then
                        IsLoadingRow = True
                        e.Item.Checked = False
                        MessageBox.Show("Priviledge only applies for one for each user", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        IsLoadingRow = False
                        Return
                    End If
                End If
                'Select Case Me.ViewingData
                '    Case ViewData.Agreement
                '        ListAgreement.Add(e.Item.SubItems(0).Text & e.Item.SubItems(4).Text)
                '    Case ViewData.Brand
                '        ListBrand.Add(e.Item.Text)
                '    Case ViewData.BrandPack
                '        ListBrandPack.Add(e.Item.Text)
                '    Case ViewData.Pack
                '        ListPack.Add(e.Item.Text)
                '    Case ViewData.Distributor
                '        ListDistributor.Add(e.Item.Text)
                'End Select
                If Me.ViewingData = ViewData.Agreement Then
                    Me.AddList(e.Item.Text & e.Item.SubItems(4).Text)
                Else
                    Me.AddList(e.Item.Text)
                End If
            Else
                'Select Case Me.ViewingData
                'Case ViewData.Agreement
                '    '.Columns.Add("AGREEMENT_NO")
                '    '.Columns.Add("DISTRIBUTOR_ID")
                '    '.Columns.Add("DISTRIBUTOR_NAME")
                '    '.Columns.Add("AGREEMENT_NAME") 'AGREEMENT_DESC
                '    '.Columns.Add("BRANDPACK_ID")
                '    '.Columns.Add("BRANDPACK_NAME")
                '    '.Columns.Add("START_DATE")
                '    '.Columns.Add("END_DATE")
                '    '.Columns.Add("FLAG") 'QS_TREATMENT_FLAG
                '    ListAgreement.Remove(e.Item.SubItems(0).Text & e.Item.SubItems(4).Text)
                'Case ViewData.Brand
                '    ListBrand.Remove(e.Item.Text)
                'Case ViewData.BrandPack
                '    ListBrandPack.Remove(e.Item.Text)
                'Case ViewData.Pack
                '    ListPack.Remove(e.Item.Text)
                'Case ViewData.Distributor
                '    ListDistributor.Remove(e.Item.Text)
                'End Select
                If Me.ViewingData = ViewData.Agreement Then
                    Me.RemoveList(e.Item.Text & e.Item.SubItems(4).Text)
                Else
                    Me.RemoveList(e.Item.Text)
                End If
            End If
        Catch

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ColumnClick(ByVal o As Object, ByVal e As ColumnClickEventArgs)
        ' Set the ListViewItemSorter property to a new ListViewItemComparer 
        ' object. Setting this property immediately sorts the 
        ' ListView using the ListViewItemComparer object.
        Me.ListView1.Sort()
        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column)

    End Sub

    Private Sub btnViewResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewResult.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.txtSearch.Text = ""
            Dim DV As DataView = Nothing
            Me.ProgresBar1.Visible = True : Me.lblResult.Visible = False
            Select Case Me.ViewingData
                Case ViewData.Agreement
                    DV = Me.clsSendData.DS.Tables(11).DefaultView
                    DV.RowFilter = ""
                    Me.FillListViewAgreement(DV, True)
                Case ViewData.Brand
                    DV = Me.clsSendData.DS.Tables(1).DefaultView
                    DV.RowFilter = ""
                    Me.FillListViewBrand(DV, True)
                Case ViewData.BrandPack
                    DV = Me.clsSendData.DS.Tables(2).DefaultView
                    DV.RowFilter = ""
                    Me.FillListViewBrandPack(DV, True)
                Case ViewData.None
                    Return
                Case ViewData.Pack
                    DV = Me.clsSendData.DS.Tables(0).DefaultView
                    DV.RowFilter = ""
                    Me.FillListViewPack(DV, True)
                Case ViewData.Distributor
                    DV = Me.clsSendData.DS.Tables(3).DefaultView
                    DV.RowFilter = ""
                    Me.FilListViewDistributor(DV, True)
                Case ViewData.PriviledgePM
                    If Me.clsSendData.DS.Tables.Contains("PriviledgePM") Then
                        DV = Me.clsSendData.DS.Tables(14).DefaultView
                        DV.RowFilter = ""
                        Me.FillListViewPM(DV, True)
                    End If
                Case ViewData.PriviledgeRSM
                    DV = Me.clsSendData.DS.Tables(12).DefaultView
                    DV.RowFilter = ""
                    Me.FillListViewRSM(DV, True)
                Case ViewData.priviledgeTM
                    DV = Me.clsSendData.DS.Tables(13).DefaultView
                    DV.RowFilter = ""
                    Me.FillListViewTM(DV, True)
                Case ViewData.PriviledgeFS
                    DV = Me.tblFS.DefaultView()
                    DV.RowFilter = ""
                    Me.fillListViewFS(DV, True)
                Case ViewData.Regional
                    DV = Me.clsSendData.DS.Tables(10).DefaultView
                    DV.RowFilter = ""
                    Me.fillListRegional(DV, True)
                Case ViewData.Territorial
                    DV = Me.clsSendData.DS.Tables(8).DefaultView
                    DV.RowFilter = ""
                    Me.fillListViewTerritory(DV, True)
            End Select
        Catch
        Finally
            Me.ProgresBar1.Visible = False : Me.lblResult.Visible = True : Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub rdbPriviledgeFS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPriviledgeFS.Click
    '    If Me.rdbPriviledgeFS.Checked Then
    '        Try

    '        Catch ex As Exception

    '        End Try
    '    End If

    'End Sub

    'Private Sub rdbRegional_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbRegional.Click
    '    'table8 territory
    '    'table9 TM
    '    'table10 REGIONAL
    '    Try
    '        Me.Cursor = Cursors.WaitCursor

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub rdbTerritory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTerritory.Click
    '    'table8 territory
    '    'table9 TM
    '    'table10 REGIONAL
    'End Sub

End Class

'Class ListViewItemComparer1
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
