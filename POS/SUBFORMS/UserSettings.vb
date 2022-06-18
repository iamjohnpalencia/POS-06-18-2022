Imports MySql.Data.MySqlClient
Public Class UserSettings
    Dim userid As String
    Dim fullname As String
    Private Sub UserSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TabControl1.TabPages(0).Text = "User Accounts"
        Usersloadusers()
    End Sub
    Public Sub Usersloadusers()
        Try
            Dim ClientDT
            Dim Fields As String = "`user_id`, `user_level`, `full_name`, `username`, `password`, `contact_number`, `email`, `position`, `gender`, `created_at`, `updated_at`, `active`, `guid`, `store_id`, `uniq_id`, `synced`"
            If ClientRole = "Crew" Then
                'GLOBAL_SELECT_ALL_FUNCTION(, "*", DataGridViewUserSettings)
                ClientDT = AsDatatable("loc_users WHERE user_level <> 'Admin' AND user_id = " & ClientCrewID & " AND store_id= " & ClientStoreID & " AND guid='" & ClientGuid & "' AND active = 1 ", Fields, DataGridViewUserSettings)
            Else
                'GLOBAL_SELECT_ALL_FUNCTION("loc_users WHERE user_level <> 'Admin' AND store_id= " & ClientStoreID & " AND guid='" & ClientGuid & "' AND active = 1 ", "*", DataGridViewUserSettings)
                ClientDT = AsDatatable("loc_users WHERE user_level <> 'Admin' AND store_id= " & ClientStoreID & " AND guid='" & ClientGuid & "' AND active = 1 ", Fields, DataGridViewUserSettings)

            End If

            With DataGridViewUserSettings
                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(4).Visible = False
                .Columns(7).Visible = False
                .Columns(9).Visible = False
                .Columns(10).Visible = False
                .Columns(11).Visible = False
                .Columns(12).Visible = False
                .Columns(13).Visible = False
                .Columns(15).Visible = False
                .Columns(2).HeaderText = "Full Name"
                .Columns(3).HeaderText = "Username"
                .Columns(5).HeaderText = "Contact Number"
                .Columns(6).HeaderText = "Email Address"
                .Columns(8).HeaderText = "Gender"
                .Columns(14).HeaderText = "Crew ID"
            End With

            For Each row As DataRow In ClientDT.rows
                DataGridViewUserSettings.Rows.Add(row("user_id"), row("user_level"), row("full_name"), row("username"), row("password"), row("contact_number"), row("email"), row("position"), row("gender"), row("created_at"), row("updated_at"), row("active"), row("guid"), row("store_id"), row("uniq_id"), row("synced"))
            Next


        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "User Settings: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub deactivateuser()
        userid = DataGridViewUserSettings.SelectedRows(0).Cells(0).Value.ToString()
        fullname = DataGridViewUserSettings.SelectedRows(0).Cells(2).Value.ToString()
        Dim deactivation = MessageBox.Show("Are you sure you want to deactivate ( " & fullname & " ) account?", "Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If deactivation = DialogResult.Yes Then
            Try
                Dim sql = "UPDATE loc_users SET active = 0 WHERE user_id =" & userid
                Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
                Dim result = cmd.ExecuteNonQuery()
                If result = 1 Then
                    AuditTrail.LogToAuditTral("System", "Users: Deleted successfully, " & fullname, "Normal")

                    MsgBox("Account Deactivated")
                    Usersloadusers()
                    SystemLogType = "USER DEACTIVATION"
                    SystemLogDesc = "Deactivated by :" & returnfullname(ClientCrewID) & " : " & ClientRole
                    GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                    AuditTrail.LogToAuditTral("User", "Delete User: User ID " & userid & " Deleted By: " & ClientCrewID, "Normal")

                End If
            Catch ex As Exception
                AuditTrail.LogToAuditTral("System", "User Settings: " & ex.ToString, "Critical")

                SendErrorReport(ex.ToString)
            End Try
        End If
    End Sub
    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub

    Private Sub UserSettings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Application.OpenForms().OfType(Of AddUser).Any Then
            AddUser.Close()
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            Enabled = False
            AddUser.AddUserText = "ADD USER"
            AddUser.Show()
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            If DataGridViewUserSettings.Rows.Count > 0 Then
                Enabled = False
                AddUser.AddUserText = "EDIT USER"
                AddUser.userid = DataGridViewUserSettings.SelectedRows(0).Cells(14).Value.ToString
                AddUser.Show()
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        If ClientRole = "Head Crew" Or ClientRole = "Admin" Then
            If DataGridViewUserSettings.Rows.Count > 0 Then
                deactivateuser()
            End If
        Else
            MsgBox("You do not have permission to perform this task" & vbNewLine & "Please contact your administrator for help.")
        End If
    End Sub
End Class