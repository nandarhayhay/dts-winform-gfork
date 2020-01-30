Imports System.Data
Namespace OrderAcceptance
    Public Class OARegistering
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Protected m_ViewOARegistering As DataView
        Private m_ViewOARegistering_1 As DataView
        Protected m_ViewPO As DataView
        Protected m_ViewShipTo As DataView
        Public DISTRIBUTOR_NAME As String
        Public DSTRIBUTOR_ID As String
        Public OA_ID As String
        Public OA_DATE As Object
        Protected Query As String = ""
        Protected drv As DataRowView
        Public PO_REF_NO As String
        Private m_ViewOA As DataView
        Private m_ViewTerritory As DataView
        Private OACOUNT As Integer
        Public OA_SHIP_TO_ID As String ' VARCHAR(50),
        Public DIST_SHIP_TO_ID As String 'VARCHAR(20),
        Private m_ViewTM As DataView
        Public SHIP_TO_ID As String
        Public PO_REF_DATE As DateTime
        Public RunNumber As String
        Public ReadOnly Property ViewTM() As DataView
            Get
                Return Me.m_ViewTM
            End Get
        End Property
        Public Function GetTM(ByVal DISTRIBUTOR_ID As String) As DataView
            Try
                'Me.CreateCommandSql("Usp_Get_TM_by_Distributor_ID", "")
                'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Query = "SET NOCOUNT ON ; " & vbCrLf & _
                        "SELECT ST.SHIP_TO_ID,TER.TERRITORY_AREA,TM.MANAGER " & vbCrLf & _
                        " FROM TERRITORY TER INNER JOIN SHIP_TO ST ON TER.TERRITORY_ID = ST.TERRITORY_ID " & vbCrLf & _
                        " INNER JOIN TERRITORY_MANAGER TM ON ST.TM_ID = TM.TM_ID  WHERE ST.INACTIVE = 0 ;"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblTM As New DataTable("TM")
                tblTM.Clear()
                Me.FillDataTable(tblTM)
                Me.m_ViewTM = tblTM.DefaultView()
                Me.m_ViewTM.Sort = "MANAGER ASC"
                Return Me.m_ViewTM
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function CreateViewterritory() As DataView
            Try
                Me.SearcData("Usp_Select_Territory_Area", "")
                Me.m_ViewTerritory = Me.baseChekTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewTerritory
        End Function
        Public Function GetOACount() As Integer
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT MAX(IDApp) FROM ORDR_ORDER_ACCEPTANCE ;"
                'Query = "SET NOCOUNT ON; " & vbCrLf & _
                '   "SELECT [ROWS] FROM sysindexes  WHERE id = OBJECT_ID('ORDR_ORDER_ACCEPTANCE') AND INDID < 2 ;"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                'Me.CreateCommandSql("", "SELECT OA_COUNT FROM OACOUNT")
                Return CInt(Me.ExecuteScalar())
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Private Function getIDApp(ByVal mustCloseConnection As Boolean) As Integer
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT MAX(IDApp) FROM ORDR_ORDER_ACCEPTANCE ;"
                'Query = "SET NOCOUNT ON; " & vbCrLf & _
                '   "SELECT [ROWS] FROM sysindexes  WHERE id = OBJECT_ID('ORDR_ORDER_ACCEPTANCE') AND INDID < 2 ;"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                'Me.CreateCommandSql("", "SELECT OA_COUNT FROM OACOUNT")
                Dim retval As Integer = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                If (mustCloseConnection) Then : Me.CloseConnection() : End If
                Return retval
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        'Public Function GetShipToDistributorByOA_ID(ByVal OA_ID As String) As String
        '    Try
        '        Me.CreateCommandSql("", "SELECT DISTRIBUTOR_ID FROM DIST_SHIP_TO WHERE DIST_SHIP_TO_ID = (" & _
        '        "SELECT TOP 1 DIST_SHIP_TO_ID FROM OA_SHIP_TO WHERE OA_ID = '" & OA_ID & "')")
        '        Dim retval As Object = Me.ExecuteScalar()
        '        If IsNothing(retval) Then
        '            Throw New Exception("DISTRIBUTOR_SHIP_TO is unknown")
        '        End If
        '        Return retval.ToString()
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        'End Function
        'Public Function getTMByOAID(ByVal OA_ID As String) As Object()
        '    Try

        '        Me.CreateCommandSql("Usp_Create_View_TM_by_OA_ID", "")
        '        Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 30)
        '        Dim tblTerritory As New DataTable("Territory")
        '        tblTerritory.Clear()
        '        Me.FillDataTable(tblTerritory)
        '        If tblTerritory.Rows.Count > 0 Then
        '            Dim retval(tblTerritory.Rows.Count - 1) As Object
        '            For i As Integer = 0 To tblTerritory.Rows.Count - 1
        '                retval(i) = tblTerritory.Rows(i)("SHIP_TO_ID")
        '            Next
        '            Return retval
        '        End If
        '        Return Nothing
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        'End Function
        Public Function getTMByOAID(ByVal OA_ID As String) As Object
            Try
                'Me.CreateCommandSql("Usp_Create_View_TM_by_OA_ID", "")
                Query = "SET NOCOUNT ON ; " & vbCrLf & _
                         "SELECT TOP 1 ST.SHIP_TO_ID " & vbCrLf & _
                        " FROM TERRITORY TER INNER JOIN SHIP_TO ST ON TER.TERRITORY_ID = ST.TERRITORY_ID " & vbCrLf & _
                        " INNER JOIN TERRITORY_MANAGER TM ON ST.TM_ID = TM.TM_ID " & vbCrLf & _
                        " WHERE ST.SHIP_TO_ID = ANY(SELECT SHIP_TO_ID FROM OA_SHIP_TO WHERE OA_ID = @OA_ID) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 32)
                'Dim tblTerritory As New DataTable("Territory") : tblTerritory.Clear()
                'Me.FillDataTable(tblTerritory)
                Return Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetShipToDistributorByOA_ID(ByVal OA_ID As String) As String
            Try
                Me.CreateCommandSql("", "SELECT TOP 1 DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = " & _
                "(SELECT TOP 1 PO_REF_NO FROM ORDR_ORDER_ACCEPTANCE WHERE OA_ID = '" & OA_ID & "')")
                Dim DISTRIBUTOR_ID As Object = Me.ExecuteScalar()
                If IsNothing(DISTRIBUTOR_ID) Then
                    Throw New Exception("Can not find distributor PO")
                End If
                Return DISTRIBUTOR_ID.ToString()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function GetDistributorTerritory(ByVal DISTRIBUTOR_ID As String) As Object
            Try
                Me.CreateCommandSql("", "SELECT TERRITORY_ID FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'")
                Return Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function GetDistributorShipToArea(ByVal DISTRIBUTOR_ID As String) As DataView
            Try
                Me.CreateCommandSql("Usp_Select_Territory_Area_By_Distributor_ID", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Dim tblDistShipTo As New DataTable("SHIP_TO_AREA")
                tblDistShipTo.Clear()
                Me.FillDataTable(tblDistShipTo)
                Me.m_ViewTerritory = tblDistShipTo.DefaultView()
                Return Me.m_ViewTerritory
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        'Protected Function RepLaceDotWithComa(ByVal text As String) As String
        '    Dim l As Integer = Len(Trim(text))
        '    Dim w As Integer = 1
        '    Dim s As String = ""
        '    Dim a As String = ""
        '    Do Until w = l + 1
        '        s = Mid(Trim(text), w, 1)
        '        If s = "." Then
        '            s = ","
        '        End If
        '        a = a & s
        '        w += 1
        '    Loop
        '    Return a
        'End Function
        Public Sub New()
            Me.OA_ID = ""
            Me.OA_DATE = Nothing
            Me.DSTRIBUTOR_ID = ""
            Me.DISTRIBUTOR_NAME = ""
            'Me.SHIP_TO_SHARE_PCT_1 = ""
            'Me.SHIP_TO_SHARE_PCT_2 = ""
            'Me.SHIP_TERRITORY_1 = ""
            'Me.SHIP_TERRITORY_2 = ""
            Me.PO_REF_NO = ""
        End Sub
        Public Function CreateViewOA(ByVal Distributor_ID As String) As DataView
            Try
                Me.CreateCommandSql("Sp_GetView_UNREFERENCED_OA_BY_DISTRIBUTOR_ID", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10)
                Dim tblOA As New DataTable("OA")
                tblOA.Clear()
                Me.FillDataTable(tblOA)
                Me.m_ViewOA = tblOA.DefaultView()
                Me.m_ViewOA.Sort = "OA_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewOA
        End Function
        Public Function ViewOARegistering() As DataView
            Me.FillDataTable("Sp_Select_OARegistering", "", "T_OrderAcceptance")
            Me.m_ViewOARegistering = Me.baseDataTable.DefaultView()
            Return Me.m_ViewOARegistering
        End Function
        Private Function SaveToDataViewOA(ByVal SaveType As String) As DataView
            Try
                With Me.m_ViewOARegistering_1
                    Select Case SaveType
                        Case "Save"
                            Me.drv = .AddNew()
                            drv("OA_ID") = Me.OA_ID
                            drv("SHIP_TO_DIST_ID") = Me.DSTRIBUTOR_ID
                            drv("DISTRIBUTOR_NAME") = Me.DISTRIBUTOR_NAME
                            drv("OA_DATE") = Me.OA_DATE
                            'drv("SHIP_TO_SHARE_PCT_1") = Me.RepLaceDotWithComa(Me.SHIP_TO_SHARE_PCT_1)
                            'drv("SHIP_TO_SHARE_PCT_2") = Me.RepLaceDotWithComa(Me.SHIP_TO_SHARE_PCT_2)
                            'drv("SHIP_TERRITORY_1") = Me.SHIP_TERRITORY_1
                            'drv("SHIP_TERRITORY_2") = Me.SHIP_TERRITORY_2
                            drv("PO_REF_NO") = Me.PO_REF_NO
                            drv.EndEdit()
                        Case "Update"
                            Dim index As Integer = Me.m_ViewOARegistering_1.Find(Me.OA_ID)
                            If index <> -1 Then
                                .Item(index)("OA_ID") = Me.OA_ID
                                .Item(index)("OA_DATE") = Me.OA_DATE
                                .Item(index)("SHIP_TO_DIST_ID") = Me.DSTRIBUTOR_ID
                                .Item(index)("DISTRIBUTOR_NAME") = Me.DISTRIBUTOR_NAME
                                '.Item(index)("SHIP_TO_SHARE_PCT_1") = Me.RepLaceDotWithComa(Me.SHIP_TO_SHARE_PCT_1)
                                '.Item(index)("SHIP_TO_SHARE_PCT_2") = Me.RepLaceDotWithComa(Me.SHIP_TO_SHARE_PCT_2)
                                '.Item(index)("SHIP_TERRITORRY_2") = Me.SHIP_TERRITORY_2
                                '.Item(index)("SHIP_TERRITORY_1") = Me.SHIP_TERRITORY_1
                                .Item(index)("PO_REF_NO") = Me.PO_REF_NO
                                .Item(index).EndEdit()
                            End If
                    End Select
                End With

            Catch ex As Exception
                Throw ex
            End Try
            Return Me.m_ViewOARegistering_1
        End Function
        'PROCEDURE UNTUK TERRITORY PADA SAAT OA_ID MAU DIEDIT I FORM ACCEPTANCE
        'public function 
        'Usp_Create_View_Territory_By_OA_ID()
        Public Function GetTerritoryByOAID(ByVal OA_ID As String) As Object()
            Try

                Me.CreateCommandSql("Usp_Create_View_Territory_By_OA_ID", "")
                Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 32)
                Dim tblTerritory As New DataTable("Territory")
                tblTerritory.Clear()
                Me.FillDataTable(tblTerritory)
                If tblTerritory.Rows.Count > 0 Then
                    Dim retval(tblTerritory.Rows.Count - 1) As Object
                    For i As Integer = 0 To tblTerritory.Rows.Count - 1
                        retval(i) = tblTerritory.Rows(i)("TERRITORY_ID")
                    Next
                    Return retval
                End If
                Return Nothing
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try

        End Function
        Public Sub GetRowByOARefNO(ByVal OA_REF_NO As String)
            Try
                'Me.CreateCommandSql("Usp_Create_View_Territory_By_OA_ID", "")
                'Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 30)

                Me.CreateCommandSql("", "SELECT TOP 1 OPO.PO_REF_NO,OPO.PO_REF_DATE,OOA.OA_DATE" & _
                                     " FROM ORDR_PURCHASE_ORDER OPO INNER JOIN ORDR_ORDER_ACCEPTANCE OOA" & _
                                    " ON OPO.PO_REF_NO = OOA.PO_REF_NO WHERE OOA.OA_ID = '" & OA_REF_NO & "'")
                Me.ExecuteReader()
                While Me.SqlRe.Read()
                    Me.OA_DATE = Me.SqlRe("OA_DATE")
                    Me.PO_REF_NO = Me.SqlRe("PO_REF_NO")
                    Me.PO_REF_DATE = CDate(Me.SqlRe("PO_REF_DATE"))
                    'Me.DSTRIBUTOR_ID = Me.SqlRe("SHIP_TO_DIST_ID")
                    'Me.SHIP_TO_SHARE_PCT_1 = Me.SqlRe("SHIP_TO_SHARE_PCT_1")
                    'Me.SHIP_TERRITORY_1 = Me.SqlRe("SHIP_TERRITORY_1")
                    'Me.SHIP_TO_SHARE_PCT_2 = Me.SqlRe("SHIP_TO_SHARE_PCT_2")
                    'Me.SHIP_TO_SHARE_PCT_1 = Me.SqlRe("SHIP_TERRITORY_2")
                End While
                Me.SqlRe.Close()
                Me.CloseConnection()
            Catch ex As Exception
                Me.SqlRe.Close()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Private Function generateOANum(ByVal PO_NUM As String) As String
            Dim num As String = "000000"
            Dim RetVal As String = ""
            Try
                Dim OACount As Integer = Me.getIDApp(False)
                'If OACount = 999999 Then
                '    OACount = 0
                'End If
                OACount += 1
                If OACount > 999999 Then
                    OACount = OACount Mod 999999
                End If
                Dim X As Integer = num.Length - CStr(OACount).Length
                If X = 0 Then
                    num = ""
                Else
                    num = num.Remove(X - 1, CStr(OACount).Length)
                End If
                num &= OACount.ToString() : Me.RunNumber = num
                RetVal = PO_NUM & "-" & num
            Catch ex As Exception

            End Try
            Return RetVal
        End Function

        Public Sub SaveOA(ByVal SaveType As String, ByVal SHIP_TO_IDS As Collection)
            Try
                'Me.OACOUNT = Me.GetOACount()
                'If Me.OACOUNT = 9999 Then
                '    Me.OACOUNT = 0
                'End If
                Me.GetConnection()
                Select Case SaveType
                    Case "Save"
                        'Me.OA_ID = Me.generateOANum(Me.PO_REF_NO)
                        Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_OA")
                        'Me.InsertData("Sp_Insert_OA", "")

                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                    Case "Update"
                        Me.UpdateData("Sp_Update_OA", "")
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                End Select

                Me.AddParameter("@OA_ID", SqlDbType.VarChar, Me.OA_ID, 32) ' VARCHAR(30),
                Me.AddParameter("@OA_DATE", SqlDbType.DateTime, Me.OA_DATE) ' DATETIME,
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, Me.PO_REF_NO, 25) ' VARCHAR(25)
                Me.AddParameter("@RUN_NUMBER", SqlDbType.VarChar, Me.RunNumber, 10)
                Me.OpenConnection() : Me.BeginTransaction()
                If Me.ExecuteNonQuery() > 0 Then
                    'Dim DIST_SHIP_TO_ID As String = ""
                    Dim OA_SHIP_TO_ID As String = ""
                    If SaveType = "Save" Then
                        For I As Integer = 1 To SHIP_TO_IDS.Count
                            Dim ShipToId As String = SHIP_TO_IDS(I).ToString()
                            OA_SHIP_TO_ID = Me.OA_ID & "" & ShipToId
                            Me.SqlCom.CommandText = "Usp_Insert_OA_Ship_To"
                            Me.SqlCom.CommandType = CommandType.StoredProcedure
                            Me.AddParameter("@OA_SHIP_TO_ID", SqlDbType.VarChar, OA_SHIP_TO_ID, 57) ' VARCHAR(50),
                            Me.AddParameter("@OA_ID", SqlDbType.VarChar, Me.OA_ID, 32) ' VARCHAR(30),
                            Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, ShipToId, 25) ' VARCHAR(20),
                            Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Next
                    Else
                        Me.SqlCom.CommandText = "Usp_Delete_OA_Ship_To"
                        Me.SqlCom.CommandType = CommandType.StoredProcedure
                        Me.AddParameter("@OA_ID", SqlDbType.VarChar, Me.OA_ID, 32)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        For I As Integer = 1 To SHIP_TO_IDS.Count
                            Dim ShipToId As String = SHIP_TO_IDS(I).ToString()
                            OA_SHIP_TO_ID = Me.OA_ID & "" & ShipToId
                            Me.SqlCom.CommandText = "Usp_Insert_OA_Ship_To"
                            Me.SqlCom.CommandType = CommandType.StoredProcedure
                            Me.AddParameter("@OA_SHIP_TO_ID", SqlDbType.VarChar, OA_SHIP_TO_ID, 57) ' VARCHAR(50),
                            Me.AddParameter("@OA_ID", SqlDbType.VarChar, Me.OA_ID, 32) ' VARCHAR(30),
                            Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, ShipToId, 25) ' VARCHAR(20),
                            Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Next
                    End If
                    Me.CommiteTransaction()
                Else
                    Me.RollbackTransaction()
                End If
                Me.CloseConnection()

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
        Public Function IsExistedOA(ByVal PO_REF_NO As String, ByVal SHIP_TO_ID As String) As Boolean
            Dim retVal As Boolean = False
            Try
                Me.CreateCommandSql("Sp_Check_Existing_OA", "")
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25)
                Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, SHIP_TO_ID, 25)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) Then
                    Me.ClearCommandParameters() : retVal = True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return retVal
        End Function
        Public Function IsExistedOA(ByVal OA_ID As String) As Boolean
            Dim retVal As Boolean = False
            Try

                Dim Query As String = "SELECT 1 WHERE EXISTS(SELECT OA_ID FROM ORDR_ORDER_ACCEPTANCE WHERE OA_ID = '" + OA_ID + "')"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                If CInt(Me.ExecuteScalar()) > 0 Then
                    retVal = True
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return retVal
        End Function
        Public Function OAHasReferencedData(ByVal OA_ID As String) As Boolean
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT TOP 1 OA_ID FROM ORDR_OA_BRANDPACK WHERE OA_ID = @OA_ID) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                'Me.CreateCommandSql("Sp_Check_REFERENCED_OA", "")
                Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 32)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                'If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                '    Me.ClearCommandParameters() : Return True
                'End If
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return (CInt(retval) > 0)
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function
        Public Sub DeleteOA(ByVal OA_ID As String)
            Try
                Me.CreateCommandSql("Sp_Delete_OA", "")
                Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 32)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Function SearchPoRefNo(ByVal PORefNo As String) As DataView
            Try
                Dim Query As String = "SET NOCOUNT ON;"
                Query &= vbCrLf
                Query &= " SELECT TOP 100 OPO.PO_REF_NO,OPO.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,OPO.PO_REF_DATE " & vbCrLf & _
                          " FROM ORDR_PURCHASE_ORDER OPO INNER JOIN DIST_DISTRIBUTOR DR ON OPO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID " & vbCrLf & _
                          " WHERE OPO.PO_REF_NO LIKE '%" & PORefNo & "%' AND EXISTS(SELECT PO_REF_NO FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = OPO.PO_REF_NO) " & vbCrLf & _
                          " ORDER BY OPO.PO_REF_DATE DESC;"
                Dim dtTbl As New DataTable("PO_Ref_No") : dtTbl.Clear()
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.FillDataTable(dtTbl)
                Me.m_ViewPO = dtTbl.DefaultView()
                Me.m_ViewPO.Sort = "PO_REF_NO"
                Return Me.m_ViewPO
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function CreateViewShipTo(ByVal ParentDistributorID As String, ByVal DISTRIBUTOR_NAME As String) As DataView
            Try
                Dim tblDistDropDown As New DataTable("Distributor")
                tblDistDropDown.Clear()
                Dim ColDISTRIBUTOR_ID As New DataColumn("DISTRIBUTOR_ID")
                ColDISTRIBUTOR_ID.DataType = Type.GetType("System.String")
                ColDISTRIBUTOR_ID.Unique = True
                tblDistDropDown.Columns.Add(ColDISTRIBUTOR_ID)
                Dim ColDISTRIBUTOR_NAME As New DataColumn("DISTRIBUTOR_NAME")
                ColDISTRIBUTOR_NAME.DataType = Type.GetType("System.String") ' As New DataColumn("Discount")
                tblDistDropDown.Columns.Add(ColDISTRIBUTOR_NAME)
                Dim row As DataRow = tblDistDropDown.NewRow()
                row("DISTRIBUTOR_ID") = ParentDistributorID
                row("DISTRIBUTOR_NAME") = DISTRIBUTOR_NAME
                tblDistDropDown.Rows.Add(row)
                Me.SearcData("", "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR WHERE PARENT_DISTRIBUTOR_ID = '" & ParentDistributorID & "'")
                If Me.baseChekTable.Rows.Count > 0 Then
                    For i As Integer = 0 To Me.baseChekTable.Rows.Count - 1
                        row = tblDistDropDown.NewRow()
                        row("DISTRIBUTOR_ID") = Me.baseChekTable.Rows(i)("DISTRIBUTOR_ID")
                        row("DISTRIBUTOR_NAME") = Me.baseChekTable.Rows(i)("DISTRIBUTOR_NAME")
                        tblDistDropDown.Rows.Add(row)
                    Next
                End If
                Me.m_ViewShipTo = tblDistDropDown.DefaultView()
                Me.m_ViewShipTo.Sort = "DISTRIBUTOR_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewShipTo
        End Function
        Public Function CreateViewPORefNO() As DataView
            Try
                Me.CreateCommandSql("Sp_GetView_DISTRIBUTOR_PO", "")
                Dim tblDistPO As New DataTable("DISTRIBUTOR_PO")
                tblDistPO.Clear()
                Me.FillDataTable(tblDistPO)
                Me.m_ViewPO = tblDistPO.DefaultView()
                'Me.m_ViewPO.Sort = "PO_REF_NO"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewPO
        End Function
        Public Function GetDistributorName(ByVal DISTRIBUTOR_ID As String) As String
            Try
                Return Me.ExecuteScalar("", "SELECT DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'").ToString()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function CreateViewOARegistering_1(ByVal OA_ID As String) As DataView
            Try
                Me.CreateCommandSql("", "SELECT ORDR_ORDER_ACCEPTANCE.OA_ID,ORDR_ORDER_ACCEPTANCE.OA_DATE,ORDR_ORDER_ACCEPTANCE.PO_REF_NO " & _
                ",ORDR_ORDER_ACCEPTANCE.SHIP_TO_DIST_ID,DIST_DISTRIBUTOR.DISTRIBUTOR_NAME,ORDR_ORDER_ACCEPTANCE.SHIP_TO_SHARE_PCT FROM ORDR_ORDER_ACCEPTANCE" & _
                " INNER JOIN DIST_DISTRIBUTOR ON ORDR_ORDER_ACCEPTANCE.SHIP_TO_DIST_ID = DIST_DISTRIBUTOR.DISTRIBUTOR_ID WHERE OA_ID = '" + OA_ID + "'")
                Dim tblOA As New DataTable("ORDER_ACCEPTANCE")
                tblOA.Clear()
                Me.FillDataTable(tblOA)
                Me.m_ViewOARegistering_1 = tblOA.DefaultView()
                Me.m_ViewOARegistering_1.Sort = "OA_ID"
                Me.m_ViewOARegistering_1.RowStateFilter = DataViewRowState.CurrentRows
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewOARegistering_1
        End Function

        Public ReadOnly Property ViewOARegistering_1() As DataView
            Get
                Return Me.m_ViewOARegistering_1
            End Get
        End Property
        Public ReadOnly Property ViewShipTo() As DataView
            Get
                Return Me.m_ViewShipTo
            End Get
        End Property
        Public ReadOnly Property ViewPO() As DataView
            Get
                Return Me.m_ViewPO
            End Get
        End Property
        Public ReadOnly Property ViewOA() As DataView
            Get
                Return Me.m_ViewOA
            End Get
        End Property
        Public ReadOnly Property ViewTerritory() As DataView
            Get
                Return Me.m_ViewTerritory
            End Get
        End Property
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewOARegistering_1) Then
                Me.m_ViewOARegistering_1.Dispose()
                Me.m_ViewOARegistering_1 = Nothing
            End If
            If Not IsNothing(Me.m_ViewOARegistering) Then
                Me.m_ViewOARegistering.Dispose()
                Me.m_ViewOARegistering = Nothing
            End If
            If Not IsNothing(Me.m_ViewPO) Then
                Me.m_ViewPO.Dispose()
                Me.m_ViewPO = Nothing
            End If
            If Not IsNothing(Me.m_ViewShipTo) Then
                Me.m_ViewShipTo.Dispose()
                Me.m_ViewShipTo = Nothing
            End If
            If Not IsNothing(Me.m_ViewOA) Then
                Me.m_ViewOA.Dispose()
                Me.m_ViewOA = Nothing
            End If
            If Not IsNothing(Me.drv) Then
                Me.drv = Nothing
            End If
        End Sub
    End Class
End Namespace

