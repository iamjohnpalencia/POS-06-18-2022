Imports MySql.Data.MySqlClient

Public Class DepositSlip
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If String.IsNullOrWhiteSpace(TextBoxNAME.Text) Then
                MsgBox("Name is required")
            ElseIf String.IsNullOrWhiteSpace(TextBoxTRANNUM.Text) Then
                MsgBox("Transaction Number is required")
            ElseIf String.IsNullOrWhiteSpace(TextBoxAMT.Text) Then
                MsgBox("Amount is required")
            ElseIf String.IsNullOrWhiteSpace(ComboBoxBankName.Text) Then
                MsgBox("Bank name is required")
            Else
                table = "loc_deposit"
                fields = "(`name`, `transaction_number`, `amount`, `bank`, `transaction_date`, `store_id`, `guid`, `synced`, `crew_id`, `created_at`)"
                value = "('" & TextBoxNAME.Text & "'  
                                ,'" & TextBoxTRANNUM.Text & "'   
                                ," & TextBoxAMT.Text & "                
                                ,'" & ComboBoxBankName.Text & "'
                                ,'" & Format(DateTimePickerDATE.Value, "yyyy-MM-dd HH:mm:ss") & "'
                                ,'" & ClientStoreID & "'
                                ,'" & ClientGuid & "'                    
                                ,'Unsynced'
                                ,'" & ClientCrewID & "'
                                ,'" & FullDate24HR() & "')"
                GLOBAL_INSERT_FUNCTION(table:=table, fields:=fields, values:=value)
                GLOBAL_SYSTEM_LOGS("DEPOSIT", "Name: " & TextBoxNAME.Text & " Trn.Number: " & TextBoxTRANNUM.Text & " Amount: " & TextBoxAMT.Text)
                AuditTrail.LogToAuditTral("User", "Deposit: Name; " & TextBoxNAME.Text & " Trn.Number; " & TextBoxTRANNUM.Text & " Amount; " & TextBoxAMT.Text & ", Created By: " & ClientCrewID, "Normal")

                MsgBox("Complete!")
                ClearTextBox(Me)
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Deposit Slip: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub TextBoxAMT_KeyPress(sender As Object, e As KeyPressEventArgs)
        Numeric(sender:=sender, e:=e)
    End Sub
    Private Sub DepositSlip_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel2.Top = (Me.Height - Panel2.Height) / 2
        Panel2.Left = (Me.Width - Panel2.Width) / 2
        Try
            sql = "SELECT bankname FROM loc_partners_transaction WHERE active = 1 ORDER BY arrid ASC"
            cmd = New MySqlCommand(sql, LocalhostConn())
            da = New MySqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            For Each row As DataRow In dt.Rows
                ComboBoxBankName.Items.Add(row("bankname"))
            Next
            ComboBoxBankName.SelectedIndex = 0
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Deposit Slip: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub TextBoxNAME_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxAMT.KeyPress
        Try
            Numeric(sender, e)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub TextBoxNAME_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles TextBoxTRANNUM.KeyPress, TextBoxNAME.KeyPress
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