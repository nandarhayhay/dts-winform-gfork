Imports NufarmBussinesRules.Brandpack.PriceHistory
Public Class Purchase_Order
#Region " Deklarasi "
    Friend Mode As ModeSave
    Private m_PORef As String
    Private clsPO As NufarmBussinesRules.PurchaseOrder.PO_BrandPack
    'Private m_HasExpired As Boolean
    Private m_PO_REF_DATE As DateTime
    Private m_Distributor_ID As String
    Private m_PROJ_REF_NO As String
    Private m_DistributorID As String
    Private MCBStateFilling As StateFilingMCB
    Private ChangeValueMode As SetValueMode
    Private HasLeave As Boolean
    Private StateSaving As WhileSaving
    Private HasLoad As Boolean
    Private BRANDPACK_ID As String
    Private SFG As StateFillingGrid
    Private IsPriceHK As Boolean = False
    Private isTypingPO As Boolean = True
    Public frmParent As PO = Nothing
#End Region

#Region " Enum "
    Friend Enum ModeSave
        Save
        Update
    End Enum
    Private Enum StateFilingMCB
        Filling
        HasFilled
    End Enum
    Private Enum SetValueMode
        HasChanged
        SetValueChanged
    End Enum
    Private Enum WhileSaving
        Saving
        SuccesSaving
        Failed
    End Enum
    Private Enum StateFillingGrid
        HasFilled
        Filling
    End Enum
#End Region

#Region " Property "
    Friend Property DistributorID() As String
        Get
            Return Me.m_Distributor_ID
        End Get
        Set(ByVal value As String)
            Me.m_Distributor_ID = value
        End Set
    End Property
    Friend Property PROJ_REF_NO() As Object
        Get
            Return Me.m_PROJ_REF_NO
        End Get
        Set(ByVal value As Object)
            Me.m_PROJ_REF_NO = value
        End Set
    End Property
    Friend Property PORef() As String
        Get
            Return Me.m_PORef
        End Get
        Set(ByVal value As String)
            Me.m_PORef = value
        End Set
    End Property
    'Friend Property HasExpired() As Boolean
    '    Get
    '        Return Me.m_HasExpired
    '    End Get
    '    Set(ByVal value As Boolean)
    '        Me.m_HasExpired = value
    '    End Set
    'End Property
    Friend Property PO_REF_DATE() As Date
        Get
            Return Me.m_PO_REF_DATE
        End Get
        Set(ByVal value As Date)
            Me.m_PO_REF_DATE = value
        End Set
    End Property
#End Region

