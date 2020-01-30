Public Class Program_BrandPack
#Region " Deklarasi "
    Dim clsMRKT_BRANDPACK As NufarmBussinesRules.Program.BrandPackInclude
    Private HasLoad As Boolean
    Friend UM As UpdateMode
    Private SFC As StateFillingCombo
    Friend Mode As ModeSave
    Private SFG As StateFillingGrid
    Private OriginalDate As Date
    Friend PROG_BRANDPACK_ID As String
    Private ColItemExisted As Hashtable
    Private OriginalStartDateFromProgram As Date = Nothing
    Private OriginalEndDateFromProgram As Date = Nothing
    Friend OriginalStartDateFromBrandPack As Date = Nothing
    Friend OriginalEndDateFromBrandPack As Date = Nothing
    Friend ProgramID As String = ""
    Friend BRANDPACK_ID As String = ""
    Friend ParentFrm As Program = Nothing
#End Region

#Region " Enum "
    Friend Enum UpdateMode
        FromOriginal
        FromGrid
    End Enum
    Private Enum StateFillingCombo
        Filling
        HasFilled
    End Enum
    Friend Enum ModeSave
        Save
        Update
    End Enum
    Private Enum StateFillingGrid
        Filling
        HasFilled
    End Enum
#End Region

#Region " Function "
    Private Function IsValid() As Boolean
        Select Case Me.Mode
            Case ModeSave.Save
                If Not IsNothing(Me.mcbBrandPack.CheckedValues) Then
                    If Me.mcbBrandPack.CheckedValues.Length = 0 Then
                        Me.baseTooltip.SetToolTip(Me.mcbBrandPack, "BrandPack is Null !." & vbCrLf & "Please Suply BrandPack Name.")
                        Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2500)
                        'Me.baseBallonSmall.SetBalloonCaption(Me.mcbBrandPack, "BRANDPACK_ID is NULL !.")
                        'Me.baseBallonSmall.SetBalloonText(Me.mcbBrandPack, "BRANDPACK_ID Must not be NULL !.")
                        'Me.baseBallonSmall.ShowBalloon(Me.mcbBrandPack)
                        '    Me.mcbBrandPack.Focus()
                        '    Return False
                        'ElseIf Me.mcbBrandPack.Text.IndexOf(",") <> -1 Then
                        '    Dim BRANDPACK_NAME As String = Microsoft.VisualBasic.Mid(Me.mcbBrandPack.Text, 1, Me.mcbBrandPack.Text.IndexOf(","))
                        '    Me.clsMRKT_BRANDPACK.ViewBrandPack.Sort = "BRANDPACK_NAME"
                        '    If Me.clsMRKT_BRANDPACK.ViewBrandPack().Find(BRANDPACK_NAME) = -1 Then
                        '        Me.baseTooltip.SetToolTip(Me.mcbBrandPack, "BrandPack is Null !." & vbCrLf & "Please Suply BrandPack Name.")
                        '        Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2000)
                        '        'Me.baseBallonSmall.SetBalloonCaption(Me.mcbBrandPack, "BRANDPACK_ID is NULL !.")
                        '        'Me.baseBallonSmall.SetBalloonText(Me.mcbBrandPack, "BRANDPACK_ID Must not be NULL !.")
                        '        'Me.baseBallonSmall.ShowBalloon(Me.mcbBrandPack)
                        '        'Me.clsMRKT_BRANDPACK.ViewBrandPack().Sort = "BRANDPACK_ID"
                        '        Me.mcbBrandPack.Focus()
                        '        Return False
                        '    End If
                        '    Me.clsMRKT_BRANDPACK.ViewBrandPack().Sort = "BRANDPACK_ID"
                        'ElseIf Me.mcbBrandPack.Text.IndexOf(",") = -1 Then
                        '    Me.clsMRKT_BRANDPACK.ViewBrandPack().Sort = "BRANDPACK_NAME"
                        '    Dim BRANDPACK As String = Me.mcbBrandPack.Text
                        '    If Me.clsMRKT_BRANDPACK.ViewBrandPack().Find(BRANDPACK) = -1 Then
                        '        Me.baseTooltip.SetToolTip(Me.mcbBrandPack, "BrandPack is Null !." & vbCrLf & "Please Suply BrandPack Name.")
                        '        Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2000)
                        '        'Me.baseBallonSmall.SetBalloonCaption(Me.mcbBrandPack, "BRANDPACK_ID is NULL !.")
                        '        'Me.baseBallonSmall.SetBalloonText(Me.mcbBrandPack, "BRANDPACK_ID Must not be NULL !.")
                        '        'Me.baseBallonSmall.ShowBalloon(Me.mcbBrandPack)
                        '        'Me.clsMRKT_BRANDPACK.ViewBrandPack().Sort = "BRANDPACK_ID"
                        '        Me.mcbBrandPack.Focus()
                        '        Return False
                        '    End If
                        '    Me.clsMRKT_BRANDPACK.ViewBrandPack().Sort = "BRANDPACK_ID"
                    End If
                Else
                    Me.baseTooltip.SetToolTip(Me.mcbBrandPack, "BrandPack is Null !." & vbCrLf & "Please Suply BrandPack Name.")
                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbBrandPack), Me.mcbBrandPack, 2500)
                    'Me.baseBallonSmall.SetBalloonCaption(Me.mcbBrandPack, "BRANDPACK_ID is NULL !.")
                    'Me.baseBallonSmall.SetBalloonText(Me.mcbBrandPack, "BRANDPACK_ID Must not be NULL !.")
                    'Me.baseBallonSmall.ShowBalloon(Me.mcbBrandPack)
                    Me.mcbBrandPack.Focus()
                    Return False
                End If

            Case ModeSave.Update
                If Me.mcbOriginalBrandPack.Value Is Nothing Then
                    Me.baseTooltip.SetToolTip(Me.mcbOriginalBrandPack, "BrandPack is Null !." & vbCrLf & "Please Suply BrandPack Name.")
                    Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbOriginalBrandPack), Me.mcbOriginalBrandPack, 2000)
                    'Me.baseBallonSmall.SetBalloonCaption(Me.mcbOriginalBrandPack, "BRANDPACK_ID is NULL !.")
                    'Me.baseBallonSmall.SetBalloonText(Me.mcbOriginalBrandPack, "BRANDPACK_ID Must not be NULL !.")
                    'Me.baseBallonSmall.ShowBalloon(Me.mcbOriginalBrandPack)
                    Me.mcbOriginalBrandPack.Focus()
                    Return False
                End If
        End Select
        If Me.mcbProgram.Value Is Nothing Then
            Me.baseTooltip.SetToolTip(Me.mcbProgram, "Program is Null !." & vbCrLf & "Please Defined Program Name.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbProgram), Me.mcbProgram, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.mcbProgram, "PROGRAM_ID is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.mcbProgram, "PROGRAM_ID Must not be NULL !.")
            'Me.baseBallonSmall.ShowBalloon(Me.mcbProgram)
            Me.mcbProgram.Focus()
            Return False
            'ElseIf Me.mcbBrandPack.Values.Count = 0 Then
            '    Me.baseBallonSmall.SetBalloonCaption(Me.mcbBrandPack, "BRANDPACK_ID is NULL !.")
            '    Me.baseBallonSmall.SetBalloonText(Me.mcbBrandPack, "BRANDPACK_ID Must not be NULL !.")
            '    Me.baseBallonSmall.ShowBalloon(Me.mcbBrandPack)
        ElseIf Me.dtPicStart.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.dtPicStart, "Start_Date is Null !." & vbCrLf & "Please defined Start_Date.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dtPicStart), Me.dtPicStart, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.dtPicStart, "START_DATE is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.dtPicStart, "START_DATE Must not be NULL !.")
            'Me.baseBallonSmall.ShowBalloon(Me.dtPicStart)
            Me.dtPicStart.Focus()
            Return False
        ElseIf Me.dtPicEnd.Text = "" Then
            Me.baseTooltip.SetToolTip(Me.dtPicEnd, "End_Date is Null !." & vbCrLf & "Please Defined End_Date.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.dtPicEnd), Me.dtPicEnd, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.dtPicEnd, "END_DATE is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.dtPicEnd, "END_DATE Must not be NULL !.")
            'Me.baseBallonSmall.ShowBalloon(Me.dtPicEnd)
            Me.dtPicEnd.Focus()
            Return False
        End If
        Return True
    End Function
