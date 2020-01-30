Imports System.Threading
Public Class Project
#Region " Deklarasi "
    Private clsProj As NufarmBussinesRules.DistributorProject.ProjectRegistering
    Private ModeSave As Mode
    Private StateFilling As FillingGrid
    Private StateFillingMCB As FillingMCB
    Private WhileSaving As Saving
    Private SelectGrid As GridSelect
    Private CVM As ChangeValueMultiClumnCombo
    Private OriginalStart_Date As Date, OriginalEndDate As DateTime
    Private HasLoad As Boolean
    Private grpEditHeight As Integer = 0
    Private LD As Loading
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    'Private ListOriginalBrandPack As List(Of String)
    Private DProject As DomainProject = Nothing
    Private DSBrandPack As DataSet = Nothing
    Dim ChekedBrandPackIDS As New List(Of String)
    Private ProjRefNo As String = ""
    Private Class DomainProject
        Public ProjRefNo As String = ""
        Public StartDate As Date = DateTime.Now
        Public EndDate As Date = DateTime.Now
        Public DistributorID As String = ""
        Public ProjRefName As String = ""
        Public Address As String = ""
    End Class
#End Region

#Region " Enum "
    Private Enum Mode
        Save
        Update
    End Enum
    Private Enum FillingGrid
        HasFilled
        Filling
    End Enum
    Private Enum FillingMCB
        HasFilled
        Filling
    End Enum
    Private Enum Saving
        Succes
        Failed
        Waiting
    End Enum
    Private Enum GridSelect
        GridEx2
        GridEx1
    End Enum
    Private Enum ChangeValueMultiClumnCombo
        FromSelectedGrid
        FromSelectedMulticolumnCombo
    End Enum
    Private Enum StatusProgress
        None
        Loading
        Processing
    End Enum
#End Region

#Region " Sub "
    Private Function ValidHeader() As Boolean
        If Me.txtProjRefNo.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.txtProjRefNo, "PROJECT REF is Null !." & vbCrLf & "Project Ref must be Suplied.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtProjRefNo), Me.txtProjRefNo, 2500)
            Me.txtProjRefNo.Focus() : Me.txtProjRefNo.SelectionLength = Me.txtProjRefNo.Text.Length
            Return False
        ElseIf Me.txtProjName.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.txtProjName, "Project name is Null !." & vbCrLf & "Project Name must not be null.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtProjName), Me.txtProjName, 2500)
            Me.txtProjName.Focus() : Me.txtProjName.SelectionLength = Me.txtProjName.Text.Length
            Return False
        ElseIf (Me.MultiColumnCombo1.Text = "") Or (Me.MultiColumnCombo1.SelectedIndex = -1) Then
            Me.baseTooltip.SetToolTip(Me.MultiColumnCombo1, "Distributor Name is Null !." & vbCrLf & "Distributor Name Must be Defined.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.MultiColumnCombo1), Me.MultiColumnCombo1, 2500)
            Me.MultiColumnCombo1.Focus()
            Return False
            'ElseIf IsNothing(Me.mcbBrandPack.CheckedValues) Then
            '    Me.baseTooltip.SetToolTip(Me.MultiColumnCombo1, "BrandPack is Null !." & vbCrLf & "BrandPack Must be Defined.")
            '    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2500)
            '    Me.MultiColumnCombo1.Focus()
            '    Return False
            'ElseIf Me.mcbBrandPack.CheckedValues.Length <= 0 Then
            '    Me.baseTooltip.SetToolTip(Me.MultiColumnCombo1, "BrandPack is Null !." & vbCrLf & "BrandPack Must be Defined.")
            '    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2500)
            '    Me.MultiColumnCombo1.Focus()
            '    Return False
        ElseIf Me.dtPicStartDate.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.dtPicStartDate, "Project Date !." & vbCrLf & "Project date must be defined.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dtPicStartDate), Me.dtPicStartDate, 2500)
            Me.dtPicStartDate.Focus()
            Return False
        End If
    End Function
    Private Sub ShowProceed()
        Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Me.LD.Refresh() : Thread.Sleep(100) : Application.DoEvents()
        End While
        Thread.Sleep(100) : Me.LD.Close() : Me.LD = Nothing
    End Sub
    Private Sub ShowThread()
        'Me.mcbDistributor.Enabled = False
        Me.StatProg = StatusProgress.Loading
        Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
        Me.ThreadProgress.Start()
    End Sub
    Private Sub ReadAcces()
        If Not Main.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Project = True Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If (NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Project = True) And (NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Project = True) Then
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Project = True Then
                Me.btnAddNew.Visible = True
                Me.btnEdit.Visible = False
                Me.btnSave.Enabled = True
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Project = True Then
                Me.btnEdit.Visible = True
                Me.btnAddNew.Visible = False
                Me.btnSave.Enabled = True
            Else
                Me.btnAddNew.Visible = False
                Me.btnEdit.Visible = False
                Me.btnSave.Enabled = False
            End If
        End If
    End Sub
    Private Sub LockedColumCreateModify(ByVal GRD As Janus.Windows.GridEX.GridEX)
        For Each item As Janus.Windows.GridEX.GridEXColumn In GRD.RootTable.Columns
            If (item.DataMember = "CREATE_BY") Or (item.DataMember = "CREATE_DATE") _
            Or (item.DataMember = "MODIFY_BY") Or (item.DataMember = "MODIFY_DATE") Then
                item.EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
        Next
    End Sub

    Private Sub UnabledControl()
        Me.txtProjRefNo.Enabled = False
        Me.txtAddress.Enabled = False
        Me.txtProjName.Enabled = False
        Me.MultiColumnCombo1.Enabled = False
        Me.dtPicStartDate.ReadOnly = True
        Me.btnApply.Enabled = False
        Me.btnSave.Enabled = False
        'Me.btnAplyBrandPack.Enabled = False
    End Sub
    Private Sub EnabledControl()
        Me.txtProjRefNo.Enabled = True
        Me.txtAddress.Enabled = True
        Me.txtProjName.Enabled = True
        Me.MultiColumnCombo1.Enabled = True
        Me.dtPicStartDate.ReadOnly = False
        Me.btnApply.Enabled = True
        Me.btnSave.Enabled = True
        'Me.btnAplyBrandPack.Enabled = True
    End Sub
    'Private Sub LockedColumnBrandPackRef()
    '    Me.GridEX2.RootTable.Columns("PROJ_BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '    Me.GridEX2.RootTable.Columns("PROJ_REF_NO").EditType = Janus.Windows.GridEX.EditType.NoEdit
    'End Sub
    'Private Sub RefreshData()
    '    Me.StateFilling = FillingGrid.Filling
    '    Me.LoadData()
    'End Sub
    'Private Sub LoadData()
    '    Me.clsProj = New NufarmBussinesRules.DistributorProject.ProjectRegistering()
    '    Me.clsProj.FetchDataSet()
    '    Me.StateFilling = FillingGrid.Filling
    '    Me.BindGrid(Me.GridEX1, Me.clsProj.GetDataViewForGridEx())
    '    Me.StateFillingMCB = FillingMCB.Filling
    '    'bindmulticolumn combo dengan distributor yang udah commite dengan agreement dan agreement tsb masih berlaku
    '    'besok sudah ,kita so capek kwa
    '    Me.clsProj.CreateViewDistributor()
    '    Me.BindMultiColumnCombo(Me.clsProj.ViewDistributor(), "")
    '    Me.StateFillingMCB = FillingMCB.HasFilled
    '    Me.MultiColumnCombo1.Text = ""
    '    Me.BindGrid(Me.GridEX1, Me.clsProj.GetDataViewForGridEx())
    'End Sub
    'Private Sub AddConditonalFormating1()
    '    Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.GridEX1.RootTable.Columns("END_DATE"), Janus.Windows.GridEX.ConditionOperator.LessThan, CObj(NufarmBussinesRules.SharedClass.ServerDate()))
    '    fc.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
    '    fc.FormatStyle.ForeColor = SystemColors.GrayText
    '    GridEX1.RootTable.FormatConditions.Add(fc)
    'End Sub
    'Private Sub AddConditionalFormatting2()
    '    Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.GridEX2.RootTable.Columns("END_DATE"), Janus.Windows.GridEX.ConditionOperator.LessThan, CObj(NufarmBussinesRules.SharedClass.ServerDate()))
    '    fc.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
    '    fc.FormatStyle.ForeColor = SystemColors.GrayText
    '    GridEX2.RootTable.FormatConditions.Add(fc)
    'End Sub
    Private Sub BindMultiColumnCombo(ByVal dtView As DataView, ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Me.StateFillingMCB = FillingMCB.Filling
        mcb.SetDataBinding(dtView, "") : mcb.Text = "" : mcb.SelectedIndex = -1
        If (dtView Is Nothing) Then : Return : End If
        mcb.DropDownList().RetrieveStructure()
        ''format combo box
        mcb.DroppedDown = True : System.Threading.Thread.Sleep(100)
        For Each col As Janus.Windows.GridEX.GridEXColumn In mcb.DropDownList.Columns
            col.AutoSize()
        Next
        Select Case mcb.Name
            Case "mcbBrandPack"
                mcb.DisplayMember = "BRANDPACK_NAME"
                mcb.ValueMember = "BRANDPACK_ID"
            Case "mcbDistributor"
                mcb.DisplayMember = "DISTRIBUTOR_NAME"
                mcb.ValueMember = "DISTRIBUTOR_ID"
        End Select
        mcb.DroppedDown = False
        Me.StateFillingMCB = FillingMCB.HasFilled
    End Sub
    'Private Sub bindCheckedCombo(ByVal dtview As DataView, ByVal DisplayMember As String, ByVal ValueMember As String)
    '    Me.StateFillingMCB = FillingMCB.Filling
    '    If IsNothing(dtview) Then
    '        Me.mcbBrandPack.Text = "" : Me.mcbBrandPack.DropDownDataSource = Nothing : Me.StateFillingMCB = FillingMCB.HasFilled : Return
    '    End If
    '    Me.mcbBrandPack.DropDownDataSource = dtview
    '    Me.mcbBrandPack.DropDownDisplayMember = DisplayMember : Me.mcbBrandPack.DropDownValueMember = ValueMember
    '    Me.mcbBrandPack.RetrieveStructure()
    '    Me.mcbBrandPack.DropDownList().Columns("BRANDPACK_ID").UseHeaderSelector = True
    '    Me.mcbBrandPack.DroppedDown = True
    '    For Each col As Janus.Windows.GridEX.GridEXColumn In Me.mcbBrandPack.DropDownList.Columns
    '        If col.Visible Then : col.AutoSize() : End If
    '    Next
    '    Me.mcbBrandPack.DropDownList.UnCheckAllRecords() : Me.mcbBrandPack.DroppedDown = False
    '    Me.StateFillingMCB = FillingMCB.HasFilled
    'End Sub
    'Private Sub FillCategoriesValueList(ByVal DISTRIBUTOR_ID As String)
    '    Me.clsProj.CreateViewBrandPack(DISTRIBUTOR_ID, True)
    '    Dim colBrandPackID As Janus.Windows.GridEX.GridEXColumn = GridEX2.RootTable.Columns("BRANDPACK_ID")
    '    colBrandPackID.EditType = Janus.Windows.GridEX.EditType.DropDownList
    '    'Set HasValueList property equal to true in order to be able to use the ValueList property
    '    colBrandPackID.HasValueList = True
    '    'Get the ValueList collection associated to this column
    '    Dim ValueListBPID As Janus.Windows.GridEX.GridEXValueListItemCollection = colBrandPackID.ValueList
    '    ValueListBPID.PopulateValueList(Me.clsProj.ViewBrandPack(), "BRANDPACK_ID", "BRANDPACK_NAME")
    '    colBrandPackID.EditTarget = Janus.Windows.GridEX.EditTarget.Text
    '    colBrandPackID.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    'End Sub
    'Private Sub FillCategoriesValueList()
    '    Me.clsProj.CreateViewBrandPack()
    '    Dim colBrandPackID As Janus.Windows.GridEX.GridEXColumn = GridEX2.RootTable.Columns("BRANDPACK_ID")
    '    colBrandPackID.EditType = Janus.Windows.GridEX.EditType.DropDownList
    '    'Set HasValueList property equal to true in order to be able to use the ValueList property
    '    colBrandPackID.HasValueList = True
    '    'Get the ValueList collection associated to this column
    '    Dim ValueListBPID As Janus.Windows.GridEX.GridEXValueListItemCollection = colBrandPackID.ValueList
    '    ValueListBPID.PopulateValueList(Me.clsProj.ViewBrandPack(), "BRANDPACK_ID", "BRANDPACK_NAME")
    '    colBrandPackID.EditTarget = Janus.Windows.GridEX.EditTarget.Text
    '    colBrandPackID.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    '    Me.GridEX2.Refetch()
    'End Sub
    'Private Sub FillCategoriesValueListDistributor(ByVal distributorID As String, ByVal Distributor_Name As String)
    '    Me.clsProj.CreateViewDistDropDown(distributorID, Distributor_Name)
    '    Dim ColshipTo As Janus.Windows.GridEX.GridEXColumn = Me.GridEX2.RootTable.Columns("DISTRIBUTOR_ID")
    '    ColshipTo.EditType = Janus.Windows.GridEX.EditType.Combo
    '    ColshipTo.HasValueList = True
    '    Dim ValueListDistributor As Janus.Windows.GridEX.GridEXValueListItemCollection = ColshipTo.ValueList
    '    'Me.clsProj.ViewDistDropDown().RowFilter = rowfilter
    '    ValueListDistributor.PopulateValueList(Me.clsProj.ViewDistDropDown(), "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME")
    '    ColshipTo.EditTarget = Janus.Windows.GridEX.EditTarget.Text
    '    ColshipTo.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    '    Me.GridEX2.Refetch()
    'End Sub
    'Private Sub FillCategoriesValueListDistributor()
    '    Dim ColshipTo As Janus.Windows.GridEX.GridEXColumn = Me.GridEX2.RootTable.Columns("DISTRIBUTOR_ID")
    '    ColshipTo.EditType = Janus.Windows.GridEX.EditType.Combo
    '    ColshipTo.HasValueList = True
    '    Dim ValueListDistributor As Janus.Windows.GridEX.GridEXValueListItemCollection = ColshipTo.ValueList
    '    'Me.clsProj.ViewDistDropDown().RowFilter = rowfilter
    '    ValueListDistributor.PopulateValueList(Me.clsProj.ViewDistDropDown(), "DISTRIBUTOR_ID", "DISTRIBUTOR_NAME")
    '    ColshipTo.EditTarget = Janus.Windows.GridEX.EditTarget.Text
    '    ColshipTo.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    '    'Me.GridEX2.Refetch()
    'End Sub
    'Private Sub ClearCategoriesValueList()
    '    Me.GridEX2.RootTable.Columns("BRANDPACK_ID").ValueList.Clear()
    '    Me.GridEX2.RootTable.Columns("BRANDPACK_iD").HasValueList = False
    '    Me.GridEX2.RootTable.Columns("DISTRIBUTOR_ID").ValueList.Clear()
    '    Me.GridEX2.RootTable.Columns("BRANDPACK_ID").HasValueList = False
    'End Sub
    Private Sub FormatGridEx1()
        For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If Item.DataMember = "PROJ_BRANDPACK_ID" Then
                Item.Visible = False
                Item.ShowInFieldChooser = False
            ElseIf Item.DataMember = "PRICE" Then
                Item.FormatString = "#,##0.00"
                Item.TotalFormatString = "#,##0.00"
                Item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Item.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.DataMember = "BRANDPACK_ID" Then
                Item.Visible = False
                Item.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.DateTime") Then
                Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                Item.FormatString = "dd MMMM yyyy"
                Item.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            Else
                Item.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            End If
            'ELECT PO_REF_NO,PO_REF_DATE,PROJ_REF_NO,BRANDPACK_NAME,QUANTITY,[PRICE/QTY],TOTAL
            Item.TextAlignment = Janus.Windows.GridEX.TextAlignment.Empty
            Item.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Next
        Me.GridEX1.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.GridEX1.RootTable.Columns("PROJ_REF_NO")))
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.RootTable.Columns("PROJ_REF_NO").Visible = False
        Me.GridEX1.RootTable.Columns("PROJ_REF_NO").ShowInFieldChooser = True
        'Me.AddConditonalFormating1()
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
        Me.GridEX1.AutoSizeColumns()
    End Sub
    'Private Sub FormatGridEx2()

    '    'Me.GridEX2.RootTable.Columns("PROJ_BRANDPACK_ID").Width = 170
    '    Me.GridEX2.RootTable.Columns("PROJ_BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '    Me.GridEX2.RootTable.Columns("PROJ_BRANDPACK_ID").Selectable = False

    '    'Me.GridEX2.RootTable.Columns("PROJ_REF_NO").Width = 130
    '    Me.GridEX2.RootTable.Columns("PROJ_REF_NO").MaxLength = 15
    '    Me.GridEX2.RootTable.Columns("PROJ_REF_NO").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '    Me.GridEX2.RootTable.Columns("PROJ_REF_NO").Selectable = False
    '    Me.GridEX2.RootTable.Columns("PROJ_REF_NO").FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
    '    'Me.GridEX2.RootTable.Columns("BRANDPACK_ID").Width = 230
    '    Me.GridEX2.RootTable.Columns("BRANDPACK_ID").Caption = "BRANDPACK_NAME"
    '    'Me.GridEX2.RootTable.Columns("DISTRIBUTOR_ID").Width = 150
    '    'Me.GridEX2.RootTable.Columns("DISTRIBUTOR_ID").Caption = "SHIP_TO_DISTRIBUTOR"
    '    'Me.GridEX2.RootTable.Columns("MAX_ORDER").Width = 80
    '    'Me.GridEX2.RootTable.Columns("APPROVED_DISC_PCT").Caption = "DISCOUNT"
    '    'Me.GridEX2.RootTable.Columns("APPROVED_DISC_PCT").Width = 80
    '    'Me.GridEX2.RootTable.Columns("APPROVED_DISC_PCT").MaxLength = 5
    '    'Me.GridEX2.RootTable.Columns("APPROVED_DISC_PCT").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
    '    'Me.GridEX2.RootTable.Columns("APPROVED_DISC_PCT").FormatString = "d"

    '    'Me.GridEX2.RootTable.Columns("START_DATE").Width = 110
    '    'Me.GridEX2.RootTable.Columns("START_DATE").EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
    '    'Me.GridEX2.RootTable.Columns("START_DATE").LimitToList = True
    '    'Me.GridEX2.RootTable.Columns("START_DATE").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
    '    'Me.GridEX2.RootTable.Columns("START_DATE").FormatString = "D"

    '    'Me.GridEX2.RootTable.Columns("END_DATE").EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
    '    'Me.GridEX2.RootTable.Columns("END_DATE").LimitToList = True
    '    'Me.GridEX2.RootTable.Columns("END_DATE").Width = 110
    '    'Me.GridEX2.RootTable.Columns("END_DATE").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
    '    'Me.GridEX2.RootTable.Columns("END_DATE").FormatString = "D"
    '    'Me.GridEX2.SearchColumn = Me.GridEX2.RootTable.Columns(0)
    '    Me.GridEX2.RootTable.Columns("CREATE_BY").Visible = False
    '    Me.GridEX2.RootTable.Columns("CREATE_DATE").Visible = False
    '    Me.GridEX2.RootTable.Columns("MODIFY_BY").Visible = False
    '    Me.GridEX2.RootTable.Columns("MODIFY_DATE").Visible = False
    '    'Me.AddConditionalFormatting2()
    '    'for each
    '    Me.LockedColumCreateModify(Me.GridEX2)
    'End Sub
    Private Sub BindGrid(ByVal grd As Janus.Windows.GridEX.GridEX, ByVal objDataSource As Object)
        Me.StateFilling = FillingGrid.Filling
        Select Case grd.Name
            Case "GridEX1"
                grd.DataSource = objDataSource
                grd.DataMember = "T_Project"
                grd.RetrieveStructure()
                Me.FormatGridEx1()
            Case "GridEX2"
                grd.SetDataBinding(objDataSource, "")
                grd.RetrieveStructure()
        End Select
        grd.ExpandRecords()
        Me.StateFilling = FillingGrid.HasFilled
    End Sub
    Private Sub InflateData()

        Me.StateFilling = FillingGrid.Filling
        Me.txtProjName.Text = Me.GridEX1.GetValue("PROJECT_NAME").ToString()
        Me.txtProjRefNo.Text = Me.GridEX1.GetValue("PROJ_REF_NO").ToString()
        Me.txtProjRefNo.ReadOnly = True
        Me.OriginalStart_Date = Convert.ToDateTime(Me.GridEX1.GetValue("START_DATE"))
        Me.dtPicStartDate.Value = Me.OriginalStart_Date
        Me.dtPicStartDate.Text = Me.OriginalStart_Date.ToShortDateString()
        Me.OriginalEndDate = Convert.ToDateTime(Me.GridEX1.GetValue("END_DATE"))
        Me.dtPicEndDate.Value = Me.OriginalEndDate
        Me.dtPicEndDate.Text = Me.OriginalEndDate.ToShortDateString()
        Me.dtPicStartDate.ReadOnly = True
        Me.dtPicEndDate.ReadOnly = True
        Me.MultiColumnCombo1.ReadOnly = True
        'get data distributor where agreement_enddate based on dtpicdate
        Dim dv As DataView = Me.clsProj.CreateViewDistributor(String.Empty, True, DateTime.Parse(Me.GridEX1.GetValue("START_DATE")), DateTime.Parse(Me.GridEX1.GetValue("END_DATE")), False)
        Me.BindMultiColumnCombo(dv, Me.MultiColumnCombo1)
        Me.StateFillingMCB = FillingMCB.Filling
        Me.MultiColumnCombo1.Value = Me.GridEX1.GetValue("DISTRIBUTOR_ID")
        If Not IsNothing(Me.GridEX1.GetValue("PROJECT_ADDRESS")) And Not IsDBNull(Me.GridEX1.GetValue("PROJECT_ADDRESS")) Then
            If Me.GridEX1.GetValue("PROJECT_ADDRESS").ToString() <> "" Then
                Me.txtAddress.Text = Me.GridEX1.GetValue("PROJECT_ADDRESS").ToString()
            Else
                Me.txtAddress.Text = ""
            End If
        Else
            Me.txtAddress.Text = ""
        End If
        Me.DProject = New DomainProject()
        With Me.DProject
            .DistributorID = Me.MultiColumnCombo1.Value.ToString()
            .Address = Me.txtAddress.Text.TrimStart().TrimEnd()
            .StartDate = Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString())
            .EndDate = Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString())
            .ProjRefName = Me.txtProjName.Text.TrimStart().TrimEnd()
            .ProjRefNo = Me.txtProjRefNo.Text.TrimStart().TrimEnd()
        End With
        'get brandpack based on value distributor 

        If Not IsNothing(Me.DSBrandPack) Then
            Me.DSBrandPack.RejectChanges()
        End If
        Me.DSBrandPack = New DataSet("DSBrandpack") : Me.DSBrandPack.Clear()
        Dim dt As DataTable = Me.clsProj.GetBrandPack(Me.txtProjRefNo.Text, Me.MultiColumnCombo1.Value.ToString(), Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()), ChekedBrandPackIDS, True)
        Me.DSBrandPack.Tables.Add(dt)
        Me.grdBrandPack.SetDataBinding(Me.DSBrandPack.Tables(0).DefaultView(), "")
        Me.grdBrandPack.RowCheckStateBehavior = Janus.Windows.GridEX.RowCheckStateBehavior.CheckIndividualRows
        ''looping set checkbox checked by checkedBrandpackIDS
        For i As Integer = 0 To ChekedBrandPackIDS.Count - 1
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdBrandPack.GetDataRows()
                If row.Cells("BRANDPACK_ID").Value = ChekedBrandPackIDS(i) Then
                    'row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked
                    If Not row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                        row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked
                    End If
                Else
                    If Not row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                        row.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked
                    End If
                End If
            Next
        Next
        Me.grdBrandPack.UpdateData()

        'Me.bindCheckedCombo(DV, "BRANDPACK_ID", "BRANDPACK_NAME")
        'Me.mcbBrandPack.ValuesDataSource = ChekedBrandPackIDS
        'Me.ListOriginalBrandPack = New List(Of String)
        'For i As Integer = 0 To ChekedBrandPackIDS.Length - 1
        '    Me.ListOriginalBrandPack.Add(ChekedBrandPackIDS(i).ToString())
        'Next
        'get datatable from po
        'Me.tblRefPO = Me.clsProj.GetRefencedData(ListOriginalBrandPack, Me.txtProjRefNo.Text.TrimEnd().TrimStart(), True)
        'Dim DVBrandPack As DataView = tblRefPO.DefaultView()
        'DVBrandPack.Sort = "BRANDPACK_ID"
        'If ListOriginalBrandPack.Count = 1 Then
        '    If dv.Find(ListOriginalBrandPack(0)) <> -1 Then
        '        Me.mcbBrandPack.ReadOnly = True
        '    End If
        'ElseIf ListOriginalBrandPack.Count > 1 Then
        '    Dim AllRef As Boolean = True
        '    For i As Integer = 0 To ListOriginalBrandPack.Count - 1
        '        Dim BrandPackID As String = ListOriginalBrandPack(i)
        '        If dv.Find(BrandPackID) <= -1 Then
        '            AllRef = False
        '            Exit For
        '        End If
        '    Next
        '    'check seluruh list apakah sudah referenced datanya
        '    If Not AllRef Then : Me.mcbBrandPack.ReadOnly = False : Else : Me.mcbBrandPack.ReadOnly = True : End If
        'Else
        '    Me.mcbBrandPack.ReadOnly = False
        'End If
        ''If ListOriginalBrandPack.Count = 1 Then
        ''    'check referensi nya apakah sudah kepake di PO
        ''End If
        'set listBrandPack dengan checkedbrandPacks
        'jika list brandPack cuma 1
        'check apakah ada
        Me.btnSave.Enabled = Me.HasChangedData()
        Me.StateFillingMCB = FillingMCB.HasFilled
    End Sub
