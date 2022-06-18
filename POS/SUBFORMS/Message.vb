Imports MySql.Data.MySqlClient
Public Class Message
    Private Sub Message_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


            If Messageboolean = False Then
                POS.Enabled = False
                GLOBAL_SELECT_ALL_FUNCTION("loc_message ORDER BY message_id DESC", "*", DataGridView1)
                Dim arg = New DataGridViewCellEventArgs(0, 0)
                DataGridView1_CellClick(sender, arg)
                ReadMessage()
                TopMost = True
            Else
                Me.MinimizeBox = True
                Me.MaximizeBox = True
                Me.WindowState = FormWindowState.Maximized

                GLOBAL_SELECT_ALL_FUNCTION("loc_message ORDER BY message_id DESC", "*", DataGridView1)
                Dim arg = New DataGridViewCellEventArgs(0, 0)
                DataGridView1_CellClick(sender, arg)
                ReadMessage()

                If DataGridView1.Rows.Count = 0 Then
                    Label1.TextAlign = ContentAlignment.MiddleCenter
                    Label1.Text = "No messages available"
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Message: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Close()
    End Sub
    Private Sub ReadMessage()
        Try
            With DataGridView1
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If .Rows(i).Cells(10).Value = 0 Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.LightSkyBlue
                    Else
                        .Rows(i).DefaultCellStyle.BackColor = Color.White
                    End If
                Next
                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(3).Visible = False
                .Columns(5).Visible = False
                .Columns(6).Visible = False
                .Columns(7).Visible = False
                .Columns(8).Visible = False
                .Columns(9).Visible = False
                .Columns(10).Visible = False
            End With
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Message_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If My.Settings.Auto_Update Then
            PROMPT_MESSAGE_DATATABLE.Clear()
        End If
        If Messageboolean = False Then
            If Application.OpenForms().OfType(Of BegBalance).Any Then
                POS.Enabled = False
            Else
                POS.Enabled = True
                For Each btn As Button In POS.Panel3.Controls.OfType(Of Button)()
                    If btn.Text = "Simply Perfect" Then
                        btn.PerformClick()
                    End If
                Next
            End If
            Messageboolean = False
        Else
            Messageboolean = False
        End If
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If DataGridView1.Rows.Count > 0 Then
                Label1.Text = "Message From: " & Me.DataGridView1.Item(2, Me.DataGridView1.CurrentRow.Index).Value.ToString & "(ADMIN)" & vbCrLf & "Subject: " & Me.DataGridView1.Item(3, Me.DataGridView1.CurrentRow.Index).Value.ToString & vbCrLf & "Date: " & Me.DataGridView1.Item(8, Me.DataGridView1.CurrentRow.Index).Value.ToString & vbCrLf & "Content: " & vbCrLf & vbCrLf & Me.DataGridView1.Item(4, Me.DataGridView1.CurrentRow.Index).Value.ToString
                SeenMessage(DataGridView1.SelectedRows(0).Cells(0).Value)
                DataGridView1.SelectedRows(0).Cells(10).Value = 1
                ReadMessage()
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub SeenMessage(messageid)
        Try
            Dim sql = "UPDATE loc_message SET seen = 1 WHERE message_id = " & messageid
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Message: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Class