Imports Nufarm.Domain
Imports NufarmBussinesRules.common
Public Class Transporter
    Private grpEditHeight As Integer = 216
    Private Mode As Helper.SaveMode = Helper.SaveMode.None
    Private OtransPorter As New NuFarm.Domain.Transporter()
    Private Ntransporter As New NuFarm.Domain.Transporter()
    Private clsTrans As New NufarmBussinesRules.OrderAcceptance.Transporter()
    Private IsLoadingRow As Boolean = False
    Private dtTable As DataTable = Nothing
    Private Sub AddNewItem()
        Me.GridEX1.BringToFront() : Me.SetNewData()
        Me.SetNewTransPorter() : Me.txtTransporterName.Focus()
        Me.grpEdit.Height = Me.grpEditHeight
        Me.btnAddNew.Text = "Cancel"
        Me.btnAddNew.Image = Me.ImageList1.Images(20)
    End Sub
    Private Sub CancelAdd()
        Me.grpEdit.Height = 0
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.Mode = Helper.SaveMode.Insert
        Me.ClearControl(Me.grpEdit)
        Me.btnAddNew.Text = "Add New"
        Me.btnAddNew.Image = Me.ImageList1.Images(1)
    End Sub
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Me.Cursor = Cursors.WaitCursor
        Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
        Try
            Select Case item.Name
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog()
                    'Case "btnCancel"
                Case "btnShowFieldChooser"
                    Me.GridEX1.ShowFieldChooser(Me)
                Case "btnAddNew"
                    If Me.btnAddNew.Text = "Add New" Then : Me.AddNewItem()
                    ElseIf Me.btnAddNew.Text = "Cancel" Then : Me.CancelAdd()
                    End If
                Case "btnRefresh"
                    Me.LoadData(False)
                Case "btnFieldChooser"
                    Me.GridEX1.ShowFieldChooser(Me, "drag column header here")
                Case "btnExport"
                    Me.SaveFileDialog1.Title = "Define the location File"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.GridEX1
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                        Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    End If
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                Case "btnPageSettings"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
                Case "btnSave"
                    Me.SaveData()
            End Select
        Catch ex As Exception
            Me.IsLoadingRow = False
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ItemPanel1_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function isValidData()
        If Me.txtTransporterName.Text = String.Empty Then
            Me.baseTooltip.Show("Please Enter transportername", Me.txtTransporterName, 2500) : Me.txtTransporterName.Focus() : Return False
        ElseIf Me.txtAddress.Text = "" Then
            Me.baseTooltip.Show("Please Enter address", Me.txtAddress, 2500) : Me.txtAddress.Focus() : Return False
        ElseIf Me.txtContactPerson.Text = "" Then
            Me.baseTooltip.Show("Please Enter contact person", Me.txtContactPerson, 2500) : Me.txtContactPerson.Focus() : Return False
        ElseIf Me.txtContactMobile.Text = "" Then
            Me.baseTooltip.Show("Please Enter contact mobile", Me.txtContactMobile, 2500) : Me.txtContactMobile.Focus() : Return False
        ElseIf Me.txtContactMobile.Text.Length <= 10 Then
            Me.baseTooltip.Show("Please Enter contact mobile", Me.txtContactMobile, 2500) : Me.txtContactMobile.Focus() : Return False
        ElseIf Me.txtContactFax.Text <> "" Then
            If Me.txtContactFax.Text.Length <= 5 Then
                Me.baseTooltip.Show("Please Enter FAX", Me.txtContactFax, 2500) : Me.txtContactFax.Focus() : Return False
            End If
        ElseIf Me.txtContactPhone.Text <> "" Then
            If Me.txtContactPhone.Text.Length <= 5 Then
                Me.baseTooltip.Show("Please Enter contact phone", Me.txtContactPhone, 2500) : Me.txtContactPhone.Focus() : Return False
            End If
        End If
        Return True
    End Function
    Private Sub LoadData(ByVal mustRelayout As Boolean)
        Me.dtTable = Me.clsTrans.GetDatatable(True)
        Me.GridEX1.SetDataBinding(dtTable.DefaultView(), "")
        Me.GridEX1.Show()
        If (mustRelayout) Then : Me.GridEX1.RetrieveStructure() : End If
        Me.FormatGrid()
    End Sub
    Private Sub SetNewTransPorter()
        Me.Ntransporter = New NuFarm.Domain.Transporter()
        Me.Ntransporter.BirthDate = Convert.ToDateTime(Me.dtPicBirtDate.Value.ToShortDateString())
        Me.Ntransporter.Address = Me.txtAddress.Text.TrimEnd().TrimStart()
        Me.Ntransporter.CodeApp = Me.lblAutoGenerate.Text
        Me.Ntransporter.ContactPerson = Me.txtContactPerson.Text.TrimStart().TrimEnd()
        Me.Ntransporter.CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
        Me.Ntransporter.CreatedDate = NufarmBussinesRules.SharedClass.ServerDate
        Me.Ntransporter.Email = Me.txtEmail.Text.TrimEnd().TrimStart().ToLower()
        Me.Ntransporter.FAX = Me.txtContactFax.Text.TrimStart().TrimEnd()
        Me.Ntransporter.Mobile = Me.txtContactMobile.Text.TrimStart().TrimEnd()
        Me.Ntransporter.NameApp = Me.txtTransporterName.Text.TrimStart().TrimStart()
        Me.Ntransporter.NPWP = Me.txtNPWP.Text.TrimStart().TrimEnd()
        Me.Ntransporter.Phone = Me.txtContactPhone.Text.TrimStart().TrimEnd()
        Me.Ntransporter.PostalCode = Me.txtPostalCode.Text.TrimStart().TrimEnd()
        Me.Ntransporter.ResponsiblePerson = Me.txtResponsiblePerson.Text.TrimStart().TrimEnd()
        If Me.Mode = Helper.SaveMode.Update Then
            Me.Ntransporter.ModifiedBy = NufarmBussinesRules.User.UserLogin.UserName
            Me.Ntransporter.ModifiedDate = NufarmBussinesRules.SharedClass.ServerDate
            Me.Ntransporter.CodeApp = Me.lblAutoGenerate.Text
        End If
    End Sub
    Private Sub SetNewData()
        Me.IsLoadingRow = True
        Me.ClearControl(Me.grpEdit) : Me.Mode = Helper.SaveMode.Insert
        Me.grpEdit.Height = Me.grpEditHeight
        Me.lblAutoGenerate.Text = "<< Auto Generate >>" : Me.txtTransporterName.ResetText()
        Me.IsLoadingRow = False
    End Sub
    Private Sub SaveData()
        If Me.grpEdit.Height <> Me.grpEditHeight Then : Return : End If

        If Me.HasChangedObject() Then
            If Not Me.isValidData() Then : Return : End If
            Me.SetNewTransPorter()
            If Me.clsTrans Is Nothing Then
                Me.clsTrans = New NufarmBussinesRules.OrderAcceptance.Transporter()
            End If
            Me.clsTrans.SaveData(Ntransporter, Me.Mode, True, Me.dtTable, True)
            Me.IsLoadingRow = True
            If IsNothing(Me.GridEX1.DataSource) Then
                Me.GridEX1.SetDataBinding(dtTable.DefaultView(), "")
                Me.GridEX1.RetrieveStructure()
            Else
                Me.GridEX1.SetDataBinding(dtTable.DefaultView(), "")
            End If
            Me.FormatGrid()

            Me.OtransPorter = Me.Ntransporter : Me.Ntransporter = New NuFarm.Domain.Transporter()
            Me.SetNewData()
        End If
        Me.IsLoadingRow = False
    End Sub
    Private Function HasChangedObject()
        If Me.txtAddress.Text.TrimEnd().TrimStart() <> Me.OtransPorter.Address Then : Return True : End If
        If Me.txtContactFax.Text.TrimEnd().TrimStart() <> Me.OtransPorter.FAX Then : Return True : End If
        If Me.txtContactMobile.Text.TrimEnd().TrimStart() <> Me.OtransPorter.Mobile Then : Return True : End If
        If Me.txtContactPerson.Text.TrimEnd().TrimStart() < Me.OtransPorter.ContactPerson Then : Return True : End If
        If Me.txtContactPhone.Text.TrimStart().TrimEnd() <> Me.OtransPorter.Phone Then : Return True : End If
        If Me.txtEmail.Text.TrimEnd().TrimStart() <> Me.OtransPorter.Email Then : Return True : End If
        If Me.txtNPWP.Text.TrimEnd().TrimStart() <> Me.OtransPorter.NPWP Then : Return True : End If
        If Me.txtPostalCode.Text.TrimEnd().TrimStart() <> Me.OtransPorter.PostalCode Then : Return True : End If
        If Me.txtResponsiblePerson.Text.TrimEnd().TrimStart() <> Me.OtransPorter.ResponsiblePerson Then : Return True : End If
        If Me.txtTransporterName.Text.TrimEnd().TrimStart() <> Me.OtransPorter.NameApp Then : Return True : End If
        If Me.dtPicBirtDate.Text <> "" Then
            If Me.dtPicBirtDate.Value.ToShortDateString() <> Convert.ToDateTime(Me.OtransPorter.BirthDate).ToShortDateString() Then : Return True : End If
        End If
        Return False
    End Function
    Private Sub FormatGrid()
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If col.DataMember = "CREATE_BY" Or col.DataMember = "MODIFY_BY" Or col.DataMember = "MODIFY_DATE" Or col.DataMember = "GT_ID" Then
                col.Visible = False
            End If
            If col.Type Is Type.GetType("System.Decimal") Or col.Type Is Type.GetType("System.Int16") Or col.Type Is Type.GetType("System.Int32") Or _
            col.Type Is Type.GetType("System.Int64") Or col.Type Is Type.GetType("System.Double") Then
                If col.Type Is Type.GetType("System.Double") Or col.Type Is Type.GetType("System.Decimal") Then
                    col.FormatString = "#,##0.000" : col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                End If
                col.FormatString = ""
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf col.Type Is Type.GetType("System.String") Then
                col.FormatString = ""
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Empty
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf col.Type Is Type.GetType("System.DateTime") Then
                col.FormatString = "dd MMMM yyyy"
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            End If
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
        Next
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        Me.GridEX1.AutoSizeColumns()
        Me.GridEX1.RootTable.Caption = "GON TRANSPORTER DATA"
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.True
        Me.GridEX1.RootTable.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX1.RootTable.TableHeaderFormatStyle.ForeColor = Color.Maroon
        Me.GridEX1.RootTable.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
    End Sub
    Private Sub SetOTransOriginal()
        Me.OtransPorter = New NuFarm.Domain.Transporter()
        Me.OtransPorter.BirthDate = Convert.ToDateTime(Me.dtPicBirtDate.Value.ToShortDateString())
        Me.OtransPorter.Address = Me.txtAddress.Text
        Me.OtransPorter.CodeApp = Me.lblAutoGenerate.Text
        Me.OtransPorter.ContactPerson = Me.txtContactPerson.Text
        Me.OtransPorter.CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
        Me.OtransPorter.CreatedDate = NufarmBussinesRules.SharedClass.ServerDate
        Me.OtransPorter.Email = Me.txtEmail.Text
        Me.OtransPorter.FAX = Me.txtContactFax.Text
        Me.OtransPorter.Mobile = Me.txtContactMobile.Text
        Me.OtransPorter.NameApp = Me.txtTransporterName.Text
        Me.OtransPorter.NPWP = Me.txtNPWP.Text
        Me.OtransPorter.Phone = Me.txtContactPhone.Text
        Me.OtransPorter.PostalCode = Me.txtPostalCode.Text
        Me.OtransPorter.ResponsiblePerson = Me.txtResponsiblePerson.Text
  
    End Sub
    Private Sub Transporter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' load data
        Me.Cursor = Cursors.WaitCursor
        Me.grpEdit.Height = 0
        Try
            Me.dtPicBirtDate.Value = NufarmBussinesRules.SharedClass.ServerDate
            Me.IsLoadingRow = True
            Me.LoadData(True)
            Me.Mode = Helper.SaveMode.Insert
            Me.SetOTransOriginal()
            Me.baseTooltip.BackColor = Me.BackColor
            Me.baseTooltip.IsBalloon = True
            Me.baseTooltip.ShowAlways = False
            Me.baseTooltip.ToolTipIcon = ToolTipIcon.Info
            Me.baseTooltip.UseAnimation = True
            Me.baseTooltip.UseFading = True
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, "_Transporter_Load")
        End Try
        Me.IsLoadingRow = False
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub inflateData()
        If Not IsDBNull(Me.GridEX1.GetValue("GT_ID")) And Not IsNothing(Me.GridEX1.GetValue("GT_ID")) Then
            Me.lblAutoGenerate.Text = Me.GridEX1.GetValue("GT_ID").ToString()
        Else
            Me.lblAutoGenerate.Text = "<< AutoGenerate >>"
        End If
        'GT_ID,TRANSPORTER_NAME,ADDRESS,POSTAL_CODE ,PHONE,FAX,CONTACT_MOBILE AS MOBILE,CONTACT_PERSON,RESPONSIBLE_PERSON,NPWP,EMAIL,BIRTHDATE,DESCRIPTION,CREATE_DATE
        If Not IsNothing(Me.GridEX1.GetValue("TRANSPORTER_NAME")) And Not IsNothing(Me.GridEX1.GetValue("TRANSPORTER_NAME")) Then
            Me.txtTransporterName.Text = Me.GridEX1.GetValue("TRANSPORTER_NAME").ToString()
        Else
            Me.txtTransporterName.Text = Me.GridEX1.GetValue("TRANSPORTER_NAME").ToString()
        End If
        Me.txtAddress.Text = IIf(Not IsDBNull(Me.GridEX1.GetValue("ADDRESS")) And Not IsNothing(Me.GridEX1.GetValue("ADDRESS")), Me.GridEX1.GetValue("ADDRESS").ToString(), "")
        Me.txtPostalCode.Text = IIf(Not IsDBNull(Me.GridEX1.GetValue("POSTAL_CODE")) And Not IsNothing(Me.GridEX1.GetValue("POSTAL_CODE")), Me.GridEX1.GetValue("POSTAL_CODE").ToString(), "")
        Me.txtContactPhone.Text = IIf(Not IsDBNull(Me.GridEX1.GetValue("PHONE")) And Not IsNothing(Me.GridEX1.GetValue("PHONE")), Me.GridEX1.GetValue("PHONE").ToString(), "")
        Me.txtContactFax.Text = IIf(Not IsDBNull(Me.GridEX1.GetValue("FAX")) And Not IsNothing(Me.GridEX1.GetValue("FAX")), Me.GridEX1.GetValue("FAX").ToString(), "")
        Me.txtContactMobile.Text = IIf(Not IsDBNull(Me.GridEX1.GetValue("MOBILE")) And Not IsNothing(Me.GridEX1.GetValue("MOBILE")), Me.GridEX1.GetValue("MOBILE").ToString(), "")
        Me.txtContactPerson.Text = IIf(Not IsDBNull(Me.GridEX1.GetValue("CONTACT_PERSON")) And Not IsNothing(Me.GridEX1.GetValue("CONTACT_PERSON")), Me.GridEX1.GetValue("CONTACT_PERSON").ToString(), "")
        Me.txtResponsiblePerson.Text = IIf(Not IsDBNull(Me.GridEX1.GetValue("RESPONSIBLE_PERSON")) And Not IsNothing(Me.GridEX1.GetValue("RESPONSIBLE_PERSON")), Me.GridEX1.GetValue("RESPONSIBLE_PERSON").ToString(), "")
        Me.txtNPWP.Text = IIf(Not IsDBNull(Me.GridEX1.GetValue("NPWP")) And Not IsNothing(Me.GridEX1.GetValue("NPWP")), Me.GridEX1.GetValue("NPWP").ToString(), "")
        Me.txtEmail.Text = IIf(Not IsDBNull(Me.GridEX1.GetValue("EMAIL")) And Not IsNothing(Me.GridEX1.GetValue("EMAIL")), Me.GridEX1.GetValue("EMAIL").ToString(), "")
        If Not IsDBNull(Me.GridEX1.GetValue("BIRTHDATE")) And Not IsNothing(Me.GridEX1.GetValue("BIRTHDATE")) Then
            Me.dtPicBirtDate.Value = Convert.ToDateTime(Me.GridEX1.GetValue("BIRTHDATE"))
            Me.dtPicBirtDate.Text = Convert.ToDateTime(Me.GridEX1.GetValue("BIRTHDATE")).ToShortDateString()
        Else
            Me.dtPicBirtDate.Text = "" ' NufarmBussinesRules.SharedClass.ServerDate().ToShortDateString()
            Me.dtPicBirtDate.Value = NufarmBussinesRules.SharedClass.ServerDate()
        End If

        'Me.dtPicBirtDate.Text = IIf(Not IsDBNull(Me.GridEX1.GetValue("BIRTHDATE")), Convert.ToDateTime(Me.GridEX1.GetValue("BIRTHDATE")).ToShortDateString(), Convert.ToDateTime().ToShortDateString())
        Me.OtransPorter = New NuFarm.Domain.Transporter()
        With Me.OtransPorter
            .Address = Me.txtAddress.Text
            .BirthDate = Me.dtPicBirtDate.Value.ToShortDateString()
            '.CodeApp = Me.lblAutoGenerate.Text
            .ContactPerson = Me.txtContactPerson.Text
            .Email = Me.txtEmail.Text
            .FAX = Me.txtContactFax.Text
            .GT_ID = Me.lblAutoGenerate.Text
            .Mobile = Me.txtContactMobile.Text
            .NameApp = Me.txtTransporterName.Text
            .NPWP = Me.txtNPWP.Text
            .Phone = Me.txtContactPhone.Text
            .PostalCode = Me.txtPostalCode.Text
            .ResponsiblePerson = Me.txtResponsiblePerson.Text
        End With
    End Sub

    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        ''inflate data
        Try
            If IsNothing(Me.GridEX1.DataSource) Then : Return : End If
            If IsNothing(Me.GridEX1.SelectedItems) Then : Return : End If
            If Me.IsLoadingRow Then : Return : End If
            If Me.GridEX1.GetRow().RowType <> Janus.Windows.GridEX.RowType.Record Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.grpEdit.Height = Me.grpEditHeight
            Me.IsLoadingRow = True
            Me.inflateData() : Me.Mode = Helper.SaveMode.Update
            Me.btnAddNew.Text = "Cancel"
            Me.btnAddNew.Image = Me.ImageList1.Images(20)
            Me.IsLoadingRow = False
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name & "_GridEX1_DoubleClick")
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
            e.Cancel = True : Return
        End If
        Try
            Dim GTID As String = Me.GridEX1.GetValue("GT_ID").ToString()
            Me.clsTrans.DeleteData(GTID, True)
            e.Cancel = False
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name & "_GridEX1_DeletingRecord")

        End Try
    End Sub

    Private Sub textBoxChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTransporterName.TextChanged, txtResponsiblePerson.TextChanged, txtNPWP.TextChanged, txtEmail.TextChanged, txtContactPhone.TextChanged, txtContactPerson.TextChanged, txtContactMobile.TextChanged, txtContactFax.TextChanged, txtAddress.TextChanged, grpEdit.TextChanged, txtPostalCode.TextChanged
        Me.btnSave.Enabled = Me.HasChangedObject()
    End Sub

    Private Sub dtPicBirtDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicBirtDate.TextChanged
        If Not Me.IsLoadingRow Then
            Me.btnSave.Enabled = Me.HasChangedObject()
        End If

    End Sub

    Private Sub TextBoxt_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTransporterName.KeyDown, txtResponsiblePerson.KeyDown, txtPostalCode.KeyDown, txtNPWP.KeyDown, txtEmail.KeyDown, txtContactPhone.KeyDown, txtContactPerson.KeyDown, txtContactMobile.KeyDown, txtContactFax.KeyDown, txtAddress.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                Me.SaveData()
            Catch ex As Exception
                Me.ShowMessageError(ex.Message)
                Me.LogMyEvent(ex.Message, Me.Name + "_TextBoxt_KeyDown")
            End Try
        End If
    End Sub

    Private Sub txtPostalCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPostalCode.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
        Else : e.Handled = True
        End If
    End Sub
End Class