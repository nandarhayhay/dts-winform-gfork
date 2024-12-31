Imports System.Data
Imports System.Data.SqlClient
Imports NufarmBussinesRules.SharedClass
Namespace Brandpack
    Public Class CompareItem
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private Query As String = ""
        Public Sub New()
            MyBase.New()
            '--==============UNCOMMENT THIS AFTER NEEDED ================
            'DBInvoiceTo = CurrentInvToUse.NI109
        End Sub
        Public Overloads Sub dispose(ByVal disposing As Boolean)
            Try
                Query = "SET NOCOUNT ON ; " & vbCrLf & _
                        "IF OBJECT_ID('tempdb..##T_BRANDPACK') IS NOT NULL " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "   DROP TABLE tempdb..##T_BRANDPACK ; " & vbCrLf & _
                        " END "
                If Not IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else : Me.CreateCommandSql("sp_executesql", "") : End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
            End Try
            MyBase.Dispose(disposing)
        End Sub

        Public Function GetCompareItems(ByVal Renew As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf
                Dim DtTable As New DataTable() : DtTable.Clear()

                Dim StoredProcNI87 As String = "Usp_Create_Temp_Table_BrandPack", StoredProcNI109 = "Usp_Create_Temp_Table_BrandPack_NI109"
                Dim StoredProcToUse As String = StoredProcNI87
                If DBInvoiceTo = CurrentInvToUse.NI109 Then
                    StoredProcToUse = StoredProcNI109
                End If

                Me.OpenConnection()
                If Renew Then
                    Query &= " IF OBJECT_ID('tempdb..##T_BRANDPACK') IS NULL " & vbCrLf & _
                             " BEGIN EXEC " & StoredProcToUse & " ; END "
                    If Not IsNothing(Me.SqlCom) Then
                        Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Else : Me.CreateCommandSql("sp_executesql", "") : End If
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT * FROM ##T_BRANDPACK BP WHERE NOT EXISTS( " & vbCrLf & _
                            " SELECT BRANDPACK_ID_DTS FROM COMPARE_ITEM WHERE BRANDPACK_ID_DTS = BP.BRANDPACK_ID_DTS " & vbCrLf & _
                            " AND BRANDPACK_ID_ACCPAC = BP.BRANDPACK_ID_ACCPAC) AND BP.BRANDPACK_ID_DTS IS NOT NULL " & vbCrLf & _
                            " AND BP.BRANDPACK_ID_ACCPAC IS NOT NULL AND (BP.BRANDPACK_NAME LIKE '%-D' OR BP.BRANDPACK_NAME LIKE '%- D') ;"
                    'CHECK FILE MANA SAJA YANG DI TEMPORARY BRANDPACK YANG TIDAK ADA DI COMPAREITEM
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)

                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(DtTable)
                    If DtTable.Rows.Count > 0 Then
                        'insert item ke database
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "INSERT INTO COMPARE_ITEM(BRAND_ID_DTS,BRAND_ID_ACCPAC,BRAND_ACCPAC,BRANDPACK_ID_DTS,BRANDPACK_ID_ACCPAC,BRANDPACK_NAME,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                                " VALUES(@BRAND_ID_DTS,@BRAND_ID_ACCPAC,@BRAND_ACCPAC,@BRANDPACK_ID_DTS,@BRANDPACK_ID_ACCPAC,@BRANDPACK_NAME,@CREATE_BY,@CREATE_DATE)"
                        Me.ResetCommandText(CommandType.Text, Query)
                        For i As Integer = 0 To DtTable.Rows.Count - 1
                            Me.AddParameter("@BRAND_ID_DTS", SqlDbType.VarChar, DtTable.Rows(i)("BRAND_ID_DTS"), 7)
                            Me.AddParameter("@BRAND_ID_ACCPAC", SqlDbType.VarChar, DtTable.Rows(i)("BRAND_ID_ACCPAC"), 7)
                            Dim BrandName As String = DtTable.Rows(i)("BRANDPACK_NAME").ToString()
                            BrandName = BrandName.Substring(0, BrandName.IndexOf("@"))
                            Me.AddParameter("@BRAND_ACCPAC", SqlDbType.VarChar, BrandName, 100)
                            Me.AddParameter("@BRANDPACK_ID_DTS", SqlDbType.VarChar, DtTable.Rows(i)("BRANDPACK_ID_DTS"), 14)
                            Me.AddParameter("@BRANDPACK_ID_ACCPAC", SqlDbType.VarChar, DtTable.Rows(i)("BRANDPACK_ID_ACCPAC"), 14)
                            Me.AddParameter("@BRANDPACK_NAME", SqlDbType.VarChar, DtTable.Rows(i)("BRANDPACK_NAME"), 100)
                            Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                            Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Next
                    End If
                End If

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "IF EXISTS(SELECT BRANDPACK_NAME FROM COMPARE_ITEM WHERE BRANDPACK_NAME NOT LIKE '%-D' AND BRANDPACK_NAME NOT LIKE '%- D') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " DELETE FROM COMPARE_ITEM WHERE (BRANDPACK_NAME NOT LIKE '%-D' AND BRANDPACK_NAME NOT LIKE '%- D') ;" & vbCrLf & _
                        " END " & vbCrLf & _
                        " IF EXISTS(SELECT BRAND_ACCPAC FROM COMPARE_ITEM WHERE BRAND_ACCPAC LIKE '%EXP%') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " DELETE FROM COMPARE_ITEM WHERE BRAND_ACCPAC LIKE '%EXP%' ;" & vbCrLf & _
                        " END "
                If Me.SqlCom Is Nothing Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                       "SELECT BRAND_ID_DTS,BRAND_ID_ACCPAC,BRAND_ACCPAC,BRANDPACK_ID_DTS,BRANDPACK_ID_ACCPAC,BRANDPACK_NAME " & vbCrLf & _
                       " FROM COMPARE_ITEM "
                ''reset datatable
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                If Me.SqlDat Is Nothing Then
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                End If
                DtTable.Clear() : Me.SqlDat.Fill(DtTable) : Me.ClearCommandParameters()
                Return DtTable
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace

