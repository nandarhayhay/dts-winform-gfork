Public Class SettingReportGrid
    'Private mySettting As List(Of NufarmBussinesRules.common.SettingConfigurations) = Nothing
    Private IsReadingSetting As Boolean = True
    Public MainParent As Settings
    Private Sub SettingReportGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'mySettting = CType(Me.MainParent, Settings).ParentSettings
        ReadSettings()
        Me.IsReadingSetting = False
    End Sub
    Private Sub ReadSettings()
        For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            With Setting
                If .CodeApp = "RPT001" Then
                    If .AllowRules Then
                        Me.chkPODispro.Checked = True
                    Else
                        Me.chkPODispro.Checked = False
                    End If
                ElseIf .CodeApp = "RPT002" Then
                    If .AllowRules Then
                        Me.chkPODisproByBrand.Checked = True
                    Else
                        Me.chkPODisproByBrand.Checked = False
                    End If
                ElseIf .CodeApp = "RPT003" Then
                    If .AllowRules Then
                        Me.chkReference.Checked = True
                    Else
                        Me.chkReference.Checked = False
                    End If
                End If
            End With
        Next
    End Sub
    Private Sub chkPODispro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPODispro.CheckedChanged
        If IsReadingSetting Then : Return : End If : Me.Cursor = Cursors.WaitCursor
        If Me.chkPODispro.Checked Then
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "RPT001" Then
                    MainParent.ParentSettings(i).AllowRules = True
                End If
            Next
            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "RPT001" Then
            '        With Setting
            '            .AllowRules = True
            '        End With
            '        Exit For
            '    End If
            'Next
        Else
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "RPT001" Then
                    MainParent.ParentSettings(i).AllowRules = False
                End If
            Next
            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "RPT001" Then
            '        With Setting
            '            .AllowRules = False
            '        End With
            '        Exit For
            '    End If
            'Next
        End If
        'CType(MainParent, Settings).ParentSettings = Me.mySettting
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub chkPODisproByBrand_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPODisproByBrand.CheckedChanged
        If IsReadingSetting Then : Return : End If : Me.Cursor = Cursors.WaitCursor
        If Me.chkPODisproByBrand.Checked Then
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "RPT002" Then
                    MainParent.ParentSettings(i).AllowRules = True
                End If
            Next
            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "RPT002" Then
            '        With Setting
            '            .AllowRules = True
            '        End With
            '        Exit For
            '    End If
            'Next
        Else
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "RPT002" Then
                    MainParent.ParentSettings(i).AllowRules = False
                End If
            Next
            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "RPT002" Then
            '        With Setting
            '            .AllowRules = False
            '        End With
            '        Exit For
            '    End If
            'Next
        End If
        'CType(MainParent, Settings).ParentSettings = Me.mySettting
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub chkReference_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReference.CheckedChanged
        If IsReadingSetting Then : Return : End If : Me.Cursor = Cursors.WaitCursor
        If Me.chkReference.Checked Then
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "RPT003" Then
                    MainParent.ParentSettings(i).AllowRules = True
                End If
            Next
            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "RPT003" Then
            '        With Setting
            '            .AllowRules = True
            '        End With
            '        Exit For
            '    End If
            'Next
        Else
            For i As Integer = 0 To MainParent.ParentSettings.Count - 1
                If MainParent.ParentSettings(i).CodeApp = "RPT003" Then
                    MainParent.ParentSettings(i).AllowRules = False
                End If
            Next
            'For Each Setting As NufarmBussinesRules.common.SettingConfigurations In MainParent.ParentSettings
            '    If Setting.CodeApp = "RPT003" Then
            '        With Setting
            '            .AllowRules = False
            '        End With
            '        Exit For
            '    End If
            'Next
        End If
        'CType(MainParent, Settings).ParentSettings = Me.mySettting
        Me.Cursor = Cursors.Default
    End Sub
End Class
