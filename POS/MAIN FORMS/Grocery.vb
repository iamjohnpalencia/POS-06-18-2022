Public Class Grocery
    Private Sub Grocery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Timer1.Start()
            LabelStorename.Text = ClientStorename
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            LabelTime.Text = Date.Now.ToString("hh:mm:ss tt")
            LabelDate.Text = Date.Now.ToString("MM/dd/yyyy")
        Catch ex As Exception
            MsgBox(ex.ToString)
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub Grocery_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Login.Show()
    End Sub
End Class