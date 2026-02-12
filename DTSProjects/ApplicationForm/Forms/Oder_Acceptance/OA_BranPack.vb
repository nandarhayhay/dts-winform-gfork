Imports NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount

Public Class OA_BranPack

#Region " Deklarasi "
    Public CMAin As Main = Nothing
    Private HasLoad As Boolean
    Private SFM As StateFillingMCB
    Private SFG As StateFillingGrid
    Private Mode As ModeSave
    Public clsOADiscount As NufarmBussinesRules.OrderAcceptance.OADiscount
    Private AgreementDiscount As AgrementDiscountType
    Private MarketingDiscount As MarketingDiscountType
    Private SDiscount As SelectedDiscount
    Private SI_MCB As SelectedItemMCB
    Private FlagSemesterly As Semesterly
    Private FlagQuarterly As Quarterly
    Private OA_BRANDPACK_ID As String 'OA_BRANDPACK FROM 
    Private WithEvents QTY As Quantity
    Private QTY_MUST_GIVE As Integer 'qty must be release per OA
    Private OA_REF_NO As String
    Private PRICE_PRQTY As Decimal = 0
    Private WithEvents Other_QTY As Other_QTY
    Private WithEvents OtherDDDRCBD As OtherDDDR
    Private DDF As DataDragFrom
    Private WithEvents LftQTY As LeftQTY
    Private SSB As StateStyleButtonBar
    Private DDT As DataDragTo
    Private WithEvents EQTY As EditQTY
    Private QData As QueryData = QueryData.ItemRadioButton
    Private isPOProject As Boolean = False
#End Region

#Region " Enum "

    Private Enum StateFillingMCB
        Filling
        HasFilled
    End Enum

    Private Enum StateFillingGrid
        Filling
        HasFilled
        Disposing
        Disposed
    End Enum

    Private Enum SelectedItemMCB
        FromGrid
        FromMCB
        FromNone
    End Enum

    Private Enum ModeSave
        Save
        Update
    End Enum

    Private Enum SelectedDiscount
        None
        MarketingDiscount
        AgreementDiscount
        ProjectDiscount
        OtherDiscount

    End Enum

    Private Enum Semesterly
        S1
        S2
    End Enum

    Private Enum Quarterly
        Q1
        Q2
        Q3
        Q4
    End Enum

    Private Enum AgrementDiscountType
        GivenDiscount
        QuarterlyDiscount
        SemesterlyDiscount
        YearlyDiscount
    End Enum

    Private Enum MarketingDiscountType
        GivenDiscount
        TargetDiscount
        SteppingDiscount
    End Enum

    Private Enum DataDragFrom
        GridRemainding
        GridEx3
        GridEx2
    End Enum

    Private Enum StateStyleButtonBar
        FromClick
        FromSelected
    End Enum

    Private Enum DataDragTo
        GridEx2
        GridRemainding
    End Enum

    Private Enum QueryData
        ItemRadioButton
        GridSelected
        NavigationPanel
        None
    End Enum

    Private Enum FlagCPD
        CPDDistributor
        CPDTMDistributor
        CPDSDistributor
        CPDSDistributor_TM
        CPMRTDistributor
        CPMRTDsitributor_TM
    End Enum
#End Region

#Region " Void "

#Region " Generating Discount "

    'Private Sub GenerateAgreementGivenDiscount(ByVal OA_BRANDPACK_ID As String, ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal OA_ORIGINAL_QTY As Decimal)
    '    'Dim index As Integer = CType(Me.GridEX1.DataSource, DataView).Find(OA_BRANDPACK_ID)


    '    'Dim GridexDataview As DataView = CType(Me.GridEX1.DataSource, DataView)

    '    ''Dim Index As Integer = GridexDataview.Find(OA_BRANDPACK_ID)
    '    ''If Index <> -1 Then
    '    ''    With GridexDataview
    '    ''        Me.SFG = StateFillingGrid.Filling
    '    ''        .Item(Index)("AGREE_DISC_QTY") = CInt(.Item(Index)("AGREE_DISC_QTY")) + CInt(Me.clsOADiscount.DiscQty.AGreeGivenDiscQTY)
    '    ''        .Item(Index).EndEdit()
    '    ''        'Me.BindGridEx(Me.GridEX1, Me.clsOADiscount.ViewOABRANDPACK())

    '    ''        Me.SFG = StateFillingGrid.HasFilled
    '    ''        'Me.GridEX1.MoveTo(Index)
    '    ''    End With
    '    ''End If
    '    ''JIKA TIDAK MERAISE EVENT GRIDEXCURRENCELL_CHANGED
    '    ''HIDUPKAN EVENT DI BAWAH INI
    '    'Me.rdbGivenDiscount_CheckedChanged(Me.rdbGivenDiscount, New System.EventArgs())
    '    'Me.GridEX1.Refetch()
    '    'Me.GridEX1.ExpandGroups()
    '    'Me.GridEX1.MoveTo(index)
    'End Sub

    'Private Sub GenerateMarketingGivenDiscount(ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, _
    '    ByVal OA_ORIGINAL_QTY As Decimal, ByVal FLAG As String)
    '    Me.clsOADiscount.GenerateSalesGivenDiscount(OA_BRANDPACK_ID, BRANDPACK_ID, DISTRIBUTOR_ID, PO_REF_NO, FLAG, OA_ORIGINAL_QTY)
    'End Sub

#End Region

#Region " Lain "

#Region " Property "

    Private m_Devided_Qty As Decimal = 0
    Private m_Devide_Factor As Decimal
    Private m_Unit As String = ""
    Private m_DistributorID As String
    Private m_Distributor_Name As String
    Private Property Devided_Qty() As Decimal
        Get
            Return Me.m_Devided_Qty
        End Get
        Set(ByVal value As Decimal)
            Me.m_Devided_Qty = value
        End Set
    End Property
    Private Property Devide_Factor() As Decimal
        Get
            Return Me.m_Devide_Factor
        End Get
        Set(ByVal value As Decimal)
            Me.m_Devide_Factor = value
        End Set
    End Property
    Private Property Unit() As String
        Get
            Return Me.m_Unit
        End Get
        Set(ByVal value As String)
            Me.m_Unit = value
        End Set
    End Property
    Private Property DistributorID() As String
        Get
            Return Me.m_DistributorID
        End Get
        Set(ByVal value As String)
            Me.m_DistributorID = value
        End Set
    End Property
    Private Property DistribtorName() As String
        Get
            Return Me.m_Distributor_Name
        End Get
        Set(ByVal value As String)
            Me.m_Distributor_Name = value
        End Set
    End Property
#End Region

    Private Sub ReadAcces()
        If Not Me.CMAin.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OA_BranPack = True Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                Me.GridEX2.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                Me.GridEX3.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                Me.GridEX2.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                Me.GridEX3.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OrderAcceptance = True Then
                Me.btnNew.Visible = True
            Else
                Me.btnNew.Visible = False
            End If
            If (NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OA_BranPack = True) And (NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.OA_BranPack = True) Then
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OA_BranPack = True Then
                Me.ButtonEntry1.btnAddNew.Visible = True
                Me.ButtonEntry1.btnInsert.Visible = True
                Me.GridEX2.AllowDrop = True
                Me.btnGenerateAgreement.Visible = True
                Me.btnGenerateMarketing.Visible = True
                Me.btnOtherDiscount.Visible = True
                Me.btnNewGiven.Visible = True
                Me.btnGenerateAgreement.Visible = True
                Me.btnGenerateMarketing.Visible = True
                Me.btnGenerateOtherDiscount.Visible = True
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.OA_BranPack = True Then
                Me.ButtonEntry1.btnInsert.Visible = True
                Me.ButtonEntry1.btnAddNew.Visible = False
                Me.GridEX2.AllowDrop = False
                Me.btnGenerateAgreement.Visible = False
                Me.btnGenerateMarketing.Visible = False
                Me.btnOtherDiscount.Visible = False
                Me.btnNewGiven.Visible = True
                Me.btnGenerateAgreement.Visible = True
                Me.btnGenerateMarketing.Visible = True
                Me.btnGenerateOtherDiscount.Visible = True
            Else
                Me.ButtonEntry1.btnInsert.Visible = False
                Me.ButtonEntry1.btnAddNew.Visible = False
                Me.GridEX2.AllowDrop = False
                Me.btnGenerateAgreement.Visible = False
                Me.btnGenerateMarketing.Visible = False
                Me.btnOtherDiscount.Visible = False
                Me.btnNewGiven.Visible = False
                Me.btnGenerateMarketing.Visible = False
                Me.btnGenerateAgreement.Visible = False
                Me.btnGenerateOtherDiscount.Visible = False
            End If
        End If

    End Sub

    Private Function DeleteOA_BrandPack_Disc() As Integer
        Dim OA_BRANDPACK_DISC_QTY As Decimal = Convert.ToDecimal(Me.GridEX2.GetValue("DISC_QUANTITY"))
        Dim OA_BRANDPACK_ID As Object = Me.GridEX1.GetValue("OA_BRANDPACK_ID")
        'Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.BRANDPACK_ID = BRANDPACK_ID
        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.OA_BRANDPACK_ID = OA_BRANDPACK_ID
        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.BRANDPACK_ID = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
        Me.SFG = StateFillingGrid.Filling
        Dim index As Integer = 0
        Dim DiscountFrom As String = Me.GridEX2.GetValue("DISCOUNT_FROM").ToString()
        Try
            Dim GridExDataView As DataView = CType(Me.GridEX1.DataSource, DataView)
            If (Not IsNothing(Me.GridEX2.GetValue("AGREE_DISC_HIST_ID"))) And (Not (TypeOf (Me.GridEX2.GetValue("AGREE_DISC_HIST_ID")) Is DBNull)) Then
                If Not String.IsNullOrEmpty(Me.GridEX2.GetValue("AGREE_DISC_HIST_ID").ToString()) Then
                    Me.clsOADiscount.DeleteOA_BRANDPACK_DISC(CInt(Me.GridEX2.GetValue("OA_BRANDPACK_DISC_ID")), NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                                    OA_BRANDPACK_DISC_QTY, Me.GridEX2.GetValue("AGREE_DISC_HIST_ID"), , , False, OA_BRANDPACK_ID.ToString())
                End If
            End If
            If (IsNothing(Me.GridEX2.GetValue("MRKT_DISC_HIST_ID"))) Or (TypeOf (Me.GridEX2.GetValue("MRKT_DISC_HIST_ID")) Is DBNull) Then
            Else
                Me.clsOADiscount.DeleteOA_BRANDPACK_DISC(CInt(Me.GridEX2.GetValue("OA_BRANDPACK_DISC_ID")), NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                 OA_BRANDPACK_DISC_QTY, , Me.GridEX2.GetValue("MRKT_DISC_HIST_ID").ToString(), , False, OA_BRANDPACK_ID.ToString())
            End If
            If (IsNothing(Me.GridEX2.GetValue("PROJ_DISC_HIST_ID"))) Or (TypeOf (Me.GridEX2.GetValue("PROJ_DISC_HIST_ID")) Is DBNull) Then
            Else
                Me.clsOADiscount.DeleteOA_BRANDPACK_DISC(CInt(Me.GridEX2.GetValue("OA_BRANDPACK_DISC_ID")), NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.ProjectDiscount, _
                 OA_BRANDPACK_DISC_QTY, , , Me.GridEX2.GetValue("PROJ_DISC_HIST_ID").ToString(), False, OA_BRANDPACK_ID.ToString())
            End If
            If (IsNothing(Me.GridEX2.GetValue("BRND_B_S_ID"))) Or (TypeOf (Me.GridEX2.GetValue("BRND_B_S_ID")) Is DBNull) Then
            Else
                Me.clsOADiscount.DeleteOA_BRANDPACK_DISC(CInt(Me.GridEX2.GetValue("OA_BRANDPACK_DISC_ID")), NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                 OA_BRANDPACK_DISC_QTY, , , , False, Me.GridEX2.GetValue("OA_BRANDPACK_ID").ToString(), Me.GridEX2.GetValue("BRND_B_S_ID").ToString())
            End If

            If (IsNothing(Me.GridEX2.GetValue("MRKT_M_S_ID"))) Or (TypeOf (Me.GridEX2.GetValue("MRKT_M_S_ID")) Is DBNull) Then
            Else
                Me.clsOADiscount.DeleteOA_BRANDPACK_DISC(CInt(Me.GridEX2.GetValue("OA_BRANDPACK_DISC_ID")), NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                 OA_BRANDPACK_DISC_QTY, , , , False, Me.GridEX2.GetValue("OA_BRANDPACK_ID").ToString(), , , Me.GridEX2.GetValue("MRKT_M_S_ID").ToString())
            End If
            If IsNothing(Me.GridEX2.GetValue("ACHIEVEMENT_BRANDPACK_ID")) Or TypeOf (Me.GridEX2.GetValue("ACHIEVEMENT_BRANDPACK_ID")) Is DBNull Then
            Else
                Me.clsOADiscount.DeleteOA_BRANDPACK_DISC(CInt(Me.GridEX2.GetValue("OA_BRANDPACK_DISC_ID")), NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                OA_BRANDPACK_DISC_QTY, , , , False, Me.GridEX2.GetValue("OA_BRANDPACK_ID").ToString(), , _
                 Me.GridEX2.GetValue("ACHIEVEMENT_BRANDPACK_ID").ToString())
            End If
            If IsDBNull(Me.GridEX2.GetValue("GQSY_SGT_P_FLAG")) Then
                Me.ShowMessageInfo("Type Discount is unknown to process" & vbCrLf & "System cannot process the request.")
                Return -1
            ElseIf Not IsDBNull(Me.GridEX2.GetValue("OA_RM_ID")) Then
                Select Case Me.GridEX2.GetValue("DISCOUNT_FROM").ToString()
                    Case "GIVEN PKD"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "G"
                    Case "QUARTERLY 1"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "Q1"
                    Case "QUARTERLY 2"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "Q2"
                    Case "QUARTERLY 3"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "Q3"
                    Case "QUARTERLY 4"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "Q4"
                    Case "SEMESTERLY 1"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "S1"
                    Case "SEMESTERLY 2"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "S2"
                    Case "YEARLY DISC"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "Y"
                    Case "DPRD"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "MG"
                    Case "PKPP"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "KP"
                    Case "CP(D)_DIST"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "CP"
                    Case "CP(D)S_DIST"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "CS"
                    Case "CP(RD)"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "CR"
                    Case "DK"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "DK"
                    Case "DP"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "DP"
                        'Case "SALES_STEPPING"
                        '    Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "MS"
                        'Case "SALES_TARGET"
                        '    Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "MT"
                        'Case "PROJECT"
                        '    Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "P"
                    Case "OTHER DISC"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "O"
                    Case "REMAINDING_OA"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "RMOA"
                    Case "CP(D)S_DIST_TM"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "TS"
                    Case "CP(D)_DIST_TM"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "TD"
                    Case "CP(R M/T)_DIST_TM"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "CT"
                    Case "CP(R M/T)_DIST"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "CD"
                    Case "DK(N)"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "DN"
                    Case "CP(D)AUTO"
                        Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = "CA"
                End Select
                Me.clsOADiscount.DeleteOA_BRANDPACK_DISC(CInt(Me.GridEX2.GetValue("OA_BRANDPACK_DISC_ID")), _
                NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.None, _
                OA_BRANDPACK_DISC_QTY, , , , False, _
                 Me.GridEX2.GetValue("OA_BRANDPACK_ID").ToString(), , , , Me.GridEX2.GetValue("OA_RM_ID").ToString())
                '                Case "OCBD" : flag = "OTHER DISC(CBD)"
                'Case "ODD" : flag = "OTHER DISC(DD)"
                'Case "ODR" : flag = "OTHER DISC(DR"
            ElseIf DiscountFrom = "OTHER DISC" Or DiscountFrom = "OTHER DISC(CBD)" Or DiscountFrom = "OTHER DISC(DD)" Or DiscountFrom = "OTHER DISC(DR)" Or DiscountFrom = "OTHER DISC(DK)" Or DiscountFrom = "OTHER DISC(DP)" Then
                Dim dtview As DataView = DirectCast(Me.GridEX2.DataSource, DataView)
                For i As Integer = 0 To dtview.Count - 1
                    If dtview(i)("OA_BRANDPACK_ID").ToString() = OA_BRANDPACK_ID Then
                        If (CBool(dtview(i)("ISREMAINDING")) = True) And ((dtview(i)("GQSY_SGT_P_FLAG") = "O") Or (dtview(i)("GQSY_SGT_P_FLAG") = "OCBD") Or (dtview(i)("GQSY_SGT_P_FLAG") = "ODD") Or (dtview(i)("GQSY_SGT_P_FLAG") = "ODR") Or (dtview(i)("GQSY_SGT_P_FLAG") = "ODK") Or (dtview(i)("GQSY_SGT_P_FLAG") = "ODP")) Then
                            Me.ShowMessageInfo("Can not delete Parent Other because has child remainding other" & vbCrLf & "You must first delete child remainding other")
                            Return -1
                        End If
                    End If
                Next
                Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG = Me.GridEX2.GetValue("GQSY_SGT_P_FLAG").ToString()
                Me.clsOADiscount.DeleteOA_BRANDPACK_DISC(CInt(Me.GridEX2.GetValue("OA_BRANDPACK_DISC_ID")), NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.OtherDiscount, _
                 OA_BRANDPACK_DISC_QTY, , , , True, Me.GridEX2.GetValue("OA_BRANDPACK_ID").ToString())
            End If
            If Not Me.GridEX2.GetValue("DISCOUNT_FROM").ToString() = "REMAINDING_OA" Then
                index = GridExDataView.Find(OA_BRANDPACK_ID)
                If index <> -1 Then
                    Dim TotalDiscQty As Decimal = Convert.ToDecimal(GridExDataView(index)("TOTAL_DISC_QTY")) - OA_BRANDPACK_DISC_QTY
                    Dim Price As Decimal = Convert.ToDecimal(GridExDataView(index)("OA_PRICE_PERQTY"))
                    GridExDataView(index)("TOTAL_DISC_QTY") = TotalDiscQty
                    GridExDataView(index)("TOTAL_AMOUNT_DISC") = TotalDiscQty * Price
                    GridExDataView(index).EndEdit()
                End If
            End If
            Return index
        Catch ex As Exception
            Return -1
        End Try

    End Function

#End Region

#Region " Loading Data & Initialize "

    Friend Sub InitializeData()
        Me.RefReshData()
    End Sub

    Private Sub RefReshData()
        Me.SFG = StateFillingGrid.Filling
        Me.SFM = StateFillingMCB.Filling
        Me.SI_MCB = SelectedItemMCB.FromNone
        Me.clsOADiscount = New NufarmBussinesRules.OrderAcceptance.OADiscount
        Me.clsOADiscount.CreateViewOARef()
        If Me.mcbRefNo.SelectedIndex = -1 Then
            Me.BindMulticolumnCombo(Me.mcbRefNo, Me.clsOADiscount.ViewOARefNo, False)
        Else
            Me.BindMulticolumnCombo(Me.mcbRefNo, Me.clsOADiscount.ViewOARefNo, True)
            Me.BindMulticolumnCombo(Me.mcbBrandPack, Nothing, True)
        End If
    End Sub

#End Region

#Region " Clearing Control "

    Private Sub ClearMulticolumnCombo()
        Me.BindMulticolumnCombo(Me.mcbRefNo, Nothing, True)
        Me.BindMulticolumnCombo(Me.mcbBrandPack, Nothing, True)
    End Sub

    Private Sub ClearDataDiscount()
        'Me.BindGridEx(Me.GridEX2, Nothing)
        Me.BindGridEx(Me.GridEX2, Nothing)
        For Each Item As Control In Me.pnlAgreementDiscount.Controls
            If TypeOf (Item) Is Janus.Windows.EditControls.UIRadioButton Then
                CType(Item, Janus.Windows.EditControls.UIRadioButton).Checked = False
            End If
        Next
        For Each item As Control In Me.pnlMarketingDiscount.Controls
            If TypeOf (item) Is Janus.Windows.EditControls.UIRadioButton Then
                CType(item, Janus.Windows.EditControls.UIRadioButton).Checked = False
            End If
        Next
        Me.ClearCheckBox()
    End Sub
    Private Sub ClearCheckBox()
        Me.rdbGivenCP.Checked = False
        Me.rdbCPMRT_Dist.Checked = False
        Me.rdbCPMRT_TMDist.Checked = False
        Me.rdbGivenCP_TM.Checked = False
        Me.rdbSpecialCPD.Checked = False
        Me.rdbSpecialCPD_TM.Checked = False
        'Me.rdbCPDAuto.Checked = False
    End Sub
    Private Sub ClearRadioButton()
        Me.rdbCPDAuto.Checked = False
        Me.rdbDKN.Checked = False
        Me.rdbGivenCPR.Checked = False
        Me.rdbGivenDiscountMarketing.Checked = False
        Me.rdbSteppingDiscount.Checked = False
        Me.rdbTSDiscountMarketing.Checked = False
        Me.rdbGivenPKPP.Checked = False
        Me.rdbGivenDK.Checked = False
    End Sub
    Private Sub ClearRDBOthers()
        Me.rdbDD.Checked = False
        Me.rdbDR.Checked = False
        Me.rdbCBD.Checked = False
        Me.rdbDK.Checked = False
        Me.rdbDP.Checked = False
    End Sub
#End Region

#Region " Locking Controls "

    Private Sub LockControlNavigationPane()
        For Each pnl As DevComponents.DotNetBar.NavigationPanePanel In Me.NavigationPane1.Controls
            pnl.Enabled = False
        Next
        For Each item As Object In Me.pnlAgreement.Controls
            If TypeOf (item) Is Janus.Windows.EditControls.UIButton Then
                CType(item, Janus.Windows.EditControls.UIButton).Enabled = False
                'ElseIf TypeOf (item) Is Janus.Windows.EditControls.UIRadioButton Then
                '    CType(item, Janus.Windows.EditControls.UIRadioButton).Enabled = False
                '    CType(item, Janus.Windows.EditControls.UIRadioButton).Checked = False
            ElseIf TypeOf (item) Is DevComponents.DotNetBar.ExpandablePanel Then
                For Each item1 As Object In Me.pnlAgreementDiscount.Controls
                    If TypeOf (item) Is Janus.Windows.EditControls.UIRadioButton Then
                        CType(item, Janus.Windows.EditControls.UIRadioButton).Enabled = False
                        CType(item, Janus.Windows.EditControls.UIRadioButton).Checked = False
                    End If
                Next
            End If
        Next
        For Each item As Object In Me.pnlMarketing.Controls
            If TypeOf (item) Is Janus.Windows.EditControls.UIButton Then
                CType(item, Janus.Windows.EditControls.UIButton).Enabled = False
            ElseIf TypeOf (item) Is DevComponents.DotNetBar.ExpandablePanel Then
                For Each item1 As Object In Me.pnlMarketingDiscount.Controls
                    If TypeOf (item) Is Janus.Windows.EditControls.UIRadioButton Then
                        CType(item, Janus.Windows.EditControls.UIRadioButton).Enabled = False
                        CType(item, Janus.Windows.EditControls.UIRadioButton).Checked = False
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub LockedOABrandPackID()
        Me.mcbRefNo.Enabled = False
        Me.mcbBrandPack.Enabled = False
    End Sub

    Private Sub EnabledControlBrandPack()
        Me.txtQuantity.ReadOnly = False
        Me.mcbBrandPack.ReadOnly = False

    End Sub

    Private Sub UnabledControlOABrandPack()
        'Me.txtQuantity.ReadOnly = True
        Me.mcbBrandPack.ReadOnly = True
    End Sub

#End Region

