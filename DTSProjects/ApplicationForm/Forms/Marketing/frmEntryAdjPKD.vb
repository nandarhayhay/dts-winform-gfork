Imports Nufarm.Domain
Imports NufarmBussinesRules.common
Imports NufarmBussinesRules.common.Helper
Imports NufarmBussinesRules.common.CommonClass

Public Class frmEntryAdjPKD
    Friend Mode As SaveMode = SaveMode.Insert
    Public Event SaveData(ByRef IAdj As Adjustment, ByRef OMode As SaveMode, ByRef Err As Object)
    Friend OAdj As Adjustment = Nothing 'hanya untuk Mode Update
    Private HasLoadForm As Boolean = False
    Private isLoadingCombo As Boolean = False
    Friend clsAdj As NufarmBussinesRules.Program.Addjustment
    Public Function hasChangedData() As Boolean
        If Not IsNothing(Me.OAdj) Then
            If Me.OAdj.BrandPackID <> Me.mcbBrandPack.Value.ToString() Then
                Return True
            End If
            If Me.OAdj.DistributorID <> Me.mcbDistributor.Value.ToString() Then
                Return True
            End If
            If Me.dtPicStartPeriode.Value.ToShortDateString() <> Convert.ToDateTime(Me.OAdj.StartDate).ToShortDateString() Then
                Return True
            End If
            If Me.dtPicEndPeriode.Value.ToShortDateString() <> Convert.ToDateTime(Me.OAdj.EndDate).ToShortDateString() Then
                Return True
            End If
            If Me.txtAdjusmentName.Text <> Me.OAdj.NameApp Then
                Return True
            End If
            Dim TypeApp As String = ""
            Select Case Me.cmbAdjustmentFor.Text
                Case "RETAILER PROGRAM"
                    TypeApp = "RP"
                Case "DPD"
                    TypeApp = "DPD"
            End Select
            If Me.OAdj.TypeApp <> TypeApp Then
                Return True
            End If
            If Me.OAdj.Quantity <> Convert.ToDecimal(Me.txtMaxQty.Value) Then
                Return True
            End If
        Else
            Return True
        End If

    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Not Me.IsValid() Then : Return : End If
        Me.Cursor = Cursors.WaitCursor
        Dim IAdj As New Adjustment()
        With IAdj
            .BrandPackID = Me.mcbBrandPack.Value.ToString()
           
            .CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
            .CreatedDate = NufarmBussinesRules.SharedClass.ServerDate
            .NameApp = Me.txtAdjusmentName.Text
            .EndDate = Convert.ToDateTime(Me.dtPicEndPeriode.Value.ToShortDateString())
            .Quantity = Convert.ToDecimal(Me.txtMaxQty.Value)
            .StartDate = Convert.ToDateTime(Me.dtPicStartPeriode.Value.ToShortDateString())
            Dim Periode As String = String.Format("{0:MM/dd/yyyy}-{1:MM/dd/yyyy}", .StartDate, .EndDate)

            If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
                .IDApp = Me.OAdj.IDApp
            End If

            Dim TypeApp As String = ""
            Select Case Me.cmbAdjustmentFor.Text
                Case "RETAILER PROGRAM"
                    TypeApp = "RP"
                Case "DPD"
                    TypeApp = "DPD"
            End Select
            .TypeApp = TypeApp
            Dim CodeApp As String = ""
            If Me.rdbPerDistributor.Checked Then
                .DistributorID = Me.mcbDistributor.Value.ToString()
                CodeApp = Periode & "|" & Me.mcbDistributor.Value.ToString() & "|" & Me.mcbBrandPack.Value.ToString() & "|" & .TypeApp
            Else
                Dim CodeGroupDistr As String = "PAN"
                If Me.Mode = SaveMode.Insert Then
                    If rdbGroupDistributor.Checked Then
                        CodeGroupDistr = getCodeGroupDistributor(Me.ChkDistributor.Text)
                        CodeApp = Periode & "|" & CodeGroupDistr & "|" & Me.mcbBrandPack.Value.ToString() & "|" & .TypeApp
                        Dim listDistributors As New List(Of String)
                        For i As Integer = 0 To Me.ChkDistributor.CheckedValues().Length - 1
                            listDistributors.Add(Me.ChkDistributor.CheckedValues().GetValue(i).ToString())
                        Next
                        .ListDistributors = listDistributors
                        .GroupCode = CodeGroupDistr
                    End If
                End If
            End If
            .IsGroup = IIf(Me.rdbGroupDistributor.Checked, True, False)
            .CodeApp = CodeApp
        End With
        If Not Me.hasChangedData() Then
            Return
        End If
        Dim err As Object = Nothing
        RaiseEvent SaveData(IAdj, Mode, err)
        If IsNothing(err) Then
            Me.OAdj = IAdj
        End If
        If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
            Me.Close()
        End If
        Me.Cursor = Cursors.Default
    End Sub
    Private Function IsValid()
        If (Me.dtPicStartPeriode.Value = Me.dtPicEndPeriode.Value) Then
            Me.baseTooltip.Show("Please enter valid periode", Me.GroupBox1, 2500) : Me.dtPicStartPeriode.Focus() : Return False
        End If
        If Me.txtAdjusmentName.Text = "" Then
            Me.baseTooltip.Show("Please enter adjustment description", Me.txtAdjusmentName, 2500) : Me.txtAdjusmentName.Focus() : Return False
        End If
        If Me.txtMaxQty.Value <= 0 Then
            Me.baseTooltip.Show("Please enter valid value", Me.txtMaxQty, 2500) : Me.txtMaxQty.Focus() : Return False
        End If
        'mcb branpack
        If Me.mcbBrandPack.Value Is Nothing Then
            Me.baseTooltip.Show("Please enter brandpack name", Me.mcbBrandPack, 2500) : Me.mcbBrandPack.Focus() : Return False
        ElseIf Me.mcbBrandPack.Text = "" Then
            Me.baseTooltip.Show("Please enter brandpack name", Me.mcbBrandPack, 2500) : Me.mcbBrandPack.Focus() : Return False
        End If
        'mcb distributor

        If Me.mcbBrandPack.Text = "" Then
            Me.baseTooltip.Show("Please enter brandpack name", Me.mcbDistributor, 2500) : Me.mcbDistributor.Focus() : Return False
        End If
        If Me.cmbAdjustmentFor.Text = "" Then
            Me.baseTooltip.Show("Please enter adjustment for ", Me.mcbDistributor, 2500) : Me.cmbAdjustmentFor.Focus() : Return False
        End If
        If Me.rdbGroupDistributor.Checked = False And Me.rdbPerDistributor.Checked = False Then
            Me.baseTooltip.Show("Please enter adjustment for ", rdbPerDistributor, 2500) : Me.cmbAdjustmentFor.Focus() : Return False
        End If
        If Me.rdbGroupDistributor.Checked Then
            If Me.Mode = SaveMode.Insert Then
                If IsNothing(Me.ChkDistributor.CheckedValues()) Then
                    Me.baseTooltip.Show("Please enter distributor", Me.ChkDistributor, 250) : Me.ChkDistributor.Focus() : Return False
                ElseIf Me.ChkDistributor.CheckedValues.Length <= 0 Then
                    Me.baseTooltip.Show("Please enter distributor", Me.ChkDistributor, 250) : Me.ChkDistributor.Focus() : Return False
                End If
            End If
        ElseIf Me.rdbPerDistributor.Checked Then
            If Me.mcbDistributor.Value Is Nothing Then
                Me.baseTooltip.Show("Please enter brandpack name", Me.mcbDistributor, 2500) : Me.mcbDistributor.Focus() : Return False
            End If
        End If
        Return True
    End Function
    Private Sub frmEntryAdjPKD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
            Me.mcbBrandPack.Value = Me.OAdj.BrandPackID
            Me.mcbDistributor.Value = Me.OAdj.DistributorID
            Me.cmbAdjustmentFor.Text = Me.OAdj.TypeApp
            Select Case Me.OAdj.TypeApp
                Case "RP"
                    Me.cmbAdjustmentFor.Text = "RETAILER PROGRAM"
                Case Else
                    Me.cmbAdjustmentFor.Text = "DPD"
            End Select
            Me.dtPicEndPeriode.Enabled = False
            Me.dtPicStartPeriode.Enabled = False

            Me.mcbBrandPack.ReadOnly = True
            Me.cmbAdjustmentFor.Enabled = False
            Me.btnSearchBrandPack.Enabled = False
            Me.btnSearchDistributor.Enabled = False
            Me.rdbGroupDistributor.Enabled = False
            Me.rdbPerDistributor.Enabled = False
            Me.ChkDistributor.CheckAll()
        End If
        Me.ChkDistributor.ReadOnly = False
        Me.mcbDistributor.ReadOnly = True
        Me.HasLoadForm = True
        'If Not IsNothing(Me.mcbBrandPack.DataSource) Then
        '    Me.mcbBrandPack.DropDownList().RetrieveStructure()
        'End If
        'If Not IsNothing(Me.mcbDistributor.DropDownList()) Then
        '    Me.mcbDistributor.DropDownList.RetrieveStructure()
        'End If
    End Sub

 
    Private Sub mcbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbDistributor.ValueChanged
        If Not Me.HasLoadForm Then
            Return
        End If
        If isLoadingCombo Then : Return : End If

        If Me.mcbDistributor.DataSource Is Nothing Then : Return : End If
        If Me.mcbDistributor.DropDownList.RecordCount <= 0 Then : Return : End If
        If Me.mcbDistributor.Text = "" Then : Return : End If
        If Me.mcbDistributor.Value Is Nothing Then : Return : End If
        If Me.mcbDistributor.SelectedIndex <= -1 Then : Return : End If
        ''get PKD branpadk by distributor
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim tblBrandPack As DataTable = Me.clsAdj.getBrandPack(Me.mcbDistributor.Value.ToString(), True)
            Me.isLoadingCombo = True
            Me.mcbBrandPack.SetDataBinding(tblBrandPack.DefaultView(), "")
            Me.isLoadingCombo = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.isLoadingCombo = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSearchBrandPack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchBrandPack.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim tblBrandPack As DataTable = Nothing
            If Me.ChkDistributor.Visible Then
                Dim listDistributors As New List(Of String)
                For i As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
                    listDistributors.Add(Me.ChkDistributor.CheckedValues.GetValue(i).ToString())
                Next
                tblBrandPack = Me.clsAdj.getBrandPack(listDistributors, Me.mcbBrandPack.Text.Trim())
                Me.isLoadingCombo = True
                Me.mcbBrandPack.SetDataBinding(tblBrandPack.DefaultView(), "")
            ElseIf Me.mcbDistributor.Visible Then
                tblBrandPack = Me.clsAdj.getBrandPack(Me.mcbDistributor.Value.ToString(), True, Me.mcbDistributor.Text.Trim())
                Me.isLoadingCombo = True
                Me.mcbBrandPack.SetDataBinding(tblBrandPack.DefaultView(), "")
            End If
            Me.ShowMessageInfo(tblBrandPack.Rows.Count.ToString() & " item() found")
            Me.isLoadingCombo = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.isLoadingCombo = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSearchDistributor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim Searchstring As String = ""
            If Me.mcbDistributor.Visible Then
                Searchstring = Me.mcbDistributor.Text.Trim()
            Else
                Searchstring = Me.ChkDistributor.Text.Trim()
            End If
            Dim tblDistributor As DataTable = Me.clsAdj.getDistributor(True, Searchstring)
            Me.isLoadingCombo = True
            If Me.ChkDistributor.Visible Then
                Me.ChkDistributor.SetDataBinding(tblDistributor.DefaultView(), "")
            ElseIf Me.mcbDistributor.Visible Then
                Me.mcbDistributor.SetDataBinding(tblDistributor.DefaultView(), "")
            End If
            Me.ShowMessageInfo(tblDistributor.Rows.Count.ToString() & " item() found")
            Me.mcbBrandPack.SetDataBinding(Nothing, "")
            Me.isLoadingCombo = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.isLoadingCombo = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub rdbPerDistributor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPerDistributor.CheckedChanged
        If isLoadingCombo Then : Return : End If
        If Not Me.HasLoadForm Then : Return : End If
        If rdbPerDistributor.Checked Then
            Me.ChkDistributor.Visible = False
            Me.ChkDistributor.ReadOnly = True

            Me.mcbDistributor.Visible = True
            Me.mcbDistributor.ReadOnly = False
        End If
    End Sub

    Private Sub rdbGroupDistributor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGroupDistributor.CheckedChanged
        If isLoadingCombo Then : Return : End If
        If Not Me.HasLoadForm Then : Return : End If
        If Me.rdbGroupDistributor.Checked Then
            Me.ChkDistributor.Visible = True
            Me.ChkDistributor.ReadOnly = False

            Me.mcbDistributor.Visible = False
            Me.mcbDistributor.ReadOnly = True

        End If
    End Sub

    Private Sub ChkDistributor_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkDistributor.CheckedValuesChanged
        If isLoadingCombo Then : Return : End If
        If Not Me.HasLoadForm Then : Return : End If
        If Me.ChkDistributor.CheckedValues Is Nothing Then
            Me.isLoadingCombo = True
            Me.mcbBrandPack.SetDataBinding(Nothing, "")
            Me.isLoadingCombo = False
            Return
        End If
        If Me.ChkDistributor.CheckedValues.Length <= 0 Then
            Me.isLoadingCombo = True
            Me.mcbBrandPack.SetDataBinding(Nothing, "")
            Me.isLoadingCombo = False
            Return
        End If
        Dim listDistributors As New List(Of String)
        For i As Integer = 0 To Me.ChkDistributor.CheckedValues.Length - 1
            listDistributors.Add(Me.ChkDistributor.CheckedValues.GetValue(i).ToString())
        Next
        Try
            Dim tblBrandPack As DataTable = Me.clsAdj.getBrandPack(listDistributors)
            Me.isLoadingCombo = True
            Me.mcbBrandPack.SetDataBinding(tblBrandPack.DefaultView(), "")
            Me.isLoadingCombo = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.isLoadingCombo = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class