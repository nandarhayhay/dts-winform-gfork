Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Nufarm.Domain
Imports NufarmBussinesRules.common
Imports NufarmDataAccesLayer.DataAccesLayer
Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices

Namespace Brandpack
    Public Class OtherProduct
        Inherits ADODotNet
        Protected Query As String

        Public Sub New()
            MyBase.New()
            Me.Query = ""
        End Sub

        Public Function Delete(ByVal IDApp As Integer, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Me.Query = "SET NOCOUNT ON; " & vbCrLf & "DELETE FROM BRND_PROD_OTHER WHERE IDApp = @IDApp;"
                If (Not Information.IsNothing(Me.SqlCom)) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If
                Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If (mustCloseConnection) Then
                    Me.CloseConnection()
                End If
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
            Return True
        End Function

        Public Function PopulateQuery(ByVal SearchBy As String, ByVal value As Object, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer, ByVal Criteria As Helper.CriteriaSearch, ByVal DataType As Helper.DataTypes) As DataView
            Dim defaultView As DataView
            Try
                Dim str As String = CommonClass.ResolveCriteria(Criteria, DataType, RuntimeHelpers.GetObjectValue(value))
                Dim query() As String = {"SET NOCOUNT ON; " & vbCrLf & "SELECT TOP ", PageSize.ToString(), " * FROM(SELECT ROW_NUMBER() OVER(ORDER BY IDApp ASC) AS ROW_NUM,IDApp,ITEM,DESCRIPTION,UNIT1,VOL1, UNIT2,VOL2,IsRowMat,UnitOfMeasure,DEVIDED_QUANTITY AS DEVIDED_QTY,InActive, " & vbCrLf & " CreatedDate,CreatedBy FROM BRND_PROD_OTHER  " & vbCrLf & " WHERE (", SearchBy, " ", str, " ) " & vbCrLf & ""}
                Me.Query = String.Concat(query)
                query = New String() {Me.Query, ")Result WHERE ROW_NUM >= ", Nothing, Nothing, Nothing}
                Dim num As Integer = PageSize * (PageIndex - 1) + 1
                query(2) = num.ToString()
                query(3) = " AND ROW_NUM <= "
                query(4) = (PageSize * PageIndex).ToString()
                Me.Query = String.Concat(query)
                If (Not Information.IsNothing(Me.SqlCom)) Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else
                    Me.CreateCommandSql("sp_executesql", "")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                Dim dataTable As System.Data.DataTable = New System.Data.DataTable("OTHER PRODUCTS")
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(dataTable)
                Me.ClearCommandParameters()
                query = New String() {"SET NOCOUNT ON; " & vbCrLf & "SELECT COUNT(ROW_NUM) FROM(SELECT ROW_NUMBER() OVER(ORDER BY ", SearchBy, " ASC)AS ROW_NUM FROM BRND_PROD_OTHER WHERE (", SearchBy, " ", str, " ))Result "}
                Me.Query = String.Concat(query)
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                Rowcount = Conversions.ToInteger(Me.SqlCom.ExecuteScalar())
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                defaultView = dataTable.DefaultView
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
            Return defaultView
        End Function

        Public Function saveData(ByVal ObjOProd As NuFarm.Domain.OtherProduct, ByVal Mode As Helper.SaveMode, ByVal mustCloseConnection As Boolean) As Boolean
            Dim flag As Boolean
            Try
                If (Mode <> Helper.SaveMode.Update) Then
                    Me.Query = "SET NOCOUNT ON;" & vbCrLf & " INSERT INTO BRND_PROD_OTHER(ITEM,DESCRIPTION,UNIT1,VOL1,UNIT2,VOL2,IsRowMat,UnitOfMeasure,INACTIVE,DEVIDED_QUANTITY,CreatedBy,CreatedDate) " & vbCrLf & " VALUES(@ITEM,@DESCRIPTION,@UNIT1,@VOL1,@UNIT2,@VOL2,0,@UnitOfMeasure,0,@DEVIDED_QUANTITY,@CreatedBy,GETDATE());"
                    If (Not Information.IsNothing(Me.SqlCom)) Then
                        Me.ResetCommandText(CommandType.Text, Me.Query)
                    Else
                        Me.CreateCommandSql("sp_executesql", "")
                    End If
                    If (Not Information.IsNothing(Me.SqlCom)) Then
                        Me.ResetCommandText(CommandType.Text, Me.Query)
                    Else
                        Me.CreateCommandSql("", Me.Query)
                    End If
                Else
                    Me.Query = "SET NOCOUNT ON;" & vbCrLf & " SET NOCOUNT ON; " & vbCrLf & " UPDATE BRND_PROD_OTHER SET ITEM = @ITEM,DESCRIPTION = @DESCRIPTION,UNIT1 = @UNIT1,UNIT2 = @UNIT2," & vbCrLf & "VOL1 = @VOL1,VOL2 = @VOL2,UnitOfMeasure = @UnitOfMeasure,DEVIDED_QUANTITY = @DEVIDED_QUANTITY,ModifiedBy = @ModifiedBy,ModifiedDate = GETDATE()" & vbCrLf & "WHERE IDApp = @IDApp;"
                    If (Not Information.IsNothing(Me.SqlCom)) Then
                        Me.ResetCommandText(CommandType.Text, Me.Query)
                    Else
                        Me.CreateCommandSql("", Me.Query)
                    End If
                    Me.AddParameter("@IDApp", SqlDbType.Int, ObjOProd.IDApp)
                End If
                Me.OpenConnection()
                Me.AddParameter("@ITEM", SqlDbType.VarChar, ObjOProd.ItemName, 150)
                Me.AddParameter("@DESCRIPTION", SqlDbType.VarChar, ObjOProd.Remark)
                Me.AddParameter("@UNIT1", SqlDbType.VarChar, ObjOProd.Unit1)
                Me.AddParameter("@VOL1", SqlDbType.[Decimal], ObjOProd.Vol1)
                Me.AddParameter("@UNIT2", SqlDbType.VarChar, ObjOProd.Unit2)
                Me.AddParameter("@VOL2", SqlDbType.[Decimal], ObjOProd.Vol2)
                Me.AddParameter("@UnitOfMeasure", SqlDbType.VarChar, ObjOProd.UOM)
                Me.AddParameter("@DEVIDED_QUANTITY", SqlDbType.[Decimal], ObjOProd.Dev_Qty)
                If (Mode <> Helper.SaveMode.Insert) Then
                    Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, ObjOProd.ModifiedBy)
                Else
                    Me.AddParameter("@CreatedBy", SqlDbType.VarChar, ObjOProd.CreatedBy)
                End If
                Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If (mustCloseConnection) Then
                    Me.CloseConnection()
                End If
                flag = True
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
            Return flag
        End Function

        Public Sub UpdateInActive(ByVal InActive As Boolean, ByVal IDApp As Integer)
            Try
                Me.Query = "SET NOCOUNT ON;" & vbCrLf & " UPDATE BRND_PROD_OTHER SET INACTIVE = @InActive WHERE IDApp = @IDApp;"
                If (Not Information.IsNothing(Me.SqlCom)) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If
                Me.AddParameter("@InActive", SqlDbType.Bit, InActive)
                Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                Me.CloseConnection()
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As System.Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
        End Sub
    End Class
End Namespace