#Region " Binding source control "

    Private Sub BindMulticolumnCombo(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal dtview As DataView, ByVal RemoveValue As Boolean)
        Me.SFM = StateFillingMCB.Filling
        If RemoveValue = True Then
            mcb.Value = Nothing
        End If
        mcb.SetDataBinding(dtview, "")
        Me.SFM = StateFillingMCB.HasFilled
    End Sub

    Private Sub BindGridEx(ByVal Grid As Janus.Windows.GridEX.GridEX, ByVal dtView As DataView)
        Me.SFG = StateFillingGrid.Filling
        If dtView Is Nothing Then
            Me.SFG = StateFillingGrid.HasFilled
            Grid.SetDataBinding(Nothing, "")
            Return
        End If
        'If Grid.Name = "GridEX2" Then
        '    For I As Integer = 0 To dtView.Count - 1
        '        dtView(I).BeginEdit()
        '        Select Case dtView(I)("GQSY_SGT_P_FLAG").ToString()
        '            Case "G"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "GIVEN"
        '            Case "Q1"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "QUARTERLY 1"
        '            Case "Q2"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "QUARTERLY 2"
        '            Case "Q3"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "QUARTERLY 3"
        '            Case "Q4"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "QUARTERLY 4"
        '            Case "S1"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "SEMESTERLY 1"
        '            Case "S2"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "SEMESTERLY 2"
        '            Case "Y"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "YEARLY"
        '            Case "MG"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "GIVEN_DPRD"
        '            Case "KP"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "GIVEN_PKPP"
        '            Case "DK"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "GIVEN_DK"
        '            Case "CP"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "GIVEN_CP(D)"
        '            Case "CS"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "GIVEN_CP(D)S"
        '            Case "CR"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "GIVEN_CP(R)"
        '            Case "MS"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "SALES_STEPPING"
        '            Case "MT"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "SALES_TARGET"
        '            Case "P"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "PROJECT"
        '            Case "O"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "OTHER"
        '            Case "RMOA"
        '                dtView(I)("GQSY_SGT_P_FLAG") = "REMAINDING_OA"
        '        End Select
        '        dtView(I).EndEdit()
        '    Next

        'End If
        Grid.SetDataBinding(dtView, "")
        If IsNothing(dtView) Then
            Me.SFG = StateFillingGrid.HasFilled
            Return
        End If
        If dtView.Count <= 0 Then
            Me.SFG = StateFillingGrid.HasFilled
            Return
        End If
        If Not Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
            Me.SFG = StateFillingGrid.HasFilled
            Return
        End If
        Dim TotalDiscQTY As Decimal = Me.GridEX2.GetTotal(Me.GridEX2.RootTable.Columns("DISC_QUANTITY"), Janus.Windows.GridEX.AggregateFunction.Sum, _
                  New Janus.Windows.GridEX.GridEXFilterCondition(Me.GridEX2.RootTable.Columns("DISCOUNT_FROM"), Janus.Windows.GridEX.ConditionOperator.NotEqual, DBNull.Value))
        Me.GridEX1.SetValue("TOTAL_DISC_QTY", TotalDiscQTY)
        Me.GridEX1.SetValue("TOTAL_AMOUNT_DISC", Convert.ToDecimal(Me.GridEX1.GetValue("OA_PRICE_PERQTY")) * TotalDiscQTY)
        Me.GridEX1.UpdateData()
        'Me.GridEX1.Refetch()
        Me.SFG = StateFillingGrid.HasFilled
    End Sub

    Private Sub BindGridEx3(ByVal dtview As DataView)
        If dtview Is Nothing Then
            Me.GridEX3.SetDataBinding(Nothing, "")
            Return
        End If
        If dtview.Count = 0 Then
            Me.GridEX3.SetDataBinding(Nothing, "")
            Return
        End If
        Select Case Me.SDiscount
            Case SelectedDiscount.AgreementDiscount
                For i As Integer = 0 To dtview.Count - 1
                    dtview(i).BeginEdit()
                    If Me.rdbGivenDiscount.Checked Then : dtview(i)("GQSY_FLAG") = "GIVEN"
                    ElseIf Me.rdbPeriodDiscount1.Checked Then : dtview(i)("QSY_FLAG") = "QUARTERLY 1"
                    ElseIf Me.rdbPeriodDiscount2.Checked Then : dtview(i)("QSY_FLAG") = "QUARTERLY 2"
                    ElseIf Me.rdbPeriodDiscount3.Checked Then : dtview(i)("QSY_FLAG") = "QUARTERLY 3"
                    ElseIf Me.rdbPeriodDiscount4.Checked Then : dtview(i)("QSY_FLAG") = "QUARTERLY 4"
                    ElseIf Me.rdbYearlyDiscount.Checked Then : dtview(i)("QSY_FLAG") = "YEARLY"
                    ElseIf Me.rdbSemesterly1Discount.Checked Then : dtview(i)("QSY_FLAG") = "SEMESTERLY 1"
                    ElseIf Me.rdbSemesterly2Discount.Checked Then : dtview(i)("QSY_FLAG") = "SEMESTERLY 2"
                    Else
                        dtview(i)("QSY_FLAG") = "UNKNOWN"
                    End If
                    dtview(i).EndEdit()
                Next
                Me.GridEX3.SetDataBinding(dtview, "") : Me.GridEX3.RetrieveStructure()
                If Me.rdbGivenDiscount.Checked = True Then
                    'format datagrid
                    With Me.GridEX3.RootTable
                        .Columns("AGREE_DISC_HIST_ID").Visible = False
                        .Columns("BRANDPACK_ID").Visible = False
                        .Columns("PO_REF_DATE").FormatString = "dd MMMM yyyy"
                        .Columns("AGREE_BRANDPACK_ID").Visible = False
                        .Columns("PO_BRANDPACK_ID").Visible = False
                        .Columns("OA_BRANDPACK_ID").Visible = False
                        .Columns("AGREE_OA_QTY").Caption = "OA_QTY"
                        .Columns("AGREE_OA_QTY").FormatString = "#,##0.000"
                        .Columns("AGREE_OA_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("AGREE_BRANDPACK_ID").Visible = False
                        .Columns("AGREE_OA_DISC_PCT").Caption = "DISCOUNT %"
                        .Columns("AGREE_OA_DISC_PCT").FormatString = "D"
                        .Columns("AGREE_OA_DISC_PCT").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("AGREE_DISC_QTY").Caption = "DISCOUNT_QTY"
                        .Columns("AGREE_DISC_QTY").FormatString = "#,##0.000"
                        .Columns("AGREE_DISC_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("AGREE_RELEASE_QTY").Caption = "RELEASE_QTY"
                        .Columns("AGREE_RELEASE_QTY").FormatString = "#,##0.000"
                        .Columns("AGREE_RELEASE_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("AGREE_LEFT_QTY").Caption = "LEFT_QTY"
                        .Columns("AGREE_LEFT_QTY").FormatString = "#,##0.000"
                        .Columns("AGREE_LEFT_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("GQSY_FLAG").Caption = "DISCOUNT_TYPE"
                    End With
                ElseIf (Me.rdbPeriodDiscount1.Checked = True) Or (Me.rdbPeriodDiscount2.Checked = True) Or (Me.rdbPeriodDiscount3.Checked = True) _
                Or (Me.rdbPeriodDiscount4.Checked = True) Or (Me.rdbYearlyDiscount.Checked = True) Or (Me.rdbSemesterly1Discount.Checked) _
                Or (Me.rdbSemesterly2Discount.Checked) Then
                    'ntar lagi
                    With Me.GridEX3.RootTable
                        If (.Columns.Contains("BRND_B_S_ID")) Then
                            .Columns("BRND_B_S_ID").Visible = False
                        End If
                        If (.Columns.Contains("ACHIEVEMENT_BRANDPACK_ID")) Then
                            .Columns("ACHIEVEMENT_BRANDPACK_ID").Visible = False
                        End If
                        If (.Columns.Contains("IDApp")) Then
                            .Columns("IDApp").Visible = True
                        End If

                        .Columns("DISPRO %").FormatString = "#,##0.00"
                        .Columns("DISPRO %").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("DISC_QTY").FormatString = "#,##0.000"
                        .Columns("DISC_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("RELEASE_QTY").FormatString = "#,##0.000"
                        .Columns("RELEASE_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("LEFT_QTY").FormatString = "#,##0.000"
                        .Columns("LEFT_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("QSY_FLAG").Caption = "DISCOUNT TYPE"
                    End With
                Else
                    Me.GridEX3.SetDataBinding(Nothing, "")
                    Me.pnlGridDiscount.TitleText = Me.pnlGridDiscount.TitleText.Remove(0, Me.pnlGridDiscount.TitleText.IndexOf(":") + 1)
                    Return
                End If
                If Me.GridEX3.RootTable.Columns.Contains("IDApp") Then
                    Me.GridEX3.RootTable.Columns("IDApp").Visible = False
                End If
            Case SelectedDiscount.MarketingDiscount
                For i As Integer = 0 To dtview.Count - 1
                    If Me.rdbGivenCP.Checked = True Then
                        dtview(i)("SGT_FLAG") = "CP(D)"
                    ElseIf Me.rdbSpecialCPD.Checked Then
                        dtview(i)("SGT_FLAG") = "CP(D)S"
                    ElseIf Me.rdbGivenCPR.Checked = True Then
                        dtview(i)("SGT_FLAG") = "CP(RD)"
                    ElseIf Me.rdbGivenDiscountMarketing.Checked = True Then
                        If dtview(i)("SGT_FLAG").ToString() = "MG" Then
                            dtview(i)("SGT_FLAG") = "GIVEN_DPRD"
                        End If
                    ElseIf Me.rdbGivenDK.Checked = True Then
                        dtview(i)("SGT_FLAG") = "DK"
                    ElseIf Me.rdbGivenPKPP.Checked = True Then
                        dtview(i)("SGT_FLAG") = "PKPP"
                    ElseIf Me.rdbSpecialCPD_TM.Checked Then
                        dtview(i)("SGT_FLAG") = "CP(D)S_DIST_TM"
                    ElseIf Me.rdbGivenCP_TM.Checked Then
                        dtview(i)("SGT_FLAG") = "CP(D)_DIST_TM"
                    ElseIf Me.rdbCPMRT_Dist.Checked Then
                        dtview(i)("SGT_FLAG") = "CP(R M/T)_DIST"
                    ElseIf rdbCPMRT_TMDist.Checked Then
                        dtview(i)("SGT_FLAG") = "CP(R M/T)_DIST_TM"
                    ElseIf Me.rdbDKN.Checked Then
                        dtview(i)("SGT_FLAG") = "DK(N)"
                    ElseIf Me.rdbCPDAuto.Checked Then
                        dtview(i)("SGT_FLAG") = "CP(D)AUTO"
                    End If
                    dtview(i).EndEdit()
                Next
                Me.GridEX3.SetDataBinding(dtview, "") : Me.GridEX3.RetrieveStructure()
                If (Me.rdbTSDiscountMarketing.Checked = True) Or (Me.rdbSteppingDiscount.Checked = True) Then
                    With Me.GridEX3.RootTable
                        .Columns("MRKT_M_S_ID").Visible = False
                        .Columns("TOTAL_REACHED_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("TOTAL_REACHED_QTY").FormatString = "#,##0.000"
                        .Columns("TOTAL_REACHED_AMOUNT").Visible = False
                        .Columns("MRKT_DISC_PCT").FormatString = "#,##0.000"
                        .Columns("MRKT_DISC_PCT").Caption = "DISCOUNT %"
                        .Columns("MRKT_DISC_PCT").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("MRKT_DISC_QTY").Caption = "DISC_QTY"
                        .Columns("MRKT_DISC_QTY").FormatString = "#,##0.000"
                        .Columns("MRKT_DISC_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("DISC_AMOUNT").Visible = False
                        .Columns("MRKT_RELEASE_QTY").Caption = "RELEASE_QTY"
                        .Columns("MRKT_RELEASE_QTY").FormatString = "#,##0.000"
                        .Columns("MRKT_RELEASE_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("MRKT_RELEASE_AMOUNT").Visible = False
                        .Columns("MRKT_LEFT_QTY").Caption = "LEFT_QTY"
                        .Columns("MRKT_LEFT_QTY").FormatString = "#,##0.000"
                        .Columns("MRKT_LEFT_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("MRKT_LEFT_AMOUNT").Visible = False
                        .Columns("ST_FLAG").Caption = "DISCOUNT TYPE"
                    End With
                ElseIf (Me.rdbGivenDiscountMarketing.Checked = True) Or (Me.rdbGivenCP.Checked = True) _
                Or (Me.rdbGivenDK.Checked = True) Or (Me.rdbGivenPKPP.Checked = True) Or (Me.rdbGivenCPR.Checked = True) Or (Me.rdbSpecialCPD.Checked = True) _
                Or Me.rdbSpecialCPD_TM.Checked Or Me.rdbGivenCP_TM.Checked Or rdbCPMRT_Dist.Checked Or rdbCPMRT_TMDist.Checked Or Me.rdbDKN.Checked _
                Or Me.rdbCPDAuto.Checked Then
                    'FORMAT DATAGRID
                    With Me.GridEX3.RootTable
                        .Columns("MRKT_DISC_HIST_ID").Visible = False
                        .Columns("BRANDPACK_ID").Visible = False
                        .Columns("PO_REF_DATE").FormatString = "dd MMMM yyyy"
                        .Columns("OA_BRANDPACK_ID").Visible = False
                        .Columns("PROG_BRANDPACK_DIST_ID").Visible = False
                        .Columns("MRKT_OA_QTY").Caption = "OA_QTY"
                        .Columns("MRKT_OA_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("MRKT_OA_QTY").FormatString = "#,##0.000"
                        .Columns("OA_BRANDPACK_ID").Visible = False
                        .Columns("PROG_BRANDPACK_DIST_ID").Visible = False
                        .Columns("MRKT_DISC_PCT").Caption = "DISCOUNT %"
                        If Me.rdbCPDAuto.Checked Then
                            .Columns("DESCRIPTION").Visible = True
                            .Columns("MRKT_DISC_PCT").Visible = False
                        Else
                            .Columns("MRKT_DISC_PCT").Visible = True
                            '.Columns("DESCRIPTION").Visible = False
                            .Columns("MRKT_DISC_PCT").FormatString = "#,##0.000"
                            .Columns("MRKT_DISC_PCT").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        End If
                        .Columns("MRKT_DISC_QTY").Caption = "DISCOUNT_QTY"
                        .Columns("MRKT_DISC_QTY").FormatString = "#,##0.000"
                        .Columns("MRKT_DISC_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("MRKT_RELEASE_QTY").Caption = "RELEASE_QTY"
                        .Columns("MRKT_RELEASE_QTY").FormatString = "#,##0.000"
                        .Columns("MRKT_RELEASE_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("MRKT_LEFT_QTY").Caption = "LEFT_QTY"
                        .Columns("MRKT_LEFT_QTY").FormatString = "#,##0.000"
                        .Columns("MRKT_LEFT_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        .Columns("SGT_FLAG").Caption = "DISCOUNT_TYPE"
                    End With
                Else
                    Me.GridEX3.SetDataBinding(Nothing, "")
                    Me.pnlGridDiscount.TitleText = Me.pnlGridDiscount.TitleText.Remove(0, Me.pnlGridDiscount.TitleText.IndexOf(":") + 1)
                    Return
                End If
            Case SelectedDiscount.OtherDiscount
                Me.GridEX3.SetDataBinding(Nothing, "")
                Me.pnlGridDiscount.TitleText = Me.pnlGridDiscount.TitleText.Remove(0, Me.pnlGridDiscount.TitleText.IndexOf(":") + 2)
                Return
            Case SelectedDiscount.ProjectDiscount
                Me.GridEX3.SetDataBinding(dtview, "") : Me.GridEX3.RetrieveStructure()
                With Me.GridEX3.RootTable
                    .Columns("PROJ_DISC_HIST_ID").Visible = False
                    .Columns("BRANDPACK_ID").Visible = False
                    .Columns("PROJ_BRANDPACK_ID").Visible = False
                    .Columns("TOTAL_PO_QTY").Caption = "TOTAL_PO_QTY"
                    .Columns("TOTAL_PO_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    .Columns("TOTAL_PO_QTY").FormatString = "#,##0.00"
                    .Columns("PROJ_DISC_PCT").Caption = "DISCOUNT %"
                    .Columns("PROJ_DISC_PCT").FormatString = "#,##0.000"
                    .Columns("PROJ_DISC_PCT").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    .Columns("PROJ_DISC_QTY").Caption = "DISCOUNT_QTY"
                    .Columns("PROJ_DISC_QTY").FormatString = "#,##0.000"
                    .Columns("PROJ_DISC_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    .Columns("PROJ_RELEASE_QTY").Caption = "RELEASE_QTY"
                    .Columns("PROJ_RELEASE_QTY").FormatString = "#,##0.000"
                    .Columns("PROJ_RELEASE_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    .Columns("PROJ_LEFT_QTY").Caption = "LEFT_QTY"
                    .Columns("PROJ_LEFT_QTY").FormatString = "#,##0.000"
                    .Columns("PROJ_LEFT_QTY").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                End With
        End Select
        If Not IsNothing(Me.GridEX3.DataSource) Then
            For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX3.RootTable.Columns
                Item.Selectable = False
                If Item.Type Is Type.GetType("System.DateTime") Then
                    Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    Item.FormatString = "dd MMMM yyyy"
                End If
                Item.AutoSize()
            Next
        End If
    End Sub

    Private Sub BindGridRemainding(ByVal dtview As DataView)
        If dtview Is Nothing Then
            Me.grdRemainding.SetDataBinding(Nothing, "")
            Me.btnInsertSystem.Enabled = False
            Me.grdRemainding.RootTable.Columns("BRANDPACK_NAME").TotalFormatString = ""
            Return
        End If
        If dtview.Count = 0 Then
            Me.grdRemainding.SetDataBinding(Nothing, "")
            Me.grdRemainding.Update()
            Me.btnInsertSystem.Enabled = False
            Me.grdRemainding.RootTable.Columns("BRANDPACK_NAME").TotalFormatString = ""
            Return
        End If
        If Not dtview.Table.Columns.Contains("LEFT_QTY_FROM") Then
            Dim colLeftQTYFrom As New DataColumn("LEFT_QTY_FROM", Type.GetType("System.String"))
            dtview.Table.Columns.Add(colLeftQTYFrom)
        End If
        For i As Integer = 0 To dtview.Count - 1
            If dtview(i)("FLAG") = "G" Then
                dtview(i)("LEFT_QTY_FROM") = "AGREEMENT_GIVEN"
            ElseIf dtview(i)("FLAG") = "Q1" Then
                dtview(i)("LEFT_QTY_FROM") = "QUARTERLY 1"
            ElseIf dtview(i)("FLAG") = "Q2" Then
                dtview(i)("LEFT_QTY_FROM") = "QUARTERLY 2"
            ElseIf dtview(i)("FLAG") = "Q3" Then
                dtview(i)("LEFT_QTY_FROM") = "QUARTERLY 3"
            ElseIf dtview(i)("FLAG") = "Q4" Then
                dtview(i)("LEFT_QTY_FROM") = "QUARTERLY 4"
            ElseIf dtview(i)("FLAG") = "S1" Then
                dtview(i)("LEFT_QTY_FROM") = "SEMESTERLY 1"
            ElseIf dtview(i)("FLAG") = "S2" Then
                dtview(i)("LEFT_QTY_FROM") = "SEMESTERLY 2"
            ElseIf dtview(i)("FLAG") = "Y" Then
                dtview(i)("LEFT_QTY_FROM") = "YEARLY"
            ElseIf dtview(i)("FLAG") = "MG" Then
                dtview(i)("LEFT_QTY_FROM") = "SALES_GIVEN"
            ElseIf dtview(i)("FLAG") = "KP" Then
                dtview(i)("LEFT_QTY_FROM") = "PKPP"
            ElseIf dtview(i)("FLAG") = "CP" Then
                dtview(i)("LEFT_QTY_FROM") = "CP(D)"
            ElseIf dtview(i)("FLAG") = "CS" Then
                dtview(i)("LEFT_QTY_FROM") = "CP(D)S"
            ElseIf dtview(i)("FLAG") = "TD" Then
                dtview(i)("LEFT_QTY_FROM") = "CP(D)_DIST_TM"
            ElseIf dtview(i)("FLAG") = "TS" Then
                dtview(i)("LEFT_QTY_FROM") = "CP(D)S_DIST_TM"
            ElseIf dtview(i)("FLAG") = "CT" Then
                dtview(i)("LEFT_QTY_FROM") = "CP(R M/T)_DIST_TM"
            ElseIf dtview(i)("FLAG") = "CD" Then
                dtview(i)("LEFT_QTY_FROM") = "CP(R M/T)_DIST"
            ElseIf dtview(i)("FLAG") = "CR" Then
                dtview(i)("LEFT_QTY_FROM") = "CP(RD)"
            ElseIf dtview(i)("FLAG") = "DK" Then
                dtview(i)("LEFT_QTY_FROM") = "DK"
            ElseIf dtview(i)("FLAG") = "MS" Then
                dtview(i)("LEFT_QTY_FROM") = "SALES_STEPPING"
            ElseIf dtview(i)("FLAG") = "MT" Then
                dtview(i)("LEFT_QTY_FROM") = "SALES_TARGET"
            ElseIf dtview(i)("FLAG") = "O" Then
                dtview(i)("LEFT_QTY_FROM") = "OTHER"
            ElseIf dtview(i)("FLAG") = "OCBD" Then
                dtview(i)("LEFT_QTY_FROM") = "OTHER(CBD)"
            ElseIf dtview(i)("FLAG") = "ODD" Then
                dtview(i)("LEFT_QTY_FROM") = "OTHER(DD)"
            ElseIf dtview(i)("FLAG") = "ODR" Then
                dtview(i)("LEFT_QTY_FROM") = "OTHER(DR)"
            ElseIf dtview(i)("FLAG") = "ODK" Then
                dtview(i)("LEFT_QTY_FROM") = "OTHER(DK)"
            ElseIf dtview(i)("FLAG") = "ODP" Then
                dtview(i)("LEFT_QTY_FROM") = "OTHER(DP)"
            ElseIf dtview(i)("FLAG") = "DN" Then
                dtview(i)("LEFT_QTY_FROM") = "DK(N)"
            ElseIf dtview(i)("FLAG") = "CA" Then
                dtview(i)("LEFT_QTY_FROM") = "CP(D)AUTO"
            ElseIf dtview(i)("FLAG") = "RMOA" Then
                dtview(i)("LEFT_QTY_FROM") = "REMAINDING_OA"
            Else
                dtview(i)("LEFT_QTY_FROM") = "UNKNOWN"
            End If
        Next
        Me.grdRemainding.SetDataBinding(dtview, "")
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdRemainding.RootTable.Columns
            col.AutoSize() : col.Selectable = False
        Next
        Dim TOTAL As Decimal = Convert.ToDecimal(Me.grdRemainding.GetTotal(Me.grdRemainding.RootTable.Columns("LEFT_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum, _
        Nothing))
        If TOTAL = Me.Devided_Qty Then
            TOTAL = Me.Devided_Qty
        ElseIf TOTAL > Me.Devided_Qty Then
            TOTAL = Decimal.Truncate(TOTAL / Me.Devided_Qty) * Me.Devided_Qty
        ElseIf TOTAL <= Me.Devided_Qty Then
            Me.grdRemainding.RootTable.Columns("BRANDPACK_NAME").TotalFormatString = "qty < " & Devided_Qty.ToString()
            Me.btnInsertSystem.Enabled = False
            Return
        End If
        Me.btnInsertSystem.Enabled = True
        Me.grdRemainding.RootTable.Columns("BRANDPACK_NAME").TotalFormatString = "Can be released = " & String.Format("{0:#,##0.000}", TOTAL)
    End Sub
#End Region

#Region " Inflating Data "

    Private Sub InflateData()
        Me.SFG = StateFillingGrid.Filling
        Me.mcbRefNo.Value = Me.GridEX1.GetValue("OA_REF_NO")
        Me.SI_MCB = SelectedItemMCB.FromGrid
        Me.mcbBrandPack.Value = Me.GridEX1.GetValue("PO_BRANDPACK_ID")
        Me.txtPrice.Value = Me.GridEX1.GetValue("OA_PRICE_PERQTY")
        Me.txtQuantity.Value = Me.GridEX1.GetValue("OA_ORIGINAL_QTY")
        Me.txtTotal.Value = Me.GridEX1.GetValue("TOTAL")
        If Not IsDBNull(Me.GridEX1.GetValue("UNIT_ORDER")) And Not IsNothing(Me.GridEX1.GetValue("UNIT_ORDER")) Then
            If Me.GridEX1.GetValue("UNIT_ORDER").ToString() = "LITRE" Then
                Me.cmbKiloLitre.SelectedIndex = 0
            ElseIf Me.GridEX1.GetValue("UNIT_ORDER") = "BAGS" Then
                Me.cmbKiloLitre.SelectedIndex = 2
            ElseIf Me.GridEX1.GetValue("UNIT_ORDER").ToString() = "" Then
                Me.cmbKiloLitre.SelectedIndex = -1
            Else
                Me.cmbKiloLitre.SelectedIndex = 1
            End If
            If Me.clsOADiscount.UNIT = "" Or IsNothing(Me.clsOADiscount.UNIT) Then
                Me.clsOADiscount.UNIT = Me.GridEX1.GetValue("UNIT_ORDER").ToString()
            End If
        Else
            Me.cmbKiloLitre.SelectedIndex = -1
        End If

        'Me.cmbKiloLitre.SelectedIndex = Me.GridEX1.GetValue("UNIT_ORDER")
        If Not IsDBNull(Me.GridEX1.GetValue("REMARK")) Then
            Me.txtRemark.Text = Me.GridEX1.GetValue("REMARK")
        Else
            Me.txtRemark.Text = ""
        End If
        Me.SI_MCB = SelectedItemMCB.FromMCB
        Me.SFG = StateFillingGrid.HasFilled
    End Sub

    Private Sub InflateDataFromMCB(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Select Case mcb.Name
            Case "mcbRefNo"
                Me.txtOARefDate.Text = CDate(Me.mcbRefNo.DropDownList.GetValue("OA_DATE")).ToLongDateString()
                Me.txtPOREFNO.Text = Me.mcbRefNo.DropDownList.GetValue("PO_REF_NO").ToString()
                Me.txTPOREFDATE.Text = CDate(Me.mcbRefNo.DropDownList().GetValue("PO_REF_DATE")).ToLongDateString()

                Me.txtPOFrom.Text = Me.clsOADiscount.PO_From(Me.mcbRefNo.Value.ToString(), False)
                Me.DistributorID = Me.clsOADiscount.DISTRIBUTOR_ID
                Me.DistribtorName = Me.clsOADiscount.DISTRIBUTOR_NAME

                Me.clsOADiscount.CreateViewPOBrandPack(Me.mcbRefNo.DropDownList.GetValue("PO_REF_NO").ToString(), Me.DistributorID)
                Me.BindMulticolumnCombo(Me.mcbBrandPack, Me.clsOADiscount.ViewPOBrandpack(), True)
                If Me.Mode = ModeSave.Save Then
                    If Me.mcbBrandPack.DropDownList.RecordCount = 1 Then
                        Dim dtview As DataView = CType(Me.mcbBrandPack.DataSource, DataView)
                        Me.SI_MCB = SelectedItemMCB.FromMCB
                        Me.mcbBrandPack.Value = dtview(0)("PO_BRANDPACK_ID")
                    End If
                End If
                Me.mcbBrandPack.ReadOnly = False
                If Me.GridEX1.RecordCount > 0 Then
                    'GENERATE DISCOUNT PER OA 
                    Me.clsOADiscount.getTotalPrice(Me.DistributorID, Me.mcbRefNo.Value.ToString(), Me.mcbRefNo.DropDownList().GetValue("PO_REF_NO").ToString())
                    Me.pnlOADiscount.TitleText = "MAX_DISCOUNT = " & Me.clsOADiscount.MAX_DISC_PER_PO.ToString() & "  TOTAL_PRICE = " & _
                    String.Format("{0:#,##0.00}", Me.clsOADiscount.TOTAL_PRICE_DISTRIBUTOR & "  MAX_DISCOUNT_PRICE : ") & _
                    String.Format("{0:#,##0.00}", (Me.clsOADiscount.TOTAL_PRICE_DISTRIBUTOR * Me.clsOADiscount.MAX_DISC_PER_PO) / 100)
                Else
                    Me.pnlOADiscount.TitleText = ""
                End If

            Case "mcbBrandPack"
                Me.clsOADiscount.GetQuantityRowPOBrandPack(Me.mcbBrandPack.Value.ToString(), Me.mcbRefNo.Value.ToString())
                Me.txtPrice.Text = Me.clsOADiscount.OA_PRICE_PERQTY
                Me.txtQuantity.Value = Convert.ToDecimal(Me.clsOADiscount.PO_OA_ORIGINAL_QTY)
                If Me.clsOADiscount.UNIT.Trim() = "LITRE" Then
                    Me.cmbKiloLitre.SelectedIndex = 0
                ElseIf Me.clsOADiscount.UNIT = "BAGS" Then
                    Me.cmbKiloLitre.SelectedIndex = 2
                ElseIf Me.clsOADiscount.UNIT = "" Then

                    Me.cmbKiloLitre.SelectedIndex = -1
                Else
                    Me.cmbKiloLitre.SelectedIndex = 1
                End If
                'Me.cmbKiloLitre.SelectedValue = Me.clsOADiscount.UNIT
                Me.txtQuantity.Enabled = True
                Me.txtQuantity.SelectionStart = 0
                Me.txtQuantity.SelectionLength = Me.txtQuantity.Value.ToString().Length
                Me.txtQuantity.Focus()
        End Select
    End Sub

#End Region

    Private Sub ProcessAdjustment(ByRef LeftQty As Decimal, ByRef NewRow As DataRow, ByRef tblAdjustment As DataTable)
        If LeftQty > 0 Then
            If Convert.ToDecimal(Me.QTY.txtAdjust2.Value) > 0 Then
                If LeftQty > Convert.ToDecimal(Me.QTY.txtAdjust2.Value) Then
                    'berarti pengurangan 2 row data
                    NewRow = tblAdjustment.NewRow()
                    NewRow("IDApp") = Me.QTY.dt.Rows(1)("IDApp")
                    NewRow("ReleaseQty") = Convert.ToDecimal(Me.QTY.txtAdjust2.Value)
                    NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                    NewRow("IsRemain") = True
                    LeftQty = LeftQty - Convert.ToDecimal(Me.QTY.txtAdjust2.Value)
                    NewRow.EndEdit()
                    tblAdjustment.Rows.Add(NewRow)
                    If LeftQty - Convert.ToDecimal(Me.QTY.txtAdjust1.Value) > 0 Then
                        NewRow = tblAdjustment.NewRow()
                        NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                        NewRow("ReleaseQty") = Convert.ToDecimal(Me.QTY.txtAdjust1.Value)
                        NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                        NewRow("IsRemain") = True
                        LeftQty = LeftQty - Convert.ToDecimal(Me.QTY.txtAdjust1.Value)
                        NewRow.EndEdit()
                        tblAdjustment.Rows.Add(NewRow)
                        If LeftQty > 0 Then
                            NewRow = tblAdjustment.NewRow()
                            If Me.GridEX3.RootTable.Columns.Contains("IDApp") Then
                                NewRow("IDApp") = Me.GridEX3.GetValue("IDApp")
                            Else
                                NewRow("IDApp") = 0
                            End If
                            NewRow("ABID") = Me.GridEX3.GetValue("ACHIEVEMENT_BRANDPACK_ID")
                            ''tidak mungkin akan melebihi dari pengurangan 3 bagian dpd qty dan adjust1 dan adjust 2
                            NewRow("ReleaseQty") = LeftQty ''sisa dari pengurangan adjust1 dan adjust 2
                            NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                            NewRow("IsRemain") = True
                            NewRow.EndEdit()
                            tblAdjustment.Rows.Add(NewRow)
                        End If
                    Else ''sisa dari LeftQty - Adjust 1
                        If LeftQty > 0 Then
                            NewRow = tblAdjustment.NewRow()
                            NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                            NewRow("ReleaseQty") = LeftQty
                            NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                            NewRow("IsRemain") = True
                            NewRow.EndEdit()
                            tblAdjustment.Rows.Add(NewRow)
                        End If
                    End If
                Else
                    'pengurangan 1 row data di txtAdjust1 saja
                    NewRow = tblAdjustment.NewRow()
                    'NewRow("IDApp") = iif(me.QTY.NumericEditBox1.Value mod Devided_Qty > 0,me.GridEX3.GetValue("IDApp") Me.QTY.dt.Rows(1)("IDApp")
                    If Me.QTY.NumericEditBox1.Value Mod Devided_Qty > 0 Then
                        If Me.GridEX3.RootTable.Columns.Contains("IDApp") Then
                            NewRow("IDApp") = Me.GridEX3.GetValue("IDApp")
                        Else
                            NewRow("IDApp") = 0
                        End If
                        NewRow("ABID") = Me.GridEX3.GetValue("ACHIEVEMENT_BRANDPACK_ID")
                    Else
                        NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                    End If
                    NewRow("ReleaseQty") = LeftQty
                    NewRow("IsRemain") = True
                    NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                    NewRow.EndEdit()
                    tblAdjustment.Rows.Add(NewRow)
                End If
            ElseIf Convert.ToDecimal(Me.QTY.txtAdjust1.Value) > 0 Then
                'pengurangan 1 row data di txtAdjust1 saja
                If LeftQty > Convert.ToDecimal(Me.QTY.txtAdjust1.Value) Then
                    NewRow = tblAdjustment.NewRow()
                    NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                    NewRow("ReleaseQty") = Convert.ToDecimal(Me.QTY.txtAdjust1.Value)
                    NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                    NewRow("IsRemain") = True
                    LeftQty = LeftQty - Convert.ToDecimal(Me.QTY.txtAdjust1.Value)
                    NewRow.EndEdit()
                    tblAdjustment.Rows.Add(NewRow)
                    If LeftQty > 0 Then
                        NewRow = tblAdjustment.NewRow()
                        If Me.GridEX3.RootTable.Columns.Contains("IDApp") Then
                            NewRow("IDApp") = Me.GridEX3.GetValue("IDApp")
                        Else
                            NewRow("IDApp") = 0
                        End If
                        NewRow("ABID") = Me.GridEX3.GetValue("ACHIEVEMENT_BRANDPACK_ID")
                        ''tidak mungkin akan melebihi dari pengurangan 3 bagian dpd qty dan adjust1 dan adjust 2
                        NewRow("ReleaseQty") = LeftQty ''sisa dari pengurangan adjust1 dan adjust 2
                        NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                        NewRow("IsRemain") = True
                        NewRow.EndEdit()
                        tblAdjustment.Rows.Add(NewRow)
                    End If
                Else
                    NewRow = tblAdjustment.NewRow()
                    If Me.QTY.NumericEditBox1.Value Mod Devided_Qty > 0 Then
                        If Me.GridEX3.RootTable.Columns.Contains("IDApp") Then
                            NewRow("IDApp") = Me.GridEX3.GetValue("IDApp")
                        Else
                            NewRow("IDApp") = 0
                        End If
                        NewRow("ABID") = Me.GridEX3.GetValue("ACHIEVEMENT_BRANDPACK_ID")
                    Else
                        NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                    End If
                    NewRow("ReleaseQty") = LeftQty
                    NewRow("IsRemain") = True
                    NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                    NewRow.EndEdit()
                    tblAdjustment.Rows.Add(NewRow)
                End If
            Else 'pengurangan remain hanya pada numeric edit box
                NewRow = tblAdjustment.NewRow()
                If Me.GridEX3.RootTable.Columns.Contains("IDApp") Then
                    NewRow("IDApp") = Me.GridEX3.GetValue("IDApp")
                Else
                    NewRow("IDApp") = 0
                End If
                NewRow("ABID") = Me.GridEX3.GetValue("ACHIEVEMENT_BRANDPACK_ID")
                ''tidak mungkin akan melebihi dari pengurangan 3 bagian dpd qty dan adjust1 dan adjust 2
                NewRow("ReleaseQty") = LeftQty ''sisa dari pengurangan adjust1 dan adjust 2
                NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                NewRow("IsRemain") = True
                NewRow.EndEdit()
                tblAdjustment.Rows.Add(NewRow)
            End If
        Else
            ''check apakah ada adjustment
            If Convert.ToDecimal(Me.QTY.txtAdjust2.Value) > 0 Then
                NewRow = tblAdjustment.NewRow()
                NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                NewRow("ReleaseQty") = Convert.ToDecimal(Me.QTY.txtAdjust2.Value)
                NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                'NewRow("IsRemain") = True
                NewRow.EndEdit()
                tblAdjustment.Rows.Add(NewRow)
                If Convert.ToDecimal(Me.QTY.txtAdjust1.Value) > 0 Then
                    NewRow = tblAdjustment.NewRow()
                    NewRow("IDApp") = Me.QTY.dt.Rows(1)("IDApp")
                    NewRow("ReleaseQty") = Convert.ToDecimal(Me.QTY.txtAdjust1.Value)
                    NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                    'NewRow("IsRemain") = True
                    NewRow.EndEdit()
                    tblAdjustment.Rows.Add(NewRow)
                End If
            ElseIf Convert.ToDecimal(Me.QTY.txtAdjust1.Value) > 0 Then
                NewRow = tblAdjustment.NewRow()
                NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                NewRow("ReleaseQty") = Convert.ToDecimal(Me.QTY.txtAdjust1.Value)
                NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                'NewRow("IsRemain") = True
                NewRow.EndEdit()
                tblAdjustment.Rows.Add(NewRow)
            End If
            If Convert.ToDecimal(Me.QTY.NumericEditBox1.Value) > 0 Then
                NewRow = tblAdjustment.NewRow()
                NewRow("IDApp") = Me.GridEX3.GetValue("IDApp")
                NewRow("ReleaseQty") = Convert.ToDecimal(Me.QTY.NumericEditBox1.Value)
                NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                'NewRow("IsRemain") = True
                NewRow("ABID") = Me.GridEX3.GetValue("ACHIEVEMENT_BRANDPACK_ID")
                NewRow.EndEdit()
                tblAdjustment.Rows.Add(NewRow)
            End If
        End If
    End Sub
#Region " Showing Ballon form (edit quantity) "

    Private Sub lnkOK_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles QTY.lnkClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.QTY.NumericEditBox1.Value Is Nothing Then
                Me.ShowMessageInfo("Invalid Data !" & vbCrLf & "System can not process invalid data." & vbCrLf & "Data can not be null / zero.")
                Me.QTY.Show(Me.pnlOADiscount, True)
                Return
            ElseIf Me.QTY.NumericEditBox1.Value = 0 Then
                Me.ShowMessageInfo("Invalid Data !" & vbCrLf & "System can not process invalid data." & vbCrLf & "Data can not be null / zero.")
                Me.QTY.Show(Me.pnlOADiscount, True)
                Return
            End If

            'If Me.QTY.chkSetAdjustment.Checked Then
            '    Dim TotalAdjust As Decimal = IIf(Me.QTY.txtAdjust2.Enabled, Convert.ToDecimal(Me.QTY.txtAdjust2.Value), 0D) + IIf(Me.QTY.txtAdjust1.Enabled, Convert.ToDecimal(Me.QTY.txtAdjust1.Value), 0D)
            '    'Convert.ToDecimal(Me.QTY.txtAdjust2.Value)
            '    If TotalAdjust > Convert.ToDecimal(Me.QTY.NumericEditBox1.Value) Then
            '        Me.ShowMessageInfo("Adjustment Quantity > Discount")
            '        Me.QTY.Show(Me.pnlOADiscount, True)
            '        Return
            '    End If
            'End If

            'GET MAXIMUM DISC_PER OA BY TAKING FROM THE VARIABLE QTY_MUST_BE_GIVEN
            'COUNT QTY ON ORDR_OA_BRANDACK
            'Dim CountQTY As Object = Me.clsOADiscount.SumQTY_OA_DISCOUNT(Me.OA_REF_NO)
            'DEFINE PRICE BY OA_BRANDPACK_ID
            If (Me.mcbRefNo.Value Is Nothing) Or (Me.mcbRefNo.SelectedItem Is Nothing) Then
                Me.ShowMessageInfo("Please define OA_REF_NO to compute by system")
                Me.QTY.Show(Me.pnlOADiscount, True)
                Return
            End If
            If Me.GridEX1.RecordCount <= 0 Then
                Me.ShowMessageInfo("Please define Brandpack name")
                Me.QTY.Show(Me.pnlOADiscount, True)
                Return
            End If
            If Me.GridEX1.SelectedItems.Count = 0 Then
                Me.ShowMessageInfo("Please define Brandpack name")
                Me.QTY.Show(Me.pnlOADiscount)
                Return
            End If
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please define Brandpack name")
                Me.QTY.Show(Me.pnlOADiscount, True)
                Return
            End If
            If IsDBNull(Me.GridEX1.GetValue("OA_PRICE_PERQTY")) Then
                Me.ShowMessageInfo("PRICE for that brandpack is null")
                Me.QTY.Show(Me.pnlOADiscount, True)
                Return
            End If
            If Not Me.QTY.chkSetAdjustment.Checked Then
                Me.QTY.txtAdjust1.Value = 0D
                Me.QTY.txtAdjust2.Value = 0D
            End If
            Dim AdjustQty As Decimal = 0D
            Dim totalQty As Decimal = IIf((Me.QTY.chkSetAdjustment.Checked), Convert.ToDecimal(QTY.NumericEditBox1.Value) + Convert.ToDecimal(Me.QTY.txtAdjust1.Value) + Convert.ToDecimal(Me.QTY.txtAdjust2.Value), Convert.ToDecimal(Me.QTY.NumericEditBox1.Value))

            Dim ResultQty As Decimal = Me.QTY.NumericEditBox1.Value
            Dim OA_BRANDPACK_DISC_QTY As Decimal = ResultQty
            'If totalQty > 0 Then
            '    If totalQty > ResultQty Then 'jika adjustment lebih besar
            '        totalQty = ResultQty
            '    End If
            'End If
            Dim PRICE As Decimal = Convert.ToDecimal(Me.GridEX1.GetValue("OA_PRICE_PERQTY"))
            Dim PrimaryIdent As Integer = 0 'OA_BRANDPACK_DISC_ID
            Dim AmountPrice As Decimal = PRICE * ResultQty  'KALIKAN QUANTITY DENGAN PRICE
            Dim DISC_PRICE_OA As Decimal = 0

            Dim indexRow As Integer = Me.GridEX1.Row
            Dim indexCol As Integer = Me.GridEX1.Col
            Dim ColLeftQTY As String = ""


            Dim OA_BRANDPACK_ID As Object = Me.GridEX1.GetValue("OA_BRANDPACK_ID")
            Me.SFG = StateFillingGrid.Filling
            Dim GridExDataView As DataView = CType(Me.GridEX1.DataSource, DataView)
            Dim index As Integer

            Dim tblAdjustment As New DataTable("T_Adjust")
            tblAdjustment.Columns.Add(New DataColumn("IDApp", Type.GetType("System.Int32")))
            tblAdjustment.Columns.Add(New DataColumn("ReleaseQty", Type.GetType("System.Decimal")))
            tblAdjustment.Columns("ReleaseQty").DefaultValue = 0D
            tblAdjustment.Columns.Add(New DataColumn("ABID", Type.GetType("System.String")))
            tblAdjustment.Columns("ABID").DefaultValue = ""
            tblAdjustment.Columns.Add(New DataColumn("PO_REF_NO", Type.GetType("System.String")))
            tblAdjustment.Columns("PO_REF_NO").DefaultValue = DBNull.Value
            tblAdjustment.Columns.Add("IsRemain", Type.GetType("System.Boolean"))
            tblAdjustment.Columns("IsRemain").DefaultValue = False
            tblAdjustment.AcceptChanges()
            ''reset structure OARemainding dan OABrandPackDiscount
            Me.clsOADiscount.OA_BRANDPACK_DISCOUNT = New NufarmBussinesRules.OrderAcceptance.OADiscount.OA_BD()
            Me.clsOADiscount.OA_Remainding = New NufarmBussinesRules.OrderAcceptance.OADiscount.OA_RM()

            'ambil price yang ada di pnl oa_discount
            Dim x As String = ""
            Dim w As Integer = Me.pnlOADiscount.TitleText.IndexOf(":") + 3
            Dim s As String
            Dim a As String = ""
            Do
                s = Microsoft.VisualBasic.Mid(Me.pnlOADiscount.TitleText, w, 1)
                If (s = "") Or (s = ".") Then
                Else
                    a &= s
                End If
                w += 1
            Loop Until s = ""
            DISC_PRICE_OA = Convert.ToDecimal(a)
            'GET TOTAL PRICE
            Dim SUMPRICE As Object = Me.clsOADiscount.GetTotalPriceOAGiven(Me.mcbRefNo.Value.ToString(), False) 'TOTAL DISCOUNT YG TELAH DIBERIKAN
            Dim TOTAL_LEFT_PRICE As Decimal = DISC_PRICE_OA - Convert.ToDecimal(SUMPRICE)
            Dim LEFT_QTY As Decimal = TOTAL_LEFT_PRICE / PRICE
            'DIM FC AS New Janus.Windows.GridEX.GridEXFilterCondition(,,
            If (AmountPrice + SUMPRICE) > DISC_PRICE_OA Then
                Me.ShowMessageInfo("Value will be more than should be given" & vbCrLf & "Discount price for OA " & _
                Me.mcbRefNo.Value.ToString() & " has " & String.Format("{0:#,##0.00}", SUMPRICE) & vbCrLf & "Amount that will be given is : " & _
                String.Format("{0:#,##0.00}", AmountPrice) & vbCrLf & "Amount that can be released for BrandPack " & Me.GridEX1.GetValue("BRANDPACK_NAME").ToString() & " = " & String.Format("{0:#,##0.000}", LEFT_QTY))
                Me.QTY.Show(Me.pnlOADiscount)
                Return
            End If
            Select Case Me.SDiscount
                Case SelectedDiscount.AgreementDiscount
                    If Me.rdbGivenDiscount.Checked = True Then
                        If Me.QTY.NumericEditBox1.Value > Convert.ToDecimal(Me.GridEX3.GetValue("AGREE_LEFT_QTY")) Then
                            Me.ShowMessageInfo("Quantity that shall be given,is more than it should be" & vbCrLf & "Quantity mustn't  be more than left Qty on bonus discount")
                            Me.QTY.Show(Me.pnlOADiscount, True)
                            Return
                        End If
                        If (Me.QTY.NumericEditBox1.Value Mod Me.Devided_Qty > 0) Or (Me.QTY.NumericEditBox1.Value < Devided_Qty) Then
                            Me.QTY.lblCanBeRealesed.Text = "Please type Correct value"
                            Me.QTY.Show(Me.pnlOADiscount, True)
                            Return
                        End If
                    Else
                        If Me.QTY.NumericEditBox1.Value > Convert.ToDecimal(Me.GridEX3.GetValue("LEFT_QTY")) Then
                            Me.ShowMessageInfo("Quantity that shall be given,is more than it should be" & vbCrLf & "Quantity mustn't  be more than left Qty on bonus discount")
                            Me.QTY.Show(pnlOADiscount, True)
                            Return
                        End If
                        If Convert.ToDecimal(Me.QTY.txtAdjust1.Value) > 0 Then
                            AdjustQty = Convert.ToDecimal(Me.QTY.txtAdjust1.Value)
                        End If
                        If Convert.ToDecimal(Me.QTY.txtAdjust2.Value) > 0 Then
                            AdjustQty = AdjustQty + Convert.ToDecimal(Me.QTY.txtAdjust1.Value)
                        End If
                    End If
                    Dim NewRow As DataRow = Nothing
                    Dim LeftQty0 As Decimal = 0D
                    Dim LeftQty1 As Decimal = 0D
                    Dim leftQTY As Decimal = totalQty Mod Devided_Qty
                    ResultQty = Decimal.Truncate(Me.QTY.NumericEditBox1.Value / Devided_Qty) * Devided_Qty
                    If (Convert.ToDecimal(Me.QTY.txtAdjust1.Value) + Convert.ToDecimal(Me.QTY.txtAdjust2.Value)) < Me.QTY.NumericEditBox1.Value Then
                        ProcessAdjustment(leftQTY, NewRow, tblAdjustment)
                        leftQTY = totalQty Mod Devided_Qty
                        ''test yang bukan remain
                        If leftQTY > 0 Then
                            If Me.QTY.NumericEditBox1.Value Mod Devided_Qty > 0 Then
                                If Convert.ToDecimal(Me.QTY.txtAdjust2.Value) > 0 And Convert.ToDecimal(Me.QTY.txtAdjust1.Value > 0) Then
                                    If Convert.ToDecimal(Me.QTY.txtAdjust2.Value) > 0 Then
                                        NewRow = tblAdjustment.NewRow()
                                        NewRow("IDApp") = Me.QTY.dt.Rows(1)("IDApp")
                                        NewRow("ReleaseQty") = Me.QTY.txtAdjust2.Value
                                        NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                                        NewRow.EndEdit()
                                        tblAdjustment.Rows.Add(NewRow)
                                    End If
                                    If Convert.ToDecimal(Me.QTY.txtAdjust1.Value) > 0 Then
                                        NewRow = tblAdjustment.NewRow()
                                        NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                                        NewRow("ReleaseQty") = Me.QTY.txtAdjust1.Value
                                        NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                                        NewRow.EndEdit()
                                        tblAdjustment.Rows.Add(NewRow)
                                    End If
                                ElseIf Convert.ToDecimal(Me.QTY.txtAdjust2.Value) > 0 Then
                                    NewRow = tblAdjustment.NewRow()
                                    NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                                    NewRow("ReleaseQty") = Me.QTY.txtAdjust2.Value
                                    NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                                    NewRow.EndEdit()
                                    tblAdjustment.Rows.Add(NewRow)
                                ElseIf Convert.ToDecimal(Me.QTY.txtAdjust1.Value) > 0 Then
                                    NewRow = tblAdjustment.NewRow()
                                    NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                                    NewRow("ReleaseQty") = Me.QTY.txtAdjust1.Value
                                    NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                                    NewRow.EndEdit()
                                    tblAdjustment.Rows.Add(NewRow)
                                End If
                            Else ''remain berarti ngambil dari adjustment
                                If Convert.ToDecimal(Me.QTY.txtAdjust2.Value) > 0 And Convert.ToDecimal(Me.QTY.txtAdjust1.Value > 0) Then
                                    If Convert.ToDecimal(Me.QTY.txtAdjust2.Value) Mod Devided_Qty > 0 Then
                                        NewRow = tblAdjustment.NewRow()
                                        NewRow("IDApp") = Me.QTY.dt.Rows(1)("IDApp")
                                        NewRow("ReleaseQty") = Me.QTY.txtAdjust2.Value - (Convert.ToDecimal(Me.QTY.txtAdjust2.Value) Mod Devided_Qty)
                                        NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                                        NewRow.EndEdit()
                                        tblAdjustment.Rows.Add(NewRow)
                                    End If
                                    If Convert.ToDecimal(Me.QTY.txtAdjust1.Value) Mod Devided_Qty > 0 Then
                                        NewRow = tblAdjustment.NewRow()
                                        NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                                        NewRow("ReleaseQty") = Me.QTY.txtAdjust1.Value - (Convert.ToDecimal(Me.QTY.txtAdjust1.Value) Mod Devided_Qty)
                                        NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                                        NewRow.EndEdit()
                                        tblAdjustment.Rows.Add(NewRow)
                                    End If
                                ElseIf Convert.ToDecimal(Me.QTY.txtAdjust2.Value) > 0 Then
                                    NewRow = tblAdjustment.NewRow()
                                    NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                                    NewRow("ReleaseQty") = Me.QTY.txtAdjust2.Value ''- (Convert.ToDecimal(Me.QTY.txtAdjust2.Value) Mod Devided_Qty)
                                    NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                                    NewRow.EndEdit()
                                    tblAdjustment.Rows.Add(NewRow)
                                ElseIf Convert.ToDecimal(Me.QTY.txtAdjust1.Value) > 0 Then
                                    NewRow = tblAdjustment.NewRow()
                                    NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                                    NewRow("ReleaseQty") = Me.QTY.txtAdjust1.Value ''- (Convert.ToDecimal(Me.QTY.txtAdjust1.Value) Mod Devided_Qty)
                                    NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                                    NewRow.EndEdit()
                                    tblAdjustment.Rows.Add(NewRow)
                                End If
                            End If
                        End If
                    Else
                        If Convert.ToDecimal(Me.QTY.txtAdjust1.Value) >= Convert.ToDecimal(Me.QTY.NumericEditBox1.Value) Then
                            NewRow = tblAdjustment.NewRow()
                            NewRow("IDApp") = Me.QTY.dt.Rows(0)("IDApp")
                            Dim RelQty As Decimal = Convert.ToDecimal(Me.QTY.NumericEditBox1.Value) '' - (Convert.ToDecimal(Me.QTY.NumericEditBox1.Value) Mod Devided_Qty)
                            NewRow("ReleaseQty") = IIf(RelQty > 0, RelQty, 0)
                            NewRow("PO_REF_NO") = Me.txtPOREFNO.Text
                            NewRow.EndEdit()
                            tblAdjustment.Rows.Add(NewRow)
                        End If
                    End If
                    With Me.clsOADiscount.OA_BRANDPACK_DISCOUNT
                        If Me.rdbGivenDiscount.Checked = True Then
                            ColLeftQTY = "AGREE_LEFT_QTY"
                            .AGREE_DISC_HIST_ID = Me.GridEX3.GetValue("AGREE_DISC_HIST_ID")
                            .GQSY_SGT_P_FLAG = "G"
                            .BRND_B_S_ID = DBNull.Value
                            .OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                            .OA_RM_ID = DBNull.Value
                            .ACHIEVEMENT_BRANDPACK_ID = DBNull.Value
                            .AdjustQty = 0
                        ElseIf Me.rdbPeriodDiscount1.Checked Or Me.rdbPeriodDiscount2.Checked Or _
                            Me.rdbPeriodDiscount3.Checked Or Me.rdbPeriodDiscount4.Checked Or Me.rdbSemesterly1Discount.Checked Or _
                            Me.rdbSemesterly2Discount.Checked Or Me.rdbYearlyDiscount.Checked Then
                            If Me.rdbPeriodDiscount1.Checked Then : .GQSY_SGT_P_FLAG = "Q1"
                            ElseIf Me.rdbPeriodDiscount2.Checked Then : .GQSY_SGT_P_FLAG = "Q2"
                            ElseIf Me.rdbPeriodDiscount3.Checked Then : .GQSY_SGT_P_FLAG = "Q3"
                            ElseIf Me.rdbPeriodDiscount4.Checked Then : .GQSY_SGT_P_FLAG = "Q4"
                            ElseIf Me.rdbSemesterly1Discount.Checked Then : .GQSY_SGT_P_FLAG = "S1"
                            ElseIf Me.rdbSemesterly2Discount.Checked Then : .GQSY_SGT_P_FLAG = "S2"
                            ElseIf Me.rdbYearlyDiscount.Checked Then : .GQSY_SGT_P_FLAG = "Y"
                            End If
                            ColLeftQTY = "LEFT_QTY"
                            If (NufarmBussinesRules.SharedClass.DISC_AGREE_FROM = "PO") Then
                                .BRND_B_S_ID = Me.GridEX3.GetValue("BRND_B_S_ID")
                                .ACHIEVEMENT_BRANDPACK_ID = DBNull.Value
                            Else
                                .ACHIEVEMENT_BRANDPACK_ID = Me.GridEX3.GetValue("ACHIEVEMENT_BRANDPACK_ID")
                                .BRND_B_S_ID = DBNull.Value
                            End If
                            .AGREE_DISC_HIST_ID = DBNull.Value
                            .OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                            .OA_RM_ID = DBNull.Value
                        Else
                            Me.ShowMessageInfo("Please Check option Discount Type")
                            Return
                        End If
                        .PRICE_PRQTY = PRICE
                        .MRKT_DISC_HIST_ID = DBNull.Value
                        .PROJ_DISC_HIST_ID = DBNull.Value
                        .MRK_M_S_ID = DBNull.Value
                    End With
                    If Me.rdbGivenDiscount.Checked = True Then
                        Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                       Me.QTY.NumericEditBox1.Value, Me.GridEX3.GetValue("AGREE_DISC_HIST_ID").ToString(), , , False, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), True)
                        If Convert.ToDecimal(Me.GridEX3.GetValue(ColLeftQTY)) > Me.QTY.NumericEditBox1.Value Then
                            'INSERT OA_REMAINDING
                            With Me.clsOADiscount.OA_Remainding
                                .AGREE_DISC_HIST_ID = Me.GridEX3.GetValue("AGREE_DISC_HIST_ID").ToString()
                                .BRND_B_S_ID = DBNull.Value
                                .ACHIEVMENT_BRANDPACK_ID = DBNull.Value
                                .FLAG = "G"
                                .MRKT_DISC_HISt_ID = DBNull.Value
                                .MRKT_M_S_ID = DBNull.Value
                                .OA_BRANDPACK_ID = OA_BRANDPACK_ID
                                .PRICE_PRQTY = Convert.ToDecimal(PRICE)
                                .PROJ_DISC_HIST_ID = DBNull.Value
                                .RM_OA_ID = DBNull.Value
                                leftQTY = Convert.ToDecimal(Me.GridEX3.GetValue(ColLeftQTY)) - Me.QTY.NumericEditBox1.Value
                                Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                                leftQTY, "G", Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), .AGREE_DISC_HIST_ID, , , , False, OA_BRANDPACK_ID, True)
                            End With
                        End If
                    ElseIf Not IsDBNull(Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.BRND_B_S_ID) _
                    Or Not IsDBNull(Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.ACHIEVEMENT_BRANDPACK_ID) Then
                        Dim hasChildTrans As Boolean = False
                        If Me.QTY.chkSetAdjustment.Checked Then
                            hasChildTrans = IIf((Convert.ToDecimal(Me.QTY.txtAdjust2.Value > 0) Or Convert.ToDecimal(Me.QTY.txtAdjust1.Value) > 0), True, False)
                        End If
                        With Me.clsOADiscount.OA_Remainding
                            .tblAdjustment = tblAdjustment
                            .AGREE_DISC_HIST_ID = DBNull.Value
                            If Me.GridEX3.RootTable.Columns.Contains("BRND_B_S_ID") Then
                                .BRND_B_S_ID = Me.GridEX3.GetValue("BRND_B_S_ID")
                                .ACHIEVMENT_BRANDPACK_ID = DBNull.Value
                            Else
                                .ACHIEVMENT_BRANDPACK_ID = Me.GridEX3.GetValue("ACHIEVEMENT_BRANDPACK_ID")
                                .BRND_B_S_ID = DBNull.Value
                            End If
                            .FLAG = Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG
                            .MRKT_DISC_HISt_ID = DBNull.Value
                            .MRKT_M_S_ID = DBNull.Value
                            .OA_BRANDPACK_ID = OA_BRANDPACK_ID
                            .PRICE_PRQTY = PRICE
                            .PROJ_DISC_HIST_ID = DBNull.Value
                            .RM_OA_ID = DBNull.Value
                        End With
                        'ResultQty = Decimal.Truncate(Me.QTY.NumericEditBox1.Value / Devided_Qty) * Devided_Qty
                        If (Convert.ToDecimal(Me.QTY.txtAdjust1.Value) + Convert.ToDecimal(Me.QTY.txtAdjust2.Value)) < Me.QTY.NumericEditBox1.Value Then
                            If totalQty Mod Devided_Qty > 0 Then
                                PrimaryIdent = Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                                                           ResultQty, , , , False, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), True)
                                If hasChildTrans Then 'yang masuk ke remainding adalah yang 
                                    Me.clsOADiscount.InsertAdjustment(True, True, True, PrimaryIdent)
                                Else 'INSERT KE OA_REMAINDING
                                    leftQTY = Me.QTY.NumericEditBox1.Value Mod Devided_Qty
                                    Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, leftQTY, Me.clsOADiscount.OA_Remainding.FLAG, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), , , , , False, OA_BRANDPACK_ID, True)
                                End If
                            Else
                                PrimaryIdent = Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                                ResultQty, , , , False, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), hasChildTrans)
                                If hasChildTrans Then
                                    Me.clsOADiscount.InsertAdjustment(False, True, True, PrimaryIdent)
                                End If
                            End If
                        Else
                            If (Me.QTY.txtAdjust1.Value) >= Me.QTY.NumericEditBox1.Value Then
                                ResultQty = Me.QTY.NumericEditBox1.Value
                            End If
                            PrimaryIdent = Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                                                            ResultQty, , , , False, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), hasChildTrans)
                            If hasChildTrans Then
                                Me.clsOADiscount.InsertAdjustment(False, True, True, PrimaryIdent)
                            End If
                        End If
                    End If
                Case SelectedDiscount.MarketingDiscount
                    If Me.QTY.NumericEditBox1.Value > Convert.ToDecimal(Me.GridEX3.GetValue("MRKT_LEFT_QTY")) Then
                        Me.ShowMessageInfo("Quantity that shall be given,is more than it should be" & vbCrLf & "Quantity mustn't  be more than left Qty on bonus discount")
                        Me.QTY.Show(Me.pnlOADiscount, True)
                        Return
                    End If
                    If (Me.rdbGivenCP.Checked = True) Or (Me.rdbGivenDK.Checked = True) Or (Me.rdbGivenPKPP.Checked = True) Or (Me.rdbGivenDiscountMarketing.Checked = True) _
                    Or (Me.rdbGivenCPR.Checked = True) Or (Me.rdbSpecialCPD.Checked) Or (Me.rdbGivenCP_TM.Checked = True) Or (Me.rdbSpecialCPD_TM.Checked = True) Or (Me.rdbCPMRT_Dist.Checked) Or (Me.rdbCPMRT_TMDist.Checked) _
                    Or (Me.rdbDKN.Checked) Or (Me.rdbCPDAuto.Checked) Then
                        If (Me.QTY.NumericEditBox1.Value Mod Devided_Qty) > 0 Or (Me.QTY.NumericEditBox1.Value < Devided_Qty) Then
                            Me.QTY.lblCanBeRealesed.Text = "Please type Correct value"
                            Me.QTY.Show(Me.pnlOADiscount)
                            Return
                        End If
                    End If
                    With Me.clsOADiscount.OA_BRANDPACK_DISCOUNT
                        If Me.rdbGivenDiscountMarketing.Checked Or Me.rdbGivenCP.Checked Or Me.rdbGivenCPR.Checked Or _
                        Me.rdbGivenDK.Checked Or Me.rdbGivenPKPP.Checked Or Me.rdbSpecialCPD.Checked Or Me.rdbGivenCP_TM.Checked = True _
                        Or Me.rdbSpecialCPD_TM.Checked = True Or Me.rdbCPMRT_Dist.Checked Or Me.rdbCPMRT_TMDist.Checked Or Me.rdbDKN.Checked Or Me.rdbCPDAuto.Checked Then
                            If Me.rdbGivenCP.Checked Then : .GQSY_SGT_P_FLAG = "CP"
                            ElseIf Me.rdbSpecialCPD.Checked Then : .GQSY_SGT_P_FLAG = "CS"
                            ElseIf Me.rdbGivenCP_TM.Checked Then : .GQSY_SGT_P_FLAG = "TD"
                            ElseIf Me.rdbSpecialCPD_TM.Checked Then : .GQSY_SGT_P_FLAG = "TS"
                            ElseIf Me.rdbGivenCPR.Checked Then : .GQSY_SGT_P_FLAG = "CR"
                            ElseIf Me.rdbGivenDK.Checked Then : .GQSY_SGT_P_FLAG = "DK"
                            ElseIf Me.rdbGivenPKPP.Checked Then : .GQSY_SGT_P_FLAG = "KP"
                            ElseIf Me.rdbGivenDiscountMarketing.Checked Then : .GQSY_SGT_P_FLAG = "MG"
                            ElseIf Me.rdbCPMRT_Dist.Checked Then : .GQSY_SGT_P_FLAG = "CD"
                            ElseIf Me.rdbCPMRT_TMDist.Checked Then : .GQSY_SGT_P_FLAG = "CT"
                            ElseIf Me.rdbDKN.Checked Then : .GQSY_SGT_P_FLAG = "DN"
                            ElseIf Me.rdbCPDAuto.Checked Then : .GQSY_SGT_P_FLAG = "CA"
                            End If
                            ColLeftQTY = "MRKT_LEFT_QTY"
                            .MRKT_DISC_HIST_ID = Me.GridEX3.GetValue("MRKT_DISC_HIST_ID")
                            .OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                            .MRK_M_S_ID = DBNull.Value
                            '.GQSY_SGT_P_FLAG = "MG"
                            .OA_RM_ID = DBNull.Value
                        Else
                            Me.ShowMessageInfo("Please check list option discount type")
                            Return
                        End If
                        .PRICE_PRQTY = PRICE
                        .AGREE_DISC_HIST_ID = DBNull.Value
                        .PROJ_DISC_HIST_ID = DBNull.Value
                        .BRND_B_S_ID = DBNull.Value
                        .ACHIEVEMENT_BRANDPACK_ID = DBNull.Value
                    End With
                    If (Me.rdbGivenDiscountMarketing.Checked = True) Or (Me.rdbGivenCP.Checked = True) Or (Me.rdbGivenDK.Checked = True) Or (Me.rdbGivenPKPP.Checked = True) Or (Me.rdbGivenCPR.Checked = True) Or (Me.rdbSpecialCPD.Checked) _
                        Or (Me.rdbSpecialCPD_TM.Checked) Or (Me.rdbGivenCP_TM.Checked) Or (Me.rdbCPMRT_Dist.Checked) Or (Me.rdbCPMRT_TMDist.Checked) Or (Me.rdbDKN.Checked) Or (Me.rdbCPDAuto.Checked) Then
                        Dim hasChildren As Boolean = Convert.ToDecimal(Me.GridEX3.GetValue(ColLeftQTY)) > Me.QTY.NumericEditBox1.Value
                        Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                        Me.QTY.NumericEditBox1.Value, , Me.GridEX3.GetValue("MRKT_DISC_HIST_ID").ToString(), , False, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), hasChildren)
                        If hasChildren Then
                            'INSERT OA_REMAINDING
                            With Me.clsOADiscount.OA_Remainding
                                .AGREE_DISC_HIST_ID = DBNull.Value
                                .BRND_B_S_ID = DBNull.Value
                                .ACHIEVMENT_BRANDPACK_ID = DBNull.Value
                                If Me.rdbGivenDiscountMarketing.Checked = True Then
                                    .FLAG = "MG"
                                ElseIf Me.rdbGivenCP.Checked = True Then
                                    .FLAG = "CP"
                                ElseIf Me.rdbSpecialCPD.Checked = True Then
                                    .FLAG = "CS"
                                ElseIf Me.rdbGivenCPR.Checked = True Then
                                    .FLAG = "CR"
                                ElseIf Me.rdbGivenDK.Checked = True Then
                                    .FLAG = "DK"
                                ElseIf Me.rdbGivenPKPP.Checked = True Then
                                    .FLAG = "KP"
                                ElseIf Me.rdbSpecialCPD_TM.Checked Then
                                    .FLAG = "TS"
                                ElseIf Me.rdbGivenCP_TM.Checked Then
                                    .FLAG = "TD"
                                ElseIf Me.rdbCPMRT_Dist.Checked Then
                                    .FLAG = "CD"
                                ElseIf Me.rdbCPMRT_TMDist.Checked Then
                                    .FLAG = "CT"
                                ElseIf Me.rdbDKN.Checked Then
                                    .FLAG = "DN"
                                ElseIf Me.rdbCPDAuto.Checked Then
                                    .FLAG = "CA"
                                End If
                                .MRKT_DISC_HISt_ID = Me.GridEX3.GetValue("MRKT_DISC_HIST_ID")
                                .MRKT_M_S_ID = DBNull.Value
                                .OA_BRANDPACK_ID = OA_BRANDPACK_ID
                                .PRICE_PRQTY = Convert.ToDecimal(PRICE)
                                .PROJ_DISC_HIST_ID = DBNull.Value
                                .RM_OA_ID = DBNull.Value
                                Dim LeftQTY = Convert.ToDecimal(Me.GridEX3.GetValue(ColLeftQTY)) - Me.QTY.NumericEditBox1.Value
                                Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                                LeftQTY, .FLAG, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), , Me.GridEX3.GetValue("MRKT_DISC_HIST_ID").ToString(), , , False, OA_BRANDPACK_ID.ToString(), True)
                            End With
                        End If
                    ElseIf Me.rdbTSDiscountMarketing.Checked = True Then
                        If Me.QTY.NumericEditBox1.Value > Devided_Qty Then
                            ResultQty = Decimal.Truncate(Me.QTY.NumericEditBox1.Value / Devided_Qty) * Devided_Qty
                            If Me.QTY.NumericEditBox1.Value Mod Devided_Qty > 0 Then
                                Dim leftQTY As Decimal = Me.QTY.NumericEditBox1.Value Mod Devided_Qty
                                Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                                ResultQty, , , , False, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), True)
                                'INSERT KE OA_REMAINDING
                                With Me.clsOADiscount.OA_Remainding
                                    .AGREE_DISC_HIST_ID = DBNull.Value
                                    .BRND_B_S_ID = DBNull.Value
                                    .ACHIEVMENT_BRANDPACK_ID = DBNull.Value
                                    .FLAG = Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG
                                    .MRKT_DISC_HISt_ID = DBNull.Value
                                    .MRKT_M_S_ID = Me.GridEX3.GetValue("MRKT_M_S_ID")
                                    .OA_BRANDPACK_ID = OA_BRANDPACK_ID
                                    .PRICE_PRQTY = Convert.ToDecimal(PRICE)
                                    .PROJ_DISC_HIST_ID = DBNull.Value
                                    .RM_OA_ID = DBNull.Value
                                    Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                                    leftQTY, .FLAG, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), , , .MRKT_M_S_ID, , False, OA_BRANDPACK_ID.ToString(), True)
                                End With
                            Else
                                Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                                                           ResultQty, , , , False, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False)
                            End If
                        Else
                            Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                             Me.QTY.NumericEditBox1.Value, , , , False, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False)
                        End If
                    End If

                Case SelectedDiscount.OtherDiscount

                Case SelectedDiscount.ProjectDiscount
                    If (Me.QTY.NumericEditBox1.Value > Me.GridEX3.GetValue("PROJ_LEFT_QTY")) Then
                        Me.ShowMessageInfo("Quantity that shall be given,is more than it should be" & vbCrLf & "Quantity mustn't  be more than left Qty on bonus discount")
                        Me.QTY.Show(Me.pnlOADiscount, True)
                        Return
                    End If
                    With Me.clsOADiscount.OA_BRANDPACK_DISCOUNT
                        ColLeftQTY = "PROJ_LEFT_QTY"
                        .AGREE_DISC_HIST_ID = DBNull.Value
                        .GQSY_SGT_P_FLAG = "P"
                        .MRKT_DISC_HIST_ID = DBNull.Value
                        .PRICE_PRQTY = Convert.ToDecimal(PRICE)
                        .PROJ_DISC_HIST_ID = Me.GridEX3.GetValue("PROJ_DISC_HIST_ID")
                        .BRND_B_S_ID = DBNull.Value
                        .ACHIEVEMENT_BRANDPACK_ID = DBNull.Value
                        .MRK_M_S_ID = DBNull.Value
                        .OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                        .OA_RM_ID = DBNull.Value
                    End With
                    If Me.QTY.NumericEditBox1.Value Mod Devided_Qty > 0 Then
                        Me.QTY.lblCanBeRealesed.Text = "Please type Correct value"
                        Me.QTY.Show(Me.pnlOADiscount)
                        Return
                    End If
                    If Me.QTY.NumericEditBox1.Value > Devided_Qty Then
                        ResultQty = Decimal.Truncate(Me.QTY.NumericEditBox1.Value / Devided_Qty) * Devided_Qty
                        If Me.QTY.NumericEditBox1.Value Mod Devided_Qty > 0 Then
                            Dim leftQTY As Decimal = Me.QTY.NumericEditBox1.Value Mod Devided_Qty
                            Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.ProjectDiscount, _
                            ResultQty, , , Me.GridEX3.GetValue("PROJ_DISC_HIST_ID").ToString(), False, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), True)
                            'INSERT KE OA_REMAINDING
                            With Me.clsOADiscount.OA_Remainding
                                .AGREE_DISC_HIST_ID = DBNull.Value
                                .BRND_B_S_ID = DBNull.Value
                                .FLAG = Me.clsOADiscount.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG
                                .MRKT_DISC_HISt_ID = DBNull.Value
                                .MRKT_M_S_ID = DBNull.Value
                                .OA_BRANDPACK_ID = OA_BRANDPACK_ID
                                .PRICE_PRQTY = Convert.ToDecimal(PRICE)
                                .PROJ_DISC_HIST_ID = Me.GridEX3.GetValue("PROJ_DISC_HIST_ID")
                                .RM_OA_ID = DBNull.Value
                                Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.ProjectDiscount, _
                                 leftQTY, .FLAG, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), , , , Me.GridEX3.GetValue("PROJ_DISC_HIST_ID").ToString(), False, OA_BRANDPACK_ID.ToString(), True)
                            End With
                        End If
                        Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.ProjectDiscount, _
                        ResultQty.ToString, , , Me.GridEX3.GetValue("PROJ_DISC_HIST_ID").ToString(), False, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False)
                    Else
                        Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.ProjectDiscount, _
                        Me.QTY.NumericEditBox1.Value, , , Me.GridEX3.GetValue("PROJ_DISC_HIST_ID").ToString(), False, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False)
                    End If
            End Select
            Me.SFG = StateFillingGrid.Filling
            index = GridExDataView.Find(OA_BRANDPACK_ID)
            If Not Me.SDiscount = SelectedDiscount.None Then
                If index <> -1 Then
                    If OA_BRANDPACK_DISC_QTY > Devided_Qty Then
                        OA_BRANDPACK_DISC_QTY = Decimal.Truncate(OA_BRANDPACK_DISC_QTY / Devided_Qty) * Devided_Qty
                    End If
                    Dim TotalDiscQty As Decimal = Convert.ToDecimal(GridExDataView(index)("TOTAL_DISC_QTY")) + OA_BRANDPACK_DISC_QTY
                    GridExDataView(index)("TOTAL_DISC_QTY") = TotalDiscQty
                    GridExDataView(index)("TOTAL_AMOUNT_DISC") = TotalDiscQty * Convert.ToDecimal(PRICE)
                    GridExDataView(index).EndEdit()
                    Me.BindGridEx(Me.GridEX1, GridExDataView)
                End If
            End If
            Me.SFG = StateFillingGrid.Filling
            Me.GridEX1.Refetch() : Me.GridEX1.Row = indexRow
            Me.SFG = StateFillingGrid.HasFilled
            Me.NavigationPane1_ItemClick(Me.NavigationPane1, New EventArgs())
            Me.clsOADiscount.CreateViewOABrandPackDiscount(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False)
            Me.BindGridEx(Me.GridEX2, Me.clsOADiscount.ViewOADiscount) : Me.SFG = StateFillingGrid.HasFilled
            Dim UnitOnOrder As String = ""
            If Not IsDBNull(Me.GridEX1.GetValue("UNIT_ORDER")) Then
                UnitOnOrder = Me.GridEX1.GetValue("UNIT_ORDER").ToString()
            End If
            Dim OrgDecimalValue As Decimal = Me.clsOADiscount.getTotalQTY(Me.GridEX1.GetValue("OA_BRANDPACK_ID"), True)
            LEFT_QTY = OrgDecimalValue Mod Devided_Qty
            Me.pnlTotalRemainder.Text = "Still Remainder =  " & String.Format("{0:#,##0.000}", LEFT_QTY * Devide_Factor) & "  " & Unit & ", = " & LEFT_QTY.ToString() & "  " & UnitOnOrder
            Me.QTY.Close() : Me.QTY = Nothing
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_lnkOK_Click")
            Me.QTY.Show(Me.pnlOADiscount, True)
        Finally
            If Not Me.SFG = StateFillingGrid.HasFilled Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub lnkCancel_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles QTY.lnkCancelClick
        Try
            Me.QTY.Close()
            Me.QTY = Nothing
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub Other_QTY_lnkCancelClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    '    Try
    '        Me.Other_QTY.Close()
    '        Me.Other_QTY = Nothing
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Function CanAccomodateDisc(ByVal TotalDisc) As Boolean
        Dim PRICE As Decimal = Convert.ToDecimal(Me.GridEX1.GetValue("OA_PRICE_PERQTY"))
        Dim AmountPrice As Decimal = 0
        AmountPrice = PRICE * TotalDisc
        'ambil price yang ada di pnl oa_discount
        Dim DISC_PRICE_OA As Decimal = 0
        Dim x As String = ""
        Dim w As Integer = Me.pnlOADiscount.TitleText.IndexOf(":") + 3
        Dim s As String
        Dim a As String = ""
        Do
            s = Microsoft.VisualBasic.Mid(Me.pnlOADiscount.TitleText, w, 1)
            If (s = "") Or (s = ".") Then
            Else
                a &= s
            End If
            w += 1
        Loop Until s = ""
        'GET TOTAL PRICE
        DISC_PRICE_OA = Convert.ToDecimal(a) 'TOTAL DISCOUNT OA
        Dim SUMPRICE As Object = Me.clsOADiscount.GetTotalPriceOAGiven(Me.mcbRefNo.Value.ToString(), False) 'TOTAL DISCOUNT YG TELAH DIBERIKAN
        Dim TOTAL_LEFT_PRICE As Decimal = DISC_PRICE_OA - Convert.ToDecimal(SUMPRICE)
        Dim LEFT_QTY_PRICE As Decimal = TOTAL_LEFT_PRICE / Convert.ToDecimal(PRICE)
        If (AmountPrice + SUMPRICE) > DISC_PRICE_OA Then
            Me.ShowMessageInfo("Discount value will exceed allowed-data" & vbCrLf & "Discount has been released " & _
            String.Format("{0:#,##0.00}", SUMPRICE) & vbCrLf & "Total left discount allowed is : " & String.Format("{0:#,##0.000}", LEFT_QTY_PRICE))
            'Me.Other_QTY.Show(Me.pnlOADiscount)
            Return False
        End If
        Return True
    End Function
    Private Sub fillDtEvenRemain(ByVal tblResult As DataTable, ByVal TotalLimitDiscQty As Decimal, ByRef dtEven As DataTable, ByRef dtRemain As DataTable)
        dtEven.Clear()
        dtRemain.Clear()
        Dim TotalDiscEven As Decimal = 0
        Dim remainDisc As Decimal = 0
        For Each Row As DataRow In tblResult.Rows

            If TotalDiscEven + CDec(Row("INC_DISC")) <= TotalLimitDiscQty Then
                dtEven.ImportRow(Row)
                TotalDiscEven += CDec(Row("INC_DISC"))
            ElseIf TotalDiscEven < TotalLimitDiscQty Then
                'ambil kekurangannyanya biar genap
                Dim MinusEvenQty As Decimal = TotalLimitDiscQty - TotalDiscEven
                remainDisc = CDec(Row("INC_DISC")) - MinusEvenQty
                'insert row to dtEven with Disc MinusEvenQty
                Row.BeginEdit()
                Row("INC_DISC") = MinusEvenQty
                Row.EndEdit()
                Row.AcceptChanges()
                dtEven.ImportRow(Row)

                'insert row to dtRemain with disc remainDisc
                Row.BeginEdit()
                Row("INC_DISC") = remainDisc
                Row.EndEdit()
                Row.AcceptChanges()
                dtRemain.ImportRow(Row)
                TotalDiscEven += MinusEvenQty
            Else
                dtRemain.ImportRow(Row)
            End If
        Next
        If dtEven.Rows.Count > 0 Then
            For i As Integer = 0 To dtEven.Rows.Count - 1
                dtEven.Rows(i).SetAdded()
            Next
        Else
            dtEven = Nothing
        End If
        If dtRemain.Rows.Count > 0 Then
            For i As Integer = 0 To dtRemain.Rows.Count - 1
                dtRemain.Rows(i).SetAdded()
            Next
        Else
            dtRemain = Nothing
        End If

    End Sub

    Private Function InsertOthersDDDRCBD(ByVal tblResult As DataTable, ByRef TotalDiscOut As Decimal) As Boolean

        Me.Cursor = Cursors.WaitCursor
        Dim OrgDecimalValue As Decimal = Me.clsOADiscount.getTotalQTY(Me.GridEX1.GetValue("OA_BRANDPACK_ID"), False)
        'Dim Devided_Qty As Decimal = Me.clsOADiscount.GetDevided_QTY(Me.GridEX1.GetValue("BRANDPACK_ID").ToString())
        Dim LeftQTY As Decimal = OrgDecimalValue Mod Devided_Qty 'sisa dari yang bisa di bagi genap
        ''jumlahkan total discount
        Dim oTotalDisc As Object = tblResult.Compute("SUM(INC_DISC)", "")
        Dim TotalDisc As Decimal = 0
        If Not IsNothing(oTotalDisc) And Not IsDBNull(oTotalDisc) Then
            TotalDisc = Convert.ToDecimal(oTotalDisc)
        Else
            Return False
        End If
        Dim TotalLimitDiscQty As Decimal = 0 'batasan untuk memberikan discount = ResultQty
        Dim MinusDevidedQty As Decimal = 0 'untuk mengetahu kekurangan nya discount agar genap
        Dim lastRemain As Decimal = 0 'Nilai decimal dari remaind yang tidak bisa di ambil
        Dim dtRemain As DataTable = tblResult.Copy()

        Dim dtEven As DataTable = tblResult.Copy()
        If LeftQTY > 0 Then
            'untuk menjadikan genap = devided_qty - kekurangan nya = 
            'Devided_qty - LeftQty
            MinusDevidedQty = Devided_Qty - LeftQTY
            If TotalDisc < MinusDevidedQty Then
                'insert reminding
                For i As Integer = 0 To dtRemain.Rows.Count - 1
                    dtRemain.Rows(i).SetAdded()
                Next
                Me.clsOADiscount.InserDDDRCBD(Nothing, dtRemain)
            Else
                TotalLimitDiscQty = MinusDevidedQty
                TotalDisc = TotalDisc - MinusDevidedQty
                'sisanya
                If TotalDisc >= Devided_Qty Then
                    If TotalDisc Mod Devided_Qty > 0 Then
                        lastRemain = TotalDisc Mod Devided_Qty
                        TotalLimitDiscQty = TotalLimitDiscQty + (TotalDisc - lastRemain)
                    Else
                        TotalLimitDiscQty = TotalLimitDiscQty + TotalDisc
                    End If
                End If
                If CanAccomodateDisc(TotalLimitDiscQty) Then
                    ''proses data
                    'insert data
                    Me.fillDtEvenRemain(tblResult, TotalLimitDiscQty, dtEven, dtRemain)
                    If Not IsNothing(dtEven) Or Not IsNothing(dtRemain) Then
                        Me.clsOADiscount.InserDDDRCBD(dtEven, dtRemain)
                    End If
                Else
                    Return False
                End If
                TotalDiscOut = TotalLimitDiscQty
            End If
        Else
            If TotalDisc < Devided_Qty Then
                For i As Integer = 0 To dtRemain.Rows.Count - 1
                    dtRemain.Rows(i).SetAdded()
                Next
                ''insert ke remainding saja
                Me.clsOADiscount.InserDDDRCBD(Nothing, dtRemain)
            Else
                If TotalDisc Mod Devided_Qty > 0 Then
                    lastRemain = TotalDisc Mod Devided_Qty
                    TotalLimitDiscQty = TotalDisc - lastRemain
                Else
                    TotalLimitDiscQty = TotalDisc
                End If
                If CanAccomodateDisc(TotalLimitDiscQty) Then
                    ''proses data
                    'insert data
                    Me.fillDtEvenRemain(tblResult, TotalLimitDiscQty, dtEven, dtRemain)
                    If Not IsNothing(dtEven) Or Not IsNothing(dtRemain) Then
                        Me.clsOADiscount.InserDDDRCBD(dtEven, dtRemain)
                    End If
                Else
                    Return False
                End If
                TotalDiscOut = TotalLimitDiscQty
            End If
        End If
    End Function

    'Private Function Other_QTY_OK(ByVal ResultOQty As Decimal, ByVal flag As String, ByVal RefOther As Integer) As Boolean

    '    Me.Cursor = Cursors.WaitCursor
    '    Dim LeftQTY As Decimal = ResultOQty Mod Me.Devided_Qty
    '    Dim ResultQTY As Decimal = (Decimal.Truncate(ResultOQty / Me.Devided_Qty)) * Me.Devided_Qty
    '    Dim PRICE As Decimal = Convert.ToDecimal(Me.GridEX1.GetValue("OA_PRICE_PERQTY"))
    '    Dim AmountPrice As Decimal = 0
    '    AmountPrice = PRICE * ResultQTY
    '    'ambil price yang ada di pnl oa_discount
    '    Dim DISC_PRICE_OA As Decimal = 0
    '    Dim x As String = ""
    '    Dim w As Integer = Me.pnlOADiscount.TitleText.IndexOf(":") + 3
    '    Dim s As String
    '    Dim a As String = ""
    '    Do
    '        s = Microsoft.VisualBasic.Mid(Me.pnlOADiscount.TitleText, w, 1)
    '        If (s = "") Or (s = ".") Then
    '        Else
    '            a &= s
    '        End If
    '        w += 1
    '    Loop Until s = ""
    '    'GET TOTAL PRICE
    '    DISC_PRICE_OA = Convert.ToDecimal(a) 'TOTAL DISCOUNT OA
    '    Dim SUMPRICE As Object = Me.clsOADiscount.GetTotalPriceOAGiven(Me.mcbRefNo.Value.ToString(), False) 'TOTAL DISCOUNT YG TELAH DIBERIKAN
    '    Dim TOTAL_LEFT_PRICE As Decimal = DISC_PRICE_OA - Convert.ToDecimal(SUMPRICE)
    '    Dim LEFT_QTY_PRICE As Decimal = TOTAL_LEFT_PRICE / Convert.ToDecimal(PRICE)
    '    If (AmountPrice + SUMPRICE) > DISC_PRICE_OA Then
    '        Me.ShowMessageInfo("Discount value will exceed allowed-data" & vbCrLf & "Discount has been released " & _
    '        String.Format("{0:#,##0.00}", SUMPRICE) & vbCrLf & "Total left discount allowed is : " & String.Format("{0:#,##0.000}", LEFT_QTY_PRICE))
    '        'Me.Other_QTY.Show(Me.pnlOADiscount)
    '        Return False
    '    End If
    '    Dim IndexRow As Integer = Me.GridEX1.Row
    '    Dim OA_BRANDPACK_DISC_QTY As Decimal = ResultQTY
    '    Dim OA_BRANDPACK_ID As Object = Me.GridEX1.GetValue("OA_BRANDPACK_ID")
    '    Me.SFG = StateFillingGrid.Filling
    '    Dim GridExDataView As DataView = CType(Me.GridEX1.DataSource, DataView)
    '    Dim index As Integer

    '    If LeftQTY > 0 Then
    '        'jadikan nilai remainding dulu
    '        With Me.clsOADiscount.OA_Remainding
    '            .OA_BRANDPACK_ID = OA_BRANDPACK_ID
    '            .AGREE_DISC_HIST_ID = DBNull.Value
    '            .BRND_B_S_ID = DBNull.Value
    '            .ACHIEVMENT_BRANDPACK_ID = DBNull.Value
    '            .FLAG = flag
    '            .MRKT_DISC_HISt_ID = DBNull.Value
    '            .MRKT_M_S_ID = DBNull.Value
    '            .PROJ_DISC_HIST_ID = DBNull.Value
    '            .PRICE_PRQTY = PRICE
    '            .RM_OA_ID = DBNull.Value
    '            .RefOther = RefOther
    '        End With
    '        'INSERT KE TABLE OA_BRANDPACK_DISC
    '        'INSERT KE TABLE ORDR_OA_REMAINDING
    '        Me.clsOADiscount.InsertOA_BRANDPACK_DISC(ResultQTY, LeftQTY, OA_BRANDPACK_ID, False)
    '    ElseIf ResultQTY > 0 Then
    '        With Me.clsOADiscount.OA_BRANDPACK_DISCOUNT
    '            .AGREE_DISC_HIST_ID = DBNull.Value
    '            .MRK_M_S_ID = DBNull.Value
    '            .BRND_B_S_ID = DBNull.Value
    '            .ACHIEVEMENT_BRANDPACK_ID = DBNull.Value
    '            .GQSY_SGT_P_FLAG = flag
    '            .MRKT_DISC_HIST_ID = DBNull.Value
    '            .OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
    '            .PRICE_PRQTY = PRICE
    '            .PROJ_DISC_HIST_ID = DBNull.Value
    '            .OA_RM_ID = DBNull.Value
    '            .BRANDPACK_ID = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
    '            .RefOther = RefOther
    '        End With
    '        Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.OtherDiscount, _
    '        ResultQTY, , , , True, Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString())
    '    End If
    '    Me.SFG = StateFillingGrid.Filling
    '    index = CType(Me.GridEX1.DataSource, DataView).Find(OA_BRANDPACK_ID)
    '    If index <> -1 Then
    '        Dim TotalDiscQty As Decimal = Convert.ToDecimal(GridExDataView(index)("TOTAL_DISC_QTY")) + ResultQTY
    '        GridExDataView(index)("TOTAL_DISC_QTY") = TotalDiscQty
    '        GridExDataView(index)("TOTAL_AMOUNT_DISC") = TotalDiscQty * Convert.ToDecimal(PRICE)
    '        GridExDataView(index).EndEdit() : Me.GridEX1.Refetch()
    '    End If
    '    Me.GridEX1.Row = IndexRow
    '    Me.clsOADiscount.CreateViewOABrandPackDiscount(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False)
    '    Me.BindGridEx(Me.GridEX2, Me.clsOADiscount.ViewOADiscount)
    '    Dim UnitOnOrder As String = ""
    '    If Not IsDBNull(Me.GridEX1.GetValue("UNIT_ORDER")) Then
    '        UnitOnOrder = Me.GridEX1.GetValue("UNIT_ORDER").ToString()
    '    End If
    '    Dim OrgDecimalValue As Decimal = Me.clsOADiscount.getTotalQTY(Me.GridEX1.GetValue("OA_BRANDPACK_ID"), True)
    '    Dim LEFT_QTY = OrgDecimalValue Mod Devided_Qty
    '    Me.pnlTotalRemainder.Text = "Still Remainder =  " & String.Format("{0:#,##0.000}", LEFT_QTY * Devide_Factor) & "  " & Unit & ", = " & LEFT_QTY.ToString() & "  " & UnitOnOrder
    '    Me.SFG = StateFillingGrid.HasFilled
    'End Function

    'Private Sub Other_QTY_txtPercentage_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Other_QTY.txtPercentage_Changed
    '    Try
    '        If (Not (Me.Other_QTY.txtPercentage.Value Is Nothing)) Or (Me.Other_QTY.txtPercentage.Value = 0) Then
    '            Me.Other_QTY.txtResult.Value = (Me.Other_QTY.txtPercentage.Value / 100) * Convert.ToDecimal(Me.GridEX1.GetValue("OA_ORIGINAL_QTY"))
    '        Else
    '            Me.Other_QTY.txtResult.Value = 0
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub OtherDDDRCBD_txtPercentage_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles OtherDDDRCBD.txtPercentage_Changed
        Try
            If (Not (Me.OtherDDDRCBD.txtPercentage.Value Is Nothing)) Or (Me.OtherDDDRCBD.txtPercentage.Value = 0) Then
                Me.OtherDDDRCBD.txtResult.Value = (Me.OtherDDDRCBD.txtPercentage.Value / 100) * Convert.ToDecimal(Me.GridEX1.GetValue("OA_ORIGINAL_QTY"))
            Else
                Me.OtherDDDRCBD.txtResult.Value = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LftQTY_LeftQtyCancel(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LftQTY.LeftQtyCancel
        Try
            If Not IsNothing(Me.LftQTY) Then
                Me.LftQTY.Dispose()
                Me.LftQTY = Nothing
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LftQTY_LeftQtyOK(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LftQTY.LeftQtyOK
        Try
            Me.Cursor = Cursors.WaitCursor : Dim colLeftQTY As String = "" : Dim Flag As String = ""
            Dim OA_BRANDPACK_ID As String = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
            Dim IndexRow As Integer = Me.GridEX1.Row
            With Me.clsOADiscount.OA_Remainding
                If Me.DDF = DataDragFrom.GridEx3 Then
                    Select Case Me.SDiscount
                        Case SelectedDiscount.AgreementDiscount
                            If Me.rdbGivenDiscount.Checked Then : Flag = "G" : .FLAG = "G"
                            ElseIf Me.rdbPeriodDiscount1.Checked Then : .FLAG = "Q1" : Flag = "Q1"
                            ElseIf Me.rdbPeriodDiscount2.Checked = True Then : .FLAG = "Q2" : Flag = "Q2"
                            ElseIf Me.rdbPeriodDiscount3.Checked Then : .FLAG = "Q3" : Flag = "Q3"
                            ElseIf Me.rdbPeriodDiscount4.Checked Then : .FLAG = "Q4" : Flag = "Q4"
                            ElseIf Me.rdbYearlyDiscount.Checked Then : .FLAG = "Y" : Flag = "Y"
                            End If
                            If Not Me.rdbGivenDiscount.Checked Then
                                If (NufarmBussinesRules.SharedClass.DISC_AGREE_FROM = "PO") Then
                                    .BRND_B_S_ID = Me.GridEX3.GetValue("BRND_B_S_ID")
                                    .ACHIEVMENT_BRANDPACK_ID = DBNull.Value
                                Else
                                    .BRND_B_S_ID = DBNull.Value : .ACHIEVMENT_BRANDPACK_ID = Me.GridEX3.GetValue("ACHIEVEMENT_BRANDPACK_ID")
                                End If
                                .AGREE_DISC_HIST_ID = DBNull.Value : colLeftQTY = "LEFT_QTY"
                            Else
                                colLeftQTY = "AGREE_LEFT_QTY" : .AGREE_DISC_HIST_ID = Me.GridEX3.GetValue("AGREE_DISC_HIST_ID")
                            End If
                            .MRKT_DISC_HISt_ID = DBNull.Value : .MRKT_M_S_ID = DBNull.Value
                            .PROJ_DISC_HIST_ID = DBNull.Value
                        Case SelectedDiscount.MarketingDiscount
                            If Me.rdbGivenDiscountMarketing.Checked Then : Flag = "MG" : .FLAG = "MG"
                            ElseIf Me.rdbGivenCP.Checked Then : Flag = "CP" : .FLAG = "CP"
                            ElseIf Me.rdbSpecialCPD.Checked Then : Flag = "CS" : .FLAG = "CS"
                            ElseIf Me.rdbGivenCPR.Checked Then : Flag = "CR" : .FLAG = "CR"
                            ElseIf Me.rdbGivenDK.Checked Then : Flag = "DK" : .FLAG = "DK"
                            ElseIf Me.rdbGivenPKPP.Checked Then : Flag = "KP" : .FLAG = "KP"
                            ElseIf Me.rdbGivenCP_TM.Checked Then : Flag = "TD" : .FLAG = "TD"
                            ElseIf Me.rdbSpecialCPD_TM.Checked Then : Flag = "TS" : .FLAG = "TS"
                            ElseIf Me.rdbCPMRT_Dist.Checked Then : Flag = "CD" : .FLAG = "CD"
                            ElseIf Me.rdbCPMRT_TMDist.Checked Then : Flag = "CT" : .FLAG = "CT"
                            ElseIf Me.rdbDKN.Checked Then : Flag = "DN" : .FLAG = "DN"
                            ElseIf Me.rdbCPDAuto.Checked Then : Flag = "CA" : .FLAG = "CA"
                            End If
                            colLeftQTY = "MRKT_LEFT_QTY"
                            .BRND_B_S_ID = DBNull.Value : .AGREE_DISC_HIST_ID = DBNull.Value
                            .MRKT_DISC_HISt_ID = Me.GridEX3.GetValue("MRKT_DISC_HIST_ID").ToString()
                            .MRKT_M_S_ID = DBNull.Value : .PROJ_DISC_HIST_ID = DBNull.Value
                            .ACHIEVMENT_BRANDPACK_ID = DBNull.Value
                        Case SelectedDiscount.None
                        Case SelectedDiscount.OtherDiscount
                        Case SelectedDiscount.ProjectDiscount
                    End Select
                    .OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                    Dim Price As Decimal = Convert.ToDecimal(Me.GridEX1.GetValue("OA_PRICE_PERQTY"))
                    If colLeftQTY = "" Then : Return : End If
                    If Flag = "" Then : Return : End If
                    .RM_OA_ID = DBNull.Value
                    Dim QtyToInsert As Decimal = Me.LftQTY.txtQty.Value
                    Select Case Me.SDiscount
                        Case SelectedDiscount.None
                        Case SelectedDiscount.MarketingDiscount
                            Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                           QtyToInsert, .FLAG, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), , _
                           , , , False, .OA_BRANDPACK_ID)
                        Case SelectedDiscount.AgreementDiscount

                            Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                             QtyToInsert, .FLAG, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), , _
                             , , , False, .OA_BRANDPACK_ID)
                    End Select
                ElseIf Me.DDF = DataDragFrom.GridRemainding Then
                    If Me.DDT = DataDragTo.GridEx2 Then
                        Dim GridExDataView As DataView = CType(Me.GridEX1.DataSource, DataView)
                        Dim index As Integer = GridExDataView.Find(OA_BRANDPACK_ID)
                        If index <> -1 Then
                            If Me.LftQTY.txtQty.Value > Me.LftQTY.RemaindingQty Then
                                Me.ShowMessageInfo(String.Format("Quantity will exceed {0:#,##0.00}", Me.LftQTY.RemaindingQty))
                                Me.LftQTY.Show(Me.pnlMinusOA, True)
                                Return
                            End If
                            Dim AmountPrice As Decimal = 0
                            Dim price As Decimal = Convert.ToDecimal(Me.GridEX1.GetValue("OA_PRICE_PERQTY"))
                            If IsDBNull(price) Then
                                Me.ShowMessageInfo("PRICE for that brandpack is null")
                                Me.LftQTY.Show(Me.pnlMinusOA, True)
                                Return
                            End If
                            Dim OrgDecimalValue As Decimal = Me.LftQTY.txtQty.Value
                            AmountPrice = price * OrgDecimalValue
                            'ambil price yang ada di pnl oa_discount
                            Dim DISC_PRICE_OA As Decimal = 0
                            Dim x As String = ""
                            Dim w As Integer = Me.pnlOADiscount.TitleText.IndexOf(":") + 3
                            Dim s As String
                            Dim a As String = ""
                            Do
                                s = Microsoft.VisualBasic.Mid(Me.pnlOADiscount.TitleText, w, 1)
                                If (s = "") Or (s = ".") Then
                                Else
                                    a &= s
                                End If
                                w += 1
                            Loop Until s = ""
                            'GET TOTAL PRICE
                            DISC_PRICE_OA = Convert.ToDecimal(a) 'TOTAL DISCOUNT OA
                            If Not Me.grdRemainding.GetValue("FLAG").ToString() = "RMOA" Then
                                Dim SUMPRICE As Object = Me.clsOADiscount.GetTotalPriceOAGiven(Me.mcbRefNo.Value.ToString(), False) 'TOTAL DISCOUNT YG TELAH DIBERIKAN
                                Dim TOTAL_LEFT_PRICE As Decimal = DISC_PRICE_OA - Convert.ToDecimal(SUMPRICE)
                                Dim LEFT_QTY_PRICE As Decimal = TOTAL_LEFT_PRICE / price
                                If (AmountPrice + SUMPRICE) > DISC_PRICE_OA Then
                                    'Me.ShowMessageInfo("Value will be more than should be given" & vbCrLf & "Discount price for OA " & _
                                    'Me.mcbRefNo.Value.ToString() & " has " & String.Format("{0:#,##0.00}", SUMPRICE) & vbCrLf & "Amount that will be given is : " & _
                                    'String.Format("{0:#,##0.00}", AmountPrice) & vbCrLf & "Amount that can be released for BrandPack " & Me.GridEX1.GetValue("BRANDPACK_NAME").ToString() & " = " & String.Format("{0:#,##0.000}", LEFT_QTY_PRICE))
                                    Me.ShowMessageInfo("Discount value will exceed allowed-data" & vbCrLf & "Discount has been released " & _
                                              String.Format("{0:#,##0.00}", SUMPRICE) & vbCrLf & "Total left discount allowed is : " & String.Format("{0:#,##0.000}", LEFT_QTY_PRICE))
                                    'Me.Other_QTY.Show(Me.pnlOADiscount)
                                    Return
                                    Me.LftQTY.Show(Me.pnlMinusOA, True)
                                    Return
                                End If
                            End If
                            With Me.clsOADiscount.OA_BRANDPACK_DISCOUNT
                                .PRICE_PRQTY = price : .AGREE_DISC_HIST_ID = DBNull.Value : .BRND_B_S_ID = DBNull.Value
                                .GQSY_SGT_P_FLAG = Me.grdRemainding.GetValue("FLAG").ToString()
                                .MRK_M_S_ID = DBNull.Value : .MRKT_DISC_HIST_ID = DBNull.Value : .PROJ_DISC_HIST_ID = DBNull.Value
                                .OA_RM_ID = Me.grdRemainding.GetValue("OA_RM_ID").ToString()
                                .OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                                .BRANDPACK_ID = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
                                '.ACHIEVEMENT_BRANDPACK_ID = Me.grdRemainding.GetValue("ACHIEVEMENT_BRANDPACK_ID")
                                .ACHIEVEMENT_BRANDPACK_ID = DBNull.Value
                                Select Case Me.grdRemainding.GetValue("FLAG").ToString()
                                    Case "MG", "KP", "CP", "CS", "CD", "CT", "TS", "TD", "CR", "DK", "MS", "MT", "DN", "CA", "RMOA", "G", "Q1", "Q2", "Q2", "Q3", "Q4", "S1", "S2", "Y"

                                        Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.None, _
                                        OrgDecimalValue, , , , False, _
                                        Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString())
                                    Case "O", "OCBD", "ODD", "ODR", "ODK", "ODP"
                                        Me.clsOADiscount.InsertOA_BRANDPACK_DISC(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.None, _
                                     OrgDecimalValue, , , , True, _
                                     Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString())
                                End Select
                                Me.SFG = StateFillingGrid.Filling
                                If Not .GQSY_SGT_P_FLAG = "RMOA" Then
                                    Dim TotalDiscQty As Decimal = Convert.ToDecimal(GridExDataView(index)("TOTAL_DISC_QTY")) + OrgDecimalValue
                                    GridExDataView(index)("TOTAL_DISC_QTY") = TotalDiscQty
                                    GridExDataView(index)("TOTAL_AMOUNT_DISC") = TotalDiscQty * price
                                    GridExDataView(index).EndEdit()
                                    Me.GridEX1.Refetch()
                                End If
                            End With
                        End If
                    End If
                ElseIf Me.DDF = DataDragFrom.GridEx2 Then
                    Me.ShowMessageInfo("Can not insert data from OA discount !" & vbCrLf & "you can only delete data.")
                End If
            End With
            Me.LftQTY.Close() : Me.LftQTY = Nothing ' Me.isInsertingOADiscount = false
            Me.GridEX1.Row = IndexRow : Me.SFG = StateFillingGrid.HasFilled : Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_LftQTY_LeftQtyOK")
        Finally
            Me.SFG = StateFillingGrid.HasFilled ' Me.isInsertingOADiscount = false : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub EQTY_CancelClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles EQTY.CancelClick
        Try
            If Not IsNothing(Me.EQTY) Then
                Me.EQTY.Dispose()
                Me.EQTY = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub EQTY_OKClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles EQTY.OKClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim IndexRow As Integer = Me.GridEX1.Row
            Dim IndexCol As Integer = Me.GridEX1.Col
            Dim Index As Integer = -1
            If Not Me.EQTY.txtQTY.Value <= 0 Then
                If Me.EQTY.txtQTY.Value = Me.EQTY.OrgDecimalValue Then
                    If Me.GridEX1.GetRow().RowType <> Janus.Windows.GridEX.RowType.Record Then
                        Me.EQTY.Close()
                        Me.EQTY = Nothing
                        Me.ShowMessageInfo("The request couldn't be completed due to :" & vbCrLf & "BRANDPACK_NAME IN InsertedOABRANDPACK is not selected.")
                        Me.EQTY.Show(Me.pnlOADiscount)
                        Return
                    End If
                    'DELETE ROW BASED ON DATATYPE GIVEN/MARKETING/PROJECT
                    Index = Me.DeleteOA_BrandPack_Disc()
                    If Index <> -1 Then
                        Me.NavigationPane1_ItemClick(Me.NavigationPane1, New EventArgs())
                        Me.clsOADiscount.CreateViewOABrandPackDiscount(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), True)
                        Me.BindGridEx(Me.GridEX2, Me.clsOADiscount.ViewOADiscount)
                        Me.SFG = StateFillingGrid.HasFilled
                    End If
                    Me.EQTY.Close()
                    Me.EQTY = Nothing
                    Return
                End If
                Dim Flag As String = Me.GridEX2.GetValue("GQSY_SGT_P_FLAG").ToString()
                Dim OA_BRANDPACK_DISC_ID As Integer = CInt(Me.GridEX2.GetValue("OA_BRANDPACK_DISC_ID"))
                Dim OA_BRANDPACK_ID As String = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim BRANDPACK_ID As String = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
                Dim QTY As Decimal = Me.EQTY.txtQTY.Value
                Dim GridExDataview As DataView = CType(Me.GridEX1.DataSource, DataView)
                Index = GridExDataview.Find(OA_BRANDPACK_ID)
                If Not Me.GridEX2.GetValue("OA_RM_ID") Is DBNull.Value And Not IsNothing(Me.GridEX2.GetValue("OA_RM_ID")) Then
                    'DISCOUNT DARI REMAINDING QTY
                    'BESOK LAGI
                    Select Case Flag
                        Case "S1", "S2", "Q1", "Q2", "Q3", "Q4", "Y"
                            Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, Me.GridEX2.GetValue("GQSY_SGT_P_FLAG").ToString(), _
                          Me.GridEX2.GetValue("OA_RM_ID").ToString(), , , "", , , , False)
                        Case "DK", "KP", "MG", "CR", "CP", "CP", "CS", "TS", "TD", "CT", "CD", "DN", "CA"
                            Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, Me.GridEX2.GetValue("GQSY_SGT_P_FLAG").ToString(), _
                                                        Me.GridEX2.GetValue("OA_RM_ID").ToString(), , , "", Me.GridEX1.GetValue("MRKT_DISC_HIST_ID").ToString(), , , False)
                        Case "O", "OCBD", "ODD", "ODR", "ODK", "ODP"
                            Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, Flag, _
                                                        Me.GridEX2.GetValue("OA_RM_ID").ToString(), , , "", , , , True)
                        Case "G"
                            Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, Flag, _
                                                       Me.GridEX2.GetValue("OA_RM_ID").ToString(), , , "", , , , False)
                        Case "RMOA"
                            Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, Flag, _
                           Me.GridEX2.GetValue("OA_RM_ID").ToString(), , , "", , , , False)
                    End Select
                ElseIf Not IsDBNull(Me.GridEX2.GetValue("AGREE_DISC_HIST_ID")) Then
                    'DARI AGREEMENT GIVEN
                    If Not String.IsNullOrEmpty(Me.GridEX2.GetValue("AGREE_DISC_HIST_ID").ToString()) Then
                        Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, "", , _
                                            Me.GridEX2.GetValue("AGREE_DISC_HIST_ID").ToString(), , "")
                    End If
                ElseIf Not IsDBNull(Me.GridEX2.GetValue("BRND_B_S_ID")) Then
                    If Not String.IsNullOrEmpty(Me.GridEX2.GetValue("BRND_B_S_ID").ToString()) Then
                        Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, "", , _
                                           , Me.GridEX2.GetValue("BRND_B_S_ID").ToString(), "")
                    End If
                ElseIf Not IsDBNull(Me.GridEX2.GetValue("ACHIEVEMENT_BRANDPACK_ID")) Then
                    If Not String.IsNullOrEmpty(Me.GridEX2.GetValue("ACHIEVEMENT_BRANDPACK_ID").ToString()) Then
                        Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, "", , _
                                                                , , Me.GridEX2.GetValue("ACHIEVEMENT_BRANDPACK_ID").ToString())
                    End If
                ElseIf Not IsDBNull(Me.GridEX2.GetValue("MRKT_DISC_HIST_ID")) Then
                    If Not String.IsNullOrEmpty(Me.GridEX2.GetValue("MRKT_DISC_HIST_ID").ToString()) Then
                        Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, "", , _
                                                             , , "", Me.GridEX2.GetValue("MRKT_DISC_HIST_ID").ToString())
                    End If
                ElseIf Not IsDBNull(Me.GridEX2.GetValue("MRKT_M_S_ID")) Then
                    If Not String.IsNullOrEmpty(Me.GridEX2.GetValue("MRKT_M_S_ID").ToString()) Then
                        Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, "", , _
                                                              , , "", , Me.GridEX2.GetValue("MRKT_M_S_ID").ToString())
                    End If
                ElseIf Not IsDBNull(Me.GridEX2.GetValue("PROJ_DISC_HIST_ID")) Then
                    Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, "", , _
                                       , , "", , , Me.GridEX2.GetValue("PROJ_DISC_HIST_ID").ToString())
                ElseIf Flag = "O" Or Flag = "OCBD" Or Flag = "ODD" Or Flag = "ODR" Or Flag = "ODK" Or Flag = "ODP" Then
                    Me.clsOADiscount.Update_OABRANDPACK_DISC(OA_BRANDPACK_DISC_ID, BRANDPACK_ID, OA_BRANDPACK_ID, QTY, Flag, , _
                                           , , "", , , , True)
                End If
                If Not Me.GridEX2.GetValue("GQSY_SGT_P_FLAG").ToString() = "RMOA" Then
                    If Index <> -1 Then
                        Dim TotalDiscQty As Decimal = Convert.ToDecimal(GridExDataview(Index)("TOTAL_DISC_QTY")) - QTY
                        Dim Price As Decimal = Convert.ToDecimal(GridExDataview(Index)("OA_PRICE_PERQTY"))
                        GridExDataview(Index)("TOTAL_DISC_QTY") = TotalDiscQty
                        GridExDataview(Index)("TOTAL_AMOUNT_DISC") = TotalDiscQty * Price
                        GridExDataview(Index).EndEdit()
                        Me.GridEX1.Refetch()
                    End If
                End If
                ' Me.isInsertingOADiscount = false
                'Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs)
                Me.NavigationPane1_ItemClick(Me.NavigationPane1, New EventArgs())
                Me.clsOADiscount.CreateViewOABrandPackDiscount(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), True)
                Me.BindGridEx(Me.GridEX2, Me.clsOADiscount.ViewOADiscount)
                Me.GridEX1.Row = IndexRow : Me.SFG = StateFillingGrid.HasFilled
                Me.EQTY.Close() : Me.EQTY = Nothing
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_EQTY_OKClick")
            Me.SFG = StateFillingGrid.HasFilled
        Finally
            ' Me.isInsertingOADiscount = false
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " CHECKING AVAILABILITY DISCOUNT "
    Private Sub CheckAvailabilityDisocunt(ByVal DISTRIBUTOR_ID As String, ByVal BRANDPACK_ID As String, ByVal OA_DATE As Object, ByVal SDiscount As SelectedDiscount)
        'Me.clsOADiscount.CheckAvailabilityDiscSalesAgreement(OA_ID, OA_DATE, DISTRIBUTOR_ID, BRANDPACK_ID)
        Dim OAQty As Decimal = Convert.ToDecimal(Me.txtQuantity.Value)
        Select Case SDiscount
            Case SelectedDiscount.AgreementDiscount
                Me.clsOADiscount.CheckAvailabilityDisc(DateTime.Parse(Me.txTPOREFDATE.Text), _
                DISTRIBUTOR_ID, BRANDPACK_ID, OAQty, NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, False)

            Case SelectedDiscount.MarketingDiscount
                Me.clsOADiscount.CheckAvailabilityDisc(DateTime.Parse(Me.txTPOREFDATE.Text), _
                               DISTRIBUTOR_ID, BRANDPACK_ID, OAQty, NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, False)
            Case SelectedDiscount.None
            Case SelectedDiscount.OtherDiscount
                Me.clsOADiscount.CheckAvailabilityDisc(DateTime.Parse(Me.txTPOREFDATE.Text), DISTRIBUTOR_ID, BRANDPACK_ID, OAQty, OtherDiscount, False)
            Case SelectedDiscount.ProjectDiscount

        End Select
        Me.unabledRadioButton()
        '---DPD Discount----
        Me.rdbPeriodDiscount1.Enabled = Me.clsOADiscount.IsgeneratedAgreement.Q1
        Me.rdbPeriodDiscount2.Enabled = Me.clsOADiscount.IsgeneratedAgreement.Q2
        Me.rdbPeriodDiscount3.Enabled = Me.clsOADiscount.IsgeneratedAgreement.Q3
        Me.rdbPeriodDiscount4.Enabled = Me.clsOADiscount.IsgeneratedAgreement.Q4
        Me.rdbSemesterly1Discount.Enabled = Me.clsOADiscount.IsgeneratedAgreement.S1
        Me.rdbSemesterly2Discount.Enabled = Me.clsOADiscount.IsgeneratedAgreement.S2
        Me.rdbYearlyDiscount.Enabled = Me.clsOADiscount.IsgeneratedAgreement.Y

        '----DPRDS--
        Me.rdbGivenDiscountMarketing.Enabled = Me.clsOADiscount.IsHasSales.MG

        '---PKPP--
        Me.rdbGivenPKPP.Enabled = Me.clsOADiscount.IsHasSales.PKPP
        '----CPD Distributor--saja
        Me.rdbGivenCP.Enabled = Me.clsOADiscount.IsHasSales.CPD
        '----SCPD Distributor----
        Me.rdbSpecialCPD.Enabled = Me.clsOADiscount.IsHasSales.SCPD

        '----CPD distributor TM
        Me.rdbGivenCP_TM.Enabled = Me.clsOADiscount.IsHasSales.CPD_TM_Distributor
        '---SCPD TM dan Distributor---
        Me.rdbSpecialCPD_TM.Enabled = Me.clsOADiscount.IsHasSales.CPDS_TM_Distributor
        '----CPR----
        Me.rdbGivenCPR.Enabled = Me.clsOADiscount.IsHasSales.CPR
        '-----DK---
        Me.rdbGivenDK.Enabled = Me.clsOADiscount.IsHasSales.DK
        '-----CPMRT DISTRIBUTOR ONLY------
        Me.rdbCPMRT_Dist.Enabled = Me.clsOADiscount.IsHasSales.CPMRT_DIST
        ''------CP MERT TM & Distributor----------------
        Me.rdbCPMRT_TMDist.Enabled = Me.clsOADiscount.IsHasSales.CPMRT_DIST_TM
        ''------------DK N--------------------------------
        Me.rdbDKN.Enabled = Me.clsOADiscount.IsHasSales.DK_N
        '--------------C P D AUTO ---------------------------------
        Me.rdbCPDAuto.Enabled = Me.clsOADiscount.IsHasSales.CPDAuto
        '---------------Other Disc-------------------------
        Me.rdbUncategorized.Enabled = True
        'check ODD
        Me.rdbDD.Enabled = Me.clsOADiscount.IsHasOtherDisc.ODD
        Me.rdbDR.Enabled = Me.clsOADiscount.IsHasOtherDisc.ODR
        Me.rdbCBD.Enabled = Me.clsOADiscount.IsHasOtherDisc.OCBD
        Me.rdbDK.Enabled = Me.clsOADiscount.IsHasOtherDisc.ODK
        Me.rdbDP.Enabled = Me.clsOADiscount.IsHasOtherDisc.ODP
        Me.setUnabledRadioButton()
    End Sub
    Private Sub setUnabledRadioButton()
        If Not Me.rdbPeriodDiscount1.Enabled Then : Me.rdbPeriodDiscount1.Checked = False : End If
        If Not Me.rdbPeriodDiscount2.Enabled Then : Me.rdbPeriodDiscount2.Checked = False : End If
        If Not Me.rdbPeriodDiscount3.Enabled Then : Me.rdbPeriodDiscount3.Checked = False : End If
        If Not Me.rdbPeriodDiscount4.Enabled Then : Me.rdbPeriodDiscount4.Checked = False : End If
        If Not Me.rdbSemesterly1Discount.Enabled Then : Me.rdbSemesterly1Discount.Checked = False : End If
        If Not Me.rdbPeriodDiscount2.Enabled Then : Me.rdbPeriodDiscount2.Checked = False : End If
        If Not Me.rdbYearlyDiscount.Enabled Then : Me.rdbYearlyDiscount.Checked = False : End If
        If Not Me.rdbGivenDiscountMarketing.Enabled Then : Me.rdbGivenDiscountMarketing.Checked = False : End If
        If Not Me.rdbGivenPKPP.Enabled Then : Me.rdbGivenPKPP.Checked = False : End If
        If Not Me.rdbGivenCP.Enabled Then : Me.rdbGivenCP.Checked = False : End If
        If Not Me.rdbSpecialCPD.Enabled Then : Me.rdbSpecialCPD.Checked = False : End If
        If Not Me.rdbGivenCP_TM.Enabled Then : Me.rdbGivenCP_TM.Checked = False : End If
        If Not Me.rdbGivenCPR.Enabled Then : Me.rdbGivenCPR.Checked = False : End If
        If Not Me.rdbSpecialCPD_TM.Enabled Then : Me.rdbSpecialCPD_TM.Checked = False : End If
        If Not Me.rdbCPMRT_Dist.Enabled Then : Me.rdbCPMRT_Dist.Checked = False : End If
        If Not Me.rdbCPMRT_TMDist.Enabled Then : Me.rdbCPMRT_TMDist.Checked = False : End If
        If (Me.rdbGivenCP.Enabled = False And Me.rdbSpecialCPD.Enabled = False And Me.rdbCPMRT_Dist.Checked = False) Then
            Me.pnlCPDDistributor.Enabled = False
        Else : Me.pnlCPDDistributor.Enabled = True
        End If
        If (Me.rdbGivenCP_TM.Enabled = False And Me.rdbSpecialCPD_TM.Enabled = False And Me.rdbCPMRT_TMDist.Enabled = False) Then
            Me.pnlCPDDist_TM.Enabled = False
        Else : Me.pnlCPDDist_TM.Enabled = True
        End If
        If Not Me.rdbGivenDK.Enabled Then : Me.rdbGivenDK.Checked = False : End If

        If Not Me.rdbDKN.Enabled Then : Me.rdbDKN.Checked = False : End If
        If Not Me.rdbCPDAuto.Enabled Then : Me.rdbCPDAuto.Checked = False : End If
        If Not Me.rdbDD.Enabled Then : Me.rdbDD.Checked = False : End If
        If Not Me.rdbDR.Enabled Then : Me.rdbDR.Checked = False : End If
        If Not Me.rdbCBD.Enabled Then : Me.rdbCBD.Checked = False : End If
        If Not Me.rdbDK.Enabled Then : Me.rdbDK.Checked = False : End If
        If Not Me.rdbDP.Enabled Then : Me.rdbDP.Checked = False : End If
    End Sub
    Private Sub unabledRadioButton()
        Me.rdbPeriodDiscount1.Checked = False
        rdbPeriodDiscount2.Checked = False
        Me.rdbPeriodDiscount3.Checked = False
        Me.rdbPeriodDiscount4.Checked = False
        Me.rdbSemesterly1Discount.Checked = False
        Me.rdbPeriodDiscount2.Checked = False
        Me.rdbYearlyDiscount.Checked = False
        Me.rdbGivenDiscountMarketing.Checked = False
        Me.rdbGivenPKPP.Checked = False
        Me.rdbGivenCP.Checked = False
        Me.rdbSpecialCPD.Checked = False
        Me.rdbGivenCP_TM.Checked = False
        Me.rdbGivenCPR.Checked = False
        Me.rdbSpecialCPD_TM.Checked = False
        Me.rdbGivenDK.Checked = False
        Me.rdbGivenDiscount.Checked = False
        Me.rdbCPMRT_Dist.Enabled = False
        Me.rdbCPMRT_TMDist.Enabled = False
        Me.rdbDKN.Enabled = False
        Me.rdbCPDAuto.Enabled = False
        Me.rdbCBD.Checked = False
        Me.rdbDD.Checked = False
        Me.rdbDR.Checked = False
        Me.rdbDK.Checked = False
        Me.rdbDP.Checked = False
        'Me.rdbDP.Checked = False
        Me.rdbUncategorized.Checked = False
    End Sub
