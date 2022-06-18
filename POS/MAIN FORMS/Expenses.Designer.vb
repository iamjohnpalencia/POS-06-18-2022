<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Expenses
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Expenses))
        Me.ButtonKeyboard = New System.Windows.Forms.Button()
        Me.TextBoxTOTAL = New System.Windows.Forms.TextBox()
        Me.TextBoxPRICE = New System.Windows.Forms.TextBox()
        Me.TextBoxITEMINF = New System.Windows.Forms.TextBox()
        Me.ComboBoxType = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxQTY = New System.Windows.Forms.TextBox()
        Me.PictureBoxAttachment = New System.Windows.Forms.PictureBox()
        Me.TextBoxAttatchment = New System.Windows.Forms.TextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.PictureBoxAttachment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonKeyboard
        '
        Me.ButtonKeyboard.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonKeyboard.BackgroundImage = CType(resources.GetObject("ButtonKeyboard.BackgroundImage"), System.Drawing.Image)
        Me.ButtonKeyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ButtonKeyboard.FlatAppearance.BorderSize = 0
        Me.ButtonKeyboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonKeyboard.Location = New System.Drawing.Point(443, 188)
        Me.ButtonKeyboard.Name = "ButtonKeyboard"
        Me.ButtonKeyboard.Size = New System.Drawing.Size(49, 30)
        Me.ButtonKeyboard.TabIndex = 230
        Me.ButtonKeyboard.UseVisualStyleBackColor = False
        '
        'TextBoxTOTAL
        '
        Me.TextBoxTOTAL.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxTOTAL.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxTOTAL.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.TextBoxTOTAL.Location = New System.Drawing.Point(214, 166)
        Me.TextBoxTOTAL.Name = "TextBoxTOTAL"
        Me.TextBoxTOTAL.Size = New System.Drawing.Size(278, 16)
        Me.TextBoxTOTAL.TabIndex = 24
        '
        'TextBoxPRICE
        '
        Me.TextBoxPRICE.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxPRICE.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxPRICE.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.TextBoxPRICE.Location = New System.Drawing.Point(356, 131)
        Me.TextBoxPRICE.Name = "TextBoxPRICE"
        Me.TextBoxPRICE.Size = New System.Drawing.Size(136, 16)
        Me.TextBoxPRICE.TabIndex = 23
        '
        'TextBoxITEMINF
        '
        Me.TextBoxITEMINF.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxITEMINF.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxITEMINF.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.TextBoxITEMINF.Location = New System.Drawing.Point(214, 94)
        Me.TextBoxITEMINF.Name = "TextBoxITEMINF"
        Me.TextBoxITEMINF.Size = New System.Drawing.Size(278, 16)
        Me.TextBoxITEMINF.TabIndex = 21
        '
        'ComboBoxType
        '
        Me.ComboBoxType.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxType.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.ComboBoxType.FormattingEnabled = True
        Me.ComboBoxType.Location = New System.Drawing.Point(214, 50)
        Me.ComboBoxType.Name = "ComboBoxType"
        Me.ComboBoxType.Size = New System.Drawing.Size(278, 24)
        Me.ComboBoxType.TabIndex = 32
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(211, 150)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 16)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "Total Amount:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(211, 31)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 16)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Expense Type:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(353, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 16)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Price:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(211, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 16)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Quantity:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(211, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 16)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Item:"
        '
        'TextBoxQTY
        '
        Me.TextBoxQTY.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxQTY.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxQTY.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.TextBoxQTY.Location = New System.Drawing.Point(214, 131)
        Me.TextBoxQTY.Name = "TextBoxQTY"
        Me.TextBoxQTY.Size = New System.Drawing.Size(136, 16)
        Me.TextBoxQTY.TabIndex = 22
        '
        'PictureBoxAttachment
        '
        Me.PictureBoxAttachment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBoxAttachment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxAttachment.Image = CType(resources.GetObject("PictureBoxAttachment.Image"), System.Drawing.Image)
        Me.PictureBoxAttachment.Location = New System.Drawing.Point(12, 34)
        Me.PictureBoxAttachment.Name = "PictureBoxAttachment"
        Me.PictureBoxAttachment.Size = New System.Drawing.Size(193, 149)
        Me.PictureBoxAttachment.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxAttachment.TabIndex = 0
        Me.PictureBoxAttachment.TabStop = False
        '
        'TextBoxAttatchment
        '
        Me.TextBoxAttatchment.Enabled = False
        Me.TextBoxAttatchment.Location = New System.Drawing.Point(80, 72)
        Me.TextBoxAttatchment.Multiline = True
        Me.TextBoxAttatchment.Name = "TextBoxAttatchment"
        Me.TextBoxAttatchment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxAttatchment.Size = New System.Drawing.Size(100, 75)
        Me.TextBoxAttatchment.TabIndex = 1
        Me.TextBoxAttatchment.Text = resources.GetString("TextBoxAttatchment.Text")
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 228)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(506, 22)
        Me.StatusStrip1.TabIndex = 211
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
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'BackgroundWorker1
        '
        '
        'Timer1
        '
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(506, 25)
        Me.ToolStrip1.TabIndex = 231
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(77, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton1.ForeColor = System.Drawing.Color.White
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(37, 22)
        Me.ToolStripButton1.Text = "Save"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton2.ForeColor = System.Drawing.Color.White
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(115, 22)
        Me.ToolStripButton2.Text = "Select Attachment"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(211, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(283, 13)
        Me.Label2.TabIndex = 232
        Me.Label2.Text = "______________________________________________"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(211, 170)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(283, 13)
        Me.Label5.TabIndex = 233
        Me.Label5.Text = "______________________________________________"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(211, 135)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(139, 13)
        Me.Label7.TabIndex = 234
        Me.Label7.Text = "______________________"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(356, 135)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(139, 13)
        Me.Label8.TabIndex = 235
        Me.Label8.Text = "______________________"
        '
        'Expenses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(506, 250)
        Me.Controls.Add(Me.PictureBoxAttachment)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxAttatchment)
        Me.Controls.Add(Me.ComboBoxType)
        Me.Controls.Add(Me.TextBoxTOTAL)
        Me.Controls.Add(Me.ButtonKeyboard)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxPRICE)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TextBoxITEMINF)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxQTY)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Expenses"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "POS | EXPENSES"
        CType(Me.PictureBoxAttachment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxAttachment As PictureBox
    Friend WithEvents ComboBoxType As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxTOTAL As TextBox
    Friend WithEvents TextBoxPRICE As TextBox
    Friend WithEvents TextBoxQTY As TextBox
    Friend WithEvents TextBoxITEMINF As TextBox
    Friend WithEvents TextBoxAttatchment As TextBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer1 As Timer
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As ToolStripProgressBar
    Friend WithEvents ButtonKeyboard As Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
End Class
