Imports System.Data.SqlClient
Imports NufarmBussinesRules.common.Helper
Imports Nufarm.Domain
Namespace OrderAcceptance
    Public Class SeparatedGON

        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Protected Query As String = ""
        Public GonMaster As NufarmBussinesRules.OrderAcceptance.SPPBEntryGON = Nothing
        Public Sub New()
            MyBase.New()
            GonMaster = New NufarmBussinesRules.OrderAcceptance.SPPBEntryGON()
        End Sub
        Public Function getProdConvertion(ByVal mode As SaveMode, ByVal closeConnection As Boolean) As DataView
            Try
                If mode = SaveMode.Insert Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT BRANDPACK_ID,UNIT1,VOL1,UNIT2,VOL2,INACTIVE FROM BRND_PROD_CONV WHERE INACTIVE = 0;"
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT BRANDPACK_ID,UNIT1,VOL1,UNIT2,VOL2,INACTIVE FROM BRND_PROD_CONV;"
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtProdConvertion As New DataTable("T_ProdConvertion")
                Me.OpenConnection()
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                dtProdConvertion.Clear()
                setDataAdapter(Me.SqlCom).Fill(dtProdConvertion)
                Me.ClearCommandParameters()
                If closeConnection Then : Me.CloseConnection() : End If
                Return dtProdConvertion.DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getTotalGon(ByVal PORefNo As String, ByVal BrandPackID As String, ByVal IDpp As Object, ByVal closeConnection As Boolean) As Decimal
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT ISNULL(SUM(GSD.QTY),0) AS TOTAL FROM GON_SEPARATED_DETAIL GSD INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSD.FKAppPODetail = GSPD.IDApp " & vbCrLf & _
                " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp WHERE GSPH.PO_NUMBER = @PO_NUMBER  AND GSPD.BRANDPACK_ID = @BRANDPACK_ID AND GSD.IDApp != @IDApp;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PORefNo)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                Me.AddParameter("@IDApp", SqlDbType.Int, IDpp)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If closeConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return Convert.ToDecimal(retval)
                End If
                Return 0
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getTotalGon(ByVal PORefNo As String, ByVal BrandPackID As String, ByVal closeConnection As Boolean) As Decimal
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT ISNULL(SUM(GSD.QTY),0) AS TOTAL FROM GON_SEPARATED_DETAIL GSD INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSD.FKAppPODetail = GSPD.IDApp " & vbCrLf & _
                " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp WHERE GSPH.PO_NUMBER = @PO_NUMBER  AND GSPD.BRANDPACK_ID = @BRANDPACK_ID;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PORefNo)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If closeConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return Convert.ToDecimal(retval)
                End If
                Return 0
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getTotalGons(ByVal PORefNo As String, ByVal closeConnection As Boolean, Optional ByVal GON_NO As String = "") As DataTable
            Try
                If Not String.IsNullOrEmpty(GON_NO) Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT GSPD.BRANDPACK_ID, ISNULL(SUM(GSD.QTY),0) AS LEFT_QTY FROM GON_SEPARATED_DETAIL GSD INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSD.FKAppPODetail = GSPD.IDApp " & vbCrLf & _
                    " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp INNER JOIN GON_SEPARATED_HEADER GSH ON GSH.FKApp = GSPH.IDApp AND GSH.IDApp = GSD.FKAppGonHeader WHERE GSPH.PO_NUMBER = @PO_NUMBER AND GSH.GON_NUMBER != @GON_NO GROUP BY GSPD.BRANDPACK_ID;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If

                    Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PORefNo)
                    Me.AddParameter("@GON_NO", SqlDbType.VarChar, GON_NO)
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT GSPD.BRANDPACK_ID, ISNULL(SUM(GSD.QTY),0) AS LEFT_QTY FROM GON_SEPARATED_DETAIL GSD INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSD.FKAppPODetail = GSPD.IDApp " & vbCrLf & _
                    " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp WHERE GSPH.PO_NUMBER = @PO_NUMBER GROUP BY GSPD.BRANDPACK_ID;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PORefNo)
                End If

                Me.OpenConnection()
                Dim tblGons As New DataTable("T_GON")
                setDataAdapter(Me.SqlCom).Fill(tblGons)
                Me.ClearCommandParameters()
                If closeConnection Then : Me.CloseConnection() : End If
                Return tblGons
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getLeftQty(ByVal PORefNo As String, ByVal BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As Decimal
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT GSPD.QUANTITY - ISNULL(IGSD.QTY,0) AS LEFT_QTY,EXIST = IGSD.QTY FROM GON_SEPARATED_PO_DETAIL GSPD " & vbCrLf & _
                " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp " & vbCrLf & _
                " INNER JOIN(SELECT FKAppPODetail,SUM(QTY) AS QTY FROM GON_SEPARATED_DETAIL GROUP BY FKAppPODetail)IGSD ON IGSD.FKAppPODetail = GSPD.IDApp WHERE GSPH.PO_NUMBER = @PO_NUMBER " & vbCrLf & _
                " AND GSPD.BRANDPACK_ID = @BRANDPACK_ID ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PORefNo)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID)
                Me.OpenConnection()
                Dim exist As Boolean = False, LeftQty As Decimal = 0
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    exist = CBool(Not IsNothing(Me.SqlRe("EXIST")))
                    LeftQty = Convert.ToDecimal(SqlRe("LEFT_QTY"))
                End While : Me.SqlRe.Close()
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If exist Then
                    Return LeftQty
                End If
                Return 0
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        ''' <summary>
        ''' buat di bind di chk product
        ''' </summary>
        ''' <param name="PoRefNO"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getPODetail(ByVal PoRefNO As String, ByVal CloseConnection As Boolean) As DataView
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT GSPD.BRANDPACK_ID,BP.BRANDPACK_NAME,GSPD.QUANTITY - ISNULL(IGSD.QTY,0) AS LEFT_QTY FROM GON_SEPARATED_PO_DETAIL GSPD " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = GSPD.BRANDPACK_ID " & vbCrLf & _
                " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp " & vbCrLf & _
                " LEFT OUTER JOIN(SELECT FKAppPODetail,SUM(QTY) AS QTY FROM GON_SEPARATED_DETAIL GROUP BY FKAppPODetail)IGSD ON IGSD.FKAppPODetail = GSPD.IDApp WHERE GSPH.PO_NUMBER = @PO_NUMBER ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PoRefNO)
                Dim dtPODetail As New DataTable("T_PODetail")
                Me.OpenConnection()
                setDataAdapter(Me.SqlCom).Fill(dtPODetail)
                Me.ClearCommandParameters()
                If CloseConnection Then : Me.CloseConnection() : End If
                Return dtPODetail.DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        ''' <summary>
        ''' Mengambil PO data saja
        ''' </summary>
        ''' <param name="PONumber"></param>
        ''' <param name="OSPPBHeader"></param>
        ''' <param name="mustCloseConnection"></param>
        ''' <remarks></remarks>
        Public Sub getPOData(ByVal PONumber As String, ByRef OSPPBHeader As SPPBHeader, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                           " SELECT * FROM GON_SEPARATED_PO_HEADER WHERE PO_NUMBER = @PO_NUMBER;"
                Me.OpenConnection()
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PONumber, 100)
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    With OSPPBHeader
                        .CreatedBy = SqlRe("CreatedBy")
                        .CreatedDate = SqlRe("CreatedDate")
                        .IDApp = SqlRe("IDApp")
                        .PODate = SqlRe("PO_DATE")
                        .PONumber = SqlRe("PO_NUMBER")
                        .SPPBDate = SqlRe("SPPB_DATE")
                        .SPPBNO = SqlRe("SPPB_NUMBER")
                        .SPPBShipTo = SqlRe("SHIP_TO")
                    End With
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    Me.SqlRe.Close() : Me.SqlRe = Nothing
                End If
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Untuk menampilkan edit data bila gon sudah ada
        ''' </summary>
        ''' <param name="GON_Number"></param>
        ''' <param name="OSPPBHeader"></param>
        ''' <param name="OGONHeader"></param>
        ''' <param name="mustCloseConnection"></param>
        ''' <remarks></remarks>
        Public Sub getFormData(ByVal GON_Number As String, ByRef OSPPBHeader As SPPBHeader, ByRef OGONHeader As GONHeader, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT * FROM GON_SEPARATED_HEADER WHERE GON_NUMBER = @GON_NUMBER ;"
                Me.OpenConnection()
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@GON_NUMBER", SqlDbType.VarChar, GON_Number, 100)
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    With OGONHeader
                        .CustomerAddress = SqlRe("SHIP_TO_ADDRESS")
                        Dim ShipToCust As Object = Nothing, DistributorID As Object = Nothing, GTID As Object = Nothing
                        Dim GOnIDArea As Object = Nothing
                        ShipToCust = SqlRe("SHIP_TO_CUST")
                        DistributorID = SqlRe("SHIP_TO")
                        GOnIDArea = SqlRe("GON_AREA")
                        GTID = SqlRe("TRANSPORTER")
                        .CustomerName = IIf((IsNothing(ShipToCust) Or IsDBNull(ShipToCust)), "", ShipToCust.ToString())
                        .DistributorID = IIf((IsNothing(DistributorID) Or IsDBNull(DistributorID)), "", DistributorID.ToString())
                        .DriverTrans = SqlRe("DRIVER_TRANS")
                        .GON_DATE = SqlRe("GON_DATE")
                        .GON_ID_AREA = IIf((IsNothing(GOnIDArea) Or IsDBNull(GOnIDArea)), "", GOnIDArea.ToString())
                        .GON_NO = GON_Number
                        .GT_ID = IIf((IsNothing(GTID) Or IsDBNull(GTID)), "", GTID.ToString())
                        .IDApp = SqlRe("IDApp")
                        .PoliceNoTrans = SqlRe("POLICE_NO_TRANS")
                        .WarhouseCode = SqlRe("WARHOUSE")
                        .FkApp = SqlRe("FKApp")
                        .CreatedBy = SqlRe("CreatedBy")
                        .CreatedDate = SqlRe("CreatedDate")
                        .ShipTo = SqlRe("GON_SHIP_TO")
                    End With
                End While : SqlRe.Close()
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT GSPH.* FROM GON_SEPARATED_PO_HEADER GSPH INNER JOIN GON_SEPARATED_HEADER GSH ON GSPH.IDApp = GSH.FKApp WHERE GSH.GON_NUMBER = @GON_NUMBER ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    With OSPPBHeader
                        .IDApp = SqlRe("IDApp")
                        .PONumber = SqlRe("PO_NUMBER")
                        .PODate = SqlRe("PO_DATE")
                        .SPPBNO = SqlRe("SPPB_NUMBER")
                        .SPPBDate = SqlRe("SPPB_DATE")
                        .SPPBShipTo = SqlRe("SHIP_TO")
                        .CreatedBy = SqlRe("CreatedBy")
                        .CreatedDate = SqlRe("CreatedDate")
                    End With
                End While : SqlRe.Close()
                Me.ClearCommandParameters()
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    Me.SqlRe.Close() : Me.SqlRe = Nothing
                End If
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function getBrandPack(ByVal closeConnection As Boolean) As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                "SELECT BRANDPACK_ID,BRANDPACK_NAME FROM BRND_BRANDPACK WHERE IsActive = 1 AND (IsObsolete = 0 or IsObsolete IS NULL);"

                Dim dtBrandPack As New DataTable("T_BrandPack")
                Me.OpenConnection()
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                dtBrandPack.Clear() : setDataAdapter(Me.SqlCom).Fill(dtBrandPack)
                Me.ClearCommandParameters()
                If closeConnection Then : Me.CloseConnection() : End If
                Return dtBrandPack.DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function SaveData(ByVal Mode As SaveMode, ByVal HasChangedpoHeader As Boolean, ByVal HasChangedPOdetail As Boolean, _
        ByVal HasChangedGoneHeader As Boolean, ByVal HasChangedGONDetail As Boolean, ByRef OGonHeader As GONHeader, ByRef OSPPBHeader As SPPBHeader, _
        ByRef DtPOdetail As DataTable, ByRef dtGonDetail As DataTable) As Boolean
            Dim commandInsert As SqlCommand = Me.SqlConn.CreateCommand(), commandUpdate As SqlCommand = Me.SqlConn.CreateCommand(), _
            commandDelete As SqlCommand = Me.SqlConn.CreateCommand(), CommandSelect As SqlCommand = SqlConn.CreateCommand()

            Try

                Dim QryGonPOHeader As String = "", QryGonPODetail As String = "", QryGONHeader As String = "", QryGonDetail As String = ""
                Me.OpenConnection()
                Me.SqlDat = New SqlDataAdapter()
                SqlDat.AcceptChangesDuringUpdate = False
                Dim UpdatedRows() As DataRow = Nothing, DeletedRows() As DataRow = Nothing, InsertedRows() As DataRow = Nothing
                Dim QrySelectGonPODetail As String = "", QrySelectGonDetail As String = ""
                Dim SavePODetail As Boolean = False, SaveGonDetail As Boolean = False
                If Mode = SaveMode.Update And HasChangedPOdetail Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_PODetailIdentity_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                    " CREATE TABLE ##T_PODetailIdentity_" & Me.ComputerName & " ( " & vbCrLf & _
                    " [PO_NUMBER] [varchar] (50),[BRANDPACK_ID] [VARCHAR](30),[IDApp][INT] )" '& vbCrLf & _
                    '" COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL) "
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                    Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    End If
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                SqlTrans = Me.SqlConn.BeginTransaction()
                Dim qryDelGonPODetail As String = "SET NOCOUNT ON;" & vbCrLf & _
                "IF EXISTS(SELECT * FROM GON_SEPARATED_DETAIL WHERE FKAppPODetail = @IDApp) " & vbCrLf & _
                " BEGIN DELETE FROM GON_SEPARATED_DETAIL WHERE FKAppPODetail = @IDApp ; END " & vbCrLf & _
                " DELETE FROM GON_SEPARATED_PO_DETAIL WHERE IDApp = @IDApp ;"
                Dim qryDelGonDetail As String = "SET NOCOUNT ON;" & vbCrLf & _
                " IF EXISTS(SELECT IDApp FROM GON_SEPARATED_DETAIL WHERE IDApp = @IDApp) " & vbCrLf & _
                " BEGIN DELETE FROM GON_SEPARATED_DETAIL WHERE IDApp = @IDApp ; END"
                Select Case Mode
                    Case SaveMode.Insert
                        ''po
                        QryGonPOHeader = "SET NOCOUNT ON;" & vbCrLf & _
                        "DECLARE @V_MESSAGE VARCHAR(200);" & vbCrLf & _
                        " SET @V_MESSAGE = CONCAT('PO NUMBER ' , @PO_NUMBER , ' Has already existed');" & vbCrLf & _
                        " IF EXISTS(SELECT PO_NUMBER FROM GON_SEPARATED_PO_HEADER WHERE PO_NUMBER = @PO_NUMBER) " & vbCrLf & _
                        " BEGIN RAISERROR(@V_MESSAGE,16,1);RETURN; END" & vbCrLf & _
                        "INSERT INTO GON_SEPARATED_PO_HEADER(PO_NUMBER,PO_DATE,SPPB_NUMBER,SPPB_DATE,SHIP_TO,CreatedBy,CreatedDate) " & vbCrLf & _
                        " VALUES(@PO_NUMBER,@PO_DATE,@SPPB_NUMBER,@SPPB_DATE,@SHIP_TO,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101)); " & vbCrLf & _
                        "SELECT @@IDENTITY;"
                        QryGonPODetail = "SET NOCOUNT ON;" & vbCrLf & _
                        " INSERT INTO GON_SEPARATED_PO_DETAIL(FKApp,BRANDPACK_ID,QUANTITY,STATUS,CreatedBy,CreatedDate) " & vbCrLf & _
                        " VALUES(@FKApp,@BRANDPACK_ID,@QUANTITY,@STATUS,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101)) ;" & vbCrLf & _
                        "SELECT @@IDENTITY; "

                        QryGONHeader = "SET NOCOUNT ON; " & vbCrLf & _
                        "DECLARE @V_MESSAGE VARCHAR(200);" & vbCrLf & _
                        " SET @V_MESSAGE = CONCAT('GON NUMBER ' , @GON_NUMBER , ' Has already existed');" & vbCrLf & _
                        " IF EXISTS(SELECT GON_NUMBER FROM GON_SEPARATED_HEADER WHERE GON_NUMBER = @GON_NUMBER) " & vbCrLf & _
                        " BEGIN  RAISERROR(@V_MESSAGE,16,1);RETURN; END" & vbCrLf & _
                        " INSERT INTO GON_SEPARATED_HEADER(FKApp,GON_NUMBER,GON_DATE,WARHOUSE,SHIP_TO,SHIP_TO_CUST,SHIP_TO_ADDRESS,TRANSPORTER,POLICE_NO_TRANS,DRIVER_TRANS,REMARK,GON_AREA,GON_SHIP_TO,CreatedDate,CreatedBy) " & vbCrLf & _
                        " VALUES (@FKApp,@GON_NUMBER,@GON_DATE,@WARHOUSE,@SHIP_TO,@SHIP_TO_CUST,@SHIP_TO_ADDRESS,@TRANSPORTER,@POLICE_NO_TRANS,@DRIVER_TRANS,'',@GON_AREA,@GON_SHIP_TO,CONVERT(VARCHAR(100),GETDATE(),101),@CreatedBy) ;" & vbCrLf & _
                        " SELECT @@IDENTITY;"
                        QryGonDetail = "SET NOCOUNT ON;" & vbCrLf & _
                        " INSERT INTO GON_SEPARATED_DETAIL(FKAppGonHeader,FKAppPODetail,ITEM,QTY,COLLY_BOX,COLLY_PACKSIZE,BATCH_NO,CreatedBy,CreatedDate) " & vbCrLf & _
                        " VALUES(@FKAppGonHeader,@FKAppPODetail,@ITEM,@QTY,@COLLY_BOX,@COLLY_PACKSIZE,@BATCH_NO,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101)) ;"
                    Case SaveMode.Update
                        QryGonPOHeader = "SET NOCOUNT ON;" & vbCrLf & _
                        "UPDATE GON_SEPARATED_PO_HEADER SET PO_DATE = @PO_DATE,SPPB_NUMBER = @SPPB_NUMBER,SPPB_DATE = @SPPB_DATE,SHIP_TO=@SHIP_TO,ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),101),ModifiedBy = @ModifiedBy " & vbCrLf & _
                        " WHERE PO_NUMBER = @PO_NUMBER ;"
                        QryGonPODetail = "SET NOCOUNT ON;" & vbCrLf & _
                        " UPDATE GON_SEPARATED_PO_DETAIL SET BRANDPACK_ID = @BRANDPACK_ID,QUANTITY = @QUANTITY,STATUS=@STATUS,ModifiedBy = @Modifiedby,ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),101) WHERE IDApp=@IDApp ;"
                        QryGONHeader = "SET NOCOUNT ON;" & vbCrLf & _
                        " UPDATE GON_SEPARATED_HEADER SET GON_NUMBER = @GON_NUMBER,GON_DATE = @GON_DATE,WARHOUSE = @WARHOUSE,SHIP_TO = @SHIP_TO,SHIP_TO_CUST = @SHIP_TO_CUST,SHIP_TO_ADDRESS=@SHIP_TO_ADDRESS, " & vbCrLf & _
                        "TRANSPORTER = @TRANSPORTER,POLICE_NO_TRANS=@POLICE_NO_TRANS,DRIVER_TRANS=@DRIVER_TRANS,GON_AREA = @GON_AREA,GON_SHIP_TO=@GON_SHIP_TO,ModifiedBy = @ModifiedBy,ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),101) WHERE IDApp = @IDApp;"
                        QryGonDetail = "SET NOCOUNT ON;" & vbCrLf & _
                        " UPDATE GON_SEPARATED_DETAIL SET ITEM=@ITEM,QTY = @QTY,COLLY_BOX=@COLLY_BOX,COLLY_PACKSIZE=@COLLY_PACKSIZE,BATCH_NO=@BATCH_NO,ModifiedBy=@ModifiedBy,ModifiedDate=CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                        " WHERE IDApp = @IDApp ;"
                End Select
                If Mode = SaveMode.Insert Then
                    Dim IDAppSPPBHeader As Integer = 0, IDAppGOnHeader As Integer = 0, IDAppPODatail As Integer = 0
                    With commandInsert
                        Dim OIDAppSPPHeader As Object = Nothing
                        If HasChangedpoHeader Then
                            .CommandText = QryGonPOHeader
                            .CommandType = CommandType.Text
                            .Transaction = SqlTrans
                            .Parameters.Add("@PO_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.PONumber
                            .Parameters.Add("@PO_DATE", SqlDbType.DateTime, 0).Value = OSPPBHeader.PODate
                            .Parameters.Add("@SPPB_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.SPPBNO
                            .Parameters.Add("@SPPB_DATE", SqlDbType.DateTime, 0).Value = OSPPBHeader.SPPBDate
                            .Parameters.Add("@SHIP_TO", SqlDbType.VarChar, 250).Value = OSPPBHeader.SPPBShipTo
                            .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100).Value = OSPPBHeader.CreatedBy     'GET IDApp
                            OIDAppSPPHeader = .ExecuteScalar()
                            .Parameters.Clear()
                            If IsNothing(OIDAppSPPHeader) Or IsDBNull(OIDAppSPPHeader) Then
                                SqlTrans.Rollback()
                                SqlTrans.Dispose()
                                .Connection.Close()
                                Throw New Exception("unknown error" & vbCrLf & "On possition of inserting GON_SEPARATED_PO_HEADER")
                            End If
                            IDAppSPPBHeader = CInt(OIDAppSPPHeader)
                            OSPPBHeader.IDApp = IDAppSPPBHeader
                        Else
                            IDAppSPPBHeader = OSPPBHeader.IDApp
                        End If

                        OGonHeader.FkApp = IDAppSPPBHeader
                        Dim OIDAppGonHeader As Object = Nothing
                        If HasChangedGoneHeader Then
                            If Not IsNothing(OGonHeader) Then
                                .CommandText = QryGONHeader
                                .Parameters.Add("@FKApp", SqlDbType.Int, 0).Value = IDAppSPPBHeader
                                .Parameters.Add("@GON_NUMBER", SqlDbType.VarChar, 50).Value = OGonHeader.GON_NO
                                .Parameters.Add("@GON_DATE", SqlDbType.SmallDateTime, 0).Value = OGonHeader.GON_DATE
                                .Parameters.Add("@WARHOUSE", SqlDbType.VarChar, 20).Value = OGonHeader.WarhouseCode
                                .Parameters.Add("@SHIP_TO", SqlDbType.VarChar, 14).Value = IIf(OGonHeader.DistributorID <> "", OGonHeader.DistributorID, DBNull.Value)
                                .Parameters.Add("@SHIP_TO_CUST", SqlDbType.VarChar, 100).Value = OGonHeader.CustomerName
                                .Parameters.Add("@SHIP_TO_ADDRESS", SqlDbType.VarChar, 150).Value = OGonHeader.CustomerAddress
                                .Parameters.Add("@TRANSPORTER", SqlDbType.VarChar, 16).Value = IIf(OGonHeader.GT_ID <> "", OGonHeader.GT_ID, DBNull.Value)
                                .Parameters.Add("@POLICE_NO_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.PoliceNoTrans
                                .Parameters.Add("@DRIVER_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.DriverTrans
                                .Parameters.Add("@GON_AREA", SqlDbType.VarChar, 20).Value = IIf(OGonHeader.GON_ID_AREA <> "", OGonHeader.GON_ID_AREA, DBNull.Value)
                                .Parameters.Add("@GON_SHIP_TO", SqlDbType.VarChar, 250).Value = OGonHeader.ShipTo
                                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100).Value = OGonHeader.CreatedBy
                                If .Transaction Is Nothing Then
                                    .Transaction = SqlTrans
                                End If
                                OIDAppGonHeader = .ExecuteScalar()
                                .Parameters.Clear()
                            End If
                            If Not IsNothing(OIDAppGonHeader) Then
                                IDAppGOnHeader = CInt(OIDAppGonHeader)
                            End If
                            If IsNothing(OIDAppGonHeader) Or IsDBNull(OIDAppGonHeader) Then
                                SqlTrans.Rollback()
                                SqlTrans.Dispose()
                                .Connection.Close()
                                Throw New Exception("unknown error" & vbCrLf & "On possition of inserting GON_SEPARATED_HEADER")
                            End If
                            OGonHeader.IDApp = IDAppGOnHeader
                        End If
                        Dim listBrandPackID As New List(Of String)
                        If Not IsNothing(DtPOdetail) Then
                            Dim POInsertedRows() As DataRow = DtPOdetail.Select("", "", DataViewRowState.Added)
                            If POInsertedRows.Length > 0 Then
                                For Each rows As DataRow In POInsertedRows
                                    .CommandText = QryGonPODetail
                                    .Parameters.Add("@FKApp", SqlDbType.Int, 0).Value = IDAppSPPBHeader
                                    Dim BrandPackID As String = rows("BRANDPACK_ID")
                                    Dim status As String = rows("STATUS")
                                    .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14).Value = BrandPackID
                                    .Parameters.Add("@QUANTITY", SqlDbType.Decimal, 0).Value = rows("QUANTITY")
                                    .Parameters.Add("@STATUS", SqlDbType.VarChar, 50).Value = status
                                    .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100).Value = OSPPBHeader.CreatedBy
                                    If .Transaction Is Nothing Then
                                        .Transaction = SqlTrans
                                    End If
                                    Dim OIDAppPODatail As Object = .ExecuteScalar()
                                    .Parameters.Clear()
                                    If IsNothing(OIDAppPODatail) Or IsDBNull(OIDAppPODatail) Then
                                        SqlTrans.Rollback()
                                        SqlTrans.Dispose()
                                        .Connection.Close()
                                        Throw New Exception("unknown error" & vbCrLf & "On possition of inserting GON_SEPARATED_PO_DETAIL")
                                    End If
                                    IDAppPODatail = CInt(OIDAppPODatail)
                                    SavePODetail = True
                                    If Not IsNothing(dtGonDetail) And HasChangedGONDetail Then
                                        Dim foundRows() As DataRow = dtGonDetail.Select("ITEM = '" & BrandPackID & "'")
                                        If foundRows.Length > 0 Then
                                            ''insert GON
                                            .CommandText = QryGonDetail
                                            .Parameters.Add("@FKAppGonHeader", SqlDbType.Int, 0).Value = IDAppGOnHeader
                                            .Parameters.Add("@FKAppPODetail", SqlDbType.Int, 0).Value = IDAppPODatail
                                            .Parameters.Add("@ITEM", SqlDbType.VarChar, 14).Value = foundRows(0)("ITEM")
                                            .Parameters.Add("@QTY", SqlDbType.Decimal, 0).Value = foundRows(0)("QTY")
                                            .Parameters.Add("@COLLY_BOX", SqlDbType.VarChar, 50).Value = foundRows(0)("COLLY_BOX")
                                            .Parameters.Add("@COLLY_PACKSIZE", SqlDbType.VarChar, 50).Value = foundRows(0)("COLLY_PACKSIZE")
                                            .Parameters.Add("@BATCH_NO", SqlDbType.NVarChar).Value = foundRows(0)("BATCH_NO")
                                            .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = OGonHeader.CreatedBy
                                            .ExecuteScalar()
                                            If Not listBrandPackID.Contains(BrandPackID) Then
                                                listBrandPackID.Add(BrandPackID)
                                            End If
                                            .Parameters.Clear()
                                            SaveGonDetail = True
                                        End If
                                    End If
                                Next
                            End If
                            UpdatedRows = DtPOdetail.Select("", "", DataViewRowState.ModifiedOriginal)
                            If UpdatedRows.Length > 0 Then
                                With commandUpdate
                                    .CommandText = "SET NOCOUNT ON;" & vbCrLf & _
                                                    " UPDATE GON_SEPARATED_PO_DETAIL SET BRANDPACK_ID = @BRANDPACK_ID,QUANTITY = @QUANTITY,STATUS=@STATUS,ModifiedBy = @Modifiedby,ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),101) WHERE IDApp=@IDApp ;"
                                    .CommandType = CommandType.Text
                                    If IsNothing(.Transaction) Then
                                        .Transaction = SqlTrans
                                    End If
                                    .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                                    .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                    .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                                    .Parameters.Add("@QUANTITY", SqlDbType.Decimal, 0, "QUANTITY")
                                    .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100, "ModifiedBy")
                                    .Parameters.Add("@STATUS", SqlDbType.VarChar, 50, "STATUS")
                                    SqlDat.UpdateCommand = commandUpdate
                                    SqlDat.Update(UpdatedRows) : SavePODetail = True
                                    .Parameters.Clear()
                                End With
                                SqlDat.UpdateCommand = Nothing
                            End If
                            DeletedRows = DtPOdetail.Select("", "", DataViewRowState.Deleted)
                            If DeletedRows.Length > 0 Then
                                With commandDelete
                                    .CommandText = qryDelGonPODetail
                                    .CommandType = CommandType.Text
                                    If IsNothing(.Transaction) Then
                                        .Transaction = SqlTrans
                                    End If
                                    .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                                    .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                    SqlDat.DeleteCommand = commandDelete
                                    SqlDat.Update(DeletedRows) : SavePODetail = True
                                    .Parameters.Clear()
                                End With
                                SqlDat.DeleteCommand = Nothing
                            End If
                        End If
                        Dim strBrandPackIDS As String = "ITEM NOT IN("
                        If listBrandPackID.Count > 0 Then
                            For i As Integer = 0 To listBrandPackID.Count - 1
                                strBrandPackIDS &= "'"
                                strBrandPackIDS &= listBrandPackID(i)
                                strBrandPackIDS &= "'"
                                If i < listBrandPackID.Count - 1 Then
                                    strBrandPackIDS &= ","
                                End If
                            Next
                            strBrandPackIDS &= ")"
                        End If
                        If HasChangedGONDetail Then
                            InsertedRows = dtGonDetail.Select(IIf(listBrandPackID.Count > 0, strBrandPackIDS, ""), "", DataViewRowState.Added)
                            If InsertedRows.Length > 0 Then
                                .CommandText = QryGonDetail
                                .CommandType = CommandType.Text
                                .Parameters.Add("@FKAppGonHeader", SqlDbType.Int, 0).Value = IDAppGOnHeader
                                .Parameters.Add("@FKAppPODetail", SqlDbType.Int, 0, "FKAppPODetail")
                                .Parameters.Add("@ITEM", SqlDbType.VarChar, 14, "ITEM")
                                .Parameters.Add("@QTY", SqlDbType.Decimal, 0, "QTY")
                                .Parameters.Add("@COLLY_BOX", SqlDbType.VarChar, 50, "COLLY_BOX")
                                .Parameters.Add("@COLLY_PACKSIZE", SqlDbType.VarChar, 50, "COLLY_PACKSIZE")
                                .Parameters.Add("@BATCH_NO", SqlDbType.NVarChar, 50, "BATCH_NO")
                                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50, "CreatedBy")
                                If .Transaction Is Nothing Then
                                    .Transaction = SqlTrans
                                End If
                                SqlDat.InsertCommand = commandInsert
                                SqlDat.Update(InsertedRows) : SaveGonDetail = True
                                .Parameters.Clear()
                                SqlDat.InsertCommand = Nothing
                            End If
                            Dim ModifiedRows() As DataRow = dtGonDetail.Select("", "", DataViewRowState.ModifiedOriginal)
                            If ModifiedRows.Length > 0 Then
                                With commandUpdate
                                    .CommandText = "SET NOCOUNT ON;" & vbCrLf & _
                                                    " UPDATE GON_SEPARATED_DETAIL SET ITEM=@ITEM,QTY = @QTY,COLLY_BOX=@COLLY_BOX,COLLY_PACKSIZE=@COLLY_PACKSIZE,BATCH_NO=@BATCH_NO,ModifiedBy=@ModifiedBy,ModifiedDate=CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                                                    " WHERE IDApp = @IDApp ;"
                                    .CommandType = CommandType.Text
                                    If IsNothing(.Transaction) Then
                                        .Transaction = SqlTrans
                                    End If
                                    .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                                    .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                    .Parameters.Add("@ITEM", SqlDbType.VarChar, 14, "ITEM")
                                    .Parameters.Add("@QTY", SqlDbType.Decimal, 0, "QTY") '.Value = foundRows(0)("QTY")
                                    .Parameters.Add("@COLLY_BOX", SqlDbType.VarChar, 50, "COLLY_BOX") '.Value = foundRows(0)("COLLY_BOX")
                                    .Parameters.Add("@COLLY_PACKSIZE", SqlDbType.VarChar, 50, "COLLY_PACKSIZE") '.Value = foundRows(0)("COLLY_PACKSIZE")
                                    .Parameters.Add("@BATCH_NO", SqlDbType.NVarChar, 50, "BATCH_NO") '.Value = foundRows(0)("BATCH_NO")
                                    .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 50, "ModifiedBy") '.Value = OGonHeader.CreatedBy
                                    SqlDat.UpdateCommand = commandUpdate
                                    SqlDat.Update(ModifiedRows) : SaveGonDetail = True
                                    .Parameters.Clear()
                                End With
                                SqlDat.UpdateCommand = Nothing
                            End If
                            DeletedRows = dtGonDetail.Select("", "", DataViewRowState.Deleted)
                            If DeletedRows.Length > 0 Then
                                With commandDelete
                                    .CommandText = qryDelGonDetail
                                    .CommandType = CommandType.Text
                                    If IsNothing(.Transaction) Then
                                        .Transaction = SqlTrans
                                    End If
                                    .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                                    .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                    SqlDat.DeleteCommand = commandDelete
                                    SqlDat.Update(DeletedRows) : SaveGonDetail = True
                                    .Parameters.Clear()
                                End With
                                SqlDat.DeleteCommand = Nothing
                            End If
                        End If

                    End With
                Else
                    If HasChangedpoHeader Then
                        With commandUpdate
                            .CommandText = QryGonPOHeader
                            .CommandType = CommandType.Text
                            .Transaction = SqlTrans
                            .Parameters.Add("@PO_DATE", SqlDbType.DateTime, 0).Value = OSPPBHeader.PODate
                            .Parameters.Add("@SPPB_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.SPPBNO
                            .Parameters.Add("@SPPB_DATE", SqlDbType.DateTime, 0).Value = OSPPBHeader.SPPBDate
                            .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 50).Value = OSPPBHeader.ModifiedBy
                            .Parameters.Add("@SHIP_TO", SqlDbType.VarChar, 250).Value = OSPPBHeader.SPPBShipTo
                            .ExecuteScalar()
                            .Parameters.Clear()
                        End With
                    End If
                    If HasChangedPOdetail Then
                        InsertedRows = DtPOdetail.Select("", "", DataViewRowState.Added)
                        If InsertedRows.Length > 0 Then
                            With commandInsert
                                Query = " SET NOCOUNT ON;" & vbCrLf & _
                                        "DECLARE @V_IDApp INT ;" & vbCrLf & _
                                " INSERT INTO GON_SEPARATED_PO_DETAIL(FKApp,BRANDPACK_ID,QUANTITY,STATUS,CreatedBy,CreatedDate) " & vbCrLf & _
                                " VALUES(@FKApp,@BRANDPACK_ID,@QUANTITY,@STATUS,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101)) ;" & vbCrLf & _
                                " SELECT @V_IDApp = @@IDENTITY; " & vbCrLf & _
                                " INSERT INTO ##T_PODetailIdentity_" & Me.ComputerName & "(PO_NUMBER,BRANDPACK_ID,IDApp)" & vbCrLf & _
                                " VALUES(@PO_NUMBER,@BRANDPACK_ID,@V_IDApp) ;"
                                .CommandType = CommandType.Text
                                .CommandText = Query
                                .Parameters.Add("PO_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.PONumber
                                .Parameters.Add("@FKApp", SqlDbType.Int, 0).Value = OSPPBHeader.IDApp
                                .Parameters.Add("@STATUS", SqlDbType.VarChar, 50, "STATUS")
                                .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                                .Parameters.Add("@QUANTITY", SqlDbType.Decimal, 0, "QUANTITY")
                                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                                If .Transaction Is Nothing Then
                                    .Transaction = SqlTrans
                                End If
                                SqlDat.InsertCommand = commandInsert
                                SqlDat.Update(InsertedRows) : SavePODetail = True
                                .Parameters.Clear()
                                SqlDat.InsertCommand = Nothing
                            End With
                        End If
                        'check yang di update rows
                        UpdatedRows = DtPOdetail.Select("", "", DataViewRowState.ModifiedOriginal)
                        If UpdatedRows.Length > 0 Then
                            With commandUpdate
                                .CommandText = QryGonPODetail
                                .CommandType = CommandType.Text
                                If IsNothing(.Transaction) Then
                                    .Transaction = SqlTrans
                                End If
                                .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                                .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                                .Parameters.Add("@QUANTITY", SqlDbType.Decimal, 0, "QUANTITY")
                                .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100, "ModifiedBy")
                                .Parameters.Add("@STATUS", SqlDbType.VarChar, 50, "STATUS")
                                SqlDat.UpdateCommand = commandUpdate
                                SqlDat.Update(UpdatedRows) : SavePODetail = True
                                .Parameters.Clear()
                            End With
                            SqlDat.UpdateCommand = Nothing
                        End If
                        'check deletedRows
                        DeletedRows = DtPOdetail.Select("", "", DataViewRowState.Deleted)
                        If DeletedRows.Length > 0 Then
                            With commandDelete
                                .CommandText = qryDelGonPODetail
                                .CommandType = CommandType.Text
                                If IsNothing(.Transaction) Then
                                    .Transaction = SqlTrans
                                End If
                                .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                                .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                SqlDat.DeleteCommand = commandDelete
                                SqlDat.Update(DeletedRows) : SavePODetail = True
                                .Parameters.Clear()
                            End With
                            SqlDat.DeleteCommand = Nothing
                        End If
                    End If
                    If HasChangedGoneHeader Then
                        Dim res As Object = Nothing
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT 1 WHERE EXISTS(SELECT GON_NUMBER FROM GON_SEPARATED_HEADER WHERE GON_NUMBER = @GON_NO);"
                        With CommandSelect
                            .CommandText = Query
                            .CommandType = CommandType.Text
                            .Parameters.Add("@GON_NO", SqlDbType.VarChar, 50).Value = OGonHeader.GON_NO
                            If IsNothing(.Transaction) Then
                                .Transaction = SqlTrans
                            End If
                            res = .ExecuteScalar()
                            .Parameters.Clear()
                        End With
                        If Not IsNothing(res) And Not IsDBNull(res) Then ''data sudah ada
                            With commandUpdate
                                .CommandText = QryGONHeader
                                .CommandType = CommandType.Text
                                If IsNothing(.Transaction) Then
                                    .Transaction = SqlTrans
                                End If
                                .Parameters.Add("@IDApp", SqlDbType.Int, 0).Value = OGonHeader.IDApp
                                .Parameters.Add("@GON_NUMBER", SqlDbType.VarChar, 50).Value = OGonHeader.GON_NO
                                .Parameters.Add("@GON_DATE", SqlDbType.SmallDateTime, 0).Value = OGonHeader.GON_DATE
                                .Parameters.Add("@WARHOUSE", SqlDbType.VarChar, 20).Value = OGonHeader.WarhouseCode
                                .Parameters.Add("@SHIP_TO", SqlDbType.VarChar, 14).Value = IIf(OGonHeader.DistributorID <> "", OGonHeader.DistributorID, DBNull.Value)
                                .Parameters.Add("@SHIP_TO_CUST", SqlDbType.VarChar, 100).Value = OGonHeader.CustomerName
                                .Parameters.Add("@SHIP_TO_ADDRESS", SqlDbType.VarChar, 150).Value = OGonHeader.CustomerAddress
                                .Parameters.Add("@TRANSPORTER", SqlDbType.VarChar, 16).Value = IIf(OGonHeader.GT_ID <> "", OGonHeader.GT_ID, DBNull.Value)
                                .Parameters.Add("@POLICE_NO_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.PoliceNoTrans
                                .Parameters.Add("@DRIVER_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.DriverTrans
                                .Parameters.Add("@GON_AREA", SqlDbType.VarChar, 20).Value = IIf(OGonHeader.GON_ID_AREA <> "", OGonHeader.GON_ID_AREA, DBNull.Value)
                                .Parameters.Add("@GON_SHIP_TO", SqlDbType.VarChar, 250).Value = OGonHeader.ShipTo
                                .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100).Value = OGonHeader.ModifiedBy
                                .ExecuteScalar()
                                .Parameters.Clear()
                            End With
                        Else
                            With commandInsert
                                .CommandText = " SET NOCOUNT ON;" & vbCrLf & _
                                " INSERT INTO GON_SEPARATED_HEADER(FKApp,GON_NUMBER,GON_DATE,WARHOUSE,SHIP_TO,SHIP_TO_CUST,SHIP_TO_ADDRESS,TRANSPORTER,POLICE_NO_TRANS,DRIVER_TRANS,REMARK,GON_AREA,GON_SHIP_TO,CreatedDate,CreatedBy) " & vbCrLf & _
                                " VALUES (@FKApp,@GON_NUMBER,@GON_DATE,@WARHOUSE,@SHIP_TO,@SHIP_TO_CUST,@SHIP_TO_ADDRESS,@TRANSPORTER,@POLICE_NO_TRANS,@DRIVER_TRANS,'',@GON_AREA,@GON_SHIP_TO,CONVERT(VARCHAR(100),GETDATE(),101),@CreatedBy) ;" & vbCrLf & _
                                " SELECT @@IDENTITY;"
                                .CommandType = CommandType.Text
                                If IsNothing(.Transaction) Then
                                    .Transaction = SqlTrans
                                End If
                                .Parameters.Add("@FKApp", SqlDbType.Int, 0).Value = OSPPBHeader.IDApp
                                .Parameters.Add("@GON_NUMBER", SqlDbType.VarChar, 50).Value = OGonHeader.GON_NO
                                .Parameters.Add("@GON_DATE", SqlDbType.SmallDateTime, 0).Value = OGonHeader.GON_DATE
                                .Parameters.Add("@WARHOUSE", SqlDbType.VarChar, 20).Value = OGonHeader.WarhouseCode
                                .Parameters.Add("@SHIP_TO", SqlDbType.VarChar, 14).Value = IIf(OGonHeader.DistributorID <> "", OGonHeader.DistributorID, DBNull.Value)
                                .Parameters.Add("@SHIP_TO_CUST", SqlDbType.VarChar, 100).Value = OGonHeader.CustomerName
                                .Parameters.Add("@SHIP_TO_ADDRESS", SqlDbType.VarChar, 150).Value = OGonHeader.CustomerAddress
                                .Parameters.Add("@TRANSPORTER", SqlDbType.VarChar, 16).Value = IIf(OGonHeader.GT_ID <> "", OGonHeader.GT_ID, DBNull.Value)
                                .Parameters.Add("@POLICE_NO_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.PoliceNoTrans
                                .Parameters.Add("@DRIVER_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.DriverTrans
                                .Parameters.Add("@GON_AREA", SqlDbType.VarChar, 20).Value = IIf(OGonHeader.GON_ID_AREA <> "", OGonHeader.GON_ID_AREA, DBNull.Value)
                                .Parameters.Add("@GON_SHIP_TO", SqlDbType.VarChar, 250).Value = OGonHeader.ShipTo
                                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100).Value = OGonHeader.ModifiedBy
                                res = .ExecuteScalar()
                                .Parameters.Clear()
                                If Not IsNothing(res) And Not IsDBNull(res) Then
                                    OGonHeader.IDApp = CInt(res)
                                Else
                                    SqlTrans.Rollback()
                                    SqlTrans.Dispose()
                                    .Connection.Close()
                                    Throw New Exception("unknown error" & vbCrLf & "On possition of inserting GON_SEPARATED_DETAIL")
                                End If
                            End With
                        End If
                    End If
                    If HasChangedGONDetail Then
                        InsertedRows = dtGonDetail.Select("", "", DataViewRowState.Added)
                        If InsertedRows.Length > 0 Then
                            With commandInsert
                                Query = "SET NOCOUNT ON;" & vbCrLf & _
                                            "DECLARE @V_FKApp INT ;" & vbCrLf & _
                                            " IF @FKAppPODetail = 0 or @FKAppPODetail IS NULL " & vbCrLf & _
                                            " BEGIN SET @V_FKApp = (SELECT IDApp FROM ##T_PODetailIdentity_" & Me.ComputerName & " WHERE PO_NUMBER = @PO_NUMBER AND BRANDPACK_ID = @ITEM) ; END" & vbCrLf & _
                                            " ELSE " & vbCrLf & _
                                            " BEGIN SELECT @V_FKApp=@FKAppPODetail; END " & vbCrLf & _
                                            " INSERT INTO GON_SEPARATED_DETAIL(FKAppGonHeader,FKAppPODetail,ITEM,QTY,COLLY_BOX,COLLY_PACKSIZE,BATCH_NO,CreatedBy,CreatedDate) " & vbCrLf & _
                                            " VALUES(@FKAppGonHeader,@V_FKApp,@ITEM,@QTY,@COLLY_BOX,@COLLY_PACKSIZE,@BATCH_NO,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101)) ;"
                                .CommandText = Query
                                .CommandType = CommandType.Text
                                .Parameters.Add("@FKAppGonHeader", SqlDbType.Int, 0).Value = OGonHeader.IDApp
                                .Parameters.Add("@FKAppPODetail", SqlDbType.Int, 0, "FKAppPODetail")
                                .Parameters.Add("@ITEM", SqlDbType.VarChar, 14, "ITEM")
                                .Parameters.Add("@QTY", SqlDbType.Decimal, 0, "QTY")
                                .Parameters.Add("@COLLY_BOX", SqlDbType.VarChar, 50, "COLLY_BOX")
                                .Parameters.Add("@COLLY_PACKSIZE", SqlDbType.VarChar, 50, "COLLY_PACKSIZE")
                                .Parameters.Add("@BATCH_NO", SqlDbType.NVarChar, 50, "BATCH_NO")
                                .Parameters.Add("PO_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.PONumber
                                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = OGonHeader.ModifiedBy
                                If IsNothing(.Transaction) Then
                                    .Transaction = SqlTrans
                                End If
                            End With
                            SqlDat.InsertCommand = commandInsert
                            SqlDat.Update(InsertedRows) : SaveGonDetail = True
                            SqlDat.InsertCommand = Nothing
                        End If
                        ''prosedure inserted rows
                        UpdatedRows = dtGonDetail.Select("", "", DataViewRowState.ModifiedOriginal)
                        If UpdatedRows.Length > 0 Then
                            With commandUpdate
                                .CommandText = QryGonDetail
                                .CommandType = CommandType.Text
                                If IsNothing(.Transaction) Then
                                    .Transaction = SqlTrans
                                End If
                                .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                                .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                .Parameters.Add("@ITEM", SqlDbType.VarChar, 14, "ITEM")
                                .Parameters.Add("@QTY", SqlDbType.Decimal, 0, "QTY") '.Value = foundRows(0)("QTY")
                                .Parameters.Add("@COLLY_BOX", SqlDbType.VarChar, 50, "COLLY_BOX") '.Value = foundRows(0)("COLLY_BOX")
                                .Parameters.Add("@COLLY_PACKSIZE", SqlDbType.VarChar, 50, "COLLY_PACKSIZE") '.Value = foundRows(0)("COLLY_PACKSIZE")
                                .Parameters.Add("@BATCH_NO", SqlDbType.NVarChar, 50, "BATCh_NO") '.Value = foundRows(0)("BATCH_NO")
                                .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 50, "ModifiedBy") '.Value = OGonHeader.CreatedBy
                                SqlDat.UpdateCommand = commandUpdate
                                SqlDat.Update(UpdatedRows) : SaveGonDetail = True
                                .Parameters.Clear()
                            End With
                            SqlDat.UpdateCommand = Nothing
                        End If
                        DeletedRows = dtGonDetail.Select("", "", DataViewRowState.Deleted)
                        If DeletedRows.Length > 0 Then
                            With commandDelete
                                .CommandText = qryDelGonDetail
                                .CommandType = CommandType.Text
                                If IsNothing(.Transaction) Then
                                    .Transaction = SqlTrans
                                End If
                                .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                                .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                                SqlDat.DeleteCommand = commandDelete
                                SqlDat.Update(DeletedRows) : SaveGonDetail = True
                                .Parameters.Clear()
                            End With
                            SqlDat.DeleteCommand = Nothing
                        End If
                    End If
                End If
                'check yang di delete
                SqlTrans.Commit()
                If Not IsNothing(DtPOdetail) And HasChangedPOdetail Then
                    DtPOdetail.AcceptChanges()
                End If
                If Not IsNothing(dtGonDetail) And HasChangedGONDetail Then
                    dtGonDetail.AcceptChanges()
                End If
                If SavePODetail Then
                    QrySelectGonPODetail = "SET NOCOUNT ON;" & vbCrLf & _
                                            "SELECT GSPD.* FROM GON_SEPARATED_PO_DETAIL GSPD INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp " & vbCrLf & _
                                            " WHERE GSPH.PO_NUMBER = @PO_NUMBER;"
                    With CommandSelect
                        .CommandType = CommandType.Text
                        .CommandText = QrySelectGonPODetail
                        .Parameters.Add("@PO_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.PONumber
                        DtPOdetail.Clear()
                        SqlDat.SelectCommand = CommandSelect
                        SqlDat.Fill(DtPOdetail)
                        .Parameters.Clear()
                    End With
                End If
                If SaveGonDetail Then
                    QrySelectGonDetail = "SET NOCOUNT ON;" & vbCrLf & _
                                    " SELECT GSD.*,BP.BRANDPACK_NAME,QTY_UNIT = CONVERT(VARCHAR(100),QTY) + ISNULL(BPC.UNIT1,BP.UNIT) FROM GON_SEPARATED_DETAIL GSD " & vbCrLf & _
                                    " INNER JOIN GON_SEPARATED_HEADER GSH ON GSH.IDApp = GSD.FKAppGonHeader LEFT OUTER JOIN BRND_PROD_CONV BPC ON BPC.BRANDPACK_ID = GSD.ITEM " & vbCrLf & _
                                    " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = GSD.ITEM WHERE GSH.GON_NUMBER = @GON_NUMBER;"
                    'QrySelectGonDetail = "SELECT GSD.* FROM GON_SEPARATED_DETAIL GSD INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSD.FKAppPODetail = GSPD.IDApp " & vbCrLf & _
                    '                " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp " & vbCrLf & _
                    '                " INNER JOIN GON_SEPARATED_HEADER GSH ON GSH.IDApp = GSD.FKAppGonHeader AND GSH.FKApp = GSPH.IDApp " & vbCrLf & _
                    '                " WHERE GSPH.PO_NUMBER = @PO_NUMBER AND GSH.IDApp = @IDApp;"
                    With CommandSelect
                        .CommandType = CommandType.Text
                        .CommandText = QrySelectGonDetail
                        '.Parameters.Add("@PO_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.PONumber
                        .Parameters.Add("@GON_NUMBER", SqlDbType.VarChar, 50).Value = OGonHeader.GON_NO
                        dtGonDetail.Clear()
                        SqlDat.SelectCommand = CommandSelect
                        SqlDat.Fill(dtGonDetail)
                        .Parameters.Clear()
                    End With
                End If
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Return True
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                'If HasChangedGoneHeader Then
                '    OGonHeader = New Nufarm.Domain.GONHeader()
                'End If
                'If HasChangedpoHeader Then
                '    OSPPBHeader = New Nufarm.Domain.SPPBHeader()
                'End If
                If Not IsNothing(CommandSelect) Then
                    CommandSelect.Parameters.Clear()
                    CommandSelect.Dispose()
                    CommandSelect = Nothing
                End If
                If Not IsNothing(commandInsert) Then
                    commandInsert.Parameters.Clear()
                    commandInsert.Dispose()
                    CommandSelect = Nothing
                End If
                If Not IsNothing(commandUpdate) Then
                    commandUpdate.Parameters.Clear()
                    commandUpdate.Dispose()
                    commandUpdate = Nothing
                End If
                If Not IsNothing(commandDelete) Then
                    commandDelete.Parameters.Clear()
                    commandDelete.Dispose()
                    commandDelete = Nothing
                End If
                System.Windows.Forms.MessageBox.Show(ex.Message, "Unhandled system exception", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' buat di bind di grid PO detail
        ''' </summary>
        ''' <param name="PORefNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getGonPODetail(ByVal PORefNo As String, ByVal closeConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                          "SELECT * FROM GON_SEPARATED_PO_DETAIL WHERE FKApp = (SELECT TOP 1 IDApp FROM GON_SEPARATED_PO_HEADER WHERE PO_NUMBER = @PO_NUMBER);"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_NUMBER", SqlDbType.NVarChar, PORefNo)
                Dim dtGonPOdetail As New DataTable("T_GonPODetail")
                Me.OpenConnection()
                dtGonPOdetail.Clear() : setDataAdapter(Me.SqlCom).Fill(dtGonPOdetail)
                dtGonPOdetail.Columns("STATUS").DefaultValue = "Pending"
                Me.ClearCommandParameters()
                If closeConnection Then : Me.CloseConnection() : End If
                Return dtGonPOdetail
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getTotalGonAllBrandPack(ByVal PORefNo As String, ByVal CloseConnection As Boolean) As DataView
            Try
                Query = "SET NOCOUNT ON " & vbCrLf & _
                " SELECT GSPD.BRANDPACK_ID,ISNULL(SUM(GSD.QTY),0)AS TOTAL_GON FROM GON_SEPARATED_PO_DETAIL GSPD " & vbCrLf & _
                " INNER JOIN GON_SEPARATED_DETAIL GSD ON GSD.FKAppPODetail = GSPD.IDApp INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp " & vbCrLf & _
                " WHERE GSPH.PO_NUMBER = @PO_NUMBER GROUP BY GSPD.BRANDPACK_ID;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_NUMBER", SqlDbType.NVarChar, PORefNo)
                Dim dtGonPOdetail As New DataTable("T_GonPODetail")
                Me.OpenConnection()
                dtGonPOdetail.Clear() : setDataAdapter(Me.SqlCom).Fill(dtGonPOdetail)
                Me.ClearCommandParameters()
                If CloseConnection Then : Me.CloseConnection() : End If
                Return dtGonPOdetail.DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getGOnDetail(ByVal GON_NO As String, ByVal closeConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT GSD.*,BP.BRANDPACK_NAME,QTY_UNIT = CONVERT(VARCHAR(100),QTY) + ISNULL(BPC.UNIT1,BP.UNIT) FROM GON_SEPARATED_DETAIL GSD " & vbCrLf & _
                " INNER JOIN GON_SEPARATED_HEADER GSH ON GSH.IDApp = GSD.FKAppGonHeader LEFT OUTER JOIN BRND_PROD_CONV BPC ON BPC.BRANDPACK_ID = GSD.ITEM " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = GSD.ITEM WHERE GSH.GON_NUMBER = @GON_NUMBER;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@GON_NUMBER", SqlDbType.NVarChar, GON_NO)
                Dim dtGondetail As New DataTable("T_GonDetail")
                Me.OpenConnection()
                dtGondetail.Clear() : setDataAdapter(Me.SqlCom).Fill(dtGondetail)
                dtGondetail.Columns("FKAppPODetail").DefaultValue = DBNull.Value
                dtGondetail.AcceptChanges()
                Me.ClearCommandParameters()
                If closeConnection Then : Me.CloseConnection() : End If
                Return dtGondetail
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getGonQty(ByVal IDApp As Integer, ByVal mustCloseConnection As Boolean) As Decimal
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                "SELECT QTY FROM GON_SEPARATED_DETAIL WHERE IDApp = @IDApp;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@IDApp", SqlDbType.NVarChar, IDApp)
                Me.OpenConnection()
                Dim result = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If Not IsNothing(result) And Not IsDBNull(result) Then
                    Return Convert.ToDecimal(result)
                End If
                If mustCloseConnection Then : Me.CloseConnection() : Me.ClearCommandParameters() : End If
                Return 0
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getLastShipTO(ByVal SPPB_NO As String, ByRef GON_SHIP_TO As String, ByRef ShipToAddress As String, ByVal mustCloseConnection As Boolean) As String
            Try
                'If Not String.IsNullOrEmpty(GON_SHIP_TO) Then
                '    Query = "SET NOCOUNT ON; " & vbCrLf & _
                '            "SELECT TOP 1 GSH.GON_SHIP_TO FROM GON_SEPARATED_HEADER GSH INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSH.FKApp = GSPH.IDApp " & vbCrLf & _
                '            " WHERE GSPH.SPPB_NUMBER = @SPPB_NO ORDER BY GSH.GON_DATE DESC ; "
                '    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                '    Else : Me.ResetCommandText(CommandType.Text, Query)
                '    End If
                '    Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 30)
                '    Me.OpenConnection()
                '    Dim retval As Object = Me.SqlCom.ExecuteScalar()
                '    If Not IsNothing(retval) And Not IsDBNull(retval) Then
                '        Return retval.ToString()
                '    End If
                '    Return ""

                'ElseIf Not String.IsNullOrEmpty(ShipToAddress) Then
                '    Query = "SET NOCOUNT ON; " & vbCrLf & _
                '            "SELECT TOP 1 GSH.SHIP_TO_ADDRESS FROM GON_SEPARATED_HEADER GSH INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSH.FKApp = GSPH.IDApp " & vbCrLf & _
                '            " WHERE GSPH.SPPB_NUMBER = @SPPB_NO ORDER BY GSH.GON_DATE DESC ; "
                '    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                '    Else : Me.ResetCommandText(CommandType.Text, Query)
                '    End If
                '    Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 30)
                '    Me.OpenConnection()
                '    Dim retval As Object = Me.SqlCom.ExecuteScalar()
                '    If Not IsNothing(retval) And Not IsDBNull(retval) Then
                '        Return retval.ToString()
                '    End If
                '    Return ""
                'Else

                'End If
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT TOP 1 GSH.SHIP_TO_ADDRESS,GSH.GON_SHIP_TO FROM GON_SEPARATED_HEADER GSH INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSH.FKApp = GSPH.IDApp " & vbCrLf & _
                        " WHERE GSPH.SPPB_NUMBER = @SPPB_NO ORDER BY GSH.GON_DATE DESC ; "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 30)
                Me.OpenConnection()
                Dim ShipToAdd = Nothing, GonShipTo As Object = Nothing
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    ShipToAdd = SqlRe(0)
                    If Not IsDBNull(ShipToAdd) And Not IsNothing(ShipToAdd) Then
                        If Not String.IsNullOrEmpty(ShipToAdd) Then
                            ShipToAddress = ShipToAdd
                        End If
                    End If
                    GonShipTo = SqlRe(1)
                    If Not IsDBNull(GonShipTo) And Not IsNothing(GonShipTo) Then
                        If Not String.IsNullOrEmpty(GonShipTo) Then
                            GON_SHIP_TO = GonShipTo
                        End If
                    End If
                End While : Me.SqlRe.Close()
                Me.ClearCommandParameters()
                If ShipToAdd = "" And GonShipTo = "" Then
                    Return ""
                End If
                Return IIf(ShipToAdd <> "", ShipToAdd, GON_SHIP_TO)
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function GetDistributor(ByVal SearchString As String, ByVal closeConnection As Boolean) As DataView
            Try
                If (String.IsNullOrEmpty(SearchString)) Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                           " SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME,ISNULL(ADDRESS,'UNKNOW ADDRESS')AS ADDRESS FROM DIST_DISTRIBUTOR "
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                           " SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,ISNULL(DR.ADDRESS,'UNKNOW ADDRESS')AS ADDRESS FROM DIST_DISTRIBUTOR DR " & vbCrLf & _
                           " WHERE DR.DISTRIBUTOR_NAME LIKE '%" & SearchString & "%';"
                End If

                Dim tblDist As New DataTable("T_Distributor") : tblDist.Clear()
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom : End If : Me.OpenConnection()
                SqlDat.Fill(tblDist) : Me.ClearCommandParameters() : If closeConnection Then : Me.CloseConnection() : End If : Return tblDist.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace

