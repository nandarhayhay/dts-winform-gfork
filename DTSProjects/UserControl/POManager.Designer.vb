<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class POManager
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
        Me.Manager1 = New DTSProjects.Manager
        Me.SuspendLayout()
        '
        'Manager1
        '
        Me.Manager1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Manager1.Location = New System.Drawing.Point(0, 0)
        Me.Manager1.Name = "Manager1"
        Me.Manager1.Size = New System.Drawing.Size(730, 473)
        Me.Manager1.TabIndex = 0
        '
        'POManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Manager1)
        Me.Name = "POManager"
        Me.Size = New System.Drawing.Size(730, 473)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Manager1 As DTSProjects.Manager

End Class
