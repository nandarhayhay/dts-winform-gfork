Public Class AgreementRelation

#Region " Deklarasi "
    Friend Event CloseThis()
    Private Mode As SaveMode
    Private clsAgInclude As NufarmBussinesRules.DistributorAgreement.Include
    Private TabAcktive As ActiveTab
    Private QS_FLAG As String
    Private Brand_IDHide As String
    Private Hload As HasLoad
    Private MaySelectGrid As CanSelectGridEx
    Private AGREE_BRAND_ID As String 'Agree_Brand_ID untuk mode Update
    Private COMB_BRAND_ID As String
    Private HFC As HasFilledComboFirstSecond
    Private Brand_ID As String 'Brand_ID untuk mode Save
    Private NewAgree_Brand_ID As String 'brand_ID untuk mode Save
    Private sGrid As SelectedGrid
    Private SFG As StateFillingGrid
    Private IsEnabledEditgrid As Boolean 'untuk mentrap supaya user tidak mengedit datagrid
    Private SFM As StateFillingCombo
    Public CMain As Main = Nothing
    Private AgreementEndDate As DateTime = Nothing
    Private isTransitionTime As Boolean = False
    Private ds4MPeriode As DataSet = Nothing
    Private DPrevDisc As New DomPrevousDisc()
#End Region

#Region " Enum "
    Private Enum StateFillingCombo
        HasFilled
        Filling
    End Enum
    Private Enum SaveMode
        Save
        Update
    End Enum
    Private Enum ActiveTab
        BrandInclude
        CombinedBrand
        OAHistory
    End Enum
    'enum supaya gridselection changed mati pada saat load
    Private Enum HasLoad
        Yes
        NotYet
    End Enum
    'enum untuk mematikan selection change pada saat grid ex membinding data
    Private Enum CanSelectGridEx
        Can
        CanNot
    End Enum
    Private Enum HasFilledComboFirstSecond
        Done
        Filling
    End Enum
    Private Enum SelectedGrid
        GridEx1
        DataGrid
    End Enum
    Private Enum StateFillingGrid
        HasFilled
        Filling
    End Enum
    Private Class DomPrevousDisc
        Public PBQ3 As Decimal = 0
        Public PBQ4 As Decimal = 0
        Public PBS2 As Decimal = 0
        Public CPQ1 As Decimal = 0
        Public CPQ2 As Decimal = 0
        Public CPQ3 As Decimal = 0
        Public CPS1 As Decimal = 0
        Public PBY As Decimal = 0
        Public PBF2 As Decimal = 0
        Public PBF3 As Decimal = 0
        Public CPF1 As Decimal = 0
        Public CPF2 As Decimal = 0
    End Class
#End Region

#Region " Sub "
    Private Function HasChangedDomPrev() As Boolean
        With Me.DPrevDisc
            If .CPF1 <> Me.txtCPF1.Value Then
                Return True
            ElseIf .CPF2 <> Me.txtCPF2.Value Then
                Return True
            ElseIf .CPQ1 <> Me.txtCPQ1.Value Then
                Return True
            ElseIf .CPQ2 <> Me.txtCPQ2.Value Then
                Return True
            ElseIf .CPQ3 <> Me.txtCPQ3.Value Then
                Return True
            ElseIf .CPS1 <> Me.txtCPS1.Value Then
                Return True
            ElseIf .PBF2 <> Me.txtPBF2.Value Then
                Return True
            ElseIf .PBF3 <> Me.txtPBF3.Value Then
                Return True
            ElseIf .PBQ3 <> Me.txtPBQ3.Value Then
                Return True
            ElseIf .PBQ4 <> Me.txtPBQ4.Value Then
                Return True
            ElseIf .PBS2 <> Me.txtPBS2.Value Then
                Return True
            ElseIf .PBY <> Me.txtPBYear.Value Then
                Return True
            End If
        End With

        Return False
    End Function
    Private Sub FormatGridDiscount(ByVal DGV As DataGridView)
        For Each col As DataGridViewColumn In DGV.Columns
            'PRGSV_DISC_PCT
            'AGREE_BRAND_ID
            'UP_TO_PCT
            'UNIQUE_ID
            'IDApp
            'QSY_DISC_FLAG
            If col.DataPropertyName = "PRGSV_DISC_PCT" Then
                If col.HeaderText <> "Discount" Then
                    col.HeaderText = "Discount"
                    col.Width = 60
                End If
            ElseIf col.DataPropertyName = "AGREE_BRAND_ID" Then
                col.Visible = False
            ElseIf col.DataPropertyName = "UP_TO_PCT" Then
                col.HeaderText = "More than %"
                col.Width = 60
            ElseIf col.DataPropertyName = "UNIQUE_ID" Then
                col.Visible = False
            ElseIf col.DataPropertyName = "IDApp" Then
                col.Visible = False
            ElseIf col.DataPropertyName = "QSY_DISC_FLAG" Then
                col.HeaderText = "FLAG"
                col.Width = 40
            End If
        Next
    End Sub
    Private Sub clearGivenProgressive()
        Me.txtPBQ4.Text = String.Empty
        Me.txtPBS2.Text = String.Empty
        Me.txtCPQ1.Text = String.Empty
        Me.txtCPQ2.Text = String.Empty
        Me.txtCPQ3.Text = String.Empty
        Me.txtCPS1.Text = String.Empty
        Me.txtPBYear.Text = String.Empty
        Me.txtPBF2.Text = String.Empty
        Me.txtPBF3.Text = String.Empty
        Me.txtCPF1.Text = String.Empty
        Me.txtCPF2.Text = String.Empty

    End Sub
    Private Function GetTargetYear(ByVal Flag As String)
        Dim s As String = ""
        Select Case Flag
            Case "S"
                s = "Yearly = : " & CStr(Me.txtS1QTY.Value + Me.txtS2QTY.Value)
            Case "Q"
                s = "Yearly = : " & CStr(Me.txtQ1QTY.Value + Me.txtQ2QTY.Value + Me.txtQ3QTY.Value + Me.txtQ4QTY.Value)
                'transisi
                s &= CStr(Me.txtFMP1.Value + Me.txtFMP2.Value + Me.txtFMP3.Value)
            Case "F"
                s = "Yearly = : " & CStr(Me.txtFMP1.Value + Me.txtFMP2.Value + Me.txtFMP3.Value)
        End Select
        Return s
    End Function
    'ini adalah sub unutk mengclear data bila di mcb valuenya ""
    Private Sub ClearData()
        Me.UnabledTextBox(Me.grpQuarterly)
        Me.UnabledTextBox(Me.grpSemesterly)
        Me.ClearControl(ActiveTab.BrandInclude)
        Me.ClearControl(ActiveTab.CombinedBrand)
        Me.ClearControl(ActiveTab.OAHistory)
        Me.BindGridEX(Nothing, "")
        Me.BindGrid(Nothing, "")
        Me.grdunAddedBrandPack.DataSource = Nothing
        Me.btnAddNew.Enabled = False
        Me.grpTypeDiscount.Visible = False
    End Sub

    Private Overloads Sub ClearControl(ByVal tbActive As ActiveTab)
        Select Case tbActive
            Case ActiveTab.BrandInclude
                Me.txtBrandName.Text = ""
                Me.txtFilterBrandName.Text = ""
                Me.txtGiven.Text = ""
                Me.txtQ1QTY.Value = ""
                Me.txtQ2QTY.Value = ""
                Me.txtQ3QTY.Value = ""
                Me.txtQ4QTY.Value = ""
                Me.txtS1QTY.Text = ""
                Me.txtS2QTY.Text = ""
            Case ActiveTab.CombinedBrand
                Me.grpComS.Visible = False
                Me.grpCombQ.Visible = False
                Me.TreeView1.Visible = False
                Me.cmbSeconBrand.DataSource = Nothing
                Me.grpComboFirstSecond.Visible = False
            Case ActiveTab.OAHistory
        End Select
    End Sub

    Private Function IsValidDatagrid() As Boolean
        If Not IsNothing(Me.clsAgInclude.getDsPeriod) Then
            If Me.clsAgInclude.getDsPeriod.HasChanges() Then
                If (CType(Me.dgvPeriodic.DataSource, DataTable).DataSet.HasChanges()) Then
                    If Me.QS_FLAG = "Q" Then
                        For i As Integer = 0 To Me.clsAgInclude.GetTableQuarterly().Rows.Count - 1
                            If (Me.dgvPeriodic.Item(2, i).Value Is DBNull.Value) Or (Me.dgvPeriodic.Item(3, i).Value Is DBNull.Value) Then
                                Me.baseTooltip.SetToolTip(Me.dgvPeriodic, "Invalid / Null value." & vbCrLf & "Some column has an invalid / null value")
                                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dgvPeriodic), Me.dgvPeriodic, 2000)
                                Me.dgvPeriodic.Focus()
                                Return False
                            End If
                        Next
                    ElseIf Me.QS_FLAG = "S" Then
                        For i As Integer = 0 To Me.clsAgInclude.GetTableSemesterly().Rows.Count - 1
                            If (Me.dgvPeriodic.Item(2, i).Value Is DBNull.Value) Or (Me.dgvPeriodic.Item(3, i).Value Is DBNull.Value) Then
                                Me.baseTooltip.SetToolTip(Me.dgvPeriodic, "Invalid / Null value." & vbCrLf & "Some column has an invalid / null value")
                                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dgvPeriodic), Me.dgvPeriodic, 2000)
                                Me.dgvPeriodic.Focus()
                                Return False
                            End If
                        Next
                    End If
                End If
                If (CType(Me.dgvYearly.DataSource, DataTable).DataSet.HasChanges()) Then
                    For i As Integer = 0 To Me.clsAgInclude.GetTableYearly().Rows.Count - 1
                        If (Me.dgvPeriodic.Item(2, i).Value Is DBNull.Value) Or (Me.dgvPeriodic.Item(3, i).Value Is DBNull.Value) Then
                            Me.baseTooltip.SetToolTip(Me.dgvPeriodic, "Invalid / Null value." & vbCrLf & "Some column has an invalid / null value")
                            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dgvPeriodic), Me.dgvPeriodic, 2500)
                            Me.dgvPeriodic.Focus()
                            Return False
                        End If
                    Next
                End If
            End If
        End If
        Return True
    End Function


    Private Sub ReadAccecs()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AgreementRelation = True Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grdAddedBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                Me.btnDeleteQ1.Enabled = True : Me.btnDeleteS.Enabled = True
                Me.GridEX2.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                Me.btnDeleteQ1.Enabled = False : Me.btnDeleteS.Enabled = False
                Me.GridEX2.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AgreementRelation = True Then
                If Not IsNothing(Me.grdAddedBrandPack.DataSource) Then
                    If Me.grdAddedBrandPack.RecordCount > 0 Then
                        GetAccesChangeAgreement()
                    Else
                        Me.SavingChanges1.btnSave.Visible = True
                    End If
                Else
                    Me.SavingChanges1.btnSave.Visible = True
                End If
                Me.GridEX2.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                Me.GridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                Me.btnAddNew.Visible = True : Me.cmbSeconBrand.Enabled = True
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AgreementRelation = True Then
                If Not IsNothing(Me.grdAddedBrandPack.DataSource) Then
                    If Me.grdAddedBrandPack.RecordCount > 0 Then
                        GetAccesChangeAgreement()
                    Else
                        Me.SavingChanges1.btnSave.Visible = True
                    End If
                Else
                    Me.SavingChanges1.btnSave.Visible = True
                End If
                Me.btnAddNew.Visible = False : Me.cmbSeconBrand.Enabled = True
                Me.GridEX2.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                Me.GridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.SavingChanges1.btnSave.Visible = False : Me.btnAddNew.Visible = False : Me.cmbSeconBrand.Enabled = False
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AgreementRelation Then
                Me.GridEX2.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                Me.GridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            End If
        End If
    End Sub

    Friend Sub InitializeData()
        Me.Hload = HasLoad.NotYet
        Me.LoadData()
    End Sub

    Private Sub LoadData()
        Me.clsAgInclude = New NufarmBussinesRules.DistributorAgreement.Include
        Me.clsAgInclude.GetData()
        Me.SFM = StateFillingCombo.Filling
        Me.BindMulticolumnCombo("")
        'edit after dubugging
        Me.isTransitionTime = NufarmBussinesRules.SharedClass.ServerDate <= New DateTime(2020, 7, 31) And NufarmBussinesRules.SharedClass.ServerDate >= New DateTime(2019, 8, 1)
    End Sub
    Private Sub EnabledTextBoxFMP()
        For Each Item As Control In Me.tbFMP.Controls
            If TypeOf (Item) Is TextBox Then
                Item.Enabled = True
                Item.BackColor = Color.FromArgb(194, 217, 247)
            ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                Item.Enabled = True
            End If
        Next
    End Sub
    Private Sub UnabledTextBoxFMP()
        For Each Item As Control In Me.tbFMP.Controls
            If TypeOf (Item) Is TextBox Then
                Item.Enabled = False
                Item.BackColor = Color.White
            ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                Item.Enabled = False
            End If
        Next
    End Sub
    Private Sub EnabledTextBox(ByVal grp As GroupBox)
        Select Case grp.Name
            Case "grpQuarterly"
                For Each Item As Control In Me.grpQuarterly.Controls
                    If TypeOf (Item) Is TextBox Then
                        Item.Enabled = True
                        Item.BackColor = Color.FromArgb(194, 217, 247)
                    ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                        Item.Enabled = True
                    End If
                Next
            Case "grpSemesterly"
                For Each Item As Control In Me.grpSemesterly.Controls
                    If TypeOf (Item) Is TextBox Then
                        Item.Enabled = True
                        Item.BackColor = Color.FromArgb(194, 217, 247)
                    ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                        Item.Enabled = True
                    End If
                Next
            Case "tbFMP"
                'For Each Item As Control In Me.tb.Controls
                '    If TypeOf (Item) Is TextBox Then
                '        Item.Enabled = True
                '        Item.BackColor = Color.FromArgb(194, 217, 247)
                '    ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                '        Item.Enabled = True
                '    End If
                'Next
        End Select
    End Sub

    Private Sub UnabledTextBox(ByVal grp As GroupBox)
        Select Case grp.Name
            Case "grpQuarterly"
                For Each Item As Control In Me.grpQuarterly.Controls
                    If TypeOf (Item) Is TextBox Then
                        Item.Enabled = False
                        Item.BackColor = Color.White
                    ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                        Item.Enabled = False
                    End If
                Next
            Case "grpSemesterly"
                For Each Item As Control In Me.grpSemesterly.Controls
                    If TypeOf (Item) Is TextBox Then
                        Item.Enabled = False
                        Item.BackColor = Color.White
                    ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                        Item.Enabled = False
                    End If
                Next
        End Select
    End Sub
    Private Sub BindGrid4MPeriode()
        Me.GridEX2.SetDataBinding(Me.ds4MPeriode.Tables(0).DefaultView(), "")

        'FLAG
        Dim VList() As String = {"F1", "F2", "F3"}
        Dim ColFlag As Janus.Windows.GridEX.GridEXColumn = Me.GridEX2.RootTable.Columns("FLAG")
        ColFlag.EditType = Janus.Windows.GridEX.EditType.DropDownList
        ColFlag.AutoComplete = True : ColFlag.HasValueList = True
        Dim ValueListFlag As Janus.Windows.GridEX.GridEXValueListItemCollection = ColFlag.ValueList
        ValueListFlag.PopulateValueList(VList, "FLAG")
        ColFlag.EditTarget = Janus.Windows.GridEX.EditTarget.Value
        ColFlag.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text

        'PRODUCT CATEGORY
        Dim listCat As New List(Of String)
        'get cat from origina; schema r
        If ds4MPeriode.Tables(0).Rows.Count > 0 Then
            'if not listcat.Contains(ds4MPeriode.Tables(0).Rows(
            For i As Integer = 0 To ds4MPeriode.Tables(0).Rows.Count - 1
                Dim prodCat As String = ds4MPeriode.Tables(0).Rows(i)("PRODUCT_CATEGORY").ToString()
                If Not listCat.Contains(prodCat) Then
                    listCat.Add(prodCat)
                End If
            Next
        End If
        ''get cat from agree brandiNclude
        Dim HasBigPS As Boolean = False, HasSmallPS As Boolean = False
        For i As Integer = 0 To Me.clsAgInclude.ViewBrand().Count - 1
            Dim ProdCat As String = ""
            Dim BrandName As String = Me.clsAgInclude.ViewBrand(i)("BRAND_NAME").ToString()
            If BrandName.Contains("POWERMAX") Then
                ProdCat = "ROUNDUP POWERMAX"
            ElseIf BrandName.Contains("TRANSORB") Then
                ProdCat = "ROUNDUP TRANSORB"
            ElseIf BrandName.Contains("ROUNDUP") Then
                ProdCat = "ROUNDUP BIOSORB"
            End If
            If ProdCat <> "" Then
                If Not listCat.Contains(ProdCat) Then
                    listCat.Add(ProdCat)
                End If
            End If
            Select Case BrandName
                Case "ROUNDUP POWERMAX 660 SL - 01", "ROUNDUP POWERMAX 660 SL - 04", "ROUNDUP SL-1", "ROUNDUP SL-200", "ROUNDUP SL-4", _
                    "ROUNDUP TRANSORB 440 SL - 01", "ROUNDUP TRANSORB 440 SL - 04", "ROUNDUP TRANSORB 440 SL - 200"
                    HasSmallPS = True
                Case "ROUNDUP POWERMAX 660 SL - 20", "ROUNDUP SL-20", "ROUNDUP TRANSORB 440 SL - 20"
                    HasBigPS = True
            End Select
        Next

        Dim ProList() As String = listCat.ToArray()
        Dim ColProdCat As Janus.Windows.GridEX.GridEXColumn = Me.GridEX2.RootTable.Columns("PRODUCT_CATEGORY")
        ColProdCat.EditType = Janus.Windows.GridEX.EditType.DropDownList
        ColProdCat.AutoComplete = True : ColProdCat.HasValueList = True
        Dim ValueListProdCat As Janus.Windows.GridEX.GridEXValueListItemCollection = ColProdCat.ValueList
        ValueListProdCat.PopulateValueList(ProList, "PRODUCT_CATEGORY")
        ColProdCat.EditTarget = Janus.Windows.GridEX.EditTarget.Value
        ColProdCat.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text

        'Pack Size group
        Dim tblPsGroup As New DataTable("PS_Category")
        tblPsGroup.Columns.Add("PSName", Type.GetType("System.String"))
        tblPsGroup.Columns.Add("PSValue", Type.GetType("System.String"))
        tblPsGroup.AcceptChanges()
        Dim row As DataRow = Nothing
        If HasBigPS Then
            row = tblPsGroup.NewRow()
            row.BeginEdit()
            row("PSName") = "BIG PACK SIZE"
            row("PSValue") = "B"
            row.EndEdit()
            tblPsGroup.Rows.Add(row)
        End If
        If HasSmallPS Then
            row = tblPsGroup.NewRow()
            row("PSName") = "SMALL PACK SIZE"
            row("PSValue") = "S"
            row.EndEdit()
            tblPsGroup.Rows.Add(row)
        End If
        tblPsGroup.AcceptChanges()

        Dim ColCat As Janus.Windows.GridEX.GridEXColumn = Me.GridEX2.RootTable.Columns("PS_CATEGORY")
        ColCat.EditType = Janus.Windows.GridEX.EditType.DropDownList
        ColCat.AutoComplete = True : ColCat.HasValueList = True
        Dim ValueListCat As Janus.Windows.GridEX.GridEXValueListItemCollection = ColCat.ValueList
        ValueListCat.PopulateValueList(tblPsGroup.DefaultView, "PSValue", "PSName")
        ColCat.EditTarget = Janus.Windows.GridEX.EditTarget.Value
        ColCat.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text



    End Sub

    Private Sub BindGridEX(ByVal dtView As DataView, ByRef rowFilter As Object)
        'Dim rw As String = "END_DATE > #" + NufarmBussinesRules.SharedClass.ServerDate.Month.ToString() + "/" + NufarmBussinesRules.SharedClass.ServerDate.Day.ToString() + _
        '"/" + NufarmBussinesRules.SharedClass.ServerDate.Year.ToString() + "#"
        If dtView Is Nothing Then
            Me.GridEX1.SetDataBinding(Nothing, "")
            Return
        Else
            dtView.RowFilter = rowFilter
            Me.MaySelectGrid = CanSelectGridEx.CanNot
            Me.GridEX1.SetDataBinding(dtView, "")
            Me.GridEX1.RetrieveStructure()
            Me.MaySelectGrid = CanSelectGridEx.Can
        End If
        'langsung format grid bila ada data yang mesti di format'
        For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If Item.Type Is Type.GetType("System.DateTime") Then
                Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                Item.FormatString = "dd MMMM yyyy"
                Item.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            ElseIf (Item.Type Is Type.GetType("System.Decimal")) Or (Item.Type Is Type.GetType("System.Double")) _
               Or (Item.Type Is Type.GetType("System.Single")) Then
                'Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                'If Item.DataMember = "TARGET_Q1" Or Item.DataMember = "TARGET_Q2" Or Item.DataMember = "TARGET_Q3" Then _
                'Or Item.DataMember = "TARGET_Q4" Or Item.DataMember = "TARGET_S1" Or Item.DataMember = "TARGET_S2" Or _
                '    Item.DataMember = "TARGET_YEAR" Then
                'End If
                Item.FormatString = "#,##0.000"
                Item.TotalFormatString = "#,##0.000"
                Item.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            Else
                Item.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo

            End If
            If Item.DataMember.Contains("ID") Then
                Item.Visible = False
            End If
            If Item.DataMember.Contains("AGREEMENT_NO") Or Item.DataMember.Contains("AGREEMENT_DESC") Or Item.DataMember.Contains("QS_TREATMENT") Or Item.DataMember.Contains("COMBINED_BRAND") Then
                Item.Visible = False
            End If
        Next
        'untuk brandInclude
        'isi item filter datagrid
        Me.GridEX1.RootTable.Columns(0).Visible = False
        Me.FillFilterColumn()
        With Me.GridEX1.RootTable
            .Columns("DISTRIBUTOR_NAME").Width = 180
            .Columns("AGREEMENT_NO").Width = 180
            .Columns("AGREEMENT_DESC").Width = 230
            .Columns("BRAND_NAME").Width = 160
            .Columns("ID").Width = 150
            .Columns("COMBINED_BRAND").Width = 150
        End With
        Select Case Me.QS_FLAG
            Case "Q"
                For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                    If Item.Key.Contains("TARGET_S") Or Item.Key.Contains("TARGET_FMP") Or Item.Key.Contains("TARGET_YEAR") Then
                        Item.Visible = False
                        'ElseIf Item.DataMember = "TARGET_S2" Then
                        '    Item.Visible = False
                    ElseIf Item.DataMember = "BRAND_ID" Then
                        Item.Visible = False
                    End If
                    'If Item.DataMember.Contains("FMP") Or Item.DataMember.Contains("PL") Then
                    '    Item.Visible = False
                    'End If
                Next
            Case "S"
                For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                    If Item.Key.Contains("TARGET_Q") Or Item.Key.Contains("TARGET_FMP") Then
                        Item.Visible = False
                        'ElseIf Item.DataMember = "TARGET_Q2" Then
                        '    Item.Visible = False
                        'ElseIf Item.DataMember = "TARGET_Q3" Then
                        '    Item.Visible = False
                        'ElseIf Item.DataMember = "TARGET_Q4" Then
                        'Item.Visible = False
                    ElseIf Item.DataMember = "BRAND_ID" Then
                        Item.Visible = False
                    End If
                    'If Item.DataMember.Contains("FM") Or Item.DataMember.Contains("PL") Then
                    '    Item.Visible = False
                    'End If
                Next
            Case "F"
                For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                    If Item.Key.Contains("TARGET_Q") Or Item.Key.Contains("TARGET_S") Or Item.Key.Contains("TARGET_YEAR") Then
                        Item.Visible = False
                    ElseIf Item.DataMember = "BRAND_ID" Then
                        Item.Visible = False
                    End If
                    'If Item.DataMember = "TARGET_S1" Then
                    '    Item.Visible = False
                    'ElseIf Item.DataMember = "TARGET_S2" Then
                    '    Item.Visible = False
                    '    If Item.DataMember.Contains("FM") Or Item.DataMember.Contains("PL") Then
                    '        Item.Visible = False
                    '    End If
                    'End If
                Next
        End Select
        'bila data yang diisi brandname hidekan item target_flag yang bukan berdasarkan value mcb
        'hide kan brand_idnya juga

        Me.FilterEditor1.SourceControl = Me.GridEX1
        Me.GridEXExporter1.GridEX = Me.GridEX1
        Me.GridEXPrintDocument1.GridEX = Me.GridEX1
        Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1


    End Sub

    Private Sub FillFilterColumn()
        For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If (Item.Type Is Type.GetType("System.Decimal")) Or (Item.Type Is Type.GetType("System.Double")) Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            End If
            If Item.Type Is Type.GetType("System.String") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.Int16") Then
                Me.GridEX1.RootTable.Columns(Item.Index).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.GridEX1.RootTable.Columns(Item.Index).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.Int32") Then
                Me.GridEX1.RootTable.Columns(Item.Index).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.GridEX1.RootTable.Columns(Item.Index).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.Decimal") Then
                Me.GridEX1.RootTable.Columns(Item.Index).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.GridEX1.RootTable.Columns(Item.Index).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.Int64") Then
                Me.GridEX1.RootTable.Columns(Item.Index).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.GridEX1.RootTable.Columns(Item.Index).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.Boolean") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
            ElseIf Item.Type Is Type.GetType("System.String") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.DateTime") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.DropDownList
            End If
        Next
    End Sub

    Private Sub BindGrid_1(ByVal grid As Janus.Windows.GridEX.GridEX, ByVal dtView As DataView)
        grid.DataSource = dtView
        If IsNothing(dtView) Then : Return : End If : grid.RetrieveStructure()
        If grid.Name Is Me.grdunAddedBrandPack Then
            If grid.RecordCount > 0 Then
                grid.RootTable.Columns("BRANDPACK_ID").ShowRowSelector = True
            End If
        End If
        For Each col As Janus.Windows.GridEX.GridEXColumn In grid.RootTable.Columns
            col.AutoSize()
        Next

    End Sub

    Private Sub BindGrid(ByVal dtView As DataView, ByVal rowFilter As String)
        If dtView Is Nothing Then
            Me.DataGrid1.TableStyles.Clear()
            Me.DataGrid1.DataSource = Nothing
            Me.DataGrid1.Refresh()
            Return
        End If
        dtView.RowFilter = rowFilter
        Me.DataGrid1.DataSource = dtView
        Me.DataGrid1.AllowSorting = True
        dtView.Sort = "BRAND_NAME"
        Me.DataGrid1.TableStyles.Clear()
        Dim grdTablestyle As New DataGridTableStyle()
        grdTablestyle.SelectionBackColor = Color.Orange
        grdTablestyle.HeaderBackColor = Color.FromArgb(194, 217, 247)
        grdTablestyle.GridLineColor = Color.FromArgb(194, 217, 247)
        grdTablestyle.HeaderForeColor = Color.DimGray
        grdTablestyle.ForeColor = Color.DimGray
        grdTablestyle.HeaderFont = New System.Drawing.Font _
            ("Verdana", 8.25F, System.Drawing.FontStyle.Bold, _
             System.Drawing.GraphicsUnit.Point, CType(0, System.Byte))
        grdTablestyle.MappingName = dtView.Table.TableName
        Dim grdColStyle1 As New DataGridTextBoxColumn         'Dim grdlst As New DataGridLineStyle
        With grdColStyle1
            .MappingName = "BRAND_ID"
            .HeaderText = "BRAND_ID"
            .Alignment = HorizontalAlignment.Left
            .Width = 80
            '.TrueValue = "True"
            '.FalseValue = "False"
        End With
        Dim grdColStyle2 As New DataGridTextBoxColumn
        With grdColStyle2
            .MappingName = "BRAND_NAME"
            .HeaderText = "BRAND_NAME"
            .Alignment = HorizontalAlignment.Left
            .Width = 230
        End With
        grdTablestyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyle1, grdColStyle2})
        Me.DataGrid1.TableStyles.Add(grdTablestyle)
        Me.DataGrid1.Refresh()
    End Sub

    Private Sub BindMulticolumnCombo(ByVal rowFilter As String)
        'Me.SFM = StateFillingCombo.Filling
        Me.clsAgInclude.ViewAgreement.RowFilter = rowFilter
        'Me.MultiColumnCombo1.SetDataBinding(Me.clsAgInclude.ViewAgreement, "")
        Me.clsAgInclude.ViewAgreement.RowFilter = rowFilter
        Me.MultiColumnCombo1.DataSource = Me.clsAgInclude.ViewAgreement
        Me.MultiColumnCombo1.DisplayMember = "AGREEMENT_NO"
        Me.MultiColumnCombo1.ValueMember = "AGREEMENT_NO"
        Me.MultiColumnCombo1.DropDownList.RetrieveStructure()
        Me.MultiColumnCombo1.DroppedDown = True
        Me.MultiColumnCombo1.DropDownList.Columns("QS_TREATMENT_FLAG").Visible = False
        Me.MultiColumnCombo1.DropDownList.Columns("AGREEMENT_DESC").Visible = False
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.MultiColumnCombo1.DropDownList.Columns
            col.AutoSize()
        Next
        Me.MultiColumnCombo1.DroppedDown = False
        'Me.SFM = StateFillingCombo.HasFilled
    End Sub

    Private Sub AddConditionalFormating()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.GridEX1.RootTable.Columns("END_DATE"), Janus.Windows.GridEX.ConditionOperator.LessThan, NufarmBussinesRules.SharedClass.ServerDate())
        fc.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
        fc.FormatStyle.ForeColor = SystemColors.GrayText
        GridEX1.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub InflateData(ByVal tbName As Janus.Windows.UI.Tab.UITabPage)
        Select Case tbName.Name
            Case "tbBrandInclude"

            Case "tbBrandPackInclude"

            Case "tbOAHistory"

            Case "tbDiscGenerator"

        End Select
    End Sub

    Private Sub FillComboSecondBrand()
        For i As Integer = 1 To Me.GridEX1.RecordCount
            If (Not IsNothing(Me.GridEX1.GetValue("COMBINED_BRAND"))) Or (Not (Me.GridEX1.GetValue("COMBINED_BRAND") Is DBNull.Value)) Then
            Else
                Me.cmbSeconBrand.Items.Add(Me.GridEX1.GetValue("BRAND_NAME"))
            End If
        Next
    End Sub

#End Region

#Region " Function "

    Private Sub GetAccesChangeAgreement()
        Try
            If Not CMain.IsSystemAdministrator Then
                If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.ChangeAgreement = True Or NufarmBussinesRules.User.UserLogin.IsAdmin Then
                    If Me.QS_FLAG = "S" Then
                        Me.EnabledTextBox(Me.grpSemesterly)
                        Me.UnabledTextBox(Me.grpQuarterly)
                        Me.UnabledTextBoxFMP()
                    ElseIf Me.QS_FLAG = "Q" Then
                        If Me.isTransitionTime Then
                            Me.EnabledTextBoxFMP()
                        Else
                            Me.UnabledTextBoxFMP()
                        End If
                        Me.EnabledTextBox(Me.grpQuarterly)
                        Me.UnabledTextBox(Me.grpSemesterly)
                    ElseIf Me.QS_FLAG = "F" Then
                        Me.EnabledTextBoxFMP()
                        Me.UnabledTextBox(Me.grpQuarterly)
                        Me.UnabledTextBox(Me.grpSemesterly)
                    End If
                    Me.txtGiven.Enabled = True
                    Me.SavingChanges1.btnSave.Enabled = True
                Else
                    Me.UnabledTextBox(Me.grpQuarterly)
                    Me.UnabledTextBox(Me.grpSemesterly)
                    Me.UnabledTextBoxFMP()
                    Me.txtGiven.Enabled = False
                    Me.SavingChanges1.btnSave.Enabled = False
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Function IsValid(ByVal tbActive As ActiveTab, ByVal ShowMessage As Boolean) As Boolean
        Dim Valid As Boolean = True
        If Me.MultiColumnCombo1.SelectedIndex = -1 Then
            'me.baseTooltip.Show(
            Me.baseTooltip.SetToolTip(Me.MultiColumnCombo1, "Agreement Number must be defined." & vbCrLf & "Please defined Agrement number !.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.MultiColumnCombo1), Me.MultiColumnCombo1)
            Me.MultiColumnCombo1.Focus()
            Return False
        End If
        Select Case tbActive
            'nanti lagi
            Case ActiveTab.BrandInclude
                If Me.txtGiven.Text = "" Then
                    If ShowMessage Then
                        Me.baseTooltip.SetToolTip(Me.txtGiven, "Given Persen is null." & vbCrLf & _
                        "in order to Evaluate discount BrandPack for distributor." & vbCrLf & "Please Defined Given %.")
                        Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtGiven), Me.txtGiven, 2000)
                        Me.txtGiven.Focus()
                    End If
                    Return False
                ElseIf Me.txtBrandName.Text = "" Then
                    If ShowMessage Then
                        Me.baseTooltip.SetToolTip(Me.txtBrandName, "Brand Name is null." & vbCrLf & "Please Defined Brand Name.")
                        Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtBrandName), Me.txtBrandName, 2000)
                        Me.txtBrandName.Focus()
                    End If
                    Return False
                End If
                Select Case Me.QS_FLAG
                    Case "Q"
                        For Each Item As Control In Me.grpQuarterly.Controls
                            If TypeOf (Item) Is TextBox Or TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                                If Item.Text = "" Then
                                    If ShowMessage Then
                                        Me.baseTooltip.SetToolTip(Item, "Target quarterly is Null." & vbCrLf & "In order to evaluate discount." & vbCrLf & "Target quarterly must be defined.")
                                        Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Item), Item, 2000)
                                        Item.Focus()
                                    End If
                                    Return False
                                End If
                            End If
                        Next
                        'transisi
                        If isTransitionTime Then
                            For Each Item As Control In Me.tbFMP.Controls
                                If TypeOf (Item) Is TextBox Or TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                                    If Item.Text = "" Then
                                        If ShowMessage Then
                                            Me.baseTooltip.SetToolTip(Item, "Target four months periode is Null." & vbCrLf & "In order to evaluate discount." & vbCrLf & "Target quarterly must be defined.")
                                            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Item), Item, 2000)
                                            Item.Focus()
                                        End If
                                        Return False
                                    End If
                                End If
                            Next
                        End If
                        'check FREE marketnya

                        ''UNCOMMENT THIS AFTER NEXT PERIODE
                        '=====================================================================
                        'If Me.txtQ1QTY.Value > 0 Then
                        '    If Me.txtFreeMarketQ1.Value <= 0 Then
                        '        Me.baseTooltip.Show("Please enter free market qty", Me.txtFreeMarketQ1, 2500) : Me.txtFreeMarketQ1.Focus() : Return False
                        '    End If
                        'End If
                        'If Me.txtQ2QTY.Value > 0 Then
                        '    If Me.txtFreeMarketQ2.Value <= 0 Then
                        '        Me.baseTooltip.Show("Please enter free market qty", Me.txtFreeMarketQ2, 2500) : Me.txtFreeMarketQ2.Focus() : Return False
                        '    End If
                        'End If
                        'If Me.txtQ3QTY.Value > 0 Then
                        '    If Me.txtFreeMarketQ3.Value <= 0 Then
                        '        Me.baseTooltip.Show("Please enter free market qty", Me.txtFreeMarketQ3, 2500) : Me.txtFreeMarketQ3.Focus() : Return False
                        '    End If
                        'End If
                        'If Me.txtQ4QTY.Value > 0 Then
                        '    If Me.txtFreeMarketQ4.Value <= 0 Then
                        '        Me.baseTooltip.Show("Please enter free market qty", Me.txtFreeMarketQ4, 2500) : Me.txtFreeMarketQ4.Focus() : Return False
                        '    End If
                        'End If
                        '================================================================================

                    Case "S"
                        For Each Item As Control In Me.grpSemesterly.Controls
                            If TypeOf (Item) Is TextBox Or TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                                If Item.Text = "" Then
                                    If ShowMessage Then
                                        Me.baseTooltip.SetToolTip(Item, "Target semesterly is Null." & vbCrLf & "In order to evaluate discount." & vbCrLf & "Target semesterly must be defined.")
                                        Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Item), Item, 2000)
                                        Item.Focus()
                                    End If
                                    Return False
                                End If
                            End If
                        Next
                        ''UNCOMMENT THIS AFTER NEXT PERIODE
                        '=====================================================================
                        'If Me.txtS1QTY.Value > 0 Then
                        '    If Me.txtFreeMarketS1.Value <= 0 Then
                        '        Me.baseTooltip.Show("Please enter free market qty", Me.txtFreeMarketS1, 2500) : Me.txtFreeMarketS1.Focus() : Return False
                        '    End If
                        'End If
                        'If Me.txtFreeMarketS2.Value > 0 Then
                        '    If Me.txtFreeMarketS2.Value <= 0 Then
                        '        Me.baseTooltip.Show("Please enter free market qty", Me.txtFreeMarketS2, 2500) : Me.txtFreeMarketS2.Focus() : Return False
                        '    End If
                        'End If
                        '============================================================================
                    Case "F"
                        For Each Item As Control In Me.tbFMP.Controls
                            If TypeOf (Item) Is TextBox Or TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                                If Item.Text = "" Then
                                    If ShowMessage Then
                                        Me.baseTooltip.SetToolTip(Item, "Target four months periode is Null." & vbCrLf & "In order to evaluate discount." & vbCrLf & "Target quarterly must be defined.")
                                        Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Item), Item, 2000)
                                        Item.Focus()
                                    End If
                                    Return False
                                End If
                            End If
                        Next
                End Select

            Case ActiveTab.CombinedBrand


            Case ActiveTab.OAHistory

        End Select

        Return Valid
    End Function

#End Region

#Region " Event "
    'Mgrid
#Region " FORM "

    Private Sub AgreementRelation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.MultiColumnCombo1.Text = ""
            'Me.BindGrid(Me.clsAgInclude.ViewBrand, "")
            Me.MultiColumnCombo1.SelectedIndex = -1
            Me.AcceptButton = Me.SavingChanges1.btnSave
            Me.CancelButton = Me.SavingChanges1.btnCLose
            Me.DataGrid1.CaptionVisible = False
            'Me.DataGrid2.CaptionVisible = False
            'Me.RefreshData1.BringToFront()
            Me.baseTooltip.ToolTipTitle = "Information"
            Me.sGrid = SelectedGrid.GridEx1
            Me.txtS1QTY.Text = "0"
            Me.txtS2QTY.Text = "0"
            Me.txtQ1QTY.Value = "0"
            Me.txtQ2QTY.Value = "0"
            Me.txtQ3QTY.Value = "0"
            Me.txtQ4QTY.Value = "0"
            Me.grpPotensi.Enabled = False
        Catch ex As Exception

        Finally
            Me.ReadAccecs()
            Me.Cursor = Cursors.Default
        End Try
        Me.SFM = StateFillingCombo.HasFilled
        Me.SFG = StateFillingGrid.HasFilled
        Me.Hload = HasLoad.Yes
    End Sub

    Private Sub AgreementRelation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.SFG = StateFillingGrid.Filling
            If Not IsNothing(Me.clsAgInclude) Then
                If Not IsNothing(Me.ds4MPeriode) Then
                    If ds4MPeriode.HasChanges Then
                        If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                            Me.SavingChanges1_btnSaveClick(Me.SavingChanges1.btnSave, New EventArgs())
                        End If
                    End If
                    Me.ds4MPeriode.Dispose()
                End If
                Me.clsAgInclude.Dispose(True)
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_AgreementRelation_FormClosed")
        Finally
            Me.Cursor = Cursors.Default
            RaiseEvent CloseThis()
        End Try
    End Sub

#End Region

#Region " BAR CONTROL "
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnShowFieldChooser"
                    Me.GridEX1.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
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
                Case "btnPageSettings"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
                Case "btnCustFilter"
                    Me.FilterEditor1.SourceControl = Me.GridEX1
                    Me.FilterEditor1.Visible = True
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.GridEX1.RemoveFilters()
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.GridEX1.RemoveFilters()
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
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
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#Region " DATAGRID "