#End Region

#End Region

#Region " Function "

    Private Function getFlagCPD(ByVal FCPD As FlagCPD)
        Dim retval As String = ""
        Select Case FCPD
            Case FlagCPD.CPDDistributor
                retval = "CP"
            Case FlagCPD.CPDSDistributor
                retval = "CS"
            Case FlagCPD.CPDSDistributor_TM
                retval = "TS"
            Case FlagCPD.CPDTMDistributor
                retval = "TD"
            Case FlagCPD.CPMRTDistributor
                retval = "CD"
            Case FlagCPD.CPMRTDsitributor_TM
                retval = "CT"
        End Select
        Return retval
    End Function

    Private Function IsValid() As Boolean
        If Me.mcbRefNo.Value Is Nothing Then
            Me.baseTooltip.SetToolTip(Me.mcbRefNo, "OA_REF_NO is Null !." & vbCrLf & "Please Defined OA_REF_NO.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbRefNo), Me.mcbRefNo, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.mcbRefNo, "OA_REF_NO is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.mcbRefNo, "OA_REF_NO must not be NULL.")
            'Me.baseBallonSmall.ShowBalloon(Me.mcbRefNo)
            Me.mcbRefNo.Focus()
            Return False
        ElseIf Me.mcbRefNo.SelectedIndex = -1 Then
            Me.baseTooltip.SetToolTip(Me.mcbRefNo, "OA_REF_NO is Null !." & vbCrLf & "Please Defined OA_REF_NO.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbRefNo), Me.mcbRefNo, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.mcbRefNo, "OA_REF_NO is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.mcbRefNo, "OA_REF_NO must not be NULL.")
            'Me.baseBallonSmall.ShowBalloon(Me.mcbRefNo)
            Me.mcbRefNo.Focus()
            Return False
        ElseIf Me.mcbBrandPack.Value Is Nothing Then
            Me.baseTooltip.SetToolTip(Me.mcbBrandPack, "BrandPack is Null !." & vbCrLf & "Please Defined BrandPack Name.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.mcbBrandPack, "BRANDPACK is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.mcbBrandPack, "BRANDPACK must not be NULL.")
            'Me.baseBallonSmall.ShowBalloon(Me.mcbBrandPack)
            Me.mcbBrandPack.Focus()
            Return False
        ElseIf Me.mcbRefNo.SelectedIndex = -1 Then
            Me.baseTooltip.SetToolTip(Me.mcbBrandPack, "BrandPack is Null !." & vbCrLf & "Please Defined BrandPack Name.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.mcbBrandPack, "BRANDPACK is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.mcbBrandPack, "BRANDPACK must not be NULL.")
            'Me.baseBallonSmall.ShowBalloon(Me.mcbBrandPack)
            Me.mcbBrandPack.Focus()
            Return False
        ElseIf Me.txtQuantity.Value Is Nothing Then
            Me.baseTooltip.SetToolTip(Me.txtQuantity, "Quantity is Null." & vbCrLf & "Quantity must not be Null.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtQuantity), Me.txtQuantity, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.txtQuantity, "Quantity is NULL .!")
            'Me.baseBallonSmall.SetBalloonText(Me.txtQuantity, "Quantity mustn't be null/Zero.")
            'Me.baseBallonSmall.ShowBalloon(Me.txtQuantity)
            Me.txtQuantity.Focus()
            Return False
        ElseIf Me.txtQuantity.Value <= 0 Then
            Me.baseTooltip.SetToolTip(Me.txtQuantity, "Quantity is invalid." & vbCrLf & "Quantity must not be Null/zero/minus.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtQuantity), Me.txtQuantity, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.txtQuantity, "Quantity is NULL .!")
            'Me.baseBallonSmall.SetBalloonText(Me.txtQuantity, "Quantity mustn't be null/Zero.")
            'Me.baseBallonSmall.ShowBalloon(Me.txtQuantity)
            Me.txtQuantity.Focus()
            Me.txtQuantity.SelectionStart = 0
            Me.txtQuantity.SelectionLength = Me.txtQuantity.Text.Length
            Return False
        ElseIf Me.txtPrice.Value Is Nothing Then
            Me.baseTooltip.SetToolTip(Me.txtPrice, "Price is Null" & vbCrLf & "Price must not be Null." & vbCrLf & "Please try to check other items to be valid.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtPrice), Me.txtPrice, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.txtPrice, "Price is NULL .!")
            'Me.baseBallonSmall.SetBalloonText(Me.txtPrice, "Price mustn't be null/Zero.")
            'Me.baseBallonSmall.ShowBalloon(Me.txtPrice)
            Return False
        ElseIf Me.txtPrice.Value = 0 Then
            Me.baseTooltip.SetToolTip(Me.txtPrice, "Price is Null" & vbCrLf & "Price must not be Null." & vbCrLf & "Please try to check other items to be valid.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtPrice), Me.txtPrice, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.txtPrice, "Price is NULL .!")
            'Me.baseBallonSmall.SetBalloonText(Me.txtPrice, "Price mustn't be null/Zero.")
            'Me.baseBallonSmall.ShowBalloon(Me.txtPrice)
            Return False


        End If
        Return True
    End Function

    Private Function CreateViewMarketingGivenDiscount(ByVal OA_ID As String, ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal FLAG As String, ByVal mustCloseConnectionDB As Boolean) As DataView
        Try
            Me.clsOADiscount.CreateViewGivenDiscount(OA_ID, BRANDPACK_ID, NufarmBussinesRules.OrderAcceptance.OADiscount.GivenDiscount.MarketingDiscount, _
             NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity, DISTRIBUTOR_ID, FLAG, mustCloseConnectionDB)
            If Me.clsOADiscount.ViewGivenDiscount().Count > 0 Then
                Me.BindGridEx3(Me.clsOADiscount.ViewGivenDiscount())
            Else
                Me.BindGridEx3(Nothing)
            End If
        Catch ex As Exception

        End Try
        Return Me.clsOADiscount.ViewGivenDiscount
    End Function

    Private Function CreateViewSemesterlyDiscount(ByVal FlagSemesterly As Semesterly, ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal PO_DATE As Object, ByVal mustCloseConnectionDB As Boolean) As DataView
        Try
            If FlagSemesterly = Semesterly.S1 Then
                Me.clsOADiscount.CreateViewSemesterlyDiscount(BRANDPACK_ID, DISTRIBUTOR_ID, NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity, _
                NufarmBussinesRules.OrderAcceptance.OADiscount.SemesterlyFlag.S1, PO_DATE, mustCloseConnectionDB)
            ElseIf FlagSemesterly = Semesterly.S2 Then
                Me.clsOADiscount.CreateViewSemesterlyDiscount(BRANDPACK_ID, DISTRIBUTOR_ID, NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity, _
                NufarmBussinesRules.OrderAcceptance.OADiscount.SemesterlyFlag.S2, PO_DATE, mustCloseConnectionDB)
            End If
            If Me.clsOADiscount.ViewSemesterlyDiscount.Count > 0 Then
                Me.BindGridEx3(Me.clsOADiscount.ViewSemesterlyDiscount())
            Else
                Me.BindGridEx3(Nothing)
            End If
        Catch ex As Exception

        End Try
        Return Me.clsOADiscount.ViewSemesterlyDiscount
    End Function

    Private Function CreateViewQuarterlyDiscount(ByVal FlagQuarterly As Quarterly, ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal PO_DATE As Object, ByVal mustCloseConnection As Boolean) As DataView
        Try
            Select Case FlagQuarterly
                Case Quarterly.Q1
                    Me.clsOADiscount.CreateViewQuarterlyDiscount(BRANDPACK_ID, DISTRIBUTOR_ID, NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity, _
                     NufarmBussinesRules.OrderAcceptance.OADiscount.QuarterlyFlag.Q1, PO_DATE, mustCloseConnection)
                Case Quarterly.Q2
                    Me.clsOADiscount.CreateViewQuarterlyDiscount(BRANDPACK_ID, DISTRIBUTOR_ID, NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity, _
                    NufarmBussinesRules.OrderAcceptance.OADiscount.QuarterlyFlag.Q2, PO_DATE, mustCloseConnection)
                Case Quarterly.Q3
                    Me.clsOADiscount.CreateViewQuarterlyDiscount(BRANDPACK_ID, DISTRIBUTOR_ID, NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity, _
                    NufarmBussinesRules.OrderAcceptance.OADiscount.QuarterlyFlag.Q3, PO_DATE, mustCloseConnection)
                Case Quarterly.Q4
                    Me.clsOADiscount.CreateViewQuarterlyDiscount(BRANDPACK_ID, DISTRIBUTOR_ID, NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity, _
                    NufarmBussinesRules.OrderAcceptance.OADiscount.QuarterlyFlag.Q4, PO_DATE, mustCloseConnection)
            End Select
            If Me.clsOADiscount.ViewQuarterlyDiscount.Count > 0 Then
                Me.BindGridEx3(Me.clsOADiscount.ViewQuarterlyDiscount())
            Else
                Me.BindGridEx3(Nothing)
            End If
        Catch ex As Exception

        End Try
        Return Me.clsOADiscount.ViewQuarterlyDiscount
    End Function

    Private Function CreateViewGivenDiscount(ByVal OA_ID As String, ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal mustCloseConnectionDB As Boolean) As DataView
        Try

            Me.clsOADiscount.CreateViewGivenDiscount(OA_ID, BRANDPACK_ID, NufarmBussinesRules.OrderAcceptance.OADiscount.GivenDiscount.AgreementDiscount, _
            NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity, DISTRIBUTOR_ID, "G", mustCloseConnectionDB)
            If Me.clsOADiscount.ViewGivenDiscount.Table.Rows.Count > 0 Then
                Me.BindGridEx3(Me.clsOADiscount.ViewGivenDiscount)
                Me.btnGenerateAgreement.Enabled = False
            Else
                'jika belum  clearkan datagrid windows
                Me.BindGridEx3(Nothing)
                Me.btnGenerateAgreement.Enabled = True
            End If
        Catch ex As Exception

        End Try
        Return Me.clsOADiscount.ViewGivenDiscount
    End Function

    Private Function CreateViewYearlyDiscount(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal PO_DATE As Object, ByVal mustCloseConnection As Boolean) As DataView
        Try
            Me.clsOADiscount.CreateViewYearlyDiscount(BRANDPACK_ID, DISTRIBUTOR_ID, NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity, PO_DATE, mustCloseConnection)
            If Me.clsOADiscount.ViewYearlyDiscount().Count > 0 Then
                Me.BindGridEx3(Me.clsOADiscount.ViewYearlyDiscount())
            Else
                Me.BindGridEx3(Me.clsOADiscount.ViewYearlyDiscount())
            End If
        Catch ex As Exception

        End Try
        Return Me.clsOADiscount.ViewYearlyDiscount
    End Function

    Private Function CreateViewProject(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String) As DataView
        Try
            Me.clsOADiscount.CreateViewProjectDiscount(BRANDPACK_ID, DISTRIBUTOR_ID, _
            NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity)
            If Me.clsOADiscount.ViewProjectDiscount().Count > 0 Then
                Me.BindGridEx3(Me.clsOADiscount.ViewProjectDiscount())
            Else
                Me.BindGridEx3(Nothing)
            End If
        Catch ex As Exception

        End Try
        Return Me.clsOADiscount.ViewProjectDiscount
    End Function

#End Region

#Region " Event "

#Region " Form Event "

    Private Sub OA_BranPack_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.AcceptButton = Me.ButtonEntry1.btnInsert
            Me.CancelButton = Me.ButtonEntry1.btnClose
            Me.baseTooltip.ToolTipTitle = " Information "
            Select Case Me.NavigationPane1.SelectedPanel().Name
                Case "pnlAgreement"
                    Me.SDiscount = SelectedDiscount.AgreementDiscount
                Case "pnlMarketing"
                    Me.SDiscount = SelectedDiscount.MarketingDiscount
                Case "pnlProject"
                    Me.SDiscount = SelectedDiscount.ProjectDiscount
                Case "pnlOtherDiscount"
                    Me.SDiscount = SelectedDiscount.OtherDiscount
                Case "pnlRemainding"
                    Me.SDiscount = SelectedDiscount.None
            End Select
            Me.rdbTSDiscountMarketing.Visible = False
            Me.rdbSteppingDiscount.Visible = False
            'Me.Size = New System.Drawing.Size(961, 656)
            Me.GridEX3.AllowDrop = True
            Me.grdRemainding.AllowDrop = True
            Me.Mode = ModeSave.Save
            Me.btnGenerateAgreement.Enabled = False
            Me.RefreshData1.btnRefresh.Text = "&Reload Data"
            'AddHandler GridEX1.CurrentCellChanged, AddressOf GetSubTotalRemainding

        Catch ex As Exception
            'Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_OA_BranPack_Load")
        Finally
            Me.ReadAcces()
            Me.HasLoad = True
            Me.SFG = StateFillingGrid.HasFilled
            Me.SFM = StateFillingMCB.HasFilled
            Me.SI_MCB = SelectedItemMCB.FromMCB
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OA_BranPack_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.SFG = StateFillingGrid.Disposing
            If Not IsNothing(Me.clsOADiscount) Then
                'dispose reference object
                Me.clsOADiscount.Dispose(False)
            End If

            'Me.Dispose()
        Catch ex As Exception

        Finally
            Me.SFG = StateFillingGrid.Disposed
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " Button "

    Private Sub RefreshData1_BtnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles RefreshData1.BtnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.SFM = StateFillingMCB.Filling
            Dim valOA As Object = Me.mcbRefNo.Value
            Me.mcbRefNo.Value = Nothing

            Me.BindGridEx(Me.GridEX2, Nothing)
            Me.BindGridEx3(Nothing)
            Me.grdRemainding.SetDataBinding(Nothing, "")
            Me.pnlGridDiscount.TitleText = ""
            Me.mcbBrandPack.Value = Nothing
            Me.txtQuantity.Value = 0
            Me.txtPrice.Value = 0
            Me.txtTotal.Value = 0
            Me.NavigationPane1.Enabled = False
            Me.txtRemark.Text = ""
            Me.cmbKiloLitre.SelectedIndex = -1
            Me.ClearControl(Me.grpEdit)
            'Return
            'Me.RefReshData()

            Me.SFM = StateFillingMCB.Filling
            Me.mcbRefNo.Value = Nothing
            Me.SFM = StateFillingMCB.HasFilled
            Me.mcbRefNo.Value = valOA
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_RefreshData1_BtnClick")
        Finally
            Me.Cursor = Cursors.Default
            Me.SFG = StateFillingGrid.HasFilled
            Me.SFM = StateFillingMCB.HasFilled
            Me.SI_MCB = SelectedItemMCB.FromMCB
        End Try
    End Sub

    Private Sub btnGenerateAgreement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateAgreement.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.GridEX1.SelectCurrentCellText()
            If (Me.rdbPeriodDiscount1.Checked = False) And (Me.rdbPeriodDiscount2.Checked = False) And (Me.rdbPeriodDiscount3.Checked = False) _
                And (Me.rdbPeriodDiscount4.Checked = False) And (Me.rdbGivenDiscount.Checked = False) And Me.rdbYearlyDiscount.Checked = False Then
                Me.ShowMessageInfo("Please define discount type to select")
                Return
            ElseIf Me.GridEX1.SelectedItems.Count = 0 Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack.")
                Return
            ElseIf Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack.") ' & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                '& vbCrLf & "And DISTRIBUTOR'S PO")
                Return
            End If
            If Me.mcbRefNo.Value Is Nothing Then
                Me.baseTooltip.Show("OA_REF_NO is null.", Me.mcbRefNo, 2000)
                Return
            End If
            If Me.mcbRefNo.SelectedItem Is Nothing Then
                Me.baseTooltip.Show("OA_REF_NO is null.", Me.mcbRefNo, 2000)
                Return
            End If
            'Dim index As Integer = Me.GridEX1.Row
            'Dim indexCol As Integer = Me.GridEX1.Col
            If Me.DistributorID = "" Then
                Me.clsOADiscount.PO_From(Me.mcbRefNo.Value.ToString(), True)
                Me.DistribtorName = Me.clsOADiscount.DISTRIBUTOR_NAME
                Me.DistributorID = Me.clsOADiscount.DISTRIBUTOR_ID
            End If

            If Me.rdbGivenDiscount.Checked = True Then
                'CHECK APAKAH DATA SUDAH DI GENERATE / BELUM 
                If Me.clsOADiscount.HasGeneratedDiscountAgreement(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False, False, False, Nothing, Nothing, True, False) = True Then
                    Me.ShowMessageInfo("Data BrandPack " & Me.GridEX1.GetValue("BRANDPACK_NAME").ToString() & _
                    " for Distributor " & Me.txtPOFrom.Text & vbCrLf & "Has been generated !.")
                    Return
                End If
                'GENERATED GIVEN DISCOUNT CODE HERE
                Me.clsOADiscount.GenerateAgreementGivenDiscount(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), _
                Me.DistributorID, Me.mcbRefNo.DropDownList().GetValue("PO_REF_NO").ToString(), Convert.ToDecimal(Me.GridEX1.GetValue("OA_ORIGINAL_QTY")), False)
                'Me.GenerateAgreementGivenDiscount(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), _
                'Me.DistributorID, Me.mcbRefNo.DropDownList().GetValue("PO_REF_NO").ToString(), Convert.ToDecimal(Me.GridEX1.GetValue("OA_ORIGINAL_QTY")))
                Me.CreateViewGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID"), Me.clsOADiscount.DISTRIBUTOR_ID, True)
                Me.btnGenerateAgreement.Enabled = False
                'SHOW DATA AGREE_BRANDPACK_ID WHERE LEFT > 0
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnGenerateAgreement_Click")
        Finally
            Me.Cursor = Cursors.Default
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingMCB.HasFilled
            End If
            If Me.SFM = StateFillingMCB.Filling Then
                Me.SFM = StateFillingMCB.HasFilled
            End If
            If (Me.SI_MCB = SelectedItemMCB.FromGrid) Or (Me.SI_MCB = SelectedItemMCB.FromNone) Then
                Me.SI_MCB = SelectedItemMCB.FromMCB
            End If
        End Try
    End Sub

    Private Sub btnGenerateMarketing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateMarketing.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If (Me.rdbGivenCP.Checked = False) And (Me.rdbGivenDK.Checked = False) And (Me.rdbGivenPKPP.Checked = False) And _
            (Me.rdbGivenCPR.Checked = False) And (Me.rdbGivenDiscountMarketing.Checked = False) And (Me.rdbSpecialCPD.Checked = False) And (Me.rdbSpecialCPD_TM.Checked = False) And (Me.rdbGivenCP_TM.Checked = False) _
             And (Me.rdbCPMRT_Dist.Checked = False) And (Me.rdbCPMRT_TMDist.Checked = False) And (Me.rdbDKN.Checked = False) And (Me.rdbCPDAuto.Checked = False) Then
                Me.ShowMessageInfo("Please define discount type")
                Return
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO") : Me.Cursor = Cursors.Default
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                        & vbCrLf & "And DISTRIBUTOR'S PO") : Me.Cursor = Cursors.Default
                    Return
                End If
                If Me.mcbRefNo.Value Is Nothing Then
                    Me.baseTooltip.Show("PO_REF_NO is null.", Me.mcbRefNo, 3000) : Me.mcbRefNo.Focus()
                    Me.Cursor = Cursors.Default : Return
                End If
            End If
            Dim countFlag As Integer = 0 'check apakah ada 2 data / lebih yang di cheklist
            If Me.rdbGivenDiscountMarketing.Checked Then : countFlag = countFlag + 1 : End If
            If Me.rdbGivenCPR.Checked Then : countFlag = countFlag + 1 : End If
            If Me.rdbGivenDK.Checked Then : countFlag = countFlag + 1 : End If
            If Me.rdbGivenPKPP.Checked Then : countFlag = countFlag + 1 : End If
            If Me.rdbGivenCP.Checked Then : countFlag = countFlag + 1 : End If
            If Me.rdbGivenCP_TM.Checked Then : countFlag = countFlag + 1 : End If
            If Me.rdbSpecialCPD.Checked Then : countFlag = countFlag + 1 : End If
            If Me.rdbSpecialCPD_TM.Checked Then : countFlag = countFlag + 1 : End If
            If Me.rdbCPMRT_Dist.Checked Then : countFlag = countFlag + 1 : End If
            If Me.rdbCPMRT_TMDist.Checked Then : countFlag = countFlag + 1 : End If
            If Me.rdbDKN.Checked Then : countFlag = countFlag + 1 : End If
            If Me.rdbCPDAuto.Checked Then : countFlag = countFlag + 1 : End If
            If countFlag > 1 Then
                Me.ShowMessageInfo("only one by one of sales flag can be generated ") : Cursor = Cursors.Default : Return
            End If
            'CHECK APAKAH DATA PERNAH DI GENERATE 
            If Me.DistributorID = "" Then
                Me.DistribtorName = Me.clsOADiscount.PO_From(Me.mcbRefNo.Value.ToString(), False)
                Me.DistributorID = Me.clsOADiscount.DISTRIBUTOR_ID
                'Me.clsOADiscount.DISTRIBUTOR_NAME
            End If
            Dim flag As String = ""
            If Me.rdbGivenDiscountMarketing.Checked Then : flag = "MG"
            ElseIf Me.rdbGivenCPR.Checked Then : flag = "CR"
            ElseIf Me.rdbGivenDK.Checked Then : flag = "DK"
            ElseIf Me.rdbGivenPKPP.Checked Then : flag = "KP"
            ElseIf Me.rdbGivenCP.Checked Then : flag = Me.getFlagCPD(FlagCPD.CPDDistributor)
            ElseIf Me.rdbSpecialCPD.Checked Then : flag = Me.getFlagCPD(FlagCPD.CPDSDistributor)
            ElseIf Me.rdbGivenCP_TM.Checked Then : flag = Me.getFlagCPD(FlagCPD.CPDTMDistributor)
            ElseIf Me.rdbSpecialCPD_TM.Checked Then : flag = Me.getFlagCPD(FlagCPD.CPDSDistributor_TM)
            ElseIf Me.rdbCPMRT_Dist.Checked Then : flag = Me.getFlagCPD(FlagCPD.CPMRTDistributor)
            ElseIf Me.rdbCPMRT_TMDist.Checked Then : flag = Me.getFlagCPD(FlagCPD.CPMRTDsitributor_TM)
            ElseIf Me.rdbDKN.Checked Then : flag = "DN"
            ElseIf Me.rdbCPDAuto.Checked Then : flag = "CA"
            End If
            If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), flag, True) = True Then
                Me.ShowMessageInfo("Data Has already been generated!!") : Me.Cursor = Cursors.Default
                Return
            End If
            If Me.rdbCPDAuto.Checked And (Me.rdbGivenCP.Enabled Or Me.rdbGivenCP_TM.Enabled) Then
                'check apakah ada data CPD yang sudah di generate
                If Me.clsOADiscount.HasGeneratedCPD(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), flag) Then
                    Me.ShowMessageInfo("Can not generate double CPD discount") : Me.Cursor = Cursors.Default : Return
                End If
            End If
            If (Me.rdbGivenCP.Checked Or Me.rdbGivenCP_TM.Checked) And Me.rdbCPDAuto.Enabled Then
                'check apakah ada data CPD auto sudah di generate
                If Me.clsOADiscount.HasGeneratedCPD(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), flag) Then
                    Me.ShowMessageInfo("Can not generate double CPD discount") : Me.Cursor = Cursors.Default : Return
                End If
            End If
            Me.clsOADiscount.GenerateSalesGivenDiscount(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), _
                          Me.DistributorID, Me.mcbRefNo.DropDownList().GetValue("PO_REF_NO").ToString(), flag, Convert.ToDecimal(Me.GridEX1.GetValue("OA_ORIGINAL_QTY")), DateTime.Parse(Me.txTPOREFDATE.Text), False)
            Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.DistributorID, flag, True)
            Me.btnGenerateMarketing.Enabled = False
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnGenerateMarketing_Click")
        Finally
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ButtonSearch2_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearch2.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsOADiscount.ViewPOBrandpack.RowFilter = "BRANDPACK_NAME LIKE '%" & Me.mcbBrandPack.Text & "%'"
            Me.BindMulticolumnCombo(Me.mcbBrandPack, Me.clsOADiscount.ViewPOBrandpack, True)
            If Me.mcbBrandPack.ReadOnly = False Then
                Dim itemCount As Integer = Me.mcbBrandPack.DropDownList.RecordCount()
                Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ButtonSearch1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearch1.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mcbRefNo.ReadOnly = False Then
                Dim dtview As DataView = Me.clsOADiscount.SearchOARef(Me.mcbRefNo.Text.Trim())
                Me.BindMulticolumnCombo(Me.mcbRefNo, Me.clsOADiscount.ViewOARefNo, True)
                Dim itemCount As Integer = Me.mcbRefNo.DropDownList().RecordCount()
                Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnChekExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChekExisting.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mcbRefNo.Value Is Nothing Then
                Me.baseTooltip.SetToolTip(Me.mcbRefNo, "OA_REF_NO is Null !." & vbCrLf & "Please Defined OA_REF_NO")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbRefNo), Me.mcbRefNo, 2000)
                'Me.baseBallonSmall.SetBalloonCaption(Me.mcbRefNo, "OA_REF_NO is NULL !.")
                'Me.baseBallonSmall.SetBalloonText(Me.mcbRefNo, "Please defined OA_REF_NO.")
                'Me.baseBallonSmall.ShowBalloon(Me.mcbRefNo)
                Me.mcbRefNo.Focus()
                Return
            ElseIf Me.mcbRefNo.SelectedIndex = -1 Then
                Me.baseTooltip.SetToolTip(Me.mcbRefNo, "OA_REF_NO is Null !." & vbCrLf & "Please Defined OA_REF_NO")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbRefNo), Me.mcbRefNo, 2000)
                'Me.baseBallonSmall.SetBalloonCaption(Me.mcbRefNo, "OA_REF_NO is NULL !.")
                'Me.baseBallonSmall.SetBalloonText(Me.mcbRefNo, "Please defined OA_REF_NO.")
                'Me.baseBallonSmall.ShowBalloon(Me.mcbRefNo)
                Me.mcbRefNo.Focus()
                Return
            ElseIf Me.mcbBrandPack.Value Is Nothing Then
                Me.baseTooltip.SetToolTip(Me.mcbBrandPack, "BrandPack is Null !." & vbCrLf & "Please Suply BrandPack Name.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2000)
                'Me.baseBallonSmall.SetBalloonCaption(Me.mcbBrandPack, "BRANDPACK is NULL !.")
                'Me.baseBallonSmall.SetBalloonText(Me.mcbBrandPack, "Please defined BRANDPACK_NAME.")
                'Me.baseBallonSmall.ShowBalloon(Me.mcbBrandPack)
                Me.mcbBrandPack.Focus()
                Return
            ElseIf Me.Mode = ModeSave.Save Then
                If Me.mcbBrandPack.SelectedIndex = -1 Then
                    Me.baseTooltip.SetToolTip(Me.mcbBrandPack, "BrandPack is Null !." & vbCrLf & "Please Suply BrandPack Name.")
                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2000)
                    'Me.baseBallonSmall.SetBalloonCaption(Me.mcbBrandPack, "BRANDPACK is NULL !.")
                    'Me.baseBallonSmall.SetBalloonText(Me.mcbBrandPack, "Please defined BRANDPACK_NAME.")
                    'Me.baseBallonSmall.ShowBalloon(Me.mcbBrandPack)
                    Me.mcbBrandPack.Focus()
                    Return
                End If
            End If
            Me.mcbBrandPack.ReadOnly = False
            Me.mcbBrandPack.SelectAll()
            Me.mcbBrandPack.Focus()
            Dim OA_BRANDPACK_ID As String = Me.mcbRefNo.Value.ToString + "" + Me.mcbBrandPack.Value.ToString() 'Me.mcbBrandPack.DropDownList().GetValue("BRANDPACK_ID").ToString()
            If Me.clsOADiscount.IsExistedOABrandPack(OA_BRANDPACK_ID) = True Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
            Else
                Me.ShowMessageInfo(Me.MessageDataSaveToAdd)
                'Dim index As Integer = Me.clsOADiscount.ViewPOBrandpack.Find(Me.mcbBrandPack.Value)
            End If
            If Me.Mode = ModeSave.Update Then
                Me.mcbBrandPack.ReadOnly = True
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ButtonEntry1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEntry1.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                Case "btnAddNew"
                    'Me.ClearControl(Me.grpEdit)
                    Me.ClearControl(Me.GrpBrandPack)
                    Me.mcbBrandPack.Value = Nothing
                    If Me.mcbRefNo.Value Is Nothing Then
                        Me.UnabledControlOABrandPack()
                        Me.mcbRefNo.Focus()
                    ElseIf Me.mcbRefNo.SelectedIndex = -1 Then
                        Me.UnabledControlOABrandPack()
                        Me.mcbRefNo.Focus()
                    Else
                        Me.EnabledControlBrandPack()
                        Me.mcbBrandPack.Focus()
                    End If
                    Me.mcbRefNo.ReadOnly = False 'Me.mcbRefNo.Enabled = True
                    Me.txtQuantity.ReadOnly = False
                    Me.ButtonEntry1.btnInsert.Text = "&Insert"
                    Me.txtRemark.Text = ""
                    Me.btnNew.Text = "Ne&w OA"
                    Me.Mode = ModeSave.Save
                Case "btnClose"
                    Me.Close()
                Case "btnInsert"
                    If Me.IsValid() = False Then
                        Return
                    End If
                    If Me.cmbKiloLitre.Text <> "" Then
                        Select Case Me.clsOADiscount.UNIT.Trim
                            Case "MILILITRE", "LITRE"
                                If Not Me.cmbKiloLitre.Text.Trim() = "LITRE" Then
                                    If Me.ShowConfirmedMessage("Are you sure you want to insert data with Unmatched Unit ?" & vbCrLf & "Please correct it if it's wrong value") = Windows.Forms.DialogResult.No Then
                                        Me.cmbKiloLitre.Focus()
                                        Return
                                    End If
                                End If
                            Case "KILOGRAM", "GRAM"
                                If Not Me.cmbKiloLitre.Text = "KILO" Then
                                    If Me.ShowConfirmedMessage("Are you sure you want to insert data with Unmatched Unit ?" & vbCrLf & "Please correct it if it's wrong value") = Windows.Forms.DialogResult.No Then
                                        Me.cmbKiloLitre.Focus()
                                        Return
                                    End If
                                End If
                            Case "SACHET"
                                If Not Me.clsOADiscount.UNIT.Trim = "SACHET" Then
                                    If Me.ShowConfirmedMessage("Are you sure you want to insert data with Unmatched Unit ?" & vbCrLf & "Please correct it if it's wrong value") = Windows.Forms.DialogResult.No Then
                                        Me.cmbKiloLitre.Focus()
                                        Return
                                    End If
                                End If
                        End Select
                    Else
                        Me.ShowMessageInfo("Unit on order is null" & vbCrLf & "Unit on BrandPack has not been set.")
                        Return
                    End If
                    Me.mcbBrandPack.ReadOnly = False : Me.mcbBrandPack.Focus() : Me.mcbBrandPack.SelectAll()
                    Me.mcbBrandPack.DroppedDown = True

                    Dim OA_BRANDPACK_ID As String = Me.mcbRefNo.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    Dim QuantityBrandPack As Decimal = 0
                    If Me.Mode = ModeSave.Update Then
                        If Me.GridEX1.GetValue("OA_ORIGINAL_QTY") <> Me.txtQuantity.Value Then
                            'check discount
                            If Me.clsOADiscount.HasDiscount(Me.GridEX1.GetValue("OA_BRANDPACK_ID")) = True Then
                                Me.ShowMessageInfo("Please delete all referenced discount before editing oa_qty")
                                Return
                            End If
                        End If
                    End If
                    If Me.Mode = ModeSave.Save Then
                        QuantityBrandPack = Me.clsOADiscount.GetQuantityOA_ORIGINAL_QTY(Me.mcbBrandPack.Value().ToString(), Me.mcbRefNo.Value().ToString(), "Save")
                    Else
                        QuantityBrandPack = Me.clsOADiscount.GetQuantityOA_ORIGINAL_QTY(Me.mcbBrandPack.Value().ToString(), Me.mcbRefNo.Value().ToString(), "Update")
                    End If
                    If Me.txtQuantity.Value > QuantityBrandPack Then
                        Me.baseTooltip.Show("Quantity BrandPack exceeds from PO BRANDPACK" & vbCrLf & "Quantity PO_BrandPack left is " & QuantityBrandPack.ToString(), Me.txtQuantity, 3000)
                        Return
                    End If

                    Me.clsOADiscount.OA_BRANDPACK_ID = OA_BRANDPACK_ID
                    Me.clsOADiscount.OA_ID = Me.mcbRefNo.Value.ToString()
                    Dim ProjBrandpackID As Object = Nothing
                    If Me.Mode = ModeSave.Save Then
                        Me.clsOADiscount.BRANDPACK_ID = Me.mcbBrandPack.DropDownList().GetValue("BRANDPACK_ID").ToString()
                        ProjBrandpackID = Me.mcbBrandPack.DropDownList().GetValue("PROJ_BRANDPACK_ID")
                        If Not IsNothing(ProjBrandpackID) And Not IsDBNull(ProjBrandpackID) Then
                            If Not String.IsNullOrEmpty(ProjBrandpackID) Then
                                Me.clsOADiscount.PROJ_BRANDPACK_ID = ProjBrandpackID.ToString()
                            Else
                                Me.clsOADiscount.PROJ_BRANDPACK_ID = DBNull.Value
                            End If
                        Else
                            Me.clsOADiscount.PROJ_BRANDPACK_ID = DBNull.Value
                        End If
                    Else
                        Me.clsOADiscount.BRANDPACK_ID = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
                        Me.clsOADiscount.PROJ_BRANDPACK_ID = Me.GridEX1.GetValue("PROJ_BRANDPACK_ID")
                    End If

                    Me.mcbBrandPack.DroppedDown = False
                    Me.clsOADiscount.BRANDPACK_NAME = Me.mcbBrandPack.Text
                    Me.clsOADiscount.OA_ORIGINAL_QTY = Me.txtQuantity.Value
                    Me.clsOADiscount.OA_PRICE_PERQTY = Me.txtPrice.Value
                    Me.clsOADiscount.OA_TOTAL_PRICE = Me.txtTotal.Value
                    Me.clsOADiscount.TOTAL_DISC_QTY = 0
                    Me.clsOADiscount.TOTAL_AMOUNT_DISC = 0
                    Me.clsOADiscount.PO_BRANDPACK_ID = Me.mcbBrandPack.Value.ToString()
                    Me.clsOADiscount.UNIT = Me.cmbKiloLitre.Text.Trim()
                    Me.clsOADiscount.RemarkOA = Me.txtRemark.Text
                    Me.SFG = StateFillingGrid.Filling
                    Select Case Me.Mode
                        Case ModeSave.Save
                            If Not CMAin.IsSystemAdministrator Then
                                If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OA_BranPack = True Then
                                    If Me.clsOADiscount.IsExistedOABrandPack(OA_BRANDPACK_ID) = True Then
                                        Me.ShowMessageInfo("Data " & Me.mcbRefNo.Text & " with BrandPack " & Me.mcbBrandPack.Text & " has Existed !." & _
                                        vbCrLf & "Data row per OA must be unique.")
                                        Me.SFG = StateFillingGrid.HasFilled
                                        Return
                                    End If
                                    Me.clsOADiscount.SaveOA_BRANDPACK("Save")
                                Else : Me.ShowMessageInfo("You have no privilege for such operation") : Return : End If
                            Else
                                If Me.clsOADiscount.IsExistedOABrandPack(OA_BRANDPACK_ID) = True Then
                                    Me.ShowMessageInfo("Data " & Me.mcbRefNo.Text & " with BrandPack " & Me.mcbBrandPack.Text & " has Existed !." & _
                                    vbCrLf & "Data row per OA must be unique.")
                                    Me.SFG = StateFillingGrid.HasFilled
                                    Return
                                End If
                                Me.clsOADiscount.SaveOA_BRANDPACK("Save")
                            End If
                        Case ModeSave.Update
                            If Not CMAin.IsSystemAdministrator Then
                                If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.OA_BranPack = True Then
                                    Me.clsOADiscount.SaveOA_BRANDPACK("Update") : Me.ShowMessageInfo(Me.MessageSavingSucces)
                                Else : Me.ShowMessageInfo("You have no privilege for such operation") : Return
                                End If
                            Else
                                Me.clsOADiscount.SaveOA_BRANDPACK("Update") : Me.ShowMessageInfo(Me.MessageSavingSucces)
                            End If
                    End Select
                    Me.GridEX1.Refetch() : Me.InflateDataFromMCB(Me.mcbRefNo)
                    Me.mcbBrandPack.Value = Me.clsOADiscount.PO_BRANDPACK_ID
                    Me.UnabledControlOABrandPack() : Me.mcbRefNo.ReadOnly = True 'Me.mcbRefNo.Enabled = False
                    Me.NavigationPane1.Enabled = False : Me.BindGridRemainding(Nothing)
                    Me.BindGridEx3(Nothing) : Me.BindGridEx(Me.GridEX2, Nothing)
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ButtonEntry1_btnClick_" + CType(sender, Janus.Windows.EditControls.UIButton).Name)
        Finally
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try

            Me.Cursor = Cursors.WaitCursor
            Dim frmOA As New OrderAcceptance()
            If Me.btnNew.Text = "Ne&w OA" Then
                frmOA.Mode = OrderAcceptance.ModeSave.Save
            Else
                frmOA.Mode = OrderAcceptance.ModeSave.Update
                frmOA.UM = OrderAcceptance.UpdateMode.FromOriginal
                frmOA.OA_ID = Me.mcbRefNo.Value.ToString()
                If Me.DistributorID = "" Then
                    Me.DistribtorName = Me.clsOADiscount.PO_From(Me.mcbRefNo.Value.ToString(), False)
                    Me.DistributorID = Me.clsOADiscount.DISTRIBUTOR_ID
                End If
                frmOA.DISTRIBUTOR_ID = Me.DistributorID
                frmOA.DISTRIBUTOR_NAME = Me.DistribtorName
                frmOA.OA_ID = Me.mcbRefNo.Value.ToString()
                frmOA.txtOARef.Text = Me.mcbRefNo.Value.ToString()
                frmOA.btnDelete.Visible = True
            End If
            frmOA.InitializeData() : frmOA.txtOARef.ReadOnly = True
            frmOA.ShowDialog(Me) : Dim OriginalValue As Object = Me.mcbRefNo.Value
            Me.mcbRefNo.Value = Nothing : Me.SFG = StateFillingGrid.Filling
            Me.SI_MCB = SelectedItemMCB.FromMCB : Me.Mode = ModeSave.Save : Me.btnNew.Text = "Ne&w OA"
            If NufarmBussinesRules.SharedClass.OA_REF_NO <> "" Then
                Dim dtview As DataView = Me.clsOADiscount.SearchOARef(NufarmBussinesRules.SharedClass.OA_REF_NO)
                Me.BindMulticolumnCombo(Me.mcbRefNo, Me.clsOADiscount.ViewOARefNo, True)
                Me.mcbRefNo.Value = NufarmBussinesRules.SharedClass.OA_REF_NO
            Else
                Me.mcbRefNo.Value = OriginalValue
            End If
            NufarmBussinesRules.SharedClass.OA_REF_NO = ""
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_btnNew_Click")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.SI_MCB = SelectedItemMCB.FromMCB
            Me.Cursor = Cursors.Default
            Me.SFG = StateFillingGrid.HasFilled
            Me.SFM = StateFillingMCB.HasFilled
        End Try
    End Sub

    Private Sub btnNewGiven_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewGiven.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim frmNewGiven As New NewGiven()
            frmNewGiven.ShowInTaskbar = False
            If Me.GridEX1.RecordCount <= 0 Then
                frmNewGiven.InitializeData()
                frmNewGiven.ShowDialog(Me)
                Return
            End If
            If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If Not IsNothing(Me.GridEX1.GetValue("BRANDPACK_ID")) Then
                    If (Me.clsOADiscount.DISTRIBUTOR_ID <> "") Then
                        frmNewGiven.InitializeData(Me.clsOADiscount.DISTRIBUTOR_ID, Me.GridEX1.GetValue("BRANDPACK_ID").ToString())
                    End If
                ElseIf Me.clsOADiscount.DISTRIBUTOR_ID <> "" Then
                    frmNewGiven.InitializeData(Me.clsOADiscount.DISTRIBUTOR_ID)
                Else
                    frmNewGiven.InitializeData()
                End If
            ElseIf Me.clsOADiscount.DISTRIBUTOR_ID <> "" Then
                frmNewGiven.InitializeData(Me.clsOADiscount.DISTRIBUTOR_ID)
            Else
                frmNewGiven.InitializeData()
            End If
            frmNewGiven.ShowDialog(Me)
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnInsertSystem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertSystem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'check to make sure data is available and
            If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack")
                Return
            End If
            If Me.grdRemainding.RecordCount <= 0 Then
                Me.ShowMessageInfo("No more data's to include")
                Return
            End If
            If Me.ShowConfirmedMessage("Insert all remaind data(s) by system ?" & vbCrLf & "System will recalculate data(s) which could be released") = Windows.Forms.DialogResult.No Then
                Return
            End If

            Dim OA_BRANDPACK_ID As String = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
            Dim PRICE As Decimal = Convert.ToDecimal(Me.GridEX1.GetValue("OA_PRICE_PERQTY"))
            If IsDBNull(PRICE) Then
                Me.ShowMessageInfo("PRICE for that brandpack is null")
                Return
            End If
            'KALIKAN QUANTITY DENGAN PRICE

            Dim OrgDecimalValue As Decimal = Me.clsOADiscount.getTotalQTY(Me.GridEX1.GetValue("OA_BRANDPACK_ID"), False)
            'Dim Devided_Qty As Decimal = Me.clsOADiscount.GetDevided_QTY(Me.GridEX1.GetValue("BRANDPACK_ID").ToString())
            Dim LeftQTY As Decimal = OrgDecimalValue Mod Devided_Qty
            Dim TOTAL As Decimal = Convert.ToDecimal(Me.grdRemainding.GetTotal(Me.grdRemainding.RootTable.Columns("LEFT_QTY"), Janus.Windows.GridEX.AggregateFunction.Sum, _
                 Nothing))
            Dim MinusDevidedQty As Decimal = 0 'untuk mengetahu kekurangan nya discount agar genap
            Dim lastRemain As Decimal = 0 'Nilai decimal dari remaind yang tidak bisa di ambil
            If LeftQTY > 0 Then
                'untuk menjadikan genap = devided_qty - kekurangan nya = 
                'Devided_qty - LeftQty
                MinusDevidedQty = Devided_Qty - LeftQTY
                If TOTAL < MinusDevidedQty Then
                    Me.ShowMessageInfo(String.Format("Qty < {0:#,##0.000}", MinusDevidedQty))
                    Return
                End If
            Else
                If TOTAL < Devided_Qty Then
                    Me.ShowMessageInfo(String.Format("Qty < {0:#,##0.000}", Devided_Qty))
                    Return
                End If
            End If
            Dim ResultQty As Decimal = 0 'batasan untuk memberikan discount = ResultQty
            If LeftQTY > 0 Then
                'kurangankan total dengan minusDevidedqty
                ResultQty = MinusDevidedQty
                TOTAL = TOTAL - MinusDevidedQty
                'sisanya
                If TOTAL >= Devided_Qty Then
                    If TOTAL Mod Devided_Qty > 0 Then
                        lastRemain = TOTAL Mod Devided_Qty
                        ResultQty = ResultQty + (TOTAL - lastRemain)
                    Else
                        ResultQty = ResultQty + TOTAL
                    End If
                Else
                    lastRemain = TOTAL ''sisa total yang sudah di kurangi minusdevidedqty
                End If
            Else
                ResultQty = TOTAL
            End If
            'Dim ResultQty As Decimal = Decimal.Truncate((LeftQTY + TOTAL) / Devided_Qty) * Devided_Qty
            Dim AmountPrice As Decimal = 0
            AmountPrice = PRICE * ResultQty
            'ambil price yang ada di pnl oa_discount
            Dim DISC_PRICE_OA As Decimal = 0
            Dim x As String = ""
            Dim w As Integer = Me.pnlOADiscount.TitleText.IndexOf(":") + 3
            Dim s As String
            Dim a As String = ""
            Do
                s = Microsoft.VisualBasic.Mid(Me.pnlOADiscount.TitleText, w, 1)
                If (s = "") Or (s = ".") Then
                Else
                    a &= s
                End If
                w += 1
            Loop Until s = ""
            'GET TOTAL PRICE
            DISC_PRICE_OA = Convert.ToDecimal(a) 'TOTAL DISCOUNT OA

            Dim SUMPRICE As Object = Me.clsOADiscount.GetTotalPriceOAGiven(Me.mcbRefNo.Value.ToString(), False) 'TOTAL DISCOUNT YG TELAH DIBERIKAN
            Dim TOTAL_LEFT_PRICE As Decimal = DISC_PRICE_OA - Convert.ToDecimal(SUMPRICE)
            Dim LEFT_QTY_PRICE As Decimal = TOTAL_LEFT_PRICE / PRICE
            If (AmountPrice + Convert.ToDecimal(SUMPRICE)) > DISC_PRICE_OA Then
                Me.ShowMessageInfo("Discount value will exceed allowed-data" & vbCrLf & "Discount has been released " & _
                String.Format("{0:#,##0.00}", SUMPRICE) & vbCrLf & "Total left discount allowed is : " & String.Format("{0:#,##0.000}", LEFT_QTY_PRICE))
                Return
            End If
            Dim dtview As DataView = CType(Me.grdRemainding.DataSource, DataView)
            Me.clsOADiscount.RecalCulateQTY(dtview, OA_BRANDPACK_ID, ResultQty, PRICE)
            'bikin table
            ' Me.isInsertingOADiscount = True
            Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_btnInsertSystem_Click")
            Me.ShowMessageInfo(ex.Message)
        Finally
            ' Me.isInsertingOADiscount = false
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mcbRefNo.Value Is Nothing Then
                Me.ShowMessageInfo("Please define OA_REF_NO")
                Return
            ElseIf Me.mcbRefNo.SelectedItem Is Nothing Then
                Me.ShowMessageInfo("Please define OA_REF_NO")
                Return
            ElseIf Me.GridEX1.RecordCount() <= 0 Then
                Me.ShowMessageInfo("No data can be printed" & vbCrLf & "If you're sure data's existed please Refresh")
                Return
            End If
            Dim frmPrintOA As New PrintOA()
            Dim PODate As Date = Convert.ToDateTime(Me.txTPOREFDATE.Text)
            frmPrintOA.InitializeData(Me.mcbRefNo.Value.ToString(), PODate)
            frmPrintOA.ShowDialog(Me)

        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnPrint_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnGenerateOtherDiscount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateOtherDiscount.Click
        If (Me.mcbRefNo.Value Is Nothing) Or (Me.mcbRefNo.SelectedItem Is Nothing) Then
            Me.ShowMessageInfo("Please define OA_REF_NO to compute by system")
            Return
        End If
        If Me.GridEX1.RecordCount <= 0 Then
            Me.ShowMessageInfo("Please define Brandpack name")
            Return
        End If
        If Me.GridEX1.SelectedItems.Count = 0 Then
            Me.ShowMessageInfo("Please define Brandpack name")
            Return
        End If
        If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
            Me.ShowMessageInfo("Please define Brandpack name")
            Return
        End If
        Dim Price As Decimal = Convert.ToDecimal(Me.GridEX1.GetValue("OA_PRICE_PERQTY"))
        Dim TotalDiscOut As Decimal = 0
        Dim Flag As String = "O"
        Try
            Dim IndexRow As Integer = Me.GridEX1.Row
            Me.OtherDDDRCBD = New OtherDDDR()
            With Me.OtherDDDRCBD
                If Me.rdbCBD.Checked Then
                    .btnDiscCBD.Checked = True
                    .TypeApp = "OCBD"
                    Flag = "OCBD"
                ElseIf Me.rdbDD.Checked Then
                    .btnDiscDD.Checked = True
                    .TypeApp = "ODD"
                    Flag = "ODD"
                ElseIf Me.rdbDR.Checked Then
                    .btnDiscDr.Checked = True
                    .TypeApp = "ODR"
                    Flag = "ODR"
                ElseIf Me.rdbUncategorized.Checked Then
                    .btnUncategorized.Checked = True
                    .TypeApp = "O"
                ElseIf Me.rdbDK.Checked Then
                    .btnDiscDK.Checked = True
                    .TypeApp = "ODK"
                    Flag = "ODK"
                ElseIf Me.rdbDP.Checked Then
                    .btnDiscDP.Checked = True
                    .TypeApp = "ODP"
                    Flag = "ODP"
                End If
                .PODate = Convert.ToDateTime(Me.txTPOREFDATE.Text)
                .BrandPackID = Me.GridEX1.GetValue("BRANDPACK_ID")
                If Me.DistributorID = "" Then
                    Me.clsOADiscount.PO_From(Me.mcbRefNo.Value.ToString(), True)
                    Me.DistribtorName = Me.clsOADiscount.DISTRIBUTOR_NAME
                    Me.DistributorID = Me.clsOADiscount.DISTRIBUTOR_ID
                End If
                .DistributorID = Me.DistributorID
                .Devided_Qty = Me.Devided_Qty
                .OAQty = Me.txtQuantity.Value
                .OABrandPackID = Me.OA_BRANDPACK_ID
                .PricePrQty = Price
                .BrandPackName = Me.GridEX1.GetValue("BRANDPACK_NAME").ToString()
                If Me.DistributorID = "" Then
                    Me.clsOADiscount.PO_From(Me.mcbRefNo.Value.ToString(), True)
                    Me.DistribtorName = Me.clsOADiscount.DISTRIBUTOR_NAME
                    Me.DistributorID = Me.clsOADiscount.DISTRIBUTOR_ID
                End If
                Dim tblResult As New DataTable("T_Result")
                If .ShowDialog(tblResult, Flag) = Windows.Forms.DialogResult.OK Then
                    Me.InsertOthersDDDRCBD(tblResult, TotalDiscOut)
                End If
            End With
            Me.SFG = StateFillingGrid.Filling
            Dim GridExDataView As DataView = CType(Me.GridEX1.DataSource, DataView)
            Dim index As Integer = CType(Me.GridEX1.DataSource, DataView).Find(OA_BRANDPACK_ID)
            If index <> -1 Then
                Dim TotalDiscQty As Decimal = Convert.ToDecimal(GridExDataView(index)("TOTAL_DISC_QTY")) + TotalDiscOut
                GridExDataView(index)("TOTAL_DISC_QTY") = TotalDiscQty
                GridExDataView(index)("TOTAL_AMOUNT_DISC") = TotalDiscQty * Price
                GridExDataView(index).EndEdit()
            End If
            'tampilkan discount
            Me.clsOADiscount.CreateViewOABrandPackDiscount(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False)
            Me.BindGridEx(Me.GridEX2, Me.clsOADiscount.ViewOADiscount)
            'cek opsi yang di pilih user
            Dim OQData = Me.QData
            Me.QData = QueryData.GridSelected
            Select Case Flag
                Case "OCBD"
                    Me.rdbCBD.Checked = True
                    Me.rdbCBD_CheckedChanged(Me.rdbCBD, New EventArgs())
                Case "ODD"
                    Me.rdbDD.Checked = True
                    Me.rdbDD_CheckedChanged(Me.rdbDD, New EventArgs())
                Case "ODR"
                    Me.rdbDR.Checked = True
                    Me.rdbDR_CheckedChanged(Me.rdbDR, New EventArgs())
                Case "ODK"
                    Me.rdbDK.Checked = True
                    Me.rdbDK_CheckedChanged(Me.rdbDR, New EventArgs())
                Case "ODP"
                    Me.rdbDP.Checked = True
                    Me.rdbDP_CheckedChanged(Me.rdbDP, New EventArgs())
            End Select
            Dim UnitOnOrder As String = ""
            If Not IsDBNull(Me.GridEX1.GetValue("UNIT_ORDER")) Then
                UnitOnOrder = Me.GridEX1.GetValue("UNIT_ORDER").ToString()
            End If
            Dim OrgDecimalValue As Decimal = Me.clsOADiscount.getTotalQTY(Me.GridEX1.GetValue("OA_BRANDPACK_ID"), True)
            Dim LEFT_QTY = OrgDecimalValue Mod Devided_Qty
            Me.pnlTotalRemainder.Text = "Still Remainder =  " & String.Format("{0:#,##0.000}", LEFT_QTY * Devide_Factor) & "  " & Unit & ", = " & LEFT_QTY.ToString() & "  " & UnitOnOrder
            'balikan state
            Me.QData = OQData
            Me.SFG = StateFillingGrid.HasFilled
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_btnGenerateOtherDiscount_Click") : Me.ShowMessageInfo(ex.Message)
        Finally
            If Me.SFM = StateFillingMCB.Filling Then
                Me.SFM = StateFillingMCB.HasFilled
            End If
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
            Me.SI_MCB = SelectedItemMCB.FromMCB
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#Region " Radio Button "

    Private Sub rdbPeriodDiscount1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPeriodDiscount1.CheckedChanged
        Try

            If Me.rdbPeriodDiscount1.Checked = True Then
                Me.btnGenerateAgreement.Enabled = False
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount1.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount1.Checked = False
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If

                Me.CreateViewQuarterlyDiscount(Quarterly.Q1, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), _
                Me.clsOADiscount.DISTRIBUTOR_ID, Convert.ToDateTime(Me.txTPOREFDATE.Text), MustcloseConnection)

            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbPeriodDiscount1")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Sub rdbPeriodDiscount2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPeriodDiscount2.CheckedChanged
        Try
            If Me.rdbPeriodDiscount2.Checked = True Then
                Me.btnGenerateAgreement.Enabled = False
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount2.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount2.Checked = False
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                Me.CreateViewQuarterlyDiscount(Quarterly.Q2, Me.GridEX1.GetValue("BRANDPACK_ID").ToString, Me.clsOADiscount.DISTRIBUTOR_ID, Convert.ToDateTime(Me.txTPOREFDATE.Text), MustcloseConnection)
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbPeriodDiscount2")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Sub rdbPeriodDiscount3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPeriodDiscount3.CheckedChanged
        Try

            If Me.rdbPeriodDiscount3.Checked = True Then
                Me.btnGenerateAgreement.Enabled = False
                ''check data jika sudah berakhir semesterly 2
                'me.clsOADiscount.cre
                'tampilkan data jika sudah ada di generate ' hanya data yang ada left nya dari semeterly 2
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount3.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount3.Checked = False
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                Me.CreateViewQuarterlyDiscount(Quarterly.Q3, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), _
                Me.clsOADiscount.DISTRIBUTOR_ID, Convert.ToDateTime(Me.txTPOREFDATE.Text), MustcloseConnection)
            End If

        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbPeriodDiscount3")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Sub rdbPeriodDiscount4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPeriodDiscount4.CheckedChanged
        Try
            If Me.rdbPeriodDiscount4.Checked = True Then
                Me.btnGenerateAgreement.Enabled = False
                'tampilkan data jika sudah ada di generate ' hanya data yang ada left nya dari semeterly 2
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount4.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount4.Checked = False
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                Me.CreateViewQuarterlyDiscount(Quarterly.Q4, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, Convert.ToDateTime(Me.txTPOREFDATE.Text), MustcloseConnection)
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbPeriodDiscount4")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Sub rdbSemesterly1Discount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSemesterly1Discount.CheckedChanged
        Try
            If Me.rdbSemesterly1Discount.Checked Then
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount4.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount4.Checked = False
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                Me.CreateViewSemesterlyDiscount(Semesterly.S1, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, DateTime.Parse(Me.txTPOREFDATE.Text), MustcloseConnection)
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbSemesterly1Discount")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Sub rdbSemesterly2Discount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSemesterly2Discount.CheckedChanged
        Try
            If Me.rdbSemesterly2Discount.Checked Then
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount4.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbPeriodDiscount4.Checked = False
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                Me.CreateViewSemesterlyDiscount(Semesterly.S2, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, DateTime.Parse(Me.txTPOREFDATE.Text), MustcloseConnection)
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbSemesterly2Discount")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Sub rdbYearlyDiscount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbYearlyDiscount.CheckedChanged
        Try

            If Me.rdbYearlyDiscount.Checked = True Then
                Me.btnGenerateAgreement.Enabled = False
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbYearlyDiscount.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbYearlyDiscount.Checked = False
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If

                Me.CreateViewYearlyDiscount(Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, Convert.ToDateTime(Me.txTPOREFDATE.Text), MustcloseConnection)
                'Me.StrAgreementDiscount.TypeDiscount = AgreementDiscount.DiscountType.YearlyDiscount
                'check data jika sudah berakhir yearly
                'jika data belum berakhir tampilkan yearly discount berdasarkan brand_agreement yang di miliki oleh distributor berdasarkan data
                'tampilkan data jika sudah ada di generate ' hanya data yang ada left nya dari semeterly 2
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbYearlyDiscount")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try

    End Sub

    Private Sub rdbGivenDiscountMarketing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGivenDiscountMarketing.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbGivenDiscountMarketing.Checked = True Then
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbGivenDiscountMarketing.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbGivenDiscountMarketing.Checked = False
                    Return
                End If
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If

                Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, "MG", False)
                If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "MG", MustcloseConnection) = True Then
                    Me.btnGenerateMarketing.Enabled = False
                Else
                    Me.btnGenerateMarketing.Enabled = True
                End If
                'Else
                '    Me.btnGenerateMarketing.Enabled = False
            End If
            'Me.StrMarketingDiscount.TypeDiscount = MarketingDiscount.DiscountType.GivenDiscount

        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbGivenDiscountMarketing")
        Finally
            Me.ReadAcces() : If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Sub rdbGivenDiscount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGivenDiscount.CheckedChanged
        Try
            If Me.rdbGivenDiscount.Checked = True Then
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbGivenDiscount.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbGivenDiscount.Checked = False
                    Return
                End If
                'Me.StrAgreementDiscount.TypeDiscount = AgreementDiscount.DiscountType.GivenDiscount
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()

                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                Me.CreateViewGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID"), Me.clsOADiscount.DISTRIBUTOR_ID, False)
                If Me.clsOADiscount.HasGeneratedDiscountAgreement(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False, False, MustcloseConnection, Nothing, Nothing, True, False) = True Then
                    Me.btnGenerateAgreement.Enabled = False
                Else
                    Me.btnGenerateAgreement.Enabled = True
                End If
                Me.AgreementDiscount = AgrementDiscountType.GivenDiscount
                'check seluruh data given discount yang belum di ambil
                'jika ada tampilkan
                'Me.clsOADiscount.CreateViewGivenDiscount(Me.GridEX1.GetValue("BRANDPACK_ID"), NufarmBussinesRules.OrderAcceptance.OADiscount.GivenDiscount.AgreementDiscount, NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity, Me.GridEX1.GetValue("DISTRIBUTOR_ID"))
                'If Me.clsOADiscount.ViewGivenDiscount.Table.Rows.Count > 0 Then
                '    Me.BindGrid(Me.clsOADiscount.ViewGivenDiscount, "")
                'Else
                '    'jika belum  clearkan datagrid windows
                '    Me.BindGrid(Nothing, "")
                'End If
            Else
                Me.btnGenerateAgreement.Enabled = False
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbGivenDiscount")
        Finally
            Me.ReadAcces() : If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Sub rdbGivenPKPP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGivenPKPP.Click
        Try

            If Me.rdbGivenPKPP.Checked = True Then
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbGivenPKPP.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbGivenPKPP.Checked = False
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor : Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()

                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If

                Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, "KP", False)
                If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "KP", MustcloseConnection) = True Then
                    Me.btnGenerateMarketing.Enabled = False
                Else
                    Me.btnGenerateMarketing.Enabled = True
                End If
            Else
                Me.btnGenerateMarketing.Enabled = False
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbGivenPKPP_Click")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Sub rdbGivenDK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGivenDK.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbGivenDK.Checked = True Then
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbGivenDK.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbGivenDK.Checked = False
                    Return
                End If
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, "DK", MustcloseConnection)
                If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "DK", MustcloseConnection) = True Then
                    Me.btnGenerateMarketing.Enabled = False
                Else
                    Me.btnGenerateMarketing.Enabled = True
                End If
            Else
                Me.btnGenerateMarketing.Enabled = False
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbGivenDK_Click")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Sub rdbGivenCPR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGivenCPR.Click
        Try
            If Me.rdbGivenCPR.Checked = True Then
                If Me.GridEX1.SelectedItems.Count = 0 Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                       & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbGivenCPR.Checked = False
                    Return
                End If
                If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                    & vbCrLf & "And DISTRIBUTOR'S PO")
                    Me.BindGridEx3(Nothing)
                    Me.rdbGivenCPR.Checked = False
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, "CR", False)
                If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "CR", MustcloseConnection) = True Then
                    Me.btnGenerateMarketing.Enabled = False
                Else
                    Me.btnGenerateMarketing.Enabled = True
                End If
            Else
                Me.btnGenerateMarketing.Enabled = False
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbCP_Click")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub
    Private Function rdbGivenCPChecked() As Boolean
        If Me.rdbGivenCP.Checked = True Then
            If Me.GridEX1.SelectedItems.Count = 0 Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                   & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                Me.rdbGivenCP.Checked = False
                Return False
            End If
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                Me.rdbGivenCP.Checked = False
                Return False
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
            Dim MustcloseConnection As Boolean = False
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
            Else : MustcloseConnection = True
            End If
            Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, "CP", False)
            If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "CP", MustcloseConnection) = True Then
                Me.btnGenerateMarketing.Enabled = False
            Else
                Me.btnGenerateMarketing.Enabled = True
            End If
        Else
            Me.btnGenerateMarketing.Enabled = False
        End If
        Return True
    End Function

    Private Sub rdbGivenCP_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles rdbGivenCP.CheckedChanged
        Try
            'FLAG = CP
            Me.rdbGivenCPChecked()
            Me.ClearRadioButton()
        Catch ex As Exception
            e.Cancel = True : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbCP_Click")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub
    Private Function rdbSpecialCPDChecked() As Boolean
        If (Me.rdbSpecialCPD.Checked) Then
            If Me.GridEX1.SelectedItems.Count = 0 Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                   & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                rdbSpecialCPD.Checked = False
                Return False
            End If
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                rdbSpecialCPD.Checked = False
                Return False
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
            Dim MustcloseConnection As Boolean = False
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
            Else : MustcloseConnection = True
            End If
            Dim Flag As String = Me.getFlagCPD(FlagCPD.CPDSDistributor)
            Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, Flag, False)
            If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), Flag, MustcloseConnection) = True Then
                Me.btnGenerateMarketing.Enabled = False
            Else
                Me.btnGenerateMarketing.Enabled = True
            End If
        End If
        Return True
    End Function
    Private Sub rdbSpecialCPD_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles rdbSpecialCPD.CheckedChanged
        Try
            rdbSpecialCPDChecked()
            Me.ClearRadioButton()
        Catch ex As Exception
            e.Cancel = True : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbCP_Click")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Function rdbGivenCP_TMChecked() As Boolean
        If Me.rdbGivenCP_TM.Checked Then
            If Me.GridEX1.SelectedItems.Count = 0 Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                   & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                rdbGivenCP_TM.Checked = False
                Return False
            End If
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                rdbGivenCP_TM.Checked = False
                Return False
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
            Dim MustcloseConnection As Boolean = False
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
            Else : MustcloseConnection = True
            End If
            Dim Flag As String = Me.getFlagCPD(FlagCPD.CPDTMDistributor)
            Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, Flag, False)
            If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), Flag, MustcloseConnection) = True Then
                Me.btnGenerateMarketing.Enabled = False
            Else
                Me.btnGenerateMarketing.Enabled = True
            End If
            Return True
        End If
    End Function
    Private Sub rdbGivenCP_TM_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles rdbGivenCP_TM.CheckedChanged
        Try
            rdbGivenCP_TMChecked()
            Me.ClearRadioButton()
        Catch ex As Exception
            e.Cancel = True : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_rdbGivenCP_TM_Click")
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub
    Private Function rdbSpeacialCPDTMCheked() As Boolean
        If Me.rdbSpecialCPD_TM.Checked Then
            If Me.GridEX1.SelectedItems.Count = 0 Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                   & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                rdbSpecialCPD_TM.Checked = False
                Return False
            End If
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                rdbSpecialCPD_TM.Checked = False
                Return False
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
            Dim MustcloseConnection As Boolean = False
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
            Else : MustcloseConnection = True
            End If
            Dim Flag As String = Me.getFlagCPD(FlagCPD.CPDSDistributor_TM)
            Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, Flag, False)
            If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), Flag, MustcloseConnection) = True Then
                Me.btnGenerateMarketing.Enabled = False
            Else
                Me.btnGenerateMarketing.Enabled = True
            End If
        End If
        Return True
    End Function

    Private Sub rdbSpecialCPD_TM_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles rdbSpecialCPD_TM.CheckedChanged
        Try
            rdbSpeacialCPDTMCheked()
            Me.ClearRadioButton()
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbSpecialCPD_TM_Click") : Me.ShowMessageInfo(ex.Message)
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Function rdbCPMRT_Dist_Checked() As Boolean
        If Me.rdbCPMRT_Dist.Checked Then
            If Me.GridEX1.SelectedItems.Count = 0 Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                   & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                rdbSpecialCPD_TM.Checked = False
                Return False
            End If
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                rdbSpecialCPD_TM.Checked = False
                Return False
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
            Dim MustcloseConnection As Boolean = False
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
            Else : MustcloseConnection = True
            End If
            Dim Flag As String = Me.getFlagCPD(FlagCPD.CPMRTDistributor)
            Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, Flag, False)
            If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), Flag, MustcloseConnection) = True Then
                Me.btnGenerateMarketing.Enabled = False
            Else
                Me.btnGenerateMarketing.Enabled = True
            End If
        End If
    End Function
    Private Sub rdbCPMRT_Dist_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles rdbCPMRT_Dist.CheckedChanged
        Try
            rdbCPMRT_Dist_Checked()
            Me.ClearRadioButton()
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbCPMRT_Dist_CheckedChanged") : Me.ShowMessageInfo(ex.Message)
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub
    Private Function rdbCPMRT_TMDist_Checked() As Boolean
        If Me.rdbCPMRT_TMDist.Checked Then
            If Me.GridEX1.SelectedItems.Count = 0 Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                   & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                rdbSpecialCPD_TM.Checked = False
                Return False
            End If
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                rdbSpecialCPD_TM.Checked = False
                Return False
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
            Dim MustcloseConnection As Boolean = False
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
            Else : MustcloseConnection = True
            End If
            Dim Flag As String = Me.getFlagCPD(FlagCPD.CPMRTDsitributor_TM)
            Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, Flag, False)
            If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), Flag, MustcloseConnection) = True Then
                Me.btnGenerateMarketing.Enabled = False
            Else
                Me.btnGenerateMarketing.Enabled = True
            End If
        End If
    End Function

    Private Sub rdbCPMRT_TMDist_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles rdbCPMRT_TMDist.CheckedChanged
        Try
            rdbCPMRT_TMDist_Checked()
            Me.ClearRadioButton()
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbCPMRT_TMDist_CheckedChanged") : Me.ShowMessageInfo(ex.Message)
        Finally
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : Me.Cursor = Cursors.WaitCursor : Else : Me.Cursor = Cursors.Default : End If
        End Try
    End Sub

    Private Sub rdbDKN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDKN.Click
        Try
            If Me.GridEX1.SelectedItems.Count = 0 Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                   & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                Me.rdbGivenCPR.Checked = False
                Return
            End If
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                Me.rdbGivenCPR.Checked = False
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
            Dim MustcloseConnection As Boolean = False
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
            Else : MustcloseConnection = True
            End If
            Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, "DN", False)
            If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "DN", MustcloseConnection) = True Then
                Me.btnGenerateMarketing.Enabled = False
            Else
                Me.btnGenerateMarketing.Enabled = True
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbDKN_Click") : Me.ShowMessageInfo(ex.Message)
        End Try

    End Sub

    Private Sub rdbCPDAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCPDAuto.Click
        Try
            If Me.GridEX1.SelectedItems.Count = 0 Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                   & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                Me.rdbGivenCPR.Checked = False
                Return
            End If
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                & vbCrLf & "And DISTRIBUTOR'S PO")
                Me.BindGridEx3(Nothing)
                Me.rdbGivenCPR.Checked = False
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
            Dim MustcloseConnection As Boolean = False
            If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
            Else : MustcloseConnection = True
            End If
            Me.CreateViewMarketingGivenDiscount(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID, "CA", False)
            If Me.clsOADiscount.HasGeneratedDiscountMarketing(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "CA", MustcloseConnection) = True Then
                Me.btnGenerateMarketing.Enabled = False
            Else
                Me.btnGenerateMarketing.Enabled = True
            End If
            Me.ClearCheckBox()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbDKN_Click") : Me.ShowMessageInfo(ex.Message)
        End Try
    End Sub

    Private Sub QTY_SetAdjustment(ByVal sender As Object, ByVal e As System.EventArgs) Handles QTY.SetAdjustment
        Try
            If Me.QTY.chkSetAdjustment.Checked Then
                Me.QTY.txtAdjust1.Enabled = IIf(CDec(Me.QTY.txtAdjust1.Value) > 0, True, False)
                Me.QTY.txtAdjust2.Enabled = IIf(CDec(Me.QTY.txtAdjust2.Value) > 0, True, False)
            Else
                Me.QTY.txtAdjust1.Enabled = False
                Me.QTY.txtAdjust2.Enabled = False
            End If
        Catch ex As Exception

        End Try

    End Sub

    'Private Sub rdbDD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub

    'Private Sub rdbDR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub

    'Private Sub rdbCBD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub

    'Private Sub rdbUncategorized_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Not IsNothing(Me.Other_QTY) Then
    '            Me.Other_QTY.Close()
    '            Me.Other_QTY = Nothing
    '        End If
    '        Me.Other_QTY = New Other_QTY()
    '        Me.Other_QTY.ShowInTaskbar = False
    '        Me.Other_QTY.ShowDialog(Me)
    '    Catch ex As Exception

    '    End Try
    'End Sub
