Imports MySql.Data.MySqlClient
Public Class ForgotPassword
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextboxIsEmpty(Panel1) = True Then
            If CheckCredentials() = True Then
                ResetPassword.Show()
                Close()
            Else
                MessageBox.Show("Account doesnt exist", "Invalid Account", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("All fields are required.", "Fill the blanks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Function CheckCredentials() As Boolean
        Dim dt As DataTable = New DataTable
        Try
            Dim sql = "SELECT username, user_id FROM loc_users WHERE email = '" & Trim(TextBoxEMAIL.Text) & "' AND contact_number = '" & Trim(TextBoxCONTACTNUMBER.Text) & "'"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            da.Fill(dt)
            For Each row As DataRow In dt.Rows
                ResetPassword.CrewUsername = row("username")
                ResetPassword.CrewUserId = row("user_id")
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Forget Password: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub ForgotPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LabelFOOTER.Text = My.Settings.Footer
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Login.Show()
        Close()
    End Sub
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Application.Exit()
    End Sub
    Private Sub TextBoxEMAIL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxEMAIL.KeyPress, TextBoxCONTACTNUMBER.KeyPress
        Try
            If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub
End Class