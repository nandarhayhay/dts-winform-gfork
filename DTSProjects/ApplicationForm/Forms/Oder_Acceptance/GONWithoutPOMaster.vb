Imports System.Threading
Imports System
Imports Nufarm.Domain
Imports NufarmBussinesRules.common.Helper
Imports DTSProjects.GonNonPODist
Imports System.Configuration
Imports System.Globalization
Public Class GONWithoutPOMaster
    Private SFm As StateFillingMCB
    Private SFG As StateFillingGrid
    Private LD As Loading
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Friend MustReload As Boolean = False
    Private m_SGonManager As NufarmBussinesRules.OrderAcceptance.SeparatedGONManager
    Private m_clsSampleGon As NufarmBussinesRules.OrderAcceptance.SampleGON
    Private PageIndex As Int32 = 0, PageSize As Integer = 0, TotalIndex As Integer = 0, RowCount As Integer = 0
    Private m_DataType As NufarmBussinesRules.common.Helper.DataTypes = NufarmBussinesRules.common.Helper.DataTypes.VarChar
    Private m_Criteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private OriginalCriteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private isUndoingCriteria As Boolean = False : Friend CMain As Main = Nothing
    Private IsSavingData As Boolean = False
    Private IsHOUser As Boolean = NufarmBussinesRules.SharedClass.IsUserHO
    Private IsSystemAdmin As Boolean = False
    Private OriginalData As New NufarmBussinesRules.common.dataTManager()
    Private hasLoadGrid As Boolean = False
    Private hasLoadForm As Boolean = False
    Private FData As FilterData = FilterData.GonOnly
    Private WithEvents frmSPPBrep As FrmSPPBReport
    Private listSPPB As New List(Of String)
    Private OriginalWaterMarkText As String
    Private isLoadingRow As Boolean = False
    'Private DVMConversiProduct As DataView = Nothing
    'Private DVSampleProduct As DataView = Nothing
    Private ReadOnly Property SGonManager() As NufarmBussinesRules.OrderAcceptance.SeparatedGONManager
        Get
            If m_SGonManager Is Nothing Then
                m_SGonManager = New NufarmBussinesRules.OrderAcceptance.SeparatedGONManager()
            End If
            Return m_SGonManager
        End Get
    End Property
    Private ReadOnly Property clsSampleGon() As NufarmBussinesRules.OrderAcceptance.SampleGON
        Get
            If m_clsSampleGon Is Nothing Then
                Me.m_clsSampleGon = New NufarmBussinesRules.OrderAcceptance.SampleGON()
            End If
            Return m_clsSampleGon
        End Get
    End Property
    Private Enum FilterData
        All
        GonOnly
        Completed
        PendingGon
    End Enum
    Private Enum StateFillingMCB
        Filling
        HasFilled
    End Enum
    Private Enum StateFillingGrid
        HasFilled
        Filling
    End Enum
    Private Enum GridSelect
        GridPO
        GridPencapaian
    End Enum
    Private Enum StatusProgress
        None
        Processing
    End Enum

    Private Sub ShowProceed()
        LD = New Loading() : LD.Show() : LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            LD.Refresh() : Thread.Sleep(100) : Application.DoEvents()
        End While
        Thread.Sleep(100) : LD.Close() : LD = Nothing
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
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.GONWithoutPOMaster Then
                Me.btnAddNew.Visible = True
            Else
                Me.btnAddNew.Visible = False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.GONWithoutPOMaster Then
                Me.btnEdit.Visible = True
            Else
                Me.btnEdit.Visible = False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.GONWithoutPOMaster Then
                Me.AdvancedTManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.AdvancedTManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
        Else
            Me.btnEdit.Visible = True
            Me.AdvancedTManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Me.btnAddNew.Visible = True
        End If
        'Dim IsDotMatrixPrint As Boolean = CBool(ConfigurationManager.AppSettings("DotMatrixPrint"))
        'Me.btnPrintcustoms.Visible = Not IsDotMatrixPrint
    End Sub
    Friend Sub LoadData()
        Me.SFG = StateFillingGrid.Filling

        Me.PageIndex = 1 : Me.PageSize = 1000 : Me.AdvancedTManager1.cbPageSize.Text = "1000"

        Me.AdvancedTManager1.cbCategory.Items.Clear() : Me.AdvancedTManager1.btnCriteria.Text = "*.*"
        Me.AdvancedTManager1.btnCriteria.Text = "*.*" : Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
        Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
        If Me.OnlyWinGonToolStripMenuItem.Checked Then
            Me.FData = FilterData.GonOnly
        ElseIf AllToolStripMenuItem.Checked Then
            Me.FData = FilterData.All
        ElseIf PendingGONToolStripMenuItem.Checked Then
            Me.FData = FilterData.PendingGon
        ElseIf POStatusCompletedToolStripMenuItem.Checked Then
            Me.FData = FilterData.Completed
        End If
        With Me.OriginalData
            .CriteriaSearch = "LIKE"
            If Me.AdvancedTManager1.txtMaxRecord.Text = "" Then
                .MaxRecord = 0 : Else : .MaxRecord = Convert.ToInt32(Me.AdvancedTManager1.txtMaxRecord.Text.Trim())
            End If
            If Me.AdvancedTManager1.txtSearch.Text = "" Then
                .SearchValue = ""
            Else
                .SearchValue = RTrim(Me.AdvancedTManager1.txtSearch.Text)
            End If
            If Me.AdvancedTManager1.cbCategory.Text = "" Then
                .SearchBy = "GON_NUMBER"
            Else
                .SearchBy = Me.AdvancedTManager1.cbCategory.Text
            End If
            Me.RunQuery(.SearchValue, .SearchBy, FData)
        End With

        If AdvancedTManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.AdvancedTManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.AdvancedTManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub
    Private Sub GONWithoutPOMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.LoadData()
            With Me.AdvancedTManager1.cbCategory
                .Items.Add("SHIP_TO_CUSTOMER")
                .Items.Add("PO_NUMBER")
                .Items.Add("PO_DATE")
                .Items.Add("SPPB_NUMBER")
                .Items.Add("SPPB_DATE")
                .Items.Add("GON_NUMBER")
                .Items.Add("GON_DATE")
                .Items.Add("WARHOUSE")
                .Items.Add("TRANSPORTER_NAME")
                .Items.Add("POLICE_NO_TRANS")
                .Items.Add("DRIVER_TRANS")
                .Items.Add("ITEM")
                .Items.Add("QUANTITY")
                .Items.Add("COLLY_BOX")
                .Items.Add("COLLY_PACKSIZE")
                .Items.Add("BATCH_NO")
                .Items.Add("CreatedBy")
            End With
            Me.IsSystemAdmin = CMain.IsSystemAdministrator
            Me.ReadAccess()
        Catch ex As Exception
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.LogMyEvent(ex.Message, Me.Name + "_GONWithoutPOMaster_Load")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.SFG = StateFillingGrid.HasFilled : Me.SFm = StateFillingMCB.HasFilled
            Me.hasLoadForm = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GONWithoutPOMaster_ClosingForm() Handles MyBase.ClosingForm
        'PERGUDANGAN PT. BGR LOGISTIC JL KESTRIAN,NO 22 A DESA SIDOKERTO KEC BUDURAN,KAB SIDOARJO JAWA TIMUR

        '22/23 001406 -02
        '        C8713ZN()
        '        JAMINGUN()
    End Sub
    Private Sub AddNewGONFG()
        Dim OSPPBHeader As New SPPBHeader()
        Dim OGONHeader As New GONHeader()
        Dim frmInputGon As New GonNonPODist()
        With frmInputGon
            .CMain = Me.CMain
            .OGONHeader = OGONHeader
            .OSPPBHeader = OSPPBHeader
            .SForm = StatusForm.Insert
            .initializedData()
            .StartPosition = FormStartPosition.CenterScreen
            .Opener = Me
            .ShowDialog()
        End With
    End Sub
    ''' <summary>
    ''' sample gon
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddNewGonVG()
        Dim OSPPBHeader As New SPPBHeader()
        Dim OGONHeader As New GONHeader()
        Dim frmInputGon As New GonSample()
        With frmInputGon
            .CMain = Me.CMain
            .OGONHeader = OGONHeader
            .OSPPBHeader = OSPPBHeader
            .SForm = SaveMode.Insert
            .initializedData()
            .StartPosition = FormStartPosition.CenterScreen
            .Opener = Me
            .ShowDialog()
        End With
    End Sub
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Me.Cursor = Cursors.WaitCursor
        Dim item As DevComponents.DotNetBar.BaseItem = DirectCast(sender, DevComponents.DotNetBar.BaseItem)
        Select Case item.Name
            Case "btnRenameColumn"
                Dim MC As New ManipulateColumn()
                MC.ShowInTaskbar = False
                MC.grid = Me.AdvancedTManager1.GridEX1
                MC.FillcomboColumn()
                MC.ManipulateColumnName = "Rename"
                MC.TopMost = True
                MC.Show(Me.Bar2)
            Case "btnShowFieldChooser"
                Me.AdvancedTManager1.GridEX1.ShowFieldChooser(Me)
            Case "btnSettingGrid"
                Dim SetGrid As New SettingGrid()
                SetGrid.Grid = AdvancedTManager1.GridEX1
                SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                SetGrid.ShowDialog()
            Case "btnPrint"
                Me.GridEXPrintDocument1.GridEX = Me.AdvancedTManager1.GridEX1
                Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                    Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                End If
                'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Me.PrintPreviewDialog1.Document.Print()
                End If
            Case "btnPageSettings"
                Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                Me.PageSetupDialog1.ShowDialog(Me)
            Case "btnCustomFilter"
                Me.FilterEditor1.Visible = True
                Me.FilterEditor1.SortFieldList = False
                Me.FilterEditor1.SourceControl = Me.AdvancedTManager1.GridEX1
                Me.AdvancedTManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                Me.AdvancedTManager1.GridEX1.RemoveFilters()
                Me.GetStateChecked(btnCustomFilter)
            Case "btnFilterEqual"
                Me.FilterEditor1.Visible = False
                Me.AdvancedTManager1.GridEX1.RemoveFilters()
                Me.AdvancedTManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Me.GetStateChecked(Me.btnFilterEqual)
                'Case "btnAddNew"

            Case "btnVarFG"
                Me.AddNewGONFG()
                If Me.MustReload Then
                    Me.ReloadOpener()
                End If
            Case "btnSampleUnreg"
                Me.AddNewGonVG() 'SAMPLE GON
                If Me.MustReload Then
                    Me.ReloadOpener()
                End If
            Case "btnExport"
                Me.SaveFileDialog1.Title = "Define the location File"
                Me.SaveFileDialog1.OverwritePrompt = True
                Me.SaveFileDialog1.DefaultExt = ".xls"
                Me.SaveFileDialog1.Filter = "All Files|*.*"
                Me.SaveFileDialog1.RestoreDirectory = True
                If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                    Me.GridEXExporter1.GridEX = Me.AdvancedTManager1.GridEX1
                    Me.GridEXExporter1.IncludeExcelProcessingInstruction = True
                    Me.GridEXExporter1.IncludeFormatStyle = True
                    Me.GridEXExporter1.Export(FS)
                    FS.Close()
                    MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Case "btnEdit"
                If Me.AdvancedTManager1.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    Me.Cursor = Cursors.WaitCursor
                    Dim gonNumber As Object = AdvancedTManager1.GridEX1.GetValue("GON_NUMBER")
                    Dim PONumber As Object = AdvancedTManager1.GridEX1.GetValue("PO_NUMBER")
                    ''cek PO Category
                    Dim POCateGory As String = Me.AdvancedTManager1.GridEX1.GetValue("PO_CATEGORY")
                    If POCateGory = "UNREG_FIN_GOODS" Then
                        If Not IsNothing(gonNumber) And Not IsDBNull(gonNumber) Then
                            If Not String.IsNullOrEmpty(gonNumber) And gonNumber <> "PENDING GON" Then
                                Me.PullAndShowDataSP("", gonNumber, SaveMode.Update)
                            Else
                                Me.PullAndShowDataSP(PONumber, "", SaveMode.Update)
                            End If
                        Else
                            Me.PullAndShowDataSP(PONumber, "", SaveMode.Update)
                        End If
                    ElseIf POCateGory = "REG_FIN_GOODS" Then
                        If Not IsNothing(gonNumber) And Not IsDBNull(gonNumber) Then
                            If Not String.IsNullOrEmpty(gonNumber) And gonNumber <> "PENDING GON" Then
                                Me.PullAndShowData("", gonNumber, StatusForm.Edit)
                            Else
                                Me.PullAndShowData(PONumber, "", StatusForm.Edit)
                            End If
                        Else
                            Me.PullAndShowData(PONumber, "", StatusForm.Edit)
                        End If
                    End If

                    If Me.MustReload Then
                        Me.ReloadOpener()
                    End If
                End If
            Case "btnRefresh"
                Me.ReloadOpener()
            Case "btnCurrentSelection"
                If Me.AdvancedTManager1.GridEX1.DataSource Is Nothing Then
                    Return
                End If
                If Me.AdvancedTManager1.GridEX1.RecordCount <= 0 Then
                    Return
                End If
                If Me.AdvancedTManager1.GridEX1.SelectedItems Is Nothing Then
                    Return
                End If
                If Me.AdvancedTManager1.GridEX1.SelectedItems.Count <= 0 Then
                    Return
                End If
                Dim sppbNo As String = ""
                'check selecttion
                If Not IsNothing(Me.AdvancedTManager1.GridEX1.GetRow.Group) Then
                    If Me.AdvancedTManager1.GridEX1.GetRow.Group.Column.Key.Contains("SPPB") Then
                        sppbNo = Me.AdvancedTManager1.GridEX1.GetRow.GroupValue
                    ElseIf Me.AdvancedTManager1.GridEX1.GetRow.Group.Column.Key.Contains("GON_NUMBER") Then
                        'GonNumber = Me.AdvancedTManager1.GridEX1.GetRow.GroupValue
                        Me.AdvancedTManager1.GridEX1.MoveNext()
                        sppbNo = Me.AdvancedTManager1.GridEX1.GetValue("SPPB_NUMBER")
                    Else
                        sppbNo = Me.AdvancedTManager1.GridEX1.GetValue("SPPB_NUMBER")
                    End If
                Else
                    sppbNo = Me.AdvancedTManager1.GridEX1.GetValue("SPPB_NUMBER")
                End If
                ''get data for printing by SPPB number
                Me.PrintCurrentSelSPPB(sppbNo)
            Case "btnPrintcustoms"
                PrintCustomSPPB()
        End Select
        Me.StatProg = StatusProgress.None
        Me.SFm = StateFillingMCB.HasFilled : Me.SFG = StateFillingGrid.HasFilled
        Me.MustReload = False
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub PrintCustomSPPB()
        Me.frmSPPBrep = New FrmSPPBReport()
        With frmSPPBrep
            'get data SPPB Header
            .isSingleReport = False
            .ShowInTaskbar = False
            Me.OriginalWaterMarkText = frmSPPBrep.txtSearch.WaterMarkText
            .StartPosition = FormStartPosition.CenterScreen
            Dim tblHeader As New DataTable("T_HEADER")
            Dim tblDetail As New DataTable("Ref_Other_SPPB")
            Me.SGonManager.getSPPBReportData("", False, tblHeader, tblDetail)
            .GridEX1.SetDataBinding(tblHeader, "")
            .GridEX1.UnCheckAllRecords()
            .hasLoadForm = True
            .ShowDialog(Me)
            Me.isLoadingRow = False
        End With
        If Not IsNothing(Me.frmSPPBrep) Then
            Me.frmSPPBrep.Dispose() : frmSPPBrep = Nothing
        End If
    End Sub
    Private Sub DisplayData(ByVal dt As DataTable)
        With Me.frmSPPBrep
            .ReportDoc = Nothing
            Application.DoEvents()
            Dim rptSPPB As New SPPBOther()
            .ReportDoc = rptSPPB
            .ReportDoc.SetDataSource(dt)
            .crvSPPB.ReportSource = .ReportDoc
        End With
        Application.DoEvents()
    End Sub
    Private Sub Grid_RowCheckStateChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles frmSPPBrep.Grid_RowCheckStateChanged
        Try
            If Me.isLoadingRow Then : Return : End If
            If Not Me.frmSPPBrep.hasLoadForm Then : Return : End If
            Cursor = Cursors.WaitCursor
            Me.isLoadingRow = True
            Dim SPPBNO As String = ""
            If e.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                SPPBNO = e.Row.Cells("SPPB_NUMBER").Value
                If Not listSPPB.Contains(SPPBNO) Then
                    listSPPB.Add(SPPBNO)
                End If
            End If
            Dim dt As DataTable = Me.SGonManager.getSPPBReportData(listSPPB)
            Dim dtTable As New DataTable("Ref_Other_SPPB")
            With dtTable
                .Columns.Add("FKApp", Type.GetType("System.Int32"))
                .Columns.Add("PO_NUMBER", Type.GetType("System.String"))
                .Columns.Add("SPPB_NUMBER", Type.GetType("System.String"))
                .Columns.Add("SPPB_DATE", Type.GetType("System.DateTime"))
                .Columns.Add("PO_DATE", Type.GetType("System.DateTime"))
                .Columns.Add("ITEM", Type.GetType("System.String"))
                .Columns.Add("PO_ORIGINAL", Type.GetType("System.Decimal"))
                .Columns("PO_ORIGINAL").DefaultValue = 0
                .Columns.Add("STATUS", Type.GetType("System.String"))
                .Columns.Add("SHIP_TO_CUSTOMER", Type.GetType("System.String"))
                .Columns.Add("QUANTITY", Type.GetType("System.String"))
                .Columns.Add("COLLY_BOX", Type.GetType("System.String"))
                .Columns.Add("COLLY_PACKSIZE", Type.GetType("System.String"))
                .Columns.Add("SHIP_TO_WARHOUSE", Type.GetType("System.String"))
                .Columns("SHIP_TO_WARHOUSE").DefaultValue = "Plant Merak" 'di isi dan di perbaiki nantinya
            End With
          
            Dim info As New CultureInfo("id-ID")
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim POOriginal As Decimal = 0
                Dim row As DataRow = dt.Rows(i)
                Dim newRow As DataRow = dtTable.NewRow()
                newRow.BeginEdit()
                newRow("PO_NUMBER") = row("PO_NUMBER")
                newRow("SPPB_NUMBER") = row("SPPB_NUMBER")
                newRow("SPPB_DATE") = row("SPPB_DATE")
                newRow("PO_DATE") = row("PO_DATE")
                newRow("ITEM") = row("ITEM")
                If Not IsNothing(row("PO_ORIGINAL")) And Not IsDBNull(row("PO_ORIGINAL")) Then
                    POOriginal = row("PO_ORIGINAL")
                End If
                Dim oVol1 As Object = row("VOL1"), oVol2 As Object = row("VOL2")
                Dim oUnit1 As Object = row("UNIT1"), oUnit2 As Object = row("UNIT2")
                Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                Dim col1 As Integer = 0
                Dim collyBox As String = "", collyPackSize As String = ""
                If POOriginal >= Dvol1 Then
                    col1 = Convert.ToInt32(Decimal.Truncate(POOriginal / Dvol1))
                    collyBox = IIf(col1 <= 0, "", String.Format("{0:g} {1}", col1, strUnit1))
                    Dim lqty As Decimal = POOriginal Mod Dvol1
                    Dim ilqty As Integer = 0
                    If lqty >= 1 Then
                        'Dim c As Decimal = Decimal.Remainder(GonQty, Dvol1)
                        ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                    ElseIf lqty > 0 And lqty < 1 Then
                        ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                        'ilqty = ilqty + DVol2
                    End If
                    collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                ElseIf POOriginal > 0 Then ''gon kurang dari 1 coly
                    Dim ilqty As Integer = Convert.ToInt32((POOriginal / Dvol1) * DVol2)
                    collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                End If
                newRow("PO_ORIGINAL") = POOriginal
                newRow("COLLY_BOX") = collyBox
                newRow("COLLY_PACKSIZE") = collyPackSize
                newRow("PO_ORIGINAL") = POOriginal
                newRow("STATUS") = row("STATUS")
                newRow("SHIP_TO_CUSTOMER") = row("SHIP_TO_CUSTOMER")
                Dim UnitOfMeasure = row("UnitOfMeasure").ToString()
                newRow("QUANTITY") = String.Format(info, "{0:#,##0.000} {1}", POOriginal, UnitOfMeasure.ToString())
                newRow.EndEdit()
                dtTable.Rows.Add(newRow)
            Next
            dtTable.AcceptChanges()
            Me.DisplayData(dtTable)
            Me.isLoadingRow = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.isLoadingRow = False
            Cursor = Cursors.Default
            e.Row.CheckState = e.OldCheckState
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Private Sub Grid_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles frmSPPBrep.Grid_KeyDown
        If e.KeyCode = Keys.F7 Then
            Cursor = Cursors.WaitCursor
            Try
                Dim FE As New Janus.Windows.GridEX.Export.GridEXExporter()
                Me.Cursor = Cursors.WaitCursor
                FE.IncludeHeaders = True
                FE.SheetName = "SPPB_HEADER_DATA"
                FE.IncludeFormatStyle = False
                FE.IncludeExcelProcessingInstruction = True
                FE.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                Dim SD As New SaveFileDialog()
                SD.OverwritePrompt = True
                SD.DefaultExt = ".xls"
                SD.Filter = "All Files|*.*"
                SD.RestoreDirectory = True
                SD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                If SD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Using FS As New System.IO.FileStream(SD.FileName, IO.FileMode.Create)
                        FE.GridEX = Me.frmSPPBrep.GridEX1
                        FE.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & SD.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                End If
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Grid_KeyDown")
            End Try
        End If
        Cursor = Cursors.Default
    End Sub
    Private Sub Search_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmSPPBrep.Search_Enter
        'If (this.txtSearchKios.WaterMarkText.Equals(this.OriginalWaterMarkText)) Then
        '    { this.txtSearchKios.Text = string.Empty; this.txtSearchKios.WaterMarkText = string.Empty; }
        If Me.frmSPPBrep.txtSearch.WaterMarkText.Equals(Me.OriginalWaterMarkText) Then
            Me.frmSPPBrep.txtSearch.Text = String.Empty
            Me.frmSPPBrep.txtSearch.WaterMarkText = String.Empty
        End If
    End Sub
    Private Sub Search_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmSPPBrep.Search_Leave
        'if (this.txtSearchKios.Text == string.Empty) { this.txtSearchKios.WaterMarkText = this.OriginalWaterMarkText; }
        If Me.frmSPPBrep.txtSearch.Text = String.Empty Then
            Me.frmSPPBrep.txtSearch.WaterMarkText = Me.OriginalWaterMarkText
        End If
    End Sub
    Private Sub Search_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles frmSPPBrep.Search_KeyDown

        If e.KeyCode = Keys.Enter Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Me.isLoadingRow = True
                'get sppb data
                Dim SearchSPPB As String = Me.frmSPPBrep.txtSearch.Text.Trim()
                Dim tblHeader As DataTable = Me.SGonManager.getSPPBData(SearchSPPB)
                frmSPPBrep.GridEX1.SetDataBinding(tblHeader, "")
                For Each row As Janus.Windows.GridEX.GridEXRow In frmSPPBrep.GridEX1.GetRows()
                    Dim SPPBNO As String = row.Cells("SPPB_NUMBER").Value
                    If Me.listSPPB.Contains(SPPBNO) Then
                        row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked
                    Else
                        row.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked
                    End If
                Next
                Me.isLoadingRow = False
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.isLoadingRow = False
                Cursor = Cursors.Default
                Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Search_KeyDown")
            End Try
        End If
    End Sub

    Private Sub PrintCurrentSelSPPB(ByVal SPPBNo As String)
        Dim dtTable As New DataTable("Ref_Other_SPPB")
        With dtTable
            .Columns.Add("FKApp", Type.GetType("System.Int32"))
            .Columns.Add("PO_NUMBER", Type.GetType("System.String"))
            .Columns.Add("SPPB_NUMBER", Type.GetType("System.String"))
            .Columns.Add("SPPB_DATE", Type.GetType("System.DateTime"))
            .Columns.Add("PO_DATE", Type.GetType("System.DateTime"))
            .Columns.Add("ITEM", Type.GetType("System.String"))
            .Columns.Add("PO_ORIGINAL", Type.GetType("System.Decimal"))
            .Columns("PO_ORIGINAL").DefaultValue = 0
            .Columns.Add("STATUS", Type.GetType("System.String"))
            .Columns.Add("SHIP_TO_CUSTOMER", Type.GetType("System.String"))
            .Columns.Add("QUANTITY", Type.GetType("System.String"))
            .Columns.Add("COLLY_BOX", Type.GetType("System.String"))
            .Columns.Add("COLLY_PACKSIZE", Type.GetType("System.String"))
            .Columns.Add("SHIP_TO_WARHOUSE", Type.GetType("System.String"))
            .Columns("SHIP_TO_WARHOUSE").DefaultValue = "Plant Merak" 'di isi dan di perbaiki nantinya
        End With
        Dim tblDummy As New DataTable("Ref_Other_SPPB")
        Me.SGonManager.getSPPBReportData(SPPBNo, True, Nothing, tblDummy)
        'Dim POCateGory As String = Me.AdvancedTManager1.GridEX1.GetValue("PO_CATEGORY")
        Dim info As New CultureInfo("id-ID")
        For i As Integer = 0 To tblDummy.Rows.Count - 1
            Dim POOriginal As Decimal = 0
            Dim row As DataRow = tblDummy.Rows(i)
            Dim newRow As DataRow = dtTable.NewRow()
            newRow.BeginEdit()
            newRow("PO_NUMBER") = row("PO_NUMBER")
            newRow("SPPB_NUMBER") = row("SPPB_NUMBER")
            newRow("SPPB_DATE") = row("SPPB_DATE")
            newRow("PO_DATE") = row("PO_DATE")
            newRow("ITEM") = row("ITEM")
            If Not IsNothing(row("PO_ORIGINAL")) And Not IsDBNull(row("PO_ORIGINAL")) Then
                POOriginal = row("PO_ORIGINAL")
            End If
            Dim oVol1 As Object = row("VOL1"), oVol2 As Object = row("VOL2")
            Dim oUnit1 As Object = row("UNIT1"), oUnit2 As Object = row("UNIT2")
            Dim ValidData As Boolean = True
            If oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                Me.ShowMessageError(row("ITEM").ToString() & ", colly for Volume 1 has not been set yet")
                ValidData = False
            ElseIf oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                Me.ShowMessageError(row("ITEM").ToString() & ", colly for Volume 2 has not been set yet")
                ValidData = False
            ElseIf oUnit1 Is Nothing Or oUnit2 Is DBNull.Value Then
                Me.ShowMessageError(row("ITEM").ToString() & ", colly for Unit 1 has not been set yet")
                ValidData = False
            ElseIf oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                Me.ShowMessageError(row("ITEM").ToString() & ", colly for Unit 2 has not been set yet")
                ValidData = False
            End If
            If Not ValidData Then : Cursor = Cursors.Default : Return : End If
            Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
            Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
            Dim col1 As Integer = 0
            Dim collyBox As String = "", collyPackSize As String = ""
            If POOriginal >= Dvol1 Then
                col1 = Convert.ToInt32(Decimal.Truncate(POOriginal / Dvol1))
                collyBox = IIf(col1 <= 0, "", String.Format("{0:g} {1}", col1, strUnit1))
                Dim lqty As Decimal = POOriginal Mod Dvol1
                Dim ilqty As Integer = 0
                If lqty >= 1 Then
                    'Dim c As Decimal = Decimal.Remainder(GonQty, Dvol1)
                    ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                ElseIf lqty > 0 And lqty < 1 Then
                    ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                    'ilqty = ilqty + DVol2
                End If
                collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
            ElseIf POOriginal > 0 Then ''gon kurang dari 1 coly
                Dim ilqty As Integer = Convert.ToInt32((POOriginal / Dvol1) * DVol2)
                collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
            End If
            newRow("PO_ORIGINAL") = POOriginal
            newRow("COLLY_BOX") = collyBox
            newRow("COLLY_PACKSIZE") = collyPackSize
            newRow("STATUS") = row("STATUS")
            newRow("SHIP_TO_CUSTOMER") = row("SHIP_TO_CUSTOMER")
            Dim UnitOfMeasure = row("UnitOfMeasure").ToString()
            newRow("QUANTITY") = String.Format(info, "{0:#,##0.000} {1}", POOriginal, UnitOfMeasure.ToString())
            newRow.EndEdit()
            dtTable.Rows.Add(newRow)
        Next
        dtTable.AcceptChanges()
        Me.frmSPPBrep = New FrmSPPBReport()
        With frmSPPBrep
            .ShowInTaskbar = False
            .StartPosition = FormStartPosition.CenterScreen
            .isSingleReport = True
            Dim rptSPPB As New SPPBOther()
            rptSPPB.SetDataSource(dtTable)
            .ReportDoc = rptSPPB
            .crvSPPB.ReportSource = .ReportDoc
            .crvSPPB.DisplayGroupTree = False
            Me.isLoadingRow = True
            .ShowDialog(Me)
        End With
        If Not IsNothing(Me.frmSPPBrep) Then
            Me.frmSPPBrep.Dispose() : frmSPPBrep = Nothing
        End If
        Me.isLoadingRow = False
    End Sub
    Private Sub setVisibleDateControl(ByVal isVisible As Boolean)
        AdvancedTManager1.lblFrom.Visible = isVisible
        AdvancedTManager1.lblUntil.Visible = isVisible
        AdvancedTManager1.dtPicFrom.Visible = isVisible
        AdvancedTManager1.dtPicUntil.Visible = isVisible
    End Sub
    Private Sub SetButtonStatus()
        Me.AdvancedTManager1.btnGoFirst.Enabled = Me.PageIndex <> 1
        Me.AdvancedTManager1.btnGoPrevios.Enabled = Me.PageIndex <> 1
        Me.AdvancedTManager1.btnNext.Enabled = Me.PageIndex <> TotalIndex : If TotalIndex <= 0 Then : AdvancedTManager1.btnNext.Enabled = False : End If
        Me.AdvancedTManager1.btnGoLast.Enabled = Me.PageIndex <> TotalIndex : If TotalIndex <= 0 Then : AdvancedTManager1.btnGoLast.Enabled = False : End If
        If (AdvancedTManager1.GridEX1.DataSource Is Nothing) Then
            AdvancedTManager1.btnGoFirst.Enabled = False : AdvancedTManager1.btnGoLast.Enabled = False
            AdvancedTManager1.btnNext.Enabled = False : AdvancedTManager1.btnGoLast.Enabled = False
        ElseIf AdvancedTManager1.GridEX1.RecordCount <= 0 Then
            AdvancedTManager1.btnGoFirst.Enabled = False : AdvancedTManager1.btnGoLast.Enabled = False
            AdvancedTManager1.btnNext.Enabled = False : AdvancedTManager1.btnGoLast.Enabled = False
        ElseIf AdvancedTManager1.GridEX1.RecordCount <= 0 Then
        End If
    End Sub
    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String, ByVal FD As FilterData)
        Dim Dv As DataView = Nothing
        If Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.DateTime And Me.AdvancedTManager1.dtPicFrom.Visible Then
            If Not String.IsNullOrEmpty(SearchString) Then
                Select Case FD
                    Case FilterData.All
                        Dv = Me.SGonManager.PopulateQueryAllData(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
                    Case FilterData.Completed
                        Dv = Me.SGonManager.PopulateQueryStatusCompleted(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
                    Case FilterData.GonOnly
                        Dv = Me.SGonManager.PopulateQuery(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
                    Case FilterData.PendingGon
                        Dv = Me.SGonManager.PopulateQueryPendingGon(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
                End Select
            Else
                '//make string StartDate and EndDate coombined and splitted by hypen
                'string strDate = string.Format("{0:MM/dd/yyyy}",tManager1.dtPicFrom.Value);
                'strDate += "-";
                'strDate += string.Format("{0:MM/dd/yyyy}", tManager1.dtPicUntil.Value);
                'DtView = tManager1.QueryComposite.GetQuery(PageSize, pageIndex, _ColumnKey, strDate, _CriteriaSearh, _DataType).Tables[0].DefaultView;
                Dim strDate As String = String.Format("{0:MM/dd/yyyy}", Me.AdvancedTManager1.dtPicFrom.Value)
                strDate += "-"
                strDate += String.Format("{0:MM/dd/yyyy}", Me.AdvancedTManager1.dtPicUntil.Value)
                Select Case FD
                    Case FilterData.All
                        Dv = Me.SGonManager.PopulateQueryAllData(SearchBy, strDate, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
                    Case FilterData.Completed
                        Dv = Me.SGonManager.PopulateQueryStatusCompleted(SearchBy, strDate, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
                    Case FilterData.GonOnly
                        Dv = Me.SGonManager.PopulateQuery(SearchBy, strDate, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
                    Case FilterData.PendingGon
                        Dv = Me.SGonManager.PopulateQueryPendingGon(SearchBy, strDate, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
                End Select
            End If
        Else
            Select Case FD
                Case FilterData.All
                    Dv = Me.SGonManager.PopulateQueryAllData(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
                Case FilterData.Completed
                    Dv = Me.SGonManager.PopulateQueryStatusCompleted(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
                Case FilterData.GonOnly
                    Dv = Me.SGonManager.PopulateQuery(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
                Case FilterData.PendingGon
                    Dv = Me.SGonManager.PopulateQueryPendingGon(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
            End Select
        End If
        If Not IsNothing(Dv) Then
            If (Dv.Count > 0) Then
                Me.AdvancedTManager1.GridEX1.SetDataBinding(Dv, "")
                If Not Me.hasLoadGrid Then
                    Me.AdvancedTManager1.GridEX1.RetrieveStructure()
                    Me.FormatDataGrid()
                    Me.hasLoadGrid = True
                Else
                    AdvancedTManager1.GridEX1.RootTable.Groups(1).Collapse()
                End If
            Else
                Me.AdvancedTManager1.GridEX1.SetDataBinding(Nothing, "")
            End If
        Else
            Me.AdvancedTManager1.GridEX1.SetDataBinding(Nothing, "")
        End If

    End Sub
    Private Sub SetStatusRecord()
        Me.TotalIndex = 0
        Me.AdvancedTManager1.lblResult.Text = String.Format("Found {0} Record(s)", RowCount.ToString())
        If (RowCount <> 0) Then
            Me.TotalIndex = RowCount / Me.PageSize
            If (RowCount - (TotalIndex * PageSize) > 0) Then
                TotalIndex += 1
            End If
        End If
        AdvancedTManager1.lblPosition.Text = String.Format("Page {0} Of {1} page(s)", Me.PageIndex, Me.TotalIndex)
    End Sub
    Private Function getStringCriteriaSearch() As String
        Dim strResult As String = "LIKE"
        Select Case Me.m_Criteria
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
                strResult = "BeginWith"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                strResult = "EndWith"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                strResult = "Equal"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                strResult = "Greater"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                strResult = "GreaterOrEqual"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.In
                strResult = "In"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                strResult = "Less"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                strResult = "LessOrEqual"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                strResult = "LIKE"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
                strResult = "NotEqual"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.NotIn
                strResult = "NotIn"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
                strResult = "Between"
        End Select
        Return strResult
    End Function
    Private Sub FormatDataGrid()
        For Each col As Janus.Windows.GridEX.GridEXColumn In AdvancedTManager1.GridEX1.RootTable.Columns
            If col.Type Is Type.GetType("System.Int32") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                If col.Key.Contains("IDApp") Then
                    col.Visible = False : col.FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    col.ShowInFieldChooser = False
                End If
            ElseIf col.Type Is Type.GetType("System.Decimal") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FormatString = "#,##0.0000"
            ElseIf col.Key.Contains("IDApp") Or col.Key = "FkCode" Then
                col.Visible = False : col.FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                col.ShowInFieldChooser = False
            End If
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            If col.Type Is Type.GetType("System.DateTime") Then
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
                col.EditType = Janus.Windows.GridEX.EditType.TextBox
                col.FormatString = "dd MMMM yyyy"
            End If
            If col.Key = "PO_ORIGINAL" Then
                col.Visible = True
            ElseIf col.Key = "QUANTITY" Then
                col.Visible = False
                col.ShowInFieldChooser = False
                'col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                'col.TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                'col.TotalFormatString = "Total Qty {0:#,##0.0000}"
            ElseIf col.Key = "QUANTITY_UNIT" Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End If
            If col.Key.Contains("Modified") Or col.Key.Contains("Created") Then
                col.Visible = False
            End If
            If col.Key = "STATUS" Then
                col.Caption = "STATUS SPPB"
            End If

        Next
        'buat group 
        ''SPPB number - GON_number
        Dim grpSPPB As New Janus.Windows.GridEX.GridEXGroup()
        With grpSPPB
            .Column = AdvancedTManager1.GridEX1.RootTable.Columns("SPPB_NUMBER")
            .AllowRemove = True
        End With
        AdvancedTManager1.GridEX1.RootTable.Groups.Add(grpSPPB)
        AdvancedTManager1.GridEX1.RootTable.Columns("SPPB_NUMBER").Visible = False
        Dim grpGon As New Janus.Windows.GridEX.GridEXGroup()
        With grpGon
            .Column = AdvancedTManager1.GridEX1.RootTable.Columns("GON_NUMBER")
            .AllowRemove = True
        End With
        AdvancedTManager1.GridEX1.RootTable.Groups.Add(grpGon)
        AdvancedTManager1.GridEX1.RootTable.Columns("GON_NUMBER").Visible = False

        AdvancedTManager1.GridEX1.AutoSizeColumns()
        AdvancedTManager1.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        If (Me.FilterEditor1.Visible) And (Not IsNothing(Me.FilterEditor1.SourceControl)) Then
            AdvancedTManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
        Else
            AdvancedTManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        End If

        Dim FS As New Janus.Windows.GridEX.GridEXFormatStyle()
        With FS
            .ForeColor = Color.Green
        End With
        Dim FC As New Janus.Windows.GridEX.GridEXFormatCondition()
        With FC
            '.TargetColumn = Me.GridEX1.RootTable.Columns("STATUS")
            '.Column = Me.GridEX1.RootTable.Columns("STATUS")
            .FilterCondition = New Janus.Windows.GridEX.GridEXFilterCondition(AdvancedTManager1.GridEX1.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "Completed")
            .FormatStyle = FS
        End With
        AdvancedTManager1.GridEX1.RootTable.FormatConditions.Add(FC)

        Dim FS1 As New Janus.Windows.GridEX.GridEXFormatStyle()
        With FS1
            .ForeColor = Color.Maroon
        End With

        Dim FC1 As New Janus.Windows.GridEX.GridEXFormatCondition()
        With FC1
            '.TargetColumn = AdvancedTManager1.GridEX1.RootTable.Columns("STATUS")
            '.Column = Me.GridEX1.RootTable.Columns("STATUS")
            .FilterCondition = New Janus.Windows.GridEX.GridEXFilterCondition(AdvancedTManager1.GridEX1.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "Partial")
            .FormatStyle = FS1
        End With
        AdvancedTManager1.GridEX1.RootTable.FormatConditions.Add(FC1)

        AdvancedTManager1.GridEX1.GroupByBoxVisible = False
        AdvancedTManager1.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        AdvancedTManager1.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
        AdvancedTManager1.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
        AdvancedTManager1.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        AdvancedTManager1.GridEX1.FrozenColumns = 10
        AdvancedTManager1.GridEX1.RootTable.Groups(1).Collapse()
    End Sub
    Friend Sub GetData()
        If Not Me.SFG = StateFillingGrid.Filling Then : Me.SFG = StateFillingGrid.Filling : End If
        RowCount = 0
        Dim SearchString As String = Me.AdvancedTManager1.txtSearch.Text.Trim(), SearchBy As String = Me.AdvancedTManager1.cbCategory.Text
        If (Me.AdvancedTManager1.cbCategory.Text = "") Then
            SearchBy = "GON_NUMBER"
        End If
        If Me.AdvancedTManager1.txtMaxRecord.Text <> "" Then
            If CInt(Me.AdvancedTManager1.txtMaxRecord.Text) < CInt(Me.AdvancedTManager1.cbPageSize.Text) Then
                Me.PageSize = CInt(Me.AdvancedTManager1.txtMaxRecord.Text)
            Else
                Me.PageSize = CInt(Me.AdvancedTManager1.cbPageSize.Text)
            End If
        Else
            Me.PageSize = CInt(Me.AdvancedTManager1.cbPageSize.Text)
        End If
        Dim MaxRecord As Integer = 0, strCriteriaSearch As String = getStringCriteriaSearch()
        If Me.AdvancedTManager1.txtMaxRecord.Text <> "" Then
            MaxRecord = Convert.ToInt32(Me.AdvancedTManager1.txtMaxRecord.Text.Trim())
        End If

        With Me.OriginalData
            .CriteriaSearch = strCriteriaSearch
            .SearchBy = SearchBy
            .SearchValue = SearchString
            If Me.OnlyWinGonToolStripMenuItem.Checked Then
                Me.FData = FilterData.GonOnly
            ElseIf AllToolStripMenuItem.Checked Then
                Me.FData = FilterData.All
            ElseIf PendingGONToolStripMenuItem.Checked Then
                Me.FData = FilterData.PendingGon
            ElseIf POStatusCompletedToolStripMenuItem.Checked Then
                Me.FData = FilterData.Completed
            End If
            Me.RunQuery(.SearchValue, .SearchBy, FData)
        End With
        'Me.RunQuery(Me.AdvancedTManager1.txtSearch.Text, SearchBy)
        If AdvancedTManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.AdvancedTManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.AdvancedTManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub
    Private Sub UndoCriteria()
        Me.isUndoingCriteria = True
        With Me.AdvancedTManager1
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
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
                    .btnCriteria.Text = "<>"
                    .btnCriteria.CompareOperator = CompareOperator.Between
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
            End Select
        End With
        Me.isUndoingCriteria = False
    End Sub
    Private Sub SetOriginalCriteria()
        Select Case Me.AdvancedTManager1.btnCriteria.CompareOperator
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
            Case CompareOperator.Between
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
        End Select
    End Sub
    Friend Sub ReloadOpener()
        Try
            Me.GetData()
            Me.SetOriginalCriteria()
            Me.SFG = StateFillingGrid.HasFilled
        Catch ex As Exception
            Me.UndoCriteria()
            Me.SFG = StateFillingGrid.HasFilled
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        'GIBRO 20%TABLET
        '        MARVEL()
        '        BLAIT(PLUS)
        'DIPEL 500 500ML
        'BRANTAS 100ML salah code
        'kuproxat salah code
        'JL RAMBA RAYA PERUIVI GRIYA'TAMAN ANGGREK BLOK C NO 1 KUBANG RAYA RIAU
        '        SM/S1/02/0184

        '        SS0184()

        'PowerMax 500 tidak ada belum di tambahkan ke table
    End Sub
    Private Sub AdvancedTManager1_ButonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedTManager1.ButonClick
        Try
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            If (TypeOf (sender) Is Button) Then
                Me.PageIndex = 1
            ElseIf (TypeOf (sender) Is Janus.Windows.EditControls.UIButton) Then
                Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                    Case "btnGoFirst" : Me.PageIndex = 1 : Case "btnGoPrevios" : Me.PageIndex -= 1
                    Case "btnNext" : Me.PageIndex += 1 : Case "btnGoLast" : Me.PageIndex = Me.TotalIndex
                End Select
            End If
            Me.GetData()
            Me.SetOriginalCriteria()
        Catch ex As Exception
            Me.UndoCriteria()
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.SFG = StateFillingGrid.HasFilled
            Me.SFm = StateFillingMCB.HasFilled
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AdvancedTManager1_ChangesCriteria(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedTManager1.ChangesCriteria
        Try
            If Not Me.SFG = StateFillingGrid.HasFilled Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.AdvancedTManager1.txtSearch.Enabled = True
            If Me.isUndoingCriteria Then : Return : End If
            Select Case Me.AdvancedTManager1.btnCriteria.CompareOperator
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
                Case CompareOperator.Between
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
                    Me.AdvancedTManager1.txtSearch.Text = ""
                    Me.AdvancedTManager1.txtSearch.Enabled = False
                    Me.setVisibleDateControl(True)
                Case Else
                    Throw New Exception("Please select another operator")
            End Select
            Me.AdvancedTManager1_ButonClick(Me.AdvancedTManager1.btnSearch, New EventArgs())
            Me.SetOriginalCriteria()
        Catch ex As Exception
            Me.UndoCriteria()
            Me.SFG = StateFillingGrid.HasFilled
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Criteria_Changed")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AdvancedTManager1_CmbSelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedTManager1.CmbSelectedIndexChanged
        Try
            If Me.SFm = StateFillingMCB.Filling Then : Return : End If
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            Me.Cursor = Cursors.WaitCursor : If AdvancedTManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            Select Case Me.AdvancedTManager1.cbCategory.Text
                Case "SHIP_TO_CUSTOMER", "PO_NUMBER", "SPPB_NUMBER", "GON_NUMBER", "WARHOUSE", "TRANSPORTER_NAME", "POLICE_NO_TRANS", _
                     "DRIVER_TRANS", "GON_ITEM", "QUANTITY", "COLLY_BOX", "COLLY_PACKSIZE", "BATCH_NO", "CreatedBy"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
                Case "PO_DATE", "SPPB_DATE", "GON_DATE"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.DateTime
            End Select
            Me.AdvancedTManager1_ButonClick(Me.AdvancedTManager1.btnSearch, New EventArgs())
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled
            Me.LogMyEvent(ex.Message, Me.Name + "_TManager1_CmbSelectedIndexChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AdvancedTManager1_DeleteGridRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles AdvancedTManager1.DeleteGridRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim IDApp As Object = Me.AdvancedTManager1.GridEX1.GetValue("IDApp")
            Dim GonNumber As Object = Me.AdvancedTManager1.GridEX1.GetValue("GON_NUMBER")
            If CInt(IDApp) <= 0 Then
                'filter data dengan IDAppPODetail
                Dim dummyV As DataView = CType(Me.AdvancedTManager1.GridEX1.DataSource, DataView)
                Dim dummyT As DataTable = dummyV.ToTable().Copy()
                Dim IDAppPODetail As Integer = Me.AdvancedTManager1.GridEX1.GetValue("IDAppPODetail")
                Dim rows() As DataRow = dummyT.Select("IDAppPODetail = " & IDAppPODetail & " AND IDApp <> 0 ")
                If rows.Length > 0 Then
                    Me.Cursor = Cursors.Default
                    Me.ShowMessageInfo("Please choose an SPPB which has GON")
                    e.Cancel = True
                End If
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage & vbCrLf & "Operation can not be undone") = Windows.Forms.DialogResult.No Then
                Me.Cursor = Cursors.Default
                e.Cancel = True
                Return
            End If
            Dim FKAppPodetail As Object = Me.AdvancedTManager1.GridEX1.GetValue("IDAppPODetail")
            Dim PO_NUMBER As String = Me.AdvancedTManager1.GridEX1.GetValue("PO_NUMBER")
            Dim GON_NUMBER As Object = Me.AdvancedTManager1.GridEX1.GetValue("GON_NUMBER")
            'Dim mustReload As Boolean = False

            Dim boolSucced As Boolean = Me.SGonManager.delete(IDApp, MustReload, FKAppPodetail, PO_NUMBER, GON_NUMBER, (Me.IsHOUser Or Me.IsSystemAdmin))
            If boolSucced Then : e.Cancel = False
            Else : e.Cancel = True
            End If
            Me.AdvancedTManager1.GridEX1.UpdateData()
            If (Me.AdvancedTManager1.GridEX1.RecordCount <= 0) Then
                If Me.TotalIndex > 1 Then
                    If Me.PageIndex > 1 Then
                        Me.PageIndex -= 1
                    End If
                End If
                Me.SetStatusRecord()
                Me.SetButtonStatus()
            Else
                Me.AdvancedTManager1_ButonClick(Me.AdvancedTManager1.btnSearch, New EventArgs())
            End If

        Catch ex As Exception
            e.Cancel = True : Me.Cursor = Cursors.Default : Me.LogMyEvent(ex.Message, Me.Name + "_AdvancedTManager1_DeleteGridRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AdvancedTManager1_GridCurrentCell_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedTManager1.GridCurrentCell_Changed
        Try
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.RecordCount <= 0 Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            ''check hak akses
            'get createdDate
            Me.Cursor = Cursors.WaitCursor
            Me.btnEdit.Enabled = True
            Dim ocreatedDate As Object = Me.AdvancedTManager1.GridEX1.GetValue("CreatedDate")
            If Not IsNothing(ocreatedDate) And Not IsDBNull(ocreatedDate) Then
                Dim createdDate As System.DateTime = Convert.ToDateTime(Me.AdvancedTManager1.GridEX1.GetValue("CreatedDate"))
                Dim nDay As Integer = DateDiff(DateInterval.Day, createdDate, NufarmBussinesRules.SharedClass.ServerDate)
                If CMain.IsSystemAdministrator Or Me.IsHOUser Then
                Else
                    Me.btnEdit.Enabled = nDay < 3
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
            If Me.SFm = StateFillingMCB.Filling Then : Me.SFm = StateFillingMCB.HasFilled : End If
            'Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, "_AdvancedTManager1_GridCurrentCell_Changed")
        End Try
    End Sub
    Private Sub PullAndShowData(ByVal PONumber As String, ByVal GONNumber As String, ByVal SForm As StatusForm)
        Dim OSPPBHeader As New SPPBHeader()
        Dim OGONHeader As New GONHeader()
        Dim frmInputGon As New GonNonPODist()
        With frmInputGon
            .CMain = Me.CMain
            If Not String.IsNullOrEmpty(GONNumber) And GONNumber <> "PENDING GON" Then
                Me.SGonManager.getFormData(GONNumber, OSPPBHeader, OGONHeader, False)
                .OGONHeader = OGONHeader
                .OSPPBHeader = OSPPBHeader
                .dtGonDetail = Me.SGonManager.getGOnDetail(OGONHeader.GON_NO, False)
            ElseIf Not String.IsNullOrEmpty(PONumber) Then
                Me.SGonManager.getPOData(PONumber, OSPPBHeader, False)
                .OSPPBHeader = OSPPBHeader
                .dtGonDetail = Me.SGonManager.getGOnDetail("", False)
            End If
            .dtGonPODetail = Me.SGonManager.getGonPODetail(OSPPBHeader.PONumber, False)
            .SForm = SForm
            .initializedData()
            .StartPosition = FormStartPosition.CenterScreen
            .Opener = Me
            .ShowDialog()
        End With
    End Sub
    Private Sub PullAndShowDataSP(ByVal PONumber As String, ByVal GONNumber As String, ByVal SForm As SaveMode)
        Dim OSPPBHeader As New SPPBHeader()
        Dim OGONHeader As New GONHeader()
        Dim frmInputGon As New GonSample()
        With frmInputGon
            .CMain = Me.CMain
            If Not String.IsNullOrEmpty(GONNumber) And GONNumber <> "PENDING GON" Then
                Me.clsSampleGon.getFormData(GONNumber, OSPPBHeader, OGONHeader, False)
                .OGONHeader = OGONHeader
                .OSPPBHeader = OSPPBHeader
                .dtGonDetail = Me.clsSampleGon.getGOnDetail(OGONHeader.GON_NO, False)
            ElseIf Not String.IsNullOrEmpty(PONumber) Then
                Me.clsSampleGon.getPOData(PONumber, OSPPBHeader, False)
                .OSPPBHeader = OSPPBHeader
                .dtGonDetail = Me.clsSampleGon.getGOnDetail("", False)
            End If
            .dtGonPODetail = Me.clsSampleGon.getGonPODetail(OSPPBHeader.PONumber, False)
            .SForm = SForm
            .initializedData()
            .StartPosition = FormStartPosition.CenterScreen
            .Opener = Me
            .ShowDialog()
        End With
    End Sub
    Private Sub AdvancedTManager1_GridDoubleClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedTManager1.GridDoubleClicked
        Try
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.RecordCount <= 0 Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim gonNumber As Object = AdvancedTManager1.GridEX1.GetValue("GON_NUMBER")
            Dim PONumber As Object = AdvancedTManager1.GridEX1.GetValue("PO_NUMBER")
            'Dim gonNumber As String = AdvancedTManager1.GridEX1.GetValue("GON_NUMBER").ToString()
            Dim POCateGory As String = Me.AdvancedTManager1.GridEX1.GetValue("PO_CATEGORY")
            If POCateGory = "UNREG_FIN_GOODS" Then
                If Not IsNothing(gonNumber) And Not IsDBNull(gonNumber) Then
                    If Not String.IsNullOrEmpty(gonNumber) And gonNumber <> "PENDING GON" Then
                        Me.PullAndShowDataSP("", gonNumber, SaveMode.Open)
                    Else
                        Me.PullAndShowDataSP(PONumber, "", SaveMode.Open)
                    End If
                Else
                    Me.PullAndShowDataSP(PONumber, "", SaveMode.Open)
                End If
            ElseIf POCateGory = "REG_FIN_GOODS" Then
                If Not IsNothing(gonNumber) And Not IsDBNull(gonNumber) Then
                    If Not String.IsNullOrEmpty(gonNumber) And gonNumber <> "PENDING GON" Then
                        Me.PullAndShowData("", gonNumber, StatusForm.Open)
                    Else
                        Me.PullAndShowData(PONumber, "", StatusForm.Open)
                    End If
                Else
                    Me.PullAndShowData(PONumber, "", StatusForm.Open)
                End If
            End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub AdvancedTManager1_TetxBoxKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles AdvancedTManager1.TetxBoxKeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.Cursor = Cursors.WaitCursor
                Me.AdvancedTManager1_ButonClick(Me.AdvancedTManager1.btnSearch, New EventArgs())
            End If
        Catch ex As Exception
            Me.UndoCriteria()
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.SFG = StateFillingGrid.HasFilled
            Me.SFm = StateFillingMCB.HasFilled
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OnlyWinGonToolStripMenuItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlyWinGonToolStripMenuItem.CheckedChanged
        If Not Me.hasLoadForm Then : Return : End If
        If OnlyWinGonToolStripMenuItem.Checked Then
            Try
                Me.AdvancedTManager1_ButonClick(Me.AdvancedTManager1.btnSearch, New EventArgs())
            Catch ex As Exception
                Me.UndoCriteria()
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Finally
                Me.SFG = StateFillingGrid.HasFilled
                Me.SFm = StateFillingMCB.HasFilled
                Cursor = Cursors.Default
            End Try
        End If
    End Sub

    Private Sub AllToolStripMenuItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllToolStripMenuItem.CheckedChanged
        If Not Me.hasLoadForm Then : Return : End If
        If AllToolStripMenuItem.Checked Then
            Try
                Me.AdvancedTManager1_ButonClick(Me.AdvancedTManager1.btnSearch, New EventArgs())
            Catch ex As Exception
                Me.UndoCriteria()
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Finally
                Me.SFG = StateFillingGrid.HasFilled
                Me.SFm = StateFillingMCB.HasFilled
                Cursor = Cursors.Default
            End Try
        End If

    End Sub

    Private Sub AllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllToolStripMenuItem.Click
        If Not Me.hasLoadForm Then : Return : End If
        Me.OnlyWinGonToolStripMenuItem.Checked = False
        PendingGONToolStripMenuItem.Checked = False
        POStatusCompletedToolStripMenuItem.Checked = False

        AllToolStripMenuItem.Checked = Not AllToolStripMenuItem.Checked
    End Sub

    Private Sub OnlyWinGonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlyWinGonToolStripMenuItem.Click
        If Not Me.hasLoadForm Then : Return : End If
        Me.AllToolStripMenuItem.Checked = False

        'Me.OnlyWinGonToolStripMenuItem.Checked = False
        POStatusCompletedToolStripMenuItem.Checked = False
        PendingGONToolStripMenuItem.Checked = False
        OnlyWinGonToolStripMenuItem.Checked = Not OnlyWinGonToolStripMenuItem.Checked
    End Sub

    Private Sub PendingGONToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendingGONToolStripMenuItem.Click

        Me.OnlyWinGonToolStripMenuItem.Checked = False
        POStatusCompletedToolStripMenuItem.Checked = False
        AllToolStripMenuItem.Checked = False
        PendingGONToolStripMenuItem.Checked = Not PendingGONToolStripMenuItem.Checked

    End Sub

    Private Sub PendingGONToolStripMenuItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendingGONToolStripMenuItem.CheckedChanged
        If Not Me.hasLoadForm Then : Return : End If
        If PendingGONToolStripMenuItem.Checked Then
            Try
                Me.AdvancedTManager1_ButonClick(Me.AdvancedTManager1.btnSearch, New EventArgs())
            Catch ex As Exception
                Me.UndoCriteria()
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Finally
                Me.SFG = StateFillingGrid.HasFilled
                Me.SFm = StateFillingMCB.HasFilled
                Cursor = Cursors.Default
            End Try
        End If

    End Sub

    Private Sub POStatusCompletedToolStripMenuItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POStatusCompletedToolStripMenuItem.CheckedChanged
        If Not Me.hasLoadForm Then : Return : End If

        If POStatusCompletedToolStripMenuItem.Checked Then
            Try
                Me.AdvancedTManager1_ButonClick(Me.AdvancedTManager1.btnSearch, New EventArgs())
            Catch ex As Exception
                Me.UndoCriteria()
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Finally
                Me.SFG = StateFillingGrid.HasFilled
                Me.SFm = StateFillingMCB.HasFilled
                Cursor = Cursors.Default
            End Try
        End If

    End Sub

    Private Sub POStatusCompletedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POStatusCompletedToolStripMenuItem.Click
        Me.OnlyWinGonToolStripMenuItem.Checked = False
        AllToolStripMenuItem.Checked = False
        PendingGONToolStripMenuItem.Checked = False
        POStatusCompletedToolStripMenuItem.Checked = Not Me.POStatusCompletedToolStripMenuItem.Checked
    End Sub
End Class
