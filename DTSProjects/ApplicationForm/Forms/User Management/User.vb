Public Class User

#Region " Deklarasi "
    Private clsUser As NufarmBussinesRules.User.UserManagemenet
    Private SaveType As Mode
    Private Const nTambah As Integer = 57
    Private Shadows TickCount As Integer = 0
    Friend CMain As Main = Nothing
#End Region

#Region " Enum "
    Private Enum Mode
        Save
        Update
    End Enum
#End Region
    Public Sub InitializeData()
        Me.LoadData()
    End Sub
    Protected Function Deskrip(ByVal cInput As String) As String
        Dim nHitung As Integer = 0
        Dim cString As String = ""
        Dim cOutput As String = ""
        cInput = Trim(cInput)
        Dim ncInput As String = StrReverse(cInput)

        For nHitung = 1 To ncInput.Length
            cString = ncInput.Substring(nHitung - 1, 1)
            cOutput += Chr((Asc(cString) + nHitung) - nTambah)
        Next

        Return cOutput

    End Function
    Private Function Enkrip(ByVal cInput As String) As String
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
    Private Sub InflateData()
        Me.txtPassword.Text = Me.Deskrip(Me.GridEX2.GetValue("PASSWORD").ToString())
        Me.txtUserName.Text = Me.GridEX2.GetValue("USER_ID").ToString()
        Me.clsUser.CreateViewDSPrivilege(Me.txtUserName.Text)
        Me.clsUser.CreateFormTable()
        Me.BindGrid(Me.GridEX1, Me.clsUser.ViewUser())
    End Sub
    Private Sub LoadData()
        If IsNothing(Me.clsUser) Then
            Me.clsUser = New NufarmBussinesRules.User.UserManagemenet()
        End If
        Me.clsUser.CreateViewUser()
        Me.BindGrid(Me.GridEX2, Me.clsUser.GetDataSet_1())
        Me.clsUser.CreateDSPrivilege()
        Me.clsUser.CreateFormTable()
        Me.BindGrid(Me.GridEX1, Me.clsUser.ViewUser())
        Me.txtPassword.Text = ""
        Me.txtUserName.Text = ""

    End Sub
    Private Sub BindGrid(ByVal Grid As Janus.Windows.GridEX.GridEX, ByVal ds As Object)
        Select Case Grid.Name
            Case "GridEX2"
                Grid.DataSource = ds
                Grid.DataMember = "LIST_USER"
                Grid.RetrieveStructure(True)
                Grid.RepeatHeaders = Janus.Windows.GridEX.InheritableBoolean.False
                Grid.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Grid.RootTable.Columns("USER_ID")))
                Grid.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                With Me.GridEX2.RootTable
                    .Columns("USER_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .Columns("PASSWORD").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    .Columns("LAST_LOGIN").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
                    .Columns("LAST_LOGIN").FormatString = "D"
                    .Columns("LAST_LOGOUT").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
                    .Columns("LAST_LOGOUT").FormatString = "D"
                    .Columns("LAST_FORM").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    '.Columns("LAST_FORM").FormatString = "D"
                    .Columns("STATUS").FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox

                    .Columns("USER_ID").EditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    .Columns("PASSWORD").EditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    .Columns("LAST_LOGIN").EditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    .Columns("LAST_LOGIN").FormatString = "D"
                    .Columns("LAST_LOGOUT").EditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    .Columns("LAST_LOGOUT").FormatString = "D"
                    .Columns("LAST_FORM").EditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    '.Columns("LAST_FORM").FormatString = "D"
                    .Columns("STATUS").FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit

                    .Columns("STATUS").Caption = "RUNNING"
                    .Columns("ISADMIN").Visible = True

                End With
                For Each col As Janus.Windows.GridEX.GridEXColumn In Grid.RootTable.Columns
                    col.AutoSize()
                Next


                'For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                '    'If Item.Type Is Type.GetType("System.String") Then
                '    '    Me.GridEX2.RootTable.Columns(Item).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                '    'ElseIf Item.Type Is Type.GetType("System.Int16") Then
                '    '    Me.GridEX2.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                '    'ElseIf Item.Type Is Type.GetType("System.Int32") Then
                '    '    Me.GridEX2.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                '    'ElseIf Item.Type Is Type.GetType("System.Int64") Then
                '    '    Me.GridEX2.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                '    'ElseIf Item.Type Is Type.GetType("System.Decimal") Then
                '    '    Me.GridEX2.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                '    'ElseIf Item.Type Is Type.GetType("System.Boolean") Then
                '    '    Me.GridEX2.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
                '    'ElseIf Item.Type Is Type.GetType("System.String") Then
                '    '    Me.GridEX2.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                '    'ElseIf Item.Type Is Type.GetType("System.DateTime") Then
                '    '    Me.GridEX2.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
                '    'End If
                'Next
                Grid.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Grid.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                Grid.RootTable.ChildTables(0).Columns("USER_ID").Visible = False
            Case "GridEX1"
                Grid.SetDataBinding(ds, "")
                Me.FilldDropDownList()
        End Select
    End Sub
    Private Sub FilldDropDownList()
        Me.GridEX1.DropDowns(0).SetDataBinding(Me.clsUser.TableForm().DefaultView(), Me.clsUser.TableForm.TableName)
    End Sub
    Private Sub UserName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserName.Click
        Try
            Me.txtUserName.Visible = Not Me.txtUserName.Visible
            If Me.txtUserName.Visible = True Then
                Me.UserName.HelpString = "Type UserName here"
            Else
                Me.UserName.HelpString = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub PassWord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PassWord.Click
        Try
            Me.txtPassword.Visible = Not Me.txtPassword.Visible
            If Me.txtPassword.Visible = True Then
                Me.PassWord.HelpString = "Type UserPassword here"
            Else
                Me.PassWord.HelpString = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub User_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.UserName.HelpString = ""
            Me.PassWord.HelpString = ""
            Me.SaveType = Mode.Save
            Me.baseTooltip.ToolTipTitle = "Information"
            AddHandler Me.Timer1.Tick, AddressOf Me.AutoRefresh
        Catch ex As Exception
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_User_Load")
        Finally
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub AutoRefresh(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.TickCount = 10 Then
            Me.Timer1.Enabled = False
            Me.Timer1.Stop()
            Me.LoadData()
            Me.BindGrid(Me.GridEX1, Me.clsUser.ViewUser())
            Me.TickCount = 0
            Me.Timer1.Enabled = True
            Me.Timer1.Start()
        End If
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Try
            Me.clsUser.CreateDSPrivilege()
            Me.clsUser.CreateFormTable()
            Me.BindGrid(Me.GridEX1, Me.clsUser.ViewUser())
            Me.txtPassword.Text = ""
            Me.txtUserName.Text = ""
            Me.SaveType = Mode.Save
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnAddNew_Click")
        End Try
    End Sub

    Private Sub SavingChanges1_btnCloseClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavingChanges1.btnCloseClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsUser) Then
                If Me.clsUser.GetDataset().HasChanges() Then
                    If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                        If Me.txtUserName.Text = "" Then
                            Me.baseTooltip.Show("Please Type UserName", Me.txtUserName, 2500)
                            Return
                        ElseIf Me.txtPassword.Text = "" Then
                            Me.baseTooltip.Show("Please Type Password", Me.txtPassword, 2500)
                            Return
                        ElseIf Me.GridEX2.RecordCount() <= 0 Then
                            Me.baseTooltip.Show("Privilege is null" & vbCrLf & "Privilege user must be assigned", Me.GridEX2, 2500)
                            Return
                        End If
                        Me.clsUser.USER_ID = Me.txtUserName.Text
                        Me.clsUser.Password = Me.txtPassword.Text
                        Select Case SaveType
                            Case Mode.Save
                                If Me.clsUser.GetDataset().HasChanges() = True Then
                                    Me.clsUser.SaveUser("Save", Me.clsUser.GetDataset().Tables(0).GetChanges(), True)
                                    Me.ShowMessageInfo(Me.MessageSavingSucces)
                                End If
                            Case Mode.Update
                                If Me.clsUser.GetDataset().HasChanges() = True Then
                                    Me.clsUser.SaveUser("Update", Me.clsUser.GetDataset().Tables(0).GetChanges(), True)
                                    Me.ShowMessageInfo(Me.MessageSavingSucces)
                                End If
                        End Select
                        'Me.ShowMessageInfo(Me.MessageSavingSucces)
                    End If
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_SavingChanges1_btnCloseClick")
        Finally
            Try
                Me.Dispose(True)
                Me.clsUser.Dispose()
                Me.clsUser = Nothing
            Catch ex As Exception

            End Try
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SavingChanges1_btnSaveClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavingChanges1.btnSaveClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.txtUserName.Text = "" Then
                Me.baseTooltip.Show("Please Type UserName", Me.txtUserName, 2500)
                Return
            ElseIf Me.txtPassword.Text = "" Then
                Me.baseTooltip.Show("Please Type Password", Me.txtPassword, 2500)
                Return
            ElseIf Me.GridEX1.RecordCount() <= 0 Then
                Me.baseTooltip.Show("Privilege is null" & vbCrLf & "Privilege user must be assigned", Me.GridEX2, 2500)
                Return
            End If
            Me.clsUser.USER_ID = Me.txtUserName.Text
            Me.clsUser.Password = Me.txtPassword.Text

            Select Case SaveType
                Case Mode.Save
                    'if me.clsUser.is
                    If Me.clsUser.GetDataset().HasChanges() = True Then
                        Me.clsUser.SaveUser("Save", Me.clsUser.GetDataset().Tables(0).GetChanges(), True)
                    Else
                        Me.clsUser.SaveUser("Save", Nothing, False)
                    End If
                    Me.ShowMessageInfo(Me.MessageSavingSucces)
                Case Mode.Update
                    If Me.clsUser.GetDataset().HasChanges() = True Then
                        Me.clsUser.SaveUser("Update", Me.clsUser.GetDataset().Tables(0).GetChanges(), True)
                    Else
                        Me.clsUser.SaveUser("Update", Nothing, False)
                    End If
                    Me.ShowMessageInfo(Me.MessageSavingSucces)
            End Select
            Me.LoadData()
            'Me.BindGrid(Me.GridEX1, Me.clsUser.ViewUser())
            Me.txtUserName.Text = ""
            Me.txtPassword.Text = ""
            Me.GridEX1.SetDataBinding(Nothing, "")
            Me.SaveType = Mode.Save
            'Me.ShowMessageInfo(Me.MessageSavingSucces)
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_SavingChanges1_btnCloseClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX2_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX2.DeletingRecord
        Try
            If Not Me.GridEX2.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                e.Cancel = True
                Me.GridEX2.Refetch()
                Me.ShowMessageInfo("If you want to manage user privilege" & vbCrLf & "please double click row.")
                Return
            End If
            If Me.ShowConfirmedMessage("Are you sure you want to delete user & Privilege?" & vbCrLf & "All associated user priviledge will be deleted too") = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Me.GridEX2.Refetch()
                Return
            End If
            If CBool(e.Row.Cells("STATUS").Value) = True Then
                Me.ShowMessageInfo("User is currently logging" & "System can not process request")
                e.Cancel = True
                Me.GridEX2.Refetch()
                Return
            End If
            If CBool(e.Row.Cells("ISADMIN").Value) = True Then
                Me.ShowMessageInfo("you want to delete admin ?" & "System can not process request")
                e.Cancel = True
                Me.GridEX2.Refetch()
                Return
            End If
            'if e.Row.Cells("USER_ID")
            Me.clsUser.DeleteUser(e.Row.Cells("USER_ID").Value.ToString())
            Me.LoadData()
            Me.ShowMessageInfo(Me.MessageSuccesDelete)
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX2_DeletingRecord")
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Not Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                e.Cancel = True
                Me.GridEX1.Refetch()
            End If
            If Me.txtUserName.Text = "" Then
            Else
                If Me.clsUser.IsLogin(Me.txtUserName.Text) = True Then
                    Me.ShowMessageInfo("User is currently still running")
                    e.Cancel = True
                    Return
                End If
            End If
            'If CBool(Me.GridEX1.GetValue("STATUS")) = True Then
            '    me.ShowMessageInfo("User is still using 
            'End If
            'If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage & vbCrLf & "All associated priviledge hold by user_id will be deleted ?") = Windows.Forms.DialogResult.No Then
            '    e.Cancel = True
            '    Me.GridEX1.Refetch()
            'End If
            'CHEK STATUS USER IN SERVER
            'if me.clsUser.IsLogin(

            '
            'If Me.SaveType = Mode.Update Then
            '    Dim dtView As DataView = CType(Me.GridEX1.DataSource, DataView)
            '    Dim Syst_ID As Object = Me.GridEX1.GetValue("SYST_ID")
            '    Me.clsUser.Delete_Priviledge(Me.txtUserName.Text, Me.GridEX1.GetValue("FORM_ID").ToString())
            '    dtView.Sort = "SYST_ID"
            '    Dim INDEX As Integer = dtView.Find(Syst_ID)
            '    If INDEX <> -1 Then
            '        dtView.Delete(INDEX)
            '        'dtView.Table.AcceptChanges()
            '        dtView.Table.DataSet.AcceptChanges()
            '        e.Cancel = False
            '    Else
            '        Me.ShowMessageInfo("Can not delete data")
            '        e.Cancel = True
            '        Me.GridEX1.Refetch()
            '        Return
            '    End If

            '    'IF ME.ModeSav
            '    'e.Cancel = False
            '    Me.GridEX1.UpdateData()
            '    'CType(Me.GridEX2.DataSource, DataSet).Tables(0).AcceptChanges()
            '    Me.ShowMessageInfo(Me.MessageSuccesDelete)
            '    dtView.Sort = "FORM_ID"
            '    'Me.grdPOBrandPack.Refetch()
            'Else
            '    e.Cancel = False
            '    Me.GridEX2.UpdateData()
            '    'Me.grdPOBrandPack.Refetch()
            'End If
            e.Cancel = False
            Me.GridEX2.UpdateData()
            'Me.LoadData()
            'Me.BindGrid(Me.GridEX1, Me.clsUser.ViewUser())
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        End Try
    End Sub

    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            'For Each Col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX2.RootTable.Columns
            '    If TypeOf (Me.GridEX1.GetValue()) Is DBNull Then
            '        Me.ShowMessageInfo("Some column has invalid / null data")
            '        Me.GridEX1.CancelCurrentEdit()
            '        Me.GridEX1.MoveToNewRecord()
            '    End If
            'Next
            If Me.txtUserName.Text = "" Then
                Me.ShowMessageInfo("Please Type UserName.")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.MoveToNewRecord()
                Return
            End If
            If Me.SaveType = Mode.Save Then
                If Me.clsUser.IsExisted(Me.txtUserName.Text) = True Then
                    Me.ShowMessageInfo("USER ID has existed in database.")
                    Me.GridEX1.CancelCurrentEdit()
                    Me.GridEX1.MoveToNewRecord()
                    Return
                End If
            End If
            Dim view As DataView = CType(Me.GridEX1.DataSource, DataView) 'Me.clsUser.GetDataset().Tables(0).DefaultView()
            'view.RowStateFilter = Data.DataViewRowState.CurrentRows
            'view.Sort = "FORM_ID"
            If view.Find(Me.GridEX1.GetValue(3)) <> -1 Then
                Me.ShowMessageInfo("Form ID has Existed")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.MoveToNewRecord()
                Return
            End If
            For i As Integer = 3 To Me.GridEX1.RootTable.Columns.Count - 1
                If Not Me.GridEX1.RootTable.Columns(i).Key = "Icon" Then
                    If TypeOf (Me.GridEX1.GetValue(i)) Is DBNull Then
                        Me.ShowMessageInfo("Some column has invalid / null data")
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.MoveToNewRecord()
                    End If
                End If
            Next
            Me.GridEX1.SetValue(1, Me.txtUserName.Text)
            Me.GridEX1.SetValue("SYST_ID", Me.txtUserName.Text & "" & Me.GridEX1.GetValue("FORM_ID").ToString())
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_AddingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkAutoRefresh_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoRefresh.CheckedChanged
        Try
            If Me.chkAutoRefresh.Checked = True Then
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
                Me.baseTooltip.Show("Every 10 seconds grid will be Refreshed.", Me.chkAutoRefresh, 2500)
            Else
                Me.Timer1.Enabled = False
                Me.Timer1.Stop()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.TickCount += 1
    End Sub

    'Private Sub GridEX2_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GridEX2.FormattingRow
    '    Try
    '        If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
    '            If e.Row.Table.Index = 0 Then
    '                'e.Row.Cells(0).ImageIndex = 8
    '                e.Row.Cells(0).Column.HeaderImageIndex = 10
    '                e.Row.Cells(0).Column.ColumnType = Janus.Windows.GridEX.ColumnType.Image
    '                e.Row.Cells(0).Column.ImageIndex = 8
    '                e.Row.Cells(0).Column.ButtonStyle = Janus.Windows.GridEX.ButtonStyle.Image
    '                e.Row.Cells(0).Column.ButtonImage = Me.ImageList1.Images(9)
    '            Else
    '                e.Row.Cells(0).Column.Selectable = False
    '                'e.Row.Cells(0).Column.HeaderImageIndex = 10
    '                'e.Row.Cells(0).Column.ColumnType = Janus.Windows.GridEX.ColumnType.Image
    '                'e.Row.Cells(0).Column.ImageIndex = 8
    '                'e.Row.Cells(0).Column.ButtonStyle = Janus.Windows.GridEX.ButtonStyle.Image
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub GridEX2_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridEX2.MouseDoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            'If Not Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
            '    Return
            'End If
            If (Me.GridEX2.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupHeader) Or (Me.GridEX2.GetRow().RowType = Janus.Windows.GridEX.RowType.GroupFooter) _
            Or (Me.GridEX2.GetRow().RowType = Janus.Windows.GridEX.RowType.FilterRow) Then
                If IsDBNull(Me.GridEX2.GetValue("USER_ID")) Then
                    Return
                End If
            End If

            'If Not e.Column.Key = "Icon" Then
            '    Return
            'End If
            Me.InflateData()
            Me.SaveType = Mode.Update
        Catch ex As Exception
            'Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX2_ColumnButtonClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        If e.KeyCode = Keys.F7 Then
            Try
                Dim FE As New Janus.Windows.GridEX.Export.GridEXExporter()
                Me.Cursor = Cursors.WaitCursor
                FE.IncludeChildTables = False
                FE.IncludeHeaders = True
                FE.SheetName = "USER PRIVILEDGE"
                FE.IncludeFormatStyle = False
                FE.IncludeExcelProcessingInstruction = True
                FE.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                Dim SD As New SaveFileDialog()
                SD.OverwritePrompt = True
                SD.DefaultExt = ".xls"
                SD.Filter = "All Files|*.*"
                SD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                If SD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Using FS As New System.IO.FileStream(SD.FileName, IO.FileMode.Create)
                        FE.GridEX = Me.GridEX1
                        FE.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & SD.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                End If
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.ShowMessageError(ex.Message)
                Cursor = Cursors.Default
            End Try
        End If
    End Sub

    Private Sub GridEX2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX2.KeyDown
        If e.KeyCode = Keys.F7 Then
            Try

                Dim FE As New Janus.Windows.GridEX.Export.GridEXExporter()
                Me.Cursor = Cursors.WaitCursor
                FE.IncludeChildTables = True
                FE.IncludeHeaders = True
                FE.SheetName = "USER PRIVILEDGE"
                FE.IncludeFormatStyle = False
                FE.IncludeExcelProcessingInstruction = True
                FE.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                Dim SD As New SaveFileDialog()
                SD.OverwritePrompt = True
                SD.DefaultExt = ".xls"
                SD.Filter = "All Files|*.*"
                SD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                If SD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Using FS As New System.IO.FileStream(SD.FileName, IO.FileMode.Create)
                        FE.GridEX = Me.GridEX2
                        FE.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & SD.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                End If
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.ShowMessageError(ex.Message)
                Cursor = Cursors.Default
            End Try
            
        End If
    End Sub

    Private Sub GridEX2_CellValueChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX2.CellValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If e.Column.Key = "INActive" Then
                Dim InActive As Boolean = CBool(Me.GridEX2.GetValue("INActive"))
                Me.clsUser.UpdateSystPriviledge(Me.GridEX2.GetValue("USER_ID"), InActive, True)
            ElseIf e.Column.Key = "ISADMIN" Then
                Dim IsAdmin As Boolean = CBool(Me.GridEX2.GetValue("ISADMIN"))
                Me.clsUser.UpdateSystPriviledge1(Me.GridEX2.GetValue("USER_ID"), IsAdmin, True)
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX2_CellValueChanged")
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
