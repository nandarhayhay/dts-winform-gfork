<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataViewRowState
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
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.btnModifiedAdded = New DevComponents.DotNetBar.ButtonItem
        Me.btnModifiedOriginal = New DevComponents.DotNetBar.ButtonItem
        Me.btnDelete = New DevComponents.DotNetBar.ButtonItem
        Me.btnCurrent = New DevComponents.DotNetBar.ButtonItem
        Me.btnUnchaigned = New DevComponents.DotNetBar.ButtonItem
        Me.btnOriginalRows = New DevComponents.DotNetBar.ButtonItem
        Me.grpViewMode = New System.Windows.Forms.GroupBox
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpViewMode.SuspendLayout()
        Me.SuspendLayout()
        '
        'Bar1
        '
        Me.Bar1.AccessibleDescription = "Bar1 (Bar1)"
        Me.Bar1.AccessibleName = "Bar1"
        Me.Bar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar1.BarType = DevComponents.DotNetBar.eBarType.MenuBar
        Me.Bar1.DockSide = DevComponents.DotNetBar.eDockSide.Bottom
        Me.Bar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnModifiedAdded, Me.btnModifiedOriginal, Me.btnDelete, Me.btnCurrent, Me.btnUnchaigned, Me.btnOriginalRows})
        Me.Bar1.Location = New System.Drawing.Point(7, 15)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(393, 25)
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar1.TabIndex = 0
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'btnModifiedAdded
        '
        Me.btnModifiedAdded.Name = "btnModifiedAdded"
        Me.btnModifiedAdded.Text = "ModifiedAdded"
        '
        'btnModifiedOriginal
        '
        Me.btnModifiedOriginal.Name = "btnModifiedOriginal"
        Me.btnModifiedOriginal.Text = "ModifiedOriginal"
        '
        'btnDelete
        '
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Text = "Deleted"
        '
        'btnCurrent
        '
        Me.btnCurrent.Name = "btnCurrent"
        Me.btnCurrent.Text = "Current"
        '
        'btnUnchaigned
        '
        Me.btnUnchaigned.Name = "btnUnchaigned"
        Me.btnUnchaigned.Text = "Unchaigned"
        '
        'btnOriginalRows
        '
        Me.btnOriginalRows.Name = "btnOriginalRows"
        Me.btnOriginalRows.Text = "OriginalRows"
        '
        'grpViewMode
        '
        Me.grpViewMode.Controls.Add(Me.Bar1)
        Me.grpViewMode.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpViewMode.Location = New System.Drawing.Point(0, 7)
        Me.grpViewMode.Name = "grpViewMode"
        Me.grpViewMode.Size = New System.Drawing.Size(404, 44)
        Me.grpViewMode.TabIndex = 1
        Me.grpViewMode.TabStop = False
        Me.grpViewMode.Text = " Data View Mode"
        '
        'DataViewRowState
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpViewMode)
        Me.Name = "DataViewRowState"
        Me.Size = New System.Drawing.Size(404, 51)
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpViewMode.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Friend WithEvents btnModifiedAdded As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnModifiedOriginal As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDelete As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCurrent As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnUnchaigned As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents grpViewMode As System.Windows.Forms.GroupBox
    Friend WithEvents btnOriginalRows As DevComponents.DotNetBar.ButtonItem

End Class
