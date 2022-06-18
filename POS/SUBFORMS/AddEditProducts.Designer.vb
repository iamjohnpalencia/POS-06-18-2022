<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddEditProducts
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddEditProducts))
        Me.TextBoxProductCode = New System.Windows.Forms.TextBox()
        Me.PictureBoxProductImage = New System.Windows.Forms.PictureBox()
        Me.TextBoxProductName = New System.Windows.Forms.TextBox()
        Me.TextBoxBarcode = New System.Windows.Forms.TextBox()
        Me.TextBoxPrice = New System.Windows.Forms.TextBox()
        Me.TextBoxDescription = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ButtonKeyboard = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxbase64 = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.TextBoxCriticalLimit = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        CType(Me.PictureBoxProductImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxProductCode
        '
        Me.TextBoxProductCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxProductCode.Location = New System.Drawing.Point(10, 23)
        Me.TextBoxProductCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBoxProductCode.Name = "TextBoxProductCode"
        Me.TextBoxProductCode.Size = New System.Drawing.Size(232, 16)
        Me.TextBoxProductCode.TabIndex = 0
        '
        'PictureBoxProductImage
        '
        Me.PictureBoxProductImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBoxProductImage.Image = CType(resources.GetObject("PictureBoxProductImage.Image"), System.Drawing.Image)
        Me.PictureBoxProductImage.Location = New System.Drawing.Point(251, 23)
        Me.PictureBoxProductImage.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PictureBoxProductImage.Name = "PictureBoxProductImage"
        Me.PictureBoxProductImage.Size = New System.Drawing.Size(202, 169)
        Me.PictureBoxProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxProductImage.TabIndex = 2
        Me.PictureBoxProductImage.TabStop = False
        '
        'TextBoxProductName
        '
        Me.TextBoxProductName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxProductName.Location = New System.Drawing.Point(10, 70)
        Me.TextBoxProductName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBoxProductName.Name = "TextBoxProductName"
        Me.TextBoxProductName.Size = New System.Drawing.Size(232, 16)
        Me.TextBoxProductName.TabIndex = 3
        '
        'TextBoxBarcode
        '
        Me.TextBoxBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxBarcode.Location = New System.Drawing.Point(10, 117)
        Me.TextBoxBarcode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBoxBarcode.Name = "TextBoxBarcode"
        Me.TextBoxBarcode.Size = New System.Drawing.Size(232, 16)
        Me.TextBoxBarcode.TabIndex = 4
        '
        'TextBoxPrice
        '
        Me.TextBoxPrice.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxPrice.Location = New System.Drawing.Point(10, 164)
        Me.TextBoxPrice.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBoxPrice.Name = "TextBoxPrice"
        Me.TextBoxPrice.Size = New System.Drawing.Size(232, 16)
        Me.TextBoxPrice.TabIndex = 5
        '
        'TextBoxDescription
        '
        Me.TextBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxDescription.Location = New System.Drawing.Point(10, 211)
        Me.TextBoxDescription.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBoxDescription.Name = "TextBoxDescription"
        Me.TextBoxDescription.Size = New System.Drawing.Size(232, 16)
        Me.TextBoxDescription.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Product Code:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Product Name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 97)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Product Barcode:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 144)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 16)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Product Price:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 191)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 16)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Product Description:"
        '
        'ButtonKeyboard
        '
        Me.ButtonKeyboard.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonKeyboard.BackgroundImage = CType(resources.GetObject("ButtonKeyboard.BackgroundImage"), System.Drawing.Image)
        Me.ButtonKeyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ButtonKeyboard.FlatAppearance.BorderSize = 0
        Me.ButtonKeyboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonKeyboard.Location = New System.Drawing.Point(392, 246)
        Me.ButtonKeyboard.Name = "ButtonKeyboard"
        Me.ButtonKeyboard.Size = New System.Drawing.Size(61, 35)
        Me.ButtonKeyboard.TabIndex = 234
        Me.ButtonKeyboard.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(251, 246)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(135, 35)
        Me.Button1.TabIndex = 232
        Me.Button1.Text = "Submit"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(251, 199)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(202, 35)
        Me.Button3.TabIndex = 235
        Me.Button3.Text = "Select Product Image"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(248, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 16)
        Me.Label6.TabIndex = 236
        Me.Label6.Text = "Product Image:"
        '
        'TextBoxbase64
        '
        Me.TextBoxbase64.Location = New System.Drawing.Point(15, 308)
        Me.TextBoxbase64.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBoxbase64.Name = "TextBoxbase64"
        Me.TextBoxbase64.ReadOnly = True
        Me.TextBoxbase64.Size = New System.Drawing.Size(232, 23)
        Me.TextBoxbase64.TabIndex = 237
        Me.TextBoxbase64.Text = resources.GetString("TextBoxbase64.Text")
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'BackgroundWorker1
        '
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 297)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(462, 22)
        Me.StatusStrip1.TabIndex = 238
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripStatusLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(44, 17)
        Me.ToolStripStatusLabel1.Text = "Status"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(200, 16)
        '
        'TextBoxCriticalLimit
        '
        Me.TextBoxCriticalLimit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxCriticalLimit.Location = New System.Drawing.Point(10, 258)
        Me.TextBoxCriticalLimit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBoxCriticalLimit.Name = "TextBoxCriticalLimit"
        Me.TextBoxCriticalLimit.Size = New System.Drawing.Size(232, 16)
        Me.TextBoxCriticalLimit.TabIndex = 239
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 238)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 16)
        Me.Label7.TabIndex = 240
        Me.Label7.Text = "Set Critical Limit:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(239, 16)
        Me.Label8.TabIndex = 241
        Me.Label8.Text = "_________________________________"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(239, 16)
        Me.Label9.TabIndex = 242
        Me.Label9.Text = "_________________________________"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 119)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(239, 16)
        Me.Label10.TabIndex = 243
        Me.Label10.Text = "_________________________________"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 166)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(239, 16)
        Me.Label11.TabIndex = 244
        Me.Label11.Text = "_________________________________"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 213)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(239, 16)
        Me.Label12.TabIndex = 245
        Me.Label12.Text = "_________________________________"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 260)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(239, 16)
        Me.Label13.TabIndex = 246
        Me.Label13.Text = "_________________________________"
        '
        'AddEditProducts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(462, 319)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxCriticalLimit)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TextBoxbase64)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.ButtonKeyboard)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxDescription)
        Me.Controls.Add(Me.TextBoxPrice)
        Me.Controls.Add(Me.TextBoxBarcode)
        Me.Controls.Add(Me.TextBoxProductName)
        Me.Controls.Add(Me.PictureBoxProductImage)
        Me.Controls.Add(Me.TextBoxProductCode)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddEditProducts"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "POS | ADD,EDIT PRODUCTS"
        CType(Me.PictureBoxProductImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxProductCode As TextBox
    Friend WithEvents PictureBoxProductImage As PictureBox
    Friend WithEvents TextBoxProductName As TextBox
    Friend WithEvents TextBoxBarcode As TextBox
    Friend WithEvents TextBoxPrice As TextBox
    Friend WithEvents TextBoxDescription As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ButtonKeyboard As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxbase64 As TextBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As ToolStripProgressBar
    Friend WithEvents TextBoxCriticalLimit As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
End Class
