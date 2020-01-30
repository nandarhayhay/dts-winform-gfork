Imports System.Data.SqlClient
Namespace Program
    Public Class SteppingDiscount
        Inherits NufarmBussinesRules.Program.BrandPackInclude
        Private m_ViewSteppingDiscount As DataView
        Private m_dsSteppingDiscount As DataSet
        Private m_ViewDistSteping As DataView
        Public Sub New()
            Me.m_dsSteppingDiscount = Nothing
            Me.m_ViewSteppingDiscount = Nothing
        End Sub
        Public Sub SaveChanges(ByVal ds As DataSet)
            Try
                Me.GetConnection()
                Dim sqlCom1 As New SqlCommand()
                With sqlCom1
                    .Connection = Me.SqlConn
                    .CommandText = "SELECT * FROM MRKT_STEPPING_DISCOUNT"
                    .CommandType = CommandType.Text
                End With
                Dim sqlDat1 As New SqlDataAdapter(sqlCom1)
                Dim sqlComb1 As New SqlCommandBuilder(sqlDat1)
                Me.OpenConnection()
                Me.BeginTransaction()
                sqlCom1.Transaction = Me.SqlTrans
                sqlDat1.Update(ds.Tables(0))
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As DBConcurrencyException
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw New DBConcurrencyException(Me.MessageDBConcurency)
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Function GetRowByPROG_BRANDPACK_DIST_ID(ByVal PROG_BRANDPACK_DIST_ID As String) As Integer
            Dim FoundRow As Integer
            Try
                'me.CreateCommandSql("","SELECT PROGRAM_NAME,BRANDPACK_NAME FROM
                If Not IsNothing(Me.m_ViewDistSteping) Then
                    Dim index As Integer = Me.m_ViewDistSteping.Find(PROG_BRANDPACK_DIST_ID)
                    If index <> -1 Then
                        FoundRow = index
                        Me.PROGRAM_NAME = Me.m_ViewDistSteping(index)("PROGRAM_NAME").ToString()
                        Me.BRANDPACK_NAME = Me.m_ViewDistSteping(index)("BRANDPACK_NAME").ToString()
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return FoundRow
        End Function

        Public Function CreateViewSteppingDiscount() As DataView
            Try
                Me.FillDataTable("", "SELECT * FROM MRKT_STEPPING_DISCOUNT WHERE PROG_BRANDPACK_DIST_ID = ''", "STEPPING_DISCOUNT")
                Me.m_dsSteppingDiscount = Me.baseDataSet
                Me.m_ViewSteppingDiscount = Me.m_dsSteppingDiscount.Tables(0).DefaultView()
                Me.m_ViewSteppingDiscount.ApplyDefaultSort = True
            Catch ex As Exception

            End Try
            Return Me.m_ViewSteppingDiscount
        End Function
        Public Function CreateViewSteppingDiscount(ByVal PROG_BRANDPACK_DIST_ID As String) As DataView
            Try
                Me.FillDataTable("", "SELECT * FROM MRKT_STEPPING_DISCOUNT WHERE PROG_BRANDPACK_DIST_ID = '" + PROG_BRANDPACK_DIST_ID + "'", "STEPPING_DISCOUNT")
                Me.m_dsSteppingDiscount = Me.baseDataSet
                Me.m_ViewSteppingDiscount = Me.m_dsSteppingDiscount.Tables(0).DefaultView()
                Me.m_ViewSteppingDiscount.ApplyDefaultSort = True
                Me.m_ViewSteppingDiscount.RowStateFilter = DataViewRowState.CurrentRows
            Catch ex As Exception

            End Try
            Return Me.m_ViewSteppingDiscount
        End Function
        Public Function GetViewDistributor() As DataView
            Try
                Me.SearcData("Sp_GetView_PROG_BRANDPACK_DIST_ID", "")
                Me.m_ViewDistSteping = Me.baseChekTable.DefaultView()
                Me.m_ViewDistSteping.Sort = "PROG_BRANDPACK_DIST_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistSteping
        End Function
        Public Function GetViewDistributor_1() As DataView
            Try
                Me.SearcData("Sp_GetView_PROG_BRANDPACK_DIST_ID_1", "")
                Me.m_ViewDistSteping = Me.baseChekTable.DefaultView()
                Me.m_ViewDistSteping.Sort = "PROG_BRANDPACK_DIST_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistSteping
        End Function
        Public ReadOnly Property GetDataset() As DataSet
            Get
                Return Me.m_dsSteppingDiscount
            End Get
        End Property
        Public ReadOnly Property ViewSteppingDiscount() As DataView
            Get
                Return Me.m_ViewSteppingDiscount
            End Get
        End Property
        Public ReadOnly Property ViewDistStepping() As DataView
            Get
                Return Me.m_ViewDistSteping
            End Get
        End Property

        Public Overloads Sub Dispose()
            If Not IsNothing(Me.m_ViewSteppingDiscount) Then
                Me.m_ViewSteppingDiscount.Dispose()
                Me.m_ViewSteppingDiscount = Nothing
            End If
            If Not IsNothing(Me.m_dsSteppingDiscount) Then
                Me.m_dsSteppingDiscount.Dispose()
            End If
            If Not IsNothing(Me.m_ViewDistSteping) Then
                Me.m_ViewDistSteping.Dispose()
                Me.m_ViewDistSteping = Nothing
            End If

        End Sub
    End Class
End Namespace


