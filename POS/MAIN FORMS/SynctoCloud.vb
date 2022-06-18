Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Imports System.Threading
Public Class SynctoCloud
    Dim totalrow As Integer
    Dim counter As Integer = 0
    Dim dt2 As New DataTable
    Dim nousers As Boolean = False
    Dim StopThread As Boolean = False
    Dim Unsuccessful As Boolean
    Dim CountStart As Boolean
    Dim INVENTORYISSYNCING As Boolean = False

    Private Shared Sub ChangeProgBarColor(ByVal ProgressBar_Name As System.Windows.Forms.ProgressBar, ByVal ProgressBar_Color As ProgressBarColor)
        SendMessage(ProgressBar_Name.Handle, &H410, ProgressBar_Color, 0)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CheckForIllegalCrossThreadCalls = False
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If Label8.Text = "Cancelling Sync." Then
            Label8.Text = "Cancelling Sync.."
        ElseIf Label8.Text = "Cancelling Sync.." Then
            Label8.Text = "Cancelling Sync..."
        ElseIf Label8.Text = "Cancelling Sync..." Then
            Label8.Text = "Cancelling Sync...."
        ElseIf Label8.Text = "Cancelling Sync...." Then
            Label8.Text = "Cancelling Sync."
        End If
    End Sub
    Private Sub SynctoCloud_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        'Try
        '    If BackgroundWorkerFILLDATAGRIDS.IsBusy Or BackgroundWorkerSYNCTOCLOUD.IsBusy Then
        '        Dim msg = MessageBox.Show("Are you sure do you want to cancel sync ?", "Cancel Sync", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        '        If msg = DialogResult.Yes Then
        '            If BackgroundWorkerFILLDATAGRIDS.IsBusy Or BackgroundWorkerSYNCTOCLOUD.IsBusy Then
        '                BackgroundWorkerFILLDATAGRIDS.CancelAsync()
        '                BackgroundWorkerSYNCTOCLOUD.CancelAsync()
        '            End If
        '            WorkerCanceled = True
        '        End If
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try

        Try

            If SyncIsOnProcess = True Then
                e.Cancel = True
            Else
                GLOBAL_SYSTEM_LOGS("CLOUD SYNC", "State: Canceled, Time End : " & FullDate24HR() & " Canceled by : " & returnfullname(ClientCrewID))
                e.Cancel = False
            End If
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LabelTime.Text = Val(LabelTime.Text) + 1
    End Sub
