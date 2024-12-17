Imports System.Data
Imports System
Imports NufarmBussinesRules.common.Helper
Imports NufarmBussinesRules.common.CommonClass

Namespace Brandpack
    Public Class OtherProduct
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Protected Query As String = ""
        Public Function PopulateQuery(ByVal SearchBy As String, ByVal value As Object, _
ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer, _
ByVal Criteria As CriteriaSearch, ByVal DataType As DataTypes) As DataView
            Try
                Dim ResolvedCriteria As String = ResolveCriteria(Criteria, DataType, value)
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT TOP " & PageSize.ToString() & " * FROM(SELECT ROW_NUMBER() OVER(ORDER BY IDApp ASC) AS ROW_NUM,IDApp,ITEM,DESCRIPTION,UNIT1,VOL1, UNIT2,VOL2,IsRowMat,UnitOfMeasure,DEVIDED_QUANTITY AS DEVIDED_QTY,InActive, " & vbCrLf & _
                        " CreatedDate,CreatedBy FROM BRND_PROD_OTHER  " & vbCrLf & _
                        " WHERE (" & SearchBy & " " & ResolvedCriteria & " ) " & vbCrLf
                Query &= ")Result WHERE ROW_NUM >= " & ((PageSize * (PageIndex - 1)) + 1).ToString() & " AND ROW_NUM <= " & (PageSize * PageIndex).ToString()
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblOProd As New DataTable("OTHER PRODUCTS")
                OpenConnection()
                setDataAdapter(Me.SqlCom).Fill(tblOProd) : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT COUNT(ROW_NUM) FROM(SELECT ROW_NUMBER() OVER(ORDER BY " & SearchBy & " ASC)AS ROW_NUM FROM BRND_PROD_OTHER WHERE (" & SearchBy & " " & ResolvedCriteria & " ))Result "
                ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                Me.CloseConnection() : Me.ClearCommandParameters()
                Return tblOProd.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub UpdateInActive(ByVal InActive As Boolean, ByVal IDApp As Integer)
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " UPDATE BRND_PROD_OTHER SET INACTIVE = @InActive WHERE IDApp = @IDApp;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@InActive", SqlDbType.Bit, InActive)
                Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CloseConnection()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function Delete(ByVal IDApp As Integer, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "DELETE FROM BRND_PROD_OTHER WHERE IDApp = @IDApp;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)

                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return True
        End Function
        Public Function saveData(ByVal ObjOProd As Nufarm.Domain.OtherProduct, ByVal Mode As SaveMode, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                If Mode = SaveMode.Update Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SET NOCOUNT ON; " & vbCrLf & _
                            " UPDATE BRND_PROD_OTHER SET ITEM = @ITEM,DESCRIPTION = @DESCRIPTION,UNIT1 = @UNIT1,UNIT2 = @UNIT2," & vbCrLf & _
                            "VOL1 = @VOL1,VOL2 = @VOL2,UnitOfMeasure = @UnitOfMeasure,DEVIDED_QUANTITY = @DEVIDED_QUANTITY,ModifiedBy = @ModifiedBy,ModifiedDate = GETDATE()" & vbCrLf & _
                            "WHERE IDApp = @IDApp;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@IDApp", SqlDbType.Int, ObjOProd.IDApp)
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " INSERT INTO BRND_PROD_OTHER(ITEM,DESCRIPTION,UNIT1,VOL1,UNIT2,VOL2,IsRowMat,UnitOfMeasure,INACTIVE,DEVIDED_QUANTITY,CreatedBy,CreatedDate) " & vbCrLf & _
                            " VALUES(@ITEM,@DESCRIPTION,@UNIT1,@VOL1,@UNIT2,@VOL2,0,@UnitOfMeasure,0,@DEVIDED_QUANTITY,@CreatedBy,GETDATE());"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                End If
                Me.OpenConnection()
                Me.AddParameter("@ITEM", SqlDbType.VarChar, ObjOProd.ItemName, 150)
                Me.AddParameter("@DESCRIPTION", SqlDbType.VarChar, ObjOProd.Remark)
                Me.AddParameter("@UNIT1", SqlDbType.VarChar, ObjOProd.Unit1)
                Me.AddParameter("@VOL1", SqlDbType.Decimal, ObjOProd.Vol1)
                Me.AddParameter("@UNIT2", SqlDbType.VarChar, ObjOProd.Unit2)
                Me.AddParameter("@VOL2", SqlDbType.Decimal, ObjOProd.Vol2)
                Me.AddParameter("@UnitOfMeasure", SqlDbType.VarChar, ObjOProd.UOM)
                Me.AddParameter("@DEVIDED_QUANTITY", SqlDbType.Decimal, ObjOProd.Dev_Qty)
                If Mode = SaveMode.Insert Then
                    Me.AddParameter("@CreatedBy", SqlDbType.VarChar, ObjOProd.CreatedBy)
                Else
                    Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, ObjOProd.ModifiedBy)
                End If
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
                Return True
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace