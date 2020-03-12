Imports System.Diagnostics
Imports System.Globalization
Imports System.Threading
Imports System.Configuration.Configuration
Public Class Main

#Region "Deklarasi"
    Private frmBrandPack As BrandPackHistory : Private frmDistributor As Distributor
    Private frmDHistory As DistributorHistory : Private FrmActive As System.Windows.Forms.Form
    Private HoldLoadForm As Int16 : Private frmPack As Pack : Private frmBrand As Brand
    Private frmBrandpackHistory As BrandPackHistory : Private frmbtnDistributorRegistering As Distributor
    Private frmRegion As Region : Private frmAgreement As Agreement
    Private frmPurchaseOrder As PO : Private frmProgram As Program : Private frmAcceptance As Acceptance
    Private frmManageUser As User : Private frmDiscount As DiscAgreement
    Private frmReport As ReportGrid ' Private frmReportByCrystal As frmReport
    Private SMSRemainding As SMS : Private O_SMS As Other_SMS : Private CPO As Cancel_PO
    Private frmSPPB As SPPB 'Private frmPODO As ThirdParty
    Private frmShipTo As Ship_To ' Private frmCrReport As frmCrystalReport
    Private frmSendData As FrmSendingData : Private frmDPRD As frmManageDPRD
    Private frmPricePlantation As PlantationPrice
    Private frmPriceHistory As PriceHistory
    Private TickCount As Integer = 0, frmInvoice As Invoice
    Private frmAchievement As AchievementDPD
    Private frmOptions As Settings
    Private frmCompareProduct As CompareBrandPackAccpack
    Private frmPlantation As PlantationManager
    Private frmProgress As Progress
    Private frmProject As Project
    Private frmtransporter As Transporter
    Private frmAreaGON As AreaGON
    Private frmClassification As Classification
    Private frmProdClassification As ProductClassified
    Private frmDKN As DKNational
    Private frmAVGPrice As AVGPrice
    Private frmCPDAuto As CPDAuto
    Private frmAdjustmentPKD As AjdustmentPKD
    Private frmDiscDDorDR As DiscountDDOrDR
    Private frmAchievementR As AchievementF
