Imports System.Threading
Public Class Invoice
    Private clsInvoice As New NufarmBussinesRules.PurchaseOrder.Invoice
    Private isLoadingCombo As Boolean = False
    Private Delegate Sub onShowingProgress()
    Private Event ShowProgres As onShowingProgress
    Private Delegate Sub onClossingProgress()
    Private Event CloseProgress As onClossingProgress
    Private IsHasLoadedForm As Boolean = False
    Private DV As DataView = Nothing
    Dim rnd As Random
    Private HasLoadReport As Boolean = True
    Private LD As Loading
    Private CounterTimer2 As Integer = 0
    Private IsLoadData As Boolean = True
    Private HasLoadForm As Boolean = False
    Private Ticcount As Integer = 0
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Friend CMain As Main = Nothing
    Private originalStartDate As String = ""
    Private originalEndDate As String = ""
    Private t2 As DataTable
    Dim pnlHeigt As Integer
    Private OtherUserProcessing As String = ""
    Private hasloadGrid As Boolean = False
    Private mustReload As Boolean = False
    Private Class classT2
        Public IDApp As Integer
        Public PoBrandPackID As String
    End Class
    Private Enum StatusProgress
        None
        Processing
        WaitingForAnotherProcess
    End Enum

    Private Sub ShowProceed()
        'LD = New Loading
        'LD.Show() : LD.TopMost = True
        'Application.DoEvents()
        'While Not Me.StatProg = StatusProgress.None
        '    If Me.StatProg = StatusProgress.WaitingForAnotherProcess Then
        '        Me.LD.Label1.Text = "Waiting for " + Me.OtherUserProcessing + " to finish processing procedure"
        '    Else
        '        Me.LD.Label1.Text = ""
        '    End If
        '    Me.LD.Refresh() : Application.DoEvents()
        '    Thread.Sleep(50)
        'End While
        'Thread.Sleep(50)

        'LD.Close() : LD = Nothing
        LD = New Loading
        LD.Show() : LD.TopMost = True
        Application.DoEvents()
        Dim BFind As Boolean = False
        BFind = Me.clsInvoice.hasReservedInvoice(Me.OtherUserProcessing)
        While Not Me.StatProg = StatusProgress.None
            If BFind Then
                If Me.OtherUserProcessing <> NufarmBussinesRules.User.UserLogin.UserName Then
                    While BFind
                        Thread.Sleep(100)
                        Me.LD.Label1.Text = "Waiting for " + Me.OtherUserProcessing + " to finish processing procedure"
                        Application.DoEvents()
                        Me.LD.Refresh()
                        BFind = Me.clsInvoice.hasReservedInvoice(Me.OtherUserProcessing)
                        If BFind Then
                            If Me.OtherUserProcessing = NufarmBussinesRules.User.UserLogin.UserName Then
                                BFind = False
                            End If
                        End If
                    End While
                End If
            End If
            Me.StatProg = StatusProgress.Processing
            Me.LD.Label1.Text = "Processing procedure...."
            Thread.Sleep(50) : Me.LD.Refresh() : Application.DoEvents()
        End While
        Thread.Sleep(50)
        LD.Close() : LD = Nothing
    End Sub
    'Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
    '    If Me.TickCount >= Me.ResultRandom Then`   '        If Me.HasLoadReport Then
    '            Me.BindGrid(Me.DV)
    '            RaiseEvent CloseProgress()
    '        Else
    '            Me.ResultRandom += 1
    '        End If
    '    End If
    'End Sub
    'Private Sub ChekTimer2(ByVal sender As Object, ByVal e As EventArgs)
    '    If Me.CounterTimer2 > 1 Then
    '        Me.Timer2.Stop() : Me.Timer2.Enabled = False
    '        Me.CounterTimer2 = 0
    '        Me.getData()
    '        Me.HasLoadReport = True
    '    End If

    'End Sub
    Private Sub closeProgressbar() Handles Me.CloseProgress
        Me.Timer1.Enabled = False
        Me.Timer1.Stop()
        Me.StatProg = StatusProgress.None
        Me.TickCount = 0
        Me.CounterTimer2 = 0
        If Not IsNothing(Me.LD) Then : Me.LD.Close() : Me.LD = Nothing : End If
    End Sub
    Private Sub BindGrid(ByVal DV As DataView)
        Me.GridEX1.SetDataBinding(DV, "")
        If IsNothing(DV) Then : Return : End If
        If Not Me.hasloadGrid Then
            Me.GridEX1.RetrieveStructure()
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If col.Type Is Type.GetType("System.String") Then
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                ElseIf col.Type Is Type.GetType("System.Decimal") Then
                    col.FormatString = "#,##0.000"
                    col.TotalFormatString = "#,##0.000"
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                ElseIf col.Type Is Type.GetType("System.DateTime") Then
                    col.FormatString = "dd MMMM yyyy"
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo

                End If
                If col.Key = "PO_BRANDPACK_ID" Or col.Key = "ChildOriginalPO" Then
                    col.Visible = False : col.ShowInFieldChooser = False
                End If
                col.AutoSize()
            Next
            Me.GridEX1.RootTable.Columns("INV_PRICE").Visible = False
            Me.GridEX1.RootTable.Columns("INV_PRICE").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            Me.GridEX1.RootTable.Columns("INV_PRICE").FormatString = "#,##0.00"

            Me.GridEX1.RootTable.Columns("INV_AMOUNT").Visible = False
            Me.GridEX1.RootTable.Columns("INV_AMOUNT").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            Me.GridEX1.RootTable.Columns("INV_AMOUNT").FormatString = "#,##0.00"

            Me.GridEX1.RootTable.Columns("TOTAL_SHIPMENT_AMOUNT").Visible = False
            Me.GridEX1.RootTable.Columns("TOTAL_SHIPMENT_AMOUNT").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            Me.GridEX1.RootTable.Columns("TOTAL_SHIPMENT_AMOUNT").FormatString = "#,##0.00"

            Me.GridEX1.RootTable.Columns("REGIONAL_ID").Visible = False
            Me.GridEX1.RootTable.Columns("TERRITORY_ID").Visible = False
            Me.GridEX1.RootTable.Columns("BRAND_ID").Visible = False
            Me.GridEX1.RootTable.Columns("BRANDPACK_ID").Visible = False
            Me.GridEX1.RootTable.Columns("DISTRIBUTOR_ID").Visible = False
            Me.GridEX1.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.GridEX1.RootTable.Columns("PO_REF_NO")))
            Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
            Me.GridEX1.GroupByBoxVisible = False
            Me.GridEX1.RootTable.Columns("IDApp").Visible = False : Me.GridEX1.RootTable.Columns("IDApp").ShowInFieldChooser = False
        End If

        If Me.GridEX1.RootTable.Columns.Contains("ChildOriginalPO") Then
            Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.GridEX1.RootTable.Columns("ChildOriginalPO"), Janus.Windows.GridEX.ConditionOperator.Equal, True)
            'fc.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
            fc.FormatStyle.ForeColor = Color.Green
            GridEX1.RootTable.FormatConditions.Add(fc)
        End If
        Me.GridEX1.ExpandGroups()
        Me.hasloadGrid = True
    End Sub
    Private Sub BindMultiColumnCombo(ByVal DV As DataView, ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, _
      ByVal ClearCombo As Boolean, ByVal DMember As String, ByVal VMember As String)
        isLoadingCombo = True
        If ClearCombo Then
            mcb.Value = Nothing : mcb.Text = ""
        End If
        With mcb
            .DataSource = DV
            .DropDownList.RetrieveStructure()
            .DroppedDown = True
            .DropDownList.AutoSizeColumns()
            .DisplayMember = DMember
            .ValueMember = VMember
            .DroppedDown = False
        End With
        isLoadingCombo = False
    End Sub
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnShowFieldChooser"
                    GridEX1.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = GridEX1
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
                    Me.FilterEditor1.SourceControl = GridEX1
                    GridEX1.RemoveFilters()
                    Me.FilterEditor1.Visible = True
                    GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    GridEX1.RemoveFilters()
                    GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Case "btnExport"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = GridEX1
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnRefresh"
                    If Me.btnCatInvoice.Checked = False And Me.btnCatPO.Checked = False Then
                        Me.ShowMessageInfo("Please define category date") : Return
                    End If
                    Me.IsLoadData = False
                    Me.mustReload = True
                    Me.OtherUserProcessing = ""
                    Me.StatProg = StatusProgress.WaitingForAnotherProcess
                    Me.ThreadProgress = New Thread(AddressOf ShowProceed)
                    Me.ThreadProgress.Start()
                    Me.Timer1.Enabled = True : Me.Timer1.Start()
            End Select
        Catch ex As Exception
            Me.closeProgressbar()
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.BindMultiColumnCombo(Me.clsInvoice.GetDistributor(Me.mcbDistributor.Text), Me.mcbDistributor, True, "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
            Dim itemCount As Integer = Me.mcbDistributor.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ btnFilterDistributor_btnClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub getData(ByVal isReload As Boolean)
        Try
            Me.Cursor = Cursors.WaitCursor
            If IsLoadData Then
                Me.BindMultiColumnCombo(Me.clsInvoice.GetDistributor(""), Me.mcbDistributor, True, "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
                'Me.clsInvoice.CreateTempTable()
                Me.HasLoadReport = True : Return
            End If
            Dim isRefByPO As Boolean = True
            If Me.btnCatInvoice.Checked Then
                isRefByPO = False
            End If
            Dim IsChangedOriginalDate = False
            If Not Me.HasLoadForm Then
                IsChangedOriginalDate = True
            ElseIf Me.mustReload Then
                IsChangedOriginalDate = True
            ElseIf Me.originalStartDate <> NufarmBussinesRules.common.CommonClass.getNumericFromDate(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString())) Then
                IsChangedOriginalDate = True
            ElseIf Me.originalEndDate <> NufarmBussinesRules.common.CommonClass.getNumericFromDate(Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())) Then
                IsChangedOriginalDate = True
            End If
            t2 = New DataTable("T_2")
            If Me.mcbDistributor.SelectedIndex <= -1 Then
                DV = Me.clsInvoice.GetInvoice(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString), _
                      Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), isRefByPO, IsChangedOriginalDate, t2)
            Else
                DV = Me.clsInvoice.GetInvoice(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString), _
                     Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), isRefByPO, IsChangedOriginalDate, t2, Me.mcbDistributor.Value.ToString())
            End If
            Me.HasLoadReport = True
        Catch ex As Exception
            Me.mustReload = False : Me.StatProg = StatusProgress.None : Me.HasLoadReport = True : Me.HasLoadForm = True : Throw ex
        Finally
            Me.originalStartDate = NufarmBussinesRules.common.CommonClass.getNumericFromDate(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()))
            Me.originalEndDate = NufarmBussinesRules.common.CommonClass.getNumericFromDate(Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Function hasDoublePOOriginalQty() As Boolean
        Dim listPOBrandPackID As New List(Of Object)
        For i As Integer = 0 To DV.Count - 1
            If Not listPOBrandPackID.Contains(DV(i)("PO_BRANDPACK_ID")) Then
                listPOBrandPackID.Add(DV(i)("PO_BRANDPACK_ID"))
            Else
                Return True
            End If
        Next
    End Function
    Private Sub fillZerowithPO()
        'format data yang mempunyai double po dengan nilai 0
        Dim DV1 As DataView = Me.DV.ToTable().Copy().DefaultView()
        'mulai fixing
        DV1.Sort = "PO_DATE,PO_REF_NO,PO_BRANDPACK_ID"
        Dim listclassT2 As New List(Of classT2)
        Dim listBrandPackIDs As New List(Of String)
        For i As Integer = 0 To DV1.Count - 1
            If Not listBrandPackIDs.Contains(DV1(i)("PO_BRANDPACK_ID").ToString()) Then
                listBrandPackIDs.Add(DV1(i)("PO_BRANDPACK_ID").ToString())
                Dim lstClas2 As New classT2()
                With lstClas2
                    .IDApp = CInt(DV1(i)("IDApp"))
                    .PoBrandPackID = DV1(i)("PO_BRANDPACK_ID").ToString()
                End With
                listclassT2.Add(lstClas2)
            End If
        Next
        ''saatnya filter data
        'check apakah po_brandpack_id ada di table 2
        'chek table apakah ada colum dengan nama colum REF_INV_BEFORE
        If Me.btnCatInvoice.Checked Then
            If Not DV1.Table.Columns.Contains("REF_INV_BEFORE") Then
                'chek colum PO_ORIGINAL_QTY berada di index mana
                Dim colPOOriginalQty As DataColumn = DV1.Table.Columns("PO_ORIGINAL_QTY")
                Dim indexColPOOriginalQty As Integer = colPOOriginalQty.Ordinal
                Dim colRIB As New DataColumn("REF_INV_BEFORE", Type.GetType("System.String"))
                colRIB.DefaultValue = ""
                'colRIB.SetOrdinal(indexColPOOriginalQty)
                DV1.Table.Columns.Add(colRIB)
                DV1.Table.Columns("REF_INV_BEFORE").SetOrdinal(indexColPOOriginalQty)
            End If
            If Not DV1.Table.Columns.Contains("ChildOriginalPO") Then
                Dim colIshasDoublePO As New DataColumn("ChildOriginalPO", Type.GetType("System.Boolean"))
                colIshasDoublePO.DefaultValue = False
                DV1.Table.Columns.Add(colIshasDoublePO)
                DV1.Table.Columns("ChildOriginalPO").SetOrdinal(DV1.Table.Columns.Count - 1)
            End If
            DV1.Table.AcceptChanges()
            Dim DVt2 As DataView = Me.t2.DefaultView()
            DVt2.Sort = "PO_BRANDPACK_ID"
            For i As Integer = 0 To listclassT2.Count - 1
                Dim index As Integer = DVt2.Find(listclassT2(i).PoBrandPackID)
                'System.Threading.Thread.Sleep(50)
                DV1.RowFilter = "PO_BRANDPACK_ID = '" + listclassT2(i).PoBrandPackID + "'"
                If index <= -1 Then
                    'edit dv1 po_branpack_id jadi 0
                    If DV1.Count > 0 Then
                        For i2 As Integer = 0 To DV1.Count - 1
                            DV1(i2).BeginEdit()
                            If CInt(DV1(i2)("IDApp")) <> CInt(listclassT2(i).IDApp) Then
                                DV1(i2)("PO_ORIGINAL_QTY") = 0
                                DV1(i2)("PO_AMOUNT") = 0
                                DV1(i2)("ChildOriginalPO") = True
                            Else
                                DV1(i2)("ChildOriginalPO") = False
                            End If
                            DV1(i2).EndEdit()
                        Next
                        DV1.Table.AcceptChanges()
                    End If
                Else
                    If DV1.Count > 0 Then
                        DVt2.RowFilter = "PO_BRANDPACK_ID = '" + listclassT2(i).PoBrandPackID + "'"
                        Dim strRefInBefore As String = ""
                        If DVt2.Count > 0 Then
                            For i3 As Integer = 0 To DVt2.Count - 1
                                If (Not IsNothing(DVt2(i3)("INVNUMBER"))) And Not (DVt2(i3)("INVNUMBER") Is DBNull.Value) Then
                                    strRefInBefore &= DVt2(i3)("INVNUMBER").ToString()
                                    If i3 < DVt2.Count - 1 Then
                                        strRefInBefore &= ", "
                                    End If
                                End If
                            Next
                        End If
                        For i2 As Integer = 0 To DV1.Count - 1
                            DV1(i2).BeginEdit()
                            DV1(i2)("PO_ORIGINAL_QTY") = 0
                            DV1(i2)("PO_AMOUNT") = 0
                            DV1(i2)("REF_INV_BEFORE") = strRefInBefore
                            DV1(i2)("ChildOriginalPO") = True
                            DV1(i2).EndEdit()
                        Next
                        DV1.Table.AcceptChanges()
                    End If
                End If
                DVt2.RowFilter = ""
            Next
        ElseIf Me.btnCatPO.Checked Then
            If DV1.Table.Columns.Contains("REF_INV_BEFORE") Then
                DV1.Table.Columns.Remove("REF_INV_BEFORE")
            End If
            If Not DV1.Table.Columns.Contains("ChildOriginalPO") Then
                Dim colIshasDoublePO As New DataColumn("ChildOriginalPO", Type.GetType("System.Boolean"))
                colIshasDoublePO.DefaultValue = False
                DV1.Table.Columns.Add(colIshasDoublePO)
                DV1.Table.Columns("ChildOriginalPO").SetOrdinal(DV1.Table.Columns.Count - 1)
            End If
            For i As Integer = 0 To listclassT2.Count - 1
                DV1.RowFilter = "PO_BRANDPACK_ID = '" + listclassT2(i).PoBrandPackID + "'"
                If DV1.Count > 0 Then
                    For i2 As Integer = 0 To DV1.Count - 1
                        If CInt(DV1(i2)("IDApp")) <> CInt(listclassT2(i).IDApp) Then
                            DV1(i2).BeginEdit()
                            If CInt(DV1(i2)("IDApp")) <> CInt(listclassT2(i).IDApp) Then
                                DV1(i2)("PO_ORIGINAL_QTY") = 0
                                DV1(i2)("PO_AMOUNT") = 0
                                DV1(i2)("ChildOriginalPO") = True
                            Else
                                DV1(i2)("ChildOriginalPO") = False
                            End If
                            DV1(i2).EndEdit()
                        End If
                    Next
                    DV1.Table.AcceptChanges()
                End If
            Next
        Else
            Me.ShowMessageInfo("please define category / type") : Return
        End If
        DV1.Table.AcceptChanges() : DV1.RowFilter = "" : Me.hasloadGrid = False : Me.BindGrid(DV1) : Me.DV = DV1 : Me.Refresh()
    End Sub
    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.btnCatInvoice.Checked = False And Me.btnCatPO.Checked = False Then
                Me.ShowMessageInfo("Please define category date") : Return
            End If
            Me.IsLoadData = False
            Me.OtherUserProcessing = ""
            'If Me.clsInvoice.hasReservedInvoice(Me.OtherUserProcessing) Then
            '    Me.StatProg = StatusProgress.WaitingForAnotherProcess
            '    Me.Timer1.Interval = 500
            '    Me.Timer1.Enabled = True : Me.Timer1.Start()
            'Else
            '    Me.Timer1.Interval = 1000
            '    Me.StatProg = StatusProgress.Processing
            'End If
            Me.StatProg = StatusProgress.WaitingForAnotherProcess
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            Me.Timer1.Enabled = True : Me.Timer1.Start()
            'Me.getData() : Me.BindGrid(Me.DV) : Me.StatProg = StatusProgress.None
            'If Me.hasDoublePOOriginalQty() Then
            '    'me.PanelEx2.Height = me.p
            '    Me.PanelEx2.Height = Me.pnlHeigt
            'Else
            '    Me.PanelEx2.Height = 0
            'End If
            'Application.DoEvents() : Me.StatProg = StatusProgress.None
            'Me.Timer1.Interval = 1000 : Me.OtherUserProcessing = ""
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.HasLoadReport = True
            'Me.Enabled = True
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnApplyRange_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    'Private Sub HandlesRDB(ByVal sender As Object, ByVal e As EventArgs)
    '    If Me.rdbByPO.Checked Then
    '        Me.grpRangedate.Text = "Range date By PO"
    '    ElseIf Me.rdbByInvoice.Checked Then
    '        Me.grpRangedate.Text = "Range date by Invoice"
    '    End If
    'End Sub
    Private Sub Invoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.pnlHeigt = Me.PanelEx2.Height
            Me.PanelEx2.Height = 0 : Application.DoEvents()
            'System.Threading.Thread.Sleep(100)
            'AddHandler Timer1.Tick, AddressOf ChekTimer
            'AddHandler Timer2.Tick, AddressOf ChekTimer2
            'LD = New Loading
            'LD.Show() : LD.TopMost = True
            'Application.DoEvents()

            'Me.rnd = New Random() : Me.ResultRandom = Me.rnd.Next(1, 6)
            'Me.Timer1.Enabled = True : Me.Timer1.Start()
            'Application.DoEvents() : Me.HasLoadReport = False
            'Me.Timer2.Enabled = True : Me.Timer2.Start()
            Me.dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate
            Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
            Me.originalStartDate = NufarmBussinesRules.common.CommonClass.getNumericFromDate(NufarmBussinesRules.SharedClass.ServerDate)
            Me.originalEndDate = NufarmBussinesRules.common.CommonClass.getNumericFromDate(NufarmBussinesRules.SharedClass.ServerDate)

            'Me.btnApplyRange_Click(Me.btnApplyRange, New EventArgs())
            If Me.btnCatPO.Checked Then
                Me.grpRangedate.Text = "Range Date By PO"
            ElseIf Me.btnCatInvoice.Checked Then
                Me.grpRangedate.Text = "Range Date By Invoice"
            End If
        Catch ex As Exception
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
            Me.StatProg = StatusProgress.None
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Invoice_Load")
        Finally
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.HasLoadForm = True : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Invoice_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            'drop table invoice
            Me.Cursor = Cursors.WaitCursor
            Me.clsInvoice.Dispose(True)
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_Invoice_FormClosing")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnCatPO"
                    If Me.btnCatPO.Checked Then ''button berarti mau di lepaskan checklist an nya
                        If Me.btnCatInvoice.Checked = False Then
                            Me.GridEX1.SetDataBinding(Nothing, "") : Me.btnCatPO.Checked = False : Return
                        End If
                        Me.btnCatPO.Checked = False
                    Else
                        Me.btnCatPO.Checked = True
                    End If
                    Me.btnCatInvoice.Checked = False
                Case "btnCatInvoice"
                    If Me.btnCatInvoice.Checked Then
                        If Me.btnCatPO.Checked = False Then
                            Me.GridEX1.SetDataBinding(Nothing, "") : Me.btnCatInvoice.Checked = False : Return
                        End If
                        Me.btnCatInvoice.Checked = False
                    Else
                        Me.btnCatInvoice.Checked = True
                    End If
                    Me.btnCatPO.Checked = False
            End Select
            If Not Me.btnCatPO.Checked And Not Me.btnCatInvoice.Checked Then
                Return
            End If
            Me.IsLoadData = False
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            Me.Timer1.Enabled = True
            Me.Timer1.Start()
            'Me.getData() : Me.BindGrid(Me.DV) : Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFixDoubleValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFixDoubleValue.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf ShowProceed)
            Me.ThreadProgress.Start()
            Me.fillZerowithPO()
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.StatProg = StatusProgress.Processing Then
            Try
                'Dim OtherUsr As String = ""
                'If Me.clsInvoice.hasReservedInvoice(OtherUsr) Then
                '    Me.OtherUserProcessing = OtherUsr
                'Else
                '    Me.StatProg = StatusProgress.Processing
                '    Me.OtherUserProcessing = ""
                '    Me.Timer1.Stop() : Me.Timer1.Enabled = False
                '    Me.Timer1.Interval = 1000
                'End If
                Me.Timer1.Stop() : Me.Timer1.Enabled = False
                Me.getData(Me.mustReload) : Me.BindGrid(Me.DV) : Me.StatProg = StatusProgress.None
                If Me.hasDoublePOOriginalQty() Then
                    'me.PanelEx2.Height = me.p
                    Me.PanelEx2.Height = Me.pnlHeigt
                Else
                    Me.PanelEx2.Height = 0
                End If
                Application.DoEvents() : Me.StatProg = StatusProgress.None
                Me.Timer1.Interval = 1000 : Me.OtherUserProcessing = ""
                mustReload = False
            Catch ex As Exception
                Me.ShowMessageInfo(ex.Message)
                Me.mustReload = False : Me.OtherUserProcessing = String.Empty
                Me.StatProg = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False
            End Try
        End If

    End Sub
End Class
