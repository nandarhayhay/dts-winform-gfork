Imports System.Data.SqlClient
Namespace Brandpack
    Public Class GeneralPlantationPrice
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Protected Query As String = ""
        Public BRANDPACK_ID As String = ""
        Public PRICE As Decimal = 0, START_DATE As Object = Nothing
        Public MustIncludeDPD As Boolean = False
        Public PRICE_TAG As String = ""
        Public IDApp As Integer = 0
        Public Function PopulateQuery(ByVal SearchBy As String, ByVal value As Object, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef RowCount As Integer, _
           ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes) As DataView
            Dim ResolvedCriteria As String = common.CommonClass.ResolveCriteria(Criteria, DataType, value)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "SELECT TOP " & PageSize.ToString() & " * FROM(SELECT ROW_NUMBER() OVER(ORDER BY GPL.IDApp DESC)AS ROW_NUM,GPL.IDApp,GPL.BRANDPACK_ID, " & vbCrLf & _
                " BP.BRANDPACK_NAME, GPL.PRICE_TAG, GPL.PRICE, GPL.START_DATE, GPL.IncludeDPD, GPL.CreatedDate, GPL.CreatedBy " & vbCrLf & _
                " FROM GEN_PLANT_PRICE GPL INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = GPL.BRANDPACK_ID " & vbCrLf & _
                " WHERE (" & SearchBy & " " & ResolvedCriteria & " ) "
                Query &= ")Result WHERE ROW_NUM >= " + ((PageSize * (PageIndex - 1)) + 1).ToString() + " AND ROW_NUM <= " + (PageSize * PageIndex).ToString()
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dt As New DataTable("GENERAL_PRICE_PLANTATION")
                OpenConnection()
                setDataAdapter(SqlCom).Fill(dt) : ClearCommandParameters()
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT COUNT(ROW_NUM) FROM(SELECT ROW_NUMBER() OVER(ORDER BY GPL.IDApp DESC)AS ROW_NUM FROM GEN_PLANT_PRICE GPL INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = GPL.BRANDPACK_ID " & vbCrLf & _
                            " WHERE (" & SearchBy & " " & ResolvedCriteria & " ))Result "
                ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                RowCount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                Return dt.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function HasReferencedDataPO(ByVal PriceTag As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT PRICE_TAG FROM ORDR_PO_BRANDPACK WHERE PRICE_TAG = @GEN_PLANT_PRICE_TAG AND PRICE_CATEGORY = 'GP';"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@GEN_PLANT_PRICE_TAG", SqlDbType.VarChar, PriceTag)

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : ResetCommandText(CommandType.Text, Query)
                End If
                Me.OpenConnection() : Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) Then
                    Return Convert.ToInt32(retval) > 0
                End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetBrandPack(ByVal SearchString As String, ByVal StartDate As Date) As DataView
            Try
                Dim Query As String = "SET NOCOUNT ON;SELECT BP.BRANDPACK_ID,BP.BRANDPACK_NAME FROM BRND_BRANDPACK BP " & vbCrLf & _
                    " WHERE BP.BRANDPACK_NAME LIKE '%'+@SearchString+'%' AND BP.IsActive = 1 AND (BP.IsObsolete = 0 or BP.IsObsolete IS NULL) " & vbCrLf & _
                    " AND EXISTS(SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE ABI INNER JOIN AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                    " WHERE ABI.BRANDPACK_ID = BP.BRANDPACK_ID AND AA.START_DATE <= @START_DATE AND AA.END_DATE >= @START_DATE)"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SearchString", SqlDbType.NVarChar, SearchString)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.OpenConnection()
                Dim dtTable As New DataTable("T_BrandPack")
                dtTable.Clear() : Me.setDataAdapter(Me.SqlCom).Fill(dtTable) : Me.ClearCommandParameters()
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub UpdateIncludedDPD(ByVal ListPRICE_TAG As List(Of String), ByVal IsIncludedDPD As Boolean)
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                "UPDATE GEN_PLANT_PRICE SET IncludeDPD = @IsIncludeDPD WHERE PRICE_TAG = @PRICE_TAG ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                For i As Integer = 0 To ListPRICE_TAG.Count - 1
                    Me.AddParameter("@PRICE_TAG", SqlDbType.VarChar, ListPRICE_TAG(i), 100)
                    Me.AddParameter("@IsIncludeDPD", SqlDbType.Bit, IsIncludedDPD)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Next
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function DeletePlantationPrice(ByVal IDApp As Integer, ByVal PriceTag As String) As Boolean
            Try
                If String.IsNullOrEmpty(PriceTag) Then
                    Query = "SET NOCOUNT ON ; " & vbCrLf & _
                            "DELETE FROM GEN_PLANT_PRICE WHERE IDApp = @IDApp;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                Else
                    If Me.HasReferencedDataPO(PriceTag, False) Then
                        Me.CloseConnection()
                        System.Windows.Forms.MessageBox.Show("can not delete Data " & vbCrLf & "Data has been used in PO", "Can not delete data", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
                        Return False
                    End If
                    Query = "SET NOCOUNT ON ; " & vbCrLf & _
                            "DELETE FROM GEN_PLANT_PRICE WHERE PRICE_TAG = @PRICE_TAG;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PRICE_TAG", SqlDbType.VarChar, PriceTag, 100)
                End If
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CloseConnection() : Return True
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub SaveData(ByVal Mode As NufarmBussinesRules.common.Helper.SaveMode)
            Try
                If Mode = common.Helper.SaveMode.Insert Then
                    Query = " SET NOCOUNT ON ;" & vbCrLf & _
                            " INSERT INTO GEN_PLANT_PRICE(PRICE_TAG,BRANDPACK_ID," & vbCrLf & _
                            " PRICE,START_DATE,IncludeDPD,CreatedBy,CreatedDate) " & vbCrLf & _
                            " VALUES(@PRICE_TAG,@BRANDPACK_ID,@PRICE,@START_DATE,@IncludeDPD,@CreatedBy,GETDATE()); "
                ElseIf Mode = common.Helper.SaveMode.Update Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "UPDATE GEN_PLANT_PRICE SET BRANDPACK_ID = @BRANDPACK_ID,PRICE = @PRICE, " & vbCrLf & _
                            "START_DATE = @START_DATE,IncludeDPD = @IncludeDPD,ModifiedBy = @ModifiedBy,modifiedDate = GETDATE() WHERE IDApp = @IDApp ;"
                Else
                    Throw New Exception("Mode error")
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, Me.BRANDPACK_ID, 14)
                Me.AddParameter("@PRICE", SqlDbType.Decimal, Me.PRICE)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, START_DATE)
                If Mode = common.Helper.SaveMode.Insert Then
                    Me.AddParameter("@PRICE_TAG", SqlDbType.VarChar, PRICE_TAG)
                    Me.AddParameter("@CreatedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                Else
                    Me.AddParameter("@IDApp", SqlDbType.Int, Me.IDApp)
                    Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                End If
                Me.AddParameter("@IncludeDPD", SqlDbType.Bit, Me.MustIncludeDPD)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
    End Class
End Namespace

