Imports System.Data.SqlClient
Namespace OrderAcceptance
    Public Class ThirdParty
        Inherits PurchaseOrder.PORegistering

#Region " Deklarasi "

        Private m_ViewSPPB_NO As DataView
        Private m_ViewThirdParty As DataView
        Public OTP_NO As Object
        Public OTP_DATE As Object
        Private m_View_DOTP As DataView
        Private m_View_OTP As DataView
        Public DO_TP_NO As Object
        Public DO_TP_DATE As Object
        'Public PO_REF_NO As String
        Private m_ViewBrandPackSPPB As DataView
        Public SPPB_NO As String
#End Region

#Region " Property "

        Public ReadOnly Property ViewBrandPackSPPB() As DataView
            Get
                Return Me.m_ViewBrandPackSPPB
            End Get
        End Property
        Public ReadOnly Property ViewDOTP() As DataView
            Get
                Return Me.m_View_DOTP
            End Get
        End Property

        Public ReadOnly Property ViewOTP() As DataView
            Get
                Return Me.m_View_OTP
            End Get
        End Property

        Public ReadOnly Property ViewThirdParty() As DataView
            Get
                Return Me.m_ViewThirdParty
            End Get
        End Property

        Public ReadOnly Property ViewSPPB_NO() As DataView
            Get
                Return Me.m_ViewSPPB_NO
            End Get
        End Property

        Public ReadOnly Property GetDataSet() As DataSet
            Get
                Return Me.baseDataSet
            End Get
        End Property

#End Region

#Region " Constructor & Destructor "

        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_View_DOTP) Then
                Me.m_View_DOTP.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_View_DOTP.Dispose()
                Me.m_View_DOTP = Nothing
            End If
            If Not IsNothing(Me.m_View_OTP) Then
                Me.m_View_OTP.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_View_OTP.Dispose()
                Me.m_View_OTP = Nothing
            End If
            If Not IsNothing(Me.m_ViewSPPB_NO) Then
                Me.m_View_OTP.Dispose()
                Me.m_ViewSPPB_NO = Nothing
            End If
            If Not IsNothing(Me.m_ViewThirdParty) Then
                Me.m_ViewThirdParty.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_ViewThirdParty.Dispose()
                Me.m_ViewThirdParty = Nothing
            End If
            If Not IsNothing(Me.m_ViewBrandPackSPPB) Then
                Me.m_ViewBrandPackSPPB.Dispose()
                Me.m_ViewBrandPackSPPB = Nothing
            End If

        End Sub

        Public Sub New()
            MyBase.New()
            Me.m_View_DOTP = Nothing
            Me.m_View_OTP = Nothing
            Me.m_ViewThirdParty = Nothing
            Me.m_ViewSPPB_NO = Nothing
            Me.m_ViewSPPB_NO = Nothing
        End Sub

#End Region

#Region " SUB "

        Public Sub GetRowDataBySPPB_NO(ByVal SPPB_NO As String)
            Try
                Me.CreateCommandSql("Usp_Get_Item_Description_SPPB_NO", "")
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 15)
                Me.AddParameter("@O_OTP_NO", SqlDbType.VarChar, ParameterDirection.Output)
                Me.SqlCom.Parameters()("@O_OTP_NO").Size = 15
                Me.AddParameter("@O_OTP_DATE", SqlDbType.DateTime, ParameterDirection.Output) ' DATETIME OUTPUT,
                Me.AddParameter("@O_DO_TP_NO", SqlDbType.VarChar, ParameterDirection.Output)
                Me.SqlCom.Parameters()("@O_DO_TP_NO").Size = 15 ' VARCHAR(15) OUTPUT,
                Me.AddParameter("@O_DO_TP_DATE", SqlDbType.DateTime, ParameterDirection.Output) ' DATETIME OUTPUT

                Me.OpenConnection()
                Me.SqlCom.ExecuteNonQuery()
                Me.CloseConnection()
                Me.OTP_NO = Me.SqlCom.Parameters()("@O_OTP_NO").Value
                Me.OTP_DATE = Me.SqlCom.Parameters()("@O_OTP_DATE").Value
                Me.DO_TP_NO = Me.SqlCom.Parameters()("@O_DO_TP_NO").Value
                Me.DO_TP_DATE = Me.SqlCom.Parameters()("@O_DO_TP_DATE").Value

            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Public Sub GetData(ByVal OTP_NO As String)
            Try
                Me.CreateCommandSql("Usp_Get_Data_PO_Third_Party", "")
                Me.AddParameter("@OTP_NO", SqlDbType.VarChar, OTP_NO, 15)
                Dim tblPOThirdParty As New DataTable("OTP_DETAIL")
                tblPOThirdParty.Clear()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection()
                Me.SqlDat.Fill(tblPOThirdParty)
                Me.ClearCommandParameters()
                Me.baseDataSet = New DataSet()
                Me.baseDataSet.Clear()
                Me.baseDataSet.Tables.Add(tblPOThirdParty)

                Me.SqlCom.CommandText = "Usp_Get_Data_DO_Third_Party"
                Me.AddParameter("@OTP_NO", SqlDbType.VarChar, OTP_NO, 15) ' VARCHAR(15)
                Me.SqlCom.CommandType = CommandType.StoredProcedure
                Dim tblDOThirdParty As New DataTable("DO_TP_DETAIL")
                tblDOThirdParty.Clear()
                Me.SqlDat.Fill(tblDOThirdParty)
                Me.CloseConnection()
                Me.baseDataSet.Tables.Add(tblDOThirdParty)
                Me.m_View_DOTP = Me.baseDataSet.Tables("DO_TP_DETAIL").DefaultView()
                Me.m_View_DOTP.RowStateFilter = DataViewRowState.CurrentRows
                Me.m_View_DOTP.Sort = "DO_TP_BRANDPACK"
                Me.m_View_OTP = Me.baseDataSet.Tables("OTP_DETAIL").DefaultView()
                Me.m_View_OTP.RowStateFilter = DataViewRowState.CurrentRows
                Me.m_View_OTP.Sort = "OTP_BRANDPACK"

            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Public Sub SaveData(ByVal SaveType As String, ByVal ds As DataSet, Optional ByVal dsHaschanged As Boolean = False)
            Try
                Select Case SaveType
                    Case "Insert"
                        Me.CreateCommandSql("Usp_Insert_ORDR_OTP_HEADER", "")
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30
                    Case "Update"
                        Me.CreateCommandSql("Usp_Update_ORDR_OTP_HEADER", "")
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(15),
                End Select
                Me.AddParameter("@OTP_NO", SqlDbType.VarChar, Me.OTP_NO, 15) ' VARCHAR(15),
                Me.AddParameter("@OTP_DATE", SqlDbType.DateTime, Me.OTP_DATE) ' DATETIME,
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, Me.SPPB_NO, 25) ' VARCHAR(25),
                'Me.SqlCom.CommandType = CommandType.StoredProcedure
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                If Me.DO_TP_NO <> "" Then
                    Me.SqlCom.CommandText = "SELECT COUNT(DO_TP_NO) FROM DO_TP_HEADER WHERE DO_TP_NO = '" & DO_TP_NO & "'"
                    Me.SqlCom.CommandType = CommandType.Text
                    If CInt(Me.SqlCom.ExecuteScalar()) > 0 Then
                        'UPDATE DATA
                        Me.SqlCom.CommandText = "Usp_Update_DO_Third_Party_Header"
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30)
                    Else
                        Me.SqlCom.CommandText = "Usp_Insert_DO_Third_Party_Header"
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(3
                    End If
                    Me.AddParameter("@DO_TP_NO", SqlDbType.VarChar, Me.DO_TP_NO, 15) ' VARCHAR(15),
                    Me.AddParameter("@DO_TP_DATE", SqlDbType.DateTime, Me.DO_TP_DATE) ' DATETIME,
                    Me.AddParameter("@OTP_NO", SqlDbType.VarChar, Me.OTP_NO) ' VARCHAR(25),
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                    Me.SqlCom.ExecuteNonQuery()
                    Me.ClearCommandParameters()
                End If
                If dsHaschanged Then
                    Me.SqlDat = New SqlDataAdapter() : Dim InsertedRows() As DataRow = Nothing
                    Dim UpdatedRows() As DataRow = Nothing, DeletedRows() As DataRow = Nothing

                    Dim CommandInsert As SqlCommand = Me.SqlConn.CreateCommand()
                    CommandInsert.CommandText = "INSERT INTO OTP_DETAIL (OTP_NO,OA_BRANDPACK_ID,OTP_BRANDPACK," _
                    & "OTP_QTY,CREATE_BY,CREATE_DATE)" & vbCrLf & _
                    "VALUES(@OTP_NO,@OA_BRANDPACK_ID,@OTP_BRANDPACK,@OTP_QTY,@CREATE_BY,@CREATE_DATE)"
                    With CommandInsert
                        .Parameters.Add("@OTP_NO", SqlDbType.VarChar, 15).SourceColumn = "OTP_NO"
                        .Parameters.Add("@OA_BRANDPACK_ID", SqlDbType.VarChar, 70).SourceColumn = "OA_BRANDPACK_ID"
                        .Parameters.Add("@OTP_BRANDPACK", SqlDbType.VarChar, 85).SourceColumn = "OTP_BRANDPACK"
                        .Parameters.Add("@OTP_QTY", SqlDbType.Decimal, 0).SourceColumn = "OTP_QTY"
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).SourceColumn = "CREATE_BY"
                        .Parameters.Add("@CREATE_DATE", SqlDbType.DateTime).SourceColumn = "CREATE_DATE"
                    End With
                    Dim CommandUpdate As SqlCommand = Me.SqlConn.CreateCommand()
                    CommandUpdate.CommandText = "UPDATE OTP_DETAIL SET OTP_QTY = @OTP_QTY,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = GETDATE() WHERE OTP_BRANDPACK = @OTP_BRANDPACK"
                    With CommandUpdate
                        .Parameters.Add("@OTP_QTY", SqlDbType.Decimal, 0).SourceColumn = "OTP_QTY"
                        .Parameters.Add("MODIFY_BY", SqlDbType.VarChar, 50).SourceColumn = "MODIFY_BY"
                        '.Parameters.Add("@MODIFY_DATE", SqlDbType.DateTime).SourceColumn = "MODIFY_DATE"
                        .Parameters.Add("@OTP_BRANDPACK", SqlDbType.VarChar, 85).SourceColumn = "OTP_BRANDPACK"
                        .Parameters("@OTP_BRANDPACK").SourceVersion = DataRowVersion.Original
                    End With
                    Dim CommandDelete As SqlCommand = Me.SqlConn.CreateCommand()
                    CommandDelete.CommandText = "DELETE FROM OTP_DETAIL WHERE OTP_BRANDPACK = @OTP_BRANDPACK"
                    With CommandDelete
                        .Parameters.Add("@OTP_BRANDPACK", SqlDbType.VarChar, 85).SourceColumn = "OTP_BRANDPACK"
                        .Parameters("@OTP_BRANDPACK").SourceVersion = DataRowVersion.Original
                    End With
                    CommandInsert.Transaction = Me.SqlTrans : CommandUpdate.Transaction = Me.SqlTrans
                    CommandDelete.Transaction = Me.SqlTrans
                    Me.SqlDat.InsertCommand = CommandInsert : Me.SqlDat.UpdateCommand = CommandUpdate
                    Me.SqlDat.DeleteCommand = CommandDelete
                    InsertedRows = ds.Tables(0).Select("", "", DataViewRowState.Added)
                    Me.SqlDat.Update(InsertedRows)
                    UpdatedRows = ds.Tables(0).Select("", "", DataViewRowState.ModifiedCurrent)
                    Me.SqlDat.Update(UpdatedRows)
                    UpdatedRows = ds.Tables(0).Select("", "", DataViewRowState.ModifiedOriginal)
                    Me.SqlDat.Update(UpdatedRows)
                    DeletedRows = ds.Tables(0).Select("", "", DataViewRowState.Deleted)
                    Me.SqlDat.Update(DeletedRows)
                    CommandInsert.Parameters.Clear() : CommandUpdate.Parameters.Clear() : CommandDelete.Parameters.Clear()
                    InsertedRows = ds.Tables(1).Select("", "", DataViewRowState.Added)
                    If InsertedRows.Length > 0 Then
                        CommandInsert.CommandText = "INSERT INTO DO_TP_DETAIL(DO_TP_BRANDPACK,DO_TP_NO,OA_BRANDPACK_ID," _
                        & " DO_TP_QTY,CREATE_BY,CREATE_DATE)" & vbCrLf & _
                          " VALUES(@DO_TP_BRANDPACK,@DO_TP_NO,@OA_BRANDPACK_ID,@DO_TP_QTY,@CREATE_BY,@CREATE_DATE)"
                        With CommandInsert
                            .Parameters.Add("@DO_TP_BRANDPACK", SqlDbType.VarChar, 85).SourceColumn = "DO_TP_BRANDPACK"
                            .Parameters.Add("@DO_TP_NO", SqlDbType.VarChar, 150).SourceColumn = "DO_TP_NO"
                            .Parameters.Add("@OA_BRANDPACK_ID", SqlDbType.VarChar, 70).SourceColumn = "OA_BRANDPACK_ID"
                            .Parameters.Add("@DO_TP_QTY", SqlDbType.Decimal, 0).SourceColumn = "DO_TP_QTY"
                            .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).SourceColumn = "CREATE_BY"
                            .Parameters.Add("@CREATE_DATE", SqlDbType.DateTime).SourceColumn = "CREATE_DATE"
                        End With
                        'InsertedRows = ds.Tables(1).Select("", "", DataViewRowState.Added)
                        Me.SqlDat.Update(InsertedRows)
                    End If
                    DeletedRows = ds.Tables(1).Select("", "", DataViewRowState.Deleted)
                    If (DeletedRows.Length > 0) Then
                        CommandDelete.CommandText = "DELETE FROM DO_TP_DETAIL WHERE DO_TP_BRANDPACK = @DO_TP_BRANDPACK"
                        CommandDelete.Parameters.Add("@DO_TP_BRANDPACK", SqlDbType.VarChar, 85).SourceColumn = "DO_TP_BRANDPACK"
                        CommandDelete.Parameters("@DO_TP_BRANDPACK").SourceVersion = DataRowVersion.Original
                        Me.SqlDat.Update(DeletedRows)
                    End If
                    UpdatedRows = ds.Tables(1).Select("", "", DataViewRowState.ModifiedOriginal)
                    If (UpdatedRows.Length > 0) Then
                        CommandUpdate.CommandText = "UPDATE DO_TP_DETAIL SET DO_TP_QTY = @DO_TP_QTY,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = GETDATE()" & vbCrLf & _
                        " WHERE DO_TP_BRANDPACK = @DO_TP_BRANDPACK"
                        With CommandUpdate
                            .Parameters.Add("@DO_TP_QTY", SqlDbType.Decimal, 0).SourceColumn = "DO_TP_QTY"
                            .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).SourceColumn = "CREATE_BY"
                            .Parameters.Add("@DO_TP_BRANDPACK", SqlDbType.VarChar, 85).SourceColumn = "DO_TP_BRANDPACK"
                            .Parameters("@DO_TP_BRANDPACK").SourceVersion = DataRowVersion.Original
                        End With
                        Me.SqlDat.Update(UpdatedRows)
                    End If
                    UpdatedRows = ds.Tables(1).Select("", "", DataViewRowState.ModifiedCurrent)
                    If (UpdatedRows.Length > 0) Then
                        Me.SqlDat.Update(UpdatedRows)
                    End If
                    'Me.SqlCom.CommandText = "SELECT * FROM OTP_DETAIL WHERE OTP_NO = '" & OTP_NO & "'"
                    'Me.SqlCom.CommandType = CommandType.Text

                    'Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                    'Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                    'Me.SqlDat.Update(ds.Tables("OTP_DETAIL"))
                    'If Me.DO_TP_NO <> "" Then
                    '    Me.SqlCom.CommandText = "SELECT * FROM DO_TP_DETAIL WHERE DO_TP_NO = '" & DO_TP_NO & "'"
                    '    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                    '    Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                    '    Me.SqlDat.Update(ds.Tables("DO_TP_DETAIL"))
                    'End If
                End If
                Me.CommiteTransaction() : Me.CloseConnection()
                System.Windows.Forms.MessageBox.Show("Data Saved succesfuly", "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Public Overloads Sub Delete(ByVal OA_BRANDPACK_ID As String)
            Try
                Me.CreateCommandSql("Usp_Delete_ORDR_OTP_BrandPack_By_OA_BrandPack", "")
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Public Sub DeleteDOThirdParty(ByVal OA_BRANDPACK_ID As String)
            Try
                Me.CreateCommandSql("Usp_Delete_DO_TP_DETAIL", "")
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try

        End Sub
#End Region

#Region " Function "

        Public Function CreateViewBrandPackSPPB(ByVal SPPB_NO As String) As DataView
            Try
                Me.CreateCommandSql("Usp_Get_BrandPack_ID_By_SPPB", "")
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 15)
                Dim tblBrandPack As New DataTable("BRANDPACK")
                tblBrandPack.Clear()
                Me.FillDataTable(tblBrandPack)
                Me.m_ViewBrandPackSPPB = tblBrandPack.DefaultView()
                Me.m_ViewBrandPackSPPB.Sort = "BRANDPACK_NAME"
                Return Me.m_ViewBrandPackSPPB
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Sub getPODescription(ByVal SPPB_NO As String, ByRef OA_ID As String, ByRef PO_REF_NO As String)
            Try
                Dim Query As String = "SET NOCOUNT ON;SELECT TOP 1 OOB.OA_ID,OPB.PO_REF_NO FROM ORDR_OA_BRANDPACK OOB INNER JOIN ORDR_PO_BRANDPACK OPB " _
                & " ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID WHERE OOB.OA_BRANDPACK_ID IN(SELECT OA_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE SPPB_NO = '" + SPPB_NO + "')"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteReader()
                While Me.SqlRe.Read
                    OA_ID = Me.SqlRe("OA_ID").ToString()
                    PO_REF_NO = Me.SqlRe("PO_REF_NO").ToString()
                End While
                Me.SqlRe.Close() : Me.CloseConnection() : Me.ClearCommandParameters()
            Catch ex As Exception
                If (Me.SqlRe.IsClosed = False) Then
                    Me.SqlRe.Close()
                End If
                Me.CloseConnection() : Throw ex
            End Try
        End Sub
        Public Function CreateViewThirdParty(ByVal DISTRIBUTOR_ID As Object, ByVal FROM_PO_DATE As Object, ByVal UNTIL_PO_DATE As Object)
            Try
                Me.CreateCommandSql("Usp_Create_View_Third_Party", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Me.AddParameter("@FROM_SPPB_DATE", SqlDbType.DateTime, FROM_PO_DATE)
                Me.AddParameter("@UNTIL_SPPB_DATE", SqlDbType.DateTime, UNTIL_PO_DATE)
                Dim tblThirdParty As New DataTable("ThirdParty")
                tblThirdParty.Clear()
                Me.FillDataTable(tblThirdParty)
                Me.m_ViewThirdParty = tblThirdParty.DefaultView()
                Return Me.m_ViewThirdParty
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function CreateViewSPPB_NO(ByVal DISTRIBUTOR_ID As String, ByVal IsRegisteredSPPB As Boolean) As DataView
            Try
                Me.CreateCommandSql("Usp_Create_View_SPPB_NO_ThirdParty_By_Distributor_ID", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                'If IsRegisteredSPPB Then
                '    Me.AddParameter("@IS_REGISTERED", SqlDbType.VarChar, 1) ' BIT
                'Else
                '    Me.AddParameter("@IS_REGISTERED", SqlDbType.VarChar, 0)
                'End If
                Dim tblOA As New DataTable("SPPB")
                tblOA.Clear() : Me.FillDataTable(tblOA)
                Me.m_ViewSPPB_NO = tblOA.DefaultView() : m_ViewSPPB_NO.Sort = "SPPB_NO DESC"
                'Me.m_ViewSPPB_NO.Sort = "SPPB_NO"
                Return Me.m_ViewSPPB_NO
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function EXISTS_OTP_BRANDPACK(ByVal OTP_BRANDPACK As String) As Boolean
            Try
                Me.CreateCommandSql("Usp_Check_Existing_OTP_BRANDPACK", "")
                Me.AddParameter("@OTP_BRANDPACK", SqlDbType.VarChar, OTP_BRANDPACK, 90)
                If CInt(Me.ExecuteScalar()) > 0 Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function EXISTSOTP_NO(ByVal OTP_NO As String) As Boolean
            Try
                Me.CreateCommandSql("Usp_Check_OTP_NO", "")
                Me.AddParameter("@OTP_NO", SqlDbType.VarChar, OTP_NO, 15)
                If CInt(Me.ExecuteScalar()) > 0 Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function EXIXTS_DO_TP_NO(ByVal DO_TP_NO As String) As Boolean
            Try
                Me.CreateCommandSql("Usp_Check_DO_TP_NO", "")
                Me.AddParameter("@DO_TP_NO", SqlDbType.VarChar, DO_TP_NO, 15)
                If CInt(Me.ExecuteScalar()) > 0 Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function EXISTS_DO_TP_BRANDPACK(ByVal DO_TP_BRANDPACK As String) As Boolean
            Try
                Me.CreateCommandSql("Usp_Check_Existing_DO_TP_BRANDPACK", "")
                Me.AddParameter("@DO_TP_BRANDPACK", SqlDbType.VarChar, DO_TP_BRANDPACK, 85)
                If CInt(Me.ExecuteScalar()) > 0 Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function CreateViewDistributorDOTP() As DataView
            Try
                Me.CreateCommandSql("Usp_Create_View_Distributor_PO_DO_ThirdParty", "")
                Dim dtTable As New DataTable("DISTRIBUTOR")
                dtTable.Clear()
                Me.FillDataTable(dtTable)
                Me.ViewDistributor() = dtTable.DefaultView()
                Return Me.ViewDistributor()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function getSPPB_QTY(ByVal SPPB_BRANDPACK_ID As String) As Decimal
            Try
                Me.CreateCommandSql("Usp_Get_SPPB_Qty", "")
                Me.AddParameter("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, SPPB_BRANDPACK_ID, 90)
                Return CDec(Me.ExecuteScalar())
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function getOTPQTY(ByVal OTP_BRANDPACK As String) As Decimal
            Try
                Me.CreateCommandSql("Usp_Get_OTP_Qty", "")
                Me.AddParameter("@OTP_BRANDPACK", SqlDbType.VarChar, OTP_BRANDPACK, 90)
                Return CDec(Me.ExecuteScalar())
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function SearchSPPB_NO(ByVal DISTRIBUTOR_ID As String, ByVal SearchSPPB As String) As DataView
            Try
                Me.CreateCommandSql("Usp_Search_ThirdParty_SPBB_NO", "")
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SearchSPPB, 30) ' VARCHAR(15),
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10)
                Dim tbl As New DataTable("SPPB")
                tbl.Clear()
                Me.FillDataTable(tbl)
                Me.m_ViewSPPB_NO = tbl.DefaultView()
                Me.m_ViewSPPB_NO.Sort = "SPPB_NO"
                Return Me.m_ViewSPPB_NO
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

#End Region

    End Class
End Namespace

