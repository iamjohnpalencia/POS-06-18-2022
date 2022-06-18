<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewStockEntry
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewStockEntry))
        Me.Label23 = New System.Windows.Forms.Label()
        Me.ButtonENTRYADDSTOCK = New System.Windows.Forms.Button()
        Me.ComboBoxDESC = New System.Windows.Forms.ComboBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBoxEPrimary = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.TextBoxESecondary = New System.Windows.Forms.TextBox()
        Me.TextBoxEQuantity = New System.Windows.Forms.TextBox()
        Me.TextBoxEFPUnit = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TextBoxEFPrimaryVal = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.TextBoxEFSUnit = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.TextBoxEFSecondVal = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.TextBoxEServingValue = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.TextBoxEServingVal = New System.Windows.Forms.TextBox()
        Me.TextBoxENoServings = New System.Windows.Forms.TextBox()
        Me.ButtonKeyboard = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(12, 14)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(94, 19)
        Me.Label23.TabIndex = 19
        Me.Label23.Text = "Description:"
        '
        'ButtonENTRYADDSTOCK
        '
        Me.ButtonENTRYADDSTOCK.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.ButtonENTRYADDSTOCK.FlatAppearance.BorderSize = 0
        Me.ButtonENTRYADDSTOCK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonENTRYADDSTOCK.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ButtonENTRYADDSTOCK.ForeColor = System.Drawing.Color.White
        Me.ButtonENTRYADDSTOCK.Location = New System.Drawing.Point(12, 267)
        Me.ButtonENTRYADDSTOCK.Name = "ButtonENTRYADDSTOCK"
        Me.ButtonENTRYADDSTOCK.Size = New System.Drawing.Size(295, 32)
        Me.ButtonENTRYADDSTOCK.TabIndex = 27
        Me.ButtonENTRYADDSTOCK.Text = "Submit"
        Me.ButtonENTRYADDSTOCK.UseVisualStyleBackColor = False
        '
        'ComboBoxDESC
        '
        Me.ComboBoxDESC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ComboBoxDESC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComboBoxDESC.BackColor = System.Drawing.SystemColors.Control
        Me.ComboBoxDESC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxDESC.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBoxDESC.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxDESC.FormattingEnabled = True
        Me.ComboBoxDESC.Location = New System.Drawing.Point(14, 36)
        Me.ComboBoxDESC.Name = "ComboBoxDESC"
        Me.ComboBoxDESC.Size = New System.Drawing.Size(362, 27)
        Me.ComboBoxDESC.TabIndex = 32
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.749999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(24, 277)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(192, 22)
        Me.TextBox1.TabIndex = 53
        '
        'TextBoxEPrimary
        '
        Me.TextBoxEPrimary.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxEPrimary.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEPrimary.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TextBoxEPrimary.Location = New System.Drawing.Point(15, 144)
        Me.TextBoxEPrimary.Name = "TextBoxEPrimary"
        Me.TextBoxEPrimary.ReadOnly = True
        Me.TextBoxEPrimary.Size = New System.Drawing.Size(116, 16)
        Me.TextBoxEPrimary.TabIndex = 37
        Me.TextBoxEPrimary.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(12, 71)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(141, 19)
        Me.Label20.TabIndex = 52
        Me.Label20.Text = "Please input stock:"
        '
        'TextBoxESecondary
        '
        Me.TextBoxESecondary.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxESecondary.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxESecondary.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TextBoxESecondary.Location = New System.Drawing.Point(15, 188)
        Me.TextBoxESecondary.Name = "TextBoxESecondary"
        Me.TextBoxESecondary.ReadOnly = True
        Me.TextBoxESecondary.Size = New System.Drawing.Size(116, 16)
        Me.TextBoxESecondary.TabIndex = 38
        Me.TextBoxESecondary.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxEQuantity
        '
        Me.TextBoxEQuantity.BackColor = System.Drawing.Color.White
        Me.TextBoxEQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEQuantity.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.TextBoxEQuantity.Location = New System.Drawing.Point(159, 71)
        Me.TextBoxEQuantity.Name = "TextBoxEQuantity"
        Me.TextBoxEQuantity.Size = New System.Drawing.Size(217, 20)
        Me.TextBoxEQuantity.TabIndex = 51
        Me.TextBoxEQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxEFPUnit
        '
        Me.TextBoxEFPUnit.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxEFPUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEFPUnit.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TextBoxEFPUnit.Location = New System.Drawing.Point(262, 144)
        Me.TextBoxEFPUnit.Name = "TextBoxEFPUnit"
        Me.TextBoxEFPUnit.ReadOnly = True
        Me.TextBoxEFPUnit.Size = New System.Drawing.Size(114, 16)
        Me.TextBoxEFPUnit.TabIndex = 39
        Me.TextBoxEFPUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label21.Location = New System.Drawing.Point(259, 215)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(105, 16)
        Me.Label21.TabIndex = 50
        Me.Label21.Text = "No. of Serving(s)"
        '
        'TextBoxEFPrimaryVal
        '
        Me.TextBoxEFPrimaryVal.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxEFPrimaryVal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEFPrimaryVal.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TextBoxEFPrimaryVal.Location = New System.Drawing.Point(140, 144)
        Me.TextBoxEFPrimaryVal.Name = "TextBoxEFPrimaryVal"
        Me.TextBoxEFPrimaryVal.ReadOnly = True
        Me.TextBoxEFPrimaryVal.Size = New System.Drawing.Size(114, 16)
        Me.TextBoxEFPrimaryVal.TabIndex = 40
        Me.TextBoxEFPrimaryVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label22.Location = New System.Drawing.Point(12, 215)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(87, 16)
        Me.Label22.TabIndex = 49
        Me.Label22.Text = "Serving Value"
        '
        'TextBoxEFSUnit
        '
        Me.TextBoxEFSUnit.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxEFSUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEFSUnit.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TextBoxEFSUnit.Location = New System.Drawing.Point(262, 188)
        Me.TextBoxEFSUnit.Name = "TextBoxEFSUnit"
        Me.TextBoxEFSUnit.ReadOnly = True
        Me.TextBoxEFSUnit.Size = New System.Drawing.Size(114, 16)
        Me.TextBoxEFSUnit.TabIndex = 41
        Me.TextBoxEFSUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label24.Location = New System.Drawing.Point(139, 215)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(77, 16)
        Me.Label24.TabIndex = 48
        Me.Label24.Text = "Serving Unit"
        '
        'TextBoxEFSecondVal
        '
        Me.TextBoxEFSecondVal.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxEFSecondVal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEFSecondVal.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TextBoxEFSecondVal.Location = New System.Drawing.Point(140, 188)
        Me.TextBoxEFSecondVal.Name = "TextBoxEFSecondVal"
        Me.TextBoxEFSecondVal.ReadOnly = True
        Me.TextBoxEFSecondVal.Size = New System.Drawing.Size(114, 16)
        Me.TextBoxEFSecondVal.TabIndex = 42
        Me.TextBoxEFSecondVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label25.Location = New System.Drawing.Point(12, 171)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(101, 16)
        Me.Label25.TabIndex = 47
        Me.Label25.Text = "Secondary Total"
        '
        'TextBoxEServingValue
        '
        Me.TextBoxEServingValue.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxEServingValue.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEServingValue.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TextBoxEServingValue.Location = New System.Drawing.Point(140, 233)
        Me.TextBoxEServingValue.Name = "TextBoxEServingValue"
        Me.TextBoxEServingValue.ReadOnly = True
        Me.TextBoxEServingValue.Size = New System.Drawing.Size(114, 16)
        Me.TextBoxEServingValue.TabIndex = 43
        Me.TextBoxEServingValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Label26.Location = New System.Drawing.Point(12, 127)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(85, 16)
        Me.Label26.TabIndex = 46
        Me.Label26.Text = "Primary Total"
        '
        'TextBoxEServingVal
        '
        Me.TextBoxEServingVal.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxEServingVal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEServingVal.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TextBoxEServingVal.Location = New System.Drawing.Point(15, 233)
        Me.TextBoxEServingVal.Name = "TextBoxEServingVal"
        Me.TextBoxEServingVal.ReadOnly = True
        Me.TextBoxEServingVal.Size = New System.Drawing.Size(116, 16)
        Me.TextBoxEServingVal.TabIndex = 44
        Me.TextBoxEServingVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxENoServings
        '
        Me.TextBoxENoServings.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxENoServings.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxENoServings.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.TextBoxENoServings.Location = New System.Drawing.Point(264, 233)
        Me.TextBoxENoServings.Name = "TextBoxENoServings"
        Me.TextBoxENoServings.ReadOnly = True
        Me.TextBoxENoServings.Size = New System.Drawing.Size(112, 16)
        Me.TextBoxENoServings.TabIndex = 45
        Me.TextBoxENoServings.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ButtonKeyboard
        '
        Me.ButtonKeyboard.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonKeyboard.BackgroundImage = CType(resources.GetObject("ButtonKeyboard.BackgroundImage"), System.Drawing.Image)
        Me.ButtonKeyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ButtonKeyboard.FlatAppearance.BorderSize = 0
        Me.ButtonKeyboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonKeyboard.Location = New System.Drawing.Point(315, 267)
        Me.ButtonKeyboard.Name = "ButtonKeyboard"
        Me.ButtonKeyboard.Size = New System.Drawing.Size(61, 32)
        Me.ButtonKeyboard.TabIndex = 232
        Me.ButtonKeyboard.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(15, 147)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(120, 16)
        Me.Label8.TabIndex = 244
        Me.Label8.Text = "________________"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 191)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 16)
        Me.Label1.TabIndex = 245
        Me.Label1.Text = "________________"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 236)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 16)
        Me.Label2.TabIndex = 246
        Me.Label2.Text = "________________"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(261, 236)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 16)
        Me.Label3.TabIndex = 247
        Me.Label3.Text = "________________"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(139, 236)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 16)
        Me.Label4.TabIndex = 248
        Me.Label4.Text = "________________"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(161, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(218, 16)
        Me.Label5.TabIndex = 249
        Me.Label5.Text = "______________________________"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(260, 147)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(120, 16)
        Me.Label6.TabIndex = 250
        Me.Label6.Text = "________________"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(260, 191)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 16)
        Me.Label7.TabIndex = 251
        Me.Label7.Text = "________________"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(139, 147)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(120, 16)
        Me.Label9.TabIndex = 252
        Me.Label9.Text = "________________"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(137, 191)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(120, 16)
        Me.Label10.TabIndex = 253
        Me.Label10.Text = "________________"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(12, 106)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(368, 16)
        Me.Label11.TabIndex = 254
        Me.Label11.Text = "------------------------------------------------------------------------" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'NewStockEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(393, 313)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.ButtonKeyboard)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.ButtonENTRYADDSTOCK)
        Me.Controls.Add(Me.TextBoxENoServings)
        Me.Controls.Add(Me.ComboBoxDESC)
        Me.Controls.Add(Me.TextBoxEServingVal)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.TextBoxEPrimary)
        Me.Controls.Add(Me.TextBoxEServingValue)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.TextBoxESecondary)
        Me.Controls.Add(Me.TextBoxEFSecondVal)
        Me.Controls.Add(Me.TextBoxEQuantity)
        Me.Controls.Add(Me.TextBoxEFPUnit)
        Me.Controls.Add(Me.TextBoxEFSUnit)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.TextBoxEFPrimaryVal)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NewStockEntry"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "POS | STOCK ENTRY"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label23 As Label
    Friend WithEvents ButtonENTRYADDSTOCK As Button
    Friend WithEvents ComboBoxDESC As ComboBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBoxEPrimary As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents TextBoxESecondary As TextBox
    Friend WithEvents TextBoxEQuantity As TextBox
    Friend WithEvents TextBoxEFPUnit As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents TextBoxEFPrimaryVal As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents TextBoxEFSUnit As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents TextBoxEFSecondVal As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents TextBoxEServingValue As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents TextBoxEServingVal As TextBox
    Friend WithEvents TextBoxENoServings As TextBox
    Friend WithEvents ButtonKeyboard As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
End Class
