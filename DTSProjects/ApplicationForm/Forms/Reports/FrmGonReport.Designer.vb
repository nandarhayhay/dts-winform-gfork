<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGonReport
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
        Me.crvGON = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'crvGON
        '
        Me.crvGON.ActiveViewIndex = -1
        Me.crvGON.AutoScroll = True
        Me.crvGON.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvGON.DisplayGroupTree = False
        Me.crvGON.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvGON.Location = New System.Drawing.Point(0, 0)
        Me.crvGON.Name = "crvGON"
        Me.crvGON.SelectionFormula = ""
        Me.crvGON.ShowGroupTreeButton = False
        Me.crvGON.Size = New System.Drawing.Size(797, 466)
        Me.crvGON.TabIndex = 1
        Me.crvGON.ViewTimeSelectionFormula = ""
        '
        'FrmGonReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(797, 466)
        Me.Controls.Add(Me.crvGON)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.Name = "FrmGonReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PRINTING GON"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crvGON As CrystalDecisions.Windows.Forms.CrystalReportViewer

End Class