#Region " DATAGRID WINDOWS "

    Private Sub DataGrid1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGrid1.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.DataGrid1.DataSource Is Nothing Then : Me.txtBrandName.Text = "" : Return : End If
            If Me.DataGrid1.CurrentRowIndex = -1 Then : Me.txtBrandName.Text = "" : Return : End If
            If (Me.MultiColumnCombo1.Text = "") Or (Me.MultiColumnCombo1.SelectedIndex = -1) Then
                Me.txtBrandName.Text = "" : Return
            End If
            Me.sGrid = SelectedGrid.DataGrid : Me.DataGrid1.Select(Me.DataGrid1.CurrentCell.RowNumber)
            Me.Brand_ID = Me.DataGrid1.Item(Me.DataGrid1.CurrentRowIndex, 0).ToString()
            Me.NewAgree_Brand_ID = Me.MultiColumnCombo1.Text.TrimStart().TrimStart.TrimEnd() + "" + Me.Brand_ID
            Me.clsAgInclude.GetItemBrandPackByBrandID(Me.DataGrid1.Item(Me.DataGrid1.CurrentRowIndex, 0).ToString())
            Me.BindGrid_1(Me.grdunAddedBrandPack, Me.clsAgInclude.ViewBrandPack())
            Me.grdunAddedBrandPack.RootTable.Columns("BRANDPACK_ID").ShowRowSelector = False
            'Me.grpIBPfromBrand.Text = "BrandPack from brand " & Me.DataGrid1.Item(Me.DataGrid1.CurrentRowIndex, 1)
            Me.txtBrandName.Text = Me.DataGrid1.Item(Me.DataGrid1.CurrentRowIndex, 1).ToString()
            Me.UnabledTextBox(Me.grpSemesterly) : Me.UnabledTextBox(Me.grpQuarterly)
            Me.txtGiven.Enabled = False : Me.ClearControl(Me.grpQuarterly) : Me.ClearControl(Me.grpSemesterly)
            Me.txtGiven.Text = "" : Me.BindGrid_1(Me.grdAddedBrandPack, Nothing)
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_DataGrid1_CurrentCellChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub dgvYearly_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvYearly.Enter
        Me.tbPeriodic_Enter(Me.tbPeriodic, New EventArgs())
    End Sub

    Private Sub dgvYearly_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvYearly.MouseClick
        Try
            If (Me.chkTypical.Checked = True) And (Me.chkTypical.Enabled = True) And (Me.QS_FLAG = "S" Or Me.QS_FLAG = "Q") Then
                Me.dgvYearly.Enabled = True
            Else
                Me.dgvYearly.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvYearly_UserDeletedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvYearly.UserDeletedRow
        Me.dgvYearly.CommitEdit(DataGridViewDataErrorContexts.RowDeletion)
    End Sub
    Private Sub dgvYearly_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvYearly.DataError
        Try
            Me.baseTooltip.Show("Please suply with valid value" & vbCrLf & "If you want to delete row " & vbCrLf & "Please press delete key on the keyboard !.", Me.dgvYearly, 2500)
            Me.dgvYearly.CancelEdit()
        Catch ex As Exception

        End Try

    End Sub
    'bila menambah data / mengedit
    Private Sub dgvYearly_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvYearly.CellEndEdit
        Try
            If Not (Me.dgvYearly.Item(2, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvYearly.Item(2, e.RowIndex).Value) > 200 Then
                    Me.ShowMessageInfo("The value exceeds from 200")
                    Me.dgvYearly.CancelEdit()
                    Return
                End If
            End If
            If Not (Me.dgvYearly.Item(3, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvYearly.Item(3, e.RowIndex).Value) > 100 Then
                    Me.ShowMessageInfo("The value exceeds from 100")
                    Me.dgvYearly.CancelEdit()
                    Return
                End If
            End If
            If Not (Me.dgvYearly.Item(2, e.RowIndex).Value Is Nothing) Or Not (Me.dgvYearly.Item(3, e.RowIndex).Value Is Nothing) Then
                If Me.Mode = SaveMode.Save Then
                    Me.dgvYearly.Item(1, e.RowIndex).Value = CObj(Me.NewAgree_Brand_ID)
                Else
                    Me.dgvYearly.Item(1, e.RowIndex).Value = CObj(Me.AGREE_BRAND_ID)
                End If

                'If Me.QS_FLAG = True Then
                Me.dgvYearly.Item(4, e.RowIndex).Value = CObj("Y")
                'ElseIf Me.rdbSemeterly.Checked = True Then
                'Me.dgvYearly.Item(4, e.RowIndex).Value = CObj("Y")
                'End If
                'Else
                '    Me.dgvYearly.Item(4, e.RowIndex).Value = DBNull.Value
                '    Me.dgvYearly.Item(1, e.RowIndex).Value = DBNull.Value
            End If
            If (Me.dgvYearly.Item(1, e.RowIndex).Value Is DBNull.Value) Or (Me.dgvYearly.Item(4, e.RowIndex).Value Is DBNull.Value) Then
                Me.ShowMessageInfo("No row's Been Edited" + vbCrLf & "Any Changed Rows will be discarded")
                Me.dgvYearly.RefreshEdit()
            Else
                Me.dgvYearly.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub dgvPeriodic_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Me.tbPeriodic_Enter(Me.tbPeriodic, New EventArgs())
    '    If (Me.chkTypical.Checked = True) And (Me.chkTypical.Enabled = True) Then
    '        Me.dgvPeriodic.Enabled = True
    '    Else
    '        Me.dgvPeriodic.Enabled = False
    '    End If
    'End Sub

    Private Sub dgvPeriodic_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvPeriodic.MouseClick
        If (Me.chkTypical.Checked = True) And (Me.chkTypical.Enabled = True) And (Me.QS_FLAG = "S" Or Me.QS_FLAG = "Q") Then
            Me.dgvPeriodic.Enabled = True
        Else
            Me.dgvPeriodic.Enabled = False
        End If
    End Sub

    Private Sub dgvPeriodic_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvPeriodic.UserDeletedRow
        Me.dgvPeriodic.CommitEdit(DataGridViewDataErrorContexts.RowDeletion)

    End Sub
    Private Sub dgvPeriodic_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvPeriodic.DataError
        Me.baseTooltip.Show("Please suply with valid value" & vbCrLf & "If you want to delete row " & vbCrLf & "Please press delete key on the keyboard !.", Me.dgvPeriodic, 2500)
        Me.dgvPeriodic.CancelEdit()
    End Sub
    'bila menambah data / mengedit
    Private Sub dgvPeriodic_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPeriodic.CellEndEdit
        Try

            If Not (Me.dgvPeriodic.Item(2, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvPeriodic.Item(2, e.RowIndex).Value) > 200 Then
                    Me.ShowMessageInfo("The value exceeds from 200")
                    Me.dgvPeriodic.CancelEdit()
                    Return
                End If
            End If
            If Not (Me.dgvPeriodic.Item(3, e.RowIndex).Value Is DBNull.Value) Then
                If CInt(Me.dgvPeriodic.Item(3, e.RowIndex).Value) > 100 Then
                    Me.ShowMessageInfo("The value exceeds from 100")
                    Me.dgvPeriodic.CancelEdit()
                    Return
                End If
            End If
            If e.ColumnIndex = 4 Then : Return : End If
            If Not (Me.dgvPeriodic.Item(2, e.RowIndex).Value Is Nothing) Or Not (Me.dgvPeriodic.Item(3, e.RowIndex).Value Is Nothing) Then
                If Me.Mode = SaveMode.Save Then
                    Me.dgvPeriodic.Item(1, e.RowIndex).Value = CObj(Me.NewAgree_Brand_ID)
                Else
                    Me.dgvPeriodic.Item(1, e.RowIndex).Value = CObj(Me.AGREE_BRAND_ID)
                End If
                If (Me.AgreementEndDate < New DateTime(2010, 9, 1)) Then
                    If Me.QS_FLAG = "Q" Then
                        Me.dgvPeriodic.Item(4, e.RowIndex).Value = CObj("Q")
                    ElseIf Me.QS_FLAG = "S" Then
                        Me.dgvPeriodic.Item(4, e.RowIndex).Value = CObj("S")
                    End If
                ElseIf (Me.dgvPeriodic.Item(4, e.RowIndex).Value Is DBNull.Value) Then
                    Me.dgvPeriodic.Item(4, e.RowIndex).Value = Me.QS_FLAG
                ElseIf Not IsDBNull(Me.dgvPeriodic.Item(4, e.RowIndex).Value) Then
                    If String.IsNullOrEmpty(Me.dgvPeriodic.Item(4, e.RowIndex).Value) Then
                        Me.dgvPeriodic.Item(4, e.RowIndex).Value = Me.QS_FLAG
                    End If
                End If
                'Else
                '    Me.dgvPeriodic.Item(4, e.RowIndex).Value = DBNull.Value
                'Me.dgvPeriodic.Item(1, e.RowIndex).Value = DBNull.Value
            End If

            If (Me.AgreementEndDate < New DateTime(2010, 9, 1)) Then
                If (Me.dgvPeriodic.Item(1, e.RowIndex).Value Is DBNull.Value) Or (Me.dgvPeriodic.Item(4, e.RowIndex).Value Is DBNull.Value) Then
                    Me.ShowMessageInfo("No row's Been Edited" + vbCrLf & "Any Changed Rows will be discarded")
                    Me.dgvPeriodic.RefreshEdit()
                Else
                    Me.dgvPeriodic.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If
            Else
                Me.dgvPeriodic.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
            'set column 0(agreement no) dengan agrement no dan qsy_flag dengan melihat rdbqs checkchanged nya
        Catch ex As InvalidCastException

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region " GRID EX "

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            If Me.Hload = HasLoad.NotYet Then : Return : End If
            If (Me.MultiColumnCombo1.SelectedIndex = -1) Or (Me.MultiColumnCombo1.Text = "") Then : Return : End If
            If Me.MaySelectGrid = CanSelectGridEx.CanNot Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.clearGivenProgressive()
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.txtQ1QTY.Enabled = False : Me.txtQ2QTY.Enabled = False
                Me.txtQ3QTY.Enabled = False : Me.txtQ4QTY.Enabled = False
                Me.txtGiven.Enabled = False : Me.txtS1QTY.Enabled = False
                Me.txtS2QTY.Enabled = False : Me.txtGiven.Value = 0
                Me.txtBrandName.Text = "" : Me.ClearControl(Me.grpSemesterly)
                Me.ClearControl(Me.grpQuarterly) : Me.ClearControl(Me.tbFMP) : Me.grpCombQ.Visible = False
                Me.grpComS.Visible = False : Me.grpComboFirstSecond.Visible = False
                Me.TreeView1.Visible = False : Me.grpTypeDiscount.Visible = False
                'reject changes
                If Not IsNothing(Me.clsAgInclude.getDsPeriod()) Then
                    Me.clsAgInclude.getDsPeriod().RejectChanges()
                End If
                'If Me.QS_FLAG = "Q" Then
                '    '===CLEAR DGV Quarter===============
                '    If Not IsNothing(Me.clsAgInclude.GetTableQuarterly()) Then

                '        Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableQuarterly()
                '    End If
                '    If Not IsNothing(Me.clsAgInclude.GetTableQuarterlyV()) Then

                '        Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableQuarterlyV()
                '    End If
                'Else
                '    '===CLEAR DGV semester===============
                '    If Not IsNothing(Me.clsAgInclude.GetTableSemesterly()) Then

                '        Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableSemesterly()
                '    End If
                '    If Not IsNothing(Me.clsAgInclude.GetTableSemesterlyV()) Then

                '        Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableSemesterlyV()
                '    End If
                'End If
                ''===CLEAR DGV Yearly===============
                'If Not IsNothing(Me.clsAgInclude.GetTableYearly()) Then

                '    Me.dgvYearly.DataSource = Me.clsAgInclude.GetTableYearly()
                'End If
                'If Not IsNothing(Me.clsAgInclude.GetTableYearlyV()) Then

                '    Me.dgvYearly.DataSource = Me.clsAgInclude.GetTableYearlyV()
                'End If

                Me.BindGrid_1(Me.grdunAddedBrandPack, Nothing) : Me.BindGrid_1(Me.grdAddedBrandPack, Nothing)
                Me.chkTypical.Checked = False
                Return
            End If
            Me.SFG = StateFillingGrid.Filling : Me.Mode = SaveMode.Update
            Me.DPrevDisc = New DomPrevousDisc()
            Me.Brand_IDHide = Me.GridEX1.GetValue("BRAND_ID") : Me.AGREE_BRAND_ID = Me.GridEX1.GetValue("ID").ToString()
            'prosedure untuk mengecek reference given story di rubah, karena sudah tidak di pakai lagi,untuk given mungkin masih perlu
            Dim hasRefGivStory As Boolean = Me.clsAgInclude.HasReferencedGivenHistory(Me.AGREE_BRAND_ID, False)
            Me.txtGiven.Value = Convert.ToDecimal(Me.GridEX1.GetValue("GIVEN%"))
            Me.txtGiven.Enabled = (hasRefGivStory = False)
            Dim HasGeneratedDisc = False
            Select Case Me.TabAcktive
                Case ActiveTab.BrandInclude
                    'HasGeneratedDiscountFMP
                    Select Case Me.QS_FLAG
                        Case "Q"
                            HasGeneratedDisc = Me.clsAgInclude.HasgeneratedDiscount(Me.AGREE_BRAND_ID, Me.QS_FLAG)
                            If HasGeneratedDisc Then
                                If Not CMain.IsSystemAdministrator Then
                                    Me.UnabledTextBox(Me.grpQuarterly)
                                    'transisi
                                    For Each Item As Control In Me.tbFMP.Controls
                                        If TypeOf (Item) Is TextBox Then
                                            Item.Enabled = False
                                            Item.BackColor = Color.White
                                        ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                                            Item.Enabled = False
                                        End If
                                    Next
                                End If
                            Else
                                Me.EnabledTextBox(Me.grpQuarterly)
                            End If
                            Me.ClearControl(Me.grpSemesterly)
                            Me.UnabledTextBox(Me.grpSemesterly)
                            Me.txtQ1QTY.Value = Me.GridEX1.GetValue("TARGET_Q1")
                            Me.txtQ2QTY.Value = Me.GridEX1.GetValue("TARGET_Q2")
                            Me.txtQ3QTY.Value = Me.GridEX1.GetValue("TARGET_Q3")
                            Me.txtQ4QTY.Value = Me.GridEX1.GetValue("TARGET_Q4")

                            'FM/PL
                            Me.txtFreeMarketQ1.Value = Me.GridEX1.GetValue("TARGET_Q1_FM")
                            Me.txtFreeMarketQ2.Value = Me.GridEX1.GetValue("TARGET_Q2_FM")
                            Me.txtFreeMarketQ3.Value = Me.GridEX1.GetValue("TARGET_Q3_FM")
                            Me.txtFreeMarketQ4.Value = Me.GridEX1.GetValue("TARGET_Q4_FM")
                            Me.txtPlQ1.Value = Me.GridEX1.GetValue("TARGET_Q1_PL")
                            Me.txtPlQ2.Value = Me.GridEX1.GetValue("TARGET_Q2_PL")
                            Me.txtPlQ3.Value = Me.GridEX1.GetValue("TARGET_Q3_PL")
                            Me.txtPlQ3.Value = Me.GridEX1.GetValue("TARGET_Q3_PL")
                            Me.tbPeriode.Text = "Quarterly Discount"
                            If Me.isTransitionTime Then
                                'transisi
                                For Each Item As Control In Me.tbFMP.Controls
                                    If TypeOf (Item) Is TextBox Then
                                        Item.Enabled = True
                                        Item.BackColor = Color.White
                                    ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                                        Item.Enabled = True
                                    End If
                                Next
                                Me.ClearControl(Me.tbFMP)
                                Me.txtFMP1.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP1"))
                                Me.txtFMP2.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP2"))
                                Me.txtFMP3.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP3"))

                                Me.txtFMPFM1.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_FM1"))
                                Me.txtFMPFM2.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_FM2"))
                                Me.txtFMPFM3.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_FM3"))

                                Me.txtFMPPL1.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_PL1"))
                                Me.txtFMPPL2.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_PL2"))
                                Me.txtFMPPL3.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_PL3"))
                                If Me.clsAgInclude.HasGeneratedDiscountFMP(Me.AGREE_BRAND_ID, "F1", False) Then
                                    Me.txtFMP1.Enabled = False
                                    Me.txtFMPFM1.Enabled = False
                                    Me.txtFMPPL1.Enabled = False
                                End If
                                If Me.clsAgInclude.HasGeneratedDiscountFMP(Me.AGREE_BRAND_ID, "F2", False) Then
                                    Me.txtFMP2.Enabled = False
                                    Me.txtFMPFM2.Enabled = False
                                    Me.txtFMPPL2.Enabled = False
                                End If
                                If Me.clsAgInclude.HasGeneratedDiscountFMP(Me.AGREE_BRAND_ID, "F3", False) Then
                                    Me.txtFMP3.Enabled = False
                                    Me.txtFMPFM3.Enabled = False
                                    Me.txtFMPPL3.Enabled = False
                                End If
                            End If

                        Case "S"
                            'tahun 2019-2020 discount tidak di berikan bersama PO maka prosedure mengecek hanya sudah di generate atau belum saja
                            'tidak harus mengecek apakah sudah di ambil oleh PO atau belum
                            'HasGeneratedDisc = Me.clsAgInclude.HasgeneratedDiscount(Me.AGREE_BRAND_ID, Me.QS_FLAG)
                            Me.txtS1QTY.Enabled = True : Me.txtS2QTY.Enabled = True
                            If HasGeneratedDisc Then
                                If Not CMain.IsSystemAdministrator Then
                                    Me.UnabledTextBox(Me.grpSemesterly)
                                End If
                            Else
                                Me.EnabledTextBox(Me.grpSemesterly)
                            End If
                            Me.ClearControl(Me.grpQuarterly) : Me.UnabledTextBox(Me.grpQuarterly)

                            Me.ClearControl(Me.tbFMP)
                            For Each Item As Control In Me.tbFMP.Controls
                                If TypeOf (Item) Is TextBox Then
                                    Item.Enabled = False
                                    Item.BackColor = Color.White
                                ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                                    Item.Enabled = False
                                End If
                            Next
                            Me.txtS1QTY.Text = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_S1"))
                            Me.txtS2QTY.Text = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_S2"))
                            Me.txtFreeMarketS1.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_S1_FM"))
                            Me.txtFreeMarketS2.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_S2_FM"))
                            Me.txtPlS1.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_S1_PL"))
                            Me.txtPlS2.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_S2_PL"))
                            Me.tbPeriode.Text = "Semesterly Discount"
                        Case "F"
                            For Each Item As Control In Me.tbFMP.Controls
                                If TypeOf (Item) Is TextBox Then
                                    Item.Enabled = True
                                    Item.BackColor = Color.White
                                ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                                    Item.Enabled = True
                                End If
                            Next
                            Me.ClearControl(Me.tbFMP)
                            Me.txtFMP1.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP1"))
                            Me.txtFMP2.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP2"))
                            Me.txtFMP3.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP3"))

                            Me.txtFMPFM1.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_FM1"))
                            Me.txtFMPFM2.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_FM2"))
                            Me.txtFMPFM3.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_FM3"))

                            Me.txtFMPPL1.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_PL1"))
                            Me.txtFMPPL2.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_PL2"))
                            Me.txtFMPPL3.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP_PL3"))
                            If Me.clsAgInclude.HasGeneratedDiscountFMP(Me.AGREE_BRAND_ID, "F1", False) Then
                                Me.txtFMP1.Enabled = False
                                Me.txtFMPFM1.Enabled = False
                                Me.txtFMPPL1.Enabled = False
                            End If
                            If Me.clsAgInclude.HasGeneratedDiscountFMP(Me.AGREE_BRAND_ID, "F2", False) Then
                                Me.txtFMP2.Enabled = False
                                Me.txtFMPFM2.Enabled = False
                                Me.txtFMPPL2.Enabled = False
                            End If
                            If Me.clsAgInclude.HasGeneratedDiscountFMP(Me.AGREE_BRAND_ID, "F3", False) Then
                                Me.txtFMP3.Enabled = False
                                Me.txtFMPFM3.Enabled = False
                                Me.txtFMPPL3.Enabled = False
                            End If
                    End Select
                    Me.txtBrandName.Text = Me.GridEX1.GetValue("BRAND_NAME").ToString()
                    Dim Index As Integer
                    Index = Me.clsAgInclude.ViewFilterBrand.Find(CObj(Me.Brand_IDHide))
                    If Index <> -1 Then
                        Me.Mode = SaveMode.Save : Me.GridEX1.DataSource = Nothing
                        Me.grpTypeDiscount.Visible = False
                    Else
                        Me.Mode = SaveMode.Update : Me.grpTypeDiscount.Visible = True
                        'Me.clsAgInclude.GetItemBrandPackByBrandName(Me.Brand_IDHide, Me.MultiColumnCombo1.Text, Me.lstItemBrandPack)
                        Me.clsAgInclude.GetItemBrandPackByBrandName(Me.Brand_IDHide, Me.MultiColumnCombo1.Value.ToString(), False)
                        'Me.grpIBPfromBrand.Text = "BrandPack Not Included"
                        Me.BindGrid_1(Me.grdunAddedBrandPack, Me.clsAgInclude.ViewBrandPack())
                        If Me.grdunAddedBrandPack.RecordCount > 0 Then
                            Me.DataToolStripMenuItem.Enabled = True
                            Me.grdunAddedBrandPack.RootTable.Columns("BRANDPACK_ID").ShowRowSelector = True
                            For Each item As Janus.Windows.GridEX.GridEXColumn In Me.grdunAddedBrandPack.RootTable.Columns
                                item.AutoSize()
                            Next
                        Else
                            Me.DataToolStripMenuItem.Enabled = False
                        End If
                        Me.IsEnabledEditgrid = True
                        If Me.QS_FLAG = "F" Then
                            Me.chkTypical.Checked = True : Me.chkTypical.Enabled = True
                            Me.dgvPeriodic.Enabled = False : Me.dgvYearly.Enabled = False
                            Me.dgvPeriodicVal.Enabled = False : Me.dgvYearlyVal.Enabled = False
                        Else
                            If Me.clsAgInclude.IsCustomProgressiveDiscount(Me.AGREE_BRAND_ID, False) Then ''sudah di input 
                                Me.chkTypical.Checked = True : Me.chkTypical.Enabled = True
                                Me.dgvPeriodic.Enabled = True : Me.dgvYearly.Enabled = True
                                Me.dgvPeriodicVal.Enabled = True : Me.dgvYearlyVal.Enabled = True
                                Me.clsAgInclude.GetDsSetPeriod(Me.AGREE_BRAND_ID, Me.QS_FLAG)
                                Me.grdunAddedBrandPack.Enabled = True
                            Else
                                'check apakah ada combined brand dengan status progressive discountnya custom
                                If Not IsDBNull(GridEX1.GetValue("COMBINED_BRAND")) Then
                                    'check ke database apakah combinedbrand ini ada di agree_prog_discount
                                    If (Me.clsAgInclude.IsCustomProgressiveDiscount(Me.GridEX1.GetValue("COMBINED_BRAND").ToString(), False)) Then
                                        Me.chkTypical.Checked = True
                                        ''matikan control
                                        Me.chkTypical.Enabled = False
                                        Me.dgvPeriodic.Enabled = False : Me.dgvPeriodicVal.Enabled = False
                                        Me.dgvYearly.Enabled = False : Me.dgvYearlyVal.Enabled = False
                                        Me.grdunAddedBrandPack.Enabled = False
                                        Me.clsAgInclude.GetDsSetPeriod(Me.GridEX1.GetValue("COMBINED_BRAND").ToString(), Me.QS_FLAG)
                                        Me.IsEnabledEditgrid = False
                                    Else
                                        Me.chkTypical.Enabled = True : Me.chkTypical.Checked = False
                                        Me.dgvPeriodic.Enabled = True : Me.dgvPeriodicVal.Enabled = True
                                        Me.dgvYearly.Enabled = True : Me.dgvYearlyVal.Enabled = True
                                        Me.grdunAddedBrandPack.Enabled = True
                                        Me.clsAgInclude.GetDsSetPeriod(Me.AGREE_BRAND_ID, Me.QS_FLAG)
                                        'check apakah data sudah di generate/belum 
                                    End If
                                Else
                                    Me.dgvPeriodic.Enabled = True : Me.dgvPeriodicVal.Enabled = True
                                    Me.dgvYearly.Enabled = True : Me.dgvYearlyVal.Enabled = True
                                    Me.chkTypical.Enabled = True : Me.chkTypical.Checked = False
                                    Me.grdunAddedBrandPack.Enabled = True
                                    Me.clsAgInclude.GetDsSetPeriod(Me.AGREE_BRAND_ID, Me.QS_FLAG)
                                End If
                            End If
                            If (Me.QS_FLAG = "Q") Then
                                Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableQuarterly()
                                Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableQuarterlyV()
                                'transisi
                                'Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableFMP()
                                'Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableFMPV()
                            ElseIf Me.QS_FLAG = "S" Then
                                Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableSemesterly()
                                Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableSemesterlyV()
                                'ElseIf Me.QS_FLAG = "F" Then
                                '    Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableFMP()
                                '    Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableFMPV()
                            End If
                            Me.dgvYearly.DataSource = Me.clsAgInclude.GetTableYearly()
                            Me.dgvYearlyVal.DataSource = Me.clsAgInclude.GetTableYearlyV()
                            Me.dgvPeriodic.Columns(0).Visible = False
                            Me.dgvPeriodicVal.Columns(0).Visible = False
                            Me.dgvYearly.Columns(0).Visible = False
                            Me.dgvYearlyVal.Columns(0).Visible = False
                            'JIKA END_DATE AGREEMENT <= 31 AGUSTUS 2009 HIDE COLUMN
                            If AgreementEndDate < New DateTime(2010, 9, 1) Then
                                Me.dgvPeriodic.Columns("QSY_DISC_FLAG").Visible = False
                            Else
                                Me.dgvPeriodic.Columns("QSY_DISC_FLAG").Visible = True
                            End If
                            'get Given Progressive
                            Dim tblProgressive As DataTable = Me.clsAgInclude.getGivenProgressive(Me.AGREE_BRAND_ID, True)
                            If tblProgressive.Rows.Count > 0 Then
                                Me.txtPBQ3.Value = Convert.ToDecimal(tblProgressive.Rows(0)("PBQ3"))
                                Me.txtPBQ4.Value = Convert.ToDecimal(tblProgressive.Rows(0)("PBQ4"))
                                Me.txtPBS2.Value = Convert.ToDecimal(tblProgressive.Rows(0)("PBS2"))
                                Me.txtCPQ1.Value = Convert.ToDecimal(tblProgressive.Rows(0)("CPQ1"))
                                Me.txtCPQ2.Value = Convert.ToDecimal(tblProgressive.Rows(0)("CPQ2"))
                                Me.txtCPQ3.Value = Convert.ToDecimal(tblProgressive.Rows(0)("CPQ3"))
                                Me.txtCPS1.Value = Convert.ToDecimal(tblProgressive.Rows(0)("CPS1"))
                                Me.txtPBYear.Value = Convert.ToDecimal(tblProgressive.Rows(0)("PBY"))
                                'Public PBF2
                                'Public PBF3
                                'Public CPF1
                                'Public CPF2
                                Me.txtPBF2.Value = Convert.ToDecimal(tblProgressive.Rows(0)("PBF2"))
                                Me.txtPBF3.Value = Convert.ToDecimal(tblProgressive.Rows(0)("PBF3"))
                                Me.txtCPF1.Value = Convert.ToDecimal(tblProgressive.Rows(0)("CPF1"))
                                Me.txtCPF2.Value = Convert.ToDecimal(tblProgressive.Rows(0)("CPF2"))
                                With Me.DPrevDisc
                                    .PBQ3 = Me.txtPBQ3.Value
                                    .PBQ4 = Me.txtPBQ4.Value
                                    .PBS2 = Me.txtPBS2.Value
                                    .CPQ1 = Me.txtCPQ1.Value
                                    .CPQ2 = Me.txtCPQ2.Value
                                    .CPQ3 = Me.txtCPQ3.Value
                                    .CPS1 = Me.txtCPS1.Value
                                    .PBF2 = Me.txtPBF2.Value
                                    .PBF3 = Me.txtPBF3.Value
                                    .CPF1 = Me.txtCPF1.Value
                                    .CPF2 = Me.txtCPF2.Value
                                End With
                            End If
                        End If
                        Dim DV As DataView = Me.clsAgInclude.GetItemBrandPackIncludedByBrandID(Me.Brand_IDHide, Me.MultiColumnCombo1.Value.ToString(), False)
                        Me.BindGrid_1(Me.grdAddedBrandPack, DV)
                        If Not IsNothing(Me.grdAddedBrandPack.DataSource) Then
                            If Me.grdAddedBrandPack.RecordCount > 0 Then
                                Me.GetAccesChangeAgreement()
                            Else
                                Me.SavingChanges1.btnSave.Enabled = True
                            End If
                        Else
                            Me.SavingChanges1.btnSave.Enabled = True
                        End If
                        Me.FormatGridDiscount(Me.dgvPeriodic)
                        Me.FormatGridDiscount(Me.dgvPeriodicVal)
                        Me.FormatGridDiscount(Me.dgvYearly)
                        Me.FormatGridDiscount(Me.dgvYearlyVal)
                    End If
                    Me.grpPotensi.Enabled = True

                    'Me.baseTooltip.SetToolTip(Me.lstItemBrandPack, "Double Click one of the Item(s) on this ListBox" & vbCrLf & "If you intend to Insert Item.")
                Case ActiveTab.CombinedBrand
                    Me.TreeView1.Visible = True
                    Me.AGREE_BRAND_ID = Me.GridEX1.GetValue("ID").ToString()
                    Me.lblFirstBrand.Text = Me.GridEX1.GetValue("BRAND_NAME").ToString()

                    'check apakah data ada di table agree_prog_disc
                    'jika tidak ada tampilkan di combofirstsecond yang combined null 
                    Me.HFC = HasFilledComboFirstSecond.Filling 'untuk mematikan event combo selection change pada saat item combo di fill
                    If (Me.clsAgInclude.IsCustomProgressiveDiscount(Me.AGREE_BRAND_ID, False) = True) Then
                        Me.clsAgInclude.FillCommBoFirstsSecond(Me.MultiColumnCombo1.Value.ToString(), True, Me.cmbSeconBrand)
                    Else
                        Me.clsAgInclude.FillCommBoFirstsSecond(Me.MultiColumnCombo1.Value.ToString(), False, Me.cmbSeconBrand)
                    End If
                    Me.grpComboFirstSecond.Visible = True
                    ' Me.clsAgInclude.FillCommBoFirstsSecond(Me.MultiColumnCombo1.Text, Me.cmbSeconBrand)
                    Me.HFC = HasFilledComboFirstSecond.Done
                    Select Case Me.QS_FLAG
                        Case "Q"
                            'transisi
                            Me.grpComS.Visible = False : Me.grpCombQ.Visible = True : Me.grpCombF.Visible = False : Me.ClearControl(Me.grpCombQ)
                            Me.txtComb1Q1.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_Q1"))
                            Me.txtComb1Q2.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_Q2"))
                            Me.txtComb1Q3.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_Q3"))
                            Me.txtComb1Q4.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_Q4"))
                            If Not (Me.GridEX1.GetValue("COMBINED_BRAND") Is DBNull.Value) Then
                                Me.clsAgInclude.CreatViewCombinedBrand(Me.MultiColumnCombo1.Text, Me.TreeView1, False)
                                Me.COMB_BRAND_ID = Me.GridEX1.GetValue("COMBINED_BRAND").ToString()
                                Me.clsAgInclude.GetTabelQuantityCombined(Me.COMB_BRAND_ID, Me.MultiColumnCombo1.Text, "Q")
                                If Me.clsAgInclude.GetTblQuantityCombined().Rows.Count > 0 Then
                                    Me.txtComb2Q1.Value = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("TARGET_Q1")
                                    Me.txtComb2Q2.Value = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("TARGET_Q2")
                                    Me.txtComb2Q3.Value = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("TARGET_Q3")
                                    Me.txtComb2Q4.Value = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("TARGET_Q4")
                                    Me.cmbSeconBrand.Text = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("BRAND_NAME")
                                    Me.cmbSeconBrand.Enabled = False
                                Else
                                    Me.txtComb2Q1.Value = 0 : Me.txtComb2Q2.Value = 0 : Me.txtComb2Q3.Value = 0 : Me.txtComb2Q4.Value = 0
                                    Me.cmbSeconBrand.SelectedIndex = -1 : Me.cmbSeconBrand.Text = "" : Me.cmbSeconBrand.Enabled = True
                                End If
                            Else
                                Me.txtComb2Q1.Value = 0 : Me.txtComb2Q2.Value = 0 : Me.txtComb2Q3.Value = 0
                                Me.txtComb2Q4.Value = 0 : Me.cmbSeconBrand.SelectedIndex = -1 : Me.cmbSeconBrand.Text = ""
                                Me.cmbSeconBrand.Enabled = True : Me.TreeView1.Nodes.Clear()
                            End If
                            Me.txtTotalComb1Q1.Value = Me.txtComb1Q1.Value + Me.txtComb2Q1.Value
                            Me.txtTotalComb2Q2.Value = Me.txtComb1Q2.Value + Me.txtComb2Q2.Value
                            Me.txtTotalComb3Q3.Value = Me.txtComb1Q3.Value + Me.txtComb2Q3.Value
                            Me.txtTotalComb1Q1.Value = Me.txtComb1Q4.Value + Me.txtComb2Q4.Value
                            'Me.ClearControl(Me.grpComS)
                        Case "F"
                            Me.grpCombQ.Visible = False : Me.grpComS.Visible = False : Me.grpCombF.Visible = True : Me.ClearControl(Me.grpCombF)
                            Me.txtComb1F1.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP1"))
                            Me.txtComb1F2.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP2"))
                            Me.txtComb1F3.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_FMP3"))
                            If Not (Me.GridEX1.GetValue("COMBINED_BRAND") Is DBNull.Value) Then
                                Me.clsAgInclude.CreatViewCombinedBrand(Me.MultiColumnCombo1.Text, Me.TreeView1, False)
                                Me.COMB_BRAND_ID = Me.GridEX1.GetValue("COMBINED_BRAND").ToString()
                                Me.clsAgInclude.GetTabelQuantityCombined(Me.COMB_BRAND_ID, Me.MultiColumnCombo1.Text, "F")
                                If Me.clsAgInclude.GetTblQuantityCombined().Rows.Count > 0 Then
                                    Me.txtComb2F1.Value = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("TARGET_FMP1")
                                    Me.txtComb2F2.Value = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("TARGET_FMP2")
                                    Me.txtComb2F3.Value = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("TARGET_FMP3")
                                    Me.cmbSeconBrand.Text = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("BRAND_NAME")
                                    Me.cmbSeconBrand.Enabled = False
                                Else
                                    Me.txtComb2F1.Value = 0
                                    Me.txtComb2F2.Value = 0
                                    Me.txtComb2F3.Value = 0
                                    Me.cmbSeconBrand.SelectedIndex = -1
                                    Me.cmbSeconBrand.Text = "" : Me.cmbSeconBrand.Enabled = True
                                End If
                            Else
                                Me.txtComb2F1.Value = 0
                                Me.txtComb2F2.Value = 0
                                Me.txtComb2F3.Value = 0
                                Me.cmbSeconBrand.SelectedIndex = -1 : Me.cmbSeconBrand.Text = ""
                                Me.cmbSeconBrand.Enabled = True : Me.TreeView1.Nodes.Clear()
                            End If
                            Me.txtTotalComb1F1.Value = Me.txtComb1F1.Value + Me.txtComb2F1.Value
                            Me.txtTotalComb2F2.Value = Me.txtComb1F2.Value + Me.txtComb2F2.Value
                            Me.txtTotalComb1F1.Value = Me.txtComb1F3.Value + Me.txtComb2F3.Value
                            'Me.ClearControl(Me.grpCombQ)
                        Case "S"
                            Me.grpCombQ.Visible = False : Me.grpCombF.Visible = False : Me.grpComS.Visible = True : Me.ClearControl(Me.grpComS)
                            Me.txtComb1S1.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_S1"))
                            Me.txtComb1S2.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_S2"))
                            If Not (Me.GridEX1.GetValue("COMBINED_BRAND") Is DBNull.Value) Then
                                Me.clsAgInclude.CreatViewCombinedBrand(Me.MultiColumnCombo1.Text, Me.TreeView1, False)
                                Me.COMB_BRAND_ID = Me.GridEX1.GetValue("COMBINED_BRAND").ToString()
                                Me.clsAgInclude.GetTabelQuantityCombined(Me.COMB_BRAND_ID, Me.MultiColumnCombo1.Text, "S")
                                If Me.clsAgInclude.GetTblQuantityCombined().Rows.Count > 0 Then
                                    Me.txtComb2S1.Value = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("TARGET_S1")
                                    Me.txtComb2S2.Value = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("TARGET_S2")
                                    Me.cmbSeconBrand.Text = Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("BRAND_NAME")
                                    Me.cmbSeconBrand.Enabled = False
                                Else
                                    Me.txtComb2S1.Value = 0 : Me.txtComb2S2.Value = 0 : Me.cmbSeconBrand.SelectedIndex = -1
                                    Me.cmbSeconBrand.Text = "" : Me.cmbSeconBrand.Enabled = True
                                End If
                            Else
                                Me.txtComb2S1.Text = 0 'Me.clsAgInclude.GetTblQuantityCombined().Rows(0)("TARGET_S1").ToString()
                                Me.txtComb2S2.Value = 0 : Me.cmbSeconBrand.SelectedIndex = -1 : Me.cmbSeconBrand.Text = ""
                                Me.cmbSeconBrand.Enabled = True : Me.TreeView1.Nodes.Clear()
                            End If
                            Me.txtTotalComb1S1.Value = Me.txtComb1S1.Value + Me.txtComb2S1.Value
                            Me.txtTotalComb2S2.Value = Me.txtComb1S2.Value + Me.txtComb2S2.Value
                            'Me.ClearControl(Me.grpCombQ)
                    End Select
                    'Case ActiveTab.OAHistory
                    '    If Not IsNothing(Me.clsAgInclude.ViewOAHistory()) Then
                    '        'Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                    '        Me.clsAgInclude.ViewOAHistory().RowFilter = "BRANDPACK_NAME LIKE '" & Me.GridEX1.GetValue("BRAND_NAME").ToString() & "%'"
                    '        'Me.GridEX2.SetDataBinding(Me.clsAgInclude.ViewOAHistory(), "")
                    '    End If

            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_CurrentCellChanged")
        Finally
            Me.ReadAccecs()
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Not (e.Row.RowType = Janus.Windows.GridEX.RowType.Record) Then : Return : End If
            Select Case Me.UiTab1.SelectedTab.Name
                Case "tbBrandInclude"
                    'check apakah ada data anaknya
                    If IsNothing(Me.grdAddedBrandPack.DataSource) Then
                        If Me.grdAddedBrandPack.RecordCount <= 0 Then
                            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                                e.Cancel = True : Return
                            End If
                        End If
                    ElseIf Me.grdAddedBrandPack.RecordCount <= 0 Then
                        If Me.grdAddedBrandPack.RecordCount <= 0 Then
                            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                                e.Cancel = True
                                Return
                            End If
                        End If
                    Else
                        e.Cancel = True : Me.ShowMessageInfo(Me.MessageCantDeleteData) : Return
                    End If
                    If Not IsDBNull(Me.GridEX1.GetValue("COMBINED_BRAND")) Then
                        Me.clsAgInclude.DeleteBrandInclude(Me.GridEX1.GetValue(0).ToString(), Me.GridEX1.GetValue("COMBINED_BRAND"))
                    Else
                        Me.clsAgInclude.DeleteBrandInclude(Me.GridEX1.GetValue(0).ToString())
                    End If
                    Me.ShowMessageInfo(Me.MessageSuccesDelete) : e.Cancel = False : Me.GridEX1.UpdateData()
                Case "tbOAHistory"
            End Select
            'btnRefresh_BtnClick(Me.btnRefresh, New System.EventArgs())
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        End Try
    End Sub

    Private Sub grdunAddedBrandPack_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdunAddedBrandPack.KeyDown
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim chekedCol As String = ""
            If Me.AGREE_BRAND_ID = "" Then
                Me.ShowMessageInfo("Please define Brand.")
                Return
            End If
            If e.KeyCode = Keys.Enter Then
                For i As Integer = 0 To Me.grdunAddedBrandPack.RecordCount - 1
                    Me.grdunAddedBrandPack.MoveTo(i)
                    If Me.grdunAddedBrandPack.GetRow().CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                        chekedCol &= Me.grdunAddedBrandPack.GetValue("BRANDPACK_ID").ToString()
                    End If
                Next
                If chekedCol <> "" Then
                    If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                        Return
                    End If
                    If Me.ShowConfirmedMessage(Me.MessageSaveChanges) = Windows.Forms.DialogResult.No Then
                        Return
                    End If
                    Dim BRANDPACKIDS As New Collection()
                    Dim X As Integer = 1
                    For i As Integer = 0 To Me.grdunAddedBrandPack.RecordCount - 1
                        Me.grdunAddedBrandPack.MoveTo(i)
                        If Me.grdunAddedBrandPack.GetRow().CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                            BRANDPACKIDS.Add(Me.grdunAddedBrandPack.GetValue("BRANDPACK_ID").ToString(), X)
                            X += 1
                        End If
                    Next
                    Me.clsAgInclude.SaveBrandPack(Me.MultiColumnCombo1.Value.ToString(), Me.AGREE_BRAND_ID, BRANDPACKIDS)
                    Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
                Else
                    Me.ShowMessageInfo("Please defined brandpack")
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdBrandPack_KeyDown")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdAddedBrandPack_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdAddedBrandPack.DeletingRecord
        Try
            If (Me.MultiColumnCombo1.SelectedIndex = -1) Or (Me.MultiColumnCombo1.Text = "") Then : Return : End If
            If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.ShowMessageInfo("Unknown brand include !" & vbCrLf & "Please select row in datagrid !")
                e.Cancel = True : Return
            End If
            Dim Message As String = ""
            If Me.clsAgInclude.CheckChildReference_BrandPack_Include(Me.grdAddedBrandPack.GetValue("BRANDPACK_ID").ToString(), _
            Me.MultiColumnCombo1.Text, Message) > 0 Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData & vbCrLf & Message) : e.Cancel = True : Return
            End If
            'confirmasikan ke user
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then : e.Cancel = True : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.clsAgInclude.DeleteBrandPack(Me.MultiColumnCombo1.Text + "" + Me.grdAddedBrandPack.GetValue("BRANDPACK_ID").ToString())
            'procedure untuk mendelete data
            Me.ShowMessageInfo(Me.MessageSuccesDelete)
            'Me.lstAddedBrand.Items.Remove(Me.lstAddedBrand.SelectedItem)
            'Dim dv As DataView = Me.clsAgInclude.GetItemBrandPackIncludedByBrandID(Me.GridEX1.GetValue("BRAND_ID").ToString(), Me.MultiColumnCombo1.Value.ToString())
            Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
            'Me.GridEX1.MoveTo(index)
        Catch ex As Exception
            e.Cancel = True : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GridEX3_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdAddedBrandPack_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdAddedBrandPack.MouseDoubleClick
        Try
            If (Me.MultiColumnCombo1.SelectedIndex = -1) Or (Me.MultiColumnCombo1.Text = "") Then : Return : End If
            If IsNothing(Me.grdAddedBrandPack.DataSource) Then : Return : End If
            If Me.grdAddedBrandPack.RecordCount <= 0 Then : Return : End If
            If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.ShowMessageInfo("Unknown brand include !" & vbCrLf & "Please select row in datagrid !") : Return
            End If
            If Not Me.grdAddedBrandPack.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then : Return : End If
            Dim Message As String = ""
            If Me.clsAgInclude.CheckChildReference_BrandPack_Include(Me.grdAddedBrandPack.GetValue("BRANDPACK_ID").ToString(), _
            Me.MultiColumnCombo1.Text, Message) > 0 Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData & vbCrLf & Message) : Return
            End If
            'confirmasikan ke user
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.clsAgInclude.DeleteBrandPack(Me.MultiColumnCombo1.Text + "" + Me.grdAddedBrandPack.GetValue("BRANDPACK_ID").ToString())
            'procedure untuk mendelete data
            Me.ShowMessageInfo(Me.MessageSuccesDelete)
            'Me.lstAddedBrand.Items.Remove(Me.lstAddedBrand.SelectedItem)
            Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GridEX3_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region
#End Region

#Region " MULTICOLUMN COMBO "

    'jika mcb.text = "" clearkan semua data
    'jika index item di pilih hidupkan semua data yang berada di tab UI
    'isi gridex dengan item berdasarkan dari tab yang di pilih
    'hidupkan mode save,setiap item value changed
    'mode update hanya hidup kalau di select didatagrid

    Private Sub MultiColumnCombo1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MultiColumnCombo1.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ClearControl(Me.grpQuarterly) : Me.ClearControl(Me.grpSemesterly) : Me.ClearControl(Me.tbFMP) : Me.txtGiven.Text = "" : Me.txtBrandName.Text = ""
            If Me.SFM = StateFillingCombo.Filling Then : Return : End If
            If Me.MultiColumnCombo1.Text = "" Then : Me.ClearData() : Return : End If
            If Me.MultiColumnCombo1.SelectedIndex <= -1 Then : Me.ClearData() : Return : End If
            If Me.MultiColumnCombo1.SelectedItem Is Nothing Then
                'jika ada item dari listbox brandpack clearkan
                Me.btnAddNew.Enabled = False : Return
            End If
            Dim Index As Integer = Me.clsAgInclude.ViewAgreement.Find(Me.MultiColumnCombo1.Text)
            If Index <> -1 Then
                Me.QS_FLAG = Me.clsAgInclude.ViewAgreement(Index)("QS_TREATMENT_FLAG").ToString()
                Dim StartDate As DateTime = Me.clsAgInclude.ViewAgreement(Index)("START_DATE")
                Dim EndDate As DateTime = Me.clsAgInclude.ViewAgreement(Index)("END_DATE")
                If StartDate >= New Date(2019, 8, 1) And EndDate <= New Date(2020, 7, 31) And Me.QS_FLAG = "Q" Then
                    Me.isTransitionTime = True
                End If
            End If
            Select Case Me.UiTab1.SelectedTab.Name
                Case "tbBrandInclude"
                    Me.UiTab1_SelectedTabChanged(Me.UiTab1, New Janus.Windows.UI.Tab.TabEventArgs(Me.UiTab1.TabPages(0)))
                    Me.btnAddNew.Enabled = True
                Case "tbCombinedBrand"
                    Me.UiTab1_SelectedTabChanged(Me.UiTab1, New Janus.Windows.UI.Tab.TabEventArgs(Me.UiTab1.TabPages(1)))
                    Me.btnAddNew.Enabled = False
                Case "tbOAHistory"
                    Me.UiTab1_SelectedTabChanged(Me.UiTab1, New Janus.Windows.UI.Tab.TabEventArgs(Me.UiTab1.TabPages(2)))
            End Select
            If Me.QS_FLAG = "F" Then
                Dim tbl As DataTable = Me.clsAgInclude.getSchemaR(Me.MultiColumnCombo1.Text, True)
                Me.ds4MPeriode = New DataSet("DS4periode")
                Me.ds4MPeriode.Tables.Add(tbl)
                BindGrid4MPeriode()
                Me.MainTbBrandProgressive.SelectedIndex = 1
                Me.TabControl1.SelectedIndex = 2
                'Me.grpPotensi.Visible = False
            ElseIf Me.QS_FLAG = "Q" Then
                If Me.isTransitionTime Then
                    Dim HasRef As Boolean = False
                    Dim tbl As DataTable = Me.clsAgInclude.getSchemaR(Me.MultiColumnCombo1.Text, True)
                    Me.ds4MPeriode = New DataSet("DS4periode")
                    Me.ds4MPeriode.Tables.Add(tbl)
                    BindGrid4MPeriode()
                    Me.MainTbBrandProgressive.SelectedIndex = 1
                    Me.TabControl1.SelectedIndex = 2
                Else
                    Me.MainTbBrandProgressive.SelectedIndex = 0
                    Me.TabControl1.SelectedIndex = 1
                End If
                'Me.grpPotensi.Visible = True
            Else
                Me.MainTbBrandProgressive.SelectedIndex = 0
                Me.TabControl1.SelectedIndex = 0
                'Me.grpPotensi.Visible = True
            End If

        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_MultiColumnCombo1_ValueChanged")
        Finally
            Me.ReadAccecs() : Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " TAB "

    Private Sub UiTab1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.UI.Tab.TabEventArgs) Handles UiTab1.SelectedTabChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.IsEnabledEditgrid = True
            If Me.clsAgInclude Is Nothing Then
                Return
            End If
            Select Case e.Page.Name
                Case "tbBrandInclude"
                    If Me.Hload = HasLoad.NotYet Then : Return : End If
                    If Me.MultiColumnCombo1.SelectedItem Is Nothing Then
                        Me.BindGrid(Nothing, "") : Me.BindGridEX(Nothing, "") : Me.grpTypeDiscount.Visible = False
                        Return
                    End If
                    Me.clsAgInclude.GetItemBrandByAgreementNo(Me.MultiColumnCombo1.Text, AgreementEndDate)
                    If Me.txtFilterBrandName.Text = "" Then
                        Me.BindGrid(Me.clsAgInclude.ViewFilterBrand, "")
                    Else
                        Me.BindGrid(Me.clsAgInclude.ViewFilterBrand, "BRAND_NAME like '%" + Me.txtFilterBrandName.Text + "%'")
                    End If
                    'Me.lstAddedBrand.DataSource = Nothing
                    'Me.lstAddedBrand.Items.Clear()
                    Me.UnabledTextBox(Me.grpSemesterly)
                    Me.UnabledTextBox(Me.grpQuarterly)
                    For Each Item As Control In Me.tbFMP.Controls
                        If TypeOf (Item) Is TextBox Then
                            Item.Enabled = False
                            Item.BackColor = Color.White
                        ElseIf TypeOf (Item) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                            Item.Enabled = False
                        End If
                    Next
                    Me.txtGiven.Text = ""
                    Me.txtBrandName.Text = ""
                    Me.ClearControl(Me.grpQuarterly)
                    Me.ClearControl(Me.grpSemesterly)
                    'BIND DATAgrid dengan dataset
                    Me.grpTypeDiscount.Visible = True
                    Me.MaySelectGrid = CanSelectGridEx.CanNot
                    Me.clsAgInclude.GetViewBrandInclude(Me.MultiColumnCombo1.Text)
                    Me.BindGridEX(Me.clsAgInclude.ViewBrand(), "")
                    Me.grdunAddedBrandPack.SetDataBinding(Nothing, "")
                    'me.clsAgInclude.GetDsSetPeriod(
                    Me.MaySelectGrid = CanSelectGridEx.Can
                    'Me.grpIBPfromBrand.Text = ""
                    Me.txtBrandName.Text = ""
                    Me.btnAddNew.Enabled = True
                    Me.txtGiven.Enabled = False
                    Me.TabAcktive = ActiveTab.BrandInclude
                Case "tbCombinedBrand"
                    If (Me.MultiColumnCombo1.Text = "") Or (Me.MultiColumnCombo1.SelectedIndex = -1) Then : Return : End If
                    If Me.Hload = HasLoad.NotYet Then
                    Else
                        Me.MaySelectGrid = CanSelectGridEx.CanNot
                        Me.HFC = HasFilledComboFirstSecond.Filling
                        Me.BindGridEX(Me.clsAgInclude.GetViewBrandInclude(Me.MultiColumnCombo1.Text), "")
                        Me.MaySelectGrid = CanSelectGridEx.Can
                    End If
                    Me.TreeView1.Visible = True
                    Me.clsAgInclude.CreatViewCombinedBrand(Me.MultiColumnCombo1.Text, Me.TreeView1, False)
                    Me.ClearControl(Me.grpCombQ)
                    Me.ClearControl(Me.grpComS)
                    Me.grpCombQ.Visible = False
                    Me.grpComS.Visible = False
                    Me.grpComboFirstSecond.Visible = False
                    Me.lblFirstBrand.Text = ""

                    Me.btnAddNew.Enabled = False
                    Me.TabAcktive = ActiveTab.CombinedBrand
                Case "tbOAHistory"
                    Me.TabAcktive = ActiveTab.OAHistory
                    Me.MaySelectGrid = CanSelectGridEx.CanNot
                    Me.BindGridEX(Me.clsAgInclude.GetViewBrandInclude(Me.MultiColumnCombo1.Text), "")
                    Me.grdunAddedBrandPack.SetDataBinding(Nothing, "")
                    'me.clsAgInclude.GetDsSetPeriod(
                    Me.MaySelectGrid = CanSelectGridEx.Can
                    'If Me.MultiColumnCombo1.SelectedItem Is Nothing Then
                    '    Me.BindGrid_1(Me.GridEX2, Nothing)
                    'Else
                    '    Me.clsAgInclude.CreateViewOAHistory(Me.MultiColumnCombo1.Value.ToString())
                    '    Me.GridEX2.SetDataBinding(Me.clsAgInclude.ViewOAHistory(), "")
                    'End If

            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_UiTab1_SelectedTabChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub tbPeriodic_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.UI.Tab.TabEventArgs) Handles tbPeriodic.SelectedTabChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim AGREE_BRAND_ID As String
            If Me.Mode = SaveMode.Save Then
                AGREE_BRAND_ID = Me.MultiColumnCombo1.Value.ToString() + "" + Me.Brand_ID
            Else
                AGREE_BRAND_ID = Me.MultiColumnCombo1.Value.ToString() + "" + Me.Brand_IDHide
            End If
            Select Case tbPeriodic.SelectedTab.Name
                Case "tbPeriode"
                    If Not IsNothing(Me.clsAgInclude.getDsPeriod) Then
                        If Me.dgvPeriodic.RowCount > 0 Then
                            If (Me.clsAgInclude.HasgeneratedDiscount(AGREE_BRAND_ID, Me.QS_FLAG)) Then
                                If Me.IsEnabledEditgrid = False Then : Return : End If
                                Me.dgvPeriodic.Enabled = False : Me.chkTypical.Enabled = False
                            Else : Me.dgvPeriodic.Enabled = True : End If
                        Else : Me.dgvPeriodic.Enabled = True : chkTypical.Enabled = True : End If
                    Else : Me.dgvPeriodic.Enabled = True : chkTypical.Enabled = True : End If
                Case "tbYearly"
                    If Not IsNothing(Me.clsAgInclude.getDsPeriod) Then
                        If Me.dgvYearly.RowCount > 0 Then
                            If Me.clsAgInclude.HasgeneratedDiscount(AGREE_BRAND_ID, "Y") Then
                                If Me.IsEnabledEditgrid = False Then : Return : End If
                                Me.dgvYearly.Enabled = False : Me.chkTypical.Enabled = False
                            Else
                                Me.dgvYearly.Enabled = True : chkTypical.Enabled = True
                            End If
                        Else : Me.dgvPeriodic.Enabled = True : chkTypical.Enabled = True : End If
                    Else : Me.dgvPeriodic.Enabled = True : chkTypical.Enabled = True : End If
            End Select
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub tbPeriodic_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPeriodic.Enter
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        If Me.IsValid(ActiveTab.BrandInclude, True) = False Then
            Return
        End If
        If (Me.chkTypical.Checked = True) And (Me.chkTypical.Enabled = True) Then
            Me.dgvPeriodic.Enabled = True
            Me.dgvPeriodicVal.Enabled = True
        Else
            Me.dgvPeriodic.Enabled = False
            Me.dgvYearlyVal.Enabled = False
        End If
    End Sub

