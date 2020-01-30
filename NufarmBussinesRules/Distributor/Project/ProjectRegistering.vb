Imports System.Data
Imports System.Data.SqlClient
Namespace DistributorProject
    Public Class ProjectRegistering
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
#Region " Deklarasi "
        Private m_dataSet As DataSet 'dataset for tracking changes in ViewProjBranpack
        Private m_ViewDistributor As DataView ' view distributor from agreement which still aplies
        Private m_ViewBrandPack As DataView 'view brandpack from agreement which still aplies
        Private m_tblProjBPDetail As DataTable 'for getting datatable from PB
        'Private DVM As DataViewManager 'for binding to datagrid main
        Private m_DSBrandPackDetail As DataSet
        Public Proj_Ref_No As String = ""
        Public Proj_Ref_Name As String = ""
        Public Distributor_ID As String = ""
        Public StartDate As DateTime, EndDate As DateTime
        Public Address As String = ""
        Public Conntact As String = ""
        Public Phone As String = ""
        Public Fax As String = ""
        Public HP As String = ""
        Public DSPBDetailHasChanges As Boolean = False
        Private PB As ProjectBrandPack
        Private m_ViewDistDropDown As DataView
        Private m_ViewProjBrandPack As DataView 'dataview for project brandpack
        Private m_ViewProject As DataView
        Dim colDistributor As Hashtable
        Protected Query As String = ""
#End Region

