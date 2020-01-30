Imports System.Diagnostics
Imports System.Globalization
Public Class BaseBigForm
    Protected ConfirmDeleteMessage As String = "Delete Data ?!!." & vbCrLf & "Procces can not be undone"
    Protected MessageSavingFailed As String = "Saving Data Failed."
    Protected MessageSavingSucces As String = "Data Saved Succesfuly."
    Protected WithEvents ST As Progress
    Public Event SavingData(ByVal message As String)
    Public Event ClosingForm()
    Protected MessageDataHasChanged As String = "Data has changed,Proceed to save data ?"
    Protected ResultRandom As Integer = 0
    Protected TickCount As Integer = 0
    Protected MayDisposed As Boolean = True
    Protected MessageInsertData As String = "Insert data !?"
    Protected MessageUpdateData As String = "Update data !?"
    Protected MessageSaveChanges As String = "Save Changes to database !?"
    Protected MessageCantDeleteData As String = "Data cannot be deleted !" & vbCrLf & "Because has child referenced data with it.!"
    Protected MessageSuccesDelete As String = "Data has been deleted !."
    Protected MessageDataCantChanged As String = "Data Cannot Be Modified !."
    Protected MessageRefreshData As String = "Data has changed !" & vbCrLf & "If you continue refreshing" & vbCrLf & "All Changes will be discarded" & _
                            vbCrLf & "Continue refreshing anyway ?."
    Protected MessageDataSaveToAdd As String = "Data is safe to add."
    Protected MessageDataHasExisted As String = "Data has existed !."
    Protected MessageDBCOncurency As String = "Update affects 0 record" & vbCrLf & "Perhaps some user has performs the same changes"
    Dim rg As New NufarmBussinesRules.SettingDTS.RegUser()
    Dim clsRef As NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting
    Dim Setting As New NufarmBussinesRules.common.SettingConfigurations()
    Dim hasCheckedSetting As Boolean = False
    Dim AllowRuleToInsertLog As Boolean = False
    Dim IsHasParamsToInsert As Boolean = False

    Protected Sub onSaving(ByVal message As String) Handles Me.SavingData
        Me.ST = New Progress
        Me.ST.Show(message)
    End Sub
    Private Sub onClosingForm() Handles Me.ClosingForm
        If Not IsNothing(Me.ST) Then
            Me.ST.Close()
        End If
        'RemoveHandler Timer1.Tick, AddressOf ChekTimer
        'Me.Timer1.Enabled = False
        'Me.Timer1.Stop()
        'Me.tmr.Enabled = False
        'Me.tmr.Stop()
        Me.TickCount = 0
        Me.ResultRandom = 0
        Me.MayDisposed = True
    End Sub
    Protected Function RepLaceComaWithDot(ByVal text As String) As String
        Dim l As Integer = Len(Trim(text))
        Dim w As Integer = 1
        Dim s As String = ""
        Dim a As String = ""
        Do Until w = l + 1
            s = Mid(Trim(text), w, 1)
            If s = "," Then
                s = "."
            End If
            a = a & s
            w += 1
        Loop
        Return a
    End Function
    Private Function FormatAngka(ByVal angka As Double) As String
        If Convert.IsDBNull(angka) = True Then
            Return ""
        End If
        Dim nfi As NumberFormatInfo = New CultureInfo("en-US", False).NumberFormat
        Dim FormatString As String = angka.ToString("C", nfi)
        FormatString = FormatString.Replace("$", "")
        FormatString = FormatString.TrimEnd("0")
        FormatString = FormatString.Replace(".", "")
        FormatString = FormatString.Replace(",", ".")
        FormatString = FormatString.Trim()
        nfi = Nothing
        Return FormatString
    End Function
    
    Protected Sub ShowProgress()
        Me.MayDisposed = False
        RaiseEvent SavingData("Saving Data....")
        'Me.ST.PictureBox1.Refresh()
        'Me.ST.Refresh()
        'Application.DoEvents()
        Dim rnd As New Random()
        Me.ResultRandom = rnd.Next(1, 4)
    End Sub
    Friend Sub CloseProgres(ByVal SuccesSaving As Boolean)
        If SuccesSaving = True Then
            RaiseEvent ClosingForm()
            Me.ShowMessageInfo(Me.MessageSavingSucces)
        Else
            RaiseEvent ClosingForm()
        End If
    End Sub
    Friend Sub LogMyEvent(ByVal Message As String, ByVal NamaEvent As String)
        Try
            If Not EventLog.SourceExists("AppException") Then
                EventLog.CreateEventSource("AppException", "Nufarm")
            End If
            EventLog.WriteEntry("AppException", String.Format("Date :{0:dd MMMM yyyy}", NufarmBussinesRules.SharedClass.ServerDate()) + ", On Hour : " + NufarmBussinesRules.SharedClass.ServerDate().Hour.ToString() & _
                     ",minutes :" & NufarmBussinesRules.SharedClass.ServerDate().Minute.ToString() + ", Error : " + Message + ", Event = " + NamaEvent, EventLogEntryType.Error)
            If Not Me.hasCheckedSetting Then
                For Each Setting In NufarmBussinesRules.SharedClass.ListSettings
                    If Setting.CodeApp = "MSC0008" Then
                        Me.hasCheckedSetting = True
                        If Setting.AllowRules Then
                            AllowRuleToInsertLog = True
                            Dim ParamValue As String = IIf((Not IsNothing(Setting.ParamValue) And Not IsDBNull(Setting.ParamValue)), Setting.ParamValue.ToString(), "")
                            If Not String.IsNullOrEmpty(ParamValue) Then
                                IsHasParamsToInsert = True
                                Dim LE As Object = rg.Read(Setting.TypeApp)
                                Dim SC As Object = rg.Read("StartDateCounter")
                                If IsNothing(LE) Then
                                    rg.Write(Setting.TypeApp, CInt(Setting.ParamValue))
                                End If
                                If IsNothing(SC) Then
                                    rg.Write("StartDateCounter", NufarmBussinesRules.SharedClass.ServerDate)
                                End If
                                If Not IsNothing(LE) And Not IsNothing(SC) Then
                                    If DateDiff(DateInterval.Day, Convert.ToDateTime(SC), NufarmBussinesRules.SharedClass.ServerDate) >= CInt(ParamValue) Then
                                        If IsNothing(Me.clsRef) Then : Me.clsRef = New NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting()
                                        End If
                                        clsRef.DeleteLogErr(NufarmBussinesRules.User.UserLogin.UserName)
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            End If

            If AllowRuleToInsertLog Then
                If IsHasParamsToInsert Then
                    If IsNothing(Me.clsRef) Then : Me.clsRef = New NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting()
                    End If
                    Dim strMessage As String = String.Format("Date : {0:dd MMMM yyyy}", NufarmBussinesRules.SharedClass.ServerDate()) + ", On Hour : " + NufarmBussinesRules.SharedClass.ServerDate().Hour.ToString() & _
                     ", minutes :" & NufarmBussinesRules.SharedClass.ServerDate().Minute.ToString() + ", Error : " + Message
                    Me.clsRef.SaveLog(Message, NamaEvent)
                Else
                    'delete registry if exists
                    rg.DeleteKey(Setting.TypeApp)
                    rg.DeleteKey("StartDateCounter")
                End If
            Else
                'delete registry if exists
                rg.DeleteKey(Setting.TypeApp)
                rg.DeleteKey("StartDateCounter")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Friend Sub ShowMessageInfo(ByVal info As String)
        MessageBox.Show(info, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Friend Sub ShowMessageError(ByVal ErrorInfo As String)
        MessageBox.Show("Unhandled system Exception due to the folowing error " & vbCrLf & ErrorInfo, "logic Error / unhandled System Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Friend Function ShowConfirmedMessage(ByVal ConfirmedMessage As String) As DialogResult
        Return MessageBox.Show(ConfirmedMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    End Function
    Friend Overridable Sub ClearControl(ByVal objControl As Janus.Windows.EditControls.UIGroupBox)
        Dim TXT As System.Windows.Forms.TextBox
        Dim CMB As System.Windows.Forms.ComboBox
        Dim CHK As System.Windows.Forms.CheckBox
        Dim RDB As System.Windows.Forms.RadioButton
        Dim DP As System.Windows.Forms.DateTimePicker
        Dim LB As System.Windows.Forms.ListBox
        Dim MCB As Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Dim CB As Janus.Windows.CalendarCombo.CalendarCombo
        Dim NE As Janus.Windows.GridEX.EditControls.NumericEditBox
        For Each ctrl As Control In objControl.Controls
            If TypeOf (ctrl) Is System.Windows.Forms.TextBox Then
                TXT = CType(ctrl, System.Windows.Forms.TextBox)
                TXT.Text = ""
            ElseIf TypeOf (ctrl) Is ComboBox Then
                CMB = CType(ctrl, System.Windows.Forms.ComboBox)
                CMB.Text = ""
                CMB.SelectedIndex = -1
            ElseIf TypeOf (ctrl) Is System.Windows.Forms.CheckBox Then
                CHK = CType(ctrl, System.Windows.Forms.CheckBox)
                CHK.Checked = False
            ElseIf TypeOf (ctrl) Is System.Windows.Forms.RadioButton Then
                RDB = CType(ctrl, System.Windows.Forms.RadioButton)
                RDB.Checked = False
            ElseIf TypeOf (ctrl) Is DateTimePicker Then
                DP = CType(ctrl, System.Windows.Forms.DateTimePicker)
                DP.Checked = False
                DP.Value = NufarmBussinesRules.SharedClass.ServerDate()
            ElseIf TypeOf (ctrl) Is System.Windows.Forms.ListBox Then
                LB = CType(ctrl, System.Windows.Forms.ListBox)
                LB.Items.Clear()
            ElseIf TypeOf (ctrl) Is Janus.Windows.GridEX.EditControls.MultiColumnCombo Then
                MCB = CType(ctrl, Janus.Windows.GridEX.EditControls.MultiColumnCombo)
                MCB.Text = ""
                MCB.SelectedIndex = -1
                MCB.Value = Nothing
            ElseIf TypeOf (ctrl) Is Janus.Windows.CalendarCombo.CalendarCombo Then
                CB = CType(ctrl, Janus.Windows.CalendarCombo.CalendarCombo)
                CB.Text = ""
            ElseIf TypeOf (ctrl) Is Janus.Windows.GridEX.EditControls.NumericEditBox Then
                NE = CType(ctrl, Janus.Windows.GridEX.EditControls.NumericEditBox)
                NE.Value = 0
            End If
        Next

    End Sub

    Friend Sub ClearControl(ByVal ObjControls As System.Windows.Forms.Control)
        Dim TXT As System.Windows.Forms.TextBox
        Dim CMB As System.Windows.Forms.ComboBox
        Dim CHK As System.Windows.Forms.CheckBox
        Dim RDB As System.Windows.Forms.RadioButton
        Dim DP As System.Windows.Forms.DateTimePicker
        Dim LB As System.Windows.Forms.ListBox
        For Each ObjControl As Control In ObjControls.Controls
            If TypeOf (ObjControl) Is TextBox Then
                TXT = CType(ObjControl, System.Windows.Forms.TextBox)
                TXT.Text = ""
            ElseIf TypeOf (ObjControl) Is ComboBox Then
                CMB = CType(ObjControl, System.Windows.Forms.ComboBox)
                CMB.Text = ""
                CMB.SelectedIndex = -1
            ElseIf TypeOf (ObjControl) Is CheckBox Then
                CHK = CType(ObjControl, System.Windows.Forms.CheckBox)
                CHK.Checked = False
            ElseIf TypeOf (ObjControl) Is RadioButton Then
                RDB = CType(ObjControl, System.Windows.Forms.RadioButton)
                RDB.Checked = False
            ElseIf TypeOf (ObjControl) Is ListBox Then
                LB = CType(ObjControl, System.Windows.Forms.ListBox)
                LB.Items.Clear()
            ElseIf TypeOf (ObjControl) Is DateTimePicker Then
                DP = CType(ObjControl, System.Windows.Forms.DateTimePicker)
                DP.Value = NufarmBussinesRules.SharedClass.ServerDate()
                DP.Checked = False
            End If
        Next

    End Sub
End Class