#Region " Function "
    Private Function IsValid() As Boolean
        If Me.Mode = ModeSave.Save Or Me.txtPOReference.Visible = True Then
            If Me.txtPOReference.Text = "" Then
                Me.baseTooltip.SetToolTip(Me.txtPOReference, "PO_Reference is Null !." & vbCrLf & "Please Suply PO_Reference.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtPOReference), Me.txtPOReference, 2000)
                Me.txtPOReference.Focus() : Return False
            End If
        Else
            If Me.mcbPOHeader.Value Is Nothing Or Me.mcbPOHeader.SelectedIndex <= -1 Then
                Me.baseTooltip.Show("Please defined PO_REF_NO", Me.mcbPOHeader, 2500) : Me.mcbPOHeader.Focus() : Return False
            End If
        End If
        If (Me.MultiColumnCombo1.Text = "") Or (Me.MultiColumnCombo1.SelectedIndex = -1) Then
            Me.baseTooltip.SetToolTip(Me.MultiColumnCombo1, "Distributor is Null !." & vbCrLf & "Please Defined Distributor Name.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.MultiColumnCombo1), Me.MultiColumnCombo1, 2000)

            Me.MultiColumnCombo1.Focus()
            Return False
        ElseIf Me.dtPicRefDate.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.dtPicRefDate, "PO_REF_DATE is Null !." & vbCrLf & "Please Defined PO_REF_DATE.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dtPicRefDate), Me.dtPicRefDate, 2000)

            Me.dtPicRefDate.Focus()
            Return False

        End If
        Return True
    End Function
#End Region

#Region " Sub Procedure "
    Private Sub LockedColumnGrid()
        Me.grdPOBrandPack.RootTable.Columns("PO_BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPOBrandPack.RootTable.Columns("PO_REF_NO").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPOBrandPack.RootTable.Columns("TOTAL").EditType = Janus.Windows.GridEX.EditType.NoEdit
    End Sub
    Private Sub FormatGridEx()
        Me.grdPOBrandPack.RootTable.Columns("PO_PRICE_PERQTY").FormatString = "#,##0.00"
        Me.grdPOBrandPack.RootTable.Columns("PO_PRICE_PERQTY").TotalFormatString = "#,##0.00"
        Me.grdPOBrandPack.RootTable.Columns("TOTAL").FormatString = "#,##0.00"
        Me.grdPOBrandPack.RootTable.Columns("TOTAL").TotalFormatString = "#,##0.00"
        Me.grdPOBrandPack.RootTable.Columns("TOTAL").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
        'Me.grdPOBrandPack.RootTable.Columns("PO_PRICE_PERQTY").EditType = Janus.Windows.GridEX.EditType.NoEdit

        Dim ColRemark As Janus.Windows.GridEX.GridEXColumn = grdPOBrandPack.RootTable.Columns("DESCRIPTIONS2")
        ColRemark.EditType = Janus.Windows.GridEX.EditType.DropDownList

        'Set HasValueList property equal to true in order to be able to use the ValueList property
        ColRemark.HasValueList = True
        'Get the ValueList collection associated to this column
        Dim ValueList As Janus.Windows.GridEX.GridEXValueListItemCollection = ColRemark.ValueList
        Dim Arr(15) As String
        With Arr
            .SetValue("OVER DUE", 0)
            .SetValue("CREDIT LIMIT", 1)
            .SetValue("PROGRAM", 2)
            .SetValue("SCHEDULE OF SHIPMENT", 3)
            .SetValue("QUOTA SHIPMENT", 4)
            .SetValue("PRICE FROM PLANTATION", 5)
            .SetValue("OVERDUE - CREDIT LIMIT", 6)
            .SetValue("BANK GUARANTEE", 7)
            .SetValue("DISTRIBUTOR PROGRAM", 8)
            .SetValue("RETAILER PROGRAM", 9)
            .SetValue("PLANTATION PROGRAM", 10)
            .SetValue("PLANTATION PO", 11)
            .SetValue("PO REVISE", 12)
            .SetValue("ON CSE PROCESS", 13)
            .SetValue("ON LOGISTIC PROCESS", 14)
            .SetValue("PROGRAM SUBMISSION", 15)
        End With
        Array.Sort(Arr)
        'Dim ListStatus() As String = {"TIDAK ADA STOCK", "STOCK_UNAVAILABLE", "AWAITING_TRANSPORTER", "QUOTA_SHIPMENT", "SHIPPED", "COMPLETED", "OTHER"}
        ValueList.PopulateValueList(Arr, "DESCRIPTIONS2")
        ColRemark.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        ColRemark.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
        grdPOBrandPack.RootTable.Columns("DESCRIPTIONS2").Width = 210
        'grdPOBrandPack.RootTable.Columns("DESCRIPTIONS2").DropDown.VisibleRows = 15
        'grdPOBrandPack.RootTable.Columns("BRANDPACK_ID").DropDown.VisibleRows = 15

    End Sub
    Private Sub BindMultiColumnCombo(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal dsSource As Object)
        Me.MCBStateFilling = StateFilingMCB.Filling
        If dsSource Is Nothing Then
            mcb.SetDataBinding(Nothing, "") : Me.MCBStateFilling = StateFilingMCB.HasFilled : Return
        End If
        mcb.Text = ""
        Select Case mcb.Name
            Case "MultiColumnCombo1"
                Me.MultiColumnCombo1.SetDataBinding(dsSource, "")
                Me.MultiColumnCombo1.DropDownList.RetrieveStructure()
                Me.MultiColumnCombo1.DroppedDown = True
                System.Threading.Thread.Sleep(100)
                For Each item As Janus.Windows.GridEX.GridEXColumn In Me.MultiColumnCombo1.DropDownList.Columns
                    If item.DataMember = "DISTRIBUTOR_ID" Or item.DataMember = "DISTRIBUTOR_NAME" Then
                        item.Visible = True : item.AutoSize()
                    Else
                        item.Visible = False
                    End If
                Next
                Me.MultiColumnCombo1.DisplayMember = "DISTRIBUTOR_NAME" : Me.MultiColumnCombo1.ValueMember = "DISTRIBUTOR_ID"
                Me.MultiColumnCombo1.DroppedDown = False
            Case "mcbProject"
                Me.mcbProject.SetDataBinding(dsSource, "")
                Me.mcbProject.DropDownList.RetrieveStructure()
                Me.mcbProject.DroppedDown = True
                System.Threading.Thread.Sleep(100)
                For Each item As Janus.Windows.GridEX.GridEXColumn In Me.mcbProject.DropDownList.Columns
                    If item.Type Is Type.GetType("System.DateTime") Then
                        item.FormatString = "dd MMMM yyyy"
                    End If
                    item.AutoSize()
                Next
                Me.mcbProject.DisplayMember = "PROJECT_NAME" : Me.mcbProject.ValueMember = "PROJ_REF_NO"
                Me.mcbProject.DroppedDown = False
            Case "mcbPOHeader"
                mcb.SetDataBinding(dsSource, "")
                mcb.DropDownList.RetrieveStructure()

                System.Threading.Thread.Sleep(100)
                mcb.DroppedDown = True
                For Each item As Janus.Windows.GridEX.GridEXColumn In mcb.DropDownList.Columns
                    item.AutoSize()
                Next
                Me.mcbPOHeader.DropDownList.Columns("PO_REF_DATE").FormatString = "dd MMMM yyyy"
                Me.mcbPOHeader.DisplayMember = "PO_REF_NO" : Me.mcbPOHeader.ValueMember = "PO_REF_NO"
                mcb.DroppedDown = False
        End Select
        Me.MCBStateFilling = StateFilingMCB.HasFilled
    End Sub
    Private Sub ClearCategoriesValueList()
        Me.grdPOBrandPack.DropDowns(0).SetDataBinding(Nothing, "")
        Me.grdPOBrandPack.DropDowns(0).Refetch()
    End Sub
    Private Sub FillCategoriesValueList()
        Me.grdPOBrandPack.DropDowns(0).SetDataBinding(Me.clsPO.ViewBrandpack(), "")
        Me.grdPOBrandPack.DropDowns(0).Refetch()
    End Sub
    Private Sub unabledControl()
        Me.txtPOReference.ReadOnly = True

        'Me.dtPicRefDate.ReadOnly = True
        Me.MultiColumnCombo1.ReadOnly = True
        Me.SavingChanges1.btnSave.Enabled = False
    End Sub
    Private Sub EnabledControl()
        Me.txtPOReference.ReadOnly = False
        'Me.dtPicRefDate.ReadOnly = False
        Me.MultiColumnCombo1.ReadOnly = False
        Me.SavingChanges1.btnSave.Enabled = True
    End Sub
    Friend Sub InitializeData()
        Me.LoadData()
    End Sub
    Private Sub RefreshData()
        Me.SFG = StateFillingGrid.Filling
        Me.ClearControl(Me.grpDistributor)
        Me.ClearControl(Me.grpProject)
        'Me.ChangeValueMode = SetValueMode.SetValueChanged
        Me.clsPO.CreateViewPOBrandPack("", False)
        Me.BindGrid(Me.clsPO.ViewPOBrandpack)
        If (Not IsNothing(Me.MultiColumnCombo1.Value)) And (Me.MultiColumnCombo1.SelectedIndex <> -1) Then
            Me.clsPO.CreateViewBrandPackByDistributorID(Me.MultiColumnCombo1.Value.ToString(), Convert.ToDateTime(Me.dtPicRefDate.Value.ToShortDateString()), True)
            'Me.ClearCategoriesValueList()
            Me.FillCategoriesValueList()
            'Me.FillCategoriesValueList(False)
            'Me.ChangeValueMode = SetValueMode.SetValueChanged
            Me.grdPOBrandPack.SetValue("BRANDPACK_ID", Nothing)
            'Me.clsPO.CreateViewPriceHistory(Me.MultiColumnCombo1.Value.ToString())
        Else
            Me.ClearCategoriesValueList()
        End If
        Me.txtPOReference.Enabled = True
        Me.grpDistributor.Enabled = False
        'Me.dtPicRefDate.Enabled = False
        Me.SavingChanges1.btnSave.Enabled = True
        'Me.dtPicRefDate.Value = NufarmBussinesRules.SharedClass.ServerDate()
    End Sub
    Private Sub LoadData()
        Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PO_BrandPack()
        If (Me.PORef <> "") Then
            Me.InflateData(Me.PORef)
            Me.dtPicRefDate.ReadOnly = True : Me.MultiColumnCombo1.ReadOnly = True
            If (Not IsNothing(Me.mcbProject.Value)) And (Not IsDBNull(Me.mcbProject.Value)) Then
                Me.mcbProject.ReadOnly = True
            Else
                Me.mcbProject.ReadOnly = False
            End If
        Else
            Me.clsPO.CreateViewPOBrandPack("", False)
            'Me.clsPO.CreateViewBrandPack("")
            Me.BindGrid(Me.clsPO.ViewPOBrandpack())
            Me.FillCategoriesValueList()
            'Me.FillCategoriesValueList(False)
            Me.clsPO.CreateViewDistributor(True)
            Me.BindMultiColumnCombo(Me.MultiColumnCombo1, Me.clsPO.ViewDistributor())
            Me.dtPicRefDate.ReadOnly = False : Me.MultiColumnCombo1.ReadOnly = False : Me.mcbProject.ReadOnly = False
            'Me.dtPicPORefDate.Value = NufarmBussinesRules.SharedClass.ServerDate()
        End If
    End Sub
    Private Sub BindGrid(ByVal objdsSource As Object)
        Me.SFG = StateFillingGrid.Filling
        Me.grdPOBrandPack.SetDataBinding(objdsSource, "")
        Me.FormatGridEx()
        Me.LockedColumnGrid()
        Me.SFG = StateFillingGrid.HasFilled
    End Sub
    Private Sub InflateData(ByVal PO_REF_NO As String)
        Me.MCBStateFilling = StateFilingMCB.Filling
        Me.txtPOReference.Text = Me.PORef
        Me.clsPO.CreateViewDistributor(False)
        Me.BindMultiColumnCombo(Me.MultiColumnCombo1, Me.clsPO.ViewDistributor())
        Me.MultiColumnCombo1.Value = Me.DistributorID
        Dim DV As DataView = CType(Me.MultiColumnCombo1.DataSource, DataView)
        DV.RowFilter = ""
        DV.Sort = "DISTRIBUTOR_ID DESC"
        Dim Index As Integer = DV.Find(Me.DistributorID)
        If Index <> -1 Then
            Me.txtDistributorContact.Text = DV(Index)("CONTACT").ToString()
            Me.txtDistributorPhone.Text = DV(Index)("PHONE").ToString()
            Me.txtTerritoryArea.Text = DV(Index)("TERRITORY_AREA").ToString()
            Me.txtRegionalArea.Text = DV(Index)("REGIONAL_AREA").ToString()
            Me.grdPOBrandPack.Enabled = True
        End If
        'Me.dtPicRefDate.Value = Me.PO_REF_DATE
        Me.dtPicRefDate.Text = Me.PO_REF_DATE.ToShortDateString()
        Me.clsPO.CreateViewPOBrandPack(PO_REF_NO, False)
        Me.BindGrid(Me.clsPO.ViewPOBrandpack())
        Dim DVProject As DataView = Me.clsPO.getProjectByDistributor(Me.PO_REF_DATE, Me.DistributorID, False)
        Me.BindMultiColumnCombo(Me.mcbProject, DVProject)
        If Not String.IsNullOrEmpty(Me.PROJ_REF_NO) Then
            ''bind mcb project by proj_ref_no
            Me.mcbProject.Value = Me.PROJ_REF_NO
            DVProject.Sort = "PROJ_REF_NO"
            Index = DVProject.Find(Me.PROJ_REF_NO)
            If Index <> -1 Then
                Me.txtProjectName.Text = DVProject(Index)("PROJECT_NAME").ToString()
                Me.txtProjectRefDate.Text = String.Format(DateTime.Parse(DVProject(Index)("START_DATE")), "dd MMMM yyyy") & " to " & String.Format(DateTime.Parse(DVProject(Index)("START_DATE")), "dd MMMM yyyy")
            End If
            DV = Me.clsPO.GetBrandPackByProject(Me.mcbProject.Value.ToString(), True)
            Me.grdPOBrandPack.DropDowns(0).SetDataBinding(DV, "")
        Else
            Me.clsPO.CreateViewBrandPackByDistributorID(Me.MultiColumnCombo1.Value.ToString(), Convert.ToDateTime(Me.dtPicRefDate.Value.ToShortDateString()), True)
            Me.FillCategoriesValueList()
        End If

        If Me.grdPOBrandPack.RecordCount() > 0 Then
            Me.unabledControl()
            Me.SavingChanges1.btnSave.Enabled = True
        Else
            Me.EnabledControl()
            Me.txtPOReference.Enabled = False
            Me.SavingChanges1.btnSave.Enabled = True
        End If
    End Sub
#End Region

#Region " Event Procedure "

#Region " TextBox "

    Private Sub txtPOReference_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPOReference.TextChanged
        Try
            If Not Me.isTypingPO Then : Return : End If
            If Me.txtPOReference.Text <> "" Then
                Me.grpDistributor.Enabled = True
                If Me.MultiColumnCombo1.SelectedIndex = -1 Then
                    Me.grdPOBrandPack.Enabled = False
                Else
                    Me.grdPOBrandPack.Enabled = True
                End If
                Me.dtPicRefDate.ReadOnly = False
                Me.MultiColumnCombo1.ReadOnly = False
            Else
                Me.MultiColumnCombo1.ReadOnly = True
                Me.dtPicRefDate.ReadOnly = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPOReference_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPOReference.Leave
        Me.isTypingPO = False
        If Not String.IsNullOrEmpty(Me.txtPOReference.Text.Trim()) Then
            Dim PO_REF_NO As String = ""
            If Me.Mode = ModeSave.Save Then
                PO_REF_NO = RTrim(Me.txtPOReference.Text)
                PO_REF_NO = LTrim(PO_REF_NO) : Me.clsPO.PO_REF_NO = PO_REF_NO
            Else
                PO_REF_NO = Me.txtPOReference.Text
            End If
            Me.txtPOReference.Text = PO_REF_NO
        End If
        Me.isTypingPO = True
    End Sub
#End Region

#Region " Multicolumn Combo "

    Private Sub MultiColumnCombo1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MultiColumnCombo1.ValueChanged
        Try
            If Not Me.HasLoad Then : Return : End If
            If Me.MCBStateFilling = StateFilingMCB.Filling Then : Return : End If
            If Me.MultiColumnCombo1.SelectedItem Is Nothing Then
                Me.MCBStateFilling = StateFilingMCB.Filling
                Me.grdPOBrandPack.Enabled = False : Me.ClearControl(Me.grpDistributor) : Me.ClearCategoriesValueList()
                Me.mcbProject.SetDataBinding(Nothing, "")
                Me.MCBStateFilling = StateFilingMCB.HasFilled
                Return
            End If
            If IsNothing(Me.MultiColumnCombo1.Value) Then
                Me.MCBStateFilling = StateFilingMCB.Filling
                Me.grdPOBrandPack.Enabled = False : Me.ClearControl(Me.grpDistributor) : Me.ClearCategoriesValueList()
                Me.mcbProject.SetDataBinding(Nothing, "")
                Me.MCBStateFilling = StateFilingMCB.HasFilled
                Return
            End If
            If Me.HasLoad = True Then
                If Me.txtPOReference.Visible Then
                    If (CObj(Me.dtPicRefDate.Value) Is Nothing) Or (Me.dtPicRefDate.Text = "") Then
                        Me.ShowMessageInfo("Please define PO_DATE.!")
                        Me.MCBStateFilling = StateFilingMCB.Filling : Me.ClearCategoriesValueList() : Me.MultiColumnCombo1.Text = ""
                        Me.MCBStateFilling = StateFilingMCB.HasFilled
                        Return
                    End If
                ElseIf Not IsNothing(Me.mcbPOHeader.Value) Or Me.mcbPOHeader.SelectedIndex <= -1 Then
                    Me.MCBStateFilling = StateFilingMCB.Filling : Me.ClearCategoriesValueList() : Me.MultiColumnCombo1.Text = ""
                    Me.MCBStateFilling = StateFilingMCB.HasFilled
                    Return
                End If
            End If
            If Me.MultiColumnCombo1.Enabled = False Then : Me.MultiColumnCombo1.Enabled = True : End If
            If Not IsNothing(Me.MultiColumnCombo1.DropDownList.GetValue("CONTACT")) Then
                If (Me.MultiColumnCombo1.DropDownList.GetValue("CONTACT") Is DBNull.Value) Or (Me.MultiColumnCombo1.DropDownList().GetValue("CONTACT").Equals("")) Then
                    Me.txtDistributorContact.Text = ""
                Else : Me.txtDistributorContact.Text = Me.MultiColumnCombo1.DropDownList().GetValue("CONTACT").ToString()
                End If
            Else : Me.txtDistributorContact.Text = "" 'Me.MultiColumnCombo1.DropDownList().GetValue("CONTACT")
            End If
            If Not IsNothing(Me.MultiColumnCombo1.DropDownList.GetValue("PHONE")) Then
                If (Me.MultiColumnCombo1.DropDownList.GetValue("PHONE") Is DBNull.Value) Or (Me.MultiColumnCombo1.DropDownList().GetValue("PHONE").Equals("")) Then
                    Me.txtDistributorPhone.Text = ""
                Else
                    Me.txtDistributorPhone.Text = Me.MultiColumnCombo1.DropDownList().GetValue("PHONE")
                End If
            Else
                Me.txtDistributorPhone.Text = ""
            End If
            Me.txtRegionalArea.Text = Me.MultiColumnCombo1.DropDownList.GetValue("REGIONAL_AREA").ToString()
            Me.txtTerritoryArea.Text = Me.MultiColumnCombo1.DropDownList.GetValue("TERRITORY_AREA").ToString()

            'check if any project can fill into mcb project
            Dim dv As DataView = Me.clsPO.getProjectByDistributor(Convert.ToDateTime(Me.dtPicRefDate.Value.ToShortDateString()), Me.MultiColumnCombo1.Value.ToString(), False)
            Me.BindMultiColumnCombo(Me.mcbProject, dv)
            '
            Me.clsPO.CreateViewBrandPackByDistributorID(Me.MultiColumnCombo1.Value.ToString(), Convert.ToDateTime(Me.dtPicRefDate.Value.ToShortDateString()), True)
            Me.FillCategoriesValueList()

            Me.grdPOBrandPack.Enabled = True
            Me.MCBStateFilling = StateFilingMCB.HasFilled
        Catch ex As System.IndexOutOfRangeException
            Me.LogMyEvent(ex.Message, Me.Name + "_MultiColumnCombo1_ValueChanged")
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_MultiColumnCombo1_ValueChanged")
        Finally
            If Me.MCBStateFilling = StateFilingMCB.Filling Then : Me.Cursor = Cursors.WaitCursor
            Else
                Me.Cursor = Cursors.Default
            End If

        End Try
    End Sub


#End Region

#Region " User control "

    Private Sub SavingChanges1_btnCloseClick_(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavingChanges1.btnCloseClick
        Try
            If Not IsNothing(Me.clsPO) Then
                If Me.clsPO.dsPPBrandPack.HasChanges() Then
                    Me.clsPO.dsPPBrandPack.RejectChanges()
                End If
                Me.clsPO.Dispose()
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SavingChanges1_btnSaveClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavingChanges1.btnSaveClick
        Try
            'Me.StateSaving = WhileSaving.Saving
            If Me.IsValid() = False Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            If Me.clsPO Is Nothing Then : Me.clsPO = New NufarmBussinesRules.PurchaseOrder.PO_BrandPack : End If
            If Me.grdPOBrandPack.RecordCount <= 0 Then
                Me.ShowMessageInfo("Data cannot be saved because has no BrandPack Included")
                Return
            End If
            If Not Me.clsPO.dsPPBrandPack.HasChanges() Then : Return : End If
            'Me.ShowProgress()
            'Me.StateSaving = WhileSaving.Saving
            Dim PO_REF_NO As String = ""
            If Me.Mode = ModeSave.Save Then
                PO_REF_NO = RTrim(Me.txtPOReference.Text)
                PO_REF_NO = LTrim(PO_REF_NO)
            Else
                If Me.txtPOReference.Visible = True Then
                    PO_REF_NO = Me.txtPOReference.Text
                ElseIf Me.mcbPOHeader.Visible = True Then
                    PO_REF_NO = Me.mcbPOHeader.Value.ToString()
                End If
            End If
            Me.clsPO.PO_REF_NO = PO_REF_NO
            Me.clsPO.PO_REF_DATE = Convert.ToDateTime(Me.dtPicRefDate.Value.ToShortDateString())
            Me.clsPO.DistributorID = Me.MultiColumnCombo1.Value.ToString()
            If Not IsNothing(Me.mcbProject.Value) And Not IsDBNull(Me.mcbProject.Value) Then
                Me.clsPO.PROJ_REF_NO = Me.mcbProject.Value
            End If
            If Not IsNothing(Me.clsPO.dsPPBrandPack) Then
                If Me.clsPO.dsPPBrandPack.HasChanges() Then
                    Me.clsPO.dsPOBrandPackHasChanged = True
                    Select Case Me.Mode
                        Case ModeSave.Save
                            Me.clsPO.SavePO("Save", Me.clsPO.dsPPBrandPack.GetChanges())
                        Case ModeSave.Update
                            Me.clsPO.SavePO("Update", Me.clsPO.dsPPBrandPack.GetChanges())
                    End Select
                Else
                    Select Case Me.Mode
                        Case ModeSave.Save
                            Me.clsPO.SavePO("Save", Nothing)
                        Case ModeSave.Update
                            Me.clsPO.SavePO("Update", Nothing)
                    End Select
                End If
            Else
                Select Case Me.Mode
                    Case ModeSave.Save
                        Me.clsPO.SavePO("Save", Nothing)
                    Case ModeSave.Update
                        Me.clsPO.SavePO("Update", Nothing)
                End Select
            End If
            Me.clsPO.dsPPBrandPack.AcceptChanges()
            'Me.ShowMessageInfo(Me.MessageSavingSucces)
            Me.Mode = ModeSave.Update
            Me.SavingChanges1.btnSave.Enabled = False
            If Not IsNothing(Me.frmParent) Then : frmParent.MustReload = True : End If
        Catch ex As DBConcurrencyException
            'Me.StateSaving = WhileSaving.Failed
            Me.ShowMessageInfo("(PO) BrandPack Detail Updated/Saved 0 record" & vbCrLf & "Some User Perhaps has changed the same data.")
            Me.LogMyEvent(ex.Message, Me.Name + "_SavingChanges1_btnSaveClick")
        Catch ex As Exception
            'Me.StateSaving = WhileSaving.Failed
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_SavingChanges1_btnSaveClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.MultiColumnCombo1.ReadOnly Then : Return : End If
            If Not IsNothing(Me.clsPO.ViewDistributor) Then
                Me.clsPO.ViewDistributor.RowFilter = "DISTRIBUTOR_NAME LIKE '%" + Me.MultiColumnCombo1.Text + "%'"
                Me.BindMultiColumnCombo(Me.MultiColumnCombo1, Me.clsPO.ViewDistributor())
                Dim itemCount As Integer = Me.MultiColumnCombo1.DropDownList().RecordCount()
                Me.Cursor = Cursors.Default
                Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterProject_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterProject.btnClick
        Try
            If Me.MultiColumnCombo1.SelectedIndex <= -1 Then
                Me.baseTooltip.Show("Please define distributor", Me.MultiColumnCombo1, 2500)
                Me.MultiColumnCombo1.Focus() : Return
            ElseIf Me.MultiColumnCombo1.Value Is Nothing Then
                Me.baseTooltip.Show("Please define distributor", Me.MultiColumnCombo1, 2500)
                Me.MultiColumnCombo1.Focus() : Return
            ElseIf String.IsNullOrEmpty(Me.MultiColumnCombo1.Text) Then
                Me.baseTooltip.Show("Please define distributor", Me.MultiColumnCombo1, 2500)
                Me.MultiColumnCombo1.Focus() : Return
            End If
            If Not Me.mcbProject.ReadOnly And Me.mcbProject.Enabled Then
                Me.Cursor = Cursors.WaitCursor
                Dim DV As DataView = Me.clsPO.getProjectByDistributor(Convert.ToDateTime(Me.dtPicRefDate.Value.ToShortDateString()), Me.MultiColumnCombo1.Value.ToString(), True, Me.mcbProject.Text.TrimStart().TrimEnd())
                Me.BindMultiColumnCombo(Me.mcbProject, DV)
                Dim itemCount As Integer = Me.mcbProject.DropDownList().RecordCount()
                Me.Cursor = Cursors.Default
                Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        End Try
    End Sub

    Private Sub btnNewPOHeader_PicClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewPOHeader.PicClick
        Me.Cursor = Cursors.WaitCursor
        Me.txtPOReference.Text = "" : Me.txtPOReference.Visible = False
        Me.mcbPOHeader.Visible = True : Me.mcbPOHeader.SelectedIndex = -1
        Try
            'LOAD TOP 100 PO ORDER BY DATE DESC
            Dim DV As DataView = Me.clsPO.getPOHeader(RTrim(Me.mcbPOHeader.Text))
            Me.BindMultiColumnCombo(Me.mcbPOHeader, DV)
            Me.mcbPOHeader.Focus()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnSearchPOHeader_btnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchPOHeader.btnClick
        Try
            If Me.txtPOReference.Visible = True Then : Return : End If
            Dim DV As DataView = Me.clsPO.getPOHeader(Me.mcbPOHeader.Text)
            Me.BindMultiColumnCombo(Me.mcbPOHeader, DV)
            Dim itemCount As String = Me.mcbPOHeader.DropDownList.RecordCount.ToString()
            Me.ShowMessageInfo(itemCount & " item(s) found ")
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " Event Form "

    Private Sub Purchase_Order_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.Mode = ModeSave.Save Then
                Me.dtPicRefDate.ReadOnly = True
                Me.grpDistributor.Enabled = False
                Me.dtPicRefDate.Value = NufarmBussinesRules.SharedClass.ServerDate()
            Else
                If Me.grdPOBrandPack.RecordCount > 0 Then
                    Me.unabledControl()
                    Me.grdPOBrandPack.Enabled = True
                    Me.SavingChanges1.btnSave.Enabled = True
                Else
                    Me.EnabledControl()
                    Me.txtPOReference.Enabled = False
                End If
                'Me.EnabledControl()
            End If
            Me.MCBStateFilling = StateFilingMCB.HasFilled

            Me.AcceptButton = Me.SavingChanges1.btnSave
            Me.CancelButton = Me.SavingChanges1.btnCLose
            'AddHandler SavingChanges1.btnSaveClick, AddressOf Me.Timer1_Tick
            'AddHandler grdPOBrandPack.Leave, AddressOf Me.Timer1_Tick
            If Me.Mode = ModeSave.Update Then
                Me.btnSearchPOHeader.Visible = True
                Me.btnNewPOHeader.Visible = True
            End If
        Catch ex As Exception
        Finally
            Me.HasLoad = True
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Purchase_Order_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Not IsNothing(Me.clsPO) Then
                If Not IsNothing(Me.clsPO.dsPPBrandPack) Then
                    If Me.clsPO.dsPPBrandPack.HasChanges() Then
                        If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                            Me.SavingChanges1_btnSaveClick(Me.SavingChanges1.btnSave, New System.EventArgs())
                            Me.clsPO.Dispose()
                            Return
                        End If
                    End If
                End If

            End If
        Catch ex As DBConcurrencyException
            Me.ShowMessageInfo("BrandPack Detail Updated 0 record" & vbCrLf & "Some User Perhaps has changed the same data.")
            Me.LogMyEvent(ex.Message, Me.Name + "_SavingChanges1_btnSaveClick")
        Catch ex As Exception
            Me.ShowMessageInfo("We are Sorry for the inconvenience of " & Me.MessageDataCantChanged & vbCrLf & "The System caught an exception with the folowing" & vbCrLf & ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Purchase_Order_FormClosing")
        End Try
    End Sub

#End Region

#Region " GridEx "

    Private Sub grdPOBrandPack_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdPOBrandPack.AddingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            'If Me.ChangeValueMode = SetValueMode.SetValueChanged Then : Return : End If
            'CHECK BRANDPACK_ID
            If IsNothing(Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID")) Then
                Me.ShowMessageInfo("PO_BRANDPACK_ID Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            ElseIf (Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID") Is DBNull.Value) Or (Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID").Equals("")) Then
                Me.ShowMessageInfo("PO_BRANDPACK_ID Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            End If
            'check PO_REF_NO
            If IsNothing(Me.grdPOBrandPack.GetValue("PO_REF_NO")) Then
                Me.ShowMessageInfo("PO_REF_NO Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            ElseIf (Me.grdPOBrandPack.GetValue("PO_REF_NO") Is DBNull.Value) Or (Me.grdPOBrandPack.GetValue("PO_REF_NO").Equals("")) Then
                Me.ShowMessageInfo("PO_REF_NO Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            End If
            'CHECK BRANDPACK_ID
            If IsNothing(Me.grdPOBrandPack.GetValue("BRANDPACK_ID")) Then
                Me.ShowMessageInfo("BRANDPACK Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            ElseIf Me.grdPOBrandPack.GetValue("BRANDPACK_ID") Is DBNull.Value Then
                Me.ShowMessageInfo("BRANDPACK Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            End If
            'CHECK QTY
            If IsNothing(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY")) Then
                Me.ShowMessageInfo("QUANTITY Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            ElseIf Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY") Is DBNull.Value Then
                Me.ShowMessageInfo("QUANTITY Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            ElseIf Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY") Is CObj("") Then
                Me.ShowMessageInfo("QUANTITY Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            ElseIf Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY") = 0 Then
                Me.ShowMessageInfo("QUANTITY Must not be null / ZERO")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            End If
            'CHECK PRICE/QTY
            If Me.grdPOBrandPack.GetValue("PO_PRICE_PERQTY") Is Nothing Then
                Me.ShowMessageInfo("PRICE/QTY Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            ElseIf Me.grdPOBrandPack.GetValue("PO_PRICE_PERQTY") Is DBNull.Value Then
                Me.ShowMessageInfo("PRICE/QTY Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            ElseIf Me.grdPOBrandPack.GetValue("PO_PRICE_PERQTY") = 0 Then
                Me.ShowMessageInfo("PRICE/QTY Must not be null / Zero")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            ElseIf Me.grdPOBrandPack.GetValue("PO_PRICE_PERQTY") Is CObj("") Then
                Me.ShowMessageInfo("PRICE/QTY Must not be null")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord()
                Return
            End If
            'CHECK EXISTING DATA IN SERVER
            If Me.clsPO.PO_BrandPackExisted(Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID").ToString()) = True Then
                Me.ShowMessageInfo("PO_BRANDPACK Has existed in Database !.")
                Me.grdPOBrandPack.CancelCurrentEdit() : Me.grdPOBrandPack.Refetch() : Me.grdPOBrandPack.MoveToNewRecord()
                Return
            End If
            ' CHECK IN DATAVIEW
            If Me.clsPO.ViewPOBrandpack().Find(Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID")) <> -1 Then
                Me.ShowMessageInfo("PO_BRANDPACK Has existed in DataView !.")
                Me.grdPOBrandPack.CancelCurrentEdit()
                'Me.grdPOBrandPack.Refetch()
                Me.grdPOBrandPack.MoveToNewRecord() : Return
            End If
            If IsPriceHK Then
                'check totalpencapaian qty
                Dim TargetHK As Decimal = 0
                Dim PO_DateString As String = Me.dtPicRefDate.Value.Month.ToString() & "/" & Me.dtPicRefDate.Value.Day.ToString() & "/" & Me.dtPicRefDate.Value.Year.ToString()
                Dim ProposedQty As Decimal = Convert.ToDecimal(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY"))
                Dim AchQTy As Decimal = Me.clsPO.GetTotalPOQTyByHK(Me.MultiColumnCombo1.Value.ToString(), _
                Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString(), PO_DateString, TargetHK)
                If TargetHK > 0 Then
                    Dim LeftQTy As Decimal = TargetHK - AchQTy
                    If (ProposedQty + AchQTy > TargetHK) Then
                        Me.ShowMessageInfo("You can only  enter Quantity " & String.Format("{0:#,##0.000}", LeftQTy) & " or less")
                        Me.grdPOBrandPack.CancelCurrentEdit()
                        'Me.grdPOBrandPack.Refetch()
                        Me.grdPOBrandPack.MoveToNewRecord()
                        Return
                    End If
                End If
            End If
            'Me.ChangeValueMode = SetValueMode.SetValueChanged
            Me.grdPOBrandPack.SetValue("CREATE_BY", NufarmBussinesRules.User.UserLogin.UserName)
            Me.grdPOBrandPack.SetValue("CREATE_DATE", NufarmBussinesRules.SharedClass.ServerDate())
            'Me.ChangeValueMode = SetValueMode.HasChanged
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_grdPOBrandPack_AddingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub grdPOBrandPack_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdPOBrandPack.DeletingRecord
        Try
            If Not Me.grdPOBrandPack.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Dim CreatedDate As Object = Nothing
            If Not IsNothing(Me.clsPO) Then
                If Me.clsPO.PO_BRANDPACK_HasReferencedData(Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID").ToString(), False) = True Then
                    Me.ShowMessageInfo(Me.MessageDataCantChanged)
                    Me.grdPOBrandPack.SelectCurrentCellText()
                    e.Cancel = True
                    Return
                End If
                If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.ChangeAgreement = True Or NufarmBussinesRules.User.UserLogin.IsAdmin Then
                Else
                    If Not IsNothing(Me.grdPOBrandPack.GetValue("CREATE_DATE")) And Not IsDBNull(Me.grdPOBrandPack.GetValue("CREATE_DATE")) Then
                        'chek apakah data sudah ada OA_ORIGINAL nya
                        CreatedDate = Convert.ToDateTime(Me.grdPOBrandPack.GetValue("CREATE_DATE"))
                        Dim nDay As Integer = DateDiff(DateInterval.Day, CreatedDate, NufarmBussinesRules.SharedClass.ServerDate)
                        If nDay > 3 Then
                            MessageBox.Show("Can not delete data" & vbCrLf & "data can be deleted before 3 days from which it is made" & vbCrLf & "You can contact Admin or SAE to delete PO", "Data has Already been inserted " & nDay.ToString() & " days before", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            e.Cancel = True
                            Return
                        End If
                    Else
                        Me.ShowMessageInfo("Can not delete data" & vbCrLf & "Unknown CreatedDate data")
                    End If
                End If
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Me.grdPOBrandPack.SelectCurrentCellText()
                Return
            End If
            e.Cancel = False
            Me.grdPOBrandPack.UpdateData()
        Catch ex As DBConcurrencyException
            Me.ShowMessageInfo("Data has been deleted by other user.")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdPOBrandPack_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPOBrandPack.Leave
        Me.StateSaving = WhileSaving.Saving
        If Not IsNothing(Me.clsPO.dsPPBrandPack) Then
            If Me.clsPO.dsPPBrandPack.HasChanges() Then
                If Me.HasLeave = True Then
                    Return
                End If
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.No Then
                    Me.HasLeave = True
                    Me.unabledControl()
                    Me.SavingChanges1.btnSave.Enabled = True
                Else
                    Me.SavingChanges1_btnSaveClick(Me.SavingChanges1.btnSave, New System.EventArgs())
                End If
            End If
        End If
    End Sub

    Private Sub grdPOBrandPack_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPOBrandPack.Enter
        Me.HasLeave = False
    End Sub

    Private Sub grdPOBrandPack_RecordAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPOBrandPack.RecordAdded
        Try
            Me.grdPOBrandPack.UpdateData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdPOBrandPack_DropDown(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdPOBrandPack.DropDown
        Try
            If CObj(Me.dtPicRefDate.Value) Is Nothing Then
                Me.ShowMessageInfo("Please define the Ref_Date!.")
                Me.grdPOBrandPack.CancelCurrentEdit()
                Me.grdPOBrandPack.MoveToNewRecord()
                Me.clsPO.ViewPriceHistory().RowFilter = ""
            ElseIf (Me.MultiColumnCombo1.Value Is Nothing) Or (Me.MultiColumnCombo1.SelectedIndex = -1) Then
                Me.ShowMessageInfo("Please define the Distributor.!")
                Me.grdPOBrandPack.CancelCurrentEdit()
                Me.grdPOBrandPack.MoveToNewRecord()
                Me.clsPO.ViewPriceHistory().RowFilter = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub grdPOBrandPack_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdPOBrandPack.CellUpdated
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            'If Me.ChangeValueMode = SetValueMode.SetValueChanged Then : Return : End If
            If Me.txtPOReference.Visible Then
                If Me.txtPOReference.Text = "" Then
                    Me.ShowMessageInfo("Please define PO_REF_NO first!.") : Me.grdPOBrandPack.CancelCurrentEdit()
                    'Me.grdPOBrandPack.Refetch()
                    Me.txtPOReference.Focus()
                    Return
                End If
            ElseIf Me.mcbPOHeader.Visible Then
                If Me.mcbPOHeader.Value Is Nothing Or Me.mcbPOHeader.SelectedIndex <= -1 Then
                    Me.ShowMessageInfo("Please define PO_REF_NO first!.")
                    Me.grdPOBrandPack.CancelCurrentEdit()
                    Me.mcbPOHeader.Focus()
                    Return
                End If
            End If
            If Not ((e.Column.Key = "BRANDPACK_ID") Or (e.Column.Key = "PO_ORIGINAL_QTY") Or (e.Column.Key.Contains("DESCRIPTIONS"))) Then : Return : End If
            Dim Price As Object = Nothing
            Dim BrandPackID As String = ""
            Dim PO_BRANDPACKID As String = ""
            Dim DescriptionPrice As String = "PRICE FROM FREE MARKET", PlantationID As String = "", TerritoryID As String = "", PriceTag As String = ""
            Dim scat As Category = Category.FreeMarket
            Dim isSPrice As Boolean = False, isGPrice As Boolean = False
            If e.Column.Key = "BRANDPACK_ID" Then
                If Me.MultiColumnCombo1.Value Is Nothing Then
                    Me.baseTooltip.Show("Please define distributor first", Me.MultiColumnCombo1, 2500)
                    Me.MultiColumnCombo1.Focus() : Return
                End If
                If Me.Mode = ModeSave.Update Then
                    If (Me.grdPOBrandPack.GetValue("BRANDPACK_ID") Is DBNull.Value) Or (Me.grdPOBrandPack.GetValue("BRANDPACK_ID") Is Nothing) Then
                        Me.ShowMessageInfo("You can not set null value for brandpack" & vbCrLf & "If you want to delete,Please press del")
                        Me.grdPOBrandPack.CancelCurrentEdit()
                        'Me.grdPOBrandPack.Refetch()
                        Return
                    End If
                    BrandPackID = Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString()
                    'chek apakah sudah ada PO_BRANDPACK NYA
                    If (Me.grdPOBrandPack.GetRow.RowType = Janus.Windows.GridEX.RowType.Record) Then
                        If Not IsNothing(Me.grdPOBrandPack.GetValue("PLANTATION_ID")) And Not IsDBNull(Me.grdPOBrandPack.GetValue("PLANTATION_ID")) Then
                            Me.ShowMessageInfo(Me.MessageDataCantChanged & vbCrLf & "ID Can not be updated")
                            Me.grdPOBrandPack.CancelCurrentEdit() : Return
                        ElseIf Me.clsPO.PO_BRANDPACK_HasReferencedData(Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID").ToString(), False) Then
                            Me.ShowMessageInfo(Me.MessageDataCantChanged & vbCrLf & "ID Can not be updated")
                            Me.grdPOBrandPack.CancelCurrentEdit() : Return
                        End If
                    End If
                Else
                    If Me.clsPO.ViewPOBrandpack().Find(Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID")) <> -1 Then
                        Me.ShowMessageInfo("PO_BRANDPACK Has existed in DataView !.")
                        Me.grdPOBrandPack.CancelCurrentEdit()
                        'Me.grdPOBrandPack.Refetch()
                        Me.grdPOBrandPack.MoveToNewRecord()
                        Return
                    End If
                    If (Me.grdPOBrandPack.GetValue("BRANDPACK_ID") Is DBNull.Value) Or (Me.grdPOBrandPack.GetValue("BRANDPACK_ID") Is Nothing) Then
                        'cancel edit
                        Me.grdPOBrandPack.CancelCurrentEdit() : Me.grdPOBrandPack.MoveToNewRecord()
                        Return
                    End If
                End If
                'check apakah brandpack_id ini harga khusus / belum
                BrandPackID = Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString()
                Dim PO_DateString As String = Me.dtPicRefDate.Value.Month.ToString() & "/" & Me.dtPicRefDate.Value.Day.ToString() & "/" & Me.dtPicRefDate.Value.Year.ToString()
                IsPriceHK = False

                ''CHECK APAKAH PROJ_REF_NO ADA 
                Dim ProjBrandPackID As Object = Nothing
                If Not IsNothing(Me.mcbProject.Value) Then
                    If Me.mcbProject.Text <> "" And Me.mcbProject.SelectedIndex <> -1 Then
                        'check price ke project
                        ProjBrandPackID = Me.grdPOBrandPack.DropDowns(0).GetValue("PROJ_BRANDPACK_ID")
                        If Not IsNothing(ProjBrandPackID) And Not IsDBNull(ProjBrandPackID) Then
                            Price = Me.grdPOBrandPack.DropDowns(0).GetValue("PRICE")
                            If Not IsNothing(Price) And Not IsDBNull(Price) Then
                                Price = Convert.ToDecimal(Price)
                                DescriptionPrice = "PRICE FROM PROJECT " & Me.mcbProject.Text.TrimStart().TrimEnd()
                            End If
                            Me.grdPOBrandPack.SetValue("PROJ_BRANDPACK_ID", ProjBrandPackID)
                        End If
                    End If
                ElseIf Not IsNothing(Me.MultiColumnCombo1.Value) Then
                    Price = Me.clsPO.GetPriceValue(Me.MultiColumnCombo1.Value.ToString(), Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString(), PO_DateString, IsPriceHK, DescriptionPrice, PriceTag, isSPrice, isGPrice)

                    If IsNothing(Price) Then
                        If isSPrice Or isGPrice Then
                            Dim frmDP As New DefinePrice()
                            With frmDP
                                .StartDate = PO_DateString
                                .BrandPackID = Me.grdPOBrandPack.GetValue(e.Column).ToString()
                                .DistributorID = Me.MultiColumnCombo1.Value.ToString()
                                If isSPrice Then
                                    .btnCatPlantation.Checked = True
                                Else
                                    .btnGeneralPlantation.Checked = True
                                End If
                                If (.ShowDialog(Price, DescriptionPrice, PlantationID, TerritoryID, PriceTag, scat) = Windows.Forms.DialogResult.OK) Then
                                Else
                                    Me.grdPOBrandPack.CancelCurrentEdit() : Me.grdPOBrandPack.MoveToNewRecord() : Cursor = Cursors.Default : Return
                                End If
                            End With
                        End If
                        'shw form choose price
                        If PriceTag = "" Then : ShowMessageError("Unknown PriceTag") : Me.grdPOBrandPack.CancelCurrentEdit() : Return : End If
                    End If
                End If
                'Price = Me.clsPO.GetPriceValue(Me.grdPOBrandPack.GetValue("BRANDPACK_ID"), Convert.ToDateTime(Me.dtPicRefDate.Value.ToShortDateString()))
                If (IsNothing(Price)) Or (IsDBNull(Price)) Then
                    Me.ShowMessageInfo("Price hasn't been set yet !.")
                    Me.grdPOBrandPack.CancelCurrentEdit()
                    Me.grdPOBrandPack.MoveToNewRecord()
                    'Me.clsPO.ViewPriceHistory().RowFilter = ""
                    Return
                End If

                'Dim ValuePrice As Decimal = Convert.ToDecimal(Me.clsPO.ViewPriceHistory()(Me.clsPO.ViewPriceHistory().Count - 1)("PRICE"))
                Dim ValuePrice As Decimal = Convert.ToDecimal(Price) 'Me.clsPO.GetPriceValue(Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString())
                Me.grdPOBrandPack.SetValue("PO_PRICE_PERQTY", ValuePrice)
                Me.grdPOBrandPack.SetValue("PO_REF_NO", Me.txtPOReference.Text)
                Me.grdPOBrandPack.SetValue("DESCRIPTIONS", DescriptionPrice)
                If String.IsNullOrEmpty(PlantationID) Then
                    Me.grdPOBrandPack.SetValue("PLANTATION_ID", DBNull.Value)
                Else
                    Me.grdPOBrandPack.SetValue("PLANTATION_ID", PlantationID)
                End If
                If String.IsNullOrEmpty(TerritoryID) Then
                    Me.grdPOBrandPack.SetValue("PLANTATION_ID", DBNull.Value)
                Else
                    Me.grdPOBrandPack.SetValue("TERRITORY_ID", TerritoryID)
                End If
                If String.IsNullOrEmpty(PriceTag) Then
                    Me.grdPOBrandPack.SetValue("PRICE_TAG", DBNull.Value)
                Else
                    Me.grdPOBrandPack.SetValue("PRICE_TAG", PriceTag)
                End If
                If Not IsNothing(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY")) Then
                    Dim QTY1 As Decimal = Convert.ToDecimal(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY"))
                    'Dim PRICEPERQTY1 As Decimal = ValuePrice
                    Dim Total1 As Decimal = Convert.ToDecimal(QTY1 * ValuePrice)
                    Me.grdPOBrandPack.SetValue("TOTAL", Total1)
                Else
                    Me.grdPOBrandPack.SetValue("TOTAL", 0)
                End If
                If Not IsNothing(ProjBrandPackID) Then
                    Me.grdPOBrandPack.SetValue("PRICE_CATEGORY", "PR")
                Else
                    Select Case scat
                        Case Category.FreeMarket
                            Me.grdPOBrandPack.SetValue("PRICE_CATEGORY", "FM")
                        Case Category.GenPricePlantation
                            Me.grdPOBrandPack.SetValue("PRICE_CATEGORY", "GP")
                        Case Category.SpecialPlantation
                            Me.grdPOBrandPack.SetValue("PRICE_CATEGORY", "SP")
                    End Select
                End If

                'If isSPrice Then

                'ElseIf isGPrice Then

                'ElseIf Not IsNothing(ProjBrandPackID) Then

                'Else

                'End If
                PO_BRANDPACKID = Me.txtPOReference.Text.TrimEnd().TrimStart() + "" + BrandPackID
                Me.grdPOBrandPack.SetValue("PO_BRANDPACK_ID", PO_BRANDPACKID)
            End If
            If e.Column.Key = "PO_ORIGINAL_QTY" Then
                'check referencedata
                If Not IsNothing(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY")) Then
                    If Me.grdPOBrandPack.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        If Convert.ToDecimal(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY")) <= 0 Then : Me.ShowMessageInfo("Can not insert 0 value") : Me.grdPOBrandPack.CancelCurrentEdit() : Return : End If
                        'check achqty yang bukan dari po_ini
                        Dim TargetHK As Decimal = 0
                        Dim PO_DateString As String = Me.dtPicRefDate.Value.Month.ToString() & "/" & Me.dtPicRefDate.Value.Day.ToString() & "/" & Me.dtPicRefDate.Value.Year.ToString()
                        Dim ProposedQty As Decimal = Convert.ToDecimal(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY"))
                        Dim AchQTy As Decimal = Me.clsPO.GetTotalPOQTyByHK(Me.MultiColumnCombo1.Value.ToString(), _
                        Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString(), PO_DateString, TargetHK, Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID").ToString())
                        If TargetHK > 0 Then
                            Dim LeftQTy As Decimal = TargetHK - AchQTy
                            If (ProposedQty + AchQTy > TargetHK) Then
                                Me.ShowMessageInfo("You can only  enter Quantity " & String.Format("{0:#,##0.000}", LeftQTy) & " or less ")
                                Me.grdPOBrandPack.CancelCurrentEdit()
                                Return
                            End If
                        End If
                        If Not IsNothing(Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID")) And Not IsDBNull(Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID")) Then
                            'check qty_oa apakah sama dengan yang di po
                            Dim OAQTy As Decimal = Me.clsPO.getTotalOAQtyByPOBrandPack(Me.grdPOBrandPack.GetValue("PO_BRANDPACK_ID"))
                            If OAQTy > 0 Then
                                If OAQTy > ProposedQty Then
                                    Me.ShowMessageInfo("You can only  enter Quantity >= " & String.Format("{0:#,##0.000}", OAQTy) & vbCrLf & "Because OAQTY is " & String.Format("{0:#,##0.000}", OAQTy))
                                    Me.grdPOBrandPack.CancelCurrentEdit()
                                    Return
                                ElseIf OAQTy < ProposedQty Then
                                    Me.ShowMessageInfo("Because OAQTY < POQty " & vbCrLf & "You must match OAQty with PO_OriginalQty" & vbCrLf & "After entry data PO." & vbCrLf & "OAQTY is " & String.Format("{0:#,##0.000}", OAQTy))
                                End If
                            End If
                        End If
                    End If
                    If Not IsNothing(Me.grdPOBrandPack.GetValue("PO_PRICE_PERQTY")) Then
                        Dim QTY1 As Decimal = Convert.ToDecimal(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY"))
                        Dim PRICEPERQTY1 As Decimal = Convert.ToDecimal(Me.grdPOBrandPack.GetValue("PO_PRICE_PERQTY"))
                        Dim Total1 As Decimal = QTY1 * PRICEPERQTY1
                        Me.grdPOBrandPack.SetValue("TOTAL", Total1)
                    Else
                        Me.grdPOBrandPack.SetValue("TOTAL", 0)
                    End If
                    If Not IsNothing(Me.grdPOBrandPack.GetValue("BRANDPACK_ID")) Then
                        BrandPackID = Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString()
                        PO_BRANDPACKID = Me.txtPOReference.Text.TrimEnd().TrimStart() + "" + BrandPackID
                        Me.grdPOBrandPack.SetValue("PO_BRANDPACK_ID", PO_BRANDPACKID)
                    End If
                    Me.grdPOBrandPack.SetValue("PO_REF_NO", Me.txtPOReference.Text)

                ElseIf Me.grdPOBrandPack.GetRow.RowType() = Janus.Windows.GridEX.RowType.Record Then
                    Me.ShowMessageInfo("Can not insert 0 value") : Me.grdPOBrandPack.CancelCurrentEdit() : Return
                Else
                    Me.grdPOBrandPack.SetValue("TOTAL", 0)
                End If
                If (Me.grdPOBrandPack.GetRow.RowType = Janus.Windows.GridEX.RowType.Record) Then
                    If Me.Mode = ModeSave.Update Then
                        'Me.ChangeValueMode = SetValueMode.SetValueChanged
                        Me.grdPOBrandPack.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                        Me.grdPOBrandPack.SetValue("MODIFY_DATE", NufarmBussinesRules.SharedClass.ServerDate)
                    End If
                End If
            End If
            If (Me.grdPOBrandPack.GetRow.RowType = Janus.Windows.GridEX.RowType.Record) Then : Me.grdPOBrandPack.UpdateData() : End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.grdPOBrandPack.CancelCurrentEdit()
            'Me.grdPOBrandPack.MoveToNewRecord()
            Me.LogMyEvent(ex.Message, Me.Name + "_grdPOBrandPack_CellValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " Button "

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.RefreshData()
            Me.Mode = ModeSave.Save
            Me.MCBStateFilling = StateFilingMCB.Filling
            Me.ClearControl(UiGroupBox1)
            Me.dtPicRefDate.ReadOnly = False
            Me.dtPicRefDate.ReadOnly = False
        Catch ex As Exception
        Finally
            Me.MCBStateFilling = StateFilingMCB.HasFilled
            Me.ChangeValueMode = SetValueMode.HasChanged
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnChekExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChekExisting.Click
        Try
            If Me.txtPOReference.Visible Then
                If Me.txtPOReference.Text <> "" Then
                    If Me.clsPO.IsExisted(Me.txtPOReference.Text.Trim()) = True Then
                        Me.ShowMessageInfo("PO Ref has Existed !.")
                    Else
                        Me.ShowMessageInfo("The value is safe to add.")
                    End If
                Else
                    Me.ShowMessageInfo("Please Define PO Ref !.")
                    Return
                End If
            ElseIf Me.mcbPOHeader.Visible Then
                If Me.clsPO.IsExisted(Me.mcbPOHeader.Value) = True Then
                    Me.ShowMessageInfo("PO Ref has Existed !.")
                Else
                    Me.ShowMessageInfo("The value is safe to add.")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtPicRefDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicRefDate.ValueChanged
        Try
            If Me.MCBStateFilling = StateFilingMCB.Filling Then
                Return
            End If
            If Me.HasLoad = False Then
                Return
            End If
            If Me.dtPicRefDate.Text = "" Then
                Me.grpDistributor.Enabled = False
                Return
            End If
            If Me.txtPOReference.Text = "" Then
                Me.ShowMessageInfo("Please define the PO_REF first.")
                Me.dtPicRefDate.ResetText()
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            If Me.grdPOBrandPack.RecordCount > 0 Then
                If Me.dtPicRefDate.Value < NufarmBussinesRules.SharedClass.ServerDate() Then
                    Me.dtPicRefDate.Value = NufarmBussinesRules.SharedClass.ServerDate()
                End If
            End If
            If Me.MultiColumnCombo1.SelectedIndex = -1 Then
                Me.ClearCategoriesValueList()
            Else
                Me.clsPO.CreateViewBrandPackByDistributorID(Me.MultiColumnCombo1.Value.ToString(), Convert.ToDateTime(Me.dtPicRefDate.Value.ToShortDateString()), True)
                Me.FillCategoriesValueList()
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#Region " Handler "
    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Application.DoEvents()
    '    If Me.StateSaving = WhileSaving.SuccesSaving Then
    '        For i As Integer = 0 To Me.ResultRandom
    '            For index As Integer = 1 To 30
    '                MyBase.ST.Refresh()
    '                MyBase.ST.Label1.Refresh()
    '                MyBase.ST.PictureBox1.Refresh()
    '            Next
    '        Next
    '        If Me.Mode = ModeSave.Update Then
    '            Me.LoadData()
    '        Else
    '            Me.SavingChanges1.btnSave.Enabled = False
    '            Me.unabledControl()
    '            Me.grdPOBrandPack.Enabled = False
    '            Me.btnAddNew.Enabled = True
    '        End If
    '        Me.CloseProgres(True)
    '    ElseIf Me.StateSaving = WhileSaving.Failed Then
    '        Me.CloseProgres(False)
    '    End If

    'End Sub
    Private Sub grdPOBrandPack_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdPOBrandPack.ColumnButtonClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.IsValid() Then : Return : End If
            If e.Column.Key = "PO_PRICE_PERQTY" Then
                Dim Price As Object = Nothing
                Dim PO_DateString As String = Me.dtPicRefDate.Value.Month.ToString() & "/" & Me.dtPicRefDate.Value.Day.ToString() & "/" & Me.dtPicRefDate.Value.Year.ToString()
                'IsPriceHK = False
                'Price = Me.clsPO.GetPriceValue(Me.MultiColumnCombo1.Value.ToString(), Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString(), PO_DateString, IsPriceHK)
                If Me.grdPOBrandPack.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                    If Me.grdPOBrandPack.GetValue("BRANDPACK_ID") Is Nothing Or Me.grdPOBrandPack.GetValue("BRANDPACK_ID") Is DBNull.Value Then
                        Cursor = Cursors.Default
                        Return
                    End If
                End If
                If Not IsNothing(Me.mcbProject.Value) Then
                    Me.Cursor = Cursors.Default
                    Return
                End If
                Dim DescriptionPrice As String = "PRICE FROM FREE MARKET", PlantationID As String = "", TerritoryID As String = "", PriceTag As String = ""
                Dim scat As Category = Category.FreeMarket
                Dim isSPrice As Boolean = False, isGPrice As Boolean = False
                If Me.clsPO.hasPricePlantation(Me.MultiColumnCombo1.Value.ToString(), Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString(), PO_DateString, isGPrice, isSPrice) Then
                    If isSPrice Or isGPrice Then
                        Dim frmDP As New DefinePrice()
                        With frmDP  'show form choose price
                            .StartDate = PO_DateString
                            .BrandPackID = Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString()
                            .DistributorID = Me.MultiColumnCombo1.Value.ToString()
                            If isSPrice Then
                                .btnCatPlantation.Checked = True
                            Else
                                .btnGeneralPlantation.Checked = True
                            End If
                            If (.ShowDialog(Price, DescriptionPrice, PlantationID, TerritoryID, PriceTag, scat) = Windows.Forms.DialogResult.OK) Then
                            Else
                                Me.grdPOBrandPack.CancelCurrentEdit() : Me.grdPOBrandPack.MoveToNewRecord()
                            End If
                        End With
                    End If
                Else
                    Cursor = Cursors.Default
                    MessageBox.Show("There is no plantation price for the item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
                'If Not IsNothing(Price) Then
                '    'Me.ChangeValueMode = SetValueMode.SetValueChanged
                '    'Dim ValuePrice As Decimal = Convert.ToDecimal(Me.clsPO.ViewPriceHistory()(Me.clsPO.ViewPriceHistory().Count - 1)("PRICE"))
                '    Dim ValuePrice As Decimal = Convert.ToDecimal(Price) 'Me.clsPO.GetPriceValue(Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString())
                '    Me.grdPOBrandPack.SetValue("PO_PRICE_PERQTY", ValuePrice)
                '    Me.grdPOBrandPack.SetValue("DESCRIPTIONS", DescriptionPrice)
                '    If String.IsNullOrEmpty(PlantationID) Then
                '        Me.grdPOBrandPack.SetValue("PLANTATION_ID", DBNull.Value)
                '    Else
                '        Me.grdPOBrandPack.SetValue("PLANTATION_ID", PlantationID)
                '    End If
                '    If String.IsNullOrEmpty(TerritoryID) Then
                '        Me.grdPOBrandPack.SetValue("TERRITORY_ID", DBNull.Value)
                '    Else
                '        Me.grdPOBrandPack.SetValue("TERRITORY_ID", TerritoryID)
                '    End If
                '    If Not IsNothing(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY")) Then
                '        Dim QTY1 As Decimal = Convert.ToDecimal(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY"))
                '        Dim Total1 As Decimal = Convert.ToDecimal(QTY1 * ValuePrice)
                '        Me.grdPOBrandPack.SetValue("TOTAL", Total1)
                '    Else
                '        Me.grdPOBrandPack.SetValue("TOTAL", CObj(0))
                '    End If
                '    If Me.Mode = ModeSave.Update Then
                '        Me.grdPOBrandPack.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                '        Me.grdPOBrandPack.SetValue("MODIFY_DATE", NufarmBussinesRules.SharedClass.ServerDate)
                '    End If
                '    Me.grdPOBrandPack.UpdateData()
                'End If
                If (IsNothing(Price)) Or (IsDBNull(Price)) Then
                    Me.ShowMessageInfo("Price hasn't been set yet !.")
                    Me.grdPOBrandPack.CancelCurrentEdit()
                    'Me.grdPOBrandPack.MoveToNewRecord()
                    'Me.clsPO.ViewPriceHistory().RowFilter = ""
                    Cursor = Cursors.Default
                    Return
                End If

                'Dim ValuePrice As Decimal = Convert.ToDecimal(Me.clsPO.ViewPriceHistory()(Me.clsPO.ViewPriceHistory().Count - 1)("PRICE"))
                Dim ValuePrice As Decimal = Convert.ToDecimal(Price) 'Me.clsPO.GetPriceValue(Me.grdPOBrandPack.GetValue("BRANDPACK_ID").ToString())
                Me.grdPOBrandPack.SetValue("PO_PRICE_PERQTY", ValuePrice)
                Me.grdPOBrandPack.SetValue("PO_REF_NO", Me.txtPOReference.Text)
                Me.grdPOBrandPack.SetValue("DESCRIPTIONS", DescriptionPrice)
                If String.IsNullOrEmpty(PlantationID) Then
                    Me.grdPOBrandPack.SetValue("PLANTATION_ID", DBNull.Value)
                Else
                    Me.grdPOBrandPack.SetValue("PLANTATION_ID", PlantationID)
                End If
                If String.IsNullOrEmpty(TerritoryID) Then
                    Me.grdPOBrandPack.SetValue("PLANTATION_ID", DBNull.Value)
                Else
                    Me.grdPOBrandPack.SetValue("TERRITORY_ID", TerritoryID)
                End If
                If String.IsNullOrEmpty(PriceTag) Then
                    Me.grdPOBrandPack.SetValue("PRICE_TAG", DBNull.Value)
                Else
                    Me.grdPOBrandPack.SetValue("PRICE_TAG", PriceTag)
                End If
                If Not IsNothing(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY")) Then
                    Dim QTY1 As Decimal = Convert.ToDecimal(Me.grdPOBrandPack.GetValue("PO_ORIGINAL_QTY"))
                    'Dim PRICEPERQTY1 As Decimal = ValuePrice
                    Dim Total1 As Decimal = Convert.ToDecimal(QTY1 * ValuePrice)
                    Me.grdPOBrandPack.SetValue("TOTAL", Total1)
                Else
                    Me.grdPOBrandPack.SetValue("TOTAL", 0)
                End If
                Select Case scat
                    Case Category.FreeMarket
                        Me.grdPOBrandPack.SetValue("PRICE_CATEGORY", "FM")
                    Case Category.GenPricePlantation
                        Me.grdPOBrandPack.SetValue("PRICE_CATEGORY", "GP")
                    Case Category.SpecialPlantation
                        Me.grdPOBrandPack.SetValue("PRICE_CATEGORY", "SP")
                End Select
            End If
        Catch ex As Exception
            'Me.ChangeValueMode = SetValueMode.HasChanged : MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#End Region

    Private Sub mcbPOHeader_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbPOHeader.ValueChanged
        Try
            If Me.MCBStateFilling = StateFilingMCB.Filling Then : Return : End If
            If IsNothing(Me.mcbPOHeader.Value) Or Me.mcbPOHeader.SelectedIndex <= -1 Then
                Me.dtPicRefDate.Text = "" : Me.MultiColumnCombo1.Text = ""
                Me.ClearControl(Me.grpDistributor) : Me.grdPOBrandPack.SetDataBinding(Nothing, "") : Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim dtTable As DataTable = Me.clsPO.getPODateAndDistributorID(Me.mcbPOHeader.Value.ToString())
            If dtTable.Rows.Count > 0 Then
                Me.MCBStateFilling = StateFilingMCB.Filling
                Me.dtPicRefDate.Value = Convert.ToDateTime(dtTable.Rows(0)("PO_REF_DATE"))
                Dim DV As DataView = CType(Me.MultiColumnCombo1.DataSource, DataView)
                DV.Sort = "DISTRIBUTOR_ID " : DV.RowFilter = ""
                Dim index As Integer = DV.Find(dtTable.Rows(0)("DISTRIBUTOR_ID"))
                If index <= -1 Then
                    DV = Me.clsPO.CreateViewDistributor(False)
                End If
                index = DV.Find(dtTable.Rows(0)("DISTRIBUTOR_ID"))
                If index <> -1 Then
                    Me.EnabledControl() : Me.dtPicRefDate.ReadOnly = True
                End If
                Me.MultiColumnCombo1.Value = dtTable.Rows(0)("DISTRIBUTOR_ID").ToString()
                Me.txtDistributorContact.Text = DV(index)("CONTACT").ToString()
                Me.txtDistributorPhone.Text = DV(index)("PHONE").ToString()
                Me.txtTerritoryArea.Text = DV(index)("TERRITORY_AREA").ToString()
                Me.txtRegionalArea.Text = DV(index)("REGIONAL_AREA").ToString()
                Me.clsPO.CreateViewPOBrandPack(Me.mcbPOHeader.Value, False)
                Me.BindGrid(Me.clsPO.ViewPOBrandpack())
                Me.clsPO.CreateViewBrandPackByDistributorID(Me.MultiColumnCombo1.Value.ToString(), Convert.ToDateTime(Me.dtPicRefDate.Value.ToShortDateString()), True)
                Me.FillCategoriesValueList() : Me.Mode = ModeSave.Update : Me.SavingChanges1.btnSave.Enabled = True

            End If
            'getitem by 
            'fill dtpicrefdate ,fill mcbdistributor,create
            Me.MCBStateFilling = StateFilingMCB.HasFilled
        Catch ex As Exception
            Me.MCBStateFilling = StateFilingMCB.HasFilled : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdPOBrandPack_CellEdited(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdPOBrandPack.CellEdited
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        Me.SavingChanges1.btnSave.Enabled = True
    End Sub

    Private Sub mcbProject_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbProject.ValueChanged
        Try
            If Not Me.HasLoad Then : Return : End If
            If Me.MCBStateFilling = StateFilingMCB.Filling Then : Return : End If
            If IsNothing(Me.mcbProject.Value) Then
                Me.txtProjectRefDate.Text = String.Empty
                Me.txtProjectName.Text = String.Empty
                Return
            End If
            If Me.mcbProject.SelectedIndex <= -1 Then
                Me.txtProjectRefDate.Text = String.Empty
                Me.txtProjectName.Text = String.Empty
                Return
            End If
            If String.IsNullOrEmpty(Me.mcbProject.Text) Then
                Me.txtProjectRefDate.Text = String.Empty
                Me.txtProjectName.Text = String.Empty
                Return
            End If
            'CHECK KALO MUNGKIN DATA SUDAH ADA DI OA    
            If Not IsNothing(Me.grdPOBrandPack.DataSource) Then
                If Me.grdPOBrandPack.RecordCount > 0 Then
                    Dim listPOBrandPackIDS As New List(Of String)
                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdPOBrandPack.GetDataRows()
                        If Not listPOBrandPackIDS.Contains(row.Cells("PO_BRANDPACK_ID").Value.ToString()) Then
                            listPOBrandPackIDS.Add(row.Cells("PO_BRANDPACK_ID").Value.ToString())
                        End If
                    Next
                    If Me.clsPO.IsHasReferencceIDOA(listPOBrandPackIDS) Then
                        Me.ShowMessageInfo(Me.MessageDataCantChanged) : Return
                    End If
                End If
            End If
            If Not IsNothing(Me.clsPO.dsPPBrandPack) Then
                If Not IsNothing(Me.grdPOBrandPack.DataSource) Then
                    'set drop down
                    If Me.clsPO.dsPPBrandPack.HasChanges() Then
                        If Me.ShowConfirmedMessage(Me.MessageDataHasChanged & vbCrLf & "Are you sure you want to change to the project ?" & vbCrLf & "All changes will be discarded !!") = Windows.Forms.DialogResult.No Then
                            Return
                        End If
                    End If
                End If
            End If
            ''DISPOSe DATASOURCE
            Me.Cursor = Cursors.WaitCursor
            Me.SFG = StateFillingGrid.Filling
            Me.clsPO.dsPPBrandPack.RejectChanges()
            Me.clsPO.dsPPBrandPack.Tables(0).Rows.Clear()
            Me.clsPO.dsPPBrandPack.AcceptChanges()

            Me.BindGrid(Me.clsPO.dsPPBrandPack.Tables(0).DefaultView())
            ''get brandpack by distributor
            Dim DV As DataView = Me.clsPO.GetBrandPackByProject(Me.mcbProject.Value.ToString(), True)
            Me.grdPOBrandPack.DropDowns(0).SetDataBinding(DV, "")
            'ISI data project
            Me.txtProjectName.Text = Me.mcbProject.DropDownList.GetValue("PROJECT_NAME").ToString()
            Me.txtProjectRefDate.Text = String.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(Me.mcbProject.DropDownList.GetValue("START_DATE"))) & " to " & String.Format("{0:dd MMMM yyyy}", Convert.ToDateTime(Me.mcbProject.DropDownList.GetValue("END_DATE")))

        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdPOBrandPack_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPOBrandPack.CurrentCellChanged
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If

        Me.grdPOBrandPack.RootTable.Columns("DESCRIPTIONS2").EditType = Janus.Windows.GridEX.EditType.DropDownList
        Dim ColRemark As Janus.Windows.GridEX.GridEXColumn = grdPOBrandPack.RootTable.Columns("DESCRIPTIONS2")
        ColRemark.EditType = Janus.Windows.GridEX.EditType.DropDownList
        'Set HasValueList property equal to true in order to be able to use the ValueList property
        ColRemark.HasValueList = True
        'Get the ValueList collection associated to this column
        Dim ValueList As Janus.Windows.GridEX.GridEXValueListItemCollection = ColRemark.ValueList
        Dim Arr(15) As String
        With Arr
            .SetValue("OVER DUE", 0)
            .SetValue("CREDIT LIMIT", 1)
            .SetValue("PROGRAM", 2)
            .SetValue("SCHEDULE OF SHIPMENT", 3)
            .SetValue("QUOTA SHIPMENT", 4)
            .SetValue("PRICE FROM PLANTATION", 5)
            .SetValue("OVERDUE - CREDIT LIMIT", 6)
            .SetValue("BANK GUARANTEE", 7)
            .SetValue("DISTRIBUTOR PROGRAM", 8)
            .SetValue("RETAILER PROGRAM", 9)
            .SetValue("PLANTATION PROGRAM", 10)
            .SetValue("PLANTATION PO", 11)
            .SetValue("PO REVISE", 12)
            .SetValue("ON CSE PROCESS", 13)
            .SetValue("ON LOGISTIC PROCESS", 14)
            .SetValue("PROGRAM SUBMISSION", 15)
        End With
        Array.Sort(Arr)
        'Dim ListStatus() As String = {"TIDAK ADA STOCK", "STOCK_UNAVAILABLE", "AWAITING_TRANSPORTER", "QUOTA_SHIPMENT", "SHIPPED", "COMPLETED", "OTHER"}
        ValueList.PopulateValueList(Arr, "DESCRIPTIONS2")
        ColRemark.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        ColRemark.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text


    End Sub

End Class
