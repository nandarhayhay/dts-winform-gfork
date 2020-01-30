Public Class Marketing_Program
#Region " Deklarasi "
    Friend Mode As ModeSave
    Private clsMrkt As NufarmBussinesRules.Program.ProgramRegistering
    Private Hasfilling As HasfillingGrid
    Public OriginalDate As Date = Nothing
    Public FirstLoad As Boolean
    Friend UpdateMode As ModeUpdate
    Friend ParentFrm As Program = Nothing
#End Region

#Region " Enum "
    Private Enum HasfillingGrid
        Has
        Hasnot
    End Enum
    Friend Enum ModeSave
        Save
        Update
    End Enum
    Friend Enum ModeUpdate
        FromGrid
        FromOriginal
    End Enum
#End Region

#Region " Sub "
    Private Sub RefreshData()
        Me.LoadData()
        Me.Hasfilling = HasfillingGrid.Hasnot
    End Sub
    Friend Sub InitializeData()
        Me.FirstLoad = True
        Me.Hasfilling = HasfillingGrid.Hasnot
        Me.LoadData()
    End Sub
    Private Sub LoadData()
        Me.Hasfilling = HasfillingGrid.Hasnot
        If Me.Mode = ModeSave.Update Then
            'Me.clsMrkt.CreateViewProgramRegistering("", Data.DataViewRowState.ModifiedOriginal)
            If Me.UpdateMode = ModeUpdate.FromOriginal Then
                Me.clsMrkt = New NufarmBussinesRules.Program.ProgramRegistering()
                Me.Size = New System.Drawing.Size(431, 216)
                If Me.clsMrkt.ProgramHasReferencedData(Me.txtProgramID.Text) Then
                    Me.UnabledControl()
                    'Me.ButtonEntry1.btnInsert.Enabled = False
                    Me.ButtonEntry1.btnInsert.Text = "&Update"
                    'Me.ButtonEntry1.btnAddNew.Enabled = False
                Else
                    Me.EnabledControl()
                    Me.txtProgramID.Enabled = False
                    Me.ButtonEntry1.btnInsert.Text = "&Update"
                    Me.ButtonEntry1.btnAddNew.Enabled = False
                End If
            Else
                Me.Size = New System.Drawing.Size(431, 531)
                Me.ClearControl(Me.UiGroupBox1)

                Me.UnabledControl()
                'Me.txtProgramID.Enabled = False
                Me.dtPicStart.Value = NufarmBussinesRules.SharedClass.ServerDate()
                Me.BindGrid()
                Me.ButtonEntry1.btnInsert.Text = "&Insert"
                Me.ButtonEntry1.btnInsert.Enabled = False
                Me.ButtonEntry1.btnAddNew.Enabled = True
                Me.ButtonEntry1.btnAddNew.Focus()
                'Me.ButtonEntry1.ButtonInsert.Text = "&Update"
            End If
        Else
            If Me.FirstLoad = True Then
                Me.clsMrkt = New NufarmBussinesRules.Program.ProgramRegistering()
                Me.clsMrkt.CreateViewProgramRegistering("", Data.DataViewRowState.CurrentRows)
            End If
            Me.BindGrid()
            Me.UnabledControl()
            Me.ButtonEntry1.btnAddNew.Enabled = True
            Me.ButtonEntry1.btnInsert.Enabled = False
            Me.ButtonEntry1.btnInsert.Text = "&Insert"
            Me.dtPicStart.Value = NufarmBussinesRules.SharedClass.ServerDate()
            'Me.txtProgramID.Enabled = True
            'Me.ButtonEntry1.ButtonInsert.Text = "&Insert"
        End If
    End Sub
    Private Sub BindGrid()
        Me.GridEX1.SetDataBinding(Me.clsMrkt.ViewProgramRegistering(), "")
        Me.GridEX1.RootTable.Columns("PROGRAM_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("PROGRAM_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("START_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarDropDown
        Me.GridEX1.RootTable.Columns("END_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarDropDown
    End Sub
    Private Sub UnabledControl()
        Me.txtProgramID.Enabled = False
        Me.txtProgramID.BackColor = Color.FromArgb(194, 217, 247)
        Me.txtProgramName.Enabled = False
        Me.txtProgramName.BackColor = Color.FromArgb(194, 217, 247)
        If Me.Mode = ModeSave.Update Then
            If Me.UpdateMode = ModeUpdate.FromOriginal Then
                If IsDate(Me.OriginalDate) Then
                    Me.dtPicEnd.MinDate = Me.OriginalDate
                End If
            End If
        End If
        Me.dtPicStart.ReadOnly = True
    End Sub
    Private Sub EnabledControl()
        Me.txtProgramID.Enabled = True
        Me.txtProgramName.Enabled = True
        Me.txtProgramName.BackColor = Color.White
        Me.txtProgramID.BackColor = Color.White
        Me.dtPicEnd.ReadOnly = False
        Me.dtPicStart.ReadOnly = False
    End Sub
#End Region

#Region " Function "
    Private Function IsValid() As Boolean
        If Me.txtProgramID.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.txtProgramID, "Program ID is Null !." & vbCrLf & "Please Suply Program ID.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtProgramID), Me.txtProgramID, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.txtProgramID, "Program ID is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.txtProgramID, "Program ID must not be null.")
            'Me.baseBallonSmall.ShowBalloon(Me.txtProgramID)
            Return False
        ElseIf Me.txtProgramName.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.txtProgramName, "Program name is Null !." & vbCrLf & "Please Suply Program name.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtProgramName), Me.txtProgramName, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.txtProgramName, "Program Name is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.txtProgramName, "proram name must not be NULL !.")
            'Me.baseBallonSmall.ShowBalloon(Me.txtProgramName)
            Me.txtProgramName.Focus()
            Return False
        ElseIf Me.dtPicStart.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.dtPicStart, "Start_date is Null !." & vbCrLf & "Please Defined Start_date.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dtPicStart), Me.dtPicStart, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.dtPicStart, "START_DATE is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.dtPicStart, "START_DATE name must not be NULL !.")
            'Me.baseBallonSmall.ShowBalloon(Me.dtPicStart)
            Me.dtPicStart.Focus()
            Return False
        ElseIf Me.dtPicEnd.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.dtPicEnd, "End_date is Null !." & vbCrLf & "Please Defined Start_date.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dtPicEnd), Me.dtPicEnd, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.dtPicEnd, "START_DATE is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.dtPicEnd, "START_DATE name must not be NULL !.")
            'Me.baseBallonSmall.ShowBalloon(Me.dtPicEnd)
            Me.dtPicEnd.Focus()
            Return False
        End If
        Return True
    End Function
#End Region

#Region " Event "
    Private Sub Marketing_Program_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.AcceptButton = Me.ButtonEntry1.btnInsert
            Me.CancelButton = Me.ButtonEntry1.btnClose
            If Me.Mode = ModeSave.Save Then
                Me.txtProgramID.Enabled = True
            End If
        Catch ex As Exception

        Finally
            Me.Hasfilling = HasfillingGrid.Has
            Me.FirstLoad = False
        End Try

    End Sub

    Private Sub btnChekExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChekExisting.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.txtProgramID.Text = "" Then
                Me.ShowMessageInfo("Please define the Program ID !.")
                Return
            End If
            If Me.clsMrkt.PROGRAMD_ID_HASEXISTED(Me.txtProgramID.Text) = True Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
            Else
                Me.ShowMessageInfo(Me.MessageDataSaveToAdd)
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnChekExisting_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            If Me.Hasfilling = HasfillingGrid.Hasnot Then
                Return
            End If
            If Me.GridEX1.Row <= -1 Then
                Return
            End If
            If Me.GridEX1.RecordCount <= 0 Then
                Return
            End If
            Me.Mode = ModeSave.Update
            Me.UpdateMode = ModeUpdate.FromGrid
            If Me.clsMrkt.ProgramHasReferencedData(Me.GridEX1.GetValue("PROGRAM_ID")) = True Then
                Me.UnabledControl()
                Me.ButtonEntry1.btnInsert.Text = "&Insert"
                Me.ButtonEntry1.btnInsert.Enabled = False
                Me.ButtonEntry1.btnAddNew.Enabled = True
                Me.ButtonEntry1.btnAddNew.Focus()
            Else
                Me.EnabledControl()
                Me.txtProgramID.Enabled = False
                Me.ButtonEntry1.btnInsert.Text = "&Update"
                Me.ButtonEntry1.btnInsert.Enabled = True
            End If
            Me.txtProgramID.Text = Me.GridEX1.GetValue("PROGRAM_ID").ToString()
            Me.ButtonEntry1.btnAddNew.Enabled = True
            Me.txtProgramName.Text = Me.GridEX1.GetValue("PROGRAM_NAME")
            Me.OriginalDate = Convert.ToDateTime(Me.GridEX1.GetValue("START_DATE"))
            Me.dtPicStart.Value = Me.OriginalDate
            Me.dtPicEnd.Value = Convert.ToDateTime(Me.GridEX1.GetValue("END_DATE"))
        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
            If Me.clsMrkt.ProgramHasReferencedData(Me.GridEX1.GetValue("PROGRAM_ID")) = True Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                e.Cancel = True
            ElseIf Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            Else
                Me.clsMrkt.DeleteProgram(Me.GridEX1.GetValue("PROGRAM_ID"))
                e.Cancel = False
                Me.GridEX1.UpdateData()
            End If
        End If
    End Sub
    Private Sub txtProgramID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProgramID.TextChanged
        Try
            If Me.Mode = ModeSave.Save Then
                If Me.txtProgramID.Text <> "" Then
                    Me.EnabledControl()
                    Me.ButtonEntry1.btnAddNew.Enabled = True
                    Me.ButtonEntry1.btnInsert.Enabled = True
                    Me.ButtonEntry1.btnInsert.Text = "&Insert"
                    Me.dtPicStart.IsNullDate = True
                Else
                    Me.UnabledControl()
                    Me.ButtonEntry1.btnInsert.Enabled = False
                End If
                Me.txtProgramID.Enabled = True
            ElseIf Me.Mode = ModeSave.Update Then
                If Me.UpdateMode = ModeUpdate.FromOriginal Then
                    Me.clsMrkt = New NufarmBussinesRules.Program.ProgramRegistering()
                    Me.clsMrkt.GetdataRowByProgramID(Me.txtProgramID.Text)
                    Me.txtProgramName.Text = Me.clsMrkt.PROGRAM_NAME
                    Me.Hasfilling = HasfillingGrid.Hasnot
                    'Me.OriginalDate = Convert.ToDateTime(Me.clsMrkt.PROGRAM_START_DATE)
                    Me.dtPicStart.Value = Convert.ToDateTime(Me.clsMrkt.PROGRAM_START_DATE)
                    Me.dtPicEnd.Value = Convert.ToDateTime(Me.clsMrkt.PROGRAM_END_DATE)
                    Me.dtPicEnd.MinDate = Me.dtPicStart.Value
                    Me.dtPicStart.MaxDate = Me.dtPicEnd.Value
                    Me.Size = New System.Drawing.Size(431, 216)
                    If Me.clsMrkt.ProgramHasReferencedData(Me.txtProgramID.Text) = True Then
                        Me.UnabledControl()
                        'Me.ButtonEntry1.btnInsert.Enabled = False
                        Me.ButtonEntry1.btnInsert.Text = "&Update"
                    Else
                        Me.EnabledControl()
                        Me.ButtonEntry1.btnInsert.Text = "&Update"
                    End If
                    Me.txtProgramID.Enabled = False
                End If
                Me.ButtonEntry1.btnAddNew.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ButtonEntry1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEntry1.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                Case "btnAddNew"
                    Me.ClearControl(Me.UiGroupBox1)
                    Me.UnabledControl()
                    Me.txtProgramID.Enabled = True
                    Me.ButtonEntry1.btnInsert.Text = "&Insert"
                    'Me.dtPicStart.Value = NufarmBussinesRules.SharedClass.ServerDate()
                    'Me.dtPicEnd.Value = Convert.ToDateTime(DateSerial(Me.dtPicStart.Value.Year, Me.dtPicStart.Value.Month + 1, Me.dtPicStart.Value.Day))
                    Me.txtProgramID.Focus()
                    Me.Mode = ModeSave.Save
                Case "btnInsert"
                    If Me.IsValid = False Then
                        Return
                    End If
                    Me.clsMrkt.PROGAM_ID = Me.txtProgramID.Text
                    Me.clsMrkt.PROGRAM_NAME = Me.txtProgramName.Text
                    Me.clsMrkt.PROGRAM_END_DATE = Convert.ToDateTime(Me.dtPicEnd.Value.ToShortDateString())
                    Me.clsMrkt.PROGRAM_START_DATE = Convert.ToDateTime(Me.dtPicStart.Value.ToShortDateString())
                    Me.Hasfilling = HasfillingGrid.Hasnot
                    Select Case Me.Mode
                        Case ModeSave.Save
                            'If Me.ShowConfirmedMessage(Me.MessageInsertData) = Windows.Forms.DialogResult.No Then
                            '    Me.Hasfilling = HasfillingGrid.Has
                            '    Return
                            'End If
                            If Me.clsMrkt.PROGRAMD_ID_HASEXISTED(Me.txtProgramID.Text) = True Then
                                'Me.baseTooltip = New System.Windows.Forms.ToolTip()
                                Me.baseTooltip.SetToolTip(Me.txtProgramID, "Program ID has existed in database." & vbCrLf & "Please suply with anothe one.")
                                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtProgramID), Me.txtProgramID, 2000)
                                'Me.baseTooltip.Active = False
                                'Me.baseBallonSmall.SetBalloonCaption(Me.txtProgramID, "PROGRAM_ID has Existed !.")
                                'Me.baseBallonSmall.SetBalloonText(Me.txtProgramID, "Program ID has existed in database !.")
                                'Me.baseBallonSmall.ShowBalloon(Me.txtProgramID)
                                Me.txtProgramID.Focus()
                                Me.txtProgramID.SelectionStart = 0
                                Me.txtProgramID.SelectionLength = Me.txtProgramID.Text.Length
                                Me.Hasfilling = HasfillingGrid.Has
                                Return
                            End If
                            If Me.clsMrkt.SaveProgram("Save") > 0 Then
                                Me.ShowMessageInfo(Me.MessageSavingSucces)
                            Else
                                Me.ShowMessageInfo(Me.MessageSavingFailed)
                            End If
                        Case ModeSave.Update
                            'If Me.ShowConfirmedMessage(Me.MessageUpdateData) = Windows.Forms.DialogResult.No Then
                            '    Return
                            'End If
                            If Me.clsMrkt.SaveProgram("Update") > 0 Then
                                Me.ShowMessageInfo(Me.MessageSavingSucces & vbCrLf & "All references of BrandPack and Distributor's End date" & vbCrLf & "are updated too")
                                If Me.UpdateMode = ModeUpdate.FromOriginal Then
                                    Me.clsMrkt.Dispose(True)
                                    Me.Close() : Return
                                End If
                            Else
                                Me.ShowMessageInfo("DBConcurency Update 0 record !." & vbCrLf & "Perhaps some user has changed the same data.")
                                'Me.RefreshData()
                                'Return
                            End If
                    End Select
                    Me.RefreshData()
                    ParentFrm.MustReloadData = True
                    'Me.UnabledControl()
                Case "btnClose"
                    Me.Close()
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ButtonEntry1_btnClick")
        Finally
            Me.Hasfilling = HasfillingGrid.Has
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Marketing_Program_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Not IsNothing(Me.clsMrkt) Then
                Me.clsMrkt.Dispose(False)
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtPicStart_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtPicStart.ValueChanged
        Try
            If Me.Hasfilling = HasfillingGrid.Hasnot Then
                Return
            End If
            If Me.dtPicStart.Text = "" Then
                Return
            End If
            If Not Me.dtPicStart.IsNullDate Then
                'If Me.Mode = ModeSave.Save Then
                '    If Me.dtPicStart.Value < NufarmBussinesRules.SharedClass.ServerDate() Then
                '        Me.dtPicStart.Value = NufarmBussinesRules.SharedClass.ServerDate
                '    End If
                'ElseIf Me.dtPicStart.Value < Me.OriginalDate Then
                '    Me.dtPicStart.Value = Me.OriginalDate
                'End If
                Me.dtPicEnd.Value = DateSerial(Me.dtPicStart.Value.Year, Me.dtPicStart.Value.Month + 1, Me.dtPicStart.Value.Day)
            Else
                Me.dtPicStart.ResetText()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtPicEnd_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtPicEnd.ValueChanged
        Try
            If Me.Hasfilling = HasfillingGrid.Hasnot Then
                Return
            End If
            If Me.dtPicEnd.Text = "" Then
                Return
            End If
            If Me.dtPicStart.IsNullDate = True Then
                Me.dtPicEnd.ResetText()
                Return
            Else
                If DateDiff(DateInterval.Day, Me.dtPicStart.Value.Date, Me.dtPicEnd.Value.Date) <= 1 Then
                    Me.dtPicEnd.Value = DateSerial(Me.dtPicStart.Value.Year, Me.dtPicStart.Value.Month + 1, Me.dtPicStart.Value.Day)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region
End Class
