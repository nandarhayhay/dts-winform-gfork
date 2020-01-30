Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Namespace DataAccesLayer
    Public Class ADODotNet
        Inherits DataAccesLayer.BaseLayer
#Region " Deklarasi "
        Protected ParentSqlDat As SqlDataAdapter
        Protected ChildSqlDat As SqlDataAdapter
        Protected ParentSqlComb As SqlCommandBuilder
        Protected ChildSqlComb As SqlCommandBuilder
        Protected baseChildTable As DataTable
        Protected baseParentTable As DataTable
        Protected baseDataSet As DataSet
        Protected baseDataSetRelation As DataSet
        Protected baseDataView As DataView
        Protected baseDataRowView As DataRowView
        Protected ParentCommandtext As String
        Protected ChildCommandText As String
        Protected baseDataTable As DataTable
        Private DR As DataRelation
        Public Event savingChanges As onSaving
        Protected baseChekTable As DataTable
        Protected SqlDatChek As SqlDataAdapter
        Protected MessageDBConcurency As String = "DBConcurency changes 0 record" & vbCrLf & "Perhaps some user has changed the same data."
        Protected MessageCantDeleteData As String = "Data cannot be deleted !" & vbCrLf & "Because has child referenced data with it."
        Protected ComputerName As String = System.Environment.UserName.Trim()
#End Region

#Region " Constructor "
        Public Sub New()
            MyBase.New()
            Me.ParentSqlDat = Nothing
            Me.ChildSqlDat = Nothing
            Me.ParentSqlComb = Nothing
            Me.ChildSqlComb = Nothing
            Me.baseChildTable = Nothing
            Me.baseParentTable = Nothing
            Me.baseDataSetRelation = Nothing
            Me.baseDataTable = Nothing
            Me.baseDataView = Nothing
            Me.ParentCommandtext = ""
            Me.ChildCommandText = ""
            Me.DR = Nothing
            Me.baseChekTable = Nothing
            Me.SqlDatChek = Nothing
        End Sub
#End Region

#Region " Property "

#End Region

