Imports System.Data.SqlClient
Namespace OrderAcceptance
    Public Class AllRemainding
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private m_ViewLeftRemainding As DataView

        Protected Function RepLaceComaWithDot(ByVal text As String) As String
            Dim l As Integer = Len(Trim(text))
            Dim w As Integer = 1
            Dim s As String = ""
            Dim a As String = ""
            Do Until w = l + 1
                s = Mid(Trim(text), w, 1)
                If s = "," Then
                    s = "."
                End If
                a = a & s
                w += 1
            Loop
            Return a
        End Function
        Public Function CreateViewAllRemainding(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String) As DataView
            Try
                Me.baseDataSet = New DataSet()
                Me.baseDataSet.Clear()
                Me.CreateCommandSql("Usp_Get_All_Remainding_By_Brand_ID", "")
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Dim tblLeft As New DataTable("T_Remainding")
                tblLeft.Clear()
                Me.FillDataTable(tblLeft)
                Me.baseDataSet.Tables.Add(tblLeft)
                Me.m_ViewLeftRemainding = tblLeft.DefaultView()
                Me.m_ViewLeftRemainding.Sort = "PO_REF_NO"
                Return Me.m_ViewLeftRemainding
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
    End Class
End Namespace

