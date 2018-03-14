<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStripUserName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdminToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuBackup = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuRestoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuClearDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.MnuLogOut = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuConfig = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHouseInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUser = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuWindows = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuloseAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportBill = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPrintReceipt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPrintTx = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPaySumary = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.cmdPay = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdHis = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdData = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tbnExit = New System.Windows.Forms.ToolStripButton()
        Me.StatusStrip.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripUserName
        '
        Me.ToolStripUserName.Name = "ToolStripUserName"
        Me.ToolStripUserName.Size = New System.Drawing.Size(39, 17)
        Me.ToolStripUserName.Text = "Status"
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripUserName})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 516)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.StatusStrip.Size = New System.Drawing.Size(832, 22)
        Me.StatusStrip.TabIndex = 14
        Me.StatusStrip.Text = "StatusStrip"
        '
        'MenuStrip
        '
        Me.MenuStrip.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.MnuDataToolStripMenuItem, Me.MnuReport, Me.MnuWindows, Me.HelpMenu})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.MdiWindowListItem = Me.MnuWindows
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip.Size = New System.Drawing.Size(832, 24)
        Me.MenuStrip.TabIndex = 19
        Me.MenuStrip.Text = "MenuStrip"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AdminToolStripMenuItem, Me.ToolStripMenuItem1, Me.MnuBackup, Me.MnuRestoreToolStripMenuItem, Me.MnuClearDataToolStripMenuItem, Me.ToolStripMenuItem3, Me.MnuLogOut, Me.MnuExit})
        Me.mnuFile.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(63, 20)
        Me.mnuFile.Text = "โปรแกรม"
        Me.mnuFile.Visible = False
        '
        'AdminToolStripMenuItem
        '
        Me.AdminToolStripMenuItem.Enabled = False
        Me.AdminToolStripMenuItem.Name = "AdminToolStripMenuItem"
        Me.AdminToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.AdminToolStripMenuItem.Text = "ผู้ดูแลระบบ"
        Me.AdminToolStripMenuItem.Visible = False
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(184, 6)
        Me.ToolStripMenuItem1.Visible = False
        '
        'MnuBackup
        '
        Me.MnuBackup.Name = "MnuBackup"
        Me.MnuBackup.Size = New System.Drawing.Size(187, 22)
        Me.MnuBackup.Text = "สำรองข้อมูล"
        '
        'MnuRestoreToolStripMenuItem
        '
        Me.MnuRestoreToolStripMenuItem.Name = "MnuRestoreToolStripMenuItem"
        Me.MnuRestoreToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.MnuRestoreToolStripMenuItem.Text = "นำสำรองข้อมูลกลับมาใช้"
        '
        'MnuClearDataToolStripMenuItem
        '
        Me.MnuClearDataToolStripMenuItem.Name = "MnuClearDataToolStripMenuItem"
        Me.MnuClearDataToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.MnuClearDataToolStripMenuItem.Text = "เปิดข้อมูลใหม่"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(184, 6)
        '
        'MnuLogOut
        '
        Me.MnuLogOut.Name = "MnuLogOut"
        Me.MnuLogOut.Size = New System.Drawing.Size(187, 22)
        Me.MnuLogOut.Text = "ออกจากระบบ"
        Me.MnuLogOut.Visible = False
        '
        'MnuExit
        '
        Me.MnuExit.Name = "MnuExit"
        Me.MnuExit.Size = New System.Drawing.Size(187, 22)
        Me.MnuExit.Text = "จบการทำงาน"
        '
        'MnuDataToolStripMenuItem
        '
        Me.MnuDataToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuConfig, Me.mnuHouseInfo, Me.mnuUser})
        Me.MnuDataToolStripMenuItem.Name = "MnuDataToolStripMenuItem"
        Me.MnuDataToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.MnuDataToolStripMenuItem.Text = "ตั้งค่าเริ่มตัน"
        '
        'mnuConfig
        '
        Me.mnuConfig.Name = "mnuConfig"
        Me.mnuConfig.Size = New System.Drawing.Size(139, 22)
        Me.mnuConfig.Text = "ข้อมูลพื้นฐาน"
        '
        'mnuHouseInfo
        '
        Me.mnuHouseInfo.Name = "mnuHouseInfo"
        Me.mnuHouseInfo.Size = New System.Drawing.Size(139, 22)
        Me.mnuHouseInfo.Text = "ข้อมูลบ้าน"
        '
        'mnuUser
        '
        Me.mnuUser.Name = "mnuUser"
        Me.mnuUser.Size = New System.Drawing.Size(139, 22)
        Me.mnuUser.Text = "ข้อมูลผู้ใช้งาน"
        '
        'MnuWindows
        '
        Me.MnuWindows.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuloseAll})
        Me.MnuWindows.Name = "MnuWindows"
        Me.MnuWindows.Size = New System.Drawing.Size(58, 20)
        Me.MnuWindows.Text = "หน้าต่าง"
        '
        'MnuloseAll
        '
        Me.MnuloseAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MnuloseAll.Name = "MnuloseAll"
        Me.MnuloseAll.Size = New System.Drawing.Size(152, 22)
        Me.MnuloseAll.Text = "C&lose All"
        '
        'MnuReport
        '
        Me.MnuReport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportBill, Me.MnuPrintReceipt, Me.mnuPrintTx, Me.mnuPaySumary})
        Me.MnuReport.Name = "MnuReport"
        Me.MnuReport.Size = New System.Drawing.Size(55, 20)
        Me.MnuReport.Text = "รายงาน"
        '
        'mnuReportBill
        '
        Me.mnuReportBill.Name = "mnuReportBill"
        Me.mnuReportBill.Size = New System.Drawing.Size(165, 22)
        Me.mnuReportBill.Text = "ออกใบแจ้งค่าใช้จ่าย"
        '
        'MnuPrintReceipt
        '
        Me.MnuPrintReceipt.Name = "MnuPrintReceipt"
        Me.MnuPrintReceipt.Size = New System.Drawing.Size(165, 22)
        Me.MnuPrintReceipt.Text = "พิมพ์ใบเสร็จรับเงิน"
        '
        'mnuPrintTx
        '
        Me.mnuPrintTx.Name = "mnuPrintTx"
        Me.mnuPrintTx.Size = New System.Drawing.Size(165, 22)
        Me.mnuPrintTx.Text = "สรุปการชำระ"
        '
        'mnuPaySumary
        '
        Me.mnuPaySumary.Name = "mnuPaySumary"
        Me.mnuPaySumary.Size = New System.Drawing.Size(165, 22)
        Me.mnuPaySumary.Text = "ตรวจสอบการชำระ"
        '
        'HelpMenu
        '
        Me.HelpMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator8, Me.AboutToolStripMenuItem})
        Me.HelpMenu.Name = "HelpMenu"
        Me.HelpMenu.Size = New System.Drawing.Size(62, 20)
        Me.HelpMenu.Text = "ช่วยเหลือ"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(120, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.AboutToolStripMenuItem.Text = "&About ..."
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ToolStrip1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(70, 70)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.cmdPay, Me.ToolStripSeparator1, Me.cmdHis, Me.ToolStripSeparator3, Me.cmdData, Me.ToolStripSeparator2, Me.tbnExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(104, 492)
        Me.ToolStrip1.TabIndex = 20
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None
        Me.ToolStripButton1.Enabled = False
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(101, 4)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None
        Me.ToolStripButton2.Enabled = False
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(101, 4)
        '
        'cmdPay
        '
        Me.cmdPay.ForeColor = System.Drawing.Color.White
        Me.cmdPay.Image = CType(resources.GetObject("cmdPay.Image"), System.Drawing.Image)
        Me.cmdPay.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdPay.Name = "cmdPay"
        Me.cmdPay.Size = New System.Drawing.Size(101, 90)
        Me.cmdPay.Text = "บันทึกชำระ"
        Me.cmdPay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(101, 6)
        '
        'cmdHis
        '
        Me.cmdHis.ForeColor = System.Drawing.Color.White
        Me.cmdHis.Image = CType(resources.GetObject("cmdHis.Image"), System.Drawing.Image)
        Me.cmdHis.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdHis.Name = "cmdHis"
        Me.cmdHis.Size = New System.Drawing.Size(101, 90)
        Me.cmdHis.Text = "ข้อมูลการชำระ"
        Me.cmdHis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(101, 6)
        '
        'cmdData
        '
        Me.cmdData.ForeColor = System.Drawing.Color.White
        Me.cmdData.Image = CType(resources.GetObject("cmdData.Image"), System.Drawing.Image)
        Me.cmdData.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdData.Name = "cmdData"
        Me.cmdData.Size = New System.Drawing.Size(101, 90)
        Me.cmdData.Text = "ประมวลผลรายเดือน"
        Me.cmdData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(101, 6)
        '
        'tbnExit
        '
        Me.tbnExit.ForeColor = System.Drawing.Color.White
        Me.tbnExit.Image = CType(resources.GetObject("tbnExit.Image"), System.Drawing.Image)
        Me.tbnExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbnExit.Name = "tbnExit"
        Me.tbnExit.Size = New System.Drawing.Size(101, 90)
        Me.tbnExit.Text = "จบการทำงาน"
        Me.tbnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(832, 538)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.StatusStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "frmMain"
        Me.Text = "ระบบบริหารนิติบุคคลหมู่บ้านเปี่ยมสุขรัตนาธิเบศร์-วัดสวนแก้ว"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripUserName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AdminToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuBackup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuRestoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuClearDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuLogOut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHouseInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuWindows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuloseAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdData As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdPay As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdHis As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tbnExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuConfig As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuReportBill As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPrintReceipt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPrintTx As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPaySumary As System.Windows.Forms.ToolStripMenuItem

End Class
