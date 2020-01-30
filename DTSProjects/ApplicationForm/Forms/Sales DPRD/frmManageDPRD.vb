Imports System.Xml
Public Class frmManageDPRD
    Private SelectedControl As ControlActive
    Private frmAttachment As Attachment
    Private _ReachingTarget As ReachingTargetKios
    Friend CMain As Main = Nothing
    Private Enum ControlActive
        None
        Kios
        POKios
        TargetReaching
        AttachmentKios
        AttachmentAgreeement
        AttachmentReaching
        AttachmentAchievementDPRDM
        AttachmentPOKios
    End Enum
    Private Sub AddControl(ByVal ctrl As Control)
        'Me.pnlData.Closed = False
        If (Not Me.XpGradientPanel1.Controls.Contains(ctrl)) Then
            Me.XpGradientPanel1.Controls.Add(ctrl)
          
        End If
    End Sub
    Private Sub ShowControl()
        Me.ShowControl(DirectCast(Me.frmAttachment, Control))
    End Sub

    Private Sub ShowControl(ByVal Ctrl As Control)
        For Each ctr As Control In Me.XpGradientPanel1.Controls
            If Not ctr.Equals(Ctrl) Then
                ctr.Dock = DockStyle.None : ctr.SendToBack() : ctr.Hide()
            End If
        Next
        Ctrl.Dock = DockStyle.Fill
        Ctrl.Show()
        Ctrl.BringToFront()
    End Sub
    Private UsrControl As UserControl = Nothing
    Private Sub btnSaveClick(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(Me.UsrControl) Then
            If (Me.UsrControl.Name = "Attachment") Then
                Me.XpGradientPanel1.Controls.Remove(Me.UsrControl)
                If Not (Me.SelectedControl = ControlActive.AttachmentAgreeement) Then
                    DirectCast(Me.UsrControl, Attachment).Dispose()
                    Me.SelectedControl = ControlActive.None
                End If
            End If
            For Each ctrl As Control In Me.pnlAttachment.Controls
                If TypeOf (ctrl) Is RadioButton Then
                    Dim c As RadioButton = CType(ctrl, RadioButton)
                    c.Checked = False
                End If
            Next
        End If
    End Sub
    Private Sub frmManageDPRD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.FilterEditor1.Visible = False
            Me.Bar2.Enabled = True
        Catch

        Finally
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ViewData()
        If (Me.rdbnViewKios.Checked) Then
            Me.rdbnViewKios_Click(Me.rdbnViewKios, New EventArgs())
        ElseIf Me.rdbViewPOKios.Checked Then
            Me.rdbViewPOKios_Click(Me.rdbViewPOKios, New EventArgs())
        ElseIf Me.rdbViewReachingTargetKios.Checked Then
            Me.rdbViewReachingTargetKios_Click(Me.rdbViewReachingTargetKios, New EventArgs())
        ElseIf Me.rdbViewAchievementDPRDM.Checked Then
            Me.rdbViewAchievementDPRDM_Click(Me.rdbViewAchievementDPRDM, New EventArgs())
        End If

    End Sub

    Private Sub NavigationPane1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NavigationPane1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case DirectCast(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnAttachment"
                    'Me.AttahData()
                    Me.Bar2.Enabled = False
                    For Each ctrl As Control In Me.pnlAttachment.Controls
                        If TypeOf (ctrl) Is RadioButton Then
                            Dim c As RadioButton = CType(ctrl, RadioButton)
                            c.Checked = False
                        End If
                    Next
                    Select Case Me.SelectedControl
                        Case ControlActive.AttachmentAgreeement, ControlActive.AttachmentKios, ControlActive.AttachmentPOKios, ControlActive.AttachmentReaching
                            Me.XpGradientPanel1.Controls.Remove(Me.frmAttachment)
                            'Me.frmAttachment.Dispose()
                            Me.frmAttachment = Nothing
                        Case ControlActive.Kios, ControlActive.POKios, ControlActive.TargetReaching
                            Me.UsrControl.Dock = DockStyle.None : Me.UsrControl.SendToBack() : Me.UsrControl.Hide()
                    End Select

                Case "btnViewData"
                    Me.ViewData()
                    Me.Bar2.Enabled = True

            End Select
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_ NavigationPane1_ItemClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbnViewKios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbnViewKios.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim _Kios As New Kios()
            Me.UsrControl = DirectCast(_Kios, UserControl)
            Me.AddControl(_Kios)
            Me.SelectedControl = ControlActive.Kios
            Me.UsrControl = _Kios
            Me.GridEXPrintDocument1.GridEX = Nothing
            Me.AcceptButton = _Kios.TManager1.btnSearch
            Me.ShowControl(_Kios)
            If (Me.FilterEditor1.Visible) Then
                Me.FilterEditor1.SortFieldList = False : Me.FilterEditor1.SourceControl = _Kios.GridEx
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbnViewKios_Click") : Me.rdbnViewKios.Checked = False
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub rdbViewPOKios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbViewPOKios.Click
        Try
            Me.Cursor = Cursors.WaitCursor : Dim _POKios As POManager = Nothing
            Dim IsPOHasAdded As Boolean = False
            If Me.XpGradientPanel1.Controls.Count > 0 Then
                Dim ctrlName As String = ""
                For i As Integer = 0 To Me.XpGradientPanel1.Controls.Count - 1
                    ctrlName = Me.XpGradientPanel1.Controls(i).Name
                    If ctrlName = "POManager" Then
                        IsPOHasAdded = True : _POKios = DirectCast(Me.XpGradientPanel1.Controls(i), POManager)
                    End If
                Next
            End If
            If Not IsPOHasAdded Then
                _POKios = New POManager()
            End If
            Me.UsrControl = DirectCast(_POKios, UserControl)
            Me.AddControl(_POKios)
            Me.SelectedControl = ControlActive.POKios
            Me.GridEXPrintDocument1.GridEX = Nothing
            Me.AcceptButton = _POKios.Manager1.btnSearch
            Me.ShowControl(_POKios)
            If IsPOHasAdded Then
                _POKios.ButtonClick(_POKios.Manager1.btnSearch, New EventArgs())
            End If
            If (Me.FilterEditor1.Visible) Then
                Me.FilterEditor1.SortFieldList = False : Me.FilterEditor1.SourceControl = _POKios.grid
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbViewPOKios_Click") : Me.rdbViewPOKios.Checked = False
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbViewReachingTargetKios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbViewReachingTargetKios.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If IsNothing(Me._ReachingTarget) OrElse Me._ReachingTarget.IsDisposed Then
                _ReachingTarget = New ReachingTargetKios()
            End If
            Me.UsrControl = DirectCast(_ReachingTarget, UserControl)
            _ReachingTarget.rdbDPRDS.Checked = True
            Me.AddControl(_ReachingTarget)
            Me.SelectedControl = ControlActive.TargetReaching
            '_ReachingTarget.Show()
            Me.GridEXPrintDocument1.GridEX = Nothing
            Me.ShowControl(_ReachingTarget)
            If Me.FilterEditor1.Visible Then
                Me.FilterEditor1.SortFieldList = False : Me.FilterEditor1.SourceControl = _ReachingTarget.GridEX1
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbViewReachingTargetKios_Click") : Me.rdbViewReachingTargetKios.Checked = False
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub rdbKios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbKios.Click
        Dim isCatchHandled As Boolean = True
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim OPD As New OpenFileDialog()
            With OPD
                .Title = "Open file Kios Attachment"
                .DefaultExt = ".Kios"
                .Filter = "Kios Files|*.Kios"
                Select Case Me.SelectedControl
                    Case ControlActive.AttachmentAgreeement, ControlActive.AttachmentKios, ControlActive.AttachmentPOKios, ControlActive.AttachmentReaching
                        Me.XpGradientPanel1.Controls.Remove(Me.frmAttachment)
                        'Me.frmAttachment.Dispose()
                        Me.frmAttachment = Nothing

                    Case ControlActive.Kios, ControlActive.POKios, ControlActive.TargetReaching
                        Me.UsrControl.Dock = DockStyle.None : Me.UsrControl.SendToBack() : Me.UsrControl.Hide()
                End Select
                Me.SelectedControl = ControlActive.None
                If OPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    ''chek file
                    Dim C As New XmlDataDocument()
                    C.Load(OPD.FileName)
                    Dim ListNode As XmlNodeList = C.GetElementsByTagName("Kios_Name")
                    If ListNode.Count <= 0 Then
                        Me.ShowMessageInfo("Invalid Data attachment." & vbCrLf & "Probably wrong attachment data.")
                        Me.rdbKios.Checked = False
                        Return
                    End If
                    Me.frmAttachment = New Attachment()
                    Dim DS As DataSet = New DataSet("DSKios")
                    DS.ReadXml(OPD.FileName, XmlReadMode.ReadSchema)
                    frmAttachment.Ds = DS
                    Me.AddControl(frmAttachment)
                    'AddHandler frmAttachment.ShowThis, AddressOf ShowControl
                    Me.SelectedControl = ControlActive.AttachmentKios
                    Me.GridEXPrintDocument1.GridEX = Nothing
                    Me.ShowControl(DirectCast(frmAttachment, Control))
                    Me.UsrControl = DirectCast(frmAttachment, UserControl)
                    RemoveHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
                    AddHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
                    isCatchHandled = False
                Else
                    Me.rdbKios.Checked = False
                End If
            End With

        Catch ex As Exception
            If Not isCatchHandled Then
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbKios_Click") : Me.rdbKios.Checked = False
                Me.ShowMessageInfo(ex.Message)
                Me.XpGradientPanel1.Controls.Remove(DirectCast(Me.frmAttachment, Control))
            Else
                AddHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbDPRD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDPRD.Click
        Dim isCatchHandled As Boolean = True
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim OPD As New OpenFileDialog()

            With OPD
                .Title = "Open file DPRD Attachment"
                .DefaultExt = ".DPRD"
                .Filter = "DPRD files|*.DPRD"
                Select Case Me.SelectedControl
                    Case ControlActive.AttachmentAgreeement, ControlActive.AttachmentKios, ControlActive.AttachmentPOKios, ControlActive.AttachmentReaching
                        Me.XpGradientPanel1.Controls.Remove(Me.frmAttachment)
                        'Me.frmAttachment.Dispose()
                        Me.frmAttachment = Nothing

                    Case ControlActive.Kios, ControlActive.POKios, ControlActive.TargetReaching
                        Me.UsrControl.Dock = DockStyle.None : Me.UsrControl.SendToBack() : Me.UsrControl.Hide()
                End Select
                Me.SelectedControl = ControlActive.None
                If OPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    ''chek file
                    Dim C As New XmlDataDocument()
                    C.Load(OPD.FileName)
                    Dim ListNode As XmlNodeList = C.GetElementsByTagName("MRKT_MARKETING_PROGRAM")
                    If ListNode.Count <= 0 Then
                        Me.ShowMessageInfo("Invalid Data attachment." & vbCrLf & "Probably wrong attachment data.")
                        Me.rdbDPRD.Checked = False
                        Return
                    End If
                    Me.frmAttachment = New Attachment()
                    Dim DS As DataSet = New DataSet("DSSalesProgramDTS")
                    DS.ReadXml(OPD.FileName, XmlReadMode.ReadSchema)
                    Me.frmAttachment.Ds = DS
                    'AddHandler frmAttachment.ShowThis, AddressOf ShowControl
                    Me.AddControl(DirectCast(Me.frmAttachment, Control))
                    Me.SelectedControl = ControlActive.AttachmentAgreeement
                    Me.GridEXPrintDocument1.GridEX = Nothing
                    Me.ShowControl(DirectCast(Me.frmAttachment, Control))
                    Me.UsrControl = DirectCast(Me.frmAttachment, UserControl)
                    RemoveHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
                    AddHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
                    isCatchHandled = False
                Else
                    Me.rdbDPRD.Checked = False
                End If
            End With
        Catch ex As Exception
            If Not isCatchHandled Then
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbDPRD_Click")
                Me.ShowMessageInfo(ex.Message) : Me.rdbDPRD.Checked = False
                Me.XpGradientPanel1.Controls.Remove(DirectCast(Me.frmAttachment, Control))
            Else
                AddHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbReaching_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbReaching.Click
        Dim isCatchHandled As Boolean = True
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim OPD As New OpenFileDialog()
            With OPD
                .Title = "Open file Achievement Kios"
                .DefaultExt = ".Achievement"
                .Filter = "Achievement files|*.Achievement"
                Select Case Me.SelectedControl
                    Case ControlActive.AttachmentAgreeement, ControlActive.AttachmentKios, _
                    ControlActive.AttachmentPOKios, ControlActive.AttachmentReaching, ControlActive.AttachmentAchievementDPRDM
                        Me.XpGradientPanel1.Controls.Remove(Me.frmAttachment)
                        'Me.frmAttachment.Dispose()
                        Me.frmAttachment = Nothing
                    Case ControlActive.Kios, ControlActive.POKios, ControlActive.TargetReaching
                        Me.UsrControl.Dock = DockStyle.None : Me.UsrControl.SendToBack() : Me.UsrControl.Hide()
                End Select
                Me.SelectedControl = ControlActive.None
                If OPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    ''chek file
                    Dim C As New XmlDataDocument()
                    C.Load(OPD.FileName)
                    Dim ListNode As XmlNodeList = C.GetElementsByTagName("BrandReachingView")
                    If ListNode.Count <= 0 Then
                        Me.ShowMessageInfo("Invalid Data attachment." & vbCrLf & "Probably wrong attachment data.")
                        Me.rdbReaching.Checked = False
                        Return
                    End If
                    Me.frmAttachment = New Attachment()
                    Dim DS As DataSet = New DataSet("TargetReaching")
                    DS.ReadXml(OPD.FileName, XmlReadMode.ReadSchema)
                    Me.frmAttachment.Ds = DS
                    Me.AddControl(DirectCast(Me.frmAttachment, Control))
                    'AddHandler frmAttachment.ShowThis, AddressOf ShowControl
                    Me.SelectedControl = ControlActive.AttachmentReaching
                    Me.GridEXPrintDocument1.GridEX = Nothing
                    Me.ShowControl(DirectCast(Me.frmAttachment, Control))
                    Me.UsrControl = DirectCast(Me.frmAttachment, UserControl)
                    RemoveHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
                    AddHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
                    isCatchHandled = False
                Else
                    Me.rdbReaching.Checked = False
                End If
            End With
        Catch ex As Exception
            If Not isCatchHandled Then
                Me.LogMyEvent(ex.Message, Me.Name + "rdbReaching_Click") : Me.rdbReaching.Checked = False
                Me.ShowMessageInfo(ex.Message)
                Me.XpGradientPanel1.Controls.Remove(DirectCast(Me.frmAttachment, Control))
            Else
                AddHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
            End If

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPO.Click
        Dim isCatchHandled As Boolean = True
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim OPD As New OpenFileDialog()
            With OPD
                .Title = "Open file PO Kios"
                .DefaultExt = ".POKios"
                .Filter = "POKios files|*.POKios"
                Select Case Me.SelectedControl
                    Case ControlActive.AttachmentAgreeement, ControlActive.AttachmentKios, ControlActive.AttachmentPOKios, ControlActive.AttachmentReaching
                        Me.XpGradientPanel1.Controls.Remove(Me.frmAttachment)
                        'Me.frmAttachment.Dispose()
                        Me.frmAttachment = Nothing
                    Case ControlActive.Kios, ControlActive.POKios, ControlActive.TargetReaching
                        Me.UsrControl.Dock = DockStyle.None : Me.UsrControl.SendToBack() : Me.UsrControl.Hide()
                End Select
                Me.SelectedControl = ControlActive.None
                If OPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    ''chek file
                    Dim C As New XmlDataDocument()
                    C.Load(OPD.FileName)
                    Dim ListNode As XmlNodeList = C.GetElementsByTagName("SalesDate")
                    If ListNode.Count <= 0 Then
                        Me.ShowMessageInfo("Invalid Data attachment." & vbCrLf & "Probably wrong attachment data.")
                        Me.rdbPO.Checked = False
                        Return
                    End If
                    Me.frmAttachment = New Attachment()
                    Dim DS As DataSet = New DataSet("DSPO")
                    DS.ReadXml(OPD.FileName, XmlReadMode.ReadSchema)
                    Me.frmAttachment.Ds = DS
                    Me.AddControl(DirectCast(Me.frmAttachment, Control))
                    'AddHandler frmAttachment.ShowThis, AddressOf ShowControl
                    Me.SelectedControl = ControlActive.AttachmentPOKios
                    Me.GridEXPrintDocument1.GridEX = Nothing
                    Me.ShowControl(DirectCast(Me.frmAttachment, Control))
                    Me.UsrControl = DirectCast(Me.frmAttachment, UserControl)
                    RemoveHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
                    AddHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
                    isCatchHandled = False
                Else
                    Me.rdbPO.Checked = False
                End If
            End With
        Catch ex As Exception
            If Not isCatchHandled Then
                Me.LogMyEvent(ex.Message, Me.Name + "_rdbPO_Click") : Me.rdbViewPOKios.Checked = False
                Me.ShowMessageInfo(ex.Message)
                Me.XpGradientPanel1.Controls.Remove(DirectCast(Me.frmAttachment, Control))
            Else
                AddHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            If Not IsNothing(Me.UsrControl) Then
                Select Case item.Name
                    Case "btnShowFieldChooser"
                        Select Case Me.UsrControl.Name
                            Case "Kios"
                                DirectCast(Me.UsrControl, Kios).GridEx.ShowFieldChooser(Me)
                            Case "POManager"
                                DirectCast(Me.UsrControl, POManager).grid.ShowFieldChooser(Me)
                            Case "ReachingTargetKios"
                                DirectCast(Me.UsrControl, ReachingTargetKios).GridEX1.ShowFieldChooser(Me)
                        End Select
                    Case "btnSettingGrid"
                        Dim SetGrid As New SettingGrid()
                        Select Case Me.UsrControl.Name
                            Case "Kios"
                                SetGrid.Grid = DirectCast(Me.UsrControl, Kios).GridEx
                            Case "POManager"
                                SetGrid.Grid = DirectCast(Me.UsrControl, POManager).grid
                            Case "ReachingTargetKios"
                                SetGrid.Grid = DirectCast(Me.UsrControl, ReachingTargetKios).GridEX1
                        End Select
                        SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                        SetGrid.ShowDialog()
                    Case "btnPrint"
                        Select Case Me.UsrControl.Name
                            Case "Kios"
                                Me.GridEXPrintDocument1.GridEX = DirectCast(Me.UsrControl, Kios).GridEx
                                Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                                If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                                    Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                                End If
                            Case "POManager"
                                Me.GridEXPrintDocument1.GridEX = DirectCast(Me.UsrControl, POManager).grid
                                Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                                If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                                    Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                                End If
                            Case "ReachingTargetKios"
                                Me.GridEXPrintDocument1.GridEX = DirectCast(Me.UsrControl, ReachingTargetKios).GridEX1
                                Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                                If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                                    Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                                End If
                        End Select
                        If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                            Me.PrintPreviewDialog1.Document.Print()
                        End If
                    Case "btnPageSettings"
                        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                        Me.PageSetupDialog1.ShowDialog(Me)
                    Case "btnCustomFilter"
                        Me.FilterEditor1.Visible = True
                        Me.FilterEditor1.SortFieldList = False
                        Select Case Me.UsrControl.Name
                            Case "Kios"
                                Dim _Kios As Kios = DirectCast(Me.UsrControl, Kios)
                                Me.FilterEditor1.SourceControl = _Kios.GridEx
                                _Kios.GridEx.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                _Kios.GridEx.RemoveFilters()
                            Case "POManager"
                                Dim PO As POManager = DirectCast(Me.UsrControl, POManager)
                                Me.FilterEditor1.SourceControl = PO.grid
                                PO.grid.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                PO.grid.RemoveFilters()
                            Case "ReachingTargetKios"
                                Dim RTarget As ReachingTargetKios = DirectCast(Me.UsrControl, ReachingTargetKios)
                                Me.FilterEditor1.SourceControl = RTarget.GridEX1
                                RTarget.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                RTarget.GridEX1.RemoveFilters()
                        End Select
                    Case "btnFilterEqual"
                        Me.FilterEditor1.Visible = False
                        Select Case Me.UsrControl.Name
                            Case "Kios"
                                Dim _Kios As Kios = DirectCast(Me.UsrControl, Kios)
                                _Kios.GridEx.RemoveFilters()
                                _Kios.GridEx.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Case "POManager"
                                Dim PO As POManager = DirectCast(Me.UsrControl, POManager)
                                PO.grid.RemoveFilters() : PO.grid.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Case "ReachingTargetKios"
                                Dim RTarget As ReachingTargetKios = DirectCast(Me.UsrControl, ReachingTargetKios)
                                RTarget.GridEx.RemoveFilters()
                                RTarget.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        End Select
                    Case "btnExport"
                        Dim SFD As New SaveFileDialog()
                        With SFD
                            .Title = "Define the location file"
                            .OverwritePrompt = True
                            .DefaultExt = ".xls"
                            .Filter = "excel file|*.xls"
                            .InitialDirectory = "C:\"
                            If SFD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Using FS As New System.IO.FileStream(SFD.FileName, IO.FileMode.Create)
                                    Using Exporter As New Janus.Windows.GridEX.Export.GridEXExporter()
                                        Exporter.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                                        Exporter.IncludeHeaders = True
                                        Exporter.IncludeExcelProcessingInstruction = True
                                        Exporter.IncludeFormatStyle = True
                                        Select Case Me.UsrControl.Name
                                            Case "Kios"
                                                Exporter.GridEX = DirectCast(Me.UsrControl, Kios).GridEx
                                            Case "POManager"
                                                Exporter.GridEX = DirectCast(Me.UsrControl, POManager).grid
                                            Case "ReachingTargetKios"
                                                Exporter.GridEX = DirectCast(Me.UsrControl, ReachingTargetKios).GridEX1
                                        End Select
                                        Exporter.Export(FS) : FS.Flush()
                                        Me.ShowMessageInfo("Data exported to " & SFD.FileName)
                                    End Using
                                End Using
                            End If
                        End With
                End Select
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbAchievementDPRDM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAchievementDPRDM.Click
        Dim isCathHandled As Boolean = True
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim OPD As New OpenFileDialog()
            With OPD
                .Title = "Open file Achievement Kios"
                .DefaultExt = ".Mrkt"
                .Filter = "DPRDM Achievement files|*.Mrkt"
                Select Case Me.SelectedControl
                    Case ControlActive.AttachmentAgreeement, ControlActive.AttachmentKios, _
                    ControlActive.AttachmentPOKios, ControlActive.AttachmentReaching, ControlActive.AttachmentAchievementDPRDM
                        Me.XpGradientPanel1.Controls.Remove(Me.frmAttachment)
                        'Me.frmAttachment.Dispose()
                        Me.frmAttachment = Nothing
                    Case ControlActive.Kios, ControlActive.POKios, ControlActive.TargetReaching
                        Me.UsrControl.Dock = DockStyle.None : Me.UsrControl.SendToBack() : Me.UsrControl.Hide()
                End Select
                Me.SelectedControl = ControlActive.None
                If OPD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    ''chek file
                    Dim C As New XmlDataDocument()
                    C.Load(OPD.FileName)
                    Dim ListNode As XmlNodeList = C.GetElementsByTagName("T_BrandAccomplishment")
                    If ListNode.Count <= 0 Then
                        Me.ShowMessageInfo("Invalid Data attachment." & vbCrLf & "Probably wrong attachment data.")
                        Me.rdbReaching.Checked = False
                        Return
                    End If
                    Me.frmAttachment = New Attachment()
                    Dim DS As DataSet = New DataSet("TargetReaching")
                    DS.ReadXml(OPD.FileName, XmlReadMode.ReadSchema)
                    Me.frmAttachment.Ds = DS
                    Me.AddControl(DirectCast(Me.frmAttachment, Control))
                    'AddHandler frmAttachment.ShowThis, AddressOf ShowControl
                    Me.SelectedControl = ControlActive.AttachmentReaching
                    Me.GridEXPrintDocument1.GridEX = Nothing
                    Me.ShowControl(DirectCast(Me.frmAttachment, Control))
                    Me.UsrControl = DirectCast(Me.frmAttachment, UserControl)
                    RemoveHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
                    AddHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
                    isCathHandled = False
                Else
                    Me.rdbAchievementDPRDM.Checked = False
                End If
            End With
        Catch ex As Exception
            If Not isCathHandled Then
                Me.LogMyEvent(ex.Message, Me.Name + "rdbReaching_Click") : Me.rdbReaching.Checked = False
                Me.ShowMessageInfo(ex.Message)
                Me.XpGradientPanel1.Controls.Remove(DirectCast(Me.frmAttachment, Control))
            Else
                AddHandler frmAttachment.btnSaveClick, AddressOf btnSaveClick
            End If

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbViewAchievementDPRDM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbViewAchievementDPRDM.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If IsNothing(Me._ReachingTarget) OrElse Me._ReachingTarget.IsDisposed Then
                _ReachingTarget = New ReachingTargetKios()
            End If
            Me.UsrControl = DirectCast(_ReachingTarget, UserControl)
            _ReachingTarget.rdbDPRDM.Checked = True
            Me.AddControl(_ReachingTarget)
            Me.SelectedControl = ControlActive.TargetReaching
            '_ReachingTarget.Show()
            Me.GridEXPrintDocument1.GridEX = Nothing
            Me.ShowControl(_ReachingTarget)
            If Me.FilterEditor1.Visible Then
                Me.FilterEditor1.SortFieldList = False : Me.FilterEditor1.SourceControl = _ReachingTarget.GridEX1
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_rdbViewReachingTargetKios_Click") : Me.rdbViewReachingTargetKios.Checked = False
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class