#End Region

#Region " Navigation Pane "

    Private Sub NavigationPane1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NavigationPane1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            ' Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            If Me.GridEX1.RecordCount > 0 Then
                If Me.GridEX1.SelectedItems.Count > 0 Then
                    If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                    Else
                        Me.pnlGridDiscount.TitleText = ""
                        Return
                    End If
                Else
                    Me.pnlGridDiscount.TitleText = ""
                    Return
                End If
            Else
                Me.pnlGridDiscount.TitleText = ""
                Return
            End If
            If Not IsNothing(Me.GridEX1.GetValue("PROJ_BRANDPACK_ID")) And Not IsDBNull(Me.GridEX1.GetValue("PROJ_BRANDPACK_ID")) Then
                Me.pnlAgreementDiscount.Enabled = False
                Me.pnlMarketingDiscount.Enabled = False
                Me.isPOProject = True
            Else
                Me.pnlAgreementDiscount.Enabled = True
                Me.pnlAgreementDiscount.Enabled = True
                Me.isPOProject = False
            End If
            Me.QData = QueryData.NavigationPanel
            Dim BRANDPACK_ID As String = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
            Me.GridEX3.SetDataBinding(Nothing, "")
            Select Case Me.NavigationPane1.CheckedButton.Name
                Case "btnAgreement"
                    Me.BindGridRemainding(Nothing) : Me.GridEX3.Visible = True : Me.SDiscount = SelectedDiscount.AgreementDiscount
                    If Not Me.isPOProject Then
                        Me.CheckAvailabilityDisocunt(Me.DistributorID, BRANDPACK_ID, DateTime.Parse(Me.txTPOREFDATE.Text), SDiscount)
                        If Me.rdbGivenDiscount.Checked = True Then
                            Me.rdbGivenDiscount_CheckedChanged(Me.rdbGivenDiscount, New System.EventArgs())
                        ElseIf Me.rdbPeriodDiscount1.Checked = True Then
                            Me.rdbPeriodDiscount1_CheckedChanged(Me.rdbPeriodDiscount1, New System.EventArgs())
                            Me.btnGenerateAgreement.Enabled = False
                        ElseIf Me.rdbPeriodDiscount2.Checked = True Then
                            Me.rdbPeriodDiscount2_CheckedChanged(Me.rdbPeriodDiscount2, New System.EventArgs())
                            Me.btnGenerateAgreement.Enabled = False
                        ElseIf Me.rdbPeriodDiscount3.Checked = True Then
                            Me.rdbPeriodDiscount3_CheckedChanged(Me.rdbPeriodDiscount3, New System.EventArgs())
                            Me.btnGenerateAgreement.Enabled = False
                        ElseIf Me.rdbPeriodDiscount4.Checked = True Then
                            Me.rdbPeriodDiscount4_CheckedChanged(Me.rdbPeriodDiscount4, New System.EventArgs())
                            Me.btnGenerateAgreement.Enabled = False
                        ElseIf Me.rdbSemesterly1Discount.Checked Then
                            Me.rdbSemesterly1Discount_CheckedChanged(Me.rdbSemesterly1Discount, New EventArgs())
                            Me.btnGenerateAgreement.Enabled = False
                        ElseIf Me.rdbSemesterly2Discount.Checked Then
                            Me.rdbSemesterly2Discount_CheckedChanged(Me.rdbSemesterly2Discount, New EventArgs())
                            Me.btnGenerateAgreement.Enabled = False
                        ElseIf Me.rdbYearlyDiscount.Checked = True Then
                            Me.rdbYearlyDiscount_CheckedChanged(Me.rdbYearlyDiscount, New System.EventArgs())
                            Me.btnGenerateAgreement.Enabled = False
                        End If
                        If Me.clsOADiscount.HasGeneratedDiscountAgreement(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False, False, True, Nothing, Nothing, True, False) = True Then
                            Me.btnGenerateAgreement.Enabled = False
                        Else
                            Me.btnGenerateAgreement.Enabled = True
                        End If
                    Else
                        Me.setUnabledRadioButton()
                    End If
                Case "btnMarketing"
                    Me.BindGridRemainding(Nothing) : Me.GridEX3.Visible = True : Me.SDiscount = SelectedDiscount.MarketingDiscount
                    If Not Me.isPOProject Then
                        Me.CheckAvailabilityDisocunt(Me.DistributorID, BRANDPACK_ID, DateTime.Parse(Me.txTPOREFDATE.Text), SDiscount)
                        If Me.rdbGivenDiscountMarketing.Checked = True Then
                            Me.rdbGivenDiscountMarketing_CheckedChanged(Me.rdbGivenDiscountMarketing, New System.EventArgs())
                        ElseIf Me.rdbGivenDK.Checked = True Then
                            Me.rdbGivenDK_Click(Me.rdbGivenDK, New EventArgs())
                        ElseIf Me.rdbGivenPKPP.Checked = True Then
                            Me.rdbGivenPKPP_Click(Me.rdbGivenPKPP, New EventArgs())
                        ElseIf Me.rdbGivenCPR.Checked = True Then
                            Me.rdbGivenCPR_Click(Me.rdbGivenCPR, New EventArgs())
                            'ElseIf Me.rdbTSDiscountMarketing.Checked = True Then
                            '    Me.rdbTSDiscountMarketing_CheckedChanged(Me.rdbTSDiscountMarketing, New System.EventArgs())
                        ElseIf Me.rdbGivenCP.Checked = True Then
                            Me.rdbGivenCPChecked()
                        ElseIf Me.rdbSpecialCPD.Checked = True Then
                            Me.rdbSpecialCPDChecked()
                        ElseIf Me.rdbGivenCP_TM.Checked Then
                            Me.rdbGivenCP_TMChecked()
                        ElseIf Me.rdbSpecialCPD_TM.Checked Then
                            Me.rdbSpeacialCPDTMCheked()
                        ElseIf Me.rdbCPMRT_Dist.Checked Then
                            Me.rdbCPMRT_Dist_Checked()
                        ElseIf Me.rdbCPMRT_TMDist.Checked Then
                            Me.rdbCPMRT_TMDist_Checked()
                        ElseIf Me.rdbDKN.Checked Then
                            Me.rdbDKN_Click(Me.rdbDKN, New EventArgs())
                        ElseIf Me.rdbCPDAuto.Checked Then
                            Me.rdbCPDAuto_Click(Me.rdbCPDAuto, New EventArgs())
                        End If
                    Else
                        Me.setUnabledRadioButton()
                    End If

                Case "btnProject"
                    Me.BindGridRemainding(Nothing) : Me.GridEX3.Visible = True
                    Me.SDiscount = SelectedDiscount.ProjectDiscount
                    If Me.GridEX1.RecordCount <= 0 Then
                        Return
                    End If
                    If Me.GridEX1.SelectedItems.Count = 0 Then
                        Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                                           & vbCrLf & "And DISTRIBUTOR'S PO")
                        Me.BindGridEx3(Nothing)
                        Return
                    End If
                    If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                        Me.ShowMessageInfo("Please select BrandPack Name from InsertedOABrandPack" & vbCrLf & "System will Show all Left quantity BrandPack" & vbCrLf & "Based on BrandPack Name you select" _
                        & vbCrLf & "And DISTRIBUTOR'S PO")
                        Me.BindGridEx3(Nothing)
                        Return
                    End If
                    If Me.mcbRefNo.Value Is Nothing Then
                        Me.baseTooltip.Show("OA_REF_NO is Null", Me.mcbRefNo, 2000)
                        Return
                    End If
                    Me.CreateViewProject(Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.clsOADiscount.DISTRIBUTOR_ID)
                Case "btnOtherDiscount"
                    Me.BindGridRemainding(Nothing)
                    'Me.GridEX3.Dock = DockStyle.Fill
                    Me.GridEX3.Visible = False
                    Me.SDiscount = SelectedDiscount.OtherDiscount
                Case "btnRemainding"
                    Me.SSB = StateStyleButtonBar.FromSelected
                    'Me.GridEX3.Dock = DockStyle.Top
                    Me.grdRemainding.Visible = True
                    Me.SDiscount = SelectedDiscount.None
                    'Dim BRANDPACK_ID As String = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
                    Me.clsOADiscount.CreateViewRemaindingLeft(Me.mcbRefNo.Value.ToString(), Me.DistributorID, BRANDPACK_ID, True)
                    Me.BindGridRemainding(Me.clsOADiscount.ViewLeftRemainding())
                    'Me.GridEX3.Visible = False
                    'Me.btnbrRemainding_GroupClick(Me.btnbrRemainding, New Janus.Windows.ButtonBar.GroupEventArgs(Me.btnbrRemainding.SelectedGroup)) ' New EventArgs())

            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_NavigationPane1_ItemClick")
        Finally
            If Me.SSB = StateStyleButtonBar.FromSelected Then
                Me.SSB = StateStyleButtonBar.FromClick
            End If
            Me.ReadAcces() : Me.Cursor = Cursors.Default
            Me.QData = QueryData.ItemRadioButton
        End Try
    End Sub