#End Region

#Region " Sub "
    Private Sub UnabledControl()
        'Me.mcbProgram.ReadOnly = True
        'Me.mcbBrandPack.ReadOnly = True
        'Me.mcbOriginalBrandPack.ReadOnly = True
        If (Me.Mode = ModeSave.Update) Then
            If Me.UM = UpdateMode.FromOriginal Then
                If IsDate(Me.OriginalEndDateFromBrandPack) Then
                    Me.dtPicEnd.MinDate = Me.OriginalEndDateFromBrandPack
                End If
            End If
        End If
        'Me.dtPicEnd.MinDate = Me.OriginalEndDateFromBrandPack
        Me.dtPicEnd.ReadOnly = True
        Me.dtPicStart.ReadOnly = True

    End Sub
    Private Sub EnabledControl()
        Me.mcbProgram.ReadOnly = False
        Me.mcbBrandPack.ReadOnly = False
        Me.dtPicStart.ReadOnly = False
        Me.dtPicEnd.ReadOnly = False
    End Sub
    Friend Sub InitializeData()
        Me.HasLoad = False
        Me.RefReshData()
    End Sub
    Private Sub ShowMCBOriginalBrandPack()
        Me.mcbOriginalBrandPack.BringToFront()
        Me.mcbOriginalBrandPack.Visible = True
        Me.mcbBrandPack.SendToBack()
        Me.mcbBrandPack.Visible = False
    End Sub
    Private Sub ShowMCBBrandPack()
        Me.mcbBrandPack.BringToFront()
        Me.mcbBrandPack.Visible = True
        Me.mcbOriginalBrandPack.SendToBack()
        Me.mcbOriginalBrandPack.Visible = False
    End Sub
    Private Sub RefReshData()
        Me.SFC = StateFillingCombo.Filling
        Me.SFG = StateFillingGrid.Filling
        If Me.Mode = ModeSave.Update Then
            If Me.UM = UpdateMode.FromOriginal Then
                Me.clsMRKT_BRANDPACK = New NufarmBussinesRules.Program.BrandPackInclude
                Me.Size = New System.Drawing.Size(499, 196)
                Me.clsMRKT_BRANDPACK.CreateViewProgram(NufarmBussinesRules.common.Helper.SaveMode.Update, False)
                Me.BindMultiColumnCombo(Me.mcbProgram, Me.clsMRKT_BRANDPACK.ViewProgram(), "")
                Me.clsMRKT_BRANDPACK.CreateViewBrandPack(Me.ProgramID, True, True)
                Me.BindMultiColumnCombo(Me.mcbOriginalBrandPack, Me.clsMRKT_BRANDPACK.ViewOriginalBrandPack(), "")
                If Me.clsMRKT_BRANDPACK.HasReferencedData(Me.PROG_BRANDPACK_ID) = True Then
                    Me.UnabledControl()
                    Me.ButtonEntry1.btnInsert.Enabled = False
                    Me.ButtonEntry1.btnInsert.Text = "&Update"
                Else
                    Me.EnabledControl()
                    Me.mcbProgram.ReadOnly = True
                    Me.mcbOriginalBrandPack.ReadOnly = True
                    Me.ButtonEntry1.btnInsert.Enabled = True
                    Me.ButtonEntry1.btnInsert.Text = "&Update"
                End If
                Me.ButtonEntry1.btnAddNew.Enabled = False
            Else
                Me.Size = New System.Drawing.Size(499, 641)
                Me.ClearControl(Me.UiGroupBox1)
                Me.mcbOriginalBrandPack.Text = ""
                Me.mcbProgram.Text = ""
                'Me.EnabledControl()
                Me.UnabledControl()
                'Me.dtPicStart.Value = NufarmBussinesRules.SharedClass.ServerDate()
                Me.BindGrid()
                Me.ButtonEntry1.btnInsert.Text = "&Insert"
                Me.ButtonEntry1.btnInsert.Enabled = False
                Me.ButtonEntry1.btnAddNew.Enabled = True
                Me.ButtonEntry1.btnAddNew.Focus()
                'Me.ShowMCBBrandPack()
            End If
            Me.ShowMCBOriginalBrandPack()
        Else
            If Me.HasLoad = False Then
                Me.clsMRKT_BRANDPACK = New NufarmBussinesRules.Program.BrandPackInclude()
                Me.clsMRKT_BRANDPACK.CreateViewProgram(NufarmBussinesRules.common.Helper.SaveMode.Insert, False)
                Me.BindMultiColumnCombo(Me.mcbProgram, Me.clsMRKT_BRANDPACK.ViewProgram(), "")
                If Me.ProgramID <> "" Then
                    Me.clsMRKT_BRANDPACK.CreateViewBrandPack(Me.ProgramID, False, True)
                End If
                Me.BindCheckCombo(Me.mcbBrandPack, Me.clsMRKT_BRANDPACK.ViewBrandPack(), "")
                Me.BindMultiColumnCombo(Me.mcbOriginalBrandPack, Me.clsMRKT_BRANDPACK.ViewOriginalBrandPack(), "")
                Me.clsMRKT_BRANDPACK.CreateViewMRKT_BRANDPACK("") 'buat dataview
                Me.mcbBrandPack.UncheckAll()
            End If
            Me.BindGrid()
            Me.UnabledControl()
            Me.mcbProgram.ReadOnly = False
            Me.ButtonEntry1.btnAddNew.Enabled = True
            Me.ButtonEntry1.btnInsert.Enabled = False
            Me.ButtonEntry1.btnInsert.Text = "&Insert"
            Me.ShowMCBBrandPack()
        End If
    End Sub
    Private Sub InflateData()
        Me.mcbProgram.Value = Me.GridEX1.GetValue("PROGRAM_ID")
        Me.mcbOriginalBrandPack.Value = Me.GridEX1.GetValue("BRANDPACK_ID")
        Me.dtPicStart.Value = Convert.ToDateTime(Me.GridEX1.GetValue("BRANDPACK_START_DATE"))
        Me.dtPicEnd.Value = Convert.ToDateTime(Me.GridEX1.GetValue("BRANDPACK_END_DATE"))
    End Sub
    Private Sub BindCheckCombo(ByVal Cbx As Janus.Windows.GridEX.EditControls.CheckedComboBox, ByVal ds As DataView, ByVal rowfilter As String)
        Me.SFC = StateFillingCombo.Filling : ds.RowFilter = rowfilter
        Cbx.SetDataBinding(ds, "")
        'Cbx.DropDownDataSource = ds : Cbx.RetrieveStructure()
        Cbx.UncheckAll() : Cbx.Text = ""
        If IsNothing(ds) Then
            Me.SFC = StateFillingCombo.HasFilled : Return
        End If
        Cbx.DropDownDisplayMember = "BRANDPACK_NAME" : Cbx.DropDownValueMember = "BRANDPACK_ID"
        Cbx.DropDownList().Columns("BRANDPACK_ID").UseHeaderSelector = True
        'For Each col As Janus.Windows.GridEX.GridEXColumn In Cbx.DropDownList.Columns
        '    If col.Visible Then : col.AutoSize() : End If
        'Next
        Me.SFC = StateFillingCombo.HasFilled
    End Sub
    Private Sub BindMultiColumnCombo(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal dsSource As DataView, ByVal rowFilter As String)
        Me.SFC = StateFillingCombo.Filling
        mcb.DataSource = dsSource : mcb.DropDownList.RetrieveStructure()
        mcb.Value = Nothing : mcb.Text = ""
        If IsNothing(dsSource) Then : Me.SFC = StateFillingCombo.HasFilled : Return : End If
        mcb.DroppedDown = True
        Select Case mcb.Name
            Case "mcbOriginalBrandPack"
                mcb.DisplayMember = "BRANDPACK_NAME" : mcb.ValueMember = "BRANDPACK_ID"
            Case "mcbProgram"
                mcb.DisplayMember = "PROGRAM_ID" : mcb.ValueMember = "PROGRAM_ID"
        End Select
        mcb.DroppedDown = True
        For Each col As Janus.Windows.GridEX.GridEXColumn In mcb.DropDownList.Columns
            If col.Visible Then : col.AutoSize() : End If
        Next
        mcb.DroppedDown = False
        Me.SFC = StateFillingCombo.HasFilled
    End Sub
    Private Sub BindGrid()
        Me.GridEX1.SetDataBinding(Me.clsMRKT_BRANDPACK.ViewMarketingBrandPack(), "")
        Me.GridEX1.RootTable.Columns("BRANDPACK_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("BRANDPACK_START_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarDropDown
        Me.GridEX1.RootTable.Columns("BRANDPACK_END_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarDropDown
    End Sub
#End Region

#Region " Event "

#Region " Event Form "
    Private Sub Program_BrandPack_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.AcceptButton = Me.ButtonEntry1.btnInsert
            Me.CancelButton = Me.ButtonEntry1.btnClose
            Me.lblItem.Text = ""
            'Me.mcbBrandPack.DropDownList().Columns("BRANDPACK_ID").ShowRowSelector = False
            If Me.ProgramID <> "" Then
                Me.mcbProgram.Value = Me.ProgramID
            End If
            If Me.BRANDPACK_ID <> "" Then
                Me.mcbOriginalBrandPack.Value = Me.BRANDPACK_ID
            End If
        Catch ex As Exception

        Finally
            Me.HasLoad = True
            Me.SFC = StateFillingCombo.HasFilled
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub Program_BrandPack_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Me.clsMRKT_BRANDPACK Is Nothing Then
            Else
                Me.clsMRKT_BRANDPACK.Dispose(False)
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region " Multicolumn Combo "
    Private Sub mcbProgram_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbProgram.ValueChanged
        Try
            If Me.SFC = StateFillingCombo.Filling Then : Return : End If
            If Me.clsMRKT_BRANDPACK Is Nothing Then : Me.clsMRKT_BRANDPACK = New NufarmBussinesRules.Program.BrandPackInclude() : End If
            If Me.Mode = ModeSave.Save Then
                If Me.HasLoad = False Then : If IsNothing(Me.mcbProgram.Value) Then : Return : End If
                ElseIf Me.mcbProgram.SelectedIndex = -1 Then

                    Me.UnabledControl() : Me.ButtonEntry1.btnInsert.Enabled = False
                    Me.mcbProgram.Enabled = True : Me.mcbProgram.Focus()
                    Me.SFC = StateFillingCombo.Filling
                    Me.mcbBrandPack.SetDataBinding(Nothing, "")
                    Me.SFC = StateFillingCombo.HasFilled
                    Me.dtPicStart.IsNullDate = True
                    Me.dtPicEnd.IsNullDate = True : Return
                End If
                Me.clsMRKT_BRANDPACK.GetDateFromProgram(Me.mcbProgram.Value.ToString())
                Me.OriginalStartDateFromProgram = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.START_DATE)
                Me.OriginalEndDateFromProgram = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
                Me.EnabledControl() : Me.dtPicStart.IsNullDate = False : Me.dtPicStart.Value = Me.OriginalStartDateFromProgram
                Me.dtPicEnd.IsNullDate = False : Me.dtPicEnd.Value = Me.OriginalEndDateFromProgram
                Me.ButtonEntry1.btnInsert.Enabled = True
            ElseIf Me.UM = UpdateMode.FromGrid Then
                If Me.mcbProgram.SelectedIndex = -1 Then : Return : End If
                Me.clsMRKT_BRANDPACK.GetDateFromProgram(Me.mcbProgram.Value.ToString())
                Me.OriginalStartDateFromProgram = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.START_DATE)
                Me.OriginalEndDateFromProgram = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
            Else
                Me.clsMRKT_BRANDPACK.GetDateFromProgram(Me.mcbProgram.Value.ToString())
                Me.OriginalStartDateFromProgram = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.START_DATE)
                Me.OriginalEndDateFromProgram = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
                Me.dtPicStart.IsNullDate = False : Me.dtPicStart.Value = Me.OriginalStartDateFromProgram
                Me.dtPicEnd.IsNullDate = False : Me.dtPicEnd.Value = Me.OriginalEndDateFromProgram
                Me.ButtonEntry1.Enabled = True
            End If
            If IsDate(Me.OriginalStartDateFromProgram) Then
                Me.dtPicStart.MinDate = Me.OriginalStartDateFromProgram
                Me.dtPicEnd.MinDate = Me.OriginalStartDateFromProgram
            End If
            If IsDate(Me.OriginalEndDateFromProgram) Then
                Me.dtPicStart.MaxDate = Me.OriginalEndDateFromProgram
                Me.dtPicEnd.MaxDate = Me.OriginalEndDateFromProgram
            End If
            Me.mcbBrandPack.UncheckAll() : Me.mcbBrandPack.Text = "" : Me.mcbOriginalBrandPack.Text = "" : Me.lblItem.Text = ""
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region " Button "
    Private Sub btnChekExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChekExisting.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.clsMRKT_BRANDPACK Is Nothing Then
                Me.clsMRKT_BRANDPACK = New NufarmBussinesRules.Program.BrandPackInclude()
            End If
            If Me.mcbProgram.Value Is Nothing Then
                Me.ShowMessageInfo("Please define the Program.")
                Return
            End If
            If Me.mcbBrandPack.Visible = True Then
                If Me.mcbBrandPack.CheckedValues Is Nothing Then
                    Me.ShowMessageInfo("Please define the BRANDPACK.")
                    Return
                End If
                If Me.mcbBrandPack.CheckedValues.Length = 0 Then
                    Me.ShowMessageInfo("Please define the BrandPack")
                End If
                If Me.mcbBrandPack.CheckedValues.Length > 0 Then
                    Me.ColItemExisted = New Hashtable()
                    Me.ColItemExisted.Clear()
                    For i As Integer = 0 To Me.mcbBrandPack.CheckedValues.Length - 1
                        Dim PROG_BRANDPACK_ID As String = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.CheckedValues().GetValue(i).ToString()
                        If Me.clsMRKT_BRANDPACK.IsExisted(PROG_BRANDPACK_ID) = True Then
                            Me.ColItemExisted.Add(i, Me.mcbBrandPack.CheckedValues().GetValue(i).ToString())
                        End If
                    Next
                    If Me.ColItemExisted.Count > 0 Then
                        Dim MessageItemCount As String = ""
                        For i As Integer = 0 To Me.ColItemExisted.Count - 1
                            MessageItemCount = MessageItemCount + Me.ColItemExisted(i).ToString()
                            If Me.ColItemExisted.Count - 1 > i Then
                                MessageItemCount &= ","
                            End If
                        Next
                        Me.ShowMessageInfo("BRANDPACK_ID " & MessageItemCount & vbCrLf & "Has been Included" & " For program " & _
                        Me.mcbProgram.Text)
                    Else
                        Me.ShowMessageInfo(Me.MessageDataSaveToAdd)
                    End If
                End If
            ElseIf Me.mcbOriginalBrandPack.Visible = True Then
                Dim PROG_BRANDPACK_ID As String = Me.mcbProgram.Value.ToString() + "" + Me.mcbOriginalBrandPack.Value.ToString()
                If Me.clsMRKT_BRANDPACK.IsExisted(PROG_BRANDPACK_ID) = True Then
                    Me.ShowMessageInfo(Me.MessageDataHasExisted)
                End If
            End If

        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnChekExisting_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ButtonEntry1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEntry1.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                Case "btnAddNew"
                    Me.ClearControl(Me.UiGroupBox1)
                    Me.mcbBrandPack.Text = ""
                    Me.mcbOriginalBrandPack.Text = ""
                    Me.ShowMCBBrandPack()
                    Me.mcbBrandPack.UncheckAll()
                    Me.UnabledControl()
                    Me.lblItem.Text = ""
                    Me.mcbProgram.Text = ""
                    Me.mcbProgram.ReadOnly = False
                    Me.ButtonEntry1.btnInsert.Text = "&Insert"
                    Me.ButtonEntry1.btnAddNew.Enabled = True
                    Me.mcbProgram.Focus()
                    Me.Mode = ModeSave.Save
                Case "btnInsert"
                    If Me.IsValid() = False Then : Return : End If
                    Me.EnabledControl()
                    Me.mcbProgram.DroppedDown = True
                    Me.clsMRKT_BRANDPACK.PROGRAM_NAME = Me.mcbProgram.DropDownList().GetValue("PROGRAM_NAME").ToString()
                    Me.clsMRKT_BRANDPACK.PROGAM_ID = Me.mcbProgram.Value
                    Me.clsMRKT_BRANDPACK.START_DATE = Convert.ToDateTime(Me.dtPicStart.Value.ToShortDateString())
                    Me.clsMRKT_BRANDPACK.END_DATE = Convert.ToDateTime(Me.dtPicEnd.Value.ToShortDateString())
                    Me.mcbProgram.DroppedDown = False
                    Me.SFG = StateFillingGrid.Filling
                    Me.SFC = StateFillingCombo.Filling
                    Select Case Me.Mode
                        Case ModeSave.Save
                            If Me.mcbBrandPack.CheckedValues.Length > 0 Then
                                Me.ColItemExisted = New Hashtable()
                                Me.ColItemExisted.Clear()
                                For i As Integer = 0 To Me.mcbBrandPack.CheckedValues.Length - 1
                                    Dim PROG_BRANDPACK_ID As String = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.CheckedValues().GetValue(i).ToString()
                                    If Me.clsMRKT_BRANDPACK.IsExisted(PROG_BRANDPACK_ID) = True Then
                                        Me.ColItemExisted.Add(i, Me.mcbBrandPack.CheckedValues().GetValue(i).ToString())
                                    End If
                                Next
                                If Me.ColItemExisted.Count > 0 Then
                                    Dim MessageItemCount As String = ""
                                    For i As Integer = 0 To Me.ColItemExisted.Count - 1
                                        MessageItemCount = MessageItemCount + Me.ColItemExisted(i).ToString()
                                        If Me.ColItemExisted.Count - 1 > i Then
                                            MessageItemCount &= ","
                                        End If
                                    Next
                                    Me.ShowMessageInfo("BRANDPACK_ID " & MessageItemCount & vbCrLf & "Has been Included" & " For program " & _
                                    Me.mcbProgram.Text)
                                    Me.SFC = StateFillingCombo.HasFilled
                                    Me.SFG = StateFillingGrid.HasFilled
                                    Return
                                End If
                                Me.clsMRKT_BRANDPACK.CreateNewItemChangesCollection()
                                For i As Integer = 0 To Me.mcbBrandPack.CheckedValues.Length - 1
                                    Me.clsMRKT_BRANDPACK.BRANDPACK_ID = Me.mcbBrandPack.CheckedValues().GetValue(i).ToString()
                                    Me.clsMRKT_BRANDPACK.PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbBrandPack.CheckedValues().GetValue(i).ToString()
                                    Me.clsMRKT_BRANDPACK.Save_MRKT_BRANDPACK("Save")
                                Next
                                'Me.ShowMessageInfo(Me.MessageSavingSucces)
                                'If Me.clsMRKT_BRANDPACK.ItemChanges.Count > 0 Then
                                '    Me.ShowMessageInfo(Me.clsMRKT_BRANDPACK.ItemChanges.Count.ToString() + " row(s) " + Me.MessageSavingSucces)
                                'Else
                                '    Me.ShowMessageInfo(Me.MessageSavingFailed)
                                'End If
                            Else
                                Me.ShowMessageInfo("Value for Brandpack hasn't been included")
                                Me.SFC = StateFillingCombo.HasFilled
                                Me.SFG = StateFillingGrid.HasFilled
                                Return
                            End If
                        Case ModeSave.Update
                            Me.clsMRKT_BRANDPACK.PROG_BRANDPACK_ID = Me.mcbProgram.Value.ToString() + "" + Me.mcbOriginalBrandPack.Value.ToString()
                            Me.clsMRKT_BRANDPACK.BRANDPACK_ID = Me.mcbOriginalBrandPack.Value.ToString()
                            Me.clsMRKT_BRANDPACK.Save_MRKT_BRANDPACK("Update")
                            'Me.ShowMessageInfo(Me.MessageSavingSucces)
                    End Select
                    Me.RefReshData()
                    Me.mcbProgram.ReadOnly = True
                    Me.SFC = StateFillingCombo.HasFilled
                    Me.SFG = StateFillingGrid.HasFilled
                    ParentFrm.MustReloadData = True
                Case "btnClose"
                    Me.Close()
            End Select
        Catch ex As Exception
            Try
                If Me.Mode = ModeSave.Save Then
                    If Me.clsMRKT_BRANDPACK.ItemChanges.Count > 0 Then
                        Me.ShowMessageInfo("We are sorry, only " + Me.clsMRKT_BRANDPACK.ItemChanges.Count + " row(s)" & Me.MessageSavingSucces)
                    End If
                End If
            Catch
                Me.ShowMessageError(ex.Message)
            End Try
            Me.LogMyEvent(ex.Message, Me.Name + "_ButtonEntry1_btnClick_Save")
        Finally
            Me.SFC = StateFillingCombo.HasFilled
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterBrandPack_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterBrandPack.btnClick
        Try
            'Me.clsMRKT_BRANDPACK.ViewBrandPack().RowFilter = 
            Me.Cursor = Cursors.WaitCursor
            Dim DV As DataView = Nothing
            If Me.mcbBrandPack.Visible = True Then
                DV = Me.clsMRKT_BRANDPACK.SearchBrandPack(Me.mcbBrandPack.Text)
                Me.BindCheckCombo(Me.mcbBrandPack, DV, "")
                Dim itemCount As Integer = Me.mcbBrandPack.DropDownList().RecordCount()
                Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
            ElseIf Me.mcbOriginalBrandPack.Visible = True Then
                DV = Me.clsMRKT_BRANDPACK.SearchBrandPack(Me.mcbOriginalBrandPack.Text)
                Me.BindMultiColumnCombo(Me.mcbOriginalBrandPack, DV, "")
                Dim itemCount As Integer = Me.mcbOriginalBrandPack.DropDownList().RecordCount()
                Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterProgram_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterProgram.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.BindMultiColumnCombo(Me.mcbProgram, Me.clsMRKT_BRANDPACK.GetProgramID(Me.mcbProgram.Text), "")
            Dim itemCount As Integer = Me.mcbProgram.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try


    End Sub

