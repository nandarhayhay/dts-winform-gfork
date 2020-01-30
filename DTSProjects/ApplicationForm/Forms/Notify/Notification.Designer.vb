<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Notification
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Notification))
        Me.lblHeaderReminder = New System.Windows.Forms.Label
        Me.lblTimeReminder = New System.Windows.Forms.Label
        Me.btnDismisAll = New System.Windows.Forms.Button
        Me.btnDismiss = New System.Windows.Forms.Button
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.cmbSnooze = New System.Windows.Forms.ComboBox
        Me.btnSnooze = New System.Windows.Forms.Button
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblHeaderReminder
        '
        Me.lblHeaderReminder.AutoSize = True
        Me.lblHeaderReminder.Location = New System.Drawing.Point(12, 9)
        Me.lblHeaderReminder.Name = "lblHeaderReminder"
        Me.lblHeaderReminder.Size = New System.Drawing.Size(19, 13)
        Me.lblHeaderReminder.TabIndex = 0
        Me.lblHeaderReminder.Text = "...."
        '
        'lblTimeReminder
        '
        Me.lblTimeReminder.AutoSize = True
        Me.lblTimeReminder.Location = New System.Drawing.Point(12, 37)
        Me.lblTimeReminder.Name = "lblTimeReminder"
        Me.lblTimeReminder.Size = New System.Drawing.Size(22, 13)
        Me.lblTimeReminder.TabIndex = 1
        Me.lblTimeReminder.Text = "....."
        '
        'btnDismisAll
        '
        Me.btnDismisAll.BackColor = System.Drawing.Color.Transparent
        Me.btnDismisAll.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnDismisAll.Location = New System.Drawing.Point(12, 337)
        Me.btnDismisAll.Name = "btnDismisAll"
        Me.btnDismisAll.Size = New System.Drawing.Size(75, 23)
        Me.btnDismisAll.TabIndex = 3
        Me.btnDismisAll.Text = "Dismiss All"
        Me.btnDismisAll.UseVisualStyleBackColor = False
        '
        'btnDismiss
        '
        Me.btnDismiss.BackColor = System.Drawing.Color.Transparent
        Me.btnDismiss.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnDismiss.Location = New System.Drawing.Point(614, 337)
        Me.btnDismiss.Name = "btnDismiss"
        Me.btnDismiss.Size = New System.Drawing.Size(75, 23)
        Me.btnDismiss.TabIndex = 5
        Me.btnDismiss.Text = "Dismiss"
        Me.btnDismiss.UseVisualStyleBackColor = False
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.Controls.Add(Me.cmbSnooze)
        Me.UiGroupBox1.Controls.Add(Me.btnSnooze)
        Me.UiGroupBox1.Location = New System.Drawing.Point(383, 366)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(306, 38)
        Me.UiGroupBox1.TabIndex = 6
        Me.UiGroupBox1.Text = "Click Snooze to be remainded again"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'cmbSnooze
        '
        Me.cmbSnooze.FormattingEnabled = True
        Me.cmbSnooze.Items.AddRange(New Object() {"1 Minute", "5 Minutes", "10 Minutes", "15 Minutes", "30 Minutes", "1 Hour", "2 Hours", "8 Hours", "12 Hours", "1 Day", "3 Days", "1 Week", "2 Weeks", "3 Weeks"})
        Me.cmbSnooze.Location = New System.Drawing.Point(6, 13)
        Me.cmbSnooze.Name = "cmbSnooze"
        Me.cmbSnooze.Size = New System.Drawing.Size(218, 21)
        Me.cmbSnooze.TabIndex = 7
        '
        'btnSnooze
        '
        Me.btnSnooze.BackColor = System.Drawing.Color.Transparent
        Me.btnSnooze.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnSnooze.Location = New System.Drawing.Point(230, 11)
        Me.btnSnooze.Name = "btnSnooze"
        Me.btnSnooze.Size = New System.Drawing.Size(75, 23)
        Me.btnSnooze.TabIndex = 6
        Me.btnSnooze.Text = "Snooze"
        Me.btnSnooze.UseVisualStyleBackColor = False
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.Location = New System.Drawing.Point(12, 66)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(677, 265)
        Me.GridEX1.TabIndex = 7
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Notification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(696, 362)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Controls.Add(Me.btnDismiss)
        Me.Controls.Add(Me.btnDismisAll)
        Me.Controls.Add(Me.lblTimeReminder)
        Me.Controls.Add(Me.lblHeaderReminder)
        Me.Name = "Notification"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reminder"
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHeaderReminder As System.Windows.Forms.Label
    Friend WithEvents lblTimeReminder As System.Windows.Forms.Label
    Friend WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents cmbSnooze As System.Windows.Forms.ComboBox
    Private WithEvents btnDismisAll As System.Windows.Forms.Button
    Private WithEvents btnDismiss As System.Windows.Forms.Button
    Private WithEvents btnSnooze As System.Windows.Forms.Button
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX

End Class
