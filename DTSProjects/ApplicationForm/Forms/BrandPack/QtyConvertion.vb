Imports System.Data
Imports System.Globalization
Public Class QtyConvertion
    Private m_clsBrandPack As NufarmBussinesRules.Brandpack.BrandPack
    Private isLoadingRow As Boolean = False
    Private ReadOnly Property clsBrandPack() As NufarmBussinesRules.Brandpack.BrandPack
        Get
            If m_clsBrandPack Is Nothing Then
                m_clsBrandPack = New NufarmBussinesRules.Brandpack.BrandPack()
            End If
            Return m_clsBrandPack
        End Get
    End Property
    Private m_clsOther As NufarmBussinesRules.OrderAcceptance.SeparatedGON
    Private ReadOnly Property clsOther() As NufarmBussinesRules.OrderAcceptance.SeparatedGON
        Get
            If m_clsOther Is Nothing Then
                m_clsOther = New NufarmBussinesRules.OrderAcceptance.SeparatedGON()
            End If
            Return m_clsOther
        End Get
    End Property

    Private DVBrandPack As DataView = Nothing
    Private DVMConversiProduct As DataView = Nothing
    Private m_DVResult As DataView = Nothing
    Private Sub LoadBrandPackData()
        DVBrandPack = Me.clsBrandPack.getActiveBrandPack(False).DefaultView()
        DVMConversiProduct = Me.clsOther.getProdConvertion(True)
        'bind ke mcb
        Me.mcbBrandPack.SetDataBinding(DVBrandPack, "")
    End Sub
    Private Sub BindGrid()
        If Me.m_DVResult Is Nothing Then
            Me.grdMeasure.SetDataBinding(Nothing, "")
        End If
        If Not IsNothing(Me.grdMeasure.DataSource) Then
            Me.grdMeasure.Refetch()
        Else
            Me.grdMeasure.SetDataBinding(Me.m_DVResult, "")
        End If
        Me.grdMeasure.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.True
        Me.grdMeasure.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
        Me.grdMeasure.TableHeaderFormatStyle.ForeColor = Color.Maroon
    End Sub
    Private ReadOnly Property CreateOrRecreateResult(Optional ByVal replaceNew As Boolean = False) As DataView
        Get
            Dim tblResult As New DataTable("CONVERTION_RESULT")
            If replaceNew Or Me.m_DVResult Is Nothing Then
                With tblResult
                    .Columns.Add(New DataColumn("BRANDPACK_ID", Type.GetType("System.String")))
                    .Columns.Add(New DataColumn("ITEM_DESCRIPTIONS", Type.GetType("System.String")))
                    .Columns.Add(New DataColumn("UNIT_QUANTITY", Type.GetType("System.String")))
                    .Columns.Add(New DataColumn("COLLY_BOX", Type.GetType("System.String")))
                    .Columns.Add(New DataColumn("COLLY_PACKSIZE", Type.GetType("System.String")))
                End With
                Me.m_DVResult = tblResult.DefaultView()
            End If
            Return Me.m_DVResult
        End Get
    End Property

    Private Overloads Sub ClearControl()
        Me.txtCollyBox.Text = ""
        Me.txtCollyPackSize.Text = ""
        Me.txtQtyToCalculate.Value = 0
        Me.txtUnitOfMeasure.Text = ""
    End Sub
    Private Sub txtQtyToCalculate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQtyToCalculate.ValueChanged
        If isLoadingRow Then : Return : End If
        Me.txtCollyBox.Text = ""
        Me.txtCollyPackSize.Text = ""
        Dim QTY As Decimal = Me.txtQtyToCalculate.Value
        If Me.txtQtyToCalculate.Value > 0 Then
            Me.Cursor = Cursors.WaitCursor
            Dim BrandPackID As String = Me.mcbBrandPack.Value.ToString()
            Dim BrandPackName As String = Me.mcbBrandPack.Text.Trim()
            ''cek apakah yang di input bisa di bagi kemasan
            Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & BrandPackID & "'"
            Dim oUOM As Object = Nothing
            Dim oVol1 As Object = Nothing, oVol2 As Object = Nothing
            Dim oUnit1 As Object = Nothing, oUnit2 As Object = Nothing
            Dim Devqty As Object = Nothing
            If Me.DVMConversiProduct.Count > 0 Then
                oUOM = DVMConversiProduct(0)("UnitOfMeasure")
                oVol1 = DVMConversiProduct(0)("VOL1") : oVol2 = DVMConversiProduct(0)("VOL2")
                oUnit1 = DVMConversiProduct(0)("UNIT1") : oUnit2 = DVMConversiProduct(0)("UNIT2")
                Devqty = DVMConversiProduct(0)("DEVIDED_QUANTITY")
                Dim strFulVall As String = Me.txtQtyToCalculate.Value.ToString()
                Dim strFullValMinOne As String = strFulVall.Substring(0, strFulVall.Length)
                If oUOM Is Nothing Or oUOM Is DBNull.Value Then
                    Me.ShowMessageError(BrandPackName & ", Unit of Measure has not been set yet")
                    Me.txtUnitOfMeasure.Text = ""
                    Me.isLoadingRow = True
                    Me.txtQtyToCalculate.Value = strFullValMinOne
                    Me.isLoadingRow = False
                    Me.Cursor = Cursors.Default
                    Return
                ElseIf oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                    Me.ShowMessageError(BrandPackName & ", colly for Volume 1 has not been set yet")
                    Me.txtUnitOfMeasure.Text = ""
                    Me.isLoadingRow = True
                    Me.txtQtyToCalculate.Value = strFullValMinOne
                    Me.isLoadingRow = False
                    Me.Cursor = Cursors.Default
                    Return
                ElseIf oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                    Me.ShowMessageError(BrandPackName & ", colly for Volume 2 has not been set yet")
                    Me.txtUnitOfMeasure.Text = ""
                    Me.isLoadingRow = True
                    Me.txtQtyToCalculate.Value = strFullValMinOne
                    Me.isLoadingRow = False
                    Me.Cursor = Cursors.Default
                    Return
                ElseIf oUnit1 Is Nothing Or oUnit2 Is DBNull.Value Then
                    Me.ShowMessageError(BrandPackName & ", colly for Unit 1 has not been set yet")
                    Me.txtUnitOfMeasure.Text = ""
                    Me.isLoadingRow = True
                    Me.txtQtyToCalculate.Value = strFullValMinOne
                    Me.isLoadingRow = False
                    Me.Cursor = Cursors.Default
                    Return
                ElseIf oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                    Me.ShowMessageError(BrandPackName & ", colly for Unit 2 has not been set yet")
                    Me.txtUnitOfMeasure.Text = ""
                    Me.isLoadingRow = True
                    Me.txtQtyToCalculate.Value = strFullValMinOne
                    Me.isLoadingRow = False
                    Me.Cursor = Cursors.Default
                    Return
                End If
                Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                Dim col1 As Integer = 0
                Dim collyBox As String = "", collyPackSize As String = ""
                Dim ilqty As Decimal = 0
                If QTY >= Dvol1 Then
                ElseIf QTY > 0 Then
                    ilqty = (QTY / Dvol1) * DVol2
                    If CInt(ilqty) < 1 Then
                        Me.txtQtyToCalculate.SelectAll()
                        Me.txtQtyToCalculate.Focus()
                        'Me.baseTooltip.Show("please enter valid quantity", Me.txtQtyToCalculate)
                        Me.isLoadingRow = False
                        Me.Cursor = Cursors.Default
                        Return
                    End If
                End If
                If QTY >= Dvol1 Then
                    col1 = Convert.ToInt32(Decimal.Truncate(QTY / Dvol1))
                    collyBox = IIf(col1 <= 0, "", String.Format("{0:g} {1}", col1, strUnit1))
                    Dim lqty As Decimal = QTY Mod Dvol1
                    'Dim ilqty As Integer = 0
                    If lqty >= 1 Then
                        'Dim c As Decimal = Decimal.Remainder(GonQty, Dvol1)
                        ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                    ElseIf lqty > 0 And lqty < 1 Then
                        'ilqty = ilqty + DVol2
                        ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                    End If
                    collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                ElseIf QTY > 0 Then ''gon kurang dari 1 coly
                    ilqty = Convert.ToInt32((QTY / Dvol1) * DVol2)
                    collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                End If
                'enter txt
                Me.txtCollyBox.Text = collyBox
                Me.txtCollyPackSize.Text = collyPackSize
                'masukan ke dataview
            Else
                Me.ShowMessageError("Convertion for " & BrandPackName & " could not be found in database")
                Me.txtQtyToCalculate.SelectAll()
                Me.txtQtyToCalculate.Focus()
                'Me.baseTooltip.Show("please enter valid quantity", Me.txtQtyToCalculate)
                Me.isLoadingRow = False
                Me.Cursor = Cursors.Default
                Return
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub ButtonSearch1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearch1.btnClick
        Me.Cursor = Cursors.WaitCursor
        Me.DVBrandPack.RowFilter = "BRANDPACK_NAME LIKE '%" & Me.mcbBrandPack.Text.Trim() & "%'"
        Me.ShowMessageInfo(Me.DVBrandPack.Count.ToString() & " Item(s) Found")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub mcbBrandPack_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbBrandPack.ValueChanged
        ''set UOM
        Try
            Me.isLoadingRow = True
            Me.ClearControl()
            If Not Me.mcbBrandPack.Value Is Nothing Then
                Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & Me.mcbBrandPack.Value.ToString() & "'"
                Me.txtUnitOfMeasure.Text = ""
                If DVMConversiProduct.Count > 0 Then
                    Me.txtUnitOfMeasure.Text = DVMConversiProduct(0)("UnitOfMeasure")
                End If
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
        Me.isLoadingRow = False
    End Sub

    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Me.Cursor = Cursors.WaitCursor
        Dim info As New CultureInfo("id-ID")
        Dim DV As DataView = CreateOrRecreateResult()
        Dim dvRow As DataRowView = DV.AddNew()
        With dvRow
            .BeginEdit()
            .Item("BRANDPACK_ID") = Me.mcbBrandPack.Value.ToString()
            .Item("ITEM_DESCRIPTIONS") = Me.mcbBrandPack.Text.ToString()
            .Item("UNIT_QUANTITY") = String.Format(info, "{0:#,##0.000} {1}", Me.txtQtyToCalculate.Value, Me.txtUnitOfMeasure.Text())
            .Item("COLLY_BOX") = Me.txtCollyBox.Text()
            .Item("COLLY_PACKSIZE") = Me.txtCollyPackSize.Text()
            .EndEdit()
        End With
        Me.BindGrid()
        Cursor = Cursors.Default
    End Sub
    Private Sub ExportToExcell()
        'If e.KeyCode = Keys.F7 Then

        'ElseIf e.KeyCode = Keys.Delete Then
        '    Me.isLoadingRow = True
        '    GridEX1.CancelCurrentEdit()
        '    Me.GridEX1.Delete()
        '    Me.isLoadingRow = False
        'End If
        Try
            Dim FE As New Janus.Windows.GridEX.Export.GridEXExporter()
            Me.Cursor = Cursors.WaitCursor
            FE.IncludeHeaders = True
            FE.SheetName = "CONVERTION_RESULT"
            FE.IncludeFormatStyle = False
            FE.IncludeExcelProcessingInstruction = True
            FE.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
            Dim SD As New SaveFileDialog()
            SD.OverwritePrompt = True
            SD.DefaultExt = ".xls"
            SD.Filter = "All Files|*.*"
            SD.RestoreDirectory = True
            SD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            If SD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Using FS As New System.IO.FileStream(SD.FileName, IO.FileMode.Create)
                    FE.GridEX = Me.grdMeasure
                    FE.Export(FS)
                    FS.Close()
                    MessageBox.Show("Data Exported to " & SD.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_KeyDown")
        End Try
    End Sub
    'export to excell
    Private Sub UiButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UiButton1.Click
        Me.ExportToExcell()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Not IsNothing(Me.m_DVResult) Then
                If Me.m_DVResult.Count > 0 Then
                    Me.Cursor = Cursors.WaitCursor
                    Dim frmReport As New FrmGonReport()
                    With frmReport
                        Dim sampleRep As New Sample_Con_Report()
                        sampleRep.SetDataSource(Me.m_DVResult.ToTable())
                        .ReportDoc = sampleRep
                        .crvGON.ReportSource = .ReportDoc
                        .crvGON.DisplayGroupTree = False
                        .isSingleReport = True
                        .ShowDialog(Me)
                    End With
                End If
            End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        If Not IsNothing(Me.m_DVResult) Then
            Me.m_DVResult.Dispose()
            Me.m_DVResult = Nothing
            Me.BindGrid()
        End If
        Me.txtCollyBox.Text = ""
        Me.txtCollyPackSize.Text = ""
        Me.txtUnitOfMeasure.Text = ""
        Me.txtQtyToCalculate.Value = 0
    End Sub

    Private Sub QtyConvertion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LoadBrandPackData()

    End Sub


    Private Sub grdMeasure_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdMeasure.KeyDown
        If e.KeyCode = Keys.F7 Then
            Me.ExportToExcell()
        End If
    End Sub
End Class
