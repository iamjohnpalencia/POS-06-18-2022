<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SeniorDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SeniorDetails))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxSENIORID = New System.Windows.Forms.TextBox()
        Me.TextBoxSENIORNAME = New System.Windows.Forms.TextBox()
        Me.ButtonCANCEL = New System.Windows.Forms.Button()
        Me.ButtonSubmit = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxNumberOfGuest = New System.Windows.Forms.NumericUpDown()
        Me.TextBoxNumberOfID = New System.Windows.Forms.NumericUpDown()
        Me.ButtonKeyboard = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxPhoneNumber = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.TextBoxNumberOfGuest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBoxNumberOfID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 16)
        Me.Label1.TabIndex = 104
        Me.Label1.Text = "Identification Number"
        '
        'TextBoxSENIORID
        '
        Me.TextBoxSENIORID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxSENIORID.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxSENIORID.Location = New System.Drawing.Point(16, 127)
        Me.TextBoxSENIORID.Name = "TextBoxSENIORID"
        Me.TextBoxSENIORID.Size = New System.Drawing.Size(360, 16)
        Me.TextBoxSENIORID.TabIndex = 0
        '
        'TextBoxSENIORNAME
        '
        Me.TextBoxSENIORNAME.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxSENIORNAME.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxSENIORNAME.Location = New System.Drawing.Point(16, 179)
        Me.TextBoxSENIORNAME.Name = "TextBoxSENIORNAME"
        Me.TextBoxSENIORNAME.Size = New System.Drawing.Size(360, 16)
        Me.TextBoxSENIORNAME.TabIndex = 1
        '
        'ButtonCANCEL
        '
        Me.ButtonCANCEL.BackColor = System.Drawing.Color.Red
        Me.ButtonCANCEL.FlatAppearance.BorderSize = 0
        Me.ButtonCANCEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCANCEL.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCANCEL.ForeColor = System.Drawing.Color.White
        Me.ButtonCANCEL.Location = New System.Drawing.Point(263, 256)
        Me.ButtonCANCEL.Name = "ButtonCANCEL"
        Me.ButtonCANCEL.Size = New System.Drawing.Size(114, 31)
        Me.ButtonCANCEL.TabIndex = 106
        Me.ButtonCANCEL.Text = "Cancel"
        Me.ButtonCANCEL.UseVisualStyleBackColor = False
        '
        'ButtonSubmit
        '
        Me.ButtonSubmit.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.ButtonSubmit.FlatAppearance.BorderSize = 0
        Me.ButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSubmit.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSubmit.ForeColor = System.Drawing.Color.White
        Me.ButtonSubmit.Location = New System.Drawing.Point(16, 256)
        Me.ButtonSubmit.Name = "ButtonSubmit"
        Me.ButtonSubmit.Size = New System.Drawing.Size(242, 31)
        Me.ButtonSubmit.TabIndex = 103
        Me.ButtonSubmit.Text = "Submit"
        Me.ButtonSubmit.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 157)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 16)
        Me.Label2.TabIndex = 105
        Me.Label2.Text = "Full Name"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(13, 129)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(365, 16)
        Me.Label6.TabIndex = 252
        Me.Label6.Text = "___________________________________________________"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 181)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(365, 16)
        Me.Label3.TabIndex = 253
        Me.Label3.Text = "___________________________________________________"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 16)
        Me.Label4.TabIndex = 256
        Me.Label4.Text = "No. of guest"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 16)
        Me.Label5.TabIndex = 257
        Me.Label5.Text = "No. of SR ID"
        '
        'TextBoxNumberOfGuest
        '
        Me.TextBoxNumberOfGuest.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TextBoxNumberOfGuest.Location = New System.Drawing.Point(16, 27)
        Me.TextBoxNumberOfGuest.Name = "TextBoxNumberOfGuest"
        Me.TextBoxNumberOfGuest.Size = New System.Drawing.Size(187, 23)
        Me.TextBoxNumberOfGuest.TabIndex = 260
        Me.TextBoxNumberOfGuest.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'TextBoxNumberOfID
        '
        Me.TextBoxNumberOfID.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TextBoxNumberOfID.Location = New System.Drawing.Point(16, 80)
        Me.TextBoxNumberOfID.Name = "TextBoxNumberOfID"
        Me.TextBoxNumberOfID.Size = New System.Drawing.Size(187, 23)
        Me.TextBoxNumberOfID.TabIndex = 261
        Me.TextBoxNumberOfID.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ButtonKeyboard
        '
        Me.ButtonKeyboard.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonKeyboard.BackgroundImage = CType(resources.GetObject("ButtonKeyboard.BackgroundImage"), System.Drawing.Image)
        Me.ButtonKeyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ButtonKeyboard.FlatAppearance.BorderSize = 0
        Me.ButtonKeyboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonKeyboard.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonKeyboard.Location = New System.Drawing.Point(315, 12)
        Me.ButtonKeyboard.Name = "ButtonKeyboard"
        Me.ButtonKeyboard.Size = New System.Drawing.Size(61, 35)
        Me.ButtonKeyboard.TabIndex = 262
        Me.ButtonKeyboard.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(13, 209)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(97, 16)
        Me.Label7.TabIndex = 263
        Me.Label7.Text = "Phone Number:"
        '
        'TextBoxPhoneNumber
        '
        Me.TextBoxPhoneNumber.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxPhoneNumber.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxPhoneNumber.Location = New System.Drawing.Point(16, 229)
        Me.TextBoxPhoneNumber.Name = "TextBoxPhoneNumber"
        Me.TextBoxPhoneNumber.Size = New System.Drawing.Size(360, 16)
        Me.TextBoxPhoneNumber.TabIndex = 264
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(13, 231)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(365, 16)
        Me.Label8.TabIndex = 265
        Me.Label8.Text = "___________________________________________________"
        '
        'SeniorDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(388, 299)
        Me.Controls.Add(Me.TextBoxPhoneNumber)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ButtonKeyboard)
        Me.Controls.Add(Me.TextBoxNumberOfID)
        Me.Controls.Add(Me.TextBoxNumberOfGuest)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxSENIORID)
        Me.Controls.Add(Me.TextBoxSENIORNAME)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ButtonCANCEL)
        Me.Controls.Add(Me.ButtonSubmit)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SeniorDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DISCOUNT DETAILS"
        CType(Me.TextBoxNumberOfGuest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBoxNumberOfID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxSENIORID As TextBox
    Friend WithEvents TextBoxSENIORNAME As TextBox
    Friend WithEvents ButtonCANCEL As Button
    Friend WithEvents ButtonSubmit As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxNumberOfGuest As NumericUpDown
    Friend WithEvents TextBoxNumberOfID As NumericUpDown
    Friend WithEvents ButtonKeyboard As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxPhoneNumber As TextBox
    Friend WithEvents Label8 As Label
End Class
