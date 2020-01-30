Imports System.Data
Namespace Program
    Public Class Core
        Inherits NufarmBussinesRules.Program.BrandPackInclude
        Private m_ViewProgramBrandPack As DataView
        Private m_ViewDistributorSteppingDiscount As DataView
        Private m_ViewStepping As DataView
        Public Sub New()
            MyBase.New()
            Me.m_ViewDistributorSteppingDiscount = Nothing
            Me.m_ViewProgramBrandPack = Nothing
        End Sub
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewProgramBrandPack) Then
                Me.m_ViewProgramBrandPack.Dispose()
                Me.m_ViewProgramBrandPack = Nothing
            End If
            If Not IsNothing(Me.m_ViewDistributorSteppingDiscount) Then
                Me.m_ViewDistributorSteppingDiscount.Dispose()
                Me.m_ViewDistributorSteppingDiscount = Nothing
            End If
        End Sub
        Public ReadOnly Property ViewDistributorSteppingDiscount() As DataView
            Get
                'set view for ViewDistributorStepping

                Return Me.m_ViewDistributorSteppingDiscount
            End Get
            'Set(ByVal value As DataView)
            '    Me.m_ViewDistributorSteppingDiscount = value
            'End Set
        End Property
        Public ReadOnly Property ViewProgramBrandPack() As DataView
            Get
                Return Me.m_ViewProgramBrandPack
            End Get
        End Property
        Public ReadOnly Property ViewStepping() As DataView
            Get
                Return Me.m_ViewStepping
            End Get
        End Property
        Public Function CreateViewProgramBrandPack_1(ByVal mustCloseConnection As Boolean, Optional ByVal Start_Date As Object = Nothing, Optional ByVal End_Date As Object = Nothing) As DataView
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT MP.PROGRAM_ID, MP.PROGRAM_NAME, MP.START_DATE, MP.END_DATE, MB.BRANDPACK_ID,BP.BRANDPACK_NAME, MB.START_DATE AS BRANDPACK_START_DATE," & vbCrLf & _
                        " MB.END_DATE AS BRANDPACK_END_DATE, MB.PROG_BRANDPACK_ID " & vbCrLf & _
                        " FROM BRND_BRANDPACK BP INNER JOIN MRKT_BRANDPACK MB ON BP.BRANDPACK_ID = MB.BRANDPACK_ID RIGHT OUTER JOIN " & vbCrLf & _
                        " MRKT_MARKETING_PROGRAM MP ON MP.PROGRAM_ID = MB.PROGRAM_ID " & vbCrLf
                If (Not IsNothing(Start_Date)) And (Not IsNothing(End_Date)) Then
                    Query &= " WHERE MP.START_DATE >= @START_DATE AND MP.END_DATE <= @END_DATE ;"
                ElseIf (Not IsNothing(Start_Date)) And (IsNothing(End_Date)) Then
                    Query &= " WHERE MP.START_DATE >= @START_DATE ;"
                ElseIf (IsNothing(Start_Date)) And (Not IsNothing(End_Date)) Then
                    Query &= " WHERE MP.END_DATE <= @END_DATE ;"
                Else
                    Query &= " WHERE MP.START_DATE <= @GETDATE AND MP.END_DATE >= @GETDATE ;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                If Not IsNothing(Start_Date) Then
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Start_Date)
                End If
                If Not IsNothing(End_Date) Then
                    Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, End_Date)
                End If
                If IsNothing(Start_Date) And IsNothing(End_Date) Then
                    Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                End If
                Dim tblMPB As New DataTable("MPB") : tblMPB.Clear() : Me.OpenConnection()

                Me.setDataAdapter(Me.SqlCom).Fill(tblMPB) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.m_ViewProgramBrandPack = tblMPB.DefaultView()
                Me.m_ViewProgramBrandPack.Sort = "PROGRAM_ID"
                Return Me.m_ViewProgramBrandPack
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Function CreateViewProgramBrandPack() As DataView
            Try
                Dim Query As String = "SET NOCOUNT ON;SELECT * FROM MRKT_PROGRAM_BRANDPACK WHERE START_DATE <= GETDATE() AND END_DATE >= GETDATE();"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblMPB As New DataTable("MBD")
                tblMPB.Clear()
                Me.FillDataTable(tblMPB)
                Me.m_ViewProgramBrandPack = tblMPB.DefaultView()
                Me.m_ViewProgramBrandPack.Sort = "PROGRAM_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewProgramBrandPack
        End Function
        Public Function CreateViewDistributorSteppingDiscount() As DataView
            Try
                Me.CreateCommandSql("", "SELECT * FROM MARKETING_BRANDPACK_DISTRIBUTOR")
                Dim tblMBD As New DataTable("MBD")
                tblMBD.Clear()
                Me.FillDataTable(tblMBD)
                Me.m_ViewDistributorSteppingDiscount = tblMBD.DefaultView()
                Me.m_ViewDistributorSteppingDiscount.Sort = "BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistributorSteppingDiscount
        End Function
        Public Function CreateViewDistributorSteppingDiscount_1(Optional ByVal Start_Date As Object = Nothing, Optional ByVal EndD_date As Object = Nothing) As DataView
            Try
                ''CHECK KE DATABASE APAKAH ADA TABLE ##T_SHIP_TO YAITU TABLE TEMPORARY SHIP_TO
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_SHIP_TO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " DROP TABLE TEMPDB..##T_SHIP_TO_" & Me.ComputerName & " ;" & vbCrLf & _
                        " END " & vbCrLf & _
                        " SELECT ST.SHIP_TO_ID,TER.TERRITORY_ID,TM.MANAGER " & vbCrLf & _
                        " INTO ##T_SHIP_TO_" & Me.ComputerName & " FROM TERRITORY TER INNER JOIN SHIP_TO ST ON TER.TERRITORY_ID = ST.TERRITORY_ID " & vbCrLf & _
                        " INNER JOIN TERRITORY_MANAGER TM ON ST.TM_ID = TM.TM_ID  WHERE ST.INACTIVE = 0 ;" & vbCrLf & _
                        " CREATE CLUSTERED INDEX IX_T_SHIP_TO ON ##T_SHIP_TO_" & Me.ComputerName & "(SHIP_TO_ID,TERRITORY_ID) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT TER.TERRITORY_AREA,MBD.PROG_BRANDPACK_DIST_ID, MBD.PROG_BRANDPACK_ID, " & vbCrLf & _
                        "MBD.DISTRIBUTOR_ID, DR.DISTRIBUTOR_NAME,MBD.START_DATE, MBD.END_DATE, MBD.GIVEN_DISC_PCT AS [Given %], MBD.TARGET_QTY," & vbCrLf & _
                        "MBD.TARGET_DISC_PCT AS [Target %], MBD.STEPPING_FLAG AS Stepping,MBD.AGREE_BRANDPACK_ID,BB.BRANDPACK_NAME, " & vbCrLf & _
                        "MB.PROGRAM_ID, MB.BRANDPACK_ID, MBD.PRICE_HK,MBD.TARGET_HK, MBD.ISHK,MBD.TARGET_DK, MBD.GIVEN_DK,MBD.ISDK, MBD.TARGET_CP, " & vbCrLf & _
                        "MBD.GIVEN_CP, MBD.ISCP, MBD.ISSCPD,MBD.ISCPMRT,MBD.GIVEN_CPMRT,MBD.TARGET_CPMRT,CAST((CASE MBD.IS_T_TM_DIST WHEN 1 THEN 0 ELSE 1 END) AS BIT)AS IS_T_TM_DIST,ST.SHIP_TO_ID,ST.MANAGER AS TM,MBD.TARGET_PKPP, MBD.GIVEN_PKPP, MBD.BONUS_VALUE, MBD.ISPKPP,MBD.ISRPK, MBD.ISCPR, " & vbCrLf & _
                        "MBD.TARGET_CPR, MBD.GIVEN_CPR,MBD.DESCRIPTIONS FROM  DIST_DISTRIBUTOR DR INNER JOIN MRKT_BRANDPACK_DISTRIBUTOR MBD ON DR.DISTRIBUTOR_ID = MBD.DISTRIBUTOR_ID INNER JOIN " & vbCrLf & _
                        "MRKT_BRANDPACK MB ON MBD.PROG_BRANDPACK_ID = MB.PROG_BRANDPACK_ID INNER JOIN BRND_BRANDPACK BB ON MB.BRANDPACK_ID = BB.BRANDPACK_ID " & vbCrLf & _
                        "INNER JOIN TERRITORY TER ON TER.TERRITORY_ID = DR.TERRITORY_ID LEFT OUTER JOIN tempdb..##T_SHIP_TO_" & Me.ComputerName & " ST ON ST.SHIP_TO_ID = MBD.SHIP_TO_ID " & vbCrLf
                If (Not IsNothing(Start_Date)) And (Not IsNothing(EndD_date)) Then
                    Query &= " WHERE MBD.START_DATE >= @START_DATE AND MBD.END_DATE <= @END_DATE ;"
                ElseIf (Not IsNothing(Start_Date)) And (IsNothing(EndD_date)) Then
                    Query &= " WHERE MBD.START_DATE >= @START_DATE "
                ElseIf (IsNothing(Start_Date)) And (Not IsNothing(EndD_date)) Then
                    Query &= " WHERE MBD.END_DATE <= @END_DATE ;"
                Else
                    'Me.CreateViewDistributorSteppingDiscount()
                    Query &= " WHERE MBD.START_DATE <= @GETDATE AND MBD.END_DATE >= @GETDATE;"
                End If
                Me.ResetCommandText(CommandType.Text, Query)

                If Not IsNothing(Start_Date) Then
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Start_Date)
                End If
                If Not IsNothing(EndD_date) Then
                    Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndD_date)
                End If
                If IsNothing(Start_Date) And IsNothing(END_DATE) Then
                    Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                End If
                Dim tblMBD As New DataTable("MBD") : tblMBD.Clear()
                setDataAdapter(Me.SqlCom).Fill(tblMBD) : Me.ClearCommandParameters() : Me.CloseConnection()
                Me.m_ViewDistributorSteppingDiscount = tblMBD.DefaultView()
                Me.m_ViewDistributorSteppingDiscount.Sort = "BRANDPACK_ID"
                Return Me.m_ViewDistributorSteppingDiscount
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Function CreateViewStepping() As DataView
            Try
                Me.CreateCommandSql("", "SELECT * FROM VIEW_STEPPING_DISCOUNT")
                Dim tblStepping As New DataTable("STEPPING_DISCOUNT")
                tblStepping.Clear()
                Me.FillDataTable(tblStepping)
                Me.m_ViewStepping = tblStepping.DefaultView()
                Me.m_ViewStepping.Sort = "PROG_BRANDPACK_DIST_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewStepping
        End Function
        'FUNCTION UNTUK MENGAMBIL ENDATE AGREEMENT BERDASARKAN BRANDPACK DI PROGRAM YANG DI PILIH USER
        'UNTUK MENTRAP END_DATE DARI CALCOMBO DTPIC_END SUPAYA MAXDATE TIDAK LEBIH
        Public Function GetAgreementEndDate(ByVal DISTRIBUTOR_ID As String, ByVal BRANDPACK_ID As String) As Date
            Try
                Dim Query As String = ""
                '"SET NOCOUNT ON;SELECT END_DATE FROM AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT" & _
                '                ",AGREE_BRANDPACK_INCLUDE WHERE AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO" & _
                '                " AND DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "' " & _
                '                " AND AGREE_AGREEMENT.AGREEMENT_NO = AGREE_BRANDPACK_INCLUDE.AGREEMENT_NO AND AGREE_BRANDPACK_INCLUDE.BRANDPACK_ID = '" & _
                '                BRANDPACK_ID & "' AND AGREE_AGREEMENT.END_DATE >= GETDATE()"
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT TOP 1 END_DATE FROM VIEW_AGREE_BRANDPACK_INCLUDE WHERE DISTRIBUTOR_ID = '" & _
                        DISTRIBUTOR_ID & "' AND BRANDPACK_ID = '" & BRANDPACK_ID & "' AND END_DATE >= @GETDATE;"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Dim ED As Object = Me.ExecuteScalar()
                If IsNothing(ED) Then
                    Throw New Exception("Could not find Agreement for brandpack_id '" & BRANDPACK_ID & "'")
                End If
                Return Convert.ToDateTime(ED)
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
       
    End Class

End Namespace

