Namespace User
    Public Class UserLogin

        Private Shared m_UserName As String
        Public Shared UserPassword As String
        Public Shared HasLogin As Boolean
        Private Shared m_IsAdmin As Boolean
        Public Shared Property UserName() As String
            Get
                'If m_UserName = "" Then
                '    m_UserName = "Admin"
                'End If
                Return m_UserName
            End Get
            Set(ByVal value As String)
                m_UserName = value
            End Set
        End Property
        Public Shared Property IsAdmin() As Boolean
            Get
                'Return True
                Return m_IsAdmin
            End Get
            Set(ByVal value As Boolean)
                m_IsAdmin = value
            End Set
        End Property

        'Public Shared Property UserPassword() As String
        '    Get
        '        Return m_UserName
        '    End Get
        '    Set(ByVal value As String)
        '        m_UserName = value
        '    End Set
        'End Property
        Public Structure ALLOW_INSERT
            Public Distributor As Boolean
            Public Agreement As Boolean
            Public DistributorAgreement As Boolean
            Public DistributorHistory As Boolean
            Public AgreementRelation As Boolean
            Public Region As Boolean
            Public Project As Boolean
            Public Program As Boolean
            Public Marketing_Program As Boolean
            Public Program_BrandPack As Boolean
            Public Distributor_Include As Boolean
            'Public Stepping_Discount As Boolean
            Public Acceptance As Boolean
            Public OA_BranPack As Boolean
            Public OrderAcceptance As Boolean
            Public PO As Boolean
            Public Purchase_Order As Boolean
            Public DiscAgreement As Boolean
            'Public DiscProject As Boolean
            'Public DiscMarketing As Boolean
            Public BRAND As Boolean
            Public PACK As Boolean
            Public BRANDPACK_PRICEHISTORY As Boolean
            Public Other_SMS As Boolean
            Public SMS As Boolean
            'Public Distributor_Ship_To As Boolean
            Public PODOThirdParty As Boolean
            Public SPPB As Boolean
            Public SHIP_TO As Boolean
            Public FrmSendingData As Boolean
            Public frmManageDPRD As Boolean
            Public PlantationPrice As Boolean
            Public PriceHistory As Boolean
            Public Achievement As Boolean
            Public Invoice As Boolean
            Public EntryPOHeader As Boolean
            Public GONReceivedBack As Boolean
            Public AreaGON As Boolean
            Public Transporter As Boolean
            Public SPPBEntryGON As Boolean
            Public ProductClassified As Boolean
            Public Classification As Boolean
            Public DKNational As Boolean
            Public AVGPrice As Boolean
            Public CPDAuto As Boolean
            Public AdjustmentPKD As Boolean
            Public Options As Boolean
            Public User As Boolean
            Public DiscPrice As Boolean
            Public GONWithoutPOMaster As Boolean
            Public ConvertionProduct As Boolean
            Public GonDetailData As Boolean
            Public OtherProduct As Boolean
            Public QtyConvertion As Boolean
            Public GeneralPlantationPrice As Boolean
        End Structure

        Public Structure ALLOW_VIEW
            Public Distributor As Boolean
            Public Agreement As Boolean
            Public DistributorAgreement As Boolean
            Public DistributorHistory As Boolean
            Public AgreementRelation As Boolean
            Public Region As Boolean
            Public Project As Boolean
            Public Program As Boolean
            Public Marketing_Program As Boolean
            Public Program_BrandPack As Boolean
            Public Distributor_Include As Boolean
            'Public Stepping_Discount As Boolean
            Public Acceptance As Boolean
            Public OA_BranPack As Boolean
            Public OrderAcceptance As Boolean
            Public PO As Boolean
            Public Purchase_Order As Boolean
            Public SettingGrid As Boolean
            Public DiscAgreement As Boolean
            'Public DiscProject As Boolean
            'Public DiscMarketing As Boolean
            Public BRAND As Boolean
            Public PACK As Boolean
            Public BRANDPACK_PRICEHISTORY As Boolean
            Public ReportGrid As Boolean
            Public frmReport As Boolean
            Public SMS As Boolean
            Public Other_SMS As Boolean
            Public Cancel_PO As Boolean
            'Public Distributor_Ship_To As Boolean
            Public PODOThirdParty As Boolean
            Public SPPB As Boolean
            Public SHIP_TO As Boolean
            Public FrmSendingData As Boolean
            Public frmManageDPRD As Boolean
            Public PlantationPrice As Boolean
            Public PriceHistory As Boolean
            Public Achievement As Boolean
            Public Invoice As Boolean
            Public Options As Boolean
            Public EntryPOHeader As Boolean
            Public GONReceivedBack As Boolean
            Public AreaGON As Boolean
            Public Transporter As Boolean
            Public SPPBEntryGON As Boolean
            Public ProductClassified As Boolean
            Public Classification As Boolean
            Public Reminder As Boolean
            Public DKNational As Boolean
            Public AVGPrice As Boolean
            Public CPDAuto As Boolean
            Public AdjustmentPKD As Boolean
            Public User As Boolean
            Public DiscDDorDR As Boolean
            Public GONWithoutPOMaster As Boolean
            Public ConvertionProduct As Boolean
            Public GonDetailData As Boolean
            Public OtherProduct As Boolean
            Public QtyConvertion As Boolean

        End Structure

        Public Structure ALLOW_UPDATE
            Public Distributor As Boolean
            Public Agreement As Boolean
            Public DistributorAgreement As Boolean
            Public DistributorHistory As Boolean
            Public AgreementRelation As Boolean
            Public Region As Boolean
            Public Project As Boolean
            Public Program As Boolean
            Public Marketing_Program As Boolean
            Public Program_BrandPack As Boolean
            Public Distributor_Include As Boolean
            'Public Stepping_Discount As Boolean
            Public Acceptance As Boolean
            Public OA_BranPack As Boolean
            Public OrderAcceptance As Boolean
            Public PO As Boolean
            Public Purchase_Order As Boolean
            Public DiscAgreement As Boolean
            'Public DiscProject As Boolean
            'Public DiscMarketing As Boolean
            Public BRAND As Boolean
            Public PACK As Boolean
            Public BRANDPACK_PRICEHISTORY As Boolean
            Public Cancel_PO As Boolean
            Public ChangeAgreement As Boolean
            Public SMS As Boolean
            Public Other_SMS As Boolean
            'Public Distributor_Ship_To As Boolean
            Public PODOThirdParty As Boolean
            Public SPPB As Boolean
            Public SHIP_TO As Boolean
            Public FrmSendingData As Boolean
            Public frmManageDPRD As Boolean
            Public PlantationPrice As Boolean
            Public PriceHistory As Boolean
            Public Achievement As Boolean
            Public Invoice As Boolean
            Public Options As Boolean
            Public EntryPOHeader As Boolean
            Public GONReceivedBack As Boolean
            Public AreaGON As Boolean
            Public Transporter As Boolean
            Public SPPBEntryGON As Boolean
            Public ProductClassified As Boolean
            Public Classification As Boolean
            Public DKNational As Boolean
            Public AVGPrice As Boolean
            Public CPDAuto As Boolean
            Public AdjustmentPKD As Boolean
            Public User As Boolean
            Public DiscDDorDR As Boolean
            Public GONWithoutPOMaster As Boolean
            Public ConvertionProduct As Boolean
            Public GonDetailData As Boolean
            Public OtherProduct As Boolean
            Public QtyConvertion As Boolean

        End Structure

        Public Structure ALLOW_DELETE
            Public Distributor As Boolean
            Public Agreement As Boolean
            Public DistributorAgreement As Boolean
            Public DistributorHistory As Boolean
            Public AgreementRelation As Boolean
            Public Region As Boolean
            Public Project As Boolean
            Public Program As Boolean
            Public Marketing_Program As Boolean
            Public Program_BrandPack As Boolean
            Public Distributor_Include As Boolean
            'Public Stepping_Discount As Boolean
            Public Acceptance As Boolean
            Public OA_BranPack As Boolean
            Public OrderAcceptance As Boolean
            Public PO As Boolean
            Public Purchase_Order As Boolean
            Public DiscAgreement As Boolean
            'Public DiscProject As Boolean
            'Public DiscMarketing As Boolean
            Public BRAND As Boolean
            Public PACK As Boolean
            Public BRANDPACK_PRICEHISTORY As Boolean
            'Public Distributor_Ship_To As Boolean
            Public PODOThirdParty As Boolean
            Public SPPB As Boolean
            Public SHIP_TO As Boolean
            Public PlantationPrice As Boolean
            Public PriceHistory As Boolean
            Public Achievement As Boolean
            Public EntryPOHeader As Boolean
            Public GONReceivedBack As Boolean
            Public AreaGON As Boolean
            Public Transporter As Boolean
            Public SPPBEntryGON As Boolean
            Public ProductClassified As Boolean
            Public Classification As Boolean
            Public DKNational As Boolean
            Public AVGPrice As Boolean
            Public CPDAuto As Boolean
            Public AdjustmentPKD As Boolean
            Public Options As Boolean
            Public User As Boolean
            Public DiscDDorDR As Boolean
            Public GONWithoutPOMaster As Boolean
            Public ConvertionProduct As Boolean
            Public GonDetailData As Boolean
            Public OtherProduct As Boolean
            Public QtyConvertion As Boolean

        End Structure
    End Class

    Public Class Privilege
        Public Shared ALLOW_VIEW As NufarmBussinesRules.User.UserLogin.ALLOW_VIEW
        Public Shared ALLOW_INSERT As NufarmBussinesRules.User.UserLogin.ALLOW_INSERT
        Public Shared ALLOW_UPDATE As NufarmBussinesRules.User.UserLogin.ALLOW_UPDATE
        Public Shared ALLOW_DELETE As NufarmBussinesRules.User.UserLogin.ALLOW_DELETE
    End Class
End Namespace