#Region " sub "

        Protected Sub SearcData(ByVal NameOfStoredProcedure As String, ByVal Sql As String)
            Try
                Me.GetConnection()
                If NameOfStoredProcedure <> "" Then
                    Me.SqlCom = New SqlCommand(NameOfStoredProcedure, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                Else
                    Me.SqlCom = New SqlCommand(Sql, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.Text
                End If
                Me.baseChekTable = New DataTable()
                Me.baseChekTable.Clear()
                Me.OpenConnection()
                Me.SqlDatChek = New SqlDataAdapter(Me.SqlCom)
                Me.SqlDatChek.Fill(Me.baseChekTable)
                Me.CloseConnection()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Protected Sub AddParameter(ByVal ParameterName As String, ByVal sqlType As SqlDbType, ByVal pDir As ParameterDirection)
            If Not IsNothing(Me.SqlCom) Then
                If Me.SqlCom.Parameters.IndexOf(ParameterName) = -1 Then
                Else
                    Me.SqlCom.Parameters.RemoveAt(ParameterName)
                End If
                Me.sqlPar = New SqlParameter()
                Me.sqlPar.ParameterName = ParameterName
                Me.sqlPar.SqlDbType = sqlType
                Me.sqlPar.Direction = pDir
                Me.SqlCom.Parameters.Add(Me.sqlPar)
            End If
        End Sub
        'Protected Sub AddParameter(ByVal ParameterName As String, ByVal sqlType As SqlDbType, ByVal pDir As ParameterDirection, ByVal ParameterValue As Object)
        '    If Not IsNothing(Me.SqlCom) Then
        '        If Me.SqlCom.Parameters.IndexOf(ParameterName) = -1 Then
        '        Else
        '            Me.SqlCom.Parameters.RemoveAt(ParameterName)
        '        End If
        '        Me.sqlPar = New SqlParameter()
        '        Me.sqlPar.ParameterName = ParameterName
        '        Me.sqlPar.SqlDbType = sqlType
        '        Me.sqlPar.Direction = pDir
        '        Me.sqlPar.Value = ParameterValue
        '        Me.SqlCom.Parameters.Add(Me.sqlPar)
        '    End If
        'End Sub
        Protected Sub AddParameter(ByVal ParameterName As String, ByVal sqlType As SqlDbType, ByVal ParameterValue As Object, ByVal Size As Integer)
            If Not IsNothing(Me.SqlCom) Then
                If Me.SqlCom.Parameters.IndexOf(ParameterName) = -1 Then
                Else
                    Me.SqlCom.Parameters.RemoveAt(ParameterName)
                End If
                Me.sqlPar = New SqlParameter(ParameterName, sqlType, Size)
                Me.sqlPar.Direction = ParameterDirection.Input
                Me.sqlPar.Value = ParameterValue
                Me.SqlCom.Parameters.Add(Me.sqlPar)
            End If
        End Sub

        Protected Sub AddParameter(ByVal ParameterName As String, ByVal sqlType As SqlDbType, ByVal ParameterValue As Object)
            If Not IsNothing(Me.SqlCom) Then
                If Me.SqlCom.Parameters.IndexOf(ParameterName) = -1 Then
                Else
                    Me.SqlCom.Parameters.RemoveAt(ParameterName)
                End If
                Me.sqlPar = New SqlParameter(ParameterName, ParameterValue)
                Me.sqlPar.SqlDbType = sqlType
                Me.sqlPar.Direction = ParameterDirection.Input
                Me.sqlPar.Value = ParameterValue
                Me.SqlCom.Parameters.Add(Me.sqlPar)
            End If
        End Sub
        ''hanya untuk Nufarm DataBase
        Protected Sub CreateCommandSql(ByVal NameOfStoredProcedure As String, ByVal sql As String)
            Try
                Me.GetConnection()
                If NameOfStoredProcedure <> "" Then
                    Me.SqlCom = New SqlCommand(NameOfStoredProcedure, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                Else
                    Me.SqlCom = New SqlCommand(sql, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.Text
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        
        'data table for saving automatically to server
        Protected Sub FillDataTable(ByVal NameOfStoredProcedure As String, ByVal sql As String, ByVal DataTableName As String)
            Try
                Me.GetConnection()
                If NameOfStoredProcedure <> "" Then
                    Me.SqlCom = New SqlCommand(NameOfStoredProcedure, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                Else
                    Me.SqlCom = New SqlCommand(sql, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.Text
                End If
                Me.baseDataSet = New DataSet
                Me.baseDataSet.Clear()
                Me.OpenConnection()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(Me.baseDataSet, DataTableName)
                Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                Me.CloseConnection()
                Me.baseDataTable = Me.baseDataSet.Tables(DataTableName)
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try

        End Sub

        Protected Sub FilDataSet(ByVal NameOfStoredProcedure As String, ByVal sql As String, ByVal DataSetName As String)
            Try
                Me.GetConnection()
                If NameOfStoredProcedure <> "" Then
                    Me.SqlCom = New SqlCommand(NameOfStoredProcedure, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                Else
                    Me.SqlCom = New SqlCommand(sql, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.Text
                End If
                Me.OpenConnection()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.baseDataSet = New DataSet(DataSetName)
                Me.baseDataSet.Clear()
                Me.SqlDat.Fill(Me.baseDataSet)
                Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                Me.CloseConnection()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Protected Sub InsertData(ByVal NameOfStoredProcedure As String, ByVal sql As String)
            If NameOfStoredProcedure <> "" Then
                Me.SqlCom = New SqlCommand(NameOfStoredProcedure, Me.SqlConn)
                Me.SqlCom.CommandType = CommandType.StoredProcedure
            Else
                Me.SqlCom = New SqlCommand(sql, Me.SqlConn)
                Me.SqlCom.CommandType = CommandType.Text
            End If
        End Sub

        Protected Sub UpdateData(ByVal NameOfStoredProcedure As String, ByVal sql As String)
            If NameOfStoredProcedure <> "" Then
                Me.SqlCom = New SqlCommand(NameOfStoredProcedure, Me.SqlConn)
                Me.SqlCom.CommandType = CommandType.StoredProcedure
            Else
                Me.SqlCom = New SqlCommand(sql, Me.SqlConn)
                Me.SqlCom.CommandType = CommandType.Text
            End If
        End Sub

        Protected Sub DeleteData(ByVal NameOfStoredProcedure As String, ByVal sql As String)
            If NameOfStoredProcedure <> "" Then
                Me.SqlCom = New SqlCommand(NameOfStoredProcedure, Me.SqlConn)
                Me.SqlCom.CommandType = CommandType.StoredProcedure
            Else
                Me.SqlCom = New SqlCommand(sql, Me.SqlConn)
                Me.SqlCom.CommandType = CommandType.Text
            End If
        End Sub

        Protected Sub ClearCommandParameters()
            If Not IsNothing(Me.SqlCom) Then
                Me.SqlCom.Parameters.Clear()
            End If

        End Sub

#End Region

#Region " Function "
        Protected Function setDataAdapter(ByVal cmd As SqlCommand) As SqlDataAdapter
            If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(cmd)
            Else : Me.SqlDat.SelectCommand = cmd
            End If
            Return Me.SqlDat
        End Function
        Protected Function CreateCommandSql(ByVal CommandType As CommandType, ByVal CommandText As String, ByVal ConnectSql As ConnectionTo) As SqlCommand
            Me.ResetConnection(ConnectSql)
            If (Me.SqlCom Is Nothing) Then
                Me.SqlCom = New SqlCommand()
            End If
            Me.SqlCom.Connection = Me.SqlConn
            Return ResetCommandText(CommandType, CommandText)
        End Function

        'data table just for another dataset to add
        Protected Function CreateNewDataTable(ByVal NameOfStoredProcedure As String, ByVal sql As String, ByVal dtTable As DataTable) As DataTable
            Try
                Me.GetConnection()
                If NameOfStoredProcedure <> "" Then
                    Me.SqlCom = New SqlCommand(NameOfStoredProcedure, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                Else
                    Me.SqlCom = New SqlCommand(sql, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.Text
                End If
                Me.OpenConnection()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(dtTable)
                Me.CloseConnection()
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return dtTable
        End Function

        Protected Function FillDataTable(ByRef dtTable As DataTable) As DataTable
            Try
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection()
                Me.SqlDat.Fill(dtTable)
                Me.CloseConnection()
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return dtTable
        End Function

        Protected Function CreateDataView(ByVal dataTable As DataTable) As DataView
            Return New DataView(dataTable)
        End Function

        Protected Function SaveChangesDSRelation(ByVal dataSetRelation As DataSet) As String
            Dim NumrowsChange As Integer = 0
            Try
                Me.GetConnection()
                Me.OpenConnection()
                Me.BeginTransaction()
                If Me.SqlCom.CommandType = CommandType.StoredProcedure Then
                    Dim ChildSqlCom As New SqlCommand(Me.ChildCommandText, Me.SqlConn)
                    ChildSqlCom.Transaction = Me.SqlTrans
                    ChildSqlCom.CommandType = CommandType.StoredProcedure
                    Me.ChildSqlDat = New SqlDataAdapter(ChildSqlCom)
                    Me.ChildSqlComb = New SqlCommandBuilder(Me.ChildSqlDat)
                    Dim ParentSqlCom As New SqlCommand(Me.ParentCommandtext, Me.SqlConn)
                    ParentSqlCom.CommandType = CommandType.StoredProcedure
                    ParentSqlCom.Transaction = Me.SqlTrans
                    Me.ParentSqlDat = New SqlDataAdapter(ParentSqlCom)
                    Me.ParentSqlComb = New SqlCommandBuilder(ParentSqlDat)
                Else
                    Dim ChildSqlCom As New SqlCommand(Me.ChildCommandText, Me.SqlConn)
                    ChildSqlCom.CommandType = CommandType.Text
                    ChildSqlCom.Transaction = Me.SqlTrans
                    Me.ChildSqlDat = New SqlDataAdapter(ChildSqlCom)
                    Me.ChildSqlComb = New SqlCommandBuilder(Me.ChildSqlDat)
                    Dim ParentSqlCom As New SqlCommand(Me.ParentCommandtext, Me.SqlConn)
                    ParentSqlCom.CommandType = CommandType.Text
                    ParentSqlCom.Transaction = Me.SqlTrans
                    Me.ParentSqlDat = New SqlDataAdapter(ParentSqlCom)
                    Me.ParentSqlComb = New SqlCommandBuilder(ParentSqlDat)
                End If
                If Not IsNothing(dataSetRelation.Tables(0)) Then
                    If Me.ChildSqlDat.Update(dataSetRelation.Tables(0)) > 0 Then
                        NumrowsChange += 1
                    Else
                        NumrowsChange = 0
                    End If
                End If
                If Not IsNothing(dataSetRelation.Tables(1)) Then
                    If Me.ParentSqlDat.Update(dataSetRelation.Tables(1)) > 0 Then
                        NumrowsChange += 1
                    End If
                End If
                Me.CommiteTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return NumrowsChange.ToString() & " Changes Saved SuccesFully"
        End Function

        Protected Function SaveDataTable(ByVal baseDataSet As DataSet) As String
            Dim Message As String = ""
            Try
                Me.GetConnection()
                Me.OpenConnection()
                Me.BeginTransaction()
                If Me.SqlDat.Update(baseDataSet.Tables(0)) > 0 Then
                    Message = "Data Saved Succesfully"
                End If
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Message
        End Function

        Protected Function ExecuteScalar(ByVal NameOfStoredProcedure As String, ByVal sql As String) As Object
            Dim RetVal As Object = Nothing
            Try
                Me.GetConnection()
                If NameOfStoredProcedure <> "" Then
                    Me.SqlCom = New SqlCommand(NameOfStoredProcedure, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                Else
                    Me.SqlCom = New SqlCommand(sql, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.Text
                End If
                Me.OpenConnection()
                RetVal = Me.SqlCom.ExecuteScalar()
                Me.CloseConnection()
                Me.SqlCom.Parameters.Clear()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return RetVal
        End Function

        'FUNFTION INI DI PAKAI APABILA MAU MENGAMBIL RETURN VALUE DARI SUATU STORED PROCEDURE SEHINGGA
        'PARAMETER DARI RETURN VALUE BISA DI AMBIL SEBAGAI REFERENSI VARIABEL /YANG LAINNYA
        'KARENA SETELAH DI EXECUTE PARAMETER TIDAK DI CLEAR KAN DULU

        Protected Function GetReturnValueByExecuteScalar(ByVal ParameterRetval As String) As Object
            Dim retVal As Object
            Try
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar()
                retVal = Me.SqlCom.Parameters(ParameterRetval).Value
                Me.CloseConnection()
                Return retVal
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function

        Protected Function ExecuteScalar() As Object
            Dim RetVal As Object = Nothing
            Try
                Me.OpenConnection()
                RetVal = Me.SqlCom.ExecuteScalar()
                Me.CloseConnection()
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
            Return RetVal
        End Function

        Protected Function ExecuteReader() As SqlDataReader
            Try
                Me.OpenConnection()
                Me.SqlRe = Me.SqlCom.ExecuteReader()
            Catch ex As Exception
                Me.CloseConnection()
                Me.SqlRe.Close()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.SqlRe
        End Function

        Protected Function ExecuteReader(ByVal NameOfStoredProcedure As String, ByVal sql As String) As SqlDataReader
            Try
                Me.GetConnection()
                If NameOfStoredProcedure <> "" Then
                    Me.SqlCom = New SqlCommand(NameOfStoredProcedure, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                Else
                    Me.SqlCom = New SqlCommand(sql, Me.SqlConn)
                    Me.SqlCom.CommandType = CommandType.Text
                End If
                Me.OpenConnection()
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                'Me.SqlCom.Parameters.Clear()
            Catch ex As Exception
                Me.CloseConnection()
                Me.SqlRe.Close()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.SqlRe
        End Function

        Protected Function GetdatasetRelation(ByVal SqlCommandType As String, ByVal ParentCommandText As String, _
      ByVal ChildCommandText As String, ByVal ParentTableName As String, ByVal ChildTableName As String, _
      ByVal ParentColumnName As String, ByVal ChildColumnName As String, ByVal NameOfRelation As String, ByVal RuleName As String) As DataSet
            Try
                Me.GetConnection()
                Me.baseDataSetRelation = New DataSet()
                Me.baseDataSetRelation.Clear()
                Me.SqlCom = New SqlCommand(ChildCommandText, Me.SqlConn)
                If SqlCommandType = "StoredProcedure" Then
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                Else
                    Me.SqlCom.CommandType = CommandType.Text
                End If
                Me.ChildSqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.baseChildTable = New DataTable(ChildTableName)
                Me.baseChildTable.Clear()
                Me.OpenConnection()
                Me.ChildSqlDat.Fill(Me.baseChildTable)
                Me.ChildSqlComb = New SqlCommandBuilder(Me.ChildSqlDat)
                Me.baseDataSetRelation.Tables.Add(Me.baseChildTable)
                Me.SqlCom = New SqlCommand(ParentCommandText, Me.SqlConn)
                If SqlCommandType = "StoredProcedure" Then
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                Else
                    Me.SqlCom.CommandType = CommandType.Text
                End If
                Me.ParentSqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.baseParentTable = New DataTable(ParentTableName)
                Me.baseParentTable.Clear()
                Me.ParentSqlDat.Fill(Me.baseParentTable)
                Me.CloseConnection()
                Me.ParentSqlComb = New SqlCommandBuilder(Me.ParentSqlDat)
                Me.baseDataSetRelation.Tables.Add(Me.baseParentTable)
                Me.DR = New DataRelation(NameOfRelation, Me.baseParentTable.Columns(ParentColumnName), Me.baseChildTable.Columns(ChildColumnName))
                Me.baseDataSetRelation.Relations.Add(Me.DR)
                Select Case RuleName
                    Case "Update"
                        Me.baseDataSetRelation.Relations(NameOfRelation).ChildKeyConstraint.UpdateRule = Rule.Cascade
                    Case "Delete"
                        Me.baseDataSetRelation.Relations(NameOfRelation).ChildKeyConstraint.DeleteRule = Rule.Cascade
                End Select
                Me.ParentCommandtext = ParentCommandText
                Me.ChildCommandText = ChildCommandText
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.baseDataSetRelation
        End Function

        Protected Function AddrelationToDataSet(ByVal ds As DataSet, ByVal ParentColumnName As DataColumn, ByVal ChildColumnName As DataColumn, ByVal RelationName As String)
            Dim NextRelation As DataRelation = New DataRelation(RelationName, ParentColumnName, ChildColumnName)
            ds.Relations.Add(NextRelation)
            Return ds
        End Function

        Protected Sub CancelEditDataView(ByVal index As Integer, ByVal DV As DataView)
            DV(index).BeginEdit()
        End Sub

#End Region

#Region " Destructor "
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If disposing Then
                If Not IsNothing(Me.DR) Then
                    Me.DR = Nothing
                End If
                If Not IsNothing(Me.baseDataTable) Then
                    Me.baseDataTable.Dispose()
                    Me.baseDataTable = Nothing
                End If
                If Not IsNothing(Me.baseDataView) Then
                    Me.baseDataView.Dispose()
                    Me.baseDataView = Nothing
                End If
                If Not IsNothing(Me.baseDataRowView) Then
                    Me.baseDataRowView = Nothing
                End If
                If Not IsNothing(Me.ParentSqlDat) Then
                    Me.ParentSqlDat.Dispose()
                    Me.ParentSqlDat = Nothing
                End If
                If Not IsNothing(Me.ChildSqlDat) Then
                    Me.ChildSqlDat.Dispose()
                    Me.ChildSqlDat = Nothing
                End If
                If Not IsNothing(Me.ParentSqlComb) Then
                    Me.ParentSqlComb.Dispose()
                    Me.ParentSqlComb = Nothing
                End If
                If Not IsNothing(Me.ChildSqlComb) Then
                    Me.ChildSqlComb.Dispose()
                    Me.ChildSqlComb = Nothing
                End If
                If Not IsNothing(Me.baseChildTable) Then
                    Me.baseChildTable.Dispose()
                    Me.baseChildTable = Nothing
                End If
                If Not IsNothing(Me.baseParentTable) Then
                    Me.baseParentTable.Dispose()
                    Me.baseParentTable = Nothing
                End If
                If Not IsNothing(Me.baseDataSetRelation) Then
                    Me.baseDataSetRelation.Dispose()
                    Me.baseDataSetRelation = Nothing
                End If
                If Not IsNothing(Me.baseChekTable) Then
                    Me.baseChekTable.Dispose()
                    Me.baseChekTable = Nothing
                End If
                If Not IsNothing(Me.SqlDatChek) Then
                    Me.SqlDatChek.Dispose()
                    Me.SqlDatChek = Nothing
                End If
                If Not IsNothing(Me.baseDataSet) Then
                    Me.baseDataSet.Dispose()
                    Me.baseDataSet = Nothing
                End If
            End If
        End Sub
#End Region

    End Class
End Namespace

