<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OtherDDDR
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
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OtherDDDR))
        Me.grdPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.btnDiscDD = New DevComponents.DotNetBar.ButtonItem
        Me.btnDiscDr = New DevComponents.DotNetBar.ButtonItem
        Me.btnDiscCBD = New DevComponents.DotNetBar.ButtonItem
        Me.btnUncategorized = New DevComponents.DotNetBar.ButtonItem
        Me.pnlUncategorizedDisc = New System.Windows.Forms.Panel
        Me.txtResult = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.grpMethode = New System.Windows.Forms.GroupBox
        Me.rdbbyUserFree = New System.Windows.Forms.RadioButton
        Me.rdbbyPercentage = New System.Windows.Forms.RadioButton
        Me.txtPercentage = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnCancel = New Janus.Windows.EditControls.UIButton
        Me.btnOK = New Janus.Windows.EditControls.UIButton
        Me.btnDiscDK = New DevComponents.DotNetBar.ButtonItem
        Me.grdPanel1.SuspendLayout()
        Me.pnlUncategorizedDisc.SuspendLayout()
        Me.grpMethode.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdPanel1
        '
        Me.grdPanel1.Controls.Add(Me.ItemPanel1)
        Me.grdPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.grdPanel1.EndColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdPanel1.Location = New System.Drawing.Point(0, 0)
        Me.grdPanel1.Name = "grdPanel1"
        Me.grdPanel1.Size = New System.Drawing.Size(622, 28)
        Me.grdPanel1.StartColor = System.Drawing.SystemColors.MenuBar
        Me.grdPanel1.TabIndex = 15
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
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnDiscDD, Me.btnDiscDr, Me.btnDiscCBD, Me.btnDiscDK, Me.btnUncategorized})
        Me.ItemPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(316, 28)
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
        Me.pnlUncategorizedDisc.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlUncategorizedDisc.Location = New System.Drawing.Point(0, 185)
        Me.pnlUncategorizedDisc.Name = "pnlUncategorizedDisc"
        Me.pnlUncategorizedDisc.Size = New System.Drawing.Size(622, 115)
        Me.pnlUncategorizedDisc.TabIndex = 16
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
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.FrozenColumns = 3
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(0, 28)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(622, 157)
        Me.GridEX1.TabIndex = 17
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnOK)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 300)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(622, 31)
        Me.Panel1.TabIndex = 18
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(526, 6)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(84, 22)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "&Cancel / close"
        Me.btnCancel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(434, 5)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 22)
        Me.btnOK.TabIndex = 17
        Me.btnOK.Text = "&OK"
        Me.btnOK.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnDiscDK
        '
        Me.btnDiscDK.Name = "btnDiscDK"
        Me.btnDiscDK.Text = "DK"
        '
        'OtherDDDR
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(622, 331)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.pnlUncategorizedDisc)
        Me.Controls.Add(Me.grdPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "OtherDDDR"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.grdPanel1.ResumeLayout(False)
        Me.pnlUncategorizedDisc.ResumeLayout(False)
        Me.pnlUncategorizedDisc.PerformLayout()
        Me.grpMethode.ResumeLayout(False)
        Me.grpMethode.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grdPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Friend WithEvents btnDiscDD As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDiscDr As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDiscCBD As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnUncategorized As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents pnlUncategorizedDisc As System.Windows.Forms.Panel
    Friend WithEvents txtResult As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents grpMethode As System.Windows.Forms.GroupBox
    Private WithEvents rdbbyUserFree As System.Windows.Forms.RadioButton
    Private WithEvents rdbbyPercentage As System.Windows.Forms.RadioButton
    Friend WithEvents txtPercentage As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents btnCancel As Janus.Windows.EditControls.UIButton
    Private WithEvents btnOK As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnDiscDK As DevComponents.DotNetBar.ButtonItem

End Class
