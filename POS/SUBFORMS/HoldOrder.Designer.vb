<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HoldOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HoldOrder))
        Me.ButtonKeyboard = New System.Windows.Forms.Button()
        Me.ButtonHoldOrder = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxCustomerName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'ButtonKeyboard
        '
        Me.ButtonKeyboard.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonKeyboard.BackgroundImage = CType(resources.GetObject("ButtonKeyboard.BackgroundImage"), System.Drawing.Image)
        Me.ButtonKeyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ButtonKeyboard.FlatAppearance.BorderSize = 0
        Me.ButtonKeyboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonKeyboard.Location = New System.Drawing.Point(356, 37)
        Me.ButtonKeyboard.Name = "ButtonKeyboard"
        Me.ButtonKeyboard.Size = New System.Drawing.Size(61, 23)
        Me.ButtonKeyboard.TabIndex = 230
        Me.ButtonKeyboard.UseVisualStyleBackColor = False
        '
        'ButtonHoldOrder
        '
        Me.ButtonHoldOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.ButtonHoldOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonHoldOrder.FlatAppearance.BorderSize = 0
        Me.ButtonHoldOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonHoldOrder.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonHoldOrder.ForeColor = System.Drawing.Color.White
        Me.ButtonHoldOrder.Location = New System.Drawing.Point(15, 37)
        Me.ButtonHoldOrder.Name = "ButtonHoldOrder"
        Me.ButtonHoldOrder.Size = New System.Drawing.Size(335, 23)
        Me.ButtonHoldOrder.TabIndex = 151
        Me.ButtonHoldOrder.Text = "Hold Order"
        Me.ButtonHoldOrder.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Customer Name:"
        '
        'TextBoxCustomerName
        '
        Me.TextBoxCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxCustomerName.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxCustomerName.Location = New System.Drawing.Point(117, 8)
        Me.TextBoxCustomerName.Name = "TextBoxCustomerName"
        Me.TextBoxCustomerName.Size = New System.Drawing.Size(294, 16)
        Me.TextBoxCustomerName.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(124, 10)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(295, 16)
        Me.Label6.TabIndex = 231
        Me.Label6.Text = "_________________________________________"
        '
        'HoldOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(433, 78)
        Me.Controls.Add(Me.TextBoxCustomerName)
        Me.Controls.Add(Me.ButtonKeyboard)
        Me.Controls.Add(Me.ButtonHoldOrder)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HoldOrder"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "POS | HOLD ORDER(S)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonHoldOrder As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxCustomerName As TextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ButtonKeyboard As Button
    Friend WithEvents Label6 As Label
End Class
