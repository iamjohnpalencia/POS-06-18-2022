Imports MySql.Data.MySqlClient
Public Class ChooseLayout
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim Layout As String = ""
            If RadioButton1.Checked Then
                Layout = "POS"
            ElseIf RadioButton2.Checked Then
                Layout = "GROCERY"
            End If
            Dim messagge = MessageBox.Show("Are you sure with your picked layout?", "NOTICE", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If messagge = DialogResult.Yes Then
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim sql = "UPDATE loc_settings SET S_Layout = '" & Layout & "' WHERE settings_id = 1"
                Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
                S_Layout = Layout
                cmd.ExecuteNonQuery()
                ConnectionLocal.Close()
                Loading.Show()
                Close()
            End If
        Catch ex As Exception

            AuditTrail.LogToAuditTrail("System", ex.ToString, "Critical")
        End Try
    End Sub

End Class