#End Region

    Private ThreadProgress As Thread = Nothing
    Friend StatProg As StatusProgress = StatusProgress.None
    Friend FormLoading As StatusForm
    Private HasCheckDataReminder As Boolean = False
    Private hasDataReminder As Boolean = False
    Friend Enum StatusForm
        None
        Loading
        HasLoaded
    End Enum
    Friend Enum StatusProgress
        None
        Processing
    End Enum
    Private Sub tmrHoldShowForm_tick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrHoldShowForm.Elapsed
        Me.HoldLoadForm += 1
        Application.DoEvents()
        If Me.HoldLoadForm >= 2 Then
            If Me.FormLoading = StatusForm.Loading Then
                Me.HoldLoadForm = 0 : Me.tmrHoldShowForm.Stop() : Me.tmrHoldShowForm.Enabled = False
                'Me.FormLoading = StatusForm.None
                Me.ShowThread()
            ElseIf Me.FormLoading = StatusForm.HasLoaded Then
                Me.HoldLoadForm = 0 : Me.tmrHoldShowForm.Stop() : Me.tmrHoldShowForm.Enabled = False
            End If
        End If
    End Sub

    Private Sub ShowProceed()
        Me.frmProgress = New Progress() : Me.frmProgress.Show("Gathering Resources.....") : Me.frmProgress.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Me.frmProgress.Refresh() : Thread.Sleep(50) : Application.DoEvents()
        End While
        Thread.Sleep(50)
        If Not IsNothing(Me.frmProgress) Then
            Me.frmProgress.Close() : Me.frmProgress = Nothing
        End If
    End Sub
    Protected Sub LogMyEvent(ByVal Message As String, ByVal NamaEvent As String)
        Try
            If Not EventLog.SourceExists("AppException") Then
                EventLog.CreateEventSource("AppException", "Nufarm")
            End If
            EventLog.WriteEntry("AppException", "Date  " + System.DateTime.Now.ToShortDateString() + ": On Hour " + DateTime.Now.Hour.ToString() & _
            ":" & DateTime.Now.Minute.ToString() + " Error = " + Message + " Event = " + NamaEvent, EventLogEntryType.Error)
        Catch ex As Exception

        End Try
    End Sub

    Private Function IsHasPrivilege(ByVal FORM_NAME As String) As Boolean
        Select Case FORM_NAME
            Case "Distributor"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Distributor
            Case "Agreement"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Agreement
            Case "DistributorHistory"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DistributorHistory
            Case "Region"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Region
            Case "DiscAgreement"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DiscAgreement
            Case "Program"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Program
            Case "Acceptance"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Acceptance
            Case "PO"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PO
            Case "Brand"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.BRAND
            Case "BRANDPACK_PRICEHISTORY"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.BRANDPACK_PRICEHISTORY
            Case "Pack"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PACK
            Case "ReportGrid"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.ReportGrid
            Case "frmReport"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.frmReport
            Case "Other_SMS"
                Return NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Other_SMS
            Case "SMS"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SMS
            Case "Cancel_PO"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Cancel_PO
            Case "PODOThirdParty"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PODOThirdParty
            Case "SPPB"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SPPB
            Case "SHIP_TO"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SHIP_TO
            Case "frmManageDPRD"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.frmManageDPRD
            Case "FrmSendingData"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.FrmSendingData
            Case "PlantationPrice"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PlantationPrice
            Case "PriceHistory"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PriceHistory
            Case "Achievement"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Achievement
            Case "Invoice"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Invoice
            Case "Options"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Options
            Case "Project"
                Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Project
                'btnGONArea
                'btnTransporter
            Case "AreaGON" : Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AreaGON
            Case "Transporter" : Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Transporter
            Case "SPPBEntryGON" : Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SPPBEntryGON
            Case "Classification" : Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Classification
            Case "ProductClassified" : Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.ProductClassified
            Case "DKNational" : Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DKNational
            Case "AVGPrice" : Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AVGPrice
            Case "CPDAuto" : Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.CPDAuto
            Case "AjdustmentPKD" : Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AdjustmentPKD
            Case "DiscountDDorDR" : Return NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DiscDDorDR

        End Select
    End Function

    Private Sub getCommonPriviledge()
        Me.btnDistributorRegistering.Visible = Me.IsHasPrivilege("Distributor")
        Me.btnAgreement.Visible = Me.IsHasPrivilege("Agreement")
        Me.btnDistributorHistory.Visible = Me.IsHasPrivilege("DistributorHistory")
        Me.btnDistributorRegion.Visible = Me.IsHasPrivilege("Region")
        'Me.btnDiscAgreement.Visible = Me.IsHasPrivilege("DiscAgreement")
        Me.btnProgram.Visible = Me.IsHasPrivilege("Program")
        Me.btnOrderAcceptance.Visible = Me.IsHasPrivilege("Acceptance")
        Me.btnPO.Visible = Me.IsHasPrivilege("PO")
        Me.btnBrand.Visible = Me.IsHasPrivilege("Brand")
        Me.btnBrandPackItem.Visible = Me.IsHasPrivilege("BRANDPACK_PRICEHISTORY")
        Me.btnPack.Visible = Me.IsHasPrivilege("Pack")
        Me.btnByGrid.Visible = Me.IsHasPrivilege("ReportGrid")
        Me.btnByCrystalReport.Visible = Me.IsHasPrivilege("frmReport")
        Me.btnOtherSMS.Visible = Me.IsHasPrivilege("Other_SMS")
        Me.btnSMSReminding.Visible = Me.IsHasPrivilege("SMS")
        Me.btncancelPO.Visible = Me.IsHasPrivilege("Cancel_PO")
        Me.btnSendingData.Visible = Me.IsHasPrivilege("FrmSendingData")
        Me.btnManageDPRD.Visible = False 'Me.IsHasPrivilege("frmManageDPRD")
        Me.btnSPPB.Visible = Me.IsHasPrivilege("SPPB")
        Me.btnPODO.Visible = Me.IsHasPrivilege("PODOThirdParty")
        Me.btnShipToTM.Visible = Me.IsHasPrivilege("SHIP_TO")
        Me.btnAchievement.Visible = Me.IsHasPrivilege("Achievement")
        Me.btnInvoice.Visible = Me.IsHasPrivilege("Invoice")
        Me.btnOptions.Visible = Me.IsHasPrivilege("Options")
        Me.btnPriceHistory.Visible = Me.IsHasPrivilege("PriceHistory")
        Me.btnBrandPackPlantation.Visible = Me.IsHasPrivilege("PlantationPrice")
        Me.btnPlantationBrandPack.Visible = Me.IsHasPrivilege("PlantationPrice")
        Me.btnProject.Visible = Me.IsHasPrivilege("Project")
        Me.btnGONArea.Visible = Me.IsHasPrivilege("AreaGON")
        Me.btnTransporter.Visible = Me.IsHasPrivilege("Transporter")
        Me.btnClasification.Visible = Me.IsHasPrivilege("Classification")
        Me.btnProductClass.Visible = Me.IsHasPrivilege("ProductClassified")
        Me.btnDKNational.Visible = Me.IsHasPrivilege("DKNational")
        Me.btnAVGPrice.Visible = Me.IsHasPrivilege("AVGPrice")
        Me.btnCPDAuto.Visible = Me.IsHasPrivilege("CPDAuto")
        Me.btnAjdustmentPKD.Visible = Me.IsHasPrivilege("AjdustmentPKD")
        Me.btnDiscDDAndDR.Visible = Me.IsHasPrivilege("DiscountDDorDR")
        Me.btnAchievementDPDR.Visible = Me.IsHasPrivilege("Achievement")
        Me.btnCompareBrandPack.Visible = False
        Me.btnManageUser.Visible = False
    End Sub
    Private Sub ReadAcces()
        'header menu
        Me.btnBrandPack.Visible = True : Me.btnDistributor.Visible = True : Me.btnSales.Visible = True
        Me.btnOrder.Visible = True : Me.btnGenerate.Visible = True : Me.btnReport.Visible = True
        Me.btnSetting.Visible = True
        Me.btnChangePassword.Visible = True
        If Me.IsSystemAdministrator Then
            Me.btnSetting.Visible = True
            Me.btnCompareBrandPack.Visible = True
            Me.btnLogIn.Visible = False : Me.btnLogout.Visible = False
            Me.btnChangePassword.Visible = False
            Me.btnProject.Visible = True
            Me.btncancelPO.Visible = True : btnManageUser.Visible = True
            Me.btnDistributorRegistering.Visible = True : Me.btnAgreement.Visible = True
            Me.btnDistributorHistory.Visible = True : Me.btnDistributorRegion.Visible = True
            Me.btnProgram.Visible = True
            Me.btnOrderAcceptance.Visible = True : Me.btnPO.Visible = True : Me.btnBrand.Visible = True
            Me.btnBrandPackItem.Visible = True : Me.btnPack.Visible = True
            Me.btnReport.Visible = True : Me.btnSMSReminding.Visible = True
            Me.btnOtherSMS.Visible = True : Me.btnShipToTM.Visible = True
            Me.btnPODO.Visible = True : Me.btnSPPB.Visible = True
            Me.btnSendingData.Visible = True
            Me.btnPriceHistory.Visible = True : Me.btnBrandPackPlantation.Visible = True
            Me.btnAchievement.Visible = True : Me.btnInvoice.Visible = True
            Me.btnByGrid.Visible = True : Me.btnByCrystalReport.Visible = True
            Me.btnOptions.Visible = True
            Me.btnTransporter.Visible = True : Me.btnGONArea.Visible = True
            Me.btnClasification.Visible = True : Me.btnProductClass.Visible = True
            Me.btnDKNational.Visible = True
            Me.btnAVGPrice.Visible = True
            Me.btnCPDAuto.Visible = True
            Me.btnAjdustmentPKD.Visible = True
            Me.btnManageUser.Visible = False
            Me.btnDiscDDAndDR.Visible = True
            Me.btnAchievementDPDR.Visible = True
        ElseIf NufarmBussinesRules.User.UserLogin.IsAdmin Then ' ITSupport
            Me.getCommonPriviledge()
            Me.btnSetting.Visible = True
            Me.btnCompareBrandPack.Visible = True
            Me.btnLogIn.Visible = False : Me.btnLogout.Visible = True
            Me.btnOptions.Visible = True
            Me.btnManageUser.Visible = True
            Me.btnDiscDDAndDR.Visible = True
        Else
            btnSetting.Visible = False : Me.btnLogIn.Visible = False
            Me.btnLogout.Visible = True
            Me.getCommonPriviledge()
        End If
        Me.btnByCrystalReport.Visible = False
        Me.btnBrandPack.Visible = Not Me.btnBrandPack.VisibleSubItems <= 0
        Me.btnDistributor.Visible = Not Me.btnDistributor.VisibleSubItems <= 0
        Me.btnSales.Visible = Not Me.btnSales.VisibleSubItems <= 0
        Me.btnOrder.Visible = Not Me.btnOrder.VisibleSubItems <= 0
        Me.btnGenerate.Visible = Not Me.btnGenerate.VisibleSubItems <= 0
        Me.btnReport.Visible = Not Me.btnReport.VisibleSubItems <= 0
        Me.btnSMS.Visible = Not Me.btnSMS.VisibleSubItems <= 0
        If btnSetting.Visible Then
            Me.btnSetting.Visible = Not Me.btnSetting.VisibleSubItems <= 0
        End If
    End Sub

    Private Sub DestroyForm()
        Me.frmBrandPack = Nothing : Me.frmPack = Nothing
        Me.frmBrand = Nothing
        For Each frm As System.Windows.Forms.Form In Me.MdiChildren
            If Not IsNothing(frm) Then
                If Not frm.IsDisposed() Then
                    frm.Dispose()
                End If
                frm = Nothing
            End If
        Next
        Me.FrmActive = Nothing
        Me.HoldLoadForm = 0 : Me.FormLoading = StatusForm.None : Me.StatProg = StatusProgress.None : Me.tmrHoldShowForm.Enabled = False
        Me.Timer1.Enabled = False : Me.Timer2.Enabled = False
    End Sub
    Private Sub DOLogin()
        Dim frmLogin As New Login() : frmLogin.ShowInTaskbar = False
        Me.Hide() : frmLogin.ShowDialog()
        Me.ReadAcces()
        'check apakah punya acces priviledge ke Reminder
        If NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Reminder = True Then
            'If Not Me.tmrReminder.Enabled Then
            '    Dim clsProgram As New NufarmBussinesRules.Program.BrandPackInclude()
            '    If clsProgram.hasDataReminder() Then
            '        'check registry
            '        Dim rg As New NufarmBussinesRules.SettingDTS.RegUser()
            '        Dim SRT As Object = rg.Read("StartReminderTime")
            '        Dim SRN As Object = rg.Read("StartReminderName")
            '        Dim ER As Object = rg.Read("EndReminder")
            '        'define interval snooze
            '        Dim SnoozeInterval = rg.Read("Reminder")
            '        If Not IsNothing(SRT) And Not IsNothing(SRN) And Not IsNothing(ER) Then
            '            ''check dengan timsespans apakah waktu sekarang sudah melewati endReminder bukan
            '            'TimeSpan diff = EndDate - StartDate;
            '            'int days = diff.Days;
            '            Dim diff As TimeSpan = DateTime.Now - Convert.ToDateTime(ER)
            '            Dim TimeDiff As Integer
            '            If SRN.ToString() = "Minute" Then
            '                TimeDiff = diff.Minutes
            '            ElseIf SRN.ToString() = "Hour" Then
            '                TimeDiff = diff.Hours
            '            ElseIf SRN.ToString() = "Day" Then
            '                TimeDiff = diff.Days
            '            End If
            '            If TimeDiff > 0 Then
            '                ''show Reminder
            '                Me.tmrReminder.Stop()
            '                Me.tmrReminder.Enabled = False
            '                'delete registry
            '                rg.DeleteKey("StartReminderTime")
            '                rg.DeleteKey("StartReminderName")
            '                rg.DeleteKey("EndReminder")
            '                rg.DeleteKey("Reminder")
            '                Dim frmReminder As New Notification()
            '                frmReminder.CMain = Me
            '                frmReminder.ShowInTaskbar = False
            '                frmReminder.StartPosition = FormStartPosition.CenterScreen
            '                frmReminder.TopMost = True
            '                frmReminder.Snooze = SnoozeInterval.ToString()
            '                frmReminder.Show(Me)
            '            Else
            '                ''hidupkan timer lama waktu interval sesuai kekurangannya
            '                diff = Convert.ToDateTime(ER) - DateTime.Now
            '                Dim tmInterval As Integer = IIf((diff.Milliseconds <= 0), 1000, diff.Milliseconds)
            '                Me.tmrReminder.Interval = tmInterval
            '                Me.tmrReminder.Enabled = True
            '                Me.tmrReminder.Start()
            '            End If
            '        Else
            '            'check apakah ada data yang harus di tampilkan ke user
            '            Dim frmReminder As New Notification()
            '            frmReminder.CMain = Me
            '            frmReminder.ShowInTaskbar = False
            '            frmReminder.StartPosition = FormStartPosition.CenterScreen
            '            frmReminder.TopMost = True
            '            frmReminder.Show(Me)
            '        End If
            '    End If
            'End If
            Dim frmReminder As New Notification()
            frmReminder.CMain = Me
            frmReminder.ShowInTaskbar = False
            frmReminder.StartPosition = FormStartPosition.CenterScreen
            frmReminder.TopMost = True
            frmReminder.Show(Me)
        End If
        Me.Show()
    End Sub
    Private Sub ShowAddjustment()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If IsNothing(Me.frmAdjustmentPKD) OrElse Me.frmAdjustmentPKD.IsDisposed Then
                frmAdjustmentPKD = New AjdustmentPKD()
                frmAdjustmentPKD.CMain = Me
                FrmActive = Me.frmAdjustmentPKD
                frmAdjustmentPKD.ShowInTaskbar = False
                frmAdjustmentPKD.MdiParent = Me
                frmAdjustmentPKD.Show() : Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                frmAdjustmentPKD = New AjdustmentPKD()
                FrmActive = frmAdjustmentPKD : frmAdjustmentPKD.ShowInTaskbar = False
                frmAdjustmentPKD.MdiParent = Me : Me.Timer1.Enabled = True
                Me.Timer1.Start()
            End If
        End If
    End Sub

    Private Sub ShowCPDAuto()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmCPDAuto)) OrElse (Me.frmCPDAuto.IsDisposed()) Then
                Me.frmCPDAuto = New CPDAuto()
                frmCPDAuto.CMain = Me
                FrmActive = frmCPDAuto
                frmCPDAuto.ShowInTaskbar = False
                frmCPDAuto.MdiParent = Me
                frmCPDAuto.Show() : Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                frmCPDAuto = New CPDAuto()
                FrmActive = frmCPDAuto : frmCPDAuto.ShowInTaskbar = False
                frmCPDAuto.MdiParent = Me : Me.Timer1.Enabled = True
                Me.Timer1.Start()
            End If
        End If
    End Sub
    Private Sub ShowAchievementRoundup()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmAchievementR)) OrElse (Me.frmAchievementR.IsDisposed()) Then
                Me.frmAchievementR = New AchievementF()
                frmAchievementR.CMain = Me
                FrmActive = frmAchievementR
                frmAchievementR.ShowInTaskbar = False
                frmAchievementR.MdiParent = Me
                frmAchievementR.Show() : Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                frmAchievementR = New AchievementF()
                FrmActive = frmAchievementR : frmAchievementR.ShowInTaskbar = False
                frmAchievementR.MdiParent = Me : Me.Timer1.Enabled = True
                Me.Timer1.Start()
            End If
        End If
    End Sub
    Private Sub ShowDDorDR()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmDiscDDorDR)) OrElse (Me.frmDiscDDorDR.IsDisposed()) Then
                Me.frmDiscDDorDR = New DiscountDDOrDR()
                frmDiscDDorDR.CMain = Me
                FrmActive = frmDiscDDorDR
                frmDiscDDorDR.ShowInTaskbar = False
                frmDiscDDorDR.MdiParent = Me
                frmDiscDDorDR.Show() : Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                frmDiscDDorDR = New DiscountDDOrDR()
                FrmActive = frmDiscDDorDR : frmDiscDDorDR.ShowInTaskbar = False
                frmDiscDDorDR.MdiParent = Me : Me.Timer1.Enabled = True
                Me.Timer1.Start()
            End If
        End If
    End Sub
    Private Sub ShowDKN()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmDKN)) OrElse (Me.frmDKN.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading
                ' Me.tmrHoldShowForm.Enabled = True
                frmDKN = New DKNational() : frmDKN.CMain = Me
                FrmActive = frmDKN : frmDKN.ShowInTaskbar = False
                frmDKN.MdiParent = Me
                frmDKN.Show() : Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                frmDKN = New DKNational()
                FrmActive = frmDKN : frmDKN.ShowInTaskbar = False
                frmDKN.MdiParent = Me : Me.Timer1.Enabled = True
                Me.Timer1.Start()
            End If
        End If
    End Sub
    Private Sub ShowBrand()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            Me.FormLoading = StatusForm.Loading
            ' Me.tmrHoldShowForm.Enabled = True
            frmBrand = New Brand() : frmBrand.CMain = Me : FrmActive = frmBrand
            frmBrand.ShowInTaskbar = False : frmBrand.ShowDialog(Me) : Me.ReadAcces()
        Else
            Me.DOLogin() : If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                frmBrand = New Brand() : FrmActive = frmBrand
                frmBrand.ShowInTaskbar = False : Me.Timer1.Enabled = True : Me.Timer1.Start()
            End If
        End If
    End Sub
    ''' <summary>
    ''' tampilkan class class untuk product
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowClassification()
        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            Me.FormLoading = StatusForm.Loading
            Me.frmClassification = New Classification()
            frmClassification.CMain = Me : FrmActive = Me.frmClassification
            frmClassification.ShowInTaskbar = False : frmClassification.ShowDialog(Me) : Me.ReadAcces()
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                frmClassification = New Classification()
                frmClassification.CMain = Me
                FrmActive = Me.frmClassification
                frmClassification.ShowInTaskbar = False : Me.Timer1.Enabled = True : Me.Timer1.Start()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Tampilkan form untuk mengclasifikasikan tiap product
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowProductClassification()
        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            Me.FormLoading = StatusForm.Loading
            Me.HoldLoadForm = 0
            Me.frmProdClassification = New ProductClassified()
            frmProdClassification.CMain = Me
            frmProdClassification.ShowInTaskbar = False
            FrmActive = Me.frmProdClassification
            Me.frmProdClassification.ShowDialog(Me) : Me.ReadAcces()
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                frmProdClassification = New ProductClassified()
                Me.FrmActive = Me.frmProdClassification : Me.frmProdClassification.ShowInTaskbar = False
                Me.Timer1.Enabled = True : Me.Timer1.Start()
            End If
        End If
    End Sub
    Private Sub ShowPack()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            Me.FormLoading = StatusForm.Loading
            Me.HoldLoadForm = 0 ' Me.tmrHoldShowForm.Enabled = True
            frmPack = New Pack : frmPack.CMain = Me : frmPack.ShowInTaskbar = False : FrmActive = frmPack
            frmPack.ShowDialog(Me) : Me.ReadAcces()
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                frmPack = New Pack : FrmActive = frmPack : frmPack.ShowInTaskbar = False
                Me.Timer1.Enabled = True : Me.Timer1.Start()
            End If
        End If
    End Sub
    Private Sub ShowBrandPackAndPriceFM()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmBrandpackHistory)) OrElse (Me.frmBrandpackHistory.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading
                ' Me.tmrHoldShowForm.Enabled = True
                frmBrandpackHistory = New BrandPackHistory() : frmBrandpackHistory.CMain = Me
                FrmActive = frmBrandpackHistory : frmBrandpackHistory.ShowInTaskbar = False
                frmBrandpackHistory.MdiParent = Me : frmBrandpackHistory.InitializeData()
                frmBrandpackHistory.Show() : Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                frmBrandpackHistory = New BrandPackHistory()
                FrmActive = frmBrandpackHistory : frmBrandpackHistory.ShowInTaskbar = False
                frmBrandpackHistory.MdiParent = Me : Me.Timer1.Enabled = True
                Me.Timer1.Start() : frmBrandpackHistory.InitializeData()
            End If
        End If
    End Sub
    Private Sub ShowCompareBrandPack()
        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            If (IsNothing(Me.frmCompareProduct)) OrElse (Me.frmCompareProduct.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmCompareProduct = New CompareBrandPackAccpack() : frmCompareProduct.CMain = Me
                FrmActive = Me.frmCompareProduct
                With Me.frmCompareProduct
                    .ShowInTaskbar = False : .MdiParent = Me : .Show()
                End With
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                frmCompareProduct = New CompareBrandPackAccpack()
                FrmActive = Me.frmCompareProduct
                With frmCompareProduct
                    .MdiParent = Me : .ShowInTaskbar = False
                End With
                Me.Timer1.Enabled = True : Me.Timer1.Start()
            End If
        End If
    End Sub
    Private Sub ShowDistributorResgistering()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmbtnDistributorRegistering)) OrElse (Me.frmbtnDistributorRegistering.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading
                ' Me.tmrHoldShowForm.Enabled = True
                Me.frmbtnDistributorRegistering = New Distributor() : frmbtnDistributorRegistering.CMain = Me
                frmbtnDistributorRegistering.MdiParent = Me
                FrmActive = frmbtnDistributorRegistering
                frmbtnDistributorRegistering.ShowInTaskbar = False
                frmbtnDistributorRegistering.InitializeData()
                frmbtnDistributorRegistering.Show()
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.frmbtnDistributorRegistering = New Distributor()
                FrmActive = frmbtnDistributorRegistering
                frmbtnDistributorRegistering.ShowInTaskbar = False
                frmbtnDistributorRegistering.MdiParent = Me
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
                frmbtnDistributorRegistering.InitializeData()
            End If
        End If
    End Sub
    Private Sub ShowDistributorHistory()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmDHistory)) OrElse (Me.frmDHistory.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading
                ' Me.tmrHoldShowForm.Enabled = True
                frmDHistory = New DistributorHistory() : frmDHistory.CMain = Me
                Me.FrmActive = frmDHistory
                frmDHistory.ShowInTaskbar = False
                frmDHistory.MdiParent = Me
                frmDHistory.InitilizeData()
                frmDHistory.Show()
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                frmDHistory = New DistributorHistory()
                Me.FrmActive = frmDHistory
                frmDHistory.ShowInTaskbar = False
                frmDHistory.MdiParent = Me
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
                frmDHistory.InitilizeData()
            End If
        End If
    End Sub
    Private Sub ShowDistributorRegion()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmRegion)) OrElse (Me.frmRegion.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmRegion = New Region() : frmRegion.CMain = Me
                Me.FrmActive = Me.frmRegion
                Me.frmRegion.InitializeData()
                Me.frmRegion.Show()
                Me.frmRegion.MdiParent = Me
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.frmRegion = New Region()
                Me.FrmActive = Me.frmRegion
                Me.frmRegion.MdiParent = Me
                Me.frmRegion.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
                Me.frmRegion.InitializeData()
            End If
        End If
    End Sub
    Private Sub ShowAgreement()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmAgreement)) OrElse (Me.frmAgreement.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmAgreement = New Agreement() : frmAgreement.CMain = Me
                Me.frmAgreement.InitializeData()
                Me.FrmActive = Me.frmAgreement
                Me.frmAgreement.MdiParent = Me
                Me.frmAgreement.ShowInTaskbar = False
                Me.frmAgreement.Show()
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.frmAgreement = New Agreement()
                Me.FrmActive = Me.frmAgreement
                Me.frmAgreement.MdiParent = Me
                Me.frmAgreement.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
                Me.frmAgreement.InitializeData()
            End If
        End If
    End Sub
    Private Sub ShowPurchaseOrder()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmPurchaseOrder)) OrElse (Me.frmPurchaseOrder.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmPurchaseOrder = New PO : frmPurchaseOrder.CMain = Me
                Me.frmPurchaseOrder.InitializeData()
                Me.FrmActive = Me.frmPurchaseOrder
                Me.frmPurchaseOrder.MdiParent = Me
                Me.frmPurchaseOrder.ShowInTaskbar = False
                Me.frmPurchaseOrder.Show()
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.frmPurchaseOrder = New PO()
                Me.FrmActive = Me.frmPurchaseOrder
                Me.frmPurchaseOrder.MdiParent = Me
                Me.frmPurchaseOrder.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
                Me.frmPurchaseOrder.InitializeData()
            End If
        End If
    End Sub
    Private Sub ShowProgram()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmProgram)) OrElse (Me.frmProgram.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmProgram = New Program() : frmProgram.CMain = Me
                Me.frmProgram.InitializeData()
                Me.FrmActive = Me.frmProgram
                Me.frmProgram.MdiParent = Me
                Me.frmProgram.ShowInTaskbar = False
                Me.frmProgram.Show()
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.frmProgram = New Program()
                Me.FrmActive = Me.frmProgram
                Me.frmProgram.MdiParent = Me
                Me.frmProgram.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
                Me.frmProgram.InitializeData()
            End If
        End If
    End Sub
    Private Sub ShowOrderAcceptance()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmAcceptance)) OrElse (Me.frmAcceptance.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmAcceptance = New Acceptance() : frmAcceptance.CMain = Me
                Me.frmAcceptance.InitializeData()
                Me.FrmActive = Me.frmAcceptance
                Me.frmAcceptance.MdiParent = Me
                Me.frmAcceptance.ShowInTaskbar = False
                Me.frmAcceptance.Show()
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.frmAcceptance = New Acceptance()
                Me.FrmActive = Me.frmAcceptance
                Me.frmAcceptance.MdiParent = Me
                Me.frmAcceptance.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
                Me.frmAcceptance.InitializeData()
            End If
        End If
    End Sub
    Private Sub ShowDiscountAgreement()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmDiscount)) OrElse (Me.frmDiscount.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmDiscount = New DiscAgreement() : frmDiscount.CMain = Me
                Me.frmDiscount.InitializeData()
                Me.FrmActive = Me.frmDiscount
                Me.frmDiscount.MdiParent = Me
                Me.frmDiscount.ShowInTaskbar = False
                Me.frmDiscount.Show()
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmDiscount = New DiscAgreement()
                Me.FrmActive = Me.frmDiscount
                Me.frmDiscount.MdiParent = Me
                Me.frmDiscount.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
                Me.frmDiscount.InitializeData()
            End If
        End If
    End Sub
    Private Sub LogOutSystem()
        Dim lgout As New NufarmBussinesRules.User.Login
        If Not IsNothing(Me.FrmActive) Then
            lgout.LogoutUser(Me.FrmActive.Name)
        Else
            lgout.LogoutUser(Me.Name)
        End If
        NufarmBussinesRules.User.UserLogin.UserName = ""
        NufarmBussinesRules.User.UserLogin.UserPassword = ""
        NufarmBussinesRules.User.UserLogin.IsAdmin = False
        NufarmBussinesRules.User.UserLogin.HasLogin = False
        If MessageBox.Show("Do you want to relogin ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Me.Close() : Return
        End If
        Me.DestroyForm() : Me.DOLogin()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            Me.ReadAcces()
        Else
            Me.btnSetting.Visible = False
            Me.btnUser.Visible = True
            Me.btnChangePassword.Visible = False
            Me.btnLogIn.Visible = True
            Me.btnLogout.Visible = False

            Me.btnBrandPack.Visible = False
            Me.btnDistributor.Visible = False
            Me.btnSales.Visible = False
            Me.btnOrder.Visible = False
            Me.btnGenerate.Visible = False
            Me.btnReport.Visible = False
            Me.btnSetting.Visible = False
            Me.btnSMS.Visible = False
        End If
    End Sub
    Private Sub ShowChangedPassword()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            Dim ChangePass As New ChangePassword()
            ChangePass.ShowInTaskbar = False
            ChangePass.ShowDialog(Me)
        End If
    End Sub
    Private Sub ShowManageUser()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If ((NufarmBussinesRules.User.UserLogin.UserName <> "") And (NufarmBussinesRules.User.UserLogin.IsAdmin = True)) Or Me.IsSystemAdministrator Then
                If IsNothing(Me.frmManageUser) OrElse (Me.frmManageUser.IsDisposed()) Then
                    Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                    Me.frmManageUser = New User() : frmManageUser.CMain = Me
                    Me.frmManageUser.InitializeData()
                    Me.FrmActive = Me.frmManageUser
                    Me.frmManageUser.MdiParent = Me
                    Me.frmManageUser.ShowInTaskbar = False
                End If
                Me.frmManageUser.Show()
                Me.ReadAcces()
            End If
        End If
    End Sub
    Private Sub ShowCancelPO()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
            Me.CPO = New Cancel_PO() : Me.CPO.CMain = Me
            Me.CPO.InitializeData()
            Me.FrmActive = Me.CPO
            Me.CPO.ShowInTaskbar = False
            Me.CPO.MdiParent = Me
            Me.CPO.Show()
            Me.ReadAcces()
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.CPO = New Cancel_PO()
                Me.FrmActive = Me.CPO
                Me.CPO.MdiParent = Me
                Me.CPO.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
                Me.CPO.InitializeData()
            End If
        End If
    End Sub
    Private Sub ShowLogin()
        NufarmBussinesRules.User.UserLogin.UserName = ""
        NufarmBussinesRules.User.UserLogin.UserPassword = ""
        NufarmBussinesRules.User.UserLogin.IsAdmin = False
        NufarmBussinesRules.User.UserLogin.HasLogin = False
        Me.DestroyForm()
        Me.DOLogin()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            Me.ReadAcces()
        Else
            Me.btnSetting.Visible = NufarmBussinesRules.User.UserLogin.IsAdmin
            Me.btnUser.Visible = True
            Me.btnChangePassword.Visible = False
            Me.btnLogIn.Visible = True
            Me.btnLogout.Visible = False

            Me.btnBrandPack.Visible = False
            Me.btnDistributor.Visible = False
            Me.btnSales.Visible = False
            Me.btnOrder.Visible = False
            Me.btnGenerate.Visible = False
            Me.btnReport.Visible = False
            Me.btnSetting.Visible = False
        End If
    End Sub
    Private Sub ShowReportByGrid()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            If (IsNothing(Me.frmReport)) OrElse (Me.frmReport.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True

                Me.frmReport = New ReportGrid() : frmReport.CMain = Me
                'Me.frmReport.InitializeData()
                Me.FrmActive = Me.frmReport
                Me.frmReport.MdiParent = Me
                Me.frmReport.ShowInTaskbar = False
                Me.frmReport.Show()
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmReport = New ReportGrid()
                Me.FrmActive = Me.frmReport
                Me.frmReport.MdiParent = Me
                Me.frmReport.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
                'Me.frmDiscProject.InitializeData()
            End If
        End If
    End Sub
    'Private Sub ShowOAReportByDistributor()
    '    If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
    '        If (IsNothing(Me.frmReportByCrystal)) OrElse (Me.frmReportByCrystal.IsDisposed()) Then
    '            Me.frmReportByCrystal = New frmReport()
    '            Me.FrmActive = Me.frmReportByCrystal
    '            Me.frmReportByCrystal.MdiParent = Me
    '            Me.frmReportByCrystal.InitializeData()
    '            Me.frmReportByCrystal.ShowInTaskbar = False
    '            Me.frmReportByCrystal.Show()
    '            Me.ReadAcces()
    '        End If
    '    Else
    '        Me.DOLogin()
    '        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
    '            Me.frmReportByCrystal = New frmReport()
    '            Me.frmReportByCrystal.MdiParent = Me
    '            Me.frmReportByCrystal.ShowInTaskbar = False
    '            Me.frmReportByCrystal.InitializeData()
    '            Me.Timer1.Enabled = True
    '            Me.Timer1.Start()
    '            Me.FrmActive = Me.frmReportByCrystal
    '        End If
    '    End If
    'End Sub
    'Private Sub ShowSummaryReport()
    '    If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
    '        If (IsNothing(Me.frmCrReport)) OrElse (Me.frmCrReport.IsDisposed()) Then
    '            Me.frmCrReport = New frmCrystalReport()
    '            Me.FrmActive = Me.frmCrReport
    '            Me.frmCrReport.MdiParent = Me
    '            Me.frmCrReport.InitializeData()
    '            Me.frmCrReport.ShowInTaskbar = False
    '            Me.frmCrReport.Show()
    '            Me.ReadAcces()
    '        End If
    '    Else
    '        Me.DOLogin()
    '        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
    '            Me.frmCrReport = New frmCrystalReport()
    '            Me.frmCrReport.MdiParent = Me
    '            Me.frmCrReport.ShowInTaskbar = False
    '            Me.frmCrReport.InitializeData()
    '            Me.Timer1.Enabled = True
    '            Me.Timer1.Start()
    '            Me.FrmActive = Me.frmCrReport
    '        End If
    '    End If
    'End Sub
    Private Sub ShowSMSRemainding()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
            Me.SMSRemainding = New SMS() : SMSRemainding.Cmain = Me
            'Me.SMSRemainding.InitializeData()
            FrmActive = Me.SMSRemainding
            Me.SMSRemainding.ShowInTaskbar = False
            Me.SMSRemainding.ShowDialog(Me)
            Me.ReadAcces()
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.SMSRemainding = New SMS()
                'Me.SMSRemainding.InitializeData()
                FrmActive = Me.SMSRemainding
                Me.SMSRemainding.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
            End If
        End If
    End Sub
    Private Sub ShowOtherSMS()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            'Me.ShowThread()
            Me.O_SMS = New Other_SMS
            O_SMS.CMain = Me
            Me.O_SMS.InitializeData()
            FrmActive = Me.O_SMS
            Me.O_SMS.ShowInTaskbar = False
            Me.O_SMS.ShowDialog(Me)
            Me.ReadAcces()
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.O_SMS = New Other_SMS
                Me.O_SMS.InitializeData()
                FrmActive = Me.O_SMS
                Me.O_SMS.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
            End If
        End If
    End Sub
    Private Sub ShowShipToTM()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            'Me.ShowThread()
            If (IsNothing(Me.frmShipTo)) OrElse (Me.frmShipTo.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmShipTo = New Ship_To() : frmShipTo.CMain = Me
                Me.frmShipTo.InitializeData()
                Me.FrmActive = Me.frmShipTo
                Me.frmShipTo.ShowInTaskbar = False
                Me.frmShipTo.MdiParent = Me
                Me.frmShipTo.Show()
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.frmShipTo = New Ship_To()
                Me.frmShipTo.InitializeData()
                Me.FrmActive = Me.frmShipTo
                Me.frmShipTo.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
            End If
        End If
    End Sub
    'Private Sub ShowPODOThirdParty()
    '    If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
    '        If (IsNothing(Me.frmPODO)) OrElse (Me.frmPODO.IsDisposed()) Then
    '            Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
    '            'Me.ShowThread()
    '            Me.frmPODO = New ThirdParty() : frmPODO.CMain = Me
    '            Me.frmPODO.InitializeData()
    '            Me.FrmActive = Me.frmPODO
    '            Me.frmPODO.ShowInTaskbar = False
    '            Me.frmPODO.MdiParent = Me
    '            Me.frmPODO.Show()
    '            Me.ReadAcces()
    '        End If
    '    Else
    '        Me.DOLogin()
    '        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
    '            Me.frmPODO = New ThirdParty()
    '            Me.frmPODO.InitializeData()
    '            Me.FrmActive = Me.frmPODO
    '            Me.frmPODO.ShowInTaskbar = False
    '            Me.frmPODO.MdiParent = Me
    '            Me.Timer1.Enabled = True
    '            Me.Timer1.Start()
    '        End If
    '    End If
    'End Sub
    Private Sub ShowSPPB()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            'Me.ShowThread()
            If (IsNothing(Me.frmSPPB)) OrElse (Me.frmSPPB.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmSPPB = New SPPB() : frmSPPB.CMain = Me
                Me.frmSPPB.InitializeData()
                Me.FrmActive = Me.frmSPPB
                Me.frmSPPB.ShowInTaskbar = False
                Me.frmSPPB.MdiParent = Me
                Me.frmSPPB.Show()
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.frmSPPB = New SPPB()
                Me.frmSPPB.InitializeData()
                Me.FrmActive = Me.frmSPPB
                Me.frmSPPB.ShowInTaskbar = False
                Me.frmSPPB.MdiParent = Me
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
            End If
        End If
    End Sub
    Private Sub ShowManageDPRD()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            'Me.ShowThread()
            If (IsNothing(Me.frmDPRD)) OrElse (Me.frmDPRD.IsDisposed()) Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmDPRD = New frmManageDPRD() : frmDPRD.CMain = Me
                Me.FrmActive = Me.frmDPRD
                Me.frmDPRD.ShowInTaskbar = False
                Me.frmDPRD.MdiParent = Me
                Me.frmDPRD.Show()
                Me.ReadAcces()
            End If
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
                Me.frmDPRD = New frmManageDPRD()
                Me.FrmActive = Me.frmDPRD
                Me.frmDPRD.ShowInTaskbar = False
                Me.frmDPRD.MdiParent = Me
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
            End If

        End If
    End Sub
    Private Sub ShowSendingData()
        If NufarmBussinesRules.User.UserLogin.HasLogin = True Then
            Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
            'Me.ShowThread()
            Me.frmSendData = New FrmSendingData() : frmSendData.CMain = Me
            Me.FrmActive = Me.frmSendData
            Me.frmSendData.ShowInTaskbar = False
            'Me.frmSendData.MdiParent = Me
            Me.frmSendData.CMain = Me
            Me.frmSendData.ShowDialog(Me)
            Me.ReadAcces()
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmSendData = New FrmSendingData()
                Me.FrmActive = Me.frmSendData
                Me.frmSendData.ShowInTaskbar = False
                Me.Timer1.Enabled = True
                Me.Timer1.Start()

            End If
        End If
    End Sub
    Private Sub ShowBrandPackPlantationPrice()
        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            'Me.ShowThread()
            If IsNothing(Me.frmPricePlantation) OrElse Me.frmPricePlantation.IsDisposed() Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmPricePlantation = New PlantationPrice() : frmPricePlantation.CMain = Me
            End If
            With Me.frmPricePlantation
                .ShowInTaskbar() = False : .MdiParent = Me : .LoadData()

                .Show() : Me.ReadAcces()
            End With
            Me.FrmActive = Me.frmPricePlantation
        Else
            Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmPricePlantation = New PlantationPrice()
                Me.FrmActive = Me.frmPricePlantation
                Me.frmPricePlantation.ShowInTaskbar() = False
                Me.frmPricePlantation.MdiParent = Me
                Me.frmPricePlantation.LoadData()
                Me.Timer1.Enabled = True : Me.Timer1.Start()
                Me.FrmActive = Me.frmPricePlantation
            End If
        End If
    End Sub
    Private Sub ShowPriceHistory()
        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            'Me.ShowThread()
            If IsNothing(Me.frmPriceHistory) OrElse Me.frmPriceHistory.IsDisposed() Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmPriceHistory = New PriceHistory() : frmPriceHistory.CMain = Me
            End If
            With Me.frmPriceHistory
                .ShowInTaskbar = False : .MdiParent = Me : .LoadData()
                .Show() : Me.ReadAcces()
            End With
            Me.FrmActive = Me.frmPriceHistory
        Else
            Me.DOLogin() : If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmPriceHistory = New PriceHistory()
                With Me.frmPriceHistory : .ShowInTaskbar = False : .LoadData() : .MdiParent = Me : End With
                Me.Timer1.Enabled = True : Me.Timer1.Start() : Me.FrmActive = Me.frmPriceHistory
            End If
        End If
    End Sub
    Private Sub ShowOptionSystem()
        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
            'Me.ShowThread()
            Me.frmOptions = New Settings() : frmOptions.CMain = Me
            With Me.frmOptions
                .ShowInTaskbar = False : .CMain = Me
                If .ShowDialog(Nothing, Nothing) = Windows.Forms.DialogResult.OK Then
                    'MessageBox.Show("All form must be closed to take affects", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DestroyForm() : Me.ReadAcces()
                End If
            End With
            FrmActive = Me.frmOptions
        Else
            Me.DOLogin() : If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmOptions = New Settings()
                With Me.frmOptions
                    Me.FrmActive = Me.frmOptions
                    .ShowInTaskbar = False
                End With
                Me.Timer1.Enabled = True : Me.Timer1.Start()
                FrmActive = Me.frmOptions
            End If
        End If
    End Sub
    Private Sub ShowInvoiceData()

        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            'Me.ShowThread()
            If IsNothing(Me.frmInvoice) OrElse Me.frmInvoice.IsDisposed Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmInvoice = New Invoice() : Application.DoEvents() : frmInvoice.CMain = Me
            End If
            With Me.frmInvoice
                .ShowInTaskbar = False : .MdiParent = Me : Application.DoEvents()

                .Show() : Me.ReadAcces()
            End With
            Me.FrmActive = Me.frmInvoice
        Else : Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmInvoice = New Invoice()
                With Me.frmInvoice : .ShowInTaskbar = False : .MdiParent = Me : End With
                Me.Timer1.Enabled = True : Me.Timer1.Start()
                Me.FrmActive = Me.frmInvoice
            End If
        End If
    End Sub
    Private Sub ShowAchievementPKD()

        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            'Me.ShowThread()
            If IsNothing(Me.frmAchievement) OrElse Me.frmAchievement.IsDisposed Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmAchievement = New AchievementDPD() : frmAchievement.CMain = Me
            End If
            With Me.frmAchievement
                .ShowInTaskbar = False : .MdiParent = Me
                .LoadData() : .Show() : Me.ReadAcces()
            End With
            Me.FrmActive = Me.frmAchievement
        Else : Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmAchievement = New AchievementDPD()
                With Me.frmAchievement : .ShowInTaskbar = False : .LoadData() : .MdiParent = Me : End With
                Me.Timer1.Enabled = True : Me.Timer1.Start()
                Me.FrmActive = Me.frmAchievement
            End If
        End If
    End Sub
    Private Sub ShowPlantationBrandPack()

        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            'Me.ShowThread()
            If IsNothing(Me.frmPlantation) OrElse Me.frmPlantation.IsDisposed() Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmPlantation = New PlantationManager() : frmPlantation.CMain = Me
            End If
            With Me.frmPlantation
                .ShowInTaskbar = False : .MdiParent = Me
                .LoadData() : .Show() : Me.ReadAcces()
            End With
            Me.FrmActive = Me.frmPlantation
        Else : Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmPlantation = New PlantationManager()
                With Me.frmPlantation
                    .ShowInTaskbar = False : .LoadData() : .MdiParent = Me
                End With
                Me.Timer1.Enabled = True : Me.Timer1.Start()
                Me.FrmActive = Me.frmPlantation
            End If
        End If

    End Sub
    Private Sub ShowThread()
        Me.StatProg = StatusProgress.Processing
        Me.ThreadProgress = New Thread(AddressOf ShowProceed)
        Me.ThreadProgress.Start()
    End Sub
    Private Sub ShowProject()
        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            'Me.ShowThread()
            If IsNothing(Me.frmProject) OrElse Me.frmProject.IsDisposed() Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmProject = New Project()
            End If
            With Me.frmProject
                .ShowInTaskbar = False : .MdiParent = Me
                Me.FrmActive = Me.frmProject : .Show() : Me.ReadAcces()
            End With
        Else : Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmProject = New Project()
                With Me.frmProject
                    .ShowInTaskbar = False : .MdiParent = Me
                End With
                Me.Timer1.Enabled = True : Me.Timer1.Start()
            End If
        End If

    End Sub
    Private Sub ShowTransporter()
        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            'Me.ShowThread()
            If IsNothing(Me.frmtransporter) OrElse Me.frmtransporter.IsDisposed() Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmtransporter = New Transporter()
            End If
            With Me.frmtransporter
                .ShowInTaskbar = False : .MdiParent = Me
                .Show() : Me.ReadAcces()
            End With
            Me.FrmActive = Me.frmtransporter
        Else : Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmtransporter = New Transporter()
                With Me.frmtransporter
                    .ShowInTaskbar = False : .MdiParent = Me
                End With
                Me.Timer1.Enabled = True : Me.Timer1.Start()
                Me.FrmActive = Me.frmAreaGON
            End If
        End If
    End Sub
    Private Sub ShowAVGPrice()
        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            'Me.ShowThread()
            If IsNothing(Me.frmAVGPrice) OrElse Me.frmAVGPrice.IsDisposed() Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmAVGPrice = New AVGPrice()
            End If
            With Me.frmAVGPrice
                .ShowInTaskbar = False : .MdiParent = Me
                .CMain = Me
                .Show() : Me.ReadAcces()
            End With
            Me.FrmActive = Me.frmAVGPrice
        Else : Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmAVGPrice = New AVGPrice()
                With Me.frmAVGPrice
                    .ShowInTaskbar = False : .MdiParent = Me
                    .CMain = Me
                End With
                Me.Timer1.Enabled = True : Me.Timer1.Start()
                Me.FrmActive = Me.frmAVGPrice
            End If
        End If
    End Sub
    Private _IsSystemAdministrator As Boolean = False
    Public Property IsSystemAdministrator() As Boolean
        Get
            Return IIf(Configuration.ConfigurationManager.AppSettings("SysA") = "True", True, False)
        End Get
        Set(ByVal value As Boolean)
            _IsSystemAdministrator = value
        End Set
    End Property

    Public _isITSupport As Boolean = False
    Public Property isITSupport() As Boolean
        Get
            Return _isITSupport
        End Get
        Set(ByVal value As Boolean)
            _isITSupport = value
        End Set
    End Property
    Private Sub ShowAreaGON()
        If NufarmBussinesRules.User.UserLogin.HasLogin Then
            'Me.ShowThread()
            If IsNothing(Me.frmAreaGON) OrElse Me.frmAreaGON.IsDisposed() Then
                Me.FormLoading = StatusForm.Loading ' Me.tmrHoldShowForm.Enabled = True
                Me.frmAreaGON = New AreaGON()
            End If
            With Me.frmAreaGON
                .ShowInTaskbar = False : .MdiParent = Me

                .Show() : Me.ReadAcces()
            End With
            Me.FrmActive = Me.frmAreaGON
        Else : Me.DOLogin()
            If NufarmBussinesRules.User.UserLogin.HasLogin Then
                Me.frmAreaGON = New AreaGON()
                With Me.frmAreaGON
                    .ShowInTaskbar = False : .MdiParent = Me
                End With
                Me.Timer1.Enabled = True : Me.Timer1.Start()
                Me.FrmActive = Me.frmAreaGON
            End If
        End If
    End Sub
    Private Sub RenewObjectForm(ByVal OForm As Form)
        If Not IsNothing(OForm) Then
            If Not OForm.IsDisposed() Then
                OForm.Dispose()
            End If
            OForm = Nothing
        End If
    End Sub
    Private Sub Bar1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
       
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnBrand" : Me.ShowBrand()
                Case "btnPack" : Me.ShowPack()
                Case "btnBrandPackItem" : Me.ShowBrandPackAndPriceFM()
                Case "btnCompareBrandPack" : Me.ShowCompareBrandPack()
                Case "btnDistributorRegistering" : Me.ShowDistributorResgistering()
                Case "btnDistributorHistory" : Me.ShowDistributorHistory()
                Case "btnDistributorRegion" : Me.ShowDistributorRegion()
                Case "btnAgreement" : Me.ShowAgreement()
                Case "btnPO" : Me.ShowPurchaseOrder()
                Case "btnProgram" : Me.ShowProgram()
                Case "btnOrderAcceptance" : Me.ShowOrderAcceptance()
                Case "btnDiscAgreement" : Me.ShowDiscountAgreement()
                Case "btnExit" : Me.Close()
                Case "btnLogout" : Me.LogOutSystem()
                Case "btnChangePassword" : Me.ShowChangedPassword()
                Case "btnManageUser" : Me.ShowManageUser()
                Case "btncancelPO" : Me.ShowCancelPO()
                Case "btnLogIn" : Me.ShowLogin()
                Case "btnByGrid" : Me.ShowReportByGrid()
                    'Case "btnOAPODistributor" : Me.ShowOAReportByDistributor()
                    'Case "btnSummaryReport" : Me.ShowSummaryReport()
                Case "btnSMSReminding" : Me.ShowSMSRemainding()
                Case "btnOtherSMS" : Me.ShowOtherSMS()
                Case "btnShipToArea"
                Case "btnShipToTM" : Me.ShowShipToTM()
                    'Case "btnPODO" : Me.ShowPODOThirdParty()
                Case "btnSPPB" : Me.ShowSPPB()
                Case "btnManageDPRD" : Me.ShowManageDPRD()
                Case "btnSendingData" : Me.ShowSendingData()
                Case "btnBrandPackPlantation" : Me.ShowBrandPackPlantationPrice()
                Case "btnPlantationBrandPack" : Me.ShowPlantationBrandPack()
                Case "btnPriceHistory" : Me.ShowPriceHistory()
                Case "btnOptions" : Me.ShowOptionSystem()
                Case "btnInvoice" : Me.ShowInvoiceData()
                Case "btnAchievement" : Me.ShowAchievementPKD()
                Case "btnContent"
                Case "btnProject" : Me.ShowProject()
                Case "btnHelp"
                    Dim path As String = Application.StartupPath() & "\" & "Help.pdf"
                    System.Diagnostics.Process.Start(path)
                Case "btnGONArea" : Me.ShowAreaGON()
                Case "btnTransporter" : Me.ShowTransporter()
                Case "btnClasification" : Me.ShowClassification()
                Case "btnProductClass" : Me.ShowProductClassification()
                Case "btnDKNational" : Me.ShowDKN()
                Case "btnAVGPrice" : Me.ShowAVGPrice()
                Case "btnCPDAuto" : Me.ShowCPDAuto()
                Case "btnAjdustmentPKD" : Me.ShowAddjustment()
                Case "btnDiscDDAndDR" : Me.ShowDDorDR()
                Case "btnAchievementDPDR" : Me.ShowAchievementRoundup()
            End Select
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : MessageBox.Show(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar1_ItemClick")
            Me.RenewObjectForm(Me.FrmActive)
        Finally
            Me.StatProg = StatusProgress.None : Me.FormLoading = StatusForm.HasLoaded : Me.Cursor = Cursors.Default
        End Try

    End Sub
    
    Private Sub Main_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim lgout As New NufarmBussinesRules.User.Login
            If Not IsNothing(Me.FrmActive) Then
                lgout.LogoutUser(Me.FrmActive.Name)
            ElseIf NufarmBussinesRules.User.UserLogin.UserName <> "" Then
                lgout.LogoutUser(Me.Name)
            End If
            Me.DestroyForm() : Me.Dispose(True)
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub ChekTimer2(ByVal sender As Object, ByVal e As EventArgs)
        If Me.TickCount >= 2 Then : Me.TickCount = 0 : Me.Timer2.Stop() : Me.Timer2.Enabled = False
            Me.Bar1_ItemClick(Me.btnLogIn, New EventArgs())
        End If
    End Sub
    Private Sub CheckTimer(ByVal sender As Object, ByVal e As EventArgs)
        If Me.HoldLoadForm = 3 Then
            If Not IsNothing(Me.FrmActive) Then
                Select Case Me.FrmActive.Name
                    Case "Brand" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : frmBrand.ShowDialog(Me)
                    Case "Pack" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : frmPack.ShowDialog(Me)
                    Case "BrandPackHistory" : frmBrandpackHistory.Show()
                    Case "btnCompareBrandPack" : frmCompareProduct.Show()
                    Case "Distributor" : Me.frmbtnDistributorRegistering.Show()
                    Case "DistributorHistory" : Me.frmDHistory.Show()
                    Case "Region" : Me.frmRegion.Show()
                    Case "Agreement" : Me.frmAgreement.Show()
                    Case "PO" : Me.frmPurchaseOrder.Show()
                    Case "Program" : Me.frmProgram.Show()
                    Case "Acceptance" : Me.frmAcceptance.Show()
                    Case "DiscAgreement" : Me.frmDiscount.Show()
                    Case "ReportGrid" : Me.frmReport.Show()
                        'Case "frmReport" : Me.frmReportByCrystal.Show()
                        'Case "frmCrystalReport" : Me.frmCrReport.Show()
                    Case "Other_SMS" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : Me.O_SMS.ShowDialog(Me)
                    Case "SMS" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : Me.SMSRemainding.ShowDialog(Me)
                    Case "Cancel_PO" : Me.CPO.Show()
                    Case "SPPB" : Me.frmSPPB.Show()
                        'Case "ThirdParty" : Me.frmPODO.Show()
                    Case "FrmSendingData" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : Me.frmSendData.ShowDialog(Me)
                    Case "frmManageDPRD" : Me.frmDPRD.Show()
                    Case "Project" : Me.ShowProject()
                    Case "btnOptions" : Me.Timer1.Enabled = False : Me.Timer1.Stop()
                        If CType(Me.FrmActive, Settings).ShowDialog(Nothing, Nothing) = Windows.Forms.DialogResult.OK Then
                            MessageBox.Show("All form(s) must be closed to take affects", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DestroyForm()
                        End If
                    Case "AreaGON" : Me.frmAreaGON.Show()
                    Case "Transporter" : Me.frmtransporter.Show()
                    Case "Classification" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : frmClassification.ShowDialog(Me)
                    Case "ProductClassified" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : frmProdClassification.ShowDialog(Me)
                    Case "frmDKN" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : Me.frmDKN.Show()
                    Case "frmCPDAuto" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : Me.frmCPDAuto.Show()
                    Case "btnAjdustmentPKD" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : Me.frmAdjustmentPKD.Show()
                    Case "btnDiscDDAndDR" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : Me.frmDiscDDorDR.Show()
                    Case "btnAchievementDPDR" : Me.Timer1.Enabled = False : Me.Timer1.Stop() : Me.frmAchievementR.Show()
                End Select
            End If
            Me.Timer1.Enabled = False : Me.Timer1.Stop() : Me.HoldLoadForm = 0 : Me.ReadAcces()
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.HoldLoadForm += 1
    End Sub

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            NufarmBussinesRules.User.UserLogin.HasLogin = Me.IsSystemAdministrator
            NufarmBussinesRules.User.UserLogin.UserName = IIf(IsSystemAdministrator, "System Administrator", "")
            NufarmBussinesRules.SharedClass.ServerDate = Convert.ToDateTime(DateTime.Now.ToShortDateString())

            If Not Me.IsSystemAdministrator Then
                Me.Hide()
                'Me.btnUserPreviledge.Visible = False
                'Me.btnUser.Visible = True
                'Me.btnChangePassword.Visible = False
                'Me.btnLogIn.Visible = True
                'Me.btnLogout.Visible = False
                'Me.btnBrandPack.Visible = False
                'Me.btnDistributor.Visible = False
                'Me.btnSales.Visible = False
                'Me.btnOrder.Visible = False
                'Me.btnGenerate.Visible = False
                'Me.btnReport.Visible = False
                'Me.btnUserPreviledge.Visible = False
                ''Me.btnSMS.Visible = False
                'Me.tmrHoldShowForm = New System.Windows.Forms.Timer()
                'Me.tmrHoldShowForm.Interval = 800
                'Me.tmrHoldShowForm.Enabled = False
                AddHandler Timer1.Tick, AddressOf CheckTimer : AddHandler Timer2.Tick, AddressOf ChekTimer2
                Me.Timer2.Enabled = True : Me.Timer2.Start()
            Else
                NufarmBussinesRules.User.UserLogin.IsAdmin = True
                Dim c As New NufarmBussinesRules.User.Login()
                c.IntilializePriviledge(True)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Me.TickCount += 1
    End Sub

    Private Sub tmrReminder_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrReminder.Tick
        Try
            For Each frm As Form In Application.OpenForms
                If frm.Modal = True Then
                    Me.tmrReminder.Interval = 10000
                    Exit Sub
                End If
            Next
            Dim clsProgram As New NufarmBussinesRules.Program.BrandPackInclude()
            If Not Me.HasCheckDataReminder Then
                hasDataReminder = clsProgram.hasDataReminder()
                HasCheckDataReminder = True
            End If
            If hasDataReminder Then
                Dim rg As New NufarmBussinesRules.SettingDTS.RegUser()
                Dim SRT As Object = rg.Read("StartReminderTime")
                Dim SRN As Object = rg.Read("StartReminderName")
                Dim ER As Object = rg.Read("EndReminder")
                'define interval snooze
                Dim SnoozeInterval = rg.Read("Reminder")
                If Not IsNothing(SRT) And Not IsNothing(SRN) And Not IsNothing(ER) Then
                    ''check dengan timsespans apakah waktu sekarang sudah melewati endReminder bukan
                    'TimeSpan diff = EndDate - StartDate;
                    'int days = diff.Days;
                    Dim diff As TimeSpan = DateTime.Now - Convert.ToDateTime(ER)
                    Dim TimeDiff As Integer
                    If SRN.ToString() = "Minute" Then
                        TimeDiff = diff.Minutes
                    ElseIf SRN.ToString() = "Hour" Then
                        TimeDiff = diff.Hours
                    ElseIf SRN.ToString() = "Day" Then
                        TimeDiff = diff.Days
                    End If
                    If TimeDiff > 0 Then
                        ''show Reminder
                        rg.DeleteKey("StartReminderTime")
                        rg.DeleteKey("StartReminderName")
                        rg.DeleteKey("EndReminder")
                        rg.DeleteKey("Reminder")
                        Me.tmrReminder.Stop()
                        Me.tmrReminder.Enabled = False
                        Dim frmReminder As New Notification()
                        frmReminder.CMain = Me
                        frmReminder.ShowInTaskbar = False
                        frmReminder.StartPosition = FormStartPosition.CenterScreen
                        frmReminder.TopMost = True
                        frmReminder.Snooze = SnoozeInterval.ToString()
                        frmReminder.Show(Me)
                    Else
                        Me.tmrReminder.Interval = 1000
                        Me.tmrReminder.Enabled = True
                        Me.tmrReminder.Start()
                    End If
                Else
                    'check apakah ada data yang harus di tampilkan ke user
                    Dim frmReminder As New Notification()
                    frmReminder.CMain = Me
                    frmReminder.ShowInTaskbar = False
                    frmReminder.StartPosition = FormStartPosition.CenterScreen
                    frmReminder.TopMost = True
                    frmReminder.Show(Me)
                End If
            Else
                Me.tmrReminder.Stop()
                Me.tmrReminder.Enabled = False
            End If
        Catch ex As Exception

            Me.tmrReminder.Enabled = False
            Me.tmrReminder.Stop()
        End Try

    End Sub
End Class