Imports NufarmDataAccesLayer
Imports System.Data.SqlClient

Namespace Brandpack
    Public Class AveragePrice : Inherits DataAccesLayer.ADODotNet

        Public Sub New()
            MyBase.New()
        End Sub
        Protected Query As String = ""
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
        End Sub

        Public Sub Delete(ByVal IDApp As Integer, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "IF EXISTS(SELECT AVGPriceID FROM ACCRUED_HEADER WHERE AVGPriceID = @IDApp) " & vbCrLf & _
                        " BEGIN RAISERROR('Can not delete data, its already used in DPD',16,1); RETURN; END " & vbCrLf & _
                        " DELETE FROM BRND_AVGPRICE WHERE IDApp = @IDApp ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                AddParameter("@IDApp", SqlDbType.Int, IDApp, 0)
                OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        Public Sub SaveData(ByVal OAVG As Nufarm.Domain.AVGPrice, ByVal mustCloseConnection As Boolean)
            Try

                ''agar menginsert cara cepat buat datatable
                Dim dtTable As New DataTable("T_AVG")
                With dtTable
                    .Columns.Add("BRAND_ID", Type.GetType("System.String"))
                    .Columns.Add("START_PERIODE", Type.GetType("System.DateTime"))
                    .Columns.Add("AVGPRICE_FM", Type.GetType("System.Decimal"))
                    .Columns.Add("AVGPRICE_PL", Type.GetType("System.Decimal"))
                End With
                dtTable.AcceptChanges()
                Dim row As DataRow = Nothing
                For i As Integer = 0 To OAVG.ListBrandID.Count - 1
                    row = dtTable.NewRow()
                    row.BeginEdit()
                    row("BRAND_ID") = OAVG.ListBrandID(i)
                    row("START_PERIODE") = OAVG.StartPeriode
                    row("AVGPRICE_FM") = OAVG.AvgPrice_FM
                    row("AVGPRICE_PL") = OAVG.AvgPrice_PL
                    row.EndEdit()
                    dtTable.Rows.Add(row)
                Next
                Dim rows() As DataRow = dtTable.Select("", "", DataViewRowState.Added)
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        " IF NOT EXISTS(SELECT IDApp FROM BRND_AVGPRICE WHERE BRAND_ID = @BRAND_ID AND START_PERIODE = @START_PERIODE) " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " INSERT INTO BRND_AVGPRICE(BRAND_ID,AVGPRICE,AVGPRICE_PL,START_PERIODE,CreatedBy,CreatedDate) " & vbCrLf & _
                        " VALUES(@BRAND_ID,@AVGPRICE,@AVGPRICE_PL,@START_PERIODE,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101) ) ; " & vbCrLf & _
                        " END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                OpenConnection() : Me.SqlTrans = Me.SqlConn.BeginTransaction()
                SqlDat = New SqlDataAdapter()
                With Me.SqlCom
                    .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 7, "BRAND_ID")
                    .Parameters.Add("@START_PERIODE", SqlDbType.SmallDateTime, 0, "START_PERIODE")
                    .Parameters.Add("@AVGPRICE", SqlDbType.Decimal, 0, "AVGPRICE_FM")
                    .Parameters.Add("@AVGPRICE_PL", SqlDbType.Decimal, 0, "AVGPRICE_PL")
                    .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                    .Transaction = Me.SqlTrans
                    SqlDat.InsertCommand = Me.SqlCom
                    SqlDat.Update(rows) : Me.ClearCommandParameters()
                End With
                Me.CommiteTransaction()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                RollbackTransaction() : Me.CloseConnection() : ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function PopulateQuery(ByVal SearchBy As String, ByVal value As Object, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef RowCount As Integer, _
           ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes, ByRef DVBrand As DataView) As DataView
            Try
                Dim ResolvedCriteria As String = common.CommonClass.ResolveCriteria(Criteria, DataType, value)

                Query = "SET NOCOUNT ON; SELECT TOP " & PageSize.ToString() & " * FROM(SELECT ROW_NUMBER() OVER(ORDER BY " & SearchBy & " ASC)AS ROW_NUM, AVG.IDApp,AVG.BRAND_ID,BR.BRAND_NAME,AVG.AVGPRICE AS AVGPRICE_FM,AVG.AVGPRICE_PL,AVG.START_PERIODE,AVG.CreatedDate " & vbCrLf & _
                        " FROM BRND_AVGPRICE AVG INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = AVG.BRAND_ID " & vbCrLf & _
                        " WHERE (" & SearchBy & " " & ResolvedCriteria & " ) "
                Query &= ")Result WHERE ROW_NUM >= " + ((PageSize * (PageIndex - 1)) + 1).ToString() + " AND ROW_NUM <= " + (PageSize * PageIndex).ToString()
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dt As New DataTable("AVERAGE_PRICE")
                OpenConnection()
                setDataAdapter(SqlCom).Fill(dt) : ClearCommandParameters()
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT COUNT(ROW_NUM) FROM(SELECT ROW_NUMBER() OVER(ORDER BY " & SearchBy & " ASC)AS ROW_NUM FROM BRND_AVGPRICE AVG INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = AVG.BRAND_ID WHERE (" & SearchBy & " " & ResolvedCriteria & " ))Result "
                ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                RowCount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()

                If IsNothing(DVBrand) Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT BRAND_ID, BRAND_NAME FROM BRND_BRAND BR WHERE EXISTS(SELECT BRAND_ID FROM VIEW_AGREE_BRAND_INCLUDE WHERE YEAR(END_DATE) > YEAR(GETDATE()) -1 AND BRAND_ID = BR.BRAND_ID); "
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Dim dtBrand As New DataTable("T_BRAND")
                    setDataAdapter(Me.SqlCom).Fill(dtBrand)
                    dtBrand.AcceptChanges() : DVBrand = dtBrand.DefaultView()
                End If
                Me.CloseConnection()
                Return dt.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace

