
Public Class Registration
    Dim gender As String
    Private ImagePath As String = ""

    Dim uniqid As String
    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OfdImage.FileOk
        ImagePath = OfdImage.FileName
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Login.Show()
        Me.Dispose()
    End Sub
    Private Sub ButtonSubmit_click(sender As Object, e As EventArgs) Handles ButtonSubmit.Click
        Try
            If String.IsNullOrWhiteSpace(TextBoxFN.Text.Trim) Then
                TextBoxFN.Clear()
                MessageBox.Show("Full name is required!", "Incomplete Fields", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf String.IsNullOrWhiteSpace(TextBoxEMAIL.Text.Trim) Then
                MessageBox.Show("Email is required!", "Incomplete Fields", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf String.IsNullOrWhiteSpace(TextBoxUN.Text.Trim) Then
                MessageBox.Show("Username is required!", "Incomplete Fields", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf String.IsNullOrWhiteSpace(TextBoxP.Text.Trim) Then
                MessageBox.Show("Password is required!", "Incomplete Fields", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf String.IsNullOrWhiteSpace(TextBoxCN.Text.Trim) Then
                MessageBox.Show("Contact number is required!", "Incomplete Fields", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If TextBoxP.Text.Trim <> TextBoxCP.Text.Trim Then
                    MessageBox.Show("Password did not match!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If CheckUserName(TextBoxUN.Text) = False Then
                        If CheckEmail(TextBoxEMAIL.Text) = False Then
                            If CheckContactNumber(TextBoxCN.Text) = False Then
                                Dim cipherText As String = ConvertPassword(SourceString:=TextBoxP.Text)
                                Try
                                    'messageboxappearance = True
                                    If RadioButtonMALE.Checked = True Then
                                        gender = "Male"
                                    ElseIf RadioButtonFEMALE.Checked = True Then
                                        gender = "Female"
                                    End If
                                    uniqid = CheckUserId()
                                    table = "loc_users"
                                    fields = " (`uniq_id`,`user_level`,`full_name`,`username`,`password`,`contact_number`,`email`,`position`,`store_id`,`gender`,`active`,`guid`,`synced`)"
                                    value = "('" & uniqid & "'
                                , 'Crew'
                                , '" & TextBoxFN.Text & "'
                                , '" & TextBoxUN.Text & "'
                                , '" & cipherText & "'
                                , '" & TextBoxCN.Text & "'
                                , '" & TextBoxEMAIL.Text & "'
                                , 'Crew'
                                , " & ClientStoreID & "  
                                , '" & gender & "'
                                , " & 1 & "
                                , '" & ClientGuid & "'
                                , 'Unsynced')"
                                    GLOBAL_INSERT_FUNCTION(table:=table, fields:=fields, values:=value)
                                Catch ex As Exception
                                End Try
                                SystemLogType = "USER REGISTRATION"
                                SystemLogDesc = "Registration of: " & TextBoxFN.Text
                                GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)

                                selectmax(whatform:=3)
                                MessageBox.Show("Success fully registered" & vbNewLine & "Username: " & TextBoxUN.Text & vbNewLine & "Password: " & TextBoxP.Text, "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                ClearTextBox(Me)
                            Else
                                MsgBox("Contact number exist")
                            End If
                        Else
                            MsgBox("Email exist")
                        End If
                    Else
                        MsgBox("Username exist")
                    End If
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Registration: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub


    Private Sub Registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LabelFOOTER.Text = My.Settings.Footer
        selectmax(whatform:=3)
    End Sub
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If MessageBox.Show("Are you sure you really want to exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
            Application.Exit()
        End If
    End Sub
    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub
    Private Sub TextBoxFN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxUN.KeyPress, TextBoxP.KeyPress, TextBoxFN.KeyPress, TextBoxEMAIL.KeyPress, TextBoxCP.KeyPress, TextBoxCN.KeyPress
        Try
            If InStr(DisallowedCharacters, e.KeyChar) > 0 Then
                e.Handled = True
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Class