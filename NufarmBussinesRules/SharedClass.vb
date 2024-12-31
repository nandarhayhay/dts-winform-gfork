Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports NufarmBussinesRules.common
Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Data
Imports System.Diagnostics

Public Class SharedClass
    Private Shared m_ServerDate As DateTime = DateTime.Now

    Private Shared m_GetDate As String

    Public Shared DBInvoiceTo As SharedClass.CurrentInvToUse

    Private Shared m_tblPoliceTrans As DataTable

    Private Shared m_tblDriverTrans As DataTable

    Private Shared m_tblVol1 As DataTable

    Private Shared m_tblVol2 As DataTable

    Private Shared m_unit1 As DataTable

    Private Shared m_unit2 As DataTable

    Private Shared m_tblUOM As DataTable

    Public Shared OA_REF_NO As String

    Public Shared DISC_AGREE_FROM As String

    Public Shared ListSettings As List(Of SettingConfigurations)

    Public Shared ReadOnly Property GetDate() As String
        Get
            Dim str() As String = {"'", Conversions.ToString(SharedClass.m_ServerDate.Month), "/", Conversions.ToString(SharedClass.m_ServerDate.Day), "/", Conversions.ToString(SharedClass.m_ServerDate.Year), " ", Nothing, Nothing, Nothing, Nothing}
            str(7) = DateTime.Now.Hour.ToString()
            str(8) = ":"
            str(9) = DateTime.Now.Minute.ToString()
            str(10) = "'"
            SharedClass.m_GetDate = String.Concat(str)
            Return SharedClass.m_GetDate
        End Get
    End Property

    Public Shared ReadOnly Property IsUserHO() As Boolean
        Get
            Return Operators.CompareString(ConfigurationManager.AppSettings("IsHO").ToString(), "True", False) = 0
        End Get
    End Property

    Public Shared Property ServerDate() As DateTime
        Get
            'If IsNothing(SharedClass.m_ServerDate) Then
            '    SharedClass.m_ServerDate = DateTime.Now
            'End If
            Return Convert.ToDateTime(SharedClass.m_ServerDate.ToShortDateString())
        End Get
        Set(ByVal value As DateTime)
            Dim num As Integer = Strings.Len(Strings.Trim(value.ToString()))
            Dim num1 As Integer = 1
            Dim str As String = ""
            Dim str1 As String = ""
            While num1 <> num + 1
                str = Strings.Mid(Strings.Trim(value.ToString()), num1, 1)
                If (Operators.CompareString(str, " ", False) <> 0) Then
                    str1 = String.Concat(str1, str)
                    num1 = num1 + 1
                Else
                    Exit While
                End If
            End While
            value = Convert.ToDateTime(str1)
            SharedClass.m_ServerDate = value
        End Set
    End Property

    Public Shared ReadOnly Property ServerDateString() As String
        Get
            Dim str() As String = {"#", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing}
            str(1) = SharedClass.ServerDate.Month.ToString()
            str(2) = "/"
            str(3) = SharedClass.ServerDate.Day.ToString()
            str(4) = "/"
            str(5) = SharedClass.ServerDate.Year.ToString()
            str(6) = "#"
            Return String.Concat(str)
        End Get
    End Property

    Public Shared ReadOnly Property ShowPrice() As Boolean
        Get
            Return Operators.CompareString(ConfigurationManager.AppSettings("ShowPrice").ToString(), "True", False) = 0
        End Get
    End Property

    Public Shared Property tblDriverTrans() As DataTable
        Get
            If (Information.IsNothing(SharedClass.m_tblPoliceTrans)) Then
                SharedClass.m_tblPoliceTrans = New DataTable()
            End If
            Return SharedClass.m_tblPoliceTrans
        End Get
        Set(ByVal value As DataTable)
            SharedClass.m_tblPoliceTrans = value
        End Set
    End Property

    Public Shared Property tblPoliceNumber() As DataTable
        Get
            If (SharedClass.m_tblDriverTrans Is Nothing) Then
                SharedClass.m_tblDriverTrans = New DataTable()
            End If
            Return SharedClass.m_tblDriverTrans
        End Get
        Set(ByVal value As DataTable)
            SharedClass.m_tblDriverTrans = value
        End Set
    End Property

    Public Shared Property tblUOM() As DataTable
        Get
            If (SharedClass.m_tblUOM Is Nothing) Then
                SharedClass.m_tblUOM = New DataTable()
            End If
            Return SharedClass.m_tblUOM
        End Get
        Set(ByVal value As DataTable)
            SharedClass.m_tblUOM = value
        End Set
    End Property

    Public Shared Property tblVo11() As DataTable
        Get
            If (SharedClass.m_tblVol1 Is Nothing) Then
                SharedClass.m_tblVol1 = New DataTable()
            End If
            Return SharedClass.m_tblVol1
        End Get
        Set(ByVal value As DataTable)
            SharedClass.m_tblVol1 = value
        End Set
    End Property

    Public Shared Property tblVol2() As DataTable
        Get
            If (SharedClass.m_tblVol2 Is Nothing) Then
                SharedClass.m_tblVol2 = New DataTable()
            End If
            Return SharedClass.m_tblVol2
        End Get
        Set(ByVal value As DataTable)
            SharedClass.m_tblVol2 = value
        End Set
    End Property

    Public Shared Property unit1() As DataTable
        Get
            If (SharedClass.m_unit1 Is Nothing) Then
                SharedClass.m_unit1 = New DataTable()
            End If
            Return SharedClass.m_unit1
        End Get
        Set(ByVal value As DataTable)
            SharedClass.m_unit1 = value
        End Set
    End Property

    Public Shared Property unit2() As DataTable
        Get
            If (SharedClass.m_unit2 Is Nothing) Then
                SharedClass.m_unit2 = New DataTable()
            End If
            Return SharedClass.m_unit2
        End Get
        Set(ByVal value As DataTable)
            SharedClass.m_unit2 = value
        End Set
    End Property

    Shared Sub New()
        SharedClass.DBInvoiceTo = SharedClass.CurrentInvToUse.NI87
        SharedClass.m_tblDriverTrans = Nothing
        SharedClass.m_tblVol1 = Nothing
        SharedClass.m_tblVol2 = Nothing
        SharedClass.m_unit1 = Nothing
        SharedClass.m_unit2 = Nothing
        SharedClass.m_tblUOM = Nothing
        SharedClass.OA_REF_NO = ""
        SharedClass.DISC_AGREE_FROM = ""
        SharedClass.ListSettings = Nothing
    End Sub

    <DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()
    End Sub

    Public Shared Function ShortGetDate() As String
        Dim str() As String = {"'", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing}
        str(1) = SharedClass.m_ServerDate.Month.ToString()
        str(2) = "/"
        str(3) = SharedClass.m_ServerDate.Day.ToString()
        str(4) = "/"
        str(5) = SharedClass.m_ServerDate.Year.ToString()
        str(6) = "'"
        Return String.Concat(str)
    End Function

    Public Enum CurrentInvToUse
        NI87
        NI109
    End Enum
End Class