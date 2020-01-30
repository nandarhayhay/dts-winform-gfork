<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Other_QTY
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
        Me.txtPercentage = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtResult = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.grpMethode = New System.Windows.Forms.GroupBox
        Me.rdbbyUserFree = New System.Windows.Forms.RadioButton
        Me.rdbbyPercentage = New System.Windows.Forms.RadioButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnCancel = New Janus.Windows.EditControls.UIButton
        Me.btnOK = New Janus.Windows.EditControls.UIButton
        Me.grdPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.btnDiscDD = New DevComponents.DotNetBar.ButtonItem
        Me.btnDiscDr = New DevComponents.DotNetBar.ButtonItem
        Me.btnDiscCBD = New DevComponents.DotNetBar.ButtonItem
        Me.btnUncategorized = New DevComponents.DotNetBar.ButtonItem
        Me.pnlUncategorizedDisc = New System.Windows.Forms.Panel
        Me.pnlTypeOfDisc = New System.Windows.Forms.Panel
        Me.lblDiscInfo = New System.Windows.Forms.Label
        Me.grpDiscInfo = New System.Windows.Forms.GroupBox
        Me.lblProgramInfo = New System.Windows.Forms.Label
        Me.lblProgramID = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDiscType = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.grpMethode.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.grdPanel1.SuspendLayout()
        Me.pnlUncategorizedDisc.SuspendLayout()
        Me.pnlTypeOfDisc.SuspendLayout()
        Me.grpDiscInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtPercentage
        '
        Me.txtPercentage.Location = New System.Drawing.Point(142, 54)
        Me.txtPercentage.Name = "txtPercentage"
        Me.txtPercentage.ReadOnly = True
        Me.txtPercentage.Size = New System.Drawing.Size(70, 20)
        Me.txtPercentage.TabIndex = 5
        Me.txtPercentage.Text = "0.00"
        Me.txtPercentage.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtPercentage.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(22, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Set Other Discount %"
        '
        'txtResult
        '
        Me.txtResult.DecimalDigits = 3
        Me.txtResult.Location = New System.Drawing.Point(87, 83)
        Me.txtResult.Name = "txtResult"
        Me.txtResult.ReadOnly = True
        Me.txtResult.Size = New System.Drawing.Size(87, 20)
        Me.txtResult.TabIndex = 9
        Me.txtResult.Text = "0.000"
        Me.txtResult.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtResult.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(24, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Quantity"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.SystemColors.InfoText
        Me.Label3.Location = New System.Drawing.Point(179, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Ltr/KG"
        '
        'grpMethode
        '
        Me.grpMethode.BackColor = System.Drawing.Color.Transparent
        Me.grpMethode.Controls.Add(Me.rdbbyUserFree)
        Me.grpMethode.Controls.Add(Me.rdbbyPercentage)
        Me.grpMethode.Location = New System.Drawing.Point(14, 6)
        Me.grpMethode.Name = "grpMethode"
        Me.grpMethode.Size = New System.Drawing.Size(199, 41)
        Me.grpMethode.TabIndex = 11
        Me.grpMethode.TabStop = False
        Me.grpMethode.Text = "Choose Method"
        '
        'rdbbyUserFree
        '
        Me.rdbbyUserFree.AutoSize = True
        Me.rdbbyUserFree.Location = New System.Drawing.Point(102, 18)
        Me.rdbbyUserFree.Name = "rdbbyUserFree"
        Me.rdbbyUserFree.Size = New System.Drawing.Size(83, 17)
        Me.rdbbyUserFree.TabIndex = 1
        Me.rdbbyUserFree.TabStop = True
        Me.rdbbyUserFree.Text = "Free by user"
        Me.rdbbyUserFree.UseVisualStyleBackColor = True
        '
        'rdbbyPercentage
        '
        Me.rdbbyPercentage.AutoSize = True
        Me.rdbbyPercentage.Location = New System.Drawing.Point(6, 18)
        Me.rdbbyPercentage.Name = "rdbbyPercentage"
        Me.rdbbyPercentage.Size = New System.Drawing.Size(94, 17)
        Me.rdbbyPercentage.TabIndex = 0
        Me.rdbbyPercentage.TabStop = True
        Me.rdbbyPercentage.Text = "by Percentage"
        Me.rdbbyPercentage.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnOK)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 301)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(317, 31)
        Me.Panel1.TabIndex = 13
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(226, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(84, 22)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "&Cancel / close"
        Me.btnCancel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(134, 4)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 22)
        Me.btnOK.TabIndex = 17
        Me.btnOK.Text = "&OK"
        Me.btnOK.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'grdPanel1
        '
        Me.grdPanel1.Controls.Add(Me.ItemPanel1)
        Me.grdPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.grdPanel1.EndColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdPanel1.Location = New System.Drawing.Point(0, 0)
        Me.grdPanel1.Name = "grdPanel1"
        Me.grdPanel1.Size = New System.Drawing.Size(317, 28)
        Me.grdPanel1.StartColor = System.Drawing.SystemColors.MenuBar
        Me.grdPanel1.TabIndex = 14
        '
        'ItemPanel1
        '
        '
        '
        '
        Me.ItemPanel1.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ItemPanel1.BackgroundStyle.BackColor2 = System.Drawing.SystemColors.MenuBar
        Me.ItemPanel1.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderBottomWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.ItemPanel1.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderLeftWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderRightWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderTopWidth = 1
        Me.ItemPanel1.BackgroundStyle.PaddingBottom = 1
        Me.ItemPanel1.BackgroundStyle.PaddingLeft = 1
        Me.ItemPanel1.BackgroundStyle.PaddingRight = 1
        Me.ItemPanel1.BackgroundStyle.PaddingTop = 1
        Me.ItemPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnDiscDD, Me.btnDiscDr, Me.btnDiscCBD, Me.btnUncategorized})
        Me.ItemPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(289, 28)
        Me.ItemPanel1.TabIndex = 1
        Me.ItemPanel1.Text = "ItemPanel1"
        '
        'btnDiscDD
        '
        Me.btnDiscDD.Name = "btnDiscDD"
        Me.btnDiscDD.Text = "Disc Distributor"
        '
        'btnDiscDr
        '
        Me.btnDiscDr.Name = "btnDiscDr"
        Me.btnDiscDr.Text = "Disc Retailer"
        '
        'btnDiscCBD
        '
        Me.btnDiscCBD.Name = "btnDiscCBD"
        Me.btnDiscCBD.Text = "Disc CBD"
        '
        'btnUncategorized
        '
        Me.btnUncategorized.Name = "btnUncategorized"
        Me.btnUncategorized.Text = "Uncategorized"
        '
        'pnlUncategorizedDisc
        '
        Me.pnlUncategorizedDisc.Controls.Add(Me.txtResult)
        Me.pnlUncategorizedDisc.Controls.Add(Me.Label3)
        Me.pnlUncategorizedDisc.Controls.Add(Me.grpMethode)
        Me.pnlUncategorizedDisc.Controls.Add(Me.txtPercentage)
        Me.pnlUncategorizedDisc.Controls.Add(Me.Label2)
        Me.pnlUncategorizedDisc.Controls.Add(Me.Label1)
        Me.pnlUncategorizedDisc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUncategorizedDisc.Location = New System.Drawing.Point(0, 194)
        Me.pnlUncategorizedDisc.Name = "pnlUncategorizedDisc"
        Me.pnlUncategorizedDisc.Size = New System.Drawing.Size(317, 107)
        Me.pnlUncategorizedDisc.TabIndex = 15
        '
        'pnlTypeOfDisc
        '
        Me.pnlTypeOfDisc.Controls.Add(Me.lblDiscInfo)
        Me.pnlTypeOfDisc.Controls.Add(Me.grpDiscInfo)
        Me.pnlTypeOfDisc.Controls.Add(Me.Label4)
        Me.pnlTypeOfDisc.Controls.Add(Me.txtDiscType)
        Me.pnlTypeOfDisc.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTypeOfDisc.Location = New System.Drawing.Point(0, 28)
        Me.pnlTypeOfDisc.Name = "pnlTypeOfDisc"
        Me.pnlTypeOfDisc.Size = New System.Drawing.Size(317, 166)
        Me.pnlTypeOfDisc.TabIndex = 16
        '
        'lblDiscInfo
        '
        Me.lblDiscInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiscInfo.Location = New System.Drawing.Point(117, 8)
        Me.lblDiscInfo.Name = "lblDiscInfo"
        Me.lblDiscInfo.Size = New System.Drawing.Size(187, 20)
        Me.lblDiscInfo.TabIndex = 19
        Me.lblDiscInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'grpDiscInfo
        '
        Me.grpDiscInfo.Controls.Add(Me.lblProgramInfo)
        Me.grpDiscInfo.Controls.Add(Me.lblProgramID)
        Me.grpDiscInfo.Controls.Add(Me.Label6)
        Me.grpDiscInfo.Controls.Add(Me.Label5)
        Me.grpDiscInfo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.grpDiscInfo.Location = New System.Drawing.Point(3, 28)
        Me.grpDiscInfo.Name = "grpDiscInfo"
        Me.grpDiscInfo.Size = New System.Drawing.Size(311, 132)
        Me.grpDiscInfo.TabIndex = 18
        Me.grpDiscInfo.TabStop = False
        Me.grpDiscInfo.Text = "Disc Info"
        '
        'lblProgramInfo
        '
        Me.lblProgramInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblProgramInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProgramInfo.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblProgramInfo.Location = New System.Drawing.Point(62, 69)
        Me.lblProgramInfo.Name = "lblProgramInfo"
        Me.lblProgramInfo.Size = New System.Drawing.Size(243, 54)
        Me.lblProgramInfo.TabIndex = 3
        '
        'lblProgramID
        '
        Me.lblProgramID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblProgramID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProgramID.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblProgramID.Location = New System.Drawing.Point(62, 12)
        Me.lblProgramID.Name = "lblProgramID"
        Me.lblProgramID.Size = New System.Drawing.Size(243, 49)
        Me.lblProgramID.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(4, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 30)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Program Info"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(3, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 33)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "ProgramID/Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Disc"
        '
        'txtDiscType
        '
        Me.txtDiscType.DecimalDigits = 3
        Me.txtDiscType.Location = New System.Drawing.Point(65, 8)
        Me.txtDiscType.Name = "txtDiscType"
        Me.txtDiscType.ReadOnly = True
        Me.txtDiscType.Size = New System.Drawing.Size(49, 20)
        Me.txtDiscType.TabIndex = 10
        Me.txtDiscType.Text = "0.000"
        Me.txtDiscType.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtDiscType.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Other_QTY
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(317, 332)
        Me.Controls.Add(Me.pnlUncategorizedDisc)
        Me.Controls.Add(Me.pnlTypeOfDisc)
        Me.Controls.Add(Me.grdPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Other_QTY"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.grpMethode.ResumeLayout(False)
        Me.grpMethode.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.grdPanel1.ResumeLayout(False)
        Me.pnlUncategorizedDisc.ResumeLayout(False)
        Me.pnlUncategorizedDisc.PerformLayout()
        Me.pnlTypeOfDisc.ResumeLayout(False)
        Me.pnlTypeOfDisc.PerformLayout()
        Me.grpDiscInfo.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtPercentage As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtResult As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents grpMethode As System.Windows.Forms.GroupBox
    Private WithEvents rdbbyUserFree As System.Windows.Forms.RadioButton
    Private WithEvents rdbbyPercentage As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents btnCancel As Janus.Windows.EditControls.UIButton
    Private WithEvents btnOK As Janus.Windows.EditControls.UIButton
    Private WithEvents grdPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Friend WithEvents btnDiscCBD As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents pnlUncategorizedDisc As System.Windows.Forms.Panel
    Friend WithEvents pnlTypeOfDisc As System.Windows.Forms.Panel
    Friend WithEvents txtDiscType As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnUncategorized As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDiscDD As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDiscDr As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents grpDiscInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lblProgramInfo As System.Windows.Forms.Label
    Friend WithEvents lblProgramID As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblDiscInfo As System.Windows.Forms.Label
End Class
