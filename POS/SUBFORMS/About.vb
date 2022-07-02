Public Class About
    Private Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel1.Top = (Me.Height - Panel1.Height) / 2
        Panel1.Left = (Me.Width - Panel1.Width) / 2
        LabelVersion.Text = My.Settings.Version
        Label12.Text = S_Dev_Comp_Name
        LabelVersion.Text = My.Settings.Version
        Label17.Text = My.Settings.Footer
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim url As String = "https://www.facebook.com/FamousBelgianWafflesOFFICIAL/"
        Process.Start(url)
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)
        Dim url As String = "https://famousbelgianwaffles.com/"
        Process.Start(url)
    End Sub

    Private Sub Label12_DoubleClick(sender As Object, e As EventArgs) Handles Label12.DoubleClick
        MsgBox("v1.1-07022022")
    End Sub
End Class