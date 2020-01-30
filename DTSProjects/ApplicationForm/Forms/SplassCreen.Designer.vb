<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplassCreen
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SplassCreen))
        Me.XpWatermark1 = New SteepValley.Windows.Forms.XPWatermark
        Me.SuspendLayout()
        '
        'XpWatermark1
        '
        Me.XpWatermark1.BackColor = System.Drawing.Color.Transparent
        Me.XpWatermark1.BackgroundImage = CType(resources.GetObject("XpWatermark1.BackgroundImage"), System.Drawing.Image)
        Me.XpWatermark1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XpWatermark1.Gamma = 1.0!
        Me.XpWatermark1.Location = New System.Drawing.Point(0, 0)
        Me.XpWatermark1.Name = "XpWatermark1"
        Me.XpWatermark1.Opacity = 1.0!
        Me.XpWatermark1.Size = New System.Drawing.Size(471, 287)
        Me.XpWatermark1.TabIndex = 0
        Me.XpWatermark1.TransparentColor.High = System.Drawing.Color.Black
        Me.XpWatermark1.TransparentColor.Low = System.Drawing.Color.Empty
        '
        'SplassCreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(471, 287)
        Me.ControlBox = False
        Me.Controls.Add(Me.XpWatermark1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SplassCreen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents XpWatermark1 As SteepValley.Windows.Forms.XPWatermark

End Class
