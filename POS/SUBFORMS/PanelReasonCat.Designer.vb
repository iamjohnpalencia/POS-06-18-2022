<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PanelReasonCat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PanelReasonCat))
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TextBoxReasonsCat = New System.Windows.Forms.TextBox()
        Me.ButtonKeyboard = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Location = New System.Drawing.Point(15, 57)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(213, 27)
        Me.Button6.TabIndex = 14
        Me.Button6.Text = "Submit"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(12, 9)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 16)
        Me.Label14.TabIndex = 13
        Me.Label14.Text = "Category:"
        '
        'TextBoxReasonsCat
        '
        Me.TextBoxReasonsCat.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxReasonsCat.Location = New System.Drawing.Point(15, 28)
        Me.TextBoxReasonsCat.Name = "TextBoxReasonsCat"
        Me.TextBoxReasonsCat.Size = New System.Drawing.Size(280, 23)
        Me.TextBoxReasonsCat.TabIndex = 12
        '
        'ButtonKeyboard
        '
        Me.ButtonKeyboard.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonKeyboard.BackgroundImage = CType(resources.GetObject("ButtonKeyboard.BackgroundImage"), System.Drawing.Image)
        Me.ButtonKeyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ButtonKeyboard.FlatAppearance.BorderSize = 0
        Me.ButtonKeyboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonKeyboard.Location = New System.Drawing.Point(234, 57)
        Me.ButtonKeyboard.Name = "ButtonKeyboard"
        Me.ButtonKeyboard.Size = New System.Drawing.Size(61, 27)
        Me.ButtonKeyboard.TabIndex = 232
        Me.ButtonKeyboard.UseVisualStyleBackColor = False
        '
        'PanelReasonCat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(309, 95)
        Me.Controls.Add(Me.ButtonKeyboard)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.TextBoxReasonsCat)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PanelReasonCat"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "POS | CATEGORY"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button6 As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents TextBoxReasonsCat As TextBox
    Friend WithEvents ButtonKeyboard As Button
End Class