#End Region

#Region " BUTTON "
    'filter data yang ada di mcb berdasarkan item yang di ketik
    Private Sub ButtonSearch1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            'Me.BindMulticolumnCombo("AGREEMENT_NO like '%" + Me.MultiColumnCombo1.Text + "%'")
            If Me.MultiColumnCombo1.Text = "" Then
                Me.baseTooltip.Show("Please type AGREEMENT_NO", Me.MultiColumnCombo1, 3000)
                Return
            End If
            Me.SFM = StateFillingCombo.Filling
            Me.clsAgInclude.SearchAgreement(Me.MultiColumnCombo1.Text)
            Me.BindMulticolumnCombo("")
            Dim itemCount As Integer = Me.MultiColumnCombo1.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ButtonSearch1_btnClick")
        Finally
            Me.SFM = StateFillingCombo.HasFilled : Me.Cursor = Cursors.Default
        End Try
    End Sub
    'bila di clik hidupkan QS flag di groupbox dengan merefer di mcb valuenya
    'bila value nya S berarti groupobox target semesterly di activ kan begijtu pula sebaliknya
    'clear semua isi item textbox
    'clearkan item brandpack_include supaya user mengerti bahwa ini akan menginput data brand
    'hidupkan mode save
    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Try
            Me.IsEnabledEditgrid = True

            If (Not IsNothing(Me.clsAgInclude.getDsPeriod())) Then
                If (Me.clsAgInclude.getDsPeriod.HasChanges()) Then
                    Me.clsAgInclude.getDsPeriod().RejectChanges()
                End If
                Me.clsAgInclude.getDsPeriod().Clear()
            End If
            If Me.QS_FLAG = "S" Then
                'Me.clsAgInclude.GetTableSemesterly().Clear()
                Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableSemesterly()
                Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableSemesterlyV()

            ElseIf Me.QS_FLAG = "Q" Then
                'Me.clsAgInclude.GetTableQuarterly().Clear()
                Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableQuarterly()
                Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableQuarterlyV()

            End If
            Me.chkTypical.Enabled = True

            Me.dgvYearly.DataSource = Me.clsAgInclude.GetTableYearly()
            Me.dgvYearlyVal.DataSource = Me.clsAgInclude.GetTableYearlyV()

            If Me.SavingChanges1.btnSave.Enabled = True Then
            Else
                Me.SavingChanges1.btnSave.Enabled = True
            End If
            Me.ClearControl(Me.grpQuarterly)
            Me.ClearControl(Me.grpSemesterly)
            Me.ClearControl(Me.tbFMP)
            Me.txtGiven.Enabled = True
            Me.txtGiven.Text = ""
            Select Case Me.QS_FLAG
                Case "Q"
                    Me.txtQ1QTY.Enabled = True
                    Me.txtQ2QTY.Enabled = True
                    Me.txtQ3QTY.Enabled = True
                    Me.txtQ4QTY.Enabled = True
                    'PL/FM
                    Me.txtFreeMarketQ1.Enabled = True
                    Me.txtFreeMarketQ2.Enabled = True
                    Me.txtFreeMarketQ3.Enabled = True
                    Me.txtFreeMarketQ4.Enabled = True
                    Me.txtPlQ1.Enabled = True
                    Me.txtPlQ2.Enabled = True
                    Me.txtPlQ3.Enabled = True
                    Me.txtPlQ4.Enabled = True
                    If Me.isTransitionTime Then
                        Me.txtFMP1.Enabled = True
                        Me.txtFMP2.Enabled = True
                        Me.txtFMP3.Enabled = True

                        Me.txtFMPFM1.Enabled = True
                        Me.txtFMPFM2.Enabled = True
                        Me.txtFMPFM3.Enabled = True

                        Me.txtFMPPL1.Enabled = True
                        Me.txtFMPPL2.Enabled = True
                        Me.txtFMPPL3.Enabled = True
                    End If

                    Me.txtFreeMarketS1.Enabled = False
                    Me.txtFreeMarketS2.Enabled = False
                    Me.txtPlS1.Enabled = False
                    Me.txtPlS2.Enabled = False
                    Me.txtS1QTY.Enabled = False
                    Me.txtS2QTY.Enabled = False
                Case "S"
                    Me.txtS1QTY.Enabled = True
                    Me.txtS2QTY.Enabled = True

                    'PL/FM
                    Me.txtFreeMarketS1.Enabled = True
                    Me.txtFreeMarketS2.Enabled = True
                    Me.txtPlS1.Enabled = True
                    Me.txtPlS2.Enabled = True

                    Me.txtFreeMarketQ1.Enabled = False
                    Me.txtFreeMarketQ2.Enabled = False
                    Me.txtFreeMarketQ3.Enabled = False
                    Me.txtFreeMarketQ4.Enabled = False

                    Me.txtPlQ1.Enabled = False
                    Me.txtPlQ2.Enabled = False
                    Me.txtPlQ3.Enabled = False
                    Me.txtPlQ4.Enabled = False

                    Me.txtQ1QTY.Enabled = False
                    Me.txtQ2QTY.Enabled = False
                    Me.txtQ3QTY.Enabled = False
                    Me.txtQ4QTY.Enabled = False

                    Me.txtFMP1.Enabled = False
                    Me.txtFMP2.Enabled = False
                    Me.txtFMP3.Enabled = False

                    Me.txtFMPFM1.Enabled = False
                    Me.txtFMPFM2.Enabled = False
                    Me.txtFMPFM3.Enabled = False

                    Me.txtFMPPL1.Enabled = False
                    Me.txtFMPPL2.Enabled = False
                    Me.txtFMPPL3.Enabled = False
                Case "F"
                    Me.txtFMP1.Enabled = True
                    Me.txtFMP2.Enabled = True
                    Me.txtFMP3.Enabled = True

                    Me.txtFMPFM1.Enabled = True
                    Me.txtFMPFM2.Enabled = True
                    Me.txtFMPFM3.Enabled = True

                    Me.txtFMPPL1.Enabled = True
                    Me.txtFMPPL2.Enabled = True
                    Me.txtFMPPL3.Enabled = True

                    Me.txtQ1QTY.Enabled = False
                    Me.txtQ2QTY.Enabled = False
                    Me.txtQ3QTY.Enabled = False
                    Me.txtQ4QTY.Enabled = False
                    'PL/FM
                    Me.txtFreeMarketQ1.Enabled = False
                    Me.txtFreeMarketQ2.Enabled = False
                    Me.txtFreeMarketQ3.Enabled = False
                    Me.txtFreeMarketQ4.Enabled = False
                    Me.txtPlQ1.Enabled = False
                    Me.txtPlQ2.Enabled = False
                    Me.txtPlQ3.Enabled = False
                    Me.txtPlQ4.Enabled = False

                    Me.txtFreeMarketS1.Enabled = False
                    Me.txtFreeMarketS2.Enabled = False
                    Me.txtPlS1.Enabled = False
                    Me.txtPlS2.Enabled = False
                    Me.txtS1QTY.Enabled = False
                    Me.txtS2QTY.Enabled = False

            End Select
            Me.BindGrid_1(Me.grdAddedBrandPack, Nothing)
            Me.chkTypical.Checked = False
            Me.txtGiven.Focus()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnAddNew_Click")
        Finally
            Me.Mode = SaveMode.Save
        End Try
    End Sub

    Private Sub btnDeleteS_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteS.EnabledChanged
        Try
            If (Me.txtS1QTY.Text <> "") Or (Me.txtS2QTY.Text <> "") Then
                Me.btnDeleteS.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDeleteQ1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteQ1.Click
        Try
            'If (Me.txtComb2Q1.Value = 0) Or (Me.txtComb2Q2.Value = 0) Or (Me.txtComb2Q3.Value = 0) _
            '    Or (Me.txtComb2Q4.Value = 0) Then : Return
            'End If
            If (Me.MultiColumnCombo1.Text = "") Or (Me.MultiColumnCombo1.SelectedIndex = -1) Then : Return : End If
            If Me.COMB_BRAND_ID = "" Then : Return : End If
            'konfirmasi ke user
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.clsAgInclude.UpdateCombinedBrand(Me.MultiColumnCombo1.Text, Me.AGREE_BRAND_ID, _
            Me.COMB_BRAND_ID, True, Me.GridEX1.GetValue("BRAND_NAME").ToString.Contains("ROUNDUP"))
            'update 'comb_brand_id jadi null'dua rows combined
            'rows yang satu yang brand_id yang berhubungan dengan agree_brand_id 
            'row yang ke dua brand_id yang berisi comb_agree_brand_id
            Me.ShowMessageInfo(Me.MessageSuccesDelete) : Me.btnRefReshCombined_BtnClick(Me.btnRefReshCombined, New System.EventArgs())
            If Not IsNothing(Me.grdunAddedBrandPack.DataSource) Then : Me.grdunAddedBrandPack.SetDataBinding(Nothing, "") : End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnDeleteQ1_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnDeleteS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteS.Click
        Try
            If (Me.MultiColumnCombo1.Text = "") Or (Me.MultiColumnCombo1.SelectedIndex = -1) Then
                Return
            End If
            If Me.COMB_BRAND_ID = "" Then
                Return
            End If
            'If (Me.txtComb2S1.Value = 0) Or (Me.txtComb2S2.Value = 0) Then
            '    Return
            'End If
            'konfirmasi ke user
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.clsAgInclude.UpdateCombinedBrand(Me.MultiColumnCombo1.Text, Me.AGREE_BRAND_ID, _
            Me.COMB_BRAND_ID, True, Me.GridEX1.GetValue("BRAND_NAME").ToString().Contains("ROUNDUP"))
            'update 'comb_brand_id jadi null'dua rows combined
            'rows yang satu yang brand_id yang berhubungan dengan agree_brand_id 
            'row yang ke dua brand_id yang berisi comb_agree_brand_id
            Me.ShowMessageInfo(Me.MessageSuccesDelete)
            Me.btnRefReshCombined_BtnClick(Me.btnRefReshCombined, New System.EventArgs())
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnDeleteQ1_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRefReshCombined_BtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefReshCombined.BtnClick
        Try
            Me.UiTab1_SelectedTabChanged(Me.UiTab1, New Janus.Windows.UI.Tab.TabEventArgs(Me.UiTab1.SelectedTab()))
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnRefReshCombined_BtnClick")
        End Try
    End Sub
    Private Sub btnRefresh_BtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.BtnClick
        Try
            Me.UiTab1_SelectedTabChanged(Me.UiTab1, New Janus.Windows.UI.Tab.TabEventArgs(Me.UiTab1.SelectedTab))
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnRefreshBrand_BtnClick")
        End Try
    End Sub