#End Region

#Region " Calendar Combo "
    Private Sub dtPicStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicStart.ValueChanged
        Try
            If Me.SFG = StateFillingGrid.Filling Then
                Return
            End If
            If Me.dtPicStart.Text = "" Then
                Return
            End If
            If Me.mcbProgram.Value Is Nothing Then
                Me.ShowMessageInfo("please Define the program.")
                Me.dtPicStart.ResetText()
                Return
            End If
            If CType(Me.OriginalStartDateFromProgram, Object) Is Nothing Then
                Me.clsMRKT_BRANDPACK.GetDateFromProgram(Me.mcbProgram.Value.ToString())
                Me.OriginalStartDateFromProgram = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.START_DATE)
                If Me.dtPicStart.Value < Me.OriginalStartDateFromProgram Then
                    Me.dtPicStart.Value = Me.OriginalStartDateFromProgram
                End If
                Me.OriginalEndDateFromProgram = Convert.ToDateTime(Me.clsMRKT_BRANDPACK.END_DATE)
                Me.dtPicEnd.Value = Me.OriginalEndDateFromProgram
                Me.dtPicStart.MinDate = Me.OriginalStartDateFromProgram
                Me.dtPicStart.MaxDate = Me.OriginalEndDateFromProgram
                Me.dtPicEnd.MinDate = Me.OriginalStartDateFromProgram
                Me.dtPicEnd.MaxDate = Me.OriginalEndDateFromProgram
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtPicEnd_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicEnd.ValueChanged
        Try
            If Me.SFG = StateFillingGrid.Filling Then
                Return
            End If
            If Me.dtPicEnd.Text = "" Then
                Return
            End If
            If Me.dtPicStart.Text = "" Then
                Me.dtPicEnd.ResetText()
                Return
            End If
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region " GridEx "

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFG = StateFillingGrid.Filling Then
                Return
            End If
            If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Me.Mode = ModeSave.Update
            Me.UM = UpdateMode.FromGrid
            Me.lblItem.Text = ""
            Me.ShowMCBOriginalBrandPack()
            Me.InflateData()
            'Me.mcbOriginalBrandPack.Value = Me.GridEX1.GetValue("BRANDPACK_ID")
            Me.OriginalStartDateFromBrandPack = Convert.ToDateTime(Me.GridEX1.GetValue("BRANDPACK_START_DATE"))
            Me.OriginalEndDateFromBrandPack = Convert.ToDateTime(Me.GridEX1.GetValue("BRANDPACK_END_DATE"))
            'Me.dtPicStart.MaxDate = Me.OriginalStartDateFromProgram
            Me.dtPicStart.IsNullDate = False
            Me.dtPicEnd.IsNullDate = False
            Me.dtPicStart.Value = Me.OriginalStartDateFromBrandPack
            Me.dtPicEnd.Value = Me.OriginalEndDateFromBrandPack
            If Me.clsMRKT_BRANDPACK.HasReferencedData(Me.GridEX1.GetValue("ID")) Then
                Me.UnabledControl()
                Me.ButtonEntry1.btnInsert.Enabled = False
            Else
                Me.EnabledControl()
                Me.mcbOriginalBrandPack.ReadOnly = True
                Me.mcbProgram.ReadOnly = True
                Me.ButtonEntry1.btnInsert.Enabled = True
                Me.ButtonEntry1.btnInsert.Text = "&Update"
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Me.clsMRKT_BRANDPACK.HasReferencedData(Me.GridEX1.GetValue("ID")) = True Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                e.Cancel = True
                Me.GridEX1.Refetch()
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Me.GridEX1.Refetch()
            End If
            Me.clsMRKT_BRANDPACK.DeletePROG_BRANDPACK(Me.GridEX1.GetValue("ID"))
            Me.ShowMessageInfo(Me.MessageSuccesDelete)
            Me.GridEX1.UpdateData()
            e.Cancel = False
            Me.ButtonEntry1.btnInsert.Text = "&Insert"
            Me.ButtonEntry1.btnInsert.Enabled = False
            Me.ClearControl(Me.UiGroupBox1)
            Me.mcbOriginalBrandPack.Text = ""
            Me.mcbProgram.Text = ""
            If Me.GridEX1.RecordCount = 0 Then
                Me.UnabledControl()
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region " Checked Combobox "

    Private Sub mcbBrandPack_CheckedValuesChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mcbBrandPack.CheckedValuesChanged
        If IsNothing(Me.mcbBrandPack.CheckedValues) Then
            Return
        End If
        If Me.mcbProgram.Value Is Nothing Then
            Me.ShowMessageInfo("Please Define the Program.")
            Me.mcbBrandPack.UncheckAll()
            Me.lblItem.Text = ""
            Return
        End If
        Me.lblItem.Text = Me.mcbBrandPack.CheckedValues.Length.ToString() + " item(s) Selected"
        Me.ButtonEntry1.btnInsert.Enabled = True
    End Sub

#End Region

#End Region

End Class