#End Region

#Region " Multicolumn Combo "

    Private Sub mcbBrandPack_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbBrandPack.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFM = StateFillingMCB.Filling Then Return
            If Not Me.SI_MCB = SelectedItemMCB.FromMCB Then
                Me.Mode = ModeSave.Update
                Return
                'Return
            Else
                Me.Mode = ModeSave.Save
            End If
            If Me.mcbBrandPack.SelectedIndex = -1 Then
                Me.ClearControl(Me.GrpBrandPack)
                Me.cmbKiloLitre.SelectedIndex = -1
                Return
            End If
            Me.InflateDataFromMCB(Me.mcbBrandPack)
            If Me.ButtonEntry1.btnInsert.Enabled = False Then
                Me.ButtonEntry1.btnInsert.Enabled = True
            End If

        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            'Me.SI_MCB = SelectedItemMCB.FromGrid
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mcbRefNo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbRefNo.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFM = StateFillingMCB.Filling Then Return
            If Me.Mode = ModeSave.Save Then
                If Me.mcbRefNo.SelectedIndex = -1 Then
                    Me.SFG = StateFillingGrid.Filling
                    Me.BindGridEx(Me.GridEX2, Nothing) : Me.BindGridEx(Me.GridEX1, Nothing)
                    Me.BindGridEx3(Nothing)
                    Me.grdRemainding.SetDataBinding(Nothing, "")
                    Me.pnlGridDiscount.TitleText = ""
                    Me.pnlOADiscount.TitleText = ""
                    Me.BindMulticolumnCombo(Me.mcbBrandPack, Nothing, True)
                    Me.ClearControl(Me.GrpBrandPack)
                    Me.ClearControl(Me.grpEdit)
                    Me.txtRemark.Text = ""
                    Me.SFG = StateFillingGrid.HasFilled
                    Return
                End If
            End If
            If Me.mcbRefNo.SelectedItem Is Nothing Then
                Me.SFG = StateFillingGrid.Filling
                Me.BindGridEx(Me.GridEX2, Nothing) : Me.BindGridEx(Me.GridEX1, Nothing)
                Me.BindGridEx3(Nothing)
                Me.grdRemainding.SetDataBinding(Nothing, "")
                Me.pnlGridDiscount.TitleText = ""
                Me.pnlOADiscount.TitleText = ""
                Me.BindMulticolumnCombo(Me.mcbBrandPack, Nothing, True)
                Me.ClearControl(Me.GrpBrandPack)
                Me.ClearControl(Me.grpEdit)
                Me.txtRemark.Text = ""
                Me.SFG = StateFillingGrid.HasFilled
                Return
            End If
            Me.ClearControl(Me.grpEdit) : Me.ClearControl(Me.GrpBrandPack)
            Me.ClearDataDiscount()
            Me.txtRemark.Text = ""
            If Me.SI_MCB = SelectedItemMCB.FromMCB Then
                If Me.mcbRefNo.SelectedIndex <> -1 Then
                    Me.OA_REF_NO = Me.mcbRefNo.Value.ToString()
                    Me.SFG = StateFillingGrid.Filling
                    Me.clsOADiscount.CreateViewOABRANDPACK(Me.mcbRefNo.Value.ToString())
                    Me.BindGridEx(Me.GridEX1, Me.clsOADiscount.ViewOABRANDPACK())
                    Me.BindGridEx(Me.GridEX2, Nothing)
                    Me.BindGridRemainding(Nothing)
                    Me.BindGridEx3(Nothing)
                    Me.SFG = StateFillingGrid.HasFilled
                    Me.NavigationPane1.Enabled = False
                    Me.btnNewGiven.Enabled = True
                    Me.ButtonEntry1.btnInsert.Text = "&Insert"
                End If
            End If
            Me.InflateDataFromMCB(mcbRefNo)
            'BIND A NEW DATA DOCUMENT TO DATAGRID 1
            Me.BindGridEx(Me.GridEX3, Nothing)
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRefixUnmatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefixUnmatched.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.ShowMessageInfo("Please Define Brandpack_Name ")
                Return
            End If
            Me.clsOADiscount.FixUnmatchedDiscount(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString())
            Me.ShowMessageInfo("Please refresh and reselect OA_Ref_No & BrandPack_Name")
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_btnRefixUnmatched_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " Data Grid "

