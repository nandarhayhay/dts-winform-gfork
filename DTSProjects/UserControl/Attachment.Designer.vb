<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Attachment
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Attachment))
        Me.btnSave = New Janus.Windows.EditControls.UIButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.XpGradientPanel2 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.txtSearch = New WatermarkTextBox.WaterMarkTextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.XpGradientPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnSave.ImageIndex = 2
        Me.btnSave.ImageList = Me.ImageList1
        Me.btnSave.Location = New System.Drawing.Point(584, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(101, 27)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "&Save All"
        Me.btnSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Cancel and Close.ico")
        Me.ImageList1.Images.SetKeyName(1, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(2, "SaveAllHS.png")
        '
        'XpGradientPanel2
        '
        Me.XpGradientPanel2.Controls.Add(Me.btnSave)
        Me.XpGradientPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.XpGradientPanel2.Location = New System.Drawing.Point(0, 410)
        Me.XpGradientPanel2.Name = "XpGradientPanel2"
        Me.XpGradientPanel2.Size = New System.Drawing.Size(689, 33)
        Me.XpGradientPanel2.TabIndex = 18
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.BackColor = System.Drawing.SystemColors.Window
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(0, 20)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(689, 390)
        Me.ListView1.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView1.TabIndex = 19
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSearch.Location = New System.Drawing.Point(0, 0)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(689, 20)
        Me.txtSearch.TabIndex = 20
        Me.txtSearch.WaterMarkColor = System.Drawing.Color.Gray
        Me.txtSearch.WaterMarkText = "Enter data"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Attachment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.XpGradientPanel2)
        Me.Name = "Attachment"
        Me.Size = New System.Drawing.Size(689, 443)
        Me.XpGradientPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents btnSave As Janus.Windows.EditControls.UIButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents XpGradientPanel2 As SteepValley.Windows.Forms.XPGradientPanel
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Private WithEvents txtSearch As WatermarkTextBox.WaterMarkTextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
