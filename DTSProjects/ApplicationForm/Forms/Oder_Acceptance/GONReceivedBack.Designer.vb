<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GONReceivedBack
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GONReceivedBack))
        Dim mcbGONNO_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbGonReceiver_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.XpGradientPanel2 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.btnAddNew = New Janus.Windows.EditControls.UIButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnCLose = New Janus.Windows.EditControls.UIButton
        Me.btnSave = New Janus.Windows.EditControls.UIButton
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.lblTransporter = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.btnSearchGON = New DTSProjects.ButtonSearch
        Me.lblSPPDate = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblGON_DATE = New System.Windows.Forms.Label
        Me.lblFromDistributor = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblSPPBNumber = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.mcbGONNO = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtReceivedBackHere = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.UiGroupBox2 = New Janus.Windows.EditControls.UIGroupBox
        Me.mcbGonReceiver = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.Label7 = New System.Windows.Forms.Label
        Me.chkGonStamped = New System.Windows.Forms.CheckBox
        Me.chkGonSigned = New System.Windows.Forms.CheckBox
        Me.UiGroupBox4 = New Janus.Windows.EditControls.UIGroupBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtPicSPPBFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicSPPBBUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.grpDescriptions = New Janus.Windows.EditControls.UIGroupBox
        Me.txtDescriptions = New System.Windows.Forms.TextBox
        Me.XpGradientPanel2.SuspendLayout()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.mcbGONNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox2.SuspendLayout()
        CType(Me.mcbGonReceiver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UiGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox4.SuspendLayout()
        CType(Me.grpDescriptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDescriptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'XpGradientPanel2
        '
        Me.XpGradientPanel2.Controls.Add(Me.btnAddNew)
        Me.XpGradientPanel2.Controls.Add(Me.btnCLose)
        Me.XpGradientPanel2.Controls.Add(Me.btnSave)
        Me.XpGradientPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.XpGradientPanel2.EndColor = System.Drawing.SystemColors.MenuBar
        Me.XpGradientPanel2.Location = New System.Drawing.Point(0, 361)
        Me.XpGradientPanel2.Name = "XpGradientPanel2"
        Me.XpGradientPanel2.Size = New System.Drawing.Size(482, 35)
        Me.XpGradientPanel2.StartColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.XpGradientPanel2.TabIndex = 5
        '
        'btnAddNew
        '
        Me.btnAddNew.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAddNew.ImageIndex = 0
        Me.btnAddNew.ImageList = Me.ImageList1
        Me.btnAddNew.Location = New System.Drawing.Point(7, 7)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(75, 22)
        Me.btnAddNew.TabIndex = 0
        Me.btnAddNew.Text = "Add &New"
        Me.btnAddNew.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "")
        Me.ImageList1.Images.SetKeyName(9, "")
        Me.ImageList1.Images.SetKeyName(10, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(11, "Cancel and Close.ico")
        Me.ImageList1.Images.SetKeyName(12, "Search.png")
        '
        'btnCLose
        '
        Me.btnCLose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnCLose.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnCLose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCLose.ImageIndex = 11
        Me.btnCLose.ImageList = Me.ImageList1
        Me.btnCLose.Location = New System.Drawing.Point(407, 7)
        Me.btnCLose.Name = "btnCLose"
        Me.btnCLose.Size = New System.Drawing.Size(70, 22)
        Me.btnCLose.TabIndex = 2
        Me.btnCLose.Text = "&Close"
        Me.btnCLose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnSave.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnSave.ImageIndex = 7
        Me.btnSave.ImageList = Me.ImageList1
        Me.btnSave.Location = New System.Drawing.Point(323, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(74, 22)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "&Save"
        Me.btnSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.Controls.Add(Me.lblTransporter)
        Me.UiGroupBox1.Controls.Add(Me.Label11)
        Me.UiGroupBox1.Controls.Add(Me.btnSearchGON)
        Me.UiGroupBox1.Controls.Add(Me.lblSPPDate)
        Me.UiGroupBox1.Controls.Add(Me.Label9)
        Me.UiGroupBox1.Controls.Add(Me.lblGON_DATE)
        Me.UiGroupBox1.Controls.Add(Me.lblFromDistributor)
        Me.UiGroupBox1.Controls.Add(Me.Label5)
        Me.UiGroupBox1.Controls.Add(Me.lblSPPBNumber)
        Me.UiGroupBox1.Controls.Add(Me.Label2)
        Me.UiGroupBox1.Controls.Add(Me.mcbGONNO)
        Me.UiGroupBox1.Controls.Add(Me.Label1)
        Me.UiGroupBox1.Controls.Add(Me.Label8)
        Me.UiGroupBox1.Location = New System.Drawing.Point(7, 54)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(463, 149)
        Me.UiGroupBox1.TabIndex = 6
        Me.UiGroupBox1.Text = "GON Information"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblTransporter
        '
        Me.lblTransporter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTransporter.Location = New System.Drawing.Point(79, 123)
        Me.lblTransporter.Name = "lblTransporter"
        Me.lblTransporter.Size = New System.Drawing.Size(375, 19)
        Me.lblTransporter.TabIndex = 12
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 127)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(61, 13)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Transporter"
        '
        'btnSearchGON
        '
        Me.btnSearchGON.Location = New System.Drawing.Point(219, 17)
        Me.btnSearchGON.Name = "btnSearchGON"
        Me.btnSearchGON.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchGON.TabIndex = 10
        '
        'lblSPPDate
        '
        Me.lblSPPDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSPPDate.Location = New System.Drawing.Point(243, 71)
        Me.lblSPPDate.Name = "lblSPPDate"
        Me.lblSPPDate.Size = New System.Drawing.Size(211, 19)
        Me.lblSPPDate.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(241, 52)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "SPPB_DATE"
        '
        'lblGON_DATE
        '
        Me.lblGON_DATE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblGON_DATE.Location = New System.Drawing.Point(19, 69)
        Me.lblGON_DATE.Name = "lblGON_DATE"
        Me.lblGON_DATE.Size = New System.Drawing.Size(205, 19)
        Me.lblGON_DATE.TabIndex = 7
        '
        'lblFromDistributor
        '
        Me.lblFromDistributor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFromDistributor.Location = New System.Drawing.Point(79, 96)
        Me.lblFromDistributor.Name = "lblFromDistributor"
        Me.lblFromDistributor.Size = New System.Drawing.Size(375, 19)
        Me.lblFromDistributor.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Distributor"
        '
        'lblSPPBNumber
        '
        Me.lblSPPBNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSPPBNumber.Location = New System.Drawing.Point(297, 16)
        Me.lblSPPBNumber.Name = "lblSPPBNumber"
        Me.lblSPPBNumber.Size = New System.Drawing.Size(157, 19)
        Me.lblSPPBNumber.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(239, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "SPPB NO"
        '
        'mcbGONNO
        '
        Me.mcbGONNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbGONNO_DesignTimeLayout.LayoutString = resources.GetString("mcbGONNO_DesignTimeLayout.LayoutString")
        Me.mcbGONNO.DesignTimeLayout = mcbGONNO_DesignTimeLayout
        Me.mcbGONNO.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbGONNO.Location = New System.Drawing.Point(60, 16)
        Me.mcbGONNO.Name = "mcbGONNO"
        Me.mcbGONNO.SelectedIndex = -1
        Me.mcbGONNO.SelectedItem = Nothing
        Me.mcbGONNO.Size = New System.Drawing.Size(153, 20)
        Me.mcbGONNO.TabIndex = 1
        Me.mcbGONNO.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "GON NO"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "GONDate"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 212)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Date Received Back here"
        '
        'dtReceivedBackHere
        '
        Me.dtReceivedBackHere.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtReceivedBackHere.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtReceivedBackHere.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtReceivedBackHere.DropDownCalendar.Name = ""
        Me.dtReceivedBackHere.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtReceivedBackHere.Location = New System.Drawing.Point(157, 210)
        Me.dtReceivedBackHere.Name = "dtReceivedBackHere"
        Me.dtReceivedBackHere.Size = New System.Drawing.Size(198, 20)
        Me.dtReceivedBackHere.TabIndex = 10
        Me.dtReceivedBackHere.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'UiGroupBox2
        '
        Me.UiGroupBox2.Controls.Add(Me.mcbGonReceiver)
        Me.UiGroupBox2.Controls.Add(Me.Label7)
        Me.UiGroupBox2.Controls.Add(Me.chkGonStamped)
        Me.UiGroupBox2.Controls.Add(Me.chkGonSigned)
        Me.UiGroupBox2.Location = New System.Drawing.Point(7, 231)
        Me.UiGroupBox2.Name = "UiGroupBox2"
        Me.UiGroupBox2.Size = New System.Drawing.Size(445, 68)
        Me.UiGroupBox2.TabIndex = 11
        Me.UiGroupBox2.Text = "Received by Distributor"
        Me.UiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'mcbGonReceiver
        '
        Me.mcbGonReceiver.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbGonReceiver_DesignTimeLayout.LayoutString = resources.GetString("mcbGonReceiver_DesignTimeLayout.LayoutString")
        Me.mcbGonReceiver.DesignTimeLayout = mcbGonReceiver_DesignTimeLayout
        Me.mcbGonReceiver.Location = New System.Drawing.Point(145, 18)
        Me.mcbGonReceiver.MaxLength = 50
        Me.mcbGonReceiver.Name = "mcbGonReceiver"
        Me.mcbGonReceiver.SelectedIndex = -1
        Me.mcbGonReceiver.SelectedItem = Nothing
        Me.mcbGonReceiver.Size = New System.Drawing.Size(294, 20)
        Me.mcbGonReceiver.TabIndex = 4
        Me.mcbGonReceiver.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Gon Accepted by"
        '
        'chkGonStamped
        '
        Me.chkGonStamped.AutoSize = True
        Me.chkGonStamped.Location = New System.Drawing.Point(343, 44)
        Me.chkGonStamped.Name = "chkGonStamped"
        Me.chkGonStamped.Size = New System.Drawing.Size(95, 17)
        Me.chkGonStamped.TabIndex = 2
        Me.chkGonStamped.Text = "GON Stamped"
        Me.chkGonStamped.UseVisualStyleBackColor = True
        '
        'chkGonSigned
        '
        Me.chkGonSigned.AutoSize = True
        Me.chkGonSigned.Location = New System.Drawing.Point(145, 45)
        Me.chkGonSigned.Name = "chkGonSigned"
        Me.chkGonSigned.Size = New System.Drawing.Size(86, 17)
        Me.chkGonSigned.TabIndex = 1
        Me.chkGonSigned.Text = "GON Signed"
        Me.chkGonSigned.UseVisualStyleBackColor = True
        '
        'UiGroupBox4
        '
        Me.UiGroupBox4.Controls.Add(Me.Label6)
        Me.UiGroupBox4.Controls.Add(Me.Label4)
        Me.UiGroupBox4.Controls.Add(Me.dtPicSPPBFrom)
        Me.UiGroupBox4.Controls.Add(Me.dtPicSPPBBUntil)
        Me.UiGroupBox4.Location = New System.Drawing.Point(7, 2)
        Me.UiGroupBox4.Name = "UiGroupBox4"
        Me.UiGroupBox4.Size = New System.Drawing.Size(463, 48)
        Me.UiGroupBox4.TabIndex = 12
        Me.UiGroupBox4.Text = "Please define boundaries of data by SPPB_Date"
        Me.UiGroupBox4.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(221, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Until"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(2, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "From"
        '
        'dtPicSPPBFrom
        '
        Me.dtPicSPPBFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicSPPBFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicSPPBFrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicSPPBFrom.DropDownCalendar.Name = ""
        Me.dtPicSPPBFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicSPPBFrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicSPPBFrom.Location = New System.Drawing.Point(33, 16)
        Me.dtPicSPPBFrom.Name = "dtPicSPPBFrom"
        Me.dtPicSPPBFrom.ShowTodayButton = False
        Me.dtPicSPPBFrom.Size = New System.Drawing.Size(188, 20)
        Me.dtPicSPPBFrom.TabIndex = 12
        Me.dtPicSPPBFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicSPPBBUntil
        '
        Me.dtPicSPPBBUntil.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicSPPBBUntil.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicSPPBBUntil.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicSPPBBUntil.DropDownCalendar.Name = ""
        Me.dtPicSPPBBUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicSPPBBUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicSPPBBUntil.Location = New System.Drawing.Point(250, 17)
        Me.dtPicSPPBBUntil.Name = "dtPicSPPBBUntil"
        Me.dtPicSPPBBUntil.Size = New System.Drawing.Size(189, 20)
        Me.dtPicSPPBBUntil.TabIndex = 11
        Me.dtPicSPPBBUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'grpDescriptions
        '
        Me.grpDescriptions.Controls.Add(Me.txtDescriptions)
        Me.grpDescriptions.Location = New System.Drawing.Point(7, 306)
        Me.grpDescriptions.Name = "grpDescriptions"
        Me.grpDescriptions.Size = New System.Drawing.Size(445, 46)
        Me.grpDescriptions.TabIndex = 13
        Me.grpDescriptions.Text = "Remark"
        Me.grpDescriptions.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtDescriptions
        '
        Me.txtDescriptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDescriptions.Location = New System.Drawing.Point(3, 16)
        Me.txtDescriptions.MaxLength = 250
        Me.txtDescriptions.Multiline = True
        Me.txtDescriptions.Name = "txtDescriptions"
        Me.txtDescriptions.Size = New System.Drawing.Size(439, 27)
        Me.txtDescriptions.TabIndex = 0
        '
        'GONReceivedBack
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.CancelButton = Me.btnCLose
        Me.ClientSize = New System.Drawing.Size(482, 396)
        Me.Controls.Add(Me.grpDescriptions)
        Me.Controls.Add(Me.UiGroupBox4)
        Me.Controls.Add(Me.UiGroupBox2)
        Me.Controls.Add(Me.dtReceivedBackHere)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Controls.Add(Me.XpGradientPanel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "GONReceivedBack"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GON DOCUMENT RECEIVED BACK FROM DISTRIBUTOR"
        Me.XpGradientPanel2.ResumeLayout(False)
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.UiGroupBox1.PerformLayout()
        CType(Me.mcbGONNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox2.ResumeLayout(False)
        Me.UiGroupBox2.PerformLayout()
        CType(Me.mcbGonReceiver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UiGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox4.ResumeLayout(False)
        Me.UiGroupBox4.PerformLayout()
        CType(Me.grpDescriptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDescriptions.ResumeLayout(False)
        Me.grpDescriptions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents XpGradientPanel2 As SteepValley.Windows.Forms.XPGradientPanel
    Friend WithEvents btnAddNew As Janus.Windows.EditControls.UIButton
    Private WithEvents btnCLose As Janus.Windows.EditControls.UIButton
    Private WithEvents btnSave As Janus.Windows.EditControls.UIButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents mcbGONNO As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents lblFromDistributor As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents lblSPPBNumber As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtReceivedBackHere As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents UiGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents chkGonSigned As System.Windows.Forms.CheckBox
    Friend WithEvents chkGonStamped As System.Windows.Forms.CheckBox
    Friend WithEvents UiGroupBox4 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtPicSPPBFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicSPPBBUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents lblGON_DATE As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents lblSPPDate As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents btnSearchGON As DTSProjects.ButtonSearch
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents mcbGonReceiver As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents grpDescriptions As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents txtDescriptions As System.Windows.Forms.TextBox
    Private WithEvents lblTransporter As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label

End Class
