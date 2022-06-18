Imports MySql.Data.MySqlClient
Imports System.Threading
Public Class Auth
    Dim threadList As List(Of Thread) = New List(Of Thread)
    Dim thread As Thread
    Dim UserCount As Integer = 0
    Dim Account
    Dim TimerCount As Integer = 0
    Private Sub Auth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CheckForIllegalCrossThreadCalls = False
            BackgroundWorker1.WorkerReportsProgress = True
            BackgroundWorker1.WorkerSupportsCancellation = True
            BackgroundWorker1.RunWorkerAsync()
            ChangeProgBarColor(ProgressBar1, ProgressBarColor.Yellow)
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            ProgressBar1.Maximum = 100
            ProgressBar1.Value = 0
            For i = 0 To 100
                BackgroundWorker1.ReportProgress(i)
                Thread.Sleep(50)
                If i = 0 Then
                    thread = New Thread(AddressOf SyncToLocalUsers)
                    thread.Start()
                    threadList.Add(thread)
                End If
                If i = 50 Then
                    Label4.Text = "Checking user(s) availability...  "
                End If
            Next
            For Each t In threadList
                t.Join()
            Next
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Function IfUserExist(uniqID) As Boolean
        Dim ReturnBool As Boolean = False
        Try
            Dim ConnectionLocal As MySqlConnection = LocalhostConn()
            Dim sql = "SELECT uniq_id FROM loc_users WHERE uniq_id = '" & uniqID & "'"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionLocal)
            Using reader As MySqlDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    ReturnBool = True
                Else
                    ReturnBool = False
                End If
            End Using
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
        Return ReturnBool
    End Function
    Private Sub SyncToLocalUsers()
        UserCount = 0
        Try
            With DataGridViewRESULT
                Dim ConnectionServer As MySqlConnection = ServerCloudCon()
                Dim ConnectionLocal As MySqlConnection = LocalhostConn()
                Dim sql = "SELECT `user_level`, `full_name`, `username`, `password`, `contact_number`, `email`, `position`, `gender`, `active`, `guid`, `store_id`, `uniq_id` , `created_at`, `updated_at` , `pwd`, `user_code` FROM `loc_users` WHERE guid IN ('" & ClientGuid & "','Admin') AND store_id IN ('" & ClientStoreID & "','0') AND synced IN ('Unsynced','N/A') AND active = 1"
                Dim cmd As MySqlCommand = New MySqlCommand(sql, ConnectionServer)
                Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                Dim DataTableServer As DataTable = New DataTable
                da.Fill(DataTableServer)
                .DataSource = DataTableServer
                For i As Integer = 0 To .Rows.Count - 1 Step +1

                    If IfUserExist(.Rows(i).Cells(11).Value.ToString) = False Then

                        Account += "Username: " & .Rows(i).Cells(2).Value.ToString & vbNewLine & "Password: **********" & vbNewLine
                        UserCount = UserCount + 1

                        table = "triggers_loc_users"
                        fields = "(`user_level`, `full_name`, `username`, `password`, `contact_number`, `email`, `position`, `gender`, `active`, `guid`, `store_id`, `uniq_id`, `synced`, `user_code`)"
                        value = "(
                         '" & .Rows(i).Cells(0).Value.ToString & "'   
                         ,'" & .Rows(i).Cells(1).Value.ToString & "'    
                         ,'" & .Rows(i).Cells(2).Value.ToString & "'                 
                         ,'" & .Rows(i).Cells(3).Value.ToString & "'   
                         ,'" & .Rows(i).Cells(4).Value.ToString & "'   
                         ,'" & .Rows(i).Cells(5).Value.ToString & "'   
                         ,'" & .Rows(i).Cells(6).Value.ToString & "'                   
                         ,'" & .Rows(i).Cells(7).Value.ToString & "'   
                         ,'" & .Rows(i).Cells(8).Value.ToString & "'   
                         ,'" & .Rows(i).Cells(9).Value.ToString & "'    
                         ,'" & .Rows(i).Cells(10).Value.ToString & "'   
                         ,'" & .Rows(i).Cells(11).Value.ToString & "'       
                         ,'Synced','" & .Rows(i).Cells(15).Value.ToString & "'    )"

                        GLOBAL_INSERT_FUNCTION(table:=table, fields:=fields, values:=value)
                        If .Rows(i).Cells(6).Value.ToString <> "Admin" Then
                            sql = "UPDATE loc_users SET synced = 'Synced' WHERE uniq_id = '" & .Rows(i).Cells(11).Value.ToString & "'"
                            cmd = New MySqlCommand(sql, ConnectionServer)
                            cmd.ExecuteNonQuery()
                        End If
                        sql = "UPDATE loc_users SET `full_name` = '" & .Rows(i).Cells(1).Value.ToString & "'  , `username` = '" & .Rows(i).Cells(2).Value.ToString & "' , `password` = '" & .Rows(i).Cells(3).Value.ToString & "'  , `contact_number` = '" & .Rows(i).Cells(4).Value.ToString & "'  WHERE uniq_id = '" & .Rows(i).Cells(11).Value.ToString & "' "
                        cmd = New MySqlCommand(sql, ConnectionLocal)
                        cmd.ExecuteNonQuery()
                    End If
                Next
            End With
        Catch ex As Exception
            SendErrorReport(ex.ToString)
            AuditTrail.LogToAuditTral("System", "Auth: " & ex.ToString, "Critical")

        End Try
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        Label2.Text = e.ProgressPercentage
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ProgressBar1.Value = 100
        If UserCount = 0 Then
            Label4.Text = "No new user(s) available.  "
            Timer1.Start()
        Else
            AuditTrail.LogToAuditTral("System", "Auth: Update successful, " & Account, "Normal")
            Label4.Text = "New user(s) available.  "
            Dim msg = "New user account has been added" & vbNewLine & Account
            Dim msgs = MsgBox(msg, MsgBoxStyle.OkOnly)
            If msgs = DialogResult.OK Then
                Timer1.Start()
            End If
        End If
    End Sub
    Private Sub Auth_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Login.Enabled = True
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            TimerCount = TimerCount + 1
            If TimerCount = 1 Then
                Label1.Text = "Closing..."
            ElseIf TimerCount = 2 Then
                Close()
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Class