#Region " Function "
        Public Function GetRefencedData(ByVal ListbrandPacks As List(Of String), ByVal PROJ_REF_NO As String, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Dim tblRef As New DataTable("T_Ref")
                With tblRef
                    .Columns.Add("PROJ_BRANDPACK_ID", Type.GetType("System.String"))
                    .Columns.Add("BRANDPACK_ID", Type.GetType("System.String"))
                    .Columns.Add("BRANDPACK_NAME", Type.GetType("System.String"))
                    .Columns.Add("HasReference", Type.GetType("System.Boolean"))
                    .Columns.Add("PO_REF_NO", Type.GetType("System.String"))
                End With
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT BP.BRANDPACK_NAME,OPB.BRANDPACK_ID,OPB.PO_REF_NO,OPB.PROJ_BRANDPACK_ID FROM BRND_BRANDPACK BP INNER JOIN BRND_BRANDPACK BB ON BP.BRANDPACK_ID = OPB.BRANDPACK_ID WHERE OPB.PROJ_REF_NO = @PROJ_REF_NO ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, PROJ_REF_NO, 15)
                Me.OpenConnection() : Me.SqlRe = Me.SqlCom.ExecuteReader() : Me.ClearCommandParameters()
                While Me.SqlRe.Read()
                    Dim ProjBrandPackID As String = Me.SqlRe.GetString(3)
                    Dim BrandPackID As String = Me.SqlRe.GetString(1)
                    Dim row As DataRow = tblRef.NewRow()
                    row("PROJ_BRANDPACK_ID") = ProjBrandPackID
                    row("BRANDPACK_ID") = BrandPackID
                    row("BRANDPACK_NAME") = SqlRe.GetString(0)
                    If (ListbrandPacks.Contains(BrandPackID)) Then
                        row("HasReference") = True
                    Else
                        row("HasReference") = False
                    End If
                    row("PO_REF_NO") = SqlRe.GetString(2)
                    row.EndEdit()
                End While : Me.SqlRe.Close()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return tblRef
            Catch ex As Exception
                If (Not Me.SqlRe.IsClosed()) Then : Me.SqlRe.Close() : End If
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function GetBrandPack(ByVal PROJ_REF_NO As String, ByVal DISTRIBUTOR_ID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByRef ListBrandPackID As List(Of String), ByVal mustCloseConnection As Boolean) As DataTable
            Try
                'get BrandPack By PK based on DateReference
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT ABI.BRANDPACK_ID,BP.BRANDPACK_NAME FROM AGREE_BRANDPACK_INCLUDE ABI INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                        " INNER JOIN AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO AND ABI.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = ABI.BRANDPACK_ID " & vbCrLf & _
                        " WHERE AA.START_DATE <= @StartDate AND AA.END_DATE >= @EndDate AND DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Dim tblBrandPack As New DataTable("T_BrandPack") : tblBrandPack.Clear()
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                Me.OpenConnection() : Me.SqlDat.Fill(tblBrandPack) : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT PROJ_BRANDPACK_ID,PROJ_REF_NO,BRANDPACK_ID,PRICE,CREATE_DATE,CREATE_BY,MODIFY_DATE,MODIFY_BY FROM PROJ_BRANDPACK WHERE PROJ_REF_NO = @PROJ_REF_NO ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, PROJ_REF_NO, 30)
                Dim tbllistBrandPack As New DataTable("T_BrandPack") : tbllistBrandPack.Clear()
                Me.SqlDat.SelectCommand = Me.SqlCom
                Me.SqlDat.Fill(tbllistBrandPack) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                'add column to tblBrandPack
                Dim colProjRefBrandPackID As New DataColumn("PROJ_BRANDPACK_ID", Type.GetType("System.String"))
                colProjRefBrandPackID.DefaultValue = DBNull.Value
                tblBrandPack.Columns.Add(colProjRefBrandPackID)

                Dim colProjRefNO As New DataColumn("PROJ_REF_NO", Type.GetType("System.String"))
                colProjRefNO.DefaultValue = DBNull.Value
                tblBrandPack.Columns.Add(colProjRefNO)

                tblBrandPack.Columns.Add("PRICE", Type.GetType("System.Decimal"))
                tblBrandPack.Columns("PRICE").DefaultValue = DBNull.Value

                Dim colCreatedDate As New DataColumn("CREATE_DATE", Type.GetType("System.DateTime"))
                colCreatedDate.DefaultValue = NufarmBussinesRules.SharedClass.ServerDate
                tblBrandPack.Columns.Add(colCreatedDate)

                Dim colCreatedBy As New DataColumn("CREATE_BY", Type.GetType("System.String"))
                colCreatedBy.DefaultValue = NufarmBussinesRules.User.UserLogin.UserName
                tblBrandPack.Columns.Add(colCreatedBy)

                Dim colModifiedDate As New DataColumn("MODIFY_DATE", Type.GetType("System.DateTime"))
                colModifiedDate.DefaultValue = DBNull.Value
                tblBrandPack.Columns.Add(colModifiedDate)

                Dim colModifiedBy As New DataColumn("MODIFY_BY", Type.GetType("System.String"))
                colModifiedBy.DefaultValue = DBNull.Value
                tblBrandPack.Columns.Add(colModifiedBy)

                Dim colHasChanged As New DataColumn("HAS_CHANGED", Type.GetType("System.Boolean"))
                colHasChanged.DefaultValue = False
                tblBrandPack.Columns.Add(colHasChanged)

                Dim DV As DataView = tblBrandPack.DefaultView()
                DV.Sort = "BRANDPACK_ID"
                If tbllistBrandPack.Rows.Count > 0 Then
                    'ListBrandPackID = New Object(tbllistBrandPack.Rows.Count) {}
                    ListBrandPackID.Clear()
                    For i As Integer = 0 To tbllistBrandPack.Rows.Count - 1
                        'ListBrandPackID(i) = tbllistBrandPack.Rows(i)("BRANDPACK_ID")
                        If Not ListBrandPackID.Contains(tbllistBrandPack.Rows(i)("BRANDPACK_ID").ToString()) Then
                            ListBrandPackID.Add(tbllistBrandPack.Rows(i)("BRANDPACK_ID").ToString())
                        End If
                        Dim Index As Integer = DV.Find(tbllistBrandPack.Rows(i)("BRANDPACK_ID"))
                        If Index <> -1 Then
                            DV(Index).BeginEdit()
                            DV(Index)("PROJ_BRANDPACK_ID") = tbllistBrandPack.Rows(i)("PROJ_BRANDPACK_ID")
                            DV(Index)("PROJ_REF_NO") = tbllistBrandPack.Rows(i)("PROJ_REF_NO")
                            DV(Index)("PRICE") = tbllistBrandPack.Rows(i)("PRICE")
                            DV(Index)("CREATE_BY") = tbllistBrandPack.Rows(i)("CREATE_DATE")
                            DV(Index)("CREATE_BY") = tbllistBrandPack.Rows(i)("CREATE_BY")
                            DV(Index)("MODIFY_BY") = tbllistBrandPack.Rows(i)("MODIFY_BY")
                            DV(Index)("MODIFY_DATE") = tbllistBrandPack.Rows(i)("MODIFY_DATE")
                            DV(Index).EndEdit()
                        End If
                    Next
                    tblBrandPack.AcceptChanges()
                End If
                Return tblBrandPack
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function IsExisted(ByVal Proj_ref_No As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                'Me.GetConnection()
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT 1 WHERE EXISTS(SELECT PROJ_REF_NO FROM PROJ_PROJECT WHERE PROJ_REF_NO = @PROJ_REF_NO) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, Proj_ref_No, 15)
                Me.OpenConnection() : Dim retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then
                        Me.CloseConnection() : Return True
                    End If
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                'If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                '    Return True
                'End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return False
        End Function
        'FUCNTION INI BERFUNGSI UNTUK MENGECEK REFERENSI DATA DI TABLE ORDR_PURCHASE_ORDER
        'BILA DI TABLE BRANPACK DETAIL TIDAK ADA ITEMNYA
        Public Function HasreferencedData(ByVal PROJ_REF_NO As String) As Boolean
            Try
                Me.GetConnection()
                Me.CreateCommandSql("Sp_Select_REFERENCE_PROJ_PROJECT", "")
                Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, PROJ_REF_NO, 15)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
            End Try
            Return False
        End Function
        Public Function HasRefernceDataBPDetail(ByVal PROJ_BRANDPACK_ID As String, ByVal MustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT 1 WHERE EXISTS(SELECT PROJ_BRANDPACK_ID FROM ORDR_PO_BRANDPACK WHERE PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, PROJ_BRANDPACK_ID, 30)
                Me.OpenConnection() : Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then
                        Me.CloseConnection() : Return True
                    End If
                End If
                If MustCloseConnection Then : Me.CloseConnection() : End If
                Return False
                'Me.CreateCommandSql("Sp_Select_REFERENCED_PROJ_BRANDPACK", "")
                'Me.AddParameter("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, PROJ_BRANDPACK_ID, 25) ' VARCHAR(15)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.VarChar, ParameterDirection.ReturnValue)
                'If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                '    Return True
                'End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function
        Public Function getProjectDetail(ByVal FromDate As DateTime, ByVal UntilDate As DateTime, ByVal mustCloseConnection As Boolean, Optional ByVal DistributorID As String = "") As DataView
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                "SELECT PB.PROJ_BRANDPACK_ID,P.PROJ_REF_NO,P.PROJECT_NAME,P.START_DATE,P.END_DATE,P.ADDRESS AS PROJECT_ADDRESS,P.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,PB.BRANDPACK_ID,BP.BRANDPACK_NAME,PB.PRICE " & vbCrLf & _
                "FROM PROJ_BRANDPACK PB INNER JOIN PROJ_PROJECT P ON PB.PROJ_REF_NO = P.PROJ_REF_NO INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = P.DISTRIBUTOR_ID " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = PB.BRANDPACK_ID WHERE P.END_DATE >= @FromDate AND P.END_DATE <= @UntilDate ;"
                If Not String.IsNullOrEmpty(DistributorID) Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                              "SELECT PB.PROJ_BRANDPACK_ID,P.PROJ_REF_NO,P.PROJECT_NAME,P.START_DATE,P.END_DATE,P.ADDRESS AS PROJECT_ADDRESS,P.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,BP.BRANDPACK_NAME,PB.PRICE " & vbCrLf & _
                              "FROM PROJ_BRANDPACK PB INNER JOIN PROJ_PROJECT P ON PB.PROJ_REF_NO = P.PROJ_REF_NO INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = P.DISTRIBUTOR_ID " & vbCrLf & _
                              " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = PB.BRANDPACK_ID WHERE P.START_DATE >= @FromDate AND P.END_DATE <= @UntilDate AND DR.DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                If Not String.IsNullOrEmpty(DistributorID) Then
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 14)
                End If
                Me.AddParameter("@FromDate", SqlDbType.SmallDateTime, FromDate)
                Me.AddParameter("@UntilDate", SqlDbType.SmallDateTime, UntilDate)
                Dim tblProj As New DataTable("SALES PROJECT DISTRIBUTOR") : tblProj.Clear()
                Me.OpenConnection() : Me.SqlDat.Fill(tblProj) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return tblProj.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function CreateViewDistDropDown(ByVal ParentDistributorID As String, ByVal distributor_name As String) As DataView
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
                '    ColMore100.DataType = Type.GetType("System.Decimal")
                '    ColMore100.Caption = "More than 100 %"
                '    Me.m_dataTableYearly.Columns.Add(ColMore100)
                '    ColDiscount.DataType = Type.GetType("System.Decimal")
                '    ColDiscount.Caption = "Discount"
                '    Me.m_dataTableYearly.Columns.Add(ColDiscount)
                Dim row As DataRow = tblDistDropDown.NewRow()
                row("DISTRIBUTOR_ID") = ParentDistributorID
                row("DISTRIBUTOR_NAME") = distributor_name
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

                'Me.GetConnection()
                'Me.CreateCommandSql("", "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR")
                'Dim tblDistributor As New DataTable("T_Distributor")
                'tblDistributor.Clear()
                'Me.FillDataTable(tblDistributor)
                Me.m_ViewDistDropDown = tblDistDropDown.DefaultView()
                Me.m_ViewDistDropDown.Sort = "DISTRIBUTOR_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistDropDown
        End Function
        Public Function CreateViewBrandPack(ByVal DISTRIBUTOR_ID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                      "SELECT ABI.BRANDPACK_ID,BP.BRANDPACK_NAME FROM AGREE_BRANDPACK_INCLUDE ABI INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                      " INNER JOIN AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO AND ABI.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = ABI.BRANDPACK_ID " & vbCrLf & _
                      " WHERE AA.START_DATE <= @StartDate AND AA.END_DATE >= @EndDate AND DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Dim tblBrandPackInclude As New DataTable("T_BrandPack_Include")
                tblBrandPackInclude.Clear()
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                Me.OpenConnection()
                Me.SqlDat.Fill(tblBrandPackInclude) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                ''add column price
                Dim colProjRefBrandPackID As New DataColumn("PROJ_BRANDPACK_ID", Type.GetType("System.String"))
                colProjRefBrandPackID.DefaultValue = DBNull.Value
                tblBrandPackInclude.Columns.Add(colProjRefBrandPackID)

                Dim colProjRefNO As New DataColumn("PROJ_REF_NO", Type.GetType("System.String"))
                colProjRefNO.DefaultValue = DBNull.Value
                tblBrandPackInclude.Columns.Add(colProjRefNO)

                Dim colPrice As New DataColumn("PRICE", Type.GetType("System.Decimal"))
                colPrice.DefaultValue = DBNull.Value
                tblBrandPackInclude.Columns.Add(colPrice)

                Dim colCreatedDate As New DataColumn("CREATE_DATE", Type.GetType("System.DateTime"))
                colCreatedDate.DefaultValue = NufarmBussinesRules.SharedClass.ServerDate
                tblBrandPackInclude.Columns.Add(colCreatedDate)

                Dim colCreatedBy As New DataColumn("CREATE_BY", Type.GetType("System.String"))
                colCreatedBy.DefaultValue = NufarmBussinesRules.User.UserLogin.UserName
                tblBrandPackInclude.Columns.Add(colCreatedBy)

                Dim colModifiedDate As New DataColumn("MODIFY_DATE", Type.GetType("System.DateTime"))
                colModifiedDate.DefaultValue = DBNull.Value
                tblBrandPackInclude.Columns.Add(colModifiedDate)

                Dim colModifiedBy As New DataColumn("MODIFY_BY", Type.GetType("System.String"))
                colModifiedBy.DefaultValue = DBNull.Value
                tblBrandPackInclude.Columns.Add(colModifiedBy)

                Dim colHasChanged As New DataColumn("HAS_CHANGED", Type.GetType("System.Boolean"))
                colHasChanged.DefaultValue = False
                tblBrandPackInclude.Columns.Add(colHasChanged)

                'Me.FillDataTable(tblBrandPackInclude)

                Return tblBrandPackInclude
                'Me.m_ViewBrandPack.Sort = "BRANDPACK_ID"
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try

        End Function
        Public Function CreateViewBrandPack() As DataView
            Try
                Me.CreateCommandSql("", "SELECT BRANDPACK_ID,BRANDPACK_NAME FROM BRND_BRANDPACK")
                Dim tblBrandPack As New DataTable("BRANDPACK")
                tblBrandPack.Clear()
                Me.FillDataTable(tblBrandPack)
                Me.m_ViewBrandPack = tblBrandPack.DefaultView()
                Me.m_ViewBrandPack.Sort = "BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewBrandPack
        End Function
        Public Function CreateViewDistributor(ByVal SearchString As String, ByVal IsMustExistsPKD As Boolean, ByVal StartDate As Object, ByVal EndDate As DateTime, ByVal mustCloseConnection As Boolean) As DataView
            Try
                Query = "SELECT DR.DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR " & vbCrLf

                If Not String.IsNullOrEmpty(SearchString) Then
                    Query = Query.Replace(";", "")
                    Query &= vbCrLf & " WHERE DR.DISTRIBUTOR_NAME LIKE '%'+@SearchString+'%' "
                End If
                If IsMustExistsPKD Then
                    If Not String.IsNullOrEmpty(SearchString) Then : Query &= " AND " : Else : Query &= " WHERE " : End If
                    Query &= " EXISTS(SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                    " WHERE AA.END_DATE >= @EndDate AND AA.START_DATE <= @StartDate AND DR.DISTRIBUTOR_ID = DA.DISTRIBUTOR_ID) ;"
                End If
                Query = Query.Insert(0, "SET NOCOUNT ON ; " & vbCrLf)
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                If Not String.IsNullOrEmpty(SearchString) Then
                    Me.AddParameter("@SearchString", SqlDbType.VarChar, SearchString, 100)
                End If
                If IsMustExistsPKD Then
                    Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                    Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                End If
                Dim tblDistributor As New DataTable("T_Distributor")
                'Me.CreateCommandSql("", "SELECT DISTINCT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM VIEW_AGREEMENT WHERE END_DATE >= " & NufarmBussinesRules.SharedClass.ShortGetDate())
                tblDistributor.Clear()
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                Me.SqlDat.Fill(tblDistributor) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                ''Me.FillDataTable(tblDistributor)
                'me.CreateCommandSql("","SELECT DISTR
                Return tblDistributor.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistributor
        End Function
        Public Function CreateViewDistributor_1() As DataView
            Try
                Me.CreateCommandSql("", "SELECT DISTINCT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM VIEW_DISTRIBUTOR")
                Dim tblDistributor As New DataTable("DISTRIBUTOR")
                tblDistributor.Clear()
                Me.FillDataTable(tblDistributor)
                Me.m_ViewDistributor = tblDistributor.DefaultView()
                Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistributor
        End Function
        Public ReadOnly Property GetDataViewForGridEx() As DataView
            Get
                Return Me.m_ViewProject
            End Get
        End Property
        Public Sub DeleteProjBrandPack(ByVal projBrandPackID As String)
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "DELETE FROM PROJ_BRANDPACK WHERE PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, projBrandPackID, 30)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Sub DeleteProject(ByVal PROJ_REF_NO As String, ByVal PROJ_BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF NOT EXISTS(SELECT PROJ_BRANDPACK_ID FROM ORDR_PO_BRANDPACK WHERE PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID) " & vbCrLf & _
                        "BEGIN " & vbCrLf & _
                        "DELETE FROM PROJ_BRANDPACK WHERE PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID ;" & vbCrLf & _
                        "END "
                'Me.CreateCommandSql("Sp_Delete_PROJ_PROJECT", "")
                'Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, PROJ_REF_NO, 15)
                'Me.OpenConnection()
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, PROJ_BRANDPACK_ID, 30)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Query = "IF NOT EXISTS(SELECT PROJ_REF_NO FROM PROJ_BRANDPACK WHERE PROJ_REF_NO = @PROJ_REF_NO ) " & vbCrLf & _
                    "BEGIN " & vbCrLf & _
                    "DELETE FROM PROJ_PROJECT WHERE PROJ_REF_NO = @PROJ_REF_NO ;" & vbCrLf & _
                    "END "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, PROJ_REF_NO, 15)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        'Public Function FetchDataSet() As DataSet
        '    Try

        '        Me.CreateCommandSql("", "SELECT * FROM PROJECT")
        '        Dim tblProject As New DataTable("PROJECT_DISTRIBUTOR")
        '        tblProject.Clear()
        '        Me.FillDataTable(tblProject)
        '        Me.m_ViewProject = tblProject.DefaultView()
        '        'Me.m_dataSet = New DataSet("Ds_Project")
        '        'Me.m_dataSet.Clear()
        '        'Me.m_dataSet.Tables.Add(tblProject)
        '        'Me.CreateCommandSql("SELECT * FROM PROJECT_BRANDPACK_DETAIL", "")
        '        'Dim tblVProjBrandPack As New DataTable("T_ProjBrandPack")
        '        'tblVProjBrandPack.Clear()
        '        'Me.FillDataTable(tblVProjBrandPack)
        '        'Me.m_dataSet.Tables.Add(tblVProjBrandPack)
        '        'Me.GetdatasetRelation("CommandText", "SELECT * FROM PROJECT_DISTRIBUTOR", "SELECT * FROM PROJECT_BRANDPACK_DETAIL" _
        '        ', "T_Project", "PROJECT_BRANPACK_DETAIL", "PROJ_REF_NO", "PROJ_REF_NO", "PBDETAIL", "Update")
        '        'Me.m_dataSet = New DataSet("Ds_Project")
        '        'Me.m_dataSet.Clear()
        '        'Me.m_dataSet = MyBase.baseDataSetRelation
        '        'Me.CreateCommandSql("Sp_Select_PROJ_BRANDPACK", "")
        '        'SUPAYA DATASET TIDAK BERAT DATASET HANYA DIISI DENGAN 3 DATATABLE SAJA
        '        'ini juga cuma KARENA UNTUK REFERENSI SAVING DATA SECARA otomatis saja
        '        'UNTUK TABEL BRANDPACK NGGAK USAH DI DIMASUKAN KEDALAN DATASET KARENA CUMA UNTUK VIEW DATA SAJA
        '        'Me.CreateCommandSql("Sp_SelectAllBrandPack", "")
        '        'Dim tblAllBrandPack As New DataTable("T_AllBrandPack")
        '        'tblAllBrandPack.Clear()
        '        'Me.FillDataTable(tblAllBrandPack)
        '        'Me.m_ViewBrandPack = tblAllBrandPack.DefaultView()
        '        'Me.m_ViewBrandPack.Sort = "BRANDPACK_ID"
        '        'SEKARANG BAGIAN UNTUK MEMANGGIL PROCEDURE PROJ BRANDPACK DETAIL DI CLASS PROJ BRANDPACK DETAIL
        '        PB = New ProjectBrandPack()
        '        Me.m_tblProjBPDetail = PB.GetDataTable()
        '        Me.m_DSBrandPackDetail = New DataSet("DS")
        '        Me.m_DSBrandPackDetail.Clear()
        '        Me.m_DSBrandPackDetail.Tables.Add(Me.m_tblProjBPDetail)
        '        Me.m_ViewProjBrandPack = Me.m_DSBrandPackDetail.Tables(0).DefaultView
        '        Me.m_ViewProjBrandPack.Sort = "PROJ_BRANDPACK_ID"
        '        'Me.m_dataSet.Tables.Add(Me.m_tblProjBPDetail)
        '        'Me.CreateViewDistDropDown()
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        '    Return Me.m_dataSet
        'End Function

        Public Function GetDasetBranpackDetail(ByVal PROJ_BRANDPACK_ID As String) As DataSet
            Try
                PB = New ProjectBrandPack(PROJ_BRANDPACK_ID)
                Me.m_tblProjBPDetail = PB.GetDataTable(PROJ_BRANDPACK_ID, False)
                Me.m_DSBrandPackDetail = New DataSet("DS")
                Me.m_DSBrandPackDetail.Clear()
                Me.m_DSBrandPackDetail.Tables.Add(Me.m_tblProjBPDetail)
                Me.m_ViewProjBrandPack = Me.m_DSBrandPackDetail.Tables(0).DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_DSBrandPackDetail
        End Function
        'Public Function GetDataSetBrandPackDetail_1(ByVal PROJ_REF_NO As String) As DataSet
        '    Try
        '        Me.FillDataTable("", "SELECT * FROM PROJ_BRANDPACK WHERE PROJ_REF_NO = '" + PROJ_REF_NO + "'", "BRANDPACK_DETAIL")
        '        Me.m_DSBrandPackDetail = MyBase.baseDataSet
        '        Me.m_DSBrandPackDetail.Clear()
        '        Me.m_ViewProjBrandPack = Me.m_DSBrandPackDetail.Tables(0).DefaultView()
        '        Me.m_ViewProjBrandPack.Sort = "PROJ_BRANDPACK_ID"
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        '    Return Me.m_DSBrandPackDetail
        'End Function
