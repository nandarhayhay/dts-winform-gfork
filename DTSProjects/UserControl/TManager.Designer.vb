<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TManager))
        Me.txtMaxRecord = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.btnGoPrevios = New Janus.Windows.EditControls.UIButton
        Me.btnGoFirst = New Janus.Windows.EditControls.UIButton
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.btnSearch = New System.Windows.Forms.Button
        Me.XpGradientPanel2 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.lblPosition = New System.Windows.Forms.Label
        Me.lblResult = New System.Windows.Forms.Label
        Me.btnGoLast = New Janus.Windows.EditControls.UIButton
        Me.btnNext = New Janus.Windows.EditControls.UIButton
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.grdpSearch = New Janus.Windows.EditControls.UIGroupBox
        Me.cbCategory = New System.Windows.Forms.ComboBox
        Me.cbPageSize = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XpGradientPanel2.SuspendLayout()
        CType(Me.grdpSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grdpSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtMaxRecord
        '
        Me.txtMaxRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMaxRecord.Location = New System.Drawing.Point(558, 23)
        Me.txtMaxRecord.Name = "txtMaxRecord"
        Me.txtMaxRecord.Size = New System.Drawing.Size(75, 20)
        Me.txtMaxRecord.TabIndex = 29
        Me.txtMaxRecord.Text = "1,000,000,000"
        Me.txtMaxRecord.Value = 1000000000
        Me.txtMaxRecord.ValueType = Janus.Windows.GridEX.NumericEditValueType.Int32
        Me.txtMaxRecord.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnGoPrevios
        '
        Me.btnGoPrevios.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnGoPrevios.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoPrevios.Location = New System.Drawing.Point(255, 7)
        Me.btnGoPrevios.Name = "btnGoPrevios"
        Me.btnGoPrevios.Size = New System.Drawing.Size(26, 23)
        Me.btnGoPrevios.TabIndex = 3
        Me.btnGoPrevios.Text = "<"
        Me.btnGoPrevios.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnGoFirst
        '
        Me.btnGoFirst.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnGoFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoFirst.Location = New System.Drawing.Point(222, 7)
        Me.btnGoFirst.Name = "btnGoFirst"
        Me.btnGoFirst.Size = New System.Drawing.Size(26, 23)
        Me.btnGoFirst.TabIndex = 2
        Me.btnGoFirst.Text = "<<"
        Me.btnGoFirst.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.GridEX1.Location = New System.Drawing.Point(0, 81)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(642, 333)
        Me.GridEX1.TabIndex = 26
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.ThemedAreas = Janus.Windows.GridEX.ThemedArea.None
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'btnSearch
        '
        Me.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Location = New System.Drawing.Point(61, 49)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 30
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'XpGradientPanel2
        '
        Me.XpGradientPanel2.Controls.Add(Me.lblPosition)
        Me.XpGradientPanel2.Controls.Add(Me.lblResult)
        Me.XpGradientPanel2.Controls.Add(Me.btnGoLast)
        Me.XpGradientPanel2.Controls.Add(Me.btnNext)
        Me.XpGradientPanel2.Controls.Add(Me.btnGoPrevios)
        Me.XpGradientPanel2.Controls.Add(Me.btnGoFirst)
        Me.XpGradientPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.XpGradientPanel2.EndColor = System.Drawing.SystemColors.MenuBar
        Me.XpGradientPanel2.Gradient = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.XpGradientPanel2.Location = New System.Drawing.Point(0, 414)
        Me.XpGradientPanel2.Name = "XpGradientPanel2"
        Me.XpGradientPanel2.Size = New System.Drawing.Size(642, 36)
        Me.XpGradientPanel2.StartColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.XpGradientPanel2.TabIndex = 25
        '
        'lblPosition
        '
        Me.lblPosition.BackColor = System.Drawing.Color.Black
        Me.lblPosition.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblPosition.Location = New System.Drawing.Point(354, 7)
        Me.lblPosition.Name = "lblPosition"
        Me.lblPosition.Size = New System.Drawing.Size(180, 23)
        Me.lblPosition.TabIndex = 7
        '
        'lblResult
        '
        Me.lblResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblResult.BackColor = System.Drawing.Color.Black
        Me.lblResult.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblResult.Location = New System.Drawing.Point(540, 7)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(93, 23)
        Me.lblResult.TabIndex = 6
        '
        'btnGoLast
        '
        Me.btnGoLast.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnGoLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoLast.Location = New System.Drawing.Point(320, 7)
        Me.btnGoLast.Name = "btnGoLast"
        Me.btnGoLast.Size = New System.Drawing.Size(26, 23)
        Me.btnGoLast.TabIndex = 5
        Me.btnGoLast.Text = ">>"
        Me.btnGoLast.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnNext
        '
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(289, 7)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(26, 23)
        Me.btnNext.TabIndex = 4
        Me.btnNext.Text = ">"
        Me.btnNext.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(61, 23)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(133, 20)
        Me.txtSearch.TabIndex = 24
        '
        'grdpSearch
        '
        Me.grdpSearch.Controls.Add(Me.cbCategory)
        Me.grdpSearch.Controls.Add(Me.cbPageSize)
        Me.grdpSearch.Controls.Add(Me.Label5)
        Me.grdpSearch.Controls.Add(Me.Label6)
        Me.grdpSearch.Controls.Add(Me.label4)
        Me.grdpSearch.Controls.Add(Me.label3)
        Me.grdpSearch.Controls.Add(Me.txtMaxRecord)
        Me.grdpSearch.Controls.Add(Me.txtSearch)
        Me.grdpSearch.Controls.Add(Me.btnSearch)
        Me.grdpSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.grdpSearch.Location = New System.Drawing.Point(0, 0)
        Me.grdpSearch.Name = "grdpSearch"
        Me.grdpSearch.Size = New System.Drawing.Size(642, 81)
        Me.grdpSearch.TabIndex = 31
        Me.grdpSearch.Text = "Search Data"
        Me.grdpSearch.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'cbCategory
        '
        Me.cbCategory.FormattingEnabled = True
        Me.cbCategory.Location = New System.Drawing.Point(225, 24)
        Me.cbCategory.Name = "cbCategory"
        Me.cbCategory.Size = New System.Drawing.Size(177, 21)
        Me.cbCategory.TabIndex = 36
        '
        'cbPageSize
        '
        Me.cbPageSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbPageSize.FormattingEnabled = True
        Me.cbPageSize.Items.AddRange(New Object() {"10", "20", "30", "40", "50", "100", "200", "300", "400", "500", "1000"})
        Me.cbPageSize.Location = New System.Drawing.Point(576, 53)
        Me.cbPageSize.Name = "cbPageSize"
        Me.cbPageSize.Size = New System.Drawing.Size(57, 21)
        Me.cbPageSize.TabIndex = 35
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(198, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(19, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "By"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Search"
        '
        'label4
        '
        Me.label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(496, 56)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(55, 13)
        Me.label4.TabIndex = 32
        Me.label4.Text = "Page Size"
        '
        'label3
        '
        Me.label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(489, 26)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(65, 13)
        Me.label3.TabIndex = 31
        Me.label3.Text = "Max Record"
        '
        'TManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.grdpSearch)
        Me.Controls.Add(Me.XpGradientPanel2)
        Me.Name = "TManager"
        Me.Size = New System.Drawing.Size(642, 450)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XpGradientPanel2.ResumeLayout(False)
        CType(Me.grdpSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grdpSearch.ResumeLayout(False)
        Me.grdpSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtMaxRecord As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents btnGoPrevios As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnGoFirst As Janus.Windows.EditControls.UIButton
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Private WithEvents XpGradientPanel2 As SteepValley.Windows.Forms.XPGradientPanel
    Friend WithEvents lblPosition As System.Windows.Forms.Label
    Friend WithEvents lblResult As System.Windows.Forms.Label
    Friend WithEvents btnGoLast As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnNext As Janus.Windows.EditControls.UIButton
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents grdpSearch As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cbPageSize As System.Windows.Forms.ComboBox

End Class