#End Region

#Region " TEXTBOX "

    'jika txtbrandname <> ""
    'jika dalam datagrid ada item brandname berarti mau nginput brand
    'clearkan listbox item brandpack included,listbox brandpack untuk saat ini hanya melihat brandpack dari brand yang di pilih

    'jika dalam datagrid tidak ada item brandname yang ada di textbox txtbrandname
    'berarti mode update

    'isi listbox brandpack dengan item brandpack dari brandID berdasarkan brandName yang ada 
    ' di tetxbox txtbrandName dengan item brandpack yang belum di input ke brandpack_included
    'brand id disini brand_id yang di hide di grid ex
    'isi listbox itembrandpack included dengan item brandpackname yang sudah ada di table agree_brandpack included
    'berdasarkan dari brand_name yang ada di textbox txtbrandname
    Private Sub txtBrandName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrandName.TextChanged
        Try
            If Me.txtBrandName.Text <> "" Then

            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_txtBrandName_TextChanged")
        End Try
    End Sub

    Private Sub txtFilterBrandName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilterBrandName.TextChanged
        Try
            Me.txtBrandName.Text = ""
            If Me.DataGrid1.DataSource Is Nothing Then
                Return
            End If
            If Me.txtFilterBrandName.Text = "" Then
                Me.BindGrid(Me.clsAgInclude.ViewFilterBrand, "")
            Else
                Me.BindGrid(Me.clsAgInclude.ViewFilterBrand, "BRAND_NAME LIKE '%" + Me.txtFilterBrandName.Text + "%'")
            End If
        Catch ex As Exception

        End Try
    End Sub
    'mode update bila ada item yang di selected
    'lihat tab yang active
    'jika tab yang active tab brandinclude isi textbox brandname dengan item brand name yang ada di di gridex
    'textbox brandname event txtbrandname_changed akan bekerja menyeleksi item yang ada di textbox tersebut
    Private Sub txtCombQ_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComb1Q1.ValueChanged, _
        txtComb1Q2.ValueChanged, txtComb1Q3.ValueChanged, txtComb1Q4.ValueChanged, txtComb2Q1.ValueChanged, txtComb2Q2.ValueChanged, _
        txtComb2Q3.ValueChanged, txtComb2Q4.ValueChanged
        Try
            If (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb1Q1) Or _
            (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb2Q1) Then
                Me.txtTotalComb1Q1.Value = Me.txtComb1Q1.Value + Me.txtComb2Q1.Value
            ElseIf (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb2Q1) Or _
              (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb2Q2) Then
                Me.txtTotalComb2Q2.Value = Me.txtComb1Q2.Value + Me.txtComb2Q2.Value
            ElseIf (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb1Q3) Or _
               (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb2Q3) Then
                Me.txtTotalComb3Q3.Value = Me.txtComb1Q3.Value + Me.txtComb2Q3.Value
            ElseIf (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb1Q4) Or _
               (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb2Q4) Then
                Me.txtTotalComb4Q4.Value = Me.txtComb1Q4.Value + Me.txtComb2Q4.Value
            End If
            'If Me.txtComb2Q1.Text <> "" Then
            '    Me.txtTotalComb1Q1.Text = Val(Me.txtComb1Q1.Text) + Val(Me.txtComb2Q1.Text)
            'Else
            '    Me.txtTotalComb1Q1.Text = ""
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtCombS_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComb1S1.ValueChanged, _
     txtComb1S2.ValueChanged, txtComb2S1.ValueChanged, txtComb2S2.ValueChanged
        Try
            If (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb1S1) Or _
               (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb2S1) Then
                Me.txtTotalComb1S1.Value = Me.txtComb1S1.Value + Me.txtComb2S1.Value
            ElseIf (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb1S2) Or _
                   (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb2S2) Then
                Me.txtTotalComb2S2.Value = Me.txtComb1S2.Value + Me.txtComb2S2.Value
            End If
            Me.txtTotalComb1Q1 = Me.txtComb1Q1.Value + Me.txtComb1Q2.Value
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtS1QTY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtS1QTY.ValueChanged
        Try
            'If Me.SFG = StateFillingGrid.Filling Then
            '    Return
            'End If
            Me.lblTargetSemesterly.Text = Me.GetTargetYear("S")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtS2QTY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtS2QTY.ValueChanged, txtQ2QTY.ValueChanged
        Try
            'If Me.SFG = StateFillingGrid.Filling Then
            '    Return
            'End If
            Me.lblTargetSemesterly.Text = Me.GetTargetYear("S")
            'Me.lblTargetSemesterly.Text = "Yearly = : 0"
            'Me.lblTargetSemesterly.Text = "Yearly = : " & CStr(Val(Me.txtS1QTY.Text) + Val(Me.txtS2QTY.Text))
            ''If Me.txtS2QTY.Validate <> "" Then
            ''    If Me.txtS1QTY.Text <> "" Then

            ''    Else
            ''        Me.lblTargetSemesterly.Text = "Yearly = : " & Me.txtS2QTY.Text
            '    End If
            'Else
            '    Me.txtS2QTY.Text = "0"
            '    Me.lblTargetSemesterly.Text = "Yearly = : " & Me.txtS1QTY.Text
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtQ1QTY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQ1QTY.ValueChanged
        Try
            'If Me.SFG = StateFillingGrid.Filling Then
            '    Return
            'End If
            'Me.lblTargetQuarterly.Text = "Yearly = : 0"
            'If Me.txtQ1QTY.Value <> 0 Then
            '    Me.lblTargetQuarterly.Text = "Yearly = : " & Val(Me.txtQ1QTY.Text) + Val(Me.txtQ2QTY.Text) _
            '    + Val(Me.txtQ3QTY.Text) + Val(Me.txtQ4QTY.Text)
            'Else
            '    Me.txtQ1QTY.Value = 0
            'End If
            Me.lblTargetQuarterly.Text = Me.GetTargetYear("Q")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtQ2QTY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'If Me.SFG = StateFillingGrid.Filling Then
            '    Return
            'End If
            Me.lblTargetQuarterly.Text = Me.GetTargetYear("Q")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtQ3QTY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQ3QTY.ValueChanged
        Try
            'If Me.SFG = StateFillingGrid.Filling Then
            '    Return
            'End If
            Me.lblTargetQuarterly.Text = Me.GetTargetYear("Q")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtQ4QTY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQ4QTY.ValueChanged
        Try
            'If Me.SFG = StateFillingGrid.Filling Then
            '    Return
            'End If
            Me.lblTargetQuarterly.Text = Me.GetTargetYear("Q")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtFreeMarket_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFreeMarketS2.ValueChanged, txtFreeMarketS1.ValueChanged, txtFreeMarketQ4.ValueChanged, txtFreeMarketQ3.ValueChanged, txtFreeMarketQ2.ValueChanged, txtFreeMarketQ1.ValueChanged
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        If Me.SFM = StateFillingCombo.Filling Then : Return : End If
        If Me.Hload = HasLoad.NotYet Then : Return : End If
        Me.SFG = StateFillingGrid.Filling
        Dim C As Janus.Windows.GridEX.EditControls.NumericEditBox = CType(sender, Janus.Windows.GridEX.EditControls.NumericEditBox)
        If C.Value Is Nothing Or C.Value Is DBNull.Value Then
            Select Case C.Name
                Case "txtFreeMarketQ1"
                    Me.txtPlQ1.Value = 0
                Case "txtFreeMarketQ2"
                    Me.txtPlQ2.Value = 0
                Case "txtFreeMarketQ3"
                    Me.txtPlQ3.Value = 0
                Case "txtFreeMarketQ4"
                    Me.txtPlQ4.Value = 0
                Case "txtFreeMarketS1"
                    Me.txtPlS1.Value = 0
                Case "txtFreeMarketS2"
                    Me.txtPlS2.Value = 0
            End Select
        ElseIf C.Value <= 0 Then
            Select Case C.Name
                Case "txtFreeMarketQ1"
                    Me.txtPlQ1.Value = 0
                Case "txtFreeMarketQ2"
                    Me.txtPlQ2.Value = 0
                Case "txtFreeMarketQ3"
                    Me.txtPlQ3.Value = 0
                Case "txtFreeMarketQ4"
                    Me.txtPlQ4.Value = 0
                Case "txtFreeMarketS1"
                    Me.txtPlS1.Value = 0
                Case "txtFreeMarketS2"
                    Me.txtPlS2.Value = 0
            End Select
        End If
        Select Case C.Name
            Case "txtFreeMarketQ1"
                Me.txtPlQ1.Value = IIf((Me.txtFreeMarketQ1.Value > 0), Convert.ToDecimal(Me.txtQ1QTY.Value) - Convert.ToDecimal(Me.txtFreeMarketQ1.Value), 0)
            Case "txtFreeMarketQ2"
                Me.txtPlQ2.Value = IIf((Me.txtFreeMarketQ2.Value > 0), Convert.ToDecimal(Me.txtQ2QTY.Value) - Convert.ToDecimal(Me.txtFreeMarketQ2.Value), 0)
            Case "txtFreeMarketQ3"
                Me.txtPlQ3.Value = IIf((Me.txtFreeMarketQ3.Value > 0), Convert.ToDecimal(Me.txtQ3QTY.Value) - Convert.ToDecimal(Me.txtFreeMarketQ3.Value), 0)
            Case "txtFreeMarketQ4"
                Me.txtPlQ4.Value = IIf((Me.txtFreeMarketQ4.Value > 0), Convert.ToDecimal(Me.txtQ4QTY.Value) - Convert.ToDecimal(Me.txtFreeMarketQ4.Value), 0)
            Case "txtFreeMarketS1"
                Me.txtPlS1.Value = IIf((Me.txtFreeMarketS1.Value > 0), Convert.ToDecimal(Me.txtS1QTY.Value) - Convert.ToDecimal(Me.txtFreeMarketS1.Value), 0)
            Case "txtFreeMarketS2"
                Me.txtPlS2.Value = IIf((Me.txtFreeMarketS2.Value > 0), Convert.ToDecimal(Me.txtS2QTY.Value) - Convert.ToDecimal(Me.txtFreeMarketS2.Value), 0)
        End Select
        Me.SFG = StateFillingGrid.HasFilled
    End Sub

    Private Sub txtPl_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlS2.ValueChanged, txtPlS1.ValueChanged, txtPlQ4.ValueChanged, txtPlQ3.ValueChanged, txtPlQ2.ValueChanged, txtPlQ1.ValueChanged
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        If Me.SFM = StateFillingCombo.Filling Then : Return : End If
        If Me.Hload = HasLoad.NotYet Then : Return : End If
        Me.SFG = StateFillingGrid.Filling
        Dim C As Janus.Windows.GridEX.EditControls.NumericEditBox = CType(sender, Janus.Windows.GridEX.EditControls.NumericEditBox)
        If C.Value Is Nothing Or C.Value Is DBNull.Value Then
            Select Case C.Name
                Case "txtPlQ1"
                    If Me.txtFreeMarketQ1.Value > 0 Then
                        Me.txtFreeMarketQ1.Value = Me.txtQ1QTY.Value
                    End If
                Case "txtPlQ2"
                    If Me.txtFreeMarketQ2.Value > 0 Then
                        Me.txtFreeMarketQ2.Value = Me.txtQ2QTY.Value
                    End If
                Case "txtPlQ3"
                    If Me.txtFreeMarketQ3.Value > 0 Then
                        Me.txtFreeMarketQ3.Value = Me.txtQ3QTY.Value
                    End If
                Case "txtPlQ4"
                    If Me.txtFreeMarketQ4.Value > 0 Then
                        Me.txtFreeMarketQ4.Value = Me.txtQ4QTY.Value
                    End If
                Case "txtPlS1"
                    If Me.txtFreeMarketS1.Value > 0 Then
                        Me.txtFreeMarketS1.Value = Me.txtS1QTY.Value
                    End If
                Case "txtPlS2"
                    If Me.txtFreeMarketS2.Value > 0 Then
                        Me.txtFreeMarketS2.Value = Me.txtS2QTY.Value
                    End If
            End Select
        ElseIf C.Value <= 0 Then
            Select Case C.Name
                Case "txtPlQ1"
                    If Me.txtFreeMarketQ1.Value > 0 Then
                        Me.txtFreeMarketQ1.Value = Me.txtQ1QTY.Value
                    End If
                Case "txtPlQ2"
                    If Me.txtFreeMarketQ2.Value > 0 Then
                        Me.txtFreeMarketQ2.Value = Me.txtQ2QTY.Value
                    End If
                Case "txtPlQ3"
                    If Me.txtFreeMarketQ3.Value > 0 Then
                        Me.txtFreeMarketQ3.Value = Me.txtQ3QTY.Value
                    End If
                Case "txtPlQ4"
                    If Me.txtFreeMarketQ4.Value > 0 Then
                        Me.txtFreeMarketQ4.Value = Me.txtQ4QTY.Value
                    End If
                Case "txtPlS1"
                    If Me.txtFreeMarketS1.Value > 0 Then
                        Me.txtFreeMarketS1.Value = Me.txtS1QTY.Value
                    End If
                Case "txtPlS2"
                    If Me.txtFreeMarketS2.Value > 0 Then
                        Me.txtFreeMarketS2.Value = Me.txtS2QTY.Value
                    End If
            End Select
        End If
        Select Case C.Name
            Case "txtPlQ1"
                Me.txtFreeMarketQ1.Value = IIf(((Me.txtFreeMarketQ1.Value > 0) And (txtPlQ1.Value > 0)), Me.txtQ1QTY.Value - Me.txtPlQ1.Value, 0)
            Case "txtPlQ2"
                Me.txtFreeMarketQ2.Value = IIf(((Me.txtFreeMarketQ2.Value > 0) And (txtPlQ2.Value > 0)), Me.txtQ2QTY.Value - Me.txtPlQ2.Value, 0)
            Case "txtPlQ3"
                Me.txtFreeMarketQ3.Value = IIf(((Me.txtFreeMarketQ3.Value > 0) And (txtPlQ3.Value > 0)), Me.txtQ3QTY.Value - Me.txtPlQ3.Value, 0)
            Case "txtPlQ4"
                Me.txtFreeMarketQ4.Value = IIf(((Me.txtFreeMarketQ4.Value > 0) And (txtPlQ4.Value > 0)), Me.txtQ4QTY.Value - Me.txtPlQ4.Value, 0)
            Case "txtPlS1"
                Me.txtFreeMarketS1.Value = IIf(((Me.txtFreeMarketS1.Value > 0) And (txtPlS1.Value > 0)), Me.txtS1QTY.Value - Me.txtPlS1.Value, 0)
            Case "txtPlS2"
                Me.txtFreeMarketS2.Value = IIf(((Me.txtFreeMarketS2.Value > 0) And (txtPlS2.Value > 0)), Me.txtS2QTY.Value - Me.txtPlS2.Value, 0)
        End Select
        Me.SFG = StateFillingGrid.HasFilled
    End Sub