#End Region

#Region " Sub "
        Public Sub SaveProject(ByVal dsBrandPack As DataSet, ByVal mustCloseConnection As Boolean)
            Try
                'Me.GetConnection()
                'Me.InsertData("Sp_Insert_Project", "")
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "IF NOT EXISTS(SELECT PROJ_REF_NO FROM PROJ_PROJECT WHERE PROJ_REF_NO = @PROJ_REF_NO) " & vbCrLf & _
                " BEGIN " & vbCrLf & _
                " INSERT INTO PROJ_PROJECT(PROJ_REF_NO,PROJECT_NAME,START_DATE,END_DATE,DISTRIBUTOR_ID,ADDRESS,CREATE_BY,CREATE_DATE)" & vbCrLf & _
                " VALUES(@PROJ_REF_NO,@PROJECT_NAME,@START_DATE,@END_DATE,@DISTRIBUTOR_ID,@ADDRESS,@CREATE_BY,GETDATE()) ;" & vbCrLf & _
                " END" & vbCrLf & _
                " ELSE " & vbCrLf & _
                " BEGIN " & vbCrLf & _
                " UPDATE PROJ_PROJECT SET PROJECT_NAME = @PROJECT_NAME,START_DATE = @START_DATE,END_DATE = @END_DATE,ADDRESS = @ADDRESS WHERE PROJ_REF_NO = @PROJ_REF_NO ;" & vbCrLf & _
                " END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, Me.Proj_Ref_No, 15) ' VARCHAR(15),
                Me.AddParameter("@PROJECT_NAME", SqlDbType.VarChar, Me.Proj_Ref_Name, 50) ' VARCHAR(50),
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, Me.StartDate) ' DATETIME,
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Me.EndDate)
                Me.AddParameter("@ADDRESS", SqlDbType.VarChar, Me.Address, 100) ' VARCHAR(100),
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.Distributor_ID, 10)
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName) ' VARCHAR(30)
                Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Me.OpenConnection() : BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                'Dim commandInsert As SqlCommand = Me.SqlConn.CreateCommand()
                'With commandInsert
                '    Query = "IF NOT EXISTS(SELECT PROJ_BRANDPACK_ID FROM PROJ_BRANDPACK WHERE PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID) " & vbCrLf & _
                '            "BEGIN " & vbCrLf & _
                '            "   IF (@PRICE > 0) " & vbCrLf & _
                '            "       BEGIN " & vbCrLf & _
                '            "           INSERT INTO PROJ_BRANDPACK(PROJ_BRANDPACK_ID,PROJ_REF_NO,BRANDPACK_ID,PRICE,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                '            "           VALUES(@PROJ_BRANDPACK_ID,@PROJ_REF_NO,@BRANDPACK_ID,@PRICE,@CREATE_BY,@CREATE_DATE) ;" & vbCrLf & _
                '            "        END " & vbCrLf & _
                '            "END "
                '    .CommandText = Query
                '    .CommandType = CommandType.Text
                '    .Parameters.Add("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, 30, "PROJ_BRANDPACK_ID")
                '    .Parameters.Add("@PROJ_REF_NO", SqlDbType.VarChar, 15, "PROJ_REF_NO")
                '    .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                '    .Parameters.Add("@PRICE", SqlDbType.Decimal).SourceColumn = "PRICE"
                '    .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50, "CREATE_BY").Value = NufarmBussinesRules.User.UserLogin.UserName
                '    .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime).SourceColumn = "CREATE_DATE"
                '    .Parameters()("@CREATE_DATE").Value = NufarmBussinesRules.SharedClass.ServerDate

                '    .Transaction = Me.SqlTrans
                'End With
                'Me.SqlDat.InsertCommand = commandInsert
                Dim CommandUpdate As SqlCommand = Me.SqlConn.CreateCommand()
                With CommandUpdate
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    "IF EXISTS(SELECT PROJ_BRANDPACK_ID FROM PROJ_BRANDPACK WHERE PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID) " & vbCrLf & _
                    "   BEGIN " & vbCrLf & _
                    "        UPDATE PROJ_BRANDPACK SET PRICE = @PRICE,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE WHERE PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID ;" & vbCrLf & _
                    "   END" & vbCrLf & _
                    "ELSE " & vbCrLf & _
                    "   BEGIN " & vbCrLf & _
                    "     INSERT INTO PROJ_BRANDPACK(PROJ_BRANDPACK_ID,PROJ_REF_NO,BRANDPACK_ID,PRICE,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                    "     VALUES(@PROJ_BRANDPACK_ID,@PROJ_REF_NO,@BRANDPACK_ID,@PRICE,@CREATE_BY,@CREATE_DATE) ;" & vbCrLf & _
                    "   END "
                    With CommandUpdate
                        .Parameters.Add("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, 30, "PROJ_BRANDPACK_ID")
                        '.Parameters()("@O_PROJ_BRANDPACK_ID").SourceVersion = DataRowVersion.Original
                        '.Parameters.Add("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, 30, "PROJ_BRANDPACK_ID")
                        .Parameters.Add("@PRICE", SqlDbType.Decimal).SourceColumn = "PRICE"
                        .Parameters.Add("@PROJ_REF_NO", SqlDbType.VarChar, 15, "PROJ_REF_NO")
                        .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50, "CREATE_BY")
                        .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_DATE")
                        .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50, "MODIFY_BY")
                        .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime, 0, "MODIFY_DATE")
                        .CommandText = Query
                        .CommandType = CommandType.Text
                        .Transaction = Me.SqlTrans
                    End With
                End With
                Me.SqlDat.UpdateCommand = CommandUpdate

                'Dim commandDelete As SqlCommand = Me.SqlConn.CreateCommand()
                'With commandDelete
                '    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                '    "IF EXISTS(SELECT PROJ_BRANDPACK_ID FROM ORDR_PO_BRANDPACK WHERE PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID ) " & vbCrLf & _
                '    "BEGIN RAISERROR('Can not delete data,data has been used in PO',16,1) ; RETURN ; END " & vbCrLf & _
                '    "IF @PRICE > 0"
                '    "DELETE FROM PROJ_BRANDPACK WHERE PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID ;"
                '    .CommandText = Query
                '    .CommandType = CommandType.Text
                '    .Transaction = Me.SqlTrans
                'End With
                'Me.SqlDat.DeleteCommand = commandDelete
                Dim rows() As DataRow = dsBrandPack.Tables(0).Select("HAS_CHANGED = " & True)
                If rows.Length > 0 Then
                    Me.SqlDat.Update(rows)
                End If
                'rows = dsBrandPack.Tables(0).Select("HAS_CHANGED = " & True, "", DataViewRowState.ModifiedOriginal)
                'If rows.Length > 0 Then
                '    Me.SqlDat.Update(rows)
                'End If
                'rows = dsBrandPack.Tables(0).Select("HAS_CHANGED = " & True, "", DataViewRowState.Deleted)
                'If rows.Length > 0 Then
                '    Me.SqlDat.Update(rows)
                'End If
                Me.ClearCommandParameters() : Me.CommiteTransaction()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try

        End Sub
        Public Sub UpdateProject(Optional ByVal dsPBDetail As DataSet = Nothing)
            Dim sqlCom1 As SqlCommand = Nothing
            Dim SqlDat1 As SqlDataAdapter = Nothing
            Dim CB As SqlCommandBuilder = Nothing
            Try
                Me.GetConnection()
                Me.InsertData("Sp_Update_Project", "")
                Me.AddParameter("@PROJ_REF_NO", SqlDbType.VarChar, Me.Proj_Ref_No, 15) ' VARCHAR(15),
                Me.AddParameter("@PROJECT_NAME", SqlDbType.VarChar, Me.Proj_Ref_Name, 50) ' VARCHAR(50),
                Me.AddParameter("@PROJECT_REF_DATE", SqlDbType.DateTime, Me.StartDate) ' DATETIME,
                Me.AddParameter("@ADDRESS", SqlDbType.VarChar, Me.Address, 100) ' VARCHAR(100),
                Me.AddParameter("@CONTACT", SqlDbType.VarChar, Me.Conntact, 15) ' VARCHAR(15),
                Me.AddParameter("@PHONE", SqlDbType.VarChar, Me.Phone, 15) ' VARCHAR(15),
                Me.AddParameter("@FAX", SqlDbType.VarChar, Me.Fax, 15) ' VARCHAR(15),
                Me.AddParameter("@HP", SqlDbType.VarChar, Me.HP, 15) ' VARCHAR(15),
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.Distributor_ID, 10)
                Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName) ' VARCHAR(30)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                If (Not IsNothing(dsPBDetail)) And (Me.DSPBDetailHasChanges = True) Then
                    sqlCom1 = New SqlCommand("SELECT * FROM PROJ_BRANDPACK", Me.SqlConn)
                    sqlCom1.CommandType = CommandType.Text
                    sqlCom1.Transaction = Me.SqlTrans
                    SqlDat1 = New SqlDataAdapter(sqlCom1)
                    CB = New SqlCommandBuilder(SqlDat1)
                    SqlDat1.Update(dsPBDetail.Tables(0))
                    'Me.CreateCommandSql("", "SELECT * FROM PROJ_BRANDPACK")
                    'Me.PB = New ProjectBrandPack()
                    'Dim sqldat1 As New SqlClient.SqlDataAdapter(Me.SqlCom)
                    'PB.SqlTrans = Me.SqlTrans
                    'PB.DataAdapter.Update(dsPBDetail.Tables(0).GetChanges())
                End If
                Me.CommiteTransaction()
                Me.CloseConnection()
                Me.DSPBDetailHasChanges = False
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            Finally
                Me.ClearCommandParameters()
                If Not IsNothing(sqlCom1) Then
                    sqlCom1.Dispose()
                    sqlCom1 = Nothing
                End If
                If Not IsNothing(SqlDat1) Then
                    SqlDat1.Dispose()
                    SqlDat1 = Nothing
                End If
                If Not IsNothing(CB) Then
                    CB.Dispose()
                    CB = Nothing
                End If
            End Try
        End Sub

