<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintOA
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PrintOA))
        Me.crvOA = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'crvOA
        '
        Me.crvOA.ActiveViewIndex = -1
        Me.crvOA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvOA.DisplayGroupTree = False
        Me.crvOA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvOA.Location = New System.Drawing.Point(0, 0)
        Me.crvOA.Name = "crvOA"
        Me.crvOA.SelectionFormula = ""
        Me.crvOA.ShowGroupTreeButton = False
        Me.crvOA.Size = New System.Drawing.Size(979, 683)
        Me.crvOA.TabIndex = 0
        Me.crvOA.ViewTimeSelectionFormula = ""
        '
        'PrintOA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(979, 683)
        Me.Controls.Add(Me.crvOA)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = True
        Me.Name = "PrintOA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PRINTING OA"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents crvOA As CrystalDecisions.Windows.Forms.CrystalReportViewer

End Class
