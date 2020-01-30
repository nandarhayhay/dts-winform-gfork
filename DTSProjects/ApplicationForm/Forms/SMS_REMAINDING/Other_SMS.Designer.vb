<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Other_SMS
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
        Me.components = New System.ComponentModel.Container
        Dim grdDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Other_SMS))
        Dim grdRetailer_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdRSMTM_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.grpDistributor = New Janus.Windows.EditControls.UIGroupBox
        Me.txtDistCCSMS = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbMobileToSend = New System.Windows.Forms.ComboBox
        Me.grdDistributor = New Janus.Windows.GridEX.GridEX
        Me.txtMessageDistributor = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnDistributor = New Janus.Windows.EditControls.UIButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grpRetailer = New Janus.Windows.EditControls.UIGroupBox
        Me.txtCustCCSMS = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.grdRetailer = New Janus.Windows.GridEX.GridEX
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbCustomerType = New System.Windows.Forms.ComboBox
        Me.txtMessageRetailer = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.txtCCSMSRSMTM = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmbRSMTM = New System.Windows.Forms.ComboBox
        Me.grdRSMTM = New Janus.Windows.GridEX.GridEX
        Me.txtMessageRSMTM = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        CType(Me.grpDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDistributor.SuspendLayout()
        CType(Me.grdDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpRetailer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRetailer.SuspendLayout()
        CType(Me.grdRetailer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.grdRSMTM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpDistributor
        '
        Me.grpDistributor.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.ExplorerBarGroupBackground
        Me.grpDistributor.Controls.Add(Me.txtDistCCSMS)
        Me.grpDistributor.Controls.Add(Me.Label4)
        Me.grpDistributor.Controls.Add(Me.Label2)
        Me.grpDistributor.Controls.Add(Me.cmbMobileToSend)
        Me.grpDistributor.Controls.Add(Me.grdDistributor)
        Me.grpDistributor.Controls.Add(Me.txtMessageDistributor)
        Me.grpDistributor.Controls.Add(Me.Label3)
        Me.grpDistributor.Location = New System.Drawing.Point(0, 289)
        Me.grpDistributor.Name = "grpDistributor"
        Me.grpDistributor.Size = New System.Drawing.Size(808, 209)
        Me.grpDistributor.TabIndex = 5
        Me.grpDistributor.Text = "Own Message to distributor"
        Me.grpDistributor.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtDistCCSMS
        '
        Me.txtDistCCSMS.Location = New System.Drawing.Point(12, 70)
        Me.txtDistCCSMS.Name = "txtDistCCSMS"
        Me.txtDistCCSMS.Size = New System.Drawing.Size(227, 20)
        Me.txtDistCCSMS.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(12, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(208, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "CC SMS to Other Mobile(for Additonal Info)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(8, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Mobile to send"
        '
        'cmbMobileToSend
        '
        Me.cmbMobileToSend.FormattingEnabled = True
        Me.cmbMobileToSend.Items.AddRange(New Object() {"1 Contact_Mobile1", "2 Contact_Mobile2", "3 All(1 and 2)"})
        Me.cmbMobileToSend.Location = New System.Drawing.Point(95, 20)
        Me.cmbMobileToSend.Name = "cmbMobileToSend"
        Me.cmbMobileToSend.Size = New System.Drawing.Size(144, 21)
        Me.cmbMobileToSend.TabIndex = 13
        '
        'grdDistributor
        '
        Me.grdDistributor.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdDistributor.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdDistributor_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdDistributor.DesignTimeLayout = grdDistributor_DesignTimeLayout
        Me.grdDistributor.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdDistributor.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdDistributor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdDistributor.GroupByBoxVisible = False
        Me.grdDistributor.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdDistributor.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdDistributor.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdDistributor.Location = New System.Drawing.Point(245, 19)
        Me.grdDistributor.Name = "grdDistributor"
        Me.grdDistributor.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdDistributor.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdDistributor.RecordNavigator = True
        Me.grdDistributor.Size = New System.Drawing.Size(552, 152)
        Me.grdDistributor.TabIndex = 12
        Me.grdDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdDistributor.WatermarkImage.Image = CType(resources.GetObject("grdDistributor.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdDistributor.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'txtMessageDistributor
        '
        Me.txtMessageDistributor.Location = New System.Drawing.Point(10, 175)
        Me.txtMessageDistributor.MaxLength = 160
        Me.txtMessageDistributor.Multiline = True
        Me.txtMessageDistributor.Name = "txtMessageDistributor"
        Me.txtMessageDistributor.Size = New System.Drawing.Size(787, 31)
        Me.txtMessageDistributor.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(12, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(165, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Message no more than 160 chars"
        '
        'btnDistributor
        '
        Me.btnDistributor.ImageIndex = 0
        Me.btnDistributor.ImageList = Me.ImageList1
        Me.btnDistributor.Location = New System.Drawing.Point(726, 704)
        Me.btnDistributor.Name = "btnDistributor"
        Me.btnDistributor.Size = New System.Drawing.Size(82, 23)
        Me.btnDistributor.TabIndex = 3
        Me.btnDistributor.Text = "Send SMS"
        Me.btnDistributor.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "MessageQueuing.ico")
        '
        'grpRetailer
        '
        Me.grpRetailer.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.ExplorerBarGroupBackground
        Me.grpRetailer.Controls.Add(Me.txtCustCCSMS)
        Me.grpRetailer.Controls.Add(Me.Label6)
        Me.grpRetailer.Controls.Add(Me.grdRetailer)
        Me.grpRetailer.Controls.Add(Me.Label5)
        Me.grpRetailer.Controls.Add(Me.cmbCustomerType)
        Me.grpRetailer.Controls.Add(Me.txtMessageRetailer)
        Me.grpRetailer.Controls.Add(Me.Label1)
        Me.grpRetailer.Location = New System.Drawing.Point(0, 12)
        Me.grpRetailer.Name = "grpRetailer"
        Me.grpRetailer.Size = New System.Drawing.Size(808, 271)
        Me.grpRetailer.TabIndex = 6
        Me.grpRetailer.Text = "Own Message to retailer"
        Me.grpRetailer.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtCustCCSMS
        '
        Me.txtCustCCSMS.Location = New System.Drawing.Point(15, 72)
        Me.txtCustCCSMS.Name = "txtCustCCSMS"
        Me.txtCustCCSMS.Size = New System.Drawing.Size(224, 20)
        Me.txtCustCCSMS.TabIndex = 18
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(15, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(208, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "CC SMS to Other Mobile(for Additonal Info)"
        '
        'grdRetailer
        '
        Me.grdRetailer.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdRetailer.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdRetailer_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdRetailer.DesignTimeLayout = grdRetailer_DesignTimeLayout
        Me.grdRetailer.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdRetailer.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdRetailer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdRetailer.GroupByBoxVisible = False
        Me.grdRetailer.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdRetailer.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdRetailer.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdRetailer.Location = New System.Drawing.Point(245, 21)
        Me.grdRetailer.Name = "grdRetailer"
        Me.grdRetailer.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdRetailer.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdRetailer.RecordNavigator = True
        Me.grdRetailer.Size = New System.Drawing.Size(552, 199)
        Me.grdRetailer.TabIndex = 11
        Me.grdRetailer.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdRetailer.WatermarkImage.Image = CType(resources.GetObject("grdRetailer.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdRetailer.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(11, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Customer Type"
        '
        'cmbCustomerType
        '
        Me.cmbCustomerType.AutoCompleteCustomSource.AddRange(New String() {"RETAILER 1", "RETAILER 2"})
        Me.cmbCustomerType.FormattingEnabled = True
        Me.cmbCustomerType.Items.AddRange(New Object() {"RETAILER 1", "RETAILER 2"})
        Me.cmbCustomerType.Location = New System.Drawing.Point(95, 21)
        Me.cmbCustomerType.Name = "cmbCustomerType"
        Me.cmbCustomerType.Size = New System.Drawing.Size(144, 21)
        Me.cmbCustomerType.TabIndex = 9
        '
        'txtMessageRetailer
        '
        Me.txtMessageRetailer.Location = New System.Drawing.Point(9, 230)
        Me.txtMessageRetailer.MaxLength = 160
        Me.txtMessageRetailer.Multiline = True
        Me.txtMessageRetailer.Name = "txtMessageRetailer"
        Me.txtMessageRetailer.Size = New System.Drawing.Size(787, 31)
        Me.txtMessageRetailer.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(6, 211)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(168, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Message(no more than 160 chars)"
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.ExplorerBarGroupBackground
        Me.UiGroupBox1.Controls.Add(Me.txtCCSMSRSMTM)
        Me.UiGroupBox1.Controls.Add(Me.Label7)
        Me.UiGroupBox1.Controls.Add(Me.Label8)
        Me.UiGroupBox1.Controls.Add(Me.cmbRSMTM)
        Me.UiGroupBox1.Controls.Add(Me.grdRSMTM)
        Me.UiGroupBox1.Controls.Add(Me.txtMessageRSMTM)
        Me.UiGroupBox1.Controls.Add(Me.Label9)
        Me.UiGroupBox1.Location = New System.Drawing.Point(0, 506)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(806, 194)
        Me.UiGroupBox1.TabIndex = 7
        Me.UiGroupBox1.Text = "Own Message to TM/RSM"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtCCSMSRSMTM
        '
        Me.txtCCSMSRSMTM.Location = New System.Drawing.Point(12, 74)
        Me.txtCCSMSRSMTM.Name = "txtCCSMSRSMTM"
        Me.txtCCSMSRSMTM.Size = New System.Drawing.Size(227, 20)
        Me.txtCCSMSRSMTM.TabIndex = 16
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(13, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(208, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "CC SMS to Other Mobile(for Additonal Info)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(8, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Mobile to send"
        '
        'cmbRSMTM
        '
        Me.cmbRSMTM.FormattingEnabled = True
        Me.cmbRSMTM.Items.AddRange(New Object() {"RSM", "TMOrJTM"})
        Me.cmbRSMTM.Location = New System.Drawing.Point(95, 20)
        Me.cmbRSMTM.Name = "cmbRSMTM"
        Me.cmbRSMTM.Size = New System.Drawing.Size(144, 21)
        Me.cmbRSMTM.TabIndex = 13
        '
        'grdRSMTM
        '
        Me.grdRSMTM.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdRSMTM.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdRSMTM_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdRSMTM.DesignTimeLayout = grdRSMTM_DesignTimeLayout
        Me.grdRSMTM.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdRSMTM.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdRSMTM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdRSMTM.GroupByBoxVisible = False
        Me.grdRSMTM.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdRSMTM.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdRSMTM.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdRSMTM.Location = New System.Drawing.Point(245, 19)
        Me.grdRSMTM.Name = "grdRSMTM"
        Me.grdRSMTM.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdRSMTM.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdRSMTM.RecordNavigator = True
        Me.grdRSMTM.Size = New System.Drawing.Size(552, 133)
        Me.grdRSMTM.TabIndex = 12
        Me.grdRSMTM.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdRSMTM.WatermarkImage.Image = CType(resources.GetObject("grdRSMTM.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdRSMTM.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'txtMessageRSMTM
        '
        Me.txtMessageRSMTM.Location = New System.Drawing.Point(12, 158)
        Me.txtMessageRSMTM.MaxLength = 160
        Me.txtMessageRSMTM.Multiline = True
        Me.txtMessageRSMTM.Name = "txtMessageRSMTM"
        Me.txtMessageRSMTM.Size = New System.Drawing.Size(785, 31)
        Me.txtMessageRSMTM.TabIndex = 8
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(15, 139)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(165, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Message no more than 160 chars"
        '
        'Other_SMS
        '
        Me.AcceptButton = Me.btnDistributor
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(814, 728)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Controls.Add(Me.grpRetailer)
        Me.Controls.Add(Me.grpDistributor)
        Me.Controls.Add(Me.btnDistributor)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Other_SMS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = ""
        Me.Text = "Custom SMS"
        CType(Me.grpDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDistributor.ResumeLayout(False)
        Me.grpDistributor.PerformLayout()
        CType(Me.grdDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpRetailer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRetailer.ResumeLayout(False)
        Me.grpRetailer.PerformLayout()
        CType(Me.grdRetailer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.UiGroupBox1.PerformLayout()
        CType(Me.grdRSMTM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpDistributor As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents txtMessageDistributor As System.Windows.Forms.TextBox
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents btnDistributor As Janus.Windows.EditControls.UIButton
    Private WithEvents grpRetailer As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents txtMessageRetailer As System.Windows.Forms.TextBox
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents cmbCustomerType As System.Windows.Forms.ComboBox
    Private WithEvents cmbMobileToSend As System.Windows.Forms.ComboBox
    Private WithEvents grdDistributor As Janus.Windows.GridEX.GridEX
    Private WithEvents grdRetailer As Janus.Windows.GridEX.GridEX
    Private WithEvents txtDistCCSMS As System.Windows.Forms.TextBox
    Private WithEvents txtCustCCSMS As System.Windows.Forms.TextBox
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents txtCCSMSRSMTM As System.Windows.Forms.TextBox
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents cmbRSMTM As System.Windows.Forms.ComboBox
    Private WithEvents grdRSMTM As Janus.Windows.GridEX.GridEX
    Private WithEvents txtMessageRSMTM As System.Windows.Forms.TextBox
    Private WithEvents Label9 As System.Windows.Forms.Label

End Class
