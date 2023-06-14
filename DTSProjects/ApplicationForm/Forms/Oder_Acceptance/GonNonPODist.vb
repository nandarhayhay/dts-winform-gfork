Imports System
Imports NufarmBussinesRules.common.Helper
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Configuration
Public Class GonNonPODist
    Dim WithEvents frmLookUp As New LookUp()
    Friend Opener As GONWithoutPOMaster = Nothing
    Private HasBoundGridGon As Boolean = False
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

    Private m_clsGonNonPO As NufarmBussinesRules.OrderAcceptance.SeparatedGON = Nothing
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
    Friend DVProduct As DataView = Nothing

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
    Friend OSPPBHeader As New Nufarm.Domain.SPPBHeader()
    ''' <summary>
    ''' only used for editing mode from grid manager
    ''' </summary>
    ''' <remarks>only used for editing mode from grid manager</remarks>
    Friend OGONHeader As New Nufarm.Domain.GONHeader()
    Private isNewGON As Boolean = False
    Friend SForm As StatusForm = StatusForm.None
    Friend CMain As Main = Nothing
    Private IsHOUser As Boolean = NufarmBussinesRules.SharedClass.IsUserHO
    Private IsSystemAdmin = CBool(ConfigurationManager.AppSettings("SysA"))
    Private checkedVals As New List(Of String)
    Private mustCheckStatus As Boolean = False
    Private SmallGonEntry As New System.Drawing.Size(776, 152)
    Private NormalGonEntry As New System.Drawing.Size(776, 214)
    Private Property clsGonNonPO() As NufarmBussinesRules.OrderAcceptance.SeparatedGON
        Get
            If Me.m_clsGonNonPO Is Nothing Then
                Me.m_clsGonNonPO = New NufarmBussinesRules.OrderAcceptance.SeparatedGON()
            End If
            Return Me.m_clsGonNonPO
        End Get
        Set(ByVal value As NufarmBussinesRules.OrderAcceptance.SeparatedGON)
            Me.m_clsGonNonPO = value
        End Set
    End Property
    Friend Enum EditData
        SPPBAndPO
        GON
        None
    End Enum
    Friend Enum StatusForm
        Insert
        Edit
        Open
        None
    End Enum
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
                Dim BrandpackId As Object = row.Cells("BRANDPACK_ID").Value
                Dim qty As Object = row.Cells("QUANTITY").Value
                If IsNothing(BrandpackId) Or IsDBNull(BrandpackId) Then
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
        If Me.cmdWarhouse.SelectedIndex <> -1 Then
            If Me.cmdWarhouse.SelectedValue.ToString() <> Me.OGONHeader.WarhouseCode Then
                Return True
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
        If Not Me.SForm = StatusForm.Open Then
            If Me.SForm = StatusForm.Edit Then
                Me.Mode = SaveMode.Update
                If Me.IsHOUser Then
                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                End If
            ElseIf Me.SForm = StatusForm.Insert Then
                Me.Mode = SaveMode.Insert
                Me.dtPicGONDate.Value = Me.dtPicSPPBDate.Value
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            End If
            If CMain.IsSystemAdministrator Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            End If
        End If

        Me.DVProduct = Me.clsGonNonPO.getBrandPack(False)
        'Me.dtGonPODetail = Me.clsGonNonPO.getGonPODetail(Me.PO_REF_NO, False)
        Me.DVDistributor = Me.clsGonNonPO.GetDistributor("", False)
        Dim cSForm As NufarmBussinesRules.common.Helper.SaveMode = SaveMode.Insert
        Select Case SForm
            Case StatusForm.Edit
                cSForm = SaveMode.Update
            Case StatusForm.Insert
                cSForm = SaveMode.Insert
            Case StatusForm.Open
                cSForm = SaveMode.Update
        End Select
        Me.DVTransporter = Me.clsGonNonPO.GonMaster.getTransporter("", cSForm, False).DefaultView()
        Me.DVArea = Me.clsGonNonPO.GonMaster.getAreaGon("", cSForm, False).DefaultView()
        If Me.SForm = StatusForm.Edit Or Me.SForm = StatusForm.Open Then
            Me.DVGonProduct = Me.clsGonNonPO.getPODetail(Me.OSPPBHeader.PONumber, False)
        End If
        Me.DVMConversiProduct = Me.clsGonNonPO.getProdConvertion(cSForm, True)
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
            If col.Key = "ITEM" Then
                col.Visible = False
            End If
            If col.Key = "BRANDPACK_NAME" Then
                col.Caption = "ITEM DESCRIPTIONS"
            End If
            If col.Key = "CreatedBy" Or col.Key = "CreatedDate" Or col.Key = "ModifiedBy" Or col.Key = "ModifiedDate" Then
                col.Visible = False
            End If
            If col.Key = "QTY_UNIT" Then
                col.Visible = False
            End If
            If col.Key = "QTY" Then
                col.FormatString = "#,##0.0000"
            End If
            'If col.Key = "BATCH_NO" Then
            '    col.FormatString = "g"
            'End If
            If col.Key = "QTY" Or col.Key = "BATCH_NO" Then
                col.EditType = Janus.Windows.GridEX.EditType.TextBox
            Else
                col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
        Next
        Me.grdGon.FilterMode = Janus.Windows.GridEX.FilterMode.None
        grdGon.RootTable.Columns("BRANDPACK_NAME").Position = 0
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
        If HasChangedHeaderPO Then
            With Me.OSPPBHeader
                .PONumber = Me.txtPORefNo.Text.Trim()
                .PODate = Convert.ToDateTime(Me.dtPicPODate.Value.ToShortDateString())
                .SPPBNO = Me.txtSPPBNo.Text.Trim()
                .SPPBDate = Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString())
                .SPPBShipTo = Me.txtDefShipto.Text.Trim.Replace(vbCrLf, " ")
                If Me.SForm = StatusForm.Edit Then
                    .ModifiedBy = NufarmBussinesRules.User.UserLogin.UserName
                Else : SForm = StatusForm.Insert
                    .CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
                End If
            End With
        End If
        Dim UserFrom = ConfigurationManager.AppSettings("WarhouseCode")
        If HasChangedGONHeader Then
            If Not IsNothing(Me.grdGon.DataSource) Then
                If Me.grdGon.RecordCount > 0 Then
                    validData = Me.ValidateGONHeader(True)
                    If Not validData Then : Return False : End If
                End If
            End If
            validData = Me.ValidateGONHeader(False)
            If Not validData Then : Return False : End If
            With Me.OGONHeader
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
                If Me.SForm = StatusForm.Edit Then
                    .ModifiedBy = NufarmBussinesRules.User.UserLogin.UserName
                Else : SForm = StatusForm.Insert
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
        Return Me.clsGonNonPO.SaveData(Me.SForm, HasChangedHeaderPO, HasChangedPODetail, HasChangedGONHeader, HasChangeGonDetail, Me.OGONHeader, Me.OSPPBHeader, Me.dtGonPODetail, Me.dtGonDetail)
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
            If Me.SForm = StatusForm.Edit Or Me.SForm = StatusForm.Open Then
                Me.txtPORefNo.Text = OSPPBHeader.PONumber
                Me.dtPicPODate.Value = OSPPBHeader.PODate
                Me.txtSPPBNo.Text = OSPPBHeader.SPPBNO
                Me.dtPicSPPBDate.Value = OSPPBHeader.SPPBDate
                Me.txtDefShipto.Text = OSPPBHeader.SPPBShipTo
                Me.BindGridEx1(dtGonPODetail)
                Me.chkProduct.SetDataBinding(Me.DVGonProduct, "")
                Dim checkedVals(dtGonDetail.Rows.Count) As Object
                For i As Integer = 0 To dtGonDetail.Rows.Count - 1
                    checkedVals.SetValue(dtGonDetail.Rows(i)("ITEM"), i)
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
                If Me.SForm = StatusForm.Open Then
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
            'Me.ReadAccess()
            If Me.SForm = StatusForm.Edit Or Me.SForm = StatusForm.Insert Then
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
            Me.GridEX1.DropDowns(0).SetDataBinding(Me.DVProduct, "")
            Me.GridEX1.DropDowns(0).AutoSizeColumns()
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
    Private Function cekAndSetBrandPackGon(ByVal columnKey As String, ByVal BrandPackID As String, ByRef BrandPackName As String, ByVal FillChkProd As Boolean, ByVal fillGridGon As Boolean) As Boolean
        If columnKey = "QUANTITY" Or columnKey = "BRANDPACK_ID" Then
            'check apakah sudah ada GON nya
            If Not IsNothing(Me.grdGon.DataSource) Then
                Dim DummyGOndetail As DataTable = CType(Me.grdGon.DataSource, DataTable).Copy()
                Dim DVDummyGOndetail As DataView = DummyGOndetail.DefaultView()
                DVDummyGOndetail.Sort = "ITEM ASC"
                DVDummyGOndetail.RowFilter = "ITEM = '" & BrandPackID & "'"
                If Me.SForm = StatusForm.Edit Then
                    If DVDummyGOndetail.Count > 0 Then
                        Me.ShowMessageError("can not edit data" & vbCrLf & "GON has already been proceeded")
                        Me.GridEX1.CancelCurrentEdit()
                        Return False
                    End If
                End If
                Dim TotalGOnQty As Decimal = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), BrandPackID, True)
                If TotalGOnQty > 0 Then
                    Me.ShowMessageError("can not edit data" & vbCrLf & "GON has already been proceeded")
                    Me.GridEX1.CancelCurrentEdit()
                    Return False
                End If
            End If
            'Dim BrandPackID As Object = Me.GridEX1.GetValue("BRANDPACK_ID")
            Dim DVProDummy As DataView = Me.DVProduct.ToTable.Copy().DefaultView()
            DVProDummy.Sort = "BRANDPACK_ID"
            'Dim BrandPackName As String = ""
            Dim index As Integer = DVProDummy.Find(BrandPackID)
            If index <> -1 Then
                BrandPackName = DVProDummy(index)("BRANDPACK_NAME")
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
            Else
                Me.Cursor = Cursors.WaitCursor
                ''cek apakah yang di input bisa di bagi kemasan
                Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & BrandPackID & "' AND INACTIVE = " & False
                If Me.DVMConversiProduct.Count > 0 Then
                    Dim oVol1 As Object = DVMConversiProduct(0)("VOL1"), oVol2 As Object = DVMConversiProduct(0)("VOL2")
                    Dim oUnit1 As Object = DVMConversiProduct(0)("UNIT1"), oUnit2 As Object = DVMConversiProduct(0)("UNIT2")
                    If oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Volume 1 has not been set yet")
                        Me.GridEX1.CancelCurrentEdit()
                        Return False
                    ElseIf oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Volume 2 has not been set yet")
                        Me.GridEX1.CancelCurrentEdit()
                        Return False
                    ElseIf oUnit1 Is Nothing Or oUnit2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Unit 1 has not been set yet")
                        Me.GridEX1.CancelCurrentEdit()
                        Return False
                    ElseIf oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Unit 2 has not been set yet")
                        Me.GridEX1.CancelCurrentEdit()
                        Return False
                    End If
                    Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                    Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                    If QTY >= Dvol1 Then
                    ElseIf QTY > 0 Then
                        Dim ilqty As Decimal = (QTY / Dvol1) * DVol2
                        If CInt(ilqty) < 1 Then
                            Me.ShowMessageError("please enter valid quantity")
                            Me.GridEX1.CancelCurrentEdit()
                            Return False
                        End If
                    Else
                        Me.ShowMessageError("please enter valid quantity")
                        Me.GridEX1.CancelCurrentEdit()
                        Return False
                    End If
                Else
                    Me.ShowMessageError("Convertion for " & BrandPackName & " could not be found in database")
                    Me.GridEX1.CancelCurrentEdit()
                    Return False
                End If
            End If
            Me.Cursor = Cursors.WaitCursor
            'update LEFT_QTY
            Me.IsloadingMCB = True
            Me.IsloadingRow = True
            If Not IsNothing(Me.chkProduct.DropDownDataSource) Then
                If Me.chkProduct.DropDownList.RecordCount > 0 Then
                    If FillChkProd Then
                        Dim checkedVals() As Object = Me.chkProduct.CheckedValues()
                        Dim dtdummyChkProd As DataTable = CType(Me.chkProduct.DropDownDataSource, DataView).ToTable().Copy()
                        Dim dvdummyChkProd As DataView = dtdummyChkProd.DefaultView
                        dvdummyChkProd.Sort = "BRANDPACK_ID ASC"
                        index = dvdummyChkProd.Find(BrandPackID)
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
                        Dim foundRows() As DataRow = dtDummyGondetail.Select("ITEM = '" & BrandPackID & "'")
                        If foundRows.Length > 0 Then
                            Dim info As New CultureInfo("id-ID")
                            foundRows(0).BeginEdit()
                            Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & BrandPackID & "' AND INACTIVE = " & False
                            If DVMConversiProduct.Count <= 0 Then
                                Me.ShowMessageError("Convertion for " & BrandPackName & " could not be found in database")
                                Me.GridEX1.CancelCurrentEdit()
                                Return False
                            End If
                            Dim oVol1 As Object = DVMConversiProduct(0)("VOL1"), oVol2 As Object = DVMConversiProduct(0)("VOL2")
                            Dim oUnit1 As Object = DVMConversiProduct(0)("UNIT1"), oUnit2 As Object = DVMConversiProduct(0)("UNIT2")
                            Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                            Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                            Dim col1 As Integer = 0
                            Dim collyBox As String = "", collyPackSize As String = ""
                            'Dim newRow As DataRow = CType(Me.grdGon.DataSource, DataTable).NewRow()
                            If QTY >= Dvol1 Then
                                col1 = Convert.ToInt32(Decimal.Truncate(QTY / Dvol1))
                                collyBox = IIf(col1 <= 0, "", String.Format("{0:g} BOX", col1))
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
                            foundRows(0)("QTY_UNIT") = String.Format(info, "{0:#,##0.000} {1}", QTY, strUnit1)
                            foundRows(0)("COLLY_BOX") = collyBox
                            foundRows(0)("COLLY_PACKSIZE") = collyPackSize
                            'newRow("BATCH_NO") = 
                            If Me.SForm = StatusForm.Edit Then
                                foundRows(0)("ModifiedBy") = NufarmBussinesRules.User.UserLogin.UserName
                                foundRows(0)("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
                            ElseIf Me.SForm = StatusForm.Insert Then
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
            Return True
        End If
        Me.IsloadingMCB = False
        Me.IsloadingRow = False
    End Function
    Private Sub GridEX1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        If Me.IsloadingRow Then : Return : End If
        If Not Me.HasLoadedForm Then : Return : End If
        Try
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "BRANDPACK_ID" Or e.Column.Key = "QUANTITY" Then
                    Me.Cursor = Cursors.WaitCursor
                    Dim valData As Boolean = True
                    If IsNothing(Me.GridEX1.GetValue("BRANDPACK_ID")) Or IsDBNull(Me.GridEX1.GetValue("BRANDPACK_ID")) Then
                        valData = False
                    End If
                    If IsNothing(Me.GridEX1.GetValue("QUANTITY")) Or IsDBNull(Me.GridEX1.GetValue("QUANTITY")) Then
                        valData = False
                    End If
                    Dim BrandPackName As String = ""
                    If Me.cekAndSetBrandPackGon(e.Column.Key, Me.GridEX1.GetValue("BRANDPACK_ID"), BrandPackName, True, True) Then
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
                                Me.chkProduct.DropDownDisplayMember = "BRANDPACK_NAME"
                                Me.chkProduct.DropDownValueMember = "BRANDPACK_ID"

                            End If
                            Dim DVChkProd As DataView = CType(Me.chkProduct.DropDownDataSource, DataView)
                            Dim sort As String = DVChkProd.Sort
                            DVChkProd.Sort = "BRANDPACK_ID"
                            Dim BrandPackID As String = Me.GridEX1.GetValue("BRANDPACK_ID")
                            Dim LeftQty As Decimal = 0
                            Dim index As Integer = DVChkProd.Find(BrandPackID)
                            Dim drv As DataRowView = Nothing
                            If index <> -1 Then
                                drv = DVChkProd(index)
                                drv.BeginEdit()
                                ''check left QTY ke database
                                LeftQty = Me.clsGonNonPO.getLeftQty(Me.txtPORefNo.Text.Trim(), BrandPackID, True)
                                drv = DVChkProd(index)
                                drv("LEFT_QTY") = IIf(LeftQty > 0, LeftQty, Me.GridEX1.GetValue("QUANTITY"))
                            Else
                                drv = DVChkProd.AddNew()
                                drv("BRANDPACK_ID") = BrandPackID
                                drv("BRANDPACK_NAME") = BrandPackName
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
            Dim BrandPackID As Object = Me.GridEX1.GetValue("BRANDPACK_ID")
            Dim BrandPackName As String = Me.GridEX1.DropDowns(0).GetValue("BRANDPACK_NAME")
            Dim QTY As Object = Me.GridEX1.GetValue("QUANTITY")
            If IsNothing(BrandPackID) Or IsDBNull(BrandPackID) Then
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
            If Me.dtGonPODetail.Select("BRANDPACK_ID = '" & BrandPackID & "'").Length > 0 Then
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
                Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & BrandPackID & "' AND INACTIVE = " & False
                If Me.DVMConversiProduct.Count > 0 Then
                    Dim oVol1 As Object = DVMConversiProduct(0)("VOL1"), oVol2 As Object = DVMConversiProduct(0)("VOL2")
                    Dim oUnit1 As Object = DVMConversiProduct(0)("UNIT1"), oUnit2 As Object = DVMConversiProduct(0)("UNIT2")
                    If oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Volume 1 has not been set yet")
                        e.Cancel = True
                    ElseIf oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Volume 2 has not been set yet")
                        e.Cancel = True
                    ElseIf oUnit1 Is Nothing Or oUnit2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Unit 1 has not been set yet")
                        e.Cancel = True
                    ElseIf oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Unit 2 has not been set yet")
                        e.Cancel = True
                    End If
                    Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                    Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
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
                    Me.ShowMessageError("Convertion for " & BrandPackName & " could not be found in database")
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
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            e.Cancel = True
            Me.ShowMessageError(ex.Message)

        End Try
    End Sub

    'Private Sub grpGON_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpGON.Enter
    '    If Me.IsloadingRow Then : Return : End If
    '    If Me.IsloadingMCB Then : Return : End If
    '    If Not Me.HasLoadedForm Then : Return : End If
    '    'validasi
    '    Me.mcbCustomer.Focus()
    '    Me.mcbCustomer.DropDownList().AutoSizeColumns()
    '    Application.DoEvents()

    '    If Not Me.SForm = StatusForm.Edit Then
    '        If Not Me.ValidataHeader(True) Then
    '            Me.IsloadingRow = False
    '            Return
    '        End If
    '        If Not Me.ValidateGONHeader(False) Then
    '            Me.IsloadingRow = False
    '            Return
    '        End If
    '    End If
    'End Sub

    Private Sub GridEX1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Try

            If Me.SForm = StatusForm.None Or Me.SForm = StatusForm.Open Then
                Return
            End If
            If Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.frmLookUp = New LookUp()
            Dim dvDummyProd As DataView = Me.DVProduct.ToTable().Copy().DefaultView
            dvDummyProd.Sort = "BRANDPACK_ID"
            Dim BrandPackID As String = ""
            If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                BrandPackID = Me.GridEX1.GetValue("BRANDPACK_ID")
                Dim TotalGOnQty As Decimal = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), BrandPackID, True)
                If TotalGOnQty > 0 Then
                    Me.ShowMessageError("can not edit data" & vbCrLf & "GON has already been proceeded")
                    Me.Cursor = Cursors.Default
                    Return
                End If
            End If
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetRows
                BrandPackID = row.Cells("BRANDPACK_ID").Value.ToString()
                Dim Index As Integer = dvDummyProd.Find(BrandPackID)
                If Index <> -1 Then
                    dvDummyProd.Delete(Index)
                End If
            Next
            Me.checkedVals = New List(Of String)
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                With frmLookUp
                    .watermarkText = "Enter the product to search for"
                    .Grid.SetDataBinding(dvDummyProd, "")
                    .Grid.RetrieveStructure()
                    Application.DoEvents()
                    .Grid.RootTable.Columns(0).ShowRowSelector = False
                    .Grid.RootTable.Columns(0).UseHeaderSelector = False
                    .Grid.AutoSizeColumns()
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog(Me)
                End With
            ElseIf Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                With frmLookUp
                    .watermarkText = "Enter the product to search for"
                    .Grid.SetDataBinding(dvDummyProd, "")
                    .Grid.RetrieveStructure()
                    Application.DoEvents()
                    .Grid.RootTable.Columns(0).ShowRowSelector = True
                    .Grid.RootTable.Columns(0).UseHeaderSelector = False
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
                    Dim BRANDPACK_ID As String = frmLookUp.Grid.GetValue("BRANDPACK_ID"), BrandPackName As String = ""
                    Dim valData As Boolean = True
                    If IsNothing(Me.GridEX1.GetValue("BRANDPACK_ID")) Or IsDBNull(Me.GridEX1.GetValue("BRANDPACK_ID")) Then
                        valData = False
                    End If
                    If IsNothing(Me.GridEX1.GetValue("QUANTITY")) Or IsDBNull(Me.GridEX1.GetValue("QUANTITY")) Then
                        valData = False
                    End If
                    Me.GridEX1.SetValue("BRANDPACK_ID", BRANDPACK_ID)
                    Me.cekAndSetBrandPackGon("BRANDPACK_ID", BRANDPACK_ID, BrandPackName, False, True)
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
                            Me.chkProduct.DropDownDisplayMember = "BRANDPACK_NAME"
                            Me.chkProduct.DropDownValueMember = "BRANDPACK_ID"
                        End If
                        Dim DVChkProd As DataView = CType(Me.chkProduct.DropDownDataSource, DataView)
                        Dim sort As String = DVChkProd.Sort
                        DVChkProd.Sort = "BRANDPACK_ID"
                        Dim BrandPackID As String = Me.GridEX1.GetValue("BRANDPACK_ID")
                        Dim LeftQty As Decimal = 0
                        Dim index As Integer = DVChkProd.Find(BrandPackID)
                        Dim drv As DataRowView = Nothing
                        If index <> -1 Then
                            ''check left QTY ke database
                            LeftQty = Me.clsGonNonPO.getLeftQty(Me.txtPORefNo.Text.Trim(), BrandPackID, True)
                            drv = DVChkProd(index)
                            drv.BeginEdit()
                            drv("LEFT_QTY") = IIf(LeftQty > 0, LeftQty, Me.GridEX1.GetValue("QUANTITY"))
                        Else
                            drv = DVChkProd.AddNew()
                            drv("BRANDPACK_ID") = BrandPackID
                            drv("BRANDPACK_NAME") = BrandPackName
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
                CType(frmLookUp.Grid.DataSource, DataView).RowFilter = "BRANDPACK_NAME like '%" & frmLookUp.txtSearch.Text.Trim() & "%'"
                For Each row As Janus.Windows.GridEX.GridEXRow In frmLookUp.Grid.GetRows
                    If Me.checkedVals.Contains(row.Cells("BRANDPACK_ID").ToString()) Then
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
            'If Not frmLookUp.Grid.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            '    Return
            'End If
            If e.Row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                If Not Me.checkedVals.Contains(e.Row.Cells("BRANDPACK_ID").Value.ToString()) Then
                    Me.checkedVals.Add(e.Row.Cells("BRANDPACK_ID").Value.ToString())
                End If
            ElseIf e.Row.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked Then
                If Me.checkedVals.Contains(e.Row.Cells("BRANDPACK_ID").Value.ToString()) Then
                    Me.checkedVals.Remove(e.Row.Cells("BRANDPACK_ID").Value.ToString())
                End If
            End If
        Catch ex As Exception
            For Each row As Janus.Windows.GridEX.GridEXRow In frmLookUp.Grid.GetCheckedRows()
                If row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                    If Not Me.checkedVals.Contains(row.Cells("BRANDPACK_ID").Value.ToString()) Then
                        Me.checkedVals.Add(row.Cells("BRANDPACK_ID").Value.ToString())
                    End If
                ElseIf row.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked Then
                    If Me.checkedVals.Contains(row.Cells("BRANDPACK_ID").Value.ToString()) Then
                        Me.checkedVals.Remove(row.Cells("BRANDPACK_ID").Value.ToString())
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
            Dim BRANDPACK_ID As String = ""
            For Each item As String In Me.checkedVals
                BRANDPACK_ID = item
                With DT
                    Dim dr As DataRow = DT.NewRow()
                    dr.BeginEdit()
                    dr("BRANDPACK_ID") = BRANDPACK_ID
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
                Dim BRANDPACK_ID As String = frmLookUp.Grid.GetValue("BRANDPACK_ID"), BrandPackName As String = ""
                Dim valData As Boolean = True
                If IsNothing(Me.GridEX1.GetValue("BRANDPACK_ID")) Or IsDBNull(Me.GridEX1.GetValue("BRANDPACK_ID")) Then
                    valData = False
                End If
                If IsNothing(Me.GridEX1.GetValue("QUANTITY")) Or IsDBNull(Me.GridEX1.GetValue("QUANTITY")) Then
                    valData = False
                End If
                Me.GridEX1.SetValue("BRANDPACK_ID", BRANDPACK_ID)
                Me.cekAndSetBrandPackGon("BRANDPACK_ID", BRANDPACK_ID, BrandPackName, False, True)
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
                        Me.chkProduct.DropDownDisplayMember = "BRANDPACK_NAME"
                        Me.chkProduct.DropDownValueMember = "BRANDPACK_ID"
                    End If
                    Dim DVChkProd As DataView = CType(Me.chkProduct.DropDownDataSource, DataView)
                    Dim sort As String = DVChkProd.Sort
                    DVChkProd.Sort = "BRANDPACK_ID"
                    Dim BrandPackID As String = Me.GridEX1.GetValue("BRANDPACK_ID")
                    Dim LeftQty As Decimal = 0
                    Dim index As Integer = DVChkProd.Find(BrandPackID)
                    Dim drv As DataRowView = Nothing
                    If index <> -1 Then
                        ''check left QTY ke database
                        LeftQty = Me.clsGonNonPO.getLeftQty(Me.txtPORefNo.Text.Trim(), BrandPackID, True)
                        drv = DVChkProd(index)
                        drv.BeginEdit()
                        drv("LEFT_QTY") = IIf(LeftQty > 0, LeftQty, Me.GridEX1.GetValue("QUANTITY"))
                    Else
                        drv = DVChkProd.AddNew()
                        drv("BRANDPACK_ID") = BrandPackID
                        drv("BRANDPACK_NAME") = BrandPackName
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
        If Me.SForm = StatusForm.None Or Me.SForm = StatusForm.Open Then
            Return
        End If
        If Not Me.ValidataHeader(True) Then
            Me.IsloadingRow = False
            Return
        End If
        If Not Me.ValidateGONHeader(False) Then
            Me.IsloadingRow = False
            Return
        End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cSForm As NufarmBussinesRules.common.Helper.SaveMode = SaveMode.Insert
            Select Case SForm
                Case StatusForm.Edit
                    cSForm = SaveMode.Update
                Case StatusForm.Insert
                    cSForm = SaveMode.Insert
                Case StatusForm.Open
                    cSForm = SaveMode.Update
            End Select
            Me.DVArea = Me.clsGonNonPO.GonMaster.getAreaGon(Me.mcbGonArea.Text, cSForm, True).DefaultView()
            Me.mcbGonArea.SetDataBinding(Me.DVArea, "")
            'Me.mcbGonArea.DisplayMember = "AREA"
            'Me.mcbGonArea.ValueMember = "GON_ID_AREA"
            'Me.mcbGonArea.DropDownList.RetrieveStructure()
            'Me.mcbGonArea.DropDownList.Columns("GON_ID_AREA").Caption = "ID_AREA"
            'Me.mcbGonArea.Focus()
            'Me.mcbGonArea.DropDownList.AutoSizeColumns()
            Me.ShowMessageInfo(Me.DVArea.Count.ToString() & " data(s) found")
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            'Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BtnFindTransporter_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFindTransporter.btnClick
        If Me.SForm = StatusForm.None Or Me.SForm = StatusForm.Open Then
            Return
        End If
        If Not Me.ValidataHeader(True) Then
            Me.IsloadingRow = False
            Return
        End If
        If Not Me.ValidateGONHeader(False) Then
            Me.IsloadingRow = False
            Return
        End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cSForm As NufarmBussinesRules.common.Helper.SaveMode = SaveMode.Insert
            Select Case SForm
                Case StatusForm.Edit
                    cSForm = SaveMode.Update
                Case StatusForm.Insert
                    cSForm = SaveMode.Insert
                Case StatusForm.Open
                    cSForm = SaveMode.Update
            End Select
            Dim dtTrans As DataTable = Me.clsGonNonPO.GonMaster.getTransporter(Me.mcbTransporter.Text.Trim(), cSForm, True)
            Me.DVTransporter = dtTrans.DefaultView
            Me.mcbTransporter.SetDataBinding(DVTransporter, "")
            'Me.mcbTransporter.DisplayMember = "TRANSPORTER_NAME"
            'Me.mcbTransporter.ValueMember = "GT_ID"
            'Me.mcbTransporter.DropDownList.RetrieveStructure()
            'Me.mcbTransporter.Focus()
            'Me.mcbTransporter.DropDownList().AutoSizeColumns()
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
        Dim BrandPackID As String = Me.grdGon.GetValue("ITEM")
        For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetRows
            If row.Cells("BRANDPACK_ID").Value.ToString() = BrandPackID Then
                Dim IDApp As Object = Me.grdGon.GetValue("IDApp")

                Dim POOriginal As Decimal = row.Cells("QUANTITY").Value
                Dim totalGonOut As Decimal = 0, GonQtyIN As Decimal = Me.grdGon.GetValue("QTY")
                totalGonOut = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), BrandPackID, IIf((IsDBNull(IDApp) Or IsNothing(IDApp)), 0, IDApp), False)
                'If POOriginal - (totalGonOut + GonQtyIN) <= 0 Then
                '    Me.GridEX1.SetValue("STATUS", "Pending")
                '    mustUpdatedData = True
                'ElseIf (totalGonOut + GonQtyIN) > 0 And (totalGonOut + GonQtyIN) < POOriginal Then
                '    Me.GridEX1.SetValue("STATUS", "Partial")
                '    mustUpdatedData = True
                'Else
                '    Me.GridEX1.SetValue("STATUS", "Completed")
                '    mustUpdatedData = True
                'End If
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
        Dim BrandPackID As String = Me.GridEX1.GetValue("BRANDPACK_ID")
        Dim IDApp As Object = Me.GridEX1.GetValue("IDApp")
        Dim POOriginal As Decimal = Me.GridEX1.GetValue("QUANTITY")
        Dim totalGonOut As Decimal = 0, GonQtyIN As Decimal
        totalGonOut = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), BrandPackID, False)

        If e.Column.Key = "QUANTITY" Then
            Dim dummyT As DataTable = CType(Me.grdGon.DataSource, DataTable).Copy()
            Dim gonRows() As DataRow = dummyT.Select("ITEM = '" & BrandPackID & "'")
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
            Dim BrandPackID As String = row.Cells("BRANDPACK_ID").Value
            Dim OPOOriginal As Object = row.Cells("QUANTITY").Value, POOriginal As Decimal = 0
            Dim totalGonOut As Decimal = 0, GonQtyIN As Decimal = 0
            If Not IsNothing(OPOOriginal) And Not IsDBNull(OPOOriginal) Then
                POOriginal = CDec(OPOOriginal)
                Dim gonRows() As DataRow = Nothing
                If Not IsNothing(tblGons) Then
                    If tblGons.Rows.Count > 0 Then
                        gonRows = tblGons.Select("BRANDPACK_ID = '" & BrandPackID & "'")
                        If gonRows.Length > 0 Then
                            totalGonOut = CDec(gonRows(0)("LEFT_QTY"))
                        End If
                    End If
                End If
                Dim dummyT As DataTable = CType(Me.grdGon.DataSource, DataTable).Copy()
                gonRows = dummyT.Select("ITEM = '" & BrandPackID & "'")
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

    '''' <summary>
    '''' Check Status untuk deleted grid gon
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub CheckStatusByDeleteGridGon(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs)
    '    If Not mustCheckStatus Then : Return : End If

    '    Dim mustUpdatedData As Boolean = False
    '    Dim BrandPackID As String = Me.grdGon.GetValue("ITEM")
    '    'Dim row As Janus.Windows.GridEX.GridEXRow = e.Row
    '    Dim DummyT As DataTable = CType(Me.GridEX1.DataSource, DataTable)
    '    Dim RowsT() As DataRow = DummyT.Select("BRANDPACK_ID = '" & BrandPackID & "'", "")
    '    Dim IDApp As Object = RowsT(0)("IDApp")
    '    Dim POOriginal As Decimal = RowsT(0)("QUANTITY")
    '    Dim totalGonOut As Decimal = 0, GonQtyIN As Decimal = Me.grdGon.GetValue("QTY")
    '    totalGonOut = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), BrandPackID, IIf((IsDBNull(IDApp) Or IsNothing(IDApp)), 0, IDApp), False)
    '    RowsT(0).BeginEdit()
    '    If (totalGonOut + GonQtyIN) >= POOriginal Then
    '        RowsT(0)("STATUS").Value = "Completed"
    '        mustUpdatedData = True
    '    ElseIf (totalGonOut + GonQtyIN) > 0 Then
    '        RowsT(0)("STATUS") = "Partial"
    '        mustUpdatedData = True
    '    End If
    '    RowsT(0).EndEdit()
    '    If mustUpdatedData Then
    '        Me.GridEX1.UpdateData()
    '        Me.grdGon.UpdateData()
    '    End If
    '    Me.mustCheckStatus = False
    'End Sub
    Private Sub chkProduct_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProduct.CheckedValuesChanged
        If Me.IsloadingMCB Then : Return : End If
        If Me.IsloadingRow Then : Return : End If
        If Me.SForm = StatusForm.Open Or Me.SForm = StatusForm.None Then
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
                Dim BrandPackID As String = rowChk.Cells("BRANDPACK_ID").Value.ToString()
                Dim FoundRow() As DataRow = dtGonDetail.Select("ITEM = '" & BrandPackID & "'")
                Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & BrandPackID & "' AND INACTIVE = " & False
                Dim oVol1 As Object = DVMConversiProduct(0)("VOL1"), oVol2 As Object = DVMConversiProduct(0)("VOL2")
                Dim oUnit1 As Object = DVMConversiProduct(0)("UNIT1"), oUnit2 As Object = DVMConversiProduct(0)("UNIT2")
                Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                Dim col1 As Integer = 0
                Dim collyBox As String = "", collyPackSize As String = ""
                Dim GonQty As Decimal = CDec(rowChk.Cells("LEFT_QTY").Value)
                If GonQty <= 0 Then
                Else
                    If GonQty >= Dvol1 Then
                        col1 = Convert.ToInt32(Decimal.Truncate(GonQty / Dvol1))
                        collyBox = IIf(col1 <= 0, "", String.Format("{0:g} BOX", col1))
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
                        Dim IDApps() As DataRow = dtPO.Select("BRANDPACK_ID = '" & BrandPackID & "'")
                        If IDApps.Length > 0 Then
                            newRow("FKAppPODetail") = IDApps(0)("IDApp")
                        Else
                            newRow("FKAppPODetail") = DBNull.Value
                        End If
                        newRow("ITEM") = BrandPackID
                        newRow("BRANDPACK_NAME") = rowChk.Cells("BRANDPACK_NAME").Value
                        newRow("QTY") = GonQty
                        newRow("QTY_UNIT") = String.Format(info, "{0:#,##0.000} {1}", GonQty, strUnit1)
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
                            FoundRow(0)("QTY_UNIT") = String.Format(info, "{0:#,##0.000} {1}", GonQty, strUnit1)
                            FoundRow(0)("COLLY_BOX") = collyBox
                            FoundRow(0)("COLLY_PACKSIZE") = collyPackSize
                            'newRow("BATCH_NO") = 
                            If SForm = StatusForm.Edit Then
                                FoundRow(0)("ModifiedBy") = NufarmBussinesRules.User.UserLogin.UserName
                                FoundRow(0)("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
                            ElseIf SForm = StatusForm.Insert Then
                                FoundRow(0)("CreatedBy") = NufarmBussinesRules.User.UserLogin.UserName
                                FoundRow(0)("CreatedDate") = NufarmBussinesRules.SharedClass.ServerDate
                            End If
                            FoundRow(0).EndEdit()
                            Me.mustCheckStatus = True
                        End If
                    ElseIf FoundRow.Length > 0 And rowChk.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked Then
                        If Me.SForm = StatusForm.Edit Then
                        Else
                            ''REMOVE data
                            Dim index As Integer = Me.grdGon.Find(Me.grdGon.RootTable.Columns("ITEM"), Janus.Windows.GridEX.ConditionOperator.Equal, BrandPackID, -1, 1)
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
            Me.DVMConversiProduct.RowFilter = ""
            If Not IsNothing(Me.grdGon.DataSource) Then
                If Me.grdGon.RecordCount > 0 Then
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                End If
            End If
            Me.IsloadingRow = False
            Me.IsloadingMCB = False
            Me.Cursor = Cursors.Default
            'check gon_no,gon_date,gon_area,gon_transporter       

        Catch ex As Exception
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
                Dim BrandPackID As String = Me.grdGon.GetValue("ITEM").ToString()

                Dim DummyPODetail As DataTable = CType(Me.GridEX1.DataSource, DataTable).Copy()
                Dim oriPoRow() As DataRow = DummyPODetail.Select("BRANDPACK_ID = '" & BrandPackID & "'")
                Dim POOriginalQty As Decimal = CDec(oriPoRow(0)("QUANTITY"))
                Dim TotalGon As Decimal = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), BrandPackID, False)
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
                Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & BrandPackID & "' AND INACTIVE = " & False
                Dim QTY As Decimal = Convert.ToDecimal(Me.grdGon.GetValue("QTY"))
                Dim oVol1 As Object = DVMConversiProduct(0)("VOL1"), oVol2 As Object = DVMConversiProduct(0)("VOL2")
                Dim oUnit1 As Object = DVMConversiProduct(0)("UNIT1"), oUnit2 As Object = DVMConversiProduct(0)("UNIT2")
                Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                Dim col1 As Integer = 0
                Dim collyBox As String = "", collyPackSize As String = ""
                'Dim newRow As DataRow = CType(Me.grdGon.DataSource, DataTable).NewRow()
                If QTY >= Dvol1 Then
                    col1 = Convert.ToInt32(Decimal.Truncate(QTY / Dvol1))
                    collyBox = IIf(col1 <= 0, "", String.Format("{0:g} BOX", col1))
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
                Me.grdGon.SetValue("QTY_UNIT", String.Format(info, "{0:#,##0.000} {1}", QTY, strUnit1))
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
            Me.Cursor = Cursors.Default
        Catch ex As Exception
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
            Dim BrandPackID As String = Me.grdGon.GetValue("ITEM").ToString()
            'matikan allow edit di gridex1
            Me.GridEX1.RootTable.Columns("QUANTITY").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.chkProduct.SetDataBinding(Me.DVGonProduct, "")
            Dim checkedVals(dtGonDetail.Rows.Count) As Object
            Dim rows() As DataRow = dtGonDetail.Select("ITEM <> '" & BrandPackID & "'")
            Dim i As Integer = 0
            For Each row As DataRow In rows
                If row("ITEM").ToString() = BrandPackID Then
                Else
                    checkedVals.SetValue(row("ITEM"), i)
                End If
                i += 1
            Next
            'For i As Integer = 0 To dtGonDetail.Rows.Count - 1
            '    If dtGonDetail.Rows(i)("ITEM").ToString() = BrandPackID Then
            '    Else
            '        checkedVals.SetValue(dtGonDetail.Rows(i)("ITEM"), i)
            '    End If
            'Next
            ''checklist brandpack mana saja yang ada po detail
            Me.chkProduct.Focus()
            Me.chkProduct.CheckedValues = checkedVals
            'Me.grdGon.Focus()
            'Me.mustCheckStatus = True
            Dim mustUpdatedData As Boolean = False
            'Dim BrandPackID As String = Me.grdGon.GetValue("ITEM")
            'Dim row As Janus.Windows.GridEX.GridEXRow = e.Row
            Dim DummyT As DataTable = CType(Me.GridEX1.DataSource, DataTable)
            Dim RowsT() As DataRow = DummyT.Select("BRANDPACK_ID = '" & BrandPackID & "'", "")
            Dim IDApp As Object = e.Row.Cells("IDApp").Value
            Dim POOriginal As Decimal = RowsT(0)("QUANTITY")
            Dim totalGonOut As Decimal = 0
            totalGonOut = Me.clsGonNonPO.getTotalGon(Me.txtPORefNo.Text.Trim(), BrandPackID, IIf((IsDBNull(IDApp) Or IsNothing(IDApp)), 0, IDApp), False)
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
            If Me.SForm = StatusForm.Edit Or Me.SForm = StatusForm.Open Then
                Me.setEnabledPOEntry(False, True)
            End If
            ''fill chk Product
            'get gon data table from datasource
            'Dim DVTotalGon As DataView = Me.clsGonNonPO.getTotalGonAllBrandPack(Me.txtPORefNo.Text.Trim(), False)
            Me.DVGonProduct = Me.clsGonNonPO.getPODetail(Me.txtPORefNo.Text.Trim(), True)
            Me.chkProduct.SetDataBinding(DVGonProduct, "")
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
            Me.SForm = StatusForm.Insert
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
                If Me.SForm = StatusForm.Insert Then
                    Me.setEnabledPOEntry(False, True)
                    Me.setEnabledGONEntry(False, True)
                    If Not IsNothing(Me.grdGon.DataSource) Then
                        Me.btnPrint.Enabled = Me.grdGon.RecordCount > 0
                    End If
                Else
                    Me.Close()
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
            If Me.SForm = StatusForm.Edit Then
                If Me.HasChangeGonDetail() Or Me.HasChangedPODetail() Or Me.HasChangedGONHeader() Then
                    BChanged = True
                End If
            ElseIf Me.SForm = StatusForm.Insert Then
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


    'Private Sub grpGonDetail_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpGonDetail.Enter
    '    If Not Me.ValidataHeader(True) Then
    '        Me.IsloadingRow = False
    '        Return
    '    End If
    '    If Not Me.ValidateGONHeader(False) Then
    '        Me.IsloadingRow = False
    '        Return
    '    End If
    'End Sub

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
        If Me.SForm = StatusForm.None Or Me.SForm = StatusForm.Open Then
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
            'Me.mcbCustomer.DropDownList.RetrieveStructure()
            'Me.mcbCustomer.DisplayMember = "DISTRIBUTOR_NAME"
            'Me.mcbCustomer.ValueMember = "DISTRIBUTOR_ID"
            'Me.mcbCustomer.Focus()
            'Me.mcbCustomer.DropDownList.AutoSizeColumns()
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
                Me.chkProduct.DropDownDisplayMember = "BRANDPACK_NAME"
                Me.chkProduct.DropDownValueMember = "BRANDPACK_ID"
            End If
            Dim DVChkProd As DataView = CType(Me.chkProduct.DropDownDataSource, DataView)
            Dim drv As DataRowView = DVChkProd.AddNew()
            drv("BRANDPACK_ID") = Me.GridEX1.GetValue("BRANDPACK_ID")
            drv("BRANDPACK_NAME") = Me.GridEX1.DropDowns(0).GetValue("BRANDPACK_NAME")
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
            Dim BrandPackID As String = e.Row.Cells("BRANDPACK_ID").Value
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
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage & vbCrLf & "All Gon data for " & e.Row.Cells("BRANDPACK_ID").Text & vbCrLf & "Will be deleted too") = Windows.Forms.DialogResult.Yes Then
                Dim BFind As Boolean = False
                Me.IsloadingMCB = True
                Me.IsloadingRow = True
                If Not IsNothing(Me.dtGonDetail) Then
                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdGon.GetRows()
                        Dim GBrandPackID As String = row.Cells("ITEM").Value
                        If GBrandPackID.ToString() = BrandPackID.ToString() Then
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
                    Dim GBrandPackID As String = row.Cells("BRANDPACK_ID").Value
                    If GBrandPackID.ToString() = BrandPackID.ToString() Then
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
                    Me.GridEX1.RootTable.Columns("BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                Else
                    Me.GridEX1.RootTable.Columns("BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.MultiColumnCombo
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
            If Me.SForm = StatusForm.Edit Then
                If Me.HasChangeGonDetail() Or Me.HasChangedPODetail() Or Me.HasChangedGONHeader() Then
                    BChanged = True
                End If
            ElseIf Me.SForm = StatusForm.Insert Then
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
            If Me.SForm = StatusForm.Edit Then
                If Me.HasChangeGonDetail() Or Me.HasChangedPODetail() Or Me.HasChangedGONHeader() Then
                    BChanged = True
                End If
            ElseIf Me.SForm = StatusForm.Insert Then
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
                Dim BrandPackID As String = tblGon.Rows(i)("ITEM").ToString()
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
                Dim BrandPackName As String = tblGon.Rows(i)("BRANDPACK_NAME")
                row("BRANDPACK_NAME") = BrandPackName
                Dim GonQty As Decimal = Convert.ToDecimal(tblGon.Rows(i)("QTY"))
                Me.DVMConversiProduct.Sort = "BRANDPACK_ID ASC"
                Dim index As Integer = DVMConversiProduct.Find(BrandPackID)
                If index <> -1 Then
                    Dim oVol1 As Object = DVMConversiProduct(index)("VOL1"), oVol2 As Object = DVMConversiProduct(index)("VOL2")
                    Dim oUnit1 As Object = DVMConversiProduct(index)("UNIT1"), oUnit2 As Object = DVMConversiProduct(index)("UNIT2")
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
                        collyBox = IIf(col1 <= 0, "", String.Format("{0:g} BOX", col1))
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
                    row("QUANTITY") = String.Format(info, "{0:#,##0.000} {1}", GonQty, oUnit1.ToString())
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
End Class
