Public Class Program

#Region " Deklarasi "
    Private SelectGrid As GridSelect
    Private clsProgram As NufarmBussinesRules.Program.Core
    Private ProgramID As String
    Private PROG_BRANDPACK_ID As String
    Private HasLoad As Boolean
    Private BRANDPACK_ID As String
    Private SFG As StateFillingGrid
    Private DISTRIBUTOR_ID As String
    Private PROG_BRANDPACK_DIST_ID
    Friend CMain As Main = Nothing
    Friend MustReloadData As Boolean = False
    Private hasLoadGrid As Boolean = False
#End Region

#Region " enum "

    Private Enum GridSelect
        GridEx1
        GridEx2
        GridEx3
    End Enum

    Private Enum StateFillingGrid
        Filling
        HasFilled
    End Enum

#End Region

#Region " Sub "

    Private Sub ShowFrmEdit(ByVal keyParent As String, ByVal item As DevComponents.DotNetBar.ButtonItem)
        Select Case item.Name
            Case "btnEditBrandPackDistributor"
                Me.SFG = StateFillingGrid.Filling
                If Me.GridEX2.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter Then
                    While Not Me.GridEX2.GetRow().RowType = Janus.Windows.GridEX.RowType.Record
                        Me.GridEX2.MoveNext()
                    End While
                End If
                Dim ISCp As Boolean = False, isCPR As Boolean = False, isDK As Boolean = False, IsPKPP As Boolean = False, ISCPMRT As Boolean = False
                Dim IS_T_TM_DIST As Boolean = False
                Me.SFG = StateFillingGrid.HasFilled
                Me.BRANDPACK_ID = Me.GridEX2.GetValue("BRANDPACK_ID").ToString()
                Me.ProgramID = Me.GridEX2.GetValue("PROGRAM_ID").ToString()
                Me.DISTRIBUTOR_ID = Me.GridEX2.GetValue("DISTRIBUTOR_ID").ToString()
                Me.PROG_BRANDPACK_DIST_ID = Me.GridEX2.GetValue("PROG_BRANDPACK_DIST_ID")
                Dim frmDistInclude As New Distributor_Include()
                frmDistInclude.Mode = Distributor_Include.ModeSave.Update
                frmDistInclude.UM = Distributor_Include.UpdateMode.FromOriginal
                frmDistInclude.PROG_BRANDPACK_DIST_ID = Me.PROG_BRANDPACK_DIST_ID

                frmDistInclude.PROGRAM_ID = Me.ProgramID : frmDistInclude.PROG_BRANDPACK_ID = Me.PROG_BRANDPACK_ID
                frmDistInclude.InitializeData()
                frmDistInclude.mcbProgram.Value = Me.ProgramID
                frmDistInclude.mcbDistributor.Visible = True
                frmDistInclude.ChkDistributor.Visible = False
                frmDistInclude.mcbBrandPack.Value = Me.GridEX2.GetValue("BRANDPACK_ID").ToString()
                'frmDistInclude.EnabledControl()
                frmDistInclude.mcbDistributor.Value = Me.GridEX2.GetValue("DISTRIBUTOR_ID")
                frmDistInclude.txtGiven.Value = Me.GridEX2.GetValue("GIVEN %")
                frmDistInclude.txtTargetPCT.Value = Me.GridEX2.GetValue("TARGET %")
                frmDistInclude.txtTargetQTY.Value = Me.GridEX2.GetValue("TARGET_QTY")

                '=========GIVEN=====================

                frmDistInclude.txtGivenCP.Value = Me.GridEX2.GetValue("GIVEN_CP")
                frmDistInclude.txtGivenCPMRT.Value = Me.GridEX2.GetValue("GIVEN_CPMRT")
                frmDistInclude.txtGivenPKPP.Value = Me.GridEX2.GetValue("GIVEN_PKPP")
                frmDistInclude.txtGivenCPR.Value = Me.GridEX2.GetValue("GIVEN_CPR")
                frmDistInclude.txtGivenDK.Value = Me.GridEX2.GetValue("GIVEN_DK")

                'frmDistInclude.txtTargetPKPP.Value = Me.GridEX2.GetValue("TARGET_PKPP")
                frmDistInclude.txtBonusValuePKPP.Value = Me.GridEX2.GetValue("BONUS_VALUE")

                If Not IsNothing(Me.GridEX2.GetValue("DESCRIPTIONS")) And Not IsNothing(Me.GridEX2.GetValue("DESCRIPTIONS")) Then
                    frmDistInclude.txtDescriptions.Text = Me.GridEX2.GetValue("DESCRIPTIONS").ToString()
                Else
                    frmDistInclude.txtDescriptions.Text = String.Empty
                End If
                If Not IsDBNull(Me.GridEX2.GetValue("ISRPK")) Then
                    Dim isRPK As Boolean = CBool(Me.GridEX2.GetValue("ISRPK"))
                    frmDistInclude.chkRPK.Checked = isRPK
                    If isRPK Then
                        frmDistInclude.tbType.SelectedTabIndex = 0
                    End If
                Else
                    frmDistInclude.chkRPK.Checked = False
                End If
                '=========CPD===============================
                If Not IsDBNull(Me.GridEX2.GetValue("ISCP")) Then
                    ISCp = CBool(Me.GridEX2.GetValue("ISCP"))
                    frmDistInclude.chkCP.Checked = ISCp
                    If ISCp Then
                        frmDistInclude.tbType.SelectedTabIndex = 1
                        Dim CPDS As Boolean = False
                        If Not ((IsNothing(Me.GridEX2.GetValue("SCPD"))) And (IsDBNull(Me.GridEX2.GetValue("SCPD")))) Then
                            CPDS = CBool(Me.GridEX2.GetValue("SCPD"))
                        End If
                        frmDistInclude.chkSpesialDiscountCPD.Checked = CPDS
                        If Not ((IsNothing(Me.GridEX2.GetValue("IS_T_TM_DIST"))) And (IsDBNull(Me.GridEX2.GetValue("IS_T_TM_DIST")))) Then
                            IS_T_TM_DIST = CBool(Me.GridEX2.GetValue("IS_T_TM_DIST"))
                        End If
                        Dim ShipToID As Object = Nothing
                        If Not IsNothing(Me.GridEX2.GetValue("SHIP_TO_ID")) And Not IsDBNull(Me.GridEX2.GetValue("SHIP_TO_ID")) Then
                            If Not String.IsNullOrEmpty(Me.GridEX2.GetValue("SHIP_TO_ID")) Then
                                ShipToID = Me.GridEX2.GetValue("SHIP_TO_ID")
                            End If
                        End If
                        frmDistInclude.chkTargetDistributor.Checked = IS_T_TM_DIST
                        If Not IS_T_TM_DIST Then
                            'getdata source
                            Dim DV As DataView = clsProgram.getTM(False)
                            frmDistInclude.BindMultiColumnCombo(frmDistInclude.mcbTM, DV, "", "MANAGER", "SHIP_TO_ID")
                            frmDistInclude.mcbTM.Value = ShipToID
                        Else
                            frmDistInclude.mcbTM.Value = Nothing
                        End If
                    Else : frmDistInclude.chkCP.Checked = False
                    End If
                Else
                    frmDistInclude.chkCP.Checked = False
                End If
                '------------CPMRT==============================================
                If Not IsDBNull(Me.GridEX2.GetValue("ISCPMRT")) Then
                    ISCPMRT = CBool(Me.GridEX2.GetValue("ISCPMRT"))
                    frmDistInclude.chkCPMRT.Checked = ISCPMRT
                    If ISCPMRT Then
                        frmDistInclude.tbType.SelectedTabIndex = 2
                        If Not ((IsNothing(Me.GridEX2.GetValue("IS_T_TM_DIST"))) And (IsDBNull(Me.GridEX2.GetValue("IS_T_TM_DIST")))) Then
                            IS_T_TM_DIST = CBool(Me.GridEX2.GetValue("IS_T_TM_DIST"))
                        End If
                        Dim ShipToID As Object = Nothing
                        If Not IsNothing(Me.GridEX2.GetValue("SHIP_TO_ID")) And Not IsDBNull(Me.GridEX2.GetValue("SHIP_TO_ID")) Then
                            If Not String.IsNullOrEmpty(Me.GridEX2.GetValue("SHIP_TO_ID")) Then
                                ShipToID = Me.GridEX2.GetValue("SHIP_TO_ID")
                            End If
                        End If
                        frmDistInclude.chkTargetDistributorCPMRT.Checked = IS_T_TM_DIST
                        If Not IS_T_TM_DIST Then
                            Dim DV As DataView = clsProgram.getTM(False)
                            frmDistInclude.BindMultiColumnCombo(frmDistInclude.mcbTMCPMRT, DV, "", "MANAGER", "SHIP_TO_ID")
                            frmDistInclude.mcbTMCPMRT.Value = ShipToID
                        Else
                            frmDistInclude.mcbTMCPMRT.Value = Nothing
                        End If
                    Else : frmDistInclude.chkCPMRT.Checked = False
                    End If
                Else : frmDistInclude.chkCPMRT.Checked = False
                End If
                '====================PKPP=============================================
                If Not IsDBNull(Me.GridEX2.GetValue("ISPKPP")) Then
                    IsPKPP = CBool(Me.GridEX2.GetValue("ISPKPP"))
                    frmDistInclude.chkPKPP.Checked = IsPKPP
                    IS_T_TM_DIST = False
                    If Not ((IsNothing(Me.GridEX2.GetValue("IS_T_TM_DIST"))) And (IsDBNull(Me.GridEX2.GetValue("IS_T_TM_DIST")))) Then
                        IS_T_TM_DIST = CBool(Me.GridEX2.GetValue("IS_T_TM_DIST"))
                    End If
                    If (Convert.ToDecimal(Me.GridEX2.GetValue("BONUS_VALUE")) > 0) Then
                        frmDistInclude.txtTargetPKPP.Value = 0 : frmDistInclude.txtTargetValuePKPP.Value = Me.GridEX2.GetValue("TARGET_PKPP")
                    Else
                        frmDistInclude.txtTargetPKPP.Value = Me.GridEX2.GetValue("TARGET_PKPP") : frmDistInclude.txtTargetValuePKPP.Value = 0
                    End If
                    If IsPKPP Then
                        frmDistInclude.tbType.SelectedTabIndex = 3
                    End If
                    Dim ShipToID As Object = Nothing
                    If Not IsNothing(Me.GridEX2.GetValue("SHIP_TO_ID")) And Not IsDBNull(Me.GridEX2.GetValue("SHIP_TO_ID")) Then
                        If Not String.IsNullOrEmpty(Me.GridEX2.GetValue("SHIP_TO_ID")) Then
                            ShipToID = Me.GridEX2.GetValue("SHIP_TO_ID")
                        End If
                    End If
                    frmDistInclude.chkTargetDistributor.Checked = IS_T_TM_DIST
                    If Not IS_T_TM_DIST Then
                        Dim DV As DataView = clsProgram.getTM(False)
                        frmDistInclude.BindMultiColumnCombo(frmDistInclude.mcbTMPKPP, DV, "", "MANAGER", "SHIP_TO_ID")
                        frmDistInclude.mcbTMPKPP.Value = ShipToID
                    Else
                        frmDistInclude.mcbTMPKPP.Value = Nothing
                    End If
                Else
                    frmDistInclude.chkPKPP.Checked = False
                End If

                '================CPRD===========================================
                If Not IsDBNull(Me.GridEX2.GetValue("ISCPR")) Then
                    isCPR = CBool(Me.GridEX2.GetValue("ISCPR"))
                    frmDistInclude.chkCPR.Checked = isCPR
                    If isCPR Then
                        frmDistInclude.tbType.SelectedTabIndex = 4
                    End If
                Else
                    frmDistInclude.chkCPR.Checked = False
                End If

                '==================DK=======================
                If Not IsDBNull(Me.GridEX2.GetValue("ISDK")) Then
                    isDK = CBool(Me.GridEX2.GetValue("ISDK"))
                    frmDistInclude.chkDK.Checked = isDK
                    If isDK Then
                        frmDistInclude.tbType.SelectedTabIndex = 5
                    End If
                Else
                    frmDistInclude.chkDK.Checked = False
                End If

                '=============HK=====================================
                Dim isHK As Boolean = CBool(Me.GridEX2.GetValue("ISHK"))
                If Not IsDBNull(Me.GridEX2.GetValue("ISHK")) Then
                    frmDistInclude.chkHK.Checked = isHK 'CBool(Me.GridEX2.GetValue("ISHK"))
                    If isHK Then
                        frmDistInclude.tbType.SelectedTabIndex = 5
                    End If
                Else
                    frmDistInclude.chkHK.Checked = False
                End If
                frmDistInclude.txtTargetCPR.Value = Me.GridEX2.GetValue("TARGET_CPR")
                frmDistInclude.txtTargetCP.Value = Me.GridEX2.GetValue("TARGET_CP")
                frmDistInclude.txtTargetCPMRT.Value = Me.GridEX2.GetValue("TARGET_CPMRT")
                frmDistInclude.txtTargetDK.Value = Me.GridEX2.GetValue("TARGET_Dk")
                frmDistInclude.txtTargetHK.Value = Me.GridEX2.GetValue("TARGET_HK")

                frmDistInclude.txtPrice.Value = Me.GridEX2.GetValue("PRICE_HK")

                frmDistInclude.dtPicStart.IsNullDate = False
                frmDistInclude.dtPicEnd.IsNullDate = False
                frmDistInclude.dtPicStart.Value = Convert.ToDateTime(Me.GridEX2.GetValue("START_DATE"))
                'frmDistInclude.dtPicStart.MinDate = Convert.ToDateTime(Me.GridEX2.GetValue("START_DATE"))
                frmDistInclude.dtPicStart.MaxDate = Convert.ToDateTime(Me.GridEX2.GetValue("END_DATE"))
                frmDistInclude.dtPicStart.Value = Convert.ToDateTime(Me.GridEX2.GetValue("START_DATE"))
                If Not IsDBNull(Me.GridEX2.GetValue("ISHK")) Then
                    If CBool(Me.GridEX2.GetValue("ISHK")) = True Then
                        frmDistInclude.dtPicEnd.ReadOnly = False
                        frmDistInclude.txtPrice.Enabled = True
                        'set maxdate untuk dtpic end
                        Dim EndDateAgreement As Date = Me.clsProgram.GetAgreementEndDate(Me.GridEX2.GetValue("DISTRIBUTOR_ID").ToString(), Me.GridEX2.GetValue("BRANDPACK_ID").ToString())
                        'enabledkan bagian tab price saja
                        frmDistInclude.tbType.Enabled = True
                        'check po terakhir
                        Dim StartDateString As String = Convert.ToDateTime(Me.GridEX2.GetValue("START_DATE")).Month.ToString() & "/" & _
                        Convert.ToDateTime(Me.GridEX2.GetValue("START_DATE")).Day.ToString() & "/" & Convert.ToDateTime(Me.GridEX2.GetValue("START_DATE")).Year.ToString()
                        Dim LastPODate As Object = Me.clsProgram.getlastPODate(Me.GridEX2.GetValue("DISTRIBUTOR_ID").ToString(), Me.GridEX2.GetValue("BRANDPACK_ID").ToString(), _
                        StartDateString)
                        If Not IsNothing(LastPODate) Then
                            frmDistInclude.dtPicEnd.MinDate = Convert.ToDateTime(LastPODate)
                        Else
                            frmDistInclude.dtPicEnd.MinDate = Convert.ToDateTime(Me.GridEX2.GetValue("START_DATE"))
                        End If

                        'frmDistInclude.dtPicEnd.MinDate = Convert.ToDateTime(Me.GridEX2.GetValue("START_DATE"))
                        frmDistInclude.dtPicEnd.MaxDate = EndDateAgreement
                        frmDistInclude.dtPicEnd.Value = Convert.ToDateTime(Me.GridEX2.GetValue("END_DATE"))
                        frmDistInclude.ButtonEntry1.btnInsert.Enabled = True
                        frmDistInclude.ButtonEntry1.btnInsert.Text = "&Update"
                        'frmDistInclude.dtPicEnd.MaxDate = 
                    Else
                        'frmDistInclude.dtPicEnd.ReadOnly = True
                        frmDistInclude.txtPrice.Enabled = False
                        frmDistInclude.txtTargetHK.Enabled = True
                        frmDistInclude.dtPicEnd.MinDate = Convert.ToDateTime(Me.GridEX2.GetValue("START_DATE"))
                        'frmDistInclude.dtPicEnd.MaxDate = Convert.ToDateTime(Me.GridEX2.GetValue("END_DATE"))
                        frmDistInclude.dtPicEnd.Value = Convert.ToDateTime(Me.GridEX2.GetValue("END_DATE"))
                    End If
                Else
                    'frmDistInclude.dtPicEnd.ReadOnly = True
                    frmDistInclude.txtPrice.Enabled = False
                    frmDistInclude.txtTargetHK.Enabled = True
                    frmDistInclude.dtPicEnd.MinDate = Convert.ToDateTime(Me.GridEX2.GetValue("START_DATE"))
                    'frmDistInclude.dtPicEnd.MaxDate = Convert.ToDateTime(Me.GridEX2.GetValue("END_DATE"))
                    frmDistInclude.dtPicEnd.Value = Convert.ToDateTime(Me.GridEX2.GetValue("END_DATE"))
                End If

                If CBool(Me.GridEX2.GetValue("STEPPING")) = True Then
                    frmDistInclude.chkStepping.Checked = True
                Else
                    frmDistInclude.chkStepping.Checked = False
                End If
                frmDistInclude.mcbProgram.ReadOnly = True
                frmDistInclude.mcbBrandPack.ReadOnly = True
                frmDistInclude.ChkDistributor.Visible = False
                frmDistInclude.mcbDistributor.Visible = True
                frmDistInclude.mcbDistributor.ReadOnly = True
                Dim ProgBrandPackDistID As String = Me.GridEX2.GetValue("PROG_BRANDPACK_DIST_ID").ToString()
                Using clsDistInclude As New NufarmBussinesRules.Program.BrandPackInclude()
                    Dim c As Boolean = clsDistInclude.HasReferencedDataMRKT_BRANDPCK_DISTRIBUTOR(ProgBrandPackDistID)
                    If ISCp Then
                        'get progbrandpack dist_id
                        ''karena di haruskan
                        frmDistInclude.txtGivenCP.ReadOnly = c
                        frmDistInclude.chkCP.Enabled = Not c
                        frmDistInclude.txtTargetCP.ReadOnly = c
                        frmDistInclude.chkSpesialDiscountCPD.Enabled = Not c
                        frmDistInclude.chkTargetDistributor.Enabled = Not c
                        frmDistInclude.mcbTM.ReadOnly = True
                    End If
                    If ISCPMRT Then

                        frmDistInclude.txtTargetCPMRT.ReadOnly = c
                        frmDistInclude.txtGivenCPMRT.ReadOnly = c
                        frmDistInclude.chkCPMRT.Enabled = Not c
                        frmDistInclude.chkTargetDistributorCPMRT.Enabled = Not c
                        frmDistInclude.mcbTMCPMRT.ReadOnly = c
                    End If
                    If isCPR Then
                        frmDistInclude.txtTargetCPR.ReadOnly = c
                        frmDistInclude.txtGivenCPR.ReadOnly = c
                        frmDistInclude.chkCPR.Enabled = Not c
                    End If
                    If IsPKPP Then
                        'txtGivenPKPP()
                        'txtTargetPKPP()
                        'mcbTMPKPP()
                        'chkTargetDistributorPKPP()
                        'txtBonusValuePKPP()
                        'txtTargetValuePKPP()
                        frmDistInclude.chkPKPP.Enabled = Not c
                        frmDistInclude.txtBonusValuePKPP.ReadOnly = c
                        frmDistInclude.txtGivenPKPP.ReadOnly = c
                        frmDistInclude.txtTargetPKPP.ReadOnly = c
                        frmDistInclude.txtTargetValuePKPP.ReadOnly = c
                        frmDistInclude.chkTargetDistributorPKPP.Enabled = Not c
                        frmDistInclude.mcbTMPKPP.ReadOnly = c
                    End If
                    If isDK Then
                        frmDistInclude.txtTargetDK.ReadOnly = c
                        frmDistInclude.txtGivenDK.ReadOnly = c
                        frmDistInclude.chkDK.Enabled = Not c
                    End If
                 
                End Using
                frmDistInclude.ButtonEntry1.btnAddNew.Enabled = False
                frmDistInclude.ParentFrm = Me
                frmDistInclude.ShowDialog(Me)
                If MustReloadData Then : Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs()) : End If
            Case "btnEditMarketingBrandPack"
                Select Case Me.SelectGrid
                    Case GridSelect.GridEx1
                        If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                            If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.GroupHeader Then
                                Me.SFG = StateFillingGrid.Filling
                                While Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record
                                    Me.GridEX1.MoveNext()
                                End While
                                Me.SFG = StateFillingGrid.HasFilled
                            Else
                                Me.ShowMessageInfo("Please select ProgramID") : Return
                            End If
                        End If
                        If Me.GridEX1.GetValue("PROGRAM_ID") Is DBNull.Value Then
                            Me.ShowMessageInfo("Please select ProgramID") : Return
                        End If
                        If Me.GridEX1.GetValue("PROG_BRANDPACK_ID") Is DBNull.Value Then
                            Me.ShowMessageInfo("can not edit data " & vbCrLf & "Because brandpack is null/empty.") : Return
                        End If
                        Me.ProgramID = Me.GridEX1.GetValue("PROGRAM_ID").ToString()
                        Me.PROG_BRANDPACK_ID = Me.GridEX1.GetValue("PROG_BRANDPACK_ID").ToString()
                        Me.BRANDPACK_ID = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
                    Case GridSelect.GridEx2
                        If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                            If Me.GridEX2.CurrentRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Then
                                If Me.GridEX2.CurrentRow().Group.Column Is Me.GridEX2.RootTable.Columns("BRANDPACK_NAME") Then
                                    Me.SFG = StateFillingGrid.Filling
                                    While Not Me.GridEX2.GetRow().RowType = Janus.Windows.GridEX.RowType.Record
                                        Me.GridEX2.MoveNext()
                                    End While
                                    Me.SFG = StateFillingGrid.HasFilled
                                    Me.BRANDPACK_ID = Me.GridEX2.GetValue("BRANDPACK_ID")
                                    Me.PROG_BRANDPACK_ID = Me.GridEX2.GetValue("PROG_BRANDPACK_ID")
                                    Me.ProgramID = Me.GridEX2.GetValue("PROGRAM_ID")
                                Else
                                    Me.ShowMessageInfo("Please select BrandPack & Program") : Return
                                End If
                            End If
                        End If
                    Case GridSelect.GridEx3
                        Return
                End Select
                Dim frmProgBrandPack As New Program_BrandPack()
                frmProgBrandPack.Mode = Program_BrandPack.ModeSave.Update
                frmProgBrandPack.UM = Program_BrandPack.UpdateMode.FromOriginal
                frmProgBrandPack.PROG_BRANDPACK_ID = Me.PROG_BRANDPACK_ID
                frmProgBrandPack.BRANDPACK_ID = Me.BRANDPACK_ID
                frmProgBrandPack.ProgramID = Me.ProgramID
                frmProgBrandPack.mcbProgram.Value = Me.ProgramID
                frmProgBrandPack.mcbOriginalBrandPack.Value = Me.BRANDPACK_ID
                frmProgBrandPack.OriginalEndDateFromBrandPack = Convert.ToDateTime(Me.GridEX1.GetValue("BRANDPACK_END_DATE"))
                frmProgBrandPack.OriginalStartDateFromBrandPack = Convert.ToDateTime(Me.GridEX1.GetValue("BRANDPACK_START_DATE"))
                frmProgBrandPack.InitializeData()
                frmProgBrandPack.ParentFrm = Me
                frmProgBrandPack.ShowDialog(Me)
                If MustReloadData Then : Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs()) : End If
                'Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs())
            Case "btnEditMarketingProgram"
                'edit item marketing program
                Dim frmMRKT As New Marketing_Program()
                'frmMRKT.txtProgramName.Text = Me.GridEX1.GetValue("PROGRAM_NAME").ToString()
                'frmMRKT.dtPicStart.Value = Me.GridEX1.GetValue("START_DATE")
                'frmMRKT.dtPicEnd.Value = Me.GridEX1.GetValue("END_DATE")
                frmMRKT.Mode = Marketing_Program.ModeSave.Update
                frmMRKT.UpdateMode = Marketing_Program.ModeUpdate.FromOriginal
                Me.SFG = StateFillingGrid.Filling
                While Not Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record
                    Me.GridEX1.MoveNext()
                End While
                Me.SFG = StateFillingGrid.HasFilled
                frmMRKT.OriginalDate = Convert.ToDateTime(Me.GridEX1.GetValue("END_DATE"))
                frmMRKT.txtProgramID.Text = keyParent
                frmMRKT.InitializeData()
                frmMRKT.ParentFrm = Me
                frmMRKT.ShowDialog(Me)
                If MustReloadData Then : Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs()) : End If
                'Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs())
            Case "btnEditStepingDiscount"
                Dim frmStepping As New Stepping_Discount()
                frmStepping.Mode = Stepping_Discount.ModeSave.Update
                frmStepping.PROG_BRANDPACK_DIST_ID = keyParent
                frmStepping.InitializeData()
                frmStepping.MultiColumnCombo3.DroppedDown = True
                frmStepping.MultiColumnCombo3.Value = Nothing
                frmStepping.MultiColumnCombo3.Value = keyParent
                frmStepping.MultiColumnCombo3.DroppedDown = False
                If Convert.ToDateTime(Me.GridEX2.GetValue("END_DATE")) < NufarmBussinesRules.SharedClass.ServerDate() Then
                    frmStepping.MultiColumnCombo3.Enabled = False
                    frmStepping.GridEX1.Enabled = False
                    frmStepping.ButtonEntry1.btnInsert.Enabled = False
                    frmStepping.ButtonEntry1.btnAddNew.Enabled = False
                End If
                frmStepping.ShowDialog(Me)
                Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs())
        End Select
    End Sub

    Private Sub GetEnabledButtonBarEdit(ByVal item As DevComponents.DotNetBar.ButtonItem)
        Select Case item.Name
            Case "btnEditBrandPackDistributor"
                Me.btnEditBrandPackDistributor.Enabled = True
                Me.btnEditMarketingBrandPack.Enabled = False
                Me.btnEditMarketingProgram.Enabled = False
                Me.btnEditStepingDiscount.Enabled = False
            Case "btnEditMarketingBrandPack"
                Me.btnEditMarketingBrandPack.Enabled = True
                Me.btnEditBrandPackDistributor.Enabled = False
                Me.btnEditMarketingProgram.Enabled = False
                Me.btnEditStepingDiscount.Enabled = False
            Case "btnEditMarketingProgram"
                Me.btnEditMarketingProgram.Enabled = True
                Me.btnEditBrandPackDistributor.Enabled = False
                Me.btnEditMarketingBrandPack.Enabled = False
                Me.btnEditStepingDiscount.Enabled = False
            Case "btnEditStepingDiscount"
                Me.btnEditStepingDiscount.Enabled = True
                Me.btnEditBrandPackDistributor.Enabled = False
                Me.btnEditMarketingBrandPack.Enabled = False
                Me.btnEditMarketingProgram.Enabled = False
        End Select
    End Sub

    Private Sub GetEnabledButtonBarEdit()
        Me.btnEditBrandPackDistributor.Enabled = True
        Me.btnEditMarketingBrandPack.Enabled = True
        Me.btnEditMarketingProgram.Enabled = True
        Me.btnEditStepingDiscount.Enabled = True
    End Sub

    Private Sub SetUnabledEditButtonBar()
        Me.btnEditBrandPackDistributor.Enabled = False
        Me.btnEditMarketingBrandPack.Enabled = False
        Me.btnEditMarketingProgram.Enabled = False
        Me.btnEditStepingDiscount.Enabled = False
    End Sub

    Friend Sub InitializeData()
        Me.HasLoad = False
        Me.RefreshData()
    End Sub

    Private Sub SetUnabledAddButtonBar()
        Me.btnBrandPackDistributor.Enabled = False
        Me.btnMarketingBrandPack.Enabled = False
        Me.btnMarketingProgram.Enabled = False
        Me.btnStepingDiscount.Enabled = False
    End Sub

    Private Sub GetEnabledAddButtonBar()
        Me.btnBrandPackDistributor.Enabled = True
        Me.btnMarketingBrandPack.Enabled = True
        Me.btnMarketingProgram.Enabled = True
        Me.btnStepingDiscount.Enabled = True
    End Sub

    Private Sub RefreshData()
        If IsNothing(Me.clsProgram) Then
            Me.clsProgram = New NufarmBussinesRules.Program.Core()
        End If
        Me.dtPicFrom.Value = DateTime.Now
        Me.dtPicUntil.Value = DateTime.Now
        Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs())
        'Me.clsProgram = New NufarmBussinesRules.Program.Core()
        Me.clsProgram.CreateViewStepping()
        'If Me.UiCheckBox1.Checked = False Then
        '    Me.clsProgram.ViewProgramBrandPack().RowFilter = "END_DATE >= " & NufarmBussinesRules.SharedClass.ServerDateString()
        '    Me.clsProgram.ViewDistributorSteppingDiscount().RowFilter = "END_DATE >= " & NufarmBussinesRules.SharedClass.ServerDateString()
        '    Me.clsProgram.ViewStepping().RowFilter = "END_DATE >= " & NufarmBussinesRules.SharedClass.ServerDateString()
        'Else
        '    Me.clsProgram.ViewProgramBrandPack().RowFilter = ""
        '    'Me.clsProgram.ViewDistributorSteppingDiscount().RowFilter = ""
        '    'Me.clsProgram.ViewStepping().RowFilter = ""
        'End If
        'Me.Bindgrid(Me.GridEX1, Me.clsProgram.ViewProgramBrandPack())
        'Me.Bindgrid(Me.GridEX2, Me.clsProgram.ViewDistributorSteppingDiscount())
        'Me.Bindgrid(Me.GridEX3, Me.clsProgram.ViewStepping())
    End Sub

    Private Sub AddConditionalFormatingGRIDEX1()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.GridEX1.RootTable.Columns("END_DATE"), Janus.Windows.GridEX.ConditionOperator.LessThan, CObj(NufarmBussinesRules.SharedClass.ServerDate()))
        fc.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
        fc.FormatStyle.ForeColor = SystemColors.GrayText
        Me.GridEX1.RootTable.FormatConditions.Add(fc)

    End Sub

    Private Sub AddCOnditionalFormattingGridEx2()
        Dim fc1 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.GridEX2.RootTable.Columns("END_DATE"), Janus.Windows.GridEX.ConditionOperator.LessThan, CObj(NufarmBussinesRules.SharedClass.ServerDate()))
        fc1.FormatStyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
        fc1.FormatStyle.ForeColor = SystemColors.GrayText
        Me.GridEX2.RootTable.FormatConditions.Add(fc1)
    End Sub

    Private Sub Bindgrid(ByVal grd As Janus.Windows.GridEX.GridEX, ByVal dsSource As Object)
        Me.SFG = StateFillingGrid.Filling
        grd.SetDataBinding(dsSource, "")
        If IsNothing(dsSource) Then : Me.SFG = StateFillingGrid.HasFilled : Return : End If
        If Not Me.hasLoadGrid Then
            Select Case grd.Name
                Case "GridEX1"
                    Me.AddConditionalFormatingGRIDEX1()

                Case "GridEX2"
                    Me.AddCOnditionalFormattingGridEx2()
            End Select
        End If
        'If Not IsNothing(dsSource) Then
        '    grd.ExpandGroups()
        'End If
        Me.hasLoadGrid = True
        Me.SFG = StateFillingGrid.HasFilled
    End Sub

    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Program = True Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Marketing_Program = True Then
                Me.btnMarketingProgram.Visible = True
            Else
                Me.btnMarketingProgram.Visible = False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Marketing_Program = True Then
                Me.btnEditMarketingProgram.Visible = True
            Else
                Me.btnEditMarketingProgram.Visible = False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Program_BrandPack = True Then
                Me.btnMarketingBrandPack.Visible = True
            Else
                Me.btnMarketingBrandPack.Visible = False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Program_BrandPack = True Then
                Me.btnEditMarketingBrandPack.Visible = True
            Else
                Me.btnEditMarketingBrandPack.Visible = False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Distributor_Include = True Then
                Me.btnBrandPackDistributor.Visible = True
            Else
                Me.btnBrandPackDistributor.Visible = False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Distributor_Include = True Then
                Me.btnEditBrandPackDistributor.Visible = True
            Else
                Me.btnEditBrandPackDistributor.Visible = False
            End If
            'If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Stepping_Discount = True Then
            '    Me.btnStepingDiscount.Visible = True
            'Else
            '    Me.btnStepingDiscount.Visible = False
            'End If
            'If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Stepping_Discount = True Then
            '    Me.btnEditStepingDiscount.Visible = True
            'Else
            '    Me.btnEditStepingDiscount.Visible = False
            'End If
            Me.btnEditStepingDiscount.Visible = False
            If Me.btnAddNew.VisibleSubItems <= 0 Then
                Me.btnAddNew.Visible = False
            Else
                Me.btnAddNew.Visible = True
            End If
            If Me.btnEdit.VisibleSubItems <= 0 Then
                Me.btnEdit.Visible = False
            Else
                Me.btnEdit.Visible = True
            End If
        End If

    End Sub