#End Region

#Region " Function "
    Private Function IsValid() As Boolean
        If Me.txtProjRefNo.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.txtProjRefNo, "PROJECT REF is Null !." & vbCrLf & "Project Ref must be Suplied.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtProjRefNo), Me.txtProjRefNo, 2500)
            Me.txtProjRefNo.Focus() : Me.txtProjRefNo.SelectionLength = Me.txtProjRefNo.Text.Length
            Return False
        ElseIf Me.txtProjName.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.txtProjName, "Project name is Null !." & vbCrLf & "Project Name must not be null.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtProjName), Me.txtProjName, 2500)
            Me.txtProjName.Focus() : Me.txtProjName.SelectionLength = Me.txtProjName.Text.Length
            Return False
        ElseIf (Me.MultiColumnCombo1.Text = "") Or (Me.MultiColumnCombo1.SelectedIndex = -1) Then
            Me.baseTooltip.SetToolTip(Me.MultiColumnCombo1, "Distributor Name is Null !." & vbCrLf & "Distributor Name Must be Defined.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.MultiColumnCombo1), Me.MultiColumnCombo1, 2500)
            Me.MultiColumnCombo1.Focus()
            Return False
            'ElseIf IsNothing(Me.mcbBrandPack.CheckedValues) Then
            '    Me.baseTooltip.SetToolTip(Me.MultiColumnCombo1, "BrandPack is Null !." & vbCrLf & "BrandPack Must be Defined.")
            '    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2500)
            '    Me.MultiColumnCombo1.Focus()
            '    Return False
            'ElseIf Me.mcbBrandPack.CheckedValues.Length <= 0 Then
            '    Me.baseTooltip.SetToolTip(Me.MultiColumnCombo1, "BrandPack is Null !." & vbCrLf & "BrandPack Must be Defined.")
            '    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2500)
            '    Me.MultiColumnCombo1.Focus()
            '    Return False
        ElseIf Me.dtPicStartDate.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.dtPicStartDate, "Project Date !." & vbCrLf & "Project date must be defined.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dtPicStartDate), Me.dtPicStartDate, 2500)
            Me.dtPicStartDate.Focus()
            Return False
        ElseIf Me.ChekedBrandPackIDS.Count <= 0 Then
            Me.baseTooltip.Show("NO BRANDPACK is selected", Me.grdBrandPack, 2500)
            Me.grdBrandPack.Focus() : Return False
        End If

        Return True
    End Function
    Private Function HasChangedData() As Boolean
        If Not IsNothing(Me.DProject) Then
            If Me.DProject.Address <> Me.txtAddress.Text.TrimStart().TrimEnd() Then
                Return True
            End If
        End If
        If Not IsNothing(Me.MultiColumnCombo1.Value) And Not IsDBNull(Me.MultiColumnCombo1.Value) Then
            If Me.DProject.DistributorID <> Me.MultiColumnCombo1.Value.ToString() Then
                Return True
            End If
        End If
        If Me.DProject.StartDate <> Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()) Then
            Return True
        End If
        If Me.DProject.ProjRefName <> Me.txtProjName.Text.TrimStart().TrimEnd() Then
            Return True
        End If
        If Me.DProject.ProjRefNo <> Me.txtProjRefNo.Text.TrimStart().TrimEnd() Then
            Return True
        End If
        If Me.DSBrandPack.HasChanges() Then
            Return True
        End If
        Return False
    End Function
#End Region

#Region " Event "

#Region " Bar "
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnRenameColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar1)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    'Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                        Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    End If
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                    'Select Case Me.SelectGrid
                    '    Case GridSelect.GridEx1

                    '    Case GridSelect.GridEx2
                    '        'Me.GridEXPrintDocument1.GridEX = Me.GridEX2
                    '        'Me.GridEX2.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                    '        Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    '        'Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    '        'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    '        If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                    '            Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    '        End If
                    '        If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    '            Me.PrintPreviewDialog1.Document.Print()
                    '        End If
                    '        'Me.GridEX2.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                    'End Select
                Case "btnShowFieldChooser"
                    Me.GridEX1.ShowFieldChooser(Me, "Drag column here to hide from grid")
                Case "btnCustomFilter"
                    Me.FilterEditor1.SourceControl = Me.GridEX1
                    Me.GridEX1.RemoveFilters()
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.FilterEditor1.Visible = True
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.GridEX1.RemoveFilters()
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
                Case "btnAddNew" : Me.AddNew()
                    'Me.GridEX1.Enabled = False
                    'Me.txtProjRefNo.Enabled = True
                    'Me.txtProjName.Enabled = True
                    'Me.ModeSave = Mode.Save
                    'Me.clsProj.GetDasetBranpackDetail("")
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
                Case "btnEdit" : Me.EditData()
                Case "btnPageSettings"
                    Me.PageSetupDialog1.ShowDialog(Me)
                Case "btnRefresh"
                    Me.btnAplyFilter_Click(Me.btnAplyFilter, New EventArgs())
                    'Me.UnabledControl()
                    'Me.StateFilling = FillingGrid.HasFilled
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
#End Region

#Region " Form Event "
    Private Sub AddNew()
        Me.grpEntryData.Height = Me.grpEditHeight
        Me.StateFillingMCB = FillingMCB.Filling
        Me.ClearControl(Me.grpProjectHeader)
        Me.txtAddress.Text = ""
        If Not IsNothing(Me.DSBrandPack) Then : Me.DSBrandPack.RejectChanges() : End If
        Me.grdBrandPack.SetDataBinding(Nothing, "")
        Me.ChekedBrandPackIDS.Clear()
        Me.txtProjRefNo.ReadOnly = False : Me.txtProjRefNo.Enabled = True
        Me.dtPicStartDate.ReadOnly = False
        Me.MultiColumnCombo1.ReadOnly = False
        Me.ModeSave = Mode.Save
        'BIND Distributor
        Dim DV As DataView = Me.clsProj.CreateViewDistributor(String.Empty, True, Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), True)
        Me.BindMultiColumnCombo(DV, Me.MultiColumnCombo1)
        Me.txtProjRefNo.Focus() : Me.txtProjRefNo.SelectionLength = Me.txtProjRefNo.Text.Length
        Me.DProject = New DomainProject()
        Me.StateFilling = FillingGrid.HasFilled
    End Sub
    Private Sub EditData()
        If Me.GridEX1.RecordCount > 0 Then
            If (Not Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please define row to edit") : Return
            End If
            Me.ModeSave = Mode.Update : Me.grpEntryData.Height = Me.grpEditHeight : Me.InflateData()
        End If
    End Sub
    Private Sub Project_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsProj) Then
                Me.clsProj.Dispose(True)
                Me.clsProj = Nothing
            End If
            Me.Dispose(True)
            'If Not IsNothing(Me.clsProj.GetDataSet()) Then
            '    If Me.clsProj.GetDataSet().HasChanges() Then
            '        If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
            '            Me.Cursor = Cursors.WaitCursor
            '            Me.clsProj.DSPBDetailHasChanges = True
            '            If Me.IsValid = False Then
            '                Return
            '            End If
            '            Me.Cursor = Cursors.WaitCursor
            '            Me.clsProj.Distributor_ID = Me.MultiColumnCombo1.Value
            '            Me.clsProj.Proj_Ref_Name = Me.txtProjName.Text
            '            Me.clsProj.Proj_Ref_No = Me.txtProjRefNo.Text
            '            Me.clsProj.Project_Ref_Date = CDate(Me.dtPicDate.Value.ToShortDateString())
            '            If Me.txtAddress.Text <> "" Then
            '                Me.clsProj.Address = Me.txtAddress.Text
            '            End If
            '            'If Me.txtContactPerson.Text <> "" Then
            '            '    Me.clsProj.Conntact = Me.txtContactPerson.Text
            '            'End If
            '            'If Me.txtHp.Text <> "" Then
            '            '    Me.clsProj.HP = Me.txtHp.Text
            '            'End If
            '            'If Me.txtFax.Text <> "" Then
            '            '    Me.clsProj.Fax = Me.txtFax.Text
            '            'End If
            '            'If Me.txtPhone.Text <> "" Then
            '            '    Me.clsProj.Phone = Me.txtPhone.Text
            '            'End If
            '            'Select Case Me.ModeSave
            '            '    Case Mode.Save
            '            '        Me.clsProj.SaveProject(Me.clsProj.GetDataSet().GetChanges())
            '            '    Case Mode.Update
            '            '        Me.clsProj.UpdateProject(Me.clsProj.GetDataSet().GetChanges())
            '            'End Select
            '            'Me.ShowMessageInfo(Me.MessageSavingSucces)
            '        End If
            '    End If
            'End If
        Catch ex As Exception

        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Project_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.grpEditHeight = Me.grpEntryData.Height
            Me.FilterEditor1.Visible = False
            Me.ModeSave = Mode.Save
            Me.dtPicStartDate.Value = NufarmBussinesRules.SharedClass.ServerDate()
            Me.dtPicEndDate.Value = NufarmBussinesRules.SharedClass.ServerDate
            Me.grpEntryData.Height = 0
            'fill distributor
            Me.StateFillingMCB = FillingMCB.Filling
            'bindmulticolumn combo dengan distributor yang udah commite dengan agreement dan agreement tsb masih berlaku
            'besok sudah ,kita so capek kwa
            Me.clsProj = New NufarmBussinesRules.DistributorProject.ProjectRegistering()
            Dim DV As DataView = Me.clsProj.CreateViewDistributor(String.Empty, True, NufarmBussinesRules.SharedClass.ServerDate, NufarmBussinesRules.SharedClass.ServerDate, True)
            Me.BindMultiColumnCombo(DV, Me.mcbDistributor)
            Me.btnSave.Enabled = False
            Me.StateFillingMCB = FillingMCB.HasFilled
            Me.DProject = New DomainProject()
            With Me.DProject
                .Address = String.Empty
                .DistributorID = String.Empty
                .StartDate = NufarmBussinesRules.SharedClass.ServerDate
                .EndDate = NufarmBussinesRules.SharedClass.ServerDate
                .ProjRefName = String.Empty
                .ProjRefNo = String.Empty
            End With
            Me.dtPicfrom.Value = NufarmBussinesRules.SharedClass.ServerDate.AddMonths(-2)
            Me.dtPicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate.AddMonths(5)
            Me.ApplyFilter()
            Me.btnEdit.Enabled = False
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name & "_Project_Load") : Me.ShowMessageInfo(ex.Message)
        Finally
            If Me.StateFilling = FillingGrid.Filling Then : Me.StateFilling = FillingGrid.HasFilled : End If
            If Me.CVM = ChangeValueMultiClumnCombo.FromSelectedGrid Then : Me.CVM = ChangeValueMultiClumnCombo.FromSelectedMulticolumnCombo : End If
            If Me.ModeSave = Mode.Update Then : Me.ModeSave = Mode.Save : End If : Me.ReadAcces() : Me.HasLoad = True : Me.Cursor = Cursors.Default
        End Try

    End Sub

#End Region

#Region " User Control "
    Private Overloads Sub ClearControl()
        Me.StateFillingMCB = FillingMCB.Filling
        Me.StateFilling = FillingGrid.Filling
        Me.ClearControl(Me.grpProjectHeader)
        Me.txtAddress.Text = ""
        If Not IsNothing(Me.DSBrandPack) Then : Me.DSBrandPack.RejectChanges() : End If
        Me.grdBrandPack.SetDataBinding(Nothing, "")
        Me.ChekedBrandPackIDS.Clear()
        Me.txtProjRefNo.ReadOnly = False : Me.txtProjRefNo.Enabled = True : Me.dtPicStartDate.ReadOnly = False : Me.MultiColumnCombo1.ReadOnly = False
        Me.ModeSave = Mode.Save
        Me.DProject = New DomainProject()
        Me.grpEntryData.Height = 0
        Me.StateFillingMCB = FillingMCB.HasFilled
        Me.StateFilling = FillingGrid.HasFilled
    End Sub
    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ClearControl()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.StateFilling = FillingGrid.HasFilled : Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Me.WhileSaving = Saving.Waiting
            If Me.IsValid = False Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            If Me.ModeSave = Mode.Save Then
                If Me.clsProj.IsExisted(Me.txtProjRefNo.Text, False) = True Then
                    Me.baseTooltip.SetToolTip(Me.txtProjRefNo, Me.txtProjRefNo.Text & " Has existed in database !." & "Please enter with another one.")
                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtProjRefNo), Me.txtProjRefNo, 2000)
                    Me.txtProjRefNo.Focus() : Me.txtProjRefNo.SelectionStart = 0 : Me.txtProjRefNo.SelectionLength = Me.txtProjRefNo.Text.Length
                    Return
                End If
            End If
            Me.clsProj.Distributor_ID = Me.MultiColumnCombo1.Value
            Me.clsProj.Proj_Ref_Name = Me.txtProjName.Text.TrimEnd().TrimStart()
            Me.clsProj.Proj_Ref_No = Me.txtProjRefNo.Text.TrimEnd().TrimStart()
            Me.clsProj.StartDate = Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString())
            Me.clsProj.EndDate = Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString())
            If Me.txtAddress.Text <> "" Then
                Me.clsProj.Address = Me.txtAddress.Text.TrimEnd().TrimStart()
            End If
            'CHECK EXISTING PROJECT
            If Me.ModeSave = Mode.Save Then
                If Me.clsProj.IsExisted(Me.txtProjRefNo.Text.TrimEnd().TrimStart(), False) Then
                    Me.ShowMessageInfo(Me.MessageDataHasExisted) : Return
                End If
            End If
            Me.clsProj.SaveProject(Me.DSBrandPack, False)
            'Me.clsProj.SaveProject(listBrandPack, False)
            'bind data to datagrid
            Me.btnAplyFilter_Click(Me.btnAplyFilter, New EventArgs())
            'Me.StateFillingMCB = FillingMCB.Filling
            'Me.ClearControl(Me.grpProjectHeader)
            'If Not IsNothing(Me.DSBrandPack) Then : Me.DSBrandPack.RejectChanges() : Me.DSBrandPack.Clear() : End If
            'If Not IsNothing(Me.grdBrandPack.DataSource) Then
            '    Me.grdBrandPack.SetDataBinding(Nothing, "")
            'End If
            'Me.ChekedBrandPackIDS.Clear() : Me.DProject = New DomainProject()
            'Me.ProjRefNo = "" : Me.txtAddress.Text = ""
            'Me.txtProjRefNo.ReadOnly = False : Me.txtProjRefNo.Enabled = True
            'Me.MultiColumnCombo1.ReadOnly = False
            'Me.dtPicDate.ReadOnly = False
            'Me.ModeSave = Mode.Save
        Catch ex As Exception
            'Me.WhileSaving = Saving.Failed
            Me.StatProg = StatusProgress.None
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnSave_Click")
        Finally
            Me.StateFilling = FillingGrid.HasFilled : Me.StateFillingMCB = FillingMCB.HasFilled : Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#Region " Grid Ex "

