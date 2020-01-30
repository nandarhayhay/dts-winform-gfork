Imports System.Data.SqlClient
Imports System.Configuration
Namespace DataAccesLayer
    Public MustInherit Class BaseLayer : Implements IDisposable

#Region " Deklarasi "
        Protected SqlCom As SqlCommand
        Protected SqlConn As SqlConnection
        Protected SqlDat As SqlDataAdapter
        Protected sqlComb As SqlCommandBuilder
        Protected SqlRe As SqlDataReader
        Protected SqlTrans As SqlTransaction
        Protected sqlPar As SqlParameter
        Public Delegate Sub onSaving()
        Private Const nTambah As Integer = 57
        Private NuFarmConstring As String
        Private ConnectTo As ConnectionTo = ConnectionTo.Nufarm
#End Region

#Region " Constructor "
        Protected Enum ConnectionTo
            Nufarm
            NI87
        End Enum
        Public Sub New()
            Me.SqlCom = Nothing
            Me.sqlComb = Nothing
            Me.SqlConn = Nothing
            Me.SqlDat = Nothing
            Me.sqlPar = Nothing
            Me.SqlRe = Nothing
            Me.SqlTrans = Nothing
        End Sub
#End Region

#Region " Void "
        Protected Sub OpenConnection()
            If Me.SqlConn Is Nothing Then
                Me.GetConnection()
            End If
            If (Me.SqlConn.State = ConnectionState.Broken) Or (Me.SqlConn.State = ConnectionState.Closed) Then
                Me.SqlConn.Open()
            End If
        End Sub

        Protected Sub BeginTransaction()
            Me.SqlTrans = Me.SqlConn.BeginTransaction(IsolationLevel.ReadCommitted)
        End Sub
        Protected Sub RollbackTransaction()
            If Not IsNothing(Me.SqlTrans) Then
                Me.SqlTrans.Rollback()
                Me.SqlTrans.Dispose()
                Me.SqlTrans = Nothing
            End If
        End Sub
        Protected Sub CommiteTransaction()
            If Not IsNothing(Me.SqlTrans) Then
                Me.SqlTrans.Commit()
                Me.SqlTrans.Dispose()
                Me.SqlTrans = Nothing
            End If
        End Sub
        Protected Sub CloseConnection()
            If (Not IsNothing(Me.SqlConn)) Then
                If (Me.SqlConn.State = ConnectionState.Broken) Or (Me.SqlConn.State = ConnectionState.Connecting) _
                          Or (Me.SqlConn.State = ConnectionState.Executing) Or (Me.SqlConn.State = ConnectionState.Fetching) _
                          Or (Me.SqlConn.State = ConnectionState.Open) Then
                    Me.SqlConn.Close()
                End If
            End If
        End Sub
#End Region

