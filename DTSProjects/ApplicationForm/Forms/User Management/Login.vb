Imports System.Diagnostics
Imports System.Threading
Public Class Login
    Private ST As Progress
    'Private SL As StatusLoading
    'Dim rnd As Random
    'Dim TickCount As Int16
    'Dim resultRandom As Int16
    'Dim Hload As Boolean
    'Protected Enum StatusLoading
    '    Loading = 0
    '    SuccesLoading = 1
    '    Saving = 2
    '    SuccesSaving = 3
    '    ConnectingAndChecking
    '    Failed = 4
    'End Enum
    'Private Delegate Sub onConnecting(ByVal message As String)
    'Private Event Connecting As onConnecting
    'Private Delegate Sub onClosingConnection()
    'Private Event ClosingConnection As onClosingConnection

    'Private Sub ShowProgress(ByVal Message As String) Handles Me.Connecting
    '    Me.ST = New Progress
    '    Application.DoEvents()
    '    Me.ST.Show(Message)

    'End Sub
    'Private Sub closeConnection() Handles Me.ClosingConnection
    '    If Me.Hload = True Then
    '        If Not IsNothing(Me.ST) Then
    '            Me.ST.Close()
    '        End If
    '        Me.Timer1.Enabled = False
    '        Me.Timer1.Stop()
    '        Me.Close()
    '        Me.Cursor = Cursors.Default
    '    Else
    '        If Not IsNothing(Me.ST) Then
    '            Me.ST.Close()
    '        End If
    '        Me.Timer1.Enabled = False
    '        Me.Timer1.Stop()
    '    End If
    '    Me.TickCount = 0
    'End Sub
    Private Ticcount As Integer = 0
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private Enum StatusProgress
        None
        Connecting
        Closed
    End Enum

    Private Sub ShowProceed()
        Me.ST = New Progress : Me.ST.Show("Connecting and gathering resources..") : Me.ST.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.Closed
            Me.ST.Refresh() : Thread.Sleep(50) : Application.DoEvents()
        End While
        Thread.Sleep(50) : Me.ST.Close() : Me.ST = Nothing
        Me.Ticcount = 0
    End Sub

    Protected Sub LogMyEvent(ByVal Message As String, ByVal NamaEvent As String)
        Try
            If Not EventLog.SourceExists("AppException") Then
                EventLog.CreateEventSource("AppException", "Nufarm")
            End If
            EventLog.WriteEntry("AppException", "Date  " + System.DateTime.Now.ToShortDateString() + ": On Hour " + DateTime.Now.Hour.ToString() & _
            ":" & DateTime.Now.Minute.ToString() + " Error = " + Message + " Event = " + NamaEvent, EventLogEntryType.Error)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub XpLoginEntry1_EnterPassword(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XpLoginEntry1.EnterPassword
        Me.XpLoginEntry1.HelpString = "Please enter your passsword"
    End Sub

    Private Sub XpLoginEntry1_Login(ByVal sender As System.Object, ByVal e As SteepValley.Windows.Forms.LoginEventArgs) Handles XpLoginEntry1.Login
        Try
            Me.Enabled = False
            'Me.Hload = False
            Me.Cursor = Cursors.WaitCursor
            'Me.Hide()
            Dim User As New NufarmBussinesRules.User.Login
            Dim HassLoad As Boolean
            If Not Me.XpLoginEntry2.Visible Then
                'RaiseEvent Connecting("Connecting and gathering resources........")
                'rnd = New Random()
                'resultRandom = rnd.Next(1, 4)
                'Me.Timer1.Enabled = True
                Me.StatProg = StatusProgress.Connecting
                Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
                Me.ThreadProgress.Start()
                HassLoad = User.ValidateUser(Me.TextBox1.Text, e.Password.ToString())
            Else
                If Me.TextBox1.Text = "" Then
                    MessageBox.Show("Please type User name .", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Me.Hload = False
                    'RaiseEvent ClosingConnection()
                    Me.Enabled = True
                    'Me.Show()
                    Return
                End If
                If e.Password.ToString() = "" Then
                    MessageBox.Show("Please type your password .", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Me.Hload = False
                    'RaiseEvent ClosingConnection()
                    'Me.Show()
                    Me.Enabled = True
                    Return
                End If
                'RaiseEvent Connecting("Connecting and gathering resource........")
                'rnd = New Random()
                'resultRandom = rnd.Next(1, 4)
                'Me.Timer1.Enabled = True
                'Me.Timer1.Start()
                Me.StatProg = StatusProgress.Connecting
                Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
                Me.ThreadProgress.Start()
                HassLoad = User.ValidateUser(Me.TextBox1.Text, e.Password.ToString())
            End If
            '= 'User.ValidateUser(me.
            'anggap untuk sekarang hasload true
            ' HassLoad = True
            If HassLoad = True Then
                'Hload = True
                'RaiseEvent ClosingConnection()

                NufarmBussinesRules.User.UserLogin.HasLogin = True
                If Not Me.XpLoginEntry2.Visible Then
                    NufarmBussinesRules.User.UserLogin.UserName = e.User.ToString()
                Else
                    NufarmBussinesRules.User.UserLogin.UserName = Me.TextBox1.Text
                End If
                NufarmBussinesRules.User.UserLogin.UserPassword = e.Password.ToString()
            Else
                'RaiseEvent ClosingConnection()
                NufarmBussinesRules.User.UserLogin.HasLogin = False
                NufarmBussinesRules.User.UserLogin.UserName = ""
                NufarmBussinesRules.User.UserLogin.UserPassword = ""
                Me.Enabled = True
            End If
            Me.StatProg = StatusProgress.Closed
            Me.Close()
        Catch ex As Exception
            'Me.Hload = False
            'RaiseEvent ClosingConnection()
            Me.StatProg = StatusProgress.Closed
            NufarmBussinesRules.User.UserLogin.HasLogin = False
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.LogMyEvent(ex.Message, "XpLoginEntry1_Login")
            Me.Cursor = Cursors.Default
            Me.Enabled = True
            'Me.TickCount = 0
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    'Private Sub ChekTimer(ByVal sender As Object, ByVal e As EventArgs)
    '    If Me.TickCount >= Me.resultRandom Then
    '        If Me.Hload = True Then
    '            RaiseEvent ClosingConnection()
    '        Else
    '            Me.resultRandom += 1
    '        End If
    '    End If
    'End Sub
    Private Sub XpLinkedLabelIcon1_LinkClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XpLinkedLabelIcon1.LinkClicked
        Me.Close()
    End Sub

    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    Me.TickCount += 1
    'End Sub

    Private Sub Login_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
            If (Not (NufarmBussinesRules.User.UserLogin.HasLogin)) Or (NufarmBussinesRules.User.UserLogin.UserName = "") Then
                Me.XpLoginEntry2.Visible = True
                Me.XpLoginEntry2.Icon = CType(resources.GetObject("XpLoginEntry2.Icon"), System.Drawing.Icon)
                Me.XpLoginEntry1.Icon = CType(resources.GetObject("XpLoginEntry1.Icon"), System.Drawing.Icon)
                Me.XpLoginEntry1.UserName = "Password"
            Else
                Me.XpLoginEntry2.Visible = False
                Me.XpLoginEntry1.Icon = CType(resources.GetObject("XpLoginEntry2.Icon"), System.Drawing.Icon)
                Me.XpLoginEntry1.UserName = NufarmBussinesRules.User.UserLogin.UserName
            End If
            Me.TextBox1.Visible = False
            Global.DTSProjects.SplassCreen.Close()
            'AddHandler Timer1.Tick, AddressOf ChekTimer
        Catch ex As Exception

        End Try
    End Sub

    Private Sub XpLoginEntry2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XpLoginEntry2.Click
        Me.TextBox1.Visible = Not Me.TextBox1.Visible
        If Me.TextBox1.Visible = True Then
            Me.XpLoginEntry2.HelpString = "Type UserName to log in"
        Else
            Me.XpLoginEntry2.HelpString = ""
        End If
    End Sub

    Private Sub XpLoginEntry1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles XpLoginEntry1.Leave
        Me.XpLoginEntry1.HelpString = ""
    End Sub

End Class