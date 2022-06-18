Public Class ExpenseImage
    Private Sub ExpenseImage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Addexpense.Enabled = True
    End Sub
    Private Sub ExpenseImage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TopMost = True
    End Sub
End Class