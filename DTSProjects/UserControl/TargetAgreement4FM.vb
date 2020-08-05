Public Class TargetAgreement4FM
    Private m_cls_agree As NufarmBussinesRules.DistributorAgreement.Agreement
    Private m_cObject As New cObject
    Private ReadOnly Property cls_agee() As NufarmBussinesRules.DistributorAgreement.Agreement
        Get
            If m_cls_agree Is Nothing Then
                m_cls_agree = New NufarmBussinesRules.DistributorAgreement.Agreement()
            End If
            Return m_cls_agree
        End Get
    End Property
    Private Sub btnAplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyRange.Click

    End Sub
    Private Class cObject

        Public StartDate As Date = NufarmBussinesRules.SharedClass.ServerDate
        Public EndDate As Date
        Public DPDType As String = "Semester"
    End Class

    Private Sub TargetAgreement4FM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim curStartDate As Date = NufarmBussinesRules.SharedClass.ServerDate
            Dim curEndDate As Date = NufarmBussinesRules.SharedClass.ServerDate
            Me.cls_agee.GetCurrentAgreement(curStartDate, curEndDate, True)
            Me.m_cObject = New cObject()
            Me.m_cObject.StartDate = curStartDate
            Me.m_cObject.EndDate = curEndDate
        Catch ex As Exception

        End Try
    End Sub
End Class