#End Region

#Region " Property "
        Public ReadOnly Property ViewDistDropDown() As DataView
            Get
                Return Me.m_ViewDistDropDown
            End Get
        End Property
        Public ReadOnly Property GetDataSet() As DataSet ' for tracking dataset changes
            Get
                Return Me.m_DSBrandPackDetail
            End Get
        End Property
        Public ReadOnly Property ViewBrandPack() As DataView
            Get
                Return Me.m_ViewBrandPack
            End Get
        End Property
        Public ReadOnly Property ViewDistributor() As DataView
            Get
                Return Me.m_ViewDistributor
            End Get
        End Property
        Public ReadOnly Property GetViewBrandpackDetail() As DataView
            Get
                Return Me.m_ViewProjBrandPack
            End Get
        End Property
#End Region

#Region " Constructor / Destructor "
        Public Sub New()
            Me.m_dataSet = Nothing
            Me.m_DSBrandPackDetail = Nothing
            Me.colDistributor = Nothing
        End Sub
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewProject) Then
                Me.m_ViewProject.Dispose()
                Me.m_ViewProject = Nothing
            End If
            If Not IsNothing(Me.m_dataSet) Then
                Me.m_dataSet.Dispose()
                Me.m_dataSet = Nothing
            End If
            If Not IsNothing(Me.m_DSBrandPackDetail) Then
                Me.m_DSBrandPackDetail.Dispose()
                Me.m_DSBrandPackDetail = Nothing
            End If
            If Not IsNothing(Me.m_tblProjBPDetail) Then
                Me.m_tblProjBPDetail.Dispose()
                Me.m_tblProjBPDetail = Nothing
            End If
            If Not IsNothing(Me.m_ViewBrandPack) Then
                Me.m_ViewBrandPack.Dispose()
                Me.m_ViewBrandPack = Nothing
            End If
            If Not IsNothing(Me.m_ViewDistributor) Then
                Me.m_ViewDistributor.Dispose()
                Me.m_ViewDistributor = Nothing
            End If
            If Not IsNothing(Me.PB) Then
                Me.PB.dispose()
                Me.PB = Nothing
            End If
            If Not IsNothing(Me.m_ViewDistDropDown) Then
                Me.m_ViewDistDropDown.Dispose()
                Me.m_ViewDistDropDown = Nothing
            End If
        End Sub
#End Region

    End Class
End Namespace

