Imports System.Data
Imports System.Data.SqlClient
Imports Nufarm.Domain
Imports NufarmBussinesRules.common
Namespace OrderAcceptance
    Public Class Transporter : Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Public Sub New()
            MyBase.New()
        End Sub
        Private Query As String = ""
        Public GT_ID As String = ""
        Public TransporterName As String = ""
        Public Description As String = ""
        Public Function GetDatatable(ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT GT_ID,TRANSPORTER_NAME,ADDRESS,POSTAL_CODE ,PHONE,FAX,MOBILE AS MOBILE,CONTACT_PERSON,RESPONSIBLE_PERSON,NPWP,EMAIL,BIRTHDATE,CREATE_DATE FROM GON_TRANSPORTER ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql(CommandType.StoredProcedure, "sp_executesql", ConnectionTo.Nufarm)
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)

                Me.OpenConnection()

                Dim dtTable As New DataTable("GON_TRANSPORTER") : dtTable.Clear()
                Me.setDataAdapter(Me.SqlCom).Fill(dtTable)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dtTable
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Overloads Sub DeleteData(ByVal GTID As String, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT GON_1_GT_ID FROM SPPB_BRANDPACK WHERE GON_1_GT_ID = @GT_ID) " & vbCrLf & _
                        " BEGIN RAISERROR('Can not delete data,data has already been used',16,1) ; RETURN ; END " & vbCrLf & _
                        " ELSE IF EXISTS(SELECT GON_2_GT_ID FROM SPPB_BRANDPACK WHERE GON_2_GT_ID = @GT_ID) " & vbCrLf & _
                        " BEGIN RAISERROR('Can not delete data,data has already been used',16,1) ; RETURN ; END " & vbCrLf & _
                        " ELSE IF EXISTS(SELECT GON_3_GT_ID FROM SPPB_BRANDPACK WHERE GON_3_GT_ID = @GT_ID) " & vbCrLf & _
                        " BEGIN RAISERROR('Can not delete data,data has already been used',16,1) ; RETURN ; END " & vbCrLf & _
                        " ELSE IF EXISTS(SELECT GON_4_GT_ID FROM SPPB_BRANDPACK WHERE GON_4_GT_ID = @GT_ID) " & vbCrLf & _
                        " BEGIN RAISERROR('Can not delete data,data has already been used',16,1) ; RETURN ; END " & vbCrLf & _
                        " ELSE IF EXISTS(SELECT GON_5_GT_ID FROM SPPB_BRANDPACK WHERE GON_5_GT_ID = @GT_ID) " & vbCrLf & _
                        " BEGIN RAISERROR('Can not delete data,data has already been used',16,1) ; RETURN ; END " & vbCrLf & _
                        " ELSE IF EXISTS(SELECT GON_6_GT_ID FROM SPPB_BRANDPACK WHERE GON_6_GT_ID = @GT_ID) " & vbCrLf & _
                        " BEGIN RAISERROR('Can not delete data,data has already been used',16,1) ; RETURN ; END " & vbCrLf & _
                        " ELSE IF EXISTS(SELECT GT_ID FROM GON_HEADER WHERE GT_ID = @GT_ID) " & vbCrLf & _
                        " BEGIN RAISERROR('Can not delete data,data has already been used',16,1) ; RETURN ; END " & vbCrLf & _
                        " DELETE FROM GON_TRANSPORTER WHERE GT_ID = @GT_ID ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@GT_ID", SqlDbType.VarChar, GTID, 16)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Sub SaveData(ByVal OTrans As Nufarm.Domain.Transporter, ByVal Mode As Helper.SaveMode, ByVal shouldReloadData As Boolean, ByRef tblData As DataTable, ByVal mustCloseConnection As Boolean)
            Try
                'getTransporterID

                Dim gtID As String = ""
                If Mode = Helper.SaveMode.Insert Then
                    Dim clsSPPBdetail As New SPPB_Detail() : gtID = clsSPPBdetail.CreatetransporterID(OTrans.NameApp)
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    " INSERT INTO GON_TRANSPORTER(GT_ID,TRANSPORTER_NAME,CONTACT_PERSON,PHONE,RESPONSIBLE_PERSON,MOBILE,FAX,ADDRESS,NPWP,EMAIL,POSTAL_CODE,DESCRIPTION,BIRTHDATE,CREATE_DATE,CREATE_BY) " & vbCrLf & _
                    " VALUES(@GT_ID,@TRANSPORTER_NAME,@CONTACT_PERSON,@PHONE,@RESPONSIBLE_PERSON,@MOBILE,@FAX,@ADDRESS,@NPWP,@EMAIL,@POSTAL_CODE,@DESCRIPTION,@BIRTHDATE,@CREATE_DATE,@CREATE_BY) ;"
                ElseIf Mode = Helper.SaveMode.Update Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                         " UPDATE GON_TRANSPORTER SET TRANSPORTER_NAME = @TRANSPORTER_NAME,CONTACT_PERSON = @CONTACT_PERSON,PHONE = @PHONE,RESPONSIBLE_PERSON = @RESPONSIBLE_PERSON," & vbCrLf & _
                         " MOBILE = @MOBILE,FAX = @FAX,ADDRESS = @ADDRESS,NPWP = @NPWP,EMAIL = @EMAIL,POSTAL_CODE = @POSTAL_CODE,DESCRIPTION = @DESCRIPTION,BIRTHDATE = @BIRTHDATE,MODIFY_DATE = @MODIFY_DATE," & vbCrLf & _
                         " MODIFY_BY = @MODIFY_BY WHERE GT_ID = @GT_ID ;"
                    gtID = OTrans.CodeApp
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql(CommandType.Text, Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                If Mode = Helper.SaveMode.Insert Then
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, OTrans.CreatedBy, 100)
                    Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, OTrans.CreatedDate)
                ElseIf Mode = Helper.SaveMode.Update Then
                    Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, OTrans.ModifiedBy, 50)
                    Me.AddParameter("MODIFY_DATE", SqlDbType.SmallDateTime, OTrans.ModifiedDate)
                End If
                Me.AddParameter("@GT_ID", SqlDbType.VarChar, gtID, 16)
                Me.AddParameter("@TRANSPORTER_NAME", SqlDbType.VarChar, OTrans.NameApp, 100)
                Me.AddParameter("@CONTACT_PERSON", SqlDbType.VarChar, OTrans.ContactPerson, 100)
                Me.AddParameter("@PHONE", SqlDbType.VarChar, OTrans.Phone, 20)
                Me.AddParameter("@RESPONSIBLE_PERSON", SqlDbType.VarChar, OTrans.ResponsiblePerson, 100)
                Me.AddParameter("@MOBILE", SqlDbType.VarChar, OTrans.Mobile, 20)
                Me.AddParameter("@FAX", SqlDbType.VarChar, OTrans.FAX, 100)
                Me.AddParameter("@ADDRESS", SqlDbType.VarChar, OTrans.Address, 150)
                Me.AddParameter("@NPWP", SqlDbType.VarChar, OTrans.NPWP, 100)
                Me.AddParameter("@EMAIL", SqlDbType.VarChar, OTrans.Email, 100)
                Me.AddParameter("@POSTAL_CODE", SqlDbType.VarChar, OTrans.PostalCode, 5)
                Me.AddParameter("@DESCRIPTION", SqlDbType.VarChar, DBNull.Value, 100)
                If IsDBNull(OTrans.BirthDate) Or IsNothing(OTrans.BirthDate) Then
                    Me.AddParameter("@BIRTHDATE", SqlDbType.SmallDateTime, DBNull.Value, 100)
                Else
                    Me.AddParameter("@BIRTHDATE", SqlDbType.SmallDateTime, OTrans.BirthDate, 100)
                End If

                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If shouldReloadData Then
                    tblData = Me.GetDatatable(mustCloseConnection)
                End If

            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
    End Class
End Namespace

