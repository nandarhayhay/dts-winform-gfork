Public Class DistributorHistory

#Region " Deklarasi "
    Private clsDistHistrory As NufarmBussinesRules.DistributorRegistering.DistrHistory
    Private SFM As StateFillingMCB
    Private SFG As StateFillingGrid
    Friend CMain As Main = Nothing
    Private pngHeight As Integer = 0
#End Region

#Region " Enum "
    Private Enum StateFillingMCB
        Filling
        HasFilled
    End Enum
    Private Enum StateFillingGrid
        Filling
        HasFilled
    End Enum
#End Region

#Region " Sub "
    'initiliaze data pada saat call form,load data tidak di simpan di form load
    Friend Sub InitilizeData()
        Me.LoadData()
    End Sub
    Private Sub LoadData()
        Me.SFM = StateFillingMCB.Filling
        Me.clsDistHistrory = New NufarmBussinesRules.DistributorRegistering.DistrHistory
        Me.clsDistHistrory.CreateViewDistributor()
        Me.BindMultiColumnCombo(Me.clsDistHistrory.ViewDistributor(), "")
        'Me.clsDistHistrory.GetDataView()
        'Me.BindChekComboBox()
    End Sub
    Private Sub Bindgrid(ByVal dtview As DataView)
        If dtview Is Nothing Then
            Me.GridEX1.SetDataBinding(Nothing, "")
            Return
        End If
        Me.SFG = StateFillingGrid.Filling
        Me.GridEX1.SetDataBinding(dtview, "")
        Me.GridEX1.RetrieveStructure()
        If Me.btnPurchaseOrder.Checked = True Then
            Me.GridEX1.RootTable.Columns("QUANTITY").FormatString = "#,##0.000"
            Me.GridEX1.RootTable.Columns("QUANTITY").TotalFormatString = "#,##0.000"
            Me.GridEX1.RootTable.Columns("PRICE/QTY").FormatString = "#,##0.00"
            Me.GridEX1.RootTable.Columns("TOTAL").FormatString = "#,##0.00"
            Me.GridEX1.RootTable.Columns("TOTAL").TotalFormatString = "#,##0.00"
            'Me.GridEX1.RootTable.Columns("GIVEN_VALUE_PKPP").FormatString = "#,##0.00"
            'Me.GridEX1.RootTable.Columns("GIVEN_VALUE_PKPP").TotalFormatString = "#,##0.00"
        Else
            For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If Item.Type Is Type.GetType("System.DateTime") Then
                    Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    Item.FormatString = "dd MMMM yyyy"
                End If
                'ELECT PO_REF_NO,PO_REF_DATE,PROJ_REF_NO,BRANDPACK_NAME,QUANTITY,[PRICE/QTY],TOTAL
                If Item.Key = "PRICE" Then
                    Item.FormatString = "#,##0.00"
                    Item.TotalFormatString = "#,##0.00"
                    Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    Item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Item.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                End If
                If (Item.Type Is Type.GetType("System.Decimal")) Or (Item.Type Is Type.GetType("System.Double")) _
                Or Item.Type Is Type.GetType("System.Single") Then
                    Item.FormatString = "#,##0.000"
                    Item.TotalFormatString = "#,##0.000"
                    If Me.btnMarketingProgram.Checked Then
                        If Item.DataMember = "GIVEN_VALUE_PKPP" Then
                            Me.GridEX1.RootTable.Columns("GIVEN_VALUE_PKPP").FormatString = "#,##0.00"
                            Me.GridEX1.RootTable.Columns("GIVEN_VALUE_PKPP").TotalFormatString = "#,##0.00"
                        End If
                    End If
                    Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    Item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Item.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                End If
                If dtview.Table.TableName = "OTHERS_DISTRIBUTOR_HISTORY_DETAIL" Then
                    If Item.Key = "DISCOUNT" Then
                        Item.Caption = "DISC(%)"
                        Item.FormatString = "p"
                    ElseIf Item.Key = "BRAND_ID" Then
                        Item.Visible = False
                    ElseIf Item.Key = "BRANDPACK_ID" Then
                        Item.Visible = False
                    End If
                End If

            Next
        End If

        Me.FillFilterColumn()
        Me.GridEX1.AutoSizeColumns()
        Me.GridEX1.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.GridEX1.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.SFG = StateFillingGrid.HasFilled

    End Sub
    Private Sub GetStateChecked(ByVal item As DevComponents.DotNetBar.ButtonItem)
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar2.Items
            If Not item1.Name = item.Name Then
                item1.Checked = False
            Else
                item = item1
                item.Checked = True
            End If
        Next
    End Sub
    Private Sub ClearCheckedState()
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar2.Items
            item1.Checked = False
        Next
    End Sub
    'Private Sub BindChekComboBox()
    '    Me.CheckedComboBox1.SetDataBinding(Me.clsDistHistrory.ViewDistributor, "")
    'End Sub
    Private Sub FillFilterColumn()
        For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If (Item.Type Is Type.GetType("System.Decimal")) Or (Item.Type Is Type.GetType("System.Double")) _
            Or Item.Type Is Type.GetType("System.Single") Then
                Me.GridEX1.RootTable.Columns(Item.Index).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            End If
            If Item.Type Is Type.GetType("System.String") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.Int16") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                Me.GridEX1.RootTable.Columns(Item.Index).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            ElseIf Item.Type Is Type.GetType("System.Int32") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                Me.GridEX1.RootTable.Columns(Item.Index).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            ElseIf Item.Type Is Type.GetType("System.Int64") Then
                Me.GridEX1.RootTable.Columns(Item.Index).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.Boolean") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
                Me.GridEX1.RootTable.Columns(Item.Index).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ElseIf Item.Type Is Type.GetType("System.String") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.DateTime") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            End If
        Next

    End Sub
    Private Sub BindMultiColumnCombo(ByVal dtview As DataView, ByVal rowFilter As String)
        Me.SFM = StateFillingMCB.Filling
        dtview.RowFilter = rowFilter
        Me.mcbDistributor.SetDataBinding(dtview, "")
        Me.SFM = StateFillingMCB.HasFilled
    End Sub
#End Region

#Region "Event"
    Private Sub GridEX1_FilterApplied(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.FilterApplied
        Try
            Me.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DistributorHistory_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If Not IsNothing(Me.clsDistHistrory) Then
                Me.clsDistHistrory.Dispose(True)
                Me.clsDistHistrory = Nothing
            End If
            Me.Dispose(True)
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "DistributorHistory_FormClosed")
        End Try
    End Sub

    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnFilter"
                    Me.FilterEditor1.SourceControl = Me.GridEX1
                    Me.FilterEditor1.Visible = True
                    'Me.PanelEx1.Visible = True
                    Me.GridEX1.RemoveFilters()
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                Case "btnFilterEqual"
                    'Me.PanelEx1.Visible = False
                    Me.FilterEditor1.Visible = False
                    Me.GridEX1.RemoveFilters()
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Case "btnRename"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar1)
                Case "btnCardView"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.CardView
                    'Me.GridEX1.RootTable.Columns("Icon").Visible = False
                Case "btnSingleCard"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.SingleCard
                    'Me.GridEX1.RootTable.Columns("Icon").Visible = False
                Case "btnTableView"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.TableView
                    'Me.GridEX1.RootTable.Columns("Icon").Visible = True
                Case "btnFieldChooser"
                    Me.GridEX1.ShowFieldChooser(Me)
                Case "btnExport"
                    Me.SaveFileDialog1.Title = "Define the location File"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.GridEX1
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                        Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    End If
                    'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                Case "btnPageSetting"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
                Case "btnRefResh"
                    Dim DISTRIBUTOR_ID As Object = Nothing
                    If Not IsNothing(Me.mcbDistributor.SelectedItem) Then
                        DISTRIBUTOR_ID = Me.mcbDistributor.Value
                    End If
                    Me.Refresh()
                    Me.mcbDistributor.Value = Nothing
                    Me.mcbDistributor.Value = DISTRIBUTOR_ID
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "DistributorHistory_ItemPanel1_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            If Me.mcbDistributor.SelectedItem Is Nothing Then
                Me.ShowMessageInfo("Please define Distributor Name")
                CType(item, DevComponents.DotNetBar.ButtonItem).Checked = False
                Me.Bindgrid(Nothing)
                Return
            End If
            'btnOtherProgram
            'btnHK
            Me.ClearCheckedState()
            CType(item, DevComponents.DotNetBar.ButtonItem).Checked = Not CType(item, DevComponents.DotNetBar.ButtonItem).Checked
            If CType(item, DevComponents.DotNetBar.ButtonItem).Checked = True Then
                If Not IsNothing(Me.clsDistHistrory) Then
                    Select Case item.Name
                        Case "btnAgrementBrand"
                            'Me.btnMarketingProgram.Checked = False
                            'Me.btnPurchaseOrder.Checked = False
                            'Me.btnProject.Checked = False
                            'Me.GetStateChecked(Me.btnAgrementBrand)
                            Me.grpDate.Height = 0
                            Me.Bindgrid(Me.clsDistHistrory.ViewAgreement)

                        Case "btnMarketingProgram"
                            'Me.btnMarketingProgram.Checked = False
                            'Me.btnPurchaseOrder.Checked = False
                            'Me.btnProject.Checked = False
                            'Me.btnAgrementBrand.Checked = False
                            'Me.GetStateChecked(btnMarketingProgram)
                            Me.grpDate.Height = 0
                            Me.Bindgrid(Me.clsDistHistrory.ViewMarketingProgram)

                        Case "btnPurchaseOrder"
                            'Me.btnProject.Checked = False
                            'Me.btnAgrementBrand.Checked = False
                            'Me.btnMarketingProgram.Checked = False
                            'Me.GetStateChecked(Me.btnPurchaseOrder)
                            Me.grpDate.Height = Me.pngHeight
                            Me.Bindgrid(Me.clsDistHistrory.ViewPurchaseOrder)
                        Case "btnProject"
                            'Me.btnAgrementBrand.Checked = False
                            'Me.btnMarketingProgram.Checked = False
                            'Me.btnPurchaseOrder.Checked = False
                            'Me.GetStateChecked(Me.btnProject)
                            Me.grpDate.Height = Me.pngHeight
                            Me.Bindgrid(Me.clsDistHistrory.ViewProject)
                            'Case "btnOtherProgram"
                            '    Me.Bindgrid(Me.clsDistHistrory.ViewOtherProgram())
                            'Case "btnHK"
                            '    'Me.GetStateChecked(Me.btnHK)
                            '    Me.Bindgrid(Me.clsDistHistrory.ViewHKProgram())
                        Case "btnOthers"
                            Me.grpDate.Height = 0
                            Me.Bindgrid(Me.clsDistHistrory.viewOthers())
                        Case "btnDetDiscProg"
                            Me.grpDate.Height = 0
                            Me.Bindgrid(Me.clsDistHistrory.ViewOthersDetail())
                    End Select
                    'Me.FillFilterColumn()
                    Me.GridEX1.RootTable.Columns(0).CardCaption = True
                    Me.FilterEditor1.SourceControl = Me.GridEX1
                    Me.GetStateChecked(CType(item, DevComponents.DotNetBar.ButtonItem))
                End If
            Else
                Me.Bindgrid(Nothing)
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "DistributorHistory_Bar2_ItemClick")
            Me.ClearCheckedState()
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub mcbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFM = StateFillingGrid.Filling Then : Return : End If
            'If String.IsNullOrEmpty(Me.mcbDistributor.Text) Then : Return : End If
            If Me.mcbDistributor.SelectedIndex <= -1 Then : Return : End If
            If Me.mcbDistributor.Value Is Nothing Then : Return : End If
            If Not (Me.mcbDistributor.SelectedItem Is Nothing) Then
                Me.clsDistHistrory.GetDataView(Me.mcbDistributor.Value.ToString(), Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
                If Me.btnAgrementBrand.Checked = True Then
                    Me.Bindgrid(Me.clsDistHistrory.ViewAgreement())
                ElseIf Me.btnMarketingProgram.Checked = True Then
                    Me.Bindgrid(Me.clsDistHistrory.ViewMarketingProgram())
                ElseIf Me.btnProject.Checked = True Then
                    Me.Bindgrid(clsDistHistrory.ViewProject())
                ElseIf Me.btnPurchaseOrder.Checked = True Then
                    Me.Bindgrid(Me.clsDistHistrory.ViewPurchaseOrder())
                ElseIf Me.btnOthers.Checked Then
                    Me.Bindgrid(Me.clsDistHistrory.viewOthers())
                Else
                    Me.Bindgrid(Nothing)
                End If
            Else
                Me.Bindgrid(Nothing)
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbDistributor_ValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rowFilter As String = "DISTRIBUTOR_NAME LIKE '%" & Me.mcbDistributor.Text & "%'"
            Me.clsDistHistrory.ViewDistributor().RowFilter = rowFilter
            Me.BindMultiColumnCombo(Me.clsDistHistrory.ViewDistributor(), rowFilter)
            Dim itemCount As Integer = Me.mcbDistributor.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() + " Item(s) Found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DistributorHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.FilterEditor1.Visible = False
            Me.pngHeight = Me.grpDate.Height
            Me.grpDate.Height = 0
        Catch ex As Exception
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_DistributorHistory_Load")
        Finally
            Me.SFM = StateFillingMCB.HasFilled
            Me.SFG = StateFillingGrid.HasFilled
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnAplyFilterPencapaian_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyFilterPencapaian.Click
        Try
            Me.mcbDistributor_ValueChanged(Me.mcbDistributor, New EventArgs())
        Catch ex As Exception

        End Try
    End Sub
#End Region

End Class
