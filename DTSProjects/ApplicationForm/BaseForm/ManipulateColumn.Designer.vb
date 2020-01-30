<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManipulateColumn
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManipulateColumn))
        Me.grpColumn = New System.Windows.Forms.GroupBox
        Me.cmbColumn = New System.Windows.Forms.ComboBox
        Me.grpRename = New System.Windows.Forms.GroupBox
        Me.txtManipulate = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grpColumn.SuspendLayout()
        Me.grpRename.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpColumn
        '
        Me.grpColumn.BackColor = System.Drawing.Color.Transparent
        Me.grpColumn.Controls.Add(Me.cmbColumn)
        Me.grpColumn.Location = New System.Drawing.Point(20, 50)
        Me.grpColumn.Name = "grpColumn"
        Me.grpColumn.Size = New System.Drawing.Size(208, 36)
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
        Me.cmbColumn.Size = New System.Drawing.Size(202, 21)
        Me.cmbColumn.TabIndex = 0
        '
        'grpRename
        '
        Me.grpRename.BackColor = System.Drawing.Color.Transparent
        Me.grpRename.Controls.Add(Me.txtManipulate)
        Me.grpRename.Location = New System.Drawing.Point(20, 92)
        Me.grpRename.Name = "grpRename"
        Me.grpRename.Size = New System.Drawing.Size(208, 36)
        Me.grpRename.TabIndex = 1
        Me.grpRename.TabStop = False
        Me.grpRename.Text = "Rename Column"
        '
        'txtManipulate
        '
        Me.txtManipulate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtManipulate.Enabled = False
        Me.txtManipulate.Location = New System.Drawing.Point(3, 16)
        Me.txtManipulate.Name = "txtManipulate"
        Me.txtManipulate.Size = New System.Drawing.Size(202, 20)
        Me.txtManipulate.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.ImageIndex = 0
        Me.Button1.ImageList = Me.ImageList1
        Me.Button1.Location = New System.Drawing.Point(168, 137)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(60, 22)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "&OK"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "CK.ico.ico")
        '
        'ManipulateColumn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Salmon
        Me.BackColor2 = System.Drawing.Color.SandyBrown
        Me.CaptionColor = System.Drawing.SystemColors.Info
        Me.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CaptionText = "Manpulate column"
        Me.ClientSize = New System.Drawing.Size(244, 163)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.grpRename)
        Me.Controls.Add(Me.grpColumn)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "ManipulateColumn"
        Me.grpColumn.ResumeLayout(False)
        Me.grpRename.ResumeLayout(False)
        Me.grpRename.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpColumn As System.Windows.Forms.GroupBox
    Private WithEvents grpRename As System.Windows.Forms.GroupBox
    Private WithEvents Button1 As System.Windows.Forms.Button
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents cmbColumn As System.Windows.Forms.ComboBox
    Private WithEvents txtManipulate As System.Windows.Forms.TextBox
End Class
