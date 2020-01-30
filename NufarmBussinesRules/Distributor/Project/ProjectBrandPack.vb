Imports System.Data.SqlClient
Namespace DistributorProject
    Public Class ProjectBrandPack
        Inherits ProjectRegistering
        Private m_daProjBPDetail As SqlDataAdapter
        Public ReadOnly Property DataAdapter() As SqlDataAdapter
            Get
                Return Me.m_daProjBPDetail
            End Get
        End Property

        Public Sub New()
            Try
                Me.CreateCommandSql("", "SELECT * FROM PROJ_BRANDPACK")
                Me.m_daProjBPDetail = New SqlDataAdapter(Me.SqlCom)
                Dim CB As New SqlCommandBuilder(Me.m_daProjBPDetail)
            Catch ex As Exception

            End Try

        End Sub
        Public Sub New(ByVal PROJ_BRANDPACK_ID As String)
            Dim tblPB As New DataTable("T_ProjBrandPack") : tblPB.Clear()
            If Not String.IsNullOrEmpty(PROJ_BRANDPACK_ID) Then
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT PROJ_BRANDPACK_ID,PROJ_REF_NO,BRANDPACK_ID,CREATE_BY,CREATE_DATE,MODIFY_BY,MODIFY_DATE FROM PROJ_BRANDPACK WHERE PROJ_BRANDPACK_ID = @PROJ_BRANDPACK_ID ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PROJ_BRANDPACK_ID", SqlDbType.VarChar, PROJ_BRANDPACK_ID, 30)
                Me.m_daProjBPDetail = New SqlDataAdapter(Me.SqlCom)
            End If
            'Me.CreateCommandSql("", "SELECT PROJ_BRANDPACK_ID,PROJ_REF_NO,BRANDPACK_ID," & _
            '"DISTRIBUTOR_ID,MAX_ORDER,APPROVED_DISC_PCT,START_DATE,END_DATE,CREATE_BY,CREATE_DATE," & _
            '"MODIFY_BY,MODIFY_DATE FROM PROJ_BRANDPACK WHERE PROJ_BRANDPACK_ID = '" + PROJ_BRANDPACK_ID + "'")

            'Dim CB As New SqlCommandBuilder(Me.m_daProjBPDetail)
        End Sub
        Public Overloads Sub dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_daProjBPDetail) Then
                Me.m_daProjBPDetail.Dispose()
                Me.m_daProjBPDetail = Nothing
            End If
        End Sub
        Public Function GetDataTable(ByVal PROJ_BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As DataTable
            Dim dtTable As DataTable
            Try
                dtTable = New DataTable("BRANDPACK_DETAIL") : dtTable.Clear()
                If String.IsNullOrEmpty(PROJ_BRANDPACK_ID) Then
                    ''BIKIN data column 
                    '     DataColumn[] Key = new DataColumn[1]; DataColumn colCodeApp = new DataColumn("CodeApp", typeof(string));
                    'tblAchHeader.Columns.Add(colCodeApp); Key[0] = colCodeApp;
                    'tblAchHeader.PrimaryKey = Key;
                    With dtTable
                        .Columns.Add("PROJ_BRANDPACK_ID", Type.GetType("System.String"))
                        .Columns.Add("BRANDPACK_ID", Type.GetType("System.String"))
                        .Columns.Add("PROJ_REF_NO", Type.GetType("System.String"))
                        .Columns.Add("CREATE_BY", Type.GetType("System.String"))
                        .Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"))
                    End With
                    Dim key() As DataColumn = New DataColumn() {}
                    key(0) = dtTable.Columns("PROJ_BRANDPACK_ID")
                    dtTable.PrimaryKey = key
                Else
                    Me.OpenConnection() : Me.m_daProjBPDetail.Fill(dtTable) : Me.ClearCommandParameters()
                    If mustCloseConnection Then : Me.CloseConnection() : End If
                End If
                'Me.FillDataTable(dtTable)

            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return dtTable
        End Function

        'Public Function GetDataTable(ByVal POJ_REF_NO As String) As DataTable
        '    Try
        '        Me.m_tblProjBPDetail = New DataTable("T_ProjBPDetail")
        '        Me.m_tblProjBPDetail.Clear()
        '        Me.FillDataTable(Me.m_tblProjBPDetail)
        '    Catch ex As Exception

        '    End Try
        '    Return Me.m_tblProjBPDetail
        'End Function
    End Class
End Namespace