#Region " GridEX 1 "

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try

            If Me.StateFilling = FillingGrid.Filling Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            If Me.GridEX1.SelectedItems.Count <= 0 Then
                Return
            End If
            Me.btnEdit.Enabled = True
            If Me.grpEntryData.Height <= 0 Then : Return : End If
            If Not (Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.StateFillingMCB = FillingMCB.Filling
                Me.ClearControl(Me.grpEntryData)
                Me.btnEdit.Enabled = False
                'Me.BindGrid(Me.GridEX2, Nothing)
                Return
            End If
            If Not Me.ProjRefNo.Equals(Me.GridEX1.GetValue("PROJ_REF_NO").ToString()) Then
                Me.Cursor = Cursors.WaitCursor
                Me.btnEdit.Enabled = True
                Me.ModeSave = Mode.Update : Me.InflateData()
                Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                'If Not IsNothing(Me.GridEX1.GetValue("PROJ_BRANDPACK_ID")) And Not IsDBNull(Me.GridEX1.GetValue("PROJ_BRANDPACK_ID")) Then
                '    If Me.GridEX1.GetValue("PROJ_BRANDPACK_ID").ToString() <> "" Then
                '        Me.clsProj.GetDasetBranpackDetail(Me.GridEX1.GetValue("PROJ_BRANDPACK_ID"))
                '    Else
                '        Me.clsProj.GetDasetBranpackDetail("")
                '    End If
                'Else
                '    Me.clsProj.GetDasetBranpackDetail("")
                'End If
            End If

            Me.CVM = ChangeValueMultiClumnCombo.FromSelectedMulticolumnCombo : Me.StateFillingMCB = FillingMCB.HasFilled : Me.StateFilling = FillingGrid.HasFilled
        Catch ex As Exception
            Me.CVM = ChangeValueMultiClumnCombo.FromSelectedMulticolumnCombo : Me.StateFillingMCB = FillingMCB.HasFilled : Me.StateFilling = FillingGrid.HasFilled : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_CurrentCellChanged")
        Finally
            Me.ReadAcces() : Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Me.GridEX1.Row <= -1 Then : e.Cancel = True : Return : End If
            If Me.GridEX1.GetRow().Children > 0 Then : e.Cancel = True : Return : End If
            If Me.clsProj.HasreferencedData(Me.GridEX1.GetValue("PROJ_REF_NO")) Then : Me.ShowMessageInfo(Me.MessageCantDeleteData)
                e.Cancel = True : Return : End If
            'delete data
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then : e.Cancel = True : Me.GridEX1.Refetch() : Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.StateFilling = FillingGrid.Filling
            Dim BrandPackID As Object = Me.GridEX1.GetValue("BRANDPACK_ID")
            Dim ProjBrandPackID As String = Me.GridEX1.GetValue("PROJ_BRANDPACK_ID").ToString()
            Me.clsProj.DeleteProject(Me.GridEX1.GetValue("PROJ_REF_NO").ToString(), ProjBrandPackID, Me.GridEX1.RecordCount > 1)
            e.Cancel = False : Me.GridEX1.UpdateData()

            'check data di grid brandpack

            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdBrandPack.GetDataRows()
                If row.Cells("BRANDPACK_ID").Value = BrandPackID Then
                    'remove check list
                    row.BeginEdit()
                    row.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked
                    'fill defaut value
                    row.Cells("PROJ_BRANDPACK_ID").Value = DBNull.Value
                    row.Cells("PROJ_REF_NO").Value = DBNull.Value
                    row.Cells("PRICE").Value = DBNull.Value
                    row.Cells("CREATE_DATE").Value = NufarmBussinesRules.SharedClass.ServerDate
                    row.Cells("CREATE_BY").Value = NufarmBussinesRules.User.UserLogin.UserName
                    row.Cells("MODIFY_BY").Value = DBNull.Value
                    row.Cells("MODIFY_DATE").Value = DBNull.Value
                    row.Cells("HAS_CHANGED").Value = False
                    'remove chekedbrandpackId
                    row.EndEdit() : Me.grdBrandPack.UpdateData() : Exit For
                End If
            Next
            If Me.ChekedBrandPackIDS.Contains(BrandPackID) Then
                Me.ChekedBrandPackIDS.Remove(BrandPackID)
            End If
            e.Cancel = False : Me.grdBrandPack.UpdateData()
            If (Me.GridEX1.RecordCount > 0) Then
                If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                    Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
                End If
            Else
                Me.StateFillingMCB = FillingMCB.Filling
                Me.ClearControl(Me.grpEntryData)
                Me.grdBrandPack.SetDataBinding(Nothing, "")
                Me.ModeSave = Mode.Save
                e.Cancel = False : Me.StateFilling = FillingGrid.HasFilled : Return
            End If
            Me.StateFillingMCB = FillingMCB.HasFilled
            Me.StateFilling = FillingGrid.HasFilled
        Catch ex As Exception
            Me.StateFillingMCB = FillingMCB.HasFilled
            Me.StateFilling = FillingGrid.HasFilled
            e.Cancel = True
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub GridEX1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.SelectionChanged

    'End Sub

    'Private Sub GridEX1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.Enter
    '    Me.SelectGrid = GridSelect.GridEx1
    'End Sub

    'Private Sub GridEX1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.Click
    '    Me.SelectGrid = GridSelect.GridEx1
    'End Sub

#End Region

#Region " GridEX 2 "
    'Private Sub GridEX2_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    Try
    '        'CHECK PROJ_BRANDPACK
    '        If Not IsNothing(Me.GridEX2.GetValue("PROJ_BRANDPACK_ID")) Then
    '            If IsDBNull(Me.GridEX2.GetValue("PROJ_BRANDPACK_ID")) Then
    '                Me.ShowMessageInfo("PROJ_BRANDPACK_ID Must not be null")
    '                Me.GridEX2.CancelCurrentEdit()
    '                Me.GridEX2.Refetch()
    '                Me.GridEX2.MoveToNewRecord()
    '                Return
    '            End If
    '        Else
    '            Me.ShowMessageInfo("PROJ_BRANDPACK_ID Must not be null")
    '            Me.GridEX2.CancelCurrentEdit()
    '            Me.GridEX2.Refetch()
    '            Me.GridEX2.MoveToNewRecord()
    '            Return
    '        End If
    '        'CHECK PROJ_REF_NO 
    '        If Not IsNothing(Me.GridEX2.GetValue("PROJ_REF_NO")) Then
    '            If (IsDBNull(Me.GridEX2.GetValue("PROJ_REF_NO"))) Or (Me.GridEX2.GetValue("PROJ_REF_NO").Equals("")) Then
    '                Me.ShowMessageInfo("PROJ_REF_NO Must not be null")
    '                Me.GridEX2.CancelCurrentEdit()
    '                Me.GridEX2.Refetch()
    '                Me.GridEX2.MoveToNewRecord()
    '                Return
    '            End If
    '        Else
    '            Me.ShowMessageInfo("PROJ_BRANDPACK_ID Must not be null")
    '            Me.GridEX2.CancelCurrentEdit()
    '            Me.GridEX2.Refetch()
    '            Me.GridEX2.MoveToNewRecord()
    '            Return
    '        End If
    '        'CHECK BRANDPACK_ID
    '        If Not IsNothing(Me.GridEX2.GetValue("BRANDPACK_ID")) Then
    '            If IsDBNull(Me.GridEX2.GetValue("BRANDPACK_ID")) Then
    '                Me.ShowMessageInfo("BRANDPACK_ID Must not be null")
    '                Me.GridEX2.CancelCurrentEdit()
    '                Me.GridEX2.Refetch()
    '                Me.GridEX2.MoveToNewRecord()
    '                Return
    '            End If
    '        Else
    '            Me.ShowMessageInfo("BRANDPACK_ID Must not be null")
    '            Me.GridEX2.CancelCurrentEdit()
    '            Me.GridEX2.Refetch()
    '            Me.GridEX2.MoveToNewRecord()
    '            Return
    '        End If
    '        'CHECK DISTRIBUTOR_ID
    '        If Not IsNothing(Me.GridEX2.GetValue("DISTRIBUTOR_ID")) Then
    '            If IsDBNull(Me.GridEX2.GetValue("DISTRIBUTOR_ID")) Then
    '                Me.ShowMessageInfo("DISTRIBUTOR_ID Must not be null")
    '                Me.GridEX2.CancelCurrentEdit()
    '                Me.GridEX2.Refetch()
    '                Me.GridEX2.MoveToNewRecord()
    '                Return
    '            End If
    '        Else
    '            Me.ShowMessageInfo("DISTRIBUTOR_ID Must not be null")
    '            Me.GridEX2.CancelCurrentEdit()
    '            Me.GridEX2.Refetch()
    '            Me.GridEX2.MoveToNewRecord()
    '            Return
    '        End If
    '        'CHECK_START_DATE
    '        If Not IsNothing(Me.GridEX2.GetValue("START_DATE")) Then
    '            If IsDBNull(Me.GridEX2.GetValue("START_DATE")) Then
    '                Me.ShowMessageInfo("START_DATE Must not be null")
    '                Me.GridEX2.CancelCurrentEdit()
    '                Me.GridEX2.Refetch()
    '                Me.GridEX2.MoveToNewRecord()
    '                Return
    '            ElseIf CDate(Me.GridEX2.GetValue("START_DATE")) < NufarmBussinesRules.SharedClass.ServerDate() Then
    '                Me.GridEX2.SetValue("START_DATE", CDate(NufarmBussinesRules.SharedClass.ServerDate()))
    '            End If
    '        Else
    '            Me.ShowMessageInfo("START_DATE Must not be null")
    '            Me.GridEX2.CancelCurrentEdit()
    '            Me.GridEX2.Refetch()
    '            Me.GridEX2.MoveToNewRecord()
    '            Return
    '        End If
    '        'CHECK END_DATE
    '        If Not IsNothing(Me.GridEX2.GetValue("END_DATE")) Then
    '            If IsDBNull(Me.GridEX2.GetValue("END_DATE")) Then
    '                Me.ShowMessageInfo("END_DATE Must not be null")
    '                Me.GridEX2.CancelCurrentEdit()
    '                Me.GridEX2.Refetch()
    '                Me.GridEX2.MoveToNewRecord()
    '                Return
    '            ElseIf CDate(Me.GridEX2.GetValue("END_DATE")) <= NufarmBussinesRules.SharedClass.ServerDate() Then
    '                Me.ShowMessageInfo("END_DATE IS NOT VALID .!")
    '                Me.GridEX2.CancelCurrentEdit()
    '                Me.GridEX2.Refetch()
    '                Me.GridEX2.MoveToNewRecord()
    '                Return
    '            End If
    '        Else
    '            Me.ShowMessageInfo("END_DATE Must not be null")
    '            Me.GridEX2.CancelCurrentEdit()
    '            Me.GridEX2.Refetch()
    '            Return
    '        End If

    '        'CHECK START_DATE END_DATE
    '        If CDate(Me.GridEX2.GetValue("START_DATE")) > CDate(Me.GridEX2.GetValue("END_DATE")) Then
    '            Me.ShowMessageInfo("END_DATE Has an invalid value.!")
    '            Me.GridEX2.CancelCurrentEdit()
    '            Me.GridEX2.Refetch()
    '            Me.GridEX2.MoveToNewRecord()
    '        End If
    '        'CHECK DISCOUNT
    '        If Not IsNothing(Me.GridEX2.GetValue("APPROVED_DISC_PCT")) Then
    '            If IsDBNull(Me.GridEX2.GetValue("APPROVED_DISC_PCT")) Then
    '                Me.ShowMessageInfo("DISCOUNT Must not be null")
    '                Me.GridEX2.CancelCurrentEdit()
    '                Me.GridEX2.Refetch()
    '                Me.GridEX2.MoveToNewRecord()
    '                Return
    '            End If
    '        Else
    '            Me.ShowMessageInfo("DISCOUNT Must not be null")
    '            Me.GridEX2.CancelCurrentEdit()
    '            Me.GridEX2.Refetch()
    '            Me.GridEX2.MoveToNewRecord()
    '            Return
    '        End If
    '        If CDec(Me.GridEX2.GetValue("APPROVED_DISC_PCT")) > 100 Then
    '            Me.ShowMessageInfo("DISCOUNT Is not valid value")
    '            Me.GridEX2.CancelCurrentEdit()
    '            Me.GridEX2.Refetch()
    '            Me.GridEX2.MoveToNewRecord()
    '            Return
    '        End If
    '        'CHECK  BRANDPACK_ID
    '        'Me.GridEX2.SearchColumn = Me.GridEX1.RootTable.Columns(0)
    '        If Me.clsProj.GetViewBrandpackDetail().Find(Me.GridEX2.GetValue("PROJ_BRANDPACK_ID")) <> -1 Then
    '            Me.ShowMessageInfo("PROJ_BRANDPACK_ID Has Existed !." & vbCrLf & "PROJ_BRANDPACK_ID Must be unique!.")
    '            Me.GridEX2.CancelCurrentEdit()
    '            Me.GridEX2.Refetch()
    '            Me.GridEX2.MoveToNewRecord()
    '            Return
    '        End If
    '        Me.GridEX2.RootTable.Columns("START_DATE").DefaultValue = Me.GridEX2.GetValue("START_DATE")
    '        Me.GridEX2.RootTable.Columns("END_DATE").DefaultValue = Me.GridEX2.GetValue("END_DATE")
    '        Me.GridEX2.SetValue("CREATE_BY", NufarmBussinesRules.User.UserLogin.UserName)
    '        Me.GridEX2.SetValue("CREATE_DATE", NufarmBussinesRules.SharedClass.ServerDate())
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub GridEX2_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
    '    Try
    '        For Each item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX2.RootTable.Columns
    '            If (e.Column.Key = "PROJ_REF_NO") Or (e.Column.Key = "PROJ_BRANDPACK_ID") Or ( _
    '            e.Column.Key = "CREATE_BY") Or (e.Column.Key = "CREATE_DATE") Or ( _
    '            e.Column.Key = "MODIFY_DATE") Or (e.Column.Key = "MODIFY_BY") Then
    '                Return
    '            End If
    '        Next
    '        'PROJ_REF_NO,PROJ_BRANDPACK_ID,BRANDPACK_ID,SHIP_TO_DISTRIBUTOR,MAX_ORDER,START_DATE,
    '        '            END_DATE, DISCOUNT
    '        If Me.txtProjRefNo.Text = "" Then
    '            Return
    '        End If
    '        If Me.GridEX2.Row <= -1 Then
    '            Me.GridEX2.SetValue("PROJ_REF_NO", Me.txtProjRefNo.Text)
    '            If IsNothing(Me.GridEX2.GetValue("BRANDPACK_ID")) Then
    '                Return
    '            ElseIf Me.GridEX2.GetValue("BRANDPACK_ID") Is DBNull.Value Then
    '                Return
    '            Else
    '                Dim PROJ_BRANDPACK_ID As String = Me.GridEX2.GetValue("PROJ_REF_NO").ToString() + "" + Me.GridEX2.GetValue("BRANDPACK_ID").ToString()
    '                Me.GridEX2.SetValue("PROJ_BRANDPACK_ID", PROJ_BRANDPACK_ID)
    '            End If
    '            'If e.Column.Key = "END_DATE" Then
    '            '    If Not (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Then
    '            '        e.Column.DefaultValue = Me.GridEX1.GetValue(e.Column)
    '            '    End If
    '            'End If
    '            'Else
    '            '    Me.GridEX1.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
    '            'Me.gr()
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub GridEX2_DropDown(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
    '    Try
    '        If e.Column.DataMember = "START_DATE" Then
    '            e.Column.DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()
    '            e.Column.LimitToList = True
    '        ElseIf e.Column.Key = "END_DATE" Then
    '            e.Column.DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()
    '        End If
    '        Me.GridEX2.RootTable.Columns(6).CalendarTodayButtonVisible = Janus.Windows.GridEX.TriState.False
    '        Me.GridEX2.RootTable.Columns(6).CalendarNoneButtonVisible = Janus.Windows.GridEX.TriState.False

    '        Me.GridEX2.RootTable.Columns(7).CalendarTodayButtonVisible = Janus.Windows.GridEX.TriState.False
    '        Me.GridEX2.RootTable.Columns(7).CalendarNoneButtonVisible = Janus.Windows.GridEX.TriState.False
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub GridEX2_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs)
    '    Try
    '        If Me.GridEX2.Row <= -1 Then
    '            Return
    '        End If
    '        If Me.clsProj.HasRefernceDataBPDetail(Me.GridEX2.GetValue("PROJ_BRANDPACK_ID").ToString()) Then
    '            Me.ShowMessageInfo(Me.MessageCantDeleteData)
    '            e.Cancel = True
    '            Me.GridEX2.Refetch()
    '            Return
    '        End If
    '        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
    '            e.Cancel = True
    '            Me.GridEX2.Refetch()
    '            Return
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub GridEX2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.GridEX2.Row <= -1 Then
    '            Me.GridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
    '            Me.LockedColumCreateModify(Me.GridEX2)
    '            Me.LockedColumnBrandPackRef()
    '        Else
    '            Me.GridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub GridEX2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    ''chek validity
    '    If Not Me.IsValid() Then : Return : End If
    '    Me.SelectGrid = GridSelect.GridEx2
    'End Sub

    'Private Sub GridEX2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.SelectGrid = GridSelect.GridEx2
    'End Sub
    'Private Sub GridEX2_Error(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ErrorEventArgs)
    '    Try
    '        Me.ShowMessageInfo("Data Can not be Updated due to " & e.Exception.Message)
    '        Me.GridEX2.CancelCurrentEdit()
    '        Me.GridEX2.Refetch()
    '    Catch ex As Exception

    '    End Try
    'End Sub
#End Region

#End Region

#Region " Multicolumn Combo "
    Private Sub MultiColumnCombo1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MultiColumnCombo1.ValueChanged
        Try
            If Me.StateFillingMCB = FillingMCB.Filling Then : Return : End If
            If Not Me.HasLoad Then : Return : End If
            'If Me.ModeSave = Mode.Save Then
            If Me.CVM = ChangeValueMultiClumnCombo.FromSelectedGrid Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            If Me.ModeSave = Mode.Save Then
                If Me.MultiColumnCombo1.Value Is Nothing Then
                    If Not IsNothing(Me.grdBrandPack.DataSource) Then
                        If Not IsNothing(Me.DSBrandPack) Then
                            If Me.DSBrandPack.HasChanges() Then
                                If Me.ShowConfirmedMessage("Are you sure you want to change distributor" & vbCrLf & "Any changes will be discarded") = Windows.Forms.DialogResult.No Then
                                    Return
                                End If
                            End If
                            Me.DSBrandPack.RejectChanges()
                        End If
                    End If
                    Me.StateFilling = FillingGrid.Filling : Me.grdBrandPack.SetDataBinding(Nothing, "") : Me.StateFilling = FillingGrid.HasFilled : Return
                Else
                    'fll brandpack 
                    If Not IsNothing(Me.grdBrandPack.DataSource) Then
                        If Not IsNothing(Me.DSBrandPack) Then
                            If Me.DSBrandPack.HasChanges() Then
                                If Me.ShowConfirmedMessage("Are you sure you want to change distributor" & vbCrLf & "Any changes will be discarded") = Windows.Forms.DialogResult.No Then
                                    Return
                                End If
                            End If
                            Me.DSBrandPack.RejectChanges()
                        End If
                    End If
                    Me.DSBrandPack = New DataSet("DSBrandpack") : Me.DSBrandPack.Clear()
                    Dim dt As DataTable = Me.clsProj.CreateViewBrandPack(Me.MultiColumnCombo1.Value.ToString(), Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), True)
                    Me.DSBrandPack.Tables.Add(dt)
                    Me.grdBrandPack.SetDataBinding(Me.DSBrandPack.Tables(0).DefaultView(), "")
                    'Me.bindCheckedCombo(dv, "BRANDPACK_NAME", "BRANDPACK_ID")

                End If
            End If
            Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.btnSave.Enabled = Me.HasChangedData()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.StateFillingMCB = FillingMCB.HasFilled : Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#Region " TextBox "
    Private Sub txtProjRefNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProjRefNo.TextChanged
        Try
            'If Me.ModeSave = Mode.Update Then
            '    Me.clsProj.GetDasetBranpackDetail(Me.txtProjRefNo.Text)
            '    Me.GridEX2.SetDataBinding(Me.clsProj.GetViewBrandpackDetail(), "")
            '    Me.GridEX2.RetrieveStructure()
            '    Me.GridEX2.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
            '    'If Me.clsProj.HasRefernceDataBPDetail(Me.txtProjRefNo.Text) = True Then
            '    '    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            '    'End If
            '    Return
            'End If
            If Not Me.HasLoad Then : Return : End If
            If Me.StateFilling = FillingGrid.Filling Then : Return : End If
            If Me.txtProjRefNo.Text = "" Then
                Me.mcbDistributor.ReadOnly = True
            Else
                Me.mcbDistributor.ReadOnly = False
            End If
            'Me.GridEX2.FilterMode = Janus.Windows.GridEX.FilterMode.None
            'Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.btnSave.Enabled = Me.HasChangedData()
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region " DateTime Picker "
    Private Sub dtPicDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicStartDate.ValueChanged
        Try
            If Me.dtPicStartDate.Text = "" Then : Return : End If
            If Not Me.HasLoad Then : Return : End If
            If Me.StateFilling = FillingGrid.Filling Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            If Me.txtProjRefNo.Text = "" Then
                Me.ShowMessageInfo("Please define the Project_Ref First.")
                Me.dtPicStartDate.ResetText()
            ElseIf Me.txtProjName.Text = "" Then
                Me.ShowMessageInfo("Please define the project name first.")
                Me.dtPicStartDate.ResetText()
            End If
            If Me.ModeSave = Mode.Save Then
                'bund data to mcbbrandpack based on multicolumn combo
                If Not IsNothing(Me.MultiColumnCombo1.Value) Then
                    If Not IsNothing(Me.DSBrandPack) Then
                        Me.DSBrandPack.RejectChanges()
                    End If
                    Me.DSBrandPack = New DataSet("DSBrandpack") : Me.DSBrandPack.Clear()
                    Dim dt As DataTable = Me.clsProj.CreateViewBrandPack(Me.MultiColumnCombo1.Value.ToString(), Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), True)
                    Me.DSBrandPack.Tables.Add(dt)
                    Me.grdBrandPack.SetDataBinding(Me.DSBrandPack.Tables(0).DefaultView(), "")
                End If
            ElseIf Me.dtPicStartDate.Value < Me.OriginalStart_Date Then
                Me.dtPicStartDate.Value = Me.OriginalStart_Date
            Else
                If Me.DSBrandPack.HasChanges() Then
                    If Me.grdBrandPack.GetCheckedRows().Length > 0 Then
                        If Me.ShowConfirmedMessage("Data Has changed !" & vbCrLf & "Are you sure you want to change date?" & vbCrLf & "All changes will be rejected.") = Windows.Forms.DialogResult.No Then
                            Return
                        End If
                    End If
                ElseIf Me.grdBrandPack.GetCheckedRows().Length > 0 Then
                    If Me.ShowConfirmedMessage("Data Has changed !" & vbCrLf & "Are you sure you want to change date?" & vbCrLf & "All changes will be rejected.") = Windows.Forms.DialogResult.No Then
                        Return
                    End If
                End If
                Me.StateFilling = FillingGrid.Filling
                ''looping brandpack yang sudah dicheck
                If Not IsNothing(Me.grdBrandPack.DataSource) Then
                    Dim rows1() As Janus.Windows.GridEX.GridEXRow = Me.grdBrandPack.GetCheckedRows()
                    Dim tblTempRow As New DataTable("TempRow") : tblTempRow.Clear()
                    With tblTempRow
                        .Columns.Add("PROJ_BRANDPACK_ID", Type.GetType("System.String")).DefaultValue = DBNull.Value
                        .Columns.Add("PROJ_REF_NO", Type.GetType("System.String")).DefaultValue = DBNull.Value ' = Row.Cells("PROJ_REF_NO").Value
                        .Columns.Add("BRANDPACK_ID", Type.GetType("System.String")).DefaultValue = DBNull.Value ' = Row.Cells("PROJ_REF_NO").Value
                        .Columns.Add("PRICE", Type.GetType("System.Decimal")).DefaultValue = DBNull.Value '  = Row.Cells("PRICE").Value
                        .Columns.Add("CREATE_DATE", Type.GetType("System.DateTime")).DefaultValue = NufarmBussinesRules.SharedClass.ServerDate  '  = Row.Cells("CREATE_DATE").Value
                        .Columns.Add("CREATE_BY", Type.GetType("System.String")).DefaultValue = NufarmBussinesRules.User.UserLogin.UserName  '  = Row.Cells("CREATE_BY").Value
                        .Columns.Add("MODIFY_BY", Type.GetType("System.String")).DefaultValue = DBNull.Value  '  = Row.Cells("MODIFY_BY").Value
                        .Columns.Add("MODIFY_DATE", Type.GetType("System.String")).DefaultValue = DBNull.Value  'Row.Cells("MODIFY_DATE").Val
                    End With
                    For Each Row As Janus.Windows.GridEX.GridEXRow In rows1
                        Dim nRow As DataRow = tblTempRow.NewRow()
                        'nRow = CType(Me.grdBrandPack.DataSource, DataView).Table.NewRow()
                        nRow("PROJ_BRANDPACK_ID") = Row.Cells("PROJ_BRANDPACK_ID").Value
                        nRow("PROJ_REF_NO") = Row.Cells("PROJ_REF_NO").Value
                        nRow("BRANDPACK_ID") = Row.Cells("PROJ_REF_NO").Value
                        nRow("PRICE") = Row.Cells("PRICE").Value
                        nRow("CREATE_DATE") = Row.Cells("CREATE_DATE").Value
                        nRow("CREATE_BY") = Row.Cells("CREATE_BY").Value
                        nRow("MODIFY_BY") = Row.Cells("MODIFY_BY").Value
                        nRow("MODIFY_DATE") = Row.Cells("MODIFY_DATE").Value

                        nRow.EndEdit()
                        tblTempRow.Rows.Add(nRow)
                    Next
                    If Not IsNothing(Me.DSBrandPack) Then
                        Me.DSBrandPack.RejectChanges()
                    End If
                    Me.DSBrandPack = New DataSet("DSBrandpack") : Me.DSBrandPack.Clear()
                    Dim dt As DataTable = Me.clsProj.CreateViewBrandPack(Me.MultiColumnCombo1.Value.ToString(), Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), True)

                    Me.DSBrandPack.Tables.Add(dt)
                    Me.grdBrandPack.SetDataBinding(Me.DSBrandPack.Tables(0).DefaultView(), "")
                    ''looping checked 
                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdBrandPack.GetDataRows()
                        For Each row1 As DataRow In tblTempRow.Rows
                            If row1("BRANDPACK_ID").ToString() = row.Cells("BRANDPACK_ID").Value.ToString() Then
                                'row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked
                                row.Delete()
                                Exit For
                            End If
                        Next
                    Next
                    Me.grdBrandPack.UpdateData() : Me.DSBrandPack.AcceptChanges()
                    Dim newRow As DataRow = Nothing
                    For Each row As DataRow In tblTempRow.Rows
                        newRow = Me.DSBrandPack.Tables(0).NewRow()
                        newRow("PROJ_BRANDPACK_ID") = row("PROJ_BRANDPACK_ID")
                        newRow("PROJ_REF_NO") = row("PROJ_REF_NO")
                        newRow("BRANDPACK_ID") = row("BRANDPACK_ID")
                        newRow("PRICE") = row("PRICE")
                        newRow("CREATE_DATE") = row("CREATE_DATE")
                        newRow("CREATE_BY") = row("CREATE_BY")
                        newRow("MODIFY_BY") = row("MODIFY_BY")
                        newRow("MODIFY_DATE") = row("MODIFY_DATE")
                        newRow("HAS_CHANGED") = True
                        newRow.EndEdit()
                        Me.DSBrandPack.Tables(0).Rows.Add(newRow)
                    Next
                    Me.DSBrandPack.AcceptChanges()
                    Me.grdBrandPack.Refetch()
                    'CType(Me.grdBrandPack.DataSource, DataView).Table().DataSet.AcceptChanges()
                    'Me.grdBrandPack.Refetch()
                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdBrandPack.GetDataRows()
                        For Each row1 As DataRow In tblTempRow.Rows
                            If row1("BRANDPACK_ID").ToString() = row.Cells("BRANDPACK_ID").Value.ToString() Then
                                row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked
                                row1.Delete()
                                Exit For
                            End If
                        Next
                    Next
                    Me.grdBrandPack.Refetch()
                End If
            End If
            Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_dtPicDate_ValueChanged")
        Finally
            Me.StateFilling = FillingGrid.HasFilled : Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

    '#Region " Check Box "
    '    Private Sub chkShowAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Try
    '            Me.StateFilling = FillingGrid.Filling
    '            'If Me.chkShowAll.Checked = True Then
    '            '    Me.clsProj.GetDataViewForGridEx().RowFilter = ""
    '            'Else
    '            '    Me.clsProj.GetDataViewForGridEx().RowFilter = "END_DATE > " & NufarmBussinesRules.SharedClass.ServerDateString
    '            'End If
    '            Me.BindGrid(Me.GridEX1, Me.clsProj.GetDataViewForGridEx())
    '        Catch ex As Exception
    '        Finally
    '            Me.StateFilling = FillingGrid.HasFilled

    '        End Try
    '    End Sub
    '#End Region

    '#Region " Handler "
    '    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        Application.DoEvents()
    '        If Me.WhileSaving = Saving.Succes Then
    '            For i As Integer = 0 To Me.ResultRandom
    '                For index As Integer = 1 To 30
    '                    MyBase.ST.Refresh()
    '                    MyBase.ST.Label1.Refresh()
    '                    MyBase.ST.PictureBox1.Refresh()
    '                Next
    '            Next
    '            Me.RefreshData()
    '            'Me.ClearControl(Me.grpDetail)
    '            'Me.grpDetail.Dock = DockStyle.None
    '            'Me.grpDetail.Visible = False
    '            Me.UnabledControl()
    '            Me.GridEX2.Enabled = False
    '            Me.CloseProgres(True)
    '            Me.ModeSave = Mode.Save
    '            'Me.chkShowAll_CheckedChanged(Me.chkShowAll, New System.EventArgs())
    '            Me.StateFilling = FillingGrid.HasFilled
    '        ElseIf Me.WhileSaving = Saving.Failed Then
    '            Me.CloseProgres(False)
    '        End If
    '    End Sub

    '#End Region

