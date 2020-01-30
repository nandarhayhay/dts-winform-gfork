Public Class Settings
    Private SetMiscelaneous As SettingMisccelaneous = Nothing
    Private SetReportGrid As SettingReportGrid = Nothing
    Private setDpdtoVal As SettingDPDByValue = Nothing
    Public dtSetDPD As DataTable = Nothing
    'copykan settinggan refbusisnesRules jadi variable 
    Public ParentSettings As New List(Of NufarmBussinesRules.common.SettingConfigurations)
    Friend CMain As Main = Nothing
    Private Sub AddControl(ByVal ctrl As UserControl)
        'Me.pnlData.Closed = False
        If (Not Me.XpTaskBox1.Controls.Contains(ctrl)) Then
            Me.XpTaskBox1.Controls.Add(ctrl)

        End If
    End Sub
    Private Sub ShowControl(ByVal Ctrl As Control)
        For Each ctr As Control In Me.XpTaskBox1.Controls
            If Not ctr.Equals(Ctrl) Then
                ctr.Dock = DockStyle.None : ctr.SendToBack() : ctr.Hide()
            End If
        Next
        Ctrl.Dock = DockStyle.Fill
        Ctrl.Show()
        Ctrl.BringToFront()
    End Sub
    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case DirectCast(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnMiscellaneous"
                    If IsNothing(Me.SetMiscelaneous) OrElse Me.SetMiscelaneous.IsDisposed Then
                        Me.SetMiscelaneous = New SettingMisccelaneous()
                        Me.SetMiscelaneous.MainParent = Me
                        Me.AddControl(DirectCast(Me.SetMiscelaneous, UserControl))
                    End If
                    Me.ShowControl(DirectCast(Me.SetMiscelaneous, UserControl))
                Case "btnReportgrid"
                    If IsNothing(Me.SetReportGrid) OrElse Me.SetReportGrid.IsDisposed Then
                        Me.SetReportGrid = New SettingReportGrid()
                        Me.SetReportGrid.MainParent = Me
                        Me.AddControl(DirectCast(Me.SetReportGrid, UserControl))
                    End If
                    Me.ShowControl(DirectCast(Me.SetReportGrid, UserControl))
                Case "btnPercDPDToValue"
                    If IsNothing(Me.setDpdtoVal) OrElse Me.setDpdtoVal.IsDisposed Then
                        Me.setDpdtoVal = New SettingDPDByValue()
                        Me.setDpdtoVal.MainParent = Me
                        Me.AddControl(DirectCast(Me.setDpdtoVal, UserControl))
                    End If
                    Me.ShowControl(DirectCast(Me.setDpdtoVal, UserControl))
            End Select
            For Each item As DevComponents.DotNetBar.ButtonItem In Me.ItemPanel1.Items
                DirectCast(item, DevComponents.DotNetBar.ButtonItem).Checked = False
            Next
            DirectCast(sender, DevComponents.DotNetBar.ButtonItem).Checked = True
            Me.XpTaskBox1.HeaderText = "Setting " + DirectCast(sender, DevComponents.DotNetBar.BaseItem).Text
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_ItemPanel1_ItemClick")
            DirectCast(sender, DevComponents.DotNetBar.ButtonItem).Checked = False
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Public Overloads Function ShowDialog(ByVal result As Object, ByVal Result2 As Object) As DialogResult
        Dim DgResult As DialogResult = Windows.Forms.DialogResult.Cancel
        Try
            If Me.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'check apakah data ada yang di perbaiki
                Me.Cursor = Cursors.WaitCursor
                Dim ListChangedSettings As New List(Of NufarmBussinesRules.common.SettingConfigurations)
                For Each OriginalSetting As NufarmBussinesRules.common.SettingConfigurations In NufarmBussinesRules.SharedClass.ListSettings
                    Dim CodeApp As String = OriginalSetting.CodeApp
                    Dim OriginalAllowRules As Boolean = OriginalSetting.AllowRules
                    Dim OriginalParamValue As String = ""
                    If Not IsDBNull(OriginalSetting.ParamValue) Then
                        OriginalParamValue = OriginalSetting.ParamValue
                    End If
                    For i As Integer = 0 To ParentSettings.Count - 1
                        If ParentSettings(i).CodeApp = CodeApp Then
                            Dim ChangedSetting As New NufarmBussinesRules.common.SettingConfigurations()
                            With ChangedSetting
                                .AllowRules = ParentSettings(i).AllowRules
                                .CodeApp = ParentSettings(i).CodeApp
                                .CreatedBy = ParentSettings(i).CreatedBy
                                .CreatedDate = ParentSettings(i).CreatedDate
                                .DescriptionApp = ParentSettings(i).DescriptionApp
                                .NameApp = ParentSettings(i).NameApp
                                .ParamValue = ParentSettings(i).ParamValue
                                .ParamValueType = ParentSettings(i).ParamValueType
                                .TypeApp = ParentSettings(i).TypeApp
                                Select Case CodeApp
                                    Case "MSC0001", "MSC0002"
                                        If Not ParentSettings(i).ParamValue.Equals(OriginalParamValue) Then
                                            ListChangedSettings.Add(ChangedSetting)
                                        End If
                                    Case "MSC0003", "MSC0004", "MSC0006", "MSC0007"
                                        If Not ParentSettings(i).AllowRules.Equals(OriginalAllowRules) Then
                                            ListChangedSettings.Add(ChangedSetting)
                                        End If
                                    Case "MSC0005"
                                        If (Not ParentSettings(i).AllowRules.Equals(OriginalAllowRules)) _
                                        Or (Not ParentSettings(i).ParamValue.Equals(OriginalParamValue)) Then
                                            ListChangedSettings.Add(ChangedSetting)
                                        End If
                                    Case "RPT001", "RPT002", "RPT003"
                                        If Not ParentSettings(i).AllowRules.Equals(OriginalAllowRules) Then
                                            ListChangedSettings.Add(ChangedSetting)
                                        End If
                                End Select
                            End With

                        End If
                    Next
                Next
                'check data di settingan DVD
                Using clsSetting As New NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting
                    Dim HasSaved As Boolean = False
                    If Not IsNothing(Me.dtSetDPD) Then
                        Dim HasChangedRows As Boolean = False
                        Dim ChangedRows() As DataRow = dtSetDPD.Select("", "", Data.DataViewRowState.Added)
                        If ChangedRows.Length > 0 Then
                            HasChangedRows = True
                        End If
                        If Not HasChangedRows Then
                            ChangedRows = dtSetDPD.Select("", "", Data.DataViewRowState.ModifiedOriginal)
                            If ChangedRows.Length > 0 Then
                                HasChangedRows = True
                            End If
                        End If
                        If Not HasChangedRows Then
                            ChangedRows = dtSetDPD.Select("", "", Data.DataViewRowState.Deleted)
                            If ChangedRows.Length > 0 Then
                                HasChangedRows = True
                            End If
                        End If
                        If HasChangedRows Then
                            clsSetting.SaveSettingDPDVal(Me.dtSetDPD, (ListChangedSettings.Count <= 0))
                            HasSaved = True
                        End If
                    End If
                    If ListChangedSettings.Count > 0 Then
                        clsSetting.SaveSettings(ListChangedSettings)
                        HasSaved = True
                    End If
                    If Not HasSaved Then
                        DgResult = Windows.Forms.DialogResult.Cancel : MessageBox.Show("No data's changed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        DgResult = Windows.Forms.DialogResult.OK
                    End If
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DgResult = Windows.Forms.DialogResult.None
        Finally
            Me.Cursor = Cursors.Default
        End Try
        Return DgResult
    End Function
    Private Sub Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Cursor = Cursors.WaitCursor
        'copy kan data setting ke parentSetting
        Me.ParentSettings.Clear()
        For i As Integer = 0 To NufarmBussinesRules.SharedClass.ListSettings.Count - 1
            Dim mysettings As New NufarmBussinesRules.common.SettingConfigurations()
            With mysettings
                .AllowRules = CBool(NufarmBussinesRules.SharedClass.ListSettings(i).AllowRules)
                .CodeApp = NufarmBussinesRules.SharedClass.ListSettings(i).CodeApp
                .CreatedBy = NufarmBussinesRules.SharedClass.ListSettings(i).CreatedBy
                .CreatedDate = NufarmBussinesRules.SharedClass.ListSettings(i).CreatedDate
                .DescriptionApp = NufarmBussinesRules.SharedClass.ListSettings(i).DescriptionApp
                .NameApp = NufarmBussinesRules.SharedClass.ListSettings(i).NameApp
                .ParamValue = NufarmBussinesRules.SharedClass.ListSettings(i).ParamValue
                .ParamValueType = NufarmBussinesRules.SharedClass.ListSettings(i).ParamValueType
                .TypeApp = NufarmBussinesRules.SharedClass.ListSettings(i).TypeApp
            End With
            Me.ParentSettings.Add(mysettings)
        Next
        'For Each setting As NufarmBussinesRules.common.SettingConfigurations In NufarmBussinesRules.SharedClass.ListSettings
        '    If Not Me.ParentSettings.Contains(setting) Then
        '        Me.ParentSettings.Add(setting)
        '    End If
        'Next
        'Me.ParentSettings = NufarmBussinesRules.SharedClass.ListSettings
        CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
        Me.Cursor = Cursors.Default
    End Sub
End Class