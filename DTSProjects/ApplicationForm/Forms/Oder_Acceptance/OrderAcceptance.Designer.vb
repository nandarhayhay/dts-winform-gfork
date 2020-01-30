<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderAcceptance
    Inherits DTSProjects.BaseBigForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OrderAcceptance))
        Dim mcbPOREFNO_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbTM_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grpPO = New Janus.Windows.EditControls.UIGroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnFilter = New DTSProjects.ButtonSearch
        Me.txtProject = New System.Windows.Forms.TextBox
        Me.txtDistributor = New System.Windows.Forms.TextBox
        Me.txtPORefDate = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.mcbPOREFNO = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtPicOADate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtOARef = New System.Windows.Forms.TextBox
        Me.mcbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.Label6 = New System.Windows.Forms.Label
        Me.ButtonEntry1 = New DTSProjects.ButtonEntry
        Me.btnChekExisting = New Janus.Windows.EditControls.UIButton
        Me.btnOK = New Janus.Windows.EditControls.UIButton
        Me.btnCancel = New Janus.Windows.EditControls.UIButton
        Me.grpOA = New Janus.Windows.EditControls.UIGroupBox
        Me.mcbTM = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.btnFilterTM = New DTSProjects.ButtonSearch
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnDelete = New Janus.Windows.EditControls.UIButton
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPO.SuspendLayout()
        CType(Me.mcbPOREFNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpOA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOA.SuspendLayout()
        CType(Me.mcbTM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 389)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(573, 188)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Cancel.ico")
        Me.ImageList1.Images.SetKeyName(1, "Add.bmp")
        Me.ImageList1.Images.SetKeyName(2, "Grid.bmp")
        Me.ImageList1.Images.SetKeyName(3, "Filter 48 h p.ico")
        Me.ImageList1.Images.SetKeyName(4, "Customize.png")
        Me.ImageList1.Images.SetKeyName(5, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(6, "Save.png")
        Me.ImageList1.Images.SetKeyName(7, "Delete.ico")
        Me.ImageList1.Images.SetKeyName(8, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(9, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(10, "TextEdit.png")
        Me.ImageList1.Images.SetKeyName(11, "Search.png")
        Me.ImageList1.Images.SetKeyName(12, "purchase_order.ICO")
        Me.ImageList1.Images.SetKeyName(13, "Acceptance.ico")
        '
        'grpPO
        '
        Me.grpPO.Controls.Add(Me.Label8)
        Me.grpPO.Controls.Add(Me.btnFilter)
        Me.grpPO.Controls.Add(Me.txtProject)
        Me.grpPO.Controls.Add(Me.txtDistributor)
        Me.grpPO.Controls.Add(Me.txtPORefDate)
        Me.grpPO.Controls.Add(Me.Label3)
        Me.grpPO.Controls.Add(Me.mcbPOREFNO)
        Me.grpPO.Controls.Add(Me.Label2)
        Me.grpPO.Controls.Add(Me.Label1)
        Me.grpPO.ImageIndex = 12
        Me.grpPO.ImageList = Me.ImageList1
        Me.grpPO.Location = New System.Drawing.Point(7, 8)
        Me.grpPO.Name = "grpPO"
        Me.grpPO.Size = New System.Drawing.Size(540, 93)
        Me.grpPO.TabIndex = 2
        Me.grpPO.Text = "PO"
        Me.grpPO.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(7, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(92, 15)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "PO REF NO"
        '
        'btnFilter
        '
        Me.btnFilter.BackColor = System.Drawing.Color.Transparent
        Me.btnFilter.Location = New System.Drawing.Point(515, 16)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(20, 20)
        Me.btnFilter.TabIndex = 7
        '
        'txtProject
        '
        Me.txtProject.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtProject.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProject.Location = New System.Drawing.Point(346, 68)
        Me.txtProject.Name = "txtProject"
        Me.txtProject.ReadOnly = True
        Me.txtProject.Size = New System.Drawing.Size(188, 21)
        Me.txtProject.TabIndex = 6
        Me.txtProject.Visible = False
        '
        'txtDistributor
        '
        Me.txtDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtDistributor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistributor.Location = New System.Drawing.Point(110, 68)
        Me.txtDistributor.Name = "txtDistributor"
        Me.txtDistributor.ReadOnly = True
        Me.txtDistributor.Size = New System.Drawing.Size(230, 20)
        Me.txtDistributor.TabIndex = 5
        '
        'txtPORefDate
        '
        Me.txtPORefDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtPORefDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPORefDate.Location = New System.Drawing.Point(110, 41)
        Me.txtPORefDate.Name = "txtPORefDate"
        Me.txtPORefDate.ReadOnly = True
        Me.txtPORefDate.Size = New System.Drawing.Size(174, 20)
        Me.txtPORefDate.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(347, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "PROJECT"
        Me.Label3.Visible = False
        '
        'mcbPOREFNO
        '
        Me.mcbPOREFNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbPOREFNO_DesignTimeLayout.LayoutString = resources.GetString("mcbPOREFNO_DesignTimeLayout.LayoutString")
        Me.mcbPOREFNO.DesignTimeLayout = mcbPOREFNO_DesignTimeLayout
        Me.mcbPOREFNO.DisplayMember = "PO_REF_NO"
        Me.mcbPOREFNO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbPOREFNO.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbPOREFNO.Location = New System.Drawing.Point(110, 15)
        Me.mcbPOREFNO.Name = "mcbPOREFNO"
        Me.mcbPOREFNO.SelectedIndex = -1
        Me.mcbPOREFNO.SelectedItem = Nothing
        Me.mcbPOREFNO.Size = New System.Drawing.Size(399, 20)
        Me.mcbPOREFNO.TabIndex = 2
        Me.mcbPOREFNO.ValueMember = "PO_REF_NO"
        Me.mcbPOREFNO.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "DISTRIBUTOR"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "PO REF DATE"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 15)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Date"
        '
        'dtPicOADate
        '
        Me.dtPicOADate.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicOADate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicOADate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        Me.dtPicOADate.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        '
        '
        '
        Me.dtPicOADate.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicOADate.DropDownCalendar.FirstMonth = New Date(2008, 6, 1, 0, 0, 0, 0)
        Me.dtPicOADate.DropDownCalendar.Name = ""
        Me.dtPicOADate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicOADate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtPicOADate.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicOADate.Location = New System.Drawing.Point(78, 43)
        Me.dtPicOADate.Name = "dtPicOADate"
        Me.dtPicOADate.ReadOnly = True
        Me.dtPicOADate.ShowTodayButton = False
        Me.dtPicOADate.Size = New System.Drawing.Size(147, 20)
        Me.dtPicOADate.TabIndex = 4
        Me.dtPicOADate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 15)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "OA REF NO"
        '
        'txtOARef
        '
        Me.txtOARef.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtOARef.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOARef.Location = New System.Drawing.Point(79, 16)
        Me.txtOARef.MaxLength = 30
        Me.txtOARef.Name = "txtOARef"
        Me.txtOARef.ReadOnly = True
        Me.txtOARef.Size = New System.Drawing.Size(146, 20)
        Me.txtOARef.TabIndex = 6
        '
        'mcbDistributor
        '
        Me.mcbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbDistributor_DesignTimeLayout.LayoutString = resources.GetString("mcbDistributor_DesignTimeLayout.LayoutString")
        Me.mcbDistributor.DesignTimeLayout = mcbDistributor_DesignTimeLayout
        Me.mcbDistributor.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.mcbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.mcbDistributor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbDistributor.Location = New System.Drawing.Point(292, 16)
        Me.mcbDistributor.Name = "mcbDistributor"
        Me.mcbDistributor.SelectedIndex = -1
        Me.mcbDistributor.SelectedItem = Nothing
        Me.mcbDistributor.Size = New System.Drawing.Size(216, 20)
        Me.mcbDistributor.TabIndex = 7
        Me.mcbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.mcbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(235, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 15)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Ship to"
        '
        'ButtonEntry1
        '
        Me.ButtonEntry1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonEntry1.Location = New System.Drawing.Point(0, 343)
        Me.ButtonEntry1.Name = "ButtonEntry1"
        Me.ButtonEntry1.Size = New System.Drawing.Size(577, 43)
        Me.ButtonEntry1.TabIndex = 11
        '
        'btnChekExisting
        '
        Me.btnChekExisting.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChekExisting.ImageIndex = 11
        Me.btnChekExisting.ImageList = Me.ImageList1
        Me.btnChekExisting.Location = New System.Drawing.Point(403, 68)
        Me.btnChekExisting.Name = "btnChekExisting"
        Me.btnChekExisting.Size = New System.Drawing.Size(105, 21)
        Me.btnChekExisting.TabIndex = 12
        Me.btnChekExisting.Text = "Check Existing "
        Me.btnChekExisting.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(357, 206)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 22)
        Me.btnOK.TabIndex = 13
        Me.btnOK.Text = "&OK"
        Me.btnOK.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(461, 206)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(84, 22)
        Me.btnCancel.TabIndex = 14
        Me.btnCancel.Text = "&Cancel / close"
        Me.btnCancel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'grpOA
        '
        Me.grpOA.Controls.Add(Me.mcbTM)
        Me.grpOA.Controls.Add(Me.btnFilterTM)
        Me.grpOA.Controls.Add(Me.Label10)
        Me.grpOA.Controls.Add(Me.btnChekExisting)
        Me.grpOA.Controls.Add(Me.Label4)
        Me.grpOA.Controls.Add(Me.dtPicOADate)
        Me.grpOA.Controls.Add(Me.Label5)
        Me.grpOA.Controls.Add(Me.txtOARef)
        Me.grpOA.Controls.Add(Me.Label6)
        Me.grpOA.Controls.Add(Me.mcbDistributor)
        Me.grpOA.ImageIndex = 13
        Me.grpOA.ImageList = Me.ImageList1
        Me.grpOA.Location = New System.Drawing.Point(8, 105)
        Me.grpOA.Name = "grpOA"
        Me.grpOA.Size = New System.Drawing.Size(540, 95)
        Me.grpOA.TabIndex = 15
        Me.grpOA.Text = "OA"
        Me.grpOA.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'mcbTM
        '
        Me.mcbTM.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbTM_DesignTimeLayout.LayoutString = resources.GetString("mcbTM_DesignTimeLayout.LayoutString")
        Me.mcbTM.DesignTimeLayout = mcbTM_DesignTimeLayout
        Me.mcbTM.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbTM.DisplayMember = "SHIP_TO_ID"
        Me.mcbTM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbTM.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbTM.Location = New System.Drawing.Point(292, 42)
        Me.mcbTM.Name = "mcbTM"
        Me.mcbTM.SelectedIndex = -1
        Me.mcbTM.SelectedItem = Nothing
        Me.mcbTM.Size = New System.Drawing.Size(216, 20)
        Me.mcbTM.TabIndex = 18
        Me.mcbTM.ValueMember = "SHIP_TO_ID"
        Me.mcbTM.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnFilterTM
        '
        Me.btnFilterTM.Location = New System.Drawing.Point(513, 44)
        Me.btnFilterTM.Name = "btnFilterTM"
        Me.btnFilterTM.Size = New System.Drawing.Size(21, 18)
        Me.btnFilterTM.TabIndex = 17
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(236, 46)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 14)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "TM_ID"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(10, 205)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 16
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.Visible = False
        Me.btnDelete.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'OrderAcceptance
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(557, 230)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.grpPO)
        Me.Controls.Add(Me.grpOA)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.ButtonEntry1)
        Me.Controls.Add(Me.GridEX1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "OrderAcceptance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Data Entry Order Acceptance"
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPO.ResumeLayout(False)
        Me.grpPO.PerformLayout()
        CType(Me.mcbPOREFNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpOA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOA.ResumeLayout(False)
        Me.grpOA.PerformLayout()
        CType(Me.mcbTM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents mcbPOREFNO As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents txtProject As System.Windows.Forms.TextBox
    Private WithEvents txtDistributor As System.Windows.Forms.TextBox
    Private WithEvents txtPORefDate As System.Windows.Forms.TextBox
    Friend WithEvents dtPicOADate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents txtOARef As System.Windows.Forms.TextBox
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ButtonEntry1 As DTSProjects.ButtonEntry
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents grpPO As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents mcbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents btnChekExisting As Janus.Windows.EditControls.UIButton
    Private WithEvents grpOA As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents btnFilter As DTSProjects.ButtonSearch
    Private WithEvents btnOK As Janus.Windows.EditControls.UIButton
    Private WithEvents btnCancel As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnDelete As Janus.Windows.EditControls.UIButton
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents btnFilterTM As DTSProjects.ButtonSearch
    Friend WithEvents mcbTM As Janus.Windows.GridEX.EditControls.MultiColumnCombo

End Class
