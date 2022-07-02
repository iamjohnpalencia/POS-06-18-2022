Imports MySql.Data.MySqlClient
Public Class ResetPassword
    Public CrewUsername As String = ""
    Public CrewUserId As String = ""
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxCONFIRMPASSWORD.KeyPress, TextBoxPASSWORD.KeyPress
        Try
            If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub ResetPassword_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ForgotPassword.Enabled = True
    End Sub
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Application.Exit()
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Login.Show()
        Close()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ForgotPassword.Show()
        Close()
    End Sub
    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub
    Private Sub ResetPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = "Hi " & CrewUsername & ","
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If Trim(TextBoxPASSWORD.Text) = Trim(TextBoxCONFIRMPASSWORD.Text) Then
                ResetPass()
                MessageBox.Show("Password reset complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Login.Show()
                Close()
            Else
                MessageBox.Show("Password did not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "ResetPassword/Button1: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub ResetPass()
        Try
            Dim sql = "UPDATE loc_users SET password = '" & ConvertPassword(TextBoxPASSWORD.Text) & "' WHERE user_id = " & CrewUserId
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim result = cmd.ExecuteNonQuery
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "ResetPassword/ResetPass(): " & ex.ToString, "Critical")
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            TextBoxPASSWORD.UseSystemPasswordChar = True
        Else
            TextBoxPASSWORD.UseSystemPasswordChar = False
        End If
    End Sub
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = False Then
            TextBoxCONFIRMPASSWORD.UseSystemPasswordChar = True
        Else
            TextBoxCONFIRMPASSWORD.UseSystemPasswordChar = False
        End If
    End Sub
End Class