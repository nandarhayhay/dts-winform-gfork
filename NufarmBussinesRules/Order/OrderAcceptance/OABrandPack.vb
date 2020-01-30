Imports System.Data.SqlClient
Namespace OrderAcceptance
    Public Class OABrandPack
        Inherits NufarmBussinesRules.OrderAcceptance.OARegistering

#Region " Deklarasi "
        Private m_ViewOABrandPack As DataView
        Private m_ViewBrandPack As DataView
        Private m_ViewOARef As DataView
        Private m_ViewOASMS As DataView
        Public PO_BRANDPACK_ID As String
        Public OA_ORIGINAL_QTY As Decimal = 0
        Public QTY_EVEN As Decimal = 0
        Public LEFT_QTY As Decimal = 0
        Public OA_PRICE_PERQTY As Decimal = 0
        Public OA_TOTAL_PRICE As Decimal = 0
        Public OA_BRANDPACK_ID As String
        Public BRANDPACK_NAME As String
        Public BRANDPACK_ID As String
        Public TOTAL_PRICE As Decimal = 0
        Public BRANDPACK_FROM As String
        Public PO_OA_ORIGINAL_QTY As Object
        Public DISTRIBUTOR_ID As String
        Public TOTAL_DISC_QTY As Decimal = 0
        Public TOTAL_AMOUNT_DISC As Decimal = 0
        Protected AGREE_DISC_QTY As Decimal = 0
        Protected PROG_DISC_QTY As Decimal = 0
        Protected PROJ_DISC_QTY As Decimal = 0
        Protected OTHER_DISC_QTY As Decimal = 0
        Private M_ViewRemainding As DataView
        Private m_ViewOARemainding As DataView
        Public UNIT As String
        Public RemarkOA As String
        Public FlagAgreement As String
        Public FlagLastAgreement As String
        Public Devided_Qty As Decimal = 0
        Public Devide_Factor As Decimal = 0
        Public PROJ_BRANDPACK_ID As Object = Nothing
#End Region

#Region " Constructor / Destructor "

        Public Sub New()

        End Sub

        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewOABrandPack) Then
                Me.m_ViewOABrandPack.Dispose()
                Me.m_ViewOABrandPack = Nothing
            End If
            If Not IsNothing(Me.m_ViewBrandPack) Then
                Me.m_ViewBrandPack.Dispose()
                Me.m_ViewBrandPack = Nothing
            End If
            If Not IsNothing(Me.m_ViewOARef) Then
                Me.m_ViewOARef.Dispose()
                Me.m_ViewOARef = Nothing
            End If
            If Not IsNothing(Me.m_ViewOASMS) Then
                Me.m_ViewOASMS.Dispose()
                Me.m_ViewOASMS = Nothing
            End If
            If Not IsNothing(Me.M_ViewRemainding) Then
                Me.M_ViewRemainding.Dispose()
                Me.M_ViewRemainding = Nothing
            End If
            If Not IsNothing(Me.m_ViewOARemainding) Then
                Me.m_ViewOARemainding.Dispose()
                Me.m_ViewOARemainding = Nothing
            End If
        End Sub

#End Region

