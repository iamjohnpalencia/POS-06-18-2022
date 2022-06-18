Imports System.Threading

Public Class Addexpense
    Dim insertcurrenttime As String
    Dim insertcurrentdate As String
    Dim thread1 As System.Threading.Thread

    Public ButtonClickCount As Integer = 0
    Private Sub Addexpense_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Label4.Text = returnfullname(where:=ClientCrewID)
        Label1.Text = SumOfColumnsToDecimal(DataGridViewExpenses, 4)
        selectmax(whatform:=2)
        With DataGridViewExpenses
            .RowHeadersVisible = False
            .Font = New Font("Century Gothic", 10)
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .Columns(0).HeaderCell.Value = "Type"
            .Columns(1).HeaderCell.Value = "Description"
            .Columns(2).HeaderCell.Value = "QTY"
            .Columns(3).HeaderCell.Value = "Price"
            .Columns(4).HeaderCell.Value = "Total Amount"
        End With
    End Sub
    Private Sub ButtonSaveCustomProducts_Click(sender As Object, e As EventArgs) Handles ButtonSaveCustomProducts.Click
        Expenses.Show()
        Enabled = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonSubmitReport.Click
        Try
            If DataGridViewExpenses.Rows.Count > 0 Then
                ButtonSubmitReport.Enabled = False
                ButtonSaveCustomProducts.Enabled = False
                ButtonRemove.Enabled = False
                BackgroundWorker1.WorkerSupportsCancellation = True
                BackgroundWorker1.WorkerReportsProgress = True
                BackgroundWorker1.RunWorkerAsync()
            Else
                MsgBox("Create report first")
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Expense: " & ex.ToString, "Critical")

            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub inserttolocal()
        Try
            With DataGridViewExpenses
                For i As Integer = 0 To DataGridViewExpenses.Rows.Count - 1 Step +1
                    'messageboxappearance = False
                    table = "loc_expense_details"
                    fields = "(`expense_number`, `expense_type`, `item_info`, `quantity`, `price`, `amount`, `attachment`, `created_at`, `crew_id`, `guid`, `store_id`, `active`, `zreading`, `synced`)"
                    value = "('" & TextBoxMAXID.Text & "'
                            ,'" & .Rows(i).Cells(0).Value & "'
                            ,'" & .Rows(i).Cells(1).Value & "'
                            ," & .Rows(i).Cells(2).Value & "
                            , " & .Rows(i).Cells(3).Value & "
                            , " & .Rows(i).Cells(4).Value & "
                            , '" & .Rows(i).Cells(5).Value & "'
                            , '" & FullDate24HR() & "'
                            , '" & ClientCrewID & "'
                            , '" & ClientGuid & "'
                            , '" & ClientStoreID & "'
                            , " & 1 & "
                            , '" & S_Zreading & "'
                            , 'Unsynced')"
                    GLOBAL_INSERT_FUNCTION(table:=table, fields:=fields, values:=value)
                Next
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Expense: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        Try
            With DataGridViewExpenses
                Dim total
                total = SumOfColumnsToDecimal(DataGridViewExpenses, 4)
                'messageboxappearance = False
                table = "loc_expense_list"
                fields = "(`expense_number`,`crew_id`, `total_amount`, `paid_amount`, `unpaid_amount`, `store_id`, `guid`, `created_at`, `active`, `synced`, `zreading`)"
                value = "('" & TextBoxMAXID.Text & "'
                         ,'" & ClientCrewID & "'
                         ," & total & "
                         ," & 0 & "
                         ," & 0 & "
                         ," & ClientStoreID & "
                         ,'" & ClientGuid & "'
                         ,'" & FullDate24HR() & "'
                         ," & 1 & "
                         ,'Unsynced'
                         ,'" & S_Zreading & "')"
                GLOBAL_INSERT_FUNCTION(table:=table, fields:=fields, values:=value)
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Expense: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
        DataGridViewExpenses.Rows.Clear()
        selectmax(whatform:=2)
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        insertcurrenttime = TimeOfDay.ToString("h:mm:ss")
        insertcurrentdate = String.Format("{0:yyyy/MM/dd}", DateTime.Now)
    End Sub
    Dim threadList As List(Of Thread) = New List(Of Thread)
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            For i = 0 To 100
                ToolStripStatusLabel1.Text = "Submitting expenses " & i & " %"
                BackgroundWorker1.ReportProgress(i)
                If i = 10 Then
                    thread1 = New System.Threading.Thread(AddressOf inserttolocal)
                    thread1.Start()
                    threadList.Add(thread1)
                End If
                Threading.Thread.Sleep(10)
            Next
            For Each t In threadList
                t.Join()
            Next
            SystemLogType = "NEW EXPENSE"
            SystemLogDesc = "Submitted by :" & returnfullname(ClientCrewID) & " : " & ClientRole
            GLOBAL_SYSTEM_LOGS(SystemLogType, SystemLogDesc)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Add Expense: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ToolStripProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub DataGridViewExpenses_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewExpenses.CellClick
        Try
            ExpenseImage.Show()
            Enabled = False
            Dim image = DataGridViewExpenses.SelectedRows(0).Cells(7).Value.ToString
            ExpenseImage.PictureBox1.BackgroundImage = Base64ToImage(image)
            ExpenseImage.PictureBox1.BackgroundImageLayout = ImageLayout.Zoom
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        Try
            If DataGridViewExpenses.Rows.Count > 0 Then
                Dim datas = DataGridViewExpenses.SelectedRows(0).Cells(6).Value.ToString()
                For x As Integer = DataGridViewExpenses.Rows.Count - 1 To 0 Step -1
                    If DataGridViewExpenses.Rows(x).Cells("Column8").Value = datas Then
                        DataGridViewExpenses.Rows.Remove(DataGridViewExpenses.Rows(x))
                    End If
                Next
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ToolStripStatusLabel1.Text = "Submitted Successfully!"
        ButtonSubmitReport.Enabled = True
        ButtonSaveCustomProducts.Enabled = True
        ButtonRemove.Enabled = True
        Label1.Text = SumOfColumnsToDecimal(DataGridViewExpenses, 4)
    End Sub
    Private Sub Addexpense_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        POS.Enabled = True
    End Sub
End Class