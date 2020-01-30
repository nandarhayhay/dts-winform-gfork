Public Class Distributor_Include

#Region " Deklarasi "
    Private clsMRKT_BRANDPACK As NufarmBussinesRules.Program.BrandPackInclude
    Private HasLoad As Boolean
    Friend Mode As ModeSave
    Friend UM As UpdateMode
    Private SFM As StateFillingMCB
    Private SFG As StateFillingGrid
    Friend PROG_BRANDPACK_DIST_ID As String
    Private AGREE_BRANDPACK_ID As String
    Friend OriginalStartDateFromBrandPack As Date
    Friend OriginalEndDateFromBrandPack As Date
    Friend PROGRAM_ID As String = ""
    Friend BRANDPACK_ID As String = ""
    Friend PROG_BRANDPACK_ID As String = ""
    Friend ParentFrm As Program = Nothing
    Private isHasCheckedRefrence As Boolean = False
    Private DtTM As DataView = Nothing
    Friend IsHasGenerated As Boolean = True
#End Region

#Region " Enum "

    Friend Enum ModeSave
        Save
        Update
    End Enum

    Friend Enum UpdateMode
        FromOriginal
        FromGrid
    End Enum

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

    Friend Sub InitializeData()
        Me.HasLoad = False
        Me.SFG = StateFillingGrid.Filling
        Me.SFM = StateFillingMCB.Filling
        Me.RefreshData()
    End Sub

    
    Private Sub UnabledControl()
        'Me.txtGiven.Enabled = False
        'Me.txtTargetPCT.Enabled = False
        'Me.txtTargetQTY.Enabled = False
        'Me.mcbBrandPack.Enabled = False
        'Me.chkStepping.Enabled = False
        'Me.tbType.Enabled = False
        Me.ChkDistributor.ReadOnly = True
        Me.mcbDistributor.ReadOnly = True
        Me.mcbProgram.Enabled = False
        Me.dtPicStart.ReadOnly = True
        Me.dtPicEnd.ReadOnly = True
        'If (Me.Mode = ModeSave.Update) Then
        '    Me.dtPicEnd.MaxDate = Me.OriginalEndDateFromBrandPack
        'End If

        'If Me.Mode = ModeSave.Update Then
        '    If Me.UM = UpdateMode.FromOriginal Then

        '    End If
        'End If

        'Me.ButtonEntry1.btnInsert.Enabled = False
    End Sub

    Friend Sub EnabledControl()
        'Me.txtGiven.Enabled = True
        'Me.txtTargetPCT.Enabled = True
        'Me.txtTargetQTY.Enabled = True
        'Me.mcbBrandPack.Enabled = True
        'Me.chkStepping.Enabled = True
        'Me.tbType.Enabled = True
        'Me.PanelEx1.Enabled = False
        Me.ChkDistributor.ReadOnly = False
        Me.mcbDistributor.ReadOnly = False
        Me.mcbProgram.Enabled = True

        Me.dtPicStart.ReadOnly = False
        Me.dtPicEnd.ReadOnly = False
        Me.ButtonEntry1.btnInsert.Enabled = True
    End Sub
    Private Sub ClearMultiColumnCombo()
        Me.SFM = StateFillingMCB.Filling
        Me.mcbProgram.Text = ""
        Me.SFM = StateFillingMCB.Filling
        Me.mcbBrandPack.Text = ""
        Me.SFM = StateFillingMCB.Filling
        Me.ChkDistributor.Text = ""
        Me.SFM = StateFillingMCB.Filling
        Me.mcbDistributor.Text = ""
        Me.SFM = StateFillingMCB.Filling
        Me.mcbTM.Text = ""
    End Sub
    
    Private Shadows Sub ClearControl()
        Me.chkCP.Checked = False
        Me.chkDK.Checked = False
        Me.chkHK.Checked = False
        Me.chkRPK.Checked = False
        Me.chkPKPP.Checked = False
        Me.chkCPR.Checked = False
        Me.chkStepping.Checked = False
        Me.txtGiven.Value = 0
        Me.txtGivenCP.Value = 0
        Me.txtGivenDK.Value = 0
        Me.txtGivenPKPP.Value = 0
        Me.txtGivenCPR.Value = 0
        Me.txtGivenCPMRT.Value = 0
        Me.txtPrice.Value = 0
        Me.txtTargetCP.Value = 0
        Me.txtTargetCPMRT.Value = 0
        Me.txtTargetDK.Value = 0
        Me.txtTargetHK.Value = 0
        Me.txtTargetPCT.Value = 0
        Me.txtTargetPKPP.Value = 0
        Me.txtTargetQTY.Value = 0
        Me.txtTargetCPR.Value = 0
        'Me.txtTargetValuePKPP.Value = 0
        Me.txtBonusValuePKPP.Value = 0
        Me.chkStepping.Checked = False
        Me.chkSpesialDiscountCPD.Checked = False
        Me.mcbTMPKPP.Value = Nothing
        Me.mcbTM.Value = Nothing
        Me.mcbTMPKPP.Text = ""
        Me.mcbTM.Text = ""
        Me.mcbTMCPMRT.Text = ""
        Me.mcbTMCPMRT.Text = Nothing
        'Me.chkTargetDistributor.Checked = True
    End Sub

    Private Sub RefreshData()
        If Me.Mode = ModeSave.Update Then
            If Me.UM = UpdateMode.FromOriginal Then
                If Me.HasLoad = False Then
                    Me.clsMRKT_BRANDPACK = New NufarmBussinesRules.Program.BrandPackInclude()
                    Me.Size = New System.Drawing.Size(506, 419)
                    Me.clsMRKT_BRANDPACK.CreateViewProgram(NufarmBussinesRules.common.Helper.SaveMode.Update, False)
                    Me.clsMRKT_BRANDPACK.CreateViewBrandPack(Me.PROGRAM_ID, True, False)
                    Me.clsMRKT_BRANDPACK.CreateViewDistributor(Me.PROG_BRANDPACK_ID, True, False)

                    Me.BindMultiColumnCombo(Me.mcbProgram, Me.clsMRKT_BRANDPACK.ViewProgram(), "", "PROGRAM_ID", "PROGRAM_ID")
                    Me.BindMultiColumnCombo(Me.mcbBrandPack, Me.clsMRKT_BRANDPACK.ViewBrandPack(), "", "BRANDPACK_NAME", "BRANDPACK_ID")
                    Me.BindMultiColumnCombo(Me.mcbDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor, "", "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
                    'Me.BindCheckedCombo(Me.ChkDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor(), "")
                End If
                If Me.clsMRKT_BRANDPACK.HasReferencedDataMRKT_BRANDPCK_DISTRIBUTOR(Me.PROG_BRANDPACK_DIST_ID) = True Then
                    Me.UnabledControl()

                    'Me.ButtonEntry1.btnAddNew.Enabled = False
                    Me.ButtonEntry1.btnInsert.Text = "&Update"
                Else
                    Me.EnabledControl()
                    Me.mcbBrandPack.Enabled = False
                    Me.ChkDistributor.ReadOnly = True
                    Me.mcbProgram.Enabled = False
                    Me.ButtonEntry1.btnAddNew.Enabled = False
                    Me.ButtonEntry1.btnInsert.Text = "&Update"
                End If
            Else
                Me.Size = New System.Drawing.Size(506, 694)
                Me.BindGrid()
                Me.UnabledControl()
                'If Me.SFM = StateFillingMCB.HasFilled Then
                'End If
                Me.SFM = StateFillingMCB.Filling : Me.ClearMultiColumnCombo() : Me.ClearControl()
                Me.SFM = StateFillingMCB.HasFilled
                Me.chkStepping.Checked = False
                Me.ButtonEntry1.btnInsert.Text = "&Insert"
                Me.ButtonEntry1.btnAddNew.Enabled = True
                Me.ButtonEntry1.btnAddNew.Focus()
            End If
            Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
            If IsNothing(Me.mcbTMPKPP.DataSource) OrElse Me.mcbTMPKPP.DropDownList.RecordCount <= 0 Then
                Me.BindMultiColumnCombo(Me.mcbTMPKPP, DV, "", "MANAGER", "SHIP_TO_ID")
            End If
            If IsNothing(Me.mcbTM.DataSource) OrElse Me.mcbTM.DropDownList.RecordCount <= 0 Then
                Me.BindMultiColumnCombo(Me.mcbTM, DV, "", "MANAGER", "SHIP_TO_ID")
            End If
            ''isi dengan data TM
        Else
            If Me.HasLoad = False Then
                Me.clsMRKT_BRANDPACK = New NufarmBussinesRules.Program.BrandPackInclude()
                Me.clsMRKT_BRANDPACK.CreateViewProgram(NufarmBussinesRules.common.Helper.SaveMode.Insert, False)
                'Me.clsMRKT_BRANDPACK.CreateViewBrandPack(Me.PROGRAM_ID, False)
                'Me.clsMRKT_BRANDPACK.CreateViewDistributor()
                Me.clsMRKT_BRANDPACK.CreateViewMRK_BRANDPACK_DISTRIBUTOR()
                Me.BindMultiColumnCombo(Me.mcbProgram, Me.clsMRKT_BRANDPACK.ViewProgram(), "", "PROGRAM_ID", "PROGRAM_ID")
                'Me.BindMultiColumnCombo(Me.mcbBrandPack, Me.clsMRKT_BRANDPACK.ViewBrandPack(), "")
                'Me.BindCheckedCombo(Me.ChkDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor(), "")
                'Me.BindMultiColumnCombo(Me.mcbDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor, "")
                Me.EnabledControl()
            Else
                Me.UnabledControl()
            End If
            Me.BindGrid()
            Me.ButtonEntry1.btnAddNew.Enabled = True
            Me.ButtonEntry1.btnAddNew.Focus()
        End If
    End Sub

    Private Sub BindGrid()
        Me.SFG = StateFillingGrid.Filling
        Me.GridEX1.SetDataBinding(Me.clsMRKT_BRANDPACK.ViewMRK_BRANDPACK_DISTRIBUTOR(), "")
        Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("START_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarDropDown
        Me.GridEX1.RootTable.Columns("END_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarDropDown
        Me.GridEX1.RootTable.Columns("GIVEN_DISC_PCT").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("TARGET_DISC_PCT").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("TARGET_QTY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("STEPPING").FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
        Me.SFG = StateFillingGrid.HasFilled
    End Sub

    Private Sub InflateData()
        Me.mcbProgram.Value = Me.GridEX1.GetValue("PROGRAM_ID")
        Me.clsMRKT_BRANDPACK.CreateViewBrandPack(Me.mcbProgram.Value.ToString(), True, False)
        Me.BindMultiColumnCombo(Me.mcbBrandPack, Me.clsMRKT_BRANDPACK.ViewBrandPack(), "", "BRANDPACK_NAME", "BRANDPACK_ID")
        Me.SFM = StateFillingMCB.Filling
        Me.mcbBrandPack.Value = Me.GridEX1.GetValue("BRANDPACK_ID")
        'Me.ChkDistributor.Values = Me.GridEX1.GetValue("DISTRIBUTOR_ID")
        Me.dtPicStart.Value = Convert.ToDateTime(Me.GridEX1.GetValue("START_DATE"))
        Me.dtPicEnd.Value = Convert.ToDateTime(Me.GridEX1.GetValue("END_DATE"))
        Me.dtPicStart.IsNullDate = False
        Me.dtPicEnd.IsNullDate = False
        Me.clsMRKT_BRANDPACK.CreateViewDistributor(Me.GridEX1.GetValue("PROG_BRANDPACK_ID").ToString(), True, True)
        'Dim StartDateString As String = Me.dtPicStart.Value.Month.ToString() + "/" & Me.dtPicStart.Value.Day.ToString() & "/" & Me.dtPicStart.Value.Year.ToString()
        'Dim EndDateString As String = Me.dtPicEnd.Value.Month.ToString() + "/" & Me.dtPicEnd.Value.Day.ToString() & "/" & Me.dtPicEnd.Value.Year.ToString()
        'Me.clsMRKT_BRANDPACK.CreateViewDistributor(Me.GridEX1.GetValue("PROG_BRANDPACK_ID").ToString(), True) 'Me.mcbProgram.Value.ToString(), Me.mcbBrandPack.Value.ToString(), StartDateString, EndDateString)
        Me.BindMultiColumnCombo(Me.mcbDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor(), "", "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
        'Me.BindCheckedCombo(Me.ChkDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor(), "", "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
        If Me.mcbDistributor.ReadOnly Then
            Me.mcbDistributor.ReadOnly = False
        End If
        Me.SFM = StateFillingMCB.Filling
        Me.mcbDistributor.Value = Me.GridEX1.GetValue("DISTRIBUTOR_ID")
        Me.ChkDistributor.Text = ""
        Me.ChkDistributor.Visible = False
        Me.ChkDistributor.UncheckAll()
        Me.mcbDistributor.Visible = True
        If CBool(Me.GridEX1.GetValue("ISRPK")) = True Then
            Me.chkRPK.Checked = True
            Me.txtGiven.Value = Me.GridEX1.GetValue("GIVEN_DISC_PCT")
            Me.txtTargetQTY.Value = Me.GridEX1.GetValue("TARGET_QTY")
            Me.txtTargetPCT.Value = Me.GridEX1.GetValue("TARGET_DISC_PCT")
            If CBool(Me.GridEX1.GetValue("STEPPING")) = True Then
                Me.chkStepping.Checked = True
            Else
                Me.chkStepping.Checked = False
            End If
        Else
            Me.chkRPK.Checked = False
            Me.txtGiven.Value = 0
            Me.txtTargetPCT.Value = 0
            Me.txtTargetQTY.Value = 0
            If CBool(Me.GridEX1.GetValue("STEPPING")) = True Then
                Me.chkStepping.Checked = True
            Else
                Me.chkStepping.Checked = False
            End If
        End If
        If CBool(Me.GridEX1.GetValue("ISPKPP")) = True Then
            Me.chkPKPP.Checked = True
            Me.txtGivenPKPP.Value = Convert.ToDecimal(Me.GridEX1.GetValue("GIVEN_PKPP"))
            'Me.txtTargetPKPP.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_PKPP"))
            If (Convert.ToDecimal(Me.GridEX1.GetValue("BONUS_VALUE")) > 0) Then
                'nolkan target qty pkpp di tab volume
                Me.txtTargetPKPP.Value = 0 : Me.txtTargetValuePKPP.Value = Me.GridEX1.GetValue("TARGET_PKPP")
            ElseIf (Convert.ToDecimal(Me.GridEX1.GetValue("BONUS_VALUE") <= 0)) Then
                Me.txtTargetValuePKPP.Value = 0 : Me.txtTargetPKPP.Value = Me.GridEX1.GetValue("TARGET_PKPP")
            End If
            Me.txtBonusValuePKPP.Value = Me.GridEX1.GetValue("BONUS_VALUE")
            If Not IsNothing(Me.GridEX1.GetValue("SHIP_TO_ID")) And Not IsDBNull(Me.GridEX1.GetValue("SHIP_TO_ID")) Then
                Me.mcbTMPKPP.Value = Me.GridEX1.GetValue("SHIP_TO_ID")
            Else
                Me.mcbTMPKPP.Value = Nothing
                Me.mcbTMPKPP.Text = ""
            End If
        Else
            Me.chkPKPP.Checked = False
            Me.txtGivenPKPP.Value = 0
            Me.txtTargetPKPP.Value = 0
            Me.txtBonusValuePKPP.Value = 0
            Me.txtTargetValuePKPP.Value = 0
            Me.mcbTMPKPP.Value = Nothing
            Me.mcbTMPKPP.Text = ""
        End If
        If CBool(Me.GridEX1.GetValue("ISDK")) = True Then
            Me.chkDK.Checked = True
            Me.txtGivenDK.Value = Convert.ToDecimal(Me.GridEX1.GetValue("GIVEN_DK"))
            Me.txtTargetDK.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_DK"))
        Else
            Me.chkDK.Checked = False
            Me.txtGivenDK.Value = 0
            Me.txtTargetDK.Value = 0
        End If
        If CBool(Me.GridEX1.GetValue("ISCP")) = True Then
            Me.chkCP.Checked = True
            Me.txtGivenCP.Value = Convert.ToDecimal(Me.GridEX1.GetValue("GIVEN_CP"))
            Me.txtTargetCP.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_CP"))

            Me.SFG = StateFillingGrid.Filling
            Me.chkTargetDistributor.Checked = CBool(Me.GridEX1.GetValue("IS_T_TM_DIST"))
            Me.chkSpesialDiscountCPD.Checked = CBool(Me.GridEX1.GetValue("SCPD"))
            If Not IsNothing(Me.GridEX1.GetValue("SHIP_TO_ID")) And Not IsDBNull(Me.GridEX1.GetValue("SHIP_TO_ID")) Then
                Me.mcbTM.Value = Me.GridEX1.GetValue("SHIP_TO_ID")
            End If
        Else
            Me.chkCP.Checked = False
            Me.txtTargetCP.Value = 0
            Me.txtGivenCP.Value = 0
            Me.chkSpesialDiscountCPD.Checked = False
            Me.mcbTM.Value = Nothing : Me.mcbTM.Text = ""
        End If
        If CBool(Me.GridEX1.GetValue("ISHK")) = True Then
            Me.chkHK.Checked = True
            Me.txtTargetHK.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_HK"))
            Me.txtPrice.Value = Convert.ToDecimal(Me.GridEX1.GetValue("PRICE_HK"))
        Else
            Me.chkHK.Checked = False
            Me.txtTargetHK.Value = 0
            Me.txtPrice.Value = 0
        End If
        If CBool(Me.GridEX1.GetValue("ISCPR")) = True Then
            Me.chkCPR.Checked = True
            Me.txtGivenCPR.Value = Convert.ToDecimal(Me.GridEX1.GetValue("GIVEN_CPR"))
            Me.txtTargetCPR.Value = Convert.ToDecimal(Me.GridEX1.GetValue("TARGET_CPR"))
        Else
            Me.chkCPR.Checked = False
            Me.txtTargetCPR.Value = 0
            Me.txtGivenCPR.Value = 0
        End If
        If Not IsNothing(Me.GridEX1.GetValue("DESCRIPTIONS")) And Not IsDBNull(Me.GridEX1.GetValue("DESCRIPTIONS")) Then
            Me.txtDescriptions.Text = Me.GridEX1.GetValue("DESCRIPTIONS").ToString()
        Else
            Me.txtDescriptions.Text = String.Empty
        End If

        Me.SFM = StateFillingMCB.HasFilled
    End Sub

    Friend Sub BindMultiColumnCombo(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, _
    ByVal dtview As DataView, ByVal rowfilter As String, ByVal DisplayMember As String, ByVal valueMember As String)
        Me.SFM = StateFillingMCB.Filling
        If dtview Is Nothing Then
            mcb.Value = Nothing : mcb.DataSource = Nothing : Me.SFM = StateFillingMCB.HasFilled : Return
        End If
        dtview.RowFilter = rowfilter
        mcb.DataSource = dtview : mcb.DisplayMember = DisplayMember : mcb.ValueMember = valueMember
        mcb.DropDownList.RetrieveStructure() : mcb.DroppedDown = True
        For Each col As Janus.Windows.GridEX.GridEXColumn In mcb.DropDownList.Columns
            If col.Visible Then : col.AutoSize() : End If
        Next
        mcb.DroppedDown = False : mcb.Value = Nothing : Me.SFM = StateFillingMCB.HasFilled
    End Sub

    Private Sub BindCheckedCombo(ByVal cbx As Janus.Windows.GridEX.EditControls.CheckedComboBox, ByVal dtview As DataView, _
    ByVal rowFilter As String, ByVal DisplayMember As String, ByVal ValueMember As String)
        Me.SFM = StateFillingMCB.Filling
        If IsNothing(dtview) Then
            cbx.Text = "" : cbx.DropDownDataSource = Nothing : Me.SFM = StateFillingMCB.HasFilled : Return
        End If
        dtview.RowFilter = rowFilter : cbx.SetDataBinding(dtview, "")
        cbx.DropDownDisplayMember = DisplayMember : cbx.DropDownValueMember = ValueMember
        'cbx.RetrieveStructure()
        'cbx.DropDownList().Columns("DISTRIBUTOR_ID").ShowRowSelector = True
        ''cbx.DropDownList().Columns("DISTRIBUTOR_ID").UseHeaderSelector = True
        'cbx.DroppedDown = True
        'For Each col As Janus.Windows.GridEX.GridEXColumn In cbx.DropDownList.Columns
        '    If col.Visible Then : col.AutoSize() : End If
        'Next

        cbx.DropDownList.UnCheckAllRecords() : cbx.DroppedDown = False
        Me.SFM = StateFillingMCB.HasFilled
    End Sub
    'procedure  untuk mengenabled dan clearkan type program
    Private Sub SetTypeProgram()
        'DK===================
        If Me.chkDK.Checked = True Then
            Me.txtGivenDK.Enabled = True
            Me.txtTargetDK.Enabled = True
        Else
            Me.txtGivenDK.Enabled = False
            Me.txtTargetDK.Enabled = False
            If Me.Mode = ModeSave.Save Then
                Me.txtGivenDK.Value = 0
                Me.txtTargetDK.Value = 0
            End If
        End If
        If Me.chkHK.Checked = True Then
            Me.txtTargetHK.Enabled = True
            Me.txtPrice.Enabled = True
        Else
            Me.txtTargetHK.Enabled = False
            Me.txtPrice.Enabled = False
            If Me.Mode = ModeSave.Save Then
                Me.txtTargetHK.Value = 0
                Me.txtPrice.Value = 0
            End If

        End If
        If Me.chkPKPP.Checked = True Then
            Me.txtGivenPKPP.Enabled = True
            Me.txtTargetPKPP.Enabled = True
            Me.txtBonusValuePKPP.Enabled = True
            Me.txtTargetValuePKPP.Enabled = True
            Me.mcbTMPKPP.ReadOnly = False

        Else
            Me.txtGivenPKPP.Enabled = False
            Me.txtTargetPKPP.Enabled = False
            Me.txtBonusValuePKPP.Enabled = False
            Me.txtTargetValuePKPP.Enabled = False
            Me.mcbTMPKPP.ReadOnly = True
            If Me.Mode = ModeSave.Save Then
                Me.txtGivenPKPP.Value = 0
                Me.txtTargetPKPP.Value = 0
                Me.txtBonusValuePKPP.Value = 0
                Me.txtTargetValuePKPP.Value = 0
            End If

        End If
        If Me.chkRPK.Checked = True Then
            Me.txtGiven.Enabled = True
            Me.txtTargetQTY.Enabled = True
            Me.txtTargetPCT.Enabled = True
            Me.chkStepping.Enabled = True
        Else
            If Me.Mode = ModeSave.Save Then
                Me.txtGiven.Value = 0
                Me.txtTargetPCT.Value = 0
                Me.txtTargetQTY.Value = 0
            End If
        End If
        If Me.chkCP.Checked = True Then
            Me.txtGivenCP.Enabled = True
            Me.txtTargetCP.Enabled = True
            Me.chkSpesialDiscountCPD.Enabled = True

        Else
            Me.txtGivenCP.Enabled = False
            Me.txtTargetCP.Enabled = False
            If Me.Mode = ModeSave.Save Then
                Me.txtGivenCP.Value = 0
                Me.txtTargetCP.Value = 0
            End If
            Me.chkSpesialDiscountCPD.Enabled = False
        End If
        If Me.chkCPR.Checked = True Then
            Me.txtGivenCPR.Enabled = True
            Me.txtTargetCPR.Enabled = True
        Else
            Me.txtGivenCPR.Enabled = False
            Me.txtTargetCPR.Enabled = False
            If Me.Mode = ModeSave.Save Then
                Me.txtGivenCPR.Value = 0
                Me.txtTargetCPR.Value = 0
            End If
        End If

        If Me.chkCPMRT.Checked Then
            Me.txtTargetCPMRT.Enabled = True
            Me.txtGivenCPMRT.Enabled = True
            Me.chkTargetDistributorCPMRT.Enabled = True
        Else
            Me.txtGivenCPMRT.Enabled = False
            Me.txtTargetCPMRT.Enabled = False
            If Me.Mode = ModeSave.Save Then
                Me.txtGivenCPMRT.Value = 0
                Me.txtTargetCPMRT.Value = 0
            End If

        End If
    End Sub
#End Region

#Region " Function "

    Private Function IsCategorized() As Boolean
        Dim B As Boolean = False
        If Me.chkCP.Checked Then
            B = True
        ElseIf Me.chkCPMRT.Checked Then
            B = True
        ElseIf Me.chkCPR.Checked Then
            B = True
        ElseIf Me.chkDK.Checked Then
            B = True
        ElseIf Me.chkHK.Checked Then
            B = True
        ElseIf Me.chkPKPP.Checked Then
            B = True
        ElseIf Me.chkRPK.Checked Then
            B = True

        End If
        Return B
    End Function

    Private Function IsValidID() As Boolean
        If Me.mcbProgram.Value Is Nothing Then
            Me.baseTooltip.Show("Please Define ProgramName", Me.mcbProgram, 3000)
            Me.mcbProgram.Focus()
            Return False
        ElseIf Me.mcbBrandPack.Value Is Nothing Then
            Me.baseTooltip.Show("Please Define BrandPackName", Me.mcbBrandPack, 3000)
            Me.mcbBrandPack.Focus()
            Return False
        End If
        If Me.mcbDistributor.Visible = True Then
            If Me.mcbDistributor.Value Is Nothing Then
                Me.baseTooltip.Show("Please Define Distributor .", Me.mcbDistributor, 3000)
                'Me.chkCP.Checked = False
                Me.mcbDistributor.Focus()
                Return False
            End If
        ElseIf Me.ChkDistributor.Visible = True Then
            If Not IsNothing(Me.ChkDistributor.DropDownDataSource) Then
                If CType(Me.ChkDistributor.DropDownDataSource, DataView).Count > 0 Then
                    If Me.ChkDistributor.CheckedValues Is Nothing Then
                        Me.baseTooltip.Show("Please define Distributor .", Me.ChkDistributor, 3000)
                        'Me.chkCP.Checked = False
                        Me.ChkDistributor.Focus()
                        Return False
                    End If
                Else
                    Me.baseTooltip.Show("Please define Distributor .", Me.ChkDistributor, 3000)
                    'Me.chkCP.Checked = False
                    Me.ChkDistributor.Focus()
                    Return False
                End If
            Else
                Me.baseTooltip.Show("Please define Distributor .", Me.ChkDistributor, 3000)
                'Me.chkCP.Checked = False
                Me.ChkDistributor.Focus()
                Return False
            End If
        End If
        Return True
    End Function

    Private Function IsValid() As Boolean
        If Me.mcbProgram.Value Is Nothing Then
            Me.baseTooltip.SetToolTip(Me.mcbProgram, "Program name is NULL !." & vbCrLf & "Please Define program name.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbProgram), Me.mcbProgram, 2500)
            Me.mcbProgram.Focus()
            Return False

        ElseIf Me.dtPicStart.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.dtPicStart, "Start_Date is Null !." & vbCrLf & "Please Defined Start_date.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dtPicStart), Me.dtPicStart, 2500)
            Me.dtPicStart.Focus()
            Return False
        ElseIf Me.dtPicEnd.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.dtPicEnd, "End_date is Null !." & vbCrLf & "Please Defined End_Date.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dtPicEnd), Me.dtPicEnd, 2500)
            Me.dtPicEnd.Focus()
            Return False
        End If
        'CHECK RPK
        If Me.chkRPK.Checked = True Then
            If Me.txtGiven.Value Is Nothing Then
                Me.tbType.SelectedTab = Me.tbiRPK
                Me.baseTooltip.SetToolTip(Me.txtGiven, "Given % is Null !." & vbCrLf & "Please Define Given %.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtGiven), Me.txtGiven, 2500)
                Me.txtGiven.Focus()
                Return False
            ElseIf Me.txtTargetPCT.Value Is Nothing Then
                Me.tbType.SelectedTab = Me.tbiRPK
                Me.baseTooltip.SetToolTip(Me.txtTargetPCT, "Target % is Null !." & vbCrLf & "Please Define Target %.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtTargetPCT), Me.txtTargetPCT, 2500)
                Me.txtTargetPCT.Focus()
                Return False
            ElseIf Me.txtTargetQTY.Text = "" Then
                Me.tbType.SelectedTab = Me.tbiRPK
                Me.baseTooltip.SetToolTip(Me.txtTargetQTY, "Target Quantity is Null !." & vbCrLf & "Please Define target Quantity.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtTargetQTY), Me.txtTargetQTY, 2500)
                Me.txtTargetQTY.Focus()
                Return False
            ElseIf Me.txtTargetQTY.Value = 0 Then
                Me.tbType.SelectedTab = Me.tbiRPK
                Me.baseTooltip.SetToolTip(Me.txtTargetQTY, "Target quantity is invalid !." & vbCrLf & "Target quantity must not be null/zero.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtTargetQTY), Me.txtTargetQTY, 2500)
                Me.txtTargetQTY.Focus()
                Me.txtTargetQTY.SelectionStart = 0
                Me.txtTargetQTY.SelectionLength = Me.txtTargetQTY.Text.Length
                Return False
            End If
        End If
        'CHECK PKPP
        If Me.chkPKPP.Checked = True Then
            'chek tab mana yang active
            Select Case Me.tbChildPKPP.SelectedTab.Name
                Case "tbValume"
                    If (Me.txtTargetPKPP.Value <= 0) Or (Me.txtTargetPKPP.Value Is Nothing) Then
                        Me.tbType.SelectedTab = Me.tbiPKPP
                        Me.baseTooltip.Show("Please Define target QTY", Me.txtTargetPKPP, 3000)
                        Me.txtTargetPKPP.Focus() : Me.txtTargetPKPP.SelectionStart = 0 : Me.txtTargetPKPP.SelectionLength = Me.txtTargetPKPP.Text.Length
                        Return False

                    End If
                    If (Me.txtBonusValuePKPP.Value > 0) Or (Me.txtTargetValuePKPP.Value > 0) Then
                        Me.tbChildPKPP.SelectedTab = Me.tbValue
                        Me.baseTooltip.Show("Please clear the values ", Me.txtTargetValuePKPP, 3000)
                        'Me.txtBonusValuePKPP.Focus() : Me.txtBonusValuePKPP.SelectionStart = 0 : Me.txtBonusValuePKPP.SelectionLength = Me.txtBonusValuePKPP.Text.Length
                        Return False
                    End If
                Case "tbValue"
                    If (Me.txtBonusValuePKPP.Value <= 0) Or (Me.txtTargetValuePKPP.Value Is Nothing) Then
                        Me.tbType.SelectedTab = Me.tbiPKPP
                        Me.baseTooltip.Show("Please Define target volume and value", Me.txtTargetValuePKPP, 3000)

                        Return False
                    End If
                    If (Me.txtTargetPKPP.Value > 0) Or (Me.txtTargetPKPP.Value > 0) Then
                        Me.tbChildPKPP.SelectedTab = Me.tbValume
                        Me.baseTooltip.Show("Please clear the values", Me.txtTargetPKPP, 3000)
                        Me.txtTargetPKPP.Focus() : Me.txtTargetPKPP.SelectionStart = 0 : Me.txtTargetPKPP.SelectionLength = Me.txtTargetPKPP.Text.Length
                        Return False
                    End If
            End Select
        End If
        'chekc cp
        If Me.chkCP.Checked = True Then
            If (Me.txtTargetCP.Value = 0) Or (Me.txtTargetCP.Value Is Nothing) Then
                Me.tbType.SelectedTab = Me.tbiCP
                Me.baseTooltip.Show("Please Define target QTY", Me.txtTargetCP, 3000)
                Return False
            ElseIf Me.chkTargetDistributor.Checked = False Then
                If Me.mcbTM.Value Is Nothing Then
                    Me.baseTooltip.Show("You must choose TM ID " & vbCrLf & "When Target to both TM and Distributor", Me.mcbTM, 2500)
                    : Me.mcbTM.Focus() : Return False
                ElseIf Me.mcbTM.SelectedIndex <= -1 Then
                    Me.baseTooltip.Show("You must choose TM ID " & vbCrLf & "When Target to both TM and Distributor", Me.mcbTM, 2500)
                    : Me.mcbTM.Focus() : Return False
                End If
            End If
        End If
        'check CPMRT
        If Me.chkCPMRT.Checked = True Then
            If (Me.txtTargetCPMRT.Value = 0) Or (Me.txtTargetCPMRT.Value Is Nothing) Then
                Me.tbType.SelectedTab = Me.tbCPMRT
                Me.baseTooltip.Show("Please Define target QTY", Me.txtTargetCPMRT, 3000)
                Return False
            ElseIf Me.chkTargetDistributorCPMRT.Checked = False Then
                If Me.mcbTMCPMRT.Value Is Nothing Then
                    Me.baseTooltip.Show("You must choose TM ID " & vbCrLf & "When Target to both TM and Distributor", Me.mcbTMCPMRT, 2500)
                    : Me.mcbTMCPMRT.Focus() : Return False
                ElseIf Me.mcbTMCPMRT.SelectedIndex <= -1 Then
                    Me.baseTooltip.Show("You must choose TM ID " & vbCrLf & "When Target to both TM and Distributor", Me.mcbTMCPMRT, 2500)
                    : Me.mcbTMCPMRT.Focus() : Return False
                End If
            End If
        End If
        'check DK
        If Me.chkDK.Checked = True Then
            If (Me.txtTargetDK.Value = 0) Or (Me.txtTargetDK.Value Is Nothing) Then
                Me.tbType.SelectedTab = Me.tbiDK
                Me.baseTooltip.Show("Please Define target QTY", Me.txtTargetCP, 3000)
                Return False
            End If
        End If
        'check CPR
        If Me.chkCPR.Checked = True Then
            If (Me.txtTargetCPR.Value = 0) Or (Me.txtTargetCPR.Value Is Nothing) Then
                Me.tbType.SelectedTab = Me.tbiCPR
                Me.baseTooltip.Show("Please Define target QTY", Me.txtTargetCPR, 3000)
                Return False
            End If
        End If
        If Me.chkHK.Checked = True Then
            If (Me.txtTargetHK.Value = 0) Or (Me.txtTargetHK.Value Is Nothing) Then
                Me.tbType.SelectedTab = Me.tbiHK
                Me.baseTooltip.Show("Please Define target QTY", Me.txtTargetHK, 3000)
                Return False
            End If
            If (Me.txtPrice.Value = 0) Or (Me.txtPrice.Value Is Nothing) Then
                Me.tbType.SelectedTab = Me.tbiHK
                Me.baseTooltip.Show("Please Define a valid Price", Me.txtPrice, 3000)
                Return False
            End If
        End If

        Return True
    End Function


#End Region

#Region " Event Procedure "

#Region " Form Event "

    Private Sub Distributor_Include_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor : Me.AcceptButton = Me.ButtonEntry1.btnInsert : Me.CancelButton = Me.ButtonEntry1.btnClose
            If Me.Mode = ModeSave.Save Then
                Me.mcbDistributor.Visible = False : Me.ChkDistributor.Visible = True : Me.btnFilterBrandPack.Enabled = True
                Me.btnFilterDistributor.Enabled = True : Me.btnFilterProgram.Enabled = True : Me.ButtonEntry1.btnInsert.Text = "&Insert"
            Else
                Me.mcbDistributor.Visible = True : Me.ChkDistributor.Visible = False : Me.btnFilterBrandPack.Enabled = False
                Me.btnFilterDistributor.Enabled = False : Me.btnFilterProgram.Enabled = False : Me.ButtonEntry1.btnInsert.Text = "&Update"
            End If
        Catch ex As Exception

        Finally
            Me.SFG = StateFillingGrid.HasFilled : Me.SFM = StateFillingMCB.HasFilled : Me.HasLoad = True
            Try
                If Me.Mode = ModeSave.Save Then
                    If Me.PROGRAM_ID <> "" Then
                        If Not IsNothing(Me.mcbProgram.DataSource) Then
                            Me.mcbProgram.Value = Me.PROGRAM_ID
                        End If
                        If Me.BRANDPACK_ID <> "" Then
                            If Not IsNothing(Me.mcbBrandPack.DataSource) Then
                                Me.mcbBrandPack.Value = Me.BRANDPACK_ID
                            End If
                        End If
                    End If
                End If

            Catch ex As Exception
                Me.HasLoad = True : Me.Cursor = Cursors.Default : Me.SFG = StateFillingGrid.HasFilled : Me.SFM = StateFillingMCB.HasFilled
            End Try
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub Distributor_Include_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Not Me.clsMRKT_BRANDPACK Is Nothing Then
                Me.clsMRKT_BRANDPACK.Dispose(False)
            End If
            'Me.Dispose(True)
            'If Me.Mode = ModeSave.Update Then
            '    If Me.UM = UpdateMode.FromOriginal Then
            '        Me.btnFilterBrandPack.Enabled = False : Me.btnFilterProgram.Enabled = False
            '        Me.btnFilterDistributor.Enabled = False
            '    End If
            'End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region " Button "

    Private Sub btnFilterProgram_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterProgram.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsMRKT_BRANDPACK.GetProgramID(Me.mcbProgram.Text)
            Me.BindMultiColumnCombo(Me.mcbProgram, Me.clsMRKT_BRANDPACK.GetProgramID(Me.mcbProgram.Text), _
           "", "PROGRAM_ID", "PROGRAM_ID")
            Dim itemCount As Integer = Me.mcbProgram.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterBrandPack_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterBrandPack.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.BindMultiColumnCombo(Me.mcbBrandPack, Me.clsMRKT_BRANDPACK.ViewBrandPack(), _
            "BRANDPACK_NAME LIKE '%" + Me.mcbBrandPack.Text + "%'", "BRANDPACK_NAME", "BRANDPACK_ID")
            Dim itemCount As Integer = Me.mcbBrandPack.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.ChkDistributor.Visible = True Then
                Me.BindCheckedCombo(Me.ChkDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor, _
                "DISTRIBUTOR_NAME LIKE '%" + Me.ChkDistributor.Text + "%'", "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
                Dim itemCount As Integer = Me.ChkDistributor.DropDownList().RecordCount()
                Me.ShowMessageInfo(itemCount.ToString() + " item(s) found")
            Else
                Me.BindMultiColumnCombo(Me.mcbDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor, _
                "DISTRIBUTOR_NAME LIKE '%" + Me.mcbDistributor.Text + "%'", "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
                Dim ItemCount As Integer = Me.mcbDistributor.DropDownList.RecordCount()
                Me.ShowMessageInfo(ItemCount.ToString() + " item(s) found")
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnChekExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChekExisting.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mcbProgram.Value Is Nothing Then
                Me.baseTooltip.SetToolTip(Me.mcbProgram, "Program not Defined !." & vbCrLf & "Please Defined Program.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbProgram), Me.mcbProgram, 2000)
                Me.mcbProgram.Focus()
                Return
            ElseIf Me.mcbBrandPack.Value Is Nothing Then
                Me.baseTooltip.SetToolTip(Me.mcbBrandPack, "BrandPack not Defined !." & vbCrLf & "Please Defined BrandPack Name.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2000)
                Me.mcbBrandPack.Focus()
                Return
                'ElseIf Me.ChkDistributor.CheckedItems.Length <= 0 Then
                '    Me.baseTooltip.SetToolTip(Me.ChkDistributor, "Distributor not Defined !." & vbCrLf & "Please Defined Distributor Name.")
                '    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.ChkDistributor), Me.ChkDistributor, 2000)
                '    Me.ChkDistributor.Focus()
                '    Return
            End If
            Dim ProgramID As String = Me.mcbProgram.Value.ToString()
            Dim BrandPackID As String = Me.mcbBrandPack.Value.ToString()
            If Me.ChkDistributor.Visible = True Then
                If Not IsNothing(Me.ChkDistributor.DropDownDataSource) Then
                    If Me.ChkDistributor.CheckedValues Is Nothing Then
                        Me.ShowMessageInfo("Please define distributor")
                        Return
                    End If
                End If
                For i As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                    Dim DistributorID As String = Me.ChkDistributor.CheckedValues.GetValue(i).ToString()
                    Dim PROG_BRANDPACK_DIST_ID As String = ProgramID + "" + BrandPackID + "" + DistributorID
                    If Me.clsMRKT_BRANDPACK.IsExistedPROG_BRANDACK_DIST_ID(PROG_BRANDPACK_DIST_ID, i = Me.ChkDistributor.CheckedValues.Length - 1) = True Then
                        Dim index As Integer = Me.clsMRKT_BRANDPACK.ViewDistributor.Find(Me.ChkDistributor.CheckedValues.GetValue(i))
                        If index <> -1 Then
                            Me.clsMRKT_BRANDPACK.ViewDistributor.Delete(index)
                            Me.SFM = StateFillingMCB.Filling
                            Me.BindCheckedCombo(Me.ChkDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor(), "", "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
                            Me.SFM = StateFillingMCB.HasFilled
                        End If
                        MessageBox.Show("DISTRIBUTOR_ID " & DistributorID & ",Already included", Me.MessageDataHasExisted, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Else
                        '    Me.ShowMessageInfo(Me.MessageDataSaveToAdd)
                        Return
                    End If
                Next
                Me.ShowMessageInfo(Me.MessageDataSaveToAdd)
            ElseIf Me.mcbDistributor.Visible = True Then
                Dim DistributorID As String = Me.mcbDistributor.Value.ToString()
                Dim PROG_BRANDPACK_DIST_ID As String = ProgramID + "" + BrandPackID + "" + DistributorID
                If Me.clsMRKT_BRANDPACK.IsExistedPROG_BRANDACK_DIST_ID(PROG_BRANDPACK_DIST_ID, True) = True Then
                    Dim index As Integer = Me.clsMRKT_BRANDPACK.ViewDistributor.Find(DistributorID)
                    If index <> -1 Then
                        Me.clsMRKT_BRANDPACK.ViewDistributor.Delete(index)
                        Me.SFM = StateFillingMCB.Filling
                        Me.BindCheckedCombo(Me.ChkDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor(), "", "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
                        Me.SFM = StateFillingMCB.HasFilled
                    End If
                    Me.ShowMessageInfo(Me.MessageDataHasExisted)
                    Return
                Else
                    Me.ShowMessageInfo(Me.MessageDataSaveToAdd)
                End If
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub AddNew()
        Dim PROGRAM_ID As Object = Me.mcbProgram.Value
        Dim BRANDPACK_ID As Object = Me.mcbBrandPack.Value
        Me.SFG = StateFillingGrid.Filling : Me.ClearControl() : Me.SFG = StateFillingGrid.HasFilled
        Me.UnabledControl()
        Me.mcbProgram.Enabled = True
        Me.ButtonEntry1.btnInsert.Enabled = True
        Me.ButtonEntry1.btnInsert.Text = "&Insert"
        Me.ButtonEntry1.btnAddNew.Enabled = True
        Me.ChkDistributor.Visible = True
        Me.mcbDistributor.Visible = False
        'Me.dtPicStart.Value = NufarmBussinesRules.SharedClass.ServerDate()
        'Me.dtPicEnd.Value = Convert.ToDateTime(DateSerial(Me.dtPicStart.Value.Year, Me.dtPicStart.Value.Month + 1, Me.dtPicStart.Value.Day))
        'Me.mcbProgram.Focus()
        Me.Mode = ModeSave.Save
        'Me.SFG = StateFillingGrid.Filling
        ''Me.mcbProgram.Value = Nothing
        'Me.SFG = StateFillingGrid.HasFilled
        If Not IsNothing(PROGRAM_ID) Then
            Me.mcbProgram.Value = PROGRAM_ID
        End If
        Me.SFG = StateFillingGrid.Filling
        Me.mcbBrandPack.Value = Nothing
        Me.SFG = StateFillingGrid.HasFilled
    End Sub

    Private Sub ButtonEntry1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEntry1.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                Case "btnAddNew"
                    Me.AddNew()
                Case "btnInsert"
                    If Me.IsValid() = False Then
                        Return
                    End If
                    If Not Me.IsCategorized() Then
                        Me.baseTooltip.Show("Please define program type", Me.PanelEx1, 2000)
                        Me.PanelEx1.Focus() : Return
                    End If
                    'CHECK DATA DISTRIBUTOR BRANDPACK YANG SAMA
                    Dim ItemsSave As Integer = 0
                    Dim ProgramID As String = Me.mcbProgram.Value.ToString()
                    Dim BrandPackID As String = Me.mcbBrandPack.Value.ToString()
                    Dim PROG_BRANDPACK_ID As String = ProgramID + "" + BrandPackID
                    Dim START_DATE As Object = CObj(Me.dtPicStart.Value)
                    Dim END_DATE As Object = CObj(Me.dtPicEnd.Value)
                    Dim SavedFail As Integer = 0
                    Dim SavedCount As Integer = 0
                    If Me.Mode = ModeSave.Save Then
                        If Me.ChkDistributor.CheckedValues.Length <= 0 Then
                            Me.baseTooltip.Show("Distributor Is Null" & vbCrLf & "Please Define Distributors", Me.ChkDistributor, 2500)
                            Return
                        End If
                        For i As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            Dim DistributorID As String = Me.ChkDistributor.CheckedValues.GetValue(i).ToString()
                            Dim PROG_BRANDPACK_DIST_ID As String = ProgramID + "" + BrandPackID + "" + DistributorID
                            If Me.clsMRKT_BRANDPACK.IsExistedPROG_BRANDACK_DIST_ID(PROG_BRANDPACK_DIST_ID, i = Me.ChkDistributor.CheckedValues.Length - 1) = True Then
                                'Dim index As Integer = Me.clsMRKT_BRANDPACK.ViewDistributor.Find(Me.ChkDistributor.CheckedValues.GetValue(i))
                                'If index <> -1 Then
                                '    Me.clsMRKT_BRANDPACK.ViewDistributor.Delete(index)
                                '    Me.SFM = StateFillingMCB.Filling
                                '    Me.BindCheckedCombo(Me.ChkDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor(), "")
                                '    Me.SFM = StateFillingMCB.HasFilled
                                'End If
                                MessageBox.Show("DISTRIBUTOR_ID " & DistributorID & ",Already included", Me.MessageDataHasExisted, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                'Else
                                '    Me.ShowMessageInfo(Me.MessageDataSaveToAdd)
                                Me.ChkDistributor.Focus()
                                Return
                            End If
                        Next
                        For i As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            Me.clsMRKT_BRANDPACK.DISTRIBUTOR_ID = Me.ChkDistributor.CheckedValues.GetValue(i).ToString() '.Value
                            Dim index_1 As String = Me.clsMRKT_BRANDPACK.ViewDistributor().Find(Me.clsMRKT_BRANDPACK.DISTRIBUTOR_ID)
                            If index_1 > -1 Then
                                Dim DistributorID As String = Me.ChkDistributor.CheckedValues.GetValue(i).ToString()
                                Dim PROG_BRANDPACK_DIST_ID As String = ProgramID + "" + BrandPackID + "" + DistributorID
                                Me.clsMRKT_BRANDPACK.PROG_BRANDPACK_DIST_ID = PROG_BRANDPACK_DIST_ID
                                Me.clsMRKT_BRANDPACK.PROGAM_ID = Me.mcbProgram.Value.ToString()
                                Me.clsMRKT_BRANDPACK.PROGRAM_NAME = Me.mcbProgram.Text
                                Me.AGREE_BRANDPACK_ID = Me.clsMRKT_BRANDPACK.GetAGREE_BRANDPACK_ID(Me.mcbBrandPack.Value.ToString(), Me.ChkDistributor.CheckedValues.GetValue(i).ToString(), Convert.ToDateTime(Me.dtPicEnd.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicStart.Value.ToShortDateString()), False)
                                Me.clsMRKT_BRANDPACK.AGREE_BRANDPACK_ID = Me.AGREE_BRANDPACK_ID
                                Me.clsMRKT_BRANDPACK.BRANDPACK_ID = Me.mcbBrandPack.Value.ToString()
                                Me.clsMRKT_BRANDPACK.BRANDPACK_NAME = Me.mcbBrandPack.Text
                                Me.clsMRKT_BRANDPACK.PROG_BRANDPACK_ID = PROG_BRANDPACK_ID
                                Me.clsMRKT_BRANDPACK.DISTRIBUTOR_NAME = Me.clsMRKT_BRANDPACK.ViewDistributor(index_1)("DISTRIBUTOR_NAME")
                                Me.clsMRKT_BRANDPACK.END_DATE = Convert.ToDateTime(Me.dtPicEnd.Value.ToShortDateString())
                                Me.clsMRKT_BRANDPACK.START_DATE = Convert.ToDateTime(Me.dtPicStart.Value.ToShortDateString())
                                If Me.chkStepping.Checked = True Then
                                    Me.clsMRKT_BRANDPACK.STEPPING = 1
                                Else
                                    Me.clsMRKT_BRANDPACK.STEPPING = 0
                                End If
                                'Me.clsMRKT_BRANDPACK.STEPPING = Me.chkStepping.Checked
                                Me.clsMRKT_BRANDPACK.ISCPR = Me.chkCPR.Checked
                                Me.clsMRKT_BRANDPACK.ISCP = Me.chkCP.Checked
                                Me.clsMRKT_BRANDPACK.ISCPMRT = Me.chkCPMRT.Checked
                                Me.clsMRKT_BRANDPACK.ISDK = Me.chkDK.Checked
                                Me.clsMRKT_BRANDPACK.ISHK = Me.chkHK.Checked
                                Me.clsMRKT_BRANDPACK.ISPKPP = Me.chkPKPP.Checked
                                Me.clsMRKT_BRANDPACK.ISRPK = Me.chkRPK.Checked
                                
                                Dim isSCPD As Boolean = Me.chkSpesialDiscountCPD.Checked
                                Me.clsMRKT_BRANDPACK.isSCPD = isSCPD

                                Dim IS_T_TM_DIST As Boolean = False
                                If Me.chkCP.Checked Then
                                    If Not Me.chkTargetDistributor.Checked Then
                                        IS_T_TM_DIST = True
                                    End If
                                    Me.clsMRKT_BRANDPACK.IS_T_TM_DIST = IS_T_TM_DIST
                                    If Not IsNothing(Me.mcbTM.Value) And Me.mcbTM.Text <> "" Then
                                        Me.clsMRKT_BRANDPACK.SHIP_TO_ID = Me.mcbTM.Value.ToString()
                                    Else
                                        Me.clsMRKT_BRANDPACK.SHIP_TO_ID = ""
                                    End If
                                End If

                                If Me.chkCPMRT.Checked Then
                                    If Not Me.chkTargetDistributorCPMRT.Checked Then
                                        IS_T_TM_DIST = True
                                    End If
                                    Me.clsMRKT_BRANDPACK.IS_T_TM_DIST = IS_T_TM_DIST
                                    If Not IsNothing(Me.mcbTMCPMRT.Value) And Me.mcbTMCPMRT.Text <> "" Then
                                        Me.clsMRKT_BRANDPACK.SHIP_TO_ID = Me.mcbTMCPMRT.Value.ToString()
                                    Else
                                        Me.clsMRKT_BRANDPACK.SHIP_TO_ID = ""
                                    End If
                                End If
                                If Me.chkPKPP.Checked Then
                                    If Not Me.chkTargetDistributorPKPP.Checked Then
                                        IS_T_TM_DIST = True
                                    End If
                                    Me.clsMRKT_BRANDPACK.IS_T_TM_DIST = IS_T_TM_DIST
                                    If Not IsNothing(Me.mcbTMPKPP.Value) And Me.mcbTMPKPP.Text <> "" Then
                                        Me.clsMRKT_BRANDPACK.SHIP_TO_ID = Me.mcbTMPKPP.Value.ToString()
                                    Else
                                        Me.clsMRKT_BRANDPACK.SHIP_TO_ID = ""
                                    End If
                                End If

                                Me.clsMRKT_BRANDPACK.GIVEN_CP = Me.txtGivenCP.Value
                                Me.clsMRKT_BRANDPACK.GIVEN_CPRMT = Me.txtGivenCPMRT.Value
                                Me.clsMRKT_BRANDPACK.GIVEN_CPR = Me.txtGivenCPR.Value
                                Me.clsMRKT_BRANDPACK.GIVEN_DISC_PCT = Me.txtGiven.Value
                                Me.clsMRKT_BRANDPACK.GIVEN_DK = Me.txtGivenDK.Value
                                Me.clsMRKT_BRANDPACK.GIVEN_PKPP = Me.txtGivenPKPP.Value
                                Me.clsMRKT_BRANDPACK.TARGET_CP = Me.txtTargetCP.Value
                                Me.clsMRKT_BRANDPACK.TARGET_CPR = Me.txtTargetCPR.Value
                                Me.clsMRKT_BRANDPACK.TARGET_CPMRT = Me.txtTargetCPMRT.Value

                                Me.clsMRKT_BRANDPACK.TARGET_DK = Me.txtTargetDK.Value
                                Me.clsMRKT_BRANDPACK.TARGET_HK = Me.txtTargetHK.Value

                                If Me.txtTargetValuePKPP.Value > 0 Then
                                    Me.clsMRKT_BRANDPACK.TARGET_PKPP = Me.txtTargetValuePKPP.Value
                                Else
                                    Me.clsMRKT_BRANDPACK.TARGET_PKPP = Me.txtTargetPKPP.Value
                                End If

                                Me.clsMRKT_BRANDPACK.PRICE_HK = Me.txtPrice.Value
                                Me.clsMRKT_BRANDPACK.TARGET_DISC_PCT = Me.txtTargetPCT.Value
                                Me.clsMRKT_BRANDPACK.TARGET_QTY = Me.txtTargetQTY.Value
                                'Me.clsMRKT_BRANDPACK.TARGET_VALUE = Me.txtTargetValuePKPP.Value
                                Me.clsMRKT_BRANDPACK.BONUS_VALUE = Me.txtBonusValuePKPP.Value
                                Me.clsMRKT_BRANDPACK.DESCRIPTIONS = Me.txtDescriptions.Text.TrimEnd().TrimStart()
                                Me.SFG = StateFillingGrid.Filling
                                Me.SFM = StateFillingMCB.Filling
                                Me.clsMRKT_BRANDPACK.SaveMRKT_BRANDPACK_DISTRIBUTOR("Save", i = Me.ChkDistributor.CheckedValues.Length - 1)
                                SavedCount += 1
                                'SuccesSaving = True
                            Else
                                SavedFail += 1
                                Me.ShowMessageInfo("Couldn't find DistributorName with ID " & Me.clsMRKT_BRANDPACK.DISTRIBUTOR_ID)
                                'Return
                            End If
                        Next
                    Else
                        Me.clsMRKT_BRANDPACK.DISTRIBUTOR_ID = Me.mcbDistributor.Value.ToString() '.Value
                        Dim index_1 As String = Me.clsMRKT_BRANDPACK.ViewDistributor().Find(Me.clsMRKT_BRANDPACK.DISTRIBUTOR_ID)
                        If index_1 <> -1 Then
                            Dim DistributorID As String = Me.mcbDistributor.Value.ToString()

                            Dim PROG_BRANDPACK_DIST_ID As String = ProgramID + "" + BrandPackID + "" + DistributorID
                            Me.clsMRKT_BRANDPACK.PROG_BRANDPACK_DIST_ID = PROG_BRANDPACK_DIST_ID
                            Me.clsMRKT_BRANDPACK.PROGAM_ID = Me.mcbProgram.Value.ToString()
                            Me.clsMRKT_BRANDPACK.PROGRAM_NAME = Me.mcbProgram.Text
                            Me.AGREE_BRANDPACK_ID = Me.clsMRKT_BRANDPACK.GetAGREE_BRANDPACK_ID(Me.mcbBrandPack.Value.ToString(), Me.mcbDistributor.Value.ToString(), Convert.ToDateTime(Me.dtPicEnd.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicStart.Value.ToShortDateString()), False)
                            Me.clsMRKT_BRANDPACK.AGREE_BRANDPACK_ID = Me.AGREE_BRANDPACK_ID
                            Me.clsMRKT_BRANDPACK.BRANDPACK_ID = Me.mcbBrandPack.Value.ToString()
                            Me.clsMRKT_BRANDPACK.BRANDPACK_NAME = Me.mcbBrandPack.Text
                            Me.clsMRKT_BRANDPACK.PROG_BRANDPACK_ID = PROG_BRANDPACK_ID
                            Me.clsMRKT_BRANDPACK.DISTRIBUTOR_NAME = Me.clsMRKT_BRANDPACK.ViewDistributor(index_1)("DISTRIBUTOR_NAME")
                            Me.clsMRKT_BRANDPACK.END_DATE = Convert.ToDateTime(Me.dtPicEnd.Value.ToShortDateString())
                            Me.clsMRKT_BRANDPACK.START_DATE = Convert.ToDateTime(Me.dtPicStart.Value.ToShortDateString())
                            If Me.chkStepping.Checked = True Then
                                Me.clsMRKT_BRANDPACK.STEPPING = 1
                            Else
                                Me.clsMRKT_BRANDPACK.STEPPING = 0
                            End If
                            'Me.clsMRKT_BRANDPACK.STEPPING = Me.chkStepping.Checked
                            Me.clsMRKT_BRANDPACK.ISCP = Me.chkCP.Checked
                            Dim isSCPD As Boolean = Me.chkSpesialDiscountCPD.Checked
                            Me.clsMRKT_BRANDPACK.isSCPD = isSCPD
                            Me.clsMRKT_BRANDPACK.ISCPMRT = Me.chkCPMRT.Checked
                            Me.clsMRKT_BRANDPACK.ISCPR = Me.chkCPR.Checked
                            Me.clsMRKT_BRANDPACK.ISDK = Me.chkDK.Checked
                            Me.clsMRKT_BRANDPACK.ISHK = Me.chkHK.Checked
                            Me.clsMRKT_BRANDPACK.ISPKPP = Me.chkPKPP.Checked
                            Me.clsMRKT_BRANDPACK.ISRPK = Me.chkRPK.Checked
                            '---------PENGHITUNGAN GENERATE BONUS QTY ,sudah tidak mengacu ke TM biarpun di register TM nya di sini--
                            'tanggal perubahan 12 april 2013
                            'jadi ship-to field di register sales program hanya menentukan siapa TM yang mengajukan program saja

                            'di balikin lagi tanggal 12 september 2013
                            Dim IS_T_TM_DIST As Boolean = False
                            If Me.chkCP.Checked Then
                                If Not Me.chkTargetDistributor.Checked Then
                                    IS_T_TM_DIST = True
                                End If
                                Me.clsMRKT_BRANDPACK.IS_T_TM_DIST = IS_T_TM_DIST
                                If Not IsNothing(Me.mcbTM.Value) And Me.mcbTM.Text <> "" Then
                                    Me.clsMRKT_BRANDPACK.SHIP_TO_ID = Me.mcbTM.Value.ToString()
                                Else
                                    Me.clsMRKT_BRANDPACK.SHIP_TO_ID = ""
                                End If
                            End If
                            If Me.chkCPMRT.Checked Then
                                If Not Me.chkTargetDistributorCPMRT.Checked Then
                                    IS_T_TM_DIST = True
                                End If
                                Me.clsMRKT_BRANDPACK.IS_T_TM_DIST = IS_T_TM_DIST
                                If Not IsNothing(Me.mcbTMCPMRT.Value) And Me.mcbTMCPMRT.Text <> "" Then
                                    Me.clsMRKT_BRANDPACK.SHIP_TO_ID = Me.mcbTMCPMRT.Value.ToString()
                                Else
                                    Me.clsMRKT_BRANDPACK.SHIP_TO_ID = ""
                                End If
                            End If
                            If Me.chkPKPP.Checked Then
                                If Not Me.chkTargetDistributorPKPP.Checked Then
                                    IS_T_TM_DIST = True
                                End If
                                Me.clsMRKT_BRANDPACK.IS_T_TM_DIST = IS_T_TM_DIST
                                If Not IsNothing(Me.mcbTMPKPP.Value) And Me.mcbTMPKPP.Text <> "" Then
                                    Me.clsMRKT_BRANDPACK.SHIP_TO_ID = Me.mcbTMPKPP.Value.ToString()
                                Else
                                    Me.clsMRKT_BRANDPACK.SHIP_TO_ID = ""
                                End If
                            End If

                            Me.clsMRKT_BRANDPACK.GIVEN_CP = Me.txtGivenCP.Value
                            Me.clsMRKT_BRANDPACK.GIVEN_CPRMT = Me.txtGivenCPMRT.Value
                            Me.clsMRKT_BRANDPACK.GIVEN_CPR = Me.txtGivenCPR.Value

                            Me.clsMRKT_BRANDPACK.GIVEN_DISC_PCT = Me.txtGiven.Value
                            Me.clsMRKT_BRANDPACK.GIVEN_DK = Me.txtGivenDK.Value
                            Me.clsMRKT_BRANDPACK.GIVEN_PKPP = Me.txtGivenPKPP.Value
                            Me.clsMRKT_BRANDPACK.TARGET_CP = Me.txtTargetCP.Value
                            Me.clsMRKT_BRANDPACK.TARGET_CPMRT = Me.txtTargetCPMRT.Value
                            Me.clsMRKT_BRANDPACK.TARGET_CPR = Me.txtTargetCPR.Value
                            Me.clsMRKT_BRANDPACK.TARGET_DK = Me.txtTargetDK.Value
                            Me.clsMRKT_BRANDPACK.TARGET_HK = Me.txtTargetHK.Value
                            If Me.txtTargetValuePKPP.Value > 0 Then
                                Me.clsMRKT_BRANDPACK.TARGET_PKPP = Me.txtTargetValuePKPP.Value
                            Else
                                Me.clsMRKT_BRANDPACK.TARGET_PKPP = Me.txtTargetPKPP.Value
                            End If
                            Me.clsMRKT_BRANDPACK.BONUS_VALUE = Me.txtBonusValuePKPP.Value
                            Me.clsMRKT_BRANDPACK.PRICE_HK = Me.txtPrice.Value
                            Me.clsMRKT_BRANDPACK.TARGET_DISC_PCT = Me.txtTargetPCT.Value
                            Me.clsMRKT_BRANDPACK.TARGET_QTY = Me.txtTargetQTY.Value
                            Me.clsMRKT_BRANDPACK.DESCRIPTIONS = Me.txtDescriptions.Text.TrimEnd().TrimStart()
                            Me.SFG = StateFillingGrid.Filling
                            Me.SFM = StateFillingMCB.Filling
                            Me.clsMRKT_BRANDPACK.SaveMRKT_BRANDPACK_DISTRIBUTOR("Update", True)
                            SavedCount += 1
                            'SavedFail += 1
                            'Me.clsMRKT_BRANDPACK.DISTRIBUTOR_NAME = Me.clsMRKT_BRANDPACK.ViewDistributor(index_1)("DISTRIBUTOR_NAME")
                        Else
                            Me.ShowMessageInfo("Couldn't find DistributorName with ID " & Me.clsMRKT_BRANDPACK.DISTRIBUTOR_ID)
                            Return
                        End If
                    End If
                    'if (SuccesSaving = True) and (succes 
                    If (SavedCount > 0) And (SavedFail <= 0) Then
                        Me.ShowMessageInfo(Me.MessageSavingSucces)
                    ElseIf (SavedCount > 0) And (SavedFail > 0) Then
                        Me.ShowMessageInfo(Me.MessageSavingSucces & vbCrLf & "But some data failed.")
                    ElseIf (SavedCount <= 0) Then
                        Me.ShowMessageInfo(Me.MessageSavingFailed)
                        Return
                    End If
                    If Me.Mode = ModeSave.Update Then
                        If Me.UM = UpdateMode.FromOriginal Then
                            Me.clsMRKT_BRANDPACK.Dispose(False)
                            Me.Close()
                        End If
                    Else
                        Me.PROGRAM_ID = Me.mcbProgram.Value.ToString() : Me.AddNew() : Me.SFG = StateFillingGrid.Filling : Me.GridEX1.Refetch()
                    End If
                    ParentFrm.MustReloadData = True
                Case "btnClose"
                    Me.clsMRKT_BRANDPACK.Dispose(False)
                    Me.Close()
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ButtonEntry1_btnClick")
        Finally
            Me.SFM = StateFillingMCB.HasFilled
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " Multicolumn Combo "

    Private Sub mcbProgram_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbProgram.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.HasLoad Then : Return : End If
            If Not Me.SFM = StateFillingMCB.HasFilled Then : Return : End If
            If (Me.mcbProgram.Value Is Nothing) Or (Me.mcbProgram.SelectedIndex = -1) Then
                Me.BindMultiColumnCombo(Me.mcbBrandPack, Nothing, Nothing, "", "")
                Me.SFM = StateFillingMCB.Filling
                Me.mcbBrandPack.Text = ""
                Me.ChkDistributor.UncheckAll()
                Me.ChkDistributor.Text = ""
                Me.SFM = StateFillingMCB.HasFilled
                Return
            End If
            Me.dtPicStart.IsNullDate = True : Me.dtPicEnd.IsNullDate = True : Me.SFM = StateFillingMCB.Filling
            If Me.Mode = ModeSave.Save Then : Me.UnabledControl() : End If
            Me.mcbProgram.Enabled = True : Me.mcbBrandPack.Enabled = True
            Me.clsMRKT_BRANDPACK.CreateViewBrandPack(Me.mcbProgram.Value.ToString(), True, True)
            Me.BindMultiColumnCombo(Me.mcbBrandPack, Me.clsMRKT_BRANDPACK.ViewBrandPack(), "", "BRANDPACK_NAME", "BRANDPACK_ID")
            'Me.SFM = StateFillingMCB.HasFilled
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbProgram_ValueChanged")
        Finally
            If Me.SFM = StateFillingMCB.Filling Then
                Me.SFM = StateFillingMCB.HasFilled
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mcbBrandPack_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbBrandPack.ValueChanged
        Try
            If Me.SFM = StateFillingMCB.Filling Then : Exit Sub : End If
            If Not Me.HasLoad Then : Exit Sub : End If
            Me.Cursor = Cursors.WaitCursor
            If (Me.mcbBrandPack.Value Is Nothing) And (Me.mcbBrandPack.SelectedIndex = -1) Then
                Me.BindCheckedCombo(Me.ChkDistributor, Nothing, Nothing, "", "")
                'Me.BindMultiColumnCombo(Me.mcbDistributor, Nothing, Nothing)
                Me.SFM = StateFillingMCB.Filling : Me.ChkDistributor.Text = "" : Me.ChkDistributor.UncheckAll()
                Me.SFM = StateFillingMCB.HasFilled : Return
            End If
            If Me.mcbProgram.Value Is Nothing Then
                Me.baseTooltip.SetToolTip(Me.mcbProgram, "Program not Defined" & vbCrLf & "Please Defined Program Name.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbProgram, 2000)
                Me.SFM = StateFillingMCB.Filling : Me.mcbBrandPack.Value = Nothing
                Me.ChkDistributor.UncheckAll() : Me.ChkDistributor.Text = "" : Me.SFM = StateFillingMCB.HasFilled
                Me.mcbProgram.Focus() : Return
            End If
            If Me.Mode = ModeSave.Save Then : Me.UnabledControl() : End If
            Me.mcbProgram.Enabled = True : Me.ChkDistributor.ReadOnly = False : Me.mcbBrandPack.Enabled = True
            Dim PROG_BRANDPACK_ID As String = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
            Me.clsMRKT_BRANDPACK.GetDateFromBrandPack(PROG_BRANDPACK_ID)
            Me.OriginalStartDateFromBrandPack = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
            Me.dtPicStart.IsNullDate = False : Me.dtPicEnd.IsNullDate = False
            Me.dtPicStart.Value = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.START_DATE) 'NufarmBussinesRules.SharedClass.ServerDate()
            Me.dtPicEnd.Value = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
            'Me.dtPicStart.MinDate = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.START_DATE) 'NufarmBussinesRules.SharedClass.ServerDate()
            'Me.dtPicStart.MaxDate = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
            'Me.dtPicEnd.MinDate = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.START_DATE) 'NufarmBussinesRules.SharedClass.ServerDate()
            'Me.dtPicEnd.MaxDate = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
            Dim StartDateString As String = Me.dtPicStart.Value.Month.ToString() + "/" & Me.dtPicStart.Value.Day.ToString() & "/" & Me.dtPicStart.Value.Year.ToString()
            Dim EndDateString As String = Me.dtPicEnd.Value.Month.ToString() + "/" & Me.dtPicEnd.Value.Day.ToString() & "/" & Me.dtPicEnd.Value.Year.ToString()
            If Me.Mode = ModeSave.Save Then
                Me.clsMRKT_BRANDPACK.CreateViewDistributor(PROG_BRANDPACK_ID, False, False)
                'Me.clsMRKT_BRANDPACK.CreateViewDistributor(Me.mcbProgram.Value.ToString(), Me.mcbBrandPack.Value.ToString(), StartDateString, EndDateString)
            Else
                Me.clsMRKT_BRANDPACK.CreateViewDistributor(PROG_BRANDPACK_ID, True, True)
            End If
            Me.BindMultiColumnCombo(Me.mcbDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor(), "", "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
            Me.BindCheckedCombo(Me.ChkDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor(), "", "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
            Me.SFM = StateFillingMCB.Filling : Me.ChkDistributor.UncheckAll() : Me.ChkDistributor.Text = "" : Me.SFM = StateFillingMCB.HasFilled
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbBrandPack_ValueChanged")
        Finally
            If Me.SFM = StateFillingMCB.Filling Then
                Me.SFM = StateFillingMCB.HasFilled
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ChkDistributor_CheckedValuesChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDistributor.CheckedValuesChanged
        Try
            If Me.SFM = StateFillingMCB.Filling Then
                Exit Sub
            End If
            If (Me.ChkDistributor.CheckedValues.Length <= 0) Then
                Me.UnabledControl()
                Me.mcbBrandPack.Enabled = True
                Me.ChkDistributor.ReadOnly = False
                Me.mcbProgram.Enabled = True
                Return
            ElseIf Me.Mode = ModeSave.Update Then
                If Me.UM = UpdateMode.FromOriginal Then
                Else
                    Me.EnabledControl()
                End If
            End If
            If Me.mcbProgram.Value Is Nothing Then
                Me.baseTooltip.SetToolTip(Me.mcbProgram, "Program not Defined !." & vbCrLf & "Please Defined Program Name.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbProgram), Me.mcbProgram, 2000)
                'Me.baseBallonSmall.SetBalloonCaption(Me.mcbProgram, "Program not Defined.")
                'Me.baseBallonSmall.SetBalloonText(Me.mcbProgram, "Please define the Program first.")
                Me.SFM = StateFillingMCB.Filling
                'Me.baseBallonSmall.ShowBalloon(Me.mcbProgram)
                Me.ChkDistributor.UncheckAll()
                Me.ChkDistributor.Text = ""
                Me.mcbProgram.Focus()
                Me.SFM = StateFillingMCB.HasFilled
            ElseIf Me.mcbBrandPack.Value Is Nothing Then
                Me.baseTooltip.SetToolTip(Me.mcbBrandPack, "BrandPack not Defined !." & vbCrLf & "Please Defined BrandPack Name.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2000)
                'Me.baseBallonSmall.SetBalloonCaption(Me.mcbBrandPack, "BrandPack not Defined.")
                'Me.baseBallonSmall.SetBalloonText(Me.mcbBrandPack, "Please define the BrandPack first.")
                'Me.baseBallonSmall.ShowBalloon(Me.mcbBrandPack)
                Me.SFM = StateFillingMCB.Filling
                Me.ChkDistributor.UncheckAll()
                Me.ChkDistributor.Text = ""
                Me.mcbBrandPack.Focus()
                Me.SFM = StateFillingMCB.HasFilled
            Else
                Me.EnabledControl()
                'Me.dtPicStart.Focus()
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " DateTime Control "

    Private Sub dtPicEnd_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicEnd.ValueChanged
        Try
            If Me.SFG = StateFillingGrid.Filling Then
                Exit Sub
            End If
            If Me.dtPicEnd.Text = "" Then
                Return
            End If
            If Me.dtPicStart.Text = "" Then
                Me.dtPicEnd.Text = ""
            End If
            'If DateDiff(DateInterval.Day, Me.dtPicStart.Value.Date, Me.dtPicEnd.Value.Date) <= 1 Then
            '    Me.dtPicEnd.Value = Convert.ToDateTime(DateSerial(Me.dtPicStart.Value.Year, Me.dtPicStart.Value.Month + 1, Me.dtPicStart.Value.Day))
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtPicStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicStart.ValueChanged
        Try
            If Me.SFM = StateFillingMCB.Filling Then : Exit Sub : End If
            If Not Me.HasLoad Then : Return : End If
            If Me.dtPicStart.Text <> "" Then
                If Me.mcbProgram.Value Is Nothing Then
                    Me.ShowMessageInfo("Please define the Program.") : Me.dtPicStart.ResetText() : Return
                ElseIf Me.mcbBrandPack.Value Is Nothing Then
                    Me.ShowMessageInfo("Please define the BrandPack.") : Me.dtPicStart.ResetText() : Return
                ElseIf Me.ChkDistributor.CheckedValues.Length <= 0 Then
                    Me.ShowMessageInfo("Please define the Distributor") : Me.dtPicStart.ResetText() : Return
                End If
                Dim ProgBrandPackID As String = Me.mcbProgram.Value.ToString() + Me.mcbBrandPack.Value.ToString()
                Me.clsMRKT_BRANDPACK.GetDateFromBrandPack(Me.mcbBrandPack.Value.ToString())
                Me.OriginalStartDateFromBrandPack = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.START_DATE)
                If Me.dtPicStart.Value < Me.OriginalStartDateFromBrandPack Then
                    Me.SFG = StateFillingGrid.Filling
                    Me.dtPicStart.Value = Me.OriginalStartDateFromBrandPack
                    Me.SFG = StateFillingGrid.HasFilled
                End If
                Me.OriginalEndDateFromBrandPack = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
                If Me.dtPicEnd.Value < Me.OriginalEndDateFromBrandPack Then
                    Me.SFG = StateFillingGrid.Filling : Me.dtPicEnd.Value = Me.OriginalEndDateFromBrandPack
                    Me.SFG = StateFillingGrid.HasFilled
                End If
                Me.dtPicEnd.Value = Me.OriginalEndDateFromBrandPack
                Me.dtPicStart.MinDate = Me.OriginalStartDateFromBrandPack
                Me.dtPicStart.MaxDate = Me.OriginalEndDateFromBrandPack
                Me.dtPicEnd.MinDate = Me.OriginalStartDateFromBrandPack
                Me.dtPicEnd.MaxDate = Me.OriginalEndDateFromBrandPack
                'bind distributor 
                Me.clsMRKT_BRANDPACK.CreateViewDistributor(ProgBrandPackID, False, True)
                'Dim StartDateString As String = Me.dtPicStart.Value.Month.ToString() + "/" & Me.dtPicStart.Value.Day.ToString() & "/" & Me.dtPicStart.Value.Year.ToString()
                'Dim EndDateString As String = Me.dtPicEnd.Value.Month.ToString() + "/" & Me.dtPicEnd.Value.Day.ToString() & "/" & Me.dtPicEnd.Value.Year.ToString()
                'Me.clsMRKT_BRANDPACK.CreateViewDistributor(Me.mcbProgram.Value.ToString(), Me.mcbBrandPack.Value.ToString(), StartDateString, EndDateString)
                Me.BindMultiColumnCombo(Me.mcbDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor(), "", "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
                Me.BindCheckedCombo(Me.ChkDistributor, Me.clsMRKT_BRANDPACK.ViewDistributor(), "", "DISTRIBUTOR_NAME", "DISTRIBUTOR_ID")
            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region " GridEx "

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor

            If Me.GridEX1.DataSource Is Nothing Then
                Me.ClearControl() : Me.SFG = StateFillingGrid.Filling : Me.ClearMultiColumnCombo() : Me.SFG = StateFillingGrid.HasFilled
                Return
            End If
            If Me.SFG = StateFillingGrid.Filling Then : Exit Sub : End If
            If Me.HasLoad = False Then : Return : End If
            Me.Mode = ModeSave.Update
            Me.UM = UpdateMode.FromGrid
            If Not (Me.GridEX1.CurrentRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.ClearControl() : Me.SFG = StateFillingGrid.Filling : Me.ClearMultiColumnCombo() : Me.SFG = StateFillingGrid.HasFilled
                Return
            End If
            Me.SFM = StateFillingMCB.Filling
            Me.InflateData()
            If Me.clsMRKT_BRANDPACK.HasReferencedDataMRKT_BRANDPCK_DISTRIBUTOR(Me.GridEX1.GetValue("PROG_BRANDPACK_DIST_ID")) = True Then
                Me.UnabledControl()
                Return
            Else
                Me.EnabledControl()
            End If
            Me.SetTypeProgram()
            Me.ButtonEntry1.btnInsert.Text = "&Update"
            Me.mcbBrandPack.Enabled = False
            Me.mcbProgram.Enabled = False
            Me.mcbDistributor.ReadOnly = True
            Me.ChkDistributor.ReadOnly = True
            Me.btnFilterBrandPack.Enabled = False : Me.btnFilterDistributor.Enabled = False
            Me.btnFilterProgram.Enabled = False
            Me.SFG = StateFillingGrid.HasFilled
            Me.SFM = StateFillingMCB.HasFilled
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled
            Me.SFM = StateFillingMCB.HasFilled
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_CurrentCellChanged")
        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.clsMRKT_BRANDPACK.HasReferencedDataMRKT_BRANDPCK_DISTRIBUTOR(Me.GridEX1.GetValue("PROG_BRANDPACK_DIST_ID").ToString()) = True Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData) : e.Cancel = True : Me.GridEX1.Refetch() : Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True : Me.GridEX1.Refetch() : Return
            End If
            Me.clsMRKT_BRANDPACK.DeletePROG_BRANDPACK_DIST_ID(Me.GridEX1.GetValue("PROG_BRANDPACK_DIST_ID").ToString())
            Me.ShowMessageInfo(Me.MessageSuccesDelete) : e.Cancel = False
            Me.GridEX1.UpdateData() : Me.ButtonEntry1.btnInsert.Text = "&Insert"
            Me.ButtonEntry1.btnInsert.Enabled = False : Me.ClearControl()
            Me.SFM = StateFillingMCB.Filling : Me.ClearMultiColumnCombo() : Me.SFM = StateFillingMCB.HasFilled
            Me.UnabledControl()
        Catch ex As Exception
            'Me.ShowMessageError(ex.Message)
            e.Cancel = True
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " Text Box "

    Private Sub txtGiven_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGiven.ValueChanged
        If Not Me.HasLoad Then : Return : End If
        If Me.SFM = StateFillingMCB.Filling Then : Return : End If
        If Not IsNothing(Me.txtGiven.Value) Then
            If Me.txtGiven.Value > 100 Then
                Me.baseTooltip.SetToolTip(Me.txtGiven, "Value Exceeds from 100" & vbCrLf & "Please suply with a valid one.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtGiven), Me.txtGiven, 3000)
                'Me.baseBallonSmall.SetBalloonCaption(Me.txtGiven, "Value Exceeds from 100")
                'Me.baseBallonSmall.SetBalloonText(Me.txtGiven, "Please suply with valid value")
                'Me.baseBallonSmall.ShowBalloon(Me.txtGiven)
                Me.txtGiven.Text = ""
            End If
        End If
    End Sub

    Private Sub txtTargetPCT_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTargetPCT.ValueChanged
        If Not Me.HasLoad Then : Return : End If
        If Me.SFM = StateFillingMCB.Filling Then : Return : End If
        If Not IsNothing(Me.txtTargetPCT.Value) Then
            If Me.txtTargetPCT.Value > 100 Then
                Me.baseTooltip.SetToolTip(Me.txtTargetPCT, "Value Excceds from 100 !." & vbCrLf & "Please Suply with a Valid one.")
                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtTargetPCT), Me.txtTargetPCT, 2000)
                'Me.baseBallonSmall.SetBalloonCaption(Me.txtTargetPCT, "Value Exceeds from 100")
                'Me.baseBallonSmall.SetBalloonText(Me.txtTargetPCT, "Please suply with valid value")
                'Me.baseBallonSmall.ShowBalloon(Me.txtTargetPCT)
                Me.txtTargetPCT.Text = ""
            End If
        End If
    End Sub

    Private Sub txtGivenPKPP_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGivenPKPP.ValueChanged
        Try
            If Not Me.HasLoad Then : Return : End If
            If Me.SFM = StateFillingMCB.Filling Then : Return : End If
            If Not IsNothing(Me.txtGivenPKPP.Value) Then
                If Me.txtGivenPKPP.Value > 0 Then
                    If Me.txtGivenPKPP.Value > 100 Then
                        Me.baseTooltip.SetToolTip(Me.txtGivenPKPP, "Value Exceeds from 100" & vbCrLf & "Please suply with a valid one.")
                        Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtGivenPKPP), Me.txtGivenPKPP, 3000)
                        'Me.baseBallonSmall.SetBalloonCaption(Me.txtGiven, "Value Exceeds from 100")
                        'Me.baseBallonSmall.SetBalloonText(Me.txtGiven, "Please suply with valid value")
                        'Me.baseBallonSmall.ShowBalloon(Me.txtGiven)
                        Me.txtGivenPKPP.Value = 0
                    ElseIf Me.txtTargetValuePKPP.Value > 0 Or Me.txtBonusValuePKPP.Value > 0 Then
                        'clearkan data
                        Me.baseTooltip.Show("you can not fill both value and volume", Me.txtGivenPKPP, 3000)
                        Me.txtGivenPKPP.Value = 0
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtTargetPKPP_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTargetPKPP.ValueChanged
        If Not Me.HasLoad Then : Return : End If
        If Me.SFM = StateFillingMCB.Filling Then : Return : End If
        If Not IsNothing(Me.txtTargetPKPP.Value) Then
            If Me.txtTargetPKPP.Value > 0 Then
                If Me.txtTargetValuePKPP.Value > 0 Or Me.txtBonusValuePKPP.Value > 0 Then
                    Me.baseTooltip.Show("you can not fill both value and volume", Me.txtTargetPKPP, 3000)
                    Me.txtTargetPKPP.Value = 0
                End If
            End If
        End If
    End Sub

    Private Sub txtBonusValuePKPP_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBonusValuePKPP.ValueChanged
        If Not Me.HasLoad Then : Return : End If
        If Me.SFM = StateFillingMCB.Filling Then : Return : End If
        If Not IsNothing(Me.txtBonusValuePKPP.Value) Then
            If Me.txtBonusValuePKPP.Value > 0 Then
                If Me.txtTargetPKPP.Value > 0 Or Me.txtGivenPKPP.Value > 0 Then
                    Me.baseTooltip.Show("you can not fill both value and volume", txtBonusValuePKPP, 3000)
                    Me.txtBonusValuePKPP.Value = 0
                End If
            End If
        End If
    End Sub

    Private Sub txtTargetValuePKPP_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTargetValuePKPP.ValueChanged
        If Not Me.HasLoad Then : Return : End If
        If Me.SFM = StateFillingMCB.Filling Then : Return : End If
        If Not IsNothing(Me.txtTargetValuePKPP.Value) Then
            If Me.txtTargetValuePKPP.Value > 0 Then
                If Me.txtTargetPKPP.Value > 0 Or Me.txtGivenPKPP.Value > 0 Then
                    Me.baseTooltip.Show("you can not fill both value and volume", txtTargetValuePKPP, 3000)
                    Me.txtTargetValuePKPP.Value = 0
                End If
            End If
        End If
    End Sub


    Private Sub txtGivenDK_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGivenDK.ValueChanged
        Try
            If Not Me.HasLoad Then : Return : End If
            If Me.SFM = StateFillingMCB.Filling Then : Return : End If
            If Not IsNothing(Me.txtGivenDK.Value) Then
                If Me.txtGivenDK.Value > 100 Then
                    Me.baseTooltip.SetToolTip(Me.txtGivenDK, "Value Exceeds from 100" & vbCrLf & "Please suply with a valid one.")
                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtGivenDK), Me.txtGivenDK, 3000)
                    'Me.baseBallonSmall.SetBalloonCaption(Me.txtGiven, "Value Exceeds from 100")
                    'Me.baseBallonSmall.SetBalloonText(Me.txtGiven, "Please suply with valid value")
                    'Me.baseBallonSmall.ShowBalloon(Me.txtGiven)
                    Me.txtGivenDK.Text = ""
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtGivenCPR_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGivenCPR.ValueChanged
        Try
            If Not Me.HasLoad Then : Return : End If
            If Me.SFM = StateFillingMCB.Filling Then : Return : End If
            If Not IsNothing(Me.txtGivenCPR.Value) Then
                If Me.txtGivenCPR.Value > 100 Then
                    Me.baseTooltip.SetToolTip(Me.txtGivenCPR, "Value Exceeds from 100" & vbCrLf & "Please suply with a valid one.")
                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtGivenCPR), Me.txtGivenCPR, 3000)
                    'Me.baseBallonSmall.SetBalloonCaption(Me.txtGiven, "Value Exceeds from 100")
                    'Me.baseBallonSmall.SetBalloonText(Me.txtGiven, "Please suply with valid value")
                    'Me.baseBallonSmall.ShowBalloon(Me.txtGiven)
                    Me.txtGivenCPR.Text = ""
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtGivenCP_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGivenCP.ValueChanged
        Try
            If Not Me.HasLoad Then : Return : End If
            If Me.SFM = StateFillingMCB.Filling Then : Return : End If
            If Not IsNothing(Me.txtGivenCP.Value) Then
                If Me.txtGivenCP.Value > 100 Then
                    Me.baseTooltip.SetToolTip(Me.txtGivenCP, "Value Exceeds from 100" & vbCrLf & "Please suply with a valid one.")
                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtGivenCP), Me.txtGivenCP, 3000)
                    'Me.baseBallonSmall.SetBalloonCaption(Me.txtGiven, "Value Exceeds from 100")
                    'Me.baseBallonSmall.SetBalloonText(Me.txtGiven, "Please suply with valid value")
                    'Me.baseBallonSmall.ShowBalloon(Me.txtGiven)
                    Me.txtGivenCP.Text = ""
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region " Check box "

    Private Sub chkHK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHK.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.chkHK.Checked = True Then
                If Not Me.IsValidID() Then
                    Me.chkHK.Checked = False
                    Return
                End If
            End If
            Me.SetTypeProgram()
            If Me.tbType.SelectedTab Is Me.tbiHK Then
                Me.tbType_SelectedTabChanged(Me.tbType, New DevComponents.DotNetBar.TabStripTabChangedEventArgs(Me.tbiHK, Me.tbiHK, DevComponents.DotNetBar.eEventSource.Mouse))
            Else
                Me.tbType.SelectedTab = Me.tbiHK
            End If
        Catch ex As Exception
            Me.chkHK.Checked = Not Me.chkHK.Checked
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkCP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCP.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim PROG_BRANDPACK_DIST_IDS As New Collection()
            Dim X As Integer = 1
            Dim PROG_BRANDPACK_ID As String = ""
            If Me.chkCP.Checked = True Then
                If Not Me.IsValidID() Then
                    Me.chkCP.Checked = False
                    Return
                ElseIf Me.Mode = ModeSave.Update Then
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                        Me.ShowMessageInfo("Please select distributor") : Me.chkCP.Checked = False : Return
                    End If
                    If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "CP") = True Then
                        Me.txtGivenCP.Enabled = False
                        Me.txtTargetCP.Enabled = False
                        Me.chkSpesialDiscountCPD.Enabled = False
                        'Me.chkTargetDistributor.Enabled = False
                        'Me.mcbTM.ReadOnly = True
                        Me.chkCP.Checked = Not Me.chkCP.Checked
                        Return
                    Else
                        Me.txtGivenCP.Enabled = True
                        Me.txtTargetCP.Enabled = True
                        Me.chkSpesialDiscountCPD.Enabled = True
                        'Me.chkTargetDistributor.Enabled = True
                        'Me.mcbTM.ReadOnly = False
                        Me.txtGivenCP.Value = 0
                        Me.txtTargetCP.Value = 0
                    End If
                End If
                Me.mcbTM.Focus()
                'check ke mcb apakah sudah di isi / belum
                If IsNothing(Me.mcbTM.DataSource) OrElse Me.mcbTM.DropDownList.RecordCount <= 0 Then
                    Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
                    Me.BindMultiColumnCombo(Me.mcbTM, DV, "", "MANAGER", "SHIP_TO_ID")
                End If
                ''isi dengan data TM
                Me.mcbTM.DroppedDown = True
                System.Threading.Thread.Sleep(200)
            ElseIf Me.Mode = ModeSave.Update Then
                PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                If Me.mcbDistributor.Visible = True Then
                    PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                ElseIf Me.ChkDistributor.Visible = True Then
                    For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                        X += 1
                    Next
                End If
                If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                    Me.ShowMessageInfo("Please select distributor") : Me.chkCP.Checked = False : Return
                End If
                Dim flag As String = ""
                If Me.chkSpesialDiscountCPD.Checked And Me.chkTargetDistributor.Checked = False Then
                    If Not IsNothing(Me.mcbTM.Value) Then
                        If Me.mcbTM.Value.ToString() <> "" Then
                            flag = "TS"
                        End If
                    End If
                ElseIf Me.chkSpesialDiscountCPD.Checked = True And Me.chkTargetDistributor.Checked = True Then
                    flag = "CS"
                ElseIf Me.chkSpesialDiscountCPD.Checked = False And Me.chkTargetDistributor.Checked = False Then
                    If Not IsNothing(Me.mcbTM.Value) Then
                        If Me.mcbTM.Value.ToString() <> "" Then
                            flag = "TD"
                        End If
                    End If
                ElseIf Me.chkSpesialDiscountCPD.Checked = False And Me.chkTargetDistributor.Checked = True Then
                    flag = "CP"
                End If

                If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, flag) = True Then
                    Me.txtGivenCP.Enabled = False
                    Me.txtTargetCP.Enabled = False
                    Me.chkSpesialDiscountCPD.Enabled = False
                    'Me.chkTargetDistributor.Enabled = False
                    'Me.mcbTM.ReadOnly = True
                    Me.chkCP.Checked = Not Me.chkCP.Checked
                    Return
                Else
                    Me.txtGivenCP.Enabled = True
                    Me.txtTargetCP.Enabled = True
                    Me.chkSpesialDiscountCPD.Enabled = True
                    'Me.chkTargetDistributor.Enabled = True
                    Me.mcbTM.Value = Nothing : Me.mcbTM.Text = ""
                    'Me.mcbTM.ReadOnly = False
                    Me.txtGivenCP.Value = 0
                    Me.txtTargetCP.Value = 0
                End If
                Me.mcbTM.Focus()
                'check ke mcb apakah sudah di isi / belum
                If IsNothing(Me.mcbTM.DataSource) OrElse Me.mcbTM.DropDownList.RecordCount <= 0 Then
                    Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
                    Me.BindMultiColumnCombo(Me.mcbTM, DV, "", "MANAGER", "SHIP_TO_ID")
                End If
                ''isi dengan data TM
                Me.mcbTM.DroppedDown = True
                System.Threading.Thread.Sleep(200)
            Else
                Me.mcbTM.Text = "" : Me.mcbTM.Value = Nothing
            End If
            Me.isHasCheckedRefrence = True
            Me.SetTypeProgram()
            If Me.tbType.SelectedTab Is Me.tbiCP Then
                Me.tbType_SelectedTabChanged(Me.tbType, New DevComponents.DotNetBar.TabStripTabChangedEventArgs(Me.tbiCP, Me.tbiCP, DevComponents.DotNetBar.eEventSource.Mouse))
            Else
                Me.tbType.SelectedTab = Me.tbiCP
            End If
            Me.isHasCheckedRefrence = False
        Catch ex As Exception
            Me.chkCP.Checked = Not Me.chkHK.Checked
        Finally
            Me.isHasCheckedRefrence = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkDK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDK.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim PROG_BRANDPACK_DIST_IDS As New Collection()
            Dim PROG_BRANDPACK_ID As String = ""
            Dim X As Integer = 1

            If Me.chkDK.Checked = True Then
                If Not Me.IsValidID() Then
                    Me.chkDK.Checked = False
                    Return
                ElseIf Me.Mode = ModeSave.Update Then
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                        Me.ShowMessageInfo("Please select distributor") : Me.chkDK.Checked = False : Return
                    End If
                    If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "DK") = True Then
                        Me.txtGivenDK.Enabled = False
                        Me.txtTargetDK.Enabled = False
                        Me.chkDK.Checked = Not Me.chkDK.Checked
                        Return
                    Else
                        Me.txtGivenDK.Enabled = True
                        Me.txtTargetDK.Enabled = True
                        Me.txtGivenDK.Value = 0
                        Me.txtTargetDK.Value = 0
                    End If
                End If
            ElseIf Me.Mode = ModeSave.Update Then
                PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                If Me.mcbDistributor.Visible = True Then
                    PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                ElseIf Me.ChkDistributor.Visible = True Then
                    For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                        X += 1
                    Next
                End If
                If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                    Me.ShowMessageInfo("Please select distributor") : Me.chkDK.Checked = False : Return
                End If
                If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "DK") = True Then
                    Me.txtGivenDK.Enabled = False
                    Me.txtTargetDK.Enabled = False
                    Me.chkDK.Checked = Not Me.chkDK.Checked
                    Return
                Else
                    Me.txtGivenDK.Enabled = True
                    Me.txtTargetDK.Enabled = True
                    Me.txtGivenDK.Value = 0
                    Me.txtTargetDK.Value = 0
                End If
            End If
            Me.isHasCheckedRefrence = True
            Me.SetTypeProgram()
            If Me.tbType.SelectedTab Is Me.tbiDK Then
                Me.tbType_SelectedTabChanged(Me.tbType, New DevComponents.DotNetBar.TabStripTabChangedEventArgs(Me.tbiDK, Me.tbiDK, DevComponents.DotNetBar.eEventSource.Mouse))
            Else
                Me.tbType.SelectedTab = Me.tbiDK
            End If
        Catch ex As Exception
            Me.chkDK.Checked = Not Me.chkDK.Checked
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkPKPP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPKPP.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim PROG_BRANDPACK_DIST_IDS As New Collection()
            Dim X As Integer = 1
            Dim PROG_BRANDPACK_ID As String = ""
            If Me.chkPKPP.Checked = True Then
                Me.tbChildPKPP.SelectedTabIndex = 0
                If Not Me.IsValidID() Then
                    Me.chkPKPP.Checked = False
                    Return
                ElseIf Me.Mode = ModeSave.Update Then
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                        Me.ShowMessageInfo("Please select distributor") : Me.chkPKPP.Checked = False : Return
                    End If
                    If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "KP") = True Then
                        Me.txtGivenPKPP.Enabled = False
                        Me.txtTargetPKPP.Enabled = False
                        Me.chkPKPP.Checked = Not Me.chkPKPP.Checked
                        Return
                    Else
                        Me.txtGivenPKPP.Enabled = True
                        Me.txtTargetPKPP.Enabled = True
                        Me.txtGivenPKPP.Value = 0
                        Me.txtTargetPKPP.Value = 0
                    End If
                End If
                Me.mcbTMPKPP.Focus()
                'check ke mcb apakah sudah di isi / belum
                If IsNothing(Me.mcbTMPKPP.DataSource) OrElse Me.mcbTMPKPP.DropDownList.RecordCount <= 0 Then
                    Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
                    Me.BindMultiColumnCombo(Me.mcbTMPKPP, DV, "", "MANAGER", "SHIP_TO_ID")
                End If
                ''isi dengan data TM
                Me.mcbTMPKPP.DroppedDown = True
                System.Threading.Thread.Sleep(200)
            ElseIf Me.Mode = ModeSave.Update Then
                PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                If Me.mcbDistributor.Visible = True Then
                    PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                ElseIf Me.ChkDistributor.Visible = True Then
                    For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                        X += 1
                    Next
                End If
                If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                    Me.ShowMessageInfo("Please select distributor") : Me.chkPKPP.Checked = False : Return
                End If
                If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "KP") = True Then
                    Me.txtGivenPKPP.Enabled = False
                    Me.txtTargetPKPP.Enabled = False
                    Me.chkPKPP.Checked = Not Me.chkPKPP.Checked
                    Return
                Else
                    Me.txtGivenPKPP.Enabled = True
                    Me.txtTargetPKPP.Enabled = True
                    Me.txtGivenPKPP.Value = 0
                    Me.txtTargetPKPP.Value = 0
                End If
                Me.mcbTMPKPP.Focus()
                'check ke mcb apakah sudah di isi / belum
                If IsNothing(Me.mcbTMPKPP.DataSource) OrElse Me.mcbTMPKPP.DropDownList.RecordCount <= 0 Then
                    Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
                    Me.BindMultiColumnCombo(Me.mcbTMPKPP, DV, "", "MANAGER", "SHIP_TO_ID")
                End If
                ''isi dengan data TM
                Me.mcbTMPKPP.DroppedDown = True
                System.Threading.Thread.Sleep(200)
            Else
                Me.mcbTMPKPP.Text = "" : Me.mcbTMPKPP.Value = Nothing
            End If
            Me.isHasCheckedRefrence = True
            Me.SetTypeProgram()
            If Me.tbType.SelectedTab Is Me.tbiPKPP Then
                Me.tbType_SelectedTabChanged(Me.tbType, New DevComponents.DotNetBar.TabStripTabChangedEventArgs(Me.tbiPKPP, Me.tbiPKPP, DevComponents.DotNetBar.eEventSource.Mouse))
            Else
                Me.tbType.SelectedTab = Me.tbiPKPP
            End If
            If IsNothing(Me.mcbTMPKPP.DataSource) Then
                Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
                Me.BindMultiColumnCombo(Me.mcbTM, DV, "", "MANAGER", "SHIP_TO_ID")
            ElseIf Me.mcbTMPKPP.DropDownList.RecordCount <= 0 Then
                Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
                Me.BindMultiColumnCombo(Me.mcbTM, DV, "", "MANAGER", "SHIP_TO_ID")
            End If

            Me.isHasCheckedRefrence = False
        Catch ex As Exception
            Me.isHasCheckedRefrence = False : Me.chkPKPP.Checked = Not Me.chkPKPP.Checked
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkRPK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRPK.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim PROG_BRANDPACK_DIST_IDS As New Collection()
            Dim X As Integer = 1
            Dim PROG_BRANDPACK_ID As String = ""
            If Me.chkRPK.Checked = True Then
                If Not Me.IsValidID() Then
                    Me.chkRPK.Checked = False
                    Return
                ElseIf Me.Mode = ModeSave.Update Then
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                        Me.ShowMessageInfo("Please select distributor") : Me.chkRPK.Checked = False : Return
                    End If
                    If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "MG") = True Then
                        Me.txtGiven.Enabled = False
                        Me.txtTargetQTY.Enabled = False
                        Me.txtTargetPCT.Enabled = False
                        Me.chkStepping.Enabled = False
                        Me.chkRPK.Checked = Not Me.chkRPK.Checked
                        Return
                    Else
                        Me.txtGiven.Value = 0
                        Me.txtTargetQTY.Value = 0
                        Me.txtTargetPCT.Value = 0
                        Me.txtGiven.Enabled = True
                        Me.txtTargetQTY.Enabled = True
                        Me.txtTargetPCT.Enabled = True
                        Me.chkStepping.Enabled = True
                    End If
                End If
            ElseIf Me.Mode = ModeSave.Update Then
                PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                If Me.mcbDistributor.Visible = True Then
                    PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                ElseIf Me.ChkDistributor.Visible = True Then
                    For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                        X += 1
                    Next
                End If
                If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                    Me.ShowMessageInfo("Please select distributor") : Me.chkRPK.Checked = False : Return
                End If
                If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "MG") = True Then
                    Me.txtGiven.Enabled = False
                    Me.txtTargetQTY.Enabled = False
                    Me.chkStepping.Enabled = False
                    Me.chkRPK.Checked = Not Me.chkRPK.Checked
                    Return
                Else
                    Me.txtGiven.Value = 0
                    Me.txtTargetQTY.Value = 0
                    Me.txtTargetPCT.Value = 0
                    Me.txtGiven.Enabled = True
                    Me.txtTargetQTY.Enabled = True
                    Me.chkStepping.Enabled = True
                    If Me.chkStepping.Checked = True Then
                        Me.chkStepping.Checked = False
                    End If
                End If
            End If
            Me.isHasCheckedRefrence = True
            Me.SetTypeProgram()
            If Me.tbType.SelectedTab Is Me.tbiRPK Then
                Me.tbType_SelectedTabChanged(Me.tbType, New DevComponents.DotNetBar.TabStripTabChangedEventArgs(Me.tbiRPK, Me.tbiRPK, DevComponents.DotNetBar.eEventSource.Mouse))
            Else
                Me.tbType.SelectedTab = Me.tbiRPK
            End If
            Me.isHasCheckedRefrence = False
        Catch ex As Exception
            Me.isHasCheckedRefrence = False : Me.chkRPK.Checked = Not Me.chkRPK.Checked
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkStepping_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkStepping.Click
        If Me.chkStepping.Checked = True Then
            Me.txtTargetPCT.Value = 0
            Me.txtTargetPCT.Enabled = False
        Else
            Me.baseTooltip.SetToolTip(Me.txtTargetPCT, "Please the textbox !." & vbCrLf & "You must fill this textbox.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtTargetPCT), Me.txtTargetPCT, 2500)
            Me.txtTargetPCT.Enabled = True
            Me.txtTargetPCT.Focus()
        End If
    End Sub

    Private Sub chkCPR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCPR.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim PROG_BRANDPACK_DIST_IDS As New Collection()
            Dim X As Integer = 1
            Dim PROG_BRANDPACK_ID As String = ""
            If Me.chkCPR.Checked = True Then
                If Not Me.IsValidID() Then
                    Me.chkCPR.Checked = False
                    Return
                ElseIf Me.Mode = ModeSave.Update Then
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                        Me.ShowMessageInfo("Please select distributor") : Me.chkCPR.Checked = False : Return
                    End If
                    If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "CR") = True Then
                        Me.txtGivenCPR.Enabled = False
                        Me.txtTargetCPR.Enabled = False
                        Me.chkCPR.Checked = Not Me.chkCPR.Checked
                        Return
                    Else
                        Me.txtGivenCPR.Value = 0
                        Me.txtTargetCPR.Value = 0
                        Me.txtGivenCPR.Enabled = True
                        Me.txtTargetCPR.Enabled = True
                    End If
                End If
            ElseIf Me.Mode = ModeSave.Update Then
                PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                If Me.mcbDistributor.Visible = True Then
                    PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                ElseIf Me.ChkDistributor.Visible = True Then
                    For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                        X += 1
                    Next
                End If
                If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                    Me.ShowMessageInfo("Please select distributor") : Me.chkCPR.Checked = False : Return
                End If
                If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "CR") = True Then
                    Me.txtGivenCPR.Enabled = False
                    Me.txtTargetCPR.Enabled = False
                    Me.chkCPR.Checked = Not Me.chkCPR.Checked
                    Return
                Else
                    Me.txtGivenCPR.Enabled = True
                    Me.txtTargetCPR.Enabled = True
                    Me.txtGivenCPR.Value = 0
                    Me.txtTargetCPR.Value = 0
                End If

            End If
            Me.isHasCheckedRefrence = True
            Me.SetTypeProgram()
            If Me.tbType.SelectedTab Is Me.tbiCPR Then
                Me.tbType_SelectedTabChanged(Me.tbType, New DevComponents.DotNetBar.TabStripTabChangedEventArgs(Me.tbiCPR, Me.tbiCPR, DevComponents.DotNetBar.eEventSource.Mouse))
            Else
                Me.tbType.SelectedTab = Me.tbiCPR
            End If
            Me.isHasCheckedRefrence = False 'balikan lagi ke semula
        Catch ex As Exception
            Me.isHasCheckedRefrence = False : Me.chkCPR.Checked = Not Me.chkCPR.Checked
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkTargetDistributor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTargetDistributor.CheckedChanged
        If Me.HasLoad Then
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        End If

        If Me.chkTargetDistributor.Checked = False Then
            'Me.mcbTM.ReadOnly = False
            Me.mcbTM.Focus()
            'check ke mcb apakah sudah di isi / belum
            If IsNothing(Me.mcbTM.DataSource) OrElse Me.mcbTM.DropDownList.RecordCount Then
                Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
                Me.BindMultiColumnCombo(Me.mcbTM, DV, "", "MANAGER", "SHIP_TO_ID")
            End If
            ''isi dengan data TM
            Me.mcbTM.DroppedDown = True
            System.Threading.Thread.Sleep(200)
        Else
            Me.mcbTM.Value = Nothing
            'Me.mcbTM.ReadOnly = True
            Me.mcbTM.DroppedDown = False
        End If
        'Me.mcbTM.Enabled 
    End Sub

#End Region

#End Region

#Region " Tab "

    Private Sub tbType_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbType.Enter
        Try
            Me.IsValidID()
            If Me.Mode = ModeSave.Save Then
                If Me.chkCP.Checked = False And Me.chkCPR.Checked = False And Me.chkDK.Checked = False _
                And Me.chkHK.Checked = False And Me.chkPKPP.Checked = False And Me.chkRPK.Checked = False And Me.chkCPMRT.Checked = False Then
                    Me.baseTooltip.Show("Please define category program", Me.PanelEx1, 2500) : Me.PanelEx1.Focus() : Return
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tbType_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbType.MouseEnter
        Try
            Me.IsValidID()
            If Me.Mode = ModeSave.Save Then
                If Me.chkCP.Checked = False And Me.chkCPR.Checked = False And Me.chkDK.Checked = False _
                And Me.chkHK.Checked = False And Me.chkPKPP.Checked = False And Me.chkRPK.Checked = False And Me.chkCPMRT.Checked = False Then
                    Me.baseTooltip.Show("Please define category program", Me.PanelEx1, 2500) : Me.PanelEx1.Focus() : Return
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tbType_SelectedTabChanged(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.TabStripTabChangedEventArgs) Handles tbType.SelectedTabChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim PROG_BRANDPACK_ID As String = ""
            If Me.HasLoad = False Then : Return : End If
            Dim IsVal As Boolean = Me.IsValidID()
            Dim PROG_BRANDPACK_DIST_IDS As New Collection()
            Dim X As Integer = 1

            Dim Isgenerated As Boolean = False
            Select Case e.NewTab.Name
                Case "tbiCP"
                    If IsVal = False Then : Me.txtGivenCP.Enabled = False : Me.txtTargetCP.Enabled = False
                        'Me.chkTargetDistributor.Enabled = False
                        Me.chkSpesialDiscountCPD.Enabled = False : Return : End If
                    If Me.isHasCheckedRefrence Then : Return : End If
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()

                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then : Me.ShowMessageInfo("Please select distributor") : Return : End If
                    Dim flag As String = ""
                    If Me.chkSpesialDiscountCPD.Checked And Me.chkTargetDistributor.Checked = False Then
                        If Not IsNothing(Me.mcbTM.Value) Then
                            If Me.mcbTM.Value.ToString() <> "" Then
                                flag = "TS"
                            End If
                        End If
                    ElseIf Me.chkSpesialDiscountCPD.Checked = True And Me.chkTargetDistributor.Checked = True Then
                        flag = "CS"
                    ElseIf Me.chkSpesialDiscountCPD.Checked = False And Me.chkTargetDistributor.Checked = False Then
                        If Not IsNothing(Me.mcbTM.Value) Then
                            If Me.mcbTM.Value.ToString() <> "" Then
                                flag = "TD"
                            End If
                        End If
                    ElseIf Me.chkSpesialDiscountCPD.Checked = False And Me.chkTargetDistributor.Checked = True Then
                        flag = "CP"
                    End If
                    If Me.Mode = ModeSave.Update Then
                        If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, flag) = True Then
                            Me.txtGivenCP.Enabled = False : Me.txtTargetCP.Enabled = False
                            'Me.chkTargetDistributor.Enabled = False
                            Me.chkSpesialDiscountCPD.Enabled = False
                        End If
                    End If
                Case "tbCPMRT"
                    If IsVal = False Then : Me.txtGivenCPMRT.Enabled = False : Me.txtTargetCPMRT.Enabled = False
                        'Me.chkTargetDistributor.Enabled = False
                        Return : End If
                    If Me.isHasCheckedRefrence Then : Return : End If
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()

                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then : Me.ShowMessageInfo("Please select distributor") : Return : End If
                    Dim flag As String = "CD"
                    If Me.chkTargetDistributorCPMRT.Checked Then
                        flag = "CD"
                    End If
                    If Not IsNothing(Me.mcbTMCPMRT.Value) Then
                        If Me.mcbTMCPMRT.Value.ToString() <> "" Then
                            flag = "CT"
                        End If
                    End If
                    If Me.Mode = ModeSave.Update Then
                        If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, flag) = True Then
                            Me.txtGivenCPMRT.Enabled = False : Me.txtTargetCPMRT.Enabled = False
                        End If
                    End If
                Case "tbiCPR"

                    If IsVal = False Then : Me.txtGivenCPR.Enabled = False : Me.txtTargetCPR.Enabled = False : Return : End If
                    If Me.isHasCheckedRefrence Then : Return : End If
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                        Me.ShowMessageInfo("Please select distributor") : Return
                    End If
                    If Me.Mode = ModeSave.Update Then
                        If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "CR") = True Then
                            Me.txtGivenCPR.Enabled = False : Me.txtTargetCPR.Enabled = False
                        End If
                    End If

                Case "tbiDK"
                    If IsVal = False Then : Me.txtGivenDK.Enabled = False : Me.txtTargetDK.Enabled = False : Return : End If
                    If Me.isHasCheckedRefrence Then : Return : End If
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then : Me.ShowMessageInfo("Please select distributor") : Return : End If
                    If Me.Mode = ModeSave.Update Then
                        If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "DK") = True Then
                            Me.txtGivenDK.Enabled = False : Me.txtTargetDK.Enabled = False
                        End If
                    End If

                Case "tbiHK"
                    If IsVal = False Then : Me.txtPrice.Enabled = False : Me.txtTargetHK.Enabled = False : Return : End If
                    'If Me.isHasCheckedRefrence Then : Return : End If
                    'PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    Dim StartDateString As String = Me.dtPicStart.Value.Month.ToString() + "/" & Me.dtPicStart.Value.Day.ToString() & "/" & Me.dtPicStart.Value.Year.ToString()
                    If Me.Mode = ModeSave.Update Then
                        Dim LastPODate As Object = Me.clsMRKT_BRANDPACK.getlastPODate(Me.mcbDistributor.Value.ToString(), Me.mcbBrandPack.Value.ToString(), StartDateString)
                        If Not IsNothing(LastPODate) Then
                            Me.dtPicStart.MinDate = Convert.ToDateTime(LastPODate) : Me.dtPicStart.MaxDate = Me.dtPicEnd.Value()
                            Me.dtPicEnd.MinDate = Convert.ToDateTime(LastPODate)
                        End If
                    End If
                    'aAMBIL PRICE JIKA HK DI CHECKED
                    If Me.chkHK.Checked = True Then
                        Dim EndDateString As String = Me.dtPicEnd.Value.Month.ToString() + "/" & Me.dtPicEnd.Value.Day.ToString() & "/" & Me.dtPicEnd.Value.Year.ToString()
                        Dim ListDistributors As New List(Of String)
                        If (Me.ChkDistributor.Visible = True) And (Me.ChkDistributor.CheckedValues.Length > 0) Then
                            For i As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                                ListDistributors.Add(Me.ChkDistributor.CheckedValues.GetValue(i).ToString())
                            Next
                        Else
                            ListDistributors.Add(Me.mcbDistributor.Value.ToString())
                        End If
                        If ListDistributors.Count <= 0 Then : Me.ShowMessageInfo("Please select distributor") : Return : End If
                        Me.txtPrice.Value = Me.clsMRKT_BRANDPACK.getPriceValue(ListDistributors, Me.mcbBrandPack.Value.ToString(), StartDateString, EndDateString)
                        If Me.txtPrice.Value > 0 Then : Me.txtPrice.Enabled = False : Else : Me.txtPrice.Enabled = True : End If
                    End If
                Case "tbiPKPP"
                    If IsVal = False Then : Me.txtGivenPKPP.Enabled = False : Me.txtTargetPKPP.Enabled = False : Me.mcbTMPKPP.ReadOnly = True : Return : End If
                    If Me.isHasCheckedRefrence Then : Return : End If
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                        Me.ShowMessageInfo("Please select distributor") : Return
                    End If
                    If Me.Mode = ModeSave.Update Then
                        If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "KP") = True Then
                            Me.txtGivenPKPP.Enabled = False : Me.txtTargetPKPP.Enabled = False : Me.mcbTMPKPP.ReadOnly = True
                        End If
                        If Me.Mode = ModeSave.Save Then
                            Me.tbChildPKPP.SelectedTabIndex = 0
                        ElseIf Me.txtTargetValuePKPP.Value > 0 Or Me.txtBonusValuePKPP.Value > 0 Then
                            Me.tbChildPKPP.SelectedTabIndex = 1
                        ElseIf Me.txtTargetPKPP.Value > 0 Or Me.txtGivenPKPP.Value > 0 Then
                            Me.tbChildPKPP.SelectedTabIndex = 0
                        End If
                    End If

                Case "tbiRPK"
                    If IsVal = False Then
                        Me.txtGiven.Enabled = False : Me.txtTargetQTY.Enabled = False : Me.txtTargetPCT.Enabled = False
                        Me.chkStepping.Enabled = False : Return
                    End If
                    If Me.isHasCheckedRefrence Then : Return : End If
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then : Me.ShowMessageInfo("Please select distributor") : Return : End If
                    If Me.Mode = ModeSave.Update Then
                        If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "MG") = True Then
                            Me.txtGiven.Enabled = False : Me.txtTargetQTY.Enabled = False : Me.txtTargetPCT.Enabled = False : Me.chkStepping.Enabled = False
                        End If
                    End If
            End Select
            If PROG_BRANDPACK_ID <> "" Then
                Me.SFM = StateFillingMCB.Filling
                Me.clsMRKT_BRANDPACK.GetDateFromBrandPack(PROG_BRANDPACK_ID)
                Me.dtPicStart.IsNullDate = False : Me.dtPicEnd.IsNullDate = False
                Me.dtPicStart.Value = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.START_DATE) 'NufarmBussinesRules.SharedClass.ServerDate()
                Me.dtPicEnd.Value = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
                Me.dtPicStart.MinDate = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.START_DATE) 'NufarmBussinesRules.SharedClass.ServerDate()
                Me.dtPicStart.MaxDate = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
                Me.dtPicEnd.MinDate = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.START_DATE) 'NufarmBussinesRules.SharedClass.ServerDate()
                Me.dtPicEnd.MaxDate = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
                Me.SFM = StateFillingMCB.HasFilled
            End If

        Catch ex As Exception
            Me.isHasCheckedRefrence = False
            Me.SFM = StateFillingMCB.HasFilled
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_tbType_SelectedTabChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TabControlPanel1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControlPanel1.Enter
        If Not Me.IsCategorized Then
            Me.baseTooltip.Show("Please define sales type", Me.PanelEx1, 2000)
            Me.PanelEx1.Focus()
        End If
    End Sub

    Private Sub TabControlPanel5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControlPanel5.Enter
        If Not Me.IsCategorized Then
            Me.baseTooltip.Show("Please define sales type", Me.PanelEx1, 2000)
            Me.PanelEx1.Focus()
        End If
    End Sub

    Private Sub TabControlPanel6_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControlPanel6.Enter
        If Not Me.IsCategorized Then
            Me.baseTooltip.Show("Please define sales type", Me.PanelEx1, 2000)
            Me.PanelEx1.Focus()
        End If
    End Sub

    Private Sub TabControlPanel2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControlPanel2.Enter
        If Not Me.IsCategorized Then
            Me.baseTooltip.Show("Please define sales type", Me.PanelEx1, 2000)
            Me.PanelEx1.Focus()
        End If
    End Sub

    Private Sub TabControlPanel3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControlPanel3.Enter
        If Not Me.IsCategorized Then
            Me.baseTooltip.Show("Please define sales type", Me.PanelEx1, 2000)
            Me.PanelEx1.Focus()
        End If
    End Sub

    Private Sub TabControlPanel4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControlPanel4.Enter
        If Not Me.IsCategorized Then
            Me.baseTooltip.Show("Please define sales type", Me.PanelEx1, 2000)
            Me.PanelEx1.Focus()
        End If

    End Sub

    Private Sub tbChildPKPP_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbChildPKPP.Enter
        If Not Me.IsCategorized Then
            Me.baseTooltip.Show("Please define sales type", Me.PanelEx1, 2000)
            Me.PanelEx1.Focus()
        End If
    End Sub
