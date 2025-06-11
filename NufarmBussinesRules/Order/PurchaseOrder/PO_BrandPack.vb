Imports System.Data.SqlClient
Imports System.Data
Imports System.Globalization
Namespace PurchaseOrder
    Public Class PO_BrandPack
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet


#Region " Deklarasi "

        Private m_ViewDistributor As DataView
        Private m_ViewPOBrandPack As DataView
        Private m_ViewProject As DataView
        Private m_ViewBrandPack As DataView
        Private m_ViewBrandPack_1 As DataView
        Private m_ViewPOSMS As DataView
        Private m_ViewActivePO As DataView
        Public DistributorID As String
        Public PO_REF_NO As String
        Public PO_REF_DATE As Date
        Public dsPPBrandPack As DataSet
        Public dsPOBrandPackHasChanged As Boolean ' to detect changed from pobrandpack detail
        Public PROJ_REF_NO As Object = DBNull.Value
        Private m_ViewPrice As DataView
        Private Query As String = ""
#End Region

#Region " Function "

        Protected Function RepLaceComaWithDot(ByVal text As String) As String
            Dim l As Integer = Len(Trim(text))
            Dim w As Integer = 1
            Dim s As String = ""
            Dim a As String = ""
            Do Until w = l + 1
                s = Mid(Trim(text), w, 1)
                If s = "," Then
                    s = "."
                End If
                a = a & s
                w += 1
            Loop
            Return a
        End Function
        Protected Function RepLaceDotWithComa(ByVal text As String) As String
            Dim l As Integer = Len(Trim(text))
            Dim w As Integer = 1
            Dim s As String = ""
            Dim a As String = ""
            Do Until w = l + 1
                s = Mid(Trim(text), w, 1)
                If s = "." Then
                    s = ","
                End If
                a = a & s
                w += 1
            Loop
            Return a
        End Function

        Public Function CreateViewPOSMS() As DataView
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_GetView_PO_SMS", "")
                Else : Me.ClearCommandParameters() : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_GetView_PO_SMS")
                End If
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                ElseIf Not (Me.SqlDat.SelectCommand Is Me.SqlCom) Then
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                End If

                'Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Dim tblMessage As New DataTable("SMS")
                Me.OpenConnection() : Me.SqlDat.Fill(tblMessage)

                Dim ColDescription As New DataColumn("Descriptions", Type.GetType("System.String"))
                tblMessage.Columns.Add(ColDescription)
                Dim colTotalPO As New DataColumn("TOTAL_PO", Type.GetType("System.Decimal"))
                tblMessage.Columns.Add(colTotalPO)
                Me.m_ViewPOSMS = tblMessage.DefaultView()
                With Me.m_ViewPOSMS
                    If Me.m_ViewPOSMS.Count > 0 Then
                        Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_PO_Description")
                        For i As Integer = 0 To Me.m_ViewPOSMS.Count - 1
                            'Me.CreateCommandSql("Usp_Get_PO_Description", "")
                            Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, .Item(i)("PO_REF_NO"), 25)
                            Dim tblDesc As New DataTable("t_po")
                            tblDesc.Clear() : Me.SqlDat.Fill(tblDesc) : Me.ClearCommandParameters()
                            'Me.FillDataTable(tblDesc)
                            Dim ItemDescription As String = "Items{(", TOTAL_PO As Decimal = 0
                            If tblDesc.Rows.Count > 0 Then
                                For I_1 As Integer = 0 To tblDesc.Rows.Count - 1
                                    ItemDescription &= tblDesc.Rows(I_1)("BRANDPACK_NAME").ToString() & ",Qty = " & String.Format(New CultureInfo("id-ID"), "{0:#,##0.000}", Convert.ToDecimal(tblDesc.Rows(I_1)("PO_ORIGINAL_QTY")))
                                    If I_1 < tblDesc.Rows.Count - 1 Then
                                        ItemDescription &= "),("
                                    Else
                                        ItemDescription &= ")}"
                                    End If
                                    TOTAL_PO += Convert.ToDecimal(tblDesc.Rows(I_1)("TOTAL"))
                                    'PO_DESCRIPTION &= String.Format("{0:#,##0.000}", tblDesc.Rows(I_1)("PO_ORIGINAL_QTY")) & "  " & tblDesc.Rows(I_1)("BRANDPACK_NAME").ToString()
                                    'If I_1 < tblDesc.Rows.Count - 1 Then
                                    '    PO_DESCRIPTION &= ","
                                    'End If
                                Next
                            End If
                            'String.Format("{0:#,##0.00}", CDec(Me.clsOADiscount.TOTAL_PRICE_DISTRIBUTOR)) 
                            .Item(i)("TOTAL_PO") = TOTAL_PO
                            .Item(i)("Descriptions") = ItemDescription
                            .Item(i).EndEdit()
                        Next
                    End If
                End With
                Me.CloseConnection()
            Catch ex As Exception
                Me.ClearCommandParameters() : Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewPOSMS
        End Function

        Public Function CreateViewGONSMS() As DataView
            Try
                Dim DV As DataView = Nothing
                'Query = "SET NOCOUNT ON ; " & vbCrLf & _
                '          "DECLARE @ParamValue INT ; " & vbCrLf & _
                '          " SELECT @ParamValue = CAST((SELECT ParamValue FROM RefBussinesRules WHERE CodeApp = 'MSC0001')AS INT); " & vbCrLf & _
                '          " SELECT SB.SPPB_NO,SB.SPPB_DATE,PO.PO_REF_NO,PO.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME " & vbCrLf & _
                '          " FROM ORDR_PURCHASE_ORDER PO INNER JOIN SPPB_HEADER SB ON PO.PO_REF_NO = SB.PO_REF_NO " & vbCrLf & _
                '          " INNER JOIN DIST_DISTRIBUTOR DR ON PO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID " & vbCrLf & _
                '          " WHERE DATEDIFF(Day, SB.SPPB_DATE, @GETDATE ) <= @ParamValue " & vbCrLf & _
                '          " AND NOT EXISTS(SELECT SPPB_NO FROM SPPB_BRANDPACK WHERE SPPB_NO = SB.SPPB_NO AND (STATUS = 'PENDING' OR STATUS = 'PARTIAL')) " & vbCrLf & _
                '          " AND NOT EXISTS(SELECT SPPB_NO FROM GON_SMS WHERE SPPB_NO = SB.SPPB_NO);"
                Query = "SET NOCOUNT ON ; " & vbCrLf & _
                          "DECLARE @ParamValue INT ; " & vbCrLf & _
                          "SELECT @ParamValue = CAST((SELECT ParamValue FROM RefBussinesRules WHERE CodeApp = 'MSC0001')AS INT); " & vbCrLf & _
                          "SELECT G.TransactionID,G.GON_NO,G.SPPB_NO,G.GON_DATE,G.PO_REF_NO,G.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,G.ITEM_DESCRIPTION,G.MESSAGE FROM GON_SMS G" & vbCrLf & _
                          " INNER JOIN DIST_DISTRIBUTOR DR ON G.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID WHERE G.STATUS_SENT IS NULL " & vbCrLf & _
                          " AND DATEDIFF(DAY,G.GON_DATE,@GETDATE) <= @ParamValue ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@GETDATE", SqlDbType.DateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Dim tblHeader As New DataTable("T_HeaderSPPB") : tblHeader.Clear()
                Me.OpenConnection() : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(tblHeader) : Me.ClearCommandParameters()
                'Dim colDescription As New DataColumn("DESCRIPTION", Type.GetType("System.String"))
                'tblHeader.Columns.Add(colDescription)
                DV = tblHeader.DefaultView()
                'If DV.Count > 0 Then
                '    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                '             "SELECT BB.BRANDPACK_NAME,SB.SPPB_QTY FROM BRND_BRANDPACK BB INNER JOIN ORDR_PO_BRANDPACK OPB " & vbCrLf & _
                '             " ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                '             " INNER JOIN SPPB_BRANDPACK SB ON OOAB.OA_BRANDPACK_ID = SB.OA_BRANDPACK_ID WHERE SB.SPPB_NO = @SPPB_NO ;"
                '    Me.ResetCommandText(CommandType.Text, Query)
                '    For i As Integer = 0 To DV.Count - 1
                '        Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, DV(i)("SPPB_NO"), 15)
                '        Dim tblDetail As New DataTable("T_Detail") : tblDetail.Clear()
                '        Me.SqlDat.Fill(tblDetail) : Me.ClearCommandParameters()
                '        If tblDetail.Rows.Count > 0 Then
                '            DV(i).BeginEdit()
                '            Dim Description As String = "Items{("
                '            For i1 As Integer = 0 To tblDetail.Rows.Count - 1
                '                Description &= tblDetail.Rows(i1)("BRANDPACK_NAME").ToString() & ",Qty = " & String.Format("{0:#,##0.000}", Convert.ToDecimal(tblDetail.Rows(i1)("SPPB_QTY")))
                '                If i1 < tblDetail.Rows.Count - 1 Then
                '                    Description &= "),("
                '                Else
                '                    Description &= ")}"
                '                End If
                '            Next
                '            DV(i)("DESCRIPTION") = Description : DV(i).EndEdit()
                '        End If

                '    Next
                'End If
                Me.CloseConnection()
                Return DV
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function IsValidDistributorDescription(ByVal distributorID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_Chek_Validity_Distributor", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Chek_Validity_Distributor")
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar()
                Dim retVal = Me.SqlCom.Parameters("@RETURN_VALUE").Value
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If CInt(retVal) > 0 Then : Return False : Else : Return True : End If

                'If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                '    Return False
                'Else
                '    Return True
                'End If
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function IsExisted(ByVal PO_REF_NO As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_PO_REF_NO", "")
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 35)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return False
        End Function

        Public Function PO_BrandPackExisted(ByVal PO_BRANDPACK_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_CheckPO_BRANDPACK_ID", "")
                Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID, 39)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.VarChar, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function

        Public Function CreateViewDistributor(ByVal mustCloseConnection As Boolean) As DataView
            Try
                Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME,ISNULL(CONTACT,'')AS CONTACT, " & vbCrLf & _
                "ISNULL(PHONE,'') AS PHONE,ISNULL(TERRITORY_AREA,'')AS TERRITORY_AREA,ISNULL(REGIONAL_AREA,'') AS REGIONAL_AREA FROM VIEW_DISTRIBUTOR " & vbCrLf & _
                " WHERE (INACTIVE = 0 OR INACTIVE IS NULL);"
                'Me.CreateCommandSql("", "SELECT * FROM VIEW_DISTRIBUTOR")
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblDistributor As New DataTable("Distributor")
                tblDistributor.Clear()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(tblDistributor) : Me.ClearCommandParameters()
                'Me.FillDataTable(tblDistributor)
                Me.m_ViewDistributor = tblDistributor.DefaultView()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewDistributor
        End Function

        Public Function GetPriceValue(ByVal FilterBrandPackID As String) As Decimal
            Try
                If Not IsNothing(Me.ViewPriceHistory()) Then
                    If Me.ViewPriceHistory.Count = 0 Then
                        Return 0
                    Else
                        Me.ViewPriceHistory.RowFilter = "BRANDPACK_ID = '" + FilterBrandPackID + "'"
                        If Me.ViewPriceHistory.Count = 0 Then
                            Return 0
                        End If
                        If Me.ViewPriceHistory(Me.ViewPriceHistory.Count - 1)("PRICE") Is DBNull.Value Then
                            Return 0
                        End If
                        Me.m_ViewPrice.Sort = "START_DATE DESC"
                    End If
                Else
                    Return 0
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return CDec(Me.ViewPriceHistory()(Me.ViewPriceHistory.Count - 1)("PRICE"))
        End Function

        Public Function GetPriceValue(ByVal BRANDPACK_ID As String, ByVal START_DATE As Object) As Object
            Dim Price As Object
            Try
                Dim START_D As String = CStr("'" & Month(CDate(START_DATE)).ToString() & "/" & Day(CDate(START_DATE)).ToString() & "/" & Year(CDate(START_DATE)).ToString() & "'")
                Me.CreateCommandSql("", "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "' " & _
                                        " AND START_DATE <= " & START_D & " ORDER BY START_DATE DESC")
                Price = Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Price
        End Function
        'Public Function getPriceByProject(ByVal ProjBrandPackID As String, ByVal mustCloseConnection As Boolean) As Object
        '    Try
        '        Query = "SET NOCOUNT ON;" & vbCrLf & _
        '                "SELECT TOP 1 PB.PRICE FROM PROJ_BRANDPACK PB INNER JOIN PROJ_PROJECT PP ON PP.PROJ_REF_NO = PB.PROJ_REF_NO WHERE PB.PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID ; "
        '        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
        '        Else : Me.ResetCommandText(CommandType.Text, Query)
        '        End If
        '        Me.AddParameter("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, ProjBrandPackID, 30)
        '        Me.OpenConnection() : Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
        '        If Not IsNothing(retval) Then
        '            If CDec(retval) > 0 Then
        '                Return retval
        '            End If
        '        End If
        '        If mustCloseConnection Then : Me.CloseConnection() : End If
        '        Return DBNull.Value
        '    Catch ex As Exception
        '        Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
        '    End Try
        'End Function
        'procedure untuk mengambil price bila ada harga khusus
        Public Function GetPriceValue(ByVal distributorID As String, ByVal brandpack_id As String, ByVal PO_DATE As String, ByRef IsPriceHK As Boolean, ByRef DescriptionPrice As String, ByRef PriceTag As String, ByRef isSPrice As Boolean, ByRef IsGenPrice As Boolean) As Object
            Try
                'check apakah distributor ikutan harga khusus diantara waktu stardate
                Dim IsHK As Object = Nothing
                Dim START_DATE As Object = Nothing 'START_DATE PROGRAM
                Dim Price As Object = Nothing
                Dim StartDateString As String = ""
                Dim TargetHK As Object = Nothing
                Dim PriceHK As Object = Nothing
                Query = "SET NOCOUNT ON;SELECT TOP 1 ISHK,START_DATE,ISNULL(TARGET_HK,0) AS TARGET_HK,ISNULL(PRICE_HK,0) AS PRICE_HK " & _
                                 " FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE START_DATE <= '" & PO_DATE & "' AND END_DATE >= '" & PO_DATE & "'" & _
                                 " AND ISHK = 1 AND DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND PROG_BRANDPACK_ID IN(" & _
                                 "SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID " & vbCrLf & _
                                 " AND START_DATE <= '" & PO_DATE & "' AND END_DATE >= '" & PO_DATE & "');"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                'Me.CreateCommandSql("sp_executesql", "")
                'me.AddParameter("@
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, brandpack_id)
                Me.ExecuteReader()
                While Me.SqlRe.Read()
                    IsHK = Me.SqlRe("ISHK") : START_DATE = Me.SqlRe("START_DATE")
                    TargetHK = Me.SqlRe("TARGET_HK") : PriceHK = Me.SqlRe("PRICE_HK")
                End While
                Me.SqlRe.Close()
                Dim retval As Object = Nothing
                If CBool(IsHK) = True Then
                    If CDec(TargetHK) <= 0 Then
                        Me.CloseConnection()
                        Throw New Exception("DISTRIBUTOR_ID " & distributorID & " TARGET_HK = 0")
                    End If
                    IsPriceHK = True
                    StartDateString = Convert.ToDateTime(START_DATE).Month.ToString() + "/" & _
                    Convert.ToDateTime(START_DATE).Day.ToString() + "/" & Convert.ToDateTime(START_DATE).Year.ToString()
                    'jika ya sum po_qty berdasarkan PO_date >= start_Date program  po_date <= startdate
                    Query = "SET NOCOUNT ON;SELECT ISNULL(SUM(PO_ORIGINAL_QTY),0) FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO IN(SELECT PO_REF_NO" & _
                            " FROM ORDR_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND PO_REF_DATE >= '" & StartDateString & _
                            "' AND PO_REF_DATE <= '" & PO_DATE & "') AND BRANDPACK_ID = @BRANDPACK_ID ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Dim SumPOQTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar)
                    If SumPOQTY >= TargetHK Then 'TARGET_TERCAPAI
                        'Me.CloseConnection()
                        'Throw New Exception("TARGET_HK for DistributorID " & distributorID & " has Reached" & vbCrLf & _
                        '"Please Select Another BRANDPACK.")
                        ''ambil ke harga
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                           " SELECT 1 WHERE EXISTS(SELECT BPH.PRICE FROM BRND_PRICE_HISTORY BPH INNER JOIN " & vbCrLf & _
                           " DIST_PLANT_PRICE DPP ON DPP.BRANDPACK_ID = BPH.BRANDPACK_ID " & vbCrLf & _
                           " WHERE BPH.BRANDPACK_ID = @BRANDPACK_ID AND BPH.START_DATE <= '" & PO_DATE & "' " & vbCrLf & _
                           " AND DPP.END_DATE >= '" & PO_DATE & "' " & vbCrLf & _
                           " AND DPP.DISTRIBUTOR_ID = @DISTRIBUTOR_ID);"
                        Me.ResetCommandText(CommandType.Text, Query)
                        retval = Me.SqlCom.ExecuteScalar
                        If Not IsNothing(retval) And Not IsDBNull(retval) Then
                            Me.ClearCommandParameters() : Me.CloseConnection() : isSPrice = True : Return Nothing
                        End If
                        ''sekarang chek apakah BRANDPACK_ID di table general price
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT 1 WHERE EXISTS(SELECT BPH.PRICE FROM BRND_PRICE_HISTORY BPH INNER JOIN " & vbCrLf & _
                                " GEN_PLANT_PRICE GPL ON GPL.BRANDPACK_ID = BPH.BRANDPACK_ID " & vbCrLf & _
                                " WHERE BPH.BRANDPACK_ID = @BRANDPACK_ID AND BPH.START_DATE <= '" & PO_DATE & "' " & vbCrLf & _
                                " AND GPL.START_DATE <= '" & PO_DATE & "') "
                        Me.ResetCommandText(CommandType.Text, Query)
                        retval = Me.SqlCom.ExecuteScalar
                        If Not IsNothing(retval) And Not IsDBNull(retval) Then
                            Me.ClearCommandParameters() : IsGenPrice = True
                            Me.CloseConnection()
                            'DescriptionPrice = "PRICE FROM GENERAL PL PRICE"
                            Return Nothing
                        End If

                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "IF NOT EXISTS(SELECT PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = @BRANDPACK_ID AND START_DATE <= '" & PO_DATE & "') " & vbCrLf & _
                                " SELECT TOP 1 PRICE, PRICE_TAG FROM DIST_PLANT_PRICE WHERE BRANDPACK_ID = @BRANDPACK_ID " & vbCrLf & _
                                " AND DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND START_DATE <= '" & PO_DATE & "' ORDER BY START_DATE DESC;" & vbCrLf & _
                                "ELSE " & vbCrLf & _
                                " SELECT TOP 1 PRICE, PRICE_TAG FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = @BRANDPACK_ID " & vbCrLf & _
                                " AND START_DATE <= '" & PO_DATE & "' ORDER BY START_DATE DESC ;"
                        Me.ResetCommandText(CommandType.Text, Query)

                        Me.SqlRe = Me.SqlCom.ExecuteReader()
                        While Me.SqlRe.Read()
                            Price = Me.SqlRe.GetDecimal(0)
                            PriceTag = Me.SqlRe.GetString(1)
                        End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                        'Price = Me.SqlCom.ExecuteScalar()
                        If IsNothing(Price) Or IsDBNull(Price) Then
                            Price = DBNull.Value
                        End If
                        Me.CloseConnection()
                    Else 'ambil harganya
                        Price = PriceHK : Me.CloseConnection()
                        DescriptionPrice = "PRICE FROM SPECIAL PRICE"
                    End If
                Else
                    ''sekarang chek apakah BRANDPACK_ID di table special price
                    'Query = "SET NOCOUNT ON;" & vbCrLf & _
                    '        " SELECT 1 WHERE EXISTS(SELECT BPH.PRICE FROM BRND_PRICE_HISTORY BPH INNER JOIN " & vbCrLf & _
                    '        " DIST_PLANT_PRICE DPP ON DPP.BRANDPACK_ID = BPH.BRANDPACK_ID" & vbCrLf & _
                    '        " WHERE BPH.BRANDPACK_ID = '" & brandpack_id & "' AND BPH.START_DATE <= '" & PO_DATE & "' " & vbCrLf & _
                    '        " AND DPP.END_DATE >= '" & PO_DATE & "' " & vbCrLf & _
                    '        " AND DPP.DISTRIBUTOR_ID = '" & distributorID & "');"
                    'Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    'If (Not IsNothing(Me.SqlCom.ExecuteScalar)) Then
                    '    Me.ClearCommandParameters() : Me.CloseConnection() : isSPrice = True : Return Nothing
                    'End If
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                             " SELECT 1 WHERE EXISTS(SELECT BPH.PRICE FROM BRND_PRICE_HISTORY BPH INNER JOIN " & vbCrLf & _
                             " DIST_PLANT_PRICE DPP ON DPP.BRANDPACK_ID = BPH.BRANDPACK_ID " & vbCrLf & _
                             " WHERE BPH.BRANDPACK_ID = @BRANDPACK_ID AND BPH.START_DATE <= '" & PO_DATE & "' " & vbCrLf & _
                             " AND DPP.END_DATE >= '" & PO_DATE & "' " & vbCrLf & _
                             " AND DPP.DISTRIBUTOR_ID = @DISTRIBUTOR_ID);"
                    Me.ResetCommandText(CommandType.Text, Query)
                    retval = Me.SqlCom.ExecuteScalar
                    If Not IsNothing(retval) And Not IsDBNull(retval) Then
                        Me.ClearCommandParameters() : Me.CloseConnection() : isSPrice = True : Return Nothing
                    End If

                    ''sekarang chek apakah BRANDPACK_ID di table general price
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT 1 WHERE EXISTS(SELECT BPH.PRICE FROM BRND_PRICE_HISTORY BPH INNER JOIN " & vbCrLf & _
                            " GEN_PLANT_PRICE GPL ON GPL.BRANDPACK_ID = BPH.BRANDPACK_ID " & vbCrLf & _
                            " WHERE BPH.BRANDPACK_ID = @BRANDPACK_ID AND BPH.START_DATE <= '" & PO_DATE & "' " & vbCrLf & _
                            " AND GPL.START_DATE <= '" & PO_DATE & "') "
                    Me.ResetCommandText(CommandType.Text, Query)
                    retval = Me.SqlCom.ExecuteScalar
                    If Not IsNothing(retval) And Not IsDBNull(retval) Then
                        Me.ClearCommandParameters() : IsGenPrice = True
                        Me.CloseConnection()
                        'DescriptionPrice = "PRICE FROM GENERAL PL PRICE"
                        Return Nothing
                    End If

                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "IF NOT EXISTS(SELECT PRICE FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = @BRANDPACK_ID AND START_DATE <= '" & PO_DATE & "') " & vbCrLf & _
                            " SELECT TOP 1 PRICE, PRICE_TAG FROM DIST_PLANT_PRICE WHERE BRANDPACK_ID = @BRANDPACK_ID " & vbCrLf & _
                            " AND DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND START_DATE <= '" & PO_DATE & "' ORDER BY START_DATE DESC;" & vbCrLf & _
                            "ELSE " & vbCrLf & _
                            " SELECT TOP 1 PRICE, PRICE_TAG FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = @BRANDPACK_ID " & vbCrLf & _
                            " AND START_DATE <= '" & PO_DATE & "' ORDER BY START_DATE DESC;"
                    Me.ResetCommandText(CommandType.Text, Query)

                    Me.SqlRe = Me.SqlCom.ExecuteReader()
                    While Me.SqlRe.Read()
                        Price = Me.SqlRe.GetDecimal(0)
                        PriceTag = Me.SqlRe.GetString(1)
                    End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                    If IsNothing(Price) Or IsDBNull(Price) Then
                        Price = DBNull.Value
                    End If : Me.CloseConnection()
                End If
                Return Price
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.ClearCommandParameters() : Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function hasPricePlantation(ByVal distributorID As String, ByVal brandpack_id As String, ByVal PO_DATE As String, ByRef isGenPrice As Boolean, ByRef isSPrice As Boolean) As Boolean
            Try
                'Me.ClearCommandParameters()
                'Query = "SET NOCOUNT ON;" & vbCrLf & _
                '       " SELECT 1 WHERE EXISTS(SELECT TOP 1 BPH.PRICE FROM BRND_PRICE_HISTORY BPH INNER JOIN " & vbCrLf & _
                '       " DIST_PLANT_PRICE DPP ON DPP.BRANDPACK_ID = BPH.BRANDPACK_ID" & vbCrLf & _
                '       " WHERE BPH.BRANDPACK_ID = '" & brandpack_id & "' AND BPH.START_DATE <= '" & PO_DATE & "' " & vbCrLf & _
                '       " AND DPP.DISTRIBUTOR_ID = '" & distributorID & "' ORDER BY BPH.START_DATE DESC);"
                'If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                'Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                'End If
                'Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.OpenConnection()
                'If (Not IsNothing(Me.SqlCom.ExecuteScalar)) Then
                '    Me.ClearCommandParameters() : Me.CloseConnection() : Return True
                'End If
                'Me.ClearCommandParameters() : Me.CloseConnection()

                ''sekarang chek apakah BRANDPACK_ID di table special price
                'Query = "SET NOCOUNT ON;" & vbCrLf & _
                '        " SELECT 1 WHERE EXISTS(SELECT BPH.PRICE FROM BRND_PRICE_HISTORY BPH INNER JOIN " & vbCrLf & _
                '        " DIST_PLANT_PRICE DPP ON DPP.BRANDPACK_ID = BPH.BRANDPACK_ID" & vbCrLf & _
                '        " WHERE BPH.BRANDPACK_ID = '" & brandpack_id & "' AND BPH.START_DATE <= '" & PO_DATE & "' " & vbCrLf & _
                '        " AND DPP.END_DATE >= '" & PO_DATE & "' " & vbCrLf & _
                '        " AND DPP.DISTRIBUTOR_ID = '" & distributorID & "');"
                'Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'If (Not IsNothing(Me.SqlCom.ExecuteScalar)) Then
                '    Me.ClearCommandParameters() : Me.CloseConnection() : isSPrice = True : Return Nothing
                'End If
                Dim retval As Object = Nothing
                Me.ClearCommandParameters()
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                         " SELECT 1 WHERE EXISTS(SELECT BPH.PRICE FROM BRND_PRICE_HISTORY BPH INNER JOIN " & vbCrLf & _
                         " DIST_PLANT_PRICE DPP ON DPP.BRANDPACK_ID = BPH.BRANDPACK_ID " & vbCrLf & _
                         " WHERE BPH.BRANDPACK_ID = @BRANDPACK_ID AND BPH.START_DATE <= '" & PO_DATE & "' " & vbCrLf & _
                         " AND DPP.END_DATE >= '" & PO_DATE & "' " & vbCrLf & _
                         " AND DPP.DISTRIBUTOR_ID = @DISTRIBUTOR_ID);"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If

                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, brandpack_id)
                Me.OpenConnection()
                retval = Me.SqlCom.ExecuteScalar
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Me.ClearCommandParameters() : Me.CloseConnection() : isSPrice = True : Return True
                End If

                ''sekarang chek apakah BRANDPACK_ID di table general price
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT 1 WHERE EXISTS(SELECT BPH.PRICE FROM BRND_PRICE_HISTORY BPH INNER JOIN " & vbCrLf & _
                        " GEN_PLANT_PRICE GPL ON GPL.BRANDPACK_ID = BPH.BRANDPACK_ID " & vbCrLf & _
                        " WHERE BPH.BRANDPACK_ID = @BRANDPACK_ID AND BPH.START_DATE <= '" & PO_DATE & "' " & vbCrLf & _
                        " AND GPL.START_DATE <= '" & PO_DATE & "') "
                Me.ResetCommandText(CommandType.Text, Query)
                retval = Me.SqlCom.ExecuteScalar
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Me.ClearCommandParameters() : isGenPrice = True
                    Me.CloseConnection()
                    'DescriptionPrice = "PRICE FROM GENERAL PL PRICE"
                    Return True
                End If

            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return False
        End Function
        Public Function GetTotalPOQTyByHK(ByVal DistributorID As String, ByVal BrandPackID As String, ByVal PODate As String, ByRef TargetHK As Decimal, Optional ByVal POBrandPackID As String = "") As Decimal
            Dim IsHK As Object = Nothing
            Dim START_DATE As Object = Nothing 'START_DATE PROGRAM
            Dim StartDateString As String = ""
            'Dim TargetHK As Object = Nothing
            Dim retval As Decimal = 0
            Try
                Query = "SET NOCOUNT ON;SELECT TOP 1 ISHK,START_DATE,ISNULL(TARGET_HK,0) AS TARGET_HK " & _
                           " FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE START_DATE <= '" & PODate & "' AND END_DATE >= '" & PODate & "'" & _
                           " AND ISHK = 1 AND DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND PROG_BRANDPACK_ID IN(" & _
                           "SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID AND START_DATE <= '" & PODate & "' AND END_DATE >= '" & PODate & "');"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID)
                Me.ExecuteReader()
                While Me.SqlRe.Read()
                    IsHK = Me.SqlRe("ISHK") : START_DATE = Me.SqlRe("START_DATE")
                    TargetHK = Me.SqlRe("TARGET_HK")
                End While
                Me.SqlRe.Close()
                If CBool(IsHK) = True Then
                    StartDateString = Convert.ToDateTime(START_DATE).Month.ToString() + "/" & _
                                      Convert.ToDateTime(START_DATE).Day.ToString() + "/" & Convert.ToDateTime(START_DATE).Year.ToString()
                    If POBrandPackID = "" Then
                        Query = "SET NOCOUNT ON;SELECT ISNULL(SUM(PO_ORIGINAL_QTY),0) FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = ANY(SELECT PO_REF_NO" & _
                                 " FROM ORDR_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND PO_REF_DATE >= '" & StartDateString & _
                                 "' ) AND BRANDPACK_ID = @BRANDPACK_ID ;"
                    Else
                        Query = "SET NOCOUNT ON;SELECT ISNULL(SUM(PO_ORIGINAL_QTY),0) FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = ANY(SELECT PO_REF_NO" & _
                                 " FROM ORDR_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND PO_REF_DATE >= '" & StartDateString & _
                                 "' ) AND BRANDPACK_ID = @BRANDPACK_ID AND PO_BRANDPACK_ID != @PO_BRANDPACK_ID ;"
                        Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, POBrandPackID)
                    End If
                    Me.ResetCommandText(CommandType.Text, Query)
                    Dim SumPOQTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar) : Me.ClearCommandParameters()
                    If SumPOQTY < TargetHK Then 'TARGET_TERCAPAI
                        Return SumPOQTY
                    End If
                End If
                Return retval
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function CreateViewPriceHistory(ByVal DistributorID As String, ByVal PRICE_START_DATE As Date) As DataView
            Try
                Me.CreateCommandSql("Sp_Select_Price_ByDIST_ID", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                Me.AddParameter("@PRICE_START_DATE", SqlDbType.DateTime, PRICE_START_DATE)
                Dim tblPrice As New DataTable("Price")
                tblPrice.Clear()
                Me.FillDataTable(tblPrice)
                Me.m_ViewPrice = tblPrice.DefaultView()
                Me.m_ViewPrice.Sort = "START_DATE DESC"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewPrice
        End Function
        Public Function GetBrandPackByProject(ByVal PROJ_REF_NO As String, ByVal mustCloseConnection As Boolean) As DataView
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT PB.PROJ_BRANDPACK_ID,PB.BRANDPACK_ID,PB.PRICE,BP.BRANDPACK_NAME FROM PROJ_BRANDPACK PB INNER JOIN BRND_BRANDPACK BP ON PB.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                        " WHERE PB.PROJ_REF_NO = @PROJ_REF_NO ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, PROJ_REF_NO, 15)
                Dim tblBrandPackProject As New DataTable("T_BProject") : tblBrandPackProject.Clear()
                Me.OpenConnection() : Me.setDataAdapter(Me.SqlCom).Fill(tblBrandPackProject) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return tblBrandPackProject.DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function CreateViewPriceHistoryByPROJ_REF_NO(ByVal PROJ_REF_NO As String, ByVal PRICE_START_DATE As Date) As DataView
            Try
                Me.CreateCommandSql("Sp_Select_Price_ByPROJ_REF_NO", "")

                'If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("",")

                Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, PROJ_REF_NO, 10)
                Me.AddParameter("@PRICE_START_DATE", SqlDbType.DateTime, PRICE_START_DATE)
                Dim tblPrice As New DataTable("Price")
                tblPrice.Clear()
                Me.FillDataTable(tblPrice)
                Me.m_ViewPrice = tblPrice.DefaultView()
                Me.m_ViewPrice.Sort = "START_DATE DESC"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewPrice
        End Function

        Public Function CreateViewBrandPackByDistributorID(ByVal DistributorID As String, ByVal PO_DATE As Object, ByVal mustCloseConnection As Boolean) As DataView
            Try
                If IsNothing(Me.SqlCom) Then : CreateCommandSql("Usp_SelectBPInclude_WITHPARAM_DISTRIBUTOR_ID_AGREEMENT_STILL_APLY", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_SelectBPInclude_WITHPARAM_DISTRIBUTOR_ID_AGREEMENT_STILL_APLY")
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                Me.AddParameter("@PO_DATE", SqlDbType.DateTime, PO_DATE)
                Dim tblBrandPack As New DataTable("BrandPack") : tblBrandPack.Clear()
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(tblBrandPack) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                'Me.FillDataTable(tblBrandPack)
                Me.m_ViewBrandPack = tblBrandPack.DefaultView()
                Me.m_ViewBrandPack.Sort = "BRANDPACK_ID"
                Me.m_ViewBrandPack_1 = tblBrandPack.DefaultView()
                Me.m_ViewBrandPack_1.Sort = "BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewBrandPack
        End Function

        Public Function CreateViewPOBrandPack(ByVal PO_REF_NO As String, ByVal mustCloseConection As Boolean) As DataView
            Try
                Dim dtTable As New DataTable("T_PO")
                If String.IsNullOrEmpty(PO_REF_NO) Then
                    With dtTable
                        .Columns.Add("PO_REF_NO", Type.GetType("System.String"))
                        .Columns.Add("PO_BRANDPACK_ID", Type.GetType("System.String"))
                        .Columns("PO_BRANDPACK_ID").Unique = True
                        Dim Key(1) As DataColumn : Key(0) = dtTable.Columns("PO_BRANDPACK_ID")
                        dtTable.PrimaryKey = Key
                        .Columns.Add("BRANDPACK_ID", Type.GetType("System.String"))
                        .Columns.Add("PO_PRICE_PERQTY", Type.GetType("System.Decimal"))
                        .Columns.Add("PO_ORIGINAL_QTY", Type.GetType("System.Decimal"))
                        .Columns.Add("TOTAL", Type.GetType("System.Decimal"))
                        .Columns.Add("PROJ_BRANDPACK_ID", Type.GetType("System.String"))
                        .Columns.Add("PLANTATION_ID", Type.GetType("System.String"))
                        .Columns("PLANTATION_ID").DefaultValue = DBNull.Value
                        .Columns.Add("PRICE_TAG", Type.GetType("System.String"))
                        .Columns.Add("TERRITORY_ID", Type.GetType("System.String"))
                        .Columns("TERRITORY_ID").DefaultValue = DBNull.Value
                        .Columns.Add("DESCRIPTIONS", Type.GetType("System.String"))
                        .Columns.Add("DESCRIPTIONS2", Type.GetType("System.String"))
                        .Columns.Add("PRICE_CATEGORY", Type.GetType("System.String"))
                        .Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"))
                        .Columns.Add("CREATE_BY", Type.GetType("System.String"))
                        .Columns.Add("MODIFY_DATE", Type.GetType("System.DateTime"))
                        .Columns.Add("MODIFY_BY", Type.GetType("System.String"))

                    End With
                    dtTable.Rows.Clear()
                Else
                    dtTable.Clear() : Me.OpenConnection()
                    Query = "SET NOCOUNT ON;SELECT *,[PO_ORIGINAL_QTY] * [PO_PRICE_PERQTY] AS TOTAL FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = '" & PO_REF_NO & "';"
                    'Me.FillDataTable("", "SELECT *,[PO_ORIGINAL_QTY] * [PO_PRICE_PERQTY] AS TOTAL FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = '" & PO_REF_NO & "'", "T_PO_BrandPack")
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                    Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    End If
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                    If mustCloseConection Then : Me.CloseConnection() : End If
                    'Me.FillDataTable(dtTable)
                End If
                Me.dsPPBrandPack = New DataSet("DS_PO") : Me.dsPPBrandPack.Clear()
                Me.dsPPBrandPack.Tables.Add(dtTable)
                'Me.dsPPBrandPack = Me.baseDataSet
                Me.m_ViewPOBrandPack = Me.dsPPBrandPack.Tables(0).DefaultView()
                Me.m_ViewPOBrandPack.Sort = "PO_BRANDPACK_ID"
                'Me.m_ViewPOBrandPack.RowStateFilter = DataViewRowState.CurrentRows
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return Me.m_ViewPOBrandPack
        End Function
        Public Sub DeletePO(ByVal PO_REF_NO As String, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "IF EXISTS(SELECT PO_REF_NO FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = @PO_REF_NO) " & vbCrLf & _
                        " BEGIN RAISERROR('Can not delete data',16,1) ; RETURN ; END " & vbCrLf & _
                        " DELETE FROM ORDR_ORDER_ACCEPTANCE WHERE PO_REF_NO = @PO_REF_NO ;" & vbCrLf & _
                        " DELETE FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = @PO_REF_NO ; " 
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else
                    Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.ClearCommandParameters() : Me.CloseConnection() : Throw ex
            End Try
        End Sub
        Public Function DeletePO_BRANDPACK(ByVal PO_REF_NO As String, ByVal PO_BRANDPACK_ID As String) As Integer
            Try
                'Me.GetConnection()
                ''DELETE DULU OA_BRANDPACK_DISC BERDASARKAN OA_BRANDPACK_ID
                Query = "SET NOCOUNT ON;SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID;"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID)
                Dim ListOABrandPack As New List(Of String)
                Me.ExecuteReader()
                While Me.SqlRe.Read
                    ListOABrandPack.Add(SqlRe.Item(0).ToString())
                End While
                Me.SqlRe.Close()
                Me.ClearCommandParameters()
                Me.BeginTransaction()
                If ListOABrandPack.Count > 0 Then
                    For i As Integer = 0 To ListOABrandPack.Count - 1
                        Dim OA_BRANDPACK_ID As String = ListOABrandPack(i).ToString()
                        ''DELETE OA_BRANDPACKNYA DULU
                        Me.SqlCom.CommandText = "SET NOCOUNT ON;IF EXISTS(SELECT DISC_QTY FROM ORDR_OA_BRANDPACK_DISC WHERE GQSY_SGT_P_FLAG != 'RMOA'" & _
                                                " AND OA_BRANDPACK_ID = @OA_BRANDPACK_ID AND GQSY_SGT_P_FLAG IN('Q1','Q2','Q3','Q4','S1','S2','Y') AND DISC_QTY > 0) " & vbCrLf & _
                                                " BEGIN " & vbCrLf _
                                                & " RAISERROR('Please delete Agreement Discount in OA first before deleting this PO',16,1);RETURN;" & vbCrLf _
                                                & "END " & vbCrLf _
                                                & "IF EXISTS(SELECT QTY FROM ORDR_OA_REMAINDING WHERE QTY > 0 AND FLAG IN('Q1','Q2','Q3','Q4','S1','S2','Y') " & vbCrLf _
                                                & " AND OA_BRANDPACK_ID = @OA_BRANDPACK_ID) " & vbCrLf _
                                                & " BEGIN " & vbCrLf _
                                                & " RAISERROR('Remainding Agreement Discount in OA_Remainding must be deleted too',16,1);RETURN;" & vbCrLf _
                                                & " END" & vbCrLf _
                                                & "DELETE FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;" _
                                                & vbCrLf & "DELETE FROM DO_TP_DETAIL WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;" _
                                                & vbCrLf & "DELETE FROM OTP_DETAIL WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;" _
                                                & vbCrLf & "DELETE FROM SPPB_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;" _
                                                & vbCrLf & "DELETE FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;" _
                                                & vbCrLf & "DELETE FROM MRKT_DISC_HISTORY WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;" _
                                                & vbCrLf & "DELETE FROM AGREE_DISC_HISTORY WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;" _
                                                & vbCrLf & "DELETE FROM ORDR_OA_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;"
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID)
                        Me.SqlCom.CommandType = CommandType.Text : Me.SqlCom.Transaction = Me.SqlTrans
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next
                    Me.SqlCom.CommandText = "SET NOCOUNT ON; SELECT TOP 1 DO_TP_NO FROM DO_TP_HEADER DTH WHERE NOT EXISTS(SELECT DO_TP_NO FROM DO_TP_DETAIL WHERE DO_TP_NO " _
                                          & " = DTH.DO_TP_NO)OPTION(KEEP PLAN);"
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO)
                    Dim DO_TP_NO As Object = Me.SqlCom.ExecuteScalar()
                    Me.SqlCom.CommandText = "SET NOCOUNT ON; SELECT TOP 1 OTP_NO FROM ORDR_OTP_HEADER OTH WHERE NOT EXISTS (SELECT OTP_NO FROM OTP_DETAIL" _
                                          & " WHERE OTP_NO = OTH.OTP_NO)OPTION(KEEP PLAN);"
                    Dim OTP_NO = Me.SqlCom.ExecuteScalar()
                    Me.SqlCom.CommandText = "SET NOCOUNT ON; SELECT TOP 1 SPPB_NO FROM SPPB_HEADER SH WHERE NOT EXISTS(SELECT SPPB_NO FROM SPPB_BRANDPACK " _
                                            & " WHERE SPPB_NO = SH.SPPB_NO)OPTION(KEEP PLAN);"
                    Dim SPPB_NO As Object = Me.SqlCom.ExecuteScalar()
                    Me.ClearCommandParameters() : Query = ""
                    If (Not IsNothing(DO_TP_NO)) Then
                        Query = "SET NOCOUNT ON;DELETE FROM DO_TP_HEADER WHERE DO_TP_NO = @DO_TP_NO;"
                        Me.AddParameter("@DO_TP_NO", SqlDbType.VarChar, DO_TP_NO)
                    End If
                    If (Not IsNothing(OTP_NO)) Then
                        Query &= "DELETE FROM ORDR_OTP_HEADER WHERE OTP_NO = @OTP_NO;"
                        Me.AddParameter("@OTP_NO", SqlDbType.VarChar, OTP_NO)
                    End If
                    If (Not IsNothing(SPPB_NO)) Then
                        Query &= "DELETE FROM SPPB_HEADER WHERE SPPB_NO = @SPPB_NO;"
                        Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO)
                    End If
                    Query = Query & vbCrLf _
                          & "DELETE FROM ORDR_PO_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID;" & vbCrLf _
                          & " IF NOT EXISTS(SELECT PO_REF_NO FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = @PO_REF_NO)" & vbCrLf _
                          & " BEGIN " & vbCrLf _
                          & " DELETE FROM DO_TP_HEADER WHERE OTP_NO IN(SELECT OTP_NO" _
                          & " FROM ORDR_OTP_HEADER WHERE SPPB_NO IN(SELECT SPPB_NO FROM SPPB_HEADER" _
                          & " WHERE PO_REF_NO = @PO_REF_NO));" & vbCrLf _
                          & " DELETE FROM ORDR_OTP_HEADER WHERE SPPB_NO IN(SELECT SPPB_NO FROM SPPB_HEADER" _
                          & " WHERE PO_REF_NO = @PO_REF_NO);" & vbCrLf _
                          & " DELETE FROM SPPB_HEADER WHERE PO_REF_NO = @PO_REF_NO;" & vbCrLf _
                          & " DELETE FROM ORDR_ORDER_ACCEPTANCE WHERE PO_REF_NO = @PO_REF_NO; " & vbCrLf _
                          & " DELETE FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = @PO_REF_NO; " & vbCrLf _
                          & " END"
                    Me.SqlCom.CommandText = Query
                    Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID)
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO)
                    Me.SqlCom.ExecuteScalar()
                Else

                    Me.SqlCom.CommandText = "SET NOCOUNT ON;DELETE FROM ORDR_PO_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID;" & vbCrLf _
                                            & "IF NOT EXISTS(SELECT PO_BRANDPACK_ID FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = @PO_REF_NO)" & vbCrLf _
                                            & " BEGIN " & vbCrLf _
                                            & " DELETE FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = @PO_REF_NO" & vbCrLf _
                                            & " END"
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO)
                    Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID)
                    Me.SqlCom.CommandType = CommandType.Text : Me.SqlCom.Transaction = Me.SqlTrans
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                Me.CommiteTransaction() : Me.CloseConnection() : Return 1
            Catch ex As DBConcurrencyException
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw New System.Exception(Me.MessageDBConcurency)
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function DeletePOBrandPack(ByVal PO_BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As Integer
            Try
                Query = "SET NOCOUNT ON;DECLARE @V_PO_REF_NO VARCHAR(30);" & _
                        "SET @V_PO_REF_NO = (SELECT TOP 1 PO_REF_NO FROM ORDR_PO_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID);" & vbCrLf & _
                        "IF EXISTS(SELECT PO_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID)" & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " RAISERROR('OA has been made,Please delete OA before deleting this PO',16,1);RETURN;" & vbCrLf & _
                        " END " & vbCrLf & _
                        "DELETE FROM ORDR_PO_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID;" & vbCrLf & _
                        "IF NOT EXISTS(SELECT PO_REF_NO FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = @V_PO_REF_NO) " & vbCrLf & _
                        "BEGIN " & vbCrLf & _
                        " DELETE FROM ORDR_ORDER_ACCEPTANCE WHERE PO_REF_NO = @V_PO_REF_NO ;" & vbCrLf & _
                        "END"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else
                    Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return 1
            Catch ex As Exception
                Me.ClearCommandParameters() : Me.CommiteTransaction() : Throw ex
            End Try
        End Function

        Public Function IsHasReferencceIDOA(ByVal listPOBrandPack As List(Of String)) As Boolean
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                "SELECT 1 WHERE EXISTS(SELECT PO_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.OpenConnection()
                For i As Integer = 0 To listPOBrandPack.Count - 1
                    Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, listPOBrandPack(i), 39)
                    Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If Not IsNothing(retval) Then
                        If CInt(retval) > 0 Then
                            Me.CloseConnection() : Return True
                        End If
                    End If
                Next
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function HasReferencedData(ByVal PO_REF_NO As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_REFERENCED_PO", "")
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return False
        End Function
        Public Function getCreateBy(ByVal PO_REF_NO As String) As String
            Try
                Query = "SET NOCOUNT ON ; " & vbCrLf & _
                        "SELECT CREATE_BY FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = @PO_REF_NO ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                Dim retval As Object = Me.ExecuteScalar()
                If Not IsNothing(retval) Then
                    Return retval.ToString()
                End If
                Return ""
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function PO_BRANDPACK_HasReferencedData(ByVal PO_BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                "SELECT 1 WHERE EXISTS(SELECT TOP 1 PO_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID) ; "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID, 40)
                Me.OpenConnection()
                Dim result As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(result) Then
                    If Convert.ToInt32(result) > 0 Then
                        Me.CloseConnection() : Return True
                    End If
                End If
                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Select_REFERENCED_PO_BRANDPACK")
                Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID, 39)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                Me.SqlCom.ExecuteScalar()
                Dim retval As Object = Me.SqlCom.Parameters()("@RETURN_VALUE").Value
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then
                        Me.CloseConnection() : Me.ClearCommandParameters() : Return True
                    End If
                End If
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : Me.ClearCommandParameters() : End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return False
        End Function

        Public Function CreateViewActivePO(ByRef DVDistributor As DataView, ByVal DistributorID As String) As DataView
            Try
                'Me.SearcData("Usp_GetView_Actice_PO", "")
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_GetView_Actice_PO", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_GetView_Actice_PO")
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                Dim T_ActivePO As New DataTable("Active PO")
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection() : Me.SqlDat.Fill(T_ActivePO) : Me.ClearCommandParameters()
                If IsNothing(DVDistributor) Then
                    Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR " & vbCrLf & _
                            " WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID);"
                    Me.SqlCom.CommandText = "sp_executesql" : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Dim DtDistributor As New DataTable("T_Distributor") : DtDistributor.Clear() : Me.SqlDat.Fill(DtDistributor)
                    Me.ClearCommandParameters() : DVDistributor = DtDistributor.DefaultView
                End If
                Me.CloseConnection() : Me.m_ViewActivePO = T_ActivePO.DefaultView
                'Me.FillDataTable(T_ActivePO)
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewActivePO
        End Function

        Public Function CheckPOBrandPackIDByPOREfNo(ByVal PO_REF_NO As String) As Boolean
            Try
                Me.CreateCommandSql("", "")
            Catch ex As Exception

            End Try
        End Function
        Public Function Getcount(ByVal PO_REF_NO As String, ByVal mustCloseConnection As Boolean) As Integer
            Try
                Query = "SET NOCOUNT ON ; SELECT COUNT(PO_BRANDPACK_ID) FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = '" + PO_REF_NO + "'"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Dim retval As Object = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return CInt(retval)
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getTotalOAQtyByPOBrandPack(ByVal PO_BRANDPACK_ID As String) As Decimal
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "IF EXISTS(SELECT PO_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID) " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "SELECT ISNULL(SUM(OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID ;" & vbCrLf & _
                        " END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID, 40)
                Dim result As Object = Me.ExecuteScalar()
                If Not IsNothing(result) Then
                    Return Convert.ToDecimal(result)
                End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return 0
        End Function
        Public Function getPOHeader(ByVal SearchPOHeader As String) As DataView
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT TOP 100 PO_REF_NO,PO_REF_DATE FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO LIKE '%" & SearchPOHeader & "%' ORDER BY PO_REF_DATE DESC ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Return Me.FillDataTable(New DataTable("T_POHeader")).DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function getPODateAndDistributorID(ByVal PO_REF_NO As String) As DataTable
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT TOP 1 PO_REF_DATE,DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = '" & PO_REF_NO & "' ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Return Me.FillDataTable(New DataTable("t_PO"))
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function IsHasAgreementDiscount(ByVal PO_BRANDPACK_ID As String) As Boolean
            Try
                Query = "SET NOCOUNT ON;IF EXISTS(SELECT TOP 1 DISC_QTY FROM ORDR_OA_BRANDPACK_DISC WHERE GQSY_SGT_P_FLAG != 'RMOA'" & _
                        "  AND GQSY_SGT_P_FLAG IN('Q1','Q2','Q3','Q4','S1','S2','Y') AND DISC_QTY > 0 " & vbCrLf _
                        & " AND OA_BRANDPACK_ID IN(SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID))" & vbCrLf _
                        & " BEGIN " & vbCrLf _
                        & " RAISERROR('Please delete Agreement Discount " & vbCrLf & "In OA Discount before cancelling this PO',16,1);RETURN;" & vbCrLf _
                        & "END " & vbCrLf _
                        & "IF EXISTS(SELECT TOP 1 QTY FROM ORDR_OA_REMAINDING WHERE QTY > 0 AND FLAG IN('Q1','Q2','Q3','Q4','S1','S2','Y') " & vbCrLf _
                         & " AND OA_BRANDPACK_ID IN(SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID))" & vbCrLf _
                        & " BEGIN " & vbCrLf _
                        & " RAISERROR('Remainding Agreement Discount must be deleted too',16,1);RETURN;" & vbCrLf _
                        & " END"

                Me.CreateCommandSql("", Query) : Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID, 40)
                Dim DiscQTY As Object = Me.ExecuteScalar()
                If (IsNothing(DiscQTY)) Then
                    Return False
                End If
                Return True
            Catch ex As Exception
                Me.ClearCommandParameters() : Me.CloseConnection() : Throw ex
            End Try
        End Function
        Public Function isHasRemainFromOtherOA(ByVal PO_BRANDPACK_ID As String) As Boolean
            Try
                Query = "SET NOCOUNT ON;IF EXISTS(SELECT OA_RM_ID FROM ORDR_OA_BRANDPACK_DISC" & vbCrLf & _
                          " WHERE OA_BRANDPACK_ID = ANY(SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK " & vbCrLf & _
                          " WHERE PO_BRANDPACK_ID = '" & PO_BRANDPACK_ID & "') " & vbCrLf & _
                          " AND OA_RM_ID = ANY(SELECT OA_RM_ID FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID " & vbCrLf & _
                          " = ANY(SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = '" & PO_BRANDPACK_ID & "'))) " & vbCrLf & _
                          " BEGIN " & vbCrLf & _
                          " RAISERROR('Please delete remaind discount in OA Discount \nWhich its discount is from different PO',16,1);" & vbCrLf & _
                          " RETURN;" & vbCrLf & _
                          " END "
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim res As Object = Me.ExecuteScalar()
                If Not IsNothing(res) Then
                    Throw New Exception("Please delete remaind discount in OA Discount" & vbCrLf & "Which its discount is from different PO")
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function getProjectByDistributor(ByVal PODate As DateTime, ByVal DistributorID As String, ByVal mustCloseConnection As Boolean, Optional ByVal SearString As String = "") As DataView
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT PROJ_REF_NO,PROJECT_NAME,START_DATE,END_DATE FROM PROJ_PROJECT WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND START_DATE <= @PO_DATE " & vbCrLf & _
                        " AND END_DATE >= @PO_DATE "
                If Not String.IsNullOrEmpty(SearString) Then
                    Query &= vbCrLf & "AND PROJECT_NAME LIKE '%'+@SearchString+'%' "
                End If
                Query &= ";"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PODate)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                If Not String.IsNullOrEmpty(SearString) Then
                    Me.AddParameter("@SearchString", SqlDbType.VarChar, SearString, 50)
                End If
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                Dim tblProject As New DataTable("T_Project") : tblProject.Clear()
                Me.OpenConnection()
                Me.SqlDat.Fill(tblProject) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return tblProject.DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

