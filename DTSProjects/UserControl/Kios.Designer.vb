<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Kios
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
        Me.TManager1 = New DTSProjects.TManager
        Me.SuspendLayout()
        '
        'TManager1
        '
        Me.TManager1.AutoSize = True
        Me.TManager1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TManager1.Location = New System.Drawing.Point(0, 0)
        Me.TManager1.Name = "TManager1"
        Me.TManager1.Size = New System.Drawing.Size(656, 455)
        Me.TManager1.TabIndex = 0
        '
        'Kios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TManager1)
        Me.Name = "Kios"
        Me.Size = New System.Drawing.Size(656, 455)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TManager1 As DTSProjects.TManager

End Class