#Region " Grid Ex "

#Region " GridEx1 "

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.HasLoad = False Then : Return : End If
            If (Me.SFG = StateFillingGrid.Filling) Or (Me.SFG = StateFillingGrid.Disposing) _
            Or (Me.SFG = StateFillingGrid.Disposed) Then : Return : End If
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.SFM = StateFillingMCB.Filling
                Me.mcbRefNo.Value = Nothing
                Me.NavigationPane1.Enabled = False
                Me.BindGridEx(Me.GridEX2, Nothing)
                Me.BindGridEx3(Nothing)
                Me.grdRemainding.SetDataBinding(Nothing, "")
                Me.pnlGridDiscount.TitleText = ""
                Me.mcbBrandPack.Value = Nothing
                Me.txtQuantity.Value = 0
                Me.txtPrice.Value = 0
                Me.txtTotal.Value = 0
                Me.txtRemark.Text = ""
                Me.cmbKiloLitre.SelectedIndex = -1
                Me.ClearControl(Me.grpEdit)
                Me.Mode = ModeSave.Save
                Me.ButtonEntry1.btnInsert.Text = "&Insert"
                Me.pnlGridDiscount.TitleText = ""
                Me.pnlTotalRemainder.Text = ""
                Me.btnNew.Text = "Ne&w OA"
                Me.Devided_Qty = 0
                Me.Devide_Factor = 0
                Me.Unit = ""
                Return
            End If
            Me.NavigationPane1.Enabled = True
            If Not IsNothing(Me.GridEX1.GetValue("PROJ_BRANDPACK_ID")) And Not IsDBNull(Me.GridEX1.GetValue("PROJ_BRANDPACK_ID")) Then
                Me.pnlAgreementDiscount.Enabled = False
                Me.pnlMarketingDiscount.Enabled = False
                Me.isPOProject = True
            Else
                Me.pnlAgreementDiscount.Enabled = True
                Me.pnlAgreementDiscount.Enabled = True
                Me.isPOProject = False
            End If
            'inflate data

            Me.ButtonEntry1.btnInsert.Text = "&Update"
            Me.ButtonEntry1.btnInsert.Refresh()
            Me.SI_MCB = SelectedItemMCB.FromGrid
            Me.InflateData()
            Me.btnNew.Text = "&Update OA"
            Me.Mode = ModeSave.Update
            If Me.clsOADiscount.HasReferencedOABrandPack(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False) = True Then
                Me.cmbKiloLitre.ReadOnly = True
            Else
                Me.txtQuantity.ReadOnly = False
                Me.cmbKiloLitre.ReadOnly = False
            End If
            Me.mcbBrandPack.ReadOnly = True
            'SET OA REF NO VARIABLE
            Me.OA_REF_NO = Me.GridEX1.GetValue("OA_REF_NO").ToString()
            Me.PRICE_PRQTY = Convert.ToDecimal(Me.GridEX1.GetValue("OA_PRICE_PERQTY"))
            'check qsy flag dari data grid ex

            Me.clsOADiscount.CreateViewOABrandPackDiscount(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False)
            Me.BindGridEx(Me.GridEX2, Me.clsOADiscount.ViewOADiscount)
            If Me.DistributorID = "" Then
                Me.clsOADiscount.PO_From(Me.OA_REF_NO, False)
                Me.DistributorID = Me.clsOADiscount.DISTRIBUTOR_ID
                Me.DistribtorName = Me.clsOADiscount.DISTRIBUTOR_NAME
            End If
            Dim BRANDPACK_ID As String = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
            Me.rdbTSDiscountMarketing.Visible = False
            Me.rdbSteppingDiscount.Visible = False
            ''check tab panel mana yang aktif
            Select Case Me.NavigationPane1.SelectedPanel.Name
                Case "pnlMarketing"
                    Me.SDiscount = SelectedDiscount.MarketingDiscount
                Case "pnlAgreement"
                    Me.SDiscount = SelectedDiscount.AgreementDiscount
                Case "pnlProject"
                    Me.SDiscount = SelectedDiscount.ProjectDiscount
                Case "pnlOtherDiscount"
                    Me.SDiscount = SelectedDiscount.OtherDiscount
                Case "NavigationPanePanel2"
                    Me.SDiscount = SelectedDiscount.None
            End Select
            ''Me.CheckAvailabilityDisocunt(Me.DistributorID, BRANDPACK_ID, DateTime.Parse(Me.txTPOREFDATE.Text), Me.mcbRefNo.Value.ToString())
            If Not Me.isPOProject Then
                Me.CheckAvailabilityDisocunt(Me.DistributorID, BRANDPACK_ID, DateTime.Parse(Me.txTPOREFDATE.Text), SDiscount)
                If (SDiscount <> SelectedDiscount.AgreementDiscount And SDiscount <> SelectedDiscount.MarketingDiscount And Me.SDiscount <> SelectedDiscount.OtherDiscount) Then
                    Me.Devided_Qty = Me.clsOADiscount.GetDevided_QTY(BRANDPACK_ID, False)
                    Me.Devide_Factor = Me.clsOADiscount.GetDivide_Factor(BRANDPACK_ID, False)
                    Me.Unit = Me.clsOADiscount.GetUnit(BRANDPACK_ID, False)
                Else
                    Me.Devided_Qty = Me.clsOADiscount.Devided_Qty
                    Me.Devide_Factor = Me.clsOADiscount.Devide_Factor
                    Me.Unit = Me.clsOADiscount.UNIT
                End If
            Else
                Me.Devided_Qty = Me.clsOADiscount.GetDevided_QTY(BRANDPACK_ID, False)
                Me.Devide_Factor = Me.clsOADiscount.GetDivide_Factor(BRANDPACK_ID, False)
                Me.Unit = Me.clsOADiscount.GetUnit(BRANDPACK_ID, False)
            End If
            Me.QData = QueryData.GridSelected
            Select Case Me.SDiscount
                Case SelectedDiscount.AgreementDiscount
                    'retrieve data if exists
                    Me.BindGridRemainding(Nothing)
                    If Me.rdbGivenDiscount.Checked = True Then
                        Me.rdbGivenDiscount_CheckedChanged(Me.rdbGivenDiscount, New System.EventArgs())
                    ElseIf Me.rdbPeriodDiscount1.Checked = True Then
                        Me.rdbPeriodDiscount1_CheckedChanged(Me.rdbPeriodDiscount1, New System.EventArgs())
                        Me.btnGenerateAgreement.Enabled = False
                    ElseIf Me.rdbPeriodDiscount2.Checked = True Then
                        Me.rdbPeriodDiscount2_CheckedChanged(Me.rdbPeriodDiscount2, New System.EventArgs())
                        Me.btnGenerateAgreement.Enabled = False
                    ElseIf Me.rdbPeriodDiscount3.Checked = True Then
                        Me.rdbPeriodDiscount3_CheckedChanged(Me.rdbPeriodDiscount3, New System.EventArgs())
                        Me.btnGenerateAgreement.Enabled = False
                    ElseIf Me.rdbPeriodDiscount4.Checked = True Then
                        Me.rdbPeriodDiscount4_CheckedChanged(Me.rdbPeriodDiscount4, New System.EventArgs())
                        Me.btnGenerateAgreement.Enabled = False
                    ElseIf Me.rdbSemesterly1Discount.Checked Then
                        Me.rdbSemesterly1Discount_CheckedChanged(Me.rdbSemesterly1Discount, New EventArgs())
                        Me.btnGenerateAgreement.Enabled = False
                    ElseIf Me.rdbSemesterly2Discount.Checked Then
                        Me.rdbSemesterly2Discount_CheckedChanged(Me.rdbSemesterly2Discount, New EventArgs())
                        Me.btnGenerateAgreement.Enabled = False
                    ElseIf Me.rdbYearlyDiscount.Checked = True Then
                        Me.rdbYearlyDiscount_CheckedChanged(Me.rdbYearlyDiscount, New EventArgs())
                        'Me.CreateViewYearlyDiscount(Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.DistributorID)
                        Me.btnGenerateAgreement.Enabled = False
                    Else
                        Me.BindGridEx3(Nothing)
                        Me.btnGenerateAgreement.Enabled = False
                    End If
                Case SelectedDiscount.MarketingDiscount
                    Me.BindGridRemainding(Nothing)
                    If Me.rdbGivenDiscountMarketing.Checked = True Then
                        Me.rdbGivenDiscountMarketing_CheckedChanged(Me.rdbGivenDiscountMarketing, New System.EventArgs())
                        'ElseIf Me.rdbTSDiscountMarketing.Checked = True Then
                        '    Me.rdbTSDiscountMarketing_CheckedChanged(Me.rdbTSDiscountMarketing, New System.EventArgs())
                    ElseIf Me.rdbGivenCP.Checked = True Then
                        Me.rdbGivenCPChecked()
                    ElseIf Me.rdbGivenDK.Checked = True Then
                        Me.rdbGivenDK_Click(Me.rdbGivenDK, New EventArgs())
                    ElseIf Me.rdbGivenPKPP.Checked = True Then
                        Me.rdbGivenPKPP_Click(Me.rdbGivenPKPP, New EventArgs())
                    ElseIf Me.rdbGivenCPR.Checked = True Then
                        Me.rdbGivenCPR_Click(Me.rdbGivenCPR, New EventArgs())
                        'ElseIf (Me.rdbSteppingDiscount.Checked = True) And (Me.rdbSteppingDiscount.Visible = True) Then
                        '    Me.rdbSteppingDiscount_CheckedChanged(Me.rdbSteppingDiscount, New EventArgs())
                    ElseIf Me.rdbSpecialCPD.Checked Then
                        Me.rdbSpecialCPDChecked()
                    ElseIf Me.rdbSpecialCPD_TM.Checked Then
                        'Me.rdbSpecialCPD_TM_CheckedChanged(Me.rdbSpecialCPD_TM, New EventArgs())
                        Me.rdbSpeacialCPDTMCheked()
                    ElseIf Me.rdbGivenCP_TM.Checked Then
                        'Me.rdbGivenCP_TM_CheckedChanged(Me.rdbGivenCP_TM, New EventArgs())
                        Me.rdbGivenCP_TMChecked()
                    ElseIf Me.rdbCPMRT_Dist.Checked Then
                        Me.rdbCPMRT_Dist_Checked()
                    ElseIf Me.rdbCPMRT_TMDist.Checked Then
                        Me.rdbCPMRT_TMDist_Checked()
                    ElseIf Me.rdbDKN.Checked Then
                        Me.rdbDKN_Click(Me.rdbDKN, New EventArgs())
                    ElseIf Me.rdbCPDAuto.Checked Then
                        Me.rdbCPDAuto_Click(Me.rdbCPDAuto, New EventArgs())
                    Else
                        Me.BindGridEx3(Nothing)
                    End If
                Case SelectedDiscount.OtherDiscount
                    'SHOW MODAL DIALOG FORM
                    'me.btn
                    Me.BindGridRemainding(Nothing)
                Case SelectedDiscount.ProjectDiscount
                    Me.BindGridRemainding(Nothing)
                    Me.clsOADiscount.CreateViewProjectDiscount(Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.DistributorID, _
                    NufarmBussinesRules.OrderAcceptance.OADiscount.TypeQuantity.LeftQuantity)
                    Me.BindGridEx3(Me.clsOADiscount.ViewProjectDiscount())
                    'getrow if project discount has been generated
                Case SelectedDiscount.None
                    Me.GridEX3.Visible = False
                    Me.GridEX3.DataSource = Nothing
                    Me.GridEX3.Update()
                    Me.grdRemainding.Visible = True
                    Me.clsOADiscount.CreateViewRemaindingLeft(Me.GridEX1.GetValue("OA_REF_NO").ToString(), Me.DistributorID, BRANDPACK_ID, False)
                    Me.BindGridRemainding(Me.clsOADiscount.ViewLeftRemainding())
            End Select
            Me.pnlGridDiscount.TitleText = "DISTRIBUTOR : " & Me.txtPOFrom.Text & ", BRANDPACK_NAME : " & Me.GridEX1.GetValue("BRANDPACK_NAME").ToString()
            Dim UnitOnOrder As String = ""
            If Not IsDBNull(Me.GridEX1.GetValue("UNIT_ORDER")) And Not IsNothing(Me.GridEX1.GetValue("UNIT_ORDER")) Then
                UnitOnOrder = Me.GridEX1.GetValue("UNIT_ORDER").ToString()
            End If
            Dim OrgDecimalValue As Decimal = Me.clsOADiscount.getTotalQTY(Me.GridEX1.GetValue("OA_BRANDPACK_ID"), True)
            Dim LeftQty As Decimal = OrgDecimalValue Mod Devided_Qty
            Me.pnlTotalRemainder.Text = "Still Remainder =  " & String.Format("{0:#,##0.000}", LeftQty * Devide_Factor) & "  " & Unit & ", = " & LeftQty.ToString() & "  " & UnitOnOrder
            Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID")
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_CurrentCellChanged")
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
        Finally
            Me.ReadAcces()
            Me.Cursor = Cursors.Default
            Me.SI_MCB = SelectedItemMCB.FromMCB
            If Me.SFM = StateFillingMCB.Filling Then
                Me.SFM = StateFillingMCB.HasFilled
            End If
            Me.QData = QueryData.ItemRadioButton
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                If Me.clsOADiscount.HasReferencedOABrandPack(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), True) = True Then
                    e.Cancel = True
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    Me.GridEX1.Refetch()
                    Me.GridEX1.SelectCurrentCellText()
                ElseIf Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True
                    Me.GridEX1.Refetch()
                    Me.GridEX1.SelectCurrentCellText()
                Else
                    Me.clsOADiscount.DeleteOA_BRANDPACK(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString())
                    e.Cancel = False
                    Me.SFM = StateFillingMCB.Filling
                    Me.GridEX1.UpdateData()
                    CType(Me.GridEX1.DataSource, DataView).Table.AcceptChanges()
                    Me.mcbRefNo.Text = ""
                    Me.mcbBrandPack.Text = ""
                    Me.txtPrice.Value = 0
                    Me.txtQuantity.Value = 0
                    Me.txtTotal.Value = 0
                End If

            End If
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        Finally
            Me.GridEX1.ExpandGroups()
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridEX1.MouseDoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not CMAin.IsSystemAdministrator Then
                If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OA_BranPack = True Then
                    Return
                End If
            End If
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim OA_BRANDPACK_ID As String = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                If Me.clsOADiscount.HasReferencedOABrandPack(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), True) = True Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    Return
                ElseIf Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                    Return
                Else
                    Me.clsOADiscount.DeleteOA_BRANDPACK(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString())
                    Me.mcbRefNo_ValueChanged(Me.mcbRefNo, New EventArgs())
                End If

            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_MouseDoubleClick")
        Finally
            Me.GridEX1.ExpandGroups()
            Me.SFM = StateFillingMCB.HasFilled
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " GridEx2 "

    Private Sub GridEX2_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX2.CurrentCellChanged
        Try
            If Me.HasLoad = False Then
                Return
            End If
            If Me.SFG = StateFillingGrid.Filling Then
                Return
            End If
            If Me.GridEX2.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.baseTooltip.ToolTipTitle = "Discount Description"
                Me.baseTooltip.BackColor = Color.FromArgb(194, 217, 247)
                Me.baseTooltip.Show(Me.GridEX2.GetValue("DISC_DESCRIPTION").ToString(), Me.GridEX2, 2000)
            End If
        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub GridEX2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridEX2.MouseDown
        Try
            If Not Me.GridEX2.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Me.GridEX3.DoDragDrop(Me.GridEX2.GetValue(0), DragDropEffects.Copy)
                Me.DDF = DataDragFrom.GridEx2
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridEX2_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridEX2.MouseDoubleClick
        Try
            If Not CMAin.IsSystemAdministrator Then
                If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OA_BranPack = True Then
                    Return
                End If
            End If
            Dim OA_BRANDPACK_DISC_QTY As Decimal = Convert.ToDecimal(Me.GridEX2.GetValue("DISC_QUANTITY"))
            If OA_BRANDPACK_DISC_QTY > 0 Then
                If Me.clsOADiscount.hasExistedInSPPBBrandPack(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False) Then
                    Return
                End If
            End If
            If Me.GridEX2.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If Not IsNothing(Me.EQTY) Then
                    Me.EQTY.Dispose()
                    Me.EQTY = Nothing
                End If
                Me.EQTY = New EditQTY()
                Me.EQTY.OrgDecimalValue = Convert.ToDecimal(Me.GridEX2.GetValue("DISC_QUANTITY"))
                Me.EQTY.txtQTY.Value = Me.EQTY.OrgDecimalValue
                Me.EQTY.txtQTY.SelectAll()
                Me.EQTY.Show(Me.pnlOADiscount)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX2_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX2.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor

            If Me.GridEX1.SelectedItems.Count = 0 Then
                Me.ShowMessageInfo("The request couldn't be completed due to :" & vbCrLf & "BRANDPACK_NAME IN InsertedOABRANDPACK is not selected.")
                e.Cancel = True
                Return
            End If
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ShowMessageInfo("The request couldn't be completed due to :" & vbCrLf & "BRANDPACK_NAME IN InsertedOABRANDPACK is not selected.")
                e.Cancel = True
                Return
            End If

            Dim OA_BRANDPACK_DISC_QTY As Decimal = Convert.ToDecimal(Me.GridEX2.GetValue("DISC_QUANTITY"))
            If OA_BRANDPACK_DISC_QTY > 0 Then
                'CHEK APAKAH sudah di masuk ke logistik
                If Not CMAin.IsSystemAdministrator Then
                    If Me.clsOADiscount.hasExistedInSPPBBrandPack(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), False) Then
                        Me.ShowMessageInfo("Can not delete data when sppb has already been created") : e.Cancel = True : Return
                    End If
                End If
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Return
            End If
            Dim index As Integer = Me.DeleteOA_BrandPack_Disc()
            If index <> -1 Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
            Me.GridEX2.UpdateData()
            Me.NavigationPane1_ItemClick(Me.NavigationPane1, New EventArgs())
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX2_DeletingRecord")
        Finally
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
            If Me.SFM = StateFillingMCB.Filling Then
                Me.SFM = StateFillingMCB.HasFilled
            End If
            ' Me.isInsertingOADiscount = false
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX2_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles GridEX2.DragDrop
        Try
            If Not IsNothing(Me.QTY) Then
                Me.QTY.Close()
                Me.QTY = Nothing
            End If
            Me.QTY = New Quantity
            If Me.DDF = DataDragFrom.GridEx2 Then
                Return
            End If
            Me.DDT = DataDragTo.GridEx2
            'Dim Devided_Qty As Decimal = Me.clsOADiscount.GetDevided_QTY(Me.GridEX1.GetValue("BRANDPACK_ID").ToString())
            If Me.DDF = DataDragFrom.GridEx3 Then
                Dim ResultQTY As Decimal = 0
                Select Case Me.SDiscount
                    Case SelectedDiscount.AgreementDiscount
                        If Me.rdbGivenDiscount.Checked = True Then
                            'bagi langsung dengan Devided_Qty integer kan dan kali dengan Devided_Qty
                            If Convert.ToDecimal(Me.GridEX3.GetValue("AGREE_LEFT_QTY")) >= Devided_Qty Then
                                ResultQTY = (Decimal.Truncate(Convert.ToDecimal(Me.GridEX3.GetValue("AGREE_LEFT_QTY")) / Devided_Qty)) * Devided_Qty
                            End If
                            Me.QTY.NumericEditBox1.Value = ResultQTY 'Convert.ToInt64(Decimal.Truncate((Me.GridEX3.GetValue("AGREE_LEFT_QTY") / Devided_Qty)))
                            Me.QTY.Focus() : Me.QTY.NumericEditBox1.SelectAll()
                        End If
                    Case SelectedDiscount.MarketingDiscount
                        If (Me.rdbGivenDiscountMarketing.Checked = True) Or (Me.rdbGivenCP.Checked = True) Or (Me.rdbGivenDK.Checked = True) Or (Me.rdbGivenPKPP.Checked = True) _
                        Or (Me.rdbGivenCPR.Checked = True) Or (Me.rdbSpecialCPD.Checked) Or (Me.rdbSpecialCPD_TM.Checked = True) Or (Me.rdbGivenCP_TM.Checked = True) _
                         Or (Me.rdbCPMRT_Dist.Checked) Or (Me.rdbCPMRT_TMDist.Checked) Or (Me.rdbDKN.Checked = True) Or (Me.rdbCPDAuto.Checked) Then
                            If Convert.ToDecimal(Me.GridEX3.GetValue("MRKT_LEFT_QTY")) >= Devided_Qty Then
                                ResultQTY = (Decimal.Truncate(Convert.ToDecimal(Me.GridEX3.GetValue("MRKT_LEFT_QTY")) / Devided_Qty)) * Devided_Qty
                            End If
                            Me.QTY.NumericEditBox1.Value = ResultQTY
                            Me.QTY.Focus()
                            Me.QTY.NumericEditBox1.SelectAll()
                        End If
                    Case SelectedDiscount.None 'diproses melalui recalculate

                End Select
                'check data di table Adjustment yang bisa di ambil 
                With Me.QTY
                    If Me.SDiscount = SelectedDiscount.AgreementDiscount Then
                        Dim dt As DataTable = Nothing
                        If Me.rdbPeriodDiscount1.Checked Or Me.rdbPeriodDiscount2.Checked Or Me.rdbPeriodDiscount3.Checked Or Me.rdbPeriodDiscount4.Checked _
                        Or Me.rdbSemesterly1Discount.Checked Or Me.rdbSemesterly2Discount.Checked Or Me.rdbYearlyDiscount.Checked Then
                            Dim BRANDPACK_ID As String = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
                            Dim PODate As Date = Convert.ToDateTime(Me.txTPOREFDATE.Text)
                            dt = Me.clsOADiscount.GetAdjustmentData(BRANDPACK_ID, DistributorID, PODate, True)
                            If dt.Rows.Count > 0 Then
                                If dt.Rows.Count = 1 Then
                                    .txtAdjust1.Value = Convert.ToDecimal(dt.Rows(0)("LEFT_QTY"))
                                    .lblAdjustment1.Text = IIf((dt.Rows(0)("TypeApp").ToString() = "RP"), "Retailer Program", "DPD")
                                Else
                                    .txtAdjust2.Value = Convert.ToDecimal(dt.Rows(1)("LEFT_QTY"))
                                    .lblAdjustment2.Text = IIf((dt.Rows(1)("TypeApp").ToString() = "RP"), "Retailer Program", "DPD")
                                End If
                                .dt = dt
                                .chkSetAdjustment.Checked = True
                            End If
                        End If
                        'If Me.rdbGivenDiscount.Checked Then
                        '    .chkSetAdjustment.Enabled = False
                        '    .txtAdjust1.Enabled = False
                        '    .txtAdjust2.Enabled = False
                        'Else
                        '    If Not IsNothing(dt) Then
                        '        If dt.Rows.Count > 0 Then
                        '            .chkSetAdjustment.Enabled = True
                        '        Else
                        '            .chkSetAdjustment.Enabled = False
                        '        End If
                        '    Else
                        '        .chkSetAdjustment.Enabled = False
                        '    End If
                        'End If
                        'Else
                        '    .chkSetAdjustment.Enabled = False
                    End If
                    .chkSetAdjustment.Enabled = False
                    .txtAdjust1.Enabled = False
                    .txtAdjust2.Enabled = False
                    .Show(Me.pnlOADiscount, True)
                End With
            Else
                Dim OA_BRANDPACK_ID As String = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim price As Decimal = Convert.ToDecimal(Me.GridEX1.GetValue("OA_PRICE_PERQTY"))
                If IsDBNull(price) Then
                    Me.ShowMessageInfo("PRICE for that brandpack is null")
                    Return
                End If
                If IsNothing(Me.LftQTY) Then
                    Me.LftQTY = New LeftQTY()
                End If
                Me.LftQTY.RemaindingQty = Convert.ToDecimal(Me.grdRemainding.GetValue("LEFT_QTY"))
                Me.LftQTY.txtQty.Value = Me.LftQTY.RemaindingQty
                Me.LftQTY.txtQty.Focus()
                Me.LftQTY.txtQty.SelectAll()
                Me.LftQTY.Show(Me.pnlMinusOA)
            End If

        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX2_DragDrop")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX2_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles GridEX2.DragEnter
        Try
            If e.Data.GetDataPresent(DataFormats.Text) Then
                e.Effect = DragDropEffects.Copy
                'Me.DDF = DataDragFrom.GridEx2
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region " GridEx3 "

    Private Sub GridEX3_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX3.DeletingRecord
        Try
            If Me.GridEX3.DataSource Is Nothing Then
                Return
            End If
            If Not Me.GridEX3.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim PrimaryKey As String = Me.GridEX3.GetValue(0).ToString()
            Select Case Me.SDiscount
                Case SelectedDiscount.AgreementDiscount
                    If Me.rdbGivenDiscount.Checked = True Then
                        If Me.clsOADiscount.HasReferencedDataAGREE_DISC_HISTORY(PrimaryKey) = True Then
                            Me.ShowMessageInfo(Me.MessageCantDeleteData)
                            e.Cancel = True
                            'Me.GridEX3.Refetch()
                            Return

                        End If
                        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                            e.Cancel = True
                            Me.GridEX3.Refetch()
                            Return
                        End If
                        Me.clsOADiscount.DeleteAGREE_DISC_HISTORY(PrimaryKey, Me.GridEX3.GetValue("OA_BRANDPACK_ID").ToString(), Me.GridEX3.GetValue("AGREE_DISC_QTY"))
                        e.Cancel = False
                    Else
                        Me.ShowMessageInfo("Generated Discount can not be deleted" & vbCrLf & "Only given Discount can be deleted.")
                        e.Cancel = True
                        Return
                    End If
                Case SelectedDiscount.MarketingDiscount
                    If (Me.rdbGivenDiscountMarketing.Checked = True) Or (Me.rdbGivenCP.Checked) Or _
                       (Me.rdbGivenCPR.Checked = True) Or (Me.rdbGivenDK.Checked = True) Or (Me.rdbGivenPKPP.Checked = True) Or _
                       (Me.rdbSpecialCPD.Checked = True) Or (Me.rdbGivenCP_TM.Checked = True) Or (Me.rdbSpecialCPD_TM.Checked = True) _
                       Or (Me.rdbCPMRT_Dist.Checked = True) Or (Me.rdbCPMRT_TMDist.Checked = True) Or (Me.rdbDKN.Checked) Or (Me.rdbCPDAuto.Checked) Then
                        If Me.clsOADiscount.HasReferencedDataMRKT_DISC_HISTORY(PrimaryKey) = True Then
                            Me.ShowMessageInfo(Me.MessageCantDeleteData)
                            e.Cancel = True
                            'Me.GridEX3.Refetch()
                            Return
                        End If
                        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                            e.Cancel = True
                            'Me.GridEX3.Refetch()
                            Return
                        End If
                        'CHECK APAKAH FLAG ITU CP,TD
                        If (e.Row.Cells("SGT_FLAG").Value.ToString() = "CP(D)_DIST") Or (e.Row.Cells("SGT_FLAG").Value.ToString() = "CP(D)_DIST_TM") Then
                            'CHECK APAKAH DISCOUNT SEBESAR DISCOUNT SEHARUSNYA
                            '' disc_qty seharusnya PROG_BRANDPACK_DIST_ID
                            Dim TargetCP As Decimal = 0, GivenDisc As Decimal = 0, IsTTMDist As Boolean = False, DiscMustQty As Decimal = 0, DiscQty As Decimal = Convert.ToDecimal(e.Row.Cells("MRKT_DISC_QTY").Value)
                            If e.Row.Cells("SGT_FLAG").Value.ToString() = "CP(D)_DIST" Then : IsTTMDist = False : Else : IsTTMDist = True : End If
                            Me.clsOADiscount.getTargetDiscCPD(e.Row.Cells("PROG_BRANDPACK_DIST_ID").Value.ToString(), IsTTMDist, TargetCP, GivenDisc, False)
                            DiscMustQty = TargetCP * GivenDisc
                            'CHEK JIKA >= DISCOUNT SEHARUSNYA
                            If (DiscQty >= DiscMustQty) Then
                                'CHEK APAKAH ADA DISCOUNT CP.TD LAINNYA DENGAN DISCOUNT LEBIH KECIL DARI DISC SEHARUSNYA
                                If Me.clsOADiscount.GetOtherDiscCPD(e.Row.Cells("PROG_BRANDPACK_DIST_ID").Value.ToString(), e.Row.Cells("MRKT_DISC_HIST_ID").Value.ToString(), DiscMustQty, False) = True Then
                                    'JIKA ADA REJECT DELETE DENGAN MESSAGE (DATA TIDAK BOLEH DI DELETE SEBAB SUDAH ADA DISCOUNT YANG LEBIH KECIL)
                                    Me.ShowMessageInfo(Me.MessageCantDeleteData & vbCrLf & "Other CPD already generated with disc value < " & String.Format("{0:#,##0.000}", DiscQty) & vbCrLf & "You should first delete data whose disc value is < " & String.Format("{0:#,##0.000}", DiscQty)) : e.Cancel = True
                                    e.Cancel = True
                                    Return
                                End If
                            End If
                        End If
                        'TAMPILKAN OA_MANA SAJA YANG HARUS DI DELETE
                        'CASE 2 JIKA DISCOUNT LEBIH KECIL DARI DISCOUNT SEHARUSNYA
                        'DELETE SAJA
                        Me.clsOADiscount.DeleteMRKT_DIST_HISTORY(PrimaryKey, Me.GridEX3.GetValue("OA_BRANDPACK_ID").ToString(), Me.GridEX3.GetValue("MRKT_DISC_QTY"), True)
                        e.Cancel = False
                        'UPDATE OA_BRANDPACK PROJ_DISC_QTY BASED ON OA_BRANDPACK_ID
                    End If
                    'Me.ShowMessageInfo("Generated Discount can not be deleted" & vbCrLf & "Only given Discount can be deleted.")
                    'e.Cancel = True
                    'Return
                Case SelectedDiscount.OtherDiscount
                    e.Cancel = True
                    Return
                Case SelectedDiscount.None
                    e.Cancel = True
                    Return
                Case SelectedDiscount.ProjectDiscount
                    Me.ShowMessageInfo("Generated Discount can not be deleted" & vbCrLf & "Only given Discount can be deleted.")
                    e.Cancel = True
                    Return
            End Select
            Me.GridEX3.UpdateData()
            'Me.NavigationPane1_ItemClick(Me.NavigationPane1, New EventArgs())
        Catch ex As Exception
            e.Cancel = True
            'Me.GridEX3.Refetch()
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX3_DeletingRecord")
        Finally
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX3_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles GridEX3.DragEnter
        Try
            If e.Data.GetDataPresent(DataFormats.Text) Then
                e.Effect = DragDropEffects.Copy
                Me.DDF = DataDragFrom.GridEx3
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridEX3_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridEX3.MouseDoubleClick
        Try
            If Me.GridEX3.DataSource Is Nothing Then
                Return
            End If
            If Not Me.GridEX3.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            If Not CMAin.IsSystemAdministrator Then
                If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OA_BranPack = True Then
                    Return
                End If
            End If
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Me.Cursor = Cursors.WaitCursor
                Dim PrimaryKey As String = Me.GridEX3.GetValue(0).ToString()
                'CHECK REFERENSI DATA DI TABLE ANAK
                Select Case Me.SDiscount
                    Case SelectedDiscount.AgreementDiscount
                        If Me.rdbGivenDiscount.Checked = True Then
                            If Me.clsOADiscount.HasReferencedDataAGREE_DISC_HISTORY(PrimaryKey) = True Then
                                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                                Me.GridEX3.Refetch()
                                Return
                            ElseIf Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                                Me.GridEX3.Refetch()
                                Return
                            Else
                                'Dim index As Integer = Me.clsOADiscount.ViewOABRANDPACK().Find(Me.GridEX1.GetValue("OA_BRANDPACK_ID"))
                                Me.clsOADiscount.DeleteAGREE_DISC_HISTORY(PrimaryKey, Me.GridEX3.GetValue("OA_BRANDPACK_ID").ToString(), Me.GridEX3.GetValue("AGREE_DISC_QTY"))
                            End If
                        Else
                            Me.ShowMessageInfo("Generated Discount can not be deleted" & vbCrLf & "Only given Discount can be deleted.")
                            Return
                        End If

                    Case SelectedDiscount.MarketingDiscount
                        If Me.rdbTSDiscountMarketing.Checked Or Me.rdbSteppingDiscount.Checked Then
                            Me.ShowMessageInfo("Generated Discount can not be deleted" & vbCrLf & "Only given Discount can be deleted.")
                            Return
                        End If
                        If (Me.rdbGivenDiscountMarketing.Checked = True) Or (Me.rdbGivenCP.Checked) Or _
                        (Me.rdbGivenCPR.Checked = True) Or (Me.rdbGivenDK.Checked = True) Or (Me.rdbGivenPKPP.Checked = True) Or _
                        (Me.rdbSpecialCPD.Checked = True) Or (Me.rdbGivenCP_TM.Checked = True) Or (Me.rdbSpecialCPD_TM.Checked = True) _
                        Or (Me.rdbCPMRT_Dist.Checked = True) Or (Me.rdbCPMRT_TMDist.Checked = True) Or (Me.rdbDKN.Checked = True) Or (Me.rdbCPDAuto.Checked) Then
                            If Me.clsOADiscount.HasReferencedDataMRKT_DISC_HISTORY(PrimaryKey) = True Then
                                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                                Me.GridEX3.Refetch()
                                Return
                            ElseIf Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                                Me.GridEX3.Refetch()
                                Return
                            Else
                                If (Me.GridEX3.GetValue("SGT_FLAG").ToString() = "CP(D)_DIST") Or (Me.GridEX3.GetValue("SGT_FLAG").ToString() = "CP(D)_DIST_TM") Then
                                    'CHECK APAKAH DISCOUNT SEBESAR DISCOUNT SEHARUSNYA
                                    '' disc_qty seharusnya PROG_BRANDPACK_DIST_ID
                                    Dim TargetCP As Decimal = 0, GivenDisc As Decimal = 0, IsTTMDist As Boolean = False, DiscMustQty As Decimal = 0, DiscQty As Decimal = Convert.ToDecimal(Me.GridEX3.GetValue("MRKT_DISC_QTY"))
                                    If Me.GridEX3.GetValue("SGT_FLAG").ToString() = "CP(D)_DIST" Then : IsTTMDist = False : Else : IsTTMDist = True : End If
                                    Me.clsOADiscount.getTargetDiscCPD(Me.GridEX3.GetValue("PROG_BRANDPACK_DIST_ID").ToString(), IsTTMDist, TargetCP, GivenDisc, False)
                                    DiscMustQty = TargetCP * GivenDisc
                                    'CHEK JIKA >= DISCOUNT SEHARUSNYA
                                    If (DiscQty >= DiscMustQty) Then
                                        'CHEK APAKAH ADA DISCOUNT CP.TD LAINNYA DENGAN DISCOUNT LEBIH KECIL DARI DISC SEHARUSNYA
                                        If Me.clsOADiscount.GetOtherDiscCPD(Me.GridEX3.GetValue("PROG_BRANDPACK_DIST_ID").ToString(), Me.GridEX3.GetValue("MRKT_DISC_HIST_ID").ToString(), DiscMustQty, False) = True Then
                                            'JIKA ADA REJECT DELETE DENGAN MESSAGE (DATA TIDAK BOLEH DI DELETE SEBAB SUDAH ADA DISCOUNT YANG LEBIH KECIL)
                                            Me.ShowMessageInfo(Me.MessageCantDeleteData & vbCrLf & "Other CPD already generated with disc value < " & String.Format("{0:#,##0.000}", DiscQty) & vbCrLf & "You should first delete data whose disc value is < " & String.Format("{0:#,##0.000}", DiscQty))
                                            Return
                                        End If
                                    End If
                                End If
                                'Dim index As Integer = Me.clsOADiscount.ViewOABRANDPACK().Find(Me.GridEX1.GetValue("OA_BRANDPACK_ID"))
                            End If
                            Me.clsOADiscount.DeleteMRKT_DIST_HISTORY(PrimaryKey, Me.GridEX3.GetValue("OA_BRANDPACK_ID").ToString(), Me.GridEX3.GetValue("MRKT_DISC_QTY"), False)
                        End If
                    Case SelectedDiscount.OtherDiscount
                        Return
                    Case SelectedDiscount.None
                        Return
                    Case SelectedDiscount.ProjectDiscount
                        Me.ShowMessageInfo("Generated Discount can not be deleted" & vbCrLf & "Only given Discount can be deleted.")
                        Return
                End Select
                ' Me.isInsertingOADiscount = True
                'Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
                Me.NavigationPane1_ItemClick(Me.NavigationPane1, New EventArgs())
            End If
        Catch ex As Exception
            'e.Cancel = False
            Me.ShowMessageError(ex.Message)
            Me.GridEX3.Refetch()
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX3_DeletingRecord")
        Finally
            ' Me.isInsertingOADiscount = False
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX3_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridEX3.MouseDown
        Try
            Me.Cursor = Cursors.Default
            If Not CMAin.IsSystemAdministrator Then
                If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OA_BranPack = True Then
                    Return
                End If
            End If
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Me.DDF = DataDragFrom.GridEx3
                Me.GridEX3.DoDragDrop(Me.GridEX3.GetValue(0), DragDropEffects.Copy)
            End If
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region " grdRemainding "

    Private Sub grdRemainding_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles grdRemainding.DragDrop
        Try
            Dim colLeftQTY As String = "", Flag As String = ""
            If Me.GridEX1.RecordCount = 0 Then : Return : End If
            Dim IndexRow As Integer = Me.GridEX1.Row
            If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then : Return : End If
            With Me.clsOADiscount.OA_Remainding
                If Me.DDF = DataDragFrom.GridEx3 Then
                    Select Case Me.SDiscount
                        Case SelectedDiscount.AgreementDiscount
                            If Me.rdbGivenDiscount.Checked Then : Flag = "G" : .FLAG = "G"
                            ElseIf Me.rdbPeriodDiscount1.Checked Then : .FLAG = "Q1" : Flag = "Q1"
                            ElseIf Me.rdbPeriodDiscount2.Checked = True Then : .FLAG = "Q2" : Flag = "Q2"
                            ElseIf Me.rdbPeriodDiscount3.Checked Then : .FLAG = "Q3" : Flag = "Q3"
                            ElseIf Me.rdbPeriodDiscount4.Checked Then : .FLAG = "Q4" : Flag = "Q4"
                            ElseIf Me.rdbYearlyDiscount.Checked Then : .FLAG = "Y" : Flag = "Y"
                            ElseIf Me.rdbSemesterly1Discount.Checked Then : .FLAG = "S1" : Flag = "S1"
                            ElseIf Me.rdbSemesterly2Discount.Checked Then : .FLAG = "S2" : Flag = "S2"
                            End If
                            If Not Me.rdbGivenDiscount.Checked Then
                                .AGREE_DISC_HIST_ID = DBNull.Value : colLeftQTY = "LEFT_QTY"
                                If (NufarmBussinesRules.SharedClass.DISC_AGREE_FROM = "PO") Then
                                    .BRND_B_S_ID = Me.GridEX3.GetValue("BRND_B_S_ID")
                                    .ACHIEVMENT_BRANDPACK_ID = DBNull.Value
                                Else
                                    .BRND_B_S_ID = DBNull.Value : .ACHIEVMENT_BRANDPACK_ID = Me.GridEX3.GetValue("ACHIEVEMENT_BRANDPACK_ID")
                                End If
                            Else
                                colLeftQTY = "AGREE_LEFT_QTY" : .AGREE_DISC_HIST_ID = Me.GridEX3.GetValue("AGREE_DISC_HIST_ID")
                                .BRND_B_S_ID = DBNull.Value
                                .ACHIEVMENT_BRANDPACK_ID = DBNull.Value
                            End If
                            .MRKT_DISC_HISt_ID = DBNull.Value : .MRKT_M_S_ID = DBNull.Value
                            .PROJ_DISC_HIST_ID = DBNull.Value
                        Case SelectedDiscount.MarketingDiscount
                            If Me.rdbGivenDiscountMarketing.Checked Then : Flag = "MG" : .FLAG = "MG"
                            ElseIf Me.rdbGivenCP.Checked Then : Flag = "CP" : .FLAG = "CP"
                            ElseIf Me.rdbGivenCPR.Checked Then : Flag = "CR" : .FLAG = "CR"
                            ElseIf Me.rdbGivenDK.Checked Then : Flag = "DK" : .FLAG = "DK"
                            ElseIf Me.rdbGivenPKPP.Checked Then : Flag = "KP" : .FLAG = "KP"
                            ElseIf Me.rdbSpecialCPD_TM.Checked Then : Flag = "TS" : .FLAG = "TS"
                            ElseIf Me.rdbGivenCP_TM.Checked Then : Flag = "TD" : .FLAG = "TD"
                            ElseIf Me.rdbCPMRT_Dist.Checked Then : Flag = "CD" : .FLAG = "CD"
                            ElseIf Me.rdbCPMRT_TMDist.Checked Then : Flag = "CT" : .FLAG = "CT"
                            ElseIf Me.rdbDKN.Checked Then : Flag = "DN" : .FLAG = "DN"
                            ElseIf Me.rdbCPDAuto.Checked Then : Flag = "CA" : .FLAG = "CA"
                            End If
                            colLeftQTY = "MRKT_LEFT_QTY"
                            .BRND_B_S_ID = DBNull.Value : .AGREE_DISC_HIST_ID = DBNull.Value
                            .MRKT_DISC_HISt_ID = Me.GridEX3.GetValue("MRKT_DISC_HIST_ID").ToString()
                            .MRKT_M_S_ID = DBNull.Value : .PROJ_DISC_HIST_ID = DBNull.Value
                            .ACHIEVMENT_BRANDPACK_ID = DBNull.Value
                            .PROJ_DISC_HIST_ID = DBNull.Value
                        Case SelectedDiscount.None
                        Case SelectedDiscount.OtherDiscount
                        Case SelectedDiscount.ProjectDiscount
                    End Select
                    .OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                    Dim Price As Decimal = Convert.ToDecimal(Me.GridEX1.GetValue("OA_PRICE_PERQTY"))
                    If colLeftQTY = "" Then : Return : End If : If Flag = "" Then : Return : End If
                    .RM_OA_ID = DBNull.Value
                    'JIKA BISA  LEBIH MUNCULKAN left_QTY
                    'INSERT langsung ke database
                    Me.Cursor = Cursors.WaitCursor
                    Dim QtyToInsert As Decimal = Convert.ToDecimal(Me.GridEX3.GetValue(colLeftQTY)) Mod Me.Devided_Qty
                    If QtyToInsert <= 0 Then : Return : End If
                    Me.DDT = DataDragTo.GridRemainding
                    'CHECK APAKAH DATA SUDAH DI INSERT / BELUM
                    'JIKA SUDAH UPDATE DATA
                    Select Case Me.SDiscount
                        Case SelectedDiscount.None
                            Select Case Me.btnbrRemainding.SelectedItem.Key
                                Case "Given", "Quarter", "Semester", "Year"
                                    Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                                   QtyToInsert, .FLAG, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), , _
                                   , , , False, .OA_BRANDPACK_ID)
                                Case "GivenSales", "Target", "Stepping", "Given_CP", "Given_CPR", "Given_PKPP", "Given_DK"
                                    Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                                    QtyToInsert, .FLAG, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), , _
                                    , , , False, .OA_BRANDPACK_ID)
                                Case "LeftProjectDetail"
                                    Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.ProjectDiscount, _
                                    QtyToInsert, .FLAG, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), , _
                                    , , , False, .OA_BRANDPACK_ID)
                            End Select
                        Case SelectedDiscount.MarketingDiscount
                            Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                           QtyToInsert, .FLAG, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), , _
                           , , , False, .OA_BRANDPACK_ID)
                        Case SelectedDiscount.AgreementDiscount
                            Me.clsOADiscount.InsertOARemainding(NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                             QtyToInsert, .FLAG, Me.GridEX1.GetValue("BRANDPACK_ID").ToString(), , _
                             , , , False, .OA_BRANDPACK_ID)
                    End Select
                    ' Me.isInsertingOADiscount = false
                    Me.GridEX1.Row = IndexRow
                    Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
                ElseIf Me.DDF = DataDragFrom.GridEx2 Then
                    Me.ShowMessageInfo("Can not insert data from OA discount !" & vbCrLf & "you can only delete data.")
                End If
            End With
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdRemainding_DragDrop")
        Finally
            ' Me.isInsertingOADiscount = false
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub grdRemainding_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdRemainding.MouseDown
        Try
            Me.Cursor = Cursors.Default
            If Not Me.grdRemainding.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Me.DDF = DataDragFrom.GridRemainding
                Me.grdRemainding.DoDragDrop(Me.grdRemainding.GetValue(0).ToString(), DragDropEffects.Copy)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdRemainding_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles grdRemainding.DragEnter
        Try
            If e.Data.GetDataPresent(DataFormats.Text) Then
                e.Effect = DragDropEffects.Copy
                'Me.DDF = DataDragFrom.GridRemainding
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub grdRemainding_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdRemainding.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            'ALGORITMA
            'CHECK APAKAH DATA SUDAH ADA TRANSAKSI NYA / BELUM
            'JIKA DATA DARI OA_ORIGINAL_QTY TRAP BAHWA DATA TIDAK BISA DI DELETE
            'UPDATE TABLE DARI DIMANA DATA TERSEBUT DIDAPAT
            'DELETE DATA

            If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.ShowMessageInfo("Please select Brandpack_Name")
                e.Cancel = True
                Return
            End If
            'Dim QTYRemainding As Decimal = Me.clsOADiscount.GetQTYRemainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString())
            'Dim Devided_Qty As Decimal = Me.clsOADiscount.GetDevided_QTY(Me.GridEX1.GetValue("BRANDPACK_ID").ToString())
            'Dim DerivedQTY As Decimal = Decimal.Round(QTYRemainding / Devided_Qty, 3)
            Me.grdRemainding.GetValue("LEFT_QTY").ToString()
            If Me.clsOADiscount.OARMHasChild(Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString(), Me.grdRemainding.GetValue("OA_RM_ID").ToString()) = True Then
                e.Cancel = True
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes Then
                'Dim GridExDataView As DataView = CType(Me.GridEX1.DataSource, DataView)
                'Dim OA_BRANDPACK_ID As String = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim indexRow As Integer = Me.GridEX1.Row
                Dim indexCol As Integer = Me.GridEX1.Col
                Dim QTY As Decimal = Convert.ToDecimal(Me.grdRemainding.GetValue("LEFT_QTY"))
                Select Case Me.grdRemainding.GetValue("FLAG")
                    Case "G"
                        Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                         NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                         QTY, Me.grdRemainding.GetValue("AGREE_DISC_HIST_ID"), , , , _
                          Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString())
                    Case "Q1", "Q2", "Q3", "Q4", "S1", "S2", "Y"
                        If NufarmBussinesRules.SharedClass.DISC_AGREE_FROM = "PO" Then
                            Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                           NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                          QTY, , , , False, _
                           Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString(), Me.grdRemainding.GetValue("BRND_B_S_ID").ToString())
                        Else
                            Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                                                     NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                                                     QTY, , , , False, _
                                                     Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString(), , Me.grdRemainding.GetValue("ACHIEVEMENT_BRANDPACK_ID").ToString())
                        End If
                    Case "MG", "CP", "CR", "DK", "KP", "CS", "TS", "TD", "CD", "CT", "DN", "CA"
                        Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                        NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                        QTY, , Me.grdRemainding.GetValue("MRKT_DISC_HIST_ID").ToString(), , , _
                        Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString())
                    Case "MS", "MT"
                        Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                        NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                        QTY, , , , False, _
                        Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString(), , Me.grdRemainding.GetValue("MRKT_M_S_ID").ToString())
                    Case "O", "OCBD", "ODD", "ODR", "ODK", "ODP"
                        Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                        NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.OtherDiscount, _
                        QTY, , , , True, _
                        Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString())
                    Case "P"
                        Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                        NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.None, _
                        QTY, , , Me.grdRemainding.GetValue("PROJ_DISC_HIST_ID").ToString(), False, _
                        Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString())
                    Case "RMOA"
                        e.Cancel = True
                        Me.ShowMessageInfo("OA_ORIGINAL_QTY can not be deleted.")
                        Return
                End Select
                e.Cancel = False : Me.grdRemainding.UpdateData()
                Dim DV As DataView = DirectCast(Me.grdRemainding.DataSource, DataView)
                Me.BindGridRemainding(DV) : Me.grdRemainding.ExpandGroups()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdRemainding_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdRemainding_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdRemainding.MouseDoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            'ALGORITMA
            'CHECK APAKAH DATA SUDAH ADA TRANSAKSI NYA / BELUM
            'JIKA DATA DARI OA_ORIGINAL_QTY TRAP BAHWA DATA TIDAK BISA DI DELETE
            'UPDATE TABLE DARI DIMANA DATA TERSEBUT DIDAPAT
            'DELETE DATA
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    Me.ShowMessageInfo("Please select Brandpack_Name")
                    Return
                End If
                Me.grdRemainding.GetValue("LEFT_QTY").ToString()
                If Me.clsOADiscount.OARMHasChild(Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString(), Me.grdRemainding.GetValue("OA_RM_ID").ToString()) = True Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    Return
                End If
                If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes Then
                    Dim indexRow As Integer = Me.GridEX1.Row
                    Dim indexCol As Integer = Me.GridEX1.Col
                    Dim Qty As Decimal = Convert.ToDecimal(Me.grdRemainding.GetValue("LEFT_QTY"))
                    Select Case Me.grdRemainding.GetValue("FLAG")
                        Case "G"
                            Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                             NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                             Qty, Me.grdRemainding.GetValue("AGREE_DISC_HIST_ID"), , , , _
                              Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString())
                        Case "Q1", "Q2", "Q3", "Q4", "S1", "S2", "Y"
                            If NufarmBussinesRules.SharedClass.DISC_AGREE_FROM = "PO" Then
                                Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                               NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                              Qty, , , , False, _
                               Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString(), Me.grdRemainding.GetValue("BRND_B_S_ID").ToString())
                            Else
                                Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                                                         NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.AgreementDiscount, _
                                                         Qty, , , , False, _
                                                         Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString(), , Me.grdRemainding.GetValue("ACHIEVEMENT_BRANDPACK_ID").ToString())
                            End If
                        Case "MG", "CP", "CR", "DK", "KP", "CS", "TS", "TD", "CD", "CT", "DN", "CA"
                            Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                            NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                            Qty, , Me.grdRemainding.GetValue("MRKT_DISC_HIST_ID").ToString(), , , _
                            Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString())
                        Case "MS", "MT"
                            Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                            NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.MarketingDiscount, _
                            Qty, , , , False, _
                            Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString(), , Me.grdRemainding.GetValue("MRKT_M_S_ID").ToString())
                        Case "O", "OCBD", "ODD", "ODR", "ODK", "ODP"
                            Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                            NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.OtherDiscount, _
                            Qty, , , , True, _
                            Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString())
                        Case "P"
                            Me.clsOADiscount.Delete_OA_Remainding(Me.grdRemainding.GetValue("OA_RM_ID").ToString(), _
                            NufarmBussinesRules.OrderAcceptance.OADiscount.SelectedDiscount.None, _
                            Qty, , , Me.grdRemainding.GetValue("PROJ_DISC_HIST_ID").ToString(), False, _
                            Me.grdRemainding.GetValue("OA_BRANDPACK_ID").ToString())
                        Case "RMOA"
                            Me.ShowMessageInfo("OA_ORIGINAL_QTY can not be deleted.")
                            Return
                    End Select
                    Me.NavigationPane1_ItemClick(Me.NavigationPane1, New EventArgs())
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdRemainding_DeletingRecord")
        Finally
            ' Me.isInsertingOADiscount = false
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#End Region

