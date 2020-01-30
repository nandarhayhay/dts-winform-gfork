<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WeeklySalesReport
    Inherits DTSProjects.BaseForm

    'Form overrides dispose to clean up the component list.
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
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WeeklySalesReport))
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.grpSalesDate = New Janus.Windows.EditControls.UIGroupBox
        Me.btnFilteDate = New Janus.Windows.EditControls.UIButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpSalesDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSalesDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridEX1
        '
        Me.GridEX1.DataSource = Me.GridEX1.Layouts
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.Location = New System.Drawing.Point(0, 68)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(884, 475)
        Me.GridEX1.TabIndex = 8
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.UpdateOnLeave = False
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'grpSalesDate
        '
        Me.grpSalesDate.Controls.Add(Me.btnFilteDate)
        Me.grpSalesDate.Controls.Add(Me.Label2)
        Me.grpSalesDate.Controls.Add(Me.Label1)
        Me.grpSalesDate.Controls.Add(Me.DateTimePicker2)
        Me.grpSalesDate.Controls.Add(Me.DateTimePicker1)
        Me.grpSalesDate.Location = New System.Drawing.Point(93, 3)
        Me.grpSalesDate.Name = "grpSalesDate"
        Me.grpSalesDate.Size = New System.Drawing.Size(626, 41)
        Me.grpSalesDate.TabIndex = 0
        Me.grpSalesDate.Text = "Filter Sales Date"
        Me.grpSalesDate.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnFilteDate
        '
        Me.btnFilteDate.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnFilteDate.ImageIndex = 3
        Me.btnFilteDate.Location = New System.Drawing.Point(525, 14)
        Me.btnFilteDate.Name = "btnFilteDate"
        Me.btnFilteDate.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnFilteDate.Size = New System.Drawing.Size(85, 20)
        Me.btnFilteDate.TabIndex = 11
        Me.btnFilteDate.Text = "Apply Filter"
        Me.btnFilteDate.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(269, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Until"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "From"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(307, 14)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker2.TabIndex = 1
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(57, 14)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'WeeklySalesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(884, 543)
        Me.Controls.Add(Me.grpSalesDate)
        Me.Controls.Add(Me.GridEX1)
        Me.Name = "WeeklySalesReport"
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpSalesDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSalesDate.ResumeLayout(False)
        Me.grpSalesDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents grpSalesDate As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnFilteDate As Janus.Windows.EditControls.UIButton

End Class
