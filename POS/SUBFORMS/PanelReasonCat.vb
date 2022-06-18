Imports MySql.Data.MySqlClient
Public Class PanelReasonCat
    Public TransferID
    Private Sub PanelReasonCat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TopMost = True

    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            If TextBoxReasonsCat.Text <> "" Then
                If StockAdjustment.AddOrUpdate = False Then
                    Dim sql = "INSERT INTO `loc_transfer_data`(`transfer_cat`, `crew_id`, `created_at`, `created_by`, `updated_at`, `active`) VALUES (@1,@2,@3,@4,@5,@6)"
                    cmd = New MySqlCommand(sql, LocalhostConn)
                    cmd.Parameters.Add("@1", MySqlDbType.Text).Value = TextBoxReasonsCat.Text
                    cmd.Parameters.Add("@2", MySqlDbType.Text).Value = ClientCrewID
                    cmd.Parameters.Add("@3", MySqlDbType.Text).Value = FullDate24HR()
                    cmd.Parameters.Add("@4", MySqlDbType.Text).Value = ClientCrewID
                    cmd.Parameters.Add("@5", MySqlDbType.Text).Value = FullDate24HR()
                    cmd.Parameters.Add("@6", MySqlDbType.Int64).Value = "1"
                    cmd.ExecuteNonQuery()
                    StockAdjustment.LoadReasonCategories()

                    TextBoxReasonsCat.Clear()
                    GLOBAL_SYSTEM_LOGS("NEW CATEGORIES REASON", "Category Name: " & TextBoxReasonsCat.Text & " ,Added By: " & ClientCrewID)
                    StockAdjustment.FillComboboxReason()
                Else
                    Dim sql = "UPDATE `loc_transfer_data` SET `transfer_cat`=@1, `crew_id`=@2, `created_at`=@3, `created_by`=@4, `updated_at`=@5, `active`=@6 WHERE transfer_id = " & TransferID
                    cmd = New MySqlCommand(sql, LocalhostConn)
                    cmd.Parameters.Add("@1", MySqlDbType.Text).Value = TextBoxReasonsCat.Text
                    cmd.Parameters.Add("@2", MySqlDbType.Text).Value = ClientCrewID
                    cmd.Parameters.Add("@3", MySqlDbType.Text).Value = FullDate24HR()
                    cmd.Parameters.Add("@4", MySqlDbType.Text).Value = ClientCrewID
                    cmd.Parameters.Add("@5", MySqlDbType.Text).Value = FullDate24HR()
                    cmd.Parameters.Add("@6", MySqlDbType.Int64).Value = "1"
                    cmd.ExecuteNonQuery()
                    StockAdjustment.LoadReasonCategories()

                    TextBoxReasonsCat.Clear()
                    GLOBAL_SYSTEM_LOGS("UPDATE CATEGORIES REASON", "Category Name: " & TextBoxReasonsCat.Text & " ,Updated By: " & ClientCrewID)
                    StockAdjustment.FillComboboxReason()
                End If
            Else
                MsgBox("Fill up required fields.")
            End If
            Close()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Reason Category: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub PanelReasonCat_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        StockAdjustment.Enabled = True
    End Sub

    Private Sub ButtonKeyboard_Click(sender As Object, e As EventArgs) Handles ButtonKeyboard.Click
        ShowKeyboard()
    End Sub
End Class