#End Region

#Region " Text Box "
    Private Sub txtQuantity_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQuantity.ValueChanged
        Try
            If Me.txtQuantity.Value Is Nothing Then
                Me.txtTotal.Text = "0"
            ElseIf Me.txtQuantity.Value = 0 Then
                Me.txtTotal.Text = "0"
            Else
                Me.txtTotal.Text = (Val(Me.txtQuantity.Value) * Val(Me.txtPrice.Value)).ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#End Region


    Private Sub rdbDD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDD.CheckedChanged
        If rdbDD.Checked Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                If Me.clsOADiscount.hasGenerateDiscountOthers(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "ODD", MustcloseConnection) = True Then
                    Me.btnGenerateOtherDiscount.Enabled = False
                Else
                    Me.btnGenerateOtherDiscount.Enabled = True
                End If
                Me.rdbDR.Checked = False
                Me.rdbCBD.Checked = False
                Me.rdbUncategorized.Checked = False
                Me.rdbDK.Checked = False
                Me.rdbDP.Checked = False
            Catch ex As Exception
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbDD_CheckedChanged") : Me.ShowMessageInfo(ex.Message)
            End Try
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub rdbDR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDR.CheckedChanged
        If Me.rdbDR.Checked Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                If Me.clsOADiscount.hasGenerateDiscountOthers(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "ODR", MustcloseConnection) = True Then
                    Me.btnGenerateOtherDiscount.Enabled = False
                Else
                    Me.btnGenerateOtherDiscount.Enabled = True
                End If
                Me.rdbDD.Checked = False
                Me.rdbCBD.Checked = False
                Me.rdbUncategorized.Checked = False
                Me.rdbDK.Checked = False
                Me.rdbDP.Checked = False
            Catch ex As Exception
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbDR_CheckedChanged") : Me.ShowMessageInfo(ex.Message)
            End Try
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub rdbCBD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCBD.CheckedChanged
        If Me.rdbCBD.Checked Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                If Me.clsOADiscount.hasGenerateDiscountOthers(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "OCBD", MustcloseConnection) = True Then
                    Me.btnGenerateOtherDiscount.Enabled = False
                Else
                    Me.btnGenerateOtherDiscount.Enabled = True
                End If
                Me.rdbDD.Checked = False
                Me.rdbDR.Checked = False
                Me.rdbUncategorized.Checked = False
                Me.rdbDK.Checked = False
                Me.rdbDP.Checked = False
            Catch ex As Exception
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbCBD_CheckedChanged") : Me.ShowMessageInfo(ex.Message)
            End Try
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub rdbUncategorized_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbUncategorized.CheckedChanged
        If Me.rdbUncategorized.Checked Then
            Me.btnGenerateOtherDiscount.Enabled = True
        End If
    End Sub

    Private Sub rdbDK_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDK.CheckedChanged
        If Me.rdbDK.Checked Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                If Me.clsOADiscount.hasGenerateDiscountOthers(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "ODK", MustcloseConnection) = True Then
                    Me.btnGenerateOtherDiscount.Enabled = False
                Else
                    Me.btnGenerateOtherDiscount.Enabled = True
                End If
                Me.rdbDD.Checked = False
                Me.rdbDR.Checked = False
                Me.rdbUncategorized.Checked = False
                Me.rdbCBD.Checked = False
                Me.rdbDP.Checked = False
            Catch ex As Exception
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbDK_CheckedChanged") : Me.ShowMessageInfo(ex.Message)
            End Try
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub rdbDP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbDP.CheckedChanged
        If Me.rdbDK.Checked Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Me.OA_BRANDPACK_ID = Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString()
                Dim MustcloseConnection As Boolean = False
                If Me.QData = QueryData.GridSelected Or Me.QData = QueryData.NavigationPanel Then : MustcloseConnection = False
                Else : MustcloseConnection = True
                End If
                If Me.clsOADiscount.hasGenerateDiscountOthers(Me.GridEX1.GetValue("OA_BRANDPACK_ID").ToString(), "ODP", MustcloseConnection) = True Then
                    Me.btnGenerateOtherDiscount.Enabled = False
                Else
                    Me.btnGenerateOtherDiscount.Enabled = True
                End If
                Me.rdbDK.Checked = False
                Me.rdbDD.Checked = False
                Me.rdbDR.Checked = False
                Me.rdbUncategorized.Checked = False
                Me.rdbCBD.Checked = False
            Catch ex As Exception
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbDP_CheckedChanged") : Me.ShowMessageInfo(ex.Message)
            End Try
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub GridEX2_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GridEX2.FormattingRow

    End Sub
End Class
