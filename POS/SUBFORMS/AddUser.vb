Imports MySql.Data.MySqlClient
Public Class AddUser
    Dim gender As String
    Public userid As String
    Dim fullname As String
    Dim result As Integer
    Dim r As Random = New Random(Guid.NewGuid().GetHashCode())
    Public AddUserText As String


    Private Sub AddUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = AddUserText
        TopMost = True

        If AddUserText = "EDIT USER" Then
            Loaduser()
        End If
    End Sub

    Private Sub Loaduser()
        Try
            Dim ConnectionLocal = LocalhostConn()
            Dim sql = "SELECT * FROM loc_users WHERE uniq_id = '" & userid & "' "
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each row As DataRow In dt.Rows
                TextBoxFULLNAME.Text = row("full_name")
                TextBoxUSERNAME.Text = row("username")
                TextBoxEMAIL.Text = row("email")
                TextBoxCONTACT.Text = row("contact_number")
                If row("gender") = "Male" Then
                    RadioButtonMALE.Checked = True
                Else
                    RadioButtonFEMALE.Checked = True
                End If
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add User: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub ButtonUser_Click(sender As Object, e As EventArgs) Handles ButtonUser.Click
        Try
            If AddUserText = "ADD USER" Then
                adduser()
            ElseIf AddUserText = "EDIT USER" Then
                updateuser()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add User: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub adduser()
        Try
            If String.IsNullOrEmpty(TextBoxFULLNAME.Text.Trim) Then
                TextBoxFULLNAME.Clear()
                MessageBox.Show("Full name is required!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf TextBoxEMAIL.Text.Trim.Length = 0 Then
                MessageBox.Show("Email is required!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf TextBoxUSERNAME.Text.Trim.Length = 0 Then
                MessageBox.Show("Username is required!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf TextBoxPASS.Text.Trim.Length = 0 Then
                MessageBox.Show("Password is required!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf TextBoxCONTACT.Text.Trim.Length = 0 Then
                MessageBox.Show("Contact number is required!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If TextBoxPASS.Text.Trim <> TextBoxCONPASS.Text.Trim Then
                    MessageBox.Show("Password did not match!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Dim cipherText As String = ConvertPassword(SourceString:=TextBoxPASS.Text)
                    If CheckUserName(TextBoxUSERNAME.Text) = False Then
                        If CheckEmail(TextBoxEMAIL.Text) = False Then
                            If CheckContactNumber(TextBoxCONTACT.Text) = False Then
                                Try
                                    If RadioButtonMALE.Checked = True Then
                                        gender = "Male"
                                    ElseIf RadioButtonFEMALE.Checked = True Then
                                        gender = "Female"
                                    End If
                                    userid = CheckUserId()
                                    table = "loc_users"
                                    fields = " (`uniq_id`,`user_level`,`full_name`,`username`,`password`,`contact_number`,`email`,`position`,`store_id`,`gender`,`active`,`guid`,`synced`)"
                                    value = "('" & userid & "'
                            ,'Crew'
                            , '" & TextBoxFULLNAME.Text & "'
                            , '" & TextBoxUSERNAME.Text & "'
                            , '" & cipherText & "'
                            , '" & TextBoxCONTACT.Text & "'
                            , '" & TextBoxEMAIL.Text & "'
                            , 'Crew'
                            , " & ClientStoreID & "  
                            , '" & gender & "'
                            , " & 1 & "
                            , '" & ClientGuid & "'
                            , 'Unsynced')"
                                    GLOBAL_INSERT_FUNCTION(table:=table, fields:=fields, values:=value)
                                Catch ex As Exception
                                    MsgBox(ex.ToString)
                                    SendErrorReport(ex.ToString)
                                End Try
                                AuditTrail.LogToAuditTral("User", "New User: User name " & TextBoxUSERNAME.Text & " Created By: " & ClientCrewID, "Normal")

                                ClearTextBox(Me)
                                selectmax(whatform:=3)
                                SystemLogType = "NEW USER"
                                SystemLogDesc = "Added by :" & returnfullname(ClientCrewID) & " : " & ClientRole
                                GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                                MsgBox("Registered Successfully")
                                MDIFORM.newMDIchildUser.Usersloadusers()
                                Close()
                            Else
                                MsgBox("Contact number already exist. Please use different ontact number")
                            End If
                        Else
                            MsgBox("Email already exist. Please use different email")
                        End If
                    Else
                        MsgBox("Username already exist. Please use different username")
                    End If
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add User: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub updateuser()
        Try
            'uniqid = DataGridViewUserSettings.SelectedRows(0).Cells(14).Value.ToString()
            If String.IsNullOrEmpty(TextBoxFULLNAME.Text.Trim) Then
                TextBoxFULLNAME.Clear()
                MessageBox.Show("Full name is required!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf TextBoxEMAIL.Text.Trim.Length = 0 Then
                MessageBox.Show("Email is required!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf TextBoxUSERNAME.Text.Trim.Length = 0 Then
                MessageBox.Show("Username is required!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf TextBoxCONTACT.Text.Trim.Length = 0 Then
                MessageBox.Show("Contact number is required!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Dim cipherText As String = ConvertPassword(SourceString:=TextBoxPASS.Text)

                Try

                    If RadioButtonMALE.Checked = True Then
                        gender = "Male"
                    ElseIf RadioButtonFEMALE.Checked = True Then
                        gender = "Female"
                    End If
                    table = " loc_users "
                    If String.IsNullOrWhiteSpace(TextBoxPASS.Text) Then
                        fields = "`full_name`='" & TextBoxFULLNAME.Text & "',`username`='" & TextBoxUSERNAME.Text & "',`contact_number`= '" & TextBoxCONTACT.Text & "',`email`='" & TextBoxEMAIL.Text & "',`gender`='" & gender & "',`active`=" & 1 & ", synced = 'Unsynced' "
                    Else
                        fields = "`full_name`='" & TextBoxFULLNAME.Text & "',`username`='" & TextBoxUSERNAME.Text & "',`password`='" & cipherText & "',`contact_number`= '" & TextBoxCONTACT.Text & "',`email`='" & TextBoxEMAIL.Text & "',`gender`='" & gender & "',`active`=" & 1 & ", synced = 'Unsynced' "
                    End If
                    Dim where = " uniq_id = '" & userid & "'"
                    GLOBAL_FUNCTION_UPDATE(table, fields, where)
                    AuditTrail.LogToAuditTral("User", "User Update: User name " & TextBoxUSERNAME.Text & " Updated By: " & ClientCrewID, "Normal")

                Catch ex As Exception
                    MsgBox(ex.ToString)
                    SendErrorReport(ex.ToString)
                End Try
                ClearTextBox(Me)
                selectmax(whatform:=3)
                SystemLogType = "USER UPDATE"
                SystemLogDesc = "Updated by :" & returnfullname(ClientCrewID) & " : " & ClientRole
                GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
                MDIFORM.newMDIchildUser.Usersloadusers()
                MsgBox("Updated Successfully")
                Close()
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add User: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try

    End Sub
    Private Sub edituser()

        'userid = DataGridViewUserSettings.SelectedRows(0).Cells(14).Value.ToString()
        TextBoxCONPASS.Enabled = False
        Try
            sql = "SELECT * FROM loc_users WHERE uniq_id = '" & userid & "' "
            cmd = New MySqlCommand(sql, LocalhostConn)
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            For Each row As DataRow In dt.Rows
                TextBoxFULLNAME.Text = row("full_name")
                TextBoxUSERNAME.Text = row("username")
                TextBoxEMAIL.Text = row("email")
                TextBoxCONTACT.Text = row("contact_number")
                gender = row("gender")
            Next
            If gender = "Male" Then
                RadioButtonMALE.Checked = True
            Else
                RadioButtonFEMALE.Checked = True
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add User: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub AddUser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MDIFORM.newMDIchildUser.Enabled = True
    End Sub

    Private Sub TextBoxFULLNAME_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxUSERNAME.KeyPress, TextBoxPASS.KeyPress, TextBoxFULLNAME.KeyPress, TextBoxEMAIL.KeyPress, TextBoxCONPASS.KeyPress
        Try
            If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub TextBoxCONTACT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxCONTACT.KeyPress
        Try
            Numeric(sender, e)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Class