Imports System.Configuration
Imports System.Data.SqlClient
Namespace User
    Public Class Login

        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        'validasi user di sini
        Private Query As String = ""
        Public Function ValidateUser() As Boolean
            Dim IsValid As Boolean = False
            Try
                MyBase.GetConnection()
                MyBase.OpenConnection()
                Dim dateNow As Object = Me.ExecuteScalar("", "SELECT GETDATE()")
                NufarmBussinesRules.SharedClass.ServerDate = dateNow
                MyBase.CloseConnection()
            Catch ex As Exception
                MyBase.CloseConnection()
                Throw ex
            End Try
            Return IsValid
        End Function
        Public Sub ChangePassword(ByVal User_id As String, ByVal PASSWORD As String)
            Try
                Me.CreateCommandSql("", "UPDATE SYST_USERNAME SET [PASSWORD] = '" & MyBase.Enkrip(PASSWORD) & "' WHERE [USER_ID] = '" & User_id & "'")
                Me.OpenConnection()
                'Me.BeginTransaction()
                Me.ExecuteNonQuery()
                'Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Sub IntilializePriviledge(ByVal InitBool As Boolean)
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Distributor = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Distributor = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Distributor = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Agreement = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Agreement = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Agreement = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Agreement = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DistributorAgreement = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DistributorAgreement = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DistributorAgreement = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DistributorAgreement = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AgreementRelation = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AgreementRelation = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AgreementRelation = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AgreementRelation = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DistributorHistory = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DistributorHistory = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DistributorHistory = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DistributorHistory = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Region = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Region = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Region = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Region = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Project = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Project = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Project = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Project = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Program = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Program = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Program = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Program = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Marketing_Program = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Marketing_Program = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Marketing_Program = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Marketing_Program = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Program_BrandPack = InitBool  'eCBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Program_BrandPack = InitBool '
            'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Program_BrandPack = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Program_BrandPack = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Distributor_Include = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Distributor_Include = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Distributor_Include = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Distributor_Include = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            'NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Stepping_Discount = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            'NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Stepping_Discount = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            'NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Stepping_Discount = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            'NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Stepping_Discount = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Acceptance = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Acceptance = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Acceptance = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Acceptance = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OA_BranPack = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OA_BranPack = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.OA_BranPack = InitBool  'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.OA_BranPack = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OrderAcceptance = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OrderAcceptance = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.OrderAcceptance = InitBool 'CBooltblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.OrderAcceptance = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PO = InitBool  'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PO = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PO = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PO = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Purchase_Order = InitBool ' CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Purchase_Order = InitBool  'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Purchase_Order = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Purchase_Order = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscAgreement = InitBool ' CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DiscAgreement = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            'NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscMarketing = InitBool ' CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            'NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DiscMarketing = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            'NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscProject = InitBool ' CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            'NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DiscProject = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.BRAND = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.BRAND = InitBool ' CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.BRAND = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.BRAND = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PACK = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PACK = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PACK = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PACK = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.BRANDPACK_PRICEHISTORY = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.BRANDPACK_PRICEHISTORY = InitBool   ' CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.BRANDPACK_PRICEHISTORY = InitBool ' CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.BRANDPACK_PRICEHISTORY = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.ReportGrid = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.frmReport = InitBool '

            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PODOThirdParty = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PODOThirdParty = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PODOThirdParty = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PODOThirdParty = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.SPPB = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SPPB = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.SPPB = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SPPB = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PODOThirdParty = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.ChangeAgreement = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))

            'NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Distributor_Ship_To = InitBool
            'NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Distributor_Ship_To = InitBool
            'NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Distributor_Ship_To = InitBool
            'NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Distributor_Ship_To = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.SHIP_TO = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SHIP_TO = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.SHIP_TO = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SHIP_TO = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.FrmSendingData = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.FrmSendingData = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.FrmSendingData = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.frmManageDPRD = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.frmManageDPRD = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.frmManageDPRD = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Cancel_PO = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Cancel_PO = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PlantationPrice = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PlantationPrice = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PlantationPrice = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PlantationPrice = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PriceHistory = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Achievement = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Achievement = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Achievement = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Achievement = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Invoice = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Invoice = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Invoice = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Options = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Options = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Options = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Options = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.User = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.User = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.User = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.User = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.EntryPOHeader = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.EntryPOHeader = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.EntryPOHeader = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.EntryPOHeader = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.GONReceivedBack = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.GONReceivedBack = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.GONReceivedBack = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.GONReceivedBack = InitBool

            ' "AreaGON"
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AreaGON = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AreaGON = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AreaGON = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AreaGON = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            ' "Transporter"
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Transporter = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Transporter = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Transporter = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Transporter = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            ' "SPPBEntryGON"
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SPPBEntryGON = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.SPPBEntryGON = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SPPBEntryGON = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.SPPBEntryGON = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))

            'Product Class
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Classification = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Classification = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Classification = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Classification = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
            ' "SPPBEntryGON"
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.ProductClassified = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.ProductClassified = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.ProductClassified = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.ProductClassified = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))

            'DKNational'
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DKNational = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DKNational = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DKNational = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DKNational = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Reminder = InitBool

            'AVGPrice'
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AVGPrice = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AVGPrice = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AVGPrice = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AVGPrice = InitBool 'CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))

            'CPDAuto
            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.CPDAuto = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.CPDAuto = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.CPDAuto = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.CPDAuto = InitBool
            'AdjustmentPKD

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AdjustmentPKD = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AdjustmentPKD = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AdjustmentPKD = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AdjustmentPKD = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DiscDDorDR = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DiscDDorDR = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscPrice = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DiscDDorDR = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.GONWithoutPOMaster = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.GONWithoutPOMaster = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.GONWithoutPOMaster = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.GONWithoutPOMaster = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.ConvertionProduct = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.ConvertionProduct = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.ConvertionProduct = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.ConvertionProduct = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.GonDetailData = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.GonDetailData = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.GonDetailData = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.GonDetailData = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.QtyConvertion = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.QtyConvertion = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.QtyConvertion = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.QtyConvertion = InitBool

            NufarmBussinesRules.User.Privilege.ALLOW_VIEW.OtherProduct = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.OtherProduct = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OtherProduct = InitBool
            NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OtherProduct = InitBool

        End Sub

        Private Sub SetPriviledge(ByVal tblPrivilege As DataTable)
            For i As Integer = 0 To tblPrivilege.Rows.Count - 1
                Dim Form_Name As String = ""
                Form_Name = tblPrivilege.Rows(i)("FORM_NAME").ToString()
                Select Case Form_Name
                    Case "Distributor"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Distributor = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Distributor = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Distributor = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Distributor = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "Agreement"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Agreement = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Agreement = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Agreement = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Agreement = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "DistributorAgreement"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DistributorAgreement = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DistributorAgreement = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DistributorAgreement = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DistributorAgreement = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "AgreementRelation"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AgreementRelation = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AgreementRelation = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AgreementRelation = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AgreementRelation = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "DistributorHistory"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DistributorHistory = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DistributorHistory = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DistributorHistory = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DistributorHistory = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "Region"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Region = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Region = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Region = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Region = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "Project"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Project = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Project = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Project = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Project = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "Program"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Program = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Program = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Program = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Program = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "Marketing_Program"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Marketing_Program = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Marketing_Program = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Marketing_Program = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Marketing_Program = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "Program_BrandPack"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Program_BrandPack = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Program_BrandPack = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Program_BrandPack = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Program_BrandPack = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "Distributor_Include"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Distributor_Include = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Distributor_Include = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Distributor_Include = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Distributor_Include = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        'Case "Stepping_Discount"
                        '    NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Stepping_Discount = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        '    NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Stepping_Discount = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        '    NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Stepping_Discount = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        '    NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Stepping_Discount = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "Acceptance"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Acceptance = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Acceptance = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Acceptance = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Acceptance = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "OA_BranPack"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OA_BranPack = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OA_BranPack = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.OA_BranPack = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.OA_BranPack = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "OrderAcceptance"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OrderAcceptance = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OrderAcceptance = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.OrderAcceptance = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.OrderAcceptance = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "PO"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PO = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PO = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PO = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PO = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "Purchase_Order"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Purchase_Order = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Purchase_Order = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Purchase_Order = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Purchase_Order = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "DiscAgreement"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscAgreement = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DiscAgreement = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        'Case "DiscMarketing"
                        '    NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscMarketing = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        '    NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DiscMarketing = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        'Case "DiscProject"
                        '    NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscProject = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        '    NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DiscProject = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "BRAND"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.BRAND = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.BRAND = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.BRAND = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.BRAND = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "PACK"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PACK = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PACK = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PACK = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PACK = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "BRANDPACK_PRICEHISTORY"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.BRANDPACK_PRICEHISTORY = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.BRANDPACK_PRICEHISTORY = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.BRANDPACK_PRICEHISTORY = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.BRANDPACK_PRICEHISTORY = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "ReportGrid"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.ReportGrid = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "frmReport"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.frmReport = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "ChangeAgreement"
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.ChangeAgreement = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                    Case "Cancel_PO"
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Cancel_PO = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Cancel_PO = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "SMS"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SMS = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SMS = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.SMS = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                    Case "Other_SMS"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Other_SMS = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Other_SMS = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Other_SMS = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                    Case "PODOThirdParty"
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PODOThirdParty = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PODOThirdParty = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PODOThirdParty = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PODOThirdParty = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "SPPB"
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.SPPB = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SPPB = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.SPPB = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SPPB = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        'Case "Distributor_Ship_To"
                        '    NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Distributor_Ship_To = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        '    NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Distributor_Ship_To = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        '    NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Distributor_Ship_To = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        '    NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Distributor_Ship_To = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))

                    Case "SHIP_TO"
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.SHIP_TO = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SHIP_TO = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.SHIP_TO = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SHIP_TO = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "frmManageDPRD"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.frmManageDPRD = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.frmManageDPRD = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.frmManageDPRD = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "FrmSendingData"
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.FrmSendingData = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.FrmSendingData = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.FrmSendingData = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "PlantationPrice"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PlantationPrice = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PlantationPrice = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PlantationPrice = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PlantationPrice = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "PriceHistory"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.PriceHistory = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "Achievement"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Achievement = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Achievement = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Achievement = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Achievement = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "Invoice"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Invoice = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Invoice = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Invoice = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        'NufarmBussinesRules.User.Privilege.ALLOW_DELETE. = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "Options"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Options = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Options = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Options = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Options = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                    Case "User"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.User = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.User = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.User = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.User = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                    Case "PO_Header"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.EntryPOHeader = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.EntryPOHeader = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.EntryPOHeader = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.EntryPOHeader = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "GONReceivedBack"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.GONReceivedBack = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.GONReceivedBack = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.GONReceivedBack = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.GONReceivedBack = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                        'Public AreaGON As Boolean
                        'Public Transporter As Boolean
                        'Public SPPBEntryGON As Boolean
                    Case "AreaGON"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AreaGON = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AreaGON = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AreaGON = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AreaGON = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "Transporter"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Transporter = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Transporter = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Transporter = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Transporter = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "SPPBEntryGON"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.SPPBEntryGON = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.SPPBEntryGON = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SPPBEntryGON = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.SPPBEntryGON = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "Classification"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Classification = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Classification = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Classification = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Classification = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "ProductClassified"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.ProductClassified = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.ProductClassified = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.ProductClassified = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.ProductClassified = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "Reminder"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.Reminder = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                    Case "DKNational"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DKNational = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DKNational = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DKNational = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DKNational = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "AVGPrice"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AVGPrice = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AVGPrice = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AVGPrice = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AVGPrice = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "CPDAuto"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.CPDAuto = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.CPDAuto = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.CPDAuto = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.CPDAuto = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "AdjustmentPKD"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.AdjustmentPKD = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.AdjustmentPKD = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AdjustmentPKD = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AdjustmentPKD = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "DiscountDDorDR"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.DiscDDorDR = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DiscDDorDR = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscPrice = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DiscDDorDR = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "GONWithoutPOMaster"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.GONWithoutPOMaster = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.GONWithoutPOMaster = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.GONWithoutPOMaster = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.GONWithoutPOMaster = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "ConvertionProduct"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.ConvertionProduct = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.ConvertionProduct = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.ConvertionProduct = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.ConvertionProduct = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "GonDetailData"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.GonDetailData = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.GonDetailData = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.GonDetailData = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.GonDetailData = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "OtherProduct"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.OtherProduct = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.OtherProduct = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OtherProduct = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OtherProduct = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                    Case "QtyConvertion"
                        NufarmBussinesRules.User.Privilege.ALLOW_VIEW.QtyConvertion = CBool(tblPrivilege.Rows(i)("ALLOW_VIEW"))
                        NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.QtyConvertion = CBool(tblPrivilege.Rows(i)("ALLOW_UPDATE"))
                        NufarmBussinesRules.User.Privilege.ALLOW_INSERT.QtyConvertion = CBool(tblPrivilege.Rows(i)("ALLOW_INSERT"))
                        NufarmBussinesRules.User.Privilege.ALLOW_DELETE.QtyConvertion = CBool(tblPrivilege.Rows(i)("ALLOW_DELETE"))
                End Select
            Next
        End Sub
        Public Function ValidateUser(ByVal USER_ID As String, ByVal PASSWORD As String) As Boolean
            Try
                PASSWORD = MyBase.Enkrip(PASSWORD)


                Me.CreateCommandSql("Sp_Login_USER", "")
                Me.AddParameter("@USER_ID", SqlDbType.VarChar, USER_ID, 30) ' VARCHAR(30),
                Me.AddParameter("@PASSWORD", SqlDbType.VarChar, PASSWORD, 30) ' VARCHAR(30),
                Me.SqlCom.Parameters.Add("@O_PASSWORD", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output
                'Me.AddParameter("@O_PASSWORD", SqlDbType.VarChar, ParameterDirection.Output) ' VARCHAR(30)OUTPUT,

                'Me.AddParameter("@O_MESSAGE", SqlDbType.VarChar, ParameterDirection.Output)
                Me.SqlCom.Parameters.Add("@O_MESSAGE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output

                'Me.AddParameter("@O_GETDATE", SqlDbType.DateTime, ParameterDirection.Output) ' DATETIME OUTPUT
                Me.SqlCom.Parameters.Add("@O_GETDATE", SqlDbType.SmallDateTime, 0).Direction = ParameterDirection.Output

                'Me.AddParameter("@O_ISADMIN", SqlDbType.Bit, ParameterDirection.Output)
                Me.SqlCom.Parameters.Add("@O_ISADMIN", SqlDbType.Bit, 0).Direction = ParameterDirection.Output
                Me.OpenConnection() : Me.SqlCom.ExecuteNonQuery()

                Dim MESSAGE As String = Me.SqlCom.Parameters()("@O_MESSAGE").Value.ToString()
                If MESSAGE <> "" Then
                    Me.CloseConnection() : Me.ClearCommandParameters() : Throw New System.Exception(MESSAGE)
                End If
                NufarmBussinesRules.User.UserLogin.UserName = USER_ID
                Dim dateNow As Date = Convert.ToDateTime(Me.SqlCom.Parameters()("@O_GETDATE").Value)
                NufarmBussinesRules.SharedClass.ServerDate() = dateNow
                Dim IsITSupport As Boolean = CBool(Me.SqlCom.Parameters()("@O_ISADMIN").Value)

                Me.ClearCommandParameters()
                Dim SettingConfig As New NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting()
                SettingConfig.ReadSettings()
                'Query = "SET NOCOUNT ON;" & vbCrLf & _
                '        "SELECT TypeApp FROM RefBussinesRules WHERE NameApp = 'Discount Quarter Semester Year' AND AllowRules = 1;"
                'Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                'Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'NufarmBussinesRules.SharedClass.DISC_AGREE_FROM = Me.SqlCom.ExecuteScalar().ToString()
                'NufarmBussinesRules.SharedClass.DISC_AGREE_FROM = ConfigurationManager.AppSettings("AgreementDiscount").ToString()

                If IsITSupport Then
                    'Me.CloseConnection()
                    NufarmBussinesRules.User.UserLogin.IsAdmin = True
                    NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.ChangeAgreement = False
                    'Return True
                End If
                'Me.ClearCommandParameters()
                Me.OpenConnection()
                Query = "DBCC SHRINKFILE ('Nufarm_Log' , 1)"
                Me.SqlCom.CommandText = "sp_executesql"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.SqlCom.Parameters.Clear()

                Query = "DBCC SHRINKFILE('Nufarm_Data' , 0, TRUNCATEONLY)"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Me.SqlCom.CommandText = "Sp_Get_User_Privilege"
                Me.AddParameter("@USER_ID", SqlDbType.VarChar, USER_ID, 30)
                Dim tblPrivilege As New DataTable("PRIVILEGE")
                tblPrivilege.Clear() : setDataAdapter(Me.SqlCom).Fill(tblPrivilege)
                Me.ClearCommandParameters()
                If tblPrivilege.Rows.Count <= 0 Then
                    Throw New System.Exception("Acces priviledge doesn't exist")
                End If
                Me.IntilializePriviledge(False) : Me.SetPriviledge(tblPrivilege)
                If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.SPPBEntryGON Or NufarmBussinesRules.User.Privilege.ALLOW_INSERT.GONWithoutPOMaster Then
                    SettingConfig.filDataExpeditions(True)
                End If
                Me.CloseConnection()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return True
        End Function
        Public Sub LogoutUser(ByVal LAST_FORM As String)
            Try
                Me.CreateCommandSql("Sp_Logout_USER", "")
                Me.AddParameter("@USER_ID", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30),
                Me.AddParameter("@LAST_FORM", SqlDbType.VarChar, LAST_FORM, 30) ' VARCHAR(30)
                Me.OpenConnection()
                Me.ExecuteNonQuery()
                Me.CloseConnection()
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace


