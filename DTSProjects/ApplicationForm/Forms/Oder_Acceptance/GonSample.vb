Imports System
Imports NufarmBussinesRules.common.Helper
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Configuration
Public Class GonSample
    Dim WithEvents frmLookUp As New LookUp()
    Friend Opener As GONWithoutPOMaster = Nothing
    Private HasBoundGridGon As Boolean = False
    Private WithEvents frmSPPBrep As FrmSPPBReport
    Public Event SaveGON(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''' <summary>
    ''' only used for editing mode for checked combo product
    ''' </summary>
    ''' <remarks></remarks>
    Friend AreaName As String = ""
    ''' <summary>
    ''' only used for editing mode for checked combo product
    ''' </summary>
    ''' <remarks></remarks>
    Friend TransporterName As String = ""
    Friend Mode As SaveMode = SaveMode.None
    ''' <summary>
    '''  only used for editing mode for checked combo product
    ''' </summary>
    ''' <remarks></remarks>
    Friend SppBrandPackValues() As Object
    Friend DS As New DataSet("DSSPPB_GON")
    ''' <summary>
    ''' to detect that data has saved already
    ''' </summary>
    ''' <remarks></remarks>
    Friend HasSavedSPPB As Boolean = True
    Friend HasSavedGON As Boolean = True
    Private IsloadingRow As Boolean = False
    Private IsloadingMCB As Boolean = False
    ''' <summary>
    ''' to detect if a form has been loaded 
    ''' </summary>
    ''' <remarks></remarks>
    Private HasLoadedForm As Boolean = False
    Friend frmParent As SPPB = Nothing
    ''' <summary>
    ''' for multicolumb combo area only, so that form doesn't need refetching data from server
    ''' </summary>
    ''' <remarks>for multicolumb combo area only, so that form doesn't need refetching data from server</remarks>
    Friend DVTransporter As DataView = Nothing

    ''' <summary>
    ''' for multicolumb combo area only, so that form doesn't need refetching data from server
    ''' </summary>
    ''' <remarks></remarks>
    Friend DVArea As DataView = Nothing

    Private m_clsSampleGon As NufarmBussinesRules.OrderAcceptance.SampleGON = Nothing
    Private WithEvents frmSetstat As FrmSetOtherStatus
    Friend DataToEdit As EditData = EditData.None ''to define what data to edit
    ''' <summary>
    ''' buat di grid gon
    ''' </summary>
    ''' <remarks></remarks>
    Friend dtGonDetail As DataTable = Nothing

    ''' <summary>
    '''buat item find di po brandpack dan dropdown po brandpack
    ''' </summary>
    ''' <remarks></remarks>
    Friend DVSampleProduct As DataView = Nothing

    ''' <summary>
    ''' untuk di bind ke chk product
    ''' </summary>
    ''' <remarks></remarks>
    Friend DVGonProduct As DataView = Nothing

    Friend DVMConversiProduct As DataView = Nothing

    ''' <summary>
    ''' buat PO Detailnya
    ''' </summary>
    ''' <remarks></remarks>
    Friend dtGonPODetail As DataTable = Nothing
    ''' <summary>
    ''' untuk di mcb distributor
    ''' </summary>
    ''' <remarks></remarks>
    Friend DVDistributor As DataView = Nothing
    ''' <summary>
    ''' only used for editing mode from grid manager
    ''' </summary>
    ''' <remarks>only used for editing  mode from grid manager</remarks>
    Friend OSPPBHeader As New NuFarm.Domain.SPPBHeader()
    ''' <summary>
    ''' only used for editing mode from grid manager
    ''' </summary>
    ''' <remarks>only used for editing mode from grid manager</remarks>
    Friend OGONHeader As New NuFarm.Domain.GONHeader()
    Private isNewGON As Boolean = False
    Friend SForm As SaveMode
    Friend CMain As Main = Nothing
    Private IsHOUser As Boolean = NufarmBussinesRules.SharedClass.IsUserHO
    Private IsSystemAdmin = CBool(ConfigurationManager.AppSettings("SysA"))
    Private checkedVals As New List(Of Integer)
    Private mustCheckStatus As Boolean = False
    Private SmallGonEntry As New System.Drawing.Size(760, 157)
    Private NormalGonEntry As New System.Drawing.Size(760, 211)
    Private Property clsGonNonPO() As NufarmBussinesRules.OrderAcceptance.SampleGON
        Get
            If Me.m_clsSampleGon Is Nothing Then
                Me.m_clsSampleGon = New NufarmBussinesRules.OrderAcceptance.SampleGON
            End If
            Return Me.m_clsSampleGon
        End Get
        Set(ByVal value As NufarmBussinesRules.OrderAcceptance.SampleGON)
            Me.m_clsSampleGon = value
        End Set
    End Property
    Friend Enum EditData
        SPPBAndPO
        GON
        None
    End Enum
    'Friend Enum StatusForm
    '    Insert
    '    Edit
    '    Open
    '    None
    'End Enum
    Private Sub setEnabledGONEntry(ByVal isEnabled As Boolean, ByVal includeGrid As Boolean)
        Me.txtGONNO.Enabled = isEnabled
        Me.ChkShiptoCustomer.Enabled = isEnabled
        Me.dtPicGONDate.ReadOnly = Not isEnabled
        Me.mcbGonArea.ReadOnly = Not isEnabled
        Me.mcbTransporter.ReadOnly = Not isEnabled
        Me.chkProduct.ReadOnly = Not isEnabled
        If IsHOUser Or Me.IsSystemAdmin Then
            Me.cmdWarhouse.ReadOnly = Not isEnabled
        End If
        Me.mcbCustomer.ReadOnly = Not isEnabled
        Me.txtPolice_no_Trans.Enabled = isEnabled
        Me.txtDriverTrans.Enabled = isEnabled
        Me.grdGon.Enabled = isEnabled
        Me.txtGonShipto.Enabled = isEnabled
        If includeGrid Then
            If isEnabled Then
                Me.grdGon.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.grdGon.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            End If
        Else
            grdGon.Enabled = False
        End If
    End Sub
    Private Sub setEnabledPOEntry(ByVal isEnabled As Boolean, ByVal inCludeGrid As Boolean)
        Me.txtPORefNo.Enabled = isEnabled
        Me.dtPicPODate.Enabled = isEnabled
        Me.txtSPPBNo.Enabled = isEnabled
        Me.dtPicSPPBDate.Enabled = isEnabled
        Me.txtDefShipto.Enabled = isEnabled
        If inCludeGrid Then
            If isEnabled Then
                Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
            End If
        End If
    End Sub
    Private Function ValidataHeader(ByVal includeGrid As Boolean) As Boolean
        ''check po  number
        If Me.txtPORefNo.Text.Trim().Length <= 4 Then
            Me.baseTooltip.Show("please enter valid PO number", Me.txtPORefNo, 2000)
            Me.txtPORefNo.Focus()
            Return False : End If
        If Me.txtSPPBNo.Text.Trim().Length <= 4 Then
            Me.baseTooltip.Show("please enter valid SPPB number", Me.txtSPPBNo, 2000)
            Me.txtSPPBNo.Focus()
            Return False : End If
        If Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString()) < Convert.ToDateTime(Me.dtPicPODate.Value.ToShortDateString()) Then
            Me.baseTooltip.Show("SPPB date is less then PO Date", Me.dtPicSPPBDate, 2000)
            Me.dtPicSPPBDate.Focus() : Return False : End If
        If Not includeGrid Then : Return True : End If

        If Me.GridEX1.DataSource Is Nothing Then
            Me.baseTooltip.Show("Please enter Item PO Product for GON", Me.GridEX1, 2000)
            If Not Me.IsloadingRow Then
                Me.IsloadingRow = True
            End If
            Me.GridEX1.Focus()
            Me.GridEX1.MoveToNewRecord()
            Me.IsloadingRow = False
            Return False
        ElseIf Me.GridEX1.RecordCount <= 0 Then
            Me.baseTooltip.Show("Please enter Item PO Product for GON", Me.GridEX1, 2000)
            If Not Me.IsloadingRow Then
                Me.IsloadingRow = True
            End If
            Me.GridEX1.Focus()
            Me.GridEX1.MoveToNewRecord()
            Me.IsloadingRow = False
            Return False
            Return False
        Else
            If Not Me.IsloadingRow Then
                Me.IsloadingRow = True
            End If
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetRows()
                Dim IDAppItemOther As Object = row.Cells("ITEM_OTHER").Value
                Dim qty As Object = row.Cells("QUANTITY").Value
                If IsNothing(IDAppItemOther) Or IsDBNull(IDAppItemOther) Then
                    Me.GridEX1.Focus()
                    Me.GridEX1.MoveTo(row)
                    Me.IsloadingRow = False
                    Return False
                End If
                If IsNothing(qty) Or IsDBNull(qty) Then
                    Me.GridEX1.Focus()
                    Me.GridEX1.MoveTo(row)
                    Me.IsloadingRow = False
                    Return False
                End If
                If CDec(qty) <= 0 Then
                    Me.GridEX1.Focus()
                    Me.GridEX1.MoveTo(row)
                    Me.IsloadingRow = False
                    Return False
                End If
            Next
        End If
        Me.IsloadingRow = False
        Return True
    End Function
    Private Function HasChangedHeaderPO() As Boolean
        If Me.txtPORefNo.Text.Trim() <> Me.OSPPBHeader.PONumber Then
            Return True
        End If
        If Not IsNothing(Me.OSPPBHeader.PODate) Then
            If Me.dtPicPODate.Value.ToShortDateString() <> Convert.ToDateTime(Me.OSPPBHeader.PODate).ToShortDateString() Then
                Return True
            End If
        Else
            Return True
        End If
        If Me.txtSPPBNo.Text.Trim() <> Me.OSPPBHeader.SPPBNO Then
            Return True
        End If
        If Me.txtDefShipto.Text <> Me.OSPPBHeader.SPPBShipTo Then
            Return True
        End If
        If Not IsNothing(Me.OSPPBHeader.SPPBDate) Then
            If Me.dtPicSPPBDate.Value.ToShortDateString() <> Convert.ToDateTime(Me.OSPPBHeader.SPPBDate).ToShortDateString() Then
                Return True
            End If
        Else
            Return True
        End If
        Return False
    End Function
    Private Function HasChangedPODetail() As Boolean
        If Not IsNothing(Me.dtGonPODetail) Then
            If Not IsNothing(Me.dtGonPODetail.GetChanges()) Then
                Return Me.dtGonPODetail.GetChanges().Rows.Count > 0
            End If
        End If
        Return False
    End Function
    Private Function HasChangedGONHeader() As Boolean
        If ChkShiptoCustomer.Checked Then
            If Not IsNothing(Me.mcbCustomer.Value) Then
                If Me.mcbCustomer.Value.ToString() <> Me.OGONHeader.DistributorID Then
                    Return True
                End If
            End If
        Else
            If Me.txtCustomerName.Text.Trim() <> Me.OGONHeader.CustomerName Then
                Return True
            End If
            If Me.txtCustomerAddress.Text.Trim() <> Me.OGONHeader.CustomerAddress Then
                Return True
            End If
        End If
        If Me.txtGONNO.Text.Trim() <> Me.OGONHeader.GON_NO Then
            Return True
        End If
        If Not IsNothing(Me.mcbGonArea.Value) Then
            If Me.mcbGonArea.Value.ToString() <> Me.OGONHeader.GON_ID_AREA Then
                Return True
            End If
        End If
        If Not IsNothing(Me.mcbTransporter) Then
            If Me.mcbTransporter.Value <> Me.OGONHeader.GT_ID Then
                Return True
            End If
        End If
        If Me.IsHOUser Then
            If Me.cmdWarhouse.SelectedIndex <> -1 Then
                If Me.cmdWarhouse.SelectedValue.ToString() <> Me.OGONHeader.WarhouseCode Then
                    Return True
                End If
            End If
        End If
        If Me.txtPolice_no_Trans.Text.Trim() <> Me.OGONHeader.PoliceNoTrans Then
            Return True
        End If
        If Me.txtDriverTrans.Text.Trim() <> Me.OGONHeader.DriverTrans Then
            Return True
        End If
        If Me.txtGonShipto.Text <> Me.OGONHeader.ShipTo Then
            Return True
        End If
        Return False
    End Function
    Private Function HasChangeGonDetail() As Boolean
        If Not IsNothing(Me.dtGonDetail) Then
            If Not IsNothing(Me.dtGonDetail.GetChanges()) Then
                Return Me.dtGonDetail.GetChanges().Rows.Count > 0
            End If
        End If
        Return False
    End Function
    Private Function ValidateGONHeader(ByVal includeGrid As Boolean) As Boolean
        If Me.ChkShiptoCustomer.Checked Then
            If Me.mcbCustomer.Value Is Nothing Then
                Me.baseTooltip.Show("Please enter customer", Me.mcbCustomer, 2000)
                Me.mcbCustomer.Focus()
                Return False
            End If
        Else
            If Me.txtCustomerName.Text.Length < 2 Then
                Me.baseTooltip.Show("Please enter customer name", Me.txtCustomerName, 2000)
                Me.txtCustomerName.Focus()
                Return False
            End If
            If Me.txtCustomerAddress.Text.Length <= 4 Then
                Me.baseTooltip.Show("Please enter customer address", Me.txtCustomerAddress, 2000)
                Me.txtCustomerAddress.Focus()
                Return False
            End If
        End If
        If Me.txtGONNO.Text.Length <= 2 Then
            Me.baseTooltip.Show("Please enter Gon Number", Me.txtGONNO, 2000)
            Me.txtGONNO.Focus()
            Return False
        End If
        If Convert.ToDateTime(Me.dtPicGONDate.Value.ToShortDateString()) < Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString()) Then
            Me.baseTooltip.Show("Gon Date is less than SPPB Date" & vbCrLf & "Valid date must be more than SPPB Date", Me.dtPicGONDate, 2000)
            Me.dtPicGONDate.Focus()
            Return False
        End If
        If Me.chkProduct.CheckedItems Is Nothing Then
            Me.baseTooltip.Show("Please check list Item", Me.chkProduct, 2000)
            Me.chkProduct.Focus()
            Return False
        ElseIf Me.chkProduct.CheckedValues.Length < 0 Then
            Me.baseTooltip.Show("Please check list Item", Me.chkProduct, 2000)
            Me.chkProduct.Focus()
            Return False
        End If
        If Me.mcbGonArea.Value Is Nothing Then
            Me.baseTooltip.Show("Please enter GON Area", Me.mcbGonArea, 2000)
            Me.mcbGonArea.Focus()
            Return False
        End If
        If Me.mcbTransporter.Value Is Nothing Then
            Me.baseTooltip.Show("Please enter Transporter", Me.mcbTransporter, 2000)
            Me.mcbTransporter.Focus()
            Return False
        End If
        If Me.IsHOUser Or Me.IsSystemAdmin Then
            If Me.cmdWarhouse.SelectedIndex <= 0 Then
                Me.baseTooltip.Show("Please enter Warhouse", Me.cmdWarhouse, 2000)
                Me.cmdWarhouse.Focus()
                Return False
            End If
        End If
        If includeGrid Then
            If Me.grdGon.DataSource Is Nothing Then
                If Not IsNothing(Me.dtGonDetail) Then
                    If Not IsNothing(Me.dtGonDetail.GetChanges()) Then
                        Return True
                        ''pending delete
                    End If
                End If
                Me.ShowMessageInfo("Please enter gon detail")
                Return False
            End If
            If Me.grdGon.RecordCount <= 0 Then
                If Not IsNothing(Me.dtGonDetail) Then
                    If Not IsNothing(Me.dtGonDetail.GetChanges()) Then
                        Return True
                        ''pending delete
                    End If
                End If
                Me.ShowMessageInfo("Please enter gon detail")
                Return False
            End If
        End If
        Return True
    End Function
    Friend Sub initializedData()
        If Not Me.SForm = SaveMode.Open Then
            If Me.SForm = SaveMode.Update Then
                Me.Mode = SaveMode.Update
                If Me.IsHOUser Then
                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                End If
            ElseIf Me.SForm = SaveMode.Insert Then
                Me.Mode = SaveMode.Insert
                Me.dtPicGONDate.Value = Me.dtPicSPPBDate.Value
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            End If
            If CMain.IsSystemAdministrator Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            End If
        End If

        Me.DVSampleProduct = Me.clsGonNonPO.getSampleProduct(False)
        'Me.dtGonPODetail = Me.clsGonNonPO.getGonPODetail(Me.PO_REF_NO, False)
        Me.DVDistributor = Me.clsGonNonPO.GetDistributor("", False)

        Me.DVTransporter = Me.clsGonNonPO.GonMaster.getTransporter("", Me.SForm, False).DefaultView()
        Me.DVArea = Me.clsGonNonPO.GonMaster.getAreaGon("", Me.SForm, False).DefaultView()
        If Me.SForm = SaveMode.Update Or Me.SForm = SaveMode.Open Then
            Me.DVGonProduct = Me.clsGonNonPO.getPODetail(Me.OSPPBHeader.PONumber, False)
        End If
        'Me.DVMConversiProduct = Me.clsGonNonPO.getProdConvertion(Me.SForm, True)
        'set MCB gudang langsung
        'JKT = 1
        'MRK = 2
        'SBY = 3
        'TGR = 4
        'SRG = 5
        If Not Me.IsHOUser And Not Me.IsSystemAdmin Then
            Select Case ConfigurationManager.AppSettings("WarhouseCode").ToString()
                Case "SRG"
                    Me.cmdWarhouse.SelectedIndex = 5
                Case "SBY"
                    Me.cmdWarhouse.SelectedIndex = 3
                Case "MRK"
                    Me.cmdWarhouse.SelectedIndex = 2
                Case "TGR"
                    Me.cmdWarhouse.SelectedIndex = 4
            End Select
            Me.cmdWarhouse.ReadOnly = True
        End If
    End Sub
    Private Sub FormatDataGrid()
        For Each col As Janus.Windows.GridEX.GridEXColumn In grdGon.RootTable.Columns
            If col.Type Is Type.GetType("System.String") Then
            ElseIf col.Type Is Type.GetType("System.Decimal") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            ElseIf col.Type Is Type.GetType("System.DateTime") Then
                col.EditType = Janus.Windows.GridEX.EditType.CalendarCombo
            End If
            If col.Key.Contains("App") Then
                col.Visible = False
            End If
            If col.Key = "ITEM_OTHER" Or col.Key = "ITEM" Then
                col.Visible = False
            End If
            'If col.Key = "ITEM_OTHER" Then
            '    col.Caption = "SAMPLE PRODUCT"
            'End If
            If col.Key = "CreatedBy" Or col.Key = "CreatedDate" Or col.Key = "ModifiedBy" Or col.Key = "ModifiedDate" Then
                col.Visible = False
            End If
            If col.Key = "QTY_UNIT" Then
                col.Visible = False
            End If
            If col.Key = "QTY" Then
                col.FormatString = "#,##0.0000"
            End If
            If col.Key = "QTY" Or col.Key = "BATCH_NO" Then
                col.EditType = Janus.Windows.GridEX.EditType.TextBox
            Else
                col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
        Next
        Me.grdGon.FilterMode = Janus.Windows.GridEX.FilterMode.None
        grdGon.RootTable.Columns("SAMPLE_PRODUCT").Position = 0
        Me.grdGon.RootTable.Columns("BATCH_NO").Width = 240
        If Not IsNothing(Me.grdGon.DataSource) Then
            If Me.grdGon.RecordCount > 0 Then
                grdGon.AutoSizeColumns()
            End If
        End If

    End Sub
    'Private Sub ReadAccess()
    '    If NufarmBussinesRules.SharedClass.IsUserHO Then
    '        If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Then Then
    '        Else

    '        End If
    'End Sub
    Private Sub ClearGonEntry()
        If Not Me.IsloadingRow Then : Me.IsloadingRow = True : End If
        Me.txtGONNO.Text = ""
        'Me.txtCustomerName.Text = ""
        'Me.txtCustomerAddress.Text = ""
        If Me.IsHOUser Or Me.IsSystemAdmin Then
            Me.cmdWarhouse.Text = ""
            Me.cmdWarhouse.SelectedIndex = -1
        End If
        Me.dtPicGONDate.Value = Me.dtPicSPPBDate.Value
        Me.dtPicGONDate.MinDate = Me.dtPicSPPBDate.Value
        Me.mcbGonArea.Text = ""
        Me.mcbGonArea.Value = Nothing
        Me.mcbTransporter.Text = ""
        Me.mcbTransporter.Value = Nothing
        Me.mcbCustomer.Value = Nothing
        Me.chkProduct.Text = ""
        Me.chkProduct.UncheckAll()
        Me.txtDriverTrans.Text = ""
        Me.txtPolice_no_Trans.Text = ""
        Me.ChkShiptoCustomer.Checked = False
        Me.txtCustomerName.Text = ""
        Me.txtCustomerAddress.Text = ""
        Me.txtGonShipto.Text = ""
        If Not IsNothing(Me.grdGon.DataSource) Then
            CType(Me.grdGon.DataSource, DataTable).RejectChanges()
        End If
        Me.grdGon.SetDataBinding(Nothing, "")
        Me.txtGONNO.Focus()
    End Sub
    Private Sub AddconditionalFormating()
        '     Janus.Windows.GridEX.GridEXFormatStyle _style = new Janus.Windows.GridEX.GridEXFormatStyle();
        '_style.ForeColor = Color.Red;

        'Janus.Windows.GridEX.GridEXFormatCondition _f = new Janus.Windows.GridEX.GridEXFormatCondition();
        '_f.TargetColumn = grdKios.RootTable.Columns["STATUS"];
        '_f.Column = grdKios.RootTable.Columns["STATUS"];
        '_f.FilterCondition = new Janus.Windows.GridEX.GridEXFilterCondition(grdKios.RootTable.Columns["STATUS"], Janus.Windows.GridEX.ConditionOperator.Equal, "Revise");
        '_f.FormatStyle = _style;
        'grdKios.RootTable.FormatConditions.Add(_f);
        Dim FS As New Janus.Windows.GridEX.GridEXFormatStyle()
        With FS
            .ForeColor = Color.Green
        End With
        Dim FC As New Janus.Windows.GridEX.GridEXFormatCondition()
        With FC
            .TargetColumn = Me.GridEX1.RootTable.Columns("STATUS")
            .Column = Me.GridEX1.RootTable.Columns("STATUS")
            .FilterCondition = New Janus.Windows.GridEX.GridEXFilterCondition(Me.GridEX1.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "Completed")
            .FormatStyle = FS
        End With
        Me.GridEX1.RootTable.FormatConditions.Add(FC)

        Dim FS1 As New Janus.Windows.GridEX.GridEXFormatStyle()
        With FS1
            .ForeColor = Color.Maroon
        End With

        Dim FC1 As New Janus.Windows.GridEX.GridEXFormatCondition()
        With FC1
            .TargetColumn = Me.GridEX1.RootTable.Columns("STATUS")
            .Column = Me.GridEX1.RootTable.Columns("STATUS")
            .FilterCondition = New Janus.Windows.GridEX.GridEXFilterCondition(Me.GridEX1.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "Partial")
            .FormatStyle = FS1
        End With
        Me.GridEX1.RootTable.FormatConditions.Add(FC1)
    End Sub

    Private Function SaveData() As Boolean
        Dim validData As Boolean = Me.ValidataHeader(True)
        If Not validData Then : Return False : End If

        Dim HasChangedHeaderPO As Boolean = Me.HasChangedHeaderPO(), HasChangedPODetail As Boolean = Me.HasChangedPODetail(), _
        HasChangedGONHeader As Boolean = Me.HasChangedGONHeader(), HasChangeGonDetail As Boolean = Me.HasChangeGonDetail()
        Dim dummyOSPPBHeader As New NuFarm.Domain.SPPBHeader()
        If HasChangedHeaderPO Then
            With dummyOSPPBHeader 'Me.OSPPBHeader
                .PONumber = Me.txtPORefNo.Text.Trim()
                .PODate = Convert.ToDateTime(Me.dtPicPODate.Value.ToShortDateString())
                .SPPBNO = Me.txtSPPBNo.Text.Trim()
                .SPPBDate = Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString())
                .SPPBShipTo = Me.txtDefShipto.Text.Trim.Replace(vbCrLf, " ")
                If Me.SForm = SaveMode.Update Then
                    .ModifiedBy = NufarmBussinesRules.User.UserLogin.UserName
                Else : SForm = SaveMode.Insert
                    .CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
                End If
            End With
        End If
        Dim UserFrom = ConfigurationManager.AppSettings("WarhouseCode")
        Dim DummyOGonHeader As New NuFarm.Domain.GONHeader()
        If HasChangedGONHeader Then
            If Not IsNothing(Me.grdGon.DataSource) Then
                If Me.grdGon.RecordCount > 0 Then
                    validData = Me.ValidateGONHeader(True)
                    If Not validData Then : Return False : End If
                End If
            End If
            validData = Me.ValidateGONHeader(False)
            If Not validData Then : Return False : End If
            With DummyOGonHeader 'Me.OGONHeader
                .GON_DATE = Convert.ToDateTime(Me.dtPicGONDate.Value.ToShortDateString())
                If Not IsNothing(Me.mcbGonArea.Value) Then
                    .GON_ID_AREA = Me.mcbGonArea.Value
                Else
                    .GON_ID_AREA = ""
                End If
                .GON_NO = Me.txtGONNO.Text.Trim()
                If Not IsNothing(Me.mcbTransporter.Value) Then
                    .GT_ID = Me.mcbTransporter.Value.ToString()
                Else
                    .GT_ID = ""
                End If
                .PoliceNoTrans = Me.txtPolice_no_Trans.Text.Trim()
                .DriverTrans = Me.txtDriverTrans.Text.Trim()
                .DistributorID = IIf((Me.ChkShiptoCustomer.Checked And Not IsNothing(Me.mcbCustomer.Value)), Me.mcbCustomer.Value, "")
                .CustomerName = IIf((Me.ChkShiptoCustomer.Checked = False), Me.txtCustomerName.Text.Trim(), "")
                .CustomerAddress = IIf((Me.ChkShiptoCustomer.Checked = False), Me.txtCustomerAddress.Text.Trim(), "")
                .ShipTo = Me.txtGonShipto.Text.Trim().Replace(vbCrLf, " ")
                If Me.SForm = SaveMode.Update Then
                    .ModifiedBy = NufarmBussinesRules.User.UserLogin.UserName
                Else : SForm = SaveMode.Insert
                    .CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
                End If
                .WarhouseCode = IIf((Me.cmdWarhouse.SelectedIndex <> -1), Me.cmdWarhouse.SelectedValue.ToString(), (IIf(Me.IsHOUser, "JKT", UserFrom)))
            End With
        End If
        If Not Me.IsloadingRow Then : Me.IsloadingRow = True : End If
        If Not Me.IsloadingMCB Then : Me.IsloadingMCB = True : End If
        If Not HasChangedHeaderPO And Not HasChangedGONHeader And Not HasChangedPODetail And Not HasChangeGonDetail Then
            Return False
        End If
        Dim B As Boolean = Me.clsGonNonPO.SaveData(Me.SForm, HasChangedHeaderPO, HasChangedPODetail, HasChangedGONHeader, HasChangeGonDetail, DummyOGonHeader, dummyOSPPBHeader, Me.dtGonPODetail, Me.dtGonDetail)
        If HasChangedHeaderPO Then
            Me.OSPPBHeader = dummyOSPPBHeader
        End If
        If HasChangedGONHeader Then
            Me.OGONHeader = DummyOGonHeader
        End If
        Return B
    End Function
    Private Sub GonNonPODist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.IsloadingMCB = True
            If IsNothing(Me.mcbGonArea.DataSource) Then
                Me.mcbGonArea.SetDataBinding(Me.DVArea, "")
                'Me.mcbGonArea.DisplayMember = "AREA"
                'Me.mcbGonArea.ValueMember = "GON_ID_AREA"
                'Me.mcbGonArea.DropDownList.RetrieveStructure()
                Me.mcbGonArea.DropDownList.Columns("GON_ID_AREA").Caption = "ID_AREA"
                'Me.mcbGonArea.Focus()
                'Me.mcbGonArea.DropDownList.AutoSizeColumns()
            End If
            If IsNothing(Me.mcbCustomer.DataSource) Then
                Me.mcbCustomer.SetDataBinding(Me.DVDistributor, "")
                'Me.mcbCustomer.DisplayMember = "DISTRIBUTOR_NAME"
                'Me.mcbCustomer.ValueMember = "DISTRIBUTOR_ID"
                'Me.mcbCustomer.DropDownList.RetrieveStructure()
                'Me.mcbCustomer.Focus()
                'Me.mcbCustomer.DropDownList().Columns("ADDRESS").Visible = False
                'Me.mcbCustomer.DropDownList().AutoSizeColumns()
            End If
            If IsNothing(Me.mcbTransporter.DataSource) Then
                Me.mcbTransporter.SetDataBinding(DVTransporter, "")
                'Me.mcbTransporter.DisplayMember = "TRANSPORTER_NAME"
                'Me.mcbTransporter.ValueMember = "GT_ID"
                'Me.mcbTransporter.DropDownList.RetrieveStructure()
                'Me.mcbTransporter.Focus()
                'Me.mcbTransporter.DropDownList().AutoSizeColumns()
            End If
            'Me.txtCustomerName.AutoCompleteSource = AutoCompleteSource.CustomSource
            'Me.txtCustomerName.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            'bind gridPOdetail
            Me.btnPrint.Enabled = False
            'Me.btnInsert.Text = "Save Data"
            If Me.SForm = SaveMode.Update Or Me.SForm = SaveMode.Open Then
                Me.txtPORefNo.Text = OSPPBHeader.PONumber
                Me.dtPicPODate.Value = OSPPBHeader.PODate
                Me.txtSPPBNo.Text = OSPPBHeader.SPPBNO
                Me.dtPicSPPBDate.Value = OSPPBHeader.SPPBDate
                Me.txtDefShipto.Text = OSPPBHeader.SPPBShipTo
                Me.BindGridEx1(dtGonPODetail)
                Me.chkProduct.DropDownDataSource = Me.DVGonProduct
                Me.chkProduct.DropDownValueMember = "ITEM_OTHER"
                Me.chkProduct.DropDownDisplayMember = "ITEM"
                Dim checkedVals(dtGonDetail.Rows.Count) As Object
                For i As Integer = 0 To dtGonDetail.Rows.Count - 1
                    checkedVals.SetValue(dtGonDetail.Rows(i)("ITEM_OTHER"), i)
                Next
                ''checklist brandpack mana saja yang ada po detail
                Me.chkProduct.Focus()
                Me.chkProduct.CheckedValues = checkedVals
                'Me.BindGridEx1(dtGonPODetail)
                Me.grdGon.SetDataBinding(Me.dtGonDetail, "")
                If Not Me.HasBoundGridGon Then
                    Me.grdGon.RetrieveStructure()
                    Me.HasBoundGridGon = True
                End If
                FormatDataGrid()
                'bind chkProduct
                'me.dtGonProduct = me.clsGonNonPO.get
                ''set unabled untuk POHeadernya
                If OGONHeader.DistributorID <> "" Then
                    Me.ChkShiptoCustomer.Checked = True
                    Me.mcbCustomer.Visible = True
                    Me.btnFindDistributor.Visible = True
                    Me.txtCustomerName.Visible = False
                    Me.txtCustomerAddress.Visible = False
                    Me.txtGonShipto.Visible = True
                    Me.grpGonEntry.Size = Me.NormalGonEntry
                Else
                    Me.ChkShiptoCustomer.Checked = False
                    Me.mcbCustomer.Visible = False
                    Me.btnFindDistributor.Visible = False
                    Me.lblCustomerName.Visible = True
                    Me.lblAddress.Visible = True
                    Me.txtCustomerName.Visible = True
                    Me.txtCustomerAddress.Visible = True
                    Me.txtGonShipto.Visible = False
                    Me.grpGonEntry.Size = Me.SmallGonEntry
                End If
                With Me.OGONHeader
                    ''set distributor
                    If OGONHeader.DistributorID <> "" Then
                        Me.mcbCustomer.Value = .DistributorID
                    End If
                    Me.txtCustomerName.Text = .CustomerName
                    Me.txtCustomerAddress.Text = .CustomerAddress
                    Me.txtGONNO.Text = .GON_NO
                    If Not String.IsNullOrEmpty(.GON_NO) Then
                        Me.dtPicGONDate.Value = .GON_DATE
                        Me.mcbGonArea.Value = .GON_ID_AREA
                    Else
                        Me.dtPicGONDate.Value = Me.dtPicSPPBDate.Value
                        Me.dtPicGONDate.MinDate = Me.dtPicSPPBDate.Value
                        Me.mcbGonArea.Value = ""
                    End If
                    If Not String.IsNullOrEmpty(.GON_NO) Then
                        Me.cmdWarhouse.SelectedValue = .WarhouseCode
                    End If
                    ''set textnya
                    Me.mcbTransporter.Value = .GT_ID
                    Me.txtPolice_no_Trans.Text = .PoliceNoTrans
                    Me.txtDriverTrans.Text = .DriverTrans
                    Me.txtGonShipto.Text = .ShipTo
                End With
                'Me.setEnabledPOEntry(Not (Me.grdGon.RecordCount > 0), True)
                If Me.SForm = SaveMode.Open Then
                    Me.setEnabledPOEntry(False, True)
                    Me.setEnabledGONEntry(False, True)
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.btnInsert.Enabled = False
                Else
                    Me.setEnabledPOEntry(Not (Me.grdGon.RecordCount > 0), True)
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                End If
                If Not IsNothing(Me.grdGon.DataSource) Then
                    Me.btnPrint.Enabled = Me.grdGon.RecordCount > 0
                Else
                    Me.btnPrint.Enabled = False
                End If
            End If
            If Me.SForm = SaveMode.Insert Or Me.SForm = SaveMode.Update Then
                If IsNothing(NufarmBussinesRules.SharedClass.tblPoliceNumber) Then
                    Dim SettingConfig As New NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting()
                    SettingConfig.filDataExpeditions(True)
                ElseIf NufarmBussinesRules.SharedClass.tblPoliceNumber.Rows.Count <= 0 Then
                    Dim SettingConfig As New NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting()
                    SettingConfig.filDataExpeditions(True)
                End If
                Dim colPolTrans As New AutoCompleteStringCollection()
                For Each row As DataRow In NufarmBussinesRules.SharedClass.tblPoliceNumber.Rows
                    colPolTrans.Add(row("POLICE_NO_TRANS"))
                Next
                Me.txtPolice_no_Trans.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                Me.txtPolice_no_Trans.AutoCompleteSource = AutoCompleteSource.CustomSource
                Me.txtPolice_no_Trans.AutoCompleteCustomSource = colPolTrans

                Dim colDriverTrans As New AutoCompleteStringCollection()
                For Each row As DataRow In NufarmBussinesRules.SharedClass.tblDriverTrans.Rows
                    colDriverTrans.Add(row("DRIVER_TRANS"))
                Next

                Me.txtDriverTrans.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                Me.txtDriverTrans.AutoCompleteSource = AutoCompleteSource.CustomSource
                Me.txtDriverTrans.AutoCompleteCustomSource = colDriverTrans

                AddHandler chkProduct.CheckedValuesChanged, AddressOf CheckStatus
                AddHandler GridEX1.CellUpdated, AddressOf CheckStatusbyGridEx1
                AddHandler grdGon.CellUpdated, AddressOf checkStatusbyGridGon
                'AddHandler grdGon.DeletingRecord, AddressOf CheckStatusByDeleteGridGon
            End If
            Application.DoEvents()
            Me.HasLoadedForm = True
            Me.IsloadingRow = False
            Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsloadingMCB = False
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try

    End Sub
    Private Sub BindGridEx1(ByVal dt As DataTable)
        Me.GridEX1.SetDataBinding(dt, "")
        If Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True Then
            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
        End If
        If Me.GridEX1.DropDowns(0).GetDataRows().Length <= 0 Then
            Me.GridEX1.DropDowns(0).SetDataBinding(Me.DVSampleProduct, "")
            'Me.GridEX1.DropDowns(0).AutoSizeColumns()
        End If
        AddconditionalFormating()
    End Sub
    Private Sub txtPORefNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPORefNo.KeyDown
        If Not txtPORefNo.Enabled Then : Return : End If
        If e.KeyCode = Keys.Enter And (Me.OSPPBHeader.PONumber <> Me.txtPORefNo.Text.Trim()) Then
            Try
                If Me.ValidataHeader(False) Then
                    Me.GridEX1.Focus()
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    If IsNothing(Me.GridEX1.DataSource) Then
                        Me.IsloadingRow = True
                        Me.dtGonPODetail = Me.clsGonNonPO.getGonPODetail(Me.txtPORefNo.Text.Trim(), True)
                        Me.BindGridEx1(dtGonPODetail)
                    End If
                Else
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                End If
                Me.IsloadingRow = False
            Catch ex As Exception
                Me.IsloadingRow = False
                Me.Cursor = Cursors.Default
                Me.ShowMessageError(ex.Message)
            End Try

        End If
    End Sub
    Private Function cekAndSetItemOtherGon(ByVal columnKey As String, ByVal ItemOther As Integer, ByRef SampleProd As String, ByVal FillChkProd As Boolean, ByVal fillGridGon As Boolean) As Boolean
        If columnKey = "QUANTITY" Or columnKey = "ITEM_OTHER" Then
            'check apakah sudah ada GON nya
            If Not IsNothing(Me.grdGon.DataSource) Then
                Dim DummyGOndetail As DataTable = CType(Me.grdGon.DataSource, DataTable).Copy()
                Dim DVDummyGOndetail As DataView = DummyGOndetail.DefaultView()
                DVDummyGOndetail.Sort = "ITEM_OTHER ASC"
                DVDummyGOndetail.RowFilter = "ITEM_OTHER = " & ItemOther
                If Me.SForm = SaveMode.Update Then
                    If DVDummyGOndetail.Count > 0 Then
                        Me.ShowMessageError("can not edit data" & vbCrLf & "GON has already been proceeded")
                        Me.GridEX1.CancelCurrentEdit()
                        Return False
                    End If
                End If
                Dim TotalGOnQty As Decimal = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), ItemOther, True)
                If TotalGOnQty > 0 Then
                    Me.ShowMessageError("can not edit data" & vbCrLf & "GON has already been proceeded")
                    Me.GridEX1.CancelCurrentEdit()
                    Return False
                End If
            End If
            'Dim BrandPackID As Object = Me.GridEX1.GetValue("BRANDPACK_ID")
            Dim DVProDummy As DataView = Me.DVSampleProduct.ToTable.Copy().DefaultView()
            DVProDummy.Sort = "IDApp"
            'Dim BrandPackName As String = ""
            Dim index As Integer = DVProDummy.Find(ItemOther)
            If index <> -1 Then
                SampleProd = DVProDummy(index)("ITEM")
            End If
            Dim QTY As Object = Me.GridEX1.GetValue("QUANTITY")


            If columnKey = "QUANTITY" Then
            ElseIf Not IsNothing(QTY) And Not IsDBNull(QTY) Then
            Else
                Return True
            End If
            If CDec(QTY) <= 0 Then
                Me.ShowMessageInfo("Please enter valid value for Qty")
                Me.GridEX1.CancelCurrentEdit()
                Return False
                'Else
                '    Me.Cursor = Cursors.WaitCursor
                '    ''cek apakah yang di input bisa di bagi kemasan
                '    Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & ItemOther & "' AND INACTIVE = " & False
                '    If Me.DVMConversiProduct.Count > 0 Then
                '        Dim oUOM As Object = DVMConversiProduct(0)("UnitOfMeasure")
                '        Dim oVol1 As Object = DVMConversiProduct(0)("VOL1"), oVol2 As Object = DVMConversiProduct(0)("VOL2")
                '        Dim oUnit1 As Object = DVMConversiProduct(0)("UNIT1"), oUnit2 As Object = DVMConversiProduct(0)("UNIT2")
                '        If oUOM Is Nothing Or oUOM Is DBNull.Value Then
                '            Me.ShowMessageError(SampleProd & ", Unit of Measure has not been set yet")
                '            Me.GridEX1.CancelCurrentEdit()
                '            Return False
                '        ElseIf oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                '            Me.ShowMessageError(SampleProd & ", colly for Volume 1 has not been set yet")
                '            Me.GridEX1.CancelCurrentEdit()
                '            Return False
                '        ElseIf oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                '            Me.ShowMessageError(SampleProd & ", colly for Volume 2 has not been set yet")
                '            Me.GridEX1.CancelCurrentEdit()
                '            Return False
                '        ElseIf oUnit1 Is Nothing Or oUnit2 Is DBNull.Value Then
                '            Me.ShowMessageError(SampleProd & ", colly for Unit 1 has not been set yet")
                '            Me.GridEX1.CancelCurrentEdit()
                '            Return False
                '        ElseIf oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                '            Me.ShowMessageError(SampleProd & ", colly for Unit 2 has not been set yet")
                '            Me.GridEX1.CancelCurrentEdit()
                '            Return False
                '        End If
                '        Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                '        Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                '        If QTY >= Dvol1 Then
                '        ElseIf QTY > 0 Then
                '            Dim ilqty As Decimal = (QTY / Dvol1) * DVol2
                '            If CInt(ilqty) < 1 Then
                '                Me.ShowMessageError("please enter valid quantity")
                '                Me.GridEX1.CancelCurrentEdit()
                '                Return False
                '            End If
                '        Else
                '            Me.ShowMessageError("please enter valid quantity")
                '            Me.GridEX1.CancelCurrentEdit()
                '            Return False
                '        End If
                '    Else
                '        Me.ShowMessageError("Convertion for " & SampleProd & " could not be found in database")
                '        Me.GridEX1.CancelCurrentEdit()
                '        Return False
                '    End If
            End If
            Me.Cursor = Cursors.WaitCursor
            'check validity QTY
            Me.DVSampleProduct.RowFilter = "IDApp = " & ItemOther
            Dim DevQty As Decimal = DVSampleProduct(0)("DEVIDED_QUANTITY")
            Dim ResultModQty As Decimal = QTY Mod Devqty
            If ResultModQty > 0 Then
                Me.ShowMessageInfo(String.Format("can not enter value {0}" & vbCrLf & "VALUE is not even with packaging" & vbCrLf & "excess/lack value = {1}", ResultModQty, QTY Mod DevQty))
                Me.GridEX1.CancelCurrentEdit()
                Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return False
            End If
            'update LEFT_QTY

            Me.IsloadingMCB = True
            Me.IsloadingRow = True
            If Not IsNothing(Me.chkProduct.DropDownDataSource) Then
                If Me.chkProduct.DropDownList.RecordCount > 0 Then
                    If FillChkProd Then
                        Dim checkedVals() As Object = Me.chkProduct.CheckedValues()
                        Dim dtdummyChkProd As DataTable = CType(Me.chkProduct.DropDownDataSource, DataView).ToTable().Copy()
                        Dim dvdummyChkProd As DataView = dtdummyChkProd.DefaultView
                        dvdummyChkProd.Sort = "ITEM_OTHER ASC"
                        index = dvdummyChkProd.Find(ItemOther)
                        If index <> -1 Then
                            dvdummyChkProd(index)("LEFT_QTY") = QTY
                            dvdummyChkProd(index).EndEdit()
                        End If
                        Me.chkProduct.SetDataBinding(dvdummyChkProd, "")
                        Application.DoEvents()
                        Me.chkProduct.CheckedValues = checkedVals
                    End If
                End If
            End If
            Me.GridEX1.UpdateData()
            If Not IsNothing(Me.grdGon.DataSource) Then
                If Me.grdGon.RecordCount > 0 Then
                    If fillGridGon Then
                        Dim dtDummyGondetail As DataTable = CType(Me.grdGon.DataSource, DataTable)
                        Dim foundRows() As DataRow = dtDummyGondetail.Select("ITEM_OTHER = " & ItemOther)
                        If foundRows.Length > 0 Then
                            Dim info As New CultureInfo("id-ID")
                            foundRows(0).BeginEdit()
                            'Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & ItemOther & "' AND INACTIVE = " & False
                            'If DVMConversiProduct.Count <= 0 Then
                            '    Me.ShowMessageError("Convertion for " & SampleProd & " could not be found in database")
                            '    Me.GridEX1.CancelCurrentEdit()
                            '    Return False
                            'End If
                            Dim oUOM As Object = DVSampleProduct(0)("UnitOfMeasure")
                            Dim oVol1 As Object = DVSampleProduct(0)("VOL1"), oVol2 As Object = DVSampleProduct(0)("VOL2")
                            Dim oUnit1 As Object = DVSampleProduct(0)("UNIT1"), oUnit2 As Object = DVSampleProduct(0)("UNIT2")
                            Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                            Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                            Dim col1 As Integer = 0
                            Dim collyBox As String = "", collyPackSize As String = ""
                            'Dim newRow As DataRow = CType(Me.grdGon.DataSource, DataTable).NewRow()
                            If QTY >= Dvol1 Then
                                col1 = Convert.ToInt32(Decimal.Truncate(QTY / Dvol1))
                                collyBox = IIf(col1 <= 0, "", String.Format("{0:g} {1}", col1, strUnit1))
                                Dim lqty As Decimal = QTY Mod Dvol1
                                Dim ilqty As Integer = 0
                                If lqty >= 1 Then
                                    'Dim c As Decimal = Decimal.Remainder(GonQty, Dvol1)
                                    ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                                ElseIf lqty > 0 And lqty < 1 Then
                                    ilqty = ilqty + DVol2
                                End If
                                collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                            ElseIf QTY > 0 Then ''gon kurang dari 1 coly
                                Dim ilqty As Integer = Convert.ToInt32((QTY / Dvol1) * DVol2)
                                collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                            End If
                            foundRows(0)("QTY") = QTY
                            foundRows(0)("QTY_UNIT") = String.Format(info, "{0:#,##0.000} {1}", QTY, oUOM.ToString())
                            foundRows(0)("COLLY_BOX") = collyBox
                            foundRows(0)("COLLY_PACKSIZE") = collyPackSize
                            'newRow("BATCH_NO") = 
                            If Me.SForm = SaveMode.Update Then
                                foundRows(0)("ModifiedBy") = NufarmBussinesRules.User.UserLogin.UserName
                                foundRows(0)("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
                            ElseIf Me.SForm = SaveMode.Insert Then
                                foundRows(0)("CreatedBy") = NufarmBussinesRules.User.UserLogin.UserName
                                foundRows(0)("CreatedDate") = NufarmBussinesRules.SharedClass.ServerDate
                            End If
                            foundRows(0).EndEdit()
                            Me.grdGon.Refetch()
                            Me.mustCheckStatus = True
                        End If
                    End If
                End If
            End If
        End If
        Me.DVSampleProduct.RowFilter = ""
        Me.IsloadingMCB = False
        Me.IsloadingRow = False
        Return True
    End Function
    Private Sub GridEX1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        If Me.IsloadingRow Then : Return : End If
        If Not Me.HasLoadedForm Then : Return : End If
        Try
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "ITEM_OTHER" Or e.Column.Key = "QUANTITY" Then
                    Me.Cursor = Cursors.WaitCursor
                    Dim valData As Boolean = True
                    If IsNothing(Me.GridEX1.GetValue("ITEM_OTHER")) Or IsDBNull(Me.GridEX1.GetValue("ITEM_OTHER")) Then
                        valData = False
                    End If
                    If IsNothing(Me.GridEX1.GetValue("QUANTITY")) Or IsDBNull(Me.GridEX1.GetValue("QUANTITY")) Then
                        valData = False
                    End If
                    Dim SampleProduct As String = ""
                    If Me.cekAndSetItemOtherGon(e.Column.Key, Me.GridEX1.GetValue("ITEM_OTHER"), SampleProduct, True, True) Then
                        If valData Then
                            Me.IsloadingMCB = True
                            Dim CheckedVals() As Object = Nothing
                            If Not IsNothing(Me.chkProduct.DropDownDataSource) Then
                                If Me.chkProduct.DropDownList.RecordCount > 0 Then
                                    CheckedVals = Me.chkProduct.CheckedValues()
                                End If
                            Else
                                Me.DVGonProduct = Me.clsGonNonPO.getPODetail(Me.txtPORefNo.Text.Trim(), True)
                                Me.chkProduct.DropDownDataSource = Me.DVGonProduct
                                Me.chkProduct.DropDownDisplayMember = "ITEM"
                                Me.chkProduct.DropDownValueMember = "ITEM_OTHER"
                            End If
                            Dim DVChkProd As DataView = CType(Me.chkProduct.DropDownDataSource, DataView)
                            Dim sort As String = DVChkProd.Sort
                            DVChkProd.Sort = "ITEM_OTHER"
                            Dim IDAppItemOther As Integer = Me.GridEX1.GetValue("ITEM_OTHER")
                            Dim LeftQty As Decimal = 0
                            Dim index As Integer = DVChkProd.Find(IDAppItemOther)
                            Dim drv As DataRowView = Nothing
                            If index <> -1 Then
                                drv = DVChkProd(index)
                                drv.BeginEdit()
                                ''check left QTY ke database
                                LeftQty = Me.clsGonNonPO.getLeftQty(Me.txtPORefNo.Text.Trim(), IDAppItemOther, True)
                                drv = DVChkProd(index)
                                drv("LEFT_QTY") = IIf(LeftQty > 0, LeftQty, Me.GridEX1.GetValue("QUANTITY"))
                            Else
                                drv = DVChkProd.AddNew()
                                drv("ITEM_OTHER") = IDAppItemOther
                                drv("ITEM") = SampleProduct
                                drv("LEFT_QTY") = Me.GridEX1.GetValue("QUANTITY")
                            End If
                            drv.EndEdit()
                            DVChkProd.Sort = sort
                            Me.chkProduct.DropDownList.Refetch()
                            Me.chkProduct.CheckedValues = CheckedVals
                            Me.GridEX1.UpdateData()
                            Me.IsloadingMCB = False
                        End If
                        If Me.Mode = SaveMode.Update Then
                            If IsDBNull(Me.GridEX1.GetValue("CreatedBy")) Then : Me.GridEX1.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName) : End If
                            If IsDBNull(Me.GridEX1.GetValue("ModifiedBy")) Then : Me.GridEX1.SetValue("ModifiedBy", NufarmBussinesRules.User.UserLogin.UserName) : End If
                        Else
                            If IsDBNull(Me.GridEX1.GetValue("CreatedBy")) Then : Me.GridEX1.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName) : End If
                        End If
                    End If
                End If
            End If

            Me.IsloadingRow = False
            Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.mustCheckStatus = False
            Me.GridEX1.CancelCurrentEdit()
            Me.IsloadingMCB = False
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try

    End Sub

    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        Try
            Dim IDAppItemOther As Object = Me.GridEX1.GetValue("ITEM_OTHER")
            Dim Item As String = Me.GridEX1.DropDowns(0).GetValue("ITEM")
            Dim QTY As Object = Me.GridEX1.GetValue("QUANTITY")
            If IsNothing(IDAppItemOther) Or IsDBNull(IDAppItemOther) Then
                Me.ShowMessageInfo("Please enter valid Value for Item")
                e.Cancel = True
                Return
            End If
            If IsNothing(QTY) Or IsDBNull(QTY) Then
                Me.ShowMessageInfo("Please enter valid value for Qty")
                e.Cancel = True
                Return
            End If
            'check existing
            If Me.dtGonPODetail.Select("ITEM_OTHER = " & IDAppItemOther).Length > 0 Then
                Me.ShowMessageError("Item has existed")
                e.Cancel = True
                Return
            End If
            If CDec(QTY) <= 0 Then
                Me.ShowMessageInfo("Please enter valid value for Qty")
                e.Cancel = True
                Return
            Else
                Me.Cursor = Cursors.WaitCursor
                ''cek apakah yang di input bisa di bagi kemasan
                Me.DVSampleProduct.RowFilter = "IDApp = " & IDAppItemOther
                If Me.DVSampleProduct.Count > 0 Then
                    Dim oUOM As Object = DVSampleProduct(0)("UnitOfMeasure")
                    Dim oVol1 As Object = DVSampleProduct(0)("VOL1"), oVol2 As Object = DVSampleProduct(0)("VOL2")
                    Dim oUnit1 As Object = DVSampleProduct(0)("UNIT1"), oUnit2 As Object = DVSampleProduct(0)("UNIT2")
                    Dim oDevQty As Object = DVSampleProduct(0)("DEVIDED_QUANTITY")
                    If oUOM Is Nothing Or oUOM Is DBNull.Value Then
                        Me.ShowMessageError(Item & ", UnitOfMeasure(UOM) has not been set yet")
                        e.Cancel = True
                    ElseIf oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                        Me.ShowMessageError(Item & ", colly for Volume 1 has not been set yet")
                        e.Cancel = True
                    ElseIf oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                        Me.ShowMessageError(Item & ", colly for Volume 2 has not been set yet")
                        e.Cancel = True
                    ElseIf oUnit1 Is Nothing Or oUnit2 Is DBNull.Value Then
                        Me.ShowMessageError(Item & ", colly for Unit 1 has not been set yet")
                        e.Cancel = True
                    ElseIf oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                        Me.ShowMessageError(Item & ", colly for Unit 2 has not been set yet")
                        e.Cancel = True
                    End If
                    Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                    Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)

                    If QTY Mod oDevQty > 0 Then
                        Me.ShowMessageInfo(String.Format("can not enter value {0}" & vbCrLf & "VALUE is not even with packaging" & vbCrLf & "excess/lack value = {1}", QTY, QTY Mod oDevQty))
                        Me.GridEX1.CancelCurrentEdit()
                        Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                    End If
                    If QTY >= Dvol1 Then
                    ElseIf QTY > 0 Then
                        Dim ilqty As Decimal = (QTY / Dvol1) * DVol2
                        If CInt(ilqty) < 1 Then
                            Me.ShowMessageError("please enter valid quantity")
                            e.Cancel = True
                        End If
                    Else
                        Me.ShowMessageError("please enter valid quantity")
                        e.Cancel = True
                    End If
                Else
                    Me.ShowMessageError("Convertion for " & Item & " could not be found in database")
                    e.Cancel = True
                End If
                'If GonQty >= Dvol1 Then
                '    col1 = Convert.ToInt32(Decimal.Truncate(GonQty / Dvol1))
                '    collyBox = String.Format("{0:g} BOX", col1)
                '    Dim lqty As Decimal = GonQty Mod Dvol1
                '    Dim ilqty As Integer = 0
                '    If lqty >= 1 Then
                '        'Dim c As Decimal = Decimal.Remainder(GonQty, Dvol1)
                '        ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                '    ElseIf lqty > 0 And lqty < 1 Then
                '        ilqty = ilqty + DVol2
                '    End If
                '    collyPackSize = String.Format("{0:g} " & strUnit2, ilqty)
                'ElseIf GonQty > 0 Then ''gon kurang dari 1 coly
                '    Dim ilqty As Integer = Convert.ToInt32((GonQty / Dvol1) * DVol2)
                '    collyPackSize = String.Format("{0:g} " & strUnit2, ilqty)
                'End If
                Me.GridEX1.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName)
                Me.GridEX1.SetValue("STATUS", "Pending")
                Me.Cursor = Cursors.Default
            End If
            Me.DVSampleProduct.RowFilter = ""
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            e.Cancel = True
            Me.ShowMessageError(ex.Message)

        End Try
    End Sub

    Private Sub GridEX1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Try
            If Me.SForm = SaveMode.Update Or Me.SForm = SaveMode.Update Then
                Return
            End If
            If Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.frmLookUp = New LookUp()
            Dim dvDummyProd As DataView = Me.DVSampleProduct.ToTable().Copy().DefaultView
            dvDummyProd.Sort = "IDApp"
            Dim ItemOther As Integer = 0
            If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                ItemOther = Me.GridEX1.GetValue("ITEM_OTHER")
                Dim TotalGOnQty As Decimal = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), ItemOther, True)
                If TotalGOnQty > 0 Then
                    Me.ShowMessageError("can not edit data" & vbCrLf & "GON has already been proceeded")
                    Me.Cursor = Cursors.Default
                    Return
                End If
            End If
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetRows
                ItemOther = row.Cells("ITEM_OTHER").Value
                Dim Index As Integer = dvDummyProd.Find(ItemOther)
                If Index <> -1 Then
                    dvDummyProd.Delete(Index)
                End If
            Next
            Me.checkedVals = New List(Of Integer)
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                With frmLookUp
                    .watermarkText = "Enter the product to search for"
                    .Grid.SetDataBinding(dvDummyProd, "")
                    .Grid.RetrieveStructure()
                    .Grid.RootTable.Columns("VOL1").FormatString = "#,##0.0000"
                    .Grid.RootTable.Columns("VOL2").FormatString = "#,##0.0000"
                    .Grid.RootTable.Columns("ITEM").Caption = "SAMPLE_PRODUCT"
                    .Grid.RootTable.Columns("IDApp").Visible = False
                    .Grid.RootTable.Columns("UnitOfMeasure").Caption = "U O M"
                    Application.DoEvents()
                    .Grid.RootTable.Columns(1).ShowRowSelector = False
                    .Grid.RootTable.Columns(1).UseHeaderSelector = False
                    .Grid.AutoSizeColumns()
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog(Me)
                End With
            ElseIf Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                With frmLookUp
                    .watermarkText = "Enter the product to search for"
                    .Grid.SetDataBinding(dvDummyProd, "")
                    .Grid.RetrieveStructure()
                    .Grid.RootTable.Columns("VOL1").FormatString = "#,##0.0000"
                    .Grid.RootTable.Columns("VOL2").FormatString = "#,##0.0000"
                    .Grid.RootTable.Columns("ITEM").Caption = "SAMPLE_PRODUCT"
                    .Grid.RootTable.Columns("IDApp").Visible = False
                    .Grid.RootTable.Columns("UnitOfMeasure").Caption = "U O M"
                    Application.DoEvents()
                    .Grid.RootTable.Columns(1).ShowRowSelector = True
                    .Grid.RootTable.Columns(1).UseHeaderSelector = False
                    .Grid.AutoSizeColumns()
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog(Me)
                End With
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub frmLookUp_GridDoubleClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmLookUp.GridDoubleClicked
        Try
            If Not Me.frmLookUp.Grid.RootTable.Columns(0).UseHeaderSelector Then
                If frmLookUp.Grid.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                    Dim IDAppItemOther As Integer = frmLookUp.Grid.GetValue("IDApp"), Item As String = ""
                    Dim valData As Boolean = True
                    If IsNothing(Me.GridEX1.GetValue("ITEM_OTHER")) Or IsDBNull(Me.GridEX1.GetValue("ITEM_OTHER")) Then
                        valData = False
                    End If
                    If IsNothing(Me.GridEX1.GetValue("QUANTITY")) Or IsDBNull(Me.GridEX1.GetValue("QUANTITY")) Then
                        valData = False
                    End If
                    Me.GridEX1.SetValue("ITEM_OTHER", IDAppItemOther)
                    Me.cekAndSetItemOtherGon("ITEM_OTHER", IDAppItemOther, Item, False, True)
                    If valData Then
                        Me.IsloadingMCB = True
                        Dim CheckedVals() As Object = Nothing
                        If Not IsNothing(Me.chkProduct.DropDownDataSource) Then
                            If Me.chkProduct.DropDownList.RecordCount > 0 Then
                                CheckedVals = Me.chkProduct.CheckedValues()
                            End If
                        Else
                            Me.DVGonProduct = Me.clsGonNonPO.getPODetail(Me.txtPORefNo.Text.Trim(), True)
                            Me.chkProduct.DropDownDataSource = Me.DVGonProduct
                            Me.chkProduct.DropDownDisplayMember = "ITEM"
                            Me.chkProduct.DropDownValueMember = "ITEM_OTHER"
                        End If
                        Dim DVChkProd As DataView = CType(Me.chkProduct.DropDownDataSource, DataView)
                        Dim sort As String = DVChkProd.Sort
                        DVChkProd.Sort = "ITEM_OTHER"
                        IDAppItemOther = Me.GridEX1.GetValue("ITEM_OTHER")
                        Dim LeftQty As Decimal = 0
                        Dim index As Integer = DVChkProd.Find(IDAppItemOther)
                        Dim drv As DataRowView = Nothing
                        If index <> -1 Then
                            ''check left QTY ke database
                            LeftQty = Me.clsGonNonPO.getLeftQty(Me.txtPORefNo.Text.Trim(), IDAppItemOther, True)
                            drv = DVChkProd(index)
                            drv.BeginEdit()
                            drv("LEFT_QTY") = IIf(LeftQty > 0, LeftQty, Me.GridEX1.GetValue("QUANTITY"))
                        Else
                            drv = DVChkProd.AddNew()
                            drv("ITEM_OTHER") = IDAppItemOther
                            drv("ITEM") = Item
                            drv("LEFT_QTY") = Me.GridEX1.GetValue("QUANTITY")
                        End If
                        drv.EndEdit()
                        DVChkProd.Sort = sort
                        Me.chkProduct.DropDownList.Refetch()
                        Me.chkProduct.CheckedValues = CheckedVals
                        Me.GridEX1.UpdateData()
                        Me.IsloadingMCB = False
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub FrmLookup_Search(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles frmLookUp.SearchKeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                Me.Cursor = Cursors.WaitCursor
                frmLookUp.Cursor = Cursors.WaitCursor
                frmLookUp.IsloadingRow = True
                CType(frmLookUp.Grid.DataSource, DataView).RowFilter = "ITEM like '%" & frmLookUp.txtSearch.Text.Trim() & "%'"
                For Each row As Janus.Windows.GridEX.GridEXRow In frmLookUp.Grid.GetRows
                    If Me.checkedVals.Contains(row.Cells("IDApp").Value) Then
                        row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked
                    End If
                Next
                frmLookUp.IsloadingRow = False
                Me.Cursor = Cursors.Default
                frmLookUp.Cursor = Cursors.Default
            Catch ex As Exception
                frmLookUp.IsloadingRow = False
                Me.Cursor = Cursors.Default
                frmLookUp.Cursor = Cursors.Default
                frmLookUp.IsloadingRow = False
            End Try

        End If
    End Sub
    Private Sub frmLookUp_GridRowCheckedChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles frmLookUp.GridRowCheckStateChanged
        Try
            If e.Row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                If Not Me.checkedVals.Contains(e.Row.Cells("IDApp").Value) Then
                    Me.checkedVals.Add(e.Row.Cells("IDApp").Value)
                End If
            ElseIf e.Row.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked Then
                If Me.checkedVals.Contains(e.Row.Cells("IDApp").Value) Then
                    Me.checkedVals.Remove(e.Row.Cells("IDApp").Value.ToString())
                End If
            End If
        Catch ex As Exception
            For Each row As Janus.Windows.GridEX.GridEXRow In frmLookUp.Grid.GetCheckedRows()
                If row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                    If Not Me.checkedVals.Contains(row.Cells("IDApp").Value) Then
                        Me.checkedVals.Add(row.Cells("IDApp").Value)
                    End If
                ElseIf row.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked Then
                    If Me.checkedVals.Contains(row.Cells("IDApp").Value) Then
                        Me.checkedVals.Remove(row.Cells("IDApp").Value)
                    End If
                End If
            Next
        End Try

    End Sub
    'Private Sub fmrLookUp_ColHeaderClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles frmLookUp.ColHeaderClicked
    '    'if e.Column.Key =
    'End Sub
    Private Sub frmLookup_OKClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmLookUp.OkClick

        If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then
            Me.frmLookUp.IsloadingRow = True
            Me.Cursor = Cursors.WaitCursor
            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Dim DT As DataTable = CType(Me.GridEX1.DataSource, DataTable)
            If checkedVals.Count <= 0 Then
                Me.frmLookUp.Close()
                Me.frmLookUp.Dispose()
                Me.frmLookUp = Nothing
                Me.frmLookUp.IsloadingRow = False
                Me.Cursor = Cursors.Default
                Return
            End If
            Dim IDAAppItemOther As Integer = -1
            For Each item As String In Me.checkedVals
                IDAAppItemOther = item
                With DT
                    Dim dr As DataRow = DT.NewRow()
                    dr.BeginEdit()
                    dr("ITEM_OTHER") = IDAAppItemOther
                    dr.EndEdit()
                    DT.Rows.Add(dr)
                End With
            Next
            Me.GridEX1.Update()
            Me.GridEX1.Refetch()
            Application.DoEvents()
            'Me.BindGridEx1(DT)
        Else
            If frmLookUp.Grid.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim IDAAppItemOther As Integer = frmLookUp.Grid.GetValue("IDApp"), Item As String = ""
                Dim valData As Boolean = True
                If IsNothing(Me.GridEX1.GetValue("ITEM_OTHER")) Or IsDBNull(Me.GridEX1.GetValue("ITEM_OTHER")) Then
                    valData = False
                End If
                If IsNothing(Me.GridEX1.GetValue("QUANTITY")) Or IsDBNull(Me.GridEX1.GetValue("QUANTITY")) Then
                    valData = False
                End If
                Me.GridEX1.SetValue("ITEM_OTHER", IDAAppItemOther)
                Me.cekAndSetItemOtherGon("ITEM_OTHER", IDAAppItemOther, Item, False, True)
                'If IsNothing(Me.GridEX1.GetValue("QUANTITY")) Or IsDBNull(Me.GridEX1.GetValue("QUANTITY")) Then ''jika mengganti brandpack_id saja
                'End If
                If valData Then
                    Me.IsloadingMCB = True
                    Dim CheckedVals() As Object = Nothing
                    If Not IsNothing(Me.chkProduct.DropDownDataSource) Then
                        If Me.chkProduct.DropDownList.RecordCount > 0 Then
                            CheckedVals = Me.chkProduct.CheckedValues()
                        End If
                    Else
                        Me.DVGonProduct = Me.clsGonNonPO.getPODetail(Me.txtPORefNo.Text.Trim(), True)
                        Me.chkProduct.DropDownDataSource = Me.DVGonProduct
                        Me.chkProduct.DropDownDisplayMember = "ITEM"
                        Me.chkProduct.DropDownValueMember = "ITEM_OTHER"
                    End If
                    Dim DVChkProd As DataView = CType(Me.chkProduct.DropDownDataSource, DataView)
                    Dim sort As String = DVChkProd.Sort
                    DVChkProd.Sort = "ITEM_OTHER"
                    IDAAppItemOther = Me.GridEX1.GetValue("ITEM_OTHER")
                    Dim LeftQty As Decimal = 0
                    Dim index As Integer = DVChkProd.Find(IDAAppItemOther)
                    Dim drv As DataRowView = Nothing
                    If index <> -1 Then
                        ''check left QTY ke database
                        LeftQty = Me.clsGonNonPO.getLeftQty(Me.txtPORefNo.Text.Trim(), IDAAppItemOther, True)
                        drv = DVChkProd(index)
                        drv.BeginEdit()
                        drv("LEFT_QTY") = IIf(LeftQty > 0, LeftQty, Me.GridEX1.GetValue("QUANTITY"))
                    Else
                        drv = DVChkProd.AddNew()
                        drv("ITEM_OTHER") = IDAAppItemOther
                        drv("ITEM") = Item
                        drv("LEFT_QTY") = Me.GridEX1.GetValue("QUANTITY")
                    End If
                    drv.EndEdit()
                    DVChkProd.Sort = sort
                    Me.chkProduct.DropDownList.Refetch()
                    Me.chkProduct.CheckedValues = CheckedVals
                    Me.GridEX1.UpdateData()
                    Me.IsloadingMCB = False
                End If
            End If
        End If
        frmLookUp.IsloadingRow = False
        Me.IsloadingRow = False
        Me.frmLookUp.Close()
        Me.frmLookUp.Dispose()
        Me.frmLookUp = Nothing
        Me.checkedVals.Clear()
        Me.checkedVals = Nothing
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub txtSPPBNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSPPBNo.TextChanged

    End Sub

    Private Sub GridEX1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.Enter
        If Not Me.HasLoadedForm Then : Return : End If
        If Me.IsloadingRow Then : Return : End If
        If Not Me.ValidataHeader(False) Then
            Me.IsloadingRow = False
            Return
        End If
        Try
            If Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True Then
                Me.GridEX1.Focus()
                If IsNothing(Me.GridEX1.DataSource) Then
                    Me.IsloadingRow = True
                    Me.dtGonPODetail = Me.clsGonNonPO.getGonPODetail(Me.txtPORefNo.Text.Trim(), True)
                    Me.BindGridEx1(dtGonPODetail)
                End If
            Else
                Me.GridEX1.Cursor = Cursors.No
            End If
            Me.IsloadingRow = False
        Catch ex As Exception
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub

    Private Sub btnFindGonArea_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindGonArea.btnClick
        If Me.SForm = SaveMode.None Or Me.SForm = SaveMode.Open Then
            Return
        End If
        If Not Me.ValidataHeader(True) Then
            Me.IsloadingRow = False
            Return
        End If
        'If Not Me.ValidateGONHeader(False) Then
        '    Me.IsloadingRow = False
        '    Return
        'End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.DVArea = Me.clsGonNonPO.GonMaster.getAreaGon(Me.mcbGonArea.Text, Me.SForm, True).DefaultView()
            Me.mcbGonArea.SetDataBinding(Me.DVArea, "")
            Me.ShowMessageInfo(Me.DVArea.Count.ToString() & " data(s) found")
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            'Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BtnFindTransporter_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFindTransporter.btnClick
        If Me.SForm = SaveMode.None Or Me.SForm = SaveMode.Open Then
            Return
        End If
        If Not Me.ValidataHeader(True) Then
            Me.IsloadingRow = False
            Return
        End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dtTrans As DataTable = Me.clsGonNonPO.GonMaster.getTransporter(Me.mcbTransporter.Text.Trim(), Me.SForm, True)
            Me.DVTransporter = dtTrans.DefaultView
            Me.mcbTransporter.SetDataBinding(DVTransporter, "")
            Me.ShowMessageInfo(Me.DVTransporter.Count.ToString() & " data(s) found")
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            'Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    ''' <summary>
    ''' Delegate untuk cell update grin gon
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub checkStatusbyGridGon(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)

        If Not mustCheckStatus Then : Return : End If

        Dim mustUpdatedData As Boolean = False
        Dim IDAppItemOther As Integer = Me.grdGon.GetValue("ITEM_OTHER")
        For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetRows
            If row.Cells("ITEM_OTHER").Value = IDAppItemOther Then
                Dim IDApp As Object = Me.grdGon.GetValue("IDApp")
                Dim POOriginal As Decimal = row.Cells("QUANTITY").Value
                Dim totalGonOut As Decimal = 0, GonQtyIN As Decimal = Me.grdGon.GetValue("QTY")
                totalGonOut = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), IDAppItemOther, IIf((IsDBNull(IDApp) Or IsNothing(IDApp)), 0, IDApp), False)
                row.BeginEdit()
                If POOriginal - (totalGonOut + GonQtyIN) <= 0 Then
                    row.Cells("STATUS").Value = "Completed"
                    mustUpdatedData = True
                ElseIf (totalGonOut + GonQtyIN) > 0 And (totalGonOut + GonQtyIN) < POOriginal Then
                    row.Cells("STATUS").Value = "Partial"
                    mustUpdatedData = True
                Else
                    row.Cells("STATUS").Value = "Pending"
                    mustUpdatedData = True
                End If
                row.EndEdit()
            End If
        Next
        If mustUpdatedData Then
            Me.GridEX1.UpdateData()
        End If
        Me.mustCheckStatus = False
    End Sub
    ''' <summary>
    ''' Delegate untuk gridex1 cell updated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CheckStatusbyGridEx1(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)


        If Not mustCheckStatus Then : Return : End If
        If IsNothing(Me.grdGon.DataSource) OrElse Me.grdGon.RecordCount <= 0 Then
            Return
        End If
        Dim mustUpdatedData As Boolean = False
        Dim IDAppItemOther As Integer = Me.GridEX1.GetValue("ITEM_OTHER")
        Dim IDApp As Object = Me.GridEX1.GetValue("IDApp")
        Dim POOriginal As Decimal = Me.GridEX1.GetValue("QUANTITY")
        Dim totalGonOut As Decimal = 0, GonQtyIN As Decimal
        totalGonOut = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), IDAppItemOther, False)

        If e.Column.Key = "QUANTITY" Then
            Dim dummyT As DataTable = CType(Me.grdGon.DataSource, DataTable).Copy()
            Dim gonRows() As DataRow = dummyT.Select("ITEM = " & IDAppItemOther)
            If gonRows.Length > 0 Then
                GonQtyIN = CDec(gonRows(0)("QTY"))
            End If
            If POOriginal - (totalGonOut + GonQtyIN) <= 0 Then
                Me.GridEX1.SetValue("STATUS", "Completed")
                mustUpdatedData = True
            ElseIf (totalGonOut + GonQtyIN) > 0 And (totalGonOut + GonQtyIN) < POOriginal Then
                Me.GridEX1.SetValue("STATUS", "Partial")
                mustUpdatedData = True
            Else
                Me.GridEX1.SetValue("STATUS", "Pending")
                mustUpdatedData = True
            End If
        End If
        If mustUpdatedData Then
            Me.GridEX1.UpdateData()
        End If
        Me.mustCheckStatus = False
    End Sub
    ''' <summary>
    ''' Delegate untuk chkProduck chckedChanged event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CheckStatus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not mustCheckStatus Then : Return : End If
        Dim tblGons As DataTable = Me.clsGonNonPO.getTotalGons(Me.txtPORefNo.Text.Trim(), False, Me.txtGONNO.Text.Trim())
        Dim mustUpdatedData As Boolean = False
        For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetRows
            Dim IDAppItemOther As String = row.Cells("ITEM_OTHER").Value
            Dim OPOOriginal As Object = row.Cells("QUANTITY").Value, POOriginal As Decimal = 0
            Dim totalGonOut As Decimal = 0, GonQtyIN As Decimal = 0
            If Not IsNothing(OPOOriginal) And Not IsDBNull(OPOOriginal) Then
                POOriginal = CDec(OPOOriginal)
                Dim gonRows() As DataRow = Nothing
                If Not IsNothing(tblGons) Then
                    If tblGons.Rows.Count > 0 Then
                        gonRows = tblGons.Select("ITEM_OTHER = " & IDAppItemOther)
                        If gonRows.Length > 0 Then
                            totalGonOut = CDec(gonRows(0)("LEFT_QTY"))
                        End If
                    End If
                End If
                Dim dummyT As DataTable = CType(Me.grdGon.DataSource, DataTable).Copy()
                gonRows = dummyT.Select("ITEM_OTHER = " & IDAppItemOther)
                If gonRows.Length > 0 Then
                    GonQtyIN = CDec(gonRows(0)("QTY"))
                End If
                row.BeginEdit()

                If POOriginal - (totalGonOut + GonQtyIN) <= 0 Then
                    'Me.GridEX1.SetValue("STATUS", "Completed")
                    'mustUpdatedData = True
                    row.Cells("STATUS").Value = "Completed"
                    mustUpdatedData = True
                ElseIf (totalGonOut + GonQtyIN) > 0 And (totalGonOut + GonQtyIN) < POOriginal Then
                    'Me.GridEX1.SetValue("STATUS", "Partial")
                    'mustUpdatedData = True
                    row.Cells("STATUS").Value = "Partial"
                    mustUpdatedData = True
                Else
                    row.Cells("STATUS").Value = "Pending"
                    mustUpdatedData = True
                End If
                row.EndEdit()
            End If
        Next
        If mustUpdatedData Then
            Me.GridEX1.UpdateData()
        End If
        Me.mustCheckStatus = False
    End Sub
    Private Sub chkProduct_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProduct.CheckedValuesChanged
        If Me.IsloadingMCB Then : Return : End If
        If Me.IsloadingRow Then : Return : End If
        If Me.SForm = SaveMode.Open Or Me.SForm = SaveMode.None Then
            Return
        End If
        If Not Me.HasLoadedForm Then : Return : End If
        Try
            ''prepare data for binding grid
            Me.Cursor = Cursors.WaitCursor
            'Dim dtGon As DataTable = Nothing
            Me.IsloadingRow = True
            Me.IsloadingMCB = True
            If Me.grdGon.DataSource Is Nothing Then
                Me.dtGonDetail = Me.clsGonNonPO.getGOnDetail(Me.txtGONNO.Text.Trim(), True)
                Me.grdGon.SetDataBinding(dtGonDetail, "")
                If Not Me.HasBoundGridGon Then
                    Me.grdGon.RetrieveStructure()
                End If
                FormatDataGrid()
                Me.HasBoundGridGon = True
            ElseIf Me.grdGon.RecordCount <= 0 Then
                dtGonDetail = Me.clsGonNonPO.getGOnDetail(Me.txtGONNO.Text.Trim(), True)
                Me.grdGon.SetDataBinding(dtGonDetail, "")
            End If
            Dim info As New CultureInfo("id-ID")

            Dim dtPO As DataTable = CType(Me.GridEX1.DataSource, DataTable).Copy()
            For Each rowChk As Janus.Windows.GridEX.GridEXRow In Me.chkProduct.DropDownList.GetRows()
                Dim IDAppItemOther As Integer = rowChk.Cells("ITEM_OTHER").Value
                Dim FoundRow() As DataRow = dtGonDetail.Select("ITEM = " & IDAppItemOther)
                Me.DVSampleProduct.RowFilter = "IDApp = " & IDAppItemOther
                Dim oUOM As Object = DVSampleProduct(0)("UnitOfMeasure")
                Dim oVol1 As Object = DVSampleProduct(0)("VOL1"), oVol2 As Object = DVSampleProduct(0)("VOL2")
                Dim oUnit1 As Object = DVSampleProduct(0)("UNIT1"), oUnit2 As Object = DVSampleProduct(0)("UNIT2")
                Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                Dim col1 As Integer = 0
                Dim collyBox As String = "", collyPackSize As String = ""
                Dim GonQty As Decimal = CDec(rowChk.Cells("LEFT_QTY").Value)
                If GonQty <= 0 Then
                Else
                    If GonQty >= Dvol1 Then
                        col1 = Convert.ToInt32(Decimal.Truncate(GonQty / Dvol1))
                        collyBox = IIf(col1 <= 0, "", String.Format("{0:g} {1}", col1, strUnit1))
                        Dim lqty As Decimal = GonQty Mod Dvol1
                        Dim ilqty As Integer = 0
                        If lqty >= 1 Then
                            'Dim c As Decimal = Decimal.Remainder(GonQty, Dvol1)
                            ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                        ElseIf lqty > 0 And lqty < 1 Then
                            'ilqty = ilqty + DVol2
                            ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                        End If
                        collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                    ElseIf GonQty > 0 Then ''gon kurang dari 1 coly
                        Dim ilqty As Integer = Convert.ToInt32((GonQty / Dvol1) * DVol2)
                        collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                    End If
                    If FoundRow.Length <= 0 And rowChk.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                        Dim newRow As DataRow = dtGonDetail.NewRow()
                        newRow.BeginEdit()
                        Dim IDApps() As DataRow = dtPO.Select("ITEM_OTHER = " & IDAppItemOther)
                        If IDApps.Length > 0 Then
                            newRow("FKAppPODetail") = IDApps(0)("IDApp")
                        Else
                            newRow("FKAppPODetail") = DBNull.Value
                        End If
                        newRow("ITEM_OTHER") = IDAppItemOther
                        newRow("SAMPLE_PRODUCT") = rowChk.Cells("ITEM").Value
                        newRow("QTY") = GonQty
                        newRow("QTY_UNIT") = String.Format(info, "{0:#,##0.000} {1}", GonQty, oUOM.ToString())
                        newRow("COLLY_BOX") = collyBox
                        newRow("COLLY_PACKSIZE") = collyPackSize
                        'newRow("BATCH_NO") = 
                        newRow("CreatedBy") = NufarmBussinesRules.User.UserLogin.UserName
                        newRow("CreatedDate") = NufarmBussinesRules.SharedClass.ServerDate
                        newRow.EndEdit()
                        dtGonDetail.Rows.Add(newRow)
                        Me.mustCheckStatus = True
                    ElseIf FoundRow.Length <= 0 And rowChk.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked Then

                    ElseIf FoundRow.Length > 0 And rowChk.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                        'check JIKA qty DI FOUNDROW TIDAK SAMA
                        Dim qtyFoundRow As Decimal = CDec(FoundRow(0)("QTY"))
                        If GonQty <> qtyFoundRow Then
                            'UPDATE data
                            FoundRow(0).BeginEdit()
                            FoundRow(0)("QTY") = GonQty
                            FoundRow(0)("QTY_UNIT") = String.Format(info, "{0:#,##0.000} {1}", GonQty, oUOM.ToString())
                            FoundRow(0)("COLLY_BOX") = collyBox
                            FoundRow(0)("COLLY_PACKSIZE") = collyPackSize
                            'newRow("BATCH_NO") = 
                            If SForm = SaveMode.Update Then
                                FoundRow(0)("ModifiedBy") = NufarmBussinesRules.User.UserLogin.UserName
                                FoundRow(0)("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
                            ElseIf SForm = SaveMode.Insert Then
                                FoundRow(0)("CreatedBy") = NufarmBussinesRules.User.UserLogin.UserName
                                FoundRow(0)("CreatedDate") = NufarmBussinesRules.SharedClass.ServerDate
                            End If
                            FoundRow(0).EndEdit()
                            Me.mustCheckStatus = True
                        End If
                    ElseIf FoundRow.Length > 0 And rowChk.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked Then
                        If Me.SForm = SaveMode.Update Then
                        Else
                            ''REMOVE data
                            Dim index As Integer = Me.grdGon.Find(Me.grdGon.RootTable.Columns("ITEM_OTHER"), Janus.Windows.GridEX.ConditionOperator.Equal, IDAppItemOther, -1, 1)
                            If index <> -1 Then
                                Me.grdGon.Delete()
                                Me.grdGon.UpdateData()
                                Me.mustCheckStatus = True
                            End If
                        End If
                        ''--lewati saja
                    End If
                End If
            Next
            Me.grdGon.Refetch()
            grdGon.AutoSizeColumns()
            Me.DVSampleProduct.RowFilter = ""
            If Not IsNothing(Me.grdGon.DataSource) Then
                If Me.grdGon.RecordCount > 0 Then
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                End If
            End If
            Me.IsloadingRow = False
            Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.DVSampleProduct.RowFilter = ""
            Me.mustCheckStatus = False
            Me.IsloadingRow = False
            Me.IsloadingMCB = False
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdGon_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdGon.CellUpdated
        If Me.IsloadingMCB = True Then : Return : End If
        If Me.IsloadingRow = True Then : Return : End If
        If Not Me.HasLoadedForm Then : Return : End If
        Try
            If e.Column.Key = "QTY" Then
                ''check total qty yang bisa di ambil
                Me.Cursor = Cursors.WaitCursor
                Dim info As New CultureInfo("id-ID")
                Dim ItemOther As Integer = Me.grdGon.GetValue("ITEM_OTHER")

                Dim DummyPODetail As DataTable = CType(Me.GridEX1.DataSource, DataTable).Copy()
                Dim oriPoRow() As DataRow = DummyPODetail.Select("ITEM_OTHER = " & ItemOther)
                Dim POOriginalQty As Decimal = CDec(oriPoRow(0)("QUANTITY"))
                Dim TotalGon As Decimal = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), ItemOther, False)
                ''get Original GonQty
                Dim oriGon As Decimal = 0
                If Not IsNothing(Me.grdGon.GetValue("IDApp")) And Not IsDBNull(Me.grdGon.GetValue("IDApp")) Then
                    oriGon = Me.clsGonNonPO.getGonQty(CInt(Me.grdGon.GetValue("IDApp")), True)
                End If
                Dim LeftQty As Decimal = POOriginalQty - (TotalGon - oriGon)
                If CDec(Me.grdGon.GetValue("QTY")) > LeftQty Then
                    Me.ShowMessageError(String.Format("Can not entered value more than {0:g}", LeftQty))
                    Me.grdGon.CancelCurrentEdit()
                    Me.Cursor = Cursors.Default
                    Return
                End If
                Me.DVSampleProduct.RowFilter = "IDApp = " & ItemOther
                Dim QTY As Decimal = Convert.ToDecimal(Me.grdGon.GetValue("QTY"))
                Dim oUOM As Object = DVSampleProduct(0)("UnitOfMeasure")
                Dim oVol1 As Object = DVSampleProduct(0)("VOL1"), oVol2 As Object = DVSampleProduct(0)("VOL2")
                Dim oUnit1 As Object = DVSampleProduct(0)("UNIT1"), oUnit2 As Object = DVSampleProduct(0)("UNIT2")
                Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                Dim DevQty As Decimal = DVSampleProduct(0)("DEVIDED_QUANTITY")
                Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                Dim col1 As Integer = 0
                Dim collyBox As String = "", collyPackSize As String = ""
                'Dim newRow As DataRow = CType(Me.grdGon.DataSource, DataTable).NewRow()
                If QTY Mod DevQty > 0 Then
                    Me.ShowMessageInfo(String.Format("can not enter value {0}" & vbCrLf & "VALUE is not even with packaging" & vbCrLf & "excess/lack value = {1}", QTY, QTY Mod DevQty))
                    Me.GridEX1.CancelCurrentEdit()

                    Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                End If
                If QTY >= Dvol1 Then
                    col1 = Convert.ToInt32(Decimal.Truncate(QTY / Dvol1))
                    collyBox = IIf(col1 <= 0, "", String.Format("{0:g} {1}", col1, strUnit1))
                    Dim lqty As Decimal = QTY Mod Dvol1
                    Dim ilqty As Integer = 0
                    If lqty >= 1 Then
                        'Dim c As Decimal = Decimal.Remainder(GonQty, Dvol1)
                        ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                    ElseIf lqty > 0 And lqty < 1 Then
                        ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                        'ilqty = ilqty + DVol2
                    End If
                    collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                ElseIf QTY > 0 Then ''gon kurang dari 1 coly
                    Dim ilqty As Integer = Convert.ToInt32((QTY / Dvol1) * DVol2)
                    collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                End If
                'foundRows(0)("QTY") = QTY
                Me.IsloadingRow = True
                Me.grdGon.SetValue("QTY_UNIT", String.Format(info, "{0:#,##0.000} {1}", QTY, oUOM.ToString()))
                Me.grdGon.SetValue("COLLY_BOX", collyBox)
                Me.grdGon.SetValue("COLLY_PACKSIZE", collyPackSize)
                ''newRow("BATCH_NO") = 
                If Me.SForm = SaveMode.Update Then
                    Me.grdGon.SetValue("ModifiedBy", NufarmBussinesRules.User.UserLogin.UserName)
                    Me.grdGon.SetValue("ModifiedDate", NufarmBussinesRules.SharedClass.ServerDate)
                End If
                Me.mustCheckStatus = True
            End If

            Me.grdGon.UpdateData()
            Me.IsloadingRow = False
            Me.DVSampleProduct.RowFilter = ""
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.DVSampleProduct.RowFilter = ""
            Me.grdGon.CancelCurrentEdit()
            Me.IsloadingRow = False
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdGon_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdGon.DeletingRecord
        Try
            If Me.IsloadingMCB Then : Return : End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.IsloadingMCB = True
            'Dim QTY As Decimal = Convert.ToDecimal(Me.grdGon.GetValue("QTY"))
            Dim IDAppItemOther As String = Me.grdGon.GetValue("ITEM_OTHER")
            'matikan allow edit di gridex1
            Me.GridEX1.RootTable.Columns("QUANTITY").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.chkProduct.SetDataBinding(Me.DVGonProduct, "")
            Dim checkedVals(dtGonDetail.Rows.Count) As Object
            Dim rows() As DataRow = dtGonDetail.Select("ITEM_OTHER <> " & IDAppItemOther)
            Dim i As Integer = 0
            For Each row As DataRow In rows
                If row("ITEM_OTHER").ToString() = IDAppItemOther Then
                Else
                    checkedVals.SetValue(row("ITEM_OTHER"), i)
                End If
                i += 1
            Next
            Me.chkProduct.Focus()
            Me.chkProduct.CheckedValues = checkedVals
            'Me.grdGon.Focus()
            'Me.mustCheckStatus = True
            Dim mustUpdatedData As Boolean = False
            'Dim BrandPackID As String = Me.grdGon.GetValue("ITEM")
            'Dim row As Janus.Windows.GridEX.GridEXRow = e.Row
            Dim DummyT As DataTable = CType(Me.GridEX1.DataSource, DataTable)
            Dim RowsT() As DataRow = DummyT.Select("ITEM_OTHER = " & IDAppItemOther, "")
            Dim IDApp As Object = e.Row.Cells("IDApp").Value
            Dim POOriginal As Decimal = RowsT(0)("QUANTITY")
            Dim totalGonOut As Decimal = 0
            totalGonOut = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), IDAppItemOther, IIf((IsDBNull(IDApp) Or IsNothing(IDApp)), 0, IDApp), False)
            RowsT(0).BeginEdit()
            If POOriginal - totalGonOut <= 0 Then
                RowsT(0)("STATUS") = "Completed"
                mustUpdatedData = True
            ElseIf totalGonOut > 0 And totalGonOut < POOriginal Then
                RowsT(0)("STATUS") = "Partial"
                mustUpdatedData = True
            Else
                RowsT(0)("STATUS") = "Pending"
                mustUpdatedData = True
            End If
            RowsT(0).EndEdit()
            If mustUpdatedData Then
                Me.GridEX1.UpdateData()
                Me.grdGon.UpdateData()
            End If
            Me.grdGon.UpdateData()
            Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Try
            Me.IsloadingMCB = True
            Me.IsloadingRow = True
            If Me.HasChangeGonDetail() Then
                If Me.ShowConfirmedMessage("GON data is already entered" & vbCrLf & "Discard any changes ?") = Windows.Forms.DialogResult.No Then
                    Return
                End If
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.ClearGonEntry()
            Me.setEnabledGONEntry(True, True)
            If Me.SForm = SaveMode.Update Or Me.SForm = SaveMode.Open Then
                Me.setEnabledPOEntry(False, True)
            End If
            ''fill chk Product
            'get gon data table from datasource
            'Dim DVTotalGon As DataView = Me.clsGonNonPO.getTotalGonAllBrandPack(Me.txtPORefNo.Text.Trim(), False)
            Me.DVGonProduct = Me.clsGonNonPO.getPODetail(Me.txtPORefNo.Text.Trim(), True)
            Me.chkProduct.DropDownDataSource = Me.DVGonProduct
            Me.chkProduct.DropDownValueMember = "ITEM_OTHER"
            Me.chkProduct.DropDownDisplayMember = "ITEM"
            ''reset object gon
            Me.OGONHeader = New Nufarm.Domain.GONHeader()
            If Not Me.IsHOUser And Not Me.IsSystemAdmin Then
                Me.cmdWarhouse.ReadOnly = False
                Me.cmdWarhouse.Focus()
                Select Case ConfigurationManager.AppSettings("WarhouseCode").ToString()
                    Case "SRG"
                        Me.cmdWarhouse.SelectedIndex = 5
                    Case "SBY"
                        Me.cmdWarhouse.SelectedIndex = 3
                    Case "MRK"
                        Me.cmdWarhouse.SelectedIndex = 2
                    Case "TGR"
                        Me.cmdWarhouse.SelectedIndex = 4
                End Select
                Me.cmdWarhouse.ReadOnly = True
            Else
                Me.cmdWarhouse.ReadOnly = False
            End If
            Me.SForm = SaveMode.Insert
            'Me.btnInsert.Text = "Save Gon"
            Me.btnInsert.Enabled = True
            Me.btnPrint.Enabled = False
            Me.IsloadingRow = False
            Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsloadingMCB = False : Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub

    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        ''validate data 
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.SaveData() Then
                Me.Opener.MustReload = True
                If Me.SForm = SaveMode.Insert Then
                    Me.setEnabledPOEntry(False, True)
                    Me.setEnabledGONEntry(False, True)
                    'Else
                    '    Me.Close()
                End If
                If Not IsNothing(Me.grdGon.DataSource) Then
                    Me.btnPrint.Enabled = Me.grdGon.RecordCount > 0
                End If
            End If
            Me.IsloadingRow = False
            Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
        Catch ex As ExecutionEngineException
            Me.IsloadingRow = False
            Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim BChanged As Boolean = False
            If Not Me.IsloadingRow Then : Me.IsloadingRow = True : End If
            If Me.SForm = SaveMode.Update Then
                If Me.HasChangeGonDetail() Or Me.HasChangedPODetail() Or Me.HasChangedGONHeader() Then
                    BChanged = True
                End If
            ElseIf Me.SForm = SaveMode.Insert Then
                If Me.HasChangedPODetail() Or Me.HasChangeGonDetail() Then
                    BChanged = True
                End If
            End If
            If BChanged Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    If Not Me.SaveData() Then : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return : End If
                    Me.Opener.MustReload = True
                End If
            End If
            Me.Close()
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub
    Private Sub ChkShiptoCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkShiptoCustomer.CheckedChanged
        If Me.IsloadingRow Then : Return : End If
        If Not Me.HasLoadedForm Then : Return : End If
        If Me.ChkShiptoCustomer.Checked Then 'distributor
            Me.mcbCustomer.Visible = True
            Me.btnFindDistributor.Visible = True
            Me.lblCustomerName.Visible = False
            Me.lblAddress.Visible = False
            Me.txtCustomerName.Visible = False
            Me.txtCustomerAddress.Visible = False
            Me.txtCustomerName.Text = ""
            Me.txtCustomerAddress.Text = ""
            Me.grpGonEntry.Size = Me.NormalGonEntry
            Dim address As String = ""
            If Me.mcbCustomer.Text <> "" Then
                Me.mcbCustomer.Focus()
                If Not IsNothing(Me.mcbCustomer.Value) Then
                    address = Me.mcbCustomer.DropDownList.GetValue("ADDRESS")
                End If
            End If
            Me.txtGonShipto.Visible = True
            'check last ship to 
            Dim GonShipto As String = "", ShiptoAdd As String = ""
            Dim LastShipTo As String = Me.clsGonNonPO.getLastShipTO(Me.txtSPPBNo.Text, GonShipto, ShiptoAdd, True)
            If Not String.IsNullOrEmpty(GonShipto) Then
                address = GonShipto.ToString()
            End If
            If Not String.IsNullOrEmpty(address) Then
                Me.txtGonShipto.Text = address
            Else
                Me.txtGonShipto.Text = Me.txtDefShipto.Text
            End If
        Else
            Me.grpGonEntry.Size = Me.SmallGonEntry
            Me.mcbCustomer.Visible = False
            Me.mcbCustomer.Value = Nothing
            Me.btnFindDistributor.Visible = False
            Dim GonShipto As String = "", ShiptoAdd As String = "", ShipTo As String = ""
            Dim LastShipTo As String = Me.clsGonNonPO.getLastShipTO(Me.txtSPPBNo.Text, GonShipto, ShiptoAdd, True)
            If Not String.IsNullOrEmpty(ShiptoAdd) Then
                ShipTo = ShiptoAdd.ToString()
            Else
                If Me.txtDefShipto.Text <> "" Then
                    ShipTo = Me.txtDefShipto.Text
                End If
            End If
            Me.txtCustomerAddress.Text = ShipTo

            Me.lblCustomerName.Visible = True
            Me.txtCustomerName.Visible = True
            Me.lblAddress.Visible = True
            Me.txtCustomerAddress.Visible = True
            Me.txtGonShipto.Visible = False
            Me.txtGonShipto.Text = ""
            Me.txtCustomerName.Focus()
        End If
    End Sub

    Private Sub btnFindDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindDistributor.btnClick
        If Me.SForm = SaveMode.None Or Me.SForm = SaveMode.Open Then
            Return
        End If
        If Not Me.ValidataHeader(True) Then
            Me.IsloadingRow = False
            Return
        End If
        'If Not Me.ValidateGONHeader(False) Then
        '    Return
        'End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.DVDistributor = Me.clsGonNonPO.GetDistributor(Me.mcbCustomer.Text.Trim(), True)
            Me.mcbCustomer.SetDataBinding(DVDistributor, "")
            Me.ShowMessageInfo(DVDistributor.Count.ToString() & " item(s) found")
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RecordAdded
        Try
            ''update brandpack di chkProduct
            If Me.IsloadingRow Then : Return : End If
            If Me.IsloadingMCB Then : Return : End If
            Me.IsloadingMCB = True
            Dim CheckedVals() As Object = Nothing
            If Not IsNothing(Me.chkProduct.DropDownDataSource) Then
                If Me.chkProduct.DropDownList.RecordCount > 0 Then
                    CheckedVals = Me.chkProduct.CheckedValues()
                End If
            Else
                Me.DVGonProduct = Me.clsGonNonPO.getPODetail(Me.txtPORefNo.Text.Trim(), True)
                Me.chkProduct.DropDownDataSource = Me.DVGonProduct
                Me.chkProduct.DropDownDisplayMember = "ITEM"
                Me.chkProduct.DropDownValueMember = "ITEM_OTHER"
            End If
            Dim DVChkProd As DataView = CType(Me.chkProduct.DropDownDataSource, DataView)
            Dim drv As DataRowView = DVChkProd.AddNew()
            drv("ITEM_OTHER") = Me.GridEX1.GetValue("ITEM_OTHER")
            drv("ITEM") = Me.GridEX1.DropDowns(0).GetValue("ITEM")
            drv("LEFT_QTY") = Me.GridEX1.GetValue("QUANTITY")

            drv.EndEdit()
            Me.chkProduct.DropDownList.Refetch()
            Me.chkProduct.CheckedValues = CheckedVals
            Me.GridEX1.UpdateData()
            Me.IsloadingMCB = False
            Me.IsloadingRow = False
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.IsloadingMCB = False
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            'Dim DummyPODetail As DataTable = Me.dtGonPODetail.Copy()
            Dim ItemOther As Integer = e.Row.Cells("ITEM_OTHER").Value
            If Not NufarmBussinesRules.SharedClass.IsUserHO And Not CMain.IsSystemAdministrator Then
                Dim CreatedDate As System.DateTime = e.Row.Cells("CreatedDate").Value
                Dim nDay As Integer = DateDiff(DateInterval.Day, CreatedDate, NufarmBussinesRules.SharedClass.ServerDate)
                If nDay >= 3 Then
                    Me.ShowMessageError("can not delete data after 3 days input")
                    e.Cancel = True
                    Return
                End If
            End If
            ''check Brandpack di GON lain
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage & vbCrLf & "All Gon data for " & e.Row.Cells("ITEM_OTHER").Text & vbCrLf & "Will be deleted too") = Windows.Forms.DialogResult.Yes Then
                Dim BFind As Boolean = False
                Me.IsloadingMCB = True
                Me.IsloadingRow = True
                If Not IsNothing(Me.dtGonDetail) Then
                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdGon.GetRows()
                        Dim IDAppItemOther As Integer = row.Cells("ITEM_OTHER").Value
                        If IDAppItemOther.ToString() = ItemOther.ToString() Then
                            row.Delete()
                            BFind = True
                        End If
                    Next
                    If BFind Then
                        Me.grdGon.UpdateData()
                    End If
                End If
                ''delete Item di mcb
                Me.chkProduct.Focus()
                BFind = False
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.chkProduct.DropDownList.GetDataRows()
                    Dim IDAppItemOther As Integer = row.Cells("ITEM_OTHER").Value
                    If IDAppItemOther.ToString() = ItemOther.ToString() Then
                        row.Delete() : BFind = True
                    End If
                Next
                If BFind Then : chkProduct.DropDownList.UpdateData() : End If
            End If
            Me.GridEX1.UpdateData()
            Me.mustCheckStatus = True
            Me.IsloadingMCB = False
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.IsloadingMCB = False
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        If Me.IsloadingRow Then : Return : End If
        If Me.IsloadingMCB Then : Return : End If
        If Not Me.HasLoadedForm Then : Return : End If
        Try
            If Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True Then
                If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    Me.GridEX1.RootTable.Columns("ITEM_OTHER").EditType = Janus.Windows.GridEX.EditType.NoEdit
                Else
                    Me.GridEX1.RootTable.Columns("ITEM_OTHER").EditType = Janus.Windows.GridEX.EditType.MultiColumnCombo
                    Me.GridEX1.Update()
                    Application.DoEvents()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GonNonPODist_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.IsloadingRow Then : Return : End If
        Try
            Dim BChanged As Boolean = False
            If Me.SForm = SaveMode.Update Then
                If Me.HasChangeGonDetail() Or Me.HasChangedPODetail() Or Me.HasChangedGONHeader() Then
                    BChanged = True
                End If
            ElseIf Me.SForm = SaveMode.Insert Then
                If Me.HasChangedPODetail() Or Me.HasChangeGonDetail() Then
                    BChanged = True
                End If
            End If
            If BChanged Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    Me.Opener.MustReload = Me.SaveData()
                End If
            End If
            Me.IsloadingRow = False : Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim BChanged As Boolean = False
            If Me.SForm = SaveMode.Update Then
                If Me.HasChangeGonDetail() Or Me.HasChangedPODetail() Or Me.HasChangedGONHeader() Then
                    BChanged = True
                End If
            ElseIf Me.SForm = SaveMode.Insert Then
                If Me.HasChangedPODetail() Or Me.HasChangeGonDetail() Then
                    BChanged = True
                End If
            End If
            If BChanged Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged & "Before printing you must save data ?") = Windows.Forms.DialogResult.Yes Then
                    Me.Cursor = Cursors.WaitCursor : Me.SaveData() : Me.Opener.MustReload = True
                Else
                    Me.Cursor = Cursors.Default : Return
                End If
            End If
            If grdGon.RecordCount <= 0 Then : Me.Cursor = Cursors.Default : Return : End If
            ''grid di design by dataset
            Dim tbl_ref_gon As New DataTable("tbl_ref_gon")
            Dim colDISTRIBUTOR_NAME As New DataColumn("DISTRIBUTOR_NAME", Type.GetType("System.String"))
            colDISTRIBUTOR_NAME.DefaultValue = ""
            Dim colVarDistAddress As New DataColumn("VAR_DIST_ADDRESS", Type.GetType("System.String"))
            Dim colADDRESS As New DataColumn("ADDRESS", Type.GetType("System.String"))
            colADDRESS.DefaultValue = ""
            Dim colPO_REF_NO As New DataColumn("PO_REF_NO", Type.GetType("System.String"))
            Dim colPO_REF_DATE As New DataColumn("PO_REF_DATE", Type.GetType("System.DateTime"))
            Dim colPONoAndDate As New DataColumn("POREF_NO_AND_DATE", Type.GetType("System.String"))
            Dim colSPPB_NO As New DataColumn("SPPB_NO", Type.GetType("System.String"))
            Dim colSPPB_DATE As New DataColumn("SPPB_DATE", Type.GetType("System.DateTime"))
            Dim colSPPBNoAndDate As New DataColumn("SPPB_NO_AND_DATE", Type.GetType("System.String"))
            Dim colVarGonDate As New DataColumn("VAR_GON_DATE_STR", Type.GetType("System.String"))
            Dim colVarWarHouse As New DataColumn("VAR_WARHOUSE", Type.GetType("System.String"))
            Dim colGON_NO As New DataColumn("GON_NO", Type.GetType("System.String"))
            'Dim colvarGonNO As New DataColumn("VAR_GON_NO", Type.GetType("System.String"))
            Dim colGON_DATE As New DataColumn("GON_DATE", Type.GetType("System.DateTime"))
            Dim colPolNoTrans As New DataColumn("POLICE_NO_TRANS", Type.GetType("System.String"))
            Dim colDriverTrans As New DataColumn("DRIVER_TRANS", Type.GetType("System.String"))
            'Dim colWARHOUSE As New DataColumn("GON_DATE", Type.GetType("System.DateTime"))
            Dim colTRANSPORTER_NAME As New DataColumn("TRANSPORTER_NAME", Type.GetType("System.String"))
            Dim colBrandPackName As New DataColumn("BRANDPACK_NAME", Type.GetType("System.String"))
            colBrandPackName.AllowDBNull = False
            Dim colQUANTITY As New DataColumn("QUANTITY", Type.GetType("System.String"))
            Dim colCOLLY_BOX As New DataColumn("COLLY_BOX", Type.GetType("System.String"))
            Dim colCOLLY_PACKSIZE As New DataColumn("COLLY_PACKSIZE", Type.GetType("System.String"))
            Dim colBATCH_NO As New DataColumn("BATCH_NO", Type.GetType("System.String"))
            colBATCH_NO.AllowDBNull = True
            tbl_ref_gon.Columns.AddRange(New DataColumn() {colDISTRIBUTOR_NAME, colVarDistAddress, colADDRESS, colPO_REF_NO, colPO_REF_DATE, colPONoAndDate, colSPPB_NO, _
            colSPPB_DATE, colSPPBNoAndDate, colVarGonDate, colVarWarHouse, colGON_NO, colGON_DATE, colPolNoTrans, colDriverTrans, colTRANSPORTER_NAME, colBrandPackName, colQUANTITY, _
            colCOLLY_BOX, colCOLLY_PACKSIZE, colBATCH_NO})
            'Me.mcbOA_REF_NO.DropDownList.Focus()
            'Me.UnabledEntryGON(False)
            Dim tblGon As DataTable = CType(Me.grdGon.DataSource, DataTable)
            Dim info As New CultureInfo("id-ID")
            Dim CustomerName As String = "", Address As String = ""
            If Me.ChkShiptoCustomer.Checked Then
                CustomerName = Me.mcbCustomer.Text
                Me.mcbCustomer.Focus()
                Address = Me.mcbCustomer.DropDownList().GetValue("ADDRESS")
            Else
                CustomerName = Me.txtCustomerName.Text.Trim()
                Address = Me.txtCustomerAddress.Text.Trim()
            End If
            ''masukan data conversi di gon_table columnya di hide saja
            For i As Integer = 0 To tblGon.Rows.Count - 1
                Dim row As DataRow = tbl_ref_gon.NewRow()
                Dim IDAppItemOther As Integer = tblGon.Rows(i)("ITEM_OTHER")
                'Address As String = Me.mcbOA_REF_NO.DropDownList().GetValue("ADDRESS").ToString()
                row("DISTRIBUTOR_NAME") = CustomerName

                row("ADDRESS") = Address
                row("VAR_DIST_ADDRESS") = String.Format("{0}" & vbCrLf & "{1}", CustomerName, Address)
                Dim PORefNo As String = Me.txtPORefNo.Text.Trim()
                Dim PORefDate As System.DateTime = Convert.ToDateTime(Me.dtPicPODate.Value.ToShortDateString())
                row("PO_REF_NO") = PORefNo
                row("PO_REF_DATE") = PORefDate
                row("POREF_NO_AND_DATE") = String.Format(info, "{0} - {1:dd/MM/yyyy}", PORefNo, PORefDate)
                Dim SPPBno As String = Me.txtSPPBNo.Text.Trim()
                Dim SPPBDate As System.DateTime = Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString())
                row("SPPB_NO") = SPPBno
                row("SPPB_DATE") = SPPBDate
                row("SPPB_NO_AND_DATE") = String.Format(info, "{0} - {1:dd/MM/yyyy}", SPPBno, SPPBDate)
                Dim GonDate As System.DateTime = Convert.ToDateTime(Me.dtPicGONDate.Value.ToShortDateString())
                row("GON_NO") = Me.txtGONNO.Text.Trim()
                'row("VAR_GON_NO") = String.Format("GON NO : {0}", Me.txtGONNO.Text.Trim())
                row("GON_DATE") = GonDate
                row("VAR_GON_DATE_STR") = String.Format(info, "Date : {0:dd/MM/yyyy}", GonDate)
                Me.cmdWarhouse.Focus()
                'row("WARHOUSE") = Me.cmdWarhouse.SelectedValue
                row("VAR_WARHOUSE") = String.Format("Gudang : {0}", Me.cmdWarhouse.SelectedValue)
                Me.mcbTransporter.Focus()
                row("POLICE_NO_TRANS") = Me.txtPolice_no_Trans.Text.Trim()
                row("DRIVER_TRANS") = Me.txtDriverTrans.Text.Trim()
                row("TRANSPORTER_NAME") = Me.mcbTransporter.Text
                Dim BrandPackName As String = tblGon.Rows(i)("SAMPLE_PRODUCT")
                row("BRANDPACK_NAME") = BrandPackName
                Dim GonQty As Decimal = Convert.ToDecimal(tblGon.Rows(i)("QTY"))
                Me.DVSampleProduct.Sort = "IDApp ASC"
                Dim index As Integer = DVSampleProduct.Find(IDAppItemOther)
                If index <> -1 Then
                    Dim oUOM As Object = DVSampleProduct(index)("UnitOfMeasure")
                    Dim oVol1 As Object = DVSampleProduct(index)("VOL1"), oVol2 As Object = DVSampleProduct(index)("VOL2")
                    Dim oUnit1 As Object = DVSampleProduct(index)("UNIT1"), oUnit2 As Object = DVSampleProduct(index)("UNIT2")
                    Dim ValidData As Boolean = True
                    If oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Volume 1 has not been set yet")
                        ValidData = False
                    ElseIf oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Volume 2 has not been set yet")
                        ValidData = False
                    ElseIf oUnit1 Is Nothing Or oUnit2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Unit 1 has not been set yet")
                        ValidData = False
                    ElseIf oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Unit 2 has not been set yet")
                        ValidData = False
                    End If
                    If Not ValidData Then : Cursor = Cursors.Default : Return : End If
                    Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                    Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                    Dim col1 As Integer = 0
                    Dim collyBox As String = "", collyPackSize As String = ""
                    If GonQty >= Dvol1 Then
                        col1 = Convert.ToInt32(Decimal.Truncate(GonQty / Dvol1))
                        collyBox = IIf(col1 <= 0, "", String.Format("{0:g} {1}", col1, strUnit1))
                        Dim lqty As Decimal = GonQty Mod Dvol1
                        Dim ilqty As Integer = 0
                        If lqty >= 1 Then
                            'Dim c As Decimal = Decimal.Remainder(GonQty, Dvol1)
                            ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                        ElseIf lqty > 0 And lqty < 1 Then
                            ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                            'ilqty = ilqty + DVol2
                        End If
                        collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                    ElseIf GonQty > 0 Then ''gon kurang dari 1 coly
                        Dim ilqty As Integer = Convert.ToInt32((GonQty / Dvol1) * DVol2)
                        collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                    End If
                    row("QUANTITY") = String.Format(info, "{0:#,##0.000} {1}", GonQty, oUOM.ToString())
                    row("COLLY_BOX") = collyBox
                    row("COLLY_PACKSIZE") = collyPackSize
                    row("BATCH_NO") = tblGon.Rows(i)("BATCH_NO")
                    row.EndEdit()
                    tbl_ref_gon.Rows.Add(row)
                End If
            Next
            tbl_ref_gon.AcceptChanges()
            Dim frmGonReport As New FrmGonReport()
            With frmGonReport
                Dim IsDotMatrixPrint As Boolean = CBool(ConfigurationManager.AppSettings("DotMatrixPrint"))
                Dim IsImmediatePrint As Boolean = CBool(ConfigurationManager.AppSettings("DotMatrixImmediatePrint"))
                If Not IsDotMatrixPrint Then
                    Dim G2 As New GonRpt2()
                    G2.SetDataSource(tbl_ref_gon)
                    .ReportDoc = G2
                    '.IsImmediatePrint = IsImmediatePrint
                Else
                    Dim g As New GonRpt()
                    g.SetDataSource(tbl_ref_gon)
                    .ReportDoc = g
                End If
                .crvGON.ReportSource = .ReportDoc
                .crvGON.DisplayGroupTree = False
                .isSingleReport = True
                '.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                If Not IsDotMatrixPrint And Not IsImmediatePrint Then
                    .ShowDialog(Me)
                ElseIf IsDotMatrixPrint And IsImmediatePrint Then
                    .crvGON.PrintReport()
                    Application.DoEvents()
                ElseIf IsDotMatrixPrint And Not IsImmediatePrint Then
                    .ShowDialog(Me)
                ElseIf Not IsDotMatrixPrint And IsImmediatePrint Then
                    .crvGON.PrintReport()
                    Application.DoEvents()
                End If

            End With
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtSPPBNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSPPBNo.KeyDown
        If Not txtSPPBNo.Enabled Then : Return : End If
        If e.KeyCode = Keys.Enter And (Me.OSPPBHeader.PONumber <> Me.txtPORefNo.Text.Trim()) Then
            Try
                If Me.ValidataHeader(False) Then
                    Me.GridEX1.Focus()
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    If IsNothing(Me.GridEX1.DataSource) Then
                        Me.IsloadingRow = True
                        Me.dtGonPODetail = Me.clsGonNonPO.getGonPODetail(Me.txtPORefNo.Text.Trim(), True)
                        Me.BindGridEx1(dtGonPODetail)
                    End If
                Else
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                End If
                Me.IsloadingRow = False
            Catch ex As Exception
                Me.IsloadingRow = False
                Me.Cursor = Cursors.Default
                Me.ShowMessageError(ex.Message)
            End Try
        End If
    End Sub

    Private Sub chkProduct_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProduct.DropDown
        If String.IsNullOrEmpty(Me.txtGONNO.Text.Trim()) Then
            Me.baseTooltip.Show("Please enter GON NO first", Me.txtGONNO, 2000)
            Me.txtGONNO.Focus()
            Return
        End If
    End Sub

    Private Sub mcbCustomer_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbCustomer.ValueChanged
        Try
            If Me.IsloadingRow Then : Return : End If
            If Me.IsloadingMCB Then : Return : End If
            If Not Me.HasLoadedForm Then : Return : End If
            If IsNothing(Me.mcbCustomer.Value) Or Me.mcbCustomer.Text = "" Then
                Return
            End If
            Dim OAddress As Object = Me.mcbCustomer.DropDownList.GetValue("ADDRESS")
            If IsNothing(OAddress) Or IsDBNull(OAddress) Then
                Me.txtGonShipto.Text = ""
            Else
                Me.txtGonShipto.Text = OAddress.ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPrintPrevSPPB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPrevSPPB.Click
        Try
            If Not Me.ValidataHeader(True) Then
                Return
            End If
            Dim BChanged As Boolean = Me.HasChangedHeaderPO()
            If Not BChanged Then
                BChanged = Me.HasChangedPODetail()
            End If
            Me.Cursor = Cursors.WaitCursor
            If BChanged Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged & "Before printing you must save data ?") = Windows.Forms.DialogResult.Yes Then
                    Me.Cursor = Cursors.WaitCursor : Me.SaveData() : Me.Opener.MustReload = True
                Else
                    Me.Cursor = Cursors.Default : Return
                End If
            End If
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
            Dim tblDummy As DataTable = CType(Me.GridEX1.DataSource, DataTable)
            Me.DVSampleProduct.RowFilter = ""
            Me.DVSampleProduct.Sort = "IDApp ASC"
            Dim info As New CultureInfo("id-ID")
            For i As Integer = 0 To tblDummy.Rows.Count - 1
                Dim POOriginal As Decimal = 0
                Dim row As DataRow = tblDummy.Rows(i)
                Dim newRow As DataRow = dtTable.NewRow()
                newRow.BeginEdit()
                newRow("PO_NUMBER") = Me.txtPORefNo.Text.Trim()
                newRow("SPPB_NUMBER") = Me.txtSPPBNo.Text.Trim()
                newRow("SPPB_DATE") = Me.dtPicSPPBDate.Value
                newRow("PO_DATE") = Me.dtPicPODate.Value
                If Not IsNothing(row("QUANTITY")) And Not IsDBNull(row("QUANTITY")) Then
                    POOriginal = row("QUANTITY")
                End If
                newRow("PO_ORIGINAL") = POOriginal
                newRow("STATUS") = row("STATUS")
                newRow("SHIP_TO_CUSTOMER") = Me.txtDefShipto.Text.Trim()
                Dim IDAppItemOther As Integer = row("ITEM_OTHER")
                Dim Index As Integer = Me.DVSampleProduct.Find(IDAppItemOther)
                Dim Item As String = "Unregistered sample product"
                Dim UnitOfMeasure = " ? "
                Dim collyBox As String = "", collyPackSize As String = ""
                If Index <> -1 Then
                    Item = Me.DVSampleProduct(Index)("ITEM")
                    UnitOfMeasure = Me.DVSampleProduct(Index)("UnitOfMeasure")
                    Dim oVol1 As Object = DVSampleProduct(Index)("VOL1"), oVol2 As Object = DVSampleProduct(Index)("VOL2")
                    Dim oUnit1 As Object = DVSampleProduct(Index)("UNIT1"), oUnit2 As Object = DVSampleProduct(Index)("UNIT2")
                    Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                    Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                    Dim col1 As Integer = 0
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
                End If
                newRow("ITEM") = Item
                newRow("COLLY_BOX") = collyBox
                newRow("COLLY_PACKSIZE") = collyPackSize
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
                .ShowDialog(Me)
            End With
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub

End Class
