Public Class Acceptance

#Region " Deklarasi "

    Private clsOA As NufarmBussinesRules.OrderAcceptance.Core
    'Private AD As Agreement_Discount
    'Private MD As Marketing_Discount
    Private SFG As StateFillingGrid
    Private SFM As StateFillingCombo
    Private SG As SelectedGrid
    Private HasLoad As Boolean
    Private AGREEMENT_NO As Object = Nothing
    Private DISTRIBUTOR_ID As String
    Private DISTRIBUTOR_NAME As String
    Friend CMain As Main = Nothing
#End Region

#Region " Enum "
    'Private Enum Agreement_Discount
    '    GivenDiscount
    '    QuarterlyDiscount
    '    SemesterlyDiscount
    'End Enum
    'Private Enum Marketing_Discount
    '    Given
    '    SteppingDiscount
    '    TargetDiscount
    'End Enum
    Private Enum StateFillingGrid
        Filling
        HasFilled
    End Enum

    Private Enum StateFillingCombo
        Filling
        HasFilled
    End Enum

    Private Enum SelectedGrid
        GridEx1
        GridEx2
        GridEx3
    End Enum

#End Region

#Region " Void "

    Private Function GetFilterDate() As String
        Dim filterdate As String = ""
        'DIM DateFrom as String = 
        If (Me.dtPicFrom.Text <> "") And (Me.dtpicUntil.Text <> "") Then
            filterdate = "OA_DATE >= #" & Me.dtPicFrom.Value.Month.ToString() & "/" & Me.dtPicFrom.Value.Day.ToString() & "/" & _
            Me.dtPicFrom.Value.Year.ToString & "# AND OA_DATE <= #" & Me.dtpicUntil.Value.Month.ToString() & "/" & Me.dtpicUntil.Value.Day.ToString() & "/" & _
            Me.dtpicUntil.Value.Year.ToString & "#"
            'Me.clsPO.ViewPORegistering.RowFilter = filterdate
        ElseIf Me.dtPicFrom.Text <> "" Then
            filterdate = "OA_DATE >= #" & Me.dtPicFrom.Value.Month.ToString() & "/" & Me.dtPicFrom.Value.Day.ToString() & "/" & _
            Me.dtPicFrom.Value.Year.ToString & "#"
            'Me.clsPO.ViewPORegistering.RowFilter = filterdate
        ElseIf Me.dtpicUntil.Text <> "" Then
            filterdate = "OA_DATE <= #" & Me.dtpicUntil.Value.Month.ToString() & "/" & Me.dtpicUntil.Value.Day.ToString() & "/" & _
            Me.dtpicUntil.Value.Year.ToString() & "#"
        End If
        Return filterdate
    End Function
    Private Sub ClearChecked()
        Me.rdbGiven.Checked = False
        Me.rdbMarketingStepping.Checked = False
        Me.rdbMarketingTarget.Checked = False
        Me.rdbGivenDiscount.Checked = False
        Me.rdbQuarterlyDiscount.Checked = False
        Me.rdbSemesterly.Checked = False
        Me.rdbYearlyDiscount.Checked = False
        Me.rdbGiven_CP.Checked = False
        Me.rdbGiven_DK.Checked = False
        Me.rdbGiven_PKPP.Checked = False
    End Sub
    Private Sub BindMultiColumnCombo(ByVal dtView As DataView)
        Me.SFM = StateFillingCombo.Filling
        Me.cmbDistributor.DataSource = dtView : Me.cmbDistributor.DisplayMember = ""
        Me.cmbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.cmbDistributor.DropDownList.RetrieveStructure()
        Me.cmbDistributor.DroppedDown = True
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.cmbDistributor.DropDownList.Columns
            col.AutoSize()
        Next
        Me.cmbDistributor.DroppedDown = False
        Me.SFM = StateFillingCombo.HasFilled
    End Sub
    Private Sub RefreshData()
        Me.SFM = StateFillingCombo.Filling
        Me.SFG = StateFillingGrid.Filling
        Me.clsOA = New NufarmBussinesRules.OrderAcceptance.Core()

        Me.clsOA.CreateViewDistributor()
        Me.BindMultiColumnCombo(Me.clsOA.ViewDistributor())
        'Me.cmbDistributor.SetDataBinding(Me.clsOA.ViewDistributor(), "")
    End Sub
    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OA_BranPack = True Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Acceptance = False Then
                Me.GridEX3.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            Else
                Me.GridEX3.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            End If
            If (NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OA_BranPack = True) Then
                Me.btnAddNew.Visible = True
            Else
                Me.btnAddNew.Visible = False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.OrderAcceptance = True Then
                Me.btnEditOA.Visible = True
            Else
                Me.btnEditOA.Visible = False
            End If
            'If NufarmBussinesRules.User.Privilege.ALLOW_VIEW.OA_BranPack = True Then
            '    Me.btnAddNew.Visible = True
            'Else
            '    Me.btnOABrandPack.Visible = False
            'End If
            'If Me.btnAddNew.VisibleSubItems <= 0 Then
            '    Me.btnAddNew.Visible = False
            'Else
            '    Me.btnAddNew.Visible = True
            'End If
        End If

    End Sub
    Friend Sub InitializeData()
        Me.RefreshData()
    End Sub

    Private Sub ResetDisplay()
        Me.GridEX1.RootTable.Caption = "DISTRIBUTOR_NAME : " & Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_NAME").ToString() '& " AGREEMENT_NO : " & Me.cmbDistributor.Value.ToString()
    End Sub

    Private Sub ClearBinding()
        Me.GridEX1.SetDataBinding(Nothing, "")
        Me.GridEX2.SetDataBinding(Nothing, "")
    End Sub

    Private Sub BindGrid(ByVal Grid As Janus.Windows.GridEX.GridEX, ByVal Ds As Object)
        Me.SFG = StateFillingGrid.Filling
        If Grid.Name = "GridEX2" Then
            If IsNothing(Ds) Then
                Grid.SetDataBinding(Ds, "")
            Else
                Dim dtView As DataView = CType(Ds, DataView)
                For i As Integer = 0 To dtView.Count - 1
                    If dtView(i)("TYPE").ToString() = "G" Then
                        dtView(i)("TYPE") = "Given"
                        dtView(i).EndEdit()
                    ElseIf dtView(i)("TYPE").ToString() = "Q1" Then
                        dtView(i)("TYPE") = "Qurterly 1"
                        dtView(i).EndEdit()
                    ElseIf dtView(i)("TYPE").ToString() = "Q2" Then
                        dtView(i)("TYPE") = "Quarterly 2"
                        dtView(i).EndEdit()
                    ElseIf dtView(i)("TYPE").ToString() = "Q3" Then
                        dtView(i)("TYPE") = "Quarterly 3"
                        dtView(i).EndEdit()
                    ElseIf dtView(i)("TYPE").ToString() = "Q4" Then
                        dtView(i)("TYPE") = "Quarterly 4"
                        dtView(i).EndEdit()
                    ElseIf dtView(i)("TYPE").ToString() = "MG" Then
                        dtView(i)("TYPE") = "GIVEN"
                        dtView(i).EndEdit()


                    ElseIf dtView(i)("TYPE").ToString() = "MS" Then
                        dtView(i)("TYPE") = "Stepping Discount"
                        dtView(i).EndEdit()
                    ElseIf dtView(i)("TYPE").ToString() = "T" Then
                        dtView(i)("TYPE") = "Target Discount"
                        dtView(i).EndEdit()
                    ElseIf dtView(i)("TYPE").ToString() = "S1" Then
                        dtView(i)("TYPE") = "Semesterly 1"
                        dtView(i).EndEdit()
                    ElseIf dtView(i)("TYPE").ToString() = "S2" Then
                        dtView(i)("TYPE") = "Semesterly 2"
                        dtView(i).EndEdit()
                    ElseIf dtView(i)("TYPE").ToString() = "P" Then
                        dtView(i)("TYPE") = "Project Discount"
                        dtView(i).EndEdit()
                    ElseIf dtView(i)("TYPE").ToString() = "y" Then
                        dtView(i)("TYPE") = "Yearly Discount"
                        dtView(i).EndEdit()
                    ElseIf dtView(i)("TYPE").ToString() = "O" Then
                        dtView(i)("TYPE") = "Other Discount"
                        dtView(i).EndEdit()
                    End If
                Next
                Grid.SetDataBinding(dtView, "")
            End If
        Else
            Grid.SetDataBinding(Ds, "")
        End If
        'Grid.Refresh()
        Me.SFG = StateFillingGrid.HasFilled
    End Sub
    'Private Sub ResetValueCombo(ByVal ValueMemeber As String, ByVal DisplayMember As String)
    '    Me.cmbDistributor.ValueMember = ValueMemeber
    '    Me.cmbDistributor.dis()
    'End Sub
    Private Sub BindComboBox(ByVal dtView As DataView, ByVal rowFilter As String)
        Me.SFM = StateFillingCombo.Filling
        If IsNothing(dtView) Then
            cmbDistributor.SetDataBinding(Nothing, "") : Me.SFM = StateFillingCombo.HasFilled : Return
        End If
        dtView.RowFilter = rowFilter
        Me.cmbDistributor.DataSource = dtView : Me.cmbDistributor.DropDownList.RetrieveStructure()
        Me.cmbDistributor.DisplayMember = "" : Me.cmbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.cmbDistributor.DroppedDown = True
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.cmbDistributor.DropDownList.Columns
            col.AutoSize()
        Next
        Me.cmbDistributor.DroppedDown = False
        'Me.cmbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        'Me.cmbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.SFM = StateFillingCombo.HasFilled
    End Sub

    Private Sub FormatGrid()
        'Parent(TABLE)
        'PARENT_ID:
        Me.GridEX2.RepeatHeaders = Janus.Windows.GridEX.InheritableBoolean.False
        Me.GridEX2.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
        Me.GridEX2.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True
        Me.GridEX2.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX2.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.True
        Me.GridEX2.RootTable.Columns("OA_BRANDPACK_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX2.RootTable.Columns("OA_BRANDPACK_ID").DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        Me.GridEX2.RootTable.Columns("OA_BRANDPACK_ID").Width = 170 '(Width = 170)
        Select Case Me.NavigationPane1.CheckedButton.Name
            Case "btnPnlAgreement"
                'Me.GridEX2.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.GridEX2.RootTable.Columns("PARENT_ID")))
                With Me.GridEX2.RootTable
                    '.Columns("PARENT_ID").Width = 270
                    '.Columns("PARENT_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    '.Columns("PARENT_ID").DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                    .Columns("AMOUNT_DISC_QTY").Width = 170 '(Width = 110)
                    .Columns("AMOUNT_DISC_QTY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .Columns("AMOUNT_DISC_QTY").DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                    '.Columns("AMOUNT_DISC_QTY").FormatString = "{0:G}"
                    .Columns("AMOUNT_DISC_QTY").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    .Columns("AMOUNT_DISC_QTY").TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                    .Columns("AMOUNT_DISC_QTY").TotalFormatString = "Total_Disc_Qty = {0:G}"

                    .Columns("AMOUNT_DISC_PRICE").Width = 180 '(Width = 200)
                    .Columns("AMOUNT_DISC_PRICE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    .Columns("AMOUNT_DISC_PRICE").TotalFormatString = "Total = {0:#,##0.00}"
                    .Columns("AMOUNT_DISC_PRICE").FormatString = "#,##0.00"
                    .Columns("AMOUNT_DISC_PRICE").TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                    .Columns("AMOUNT_DISC_PRICE").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .Columns("AMOUNT_DISC_PRICE").DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains

                    '.Columns("AMOUNT_DISC_QTY").TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat

                    '.Columns("AMOUNT_DISC_QTY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    '.Columns("AMOUNT_DISC_QTY").DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains

                    .Columns("DISCOUNT %").Width = 110
                    .Columns("DISCOUNT %").FormatString = "D"
                    .Columns("DISCOUNT %").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .Columns("DISCOUNT %").DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains

                    .Columns("RELEASE_QTY").Width = 200
                    .Columns("RELEASE_QTY").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    .Columns("RELEASE_QTY").TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                    .Columns("RELEASE_QTY").TotalFormatString = "Total_Release_Qty = {0:G}"
                    .Columns("RELEASE_QTY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .Columns("RELEASE_QTY").DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains

                    .Columns("AMOUNT_RELEASE_PRICE").Width = 180 '(Width = 200)
                    .Columns("AMOUNT_RELEASE_PRICE").FormatString = "#,##0.00"
                    .Columns("AMOUNT_RELEASE_PRICE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    .Columns("AMOUNT_RELEASE_PRICE").TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                    .Columns("AMOUNT_RELEASE_PRICE").TotalFormatString = "Total = {0:#,##0.00}" ''XX
                    .Columns("AMOUNT_RELEASE_PRICE").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .Columns("AMOUNT_RELEASE_PRICE").DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains

                    .Columns("LEFT_QTY").Width = 200
                    .Columns("LEFT_QTY").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    .Columns("LEFT_QTY").TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                    .Columns("LEFT_QTY").TotalFormatString = "Total_Left_Qty = {0:G}" '.AGREGATEFUNCTION = SUM
                    .Columns("LEFT_QTY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .Columns("LEFT_QTY").DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains

                    .Columns("AMOUNT_LEFT_PRICE").Width = 180 '(Width = 200)
                    .Columns("AMOUNT_LEFT_PRICE").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    .Columns("AMOUNT_LEFT_PRICE").FormatString = "#,##0.00"
                    .Columns("AMOUNT_LEFT_PRICE").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    .Columns("AMOUNT_LEFT_PRICE").TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                    .Columns("AMOUNT_LEFT_PRICE").TotalFormatString = "Total = {0:#,##0.00}"
                    .Columns("AMOUNT_LEFT_PRICE").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .Columns("AMOUNT_LEFT_PRICE").DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains

                    .Columns("PARENT_ID").Visible = False
                    .Columns("PARENT_ID").ShowInFieldChooser = False
                    .Columns("PARENT_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                End With
                'CHILD TABLE
                With Me.GridEX2.RootTable.ChildTables(0)
                    .TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
                    '.Columns("CHILD_ID").Width = 270 '(Width = 270)
                    '.Columns("CHILD_ID").Visible = False
                    '.Columns("CHILD_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    '.Columns("CHILD_ID").ShowInFieldChooser = False
                    .Columns("PRICE_PRQTY").Caption = "PRICE / QTY"
                    .Columns("PRICE_PRQTY").FormatString = "#,##0.00"
                    .Columns("PRICE_PRQTY").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable

                    '        
                    .Columns("AMOUNT_DISC_PRICE").Width = 180 '(Width = 120)
                    .Columns("AMOUNT_DISC_PRICE").FormatString = "#,##0.00"
                    .Columns("AMOUNT_DISC_PRICE").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                End With
            Case "btnPnlMarketingDiscount"
                With Me.GridEX2.RootTable
                    .Columns("PARENT_ID").Visible = False
                    .Columns("PARENT_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    .Columns("PARENT_ID").ShowInFieldChooser = False
                    'OA_BRANDPACK_ID
                    'AMOUNT_DISC_QTY
                    With Me.GridEX2.RootTable.Columns("AMOUNT_DISC_QTY")
                        .Width = 140 '(Width = 110)
                        .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total_Disc_Qty = {0:G}"
                    End With
                    'AMOUNT_DISC_PRICE
                    With Me.GridEX2.RootTable.Columns("AMOUNT_DISC_PRICE")
                        .Width = 180
                        .FormatString = "#,##0.00"
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total = {0:#,##0.00}"
                    End With
                    'RELEASE_QTY
                    With Me.GridEX2.RootTable.Columns("RELEASE_QTY")
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total_Release_Qty = {0:G}"
                    End With
                    'AMOUNT_RELEASE_PRICE
                    With Me.GridEX2.RootTable.Columns("AMOUNT_RELEASE_PRICE")
                        .Width = 180
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .FormatString = "#,##0.00"
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total = {0:#,##0.00}"
                    End With
                    'LEFT_QTY
                    With Me.GridEX2.RootTable.Columns("LEFT_QTY")
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total_Left_Qty = {0:G}"
                    End With
                    'AMOUNT LEFT_PRICE
                    With Me.GridEX2.RootTable.Columns("AMOUNT_LEFT_PRICE")
                        .Width = 180
                        .FormatString = "#,##0.00"
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total = {0:#,##0.00}"
                    End With
                    'CHILD TABLES
                    'CHILD_ID
                    'Me.GridEX2.RootTable.ChildTables(0).TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
                    'With Me.GridEX2.RootTable.ChildTables(0).Columns("CHILD_ID")
                    '    .Visible = False
                    '    .ShowInFieldChooser = False
                    '    .FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    'End With
                    'PRICE_PRQTY
                    With Me.GridEX2.RootTable.ChildTables(0).Columns("PRICE_PRQTY")
                        .Caption = "PRICE / QTY"
                        .Width = 110
                        .FormatString = "#,##0.00"
                    End With
                    'AMOUNT_DISC_PRICE
                    With Me.GridEX2.RootTable.ChildTables(0).Columns("AMOUNT_DISC_PRICE")
                        .Width = 120
                        .FormatString = "#,##0.00"
                    End With
                End With
            Case "btnProjectDiscount"
                With Me.GridEX2.RootTable
                    .Columns("PARENT_ID").Visible = False
                    .Columns("PARENT_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    .Columns("PARENT_ID").ShowInFieldChooser = False
                    'OA_BRANDPACK_ID
                    'AMOUNT DISC QTY
                    With Me.GridEX2.RootTable.Columns("AMOUNT_DISC_QTY")
                        .Width = 170
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total_Disc_Qty = {0:G}"
                    End With
                    'AMOUNT_DISC_PRICE
                    With Me.GridEX2.RootTable.Columns("AMOUNT_DISC_PRICE")
                        .Width = 180
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .FormatString = "#,##0.00"
                        .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total = {0:#,##0.00}"
                    End With
                    'RELEASE_QTY
                    With Me.GridEX2.RootTable.Columns("RELEASE_QTY")
                        .Width = 200
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total_Release_Qty = {0:G}"
                    End With
                    'AMOUNT_RELEASE_PRICE
                    With Me.GridEX2.RootTable.Columns("AMOUNT_RELEASE_PRICE")
                        .Width = 180
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .FormatString = "#,##0.00"
                        .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total = {0:#,##0.00}"
                    End With
                    'LEFT_QTY
                    With Me.GridEX2.RootTable.Columns("LEFT_QTY")
                        .Width = 190
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total_Left_Qty = {0:G}"
                    End With
                    'AMOUNT_LEFT_PRICE
                    With Me.GridEX2.RootTable.Columns("AMOUNT_LEFT_PRICE")
                        .Width = 180
                        .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                        .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                        .FormatString = "#,##0.00"
                        .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                        .TotalFormatString = "Total = {0:#,##0.00}"
                    End With
                    'CHILD TABLES
                    'CHILD_ID
                    'Me.GridEX2.RootTable.ChildTables(0).TotalRow = Janus.Windows.GridEX.InheritableBoolean.False
                    'With Me.GridEX2.RootTable.ChildTables(0).Columns("CHILD_ID")
                    '    .Visible = False
                    '    .ShowInFieldChooser = False
                    '    .FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    'End With
                    'PRICE_PRQTY
                    With Me.GridEX2.RootTable.ChildTables(0).Columns("PRICE_PRQTY")
                        .Caption = "PRICE / QTY"
                        .Width = 110
                        .FormatString = "#,##0.00"
                    End With
                    'AMOUNT_DISC_PRICE
                    With Me.GridEX2.RootTable.ChildTables(0).Columns("AMOUNT_DISC_PRICE")
                        .Width = 120
                        .FormatString = "#,##0.00"
                    End With
                End With
            Case "btnOtherDiscount"
                'DISC_QTY
                With Me.GridEX2.RootTable.Columns("DISC_QTY")
                    .Width = 180
                    .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                    .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                    .TotalFormatString = "Total_Disc_Qty = {0:G}"
                End With
                'PRICE_PRQTY
                With Me.GridEX2.RootTable.ChildTables(0).Columns("PRICE")
                    .Caption = "PRICE / QTY"
                    .Width = 110
                    .FormatString = "#,##0.00"
                    .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                End With
                'AMOUNT_DISC_PRICE
                With Me.GridEX2.RootTable.ChildTables(0).Columns("AMOUNT_DISC_PRICE")
                    .Width = 120
                    .FormatString = "#,##0.00"
                    .AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    .FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                    .TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat
                    .TotalFormatString = "Total = {0:#,##0.00}"
                End With
        End Select
    End Sub

#End Region

#Region " Event Procedure "

#Region " GRID EX 1 "

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            If Me.SFG = StateFillingGrid.Filling Then
                Return
            End If
            Me.GridEX2.SetDataBinding(Nothing, "")
            Me.GridEX2.RemoveFilters()
            If Me.NavigationPane1.CheckedButton.Name = "btnOtherDiscount" Then
                If Me.clsOA.ViewOtherDiscount().Count > 0 Then
                    If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        Me.BindGrid(Me.GridEX2, Me.clsOA.ViewOtherDiscount())
                        Dim FC As New Janus.Windows.GridEX.GridEXFilterCondition(Me.GridEX2.RootTable.Columns("OA_BRANDPACK_ID"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.GridEX1.GetValue("OA_BRANDPACK_ID"))
                        Me.GridEX2.RootTable.FilterCondition = FC : Me.GridEX2.RootTable.ApplyFilter(FC)
                    Else
                        Me.BindGrid(Me.GridEX2, Nothing)
                    End If
                Else
                    Me.BindGrid(Me.GridEX2, Nothing)
                End If
            Else
                If Me.clsOA.ViewOADiscount().Count > 0 Then
                    If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        Me.BindGrid(Me.GridEX2, Me.clsOA.ViewOADiscount())
                        Dim FC As New Janus.Windows.GridEX.GridEXFilterCondition(Me.GridEX2.RootTable.Columns("OA_BRANDPACK_ID"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.GridEX1.GetValue("OA_BRANDPACK_ID"))
                        Me.GridEX2.RootTable.FilterCondition = FC : Me.GridEX2.RootTable.ApplyFilter(FC)
                    Else
                        Me.BindGrid(Me.GridEX2, Nothing)
                    End If
                Else
                    Me.BindGrid(Me.GridEX2, Nothing)
                End If
            End If


        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Dim OA_BRANDPACK_ID As String = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
            If Me.clsOA.HasReferencedOABrandPack(OA_BRANDPACK_ID, True) = True Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                e.Cancel = True
                Me.GridEX1.Refetch()
                Me.GridEX1.SelectCurrentCellText()
                Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Return
            End If
            'e.Cancel = False
            Me.clsOA.DeleteOA_BRANDPACK(OA_BRANDPACK_ID)
            Dim DistributorID As Object = Me.cmbDistributor.Value
            Me.cmbDistributor.Value = Nothing
            Me.cmbDistributor.Value = DistributorID
            Me.ShowMessageInfo(Me.MessageSuccesDelete)
            'Me.GridEX1.UpdateData()
            'Me.GridEX1.Refetch()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.Enter
        Try
            Me.SG = SelectedGrid.GridEx1
            Me.GridEX1.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX1.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX1.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX1.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
            Me.GridEX1.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
            'ME.GridEX1.SelectedFormatStyle.ForeColor = SYSTEM.Drawing.Color.

            Me.GridEX2.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX2.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX2.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)


            Me.GridEX3.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX3.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX3.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)

            Dim S As Boolean = Me.FilterEditor1.Visible
            Me.FilterEditor1.SourceControl = Me.GridEX1
            Me.FilterEditor1.Visible = S
            'Me.FilterEditor1.SourceControl = Me.GridEX1
            'If Me.FilterEditor1.Visible = False Then
            '    Me.FilterEditor1.Visible = False
            'End If
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region " GRID EX 2 "

    Private Sub GridEX2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX2.Enter
        Try
            Me.SG = SelectedGrid.GridEx2
            Me.GridEX2.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX2.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX2.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX2.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
            Me.GridEX2.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText

            Me.GridEX1.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX1.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX1.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)

            Me.GridEX3.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX3.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX3.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)

            Dim S As Boolean = Me.FilterEditor1.Visible
            Me.FilterEditor1.SourceControl = Me.GridEX2
            Me.FilterEditor1.Visible = S
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region " GRID EX 3 "
    Private Sub GridEX3_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX3.DeletingRecord
        Try
            If Not Me.GridEX3.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                e.Cancel = True
                Return

            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Return
            End If
            Me.clsOA.DeleteOA(Me.GridEX3.GetValue("OA_REF_NO").ToString())
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX3_DeletingRecord")
        End Try
    End Sub

    Private Sub GridEX3_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX3.Enter
        Try
            Me.SG = SelectedGrid.GridEx2
            Me.GridEX3.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX3.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX3.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX3.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
            Me.GridEX3.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText

            Me.GridEX1.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX1.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX1.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)

            Me.GridEX2.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX2.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX2.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)

            Dim S As Boolean = Me.FilterEditor1.Visible
            Me.FilterEditor1.SourceControl = Me.GridEX3
            Me.FilterEditor1.Visible = S
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region " RADION BUTTONS "

    Private Sub rdbGivenDiscount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGivenDiscount.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbGivenDiscount.Checked = True Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "G", _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewAgreementDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeAgreementDiscount.Given, _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define Distributor.!")
                    Me.rdbGivenDiscount.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbGivenDiscount_CheckedChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbQuarterlyDiscount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbQuarterlyDiscount.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbQuarterlyDiscount.Checked = True Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "Q", _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewAgreementDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeAgreementDiscount.Quarterly, _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define distributor.!")
                    Me.rdbQuarterlyDiscount.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbQuarterlyDiscount_CheckedChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbSemesterly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSemesterly.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbSemesterly.Checked = True Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "S", Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewAgreementDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeAgreementDiscount.Semesterly, _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define distributor.!")
                    Me.rdbSemesterly.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbSemesterly_CheckedChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbGiven_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGiven.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbGiven.Checked = True Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "MG", _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.GivenDiscount, _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString())) '
                Else
                    Me.ShowMessageInfo("Please Define distributor.!")
                    Me.rdbGiven.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbGiven_CheckedChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    'Private Sub rdbMarketingStepping_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbMarketingStepping.CheckedChanged
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        If Me.rdbMarketingStepping.Checked = True Then
    '            If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
    '                Me.GridEX1.SetDataBinding(Nothing, "")
    '                Me.GridEX2.SetDataBinding(Nothing, "")
    '                Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "MS")
    '                Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
    '                Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
    '                Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.SteppingDiscount)
    '                'Me.BindGrid(Me.GridEX2, Me.clsOA.ViewOADiscount())
    '                'ElseIf Not IsNothing(Me.AGREEMENT_NO) Then
    '                '    Me.cmbDistributor.Value = Me.AGREEMENT_NO
    '                'Me.ShowMessageInfo("Please Define Agreement.!")
    '                'Me.rdbYearlyDiscount.Checked = False
    '                'Return
    '            Else
    '                Me.ShowMessageInfo("Please Define distributor.!")
    '                Me.rdbMarketingStepping.Checked = False
    '                Return
    '            End If
    '            'Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "S")
    '            'Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
    '            'Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.SteppingDiscount, _
    '            ' Me.cmbDistributor.Value.ToString())
    '            'Me.BindGrid(Me.GridEX2, Me.clsOA.ViewOADiscount())
    '            'Me.GridEX2.DataSource = Me.clsOA.ViewOADiscount()
    '            'If Me.clsOA.GetDataset.Tables(0).Rows.Count <= 0 Then
    '            '    Me.BindGrid(Me.GridEX2, Nothing)
    '            'Else
    '            '    Me.GridEX2.DataSource = Me.clsOA.GetDataset()
    '            '    Me.GridEX2.DataMember = "MARKETING_DISCOUNT_DETAIL"
    '            'Me.GridEX2.RetrieveStructure()
    '            '    Me.FormatGrid()
    '            'End If
    '            'Me.AGREEMENT_NO = Me.cmbDistributor.Value.ToString()
    '            Me.ResetDisplay()
    '        End If
    '    Catch ex As Exception
    '        Me.ShowMessageInfo(ex.Message)
    '        Me.LogMyEvent(Me.Name, +"_rdbMarketingStepping_CheckedChanged")
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    'Private Sub rdbMarketingTarget_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbMarketingTarget.CheckedChanged
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        If Me.rdbMarketingTarget.Checked = True Then
    '            If Not Me.cmbDistributor.SelectedItem Is Nothing Then
    '                Me.GridEX1.SetDataBinding(Nothing, "")
    '                Me.GridEX2.SetDataBinding(Nothing, "")
    '                Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "MT")
    '                Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
    '                Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
    '                Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.TargetDiscount)
    '                'Me.BindGrid(Me.GridEX2, Me.clsOA.ViewOADiscount())
    '                'Me.ShowMessageInfo("Please Define AGREEMENT_NO.!")
    '                'Me.rdbMarketingTarget.Checked = False
    '                'Return
    '                'ElseIf Not IsNothing(Me.AGREEMENT_NO) Then
    '                '    Me.cmbDistributor.Value = Me.AGREEMENT_NO
    '                'Me.clsOA.CreateViewOABrandPack(Me.AGREEMENT_NO, "T")
    '                'Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
    '                'Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.TargetDiscount, _
    '                ' Me.cmbDistributor.Value.ToString())
    '                'Me.BindGrid(Me.GridEX2, Me.clsOA.ViewOADiscount())
    '            Else
    '                Me.ShowMessageInfo("Please Define distributor.!")
    '                Me.rdbMarketingTarget.Checked = False
    '                Return
    '            End If
    '            'Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "T")
    '            'Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
    '            'Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_ID").ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.TargetDiscount, _
    '            ' Me.cmbDistributor.Value.ToString())
    '            'Me.BindGrid(Me.GridEX2, Me.clsOA.ViewOADiscount())
    '            'Me.GridEX2.DataSource = Me.clsOA.ViewOADiscount()
    '            'If Me.clsOA.GetDataset.Tables(0).Rows.Count <= 0 Then
    '            '    Me.BindGrid(Me.GridEX2, Nothing)
    '            'Else
    '            '    Me.GridEX2.DataSource = Me.clsOA.GetDataset()
    '            '    Me.GridEX2.DataMember = "MARKETING_DISCOUNT_DETAIL"
    '            'Me.GridEX2.RetrieveStructure()
    '            '    Me.FormatGrid()
    '            'End If
    '            'Me.AGREEMENT_NO = Me.cmbDistributor.Value.ToString()
    '            Me.ResetDisplay()
    '        End If
    '    Catch ex As Exception
    '        Me.ShowMessageInfo(ex.Message)
    '        Me.LogMyEvent(ex.Message, Me.Name + "_rdbMarketingTarget_CheckedChanged")
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub rdbYearlyDiscount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbYearlyDiscount.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbYearlyDiscount.Checked = True Then
                If Not Me.cmbDistributor.SelectedItem Is Nothing Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "Y", _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewAgreementDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeAgreementDiscount.Yearly, _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define distributor.!")
                    Me.rdbYearlyDiscount.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ rdbYearlyDiscount_CheckedChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbGiven_CP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGiven_CP.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbGiven_CP.Checked = True Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "CP", _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.Given_CP, _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define Distributor.!")
                    Me.rdbGiven_CP.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbGiven_CP_CheckedChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbGiven_PKPP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGiven_PKPP.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbGiven_PKPP.Checked = True Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "KP", Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.Given_PKPP, _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define Distributor.!")
                    Me.rdbGiven_PKPP.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbGiven_PKPP_CheckedChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbGiven_DK_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGiven_DK.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbGiven_DK.Checked = True Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "DK", Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.Given_DK, _
                    Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define Distributor.!")
                    Me.rdbGiven_DK.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbGiven_DK_CheckedChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbGivenCPR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGivenCPR.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbGivenCPR.Checked = True Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "CR", Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    'Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.Given_CPR, _
                     Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define Distributor.!")
                    Me.rdbGivenCPR.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbGivenCPR_CheckedChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbSpecialCPD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSpecialCPD.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbSpecialCPD.Checked = True Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "CS", Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    'Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.Given_CPSD, _
                     Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define Distributor.!")
                    Me.rdbGivenCPR.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbSpecialCPD_CheckedChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " BUTTONS "

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            If Not IsNothing(Me.cmbDistributor.DataSource) Then
                Dim rowfilter As String = "DISTRIBUTOR_NAME LIKE '%" & Me.cmbDistributor.Text & "%'"
                Me.BindComboBox(Me.clsOA.ViewDistributor(), rowfilter)
                Dim itemCount As Integer = Me.cmbDistributor.DropDownList().RecordCount()
                Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyRange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.cmbDistributor.SelectedItem) Then
                If Not IsNothing(Me.clsOA.ViewOABrandPack()) Then
                    Me.NavigationPane1_ItemClick(Me.NavigationPane1, New System.EventArgs())
                End If
            Else
                Me.ShowMessageInfo("Please define distributor")
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnAplyRange_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " EVENT FORM "

    Private Sub Acceptance_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not IsNothing(Me.clsOA) Then
                Me.clsOA.Dispose(True)
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Acceptance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            For i As Integer = 1 To 6
                Me.NavigationPane1.ShowMoreButtons()
            Next
            Me.FilterEditor1.Visible = False
            Me.dtPicFrom.Value = NufarmBussinesRules.SharedClass.ServerDate
            Me.dtpicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
            'Me.NavigationPane1.Expanded = False
            Me.spliterOA.Expanded = False
        Catch ex As Exception
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Acceptance_Load")
        Finally
            Me.ReadAcces()
            Me.SFM = StateFillingCombo.HasFilled
            Me.SFG = StateFillingGrid.HasFilled
            Me.HasLoad = True
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
        End Try
    End Sub

#End Region

#Region " BAR "

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnRenameColumn"
                    Select Case Me.SG
                        Case SelectedGrid.GridEx1
                            Dim MC As New ManipulateColumn()
                            MC.ShowInTaskbar = False
                            MC.grid = Me.GridEX1
                            MC.FillcomboColumn()
                            MC.ManipulateColumnName = "Rename"
                            MC.TopMost = True
                            MC.Show(Me.Bar1, True)
                        Case SelectedGrid.GridEx2
                            Dim MC As New ManipulateColumn()
                            MC.ShowInTaskbar = False
                            MC.grid = Me.GridEX2
                            MC.FillcomboColumn()
                            MC.ManipulateColumnName = "Rename"
                            MC.TopMost = True
                            MC.Show(Me.Bar3, True)
                        Case SelectedGrid.GridEx3
                            Dim MC As New ManipulateColumn()
                            MC.ShowInTaskbar = False
                            MC.grid = Me.GridEX3
                            MC.FillcomboColumn()
                            MC.ManipulateColumnName = "Rename"
                            MC.TopMost = True
                            MC.Show(Me.spliterOA, True)
                    End Select
                Case "btnShowFieldChooser"
                    Select Case Me.SG
                        Case SelectedGrid.GridEx1
                            Me.GridEX1.ShowFieldChooser(Me)
                        Case SelectedGrid.GridEx2
                            Me.GridEX2.ShowFieldChooser(Me)
                        Case SelectedGrid.GridEx3
                            Me.GridEX3.ShowFieldChooser(Me)
                    End Select
                Case "btnSettingGrid"
                    Select Case Me.SG
                        Case SelectedGrid.GridEx1
                            Dim SetGrid As New SettingGrid()
                            SetGrid.Grid = Me.GridEX1
                            SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                            SetGrid.ShowDialog(Me)
                        Case SelectedGrid.GridEx2
                            Dim SetGrid As New SettingGrid()
                            Me.GridEXPrintDocument2.GridEX = Me.GridEX2
                            SetGrid.Grid = Me.GridEX2
                            SetGrid.GridExPrintDock = Me.GridEXPrintDocument2
                            SetGrid.ShowDialog(Me)
                        Case SelectedGrid.GridEx3
                            Dim SetGrid As New SettingGrid()
                            Me.GridEXPrintDocument2.GridEX = Me.GridEX3
                            SetGrid.Grid = Me.GridEX3

                            SetGrid.GridExPrintDock = Me.GridEXPrintDocument2
                            SetGrid.ShowDialog(Me)
                    End Select
                Case "btnPrint"
                    Select Case Me.SG
                        Case SelectedGrid.GridEx1
                            Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                            Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                            'Me.PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                            If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                                Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                            End If
                            If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Me.PrintPreviewDialog1.Document.Print()
                            End If
                        Case SelectedGrid.GridEx2
                            Me.GridEXPrintDocument2.GridEX = Me.GridEX2
                            Me.PrintPreviewDialog2.Document = Me.GridEXPrintDocument2
                            'Me.PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                            If Not IsNothing(Me.PageSetupDialog2.PageSettings) Then
                                Me.PrintPreviewDialog2.Document.DefaultPageSettings = Me.PageSetupDialog2.PageSettings
                            End If
                            If Me.PrintPreviewDialog2.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Me.PrintPreviewDialog2.Document.Print()
                            End If
                        Case SelectedGrid.GridEx3
                            Me.GridEXPrintDocument2.GridEX = Me.GridEX3
                            Me.PrintPreviewDialog2.Document = Me.GridEXPrintDocument2
                            'Me.PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                            If Not IsNothing(Me.PageSetupDialog2.PageSettings) Then
                                Me.PrintPreviewDialog2.Document.DefaultPageSettings = Me.PageSetupDialog2.PageSettings
                            End If
                            If Me.PrintPreviewDialog2.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Me.PrintPreviewDialog2.Document.Print()
                            End If
                    End Select
                Case "btnPageSettings"
                    Select Case Me.SG
                        Case SelectedGrid.GridEx1
                            Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                            Me.PageSetupDialog1.ShowDialog(Me)
                        Case SelectedGrid.GridEx2
                            Me.GridEXPrintDocument2.GridEX = Me.GridEX2
                            Me.PageSetupDialog2.Document = Me.GridEXPrintDocument2
                            Me.PageSetupDialog2.ShowDialog(Me)
                        Case SelectedGrid.GridEx3
                            Me.GridEXPrintDocument2.GridEX = Me.GridEX3
                            Me.PageSetupDialog2.Document = Me.GridEXPrintDocument2
                            Me.PageSetupDialog2.ShowDialog(Me)
                    End Select
                Case "btnCustomFilter"
                    'size non active 878; 394
                    Select Case Me.SG
                        Case SelectedGrid.GridEx1
                            Me.FilterEditor1.SourceControl = Me.GridEX1
                            Me.GridEX1.RemoveFilters()
                            Me.FilterEditor1.Visible = True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            Me.grpRangeDate.Enabled = False
                            Me.dtPicFrom.Text = ""
                            Me.dtpicUntil.Text = ""
                            If Not (Me.GridEX1.DataSource Is Nothing) Then
                                CType(Me.GridEX1.DataSource, DataView).RowFilter = ""
                                Me.GridEX1.Refetch()
                            End If
                        Case SelectedGrid.GridEx2
                            Me.FilterEditor1.SourceControl = Me.GridEX2
                            Me.GridEX2.RemoveFilters()
                            Me.FilterEditor1.Visible = True
                            Me.GridEX2.FilterMode = Janus.Windows.GridEX.FilterMode.None
                        Case SelectedGrid.GridEx3
                            Me.FilterEditor1.SourceControl = Me.GridEX3
                            Me.GridEX3.RemoveFilters()
                            Me.FilterEditor1.Visible = True
                            Me.GridEX3.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            Me.grpRangeDate.Enabled = False
                            Me.dtPicFrom.Text = ""
                            Me.dtpicUntil.Text = ""

                    End Select
                    Me.grpRangeDate.BringToFront()
                    Me.GridEX1.BringToFront()
                Case "btnFilterEqual"
                    Select Case Me.SG
                        Case SelectedGrid.GridEx1
                            Me.FilterEditor1.Visible = False
                            Me.GridEX1.RemoveFilters()
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.grpRangeDate.Enabled = True
                        Case SelectedGrid.GridEx2
                            Me.FilterEditor1.Visible = False
                            Me.GridEX2.RemoveFilters()
                            Me.GridEX2.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case SelectedGrid.GridEx3
                            Me.FilterEditor1.Visible = False
                            Me.GridEX3.RemoveFilters()
                            Me.GridEX3.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    End Select
                Case "btnAddNew"
                    Dim frmOABrandPack As New OA_BranPack()
                    frmOABrandPack.CMAin = Me.CMain
                    frmOABrandPack.InitializeData()
                    frmOABrandPack.ShowInTaskbar = False
                    frmOABrandPack.ShowDialog(Me)
                    Dim Distributor_ID As Object = Me.cmbDistributor.Value
                    Me.RefreshData()
                    Me.SFM = StateFillingCombo.HasFilled
                    Me.cmbDistributor.Value = Nothing
                    Me.cmbDistributor.Value = Distributor_ID
                    'Case "btnOABrandPack"

                Case "btnExport"
                    Select Case Me.SG
                        Case SelectedGrid.GridEx1
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.GridEX1
                                Me.GridEXExporter1.SheetName = "OA_BRANDPACK"
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Case SelectedGrid.GridEx2
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.GridEX2
                                Me.GridEXExporter1.SheetName = "DISCOUNT DETAIL"
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Case SelectedGrid.GridEx3
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.GridEX3
                                Me.GridEXExporter1.SheetName = "OA_SHIP_TO"
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                    End Select
                Case "btnRefresh"
                    Dim Distributor_ID As Object = Me.cmbDistributor.Value
                    Me.RefreshData()
                    Me.SFM = StateFillingCombo.HasFilled
                    Me.cmbDistributor.Value = Nothing
                    Me.cmbDistributor.Value = Distributor_ID

                Case "btnApplyEdit"
                    If Me.cmbOA.ControlText = "" Then
                        Me.ShowMessageInfo("Please Define OA")
                        Me.cmbOA.ControlText = ""
                        Return
                    End If
                    If Me.cmbDistributor.Value Is Nothing Then
                        Me.cmbOA.ControlText = ""
                        Me.ShowMessageInfo("Please Define Distributor")
                        Return
                    End If
                    If Me.cmbDistributor.SelectedItem Is Nothing Then
                        Me.ShowMessageInfo("Please Define Distributor")
                        Me.cmbOA.ControlText = ""
                        Return
                    End If
                    'CHECK OA 
                    If Me.clsOA.IsExistedOA(Me.cmbOA.ControlText) = False Then
                        Me.ShowMessageInfo("Please Define OA")
                        Return
                    End If
                    Dim OA As New OrderAcceptance()
                    OA.OA_ID = Me.cmbOA.ControlText
                    OA.Mode = OrderAcceptance.ModeSave.Update
                    OA.UM = OrderAcceptance.UpdateMode.FromOriginal
                    OA.DISTRIBUTOR_ID = Me.cmbDistributor.Value.ToString()
                    OA.DISTRIBUTOR_NAME = Me.cmbDistributor.DropDownList.GetValue("DISTRIBUTOR_NAME").ToString()
                    'OA.PO_REF_DATE = 
                    OA.InitializeData()
                    OA.txtOARef.Text = Me.cmbOA.ControlText
                    OA.txtOARef.ReadOnly = True : OA.btnDelete.Visible = True
                    OA.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
                    OA.ShowDialog(Me)
                    Dim Distributor_ID As Object = Me.cmbDistributor.Value
                    Me.RefreshData()
                    Me.SFM = StateFillingCombo.HasFilled
                    Me.cmbDistributor.Value = Nothing
                    Me.cmbDistributor.Value = Distributor_ID
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        Finally
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
            If Me.SFM = StateFillingCombo.Filling Then
                Me.SFM = StateFillingCombo.HasFilled
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " NAVIGATION PANE "

    Private Sub NavigationPane1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NavigationPane1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            'Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            If Not Me.cmbDistributor.SelectedItem Is Nothing Then
                Me.ClearChecked()
                Select Case Me.NavigationPane1.CheckedButton.Name
                    Case "btnPnlAgreement"
                        Me.GridEX1.SetDataBinding(Nothing, "")
                        Me.GridEX2.SetDataBinding(Nothing, "")
                        'Me.ClearChecked()
                        If Me.rdbGivenDiscount.Checked = True Then
                            Me.rdbGivenDiscount_CheckedChanged(Me.rdbGivenDiscount, New System.EventArgs())
                        ElseIf Me.rdbQuarterlyDiscount.Checked = True Then
                            Me.rdbQuarterlyDiscount_CheckedChanged(Me.rdbQuarterlyDiscount, New System.EventArgs())
                        ElseIf Me.rdbSemesterly.Checked = True Then
                            Me.rdbSemesterly_CheckedChanged(Me.rdbSemesterly, New System.EventArgs())
                        Else
                            Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), DBNull.Value, _
                            Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                            'Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                            Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                            Me.clsOA.CreateViewAgreementDiscount(Me.cmbDistributor.Value.ToString(), _
                            Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                            'me.clsOA.CreateViewAgreementDiscou
                        End If
                        Me.ClearChecked()
                        Me.GridEX1.RootTable.Caption = "DISTRIBUTOR_NAME : " & DISTRIBUTOR_NAME & "(OA_BRANDPACK)"
                    Case "btnPnlMarketingDiscount"
                        Me.GridEX1.SetDataBinding(Nothing, "")
                        Me.GridEX2.SetDataBinding(Nothing, "")
                        If Me.rdbGiven.Checked = True Then
                            Me.rdbGiven_CheckedChanged(Me.rdbGiven, New System.EventArgs())
                        ElseIf Me.rdbGiven_CP.Checked Then
                            Me.rdbGiven_CP_CheckedChanged(Me.rdbGiven_CP, New EventArgs())
                        ElseIf Me.rdbGiven_DK.Checked Then
                            Me.rdbGiven_DK_CheckedChanged(Me.rdbGiven_DK, New EventArgs())
                        ElseIf Me.rdbGiven_PKPP.Checked Then
                            Me.rdbGiven_PKPP_CheckedChanged(Me.rdbGiven_PKPP, New EventArgs())
                        ElseIf Me.rdbGivenCPR.Checked Then
                            Me.rdbGivenCPR_CheckedChanged(Me.rdbGivenCPR, New EventArgs())
                        ElseIf Me.rdbSpecialCPD.Checked Then
                            Me.rdbSpecialCPD_CheckedChanged(Me.rdbSpecialCPD, New EventArgs())
                        ElseIf Me.rdbCPMRT.Checked Then
                            Me.rdbCPMRT_CheckedChanged(Me.rdbCPMRT, New EventArgs())
                        ElseIf Me.rdbGivenDKN.Checked Then
                            Me.rdbGivenDKN_CheckedChanged(Me.rdbGivenDKN, New EventArgs())
                        ElseIf Me.rdbCPDAuto.Checked Then
                            Me.rdbCPDAuto_CheckedChanged(Me.rdbCPDAuto, New EventArgs())
                        Else
                            Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), DBNull.Value, _
                           Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                            Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                            Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                            Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                            Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                        End If
                        Me.ClearChecked()
                        Me.GridEX1.RootTable.Caption = "DISTRIBUTOR_NAME : " & DISTRIBUTOR_NAME & "(OA_BRANDPACK)"
                        'Case "btnProjectDiscount"
                        '    Me.GridEX1.SetDataBinding(Nothing, "")
                        '    Me.GridEX2.SetDataBinding(Nothing, "")
                        '    If IsNothing(Me.cmbDistributor.SelectedItem) Then
                        '        Me.ShowMessageInfo("Please Define distributor.!")
                        '        Return
                        '    End If
                        '    If Me.cmbDistributor.Value Is Nothing Then
                        '        Return
                        '    End If
                        '    Me.GridEX1.RootTable.Caption = "DISTRIBUTOR_NAME : " & DISTRIBUTOR_NAME & "(OA Project)"
                        '    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString())
                        '    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                        '    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                        '    Me.clsOA.CreateViewProjectDiscount(Me.cmbDistributor.Value.ToString())
                    Case "btnOtherDiscount"
                        Me.GridEX1.SetDataBinding(Nothing, "")
                        Me.GridEX2.SetDataBinding(Nothing, "")
                        If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                            Dim OtherType As String = "O"
                            If Me.rdbCBD.Checked Then
                                OtherType = "OCBD"
                            ElseIf Me.rdbDD.Checked Then
                                OtherType = "ODD"
                            ElseIf Me.rdbDR.Checked Then
                                OtherType = "ODR"
                            End If
                            Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), OtherType, _
                           Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))

                            'Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "O")
                            Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                            Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                            Me.clsOA.CreateViewOtherDiscount(Me.cmbDistributor.Value.ToString(), Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                            Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()), OtherType)
                        Else
                            Me.ShowMessageInfo("Please Define distributor.!")
                            'Me.rdbYearly.Checked = False
                            Return
                        End If
                        Me.GridEX1.RootTable.Caption = "DISTRIBUTOR_NAME : " & DISTRIBUTOR_NAME & "(OA_BRANDPACK)"
                End Select
                Me.clsOA.CreateViewOAShipTo(Me.cmbDistributor.Value.ToString(), Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                'Me.clsOA.ViewOAShipTo.RowFilter = Me.GetFilterDate()
                Me.BindGrid(Me.GridEX3, Me.clsOA.ViewOAShipTo())
                Me.spliterOA.Expanded = False
            Else
                Me.ShowMessageInfo("Please Define distributor.!")
                Me.rdbGivenDiscount.Checked = False
                Return
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_NavigationPane1_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " MULTICOLUMN COMBO "

    Private Sub cmbDistributor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFM = StateFillingCombo.Filling Then
                Return
            End If
            If Not IsNothing(Me.cmbDistributor.SelectedItem) Then
                Me.NavigationPane1_ItemClick(Me.NavigationPane1, New EventArgs())
                'Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                'Me.DISTRIBUTOR_ID = Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_ID")
                'Me.DISTRIBUTOR_NAME = Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_NAME")
                'Me.NavigationPane1_ItemClick(Me.NavigationPane1, New System.EventArgs())
            ElseIf Me.cmbDistributor.Text = "" Then
                Me.ClearBinding()
                'Me.cmbOA.Items.Clear()
                Return
            ElseIf Me.cmbDistributor.Value Is Nothing Then
                Me.ClearBinding()
                'Me.cmbOA.Items.Clear()
                Return
            Else
                Me.ClearBinding()
                'Me.cmbOA.Items.Clear()
                Return
            End If
            'CREATE OA TO BIND TO CMBOA
            Me.clsOA.CreateViewOA(Me.cmbDistributor.Value.ToString()) 'Me.cmbDistributor.DropDownList().GetValue("DISTRIBUTOR_ID").ToString())
            Me.cmbOA.Items.Clear()
            Me.cmbOA.ControlText = ""
            For i As Integer = 0 To Me.clsOA.ViewOA.Count - 1
                Me.cmbOA.Items.Add(Me.clsOA.ViewOA(i)("OA_ID"))
            Next
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_cmbDistributor_SelectedIndexChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " DATE TIME PICER "
    Private Sub dtPicFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicFrom.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.ShowMessageInfo("Can not make null StartDate" & vbCrLf & "You must define StarDate")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtpicUntil_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpicUntil.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.ShowMessageInfo("Can not make null EndDate" & vbCrLf & "You must define EndDate")
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#End Region

   
    Private Sub rdbCPMRT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCPMRT.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbCPMRT.Checked = True Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "CT", Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    'Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.Given_CPMRT, _
                     Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define Distributor.!")
                    Me.rdbGivenCPR.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbCPMRT_CheckedChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbGivenDKN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGivenDKN.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbGivenDKN.Checked = True Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "DN", Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    'Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.Given_DKN, _
                     Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define Distributor.!")
                    Me.rdbGivenDKN.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbGivenDKN_CheckedChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbCPDAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCPDAuto.CheckedChanged
        Try
            If Me.rdbCPDAuto.Checked Then
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.GridEX1.SetDataBinding(Nothing, "")
                    Me.GridEX2.SetDataBinding(Nothing, "")
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "CA", Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                  Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                    'Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewMarketingDiscount(Me.cmbDistributor.Value.ToString(), NufarmBussinesRules.OrderAcceptance.OADiscount.TypeMarketingDiscount.Given_CPDAuto, _
                     Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
                Else
                    Me.ShowMessageInfo("Please Define Distributor.!")
                    Me.rdbGivenDKN.Checked = False
                    Return
                End If
                Me.ResetDisplay()
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbCPDAuto_CheckedChanged")
            Me.ShowMessageInfo(ex.Message)
        End Try
    End Sub

    Private Sub rdbDD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDD.CheckedChanged
        If Me.rdbDD.Checked Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Me.GridEX1.SetDataBinding(Nothing, "")
                Me.GridEX2.SetDataBinding(Nothing, "")
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "ODD", _
                   Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))

                    'Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "O")
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewOtherDiscount(Me.cmbDistributor.Value.ToString(), Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()), "ODD")
                Else
                    Me.Cursor = Cursors.Default : Me.ShowMessageInfo("Please Define distributor.!")
                    'Me.rdbYearly.Checked = False
                    Return
                End If
                Me.GridEX1.RootTable.Caption = "DISTRIBUTOR_NAME : " & DISTRIBUTOR_NAME & "(OA_BRANDPACK)"
            Catch ex As Exception
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbDD_CheckedChanged")
                Me.ShowMessageInfo(ex.Message)
            End Try
            Me.Cursor = Cursors.Default
        End If


    End Sub

    Private Sub rdbDR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDR.CheckedChanged
        If Me.rdbDR.Checked Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Me.GridEX1.SetDataBinding(Nothing, "")
                Me.GridEX2.SetDataBinding(Nothing, "")
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "ODR", _
                   Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))

                    'Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "O")
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewOtherDiscount(Me.cmbDistributor.Value.ToString(), Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()), "ODR")
                Else
                    Me.Cursor = Cursors.Default : Me.ShowMessageInfo("Please Define distributor.!")
                    'Me.rdbYearly.Checked = False
                    Return
                End If
                Me.GridEX1.RootTable.Caption = "DISTRIBUTOR_NAME : " & DISTRIBUTOR_NAME & "(OA_BRANDPACK)"
            Catch ex As Exception
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbDR_CheckedChanged")
                Me.ShowMessageInfo(ex.Message)
            End Try
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub rdbCBD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCBD.CheckedChanged
        If Me.rdbCBD.Checked Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Me.GridEX1.SetDataBinding(Nothing, "")
                Me.GridEX2.SetDataBinding(Nothing, "")
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "OCBD", _
                   Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))

                    'Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "O")
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewOtherDiscount(Me.cmbDistributor.Value.ToString(), Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()), "OCBD")
                Else
                    Me.Cursor = Cursors.Default : Me.ShowMessageInfo("Please Define distributor.!")
                    'Me.rdbYearly.Checked = False
                    Return
                End If
                Me.GridEX1.RootTable.Caption = "DISTRIBUTOR_NAME : " & DISTRIBUTOR_NAME & "(OA_BRANDPACK)"
            Catch ex As Exception
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbCBD_CheckedChanged")
                Me.ShowMessageInfo(ex.Message)
            End Try
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub rdbUncategorized_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbUncategorized.CheckedChanged
        If Me.rdbUncategorized.Checked Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Me.GridEX1.SetDataBinding(Nothing, "")
                Me.GridEX2.SetDataBinding(Nothing, "")
                If Not (Me.cmbDistributor.SelectedItem Is Nothing) Then
                    Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "O", _
                   Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))

                    'Me.clsOA.CreateViewOABrandPack(Me.cmbDistributor.Value.ToString(), "O")
                    Me.clsOA.ViewOABrandPack().RowFilter = Me.GetFilterDate()
                    Me.BindGrid(Me.GridEX1, Me.clsOA.ViewOABrandPack())
                    Me.clsOA.CreateViewOtherDiscount(Me.cmbDistributor.Value.ToString(), Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                    Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()), "O")
                Else
                    Me.Cursor = Cursors.Default : Me.ShowMessageInfo("Please Define distributor.!")
                    'Me.rdbYearly.Checked = False
                    Return
                End If
                Me.GridEX1.RootTable.Caption = "DISTRIBUTOR_NAME : " & DISTRIBUTOR_NAME & "(OA_BRANDPACK)"
            Catch ex As Exception
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbUncategorized_CheckedChanged")
                Me.ShowMessageInfo(ex.Message)
            End Try
            Me.Cursor = Cursors.Default
        End If
    End Sub
End Class
