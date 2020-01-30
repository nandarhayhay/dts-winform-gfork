<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Filter
    Inherits DevComponents.DotNetBar.Balloon

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Filter))
        Me.grpColumn = New System.Windows.Forms.GroupBox
        Me.cmbColumn = New System.Windows.Forms.ComboBox
        Me.grpCriteria = New System.Windows.Forms.GroupBox
        Me.cmbFilter = New System.Windows.Forms.ComboBox
        Me.grpDataNumeric = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtValue2 = New System.Windows.Forms.TextBox
        Me.grpValue1 = New System.Windows.Forms.GroupBox
        Me.txtValue1 = New System.Windows.Forms.TextBox
        Me.grpTime = New System.Windows.Forms.GroupBox
        Me.grpDateUntil = New System.Windows.Forms.GroupBox
        Me.dtPicUntil = New System.Windows.Forms.DateTimePicker
        Me.grpDateFrom = New System.Windows.Forms.GroupBox
        Me.dtPicFrom = New System.Windows.Forms.DateTimePicker
        Me.btnFilter = New Janus.Windows.EditControls.UIButton
        Me.grpColumn.SuspendLayout()
        Me.grpCriteria.SuspendLayout()
        Me.grpDataNumeric.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpValue1.SuspendLayout()
        Me.grpTime.SuspendLayout()
        Me.grpDateUntil.SuspendLayout()
        Me.grpDateFrom.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpColumn
        '
        Me.grpColumn.BackColor = System.Drawing.Color.Transparent
        Me.grpColumn.Controls.Add(Me.cmbColumn)
        Me.grpColumn.Location = New System.Drawing.Point(12, 60)
        Me.grpColumn.Name = "grpColumn"
        Me.grpColumn.Size = New System.Drawing.Size(148, 35)
        Me.grpColumn.TabIndex = 0
        Me.grpColumn.TabStop = False
        Me.grpColumn.Text = "Column"
        '
        'cmbColumn
        '
        Me.cmbColumn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbColumn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbColumn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColumn.FormattingEnabled = True
        Me.cmbColumn.Location = New System.Drawing.Point(3, 16)
        Me.cmbColumn.Name = "cmbColumn"
        Me.cmbColumn.Size = New System.Drawing.Size(142, 21)
        Me.cmbColumn.TabIndex = 0
        '
        'grpCriteria
        '
        Me.grpCriteria.BackColor = System.Drawing.Color.Transparent
        Me.grpCriteria.Controls.Add(Me.cmbFilter)
        Me.grpCriteria.Location = New System.Drawing.Point(170, 60)
        Me.grpCriteria.Name = "grpCriteria"
        Me.grpCriteria.Size = New System.Drawing.Size(148, 35)
        Me.grpCriteria.TabIndex = 1
        Me.grpCriteria.TabStop = False
        Me.grpCriteria.Text = "filter Criteria"
        '
        'cmbFilter
        '
        Me.cmbFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbFilter.Enabled = False
        Me.cmbFilter.FormattingEnabled = True
        Me.cmbFilter.Location = New System.Drawing.Point(3, 16)
        Me.cmbFilter.Name = "cmbFilter"
        Me.cmbFilter.Size = New System.Drawing.Size(142, 21)
        Me.cmbFilter.TabIndex = 0
        '
        'grpDataNumeric
        '
        Me.grpDataNumeric.BackColor = System.Drawing.Color.Transparent
        Me.grpDataNumeric.Controls.Add(Me.GroupBox1)
        Me.grpDataNumeric.Controls.Add(Me.grpValue1)
        Me.grpDataNumeric.Location = New System.Drawing.Point(12, 101)
        Me.grpDataNumeric.Name = "grpDataNumeric"
        Me.grpDataNumeric.Size = New System.Drawing.Size(306, 58)
        Me.grpDataNumeric.TabIndex = 2
        Me.grpDataNumeric.TabStop = False
        Me.grpDataNumeric.Text = "Text / Numeric Data to Filter"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtValue2)
        Me.GroupBox1.Location = New System.Drawing.Point(151, 18)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(149, 33)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Value 1"
        '
        'txtValue2
        '
        Me.txtValue2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtValue2.Location = New System.Drawing.Point(3, 16)
        Me.txtValue2.Name = "txtValue2"
        Me.txtValue2.Size = New System.Drawing.Size(143, 20)
        Me.txtValue2.TabIndex = 1
        '
        'grpValue1
        '
        Me.grpValue1.Controls.Add(Me.txtValue1)
        Me.grpValue1.Location = New System.Drawing.Point(6, 19)
        Me.grpValue1.Name = "grpValue1"
        Me.grpValue1.Size = New System.Drawing.Size(139, 33)
        Me.grpValue1.TabIndex = 0
        Me.grpValue1.TabStop = False
        Me.grpValue1.Text = "Value 1"
        '
        'txtValue1
        '
        Me.txtValue1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtValue1.Location = New System.Drawing.Point(3, 16)
        Me.txtValue1.Name = "txtValue1"
        Me.txtValue1.Size = New System.Drawing.Size(133, 20)
        Me.txtValue1.TabIndex = 1
        '
        'grpTime
        '
        Me.grpTime.BackColor = System.Drawing.Color.Transparent
        Me.grpTime.Controls.Add(Me.grpDateUntil)
        Me.grpTime.Controls.Add(Me.grpDateFrom)
        Me.grpTime.Enabled = False
        Me.grpTime.Location = New System.Drawing.Point(12, 165)
        Me.grpTime.Name = "grpTime"
        Me.grpTime.Size = New System.Drawing.Size(306, 64)
        Me.grpTime.TabIndex = 3
        Me.grpTime.TabStop = False
        Me.grpTime.Text = "Date time Data to Filter"
        '
        'grpDateUntil
        '
        Me.grpDateUntil.Controls.Add(Me.dtPicUntil)
        Me.grpDateUntil.Location = New System.Drawing.Point(151, 19)
        Me.grpDateUntil.Name = "grpDateUntil"
        Me.grpDateUntil.Size = New System.Drawing.Size(149, 39)
        Me.grpDateUntil.TabIndex = 1
        Me.grpDateUntil.TabStop = False
        Me.grpDateUntil.Text = "Until"
        '
        'dtPicUntil
        '
        Me.dtPicUntil.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtPicUntil.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtPicUntil.Location = New System.Drawing.Point(3, 16)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.Size = New System.Drawing.Size(143, 20)
        Me.dtPicUntil.TabIndex = 0
        '
        'grpDateFrom
        '
        Me.grpDateFrom.Controls.Add(Me.dtPicFrom)
        Me.grpDateFrom.Location = New System.Drawing.Point(6, 19)
        Me.grpDateFrom.Name = "grpDateFrom"
        Me.grpDateFrom.Size = New System.Drawing.Size(139, 39)
        Me.grpDateFrom.TabIndex = 0
        Me.grpDateFrom.TabStop = False
        Me.grpDateFrom.Text = "From"
        '
        'dtPicFrom
        '
        Me.dtPicFrom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtPicFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtPicFrom.Location = New System.Drawing.Point(3, 16)
        Me.dtPicFrom.Name = "dtPicFrom"
        Me.dtPicFrom.Size = New System.Drawing.Size(133, 20)
        Me.dtPicFrom.TabIndex = 0
        '
        'btnFilter
        '
        Me.btnFilter.Location = New System.Drawing.Point(238, 235)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Office2007ColorScheme = Janus.Windows.UI.Office2007ColorScheme.Silver
        Me.btnFilter.Size = New System.Drawing.Size(80, 19)
        Me.btnFilter.TabIndex = 4
        Me.btnFilter.Text = "Show Filter"
        Me.btnFilter.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Filter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Salmon
        Me.BackColor2 = System.Drawing.Color.SandyBrown
        Me.CaptionColor = System.Drawing.SystemColors.Info
        Me.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CaptionIcon = CType(resources.GetObject("$this.CaptionIcon"), System.Drawing.Icon)
        Me.CaptionText = "Customize The Grid Filter"
        Me.ClientSize = New System.Drawing.Size(330, 264)
        Me.Controls.Add(Me.btnFilter)
        Me.Controls.Add(Me.grpTime)
        Me.Controls.Add(Me.grpDataNumeric)
        Me.Controls.Add(Me.grpCriteria)
        Me.Controls.Add(Me.grpColumn)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "Filter"
        Me.grpColumn.ResumeLayout(False)
        Me.grpCriteria.ResumeLayout(False)
        Me.grpDataNumeric.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpValue1.ResumeLayout(False)
        Me.grpValue1.PerformLayout()
        Me.grpTime.ResumeLayout(False)
        Me.grpDateUntil.ResumeLayout(False)
        Me.grpDateFrom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpColumn As System.Windows.Forms.GroupBox
    Private WithEvents grpCriteria As System.Windows.Forms.GroupBox
    Private WithEvents grpDataNumeric As System.Windows.Forms.GroupBox
    Private WithEvents grpTime As System.Windows.Forms.GroupBox
    Private WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtValue2 As System.Windows.Forms.TextBox
    Private WithEvents grpValue1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtValue1 As System.Windows.Forms.TextBox
    Friend WithEvents cmbColumn As System.Windows.Forms.ComboBox
    Private WithEvents grpDateUntil As System.Windows.Forms.GroupBox
    Private WithEvents dtPicUntil As System.Windows.Forms.DateTimePicker
    Private WithEvents grpDateFrom As System.Windows.Forms.GroupBox
    Private WithEvents dtPicFrom As System.Windows.Forms.DateTimePicker
    Private WithEvents cmbFilter As System.Windows.Forms.ComboBox
    Private WithEvents btnFilter As Janus.Windows.EditControls.UIButton
End Class
