<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DefinePeriode
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
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.btnAplyRange = New Janus.Windows.EditControls.UIButton
        Me.dtPicFilterEnd = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicFilterStart = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.PanelEx1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.btnAplyRange)
        Me.PanelEx1.Controls.Add(Me.dtPicFilterEnd)
        Me.PanelEx1.Controls.Add(Me.dtPicFilterStart)
        Me.PanelEx1.Controls.Add(Me.Label5)
        Me.PanelEx1.Controls.Add(Me.Label1)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(501, 46)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
        '
        'btnAplyRange
        '
        Me.btnAplyRange.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAplyRange.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnAplyRange.Location = New System.Drawing.Point(418, 14)
        Me.btnAplyRange.Name = "btnAplyRange"
        Me.btnAplyRange.Size = New System.Drawing.Size(70, 20)
        Me.btnAplyRange.TabIndex = 10
        Me.btnAplyRange.Text = "Apply filter"
        Me.btnAplyRange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'dtPicFilterEnd
        '
        Me.dtPicFilterEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFilterEnd.CustomFormat = "dd MMMM yyyy"
        Me.dtPicFilterEnd.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicFilterEnd.DropDownCalendar.Name = ""
        Me.dtPicFilterEnd.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFilterEnd.Location = New System.Drawing.Point(249, 13)
        Me.dtPicFilterEnd.Name = "dtPicFilterEnd"
        Me.dtPicFilterEnd.ShowTodayButton = False
        Me.dtPicFilterEnd.Size = New System.Drawing.Size(157, 20)
        Me.dtPicFilterEnd.TabIndex = 9
        Me.dtPicFilterEnd.Value = New Date(2014, 1, 7, 0, 0, 0, 0)
        Me.dtPicFilterEnd.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicFilterStart
        '
        Me.dtPicFilterStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFilterStart.CustomFormat = "dd MMMM yyyy"
        Me.dtPicFilterStart.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicFilterStart.DropDownCalendar.Name = ""
        Me.dtPicFilterStart.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFilterStart.Location = New System.Drawing.Point(39, 12)
        Me.dtPicFilterStart.Name = "dtPicFilterStart"
        Me.dtPicFilterStart.ShowTodayButton = False
        Me.dtPicFilterStart.Size = New System.Drawing.Size(157, 20)
        Me.dtPicFilterStart.TabIndex = 8
        Me.dtPicFilterStart.Value = New Date(2014, 1, 7, 0, 0, 0, 0)
        Me.dtPicFilterStart.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(211, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(26, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "End"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Start"
        '
        'DefinePeriode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(501, 46)
        Me.Controls.Add(Me.PanelEx1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "DefinePeriode"
        Me.ShowIcon = False
        Me.Text = "Define Periode"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAplyRange As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtPicFilterEnd As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicFilterStart As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
End Class
