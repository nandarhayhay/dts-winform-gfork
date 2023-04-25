Imports System.Data
Imports System.Data.SqlClient
Imports NufarmBussinesRules.SharedClass
Namespace Brandpack
    Public Class Pack
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Public Sub New()
            MyBase.New()
        End Sub
        Private Query As String = ""
        Private m_dataView As DataView
        Private m_dataSet As DataSet
        Private m_AllDataViewPack As DataView
        Public Function getUnitAccPacc() As List(Of String)
            Try
                Dim DBConnect As String = "NI87"
                'If DBInvoiceTo = CurrentInvToUse.NI109 Then

                'End If
                If DBInvoiceTo = CurrentInvToUse.NI109 Then
                    DBConnect = "NI109"
                End If
                Dim ListOfUnit As New List(Of String)
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT DISTINCT UPPER(STOCKUNIT) FROM " & DBConnect & ".dbo.ICITEM;"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Me.ExecuteReader()
                While Me.SqlRe.Read
                    ListOfUnit.Add(SqlRe.GetString(0))
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                Me.CloseConnection()
                Return ListOfUnit
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public ReadOnly Property GetDataSet() As DataSet
            Get
                Return m_dataSet
            End Get
        End Property
        Public Function GetDataViewRowState(ByVal RowState As String) As DataView
            Select Case RowState
                Case "ModifiedAdded"
                    Me.m_AllDataViewPack.RowStateFilter = DataViewRowState.Added
                Case "ModifiedOriginal"
                    Me.m_AllDataViewPack.RowStateFilter = DataViewRowState.ModifiedOriginal
                Case "Deleted"
                    Me.m_AllDataViewPack.RowStateFilter = DataViewRowState.Deleted
                Case "Current"
                    Me.m_AllDataViewPack.RowStateFilter = DataViewRowState.CurrentRows
                Case "Unchaigned"
                    Me.m_AllDataViewPack.RowStateFilter = DataViewRowState.Unchanged
                Case "OriginalRows"
                    Me.m_AllDataViewPack.RowStateFilter = DataViewRowState.OriginalRows
            End Select
            Return Me.m_AllDataViewPack
        End Function
        Private Sub getPack(ByVal PackName As String, ByRef Qty As Integer, ByRef unit As String, ByRef DevideFactor As Integer)
            Dim l As Integer = Len(PackName)
            Dim w As Integer = 1
            Dim s As String = ""
            Dim a As String = "" 'packname
            Dim Quantity As String = "", Unit1 As String = ""

            Do Until w = l + 1
                s = Mid(PackName, w, 1)
                If s = "-" Then
                    Exit Do
                End If
                a = a & s
                w += 1
            Loop
            ''pack sudah dapat
            'sekarang ambil qty nya
            l = Len(a) : w = 1 : s = ""
            Do Until w = l + 1
                s = Mid(a, w, 1)
                If Not IsNumeric(s) Then
                    Unit1 &= s
                Else
                    Quantity = Quantity & s
                End If
                w += 1
            Loop
            Quantity = Quantity.Trim()
            unit = Unit1.Trim() : Qty = Convert.ToInt32(Quantity)
            Select Case Unit1.Trim()
                Case "LT", "L"
                    DevideFactor = 1
                    unit = "LITRE"
                Case "K", "KG"
                    unit = "KILOGRAM"
                    DevideFactor = 1
                Case "ML", "M"
                    unit = "MILILITRE"
                    DevideFactor = 1000
                Case "GR"
                    unit = "GRAM"
                    DevideFactor = 1000
                Case "TABLET"
                    unit = "TABLET"
                    DevideFactor = 1
            End Select
        End Sub
        Public Function CreateDataViewAllPack() As DataView
            Try
                ''ambil pack yang ada di accpac,bila belum ada inputkan
                'Dim DBInvoiceTo As NufarmBussinesRules.SharedClass.CurrentInvToUse
                'If DBInvoiceTo = CurrentInvToUse.NI109 Then

                'End If
                Dim DBConnect As String = "NI87"
                'If DBInvoiceTo = CurrentInvToUse.NI109 Then
                DBConnect = "NI109"
                'End If
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT PACK_ID,BRANDPACK_NAME FROM (" & vbCrLf & _
                        "                      SELECT DISTINCT RTRIM(SEGMENT4) + '' + RTRIM(SEGMENT3) AS PACK_ID,RTRIM([DESC]) AS BRANDPACK_NAME FROM " & DBConnect & ".dbo.ICITEM " & vbCrLf & _
                        " WHERE INACTIVE = 0 AND (RTRIM(ITEMBRKID) = 'FG' OR RTRIM(ITEMBRKID) = 'FGST' OR RTRIM(ITEMBRKID) = 'FGTOLL') AND [DESC] LIKE '%@%')IC " & vbCrLf & _
                        " WHERE NOT EXISTS(SELECT PACK_ID FROM Nufarm.dbo.BRND_PACK WHERE PACK_ID = IC.PACK_ID); "
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblPack As New DataTable("T_Pack") : tblPack.Clear()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection() : Me.SqlDat.Fill(tblPack)
                If tblPack.Rows.Count > 0 Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "IF NOT EXISTS(SELECT PACK_ID FROM BRND_PACK WHERE PACK_ID = @PACK_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "INSERT INTO Nufarm.dbo.BRND_PACK(PACK_ID,PACK_NAME,QUANTITY,DEVIDE_FACTOR,UNIT,IsActive,IsObsolete,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                            " VALUES(@PACK_ID,@PACK_NAME,@QUANTITY,@DEVIDE_FACTOR,@UNIT,1,0,@CREATE_BY,@CREATE_DATE); " & vbCrLf & _
                            " END"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                    For i As Integer = 0 To tblPack.Rows.Count - 1
                        If tblPack.Rows(i)("BRANDPACK_NAME").ToString.Contains("BULK") Then
                        Else
                            Dim Quantity As Integer = 0, DevideFactor As Decimal = 0, Unit As String = "", OriginalPackName As String = ""
                            Me.AddParameter("@PACK_ID", SqlDbType.VarChar, tblPack.Rows(i)("PACK_ID"))
                            Dim PackName As String = tblPack.Rows(i)("BRANDPACK_NAME").ToString()
                            If PackName.IndexOf("@") >= 0 Then
                                PackName = PackName.Replace(PackName.Substring(0, PackName.IndexOf("@") + 1), "")
                                OriginalPackName = PackName
                                PackName = PackName.Trim() : Dim Query1 As String = "" : Dim StockUnit As Object = Nothing
                                If PackName = "" Then
                                    Query1 = "SET NOCOUNT ON;" & vbCrLf & _
                                            "SELECT TOP 1 RTRIM(STOCKUNIT)AS StockUnit,RIGHT(RTRIM(SEGMENT4),1)AS FLAG FROM " & DBConnect & ".dbo.ICITEM WHERE RTRIM(SEGMENT4) + '' + RTRIM(SEGMENT3) = @PACK_ID AND INACTIVE = 0) OPTION(KEEP PLAN);"
                                    Me.ResetCommandText(CommandType.Text, Query1)
                                    Me.SqlRe = Me.SqlCom.ExecuteReader()
                                    While Me.SqlRe.Read()
                                        StockUnit = SqlRe("StockUnit")
                                        If Not IsNothing(StockUnit) Then
                                            If StockUnit.ToString().ToUpper() = "BAGS" Then
                                                Quantity = 1 : DevideFactor = 1 : OriginalPackName = "BAGS"
                                            End If
                                        End If
                                    End While : SqlRe.Close()
                                Else
                                    Me.getPack(PackName, Quantity, Unit, DevideFactor)
                                End If
                            Else
                                OriginalPackName = PackName
                            End If
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@PACK_NAME", SqlDbType.VarChar, OriginalPackName.Trim(), 100)
                            Me.AddParameter("@QUANTITY", SqlDbType.Int, Quantity)
                            Me.AddParameter("@DEVIDE_FACTOR", SqlDbType.Int, DevideFactor)
                            Me.AddParameter("@UNIT", SqlDbType.VarChar, Unit.Trim())
                            Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, "SYSTEM", 100)
                            Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        End If
                    Next
                End If
                'sekarang ambil pack yang gack ada di accpack dengan status active
                'If IsNothing(Me.SqlTrans) Then
                '    Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                'End If
                'Query = "SET NOCOUNT ON;" & vbCrLf & _
                '          " UPDATE BRND_PACK SET IsActive = 0 WHERE PACK_ID " & vbCrLf & _
                '          " = ANY(SELECT PACK_ID FROM BRND_BRANDPACK BB " & vbCrLf & _
                '          "        WHERE EXISTS( " & vbCrLf & _
                '          "                          SELECT RTRIM(SEGMENT4) + '' + RTRIM(SEGMENT3) FROM NI87.dbo.ICITEM " & vbCrLf & _
                '          "                          WHERE RTRIM(ITEMBRKID) = 'FG' AND INACTIVE = 1 " & vbCrLf & _
                '          "                          AND  RTRIM(SEGMENT4) + '' + RTRIM(SEGMENT3) = BB.PACK_ID " & vbCrLf & _
                '          "                         ) " & vbCrLf & _
                '          " AND PACK_ID != '022K-D' " & vbCrLf & _
                '          "       ); "
                'Query &= vbCrLf & _
                '" UPDATE BRND_PACK SET IsObsolete = 1 WHERE PACK_ID " & vbCrLf & _
                '"  = ANY(SELECT PACK_ID FROM Nufarm.dbo.BRND_BRANDPACK BB " & vbCrLf & _
                '"        WHERE NOT EXISTS( " & vbCrLf & _
                '"                          SELECT RTRIM(SEGMENT4) + '' + RTRIM(SEGMENT3) FROM NI87.dbo.ICITEM " & vbCrLf & _
                '"                          WHERE RTRIM(ITEMBRKID) = 'FG' AND INACTIVE = 0 " & vbCrLf & _
                '"                          AND  RTRIM(SEGMENT4) + '' + RTRIM(SEGMENT3) = BB.PACK_ID " & vbCrLf & _
                '"                         ) " & vbCrLf & _
                '" AND PACK_ID != '022K-D'); "
                '"              AND EXISTS(SELECT BRANDPACK_ID FROM Nufarm.dbo.AGREE_BRANDPACK_INCLUDE WHERE BRANDPACK_ID = BB.BRANDPACK_ID) " & vbCrLf & _

                'Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                'Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters() : Me.CommiteTransaction()
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT * FROM BRND_PACK;"
                Me.CommiteTransaction() : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.CreateCommandSql("Sp_SelectAll_PACK", "")
                tblPack = New DataTable("T_AllPack")
                tblPack.Clear()
                Me.FillDataTable(tblPack) : Me.m_dataSet = New DataSet("DSPack")
                Me.m_dataSet.Tables.Add(tblPack)
                Me.m_AllDataViewPack = Me.m_dataSet.Tables(0).DefaultView()
                Me.m_AllDataViewPack.RowStateFilter = DataViewRowState.CurrentRows
                'Me.m_AllDataViewPack.RowStateFilter = DataViewRowState.OriginalRows
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_AllDataViewPack
        End Function
        Public Sub SaveData(ByVal ds As DataSet)
            Try
                'Me.CreateCommandSql("", "SELECT * FROM BRND_PACK")
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlDat = New SqlDataAdapter()
                Dim Rows() As DataRow = ds.Tables(0).Select("", "", DataViewRowState.Added)
                If Rows.Length > 0 Then
                    Dim CommandInsert As SqlCommand = Me.SqlConn.CreateCommand()
                    With CommandInsert
                        .CommandType = CommandType.Text
                        .CommandText = "SET NOCOUNT ON;" & vbCrLf & _
                        "INSERT INTO BRND_PACK(PACK_ID,PACK_NAME,UNIT,QUANTITY,DEVIDE_FACTOR,IsActive,IsObsolete,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                        " VALUES(@PACK_ID,@PACK_NAME,@UNIT,@QUANTITY,@DEVIDE_FACTOR,1,0,@CREATE_BY,@CREATE_DATE);"
                        .Transaction = Me.SqlTrans
                        .Parameters.Add("@PACK_ID", SqlDbType.VarChar, 7).SourceColumn = "PACK_ID"
                        .Parameters.Add("@UNIT", SqlDbType.VarChar, 20).SourceColumn = "UNIT"
                        .Parameters.Add("@PACK_NAME", SqlDbType.VarChar, 100).SourceColumn = "PACK_NAME"
                        .Parameters.Add("@QUANTITY", SqlDbType.Int, 0).SourceColumn = "QUANTITY"
                        .Parameters.Add("@DEVIDE_FACTOR", SqlDbType.Int, 0).SourceColumn = "DEVIDE_FACTOR"
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).SourceColumn = "CREATE_BY"
                        .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime).SourceColumn = "CREATE_DATE"
                    End With
                    SqlDat.InsertCommand = CommandInsert
                    SqlDat.Update(Rows)
                End If
                Rows = ds.Tables(0).Select("", "", DataViewRowState.ModifiedCurrent)
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                       "IF EXISTS(SELECT PACK_ID FROM BRND_BRANDPACK WHERE PACK_ID = @PACK_ID) " & vbCrLf & _
                       " BEGIN " & vbCrLf & _
                       " UPDATE BRND_BRANDPACK SET IsActive = @IsActive,IsObsolete = @IsObsolete," & vbCrLf & _
                       "DEVIDED_QUANTITY = CAST((@QUANTITY)AS DECIMAL(10,3)) / CAST((@DEVIDE_FACTOR) AS DECIMAL(10,3)),UNIT = @UNIT,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " & vbCrLf & _
                       " WHERE PACK_ID = @PACK_ID;" & vbCrLf & _
                       " END " & vbCrLf & _
                       " UPDATE BRND_PACK SET PACK_NAME = @PACK_NAME,UNIT = @UNIT,DEVIDE_FACTOR = @DEVIDE_FACTOR," & vbCrLf & _
                       "QUANTITY = @QUANTITY,IsObsolete = @IsObsolete,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " & vbCrLf & _
                       " WHERE PACK_ID = @PACK_ID;"
                Dim CommandUpdate As SqlCommand = Me.SqlConn.CreateCommand()
                With CommandUpdate
                    .CommandType = CommandType.Text : .CommandText = Query
                    .Transaction = Me.SqlTrans
                    .Parameters.Add("@PACK_ID", SqlDbType.VarChar, 7).SourceColumn = "PACK_ID"
                    .Parameters("@PACK_ID").SourceVersion = DataRowVersion.Original
                    .Parameters.Add("@PACK_NAME", SqlDbType.VarChar, 100).SourceColumn = "PACK_NAME"
                    .Parameters.Add("@QUANTITY", SqlDbType.Int, 0).SourceColumn = "QUANTITY"
                    .Parameters.Add("@UNIT", SqlDbType.VarChar, 20).SourceColumn = "UNIT"
                    .Parameters.Add("@DEVIDE_FACTOR", SqlDbType.Int, 0).SourceColumn = "DEVIDE_FACTOR"
                    .Parameters.Add("@IsObsolete", SqlDbType.Bit).SourceColumn = "IsObsolete"
                    .Parameters.Add("@IsActive", SqlDbType.Bit).SourceColumn = "IsActive"
                    .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50).SourceColumn = "MODIFY_BY"
                    .Parameters.Add("@MODIFY_DATE", SqlDbType.SmallDateTime).SourceColumn = "MODIFY_DATE"
                    SqlDat.UpdateCommand = CommandUpdate
                End With
                If Rows.Length > 0 Then
                    SqlDat.Update(Rows)
                End If
                Rows = ds.Tables(0).Select("", "", DataViewRowState.ModifiedOriginal)
                If Rows.Length > 0 Then
                    SqlDat.Update(Rows)
                End If
                Rows = ds.Tables(0).Select("", "", DataViewRowState.Deleted)
                If Rows.Length > 0 Then
                    Dim CommandDelete As SqlCommand = Me.SqlConn.CreateCommand()
                    With CommandDelete
                        .Transaction = Me.SqlTrans
                        .CommandType = CommandType.Text
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "DELETE FROM BRND_PACK WHERE PACK_ID = @PACK_ID " & vbCrLf & _
                                " AND NOT EXISTS(SELECT PACK_ID FROM BRND_BRANDPACK WHERE PACK_ID = @PACK_ID);"
                        .CommandText = Query
                        .Parameters.Add("@PACK_ID", SqlDbType.VarChar, 7).SourceColumn = "PACK_ID"
                        .Parameters("@PACK_ID").SourceVersion = DataRowVersion.Original
                        SqlDat.DeleteCommand = CommandDelete
                        SqlDat.Update(Rows)
                    End With
                End If
                'Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                'Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                'Me.OpenConnection() : Me.BeginTransaction()
                'Me.SqlCom.Transaction = Me.SqlTrans
                'Me.SqlDat.Update(ds.Tables(0))
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Property ViewPack() As DataView
            Get
                Return Me.m_dataView
            End Get
            Set(ByVal value As DataView)
                Me.m_dataView = value
            End Set
        End Property
        Public ReadOnly Property GetAllDataViewPack() As DataView
            Get
                Return Me.m_AllDataViewPack
            End Get
        End Property

        Public Function GetDataView() As DataView
            Try
                MyBase.FilDataSet("Usp_Select_Pack", "", "PackDataSet")
                Me.m_dataSet = MyBase.baseDataSet
                'Me.m_dataView = MyBase.CreateDataView(m_dataSet.Tables(0))
                Me.m_dataView = m_dataSet.Tables(0).DefaultView()
                'Me.m_dataView.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_dataView.Sort = "PACK_ID"
            Catch ex As Exception
                Throw ex
            End Try
            Return m_dataView
        End Function
        Public Function HasReferencedData(ByVal PACK_ID) As Integer
            Dim RetVal As Integer = 0
            Try
                Me.CreateCommandSql("Sp_Check_PACK_ID", "")
                Me.AddParameter("@PACK_ID", SqlDbType.VarChar, PACK_ID, 7)
                'Me.AddParameter("@RETVAL", SqlDbType.VarChar, ParameterDirection.ReturnValue)
                'Me.SqlCom.Parameters.Add("@RETVAL", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                RetVal = CInt(Me.GetReturnValueByExecuteScalar("@RETVAL_VALUE"))
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
            End Try
            Return RetVal
        End Function
        'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        '    If (disposing) Then
        '        If Not IsNothing(Me.m_dataSet) Then
        '            Me.m_dataSet.Dispose()
        '            Me.m_dataSet = Nothing
        '        End If
        '        If Not IsNothing(Me.m_dataView) Then
        '            Me.m_dataView.Dispose()
        '            Me.m_dataView = Nothing
        '        End If
        '    Else
        '        MyBase.Dispose()
        '    End If
        'End Sub
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_dataView) Then
                Me.m_dataView.Dispose()
                Me.m_dataView = Nothing
            End If
            If Not IsNothing(Me.m_dataSet) Then
                Me.m_dataSet.Dispose()
                Me.m_dataSet = Nothing
            End If

        End Sub
    End Class
End Namespace

