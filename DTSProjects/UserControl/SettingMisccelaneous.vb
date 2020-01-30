Public Class SettingMisccelaneous
    'Private mySettting As List(Of NufarmBussinesRules.common.SettingConfigurations) = Nothing
    Private IsReadingSettings As Boolean = True
    Public MainParent As Settings
    Private Sub SettingMisccelaneous_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'mySettting = CType(Me.MainParent, Settings).ParentSettings
        ReadSettings()
        Me.IsReadingSettings = False
    End Sub
    Private Sub ReadSettings()
        For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            With Setting
                If .CodeApp = "MSC0001" Then
                    Me.txtViewSMSPO.Value = CInt(.ParamValue)
                ElseIf .CodeApp = "MSC0002" Then
                    Me.txtCancelPO.Value = CInt(.ParamValue)
                ElseIf .CodeApp = "MSC0003" Then
                    If .AllowRules Then
                        Me.chkReadBrandPack.Checked = True
                    Else
                        Me.chkReadBrandPack.Checked = False
                    End If
                ElseIf .CodeApp = "MSC0004" Then
                    If .AllowRules Then
                        Me.chkReadBrandPackIDAgreement.Checked = True
                    Else
                        Me.chkReadBrandPackIDAgreement.Checked = False
                    End If
                ElseIf .CodeApp = "MSC0005" Then
                    If .AllowRules Then
                        Me.chkDeleteSMSPO.Checked = True
                    Else
                        Me.chkDeleteSMSPO.Checked = False
                    End If
                    Me.txtDeleteSMS.Value = CInt(.ParamValue)
                ElseIf .CodeApp = "MSC0006" Then
                    If .AllowRules Then
                        Me.chkQSYOA.Checked = True
                    Else
                        Me.chkQSYOA.Checked = False
                    End If
                ElseIf .CodeApp = "MSC0007" Then
                    If .AllowRules Then
                        Me.chkComputeQSY.Checked = True
                    Else
                        Me.chkComputeQSY.Checked = False
                    End If
                End If
            End With
        Next
    End Sub
    Private Sub txtViewSMSPO_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtViewSMSPO.ValueChanged
        If IsReadingSettings Then : Return : End If
        Me.Cursor = Cursors.WaitCursor
        'MSC0001 = Setting berapa hari PO yang boleh di sms ke distributor terhitung dari tanggal PO
        For i As Integer = 0 To MainParent.ParentSettings.Count - 1
            If MainParent.ParentSettings(i).CodeApp = "MSC0001" Then
                MainParent.ParentSettings(i).ParamValue = CStr(Me.txtViewSMSPO.Value)
            End If
        Next
        'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
        '    If Setting.CodeApp = "MSC0001" Then
        '        With Setting
        '            .ParamValue = CStr(Me.txtViewSMSPO.Value)
        '        End With
        '        Exit For
        '    End If
        'Next
        'CType(MainParent, Settings).ParentSettings = mySettting
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub txtCancelPO_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCancelPO.ValueChanged
        If IsReadingSettings Then : Return : End If
        ' MSC0002 = Setting berapa bulan PO dapat di cancel terhitung dari tanggal PO
        For i As Integer = 0 To MainParent.ParentSettings.Count - 1
            If MainParent.ParentSettings(i).CodeApp = "MSC0002" Then
                MainParent.ParentSettings(i).ParamValue = CStr(Me.txtCancelPO.Value)
            End If
        Next
        'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
        '    If Setting.CodeApp = "MSC0002" Then
        '        With Setting
        '            .ParamValue = CStr(Me.txtCancelPO.Value)
        '        End With
        '        Exit For
        '    End If
        'Next
        'CType(MainParent, Settings).ParentSettings = mySettting
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub chkQSYOA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkQSYOA.CheckedChanged
        If IsReadingSettings Then : Return : End If
        Me.Cursor = Cursors.WaitCursor
        If Me.chkQSYOA.Checked Then
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "MSC0006" Then
                    MainParent.ParentSettings(i).AllowRules = True
                End If
            Next
            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "MSC0006" Then
            '        With Setting
            '            .AllowRules = True
            '        End With
            '        Exit For
            '    End If
            'Next
        Else
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "MSC0006" Then
                    MainParent.ParentSettings(i).AllowRules = False
                End If
            Next

            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "MSC0006" Then
            '        Setting.AllowRules = False
            '        Exit For
            '    End If
            'Next
        End If
        'CType(MainParent, Settings).ParentSettings = mySettting
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub chkDeleteSMSPO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDeleteSMSPO.CheckedChanged
        If IsReadingSettings Then : Return : End If
        Me.Cursor = Cursors.WaitCursor
        If chkDeleteSMSPO.Checked Then
            If IsNothing(Me.txtDeleteSMS.Value) Or (Me.txtDeleteSMS.Value <= 0) Then
                MessageBox.Show("You can't set the option if the value of month is not defined.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.IsReadingSettings = True : Me.chkDeleteSMSPO.Checked = False : Me.IsReadingSettings = False : Return
            End If   'MSC0005 adalah codeapp untuk setting delete po
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "MSC0005" Then
                    MainParent.ParentSettings(i).AllowRules = True
                    MainParent.ParentSettings(i).ParamValue = CStr(Me.txtDeleteSMS.Value)
                End If
            Next

            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "MSC0005" Then
            '        With Setting
            '            .ParamValue = CStr(Me.txtDeleteSMS.Value)
            '            .AllowRules = True
            '        End With
            '        Exit For
            '    End If
            'Next
        Else
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "MSC0005" Then
                    MainParent.ParentSettings(i).AllowRules = False
                    MainParent.ParentSettings(i).ParamValue = CStr(Me.txtDeleteSMS.Value)
                End If
            Next
            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "MSC0005" Then
            '        With Setting
            '            .AllowRules = False
            '            .ParamValue = CStr(Me.txtDeleteSMS.Value)
            '        End With
            '        Exit For
            '    End If
            'Next
        End If
        'CType(MainParent, Settings).ParentSettings = mySettting
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub chkReadBrandPack_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReadBrandPack.CheckedChanged
        If IsReadingSettings Then : Return : End If
        Me.Cursor = Cursors.WaitCursor
        If Me.chkReadBrandPack.Checked Then
            'MSC0003 = Refer BrandPack ID dengan id sama/bisa beda dengan AccPac pada saat ngambil discount QSY di OA 1= sama,0=bisabeda
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "MSC0003" Then
                    MainParent.ParentSettings(i).AllowRules = True
                End If
            Next
            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "MSC0003" Then
            '        With Setting
            '            .AllowRules = True
            '        End With
            '        Exit For
            '    End If
            'Next
        Else
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "MSC0003" Then
                    MainParent.ParentSettings(i).AllowRules = False
                End If
            Next
            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "MSC0003" Then
            '        Setting.AllowRules = False
            '        Exit For
            '    End If
            'Next
        End If
        'CType(MainParent, Settings).ParentSettings = mySettting
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub chkReadBrandPackIDAgreement_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReadBrandPackIDAgreement.CheckedChanged
        If IsReadingSettings Then : Return : End If
        Me.Cursor = Cursors.WaitCursor
        'MSC0004 = Brand untuk Agreement relation di register dengan Brand yang statusnya Active saja  =1,active dan obsolete =0
        If Me.chkReadBrandPackIDAgreement.Checked Then
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "MSC0004" Then
                    MainParent.ParentSettings(i).AllowRules = True
                End If
            Next

            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "MSC0004" Then
            '        With Setting
            '            .AllowRules = True
            '        End With
            '        Exit For
            '    End If
            'Next
        Else
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "MSC0004" Then
                    MainParent.ParentSettings(i).AllowRules = False
                End If
            Next

            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "MSC0004" Then
            '        Setting.AllowRules = False
            '        Exit For
            '    End If
            'Next
        End If
        'CType(MainParent, Settings).ParentSettings = mySettting
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub txtDeleteSMS_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDeleteSMS.ValueChanged
        If IsReadingSettings Then : Return : End If
        For i As Integer = 0 To MainParent.ParentSettings.Count - 1
            If MainParent.ParentSettings(i).CodeApp = "MSC0005" Then
                MainParent.ParentSettings(i).ParamValue = CStr(Me.txtDeleteSMS.Value)
            End If
        Next

    End Sub

    Private Sub chkComputeQSY_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkComputeQSY.CheckedChanged
        If IsReadingSettings Then : Return : End If
        Me.Cursor = Cursors.WaitCursor
        'MSC0006 = Brand untuk Agreement relation di register dengan Brand yang statusnya Active saja  =1,active dan obsolete =0
        If Me.chkComputeQSY.Checked Then
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "MSC0007" Then
                    MainParent.ParentSettings(i).AllowRules = True
                End If
            Next
        Else
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "MSC0007" Then
                    MainParent.ParentSettings(i).AllowRules = False
                End If
            Next
        End If
        Me.Cursor = Cursors.Default
    End Sub
End Class
