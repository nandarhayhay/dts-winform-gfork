Imports System.Data.SqlClient
Imports NufarmBussinesRules.common.Helper
Namespace OrderAcceptance
    Public Class SPPB
        Inherits PurchaseOrder.PORegistering
        Private clsSPP As NufarmBussinesRules.OrderAcceptance.SPPBEntryGON = Nothing
        Public SPPB_NO As String
        Public SPPBDate As Object
        Public SPPBStatus As String
        Protected Query As String = ""

        Public Sub New()
            MyBase.New()
            Me.SPPB_NO = ""
            Me.SPPBDate = Nothing
        End Sub
      
        Public Function getSPPBDescriptionByPO(ByVal PONumber As String, ByVal mustCloseConnection As Boolean) As DataRow
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT TOP 1 SPPB_NO,SPPB_DATE FROM SPPB_HEADER WHERE PO_REF_NO = @PO_REF_NO ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PONumber, 30)
                Me.OpenConnection()
                Dim tblSPPB As New DataTable("T_SPPB") : Me.setDataAdapter(Me.SqlCom).Fill(tblSPPB)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If tblSPPB.Rows.Count > 0 Then
                    Return tblSPPB.Rows(0)
                End If
                Return Nothing
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function IsExistsSPPBNO(ByVal SPPB_NO As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "SELECT 1 WHERE EXISTS(SELECT SPPB_NO FROM SPPB_HEADER WHERE SPPB_NO = '" & SPPB_NO & "') ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim Retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If

                If Not IsNothing(Retval) And Not IsDBNull(Retval) Then
                    If CInt(Retval) > 0 Then : Return True : End If
                End If
                Return False
            Catch ex As Exception
                Me.ClearCommandParameters() : Me.CloseConnection() : Throw ex
            End Try
        End Function
        Public Function getPO(ByVal SearchPO As String, ByVal Mode As SaveMode, ByVal MustCloseConnection As Boolean) As DataTable
            Try
                'PO_DATE
                'SHIP_TO_SalesPerson()
                'DISTRIBUTOR_NAME()
                'PO_CateGroy()
                If Mode = SaveMode.Insert Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                               " SELECT TOP 100 PO.PO_REF_NO,PO.PO_REF_DATE AS PO_DATE,DR.DISTRIBUTOR_NAME,DR.ADDRESS FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_ORDER_ACCEPTANCE OA " & vbCrLf & _
                               " ON PO.PO_REF_NO = OA.PO_REF_NO INNER JOIN DIST_DISTRIBUTOR DR ON PO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID WHERE ((PO.PO_REF_NO LIKE '%'+@SearchPO+'%') OR (DR.DISTRIBUTOR_NAME LIKE '%'+@SearchPO+'%')) " & vbCrLf & _
                               " AND PO.PO_REF_NO <> ALL(SELECT PO_REF_NO FROM SPPB_HEADER) AND DATEDIFF(MONTH,PO.PO_REF_DATE,GETDATE()) <= 12 AND EXISTS(SELECT OA_ID FROM ORDR_OA_BRANDPACK WHERE OA_ID = OA.OA_ID AND OA_ORIGINAL_QTY > 0);"
                ElseIf Mode = SaveMode.Update Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT TOP 1 PO.PO_REF_NO,PO.PO_REF_DATE AS PO_DATE,DR.DISTRIBUTOR_NAME,DR.ADDRESS FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_ORDER_ACCEPTANCE OA " & vbCrLf & _
                            " ON PO.PO_REF_NO = OA.PO_REF_NO  INNER JOIN DIST_DISTRIBUTOR DR ON PO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID WHERE PO.PO_REF_NO = @SearchPO ;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SearchPO", SqlDbType.VarChar, SearchPO, 30)
                Dim tblPO As New DataTable("T_PO") : tblPO.Clear()
                Me.setDataAdapter(Me.SqlCom).Fill(tblPO) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return tblPO
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace

