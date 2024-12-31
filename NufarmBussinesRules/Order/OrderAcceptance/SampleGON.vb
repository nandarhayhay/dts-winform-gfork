Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Nufarm.Domain
Imports NufarmBussinesRules.common
Imports NufarmDataAccesLayer.DataAccesLayer
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Namespace OrderAcceptance

    Public Class SampleGON
        Inherits ADODotNet

        Protected Query As String

        Public GonMaster As SPPBEntryGON

        Public Sub New()
            MyBase.New()
            Me.Query = ""
            Me.GonMaster = Nothing
            Me.GonMaster = New SPPBEntryGON
        End Sub

        Public Function GetDistributor(ByVal SearchString As String, ByVal closeConnection As Boolean) As DataView
            Dim defaultView As DataView
            Try
                If Not String.IsNullOrEmpty(SearchString) Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,ISNULL(DR.ADDRESS,'UNKNOW ADDRESS')AS " & vbCrLf & _
                            " ADDRESS FROM DIST_DISTRIBUTOR DR " & vbCrLf & _
                            " WHERE DR.DISTRIBUTOR_NAME LIKE '%" & SearchString & "%';"
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME,ISNULL(ADDRESS,'UNKNOW ADDRESS')AS ADDRESS FROM DIST_DISTRIBUTOR "
                End If

                Dim dataTable As DataTable = New DataTable("T_Distributor")
                dataTable.Clear()
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                If Not Information.IsNothing(Me.SqlDat) Then
                    Me.SqlDat.SelectCommand = Me.SqlCom
                Else
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                End If

                Me.OpenConnection()
                Me.SqlDat.Fill(dataTable)
                Me.ClearCommandParameters()
                If closeConnection Then
                    Me.CloseConnection()
                End If

                defaultView = dataTable.DefaultView
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try

            Return defaultView
        End Function

        Public Sub getFormData(ByVal GON_Number As String, ByRef OSPPBHeader As SPPBHeader, ByRef OGONHeader As GONHeader, ByVal mustCloseConnection As Boolean)
            Try
                Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                           " SELECT * FROM GON_SEPARATED_HEADER WHERE GON_NUMBER = @GON_NUMBER ;"
                Me.OpenConnection()
                If Not Information.IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If

                Me.AddParameter("@GON_NUMBER", SqlDbType.VarChar, GON_Number, 100)
                Me.SqlRe = Me.SqlCom.ExecuteReader

                While Me.SqlRe.Read
                    OGONHeader.CustomerAddress = Conversions.ToString(Me.SqlRe("SHIP_TO_ADDRESS"))
                    Dim objectValue As Object = Nothing
                    Dim obj As Object = Nothing
                    Dim objectValue1 As Object = Nothing
                    Dim obj1 As Object = Nothing
                    objectValue = RuntimeHelpers.GetObjectValue(Me.SqlRe("SHIP_TO_CUST"))
                    obj = RuntimeHelpers.GetObjectValue(Me.SqlRe("SHIP_TO"))
                    obj1 = RuntimeHelpers.GetObjectValue(Me.SqlRe("GON_AREA"))
                    objectValue1 = RuntimeHelpers.GetObjectValue(Me.SqlRe("TRANSPORTER"))
                    oGONHeader.CustomerName = Conversions.ToString(Interaction.IIf((Information.IsNothing(RuntimeHelpers.GetObjectValue(objectValue)) Or Information.IsDBNull(RuntimeHelpers.GetObjectValue(objectValue))), "", objectValue.ToString))
                    oGONHeader.DistributorID = Conversions.ToString(Interaction.IIf((Information.IsNothing(RuntimeHelpers.GetObjectValue(obj)) Or Information.IsDBNull(RuntimeHelpers.GetObjectValue(obj))), "", obj.ToString))
                    oGONHeader.DriverTrans = Conversions.ToString(Me.SqlRe("DRIVER_TRANS"))
                    oGONHeader.GON_DATE = RuntimeHelpers.GetObjectValue(Me.SqlRe("GON_DATE"))
                    oGONHeader.GON_ID_AREA = Conversions.ToString(Interaction.IIf((Information.IsNothing(RuntimeHelpers.GetObjectValue(obj1)) Or Information.IsDBNull(RuntimeHelpers.GetObjectValue(obj1))), "", obj1.ToString))
                    oGONHeader.GON_NO = GON_Number
                    oGONHeader.GT_ID = Conversions.ToString(Interaction.IIf((Information.IsNothing(RuntimeHelpers.GetObjectValue(objectValue1)) Or Information.IsDBNull(RuntimeHelpers.GetObjectValue(objectValue1))), "", objectValue1.ToString))
                    oGONHeader.IDApp = Conversions.ToInteger(Me.SqlRe("IDApp"))
                    oGONHeader.PoliceNoTrans = Conversions.ToString(Me.SqlRe("POLICE_NO_TRANS"))
                    oGONHeader.WarhouseCode = Conversions.ToString(Me.SqlRe("WARHOUSE"))
                    oGONHeader.FkApp = Conversions.ToInteger(Me.SqlRe("FKApp"))
                    oGONHeader.CreatedBy = Conversions.ToString(Me.SqlRe("CreatedBy"))
                    oGONHeader.CreatedDate = RuntimeHelpers.GetObjectValue(Me.SqlRe("CreatedDate"))
                    oGONHeader.ShipTo = Conversions.ToString(Me.SqlRe("GON_SHIP_TO"))
                End While

                Me.SqlRe.Close()
                Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT GSPH.* FROM GON_SEPARATED_PO_HEADER GSPH INNER JOIN GON_SEPARATED_HEADER GSH ON GSPH.IDApp = GSH.FKApp WHERE GSH.GON_NUMBER = @GON_NUMBER ;"
                Me.ResetCommandText(CommandType.Text, Me.Query)
                Me.SqlRe = Me.SqlCom.ExecuteReader

                While Me.SqlRe.Read
                    OSPPBHeader.IDApp = Conversions.ToInteger(Me.SqlRe("IDApp"))
                    oSPPBHeader.PONumber = Conversions.ToString(Me.SqlRe("PO_NUMBER"))
                    oSPPBHeader.PODate = RuntimeHelpers.GetObjectValue(Me.SqlRe("PO_DATE"))
                    oSPPBHeader.SPPBNO = Conversions.ToString(Me.SqlRe("SPPB_NUMBER"))
                    oSPPBHeader.SPPBDate = RuntimeHelpers.GetObjectValue(Me.SqlRe("SPPB_DATE"))
                    oSPPBHeader.SPPBShipTo = Conversions.ToString(Me.SqlRe("SHIP_TO"))
                    oSPPBHeader.CreatedBy = Conversions.ToString(Me.SqlRe("CreatedBy"))
                    oSPPBHeader.CreatedDate = RuntimeHelpers.GetObjectValue(Me.SqlRe("CreatedDate"))
                End While
                Me.SqlRe.Close()
                Me.ClearCommandParameters()
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If

            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                If Not Information.IsNothing(Me.SqlRe) Then
                    Me.SqlRe.Close()
                    Me.SqlRe = Nothing
                End If

                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try

        End Sub

        Public Function getGOnDetail(ByVal GON_NO As String, ByVal closeConnection As Boolean) As DataTable
            Dim dataTable As DataTable
            Try
                Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT GSD.*,SAMPLE_PRODUCT = BPO.ITEM,QTY_UNIT = CONVERT(VARCHAR(100),QTY) + ISNULL(BPO.UnitOfMeasure,'') FROM GON_SEPARATED_DETAIL GSD " & vbCrLf & _
                " INNER JOIN GON_SEPARATED_HEADER GSH ON GSH.IDApp = GSD.FKAppGonHeader INNER JOIN BRND_PROD_OTHER BPO ON BPO.IDApp=GSD.ITEM_OTHER " & vbCrLf & _
                " WHERE GSH.GON_NUMBER = @GON_NUMBER AND BPO.INACTIVE =0;"
                If Not Information.IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If

                Me.AddParameter("@GON_NUMBER", SqlDbType.NVarChar, GON_NO)
                Dim value As DataTable = New DataTable("T_GonDetail")
                Me.OpenConnection()
                value.Clear()
                Me.setDataAdapter(Me.SqlCom).Fill(value)
                value.Columns("FKAppPODetail").DefaultValue = DBNull.Value
                value.AcceptChanges()
                Me.ClearCommandParameters()
                If closeConnection Then
                    Me.CloseConnection()
                End If

                dataTable = value
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try

            Return dataTable
        End Function

        Public Function getGonPODetail(ByVal PORefNo As String, ByVal closeConnection As Boolean) As DataTable
            Dim dataTable As DataTable
            Try
                Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                "SELECT * FROM GON_SEPARATED_PO_DETAIL WHERE FKApp = (SELECT TOP 1 IDApp FROM GON_SEPARATED_PO_HEADER WHERE PO_NUMBER = @PO_NUMBER);"
                If Not Information.IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If

                Me.AddParameter("@PO_NUMBER", SqlDbType.NVarChar, PORefNo)
                Dim dataTable1 As DataTable = New DataTable("T_GonPODetail")
                Me.OpenConnection()
                dataTable1.Clear()
                Me.setDataAdapter(Me.SqlCom).Fill(dataTable1)
                dataTable1.Columns("STATUS").DefaultValue = "Pending"
                Me.ClearCommandParameters()
                If closeConnection Then
                    Me.CloseConnection()
                End If

                dataTable = dataTable1
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try

            Return dataTable
        End Function

        Public Function getGonQty(ByVal IDApp As Integer, ByVal mustCloseConnection As Boolean) As Decimal
            Dim zero As Decimal
            Try
                Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                           "SELECT QTY FROM GON_SEPARATED_DETAIL WHERE IDApp = @IDApp;"
                If Not Information.IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If

                Me.AddParameter("@IDApp", SqlDbType.NVarChar, IDApp)
                Me.OpenConnection()
                Dim objectValue As Object = RuntimeHelpers.GetObjectValue(Me.SqlCom.ExecuteScalar)
                Me.ClearCommandParameters()
                If Not (Not Information.IsNothing(RuntimeHelpers.GetObjectValue(objectValue)) _
                            And Not Information.IsDBNull(RuntimeHelpers.GetObjectValue(objectValue))) Then
                    If mustCloseConnection Then
                        Me.CloseConnection()
                        Me.ClearCommandParameters()
                    End If

                    zero = Decimal.Zero
                Else
                    zero = Convert.ToDecimal(RuntimeHelpers.GetObjectValue(objectValue))
                End If

            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try

            Return zero
        End Function

        Public Function getLastShipTO(ByVal SPPB_NO As String, ByRef GON_SHIP_TO As String, ByRef ShipToAddress As String, ByVal mustCloseConnection As Boolean) As String
            Dim str As String
            Try
                Me.Query = "SET NOCOUNT ON; " & vbCrLf & _
                "SELECT TOP 1 GSH.SHIP_TO_ADDRESS,GSH.GON_SHIP_TO FROM GON_SEPARATED_HEADER GSH INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSH.FKApp = GSPH.IDApp " & vbCrLf & _
                " WHERE GSPH.SPPB_NUMBER = @SPPB_NO ORDER BY GSH.GON_DATE DESC ; "
                If Not Information.IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If

                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 30)
                Me.OpenConnection()
                Dim objectValue As Object = Nothing
                Dim obj As Object = Nothing
                Me.SqlRe = Me.SqlCom.ExecuteReader

                While Me.SqlRe.Read
                    objectValue = RuntimeHelpers.GetObjectValue(Me.SqlRe(0))
                    If ((Not Information.IsDBNull(RuntimeHelpers.GetObjectValue(objectValue)) _
                                And Not Information.IsNothing(RuntimeHelpers.GetObjectValue(objectValue))) _
                                AndAlso Not String.IsNullOrEmpty(Conversions.ToString(objectValue))) Then
                        ShipToAddress = Conversions.ToString(objectValue)
                    End If

                    obj = RuntimeHelpers.GetObjectValue(Me.SqlRe(1))
                    If (Not (Not Information.IsDBNull(RuntimeHelpers.GetObjectValue(obj)) _
                                And Not Information.IsNothing(RuntimeHelpers.GetObjectValue(obj))) _
                                OrElse String.IsNullOrEmpty(Conversions.ToString(obj))) Then
                        'TODO: Warning!!! continue If
                    End If

                    GON_SHIP_TO = Conversions.ToString(obj)

                End While

                Me.SqlRe.Close()
                Me.ClearCommandParameters()
                str = Conversions.ToString(Interaction.IIf(Operators.ConditionalCompareObjectNotEqual(objectValue, "", False), RuntimeHelpers.GetObjectValue(objectValue), GON_SHIP_TO))
                'TODO: Warning!!!, inline IF is not supported ?
                'Not Conversions.ToBoolean(Operators.AndObject(Operators.CompareObjectEqual(objectValue, "", false), Operators.CompareObjectEqual(obj, "", false)))
                '""
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try

            Return str
        End Function

        Public Function getLeftQty(ByVal PORefNo As String, ByVal IDAppItemOther As Integer, ByVal mustCloseConnection As Boolean) As Decimal

            Try
                Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT GSPD.QUANTITY - ISNULL(IGSD.QTY,0) AS LEFT_QTY,EXIST = IGSD.QTY FROM GON_SEPARATED_PO_DETAIL GSPD " & vbCrLf & _
                            " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp " & vbCrLf & _
                            " INNER JOIN(SELECT FKAppPODetail,SUM(QTY) AS QTY FROM GON_SEPARATED_DETAIL WHERE ITEM_OTHER IS NOT NULL GROUP BY FKAppPODetail)IGSD ON IGSD.FKAppPODetail = GSPD.IDApp WHERE GSPH.PO_NUMBER = @PO_NUMBER " & vbCrLf & _
                            " AND GSPD.ITEM_OTHER = @IDAppItemOther ;"
                If Not Information.IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If

                Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PORefNo)
                Me.AddParameter("@IDAppItemOther", SqlDbType.VarChar, IDAppItemOther)
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
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
        End Function

        Public Sub getPOData(ByVal PONumber As String, ByRef OSPPBHeader As SPPBHeader, ByVal mustCloseConnection As Boolean)
            Try
                Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT * FROM GON_SEPARATED_PO_HEADER WHERE PO_NUMBER = @PO_NUMBER;"
                Me.OpenConnection()
                If Not Information.IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If

                Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PONumber, 100)
                Me.SqlRe = Me.SqlCom.ExecuteReader

                While Me.SqlRe.Read
                    OSPPBHeader.CreatedBy = Conversions.ToString(Me.SqlRe("CreatedBy"))
                    oSPPBHeader.CreatedDate = RuntimeHelpers.GetObjectValue(Me.SqlRe("CreatedDate"))
                    oSPPBHeader.IDApp = Conversions.ToInteger(Me.SqlRe("IDApp"))
                    oSPPBHeader.PODate = RuntimeHelpers.GetObjectValue(Me.SqlRe("PO_DATE"))
                    oSPPBHeader.PONumber = Conversions.ToString(Me.SqlRe("PO_NUMBER"))
                    oSPPBHeader.SPPBDate = RuntimeHelpers.GetObjectValue(Me.SqlRe("SPPB_DATE"))
                    oSPPBHeader.SPPBNO = Conversions.ToString(Me.SqlRe("SPPB_NUMBER"))
                    oSPPBHeader.SPPBShipTo = Conversions.ToString(Me.SqlRe("SHIP_TO"))
                End While

                Me.SqlRe.Close()
                Me.ClearCommandParameters()
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If

            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                If Not Information.IsNothing(Me.SqlRe) Then
                    Me.SqlRe.Close()
                    Me.SqlRe = Nothing
                End If

                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try

        End Sub

        Public Function getPODetail(ByVal PoRefNO As String, ByVal CloseConnection As Boolean) As DataView
            Dim defaultView As DataView
            Try
                Me.Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT GSPD.ITEM_OTHER,BPO.ITEM,GSPD.QUANTITY - ISNULL(IGSD.QTY,0) AS LEFT_QTY FROM GON_SEPARATED_PO_DETAIL GSPD " & vbCrLf & _
                " INNER JOIN BRND_PROD_OTHER BPO ON BPO.IDApp = GSPD.ITEM_OTHER " & vbCrLf & _
                " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp " & vbCrLf & _
                " LEFT OUTER JOIN(SELECT FKAppPODetail,SUM(QTY) AS QTY FROM GON_SEPARATED_DETAIL WHERE ITEM_OTHER IS NOT NULL GROUP BY FKAppPODetail)" & _
                "IGSD ON IGSD.FKAppPODetail = GSPD.IDApp WHERE GSPH.PO_NUMBER = @PO_NUMBER ;"
                If Not Information.IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If

                Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PoRefNO)
                Dim dataTable As DataTable = New DataTable("T_PODetail")
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(dataTable)
                Me.ClearCommandParameters()
                If CloseConnection Then
                    Me.CloseConnection()
                End If

                defaultView = dataTable.DefaultView
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try

            Return defaultView
        End Function

        Public Function getSampleProduct(ByVal closeConnection As Boolean) As DataView
            Dim defaultView As DataView
            Try
                Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                "SELECT IDApp,ITEM,UNIT1,VOL1,UNIT2,VOL2,UnitOfMeasure,DEVIDED_QUANTITY FROM BRND_PROD_OTHER WHERE INActive = 0;"
                Dim dataTable As DataTable = New DataTable("T_BrandPack")
                Me.OpenConnection()
                If Not Information.IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else
                    Me.CreateCommandSql("sp_executesql", "")
                End If

                Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                dataTable.Clear()
                Me.setDataAdapter(Me.SqlCom).Fill(dataTable)
                Me.ClearCommandParameters()
                If closeConnection Then
                    Me.CloseConnection()
                End If

                defaultView = dataTable.DefaultView
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try

            Return defaultView
        End Function

        Public Overloads Function getTotalGon(ByVal PORefNo As String, ByVal IDAppItemOther As Integer, ByVal IDpp As Object, ByVal closeConnection As Boolean) As Decimal
            Try
                Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT ISNULL(SUM(GSD.QTY),0) AS TOTAL FROM GON_SEPARATED_DETAIL GSD INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSD.FKAppPODetail = GSPD.IDApp " & vbCrLf & _
                " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp WHERE GSPH.PO_NUMBER = @PO_NUMBER AND GSPD.ITEM_OTHER = @ITEM_OTHER AND GSD.IDApp != @IDApp;"
                If Not Information.IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If

                Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PORefNo)
                Me.AddParameter("@ITEM_OTHER", SqlDbType.Int, IDAppItemOther)
                Me.AddParameter("@IDApp", SqlDbType.Int, RuntimeHelpers.GetObjectValue(IDpp))
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If closeConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return Convert.ToDecimal(retval)
                End If
                Return 0
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try
        End Function

        Public Overloads Function getTotalGon(ByVal PORefNo As String, ByVal ItemOther As Integer, ByVal closeConnection As Boolean) As Decimal
            Dim num As Decimal
            Try
                Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT ISNULL(SUM(GSD.QTY),0) AS TOTAL FROM GON_SEPARATED_DETAIL GSD INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSD.FKAppPODetail = GSPD.IDApp " & vbCrLf & _
                " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp WHERE GSPH.PO_NUMBER = @PO_NUMBER  AND GSPD.ITEM_OTHER = @ItemOther;"
                If Not Information.IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.Text, Me.Query)
                Else
                    Me.CreateCommandSql("", Me.Query)
                End If

                Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PORefNo)
                Me.AddParameter("@ItemOther", SqlDbType.Int, ItemOther)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If closeConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return Convert.ToDecimal(retval)
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try

            Return num
        End Function

        Public Function getTotalGons(ByVal PORefNo As String, ByVal closeConnection As Boolean, Optional ByVal GON_NO As String = "") As DataTable
            Dim dataTable As DataTable
            Try
                If String.IsNullOrEmpty(GON_NO) Then
                    Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT GSPD.ITEM_OTHER, ISNULL(SUM(GSD.QTY),0) AS LEFT_QTY FROM GON_SEPARATED_DETAIL GSD INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSD.FKAppPODetail = GSPD.IDApp " & vbCrLf & _
                    " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp WHERE GSPH.PO_NUMBER = @PO_NUMBER GROUP BY GSPD.ITEM_OTHER;"
                    If Not Information.IsNothing(Me.SqlCom) Then
                        Me.ResetCommandText(CommandType.Text, Me.Query)
                    Else
                        Me.CreateCommandSql("", Me.Query)
                    End If

                    Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PORefNo)
                Else
                    Me.Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT GSPD.ITEM_OTHER, ISNULL(SUM(GSD.QTY),0) AS LEFT_QTY FROM GON_SEPARATED_DETAIL GSD INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSD.FKAppPODetail = GSPD.IDApp " & vbCrLf & _
                    " INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp INNER JOIN GON_SEPARATED_HEADER GSH ON GSH.FKApp = GSPH.IDApp AND GSH.IDApp = GSD.FKAppGonHeader WHERE GSPH.PO_NUMBER = @PO_NUMBER AND GSH.GON_NUMBER != @GON_NO GROUP BY GSPD.ITEM_OTHER;"
                    If Not Information.IsNothing(Me.SqlCom) Then
                        Me.ResetCommandText(CommandType.Text, Me.Query)
                    Else
                        Me.CreateCommandSql("", Me.Query)
                    End If

                    Me.AddParameter("@PO_NUMBER", SqlDbType.VarChar, PORefNo)
                    Me.AddParameter("@GON_NO", SqlDbType.VarChar, GON_NO)
                End If

                Me.OpenConnection()
                Dim dataTable1 As DataTable = New DataTable("T_GON")
                Me.setDataAdapter(Me.SqlCom).Fill(dataTable1)
                Me.ClearCommandParameters()
                If closeConnection Then
                    Me.CloseConnection()
                End If

                dataTable = dataTable1
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw exception
            End Try

            Return dataTable
        End Function

        Public Function SaveData(ByVal Mode As Helper.SaveMode, ByVal HasChangedpoHeader As Boolean, ByVal HasChangedPOdetail As Boolean, ByVal HasChangedGoneHeader As Boolean, ByVal HasChangedGONDetail As Boolean, ByRef OGonHeader As GONHeader, ByRef OSPPBHeader As SPPBHeader, ByRef DtPOdetail As DataTable, ByRef dtGonDetail As DataTable) As Boolean
            Dim flag As Boolean
            Me.OpenConnection()
            Dim commandInsert As SqlCommand = Me.SqlConn.CreateCommand(), commandUpdate As SqlCommand = Me.SqlConn.CreateCommand(), _
            commandDelete As SqlCommand = Me.SqlConn.CreateCommand(), CommandSelect As SqlCommand = SqlConn.CreateCommand()
            SqlCom.Connection = Me.SqlConn
            Try
                Dim str As String = ""
                Dim str1 As String = ""
                Dim str2 As String = ""
                Dim str3 As String = ""
                Me.SqlDat = New SqlDataAdapter
                Dim dataRowArray() As DataRow = Nothing
                Dim dataRowArray1() As DataRow = Nothing
                Dim dataRowArray2() As DataRow = Nothing
                Dim str4 As String = ""
                Dim str5 As String = ""
                Dim flag1 As Boolean = False
                Dim flag2 As Boolean = False
                Me.SqlTrans = Me.SqlConn.BeginTransaction
                Me.SqlCom.Transaction = Me.SqlTrans
                CommandSelect.Transaction = Me.SqlTrans
                If ((Mode = Helper.SaveMode.Update) _
                            And HasChangedPOdetail) Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_PODetailIdentity_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                    " CREATE TABLE ##T_PODetailIdentity_" & Me.ComputerName & " ( " & vbCrLf & _
                    " [PO_NUMBER] [varchar] (50),[ITEM_OTHER] [INT],[IDApp][INT] )"
                    If Not Information.IsNothing(Me.SqlCom) Then
                        Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Else
                        Me.CreateCommandSql("sp_executesql", "")
                    End If
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Me.Query)
                    Me.SqlCom.ExecuteScalar()
                    Me.ClearCommandParameters()
                End If
                Dim str6 As String = "SET NOCOUNT ON;" & vbCrLf & _
                "IF EXISTS(SELECT * FROM GON_SEPARATED_DETAIL WHERE FKAppPODetail = @IDApp) " & vbCrLf & _
                " BEGIN " & vbCrLf & _
                "DELETE FROM GON_SEPARATED_DETAIL WHERE FKAppPODetail = @IDApp ; END " & vbCrLf & _
                " DELETE FROM GON_SEPARATED_PO_DETAIL WHERE IDApp = @IDApp ;"
                Dim str7 As String = "SET NOCOUNT ON;" & vbCrLf & _
                " IF EXISTS(SELECT IDApp FROM GON_SEPARATED_DETAIL WHERE IDApp = @IDApp) " & vbCrLf & _
                " BEGIN DELETE FROM GON_SEPARATED_DETAIL WHERE IDApp = @IDApp ; END"
                Select Case (Mode)
                    Case Helper.SaveMode.Insert
                        str = "SET NOCOUNT ON;" & vbCrLf & _
                        "DECLARE @V_MESSAGE VARCHAR(200);" & vbCrLf & _
                        " SET @V_MESSAGE = CONCAT('PO NUMBER ' , @PO_NUMBER, ' Has already existed');" & vbCrLf & _
                        " IF EXISTS(SELECT PO_NUMBER FROM GON_SEPARATED_PO_HEADER WHERE PO_NUMBER = @PO_NUMBER) " & vbCrLf & _
                        " BEGIN RAISERROR(@V_MESSAGE,16,1);RETURN; END" & vbCrLf & _
                        " SET @V_MESSAGE = CONCAT('SPPB_NUMBER ' , @SPPB_NUMBER , ' Has already existed');" & vbCrLf & _
                        " IF EXISTS(SELECT SPPB_NUMBER FROM GON_SEPARATED_PO_HEADER WHERE SPPB_NUMBER = @SPPB_NUMBER) " & vbCrLf & _
                        " BEGIN RAISERROR(@V_MESSAGE,16,1);RETURN; END" & vbCrLf & _
                        " INSERT INTO GON_SEPARATED_PO_HEADER(PO_NUMBER,PO_DATE,SPPB_NUMBER,SPPB_DATE,SHIP_TO,CreatedBy,CreatedDate) " & vbCrLf & _
                        " VALUES(@PO_NUMBER,@PO_DATE,@SPPB_NUMBER,@SPPB_DATE,@SHIP_TO,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101)); " & vbCrLf & _
                        " SELECT @@IDENTITY;"
                        str1 = "SET NOCOUNT ON;" & vbCrLf & _
                        " INSERT INTO GON_SEPARATED_PO_DETAIL(FKApp,QUANTITY,STATUS,CreatedBy,CreatedDate,ITEM_OTHER) " & vbCrLf & _
                        " VALUES(@FKApp,@QUANTITY,@STATUS,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101),@ITEM_OTHER) ;" & vbCrLf & _
                        " SELECT @@IDENTITY; "
                        str2 = "SET NOCOUNT ON; " & vbCrLf & _
                        "DECLARE @V_MESSAGE VARCHAR(200);" & vbCrLf & _
                        " SET @V_MESSAGE = CONCAT('GON NUMBER ' , @GON_NUMBER , ' Has already existed');" & vbCrLf & _
                        " IF EXISTS(SELECT GON_NUMBER FROM GON_SEPARATED_HEADER WHERE GON_NUMBER = @GON_NUMBER) " & vbCrLf & _
                        " BEGIN  RAISERROR(@V_MESSAGE,16,1);RETURN; END" & vbCrLf & _
                        " INSERT INTO GON_SEPARATED_HEADER(FKApp,GON_NUMBER,GON_DATE,WARHOUSE,SHIP_TO,SHIP_TO_CUST,SHIP_TO_ADDRESS,TRANSPORTER,POLICE_NO_TRANS,DRIVER_TRANS,REMARK,GON_AREA,GON_SHIP_TO,CreatedDate,CreatedBy) " & vbCrLf & _
                        " VALUES (@FKApp,@GON_NUMBER,@GON_DATE,@WARHOUSE,@SHIP_TO,@SHIP_TO_CUST,@SHIP_TO_ADDRESS,@TRANSPORTER,@POLICE_NO_TRANS,@DRIVER_TRANS,'',@GON_AREA,@GON_SHIP_TO,CONVERT(VARCHAR(100),GETDATE(),101),@CreatedBy) ;" & vbCrLf & _
                        " SELECT @@IDENTITY;"
                        str3 = "SET NOCOUNT ON;" & vbCrLf & _
                        " INSERT INTO GON_SEPARATED_DETAIL(FKAppGonHeader,FKAppPODetail,QTY,COLLY_BOX,COLLY_PACKSIZE,BATCH_NO,CreatedBy,CreatedDate,ITEM_OTHER) " & vbCrLf & _
                        " VALUES(@FKAppGonHeader,@FKAppPODetail,@QTY,@COLLY_BOX,@COLLY_PACKSIZE,@BATCH_NO,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101),@ITEM_OTHER) ;"
                    Case Helper.SaveMode.Update
                        str = "SET NOCOUNT ON;" & vbCrLf & _
                        "UPDATE GON_SEPARATED_PO_HEADER SET PO_DATE = @PO_DATE,SPPB_NUMBER = @SPPB_NUMBER,SPPB_DATE = @SPPB_DATE,SHIP_TO=@SHIP_TO,ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),101),ModifiedBy = @ModifiedBy " & vbCrLf & _
                        " WHERE PO_NUMBER = @PO_NUMBER ;"
                        str1 = "SET NOCOUNT ON;" & vbCrLf & _
                        " UPDATE GON_SEPARATED_PO_DETAIL SET ITEM_OTHER = @ITEM_OTHER,QUANTITY = @QUANTITY,STATUS=@STATUS,ModifiedBy = @Modifiedby,ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),101) WHERE IDApp=@IDApp ;"
                        str2 = "SET NOCOUNT ON;" & vbCrLf & _
                        " UPDATE GON_SEPARATED_HEADER SET GON_NUMBER = @GON_NUMBER,GON_DATE = @GON_DATE,WARHOUSE = @WARHOUSE,SHIP_TO = @SHIP_TO,SHIP_TO_CUST = @SHIP_TO_CUST,SHIP_TO_ADDRESS=@SHIP_TO_ADDRESS, " & vbCrLf & _
                        " TRANSPORTER = @TRANSPORTER,POLICE_NO_TRANS=@POLICE_NO_TRANS,DRIVER_TRANS=@DRIVER_TRANS,GON_AREA = @GON_AREA,GON_SHIP_TO=@GON_SHIP_TO,ModifiedBy = @ModifiedBy,ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),101) WHERE IDApp = @IDApp;"
                        str3 = "SET NOCOUNT ON;" & vbCrLf & _
                        " UPDATE GON_SEPARATED_DETAIL SET ITEM_OTHER=@ITEM_OTHER,QTY = @QTY,COLLY_BOX=@COLLY_BOX,COLLY_PACKSIZE=@COLLY_PACKSIZE,BATCH_NO=@BATCH_NO,ModifiedBy=@ModifiedBy,ModifiedDate=CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                        " WHERE IDApp = @IDApp ;"
                End Select

                If (Mode <> Helper.SaveMode.Insert) Then
                    If HasChangedpoHeader Then
                        commandUpdate.CommandText = str
                        commandUpdate.CommandType = CommandType.Text
                        commandUpdate.Transaction = Me.SqlTrans
                        commandUpdate.Parameters.Add("@PO_DATE", SqlDbType.DateTime, 0).Value = RuntimeHelpers.GetObjectValue(OSPPBHeader.PODate)
                        commandUpdate.Parameters.Add("@SPPB_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.SPPBNO
                        commandUpdate.Parameters.Add("@SPPB_DATE", SqlDbType.DateTime, 0).Value = RuntimeHelpers.GetObjectValue(OSPPBHeader.SPPBDate)
                        commandUpdate.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 50).Value = OSPPBHeader.ModifiedBy
                        commandUpdate.Parameters.Add("@SHIP_TO", SqlDbType.VarChar, 250).Value = OSPPBHeader.SPPBShipTo
                        commandUpdate.ExecuteScalar()
                        commandUpdate.Parameters.Clear()
                    End If
                    If HasChangedPOdetail Then
                        dataRowArray2 = DtPOdetail.Select("", "", DataViewRowState.Added)
                        If (CType(dataRowArray2.Length, Integer) > 0) Then
                            'TODO: checked/unchecked is not supported at this time
                            Query = " SET NOCOUNT ON;" & vbCrLf & _
                            "DECLARE @V_IDApp INT ;" & vbCrLf & _
                            " INSERT INTO GON_SEPARATED_PO_DETAIL(FKApp,QUANTITY,STATUS,CreatedBy,CreatedDate,ITEM_OTHER) " & vbCrLf & _
                            " VALUES(@FKApp,@QUANTITY,@STATUS,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101),@ITEM_OTHER) ;" & vbCrLf & _
                            " SELECT @V_IDApp = @@IDENTITY; " & vbCrLf & _
                            " INSERT INTO ##T_PODetailIdentity_" & Me.ComputerName & "(PO_NUMBER,ITEM_OTHER,IDApp)" & vbCrLf & _
                            " VALUES(@PO_NUMBER,@ITEM_OTHER,@V_IDApp) ;"
                            commandInsert.CommandType = CommandType.Text
                            commandInsert.CommandText = Query
                            commandInsert.Parameters.Add("PO_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.PONumber
                            commandInsert.Parameters.Add("@FKApp", SqlDbType.Int, 0).Value = OSPPBHeader.IDApp
                            commandInsert.Parameters.Add("@STATUS", SqlDbType.VarChar, 50, "STATUS")
                            commandInsert.Parameters.Add("@ITEM_OTHER", SqlDbType.Int, 0, "ITEM_OTHER")
                            commandInsert.Parameters.Add("@QUANTITY", SqlDbType.Decimal, 0, "QUANTITY")
                            commandInsert.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                            If (commandInsert.Transaction Is Nothing) Then
                                commandInsert.Transaction = Me.SqlTrans
                            End If
                            Me.SqlDat.InsertCommand = commandInsert
                            Me.SqlDat.Update(dataRowArray2)
                            flag1 = True
                            commandInsert.Parameters.Clear()
                            Me.SqlDat.InsertCommand = Nothing
                            'commandInsert = Nothing
                        End If
                        dataRowArray = DtPOdetail.Select("", "", DataViewRowState.ModifiedOriginal)
                        If (CType(dataRowArray.Length, Integer) > 0) Then
                            'TODO: checked/unchecked is not supported at this time
                            commandUpdate.CommandText = str1
                            commandUpdate.CommandType = CommandType.Text
                            If Information.IsNothing(commandUpdate.Transaction) Then
                                commandUpdate.Transaction = Me.SqlTrans
                            End If

                            commandUpdate.Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            commandUpdate.Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                            commandUpdate.Parameters.Add("@ITEM_OTHER", SqlDbType.Int, 0, "ITEM_OTHER")
                            commandUpdate.Parameters.Add("@QUANTITY", SqlDbType.Decimal, 0, "QUANTITY")
                            commandUpdate.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100, "ModifiedBy")
                            commandUpdate.Parameters.Add("@STATUS", SqlDbType.VarChar, 50, "STATUS")
                            Me.SqlDat.UpdateCommand = commandUpdate
                            Me.SqlDat.Update(dataRowArray)
                            flag1 = True
                            commandUpdate.Parameters.Clear()
                            'commandUpdate = Nothing
                            Me.SqlDat.UpdateCommand = Nothing
                        End If
                        dataRowArray1 = DtPOdetail.Select("", "", DataViewRowState.Deleted)
                        If (CType(dataRowArray1.Length, Integer) > 0) Then
                            'TODO: checked/unchecked is not supported at this time
                            commandDelete.CommandText = str6
                            commandDelete.CommandType = CommandType.Text
                            If Information.IsNothing(commandDelete.Transaction) Then
                                commandDelete.Transaction = Me.SqlTrans
                            End If
                            commandDelete.Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            commandDelete.Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                            Me.SqlDat.DeleteCommand = commandDelete
                            Me.SqlDat.Update(dataRowArray1)
                            flag1 = True
                            commandDelete.Parameters.Clear()
                            'commandDelete = Nothing
                            Me.SqlDat.DeleteCommand = Nothing
                        End If
                    End If

                    If HasChangedGoneHeader Then
                        Dim objectValue As Object = Nothing
                        Me.Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT 1 WHERE EXISTS(SELECT GON_NUMBER FROM GON_SEPARATED_HEADER WHERE GON_NUMBER = @GON_NO);"
                        CommandSelect.CommandText = Me.Query
                        CommandSelect.CommandType = CommandType.Text
                        CommandSelect.Parameters.Add("@GON_NO", SqlDbType.VarChar, 50).Value = OGonHeader.GON_NO
                        objectValue = RuntimeHelpers.GetObjectValue(CommandSelect.ExecuteScalar)
                        CommandSelect.Parameters.Clear()
                        If IsNothing(objectValue) Or IsDBNull(objectValue) Then
                            commandInsert.CommandText = " SET NOCOUNT ON;" & vbCrLf & _
                            " INSERT INTO GON_SEPARATED_HEADER(FKApp,GON_NUMBER,GON_DATE,WARHOUSE,SHIP_TO,SHIP_TO_CUST,SHIP_TO_ADDRESS,TRANSPORTER,POLICE_NO_TRANS,DRIVER_TRANS,REMARK,GON_AREA,GON_SHIP_TO,CreatedDate,CreatedBy) " & vbCrLf & _
                            " VALUES (@FKApp,@GON_NUMBER,@GON_DATE,@WARHOUSE,@SHIP_TO,@SHIP_TO_CUST,@SHIP_TO_ADDRESS,@TRANSPORTER,@POLICE_NO_TRANS,@DRIVER_TRANS,'',@GON_AREA,@GON_SHIP_TO,CONVERT(VARCHAR(100),GETDATE(),101),@CreatedBy) ;" & vbCrLf & _
                            " SELECT @@IDENTITY;"
                            commandInsert.CommandType = CommandType.Text
                            If Information.IsNothing(commandInsert.Transaction) Then
                                commandInsert.Transaction = Me.SqlTrans
                            End If
                            commandInsert.Parameters.Add("@FKApp", SqlDbType.Int, 0).Value = OSPPBHeader.IDApp
                            commandInsert.Parameters.Add("@GON_NUMBER", SqlDbType.VarChar, 50).Value = OGonHeader.GON_NO
                            commandInsert.Parameters.Add("@GON_DATE", SqlDbType.SmallDateTime, 0).Value = RuntimeHelpers.GetObjectValue(OGonHeader.GON_DATE)
                            commandInsert.Parameters.Add("@WARHOUSE", SqlDbType.VarChar, 20).Value = OGonHeader.WarhouseCode
                            commandInsert.Parameters.Add("@SHIP_TO", SqlDbType.VarChar, 14).Value = RuntimeHelpers.GetObjectValue(Interaction.IIf((Operators.CompareString(OGonHeader.DistributorID, "", False) <> 0), OGonHeader.DistributorID, DBNull.Value))
                            commandInsert.Parameters.Add("@SHIP_TO_CUST", SqlDbType.VarChar, 100).Value = OGonHeader.CustomerName
                            commandInsert.Parameters.Add("@SHIP_TO_ADDRESS", SqlDbType.VarChar, 150).Value = OGonHeader.CustomerAddress
                            commandInsert.Parameters.Add("@TRANSPORTER", SqlDbType.VarChar, 16).Value = RuntimeHelpers.GetObjectValue(Interaction.IIf((Operators.CompareString(OGonHeader.GT_ID, "", False) <> 0), OGonHeader.GT_ID, DBNull.Value))
                            commandInsert.Parameters.Add("@POLICE_NO_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.PoliceNoTrans
                            commandInsert.Parameters.Add("@DRIVER_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.DriverTrans
                            commandInsert.Parameters.Add("@GON_AREA", SqlDbType.VarChar, 20).Value = RuntimeHelpers.GetObjectValue(Interaction.IIf((Operators.CompareString(OGonHeader.GON_ID_AREA, "", False) <> 0), OGonHeader.GON_ID_AREA, DBNull.Value))
                            commandInsert.Parameters.Add("@GON_SHIP_TO", SqlDbType.VarChar, 250).Value = OGonHeader.ShipTo
                            commandInsert.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100).Value = OGonHeader.ModifiedBy
                            objectValue = RuntimeHelpers.GetObjectValue(commandInsert.ExecuteScalar)
                            commandInsert.Parameters.Clear()
                            If IsNothing(objectValue) Or IsDBNull(objectValue) Then
                                Me.SqlTrans.Rollback()
                                Me.SqlTrans.Dispose()
                                commandInsert.Connection.Close()
                                Throw New Exception("unknown error" & vbCrLf & _
                                " On possition of inserting GON_SEPARATED_DETAIL")
                            End If
                            OGonHeader.IDApp = Conversions.ToInteger(objectValue)
                            'commandInsert = Nothing
                        Else
                            commandUpdate.CommandText = str2
                            commandUpdate.CommandType = CommandType.Text
                            If Information.IsNothing(commandUpdate.Transaction) Then
                                commandUpdate.Transaction = Me.SqlTrans
                            End If
                            commandUpdate.Parameters.Add("@IDApp", SqlDbType.Int, 0).Value = OGonHeader.IDApp
                            commandUpdate.Parameters.Add("@GON_NUMBER", SqlDbType.VarChar, 50).Value = OGonHeader.GON_NO
                            commandUpdate.Parameters.Add("@GON_DATE", SqlDbType.SmallDateTime, 0).Value = RuntimeHelpers.GetObjectValue(OGonHeader.GON_DATE)
                            commandUpdate.Parameters.Add("@WARHOUSE", SqlDbType.VarChar, 20).Value = OGonHeader.WarhouseCode
                            commandUpdate.Parameters.Add("@SHIP_TO", SqlDbType.VarChar, 14).Value = RuntimeHelpers.GetObjectValue(Interaction.IIf((Operators.CompareString(OGonHeader.DistributorID, "", False) <> 0), OGonHeader.DistributorID, DBNull.Value))
                            commandUpdate.Parameters.Add("@SHIP_TO_CUST", SqlDbType.VarChar, 100).Value = OGonHeader.CustomerName
                            commandUpdate.Parameters.Add("@SHIP_TO_ADDRESS", SqlDbType.VarChar, 150).Value = OGonHeader.CustomerAddress
                            commandUpdate.Parameters.Add("@TRANSPORTER", SqlDbType.VarChar, 16).Value = RuntimeHelpers.GetObjectValue(Interaction.IIf((Operators.CompareString(OGonHeader.GT_ID, "", False) <> 0), OGonHeader.GT_ID, DBNull.Value))
                            commandUpdate.Parameters.Add("@POLICE_NO_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.PoliceNoTrans
                            commandUpdate.Parameters.Add("@DRIVER_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.DriverTrans
                            commandUpdate.Parameters.Add("@GON_AREA", SqlDbType.VarChar, 20).Value = RuntimeHelpers.GetObjectValue(Interaction.IIf((Operators.CompareString(OGonHeader.GON_ID_AREA, "", False) <> 0), OGonHeader.GON_ID_AREA, DBNull.Value))
                            commandUpdate.Parameters.Add("@GON_SHIP_TO", SqlDbType.VarChar, 250).Value = OGonHeader.ShipTo
                            commandUpdate.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100).Value = OGonHeader.ModifiedBy
                            commandUpdate.ExecuteScalar()
                            commandUpdate.Parameters.Clear()
                            'warhouseCode = Nothing
                        End If
                    End If
                    If HasChangedGONDetail Then
                        dataRowArray2 = dtGonDetail.Select("", "", DataViewRowState.Added)
                        If (CType(dataRowArray2.Length, Integer) > 0) Then
                            'TODO: checked/unchecked is not supported at this time
                            Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "DECLARE @V_FKApp INT ;" & vbCrLf & _
                            " IF @FKAppPODetail = 0 or @FKAppPODetail IS NULL " & vbCrLf & _
                            " BEGIN SET @V_FKApp = (SELECT IDApp FROM ##T_PODetailIdentity_" & Me.ComputerName & " WHERE PO_NUMBER = @PO_NUMBER AND ITEM_OTHER = @ITEM_OTHER) ; END" & vbCrLf & _
                            " ELSE " & vbCrLf & _
                            " BEGIN SELECT @V_FKApp=@FKAppPODetail; END " & vbCrLf & _
                            " INSERT INTO GON_SEPARATED_DETAIL(FKAppGonHeader,FKAppPODetail,QTY,COLLY_BOX,COLLY_PACKSIZE,BATCH_NO,CreatedBy,CreatedDate,ITEM_OTHER) " & vbCrLf & _
                            " VALUES(@FKAppGonHeader,@V_FKApp,@QTY,@COLLY_BOX,@COLLY_PACKSIZE,@BATCH_NO,@CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101),@ITEM_OTHER) ;"
                            commandInsert.CommandText = Query
                            commandInsert.CommandType = CommandType.Text
                            commandInsert.Parameters.Add("@FKAppGonHeader", SqlDbType.Int, 0).Value = OGonHeader.IDApp
                            commandInsert.Parameters.Add("@FKAppPODetail", SqlDbType.Int, 0, "FKAppPODetail")
                            commandInsert.Parameters.Add("@ITEM_OTHER", SqlDbType.Int, 0, "ITEM_OTHER")
                            commandInsert.Parameters.Add("@QTY", SqlDbType.Decimal, 0, "QTY")
                            commandInsert.Parameters.Add("@COLLY_BOX", SqlDbType.VarChar, 50, "COLLY_BOX")
                            commandInsert.Parameters.Add("@COLLY_PACKSIZE", SqlDbType.VarChar, 50, "COLLY_PACKSIZE")
                            commandInsert.Parameters.Add("@BATCH_NO", SqlDbType.NVarChar, 50, "BATCH_NO")
                            commandInsert.Parameters.Add("PO_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.PONumber
                            commandInsert.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = OGonHeader.ModifiedBy
                            If Information.IsNothing(commandInsert.Transaction) Then
                                commandInsert.Transaction = Me.SqlTrans
                            End If
                            'commandInsert = Nothing
                            Me.SqlDat.InsertCommand = commandInsert
                            Me.SqlDat.Update(dataRowArray2)
                            flag2 = True
                            Me.SqlDat.InsertCommand = Nothing
                        End If

                        dataRowArray = dtGonDetail.Select("", "", DataViewRowState.ModifiedOriginal)
                        If (CType(dataRowArray.Length, Integer) > 0) Then
                            'TODO: checked/unchecked is not supported at this time
                            commandUpdate.CommandText = str3
                            commandUpdate.CommandType = CommandType.Text
                            If Information.IsNothing(commandUpdate.Transaction) Then
                                commandUpdate.Transaction = Me.SqlTrans
                            End If
                            commandUpdate.Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            commandUpdate.Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                            commandUpdate.Parameters.Add("@ITEM_OTHER", SqlDbType.Int, 0, "ITEM_OTHER")
                            commandUpdate.Parameters.Add("@QTY", SqlDbType.Decimal, 0, "QTY")
                            commandUpdate.Parameters.Add("@COLLY_BOX", SqlDbType.VarChar, 50, "COLLY_BOX")
                            commandUpdate.Parameters.Add("@COLLY_PACKSIZE", SqlDbType.VarChar, 50, "COLLY_PACKSIZE")
                            commandUpdate.Parameters.Add("@BATCH_NO", SqlDbType.NVarChar, 50, "BATCh_NO")
                            commandUpdate.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 50, "ModifiedBy")
                            Me.SqlDat.UpdateCommand = commandUpdate
                            Me.SqlDat.Update(dataRowArray)
                            flag2 = True
                            commandUpdate.Parameters.Clear()
                            'commandUpdate = Nothing
                            Me.SqlDat.UpdateCommand = Nothing
                        End If

                        dataRowArray1 = dtGonDetail.Select("", "", DataViewRowState.Deleted)
                        If (CType(dataRowArray1.Length, Integer) > 0) Then
                            'TODO: checked/unchecked is not supported at this time
                            commandDelete.CommandText = str7
                            commandDelete.CommandType = CommandType.Text
                            If Information.IsNothing(commandDelete.Transaction) Then
                                commandDelete.Transaction = Me.SqlTrans
                            End If
                            commandDelete.Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            commandDelete.Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                            Me.SqlDat.DeleteCommand = commandDelete
                            Me.SqlDat.Update(dataRowArray1)
                            flag2 = True
                            commandDelete.Parameters.Clear()
                            'commandDelete = Nothing
                            Me.SqlDat.DeleteCommand = Nothing
                        End If

                    End If

                Else
                    Dim IDAppPOHeader As Integer = 0
                    Dim num As Integer = 0
                    Dim integer1 As Integer = 0
                    'Dim sPPBNO As SqlCommand = SqlCommand
                    Dim obj As Object = Nothing
                    If Not HasChangedpoHeader Then
                        IDAppPOHeader = OSPPBHeader.IDApp
                    Else
                        commandInsert.CommandText = str
                        commandInsert.CommandType = CommandType.Text
                        commandInsert.Transaction = Me.SqlTrans
                        commandInsert.Parameters.Add("@PO_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.PONumber
                        commandInsert.Parameters.Add("@PO_DATE", SqlDbType.DateTime, 0).Value = RuntimeHelpers.GetObjectValue(OSPPBHeader.PODate)
                        commandInsert.Parameters.Add("@SPPB_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.SPPBNO
                        commandInsert.Parameters.Add("@SPPB_DATE", SqlDbType.DateTime, 0).Value = RuntimeHelpers.GetObjectValue(OSPPBHeader.SPPBDate)
                        commandInsert.Parameters.Add("@SHIP_TO", SqlDbType.VarChar, 250).Value = OSPPBHeader.SPPBShipTo
                        commandInsert.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100).Value = OSPPBHeader.CreatedBy
                        obj = RuntimeHelpers.GetObjectValue(commandInsert.ExecuteScalar)
                        commandInsert.Parameters.Clear()
                        If IsNothing(obj) Or IsDBNull(obj) Then
                            Me.SqlTrans.Rollback()
                            Me.SqlTrans.Dispose()
                            commandInsert.Connection.Close()
                            Throw New Exception("unknown error" & vbCrLf & _
                            "On possition of inserting GON_SEPARATED_PO_HEADER")
                        End If
                        IDAppPOHeader = Conversions.ToInteger(obj)
                        OSPPBHeader.IDApp = IDAppPOHeader
                    End If

                    OGonHeader.FkApp = IDAppPOHeader
                    Dim objectValue1 As Object = Nothing
                    If HasChangedGoneHeader Then
                        If Not Information.IsNothing(OGonHeader) Then
                            commandInsert.CommandText = str2
                            commandInsert.CommandType = CommandType.Text
                            commandInsert.Parameters.Add("@FKApp", SqlDbType.Int, 0).Value = IDAppPOHeader
                            commandInsert.Parameters.Add("@GON_NUMBER", SqlDbType.VarChar, 50).Value = OGonHeader.GON_NO
                            commandInsert.Parameters.Add("@GON_DATE", SqlDbType.SmallDateTime, 0).Value = RuntimeHelpers.GetObjectValue(OGonHeader.GON_DATE)
                            commandInsert.Parameters.Add("@WARHOUSE", SqlDbType.VarChar, 20).Value = OGonHeader.WarhouseCode
                            commandInsert.Parameters.Add("@SHIP_TO", SqlDbType.VarChar, 14).Value = RuntimeHelpers.GetObjectValue(Interaction.IIf((Operators.CompareString(OGonHeader.DistributorID, "", False) <> 0), OGonHeader.DistributorID, DBNull.Value))
                            commandInsert.Parameters.Add("@SHIP_TO_CUST", SqlDbType.VarChar, 100).Value = OGonHeader.CustomerName
                            commandInsert.Parameters.Add("@SHIP_TO_ADDRESS", SqlDbType.VarChar, 150).Value = OGonHeader.CustomerAddress
                            commandInsert.Parameters.Add("@TRANSPORTER", SqlDbType.VarChar, 16).Value = RuntimeHelpers.GetObjectValue(Interaction.IIf((Operators.CompareString(OGonHeader.GT_ID, "", False) <> 0), OGonHeader.GT_ID, DBNull.Value))
                            commandInsert.Parameters.Add("@POLICE_NO_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.PoliceNoTrans
                            commandInsert.Parameters.Add("@DRIVER_TRANS", SqlDbType.VarChar, 50).Value = OGonHeader.DriverTrans
                            commandInsert.Parameters.Add("@GON_AREA", SqlDbType.VarChar, 20).Value = RuntimeHelpers.GetObjectValue(Interaction.IIf((Operators.CompareString(OGonHeader.GON_ID_AREA, "", False) <> 0), OGonHeader.GON_ID_AREA, DBNull.Value))
                            commandInsert.Parameters.Add("@GON_SHIP_TO", SqlDbType.VarChar, 250).Value = OGonHeader.ShipTo
                            commandInsert.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100).Value = OGonHeader.CreatedBy
                            If (commandInsert.Transaction Is Nothing) Then
                                commandInsert.Transaction = Me.SqlTrans
                            End If

                            objectValue1 = RuntimeHelpers.GetObjectValue(commandInsert.ExecuteScalar)
                            commandInsert.Parameters.Clear()
                        End If

                        If Not Information.IsNothing(RuntimeHelpers.GetObjectValue(objectValue1)) Then
                            num = Conversions.ToInteger(objectValue1)
                        End If

                        If (Information.IsNothing(RuntimeHelpers.GetObjectValue(objectValue1)) Or Information.IsDBNull(RuntimeHelpers.GetObjectValue(objectValue1))) Then
                            Me.SqlTrans.Rollback()
                            Me.SqlTrans.Dispose()
                            commandInsert.Connection.Close()
                            Throw New Exception("unknown error" & vbCrLf & _
                            "On possition of inserting GON_SEPARATED_HEADER")
                        End If
                        OGonHeader.IDApp = num
                    End If

                    Dim strs As List(Of String) = New List(Of String)
                    If Not Information.IsNothing(DtPOdetail) Then
                        Dim dataRowArray3() As DataRow = DtPOdetail.Select("", "", DataViewRowState.Added)
                        If (CType(dataRowArray3.Length, Integer) > 0) Then
                            Dim dataRowArray4() As DataRow = dataRowArray3
                            'TODO: checked/unchecked is not supported at this time
                            Dim i As Integer = 0
                            Do While (i < CType(dataRowArray4.Length, Integer))
                                Dim dataRow As DataRow = dataRowArray4(i)
                                'TODO: checked/unchecked is not supported at this time
                                'TODO: checked/unchecked is not supported at this time
                                commandInsert.CommandText = str1
                                commandInsert.CommandType = CommandType.Text
                                commandInsert.Parameters.Add("@FKApp", SqlDbType.Int, 0).Value = IDAppPOHeader
                                Dim num1 As Integer = Conversions.ToInteger(dataRow("ITEM_OTHER"))
                                Dim str8 As String = Conversions.ToString(dataRow("STATUS"))
                                commandInsert.Parameters.Add("@ITEM_OTHER", SqlDbType.Int).Value = num1
                                commandInsert.Parameters.Add("@QUANTITY", SqlDbType.Decimal, 0).Value = RuntimeHelpers.GetObjectValue(dataRow("QUANTITY"))
                                commandInsert.Parameters.Add("@STATUS", SqlDbType.VarChar, 50).Value = str8
                                commandInsert.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100).Value = OSPPBHeader.CreatedBy
                                If (commandInsert.Transaction Is Nothing) Then
                                    commandInsert.Transaction = Me.SqlTrans
                                End If

                                Dim obj1 As Object = RuntimeHelpers.GetObjectValue(commandInsert.ExecuteScalar)
                                commandInsert.Parameters.Clear()
                                If (Information.IsNothing(RuntimeHelpers.GetObjectValue(obj1)) Or Information.IsDBNull(RuntimeHelpers.GetObjectValue(obj1))) Then
                                    Me.SqlTrans.Rollback()
                                    Me.SqlTrans.Dispose()
                                    commandInsert.Connection.Close()
                                    Throw New Exception("unknown error" & vbCrLf & _
                                    "On possition of inserting GON_SEPARATED_PO_DETAIL")
                                End If
                                integer1 = Conversions.ToInteger(obj1)
                                flag1 = True
                                If (Not Information.IsNothing(dtGonDetail) _
                                            And HasChangedGONDetail) Then
                                    Dim dataRowArray5() As DataRow = dtGonDetail.Select(String.Concat("ITEM_OTHER = ", Conversions.ToString(num1)))
                                    If (CType(dataRowArray5.Length, Integer) > 0) Then
                                        commandInsert.CommandText = str3
                                        'TODO: checked/unchecked is not supported at this time
                                        commandInsert.Parameters.Add("@FKAppGonHeader", SqlDbType.Int, 0).Value = num
                                        commandInsert.Parameters.Add("@FKAppPODetail", SqlDbType.Int, 0).Value = integer1
                                        commandInsert.Parameters.Add("@ITEM_OTHER", SqlDbType.Int).Value = RuntimeHelpers.GetObjectValue(dataRowArray5(0)("ITEM_OTHER"))
                                        commandInsert.Parameters.Add("@QTY", SqlDbType.Decimal, 0).Value = RuntimeHelpers.GetObjectValue(dataRowArray5(0)("QTY"))
                                        commandInsert.Parameters.Add("@COLLY_BOX", SqlDbType.VarChar, 50).Value = RuntimeHelpers.GetObjectValue(dataRowArray5(0)("COLLY_BOX"))
                                        commandInsert.Parameters.Add("@COLLY_PACKSIZE", SqlDbType.VarChar, 50).Value = RuntimeHelpers.GetObjectValue(dataRowArray5(0)("COLLY_PACKSIZE"))
                                        commandInsert.Parameters.Add("@BATCH_NO", SqlDbType.NVarChar).Value = RuntimeHelpers.GetObjectValue(dataRowArray5(0)("BATCH_NO"))
                                        commandInsert.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = OGonHeader.CreatedBy
                                        commandInsert.ExecuteScalar()
                                        If Not strs.Contains(Conversions.ToString(num1)) Then
                                            strs.Add(Conversions.ToString(num1))
                                        End If

                                        commandInsert.Parameters.Clear()
                                        flag2 = True
                                    End If
                                End If
                                i = (i + 1)
                            Loop
                        End If

                        dataRowArray = DtPOdetail.Select("", "", DataViewRowState.ModifiedOriginal)
                        If (CType(dataRowArray.Length, Integer) > 0) Then

                            'TODO: checked/unchecked is not supported at this time
                            commandUpdate.CommandText = "SET NOCOUNT ON;" & vbCrLf & _
                            " UPDATE GON_SEPARATED_PO_DETAIL SET ITEM_OTHER = @ITEM_OTHER,QUANTITY = @QUANTITY,STATUS=@STATUS,ModifiedBy = @Modifiedby,ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),101) WHERE IDApp=@IDApp ;"
                            commandUpdate.CommandType = CommandType.Text
                            If Information.IsNothing(commandUpdate.Transaction) Then
                                commandUpdate.Transaction = Me.SqlTrans
                            End If
                            commandUpdate.Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            commandUpdate.Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                            commandUpdate.Parameters.Add("@ITEM_OTHER", SqlDbType.Int, 0, "ITEM_OTHER")
                            commandUpdate.Parameters.Add("@QUANTITY", SqlDbType.Decimal, 0, "QUANTITY")
                            commandUpdate.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100, "ModifiedBy")
                            commandUpdate.Parameters.Add("@STATUS", SqlDbType.VarChar, 50, "STATUS")
                            Me.SqlDat.UpdateCommand = commandUpdate
                            Me.SqlDat.Update(dataRowArray)
                            flag1 = True
                            commandUpdate.Parameters.Clear()
                            'commandUpdate = Nothing
                            Me.SqlDat.UpdateCommand = Nothing
                        End If

                        dataRowArray1 = DtPOdetail.Select("", "", DataViewRowState.Deleted)
                        If (CType(dataRowArray1.Length, Integer) > 0) Then
                            ''Dim sqlTrans5 As SqlCommand = sqlCommand2
                            'TODO: checked/unchecked is not supported at this time
                            commandDelete.CommandText = str6
                            commandDelete.CommandType = CommandType.Text
                            If Information.IsNothing(commandDelete.Transaction) Then
                                commandDelete.Transaction = Me.SqlTrans
                            End If

                            commandDelete.Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            commandDelete.Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                            Me.SqlDat.DeleteCommand = commandDelete
                            Me.SqlDat.Update(dataRowArray1)
                            flag1 = True
                            commandDelete.Parameters.Clear()
                            'commandDelete = Nothing
                            Me.SqlDat.DeleteCommand = Nothing
                        End If

                    End If

                    Dim str9 As String = "ITEM NOT IN("
                    If (strs.Count > 0) Then
                        Dim count As Integer = (strs.Count - 1)
                        'TODO: checked/unchecked is not supported at this time
                        Dim j As Integer = 0
                        Do While (j <= count)
                            str9 = String.Concat(str9, strs(j))
                            'TODO: checked/unchecked is not supported at this time
                            If (j _
                                        < (strs.Count - 1)) Then
                                str9 = String.Concat(str9, ",")
                                'TODO: checked/unchecked is not supported at this time
                            End If
                            j = (j + 1)
                        Loop
                        str9 = String.Concat(str9, ")")
                    End If

                    If HasChangedGONDetail Then
                        dataRowArray2 = dtGonDetail.Select(Conversions.ToString(Interaction.IIf((strs.Count > 0), str9, "")), "", DataViewRowState.Added)
                        If (CType(dataRowArray2.Length, Integer) > 0) Then
                            commandInsert.CommandText = str3
                            'TODO: checked/unchecked is not supported at this time
                            commandInsert.CommandType = CommandType.Text
                            commandInsert.Parameters.Add("@FKAppGonHeader", SqlDbType.Int, 0).Value = num
                            commandInsert.Parameters.Add("@FKAppPODetail", SqlDbType.Int, 0, "FKAppPODetail")
                            commandInsert.Parameters.Add("@ITEM_OTHER", SqlDbType.Int, 0, "ITEM_OTHER")
                            commandInsert.Parameters.Add("@QTY", SqlDbType.Decimal, 0, "QTY")
                            commandInsert.Parameters.Add("@COLLY_BOX", SqlDbType.VarChar, 50, "COLLY_BOX")
                            commandInsert.Parameters.Add("@COLLY_PACKSIZE", SqlDbType.VarChar, 50, "COLLY_PACKSIZE")
                            commandInsert.Parameters.Add("@BATCH_NO", SqlDbType.NVarChar, 50, "BATCH_NO")
                            commandInsert.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50, "CreatedBy")
                            If (commandInsert.Transaction Is Nothing) Then
                                commandInsert.Transaction = Me.SqlTrans
                            End If

                            Me.SqlDat.InsertCommand = commandInsert
                            Me.SqlDat.Update(dataRowArray2)
                            flag2 = True
                            commandInsert.Parameters.Clear()
                            Me.SqlDat.InsertCommand = Nothing
                        End If

                        Dim dataRowArray6() As DataRow = dtGonDetail.Select("", "", DataViewRowState.ModifiedOriginal)
                        If (CType(dataRowArray6.Length, Integer) > 0) Then
                            'Dim sqlCommand5 As SqlCommand = sqlCommand1
                            'TODO: checked/unchecked is not supported at this time
                            commandUpdate.CommandText = "SET NOCOUNT ON;" & vbCrLf & _
                            " UPDATE GON_SEPARATED_DETAIL SET ITEM_OTHER=@ITEM_OTHER,QTY = @QTY,COLLY_BOX=@COLLY_BOX,COLLY_PACKSIZE=@COLLY_PACKSIZE,BATCH_NO=@BATCH_NO,ModifiedBy=@ModifiedBy,ModifiedDate=CONVERT(VARCHAR(100),GETDATE(),101) " & vbCrLf & _
                            " WHERE IDApp = @IDApp ;"
                            commandUpdate.CommandType = CommandType.Text
                            If Information.IsNothing(commandUpdate.Transaction) Then
                                commandUpdate.Transaction = Me.SqlTrans
                            End If

                            commandUpdate.Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            commandUpdate.Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                            commandUpdate.Parameters.Add("@ITEM_OTHER", SqlDbType.Int, 0, "ITEM_OTHER")
                            commandUpdate.Parameters.Add("@QTY", SqlDbType.Decimal, 0, "QTY")
                            commandUpdate.Parameters.Add("@COLLY_BOX", SqlDbType.VarChar, 50, "COLLY_BOX")
                            commandUpdate.Parameters.Add("@COLLY_PACKSIZE", SqlDbType.VarChar, 50, "COLLY_PACKSIZE")
                            commandUpdate.Parameters.Add("@BATCH_NO", SqlDbType.NVarChar, 50, "BATCH_NO")
                            commandUpdate.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 50, "ModifiedBy")
                            Me.SqlDat.UpdateCommand = commandUpdate
                            Me.SqlDat.Update(dataRowArray6)
                            flag2 = True
                            commandUpdate.Parameters.Clear()
                            'commandUpdate = Nothing
                            Me.SqlDat.UpdateCommand = Nothing
                        End If

                        dataRowArray1 = dtGonDetail.Select("", "", DataViewRowState.Deleted)
                        If (CType(dataRowArray1.Length, Integer) > 0) Then

                            'TODO: checked/unchecked is not supported at this time
                            commandDelete.CommandText = str7
                            commandDelete.CommandType = CommandType.Text
                            If Information.IsNothing(commandDelete.Transaction) Then
                                commandDelete.Transaction = Me.SqlTrans
                            End If

                            commandDelete.Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            commandDelete.Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                            Me.SqlDat.DeleteCommand = commandDelete
                            Me.SqlDat.Update(dataRowArray1)
                            flag2 = True
                            commandDelete.Parameters.Clear()
                            'commandDelete = Nothing
                            Me.SqlDat.DeleteCommand = Nothing
                        End If

                    End If
                End If
                Me.SqlTrans.Commit()
                If (Not Information.IsNothing(DtPOdetail) _
                            And HasChangedPOdetail) Then
                    DtPOdetail.AcceptChanges()
                End If
                If (Not Information.IsNothing(dtGonDetail) _
                            And HasChangedGONDetail) Then
                    dtGonDetail.AcceptChanges()
                End If
                If flag1 Then
                    str4 = "SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT GSPD.* FROM GON_SEPARATED_PO_DETAIL GSPD INNER JOIN GON_SEPARATED_PO_HEADER GSPH ON GSPH.IDApp = GSPD.FKApp " & vbCrLf & _
                    " WHERE GSPH.PO_NUMBER = @PO_NUMBER;"
                    'Dim commandSelect As SqlCommand = sqlCommand3
                    CommandSelect.CommandType = CommandType.Text
                    CommandSelect.CommandText = str4
                    CommandSelect.Parameters.Add("@PO_NUMBER", SqlDbType.VarChar, 50).Value = OSPPBHeader.PONumber
                    DtPOdetail.Clear()
                    Me.SqlDat.SelectCommand = CommandSelect
                    Me.SqlDat.Fill(DtPOdetail)
                    CommandSelect.Parameters.Clear()
                    'CommandSelect = Nothing
                End If

                If flag2 Then
                    str5 = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT GSD.*,SAMPLE_PRODUCT = BPO.ITEM,QTY_UNIT = CONVERT(VARCHAR(100),QTY) + ISNULL(BPO.UnitOfMeasure,'') FROM GON_SEPARATED_DETAIL GSD " & vbCrLf & _
                    " INNER JOIN GON_SEPARATED_HEADER GSH ON GSH.IDApp = GSD.FKAppGonHeader INNER JOIN BRND_PROD_OTHER BPO ON BPO.IDApp=GSD.ITEM_OTHER " & vbCrLf & _
                    " WHERE GSH.GON_NUMBER = @GON_NUMBER;"
                    'Dim gONNO1 As SqlCommand = sqlCommand3
                    CommandSelect.CommandType = CommandType.Text
                    CommandSelect.CommandText = str5
                    CommandSelect.Parameters.Add("@GON_NUMBER", SqlDbType.VarChar, 50).Value = OGonHeader.GON_NO
                    dtGonDetail.Clear()
                    Me.SqlDat.SelectCommand = CommandSelect
                    Me.SqlDat.Fill(dtGonDetail)
                    CommandSelect.Parameters.Clear()
                    'CommandSelect = Nothing
                End If

                Me.ClearCommandParameters()
                Me.CloseConnection()
                flag = True
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                Me.RollbackTransaction()
                Me.CloseConnection()
                If Not Information.IsNothing(CommandSelect) Then
                    CommandSelect.Parameters.Clear()
                    CommandSelect.Dispose()
                    CommandSelect = Nothing
                End If

                If Not Information.IsNothing(commandInsert) Then
                    commandInsert.Parameters.Clear()
                    commandInsert.Dispose()
                    commandInsert = Nothing
                End If

                If Not Information.IsNothing(commandUpdate) Then
                    commandUpdate.Parameters.Clear()
                    commandUpdate.Dispose()
                    commandUpdate = Nothing
                End If

                If Not Information.IsNothing(commandDelete) Then
                    commandDelete.Parameters.Clear()
                    commandDelete.Dispose()
                    commandDelete = Nothing
                End If

                MessageBox.Show(exception.Message, "Unhandled system exception", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                flag = False
                ProjectData.ClearProjectError()
            End Try
            Return flag
        End Function
    End Class
End Namespace