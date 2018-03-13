<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditHouseInfo
    Inherits PS_Management.iEditForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditHouseInfo))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtLandNo = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtPeroid = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdoHouse2 = New System.Windows.Forms.RadioButton()
        Me.rdoHouse1 = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtResidentPhone = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtResidentName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtOwnerPhone = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtHouseNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtOwnerName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtLandNo)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtRemark)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtPeroid)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtAmount)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtResidentPhone)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtResidentName)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtOwnerPhone)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtHouseNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtOwnerName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 53)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(689, 301)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'txtLandNo
        '
        Me.txtLandNo.Location = New System.Drawing.Point(507, 25)
        Me.txtLandNo.MaxLength = 50
        Me.txtLandNo.Name = "txtLandNo"
        Me.txtLandNo.Size = New System.Drawing.Size(164, 24)
        Me.txtLandNo.TabIndex = 2
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(434, 28)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(69, 18)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "โฉนดเลขที่"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(95, 195)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(576, 87)
        Me.txtRemark.TabIndex = 20
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 198)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(61, 18)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "หมายเหตุ"
        '
        'txtPeroid
        '
        Me.txtPeroid.Location = New System.Drawing.Point(507, 156)
        Me.txtPeroid.MaxLength = 20
        Me.txtPeroid.Name = "txtPeroid"
        Me.txtPeroid.Size = New System.Drawing.Size(120, 24)
        Me.txtPeroid.TabIndex = 18
        Me.txtPeroid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(633, 159)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 18)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "เดือน"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(434, 159)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 18)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "ชำระทุก"
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(507, 124)
        Me.txtAmount.MaxLength = 20
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(120, 24)
        Me.txtAmount.TabIndex = 15
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(633, 127)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 18)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "บาท"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(434, 127)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 18)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "อัตราจัดเก็บ"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.rdoHouse2)
        Me.Panel1.Controls.Add(Me.rdoHouse1)
        Me.Panel1.Location = New System.Drawing.Point(95, 127)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(149, 53)
        Me.Panel1.TabIndex = 11
        '
        'rdoHouse2
        '
        Me.rdoHouse2.AutoSize = True
        Me.rdoHouse2.Location = New System.Drawing.Point(13, 26)
        Me.rdoHouse2.Name = "rdoHouse2"
        Me.rdoHouse2.Size = New System.Drawing.Size(75, 22)
        Me.rdoHouse2.TabIndex = 1
        Me.rdoHouse2.Text = "บ้านแฝด"
        Me.rdoHouse2.UseVisualStyleBackColor = True
        '
        'rdoHouse1
        '
        Me.rdoHouse1.AutoSize = True
        Me.rdoHouse1.Checked = True
        Me.rdoHouse1.Location = New System.Drawing.Point(13, 3)
        Me.rdoHouse1.Name = "rdoHouse1"
        Me.rdoHouse1.Size = New System.Drawing.Size(107, 22)
        Me.rdoHouse1.TabIndex = 0
        Me.rdoHouse1.TabStop = True
        Me.rdoHouse1.Text = "บ้านทาวน์เฮ้าส์"
        Me.rdoHouse1.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 127)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 18)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "ประเภทบ้าน"
        '
        'txtResidentPhone
        '
        Me.txtResidentPhone.Location = New System.Drawing.Point(507, 85)
        Me.txtResidentPhone.MaxLength = 50
        Me.txtResidentPhone.Name = "txtResidentPhone"
        Me.txtResidentPhone.Size = New System.Drawing.Size(164, 24)
        Me.txtResidentPhone.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(434, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 18)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "เบอร์โทร"
        '
        'txtResidentName
        '
        Me.txtResidentName.Location = New System.Drawing.Point(95, 88)
        Me.txtResidentName.MaxLength = 255
        Me.txtResidentName.Name = "txtResidentName"
        Me.txtResidentName.Size = New System.Drawing.Size(321, 24)
        Me.txtResidentName.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 18)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "ผู้อาศัยปัจจุบัน"
        '
        'txtOwnerPhone
        '
        Me.txtOwnerPhone.Location = New System.Drawing.Point(507, 55)
        Me.txtOwnerPhone.MaxLength = 50
        Me.txtOwnerPhone.Name = "txtOwnerPhone"
        Me.txtOwnerPhone.Size = New System.Drawing.Size(164, 24)
        Me.txtOwnerPhone.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(434, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 18)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "เบอร์โทร"
        '
        'txtHouseNo
        '
        Me.txtHouseNo.Location = New System.Drawing.Point(95, 28)
        Me.txtHouseNo.MaxLength = 50
        Me.txtHouseNo.Name = "txtHouseNo"
        Me.txtHouseNo.Size = New System.Drawing.Size(149, 24)
        Me.txtHouseNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 31)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "บ้านเลขที่"
        '
        'txtOwnerName
        '
        Me.txtOwnerName.Location = New System.Drawing.Point(95, 58)
        Me.txtOwnerName.MaxLength = 255
        Me.txtOwnerName.Name = "txtOwnerName"
        Me.txtOwnerName.Size = New System.Drawing.Size(321, 24)
        Me.txtOwnerName.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 18)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "เจ้าบ้าน"
        '
        'frmEditHouseInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(713, 358)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmEditHouseInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtHouseNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOwnerName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtOwnerPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtResidentPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtResidentName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoHouse2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoHouse1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtPeroid As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtLandNo As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label

End Class