#End Region


    Private Sub txtProjName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProjName.TextChanged
        If Not Me.HasLoad Then : Return : End If
        If Me.StateFilling = FillingGrid.Filling Then : Return : End If
        If Me.txtProjName.Text <> "" Then
            If String.IsNullOrEmpty(Me.txtProjRefNo.Text) Then
                Me.txtProjRefNo.Focus() : Me.txtProjRefNo.SelectionLength = Me.txtProjRefNo.Text.Length
                Me.txtProjName.Text = ""
            End If
        End If
        Me.btnSave.Enabled = Me.HasChangedData()
    End Sub

    Private Sub ApplyFilter()
        Me.ShowThread()
        Dim DistributorID As String = String.Empty
        If Me.mcbDistributor.SelectedIndex <> -1 Then
            If Not IsNothing(Me.mcbDistributor.Value) Then
                If Me.mcbDistributor.Text <> "" Then
                    DistributorID = Me.mcbDistributor.Value.ToString()
                End If
            End If
        End If
        Me.mcbDistributor.ReadOnly = True
        Dim Dv As DataView = Me.clsProj.getProjectDetail(Convert.ToDateTime(Me.dtPicfrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), True, DistributorID)
        Me.BindGrid(Me.GridEX1, Dv)
        Me.StatProg = StatusProgress.None
        Me.mcbDistributor.ReadOnly = False
    End Sub
    Private Sub btnAplyFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyFilter.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ApplyFilter() : Me.ClearControl()
        Catch ex As Exception
            Me.StateFilling = FillingGrid.HasFilled : Me.StateFillingMCB = FillingMCB.HasFilled : Me.StatProg = StatusProgress.None : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.mcbDistributor.ReadOnly = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSearchEntryDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEntryDistributor.btnClick
        Try
            Try
                If Me.MultiColumnCombo1.ReadOnly Then : Return : End If
                Me.Cursor = Cursors.WaitCursor
                Me.StateFillingMCB = FillingMCB.Filling
                Dim dv As DataView = Me.clsProj.CreateViewDistributor(Me.MultiColumnCombo1.Text, True, Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), True)
                'Me.BindMultiColumnCombo(Me.clsProj.ViewDistributor(), "DISTRIBUTOR_NAME LIKE '%" + Me.MultiColumnCombo1.Text + "%'")
                Me.BindMultiColumnCombo(dv, Me.MultiColumnCombo1)
                Dim itemCount As Integer = Me.MultiColumnCombo1.DropDownList().RecordCount()
                Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
                Me.mcbDistributor.Focus()
                ''clearkan brandpack
                'Me.bindCheckedCombo(Nothing, "", "")
            Catch ex As Exception
                Me.ShowMessageInfo(ex.Message)
            Finally
                Me.StateFillingMCB = FillingMCB.HasFilled : Me.Cursor = Cursors.Default
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSearchDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.StateFillingMCB = FillingMCB.Filling
            'Dim dv As DataView = Me.clsProj.CreateViewDistributor(Me.mcbDistributor.Text, False, True, True)
            'Me.BindMultiColumnCombo(Me.clsProj.ViewDistributor(), "DISTRIBUTOR_NAME LIKE '%" + Me.MultiColumnCombo1.Text + "%'")
            'Me.BindMultiColumnCombo(dv, Me.mcbDistributor)
            Dim itemCount As Integer = Me.mcbDistributor.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
            Me.mcbDistributor.Focus()
            ''clearkan brandpack
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.StateFillingMCB = FillingMCB.HasFilled : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        Try
            If Me.GridEX1.SelectedItems.Count <= 0 Then
                Return
            End If
            If Not (Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.StateFillingMCB = FillingMCB.Filling
                Me.ClearControl(Me.grpEntryData)
                'Me.BindGrid(Me.GridEX2, Nothing)
                Return
            End If
            Me.grpEntryData.Height = Me.grpEditHeight
            Me.Cursor = Cursors.WaitCursor
            Me.ModeSave = Mode.Update : Me.InflateData()
            'If Not IsNothing(Me.GridEX1.GetValue("PROJ_BRANDPACK_ID")) And Not IsDBNull(Me.GridEX1.GetValue("PROJ_BRANDPACK_ID")) Then
            '    If Me.GridEX1.GetValue("PROJ_BRANDPACK_ID").ToString() <> "" Then
            '        Me.clsProj.GetDasetBranpackDetail(Me.GridEX1.GetValue("PROJ_BRANDPACK_ID"))
            '    Else
            '        Me.clsProj.GetDasetBranpackDetail("")
            '    End If
            'Else
            '    Me.clsProj.GetDasetBranpackDetail("")
            'End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.StateFillingMCB = FillingMCB.HasFilled : Me.StateFilling = FillingGrid.HasFilled : Me.Cursor = Cursors.Default
        End Try


    End Sub

    'Private Sub mcbBrandPack_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        'chek apakah status mode update/inser
    '        Me.Cursor = Cursors.WaitCursor
    '        If Me.ModeSave = Mode.Update Then
    '            'check seluruh value listOriginalBrandPack
    '            If tblRefPO.Rows.Count > 0 Then
    '                If Me.mcbBrandPack.CheckedValues Is Nothing Then
    '                    For i As Integer = 0 To Me.tblRefPO.Rows.Count - 1
    '                        ''check apakah ada brandpack yang hilang di checkedValues
    '                        If Me.clsProj.HasRefernceDataBPDetail(tblRefPO.Rows(i)("PROJ_BRANDPACK_ID").ToString(), i = Me.tblRefPO.Rows.Count - 1) Then
    '                            Me.ShowMessageInfo("Can not Delete brandpack" & vbCrLf & tblRefPO.Rows(i)("BRANDPACK_NAME").ToString() & " Has used In PO " & tblRefPO.Rows(i)("PO_REF_NO"))
    '                            Me.StateFillingMCB = FillingMCB.Filling
    '                            Me.mcbBrandPack.ValuesDataSource = Me.ListOriginalBrandPack.ToArray()
    '                            Me.StateFillingMCB = FillingMCB.HasFilled
    '                            Me.mcbBrandPack.Focus()
    '                            Return
    '                        End If
    '                    Next
    '                ElseIf Me.mcbBrandPack.CheckedValues.Length <= 0 Then
    '                    For i As Integer = 0 To Me.tblRefPO.Rows.Count - 1
    '                        If Me.clsProj.HasRefernceDataBPDetail(tblRefPO.Rows(i)("PROJ_BRANDPACK_ID").ToString(), i = Me.tblRefPO.Rows.Count - 1) Then
    '                            Me.ShowMessageInfo("Can not Delete brandpack" & vbCrLf & tblRefPO.Rows(i)("BRANDPACK_NAME").ToString() & " Has used In PO " & tblRefPO.Rows(i)("PO_REF_NO"))
    '                            Me.StateFillingMCB = FillingMCB.Filling
    '                            Me.mcbBrandPack.ValuesDataSource = Me.ListOriginalBrandPack.ToArray()
    '                            Me.StateFillingMCB = FillingMCB.HasFilled
    '                            Me.mcbBrandPack.Focus()
    '                            Return
    '                        End If
    '                    Next
    '                ElseIf Me.mcbBrandPack.CheckedValues.Length > 0 Then
    '                    Dim items() As Object = Me.mcbBrandPack.CheckedValues()
    '                    Dim listString As New List(Of String)
    '                    For i1 As Integer = 0 To items.Length - 1
    '                        listString.Add(items(i1))
    '                    Next
    '                    For i As Integer = 0 To Me.tblRefPO.Rows.Count - 1
    '                        ''check apakah ada brandpack yang hilang di checkedValues
    '                        Dim BrandPackID As String = tblRefPO.Rows(i)("BRANDPACK_ID").ToString()
    '                        If Not listString.Contains(BrandPackID) Then
    '                            Me.ShowMessageInfo("Can not Delete brandpack" & vbCrLf & tblRefPO.Rows(i)("BRANDPACK_NAME").ToString() & " Has used In PO " & tblRefPO.Rows(i)("PO_REF_NO"))
    '                            Me.StateFillingMCB = FillingMCB.Filling
    '                            Me.mcbBrandPack.ValuesDataSource = Me.ListOriginalBrandPack.ToArray()
    '                            Me.StateFillingMCB = FillingMCB.HasFilled
    '                            Me.mcbBrandPack.Focus()
    '                            Return
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Me.ShowMessageInfo(ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub mcbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbDistributor.ValueChanged
        Try
            If IsNothing(Me.mcbDistributor) Then : Return : End If
            If Me.mcbDistributor.SelectedIndex <= -1 Then : Return : End If
            If Me.mcbDistributor.Text = "" Then : Return : End If
            If Me.StateFillingMCB = FillingMCB.Filling Then : Return : End If
            Me.ApplyFilter() : Me.ClearControl()
        Catch ex As Exception
            Me.StateFilling = FillingGrid.Filling : Me.StateFillingMCB = FillingMCB.HasFilled : Me.StatProg = StatusProgress.None : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.mcbDistributor.ReadOnly = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtSearch_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Enter
        If Not Me.ValidHeader() Then
            Return
        End If
    End Sub

    Private Sub grdBrandPack_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdBrandPack.Enter
        If Not Me.ValidHeader() Then
            Return
        End If
    End Sub
    Private Sub SetValuebrandPack(ByVal HasChanged As Boolean)
        If Not IsDBNull(Me.grdBrandPack.GetValue("PROJ_REF_NO")) And Not IsNothing(Me.grdBrandPack.GetValue("PROJ_REF_NO")) Then
            If Me.grdBrandPack.GetValue("PROJ_REF_NO") <> Me.txtProjRefNo.Text.TrimStart().TrimEnd() Then
                Me.grdBrandPack.SetValue("PROJ_REF_NO", Me.txtProjRefNo.Text.TrimStart().TrimEnd())
            End If
        Else
            Me.grdBrandPack.SetValue("PROJ_REF_NO", Me.txtProjRefNo.Text.TrimStart().TrimEnd())
        End If
        Dim ProjBrandPackID As String = Me.txtProjRefNo.Text.TrimStart().TrimEnd() + "-" + Me.grdBrandPack.GetValue("BRANDPACK_ID").ToString()
        If Not IsNothing(Me.grdBrandPack.GetValue("PROJ_BRANDPACK_ID")) And Not IsDBNull(Me.grdBrandPack.GetValue("PROJ_BRANDPACK_ID")) Then
            If Me.grdBrandPack.GetValue("PROJ_BRANDPACK_ID") <> ProjBrandPackID Then
                Me.grdBrandPack.SetValue("PROJ_BRANDPACK_ID", ProjBrandPackID)
            End If
        Else
            Me.grdBrandPack.SetValue("PROJ_BRANDPACK_ID", ProjBrandPackID)
        End If
        If Me.ModeSave = Mode.Update Then
            Me.grdBrandPack.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
            Me.grdBrandPack.SetValue("MODIFY_DATE", NufarmBussinesRules.SharedClass.ServerDate)
        ElseIf Me.ModeSave = Mode.Save Then
            Me.grdBrandPack.SetValue("CREATE_DATE", NufarmBussinesRules.SharedClass.ServerDate)
            Me.grdBrandPack.SetValue("CREATE_BY", NufarmBussinesRules.User.UserLogin.UserName)
        End If
        Me.grdBrandPack.SetValue("HAS_CHANGED", HasChanged)
    End Sub
    Private Sub grdBrandPack_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdBrandPack.CellUpdated
        Try
            If Me.StateFilling = FillingGrid.Filling Then : Return : End If
            If Not Me.HasLoad Then : Return : End If
            If e.Column.Key = "PRICE" Then
                Me.Cursor = Cursors.WaitCursor
                'check apakah brandpack sudah ada referencnya
                If Me.ModeSave = Mode.Update Then
                    If Me.clsProj.HasRefernceDataBPDetail(Me.grdBrandPack.GetValue("PROJ_BRANDPACK_ID").ToString(), True) Then
                        Me.ShowMessageInfo(Me.MessageDataCantChanged) : Me.grdBrandPack.CancelCurrentEdit() : Return
                    End If
                End If
                Me.SetValuebrandPack(True)
            End If
            Me.grdBrandPack.UpdateData() : Me.btnSave.Enabled = Me.HasChangedData()
        Catch ex As Exception
            Me.grdBrandPack.CancelCurrentEdit() : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub filColumnWIthDefaultValue(ByVal HasChanged As Boolean)
        Me.grdBrandPack.SetValue("PROJ_BRANDPACK_ID", DBNull.Value)
        Me.grdBrandPack.SetValue("PROJ_REF_NO", DBNull.Value)
        Me.grdBrandPack.SetValue("PRICE", DBNull.Value)
        Me.grdBrandPack.SetValue("CREATE_DATE", NufarmBussinesRules.SharedClass.ServerDate)
        Me.grdBrandPack.SetValue("CREATE_BY", NufarmBussinesRules.User.UserLogin.UserName)
        Me.grdBrandPack.SetValue("MODIFY_BY", DBNull.Value)
        Me.grdBrandPack.SetValue("MODIFY_DATE", DBNull.Value)
        Me.grdBrandPack.SetValue("HAS_CHANGED", HasChanged)
    End Sub

    Private Sub grdBrandPack_RowCheckStateChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles grdBrandPack.RowCheckStateChanged
        Try
            If Me.StateFilling = FillingGrid.Filling Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Dim DV As DataView = Nothing
            If (e.ChangeType = Janus.Windows.GridEX.CheckStateChangeType.RowChange) Then
                If e.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked Then
                    If Me.ModeSave = Mode.Update Then
                        ''check reference
                        Dim projBrandPackID As String = Me.grdBrandPack.GetValue("PROJ_BRANDPACK_ID").ToString()
                        If (Me.clsProj.HasRefernceDataBPDetail(projBrandPackID, False)) Then
                            Me.ShowMessageInfo(Me.MessageDataCantChanged)
                            Me.StateFilling = FillingGrid.Filling
                            e.Row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked
                            Me.StateFilling = FillingGrid.HasFilled
                            Return
                        End If
                        Me.clsProj.DeleteProject(Me.txtProjRefNo.Text.TrimStart().TrimEnd(), projBrandPackID, True)
                        DV = CType(Me.GridEX1.DataSource, DataView).Table.Copy().DefaultView()
                        DV.Sort = "PROJ_BRANDPACK_ID"
                        Dim Index As Integer = DV.Find(projBrandPackID)
                        If Index <> -1 Then : DV(Index).Delete() : End If : DV.Table.AcceptChanges()
                        'check apakah masih ada child brandpack 
                        'delete data di gridex
                        ''fill column with defaultvalue
                        'delete data di database
                    End If
                    Me.filColumnWIthDefaultValue(False)
                    Dim BrandPackID As String = Me.grdBrandPack.GetValue("BRANDPACK_ID").ToString()
                    If Me.ChekedBrandPackIDS.Contains(BrandPackID) Then
                        Me.ChekedBrandPackIDS.Remove(BrandPackID)
                    End If
                    If Me.ModeSave = Mode.Update Then
                        If Me.ChekedBrandPackIDS.Count <= 0 Then
                            'reload data
                            Me.ApplyFilter()
                            Me.StateFillingMCB = FillingMCB.Filling
                            Me.StateFilling = FillingGrid.Filling
                            Me.ClearControl(Me.grpProjectHeader)
                            Me.txtAddress.Text = ""
                            If Not IsNothing(Me.DSBrandPack) Then : Me.DSBrandPack.RejectChanges() : End If
                            Me.grdBrandPack.SetDataBinding(Nothing, "")
                            Me.ChekedBrandPackIDS.Clear()
                            Me.txtProjRefNo.ReadOnly = False
                            Me.MultiColumnCombo1.ReadOnly = False
                            Me.dtPicStartDate.ReadOnly = False
                            Me.ModeSave = Mode.Save
                            Me.DProject = New DomainProject()
                        Else
                            Me.BindGrid(Me.GridEX1, DV)
                        End If
                    End If
                Else
                    Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.SetValuebrandPack(True)
                    Dim BrandPackID As String = Me.grdBrandPack.GetValue("BRANDPACK_ID").ToString()
                    If Not Me.ChekedBrandPackIDS.Contains(BrandPackID) Then
                        Me.ChekedBrandPackIDS.Add(BrandPackID)
                    End If
                End If
            End If
        Catch ex As Exception
            Me.StatProg = StatusProgress.None
            Me.StateFilling = FillingGrid.Filling
            If e.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                e.Row.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked
            ElseIf e.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked Then
                e.Row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked
            End If
            Me.ShowMessageInfo(ex.Message) : Me.StateFilling = FillingGrid.HasFilled
        Finally
            Me.StateFilling = FillingGrid.HasFilled : Me.StateFillingMCB = FillingMCB.HasFilled : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdBrandPack_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdBrandPack.CurrentCellChanged
        Try
            If Me.StateFilling = FillingGrid.Filling Then : Return : End If
            If Not Me.grdBrandPack.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then : Return : End If
            If Me.grdBrandPack.GetRow().CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.grdBrandPack.DataSource) Then
                CType(Me.grdBrandPack.DataSource, DataView).RowFilter = "BRANDPACK_NAME LIKE '%" + Me.txtSearch.Text.TrimStart().TrimEnd() + "%'"
                Me.grdBrandPack.Refetch()
                ''loooping checkbrandpack_id
                Me.StateFilling = FillingGrid.Filling
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdBrandPack.GetDataRows()
                    Dim BrandPackID As String = row.Cells("BRANDPACK_ID").Value.ToString()
                    If Me.ChekedBrandPackIDS.Contains(BrandPackID) Then
                        row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked
                    End If
                Next
            End If
            Me.StateFilling = FillingGrid.HasFilled
        Catch ex As Exception
            Me.StateFilling = FillingGrid.HasFilled : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddress.TextChanged
        If Not Me.HasLoad Then : Return : End If
        If Me.StateFilling = FillingGrid.Filling Then : Return : End If
        If Me.txtAddress.Text <> "" Then
            If String.IsNullOrEmpty(Me.txtAddress.Text) Then
                Me.txtProjRefNo.Focus() : Me.txtProjRefNo.SelectionLength = Me.txtProjRefNo.Text.Length
                Me.txtProjName.Text = ""
            End If
        End If
        Me.btnSave.Enabled = Me.HasChangedData()
    End Sub
End Class