#End Region

#Region " Event Procedure "

#Region " Event Form "

    Private Sub Program_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not IsNothing(Me.clsProgram) Then
                Me.clsProgram.Dispose(True)
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Program_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ExpandableSplitter1.Expanded = False
            Me.ExpandableSplitter2.Expanded = False
            Me.FilterEditor1.Visible = False
            'Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs())
        Catch ex As Exception
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : MessageBox.Show(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Program_Load")
        Finally
            Me.HasLoad = True
            Me.ReadAcces()
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " GridEx1 "

#Region " GridEx1 "

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.HasLoad = False Then
                Return
            End If
            If Me.SFG = StateFillingGrid.Filling Then
                Return
            End If
            Me.btnMarketingBrandPack.Enabled = True
            Me.btnMarketingProgram.Enabled = True
            Me.btnBrandPackDistributor.Enabled = False
            'Me.btnStepingDiscount.Enabled = False
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Then
                If Me.GridEX1.GetRow().Group.Column Is Me.GridEX1.RootTable.Columns("PROGRAM_ID") Then
                    Me.ProgramID = Me.GridEX1.GetRow().GroupCaption
                    Me.BRANDPACK_ID = ""
                    Me.GetEnabledButtonBarEdit(Me.btnEditMarketingProgram)
                    Me.SFG = StateFillingGrid.Filling
                    Me.clsProgram.ViewDistributorSteppingDiscount().RowFilter = "PROGRAM_ID = '" & Me.GridEX1.GetRow().GroupCaption + "'"
                    If Me.clsProgram.ViewDistributorSteppingDiscount().Count > 0 Then
                        Me.ExpandableSplitter1.Expanded = True
                    Else
                        Me.ExpandableSplitter1.Expanded = False
                    End If
                    Me.Bindgrid(Me.GridEX2, Me.clsProgram.ViewDistributorSteppingDiscount())
                    Me.SFG = StateFillingGrid.HasFilled
                    Me.XpCaption1.Text = "BrandPack (with Distributor) detail from PROGRAM_ID : " + Me.ProgramID
                Else
                    Me.SetUnabledEditButtonBar()
                    Me.ExpandableSplitter1.Expanded = False
                End If
            ElseIf Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                If Not IsNothing(Me.GridEX1.GetValue("BRANDPACK_ID")) Then
                    If Me.GridEX1.GetValue("PROG_BRANDPACK_ID") Is DBNull.Value Then
                        Me.BRANDPACK_ID = ""
                        Me.PROG_BRANDPACK_ID = ""
                        Me.Bindgrid(Me.GridEX2, Nothing)
                        Me.SetUnabledEditButtonBar()
                        Me.ExpandableSplitter1.Expanded = False
                        Me.ProgramID = Me.GridEX1.GetValue("PROGRAM_ID").ToString()
                        Me.GetEnabledButtonBarEdit(Me.btnEditMarketingProgram)
                    Else
                        Me.SFG = StateFillingGrid.Filling
                        Me.PROG_BRANDPACK_ID = Me.GridEX1.GetValue("PROG_BRANDPACK_ID").ToString()
                        Me.BRANDPACK_ID = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
                        Me.ProgramID = Me.GridEX1.GetValue("PROGRAM_ID").ToString()
                        Me.clsProgram.ViewDistributorSteppingDiscount().RowFilter = "PROG_BRANDPACK_ID  = '" + Me.PROG_BRANDPACK_ID + "'"
                        If Me.clsProgram.ViewDistributorSteppingDiscount().Count > 0 Then
                            Me.ExpandableSplitter1.Expanded = True
                        Else
                            Me.ExpandableSplitter1.Expanded = False
                        End If
                        Me.Bindgrid(Me.GridEX2, Me.clsProgram.ViewDistributorSteppingDiscount())
                        Me.SFG = StateFillingGrid.HasFilled

                        'Me.Bindgrid(Me.GridEX2, Me.clsProgram.ViewDistributorSteppingDiscount())
                        Me.XpCaption1.Text = "BrandPack (with Distributor detail) FROM PROGRAM_ID : " + Me.GridEX1.GetValue("PROGRAM_ID").ToString() + " And BrandPack_Name : " + Me.GridEX1.GetValue("BRANDPACK_NAME").ToString()
                        Me.GetEnabledButtonBarEdit(Me.btnEditMarketingBrandPack)
                    End If
                Else
                    Me.BRANDPACK_ID = ""
                    Me.PROG_BRANDPACK_ID = ""
                    Me.Bindgrid(Me.GridEX2, Nothing)
                    Me.SetUnabledEditButtonBar()
                    Me.ExpandableSplitter1.Expanded = False
                    Me.ProgramID = Me.GridEX1.GetValue("PROGRAM_ID").ToString()
                    Me.GetEnabledButtonBarEdit(Me.btnEditMarketingProgram)
                End If
                'Me.ProgramID = Me.GridEX1.GetValue("PROGRAM_ID").ToString()
                'Me.GetEnabledButtonBarEdit(Me.btnEditMarketingProgram)
            Else
                Me.SetUnabledEditButtonBar()
                Me.PROG_BRANDPACK_ID = ""
                Me.ProgramID = ""
                Me.BRANDPACK_ID = ""
                Me.ExpandableSplitter1.Expanded = False
                Me.XpCaption1.Text = ""
                Me.Bindgrid(Me.GridEX2, Nothing)
            End If
            Me.ExpandableSplitter2.Expanded = False
            Me.Bindgrid(Me.GridEX3, Nothing)
            'me.clsProgram.ViewStepping().
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_CurrentCellChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.Enter
        Try
            Me.grpProgBrandPack.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.ExplorerBarBackground
            Me.grpProgBrandPack.Text = "Active"
            Me.grpDistributor.Text = ""
            Me.grpDistributor.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.Default
            Me.XpCaption2.Active = True
            Me.XpCaption1.Active = False
            Me.btnEditBrandPackDistributor.Enabled = False
            Me.btnEditStepingDiscount.Enabled = False
            Me.btnEditMarketingBrandPack.Enabled = True
            Me.btnEditMarketingProgram.Enabled = True

            Me.btnBrandPackDistributor.Enabled = False
            Me.btnStepingDiscount.Enabled = False
            Me.btnMarketingBrandPack.Enabled = True
            Me.btnMarketingProgram.Enabled = True
            Me.SelectGrid = GridSelect.GridEx1
            Dim S As Boolean = Me.FilterEditor1.Visible
            Me.FilterEditor1.SourceControl = Me.GridEX1
            Me.FilterEditor1.Visible = S
            Me.GridEX1.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX1.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX1.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
            Me.GridEX1.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
            Me.GridEX1.BackColor = Color.FromArgb(158, 190, 245)

            Me.GridEX2.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX2.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX2.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX2.BackColor = Color.FromArgb(194, 217, 247)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                If (Me.GridEX1.GetValue("PROG_BRANDPACK_ID") Is Nothing) Or (Me.GridEX1.GetValue("PROG_BRANDPACK_ID") Is DBNull.Value) Then
                    If Me.clsProgram.ProgramHasReferencedData(Me.GridEX1.GetValue("PROGRAM_ID")) = True Then
                        e.Cancel = True
                        Me.ShowMessageInfo(Me.MessageCantDeleteData)
                        'Me.GridEX1.Refetch()
                        'Me.GridEX1.SelectCurrentCellText()
                        Return
                    ElseIf Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                        e.Cancel = True
                        'Me.GridEX1.Refetch()
                        'Me.GridEX1.SelectCurrentCellText()
                        Return
                    Else
                        Me.clsProgram.DeleteProgram(Me.GridEX1.GetValue("PROGRAM_ID"))
                        Me.ShowMessageInfo(Me.MessageSuccesDelete)
                        e.Cancel = False : Me.GridEX1.UpdateData()
                        Return
                    End If
                End If
                Me.Cursor = Cursors.WaitCursor
                If Me.clsProgram.HasReferencedData(Me.GridEX1.GetValue("PROG_BRANDPACK_ID")) = True Then
                    e.Cancel = True
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    'Me.GridEX1.Refetch()
                    'Me.GridEX1.SelectCurrentCellText()
                ElseIf Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True
                    'Me.GridEX1.Refetch()
                    'Me.GridEX1.SelectCurrentCellText()
                Else
                    Me.Cursor = Cursors.WaitCursor
                    Dim cnt As Integer = Me.GridEX1.GetTotal(Me.GridEX1.RootTable.Columns("PROGRAM_ID"), _
                                      Janus.Windows.GridEX.AggregateFunction.ValueCount, _
                                      New Janus.Windows.GridEX.GridEXFilterCondition(Me.GridEX1.RootTable.Columns("PROGRAM_ID"), Janus.Windows.GridEX.ConditionOperator.Equal, _
                                      Me.GridEX1.GetValue("PROGRAM_ID"), Me.GridEX1.GetValue("PROGRAM_ID")))
                    Me.clsProgram.DeletePROG_BRANDPACK(Me.GridEX1.GetValue("PROG_BRANDPACK_ID").ToString())
                    If cnt > 1 Then
                        e.Cancel = False : Me.GridEX1.UpdateData()
                    Else
                        Me.btnAplyrange_Click(Me.btnAplyrange, New EventArgs())
                    End If
                    Me.ShowMessageInfo(Me.MessageSuccesDelete)
                    'Me.btnAplyrange_Click(Me.btnAplyrange, New EventArgs())
                    'If Not IsNothing(Me.GridEX1.DataSource) Then
                    '    Me.GridEX1.ExpandGroups()
                    'End If
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub GridEX1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.Cursor = Cursors.WaitCursor
                If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Then
                    If Me.GridEX1.GetRow().Group.Column Is Me.GridEX1.RootTable.Columns("PROGRAM_ID") Then
                        Dim x As String = Me.GridEX1.GetRow().GroupCaption
                        If Me.clsProgram.ProgramHasReferencedData(x) = True Then
                            Me.ShowMessageInfo(Me.MessageCantDeleteData)
                            'Me.GridEX1.Refetch()
                            'Me.GridEX1.SelectCurrentCellText()
                            Return
                        ElseIf Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                            'Me.GridEX1.Refetch()
                            'Me.GridEX1.SelectCurrentCellText()
                            Return
                        Else
                            Me.Cursor = Cursors.WaitCursor
                            Me.clsProgram.DeleteProgram(x)
                            Me.ShowMessageInfo(Me.MessageSuccesDelete)
                            Me.btnAplyrange_Click(Me.btnAplyrange, New EventArgs())
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_KeyDown")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " GRIDEX2 "

    Private Sub GridEX2_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX2.CurrentCellChanged
        Try
            If Me.HasLoad = False Then
                Return
            End If
            If Me.SFG = StateFillingGrid.Filling Then
                Return
            End If
            'Me.btnMarketingBrandPack.Enabled = True
            'Me.btnMarketingProgram.Enabled = True
            'Me.btnBrandPackDistributor.Enabled = False
            'Me.btnStepingDiscount.Enabled = False
            If Me.GridEX2.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader Then
                If Me.GridEX2.GetRow().Group.Column Is Me.GridEX2.RootTable.Columns("DISTRIBUTOR_NAME") Then
                    Me.GetEnabledButtonBarEdit(Me.btnEditBrandPackDistributor)
                    'Me.SetUnabledEditButtonBar()
                Else
                    Me.SetUnabledEditButtonBar()
                    'Me.BRANDPACK_ID = ""
                    'ElseIf Me.GridEX2.CurrentRow().Group.Column Is Me.GridEX2.RootTable.Columns("BRANDPACK_NAME") Then
                    '    Me.GetEnabledButtonBarEdit(Me.btnEditMarketingBrandPack)
                End If
                'If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then

                'End If
                Me.BRANDPACK_ID = ""
                Me.ExpandableSplitter2.Expanded = False
            ElseIf Me.GridEX2.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.ProgramID = Me.GridEX2.GetValue("PROGRAM_ID").ToString()
                Me.BRANDPACK_ID = Me.GridEX2.GetValue("BRANDPACK_ID")
                Me.DISTRIBUTOR_ID = Me.GridEX2.GetValue("DISTRIBUTOR_ID")
                Me.PROG_BRANDPACK_DIST_ID = Me.GridEX2.GetValue("PROG_BRANDPACK_DIST_ID").ToString()
                Me.PROG_BRANDPACK_ID = Me.GridEX2.GetValue("PROG_BRANDPACK_ID")
                Me.GetEnabledButtonBarEdit(Me.btnEditBrandPackDistributor)
                'If Not IsDBNull(Me.GridEX2.GetValue("STEPPING")) Then
                '    If CBool(Me.GridEX2.GetValue("STEPPING")) = True Then
                '        Me.GetEnabledButtonBarEdit(Me.btnEditStepingDiscount)
                '        Me.ProgramID = Me.GridEX2.GetValue("PROGRAM_ID").ToString()
                '        Me.BRANDPACK_ID = Me.GridEX2.GetValue("BRANDPACK_ID")
                '        Me.DISTRIBUTOR_ID = Me.GridEX2.GetValue("DISTRIBUTOR_ID")
                '        Me.PROG_BRANDPACK_DIST_ID = Me.GridEX2.GetValue("PROG_BRANDPACK_DIST_ID").ToString()
                '        Me.clsProgram.ViewStepping().RowFilter = "PROG_BRANDPACK_DIST_ID = '" & PROG_BRANDPACK_DIST_ID & "'"
                '        If Me.clsProgram.ViewStepping().Count > 0 Then
                '            Me.ExpandableSplitter2.Expanded = True
                '        Else
                '            Me.ExpandableSplitter2.Expanded = False
                '        End If
                '        Me.Bindgrid(Me.GridEX3, Me.clsProgram.ViewStepping())
                '    Else
                '        Me.ProgramID = Me.GridEX2.GetValue("PROGRAM_ID").ToString()
                '        Me.BRANDPACK_ID = Me.GridEX2.GetValue("BRANDPACK_ID")
                '        Me.SetUnabledEditButtonBar()
                '        Me.Bindgrid(Me.GridEX3, Nothing)
                '        Me.ExpandableSplitter2.Expanded = False
                '    End If
                'Else
                '    Me.ProgramID = Me.GridEX2.GetValue("PROGRAM_ID").ToString()
                '    Me.BRANDPACK_ID = Me.GridEX2.GetValue("BRANDPACK_ID")
                '    Me.SetUnabledEditButtonBar()
                '    Me.Bindgrid(Me.GridEX3, Nothing)
                '    Me.ExpandableSplitter2.Expanded = False
                'End If
            Else
                Me.BRANDPACK_ID = ""
                Me.SetUnabledEditButtonBar()
                Me.ExpandableSplitter2.Expanded = False
            End If
            Me.btnBrandPackDistributor.Enabled = True
            'Me.btnStepingDiscount.Enabled = True
            Me.btnMarketingProgram.Enabled = False
            Me.btnMarketingBrandPack.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX2.Enter
        Try
            Me.grpProgBrandPack.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.Default
            Me.grpProgBrandPack.Text = ""
            Me.grpDistributor.Text = "Active"
            Me.grpDistributor.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.ExplorerBarBackground
            Me.XpCaption1.Active = True
            Me.XpCaption2.Active = False
            Me.btnEditBrandPackDistributor.Enabled = True
            Me.btnEditStepingDiscount.Enabled = True
            Me.btnEditMarketingBrandPack.Enabled = False
            Me.btnEditMarketingProgram.Enabled = False

            Me.btnMarketingProgram.Enabled = False
            Me.btnMarketingBrandPack.Enabled = False
            Me.btnStepingDiscount.Enabled = True
            Me.btnBrandPackDistributor.Enabled = True

            Me.GridEX2.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX2.RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX2.FilterRowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
            Me.GridEX2.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
            Me.GridEX2.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
            Me.GridEX2.BackColor = Color.FromArgb(158, 190, 245)

            Me.GridEX1.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX1.RowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX1.FilterRowFormatStyle.BackColor = Color.FromArgb(194, 217, 247)
            Me.GridEX1.BackColor = Color.FromArgb(194, 217, 247)

            Me.SelectGrid = GridSelect.GridEx2
            Dim S As Boolean = Me.FilterEditor1.Visible
            Me.FilterEditor1.SourceControl = Me.GridEX2
            Me.FilterEditor1.Visible = S
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX2_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX2.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                If Me.clsProgram.HasReferencedDataMRKT_BRANDPCK_DISTRIBUTOR(Me.GridEX2.GetValue("PROG_BRANDPACK_DIST_ID")) = True Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    e.Cancel = True
                    Me.GridEX2.Refetch()
                    Return
                End If
                If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True
                    Me.GridEX2.Refetch()
                    Return
                End If
                Me.clsProgram.DeletePROG_BRANDPACK_DIST_ID(Me.GridEX2.GetValue("PROG_BRANDPACK_DIST_ID").ToString())
                Me.ShowMessageInfo(Me.MessageSuccesDelete)
                e.Cancel = False
                Me.GridEX2.UpdateData()
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX3_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX3.Enter
        Try
            Me.grpDistributor.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.Default
            Me.grpProgBrandPack.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.Default
            Me.grpDistributor.Text = ""
            Me.grpProgBrandPack.Text = ""
            Me.XpCaption1.Active = False
            Me.XpCaption2.Active = False
            Me.SelectGrid = GridSelect.GridEx3
            Me.SetUnabledEditButtonBar()
            Me.SetUnabledAddButtonBar()
        Catch ex As Exception

        End Try

    End Sub

#End Region

#End Region

#Region " Bar "

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnRenameColumn"
                    Select Case Me.SelectGrid
                        Case GridSelect.GridEx1
                            Dim MC As New ManipulateColumn()
                            MC.ShowInTaskbar = False
                            MC.grid = Me.GridEX1
                            MC.FillcomboColumn()
                            MC.ManipulateColumnName = "Rename"
                            MC.TopMost = True
                            MC.Show(Me.Bar1, True)
                        Case GridSelect.GridEx2
                            Dim MC As New ManipulateColumn()
                            MC.ShowInTaskbar = False
                            MC.grid = Me.GridEX2
                            MC.FillcomboColumn()
                            MC.ManipulateColumnName = "Rename"
                            MC.TopMost = True
                            MC.Show(Me.Bar3, True)
                    End Select
                Case "btnShowFieldChooser"
                    Select Case Me.SelectGrid
                        Case GridSelect.GridEx1
                            Me.GridEX1.ShowFieldChooser(Me)
                        Case GridSelect.GridEx2
                            Me.GridEX2.ShowFieldChooser(Me)
                    End Select
                Case "btnSettingGrid"
                    Select Case Me.SelectGrid
                        Case GridSelect.GridEx1
                            Dim SetGrid As New SettingGrid()
                            SetGrid.Grid = Me.GridEX1
                            SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                            SetGrid.ShowDialog(Me)
                        Case GridSelect.GridEx2
                            Dim SetGrid As New SettingGrid()
                            SetGrid.Grid = Me.GridEX2
                            SetGrid.GridExPrintDock = Me.GridEXPrintDocument2
                            SetGrid.ShowDialog(Me)
                        Case GridSelect.GridEx3
                            Return
                    End Select

                Case "btnPrint"
                    Select Case Me.SelectGrid
                        Case GridSelect.GridEx1
                            Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                            Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                            'Me.PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                            If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                                Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                            End If
                            If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Me.PrintPreviewDialog1.Document.Print()
                            End If
                        Case GridSelect.GridEx2
                            Me.GridEXPrintDocument2.GridEX = Me.GridEX2
                            Me.PrintPreviewDialog2.Document = Me.GridEXPrintDocument2
                            'Me.PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                            If Not IsNothing(Me.PageSetupDialog2.PageSettings) Then
                                Me.PrintPreviewDialog2.Document.DefaultPageSettings = Me.PageSetupDialog2.PageSettings
                            End If
                            If Me.PrintPreviewDialog2.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Me.PrintPreviewDialog2.Document.Print()
                            End If
                        Case GridSelect.GridEx3
                            Me.GridEXPrintDocument2.GridEX = Me.GridEX3
                            Me.PrintPreviewDialog2.Document = Me.GridEXPrintDocument2
                            If Not IsNothing(Me.PageSetupDialog2.PageSettings) Then
                                Me.PrintPreviewDialog2.Document.DefaultPageSettings = Me.PageSetupDialog2.PageSettings
                            End If
                            If Me.PrintPreviewDialog2.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Me.PrintPreviewDialog2.Document.Print()
                            End If
                    End Select
                Case "btnPageSettings"
                    Select Case Me.SelectGrid
                        Case GridSelect.GridEx1
                            Me.PageSetupDialog1.ShowDialog(Me)
                        Case GridSelect.GridEx2
                            Me.PageSetupDialog2.ShowDialog(Me)
                    End Select
                Case "btnCustomFilter"
                    Select Case Me.SelectGrid
                        Case GridSelect.GridEx1
                            Me.FilterEditor1.SourceControl = Me.GridEX1
                            Me.GridEX1.RemoveFilters()
                            Me.FilterEditor1.Visible = True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                        Case GridSelect.GridEx2
                            Me.FilterEditor1.SourceControl = Me.GridEX2
                            Me.GridEX2.RemoveFilters()
                            Me.FilterEditor1.Visible = True
                            Me.GridEX2.FilterMode = Janus.Windows.GridEX.FilterMode.None
                        Case GridSelect.GridEx3
                            Return
                    End Select

                Case "btnFilterEqual"
                    Select Case Me.SelectGrid
                        Case GridSelect.GridEx1
                            Me.FilterEditor1.Visible = False
                            Me.GridEX1.RemoveFilters()
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case GridSelect.GridEx2
                            Me.FilterEditor1.Visible = False
                            Me.GridEX2.RemoveFilters()
                            Me.GridEX2.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case GridSelect.GridEx3
                            Return
                    End Select
                Case "btnMarketingProgram"
                    Dim frmMRKT As New Marketing_Program() : frmMRKT.Mode = Marketing_Program.ModeSave.Save
                    frmMRKT.FirstLoad = True : frmMRKT.InitializeData()
                    frmMRKT.ParentFrm = Me
                    frmMRKT.ShowDialog(Me) : If MustReloadData Then : Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs()) : End If
                    Me.SFG = StateFillingGrid.Filling : Me.GridEX1.ExpandGroups() : Me.SFG = StateFillingGrid.HasFilled
                Case "btnMarketingBrandPack"
                    Dim frmMRKTBP As New Program_BrandPack()
                    Dim Index As Integer = -1
                    If Me.ProgramID <> "" Then
                        If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                            Me.SFG = StateFillingGrid.Filling
                            While Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record
                                Me.GridEX1.MoveNext()
                                If Not IsDBNull(Me.GridEX1.GetValue("PROGRAM_ID")) And Not IsNothing(Me.GridEX1.GetValue("PROGRAM_ID")) Then
                                    If Me.GridEX1.GetValue("PROGRAM_ID").ToString() = Me.ProgramID Then
                                        Index = Me.GridEX1.Row
                                        Exit While
                                    End If
                                End If

                            End While
                            Me.SFG = StateFillingGrid.HasFilled
                        End If
                        'frmMRKTBP.mcbProgram.Value = Me.ProgramID
                    End If
                    With frmMRKTBP
                        .Mode = Program_BrandPack.ModeSave.Save
                        If Me.ProgramID <> "" Then
                            .ProgramID = Me.ProgramID
                            Me.SFG = StateFillingGrid.Filling
                            While Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record
                                Me.GridEX1.MoveNext()
                                If Not IsDBNull(Me.GridEX1.GetValue("PROGRAM_ID")) And Not IsNothing(Me.GridEX1.GetValue("PROGRAM_ID")) Then
                                    If Me.GridEX1.GetValue("PROGRAM_ID").ToString() = Me.ProgramID Then
                                        Index = Me.GridEX1.Row
                                        Exit While
                                    End If
                                End If
                            End While
                            Me.SFG = StateFillingGrid.HasFilled
                            .dtPicStart.Value = Convert.ToDateTime(Me.GridEX1.GetValue("START_DATE"))
                            .dtPicEnd.Value = Convert.ToDateTime(Me.GridEX1.GetValue("END_DATE"))
                            .dtPicStart.MinDate = Convert.ToDateTime(Me.GridEX1.GetValue("START_DATE"))
                            .dtPicStart.MaxDate = Convert.ToDateTime(Me.GridEX1.GetValue("END_DATE"))
                            .dtPicEnd.MinDate = Convert.ToDateTime(Me.GridEX1.GetValue("START_DATE"))
                            .dtPicEnd.MaxDate = Convert.ToDateTime(Me.GridEX1.GetValue("END_DATE"))
                        End If
                        .ParentFrm = Me
                        .InitializeData()
                        .ShowDialog(Me)
                    End With
                    Me.SFG = StateFillingGrid.Filling : If MustReloadData Then : Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs()) : End If
                    Me.GridEX1.Row = Index : Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
                    Me.SFG = StateFillingGrid.HasFilled
                Case "btnBrandPackDistributor"
                    Dim Index As Integer = -1
                    Dim frmDistInclude As New Distributor_Include()
                    If Not Me.GridEX2.DataSource Is Nothing Then
                        If Me.GridEX2.RecordCount > 0 Then
                            If (Not Me.GridEX2.GetRow.RowType = Janus.Windows.GridEX.RowType.FilterRow) _
                                And (Not Me.GridEX2.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter) _
                                And (Not Me.GridEX2.GetRow.RowType = Janus.Windows.GridEX.RowType.TotalRow) Then
                                Me.SFG = StateFillingGrid.Filling
                                While Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record
                                    Me.GridEX1.MoveNext()
                                    If Not IsNothing(Me.GridEX2.GetValue("PROGRAM_ID")) And Not IsDBNull(Me.GridEX1.GetValue("PROGRAM_ID")) Then
                                        Me.ProgramID = Me.GridEX1.GetValue("PROGRAM_ID").ToString()
                                        Exit While
                                    End If
                                End While
                                Me.SFG = StateFillingGrid.HasFilled
                            End If
                        End If
                    End If
                    With frmDistInclude
                        .ParentFrm = Me
                        .Mode = Distributor_Include.ModeSave.Save
                        .PROGRAM_ID = Me.ProgramID
                        .BRANDPACK_ID = Me.BRANDPACK_ID
                        .InitializeData()
                        .ShowDialog(Me)
                    End With
                    If MustReloadData Then : Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs()) : End If
                Case "btnStepingDiscount"
                    Dim frmStepDisc As New Stepping_Discount()
                    frmStepDisc.InitializeData()
                    frmStepDisc.ShowDialog(Me)
                    Me.SFG = StateFillingGrid.Filling
                    Me.RefreshData()
                    Me.GridEX1.Refresh()
                    Me.GridEX2.Refresh()
                    Me.SFG = StateFillingGrid.HasFilled
                Case "btnExport"
                    Select Case Me.SelectGrid
                        Case GridSelect.GridEx1
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.GridEX1
                                Me.GridEXExporter1.SheetName = "Marketing BrandPack Detail"
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Case GridSelect.GridEx2
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.GridEX2
                                Me.GridEXExporter1.SheetName = "Marketing Distributor"
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Case GridSelect.GridEx3
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.GridEX3
                                Me.GridEXExporter1.SheetName = "Distributor Stepping Discount"
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                    End Select
                Case "btnEditMarketingProgram"
                    If Me.ProgramID = "" Then
                        Return
                    End If
                    Me.ShowFrmEdit(Me.ProgramID, Me.btnEditMarketingProgram)
                Case "btnEditMarketingBrandPack"
                    If Me.PROG_BRANDPACK_ID = "" Then
                        Return
                    End If
                    Me.ShowFrmEdit(Me.PROG_BRANDPACK_ID, Me.btnEditMarketingBrandPack)
                Case "btnEditBrandPackDistributor"
                    Me.ShowFrmEdit("", Me.btnEditBrandPackDistributor)
                Case "btnEditStepingDiscount"
                    If Me.PROG_BRANDPACK_DIST_ID = "" Then
                        Return
                    End If
                    Me.ShowFrmEdit(Me.PROG_BRANDPACK_DIST_ID, Me.btnEditStepingDiscount)
                Case "btnRefresh"
                    Me.btnAplyrange_Click(Me.btnAplyrange, New System.EventArgs())
            End Select
        Catch ex As Exception

        Finally
            MustReloadData = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnAplyrange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyrange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ExpandableSplitter1.Expanded = False
            'CStr("'" & Month(Convert.ToDateTime(AGREEMENT_START_DATE)).ToString() & "/" & Day(Convert.ToDateTime(AGREEMENT_START_DATE)).ToString() & "/" & Year(Convert.ToDateTime(AGREEMENT_START_DATE)).ToString() & "'")
            If (Me.dtPicFrom.Text <> "") And (Me.dtPicUntil.Text <> "") Then
                'Dim start_date As String = CStr("'" & Month(Me.dtPicFrom.Value).ToString() & "/" & DateAndTime.Day(Me.dtPicFrom.Value).ToString() & _
                '"/" & Year(dtPicFrom.Value).ToString() & "'")
                'Dim End_Date As String = CStr("'" & Month(dtPicUntil.Value).ToString() & "/" & DateAndTime.Day(dtPicUntil.Value).ToString() & _
                '"/" & Year(dtPicUntil.Value).ToString() & "'")
                Me.clsProgram.CreateViewProgramBrandPack_1(False, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
                Me.clsProgram.CreateViewDistributorSteppingDiscount_1(Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            ElseIf (Me.dtPicFrom.Text <> "") And (Me.dtPicUntil.Text = "") Then
                'Dim start_date As String = CStr("'" & Month(Me.dtPicFrom.Value).ToString() & "/" & DateAndTime.Day(Me.dtPicFrom.Value).ToString() & _
                '"/" & Year(dtPicFrom.Value).ToString() & "'")
                Me.clsProgram.CreateViewProgramBrandPack_1(False, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Nothing)
                Me.clsProgram.CreateViewDistributorSteppingDiscount_1(Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Nothing)
            ElseIf (Me.dtPicFrom.Text = "") And (Me.dtPicUntil.Text <> "") Then
                'Dim End_Date As String = CStr("'" & Month(dtPicUntil.Value).ToString() & "/" & DateAndTime.Day(dtPicUntil.Value).ToString() & _
                '"/" & Year(dtPicUntil.Value).ToString() & "'")
                Me.clsProgram.CreateViewProgramBrandPack_1(False, Nothing, Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
                Me.clsProgram.CreateViewDistributorSteppingDiscount_1(Nothing, Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()))
            Else
                Me.clsProgram.CreateViewProgramBrandPack_1(False, Nothing, Nothing)
                Me.clsProgram.CreateViewDistributorSteppingDiscount_1(Nothing, Nothing)
                Me.ShowMessageInfo("You don't define start and end program" & vbCrLf & "System only view data(s) whose apply until now")
            End If
            Me.UiCheckBox1_CheckedChanged(Me.UiCheckBox1, New EventArgs())
            Me.Bindgrid(Me.GridEX1, Me.clsProgram.ViewProgramBrandPack())
            Me.Bindgrid(Me.GridEX2, Me.clsProgram.ViewDistributorSteppingDiscount())
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnAplyrange_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " CheckedBox "

    Private Sub UiCheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiCheckBox1.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.UiCheckBox1.Checked = False Then
                Me.clsProgram.ViewProgramBrandPack().RowFilter = "END_DATE >= " & NufarmBussinesRules.SharedClass.ServerDateString()
                Me.clsProgram.ViewDistributorSteppingDiscount().RowFilter = "END_DATE >= " & NufarmBussinesRules.SharedClass.ServerDateString()
                Me.clsProgram.ViewStepping().RowFilter = "END_DATE >= " & NufarmBussinesRules.SharedClass.ServerDateString()
            Else
                Me.clsProgram.ViewProgramBrandPack().RowFilter = ""
                'Me.clsProgram.ViewDistributorSteppingDiscount().RowFilter = ""
                'Me.clsProgram.ViewStepping().RowFilter = ""
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#End Region

    Private Sub dtPicFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicFrom.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.dtPicFrom.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtPicUntil_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtPicUntil.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Me.dtPicUntil.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
