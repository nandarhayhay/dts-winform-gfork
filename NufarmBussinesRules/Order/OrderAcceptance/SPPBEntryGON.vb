Imports NufarmBussinesRules.common.Helper
Imports System.Data
Imports System.Data.SqlClient
Imports Nufarm.Domain
Imports System.Globalization
Namespace OrderAcceptance
    Public Class SPPBEntryGON
        Inherits SPPB
        Public Function HasExistsGONHeaderID(ByVal GONHeaderID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT GON_HEADER_ID FROM GON_HEADER WHERE GON_HEADER_ID = @GON_HEADER_ID);"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@GON_HEADER_ID", SqlDbType.VarChar, GONHeaderID, 50)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    If CInt(retval) > 0 Then
                        Return True
                    End If
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getGONDescriptionBySPPB(ByVal SPPB_NO As String, ByRef status As String, ByVal mustCloseConnection As Boolean) As GONHeader
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT TOP 1 * FROM GON_HEADER WHERE SPPB_NO = @SPPB_NO ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 25)
                Me.ExecuteReader()
                Dim ObjGONHeader As New GONHeader()
                While Me.SqlRe.Read()
                    With ObjGONHeader
                        .GON_DATE = Me.SqlRe.Item("GON_DATE")
                        .GON_ID_AREA = IIf((IsDBNull(Me.SqlRe.Item("GON_ID_AREA")) Or IsNothing(Me.SqlRe.Item("GON_ID_AREA"))), "", Me.SqlRe.Item("GON_ID_AREA").ToString())
                        .GON_NO = Me.SqlRe("GON_NO").ToString()

                        .GT_ID = IIf((IsDBNull(Me.SqlRe.Item("GT_ID")) Or IsNothing(Me.SqlRe.Item("GT_ID"))), "", Me.SqlRe.Item("GT_ID").ToString())
                        .ModifiedBy = IIf((IsDBNull(Me.SqlRe.Item("ModifiedBy")) Or IsNothing(Me.SqlRe.Item("ModifiedBy"))), "", Me.SqlRe.Item("ModifiedBy").ToString())
                        .ModifiedDate = Me.SqlRe.Item("ModifiedDate")
                        .SPPBNO = Me.SqlRe.Item("SPPB_NO").ToString()
                        Dim oDriverTrans As Object = SqlRe("DRIVER_TRANS"), oPoliceNoTrans As Object = SqlRe("POLICE_NO_TRANS")
                        Dim oCreatedBy As Object = SqlRe("CreatedBy"), oCreatedDate As Object = SqlRe("CreatedDate")
                        Dim oDescpriptionApp As Object = SqlRe("REMARK")
                        Dim oGTID As Object = SqlRe("GT_ID"), oModifiedBy As Object = SqlRe("ModifiedBy")
                        Dim oModifiedDate As Object = SqlRe("ModifiedDate")
                        Dim warhouseCode As Object = SqlRe("WARHOUSE")
                        If Not IsNothing(oGTID) And Not IsDBNull(oGTID) Then
                            .GT_ID = oGTID.ToString
                        End If
                        If Not IsNothing(oModifiedDate) And Not IsDBNull(oModifiedDate) Then
                            .ModifiedDate = oModifiedDate
                        End If
                        If Not IsNothing(oModifiedBy) And Not IsDBNull(oModifiedBy) Then
                            .ModifiedBy = oModifiedBy.ToString()
                        End If
                        If Not IsNothing(oDriverTrans) And Not IsDBNull(oDriverTrans) Then
                            .DriverTrans = oDriverTrans.ToString()
                        End If
                        If Not IsNothing(oPoliceNoTrans) And Not IsDBNull(oPoliceNoTrans) Then
                            .PoliceNoTrans = oPoliceNoTrans.ToString()
                        End If
                        '.DriverTrans = IIf(IsDBNull(SqlRe("DRIVER_TRANS") Or IsNothing(SqlRe("DRIVER_TRANS"))), "", SqlRe("DRIVER_TRANS").ToString())
                        '.PoliceNoTrans = IIf(IsDBNull(SqlRe("POLICE_NO_TRANS")) Or IsNothing(SqlRe("POLICE_NO_TRANS")), "", SqlRe("POLICE_NO_TRANS").ToString())
                        If Not IsNothing(oCreatedBy) And Not IsDBNull(oCreatedBy) Then
                            .CreatedBy = oCreatedBy.ToString()
                        End If
                        If Not IsNothing(oCreatedDate) And Not IsDBNull(oCreatedDate) Then
                            .CreatedDate = oCreatedDate
                        End If
                        If Not IsNothing(oDescpriptionApp) And Not IsDBNull(oDescpriptionApp) Then
                            .DescriptionApp = oDescpriptionApp.ToString()
                        End If
                        If Not IsNothing(warhouseCode) And Not IsDBNull(warhouseCode) Then
                            .WarhouseCode = warhouseCode.ToString()
                        End If
                        '.DescriptionApp = IIf((IsDBNull(Me.SqlRe.Item("REMARK")) Or IsNothing(Me.SqlRe.Item("REMARK"))), "", Me.SqlRe.Item("REMARK").ToString())
                    End With
                End While : Me.SqlRe.Close()
                ''get Status
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT STATUS FROM SPPB_BRANDPACK WHERE SPPB_NO = @SPPB_NO "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.ExecuteReader()
                Dim listStatus As New List(Of String)
                While Me.SqlRe.Read()
                    If Not Me.SqlRe.IsDBNull(0) And Not IsNothing(Me.SqlRe.Item(0)) Then
                        If Not listStatus.Contains(Me.SqlRe.GetString(0)) Then
                            listStatus.Add(SqlRe.GetString(0))
                        End If
                    End If
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                If listStatus.Count = 1 Then
                    status = listStatus(0)
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return ObjGONHeader
            Catch ex As Exception
                Me.ClearCommandParameters()
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed() Then
                        Me.SqlRe.Close() : Me.SqlRe = Nothing
                    End If
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Throw ex
            End Try
        End Function
        Public Function getSalesPerson(ByVal PONumber As String, ByVal MustCloseConnection As Boolean) As String
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT TOP 1 TM.MANAGER FROM TERRITORY_MANAGER TM INNER JOIN SHIP_TO ST ON TM.TM_ID = ST.TM_ID INNER JOIN OA_SHIP_TO OST ON OST.SHIP_TO_ID = ST.SHIP_TO_ID " & vbCrLf & _
                " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OOA.OA_ID = OST.OA_ID INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OOA.PO_REF_NO WHERE PO.PO_REF_NO = @PO_REF_NO ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PONumber, 30)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return retval.ToString()
                End If
                Return ""
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getMinDate(ByVal PORefNo As DateTime, ByVal SPPB_NO As String, ByVal MustCloseConnection As Boolean) As Object
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                " DECLAR @V_Result = SMALLDATETIME ;" & vbCrLf & _
                " SELECT @V_Result = (SELECT TOP 1 SPPB_DATE FROM SPPB_HEADER WHERE SPPB_NO = @SPP) ;" & vbCrLf & _
                " IF @V_Result IS NOT NULL " & vbCrLf & _
                " BEGIN SELECT Result = @V_Result ; RETURN ; END " & vbCrLf & _
                " SELECT @V_Result = (SELECT TOP 1 PO_REF_DATE FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = ANY(SELECT TOP 1 PO_REF_NO FROM SPPB_HEADER WHERE PO_REF_NO = @PO_REF_NO) ; " & vbCrLf & _
                " SELECT Result = @V_Result;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PORefNo, 30)
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 30)
                Me.OpenConnection()
                Dim Retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If MustCloseConnection Then : Me.CloseConnection() : End If
                Return Retval
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetlastGONDate(ByVal SPPBNO As String, ByVal MustCloseConnection As Boolean) As Object
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT MAX(GON_DATE) FROM GON_HEADER WHERE SPPB_NO = @SPPB_NO ; "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 15)
                Me.OpenConnection() : Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                If MustCloseConnection Then : Me.CloseConnection() : End If
                Return retval
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Sub SaveOrUpdateGON(ByVal domGON As GONHeader)
            Query = "SET NOCOUNT ON; " & vbCrLf & _
        "IF NOT EXISTS(SELECT GON_HEADER_ID FROM GON_HEADER WHERE GON_HEADER_ID = @GON_HEADER_ID) " & vbCrLf & _
        " BEGIN " & vbCrLf & _
        " INSERT INTO GON_HEADER(GON_HEADER_ID,GON_DATE,GT_ID,GON_ID_AREA,SPPB_NO,GON_NO,POLICE_NO_TRANS,DRIVER_TRANS,WARHOUSE,REMARK,CreatedBy,CreatedDate) " & vbCrLf & _
        " VALUES(@GON_HEADER_ID,@GON_DATE,@GT_ID,@GON_ID_AREA,@SPPB_NO,@GON_NO,@POLICE_NO_TRANS,@DRIVER_TRANS,@WARHOUSE,@REMARK,@CreatedBy,CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100), GETDATE(),101))) ; " & vbCrLf & _
        " END " & vbCrLf & _
        " ELSE  " & vbCrLf & _
        " BEGIN " & vbCrLf & _
        " UPDATE GON_HEADER SET GON_ID_AREA = @GON_ID_AREA,GT_ID = @GT_ID,GON_DATE = @GON_DATE,POLICE_NO_TRANS=@POLICE_NO_TRANS," & vbCrLf & _
        " DRIVER_TRANS=@DRIVER_TRANS,WARHOUSE=@WARHOUSE,REMARK = @REMARK,ModifiedBy = @ModifiedBy, " & vbCrLf & _
        " ModifiedDate = CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100), GETDATE(),101)) WHERE GON_HEADER_ID = @GON_HEADER_ID ; " & vbCrLf & _
        " END "
            If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
            Else : Me.ResetCommandText(CommandType.Text, Query)
            End If
            Me.AddParameter("@GON_HEADER_ID", SqlDbType.VarChar, domGON.CodeApp, 40)
            Me.AddParameter("@GON_NO", SqlDbType.VarChar, domGON.GON_NO, 25)
            Me.AddParameter("@GON_DATE", SqlDbType.SmallDateTime, domGON.GON_DATE)
            Me.AddParameter("@GT_ID", SqlDbType.VarChar, domGON.GT_ID, 10)
            Me.AddParameter("@GON_ID_AREA", SqlDbType.VarChar, domGON.GON_ID_AREA, 10)
            Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, domGON.SPPBNO, 15)
            Me.AddParameter("@REMARK", SqlDbType.VarChar, domGON.DescriptionApp, 200)
            Me.AddParameter("@POLICE_NO_TRANS", SqlDbType.VarChar, domGON.PoliceNoTrans, 50)
            Me.AddParameter("@DRIVER_TRANS", SqlDbType.VarChar, domGON.DriverTrans, 50)
            Me.AddParameter("@WARHOUSE", SqlDbType.VarChar, domGON.WarhouseCode, 20)
            Me.AddParameter("@CreatedBy", SqlDbType.VarChar, domGON.CreatedBy, 100)
            Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, IIf(String.IsNullOrEmpty(domGON.ModifiedBy), NufarmBussinesRules.User.UserLogin.UserName, domGON.ModifiedBy))
            If IsNothing(Me.SqlCom.Transaction) Then : Me.SqlCom.Transaction = Me.SqlTrans : End If
            Me.OpenConnection()
            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

        End Sub
        Public Function SaveDataSPPBGON(ByVal DS As DataSet, ByRef NewDS As DataSet, ByVal domSPPB As SPPBHeader, ByVal domGON As GONHeader, ByVal IsOnlyEditGON As Boolean, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                ''GON
                Dim InsertedGONRows() As DataRow = DS.Tables("GON_DETAIL_INFO").Select("", "", DataViewRowState.Added)
                Dim UpdatedGONRows() As DataRow = DS.Tables("GON_DETAIL_INFO").Select("", "", DataViewRowState.ModifiedOriginal)
                Dim DeletedGONRows() As DataRow = DS.Tables("GON_DETAIL_INFO").Select("", "", DataViewRowState.Deleted)
                ''SPPB
                Dim InsertedSPPBRows() As DataRow = DS.Tables("SPPB_BRANDPACK_INFO").Select("", "", DataViewRowState.Added)
                Dim UpdatedSPPBRows() As DataRow = DS.Tables("SPPB_BRANDPACK_INFO").Select("", "", DataViewRowState.ModifiedOriginal)
                Dim DeletedSPPBRows() As DataRow = DS.Tables("SPPB_BRANDPACK_INFO").Select("", "", DataViewRowState.Deleted)
                If (InsertedSPPBRows.Length <= 0 And UpdatedSPPBRows.Length <= 0 And DeletedSPPBRows.Length <= 0 And InsertedGONRows.Length <= 0 And UpdatedGONRows.Length <= 0 And DeletedGONRows.Length <= 0 And (IsOnlyEditGON = False)) Then
                    Throw New Exception("NO Changes should be saved")
                End If
                Me.OpenConnection() : Me.BeginTransaction()
                If Not IsOnlyEditGON Then
                    ''===================INSERT /UPDATE SPPB HEADER =================================================
                    If Not IsNothing(domSPPB) Then
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                  " IF NOT EXISTS(SELECT SPPB_NO FROM SPPB_HEADER WHERE SPPB_NO = @SPPB_NO) " & vbCrLf & _
                                  " BEGIN " & vbCrLf & _
                                  " INSERT INTO SPPB_HEADER(SPPB_NO,SPPB_DATE,PO_REF_NO,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                                  " VALUES(@SPPB_NO,@SPPB_DATE,@PO_REF_NO,@CREATE_BY,CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100), GETDATE(),101))) " & vbCrLf & _
                                  " END " & vbCrLf & _
                                  " BEGIN " & vbCrLf & _
                                  " UPDATE SPPB_Header SET SPPB_DATE = @SPPB_DATE,MODIFY_BY = @MODIFY_BY,MODIFY_DATE =  CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100), GETDATE(),101)) WHERE SPPB_NO = @SPPB_NO ;" & vbCrLf & _
                                  " END "
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                        Else : Me.ResetCommandText(CommandType.Text, Query)
                        End If
                        Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, domSPPB.SPPBNO, 20) ' VARCHAR(15),
                        Me.AddParameter("@SPPB_DATE", SqlDbType.DateTime, domSPPB.SPPBDate) ' DATETIME,
                        'Me.AddParameter("@DATE_RECEIVED", SqlDbType.DateTime, domSPPB.SppBReceived) ' DATETIME,
                        Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, domSPPB.PONumber, 25) 'VARCHAR(25),
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)

                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)

                        Me.SqlCom.Transaction = Me.SqlTrans : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    End If
                    '===========================================================================================
                End If

                '====================INSERT / UPDATE GON HEADER==============================================
                If Not IsNothing(domGON) Then
                    SaveOrUpdateGON(domGON)
                End If
                '================================================================================================================================

                '=============================INSERT DETAIL GON & SPPB===========================================

                '-----------------------------INSERT GON-------------------------------------------------------------
                Dim CommandInsert As SqlCommand = Nothing, CommandUpdate As SqlCommand = Nothing, CommandDelete As SqlCommand = Nothing
                Me.SqlDat = New SqlDataAdapter()
                If InsertedGONRows.Length > 0 Then
                    CommandInsert = Me.SqlConn.CreateCommand()
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " IF NOT EXISTS(SELECT GON_DETAIL_ID FROM GON_DETAIL WHERE GON_DETAIL_ID = @GON_DETAIL_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " INSERT INTO GON_DETAIL(GON_DETAIL_ID,GON_HEADER_ID,SPPB_BRANDPACK_ID,BRANDPACK_ID,GON_QTY,IsCompleted,IsOpen,BatchNo,UNIT1,VOL1,UNIT2,VOL2,IsUpdatedBySystem,CreatedBy,CreatedDate) " & vbCrLf & _
                            " VALUES(@GON_DETAIL_ID,@GON_HEADER_ID,@SPPB_BRANDPACK_ID,@BRANDPACK_ID,@GON_QTY,@IsCompleted,@IsOpen,@BatchNo,@UNIT1,@VOL1,@UNIT2,@VOL2,@UBS,@CreatedBy,CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100), GETDATE(),101))) " & vbCrLf & _
                            " END "
                    CommandInsert.Transaction = Me.SqlTrans : CommandInsert.CommandType = CommandType.Text : CommandInsert.CommandText = Query
                    With CommandInsert
                        .Parameters.Add("@GON_DETAIL_ID", SqlDbType.VarChar, 140, "GON_DETAIL_ID")
                        .Parameters.Add("@GON_HEADER_ID", SqlDbType.VarChar, 40, "GON_HEADER_ID")
                        .Parameters.Add("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, 90, "SPPB_BRANDPACK_ID")
                        .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                        .Parameters.Add("@GON_QTY", SqlDbType.Decimal, 0, "GON_QTY")
                        .Parameters.Add("@IsCompleted", SqlDbType.Bit, 0, "IsCompleted")
                        .Parameters.Add("@IsOpen", SqlDbType.Bit, 0, "IsOpen")
                        .Parameters.Add("@BatchNo", SqlDbType.NVarChar, 50, "BatchNo")
                        .Parameters.Add("@UNIT1", SqlDbType.VarChar, 30, "UNIT1")
                        .Parameters.Add("@VOL1", SqlDbType.Decimal, 0, "VOL1")
                        .Parameters.Add("@UNIT2", SqlDbType.VarChar, 30, "UNIT2")
                        .Parameters.Add("@VOL2", SqlDbType.Decimal, 0, "VOL2")

                        .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                        .Parameters.Add("@UBS", SqlDbType.Bit, 0, "IsUpdatedBySystem")
                        Me.SqlDat.InsertCommand = CommandInsert
                        Me.SqlDat.Update(InsertedGONRows) : CommandInsert.Parameters.Clear()
                    End With
                End If
                '----------------------------------------------------------------------------------------------------------------------


                '-------------------------UPDATE GON DETAIL--------------------------------------------------------------------------
                If UpdatedGONRows.Length > 0 Then
                    CommandUpdate = Me.SqlConn.CreateCommand()
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " UPDATE GON_DETAIL SET GON_QTY = @GON_QTY,IsCompleted = @IsCompleted,IsOpen = @IsOpen,BatchNo = @BatchNo," & vbCrLf & _
                    " UNIT1 = @UNIT1,VOL1=@VOL1,UNIT2=@UNIT2,VOL2=@VOL2,IsUpdatedBySystem = @UBS,ModifiedBy = @ModifiedBy,ModifiedDate = CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100), GETDATE(),101)) " & vbCrLf & _
                    " WHERE GON_DETAIL_ID = @GON_DETAIL_ID; "
                    With CommandUpdate
                        .CommandText = Query
                        .CommandType = CommandType.Text
                        .Transaction = Me.SqlTrans
                        .Parameters.Add("@GON_QTY", SqlDbType.Decimal, 0, "GON_QTY")
                        .Parameters.Add("@IsCompleted", SqlDbType.Bit, 0, "IsCompleted")
                        .Parameters.Add("@IsOpen", SqlDbType.Bit, 0, "IsOpen")
                        .Parameters.Add("@BatchNo", SqlDbType.NVarChar, 50, "BatchNo")
                        .Parameters.Add("@UNIT1", SqlDbType.VarChar, 30, "UNIT1")
                        .Parameters.Add("@VOL1", SqlDbType.Decimal, 0, "VOL1")
                        .Parameters.Add("@UNIT2", SqlDbType.VarChar, 30, "UNIT2")
                        .Parameters.Add("@VOL2", SqlDbType.Decimal, 0, "VOL2")
                        .Parameters.Add("@UBS", SqlDbType.Bit, 0, "IsUpdatedBySystem")
                        .Parameters.Add("@GON_DETAIL_ID", SqlDbType.VarChar, 140, "GON_DETAIL_ID")
                        .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100).Value = NufarmBussinesRules.User.UserLogin.UserName
                        .Parameters("@GON_DETAIL_ID").SourceVersion = DataRowVersion.Original
                    End With
                    Me.SqlDat.UpdateCommand = CommandUpdate
                    Me.SqlDat.Update(UpdatedGONRows)
                    CommandUpdate.Parameters.Clear()
                End If
                '---------------------------------------------------------------------------------------------------------------------------------

                '-----------------------------------DELETE GON_DETAIL-------------------------------------------------------------------------------
                If DeletedGONRows.Length > 0 Then
                    CommandDelete = Me.SqlConn.CreateCommand()
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                         " DECLARE @V_GON_HEADER_ID VARCHAR(40) ; " & vbCrLf & _
                         " SET @V_GON_HEADER_ID = (SELECT TOP 1 GON_HEADER_ID FROM GON_DETAIL WHERE GON_DETAIL_ID = @GON_DETAIL_ID );" & vbCrLf & _
                         "   DELETE FROM GON_DETAIL WHERE GON_DETAIL_ID = @GON_DETAIL_ID ;" & vbCrLf & _
                         " IF NOT EXISTS(SELECT GON_HEADER_ID FROM GON_DETAIL WHERE GON_HEADER_ID = @V_GON_HEADER_ID) " & vbCrLf & _
                         " BEGIN DELETE FROM GON_HEADER WHERE GON_HEADER_ID = @V_GON_HEADER_ID ; END "
                    With CommandDelete
                        .CommandType = CommandType.Text : .CommandText = Query : .Transaction = Me.SqlTrans
                        .Parameters.Add("@GON_DETAIL_ID", SqlDbType.VarChar, 140, "GON_DETAIL_ID")
                        .Parameters("@GON_DETAIL_ID").SourceVersion = DataRowVersion.Original
                    End With
                    Me.SqlDat.DeleteCommand = CommandDelete
                    Me.SqlDat.Update(DeletedGONRows)
                    CommandDelete.Parameters.Clear()
                End If
                '--------------------------------------------------------------------------------------------------------------------------------
                '-------------------------------------INSERT SPPB DETAIL-----------------------------------------------------------------------
                If InsertedSPPBRows.Length > 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " IF NOT EXISTS(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE SPPB_BRANDPACK_ID = @SPPB_BRANDPACK_ID) " & vbCrLf & _
                    " BEGIN " & vbCrLf & _
                    " INSERT INTO SPPB_BRANDPACK(SPPB_BRANDPACK_ID,SPPB_NO,OA_BRANDPACK_ID,BRANDPACK_ID,SPPB_QTY,STATUS,REMARK,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                    " VALUES(@SPPB_BRANDPACK_ID,@SPPB_NO,@OA_BRANDPACK_ID,@BRANDPACK_ID,@SPPB_QTY,@STATUS,@REMARK,@CREATE_BY,CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100), GETDATE(),101))) ;" & vbCrLf & _
                    " END " & vbCrLf & _
                    " ELSE " & vbCrLf & _
                    " BEGIN " & vbCrLf & _
                    " UPDATE SPPB_BRANDPACK SET SPPB_QTY = @SPPB_QTY,STATUS = @STATUS,REMARK = @REMARK,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100), GETDATE(),101)) " & vbCrLf & _
                    " WHERE SPPB_BRANDPACK_ID = @SPPB_BRANDPACK_ID ;" & vbCrLf & _
                    " END "
                    If IsNothing(CommandInsert) Then : CommandInsert = Me.SqlConn.CreateCommand() : End If
                    If IsNothing(CommandInsert.Transaction) Then : CommandInsert.Transaction = Me.SqlTrans : End If
                    CommandInsert.CommandType = CommandType.Text : CommandInsert.CommandText = Query
                    CommandInsert.Parameters.Add("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, 90, "SPPB_BRANDPACK_ID")
                    CommandInsert.Parameters.Add("@SPPB_NO", SqlDbType.VarChar, 30, "SPPB_NO")
                    CommandInsert.Parameters.Add("@OA_BRANDPACK_ID", SqlDbType.VarChar, 75, "OA_BRANDPACK_ID")
                    CommandInsert.Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                    CommandInsert.Parameters.Add("@SPPB_QTY", SqlDbType.Decimal, 0, "SPPB_QTY")
                    CommandInsert.Parameters.Add("@STATUS", SqlDbType.VarChar, 50, "STATUS")
                    CommandInsert.Parameters.Add("@REMARK", SqlDbType.VarChar, 250, "REMARK")
                    CommandInsert.Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 100, "CREATE_BY")
                    CommandInsert.Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100, "MODIFY_BY")

                    Me.SqlDat.InsertCommand = CommandInsert : Me.SqlDat.Update(InsertedSPPBRows) : CommandInsert.Parameters.Clear()
                End If
                '------------------------------------------------------------------------------------------------------------------------------------

                '-------------------------------UPDATE SPPB DETAIL---------------------------------------------------------------------------------------------------
                If UpdatedSPPBRows.Length > 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " UPDATE SPPB_BRANDPACK SET SPPB_QTY = @SPPB_QTY,STATUS = @STATUS,IsUpdatedBySystem = @UBS,REMARK = @REMARK," & vbCrLf & _
                    " MODIFY_BY = @MODIFY_BY,MODIFY_DATE = CONVERT(SMALLDATETIME,CONVERT(VARCHAR(100), GETDATE(),101)) WHERE SPPB_BRANDPACK_ID = @SPPB_BRANDPACK_ID ; "
                    If IsNothing(CommandUpdate) Then : CommandUpdate = Me.SqlConn.CreateCommand() : End If
                    If IsNothing(CommandUpdate.Transaction()) Then : CommandUpdate.Transaction = Me.SqlTrans : End If
                    CommandUpdate.CommandType = CommandType.Text : CommandUpdate.CommandText = Query
                    With CommandUpdate
                        .Parameters.Add("@SPPB_QTY", SqlDbType.Decimal, 0, "SPPB_QTY")
                        .Parameters.Add("@STATUS", SqlDbType.VarChar, 50, "STATUS")
                        .Parameters.Add("@UBS", SqlDbType.Bit, 0, "IsUpdatedBySystem")
                        .Parameters.Add("@REMARK", SqlDbType.VarChar, 200, "REMARK")
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 100, "MODIFY_BY")
                        .Parameters.Add("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, 90, "SPPB_BRANDPACK_ID")
                        .Parameters("@SPPB_BRANDPACK_ID").SourceVersion = DataRowVersion.Original
                    End With
                    Me.SqlDat.UpdateCommand = CommandUpdate : Me.SqlDat.Update(UpdatedSPPBRows) : CommandUpdate.Parameters.Clear()
                End If
                '-----------------------------------------------------------------------------------------------------------------------------------

                '-------------------------------------DELETE SPPB DETAIL-------------------------------------------------------------------------------------
                If DeletedSPPBRows.Length > 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                             " IF  EXISTS(SELECT GON_DETAIL_ID FROM GON_DETAIL WHERE SPPB_BRANDPACK_ID = ANY(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK " & vbCrLf & _
                            " WHERE OA_BRANDPACK_ID = @I_OA_BRANDPACK_ID)) " & vbCrLf & _
                            " BEGIN RAISERROR('Can not delete data,sppb has already had gon',16,1); RETURN ; END" & vbCrLf & _
                            " EXEC Usp_Delete_SPPB_BrandPack @OA_BRANDPACK_ID = @I_OA_BRANDPACK_ID ;"
                    If IsNothing(CommandDelete) Then : CommandDelete = Me.SqlConn.CreateCommand() : End If
                    If IsNothing(CommandDelete.Transaction) Then : CommandDelete.Transaction = Me.SqlTrans : End If
                    CommandDelete.CommandType = CommandType.Text : CommandDelete.CommandText = Query
                    With CommandDelete
                        .Parameters.Add("@I_OA_BRANDPACK_ID", SqlDbType.VarChar, 90, "OA_BRANDPACK_ID")
                        .Parameters("@I_OA_BRANDPACK_ID").SourceVersion = DataRowVersion.Original
                    End With
                    Me.SqlDat.DeleteCommand = CommandDelete : Me.SqlDat.Update(DeletedSPPBRows) : CommandDelete.Parameters.Clear()
                End If
                '----------------------------------------------------------------------------------------------------------------------------------------

                ''-==================MATIKAN SMS==========================
                'If IsNothing(domGON) Then
                '    'Me.CommiteTransaction() : Me.ClearCommandParameters()
                'ElseIf domGON.GON_NO = "" Or IsNothing(domGON.GON_DATE) Or IsNothing(domGON.SPPBNO) Then
                '    'Me.CommiteTransaction() : Me.ClearCommandParameters()
                'Else
                '    Query = "SET NOCOUNT ON ; " & vbCrLf & _
                '        "SELECT TransactionID FROM GON_SMS WHERE SPPB_NO = @SPPB_NO AND GON_NO = @GON_NO AND STATUS_SENT IS NULL ;"
                '    Me.ResetCommandText(CommandType.Text, Query)
                '    Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, domGON.SPPBNO, 15)
                '    Me.AddParameter("@GON_NO", SqlDbType.VarChar, domGON.GON_NO, 25)
                '    Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                '    If IsNothing(retval) Or IsDBNull(retval) Then
                '        ''insert GON
                '        Me.InsertSMSSPPB(domSPPB.PONumber, domGON.SPPBNO, domGON)
                '    End If
                'End If
                '-=================END MATIKAN SMS=========================
                Me.CommiteTransaction() : Me.ClearCommandParameters()
                Dim tblSPPBBrandPack As DataTable = Me.getSPPBBrandPack(domSPPB.SPPBNO, domSPPB.PONumber, False, False)
                Dim tblGON As DataTable = Me.getGOnData(domSPPB.SPPBNO, True)

                'UPDATE Balance

                'DS.AcceptChanges()
                NewDS = New DataSet("DSSPPB_GON") : NewDS.Tables.Add(tblSPPBBrandPack)
                NewDS.Tables.Add(tblGON) : NewDS.AcceptChanges()
                Return True
            Catch ex As Exception

                Me.RollbackTransaction() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Private Function InsertSMSSPPB(ByVal PORefNO As String, ByVal SPPPBNo As String, ByVal DomGON As GONHeader) As Boolean

            Dim StatusPending As Boolean = False, StatusPartial As Boolean = False, StatusCompleted As Boolean = False
            Dim StatusGon As String = "", ItemDescription As String = "Items{(", Message As String = "", strGonDate = "", _
            DistributorID As String = "", DistributorName As String = "", Contact_Person As String = "", Mobile As String = ""
            'strGonDate = Me.ChangedGonDate.Day.ToString() + "/" & Me.ChangedGonDate.Month.ToString() + "/" & Me.ChangedGonDate.Year.ToString()

            Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    "SELECT BB.BRANDPACK_NAME,SB.SPPB_BRANDPACK_ID,ISNULL(GON.TOTAL_GON_QTY,0) AS Qty,SB.STATUS FROM BRND_BRANDPACK BB INNER JOIN ORDR_PO_BRANDPACK OPB " & vbCrLf & _
                    " ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                    " INNER JOIN SPPB_BRANDPACK SB ON OOAB.OA_BRANDPACK_ID = SB.OA_BRANDPACK_ID " & vbCrLf & _
                    " LEFT OUTER JOIN(" & vbCrLf & _
                    "                 SELECT SPPB_BRANDPACK_ID,ISNULL(SUM(GON_QTY),0)AS TOTAL_GON_QTY FROM GON_DETAIL WHERE SPPB_BRANDPACK_ID = ANY(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK " & vbCrLf & _
                    "                 WHERE SPPB_NO = @SPPB_NO) GROUP BY SPPB_BRANDPACK_ID " & vbCrLf & _
                    "                 )GON ON GON.SPPB_BRANDPACK_ID = SB.SPPB_BRANDPACK_ID WHERE SB.SPPB_NO = @SPPB_NO ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, DomGON.SPPBNO, 15)
            Dim dtTable As New DataTable("T_SPPB") : dtTable.Clear()
            Me.SqlDat.SelectCommand = Me.SqlCom
            Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
            For i As Integer = 0 To dtTable.Rows.Count - 1
                If dtTable.Rows(i)("STATUS").ToString() = "--NEW SPPB--" Then
                    StatusPending = True
                ElseIf dtTable.Rows(i)("STATUS").ToString() = "SHIPPED" Then
                    StatusCompleted = True
                ElseIf dtTable.Rows(i)("STATUS").ToString() <> "OTHER" And dtTable.Rows(i)("STATUS").ToString() <> "" Then
                    StatusPartial = True
                End If
                ItemDescription &= dtTable.Rows(i)("BRANDPACK_NAME").ToString() & ", Qty = " & String.Format(New CultureInfo("id-ID"), "{0:#,##0.000}", Convert.ToDecimal(dtTable.Rows(i)("Qty")))
                If i < dtTable.Rows.Count - 1 Then
                    ItemDescription &= "),("
                Else
                    ItemDescription &= ")}"
                End If
            Next
            If StatusPending = True And StatusPartial = True And StatusCompleted = True Then
                StatusGon = "PARTIAL"
            ElseIf StatusPending = True And StatusPartial = True And StatusCompleted = False Then
                StatusGon = "PARTIAL"
            ElseIf StatusPending = True And StatusPartial = False And StatusCompleted = True Then
                StatusGon = "PARTIAL"
            ElseIf StatusPending = False And StatusPartial = True And StatusCompleted = True Then
                StatusGon = "PARTIAL"
            ElseIf StatusPending = True And StatusPartial = False And StatusCompleted = False Then
                StatusGon = ""
            ElseIf StatusPending = False And StatusPartial = True And StatusCompleted = False Then
                StatusGon = "PARTIAL"
            ElseIf StatusPending = False And StatusPartial = False And StatusCompleted = True Then
                StatusGon = "COMPLETED"
            ElseIf StatusPending = False And StatusPartial = False And StatusCompleted = False Then
                StatusGon = ""
            End If
            Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    "DECLARE @V_DISTRIBUTOR_ID VARCHAR(10); " & vbCrLf & _
                    " SET @V_DISTRIBUTOR_ID = (SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = @PO_REF_NO) ;" & vbCrLf & _
                    "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME,CONTACT AS CONTACT_PERSON,HP AS MOBILE FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = @V_DISTRIBUTOR_ID ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PORefNO, 30)
            Me.SqlRe = Me.SqlCom.ExecuteReader() : Me.ClearCommandParameters()
            While Me.SqlRe.Read()
                DistributorID = Me.SqlRe.GetString(0)
                DistributorName = Me.SqlRe.GetString(1)
                If Not Me.SqlRe.IsDBNull(2) Then
                    Contact_Person = Me.SqlRe.GetString(2)
                End If
                'Contact_Person = Me.SqlRe.GetString(2)
                If Not Me.SqlRe.IsDBNull(3) Then
                    Mobile = Me.SqlRe.GetString(3)
                End If
            End While : Me.SqlRe.Close()

            If Mobile = "" Then
                Throw New Exception("Contact_Mobile of Distributor is null")
            ElseIf Contact_Person = "" Then
                Throw New Exception("Contact_Person of Distributor is null")
            End If

            If StatusGon = "" Then : Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Return True
            End If
            Dim GonHeaderID As String = SPPB_NO & "|" & DomGON.GON_NO
            Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " SELECT GD.GON_QTY,BR.BRANDPACK_NAME FROM GON_DETAIL GD INNER JOIN BRND_BRANDPACK BR ON GD.BRANDPACK_ID = BR.BRANDPACK_ID " & vbCrLf & _
                    " WHERE GON_HEADER_ID = @GON_HEADER_ID ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@GON_HEADER_ID", SqlDbType.VarChar, GonHeaderID, 50)
            Dim tblDesc As New DataTable("t_gon")
            tblDesc.Clear() : Me.SqlDat.Fill(tblDesc) : Me.ClearCommandParameters()
            'Me.FillDataTable(tblDesc)
            ItemDescription = ""

            If tblDesc.Rows.Count > 0 Then
                ItemDescription = "Item GON{("
                For I_1 As Integer = 0 To tblDesc.Rows.Count - 1
                    ItemDescription &= tblDesc.Rows(I_1)("BRANDPACK_NAME").ToString() & ",Qty = " & String.Format(New CultureInfo("id-ID"), "{0:#,##0.000}", Convert.ToDecimal(tblDesc.Rows(I_1)("GON_QTY")))
                    If I_1 < tblDesc.Rows.Count - 1 Then
                        ItemDescription &= "),("
                    Else
                        ItemDescription &= ")}"
                    End If
                    'PO_DESCRIPTION &= String.Format("{0:#,##0.000}", tblDesc.Rows(I_1)("PO_ORIGINAL_QTY")) & "  " & tblDesc.Rows(I_1)("BRANDPACK_NAME").ToString()
                    'If I_1 < tblDesc.Rows.Count - 1 Then
                    '    PO_DESCRIPTION &= ","
                    'End If
                Next
            End If
            'String.Format("{0:#,##0.00}", CDec(Me.clsOADiscount.TOTAL_PRICE_DISTRIBUTOR)) 

            Message = "Bpk/Ibu Yth, PO " & DistributorName & ", NO PO:" & PORefNO & " SUDAH TERKIRIM(" & StatusGon & ")NO GON:" & DomGON.GON_NO & ", TGL:" & String.Format(New CultureInfo("id-ID"), "{0:dd-MM-yyyy}", Convert.ToDateTime(DomGON.GON_DATE)) & vbCrLf & _
             "Terima kasih-Nufarm."
            If (ItemDescription & "" & Message).Length <= 159 Then
                Message = "Bpk/Ibu Yth, PO " & DistributorName & ", NO PO:" & PORefNO & " SUDAH TERKIRIM(" & StatusGon & ")NO GON:" & DomGON.GON_NO & ", TGL:" & String.Format(New CultureInfo("id-ID"), "{0:dd-MM-yyyy}", Convert.ToDateTime(DomGON.GON_DATE)) & vbCrLf & _
                           ItemDescription & ", Terima kasih-Nufarm."
            End If

            Query = "SET NOCOUNT ON ; " & vbCrLf & _
                    "IF EXISTS(SELECT TransactionID FROM GON_SMS WHERE SPPB_NO = @SPPB_NO AND GON_NO = @GON_NO AND STATUS_SENT IS NULL) " & vbCrLf & _
                    " BEGIN " & vbCrLf & _
                    " UPDATE GON_SMS SET STATUS_GON = @STATUS_GON,GON_DATE = @GON_DATE WHERE (SPPB_NO = @SPPB_NO AND GON_NO = @GON_NO AND STATUS_SENT IS NULL ) ;" & vbCrLf & _
                    " END " & vbCrLf & _
                    "ELSE " & vbCrLf & _
                    " BEGIN " & vbCrLf & _
                    " INSERT INTO GON_SMS(DISTRIBUTOR_ID,SPPB_NO,PO_REF_NO,GON_NO,STATUS_GON,GON_DATE,SENT_TO,MOBILE,ORIGIN_COMPANY,ITEM_DESCRIPTION,MESSAGE) " & vbCrLf & _
                    " VALUES(@DISTRIBUTOR_ID,@SPPB_NO,@PO_REF_NO,@GON_NO,@STATUS_GON,@GON_DATE,@SENT_TO,@MOBILE,@ORIGIN_COMPANY,@ITEM_DESCRIPTION,@MESSAGE) ;" & vbCrLf & _
                    " END "
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
            Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, DomGON.SPPBNO, 15)
            Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PORefNO, 30)
            Me.AddParameter("GON_NO", SqlDbType.VarChar, DomGON.GON_NO, 15)
            'Me.AddParameter("@GS_NO", SqlDbType.VarChar, Me.ChangedGSNumber, 2)
            Me.AddParameter("@STATUS_GON", SqlDbType.VarChar, StatusGon, 20)
            Me.AddParameter("@GON_DATE", SqlDbType.DateTime, DomGON.GON_DATE)
            Me.AddParameter("@SENT_TO", SqlDbType.VarChar, Contact_Person, 50)
            Me.AddParameter("@MOBILE", SqlDbType.VarChar, Mobile, 20)
            Me.AddParameter("@ORIGIN_COMPANY", SqlDbType.VarChar, DistributorName, 100)
            Me.AddParameter("@ITEM_DESCRIPTION", SqlDbType.VarChar, ItemDescription, 300)
            Me.AddParameter("@MESSAGE", SqlDbType.VarChar, Message, 160)
            Me.SqlCom.ExecuteScalar() : Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()

        End Function
        Public Function GetProduct(ByVal SPPB_NO As String, ByVal TblGonToIncludeInCalculation As DataTable, ByVal tblSPPB As DataTable, ByVal mustCloseConnection As Boolean) As DataView
            Try
                Dim tblProduct As New DataTable("T_Product") : tblProduct.Clear()
                Dim colProdSPPBBrandPack As New DataColumn("SPPB_BRANDPACK_ID", Type.GetType("System.String"))
                Dim colProdSPPBQTy As New DataColumn("LEFT_QTY", Type.GetType("System.Decimal"))
                Dim colProdBrandPackName As New DataColumn("BRANDPACK_NAME", Type.GetType("System.String"))
                Dim colBrandPackID As New DataColumn("BRANDPACK_ID", Type.GetType("System.String"))
                tblProduct.Columns.AddRange(New DataColumn() {colProdSPPBBrandPack, colProdBrandPackName, colProdSPPBQTy, colBrandPackID})
                tblProduct.AcceptChanges()

                If Not IsNothing(TblGonToIncludeInCalculation) Then
                    Dim InsertedRows() As DataRow = TblGonToIncludeInCalculation.Select("", "", DataViewRowState.CurrentRows)
                    If InsertedRows.Length > 0 Then
                        'For i As Integer = 0 To CurRows.Length - 1
                        '    Dim NewRow As DataRow = tblToMerge.NewRow()
                        '    NewRow.BeginEdit()
                        '    NewRow("SPPB_BRANDPACK_ID") = CurRows(i)("SPPB_BRANDPACK_ID")
                        '    NewRow("BRANDPACK_NAME") = CurRows(i)("BRANDPACK_NAME")
                        '    NewRow("GON_QTY") = CurRows(i)("GON_QTY")
                        '    NewRow.EndEdit()
                        'Next
                        Throw New Exception("You should save data before adding new gon." & vbCrLf & "Please klik the button save changes")
                    End If
                    Dim DeletedRows() As DataRow = TblGonToIncludeInCalculation.Select("", "", DataViewRowState.Deleted)
                    If (DeletedRows.Length > 0) Then
                        Throw New Exception("You should save data before adding new gon." & vbCrLf & "Please klik the button save changes")
                    End If
                    Dim UpdatedRows() As DataRow = TblGonToIncludeInCalculation.Select("", "", DataViewRowState.ModifiedOriginal)
                    If UpdatedRows.Length > 0 Then
                        Throw New Exception("You should save data before adding new gon." & vbCrLf & "Please klik the button save changes")
                    End If
                End If

                ''CHECK ke database apakah sudah dibuatkan SPPB_Sebelumnya
                Dim hasExistedGON As Boolean = False
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT 1 WHERE EXISTS(SELECT TOP 1 SPPB_BRANDPACK_ID FROM GON_DETAIL WHERE SPPB_BRANDPACK_ID = ANY(SELECT TOP 1 SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE SPPB_NO = @SPPB_NO)) ; "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 30)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    If CInt(retval) > 0 Then
                        hasExistedGON = True
                    End If
                End If
                If Not hasExistedGON Then
                    'input tblProduct based on sppb table
                    Dim NewRow As DataRow = Nothing
                    For i As Integer = 0 To tblSPPB.Rows.Count - 1
                        If Convert.ToDecimal(tblSPPB.Rows(i)("SPPB_QTY")) > 0 Then
                            NewRow = tblProduct.NewRow()
                            NewRow("SPPB_BRANDPACK_ID") = tblSPPB.Rows(i)("SPPB_BRANDPACK_ID")
                            NewRow("BRANDPACK_NAME") = tblSPPB.Rows(i)("BRANDPACK_NAME")
                            NewRow("LEFT_QTY") = tblSPPB.Rows(i)("SPPB_QTY")
                            NewRow("BRANDPACK_ID") = tblSPPB.Rows(i)("BRANDPACK_ID")
                            NewRow.EndEdit()
                            tblProduct.Rows.Add(NewRow)
                        End If
                    Next
                    tblProduct.AcceptChanges()
                Else
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                               "SELECT BR.BRANDPACK_NAME,SB.SPPB_BRANDPACK_ID,SB.BRANDPACK_ID,SB.SPPB_QTY - ISNULL(GON.TOTAL_GON_QTY,0) AS LEFT_QTY " & vbCrLf & _
                               " FROM ORDR_PO_BRANDPACK PB INNER JOIN BRND_BRANDPACK BR ON PB.BRANDPACK_ID = BR.BRANDPACK_ID INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOAB.PO_BRANDPACK_ID = PB.PO_BRANDPACK_ID " & vbCrLf & _
                               " INNER JOIN SPPB_BRANDPACK SB ON OOAB.OA_BRANDPACK_ID = SB.OA_BRANDPACK_ID LEFT OUTER JOIN( " & vbCrLf & _
                               " SELECT SPPB_BRANDPACK_ID,ISNULL(SUM(GON_QTY),0) AS TOTAL_GON_QTY FROM GON_DETAIL WHERE SPPB_BRANDPACK_ID = ANY(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE SPPB_NO = @SPPB_NO) " & vbCrLf & _
                               " GROUP BY SPPB_BRANDPACK_ID)GON ON GON.SPPB_BRANDPACK_ID = SB.SPPB_BRANDPACK_ID WHERE SB.SPPB_NO = @SPPB_NO AND PB.PO_ORIGINAL_QTY > 0 ; "
                    Me.ResetCommandText(CommandType.Text, Query)
                    tblProduct = New DataTable("T_GON") : tblProduct.Clear()
                    Me.setDataAdapter(Me.SqlCom).Fill(tblProduct)
                    If (tblSPPB.Rows.Count > tblProduct.Rows.Count) Then
                        Dim NewRow As DataRow = Nothing
                        For i As Integer = 0 To tblSPPB.Rows.Count - 1
                            If (tblProduct.Select("SPPB_BRANDPACK_ID = '" + tblSPPB.Rows(i)("SPPB_BRANDPACK_ID").ToString() & "'").Length <= 0) Then
                                If Convert.ToDecimal(tblSPPB.Rows(i)("SPPB_QTY")) > 0 Then
                                    NewRow = tblProduct.NewRow()
                                    NewRow("SPPB_BRANDPACK_ID") = tblSPPB.Rows(i)("SPPB_BRANDPACK_ID")
                                    NewRow("BRANDPACK_NAME") = tblSPPB.Rows(i)("BRANDPACK_NAME")
                                    NewRow("LEFT_QTY") = tblSPPB.Rows(i)("SPPB_QTY")
                                    NewRow("BRANDPACK_ID") = tblSPPB.Rows(i)("BRANDPACK_ID")
                                    NewRow.EndEdit()
                                    tblProduct.Rows.Add(NewRow)
                                End If
                            End If
                        Next
                        tblProduct.AcceptChanges()
                    End If

                    If mustCloseConnection Then : Me.CloseConnection() : End If
                End If
                Me.ClearCommandParameters()
                Return tblProduct.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Sub getMinAndMaxGON(ByVal SPPBNO As String, ByVal SPPBDate As Object, ByVal ProposedGONO As String, ByVal ProposedGONDate As Object, ByRef IsHasGON As Boolean, ByRef ResMinDate As Object, ByRef ResMaxDate As Object, ByVal MustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT GON_NO FROM GON_HEADER WHERE SPPB_NO = @SPPB_NO) ;"
                If IsNothing(Me.SqlRe) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 30)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                Dim HasGON As Boolean = False
                If Not IsNothing(retval) And Not IsDBNull(retval) Then : HasGON = True : End If
                If Not HasGON Then : IsHasGON = False
                    Me.ClearCommandParameters()
                    If MustCloseConnection Then : Me.CloseConnection() : End If
                    Return
                End If
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "SELECT TOP 1 GON_DATE FROM GON_HEADER WHERE SPPB_NO = @SPPB_NO AND GON_DATE <= @ProposedGONDate AND GON_NO != @ProposedGONNO ORDER BY GON_DATE DESC  ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@ProposedGONNO", SqlDbType.VarChar, ProposedGONDate)
                Me.AddParameter("@ProposedGONDate", SqlDbType.SmallDateTime, ProposedGONDate)
                retval = Me.SqlCom.ExecuteScalar()
                If IsDBNull(retval) Or IsNothing(retval) Then
                    ResMinDate = SPPBDate
                Else
                    ResMinDate = Convert.ToDateTime(retval)
                End If

                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT TOP 1 GON_DATE FROM GON_HEADER WHERE SPPB_NO = @SPPB_NO AND GON_DATE >= @ProposedGONDate AND GON_NO != @ProposedGONNO ORDER BY GON_DATE ASC  ;"
                Me.ResetCommandText(CommandType.Text, Query)
                retval = Me.SqlCom.ExecuteScalar()
                If IsDBNull(retval) Or IsNothing(retval) Then
                Else
                    ResMaxDate = Convert.ToDateTime(retval)
                End If
                Me.ClearCommandParameters()
                If MustCloseConnection Then : Me.CloseConnection() : End If
                'if isnothing(me.SqlCom) then :me.CreateCommandSql(
            Catch ex As Exception
                Me.ClearCommandParameters() : If MustCloseConnection Then : Me.CloseConnection() : End If : Throw ex
            End Try
        End Sub

        Public Function IshasSPPBReferencedGON(ByVal SPPB_BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT TOP 1 SPPB_BRANDPACK_ID FROM GON_DETAIL WHERE SPPB_BRANDPACK_ID = @SPPB_BRANDPACK_ID) ;"

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, SPPB_BRANDPACK_ID, 90)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    If CInt(retval) > 0 Then
                        Return True
                    End If
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        ''TAMBAHKAN COLUMN Brandpack ID DI database agar tidak terlalu nyelek data joinnya
        Public Function getGOnData(ByVal SPPB_NO As String, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                'drv("GON_DETAIL_ID") = GON_DetailID
                'drv("GON_NO") = Me.txtGONNO.Text.TrimStart().TrimEnd()
                'drv("SPPB_BRANDPACK_ID") = SPPBBrandPackID
                'drv("BRANDPACK_ID") = BrandPackID
                'drv("BRANDPACK_NAME") = BrandPackName
                'drv("GON_QTY") = GONQTy
                'drv("IsOPen") = True
                'drv("IsCompleted") = False
                'drv("IsUpdatedBySystem") = False
                'drv("CreatedBy") = NufarmBussinesRules.User.UserLogin.UserName
                'drv("CreatedDate") = NufarmBussinesRules.SharedClass.ServerDate
                'drv("ModifiedBy") = String.Empty
                'drv("ModifiedDate") = NufarmBussinesRules.SharedClass.ServerDate
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT GD.GON_DETAIL_ID,GD.GON_HEADER_ID,SB.SPPB_BRANDPACK_ID,GD.BRANDPACK_ID,BR.BRANDPACK_NAME,GD.GON_QTY,GD.IsOpen,GD.IsCompleted,GD.BatchNo,GD.UNIT1,GD.VOL1,GD.UNIT2,GD.VOL2,GD.IsUpdatedBySystem," & vbCrLf & _
                "GD.CreatedBy,GD.CreatedDate,GD.ModifiedBy,GD.ModifiedDate FROM GON_DETAIL GD INNER JOIN SPPB_BRANDPACK SB ON SB.SPPB_BRANDPACK_ID = GD.SPPB_BRANDPACK_ID " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BR ON BR.BRANDPACK_ID = GD.BRANDPACK_ID WHERE SB.SPPB_NO = @SPPB_NO ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 30)
                Dim dtTable As New DataTable("GON_DETAIL_INFO") : dtTable.Clear()
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(dtTable)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dtTable
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function getSPPBBrandPack(ByVal SPPBNumber As String, ByVal PONumber As String, ByVal IsReload As Boolean, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " IF (@IsReload = 0) " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "SELECT SB.SPPB_BRANDPACK_ID,PB.BRANDPACK_ID,SB.SPPB_NO,BP.BRANDPACK_NAME,SB.SPPB_QTY,TOTAL_GON_QTY = ISNULL(GON.TOTAL_GON_QTY,0),SB.OA_BRANDPACK_ID,SB.STATUS," & vbCrLf & _
                        " PO_CATEGORY = CASE WHEN (PB.PLANTATION_ID IS NOT NULL) THEN 'PLANTATATION' " & vbCrLf & _
                        "                     WHEN (PB.PROJ_BRANDPACK_ID IS NOT NULL) THEN 'PROJECT' " & vbCrLf & _
                        "                     ELSE 'FREE MARKET' END,  SB.CREATE_DATE,SB.CREATE_BY,SB.MODIFY_DATE,SB.MODIFY_BY," & vbCrLf & _
                        " SB.REMARK,SB.IsUpdatedBySystem FROM SPPB_BRANDPACK SB INNER JOIN ORDR_OA_BRANDPACK OOA ON OOA.OA_BRANDPACK_ID = SB.OA_BRANDPACK_ID INNER JOIN ORDR_PO_BRANDPACK PB ON PB.PO_BRANDPACK_ID = OOA.PO_BRANDPACK_ID " & vbCrLf & _
                        "INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = PB.BRANDPACK_ID " & vbCrLf & _
                        " LEFT OUTER JOIN(" & vbCrLf & _
                        "                 SELECT SPPB_BRANDPACK_ID,ISNULL(SUM(GON_QTY),0)AS TOTAL_GON_QTY FROM GON_DETAIL WHERE SPPB_BRANDPACK_ID = ANY(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK " & vbCrLf & _
                        "                 WHERE SPPB_NO = @SPPB_NO) GROUP BY SPPB_BRANDPACK_ID " & vbCrLf & _
                        "                 )GON ON GON.SPPB_BRANDPACK_ID = SB.SPPB_BRANDPACK_ID WHERE SB.SPPB_NO = @SPPB_NO ;" & vbCrLf & _
                        " END " & vbCrLf & _
                        " ELSE " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " SELECT SPPB_BRANDPACK_ID = @SPPB_NO + OOA.OA_BRANDPACK_ID,SPPB_NO = @SPPB_NO,PB.BRANDPACK_ID,BB.BRANDPACK_NAME,OOA.QTY_EVEN + ISNULL(OOB.TOTAL_DISC_QTY,0) AS SPPB_QTY,TOTAL_GON_QTY = CONVERT(DECIMAL(18,3),0),OOA.OA_BRANDPACK_ID,STATUS = '--NEW SPPB--'," & vbCrLf & _
                        " PO_CATEGORY = CASE WHEN (PB.PLANTATION_ID IS NOT NULL) THEN 'PLANTATATION' " & vbCrLf & _
                        "                     WHEN (PB.PROJ_BRANDPACK_ID IS NOT NULL) THEN 'PROJECT' " & vbCrLf & _
                        "                     ELSE 'FREE MARKET' END,CREATE_DATE = @CREATE_DATE,CREATE_BY = @CREATE_BY, " & vbCrLf & _
                        " MODIFY_BY = NULL,MODIFY_DATE = NULL,REMARK = '',IsUpdatedBySystem = CONVERT(BIT,0) FROM ORDR_OA_BRANDPACK OOA INNER JOIN ORDR_PO_BRANDPACK PB ON PB.PO_BRANDPACK_ID = OOA.PO_BRANDPACK_ID " & vbCrLf & _
                        " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = PB.BRANDPACK_ID " & vbCrLf & _
                        " LEFT OUTER JOIN(" & vbCrLf & _
                        "                 SELECT OOBD.OA_BRANDPACK_ID,ISNULL(SUM(OOBD.DISC_QTY),0) - ISNULL(SUM(AT.ADJ_DISC_QTY) ,0) AS TOTAL_DISC_QTY FROM ORDR_OA_BRANDPACK_DISC OOBD " & vbCrLf & _
                        "                 LEFT OUTER JOIN ADJUSTMENT_TRANS AT ON AT.OA_BRANDPACK_DISC_ID = OOBD.OA_BRANDPACK_DISC_ID " & vbCrLf & _
                        "                 WHERE OOBD.OA_BRANDPACK_ID = ANY( " & vbCrLf & _
                        "                                                   SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK " & vbCrLf & _
                        "                                                   WHERE PO_BRANDPACK_ID = ANY(SELECT PO_BRANDPACK_ID FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = @PO_REF_NO) " & vbCrLf & _
                        "                                                  ) " & vbCrLf & _
                        "                  GROUP BY OOBD.OA_BRANDPACK_ID " & vbCrLf & _
                        "                 )OOB ON OOB.OA_BRANDPACK_ID = OOA.OA_BRANDPACK_ID " & vbCrLf & _
                        " WHERE PB.PO_REF_NO = @PO_REF_NO " & vbCrLf & _
                        " AND PB.PO_ORIGINAL_QTY > 0 ;" & vbCrLf & _
                        " END "

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@IsReload", SqlDbType.Bit, IsReload)
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPBNumber, 30)
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PONumber, 30)
                Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)

                Dim tblSPPBBrandPack As New DataTable("SPPB_BRANDPACK_INFO") : tblSPPBBrandPack.Clear()
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(tblSPPBBrandPack)
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
                Return tblSPPBBrandPack
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        ''' <summary>
        ''' TO sum TotalGOn in table GON_DETAIL in database
        ''' </summary>
        ''' <param name="SPPBBrandPackID"></param>
        ''' <param name="mustCloseConnection"></param>
        ''' <param name="tblGon">optional if GONHeaderID is not defined and perhaps more than one ,system will loop on table gon to include any gonheaderID</param>
        ''' <param name="GONHeaderID">optional if gonheader id is defined</param>
        ''' <returns>decimal total gon in database</returns>
        ''' <remarks></remarks>
        Public Function GetTotalGONQTY(ByVal SPPBBrandPackID As String, ByVal mustCloseConnection As Boolean, Optional ByVal tblGon As DataTable = Nothing, Optional ByVal GONHeaderID As Object = Nothing) As Decimal
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT ISNULL(SUM(GON_QTY),0) FROM GON_DETAIL WHERE SPPB_BRANDPACK_ID = @SPPB_BRANDPACK_ID "
                If Not IsNothing(GONHeaderID) Then
                    Query &= vbCrLf
                    Query &= " AND GON_HEADER_ID != @GON_HEADER_ID ;"
                ElseIf Not IsNothing(tblGon) Then
                    If tblGon.Rows.Count > 0 Then
                        Dim strGONHeaderIDS As String = " NOT IN("
                        Dim listGOnHeaderID As New List(Of String) 'to check distinct data
                        For i As Integer = 0 To tblGon.Rows.Count - 1
                            Dim GHeaderID As String = tblGon.Rows(i)("GON_HEADER_ID").ToString()
                            If Not listGOnHeaderID.Contains(GHeaderID) Then
                                listGOnHeaderID.Add(GHeaderID)
                            End If
                        Next
                        If listGOnHeaderID.Count > 0 Then
                            For i As Integer = 0 To listGOnHeaderID.Count - 1
                                strGONHeaderIDS = strGONHeaderIDS & "'" & listGOnHeaderID(i) & "'"
                                If i < listGOnHeaderID.Count - 1 Then
                                    strGONHeaderIDS &= ","
                                End If
                            Next
                            If strGONHeaderIDS <> " NOT IN(" Then 'there is item(s) to filter in query to include in 
                                strGONHeaderIDS &= ")"
                                Query &= " AND GON_HEADER_ID " & strGONHeaderIDS
                            End If
                        End If
                    End If

                End If
                Query &= " ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, SPPBBrandPackID, 90)
                If Not IsNothing(GONHeaderID) Then
                    Me.AddParameter("@GON_HEADER_ID", SqlDbType.VarChar, GONHeaderID, 50)
                End If
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Return Convert.ToDecimal(retval)
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetAreaByGONNO(ByVal GONNO As String, ByVal mustCloseConnection As Boolean) As Object
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT TOP 1 AREA FROM GON_AREA WHERE GON_ID_AREA = (SELECT TOP 1 GON_ID_AREA FROM GON_HEADER WHERE GON_NO = @GON_NO) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return retval.ToString()
                End If
                Return retval
            Catch ex As Exception
                If mustCloseConnection Then : Me.CloseConnection() : End If : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getTransporterByGONNO(ByVal GONNo As String, ByVal mustCloseConnection As Boolean) As Object
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT TOP 1 TRANSPORTER_NAME FROM GON_TRANSPORTER WHERE GT_ID = (SELECT TOP 1 GT_ID FROM GON_HEADER WHERE GON_NO = @GON_NO) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return retval.ToString()
                End If
                Return retval
            Catch ex As Exception
                If mustCloseConnection Then : Me.CloseConnection() : End If : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        ''' <summary>
        ''' procedure to get transporter data to bind in multi colum combo
        ''' </summary>
        ''' <param name="SearchTrans"></param>
        ''' <param name="mode">what data to bind if mode = Update only one row data to get</param>
        ''' <param name="MustCloseConnection"></param>
        ''' <returns>DataTable</returns>
        ''' <remarks></remarks>
        Public Function getTransporter(ByVal SearchTrans As String, ByVal mode As SaveMode, ByVal MustCloseConnection As Boolean) As DataTable
            Try
                If mode = SaveMode.Insert Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT TOP 200 GT_ID,TRANSPORTER_NAME FROM GON_TRANSPORTER WHERE TRANSPORTER_NAME LIKE '%'+@SearchTrans+'%';"
                ElseIf mode = SaveMode.Update Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT GT_ID,TRANSPORTER_NAME FROM GON_TRANSPORTER ;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SearchTrans", SqlDbType.VarChar, SearchTrans, 100)
                Me.OpenConnection()
                Dim tblTrans As New DataTable("T_Trans") : tblTrans.Clear()
                Me.setDataAdapter(Me.SqlCom).Fill(tblTrans)
                Me.ClearCommandParameters()
                If MustCloseConnection Then : Me.CloseConnection() : End If
                Return tblTrans
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function getAreaGon(ByVal SearchArea As String, ByVal mode As SaveMode, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                If mode = SaveMode.Insert Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT TOP 200 GON_ID_AREA,AREA FROM GON_AREA WHERE AREA LIKE '%'+@SearchArea+'%';"
                ElseIf mode = SaveMode.Update Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT GON_ID_AREA,AREA FROM GON_AREA; "
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SearchArea", SqlDbType.VarChar, SearchArea, 100)
                Me.OpenConnection()
                Dim tblGonArea As New DataTable("T_GonArea") : tblGonArea.Clear()
                Me.setDataAdapter(Me.SqlCom).Fill(tblGonArea)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return tblGonArea
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getProdConvertion(ByVal mode As SaveMode, ByVal closeConnection As Boolean) As DataView
            Try
                If mode = SaveMode.Insert Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT BRANDPACK_ID,UNIT1,VOL1,UNIT2,VOL2 FROM BRND_PROD_CONV WHERE INACTIVE = 0;"
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT BRANDPACK_ID,UNIT1,VOL1,UNIT2,VOL2 FROM BRND_PROD_CONV;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If

                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtProdConvertion As New DataTable("T_ProdConvertion")
                Me.OpenConnection()

                dtProdConvertion.Clear()
                setDataAdapter(Me.SqlCom).Fill(dtProdConvertion)
                Me.ClearCommandParameters()
                If closeConnection Then : Me.CloseConnection() : End If
                Return dtProdConvertion.DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace

