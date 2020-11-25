Public Class SharedClass
    Private Shared m_ServerDate As DateTime
    Private Shared m_GetDate As String
    Public Shared DBInvoiceTo As CurrentInvToUse = CurrentInvToUse.NI87
    Public Enum CurrentInvToUse
        NI87
        NI109
    End Enum

    Public Shared ReadOnly Property GetDate() As String 'formaT date with full time
        Get
            Dim mydate As String = "'" & m_ServerDate.Month & "/" & m_ServerDate.Day & "/" & m_ServerDate.Year & _
            " " & DateTime.Now.Hour.ToString() + ":" & DateTime.Now.Minute.ToString() & "'"
            m_GetDate = mydate
            Return m_GetDate
        End Get
        'Set(ByVal value As String)
        '    m_GetDate = value
        'End Set
    End Property
    Public Shared Function ShortGetDate() As String
        Return "'" + m_ServerDate.Month.ToString() & "/" & m_ServerDate.Day.ToString() & "/" & m_ServerDate.Year.ToString() & "'"
    End Function
    Public Shared Property ServerDate() As DateTime ' format date with short time
        Get
            If CType(m_ServerDate, Object) Is Nothing Then
                m_ServerDate = DateTime.Now
            End If
            Return Convert.ToDateTime(m_ServerDate.ToShortDateString())
        End Get
        Set(ByVal value As DateTime)
            Dim l As Integer = Len(Trim(value.ToString()))
            Dim w As Integer = 1
            Dim s As String = ""
            Dim a As String = ""
            Do Until w = l + 1
                s = Mid(Trim(value.ToString()), w, 1)
                If s = " " Then
                    Exit Do
                End If
                a = a & s
                w += 1
            Loop
            value = Convert.ToDateTime(a)
            m_ServerDate = value
        End Set
    End Property
    Public Shared ReadOnly Property ServerDateString() As String
        Get
            Return "#" + NufarmBussinesRules.SharedClass.ServerDate.Month.ToString() + "/" + NufarmBussinesRules.SharedClass.ServerDate.Day.ToString() + _
        "/" + NufarmBussinesRules.SharedClass.ServerDate.Year.ToString() + "#"
        End Get
    End Property
    Public Shared OA_REF_NO As String = ""
    Public Shared DISC_AGREE_FROM As String = ""
    Public Shared ListSettings As List(Of common.SettingConfigurations) = Nothing
End Class
