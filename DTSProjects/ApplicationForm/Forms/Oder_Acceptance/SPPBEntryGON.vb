Imports System.Data
Imports NufarmBussinesRules.common.Helper
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Configuration
Public Class SPPBEntryGON
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
    ''' <summary>
    ''' to detect if a form has been loaded 
    ''' </summary>
    ''' <remarks></remarks>
    Private HasLoadedForm As Boolean = False
    Friend frmParent As SPPB = Nothing
    Friend dvPO As DataView = Nothing
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

    Private clsSPPB As New NufarmBussinesRules.OrderAcceptance.SPPBEntryGON()
    Private WithEvents frmSetstat As FrmSetOtherStatus
    Friend DataToEdit As EditData = EditData.None ''to define what data to edit
    Friend DVProduct As DataView = Nothing
    Friend DVMConversiProduct As DataView = Nothing
    ''' <summary>
    '''  only used for editing mode from grid manager
    ''' </summary>
    ''' <remarks></remarks>
    Friend GON_NO As String = ""

    ''' <summary>
    '''  only used for editing mode from grid manager
    ''' </summary>
    ''' <remarks></remarks>
    Friend SPPB_NO As String = ""

    ''' <summary>
    ''' only used for editing mode from grid manager
    ''' </summary>
    ''' <remarks></remarks>
    Friend PO_REF_NO As String = ""
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
    Private DefWarHouseCode As String = ConfigurationManager.AppSettings("WarHouseCode")
    Private IsHOUser As Boolean = NufarmBussinesRules.SharedClass.IsUserHO
    Private IsSystemAdmin = CBool(ConfigurationManager.AppSettings("SysA"))

    Private Sub DisposeAllObjects()
        If Not IsNothing(Me.DS) Then
            DS.RejectChanges()
            DS.Dispose() : DS = Nothing
        End If
        If Not IsNothing(Me.dvPO) Then : Me.dvPO.Dispose() : Me.dvPO = Nothing : End If
        If Not IsNothing(Me.DVArea) Then : DVArea.Dispose() : DVArea = Nothing : End If
        If Not IsNothing(Me.DVTransporter) Then : DVTransporter.Dispose() : DVTransporter = Nothing : End If
        If Not IsNothing(Me.DVProduct) Then : DVProduct.Dispose() : DVProduct = Nothing : End If
        If Not IsNothing(Me.clsSPPB) Then : Me.clsSPPB.Dispose() : Me.clsSPPB = Nothing : End If
        If Not IsNothing(Me.frmSetstat) Then : Me.frmSetstat.Dispose() : Me.frmSetstat = Nothing : End If
        If Not IsNothing(Me.DVMConversiProduct) Then : Me.DVMConversiProduct.Dispose() : Me.frmSetstat = Nothing : End If
        'If Not IsNothing(Me.clsSPPB) Then : Me.clsSPPB.Dispose() : Me.clsSPPB = Nothing : End If
    End Sub
    ''' <summary>
    ''' Edit SPPB , can edit SPPB row data and can edit(if exists gon)/add new GON
    ''' Edit GON  can only edit gon on GON_QTY and gon no is based on GON NO
    ''' control can be edited only Transporter,Area, and date 
    ''' if edit is date control(GON_Date) minDate must be Min Gondate in database or spp_date if gon hasn't been recorded, max date is original gon_date
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EditData
        SPPB
        GON
        None
    End Enum
    Private Sub ClearText()
        Me.lblDistributor.Text = ""
        Me.lblPODate.Text = ""
        Me.lblSalesPerson.Text = ""
    End Sub
    'Private m_KodeGudang As String = "JKT"
    Private Sub ClearSPPBdata()
        Me.ClearText()
        Me.txtSPPBNO.Text = ""
        'Me.txtSPPBStatus.Text = "--New SPPB--"
        Me.dtPicSPPBDate.Text = NufarmBussinesRules.SharedClass.ServerDate
        If Not IsNothing(Me.mcbOA_REF_NO.Value) Then
            If Not String.IsNullOrEmpty(Me.lblPODate.Text) Then
                Me.dtPicSPPBDate.MinDate = Convert.ToDateTime(Me.lblPODate.Text)
            Else
                Me.dtPicSPPBDate.MinDate = NufarmBussinesRules.SharedClass.ServerDate
            End If
        Else
            Me.dtPicSPPBDate.MinDate = NufarmBussinesRules.SharedClass.ServerDate
        End If
        Me.btnReloadSPPBProduct.Enabled = False
        Me.grdSPPB.DataSource = Nothing
        Me.cmbStatusSPPB.Text = ""
        'Me.txtRemark.Text = ""
    End Sub
    Private Sub ClearGonData()
        If Not Me.IsloadingRow Then : Me.IsloadingRow = True : End If
        Me.txtGONNO.Text = "" : Me.txtGONNO.ReadOnly = False
        Me.dtPicGONDate.Text = ""
        Me.mcbTransporter.DataSource = Nothing
        Me.mcbTransporter.Text = ""
        Me.mcbGonArea.DataSource = Nothing
        Me.mcbGonArea.Text = ""
        Me.chkProduct.DropDownDataSource = Nothing
        Me.chkProduct.Text = "" : Me.chkProduct.ReadOnly = False
        Me.grdGon.DataSource = Nothing
        'Me.txtRemark.Text = ""
        Me.cmbStatusSPPB.SelectedIndex = 0
        Me.btnAddGon.Enabled = False
        Me.txtPolice_no_Trans.Text = ""
        Me.txtDriverTrans.Text = ""
        Me.txtShipTo.Text = ""
        ''get maximum gon date value
    End Sub
    Private Function isValidGON(ByVal CtrlToExclude As Control, ByVal IsAllToCheck As Boolean) As Boolean
        If Me.txtGONNO.Text = "" Then
            If Not IsNothing(CtrlToExclude) Then
                If CtrlToExclude.Name <> "txtGONNO" Then
                    Me.baseTooltip.Show("Invalid GON NO", Me.txtGONNO, 2500) : Me.txtGONNO.Focus() : Return False
                End If
            ElseIf IsAllToCheck Then
                Me.baseTooltip.Show("Invalid GON NO", Me.txtGONNO, 2500) : Me.txtGONNO.Focus() : Return False
            End If
        End If
        If Me.mcbTransporter.Value Is Nothing Then
            If Not IsNothing(CtrlToExclude) Then
                If CtrlToExclude.Name <> "mcbTransporter" Then
                    Me.baseTooltip.Show("Invalid Transporter", Me.mcbTransporter, 2500) : Me.mcbTransporter.Focus() : Return False
                End If
            ElseIf IsAllToCheck Then
                Me.baseTooltip.Show("Invalid Transporter", Me.mcbTransporter, 2500) : Me.mcbTransporter.Focus() : Return False
            End If
        End If
        If Me.mcbTransporter.SelectedIndex <= -1 Then
            If Not IsNothing(CtrlToExclude) Then
                If CtrlToExclude.Name <> "mcbTransporter" Then
                    Me.baseTooltip.Show("Invalid Transporter", Me.mcbTransporter, 2500) : Me.mcbTransporter.Focus() : Return False
                End If
            Else
                Me.baseTooltip.Show("Invalid Transporter", Me.mcbTransporter, 2500) : Me.mcbTransporter.Focus() : Return False
            End If
        End If
        If Me.mcbGonArea.Value Is Nothing Then
            If Not IsNothing(CtrlToExclude) Then
                If CtrlToExclude.Name <> "mcbGonArea" Then
                    Me.baseTooltip.Show("Invalid GON area", Me.mcbGonArea, 2500) : Me.mcbGonArea.Focus() : Return False
                End If
            Else
                Me.baseTooltip.Show("Invalid GON area", Me.mcbGonArea, 2500) : Me.mcbGonArea.Focus() : Return False
            End If
        End If
        If Me.mcbGonArea.SelectedIndex <= -1 Then
            If Not IsNothing(CtrlToExclude) Then
                If CtrlToExclude.Name <> "mcbGonArea" Then
                    Me.baseTooltip.Show("Invalid GON area", Me.mcbGonArea, 2500) : Me.mcbGonArea.Focus() : Return False
                End If
            Else
                Me.baseTooltip.Show("Invalid GON area", Me.mcbGonArea, 2500) : Me.mcbGonArea.Focus() : Return False
            End If
            'ElseIf Me.chkProduct.Values Is Nothing Then
            '    Me.baseTooltip.Show("Invalid product", Me.chkProduct, 2500) : Me.chkProduct.Focus() : Return False
        End If

        If Me.chkProduct.DropDownList.GetCheckedRows() Is Nothing Then
            If Not IsNothing(CtrlToExclude) Then
                If CtrlToExclude.Name <> "chkProduct" Then
                    Me.baseTooltip.Show("Invalid product", Me.chkProduct, 2500) : Me.chkProduct.Focus() : Return False
                End If
            Else
                Me.baseTooltip.Show("Invalid product", Me.chkProduct, 2500) : Me.chkProduct.Focus() : Return False
            End If
        End If
        If Me.chkProduct.DropDownList.GetCheckedRows.Length <= 0 Then
            If Not IsNothing(CtrlToExclude) Then
                If CtrlToExclude.Name <> "chkProduct" Then
                    Me.baseTooltip.Show("Invalid product", Me.chkProduct, 2500) : Me.chkProduct.Focus() : Return False
                End If
            ElseIf IsNothing(Me.chkProduct.CheckedValues) Then
                Me.baseTooltip.Show("Invalid product", Me.chkProduct, 2500) : Me.chkProduct.Focus() : Return False
            End If
        End If
        If Me.cmbStatusSPPB.Text = "" Then
            If Not IsNothing(CtrlToExclude) Then
                If CtrlToExclude.Name <> "cmbStatusSPPB" Then
                    Me.baseTooltip.Show("Invalid status", Me.cmbStatusSPPB, 2500) : Me.cmbStatusSPPB.Focus() : Return False
                End If
            Else
                Me.baseTooltip.Show("Invalid status", Me.cmbStatusSPPB, 2500) : Me.cmbStatusSPPB.Focus() : Return False
            End If
        End If
        If Me.cmbStatusSPPB.SelectedValue Is Nothing Then
            If Not IsNothing(CtrlToExclude) Then
                If CtrlToExclude.Name <> "cmbStatusSPPB" Then
                    Me.baseTooltip.Show("Invalid status", Me.cmbStatusSPPB, 2500) : Me.cmbStatusSPPB.Focus() : Return False
                End If
            Else
                Me.baseTooltip.Show("Invalid status", Me.cmbStatusSPPB, 2500) : Me.cmbStatusSPPB.Focus() : Return False
            End If
        End If
        If Me.cmbStatusSPPB.Text = "---Select---" Then
            If Not IsNothing(CtrlToExclude) Then
                If CtrlToExclude.Name <> "cmbStatusSPPB" Then
                    Me.baseTooltip.Show("Invalid status", Me.cmbStatusSPPB, 2500) : Me.cmbStatusSPPB.Focus() : Return False
                End If
            Else
                Me.baseTooltip.Show("Invalid status", Me.cmbStatusSPPB, 2500) : Me.cmbStatusSPPB.Focus() : Return False
            End If
        End If
        Return True
        If Me.cmdWarhouse.SelectedIndex <= 0 Then
            If Not IsNothing(CtrlToExclude) Then
                If CtrlToExclude.Name <> "cmdWarhouse" Then
                    Me.baseTooltip.Show("Invalid status", Me.cmdWarhouse, 2500) : Me.cmdWarhouse.Focus() : Return False
                End If
            Else
                Me.baseTooltip.Show("Invalid status", Me.cmdWarhouse, 2500) : Me.cmdWarhouse.Focus() : Return False
            End If
        End If

    End Function
    Private m_TblGON As DataTable = Nothing
    Friend Property tblMasterGON() As DataTable
        Get
            If m_TblGON Is Nothing Then
                m_TblGON = New DataTable("GON_DETAIL_INFO")
                Dim colGDID As New DataColumn("GON_DETAIL_ID", Type.GetType("System.String"))
                colGDID.AllowDBNull = False
                colGDID.Unique = True

                Dim colGONNO As New DataColumn("GON_HEADER_ID", Type.GetType("System.String"))
                colGONNO.AllowDBNull = False
                Dim colSPPBBrandpackID As New DataColumn("SPPB_BRANDPACK_ID", Type.GetType("System.String"))
                colSPPBBrandpackID.AllowDBNull = False
                Dim colBRANDPACKID As New DataColumn("BRANDPACK_ID", Type.GetType("System.String"))
                colBRANDPACKID.AllowDBNull = False
                Dim colBrandPackName As New DataColumn("BRANDPACK_NAME", Type.GetType("System.String"))
                colBrandPackName.AllowDBNull = False
                Dim colGOnQty As New DataColumn("GON_QTY", Type.GetType("System.Decimal"))
                colGOnQty.AllowDBNull = False
                colGOnQty.DefaultValue = 0
                Dim colIsOpen As New DataColumn("IsOPen", Type.GetType("System.Boolean"))
                colIsOpen.AllowDBNull = False
                colIsOpen.DefaultValue = False
                Dim colBatchNo As New DataColumn("BatchNo", Type.GetType("System.String"))
                colBatchNo.AllowDBNull = False
                colBatchNo.DefaultValue = ""

                Dim colUnit1 As New DataColumn("UNIT1", Type.GetType("System.String"))
                colUnit1.AllowDBNull = False

                Dim colVO11 As New DataColumn("VOL1", Type.GetType("System.Decimal"))
                colVO11.AllowDBNull = False

                Dim colUnit2 As New DataColumn("UNIT2", Type.GetType("System.String"))
                colUnit2.AllowDBNull = False
                Dim colVO12 As New DataColumn("VOL2", Type.GetType("System.Decimal"))
                colVO12.AllowDBNull = False


                Dim colIsCompleted As New DataColumn("IsCompleted", Type.GetType("System.Boolean"))
                colIsCompleted.AllowDBNull = False
                colIsCompleted.DefaultValue = False

                Dim colIsUpdatedBySystem As New DataColumn("IsUpdatedBySystem", Type.GetType("System.Boolean"))
                colIsUpdatedBySystem.AllowDBNull = False
                colIsUpdatedBySystem.DefaultValue = False

                Dim colCreatedBy As New DataColumn("CreatedBy", Type.GetType("System.String"))
                colCreatedBy.AllowDBNull = False
                colCreatedBy.DefaultValue = NufarmBussinesRules.User.UserLogin.UserName
                Dim colCreatedDate As New DataColumn("CreatedDate", Type.GetType("System.DateTime"))
                colCreatedDate.AllowDBNull = False
                colCreatedDate.DefaultValue = NufarmBussinesRules.SharedClass.ServerDate
                Dim ColModifiedBy As New DataColumn("ModifiedBy", Type.GetType("System.String"))
                Dim colModifiedDate As New DataColumn("ModifiedDate", Type.GetType("System.DateTime"))
                m_TblGON.Columns.AddRange(New DataColumn() {colGDID, colGONNO, colSPPBBrandpackID, colBRANDPACKID, colBrandPackName, colGOnQty, _
                colIsOpen, colBatchNo, colUnit1, colVO11, colUnit2, colVO12, colIsCompleted, colIsUpdatedBySystem, colCreatedBy, colCreatedDate, ColModifiedBy, colModifiedDate})
                Dim Key(1) As DataColumn
                Key(0) = colGDID
                m_TblGON.PrimaryKey = Key
                'DataColumn[] Key = new DataColumn[1]; DataColumn colCodeApp = new DataColumn("CodeApp", typeof(string));
                '        tblAchHeader.Columns.Add(colCodeApp); Key[0] = colCodeApp;
                '        tblAchHeader.PrimaryKey = Key;
                m_TblGON.AcceptChanges()
            ElseIf m_TblGON.Rows.Count > 0 Then
                m_TblGON.Clear()
                m_TblGON.AcceptChanges()
            End If
            Return m_TblGON
        End Get
        Set(ByVal value As DataTable)
            m_TblGON = value
        End Set
    End Property
    Private Sub InflateDataFromMCB(ByVal PO_REF_NO As String)
        'clearkan data
        Me.btnFilterOAREfNo.Enabled = True
    End Sub
    Private Sub btnFilterOAREfNo_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterOAREfNo.btnClick
        Try

            If Not Me.mcbOA_REF_NO.Enabled Then : Return : End If
            If Me.mcbOA_REF_NO.ReadOnly Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Dim tblPO As DataTable = Me.clsSPPB.getPO(Me.mcbOA_REF_NO.Text.TrimEnd().TrimStart(), Me.Mode, True)
            Me.dvPO = tblPO.DefaultView()
            Me.BindMulticolumnCombo(Me.mcbOA_REF_NO, Me.dvPO, "PO_REF_NO", "PO_REF_NO")
            Me.ShowMessageInfo(tblPO.Rows.Count.ToString() & " item(s) found")
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.LogMyEvent(ex.Message, Me.Name + "_btnFilterOAREfNo_btnClick")
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub

    Private Function ConfirmClearData() As Boolean
        If Me.Mode = SaveMode.Insert Then
            If Not IsNothing(Me.DS) Then
                If Me.DS.HasChanges() Then
                    If Me.ShowConfirmedMessage("if you changed PO Number, any changes will be discarded") = Windows.Forms.DialogResult.Yes Then
                        Return True
                    End If
                End If
            End If
        End If
        Return False
    End Function
    ''' <summary>
    ''' Clear all information and unabled the button
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clearData()
        Me.ClearSPPBdata()
        'clear GON DATA
        Me.ClearGonData()
        Me.DS = New DataSet("DSSPPB_GON") : Me.DS.Clear()
    End Sub
    Private Sub UnabledGonControl()
        Me.txtGONNO.Text = ""
        Me.txtGONNO.ReadOnly = True
        Me.dtPicGONDate.ReadOnly = True
        Me.mcbTransporter.ReadOnly = True : Me.mcbTransporter.Text = ""
        Me.mcbGonArea.ReadOnly = True : Me.mcbTransporter.Text = ""
        Me.chkProduct.ReadOnly = True : Me.chkProduct.Text = ""
        'Me.txtRemark.ReadOnly = True
    End Sub
    Private Sub enabledGonControl()
        Me.txtGONNO.Text = ""
        Me.txtGONNO.ReadOnly = False
        Me.dtPicGONDate.ReadOnly = False
        Me.mcbTransporter.ReadOnly = False : Me.mcbTransporter.Text = ""
        Me.mcbGonArea.ReadOnly = False : Me.mcbTransporter.Text = ""
        Me.chkProduct.ReadOnly = False : Me.chkProduct.Text = ""
        'Me.txtRemark.ReadOnly = False
    End Sub
    Private Function isValidSPPB() As Boolean
        If Me.mcbOA_REF_NO.Text = "" Then : Me.baseTooltip.Show("Please define PO_REF_NO", Me.mcbOA_REF_NO, 2500) : Me.mcbOA_REF_NO.Focus() : Return False : End If
        'If Me.mcbTransporter.SelectedIndex <= -1 Then : Me.baseTooltip.Show("Please define PO_REF_NO", Me.mcbOA_REF_NO, 2500) : Me.mcbOA_REF_NO.Focus() : Return False : End If
        'If Me.mcbOA_REF_NO.Value Is Nothing Then : Me.baseTooltip.Show("Please define PO_REF_NO", Me.mcbOA_REF_NO, 2500) : Me.mcbOA_REF_NO.Focus() : Return False : End If
        If Me.txtSPPBNO.Text = String.Empty Then : Me.baseTooltip.Show("Please define SPPB_NO", Me.txtSPPBNO, 2500) : Me.txtSPPBNO.Focus() : Return False : End If
        Return True
    End Function
    Private Sub mcbOA_REF_NO_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbOA_REF_NO.ValueChanged
        Try
            If Not Me.HasLoadedForm Then : Return : End If
            If Me.IsloadingRow Then : Return : End If
            If IsNothing(Me.mcbOA_REF_NO.Value) Then
                If Not ConfirmClearData() Then : Return : End If
                Me.clearData() : Me.Mode = SaveMode.Insert : Return
            ElseIf Me.mcbOA_REF_NO.Text = "" Then
                If Not ConfirmClearData() Then : Return : End If
                Me.clearData() : Me.Mode = SaveMode.Insert : Return
            ElseIf Me.mcbOA_REF_NO.SelectedIndex <= -1 Then
                If Not ConfirmClearData() Then : Return : End If
                Me.clearData() : Me.Mode = SaveMode.Insert : Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim ValMCB As Object = mcbOA_REF_NO.Value.ToString()
            Me.IsloadingRow = True
            If Me.HasChangedGONData() Or Me.HasChangedSPPBData() Then
                Me.ShowMessageInfo("This will reject any changes you've made")
            End If
            'check if dataset has change
            Me.DS.RejectChanges()
            'Clear SPPB Data
            Me.ClearSPPBdata()
            ''fill information in SPP
            'Dim DVDummyPO As DataView = Me.dvPO.ToTable().Copy().DefaultView()
            'DVDummyPO.Sort = "PO_REF_NO DESC"
            'Dim index As Integer = DVDummyPO.Find(Me.mcbOA_REF_NO.Value.ToString())
            Me.ClearText()
            Dim DV As DataView = CType(Me.mcbOA_REF_NO.DataSource, DataView)
            Dim DVCopy As DataView = DV.Table.Copy().DefaultView
            DVCopy.Sort = "PO_REF_NO DESC"
            Dim Index As Integer = DVCopy.Find(ValMCB)
            If Index <> -1 Then
                Me.lblDistributor.Text = DVCopy(Index)("DISTRIBUTOR_NAME")
                Me.lblPODate.Text = String.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(DVCopy(Index)("PO_DATE")))
                Me.lblSalesPerson.Text = Me.clsSPPB.getSalesPerson(ValMCB, False)
            End If

            ''Me.mcbOA_REF_NO.DropDownList().Select()
            'Me.lblDistributor.Text = Me.mcbOA_REF_NO.DropDownList.GetValue("DISTRIBUTOR_NAME").ToString()
            'Me.lblPODate.Text = String.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(Me.mcbOA_REF_NO.DropDownList.GetValue("PO_DATE")))
            'Me.lblSalesPerson.Text = Me.clsSPPB.getSalesPerson(mcbOA_REF_NO.Value.ToString(), False)

            Me.ClearGonData()
            Me.dtPicSPPBDate.MinDate = Convert.ToDateTime(Me.mcbOA_REF_NO.DropDownList.GetValue("PO_DATE"))
            Me.DS = New DataSet("DSSPPB_GON")
            Me.btnReloadSPPBProduct.Enabled = True
            'Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
            Me.dtPicSPPBDate.ReadOnly = False
            Me.txtSPPBNO.ReadOnly = False
            Me.btnSave.Enabled = False
            Me.grdSPPB.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.txtSPPBNO.Focus()
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
            'me.lblDistributor.Text = 
        Catch ex As Exception
            Me.IsloadingRow = False : Me.LogMyEvent(ex.Message, Me.Name + "_mcbOA_REF_NO_ValueChange")
            Me.Cursor = Cursors.Default
            'Me.ShowMessageError(ex.Message)
        End Try
    End Sub
    Private Sub BindGrid(ByVal grid As Janus.Windows.GridEX.GridEX, ByVal tbl As Object)
        If Not Me.IsloadingRow Then : Me.IsloadingRow = True : End If
        grid.SetDataBinding(tbl, "") : If tbl Is Nothing Then : Me.IsloadingRow = False : Return : End If
        Select Case grid.Name
            Case "grdSPPB"
                If Me.grdSPPB.RootTable Is Nothing Then : Me.grdSPPB.RetrieveStructure()
                ElseIf Me.grdSPPB.RootTable.Columns.Count <= 0 Then : Me.grdSPPB.RetrieveStructure()
                End If

                For Each col As Janus.Windows.GridEX.GridEXColumn In grid.RootTable.Columns
                    If col.DataMember <> "PO_CATEGORY" And col.DataMember <> "BRANDPACK_NAME" And col.DataMember <> "SPPB_QTY" And col.DataMember <> "STATUS" And col.DataMember <> "REMARK" And col.DataMember <> "TOTAL_GON_QTY" Then
                        col.Visible = False
                    Else : col.Visible = True
                    End If
                    If col.DataMember = "SPPB_QTY" Then
                        col.FormatString = "#,##0.000" : col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far : col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    End If
                    If col.DataMember = "TOTAL_GON_QTY" Then
                        col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        col.FormatString = "#,##0.000"
                        col.Caption = "TOTAL_GON"
                        col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    End If

                    If col.DataMember = "STATUS" Then : col.EditType = Janus.Windows.GridEX.EditType.DropDownList
                    ElseIf col.DataMember = "REMARK" Then : col.EditType = Janus.Windows.GridEX.EditType.TextBox
                    ElseIf col.DataMember = "SPPB_QTY" Then : col.EditType = Janus.Windows.GridEX.EditType.TextBox
                    ElseIf col.DataMember = "IsUpdatedBySystem" Then
                        col.Caption = "UBS"
                        If NufarmBussinesRules.User.UserLogin.IsAdmin Then
                            col.EditType = Janus.Windows.GridEX.EditType.CheckBox
                        Else : col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                        End If
                    Else : col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    If col.DataMember <> "REMARK" Then
                        col.AutoSize()
                    End If
                Next
                Me.grdSPPB.RootTable.Columns("IsUpdatedBySystem").Visible = NufarmBussinesRules.User.UserLogin.IsAdmin

                Me.AddCategoriesValueListSPPB()
                Me.grdSPPB.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Me.grdSPPB.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                Me.grdSPPB.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
                Me.AddConditionalFormatingGridSPPB()
                Me.AddConditionalFormatingGridSPPB1()
                Me.AddCategoriesRemark()
            Case "grdGon"
                If Me.grdGon.RootTable Is Nothing Then : Me.grdGon.RetrieveStructure()
                ElseIf Me.grdGon.RootTable.Columns.Count <= 0 Then : Me.grdGon.RetrieveStructure()
                End If
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdGon.RootTable.Columns
                    If col.DataMember = "BRANDPACK_NAME" Or col.DataMember = "GON_QTY" Or col.DataMember = "BatchNo" Then
                        col.Visible = True
                    Else : col.Visible = False
                    End If
                    If col.DataMember = "GON_QTY" Then
                        col.FormatString = "#,##0.000" : col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far : col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                        col.EditType = Janus.Windows.GridEX.EditType.TextBox
                    ElseIf col.DataMember = "IsUpdatedBySystem" Then
                        col.Caption = "UBS"
                        col.EditType = Janus.Windows.GridEX.EditType.CheckBox
                    ElseIf col.DataMember = "BatchNo" Then
                        col.Caption = "BATCH_NO"
                        col.EditType = Janus.Windows.GridEX.EditType.TextBox
                        col.MaxLength = 250
                    Else
                        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
                Me.grdGon.RootTable.Columns("IsUpdatedBySystem").Visible = NufarmBussinesRules.User.UserLogin.IsAdmin
                If NufarmBussinesRules.User.UserLogin.IsAdmin Then
                    Me.grdGon.RootTable.Columns("IsCompleted").Visible = True
                    Me.grdGon.RootTable.Columns("IsCompleted").EditType = Janus.Windows.GridEX.EditType.CheckBox
                    Me.grdGon.RootTable.Columns("IsUpdatedBySystem").EditType = Janus.Windows.GridEX.EditType.CheckBox
                End If
                'Me.grdGon.RootTable.Columns("IsOpen").Visible = True
                Me.grdGon.RootTable.Columns("IsUpdatedBySystem").Caption = "UBS"
                Me.AddConditionalFormatingGridGON()
                'Me.AddCategoriesValueListTransporter() : Me.AddCategoriesValueListGonArea() :  
                grid.AutoSizeColumns()
        End Select

    End Sub
    Private Sub AddCategoriesRemark()
        Dim ColRemark As Janus.Windows.GridEX.GridEXColumn = grdSPPB.RootTable.Columns("REMARK")
        ColRemark.EditType = Janus.Windows.GridEX.EditType.Combo
        'Set HasValueList property equal to true in order to be able to use the ValueList property
        ColRemark.HasValueList = True
        'Get the ValueList collection associated to this column
        Dim ValueList As Janus.Windows.GridEX.GridEXValueListItemCollection = ColRemark.ValueList
        Dim Arr(12) As String
        With Arr
            .SetValue("TIDAK ADA STOCK", 0)
            .SetValue("STOCK TAK ADA DI GUDANG X", 1)
            .SetValue("JADWAL PENGIRIMAN", 2)
            .SetValue("IN TRANSIT TRANSFER FG", 3)
            .SetValue("QUOTA SHIPMENT", 4)
            .SetValue("MENUNGGU TRANSPORTER", 5)
            .SetValue("OVERLOADING GUDANG", 6)
            .SetValue("[+ 2 KARENA LIBUR KERJA]", 7)
            .SetValue("BATAS FORECAST ROUNDUP", 8)
            .SetValue("MENUNGGU RILIS DO ROUNDUP", 9)
            .SetValue("ANTRIAN PRODUKSI", 10)
            .SetValue("PROSES PEMBUATAN PO ROUNDUP", 11)
            .SetValue("PENGAMBILAN OLEH CUSTOMER", 12)
        End With
        'Dim ListStatus() As String = {"TIDAK ADA STOCK", "STOCK_UNAVAILABLE", "AWAITING_TRANSPORTER", "QUOTA_SHIPMENT", "SHIPPED", "COMPLETED", "OTHER"}
        ValueList.PopulateValueList(Arr, "REMARK")
        ColRemark.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        ColRemark.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
        grdSPPB.RootTable.Columns("REMARK").Width = 210
    End Sub
    Private Sub AddCategoriesValueListSPPB()
        Dim ColStatus As Janus.Windows.GridEX.GridEXColumn = grdSPPB.RootTable.Columns("STATUS")
        ColStatus.EditType = Janus.Windows.GridEX.EditType.DropDownList
        'Set HasValueList property equal to true in order to be able to use the ValueList property
        ColStatus.HasValueList = True
        'Get the ValueList collection associated to this column
        Dim ValueList As Janus.Windows.GridEX.GridEXValueListItemCollection = ColStatus.ValueList
        Dim ListStatus() As String = {"--NEW SPPB--", "STOCK_UNAVAILABLE", "AWAITING_TRANSPORTER", "QUOTA_SHIPMENT", "SHIPPED", "COMPLETED", "ANOTHER WAREHOUSE", "FULL TRUCK", "WAITING LIST DO", "OVERLOAD WAREHOUSE", "OTHER"}
        ValueList.PopulateValueList(ListStatus, "STATUS")
        ColStatus.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        ColStatus.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    End Sub
    Private Sub AddConditionalFormatingGridSPPB()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSPPB.RootTable.Columns("IsUpdatedBySystem"), Janus.Windows.GridEX.ConditionOperator.Equal, True)
        fc.FormatStyle.ForeColor = Color.Red
        grdSPPB.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub AddConditionalFormatingGridSPPB1()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSPPB.RootTable.Columns("STATUS"), Janus.Windows.GridEX.ConditionOperator.Equal, "UNKNOWN")
        fc.FormatStyle.ForeColor = Color.Red
        grdSPPB.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub AddConditionalFormatingGridGON()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdGon.RootTable.Columns("IsUpdatedBySystem"), Janus.Windows.GridEX.ConditionOperator.Equal, True)
        fc.FormatStyle.ForeColor = Color.Red
        fc.AllowMerge = True
        grdGon.RootTable.FormatConditions.Add(fc)
    End Sub
    Private Sub AddCategoriesValueListGonArea()
        Me.grdGon.DropDowns.Add("GON_AREA")
        Me.grdGon.DropDowns("GON_AREA").SetDataBinding(Me.DVArea, "")
        Me.grdGon.DropDowns("GON_AREA").RetrieveStructure()
        Me.grdGon.DropDowns("GON_AREA").DisplayMember = "GON_AREA"
        Me.grdGon.DropDowns("GON_AREA").ValueMember = "GON_ID_AREA"
        Me.grdGon.RootTable.Columns("GON_ID_AREA").DropDown = Me.grdGon.DropDowns("GON_AREA")
        Me.grdGon.RootTable.Columns("GON_ID_AREA").EditType = Janus.Windows.GridEX.EditType.MultiColumnCombo
        Me.grdGon.DropDowns("GON_AREA").AutoSizeColumns()
    End Sub
    'Private Sub AddCategoriesValueListDescription()
    Private Sub AddCategoriesValueListTransporter()
        'Dim drpTrans As New Janus.Windows.GridEX.GridEXDropDown()
        'drpTrans.DisplayMember = "TRANSPORTER_NAME"
        'drpTrans.ValueMember = "GT_ID"
        'drpTrans.SetDataBinding(Me.DVTransporter, "")
        'drpTrans.RetrieveStructure()
        'drpTrans.AutoSizeColumns()
        'drpTrans.Key = "TRANSPORTER"

        Me.grdGon.DropDowns.Add("TRANSPORTER")
        Me.grdGon.DropDowns("TRANSPORTER").SetDataBinding(Me.DVTransporter, "")
        Me.grdGon.DropDowns("TRANSPORTER").RetrieveStructure()
        Me.grdGon.DropDowns("TRANSPORTER").DisplayMember = "TRANSPORTER_NAME"
        Me.grdGon.DropDowns("TRANSPORTER").ValueMember = "GT_ID"
        Me.grdGon.RootTable.Columns("GT_ID").DropDown = Me.grdGon.DropDowns("TRANSPORTER")
        Me.grdGon.RootTable.Columns("GT_ID").EditType = Janus.Windows.GridEX.EditType.MultiColumnCombo
        Me.grdGon.DropDowns("TRANSPORTER").AutoSizeColumns()

    End Sub
    Private Sub InitializeData()

        If Me.Mode = SaveMode.Insert Then
            Me.baseTooltip.BackColor = Me.BackColor
            Me.baseTooltip.IsBalloon = True : Me.baseTooltip.ShowAlways = False : Me.baseTooltip.ToolTipIcon = ToolTipIcon.Info
            Me.baseTooltip.ToolTipTitle = "Information" : Me.baseTooltip.UseAnimation = True : Me.baseTooltip.UseFading = True
            Me.btnFilterOAREfNo.Enabled = True
            ''fill mcb PORefNO
            Dim tblPO As DataTable = Me.clsSPPB.getPO(Me.mcbOA_REF_NO.Text.TrimEnd().TrimStart(), SaveMode.Insert, False)
            Me.dvPO = tblPO.DefaultView()

            Me.BindMulticolumnCombo(Me.mcbOA_REF_NO, dvPO, "PO_REF_NO", "PO_REF_NO")
            Me.ClearSPPBdata() : Me.ClearGonData()
            Me.btnSave.Enabled = False
        Else
            'bind grid
            'bind MulticolumnCombo with DVPO
            If Not IsNothing(Me.dvPO) Then
                Me.BindMulticolumnCombo(Me.mcbOA_REF_NO, Me.dvPO, "PO_REF_NO", "PO_REF_NO")
                Me.mcbOA_REF_NO.Value = Me.PO_REF_NO
                'Me.mcbOA_REF_NO.ReadOnly = True
            Else
                Me.ShowMessageInfo("Can not find PO record" & vbCrLf & "Please inform system administrator")
                Me.GRPSPPB.Enabled = False
                Me.grpGON.Enabled = False
                Me.DS = Nothing
                Me.btnSave.Enabled = False
                Me.HasSavedSPPB = True : Me.HasSavedGON = True
                Me.Cursor = Cursors.Default : Return
            End If
            'bind SPPB
            If Not IsNothing(Me.DS) Then
                If Me.DS.Tables.Contains("SPPB_BRANDPACK_INFO") Then
                    Me.BindGrid(Me.grdSPPB, Me.DS.Tables("SPPB_BRANDPACK_INFO").DefaultView())
                Else
                    Me.ShowMessageInfo("Can not find sppb record" & vbCrLf & "Please inform system administrator")
                    Me.GRPSPPB.Enabled = False
                    Me.grpGON.Enabled = False
                    Me.DS = Nothing
                    Me.btnSave.Enabled = False
                    Me.HasSavedSPPB = True : Me.HasSavedGON = True
                    Me.Cursor = Cursors.Default : Return
                End If
            Else
                Me.ShowMessageInfo("Can not find sppb record" & vbCrLf & "Please inform system administrator")
                Me.GRPSPPB.Enabled = False
                Me.grpGON.Enabled = False
                Me.DS = Nothing
                Me.btnSave.Enabled = False
                Me.HasSavedSPPB = True : Me.HasSavedGON = True
                Me.Cursor = Cursors.Default : Return
            End If
            ''property gon_no,gon_date,remark has been set in the parent
            'jus bind data to multicolumn combo and grid gon if object gon header has contained its property
            If Not IsNothing(Me.OGONHeader) Then
                If Not String.IsNullOrEmpty(Me.OGONHeader.GON_NO) Then
                    With Me.OGONHeader
                        If Not IsNothing(Me.DVTransporter) Then
                            Me.BindMulticolumnCombo(Me.mcbTransporter, Me.DVTransporter, "TRANSPORTER_NAME", "GT_ID")

                        End If
                        If Not IsNothing(Me.DVArea) Then
                            Me.BindMulticolumnCombo(Me.mcbGonArea, DVArea, "AREA", "GON_ID_AREA")

                        End If
                        If Not IsNothing(Me.DVProduct) Then
                            Me.BindCheckedCombo() : Me.chkProduct.Values = Me.SppBrandPackValues
                        End If
                        ''bindWarhouseCode
                    End With
                End If
            End If
            'BIND GRID
            If Not IsNothing(Me.DS) Then
                If Me.DS.Tables.Contains("GON_DETAIL_INFO") Then
                    Me.BindGrid(Me.grdGon, Me.DS.Tables("GON_DETAIL_INFO").DefaultView())
                End If
            End If
            'Me.btnSave.Enabled = False
            HasSavedSPPB = True
            ''fill MCBRefno only with PO Ref No in SPPBManager
        End If
        'get convertion data
        Me.DVMConversiProduct = Me.clsSPPB.getProdConvertion(Me.Mode, False)
        Me.DVMConversiProduct.Sort = "BRANDPACK_ID"
    End Sub
    Private Sub SPPBEntryGON_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'initial base tool tiip

            Me.InitializeData()
            If Me.Mode = SaveMode.Insert Then
                Me.mcbOA_REF_NO.Focus()
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
                Me.DefWarHouseCode = "JKT"
            End If
            If Me.Mode = SaveMode.Update Then
                'Me.mcbTransporter.DisplayMember = "TRANSPORTER_NAME"
                'Me.mcbTransporter.ValueMember = "GT_ID"
                'Me.mcbGonArea.DisplayMember = "AREA"
                'Me.mcbGonArea.ValueMember = "GON_ID_AREA"
                ''Me.mcbTransporter.ReadOnly = True
                'Me.mcbGonArea.ReadOnly = True
                'Me.mcbTransporter.Refresh()
                'Me.mcbGonArea.Refresh()
                If Not String.IsNullOrEmpty(Me.txtGONNO.Text) Then
                    'Me.mcbTransporter.Value = Me.OGONHeader.GT_ID
                    'Me.mcbTransporter.SelectedText = Me.OGONHeader.GT_ID
                    If Not String.IsNullOrEmpty(Me.TransporterName) Then
                        Me.mcbTransporter.Text = Me.TransporterName
                    End If
                    'Me.mcbGonArea.Value = Me.OGONHeader.GON_ID_AREA
                    If Not String.IsNullOrEmpty(Me.AreaName) Then
                        Me.mcbGonArea.Text = Me.AreaName
                    End If
                    Me.mcbGonArea.Focus()
                    Me.mcbGonArea.Value = Me.OGONHeader.GON_ID_AREA
                    'Me.mcbGonArea.SelectedText = Me.OGONHeader.GON_ID_AREA
                    Me.mcbGonArea.SelectAll()
                    'set value
                    Me.mcbTransporter.Focus()
                    Me.mcbTransporter.Value = Me.OGONHeader.GT_ID
                    Me.mcbTransporter.SelectAll()
                End If
            End If
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
            HasLoadedForm = True : Me.IsloadingRow = False

        Catch ex As Exception
            Me.HasLoadedForm = True
            Me.LogMyEvent(ex.Message, Me.Name + "_SPPBEntryGON_Load")
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function ReloadSPPData() As Boolean
        ''looping data if there is GON data
        Dim tblGOn As DataTable = Me.clsSPPB.getGOnData(Me.txtSPPBNO.Text.TrimEnd().TrimStart(), False)
        If tblGOn.Rows.Count > 0 Then
            If MessageBox.Show("This will reload sppb data" & vbCrLf & "If system finds some different data(s)" & vbCrLf & "gon data will be marked by red row", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Return False
            End If
        End If
        'reload data from server which po original qty > 0
        Dim tblSPPBBrandPack As DataTable = Me.clsSPPB.getSPPBBrandPack(Me.txtSPPBNO.Text, Me.mcbOA_REF_NO.Text.TrimEnd().TrimStart(), True, False)
        ''check if grid SPPB has been fiiled with data
        Dim DVSPPB As DataView = IIf(IsNothing(Me.grdSPPB.DataSource), Nothing, CType(Me.grdSPPB.DataSource, DataView))
        Dim Index As Integer = -1
        If Not IsNothing(Me.grdSPPB.DataSource) Then
            If Me.grdSPPB.RecordCount > 0 Then
                'check if data in grid has SPPNO which is equal to txtSPPNO 
                'if not equal clear data and replace with new on
                Dim DVDummySPPB As DataView = DVSPPB.ToTable().Copy().DefaultView()
                DVDummySPPB.Sort = "SPPB_NO DESC"
                Index = DVDummySPPB.Find(Me.txtSPPBNO.Text.TrimStart().TrimEnd())
                If Index <> -1 Then : Me.ReloadGridSPPB(DVSPPB, tblSPPBBrandPack) : Me.ReloadGridGON()
                Else : Me.FillGridAndView(False, tblGOn, tblSPPBBrandPack)
                End If
            Else
                Me.FillGridAndView(False, tblGOn, tblSPPBBrandPack)
            End If
        Else
            Me.FillGridAndView(False, tblGOn, tblSPPBBrandPack)
        End If
        If Not IsNothing(Me.grdSPPB.DataSource) Then
            If Me.grdSPPB.RecordCount > 0 Then
                Me.txtSPPBNO.ReadOnly = True
            Else
                Me.txtSPPBNO.ReadOnly = False
            End If
        Else
            Me.txtSPPBNO.ReadOnly = False
        End If
        Me.IsloadingRow = False
    End Function
    Private Sub ReloadGridGON()
        Dim ORowFilter As String = "", OSort As String = "", DVSPPB As DataView = Nothing, Index As Integer = -1
        Dim DVGOn As DataView = Nothing : Dim tblOGon As DataTable = Nothing
        If Not IsNothing(Me.grdGon.DataSource) Then
            DVGOn = CType(Me.grdGon.DataSource, DataView)
            If DVGOn.RowFilter <> "" Then
                ORowFilter = DVGOn.RowFilter
                DVGOn.RowFilter = ""
            End If
            If DVGOn.Sort <> "SPPB_BRANDPACK_ID" Then
                OSort = DVGOn.Sort : DVGOn.Sort = "SPPB_BRANDPACK_ID"
            End If
            tblOGon = DVGOn.ToTable()
        End If
        Dim tblOSPPB As DataTable = CType(Me.grdSPPB.DataSource, DataView).ToTable().Copy()

        If IsNothing(DVGOn) Then
            Me.BindGrid(Me.grdGon, Me.DS.Tables("GON_DETAIL_INFO").DefaultView())
        ElseIf DVGOn.Count <= 0 Then
            Me.BindGrid(Me.grdGon, Me.DS.Tables("GON_DETAIL_INFO").DefaultView())
        Else
            For i As Integer = 0 To tblOSPPB.Rows.Count - 1
                Dim OSPPBBrandPackID As String = tblOSPPB.Rows(i)("SPPB_BRANDPACK_ID").ToString()
                Dim OSppbQty As Decimal = IIf((Not IsNothing(tblOSPPB.Rows(i)("SPPB_QTY")) And Not IsDBNull(tblOSPPB.Rows(i)("SPPB_QTY"))), Convert.ToDecimal(tblOSPPB.Rows(i)("SPPB_QTY")), 0)
                Dim OTotalGOnQty As Object = tblOGon.Compute("SUM(GON_QTY)", "SPPB_BRANDPACK_ID = '" & OSPPBBrandPackID & "'")
                'getTotalGOn where GONNO not in gridGon
                If Not IsNothing(OTotalGOnQty) And Not IsDBNull(OTotalGOnQty) Then
                    OTotalGOnQty += Me.clsSPPB.GetTotalGONQTY(OSPPBBrandPackID, False, tblOGon)
                Else
                    OTotalGOnQty = Me.clsSPPB.GetTotalGONQTY(OSPPBBrandPackID, False, tblOGon)
                End If
                If (Convert.ToDecimal(OTotalGOnQty) > OSppbQty) Then
                    ''set row red
                    DVGOn.RowFilter = "SPPB_BRANDPACK_ID = '" & OSPPBBrandPackID & "'"
                    For i1 As Integer = 0 To DVGOn.Count - 1
                        DVGOn(i1).BeginEdit()
                        DVGOn(i1)("IsUpdatedBySystem") = True
                        If Me.Mode = SaveMode.Update Then
                            DVGOn(i1)("ModifiedBy") = NufarmBussinesRules.User.UserLogin.UserName
                            DVGOn(i1)("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
                        End If
                        DVGOn(i1).EndEdit()
                    Next
                    Me.grdGon.UpdateData()
                ElseIf (Convert.ToDecimal(OTotalGOnQty) = OSppbQty) And (OSppbQty <> 0) Then
                    'edit SPPB
                    'Dim drv As DataRowView = CType(Me.grdSPPB.DataSource, DataView)
                    DVSPPB = CType(grdSPPB.DataSource, DataView)
                    If (DVSPPB.Sort <> "SPPB_BRANDPACK_ID") Then : DVSPPB.Sort = "SPPB_BRANDPACK_ID" : End If
                    Index = DVSPPB.Find(OSPPBBrandPackID)
                    'DVSPPB(Index)("STATUS") = "SHIPPED"
                    If DVSPPB(Index)("STATUS").ToString() <> "SHIPPED" Then
                        DVSPPB(Index).BeginEdit()
                        DVSPPB(Index)("STATUS") = "SHIPPED"
                        'DVSPPB(Index)("IsUpdatedBySystem") = False
                        If Me.Mode = SaveMode.Update Then
                            DVSPPB(Index)("ModifiedBy") = NufarmBussinesRules.User.UserLogin.UserName
                            DVSPPB(Index)("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
                        End If
                        DVSPPB(Index).EndEdit()
                    End If
                End If
            Next
            Me.grdSPPB.UpdateData()
        End If
        If Not IsNothing(DVGOn) Then
            If DVGOn.RowFilter <> ORowFilter Then
                DVGOn.RowFilter = ORowFilter
            End If
            If DVGOn.Sort <> OSort Then
                DVGOn.Sort = OSort
            End If
        End If
    End Sub
    ''' <summary>
    ''' If data is in edit mode
    ''' </summary>
    ''' <param name="DVSPPB"></param>
    ''' <param name="tblSPPBBrandPack"></param>
    ''' <remarks></remarks>
    Private Sub ReloadGridSPPB(ByVal DVSPPB As DataView, ByVal tblSPPBBrandPack As DataTable)
        Dim ORowFilter As String = DVSPPB.RowFilter
        Dim OSort As String = DVSPPB.Sort
        Dim Index As Integer = -1
        DVSPPB.Sort = "SPPB_BRANDPACK_ID DESC"
        'jika data di dataset ada yang beda dengan data asli
        For Each row As DataRow In tblSPPBBrandPack.Rows
            Dim NSppbqty As Object = row("SPPB_QTY")
            Dim NsppbBrandpackID As String = row("SPPB_BRANDPACK_ID").ToString()
            Dim Nstatus As String = IIf((Not IsNothing(row("STATUS")) And Not IsDBNull(row("STATUS"))), row("STATUS").ToString(), "")
            Dim BrandpackName As String = row("BRANDPACK_NAME").ToString()
            Dim TotalGON As Object = row("TOTAL_GON_QTY")
            Dim POCategory As String = row("PO_CATEGORY").ToString()
            Dim OABrandPackID As String = row("OA_BRANDPACK_ID").ToString()
            Dim BrandPackID As String = row("BRANDPACK_ID").ToString()
            Index = -1
            Index = DVSPPB.Find(NsppbBrandpackID)
            If Index <> -1 Then
                Dim OSppbQTy As Decimal = IIf((Not IsNothing(DVSPPB(Index)("SPPB_QTY")) And Not IsDBNull(DVSPPB(Index)("SPPB_QTY"))), Convert.ToDecimal(DVSPPB(Index)("SPPB_QTY")), 0)
                Dim OStatus As String = IIf((Not IsNothing(DVSPPB(Index)("STATUS")) And Not IsDBNull(DVSPPB(Index)("STATUS"))), DVSPPB(Index)("STATUS").ToString(), "")
                If NSppbqty <> OSppbQTy Then
                    DVSPPB(Index).BeginEdit()
                    If OSppbQTy = 0 And NSppbqty > 0 Then
                        ''update data
                        If OStatus <> "CANCELED" Then
                            DVSPPB(Index)("SPPB_QTY") = NSppbqty
                            DVSPPB(Index)("STATUS") = "--NEW SPPB--"
                            DVSPPB(Index)("IsUpdatedBySystem") = True
                            DVSPPB(Index)("MODIFY_BY") = "System"
                            DVSPPB(Index)("MODIFY_DATE") = NufarmBussinesRules.SharedClass.ServerDate
                        End If
                    Else
                        DVSPPB(Index)("SPPB_QTY") = NSppbqty
                        DVSPPB(Index)("STATUS") = "UNKNOWN"
                        DVSPPB(Index)("IsUpdatedBySystem") = True
                        DVSPPB(Index)("MODIFY_BY") = "System"
                        DVSPPB(Index)("MODIFY_DATE") = NufarmBussinesRules.SharedClass.ServerDate
                    End If
                    DVSPPB(Index).EndEdit()
                End If
            Else
                Dim DRV As DataRowView = DVSPPB.AddNew()
                DRV.BeginEdit()
                DRV("SPPB_BRANDPACK_ID") = NsppbBrandpackID
                DRV("OA_BRANDPACK_ID") = OABrandPackID
                DRV("BRANDPACK_ID") = BrandPackID
                DRV("SPPB_NO") = Me.txtSPPBNO.Text.TrimStart().TrimEnd()
                DRV("BRANDPACK_NAME") = BrandpackName
                DRV("SPPB_QTY") = NSppbqty
                DRV("TOTAL_GON_QTY") = TotalGON
                DRV("STATUS") = Nstatus
                DRV("PO_CATEGORY") = POCategory
                DRV("CREATE_DATE") = NufarmBussinesRules.SharedClass.ServerDate
                DRV("CREATE_BY") = NufarmBussinesRules.User.UserLogin.UserName
                DRV("MODIFY_DATE") = NufarmBussinesRules.SharedClass.ServerDate
                DRV("MODIFY_BY") = NufarmBussinesRules.User.UserLogin.UserName
                DRV("REMARK") = ""
                DRV("IsUpdatedBySystem") = False
                DRV.EndEdit()
            End If
        Next
        Me.grdSPPB.UpdateData()
        ''balikan lagi rowfilter ke default 
        If CType(Me.grdSPPB.DataSource, DataView).RowFilter <> ORowFilter Then
            CType(Me.grdSPPB.DataSource, DataView).RowFilter = ORowFilter
        End If
        If CType(Me.grdSPPB.DataSource, DataView).Sort = OSort Then
            CType(Me.grdSPPB.DataSource, DataView).Sort = OSort
        End If

    End Sub
    Private Sub FillGridAndView(ByVal HasReloadDataSPPB As Boolean, ByVal tblGON As DataTable, ByVal tblSPPBBrandPack As DataTable)
        'Dim DVGOn As DataView = tblGOn.DefaultView()
        'DVGOn.Sort = "SPPB_BRANDPACK_ID DESC "

        'For i As Integer = 0 To tblSPPBBrandPack.Rows.Count - 1
        '    Dim SPPBBrandPackID As String = tblSPPBBrandPack.Rows(i)("SPPB_BRANDPACK_ID").ToString()
        '    'check data di datagrid/dataset

        '    Dim Index As Integer = DVGOn.Find(SPPBBrandPackID)
        '    Dim SPPBQty As Decimal = Convert.ToDecimal(tblSPPBBrandPack.Rows(i)("SPPB_QTY"))
        '    Dim statusSPPB As String = IIf((Not IsDBNull(tblSPPBBrandPack.Rows(i)("STATUS")) And Not IsNothing(tblSPPBBrandPack.Rows(i)("STATUS"))), tblSPPBBrandPack.Rows(i)("STATUS").ToString(), "")
        '    If Index <> -1 Then
        '        ''data found
        '        ''check total_qty by sum

        '        If SPPBQty < 0 Then
        '            Throw New Exception("Invalid data PO" & vbCrLf & "SPBB_Qty < 0)")
        '        ElseIf SPPBQty = 0 Then
        '            'if qppbqty = 0 total gon must be all 0

        '            Dim Rows() As DataRow = tblGOn.Select("SPPB_BRANDPACK_ID = '" & SPPBBrandPackID & "'")
        '            If Rows.Length > 0 Then
        '                For Each row As DataRow In Rows
        '                    row.BeginEdit()
        '                    row("GON_QTY") = 0
        '                    row("ModifiedBy") = NufarmBussinesRules.User.UserLogin.UserName
        '                    row("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
        '                    row("status") = "unknown"
        '                    row.EndEdit()
        '                Next
        '            End If
        '        End If
        '        Dim totalGON As Object = tblGOn.Compute("SUM(GON_QTY)", "SPPB_BRANDPACK_ID = '" + SPPBBrandPackID + "'")
        '        If Not IsNothing(totalGON) Then
        '            If Convert.ToDecimal(totalGON) = SPPBQty Then
        '                ''check status
        '                If statusSPPB <> "Shipped" Then

        '                End If
        '            End If
        '        End If
        '        'dim TotalGON as Decimal = DVGon.ge
        '    End If
        '    Me.grdGon.UpdateData()
        'Next
        If Me.DVTransporter Is Nothing Then
            Dim tblTrans As DataTable = Me.clsSPPB.getTransporter("", SaveMode.Insert, False)
            Me.DVTransporter = tblTrans.DefaultView()
        ElseIf Me.DVTransporter.Count <= 0 Then
            Dim tblTrans As DataTable = Me.clsSPPB.getTransporter("", SaveMode.Insert, False)
            Me.DVTransporter = tblTrans.DefaultView()
        End If
        If Me.DVArea Is Nothing Then
            Dim tblGonArea As DataTable = Me.clsSPPB.getAreaGon("", SaveMode.Insert, True)
            Me.DVArea = tblGonArea.DefaultView()
        ElseIf Me.DVArea.Count <= 0 Then
            Dim tblGonArea As DataTable = Me.clsSPPB.getAreaGon("", SaveMode.Insert, True)
            Me.DVArea = tblGonArea.DefaultView()
        End If

        If Not HasReloadDataSPPB Then
            Me.DS = New DataSet("DSSPPB_GON")
            Me.DS.Clear() : Me.DS.Tables.Add(tblSPPBBrandPack)

            Me.DS.Tables.Add(tblGON)
            Me.DS.AcceptChanges()
            'bind data
            Me.BindGrid(Me.grdSPPB, Me.DS.Tables("SPPB_BRANDPACK_INFO").DefaultView())

            Me.BindGrid(Me.grdGon, Me.DS.Tables("GON_DETAIL_INFO").DefaultView())
            If Me.Mode = SaveMode.Insert Then
                Me.HasSavedSPPB = (Me.grdSPPB.RecordCount <= 0)
            End If
            'ME.BindGrid(
        End If
        'detect changes
        If Not Me.HasSavedSPPB Then : Me.btnSave.Enabled = True : End If
        If Me.HasChangedSPPBData() Then : Me.btnSave.Enabled = True : End If
        ''if there is gon data found 'modify status
        ''if totalQtyGOn = sppbqty then status is shipped 
        '' if totalQtyGOn < sppbqty and totalQtyGon > 0
        ''status unknown set red text status must be revised ,because can not be difined automaticaly by system
        ''if totalQtyGon = 0 means it's been canceled by user set status become "canceled"
        '' if not exists gon set status = '--new sppb--'
    End Sub
    Private Sub btnReloadSPPBProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReloadSPPBProduct.Click
        Try
            If Not Me.isValidSPPB() Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.IsloadingRow = True
            'chek if SPP_NO has exists in database
            Dim HasSPPBBefore As Boolean = False
            If Me.Mode = SaveMode.Insert Then
                HasSPPBBefore = Me.clsSPPB.IsExistsSPPBNO(Me.txtSPPBNO.Text.TrimStart().TrimEnd(), False)
            ElseIf Me.grdSPPB.DataSource Is Nothing Then
                HasSPPBBefore = Me.clsSPPB.IsExistsSPPBNO(Me.txtSPPBNO.Text.TrimStart().TrimEnd(), False)
            ElseIf Me.grdSPPB.RecordCount <= 0 Then
                CType(Me.grdSPPB.DataSource, DataView).RowFilter = ""
                If Me.grdSPPB.RecordCount <= 0 Then
                    HasSPPBBefore = Me.clsSPPB.IsExistsSPPBNO(Me.txtSPPBNO.Text.TrimStart().TrimEnd(), False)
                End If
            End If
            If HasSPPBBefore Then : Me.ShowMessageInfo(Me.MessageDataHasExisted) : IsloadingRow = False : Me.Cursor = Cursors.Default : Return : End If
            Me.ReloadSPPData()
            Me.btnAddGon.Enabled = True
            Me.UnabledEntryGON(True)
            If Not Me.btnSave.Enabled Then : Me.btnSave.Enabled = (Me.HasChangedSPPBData() Or Me.HasChangedGONData()) : End If
            'Me.btnNewSPPB.Enabled = (Me.grdSPPB.RecordCount <= 0)
            Me.HasSavedSPPB = (Me.grdSPPB.RecordCount <= 0)
            Me.Cursor = Cursors.Default
            IsloadingRow = False
        Catch ex As Exception
            Me.HasSavedSPPB = (Me.grdSPPB.RecordCount <= 0)
            Me.Cursor = Cursors.Default
            Me.IsloadingRow = False
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnReloadSPPBProduct_Click")

        End Try
    End Sub
    Private Sub UnabledEntryGON(ByVal IsReadOnly As Boolean)
        Me.txtGONNO.ReadOnly = IsReadOnly
        Me.mcbGonArea.ReadOnly = IsReadOnly
        Me.mcbTransporter.ReadOnly = IsReadOnly
        Me.dtPicGONDate.ReadOnly = IsReadOnly
        Me.cmbStatusSPPB.ReadOnly = IsReadOnly
        'Me.txtRemark.ReadOnly = IsReadOnly
        Me.chkProduct.ReadOnly = IsReadOnly
    End Sub
    Private Sub btnAddGon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddGon.Click
        Try
            'CHECK dataset with table gon if there's been changed by user/system
            If Me.HasChangedGONData() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    If Not Me.SaveData() Then : Me.IsloadingRow = False : Return : End If
                End If
            End If

            If DS.Tables.Contains("GON_DETAIL_INFO") Then
                Me.DS.Tables("GON_DETAIL_INFO").RejectChanges()
            End If
            Me.IsloadingRow = True
            Me.Cursor = Cursors.WaitCursor
            Me.ClearGonData() : Me.btnAddGon.Enabled = True
            'Me.btnSave.Enabled = True
            If Not Me.isValidSPPB() Then : Return : End If
            If Me.grdSPPB.DataSource Is Nothing Then : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Me.ShowMessageInfo("Please enter SPPB before GON") : Return : End If
            If Me.grdSPPB.RecordCount <= 0 Then : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Me.ShowMessageInfo("Please enter SPPB before GON") : Return : End If

            Me.UnabledEntryGON(False)
            ' set min date
            Dim SPPB_NO As String = Me.txtSPPBNO.Text.TrimStart().TrimEnd() ' CType(Me.grdSPPB.DataSource, DataView)(0)("SPPB_NO").ToString()
            Dim PO_REF_NO As String = Me.mcbOA_REF_NO.Text.TrimStart().TrimEnd()
            ''if not exists SPPB in database minDate = SPPBdate
            Dim ObjMinDate As Object = Nothing
            If Me.clsSPPB.IsExistsSPPBNO(Me.txtSPPBNO.Text.TrimStart().TrimEnd(), False) Then
                ''check date terakhir yang di input
                Dim MaxGonDate As Object = Me.clsSPPB.GetlastGONDate(Me.txtSPPBNO.Text.TrimStart().TrimEnd(), False)
                If IsNothing(MaxGonDate) Or TypeOf (MaxGonDate) Is DBNull Then
                    ObjMinDate = Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString())
                Else
                    ObjMinDate = Convert.ToDateTime(MaxGonDate)
                End If
                'ObjMinDate = Me.clsSPPB.getMinDate(PO_REF_NO, SPPB_NO, False)
            Else
                ObjMinDate = Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString())
            End If
            If IsNothing(ObjMinDate) Or IsDBNull(ObjMinDate) Then
                Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Me.ShowMessageError("can not find SPPB or PONumber" & vbCrLf & "It must have been deleted by other user") : Return
            End If
            Me.dtPicGONDate.MinDate = Convert.ToDateTime(ObjMinDate)
            'Me.dtPicGONDate.MaxDate = NufarmBussinesRules.SharedClass.ServerDate.AddDays(3)
            ''get dvTransporter
            If IsNothing(Me.DVTransporter) Then
                Me.DVTransporter = Me.clsSPPB.getTransporter("", SaveMode.Insert, False).DefaultView()
            ElseIf Me.DVTransporter.Count <= 0 Then
                Me.DVTransporter = Me.clsSPPB.getTransporter("", SaveMode.Insert, False).DefaultView()
            End If
            ''BIND DvTransporter if not exists in MCB
            Me.BindMulticolumnCombo(Me.mcbTransporter, DVTransporter, "TRANSPORTER_NAME", "GT_ID")

            ''BIND DVGONArea if not exists in mcbArea
            If IsNothing(Me.DVArea) Then
                Me.DVArea = Me.clsSPPB.getAreaGon("", SaveMode.Insert, False).DefaultView()
            ElseIf Me.DVArea.Count <= 0 Then
                Me.DVArea = Me.clsSPPB.getAreaGon("", SaveMode.Insert, False).DefaultView()
            End If
            Me.BindMulticolumnCombo(Me.mcbGonArea, Me.DVArea, "AREA", "GON_ID_AREA")
            Dim DVGON As DataView = IIf(Not IsNothing(Me.grdGon.DataSource), CType(Me.grdGon.DataSource, DataView), Nothing)
            Dim ORowFilter As String = "" ' IIf(Not IsNothing(DVGON), DVGON.RowFilter, "")
            Dim tblGON As DataTable = Nothing
            If Not IsNothing(DVGON) Then
                ORowFilter = DVGON.RowFilter
                If DVGON.RowFilter <> "" Then
                    DVGON.RowFilter = ""
                End If
                tblGON = DVGON.ToTable().Copy()
            End If
            Dim tblSPPB As DataTable = CType(Me.grdSPPB.DataSource, DataView).ToTable().Copy()

            Me.DVProduct = Me.clsSPPB.GetProduct(SPPB_NO, tblGON, tblSPPB, False) : Me.BindCheckedCombo()
            ''BInd DVProduct if not exists in MCBProduct(SPPB_BRANDPACK_ID,BRANDPACK_NAME,LEFT_QTY) ''Display Member BRANDPACK_NAME,ValueMember SPPB_BRANDPACK_ID
            ''in mcbProduct
            ''get default shipto
            Dim IsReadOnly As Boolean = Me.mcbOA_REF_NO.ReadOnly
            Me.mcbOA_REF_NO.ReadOnly = False
            Me.mcbOA_REF_NO.Enabled = True
            Me.mcbOA_REF_NO.Focus()
            Me.mcbOA_REF_NO.DroppedDown = True
            Dim Address As String = Me.mcbOA_REF_NO.DropDownList().GetValue("ADDRESS")
            Me.txtShipTo.Text = Address
            Me.mcbOA_REF_NO.DroppedDown = False
            Me.mcbOA_REF_NO.ReadOnly = IsReadOnly
            If Me.IsHOUser Or Me.IsSystemAdmin Then
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
            End If
            Me.txtGONNO.Focus() : Me.txtGONNO.SelectAll()
            Me.isNewGON = True
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.IsloadingRow = False
            Me.LogMyEvent(ex.Message, Me.Name + "_btnAddGon_Click")
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub
    Private Sub BindCheckedCombo()
        If Not Me.IsloadingRow Then : Me.IsloadingRow = True : End If
        Dim HasLoadDataBefore As Boolean = False
        If Not IsNothing(Me.chkProduct.DropDownDataSource) Then
            If Not IsNothing(Me.chkProduct.DropDownList.Columns) Then
                If Me.chkProduct.DropDownList.Columns.Count > 0 Then
                    HasLoadDataBefore = True
                End If
            End If
        End If
        Me.chkProduct.SetDataBinding(Me.DVProduct, "")
        If IsNothing(Me.DVProduct) Then
            Me.IsloadingRow = False : Return
        ElseIf Me.DVProduct.Count <= 0 Then
            Me.IsloadingRow = False : Return
        End If
        'If Not HasLoadDataBefore Then : Me.chkProduct.DropDownList.RetrieveStructure() : End If
        'Me.chkProduct.DropDownDisplayMember = "BRANDPACK_NAME"
        'Me.chkProduct.DropDownValueMember = "SPPB_BRANDPACK_ID"

        'Me.chkProduct.DropDownList.Columns("SPPB_BRANDPACK_ID").Caption = ""
        'Me.chkProduct.DropDownList.Columns("SPPB_BRANDPACK_ID").ShowRowSelector = True
        'Me.chkProduct.DropDownList.Columns("SPPB_BRANDPACK_ID").UseHeaderSelector = True
        'Me.chkProduct.DropDownList.Columns("BRANDPACK_ID").Visible = False
        ' ''klik tombol cancel 
        'Me.chkProduct.DroppedDown() = True : Application.DoEvents()
        'System.Threading.Thread.Sleep(100)
        'Me.chkProduct_ButtonClick(Me.chkProduct, New EventArgs())
        'Application.DoEvents()
        'For Each col As Janus.Windows.GridEX.GridEXColumn In Me.chkProduct.DropDownList.Columns
        '    If col.Type Is Type.GetType("System.Decimal") Then
        '        col.FormatString = "#,##0.000" : col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far : col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        '    End If
        '    col.AutoSize()
        'Next
        Me.chkProduct.UncheckAll()
        'Me.chkProduct.DroppedDown = False : Application.DoEvents()

    End Sub
    Private Sub BindMulticolumnCombo(ByVal MCB As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal DV As DataView, ByVal DisplayMember As String, ByVal ValueMember As String)
        If Not Me.IsloadingRow Then : Me.IsloadingRow = True : End If
        Dim HasLoadDataBefore As Boolean = False
        If Not IsNothing(MCB.DataSource) Then
            If Not IsNothing(MCB.DropDownList.Columns) Then
                If MCB.DropDownList.Columns.Count > 0 Then
                    HasLoadDataBefore = True
                End If
            End If
        End If
        MCB.DataSource = DV
        If DV Is Nothing Then
            Me.IsloadingRow = False : Return
        ElseIf DV.Count <= 0 Then
            Me.IsloadingRow = False : Return
        End If

        If Not HasLoadDataBefore Then : MCB.DropDownList.RetrieveStructure()
            If MCB.Name = "mcbOA_REF_NO" Then
                MCB.DropDownList.CurrentTable.Columns("ADDRESS").Visible = False
            End If
        End If

        MCB.DisplayMember = DisplayMember
        MCB.ValueMember = ValueMember
        MCB.DroppedDown = True
        MCB.SelectAll()
        System.Threading.Thread.Sleep(100)
        Application.DoEvents()

        MCB.DroppedDown = False
        Application.DoEvents()
        If MCB.Name = "mcbOA_REF_NO" Then
            If Me.mcbOA_REF_NO.DropDownList.RecordCount > 0 Then
                Me.mcbOA_REF_NO.DropDownList.CurrentTable.Columns("PO_DATE").FormatString = "dd MMMM yyyy"
            End If
        End If
        'System.Threading.Thread.Sleep(100)
        'format data in MCB
        'MCB.Text = ""
    End Sub
    Private Sub BtnFindTransporter_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFindTransporter.btnClick
        If Me.mcbTransporter.ReadOnly Then : Return : End If
        If Not Me.mcbTransporter.Enabled Then : Return : End If
        Try

            Me.Cursor = Cursors.WaitCursor
            Dim tblTransPorter As DataTable = Me.clsSPPB.getTransporter(Me.mcbTransporter.Text.TrimStart().TrimEnd(), SaveMode.Insert, True)
            Me.IsloadingRow = True
            Me.BindMulticolumnCombo(Me.mcbTransporter, tblTransPorter.DefaultView(), "TRANSPORTER_NAME", "GT_ID")
            Me.ShowMessageInfo(Me.mcbTransporter.DropDownList.RecordCount().ToString() & " item(s) found")
            Me.Cursor = Cursors.Default
            Me.IsloadingRow = False
        Catch ex As Exception
            Me.IsloadingRow = False
            Me.LogMyEvent(ex.Message, Me.Name + "_BtnFindTransporter_btnClick")
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFindGonArea_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindGonArea.btnClick
        If Me.mcbGonArea.ReadOnly Then : Return : End If
        If Not Me.mcbGonArea.Enabled Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.IsloadingRow = True
            Dim tblGONArea As DataTable = Me.clsSPPB.getAreaGon(Me.mcbGonArea.Text.TrimStart().TrimEnd(), SaveMode.Insert, True)
            Me.BindMulticolumnCombo(Me.mcbGonArea, tblGONArea.DefaultView(), "AREA", "GON_ID_AREA")
            Me.ShowMessageInfo(Me.mcbGonArea.DropDownList.RecordCount().ToString() & " item(s) found")
            Me.Cursor = Cursors.Default
            Me.IsloadingRow = False
        Catch ex As Exception
            Me.IsloadingRow = False
            Me.LogMyEvent(ex.Message, Me.Name + "_BtnFindTransporter_btnClick")
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grpProductDetail_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpProductDetail.Enter
        ''chek validity
        If Not Me.HasLoadedForm Then : Return : End If
        If Me.mcbOA_REF_NO.Text = "" Then : Me.baseTooltip.BackColor = Me.BackColor
            Me.baseTooltip.Show("Please define PO_REF_NO", Me.mcbOA_REF_NO, 2500) : Me.mcbOA_REF_NO.Focus() : Return : End If
    End Sub

    Private Sub txtSPPBNO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSPPBNO.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.btnReloadSPPBProduct_Click(Me.btnReloadSPPBProduct, New EventArgs())
        End If
    End Sub
    Private Function HasChangedGONHeader() As Boolean
        With Me.OGONHeader
            If Not IsNothing(.GON_DATE) And Not IsDBNull(.GON_DATE) Then
                If CDate(.GON_DATE).ToShortDateString() <> Me.dtPicGONDate.Value.ToShortDateString() Then
                    Return True
                End If
                If .GON_ID_AREA <> Me.mcbGonArea.Value.ToString() Then
                    Return True
                End If
                If .GT_ID <> Me.mcbTransporter.Value.ToString() Then
                    Return True
                End If
                If .WarhouseCode <> Me.cmdWarhouse.SelectedValue.ToString() Then
                    Return True
                End If
                If .PoliceNoTrans <> Me.txtPolice_no_Trans.Text.Trim() Then
                    Return True
                End If
                If .DriverTrans <> Me.txtDriverTrans.Text.Trim() Then
                    Return True
                End If
                If .ShipTo <> Me.txtShipTo.Text.Trim().Replace(vbCrLf, " ").ToUpper() Then
                End If
            End If
        End With
        Return False
    End Function
    Private Function CreateGONHeader() As Nufarm.Domain.GONHeader
        Dim ObjGONHeader As New Nufarm.Domain.GONHeader()
        With ObjGONHeader
            .SPPBNO = Me.txtSPPBNO.Text.TrimStart().TrimEnd()
            .GON_NO = Me.txtGONNO.Text.TrimStart().TrimEnd()
            .CodeApp = .SPPBNO & "|" & .GON_NO
            .GON_DATE = Convert.ToDateTime(Me.dtPicGONDate.Value.ToShortDateString())
            .GON_ID_AREA = Me.mcbGonArea.Value.ToString()
            .GT_ID = Me.mcbTransporter.Value.ToString()
            '.DescriptionApp = Me.txtRemark.Text.TrimStart().TrimEnd()
            .CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
            .ModifiedBy = NufarmBussinesRules.User.UserLogin.UserName
            .PoliceNoTrans = Me.txtPolice_no_Trans.Text.Trim().ToUpper()
            .DriverTrans = Me.txtDriverTrans.Text.Trim().ToUpper()
            .WarhouseCode = Me.cmdWarhouse.SelectedValue
            .ShipTo = Me.txtShipTo.Text.Trim().Replace(vbCrLf, " ").ToUpper()
        End With
        Return ObjGONHeader
    End Function
    Private Function getGOnHeader() As NuFarm.Domain.GONHeader
        Dim ObjGONHeader As New NuFarm.Domain.GONHeader()
        With ObjGONHeader
            .GON_DATE = Convert.ToDateTime(Me.dtPicGONDate.Value.ToShortDateString())
            .GON_ID_AREA = Me.mcbGonArea.Value.ToString()
            .GON_NO = Me.OGONHeader.GON_NO
            .SPPBNO = Me.txtSPPBNO.Text.TrimStart().TrimEnd()
            .CodeApp = .SPPBNO & "|" & .GON_NO
            .GT_ID = Me.mcbTransporter.Value.ToString()
            '.DescriptionApp = Me.txtRemark.Text.TrimStart().TrimEnd()
            .CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
            .ModifiedBy = NufarmBussinesRules.User.UserLogin.UserName
            .PoliceNoTrans = Me.txtPolice_no_Trans.Text.Trim().ToUpper()
            .DriverTrans = Me.txtDriverTrans.Text.Trim().ToUpper()
            .WarhouseCode = Me.cmdWarhouse.SelectedValue
            .ShipTo = Me.txtShipTo.Text.Trim().Replace(vbCrLf, " ").ToUpper()
        End With
        Return ObjGONHeader
    End Function
    Private Function SaveData() As Boolean
        'save any changes to database
        Dim NewDS As New DataSet("DSSPPB_GON") : NewDS.Clear()
        If Not IsNothing(Me.grdGon.DataSource) Then
            If Me.grdGon.RecordCount > 0 Then
                Me.grdGon.UpdateData()
            End If
        End If
        If Not IsNothing(Me.grdSPPB.DataSource) Then
            If Me.grdSPPB.RecordCount > 0 Then
                Me.grdSPPB.UpdateData()
            End If
        End If
        Dim SuccessSaving As Boolean = False
        Dim ChangedGON As Boolean = Me.HasChangedGONData()
        Dim ChangedSPPB As Boolean = Me.HasChangedSPPBData()
        Dim objSPPBHeader As NuFarm.Domain.SPPBHeader = Nothing
        Dim ObjGONHeader As Nufarm.Domain.GONHeader = Nothing
        If ChangedGON = False And ChangedSPPB = False Then
            If Me.HasChangedGONHeader() Then
                ''insert / update gon
                Dim GonHeader As Nufarm.Domain.GONHeader = Me.CreateGONHeader()
                Me.clsSPPB.SaveOrUpdateGON(GonHeader)
                Me.OGONHeader = GonHeader
                Me.DS = NewDS : Me.frmParent.MustReloadData = True
                isNewGON = False
                Return True
            Else
                Return False
            End If
        End If
        If ChangedGON Or ChangedSPPB Then
            objSPPBHeader = New NuFarm.Domain.SPPBHeader()
            With objSPPBHeader
                .SPPBNO = Me.txtSPPBNO.Text.TrimStart().TrimEnd()
                .PONumber = Me.mcbOA_REF_NO.Value.ToString()
                .SPPBDate = Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString())
                .SPPBShipTo = Me.txtShipTo.Text.Trim().Replace(vbCrLf, " ")
            End With
            If ChangedGON Then
                ''SET GON_HEADER
                If Me.isNewGON Then
                    ObjGONHeader = Me.CreateGONHeader()
                ElseIf Me.Mode = SaveMode.Update Then
                    If Not IsNothing(Me.OGONHeader) Then
                        If Not String.IsNullOrEmpty(Me.OGONHeader.GON_NO) Then
                            ObjGONHeader = Me.getGOnHeader()
                        Else
                            Me.ShowMessageInfo("GON Header can not be found " & vbCrLf & "Please contact system administrator") : Return False
                        End If
                    Else
                        ObjGONHeader = Me.CreateGONHeader()
                    End If
                Else
                    If Not IsNothing(Me.OGONHeader) Then
                        If Not String.IsNullOrEmpty(Me.OGONHeader.GON_NO) Then
                            ObjGONHeader = Me.getGOnHeader()
                        Else
                            ObjGONHeader = Me.CreateGONHeader()
                        End If
                    Else
                        ObjGONHeader = Me.CreateGONHeader()
                    End If
                End If
            End If
            'check if grid gon has a value of -1
            If Not IsNothing(Me.grdGon.DataSource) Then
                If Me.grdGon.RecordCount > 0 Then
                    Dim DV As DataView = CType(Me.grdGon.DataSource, DataView)
                    Dim OrowFilter As String = DV.RowFilter
                    If DV.RowFilter <> "" Then
                        OrowFilter = DV.RowFilter
                    End If
                    DV.RowFilter = ""
                    Dim DummyDV As DataView = DV.ToTable().Copy().DefaultView()
                    For Each Dr As DataRowView In DV
                        If IsNothing(Dr("GON_QTY")) Then
                            Me.ShowMessageInfo("Please enter a valid value of GON_QTY")
                            DV.RowFilter = OrowFilter
                            Me.Cursor = Cursors.Default : Me.IsloadingRow = False : Return False
                        ElseIf IsDBNull(Dr("GON_QTY")) Then
                            Me.ShowMessageInfo("Please enter a valid value of GON_QTY")
                            DV.RowFilter = OrowFilter
                            Me.Cursor = Cursors.Default : Me.IsloadingRow = False : Return False
                        ElseIf Convert.ToDecimal(Dr("GON_QTY")) < 0 Then
                            Me.ShowMessageInfo("Please enter a valid value of GON_QTY")
                            DV.RowFilter = OrowFilter
                            Me.Cursor = Cursors.Default : Me.IsloadingRow = False : Return False
                        End If
                    Next
                End If
            End If
            SuccessSaving = Me.clsSPPB.SaveDataSPPBGON(Me.DS, NewDS, objSPPBHeader, ObjGONHeader, (DataToEdit = EditData.GON), True)
        ElseIf Me.Mode = SaveMode.Update Then
            If Me.DataToEdit = EditData.GON Then
                If Not IsNothing(Me.OGONHeader) Then
                    If Not String.IsNullOrEmpty(Me.OGONHeader.GON_NO) Then
                        ObjGONHeader = Me.getGOnHeader()
                    Else
                        Me.ShowMessageInfo("GON Header can not be found " & vbCrLf & "Please contact system administrator") : Return False
                    End If
                Else
                    ObjGONHeader = Me.CreateGONHeader()
                End If
                If Not IsNothing(Me.grdGon.DataSource) Then
                    If Me.grdGon.RecordCount > 0 Then
                        Dim DV As DataView = CType(Me.grdGon.DataSource, DataView)
                        Dim OrowFilter As String = DV.RowFilter
                        If DV.RowFilter <> "" Then
                            OrowFilter = DV.RowFilter
                        End If
                        DV.RowFilter = ""
                        Dim DummyDV As DataView = DV.ToTable().Copy().DefaultView()
                        For Each Dr As DataRowView In DV
                            If IsNothing(Dr("GON_QTY")) Then
                                Me.ShowMessageInfo("Please enter a valid value of GON_QTY")
                                DV.RowFilter = OrowFilter
                                Me.Cursor = Cursors.Default : Me.IsloadingRow = False : Return False
                            ElseIf IsDBNull(Dr("GON_QTY")) Then
                                Me.ShowMessageInfo("Please enter a valid value of GON_QTY")
                                DV.RowFilter = OrowFilter
                                Me.Cursor = Cursors.Default : Me.IsloadingRow = False : Return False
                            ElseIf Convert.ToDecimal(Dr("GON_QTY")) < 0 Then
                                Me.ShowMessageInfo("Please enter a valid value of GON_QTY")
                                DV.RowFilter = OrowFilter
                                Me.Cursor = Cursors.Default : Me.IsloadingRow = False : Return False
                            End If
                        Next
                    End If
                End If
                objSPPBHeader = New Nufarm.Domain.SPPBHeader()
                With objSPPBHeader
                    .SPPBNO = Me.txtSPPBNO.Text.TrimStart().TrimEnd()
                    .PONumber = Me.mcbOA_REF_NO.Value.ToString()
                    .SPPBDate = Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString())
                    .SPPBShipTo = Me.txtShipTo.Text.Trim().Replace(vbCrLf, " ")
                End With
                SuccessSaving = Me.clsSPPB.SaveDataSPPBGON(Me.DS, NewDS, objSPPBHeader, ObjGONHeader, (DataToEdit = EditData.GON), True)
            End If
        Else
            Me.Cursor = Cursors.Default : Return False
        End If
        If SuccessSaving Then : Me.DS = NewDS : Me.frmParent.MustReloadData = True : Me.OGONHeader = ObjGONHeader : Me.btnPrint.Enabled = False : End If
        Me.DS.AcceptChanges()
        Me.isNewGON = False
        Return True
    End Function
    Private Function HasChangedGONData() As Boolean
        If Not Me.IsloadingRow Then : Me.IsloadingRow = True : End If
        'Dim DVGON As DataView = CType(Me.grdGon.DataSource, DataView)
        'Dim ORowFilter As String = DVGON.RowFilter
        'If DVGON.RowFilter <> "" Then
        '    DVGON.RowFilter = ""
        'End If
        If Not IsNothing(Me.DS) Then
            If Me.DS.Tables.Contains("GON_DETAIL_INFO") Then
                Dim tblGON As DataTable = Me.DS.Tables("GON_DETAIL_INFO")
                Dim InsertedRows() As DataRow = tblGON.Select("", "", Data.DataViewRowState.Added)
                Dim UpdatedRows() As DataRow = tblGON.Select("", "", Data.DataViewRowState.ModifiedOriginal)
                Dim DeletedRows() As DataRow = tblGON.Select("", "", Data.DataViewRowState.Deleted)
                If InsertedRows.Length > 0 Then : Return True : End If
                If UpdatedRows.Length > 0 Then : Return True : End If
                If DeletedRows.Length > 0 Then : Return True : End If
                If Me.Mode = SaveMode.Insert Then
                    If Not Me.HasSavedGON Then
                        InsertedRows = tblGON.Select("", "", Data.DataViewRowState.Unchanged)
                        If InsertedRows.Length > 0 Then
                            For Each row As DataRow In InsertedRows
                                row.SetAdded()
                            Next
                            Return True
                        End If
                    End If
                End If
            End If
        End If
        Return False
    End Function
    Private Function HasChangedSPPBData() As Boolean
        Dim InsertedRows() As DataRow = Nothing
        If Not IsNothing(Me.DS) Then
            If Me.DS.Tables.Contains("SPPB_BRANDPACK_INFO") Then
                Dim tblSPPB As DataTable = Me.DS.Tables("SPPB_BRANDPACK_INFO")
                InsertedRows = tblSPPB.Select("", "", Data.DataViewRowState.Added)
                Dim UpdatedRows() As DataRow = tblSPPB.Select("", "", Data.DataViewRowState.ModifiedOriginal)
                Dim DeletedRows() As DataRow = tblSPPB.Select("", "", Data.DataViewRowState.Deleted)
                If InsertedRows.Length > 0 Then : Return True : End If
                If UpdatedRows.Length > 0 Then : Return True : End If
                If DeletedRows.Length > 0 Then : Return True : End If
                If Me.Mode = SaveMode.Insert Then
                    If Not Me.HasSavedSPPB Then
                        InsertedRows = tblSPPB.Select("", "", Data.DataViewRowState.Unchanged)
                        If InsertedRows.Length > 0 Then
                            For Each row As DataRow In InsertedRows
                                row.SetAdded()
                            Next
                            Return True
                        End If
                    End If
                End If
            End If


        End If
        Return False
    End Function
    Private Function CreateOrReCreateDataTable(ByVal ctrlName As String, ByVal Status As String) As DataTable
        Dim colSPPBBrandpackID As New DataColumn("SPPB_BRANDPACK_ID", Type.GetType("System.String"))
        Dim colBrandPackName As New DataColumn("BRANDPACK_NAME", Type.GetType("System.String"))
        Dim colStatus As New DataColumn("STATUS", Type.GetType("System.String"))
        Dim tblStatus As New DataTable("T_Status") : tblStatus.Clear()
        tblStatus.Columns.AddRange(New DataColumn() {colSPPBBrandpackID, colBrandPackName, colStatus})
        'isi data
        Dim row As DataRow = Nothing
        If ctrlName = "grdSPPB" Then
            row = tblStatus.NewRow()
            row.BeginEdit()
            row("SPPB_BRANDPACK_ID") = Me.grdSPPB.GetValue("SPPB_BRANDPACK_ID").ToString()
            row("BRANDPACK_NAME") = Me.grdSPPB.GetValue("BRANDPACK_NAME").ToString()
            row("STATUS") = Status
            row.EndEdit()
            tblStatus.Rows.Add(row)
            tblStatus.AcceptChanges()
        ElseIf ctrlName = "cmbStatusSPPB" Then
            For Each Jrow As Janus.Windows.GridEX.GridEXRow In Me.chkProduct.DropDownList.GetCheckedRows()
                row = tblStatus.NewRow()
                row.BeginEdit()
                row("SPPB_BRANDPACK_ID") = Jrow.Cells("SPPB_BRANDPACK_ID").Value.ToString()
                row("BRANDPACK_NAME") = Jrow.Cells("BRANDPACK_NAME").Value.ToString()
                row("STATUS") = Status
                row.EndEdit()
                tblStatus.Rows.Add(row)
            Next
            tblStatus.AcceptChanges()
        End If
        Return tblStatus
    End Function
    Private Sub grdGon_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdGon.CellUpdated
        If Me.grdGon.DataSource Is Nothing Then : Return : End If
        If Me.grdGon.RecordCount <= 0 Then : Return : End If
        If Me.IsloadingRow Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim SPPBBrandPackID As String = Me.grdGon.GetValue("SPPB_BRANDPACK_ID").ToString()
            Me.IsloadingRow = True
            If e.Column.Key = "GON_QTY" Then
                'chek ke database
              
                If IsNothing(Me.grdGon.GetValue(e.Column)) Or TypeOf Me.grdGon.GetValue(e.Column) Is DBNull Then
                    Me.ShowMessageInfo("Can not set null value on gon_qty") : Me.grdGon.CancelCurrentEdit() : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                End If
                'get sppbQTy
                Dim DVDummySPPB As DataView = CType(Me.grdSPPB.DataSource, DataView).ToTable().Copy().DefaultView()
                DVDummySPPB.Sort = "SPPB_BRANDPACK_ID"
                Dim index As Integer = DVDummySPPB.Find(SPPBBrandPackID)
                Dim SPPBQTy As Decimal = Convert.ToDecimal(DVDummySPPB(index)("SPPB_QTY"))
                If SPPBQTy <= 0 Then
                    Me.ShowMessageInfo("can not insert the value when SPPB_QTY is equal with zero") : Me.grdGon.CancelCurrentEdit() : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                End If

                Dim StatusSPPB As String = IIf((Not IsNothing(DVDummySPPB(index)("STATUS")) And Not IsDBNull(DVDummySPPB(index)("STATUS"))), DVDummySPPB(index)("STATUS").ToString(), "")
                Dim tblDummyGON As DataTable = Me.DS.Tables("GON_DETAIL_INFO").Copy()
                'Dim GONHeaderID As String = Me.grdGon.GetValue("GON_HEADER_ID").ToString()
                Dim DVDummyGOn As DataView = tblDummyGON.DefaultView()
                DVDummyGOn.RowStateFilter = Data.DataViewRowState.CurrentRows
                DVDummyGOn.RowFilter = ""

                Dim NGOnQTY As Decimal = Convert.ToDecimal(Me.grdGon.GetValue(e.Column))
                Dim NTotalGONQTy As Decimal = Me.clsSPPB.GetTotalGONQTY(SPPBBrandPackID, True, DVDummyGOn.ToTable())
                Dim SPPBrandPackID As String = Me.grdGon.GetValue("SPPB_BRANDPACK_ID").ToString()
                Dim GOnDetailID As String = Me.grdGon.GetValue("GON_DETAIL_ID").ToString()
                Dim ObjTotalGON As Object = DVDummyGOn.ToTable().Compute("SUM(GON_QTY)", "SPPB_BRANDPACK_ID = '" & SPPBrandPackID & "' AND GON_DETAIL_ID <> '" & GOnDetailID & "'")
                If Not IsNothing(ObjTotalGON) And Not IsDBNull(ObjTotalGON) Then
                    NTotalGONQTy += Convert.ToDecimal(ObjTotalGON)
                End If
                If (NTotalGONQTy + NGOnQTY) > SPPBQTy Then
                    Me.ShowMessageInfo("Invalid quantity" & vbCrLf & String.Format("TotalGON will be {0:#,##0.000}", (NTotalGONQTy + NGOnQTY)) & vbCrLf & String.Format("Value GON you enter is {0:#,##0.000}", NGOnQTY) _
                    & vbCrLf & String.Format("you can only enter value less than or equal {0:#,##0.000}", SPPBQTy - NTotalGONQTy))
                    Me.grdGon.CancelCurrentEdit() : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                End If
                ''check total GON dan status

                Dim BFind As Boolean = Me.grdSPPB.Find(Me.grdSPPB.RootTable.Columns("SPPB_BRANDPACK_ID"), Janus.Windows.GridEX.ConditionOperator.Equal, SPPBBrandPackID, -1, 1)
                If (NTotalGONQTy + NGOnQTY) = SPPBQTy Then
                    ''reset status jika status buka shipped
                    If String.IsNullOrEmpty(StatusSPPB) Or StatusSPPB <> "SHIPPED" Then
                        If BFind Then : Me.grdSPPB.SetValue("STATUS", "SHIPPED") : End If
                    End If
                ElseIf (NTotalGONQTy + NGOnQTY) > 0 And ((NTotalGONQTy + NGOnQTY) < SPPBQTy) Then
                    If String.IsNullOrEmpty(StatusSPPB) Or (StatusSPPB = "--NEW SPPB--" Or StatusSPPB = "SHIPPED") Then
                        Me.grdSPPB.Focus()
                        'BFind = Me.grdSPPB.Find(Me.grdSPPB.RootTable.Columns("SPPB_BRANDPACK_ID"), Janus.Windows.GridEX.ConditionOperator.Equal, SPPBBrandPackID, -1, 1)
                        Me.ShowMessageInfo("Please reset status sppb based on Qty value you've just entered")
                    End If
                ElseIf (NTotalGONQTy + NGOnQTY) <= 0 Then
                    If String.IsNullOrEmpty(StatusSPPB) Or StatusSPPB <> "--NEW SPPB--" Then
                        'BFind = Me.grdSPPB.Find(Me.grdSPPB.RootTable.Columns("SPPB_BRANDPACK_ID"), Janus.Windows.GridEX.ConditionOperator.Equal, SPPBBrandPackID, -1, 1)
                        If BFind Then : Me.grdSPPB.SetValue("STATUS", "--NEW SPPB--") : End If
                    End If
                End If
                If BFind Then
                    If Me.Mode = SaveMode.Update Then
                        Me.grdSPPB.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                        Me.grdSPPB.SetValue("MODIFY_DATE", NufarmBussinesRules.SharedClass.ServerDate)
                        Me.grdSPPB.SetValue("TOTAL_GON_QTY", (NTotalGONQTy + NGOnQTY))
                        Me.grdGon.SetValue("ModifiedBy", NufarmBussinesRules.User.UserLogin.UserName)
                        Me.grdGon.SetValue("ModifiedDate", NufarmBussinesRules.SharedClass.ServerDate)
                    End If
                End If

            End If
            grdGon.UpdateData() : Me.grdSPPB.UpdateData()
            If Not Me.btnSave.Enabled Then : Me.btnSave.Enabled = (Me.HasChangedGONData() Or Me.HasChangedSPPBData()) : End If
            Me.IsloadingRow = False : Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.grdGon.CancelCurrentEdit()
            Me.IsloadingRow = False
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdSPPB_CellUpdated")
            'Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSPPB_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSPPB.CurrentCellChanged
        If IsNothing(Me.grdSPPB.DataSource) Then : Return : End If
        If IsloadingRow Then : Return : End If
        If IsNothing(Me.grdGon.SelectedItems) Then : Return : End If
        'If Me.grdSPPB.GetRow().RowType <> Janus.Windows.GridEX.RowType.Record Then : Return : End If
        ''check data bila sudah ada GON
        Me.Cursor = Cursors.WaitCursor
        Try
            If Not Me.grdSPPB.GetRow().RowType = Janus.Windows.GridEX.RowType.Record And Not Me.grdSPPB.GetRow().RowType = Janus.Windows.GridEX.RowType.FilterRow Then : Me.Cursor = Cursors.Default : Return : End If
            If Me.grdSPPB.GetRow().RowType = Janus.Windows.GridEX.RowType.FilterRow Then
                'clear grdgon filter
                If Me.DS.Tables.Contains("GON_DETAIL_INFO") Then
                    If Not IsNothing(Me.grdGon.DataSource) Then
                        CType(Me.grdGon.DataSource, DataView).RowFilter = ""
                    End If
                End If
            Else
                Dim SPPBBrandpackID = Me.grdSPPB.GetValue("SPPB_BRANDPACK_ID").ToString()
                If Me.DS.Tables.Contains("GON_DETAIL_INFO") Then
                    If Not IsNothing(Me.grdGon.DataSource) Then
                        CType(Me.grdGon.DataSource, DataView).RowFilter = "SPPB_BRANDPACK_ID = '" & SPPBBrandpackID & "'"
                    End If
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_grdSPPB_CurrentCellChanged")
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSPPB_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdSPPB.DeletingRecord
        'Dim SuccesDelete As Boolean = False
        Try
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True : Return
            End If
            'delete data gon
            Me.Cursor = Cursors.WaitCursor
            Me.IsloadingRow = True
            Dim SPPBBrandPackID As String = Me.grdSPPB.GetValue("SPPB_BRANDPACK_ID").ToString()
            Me.grdGon.RemoveFilters()
            Dim DVGON As DataView = CType(Me.grdGon.DataSource, DataView)
            Dim OSort As String = DVGON.Sort
            If OSort <> "GON_DETAIL_ID" Then : DVGON.Sort = "GON_DETAIL_ID" : End If
            Dim DVDummyGON As DataView = DVGON.ToTable().Copy().DefaultView()
            'DVDummyGON.Sort = "GON_DETAIL_ID"
            DVDummyGON.RowFilter = "SPPB_BRANDPACK_ID = '" & SPPBBrandPackID & "'"
            If DVDummyGON.Count > 0 Then
                For i As Integer = 0 To DVDummyGON.Count - 1
                    Dim Index As Integer = DVGON.Find(DVDummyGON(Index)("GON_DETAIL_ID"))
                    If Index <> -1 Then
                        DVGON.Delete(Index) ': SuccesDelete = True
                    End If
                Next
            End If
            Me.grdGon.UpdateData()
            Dim DV As DataView = Nothing
            Dim ORowFilter As String = ""
            If Not IsNothing(Me.grdGon.DataSource) Then
                DV = CType(Me.grdGon.DataSource, DataView)
                ORowFilter = DV.RowFilter
                DV.RowFilter = ""
                If Me.grdGon.RecordCount <= 0 Then
                    Me.ClearGonData()
                    Me.dtPicSPPBDate.ReadOnly = False
                Else

                    DV.RowFilter = ""
                    Dim DummyDV As DataView = DV.ToTable().Copy().DefaultView()
                    Dim SPPBrandPackIDs As String = ""
                    Dim sppbBrandpackIDs(Me.grdGon.RecordCount - 1) As Object
                    For i As Integer = 0 To DummyDV.Count - 1
                        sppbBrandpackIDs(i) = DummyDV(i)("SPPB_BRANDPACK_ID").ToString()
                    Next
                    Me.chkProduct.CheckedValues = sppbBrandpackIDs
                    DV.RowFilter = ORowFilter
                End If
            End If
            If ORowFilter <> "" Then
                CType(Me.grdGon.DataSource, DataView).RowFilter = ORowFilter
            End If
            e.Cancel = False
            Me.grdSPPB.UpdateData()
            If Me.grdSPPB.RecordCount <= 0 Then
                Me.txtSPPBNO.ReadOnly = False : Me.dtPicSPPBDate.ReadOnly = False
            Else
                Me.txtSPPBNO.ReadOnly = True : Me.dtPicSPPBDate.ReadOnly = True
            End If
            Me.grdSPPB.Update()
            Me.UnabledEntryGON(True)
            Me.IsloadingRow = False
            'Me.btnSave.Enabled = Me.HasChangedSPPBData()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            e.Cancel = True
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdSPPB_DeletingRecord")
        End Try
    End Sub

    Private Sub grdSPPB_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSPPB.Enter
        If IsloadingRow Then : Return : End If
        If Not Me.isValidSPPB() Then : Return : End If
    End Sub

    Private Sub cmbStatusSPPB_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatusSPPB.SelectedValueChanged
        If Me.cmbStatusSPPB.Text = "" Then : Return : End If
        If Me.cmbStatusSPPB.SelectedValue Is Nothing Then : Return : End If
        If Me.IsloadingRow Then : Return : End If
        If Not Me.HasLoadedForm Then : Return : End If
        If Me.isValidGON(CType(Me.cmbStatusSPPB, Control), False) = False Then
            Me.IsloadingRow = False : Return
        End If
        Me.Cursor = Cursors.WaitCursor
        Me.IsloadingRow = True
        Try
            If cmbStatusSPPB.Text = "OTHER" Then
                ''MUNCULKAN FORM SetStatus
                If IsNothing(Me.frmSetstat) OrElse Me.frmSetstat.IsDisposed() Then
                    Me.frmSetstat = New FrmSetOtherStatus()
                End If
                With frmSetstat
                    .FrmParent = Me
                    ''bikin datatable 
                    Dim tblStatus As DataTable = Me.CreateOrReCreateDataTable("cmbStatusSPPB", "")
                    If tblStatus.Rows.Count <= 0 Then
                        MessageBox.Show("Unknown error", "", MessageBoxButtons.OK, MessageBoxIcon.Error) : Me.Cursor = Cursors.Default : Return
                    End If
                    .tblStatus = tblStatus
                    .RefControl = FrmSetOtherStatus.ReferencedControl.Combo
                    .TipPosition = DevComponents.DotNetBar.eTipPosition.Top
                    .Owner = Me
                    .TopLevel = True
                    .Show(Me.cmbStatusSPPB) : Me.Cursor = Cursors.Default : Return
                End With
            End If
            'BIND DATA to Grid GON
            'BIND update status grid SPPB
            'find SPPB_BRANDPACK_ID
            Dim ORowFilter As String = "", OSort As String = "SPPB_BRANDPACK_ID"
            Dim DVSPPB As DataView = CType(Me.grdSPPB.DataSource, DataView)
            If DVSPPB.RowFilter <> "" Then
                ORowFilter = DVSPPB.RowFilter
            End If
            If DVSPPB.Sort <> OSort Then
                OSort = DVSPPB.Sort
                DVSPPB.Sort = "SPPB_BRANDPACK_ID"
            End If
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.chkProduct.DropDownList.GetCheckedRows()
                Dim SPPBBrandPackID As String = row.Cells("SPPB_BRANDPACK_ID").Value.ToString()
                Dim Index As Integer = DVSPPB.Find(SPPBBrandPackID)
                If Index <> -1 Then
                    DVSPPB(Index).BeginEdit()
                    DVSPPB(Index)("STATUS") = Me.cmbStatusSPPB.Text.TrimStart().TrimEnd()
                    If Me.Mode = SaveMode.Update Then
                        DVSPPB(Index)("MODIFY_BY") = NufarmBussinesRules.User.UserLogin.UserName
                        DVSPPB(Index)("MODIFY_DATE") = NufarmBussinesRules.SharedClass.ServerDate
                    End If
                    DVSPPB(Index)("IsUpdatedBySystem") = False
                    DVSPPB(Index).EndEdit()
                End If
            Next
            Me.grdSPPB.UpdateData()
            If Not Me.btnSave.Enabled Then : Me.btnSave.Enabled = Me.HasChangedGONData() : End If
            'lock dtpic date
            Me.dtPicSPPBDate.ReadOnly = True
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            If Not IsNothing(Me.frmSetstat) Then
                Me.frmSetstat.Dispose() : Me.frmSetstat = Nothing
            End If
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_cmbStatusSPPB_SelectedValueChanged")
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub frmSetstat_CancelClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles frmSetstat.CancelClick
        Try
            Me.Cursor = Cursors.WaitCursor

            If Me.frmSetstat.RefControl = FrmSetOtherStatus.ReferencedControl.Combo Then
                Me.IsloadingRow = True
                Dim DVSPPB As DataView = CType(Me.grdSPPB.DataSource, DataView)
                Dim ORowFilter As String = DVSPPB.RowFilter
                If DVSPPB.Sort <> "SPPB_BRANDPACK_ID" Then
                    DVSPPB.Sort = "SPPB_BRANDPACK_ID"
                End If
                Dim OSort As String = DVSPPB.Sort
                For Each JRow As Janus.Windows.GridEX.GridEXRow In frmSetstat.GridEX1.GetRows()
                    Dim SPPBBrandpackID As String = JRow.Cells("SPPB_BRANDPACK_ID").Value.ToString()
                    Dim Index = DVSPPB.Find(SPPBBrandpackID)
                    DVSPPB(Index).BeginEdit()
                    Dim Status As String = JRow.Cells("STATUS").Text()
                    If Status = "" Then : Status = "UNKNOWN" : End If
                    Dim Remark As String = IIf((Not IsDBNull(DVSPPB(Index)("REMARK")) And Not IsNothing(DVSPPB(Index)("REMARK"))), DVSPPB(Index)("REMARK").ToString(), "")
                    If Microsoft.VisualBasic.Right(Remark.TrimStart().TrimEnd(), 1) = "," Then
                        Remark = Remark.Remove(Remark.Length - 1, 1)
                    End If
                    Remark = Remark.Replace("UNKNOWN", "") & "," & Status
                    If Microsoft.VisualBasic.Left(Remark.TrimStart().TrimEnd(), 1) = "," Then
                        Remark = Remark.Remove(0, 1)
                    End If
                    'If Microsoft.VisualBasic.Right(Remark.TrimStart().TrimEnd(), 1) = "," Then
                    '    Remark = Remark.Replace(",", "")
                    'End If
                    DVSPPB(Index)("STATUS") = "OTHER"
                    DVSPPB(Index)("REMARK") = Remark
                    DVSPPB(Index)("IsUpdatedBySystem") = True
                    If Me.Mode = SaveMode.Update Then
                        Me.grdSPPB.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                        Me.grdSPPB.SetValue("MODIFY_DATE", NufarmBussinesRules.SharedClass.ServerDate)
                    End If
                    DVSPPB(Index).EndEdit()
                Next
                Me.grdSPPB.UpdateData()
                If DVSPPB.Sort <> "SPPB_BRANDPACK_ID" Then
                    DVSPPB.Sort = OSort
                End If
                If DVSPPB.RowFilter <> "" Then
                    DVSPPB.RowFilter = ORowFilter
                End If
            ElseIf Me.frmSetstat.RefControl = FrmSetOtherStatus.ReferencedControl.Grid Then
                Dim tblStatus As DataTable = CType(frmSetstat.GridEX1.DataSource, DataTable)
                Dim Status = tblStatus.Rows(0)("STATUS").ToString()
                If Status = "" Then : Status = "UNKNOWN" : End If
                Dim Remark As String = IIf((Not IsDBNull(Me.grdSPPB.GetValue("REMARK")) And Not IsNothing(Me.grdSPPB.GetValue("REMARK"))), Me.grdSPPB.GetValue("REMARK").ToString(), "")
                If Microsoft.VisualBasic.Right(Remark.TrimStart().TrimEnd(), 1) = "," Then
                    Remark = Remark.Replace(",", "")
                End If
                Remark = Remark.Replace("UNKNOWN", "") & "," & Status
                If Microsoft.VisualBasic.Right(Remark.TrimStart().TrimEnd(), 1) = "," Then
                    Remark = Remark.Remove(Remark.Length - 1, 1)
                End If
                If Microsoft.VisualBasic.Left(Remark.TrimStart().TrimEnd(), 1) = "," Then
                    Remark = Remark.Remove(0, 1)
                End If
                Me.grdSPPB.SetValue("STATUS", "OTHER")
                Me.grdSPPB.SetValue("REMARK", Remark)
                Me.grdSPPB.SetValue("IsUpdatedBySystem", True)
                If Me.Mode = SaveMode.Update Then
                    Me.grdSPPB.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                    Me.grdSPPB.SetValue("MODIFY_DATE", NufarmBussinesRules.SharedClass.ServerDate)
                End If
                Me.grdSPPB.UpdateData()
            End If
            Me.frmSetstat.Close() : Me.frmSetstat = Nothing
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_frmSetstat_CancelClick")
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub frmSetstat_OKClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles frmSetstat.OKClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.IsloadingRow = True
            'VALIDATE DATA
            For Each JRow As Janus.Windows.GridEX.GridEXRow In frmSetstat.GridEX1.GetRows()
                Dim Status As String = JRow.Cells("STATUS").Text()
                If Status = "" Then : Me.frmSetstat.lblResult.Text = "Please enter status" : Me.frmSetstat.GridEX1.MoveTo(JRow)
                    Me.frmSetstat.GridEX1.SelectCurrentCellText() : Me.Cursor = Cursors.Default : Return
                End If
            Next
            If Me.frmSetstat.RefControl = FrmSetOtherStatus.ReferencedControl.Combo Then

                Me.IsloadingRow = True
                Dim DVSPPB As DataView = CType(Me.grdSPPB.DataSource, DataView)
                Dim ORowFilter As String = DVSPPB.RowFilter
                If DVSPPB.Sort <> "SPPB_BRANDPACK_ID" Then
                    DVSPPB.Sort = "SPPB_BRANDPACK_ID"
                End If
                Dim OSort As String = DVSPPB.Sort
                For Each JRow As Janus.Windows.GridEX.GridEXRow In frmSetstat.GridEX1.GetRows()
                    Dim SPPBBrandpackID As String = JRow.Cells("SPPB_BRANDPACK_ID").Value.ToString()
                    Dim Index = DVSPPB.Find(SPPBBrandpackID)
                    DVSPPB(Index).BeginEdit()
                    Dim Status As String = JRow.Cells("STATUS").Text()
                    Dim Remark As String = IIf((Not IsDBNull(DVSPPB(Index)("REMARK")) And Not IsNothing(DVSPPB(Index)("REMARK"))), DVSPPB(Index)("REMARK").ToString(), "")
                    If Microsoft.VisualBasic.Right(Remark.TrimStart().TrimEnd(), 1) = "," Then
                        Remark = Remark.Remove(Remark.Length - 1, 1)
                    End If
                    If Microsoft.VisualBasic.Left(Remark.TrimStart().TrimEnd(), 1) = "," Then
                        Remark = Remark.Remove(0, 1)
                    End If
                    Remark = Remark.Replace("UNKNOWN", "")
                    DVSPPB(Index)("STATUS") = "OTHER"
                    Remark = Remark & "," & Status
                    If Microsoft.VisualBasic.Left(Remark.TrimStart().TrimEnd(), 1) = "," Then
                        Remark = Remark.Remove(0, 1)
                    End If

                    DVSPPB(Index)("REMARK") = Remark
                    DVSPPB(Index)("IsUpdatedBySystem") = False
                    If Me.Mode = SaveMode.Update Then
                        Me.grdSPPB.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                        Me.grdSPPB.SetValue("MODIFY_DATE", NufarmBussinesRules.SharedClass.ServerDate)
                    End If
                    DVSPPB(Index).EndEdit()
                Next
                Me.grdSPPB.UpdateData()
                If DVSPPB.Sort <> "SPPB_BRANDPACK_ID" Then
                    DVSPPB.Sort = OSort
                End If
                If DVSPPB.RowFilter <> "" Then
                    DVSPPB.RowFilter = ORowFilter
                End If
            ElseIf Me.frmSetstat.RefControl = FrmSetOtherStatus.ReferencedControl.Grid Then
                Dim tblStatus As DataTable = CType(frmSetstat.GridEX1.DataSource, DataTable)
                Dim Status = tblStatus.Rows(0)("STATUS").ToString()
                If Status = "OTHER" Then : Return : End If
                Dim Remark As String = IIf((Not IsDBNull(Me.grdSPPB.GetValue("REMARK")) And Not IsNothing(Me.grdSPPB.GetValue("REMARK"))), Me.grdSPPB.GetValue("REMARK").ToString(), "")
                If Microsoft.VisualBasic.Right(Remark.TrimStart().TrimEnd(), 1) = "," Then
                    Remark = Remark.Replace(",", "")
                End If
                Remark = Remark.Replace("UNKNOWN", "")
                If Microsoft.VisualBasic.Right(Remark.TrimStart().TrimEnd(), 1) = "," Then
                    Remark = Remark.Remove(Remark.Length - 1, 1)
                End If
                If Microsoft.VisualBasic.Left(Remark.TrimStart().TrimEnd(), 1) = "," Then
                    Remark = Remark.Remove(0, 1)
                End If
                Remark = Remark & "," & Status
                If Microsoft.VisualBasic.Left(Remark.TrimStart().TrimEnd(), 1) = "," Then
                    Remark = Remark.Remove(0, 1)
                End If
                Me.grdSPPB.SetValue("REMARK", Remark)
                If Me.Mode = SaveMode.Update Then
                    Me.grdSPPB.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                    Me.grdSPPB.SetValue("MODIFY_DATE", NufarmBussinesRules.SharedClass.ServerDate)
                End If
                Me.grdSPPB.UpdateData()
            End If
            Me.frmSetstat.Close() : Me.frmSetstat = Nothing
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
            Me.LogMyEvent(ex.Message, Me.Name + "_frmSetstat_OKClick")
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            ''update data bila mungkin belum
            If Not Me.isValidSPPB() Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.IsloadingRow = True : If Not Me.SaveData() Then : Me.IsloadingRow = False : Cursor = Cursors.Default : Return : End If
            If Me.Mode = SaveMode.Update Then
                Me.IsloadingRow = True : Me.Close() : Me.Cursor = Cursors.Default : Return
            End If
            ''bind grid
            Me.BindGrid(Me.grdGon, Me.DS.Tables("GON_DETAIL_INFO").DefaultView)
            Me.BindGrid(Me.grdSPPB, Me.DS.Tables("SPPB_BRANDPACK_INFO").DefaultView)
            Me.IsloadingRow = True
            Dim DV As DataView = CType(Me.mcbOA_REF_NO.DataSource, DataView)

            Dim OrowFilter As String = DV.RowFilter : Dim OSort As String = DV.Sort
            DV.RowFilter = ""
            DV.Sort = "PO_REF_NO DESC "
            Dim DVDummy As DataView = DV.ToTable().Copy().DefaultView()
            DVDummy.RowFilter = ""
            DVDummy.Sort = "PO_REF_NO DESC "

            Dim PONUMber As String = Me.mcbOA_REF_NO.Value.ToString()
            Dim Index As Integer = DVDummy.Find(PONUMber)
            If Index <> -1 Then
                DVDummy.Delete(Index)
            End If
            Me.BindMulticolumnCombo(Me.mcbOA_REF_NO, DVDummy, "PO_REF_NO", "PO_REF_NO")
            Me.mcbOA_REF_NO.Text = PONUMber
            ''matikan combo refno
            'Me.mcbOA_REF_NO.Enabled = False
            Me.btnPrint.Enabled = True
            Me.IsloadingRow = False
            Me.btnSave.Enabled = False
            Me.HasSavedSPPB = True

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsloadingRow = False
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnSave_Click")
            Me.Cursor = Cursors.Default
        End Try
        ''reload data SPPBBrandpack from the server
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Try
            Me.IsloadingRow = True
            Me.Cursor = Cursors.WaitCursor
            If Me.HasSavedSPPB Then
                If Me.DS.HasChanges() Then
                    If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.No Then
                        Me.DS.RejectChanges()
                        Me.DisposeAllObjects()
                        Me.Close() : Me.Cursor = Cursors.Default
                        Return
                    Else
                        If Not Me.SaveData() Then : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return : End If
                        Me.DS.AcceptChanges()
                    End If
                End If
                Me.DisposeAllObjects()
                Me.Close()
                Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
            End If
            If Me.HasChangedGONData Or Me.HasChangedSPPBData Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.No Then
                    Me.DS.RejectChanges()
                    Me.DisposeAllObjects()
                    Me.Cursor = Cursors.Default
                    Me.Close() : Me.IsloadingRow = False
                    Return
                Else
                    If Not Me.SaveData() Then : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return : End If
                    Me.DS.AcceptChanges()
                    Me.DisposeAllObjects()
                    Me.Close()
                    Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                End If
            Else
                Me.IsloadingRow = True
                Me.DisposeAllObjects()
                Me.Close()
            End If

        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_btnCLose_Click")
            Me.IsloadingRow = False
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub grdGon_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdGon.DeletingRecord
        If Me.IsloadingRow Then : Return : End If
        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage & vbCrLf & "You'll need to reset sppb status by yourself if system doesn't fix it") = Windows.Forms.DialogResult.No Then
            e.Cancel = True : Return
        End If

        Try
            e.Cancel = False
            Me.grdGon.UpdateData()
            Me.btnSave.Enabled = Me.HasChangedGONData()
        Catch ex As Exception
            e.Cancel = True
            Me.IsloadingRow = False
            Me.LogMyEvent(ex.Message, Me.Name + "_grdGon_DeletingRecord")
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub grdGon_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdGon.Enter
        If Not Me.isValidGON(Nothing, True) Then : Return : End If
    End Sub

    Private Sub SPPBEntryGON_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.IsloadingRow Then : Return : End If
        Try
            If Me.HasSavedSPPB Then
                If Me.DS.HasChanges() Then
                    If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.No Then
                        Me.Cursor = Cursors.WaitCursor
                        Me.IsloadingRow = True
                        Me.DisposeAllObjects()
                        Return
                    Else
                        Me.Cursor = Cursors.WaitCursor
                        Me.IsloadingRow = True
                        If Not Me.SaveData() Then
                            Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                        End If
                        Me.DisposeAllObjects()
                    End If
                End If
                Me.IsloadingRow = False : Me.Cursor = Cursors.Default
                Return
            End If
            If Me.HasChangedGONData() Or Me.HasChangedSPPBData() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.No Then
                    Me.Cursor = Cursors.WaitCursor
                    Me.IsloadingRow = True
                    Me.DisposeAllObjects()
                Else
                    Me.Cursor = Cursors.WaitCursor
                    If Not Me.SaveData() Then
                        Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                    End If
                    Me.DisposeAllObjects()
                End If
            End If
            Me.IsloadingRow = False
            Me.HasLoadedForm = True
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsloadingRow = False
            Me.HasLoadedForm = True
            Me.LogMyEvent(ex.Message, Me.Name + "_SPPBEntryGON_FormClosing")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnNewSPPB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.mcbOA_REF_NO.ReadOnly = False

    End Sub

    Private Sub chkProduct_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProduct.CheckedValuesChanged
        If Me.IsloadingRow Then : Return : End If

        If Me.chkProduct.DropDownDataSource Is Nothing Then : Me.cmbStatusSPPB.ReadOnly = True : Me.cmbStatusSPPB.Text = "" : Return : End If
        If Me.chkProduct.DropDownList.RecordCount <= 0 Then : Me.cmbStatusSPPB.ReadOnly = True : Me.cmbStatusSPPB.Text = "" : Return : End If
        If Me.chkProduct.DropDownList.GetCheckedRows().Length <= 0 Then : Me.cmbStatusSPPB.ReadOnly = True : Me.cmbStatusSPPB.Text = "" : Return : End If
        If Me.chkProduct.Text = "" Then : Me.cmbStatusSPPB.ReadOnly = True : Me.cmbStatusSPPB.Text = "" : Return : End If
        If Not Me.isValidGON(CType(Me.cmbStatusSPPB, Control), False) Then
            Me.IsloadingRow = True : Me.grdGon.SetDataBinding(Nothing, "") : Me.chkProduct.Text = "" : Me.IsloadingRow = False
            Return
        End If
        Me.cmbStatusSPPB.Text = "" : Me.cmbStatusSPPB.ReadOnly = False
        Me.IsloadingRow = True : Dim ORowFilter As String = "", OSort As String = ""
        Dim HasGONBefore As Boolean = False
        Dim GOnHeaderID As String = Me.txtSPPBNO.Text.TrimStart().TrimEnd() & "|" & Me.txtGONNO.Text.TrimStart().TrimEnd()
        If Me.Mode = SaveMode.Insert Then
            HasGONBefore = Me.clsSPPB.HasExistsGONHeaderID(GOnHeaderID, False)
            ': Me.ShowMessageInfo(Me.MessageDataHasExisted) : IsloadingRow = False : Me.Cursor = Cursors.Default : Return : End If
        ElseIf Me.grdGon.DataSource Is Nothing Then
            HasGONBefore = Me.clsSPPB.HasExistsGONHeaderID(GOnHeaderID, False)
        ElseIf Me.grdGon.RecordCount <= 0 Then
            CType(Me.grdGon.DataSource, DataView).RowFilter = ""
            If Me.grdGon.RecordCount <= 0 Then
                HasGONBefore = Me.clsSPPB.HasExistsGONHeaderID(GOnHeaderID, False)
            End If
        End If
        If HasGONBefore Then
            CType(sender, Janus.Windows.GridEX.EditControls.CheckedComboBox).UncheckAll()
            Me.ShowMessageInfo(Me.MessageDataHasExisted) : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Me.txtGONNO.Focus() : Return
        End If
        'Dim HasReloadData As Boolean = False
        Try
            'check apakah ada data di 
            If Not IsNothing(Me.grdGon.DataSource) Then
                If Me.grdGon.RecordCount > 0 Then
                    If Me.HasChangedGONData() Then
                        'looping data to insert
                        Dim DV As DataView = CType(Me.grdGon.DataSource, DataView)
                        If DV.RowFilter <> "" Then
                            ORowFilter = DV.RowFilter
                        End If
                        DV.RowFilter = ""
                        Dim DummyDV As DataView = DV.ToTable().Copy().DefaultView()
                        DummyDV.Sort = "GON_DETAIL_ID DESC"
                        For Each row As Janus.Windows.GridEX.GridEXRow In Me.chkProduct.DropDownList.GetCheckedRows()
                            Dim SPPBBrandPackID As String = row.Cells("SPPB_BRANDPACK_ID").Value.ToString()
                            Dim BrandPackID As String = row.Cells("BRANDPACK_ID").Value.ToString()
                            Dim BrandPackName As String = row.Cells("BRANDPACK_NAME").Value.ToString()
                            Dim GONQTy As Object = IIf((Not IsDBNull(row.Cells("LEFT_QTY").Value) And Not IsNothing(row.Cells("LEFT_QTY").Value)), Convert.ToDecimal(row.Cells("LEFT_QTY").Value), 0)
                            'Dim GONHeaderID As String = Me.txtSPPBNO.Text.TrimEnd().TrimStart() & "|" & Me.txtGONNO.Text.TrimStart().TrimEnd()
                            Dim GON_DetailID As String = GONHeaderID & "|" & SPPBBrandPackID
                            Dim Index As Integer = DummyDV.Find(GON_DetailID)
                            DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & BrandPackID & "' AND INACTIVE = " & False
                            'Dim IndexConvProd As Integer = Me.DVMConversiProduct.Find("BRANDPACK_ID = '" & BrandPackID & "'")
                            'If Not ValidData Then : Cursor = Cursors.Default : Return : End If
                            If Index <= -1 Then
                                Dim drv As DataRowView = DV.AddNew()
                                drv.BeginEdit()
                                drv("GON_DETAIL_ID") = GON_DetailID
                                drv("GON_HEADER_ID") = GOnHeaderID
                                drv("SPPB_BRANDPACK_ID") = SPPBBrandPackID
                                drv("BRANDPACK_ID") = BrandPackID
                                drv("BRANDPACK_NAME") = BrandPackName
                                drv("GON_QTY") = -1

                                drv("IsOPen") = True
                                drv("BatchNo") = ""
                                If DVMConversiProduct.Count > 0 Then
                                    Dim oVol1 As Object = DVMConversiProduct(0)("VOL1"), oVol2 As Object = DVMConversiProduct(0)("VOL2")
                                    Dim oUnit1 As Object = DVMConversiProduct(0)("UNIT1"), oUnit2 As Object = DVMConversiProduct(0)("UNIT2")
                                    'Dim ValidData As Boolean = True
                                    If oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                                        Me.ShowMessageError(BrandPackName & ", colly for Volume 1 has not been set yet")
                                        'ValidData = False
                                    ElseIf oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                                        Me.ShowMessageError(BrandPackName & ", colly for Volume 2 has not been set yet")
                                        'ValidData = False
                                    ElseIf oUnit1 Is Nothing Or oUnit2 Is DBNull.Value Then
                                        Me.ShowMessageError(BrandPackName & ", colly for Unit 1 has not been set yet")
                                        'ValidData = False
                                    ElseIf oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                                        Me.ShowMessageError(BrandPackName & ", colly for Unit 2 has not been set yet")
                                        'ValidData = False
                                    End If
                                    drv("UNIT1") = DVMConversiProduct(0)("UNIT1")
                                    drv("VOL1") = DVMConversiProduct(0)("VOL1")
                                    drv("UNIT2") = DVMConversiProduct(0)("UNIT1")
                                    drv("VOL2") = DVMConversiProduct(0)("VOL2")
                                Else
                                    Me.ShowMessageError("Convertion Product for " & BrandPackName & vbCrLf & "is not found" & vbCrLf & "Printing gon will be problem")
                                    drv("UNIT1") = ""
                                    drv("VOL1") = 0
                                    drv("UNIT1") = ""
                                    drv("VOL2") = 0
                                End If
                                drv("IsCompleted") = False
                                drv("IsUpdatedBySystem") = False
                                drv("CreatedBy") = NufarmBussinesRules.User.UserLogin.UserName
                                drv("CreatedDate") = NufarmBussinesRules.SharedClass.ServerDate
                                drv("ModifiedBy") = String.Empty
                                drv("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
                                drv.EndEdit()
                            End If
                        Next
                        ''delete data di grid gon yang tidak ada di chk product
                        Dim rows() As Janus.Windows.GridEX.GridEXRow = Me.chkProduct.DropDownList().GetCheckedRows()
                        OSort = DV.Sort
                        DV.Sort = "GON_DETAIL_ID DESC"
                        For i As Integer = 0 To DummyDV.Count - 1
                            Dim SPPBBrandPackID As String = DummyDV(i)("SPPB_BRANDPACK_ID").ToString()
                            Dim GONDetailID As String = Me.txtSPPBNO.Text.TrimEnd().TrimStart() & "|" & Me.txtGONNO.Text.TrimStart().TrimEnd() & "|" & SPPBBrandPackID
                            Dim BFInd As Boolean = False
                            For i1 As Integer = 0 To rows.Length - 1
                                BFInd = (rows(i1).Cells("SPPB_BRANDPACK_ID").Value.ToString() = SPPBBrandPackID)
                                If BFInd Then : Exit For : End If
                            Next
                            If Not BFInd Then
                                Dim Index As Integer = DV.Find(GONDetailID)
                                If Index <> -1 Then
                                    DV.Delete(Index)
                                End If
                            End If
                        Next
                        DV.RowFilter = ORowFilter
                        If DV.Sort <> "" Then : DV.Sort = OSort : End If
                    End If
                    'HasReloadData = True
                Else
                    fillTblGon(GOnHeaderID)
                End If
                Me.grdGon.UpdateData()
            Else
                fillTblGon(GOnHeaderID)
                'set readonly for GON Header
                Me.BindGrid(Me.grdGon, DS.Tables("GON_DETAIL_INFO").DefaultView())
            End If
            CType(Me.grdGon.DataSource, DataView).RowFilter = ""
            Me.HasSavedGON = (Me.grdGon.RecordCount <= 0)
            Me.btnPrint.Enabled = (Me.grdGon.RecordCount > 0)
            Me.txtGONNO.ReadOnly = True
            'Me.chkProduct.ReadOnly = True
            Me.dtPicGONDate.ReadOnly = True
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.HasSavedGON = (Me.grdGon.RecordCount <= 0)
            Me.Cursor = Cursors.Default
            Me.IsloadingRow = True
            Me.chkProduct.UncheckAll() : Me.chkProduct.Text = ""
            Me.LogMyEvent(ex.Message, Me.Name + "_chkProduct_CheckedValuesChanged")
            Me.IsloadingRow = False
        End Try
    End Sub
    Private Sub fillTblGon(ByVal GOnHeaderID As String)
        'check if is valid SPPB
        If Not IsNothing(Me.DS) Then
            If Not Me.DS.Tables.Contains("GON_DETAIL_INFO") Then
                Me.DS.Tables.Add(Me.tblMasterGON)
            Else
                Me.DS.Tables("GON_DETAIL_INFO").RejectChanges()
                Me.DS.Tables("GON_DETAIL_INFO").Rows.Clear()
                Me.DS.Tables("GON_DETAIL_INFO").AcceptChanges()
            End If
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.chkProduct.DropDownList.GetCheckedRows()
                Dim SPPBBrandPackID As String = row.Cells("SPPB_BRANDPACK_ID").Value.ToString()
                Dim BrandPackID As String = row.Cells("BRANDPACK_ID").Value.ToString()
                Dim BrandPackName As String = row.Cells("BRANDPACK_NAME").Value.ToString()
                'Dim GONHeaderID As String = Me.txtSPPBNO.Text.TrimEnd().TrimStart() & "|" & Me.txtGONNO.Text.TrimStart().TrimEnd()
                Dim GONQTy As Object = IIf((Not IsDBNull(row.Cells("LEFT_QTY").Value) And Not IsNothing(row.Cells("LEFT_QTY").Value)), Convert.ToDecimal(row.Cells("LEFT_QTY").Value), 0)
                Dim GON_DetailID As String = GOnHeaderID & "|" & SPPBBrandPackID
                Dim drv As DataRow = Me.DS.Tables("GON_DETAIL_INFO").NewRow()
                drv.BeginEdit()
                drv("GON_DETAIL_ID") = GON_DetailID
                drv("GON_HEADER_ID") = GOnHeaderID
                drv("SPPB_BRANDPACK_ID") = SPPBBrandPackID
                drv("BRANDPACK_ID") = BrandPackID
                drv("BRANDPACK_NAME") = BrandPackName
                drv("GON_QTY") = -1
                Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & BrandPackID & "' AND INACTIVE = " & False
                'Dim IndexConvProd As Integer = Me.DVMConversiProduct.Find(BrandPackID)
                If DVMConversiProduct.Count > 0 Then
                    Dim oVol1 As Object = DVMConversiProduct(0)("VOL1"), oVol2 As Object = DVMConversiProduct(0)("VOL2")
                    Dim oUnit1 As Object = DVMConversiProduct(0)("UNIT1"), oUnit2 As Object = DVMConversiProduct(0)("UNIT2")
                    'Dim ValidData As Boolean = True
                    If oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Volume 1 has not been set yet")
                        'ValidData = False
                    ElseIf oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Volume 2 has not been set yet")
                        'ValidData = False
                    ElseIf oUnit1 Is Nothing Or oUnit2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Unit 1 has not been set yet")
                        'ValidData = False
                    ElseIf oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                        Me.ShowMessageError(BrandPackName & ", colly for Unit 2 has not been set yet")
                        'ValidData = False
                    End If
                    drv("UNIT1") = DVMConversiProduct(0)("UNIT1")
                    drv("VOL1") = DVMConversiProduct(0)("VOL1")
                    drv("UNIT2") = DVMConversiProduct(0)("UNIT2")
                    drv("VOL2") = DVMConversiProduct(0)("VOL2")
                Else
                    Me.ShowMessageError("Convertion Product for " & BrandPackName & vbCrLf & " is not found" & vbCrLf & "Printing gon will be problem")
                    drv("UNIT1") = ""
                    drv("VOL1") = 0
                    drv("UNIT2") = ""
                    drv("VOL2") = 0
                End If
                drv("BatchNo") = ""
                drv("IsOPen") = True
                drv("IsCompleted") = False
                drv("IsUpdatedBySystem") = False
                drv("CreatedBy") = NufarmBussinesRules.User.UserLogin.UserName
                drv("CreatedDate") = NufarmBussinesRules.SharedClass.ServerDate
                drv("ModifiedBy") = String.Empty
                drv("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
                drv.EndEdit()
                Me.DS.Tables("GON_DETAIL_INFO").Rows.Add(drv)
            Next
        End If
    End Sub
    Private Sub grdGon_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdGon.KeyDown
        If e.KeyCode = Keys.F2 Then
            'reload data
            If Me.HasChangedGONData() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.No Then
                    Return
                Else
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        Me.IsloadingRow = True
                        Me.DS.RejectChanges()
                        If Me.DS.Tables.Contains("GON_DETAIL_INFO") Then
                            Me.DS.Tables.Remove("GON_DETAIL_INFO")
                        End If
                        Me.DS.AcceptChanges()
                        Dim tblGON As DataTable = Me.clsSPPB.getGOnData(Me.txtSPPBNO.Text.TrimStart().TrimEnd(), True)
                        'bind grid
                        Me.BindGrid(Me.grdGon, tblGON.DefaultView)
                        Me.IsloadingRow = False
                        Me.Cursor = Cursors.Default
                    Catch ex As Exception
                        Me.IsloadingRow = False
                        Me.Cursor = Cursors.Default
                        Me.LogMyEvent(ex.Message, Me.Name + "_grdGon_KeyDown")
                        Me.ShowMessageInfo(ex.Message)
                    End Try
                End If
            End If
        End If
    End Sub
    Private Sub grdSPPB_CellValueChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSPPB.CellValueChanged
        If IsNothing(Me.grdSPPB.DataSource) Then : Return : End If
        If Not Me.HasLoadedForm Then : Return : End If
        If IsloadingRow Then : Return : End If
        If IsNothing(Me.grdGon.SelectedItems) Then : Return : End If
        If Me.grdSPPB.GetRow().RowType <> Janus.Windows.GridEX.RowType.Record Then : Return : End If

        Me.Cursor = Cursors.WaitCursor
        'If Not NufarmBussinesRules.User.UserLogin.IsAdmin Then
        '    Dim SPPBBrandPackID As String = Me.grdSPPB.GetValue("SPPB_BRANDPACK_ID").ToString()
        '    If Me.clsSPPB.IshasSPPBReferencedGON(SPPBBrandPackID, True) Then
        '        Me.ShowMessageInfo("Can not edit data" & vbCrLf & "SPPB has some GON(s) referenced") : Me.IsloadingRow = True : Me.grdSPPB.CancelCurrentEdit()
        '        Me.Cursor = Cursors.Default : Me.IsloadingRow = False : Return
        '    End If
        'End If
        'if me.clsSPPB.IshasSPPBReferencedGON(
        Try
            If e.Column.Key = "STATUS" Then
                ''check status tblGon apakah ada yang sudah berubah
                'If Me.HasChangedGONData() Then : Me.ShowMessageInfo("can not set status when gon data's already changed" & vbCrLf & "You must save any changes first, before editing status") : Me.grdSPPB.CancelCurrentEdit()
                '    Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                'End If
                Dim SPPBrandPackID As String = Me.grdSPPB.GetValue("SPPB_BRANDPACK_ID").ToString()
                Dim SPPBQTY As Decimal = Convert.ToDecimal(Me.grdSPPB.GetValue("SPPB_QTY"))
                ''check totalGonQty
                Dim TotalGOnQty As Decimal = Me.clsSPPB.GetTotalGONQTY(SPPBrandPackID, True)
                If TotalGOnQty >= SPPBQTY Then
                    Me.ShowMessageInfo("can not set status while SPPBQty is equal with TotalGONQty" & vbCrLf & "The status has been shipped") : Me.grdSPPB.CancelCurrentEdit() : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                End If
                If Me.grdSPPB.GetValue("STATUS").ToString() = "OTHER" Then
                    If IsNothing(Me.frmSetstat) OrElse Me.frmSetstat.IsDisposed() Then
                        frmSetstat = New FrmSetOtherStatus()
                    End If
                    Dim tblStatus As DataTable = Me.CreateOrReCreateDataTable("grdSPPB", Me.grdSPPB.GetValue("REMARK").ToString())
                    With frmSetstat
                        .FrmParent = Me
                        ''bikin datatable 
                        If tblStatus.Rows.Count <= 0 Then
                            MessageBox.Show("Unknown error", "", MessageBoxButtons.OK, MessageBoxIcon.Error) : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                        End If
                        .tblStatus = tblStatus
                        .RefControl = FrmSetOtherStatus.ReferencedControl.Grid
                        .TipPosition = DevComponents.DotNetBar.eTipPosition.Top
                        .Owner = Me
                        .TopLevel = True
                        .Show(Me.grdSPPB)
                    End With
                ElseIf Me.grdSPPB.GetValue("STATUS").ToString() = "--NEW SPPB--" Then
                    'check data di datagrid gon
                    If (Not IsNothing(Me.grdGon.DataSource)) Then
                        If Me.grdGon.RecordCount > 0 Then
                            Me.ShowMessageInfo("invalid status, GON has already been made") : Me.grdSPPB.CancelCurrentEdit() : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                        End If
                    End If
                ElseIf Me.grdSPPB.GetValue("STATUS").ToString() = "SHIPPED" Or Me.grdSPPB.GetValue("STATUS").ToString() = "COMPLETED" Then
                    Me.ShowMessageInfo("the status you've choosen can only be set by system") : Me.grdSPPB.CancelCurrentEdit() : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                Else
                    If Not IsNothing(Me.grdGon.DataSource) Then
                        If Me.grdGon.RecordCount > 0 Then
                            Dim DVGOn As DataView = CType(Me.grdGon.DataSource, DataView)
                            Dim ORowfilter = DVGOn.RowFilter
                            Dim RowFilter = "SPPB_BRANDPACK_ID = '" & Me.grdSPPB.GetValue("SPPB_BRANDPACK_ID").ToString() & "'"
                            If DVGOn.RowFilter <> RowFilter Then
                                DVGOn.RowFilter = "SPPB_BRANDPACK_ID = '" & Me.grdSPPB.GetValue("SPPB_BRANDPACK_ID").ToString() & "'"
                                For Each RView As DataRowView In DVGOn
                                    RView.BeginEdit()
                                    RView("IsUpdatedBySystem") = False
                                    If Me.Mode = SaveMode.Update Then
                                        RView("MODIFY_BY") = NufarmBussinesRules.User.UserLogin.UserName
                                        RView("MODIFY_DATE") = NufarmBussinesRules.SharedClass.ServerDate
                                    End If
                                    RView.EndEdit()
                                Next
                            End If
                            Me.grdGon.UpdateData()
                        End If
                    End If
                End If
                Me.grdSPPB.SetValue("IsUpdatedBySystem", False)
                If Me.Mode = SaveMode.Update Then
                    Me.grdSPPB.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                    Me.grdSPPB.SetValue("MODIFY_DATE", NufarmBussinesRules.SharedClass.ServerDate)
                End If
                Me.grdSPPB.UpdateData()
                If Not Me.btnSave.Enabled Then : Me.btnSave.Enabled = (Me.HasChangedSPPBData() Or Me.HasChangedGONData()) : End If
            End If

            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.IsloadingRow = False
            Me.LogMyEvent(ex.Message, Me.Name + "_grdSPPB_CellUpdated")
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdGon_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdGon.RecordsDeleted
        Try
            If Me.IsloadingRow Then : Return : End If
            'check value chkproduct
            ''create dummy datarowview
            Dim tblProduct As New DataTable("T_Product") : tblProduct.Clear()
            Dim colProdSPPBBrandPack As New DataColumn("SPPB_BRANDPACK_ID", Type.GetType("System.String"))
            Dim colProdSPPBQTy As New DataColumn("LEFT_QTY", Type.GetType("System.Decimal"))
            Dim colProdBrandPackName As New DataColumn("BRANDPACK_NAME", Type.GetType("System.String"))
            Dim colBrandPackID As New DataColumn("BRANDPACK_ID", Type.GetType("System.String"))
            tblProduct.Columns.AddRange(New DataColumn() {colProdSPPBBrandPack, colProdBrandPackName, colProdSPPBQTy, colBrandPackID})
            tblProduct.AcceptChanges()
            Dim DVDummyProduct As DataView = tblProduct.DefaultView()
            DVDummyProduct.Sort = Me.DVProduct.Sort
            Me.IsloadingRow = True
            'make equal between chk product data checked and item in grid
            'delete checked item if doesn't exist in grid 
            If Me.grdGon.RecordCount > 0 Then
                Dim DV As DataView = CType(Me.grdGon.DataSource, DataView)
                Dim ORowFilter As String = DV.RowFilter
                DV.RowFilter = ""
                Dim DummyDV As DataView = DV.ToTable().Copy().DefaultView()
                Dim SPPBrandPackIDs As String = ""
                Dim sppbBrandpackID(Me.grdGon.RecordCount - 1) As Object
                For i As Integer = 0 To DummyDV.Count - 1
                    sppbBrandpackID(i) = DummyDV(i)("SPPB_BRANDPACK_ID").ToString()
                Next
                Me.chkProduct.CheckedValues = sppbBrandpackID
                DV.RowFilter = ORowFilter
            End If
            Me.grdGon.UpdateData()
            ''reload gon to set any needed status
            Me.ReloadGridGON()
            Me.Cursor = Cursors.Default
            Me.IsloadingRow = False
            Me.ShowMessageInfo("Data has already been deleted" & vbCrLf & "However, to reflect any changes to the database" & vbCrLf & "You should immediately save changes")
        Catch ex As Exception
            Me.IsloadingRow = False
            Me.LogMyEvent(ex.Message, Me.Name + "_ grdGon_RecordsDeleted")
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSPPB_RecordsDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSPPB.RecordsDeleted
        Me.grdSPPB.UpdateData()
        Me.btnSave.Enabled = Me.HasChangedSPPBData()
    End Sub

    Private Sub grdSPPB_CellUpdated_1(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSPPB.CellUpdated
        If Not Me.HasLoadedForm Then : Return : End If
        If Me.IsloadingRow Then : Return : End If
        Try
            If e.Column.Key = "REMARK" Then
                Dim Remark As String = IIf((Not IsNothing(Me.grdSPPB.GetValue("REMARK")) And Not IsNothing(Me.grdSPPB.GetValue("REMARK"))), Me.grdSPPB.GetValue("REMARK").ToString(), "")
                If String.IsNullOrEmpty(Remark) Then
                    'check status 
                    If Me.grdSPPB.GetValue("STATUS").ToString() = "OTHER" Then
                        Me.ShowMessageInfo("can not set empty remark while status is 'Other'") : Me.grdSPPB.CancelCurrentEdit() : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return
                    End If
                End If
                If Microsoft.VisualBasic.Right(Remark, 7).Contains("UNKNOWN") Then
                    Me.grdSPPB.SetValue("IsUpdatedBySystem", True)
                Else
                    Me.grdSPPB.SetValue("IsUpdatedBySystem", False)
                End If
                Me.grdSPPB.UpdateData()
                Me.btnSave.Enabled = (Me.HasChangedSPPBData() Or Me.HasChangedGONData())
            End If

        Catch ex As Exception
            Me.IsloadingRow = False
            Me.grdSPPB.CancelCurrentEdit()
            Me.LogMyEvent(ex.Message, Me.Name + "_grdSPPB_CellUpdated_1")
            Me.IsloadingRow = False
        End Try

    End Sub

    Private Sub dtPicGONDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicGONDate.ValueChanged
        If Not Me.HasLoadedForm Then
            Return
        End If
        If Me.DataToEdit = EditData.GON Then
            Me.btnSave.Enabled = (Convert.ToDateTime(Me.OGONHeader.GON_DATE) <> Convert.ToDateTime(dtPicGONDate.Value.ToShortDateString()))
        End If
    End Sub

    Private Sub mcbGonArea_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbGonArea.ValueChanged
        If Not Me.HasLoadedForm Then
            Return
        End If
        If Me.DataToEdit = EditData.GON Then
            Me.btnSave.Enabled = (Me.OGONHeader.GON_ID_AREA <> Me.mcbGonArea.Value)
        End If
    End Sub

    Private Sub mcbTransporter_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbTransporter.ValueChanged
        If Not Me.HasLoadedForm Then
            Return
        End If
        If Me.DataToEdit = EditData.GON Then
            Me.btnSave.Enabled = (Me.OGONHeader.GT_ID <> Me.mcbTransporter.Value)
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.lblDistributor.Text = "" Then : Return : End If
            If Me.lblPODate.Text = "" Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            'check gon
            Dim tblGON As DataTable = Me.DS.Tables("GON_DETAIL_INFO")
            Dim InsertedRows() As DataRow = tblGON.Select("", "", Data.DataViewRowState.Added)
            Dim UpdatedRows() As DataRow = tblGON.Select("", "", Data.DataViewRowState.ModifiedOriginal)
            Dim DeletedRows() As DataRow = tblGON.Select("", "", Data.DataViewRowState.Deleted)
            Dim GChanged As Boolean = False
            If InsertedRows.Length > 0 Then : GChanged = True
            ElseIf UpdatedRows.Length > 0 Then : GChanged = True
            ElseIf DeletedRows.Length > 0 Then : GChanged = True : End If
            If GChanged Then
                If Not Me.isValidSPPB() Then : Me.Cursor = Cursors.Default : Return : End If
                Me.IsloadingRow = True : If Not Me.SaveData() Then : Me.IsloadingRow = False : Me.Cursor = Cursors.Default : Return : End If
                ''bind grid
                Me.IsloadingRow = True
                Me.BindGrid(Me.grdGon, Me.DS.Tables("GON_DETAIL_INFO").DefaultView)
                Me.BindGrid(Me.grdSPPB, Me.DS.Tables("SPPB_BRANDPACK_INFO").DefaultView)
                Me.IsloadingRow = False
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
            Dim colGON_DATE As New DataColumn("GON_DATE", Type.GetType("System.DateTime"))
            Dim colTRANSPORTER_NAME As New DataColumn("TRANSPORTER_NAME", Type.GetType("System.String"))
            Dim colBrandPackName As New DataColumn("BRANDPACK_NAME", Type.GetType("System.String"))
            colBrandPackName.AllowDBNull = False
            Dim colQUANTITY As New DataColumn("QUANTITY", Type.GetType("System.String"))
            Dim colCOLLY_BOX As New DataColumn("COLLY_BOX", Type.GetType("System.String"))
            Dim colCOLLY_PACKSIZE As New DataColumn("COLLY_PACKSIZE", Type.GetType("System.String"))
            Dim colBATCH_NO As New DataColumn("BATCH_NO", Type.GetType("System.String"))
            Dim colPolNoTrans As New DataColumn("POLICE_NO_TRANS", Type.GetType("System.String"))
            Dim colDriverTrans As New DataColumn("DRIVER_TRANS", Type.GetType("System.String"))
            colBATCH_NO.AllowDBNull = True
            tbl_ref_gon.Columns.AddRange(New DataColumn() {colDISTRIBUTOR_NAME, colVarDistAddress, colADDRESS, colPO_REF_NO, colPO_REF_DATE, colPONoAndDate, colSPPB_NO, _
            colSPPB_DATE, colSPPBNoAndDate, colVarGonDate, colVarWarHouse, colGON_NO, colGON_DATE, colPolNoTrans, colDriverTrans, colTRANSPORTER_NAME, colBrandPackName, colQUANTITY, _
            colCOLLY_BOX, colCOLLY_PACKSIZE, colBATCH_NO})
            Me.mcbOA_REF_NO.DropDownList.Focus()
            Me.UnabledEntryGON(False)
            'Dim tblGon As DataTable = Me.DS.Tables("GON_DETAIL_INFO")
            Dim info As New CultureInfo("id-ID")
            ''masukan data conversi di gon_table columnya di hide saja
            Me.mcbOA_REF_NO.Focus()
            Me.mcbOA_REF_NO.DroppedDown = True
            Dim Address As String = Me.mcbOA_REF_NO.DropDownList().GetValue("ADDRESS").ToString()
            Dim DistributorName As String = Me.mcbOA_REF_NO.DropDownList.GetValue("DISTRIBUTOR_NAME").ToString()
            If Me.txtShipTo.Text <> "" Then
                Address = Me.txtShipTo.Text.Trim().Replace(vbCrLf, " ")
            End If
            Me.mcbOA_REF_NO.DroppedDown = False
            For i As Integer = 0 To tblGON.Rows.Count - 1
                Dim row As DataRow = tbl_ref_gon.NewRow()
                row("DISTRIBUTOR_NAME") = DistributorName
                row("ADDRESS") = Address
                row("VAR_DIST_ADDRESS") = String.Format("{0}" & vbCrLf & "{1}", DistributorName, Address)
                Dim PORefNo As String = Me.mcbOA_REF_NO.DropDownList().GetValue("PO_REF_NO").ToString()
                Dim PORefDate As System.DateTime = Convert.ToDateTime(Me.mcbOA_REF_NO.DropDownList().GetValue("PO_DATE"))
                row("PO_REF_NO") = PORefNo
                row("PO_REF_DATE") = PORefDate
                row("POREF_NO_AND_DATE") = String.Format(info, "{0} - {1:dd/MM/yyyy}", PORefNo, PORefDate)
                Dim SPPBno As String = Me.txtSPPBNO.Text.Trim()
                Dim SPPBDate As System.DateTime = Convert.ToDateTime(Me.dtPicSPPBDate.Value.ToShortDateString())
                row("SPPB_NO") = SPPBno
                row("SPPB_DATE") = SPPBDate
                row("SPPB_NO_AND_DATE") = String.Format(info, "{0} - {1:dd/MM/yyyy}", SPPBno, SPPBDate)
                Dim GonDate As System.DateTime = Convert.ToDateTime(Me.dtPicGONDate.Value.ToShortDateString())
                row("GON_NO") = Me.txtGONNO.Text.Trim()
                row("GON_DATE") = GonDate
                row("VAR_GON_DATE_STR") = String.Format(info, "Date : {0:dd/MM/yyyy}", GonDate)
                Me.cmdWarhouse.Focus()
                row("POLICE_NO_TRANS") = Me.txtPolice_no_Trans.Text.Trim()
                row("DRIVER_TRANS") = Me.txtDriverTrans.Text.Trim()
                row("VAR_WARHOUSE") = String.Format("Gudang : {0}", Me.cmdWarhouse.SelectedValue)
                Me.mcbTransporter.Focus()
                row("TRANSPORTER_NAME") = Me.mcbTransporter.Text
                Dim BrandPackName As String = tblGON.Rows(i)("BRANDPACK_NAME")
                row("BRANDPACK_NAME") = BrandPackName
                Dim GonQty As Decimal = Convert.ToDecimal(tblGON.Rows(i)("GON_QTY"))
                Dim oVol1 As Object = tblGON.Rows(i)("VOL1"), oVol2 As Object = tblGON.Rows(i)("VOL2")
                Dim oUnit1 As Object = tblGON.Rows(i)("UNIT1"), oUnit2 As Object = tblGON.Rows(i)("UNIT2")
                Dim ValidData As Boolean = True
                If oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                    Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & tblGON.Rows(i)("BRANDPACK_ID") & "'"
                    If Me.DVMConversiProduct.Count <= 0 Then
                        Me.ShowMessageError(BrandPackName & ", colly for Volume 1 has not been set yet")
                        ValidData = False
                    Else
                        oVol1 = Me.DVMConversiProduct(0)("VOL1")
                    End If
                End If
                If oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                    Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & tblGON.Rows(i)("BRANDPACK_ID") & "'"
                    If Me.DVMConversiProduct.Count <= 0 Then
                        Me.ShowMessageError(BrandPackName & ", colly for Volume 2 has not been set yet")
                        ValidData = False
                    Else
                        oVol2 = Me.DVMConversiProduct(0)("VOL2")
                    End If
                End If
                If oUnit1 Is Nothing Or oUnit1 Is DBNull.Value Then
                    Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & tblGON.Rows(i)("BRANDPACK_ID") & "'"
                    If Me.DVMConversiProduct.Count <= 0 Then
                        Me.ShowMessageError(BrandPackName & ", colly for unit 1 has not been set yet")
                        ValidData = False
                    Else
                        oUnit1 = Me.DVMConversiProduct(0)("UNIT1")
                    End If
                End If

                If oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                    Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & tblGON.Rows(i)("BRANDPACK_ID") & "'"
                    If Me.DVMConversiProduct.Count <= 0 Then
                        Me.ShowMessageError(BrandPackName & ", colly for unit 1 has not been set yet")
                        ValidData = False
                    Else
                        oUnit2 = Me.DVMConversiProduct(0)("UNIT2")
                    End If
                End If

                'If oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                '    Me.ShowMessageError(BrandPackName & ", colly for Volume 1 has not been set yet")
                '    ValidData = False
                'ElseIf oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                '    Me.ShowMessageError(BrandPackName & ", colly for Volume 2 has not been set yet")
                '    ValidData = False
                'ElseIf oUnit1 Is Nothing Or oUnit2 Is DBNull.Value Then
                '    Me.ShowMessageError(BrandPackName & ", colly for Unit 1 has not been set yet")
                '    ValidData = False
                'ElseIf oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                '    Me.ShowMessageError(BrandPackName & ", colly for Unit 2 has not been set yet")
                '    ValidData = False
                'End If
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
                        'ilqty = ilqty + DVol2
                        ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                    End If
                    collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                ElseIf GonQty > 0 Then ''gon kurang dari 1 coly
                    Dim ilqty As Integer = Convert.ToInt32((GonQty / Dvol1) * DVol2)
                    collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                End If
                row("QUANTITY") = String.Format(info, "{0:#,##0.000} {1}", GonQty, tblGON.Rows(i)("UNIT1").ToString())
                row("COLLY_BOX") = collyBox
                row("COLLY_PACKSIZE") = collyPackSize
                row("BATCH_NO") = tblGON.Rows(i)("BatchNo")
                row.EndEdit()
                tbl_ref_gon.Rows.Add(row)
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
            Me.IsloadingRow = False
            Me.Cursor = Cursors.Default
            Me.LogMyEvent(ex.Message, Me.Name + "_btnPrint_Click")
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