#End Region

#Region " Sub "

        Public Sub InsertSMSTable(ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal lstMESSAGE As List(Of String), ByVal ToTableName As String, ByVal mustCloseConnection As Boolean, Optional ByVal TransactionID As String = "", Optional ByVal MustIncludeTMRSM As Boolean = False)
            Try
                Dim Manager_Territory As Object = Nothing
                Dim Manager_Regional As Object = Nothing
                Dim HPManagerTerritory As Object = Nothing
                Dim HPManagerRegional As Object = Nothing
                Dim REGIONAL_ID As Object = Nothing
                Dim ContactMobile As Object = Nothing
                Dim DISTRIBUTOR_NAME As Object = Nothing
                Dim ContactPerson As Object = Nothing
                Dim TerritoryArea As Object = Nothing
                Dim RegionalArea As Object = Nothing
                Dim Query As String = "SET NOCOUNT ON;SELECT DISTRIBUTOR_NAME,HP,CONTACT FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "';"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteReader()
                While Me.SqlRe.Read()
                    ContactMobile = Me.SqlRe("HP")
                    DISTRIBUTOR_NAME = Me.SqlRe("DISTRIBUTOR_NAME")
                    ContactPerson = Me.SqlRe("CONTACT")
                End While
                Me.SqlRe.Close() : Me.ClearCommandParameters()
                Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                If ToTableName = "SMS_TABLE" Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Insert_SMS_TABLE")
                    For i As Integer = 0 To lstMESSAGE.Count - 1
                        Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25) ' VARCHAR(35),
                        'Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, DBNull.Value, 44) ' VARCHAR(44),
                        'Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 15)
                        'Me.AddParameter("@TABLE_NAME", SqlDbType.VarChar, ToTableName, 20)
                        Me.AddParameter("@CONTACT_PERSON", SqlDbType.VarChar, ContactPerson, 30) ' VARCHAR(30),
                        Me.AddParameter("@ORIGIN_COMPANY", SqlDbType.VarChar, DISTRIBUTOR_NAME) ' VARCHAR(50),
                        Me.AddParameter("@CONTACT_MOBILE", SqlDbType.VarChar, ContactMobile, 20) ' VARCHAR(20),
                        Me.AddParameter("@MESSAGE", SqlDbType.VarChar, lstMESSAGE(i), 200) ' VARCHAR(120),
                        Me.AddParameter("@STATUS_SENT", SqlDbType.Bit, 0) ' BIT,
                        Me.AddParameter("@SENT_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " UPDATE GON_SMS SET STATUS_SENT = 0,SENT_DATE = CURRENT_TIMESTAMP,SENT_BY = @SENT_BY WHERE CAST(TransactionID AS VARCHAR(100)) = @TransactionID ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@SENT_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                    Me.AddParameter("@TransactionID", SqlDbType.VarChar, TransactionID.ToString(), 150)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If

                'If Not withRSMAndTM Then
                '    Me.CommiteTransaction() : If mustCloseConnection Then : Me.CloseConnection() : End If : Return
                'End If
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT TERRITORY_AREA,REGIONAL_ID FROM TERRITORY WHERE TERRITORY_ID" & vbCrLf & _
                        "=(SELECT TOP 1 TERRITORY_ID FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = " & _
                        "(SELECT TOP 1 DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = '" & PO_REF_NO & "')) AND INACTIVE = 0 ;"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    TerritoryArea = Me.SqlRe("TERRITORY_AREA")
                    REGIONAL_ID = Me.SqlRe("REGIONAL_ID")
                End While
                Me.SqlRe.Close() : Me.ClearCommandParameters()
                Me.SqlCom.CommandText = "Usp_Get_TM_Description_By_PO"
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                Dim tblManager As New DataTable("T_Manager") : tblManager.Clear()
                If IsNothing(Me.SqlDat) Then
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                ElseIf Not (Me.SqlDat.SelectCommand Is Me.SqlCom) Then
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                End If : Me.SqlDat.Fill(tblManager) : Me.ClearCommandParameters()
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT HP,REGIONAL_AREA,MANAGER FROM DIST_REGIONAL WHERE REGIONAL_ID = '" & REGIONAL_ID.ToString() & "' AND INACTIVE = 0 ;"
                Me.SqlCom.CommandText = "sp_executesql"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    Manager_Regional = Me.SqlRe("MANAGER")
                    HPManagerRegional = Me.SqlRe("HP")
                    RegionalArea = Me.SqlRe("REGIONAL_AREA")
                End While
                Me.SqlRe.Close() : Me.ClearCommandParameters()
                If MustIncludeTMRSM Then
                    For i As Integer = 0 To tblManager.Rows.Count - 1
                        Me.SqlCom.CommandText = "Usp_Insert_SMS_TABLE"
                        HPManagerTerritory = tblManager.Rows(i)("HP")
                        Manager_Territory = tblManager.Rows(i)("MANAGER")
                        If (Not IsDBNull(HPManagerTerritory)) And (Not IsDBNull(Manager_Territory)) And (Not IsNothing(HPManagerTerritory)) And (Not IsNothing(Manager_Territory)) Then
                            'Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 15)
                            'Me.AddParameter("@TABLE_NAME", SqlDbType.VarChar, "SMS_TABLE", 20)
                            For i2 As Integer = 0 To lstMESSAGE.Count - 1
                                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25) ' VARCHAR(35),
                                Me.AddParameter("@CONTACT_PERSON", SqlDbType.VarChar, Manager_Territory, 30) ' VARCHAR(30),
                                Me.AddParameter("@ORIGIN_COMPANY", SqlDbType.VarChar, "TERRITORY_MANAGER", 50) ' VARCHAR(50),
                                Me.AddParameter("@CONTACT_MOBILE", SqlDbType.VarChar, HPManagerTerritory, 20) ' VARCHAR(20),
                                Me.AddParameter("@MESSAGE", SqlDbType.VarChar, lstMESSAGE(i2), 200) ' VARCHAR(120),
                                Me.AddParameter("@STATUS_SENT", SqlDbType.Bit, 0) ' BIT,
                                Me.AddParameter("@SENT_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Next
                        End If
                    Next
                    If (Not IsDBNull(Manager_Regional)) And (Not IsDBNull(HPManagerRegional)) And (Not IsNothing(Manager_Regional)) And (Not IsNothing(HPManagerRegional)) Then
                        Me.SqlCom.CommandText = "Usp_Insert_SMS_TABLE"
                        For i2 As Integer = 0 To lstMESSAGE.Count - 1
                            Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25) ' VARCHAR(35),
                            Me.AddParameter("@CONTACT_PERSON", SqlDbType.VarChar, Manager_Regional, 30) ' VARCHAR(30),
                            If RegionalArea.ToString().Contains("REGIONAL") Then
                                Me.AddParameter("@ORIGIN_COMPANY", SqlDbType.VarChar, RegionalArea.ToString & " MANAGER") ' VARCHAR(50),
                            Else
                                Me.AddParameter("@ORIGIN_COMPANY", SqlDbType.VarChar, RegionalArea.ToString & " REGIONAL_MANAGER") ' VARCHAR(50),
                            End If
                            Me.AddParameter("@CONTACT_MOBILE", SqlDbType.VarChar, HPManagerRegional, 20) ' VARCHAR(20),
                            Me.AddParameter("@MESSAGE", SqlDbType.VarChar, lstMESSAGE(i2), 200) ' VARCHAR(120),
                            Me.AddParameter("@STATUS_SENT", SqlDbType.Bit, 0) ' BIT,
                            Me.AddParameter("@SENT_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Next
                    End If
                End If
                'Me.CloseConnection()
                Me.CommiteTransaction() : If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                If Not Me.SqlRe.IsClosed() Then
                    Me.SqlRe.Close()
                End If
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Private Sub UpdateOABrandPack(ByVal OA_BrandPack_ID As String)
            Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; DECLARE @V_SPPB_QTY DECIMAL(18,3);" _
            & vbCrLf & " DELETE FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;" _
            & vbCrLf & "IF EXISTS(SELECT OA_BRANDPACK_ID FROM DO_TP_DETAIL WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID AND " _
            & " DO_TP_QTY > 0)" _
            & vbCrLf & " BEGIN " _
            & vbCrLf & " UPDATE DO_TP_DETAIL SET DO_TP_QTY = 0,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = GETDATE() WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID; " _
            & vbCrLf & " END " _
            & vbCrLf & "IF EXISTS(SELECT OA_BRANDPACK_ID FROM OTP_DETAIL WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID AND OTP_QTY > 0) " _
            & vbCrLf & " BEGIN " _
            & vbCrLf & " UPDATE OTP_DETAIL SET OTP_QTY = 0,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = GETDATE() WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID; " _
            & vbCrLf & " END " _
            & vbCrLf & "IF EXISTS(SELECT OA_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID AND SPPB_QTY > 0)" _
            & vbCrLf & " BEGIN " _
            & vbCrLf & " SET @V_SPPB_QTY = (SELECT SPPB_QTY FROM SPPB_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID);" _
            & vbCrLf & " UPDATE SPPB_BRANDPACK SET SPPB_QTY = 0,GON_1_NO = NULL,GON_1_QTY = 0,GON_1_DATE = NULL," _
            & vbCrLf & " GON_2_NO = NULL,GON_2_QTY = 0,GON_2_DATE = NULL,GON_3_NO = NULL,GON_3_QTY = 0,GON_3_DATE = NULL," _
            & vbCrLf & "GON_4_NO = NULL,GON_4_QTY = 0,GON_4_DATE = NULL,GON_5_NO = NULL,GON_5_QTY = 0,GON_5_DATE = NULL," _
            & vbCrLf & "GON_6_NO = NULL,GON_6_QTY = 0,GON_6_DATE = NULL,STATUS = 'CANCELED',BALANCE = 0,ISREVISION = 1," _
            & vbCrLf & " REMARK = 'LAST_QUANTITY =  ' + CAST(@V_SPPB_QTY AS VARCHAR(100)) + ' Modified by ' + @MODIFY_BY,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = GETDATE()" _
            & vbCrLf & " WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID; " _
            & vbCrLf & " END " _
            & vbCrLf & "DELETE FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;" _
            & vbCrLf & "DELETE FROM MRKT_DISC_HISTORY WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;" _
            & vbCrLf & "DELETE FROM AGREE_DISC_HISTORY WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;" _
            & vbCrLf & "UPDATE ORDR_OA_BRANDPACK SET OA_ORIGINAL_QTY = 0,QTY_EVEN = 0,LEFT_QTY = 0," _
            & "AGREE_DISC_QTY = 0,PROG_DISC_QTY = 0,PROJ_DISC_QTY = 0,OTHER_DISC_QTY = 0,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = GETDATE() WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID; " _
            & vbCrLf & "UPDATE GON_DETAIL SET GON_QTY = 0,IsOpen = 0,IsCompleted = 0,ModifiedBy = 'System',ModifiedDate = GETDATE() WHERE SPPB_BRANDPACK_ID = ANY(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID) AND GON_QTY > 0 ;"
            Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BrandPack_ID, 75)
            Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
            Me.SqlCom.CommandType = CommandType.Text : Me.SqlCom.CommandText = Query
            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

            'Query = "SET NOCOUNT  ON; " & vbCrLf & _
            '" IF EXISTS(SELECT SPPB_BRANDPACK_ID FROM GON_DETAIL WHERE SPPB_BRANDPACK_ID = ANY(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID) AND GON_QTY > 0) " & vbCrLf & _
            '" BEGIN UPDATE GON_DETAIL SET GON_QTY = 0 WHERE SPPB_BRANDPACK_ID = ANY(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID) AND GON_QTY > 0 ; END "
        End Sub
        Public Sub CancelPO(ByVal DV As DataView, ByVal mustCloseConnection As Boolean)
            Try
                If (DV.Count <= 0) Then
                    Throw New Exception("No item(s) selected to release") : Return
                End If
                If IsNothing(Me.SqlCom) Then : Me.SqlCom = New SqlCommand() : Me.SqlCom.CommandType = CommandType.Text
                Else : Me.ResetCommandText(CommandType.Text, "")
                End If
                Me.OpenConnection() : Me.BeginTransaction()
                Me.SqlCom.Connection = Me.SqlConn : Me.SqlCom.Transaction = Me.SqlTrans

                'Me.CreateCommandSql("sp_executesql", "") : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                'Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                For i As Integer = 0 To DV.Count - 1
                    Dim PO_ORIGINAL_QTY As Decimal = Convert.ToDecimal(DV(i)("PO_ORIGINAL_QTY"))
                    Dim PO_BRANDPACK_ID As String = DV(i)("PO_BRANDPACK_ID").ToString() : Dim OA_BRANDPACK_IDS As New List(Of String)
                    Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; " & vbCrLf & _
                            "IF EXISTS(SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID)" & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID OPTION(KEEP PLAN); " & vbCrLf & _
                            " END"
                    Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID, 40)
                    Me.SqlCom.CommandText = Query : Me.ExecuteReader()
                    While Me.SqlRe.Read() : OA_BRANDPACK_IDS.Add(Me.SqlRe.GetString(0)) : End While : Me.SqlRe.Close()
                    Me.ClearCommandParameters()
                    For i_4 As Integer = 0 To OA_BRANDPACK_IDS.Count - 1
                        'UPDATE OA_BRANDPACK AND DELETE DISCOUNT NYA
                        UpdateOABrandPack(OA_BRANDPACK_IDS(i_4).ToString())
                    Next
                    'SEKARANG TINGGAL UPDATE PO_BRANDPACK
                    Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON;UPDATE ORDR_PO_BRANDPACK SET PO_ORIGINAL_QTY = 0,DESCRIPTIONS = 'Last ORIGINAL_QTY = " & PO_ORIGINAL_QTY.ToString() & ";Reason = " & DV(i)("REASON").ToString() & "'" & vbCrLf & _
                            ",MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID"
                    Me.AddParameter("PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID, 40)
                    Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.SqlCom.CommandText = Query : Me.SqlCom.CommandType = CommandType.Text
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Next
                'DELETE DATA YANG GAK PERLU/SAMPAH
                Me.SqlCom.CommandType = CommandType.StoredProcedure : Me.SqlCom.CommandText = "sp_executesql"
                Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; " & vbCrLf & _
                         "DELETE FROM DO_TP_HEADER WHERE DO_TP_NO = ANY(SELECT DO_TP_NO FROM DO_TP_HEADER DTH " & vbCrLf & _
                         " WHERE NOT EXISTS(SELECT DO_TP_NO FROM DO_TP_DETAIL WHERE DO_TP_NO = DTH.DO_TP_NO)" & vbCrLf & _
                         ");"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; " & vbCrLf & _
                        "DELETE FROM ORDR_OTP_HEADER WHERE OTP_NO = ANY(SELECT OTP_NO FROM ORDR_OTP_HEADER OTH " & vbCrLf & _
                        " WHERE NOT EXISTS (SELECT OTP_NO FROM OTP_DETAIL WHERE OTP_NO = OTH.OTP_NO) " & vbCrLf & _
                        " AND NOT EXISTS(SELECT OTP_NO FROM DO_TP_HEADER WHERE OTP_NO = OTH.OTP_NO));"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                'Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO)
                Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; " & vbCrLf & _
                        " DELETE FROM SPPB_HEADER WHERE SPPB_NO = ANY(SELECT SPPB_NO FROM SPPB_HEADER SH WHERE NOT EXISTS(SELECT SPPB_NO FROM SPPB_BRANDPACK " _
                      & " WHERE SPPB_NO = SH.SPPB_NO) AND NOT EXISTS(SELECT SPPB_NO FROM ORDR_OTP_HEADER WHERE SPPB_NO = SH.SPPB_NO));"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CommiteTransaction() : If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Sub Delete(ByVal PO_REF_NO As String)
            Try
                Me.GetConnection()
                Me.DeleteData("Sp_Delete_PO", "")
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As DBConcurrencyException
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw New System.Exception("DBConcurency, delete Affects 0 record" & vbCrLf & "Perhaps some user has changed the same data.")
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        'Public Function getGivenPKPPValue(ByVal PROG_BRANDPACK_DIST_ID As String, ByRef targetPKPP As Decimal) As Decimal
        '    Dim ret As Decimal = 0
        '    Try
        '        Query = "SET NOCOUNT ON; " & vbCrLf & _
        '                "SELECT TARGET_PKPP FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_DIST_ID = '" & PROG_BRANDPACK_DIST_ID & "' ;"
        '        If Not IsNothing(Me.SqlCom) Then
        '            Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
        '        Else
        '            Me.CreateCommandSql("sp_executesql", "")
        '        End If
        '        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
        '        Me.OpenConnection()
        '        Dim retTarget As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
        '        If Not IsNothing(retTarget) Then
        '            targetPKPP = retTarget
        '        End If
        '        Query = "SET NOCOUNT ON; " & vbCrLf & _
        '                "SELECT BONUS_VALUE FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_DIST_ID = '" & PROG_BRANDPACK_DIST_ID & "' ;"
        '        Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
        '        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)

        '        Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters() : Me.CloseConnection()
        '        If Not IsNothing(retval) Then
        '            If Convert.ToDecimal(retval) > 0 Then
        '                ret = Convert.ToDecimal(retval)
        '            End If
        '        End If
        '    Catch ex As Exception
        '        Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
        '    End Try
        '    Return ret
        'End Function
        Public Sub SavePO(ByVal SaveMode As String, Optional ByVal dsPOBrandPack As DataSet = Nothing)
            'Dim sqlcom1 As New SqlCommand()
            'Dim sqldat1 As SqlDataAdapter = Nothing
            'Dim sqlComb1 As SqlCommandBuilder = Nothing

            Try
                Me.OpenConnection() : Me.BeginTransaction()
                Select Case SaveMode
                    Case "Update"
                        Me.UpdateData("Sp_Update_PO", "")
                        Me.SqlCom.Transaction = Me.SqlTrans
                        Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, Me.PO_REF_NO, 25) ' VARCHAR(25),
                        Me.AddParameter("@PO_REF_DATE", SqlDbType.DateTime, Me.PO_REF_DATE) ' DATETIME,
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DistributorID, 10) '
                        Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, Me.PROJ_REF_NO, 15)
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30)
                        Me.SqlCom.Transaction = Me.SqlTrans
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Case "Save"
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                               "IF EXISTS(SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER PO WHERE EXISTS(SELECT PO_REF_NO FROM ORDR_PO_BRANDPACK " & vbCrLf & _
                               "          WHERE PO_REF_NO = PO.PO_REF_NO) AND PO_REF_NO = @PO_REF_NO) " & vbCrLf & _
                               "BEGIN RAISERROR('Data Has existed and has referenced-data',16,1); RETURN; END " & vbCrLf & _
                               " DELETE FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = @PO_REF_NO; "
                        Me.CreateCommandSql("", Query)
                        Me.SqlCom.Transaction = Me.SqlTrans
                        Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, Me.PO_REF_NO, 25)
                        Me.SqlCom.ExecuteScalar()
                        Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_PO")
                        'Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, Me.PO_REF_NO, 25) ' VARCHAR(25),
                        Me.AddParameter("@PO_REF_DATE", SqlDbType.DateTime, Me.PO_REF_DATE) ' DATETIME,
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DistributorID, 10) ' VARCHAR(10),
                        Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, Me.PROJ_REF_NO, 15)
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End Select
                'Me.OpenConnection() : Me.BeginTransaction() : Me.ExecuteNonQuery() : Me.ClearCommandParameters()
                If (Me.dsPOBrandPackHasChanged = True) And (Not IsNothing(dsPOBrandPack)) Then
                    Me.SqlDat = New SqlDataAdapter() : Dim InsertedRows() As DataRow = Nothing
                    Dim UpdatedRows() As DataRow = Nothing, DeletedRows() As DataRow = Nothing

                    Dim CommandInsert As SqlCommand = Me.SqlConn.CreateCommand()
                    CommandInsert.CommandText = "SET NOCOUNT ON;INSERT INTO ORDR_PO_BRANDPACK(PO_BRANDPACK_ID,BRANDPACK_ID,PO_REF_NO,PO_ORIGINAL_QTY,PO_PRICE_PERQTY,PRICE_TAG,PROJ_BRANDPACK_ID,DESCRIPTIONS,DESCRIPTIONS2,PLANTATION_ID,TERRITORY_ID,PRICE_CATEGORY,CREATE_BY,CREATE_DATE)" _
                                                & " VALUES(@PO_BRANDPACK_ID,@BRANDPACK_ID,@PO_REF_NO,@PO_ORIGINAL_QTY,@PO_PRICE_PERQTY,@PRICE_TAG,@PROJ_BRANDPACK_ID,@DESCRIPTIONS,@DESCRIPTIONS2,@PLANTATION_ID,@TERRITORY_ID,@PRICE_CATEGORY,@CREATE_BY,@CREATE_DATE);"
                    With CommandInsert
                        .Parameters.Add("@PO_BRANDPACK_ID", SqlDbType.VarChar, 39).SourceColumn = "PO_BRANDPACK_ID"
                        .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14).SourceColumn = "BRANDPACK_ID"
                        .Parameters.Add("@PO_REF_NO", SqlDbType.VarChar, 25).SourceColumn = "PO_REF_NO"
                        .Parameters.Add("@PO_ORIGINAL_QTY", SqlDbType.Decimal, 0).SourceColumn = "PO_ORIGINAL_QTY"
                        .Parameters.Add("@PO_PRICE_PERQTY", SqlDbType.Decimal, 0).SourceColumn = "PO_PRICE_PERQTY"
                        .Parameters.Add("@PRICE_TAG", SqlDbType.VarChar, 150, "PRICE_TAG")
                        .Parameters.Add("@DESCRIPTIONS", SqlDbType.VarChar, 100).SourceColumn = "DESCRIPTIONS"
                        .Parameters.Add("@DESCRIPTIONS2", SqlDbType.VarChar, 100).SourceColumn = "DESCRIPTIONS2"
                        .Parameters.Add("@PLANTATION_ID", SqlDbType.VarChar, 50).SourceColumn = "PLANTATION_ID"
                        .Parameters.Add("@TERRITORY_ID", SqlDbType.VarChar, 50).SourceColumn = "TERRITORY_ID"
                        .Parameters.Add("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, 30, "PROJ_BRANDPACK_ID")
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).SourceColumn = "CREATE_BY"
                        .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime).SourceColumn = "CREATE_DATE"
                        .Parameters.Add("@PRICE_CATEGORY", SqlDbType.Char).SourceColumn = "PRICE_CATEGORY"
                    End With
                    Dim CommandUpdate As SqlCommand = Me.SqlConn.CreateCommand()
                    CommandUpdate.CommandText = "SET NOCOUNT ON;UPDATE ORDR_PO_BRANDPACK SET PO_ORIGINAL_QTY = @PO_ORIGINAL_QTY, " & vbCrLf & _
                                                 " PLANTATION_ID = @PLANTATION_ID,TERRITORY_ID = @TERRITORY_ID,PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID," & vbCrLf & _
                                                 " PO_PRICE_PERQTY = @PO_PRICE_PERQTY, PRICE_TAG = @PRICE_TAG,DESCRIPTIONS = @DESCRIPTIONS,DESCRIPTIONS2 = @DESCRIPTIONS2," & vbCrLf & _
                                                 " PRICE_CATEGORY = @PRICE_CATEGORY,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " _
                                                & " WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID ;"
                    With CommandUpdate
                        .Parameters.Add("@PO_BRANDPACK_ID", SqlDbType.VarChar, 39).SourceColumn = "PO_BRANDPACK_ID"
                        .Parameters("@PO_BRANDPACK_ID").SourceVersion = DataRowVersion.Original
                        .Parameters.Add("@PO_PRICE_PERQTY", SqlDbType.Decimal, 0).SourceColumn = "PO_PRICE_PERQTY"
                        .Parameters.Add("@PRICE_TAG", SqlDbType.VarChar, 150, "PRICE_TAG")
                        .Parameters.Add("@PLANTATION_ID", SqlDbType.VarChar, 50).SourceColumn = "PLANTATION_ID"
                        .Parameters.Add("@TERRITORY_ID", SqlDbType.VarChar, 50).SourceColumn = "TERRITORY_ID"
                        .Parameters.Add("@PO_ORIGINAL_QTY", SqlDbType.Decimal, 0).SourceColumn = "PO_ORIGINAL_QTY"
                        .Parameters.Add("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, 30, "PROJ_BRANDPACK_ID")
                        .Parameters.Add("@DESCRIPTIONS", SqlDbType.VarChar, 100).SourceColumn = "DESCRIPTIONS"
                        .Parameters.Add("@DESCRIPTIONS2", SqlDbType.VarChar, 100, "DESCRIPTIONS2")
                        If (.Parameters()("@DESCRIPTIONS2").Value Is Nothing) Or (.Parameters()("@DESCRIPTIONS2").Value = String.Empty) Then
                            .Parameters()("@DESCRIPTIONS2").Value = DBNull.Value
                        End If
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50)
                        .Parameters("@MODIFY_BY").Value = NufarmBussinesRules.User.UserLogin.UserName
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime).SourceColumn = "MODIFY_DATE"
                        .Parameters.Add("@PRICE_CATEGORY", SqlDbType.Char).SourceColumn = "PRICE_CATEGORY"
                    End With
                    Dim CommandDelete As SqlCommand = Me.SqlConn.CreateCommand()
                    CommandDelete.CommandText = "SET NOCOUNT ON;" & vbCrLf & _
                    "IF EXISTS(SELECT PO_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID) " & vbCrLf & _
                    " BEGIN RAISERROR('Can not delete data because has child referenced-data',16,1);RETURN; END " & vbCrLf & _
                    " DELETE FROM ORDR_PO_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID;"
                    With CommandDelete
                        .Parameters.Add("@PO_BRANDPACK_ID", SqlDbType.VarChar, 39).SourceColumn = "PO_BRANDPACK_ID"
                        .Parameters("@PO_BRANDPACK_ID").SourceVersion = DataRowVersion.Original
                    End With
                    CommandInsert.Transaction = Me.SqlTrans : CommandUpdate.Transaction = Me.SqlTrans
                    CommandDelete.Transaction = Me.SqlTrans
                    Me.SqlDat.InsertCommand = CommandInsert : Me.SqlDat.UpdateCommand = CommandUpdate
                    Me.SqlDat.DeleteCommand = CommandDelete
                    InsertedRows = dsPOBrandPack.Tables(0).Select("", "", DataViewRowState.Added)
                    If (InsertedRows.Length > 0) Then

                        Me.SqlDat.Update(InsertedRows)
                    End If
                    DeletedRows = dsPOBrandPack.Tables(0).Select("", "", DataViewRowState.Deleted)
                    If (DeletedRows.Length > 0) Then
                        Me.SqlDat.Update(DeletedRows)
                    End If
                    UpdatedRows = dsPOBrandPack.Tables(0).Select("", "", DataViewRowState.ModifiedOriginal Or DataViewRowState.ModifiedCurrent)
                    If (UpdatedRows.Length > 0) Then
                        Me.SqlDat.Update(UpdatedRows)
                    End If
                    If SaveMode = "Update" Then
                        If UpdatedRows.Length > 0 Then
                            'Update description di sppb brandpack
                            Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                      " SELECT OPB.PO_BRANDPACK_ID,SB.SPPB_BRANDPACK_ID,SB.REMARK FROM SPPB_BRANDPACK SB INNER JOIN ORDR_OA_BRANDPACK OOAB " & vbCrLf & _
                                      " ON OOAB.OA_BRANDPACK_ID = SB.OA_BRANDPACK_ID INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_BRANDPACK_ID = OOAB.PO_BRANDPACK_ID " & vbCrLf & _
                                      " WHERE OPB.PO_REF_NO = @PO_REF_NO AND SB.SPPB_QTY > 0; "
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                            Dim dtTable As New DataTable("T_SPPB") : dtTable.Clear()
                            Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                            If dtTable.Rows.Count > 0 Then
                                Dim BChanged As Boolean = False
                                dtTable.Columns.Add("IsChanged", Type.GetType("System.Boolean"))
                                dtTable.Columns("IsChanged").DefaultValue = False
                                Dim dv As DataView = dtTable.DefaultView() : dv.Sort = "PO_BRANDPACK_ID DESC "
                                For i As Integer = 0 To UpdatedRows.Length - 1
                                    Dim Index As Integer = dv.Find(UpdatedRows(i)("PO_BRANDPACK_ID"))
                                    If Index <> -1 Then
                                        If Not dv(Index)("REMARK").Equals(UpdatedRows(i)("DESCRIPTIONS")) Then
                                            dv(Index)("REMARK") = UpdatedRows(i)("DESCRIPTIONS") : dv(i)("IsChanged") = True : BChanged = True
                                            dv(Index).EndEdit()
                                        End If
                                    End If
                                Next : dv.Table.AcceptChanges()
                                If BChanged Then
                                    dv.RowFilter = "IsChanged = " & True
                                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                                            "UPDATE SPPB_BRANDPACK SET REMARK = @REMARK,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE WHERE SPPB_BRANDPACK_ID = @SPPB_BRANDPACK_ID ;"
                                    Me.ResetCommandText(CommandType.Text, Query)
                                    For i As Integer = 0 To dv.Count - 1
                                        Me.AddParameter("@REMARK", SqlDbType.VarChar, dv(i)("REMARK"), 150)
                                        Me.AddParameter("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, dv(i)("SPPB_BRANDPACK_ID"), 90)
                                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                                        Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                                    Next
                                End If
                            End If
                        End If
                    End If
                End If
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            Finally
                Me.dsPOBrandPackHasChanged = False
            End Try
        End Sub
        Public Sub DeleteSMS(ByVal TableName As String, ByVal TransactionID As String)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "DELETE FROM " & TableName & " WHERE TransactionID = '" & TransactionID & "' ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
#End Region

#Region " property "
        Public ReadOnly Property ViewBrandpack() As DataView
            Get
                Return Me.m_ViewBrandPack
            End Get
        End Property
        Public Shadows ReadOnly Property ViewDistributor() As DataView
            Get
                Return Me.m_ViewDistributor
            End Get
        End Property
        Public ReadOnly Property ViewPOBrandpack() As DataView
            Get
                Return Me.m_ViewPOBrandPack
            End Get
        End Property
        Public ReadOnly Property ViewProject() As DataView
            Get
                Return Me.m_ViewProject
            End Get
        End Property
        Public ReadOnly Property ViewPriceHistory() As DataView
            Get
                Return Me.m_ViewPrice
            End Get
        End Property
        Public ReadOnly Property ViewBrandPack_1() As DataView
            Get
                Return Me.m_ViewBrandPack_1
            End Get
        End Property
        Public ReadOnly Property ViewPOSMS() As DataView
            Get
                Return Me.m_ViewPOSMS
            End Get
        End Property
        Public ReadOnly Property ViewActivePO() As DataView
            Get
                Return Me.m_ViewActivePO
            End Get
        End Property
#End Region

#Region " Constructor + Destructor "
        Public Sub New()
            Me.DistributorID = ""
            Me.dsPOBrandPackHasChanged = False
            Me.dsPPBrandPack = Nothing
            Me.PO_REF_NO = ""
            Me.PROJ_REF_NO = DBNull.Value
        End Sub
        Public Overloads Sub dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewBrandPack) Then
                Me.m_ViewBrandPack.Dispose()
                Me.m_ViewBrandPack = Nothing
            End If
            If Not IsNothing(Me.m_ViewDistributor) Then
                Me.m_ViewDistributor.Dispose()
                Me.m_ViewDistributor = Nothing
            End If
            If Not IsNothing(Me.m_ViewPOBrandPack) Then
                Me.m_ViewPOBrandPack.Dispose()
                Me.m_ViewPOBrandPack = Nothing
            End If

            If Not IsNothing(Me.m_ViewProject) Then
                Me.m_ViewProject.Dispose()
                Me.m_ViewProject = Nothing
            End If
            If Not IsNothing(Me.dsPPBrandPack) Then
                Me.dsPPBrandPack.Dispose()
                Me.dsPPBrandPack = Nothing
            End If
            If Not IsNothing(Me.m_ViewBrandPack_1) Then
                Me.m_ViewBrandPack_1.Dispose()
                Me.m_ViewBrandPack_1 = Nothing
            End If
            If Not IsNothing(Me.m_ViewPOSMS) Then
                Me.m_ViewPOSMS.Dispose()
                Me.m_ViewPOSMS = Nothing
            End If
        End Sub
#End Region

    End Class
End Namespace


