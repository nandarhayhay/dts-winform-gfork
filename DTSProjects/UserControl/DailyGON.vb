Imports System.Globalization
Public Class DailyGON
    Private IsLoadingRow As Boolean = False
    Private clsGon As New NufarmBussinesRules.OrderAcceptance.SPPBGONManager()
    Private DVMConversiProduct As DataView = Nothing
    'Friend WithEvents frmParentSPPB As SPPB = Nothing
    Dim HasGridReload As Boolean = False
    Friend ReportType As String = "Sales_Distributor"
    Private Sub bindGrid(ByVal dv As DataView)
        Me.GridEX1.SetDataBinding(dv, "")
        Dim mustReLoadGrid As Boolean = False

        If Not IsNothing(dv) Then
            If Me.GridEX1.DataSource Is Nothing Then
                mustReLoadGrid = True
            ElseIf Me.GridEX1.RecordCount <= 0 Then
                mustReLoadGrid = True
            End If
        End If
        If mustReLoadGrid Or (Not HasGridReload) Then
            Me.GridEX1.RetrieveStructure()

            For Each col As Janus.Windows.GridEX.GridEXColumn In GridEX1.RootTable.Columns
                If col.Type Is Type.GetType("System.Int32") Then
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    If col.Key.Contains("IDApp") Then
                        col.Visible = False : col.FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                        col.ShowInFieldChooser = False
                    End If
                ElseIf col.Type Is Type.GetType("System.Decimal") Then
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.FormatString = "#,##0.0000"
                    If col.Key = "UNIT1" Or col.Key = "VOL1" Or col.Key = "UNIT2" Or col.Key = "VOL2" Or col.Key = "GON_QTY" Then
                        col.Visible = False
                    End If
                End If
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                If col.Type Is Type.GetType("System.DateTime") Then
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
                    col.FormatString = "dd MMMM yyyy"
                Else
                    col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
                    col.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
                End If
                If col.Key = "UnitOfMeasure" Or col.Key = "UNIT1" Or col.Key = "VOL1" Or col.Key = "UNIT2" Or col.Key = "VOL2" Or col.Key = "GON_QTY" Or col.Key.Contains("Modified") Or col.Key.Contains("Created") Then
                    col.Visible = False
                End If
            Next
            ''SPPB number - GON_number
            If GridEX1.RootTable.Groups.Count > 0 Then
                If GridEX1.RootTable.Groups("SPPB_NO") Is Nothing Then
                    Dim grpSPPB As New Janus.Windows.GridEX.GridEXGroup()
                    With grpSPPB
                        .Column = GridEX1.RootTable.Columns("SPPB_NO")
                        .AllowRemove = True
                    End With
                    GridEX1.RootTable.Groups.Add(grpSPPB)
                End If
            Else
                If GridEX1.RootTable.Groups("SPPB_NO") Is Nothing Then
                    Dim grpSPPB As New Janus.Windows.GridEX.GridEXGroup()
                    With grpSPPB
                        .Column = GridEX1.RootTable.Columns("SPPB_NO")
                        .AllowRemove = True
                    End With
                    GridEX1.RootTable.Groups.Add(grpSPPB)
                End If
            End If
            Me.HasGridReload = True
        End If
        GridEX1.ExpandGroups()
        Me.GridEX1.AutoSizeColumns()
    End Sub
    Private Sub LoadData(ByVal tbl As DataTable)
        Dim info As New CultureInfo("id-ID")

        'VAR_DIST_ADDRESS
        'Dim Address As String = tbl.Rows(0)("ADDRESS").ToString()
        'Dim DistributorName As String = tbl.Rows(0)("DISTRIBUTOR_NAME").ToString()
        'POREF_NO_AND_DATE
        'Dim PORefNO As String = tbl.Rows(0)("PO_REF_NO"), PoRefDate As Date = tbl.Rows(0)("PO_REF_DATE")
        ''SPPB_NO_AND_DATE
        'Dim SppbNo As String = tbl.Rows(0)("SPPB_NO"), SppbDate As Date = tbl.Rows(0)("SPPB_DATE")
        ''VAR_GON_DATE_STR
        'Dim gonDate As Date = tbl.Rows(0)("GON_DATE")
        For i As Integer = 0 To tbl.Rows.Count - 1
            Dim row As DataRow = tbl.Rows(i)
            row.BeginEdit()
            'row("VAR_DIST_ADDRESS") = String.Format("{0}" & vbCrLf & "{1}", DistributorName, Address)
            'row("POREF_NO_AND_DATE") = String.Format(info, "{0} - {1:dd/MM/yyyy}", PORefNO, PoRefDate)
            'row("SPPB_NO_AND_DATE") = String.Format(info, "{0} - {1:dd/MM/yyyy}", SppbNo, SppbDate)
            'row("VAR_GON_DATE_STR") = String.Format(info, "Date : {0:dd/MM/yyyy}", gonDate)
            'Dim BrandPackName As String = row("BRANDPACK_NAME")
            Dim GonQty As Decimal = Convert.ToDecimal(row("GON_QTY"))
            Dim oUOM As Object = row("UnitOfMeasure")
            Dim oVol1 As Object = row("VOL1"), oVol2 As Object = row("VOL2")
            Dim oUnit1 As Object = row("UNIT1"), oUnit2 As Object = row("UNIT2")
            Dim ValidData As Boolean = True
            If oUOM Is Nothing Or oUOM Is DBNull.Value Then
                Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & tbl.Rows(0)("BRANDPACK_ID") & "'"
                If Me.DVMConversiProduct.Count <= 0 Then
                    'MessageBox.Show(BrandPackName & ", Unit of Measure has not been set yet")
                    ValidData = False
                    oUOM = ""
                Else
                    oUOM = Me.DVMConversiProduct(0)("UnitOfMeasure")
                End If
            End If
            If oVol1 Is Nothing Or oVol2 Is DBNull.Value Then
                Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & tbl.Rows(0)("BRANDPACK_ID") & "'"
                If Me.DVMConversiProduct.Count <= 0 Then
                    'MessageBox.Show(BrandPackName & ", colly for Volume 1 has not been set yet")
                    ValidData = False
                Else
                    oVol1 = Me.DVMConversiProduct(0)("VOL1")
                End If
            End If
            If oVol2 Is Nothing Or oVol2 Is DBNull.Value Then
                Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & tbl.Rows(0)("BRANDPACK_ID") & "'"
                If Me.DVMConversiProduct.Count <= 0 Then
                    'MessageBox.Show(BrandPackName & ", colly for Volume 2 has not been set yet")
                    ValidData = False
                Else
                    oVol2 = Me.DVMConversiProduct(0)("VOL2")
                End If
            End If
            If oUnit1 Is Nothing Or oUnit1 Is DBNull.Value Then
                Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & tbl.Rows(0)("BRANDPACK_ID") & "'"
                If Me.DVMConversiProduct.Count <= 0 Then
                    'MessageBox.Show(BrandPackName & ", colly for unit 1 has not been set yet")
                    ValidData = False
                Else
                    oUnit1 = Me.DVMConversiProduct(0)("UNIT1")
                End If
            End If
            If oUnit2 Is Nothing Or oUnit2 Is DBNull.Value Then
                Me.DVMConversiProduct.RowFilter = "BRANDPACK_ID = '" & tbl.Rows(0)("BRANDPACK_ID") & "'"
                If Me.DVMConversiProduct.Count <= 0 Then
                    'MessageBox.Show(BrandPackName & ", colly for unit 1 has not been set yet")
                    ValidData = False
                Else
                    oUnit2 = Me.DVMConversiProduct(0)("UNIT2")
                End If
            End If

            If ValidData Then
                Dim Dvol1 As Decimal = Convert.ToDecimal(oVol1), DVol2 As Decimal = Convert.ToDecimal(oVol2)
                If Dvol1 > 0 Then
                    Dim strUnit1 As String = CStr(oUnit1), strUnit2 As String = CStr(oUnit2)
                    Dim col1 As Integer = 0
                    Dim collyBox As String = "", collyPackSize As String = ""
                    If GonQty >= Dvol1 Then
                        col1 = Convert.ToInt32(Decimal.Truncate(GonQty / Dvol1))
                        collyBox = IIf(col1 <= 0, "", String.Format("{0:g} {1}", col1, strUnit1))
                        Dim lqty As Decimal = GonQty Mod Dvol1
                        Dim ilqty As Integer = 0
                        If lqty > 0 Then
                            'Dim c As Decimal = Decimal.Remainder(GonQty, Dvol1)
                            ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                            'ElseIf lqty > 0 And lqty < 1 Then
                            '    'ilqty = ilqty + DVol2
                            '    ilqty = Convert.ToInt32((lqty / Dvol1) * DVol2)
                        End If
                        collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                    ElseIf GonQty > 0 Then ''gon kurang dari 1 coly
                        Dim ilqty As Integer = Convert.ToInt32((GonQty / Dvol1) * DVol2)
                        collyPackSize = IIf(ilqty <= 0, "", String.Format("{0:g} " & strUnit2, ilqty))
                    End If
                    row("COLLY_BOX") = collyBox
                    row("COLLY_PACKSIZE") = collyPackSize
                Else
                    row("COLLY_BOX") = "Unknown colly"
                    row("COLLY_PACKSIZE") = "Unknown colly"
                End If
            Else
                row("COLLY_BOX") = "Unknown colly"
                row("COLLY_PACKSIZE") = "Unknown colly"
            End If
            row("QUANTITY") = String.Format(info, "{0:#,##0.000} {1}", GonQty, oUOM.ToString())
            'row("BATCH_NO") = tbl.Rows(i)("BATCH_NO")
            row.EndEdit()
        Next
    End Sub
    Private Sub btnApplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyRange.Click
        If Me.cbYear.SelectedIndex <= -1 Or Me.cbMonth.SelectedIndex <= -1 Then
            MessageBox.Show("Please enter Year/Month")
            Return
        End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim MonthName As String = Me.cbMonth.Items(Me.cbMonth.SelectedIndex)
            Dim monthNumber = DateTime.ParseExact(MonthName, "MMMM", CultureInfo.CurrentCulture).Month
            Dim DateNumber As Object = Nothing
            If Me.cbDate.SelectedIndex <> -1 Then
                DateNumber = Me.cbDate.Items(Me.cbDate.SelectedIndex)
            End If

            Dim tbl As DataTable = Me.clsGon.getDailyGonReport(Me.cbYear.Items(Me.cbYear.SelectedIndex), monthNumber, DateNumber)
            Me.LoadData(tbl)
            Me.bindGrid(tbl.DefaultView())
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Private Sub ComboBox1_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedValueChanged
    '    Dim month As DateTime = Convert.ToDateTime("1/1/2000")
    '    For i As Integer = 0 To 11
    '        Dim NextMont As DateTime = month.AddMonths(i)
    '        Dim list As New ListItem()
    '        list.Text = NextMont.ToString("MMMM")
    '        list.Value = NextMont.Month.ToString()
    '        MyddlMonthList.Items.Add(list)
    '    Next
    'End Sub

    Private Sub DailyGON_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.IsLoadingRow = True
        Dim Month As Date = Convert.ToDateTime(DateTime.Now().ToShortDateString())
        For i As Integer = 0 To 11
            Dim NextMonth As DateTime = Month.AddMonths(i)
            Me.cbMonth.Items.Add(MonthName(NextMonth.Month))
            ''Dim list As New ListItem()
            ''list.Text = NextMont.ToString("MMMM")
            ''list.Value = NextMont.Month.ToString()
            ''MyddlMonthList.Items.Add(list)
        Next
        Me.cbYear.SelectedIndex = Me.cbYear.Items.IndexOf(DateTime.Now.Year.ToString())
        'Me.cbYear.SelectedText = DateTime.Now.Year.ToString()
        Me.cbMonth.SelectedIndex = Me.cbMonth.Items.IndexOf(MonthName(DateTime.Now.Month).ToString())
        'Me.cbMonth.SelectedText = MonthName(DateTime.Now.Month).ToString()
        Dim monthNumber = DateTime.ParseExact(MonthName(DateTime.Now.Month).ToString(), "MMMM", CultureInfo.CurrentCulture).Month()
        'get days by month selected
        Dim daysInmonth As Integer = DateTime.DaysInMonth(Me.cbYear.Items(Me.cbYear.SelectedIndex).ToString(), monthNumber)

        For i As Integer = 0 To daysInmonth - 1
            Me.cbDate.Items.Add(i + 1)
        Next
        'get data convertion
        Using clsProd As New NufarmBussinesRules.Brandpack.BrandPack
            Me.DVMConversiProduct = clsProd.getProdConvertion(True).DefaultView()
        End Using
        Me.IsLoadingRow = False
    End Sub

    Private Sub cbMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMonth.SelectedIndexChanged
        If Me.IsLoadingRow Then : Return : End If
        If Me.cbMonth.SelectedValue Is Nothing Then : Return : End If
        If Me.cbMonth.SelectedText = "" Then : Return : End If
        If Me.cbMonth.SelectedIndex = -1 Then : Return : End If
        Me.IsLoadingRow = True
        Me.cbDate.Items.Clear()
        Dim daysInmonth As Integer = DateTime.DaysInMonth(Me.cbYear.SelectedValue, Me.cbMonth.SelectedValue)

        For i As Integer = 0 To daysInmonth - 1
            Me.cbDate.Items.Add(i)
        Next
        Me.cbDate.Text = ""
        Me.cbDate.SelectedIndex = -1
        Me.IsLoadingRow = False
    End Sub
End Class
