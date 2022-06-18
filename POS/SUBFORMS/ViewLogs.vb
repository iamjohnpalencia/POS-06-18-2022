Public Class ViewLogs
    Property LogText As String = ""
    Public Sub New(GetLogText As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LogText = GetLogText
    End Sub
    Private Sub ViewLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Text = LogText
    End Sub
End Class