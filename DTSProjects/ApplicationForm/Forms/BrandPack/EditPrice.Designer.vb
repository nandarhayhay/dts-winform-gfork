<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditPrice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditPrice))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPrice = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.dtPicStartDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnOK = New Janus.Windows.EditControls.UIButton
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtBrandPack = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Price"
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(49, 29)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(100, 20)
        Me.txtPrice.TabIndex = 1
        Me.txtPrice.Text = "0.00"
        Me.txtPrice.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'dtPicStartDate
        '
        Me.dtPicStartDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicStartDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicStartDate.DropDownCalendar.Name = ""
        Me.dtPicStartDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicStartDate.Location = New System.Drawing.Point(216, 30)
        Me.dtPicStartDate.Name = "dtPicStartDate"
        Me.dtPicStartDate.ShowTodayButton = False
        Me.dtPicStartDate.Size = New System.Drawing.Size(147, 20)
        Me.dtPicStartDate.TabIndex = 2
        Me.dtPicStartDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(155, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Start_Date"
        '
        'btnOK
        '
        Me.btnOK.Appearance = Janus.Windows.UI.Appearance.Normal
        Me.btnOK.Location = New System.Drawing.Point(298, 55)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(64, 23)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        Me.btnOK.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "BrandPack_Name"
        '
        'txtBrandPack
        '
        Me.txtBrandPack.BackColor = System.Drawing.SystemColors.Window
        Me.txtBrandPack.Location = New System.Drawing.Point(112, 6)
        Me.txtBrandPack.Name = "txtBrandPack"
        Me.txtBrandPack.ReadOnly = True
        Me.txtBrandPack.Size = New System.Drawing.Size(250, 20)
        Me.txtBrandPack.TabIndex = 6
        '
        'EditPrice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 80)
        Me.Controls.Add(Me.txtBrandPack)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtPicStartDate)
        Me.Controls.Add(Me.txtPrice)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "EditPrice"
        Me.Text = "Edit Price"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPrice As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents dtPicStartDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOK As Janus.Windows.EditControls.UIButton
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBrandPack As System.Windows.Forms.TextBox
End Class
