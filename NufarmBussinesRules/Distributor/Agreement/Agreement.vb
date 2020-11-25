Imports System.Data
Namespace DistributorAgreement
    Public Class Agreement
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private m_ViewAgrement As DataView
        Private m_ViewBrandPack As DataView
        Public Function CreateViewAgreement() As DataView
            Me.CreateCommandSql("", "SET NOCOUNT ON;SELECT * FROM VIEW_DISTRIBUTOR_AGREEMENT")
            Dim tblAgreement As New DataTable("DATA_AGREEMENT")
            tblAgreement.Clear()
            Me.FillDataTable(tblAgreement)
            Me.m_ViewAgrement = tblAgreement.DefaultView
            Me.m_ViewAgrement.Sort = "DISTRIBUTOR_ID"
            Return Me.m_ViewAgrement
        End Function
        Public Sub GetCurrentAgreement(ByRef StartDate As DateTime, ByRef EndDate As DateTime, ByVal mustCloseConnection As Boolean)
            Try
                Dim Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT TOP 1 START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE END_DATE >= CONVERT(VARCHAR(100),GETDATE(),101); "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteReader() : Me.ClearCommandParameters()
                While Me.SqlRe.Read()
                    StartDate = SqlRe.GetDateTime(0)
                    EndDate = SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close()
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Function CreateViewAgreement(ByVal Start_Date As Object, ByVal End_Date As Object) As DataView
            Try
                Me.CreateCommandSql("Usp_Create_View_Agreement", "")
                If (Not IsNothing(Start_Date)) And (Not IsNothing(End_Date)) Then
                    Me.AddParameter("@START_DATE", SqlDbType.DateTime, Start_Date) ' DATETIME,
                    Me.AddParameter("@END_DATE", SqlDbType.DateTime, End_Date) ' DATETIME
                ElseIf (Not IsNothing(Start_Date)) And (IsNothing(End_Date)) Then
                    Me.AddParameter("@START_DATE", SqlDbType.DateTime, Start_Date) ' DATETIME,
                    Me.AddParameter("@END_DATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME\
                ElseIf (IsNothing(Start_Date)) And (Not IsNothing(End_Date)) Then
                    Me.AddParameter("@START_DATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@END_DATE", SqlDbType.DateTime, End_Date) ' DATETIME
                Else
                    Me.CreateViewAgreement()
                    Return Me.m_ViewAgrement
                End If
                Dim tblAgreement As New DataTable("DATA_AGREEEMENT")
                tblAgreement.Clear()
                Me.FillDataTable(tblAgreement)
                Me.m_ViewAgrement = tblAgreement.DefaultView()
                Return Me.m_ViewAgrement
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Sub New()
            MyBase.New()
            Me.m_ViewAgrement = Nothing
        End Sub
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewAgrement) Then
                Me.m_ViewAgrement.Dispose()
                Me.m_ViewAgrement = Nothing
            End If
        End Sub
        Public Function getTargetDPD(ByVal StartDate As Date, ByVal endDate As Date, ByVal DPDType As String) As DataView
            Try
                Me.OpenConnection()
                If DPDType.ToUpper().Contains("ROUNDUP") Then
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Target_DPD_FMP_Roundup", "")
                    Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Target_DPD_FMP_Roundup")
                    End If
                ElseIf DPDType.ToUpper().Contains("NUFARM") Then
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Target_DPD_FMP_Nufarm", "")
                    Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Target_DPD_FMP_Nufarm")
                    End If
                End If
                Me.AddParameter("@START_DATE", SqlDbType.Date, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.Date, endDate)
                Dim dtTable As New DataTable("TARGET_DPD_4_MONTHS_PERIODE")
                setDataAdapter(Me.SqlCom).Fill(dtTable)
                Return dtTable.DefaultView
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
            Return Nothing
        End Function
        Public ReadOnly Property ViewBrandPack() As DataView
            Get
                Return Me.m_ViewBrandPack
            End Get
        End Property
        Public ReadOnly Property ViewAgreement() As DataView
            Get
                Return Me.m_ViewAgrement
            End Get
        End Property
        Public Function ViewBrandPackInclude() As DataView
            Try
                Me.CreateCommandSql("", "SET NOCOUNT ON;SELECT * FROM VIEW_AGREE_BRANDPACK_INCLUDE")
                Dim TblBrandPack As New DataTable("T_BrandPackInclude")
                TblBrandPack.Clear()
                Me.FillDataTable(TblBrandPack)
                Me.m_ViewBrandPack = TblBrandPack.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewBrandPack
        End Function
    End Class
End Namespace

