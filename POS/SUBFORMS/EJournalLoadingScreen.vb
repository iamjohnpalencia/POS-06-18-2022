Public Class EJournalLoadingScreen
    Property ProgressBarVal As Integer
    Private Shared _instance As EJournalLoadingScreen
    Public ReadOnly Property Instance As EJournalLoadingScreen
        Get
            Return _instance
        End Get
    End Property
    Private Sub EJournalLoadingScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _instance = Me

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Select Case Label1.Text
            Case "Generating report. Please wait."
                Label1.Text = "Generating report. Please wait.."
            Case "Generating report. Please wait.."
                Label1.Text = "Generating report. Please wait..."
            Case "Generating report. Please wait..."
                Label1.Text = "Generating report. Please wait."
        End Select
    End Sub
End Class