#Region " Function "

        Public Function GetUnit(ByVal BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As String
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_Get_Unit_Brandpack", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Unit_Brandpack")
                End If
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) '
                Me.OpenConnection()
                Dim Unit As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If (IsNothing(Unit)) Or (IsDBNull(Unit)) Then
                    Me.CloseConnection()
                    Throw New Exception("BrandPack has not had Unit yet.")
                End If
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
                Return Unit.ToString()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function CreateViewOASMS() As DataView
            Try
                Me.SearcData("Usp_GetView_OA_SM", "")
                Me.m_ViewOASMS = Me.baseChekTable.DefaultView
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewOASMS
        End Function

        Public Function GetDevided_QTY(ByVal BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As Decimal
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_Get_Divided_Qty", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Divided_Qty")
                End If
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) '

                Me.OpenConnection()
                Dim Devided_QTY As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If (IsNothing(Devided_QTY)) Or (IsDBNull(Devided_QTY)) Then
                    Me.CloseConnection()
                    Throw New Exception("DIVIDE_FACTOR FOR PACK has not been set yet." & vbCrLf & _
                    "Please Suply Divide Factor for pack with the BrandPack.")
                End If
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
                Return Convert.ToDecimal(Devided_QTY)
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function GetDivide_Factor(ByVal BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As Decimal
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_Get_Divide_Factor", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Divide_Factor")
                End If
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) '
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If (IsNothing(retval)) Or (IsDBNull(retval)) Then
                    Throw New Exception("Devide_Factor has not been set yet")
                End If
                'Dim Divide_Factor As Object = Me.ExecuteScalar() '"", "SELECT DEVIDE_FACTOR" & _
                '" FROM BRND_PACK WHERE PACK_ID = (SELECT PACK_ID FROM BRND_BRANDPACK" & _
                '" WHERE BRANDPACK_ID = '" & BRANDPACK_ID & "')")
                'If (IsNothing(Divide_Factor)) Or (IsDBNull(Divide_Factor)) Then
                '    Throw New Exception("Devide_Factor has not been set yet")
                'End If
                'Return Convert.ToDecimal(Divide_Factor)
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
                Return Convert.ToDecimal(retval)
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function GetPercentage(ByVal persen As Integer, ByVal SUM_PO_ORIGINALQTY As Decimal, ByVal TARGET_QTY As Int64) As Decimal
            Return Convert.ToDecimal((SUM_PO_ORIGINALQTY * persen * 1) / TARGET_QTY)
        End Function

        Private Function UpdateRemain(ByVal OA_BRANDPACK_ID As String, ByVal tblUpRemainding As DataTable, ByVal dtview As DataView, ByRef row As DataRow, ByRef rowDisc As DataRow, ByRef tblInsOBDisc As DataTable, ByVal Price As Decimal, ByVal ResultQty As Decimal, ByRef LEFT_QTY As Decimal, ByVal Flag As String) As Boolean
            With tblUpRemainding
                For I As Integer = 0 To dtview.Count - 1
                    LEFT_QTY += Convert.ToDecimal(dtview(I)("LEFT_QTY"))
                    If LEFT_QTY = ResultQty Then
                        'include dulu ke table Upremainding
                        row = .NewRow()
                        row("LEFT_QTY") = 0
                        row("RELEASE_QTY") = dtview(I)("LEFT_QTY")
                        row("OA_RM_ID") = dtview(I)("OA_RM_ID").ToString()
                        .Rows.Add(row)
                        'INSERT KE TABLE insOBDisc
                        With tblInsOBDisc
                            rowDisc = .NewRow()
                            rowDisc("OA_BRANDPACK_ID") = OA_BRANDPACK_ID
                            rowDisc("GQSY_SGT_P_FLAG") = Flag
                            rowDisc("DISC_QTY") = dtview(I)("LEFT_QTY")
                            rowDisc("PRICE_PRQTY") = Price
                            rowDisc("BRND_B_S_ID") = DBNull.Value
                            rowDisc("MRKT_M_S_ID") = DBNull.Value
                            rowDisc("AGREE_DISC_HIST_ID") = DBNull.Value
                            rowDisc("MRKT_DISC_HIST_ID") = DBNull.Value
                            rowDisc("PROJ_DISC_HIST_ID") = DBNull.Value
                            rowDisc("OA_RM_ID") = dtview(I)("OA_RM_ID").ToString()
                            rowDisc("ACHIEVEMENT_BRANDPACK_ID") = DBNull.Value
                            .Rows.Add(rowDisc)
                        End With
                        'insert langsung ke database
                        Me.UpdateRemainding(OA_BRANDPACK_ID, tblUpRemainding, tblInsOBDisc)
                        Return True
                    ElseIf LEFT_QTY > ResultQty Then
                        row = .NewRow()
                        row("LEFT_QTY") = LEFT_QTY - ResultQty
                        row("RELEASE_QTY") = Convert.ToDecimal(dtview(I)("LEFT_QTY")) - Convert.ToDecimal(row("LEFT_QTY"))
                        row("OA_RM_ID") = dtview(I)("OA_RM_ID").ToString()
                        .Rows.Add(row)
                        With tblInsOBDisc
                            rowDisc = .NewRow()
                            rowDisc("OA_BRANDPACK_ID") = OA_BRANDPACK_ID
                            rowDisc("GQSY_SGT_P_FLAG") = Flag
                            rowDisc("DISC_QTY") = row("RELEASE_QTY")
                            rowDisc("PRICE_PRQTY") = Price
                            rowDisc("BRND_B_S_ID") = DBNull.Value
                            rowDisc("MRKT_M_S_ID") = DBNull.Value
                            rowDisc("AGREE_DISC_HIST_ID") = DBNull.Value
                            rowDisc("MRKT_DISC_HIST_ID") = DBNull.Value
                            rowDisc("PROJ_DISC_HIST_ID") = DBNull.Value
                            rowDisc("OA_RM_ID") = dtview(I)("OA_RM_ID").ToString()
                            rowDisc("ACHIEVEMENT_BRANDPACK_ID") = DBNull.Value
                            .Rows.Add(rowDisc)
                        End With
                        'insert langsung ke database
                        Me.UpdateRemainding(OA_BRANDPACK_ID, tblUpRemainding, tblInsOBDisc)
                        Return True
                    ElseIf LEFT_QTY < ResultQty Then
                        row = .NewRow()
                        row("LEFT_QTY") = 0
                        row("RELEASE_QTY") = dtview(I)("LEFT_QTY")
                        row("OA_RM_ID") = dtview(I)("OA_RM_ID").ToString()
                        .Rows.Add(row)
                    End If
                    With tblInsOBDisc
                        rowDisc = .NewRow()
                        rowDisc("OA_BRANDPACK_ID") = OA_BRANDPACK_ID
                        rowDisc("GQSY_SGT_P_FLAG") = Flag
                        rowDisc("DISC_QTY") = dtview(I)("LEFT_QTY")
                        rowDisc("PRICE_PRQTY") = Price
                        rowDisc("BRND_B_S_ID") = DBNull.Value
                        rowDisc("MRKT_M_S_ID") = DBNull.Value
                        rowDisc("AGREE_DISC_HIST_ID") = DBNull.Value
                        rowDisc("MRKT_DISC_HIST_ID") = DBNull.Value
                        rowDisc("PROJ_DISC_HIST_ID") = DBNull.Value
                        rowDisc("OA_RM_ID") = dtview(I)("OA_RM_ID").ToString()
                        rowDisc("ACHIEVEMENT_BRANDPACK_ID") = DBNull.Value
                        .Rows.Add(rowDisc)
                    End With
                    'If I <= dtview.Count - 1 Then
                    '    Me.UpdateRemainding(OA_BRANDPACK_ID, tblUpRemainding, tblInsOBDisc)
                    '    Return True
                    'End If
                Next
                Return False
            End With
        End Function

        Public Function CreateViewOABRANDPACK(ByVal OA_ID As String) As DataView
            Try
                Me.CreateCommandSql("Sp_GetViewOA_BRANDPACK", "")
                Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 32)
                Dim tblOABrandPack As New DataTable("OA_BRANDPACK")
                tblOABrandPack.Clear()
                Me.FillDataTable(tblOABrandPack)
                Me.m_ViewOABrandPack = tblOABrandPack.DefaultView()
                Me.m_ViewOABrandPack.Sort = "OA_BRANDPACK_ID"
                Me.m_ViewOABrandPack.RowStateFilter = DataViewRowState.CurrentRows
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewOABrandPack
        End Function

        Protected Function SaveToDataView(ByVal SaveType As String, Optional ByVal Key As Object = Nothing) As DataView
            Try
                With ViewOABRANDPACK
                    Select Case SaveType
                        Case "Save"
                            drv = .AddNew()
                            drv("OA_BRANDPACK_ID") = Me.OA_BRANDPACK_ID
                            drv("OA_REF_NO") = Me.OA_ID
                            drv("PO_BRANDPACK_ID") = Me.PO_BRANDPACK_ID
                            drv("BRANDPACK_ID") = Me.BRANDPACK_ID
                            drv("BRANDPACK_NAME") = Me.BRANDPACK_NAME
                            drv("OA_ORIGINAL_QTY") = Me.OA_ORIGINAL_QTY
                            drv("OA_PRICE_PERQTY") = Me.OA_PRICE_PERQTY
                            Dim TOTAL_PRICE As Decimal = Me.OA_PRICE_PERQTY * Me.OA_ORIGINAL_QTY
                            drv("TOTAL_PRICE") = TOTAL_PRICE
                            drv("TOTAL_DISC_QTY") = Me.TOTAL_DISC_QTY
                            drv("TOTAL_AMOUNT_DISC") = Me.TOTAL_AMOUNT_DISC
                            drv("QTY_EVEN") = Me.QTY_EVEN
                            drv("LEFT_QTY") = Me.LEFT_QTY
                            drv("UNIT_ORDER") = Me.UNIT
                            drv("REMARK") = Me.RemarkOA
                            drv("PROJ_BRANDPACK_ID") = Me.PROJ_BRANDPACK_ID
                            'drv("AGREE_DISC_QTY") = Me.AGREE_DISC_QTY
                            'drv("PROG_DISC_QTY") = Me.PROG_DISC_QTY
                            'drv("PROJ_DISC_QTY") = Me.PROJ_DISC_QTY
                            'drv("OTHER_DISC_QTY") = Me.OTHER_DISC_QTY
                            drv.EndEdit()
                        Case "Update"
                            Dim index As Integer = Me.m_ViewOABrandPack.Find(Key)
                            If index <> -1 Then
                                .Item(index)("OA_BRANDPACK_ID") = Key
                                .Item(index)("OA_REF_NO") = Me.OA_ID
                                .Item(index)("PO_BRANDPACK_ID") = Me.PO_BRANDPACK_ID
                                .Item(index)("BRANDPACK_ID") = Me.BRANDPACK_ID
                                .Item(index)("BRANDPACK_NAME") = Me.BRANDPACK_NAME
                                .Item(index)("OA_ORIGINAL_QTY") = Me.OA_ORIGINAL_QTY
                                .Item(index)("OA_PRICE_PERQTY") = Me.OA_PRICE_PERQTY
                                Dim TOTAL_PRICE As Decimal = Me.OA_PRICE_PERQTY * Me.OA_ORIGINAL_QTY
                                .Item(index)("TOTAL_PRICE") = TOTAL_PRICE
                                .Item(index)("TOTAL_DISC_QTY") = Me.TOTAL_DISC_QTY
                                .Item(index)("TOTAL_AMOUNT_DISC") = Me.TOTAL_AMOUNT_DISC
                                .Item(index)("LEFT_QTY") = Me.LEFT_QTY
                                .Item(index)("QTY_EVEN") = Me.QTY_EVEN
                                .Item(index)("UNIT_ORDER") = Me.UNIT
                                .Item(index)("REMARK") = Me.RemarkOA
                                '.Item(index)("AGREE_DISC_QTY") = Me.AGREE_DISC_QTY
                                '.Item(index)("PROG_DISC_QTY") = Me.PROG_DISC_QTY
                                '.Item(index)("PROJ_DISC_QTY") = Me.PROJ_DISC_QTY
                                '.Item(index)("OTHER_DISC_QTY") = Me.OTHER_DISC_QTY
                                .Item(index)("PROJ_BRANDPACK_ID") = Me.PROJ_BRANDPACK_ID
                                .Item(index).EndEdit()
                            End If
                    End Select

                End With
            Catch ex As Exception
            End Try
            Return Me.m_ViewOABrandPack
        End Function

        Public Function IsExistedOABrandPack(ByVal OA_BRANDPACK_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_Existing_OA_BRANDPACK", "")
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
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
     
        'Public Function getShipToArea(ByVal OA_REF_NO As String) As String
        '    Try
        '        Me.CreateCommandSql("Usp_Get_OA_Description", "")
        '        Me.AddParameter("@OA_REF_NO", SqlDbType.VarChar, OA_REF_NO, 30)
        '        Dim tbl As New DataTable("OADescription")
        '        tbl.Clear()
        '        Me.FillDataTable(tbl)
        '        If tbl.Rows.Count <= 0 Then
        '            Return "can not find territory"
        '        End If
        '        Dim ShipTo As String = ""
        '        For i As Integer = 0 To tbl.Rows.Count - 1
        '            ShipTo += tbl.Rows(i)("TERRITORY_AREA").ToString()
        '            If i = tbl.Rows.Count - 1 Then
        '                Exit For
        '            Else
        '                ShipTo += ","
        '            End If
        '        Next
        '        Return ShipTo
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        'End Function
        Public Function CreateViewOARef() As DataView
            Try
                'Me.CreateCommandSql("Sp_GetViewOA", "")
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT TOP 100 OOA.OA_ID AS OA_REF_NO,OOA.OA_DATE,OOP.DISTRIBUTOR_ID ,OOP.PO_REF_DATE,DR.DISTRIBUTOR_NAME AS PO_FROM," & vbCrLf & _
                        " OOP.PO_REF_NO FROM ORDR_ORDER_ACCEPTANCE OOA INNER JOIN ORDR_PURCHASE_ORDER OOP ON OOP.PO_REF_NO = OOA.PO_REF_NO" & vbCrLf & _
                        " INNER JOIN DIST_DISTRIBUTOR DR ON OOP.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID " & vbCrLf & _
                        " ORDER BY OOA.IDAPP DESC ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If

                Dim tblOA_Ref As New DataTable("OA_REF")
                tblOA_Ref.Clear()
                Me.FillDataTable(tblOA_Ref)
                Me.m_ViewOARef = tblOA_Ref.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewOARef
        End Function
        Public Function SearchOARef(ByVal SearchOA_REF_NO As String) As DataView
            Try
                Me.CreateCommandSql("Usp_Search_OARef", "")
                Me.AddParameter("@SearchOARef", SqlDbType.VarChar, SearchOA_REF_NO, 70)
                Dim tblOA_Ref As New DataTable("OA_REF")
                tblOA_Ref.Clear()
                Me.FillDataTable(tblOA_Ref)
                Me.m_ViewOARef = tblOA_Ref.DefaultView()
                Return Me.m_ViewOARef
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function PO_From(ByVal OA_Ref_NO As String, ByVal mustCloseConnection As Boolean) As String
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Sp_Select_Distributor_PO", "")
                Else
                    Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Select_Distributor_PO")
                End If

                Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_Ref_NO, 32)
                Me.ExecuteReader()
                While Me.SqlRe.Read()
                    Me.DISTRIBUTOR_ID = Me.SqlRe("DISTRIBUTOR_ID").ToString()
                    Me.DISTRIBUTOR_NAME = Me.SqlRe("DISTRIBUTOR_NAME").ToString()
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
                Return Me.DISTRIBUTOR_NAME
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function

        Public Function CreateViewPOBrandPack(ByVal PO_REF_NO As String, ByVal DISTRIBUTOR_ID As String) As DataView
            Try
                Me.CreateCommandSql("Usp_SelectBrandPack_IN_ORDR_PO_BRANDPACK", "")
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25)
                'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Dim tblBrandPack As New DataTable("BRANDPACK")
                tblBrandPack.Clear()
                Me.FillDataTable(tblBrandPack)
                Me.m_ViewBrandPack = tblBrandPack.DefaultView()
                Me.m_ViewBrandPack.Sort = "PO_BRANDPACK_ID"
                'Me.m_ViewOABrandPack.RowStateFilter = DataViewRowState.Added Or DataViewRowState.ModifiedCurrent
                Return Me.m_ViewBrandPack
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try

        End Function

        Public Function HasReferencedOABrandPack(ByVal OA_BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_Check_REFERENCED_OA_BRANDPACK", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_REFERENCED_OA_BRANDPACK")
                End If
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar()
                Dim retval As Object = Me.SqlCom.Parameters("@RETURN_VALUE").Value
                If Not IsNothing(retval) Then
                    If mustCloseConnection Then : Me.CloseConnection() : End If
                    Me.ClearCommandParameters()
                    Return (CInt(retval) > 0)
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
                Return False
                'If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                '    Return True
                'End If
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function
        Public Function HasDiscount(ByVal OA_BRANDPACK_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Usp_Check_OA_BrandPack_Discount", "")
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                If CInt(Me.ExecuteScalar()) > 0 Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function GetQuantityOA_ORIGINAL_QTY(ByVal PO_BRANDPACK_ID As String, ByVal OA_ID As String, ByVal Savetype As String) As Decimal
            Try
                Dim QTY As Object = Nothing
                Select Case Savetype
                    Case "Save"
                        Me.CreateCommandSql("Sp_Select_QTY_PO_BRANDPACK", "")
                        Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID, 39) ' VARCHAR(35),
                        'Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 30) ' VARCHAR(30),
                        'Me.AddParameter("@O_PO_ORIGINAL_QTY", SqlDbType.Float, ParameterDirection.Output) ' INT OUTPUT,
                        'Me.AddParameter("@O_OA_ORIGINAL_QTY", SqlDbType.Float, ParameterDirection.Output) ' INT OUTPUT,
                        'Me.AddParameter("@O_PRICE", SqlDbType.Decimal, ParameterDirection.Output) ' varchar(10) OUTPUT,


                        Me.SqlCom.Parameters.Add("@O_PRICE", SqlDbType.Decimal, 0).Direction = ParameterDirection.Output
                        Me.SqlCom.Parameters()("@O_PRICE").Scale = 2

                        'Me.AddParameter("@O_LEFT_QTY", SqlDbType.Decimal, ParameterDirection.Output) ' INT OUTPUT
                        Me.SqlCom.Parameters.Add("@O_LEFT_QTY", SqlDbType.Decimal, 0).Direction = ParameterDirection.Output
                        Me.SqlCom.Parameters()("@O_LEFT_QTY").Scale = 3

                        'Me.AddParameter("@O_UNIT", SqlDbType.VarChar, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_UNIT", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output
                        Me.SqlCom.Parameters()("@O_UNIT").Size = 10
                        Me.OpenConnection()
                        Me.SqlCom.ExecuteNonQuery()
                        Me.CloseConnection()
                        QTY = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_LEFT_QTY").Value)
                    Case "Update"
                        Me.CreateCommandSql("", "SELECT SUM(PO_ORIGINAL_QTY) FROM ORDR_PO_BRANDPACK WHERE PO_BRANDPACK_ID = '" & PO_BRANDPACK_ID & "'")
                        QTY = Me.ExecuteScalar()
                        If IsDBNull(QTY) Then
                            Return 0
                        Else
                            Return Convert.ToDecimal(QTY)
                        End If
                End Select

                Me.ClearCommandParameters()
                Return QTY
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function

        Public Function CheckDistBrandPack(ByVal OA_BRANDPACK_ID As String, ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String) As Object
            Try
                Me.CreateCommandSql("Sp_Check_BRANDPACK_ID_IN_PROJ_BRANDPACK", "")
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10)
                Return Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function

        Public Sub GetFlagSalesAgreementFlag(ByRef dt As DataTable, ByVal DISTRIBUTOR_ID As String, _
        ByVal BRANDPACK_ID As String, ByVal PO_DATE As DateTime)
            'AMBIL FLAG AGREEMENT DULU
            'AMBIL FLAG LAST AGREEMENT
            'BIKIN DATATABLE FLAG MRKT
            Try
                Me.CreateCommandSql("Usp_Get_Flag_Agrement_First_Last", "")
                'Me.AddParameter("@FLAG", SqlDbType.Char, ParameterDirection.Output) ' CHAR(1) OUTPUT,
                Me.SqlCom.Parameters.Add("@FLAG", SqlDbType.Char, 1).Direction = ParameterDirection.Output
                Me.SqlCom.Parameters("@FLAG").Size = 1
                'Me.AddParameter("@LAST_FLAG", SqlDbType.Char, ParameterDirection.Output) ' CHAR(1) OUTPUT,
                Me.SqlCom.Parameters.Add("@LAST_FLAG", SqlDbType.Char, 1).Direction = ParameterDirection.Output
                Me.SqlCom.Parameters("@LAST_FLAG").Size = 1 '@LAST_FLAG CHAR(1)OUTPUT,

                Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14)
                Me.OpenConnection()
                Me.SqlCom.ExecuteNonQuery()
                Me.FlagAgreement = ""
                Me.FlagLastAgreement = ""
                If (Not IsNothing(Me.SqlCom.Parameters()("@FLAG").Value)) And (Not IsDBNull(Me.SqlCom.Parameters()("@FLAG").Value)) Then
                    Me.FlagAgreement = Me.SqlCom.Parameters()("@FLAG").Value.ToString()
                    If (Not IsNothing(Me.SqlCom.Parameters()("@LAST_FLAG").Value)) And (Not IsDBNull(Me.SqlCom.Parameters()("@LAST_FLAG").Value)) Then
                        Me.FlagLastAgreement = Me.SqlCom.Parameters()("@LAST_FLAG").Value.ToString()
                    End If
                    Me.ClearCommandParameters()
                    Me.SqlCom.CommandText = "Usp_Check_BRANDPACK_ID_IN_MRKT_BRANPACK_DISTRIBUTOR"
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14)
                    Me.FillDataTable(dt)
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try

        End Sub

        Public Sub RecalCulateQTY(ByVal dtview As DataView, ByVal OA_BRANDPACK_ID As String, ByVal ResultQty As Decimal, ByVal PRICE As Decimal)
            Try

                '---------------------hah salah methode euy...kuduna di jieun function--------------
                '--------------------haduh kacou......panjang pisan...tapi arek ngarobah males ngadebug deui......
                Dim tblUpRemainding As New DataTable("tblUpdateRemainding")
                Dim colLeftQtyRemainding As New DataColumn("LEFT_QTY", Type.GetType("System.Decimal"))
                Dim ColReleaseQty As New DataColumn("RELEASE_QTY", Type.GetType("System.Decimal"))
                Dim ColOARMID As New DataColumn("OA_RM_ID", Type.GetType("System.String"))
                tblUpRemainding.Columns.Add(colLeftQtyRemainding)
                tblUpRemainding.Columns.Add(ColReleaseQty)
                tblUpRemainding.Columns.Add(ColOARMID)
                tblUpRemainding.Clear()

                Dim tblInsOBDisc As New DataTable("OA_BRANDPACK_DISC")
                Dim colOA_BRANDPACK_ID As New DataColumn("OA_BRANDPACK_ID", Type.GetType("System.String"))
                Dim colGQSY_SGT_P_FLAG As New DataColumn("GQSY_SGT_P_FLAG", Type.GetType("System.String"))
                Dim colDISC_QTY As New DataColumn("DISC_QTY", Type.GetType("System.String"))
                Dim colPRICE_PRQTY As New DataColumn("PRICE_PRQTY", Type.GetType("System.String"))
                Dim colBRND_B_S_ID As New DataColumn("BRND_B_S_ID", Type.GetType("System.String"))
                colBRND_B_S_ID.DefaultValue = DBNull.Value

                Dim colMRKT_M_S_ID As New DataColumn("MRKT_M_S_ID", Type.GetType("System.String"))
                colMRKT_M_S_ID.DefaultValue = DBNull.Value
                Dim colAGREE_DISC_HIST_ID As New DataColumn("AGREE_DISC_HIST_ID", Type.GetType("System.String"))
                colAGREE_DISC_HIST_ID.DefaultValue = DBNull.Value
                Dim colMRKT_DISC_HIST_ID As New DataColumn("MRKT_DISC_HIST_ID", Type.GetType("System.String"))
                colMRKT_DISC_HIST_ID.DefaultValue = DBNull.Value
                Dim colPROJ_DISC_HIST_ID As New DataColumn("PROJ_DISC_HIST_ID", Type.GetType("System.String"))
                colPROJ_DISC_HIST_ID.DefaultValue = DBNull.Value
                Dim colAchBrandPackID As New DataColumn("ACHIEVEMENT_BRANDPACK_ID", Type.GetType("System.String"))
                colAchBrandPackID.DefaultValue = DBNull.Value
                Dim colOA_RM_ID As New DataColumn("OA_RM_ID", Type.GetType("System.String"))
                colOA_RM_ID.DefaultValue = DBNull.Value
                With tblInsOBDisc.Columns
                    .Add(colOA_BRANDPACK_ID)
                    .Add(colGQSY_SGT_P_FLAG)
                    .Add(colDISC_QTY)
                    .Add(colPRICE_PRQTY)
                    .Add(colBRND_B_S_ID)
                    .Add(colMRKT_M_S_ID)
                    .Add(colAGREE_DISC_HIST_ID)
                    .Add(colMRKT_DISC_HIST_ID)
                    .Add(colPROJ_DISC_HIST_ID)
                    .Add(colOA_RM_ID)
                    .Add(colAchBrandPackID)
                End With
                tblInsOBDisc.Clear()
                Dim mustReturn As Boolean = False
                Dim LEFT_QTY As Decimal = 0
                'AMBIL YANG SISA DARI OA DULU
                'CARI SISA OA
                Dim row As DataRow = Nothing
                Dim rowDisc As DataRow = Nothing
                dtview.Sort = "CREATE_DATE ASC"
                'FILTER BERDASARKAN OA_BRANDPACK YANG DIPILIH DI DATAGRID1
                dtview.RowFilter = "OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "' AND FLAG = 'RMOA'"
                'INSERT LANGSUNG KE TABLE
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "RMOA")
                End If
                If mustReturn Then : Return : End If

                'FILTER BERDASARKAN RMOA YANG BUKAN OA_BRANDPACK_ID DI GRID1
                dtview.RowFilter = "FLAG = 'RMOA' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                'SORt DTVIEW BY CREATE_DATE
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "RMOA")
                End If
                If mustReturn Then : Return : End If


                'AMBIL DATA DARI GIVEN AGREEMENT
                'CARI DATA DARI GIVEN AGREEMENT
                dtview.RowFilter = "FLAG = 'G' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "G")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'G' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "G")
                End If
                If mustReturn Then : Return : End If

                'AMBIL DARI OTHER
                dtview.RowFilter = "FLAG = 'O' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "O")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'O' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "O")
                End If
                If mustReturn Then : Return : End If


                '========================OCBD==============================
                dtview.RowFilter = "FLAG = 'OCBD' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "O")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'OCBD' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "O")
                End If
                If mustReturn Then : Return : End If


                '=========================== O DD ==============================
                dtview.RowFilter = "FLAG = 'ODD' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "O")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'ODD' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "O")
                End If
                If mustReturn Then : Return : End If


                '======================== O DR =================================================
                dtview.RowFilter = "FLAG = 'ODR' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "O")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'ODR' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "O")
                End If
                If mustReturn Then : Return : End If

                '==================  DPD =================================================
                dtview.RowFilter = "FLAG = 'Q1' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "Q1")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'Q1' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "Q1")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'Q2' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "Q2")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'Q2' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "Q2")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'Q3' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "Q3")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'Q3' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "Q3")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'Q4' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "Q4")
                End If
                If mustReturn Then : Return : End If


                dtview.RowFilter = "FLAG = 'Q4' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "Q4")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'S1' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "S1")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'S1' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "S1")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'S2' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "S2")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'S2' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "S2")
                End If
                If mustReturn Then : Return : End If


                dtview.RowFilter = "FLAG = 'Y' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "Y")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'Y' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "Y")
                End If
                If mustReturn Then : Return : End If

                '====================================== BAGIAN SALES ============================
                dtview.RowFilter = "FLAG = 'DK' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "DK")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'DK' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "DK")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'DN' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "DN")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'DN' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "DN")
                End If
                If mustReturn Then : Return : End If


                dtview.RowFilter = "FLAG = 'CP' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CP")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'CP' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CP")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'CS' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CS")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'CS' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CS")
                End If
                If mustReturn Then : Return : End If

                'TARGET TM DAN DISTRIBUTOR
                dtview.RowFilter = "FLAG = 'TS' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "TS")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'TS' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "TS")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'TD' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "TD")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'TD' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "TD")
                End If
                If mustReturn Then : Return : End If


                dtview.RowFilter = "FLAG = 'CT' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CT")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'CT' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CT")
                End If
                If mustReturn Then : Return : End If


                dtview.RowFilter = "FLAG = 'CD' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CD")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'CD' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CD")
                End If
                If mustReturn Then : Return : End If


                dtview.RowFilter = "FLAG = 'CA' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CA")
                End If
                dtview.RowFilter = "FLAG = 'CA' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CA")
                End If
                If mustReturn Then : Return : End If


                dtview.RowFilter = "FLAG = 'CR' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CR")
                End If
                If mustReturn Then : Return : End If

                dtview.RowFilter = "FLAG = 'CR' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                If dtview.Count > 0 Then
                    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "CR")
                End If
                If mustReturn Then : Return : End If

                '-----------------------SUDAH tidak di pakai lagi---------------------------------------------------
                'dtview.RowFilter = "FLAG = 'KP' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                'If dtview.Count > 0 Then
                '    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "KP")
                'End If
                'If mustReturn Then : Return : End If

                'dtview.RowFilter = "FLAG = 'KP' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                'If dtview.Count > 0 Then
                '    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "KP")
                'End If
                'If mustReturn Then : Return : End If


                'dtview.RowFilter = "FLAG = 'MG' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                'If dtview.Count > 0 Then
                '    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "MG")
                'End If
                'If mustReturn Then : Return : End If

                'dtview.RowFilter = "FLAG = 'MG' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                'If dtview.Count > 0 Then
                '    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "MG")
                'End If
                'If mustReturn Then : Return : End If

                'dtview.RowFilter = "FLAG = 'P' AND OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                'If dtview.Count > 0 Then
                '    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "P")
                'End If
                'If mustReturn Then : Return : End If

                'dtview.RowFilter = "FLAG = 'P' AND OA_BRANDPACK_ID <> '" & OA_BRANDPACK_ID & "'"
                'If dtview.Count > 0 Then
                '    mustReturn = Me.UpdateRemain(OA_BRANDPACK_ID, tblUpRemainding, dtview, row, rowDisc, tblInsOBDisc, PRICE, ResultQty, LEFT_QTY, "P")
                'End If
                'If mustReturn Then : Return : End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        'HANYA UNTUK MENGINSERT ALL DATA REMAINDING KE OA_BRANDPACK_DISCOUNT
        Private Sub UpdateRemainding(ByVal OA_BRANDPACK_ID As String, ByVal dtTableRemainding As DataTable, ByVal tblOABPDisc As DataTable)
            Try
                'insert oa_brandpack_disc dulu
                Dim dtViewRemainding As DataView = dtTableRemainding.DefaultView()
                dtViewRemainding.Sort = "OA_RM_ID"

                Dim dtViewDisc As DataView = tblOABPDisc.DefaultView()
                dtViewDisc.Sort = "OA_RM_ID"
                Me.GetConnection()
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.SqlCom = New SqlCommand()
                Me.SqlCom.CommandText = "Usp_Insert_ORDR_OA_BRANDPACK_DISCOUNT"
                Me.SqlCom.CommandType = CommandType.StoredProcedure
                Me.SqlCom.Connection = Me.SqlConn
                Me.SqlCom.Transaction = Me.SqlTrans
                For i As Integer = 0 To dtViewDisc.Count - 1
                    Dim GQSY_SGT_P_FLAG As String = dtViewDisc(i)("GQSY_SGT_P_FLAG").ToString()
                    Dim DISC_QTY As Decimal = Convert.ToDecimal(dtViewDisc(i)("DISC_QTY"))
                    Dim PRICE_PRQTY As Decimal = Convert.ToDecimal(dtViewDisc(i)("PRICE_PRQTY"))
                    Dim OA_RM_ID As String = dtViewDisc(i)("OA_RM_ID").ToString()
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) '(44),
                    Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, GQSY_SGT_P_FLAG, 5) ' VARCHAR(5),
                    Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DISC_QTY) ' INT,
                    Me.AddParameter("@PRICE_PRQTY", SqlDbType.Decimal, PRICE_PRQTY, 10) '  VARCHAR(10),
                    Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 115) ' VARCHAR(71),
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 115) ' VARCHAR(66),
                    Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 66) ' VARCHAR(66),
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName) ' VARCHAR(30)
                    Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, DBNull.Value, 60)
                    Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, DBNull.Value, 60) ' VARCHAR(60),
                    Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, DBNull.Value, 70)
                    Me.AddParameter("@OA_RM_ID", SqlDbType.VarChar, OA_RM_ID, 200)
                    Me.SqlCom.ExecuteNonQuery()
                    Me.ClearCommandParameters()
                Next
                Me.SqlCom.CommandText = "Usp_Check_Sum_Mathching_Disc_Qty"
                Me.SqlCom.CommandType = CommandType.StoredProcedure

                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Agreement_Discount", 20) ' VARCHAR(20)
                Me.SqlCom.ExecuteNonQuery()
                Me.ClearCommandParameters()

                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Sales_Discount", 20) ' VARCHAR(20)
                Me.SqlCom.ExecuteNonQuery()
                Me.ClearCommandParameters()

                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Other_Discount", 20) ' VARCHAR(20)
                Me.SqlCom.ExecuteNonQuery()
                Me.ClearCommandParameters()

                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Project_Discount", 20) ' VARCHAR(20)
                Me.SqlCom.ExecuteNonQuery()
                Me.ClearCommandParameters()


                Me.SqlCom.CommandText = "Usp_Check_Sum_Mathing_Qty_ORDDR_OA_REMAINDING"
                Me.SqlCom.CommandType = CommandType.StoredProcedure
                For i As Integer = 0 To dtViewRemainding.Count - 1
                    Dim OA_RM_ID As String = dtViewRemainding(i)("OA_RM_ID").ToString()
                    Me.AddParameter("@OA_RM_ID", SqlDbType.VarChar, OA_RM_ID, 200)
                    Me.SqlCom.ExecuteNonQuery()
                    Me.ClearCommandParameters()
                Next
                Me.ClearCommandParameters()
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        'function untuk memview keseluruhan oa left
        Public Function CreateViewRemaindingLeft(ByVal OA_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As DataView
            Try
                If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Select_OA_Remainding_Detail")
                Else : CreateCommandSql("Usp_Select_OA_Remainding_Detail", "")
                End If
                AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 32)
                Dim dt As New DataTable("Remainding_Item")
                If Me.SqlDat Is Nothing Then
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                Me.SqlDat.SelectCommand.CommandTimeout = 0
                Me.SqlDat.Fill(dt)
                'dt.Clear() : FillDataTable(dt)
                Me.M_ViewRemainding = dt.DefaultView()
                If mustCloseConnection Then : Me.CloseConnection() : Me.ClearCommandParameters() : End If
                Me.ClearCommandParameters()
                Return Me.M_ViewRemainding
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function

        Public Function CreateViewOARemaindingLeft(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String) As DataView
            Try
                Me.CreateCommandSql("Usp_Select_REMAINDING_OA_LEFT_QTY", "")
                '                @DISTRIBUTOR_ID VARCHAR(10),
                '@BRANDPACK_ID VARCHAR(14)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Dim Dt As New DataTable("OA_REMAINDING_QTY")
                Dt.Clear()
                Me.FillDataTable(Dt)
                Me.m_ViewOARemainding = Dt.DefaultView()
                Return Me.m_ViewOARemainding
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        'function untuk memview remainding left qty hanya untuk oa_original qty
        Public Function CreateViewOtherRemaindingLeft(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String) As DataView
            Try

                Me.CreateCommandSql("Usp_Select_REMAINDING_OTHER_DISCOUNT", "")
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Dim Dt As New DataTable("OA_REMAINDING_QTY")
                Dt.Clear()
                Me.FillDataTable(Dt)
                Me.m_ViewOARemainding = Dt.DefaultView()
                Return Me.m_ViewOARemainding
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        'FUNCTION UNTUK MENGECEK TOTAL QTY 
        Public Function getTotalQTY(ByVal OA_BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As Decimal
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Sum_QTY_OA_BRANDPACK", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Sum_QTY_OA_BRANDPACK")
                End If
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                Me.OpenConnection()
                Dim ttlQTy As Object = Me.ExecuteScalar() : Me.ClearCommandParameters()
                If IsNothing(ttlQTy) Then
                    If mustCloseConnection Then : Me.CloseConnection() : End If
                    Throw New Exception("OA_ORIGINAL_QTY is null")
                End If
                Return Convert.ToDecimal(ttlQTy)
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function OARMHasChild(ByVal OA_BRANDPACK_ID As String, ByVal OA_RM_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Usp_Check_OA_RM_ID_IN_OA_BRANDPACK_DISC", "")
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                Me.AddParameter("@OA_RM_ID", SqlDbType.VarChar, OA_RM_ID, 200) ' VARCHAR(150)
                If Not IsNothing(Me.ExecuteScalar()) Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function GetUnitOnOrder(ByVal OA_BRANDPACK_ID As String) As String
            Try
                Dim UnitOnOrder As Object = Me.ExecuteScalar("", "SELECT RIGHT(UNIT,7) FROM BRND_BRANDPACK WHERE BRANDPACK_ID = " & _
                 "(SELECT BRANDPACK_ID FROM ORDR_PO_BRANDPACK WHERE PO_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "')")
                If (UnitOnOrder Is Nothing) Or (IsDBNull(UnitOnOrder)) Then
                    Throw New Exception("Unit on order has not been difined")
                End If
                Return CStr(UnitOnOrder).Trim()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

#End Region

#Region " Sub /Void "

        'private sub InsertLogSMS(byval oi
        Public Sub GetQuantityRowPOBrandPack(ByVal PO_BRANDPACK_ID As String, ByVal OA_REF_NO As String)
            Try
                Me.CreateCommandSql("Sp_Select_QTY_PO_BRANDPACK", "")
                Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, PO_BRANDPACK_ID, 39) ' VARCHAR(35),
                'Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_REF_NO, 30) ' VARCHAR(30)
                'Me.AddParameter("@O_LEFT_QTY", SqlDbType.Decimal, ParameterDirection.Output)
                Me.SqlCom.Parameters.Add("@O_LEFT_QTY", SqlDbType.Decimal, 0).Direction = ParameterDirection.Output
                Me.SqlCom.Parameters()("@O_LEFT_QTY").Scale = 3

                'Me.AddParameter("@O_PRICE", SqlDbType.Decimal, ParameterDirection.Output) 'DECIMAL 
                Me.SqlCom.Parameters.Add("@O_PRICE", SqlDbType.Decimal, 0).Direction = ParameterDirection.Output
                Me.SqlCom.Parameters()("@O_PRICE").Scale = 2
                'Me.AddParameter("@O_UNIT", SqlDbType.VarChar, ParameterDirection.Output)
                Me.SqlCom.Parameters.Add("@O_UNIT", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output
                Me.SqlCom.Parameters()("@O_UNIT").Size = 10
                Me.OpenConnection() : Me.SqlCom.ExecuteNonQuery() : Me.CloseConnection()
                If (Me.SqlCom.Parameters()("@O_PRICE").Value Is Nothing) Or (TypeOf (Me.SqlCom.Parameters()("@O_PRICE").Value) Is DBNull) Then
                    Throw New Exception("Could not find price at OA " & OA_REF_NO)
                End If
                Me.OA_PRICE_PERQTY = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_PRICE").Value)
                Me.PO_OA_ORIGINAL_QTY = Me.SqlCom.Parameters()("@O_LEFT_QTY").Value
                Me.UNIT = Me.SqlCom.Parameters()("@O_UNIT").Value.ToString().Trim()
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        Public Sub SaveOA_BRANDPACK(ByVal SaveType As String)
            Try
                Dim Query As String = "SET NOCOUNT ON;SELECT TOP 1 DEVIDED_QUANTITY FROM BRND_BRANDPACK WHERE " & _
                " BRANDPACK_ID = '" & Me.BRANDPACK_ID & "';"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Dim DEVIDED_QTY As Object = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If IsNothing(DEVIDED_QTY) Or (IsDBNull(DEVIDED_QTY)) Then
                    Me.CloseConnection() : Throw New Exception("Divided Quantity is null")
                End If
                Dim ResultQty As Decimal = Decimal.Truncate(Me.OA_ORIGINAL_QTY / Convert.ToDecimal(DEVIDED_QTY)) * Convert.ToDecimal(DEVIDED_QTY)
                Dim Left_QTY As Decimal = Me.OA_ORIGINAL_QTY Mod DEVIDED_QTY
                Me.QTY_EVEN = ResultQty : Me.LEFT_QTY = Left_QTY
                'reset command text
                Select Case SaveType
                    Case "Save"
                        Me.SqlCom.CommandText = "Sp_Insert_OA_BRANDPACK"
                        'Me.InsertData("Sp_Insert_OA_BRANDPACK", "")
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                    Case "Update"
                        Me.SqlCom.CommandText = "Sp_Update_OA_BRANDPACK"
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                End Select
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_ID, 75) 'VARCHAR
                Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, Me.PO_BRANDPACK_ID, 39)
                Me.AddParameter("@OA_ID", SqlDbType.VarChar, Me.OA_ID, 32) ' VARCHAR(30),
                Me.AddParameter("@QTY_EVEN", SqlDbType.Decimal, QTY_EVEN)
                Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, Left_QTY)
                Me.AddParameter("@OA_ORIGINAL_QTY", SqlDbType.Decimal, Convert.ToDecimal(Me.OA_ORIGINAL_QTY))
                Me.AddParameter("@OA_PRICE_PERQTY", SqlDbType.Decimal, Me.OA_PRICE_PERQTY) ' VARCHAR(10
                Me.AddParameter("@UNIT", SqlDbType.VarChar, Me.UNIT, 10) '
                Me.AddParameter("@REMARK", SqlDbType.VarChar, Me.RemarkOA, 150)
                Me.BeginTransaction()
                If Me.ExecuteNonQuery() > 0 Then
                    If Me.LEFT_QTY > 0 Then
                        'CHECK APAKAH OA_ORIGINAL_NY DIRUBAH TIDAK
                        If SaveType = "Update" Then
                            Me.SqlCom.CommandType = CommandType.Text
                            Me.SqlCom.CommandText = "SET NOCOUNT ON;SELECT OA_ORIGINAL_QTY FROM ORDR_OA_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID;"
                            Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_ID)
                            Dim OrgQtyDB As Decimal = Decimal.Round(Convert.ToDecimal(Me.SqlCom.ExecuteScalar()), 3)
                            If Not OrgQtyDB.Equals(Decimal.Round(Convert.ToDecimal(Me.OA_ORIGINAL_QTY), 3)) Then
                                Me.SqlCom.CommandText = "SET NOCOUNT ON;DELETE FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID AND " _
                                                      & " GQSY_SGT_P_FLAG = 'RMOA' AND OA_RM_ID IS NOT NULL;" _
                                                      & " DELETE FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID AND FLAG = 'RMOA';"
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                                Query = "SET NOCOUNT ON;" & vbCrLf & _
                                        "INSERT INTO ORDR_OA_REMAINDING(OA_BRANDPACK_ID,AGREE_DISC_HIST_ID,BRND_B_S_ID,ACHIEVEMENT_BRANDPACK_ID,MRKT_DISC_HIST_ID," & vbCrLf & _
                                        "MRKT_M_S_ID,PROJ_DISC_HIST_ID,FLAG,QTY,RELEASE_QTY,LEFT_QTY,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                                        "VALUES(@OA_BRANDPACK_ID,@AGREE_DISC_HIST_ID,@BRND_B_S_ID,@ACHIEVEMENT_BRANDPACK_ID,@MRKT_DISC_HIST_ID,@MRKT_M_S_ID," & vbCrLf & _
                                        "@PROJ_DISC_HIST_ID,@FLAG,@QTY,@RELEASE_QTY,@LEFT_QTY,@CREATE_BY,GETDATE())"
                                Me.ResetCommandText(CommandType.Text, Query)
                                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                                Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 115) ' VARCHAR(110),
                                Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, DBNull.Value, 60) ' VARCHAR(60),
                                Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 115) ' VARCHAR(110),
                                Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, DBNull.Value, 60) ' VARCHAR(60),
                                Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 66) ' VARCHAR(66),
                                Me.AddParameter("@FLAG", SqlDbType.VarChar, "RMOA") ' VARCHAR(5),
                                Me.AddParameter("@QTY", SqlDbType.Decimal, Left_QTY)
                                Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' CObj("0"), 13) ' VARCHAR(13),
                                Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, Left_QTY)
                                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(50)
                                System.Windows.Forms.MessageBox.Show("Because OA_ORIGINAL_Qty is changed" & vbCrLf & "You should insert again OA_Remaind_Qty", "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                Me.SqlCom.ExecuteScalar()
                            End If
                        Else
                            Query = "SET NOCOUNT ON;" & vbCrLf & _
                                    "INSERT INTO ORDR_OA_REMAINDING(OA_BRANDPACK_ID,AGREE_DISC_HIST_ID,BRND_B_S_ID,ACHIEVEMENT_BRANDPACK_ID,MRKT_DISC_HIST_ID," & vbCrLf & _
                                    "MRKT_M_S_ID,PROJ_DISC_HIST_ID,FLAG,QTY,RELEASE_QTY,LEFT_QTY,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                                    "VALUES(@OA_BRANDPACK_ID,@AGREE_DISC_HIST_ID,@BRND_B_S_ID,@ACHIEVEMENT_BRANDPACK_ID,@MRKT_DISC_HIST_ID,@MRKT_M_S_ID," & vbCrLf & _
                                    "@PROJ_DISC_HIST_ID,@FLAG,@QTY,@RELEASE_QTY,@LEFT_QTY,@CREATE_BY,GETDATE())"
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                            Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 115) ' VARCHAR(110),
                            Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, DBNull.Value, 60) ' VARCHAR(60),
                            Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 115) ' VARCHAR(110),
                            Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, DBNull.Value, 60) ' VARCHAR(60),
                            Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 66) ' VARCHAR(66),
                            Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, DBNull.Value, 70)
                            Me.AddParameter("@FLAG", SqlDbType.VarChar, "RMOA") ' VARCHAR(5),
                            Me.AddParameter("@QTY", SqlDbType.Decimal, Left_QTY)
                            Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' CObj("0"), 13) ' VARCHAR(13),
                            Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, Left_QTY)
                            Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(50)
                            Me.SqlCom.ExecuteScalar()
                        End If
                    Else
                        Me.SqlCom.CommandText = "SET NOCOUNT ON;DELETE FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID" _
                                                & " AND GQSY_SGT_P_FLAG = 'RMOA' AND OA_RM_ID IS NOT NULL;" & vbCrLf _
                                                & "DELETE FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID AND FLAG = 'RMOA';"
                        Me.SqlCom.CommandType = CommandType.Text
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_ID)
                        Me.SqlCom.ExecuteScalar() ': Me.CommiteTransaction() : Me.CloseConnection()
                    End If
                    Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                    If Not IsNothing(m_ViewOABrandPack) Then
                        Me.SaveToDataView(SaveType, Me.OA_BRANDPACK_ID)
                    End If
                Else
                    Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                    Throw New Exception("unknown error.")
                End If
            Catch EX As DBConcurrencyException
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw New System.Exception(Me.MessageDBConcurency)
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub


        Public Sub DeleteOA_BRANDPACK(ByVal OA_BRANDPACK_ID As String, Optional ByVal OA_RM_ID As String = "")
            Try
                'CHECK OA_RM_ID
                'Usp_Check_OA_RM_ID_IN_OA_BRANDPACK_DISC
                Dim objOARMID As Object = Nothing
                If OA_RM_ID <> "" Then
                    Me.CreateCommandSql("Usp_Check_OA_RM_ID_IN_OA_BRANDPACK_DISC", "")
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                    Me.AddParameter("@OA_RM_ID", SqlDbType.VarChar, OA_RM_ID, 200) ' VARCHAR(150)
                    objOARMID = Me.ExecuteScalar()
                End If
                If (IsNothing(objOARMID)) Or (IsDBNull(objOARMID)) Then
                    'DELETE RM_OA DI ORDR_OA_REMAINDING
                    Me.CreateCommandSql("", "DELETE FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'")
                    Me.OpenConnection() : Me.BeginTransaction() : Me.ExecuteNonQuery()
                    Me.SqlCom.CommandText = "Sp_Delete_OA_BRANDPACK_ID"
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                    'Me.SqlCom.CommandText = "DELETE FROM ORDR_OA_BRANDPACK WHERE OA_BRANDPACK_ID = '" & OA_BRANDPACK_ID & "'"
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                    Me.SqlCom.ExecuteNonQuery()
                    Me.CommiteTransaction()
                    Me.CloseConnection()
                    Me.ClearCommandParameters()
                Else
                    Me.CloseConnection()
                    Throw New Exception(Me.MessageCantDeleteData)
                End If
            Catch ex As DBConcurrencyException
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw New System.Exception(Me.MessageDBConcurency)
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

#End Region

#Region " Property "
        Public ReadOnly Property ViewOABRANDPACK() As DataView
            Get
                Return m_ViewOABrandPack
            End Get
        End Property
        Public ReadOnly Property ViewPOBrandpack() As DataView
            Get
                Return Me.m_ViewBrandPack
            End Get
        End Property
        Public ReadOnly Property ViewOARefNo() As DataView
            Get
                Return Me.m_ViewOARef
            End Get
        End Property
        Public ReadOnly Property ViewOASMS() As DataView
            Get
                Return Me.m_ViewOASMS
            End Get
        End Property
        Public ReadOnly Property ViewOALeftRemainding() As DataView
            Get
                Return Me.m_ViewOARemainding
            End Get
        End Property
        Public Property ViewLeftRemainding() As DataView
            Get
                Return Me.M_ViewRemainding
            End Get
            Set(ByVal value As DataView)

            End Set
        End Property
#End Region

    End Class

End Namespace

