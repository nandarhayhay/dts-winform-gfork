Public Class AchievementF
    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            'check reference data
            Me.Cursor = Cursors.WaitCursor
            Dim AchHeaderID As String = Me.GridEX1.GetValue("ACH_HEADER_ID").ToString()
            'delete data beserta anaknya
            Me.clsDPD.DeleteAchievementHeader(Me.GridEX1.GetValue("ACH_HEADER_ID").ToString())
            Dim DV As DataView = Me.DS.Tables(1).DefaultView()
            DV.Sort = "ACH_HEADER_ID"
            Dim Dr() As DataRowView = DV.FindRows(AchHeaderID)
            If Dr.Length > 0 Then
                For i As Integer = 0 To DV.Count - 1
                    Dim Index As Integer = DV.Find(AchHeaderID)
                    If Index <> -1 Then
                        DV(i).Delete()
                        i -= 1
                    Else
                        Exit For
                    End If
                Next
            End If
            e.Cancel = False
            CheckedFilter()
            Me.GridEX1.UpdateData()
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles chkFilter.CheckedChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.isLoadingRow = True
            CheckedFilter()
            Me.isLoadingRow = False
        Catch ex As Exception
            Me.isLoadingRow = False
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub HideColumnsPBOrCP(ByVal Grid As Janus.Windows.GridEX.GridEX, ByVal cols As List(Of String), ByVal isVisible As Boolean)
        Select Case Grid.Name
            Case "GridEX1"
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                    If col.Caption.Contains("LEFT_PERIODE_") Or col.Caption.Contains("TOTAL_LEFT_") Then
                        If cols.Contains(col.Caption) Then
                            col.Visible = isVisible
                        Else
                            col.Visible = Not isVisible
                        End If
                    End If
                Next
            Case "GridEX2"
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX2.RootTable.Columns
                    If col.Caption.Contains("LEFT_PERIODE_") Then
                        If cols.Contains(col.Caption) Then
                            col.Visible = isVisible
                        Else
                            col.Visible = Not isVisible
                        End If
                    End If
                Next
        End Select
    End Sub
    Private Sub getDS(ByVal Sprog As StatusProgress)
        Try
            Select Case Sprog
                Case StatusProgress.LoadingAchiement
                    If Me.Flag = "" Then
                        Me.SP = StatusProgress.None : Me.Cursor = Cursors.Default
                        MessageBox.Show("System can not view data when flag is not defined", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                    ''check if Disc has been Applied by (Volume/Value

                    If (Not IsNothing(Me.mcbDistributor.Value)) And (Not IsNothing(Me.chkDistributors.CheckedValues)) Then
                        If Me.chkDistributors.CheckedValues.Length > 0 Then
                            Me.DS = Me.clsDPD.getAccrued(Me.Flag, Me.mcbDistributor.Value.ToString(), ListAgreementNo)
                        Else
                            Me.DS = Me.clsDPD.getAccrued(Me.Flag, Me.mcbDistributor.Value.ToString())
                        End If
                    ElseIf Not IsNothing(Me.chkDistributors.CheckedValues) Then
                        If Me.chkDistributors.CheckedValues.Length > 0 Then
                            For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
                                If Not ListAgreementNo.Contains(Me.chkDistributors.CheckedValues.GetValue(i).ToString()) Then
                                    ListAgreementNo.Add(Me.chkDistributors.CheckedValues.GetValue(i).ToString())
                                End If
                            Next
                            Me.DS = Me.clsDPD.getAccrued(Me.Flag, , ListAgreementNo)
                        End If
                    ElseIf (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                        Me.DS = Me.clsDPD.getAccrued(Me.Flag, Me.mcbDistributor.Value.ToString())
                    Else
                        Me.DS = Me.clsDPD.getAccrued(Me.Flag, True)
                    End If
                Case StatusProgress.ProcessingDisc
                    Dim DVAgreement As DataView = Nothing 'DirectCast(Me.chkDistributors.DropDownDataSource, DataView).ToTable()
                    Dim DVAgreeOri As DataView = Nothing
                    If (Not IsNothing(Me.mcbDistributor.Value) And Me.mcbDistributor.SelectedIndex <> -1) And _
                        (Not IsNothing(Me.chkDistributors.CheckedValues)) Then
                        If Me.chkDistributors.CheckedValues.Length > 0 Then
                            DVAgreement = DirectCast(Me.chkDistributors.DropDownDataSource, DataView).ToTable.Copy().DefaultView
                            DVAgreeOri = DirectCast(Me.chkDistributors.DropDownDataSource, DataView).ToTable.Copy().DefaultView
                            DVAgreement.Table.Clear() : DVAgreement.Sort = "AGREEMENT_NO"
                            DVAgreeOri.Sort = "AGREEMENT_NO"
                            Dim DrV As DataRowView = Nothing
                            For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
                                Dim AgreementNo = Me.chkDistributors.CheckedValues.GetValue(i)
                                Dim Index As Integer = DVAgreement.Find(AgreementNo)
                                If Index = -1 Then
                                    DrV = DVAgreement.AddNew()
                                    Dim IndexOri As Integer = DVAgreeOri.Find(AgreementNo)
                                    DrV("AGREEMENT_NO") = AgreementNo
                                    DrV("START_DATE") = DVAgreeOri(IndexOri)("START_DATE")
                                    DrV("END_DATE") = DVAgreeOri(IndexOri)("END_DATE")
                                    DrV.EndEdit()
                                End If
                            Next
                            Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, Me.mcbDistributor.Value.ToString(), DVAgreement.ToTable())
                        Else
                            Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, Me.mcbDistributor.Value.ToString())
                        End If
                    ElseIf (Not IsNothing(Me.mcbDistributor.Value)) And (Me.mcbDistributor.SelectedIndex <> -1) Then
                        Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, Me.mcbDistributor.Value.ToString())
                    ElseIf Not IsNothing(Me.chkDistributors.CheckedValues) Then
                        If Me.chkDistributors.CheckedValues.Length > 0 Then
                            DVAgreement = DirectCast(Me.chkDistributors.DropDownDataSource, DataView).ToTable.Copy().DefaultView
                            DVAgreeOri = DirectCast(Me.chkDistributors.DropDownDataSource, DataView).ToTable.Copy().DefaultView
                            DVAgreement.Table.Clear() : DVAgreement.Sort = "AGREEMENT_NO"
                            DVAgreeOri.Sort = "AGREEMENT_NO"
                            Dim DrV As DataRowView = Nothing
                            For i As Integer = 0 To Me.chkDistributors.CheckedValues.Length - 1
                                Dim AgreementNo = Me.chkDistributors.CheckedValues.GetValue(i)
                                Dim Index As Integer = DVAgreement.Find(AgreementNo)
                                If Index = -1 Then
                                    DrV = DVAgreement.AddNew()
                                    Dim IndexOri As Integer = DVAgreeOri.Find(AgreementNo)
                                    DrV("AGREEMENT_NO") = AgreementNo
                                    DrV("START_DATE") = DVAgreeOri(IndexOri)("START_DATE")
                                    DrV("END_DATE") = DVAgreeOri(IndexOri)("END_DATE")
                                    DrV.EndEdit()
                                End If
                            Next
                            Me.DS = Me.clsTA.CalculateAccrue(Me.Flag, , DVAgreement.ToTable())
                        Else
                            Me.DS = Me.clsTA.CalculateAccrue(Me.Flag)
                        End If
                    ElseIf Me.Flag <> "" Then
                        Me.DS = Me.clsTA.CalculateAccrue(Me.Flag)
                    End If
            End Select
        Catch ex As Exception
            'If Me.SP = StatusProgress.ProcessingAcrrue Then : Me.EnabledFlag("") : End If
            Me.HasLoadReport = True : Me.SP = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False : Me.TickCount = 0
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_getDS") : Me.Cursor = Cursors.Default
            Me.LAF = LoadAchievementFrom.None
            'Finally
            '    Me.SP = StatusProgress.None
        End Try
    End Sub
    Private Sub BindGrid()
        If Not Me.isLoadingRow Then : Me.isLoadingRow = True : End If
        If Not IsNothing(Me.DS) Then
            Me.GridEX1.SetDataBinding(Me.DS.Tables("ACHIEVEMENT_HEADER").DefaultView, "")
            Me.GridEX1.DropDowns("BRAND").SetDataBinding(Me.DS.Tables("T_BRAND").DefaultView(), "")
            Me.GridEX2.SetDataBinding(Me.DS.Tables("ACHIEVEMENT_DETAIL").DefaultView(), "")
            If Me.chkFilter.Checked Then
                Me.isLoadingRow = False
                Me.GridEX1_CurrentCellChanged(Me.GridEX1, New EventArgs())
            End If
        Else
            Me.GridEX1.SetDataBinding(Nothing, "") : Me.GridEX2.SetDataBinding(Nothing, "")
        End If
        'Dim listCols As New List(Of String)
        'Dim ListColsCopy As New List(Of String)
        ''LEFT_PREVIOUS_Q1,LEFT_CURRENT_Q2,LEFT_PREVIOUS_Q3,LEFT_CURRENT_Q3,LEFT_CURRENT_F1,LEFT_CURRENT_F2,LEFT_PREVIOUS_F3
        'Select Case Me.Flag
        '    Case "F1" : listCols.AddRange(New String() {"LEFT_PREVIOUS_Q3", "LEFT_CURRENT_Q2", "LEFT_CURRENT_Q3", "LEFT_PREVIOUS_F3"})
        '        ListColsCopy.AddRange(New String() {"LEFT_PREVIOUS_Q3", "LEFT_CURRENT_Q2", "LEFT_CURRENT_Q3", "LEFT_PREVIOUS_F3"})
        '    Case "F2" : listCols.AddRange(New String() {"LEFT_CURRENT_Q2", "LEFT_CURRENT_Q3", "LEFT_CURRENT_F1"})
        '        ListColsCopy.AddRange(New String() {"LEFT_CURRENT_Q2", "LEFT_CURRENT_Q3", "LEFT_CURRENT_F1"})
        '    Case "F3" : listCols.AddRange(New String() {"LEFT_CURRENT_Q3", "LEFT_CURRENT_F2"})
        '        ListColsCopy.AddRange(New String() {"LEFT_PERIODE_Q1", "TOTAL_LEFT_Q1", "LEFT_PERIODE_Q2", "TOTAL_LEFT_Q2"})
        '    Case "Q4" : listCols.AddRange(New String() {"LEFT_PERIODE_Q2", "TOTAL_LEFT_Q2", "LEFT_PERIODE_CUR_Q3", "TOTAL_LEFT_CUR_Q3"})
        '        ListColsCopy.AddRange(New String() {"LEFT_PERIODE_Q2", "TOTAL_LEFT_Q2", "LEFT_PERIODE_CUR_Q3", "TOTAL_LEFT_CUR_Q3"})
        '    Case "S1" : listCols.AddRange(New String() {"LEFT_PERIODE_S2", "TOTAL_LEFT_S2", "LEFT_PERIODE_Q3_BEFORE", "TOTAL_LEFT_Q3_BEFORE", "LEFT_PERIODE_Q4", "TOTAL_LEFT_Q4"})
        '        ListColsCopy.AddRange(New String() {"LEFT_PERIODE_S2", "TOTAL_LEFT_S2", "LEFT_PERIODE_Q3_BEFORE", "TOTAL_LEFT_Q3_BEFORE", "LEFT_PERIODE_Q4", "TOTAL_LEFT_Q4"})
        '    Case "S2" : listCols.AddRange(New String() {"LEFT_PERIODE_S1", "TOTAL_LEFT_S1"})
        '        ListColsCopy.AddRange(New String() {"LEFT_PERIODE_S1", "TOTAL_LEFT_S1"})
        'End Select
        'TOTAL_PBQ1, TOTAL_PBQ2, TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3
        'Me.HideColumnsPBOrCP(Me.GridEX1, listCols, True)
        'For Each colTotal As String In ListColsCopy
        '    If colTotal.Contains("TOTAL_LEFT") Then
        '        listCols.Remove(colTotal)
        '    End If
        'Next
        'Me.HideColumnsPBOrCP(Me.GridEX2, listCols, True)
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.isAwaiting Then
            Return
        End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.Timer1.Stop()
            Me.Timer1.Enabled = False

            If Me.SP = StatusProgress.LoadingAchiement Then ''DS di peroleh di procedure ApplyDiscByVolume
            Else
                Me.getDS(Me.SP)
            End If
            Me.HasLoadReport = True : Me.BindGrid()
            Me.SP = StatusProgress.None
            If Me.clsDPD.MessageError <> "" Then
                Me.ShowMessageInfo(Me.clsTA.MessageError)
            End If
            Me.OtherUserProcessing = ""
            Me.SP = StatusProgress.None
            If Me.isLoadingRow Then : isLoadingRow = False : End If
        Catch ex As Exception
            Me.SP = StatusProgress.None : Me.Timer1.Stop() : Me.Timer1.Enabled = False
            Me.OtherUserProcessing = ""
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AchievementF_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not IsNothing(Me.DS) Then
            Me.isLoadingRow = True
            Me.DS.Dispose()
            'Me.clsTA.Dispose()
            Me.clsDPD.Dispose()
        End If
    End Sub

    Private Sub AchievementF_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AddHandler Timer1.Tick, AddressOf ChekTimer
        Me.GridEX1_Enter(Me.GridEX1, New EventArgs())
        Me.IsHasLoadedForm = True
        Me.isLoadingCombo = False
        Me.EnabledFlag("")
        checkEnabledFlagValue()
        Me.ReadAcces()
        CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
        Me.ExpandableSplitter2.Expanded = False
        'For i As Integer = 0 To Me.GridEX1.RootTable.Columns.Count - 1
        '    Me.ListDefaultVisibleColumns.Add(Me.GridEX1.RootTable.Columns(i).Key)
        'Next
    End Sub

    Private Sub GridEX2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX2.Enter
        Me.Grid = Me.GridEX2
        Me.Panel1.BorderStyle = BorderStyle.Fixed3D
        Me.Panel2.BorderStyle = BorderStyle.None
        Me.SelectGrid = SelectedGrid.DetailDPD
        Dim B As Boolean = Me.FilterEditor1.Visible
        Me.FilterEditor1.Visible = B
        Me.FilterEditor1.SourceControl = Me.Grid
        Me.FilterEditor1.SortFieldList = False
    End Sub

    Private Sub GridEX1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.Enter
        Me.Grid = Me.GridEX1
        Me.Panel1.BorderStyle = BorderStyle.Fixed3D
        Me.Panel2.BorderStyle = BorderStyle.None
        Me.SelectGrid = SelectedGrid.Header
        Dim B As Boolean = Me.FilterEditor1.Visible
        Me.FilterEditor1.Visible = B
        Me.FilterEditor1.SourceControl = Me.Grid
        Me.FilterEditor1.SortFieldList = False
    End Sub
End Class