#Region "FillDatagrid"
    Private Sub filldatagridrefretdetails()
        Try
            Dim fields = "*"
            Dim table = "loc_refund_return_details WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewRetrefdetails)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewRetrefdetails.Rows.Add(row("refret_id"), row("transaction_number"), row("crew_id"), row("reason"), row("total"), row("guid"), row("store_id"), row("created_at"), row("zreading"), row("synced"))
            Next
            gettablesize(tablename:="loc_refund_return_details")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    '=====================================================SYSTEMLOGS
    Private Sub filldatagridsystemlog1()
        Try
            Dim table = "loc_system_logs WHERE synced = 'Unsynced' AND log_store = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Dim fields = "*"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewSYSLOG1)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewSYSLOG1.Rows.Add(row("crew_id"), row("log_type"), row("log_description"), row("log_date_time"), row("log_store"), row("guid"), row("loc_systemlog_id"), row("zreading"), row("synced"))
            Next
            gettablesize(tablename:="loc_system_logs")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub filldatagridsystemlog2()
        Try
            Dim fields = "*"
            Dim table = "loc_system_logs WHERE synced = 'Unsynced' AND log_type IN ('MENU FORM', 'STOCK ENTRY', 'STOCK REMOVAL', 'STOCK TRANSFER') AND log_store = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewSYSLOG2)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewSYSLOG2.Rows.Add(row("crew_id"), row("log_type"), row("log_description"), row("log_date_time"), row("log_store"), row("guid"), row("loc_systemlog_id"), row("zreading"), row("synced"))
            Next
            gettablesize(tablename:="loc_system_logs")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagridsystemlog3()
        Try
            Dim fields = "*"
            Dim table = "loc_system_logs WHERE synced = 'Unsynced' AND log_type IN ('NEW CUSTOM PRODUCT', 'NEW EXPENSE', 'NEW STOCK ADDED', 'NEW USER') AND log_store = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"

            Dim ThisDT = AsDatatable(table, fields, DataGridViewSYSLOG3)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewSYSLOG3.Rows.Add(row("crew_id"), row("log_type"), row("log_description"), row("log_date_time"), row("log_store"), row("guid"), row("loc_systemlog_id"), row("zreading"), row("synced"))
            Next
            gettablesize(tablename:="loc_system_logs")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagridsystemlog4()
        Try
            Dim fields = "*"
            Dim table = "loc_system_logs WHERE synced = 'Unsynced' AND log_type IN ('TRANSACTION', 'USER UPDATE') AND log_store = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewSYSLOG4)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewSYSLOG4.Rows.Add(row("crew_id"), row("log_type"), row("log_description"), row("log_date_time"), row("log_store"), row("guid"), row("loc_systemlog_id"), row("zreading"), row("synced"))
            Next
            gettablesize(tablename:="loc_system_logs")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    '=====================================================SYSTEMLOGS
    Private Sub filldatagridtransaction()
        Try
            Dim fields = "*"
            Dim table = "loc_daily_transaction WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            GLOBAL_SELECT_ALL_FUNCTION(table, fields, DataGridViewTRAN)
            gettablesize(tablename:="loc_daily_transaction")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    '======================================================TRANSACTION DETAILS
    Private Sub filldatagridtransactiondetails1()
        Try
            Dim fields = "*"
            Dim table = "loc_daily_transaction_details WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewTRANDET)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewTRANDET.Rows.Add(row("details_id"), row("product_id"), row("product_sku"), row("product_name"), row("quantity"), row("price"), row("total"), row("crew_id"), row("transaction_number"), row("active"), row("created_at"), row("guid"), row("store_id"), row("total_cost_of_goods"), row("product_category"), row("zreading"), row("transaction_type"), row("synced"))
            Next
            gettablesize(tablename:="loc_daily_transaction_details")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagridinventory()
        Try
            Dim fields = "*"
            Dim table = "loc_pos_inventory WHERE store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewINV)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewINV.Rows.Add(row("inventory_id"), row("store_id"), row("formula_id"), row("product_ingredients"), row("sku"), row("stock_primary"), row("stock_secondary"), row("stock_no_of_servings"), row("stock_status"), row("critical_limit"), row("guid"), row("date_modified"), row("crew_id"), row("server_inventory_id"))
            Next
            gettablesize(tablename:="loc_pos_inventory")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagridzreadinventory()
        Try
            Dim fields = "*"
            Dim table = "loc_zread_inventory WHERE synced = 'Unsynced'"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewZREADINVENTORY)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewZREADINVENTORY.Rows.Add(row("zreadinv_id"), row("inventory_id"), row("store_id"), row("formula_id"), row("product_ingredients"), row("sku"), row("stock_primary"), row("stock_secondary"), row("stock_no_of_servings"), row("stock_status"), row("critical_limit"), row("guid"), row("created_at"), row("crew_id"), row("server_date_modified"), row("server_inventory_id"), row("zreading"))
            Next
            gettablesize(tablename:="loc_zread_inventory")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagridexpenses()
        Try
            Dim fields = "*"
            Dim table = "loc_expense_list WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewEXP)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewEXP.Rows.Add(row("expense_id"), row("crew_id"), row("expense_number"), row("total_amount"), row("paid_amount"), row("unpaid_amount"), row("store_id"), row("guid"), row("created_at").ToString, row("active"), row("zreading"), row("synced"))
            Next
            gettablesize(tablename:="loc_expense_list")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagridexpensesdetails()
        Try
            Dim fields = "*"
            Dim table = "loc_expense_details WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewEXPDET)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewEXPDET.Rows.Add(row("expense_id"), row("expense_number"), row("expense_type"), row("item_info"), row("quantity"), row("price"), row("amount"), row("attachment"), row("created_at"), row("crew_id"), row("guid"), row("store_id"), row("active"), row("zreading"), row("synced"))
            Next
            gettablesize(tablename:="loc_expense_details")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagridlocusers()
        Try
            Dim fields = "*"
            Dim table = "loc_users WHERE store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "' AND synced = 'Unsynced'"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewLocusers)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewLocusers.Rows.Add(row("user_id"), row("user_level"), row("full_name"), row("username"), row("password"), row("contact_number"), row("email"), row("position"), row("gender"), row("created_at"), row("updated_at"), row("active"), row("guid"), row("store_id"), row("uniq_id"), row("synced"))
            Next
            gettablesize(tablename:="loc_users")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagridproducts()
        Try
            Dim fields = "*"
            Dim table = "loc_admin_products WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "' AND product_status = 0"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewCUSTOMPRODUCTS)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewCUSTOMPRODUCTS.Rows.Add(row("product_id"), row("product_sku"), row("product_name"), row("formula_id"), row("product_barcode"), row("product_category"), row("product_price"), row("product_desc"), row("product_image"), row("product_status"), row("origin"), row("date_modified"), row("guid"), row("store_id"), row("crew_id"), row("synced"), row("server_product_id"))
            Next
            gettablesize(tablename:="loc_daily_transaction")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagriddepositslip()
        Try
            Dim fields = "*"
            Dim table = "loc_deposit WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewDepositSlip)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewDepositSlip.Rows.Add(row("dep_id"), row("name"), row("crew_id"), row("transaction_number"), row("amount"), row("bank"), row("transaction_date"), row("store_id"), row("guid"), row("created_at"), row("synced"))
            Next
            gettablesize(tablename:="loc_deposit")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagridmodeoftransaction()
        Try
            Dim fields = "*"
            Dim table = "loc_transaction_mode_details WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            Dim ThisDT = AsDatatable(table, fields, DataGridViewMODEOFTRANSACTION)
            For Each row As DataRow In ThisDT.Rows
                DataGridViewMODEOFTRANSACTION.Rows.Add(row("mode_id"), row("transaction_type"), row("transaction_number"), row("fullname"), row("reference"), row("markup"), row("created_at"), row("status"), row("store_id"), row("guid"), row("synced"))
            Next
            gettablesize(tablename:="loc_transaction_mode_details")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub fillpricerequestchange()
        Try
            Dim fields = "*"
            Dim table = "loc_price_request_change WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            GLOBAL_SELECT_ALL_FUNCTION(table, fields, DataGridViewPriceChangeRequest)
            gettablesize(tablename:="loc_price_request_change")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagridviewcoupon()
        Try
            Dim fields = "*"
            Dim table = "tbcoupon WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "' AND origin = 'Local'"
            GLOBAL_SELECT_ALL_FUNCTION(table, fields, DataGridViewCoupons)
            gettablesize(tablename:="tbcoupon")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub filldatagridviewerrors()
        Try
            Dim fields = "*"
            Dim table = "loc_send_bug_report WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            GLOBAL_SELECT_ALL_FUNCTION(table, fields, DataGridViewERRORS)
            gettablesize(tablename:="loc_send_bug_report")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub filldatagridviewsenior()
        Try
            Dim fields = "*"
            Dim table = "loc_senior_details WHERE synced = 'Unsynced' AND store_id = " & ClientStoreID & " AND guid = '" & ClientGuid & "'"
            GLOBAL_SELECT_ALL_FUNCTION(table, fields, DatagridviewSenior)
            gettablesize(tablename:="loc_senior_details")
            countrows(tablename:=table)
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub countrows(ByVal tablename As String)
        Try
            Dim sql = "SELECT COUNT(*) FROM " & tablename & " "
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn())
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            If BackgroundWorkerFILLDATAGRIDS.IsBusy Then
                For Each row As DataRow In dt.Rows
                    DataGridView2.Rows.Add(row("COUNT(*)"), tablename)
                Next
            End If

        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        Finally
            LocalhostConn.close
            cmd.Dispose()
            da.Dispose()
        End Try
    End Sub
    Private Sub gettablesize(ByVal tablename As String)
        Try
            Dim sql = "SELECT table_name AS `Table`, round(((data_length + index_length) / 1024 / 1024), 2) `Size in MB` FROM information_schema.TABLES WHERE table_schema = 'pos' AND table_name = '" & tablename & "'"
            Dim cmd As MySqlCommand = New MySqlCommand(sql, LocalhostConn)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            For Each row As DataRow In dt.Rows
                DataGridView1.Rows.Add(row("Table"), row("Size in MB"))
            Next
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        Finally
            LocalhostConn.close
            cmd.Dispose()
            da.Dispose()
        End Try
    End Sub
    Private Sub LoadData1()
        Try
            POS.Instance.Invoke(Sub()
                                    POS.ProgressBar1.Maximum = totalrow
                                End Sub)
            POS.ProgressBar1.Value = 0
            ProgressBar1.Value = 0
            Label1.Text = ""
            Label2.Text = ""
            LabelTTLRowtoSync.Text = ""
            Label5.Text = ""
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub LoadData2()
        Try
            totalrow = SumOfColumnsToInt(DataGridView2, 0)
            LabelTTLRowtoSync.Text = totalrow
            POS.Instance.Invoke(Sub()
                                    POS.ProgressBar1.Maximum = totalrow
                                End Sub)
            ProgressBar1.Maximum = Val(LabelTTLRowtoSync.Text)
            Label2.Text = "Item(s)"
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim ThreadLoaddata As Thread
    Dim Threadlist As List(Of Thread) = New List(Of Thread)
#End Region
    Private Sub BackgroundWorker2_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorkerFILLDATAGRIDS.DoWork
        Try
            If WorkerCanceled Then
                Exit Sub
            End If
            If CheckForInternetConnection() = True Then
                ThreadLoaddata = New Thread(AddressOf LoadData1)
                ThreadLoaddata.Start()
                Threadlist.Add(ThreadLoaddata)
                For Each t In Threadlist
                    t.Join()
                    If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        e.Cancel = True
                        Exit For
                    End If
                Next
                If INVENTORYISSYNCING = False Then
                    ThreadLoaddata = New Thread(AddressOf filldatagridtransaction)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.
                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridtransactiondetails1)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.
                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridzreadinventory)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.
                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridexpenses)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.
                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridexpensesdetails)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.
                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridlocusers)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridsystemlog1)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridsystemlog2)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridsystemlog3)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridsystemlog4)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridrefretdetails)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridproducts)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridmodeoftransaction)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagriddepositslip)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf fillpricerequestchange)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                    ThreadLoaddata = New Thread(AddressOf filldatagridviewcoupon)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                Else
                    ThreadLoaddata = New Thread(AddressOf filldatagridinventory)
                    ThreadLoaddata.Start()
                    Threadlist.Add(ThreadLoaddata)
                    For Each t In Threadlist
                        t.Join()
                        If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                            ' Indicate that the task was canceled.

                            e.Cancel = True
                            Exit For
                        End If
                    Next
                End If
                ThreadLoaddata = New Thread(AddressOf filldatagridviewerrors)
                ThreadLoaddata.Start()
                Threadlist.Add(ThreadLoaddata)
                For Each t In Threadlist
                    t.Join()
                    If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                        ' Indicate that the task was canceled.

                        e.Cancel = True
                        Exit For
                    End If
                Next

                ThreadLoaddata = New Thread(AddressOf filldatagridviewsenior)
                ThreadLoaddata.Start()
                Threadlist.Add(ThreadLoaddata)
                For Each t In Threadlist
                    t.Join()
                    If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                        ' Indicate that the task was canceled.

                        e.Cancel = True
                        Exit For
                    End If
                Next
                ThreadLoaddata = New Thread(AddressOf LoadData2)
                ThreadLoaddata.Start()
                Threadlist.Add(ThreadLoaddata)
                For Each t In Threadlist
                    t.Join()
                    If (BackgroundWorkerFILLDATAGRIDS.CancellationPending) Then
                        ' Indicate that the task was canceled.

                        e.Cancel = True
                        Exit For
                    End If
                Next

            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorkerFILLDATAGRIDS.RunWorkerCompleted
        Try
            POS.ProgressBar1.Visible = True
            POS.Button2.Visible = True
            Timer1.Start()
            If INVENTORYISSYNCING = False Then
                ButtonSYNCDATA.Text = "CANCEL SYNC"
            Else
                ButtonSYNCINVENTORY.Text = "CANCEL SYNC"
            End If
            BackgroundWorkerSYNCTOCLOUD.WorkerSupportsCancellation = True
            BackgroundWorkerSYNCTOCLOUD.WorkerReportsProgress = True
            BackgroundWorkerSYNCTOCLOUD.RunWorkerAsync()
        Catch ex As Exception
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ButtonSYNCDATA.Click
        SyncData()
    End Sub

    Private Sub SyncData()
        Try
            If ButtonSYNCDATA.Text = "SYNC" Then
                If ValidCloudConnection = True Then
                    ButtonSYNCINVENTORY.Enabled = False
                    INVENTORYISSYNCING = False
                    SyncIsOnProcess = True
                    Button2.Text = "Cancel Sync"
                    GLOBAL_SYSTEM_LOGS("CLOUD SYNC", "Start : " & FullDate24HR() & " Synced by : " & returnfullname(ClientCrewID))
                    BackgroundWorkerFILLDATAGRIDS.WorkerSupportsCancellation = True
                    BackgroundWorkerFILLDATAGRIDS.WorkerReportsProgress = True
                    BackgroundWorkerFILLDATAGRIDS.RunWorkerAsync()
                    RadioButton1.Enabled = False
                    RadioButton2.Enabled = False
                Else
                    MsgBox("Cloud connection is not valid.")
                End If
            Else
                Dim msg = MessageBox.Show("Are you sure do you want to cancel sync ?", "Cancel Sync", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If msg = DialogResult.Yes Then
                    SyncIsOnProcess = False
                    BackgroundWorkerSYNCTOCLOUD.CancelAsync()
                    WorkerCanceled = True
                    Label1.Visible = False
                    Label2.Visible = False
                    LabelTTLRowtoSync.Visible = False
                    Label8.Visible = True
                    Timer2.Stop()
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub ButtonSYNCINVENTORY_Click(sender As Object, e As EventArgs) Handles ButtonSYNCINVENTORY.Click
        SyncInventory()
    End Sub

    Private Sub SyncInventory()
        Try
            ButtonSYNCDATA.Enabled = False
            If ButtonSYNCINVENTORY.Text = "SYNC INVENTORY" Then
                If ValidCloudConnection = True Then
                    ButtonSYNCDATA.Enabled = False
                    INVENTORYISSYNCING = True
                    SyncIsOnProcess = True
                    Button2.Text = "Cancel Sync"
                    GLOBAL_SYSTEM_LOGS("CLOUD SYNC", "Start : " & FullDate24HR() & " Synced by : " & returnfullname(ClientCrewID))
                    BackgroundWorkerFILLDATAGRIDS.WorkerSupportsCancellation = True
                    BackgroundWorkerFILLDATAGRIDS.WorkerReportsProgress = True
                    BackgroundWorkerFILLDATAGRIDS.RunWorkerAsync()
                    RadioButton1.Enabled = False
                    RadioButton2.Enabled = False
                Else
                    MsgBox("Cloud connection is not valid.")
                End If
            Else
                Dim msg = MessageBox.Show("Are you sure do you want to cancel sync ?", "Cancel Sync", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If msg = DialogResult.Yes Then
                    SyncIsOnProcess = False
                    BackgroundWorkerSYNCTOCLOUD.CancelAsync()
                    WorkerCanceled = True
                    Label1.Visible = False
                    Label2.Visible = False
                    LabelTTLRowtoSync.Visible = False
                    Label8.Visible = True
                    Timer2.Stop()
                End If
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Dim threadListLOCTRAN As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCTD1 As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCTD2 As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCINV As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCEXP As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCEXPD As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCTUSER As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCSYSLOG1 As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCSYSLOG2 As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCSYSLOG3 As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCSYSLOG4 As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCREFRET As List(Of Thread) = New List(Of Thread)
    Dim threadListLOCPRODUCT As List(Of Thread) = New List(Of Thread)
    Dim threadListMODEOFTRANSACTION As List(Of Thread) = New List(Of Thread)
    Dim threadListLocDeposit As List(Of Thread) = New List(Of Thread)
    Dim threadListloadData As List(Of Thread) = New List(Of Thread)
    Dim threadListPRICEREQUEST As List(Of Thread) = New List(Of Thread)
    Dim threadListCoupon As List(Of Thread) = New List(Of Thread)
    Dim threadListErrors As List(Of Thread) = New List(Of Thread)
    Dim threadListzreadInventory As List(Of Thread) = New List(Of Thread)
    Dim thread1 As Thread
    Dim WorkerCanceled As Boolean = False
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorkerSYNCTOCLOUD.DoWork
        Try
            If CheckForInternetConnection() = True Then
                'System Logs
                If INVENTORYISSYNCING = False Then
                    For i = 0 To 100
                        BackgroundWorkerSYNCTOCLOUD.ReportProgress(i)
                        Thread.Sleep(50)
                        If i = 0 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelSYS1.Text = "Syncing Systemlogs 1"
                                                          End Sub))
                            t1.Start()
                            thread1 = New Thread(AddressOf insertsystemlogs1)
                            thread1.Start()
                            threadListLOCSYSLOG1.Add(thread1)
                        End If
                        If i = 5 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelDTransac.Text = "Syncing Daily Transaction"
                                                          End Sub))
                            t1.Start()
                            thread1 = New Thread(AddressOf insertlocaldailytransaction)
                            thread1.Start()
                            threadListLOCTRAN.Add(thread1)
                        End If
                        If i = 10 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelDTransactD.Text = "Syncing Transaction Details"
                                                          End Sub))
                            t1.Start()
                            thread1 = New Thread(AddressOf inserttransactiondetails1)
                            thread1.Start()
                            threadListLOCTD1.Add(thread1)
                        End If
                        If i = 15 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelZREADINV.Text = "Syncing Zread Inventory"
                                                          End Sub))
                            t1.Start()
                            thread1 = New Thread(AddressOf insertzreadinventory)
                            thread1.Start()
                            threadListzreadInventory.Add(thread1)
                        End If
                        If i = 20 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelEXP.Text = "Syncing Expense List"
                                                          End Sub))
                            t1.Start()
                            thread1 = New Thread(AddressOf insertexpenses)
                            thread1.Start()
                            threadListLOCEXP.Add(thread1)
                        End If
                        If i = 25 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelEXPD.Text = "Syncing Expense Details"
                                                          End Sub))
                            t1.Start()
                            thread1 = New Thread(AddressOf insertexpensedetails)
                            thread1.Start()
                            threadListLOCEXPD.Add(thread1)
                        End If
                        If i = 30 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelACC.Text = "Syncing Accounts"
                                                          End Sub))
                            t1.Start()
                            thread1 = New Thread(AddressOf insertlocalusers)
                            thread1.Start()
                            threadListLOCTUSER.Add(thread1)
                        End If
                        If i = 35 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelRET.Text = "Syncing Refund Details"
                                                          End Sub))
                            t1.Start()
                            thread1 = New Thread(AddressOf insertrefretdetails)
                            thread1.Start()
                            threadListLOCREFRET.Add(thread1)
                        End If
                        If i = 40 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelCPROD.Text = "Syncing Local Products"
                                                          End Sub))
                            t1.Start()
                            'Custom Products
                            thread1 = New Thread(AddressOf insertlocproducts)
                            thread1.Start()
                            threadListLOCPRODUCT.Add(thread1)
                        End If
                        If i = 45 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelMODET.Text = "Syncing Mode of Transaction"
                                                          End Sub))
                            t1.Start()
                            'Mode of transaction details
                            thread1 = New Thread(AddressOf insertlocmodeoftransaction)
                            thread1.Start()
                            threadListMODEOFTRANSACTION.Add(thread1)
                        End If
                        If i = 50 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelDEPOSIT.Text = "Syncing Deposit Details"
                                                          End Sub))
                            t1.Start()
                            'Deposits
                            thread1 = New Thread(AddressOf insertlocdeposit)
                            thread1.Start()
                            threadListLocDeposit.Add(thread1)
                        End If
                        If i = 55 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelPRICEREQ.Text = "Syncing Price Request"
                                                          End Sub))
                            t1.Start()
                            'Price Request
                            thread1 = New Thread(AddressOf insertpricerequest)
                            thread1.Start()
                            threadListPRICEREQUEST.Add(thread1)
                        End If
                        If i = 60 Then
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelCoupon.Text = "Syncing Coupons"
                                                          End Sub))
                            t1.Start()
                            'Coupons
                            thread1 = New Thread(AddressOf insertcoupon)
                            thread1.Start()
                            threadListCoupon.Add(thread1)
                        End If
                        If i = 65 Then
                            'Errors
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelError.Text = "Syncing Errors"
                                                          End Sub))
                            t1.Start()
                            thread1 = New Thread(AddressOf inserterrors)
                            thread1.Start()
                            threadListErrors.Add(thread1)
                        End If
                        If i = 75 Then
                            'Errors
                            Dim t1 As New Task(New Action(Sub()
                                                              LabelSeniorDetails.Text = "Syncing Senior Details"
                                                          End Sub))
                            t1.Start()
                            thread1 = New Thread(AddressOf insertseniordetails)
                            thread1.Start()
                            threadListErrors.Add(thread1)
                        End If

                    Next
                Else
                    Dim t1 As New Task(New Action(Sub()
                                                      LabelINV.Text = "Syncing Inventory"
                                                  End Sub))
                    t1.Start()

                    thread1 = New Thread(AddressOf insertinventory)
                    thread1.Start()
                    threadListLOCINV.Add(thread1)
                End If

                For Each t In threadListLOCTRAN
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCTD1
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCTD2
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListzreadInventory
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCEXP
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCEXPD
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCTUSER
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCSYSLOG1
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCSYSLOG2
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCSYSLOG3
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCSYSLOG4
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCREFRET
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCPRODUCT
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next

                For Each t In threadListMODEOFTRANSACTION
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLocDeposit
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListPRICEREQUEST
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListCoupon
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListLOCINV
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
                For Each t In threadListErrors
                    t.Join()
                    If (BackgroundWorkerSYNCTOCLOUD.CancellationPending) Then
                        ' Indicate that the task was canceled.
                        WorkerCanceled = True
                        e.Cancel = True
                        Exit For
                    End If
                Next
            Else
                Unsuccessful = True
            End If
        Catch ex As Exception
            AuditTrail.LogToAuditTral("Data", "Menu/Sync: Sync not Successful, " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorkerSYNCTOCLOUD.RunWorkerCompleted
        Try
            If Unsuccessful = True Then
                ChangeProgBarColor(ProgressBar1, ProgressBarColor.Yellow)
                If LabelRowtoSync.Text <> "0" Then
                    Label1.Text = "Synced " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                Else
                    LabelTTLRowtoSync.Text = "0"
                    Label2.Text = "Item(s)"
                    Label1.Text = "Synced 0 of 0"
                End If
                Label5.Text = "Synchronization Failed"
                MsgBox("An error occured")
                ButtonSYNCDATA.Enabled = True
                GLOBAL_SYSTEM_LOGS("CLOUD SYNC", "State: Unsuccessful, Time End : " & FullDate24HR() & " Synced by : " & returnfullname(ClientCrewID))
                Close()
            Else
                If WorkerCanceled = True Then

                End If
                If e.Cancelled Then
                    Close()
                Else
                    ProgressBar1.Value = Val(LabelTTLRowtoSync.Text)
                    SyncIsOnProcess = False
                    Timer1.Enabled = False
                    Label1.Text = "Synced " & LabelTTLRowtoSync.Text & " of " & LabelTTLRowtoSync.Text
                    Label5.Text = "Successfully Synchronized "
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value = totalrow
                                        End Sub)
                    Dim sync = MessageBox.Show("Synchronization Complete", "Sync", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    AuditTrail.LogToAuditTral("Data", "Menu/Sync: Sync Successful, " & ProgressBar1.Value.ToString, "Normal")

                    If sync = DialogResult.OK Then
                        Me.Close()
                        ButtonSYNCDATA.Enabled = True
                    End If
                    GLOBAL_SYSTEM_LOGS("CLOUD SYNC", "State: Successful, Time End : " & FullDate24HR() & " Synced by : " & returnfullname(ClientCrewID))
                End If
            End If
            POS.ProgressBar1.Visible = False
            POS.Button2.Visible = False
            POS.Button2.Text = "Show"
            ButtonSYNCDATA.Enabled = True
            ButtonSYNCINVENTORY.Enabled = True
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertlocaldailytransaction()
        Try
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand
            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()
            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewTRAN
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO `Triggers_admin_daily_transaction`(`loc_transaction_id`, `transaction_number`, `grosssales`, `totaldiscount`, `amounttendered`, `change`, `amountdue`, `vatablesales`, `vatexemptsales`, `zeroratedsales`, `vatpercentage`, `lessvat`, `transaction_type`, `discount_type`, `totaldiscountedamount`, `si_number`, `crew_id`, `guid`, `active`, `store_id`, `created_at`, `shift`, `zreading`)
                                             VALUES (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19,@20,@21,@22,@23)", server)

                    cmd.Parameters.Add("@1", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.Decimal).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.Decimal).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.Decimal).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@6", MySqlDbType.Decimal).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@7", MySqlDbType.Decimal).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@8", MySqlDbType.Decimal).Value = .Rows(i).Cells(7).Value.ToString()
                    cmd.Parameters.Add("@9", MySqlDbType.Decimal).Value = .Rows(i).Cells(8).Value.ToString()
                    cmd.Parameters.Add("@10", MySqlDbType.Decimal).Value = .Rows(i).Cells(9).Value.ToString()
                    cmd.Parameters.Add("@11", MySqlDbType.Decimal).Value = .Rows(i).Cells(10).Value.ToString()
                    cmd.Parameters.Add("@12", MySqlDbType.Decimal).Value = .Rows(i).Cells(11).Value.ToString()
                    cmd.Parameters.Add("@13", MySqlDbType.VarChar).Value = .Rows(i).Cells(12).Value.ToString()
                    cmd.Parameters.Add("@14", MySqlDbType.Text).Value = .Rows(i).Cells(13).Value.ToString()
                    cmd.Parameters.Add("@15", MySqlDbType.Decimal).Value = .Rows(i).Cells(14).Value.ToString()
                    cmd.Parameters.Add("@16", MySqlDbType.Int64).Value = .Rows(i).Cells(15).Value.ToString()
                    cmd.Parameters.Add("@17", MySqlDbType.VarChar).Value = .Rows(i).Cells(16).Value.ToString()
                    cmd.Parameters.Add("@18", MySqlDbType.VarChar).Value = .Rows(i).Cells(17).Value.ToString()
                    cmd.Parameters.Add("@19", MySqlDbType.VarChar).Value = .Rows(i).Cells(18).Value.ToString()
                    cmd.Parameters.Add("@20", MySqlDbType.VarChar).Value = .Rows(i).Cells(19).Value.ToString()
                    cmd.Parameters.Add("@21", MySqlDbType.Text).Value = .Rows(i).Cells(20).Value.ToString()
                    cmd.Parameters.Add("@22", MySqlDbType.VarChar).Value = .Rows(i).Cells(21).Value.ToString()
                    cmd.Parameters.Add("@23", MySqlDbType.Text).Value = .Rows(i).Cells(22).Value.ToString()

                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelDTransacItem.Text = Val(LabelDTransacItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)
                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    cmd.ExecuteNonQuery()
                    '====================================================================
                    Dim table = " loc_daily_transaction "
                    Dim where = " transaction_number = '" & .Rows(i).Cells(1).Value.ToString & "'"
                    Dim fields = "`synced`='Synced' "
                    Dim sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelDTransac.Text = "Synced Daily Transaction"
                                                     LabelDTransacTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub inserttransactiondetails1()
        Try
            Dim ProgBarVal As Integer = 0
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewTRANDET
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_daily_transaction_details(`loc_details_id`, `product_id`, `product_sku`, `product_name`, `quantity`, `price`, `total`, `crew_id`
                                            , `transaction_number`, `active`, `created_at`, `guid`, `store_id`, `total_cost_of_goods`, `product_category` , `zreading`, `transaction_type`) 
                    VALUES (@a0, @a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9, @a10, @a11, @a12, @a13, @a14, @a15, @a16)", server)
                    cmd.Parameters.Add("@a0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@a1", MySqlDbType.Int64).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@a2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@a3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@a4", MySqlDbType.Int64).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@a5", MySqlDbType.Decimal).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@a6", MySqlDbType.Decimal).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@a7", MySqlDbType.VarChar).Value = .Rows(i).Cells(7).Value.ToString()
                    cmd.Parameters.Add("@a8", MySqlDbType.VarChar).Value = .Rows(i).Cells(8).Value.ToString()
                    cmd.Parameters.Add("@a9", MySqlDbType.Int64).Value = .Rows(i).Cells(9).Value.ToString()
                    cmd.Parameters.Add("@a10", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value.ToString()
                    cmd.Parameters.Add("@a11", MySqlDbType.VarChar).Value = .Rows(i).Cells(11).Value.ToString()
                    cmd.Parameters.Add("@a12", MySqlDbType.VarChar).Value = .Rows(i).Cells(12).Value.ToString()
                    cmd.Parameters.Add("@a13", MySqlDbType.Decimal).Value = .Rows(i).Cells(13).Value.ToString()
                    cmd.Parameters.Add("@a14", MySqlDbType.VarChar).Value = .Rows(i).Cells(14).Value.ToString()
                    cmd.Parameters.Add("@a15", MySqlDbType.VarChar).Value = .Rows(i).Cells(15).Value.ToString()
                    cmd.Parameters.Add("@a16", MySqlDbType.VarChar).Value = .Rows(i).Cells(16).Value.ToString()
                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelDTransactDItem.Text = Val(LabelDTransactDItem.Text) + 1
                    ProgBarVal += 1
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    cmd.ExecuteNonQuery()
                    '====================================================================
                    Dim table = " loc_daily_transaction_details "
                    Dim where = " details_id =" & .Rows(i).Cells(0).Value.ToString
                    Dim fields = "`synced`='Synced' "
                    Dim sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelDTransactD.Text = "Synced Transaction Details"
                                                     LabelDTransactDTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertexpenses()
        Try
            Dim ProgBarVal As Integer = 0
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewEXP
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_expense_list(`loc_expense_id`, `crew_id`, `expense_number`, `total_amount`, `paid_amount`, `unpaid_amount`, `store_id`, `guid`, `created_at`, `active`, `zreading`) 
                                             VALUES (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11)", server)

                    cmd.Parameters.Add("@1", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()

                    cmd.Parameters.Add("@4", MySqlDbType.Decimal).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.Decimal).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@6", MySqlDbType.Decimal).Value = .Rows(i).Cells(5).Value.ToString()

                    cmd.Parameters.Add("@7", MySqlDbType.VarChar).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@8", MySqlDbType.VarChar).Value = .Rows(i).Cells(7).Value.ToString()

                    cmd.Parameters.Add("@9", MySqlDbType.Text).Value = .Rows(i).Cells(8).Value.ToString()
                    cmd.Parameters.Add("@10", MySqlDbType.Int64).Value = .Rows(i).Cells(9).Value.ToString()
                    cmd.Parameters.Add("@11", MySqlDbType.Text).Value = .Rows(i).Cells(10).Value.ToString()
                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelEXPItem.Text = Val(LabelEXPItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    '====================================================================
                    cmd.ExecuteNonQuery()
                    Dim table = " loc_expense_list "
                    Dim where = " expense_id = '" & .Rows(i).Cells(0).Value.ToString & "'"
                    Dim fields = "`synced`='Synced' "
                    Dim sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelEXP.Text = "Synced Expense List"
                                                     LabelEXPTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
            'truncatetable(tablename:="loc_expense_list")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertexpensedetails()
        Try
            Dim ProgBarVal As Integer = 0
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewEXPDET
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_expense_details(`loc_details_id`
                                                                            , `expense_number`
                                                                            , `expense_type`
                                                                            , `item_info`
                                                                            , `quantity`
                                                                            , `price`
                                                                            , `amount`
                                                                            , `attachment`
                                                                            , `created_at`
                                                                            , `crew_id`
                                                                            , `guid`
                                                                            , `store_id`
                                                                            , `active`, `zreading`) 
                                             VALUES (@loc_details_id, @expense_number, @expense_type
                                             , @item_info, @quantity, @price, @amount, @attachment, @created_at
                                              , @crew_id, @guid, @store_id, @active, @zreading)", server)
                    cmd.Parameters.Add("@loc_details_id", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@expense_number", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@expense_type", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@item_info", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@quantity", MySqlDbType.Int64).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@price", MySqlDbType.Decimal).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@amount", MySqlDbType.Decimal).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@attachment", MySqlDbType.Text).Value = .Rows(i).Cells(7).Value.ToString()
                    cmd.Parameters.Add("@created_at", MySqlDbType.VarChar).Value = .Rows(i).Cells(8).Value.ToString()
                    cmd.Parameters.Add("@crew_id", MySqlDbType.VarChar).Value = .Rows(i).Cells(9).Value.ToString()
                    cmd.Parameters.Add("@guid", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value.ToString()
                    cmd.Parameters.Add("@store_id", MySqlDbType.Int64).Value = .Rows(i).Cells(11).Value.ToString()
                    cmd.Parameters.Add("@active", MySqlDbType.Int64).Value = .Rows(i).Cells(12).Value.ToString()
                    cmd.Parameters.Add("@zreading", MySqlDbType.LongText).Value = .Rows(i).Cells(13).Value.ToString()
                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelEXPDItem.Text = Val(LabelEXPDItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    cmd.ExecuteNonQuery()
                    '====================================================================
                    Dim table = " loc_expense_details "
                    Dim fields = "`synced`='Synced' "
                    Dim where = " expense_id = '" & .Rows(i).Cells(0).Value.ToString & "'"
                    Dim sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelEXPD.Text = "Synced Expense Details"
                                                     LabelEXPDTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
            'truncatetable(tablename:="loc_expense_details")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertlocalusers()
        Try
            Dim cmdupdateinventory As MySqlCommand
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewLocusers
                'messageboxappearance = False
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmdupdateinventory = New MySqlCommand("UPDATE loc_users SET full_name = '" & .Rows(i).Cells(2).Value & "' , username = '" & .Rows(i).Cells(3).Value & "' , email = '" & .Rows(i).Cells(6).Value & "' , password = '" & .Rows(i).Cells(4).Value & "' , contact_number = '" & .Rows(i).Cells(5).Value & "' WHERE uniq_id = '" & .Rows(i).Cells(14).Value & "' AND guid = '" & ClientGuid & "' AND store_id = '" & ClientStoreID & "'", server)
                    cmd = New MySqlCommand("INSERT INTO Triggers_loc_users (`loc_user_id`, `user_level`, `full_name`, `username`, `password`, `contact_number`, `email`, `position`, `gender`, `created_at`, `updated_at`, `active`, `guid`, `store_id`, `uniq_id`)
                                             VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14)", server)
                    cmd.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@6", MySqlDbType.VarChar).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@7", MySqlDbType.VarChar).Value = .Rows(i).Cells(7).Value.ToString()
                    cmd.Parameters.Add("@8", MySqlDbType.VarChar).Value = .Rows(i).Cells(8).Value.ToString()
                    cmd.Parameters.Add("@9", MySqlDbType.VarChar).Value = .Rows(i).Cells(9).Value.ToString()
                    cmd.Parameters.Add("@10", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value.ToString()
                    cmd.Parameters.Add("@11", MySqlDbType.VarChar).Value = .Rows(i).Cells(11).Value.ToString()
                    cmd.Parameters.Add("@12", MySqlDbType.VarChar).Value = .Rows(i).Cells(12).Value.ToString()
                    cmd.Parameters.Add("@13", MySqlDbType.VarChar).Value = .Rows(i).Cells(13).Value.ToString()
                    cmd.Parameters.Add("@14", MySqlDbType.VarChar).Value = .Rows(i).Cells(14).Value.ToString()
                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelACCItem.Text = Val(LabelACCItem.Text) + 1
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    cmd.ExecuteNonQuery()
                    cmdupdateinventory.ExecuteNonQuery()
                    '====================================================================
                    Dim table = " loc_users "
                    Dim where = " user_id = " & .Rows(i).Cells(0).Value.ToString
                    Dim fields = "`synced`='Synced' "
                    Dim sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelACC.Text = "Synced Accounts"
                                                     LabelACCTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertsystemlogs1()
        Try

            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewSYSLOG1
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_system_logs(`crew_id`, `log_type`, `log_description`, `log_date_time`, `log_store`, `guid`, `loc_systemlog_id`, `zreading`) 
                    VALUES (@0, @1, @2, @3, @4, @5, @6, @7)", server)
                    cmd.Parameters.Add("@0", MySqlDbType.VarChar).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.Text).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@6", MySqlDbType.VarChar).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@7", MySqlDbType.Text).Value = .Rows(i).Cells(7).Value.ToString()
                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelSYS1Item.Text = Val(LabelSYS1Item.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    cmd.ExecuteNonQuery()
                    '====================================================================

                    If BackgroundWorkerSYNCTOCLOUD.IsBusy Then
                        Dim table = " loc_system_logs "
                        Dim where = " loc_systemlog_id = '" & .Rows(i).Cells(6).Value.ToString & "'"
                        Dim fields = "`synced`='Synced' "
                        Dim sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                        cmdloc = New MySqlCommand(sql, local)
                        cmdloc.ExecuteNonQuery()
                    End If

                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelSYS1.Text = "Synced Systemlogs 1"
                                                     LabelSYS1Time.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertrefretdetails()
        Try
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewRetrefdetails
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_refund_return_details( `loc_refret_id`, `transaction_number`, `crew_id`, `reason`, `total`, `guid`, `store_id`, `datereturned`, `zreading`)
                                             VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8)", server)
                    cmd.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.Text).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.Decimal).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@6", MySqlDbType.Int64).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@7", MySqlDbType.VarChar).Value = .Rows(i).Cells(7).Value.ToString()
                    cmd.Parameters.Add("@8", MySqlDbType.LongText).Value = .Rows(i).Cells(8).Value.ToString()
                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelRETItem.Text = Val(LabelRETItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    '====================================================================
                    cmd.ExecuteNonQuery()

                    Dim table = " loc_refund_return_details "
                    Dim where = " refret_id = '" & .Rows(i).Cells(0).Value.ToString & "'"
                    Dim fields = "`synced`='Synced' "
                    sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelRET.Text = "Synced Refund Details"
                                                     LabelRETTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
            'truncatetable(tablename:="loc_expense_list")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertlocproducts()
        Try
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewCUSTOMPRODUCTS
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_products( `loc_product_id`, `product_sku`, `product_name`, `formula_id`, `product_barcode`, `product_category`, `product_price`, `product_desc`, `product_image`, `product_status`, `origin`, `date_modified`, `guid`, `store_id`, `crew_id`, `synced`)
                                             VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15)", server)
                    cmd.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@6", MySqlDbType.Int64).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@7", MySqlDbType.VarChar).Value = .Rows(i).Cells(7).Value.ToString()
                    cmd.Parameters.Add("@8", MySqlDbType.LongText).Value = .Rows(i).Cells(8).Value.ToString()
                    cmd.Parameters.Add("@9", MySqlDbType.VarChar).Value = .Rows(i).Cells(9).Value.ToString()
                    cmd.Parameters.Add("@10", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value.ToString()
                    cmd.Parameters.Add("@11", MySqlDbType.VarChar).Value = .Rows(i).Cells(11).Value.ToString()
                    cmd.Parameters.Add("@12", MySqlDbType.VarChar).Value = .Rows(i).Cells(12).Value.ToString()
                    cmd.Parameters.Add("@13", MySqlDbType.Int64).Value = .Rows(i).Cells(13).Value.ToString()
                    cmd.Parameters.Add("@14", MySqlDbType.VarChar).Value = .Rows(i).Cells(14).Value.ToString()
                    cmd.Parameters.Add("@15", MySqlDbType.Text).Value = "Synced"

                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelCPRODItem.Text = Val(LabelCPRODItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    '====================================================================
                    cmd.ExecuteNonQuery()
                    Dim table = " loc_admin_products "
                    Dim where = " product_id = '" & .Rows(i).Cells(0).Value.ToString & "'"
                    Dim fields = " `synced`='Synced' "
                    sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelCPROD.Text = "Synced Local Products"
                                                     LabelCPRODTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
            'truncatetable(tablename:="loc_expense_list")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertlocmodeoftransaction()
        Try
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewMODEOFTRANSACTION
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_transaction_mode_details(`loc_mode_id`, `transaction_type`, `transaction_number`, `full_name`, `reference`, `markup`, `created_at`, `status`, `store_id`, `guid`)
                                             VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9)", server)
                    cmd.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = .Rows(i).Cells(5).Value.ToString()

                    cmd.Parameters.Add("@6", MySqlDbType.VarChar).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@7", MySqlDbType.Int64).Value = .Rows(i).Cells(7).Value.ToString()
                    cmd.Parameters.Add("@8", MySqlDbType.VarChar).Value = .Rows(i).Cells(8).Value.ToString()
                    cmd.Parameters.Add("@9", MySqlDbType.VarChar).Value = .Rows(i).Cells(9).Value.ToString()
                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelMODETItem.Text = Val(LabelMODETItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    '====================================================================
                    cmd.ExecuteNonQuery()
                    Dim table = " loc_transaction_mode_details "
                    Dim where = " mode_id = '" & .Rows(i).Cells(0).Value.ToString & "'"
                    Dim fields = " `synced`='Synced' "
                    sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelMODET.Text = "Synced Mode of Transaction"
                                                     LabelMODETTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
            'truncatetable(tablename:="loc_expense_list")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertlocdeposit()
        Try
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewDepositSlip
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_deposit_slip_details( `loc_dep_id`, `name`, `crew_id`, `transaction_number`, `amount`, `bank`, `transaction_date`, `store_id`, `guid`, `created_at`)
                                             VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9)", server)
                    cmd.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.Decimal).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.VarChar).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@6", MySqlDbType.VarChar).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@7", MySqlDbType.VarChar).Value = .Rows(i).Cells(7).Value.ToString()
                    cmd.Parameters.Add("@8", MySqlDbType.VarChar).Value = .Rows(i).Cells(8).Value.ToString()

                    cmd.Parameters.Add("@9", MySqlDbType.Timestamp).Value = .Rows(i).Cells(9).Value.ToString()
                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelDEPOSITItem.Text = Val(LabelDEPOSITItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    '====================================================================
                    cmd.ExecuteNonQuery()
                    Dim table = " loc_deposit "
                    Dim where = " dep_id = '" & .Rows(i).Cells(0).Value.ToString & "'"
                    Dim fields = " `synced`='Synced' "
                    sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelDEPOSIT.Text = "Synced  Deposit Details"
                                                     LabelDEPOSITTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
            'truncatetable(tablename:="loc_expense_list")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertpricerequest()
        Try
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewPriceChangeRequest
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_price_request(`loc_request_id`, `store_name`, `server_product_id`, `request_price`, `created_at`, `active`, `store_id`, `crew_id`, `guid`)
                                             VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8)", server)

                    cmd.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@1", MySqlDbType.Text).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.Text).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.Text).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.Text).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.Text).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@6", MySqlDbType.Text).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@7", MySqlDbType.Text).Value = .Rows(i).Cells(7).Value.ToString()
                    cmd.Parameters.Add("@8", MySqlDbType.Text).Value = .Rows(i).Cells(8).Value.ToString()
                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelPRICEREQItem.Text = Val(LabelPRICEREQItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    '====================================================================
                    cmd.ExecuteNonQuery()
                    Dim table = " loc_price_request_change "
                    Dim where = " request_id = " & .Rows(i).Cells(0).Value.ToString & ""
                    Dim fields = " `synced`='Synced' "
                    sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelPRICEREQ.Text = "Synced Price Request Change"
                                                     LabelPRICEREQTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertcoupon()
        Try
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewCoupons
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_custom_coupon(`ID`, `Couponname_`, `Desc_`, `Discountvalue_`, `Referencevalue_`, `Type`, `Bundlebase_`, `BBValue_`, `Bundlepromo_`, `BPValue_`, `Effectivedate`, `Expirydate`, `active`, `store_id`, `crew_id`, `guid`, `synced`)
                                             VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9 ,@10 ,@11, @12, @13, @14, @15, @16)", server)
                    cmd.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@1", MySqlDbType.Text).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.Text).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.Text).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.Text).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.Text).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@6", MySqlDbType.Text).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@7", MySqlDbType.Text).Value = .Rows(i).Cells(7).Value.ToString()
                    cmd.Parameters.Add("@8", MySqlDbType.Text).Value = .Rows(i).Cells(8).Value.ToString()
                    cmd.Parameters.Add("@9", MySqlDbType.Text).Value = .Rows(i).Cells(9).Value.ToString()
                    cmd.Parameters.Add("@10", MySqlDbType.Text).Value = .Rows(i).Cells(10).Value.ToString()
                    cmd.Parameters.Add("@11", MySqlDbType.Text).Value = .Rows(i).Cells(11).Value.ToString()
                    cmd.Parameters.Add("@12", MySqlDbType.Text).Value = .Rows(i).Cells(12).Value.ToString()
                    cmd.Parameters.Add("@13", MySqlDbType.Text).Value = .Rows(i).Cells(13).Value.ToString()
                    cmd.Parameters.Add("@14", MySqlDbType.Text).Value = .Rows(i).Cells(14).Value.ToString()
                    cmd.Parameters.Add("@15", MySqlDbType.Text).Value = .Rows(i).Cells(15).Value.ToString()
                    cmd.Parameters.Add("@16", MySqlDbType.Text).Value = "Synced"

                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelCouponItem.Text = Val(LabelCouponItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    '====================================================================
                    cmd.ExecuteNonQuery()
                    Dim table = " tbcoupon "
                    Dim where = " ID = " & .Rows(i).Cells(0).Value.ToString & ""
                    Dim fields = " `synced`='Synced' "
                    sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelCoupon.Text = "Synced Coupons"
                                                     LabelCouponTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
            'truncatetable(tablename:="loc_expense_list")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub inserterrors()
        Try
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewERRORS
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_error_reports(`loc_bug_id`,`bug_desc`,`crew_id`,`guid`,`store_id`,`date_created`) VALUES (@0, @1, @2, @3, @4, @5)", server)

                    cmd.Parameters.Add("@0", MySqlDbType.Text).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@1", MySqlDbType.Text).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.Text).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.Text).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.Text).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.Text).Value = .Rows(i).Cells(5).Value.ToString()


                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelErrorItem.Text = Val(LabelErrorItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    '====================================================================
                    cmd.ExecuteNonQuery()
                    Dim table = " loc_send_bug_report "
                    Dim where = " bug_id = " & .Rows(i).Cells(0).Value.ToString & ""
                    Dim fields = " `synced`='Synced' "
                    sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelError.Text = "Synced Error logs"
                                                     LabelErrorTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
            'truncatetable(tablename:="loc_expense_list")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertseniordetails()
        Try
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DatagridviewSenior
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO triggers_admin_senior_details(`loc_id`,`transaction_number`,`senior_id`,`senior_name`,`active`,`crew_id`,`store_id`,`guid`,`date_created`) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8)", server)

                    cmd.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString()
                    cmd.Parameters.Add("@1", MySqlDbType.Text).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.Text).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@3", MySqlDbType.Text).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.Text).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.Text).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@6", MySqlDbType.Text).Value = .Rows(i).Cells(6).Value.ToString()
                    cmd.Parameters.Add("@7", MySqlDbType.Text).Value = .Rows(i).Cells(7).Value.ToString()
                    cmd.Parameters.Add("@8", MySqlDbType.Text).Value = .Rows(i).Cells(8).Value.ToString()

                    '====================================================================
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelSeniorDetailsItem.Text = Val(LabelSeniorDetailsItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    '====================================================================
                    cmd.ExecuteNonQuery()
                    Dim table = " loc_senior_details "
                    Dim where = " id = " & .Rows(i).Cells(0).Value.ToString & ""
                    Dim fields = " `synced`='Synced' "
                    sql = "UPDATE " & table & " SET " & fields & " WHERE " & where
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                    '====================================================================
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelSeniorDetails.Text = "Synced Error seniordetails"
                                                     LabelSeniorDetailsTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
            'truncatetable(tablename:="loc_expense_list")
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertinventory()
        Try
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewINV
                Dim DateSynced As String = ""
                DateSynced = FullDate24HR()
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If

                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_pos_inventory( `loc_inventory_id`, `store_id`, `formula_id`, `product_ingredients`, `sku`, `stock_primary`, `stock_secondary`, `stock_no_of_servings`, `stock_status`, `critical_limit`, `guid`, `date`)
                                             VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)", server)
                    cmd.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value
                    cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.Int64).Value = .Rows(i).Cells(2).Value
                    cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(3).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.Double).Value = .Rows(i).Cells(5).Value
                    cmd.Parameters.Add("@6", MySqlDbType.Double).Value = .Rows(i).Cells(6).Value
                    cmd.Parameters.Add("@7", MySqlDbType.Double).Value = .Rows(i).Cells(7).Value
                    cmd.Parameters.Add("@8", MySqlDbType.Int64).Value = .Rows(i).Cells(8).Value
                    cmd.Parameters.Add("@9", MySqlDbType.Int64).Value = .Rows(i).Cells(9).Value
                    cmd.Parameters.Add("@10", MySqlDbType.VarChar).Value = .Rows(i).Cells(10).Value.ToString()
                    cmd.Parameters.Add("@11", MySqlDbType.Text).Value = DateSynced
                    cmd.ExecuteNonQuery()
                    'inventory_id,store_id,formula_id,product_ingredients,sku,stock_primary,stock_secondary,stock_no_of_servings,stock_status,critical_limit,guid,date_modified,crew_id,server_inventory_id
                    cmd = New MySqlCommand("UPDATE admin_pos_inventory SET `stock_primary` = @1, `stock_secondary`= @2, `stock_no_of_servings` = @3, `stock_status` = @4, `critical_limit`= @5, `date` = @6 WHERE loc_inventory_id = " & .Rows(i).Cells(0).Value & " AND guid = '" & ClientGuid & "' AND store_id = '" & ClientStoreID & "'", server)
                    cmd.Parameters.Add("@1", MySqlDbType.Double).Value = .Rows(i).Cells(5).Value
                    cmd.Parameters.Add("@2", MySqlDbType.Double).Value = .Rows(i).Cells(6).Value
                    cmd.Parameters.Add("@3", MySqlDbType.Double).Value = .Rows(i).Cells(7).Value
                    cmd.Parameters.Add("@4", MySqlDbType.Int64).Value = .Rows(i).Cells(8).Value
                    cmd.Parameters.Add("@5", MySqlDbType.Int64).Value = .Rows(i).Cells(9).Value
                    cmd.Parameters.Add("@6", MySqlDbType.Text).Value = DateSynced
                    cmd.ExecuteNonQuery()

                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelINVItem.Text = Val(LabelINVItem.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)

                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "

                    Dim sql = "UPDATE loc_pos_inventory SET `synced`='Synced' WHERE inventory_id = " & .Rows(i).Cells(0).Value.ToString
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelINV.Text = "Synced Inventories"
                                                     LabelINVTime.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub
    Private Sub insertzreadinventory()
        Try
            Dim cmd As MySqlCommand
            Dim cmdloc As MySqlCommand

            Dim server As MySqlConnection = New MySqlConnection
            server.ConnectionString = CloudConnectionString
            server.Open()

            Dim local As MySqlConnection = New MySqlConnection
            local.ConnectionString = LocalConnectionString
            local.Open()

            With DataGridViewZREADINVENTORY
                For i As Integer = 0 To .Rows.Count - 1 Step +1
                    If WorkerCanceled = True Then
                        Exit For
                    End If
                    cmd = New MySqlCommand("INSERT INTO Triggers_admin_pos_zread_inventory(`loc_zreadinv_id`,`inventory_id`,`store_id`,`formula_id`,`product_ingredients`,`sku`,`stock_primary`,`stock_secondary`,`stock_no_of_servings`,`stock_status`,`critical_limit`,`guid`,`created_at`,`crew_id`,`server_date_modified`,`server_inventory_id`,`zreading`)
                                             VALUES (@0, @ID, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11,@12,@13,@14,@15)", server)
                    cmd.Parameters.Add("@0", MySqlDbType.Int64).Value = .Rows(i).Cells(0).Value.ToString
                    cmd.Parameters.Add("@ID", MySqlDbType.Int64).Value = .Rows(i).Cells(1).Value.ToString()
                    cmd.Parameters.Add("@1", MySqlDbType.VarChar).Value = .Rows(i).Cells(2).Value.ToString()
                    cmd.Parameters.Add("@2", MySqlDbType.Int64).Value = .Rows(i).Cells(3).Value.ToString
                    cmd.Parameters.Add("@3", MySqlDbType.VarChar).Value = .Rows(i).Cells(4).Value.ToString()
                    cmd.Parameters.Add("@4", MySqlDbType.VarChar).Value = .Rows(i).Cells(5).Value.ToString()
                    cmd.Parameters.Add("@5", MySqlDbType.Double).Value = .Rows(i).Cells(6).Value.ToString
                    cmd.Parameters.Add("@6", MySqlDbType.Double).Value = .Rows(i).Cells(7).Value.ToString
                    cmd.Parameters.Add("@7", MySqlDbType.Double).Value = .Rows(i).Cells(8).Value.ToString
                    cmd.Parameters.Add("@8", MySqlDbType.Int64).Value = .Rows(i).Cells(9).Value.ToString
                    cmd.Parameters.Add("@9", MySqlDbType.Int64).Value = .Rows(i).Cells(10).Value.ToString
                    cmd.Parameters.Add("@10", MySqlDbType.VarChar).Value = .Rows(i).Cells(11).Value.ToString()
                    cmd.Parameters.Add("@11", MySqlDbType.Text).Value = .Rows(i).Cells(12).Value.ToString()
                    cmd.Parameters.Add("@12", MySqlDbType.VarChar).Value = .Rows(i).Cells(13).Value.ToString()
                    cmd.Parameters.Add("@13", MySqlDbType.Text).Value = .Rows(i).Cells(14).Value.ToString()
                    cmd.Parameters.Add("@14", MySqlDbType.Int64).Value = .Rows(i).Cells(15).Value.ToString
                    cmd.Parameters.Add("@15", MySqlDbType.Text).Value = .Rows(i).Cells(16).Value.ToString()
                    LabelRowtoSync.Text = Val(LabelRowtoSync.Text + 1)
                    LabelZREADINVITEM.Text = Val(LabelZREADINVITEM.Text) + 1
                    POS.Instance.Invoke(Sub()
                                            POS.ProgressBar1.Value += 1
                                        End Sub)
                    ProgressBar1.Value = CInt(LabelRowtoSync.Text)
                    Label1.Text = "Syncing " & LabelRowtoSync.Text & " of " & LabelTTLRowtoSync.Text & " "
                    cmd.ExecuteNonQuery()
                    Dim sql = "UPDATE loc_zread_inventory SET `synced`='Synced' WHERE zreadinv_id = " & .Rows(i).Cells(0).Value.ToString
                    cmdloc = New MySqlCommand(sql, local)
                    cmdloc.ExecuteNonQuery()
                Next
                server.Close()
                local.Close()
                If WorkerCanceled = False Then
                    Dim t As New Task(New Action(Sub()
                                                     LabelZREADINV.Text = "Synced Zread Inventory"
                                                     LabelZREADINVTIME.Text = LabelTime.Text & " Seconds"
                                                 End Sub))
                    t.Start()
                End If
            End With
        Catch ex As Exception
            AuditTrail.LogToAuditTral("System", "Sync to cloud: " & ex.ToString, "Critical")

            Unsuccessful = True
            BackgroundWorkerSYNCTOCLOUD.CancelAsync()
            SendErrorReport(ex.ToString)
        End Try
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            If Button2.Text = "Start" Then
                If RadioButton1.Checked = True Then
                    ButtonSYNCDATA.PerformClick()
                ElseIf RadioButton2.Checked = True Then
                    ButtonSYNCINVENTORY.PerformClick()
                End If
            Else
                If RadioButton1.Checked = True Then
                    ButtonSYNCDATA.PerformClick()
                ElseIf RadioButton2.Checked = True Then
                    ButtonSYNCINVENTORY.PerformClick()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
            SendErrorReport(ex.ToString)
        End Try
    End Sub
End Class