#End Region

#Region " USER CONTROL SAVING CHANGES "

    Private Sub SavingChanges1_btnCloseClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavingChanges1.btnCloseClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.clsAgInclude Is Nothing Then
            Else
                Me.clsAgInclude.Dispose()
                Me.clsAgInclude = Nothing
            End If
            Me.Close()
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub fillAgreebrandIDinGridProgressive()
        If Me.clsAgInclude.getDsPeriod.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To Me.clsAgInclude.getDsPeriod.Tables(0).Rows.Count - 1
                If Not IsDBNull(Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("UP_TO_PCT")) And Not IsNothing(Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("UP_TO_PCT")) Then
                    If IsDBNull(Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("AGREE_BRAND_ID")) Or IsNothing(Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("AGREE_BRAND_ID")) Then
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i).BeginEdit()
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("AGREE_BRAND_ID") = Me.NewAgree_Brand_ID
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i).EndEdit()
                    ElseIf Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("AGREE_BRAND_ID").ToString() <> Me.NewAgree_Brand_ID Then
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i).BeginEdit()
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("AGREE_BRAND_ID") = Me.NewAgree_Brand_ID
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i).EndEdit()
                    End If
                    If Not IsDBNull(Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("PRGSV_DISC_PCT")) And Not IsNothing(Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("PRGSV_DISC_PCT")) Then
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i).BeginEdit()
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("AGREE_BRAND_ID") = Me.NewAgree_Brand_ID
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i).EndEdit()
                    ElseIf Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("AGREE_BRAND_ID").ToString() <> Me.NewAgree_Brand_ID Then
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i).BeginEdit()
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i)("AGREE_BRAND_ID") = Me.NewAgree_Brand_ID
                        Me.clsAgInclude.getDsPeriod.Tables(0).Rows(i).EndEdit()
                    End If
                End If
            Next
            For i As Integer = 0 To Me.clsAgInclude.getDsPeriod.Tables(1).Rows.Count - 1
                If Not IsDBNull(Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("UP_TO_PCT")) And Not IsNothing(Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("UP_TO_PCT")) Then
                    If IsDBNull(Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("AGREE_BRAND_ID")) Or IsNothing(Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("AGREE_BRAND_ID")) Then
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i).BeginEdit()
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("AGREE_BRAND_ID") = Me.NewAgree_Brand_ID
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i).EndEdit()
                    ElseIf Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("AGREE_BRAND_ID").ToString() <> Me.NewAgree_Brand_ID Then
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i).BeginEdit()
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("AGREE_BRAND_ID") = Me.NewAgree_Brand_ID
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i).EndEdit()
                    End If
                    If Not IsDBNull(Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("PRGSV_DISC_PCT")) And Not IsNothing(Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("PRGSV_DISC_PCT")) Then
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i).BeginEdit()
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("AGREE_BRAND_ID") = Me.NewAgree_Brand_ID
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i).EndEdit()
                    ElseIf Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("AGREE_BRAND_ID").ToString() <> Me.NewAgree_Brand_ID Then
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i).BeginEdit()
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i)("AGREE_BRAND_ID") = Me.NewAgree_Brand_ID
                        Me.clsAgInclude.getDsPeriod.Tables(1).Rows(i).EndEdit()
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub SavingChanges1_btnSaveClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavingChanges1.btnSaveClick
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim IndexRow As Integer = 0
            If Me.Mode = SaveMode.Update Then
                If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                    IndexRow = Me.GridEX1.Row
                End If
            End If

            Dim saveOnlySchemaR As Boolean = False
            If Not IsNothing(Me.ds4MPeriode) Then
                If Me.ds4MPeriode.HasChanges() Then
                    ''save
                    saveOnlySchemaR = True
                End If
            End If
            Dim val As Boolean = Me.IsValid(ActiveTab.BrandInclude, (Not saveOnlySchemaR))
            If Not val Then
                If saveOnlySchemaR Then
                    Me.clsAgInclude.SaveDS4Month(Me.ds4MPeriode.Tables(0), False)
                    Me.MultiColumnCombo1_ValueChanged(Me.MultiColumnCombo1, New EventArgs())
                    Me.SFG = StateFillingGrid.Filling
                    Me.GridEX1.Row = IndexRow
                    Me.GridEX1.SelectCurrentCellText()
                    Me.SFG = StateFillingGrid.HasFilled
                    Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
                    Return
                Else
                    Me.Cursor = Cursors.Default
                    Return
                End If
            End If

            'Me.clsAgInclude = New NufarmBussinesRules.DistributorAgreement.Include()
            Me.clsAgInclude.Agreement_no = Me.MultiColumnCombo1.Text
            Dim GivenPersen As Decimal = Me.txtGiven.Value
            Me.clsAgInclude.GIvenDiscount = GivenPersen
            Select Case Me.QS_FLAG
                Case "Q"
                    Me.clsAgInclude.Q1 = Me.txtQ1QTY.Value
                    Me.clsAgInclude.Q2 = Me.txtQ2QTY.Value
                    Me.clsAgInclude.Q3 = Me.txtQ3QTY.Value
                    Me.clsAgInclude.Q4 = Me.txtQ4QTY.Value

                    Me.clsAgInclude.Q1_FM = Me.txtFreeMarketQ1.Value
                    Me.clsAgInclude.Q2_FM = Me.txtFreeMarketQ2.Value
                    Me.clsAgInclude.Q3_FM = Me.txtFreeMarketQ3.Value
                    Me.clsAgInclude.Q4_FM = Me.txtFreeMarketQ4.Value
                    Me.clsAgInclude.Q1_PL = Me.txtPlQ1.Value
                    Me.clsAgInclude.Q2_PL = Me.txtPlQ2.Value
                    Me.clsAgInclude.Q3_PL = Me.txtPlQ3.Value
                    Me.clsAgInclude.Q4_PL = Me.txtPlQ4.Value

                    Me.clsAgInclude.S1_FM = 0
                    Me.clsAgInclude.S2_FM = 0
                    Me.clsAgInclude.S1_PL = 0
                    Me.clsAgInclude.S2_PL = 0
                    Me.clsAgInclude.S1 = 0
                    Me.clsAgInclude.S2 = 0

                    If Me.isTransitionTime Then
                        Me.clsAgInclude.FMP1 = Me.txtFMP1.Value
                        Me.clsAgInclude.FMP_FM1 = Me.txtFMPFM1.Value
                        Me.clsAgInclude.FMP_PL1 = Me.txtFMPPL1.Value

                        Me.clsAgInclude.FMP2 = Me.txtFMP2.Value
                        Me.clsAgInclude.FMP_FM2 = Me.txtFMPFM2.Value
                        Me.clsAgInclude.FMP_PL2 = Me.txtFMPPL2.Value

                        Me.clsAgInclude.FMP3 = Me.txtFMP3.Value
                        Me.clsAgInclude.FMP_FM3 = Me.txtFMPFM3.Value
                        Me.clsAgInclude.FMP_PL3 = Me.txtFMPPL3.Value
                    End If
                    'me.clsAgInclude.F
                Case "S"
                    Me.clsAgInclude.S1 = Me.txtS1QTY.Value
                    Me.clsAgInclude.S2 = Me.txtS2QTY.Value

                    Me.clsAgInclude.S1_FM = Me.txtFreeMarketS1.Value
                    Me.clsAgInclude.S2_FM = Me.txtFreeMarketS2.Value
                    Me.clsAgInclude.S1_PL = Me.txtPlS1.Value
                    Me.clsAgInclude.S2_PL = Me.txtPlS2.Value


                    Me.clsAgInclude.Q1_FM = 0
                    Me.clsAgInclude.Q2_FM = 0 'Me.txtFreeMarketQ2.Value
                    Me.clsAgInclude.Q3_FM = 0 'Me.txtFreeMarketQ3.Value
                    Me.clsAgInclude.Q4_FM = 0 'Me.txtFreeMarketQ4.Value
                    Me.clsAgInclude.Q1_PL = 0 'Me.txtPlQ1.Value
                    Me.clsAgInclude.Q2_PL = 0 'Me.txtPlQ2.Value
                    Me.clsAgInclude.Q3_PL = 0 'Me.txtPlQ3.Value
                    Me.clsAgInclude.Q4_PL = 0 'Me.txtPlQ4.Value

                    Me.clsAgInclude.Q1 = 0
                    Me.clsAgInclude.Q2 = 0
                    Me.clsAgInclude.Q3 = 0
                    Me.clsAgInclude.Q4 = 0
                Case "F"
                    Me.clsAgInclude.FMP1 = Me.txtFMP1.Value
                    Me.clsAgInclude.FMP_FM1 = Me.txtFMPFM1.Value
                    Me.clsAgInclude.FMP_PL1 = Me.txtFMPPL1.Value

                    Me.clsAgInclude.FMP2 = Me.txtFMP2.Value
                    Me.clsAgInclude.FMP_FM2 = Me.txtFMPFM2.Value
                    Me.clsAgInclude.FMP_PL2 = Me.txtFMPPL2.Value

                    Me.clsAgInclude.FMP3 = Me.txtFMP3.Value
                    Me.clsAgInclude.FMP_FM3 = Me.txtFMPFM3.Value
                    Me.clsAgInclude.FMP_PL3 = Me.txtFMPPL3.Value
            End Select
            Me.clsAgInclude.PBQ3 = Me.txtPBQ3.Value
            Me.clsAgInclude.PBQ4 = Me.txtPBQ4.Value
            Me.clsAgInclude.PBS2 = Me.txtPBS2.Value
            Me.clsAgInclude.CPQ1 = Me.txtCPQ1.Value
            Me.clsAgInclude.CPQ2 = Me.txtCPQ2.Value
            Me.clsAgInclude.CPQ3 = Me.txtCPQ3.Value
            Me.clsAgInclude.CPS1 = Me.txtCPS1.Value
            Me.clsAgInclude.PBY = Me.txtPBYear.Value
            Me.clsAgInclude.PBF2 = Me.txtPBF2.Value
            Me.clsAgInclude.PBF3 = Me.txtPBF3.Value
            Me.clsAgInclude.CPF1 = Me.txtCPF1.Value
            Me.clsAgInclude.CPF2 = Me.txtCPF2.Value
            Dim IsRoundUp As Boolean = False
            If Me.txtBrandName.Text.ToUpper.StartsWith("ROUNDUP") Then
                IsRoundUp = True
            End If
            Me.clsAgInclude.Flag = Me.QS_FLAG
            Dim HasChangedPrev As Boolean = Me.HasChangedDomPrev()
            Select Case Me.Mode
                Case SaveMode.Save
                    Me.clsAgInclude.Agree_Brand_ID = Me.NewAgree_Brand_ID
                    If Me.clsAgInclude.IsExisted(Me.NewAgree_Brand_ID) = True Then
                        Me.ShowMessageInfo("Agreement brand has existed !")
                        Return
                    End If
                    Dim DistriButorIDs As New List(Of String)
                    Dim DV As DataView = Me.clsAgInclude.ViewAgreement.Table.Copy().DefaultView
                    DV.RowFilter = "AGREEMENT_NO = '" & Me.MultiColumnCombo1.Value.ToString() & "'"
                    For i As Integer = 0 To DV.Count - 1
                        If Not DistriButorIDs.Contains(DV(i)("DISTRIBUTOR_ID").ToString()) Then
                            DistriButorIDs.Add(DV(i)("DISTRIBUTOR_ID").ToString())
                        End If
                    Next
                    Dim Message As String = ""
                    If Not isTransitionTime Then
                        If Me.clsAgInclude.IsExistItemBrandInOtherAgreement(Me.Brand_ID, DistriButorIDs, Message) = True Then
                            Me.baseTooltip.Show(Message, Me.txtBrandName, 2500) : Return
                        End If
                    End If

                    Me.clsAgInclude.BrandID = Me.Brand_ID
                    If String.IsNullOrEmpty(Me.NewAgree_Brand_ID) Then
                        Me.clsAgInclude.Agree_Brand_ID = Me.MultiColumnCombo1.Value.ToString().TrimStart().TrimEnd()
                        Me.clsAgInclude.Agree_Brand_ID &= Me.Brand_ID
                    End If
                    'Me.fillAgreebrandIDinGridProgressive()

                    If Not IsNothing(Me.clsAgInclude.getDsPeriod()) Then
                        If (Me.clsAgInclude.getDsPeriod.HasChanges()) Then
                            Me.clsAgInclude.SaveBrandInclude(Me.QS_FLAG, Me.clsAgInclude.getDsPeriod.GetChanges(), Me.ds4MPeriode, IsRoundUp, HasChangedPrev)
                        Else
                            Me.clsAgInclude.SaveBrandInclude(Me.QS_FLAG, , Me.ds4MPeriode, IsRoundUp, HasChangedPrev)
                        End If
                    Else
                        Me.clsAgInclude.SaveBrandInclude(Me.QS_FLAG, , Me.ds4MPeriode, IsRoundUp, HasChangedPrev)
                    End If
                Case SaveMode.Update
                    Me.clsAgInclude.Agree_Brand_ID = Me.AGREE_BRAND_ID
                    Me.clsAgInclude.Comb_Agree_Brand_ID = Me.GridEX1.GetValue("COMBINED_BRAND")
                    Me.clsAgInclude.BrandID = Me.Brand_IDHide
                    Dim BRANDPACKIDS As New Collection()
                    Dim X As Integer = 1
                    For i As Integer = 0 To Me.grdunAddedBrandPack.RecordCount - 1
                        Me.grdunAddedBrandPack.MoveTo(i)
                        If Me.grdunAddedBrandPack.GetRow().CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                            BRANDPACKIDS.Add(Me.grdunAddedBrandPack.GetValue("BRANDPACK_ID").ToString(), X)
                            X += 1
                        End If
                    Next
                    If (BRANDPACKIDS.Count > 0) Then
                        If Not IsNothing(Me.clsAgInclude.getDsPeriod()) Then
                            'Me.fillAgreebrandIDinGridProgressive()
                            If (Me.clsAgInclude.getDsPeriod.HasChanges()) Then
                                Me.clsAgInclude.UpdateBrandInclude(Me.QS_FLAG, Me.clsAgInclude.getDsPeriod.GetChanges(), Me.ds4MPeriode, BRANDPACKIDS, IsRoundUp, HasChangedPrev)
                            Else
                                Me.clsAgInclude.UpdateBrandInclude(Me.QS_FLAG, , Me.ds4MPeriode, BRANDPACKIDS, IsRoundUp, HasChangedPrev)
                            End If
                        Else
                            Me.clsAgInclude.UpdateBrandInclude(Me.QS_FLAG, , Me.ds4MPeriode, BRANDPACKIDS, IsRoundUp, HasChangedPrev)
                        End If
                    Else
                        If Not IsNothing(Me.clsAgInclude.getDsPeriod()) Then
                            'Me.fillAgreebrandIDinGridProgressive()
                            If (Me.clsAgInclude.getDsPeriod.HasChanges()) Then
                                Me.clsAgInclude.UpdateBrandInclude(Me.QS_FLAG, Me.clsAgInclude.getDsPeriod.GetChanges(), Me.ds4MPeriode, , IsRoundUp, HasChangedPrev)
                            Else
                                Me.clsAgInclude.UpdateBrandInclude(Me.QS_FLAG, , Me.ds4MPeriode, , IsRoundUp, HasChangedPrev)
                            End If
                        Else
                            Me.clsAgInclude.UpdateBrandInclude(Me.QS_FLAG, , Me.ds4MPeriode, , IsRoundUp, HasChangedPrev)
                        End If
                    End If
            End Select
            Me.MultiColumnCombo1_ValueChanged(Me.MultiColumnCombo1, New EventArgs())
            Me.SFG = StateFillingGrid.Filling
            Me.GridEX1.Row = IndexRow
            Me.GridEX1.SelectCurrentCellText()
            Me.SFG = StateFillingGrid.HasFilled
            Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_SavingChanges1_btnSaveClick")
        Finally
            Me.ReadAccecs()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " COMBO BOX "
    Private Sub cmbSeconBrand_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSeconBrand.SelectedIndexChanged
        Try
            If Me.HFC = HasFilledComboFirstSecond.Filling Then : Return : End If
            If (Me.MultiColumnCombo1.Text = "") Or (Me.MultiColumnCombo1.SelectedIndex = -1) Then : Return : End If
            If Me.cmbSeconBrand.DataSource Is Nothing Then : Return : End If
            If Me.cmbSeconBrand.SelectedIndex = -1 Then : Return : End If
            If Me.lblFirstBrand.Text = Me.cmbSeconBrand.Text Then : Return : End If
            If Me.ShowConfirmedMessage(Me.MessageUpdateData) = Windows.Forms.DialogResult.No Then : Return : End If
            Dim Brand_ID As String = Me.cmbSeconBrand.SelectedValue.ToString()
            'tentukan agree_brand_id dengan function GetAGBrandIDInComboFirstSecond
            Dim CABID As String = Me.MultiColumnCombo1.Text + "" + Brand_ID 'Me.clsAgInclude.GetAGBrandIDInComboFirstSecond(Brand_ID, Me.MultiColumnCombo1.Text)
            Me.clsAgInclude.UpdateCombinedBrand(Me.MultiColumnCombo1.Text, Me.AGREE_BRAND_ID, CABID, _
            False, Me.GridEX1.GetValue("BRAND_NAME").ToString().Contains("ROUNDUP"))
            Me.ShowMessageInfo(Me.MessageSavingSucces)
            Me.btnRefReshCombined_BtnClick(Me.btnRefReshCombined, New System.EventArgs())
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_cmbSeconBrand_SelectedIndexChanged")
        End Try
    End Sub

