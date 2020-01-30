Public Class Distributor
    Private clsDitributor As NufarmBussinesRules.DistributorRegistering.DistributorRegistering
    Private Mode As ModeSaving
    Friend CMain As Main = Nothing
    Private isLoadingRow As Boolean = False
#Region " Sub "

    Friend Sub InitializeData()
        Me.LoadData()
    End Sub

    Private Sub LoadData()
        Me.clsDitributor = New NufarmBussinesRules.DistributorRegistering.DistributorRegistering
        Me.clsDitributor.GetDataView()
        Me.isLoadingRow = True
        Me.BindGrid()
    End Sub

    Private Sub BindGrid()
        Me.GridEX1.SetDataBinding(Me.clsDitributor.ViewDistributor, "")
        Me.GridEX1.DropDowns("ParentDistributor").SetDataBinding(Me.clsDitributor.ViewDistributor, "")
        Me.GridEX1.DropDowns("TERRITORY").SetDataBinding(Me.clsDitributor.Viewterritory, "")
        Me.BindMulticolumnCombo(Me.clsDitributor.Viewterritory, "", Me.mcbTerritoryID)
        Me.BindMulticolumnCombo(Me.clsDitributor.ViewHolding, "", Me.mcbHolding)
        Me.GridEX1.RootTable.Columns("DISTRIBUTOR_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("DISTRIBUTOR_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        'me.GridEX1.RootTable.Columns("PARENT DISTRIBUTOR_ID").FilterEditType = 
        Me.GridEX1.RootTable.Columns("NPWP").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("MAX_DISC_PER_PO").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("ADDRESS").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("CONTACT").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("PHONE").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("FAX").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("HP").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("CREATE_BY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("CREATE_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.GridEX1.RootTable.Columns("MODIFY_BY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("MODIFY_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
        If Not CMain.IsSystemAdministrator Then
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If col.DataMember = "INACTIVE" Then
                    If (NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Distributor = True) And (NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Distributor = True) Then
                    ElseIf NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Distributor = True Then
                        col.EditType = Janus.Windows.GridEX.EditType.CheckBox
                        col.Visible = True
                    Else
                        col.Visible = False
                        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Else
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
        Else
            Me.GridEX1.RootTable.Columns("INACTIVE").EditType = Janus.Windows.GridEX.EditType.CheckBox
        End If

    End Sub

    Private Sub BindMulticolumnCombo(ByVal dtview As DataView, ByVal rowfilter As String, ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        dtview.RowFilter = rowfilter
        mcb.SetDataBinding(dtview, "")
    End Sub

    Private Sub InflateData()
        If Me.GridEX1.Row = -1 Then
            Return
        End If
        Me.txtDistributorID.Text = Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()
        Me.txtDistributorName.Text = Me.GridEX1.GetValue("DISTRIBUTOR_NAME").ToString()
        Me.txtMaxDistributor.Text = Me.GridEX1.GetValue("MAX_DISC_PER_PO").ToString()
        If (IsNothing(Me.GridEX1.GetValue("TERRITORY_AREA"))) Or (Me.GridEX1.GetValue("TERRITORY_AREA") Is DBNull.Value) Then
            Me.mcbTerritoryID.Value = Nothing
        Else
            Me.mcbTerritoryID.Value = Me.GridEX1.GetValue("TERRITORY_AREA")
        End If
        If (Me.GridEX1.GetValue("PARENT_DISTRIBUTOR_ID") Is DBNull.Value) Or (Me.GridEX1.GetValue("PARENT_DISTRIBUTOR_ID").Equals("")) Then
            Me.mcbHolding.Text = ""
            Me.mcbHolding.SelectedIndex = -1
        Else
            Me.mcbHolding.Value = Me.GridEX1.GetValue("PARENT_DISTRIBUTOR_ID").ToString()
        End If
        If (Me.GridEX1.GetValue("NPWP") Is DBNull.Value) Or (Me.GridEX1.GetValue("NPWP").Equals("")) Then
            Me.txtNPWP.Text = ""
        Else
            Me.txtNPWP.Text = Me.GridEX1.GetValue("NPWP").ToString()
        End If
        If (Me.GridEX1.GetValue("ADDRESS") Is DBNull.Value) Or Me.GridEX1.GetValue("ADDRESS").Equals("") Then
            Me.txtAddress.Text = ""
        Else
            Me.txtAddress.Text = Me.GridEX1.GetValue("ADDRESS").ToString()
        End If
        If (Me.GridEX1.GetValue("CONTACT") Is DBNull.Value) Or (Me.GridEX1.GetValue("CONTACT").Equals("")) Then
            Me.txtContactPerson.Text = ""
        Else
            Me.txtContactPerson.Text = Me.GridEX1.GetValue("CONTACT").ToString()
        End If
        If (Me.GridEX1.GetValue("PHONE") Is DBNull.Value) Or (Me.GridEX1.GetValue("PHONE").Equals("")) Then
            Me.txtContactPhone.Text = ""
        Else
            Me.txtContactPhone.Text = Me.GridEX1.GetValue("PHONE").ToString()
        End If
        If (Me.GridEX1.GetValue("FAX") Is DBNull.Value) Or (Me.GridEX1.GetValue("FAX").Equals("")) Then
            Me.txtContactFax.Text = ""
        Else
            Me.txtContactFax.Text = Me.GridEX1.GetValue("FAX").ToString()
        End If
        If (Me.GridEX1.GetValue("HP") Is DBNull.Value) Or (Me.GridEX1.GetValue("HP").Equals("")) Then
            Me.txtContactMobile.Text = ""
        Else
            Me.txtContactMobile.Text = Me.GridEX1.GetValue("HP").ToString()
        End If
        If (Me.GridEX1.GetValue("BIRTHDATE") Is DBNull.Value) Or (IsNothing(Me.GridEX1.GetValue("BIRTHDATE"))) Then
            Me.dtPicBirtDate.Text = ""
        Else
            Me.dtPicBirtDate.Value = CDate(Me.GridEX1.GetValue("BIRTHDATE"))
        End If
        If Me.GridEX1.GetValue("RESPONSIBLE_PERSON") Is DBNull.Value Then
            Me.txtResponsiblePerson.Text = ""
        Else
            Me.txtResponsiblePerson.Text = Me.GridEX1.GetValue("RESPONSIBLE_PERSON").ToString()
        End If
        If Not IsDBNull(Me.GridEX1.GetValue("JOIN_DATE")) Then
            Me.dtPicJonDate.Value = Convert.ToDateTime(Me.GridEX1.GetValue("JOIN_DATE"))
        Else
            Me.dtPicBirtDate.Text = ""
        End If
        Me.txtContactMobile1.Text = IIf((IsNothing(Me.GridEX1.GetValue("HP1")) Or IsDBNull(Me.GridEX1.GetValue("HP1"))), "", Me.GridEX1.GetValue("HP1").ToString())
    End Sub

    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Distributor = True Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If (NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Distributor = True) And (NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Distributor = True) Then
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Distributor = True Then
                Me.btnAddItem.Visible = True
                'Me.grpViewMode.Text = "Data View Mode"
                'Me.Bar1.Visible = True
                Me.btnSave.Visible = True
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Distributor = True Then
                Me.btnAddItem.Visible = False
                'Me.grpViewMode.Text = "Data View Mode"
                'Me.Bar1.Visible = True
                Me.btnSave.Enabled = True
            Else
                Me.btnAddItem.Visible = False
                'Me.grpViewMode.Text = ""
                'Me.Bar1.Visible = False
                Me.btnSave.Enabled = False
            End If
        End If

    End Sub

#End Region

#Region " Enum "
    Private Enum ModeSaving
        Save
        Update
    End Enum
#End Region

#Region " Function "

    Private Function IsValid() As Boolean
        Dim IsVal As Boolean = True
        If Me.txtDistributorName.Text = "" Then
            MessageBox.Show("Please Suply distributor name.", "Distributor Name Is Null !", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Me.BalloonTip1.SetBalloonCaption(Me.mcbHolding, "")
            'Me.BalloonTip1.SetBalloonText(Me.mcbHolding, "")
            'Me.BalloonTip1.ShowBalloon(Me.mcbTerritoryID)
            Me.txtDistributorName.Focus()
            IsVal = False
        ElseIf Me.clsDitributor.Viewterritory.Find(Me.mcbTerritoryID.Value) = -1 Then
            MessageBox.Show("Please TM_ID", "TM_ID IS NULL !", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Me.BalloonTip1.SetBalloonCaption(Me.txtDistributorName, "")
            'Me.BalloonTip1.SetBalloonText(Me.txtDistributorName, "")
            'Me.BalloonTip1.ShowBalloon(Me.txtDistributorName)
            Me.mcbTerritoryID.Focus()
            IsVal = False
        ElseIf Me.txtDistributorID.Text = "" Then
            MessageBox.Show("Please Suply distributor ID.", "Distributor ID Is Null !", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Me.BalloonTip1.SetBalloonCaption(Me.txtDistributorID, "")
            'Me.BalloonTip1.SetBalloonText(Me.txtDistributorID, "")
            'Me.BalloonTip1.ShowBalloon(Me.txtDistributorID)
            Me.txtDistributorID.Focus()
            IsVal = False
        ElseIf Me.txtMaxDistributor.Text = "" Then
            MessageBox.Show("Please Suply maximum discount per PO.", "Maximum discount per PO is Null!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Me.BalloonTip1.SetBalloonCaption(Me.txtMaxDistributor, "")
            'Me.BalloonTip1.SetBalloonText(Me.txtDistributorID, "")
            'Me.BalloonTip1.ShowBalloon(Me.txtDistributorID)
            Me.txtMaxDistributor.Focus()
            IsVal = False
        End If
        Return IsVal
    End Function

#End Region

#Region " Event Procedure "

    Private Sub GridEX1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Try
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Return
            End If
            If e.Column.Key = "Icon" Then
                Me.Mode = ModeSaving.Update
                Me.XpGradientPanel1.Visible = True
                Me.GridEX1.BringToFront()
                Me.InflateData()
                Me.txtDistributorID.Enabled = False
                'If Me.clsDitributor.HasReferencedChildData(Me.GridEX1.GetValue("DISTRIBUTOR_ID")) = True Then
                '    Me.txtMaxDistributor.Enabled = False
                'Else
                '    Me.txtMaxDistributor.Enabled = True
                'End If
                'Me.grpEdit.Visible = True
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "Distributor_GridEX1_ColumnButtonClick")
        End Try
    End Sub

    Private Sub GridEX1_FilterApplied(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.FilterApplied
        Try
            Me.Refresh()
            'Me.GridEX1.Refresh()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog()
                Case "btnCancel"
                    Me.XpGradientPanel1.Visible = False
                    Me.FilterEditor1.Visible = False
                    'Me.grpEdit.Visible = False
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.Mode = ModeSaving.Save
                Case "btnAddItem"
                    'Me.GridEX1.Dock = DockStyle.Bottom
                    'Me.grpEdit.Dock = DockStyle.Top
                    Me.GridEX1.BringToFront()
                    Me.Mode = ModeSaving.Save
                    Me.ClearControl(Me.grpEdit)
                    Me.txtDistributorID.Enabled = True
                    Me.txtMaxDistributor.Enabled = True
                    Me.GridEX1.Dock = DockStyle.Fill
                    'Me.grpEdit.Dock = DockStyle.Top
                    Me.XpGradientPanel1.Visible = True
                Case "btnRefresh"
                    Me.LoadData()
                    Me.Refresh()
                Case "btnFilter"
                    Me.FilterEditor1.Visible = True
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.GridEX1.RemoveFilters()
                Case "btnRename"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar1)
                Case "btnHideColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Hide"
                    MC.TopMost = True
                    MC.Show(Me.Bar1)
                Case "btnShowColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Show"
                    MC.TopMost = True
                    MC.Show(Me.Bar1)
                    'Case "btnAddItem"
                    '    'Me.GridEX1.Size = New System.Drawing.Size(820, 400)
                    '    Me.grpEdit.Visible = True
                    '    Me.grpEdit.Dock = DockStyle.Top
                    '    Me.FilterEditor1.Visible = False
                    '    Me.txtDistributorID.Enabled = True
                Case "btnCardView"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.CardView
                    Me.GridEX1.RootTable.Columns("Icon").Visible = False
                Case "btnSingleCard"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.SingleCard
                    Me.GridEX1.RootTable.Columns("Icon").Visible = False
                Case "btnViewTable"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.TableView
                    Me.GridEX1.RootTable.Columns("Icon").Visible = True
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
                    'Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                        Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    End If
                    'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                Case "btnPageSettings"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
                Case "btnSave"
                    'If Me.GridEX1.Dock = DockStyle.Fill Then
                    '    
                    'End If
                    If Not Me.grpEdit.Visible = True Then
                        Return
                    End If
                    If Me.IsValid() = False Then
                        Return
                    End If
                    If (Me.txtMaxDistributor.Value) > 100 Then
                        If Me.ShowConfirmedMessage("Are you sure you want to fill Max_disc_per_po > 100") = Windows.Forms.DialogResult.No Then
                            Return
                        End If
                    End If
                    If Not IsNothing(Me.mcbHolding.SelectedItem) And Me.mcbHolding.Text <> "" Then
                        If Me.clsDitributor.FindDistributorHolding(CType(Me.mcbHolding.Value, Object)) = -1 Then
                            Me.baseTooltip.SetToolTip(Me.mcbHolding, "Distributor Name is unknown." & vbCrLf & "Distributor name not found.")
                            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbHolding), Me.mcbHolding, 3000)
                            'Me.baseBallon.SetBalloonCaption(Me.mcbHolding, "Distributor Name ?")
                            'Me.baseBallon.SetBalloonText(Me.mcbHolding, "Distributor name not found !")
                            'Me.baseBallon.ShowBalloon(Me.mcbHolding)
                            Me.mcbHolding.Focus()
                            Return
                        End If
                        Me.clsDitributor.Parent_Distributor_ID = Me.mcbHolding.Value
                    Else
                        Me.clsDitributor.Parent_Distributor_ID = DBNull.Value
                    End If
                    If Not IsNothing(Me.mcbTerritoryID.SelectedItem) Then
                        If Me.clsDitributor.FindTerritory(CType(Me.mcbTerritoryID.Value, Object)) = -1 Then
                            Me.baseTooltip.SetToolTip(Me.mcbTerritoryID, "TERRITORY_ID is unknown." & vbCrLf & "TERRITORY_ID not Found.")
                            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.mcbTerritoryID), Me.mcbTerritoryID, 3000)
                            'Me.baseBallon.SetBalloonCaption(Me.mcbTerritoryID, "Territory Name ?")
                            'Me.baseBallon.SetBalloonText(Me.mcbTerritoryID, "Territory Name not found !")
                            'Me.baseBallon.ShowBalloon(Me.mcbTerritoryID)
                            Me.mcbTerritoryID.Focus()
                            Return
                        End If
                    End If
                    Me.clsDitributor.TERRITORY_ID = Me.mcbTerritoryID.Value

                    If IsNothing(Me.clsDitributor) Then
                        Me.clsDitributor = New NufarmBussinesRules.DistributorRegistering.DistributorRegistering
                    End If
                    Me.clsDitributor.DistributorID = Me.txtDistributorID.Text
                    Me.clsDitributor.DistributorName = Me.txtDistributorName.Text

                    Me.clsDitributor.Max_Disc_Per_PO = Me.txtMaxDistributor.Value
                    If Me.txtAddress.Text <> "" Then
                        Me.clsDitributor.Address = Me.txtAddress.Text
                    Else
                        Me.clsDitributor.Address = DBNull.Value
                    End If
                    If Me.txtContactFax.Text <> "" Then
                        Me.clsDitributor.Fax = Me.txtContactFax.Text
                    Else
                        Me.clsDitributor.Fax = DBNull.Value
                    End If
                    If Me.txtContactMobile.Text <> "" Then
                        Me.clsDitributor.HP = Me.txtContactMobile.Text
                    End If
                    If Me.txtContactPerson.Text <> "" Then
                        Me.clsDitributor.ContactPerson = Me.txtContactPerson.Text
                    Else
                        Me.clsDitributor.ContactPerson = DBNull.Value
                    End If
                    If Me.txtContactPhone.Text <> "" Then
                        Me.clsDitributor.Phone = Me.txtContactPhone.Text
                    Else
                        Me.clsDitributor.Phone = DBNull.Value
                    End If
                    If Me.txtNPWP.Text <> "" Then
                        Me.clsDitributor.NPWP = Me.txtNPWP.Text
                    Else
                        Me.clsDitributor.NPWP = DBNull.Value
                    End If
                    If Me.dtPicBirtDate.Text <> "" Then
                        Me.clsDitributor.BIRTHDATE = CObj(CDate(Me.dtPicBirtDate.Value.ToShortDateString()))
                    Else
                        Me.clsDitributor.BIRTHDATE = DBNull.Value
                    End If
                    Me.clsDitributor.HP1 = Me.txtContactMobile1.Text.TrimStart().TrimEnd()
                    Me.clsDitributor.RESPONSIBLE_PERSON = Me.txtResponsiblePerson.Text
                    Me.clsDitributor.JOIN_DATE = IIf(Me.dtPicJonDate.Text = "", DBNull.Value, Convert.ToDateTime(Me.dtPicJonDate.Value.ToShortDateString()))
                    Select Case Me.Mode
                        Case ModeSaving.Save
                            If Me.clsDitributor.IsDistributorExist(Me.txtDistributorID.Text.Trim()) = True Then
                                Me.baseTooltip.SetToolTip(Me.txtDistributorID, "DistributorID has Existed !." & vbCrLf & "Please Suply another one.")
                                Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtDistributorID), Me.txtDistributorID, 2000)
                                Me.txtDistributorID.Focus()
                                'Me.baseBallon.SetBalloonCaption(Me.txtDistributorID, "DistributorID has Existed!")
                                'Me.baseBallon.SetBalloonText(Me.txtDistributorID, "DistirbutorID has Existed in Database")
                                'Me.baseBallon.ShowBalloon(Me.txtDistributorID)
                                Return
                            End If
                            Me.clsDitributor.InsertData()
                        Case ModeSaving.Update
                            Me.clsDitributor.UpdateData()
                    End Select
                    Me.ShowMessageInfo(Me.MessageSavingSucces)
                    Me.LoadData()
                    'Me.GridEX1.Dock = DockStyle.Fill
                    Me.ClearControl(Me.grpEdit)
                    Me.Mode = ModeSaving.Save

            End Select
            Me.isLoadingRow = False
        Catch ex As Exception
            Me.isLoadingRow = False
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ItemPanel1_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Distributor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            'Me.LoadData()
            'Me.BindGrid()
            Me.GridEX1.Dock = DockStyle.Fill
            Me.XpGradientPanel1.Visible = False
            Me.baseTooltip.ToolTipTitle = "Information"
            Me.dtPicBirtDate.MaxDate = CDate(DateTime.Now.ToShortDateString())
        Catch ex As Exception
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "Distributor_Load")
        Finally
            Me.ReadAcces() : Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub btnFilterHolding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Not IsNothing(Me.clsDitributor) Then
    '            'Me.clsDitributor.ViewHolding.RowFilter = 
    '            Me.BindMulticolumnCombo(Me.clsDitributor.ViewHolding, "DISTRIBUTOR_NAME LIKE '%" & Me.mcbHolding.Text & "%'", Me.mcbHolding)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub btnFilterTerritory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Not IsNothing(Me.clsDitributor) Then
    '            'Me.clsDitributor.ViewHolding.RowFilter = 
    '            Me.BindMulticolumnCombo(Me.clsDitributor.Viewterritory, "TERRITORY_AREA LIKE '%" & Me.mcbTerritoryID.Text & "%'", Me.mcbHolding)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub Distributor_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsDitributor) Then
                Me.clsDitributor.Dispose()
                Me.clsDitributor = Nothing
            End If
            Me.Dispose(True)
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "Distributor_FormClosed")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ButtonSearch1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearch1.btnClick
        Try
            If Not IsNothing(Me.clsDitributor) Then
                'Me.clsDitributor.ViewHolding.RowFilter = 
                Me.BindMulticolumnCombo(Me.clsDitributor.ViewHolding, "DISTRIBUTOR_NAME LIKE '%" & Me.mcbHolding.Text & "%'", Me.mcbHolding)
                Dim itemCount As Integer = Me.mcbHolding.DropDownList().RecordCount()
                Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ButtonSearch2_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearch2.btnClick
        Try
            If Not IsNothing(Me.clsDitributor) Then
                'Me.clsDitributor.ViewHolding.RowFilter = 
                Me.BindMulticolumnCombo(Me.clsDitributor.Viewterritory, "TERRITORY_AREA LIKE '%" & Me.mcbTerritoryID.Text & "%'", Me.mcbHolding)
                Dim itemCount As Integer = Me.mcbTerritoryID.DropDownList().RecordCount()
                Me.ShowMessageInfo(itemCount.ToString() + " item(s) Found")
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub txtMaxDistributor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMaxDistributor.TextChanged
    '    If Val(Me.txtMaxDistributor.Text) > 100 Then
    '        Me.baseTooltip.SetToolTip(Me.txtMaxDistributor, "Value Exceeds from 100 !.")
    '        Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.txtMaxDistributor), Me.txtDistributorID, 3000)
    '        'Me.baseBallon.SetBalloonCaption(Me.txtMaxDistributor, "INVALID VALUE !")
    '        'Me.baseBallon.SetBalloonCaption(Me.txtMaxDistributor, "VALUE exceeds from 100")
    '        'Me.baseBallon.ShowBalloon(Me.txtMaxDistributor)
    '        Me.txtMaxDistributor.Text = Me.txtMaxDistributor.Text.Remove(Me.txtMaxDistributor.Text.Length - 1, 1)
    '    End If
    'End Sub

    Private Sub GridEX1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridEX1.MouseDoubleClick
        Try
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Return
            End If
            Me.Mode = ModeSaving.Update
            Me.XpGradientPanel1.Visible = True
            Me.GridEX1.BringToFront()
            Me.InflateData()
            Me.txtDistributorID.Enabled = False
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "Distributor_GridEX1_ColumnButtonClick")
        End Try
    End Sub
    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Me.GridEX1.Row <= -1 Then
                Me.GridEX1.Refetch()
                Me.GridEX1.SelectCurrentCellText()
                e.Cancel = True
                Return
            End If
            If Not (e.Row.RowType = Janus.Windows.GridEX.RowType.Record) Then
                Return
            End If
            If Me.clsDitributor.HasReferencedChildData(Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()) = True Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                e.Cancel = True
                Me.GridEX1.Refetch()
                Me.GridEX1.SelectCurrentCellText()
                Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Me.GridEX1.Refetch()
                Me.GridEX1.SelectCurrentCellText()
                Return
            End If
            Cursor = Cursors.WaitCursor
            Me.clsDitributor.DeleteDistributor(Me.GridEX1.GetValue("DISTRIBUTOR_ID").ToString())
            Me.ShowMessageInfo(Me.MessageSuccesDelete)
            e.Cancel = False
            Me.ClearControl(Me.grpEdit)
            Me.GridEX1.MoveToNewRecord()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub grpEdit_VisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpEdit.VisibleChanged
        If Me.grpEdit.Visible = True Then
            Me.btnFilter.Enabled = False
        Else
            Me.btnFilter.Enabled = True
        End If
    End Sub

#End Region

    Private Sub GridEX1_CellEdited(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellEdited
        If Me.isLoadingRow Then : Return : End If

        If e.Column.Key = "INACTIVE" Then
            Dim valB As Boolean = Me.GridEX1.GetValue(e.Column)
            Try
                Cursor = Cursors.WaitCursor
                Me.clsDitributor.SetInactive(Me.GridEX1.GetValue("DISTRIBUTOR_ID"), valB)
            Catch ex As Exception
                Me.ShowMessageError(ex.Message)
                Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
            End Try
            Cursor = Cursors.Default
        End If
    End Sub
End Class