#Region " Function "
        Protected Function GetConnection() As SqlConnection
            If Me.SqlConn Is Nothing Then '"Data Source=PRISONBREAK;Initial Catalog=Nufarm;User ID=sa;Password=nufDB2007" 
                Me.SqlConn = New SqlConnection()
            End If
            If Me.SqlConn.ConnectionString <> "" Then
                If Me.ConnectTo = ConnectionTo.NI87 Then ''balikan connection to DTS
                    Me.NuFarmConstring = ConfigurationManager.ConnectionStrings("NuFarmConstring").ConnectionString
                    Me.SqlConn.ConnectionString = Me.NuFarmConstring
                End If
            Else
                Me.NuFarmConstring = ConfigurationManager.ConnectionStrings("NuFarmConstring").ConnectionString
                Me.SqlConn.ConnectionString = Me.NuFarmConstring
            End If
            Return Me.SqlConn
        End Function
        Protected Function ResetCommandText(ByVal CommandType As CommandType, ByVal CommandText As String) As SqlCommand
            Me.SqlCom.CommandType = CommandType
            Me.SqlCom.CommandText = CommandText
            If IsNothing(Me.SqlCom.Connection) Then
                Me.SqlCom.Connection = Me.GetConnection()
            End If
            Return Me.SqlCom
        End Function
        Protected Function ResetConnection(ByVal ConnecSql As ConnectionTo) As SqlConnection
            If Me.SqlConn Is Nothing Then
                Me.SqlConn = New SqlConnection()
            End If
            If ConnecSql = ConnectionTo.NI87 Then
                If Not IsNothing(Me.SqlConn) Then
                    If Me.SqlConn.State = ConnectionState.Broken Or Me.SqlConn.State = ConnectionState.Closed Then
                        Me.SqlConn.ConnectionString = ConfigurationManager.ConnectionStrings("NI87Constring").ConnectionString
                    End If
                End If
            Else
                If Not IsNothing(Me.SqlConn) Then
                    If Me.SqlConn.State = ConnectionState.Broken Or Me.SqlConn.State = ConnectionState.Closed Then
                        Me.NuFarmConstring = ConfigurationManager.ConnectionStrings("NuFarmConstring").ConnectionString
                        Me.SqlConn.ConnectionString = Me.NuFarmConstring
                    End If
                End If
            End If
            'NI78Constring
            Return Me.SqlConn
        End Function
        Protected Function ResetConnectiontiontoDefault() As SqlConnection
            If Not IsNothing(Me.SqlConn) Then
                If Me.SqlConn.ConnectionString <> "" Then
                    If Me.ConnectTo = ConnectionTo.NI87 Then ''balikan connection to DTS
                        Me.NuFarmConstring = ConfigurationManager.ConnectionStrings("NuFarmConstring").ConnectionString
                        Me.SqlConn.ConnectionString = Me.NuFarmConstring
                    End If
                Else
                    Me.NuFarmConstring = ConfigurationManager.ConnectionStrings("NuFarmConstring").ConnectionString
                    Me.SqlConn.ConnectionString = Me.NuFarmConstring
                End If
            End If
            Return Me.SqlConn
        End Function
        Protected Function ExecuteNonQuery() As Integer
            Dim RetVal As Integer = 0
            Me.SqlCom.Transaction = Me.SqlTrans
            RetVal = Me.SqlCom.ExecuteNonQuery()
            Me.SqlCom.Parameters.Clear()
            Return RetVal
        End Function
        'deskrip configuration setting
        Protected Function Deskrip(ByVal cInput As String) As String
            Dim nHitung As Integer = 0
            Dim cString As String = ""
            Dim cOutput As String = ""
            cInput = Trim(cInput)
            cInput = StrReverse(cInput)

            For nHitung = 1 To cInput.Length
                cString = cInput.Substring(nHitung - 1, 1)
                cOutput += Chr((Asc(cString) + nHitung) - nTambah)
            Next

            Return cOutput

        End Function
        Protected Function Enkrip(ByVal cInput As String) As String
            Dim nHitung As Integer
            Dim cString = "", cOutput As String = ""
            'Menghapus karakter spasi disebelah kiri dan kanan
            cInput = Trim(cInput)

            For nHitung = 1 To cInput.Length
                cString = cInput.Substring(nHitung - 1, 1)
                cOutput += Chr((Asc(cString) + nTambah) - nHitung)
            Next
            'Hasilnya dibalik 
            cOutput = StrReverse(cOutput)
            Return cOutput
        End Function
        'protected fui
#End Region

#Region " IDisposable Support "
        Private disposedValue As Boolean = False        ' To detect redundant calls
        ' IDisposable

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If Not IsNothing(Me.SqlCom) Then
                        Me.SqlCom.Dispose()
                        Me.SqlCom = Nothing
                    End If
                    If Not IsNothing(Me.sqlComb) Then
                        Me.sqlComb.Dispose()
                        Me.sqlComb = Nothing
                    End If
                    If Not IsNothing(Me.SqlConn) Then
                        If Me.SqlConn.State = ConnectionState.Broken Or Me.SqlConn.State = ConnectionState.Connecting _
                        Or Me.SqlConn.State = ConnectionState.Executing Or Me.SqlConn.State = ConnectionState.Fetching _
                        Or Me.SqlConn.State = ConnectionState.Open Then
                            Me.SqlConn.Close()
                        End If
                        Me.SqlConn.Dispose()
                        Me.SqlConn = Nothing
                    End If
                    If Not IsNothing(Me.SqlDat) Then
                        Me.SqlDat.Dispose()
                        Me.SqlDat = Nothing
                    End If
                    If Not IsNothing(Me.SqlRe) Then
                        Me.SqlRe = Nothing
                    End If
                    If Not IsNothing(Me.SqlTrans) Then
                        Me.SqlTrans.Dispose()
                        Me.SqlTrans = Nothing
                    End If
                    If Not IsNothing(Me.sqlPar) Then
                        Me.sqlPar = Nothing
                    End If
                    GC.SuppressFinalize(Me)
                    ' TODO: free unmanaged resources when explicitly called
                End If
                Me.disposedValue = True
                ' TODO: free shared unmanaged resources
            End If
        End Sub
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Me.Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace

