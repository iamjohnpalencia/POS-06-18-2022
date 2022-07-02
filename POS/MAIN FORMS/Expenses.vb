Imports System.Drawing.Imaging
Imports System.IO
Imports System.Threading

Public Class Expenses
    Dim thread1 As Thread
    Dim encodeType As ImageFormat = ImageFormat.Jpeg
    Dim decodingstring As String = String.Empty
    Private ImagePath As String = ""
    Dim insertcurrenttime As String
    Dim insertcurrentdate As String
    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        If Application.OpenForms().OfType(Of Expenses).Any Then
            Me.BringToFront()
        Else
        End If
    End Sub
    Dim threadList As List(Of Thread) = New List(Of Thread)
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            For i = 0 To 100
                ToolStripStatusLabel1.Text = "Uploading image please wait " & i & " %"
                BackgroundWorker1.ReportProgress(i)
                If i = 10 Then
                    thread1 = New Thread(AddressOf convertimage)
                    thread1.Start()
                    threadList.Add(thread1)
                ElseIf i = 100 Then

                End If
                Thread.Sleep(10)
            Next
            For Each t In threadList
                t.Join()
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Expenses/BackgroundWorker1(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If e.Error IsNot Nothing Then
            '' if BackgroundWorker terminated due to error
            MessageBox.Show(e.Error.Message)
            Label1.Text = "Error occurred!"
        ElseIf e.Cancelled Then
            '' otherwise if it was cancelled
            MessageBox.Show("Task cancelled!")
            Label1.Text = "Task Cancelled!"
        Else
            '' otherwise it completed normally
            ToolStripButton1.Enabled = True
            ToolStripButton2.Enabled = True
            ToolStripStatusLabel1.Text = "Successfully Uploaded!"
            'expensesload()
        End If
    End Sub
    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        ImagePath = OpenFileDialog1.FileName
    End Sub
    Private Sub convertimage()
        Try
            Dim ImageToConvert As Bitmap = Bitmap.FromFile(ImagePath)
            ImageToConvert.MakeTransparent()
            Dim encoding As String = String.Empty
            If ImagePath.ToLower.EndsWith(".jpg") Then
                encodeType = ImageFormat.Jpeg
            ElseIf ImagePath.ToLower.EndsWith(".png") Then
                encodeType = ImageFormat.Png
            ElseIf ImagePath.ToLower.EndsWith(".gif") Then
                encodeType = ImageFormat.Gif
            ElseIf ImagePath.ToLower.EndsWith(".bmp") Then
                encodeType = ImageFormat.Bmp
            End If
            decodingstring = encoding
            TextBoxAttatchment.Text = ImageToBase64(ImageToConvert, encodeType)
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Expenses/convertimage(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        insertcurrenttime = TimeOfDay.ToString("h:mm:ss")
        insertcurrentdate = String.Format("{0:yyyy/MM/dd}", DateTime.Now)
    End Sub
    Private Sub TextBoxQTY_TextChanged(sender As Object, e As EventArgs) Handles TextBoxQTY.TextChanged
        TextBoxTOTAL.Text = Val(TextBoxQTY.Text) * Val(TextBoxPRICE.Text)
    End Sub
    Private Sub TextBoxPRICE_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPRICE.TextChanged
        TextBoxTOTAL.Text = Val(TextBoxQTY.Text) * Val(TextBoxPRICE.Text)
    End Sub

    Private Sub Expenses_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Addexpense.Enabled = True
    End Sub
    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub
    Private Sub TextBoxQTY_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxQTY.KeyPress, TextBoxPRICE.KeyPress, TextBoxTOTAL.KeyPress
        Try
            Numeric(sender, e)
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Expenses/keypress-Numeric(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub TextBoxITEMINF_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxITEMINF.KeyPress
        Try
            If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Expenses/keypress-DisallowedCharacters(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            If String.IsNullOrWhiteSpace(ComboBoxType.Text) Then
                MsgBox("Fill up blanks")
            ElseIf String.IsNullOrWhiteSpace(TextBoxITEMINF.Text) Then
                MsgBox("Fill up blanks")
            ElseIf String.IsNullOrWhiteSpace(TextBoxQTY.Text) Then
                MsgBox("Fill up blanks")
            ElseIf String.IsNullOrWhiteSpace(TextBoxPRICE.Text) Then
                MsgBox("Fill up blanks")
            Else
                With Addexpense
                    .ButtonClickCount += 1
                    .DataGridViewExpenses.Rows.Add(ComboBoxType.Text, TextBoxITEMINF.Text, TextBoxQTY.Text, TextBoxPRICE.Text, TextBoxTOTAL.Text, TextBoxAttatchment.Text, .ButtonClickCount)
                    .Label1.Text = SumOfColumnsToDecimal(.DataGridViewExpenses, 4)
                End With
                Me.Close()
                ClearTextBox(root:=Me)
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Expenses/ToolStripButton1: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        TextBoxAttatchment.Clear()
        Try
            With OpenFileDialog1
                .Filter = ("Images | *.png; *.bmp; *.jpg; *.jpeg; *.gif; *.ico;")
                .FilterIndex = 4
            End With
            OpenFileDialog1.FileName = ""
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                If My.Computer.FileSystem.FileExists(ImagePath) Then
                    ToolStripButton1.Enabled = False
                    ToolStripButton2.Enabled = False
                    BackgroundWorker1.WorkerSupportsCancellation = True
                    BackgroundWorker1.WorkerReportsProgress = True
                    BackgroundWorker1.RunWorkerAsync()
                End If
                PictureBoxAttachment.Image = Image.FromFile(OpenFileDialog1.FileName)
                PictureBoxAttachment.SizeMode = PictureBoxSizeMode.Zoom
                ToolStripButton1.Enabled = False
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Expenses/ToolStripButton2: " & ex.ToString, "Critical")
        End Try
    End Sub
End Class