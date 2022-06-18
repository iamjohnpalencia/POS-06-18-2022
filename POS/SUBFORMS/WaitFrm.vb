Public Class WaitFrm
    Private Sub WaitFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        POS.Enabled = False
        POS.BringToFront()
        Timer1.Start()
        ProgressBar1.Maximum = 100
    End Sub
    Private Sub WaitFrm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub
End Class