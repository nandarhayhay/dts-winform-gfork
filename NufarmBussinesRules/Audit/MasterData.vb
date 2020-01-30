Imports System.Data.SqlClient
Namespace Audit
    Public Class MasterData
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Public Sub New()
            MyBase.New()
        End Sub
        Dim Query As String = ""
        Public Sub getCurrentPKD(ByRef StartDate As DateTime, ByRef EndDate As DateTime, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SELECT TOP 1 START_DATE, END_DATE FROM AGREE_AGREEMENT WHERE END_DATE >= CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                        " AND START_DATE >= CONVERT(VARCHAR(100),GETDATE(),101) ORDER BY END_DATE DESC;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    StartDate = SqlRe.GetDateTime(0)
                    EndDate = SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close()
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
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
        Public Function getDataPriceFM(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal mustCloseConnection As Boolean)
            Try
                'Query = "SET NOCOUNT ON; " & vbCrLf & _
                '        " IDApp,BP.BRANDPACK_NAME,BPP.PRICE,BPP.START_DATE " & vbCrLf & _
                '        " FROM BRND_PRICE_HISTORY BPP INNER JOIN BRND_BRANDPACK BP ON BPP.BRANDPACK_ID = BP.BRANDPACK_ID; "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Audit_PriceFM", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Audit_PriceFM")
                End If
                Me.AddParameter("@START_ACTION", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_ACTION", SqlDbType.SmallDateTime, EndDate)
                Dim THeader As New DataTable("HEADER_DATA")
                setDataAdapter(Me.SqlCom).Fill(THeader) : Me.ClearCommandParameters()
                Dim tblDetail As DataTable = Me.getTRA("BRND_PRICE_HISTORY", StartDate, EndDate)

                Dim ds As New DataSet("DSPriceFM")
                ds.Tables.Add(THeader)
                ds.Tables.Add(tblDetail)
                ds.Relations.Add("PriceFM_Header_to_Detail", ds.Tables(0).Columns("IDApp"), ds.Tables(1).Columns("IDApp"))
                ds.AcceptChanges()
                Return ds
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function getDataAvPrice(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal mustCloseConnection As Boolean)
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Audit_AVG_Price", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Audit_AVG_Price")
                End If
                Me.AddParameter("@START_ACTION", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_ACTION", SqlDbType.SmallDateTime, EndDate)
                Dim THeader As New DataTable("HEADER_DATA")
                setDataAdapter(Me.SqlCom).Fill(THeader) : Me.ClearCommandParameters()
                Dim tblDetail As DataTable = Me.getTRA("BRND_AVGPRICE", StartDate, EndDate)

                Dim ds As New DataSet("DSAVGPrice")
                ds.Tables.Add(THeader)
                ds.Tables.Add(tblDetail)
                ds.Relations.Add("AVGPrice_Header_to_Detail", ds.Tables(0).Columns("IDApp"), ds.Tables(1).Columns("IDApp"))
                ds.AcceptChanges()
                Return ds
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function getAdjumentPKD(ByVal startDate As DateTime, ByVal EndDate As DateTime, ByVal StartBoundaryPKD As Object, ByVal EndBoundaryPKD As Object, ByVal mustCloseconnection As Boolean)
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Audit_Adjustment_PKD", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Audit_Adjustment_PKD")
                End If
                Dim DefStartPKD As Object = Nothing, DefEndPKD As Object = Nothing
                If IsNothing(StartBoundaryPKD) Then
                    'get Current Periode
                    Dim curMonth As Integer = NufarmBussinesRules.SharedClass.ServerDate.Month
                    If curMonth >= 8 And curMonth <= 12 Then
                        DefStartPKD = New DateTime(NufarmBussinesRules.SharedClass.ServerDate.Year, 8, 1)
                        DefEndPKD = New DateTime((NufarmBussinesRules.SharedClass.ServerDate.Year + 1), 7, 31)
                    Else
                        DefStartPKD = New DateTime(NufarmBussinesRules.SharedClass.ServerDate.Year - 1, 8, 1)
                        DefEndPKD = New DateTime((NufarmBussinesRules.SharedClass.ServerDate.Year), 7, 31)
                    End If
                End If
                Me.AddParameter("@BOUNDARY_START", SqlDbType.SmallDateTime, (IIf(IsNothing(StartBoundaryPKD), DefStartPKD, Convert.ToDateTime(StartBoundaryPKD)))) ' SMALLDATETIME,
                Me.AddParameter("@BOUNDARY_END", SqlDbType.SmallDateTime, (IIf(IsNothing(EndBoundaryPKD), DefEndPKD, Convert.ToDateTime(EndBoundaryPKD)))) ' SMALLDATETIME,
                Me.AddParameter("@START_ACTION", SqlDbType.SmallDateTime, startDate) ' SMALLDATETIME,@START_ACTION SMALLDATETIME,
                Me.AddParameter("@END_ACTION", SqlDbType.SmallDateTime, EndDate) ' SMALLDATETIME,@START_ACTION SMALLDATETIME,
                OpenConnection()
                Dim THeader As New DataTable("HEADER_DATA")
                setDataAdapter(Me.SqlCom).Fill(THeader) : Me.ClearCommandParameters()
                Dim tblDetail As DataTable = Me.getTRA("ADJUSTMENT_DPD", startDate, EndDate)
                Dim ds As New DataSet("DSADJPKD")
                ds.Tables.Add(THeader)
                ds.Tables.Add(tblDetail)
                ds.Relations.Add("AdjPKD_Header_to_Detail", ds.Tables(0).Columns("IDApp"), ds.Tables(1).Columns("IDApp"))
                ds.AcceptChanges()
                Return ds
                If mustCloseconnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function getDataTargetPKD(ByVal startDate As DateTime, ByVal EndDate As DateTime, ByVal StartBoundaryPKD As Object, ByVal EndBoundaryPKD As Object, ByVal mustCloseconnection As Boolean) As DataSet
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Audit_Target_PKD", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Audit_Target_PKD")
                End If
                Dim DefStartPKD As Object = Nothing, DefEndPKD As Object = Nothing
                If IsNothing(StartBoundaryPKD) Then
                    'get Current Periode
                    Dim curMonth As Integer = NufarmBussinesRules.SharedClass.ServerDate.Month
                    If curMonth >= 8 And curMonth <= 12 Then
                        DefStartPKD = New DateTime(NufarmBussinesRules.SharedClass.ServerDate.Year, 8, 1)
                        DefEndPKD = New DateTime((NufarmBussinesRules.SharedClass.ServerDate.Year + 1), 7, 31)
                    Else
                        DefStartPKD = New DateTime(NufarmBussinesRules.SharedClass.ServerDate.Year - 1, 8, 1)
                        DefEndPKD = New DateTime((NufarmBussinesRules.SharedClass.ServerDate.Year), 7, 31)
                    End If
                End If
                Me.AddParameter("@BOUNDARY_START", SqlDbType.SmallDateTime, (IIf(IsNothing(StartBoundaryPKD), DefStartPKD, StartBoundaryPKD)))
                Me.AddParameter("@BOUNDARY_END", SqlDbType.SmallDateTime, (IIf(IsNothing(StartBoundaryPKD), DefEndPKD, StartBoundaryPKD)))
                Me.AddParameter("@START_ACTION", SqlDbType.SmallDateTime, startDate)
                Me.AddParameter("@END_ACTION", SqlDbType.SmallDateTime, EndDate)
                OpenConnection()
                Dim THeader As New DataTable("HEADER_DATA")
                setDataAdapter(Me.SqlCom).Fill(THeader) : Me.ClearCommandParameters()
                'Dim tblDetail As DataTable = Me.getTRA("AGREE_BRAND_INCLUDE", startDate, EndDate)
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT VALUE_ID + ACTION_NAME AS IDApp,DESCRIPTIONS,ACTION_DATE,ACTION_BY FROM TR_AUDIT_LOG WHERE TABLE_NAME = 'AGREE_BRAND_INCLUDE' " & vbCrLf & _
                        " AND CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100),ACTION_DATE,101)) >= @StartDate " & vbCrLf & _
                        " AND CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100),ACTION_DATE,101)) <= @EndDate ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else
                    Me.ResetCommandText(CommandType.Text, Query)
                End If
                Dim TDetail As New DataTable("DETAIL_DATA")
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, startDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Me.setDataAdapter(Me.SqlCom).Fill(TDetail)
                Me.ClearCommandParameters()
                Dim ds As New DataSet("DSPKD")
                ds.Tables.Add(THeader)
                ds.Tables.Add(TDetail)
                ds.Relations.Add("PKD_Header_to_Detail", ds.Tables(0).Columns("IDApp"), ds.Tables(1).Columns("IDApp"))
                ds.AcceptChanges()
                Return ds
                If mustCloseconnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Private Function getTRA(ByVal TableName As String, ByVal StartAction As DateTime, ByVal EndAction As DateTime) As DataTable
            Query = "SET NOCOUNT ON; " & vbCrLf & _
                         "SELECT VALUE_ID + ACTION_NAME AS IDApp,DESCRIPTIONS,ACTION_DATE,ACTION_BY FROM TR_AUDIT_LOG WHERE TABLE_NAME = '" & TableName & "' " & vbCrLf & _
                         " AND CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100),ACTION_DATE,101)) >= @StartDate " & vbCrLf & _
                         " AND CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100),ACTION_DATE,101)) <= @EndDate ;"
            If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
            Else
                Me.ResetCommandText(CommandType.Text, Query)
            End If
            Dim TDetail As New DataTable("DETAIL_DATA")
            Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartAction)
            Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndAction)
            Me.setDataAdapter(Me.SqlCom).Fill(TDetail)
            Me.ClearCommandParameters()
            Return TDetail
        End Function
        Public Function getDataSalesProgram(ByVal startDate As DateTime, ByVal EndDate As DateTime, ByVal mustCloseconnection As Boolean) As DataSet
            Try
                ''query header
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Audit_Sales_Program", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Audit_Sales_Program")
                End If
                Me.AddParameter("@START_ACTION", SqlDbType.SmallDateTime, startDate)
                Me.AddParameter("@END_ACTION", SqlDbType.SmallDateTime, EndDate)
                Me.OpenConnection()
                Dim THeader As New DataTable("HEADER_DATA")
                Me.setDataAdapter(Me.SqlCom).Fill(THeader) : Me.ClearCommandParameters()
         
                Dim ds As New DataSet("DSSALES")
                ds.Tables.Add(THeader)
                Dim tblDetail As DataTable = Me.getTRA("MRKT_BRANDPACK_DISTRIBUTOR", startDate, EndDate)

                ds.Tables.Add(tblDetail)
                ds.Relations.Add("Sales_Header_to_Detail", ds.Tables(0).Columns("IDApp"), ds.Tables(1).Columns("IDApp"))
                ds.AcceptChanges()
                Return ds
                If mustCloseconnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

    End Class
End Namespace