#End Region

#Region " ContextMenu "
    Private Sub InsertToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsertToolStripMenuItem.Click
        Try
            Me.grdunAddedBrandPack_KeyDown(Me.grdunAddedBrandPack, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        Try
            Me.grdunAddedBrandPack.CheckAllRecords()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UnchekAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnchekAllToolStripMenuItem.Click
        Try
            Me.grdunAddedBrandPack.UnCheckAllRecords()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mnuClearData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClearData.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'check apakah datagrid diseleck atau tidak
            If Not (Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please select Brand !" & vbCrLf & "System can not find the brand.") : Return
            End If
            'cek apakah data sudah ada di data di database
            'bila ada delete berdasarkan AGREE_BRAND_ID yang di pilih,
            If Not Me.clsAgInclude.getDsPeriod.HasChanges Then
                Me.ShowMessageInfo("No changes have been made") : Return
            End If
            If Not NufarmBussinesRules.User.UserLogin.IsAdmin Then
                If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AgreementRelation Then
                    Me.ShowMessageInfo("Sorry, you can not do such operation") : Return
                End If
            End If
            Me.clsAgInclude.Flag = Me.QS_FLAG
            Dim AGREE_BRAND_ID As String = Me.GridEX1.GetValue("ID").ToString()
            If (Me.clsAgInclude.CountProgDisc(AGREE_BRAND_ID) > 0) Then
                'CHECK APAKAH DATA SUDAH DI GENERATE BERDASARKAN AGREE_BRAND_ID 
                If (Me.clsAgInclude.HasgeneratedDiscount(AGREE_BRAND_ID, Me.QS_FLAG)) Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    Return
                End If
                If (Me.clsAgInclude.HasgeneratedDiscount(AGREE_BRAND_ID, "Y")) Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    Return
                End If
                Dim IsRoundUP As Boolean = Me.GridEX1.GetValue("BRAND_NAME").ToString().StartsWith("ROUNDUP")
                Me.clsAgInclude.DeleteAgreeProgDiscount(Me.clsAgInclude.getDsPeriod, AGREE_BRAND_ID, Me.QS_FLAG, True)
            Else
                If (Not IsNothing(Me.clsAgInclude.getDsPeriod())) Then
                    Me.clsAgInclude.getDsPeriod().RejectChanges()
                End If
            End If
            If Not IsNothing(Me.clsAgInclude.getDsPeriod()) Then
                If Not Me.SFG = StateFillingGrid.Filling Then
                    Me.SFG = StateFillingGrid.Filling
                End If
                Me.clearGivenProgressive()
                For Each table As DataTable In Me.clsAgInclude.getDsPeriod().Tables
                    table.Rows.Clear()
                Next
                Me.dgvPeriodicVal.Rows.Clear()
                Me.dgvPeriodicVal.Rows.Clear()
                Me.dgvYearly.Rows.Clear()
                Me.dgvYearlyVal.Rows.Clear()
                If (Me.QS_FLAG = "S") Then
                    Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableSemesterly()
                    Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableSemesterlyV()
                ElseIf (Me.QS_FLAG = "Q") Then
                    Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableQuarterly()
                    Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableQuarterlyV()
                End If
                Me.dgvYearly.DataSource = Me.clsAgInclude.GetTableYearly()
                Me.dgvYearlyVal.DataSource = Me.clsAgInclude.GetTableYearlyV()
                Me.grpPotensi.Enabled = False
            End If
            Me.FormatGridDiscount(Me.dgvPeriodic)
            Me.FormatGridDiscount(Me.dgvPeriodicVal)
            Me.FormatGridDiscount(Me.dgvYearly)
            Me.FormatGridDiscount(Me.dgvYearlyVal)
            Me.SFG = StateFillingGrid.HasFilled
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mnuClearData_Click")
            Me.SFG = StateFillingGrid.HasFilled
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub mnuSaveGridDiscount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveGridDiscount.Click
        'check apakah data ada perubahan di dataset'
        'bila ada simpan perubahan tersebut'
        'check focus

        If Not (Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record) Then
            Me.ShowMessageInfo("Please select Brand !" & vbCrLf & "System can not find the brand.") : Return
        End If
        If Not NufarmBussinesRules.User.UserLogin.IsAdmin Then
            If Not NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AgreementRelation Then
                Me.ShowMessageInfo("Sorry, you can not do such operation") : Return
            End If
        End If
        Try
            Me.Cursor = Cursors.WaitCursor
            'Dim IndexRow As Integer = Me.GridEX1.Row
            Me.SavingChanges1_btnSaveClick(Me.SavingChanges1.btnSave, New EventArgs())
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mnuSaveGridDiscount_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

#End Region

#Region " Chekbox "
    Private Sub chkTypical_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTypical.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If (Me.chkTypical.Checked = True) Then
                If Me.IsValid(ActiveTab.BrandInclude, True) = False Then
                    Me.chkTypical.Checked = False
                    Return
                End If
                'create dataset
                If Not Me.SFG = StateFillingGrid.Filling Then
                    Me.SFG = StateFillingGrid.Filling
                End If
                Dim AGREE_BRAND_ID As String = ""
                If (Me.Mode = SaveMode.Save) Then
                    AGREE_BRAND_ID = Me.MultiColumnCombo1.Value.ToString() + "" + Me.Brand_ID.ToString()
                Else
                    AGREE_BRAND_ID = Me.MultiColumnCombo1.Value.ToString() + "" + Me.Brand_IDHide
                End If
                Me.clsAgInclude.GetDsSetPeriod(AGREE_BRAND_ID, Me.QS_FLAG)
                If (Me.QS_FLAG = "S") Then
                    Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableSemesterly()
                    Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableSemesterlyV()
                ElseIf (Me.QS_FLAG = "Q") Then
                    Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableQuarterly()
                    Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableQuarterlyV()
                End If
                Me.dgvYearly.DataSource = Me.clsAgInclude.GetTableYearly()
                Me.dgvYearlyVal.DataSource = Me.clsAgInclude.GetTableYearlyV()
                Me.dgvPeriodic.Enabled = True : Me.dgvYearly.Enabled = True
                Me.dgvPeriodicVal.Enabled = True : Me.dgvYearlyVal.Enabled = True
                Me.grpPotensi.Enabled = True
                'CHEK APAKAH DATA SUDAH DI GENERATE
            Else
                If Not Me.SFG = StateFillingGrid.Filling Then
                    Me.SFG = StateFillingGrid.Filling
                End If
                If (Not IsNothing(Me.clsAgInclude.getDsPeriod)) Then
                    'CHECK DI DATABASE Apakah data sudah ada / belum
                    Dim AGREE_BRAND_ID As String = ""
                    If (Me.Mode = SaveMode.Save) Then
                        'AGREE_BRAND_ID = Me.MultiColumnCombo1.Value.ToString() + "" + Me.Brand_ID.ToString()
                    Else
                        AGREE_BRAND_ID = Me.MultiColumnCombo1.Value.ToString() + "" + Me.Brand_IDHide
                        If Me.clsAgInclude.CountProgDisc(AGREE_BRAND_ID) > 0 Then
                            If Me.clsAgInclude.HasgeneratedDiscount(AGREE_BRAND_ID, Me.QS_FLAG) = True Then
                                Me.ShowMessageInfo(Me.MessageDataCantChanged)
                                Return
                            Else
                                If (Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes) Then
                                    'DELETE DATA
                                    'Dim IsRoundUp As Boolean = Me.GridEX1.GetValue("BRAND_NAME").ToString().Contains("ROUNDUP")
                                    Me.clsAgInclude.DeleteAgreeProgDiscount(Me.clsAgInclude.getDsPeriod, AGREE_BRAND_ID, Me.QS_FLAG, False)
                                Else
                                    Me.chkTypical.Checked = True
                                    If Not Me.clsAgInclude.HasUsedGivenProgressive(Me.AGREE_BRAND_ID, True) Then
                                        Me.grpPotensi.Enabled = True
                                    Else
                                        Me.grpPotensi.Enabled = False
                                    End If
                                    Return
                                End If
                            End If
                        End If
                    End If
                    'clearkan text

                    Me.clearGivenProgressive()
                    For Each table As DataTable In Me.clsAgInclude.getDsPeriod().Tables
                        table.Rows.Clear()
                    Next
                    Me.clsAgInclude.getDsPeriod.AcceptChanges()
                    If (Me.QS_FLAG = "S") Then
                        Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableSemesterly()
                        Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableSemesterlyV()
                    ElseIf (Me.QS_FLAG = "Q") Then
                        Me.dgvPeriodic.DataSource = Me.clsAgInclude.GetTableQuarterly()
                        Me.dgvPeriodicVal.DataSource = Me.clsAgInclude.GetTableQuarterlyV()
                    End If
                    Me.dgvYearly.DataSource = Me.clsAgInclude.GetTableYearly()
                    Me.dgvYearlyVal.DataSource = Me.clsAgInclude.GetTableYearlyV()
                    Me.grpPotensi.Enabled = False
                End If
            End If
            'buat dataset untuk saving otomatis
            'ada isinya
            'check ke database apakah data sudah di generate / belum
            'bila sudah unabledkan datagrid untuk bisa di edit
            'bila belum enabledkan datagrid
            Me.FormatGridDiscount(Me.dgvPeriodic)
            Me.FormatGridDiscount(Me.dgvPeriodicVal)
            Me.FormatGridDiscount(Me.dgvYearly)
            Me.FormatGridDiscount(Me.dgvYearlyVal)
            Me.SFG = StateFillingGrid.HasFilled
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled
            Me.ShowMessageInfo(ex.Message)

            Me.LogMyEvent(ex.Message, Me.Name + "_chkTypical_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region
#End Region
  

    Private Sub tbPeriodicVal_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPeriodicVal.Enter
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        If Me.IsValid(ActiveTab.BrandInclude, True) = False Then
            Return
        End If
        If (Me.chkTypical.Checked = True) And (Me.chkTypical.Enabled = True) Then
            Me.dgvPeriodicVal.Enabled = True
            Me.dgvYearlyVal.Enabled = True
        Else
            Me.dgvPeriodicVal.Enabled = False
            Me.dgvYearlyVal.Enabled = False
        End If
    End Sub

    Private Sub dgvPeriodicVal_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPeriodicVal.CellValueChanged
        Try
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            If Not Me.Hload = HasLoad.Yes Then
                Return
            End If
            If Me.IsValid(ActiveTab.BrandInclude, True) = False Then
                Return
            End If
            ''FLAG COLUMN
            Me.SFG = StateFillingGrid.Filling
            If e.ColumnIndex = 2 Then
                If Not (Me.dgvPeriodicVal.Item(2, e.RowIndex).Value Is DBNull.Value) Then
                    If Me.dgvPeriodicVal.Item(2, e.RowIndex).Value = String.Empty Then
                        Me.ShowMessageInfo("you should either enter flag(S1//S2/Y/Q[1,2,3,4])")
                        Me.dgvPeriodicVal.CancelEdit()
                        Return
                    End If
                End If
            ElseIf e.ColumnIndex = 3 Then
                'DISCOUNT COLUMN
                If Not (Me.dgvPeriodicVal.Item(3, e.RowIndex).Value Is DBNull.Value) Then
                    If CInt(Me.dgvPeriodicVal.Item(3, e.RowIndex).Value) > 100 Then
                        Me.ShowMessageInfo("The value exceeds from 100")
                        Me.dgvPeriodicVal.CancelEdit()
                        Return
                    End If
                End If
            End If
            If Not (Me.dgvPeriodicVal.Item(2, e.RowIndex).Value Is Nothing) Or Not (Me.dgvPeriodicVal.Item(3, e.RowIndex).Value Is Nothing) Then
                If Me.Mode = SaveMode.Save Then
                    Me.dgvPeriodicVal.Item(1, e.RowIndex).Value = CObj(Me.NewAgree_Brand_ID)
                Else
                    Me.dgvPeriodicVal.Item(1, e.RowIndex).Value = CObj(Me.AGREE_BRAND_ID)
                End If
            End If
            Me.dgvPeriodicVal.CommitEdit(DataGridViewDataErrorContexts.Commit)
            Me.SFG = StateFillingGrid.HasFilled
            'set column 0(agreement no) dengan agrement no dan qsy_flag dengan melihat rdbqs checkchanged nya
        Catch ex As InvalidCastException
            Me.SFG = StateFillingGrid.HasFilled
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled
        End Try
    End Sub

    Private Sub dgvPeriodicVal_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvPeriodicVal.MouseClick
        If (Me.chkTypical.Checked = True) And (Me.chkTypical.Enabled = True) Then
            Me.dgvPeriodicVal.Enabled = True
        Else
            Me.dgvPeriodicVal.Enabled = False
        End If
    End Sub

    Private Sub dgvPeriodicVal_UserDeletedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvPeriodicVal.UserDeletedRow
        dgvPeriodicVal.CommitEdit(DataGridViewDataErrorContexts.RowDeletion)
    End Sub

    Private Sub dgvPeriodicVal_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvPeriodicVal.DataError
        Me.baseTooltip.Show("Please enter valid value" & vbCrLf & "If you want to delete row " & vbCrLf & "Please press delete key on the keyboard !.", Me.dgvPeriodicVal, 2500)
        Me.dgvPeriodicVal.CancelEdit()
    End Sub

    Private Sub dgvYearlyVal_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvYearlyVal.CellValueChanged
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        If Not Me.Hload = HasLoad.Yes Then
            Return
        End If
        If Me.IsValid(ActiveTab.BrandInclude, True) = False Then
            Return
        End If
        Me.SFG = StateFillingGrid.Filling
        Try
            If e.ColumnIndex = 3 Then
                'DISCOUNT COLUMN
                If Not (Me.dgvYearlyVal.Item(3, e.RowIndex).Value Is DBNull.Value) Then
                    If CInt(Me.dgvYearlyVal.Item(3, e.RowIndex).Value) > 100 Then
                        Me.ShowMessageInfo("The value exceeds 100")
                        Me.dgvYearlyVal.CancelEdit()
                        Return
                    End If
                End If
            End If
            If Not (Me.dgvYearlyVal.Item(3, e.RowIndex).Value Is Nothing) Then
                If Me.Mode = SaveMode.Save Then
                    Me.dgvYearlyVal.Item(1, e.RowIndex).Value = Me.NewAgree_Brand_ID
                Else
                    Me.dgvYearlyVal.Item(1, e.RowIndex).Value = Me.AGREE_BRAND_ID
                End If
                Me.dgvYearlyVal.Item(2, e.RowIndex).Value = "Y"
            End If
            Me.dgvYearlyVal.CommitEdit(DataGridViewDataErrorContexts.Commit)
            'set column 0(agreement no) dengan agrement no dan qsy_flag dengan melihat rdbqs checkchanged nya
            Me.SFG = StateFillingGrid.HasFilled
        Catch ex As InvalidCastException
            Me.ShowMessageError(ex.Message)
            dgvYearlyVal.CancelEdit()
            Me.SFG = StateFillingGrid.HasFilled
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.SFG = StateFillingGrid.HasFilled
            dgvYearlyVal.CancelEdit()
        End Try
    End Sub

    Private Sub dgvYearlyVal_UserDeletedRow(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvYearlyVal.UserDeletedRow
        Me.dgvYearlyVal.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub dgvYearlyVal_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvYearlyVal.MouseClick
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        If Me.IsValid(ActiveTab.BrandInclude, True) = False Then
            Return
        End If
        If (Me.chkTypical.Checked = True) And (Me.chkTypical.Enabled = True) Then
            Me.dgvYearlyVal.Enabled = True
        Else
            Me.dgvYearlyVal.Enabled = False
        End If
    End Sub

    Private Sub dgvYearlyVal_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvYearlyVal.DataError
        Me.baseTooltip.Show("Please enter valid value" & vbCrLf & "If you want to delete row " & vbCrLf & "Please press delete key on the keyboard !.", Me.dgvYearlyVal, 2500)
        Me.dgvYearlyVal.CancelEdit()
    End Sub

    Private Sub txtFMPFM1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFMPFM3.ValueChanged, txtFMPFM2.ValueChanged, txtFMPFM1.ValueChanged
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        If Me.SFM = StateFillingCombo.Filling Then : Return : End If
        If Me.Hload = HasLoad.NotYet Then : Return : End If
        Me.SFG = StateFillingGrid.Filling
        Dim C As Janus.Windows.GridEX.EditControls.NumericEditBox = CType(sender, Janus.Windows.GridEX.EditControls.NumericEditBox)
        If C.Value Is Nothing Or C.Value Is DBNull.Value Then
            Select Case C.Name
                Case "txtFMPFM1"
                    Me.txtFMPPL1.Value = 0
                Case "txtFMPFM2"
                    Me.txtFMPPL2.Value = 0
                Case "txtFMPFM3"
                    Me.txtFMPPL3.Value = 0
            End Select
        ElseIf C.Value <= 0 Then
            Select Case C.Name
                Case "txtFMPFM1"
                    Me.txtFMPPL1.Value = 0
                Case "txtFMPFM2"
                    Me.txtFMPPL2.Value = 0
                Case "txtFMPFM3"
                    Me.txtFMPPL3.Value = 0
            End Select
        Else
            Select Case C.Name
                Case "txtFMPFM1"
                    Me.txtFMPPL1.Value = IIf((Me.txtFMPFM1.Value > 0), Convert.ToDecimal(Me.txtFMP1.Value) - Convert.ToDecimal(Me.txtFMPFM1.Value), 0)
                Case "txtFMPFM2"
                    Me.txtFMPPL2.Value = IIf((Me.txtFMPFM2.Value > 0), Convert.ToDecimal(Me.txtFMP2.Value) - Convert.ToDecimal(Me.txtFMPFM2.Value), 0)
                Case "txtFMPFM3"
                    Me.txtFMPPL2.Value = IIf((Me.txtFMPFM3.Value > 0), Convert.ToDecimal(Me.txtFMP3.Value) - Convert.ToDecimal(Me.txtFMPFM3.Value), 0)
            End Select
        End If
        Me.SFG = StateFillingGrid.HasFilled
    End Sub

    Private Sub txtFMPPL1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFMPPL3.ValueChanged, txtFMPPL2.ValueChanged, txtFMPPL1.ValueChanged
        If Me.SFM = StateFillingCombo.Filling Then : Return : End If
        If Me.Hload = HasLoad.NotYet Then : Return : End If
        Me.SFG = StateFillingGrid.Filling
        Dim C As Janus.Windows.GridEX.EditControls.NumericEditBox = CType(sender, Janus.Windows.GridEX.EditControls.NumericEditBox)
        If C.Value Is Nothing Or C.Value Is DBNull.Value Then
            Select Case C.Name
                Case "txtFMPPL1"
                    If Me.txtFMPFM1.Value > 0 Then
                        Me.txtFMPFM1.Value = Me.txtFMP1.Value
                    End If
                Case "txtFMPPL2"
                    If Me.txtFMPFM2.Value > 0 Then
                        Me.txtFMPFM2.Value = Me.txtFMP2.Value
                    End If
                Case "txtFMPPL3"
                    If Me.txtFMPFM2.Value > 0 Then
                        Me.txtFMPFM2.Value = Me.txtFMP2.Value
                    End If
            End Select
        ElseIf C.Value <= 0 Then
            Select Case C.Name
                Case "txtFMPPL1"
                    If Me.txtFMPFM1.Value > 0 Then
                        Me.txtFMPFM1.Value = Me.txtFMP1.Value
                    End If
                Case "txtFMPPL2"
                    If Me.txtFMPFM2.Value > 0 Then
                        Me.txtFMPFM2.Value = Me.txtFMP2.Value
                    End If
                Case "txtFMPPL3"
                    If Me.txtFMPFM2.Value > 0 Then
                        Me.txtFMPFM2.Value = Me.txtFMP2.Value
                    End If
            End Select
        Else
            Select Case C.Name
                Case "txtFMPPL1"
                    Me.txtFMPFM1.Value = IIf(((Me.txtFMPFM1.Value > 0) And (txtFMPPL1.Value > 0)), Me.txtFMP1.Value - Me.txtFMPPL1.Value, 0)
                Case "txtFMPPL2"
                    Me.txtFMPFM2.Value = IIf(((Me.txtFMPFM2.Value > 0) And (txtFMPPL2.Value > 0)), Me.txtFMP2.Value - Me.txtFMPPL2.Value, 0)
                Case "txtFMPPL3"
                    Me.txtFMPFM3.Value = IIf(((Me.txtFMPFM3.Value > 0) And (txtFMPPL3.Value > 0)), Me.txtFMP3.Value - Me.txtFMPPL3.Value, 0)
            End Select
            Me.SFG = StateFillingGrid.HasFilled
        End If
    End Sub

    Private Sub txtFMP1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFMP3.ValueChanged, txtFMP2.ValueChanged, txtFMP1.ValueChanged
        Me.lblTargetFPY.Text = Me.GetTargetYear(Me.QS_FLAG)
    End Sub

    Private Sub GridEX2_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX2.AddingRecord
        Try
            If Me.SFG = StateFillingGrid.Filling Then
                Return
            End If

            'check validity
            If IsNothing(Me.GridEX2.GetValue("PRODUCT_CATEGORY")) Or IsDBNull(Me.GridEX2.GetValue("PRODUCT_CATEGORY")) Then
                Me.ShowMessageInfo("Please enter product category")
                e.Cancel = True
                Me.GridEX2.MoveToNewRecord()
                Me.GridEX2.Select()

            ElseIf IsNothing(Me.GridEX2.GetValue("PS_CATEGORY")) Or IsDBNull(Me.GridEX2.GetValue("PS_CATEGORY")) Then
                Me.ShowMessageInfo("Please enter Pack size group")
                e.Cancel = True
                Me.GridEX2.MoveToNewRecord()
                Me.GridEX2.Select()

            ElseIf IsNothing(GridEX2.GetValue("UP_TO_PCT")) Or IsDBNull(Me.GridEX2.GetValue("UP_TO_PCT")) Then
                Me.ShowMessageInfo("Please enter percent achievement")
                e.Cancel = True
                Me.GridEX2.MoveToNewRecord()
                Me.GridEX2.Select()
            ElseIf CDec(Me.GridEX2.GetValue("UP_TO_PCT")) <= 0 Then
                Me.ShowMessageInfo("Please enter percent achievement")
                e.Cancel = True
                Me.GridEX2.MoveToNewRecord()
                Me.GridEX2.Select()
            ElseIf IsNothing(Me.GridEX2.GetValue("DISC_PCT")) Or IsDBNull(Me.GridEX2.GetValue("DISC_PCT")) Then
                Me.ShowMessageInfo("Please enter Disc %")
                e.Cancel = True
                Me.GridEX2.MoveToNewRecord()
                Me.GridEX2.Select()
            ElseIf CDec(Me.GridEX2.GetValue("DISC_PCT")) <= 0 Then
                Me.ShowMessageInfo("Please enter Disc %")
                e.Cancel = True
                Me.GridEX2.MoveToNewRecord()
                Me.GridEX2.Select()
            ElseIf IsNothing(Me.GridEX2.GetValue("FLAG")) Or IsDBNull(Me.GridEX2.GetValue("FLAG")) Then
                Me.ShowMessageInfo("Please enter Flag")
                e.Cancel = True
                Me.GridEX2.MoveToNewRecord()
                Me.GridEX2.Select()
            End If
            'check IDrow
            Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView).ToTable().Copy().DefaultView()
            DV.Sort = "IDRow"
            Dim IDRow As String = Me.MultiColumnCombo1.Value + Me.GridEX2.GetValue("PRODUCT_CATEGORY") + Me.GridEX2.GetValue("PS_CATEGORY") + Me.GridEX2.GetValue("FLAG")
            If DV.Find(IDRow) > 0 Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
                e.Cancel = True
                Me.GridEX2.MoveToNewRecord()
                Me.GridEX2.Select()
            End If
            Cursor = Cursors.WaitCursor
            'set agreement_no,ach_methode,IDrow,createdby,CreatedDate
            Me.GridEX2.SetValue("HasRef", 0)
            Me.GridEX2.SetValue("AGREEMENT_NO", Me.MultiColumnCombo1.Value)
            Me.GridEX2.SetValue("ACH_METHODE", "PSG")
            Me.GridEX2.SetValue("IDRow", IDRow)
            Me.GridEX2.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName)
            Me.GridEX2.SetValue("CreatedDate", NufarmBussinesRules.SharedClass.ServerDate)
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            e.Cancel = True
        End Try
        Cursor = Cursors.Default
    End Sub

    'Private Sub GridEX2_CurrentCellChanging(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.CurrentCellChangingEventArgs) Handles GridEX2.CurrentCellChanging
    '    If Me.SFG = StateFillingGrid.Filling Then
    '        Return
    '    End If
    '    If Me.Hload = HasLoad.NotYet Then
    '        Return
    '    End If
    '    If Me.SFM = StateFillingCombo.Filling Then
    '        Return
    '    End If
    '    Try
    '        If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
    '            If Not e.Column.Key = "PS_CATEGORY" And Not e.Column.Key = "FLAG" And Not e.Column.Key = "PRODUCT_CATEGORY" Then
    '                Me.GridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
    '            Else
    '                If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AgreementRelation Then
    '                    Me.GridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
    '                Else
    '                    Me.GridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub GridEX2_UpdatingCell(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.UpdatingCellEventArgs) Handles GridEX2.UpdatingCell
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        Try
            If Me.GridEX2.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "PS_CATEGORY" Then
                    Dim ID As String = Me.MultiColumnCombo1.Value + Me.GridEX2.GetValue("PRODUCT_CATEGORY") + e.Value + Me.GridEX2.GetValue("FLAG")
                    Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                    Dim rowFilter As String = DV.RowFilter
                    DV.RowFilter = ""
                    Dim dummyDV As DataView = DV.ToTable().Copy().DefaultView()
                    dummyDV.Sort = "IDRow"
                    If dummyDV.Find(ID) >= 0 Then
                        Me.ShowMessageInfo(Me.MessageDataCantChanged)
                        e.Cancel = True
                    End If
                ElseIf e.Column.Key = "FLAG" Then
                    Dim ID As String = Me.MultiColumnCombo1.Value + Me.GridEX2.GetValue("PRODUCT_CATEGORY") + Me.GridEX2.GetValue("PS_CATEGORY") + e.Value
                    Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                    Dim rowFilter As String = DV.RowFilter
                    DV.RowFilter = ""
                    Dim dummyDV As DataView = DV.ToTable().Copy().DefaultView()
                    dummyDV.Sort = "IDRow"
                    If dummyDV.Find(ID) >= 0 Then
                        Me.ShowMessageInfo(Me.MessageDataCantChanged)
                        e.Cancel = True
                    End If
                ElseIf e.Column.Key = "PRODUCT_CATEGORY" Then
                    Dim ID As String = Me.MultiColumnCombo1.Value + e.Value + Me.GridEX2.GetValue("PS_CATEGORY") + Me.GridEX2.GetValue("FLAG")
                    Dim DV As DataView = CType(Me.GridEX2.DataSource, DataView)
                    Dim rowFilter As String = DV.RowFilter
                    DV.RowFilter = ""
                    Dim dummyDV As DataView = DV.ToTable().Copy().DefaultView()
                    dummyDV.Sort = "IDRow"
                    If dummyDV.Find(ID) >= 0 Then
                        Me.ShowMessageInfo(Me.MessageDataCantChanged)
                        e.Cancel = True
                    End If
                End If
                If CInt(Me.GridEX2.GetValue("HasRef")) > 0 Then
                    Me.ShowMessageInfo(Me.MessageDataCantChanged & vbCrLf & "Data already used in achievement")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
        End Try

    End Sub

    Private Sub GridEX2_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX2.DeletingRecord
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If

        Try

            If CInt(GridEX2.GetValue("HasRef")) > 0 Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                e.Cancel = True
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub GridEX2_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX2.RecordAdded
        Me.GridEX2.UpdateData()
    End Sub

    Private Sub GridEX2_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX2.RecordsDeleted
        Me.GridEX2.UpdateData()
    End Sub

    Private Sub btnDeleteF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteF.Click
        Try
            If (Me.MultiColumnCombo1.Text = "") Or (Me.MultiColumnCombo1.SelectedIndex = -1) Then
                Return
            End If
            If Me.COMB_BRAND_ID = "" Then
                Return
            End If
            'If (Me.txtComb2S1.Value = 0) Or (Me.txtComb2S2.Value = 0) Then
            '    Return
            'End If
            'konfirmasi ke user
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.clsAgInclude.UpdateCombinedBrand(Me.MultiColumnCombo1.Text, Me.AGREE_BRAND_ID, _
            Me.COMB_BRAND_ID, True, Me.GridEX1.GetValue("BRAND_NAME").ToString().Contains("ROUNDUP"))
            'update 'comb_brand_id jadi null'dua rows combined
            'rows yang satu yang brand_id yang berhubungan dengan agree_brand_id 
            'row yang ke dua brand_id yang berisi comb_agree_brand_id
            Me.ShowMessageInfo(Me.MessageSuccesDelete)
            Me.btnRefReshCombined_BtnClick(Me.btnRefReshCombined, New System.EventArgs())
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnDeleteQ1_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtCombF_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComb2F1.ValueChanged, txtComb2F3.ValueChanged, txtComb1F3.ValueChanged, txtComb2F2.ValueChanged, txtComb1F2.ValueChanged, txtComb1F1.ValueChanged
        If (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb1F1) Or _
           (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb1F2) Then
            Me.txtTotalComb1F1.Value = Me.txtComb1F1.Value + Me.txtComb1F2.Value
        ElseIf (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb2F1) Or _
               (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb2F2) Then
            Me.txtTotalComb2F2.Value = Me.txtComb2F1.Value + Me.txtComb2F2.Value
        ElseIf (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb1F3) Or _
            (DirectCast(sender, Janus.Windows.GridEX.EditControls.NumericEditBox) Is Me.txtComb2F3) Then
            Me.txtTotalComb3F3.Value = Me.txtComb1F3.Value + Me.txtComb2F3.Value
        End If
    End Sub
End Class