#End Region

    Private Sub chkCPMRT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCPMRT.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim PROG_BRANDPACK_DIST_IDS As New Collection()
            Dim PROG_BRANDPACK_ID As String = ""
            Dim X As Integer = 1

            If Me.chkCPMRT.Checked = True Then
                If Not Me.IsValidID() Then
                    Me.chkCPMRT.Checked = False
                    Return
                ElseIf Me.Mode = ModeSave.Update Then
                    PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                    If Me.mcbDistributor.Visible = True Then
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                    ElseIf Me.ChkDistributor.Visible = True Then
                        For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                            PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                            X += 1
                        Next
                    End If
                    If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                        Me.ShowMessageInfo("Please select distributor") : Me.chkCPMRT.Checked = False : Return
                    End If
                    If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "CT") = True Then
                        Me.txtGivenCPMRT.Enabled = False
                        Me.txtTargetCPMRT.Enabled = False
                        Me.chkCPMRT.Checked = Not Me.chkCPMRT.Checked
                        Return
                    Else
                        Me.txtGivenCPMRT.Enabled = True
                        Me.txtTargetCPMRT.Enabled = True
                        Me.txtGivenCPMRT.Value = 0
                        Me.txtTargetCPMRT.Value = 0
                    End If
                End If
                Me.mcbTMCPMRT.Focus()
                'check ke mcb apakah sudah di isi / belum
                If IsNothing(Me.mcbTMCPMRT.DataSource) OrElse Me.mcbTMCPMRT.DropDownList.RecordCount <= 0 Then
                    Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
                    Me.BindMultiColumnCombo(Me.mcbTMCPMRT, DV, "", "MANAGER", "SHIP_TO_ID")
                End If
                ''isi dengan data TM
                Me.mcbTMCPMRT.DroppedDown = True
                System.Threading.Thread.Sleep(200)
            ElseIf Me.Mode = ModeSave.Update Then
                'Me.mcbTMCPMRT.Text = "" : Me.mcbTMCPMRT.Value = Nothing
                PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.Value.ToString()
                If Me.mcbDistributor.Visible = True Then
                    PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.mcbDistributor.Value.ToString(), X)
                ElseIf Me.ChkDistributor.Visible = True Then
                    For I As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                        PROG_BRANDPACK_DIST_IDS.Add(PROG_BRANDPACK_ID + "" + Me.ChkDistributor.CheckedValues.GetValue(I).ToString(), X)
                        X += 1
                    Next
                End If
                If PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                    Me.ShowMessageInfo("Please select distributor") : Me.chkCPMRT.Checked = False : Return
                End If
                If Me.clsMRKT_BRANDPACK.HasgeneratedDiscount(PROG_BRANDPACK_DIST_IDS, "CT") = True Then
                    Me.txtGivenCPMRT.Enabled = False
                    Me.txtTargetCPMRT.Enabled = False
                    Me.chkCPMRT.Checked = Not Me.chkCPMRT.Checked
                    Return
                Else
                    Me.txtGivenCPMRT.Enabled = True
                    Me.txtTargetCPMRT.Enabled = True
                    Me.txtGivenCPMRT.Value = 0
                    Me.txtTargetCPMRT.Value = 0
                End If
                Me.mcbTMCPMRT.Focus()
                'check ke mcb apakah sudah di isi / belum
                If IsNothing(Me.mcbTMCPMRT.DataSource) OrElse Me.mcbTMCPMRT.DropDownList.RecordCount <= 0 Then
                    Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
                    Me.BindMultiColumnCombo(Me.mcbTMCPMRT, DV, "", "MANAGER", "SHIP_TO_ID")
                End If
                ''isi dengan data TM
                Me.mcbTMCPMRT.DroppedDown = True
                System.Threading.Thread.Sleep(200)
            Else
                Me.mcbTMCPMRT.Text = "" : Me.mcbTMCPMRT.Value = Nothing
            End If
            Me.isHasCheckedRefrence = True
            Me.SetTypeProgram()
            If Me.tbType.SelectedTab Is Me.tbCPMRT Then
                Me.tbType_SelectedTabChanged(Me.tbType, New DevComponents.DotNetBar.TabStripTabChangedEventArgs(Me.tbCPMRT, Me.tbCPMRT, DevComponents.DotNetBar.eEventSource.Mouse))
            Else
                Me.tbType.SelectedTab = Me.tbCPMRT
            End If
        Catch ex As Exception
            Me.chkCPMRT.Checked = Not Me.chkCPMRT.Checked
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkTargetDistributorPKPP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTargetDistributorPKPP.CheckedChanged
        'If Me.HasLoad Then
        '    If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        'End If

        'If Me.chkTargetDistributor.Checked = False Then
        '    'Me.mcbTM.ReadOnly = False
        '    Me.mcbTM.Focus()
        '    'check ke mcb apakah sudah di isi / belum
        '    If IsNothing(Me.mcbTM.DataSource) OrElse Me.mcbTM.DropDownList.RecordCount Then
        '        Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
        '        Me.BindMultiColumnCombo(Me.mcbTM, DV, "", "MANAGER", "SHIP_TO_ID")
        '    End If
        '    ''isi dengan data TM
        '    Me.mcbTM.DroppedDown = True
        '    System.Threading.Thread.Sleep(200)
        'Else
        '    Me.mcbTM.Value = Nothing
        '    'Me.mcbTM.ReadOnly = True
        '    Me.mcbTM.DroppedDown = False
        'End If
        If Not Me.HasLoad Then : Return : End If
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        If Me.chkTargetDistributorPKPP.Checked Then
            Me.mcbTMPKPP.Value = Nothing
            Me.mcbTMPKPP.Text = ""
            Me.mcbTMPKPP.DroppedDown = False
        Else
            Me.mcbTMPKPP.Focus()
            If IsNothing(Me.mcbTMPKPP.DataSource) OrElse Me.mcbTMPKPP.DropDownList.RecordCount <= 0 Then
                Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
                Me.BindMultiColumnCombo(Me.mcbTMPKPP, DV, "", "MANAGER", "SHIP_TO_ID")
            End If
            ''isi dengan data TM
            Me.mcbTMPKPP.DroppedDown = True
            System.Threading.Thread.Sleep(200)
        End If
    End Sub

    Private Sub chkTargetDistributorCPMRT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTargetDistributorCPMRT.Click
        If Not Me.HasLoad Then : Return : End If
        If Me.SFG = StateFillingGrid.Filling Then : Return : End If
        If Me.chkTargetDistributorCPMRT.Checked Then
            Me.mcbTMCPMRT.Value = Nothing
            Me.mcbTMCPMRT.Text = ""
            Me.mcbTMCPMRT.DroppedDown = False
        Else
            Me.mcbTMCPMRT.Focus()
            If IsNothing(Me.mcbTMCPMRT.DataSource) OrElse Me.mcbTMCPMRT.DropDownList.RecordCount <= 0 Then
                Dim DV As DataView = Me.clsMRKT_BRANDPACK.getTM(True)
                Me.BindMultiColumnCombo(Me.mcbTMCPMRT, DV, "", "MANAGER", "SHIP_TO_ID")
            End If
            Me.mcbTMCPMRT.DroppedDown = True
            System.Threading.Thread.Sleep(200)
        End If
    End Sub
End Class
