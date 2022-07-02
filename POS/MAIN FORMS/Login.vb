Imports MySql.Data.MySqlClient
Public Class Login
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LabelFOOTER.Text = My.Settings.Footer
            CheckDatabaseBackup()
            txtusername.Focus()
            Timer1.Enabled = True
            ButttonLogin.Text = "LOGIN (" & ClientStorename & ")"
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Login/Load: " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub BackupDatabase()
        Try
            Dim DatabaseName = "\POS" & Format(Now(), "yyyy-MM-dd") & ".sql"
            Process.Start("cmd.exe", "/k cd C:\xampp\mysql\bin & mysqldump --databases -h " & connectionModule.LocServer & " -u " & connectionModule.LocUser & " -p " & connectionModule.LocPass & " " & connectionModule.LocDatabase & " > """ & S_ExportPath & DatabaseName & """")
            'CheckIfRunning()
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Login/BackupDatabase(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub CheckDatabaseBackup()
        Try
            If S_Backup_Interval = "1" Then
                'Daily
                If S_Backup_Date = Format(Now(), "yyyy-MM-dd") Then
                    S_Backup_Date = Format(Now().AddDays(1), "yyyy-MM-dd")
                    BackupDatabase()
                    Dim sql As String = "UPDATE loc_settings SET S_BackupDate = '" & S_Backup_Date & "' WHERE settings_id = 1"
                    Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                    cmd.ExecuteNonQuery()
                End If
            ElseIf S_Backup_Interval = "2" Then
                'Weekly
                If S_Backup_Date = Format(Now(), "yyyy-MM-dd") Then
                    S_Backup_Date = Format(Now().AddDays(7), "yyyy-MM-dd")
                    BackupDatabase()
                    Dim sql As String = "UPDATE loc_settings SET S_BackupDate = '" & S_Backup_Date & "' WHERE settings_id = 1"
                    Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                    cmd.ExecuteNonQuery()
                End If
            ElseIf S_Backup_Interval = "3" Then
                'Monthly
                If S_Backup_Date <> Format(Now(), "yyyy-MM-dd") Then
                    If FirstDayOfMonth(Now()) = Format(Now(), "yyyy-MM-dd") Then
                        S_Backup_Date = Format(Now(), "yyyy-MM-dd")
                        BackupDatabase()
                        Dim sql As String = "UPDATE loc_settings SET S_BackupDate = '" & S_Backup_Date & "' WHERE settings_id = 1"
                        Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                        cmd.ExecuteNonQuery()
                    Else
                        Dim sql As String = "SELECT S_BackupDate FROM loc_settings WHERE settings_id = 1"
                        Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                        Dim result1 = cmd.ExecuteScalar()
                        If FirstDayOfMonth(Now) <> result1 Then
                            S_Backup_Date = FirstDayOfMonth(Now)
                            BackupDatabase()
                            Dim sql1 As String = "UPDATE loc_settings SET S_BackupDate = '" & S_Backup_Date & "' WHERE settings_id = 1"
                            Dim cmd1 As MySqlCommand = New MySqlCommand(sql1, LocalhostConn)
                            cmd1.ExecuteNonQuery()
                        End If
                    End If
                End If

            ElseIf S_Backup_Interval = "4" Then
                'Yearly
                Dim iDate As String = S_Backup_Date
                Dim oDate As DateTime = Convert.ToDateTime(iDate)
                Dim iDate1 As String = Now()
                Dim oDate1 As DateTime = Convert.ToDateTime(iDate1)
                If oDate1.Year = oDate.Year Then
                    S_Backup_Date = Format(Now().AddYears(1), "yyyy-MM-dd")
                    BackupDatabase()
                    Dim sql1 As String = "UPDATE loc_settings SET S_BackupDate = '" & S_Backup_Date & "' WHERE settings_id = 1"
                    Dim cmd1 As MySqlCommand = New MySqlCommand(sql1, LocalhostConn)
                    cmd1.ExecuteNonQuery()
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Login/CheckDatabaseBackup(): " & ex.ToString, "Critical")
        End Try
    End Sub
    Private Sub ButttonLogin_Click(sender As Object, e As EventArgs) Handles ButttonLogin.Click
        retrieveLoginDetails()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If MessageBox.Show("Are you sure you really want to exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
            Application.Exit()
        End If
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        Registration.Show()
    End Sub
    Private Sub txtpassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            ButttonLogin.PerformClick()
        End If
    End Sub
    Private Sub txtusername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtusername.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtpassword.Focus()
        End If
    End Sub
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Application.Exit()
    End Sub
    Dim bat As String
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If CheckForInternetConnection() Then
            Enabled = False
            Auth.Show()
        Else
            MessageBox.Show("Cannot connect to cloud server please try again.", "No internet connection", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub txtusername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtusername.KeyPress, txtpassword.KeyPress
        Try
            If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
        Application.DoEvents()
        txtusername.Focus()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        ForgotPassword.Show()
        Close()
    End Sub
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Try
            If txtpassword.UseSystemPasswordChar Then
                txtpassword.UseSystemPasswordChar = False
            Else
                txtpassword.UseSystemPasswordChar = True
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTrail("System", "Login/PictureBox3: " & ex.ToString, "Critical")
        End Try
    End Sub
End Class