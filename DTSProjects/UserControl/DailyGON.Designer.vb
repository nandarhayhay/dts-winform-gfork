<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DailyGON
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DailyGON))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.grpRangeDate = New Janus.Windows.EditControls.UIGroupBox
        Me.cbDate = New System.Windows.Forms.ComboBox
        Me.cbMonth = New System.Windows.Forms.ComboBox
        Me.cbYear = New System.Windows.Forms.ComboBox
        Me.lblDate = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnApplyRange = New Janus.Windows.EditControls.UIButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRangeDate.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpRangeDate
        '
        Me.grpRangeDate.BackColor = System.Drawing.Color.Transparent
        Me.grpRangeDate.Controls.Add(Me.cbDate)
        Me.grpRangeDate.Controls.Add(Me.cbMonth)
        Me.grpRangeDate.Controls.Add(Me.cbYear)
        Me.grpRangeDate.Controls.Add(Me.lblDate)
        Me.grpRangeDate.Controls.Add(Me.Label2)
        Me.grpRangeDate.Controls.Add(Me.btnApplyRange)
        Me.grpRangeDate.Controls.Add(Me.Label1)
        Me.grpRangeDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpRangeDate.Location = New System.Drawing.Point(0, 0)
        Me.grpRangeDate.Name = "grpRangeDate"
        Me.grpRangeDate.Size = New System.Drawing.Size(966, 47)
        Me.grpRangeDate.TabIndex = 4
        Me.grpRangeDate.Text = "Range Data"
        Me.grpRangeDate.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'cbDate
        '
        Me.cbDate.FormattingEnabled = True
        Me.cbDate.Location = New System.Drawing.Point(362, 19)
        Me.cbDate.Name = "cbDate"
        Me.cbDate.Size = New System.Drawing.Size(71, 21)
        Me.cbDate.TabIndex = 14
        '
        'cbMonth
        '
        Me.cbMonth.FormattingEnabled = True
        Me.cbMonth.Location = New System.Drawing.Point(193, 19)
        Me.cbMonth.Name = "cbMonth"
        Me.cbMonth.Size = New System.Drawing.Size(126, 21)
        Me.cbMonth.TabIndex = 13
        '
        'cbYear
        '
        Me.cbYear.FormattingEnabled = True
        Me.cbYear.Items.AddRange(New Object() {"2028", "2027", "2026", "2025", "2024", "2023", "2022", "2021", "2018", "2017", "2015", "2014"})
        Me.cbYear.Location = New System.Drawing.Point(51, 19)
        Me.cbYear.Name = "cbYear"
        Me.cbYear.Size = New System.Drawing.Size(93, 21)
        Me.cbYear.TabIndex = 12
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblDate.Location = New System.Drawing.Point(326, 23)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 13)
        Me.lblDate.TabIndex = 11
        Me.lblDate.Text = "Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(150, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Month"
        '
        'btnApplyRange
        '
        Me.btnApplyRange.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnApplyRange.Icon = CType(resources.GetObject("btnApplyRange.Icon"), System.Drawing.Icon)
        Me.btnApplyRange.ImageIndex = 3
        Me.btnApplyRange.Location = New System.Drawing.Point(458, 20)
        Me.btnApplyRange.Name = "btnApplyRange"
        Me.btnApplyRange.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnApplyRange.Size = New System.Drawing.Size(80, 20)
        Me.btnApplyRange.TabIndex = 9
        Me.btnApplyRange.Text = "Apply Filter"
        Me.btnApplyRange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(16, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Year"
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.Location = New System.Drawing.Point(0, 47)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Size = New System.Drawing.Size(966, 458)
        Me.GridEX1.TabIndex = 5
        Me.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'DailyGON
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.grpRangeDate)
        Me.Name = "DailyGON"
        Me.Size = New System.Drawing.Size(966, 505)
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRangeDate.ResumeLayout(False)
        Me.grpRangeDate.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpRangeDate As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnApplyRange As Janus.Windows.EditControls.UIButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbDate As System.Windows.Forms.ComboBox
    Friend WithEvents cbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents cbYear As System.Windows.Forms.ComboBox
    Private WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX

End Class
